' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC040    : 在庫引当
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMC040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC040DAC = New LMC040DAC()

    '2014.01.28 一括引当速度UP対応 追加START

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' 営業所
    ''' </summary>
    ''' <remarks></remarks>
    Private _Eigyo As String

    ''' <summary>
    ''' 倉庫
    ''' </summary>
    ''' <remarks></remarks>
    Private _Soko As String

    ''' <summary>
    ''' 他荷主モードフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _TaninusiFlg As String

    ''' <summary>
    ''' 出荷(小)件数
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutkaSCnt As Integer

    ''' <summary>
    ''' 格納変数（残個数）
    ''' </summary>
    ''' <remarks></remarks>
    Friend _numHikiZanCnt As Decimal = 0

    ''' <summary>
    ''' 格納変数（残数量）
    ''' </summary>
    ''' <remarks></remarks>
    Friend _numHikiZanAmt As Decimal = 0

    ''' <summary>
    ''' ロンザエラーフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnzErrFlg As String = "0"

    ''' <summary>
    ''' 遷移元PGID
    ''' </summary>
    ''' <remarks></remarks>
    Private _RootPgid As String

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

    'イベント種別
    Public Enum EventShubetsu As Integer

        TANINUSI = 0    '他荷主
        KENSAKU         '検索
        SENTAKU         '選択
        CLOSE           '閉じる
        DOUBLE_CLICK    'ダブルクリック
        CAL_KONSU       '梱数・端数変更
        CAL_SURYO       '数量変更
        CAL_IRIME       '入目変更
        CHANGE_SPREAD   'スプレッド変更
        SYOKI           '初期処理
        OPT_KOSU        '出荷単位（個数）
        OPT_SURYO       '出荷単位（数量）
        OPT_KOWAKE      '出荷単位（小分け）
        OPT_SAMPLE      '出荷単位（サンプル）

    End Enum

    '2014.01.28 一括引当速度UP対応 追加END

#End Region

#Region "Const"

    '2015.11.02 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region


#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf MyBase.GetLimitCount() < count Then
            'START YANAI EDIメモNo.69
            If ("01").Equals(ds.Tables("LMC040IN").Rows(0).Item("HIKIATE_FLG").ToString()) = True Then
                '自動引当時はワーニングポップを表示しない
                Return ds
            End If
            'END YANAI EDIメモNo.69

            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    '2013.02.13 要望番号1824 START
    ''' <summary>
    ''' 初期出荷マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'Return MyBase.CallDAC(_Dac, "SelectListData", ds)

        ds = MyBase.CallDAC(_Dac, "SelectListData", ds)

        'ロンザの一括引当の場合は商品明細マスタより有効期限日数を取得
        Dim zaiOutDs As DataTable = ds.Tables("LMC040OUT_ZAI")
        Dim inDs As DataTable = ds.Tables("LMC040IN")

        If zaiOutDs.Rows.Count > 0 Then

            If zaiOutDs.Rows(0).Item("NRS_BR_CD").Equals("10") = True AndAlso _
               zaiOutDs.Rows(0).Item("CUST_CD_L").Equals("00182") = True AndAlso _
               zaiOutDs.Rows(0).Item("CUST_CD_M").Equals("00") = True AndAlso _
               inDs.Rows(0).Item("HIKIATE_FLG").ToString().Equals("02") = True Then

                ds = MyBase.CallDAC(_Dac, "SelectMGoodsDetailList", ds)

            End If

        End If

        Return ds

    End Function
    '2013.02.13 要望番号1824 END

    ''' <summary>
    ''' 更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataTANINUSI(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectDataTANINUSI", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListDataTANINUSI(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListDataTANINUSI", ds)

    End Function
#End Region

#Region "設定処理"

