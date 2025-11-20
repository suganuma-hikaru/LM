' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ210H : 商品マスタ照会
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMZ210ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMZ210H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMZ210V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ210G
    ''' <summary>
    ''' Sessionクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _S As LMZControlS

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConG As LMZControlG

    ''' <summary>
    ''' ハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConH As LMZControlH

    ''' <summary>
    ''' Validateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConV As LMZControlV

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor
        'セッションクラスのインスタンス生成
        Me._S = New LMZControlS()

        'パラメータオブジェクトを退避
        Me._S.FormPrm = prm

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        '画面間データを取得する
        Me._S.PrmDs = Me._S.FormPrm.ParamDataSet

        'フォームの作成
        Dim frm As LMZ210F = New LMZ210F(Me)

        Dim popL As LMFormPopL = DirectCast(frm, LMFormPopL)

        'Validate共通クラスの設定
        Me._LMZConV = New LMZControlV(popL, Me)

        'Hnadler共通クラスの設定
        Me._LMZConH = New LMZControlH(popL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMZConG = New LMZControlG(frm)

        'Validateクラスの設定
        Me._V = New LMZ210V(Me, frm, Me._LMZConV)

        'Gamenクラスの設定
        Me._G = New LMZ210G(Me, frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn1 _
                                         , LMZControlC.F11Pattern.ptn2)

        'タブインデックスの設定
        Call Me._LMZConG.SetTabIndex(frm.sprDetail)

        'フォーカスの設定
        Call Me._LMZConG.SetFoucus(frm.sprDetail)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ210C.TABLE_NM_IN).Rows(0)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

        '入力チェック
        If Me._V.IsInputChk() = True Then

            '入力チェックでエラーが無い場合

            ''検索処理(キャッシュ)
            'Dim ds As DataSet = New LMZ210DS()
            'ds.Tables(LMZ210C.TABLE_NM_IN).ImportRow(prmdRow)
            'Me._S.OutDs = Me.SelectDestOutListData(frm, ds)

            '検索処理(キャッシュ)
            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")
            '==========================
            'WSAクラス呼出
            '==========================
            Me._S.OutDs = Me._LMZConH.LoadCallWSAAction(Me._S)

            Dim outTbl As DataTable = Me._S.OutDs.Tables(LMZ210C.TABLE_NM_OUT)
            Dim count As Integer = outTbl.Rows.Count

            If count = 1 AndAlso prm.SkipFlg <> True Then
                '初期検索結果＝1件 かつ 画面表示フラグがTrue以外の場合画面を開かない
                prm.ParamDataSet = Me._S.OutDs
                'リターンフラグを立てる
                prm.ReturnFlg = True
                LMFormNavigate.Revoke(Me)
                Exit Sub
            End If

            '荷主コードの取得
            Dim CustCdL As String = prmdRow("CUST_CD_L").ToString()

            '画面ヘッダー部の設定(荷主キャッシュ)
            Dim headerRow As DataRow() = Me._LMZConG.SelectCustListDataRow(CustCdL)

            '荷主キャッシュから取得件数が0件のとき処理終了
            If headerRow.Length < 1 Then

                '2017/09/25 修正 李↓
                Call Me._LMZConV.SetMstErrMessage(lgm.Selector({"荷主マスタ", "Custmer Master", "하주마스터", "中国語"}),
                                                  lgm.Selector({"荷主コード", "Custmer Code", "하주코드", "中国語"}))
                '2017/09/25 修正 李↑

                'キャンセルボタン以外をロックする
                Call Me._G.LockControl(True)


            Else
                'ヘッダー部にデータをセット
                Call Me._G.CustHeaderDataSet(headerRow(0))
                '初期処理ロードメッセージチェック
                If Me._LMZConH.LoadMsgChk(frm, prmdRow, count) = True Then
                    outTbl = Me._LMZConH.SetLoadData(Me._S, frm, outTbl, LMZ210C.TABLE_NM_OUT)
                    '取得データをSPREADに表示
                    Call Me._G.SetSpread(outTbl)
                End If
            End If

            '↑ データ取得の必要があればここにコーディングする。
        End If

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub


