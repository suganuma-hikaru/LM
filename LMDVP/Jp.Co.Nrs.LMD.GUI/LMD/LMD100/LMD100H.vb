' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMD100C : 在庫テーブル照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMD100ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD100H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD100V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD100G

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 検索結果を保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Dim _RtnDs As DataSet

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconV As LMDControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconH As LMDControlH

    ''' <summary>
    ''' Gamen共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG

    ''' <summary>
    ''' 初期検索件数格納用
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiN As Integer

    ''' <summary>
    ''' 遷移元画面設定営業所コード格納用
    ''' </summary>
    ''' <remarks></remarks>
    Private _BrCd As String

    ''' <summary>
    ''' 遷移元画面設定倉庫コード格納用
    ''' </summary>
    ''' <remarks></remarks>
    Private _WhCd As String

    ''' <summary>
    ''' 遷移元画面設定届先コード格納用
    ''' </summary>
    ''' <remarks></remarks>
    Private _DestCd As String

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMD100DS

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'フォームの作成
        Dim frm As LMD100F = New LMD100F(Me)

        'Validateクラスの設定
        Me._V = New LMD100V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMD100G(Me, frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'フォームの初期化
        Me.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD100C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID())

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        Call Me._G.SetInitValue(frm, Me._PrmDs)

        'Hnadler共通クラスの設定
        Me._LMDconH = New LMDControlH(DirectCast(frm, Form), MyBase.GetPGID())

        '↓ データ取得の必要があればここにコーディングする。

        '遷移元画面設定営業所コード取得
        Me._BrCd = Me._PrmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()

        '遷移元画面設定倉庫コード取得
        Me._WhCd = Me._PrmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows(0).Item("WH_CD").ToString()

        '遷移元画面設定届先コード取得
        Me._DestCd = Me._PrmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows(0).Item("DEST_CD").ToString()

        '初期検索フラグの取得
        Dim shokiKensakuFlg As String = Me._PrmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows(0).Item("DEFAULT_SEARCH_FLG").ToString()

        '初期検索件数格納用フィールド変数初期化
        Me._ShokiN = 0

        'ヘッダ部表示
        If Me._G.SetInitForm(frm, Me._PrmDs) = False Then
            '荷主マスタから荷主名称が取得できなかった場合、エラーを表示し処理終了
            '2015.10.22 tusnehira add
            '英語化対応
            MyBase.ShowMessage(frm, "E773")
            'MyBase.ShowMessage(frm, "E079", New String() {"荷主マスタ", "荷主コード"})

            '画面の入力項目の制御
            _G.SetControlsStatus()

            'フォーカスの設定
            _G.SetFoucus()

            '呼び出し元画面情報を設定
            frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

            'フォームの表示
            frm.ShowDialog()

            'カーソルを元に戻す
            Cursor.Current = Cursors.Default

            '処理終了
            Exit Sub

        End If

        ''請求鑑検索処理
        'Dim rootPgId As String = MyBase.RootPGID()
        'If rootPgId IsNot Nothing AndAlso rootPgId.Equals("LMC020") = True Then
        '    Me._Ds = Me.SelectFuriGoodsData(frm)
        'End If

        '初期検索件数が1件より多い場合、画面を表示（0件の時は他荷主処理の流れになるので開かない）
        If shokiKensakuFlg = "1" Then

            '初期検索
            Me.SelectData(frm, LMD100C.SearchShubetsu.FIRST_SEARCH, _PrmDs)

        ElseIf shokiKensakuFlg = "0" Then '初期検索フラグ = 0 の場合、検索をせずにメッセージを表示
            MyBase.ShowMessage(frm, "G007")

        Else                              '上記以外の場合、検索をせずにメッセージを表示
            MyBase.ShowMessage(frm, "G007")

        End If

        '↑ データ取得の必要があればここにコーディングする。

        '初期検索件数が1件より多い場合、画面を表示（0件の時は他荷主処理の流れになるので開かない）
        If 1 < Me._ShokiN OrElse
           (Me._ShokiN = 0 AndAlso ("01").Equals(Me._PrmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows(0).Item("ZERO_SEARCH_FLG").ToString()) = True) Then

            '画面の入力項目の制御
            _G.SetControlsStatus()

            'フォーカスの設定
            _G.SetFoucus()

            '呼び出し元画面情報を設定
            frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

            'フォームの表示
            frm.ShowDialog()

        End If

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMD100F, ByVal reFlg As LMD100C.SearchShubetsu, ByVal ds As DataSet)

        '検索結果格納用フィールドの初期化
        Me._RtnDs = Nothing

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)
        Dim maxNum As Integer = Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1")))

        'DataSet設定
        Dim rtDs As DataSet = New LMD100DS()
        rtDs = Me.SetInDataSet(frm, reFlg)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
     
        '検索時WSAクラス呼び出し
        Dim rtnDs As DataSet = Me._LMDconH.CallWSAAction(DirectCast(frm, Form) _
                                                     , "LMD100BLF", "SelectListData", rtDs _
                                                     , maxNum)
   
        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs, reFlg, ds)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

    End Sub

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataSet(ByVal frm As LMD100F, ByVal reFlg As LMD100C.SearchShubetsu) As DataSet

        '格納用データセット
        Dim ds As DataSet = New LMD100DS()

        '検索種別によるデータセット設定分岐
        Select Case reFlg

            Case LMD100C.SearchShubetsu.FIRST_SEARCH   '初期検索時

                '初期検索条件を格納
                ds = Me._PrmDs.Copy

            Case LMD100C.SearchShubetsu.NEW_SEARCH    '通常検索時

                '初期検索条件を格納
                ds = Me._PrmDs.Copy
                '検索条件格納データ行取得
                Dim dr As DataRow = ds.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows(0)

                'ヘッダ部営業所コード取得
                dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()

                'ヘッダ部倉庫コード取得
                dr.Item("WH_CD") = frm.cmbSoko.SelectedValue.ToString()

                'Spread検索行の値を格納
                With frm.sprZai.Sheets(0)

                    dr.Item("GOODS_CD_CUST") = .Cells(0, LMD100G.sprDetailDef.GOODS_CD_CUST.ColNo).Value         '荷主商品コード
                    '(2013.01.11)要望番号1700 -- START --
                    'dr.Item("GOODS_NM") = .Cells(0, LMD100G.sprDetailDef.GOODS_NM.ColNo).Value                  '商品名
                    'dr.Item("LOT_NO") = .Cells(0, LMD100G.sprDetailDef.LOT_NO.ColNo).Value                      'ロットNo.
                    'dr.Item("GOODS_NM") = .Cells(0, LMD100G.sprDetailDef.GOODS_NM.ColNo).Value.ToString.Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")  '商品名
                    'dr.Item("LOT_NO") = .Cells(0, LMD100G.sprDetailDef.LOT_NO.ColNo).Value.ToString.Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")      'ロットNo.
