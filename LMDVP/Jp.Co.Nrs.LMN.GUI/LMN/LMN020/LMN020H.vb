' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN020H   : 出荷データ詳細
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN020ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMN020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMN020G

    ''' <summary>
    ''' ParameterDS格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' 初期検索件数格納用フィールド
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
        Dim frm As LMN020F = New LMN020F(Me)

        'Validateクラスの設定
        Me._V = New LMN020V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMN020G(Me, frm)

        'フォームの初期化
        MyBase.InitControl(frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '初期検索を行う
        Call Me.SelectData(frm)

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
    ''' 削除ボタン押下時処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteEvent(ByVal frm As LMN020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN020C.EventShubetsu.SAKUJHO) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ステータスチェック
        If Me._V.IsStatusChk(LMN020C.EventShubetsu.SAKUJHO) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "削除") = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        'DataSet設定
        Dim ds As DataSet = Me._PrmDs.Copy
        Dim rtnDs As DataSet = Nothing

        '==========================
        'WSAクラス呼出
        '==========================
        'Insertフラグによって呼び出すメソッドを分ける
        Dim insFlg As String = Me._PrmDs.Tables(LMN020C.TABLE_NM_IN).Rows(0).Item("INSERT_FLG").ToString()
        If insFlg.Equals(LMConst.FLG.ON) = True Then
            rtnDs = MyBase.CallWSA("LMN020BLF", "DeleteDataInsFlgOn", ds)
        Else
            'ステータスによって呼び出すメソッドを分ける
            Dim status As String = frm.cmbStatus.SelectedValue.ToString()

            Select Case status
                Case LMN020C.STATUS_MISETTEI _
                   , LMN020C.STATUS_SETTEIZUMI        'ステータス「00:未設定」「01:設定済み」時
                    rtnDs = MyBase.CallWSA("LMN020BLF", "DeleteDataInsFlgOff", ds)

                Case LMN020C.STATUS_SOKOSIJIZUMI      'ステータス「02:倉庫指示済み」時
                    rtnDs = MyBase.CallWSA("LMN020BLF", "DeleteDataInsFlgOffSoko", ds)

            End Select
        End If

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist = True Then
            '返却メッセージを設定
            MyBase.ShowMessage(frm)

            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '出荷データ照会画面(LMN010)に遷移する
        LMFormNavigate.NextFormNavigate(Me, "LMN010", New LMFormData())

        '画面を閉じる
        frm.Close()

    End Sub

    ''' <summary>
    ''' 初期化ボタン押下時処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub InitializeEvent(ByVal frm As LMN020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN020C.EventShubetsu.SHOKIKA) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '初期状態チェック
        Dim insertFlg As String = Me._PrmDs.Tables(LMN020C.TABLE_NM_IN).Rows(0).Item("INSERT_FLG").ToString()
        If Me._V.IsInitStateChk(insertFlg) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ステータスチェック
        If Me._V.IsStatusChk(LMN020C.EventShubetsu.SHOKIKA) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "初期化") = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "InitializeEvent")

        'DataSet設定
        Dim ds As DataSet = Me._PrmDs.Copy
        Dim rtnDs As DataSet = Nothing
        '==========================
        'WSAクラス呼出
        '==========================
        'ステータスによって呼び出すメソッドを分ける
        Dim status As String = frm.cmbStatus.SelectedValue.ToString()
        Select Case status
            Case LMN020C.STATUS_MISETTEI _
               , LMN020C.STATUS_SETTEIZUMI        'ステータス「00:未設定」「01:設定済み」時
                rtnDs = MyBase.CallWSA("LMN020BLF", "InitData", ds)

            Case LMN020C.STATUS_SOKOSIJIZUMI      'ステータス「02:倉庫指示済み」時
                rtnDs = MyBase.CallWSA("LMN020BLF", "InitDataSokoChk", ds)

        End Select

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist = True Then
            '返却メッセージを設定
            MyBase.ShowMessage(frm)

            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "InitializeEvent")

        '出荷データ照会画面(LMN010)に遷移する
        LMFormNavigate.NextFormNavigate(Me, "LMN010", New LMFormData())

        '画面を閉じる
        frm.Close()

    End Sub

    ''' <summary>
    ''' 在庫照会ボタン押下時処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub InquiryStockEvent(ByVal frm As LMN020F)

        'パラメータ生成
        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMN060DS()
        Dim prmDt As DataTable = prmDs.Tables("LMN060IN")
        Dim prmDr As DataRow = prmDt.NewRow

        Dim setDr As DataRow = Me._PrmDs.Tables(LMN020C.TABLE_NM_IN).Rows(0)
        prmDr.Item("SCM_CUST_CD") = setDr.Item("SCM_CUST_CD")

        prmDt.Rows.Add(prmDr)
        prm.ParamDataSet = prmDs

        '拠点別在庫一覧(LMN060)を開く
        LMFormNavigate.NextFormNavigate(Me, "LMN060", prm)

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMN020F)

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
    Private Sub EndAction(ByVal frm As LMN020F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMN020F)

        'DataSet設定
        Call SetDataSetInData()
        Dim ds As DataSet = Me._PrmDs.Copy

        'SPREAD(表示行)初期化
        frm.sprGoods.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMN020BLF", "SelectListData", ds)
        Dim errorFlg As Boolean = False

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            '検索失敗時共通処理を行う
            Call Me.FailureSelect(frm)
            errorFlg = True
        Else

            Dim rtnDr As DataRow = rtnDs.Tables(LMN020C.TABLE_NM_OUT_HDR).Rows(0)
            '排他処理用に取得データを格納
            Dim drParam As DataRow = Me._PrmDs.Tables(LMN020C.TABLE_NM_IN).Rows(0)
            drParam.Item("SCM_CUST_CD") = rtnDr.Item("SCM_CUST_CD")
            drParam.Item("SOKO_CD") = rtnDr.Item("SOKO_CD")
            drParam.Item("WH_CD") = rtnDr.Item("WH_CD")
            drParam.Item("HED_BP_SYS_UPD_DATE") = rtnDr.Item("HED_BP_SYS_UPD_DATE")
            drParam.Item("HED_BP_SYS_UPD_TIME") = rtnDr.Item("HED_BP_SYS_UPD_TIME")
            drParam.Item("L_SYS_UPD_DATE") = rtnDr.Item("L_SYS_UPD_DATE")
            drParam.Item("L_SYS_UPD_TIME") = rtnDr.Item("L_SYS_UPD_TIME")

            '検索成功時共通処理を行う
            errorFlg = Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm(errorFlg)

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Function SuccessSelect(ByVal frm As LMN020F, ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = False

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをHEADER部に表示
        Call Me._G.SetHeader(ds.Tables(LMN020C.TABLE_NM_OUT_HDR))

        Dim dtlDt As DataTable = ds.Tables(LMN020C.TABLE_NM_OUT_DTL)
        If dtlDt.Rows.Count > 0 Then
            '取得データをSPREADに表示
            Call Me._G.SetSpread(dtlDt)

            '検索件数を変数に保持
            Me._CntSelect = dtlDt.Rows.Count.ToString()

            'メッセージエリアの設定
            Dim msg As String = String.Concat("明細数", Me._CntSelect, "件")
            MyBase.ShowMessage(frm, "G002", New String() {"明細表示処理", msg})

        Else

            rtnResult = True

            '画面項目をロックする
            Call Me._G.SetErrorLock()

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "E180")

        End If

        Return rtnResult

    End Function

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal frm As LMN020F)

        '画面解除
        MyBase.UnLockedControls(frm)

        '画面項目をロックする
        Call Me._G.SetErrorLock()

        'メッセージエリアの設定
        MyBase.ShowMessage(frm)

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msgC001">メッセージ置換文字列(処理名)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal frm As LMN020F, ByVal msgC001 As String) As Boolean

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
    ''' データセット設定(在庫テーブル参照区分設定)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim dr As DataRow = Me._PrmDs.Tables(LMN020C.TABLE_NM_IN).Rows(0)

        '区分マスタ検索処理
        Dim filter As String = String.Empty
        filter = String.Concat("KBN_GROUP_CD = 'L001' AND KBN_NM3 = '", dr.Item("BR_CD").ToString(), "'")
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)

        dr.Item("IKO_FLG") = getDr(0).Item("KBN_NM4").ToString()


    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F4押下時処理呼び出し(削除処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMN020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteEvent")

        '削除処理
        Call Me.DeleteEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteEvent")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し(初期化処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMN020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "InitializeEvent")

        '初期化処理
        Call Me.InitializeEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "InitializeEvent")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し(在庫照会処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMN020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "InquiryStockEvent")

        '在庫照会処理
        Call Me.InquiryStockEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "InquiryStockEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMN020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMN020F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

#End Region

#End Region

End Class