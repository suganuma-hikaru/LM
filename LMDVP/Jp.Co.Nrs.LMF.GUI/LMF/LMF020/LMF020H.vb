' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF020H : 運送入力
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF020G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconV As LMFControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconH As LMFControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet

    ''' <summary>
    ''' 前回値を保持する変数
    ''' </summary>
    ''' <remarks></remarks>
    Private _PreInputData As String = String.Empty

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
        Dim frm As LMF020F = New LMF020F(Me)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Hnadler共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMF020G(Me, frm, Me._LMFconG)

        'Validateクラスの設定
        Me._V = New LMF020V(Me, frm, Me._LMFconV, Me._G, Me._LMFconG)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'コントロール設定
        Call Me._G.SetControl()

        '値のクリア
        Call Me._G.ClearControl()

        '初期設定
        If Me.SetForm(frm, prmDs, prm.RecStatus) = False Then
            Exit Sub
        End If

        'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
        '複写処理
        If (RecordStatus.COPY_REC).Equals(prm.RecStatus) = True Then
            Call Me.ShiftCopyMode(frm, LMF020C.ActionType.COPY_INIT)

            '呼び元画面の値を設定する
            Call Me._G.SetCopyInitValue(prmDs)
        End If
        'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

        'メッセージの表示
        Call Me.SetGMessage(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' ロード処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="prmDs">データセット</param>
    ''' <param name="recStatus">レコードステータス</param>
    ''' <returns>
    ''' True ：検索成功
    ''' false：検索失敗
    ''' </returns>
    ''' <remarks></remarks>

    Private Function SetForm(ByVal frm As LMF020F, ByVal prmDs As DataSet, ByVal recStatus As String) As Boolean

        Dim rtnResult As Boolean = False
        Dim mode As String = String.Empty
        Dim status As String = String.Empty

        'ステータス判定
        Select Case recStatus

            Case RecordStatus.NEW_REC

                '初期検索処理
                Me._Ds = Me.ServerAccess(prmDs, LMF020C.ACTION_ID_INIT_NEW)
                mode = DispMode.EDIT
                status = RecordStatus.NEW_REC

            Case RecordStatus.NOMAL_REC

                '初期検索処理
                Me._Ds = Me.ServerAccess(prmDs, LMF020C.ACTION_ID_INIT_SELECT)
                mode = DispMode.VIEW
                status = RecordStatus.NOMAL_REC

                'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
            Case RecordStatus.COPY_REC

                '初期検索処理
                Me._Ds = Me.ServerAccess(prmDs, LMF020C.ACTION_ID_INIT_SELECT)
                mode = DispMode.VIEW
                status = RecordStatus.COPY_REC
                'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

        End Select

        '検索成功
        If Me._Ds Is Nothing = False _
            AndAlso 0 < Me._Ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows.Count Then

            rtnResult = True

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(mode, status)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

            '初期表示時の値設定
            Call Me._G.SetInitValue(Me._Ds)

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(False)

            'LMF800DSを設定
            Me._Ds = Me.SetUnchinCalcDataSet(Me._Ds)

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            'LMF810DSを設定
            Me._Ds = Me.SetShiharaiCalcDataSet(Me._Ds)
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End If

        Return rtnResult

    End Function

#End Region '初期処理

#Region "イベント定義(一覧)"


    ''' <summary>
    ''' 運送新規処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub NewUnsoData(ByVal frm As LMF020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.UNSO_NEW)

        ''入力チェック
        'rtnResult = rtnResult AndAlso Me._V.IsCopyChk()

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        Dim rtDs As DataSet = New LMF020DS()
        rtDs.Clear()

        Dim dt As DataTable = rtDs.Tables(LMF020C.TABLE_NM_IN)
        Dim outDr As DataRow = dt.NewRow()

        outDr = dt.NewRow()
        outDr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        outDr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue.ToString
        outDr.Item("CUST_CD_M") = frm.txtCustCdM.TextValue.ToString

        '設定値をデータセットに設定
        rtDs.Tables(LMF020C.TABLE_NM_IN).Rows.Add(outDr)

        '値のクリア
        Call Me._G.ClearControl()

        Call SetForm(frm, rtDs, RecordStatus.NEW_REC)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(False)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftEditMode(ByVal frm As LMF020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.EDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsEditChk(Me._Ds)

        '排他チェック
        rtnResult = rtnResult AndAlso Me.ChkHaitaData(frm, Me._Ds)

        '運行データキャンセル可否チェック
        rtnResult = rtnResult AndAlso Me.SetTripStatusData(frm, Me._Ds)

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(False)

        'ロック制御用に値の再設定
        Call Me._G.SetSpread(Me._Ds)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
    '''' <summary>
    '''' 複写処理
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Sub ShiftCopyMode(ByVal frm As LMF020F)
    ''' <summary>
    ''' 複写処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftCopyMode(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType)
        'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.COPY)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsCopyChk()

        'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
        ''確認メッセージ表示
        'rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F3ButtonName))
        If (LMF020C.ActionType.COPY).Equals(actionType) = True Then
            '確認メッセージ表示
            rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F3ButtonName))
        End If
        'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'コピーデータ設定
        Me._Ds = Me.SetCopyData(Me._Ds)

        '値設定
        Call Me._G.SetInitValue(Me._Ds)

        'START YANAI 要望番号1259 複写時の初期値変更
        ''計算処理
        'Call Me.AllCalculation(frm, LMF020C.ActionType.CALC)
        '計算処理
        Call Me.AllCalculation(frm, LMF020C.ActionType.COPY)
        'END YANAI 要望番号1259 複写時の初期値変更

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(False)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteAction(ByVal frm As LMF020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.EDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsDeleteChk(Me._Ds)

        '運行データキャンセル可否チェック
        rtnResult = rtnResult AndAlso Me.SetTripStatusData(frm, Me._Ds)

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F4ButtonName))

        '削除処理
        rtnResult = rtnResult AndAlso Me.DeleteAction(frm, Me._Ds)

        'エラーの場合、ロック解除
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '画面を閉じる
        frm.Close()

    End Sub

    ''' <summary>
    ''' 届先登録
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SaveDestMaster(ByVal frm As LMF020F)

        '権限
        If Me._V.IsAuthority(LMF020C.ActionType.DEST_SAVE) = False Then
            Exit Sub
        End If

        Dim ds As DataSet = New LMM040DS()
        Dim dt As DataTable = ds.Tables(LMControlC.LMM040C_TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
        dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        dr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
        dr.Item("CUST_NM_L") = Me._Ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0).Item("CUST_NM_L").ToString()
        dr.Item("MODE_FLG") = LMConst.FLG.ON
        dt.Rows.Add(dr)

        '届先マスタメンテを表示
        Call Me._LMFconH.FormShow(ds, "LMM040", RecordStatus.NEW_REC)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMF020F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF020C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        '項目チェック：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMF020C.ActionType.MASTEROPEN)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        '参照の場合、Tab移動して終了
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
            Call Me._LMFconH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        'Enterキー判定
        Dim rtnResult As Boolean = eventFlg

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMF020C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF020C.ActionType.ENTER)

        '計算処理
        rtnResult = rtnResult AndAlso Me.EnterCalcData(frm, objNm)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

            'メッセージ設定
            Call Me.ShowGMessage(frm)

            Exit Sub

        End If

        '項目チェック：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMF020C.ActionType.ENTER)

        'フォーカス移動処理
        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SaveUnsoItemData(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As Boolean

        'フォーカス位置コントロール名
        Dim objNm As String = frm.ActiveControl.Name

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.SAVE)

        'Leaveイベントを強制的に処理
        rtnResult = rtnResult AndAlso Me.SaveLeaveAction(frm, objNm)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

        '計算処理
        rtnResult = rtnResult AndAlso Me.AllCalculation(frm, LMF020C.ActionType.SAVE)

        '値設定
        rtnResult = rtnResult AndAlso Me.SetData(frm, Me._Ds)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnsoSaveData(frm, Me._Ds)

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、処理を終了
        If rtnResult = False Then
            Return False
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        Dim dt As DataTable = Me._Ds.Tables(LMF020C.TABLE_NM_IN)
        Dim outDr As DataRow = dt.NewRow()

        outDr = dt.NewRow()
        outDr.Item("NRS_BR_CD") = Me._Ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString()
        outDr.Item("UNSO_NO_L") = Me._Ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0).Item("UNSO_NO_L").ToString()
        outDr.Item("CUST_CD_L") = Me._Ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0).Item("CUST_CD_L").ToString()
        outDr.Item("CUST_CD_M") = Me._Ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0).Item("CUST_CD_M").ToString()

        '設定値をデータセットに設定
        Me._Ds.Tables(LMF020C.TABLE_NM_IN).Rows.Add(outDr)

        '値のクリア
        Call Me._G.ClearControl()

        Me._Ds = Me.ServerAccess(Me._Ds, LMF020C.ACTION_ID_INIT_SELECT)

        '値の設定
        Call Me._G.SetInitValue(Me._Ds)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(False)

        '処理終了メッセージの表示
        Call Me._LMFconH.SetMessageG002(frm _
                                        , Me._LMFconV.SetRepMsgData(frm.FunctionKey.F11ButtonName) _
                                        , String.Concat(LMFControlC.KAKKO_1, frm.lblTitleUnsoNo.Text, LMFControlC.EQUAL, frm.lblUnsoNo.TextValue, LMFControlC.KAKKO_2))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        Return True

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PrintAction(ByVal frm As LMF020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.SAVE)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsPrintChk(Me._Ds)

        'メイン処理
        rtnResult = rtnResult AndAlso Me.PrintAction(frm, Me._Ds)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 行追加
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddUnsoMData(ByVal frm As LMF020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.ADD)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsAddChk()

        '上限チェック
        rtnResult = rtnResult AndAlso Me._LMFconV.IsMaxSeqChk(frm.sprDetail.ActiveSheet.Rows.Count, 1, LMF020C.UNSO_L_NM)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '運送(中)の設定
        Me._Ds = Me.SetUnsoMData(frm, Me._Ds)

        '1行追加
        Me._Ds = Me.SetUnsoMInitData(frm, Me._Ds)

        '値設定
        Call Me._G.SetSpread(Me._Ds)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 行削除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DelUnsoMData(ByVal frm As LMF020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF020C.ActionType.DEL)

        '未選択チェック
        Dim arr As ArrayList = Nothing
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        If rtnResult = True Then

            '運送(中)の設定
            Me._Ds = Me.SetUnsoMData(frm, Me._Ds)

            '並び替え
            Call Me._G.sprSortColumnCommand(spr, LMF020G.sprDetailDef.REC_NO.ColNo)
            arr = Me._LMFconG.GetCheckList(spr.ActiveSheet, LMF020G.sprDetailDef.DEF.ColNo)

        End If
        rtnResult = rtnResult AndAlso Me._LMFconV.IsSelectChk(arr.Count)

        'エラーの場合、終了
        If rtnResult = False Then

            '値設定
            Call Me._G.SetSpread(Me._Ds)

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = Me._Ds.Tables(LMF020C.TABLE_NM_UNSO_M)
        For i As Integer = max To 0 Step -1

            '選択行の削除
            dt.Rows(Convert.ToInt32(Me._LMFconG.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMF020G.sprDetailDef.REC_NO.ColNo)))).Delete()

        Next

        '処理終了アクション
        Call Me.EndAction(frm)

        '値設定
        Call Me._G.SetSpread(Me._Ds)

    End Sub

    ''' <summary>
    ''' タリフ分類区分によるロック制御
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangedTariffKbn(ByVal frm As LMF020F)

        '処理開始判定
        If Me.ChkActionStart(frm) = False Then
            Exit Sub
        End If

        'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
        '運送会社コードOLD設定
        Me._G.SetUnsoCdOld(frm)

        '運賃タリフセットからタリフコードを設定
        Call Me._G.GetUnchinTariffSet(frm, True)

        '割増備考の設定
        Call Me.SetExtcRemData(frm)
        'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

        'ロック制御
        Call Me._G.TariffKbnLockControl()

    End Sub

    ''' <summary>
    ''' タリフセットマスタの値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetTariffData(ByVal frm As LMF020F, Optional ByVal lockFlg As Boolean = True)

        With frm

            '処理開始判定
            If Me.ChkActionStart(frm) = False Then
                Exit Sub
            End If

            '前回値と同じ場合、スルー
            Dim destCd As String = .txtDestCd.TextValue
            If Me._PreInputData.Equals(destCd) = True Then
                Exit Sub
            End If

            '値が無い場合、スルー
            If String.IsNullOrEmpty(destCd) = True Then
                Exit Sub
            End If

            If lockFlg = True Then

                '処理開始アクション
                Call Me.StartAction(frm)

            End If

            '値設定
            If Me._G.SetTariffSetData(Me._V.GetCalcUlation()) = True Then

                '割増備考の設定
                Call Me.SetExtcRemData(frm)

            End If

            '保持している値の更新
            Call Me.UpdatePreInputData(destCd)

            If lockFlg = True Then

                '処理終了アクション
                Call Me.EndAction(frm)

            End If

        End With

    End Sub

    ''' <summary>
    ''' 計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCalcData(ByVal frm As LMF020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '計算処理
        Call Me.AllCalculation(frm, LMF020C.ActionType.CALC)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' スプレッドの値変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub CellValueChange(ByVal frm As LMF020F)

        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        With spr.ActiveSheet

            '行がない場合、
            If .Rows.Count < 1 Then
                Exit Sub
            End If

            '商品KEY以外、スルー
            Dim cell As FarPoint.Win.Spread.Cell = .ActiveCell
            Dim colNo As Integer = cell.Column.Index
            Dim rowNo As Integer = cell.Row.Index
            If LMF020G.sprDetailDef.GOODS_CD.ColNo <> colNo OrElse _
               String.IsNullOrEmpty(Me._LMFconG.GetCellValue(.Cells(rowNo, colNo))) = True Then
                Exit Sub
            End If

            '処理開始アクション
            Call Me.StartAction(frm)

            'キャッシュ(商品M)から値取得
            Dim drs As DataRow() = Me._LMFconG.SelectGoodsListDataRow(frm.cmbEigyo.SelectedValue.ToString(), _
                                                                      Me._LMFconG.GetCellValue(.Cells(rowNo, colNo)), _
                                                                      frm.txtCustCdL.TextValue.ToString(), _
                                                                      frm.txtCustCdM.TextValue.ToString() _
                                                                      )
            '処理終了アクション
            Call Me.EndAction(frm)

            '取得できない場合、スルー
            If drs.Length < 1 Then
                Exit Sub
            End If

            '値設定 + ロック制御
            Call Me._G.SetGoodsInfo(drs(0), rowNo)

        End With

    End Sub

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub UnsocoCdLeave(ByVal frm As LMF020F)

        '運送会社コードOLD設定
        Me._G.SetUnsoCdOld(frm)

        '運賃タリフセットからタリフコードを設定
        Call Me._G.GetUnchinTariffSet(frm, False)

        '割増備考の設定
        Call Me.SetExtcRemData(frm)

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMF020F, ByVal e As FormClosingEventArgs)

        '編集モード以外なら処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False Then
            Exit Sub
        End If

        'メッセージの表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes '「はい」押下時

                '保存処理
                If Me.SaveUnsoItemData(frm, LMF020C.ActionType.CLOSE) = False Then
                    e.Cancel = True
                End If

            Case MsgBoxResult.Cancel '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    '要望対応:1816 yamanaka 2013.02.22 Start
    ''' <summary>
    ''' 印刷種別による値変更
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangedCmbPrint(ByVal frm As LMF020F)

        '参照モード以外なら終了
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = False Then
            Exit Sub
        End If

        '印刷種別による値の設定
        Me._G.SetCmbPrint(frm)

        Me._G.PrintCntSetControl(frm)

    End Sub
    '要望対応:1816 yamanaka 2013.02.22 End

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "計算"

    ''' <summary>
    ''' 全行計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function AllCalculation(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail

        '(2012.12.20)要望番号1692 容器重量対応 -- START --
        Dim dsCom As DataSet = Nothing
        Dim goodsDtlDt As DataTable = Nothing
        Dim drCom As DataRow = Nothing
        Dim hutaiJuryo As String = String.Empty
        '(2012.12.20)要望番号1692 容器重量対応 --  END  --

        With spr.ActiveSheet

            Dim konpoSu As Decimal = 0
            Dim juryo As Decimal = 0
            Dim max As Integer = .Rows.Count - 1
            Dim tareYnUnso As String = frm.lblTareYn.TextValue

            '各明細の計算処理
            For i As Integer = 0 To max

                'オーバーフローした場合、終了
                If Me.CalculationRowData(spr, i) = False Then
                    Return False
                End If

                '(2012.12.20)要望番号1692 容器重量対応 -- START --

                'DataSet・変数初期化
                dsCom = New DSL.LMCOMDS
                goodsDtlDt = dsCom.Tables("GOODS_DETAILS_IN")
                drCom = goodsDtlDt.NewRow()
                hutaiJuryo = String.Empty

                '商品明細マスタ抽出条件セット
                drCom.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                drCom.Item("GOODS_CD_NRS") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.GOODS_CD.ColNo))
                drCom.Item("SUB_KB") = "16"    '容器重量用SUB_KB
                goodsDtlDt.Rows.Add(drCom)

                '商品明細マスタデータ取得(BLF呼び出し)
                dsCom = _LMFconH.ServerAccessLmcom(dsCom, "SelectGoodsDetailsData")

                If dsCom.Tables("GOODS_DETAILS_OUT").Rows.Count <> 0 Then
                    hutaiJuryo = dsCom.Tables("GOODS_DETAILS_OUT").Rows(0).Item("SET_NAIYO").ToString   '風袋重量
                End If

                '(2012.12.20)要望番号1692 容器重量対応 --  END  --

                '運送梱包数の集計値
                konpoSu += Me.GetUnsoKonpoData(spr, i)

                '運送重量の集計値
                juryo += Me.GetJuryoData(spr, i, tareYnUnso, hutaiJuryo)

            Next

            '計算ボタン,保存イベントでない場合、スルー
            Select Case actionType
                Case LMF020C.ActionType.SAVE, LMF020C.ActionType.CALC
                Case Else
                    Return True
            End Select

            '運送重量の切り上げ処理
            juryo = System.Math.Ceiling(juryo)

            '値設定
            Return Me.SetKonpoJuryo(frm, actionType, konpoSu, juryo)

        End With

    End Function

    ''' <summary>
    ''' 運送梱包数 , 運送重量の設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="konpoSu">計算した運送梱包数</param>
    ''' <param name="juryo">計算した運送重量の</param>
    ''' <remarks></remarks>
    Private Function SetKonpoJuryo(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType, ByVal konpoSu As Decimal, ByVal juryo As Decimal) As Boolean

        With frm

            Select Case actionType

                Case LMF020C.ActionType.SAVE

                    '画面の梱包数に値がある場合、画面値をそのまま
                    Dim value As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(.numUnsoPkgCnt.TextValue))
                    If 0 <> value Then
                        konpoSu = value
                    End If

                    '画面の運送重量に値がある場合、画面値をそのまま
                    value = Convert.ToDecimal(Me._LMFconG.FormatNumValue(.numUnsoWtL.TextValue))
                    If 0 <> value Then
                        juryo = value
                    End If

            End Select

            '運送梱包数の上限チェック
            If Me._V.IsCalcOver(konpoSu.ToString(), LMFControlC.MIN_0, LMF020C.MAX_10, frm.lblTitleUnsoPkgCnt.Text) = False Then
                frm.numUnsoPkgCnt.Value = LMF020C.MAX_10
                Return False
            End If

            '運送重量の上限チェック
            If Me._V.IsCalcOver(juryo.ToString(), LMFControlC.MIN_0, LMF020C.MAX_9, frm.lblTitleUnsoWtL.Text) = False Then
                frm.numUnsoWtL.Value = LMF020C.MAX_9
                Return False
            End If

            '値設定
            .numUnsoPkgCnt.Value = konpoSu
            .numUnsoWtL.Value = juryo

            Return True

        End With

    End Function

    ''' <summary>
    ''' 行単位の計算処理
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CalculationRowData(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer) As Boolean

        With spr.ActiveSheet

            '計算フラグ <> 1の場合、スルー
            If LMConst.FLG.ON.Equals(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.CALC_FLG.ColNo))) = False Then
                Return True
            End If

            '端数 , 入数の関連チェック
            If Me._V.IsHasuIrisuChk(rowNo) = False Then
                Return False
            End If

            '運送個数　×　梱包個数　×　入目　＋　端数　×　入目
            Dim kosu As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.UNSO_KOSU.ColNo))))
            Dim konpo As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.KONPO_KOSU.ColNo))))
            Dim irime As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.IRIME.ColNo))))
            Dim hasu As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.HASU.ColNo))))
            Dim value As String = (kosu * konpo * irime + hasu * irime).ToString()

            '上限チェック
            If Me._V.IsCalcOver(value, LMFControlC.MIN_0, LMF020C.MAX_9_3, LMF020G.sprDetailDef.UNSO_SURYO.ColName) = False Then
                spr.SetCellValue(rowNo, LMF020G.sprDetailDef.UNSO_SURYO.ColNo, LMF020C.MAX_9_3)
                Return False
            End If

            '値設定
            spr.SetCellValue(rowNo, LMF020G.sprDetailDef.UNSO_SURYO.ColNo, value)

        End With

        Return True

    End Function

    ''' <summary>
    ''' 運送梱包数の計算
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoKonpoData(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer) As Decimal

        With spr.ActiveSheet

            Return Me._LMFconH.GetUnsoKonpoData(Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.UNSO_KOSU.ColNo)))) _
                                                , Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.HASU.ColNo)))) _
                                                , Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.KONPO_KOSU.ColNo)))) _
                                                )

        End With

    End Function

    ''' <summary>
    ''' 重量を取得
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="tareYnUnso">風袋加算フラグ(運送会社)</param>
    ''' <returns>重量</returns>
    ''' <remarks></remarks>
    Private Function GetJuryoData(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer, ByVal tareYnUnso As String, Optional ByVal hutaiJyuryo As String = "") As Decimal

        With spr.ActiveSheet

            Dim kosu As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.UNSO_KOSU.ColNo))))
            Dim konpo As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.KONPO_KOSU.ColNo))))
            Dim hasu As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.HASU.ColNo))))
            '(2012.12.06)要望番号1649 小分け時の運送重量対応 --- START ---
            Dim suryo As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.UNSO_SURYO.ColNo))))
            '(2012.12.06)要望番号1649 小分け時の運送重量対応 ---  END  ---

            '計算フラグ = 0の場合
            If LMConst.FLG.OFF.Equals(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.CALC_FLG.ColNo))) = True Then

                '重量 * 運送梱包個数
                Return Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.JURYO.ColNo)))) _
                       * (kosu * konpo + hasu)

            End If

            '(2012.12.06)要望番号1649 小分け時の運送重量対応 suryo追加
            Return Me._LMFconH.GetJuryoCalcData(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.TARE_YN.ColNo)), tareYnUnso _
                                                , Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.SURYO_TANI.ColNo)) _
                                                , Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.IRIME.ColNo)))) _
                                                , konpo _
                                                , kosu _
                                                , hasu _
                                                , Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.STD_IRIME_NB.ColNo)))) _
                                                , Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.JURYO.ColNo)))) _
                                                , suryo _
                                                , hutaiJyuryo.ToString _
                                                )
        End With

    End Function