#If False Then  'UPD 2020/0624 012642   引き当て時に古いロット（入庫日）がわからない テストで商品名をクリアして検索すると、この箇所でエラーになる対応
                   dr.Item("GOODS_NM") = .Cells(0, LMD100G.sprDetailDef.GOODS_NM.ColNo).Value.ToString.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")  '商品名    '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明

#Else
                    If Len(.Cells(0, LMD100G.sprDetailDef.GOODS_NM.ColNo).Value) = 0 Then
                        dr.Item("GOODS_NM") = String.Empty
                    Else
                        dr.Item("GOODS_NM") = .Cells(0, LMD100G.sprDetailDef.GOODS_NM.ColNo).Value.ToString.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")
                    End If
#End If
                    dr.Item("LOT_NO") = .Cells(0, LMD100G.sprDetailDef.LOT_NO.ColNo).Value.ToString.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")      'ロットNo. '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
                    '(2013.01.11)要望番号1700 --  END  --
                    dr.Item("IRIME") = .Cells(0, LMD100G.sprDetailDef.IRIME.ColNo).Value                         '入目
                    dr.Item("IRIME_UT") = .Cells(0, LMD100G.sprDetailDef.IRIME_UT.ColNo).Value                   '入目単位
                    dr.Item("NB_UT") = .Cells(0, LMD100G.sprDetailDef.NB_UT.ColNo).Value                         '個数単位
                    dr.Item("REMARK") = .Cells(0, LMD100G.sprDetailDef.REMARK.ColNo).Value                       '備考小（社内）
                    dr.Item("REMARK_OUT") = .Cells(0, LMD100G.sprDetailDef.REMARK_OUT.ColNo).Value               '備考小（社外）
                    dr.Item("TAX_KB") = .Cells(0, LMD100G.sprDetailDef.TAX_KB.ColNo).Value                       '税区分
                    dr.Item("HIKIATE_ALERT_YN") = .Cells(0, LMD100G.sprDetailDef.HIKIATE_ALERT_NM.ColNo).Value   '引当注意品
                    dr.Item("DEST_NM") = .Cells(0, LMD100G.sprDetailDef.DEST_NM.ColNo).Value
                    dr.Item("OUTKA_FROM_ORD_NO_L") = .Cells(0, LMD100G.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo).Value

                End With

        End Select

        'Dim dt As DataTable = Me._Ds.Tables(LMD100C.TABLE_NM_FURIGOODS)
        'Dim max As Integer = dt.Rows.Count - 1
        'Dim row As DataRow = Nothing

        'For i As Integer = 0 To max
        '    row = ds.Tables(LMD100C.TABLE_NM_FURIGOODS).NewRow
        '    row("CUST_CD_L") = Me._Ds.Tables(LMD100C.TABLE_NM_FURIGOODS).Rows(i).Item("CUST_CD_L").ToString()
        '    ds.Tables(LMD100C.TABLE_NM_FURIGOODS).Rows.Add(row)

        'Next

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(Spread選択行格納)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="reflg"></param>
    ''' <remarks></remarks>
    Private Sub SetOutDataSet(ByVal dr As DataRow, ByVal reflg As LMD100C.EventShubetsu)

        '「選択」処理時、ロット番号と小分けフラグは設定しない
        If reflg = LMD100C.EventShubetsu.SENTAKU Then

            'ロット番号と小分けフラグに空白を設定
            dr.Item("LOT_NO") = String.Empty
            dr.Item("SMPL") = String.Empty

        End If

        '画面間パラメータに選択データを設定
        _Prm.ParamDataSet.Tables(LMControlC.LMD100C_TABLE_NM_OUT).ImportRow(dr)

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMD100F, ByVal ds As DataSet, ByVal reFlg As LMD100C.SearchShubetsu, ByVal prmDs As DataSet)

        'SPREAD(表示行)初期化
        frm.sprZai.CrearSpread()

        '検索結果をフィールド変数に保存
        Me._RtnDs = ds.Copy

        '検索結果件数取得
        Dim resultCount As Integer = ds.Tables("LMD100OUT").Rows.Count

        '検索種別によるデータセット設定分岐
        Select Case reFlg

            Case LMD100C.SearchShubetsu.FIRST_SEARCH  '初期検索時

                '件数をフィールド変数に格納
                Me._ShokiN = resultCount

                'START YANAI 要望番号389
                If 0 < resultCount Then
                    '画面解除
                    Call MyBase.UnLockedControls(frm)

                    'Spreadに取得結果を表示
                    resultCount = Me._G.SetSpread(ds, prmDs)

                    'START YANAI 要望番号1069 他荷主引当時に引当画面に遷移しないパターンが存在する
                    '件数をフィールド変数に格納
                    Me._ShokiN = resultCount
                    'END YANAI 要望番号1069 他荷主引当時に引当画面に遷移しないパターンが存在する
                End If
                'END YANAI 要望番号389

                '検索結果が0件の場合
                If resultCount <= 1 Then

                    'START YANAI 要望番号389
                    If resultCount = 0 Then
                        'outのパラメータをセット
                        Dim rtDs As DataSet = New LMD100DS()
                        Me._Prm.ParamDataSet = rtDs
                    Else
                        'outのパラメータをセット
                        Me._Prm.ParamDataSet = ds
                    End If
                    'END YANAI 要望番号389

                    'リターンフラグにTrueをセット
                    Me._Prm.ReturnFlg = True

                    '画面を閉じる
                    LMFormNavigate.Revoke(Me)
                    Exit Sub

                Else '検索結果が2件以上の場合

                    'START YANAI 要望番号389
                    ''画面解除
                    'Call MyBase.UnLockedControls(frm)

                    ''Spreadに取得結果を表示
                    'resultCount = Me._G.SetSpread(ds, prmDs)
                    'END YANAI 要望番号389

                    '処理結果表示
                    If resultCount = 0 Then
                        MyBase.ShowMessage(frm, "G001")
                    Else
                        MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(resultCount)})
                    End If

                End If

                'START YANAI 要望番号389
                '件数をフィールド変数に格納
                Me._ShokiN = frm.sprZai.ActiveSheet.Rows.Count
                'END YANAI 要望番号389

            Case LMD100C.SearchShubetsu.NEW_SEARCH    '通常検索時

                '画面解除
                Call MyBase.UnLockedControls(frm)

                '取得データをSPREADに表示
                resultCount = Me._G.SetSpread(ds, prmDs)

                'START YANAI 要望番号1069 他荷主引当時に引当画面に遷移しないパターンが存在する
                '件数をフィールド変数に格納
                Me._ShokiN = resultCount
                'END YANAI 要望番号1069 他荷主引当時に引当画面に遷移しないパターンが存在する

                '処理結果表示
                If resultCount = 0 Then
                    MyBase.ShowMessage(frm, "G001")
                Else
                    MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(resultCount)})
                End If

        End Select

    End Sub

    ''' <summary>
    ''' 選択データを取得
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSpreadData(ByVal frm As LMD100F, ByVal index As Integer) As DataRow

        'Spread行データ格納用
        Dim dr As DataRow() = Nothing

        '選択行の在庫レコード番号を取得
        Dim zaiRecNo As String = frm.sprZai.Sheets(0).Cells(index, LMD100G.sprDetailDef.ZAI_REC_NO.ColNo).Value.ToString()

        '選択行データを検索結果格納用フィールド変数より取得
        dr = Me._RtnDs.Tables(LMControlC.LMD100C_TABLE_NM_OUT).Select(String.Concat("ZAI_REC_NO = '", zaiRecNo, "'"))

        Return dr(0)

    End Function

    ''' <summary>
    ''' 初期検索時WSAクラス呼出
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="BLF">BLFファイル名</param>
    ''' <param name="methodName">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <remarks></remarks>
    Friend Function FirstSearchCallWSAAction(ByRef frm As Form, ByVal BLF As String, _
                             ByVal methodName As String, ByRef rtDs As DataSet, ByVal rc As Integer) As DataSet

        '閾値の設定
        MyBase.SetLimitCount(rc)

        Dim rtnDs As DataSet = MyBase.CallWSA(BLF, methodName, rtDs)

        '初期検索件数が1件以外の場合、画面を表示
        If MyBase.GetResultCount <> 1 Then

            '画面の入力項目の制御
            _G.SetControlsStatus()

            'フォーカスの設定
            _G.SetFoucus()

            'フォームの表示
            frm.Show()

        End If

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            '画面の入力項目の制御
            Me._G.SetControlsStatus()

            'フォーカスの設定
            Me._G.SetFoucus()

            'フォームの表示
            frm.Show()

            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(BLF, methodName, rtDs)

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                    '検索成功時
                    Return rtnDs

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '検索失敗時、共通処理を行う
                    Call Me._LMDconH.FailureSelect(frm)
                    Return Nothing

                End If
            Else

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                '検索失敗時、共通処理を行う
                Call Me._LMDconH.FailureSelect(frm)
                Return Nothing

            End If
        Else

            '検索成功時
            Return rtnDs

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' 振替商品検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectFuriGoodsData(ByVal frm As LMD100F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMD100DS()
        Dim row As DataRow = rtDs.Tables(LMD100C.TABLE_NM_FURIGOODS_IN).NewRow
        row("NRS_BR_CD") = Me._BrCd

        rtDs.Tables(LMD100C.TABLE_NM_FURIGOODS_IN).Rows.Add(row)
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectFuriGoodsData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs = MyBase.CallWSA("LMD100BLF", "SelectFuriGoodsData", rtDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectFuriGoodsData")

        Return rtDs

    End Function

#End Region '内部メソッド

#Region "外部メソッド"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMD100C.EventShubetsu, ByVal frm As LMD100F _
                             , Optional ByVal e As Integer = 0)

        '処理開始アクション
        Call Me._LMDconH.StartAction(frm)

        'イベント種別による分岐
        Select Case eventShubetsu

            Case LMD100C.EventShubetsu.KENSAKU

                '******************「検索」******************'

                '項目チェック
                If Me._V.IsKensakuSingleCheck() = False Then
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '検索処理を行う
                Call Me.SelectData(frm, LMD100C.SearchShubetsu.NEW_SEARCH, _PrmDs)

                'フォーカスの設定
                Call Me._G.SetFoucus()



            Case LMD100C.EventShubetsu.LOTSENTAKU

                '******************「ロット選択」******************'

                'チェックの付いたSpreadのRowIndexを取得
                Dim list As ArrayList = Me._V.SprSelectCount()

                '選択行共通チェック
                If Me._V.SentakuCheck(list) = False Then
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    Exit Sub

                End If

                '選択行のindexを取得
                Dim index As Integer = Convert.ToInt32(list(0))
                '選択データを取得
                Dim outDr As DataRow = Me.GetSpreadData(frm, index)

                '届先チェック
                If Me._V.DestCdCheck(Me._DestCd, outDr) = False Then
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    Exit Sub
                End If

                '営業所コード、倉庫コード変更チェック
                If Me._V.BrSokoCheck(outDr, Me._BrCd, Me._WhCd) = False Then
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    Exit Sub
                End If

                '選択データが引当注意品の場合、メッセージを表示
                If outDr.Item("HIKIATE_ALERT_YN").ToString() = "01" Then

                    'メッセージを表示し、戻り値により処理を分ける
                    If MyBase.ShowMessage(frm, "W138", New String() {String.Empty}) = MsgBoxResult.Cancel Then
                        Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                        Exit Sub

                    End If

                End If

                'ロット番号が指定されていない場合、メッセージを表示
                If String.IsNullOrEmpty(outDr.Item("LOT_NO").ToString()) = True Then
                    'メッセージを表示し、戻り値により処理を分ける
                    If MyBase.ShowMessage(frm, "W137", New String() {String.Empty}) = MsgBoxResult.Cancel Then
                        Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                        Exit Sub

                    End If

                End If

                'OUT情報を設定
                Call Me.SetOutDataSet(outDr, LMD100C.EventShubetsu.LOTSENTAKU)

                '画面を閉じる
                frm.Close()



            Case LMD100C.EventShubetsu.SENTAKU

                '******************「選択」******************'
                'チェックの付いたSpreadのRowIndexを取得
                Dim list As ArrayList = Me._V.SprSelectCount()

                '選択行共通チェック
                If Me._V.SentakuCheck(list) = False Then
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    Exit Sub

                End If

                '選択行のRowIndexを取得
                Dim index As Integer = Convert.ToInt32(list(0))
                '選択データを取得
                Dim outDr As DataRow = Me.GetSpreadData(frm, index)

                '届先チェック
                If Me._V.DestCdCheck(Me._DestCd, outDr) = False Then
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    Exit Sub
                End If

                '営業所コード、倉庫コード変更チェック
                If Me._V.BrSokoCheck(outDr, Me._BrCd, Me._WhCd) = False Then
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    Exit Sub
                End If

                '選択データが引当注意品の場合、メッセージを表示
                If outDr.Item("HIKIATE_ALERT_YN").ToString() = "01" Then

                    'メッセージを表示し、戻り値により処理を分ける
                    If MyBase.ShowMessage(frm, "W138", New String() {String.Empty}) = MsgBoxResult.Cancel Then
                        Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                        Exit Sub

                    End If

                End If

                'OUT情報を設定
                Call Me.SetOutDataSet(outDr, LMD100C.EventShubetsu.SENTAKU)

                '画面を閉じる
                frm.Close()



            Case LMD100C.EventShubetsu.DOUBLE_CLICK

                '******************「ダブルクリック」******************

                '選択行のRowIndexを取得
                Dim index As Integer = e
                '選択データを取得
                Dim outDr As DataRow = Me.GetSpreadData(frm, index)

                '届先チェック
                If Me._V.DestCdCheck(Me._DestCd, outDr) = False Then
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    Exit Sub
                End If

                '営業所コード、倉庫コード変更チェック
                If Me._V.BrSokoCheck(outDr, Me._BrCd, Me._WhCd) = False Then
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    Exit Sub
                End If

                '選択データが引当注意品の場合、メッセージを表示
                If outDr.Item("HIKIATE_ALERT_YN").ToString() = "01" Then

                    'メッセージを表示し、戻り値により処理を分ける
                    If MyBase.ShowMessage(frm, "W138", New String() {String.Empty}) = MsgBoxResult.Cancel Then
                        Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                        Exit Sub

                    End If

                End If

                'ロット番号が指定されていない場合、メッセージを表示
                If String.IsNullOrEmpty(outDr.Item("LOT_NO").ToString()) = True Then
                    'メッセージを表示し、戻り値により処理を分ける
                    If MyBase.ShowMessage(frm, "W137", New String() {String.Empty}) = MsgBoxResult.Cancel Then
                        Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                        Exit Sub

                    End If

                End If

                'OUT情報を設定
                Call Me.SetOutDataSet(outDr, LMD100C.EventShubetsu.LOTSENTAKU)

                '画面を閉じる
                frm.Close()


        End Select

        '処理終了アクション
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage())

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMD100F) As Boolean

        If Me._Prm.ParamDataSet.Tables(LMControlC.LMD100C_TABLE_NM_OUT) Is Nothing = True _
            OrElse Me._Prm.ParamDataSet.Tables(LMControlC.LMD100C_TABLE_NM_OUT).Rows.Count = 0 Then

            'リターンコードの設定
            Me._Prm.ReturnFlg = False
        Else

            'リターンコードの設定
            Me._Prm.ReturnFlg = True

        End If

    End Function

    ''' <summary>
    ''' ガイダンスメッセージを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String
        Return "G007"
    End Function

#End Region '外部メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMD100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "KensakuEvent")

        '検索処理
        Me.ActionControl(LMD100C.EventShubetsu.KENSAKU, frm)

        Logger.EndLog(Me.GetType.Name, "KensakuEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMD100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "LotSentakuEvent")

        'ロット選択処理
        Me.ActionControl(LMD100C.EventShubetsu.LOTSENTAKU, frm)

        Logger.EndLog(Me.GetType.Name, "LotSentakuEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMD100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SentakuEvent")

        '選択処理
        Me.ActionControl(LMD100C.EventShubetsu.SENTAKU, frm)

        Logger.EndLog(Me.GetType.Name, "SentakuEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMD100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMD100F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMD100F, ByVal e As Integer)

        '検索行がダブルクリックされた場合は処理しない
        If e > 0 Then

            Logger.StartLog(Me.GetType.Name, "RowSelection")

            'ダブルクリック（ロット指定と同じ）
            Me.ActionControl(LMD100C.EventShubetsu.DOUBLE_CLICK, frm, e)

            Logger.EndLog(Me.GetType.Name, "RowSelection")

        End If

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class