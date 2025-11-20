' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM010H : ユーザーマスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMM010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private _V As LMM010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM010G

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
    '''担当者別荷主情報格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _TcustDs As DataTable

    '要望番号:1248 yamanaka 2013.03.21 Start
    ''' <summary>
    '''担当者別荷主情報格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _TunsocoDs As DataTable

    ''' <summary>
    '''担当者別荷主情報格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _TtariffDs As DataTable
    '要望番号:1248 yamanaka 2013.03.21 End

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Ds As DataSet

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
        Dim frm As LMM010F = New LMM010F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sFrom, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Gamenクラスの設定
        Me._G = New LMM010G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMM010V(Me, frm, Me._LMMConV)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbNrsBrCd, frm.cmbWare)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '2011/08/11 福田 共通動作(右セル移動不可) スタート
        'Enter押下イベントの設定
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

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM010C.EventShubetsu.MAIN)

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
    Private Sub NewDataEvent(ByVal frm As LMM010F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM010C.EventShubetsu.SHINKI) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(MAXユーザーコード枝番のクリア)
        If Me._Ds Is Nothing Then
            Me._Ds = New LMM010DS()
        End If
        Me._Ds.Clear()

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面全ロックの解除
        Me.UnLockedControls(frm)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '画面入力項目のクリア
        Call Me._G.ClearControl(frm)

        'コンボボックスの値の設定
        Me._G.SetcmbValue()
        'ADD 2022/02/26  027945
        Me._G.CreateComboBoxPrt(DispMode.INIT)

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM010C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM010F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM010C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtUserId.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM010C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim ds As DataSet = New LMM010DS()
        Call SetDatasetUserItemData(frm, ds)
        Me._Ds = New LMM010DS()

        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMM010BLF", "HaitaData", ds)

        'データセットの内容保持
        _Ds = ds

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

            ''コンボ再設定 ADD 2022/02/25 027945
            'Call Me._G.CreateComboBoxPrt(DispMode.EDIT, sKBN_CD)

            '画面全ロックの解除
            Me.UnLockedControls(frm)

            '画面の入力項目/ファンクションキーの制御
            Call Me._G.UnLockedForm()

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM010C.EventShubetsu.HENSHU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM010F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM010C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtUserId.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM010C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(MAXユーザーコード枝番のクリア)
        If Me._Ds Is Nothing Then
            Me._Ds = New LMM010DS()
        End If
        Me._Ds.Clear()

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'MAXユーザーコード枝番をデータセットに設定
        Dim ds As DataSet = New LMM010DS()
        Dim row As DataRow = ds.Tables(LMM010C.TABLE_NM_TCUST_MAXEDA).NewRow
        Dim maxRowCnt As Integer = frm.sprDetail2.ActiveSheet.Rows.Count - 1
        If -1 < maxRowCnt Then
            row("USER_CD_MAXEDA") = Me._LMMConV.GetCellValue(frm.sprDetail2.ActiveSheet.Cells(maxRowCnt, LMM010G.sprDetailDef2.USER_CD_EDA.ColNo))
            ds.Tables(LMM010C.TABLE_NM_TCUST_MAXEDA).Rows.Add(row)
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM010C.EventShubetsu.HUKUSHA)

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
    Private Sub DeleteDataEvent(ByVal frm As LMM010F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM010C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtUserId.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM010C.EventShubetsu.SAKUJO) = False Then
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
        Dim ds As DataSet = New LMM010DS()
        Call Me.SetDatasetDelData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM010BLF", "DeleteData", ds)

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
        MyBase.LMCacheMasterData(LMConst.CacheTBL.USER)
        MyBase.LMCacheMasterData(LMConst.CacheTBL.TCUST)

        'メッセージ用請求先コード格納
        Dim userCd As String = frm.txtUserId.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty), String.Concat("[", frm.lblUserId.Text, " = ", frm.txtUserId.TextValue, "]")})

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM010F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM010C.EventShubetsu.KENSAKU) = False Then
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
                'メッセージ設定
                Call Me.SetGMessage(frm)
                Exit Sub
            Else      'OK押下
                clearFlg = 1
            End If
        End If

        '検索処理を行う
        Call Me.SelectData(frm, clearFlg)

        'コンボ再設定 ADD 2022/02/24 027945 【LMS】ユーザマスタメンテ　デフォルトプリンタ絞込み
        Me._G.CreateComboBoxPrt()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM010C.EventShubetsu.KENSAKU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()
    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM010F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    Private Sub SprCellSelect(ByVal frm As LMM010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim userid As String = String.Empty
        userid = frm.sprDetail.ActiveSheet.Cells(e.Row, LMM010G.sprDetailDef.USER_ID.ColNo).Text()

        Dim dt2 As DataTable = Me._TcustDs

        ''メッセージ表示(編集モードの場合確認メッセージを表示する)
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(LMM010C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(e.Row, LMM010G.sprDetailDef.SYS_DEL_FLG.ColNo)).Equals(LMConst.FLG.OFF) Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(e.Row)

        'SPREAD(明細)初期化
        frm.sprDetail2.CrearSpread()

        'SPREAD(明細)へデータを移動
        Call Me._G.SetSpread2(dt2, userid)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")
    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMM010F, ByVal rowNo As Integer)

        Dim userid As String = String.Empty
        userid = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM010G.sprDetailDef.USER_ID.ColNo).Text()

        Dim dt2 As DataTable = Me._TcustDs

        '要望番号:1248 yamanaka 2013.03.21 Start
        Dim dtTunsoco As DataTable = Me._TunsocoDs
        Dim dtTtariff As DataTable = Me._TtariffDs
        '要望番号:1248 yamanaka 2013.03.21 End

        '権限チェック
        If Me._V.IsAuthorityChk(LMM010C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM010G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'SPREAD(明細)初期化
        frm.sprDetail2.CrearSpread()

        '要望番号:1248 yamanaka 2013.03.21 Start
        frm.sprMyUnsoco.CrearSpread()
        frm.sprMyTariff.CrearSpread()
        '要望番号:1248 yamanaka 2013.03.21 End

        'SPREAD(明細)へデータを移動
        Call Me._G.SetSpread2(dt2, userid)

        '要望番号:1248 yamanaka 2013.03.21 Start
        'SPREAD(My運送会社)へデータを移動
        Call Me._G.SetMyUnsocoSpread(dtTunsoco, userid)

        'SPREAD(My運賃タリフ)へデータを移動
        Call Me._G.SetMyTariffSpread(dtTtariff, userid)
        '要望番号:1248 yamanaka 2013.03.21 End

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveUserItemData(ByVal frm As LMM010F, ByVal eventShubetsu As LMM010C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM010C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        '項目チェック
        If Me._V.IsSaveInputChk() = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM010DS()
        Call Me.SetDatasetUserItemData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM010BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM010BLF", "UpdateData", ds)
        End Select

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.USER)
        MyBase.LMCacheMasterData(LMConst.CacheTBL.TCUST)

        '処理結果メッセージ表示
        Dim userCd As String = frm.txtUserId.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), String.Concat("[", frm.lblUserId.Text, " = ", frm.txtUserId.TextValue, "]")})

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM010C.EventShubetsu.MAIN)

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
    Friend Sub CloseFormEvent(ByVal frm As LMM010F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveUserItemData(frm, LMM010C.EventShubetsu.TOJIRU) = False Then

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
    Private Sub EnterAction(ByVal frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM010C.EventShubetsu.ENTER)

        ''カーソル位置チェック
        'rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM010C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        ''Pop起動処理
        'Call Me.ShowPopupControl(frm, objNm, LMM010C.EventShubetsu.ENTER)

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
    Private Sub SelectData(ByVal frm As LMM010F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM010DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM010BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM010BLF", "SelectListData", _FindDs _
        '                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
        '                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
        '                                   , , Convert.ToInt32(Convert.ToDouble( _
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
        If clearflg <> 1 Then
        Else
            '画面の入力項目/ファンクションキーの制御
            Call Me._G.UnLockedForm()
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
    Private Sub SuccessSelect(ByVal frm As LMM010F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM010C.TABLE_NM_OUT)
        Dim dt2 As DataTable = ds.Tables(LMControlC.LMM010C_TABLE_NM_OUT2)

        '要望番号:1248 yamanaka 2013.03.21 Start
        Dim dtTunsoco As DataTable = ds.Tables(LMM010C.TABLE_NM_OUT_TUNSOCO)
        Dim dtTtariff As DataTable = ds.Tables(LMM010C.TABLE_NM_OUT_TTARIFF)
        '要望番号:1248 yamanaka 2013.03.21 End

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行・明細)初期化
        frm.sprDetail.CrearSpread()
        frm.sprDetail2.CrearSpread()

        '要望番号:1248 yamanaka 2013.03.21 Start
        frm.sprMyUnsoco.CrearSpread()
        frm.sprMyTariff.CrearSpread()
        '要望番号:1248 yamanaka 2013.03.21 End

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '要望番号:1248 yamanaka 2013.03.21 Start
        '取得データ(TCUST)をPrivate変数に保持
        Call Me.SetDataSetTcustData(dt2, dtTunsoco, dtTtariff)
        'Call Me.SetDataSetTcustData(dt2)
        '要望番号:1248 yamanaka 2013.03.21 End

        Me._CntSelect = dt.Rows.Count.ToString()

        ''メッセージエリアの設定
        'If (LMConst.FLG.OFF).Equals(Me._CntSelect) = True Then
        '    MyBase.ShowMessage(frm, "G001")
        'Else
        '    MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        'End If

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

    End Sub

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM010F)

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
    Private Sub SetGMessage(ByVal frm As LMM010F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMM010C.EventShubetsu, ByVal frm As LMM010F)

        'ディスプレイモード、レコードステータス保存域
        Dim mode As String = String.Empty
        Dim status As String = String.Empty

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        '要望番号:1248 yamanaka 2013.03.19 Start
        'タブのページ名を取得
        Dim pageNm As String = frm.tabMyData.SelectedTab.Name
        '要望番号:1248 yamanaka 2013.03.19 End

        Select Case eventShubetsu

            Case LMM010C.EventShubetsu.INS_T    '行追加

                '処理開始アクション
                _LMMConH.StartAction(frm)

                'ユーザーコード枝番の採番
                If Me._G.SetUserCdEdaDataSet(pageNm) = False Then
                    '処理終了アクション
                    _LMMConH.EndAction(frm)
                    Exit Sub
                End If

                '要望番号:1248 yamanaka 2013.03.19 Start
                Dim ds As DataSet = New DataSet
                Dim dt As DataTable = Nothing

                Select Case pageNm

                    Case frm.tpgMyCust.Name

                        '荷主マスタ照会POP表示
                        ds = Me.ShowPopup(frm, pageNm, prm)
                        dt = ds.Tables(LMZ260C.TABLE_NM_OUT)

                    Case frm.tpgMyUnsoco.Name

                        '運送会社マスタ照会POP表示
                        ds = Me.ShowPopup(frm, pageNm, prm)
                        dt = ds.Tables(LMZ250C.TABLE_NM_OUT)

                    Case frm.tpgMyTariff.Name

                        '運賃タリフマスタ照会POP表示
                        ds = Me.ShowPopup(frm, pageNm, prm)
                        dt = ds.Tables(LMZ230C.TABLE_NM_OUT)

                End Select

                '行数設定
                If dt.Rows.Count = 0 Then

                    '処理終了アクション
                    Me._LMMConH.EndAction(frm)

                    'メッセージの表示
                    MyBase.ShowMessage(frm, "G003")

                    Exit Sub

                Else

                    If Me._V.IsRowCheck(eventShubetsu, ds, pageNm) = False Then
                        '処理終了アクション
                        _LMMConH.EndAction(frm)
                        Exit Sub
                    End If

                End If

                '戻り値をスプレッドに設定
                Select Case pageNm

                    Case frm.tpgMyCust.Name

                        Call Me._G.AddSetSpread2(dt)

                    Case frm.tpgMyUnsoco.Name

                        Call Me._G.AddSetMyUnsoco(dt)

                    Case frm.tpgMyTariff.Name

                        Call Me._G.AddSetMyTariff(dt)

                End Select

                ''荷主マスタ照会POP表示
                'Dim ds As DataSet = Me.ShowPopup(frm, LMM010C.EventShubetsu.INS_T.ToString(), prm)
                'Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_OUT)

                ''行数設定
                'If prm.ReturnFlg = False Then
                '    '戻り値が無い場合は終了
                '    'メッセージの表示
                '    ShowMessage(frm, "G003")
                '    '処理終了アクション
                '    _LMMConH.EndAction(frm)
                '    Exit Sub
                'Else
                '    '項目チェック
                '    If Me._V.IsRowCheck(eventShubetsu, ds, frm) = False Then
                '        '処理終了アクション
                '        _LMMConH.EndAction(frm)
                '        Exit Sub
                '    End If
                'End If

                ''戻り値をデフォルト荷主スプレッドに設定
                'Call Me._G.AddSetSpread2(dt)
                '要望番号:1248 yamanaka 2013.03.19 End

                '処理終了アクション
                _LMMConH.EndAction(frm)

                '画面全ロックの解除
                Me.UnLockedControls(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey()

                'フォーカスの設定
                Call Me._G.SetFoucus(LMM010C.EventShubetsu.INS_T)

                'メッセージの表示
                ShowMessage(frm, "G003")

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

            Case LMM010C.EventShubetsu.DEL_T    '行削除

                '要望番号:1248 yamanaka 2013.03.19 Start
                '項目チェック
                If Me._V.IsRowCheck(eventShubetsu, Nothing, pageNm) = False Then
                    Exit Sub
                End If

                'If Me._V.IsRowCheck(eventShubetsu, Nothing) = False Then
                '    Exit Sub
                'End If

                '選択されたページのスプレッドを格納
                Dim spr As LMSpread = Nothing
                Dim sprDefNo As SpreadColProperty = Nothing

                Select Case pageNm

                    Case frm.tpgMyCust.Name

                        '初期荷主
                        spr = frm.sprDetail2
                        sprDefNo = LMM010G.sprDetailDef2.DEF

                    Case frm.tpgMyUnsoco.Name

                        'My運送会社
                        spr = frm.sprMyUnsoco
                        sprDefNo = LMM010G.sprMyUnsocoDef.DEF

                    Case frm.tpgMyTariff.Name

                        'My運賃タリフ
                        spr = frm.sprMyTariff
                        sprDefNo = LMM010G.sprMyTariffDef.DEF

                End Select

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._LMMConH.GetCheckList(spr.ActiveSheet, sprDefNo.ColNo)
                'arr = Me._LMMConH.GetCheckList(frm.sprDetail2.ActiveSheet, LMM010G.sprDetailDef2.DEF.ColNo)
                '要望番号:1248 yamanaka 2013.03.19 End

                If 0 < arr.Count Then

                    '処理開始アクション
                    _LMMConH.StartAction(frm)

                    '要望番号:1248 yamanaka 2013.03.19 Start
                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelTcust(spr, arr)
                    'Call Me._G.DelTcust(frm.sprDetail2)

                    'スプレッドの再描画
                    'Me._G.ReSetSpread(frm.sprDetail2)
                    '要望番号:1248 yamanaka 2013.03.19 Start

                    '処理終了アクション
                    _LMMConH.EndAction(frm)

                    '画面全ロックの解除
                    Me.UnLockedControls(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey()

                    'フォーカスの設定
                    Call Me._G.SetFoucus(LMM010C.EventShubetsu.DEL_T)

                    'メッセージの表示
                    ShowMessage(frm, "G003")

                    'カーソルを元に戻す
                    Cursor.Current = Cursors.Default()

                End If

            Case LMM010C.EventShubetsu.DEF_T    '初期荷主

                '項目チェック
                If Me._V.IsRowCheck(eventShubetsu, Nothing) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._LMMConH.GetCheckList(frm.sprDetail2.ActiveSheet, LMM010G.sprDetailDef2.DEF.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _LMMConH.StartAction(frm)

                    'スプレッドの初期荷主フラグ設定処理
                    Call Me._G.DefTcust(frm.sprDetail2)

                    'デフォルト荷主スプレッドの再描画
                    Me._G.ReSetSpread(frm.sprDetail2)

                    '処理終了アクション
                    _LMMConH.EndAction(frm)

                    '画面全ロックの解除
                    Me.UnLockedControls(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey()

                    'フォーカスの設定
                    Call Me._G.SetFoucus(LMM010C.EventShubetsu.DEL_T)

                    'メッセージの表示
                    ShowMessage(frm, "G003")

                    'カーソルを元に戻す
                    Cursor.Current = Cursors.Default()

                End If

        End Select

    End Sub

#End Region

#Region "PopUp"

    ''' <summary>
    ''' POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LMM010F, ByVal pageNm As String, ByVal prm As LMFormData) As DataSet

        Dim value As String = String.Empty

        '要望番号:1248 yamanaka 2013.03.19 Start
        Dim prmDs As DataSet = New DataSet
        Dim row As DataRow = Nothing
        Dim popNm As String = String.Empty

        Select Case pageNm

            Case frm.tpgMyCust.Name

                '荷主マスタ
                prm.ParamDataSet = Me.ShowCustPopup()
                popNm = "LMZ260"

            Case frm.tpgMyUnsoco.Name

                '運送会社マスタ
                prm.ParamDataSet = Me.ShowUnsocoPop()
                popNm = "LMZ250"

            Case frm.tpgMyTariff.Name

                '運賃タリフマスタ
                prm.ParamDataSet = Me.ShowTariffPopup()
                popNm = "LMZ230"

        End Select

        'POP呼出
        LMFormNavigate.NextFormNavigate(Me, popNm, prm)

        'Select Case objNM


        '    Case LMM010C.EventShubetsu.INS_T.ToString()         '行追加

        '        Dim prmDs As DataSet = New LMZ260DS()
        '        Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
        '        row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
        '        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.OFF
        '        row("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)
        '        prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
        '        prm.ParamDataSet = prmDs

        '        'POP呼出
        '        LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

        'End Select
        '要望番号:1248 yamanaka 2013.03.19 End

        Return prm.ParamDataSet

    End Function

    '要望番号:1248 yamanaka 2013.03.19 Start
    ''' <summary>
    ''' 荷主マスタ参照用DataSet作成
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ShowCustPopup() As DataSet

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            .Item("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S

        End With

        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照用DataSet作成
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ShowUnsocoPop() As DataSet

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            .Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' タリフマスタ参照用DataSet作成
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ShowTariffPopup() As DataSet

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            .Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        dt.Rows.Add(dr)

        Return ds

    End Function
    '要望番号:1248 yamanaka 2013.03.19 End

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM010F)

        Dim dr As DataRow = Me._FindDs.Tables(LMControlC.LMM010C_TABLE_NM_IN).NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM010G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM010G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("USER_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM010G.sprDetailDef.USER_ID.ColNo))
            dr("USER_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM010G.sprDetailDef.USER_NM.ColNo))
            dr("AUTHO_LV") = Me._LMMConV.GetCellValue(.Cells(0, LMM010G.sprDetailDef.AUTHO_LV_NM.ColNo))
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM010G.sprDetailDef.NRS_BR_CD.ColNo))

            Me._FindDs.Tables(LMControlC.LMM010C_TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUserItemData(ByVal frm As LMM010F, ByVal ds As DataSet)

        With frm

            '編集部の値（ユーザー情報）をデータセットに設定
            Dim dr As DataRow = ds.Tables(LMControlC.LMM010C_TABLE_NM_IN).NewRow()

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("USER_CD") = .txtUserId.TextValue.Trim
            dr("USER_NM") = .txtUserNm.TextValue.Trim
            dr("AUTHO_LV") = .cmbAuthoLv.SelectedValue
            dr("PW") = .txtPw.TextValue.Trim
            dr("EMAIL") = .txtEMail.TextValue.Trim
            If .chkNoticeYn.Checked Then
                dr("NOTICE_YN") = "01"
            Else
                dr("NOTICE_YN") = "00"
            End If
            dr("RIYOUSHA_KBN") = .cmbRiyoushaKbn.SelectedValue
            dr("WH_CD") = .cmbWare.SelectedValue
            dr("INKA_DATE_INIT") = .cmbInkaDateInit.SelectedValue
            dr("OUTKA_DATE_INIT") = .cmbOutkaDateInit.SelectedValue
            dr("DEF_PRT1") = .cmbDefPrt1.SelectedValue
            dr("DEF_PRT2") = .cmbDefPrt2.SelectedValue
            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            dr("DEF_PRT3") = .cmbDefPrt3.SelectedValue
            dr("DEF_PRT4") = .cmbDefPrt4.SelectedValue
            dr("DEF_PRT5") = .cmbDefPrt5.SelectedValue
            dr("DEF_PRT6") = .cmbDefPrt6.SelectedValue
            dr("DEF_PRT7") = .cmbDefPrt7.SelectedValue
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
            dr("DEF_PRT8") = .cmbDefPrt8.SelectedValue
            dr("COA_PRT") = .cmbCoaPrt.SelectedText
            dr("BUSYO_CD") = .cmbBusyoCd.SelectedValue
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            dr("YELLOW_CARD_PRT") = .cmbYellowCardPrt.SelectedText
            dr("SAP_LINK_AUTHO") = .cmbSapLinkAutho.SelectedValue

            ds.Tables(LMControlC.LMM010C_TABLE_NM_IN).Rows.Add(dr)

            'デフォルト荷主Spread情報をデータセットに設定
            Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr2 As DataRow = ds.Tables(LMControlC.LMM010C_TABLE_NM_OUT2).NewRow()

                dr2("USER_CD") = .txtUserId.TextValue.Trim
                dr2("USER_CD_EDA") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010G.sprDetailDef2.USER_CD_EDA.ColNo))
                dr2("CUST_CD_L") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010G.sprDetailDef2.CUST_CD_L.ColNo))
                dr2("CUST_CD_M") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010G.sprDetailDef2.CUST_CD_M.ColNo))
                dr2("DEFAULT_CUST_YN") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010G.sprDetailDef2.DEFAULT_CUST_YN.ColNo))
                dr2("UPD_FLG") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010G.sprDetailDef2.UPD_FLG.ColNo))
                dr2("SYS_DEL_FLG") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010G.sprDetailDef2.SYS_DEL_FLG_T.ColNo))

                ds.Tables(LMControlC.LMM010C_TABLE_NM_OUT2).Rows.Add(dr2)

            Next

            '要望番号:1248 yamanaka 2013.03.22 Start
            'My運送会社Spread情報をデータセットに設定
            ds = Me.SetDataSetTunsoco(frm, ds)

            'My運賃タリフSpread情報をデータセットに設定
            ds = Me.SetDataSetTtariff(frm, ds)
            '要望番号:1248 yamanaka 2013.03.22 End

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM010F, ByVal ds As DataSet)
        Dim dr As DataRow = ds.Tables(LMControlC.LMM010C_TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("USER_CD") = .txtUserId.TextValue.Trim
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

        ds.Tables(LMControlC.LMM010C_TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(担当者別荷主情報格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetTcustData(ByVal dt As DataTable, ByVal dtTunsoco As DataTable, ByVal dtTtariff As DataTable)

        Me._TcustDs = dt

        '要望番号:1248 yamanaka 2013.03.21 Start
        Me._TunsocoDs = dtTunsoco
        Me._TtariffDs = dtTtariff
        '要望番号:1248 yamanaka 2013.03.21 End

    End Sub

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' データセット設定(My運送会社)
    ''' </summary>    
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Function SetDataSetTunsoco(ByVal frm As LMM010F, ByVal ds As DataSet) As DataSet

        With frm

            Dim max As Integer = .sprMyUnsoco.ActiveSheet.Rows.Count - 1

            For i As Integer = 0 To max

                Dim dr As DataRow = ds.Tables(LMM010C.TABLE_NM_OUT_TUNSOCO).NewRow()

                dr("USER_CD") = .txtUserId.TextValue.Trim
                dr("USER_CD_EDA") = _LMMConG.GetCellValue(.sprMyUnsoco.ActiveSheet.Cells(i, LMM010G.sprMyUnsocoDef.USER_CD_EDA.ColNo))
                dr("UNSOCO_CD") = _LMMConG.GetCellValue(.sprMyUnsoco.ActiveSheet.Cells(i, LMM010G.sprMyUnsocoDef.UNSOCO_CD.ColNo))
                dr("UNSOCO_BR_CD") = _LMMConG.GetCellValue(.sprMyUnsoco.ActiveSheet.Cells(i, LMM010G.sprMyUnsocoDef.UNSOCO_BR_CD.ColNo))
                dr("UPD_FLG") = _LMMConG.GetCellValue(.sprMyUnsoco.ActiveSheet.Cells(i, LMM010G.sprMyUnsocoDef.UPD_FLG.ColNo))
                dr("SYS_DEL_FLG") = _LMMConG.GetCellValue(.sprMyUnsoco.ActiveSheet.Cells(i, LMM010G.sprMyUnsocoDef.SYS_DEL_FLG_T.ColNo))

                ds.Tables(LMM010C.TABLE_NM_OUT_TUNSOCO).Rows.Add(dr)

            Next

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(My運賃タリフ)
    ''' </summary>    
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Function SetDataSetTtariff(ByVal frm As LMM010F, ByVal ds As DataSet) As DataSet

        With frm

            Dim max As Integer = .sprMyTariff.ActiveSheet.Rows.Count - 1

            For i As Integer = 0 To max

                Dim dr As DataRow = ds.Tables(LMM010C.TABLE_NM_OUT_TTARIFF).NewRow()

                dr("USER_CD") = .txtUserId.TextValue.Trim
                dr("USER_CD_EDA") = _LMMConG.GetCellValue(.sprMyTariff.ActiveSheet.Cells(i, LMM010G.sprMyTariffDef.USER_CD_EDA.ColNo))
                dr("UNCHIN_TARIFF_CD") = _LMMConG.GetCellValue(.sprMyTariff.ActiveSheet.Cells(i, LMM010G.sprMyTariffDef.UNCHIN_TARIFF.ColNo))
                dr("UPD_FLG") = _LMMConG.GetCellValue(.sprMyTariff.ActiveSheet.Cells(i, LMM010G.sprMyTariffDef.UPD_FLG.ColNo))
                dr("SYS_DEL_FLG") = _LMMConG.GetCellValue(.sprMyTariff.ActiveSheet.Cells(i, LMM010G.sprMyTariffDef.SYS_DEL_FLG_T.ColNo))

                ds.Tables(LMM010C.TABLE_NM_OUT_TTARIFF).Rows.Add(dr)

            Next

            Return ds

        End With

    End Function
    '要望番号:1248 yamanaka 2013.03.22 End

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey3Press(ByRef frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey4Press(ByRef frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey9Press(ByRef frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    '''' <summary>
    '''' F10押下時処理呼び出し(マスタ参照処理)
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <param name="e">ファンクションキーイベント</param>
    '''' <remarks></remarks>
    'Friend Sub FunctionKey10Press(ByRef frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

    '    MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

    '    Me.MasterShowEvent(frm)

    '    MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    'End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveUserItemData")

        Me.SaveUserItemData(frm, LMM010C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveUserItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM010F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_TCUST_Click(ByRef frm As LMM010F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_TCUST_Click")

        '「行追加」処理
        Me.ActionControl(LMM010C.EventShubetsu.INS_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_TCUST_Click")

    End Sub

    ''' <summary>
    ''' 行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_TCUST_Click(ByRef frm As LMM010F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_TCUST_Click")

        '「行削除」処理
        Me.ActionControl(LMM010C.EventShubetsu.DEL_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_TCUST_Click")

    End Sub

    ''' <summary>
    ''' 初期荷主 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnDEFAULT_CUST_Click(ByRef frm As LMM010F)

        Logger.StartLog(Me.GetType.Name, "btnDEFAULT_CUST_Click")

        '「初期荷主」処理
        Me.ActionControl(LMM010C.EventShubetsu.DEF_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnDEFAULT_CUST_Click")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM010F_KeyDown(ByVal frm As LMM010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM010F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM010F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM010F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class