#End Region '初期処理

#Region "イベント定義(一覧)"


    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMZ210F)

        Call Me._LMZConH.CloseForm(Me._S, LMZ210C.TABLE_NM_OUT)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMZ210F)

        '処理開始アクション
        Call Me._LMZConH.StartAction(frm)

        '入力チェック
        If Me._V.IsInputChk() = False Then

            '終了処理
            Call Me._LMZConH.EndAction(frm)

            Exit Sub

        End If

        '検索
        Dim rtnDs As DataSet = Me.SelectList(frm)

        Dim outTbl As DataTable = rtnDs.Tables(LMZ210C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMZConH.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

            'セッションクラスのOUTテーブルに設定
            Call Me._LMZConH.SetOutds(Me._S, outTbl, count, LMZ210C.TABLE_NM_OUT)

            '取得データをSPREADに表示
            Call Me._G.SetSpread(outTbl)

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(count)})

        End If

        '終了処理
        Call Me._LMZConH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn1 _
                                        , LMZControlC.F11Pattern.ptn2)

        Call Me._LMZConG.SetFoucus(frm.sprDetail)


    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"
    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMZ210F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ210DS()
        Call Me.SetDatasetDestInData(frm, ds)

        ''キャッシュテーブルからデータ抽出
        'Return Me.SelectDestOutListData(frm, ds)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ210C.TABLE_NM_IN).Rows(0)

        Dim rtnDs As DataSet = Nothing

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me._LMZConH.CallWSAAction(frm, frm.sprDetail, ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMZ210F, ByVal e As Integer)

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, e)

    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMZ210F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ210G.sprDetailDef.DEF.ColNo
        Dim arr As ArrayList = Me._LMZConV.SprSelectCount(frm.sprDetail, defNo)

        '単一選択チェック
        If Me._LMZConV.IsSelectOneChk(arr.Count) = False Then
            Exit Sub
        End If

        '未選択チェック
        If Me._LMZConV.IsSelectChk(arr.Count) = False Then
            Exit Sub
        End If

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)))

    End Sub

    '''' <summary>
    '''' 届先マスタ(キャッシュテーブル)からデータを抽出する
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <param name="ds">LMZ210DS</param>
    '''' <returns>抽出された行列データセット</returns>
    '''' <remarks></remarks>
    'Private Function SelectDestOutListData(ByVal frm As LMZ210F, ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)

    '    'INTableの条件rowの格納
    '    Dim drow As DataRow = inTbl.Rows(0)

    '    Dim whereStr As String = String.Empty
    '    Dim andstr As New System.Text.StringBuilder()
    '    Dim custCdL As String = drow("CUST_CD_L").ToString()
    '    '▼▼キャッシュテーブルへの抽出条件文作成▼▼
    '    '営業所
    '    andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

    '    '関連表示有無フラグのチェックの有無
    '    If drow("RELATION_SHOW_FLG").ToString().Equals(LMConst.FLG.ON) = True Then
    '        Dim dr As DataRow() = Me._LMZConG.SelectKbnListDataRow(LMKbnConst.KBN_T017)
    '        Dim max As Integer = dr.Length - 1
    '        If String.IsNullOrEmpty(custCdL) = False Then
    '            'チェックあり：荷主コード(大)＝入力された値 or 荷主コード(大)＝"ZZZZZ"
    '            andstr.Append(Me._LMZConH.SetWhereData(andstr, custCdL, LMZControlC.ConditionPattern.equal, "(CUST_CD_L"))
    '            Dim whereData As String = " OR CUST_CD_L = '"
    '            For i As Integer = 0 To max
    '                andstr.Append(String.Concat(whereData, dr(i)("KBN_NM1").ToString(), "' "))
    '            Next
    '            andstr.Append(") ")
    '        Else
    '            Dim whereCust As String = "AND CUST_CD_L = '"
    '            For i As Integer = 0 To max
    '                andstr.Append(String.Concat(whereCust, dr(i)("KBN_NM1").ToString(), "' "))
    '            Next
    '        End If
    '    Else
    '        'チェックなし：荷主コード(大)=入力された値
    '        andstr.Append(Me._LMZConH.SetWhereData(andstr, custCdL, LMZControlC.ConditionPattern.equal, "CUST_CD_L"))
    '    End If

    '    '届先名
    '    andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("DEST_NM").ToString(), LMZControlC.ConditionPattern.all, "DEST_NM"))

    '    '住所
    '    andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("AD_1").ToString(), LMZControlC.ConditionPattern.all, "AD_1 + AD_2 + AD_3"))

    '    'START YANAI 要望番号881
    '    '備考
    '    andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("REMARK").ToString(), LMZControlC.ConditionPattern.all, "REMARK"))
    '    'END YANAI 要望番号881

    '    '届先コード
    '    'START YANAI 要望番号690
    '    'andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("DEST_CD").ToString(), LMZControlC.ConditionPattern.pre, "DEST_CD"))
    '    andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("DEST_CD").ToString(), LMZControlC.ConditionPattern.all, "DEST_CD"))
    '    'END YANAI 要望番号690

    '    'EDI届先コード
    '    andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("EDI_DEST_CD").ToString(), LMZControlC.ConditionPattern.all, "EDI_CD"))

    '    '▲▲キャッシュテーブルへの抽出条件文作成▲▲

    '    'キャッシュテーブルからデータ抽出
    '    If LMConst.FLG.ON.Equals(LMUserInfoManager.GetCacheAll) And MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Rows.Count = 0 Then MyBase.LMCacheMasterData(LMConst.CacheTBL.DEST)

    '    Dim sort As String = "DEST_CD"
    '    Return Me._LMZConH.SelectListData(ds, LMZ210C.TABLE_NM_OUT, LMConst.CacheTBL.DEST, andstr.ToString(), sort)

    'End Function

    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMZ210F, ByVal rowIdx As Integer)

        If 0 < rowIdx Then

            'データテーブルから選択行を抽出
            Dim rowI As Integer = Convert.ToInt32(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMZ210G.sprDetailDef.ROW_INDEX.ColNo).Value.ToString())

            Call Me._LMZConH.SetRtnParam(Me._S, New LMZ210DS(), LMZ210C.TABLE_NM_OUT, rowI)

        End If

    End Sub