#End Region

    '2014.01.28 一括引当速度UP対応 追加START
    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Function Main(ByVal dsHiki As DataSet) As DataSet

        '画面間データを取得する
        Me._PrmDs = dsHiki

        '営業所・倉庫の保存
        Dim inDr As DataRow = Me._PrmDs.Tables("LMC040IN").Rows(0)
        Me._Eigyo = inDr("NRS_BR_CD").ToString()
        Me._Soko = inDr("WH_CD").ToString()
        Me._TaninusiFlg = inDr("TANINUSI_FLG").ToString()
        Me._OutkaSCnt = Convert.ToInt32(inDr("OUTKA_S_CNT").ToString())

        Me._RootPgid = inDr("PGID").ToString()

        '★★★フォームを作成する前に引当処理判定を行う
        'Dim frm As LMC040F = Nothing

        '出荷検索画面以外からの遷移の場合、フォームを作成する
        If ("LMC010").Equals(Me._RootPgid) = True Then

            '個数の計算をする
            Call Me.SetCalSumForWithoutForm(LMC040BLC.EventShubetsu.SYOKI, Me._PrmDs, inDr("HIKIATE_FLG").ToString())

        Else

            ''2014.01.28 一括引当速度UP対応 手動コメントアウトSTART
            ''フォームの作成
            'frm = New LMC040F(Me)

            ''Validateクラスの設定
            'Me._V = New LMC040V(Me, frm)

            ''Gamenクラスの設定
            'Me._G = New LMC040G(Me, frm)

            ''フォームの初期化
            'MyBase.InitControl(frm)

            ''キーイベントをフォームで受け取る
            'frm.KeyPreview = True

            ''ファンクションキーの設定
            'Me._G.SetFunctionKey(Me._TaninusiFlg, Me._OutkaSCnt)

            ''タブインデックスの設定
            'Me._G.SetTabIndex()

            ''コントロール個別設定
            'Me._G.SetControl(MyBase.GetPGID())

            'Me._G.SetInitValue(frm)

            ''Validate共通クラスの設定
            'Me._LMCconV = New LMCControlV(Me, DirectCast(frm, Form))

            ''Hnadler共通クラスの設定
            'Me._LMCconH = New LMCControlH(DirectCast(frm, Form))

            ''Gamen共通クラスの設定
            'Me._LMCconG = New LMCControlG(Me, DirectCast(frm, Form))

            ''INの値を画面に表示
            'Me._G.SetInitForm(frm, Me._PrmDs)

            ''スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
            'Me._G.InitSpread()

            ''呼び元による分岐
            'If MyBase.RootPGID.Equals("LMD010") Then

            '    'オプションボタンロック制御
            '    frm.optCnt.Checked = True
            '    frm.optAmt.Enabled = False
            '    frm.optKowake.Enabled = False
            '    frm.optSample.Enabled = False

            'End If
            '2014.01.28 一括引当速度UP対応 手動コメントアウトEND

        End If

        If ("01").Equals(inDr("HIKIATE_FLG").ToString()) = True OrElse _
           ("02").Equals(inDr("HIKIATE_FLG").ToString()) = True OrElse _
           ("03").Equals(inDr("HIKIATE_FLG").ToString()) = True Then

            '自動引当時

            '自動引当処理を行う
            Dim rtnDs As DataSet = Me.SelectDataAutoHiki(Me._PrmDs)

            Dim openFlg As Boolean = False
            If rtnDs Is Nothing = True Then
                openFlg = True
            End If

            If openFlg = False Then
                If rtnDs.Tables("LMC040OUT").Rows.Count = 0 Then
                    openFlg = True
                End If
            End If

            If openFlg = True Then

                '自動引当時、エラーの場合
                If ("LMC010").Equals(Me._RootPgid) = True Then
                    ' ''2014.01.28 一括引当速度UP対応 一旦コメントアウトSTART
                    ''出荷検索画面から遷移の場合
                    ''リターンフラグにFalseをセット
                    'Me._Prm.ReturnFlg = False

                    ''画面を閉じる
                    'LMFormNavigate.Revoke(Me)
                    ' ''2014.01.28 一括引当速度UP対応 一旦コメントアウトEND

                    Return rtnDs
                    Exit Function
                Else

                    '2014.01.28 一括引当速度UP対応 手動コメントアウトSTART
                    ''出荷検索画面以外からの遷移の場合
                    ''画面の入力項目の制御
                    'Call _G.SetControlsStatus()

                    ''フォーカスの設定
                    'Call Me._G.SetFoucus()

                    ''呼び出し元画面情報を設定
                    'frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

                    ''フォームの表示
                    'frm.ShowDialog()

                    'Exit Function
                    '2014.01.28 一括引当速度UP対応 手動コメントアウトEND

                End If

            End If

            ''2014.01.28 一括引当速度UP対応 一旦コメントアウトSTART
            ''outのパラメータをセット
            'Me._Prm.ParamDataSet = rtnDs

            ''リターンフラグにTrueをセット
            'Me._Prm.ReturnFlg = True

            ''画面を閉じる
            'LMFormNavigate.Revoke(Me)
            ''2014.01.28 一括引当速度UP対応 一旦コメントアウトEND
            Return rtnDs
            Exit Function

        Else

            ''2014.01.28 一括引当速度UP対応 手動コメントアウトSTART
            ''手動引当時

            ''メッセージの表示
            'MyBase.ShowMessage(frm, "G007")

            ''検索処理を行う
            'Me.SelectData(frm, LMC040C.NEW_MODE, LMC040C.EventShubetsu.KENSAKU, Me._PrmDs)

            ''フォーカスの設定
            'Call Me._G.SetFoucus()

            ''画面の入力項目の制御
            'Call Me._G.SetControlsStatus()

            ''呼び出し元画面情報を設定
            'frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

            ''フォームの表示
            'frm.ShowDialog()

            ''カーソルを元に戻す
            'Cursor.Current = Cursors.Default
            ''2014.01.28 一括引当速度UP対応 手動コメントアウトEND
        End If

        Return Nothing

    End Function
    '2014.01.28 一括引当速度UP対応 追加END

    '2014.01.28 一括引当速度UP対応 追加START
    ''' <summary>
    ''' 個数・数量を求める
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCalSumForWithoutForm(ByVal eventShubetsu As LMC040BLC.EventShubetsu, ByVal prmDs As DataSet, _
                                       ByVal hikiFlg As String)

        With prmDs.Tables("LMC040IN")

            'START 2013/3/4 CSV引当対応（引当数量=入目・個数=1）
            If hikiFlg.Equals("03") = True Then
                '格納変数に計算済みの値を設定する（1固定）
                Me._numHikiZanCnt = 1
                Me._numHikiZanAmt = Convert.ToDecimal(.Rows(0)("IRIME").ToString())
                Exit Sub
            End If
            'END   2013/3/4 CSV引当対応（引当数量=入目・個数=1）

            Dim souAmt As Decimal = 0
            Dim souCnt As Decimal = 0
            Dim kosu As Decimal = 0
            Dim hasu As Decimal = 0

            Dim numHikiZanCnt As Decimal = Convert.ToDecimal(.Rows(0)("BACKLOG_NB").ToString())
            Dim numHikiZanAmt As Decimal = Convert.ToDecimal(.Rows(0)("BACKLOG_QT").ToString())
            Dim numHikiSumiCnt As Decimal = Convert.ToDecimal(.Rows(0)("ALCTD_NB").ToString())
            Dim numSyukkaKosu As Decimal = Convert.ToDecimal(.Rows(0)("KONSU").ToString())
            Dim numIrisu As Decimal = Convert.ToDecimal(.Rows(0)("PKG_NB").ToString())
            Dim numSyukkaHasu As Decimal = Convert.ToDecimal(.Rows(0)("HASU").ToString())
            Dim numHikiSumiAmt As Decimal = Convert.ToDecimal(.Rows(0)("ALCTD_QT").ToString())
            Dim numSyukkaSouAmt As Decimal = Convert.ToDecimal(.Rows(0)("SURYO").ToString())
            Dim numSyukkaSouCnt As Decimal = Convert.ToDecimal(.Rows(0)("KOSU").ToString())

            If kosu = 0 Then
                '出荷個数計算
                kosu = Convert.ToDecimal( _
                       Convert.ToDecimal(numSyukkaKosu) _
                     * Convert.ToDecimal(numIrisu) _
                     + Convert.ToDecimal(numSyukkaHasu))
            End If

            '値設定
            If souAmt <> 0 Then
                numSyukkaSouAmt = souAmt
            End If

            If souCnt <> 0 OrElse hasu <> 0 Then
                numSyukkaKosu = souCnt
                numSyukkaHasu = hasu
            ElseIf (LMC040BLC.EventShubetsu.KENSAKU).Equals(eventShubetsu) = False AndAlso _
                souCnt = 0 AndAlso _
                (.Rows(0)("ALCTD_KB").ToString().Equals("03") = True OrElse .Rows(0)("ALCTD_KB").ToString().Equals("04") = True) Then
                numSyukkaKosu = 1
                numSyukkaHasu = 0
                kosu = 1
            End If

            If .Rows(0)("ALCTD_KB").ToString().Equals("04") = True Then  'サンプルチェック時
                numSyukkaKosu = 0
                numSyukkaHasu = 0
                kosu = 0
            End If

            numHikiZanCnt = kosu - Convert.ToDecimal(numHikiSumiCnt)
            numHikiZanAmt = Convert.ToDecimal(numSyukkaSouAmt) - Convert.ToDecimal(numHikiSumiAmt)
            numSyukkaSouCnt = kosu

            '格納変数に計算済みの値を設定する
            Me._numHikiZanCnt = numHikiZanCnt
            Me._numHikiZanAmt = numHikiZanAmt

        End With

    End Sub
    '2014.01.28 一括引当速度UP対応 追加END

    '2014.01.28 一括引当速度UP対応 追加START
    ''' <summary>
    ''' 検索処理（自動引当）
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SelectDataAutoHiki(ByVal prmDs As DataSet) As DataSet

        '閾値の取得

        'Dim dr As DataRow

        ''2014.01.29 サーバーに取りに行くように修正が必要START
        'dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)
        ''2014.01.29 サーバーに取りに行くように修正が必要END

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectDataAutoHiki")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        If ("01").Equals(prmDs.Tables("LMC040IN").Rows(0).Item("TANINUSI_FLG").ToString()) = False Then

            If prmDs.Tables("LMC040IN").Rows(0).Item("PGID").ToString().Equals("LMC010") = True Then
                rtnDs = Me.SelectListDataHiki(prmDs)
            Else
                ''2014.01.28 一括引当速度UP対応 手動コメントアウトSTART
                'rtnDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
                '                                              "LMC040BLF", "SelectListData", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
                ''2014.01.28 一括引当速度UP対応 手動コメントアウトEND
            End If
        Else

            If prmDs.Tables("LMC040IN").Rows(0).Item("PGID").ToString().Equals("LMC010") = True Then
                rtnDs = Me.SelectListDataTANINUSI(prmDs)
            Else
                ''2014.01.28 一括引当速度UP対応 手動コメントアウトSTART
                'rtnDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
                '                                              "LMC040BLF", "SelectListDataTANINUSI", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
                ''2014.01.28 一括引当速度UP対応 手動コメントアウトEND
            End If
        End If

        Dim hikiDs As DataSet = New DataSet()

        '検索成功時、引当計算処理を行う
        If rtnDs Is Nothing = False Then

            '自動引当時のチェック
            If Me.IsAutoCheck(rtnDs, prmDs) = True Then
                'チェックがOKの場合はセット（遷移元が出荷編集、在庫振替それぞれの場合で、全量の意味合いが異なるので、ここで分岐）
                ''2014.01.28 一括引当速度UP対応 手動コメントアウトSTART
                'If ("LMC020").Equals(MyBase.RootPGID()) = True Then
                '    hikiDs = SetHikiateLMC020(frm, rtnDs, prmDs)
                'ElseIf ("LMD010").Equals(MyBase.RootPGID()) = True Then
                '    hikiDs = Me.SetHikiateLMD010(frm, rtnDs, prmDs)
                'ElseIf ("LMC010").Equals(MyBase.RootPGID()) = True Then

                If ("LMC010").Equals((prmDs.Tables("LMC040IN").Rows(0).Item("PGID").ToString())) = True Then
                    hikiDs = Me.SetHikiateLMC010(rtnDs, prmDs)
                    ''2014.01.28 一括引当速度UP対応 手動コメントアウトEND
                    If hikiDs Is Nothing = True Then

                        If Me._lnzErrFlg = "1" Then
                            ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定START
                            MyBase.SetMessage("E536", New String() {})
                            'MyBase.SetMessageStore("00", "E536", New String() {})
                            ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定END
                        ElseIf Me._lnzErrFlg = "2" Then
                            ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定START
                            MyBase.SetMessage("E537", New String() {})
                            'MyBase.SetMessageStore("00", "E537", New String() {})
                            ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定END
                        Else
                            ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定START
                            MyBase.SetMessage("E192")
                            'MyBase.SetMessageStore("00", "E192", New String() {})
                            ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定END
                        End If

                    ElseIf hikiDs.Tables("LMC040OUT").Rows.Count = 0 Then
                        ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定START
                        MyBase.SetMessage("E192")
                        'MyBase.SetMessageStore("00", "E192", New String() {})
                        ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定END
                    End If
                Else
                    ''2014.01.28 一括引当速度UP対応 手動コメントアウトSTART
                    'hikiDs = SetHikiateLMC020(frm, rtnDs, prmDs)
                    ''2014.01.28 一括引当速度UP対応 手動コメントアウトEND
                End If

            Else
                hikiDs = Nothing
                If ("LMC010").Equals((prmDs.Tables("LMC040IN").Rows(0).Item("PGID").ToString())) = True Then
                    ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定START
                    MyBase.SetMessage("E192")
                    'MyBase.SetMessageStore("00", "E192", New String() {})
                    ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定END
                End If
            End If

        Else
            hikiDs = Nothing
            If ("LMC010").Equals((prmDs.Tables("LMC040IN").Rows(0).Item("PGID").ToString())) = True Then
                ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定START
                MyBase.SetMessage("E192")
                'MyBase.SetMessageStore("00", "E192", New String() {})
                ''2014.01.28 一括引当速度UP対応 エラーメッセージ設定END
            End If
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectDataAutoHiki")

        Return hikiDs

    End Function
    '2014.01.28 一括引当速度UP対応 追加END

    '2014.01.28 一括引当速度UP対応 追加START
    ''' <summary>
    ''' 一括引当（遷移元が出荷検索の場合）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetHikiateLMC010(ByVal ds As DataSet, ByVal prmDs As DataSet) As DataSet

        'フォームを参照しないので、パラメータから直接値を取得する
        Dim syukkakosu As Decimal = Me._numHikiZanCnt
        Dim syukkasuryo As Decimal = Me._numHikiZanAmt

        Dim rtDs As DataSet = New LMC040DS()
        Dim dt As DataTable = rtDs.Tables("LMC040OUT")
        Dim indt As DataTable = prmDs.Tables("LMC040IN")
        Dim outDr As DataRow = dt.NewRow()
        Dim indt2 As DataTable = prmDs.Tables("LMC040IN2")

        If indt2 Is Nothing = False Then
            Dim lngcnt2 As Integer = indt2.Rows.Count - 1
            Dim zaiDr() As DataRow = Nothing
            For i As Integer = 0 To lngcnt2
                '2014.03.14 修正START

                'zaiDr = ds.Tables("LMC040OUT_ZAI").Select(String.Concat("ZAI_REC_NO = '", indt2.Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                zaiDr = ds.Tables("LMC040OUT_ZAI").Select(String.Concat("ZAI_REC_NO = '", indt2.Rows(i).Item("ZAI_REC_NO").ToString(), _
                "' AND OUTKA_NO_L = '", indt.Rows(0).Item("OUTKA_NO_L").ToString(), _
                "'"))

                '2014.03.14 修正END
                If 0 < zaiDr.Length Then
                    zaiDr(0).Item("ALCTD_NB") = indt2.Rows(i).Item("ALCTD_NB").ToString()
                    zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) - _
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_NB").ToString()))
                    zaiDr(0).Item("ALCTD_QT") = indt2.Rows(i).Item("ALCTD_QT").ToString()
                    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) - _
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_QT").ToString()))
                    If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) = True OrElse ("0").Equals(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) = True Then
                        ds.Tables("LMC040OUT_ZAI").Rows.Remove(zaiDr(0))
                    End If
                End If
            Next
        End If
        'END YANAI 20110914 一括引当対応

        Dim ofbFlg As Boolean = False

        'START YANAI 要望番号1239 一括引当時にエラー
        '行数設定
        Dim lngcnt As Integer = ds.Tables("LMC040OUT_ZAI").Rows.Count - 1
        'END YANAI 要望番号1239 一括引当時にエラー