#End Region

#Region "PopUp"

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actinType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMF020F, ByVal actinType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
            Dim csFlg As String = LMConst.FLG.ON

            Select Case actinType

                Case LMF020C.ActionType.UNSO_NEW

                    csFlg = LMConst.FLG.OFF

            End Select

            .Item("NRS_BR_CD") = brCd
            'If actinType = LMF020C.ActionType.ENTER _
            '  OrElse actinType = LMF020C.ActionType.UNSO_NEW Then
            '    .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            '    .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            'End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csFlg
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMF020F, ByVal objNm As String, ByVal actionType As LMF020C.ActionType) As Boolean

        With frm

            Dim rtnResult As Boolean = False

            'スプレッドの場合、後でロック
            Dim sprNm As String = .sprDetail.Name
            If sprNm.Equals(objNm) = False Then

                '処理開始アクション
                Call Me.StartAction(frm)

            End If

            Select Case objNm

                Case sprNm

                    Return Me.ShowPopupSpread(frm, objNm, actionType)

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    rtnResult = Me.SetReturnUnsocoPop(frm, actionType)

                    '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
                Case .txtCustCdM.Name

                    rtnResult = Me._V.IsCustExistChk(False) 'エラー表示無しで名称を表示する
                    '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End

                Case .txtShipCd.Name

                    rtnResult = Me.SetReturnDestPop(frm, objNm, actionType)

                Case .txtTariffCd.Name

                    rtnResult = Me.SetReturnTariffPop(frm, actionType)

                Case .txtExtcTariffCd.Name

                    rtnResult = Me.SetReturnExtcPop(frm, actionType)

                    'START UMANO 要望番号1302 支払運賃に伴う修正。
                Case .txtPayTariffCd.Name

                    rtnResult = Me.SetReturnShiharaiTariffPop(frm, actionType)

                Case .txtPayExtcTariffCd.Name

                    rtnResult = Me.SetReturnShiharaiExtcPop(frm, actionType)
                    'END UMANO 要望番号1302 支払運賃に伴う修正。

                Case .txtOrigCd.Name, .txtDestCd.Name

                    rtnResult = Me.SetReturnDestPop(frm, objNm, actionType)

                Case .txtAreaCd.Name

                    rtnResult = Me.SetReturnAriaPop(frm, actionType)

            End Select

            '処理終了アクション
            Call Me.EndAction(frm)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' スプレッドのマスタ参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupSpread(ByVal frm As LMF020F, ByVal objNm As String, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail

        With spr.ActiveSheet

            If 0 < .Rows.Count Then

                Dim cell As FarPoint.Win.Spread.Cell = .ActiveCell
                Dim colNo As Integer = cell.Column.Index
                Dim rowNo As Integer = cell.Row.Index

                Select Case colNo

                    Case LMF020G.sprDetailDef.GOODS_CD.ColNo _
                       , LMF020G.sprDetailDef.GOODS_NM.ColNo

                        '禁止文字チェック
                        If Me._V.IsPopupInputCheck(spr, rowNo, LMF020G.sprDetailDef.GOODS_CD.ColNo, LMF020G.sprDetailDef.GOODS_CD.ColName) = False Then
                            Return False
                        End If

                        '禁止文字チェック
                        If Me._V.IsPopupInputCheck(spr, rowNo, LMF020G.sprDetailDef.GOODS_NM.ColNo, LMF020G.sprDetailDef.GOODS_NM.ColName) = False Then
                            Return False
                        End If

                        'Enter処理は値がない場合、スルー
                        Select Case actionType

                            Case LMF020C.ActionType.ENTER

                                If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMF020G.sprDetailDef.GOODS_CD.ColNo))) = True _
                                    AndAlso String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMF020G.sprDetailDef.GOODS_NM.ColNo))) = True _
                                    Then
                                    Return False
                                End If

                        End Select

                        'ロック項目はスルー
                        If Me._V.IsFocusSprChk(spr, cell) = False Then
                            Return False
                        End If

                        '処理開始アクション
                        Call Me.StartAction(frm)

                        '商品Pop起動
                        Return Me.SetReturnGoodsPop(frm, rowNo, actionType)

                End Select

            End If

        End With

        'START YANAI 要望番号530
        'Return Me._LMFconV.SetFocusErrMessage()
        If (actionType).Equals(LMF020C.ActionType.MASTEROPEN) = True Then
            'マスタ参照押下時のみ
            Return Me._LMFconV.SetFocusErrMessage()
        End If
        'END YANAI 要望番号530

    End Function

    ''' <summary>
    ''' 運送会社Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsocoPop(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowUnsocoPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtUnsocoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtUnsocoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblUnsocoNm.TextValue = Me._LMFconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), Space(1))
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                .txtPayTariffCd.TextValue = dr.Item("UNCHIN_TARIFF_CD").ToString()
                .lblPayTariffRem.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()
                .txtPayExtcTariffCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblPayExtcTariffRem.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
                'END UMANO 要望番号1302 支払運賃に伴う修正。
            End With

            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            '運送会社コードOLD設定
            Me._G.SetUnsoCdOld(frm)

            '運賃タリフセットからタリフコードを設定
            Call Me._G.GetUnchinTariffSet(frm, False)

            '割増備考の設定
            Call Me.SetExtcRemData(frm)
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnsocoPopup(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNSOCO_CD") = frm.txtUnsocoCd.TextValue
                .Item("UNSOCO_BR_CD") = frm.txtUnsocoBrCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim prm As LMFormData = Nothing
        Dim tblNm As String = String.Empty
        Dim code As String = String.Empty
        Dim name As String = String.Empty

        With frm

            If LMFControlC.TARIFF_YOKO.Equals(.cmbTariffKbn.SelectedValue.ToString()) = True Then

                '横持ちタリフPop
                prm = Me.ShowYokoTariffPopup(frm, actionType)
                tblNm = LMZ100C.TABLE_NM_OUT
                code = "YOKO_TARIFF_CD"
                name = "YOKO_REM"

            Else

                '運賃タリフPop
                prm = Me.ShowUnchinTariffPopup(frm, actionType)
                tblNm = LMZ230C.TABLE_NM_OUT
                code = "UNCHIN_TARIFF_CD"
                name = "UNCHIN_TARIFF_REM"

            End If

            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(tblNm).Rows(0)

                .txtTariffCd.TextValue = dr.Item(code).ToString()
                .lblTariffRem.TextValue = dr.Item(name).ToString()

                Return True

            End If

        End With

        Return False

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMF020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNCHIN_TARIFF_CD") = frm.txtTariffCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("STR_DATE") = Me._G.GetUnchinTariffDate(Me._V.GetCalcUlation())
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("YOKO_TARIFF_CD") = frm.txtTariffCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 割増タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnExtcPop(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowExtcPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtExtcTariffCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblExtcTariffRem.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowExtcPopup(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("EXTC_TARIFF_CD") = frm.txtExtcTariffCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnShiharaiTariffPop(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim prm As LMFormData = Nothing
        Dim tblNm As String = String.Empty
        Dim code As String = String.Empty
        Dim name As String = String.Empty

        With frm

            If LMFControlC.TARIFF_YOKO.Equals(.cmbTariffKbn.SelectedValue.ToString()) = True Then

                '支払横持ちタリフPop
                prm = Me.ShowShiharaiYokoTariffPopup(frm, actionType)
                tblNm = LMZ320C.TABLE_NM_OUT
                code = "YOKO_TARIFF_CD"
                name = "YOKO_REM"

            Else

                '支払運賃タリフPop
                prm = Me.ShowShiharaiTariffPopup(frm, actionType)
                tblNm = LMZ290C.TABLE_NM_OUT
                code = "SHIHARAI_TARIFF_CD"
                name = "SHIHARAI_TARIFF_REM"

            End If

            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(tblNm).Rows(0)

                .txtPayTariffCd.TextValue = dr.Item(code).ToString()
                .lblPayTariffRem.TextValue = dr.Item(name).ToString()

                Return True

            End If

        End With

        Return False

    End Function

    ''' <summary>
    ''' 支払運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowShiharaiTariffPopup(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ290DS()
        Dim dt As DataTable = ds.Tables(LMZ290C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If actionType = LMF020C.ActionType.ENTER Then
                .Item("UNSOCO_CD") = frm.txtUnsocoCd.TextValue
                .Item("UNSOCO_BR_CD") = frm.txtUnsocoBrCd.TextValue
                .Item("SHIHARAI_TARIFF_CD") = frm.txtPayTariffCd.TextValue
            End If
            .Item("STR_DATE") = frm.imdDestDate.TextValue
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ290", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 支払横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowShiharaiYokoTariffPopup(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ320DS()
        Dim dt As DataTable = ds.Tables(LMZ320C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If actionType = LMF020C.ActionType.ENTER Then
                .Item("YOKO_TARIFF_CD") = frm.txtPayTariffCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ320", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 支払割増タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnShiharaiExtcPop(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowShiharaiExtcPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ300C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtPayExtcTariffCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblPayExtcTariffRem.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowShiharaiExtcPopup(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ300DS()
        Dim dt As DataTable = ds.Tables(LMZ300C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If actionType = LMF020C.ActionType.ENTER Then
                .Item("UNSOCO_CD") = frm.txtUnsocoCd.TextValue
                .Item("UNSOCO_BR_CD") = frm.txtUnsocoBrCd.TextValue
                .Item("EXTC_TARIFF_CD") = frm.txtPayExtcTariffCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ300", "", Me._PopupSkipFlg)

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMF020F, ByVal objNm As String, ByVal actionType As LMF020C.ActionType) As Boolean

        With frm

            Dim ctl As Win.InputMan.LMImTextBox = Me._LMFconH.GetTextControl(frm, objNm)
            Dim prm As LMFormData = Me.ShowDestPopup(frm, ctl, actionType)
            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                ctl.TextValue = dr.Item("DEST_CD").ToString()
                Dim destNm As String = dr.Item("DEST_NM").ToString()
                Dim jis As String = dr.Item("JIS").ToString()

                Select Case ctl.Name

                    Case .txtShipCd.Name

                        .lblShipNm.TextValue = destNm

                    Case .txtOrigCd.Name

                        .lblOrigNm.TextValue = destNm
                        .lblOrigJisCd.TextValue = jis

                    Case .txtDestCd.Name

                        .lblDestNm.TextValue = destNm
                        .lblDestJisCd.TextValue = jis
                        .lblZipNo.TextValue = dr.Item("ZIP").ToString()
                        .lblDestAdd1.TextValue = dr.Item("AD_1").ToString()
                        .lblDestAdd2.TextValue = dr.Item("AD_2").ToString()
                        'START YANAI 要望番号1319 届け先のPOPから住所３が下ってこない
                        .txtDestAdd3.TextValue = dr.Item("AD_3").ToString()
                        'END YANAI 要望番号1319 届け先のPOPから住所３が下ってこない

                        .txtTel.TextValue = dr.Item("TEL").ToString()       'ADD 2018/05/14 依頼番号 001545 

                End Select

                Return True

            End If

            Return False

        End With

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMF020F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMF020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("DEST_CD") = ctl.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            'START YANAI 要望番号376
            .Item("RELATION_SHOW_FLG") = LMConst.FLG.ON
            'END YANAI 要望番号376
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' エリアPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnAriaPop(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowAriaPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ090C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtAreaCd.TextValue = dr.Item("AREA_CD").ToString()
                .lblAreaNm.TextValue = dr.Item("AREA_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' エリアマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowAriaPopup(ByVal frm As LMF020F, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ090DS()
        Dim dt As DataTable = ds.Tables(LMZ090C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()

            'START SHINOHARA 要望番号513
            If actionType = LMF020C.ActionType.ENTER Then
                .Item("AREA_CD") = frm.txtAreaCd.TextValue
                .Item("BIN_KB") = frm.cmbBinKbn.SelectedValue.ToString()
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ090", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 商品Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMF020F, ByVal rowNo As Integer, ByVal actionType As LMF020C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, rowNo, actionType)

        '解除しないと制御できない
        '処理終了アクション
        Call Me.EndAction(frm)
        If prm.ReturnFlg = True Then

            Call Me._G.SetGoodsInfo(prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0), rowNo)
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMF020F, ByVal rowNo As Integer, ByVal actionType As LMF020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMF020C.ActionType.ENTER Then
                .Item("GOODS_NM_1") = Me._LMFconG.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMF020G.sprDetailDef.GOODS_NM.ColNo))
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 保存処理時のLeave処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SaveLeaveAction(ByVal frm As LMF020F, ByVal objNm As String) As Boolean

        'フォーカス位置判定
        Select Case objNm

            Case frm.txtDestCd.Name

                Call Me.SetTariffData(frm, False)

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMF020F)

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
    Private Sub EndAction(ByVal frm As LMF020F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 前回の値を保持
    ''' </summary>
    ''' <param name="value">更新する値</param>
    ''' <remarks></remarks>
    Private Sub UpdatePreInputData(ByVal value As String)
        Me._PreInputData = value
    End Sub

    ''' <summary>
    ''' Enterイベント共通前処理
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <returns>Falseの場合、フォームにない</returns>
    ''' <remarks></remarks>
    Private Function BeforeEnterAction(ByVal ctl As Control) As Boolean

        Dim frm As Form = TryCast(ctl.FindForm, Form)

        If frm Is Nothing = True Then

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 割増備考の設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetExtcRemData(ByVal frm As LMF020F)

        With frm

            '値がない場合、スルー
            Dim extcCd As String = .txtExtcTariffCd.TextValue
            If String.IsNullOrEmpty(extcCd) = True Then
                Exit Sub
            End If

            'キャッシュ検索
            Dim drs As DataRow() = Me._LMFconG.SelectExtcUnchinListDataRow(.cmbEigyo.SelectedValue.ToString(), extcCd, String.Empty)

            '取得できない場合、終了
            If drs.Length < 1 Then
                Exit Sub
            End If

            '値設定
            .lblExtcTariffRem.TextValue = drs(0).Item("EXTC_TARIFF_REM").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 処理開始判定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:処理を開始　False:処理を終了</returns>
    ''' <remarks></remarks>
    Private Function ChkActionStart(ByVal frm As LMF020F) As Boolean

        '参照の場合、スルー
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
            Return False
        End If

        '元データ区分 <> 運送の場合、スルー
        If LMFControlC.MOTO_DATA_UNSO.Equals(frm.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' Enterイベントによる計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function EnterCalcData(ByVal frm As LMF020F, ByVal objNm As String) As Boolean

        'スプレッドにフォーカスが無い場合、スルー
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        If spr.Name.Equals(objNm) = False Then
            Return True
        End If

        With spr.ActiveSheet

            '行がない場合、スルー
            If .Rows.Count < 1 Then
                Return True
            End If

            Dim cell As FarPoint.Win.Spread.Cell = .ActiveCell
            Dim colNo As Integer = cell.Column.Index

            Select Case colNo

                Case LMF020G.sprDetailDef.JURYO.ColNo _
                   , LMF020G.sprDetailDef.UNSO_KOSU.ColNo _
                   , LMF020G.sprDetailDef.HASU.ColNo _
                   , LMF020G.sprDetailDef.IRIME.ColNo _
                   , LMF020G.sprDetailDef.KONPO_KOSU.ColNo


                    Return Me.CalculationRowData(spr, cell.Row.Index)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UnsoSaveData(ByVal frm As LMF020F, ByVal ds As DataSet) As Boolean

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Dim actionId As String = LMF020C.ACTION_ID_INIT_SAVE
        If RecordStatus.NOMAL_REC.Equals(frm.lblSituation.RecordStatus) = True Then
            actionId = LMF020C.ACTION_ID_EDIT_SAVE
        End If

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, actionId, rtnDs) = False Then
            Return False
        End If

        '値の設定
        Me._Ds = rtnDs

        Return True

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal frm As LMF020F, ByVal ds As DataSet) As Boolean

        Return Me.ActionData(frm, ds, LMF020C.ACTION_ID_HAITA_CHK)

    End Function

    ''' <summary>
    ''' 運行データキャンセル可否チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetTripStatusData(ByVal frm As LMF020F, ByVal ds As DataSet) As Boolean

        If frm.lblUnkoNo.TextValue = String.Empty Then

            Return True

        Else

            'パラメータ設定
            Dim ds10 As DataSet = New LMF010DS()
            Dim dt As DataTable = ds10.Tables(LMF010C.TABLE_NM_ITEM)
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr.Item("TRIP_NO") = frm.lblUnkoNo.TextValue
            dt.Rows.Add(dr)

            'エラーがある場合、終了
            Dim rtnDs As DataSet = Nothing
            If Me.ActionData(frm, ds10, LMF020C.ACTION_ID_CANCEL_DATA, rtnDs) = False Then
                Return False
            End If

            Return True

        End If

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal frm As LMF020F, ByVal ds As DataSet) As Boolean

        Return Me.ActionData(frm, ds, LMF020C.ACTION_ID_DELETE)

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal frm As LMF020F, ByVal ds As DataSet) As Boolean

        '印刷種別を設定
        Dim dr As DataRow = ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0)
        dr.Item("PRINT_KB") = frm.cmbPrint.SelectedValue.ToString()

        If (LMF020C.PRINT_NIFUDA).Equals(frm.cmbPrint.SelectedValue) = True Then
            dr.Item("PRT_NB") = Convert.ToInt32(frm.numPrtCnt_To.Value) - Convert.ToInt32(frm.numPrtCnt_From.Value) + 1
            dr.Item("PRT_NB_From") = frm.numPrtCnt_From.Value
            dr.Item("PRT_NB_To") = frm.numPrtCnt_To.Value
#If True Then   'ADD 2018/11/21 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能
        ElseIf (LMF020C.PRINT_ALL).Equals(frm.cmbPrint.SelectedValue) = True Then
            dr.Item("PRT_NB") = frm.numPrtCnt.Value
            dr.Item("PRT_NB_From") = "1"
            dr.Item("PRT_NB_To") = frm.numUnsoPkgCnt.TextValue
#If True Then    'ADD 2019/06/10 005795【LMS】運送メニュー日陸便の場合、一括印刷で荷札印刷しない

            'キャッシュ(運送会社M)から値取得
            Dim drs As DataRow() = Me._LMFconG.SelectUnsocoListDataRow(frm.cmbEigyo.SelectedValue.ToString.Trim, _
                                                                      frm.txtUnsocoCd.TextValue.ToString.Trim, _
                                                                      frm.txtUnsocoBrCd.TextValue.ToString.Trim _
                                                                      )

            If drs.Length > 0 Then
                dr.Item("NIHUDA_FLAG") = drs(0).Item("NIHUDA_YN").ToString()
            Else
                dr.Item("NIHUDA_FLAG") = "00"
            End If

#End If

#End If
        Else
            dr.Item("PRT_NB") = frm.numPrtCnt.Value
            dr.Item("PRT_NB_From") = "1"
            dr.Item("PRT_NB_To") = "1"
        End If

#If True Then   'ADD 2021/01/21026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
        dr.Item("MOTO_DATA_KB") = frm.cmbMotoDataKbn.TextValue '名称を設定
        dr.Item("INOUTKA_NO_L") = frm.lblKanriNo.TextValue
#End If
        'エラーの場合、終了
        Dim rtnDs As DataSet = Nothing
        If Me.ActionData(frm, ds, LMF020C.ACTION_ID_PRINT_ONLY, rtnDs) = False Then
            Return False
        End If

        'プレビュー表示
        Call Me._LMFconH.ShowPreviewData(rtnDs)

        Return True

    End Function

    ''' <summary>
    ''' サーバアクセス(チェック有)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <param name="rtnDs">戻りDataSet 初期値 = Nothing</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ActionData(ByVal frm As LMF020F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        'サーバアクセス
        rtnDs = Me.ServerAccess(ds, actionId)

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' サーバアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ServerAccess(ByVal ds As DataSet, ByVal actionStr As String) As DataSet

        ds = Me._LMFconH.ServerAccess(ds, actionStr)

        '(請求)運賃テーブルを設定
        Dim dt As DataTable = ds.Tables(LMF800C.TABLE_NM_UNCHIN)

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        '(支払)運賃テーブルを設定
        Dim dtS As DataTable = ds.Tables(LMF810C.TABLE_NM_SHIHARAI)
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        '値がある場合初期化
        If dt Is Nothing = False Then
            dt.Clear()
        End If

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        '値がある場合初期化
        If dtS Is Nothing = False Then
            dtS.Clear()
        End If
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        Return ds

    End Function

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMF020F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMF020F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetData(ByVal frm As LMF020F, ByVal ds As DataSet) As Boolean

        '運送(大)データの設定
        ds = Me.SetUnsoLData(frm, ds)

        '運送(中)データの設定
        ds = Me.SetUnsoMData(frm, ds)

        '支払いデータの設定
        ds = Me.SetShiharaiData(frm, ds)

        '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen upd start
        Return _G.SetTollOkurijyoTblDataSet(ds)
        'Return True
        '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen upd end

    End Function

    ''' <summary>
    ''' 運送(大)データの設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoLData(ByVal frm As LMF020F, ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0)
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("YUSO_BR_CD") = .cmbYosoEigyo.SelectedValue
            dr.Item("UNSO_NO_L") = .lblUnsoNo.TextValue
            dr.Item("INOUTKA_NO_L") = .lblKanriNo.TextValue
            dr.Item("MOTO_DATA_KB") = .cmbMotoDataKbn.SelectedValue
            dr.Item("JIYU_KB") = .cmbUnsoJiyuKbn.SelectedValue
            dr.Item("PC_KB") = .cmbPcKbn.SelectedValue
            dr.Item("TAX_KB") = .cmbTax.SelectedValue
            dr.Item("TRIP_NO") = .lblUnkoNo.TextValue
            dr.Item("UNSO_TEHAI_KB") = .cmbTehaiKbn.SelectedValue
            dr.Item("BIN_KB") = .cmbBinKbn.SelectedValue
            dr.Item("TARIFF_BUNRUI_KB") = .cmbTariffKbn.SelectedValue
            dr.Item("VCLE_KB") = .cmbSharyoKbn.SelectedValue
            dr.Item("UNSO_CD") = .txtUnsocoCd.TextValue
            dr.Item("UNSO_BR_CD") = .txtUnsocoBrCd.TextValue
            dr.Item("UNSO_NM") = .lblUnsoNm.TextValue
            dr.Item("UNSO_BR_NM") = .lblUnsoBrNm.TextValue
            dr.Item("TARE_YN") = .lblTareYn.TextValue
            dr.Item("DENP_NO") = .txtOkuriNo.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
            dr.Item("CUST_NM_M") = .lblCustNmM.TextValue
            '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End
            dr.Item("CUST_REF_NO") = .txtOrdNo.TextValue
            dr.Item("SHIP_CD") = .txtShipCd.TextValue
            'START YANAI 要望番号376
            dr.Item("SHIP_NM") = .lblShipNm.TextValue
            'END YANAI 要望番号376
            dr.Item("BUY_CHU_NO") = .txtBuyerOrdNo.TextValue
            dr.Item("SEIQ_TARIFF_CD") = .txtTariffCd.TextValue
            dr.Item("TARIFF_REM") = .lblTariffRem.TextValue
            dr.Item("SEIQ_ETARIFF_CD") = .txtExtcTariffCd.TextValue
            dr.Item("EXTC_TARIFF_REM") = .lblExtcTariffRem.TextValue
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            dr.Item("SHIHARAI_TARIFF_CD") = .txtPayTariffCd.TextValue
            dr.Item("SHIHARAI_TARIFF_REM") = .lblPayTariffRem.TextValue
            dr.Item("SHIHARAI_ETARIFF_CD") = .txtPayExtcTariffCd.TextValue
            dr.Item("SHIHARAI_EXTC_TARIFF_REM") = .lblPayExtcTariffRem.TextValue
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            dr.Item("OUTKA_PLAN_DATE") = .imdOrigDate.TextValue
            dr.Item("OUTKA_PLAN_TIME") = .txtOrigTime.TextValue
            dr.Item("ORIG_CD") = .txtOrigCd.TextValue
            dr.Item("ORIG_NM") = .lblOrigNm.TextValue
            dr.Item("ORIG_JIS_CD") = .lblOrigJisCd.TextValue
            dr.Item("ARR_PLAN_DATE") = .imdDestDate.TextValue
            dr.Item("ARR_PLAN_TIME") = .txtDestTime.TextValue
            dr.Item("ARR_ACT_TIME") = .txtJiDestTime.TextValue
            '要望番号:2408 2015.09.17 追加START
            Me._G.GetAutoDenpSet(frm)
            dr.Item("AUTO_DENP_KBN") = .cmbAutoDenpKbn.SelectedValue
            dr.Item("AUTO_DENP_NO") = .lblAutoDenpNo.TextValue
            '要望番号:2408 2015.09.17 追加END
            dr.Item("DEST_CD") = .txtDestCd.TextValue
            dr.Item("DEST_NM") = .lblDestNm.TextValue
            dr.Item("DEST_JIS_CD") = .lblDestJisCd.TextValue
            dr.Item("ZIP") = .lblZipNo.TextValue
            dr.Item("AD_1") = .lblDestAdd1.TextValue
            dr.Item("AD_2") = .lblDestAdd2.TextValue
            dr.Item("AD_3") = .txtDestAdd3.TextValue
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
            dr.Item("TEL") = .txtTel.TextValue
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end
            dr.Item("AREA_CD") = .txtAreaCd.TextValue
            dr.Item("AREA_NM") = .lblAreaNm.TextValue
            dr.Item("UNSO_PKG_NB") = .numUnsoPkgCnt.Value
            dr.Item("UNSO_WT") = .numUnsoWtL.Value
            dr.Item("NB_UT") = .cmbUnsoCntUt.SelectedValue
            dr.Item("UNSO_ONDO_KB") = .cmbThermoKbn.SelectedValue
            dr.Item("REMARK") = .txtUnsoComment.TextValue
            dr.Item("NHS_REMARK") = .txtRemark.TextValue

        End With

        Return ds

    End Function

    ''' <summary>
    ''' 運送(中)データの設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoMData(ByVal frm As LMF020F, ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF020C.TABLE_NM_UNSO_M)
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim max As Integer = spr.Rows.Count - 1
        Dim recNo As Integer = 0

        With spr

            For i As Integer = 0 To max

                '行番号を特定
                recNo = Convert.ToInt32(Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.REC_NO.ColNo)))

                '値設定
                dt.Rows(recNo).Item("GOODS_CD_NRS") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.GOODS_CD.ColNo))
                dt.Rows(recNo).Item("PRINT_SORT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.PRT_ORDER.ColNo))      'ADD 20187/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
                '要望対応:1816 yamanaka 2013.02.22 Start
                dt.Rows(recNo).Item("GOODS_CD_CUST") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.GOODS_CD_CUST.ColNo))
                '要望対応:1816 yamanaka 2013.02.22 End
                dt.Rows(recNo).Item("GOODS_NM") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.GOODS_NM.ColNo))
                dt.Rows(recNo).Item("LOT_NO") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.LOT_NO.ColNo)).ToUpper()
                dt.Rows(recNo).Item("BETU_WT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.JURYO.ColNo))
                dt.Rows(recNo).Item("UNSO_TTL_NB") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.UNSO_KOSU.ColNo))
                dt.Rows(recNo).Item("NB_UT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.KOSU_TANI.ColNo))
                dt.Rows(recNo).Item("UNSO_TTL_QT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.UNSO_SURYO.ColNo))
                dt.Rows(recNo).Item("QT_UT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.SURYO_TANI.ColNo))
                dt.Rows(recNo).Item("HASU") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.HASU.ColNo))
                dt.Rows(recNo).Item("ZAI_REC_NO") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.ZAI_REC_NO.ColNo))
                dt.Rows(recNo).Item("UNSO_ONDO_KB") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.ONDO_KANRI.ColNo))
                dt.Rows(recNo).Item("IRIME") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.IRIME.ColNo))
                dt.Rows(recNo).Item("IRIME_UT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.IRIME_TANI.ColNo))
                dt.Rows(recNo).Item("REMARK") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.REMARK.ColNo))
                dt.Rows(recNo).Item("SIZE_KB") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.SIZE.ColNo))
                dt.Rows(recNo).Item("PKG_NB") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.KONPO_KOSU.ColNo))
                dt.Rows(recNo).Item("ZBUKA_CD") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.ZAI_BUKA.ColNo))
                dt.Rows(recNo).Item("ABUKA_CD") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.HOKA_BUKA.ColNo))
                dt.Rows(recNo).Item("CALC_FLG") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.CALC_FLG.ColNo))

                dt.Rows(recNo).Item("STD_WT_KGS") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.STD_WT_KGS.ColNo))
                dt.Rows(recNo).Item("NB_UT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.KOSU_TANI.ColNo))
                dt.Rows(recNo).Item("QT_UT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.SURYO_TANI.ColNo))
                dt.Rows(recNo).Item("STD_IRIME_NB") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.STD_IRIME_NB.ColNo))
                dt.Rows(recNo).Item("IRIME_UT") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.IRIME_TANI.ColNo))
                dt.Rows(recNo).Item("PKG_NB") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.KONPO_KOSU.ColNo))
                dt.Rows(recNo).Item("TARE_YN") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.TARE_YN.ColNo))
                dt.Rows(recNo).Item("UNSO_HOKEN_UM") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.UNSO_HOKEN_UM.ColNo))     'ADD 2022/01/21 026832
                dt.Rows(recNo).Item("KITAKU_GOODS_UP") = Me._LMFconG.GetCellValue(.Cells(i, LMF020G.sprDetailDef.KITAKU_GOODS_UP.ColNo))     'ADD 2022/01/12 026832

            Next

        End With

        Return ds

    End Function

    ''' <summary>
    ''' 支払いデータの設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiData(ByVal frm As LMF020F, ByVal ds As DataSet) As DataSet

        ds.Tables(LMF020C.TABLE_NM_SHIHARAI).Clear()
        Dim dt As DataTable = ds.Tables(LMF020C.TABLE_NM_SHIHARAI)
        Dim dr As DataRow = dt.NewRow()
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("UNSO_NO_L") = .lblUnsoNo.TextValue
            dr.Item("FLAG_CNT") = LMConst.FLG.OFF
            dr.Item("GROPU_CNT") = LMConst.FLG.OFF
            dr.Item("UNSO_WT") = .numUnsoWtL.Value
            dr.Item("DECI_KYORI") = .numPaySeiqTariffDes.TextValue
            dr.Item("DECI_WT") = .numPayUnsoWt.TextValue
            dr.Item("DECI_UNCHIN") = .numPayPayUnchin.TextValue
            dr.Item("DECI_CITY_EXTC") = .numPayCityExtc.TextValue
            dr.Item("DECI_WINT_EXTC") = .numPayWintExtc.TextValue
            dr.Item("DECI_RELY_EXTC") = .numPayRelyExtc.TextValue
            dr.Item("DECI_TOLL") = .numPayPassExtc.TextValue
            dr.Item("DECI_INSU") = .numPayInsurExtc.TextValue

            dt.Rows.Add(dr)

        End With

        Return ds

    End Function

    ''' <summary>
    ''' 運送(中)行追加
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoMInitData(ByVal frm As LMF020F, ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF020C.TABLE_NM_UNSO_M)
        Dim dr As DataRow = dt.NewRow()

        With dr

            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("UNSO_NO_L") = frm.lblUnsoNo.TextValue
            .Item("UNSO_NO_M") = String.Empty
            .Item("PRINT_SORT") = "99"              'ADD 20187/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
            .Item("GOODS_CD_NRS") = String.Empty
            '要望対応:1816 yamanaka 2013.02.22 Start
            .Item("GOODS_CD_CUST") = String.Empty
            '要望対応:1816 yamanaka 2013.02.22 End
            .Item("GOODS_NM") = String.Empty
            .Item("LOT_NO") = String.Empty
            .Item("BETU_WT") = 0
            .Item("UNSO_TTL_NB") = 0
            .Item("NB_UT") = String.Empty
            .Item("UNSO_TTL_QT") = 0
            .Item("QT_UT") = String.Empty
            .Item("HASU") = 0
            .Item("ZAI_REC_NO") = String.Empty
            .Item("UNSO_ONDO_KB") = String.Empty
            .Item("IRIME") = 0
            .Item("IRIME_UT") = String.Empty
            .Item("REMARK") = String.Empty
            'START YANAI 要望番号1259 複写時の初期値変更
            '.Item("PKG_NB") = 0
            .Item("PKG_NB") = 1
            'END YANAI 要望番号1259 複写時の初期値変更
            .Item("SIZE_KB") = String.Empty
            .Item("ZBUKA_CD") = String.Empty
            .Item("ABUKA_CD") = String.Empty
            .Item("SYS_UPD_DATE") = String.Empty
            .Item("SYS_UPD_TIME") = String.Empty
            .Item("STD_IRIME_NB") = 0
            .Item("STD_WT_KGS") = 0
            .Item("TARE_YN") = LMFControlC.FLG_OFF
            .Item("CALC_FLG") = LMConst.FLG.OFF
            .Item("UNSO_HOKEN_UM") = "00"     'ADD 2022/01/21 026832
            .Item("KITAKU_GOODS_UP") = 0     'ADD 2022/01/12 026832
        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' コピーデータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetCopyData(ByVal ds As DataSet) As DataSet

        '運賃テーブルのクリア
        ds.Tables(LMF800C.TABLE_NM_UNCHIN).Clear()

        '運送(大)
        Dim unsoLDr As DataRow = ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0)
        With unsoLDr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = ds.Tables(LMF020C.TABLE_NM_UNSO_L)(0).Item("NRS_BR_CD")
            .Item("UNSO_NO_L") = String.Empty
            .Item("TRIP_NO") = String.Empty
            .Item("TRIP_NO_TYUKEI") = String.Empty
            .Item("INOUTKA_NO_L") = String.Empty
            .Item("MOTO_DATA_KB") = LMFControlC.MOTO_DATA_UNSO
            .Item("DENP_NO") = String.Empty
            .Item("CUST_REF_NO") = String.Empty
            .Item("BUY_CHU_NO") = String.Empty
            '(2012.11.29)要望番号1630 初期化しない --- START ---
            'Dim sysDate As String = MyBase.GetSystemDateTime(0)
            '.Item("OUTKA_PLAN_DATE") = sysDate
            '.Item("OUTKA_PLAN_TIME") = String.Empty
            '.Item("ARR_PLAN_DATE") = Convert.ToDateTime(DateFormatUtility.EditSlash(sysDate)).AddDays(1).ToString(LMFControlC.DATE_YYYYMMDD)
            '.Item("ARR_PLAN_TIME") = String.Empty
            '.Item("ARR_ACT_TIME") = String.Empty
            '(2012.11.29)要望番号1630 初期化しない ---  END  ---
            .Item("UNSO_PKG_NB") = 0
            .Item("UNSO_WT") = 0

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue

            ' 自動送り状番号は複写しない
            .Item("AUTO_DENP_NO") = String.Empty
#End If

        End With

        '運送(中)
        Dim unsoMDt As DataTable = ds.Tables(LMF020C.TABLE_NM_UNSO_M)
        Dim max As Integer = unsoMDt.Rows.Count - 1
        For i As Integer = 0 To max

            With unsoMDt.Rows(i)

                .Item("ZAI_REC_NO") = String.Empty

            End With

        Next

        '料金情報
        Dim infoDt As DataTable = ds.Tables(LMF020C.TABLE_NM_INFO)
        If 0 < infoDt.Rows.Count Then

            Dim infoDr As DataRow = infoDt.Rows(0)
            Dim colMax As Integer = infoDt.Columns.Count - 1

            '(2012.11.29)要望番号1630 max→colMax --- START ---
            'For i As Integer = 0 To max
            '    infoDr.Item(i) = 0
            'Next
            For i As Integer = 0 To colMax
                infoDr.Item(i) = 0
            Next
            '(2012.11.29)要望番号1630 max→colMax ---  END  ---

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'infoDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            infoDr.Item("NRS_BR_CD") = ds.Tables(LMF020C.TABLE_NM_INFO)(0).Item("NRS_BR_CD")
            infoDr.Item("UNSO_NO_L") = String.Empty

            'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
            infoDr.Item("FLAG_CNT") = LMConst.FLG.OFF
            infoDr.Item("GROPU_CNT") = LMConst.FLG.OFF
            'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

        End If

        Return ds

    End Function

    ''' <summary>
    ''' LMF800DSを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinCalcDataSet(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = New LMF800DS()
        Dim tblNm As String = String.Empty
        Dim max As Integer = setDs.Tables.Count - 1
        Dim cnt As Integer = ds.Tables.Count - 1
        Dim setFlg As Boolean = True
        For i As Integer = 0 To max

            setFlg = True
            tblNm = setDs.Tables(i).TableName

            For j As Integer = 0 To cnt

                '同じ名前のものは追加しない
                If tblNm.Equals(ds.Tables(j).TableName) = True Then
                    setFlg = False
                    Exit For
                End If

            Next

            '違うテーブルの場合
            If setFlg = True Then

                'テーブル追加
                ds = Me._LMFconH.SetBetuDataTable(ds, setDs, tblNm)

            End If

        Next

        Return ds

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' LMF810DSを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiCalcDataSet(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = New LMF810DS()
        Dim tblNm As String = String.Empty
        Dim max As Integer = setDs.Tables.Count - 1
        Dim cnt As Integer = ds.Tables.Count - 1
        Dim setFlg As Boolean = True
        For i As Integer = 0 To max

            setFlg = True
            tblNm = setDs.Tables(i).TableName

            For j As Integer = 0 To cnt

                '同じ名前のものは追加しない
                If tblNm.Equals(ds.Tables(j).TableName) = True Then
                    setFlg = False
                    Exit For
                End If

            Next

            '違うテーブルの場合
            If setFlg = True Then

                'テーブル追加
                ds = Me._LMFconH.SetBetuDataTable(ds, setDs, tblNm)

            End If

        Next

        Return ds

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.NewUnsoData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ShiftEditMode(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し(複写)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
        'Call Me.ShiftCopyMode(frm)
        Call Me.ShiftCopyMode(frm, LMF020C.ActionType.COPY)
        'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.DeleteAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveDestMaster(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveUnsoItemData(frm, LMF020C.ActionType.SAVE)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMF020F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' 行追加ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnAdd_Click(ByVal frm As LMF020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.AddUnsoMData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 行削除ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnDel_Click(ByVal frm As LMF020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.DelUnsoMData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 計算ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnKeisan_Click(ByVal frm As LMF020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SetCalcData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByVal frm As LMF020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.PrintAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 値変更時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbTariffKbn_SelectedValueChanged(ByVal frm As LMF020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ChangedTariffKbn(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' ロストフォーカス時に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtDestCd_Leave(ByVal frm As LMF020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SetTariffData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 値編集の直前に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtDestCd_Enter(ByVal frm As LMF020F, ByVal e As System.EventArgs)

        'コントロールがない場合、終了
        If Me.BeforeEnterAction(frm) = False Then
            Exit Sub
        End If
        Me.UpdatePreInputData(frm.txtDestCd.TextValue)

    End Sub

    ''' <summary>
    ''' フォームのボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub LMF020F_KeyDown(ByVal frm As LMF020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_Change(ByVal frm As LMF020F, ByVal e As FarPoint.Win.Spread.ChangeEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CellValueChange(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社コードの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub txtUnsocoCd_Leave(ByRef frm As LMF020F)

        Logger.StartLog(Me.GetType.Name, "txtUnsocoCd_Leave")

        If (frm.txtUnsocoCd.TextValue).Equals(frm.txtUnsocoCdOld.TextValue) = False OrElse _
            (frm.txtUnsocoBrCd.TextValue).Equals(frm.txtUnsocoBrCdOld.TextValue) = False Then
            Call Me.UnsocoCdLeave(frm)
        End If

        '要望番号:2408 2015.09.17 追加START
        '運賃会社情報から自動送状区分を設定
        Call Me._G.GetAutoDenpSet(frm)
        '要望番号:2408 2015.09.17 追加START

        Logger.EndLog(Me.GetType.Name, "txtUnsocoCd_Leave")

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    '要望対応:1816 yamanaka 2013.02.22 Start
    ''' <summary>
    ''' 印刷種別変更時の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub CmbPrint_SelectedValueChanged(ByRef frm As LMF020F)

        Logger.StartLog(Me.GetType.Name, "CmbPrint_SelectedValueChanged")

        Call Me.ChangedCmbPrint(frm)

        Logger.EndLog(Me.GetType.Name, "CmbPrint_SelectedValueChanged")

    End Sub
    '要望対応:1816 yamanaka 2013.02.22 End

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class