#Region "DataSet設定"


    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDestInData(ByVal frm As LMZ210F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMZ210C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        drow("CUST_CD_L") = frm.lblCustCdL.TextValue
        drow("RELATION_SHOW_FLG") = frm.chkRelateFlg.GetBinaryValue

        With frm.sprDetail.ActiveSheet

            drow("DEST_NM") = Me._LMZConV.GetCellValue(.Cells(0, LMZ210G.sprDetailDef.DEST_NM.ColNo))
            drow("AD_1") = Me._LMZConV.GetCellValue(.Cells(0, LMZ210G.sprDetailDef.AD_1.ColNo))
            'START YANAI 要望番号881
            drow("REMARK") = Me._LMZConV.GetCellValue(.Cells(0, LMZ210G.sprDetailDef.REMARK.ColNo))
            'END YANAI 要望番号881

            drow("DEST_CD") = Me._LMZConV.GetCellValue(.Cells(0, LMZ210G.sprDetailDef.DEST_CD.ColNo))

#If True Then ' フィルメニッヒ セミEDI対応  20160930 added inoue
            drow("DELI_ATT") = Me._LMZConV.GetCellValue(.Cells(0, LMZ210G.sprDetailDef.DELI_ATT.ColNo))
#End If

        End With

        ds.Tables(LMZ210C.TABLE_NM_IN).Rows.Add(drow)

    End Sub


#End Region 'DataSet設定

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMZ210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "selectdata")

        '検索処理
        Call Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "selectdata")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(OK処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMZ210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "OKbutton")

        '選択処理
        Call Me.RowOkSelect(frm)

        '選択行の取得に成功時自フォームを閉じる
        If Me._S.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "OKbutton")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMZ210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()


    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMZ210F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMZ210F, ByVal e As Integer)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        '選択処理
        Call Me.RowSelection(frm, e)

        '選択行の取得に成功時自フォームを閉じる
        If Me._S.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub


    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class