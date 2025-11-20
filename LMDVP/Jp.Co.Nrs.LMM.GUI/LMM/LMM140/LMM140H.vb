' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM140H : ZONEマスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMM140ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM140H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM140V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM140G

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
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' ゾーンマスタ消防情報格納用フィールド
    ''' </summary>
    Private _ZoneShobo As DataTable

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ格納用フィールド
    ''' </summary>
    Private _TouSituZoneChk As DataTable


#End Region
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
        Dim frm As LMM140F = New LMM140F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sFrom, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Gamenクラスの設定
        Me._G = New LMM140G(Me, frm, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM140V(Me, frm, Me._LMMConV)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbNrsBrCd, frm.cmbSokoKb)

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

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        'ステータス設定
        'Me._G.SetModeAndStatus()

        '画面の入力項目ロック
        Call Me._G.LockControl(True)

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM140C.EventShubetsu.MAIN)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent(ByVal frm As LMM140F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM140C.EventShubetsu.SHINKI) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        Call Me._G.ClearControl()

        '各コンボボックスの初期値をセット
        Me._G.SetcmbValue()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM140C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM140F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM140C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtZoneCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM140C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim ds As DataSet = New LMM140DS()
        Call SetDatasetUserItemData(frm, ds)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM140BLF", "HaitaData", ds)

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

            '温度管理区分の値に応じてロック
            Call Me.SetLockControl()

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM140C.EventShubetsu.HENSHU)

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM140F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM140C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtZoneCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM140C.EventShubetsu.HUKUSHA) = False Then
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

        '温度管理区分の値に応じてロック
        Call Me.SetLockControl()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM140C.EventShubetsu.HUKUSHA)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G003")


    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM140F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM140C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtZoneCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM140C.EventShubetsu.SAKUJO) = False Then
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
        Dim ds As DataSet = New LMM140DS()
        Call Me.SetDatasetDelData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM140BLF", "DeleteData", ds)

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
        MyBase.LMCacheMasterData(LMConst.CacheTBL.TOU_SITU_ZONE)

        'メッセージ用請求先コード格納
        Dim touNo As String = frm.txtTouNo.TextValue
        Dim situNo As String = frm.txtSituNo.TextValue
        Dim zoneCd As String = frm.txtZoneCd.TextValue
        'メッセージ
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName _
        '                                      , String.Concat("[", LMM140C.TOU, " = ", touNo, "]" _
        '                                                            , "[", LMM140C.SHITU, " = ", situNo, "]" _
        '                                                            , "[", LMM140C.ZONECD, " = ", zoneCd, "]")})

        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName _
                                              , String.Concat("[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.TOU_NO), " = ", touNo, "]" _
                                                                    , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.SITU_NO), " = ", situNo, "]" _
                                                                    , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.ZONE_CD), " = ", zoneCd, "]")})
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
    Private Sub SelectListEvent(ByVal frm As LMM140F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM140C.EventShubetsu.KENSAKU) = False Then
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
        Call Me._G.SetFoucus(LMM140C.EventShubetsu.KENSAKU)

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM140F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal frm As LMM140F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        ''メッセージ表示(編集モードの場合確認メッセージを表示する)        
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) Then
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
    Private Sub RowSelection(ByVal frm As LMM140F, ByVal rowNo As Integer)

        Dim sokocd As String = String.Empty
        Dim touno As String = String.Empty
        Dim situno As String = String.Empty
        Dim zonecd As String = String.Empty

        Dim dtZoneShobo As DataTable = Me._ZoneShobo
        Dim dtTouSituZoneChk As DataTable = Me._TouSituZoneChk

        sokocd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM140G.sprDetailDef.WH_CD.ColNo).Text()
        touno = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM140G.sprDetailDef.TOU_NO.ColNo).Text()
        situno = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM140G.sprDetailDef.SITU_NO.ColNo).Text()
        zonecd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM140G.sprDetailDef.ZONE_CD.ColNo).Text()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM140C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM140G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(LMM140C.EventShubetsu.SANSHO, frm.lblSituation.RecordStatus)

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'ゾーンマスタ消防情報スプレッドの設定
        Call Me._G.SetSpreadShobo(dtZoneShobo, sokocd, touno, situno, zonecd)
        'スプレッド値セット後にロック
        Call Me._G.SetLockZoneShoboSpreadControl(True)

        '棟室ゾーンチェックマスタスプレッドの設定
        Call Me._G.SetSpreadDoku(dtTouSituZoneChk, sokocd, touno, situno, zonecd)
        Call Me._G.SetSpreadKouathuGas(dtTouSituZoneChk, sokocd, touno, situno, zonecd)
        Call Me._G.SetSpreadYakuziho(dtTouSituZoneChk, sokocd, touno, situno, zonecd)
        'スプレッド値セット後にロック
        Call Me._G.SetLockTouSituZoneChkSpreadControl(True)

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
    Private Sub MasterShowEvent(ByVal frm As LMM140F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM140C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM140C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM140C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        '温度管理区分の値に応じてロック
        Call Me.SetLockControl()

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveCostItemData(ByVal frm As LMM140F, ByVal eventShubetsu As LMM140C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM140C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        '項目チェック
        If Me._V.IsSaveInputChk() = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM140DS()
        Call Me.SetDatasetUserItemData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet = Nothing

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM140BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM140BLF", "UpdateData", ds)
        End Select

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.TOU_SITU_ZONE)

        'メッセージ用請求先コード格納
        Dim touNo As String = frm.txtTouNo.TextValue
        Dim situNo As String = frm.txtSituNo.TextValue
        Dim zoneCd As String = frm.txtZoneCd.TextValue

        'メッセージ表示
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
        '                                              , String.Concat("[", LMM140C.TOU, " = ", touNo, "]" _
        '                                                            , "[", LMM140C.SHITU, " = ", situNo, "]" _
        '                                                            , "[", LMM140C.ZONECD, " = ", zoneCd, "]")})

        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat("[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.TOU_NO), " = ", touNo, "]" _
                                                                            , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.SITU_NO), " = ", situNo, "]" _
                                                                            , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.ZONE_CD), " = ", zoneCd, "]")})
        '2016.01.06 UMANO 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM140C.EventShubetsu.MAIN)

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
    Friend Function CloseFormEvent(ByVal frm As LMM140F, ByVal e As FormClosingEventArgs) As Boolean

        'ディスプレイモードが編集の場合
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            'メッセージ表示
            Select Case MyBase.ShowMessage(frm, "W002")
                Case MsgBoxResult.Yes  '「はい」押下時
                    If Me.SaveCostItemData(frm, LMM140C.EventShubetsu.TOJIRU) = True Then
                        Return True
                    Else '保存失敗時
                        e.Cancel = True
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
    ''' 画面の値によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLockControl()
        Call Me._G.ChangeLockControl(LMM140C.EventShubetsu.VALUECHANGED)
    End Sub


    ''' <summary>
    ''' ダブルクリック時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CellDoubleClick(ByRef frm As LMM140F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)
        If e.Row.Equals(0) = False Then
            Me.sprCellDoubleClick(frm, e)
        End If
    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM140C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM140C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM140C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMMConH.NextFocusedControl(frm, eventFlg)

    End Sub


#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM140F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM140DS()
        Call Me.SetDataSetInData(frm)

        ''SPREAD(表示行)初期化
        'frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM140BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM140BLF", "SelectListData", _FindDs _
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
    Private Sub SuccessSelect(ByVal frm As LMM140F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM140C.TABLE_NM_OUT)
        Dim dt2 As DataTable = ds.Tables(LMM140C.TABLE_NM_ZONE_SHOBO)
        Dim dt3 As DataTable = ds.Tables(LMM140C.TABLE_NM_TOU_SITU_ZONE_CHK)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        'ゾーンマスタ消防情報 初期化
        frm.sprDetailShobo.CrearSpread()

        '棟室ゾーンチェックマスタ情報 初期化
        frm.sprDetailDoku.CrearSpread()
        frm.sprDetailKouathuGas.CrearSpread()
        frm.sprDetailYakuzihoJoho.CrearSpread()

        '取得データ(M_ZONE_SHOBO、M_TOU_SITU_ZONE_CHK)をPrivate変数に保持
        Call Me.SetDataSetZoneShoboData(dt2)
        Call Me.SetDataSetTouSituZoneChkData(dt3)

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
    ''' データセット設定(ゾーンマスタ消防情報格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetZoneShoboData(ByVal dt As DataTable)

        Me._ZoneShobo = dt

    End Sub

    ''' <summary>
    ''' データセット設定(棟室ゾーンチェックマスタ格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetTouSituZoneChkData(ByVal dt As DataTable)

        Me._TouSituZoneChk = dt

    End Sub

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM140F)

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
    Private Sub SetGMessage(ByVal frm As LMM140F)

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
    Private Function ShowPopupControl(ByVal frm As LMM140F, ByVal objNm As String, ByVal eventshubetsu As LMM140C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            Select Case objNm

                Case .txtTouNo.Name, .txtSituNo.Name

                    Call Me.SetReturnTouSituPop(frm, eventshubetsu)

                Case .txtSeiqCd.Name

                    Call Me.SetReturnSeiqtoPop(frm, objNm, eventshubetsu)
            End Select

        End With

        Return True

    End Function


    ''' <summary>
    ''' 請求先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSeiqtoPop(ByVal frm As LMM140F, ByVal objNm As String, ByVal eventshubetsu As LMM140C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowSeiqtoPopup(frm, ctl, eventshubetsu)
        Dim item As String = String.Empty

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)
            ctl.TextValue = dr.Item("SEIQTO_CD").ToString()
            item = dr.Item("SEIQTO_NM").ToString()
            item = String.Concat(item, " ", dr.Item("SEIQTO_BUSYO_NM").ToString())
            frm.lblSeiqNm.TextValue = item
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
    Private Function ShowSeiqtoPopup(ByVal frm As LMM140F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM140C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM140C.EventShubetsu.ENTER Then
                .Item("SEIQTO_CD") = frm.txtSeiqCd.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function
    ''' <summary>
    '''棟室Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTouSituPop(ByVal frm As LMM140F, ByVal eventshubetsu As LMM140C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowTouSituPopup(frm, eventshubetsu)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ050C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtTouNo.TextValue = dr.Item("TOU_NO").ToString()
                .txtSituNo.TextValue = dr.Item("SITU_NO").ToString()
                .lblTouSituNm.TextValue = dr.Item("TOU_SITU_NM").ToString()


            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 棟室マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowTouSituPopup(ByVal frm As LMM140F, ByVal eventshubetsu As LMM140C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ050DS()
        Dim dt As DataTable = ds.Tables(LMZ050C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
        Dim whCd As String = frm.cmbSokoKb.SelectedValue.ToString()
        Dim touNo As String = frm.txtTouNo.TextValue
        Dim situNo As String = frm.txtSituNo.TextValue

        With dr

            .Item("NRS_BR_CD") = brCd
            .Item("WH_CD") = whCd
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM140C.EventShubetsu.ENTER Then
                .Item("TOU_NO") = touNo
                .Item("SITU_NO") = situNo
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ050", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM140F)

        Dim datatable As DataTable = Me._FindDs.Tables(LMM140C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("WH_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.WH_NM.ColNo))
            dr("TOU_NO") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.TOU_NO.ColNo))
            dr("SITU_NO") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.SITU_NO.ColNo))
            dr("ZONE_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.ZONE_CD.ColNo))
            dr("ZONE_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.ZONE_NM.ColNo))
            dr("HOZEI_KB") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.HOZEI_KB_NM.ColNo))
            dr("ONDO_CTL_KB") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.ONDO_CTL_KB_NM.ColNo))
            dr("ONDO_CTL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.ONDO_CTL_FLG_NM.ColNo))
            dr("ONDO") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.ONDO.ColNo))
            dr("YAKUJI_YN") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.YAKUJI_YN_NM.ColNo))
            dr("DOKU_YN") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.DOKU_YN_NM.ColNo))
            dr("GASS_YN") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.GASS_YN_NM.ColNo))
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd().ToString()
            dr("USER_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM140G.sprDetailDef.NRS_BR_CD.ColNo))

            datatable.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUserItemData(ByVal frm As LMM140F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM140C.TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("WH_CD") = .cmbSokoKb.SelectedValue
            dr("TOU_NO") = .txtTouNo.TextValue.Trim
            dr("SITU_NO") = .txtSituNo.TextValue.Trim
            dr("ZONE_CD") = .txtZoneCd.TextValue.Trim
            dr("ZONE_NM") = .txtZoneNm.TextValue.Trim
            dr("ZONE_KB") = .cmbZoneKb.SelectedValue
            dr("HOZEI_KB") = .cmbBondCtlKb.SelectedValue
            dr("ONDO_CTL_KB") = .cmbHeatCtlKb.SelectedValue
            dr("ONDO_CTL_FLG") = .cmbHeatCtl.SelectedValue
            dr("ONDO") = .numSetHeat.Value
            dr("MAX_ONDO_UP") = .numMaxHeatLim.Value
            dr("MINI_ONDO_DOWN") = .numMinHeatLow.Value
            dr("YAKUJI_YN") = .cmbPharKb.SelectedValue
            dr("DOKU_YN") = .cmbPflKb.SelectedValue
            dr("GASS_YN") = .cmbGasCtlKb.SelectedValue
            dr("SEIQTO_CD") = .txtSeiqCd.TextValue.Trim
            dr("TSUBO_AM") = .numRentMonthly.Value

            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd().ToString()
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            ds.Tables(LMM140C.TABLE_NM_IN).Rows.Add(dr)

            'ゾーンマスタ消防Spread情報をデータセットに設定
            Dim sprMax As Integer = .sprDetailShobo.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr2 As DataRow = ds.Tables(LMM140C.TABLE_NM_ZONE_SHOBO).NewRow()

                dr2.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr2.Item("WH_CD") = .cmbSokoKb.SelectedValue
                dr2.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr2.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr2.Item("ZONE_CD") = .txtZoneCd.TextValue.Trim
                dr2.Item("SHOBO_CD") = _LMMConG.GetCellValue(.sprDetailShobo.ActiveSheet.Cells(i, LMM140G.sprDetailDefShobo.SHOBO_CD.ColNo))
                dr2.Item("WH_KYOKA_DATE") = _LMMConG.GetCellValue(.sprDetailShobo.ActiveSheet.Cells(i, LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo))
                dr2.Item("BAISU") = _LMMConG.GetCellValue(.sprDetailShobo.ActiveSheet.Cells(i, LMM140G.sprDetailDefShobo.BAISU.ColNo))
                dr2.Item("UPD_FLG") = _LMMConG.GetCellValue(.sprDetailShobo.ActiveSheet.Cells(i, LMM140G.sprDetailDefShobo.UPD_FLG.ColNo))
                dr2.Item("SYS_DEL_FLG") = _LMMConG.GetCellValue(.sprDetailShobo.ActiveSheet.Cells(i, LMM140G.sprDetailDefShobo.SYS_DEL_FLG_T.ColNo))

                dr2.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM140C.TABLE_NM_ZONE_SHOBO).Rows.Add(dr2)

            Next

            '棟室ゾーンチェックマスタSpread情報をデータセットに設定
            '毒劇情報
            sprMax = .sprDetailDoku.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr4 As DataRow = ds.Tables(LMM140C.TABLE_NM_TOU_SITU_ZONE_CHK).NewRow()

                dr4.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr4.Item("WH_CD") = .cmbSokoKb.SelectedValue
                dr4.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr4.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr4.Item("ZONE_CD") = .txtZoneCd.TextValue.Trim
                dr4.Item("KBN_GROUP_CD") = LMM140C.M_Z_KBN_DOKUGEKI
                dr4.Item("KBN_CD") = _LMMConG.GetCellValue(.sprDetailDoku.ActiveSheet.Cells(i, LMM140G.sprDetailDefDoku.DOKU_KB.ColNo))
                dr4.Item("KBN_NM1") = .sprDetailDoku.ActiveSheet.Cells(i, LMM140G.sprDetailDefDoku.DOKU_KB.ColNo).Text
                dr4.Item("UPD_FLG") = _LMMConG.GetCellValue(.sprDetailDoku.ActiveSheet.Cells(i, LMM140G.sprDetailDefDoku.UPD_FLG.ColNo))
                dr4.Item("SYS_DEL_FLG") = _LMMConG.GetCellValue(.sprDetailDoku.ActiveSheet.Cells(i, LMM140G.sprDetailDefDoku.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr4.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM140C.TABLE_NM_TOU_SITU_ZONE_CHK).Rows.Add(dr4)

            Next

            '高圧ガス情報
            sprMax = .sprDetailKouathuGas.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr5 As DataRow = ds.Tables(LMM140C.TABLE_NM_TOU_SITU_ZONE_CHK).NewRow()

                dr5.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr5.Item("WH_CD") = .cmbSokoKb.SelectedValue
                dr5.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr5.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr5.Item("ZONE_CD") = .txtZoneCd.TextValue.Trim
                dr5.Item("KBN_GROUP_CD") = LMM140C.M_Z_KBN_KOUATHUGAS
                dr5.Item("KBN_CD") = _LMMConG.GetCellValue(.sprDetailKouathuGas.ActiveSheet.Cells(i, LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo))
                dr5.Item("KBN_NM1") = .sprDetailKouathuGas.ActiveSheet.Cells(i, LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo).Text
                dr5.Item("UPD_FLG") = _LMMConG.GetCellValue(.sprDetailKouathuGas.ActiveSheet.Cells(i, LMM140G.sprDetailDefKouathuGas.UPD_FLG.ColNo))
                dr5.Item("SYS_DEL_FLG") = _LMMConG.GetCellValue(.sprDetailKouathuGas.ActiveSheet.Cells(i, LMM140G.sprDetailDefKouathuGas.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr5.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM140C.TABLE_NM_TOU_SITU_ZONE_CHK).Rows.Add(dr5)

            Next

            '薬事法情報
            sprMax = .sprDetailYakuzihoJoho.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr6 As DataRow = ds.Tables(LMM140C.TABLE_NM_TOU_SITU_ZONE_CHK).NewRow()

                dr6.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr6.Item("WH_CD") = .cmbSokoKb.SelectedValue
                dr6.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr6.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr6.Item("ZONE_CD") = .txtZoneCd.TextValue.Trim
                dr6.Item("KBN_GROUP_CD") = LMM140C.M_Z_KBN_YAKUZIHO
                dr6.Item("KBN_CD") = _LMMConG.GetCellValue(.sprDetailYakuzihoJoho.ActiveSheet.Cells(i, LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo))
                dr6.Item("KBN_NM1") = .sprDetailYakuzihoJoho.ActiveSheet.Cells(i, LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo).Text
                dr6.Item("UPD_FLG") = _LMMConG.GetCellValue(.sprDetailYakuzihoJoho.ActiveSheet.Cells(i, LMM140G.sprDetailDefYakuziho.UPD_FLG.ColNo))
                dr6.Item("SYS_DEL_FLG") = _LMMConG.GetCellValue(.sprDetailYakuzihoJoho.ActiveSheet.Cells(i, LMM140G.sprDetailDefYakuziho.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr6.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM140C.TABLE_NM_TOU_SITU_ZONE_CHK).Rows.Add(dr6)

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM140F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM140C.TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("WH_CD") = .cmbSokoKb.SelectedValue
            dr("TOU_NO") = .txtTouNo.TextValue.Trim
            dr("SITU_NO") = .txtSituNo.TextValue.Trim
            dr("ZONE_CD") = .txtZoneCd.TextValue.Trim
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            Dim delflg As String = String.Empty
            If .lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If

            dr("SYS_DEL_FLG") = delflg

        End With

        ds.Tables(LMM140C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMM140C.EventShubetsu, ByVal frm As LMM140F)

        'ディスプレイモード、レコードステータス保存域
        Dim mode As String = String.Empty
        Dim status As String = String.Empty
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim targetSpr As Spread.LMSpread
        Dim tagetDefColNo As Integer
        Dim tagetComboColNo As Integer
        Dim tagetUpdFlgColNo As Integer
        Dim tagetSysDelFlgColNo As Integer

        Select Case eventShubetsu

            Case LMM140C.EventShubetsu.INS_T    '行追加

                '処理開始アクション
                _LMMConH.StartAction(frm)

                '消防マスタ照会POP表示
                Dim ds As DataSet = Me.ShowPopup(frm, LMM140C.EventShubetsu.INS_T.ToString(), prm)
                Dim dt As DataTable = ds.Tables(LMZ280C.TABLE_NM_OUT)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り値が無い場合は終了
                    'メッセージの表示
                    ShowMessage(frm, "G003")
                    '処理終了アクション
                    _LMMConH.EndAction(frm)
                    Exit Sub
                Else
                    '項目チェック
                    If Me._V.IsRowCheck(eventShubetsu, ds, frm) = False Then
                        '処理終了アクション
                        _LMMConH.EndAction(frm)
                        Exit Sub
                    End If
                End If

                '戻り値をゾーンマスタ消防スプレッドに設定
                Call Me._G.AddSetSpreadShobo(dt)

                '処理終了アクション
                _LMMConH.EndAction(frm)

                '画面全ロックの解除
                Me.UnLockedControls(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey()

                'フォーカスの設定
                Call Me._G.SetFoucus(eventShubetsu)

                'メッセージの表示
                ShowMessage(frm, "G003")

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

            Case LMM140C.EventShubetsu.DEL_T    '行削除

                '項目チェック
                If Me._V.IsRowCheck(eventShubetsu, Nothing, frm) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._LMMConH.GetCheckList(frm.sprDetailShobo.ActiveSheet, LMM140G.sprDetailDefShobo.DEF.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _LMMConH.StartAction(frm)

                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelZoneShobo(frm.sprDetailShobo)

                    'ゾーンマスタ消防スプレッドの再描画
                    Me._G.ReSetSpread(frm.sprDetailShobo)

                    '処理終了アクション
                    _LMMConH.EndAction(frm)

                    '画面全ロックの解除
                    Me.UnLockedControls(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey()

                    'フォーカスの設定
                    Call Me._G.SetFoucus(eventShubetsu)

                    'メッセージの表示
                    ShowMessage(frm, "G003")

                    'カーソルを元に戻す
                    Cursor.Current = Cursors.Default()

                End If

            Case LMM140C.EventShubetsu.INS_DOKU,
                 LMM140C.EventShubetsu.INS_KOUATHUGAS,
                 LMM140C.EventShubetsu.INS_YAKUZIHO     '行追加

                Select Case eventShubetsu
                    Case LMM140C.EventShubetsu.INS_DOKU
                        targetSpr = frm.sprDetailDoku
                        tagetDefColNo = LMM140G.sprDetailDefDoku.DEF.ColNo
                    Case LMM140C.EventShubetsu.INS_KOUATHUGAS
                        targetSpr = frm.sprDetailKouathuGas
                        tagetDefColNo = LMM140G.sprDetailDefKouathuGas.DEF.ColNo
                    Case LMM140C.EventShubetsu.INS_YAKUZIHO
                        targetSpr = frm.sprDetailYakuzihoJoho
                        tagetDefColNo = LMM140G.sprDetailDefYakuziho.DEF.ColNo
                End Select

                '処理開始アクション
                _LMMConH.StartAction(frm)

                '項目チェック
                If Me._V.IsTouSituZoneChkRowCheck(eventShubetsu, frm, targetSpr, tagetDefColNo) = False Then
                    '処理終了アクション
                    _LMMConH.EndAction(frm)
                    Exit Sub
                End If

                '棟室ゾーンチェックマスタスプレッドを設定
                Select Case eventShubetsu
                    Case LMM140C.EventShubetsu.INS_DOKU
                        Call Me._G.AddSetSpreadDoku()
                    Case LMM140C.EventShubetsu.INS_KOUATHUGAS
                        Call Me._G.AddSetSpreadKouathuGas()
                    Case LMM140C.EventShubetsu.INS_YAKUZIHO
                        Call Me._G.AddSetSpreadYakuziho()
                End Select

                '処理終了アクション
                _LMMConH.EndAction(frm)

                '画面全ロックの解除
                Me.UnLockedControls(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey()

                'フォーカスの設定
                Call Me._G.SetFoucus(eventShubetsu)

                'メッセージの表示
                ShowMessage(frm, "G003")

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

            Case LMM140C.EventShubetsu.DEL_DOKU,
                 LMM140C.EventShubetsu.DEL_KOUATHUGAS,
                 LMM140C.EventShubetsu.DEL_YAKUZIHO     '行削除

                Select Case eventShubetsu
                    Case LMM140C.EventShubetsu.DEL_DOKU
                        targetSpr = frm.sprDetailDoku
                        tagetDefColNo = LMM140G.sprDetailDefDoku.DEF.ColNo
                        tagetComboColNo = LMM140G.sprDetailDefDoku.DOKU_KB.ColNo
                        tagetUpdFlgColNo = LMM140G.sprDetailDefDoku.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM140G.sprDetailDefDoku.SYS_DEL_FLG_T.ColNo
                    Case LMM140C.EventShubetsu.DEL_KOUATHUGAS
                        targetSpr = frm.sprDetailKouathuGas
                        tagetDefColNo = LMM140G.sprDetailDefKouathuGas.DEF.ColNo
                        tagetComboColNo = LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo
                        tagetUpdFlgColNo = LMM140G.sprDetailDefKouathuGas.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM140G.sprDetailDefKouathuGas.SYS_DEL_FLG_T.ColNo
                    Case LMM140C.EventShubetsu.DEL_YAKUZIHO
                        targetSpr = frm.sprDetailYakuzihoJoho
                        tagetDefColNo = LMM140G.sprDetailDefYakuziho.DEF.ColNo
                        tagetComboColNo = LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo
                        tagetUpdFlgColNo = LMM140G.sprDetailDefYakuziho.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM140G.sprDetailDefYakuziho.SYS_DEL_FLG_T.ColNo
                End Select

                '項目チェック
                If Me._V.IsTouSituZoneChkRowCheck(eventShubetsu, frm, targetSpr, tagetDefColNo) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._LMMConH.GetCheckList(targetSpr.ActiveSheet, tagetDefColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _LMMConH.StartAction(frm)

                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelTouSituZoneChk(targetSpr, tagetDefColNo, tagetUpdFlgColNo, tagetSysDelFlgColNo)

                    '棟室ゾーンチェックマスタスプレッドの再描画
                    Me._G.ReSetTouSituZoneChkSpread(targetSpr, tagetDefColNo, tagetComboColNo, tagetUpdFlgColNo, tagetSysDelFlgColNo)

                    '処理終了アクション
                    _LMMConH.EndAction(frm)

                    '画面全ロックの解除
                    Me.UnLockedControls(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey()

                    'フォーカスの設定
                    Call Me._G.SetFoucus(eventShubetsu)

                    'メッセージの表示
                    ShowMessage(frm, "G003")

                    'カーソルを元に戻す
                    Cursor.Current = Cursors.Default()

                End If
        End Select

    End Sub

#End Region

#End Region

#Region "PopUp"

    ''' <summary>
    ''' POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LMM140F, ByVal objNM As String, ByVal prm As LMFormData) As DataSet

        Dim value As String = String.Empty

        Select Case objNM

            Case LMM140C.EventShubetsu.INS_T.ToString()         '行追加

                Dim prmDs As DataSet = New LMZ280DS()
                Dim row As DataRow = prmDs.Tables(LMZ280C.TABLE_NM_IN).NewRow
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("SELECT_PLURAL_FLG") = LMConst.FLG.ON   '複数選択可
                prmDs.Tables(LMZ280C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ280", prm)

        End Select

        Return prm.ParamDataSet

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し(複写)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CopyDataEvent")

        Me.CopyDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CopyDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し(削除)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveCostItemData")

        Me.SaveCostItemData(frm, LMM140C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveCostItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByRef frm As LMM140F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM140F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

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
    Friend Sub LMM140F_KeyDown(ByVal frm As LMM140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM140F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM140F_KeyDown")

    End Sub
    ''' <summary>
    ''' 温度管理区分の値変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbHeatCtlKb_SelectedValueChanged(ByVal frm As LMM140F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbHeatCtlKb_SelectedValueChanged")

        Call Me.SetLockControl()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbHeatCtlKb_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM140F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    ''' <summary>
    ''' ゾーンマスタ消防情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_SHOBO_Click(ByRef frm As LMM140F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_SHOBO_Click")

        '「行追加」処理
        Me.ActionControl(LMM140C.EventShubetsu.INS_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_SHOBO_Click")

    End Sub

    ''' <summary>
    ''' 毒劇情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_DOKU_Click(ByRef frm As LMM140F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_DOKU_Click")

        '「行追加」処理
        Me.ActionControl(LMM140C.EventShubetsu.INS_DOKU, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_DOKU_Click")

    End Sub

    ''' <summary>
    ''' 高圧ガス情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_KOUATHUGAS_Click(ByRef frm As LMM140F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_KOUATHUGAS_Click")

        '「行追加」処理
        Me.ActionControl(LMM140C.EventShubetsu.INS_KOUATHUGAS, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_KOUATHUGAS_Click")

    End Sub

    ''' <summary>
    ''' 薬事法情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_YAKUZIHO_Click(ByRef frm As LMM140F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_YAKUZIHO_Click")

        '「行追加」処理
        Me.ActionControl(LMM140C.EventShubetsu.INS_YAKUZIHO, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_YAKUZIHO_Click")

    End Sub

    ''' <summary>
    ''' ゾーンマスタ消防行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_SHOBO_Click(ByRef frm As LMM140F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_SHOBO_Click")

        '「行削除」処理
        Me.ActionControl(LMM140C.EventShubetsu.DEL_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_SHOBO_Click")

    End Sub

    ''' <summary>
    ''' 毒劇情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_DOKU_Click(ByRef frm As LMM140F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_DOKU_Click")

        '「行削除」処理
        Me.ActionControl(LMM140C.EventShubetsu.DEL_DOKU, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_DOKU_Click")

    End Sub

    ''' <summary>
    ''' 高圧ガス情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_KOUATHUGAS_Click(ByRef frm As LMM140F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_KOUATHUGAS_Click")

        '「行削除」処理
        Me.ActionControl(LMM140C.EventShubetsu.DEL_KOUATHUGAS, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_KOUATHUGAS_Click")

    End Sub

    ''' <summary>
    ''' 薬事法情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_YAKUZIHO_Click(ByRef frm As LMM140F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_YAKUJZHO_Click")

        '「行削除」処理
        Me.ActionControl(LMM140C.EventShubetsu.DEL_YAKUZIHO, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_YAKZJIHO_Click")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class