' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM020H : 請求先マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports System.IO
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI

''' <summary>
''' LMM020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM020V
    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM020G

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
        Dim frm As LMM020F = New LMM020F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sFrom, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM020V(Me, frm, Me._LMMConV)

        'Gamenクラスの設定
        Me._G = New LMM020G(Me, frm, Me._LMMConG)

        'フォームの初期化
        MyBase.InitControl(frm)

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
        Call Me._G.SetInitValue(frm)

        If (LMM020C.PGID_LMB020).Equals(MyBase.RootPGID()) = True Then

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()

            '画面全ロックの解除
            MyBase.UnLockedControls(frm)

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

            Call Me._G.ClearControl()

            '入荷データ編集画面からのデータを設定
            Me._G.SetInkaSData(prmDs)

            'メッセージ表示
            MyBase.ShowMessage(frm, "G003")

            'フォーカスの設定
            Call Me._G.SetFoucus(LMM020C.EventShubetsu.SHINKI)

        Else
            'メッセージの表示
            MyBase.ShowMessage(frm, "G007")

            '画面の入力項目ロック
            Call Me._G.LockControl(True)

            'ファンクションキーの制御
            Call Me._G.SetFunctionKey()

            'フォーカスの設定
            Call Me._G.SetFoucus(LMM020C.EventShubetsu.MAIN)

        End If

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
    Private Sub NewDataEvent(ByVal frm As LMM020F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.SHINKI) = False Then
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

        Call Me._G.ClearControl()

        '営業所はログインユーザーの営業所
        Me._G.SetcmbNrsBrCd()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM020C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM020F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtDestCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM020C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim ds As DataSet = New LMM020DS()
        Call SetDatasetUserItemData(frm, ds)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM020BLF", "HaitaData", ds)

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
            MyBase.UnLockedControls(frm)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()


            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM020C.EventShubetsu.HENSHU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM020F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtDestCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM020C.EventShubetsu.HUKUSHA) = False Then
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
        Call Me._G.SetFoucus(LMM020C.EventShubetsu.HUKUSHA)

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
    Private Sub DeleteDataEvent(ByVal frm As LMM020F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtDestCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM020C.EventShubetsu.SAKUJO) = False Then
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
        Dim ds As DataSet = New LMM020DS()
        Call Me.SetDatasetDelData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM020BLF", "DeleteData", ds)

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
        MyBase.LMCacheMasterData(LMConst.CacheTBL.COA)

        'メッセージ用商品コード格納
        Dim goodsCd As String = frm.txtGoodsCd.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName _
                                              , String.Concat("[", LMM020C.GOODSCDMSG, " = ", goodsCd, "]")})

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
    ''' 開く処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OpenDataEvent(ByVal frm As LMM020F)

        '編集モードの場合、スルー
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.OPEN) = False Then
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMM020G.sprDetailDef.DEF.ColNo
        Dim arr As ArrayList = Me._LMMConV.SprSelectCount(frm.sprDetail, defNo)

        '単一選択チェック
        If Me._LMMConV.IsSelectOneChk(arr.Count) = False Then
            Exit Sub
        End If

        '未選択チェック
        If Me._LMMConV.IsSelectChk(arr.Count) = False Then
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, arr, LMM020C.EventShubetsu.OPEN) = False Then
            Exit Sub
        End If


        'ファイルパス名・ファイル名の取得
        Dim link As String = Me._V.PathNmExistchk(arr)
        If link Is Nothing Then
            Exit Sub
        End If

        'パス存在チェック
        If Me._V.FileExistChk(link) = False Then
            Exit Sub
        End If

        'ファイルを開く
        Call Me.OpenFile(frm, link)

    End Sub

    ''' <summary>
    ''' ファイルを開く
    ''' </summary>
    ''' <param name="link"></param>
    ''' <remarks></remarks>
    Private Sub OpenFile(ByVal frm As LMM020F, ByVal link As String)

        Dim Proc As New System.Diagnostics.Process
        Dim targetPath As String = String.Empty
        Dim fileNM As String = String.Empty

        targetPath = link.Substring(0, link.LastIndexOf("\") + 1)
        fileNM = link.Substring(link.LastIndexOf("\") + 1)

        Proc.StartInfo.WorkingDirectory = targetPath
        Proc.StartInfo.FileName = fileNM

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Select Case MyBase.ShowMessage(frm, "C008")
            Case MsgBoxResult.Cancel '「キャンセル」押下時
                Exit Sub
        End Select

        Proc.Start()
        Proc.Close()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM020F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.KENSAKU) = False Then
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
                Call Me.ShowGMessage(frm)
                Exit Sub
            Else      'OK押下
                clearFlg = 1
            End If
        End If

        '検索処理を行う
        Call Me.SelectData(frm, clearFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM020C.EventShubetsu.KENSAKU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM020F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    Private Sub SprCellSelect(ByVal frm As LMM020F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        ''メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        '背景色の設定
        Call Me.SetBackColor(frm)

        Call Me.RowSelection(frm, e.Row)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMM020F, ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM020G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(LMM020C.EventShubetsu.SANSHO, frm.lblSituation.RecordStatus)

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
    Private Sub MasterShowEvent(ByVal frm As LMM020F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM020C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM020C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM020C.EventShubetsu.MASTEROPEN)

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
    Private Function SaveItemData(ByVal frm As LMM020F, ByVal eventShubetsu As LMM020C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '背景色の設定
        Call Me.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        'サーバーパス名の取得
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim svPath As String = Me.KbnSvString(LMUserInfoManager.GetNrsBrCd)
        Dim svPath As String = Me.KbnSvString(frm.cmbNrsBrCd.SelectedValue.ToString())

        '画面のパス名とファイル名
        Dim coaLink As String = frm.lblCoaLink.TextValue
        Dim coaNm As String = frm.lblCoaName.TextValue

        '隠し項目のパス名とファイル名
        Dim hidLink As String = frm.lblSvPath.TextValue
        Dim hidNm As String = frm.lblSvFileNm.TextValue

        '項目チェック
        If Me._V.IsSaveInputChk(svPath, coaLink, coaNm) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        '変更されたファイルがあるときファイル名に日時をセット
        Dim backDirectory As String = String.Empty
        If Me._V.IsCoaHidChk(coaLink, coaNm, hidLink, hidNm) = True Then
            backDirectory = Me.FileSysDateTimeSet(frm, coaLink, coaNm, svPath)
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM020DS()
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
                rtnDs = MyBase.CallWSA("LMM020BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM020BLF", "UpdateData", ds)
        End Select

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me.DeleteBackDirectory(backDirectory)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        Dim svPathCp As String = frm.lblSvPath.TextValue
        Dim svNmCp As String = frm.lblSvFileNm.TextValue

        'ファイルをファイルサーバーにコピー
        If Me._V.IsCoaHidChk(coaLink, svNmCp, svPathCp, svNmCp) = True Then
            Call Me.PathCopySv(frm, backDirectory, svPathCp, svNmCp)
        End If
        Call Me.DeleteBackDirectory(backDirectory)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.COA)

        'メッセージ用商品コード格納
        Dim goodsCd As String = frm.txtGoodsCd.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat("[", LMM020C.GOODSCDMSG, " = ", goodsCd, "]")})

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM020C.EventShubetsu.MAIN)

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
    Friend Sub CloseFormEvent(ByVal frm As LMM020F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveItemData(frm, LMM020C.EventShubetsu.TOJIRU) = False Then

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
    Private Sub EnterAction(ByVal frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM020C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM020C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM020C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMMConH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' クリアボタン押下処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ClearFileEvent(ByVal frm As LMM020F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = False Then
            'メッセージ設定
            Call Me.SetGMessage(frm)
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.CLEAR) = False Then
            Exit Sub
        End If

        'ファイルパス名ラベルに値があるかどうかチェック
        If Me._V.IslblPathFileInputChk() = False Then
            Exit Sub
        End If

        'パス名、ファイル名、隠し項目クリア
        With frm
            .lblCoaLink.TextValue = String.Empty
            .lblCoaName.TextValue = String.Empty
            .lblSvPath.TextValue = String.Empty
            .lblSvFileNm.TextValue = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' ファイル追加処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub AddFileEvent(ByVal frm As LMM020F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = False Then
            'メッセージ設定
            Call Me.SetGMessage(frm)
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(LMM020C.EventShubetsu.ADD) = False Then
            Exit Sub
        End If

        'もともとのリンク名・ファイル名を格納
        Dim orgFilePath As String = frm.lblCoaLink.TextValue
        Dim orgFileNm As String = frm.lblCoaName.TextValue

        Dim ofd As OpenFileDialog = New OpenFileDialog()
        ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        ofd.Filter = LMM020C.FILETYPE
        ofd.FilterIndex = LMM020C.ALL_FILE
        ofd.Title = LMM020C.DLGTITLE
        ofd.RestoreDirectory = True

        ofd.CheckFileExists = True
        ofd.CheckPathExists = True

        Dim filePath As String = String.Empty
        Dim fileNm As String = String.Empty

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.OK Then
            Console.WriteLine(ofd.FileName)
            'ディレクトリ名の取得
            filePath = Path.GetDirectoryName(ofd.FileName)
            'ファイル名の取得
            fileNm = Path.GetFileName(ofd.FileName)
        Else
            'キャンセルを押されたときなど選択されなかったとき押下前の状態に戻す
            filePath = orgFilePath
            fileNm = orgFileNm
        End If

        frm.lblCoaLink.TextValue = filePath
        frm.lblCoaName.TextValue = fileNm

    End Sub

    'ADD START 2018/11/14 要望番号001939
    ''' <summary>
    ''' 汎用チェックボックスチェック状態反映処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ChangeInkaDateVersFlgEvent(ByVal frm As LMM020F)

        '入荷日・汎用チェックボックス編集可の場合のみ
        If frm.lblInkaDateEditableFlg.TextValue = "1" Then
            If frm.chkInkaDateVersFlg.Checked Then
                frm.imdInkaDate.TextValue = String.Empty
                frm.lblInkaDateVersFlg.TextValue = "1"
            Else
                frm.lblInkaDateVersFlg.TextValue = String.Empty
            End If
        End If

    End Sub

    ''' <summary>
    ''' 入荷日入力状態反映処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ChangeInkaDateEvent(ByVal frm As LMM020F)

        '入荷日・汎用チェックボックス編集可の場合のみ
        If frm.lblInkaDateEditableFlg.TextValue = "1" Then
            If Not String.IsNullOrEmpty(frm.imdInkaDate.TextValue) Then
                frm.chkInkaDateVersFlg.Checked = False
            End If
        End If

    End Sub


    ''' <summary>
    ''' 荷主コード変更反映処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ChangeCustCdEvent(ByVal frm As LMM020F)

        '新規の場合
        If frm.lblSituation.RecordStatus = RecordStatus.NEW_REC Then

            'コントロールの編集可否
            Dim ctrlEnabled As Boolean

            If GetCoaInkaDateFlg(frm) = "1" Then
                '分析票マスタ入荷日フラグ='1'(あり)の場合、編集可
                ctrlEnabled = True
            Else
                '上記以外の場合、編集不可
                ctrlEnabled = False

                '編集不可の場合は「汎用」
                frm.imdInkaDate.TextValue = String.Empty
                frm.chkInkaDateVersFlg.Checked = True
            End If

            'コントロールの編集可否設定
            frm.imdInkaDate.ReadOnly = Not ctrlEnabled
            frm.chkInkaDateVersFlg.Enabled = ctrlEnabled

            '入荷日・汎用チェックボックス編集可否フラグの設定
            frm.lblInkaDateEditableFlg.TextValue = If(ctrlEnabled, "1", "0")
        End If

    End Sub
    'ADD END   2018/11/14 要望番号001939

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM020F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM020DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM020BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM020BLF", "SelectListData", _FindDs _
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
    Private Sub SuccessSelect(ByVal frm As LMM020F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM020C.TABLE_NM_OUT)

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

#End Region

#Region "ファイル関連メソッド"

    ''' <summary>
    ''' ファイル名にシステム日付を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="coaLink">パス名</param>
    ''' <param name="coaNm">ファイル名</param>
    ''' <param name="svPath">サーバパス名</param>
    ''' <returns>BACKファイルディレクトリ</returns>
    ''' <remarks></remarks>
    Private Function FileSysDateTimeSet(ByVal frm As LMM020F _
                                       , ByVal coaLink As String _
                                       , ByVal coaNm As String _
                                       , ByVal svPath As String) As String

        'ファイル名を変更(システム日付･時刻を付ける)
        coaNm = Me.FileNameSysDateTime(coaLink, coaNm)

        '保存するファイル名
        Dim svFileNm As String = Path.GetFileName(coaNm)

        'サーバーに保存する分析票ファイルパスとファイル名を隠し項目に格納
        With frm

            .lblSvPath.TextValue = svPath
            .lblSvFileNm.TextValue = svFileNm

        End With

        Return Path.GetDirectoryName(coaNm)

    End Function

    ''' <summary>
    ''' ファイルをBACKフォルダにコピー
    ''' </summary>
    ''' <param name="coaLink">パス名</param>
    ''' <param name="coaNm">ファイル名</param>
    ''' <returns>フルパス(コピー先)</returns>
    ''' <remarks></remarks>
    Private Function FileNameSysDateTime(ByVal coaLink As String, ByVal coaNm As String) As String

        Dim pathNm As String = LMM020C.DEFADRIVE
        Dim cnt As Integer = 0

        '存在しないBACKフォルダディレクトリ名を設定
        While Directory.Exists(pathNm) = True

            cnt += 1
            pathNm = String.Concat(pathNm, cnt.ToString())

        End While

        '新しくフォルダを作成
        Directory.CreateDirectory(pathNm)

        'ファイルをコピーする
        File.Copy(Path.Combine(coaLink, coaNm), Path.Combine(pathNm, coaNm))

        'コピーしたファイル名を変更
        '拡張子の取得
        Dim ext As String = Path.GetExtension(coaNm)

        '拡張子なしのファイル名
        Dim fileNm As String = Path.GetFileNameWithoutExtension(coaNm)

        '画面にあるファイルパス＋ファイル名
        Dim coaPathNoTime As String = Path.Combine(pathNm, coaNm)

        'システム日時を取得してもともとのファイルパス＋ファイル名に付加
        Dim coaNmTime As String = String.Concat(fileNm, Me.SetSysDateTime(), ext)

        coaNmTime = Path.Combine(pathNm, coaNmTime)

        'ファイル名を変更
        File.Move(coaPathNoTime, coaNmTime)

        Return coaNmTime

    End Function

    ''' <summary>
    ''' BACKフォルダ削除処理
    ''' </summary>
    ''' <param name="pathNm">パス名</param>
    ''' <remarks></remarks>
    Private Sub DeleteBackDirectory(ByVal pathNm As String)

        '空の場合、スルー
        If String.IsNullOrEmpty(pathNm) = True Then
            Exit Sub
        End If

        'フォルダがない場合、スルー
        If Directory.Exists(pathNm) = False Then
            Exit Sub
        End If

        'フォルダを全削除
        Directory.Delete(pathNm, True)

    End Sub

    ''' <summary>
    ''' ファイル名に付加するシステム日時の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSysDateTime() As String

        Dim dsDateTime As DataSet = New LMM020DS()
        Dim timeDs As DataSet = MyBase.CallWSA("LMM020BLF", "SetSysDateTime", dsDateTime)
        Dim dr As DataRow = timeDs.Tables(LMM020C.TABLE_NM_TIME).Rows(0)

        Dim dateTime As String = String.Concat(dr.Item("SYS_DATE").ToString(), "_", dr.Item("SYS_TIME").ToString())

        Return dateTime

    End Function


    ''' <summary>
    ''' ファイルをサーバーへコピーする
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="link"></param>
    ''' <param name="nm"></param>
    ''' <remarks></remarks>
    Private Sub PathCopySv(ByVal frm As LMM020F, ByVal coaLink As String, ByVal link As String, ByVal nm As String)

        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim nrsBrCd As String = LMUserInfoManager.GetNrsBrCd()
        Dim nrsBrCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
        Dim svLink As String = String.Empty
        Dim clLink As String = String.Empty

        'コピー先のパス名
        svLink = Path.Combine(link, nm)

        'コピー元のパス名
        clLink = Path.Combine(coaLink, nm)

        If svLink <> clLink Then
            'コピー
            Call Me.FileCopyServer(clLink, svLink)
        End If

    End Sub

    ''' <summary>
    ''' ファイルをローカルからサーバーにコピー
    ''' </summary>
    ''' <param name="clLink">コピー元</param>
    ''' <param name="svLink">コピー先</param>
    ''' <remarks></remarks>
    Private Sub FileCopyServer(ByVal clLink As String, ByVal svLink As String)

        'ファイルをコピーする
        File.Copy(clLink, svLink)

    End Sub

    ''' <summary>
    ''' 区分マスタからC￥のパス名を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function KbnCoaString(ByVal kbnNm As String) As String

        'C:\LM\COAフォルダまたはC:\LM\COABACKフォルダの名前取得
        Return (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                                .Select(String.Concat("KBN_GROUP_CD = 'B009' AND KBN_CD = ", "00"))(0).Item(kbnNm)).ToString()

    End Function

    ''' <summary>
    ''' 区分マスタからファイルサーバーのパス名を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function KbnSvString(ByVal nrsBrCd As String) As String

        'サーバーパスの名前取得
        'Return (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                        .Select(String.Concat("KBN_GROUP_CD = 'B010' AND KBN_CD = '", nrsBrCd, "'"))(0).Item(LMM020C.KBN_NM1)).ToString()
        'テスト用
        Return (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                        .Select(String.Concat("KBN_GROUP_CD = 'B010' AND KBN_CD = '", nrsBrCd, "'"))(0).Item(LMM020C.KBN_NM3)).ToString()

    End Function

#End Region

    ''' <summary>
    ''' 背景色の設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetBackColor(ByVal frm As LMM020F)

        Call Me._LMMConG.SetBackColor(frm.lblGoodsKey)
        Call Me._LMMConG.SetBackColor(frm.lblCoaLink)

    End Sub



#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM020F)

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
    Private Sub SetGMessage(ByVal frm As LMM020F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub


    ''' <summary>
    ''' 注意メッセージを設定する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowCMessage(ByVal frm As LMM020F)

        Dim messageId As String = "C008"


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
    Private Function ShowPopupControl(ByVal frm As LMM020F, ByVal objNm As String, ByVal eventshubetsu As LMM020C.EventShubetsu) As Boolean

        With frm

            Dim rtnResult As Boolean = False

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            '背景色の設定
            Call Me.SetBackColor(frm)

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    rtnResult = Me.SetReturnCustPop(frm, eventshubetsu)

                Case .txtGoodsCd.Name

                    rtnResult = Me.SetReturnGoodsPop(frm, eventshubetsu)

                Case .txtDestCd.Name

                    rtnResult = Me.SetReturnDestPop(frm, eventshubetsu)

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
    Private Function SetReturnCustPop(ByVal frm As LMM020F, ByVal eventshubetsu As LMM020C.EventShubetsu) As Boolean

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
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMM020F, ByVal eventshubetsu As LMM020C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM020C.eventshubetsu.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 商品Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMM020F, ByVal eventshubetsu As LMM020C.EventShubetsu) As Boolean


        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, eventshubetsu)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtGoodsCd.TextValue = dr.Item("GOODS_CD_CUST").ToString()
                .lblGoodsNm.TextValue = dr.Item("GOODS_NM_1").ToString()
                .lblGoodsKey.TextValue = dr.Item("GOODS_CD_NRS").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMM020F, ByVal eventshubetsu As LMM020C.EventShubetsu) As LMFormData

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
            If eventshubetsu = LMM020C.EventShubetsu.ENTER Then
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
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMM020F, ByVal eventshubetsu As LMM020C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowDestPopup(frm, eventshubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtDestCd.TextValue = dr.Item("DEST_CD").ToString()
                .lblDestNm.TextValue = dr.Item("DEST_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMM020F, ByVal eventshubetsu As LMM020C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM020C.EventShubetsu.ENTER Then
                .Item("DEST_CD") = frm.txtDestCd.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM020F)

        Dim dr As DataRow = Me._FindDs.Tables(LMControlC.LMM020C_TABLE_NM_IN).NewRow()

        dr("REC_CHK") = frm.chkRec.GetBinaryValue()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("CUST_CD_L") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.CUST_CD_L.ColNo))
            dr("CUST_NM_L") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.CUST_NM_L.ColNo))
            dr("GOODS_CD_CUST") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.GOODS_CD_CUST.ColNo))
            dr("GOODS_NM_1") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.GOODS_NM_1.ColNo))
            dr("LOT_NO") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.LOT_NO.ColNo))
            dr("DEST_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.DEST_NM.ColNo))
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM020G.sprDetailDef.NRS_BR_NM.ColNo))

            Me._FindDs.Tables(LMControlC.LMM020C_TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUserItemData(ByVal frm As LMM020F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMControlC.LMM020C_TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue.Trim
            dr("CUST_CD_M") = .txtCustCdM.TextValue.Trim
            dr("GOODS_CD_NRS") = .lblGoodsKey.TextValue.Trim
            dr("LOT_NO") = .txtLotNo.TextValue.Trim
            dr("DEST_CD") = .txtDestCd.TextValue.Trim
            'ADD START 2018/11/14 要望番号001939
            dr("INKA_DATE") = .imdInkaDate.TextValue.Trim
            dr("INKA_DATE_VERS_FLG") = .lblInkaDateVersFlg.TextValue.Trim
            'ADD END   2018/11/14 要望番号001939
            dr("COA_LINK") = .lblSvPath.TextValue.Trim
            dr("COA_NAME") = .lblSvFileNm.TextValue.Trim

            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

        End With

        ds.Tables(LMControlC.LMM020C_TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM020F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMControlC.LMM020C_TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("GOODS_CD_NRS") = .lblGoodsKey.TextValue.Trim
            dr("DEST_CD") = .txtDestCd.TextValue.Trim
            dr("LOT_NO") = .txtLotNo.TextValue.Trim
            'ADD START 2018/11/14 要望番号001939
            dr("INKA_DATE") = .imdInkaDate.TextValue.Trim
            dr("INKA_DATE_VERS_FLG") = .lblInkaDateVersFlg.TextValue.Trim
            'ADD END   2018/11/14 要望番号001939
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

        ds.Tables(LMControlC.LMM020C_TABLE_NM_IN).Rows.Add(dr)

    End Sub

#End Region

    'ADD START 2018/11/14 要望番号001939
#Region "マスタ取得"
    ''' <summary>
    ''' 荷主マスタ.分析票マスタ入荷日フラグを取得する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>荷主マスタ.分析票マスタ入荷日フラグ</returns>
    ''' <remarks></remarks>
    Friend Function GetCoaInkaDateFlg(ByVal frm As LMM020F) As String
        With frm

            '営業所コードおよび荷主(大)コードが入力されている場合
            If Not String.IsNullOrEmpty(.cmbNrsBrCd.SelectedValue.ToString()) AndAlso Not String.IsNullOrEmpty(.txtCustCdL.TextValue) Then

                '荷主マスタ取得条件式の生成
                Dim SelectCustString As String = String.Empty
                '削除フラグ
                SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")
                '営業所コード
                SelectCustString = String.Concat(SelectCustString, " AND NRS_BR_CD = '", .cmbNrsBrCd.SelectedValue.ToString(), "' ")
                '荷主コード（大）
                SelectCustString = String.Concat(SelectCustString, " AND CUST_CD_L = '", .txtCustCdL.TextValue, "' ")
                '荷主コード（中）
                SelectCustString = String.Concat(SelectCustString, " AND CUST_CD_M = '00' ")
                '荷主コード（小）
                SelectCustString = String.Concat(SelectCustString, " AND CUST_CD_S = '00' ")
                '荷主コード（極小）
                SelectCustString = String.Concat(SelectCustString, " AND CUST_CD_SS = '00' ")

                '荷主マスタ取得
                Dim drsCust As DataRow()
                drsCust = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(SelectCustString)

                '該当レコードありの場合
                If drsCust.Length > 0 Then
                    '分析票マスタ入荷日フラグの取得値を戻す
                    Return drsCust(0).Item("COA_INKA_DATE_FLG").ToString()
                End If
            End If

            Return "0"
        End With

    End Function
#End Region
    'ADD END   2018/11/14 要望番号001939
#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey3Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey4Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")
        '開く処理
        Me.OpenDataEvent(frm)

        Logger.EndLog(Me.GetType.Name, "")

    End Sub
    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveItemData")

        Me.SaveItemData(frm, LMM020C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM020F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM020F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

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
    Friend Sub LMM020F_KeyDown(ByVal frm As LMM020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM020F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM020F_KeyDown")

    End Sub

    ''' <summary>
    ''' 追加ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnAdd_Click(ByVal frm As LMM020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.AddFileEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub


    ''' <summary>
    ''' クリアボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnClear_Click(ByVal frm As LMM020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ClearFileEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM020F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    'ADD START 2018/11/14 要望番号001939
    ''' <summary>
    ''' 入荷日のロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub imdInkaDate_Leave(ByVal frm As LMM020F, e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "imdInkaDate_Leave")

        Call Me.ChangeInkaDateEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "imdInkaDate_Leave")

    End Sub

    ''' <summary>
    ''' 汎用チェックボックスのチェック状態変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub chkInkaDateVersFlg_CheckedChanged(ByVal frm As LMM020F, e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "chkInkaDateVersFlg_CheckedChanged")

        Call Me.ChangeInkaDateVersFlgEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "chkInkaDateVersFlg_CheckedChanged")

    End Sub

    ''' <summary>
    ''' 荷主コード(大)(中)のロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCd_Leave(ByVal frm As LMM020F, e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "txtCustCd_Leave")

        Call Me.ChangeCustCdEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "txtCustCd_Leave")

    End Sub
    'ADD END   2018/11/14 要望番号001939

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================


#End Region 'イベント振分け

#End Region 'Method

End Class