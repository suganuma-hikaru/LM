' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM230H : エリアマスタメンテ
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
''' LMM230ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM230H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM230V
    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM230G

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
    '''JIS情報格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _JisDt As DataTable

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Ds As DataSet

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
        Dim frm As LMM230F = New LMM230F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sForm, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM230V(Me, frm, Me._LMMConV)

        'Gamenクラスの設定
        Me._G = New LMM230G(Me, frm, Me._LMMConG)

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

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM230C.EventShubetsu.MAIN)

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
    Private Sub NewDataEvent(ByVal frm As LMM230F)

        '保存用データセットインスタンス作成
        Me._Ds = New LMM230DS()

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM230C.EventShubetsu.SHINKI) = False Then
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

        '画面入力項目のクリア
        Call Me._LMMConG.ClearControl(frm)

        'コンボボックスの値の設定
        Call Me._G.SetcmbValue()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM230C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM230F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM230C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '画面のエリアコード
        Dim areaCd As String = frm.txtAreaCd.TextValue

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        '画面の便区分
        Dim binKb As String = frm.cmbBin.SelectedValue.ToString
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, areaCd) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk() = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(LMM230C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Me._Ds = New LMM230DS()

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Dim drs As DataRow() = Me._JisDt.Select(String.Concat("AREA_CD = ", "'", areaCd, "'"))
        Dim drs As DataRow() = Me._JisDt.Select(Me._G.SetJisSql(areaCd, binKb))
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


        Dim cnt As Integer = drs.Length - 1
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To cnt
            dr = drs(i)
            Me._Ds.Tables(LMM230C.TABLE_NM_JIS).ImportRow(dr)
        Next

        Call SetDatasetItemData(frm, Me._Ds)

        Dim rtnDs As DataSet

        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = MyBase.CallWSA("LMM230BLF", "HaitaData", Me._Ds)

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
        Call Me._G.SetFoucus(LMM230C.EventShubetsu.HENSHU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM230F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM230C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '画面のエリアコード
        Dim areaCd As String = frm.txtAreaCd.TextValue

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, areaCd) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(LMM230C.EventShubetsu.SAKUJO) = False Then
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
        Me._Ds = New LMM230DS()
        Call Me.SetDatasetDelData(frm, Me._Ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM230BLF", "DeleteData", Me._Ds)

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
        MyBase.LMCacheMasterData(LMConst.CacheTBL.AREA_GRP)

        'メッセージ用エリアコード格納
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat("[", LMM230C.AREACD, " = ", areaCd, "]")})

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
    Private Sub SelectListEvent(ByVal frm As LMM230F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM230C.EventShubetsu.KENSAKU) = False Then
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

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM230C.EventShubetsu.KENSAKU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM230F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    Private Sub SprCellSelect(ByVal frm As LMM230F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim areaCd As String = String.Empty
        areaCd = frm.sprDetail.ActiveSheet.Cells(e.Row, LMM230G.sprDetailDef.AREA_CD.ColNo).Text()

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        Dim binKb As String = frm.sprDetail.ActiveSheet.Cells(e.Row, LMM230G.sprDetailDef.BIN_KB.ColNo).Text()
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


        Dim dt2 As DataTable = Me._JisDt

        ''メッセージ表示(編集モードの場合確認メッセージを表示する)
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(LMM230C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(e.Row, LMM230G.sprDetailDef.SYS_DEL_FLG.ColNo)).Equals(LMConst.FLG.OFF) Then
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
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Call Me._G.SetSpread2(dt2, areaCd)
        Call Me._G.SetSpread2(dt2, areaCd, binKb)
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

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
    Private Sub RowSelection(ByVal frm As LMM230F, ByVal rowNo As Integer)

        Dim areaCd As String = String.Empty
        areaCd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM230G.sprDetailDef.AREA_CD.ColNo).Text()

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        Dim binKb As String = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM230G.sprDetailDef.BIN_KB.ColNo).Text()
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

        Dim dt2 As DataTable = Me._JisDt

        '権限チェック
        If Me._V.IsAuthorityChk(LMM230C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM230G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
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

        'SPREAD(明細)へデータを移動
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Call Me._G.SetSpread2(dt2, areaCd)
        Call Me._G.SetSpread2(dt2, areaCd, binKb)
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

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
    Private Sub MasterShowEvent(ByVal frm As LMM230F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM230C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM230C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM230C.EventShubetsu.MASTEROPEN)

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
    Private Function SaveItemData(ByVal frm As LMM230F, ByVal eventShubetsu As LMM230C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM230C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        '単項目チェック
        If Me._V.IsSaveInputChk() = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        Call Me.SetDatasetItemData(frm, Me._Ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM230BLF", "InsertData", Me._Ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM230BLF", "UpdateData", Me._Ds)
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
        MyBase.LMCacheMasterData(LMConst.CacheTBL.AREA_GRP)

        '処理結果メッセージ表示
        Dim areaCd As String = frm.txtAreaCd.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat("[", LMM230C.AREACD, " = ", areaCd, "]")})

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM230C.EventShubetsu.MAIN)

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
    Friend Sub CloseFormEvent(ByVal frm As LMM230F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveItemData(frm, LMM230C.EventShubetsu.TOJIRU) = False Then

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
    Private Sub EnterAction(ByVal frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM230C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM230C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM230C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMMConH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub RowAdd(ByVal frm As LMM230F)

        Dim rtnResult As Boolean = False
        Dim areaCd As String = frm.txtAreaCd.TextValue

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        Dim binKb As String = frm.cmbBin.SelectedValue.ToString
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


        'マスタ参照
        rtnResult = Me.ShowPopupControl(frm, frm.btnRowAdd.Name, LMM230C.EventShubetsu.INS_T)

        If rtnResult = True Then

            Dim dt As DataTable = Me._Ds.Tables(LMM230C.TABLE_NM_JIS)

            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
            'Call Me._G.SetSpread2(dt, areaCd)
            Call Me._G.SetSpread2(dt, areaCd, binKb)
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

            'メッセージの表示
            MyBase.ShowMessage(frm, "G003")

        End If

        Call Me._LMMConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 行削除
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub RowDel(ByVal frm As LMM230F)

        Dim dt As DataTable = Me._Ds.Tables(LMM230C.TABLE_NM_JIS)
        Dim areaCd As String = frm.txtAreaCd.TextValue

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        Dim binKb As String = frm.cmbBin.SelectedValue.ToString
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

        '項目チェック
        If Me._V.IsRowCheck() = False Then
            Exit Sub
        End If

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        arr = Me._LMMConH.GetCheckList(frm.sprDetail2.ActiveSheet, LMM230G.sprDetailDef2.DEF.ColNo)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        'スプレッドの削除処理(削除フラグの設定・行の削除)
        dt = Me.DelJis(frm, dt, arr)

        'JISスプレッドの再描画
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Call Me._G.SetSpread2(dt, areaCd)
        Call Me._G.SetSpread2(dt, areaCd, binKb)
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

        'メッセージの表示
        MyBase.ShowMessage(frm, "G003")

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM230C.EventShubetsu.DEL_T)

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM230F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM230DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM230BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM230BLF", "SelectListData", _FindDs _
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
    Private Sub SuccessSelect(ByVal frm As LMM230F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM230C.TABLE_NM_OUT)
        Dim dt2 As DataTable = ds.Tables(LMM230C.TABLE_NM_JIS)

        '画面解除
        MyBase.UnLockedControls(frm)

        'SPREAD(表示行・明細)初期化
        frm.sprDetail.CrearSpread()
        frm.sprDetail2.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '取得データ(JIS)をPrivate変数に保持
        Call Me.SetDataSetJisData(dt2)

        Me._CntSelect = dt.Rows.Count.ToString()

        If LMConst.FLG.OFF.Equals(Me._CntSelect) = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        End If

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ステータスの設定
        Call Me._G.SetControlsStatus()

    End Sub


    ''' <summary>
    ''' データテーブル(JIS)に行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Function RowAddJisDt(ByVal frm As LMM230F, ByVal dr As DataRow) As Boolean

        Dim dt As DataTable = Me._Ds.Tables(LMM230C.TABLE_NM_JIS)

        Dim jisCd As String = dr("JIS_CD").ToString()
        Dim tmpDatarow2 As DataRow() = dt.Select(String.Concat("JIS_CD = '", jisCd, "' "))

        If tmpDatarow2.Length > 0 Then

            If LMConst.FLG.ON.Equals(tmpDatarow2(0).Item("SYS_DEL_FLG").ToString()) = True Then

                '削除データの場合、元の戻す
                tmpDatarow2(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                Return True

            Else

                '既に設定済みの場合、エラー
                MyBase.ShowMessage(frm, "E177", New String() {LMM230C.JISCD})
                Return False

            End If

        End If

        dt.ImportRow(dr)

        Dim dRow As DataRow = dt.Rows(dt.Rows.Count - 1)

        With frm
            dRow("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dRow("NRS_BR_NM") = .cmbNrsBrCd.TextValue
            dRow("AREA_CD") = .txtAreaCd.TextValue
            dRow("AREA_NM") = .txtAreaNm.TextValue
            dRow("BIN_KB") = .cmbBin.SelectedValue
            dRow("BIN_KB_NM") = .cmbBin.TextValue
            dRow("AREA_INFO") = .txtAreaInfo.TextValue
            dRow("SYS_ENT_DATE") = .lblCrtDate.TextValue
            dRow("SYS_ENT_USER_NM") = .lblCrtUser.TextValue
            dRow("SYS_UPD_DATE") = .lblUpdDate.TextValue
            dRow("SYS_UPD_TIME") = .lblUpdTime.TextValue
            dRow("SYS_UPD_USER_NM") = .lblUpdUser.TextValue
        End With

        dRow("UPD_FLG") = LMConst.FLG.OFF
        dRow("SYS_DEL_FLG") = LMConst.FLG.OFF

        Return True

    End Function

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM230F)

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
    Private Sub SetGMessage(ByVal frm As LMM230F)

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
    Private Function ShowPopupControl(ByVal frm As LMM230F, ByVal objNm As String, ByVal eventshubetsu As LMM230C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            Select Case objNm

                Case .txtJis.Name

                    Call Me.SetReturnJisPop(frm, objNm, eventshubetsu)

                Case .btnRowAdd.Name

                    Return Me.SetReturnJisPop(frm, objNm, eventshubetsu)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' JISマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowJisPopup(ByVal frm As LMM230F, Optional ByVal ctl As Win.InputMan.LMImTextBox = Nothing, Optional ByVal eventshubetsu As LMM230C.EventShubetsu = Nothing) As LMFormData

        Dim ds As DataSet = New LMZ070DS()
        Dim dt As DataTable = ds.Tables(LMZ070C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            If ctl Is Nothing = False Then
                'START SHINOHARA 要望番号513
                If eventshubetsu = LMM230C.EventShubetsu.ENTER Then
                    .Item("JIS_CD") = ctl.TextValue
                    'END SHINOHARA 要望番号513		
                End If
                .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            End If
End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ070", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' JISPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnJisPop(ByVal frm As LMM230F, ByVal objNm As String, ByVal eventshubetsu As LMM230C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Nothing
        If objNm.Equals("txtJis") = True Then
            ctl = Me._LMMConH.GetTextControl(frm, objNm)
        End If

        Dim prm As LMFormData = Me.ShowJisPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ070C.TABLE_NM_OUT).Rows(0)

            Select Case objNm

                Case frm.txtJis.Name
                    '画面ヘッダー部
                    frm.txtJis.TextValue = dr.Item("JIS_CD").ToString()
                    frm.lblKen.TextValue = dr.Item("KEN").ToString()
                    frm.lblShi.TextValue = dr.Item("SHI").ToString()

                Case frm.btnRowAdd.Name

                    'JISスプレッド
                    Return Me.RowAddJisDt(frm, dr)

            End Select

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' JISPOP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LMM230F, ByVal prm As LMFormData) As DataSet

        Dim prmDs As DataSet = New LMZ070DS()
        Dim dr As DataRow = prmDs.Tables(LMZ070C.TABLE_NM_IN).NewRow

        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'dr("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
        dr("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
        dr("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        prmDs.Tables(LMZ070C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs

        'POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ070", prm)

        Return prm.ParamDataSet

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM230F)

        Dim dt As DataTable = Me._FindDs.Tables(LMM230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM230G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM230G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("AREA_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM230G.sprDetailDef.AREA_CD.ColNo))
            dr("AREA_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM230G.sprDetailDef.AREA_NM.ColNo))
            dr("BIN_KB") = Me._LMMConV.GetCellValue(.Cells(0, LMM230G.sprDetailDef.BIN_KB_NM.ColNo))
            dr("AREA_INFO") = Me._LMMConV.GetCellValue(.Cells(0, LMM230G.sprDetailDef.AREA_INFO.ColNo))
            dr("JIS_CD") = frm.txtJis.TextValue.Trim()
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
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
    Private Sub SetDatasetItemData(ByVal frm As LMM230F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM230C.TABLE_NM_JIS)
        Dim dr As DataRow = Nothing


        With frm

            Dim nrsBrCd As String = .cmbNrsBrCd.SelectedValue.ToString()
            Dim areaCd As String = .txtAreaCd.TextValue.Trim()
            Dim areaNm As String = .txtAreaNm.TextValue.Trim()
            Dim binKb As String = .cmbBin.SelectedValue.ToString()
            Dim areaInfo As String = .txtAreaInfo.TextValue.Trim()
            Dim updDate As String = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim())
            Dim updTime As String = .lblUpdTime.TextValue.Trim()
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'Dim userBrCd As String = LMUserInfoManager.GetNrsBrCd
            Dim userBrCd As String = .cmbNrsBrCd.SelectedValue.ToString()

            Dim max As Integer = dt.Rows.Count - 1

            For i As Integer = 0 To max

                dr = dt.Rows(i)

                '編集部の値(エリアコード)をデータセットに設定
                dr("NRS_BR_CD") = nrsBrCd
                dr("AREA_CD") = areaCd
                dr("AREA_NM") = areaNm
                dr("BIN_KB") = binKb
                dr("AREA_INFO") = areaInfo
                dr("SYS_UPD_DATE") = updDate
                dr("SYS_UPD_TIME") = updTime
                dr("USER_BR_CD") = userBrCd

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM230F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("AREA_CD") = .txtAreaCd.TextValue.Trim()
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
            dr("BIN_KB") = .cmbBin.SelectedValue
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim())
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim()

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            Dim delflg As String = String.Empty
            If LMConst.FLG.OFF.Equals(.lblSituation.RecordStatus) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If

            dr("SYS_DEL_FLG") = delflg

        End With

        dt.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 行削除(データセットから削除)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="arr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DelJis(ByVal frm As LMM230F, ByVal dt As DataTable, ByVal arr As ArrayList) As DataTable

        Dim max As Integer = arr.Count - 1
        Dim jisCd As String = String.Empty
        Dim dr As DataRow = Nothing
        Dim rowNo As Integer = 0

        With frm.sprDetail2

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                dr = dt.Select(String.Concat("JIS_CD = '" _
                                             , Me._LMMConV.GetCellValue(.ActiveSheet.Cells(Convert.ToInt32(arr(i)) _
                                             , LMM230G.sprDetailDef2.JIS_CD.ColNo)), "' "))(0)

                If LMConst.FLG.ON.Equals(dr.Item("UPD_FLG")) = True Then
                    dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                Else
                    dr.Delete()
                End If

            Next

        End With

        Return dt

    End Function

    ''' <summary>
    ''' データセット設定(JIS情報格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetJisData(ByVal dt As DataTable)

        Me._JisDt = dt

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
    Friend Sub FunctionKey1Press(ByRef frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Call Me.NewDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Call Me.EditDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Call Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Call Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Call Me.MasterShowEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub


    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveItemData")

        Call Me.SaveItemData(frm, LMM230C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM230F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM230F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Call Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROWADD_Click(ByVal frm As LMM230F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_TCUST_Click")

        '「行追加」処理
        Call Me.RowAdd(frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_TCUST_Click")

    End Sub

    ''' <summary>
    ''' 行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROWDEL_Click(ByRef frm As LMM230F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_TCUST_Click")

        '「行削除」処理
        Call Me.RowDel(frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_TCUST_Click")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM230F_KeyDown(ByVal frm As LMM230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM230F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM230F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM230F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

End Class