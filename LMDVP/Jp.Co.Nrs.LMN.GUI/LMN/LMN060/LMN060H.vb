' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN060H   : 拠点別在庫一覧
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN060ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN060H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMN060V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMN060G

    ''' <summary>
    ''' ParameterDS格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' 検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMN060F = New LMN060F(Me)

        'Validateクラスの設定
        Me._V = New LMN060V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMN060G(Me, frm)

        'フォームの初期化
        MyBase.InitControl(frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me._PrmDs)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread(Me._PrmDs, LMConst.FLG.ON)

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 在庫日数算出処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub CalculateEvent(ByVal frm As LMN060F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk() = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'Spread表示行Zero件チェック
        If frm.sprDetail.ActiveSheet.Rows.Count = 1 Then
            MyBase.ShowMessage(frm, "E028", New String() {"検索を未実行または検索結果が0件", "在庫日数算出"})
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'inputDataSet設定
        Dim ds As DataSet = New LMN060DS()
        ds = SetDataSetZaikoNissu(frm, ds)

        '在庫日数算出、更新処理
        ds = MyBase.CallWSA("LMN060BLF", "UpdZaikoNissu", ds)

        '明細部Spreadをクリアー
        frm.sprDetail.CrearSpread()

        '明細部に取得した在庫日数を設定する
        Call Me._G.SetSpread(ds)

        '処理終了メッセージ表示
        MyBase.ShowMessage(frm, "G002", New String() {"在庫日数算出処理", ""})

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub FindEvent(ByVal frm As LMN060F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '検索処理を行う
        Call Me.SelectDetail(frm)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread(Me._PrmDs)

        '在庫日数ラベルを初期化する
        frm.lblZaikoNissuSanshutsuDate.Text = String.Empty

        If Me._PrmDs.Tables(LMN060C.TABLE_NM_OUT) Is Nothing _
        OrElse Me._PrmDs.Tables(LMN060C.TABLE_NM_OUT).Rows.Count = 0 Then
            '検索失敗時共通処理
            Call Me.FailureSelect(frm)
            Exit Sub
        End If

        '明細部のデータを設定する
        Call Me._G.SetSpread(Me._PrmDs)

        '検索成功時共通処理
        Call Me.SuccessSelect(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' Spread行ダブルクリックイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SpreadDoubleClickEvent(ByVal frm As LMN060F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        If e.Row = 0 Then
            Exit Sub
        End If

        '処理開始アクション
        Call Me.StartAction(frm)

        'データセット設定
        Dim ds As DataSet = New LMN060DS
        Dim dr As DataRow = ds.Tables("LMN060IN").NewRow

        'データ取得Spread行番号取得(行数1,4,7,10,・・・の行に各データが設定されているため)
        Dim rowNo As Integer = 0
        '商品コード毎の行数
        Dim rowNum As Integer = 3
        'データ取得行番号算出
        If e.Row Mod rowNum = 1 Then
            rowNo = e.Row
        Else
            rowNo = (((e.Row - 1) \ rowNum) * rowNum) + 1
        End If

        With frm.sprDetail.ActiveSheet
            dr.Item("BR_CD") = .Cells(rowNo, LMN060G.sprDetailDef.BR_CD.ColNo).Value
            dr.Item("SOKO_CD") = .Cells(rowNo, LMN060G.sprDetailDef.SOKO_CD.ColNo).Value
            dr.Item("SCM_CUST_CD") = frm.lblKakushiCustCd.Text
            dr.Item("CUST_GOODS_CD") = .Cells(rowNo, LMN060G.sprDetailDef.ITEM_CD.ColNo).Value
        End With

        '移行フラグの取得
        Dim Filter As String = String.Empty
        Filter = String.Concat("KBN_GROUP_CD = 'L001' AND KBN_NM3 = '", _
                                frm.sprDetail.ActiveSheet.Cells(rowNo, LMN060G.sprDetailDef.BR_CD.ColNo).Value, "'")
        Dim getDr_L001 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Filter)
        dr.Item("IKO_FLG") = getDr_L001(0).Item("KBN_NM4").ToString()

        ds.Tables("LMN060IN").Rows.Add(dr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMN520Preview")

        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMN060BLF", "LMN520Preview", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMN520Preview")

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMN060F)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMN060F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 検索処理を行う
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectDetail(ByVal frm As LMN060F)

        'Spreadダブルクリック時に渡すパラメータ用のCust_Cdを隠し項目に移す
        frm.lblKakushiCustCd.Text = frm.cmbCustCd.SelectedValue.ToString()

        'DataSet設定
        Dim ds As DataSet = New LMN060DS()
        Call SetDataSetInData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectDetail")

        '==========================
        'WSAクラス呼出
        '==========================
        Me._PrmDs = MyBase.CallWSA("LMN060BLF", "SelectDetail", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectDetail")

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMN060F)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '検索件数を変数に保持
        Me._CntSelect = ((frm.sprDetail.ActiveSheet.Rows.Count - 1) / 3).ToString()

        If Me._CntSelect.Equals("0") Then
            'メッセージを表示する
            MyBase.ShowMessage(frm, "G001")
        Else
            'メッセージエリアの設定
            Dim msg As String = String.Concat("明細数", Me._CntSelect, "件")
            MyBase.ShowMessage(frm, "G002", New String() {"明細表示処理", msg})
        End If
 
    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal frm As LMN060F)

        'メッセージを表示する
        MyBase.ShowMessage(frm, "G001")

        '終了処理を行う
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msgC001">メッセージ置換文字列(処理名)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal frm As LMN060F, ByVal msgC001 As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {msgC001}) = MsgBoxResult.Cancel Then
            'メッセージエリアの設定
            Dim msgG002 As String = String.Concat("明細数", Me._CntSelect, "件")
            MyBase.ShowMessage(frm, "G002", New String() {"明細表示処理", msgG002})
            Return False
        End If

        Return True

    End Function

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索条件設定用DS</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMN060F, ByVal ds As DataSet)

        '検索条件、検索結果格納用テーブルの変数定義
        Dim dt As DataTable = ds.Tables(LMN060C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        dr.Item("SCM_CUST_CD") = frm.cmbCustCd.SelectedValue()
        If frm.chkDispSoko.GetBinaryValue() = LMConst.FLG.ON Then
            dr.Item("STOCK_UNDISP_FLG") = "0"
        Else
            dr.Item("STOCK_UNDISP_FLG") = "1"
        End If
        dr.Item("BR_CD") = LMUserInfoManager.GetNrsBrCd()   'ログインユーザの営業所設定

        With frm.sprDetail.ActiveSheet
            dr.Item("CUST_GOODS_CD") = Me._V.GetCellValue(.Cells(0, LMN060G.sprDetailDef.ITEM_CD.ColNo))
            dr.Item("GOODS_NM") = Me._V.GetCellValue(.Cells(0, LMN060G.sprDetailDef.ITEM_NM.ColNo))
        End With

        dt.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(在庫日数算出、更新)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索条件設定用DS</param>
    ''' <remarks></remarks>
    Private Function SetDataSetZaikoNissu(ByVal frm As LMN060F, ByVal ds As DataSet) As DataSet

        '画面内パラメータを設定
        ds = Me._PrmDs.Copy

        '倉庫側荷主コードを取得
        Dim outDt As DataTable = ds.Tables("LMN060OUT")
        Dim outNum As Integer = outDt.Rows.Count
        For i As Integer = 0 To outNum - 1
            '営業所コード取得
            Dim brCd As String = outDt.Rows(i).Item("BR_CD").ToString()
            'SCM荷主コード取得
            Dim scmCustCd As String = frm.cmbCustCd.SelectedValue.ToString()
            outDt.Rows(i).Item("SCM_CUST_CD") = scmCustCd
            '区分マスタより倉庫側荷主コードを取得
            Dim filter As String = String.Concat("KBN_GROUP_CD = 'S033' AND KBN_NM3 = '", scmCustCd, "' AND KBN_NM4 = '", brCd, "' AND SYS_DEL_FLG = '0'")
            Dim sokoDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
            '倉庫側荷主コードを設定
            outDt.Rows(i).Item("LMS_CUST_CD") = sokoDr(0).Item("KBN_NM5").ToString()
        Next

        Return ds

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F5押下時処理呼び出し(在庫日数算出処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMN060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CalculateEvent")

        '在庫日数算出処理
        Call Me.CalculateEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CalculateEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMN060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FindEvent")

        '検索処理
        Call Me.FindEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FindEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMN060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMN060F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓その他のイベント ↓↓↓========================

    ''' <summary>
    ''' Spread行ダブルクリックイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub SpreadDoubleClick(ByRef frm As LMN060F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SpreadDoubleClickEvent")

        'ダブルクリック処理
        Call Me.SpreadDoubleClickEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SpreadDoubleClickEvent")

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================
#End Region

#End Region

End Class
