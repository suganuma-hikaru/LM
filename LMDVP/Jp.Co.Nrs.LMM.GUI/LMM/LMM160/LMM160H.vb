' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM160H : 届先商品マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI

''' <summary>
''' LMM160ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM160H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM160V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM160G

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConV As LMMControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConH As LMMControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

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

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMM160F = New LMM160F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sFrom, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Gamenクラスの設定
        Me._G = New LMM160G(Me, frm, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM160V(Me, frm, Me._LMMConV)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'キーイベントをフォームで受け取る                                                           
        frm.KeyPreview = True

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Me._G.SetInitValue(frm)

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        '画面の入力項目ロック
        Call Me._G.LockControl(True)

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM160C.EventShubetsu.MAIN)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent(ByVal frm As LMM160F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM160C.EventShubetsu.SHINKI) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        Call Me._G.ClearControl()

        '営業所はログインユーザーの営業所
        Me._G.SetcmbNrsBrCd()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM160C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM160F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM160C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._V.IsExistDataChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM160C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim ds As DataSet = New LMM160DS()
        Call SetDatasetUserItemData(frm, ds)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM160BLF", "HaitaData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)

            '終了処理
            Call Me._LMMConH.EndAction(frm)

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

            '終了処理
            Call Me._LMMConH.EndAction(frm)

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)


            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()


            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM160C.EventShubetsu.HENSHU)


    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM160F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM160C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._V.IsExistDataChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM160C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '編集部の項目複写
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM160C.EventShubetsu.HUKUSHA)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G003")


    End Sub

