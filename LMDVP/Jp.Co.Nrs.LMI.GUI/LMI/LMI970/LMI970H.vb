' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI970H : 運賃データ入力・確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI970ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI970H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI970V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI970G

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
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMIControlH

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
        Dim frm As LMI970F = New LMI970F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Hnadler共通クラスの設定
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI970V(Me, frm, Me._LMFconV, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMI970G(Me, frm, Me._LMFconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(DispMode.INIT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '初期値設定
        Call Me._G.SetInitValue()

        'メッセージの表示
        Call Me.SetInitMessage(frm)

        'フォームの表示
        frm.Show()

        'フォーカスの設定
        Call Me._G.SetFoucus(DispMode.INIT)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMI970F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI970C.ActionType.KENSAKU)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI970C.EventShubetsu.KENSAKU)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        If IsDispMode(frm, DispMode.EDIT) Then
            '編集中の場合、確認メッセージ
            If MyBase.ShowMessage(frm, "W158") = MsgBoxResult.Cancel Then
                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub
            End If
        End If

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        MyBase.SetLimitCount(Me._LMFconG.GetLimitData())

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)


        '検索条件の設定
        Dim ds As DataSet = Me.SetConditionDataSet(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMI970C.ACTION_ID_SELECT, ds)

        '通常検索の場合
        Dim count As String = String.Empty
        count = MyBase.GetResultCount.ToString()

        '検索処理
        rtnDs = Me.SelectListData(frm, ds, rtnDs, blf, count)
        If rtnDs Is Nothing = True Then
            Exit Sub
        End If

        '値の設定
        Me._G.SetSpread(rtnDs) 

        '色の設定
        Call Me._G.SetSpreadColor()

        '処理終了アクション
        Call Me.EndAction(frm)

        'ファンクションキーの設定
        If Convert.ToInt32(count) > 0 Then
            Call Me._G.SetFunctionKey(DispMode.VIEW)
        Else
            Call Me._G.SetFunctionKey(DispMode.INIT)
        End If

        'ヘッダのコントロールの設定
        _G.SetHeaderControls(DispMode.VIEW)

        'スプレッドの設定
        _G.SetSpreadLock(True)

        'フォーカスの設定
        Call Me._G.SetFoucus(DispMode.VIEW)

        'モード設定
        frm.lblDispMode.Text = DispMode.VIEW

    End Sub

    ''' <summary>
    ''' 明細データ編集処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub EditListData(ByVal frm As LMI970F)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI970C.ActionType.SAVE)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'ファンクションキーの設定
        _G.SetFunctionKey(DispMode.EDIT)

        'ヘッダのコントロールの設定
        _G.SetHeaderControls(DispMode.EDIT)

        'スプレッドの設定
        _G.SetSpreadLock(False)

        'フォーカスの設定
        Call Me._G.SetFoucus(DispMode.EDIT)

        'モード設定
        frm.lblDispMode.Text = DispMode.EDIT

    End Sub

    ''' <summary>
    ''' 明細データ保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SaveListData(ByVal frm As LMI970F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI970C.ActionType.SAVE)

        '営業所必須
        rtnResult = rtnResult AndAlso _V.IsInputted(frm.cmbEigyo, frm.lblTitleEigyo.Text)

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, frm.FunctionKey.F11ButtonName)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = Me.SetConditionDataSet(frm)
        rtnResult = rtnResult AndAlso Me.SetUpdateYusoIraiData(frm, ds)

        '保存処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, frm.FunctionKey.F11ButtonName)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F11ButtonName))

        '処理終了アクション
        Call Me.EndAction(frm)

        'ファンクションキーの設定
        _G.SetFunctionKey(DispMode.VIEW)

        'ヘッダのコントロールの設定
        _G.SetHeaderControls(DispMode.VIEW)

        'スプレッドの設定
        _G.SetSpreadLock(True)

        'フォーカスの設定
        Call Me._G.SetFoucus(DispMode.VIEW)

        'モード設定
        frm.lblDispMode.Text = DispMode.VIEW

        If rtnResult Then   'ADD 2019/05/30 要望管理006030

            '検索処理
            Call Me.SelectListData(frm)

            'ADD START 2019/05/30 要望管理006030
            MyBase.ShowMessage(frm, "G002", {"保存処理", ""})

        End If
        'ADD END   2019/05/30 要望管理006030

    End Sub

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JissekiSakusei(ByVal frm As LMI970F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI970C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI970C.EventShubetsu.JISSEKI_SAKUSEI)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI970G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsTargetValid(frm, arr)

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, frm.FunctionKey.F1ButtonName)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = Me.SetConditionDataSet(frm)
        rtnResult = rtnResult AndAlso Me.SetSendUnchinTarget(frm, ds, arr)

        '保存処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, frm.FunctionKey.F1ButtonName)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F1ButtonName))

        '一覧の更新
        rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr, LMI970C.EventShubetsu.JISSEKI_SAKUSEI)  'MOD 2019/05/30 引数eventShubetsu追加

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus(DispMode.VIEW)

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnPrintClick(ByVal frm As LMI970F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI970C.ActionType.PRINT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI970C.EventShubetsu.PRINT)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI970G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, frm.btnPrint.Text)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = Me.SetConditionDataSet(frm)
        rtnResult = rtnResult AndAlso Me.SetPrintTarget(frm, ds, arr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

        'サーバサイド処理
        Dim rtnDs As DataSet = Nothing
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, frm.btnPrint.Name, rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintData")

        '正常終了の場合
        If rtnResult = True Then

            '終了メッセージ表示
            MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})

            Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)

            If prevDt.Rows.Count > 0 Then

                'プレビューの生成 
                Dim prevFrm As New RDViewer()

                'データ設定 
                prevFrm.DataSource = prevDt

                'プレビュー処理の開始 
                prevFrm.Run()

                'プレビューフォームの表示 
                prevFrm.Show()

            End If

        End If

        '一覧の更新
        Me._G.SetUpdSpread(frm, arr, LMI970C.EventShubetsu.PRINT)  'ADD 2019/05/30 要望管理006030

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    'ADD START 2019/05/30 要望管理006030
    ''' <summary>
    ''' 単価更新処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub UpdateTanka(ByVal frm As LMI970F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI970C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI970C.EventShubetsu.UPDATE_TANKA)


        '確認メッセージ表示
        Dim sMsg As String = "出荷日が" + DateFormatUtility.EditSlash(Left(frm.imdTargetYM.TextValue, 6)) + "で未送信である全データの単価を更新"
        rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, sMsg)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = Me.SetConditionDataSet(frm)

        '保存処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, frm.btnUpdateTanka.Name)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.btnUpdateTanka.Name))

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus(DispMode.VIEW)

        If rtnResult Then

            '検索処理
            Call Me.SelectListData(frm)
            'メッセージの設定
            MyBase.ShowMessage(frm, "G002", {"更新処理", ""})

        End If


    End Sub

    ''' <summary>
    ''' 明細項目自動更新処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="spr"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UpdateMeisaiItem(ByVal frm As LMI970F, ByVal spr As Win.Spread.LMSpread, ByVal e As FarPoint.Win.Spread.ChangeEventArgs)

        '重量を変更した場合
        If e.Column = LMI970C.SprColumnIndex.JURYO Then

            With spr.ActiveSheet

                '運賃＝重量×単価　※四捨五入して整数にする
                .Cells(e.Row, LMI970C.SprColumnIndex.UNCHIN).Value = _
                    Math.Round(CDec(.Cells(e.Row, LMI970C.SprColumnIndex.JURYO).Value) * CDec(.Cells(e.Row, LMI970C.SprColumnIndex.TANKA).Value), MidpointRounding.AwayFromZero)

            End With

        End If

    End Sub
    'ADD END   2019/05/30 要望管理006030

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMI970F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI970C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI970C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMI970C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI970F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMI970C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI970C.ActionType.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

            'メッセージ設定
            Call Me.ShowGMessage(frm)

            Exit Sub

        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMI970C.ActionType.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI970F, ByVal e As FormClosingEventArgs) As Boolean

        If IsDispMode(frm, DispMode.EDIT) Then
            '編集中の場合、確認メッセージ
            If MyBase.ShowMessage(frm, "W016") = MsgBoxResult.Cancel Then
                e.Cancel = True
                Exit Function
            End If
        End If

    End Function

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMI970F, ByVal objNm As String, ByVal actionType As LMI970C.ActionType) As Boolean

        With frm
            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtCustCdL.Name

                    Call Me.SetReturnCustPop(frm, objNm, actionType)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMI970F, ByVal objNm As String, ByVal actionType As LMI970C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, objNm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                Select Case objNm

                    Case .txtCustCdL.Name

                        .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                        .lblCustNm.TextValue = String.Concat(dr.Item("CUST_NM_L").ToString())

                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMI970F, ByVal objNm As String, ByVal actionType As LMI970C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim brCd As String = String.Empty
        Dim custL As String = String.Empty
        Dim custM As String = String.Empty
        Dim custS As String = String.Empty
        Dim custSS As String = String.Empty
        Dim csCd As String = String.Empty

        Select Case objNm

            Case frm.txtCustCdL.Name

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                custL = frm.txtCustCdL.TextValue
                csCd = LMConst.FLG.ON

        End Select

        With dr

            .Item("NRS_BR_CD") = brCd
            If actionType = LMI970C.ActionType.ENTER Then
                .Item("CUST_CD_L") = custL
                .Item("CUST_CD_M") = custM
                .Item("CUST_CD_S") = custS
                .Item("CUST_CD_SS") = custSS
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csCd
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

