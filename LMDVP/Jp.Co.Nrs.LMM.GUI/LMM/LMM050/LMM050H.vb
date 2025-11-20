' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM050H : 請求先マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI
Imports GrapeCity.Win.Editors

''' <summary>
''' LMM050ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM050H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM050V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM050G

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

    ''' <summary>
    ''' 非必須部署フラグ
    ''' </summary>
    Private _NotHissuBusyo As Boolean = False
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
        Dim frm As LMM050F = New LMM050F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sFrom, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM050V(Me, frm, Me._LMMConV)

        'Gamenクラスの設定
        Me._G = New LMM050G(Me, frm, Me._LMMConG)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '必須項目変更のための部署コードリスト取得
        Call Me.SetDataSetBusyoCd()

        'コントロール個別設定
        Call Me._G.SetControl(_NotHissuBusyo)

        Call Me.SelectComboSeiqCurrCd(frm)

        'コンボボックス設定
        Call Me._G.SetComboControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目ロック
        Call Me._LMMConG.SetLockControl(frm, True)

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM050C.EventShubetsu.MAIN)

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
    Private Sub NewDataEvent(ByVal frm As LMM050F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM050C.EventShubetsu.SHINKI) = False Then
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

        '画面項目全クリア
        Call Me._G.ClearControl(frm)

        '画面初期値設定
        Call Me._G.DefaultSetControl()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM050C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM050F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM050C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtSeiqtoCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM050C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim ds As DataSet = New LMM050DS()
        Call SetDatasetUserItemData(frm, ds)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM050BLF", "HaitaData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)

            '終了処理
            Call Me._LMMConH.EndAction(frm)

            '画面全ロックの解除
            MyBase.UnLockedControls(frm)

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

            '終了処理
            Call Me._LMMConH.EndAction(frm)

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

            '画面全ロックの解除
            Me.UnLockedControls(frm)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()


            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM050C.EventShubetsu.HENSHU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM050F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM050C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtSeiqtoCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM050C.EventShubetsu.HUKUSHA) = False Then
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
        Call Me._G.SetFoucus(LMM050C.EventShubetsu.HUKUSHA)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージエリアの設定
        Me.ShowMessage(frm, "G003")

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM050F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM050C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtSeiqtoCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM050C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '2016.01.06 UMANO 英語化対応START
        Dim str As String() = Split(frm.FunctionKey.F4ButtonName, "･")
        '2016.01.06 UMANO 英語化対応END

        '削除フラグチェック
        If frm.lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) Then
            '2016.01.06 UMANO 英語化対応START
            'Select Case MyBase.ShowMessage(frm, "C001", New String() {"削除"})
            Select Case MyBase.ShowMessage(frm, "C001", New String() {str(0)})
                '2016.01.06 UMANO 英語化対応END
                Case MsgBoxResult.Cancel '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        Else
            '2016.01.06 UMANO 英語化対応START
            'Select Case MyBase.ShowMessage(frm, "C001", New String() {"復活"})
            Select Case MyBase.ShowMessage(frm, "C001", New String() {str(1)})
                '2016.01.06 UMANO 英語化対応END
                Case MsgBoxResult.Cancel  '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM050DS()
        Call Me.SetDatasetDelData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM050BLF", "DeleteData", ds)

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
        MyBase.LMCacheMasterData(LMConst.CacheTBL.SEIQTO)

        'メッセージ用請求先コード格納
        Dim seiqCd As String = frm.txtSeiqtoCd.TextValue
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName _
        '                              , String.Concat("[ ", LMM050C.SEIQTOMSG, " = ", seiqCd, " ]")})
        MyBase.ShowMessage(frm, "G080", New String() {frm.FunctionKey.F4ButtonName, seiqCd})
        '2016.01.06 UMANO 英語化対応END

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
    Private Sub SelectListEvent(ByVal frm As LMM050F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM050C.EventShubetsu.KENSAKU) = False Then
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
        Call Me._G.SetFoucus(LMM050C.EventShubetsu.KENSAKU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM050F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    Private Sub SprCellSelect(ByVal frm As LMM050F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
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
    Private Sub RowSelection(ByVal frm As LMM050F, ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM050C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM050G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(LMM050C.EventShubetsu.SANSHO, frm.lblSituation.RecordStatus)

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
    Private Sub MasterShowEvent(ByVal frm As LMM050F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM050C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM050C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM050C.EventShubetsu.MASTEROPEN)

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
    Private Function SaveSeiqtoItemData(ByVal frm As LMM050F, ByVal eventShubetsu As LMM050C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM050C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        '必須項目変更のための部署コードリスト取得
        Call Me.SetDataSetBusyoCd()

        ''項目チェック
        If Me._V.IsSaveInputChk(_NotHissuBusyo) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        '変動保管料適用と荷主マスタの関連チェック
        If frm.optVarStrageFlgY.Checked Then
            Dim dsVar As DataSet = New LMM050DS()
            Dim drVar As DataRow = dsVar.Tables(LMM050C.TABLE_NM_VAR_STRAGE).NewRow()
            drVar("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
            drVar("SEIQTO_CD") = frm.txtSeiqtoCd.TextValue.Trim
            dsVar.Tables(LMM050C.TABLE_NM_VAR_STRAGE).Rows.Add(drVar)

            dsVar = Me.CallWSA("LMM050BLF", "SelectVarStrage", dsVar)

            If dsVar.Tables(LMM050C.TABLE_NM_VAR_STRAGE).Rows.Count > 0 Then
                MyBase.ShowMessage(frm, "E02N")
                Call Me._LMMConH.EndAction(frm) '終了処理
                Return False
            End If
        End If

        '変動保管料適用の確認
        If frm.optVarStrageFlgY.Checked Then
            If MyBase.ShowMessage(frm, "W315") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                Return False
            End If
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM050DS()
        Call Me.SetDatasetUserItemData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM050BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM050BLF", "UpdateData", ds)
        End Select

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then

            Dim errCtl As Control = Nothing

            Select Case MyBase.GetMessageID()

                ''2011.09.08 検証結果_導入時要望№1対応 START
                'Case "E079"

                '    MyBase.SetMessage("E079", New String() {"郵便番号マスタ", frm.txtZip.TextValue})
                '    errCtl = frm.txtZip
                ''2011.09.08 検証結果_導入時要望№1対応 END

                Case "E010"

                    '③重複チェックエラー
                    errCtl = frm.txtSeiqtoCd

            End Select

            MyBase.ShowMessage(frm)

            'エラーコントロールがある場合、フォーカス設定
            If errCtl Is Nothing = False Then
                Call Me._LMMConV.SetErrorControl(errCtl)
            End If

            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.SEIQTO)

        '処理結果メッセージ表示
        Dim seiqCd As String = frm.txtSeiqtoCd.TextValue
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
        '                                              , String.Concat("[", LMM050C.SEIQTOMSG, " = ", seiqCd, "]")})
        MyBase.ShowMessage(frm, "G080", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), seiqCd})
        '2016.01.06 UMANO 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM050C.EventShubetsu.MAIN)

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
    Friend Sub CloseFormEvent(ByVal frm As LMM050F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveSeiqtoItemData(frm, LMM050C.EventShubetsu.TOJIRU) = False Then

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
    Private Sub EnterAction(ByVal frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM050C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM050C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM050C.EventShubetsu.ENTER)

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
    ''' 起動時処理(必須項目の変更)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetBusyoCd()

        'ログインユーザーの部署
        Dim busyo As String = String.Empty
        Dim busyoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LMUserInfoManager.GetUserID, "'"))
        If 0 < busyoDr.Length Then
            busyo = busyoDr(0).Item("BUSYO_CD").ToString
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM050DS()
        Dim row As DataRow = ds.Tables("LMM050_BUSYO_CD").NewRow()
        row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd
        ds.Tables("LMM050_BUSYO_CD").Rows.Add(row)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SetDataSetBusyoCd")
        '==========================
        'WSAクラス呼出
        '==========================
        ds = Me.CallWSA("LMM050BLF", "SelectBusyo", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SetDataSetBusyoCd")

        '日陸経理コード(JDE)非必須部署コードリスト取得
        Dim aa As DataRow() = ds.Tables(LMM050C.TABLE_NM_BUSYO_CD).Select(String.Concat("BUSYO_CD = '", busyo, "'"))
        '非必須部署か判断
        If 0 < aa.Length Then
            _NotHissuBusyo = True
        End If

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM050F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM050DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM050BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM050BLF", "SelectListData", _FindDs _
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
    Private Sub SuccessSelect(ByVal frm As LMM050F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM050C.TABLE_NM_OUT)

        '画面解除
        MyBase.UnLockedControls(frm)

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
    ''' 請求通貨コードコンボ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectComboSeiqCurrCd(ByVal frm As LMM050F)

        Dim item As String = String.Empty
        'DataSet設定
        Dim rtnDs As DataSet = New LMM050DS()
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectComboSeiqCurrCd")
        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me.CallWSA("LMM050BLF", "SelectComboSeiqCurrCd", rtnDs)
        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectComboSeiqCurrCd")
        Dim dt As DataTable = rtnDs.Tables("LMM050OUT")
        With frm

            Dim max As Integer = dt.Rows.Count() - 1
            .cmbSeiqCurrCd.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

            For i As Integer = 0 To max
                item = dt.Rows(i).Item("SEIQ_CURR_CD").ToString()
                '.cmbSeiqCurrCd.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(i.ToString())}))
                .cmbSeiqCurrCd.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(item)}))
            Next

        End With

    End Sub

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM050F)

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
    Private Sub SetGMessage(ByVal frm As LMM050F)

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
    Private Function ShowPopupControl(ByVal frm As LMM050F, ByVal objNm As String, ByVal eventshubetsu As LMM050C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            Select Case objNm

                Case .txtNrsKeiriCd1.Name

                    Call Me.SetReturnSeiqtoPop(frm, objNm, eventshubetsu)

                Case .txtZip.Name

                    Call Me.SetReturnZipPop(frm, objNm, eventshubetsu)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 郵便番号Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnZipPop(ByVal frm As LMM050F, ByVal objNm As String, ByVal eventshubetsu As LMM050C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowZipPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ060C.TABLE_NM_OUT).Rows(0)

            '住所1(都道府県+市区町村名)
            Dim add1 As String = String.Concat(dr.Item("KEN_N").ToString(), dr.Item("CITY_N").ToString())

            ctl.TextValue = dr.Item("ZIP_NO").ToString()         '郵便番号

            If String.IsNullOrEmpty(frm.txtAd1.TextValue) _
            AndAlso String.IsNullOrEmpty(frm.txtAd2.TextValue) Then
                frm.txtAd1.TextValue = add1
                frm.txtAd2.TextValue = dr.Item("TOWN_N").ToString    '住所2(町域名)
            End If

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 郵便番号マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowZipPopup(ByVal frm As LMM050F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM050C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ060DS()
        Dim dt As DataTable = ds.Tables(LMZ060C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM050C.EventShubetsu.ENTER Then
                .Item("ZIP_NO") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ060", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 請求先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSeiqtoPop(ByVal frm As LMM050F, ByVal objNm As String, ByVal eventshubetsu As LMM050C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowSeiqtoPopup(frm, ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)
            ctl.TextValue = dr.Item("SEIQTO_CD").ToString()
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
    Private Function ShowSeiqtoPopup(ByVal frm As LMM050F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM050C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM050C.EventShubetsu.ENTER Then
                .Item("SEIQTO_CD") = frm.txtNrsKeiriCd1.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM050F)

        Dim dr As DataRow = Me._FindDs.Tables(LMM050C.TABLE_NM_IN).NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM050G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM050G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("SEIQTO_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM050G.sprDetailDef.SEIQTO_CD.ColNo))
            dr("SEIQTO_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM050G.sprDetailDef.SEIQTO_NM.ColNo))
            dr("SEIQTO_BUSYO_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM050G.sprDetailDef.SEIQTO_BUSYO_NM.ColNo))
            dr("KOUZA_KB") = Me._LMMConV.GetCellValue(.Cells(0, LMM050G.sprDetailDef.KOUZA_KB_NM.ColNo))
            dr("OYA_PIC") = Me._LMMConV.GetCellValue(.Cells(0, LMM050G.sprDetailDef.OYA_PIC.ColNo))
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            Me._FindDs.Tables(LMM050C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUserItemData(ByVal frm As LMM050F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM050C.TABLE_NM_IN).NewRow()

        Dim sei As String = String.Empty
        Dim huku As String = String.Empty
        Dim hikae As String = String.Empty
        Dim keiri As String = String.Empty
        Dim dest As String = String.Empty

        With frm
            sei = .chkSei.GetBinaryValue()
            huku = .chkFuku.GetBinaryValue()
            hikae = .chkHikae.GetBinaryValue()
            keiri = .chkKeiri.GetBinaryValue()
            dest = .chkdest.GetBinaryValue()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

            '請求書種別はデータ上前ゼロでフォーマット
            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("SEIQTO_CD") = .txtSeiqtoCd.TextValue.Trim
            dr("SEIQTO_NM") = .txtSeiqtoNm.TextValue.Trim
            dr("SEIQTO_BUSYO_NM") = .txtSeiqtoBusyoNm.TextValue.Trim
            dr("KOUZA_KB") = .cmbKouzaKbn.SelectedValue
            dr("MEIGI_KB") = .cmbMeigiKbn.SelectedValue
            dr("DOC_PTN") = .cmbDocPtn.SelectedValue
            'START YANAI 要望番号661
            dr("DOC_PTN2") = .cmbDocPtnNomal.SelectedValue
            'END YANAI 要望番号661


            dr("DOC_SEI_YN") = String.Concat(0, sei)
            dr("DOC_HUKU_YN") = String.Concat(0, huku)
            dr("DOC_HIKAE_YN") = String.Concat(0, hikae)
            dr("DOC_KEIRI_YN") = String.Concat(0, keiri)
            dr("DOC_DEST_YN") = String.Concat(0, dest)  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr("NRS_KEIRI_CD1") = .txtNrsKeiriCd1.TextValue.Trim
            dr("NRS_KEIRI_CD2") = .txtNrsKeiriCd2.TextValue.Trim
            dr("SEIQ_SND_PERIOD") = .txtSeiqSndPeriod.TextValue.Trim
            dr("CUST_KAGAMI_TYPE1") = .txtCustKagamiType1.TextValue.Trim
            dr("CUST_KAGAMI_TYPE2") = .txtCustKagamiType2.TextValue.Trim
            dr("CUST_KAGAMI_TYPE3") = .txtCustKagamiType3.TextValue.Trim
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            dr("CUST_KAGAMI_TYPE4") = .txtCustKagamiType4.TextValue.Trim
            dr("CUST_KAGAMI_TYPE5") = .txtCustKagamiType5.TextValue.Trim
            dr("CUST_KAGAMI_TYPE6") = .txtCustKagamiType6.TextValue.Trim
            dr("CUST_KAGAMI_TYPE7") = .txtCustKagamiType7.TextValue.Trim
            dr("CUST_KAGAMI_TYPE8") = .txtCustKagamiType8.TextValue.Trim
            dr("CUST_KAGAMI_TYPE9") = .txtCustKagamiType9.TextValue.Trim
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
            dr("OYA_PIC") = .txtOyaPic.TextValue.Trim
            dr("TEL") = .txtTel.TextValue.Trim
            dr("FAX") = .txtFax.TextValue.Trim
            dr("CLOSE_KB") = .cmbCloseKBN.SelectedValue
            dr("ZIP") = .txtZip.TextValue.Trim
            dr("AD_1") = .txtAd1.TextValue.Trim
            dr("AD_2") = .txtAd2.TextValue.Trim
            dr("AD_3") = .txtAd3.TextValue.Trim

            '(2014.03.17)要望番号2229 請求通貨 追加 -- START --
            dr("SEIQ_CURR_CD") = .cmbSeiqCurrCd.TextValue
            '(2014.03.17)要望番号2229 請求通貨 追加 -- START --
            dr("STORAGE_NR") = .numStorageNr.Value
            dr("STORAGE_NG") = .numStorageNg.Value
            dr("STORAGE_MIN") = .numStorageMinBak.Value
            dr("HANDLING_NR") = .numHandlingNr.Value
            dr("HANDLING_NG") = .numHandlingNg.Value
            dr("UNCHIN_NR") = .numUnchinNr.Value
            dr("UNCHIN_NG") = .numUnchinNg.Value
            dr("SAGYO_NR") = .numSagyoNr.Value
            dr("SAGYO_NG") = .numSagyoNg.Value
            dr("CLEARANCE_NR") = .numClearanceNr.Value
            dr("CLEARANCE_NG") = .numClearanceNg.Value
            dr("YOKOMOCHI_NR") = .numYokomochiNr.Value
            dr("YOKOMOCHI_NG") = .numYokomochiNg.Value
            dr("TOTAL_NR") = .numTotalNr.Value
            dr("TOTAL_NG") = .numTotalNg.Value


            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            dr("TOTAL_MIN_SEIQ_AMT") = .numTotalMinSeiqAmt.TextValue
            dr("STORAGE_TOTAL_FLG") = .chkStorageTotalFlg.GetBinaryValue
            dr("HANDLING_TOTAL_FLG") = .chkHandlingTotalFlg.GetBinaryValue
            dr("UNCHIN_TOTAL_FLG") = .chkUnchinTotalFlg.GetBinaryValue
            dr("SAGYO_TOTAL_FLG") = .chkSagyoTotalFlg.GetBinaryValue
            dr("STORAGE_MIN_AMT") = .numStorageMin.TextValue
            dr("STORAGE_OTHER_MIN_AMT") = .numStorageOtherMin.TextValue
            dr("HANDLING_MIN_AMT") = .numHandlingMin.TextValue
            dr("HANDLING_OTHER_MIN_AMT") = .numHandlingOtherMin.TextValue
            dr("UNCHIN_MIN_AMT") = .numUnchinMin.TextValue
            dr("SAGYO_MIN_AMT") = .numSagyoMin.TextValue
            dr("STORAGE_ZERO_FLG") = .cmbStorageZeroFlgKBN.SelectedValue
            dr("HANDLING_ZERO_FLG") = .cmbHandlingZeroFlgKBN.SelectedValue
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            dr("EIGYO_TANTO") = .txtEigyoTanto.TextValue.Trim
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
            dr("REMARK") = .txtTekiyo.TextValue.Trim        'ADD 2019/07/10 002520
            If .optVarStrageFlgY.Checked Then
                dr.Item("VAR_STRAGE_FLG") = "1"
            Else
                dr.Item("VAR_STRAGE_FLG") = "0"
            End If
            dr.Item("VAR_RATE_3") = .cmbVarRate3.SelectedValue
            dr.Item("VAR_RATE_6") = .cmbVarRate6.SelectedValue
        End With

        ds.Tables(LMM050C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM050F, ByVal ds As DataSet)
        Dim dr As DataRow = ds.Tables(LMM050C.TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("SEIQTO_CD") = .txtSeiqtoCd.TextValue.Trim
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            Dim delflg As String = String.Empty
            If .lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If

            dr("SYS_DEL_FLG") = delflg

        End With

        ds.Tables(LMM050C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey3Press(ByRef frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey4Press(ByRef frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey9Press(ByRef frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByRef frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveSeiqtoItemData")

        Me.SaveSeiqtoItemData(frm, LMM050C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveSeiqtoItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM050F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM050F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

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
    Friend Sub LMM050F_KeyDown(ByVal frm As LMM050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM050F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM050F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM050F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    ''' <summary>
    ''' 変動保管料なし／あり　CheckedChangedイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ctl"></param>
    ''' <param name="e"></param>
    Friend Sub optVarStrageFlgCheckedChanged(ByVal frm As LMM050F, ByVal ctl As Win.LMOptionButton, ByVal e As EventArgs)

        If frm.optVarStrageFlgN.Checked Then
            '変動保管料[なし]が選択された：倍率は入力不可
            frm.cmbVarRate3.Enabled = False
            frm.cmbVarRate6.Enabled = False

        Else
            '変動保管料[あり]が選択された：倍率は入力可
            frm.cmbVarRate3.Enabled = True
            frm.cmbVarRate6.Enabled = True
        End If

    End Sub


    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class