#End Region 'イベント定義(一覧)

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM160F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM160C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._V.IsExistDataChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM160C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '削除フラグチェック
        If frm.lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) Then
            Select Case MyBase.ShowMessage(frm, "C001", New String() {"削除"})
                Case MsgBoxResult.Cancel '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        Else
            Select Case MyBase.ShowMessage(frm, "C001", New String() {"復活"})
                Case MsgBoxResult.Cancel  '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM160DS()
        Call Me.SetDatasetDelData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM160BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.DESTGOODS)

        'メッセージ用請求先コード格納
        Dim custCd As String = frm.txtCustCdL.TextValue
        Dim destCd As String = frm.txtDestCd.TextValue
        'メッセージ
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName _
                                              , String.Concat("[", LMM160C.CUSTCDLMSG, " = ", custCd, "]" _
                                                              , "[", LMM160C.DESTCDMSG, " = ", destCd, "]")})


        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM160F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM160C.EventShubetsu.KENSAKU) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '編集部クリアフラグ
        Dim clearFlg As Integer = 0

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                MyBase.ShowMessage(frm, "G003")
                Exit Sub
            Else      'OK押下
                clearFlg = 1
            End If
        End If

        '検索処理を行う
        Call Me.SelectData(frm, clearFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM160C.EventShubetsu.KENSAKU)

    End Sub
    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM160F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        Dim rowNo As Integer = e.NewRow
        If rowNo < 1 Then
            Exit Sub
        End If

        '同じ行の場合、スルー
        If e.Row = rowNo Then
            Exit Sub
        End If

        Call Me.RowSelection(frm, rowNo)

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal frm As LMM160F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        ''メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        Call Me.RowSelection(frm, e.Row)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMM160F, ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM160C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM160G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(LMM160C.EventShubetsu.SANSHO, frm.lblSituation.RecordStatus)

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")

    End Sub
    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMM160F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM160C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM160C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM160C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveCostItemData(ByVal frm As LMM160F, ByVal eventShubetsu As LMM160C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM160C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        '項目チェック()
        If Me._V.IsSaveInputChk() = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM160DS()
        Call Me.SetDatasetUserItemData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet = Nothing

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = Me.CallWSA("LMM160BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = Me.CallWSA("LMM160BLF", "UpdateData", ds)
        End Select

        Dim CustCdL As String = frm.txtCustCdL.TextValue
        Dim DestCd As String = frm.txtDestCd.TextValue

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            If MyBase.GetMessageID().Equals("E079") = True Then

                MyBase.SetMessage("E079", New String() {String.Concat("荷主マスタ", "", CustCdL, "")})
                Call Me._V.SetErrorControl(frm.txtCustCdL)

            End If
            MyBase.ShowMessage(frm)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.DESTGOODS)

        'メッセージ表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat("[", LMM160C.CUSTCDLMSG, " = ", CustCdL, "]" _
                                                                      , "[", LMM160C.DESTCDMSG, " = ", DestCd, "]")})

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM160C.EventShubetsu.MAIN)


        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">LMM160F</param>
    ''' <remarks></remarks>
    Friend Function CloseFormEvent(ByVal frm As LMM160F, ByVal e As FormClosingEventArgs) As Boolean

        'ディスプレイモードが編集の場合
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            'メッセージ表示
            Select Case MyBase.ShowMessage(frm, "W002")
                Case MsgBoxResult.Yes  '「はい」押下時
                    If Me.SaveCostItemData(frm, LMM160C.EventShubetsu.TOJIRU) = True Then
                        Return True
                    Else '保存失敗時
                        Return False
                    End If
                Case MsgBoxResult.No   '「いいえ」押下時
                    Return True
                Case Else                   '「キャンセル」押下時
                    e.Cancel = True
                    Return False
            End Select
        End If

        Return True

    End Function

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM160C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM160C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM160C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMMConH.NextFocusedControl(frm, eventFlg)

    End Sub

#End Region 'イベント振分け

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM160F, ByVal clearflg As Integer)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))

        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Me._FindDs = New LMM160DS()
        Call Me.SetDataSetInData(frm)

        ''SPREAD(表示行)初期化
        'frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM160BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM160BLF", "SelectListData", _FindDs _
        '                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
        '                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
        '                                  , , Convert.ToInt32(Convert.ToDouble( _
        '                                     MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                     .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'ステータスの設定
        If clearflg = 1 Then
            Call Me._G.SetControlsStatus()
        End If

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()


    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMM160F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM160C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()


        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()
        '0件でないとき
        If Me._CntSelect.Equals(LMConst.FLG.OFF) = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        End If

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ステータスの設定
        Call Me._G.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' 背景色の設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetBackColor(ByVal frm As LMM160F)

        Call Me._LMMConG.SetBackColor(frm.lblGoodsNrs)

    End Sub


#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM160F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMM160F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

#End Region

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMM160F, ByVal objNm As String, ByVal eventshubetsu As LMM160C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            '背景色の設定
            Call Me.SetBackColor(frm)

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Call Me.SetReturnCustPop(frm, eventshubetsu)

                Case .txtDestCd.Name

                    Call Me.SetReturnDestPop(frm, objNm, eventshubetsu)

                Case .txtGoodsCd.Name

                    Call Me.SetReturnGoodsPop(frm, eventshubetsu)

                Case .txtWorkSeiqCd.Name

                    Call Me.SetReturnSeiqtoPop(frm, objNm, eventshubetsu)

                Case .txtWork1Kb.Name, .txtWork2Kb.Name

                    Call Me.SetReturnSagyoPop(frm, objNm, eventshubetsu)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMM160F, ByVal eventshubetsu As LMM160C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, eventshubetsu)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMM160F, ByVal eventshubetsu As LMM160C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            Dim custCdL As String = frm.txtCustCdL.TextValue
            Dim custCdM As String = frm.txtCustCdM.TextValue

            Dim csFlg As String = LMConst.FLG.OFF

            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM160C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = custCdL
                .Item("CUST_CD_M") = custCdM
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csFlg
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMM160F, ByVal objNm As String, ByVal eventshubetsu As LMM160C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowDestPopup(frm, ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
            ctl.TextValue = dr.Item("DEST_CD").ToString()
            frm.lblDestNm.TextValue = dr.Item("DEST_NM").ToString()
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMM160F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM160C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM160C.EventShubetsu.ENTER Then
                .Item("DEST_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function


    ''' <summary>
    ''' 商品Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMM160F, ByVal eventshubetsu As LMM160C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, eventshubetsu)

        '解除しないと制御できない
        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
            frm.txtGoodsCd.TextValue = dr.Item("GOODS_CD_CUST").ToString()
            frm.lblGoodsNm.TextValue = dr.Item("GOODS_NM_1").ToString()
            frm.lblGoodsNrs.TextValue = dr.Item("GOODS_CD_NRS").ToString()
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMM160F, ByVal eventshubetsu As LMM160C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM160C.EventShubetsu.ENTER Then
                .Item("GOODS_CD_CUST") = frm.txtGoodsCd.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 請求先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSeiqtoPop(ByVal frm As LMM160F, ByVal objNm As String, ByVal eventshubetsu As LMM160C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowSeiqtoPopup(frm, ctl, eventshubetsu)
        Dim item As String = String.Empty

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)
            ctl.TextValue = dr.Item("SEIQTO_CD").ToString()
            item = dr.Item("SEIQTO_NM").ToString()
            item = String.Concat(item, " ", dr.Item("SEIQTO_BUSYO_NM").ToString())
            frm.lblWorkSeiqNm.TextValue = item
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowSeiqtoPopup(ByVal frm As LMM160F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM160C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM160C.EventShubetsu.ENTER Then
                .Item("SEIQTO_CD") = frm.txtWorkSeiqCd.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 作業項目Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoPop(ByVal frm As LMM160F, ByVal objNm As String, ByVal eventshubetsu As LMM160C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowSagyoPopup(frm, ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)
            ctl.TextValue = dr.Item("SAGYO_CD").ToString()

            If ctl.Name.Equals(frm.txtWork1Kb.Name) = True Then
                frm.lblWork1Nm.TextValue = dr.Item("SAGYO_NM").ToString()
            ElseIf ctl.Name.Equals(frm.txtWork2Kb.Name) = True Then
                frm.lblWork2Nm.TextValue = dr.Item("SAGYO_NM").ToString()
            End If

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 作業項目マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowSagyoPopup(ByVal frm As LMM160F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM160C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ200DS()
        Dim dt As DataTable = ds.Tables(LMZ200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM160C.EventShubetsu.ENTER Then
                .Item("SAGYO_CD") = ctl.TextValue
                .Item("SAGYO_CNT") = LMM160C.SAGYO_CNT
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ200", "", Me._PopupSkipFlg)

    End Function

#End Region

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM160F)

        Dim dr As DataRow = Me._FindDs.Tables(LMM160C.TABLE_NM_IN).NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("CUST_CD_L") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.CUST_CD_L.ColNo))
            dr("CUST_NM_L") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.CUST_NM_L.ColNo))
            dr("CUST_CD_M") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.CUST_CD_M.ColNo))
            dr("CUST_NM_M") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.CUST_NM_M.ColNo))
            dr("CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.CD.ColNo))
            dr("DEST_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.DEST_NM.ColNo))
            dr("GOODS_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.GOODS_CD.ColNo))
            dr("GOODS_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.GOODS_NM.ColNo))
            dr("SAGYO_KB_1") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.SAGYO_KB_1.ColNo))
            dr("SAGYO_KB_2") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.SAGYO_KB_2.ColNo))
            dr("SAGYO_SEIQTO_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.SAGYO_SEIQTO_CD.ColNo))
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM160G.sprDetailDef.NRS_BR_CD.ColNo))

            Me._FindDs.Tables(LMM160C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUserItemData(ByVal frm As LMM160F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM160C.TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue.Trim
            dr("CUST_CD_M") = .txtCustCdM.TextValue.Trim
            dr("CUST_NM_L") = .lblCustNmL.TextValue.Trim
            dr("CUST_NM_M") = .lblCustNmM.TextValue.Trim
            dr("CD") = .txtDestCd.TextValue.Trim
            dr("DEST_NM") = .lblDestNm.TextValue.Trim
            dr("GOODS_CD_NRS") = .lblGoodsNrs.TextValue.Trim
            dr("GOODS_NM") = .txtDelverGoodsNm.TextValue.Trim
            dr("SAGYO_SEIQTO_CD") = .txtWorkSeiqCd.TextValue.Trim
            dr("SAGYO_KB_1") = .txtWork1Kb.TextValue.Trim
            dr("SAGYO_KB_2") = .txtWork2Kb.TextValue.Trim
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
        End With

        ds.Tables(LMM160C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM160F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM160C.TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue.Trim
            dr("CUST_CD_M") = .txtCustCdM.TextValue.Trim
            dr("CD") = .txtDestCd.TextValue.Trim
            dr("GOODS_CD_NRS") = .lblGoodsNrs.TextValue.Trim
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            Dim delflg As String = String.Empty
            If .lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If

            dr("SYS_DEL_FLG") = delflg

        End With

        ds.Tables(LMM160C.TABLE_NM_IN).Rows.Add(dr)


    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Me.NewDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CopyDataEvent")

        Me.CopyDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CopyDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "Import JDE")

        Logger.EndLog(MyBase.GetType.Name, "Import JDE")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)


        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")


    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")
    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveCostItemData")

        Me.SaveCostItemData(frm, LMM160C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveCostItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        '終了処理  
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub


    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM160F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Me.CloseFormEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")


    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMM160F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        'DBより該当データの取得処理
        Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM160F_KeyDown(ByVal frm As LMM160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM160F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM160F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM160F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================


#End Region 'Method

End Class