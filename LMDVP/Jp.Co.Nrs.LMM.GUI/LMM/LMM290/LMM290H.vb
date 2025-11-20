' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM290H : 振替対象商品マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI

''' <summary>
''' LMM290ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM290H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM290V
    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM290G

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
    ''' ParameterDS格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

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
        Dim frm As LMM290F = New LMM290F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sForm, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM290V(Me, frm, Me._LMMConV)

        'Gamenクラスの設定
        Me._G = New LMM290G(Me, frm, Me._LMMConG)

        'フォームの初期化
        MyBase.InitControl(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '2011/08/11 福田 共通動作(右セル移動不可) スタート
        'Enter押下イベント設定
        'Call Me._LMMConH.SetEnterEvent(frm)
        '2011/08/11 福田 共通動作(右セル移動不可) エンド

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目全ロック
        Call Me._LMMConG.SetLockControl(frm, True)

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM290C.EventShubetsu.MAIN)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"


    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent(ByVal frm As LMM290F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM290C.EventShubetsu.SHINKI) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面全ロックの解除
        MyBase.UnLockedControls(frm)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        Call Me._G.ClearControl(frm)

        'コンボボックス初期設定
        Call Me._G.SetcmbBox()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM290C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM290F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM290C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtCustCdL.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM290C.EventShubetsu.HUKUSHA) = False Then
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
        Call Me._G.SetFoucus(LMM290C.EventShubetsu.HUKUSHA)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G003")


    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM290F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM290C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtCdCust.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM290C.EventShubetsu.SAKUJO) = False Then
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
        Dim ds As DataSet = New LMM290DS()
        Call Me.SetDatasetDelData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM290BLF", "DeleteData", ds)

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
        MyBase.LMCacheMasterData(LMConst.CacheTBL.FURI_GOODS)

        'メッセージ用商品コード格納
        Dim cdCust As String = frm.txtCdCust.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName _
                                              , String.Concat("[", LMM290C.GOODSMSG, " = ", cdCust, "]")})

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
    Private Sub SelectListEvent(ByVal frm As LMM290F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM290C.EventShubetsu.KENSAKU) = False Then
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
        Call Me._G.SetFoucus(LMM290C.EventShubetsu.KENSAKU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM290F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    Private Sub SprCellSelect(ByVal frm As LMM290F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

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
    Private Sub RowSelection(ByVal frm As LMM290F, ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM290C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM290G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(LMM290C.EventShubetsu.SANSHO, frm.lblSituation.RecordStatus)

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
    Private Sub MasterShowEvent(ByVal frm As LMM290F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM290C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM290C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM290C.EventShubetsu.MASTEROPEN)

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
    Private Function SaveItemData(ByVal frm As LMM290F, ByVal eventShubetsu As LMM290C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM290C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        ''項目チェック
        If Me._V.IsSaveInputChk() = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM290DS()
        Call Me.SetDatasetUserItemData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        '新規登録処理
        rtnDs = MyBase.CallWSA("LMM290BLF", "InsertData", ds)

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "InsertData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.FURI_GOODS)

        '処理結果メッセージ表示
        Dim cdCust As String = frm.txtCdCust.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat("[", LMM290C.GOODSMSG, " = ", cdCust, "]")})


        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM290C.EventShubetsu.MAIN)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal frm As LMM290F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveItemData(frm, LMM290C.EventShubetsu.TOJIRU) = False Then

                    e.Cancel = True

                End If


            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub


    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM290C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM290C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM290C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMMConH.NextFocusedControl(frm, eventFlg)

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM290F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM290DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM290BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM290BLF", "SelectListData", _FindDs _
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
    Private Sub SuccessSelect(ByVal frm As LMM290F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM290C.TABLE_NM_OUT)

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
    Private Sub SetBackColor(ByVal frm As LMM290F)

        With frm

            Call Me._LMMConG.SetBackColor(.lblCdNrs)
            Call Me._LMMConG.SetBackColor(.lblCdNrsTo)
            Call Me._LMMConG.SetBackColor(.txtCustCdL)
            Call Me._LMMConG.SetBackColor(.lblIrime)
            Call Me._LMMConG.SetBackColor(.lblIrimeTo)

        End With

    End Sub

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM290F)

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
    Private Sub SetGMessage(ByVal frm As LMM290F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM290F)

        Dim dt As DataTable = Me._FindDs.Tables(LMM290C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("CUST_CD_L") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.CUST_CD_L.ColNo))
            dr("CUST_NM_L") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.CUST_NM_L.ColNo))
            dr("GOODS_CD_CUST") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.GOODS_CD_CUST.ColNo))
            dr("GOODS_NM_1") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.GOODS_NM_1.ColNo))
            dr("CUST_CD_L_TO") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.CUST_CD_L_TO.ColNo))
            dr("CUST_NM_L_TO") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.CUST_NM_L_TO.ColNo))
            dr("GOODS_CD_CUST_TO") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.GOODS_CD_CUST_TO.ColNo))
            dr("GOODS_NM_1_TO") = Me._LMMConV.GetCellValue(.Cells(0, LMM290G.sprDetailDef.GOODS_NM_1_TO.ColNo))
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            dr("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            dt.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUserItemData(ByVal frm As LMM290F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM290C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("CD_NRS") = .lblCdNrs.TextValue
            dr("CD_NRS_TO") = .lblCdNrsTo.TextValue

            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

        End With

        dt.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM290F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM290C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()


        With frm

            dr("CD_NRS") = .lblCdNrs.TextValue.Trim
            dr("CD_NRS_TO") = .lblCdNrsTo.TextValue.Trim
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            Dim delflg As String = String.Empty
            If .lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If

            dr("SYS_DEL_FLG") = delflg

        End With

        dt.Rows.Add(dr)

    End Sub

#End Region

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
    Private Function ShowPopupControl(ByVal frm As LMM290F, ByVal objNm As String, ByVal eventshubetsu As LMM290C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            '背景色の設定
            Call Me.SetBackColor(frm)

            Select Case objNm
                '荷主POP
                Case .txtCustCdL.Name, .txtCustCdM.Name, .txtCustCdLTo.Name, .txtCustCdMTo.Name

                    Call Me.SetReturnCustPop(frm, objNm, eventshubetsu)



                Case .txtCdCust.Name, .txtCdCustTo.Name

                    Call Me.SetReturnGoodsPop(frm, objNm, eventshubetsu)


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
    Private Function SetReturnCustPop(ByVal frm As LMM290F, ByVal objNm As String, ByVal eventshubetsu As LMM290C.EventShubetsu) As Boolean
        Dim ctls As Win.InputMan.LMImTextBox() = Me.GetCustControls(frm, objNm)
        Dim prm As LMFormData = Me.ShowCustPopup(frm, eventshubetsu, ctls)

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            ctls(LMM290C.MasterRow.CDL).TextValue = dr.Item("CUST_CD_L").ToString()
            ctls(LMM290C.MasterRow.NML).TextValue = dr.Item("CUST_NM_L").ToString()
            ctls(LMM290C.MasterRow.CDM).TextValue = dr.Item("CUST_CD_M").ToString()
            ctls(LMM290C.MasterRow.NMM).TextValue = dr.Item("CUST_NM_M").ToString()
            ctls(LMM290C.MasterRow.CDS).TextValue = dr.Item("CUST_CD_S").ToString()
            ctls(LMM290C.MasterRow.NMS).TextValue = dr.Item("CUST_NM_S").ToString()
            ctls(LMM290C.MasterRow.CDSS).TextValue = dr.Item("CUST_CD_SS").ToString()
            ctls(LMM290C.MasterRow.NMSS).TextValue = dr.Item("CUST_NM_SS").ToString()

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ctls"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMM290F, ByVal eventshubetsu As LMM290C.EventShubetsu, ByVal ctls As Win.InputMan.LMImTextBox()) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM290C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = ctls(LMM290C.MasterRow.CDL).TextValue
                .Item("CUST_CD_M") = ctls(LMM290C.MasterRow.CDM).TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 荷主マスタ照会：コントロール配列作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCustControls(ByVal frm As LMM290F, ByVal objNm As String) As Win.InputMan.LMImTextBox()


        With frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Return New Win.InputMan.LMImTextBox() {.txtCustCdL, .lblCustNmL, .txtCustCdM, .lblCustNmM _
                                                           , .lblCustCdS, .lblCustNmS, .lblCustCdSS, .lblCustNmSS}

                Case Else

                    Return New Win.InputMan.LMImTextBox() {.txtCustCdLTo, .lblCustNmLTo, .txtCustCdMTo, .lblCustNmMTo _
                                       , .lblCustCdSTo, .lblCustNmSTo, .lblCustCdSSTo, .lblCustNmSSTo}

            End Select

        End With

    End Function

    ''' <summary>
    ''' 商品Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMM290F, ByVal objNm As String, ByVal eventshubetsu As LMM290C.EventShubetsu) As Boolean
        Dim ctls As Win.InputMan.LMImTextBox() = Me.GetGoodsControls(frm, objNm)
        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, ctls, eventshubetsu)

        '解除しないと制御できない
        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
            ctls(LMM290C.MasterRow.CDL).TextValue = dr.Item("CUST_CD_L").ToString()
            ctls(LMM290C.MasterRow.NML).TextValue = dr.Item("CUST_NM_L").ToString()
            ctls(LMM290C.MasterRow.CDM).TextValue = dr.Item("CUST_CD_M").ToString()
            ctls(LMM290C.MasterRow.NMM).TextValue = dr.Item("CUST_NM_M").ToString()
            ctls(LMM290C.MasterRow.CDS).TextValue = dr.Item("CUST_CD_S").ToString()
            ctls(LMM290C.MasterRow.NMS).TextValue = dr.Item("CUST_NM_S").ToString()
            ctls(LMM290C.MasterRow.CDSS).TextValue = dr.Item("CUST_CD_SS").ToString()
            ctls(LMM290C.MasterRow.NMSS).TextValue = dr.Item("CUST_NM_SS").ToString()
            ctls(LMM290C.MasterRow.CDCUST).TextValue = dr.Item("GOODS_CD_CUST").ToString()
            ctls(LMM290C.MasterRow.CDNRS).TextValue = dr.Item("GOODS_CD_NRS").ToString()
            ctls(LMM290C.MasterRow.NM1).TextValue = dr.Item("GOODS_NM_1").ToString()
            ctls(LMM290C.MasterRow.PKG).TextValue = dr.Item("PKG_UT_NM").ToString()
            ctls(LMM290C.MasterRow.IRIME).TextValue = String.Concat((dr.Item("STD_IRIME_NB").ToString), " ", (dr.Item("STD_IRIME_UT_NM").ToString))
            '隠し項目
            ctls(LMM290C.MasterRow.HIDCDL).TextValue = dr.Item("CUST_CD_L").ToString()

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMM290F, ByVal ctls As Win.InputMan.LMImTextBox(), ByVal eventshubetsu As LMM290C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = ctls(LMM290C.MasterRow.CDL).TextValue
            .Item("CUST_CD_M") = ctls(LMM290C.MasterRow.CDM).TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM290C.EventShubetsu.ENTER Then
                .Item("GOODS_CD_CUST") = ctls(LMM290C.MasterRow.CDCUST).TextValue
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 商品マスタ照会：コントロール配列作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGoodsControls(ByVal frm As LMM290F, ByVal objNm As String) As Win.InputMan.LMImTextBox()


        With frm

            Select Case objNm

                Case .txtCdCust.Name

                    Return New Win.InputMan.LMImTextBox() {.txtCustCdL, .lblCustNmL, .txtCustCdM, .lblCustNmM _
                                                           , .lblCustCdS, .lblCustNmS, .lblCustCdSS, .lblCustNmSS _
                                                           , .txtCdCust, .lblCdNrs, .lblNm1, .lblPkg, .lblIrime _
                                                           , .lblGoodsCustL}

                Case Else

                    Return New Win.InputMan.LMImTextBox() {.txtCustCdLTo, .lblCustNmLTo, .txtCustCdMTo, .lblCustNmMTo _
                                                           , .lblCustCdSTo, .lblCustNmSTo, .lblCustCdSSTo, .lblCustNmSSTo _
                                                           , .txtCdCustTo, .lblCdNrsTo, .lblNm1To, .lblPkgTo, .lblIrimeTo _
                                                           , .lblGoodsCustLTo}

            End Select

        End With

    End Function
#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Me.NewDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey4Press(ByRef frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveItemData")

        Me.SaveItemData(frm, LMM290C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM290F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMM290F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM290F_KeyDown(ByVal frm As LMM290F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM290F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM290F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM290F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class