#End Region 'PopUp

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索部データ)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetConditionDataSet(ByVal frm As LMI970F) As DataSet

        Dim ds As DataSet = New LMI970DS()
        Dim dt As DataTable = ds.Tables(LMI970C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'ヘッダ項目
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("TORIKOMI_DATE_FROM") = .imdTorikomiDateFrom.TextValue
            dr.Item("TORIKOMI_DATE_TO") = .imdTorikomiDateTo.TextValue
            dr.Item("SEARCH_DATE_KB") = .cmbSearchDateKb.TextValue
            If Not String.IsNullOrEmpty(.cmbSearchDateKb.TextValue) Then
                dr.Item("SEARCH_DATE_FROM") = .imdSearchDateFrom.TextValue
                dr.Item("SEARCH_DATE_TO") = .imdSearchDateTo.TextValue
            End If
            dr.Item("PRINT_SHUBETU") = .cmbPrint.SelectedValue.ToString()
            'ADD START 2019/05/30 要望管理006030
            dr.Item("TARGET_YM") = Left(.imdTargetYM.TextValue, 6)
            dr.Item("TANKA") = .numTanka.TextValue
            'ADD END   2019/05/30 要望管理006030

        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 保存対象情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetUpdateYusoIraiData(ByVal frm As LMI970F, ByVal ds As DataSet) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1

        Dim dt As DataTable = ds.Tables(LMI970C.TABLE_NM_SENDUNCHIN_TARGET)
        Dim dr As DataRow = Nothing

        With spr.ActiveSheet

            For i As Integer = 0 To max

                '送信済みでない明細のみ更新対象
                If Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.SEND_KB.ColNo)) <> LMI970C.SendKbName.SoushinZumi Then   'ADD 2019/05/30 要望管理006030

                    'インスタンス生成
                    dr = dt.NewRow()

                    'スプレッドの値を設定
                    dr.Item("KOJO_KANRI_NO") = Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.KOJO_KANRI_NO.ColNo))
                    dr.Item("JURYO") = Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.JURYO.ColNo))    'ADD 2019/05/30 要望管理006030
                    dr.Item("UNCHIN") = Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.UNCHIN.ColNo))
                    dr.Item("CRT_DATE") = Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.CRT_DATE.ColNo))
                    dr.Item("FILE_NAME") = Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.FILE_NAME.ColNo))
                    dr.Item("REC_NO") = Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.REC_NO.ColNo))
                    dr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.SYS_UPD_DATE.ColNo))
                    dr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(i, LMI970G.sprDetailDef.SYS_UPD_TIME.ColNo))

                    '行追加
                    dt.Rows.Add(dr)

                End If  'ADD 2019/05/30 要望管理006030

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 実績作成対象情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetSendUnchinTarget(ByVal frm As LMI970F, ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim rowNo As Integer = 0

        Dim dt As DataTable = ds.Tables(LMI970C.TABLE_NM_SENDUNCHIN_TARGET)
        Dim dr As DataRow = Nothing

        With spr.ActiveSheet

            For i As Integer = 0 To max

                'インスタンス生成
                dr = dt.NewRow()

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                'スプレッドの値を設定
                dr.Item("ROW_NO") = rowNo
                dr.Item("KOJO_KANRI_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.KOJO_KANRI_NO.ColNo))
                dr.Item("CRT_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.CRT_DATE.ColNo))
                dr.Item("FILE_NAME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.FILE_NAME.ColNo))
                dr.Item("REC_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.REC_NO.ColNo))
                dr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.SYS_UPD_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.SYS_UPD_TIME.ColNo))

                '行追加
                dt.Rows.Add(dr)

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' データセット設定(印刷処理)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Function SetPrintTarget(ByVal frm As LMI970F, ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        Dim dt As DataTable = ds.Tables(LMI970C.TABLE_NM_SENDUNCHIN_TARGET)
        Dim dr As DataRow = Nothing
        Dim rowNo As Integer = 0

        With frm.sprDetail.ActiveSheet

            For i As Integer = 0 To arr.Count - 1

                'インスタンス生成
                dr = dt.NewRow()

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                'スプレッドの値を設定
                dr.Item("CRT_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.CRT_DATE.ColNo))
                dr.Item("FILE_NAME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.FILE_NAME.ColNo))
                dr.Item("REC_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.REC_NO.ColNo))
                dr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.SYS_UPD_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI970G.sprDetailDef.SYS_UPD_TIME.ColNo))
                dt.Rows.Add(dr)

            Next

        End With

        ds.Merge(New RdPrevInfoDS)

        Return True

    End Function

#End Region 'DataSet設定

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMI970F)

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
    Private Sub EndAction(ByVal frm As LMI970F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' サーバサイド処理の実行
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionTyp">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CallServerProcess(ByVal frm As LMI970F, ByVal ds As DataSet, ByVal actionTyp As String, Optional ByRef rtnDs As DataSet = Nothing) As Boolean

        Dim blfName As String = "LMI970BLF"

        Dim actionId As String = String.Empty
        Select Case actionTyp

            Case frm.FunctionKey.F1ButtonName
                actionId = LMI970C.ACTION_ID_INSERT_SENDUNCHIN

            Case frm.FunctionKey.F11ButtonName
                actionId = LMI970C.ACTION_ID_UPDATE_YUSOIRAI

            Case frm.btnPrint.Name
                actionId = LMI970C.ACTION_ID_PRINTDATA

                'ADD START 2019/05/30 要望管理006030
            Case frm.btnUpdateTanka.Name
                actionId = LMI970C.ACTION_ID_UPDATE_TANKA
                'ADD END   2019/05/30 要望管理006030

        End Select

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, blfName, actionId, rtnDs) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' サーバアクセス(チェック有)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="blfName">BLF名</param>
    ''' <param name="actionId">アクションID</param>
    ''' <param name="rtnDs">戻りDataSet 初期値 = Nothing</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ActionData(ByVal frm As LMI970F _
                                , ByVal ds As DataSet _
                                , ByVal blfName As String _
                                , ByVal actionId As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        'サーバアクセス
        rtnDs = MyBase.CallWSA(blfName, actionId, ds)

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        'エラーが保持されている場合、False
        If MyBase.IsMessageStoreExist = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="rtnDs">DataSet</param>
    ''' <param name="blf">BLF</param>
    ''' <param name="count">件数</param>
    ''' <returns>DataSet　ワーニングで「いいえ」を選択した場合、Nothing</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal frm As LMI970F _
                                    , ByVal ds As DataSet _
                                    , ByVal rtnDs As DataSet _
                                    , ByVal blf As String _
                                    , ByVal count As String _
                                    ) As DataSet

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Warningの場合
            If MyBase.IsWarningMessageExist() = True Then

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(blf, LMI970C.ACTION_ID_SELECT, ds)

                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G008", New String() {count})

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '処理終了アクション
                    Call Me.EndAction(frm)

                    Return Nothing

                End If

            Else

                'メッセージエリアの設定(0件エラー)
                MyBase.ShowMessage(frm)

            End If

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {count})

        End If

        Return rtnDs

    End Function

#End Region 'ユーティリティ

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMI970F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetInitMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' 初期メッセージ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetInitMessage(ByVal frm As LMI970F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region 'メッセージ設定

#Region "チェック"

#Region "各処理のチェック"

    ''' <summary>
    ''' DispModeチェック
    ''' </summary>
    ''' <param name="mode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDispMode(ByVal frm As LMI970F, ByVal mode As String) As Boolean

        If frm.lblDispMode.Text = mode Then
            Return True
        Else
            Return False
        End If

    End Function


    ''' <summary>
    ''' 対象データチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTargetValid(ByVal frm As LMI970F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0
            Dim unchinZeroExists As Boolean = False

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI970G.sprDetailDef.SEND_KB.ColNo)) = LMI970C.SendKbName.SoushinZumi Then
                    '既に送信済の場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"既に送信済みの行が選択されている", "、実績作成", ""})
                    Return False
                End If

                If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI970G.sprDetailDef.KOJO_KANRI_NO.ColNo))) Then
                    '工場管理番号は必須
                    MyBase.ShowMessage(frm, "E428", New String() {"工場管理番号のない行が選択されている", "、実績作成", ""})
                    Return False
                End If

                If CDec(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI970G.sprDetailDef.UNCHIN.ColNo))) = CDec(0) Then
                    '金額0の行があるか
                    unchinZeroExists = True
                End If

            Next

            If unchinZeroExists Then
                '金額0が1行でもある場合、ワーニング表示
                If MyBase.ShowMessage(frm, "W217", New String() {"運賃が0円の行が選択されています。実績作成", ""}) = MsgBoxResult.Cancel Then
                    Return False
                End If
            End If

            Return True

        End With

    End Function

#End Region '各処理のチェック

#End Region 'チェック

#End Region '内部メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMI970F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.JissekiSakusei(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMI970F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EditListData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI970F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SelectListData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMI970F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMI970F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveListData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI970F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI970F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' 印刷ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub PrintClick(ByVal frm As LMI970F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintClick")

        '印刷処理
        Call Me.BtnPrintClick(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintClick")

    End Sub

    'ADD START 2019/05/30 要望管理006030
    ''' <summary>
    ''' 更新ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub UpdateTankaClick(ByVal frm As LMI970F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateTankaClick")

        '単価更新処理
        Call Me.UpdateTanka(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateTankaClick")

    End Sub


    Friend Sub MeisaiItemChanged(ByVal frm As LMI970F, ByVal spr As Win.Spread.LMSpread, ByVal e As FarPoint.Win.Spread.ChangeEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MeisaiItemChanged")

        '明細項目自動更新処理
        Call Me.UpdateMeisaiItem(frm, spr, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MeisaiItemChanged")

    End Sub

    'ADD END   2019/05/30 要望管理006030

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI970F_KeyDown(ByVal frm As LMI970F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
