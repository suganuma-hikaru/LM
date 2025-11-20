' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ040 : 単価マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMZ040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMZ040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMZ040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ040G

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

    ''' <summary>
    ''' パラメータのロウを保持する
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmRow As DataRow

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

        'セッションクラスのインスタンス生成
        Me._S = New LMZControlS()

        'パラメータオブジェクトを退避
        Me._S.FormPrm = prm

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        '画面間データを取得する
        Me._S.PrmDs = Me._S.FormPrm.ParamDataSet

        'フォームの作成
        Dim frm As LMZ040F = New LMZ040F(Me)

        Dim popL As LMFormPopL = DirectCast(frm, LMFormPopL)

        'Validate共通クラスの設定
        Me._LMZConV = New LMZControlV(popL, Me)

        'Hnadler共通クラスの設定
        Me._LMZConH = New LMZControlH(popL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMZConG = New LMZControlG(frm)

        'Validateクラスの設定
        Me._V = New LMZ040V(Me, frm, Me._LMZConV)

        'Gamenクラスの設定
        Me._G = New LMZ040G(Me, frm)

        'フォームの初期化
        Call Me.InitControl(frm)

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
        Call Me._LMZConG.SetTabIndex(frm.sprTanka)

        'フォーカスの設定
        Call Me._LMZConG.SetFoucus(frm.sprTanka)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._PrmRow = Me._S.PrmDs.Tables(LMZ040C.TABLE_NM_IN).Rows(0)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread(Me._PrmRow)

        '↓ データ取得の必要があればここにコーディングする。

        '検索処理(キャッシュ)
        Dim ds As DataSet = New LMZ040DS()
        ds.Tables(LMZ040C.TABLE_NM_IN).ImportRow(Me._PrmRow)
        Me._S.OutDs = Me.SelectTankaOutListData(frm, ds)

        Dim outTbl As DataTable = Me._S.OutDs.Tables(LMZ040C.TABLE_NM_OUT)
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
        Dim CustCdL As String = Me._PrmRow("CUST_CD_L").ToString()
        Dim CustCdM As String = Me._PrmRow("CUST_CD_M").ToString()

        '画面ヘッダー部の設定(荷主キャッシュ)
        Dim headerRow As DataRow() = Me._LMZConG.SelectCustListDataRow(CustCdL, CustCdM)

        '荷主キャッシュから取得件数が0件のとき処理終了
        If headerRow.Length < 1 Then
            '2016.01.06 UMANO 英語化対応START
            'Call Me._LMZConV.SetMstErrMessage("荷主マスタ", "荷主コード")
            Call Me._LMZConV.SetErrMessage("E773", New String() {})
            '2016.01.06 UMANO 英語化対応END
            'キャンセルボタン以外をロックする
            Call Me._G.LockControl(True)
            'Call Me._G.FunctionKeyLock()

        Else
            'ヘッダー部にデータをセット
            Call Me._G.CustHeaderDataSet(headerRow(0))
            '初期処理ロードメッセージチェック
            If Me._LMZConH.LoadMsgChk(frm, Me._PrmRow, count) = True Then
                outTbl = Me._LMZConH.SetLoadData(Me._S, frm, outTbl, LMZ040C.TABLE_NM_OUT)
                '取得データをSPREADに表示
                Call Me._G.SetSpread(outTbl)
            End If
        End If

        '↑ データ取得の必要があればここにコーディングする。

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
    Private Sub CloseForm(ByVal frm As LMZ040F)

        Call Me._LMZConH.CloseForm(Me._S, LMZ040C.TABLE_NM_OUT)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMZ040F)

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

        Dim outTbl As DataTable = rtnDs.Tables(LMZ040C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMZConH.CountRows(frm, frm.sprTanka, outTbl) = True AndAlso 0 < count Then

            'セッションクラスのOUTテーブルに設定
            Call Me._LMZConH.SetOutds(Me._S, outTbl, count, LMZ040C.TABLE_NM_OUT)

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

        Call Me._LMZConG.SetFoucus(frm.sprTanka)


    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"


    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMZ040F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ040DS()
        Call Me.SetDatasetTankaInData(frm, ds)

        'キャッシュテーブルからデータ抽出
        Return Me.SelectTankaOutListData(frm, ds)

    End Function

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMZ040F, ByVal e As Integer)

        Dim arr As ArrayList = New ArrayList()
        arr.Add(e)

        '選択時チェックを行う
        If Me._V.IsSelectCheck(arr _
                          , Me._PrmRow.Item("FUTURE_STR_DATE_FLG").ToString() _
                          , MyBase.GetSystemDateTime(0)) = False Then
            Exit Sub
        End If

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, e)

    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMZ040F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ040G.sprDetailDef.DEF.ColNo
        Dim arr As ArrayList = Me._LMZConV.SprSelectCount(frm.sprTanka, defNo)

        '選択時チェックを行う
        If Me._V.IsOKCheck(arr _
                          , Me._PrmRow.Item("FUTURE_STR_DATE_FLG").ToString() _
                          , MyBase.GetSystemDateTime(0)) = False Then
            Exit Sub
        End If

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)))

    End Sub


    ''' <summary>
    ''' 単価マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ040DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectTankaOutListData(ByVal frm As LMZ040F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ040C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim strSqlCust As New System.Text.StringBuilder()
        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        '荷主コード(大)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_CD_L").ToString(), LMZControlC.ConditionPattern.equal, "CUST_CD_L"))

        '荷主コード(中)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_CD_M").ToString(), LMZControlC.ConditionPattern.equal, "CUST_CD_M"))

        '単価マスタコード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UP_GP_CD_1").ToString(), LMZControlC.ConditionPattern.pre, "UP_GP_CD_1"))

        '期割区分
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("KIWARI_KB").ToString(), LMZControlC.ConditionPattern.equal, "KIWARI_KB"))

        '摘要
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("REMARK").ToString(), LMZControlC.ConditionPattern.all, "REMARK"))

        '保管（常温）
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("STORAGE_KB1").ToString(), LMZControlC.ConditionPattern.equal, "STORAGE_KB1"))

        '保管（定温）
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("STORAGE_KB2").ToString(), LMZControlC.ConditionPattern.equal, "STORAGE_KB2"))

        '入庫料建
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("HANDLING_IN_KB").ToString(), LMZControlC.ConditionPattern.equal, "HANDLING_IN_KB"))

        '出庫料建
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("HANDLING_OUT_KB").ToString(), LMZControlC.ConditionPattern.equal, "HANDLING_OUT_KB"))

        'レコード№
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("REC_NO").ToString(), LMZControlC.ConditionPattern.pre, "REC_NO"))