#If False Then   'ADD 2020/05/22 007999 
        Dim JJ_FLG As String = indt.Rows(0)("JJ_FLG").ToString()
        Dim sGOODS_COND_KB_3 As String = indt.Rows(0)("JJ_KAKUTEI").ToString()
#End If

        '値設定
        For i As Integer = 0 To lngcnt

            If ("02").Equals(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OFB_KB")) = True Then
                '簿外品の時は引当しない
                ofbFlg = True
                Exit For
            End If

            '2013.02.13 要望番号1824 START
            '千葉　ロンザの場合、
            If ("10").Equals(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("NRS_BR_CD")) = True AndAlso _
               ("00182").Equals(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("CUST_CD_L")) = True AndAlso _
               ("00").Equals(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("CUST_CD_M")) = True Then

                '①割当優先区分が"リザーブ"の場合は引当しない
                If ("20").Equals(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_PRIORITY")) = True Then
                    Me._lnzErrFlg = "0"
                    Exit For
                End If

                '②商品明細マスタが存在する場合
                If ds.Tables("LMC040_M_GOODS_DETAILS").Rows.Count > 0 Then

                    '賞味期限 < 有効期限日数 + 出荷日　の場合は引当しない
                    If Convert.ToDecimal(ds.Tables("LMC040_M_GOODS_DETAILS").Rows(0).Item("SET_NAIYO")) > 0 Then

                        Dim outkaPlanDate As String = ds.Tables("LMC040_M_GOODS_DETAILS").Rows(0).Item("OUTKA_PLAN_DATE").ToString()
                        Dim eDate As String = Convert.ToString(DateAdd("d", Convert.ToDecimal(ds.Tables("LMC040_M_GOODS_DETAILS").Rows(0).Item("SET_NAIYO")), _
                                                         String.Concat(Left(outkaPlanDate, 4), "/", Mid(outkaPlanDate, 5, 2), "/", Mid(outkaPlanDate, 7, 2))))

                        eDate = String.Concat(Left(eDate, 4), Mid(eDate, 6, 2), Mid(eDate, 9, 2))
                        If Convert.ToString(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("LT_DATE")) < eDate Then
                            Me._lnzErrFlg = "2"
                            Exit For
                        End If

                    End If

                End If

            End If
            '2013.02.13 要望番号1824 END

            'START YANAI 要望番号1200 自動引当・一括引当変更
            If String.IsNullOrEmpty(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_KB_1").ToString) = False Then
                '商品状態区分(中身)が設定されている場合は、自動引当対象外
                Continue For
            End If

            If String.IsNullOrEmpty(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_KB_2").ToString) = False Then
                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                Continue For
            End If

            If ("1.000").Equals(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SPD_KB_FLG").ToString) = False Then
                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                Continue For
            End If
            'END YANAI 要望番号1200 自動引当・一括引当変更

#If False Then   'ADD 2020/05/22 007999 
            If JJ_FLG.Equals("1") Then
                'ジョンソン&ジョンソンs専用
                If (sGOODS_COND_KB_3).Equals(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_KB_3").ToString) = False Then
                    '状態 荷主が設定されている値以外は、自動引当・一括引当対象外
                    Continue For
                End If
            End If
#End If

            outDr("ZAI_REC_NO") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ZAI_REC_NO")
            outDr("TOU_NO") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("TOU_NO")
            outDr("SITU_NO") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SITU_NO")
            outDr("ZONE_CD") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ZONE_CD")
            outDr("LOCA") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("LOCA")
            outDr("LOT_NO") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("LOT_NO")
            outDr("INKA_NO_L") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("INKA_NO_L")
            outDr("INKA_NO_M") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("INKA_NO_M")
            outDr("INKA_NO_S") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("INKA_NO_S")
            outDr("ALLOC_PRIORITY") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_PRIORITY")
            outDr("RSV_NO") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("RSV_NO")
            outDr("SERIAL_NO") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SERIAL_NO")
            outDr("HOKAN_YN") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("HOKAN_YN")
            outDr("TAX_KB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("TAX_KB")
            outDr("GOODS_COND_KB_1") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_KB_1")
            outDr("GOODS_COND_KB_2") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_KB_2")
            outDr("GOODS_COND_KB_3") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_KB_3")
            outDr("OFB_KB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OFB_KB")
            outDr("SPD_KB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SPD_KB")
            outDr("REMARK_OUT") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("REMARK_OUT")
            outDr("PORA_ZAI_NB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("PORA_ZAI_NB")

            outDr("IRIME") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("IRIME")
            outDr("PORA_ZAI_QT") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("PORA_ZAI_QT")
            outDr("INKO_DATE") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("INKO_DATE")
            outDr("INKO_PLAN_DATE") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("INKO_PLAN_DATE")
            outDr("ZERO_FLAG") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ZERO_FLAG")
            outDr("LT_DATE") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("LT_DATE")
            outDr("GOODS_CRT_DATE") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_CRT_DATE")
            outDr("DEST_CD_P") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("DEST_CD_P")
            outDr("REMARK") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("REMARK")
            outDr("SMPL_FLAG") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SMPL_FLAG")
            outDr("GOODS_COND_NM_1") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_NM_1")
            outDr("GOODS_COND_NM_2") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_NM_2")
            outDr("GOODS_COND_NM_3") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_COND_NM_3")
            outDr("ALLOC_PRIORITY_NM") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_PRIORITY_NM")
            outDr("OFB_KB_NM") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OFB_KB_NM")
            outDr("SPD_KB_NM") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SPD_KB_NM")
            outDr("GOODS_CD_NRS_FROM") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_CD_NRS_FROM")
            outDr("KONSU") = "-1"
            outDr("HASU") = "-1"
            outDr("SURYO") = "-1"
            outDr("ALCTD_KB") = "-1"
            'outDr("OUTKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_NO_S")
            outDr("BUYER_ORD_NO_DTL") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("BUYER_ORD_NO_DTL")
            outDr("SERIAL_NO_L") = "-1"
            outDr("RSV_NO_L") = "-1"
            outDr("LOT_NO_L") = "-1"
            outDr("IRIME_L") = "-1"

            '引当個数を求める
            If String.IsNullOrEmpty(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB").ToString()) = False Then
                If syukkakosu < Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB").ToString()) Then
                    '出荷個数 < 引当可能個数 の時
                    outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_NB"))
                    outDr("HIKI_KOSU") = syukkakosu
                    outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_NB")) + syukkakosu
                    syukkakosu = 0
                Else
                    '引当可能個数 < 出荷個数 の時
                    outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_NB"))
                    outDr("HIKI_KOSU") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB").ToString())
                    outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_NB")) + _
                                        Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB").ToString())
                    syukkakosu = syukkakosu - Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB").ToString())
                End If
            End If

            '引当数量を求める
            If String.IsNullOrEmpty(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT").ToString()) = False Then
                If syukkasuryo < Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT").ToString()) Then
                    '出荷個数 < 引当可能個数 の時
                    outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_QT"))
                    outDr("HIKI_SURYO") = syukkasuryo
                    outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_QT")) + _
                                        syukkasuryo
                    syukkasuryo = 0
                Else
                    '引当可能個数 < 出荷個数 の時
                    outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_QT"))
                    outDr("HIKI_SURYO") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT").ToString())
                    outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_QT")) + _
                                        Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT").ToString())
                    syukkasuryo = syukkasuryo - Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT").ToString())
                End If
            End If

            If ("04").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                'サンプルの時
                outDr("ALCTD_NB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_NB")
                outDr("ALCTD_QT") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALCTD_QT")
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT"))
                outDr("ALLOC_CAN_NB_HOZON") = "0"
                outDr("ALLOC_CAN_QT_HOZON") = "0"
                syukkakosu = 0
                syukkasuryo = 0
            ElseIf ("03").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                '小分けの時
                outDr("ALLOC_CAN_NB") = "1"
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT").ToString)
                outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT"))
            Else
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("ALCTD_NB")) + Convert.ToInt32(outDr("ALCTD_NB_HOZON"))
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT")) + Convert.ToDecimal(outDr("ALCTD_QT_HOZON"))
                outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT"))
            End If

            outDr("ALCTD_KOSU") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_NB")
            outDr("ALCTD_SURYO") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("ALLOC_CAN_QT")

            outDr("GOODS_CD_CUST") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_CD_CUST")
            outDr("NM_1") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_NM_1")
            outDr("OUTKA_ATT") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OUTKA_ATT")
            outDr("SEARCH_KEY_1") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SEARCH_KEY_1")
            outDr("UNSO_ONDO_KB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("UNSO_ONDO_KB")
            outDr("PKG_UT") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("PKG_UT")
            outDr("STD_IRIME_NB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("STD_IRIME_NB")
            outDr("STD_WT_KGS") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("STD_WT_KGS")
            outDr("TARE_YN") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("TARE_YN")
            outDr("HIKIATE_ALERT_YN") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("HIKIATE_ALERT_YN")
            outDr("STD_IRIME_UT") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("STD_IRIME_UT")
            outDr("PKG_NB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("PKG_NB")
            outDr("NB_UT_NM") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("NB_UT_NM")
            outDr("IRIME_UT_NM") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("IRIME_UT_NM")
            outDr("GOODS_CD_NRS") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_CD_NRS")
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            outDr("GOODS_KANRI_NO") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("GOODS_KANRI_NO")
            'END   ADD 2013/09/10 KOBAYASHI WIT対応
            outDr("CUST_CD_S") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("CUST_CD_S")
            outDr("CUST_CD_SS") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("CUST_CD_SS")
            outDr("IDO_DATE") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("IDO_DATE")
            outDr("INKA_DATE") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("INKA_DATE")
            outDr("HOKAN_STR_DATE") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("HOKAN_STR_DATE")
            outDr("COA_YN") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("COA_YN")
            outDr("OUTKA_KAKO_SAGYO_KB_1") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OUTKA_KAKO_SAGYO_KB_1")
            outDr("OUTKA_KAKO_SAGYO_KB_2") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OUTKA_KAKO_SAGYO_KB_2")
            outDr("OUTKA_KAKO_SAGYO_KB_3") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OUTKA_KAKO_SAGYO_KB_3")
            outDr("OUTKA_KAKO_SAGYO_KB_4") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OUTKA_KAKO_SAGYO_KB_4")
            outDr("OUTKA_KAKO_SAGYO_KB_5") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("OUTKA_KAKO_SAGYO_KB_5")
            outDr("SIZE_KB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SIZE_KB")
            outDr("NB_UT") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("NB_UT")
            'START YANAI 要望番号499
            outDr("CUST_CD_L_GOODS") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("CUST_CD_L_GOODS")
            outDr("CUST_CD_M_GOODS") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("CUST_CD_M_GOODS")
            'END YANAI 要望番号499
            'START YANAI 要望番号780
            outDr("INKA_DATE_KANRI_KB") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("INKA_DATE_KANRI_KB")
            'END YANAI 要望番号780

            outDr("SYS_UPD_DATE") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SYS_UPD_DATE")
            outDr("SYS_UPD_TIME") = ds.Tables("LMC040OUT_ZAI").Rows(i).Item("SYS_UPD_TIME")

            '設定値をデータセットに設定
            rtDs.Tables("LMC040OUT").Rows.Add(outDr)
            If syukkakosu = 0 AndAlso syukkasuryo = 0 Then
                '出荷個数・出荷数量分、引当終わったら、処理を抜ける
                Exit For
            End If

            outDr = dt.NewRow()

        Next

        If ofbFlg = True Then
            'ofbFlg = Trueの場合は、実質引当対象がなかった場合
            Return Nothing

        ElseIf Me._lnzErrFlg <> "0" Then
            Return Nothing
        Else
            Return rtDs
        End If

    End Function
    '2014.01.28 一括引当速度UP対応 追加END

    '2014.01.28 一括引当速度UP対応 追加START
    ''' <summary>
    ''' 入力チェック（自動引当時のチェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsAutoCheck(ByVal rDs As DataSet, ByVal prmDs As DataSet) As Boolean

        '【一覧チェック】
        Dim max As Integer = rDs.Tables("LMC040OUT_ZAI").Rows.Count - 1
        Dim kanoKosu As Decimal = 0
        Dim kanoSuryo As Decimal = 0

        Dim indt2 As DataTable = prmDs.Tables("LMC040IN")
        If ("03").Equals(indt2.Rows(0).Item("ALCTD_KB").ToString()) = True Or _
             ("04").Equals(indt2.Rows(0).Item("ALCTD_KB").ToString()) = True Then

            With rDs.Tables("LMC040OUT_ZAI")

                Dim indt As DataTable = prmDs.Tables("LMC040IN2")
                Dim indr() As DataRow = Nothing

                For i As Integer = 0 To max

                    If Convert.ToDouble(.Rows(i).Item("INKA_STATE_KB")) < 50 Then
                        '次の行へ
                        Continue For

                    Else
                        kanoKosu = kanoKosu + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_NB"))
                        kanoSuryo = kanoSuryo + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_QT"))

                        indr = indt.Select(String.Concat("ZAI_REC_NO = '", .Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                        If 0 < indr.Length Then
                            kanoKosu = kanoKosu - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_NB").ToString())
                            kanoSuryo = kanoSuryo - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_QT").ToString())
                        End If

                    End If

                Next i

            End With

        Else

            With rDs.Tables("LMC040OUT_ZAI")
                Dim indt As DataTable = prmDs.Tables("LMC040IN2")
                Dim maxIndt As Integer = -1
                '2014.03.14 追加START
                If indt.Rows.Count <> 0 Then
                    maxIndt = prmDs.Tables("LMC040IN2").Rows.Count - 1
                End If
                '2014.03.14 追加END
                Dim indr() As DataRow = Nothing

                '2014.03.14 修正START
                If maxIndt = -1 Then

                    For i As Integer = 0 To max

                        kanoKosu = kanoKosu + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_NB"))
                        kanoSuryo = kanoSuryo + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_QT"))

                        indr = indt.Select(String.Concat("ZAI_REC_NO = '", .Rows(i).Item("ZAI_REC_NO").ToString(), "'"))

                        If 0 < indr.Length Then
                            kanoKosu = kanoKosu - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_NB").ToString())
                            kanoSuryo = kanoSuryo - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_QT").ToString())
                        End If

                    Next i

                Else

                    For i As Integer = 0 To max

                        kanoKosu = kanoKosu + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_NB"))
                        kanoSuryo = kanoSuryo + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_QT"))

                        ''2014.03.28 コメントSTART
                        'For j As Integer = 0 To maxIndt

                        '    '2014.03.14 修正START
                        '    'indr = indt.Select(String.Concat("ZAI_REC_NO = '", .Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                        '    indr = indt.Select(String.Concat("ZAI_REC_NO = '", prmDs.Tables("LMC040IN2").Rows(j).Item("ZAI_REC_NO").ToString(), _
                        '                                     "' AND OUTKA_NO_L = '", prmDs.Tables("LMC040IN").Rows(0).Item("OUTKA_NO_L").ToString(), _
                        '                                     "'"))
                        '    '2014.03.14 修正END
                        '    If 0 < indr.Length Then
                        '        kanoKosu = kanoKosu - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_NB").ToString())
                        '        kanoSuryo = kanoSuryo - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_QT").ToString())
                        '    End If

                        'Next j
                        ''2014.03.28 コメントEND

                    Next i

                End If
                '2014.03.14 修正END

            End With

        End If

        If prmDs.Tables("LMC040IN").Rows(0).Item("PGID").ToString().Equals("LMC010") = True Then

            With prmDs.Tables("LMC040IN")
                '引当残個数 + 引当可能個数の合計

                If kanoKosu < Me._numHikiZanCnt Then
                    'MyBase.ShowMessage("E192")
                    Return False
                End If

                '引当残数量 + 引当可能数量の合計
                If kanoSuryo < Me._numHikiZanAmt Then
                    'MyBase.ShowMessage("E192")
                    Return False
                End If
            End With
        Else

            ''2014.01.28 一括引当速度UP対応 手動コメントアウトSTART
            'With Me._Frm

            '    '引当残個数 + 引当可能個数の合計
            '    If kanoKosu < Convert.ToDecimal(.numHikiZanCnt.Value) Then
            '        MyBase.SetMessageStore("00", "E192")
            '        Return False
            '    End If

            '    '引当残数量 + 引当可能数量の合計
            '    If kanoSuryo < Convert.ToDecimal(.numHikiZanAmt.Value) Then
            '        MyBase.SetMessageStore("00", "E192")
            '        Return False
            '    End If

            'End With
            ''2014.01.28 一括引当速度UP対応 手動コメントアウトEND

        End If

        Return True

    End Function
    '2014.01.28 一括引当速度UP対応 追加END

    '2014.01.28 一括引当速度UP対応 追加START
    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListDataHiki(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        If ("02").Equals(ds.Tables("LMC040IN").Rows(0).Item("HIKIATE_FLG").ToString()) = True Then
            rtnDs = Me.SelectListData(ds)
            If rtnDs.Tables("LMC040OUT_ZAI").Rows.Count < 1 Then
                MyBase.SetMessage("G001")
            End If
        Else

            ''2014.01.29 強制実行フラグが取得できない為　一旦コメントSTART
            ''強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
            'If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = Me.SelectData(ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'End If
            ''2014.01.29 強制実行フラグが取得できない為　一旦コメントEND

            '検索結果取得
            rtnDs = Me.SelectListData(ds)

        End If

        Return rtnDs

    End Function
    '2014.01.28 一括引当速度UP対応 追加END

#End Region

End Class