#If True Then   'ADD 2019/05/09 依頼番号 : 004862   【LMS】単価マスタ_大阪しばらく不使用の単価マスタを分かりやすくする
        '使用可能のみ対象にする
        If andstr.Length <> 0 Then
            andstr.Append("AND")
        End If

        andstr.Append(" AVAL_YN  in ('01','') ")

#End If
        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "UP_GP_CD_1,STR_DATE,STORAGE_KB1_NM,STORAGE_KB2_NM"

        Return Me._LMZConH.SelectListData(ds, LMZ040C.TABLE_NM_OUT, LMConst.CacheTBL.TANKA_GRP, andstr.ToString(), sort)

    End Function

    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMZ040F, ByVal rowIdx As Integer)

        If 0 < rowIdx Then

            'データテーブルから選択行を抽出
            Dim rowI As Integer = Convert.ToInt32(frm.sprTanka.ActiveSheet.Cells(rowIdx, LMZ040G.sprDetailDef.ROW_INDEX.ColNo).Value.ToString())

            Call Me._LMZConH.SetRtnParam(Me._S, New LMZ040DS(), LMZ040C.TABLE_NM_OUT, rowI)

        End If

    End Sub

#Region "DataSet設定"


    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetTankaInData(ByVal frm As LMZ040F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMZ040C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        drow("CUST_CD_L") = frm.lblCustCdL.TextValue
        drow("CUST_CD_M") = frm.lblCustCdM.TextValue

        With frm.sprTanka.ActiveSheet

            drow("UP_GP_CD_1") = Me._LMZConV.GetCellValue(.Cells(0, LMZ040G.sprDetailDef.UP_GP_CD_1.ColNo))
            drow("KIWARI_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ040G.sprDetailDef.KIWARI_KB_NM.ColNo))
            drow("REMARK") = Me._LMZConV.GetCellValue(.Cells(0, LMZ040G.sprDetailDef.REMARK.ColNo))
            drow("STORAGE_KB1") = Me._LMZConV.GetCellValue(.Cells(0, LMZ040G.sprDetailDef.STORAGE_KB1_NM.ColNo))
            drow("STORAGE_KB2") = Me._LMZConV.GetCellValue(.Cells(0, LMZ040G.sprDetailDef.STORAGE_KB2_NM.ColNo))
            drow("HANDLING_IN_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ040G.sprDetailDef.HANDLING_IN_KB_NM.ColNo))
            drow("HANDLING_OUT_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ040G.sprDetailDef.HANDLING_OUT_KB_NM.ColNo))
            drow("REC_NO") = Me._LMZConV.GetCellValue(.Cells(0, LMZ040G.sprDetailDef.REC_NO.ColNo))

        End With

        ds.Tables(LMZ040C.TABLE_NM_IN).Rows.Add(drow)

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
    Friend Sub FunctionKey9Press(ByRef frm As LMZ040F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMZ040F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMZ040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()


    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMZ040F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMZ040F, ByVal e As Integer)

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