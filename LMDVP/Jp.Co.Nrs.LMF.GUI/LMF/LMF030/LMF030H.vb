' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF030H : 運行情報入力
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF030ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF030H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF030V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF030G

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
        Dim frm As LMF030F = New LMF030F(Me)

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        ''フォームの初期化
        'Call MyBase.InitControl(frm)
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

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
        Me._G = New LMF030G(Me, frm, Me._LMFconG)

        'Validateクラスの設定
        Me._V = New LMF030V(Me, frm, Me._LMFconV, Me._G, Me._LMFconG)

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        'フォームの初期化
        Call MyBase.InitControl(frm)
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'コントロール個別設定
        Call Me._G.SetControl()

        '値のクリア
        Call Me._G.ClearControl()

        '初期設定
        If Me.SetForm(frm, prmDs, prm.RecStatus) = False Then
            Exit Sub
        End If

        'メッセージの表示
        Call Me.SetGMessage(frm)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

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
    Private Function SetForm(ByVal frm As LMF030F, ByVal prmDs As DataSet, ByVal recStatus As String) As Boolean

        Dim rtnResult As Boolean = False
        Dim mode As String = String.Empty
        Dim status As String = String.Empty
        Dim actionId As String = String.Empty

        'ステータス判定
        Select recStatus

            Case RecordStatus.NEW_REC

                '初期検索処理
                actionId = LMF030C.ACTION_ID_INIT_NEW
                mode = DispMode.EDIT
                status = RecordStatus.NEW_REC

            Case RecordStatus.NOMAL_REC

                '初期検索処理
                actionId = LMF030C.ACTION_ID_INIT_SELECT
                mode = DispMode.VIEW
                status = RecordStatus.NOMAL_REC

        End Select

        'データ取得
        Me._Ds = Me._LMFconH.ServerAccess(prmDs, actionId)

        '検索成功
        If Me._Ds Is Nothing = False _
            AndAlso 0 < Me._Ds.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows.Count Then

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
            Call Me._G.SetControlsStatus()

            '計算処理
            Call Me.AllCalculation(frm)

        End If

        Return rtnResult

    End Function

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftEditMode(ByVal frm As LMF030F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF030C.ActionType.EDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsEditChk()

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        '支払確定チェック
        rtnResult = rtnResult AndAlso Me._V.IsFixShiharaiChk(Me._Ds)
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

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
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteAction(ByVal frm As LMF030F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF030C.ActionType.EDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsDeleteChk()

        '運行データキャンセル可否チェック
        rtnResult = rtnResult AndAlso Me.SetTripStatusData(frm, Me._Ds)

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        '支払確定チェック
        rtnResult = rtnResult AndAlso Me._V.IsFixShiharaiChk(Me._Ds)
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F4ButtonName))

        '運送(大)レコードから運行番号を削除
        rtnResult = rtnResult AndAlso Me.SetDeleteData(frm, Me._Ds)

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        '支払運賃データの設定
        If rtnResult = True Then
            Dim rtnDs As DataSet = Me.SetShiharaiData(frm, Me._Ds, True)
        End If
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

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
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMF030F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF030C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF030C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        '項目チェック：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMF030C.ActionType.MASTEROPEN)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        ''参照の場合、Tab移動して終了
        'If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
        '    Call Me._LMFconH.NextFocusedControl(frm, eventFlg)
        '    Exit Sub
        'End If
        If objNm = Nothing Then
            Call Me._LMFconH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If
        If (objNm).Equals(frm.txtTariffCd.Name) = True OrElse _
            (objNm).Equals(frm.txtExtcCd.Name) = True Then
            '編集の場合、Tab移動して終了
            If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = False Then
                Call Me._LMFconH.NextFocusedControl(frm, eventFlg)
                Exit Sub
            End If
        Else
            '参照の場合、Tab移動して終了
            If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
                Call Me._LMFconH.NextFocusedControl(frm, eventFlg)
                Exit Sub
            End If
        End If
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        'Enterキー判定
        Dim rtnResult As Boolean = eventFlg

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMF030C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF030C.ActionType.ENTER)

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
        Call Me.ShowPopupControl(frm, objNm, LMF030C.ActionType.ENTER)

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
    Private Function SaveTripItemData(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF030C.ActionType.SAVE)

        '計算処理
        rtnResult = rtnResult AndAlso Me.AllCalculation(frm)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(Me._Ds)

        '2022.09.06 追加START
        If rtnResult Then
            '車輌マスタ情報設定
            SetCarInfoData(frm)
        End If
        '2022.09.06 追加END

        '値設定
        Dim ds As DataSet = Me._Ds.Copy

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        'rtnResult = rtnResult AndAlso Me.SetData(frm, ds, Me._Ds.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0).Item("HAISO_KB").ToString())
        If rtnResult Then
            ds = Me.SetData(frm, ds, Me._Ds.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0).Item("HAISO_KB").ToString())
        End If
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

        '保存処理
        rtnResult = rtnResult AndAlso Me.TripSaveData(frm, ds)

        'START KIM 要望番号1485 支払い関連修正
        '支払運賃更新
        rtnResult = rtnResult AndAlso Me.SaveShiharaiTo(frm, ds)
        'END KIM 要望番号1485 支払い関連修正

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、処理を終了
        If rtnResult = False Then
            Return rtnResult
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '値のクリア
        Call Me._G.ClearControl()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '値の設定
        Call Me._G.SetInitValue(Me._Ds)

        '計算処理
        Call Me.AllCalculation(frm)

        '処理終了メッセージの表示
        Call Me._LMFconH.SetMessageG002(frm _
                                        , Me._LMFconV.SetRepMsgData(frm.FunctionKey.F11ButtonName) _
                                        , String.Concat(LMFControlC.KAKKO_1, frm.lblTitleTripNo.Text, LMFControlC.EQUAL, frm.lblTripNo.TextValue, LMFControlC.KAKKO_2))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        Return rtnResult

    End Function

    '要望番号2063 追加START 2015.05.27
    ''' <summary>
    ''' 手配作成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CreateTehaiData(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF030C.ActionType.TEHAICREATE)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsTehaiCheck(DispMode.EDIT, RecordStatus.NOMAL_REC)

        '値設定
        Dim ds As DataSet = Me._Ds.Copy

        If rtnResult Then
            ds = Me.SetTehaiData(frm, ds, Me._Ds.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0).Item("HAISO_KB").ToString(), LMF030C.ActionType.TEHAICREATE)
        End If

        '保存処理
        rtnResult = rtnResult AndAlso Me.CreateTehaiData(frm, ds)

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、処理を終了
        If rtnResult = False Then
            Return rtnResult
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '値のクリア
        Call Me._G.ClearControl()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '値の設定
        Call Me._G.SetInitValue(Me._Ds)

        '計算処理
        Call Me.AllCalculation(frm)

        '処理終了メッセージの表示
        Call Me._LMFconH.SetMessageG002(frm _
                                        , Me._LMFconV.SetRepMsgData(frm.FunctionKey.F11ButtonName) _
                                        , String.Concat(LMFControlC.KAKKO_1, frm.lblTitleTripNo.Text, LMFControlC.EQUAL, frm.lblTripNo.TextValue, LMFControlC.KAKKO_2))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        Return rtnResult

    End Function
    '要望番号2063 追加END 2015.05.27

    ''' <summary>
    ''' 選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMF030F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        '列ヘッダをクリックした場合、スルー
        If e.ColumnHeader = True Then
            Exit Sub
        End If

        'デフォルト処理をキャンセルします。↓これをしないと前に表示されない。
        If e.Row > -1 AndAlso e.ColumnHeader = False AndAlso e.ColumnHeader = False Then
            e.Cancel = True
        End If

        If rowNo > -1 Then

            '処理開始アクション
            Call Me.StartAction(frm)

            If Me._V.IsAuthority(LMF030C.ActionType.DOUBLECLICK) = False Then

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            '送信するデータセットに検索条件を設定
            Dim ds As DataSet = New LMF030DS()
            Dim dt As DataTable = ds.Tables(LMF030C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow()
            Dim unsoNo As String = String.Empty
            With frm.sprDetail.ActiveSheet

                Dim recNo As Integer = Convert.ToInt32(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF030G.sprDetailDef.REC_NO.ColNo)))
                Dim unsoLDr As DataRow = Me._Ds.Tables(LMF030C.TABLE_NM_UNSO_L).Rows(recNo)
                dr.Item("NRS_BR_CD") = unsoLDr.Item("NRS_BR_CD").ToString()
                unsoNo = unsoLDr.Item("UNSO_NO_L").ToString()
                dr.Item("UNSO_NO_L") = unsoNo
                dt.Rows.Add(dr)

            End With

            '==========================
            'WSAクラス呼出
            '==========================
            Dim rtnDs As DataSet = Me._LMFconH.ServerAccess(ds, LMF030C.ACTION_ID_SELECT_L)

            'エラー判定
            If MyBase.IsMessageExist() = True Then

                'メッセージ設定
                '2016.01.06 UMANO 英語化対応START
                'Call Me._LMFconV.SetMstErrMessage("運送(大)テーブル", unsoNo)
                Call Me._LMFconV.SetMstErrMessage("運送(大)テーブル(Deliv. Large Table)", unsoNo)
                '2016.01.06 UMANO 英語化対応END

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            'パラメータ設定
            Dim prmDs As DataSet = New LMF020DS()
            Dim prmDt As DataTable = prmDs.Tables(LMF020C.TABLE_NM_IN)
            prmDt.ImportRow(ds.Tables(LMF030C.TABLE_NM_IN).Rows(0))

            Call Me._LMFconH.FormShow(prmDs, "LMF020", RecordStatus.NOMAL_REC)

            '処理終了アクション
            Call Me.EndAction(frm)

        End If

    End Sub

    ''' <summary>
    ''' 行追加
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddUnsoLData(ByVal frm As LMF030F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF030C.ActionType.ADD)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '行追加(Popの戻り値設定)
        Me._Ds = Me.SetReturnUnsoLPop(frm, Me._Ds)

        '値設定
        Call Me._G.SetSpread(Me._Ds)

        '計算処理
        Call Me.AllCalculation(frm)

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        'ぎょうへんしゅうふらぐをオンにせってい
        frm.txtIsEditRowFlg.TextValue = "1"
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 行削除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DelUnsoLData(ByVal frm As LMF030F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF030C.ActionType.ADD)

        'チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            '並び替え
            Call Me._G.sprSortColumnCommand(frm.sprDetail, LMF030G.sprDetailDef.REC_NO.ColNo)
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMF030G.sprDetailDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsRowDelChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '行削除
        Call Me.DeleteTableData(frm, Me._Ds, arr)

        '値設定
        Call Me._G.SetSpread(Me._Ds)

        '計算処理
        Call Me.AllCalculation(frm)

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        'ぎょうへんしゅうふらぐをオンにせってい
        frm.txtIsEditRowFlg.TextValue = "1"
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End


        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 計算
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiharaiKeisan(ByVal frm As LMF030F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF030C.ActionType.SHIHARAIKEISAN)

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        '支払確定チェック
        rtnResult = rtnResult AndAlso Me._V.IsFixShiharaiChk(Me._Ds)
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsKeisanCheck(Me._Ds)

        'タリフマスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsUnchinExistChk(frm)

        '横持タリフマスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsYokoTariffExistChk(frm)

        '割増タリフマスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsETariffExistChk(frm)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセットの設定
        Dim newUpdDate As String = MyBase.GetSystemDateTime(0)
        Dim newUpdTime As String = MyBase.GetSystemDateTime(1)
        Dim ds As DataSet = New LMF030DS()
        ds = Me.SetInKeisan(frm, ds, newUpdDate, newUpdTime)

        'データセットのチェック
        rtnResult = rtnResult AndAlso Me._V.IsKeisanDataSetCheck(ds)

        '保存処理
        rtnResult = rtnResult AndAlso Me.ShiharaiKeisanSaveData(frm, ds)

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、処理を終了
        If rtnResult = False Then
            Exit Sub
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '値のクリア
        Call Me._G.ClearControl()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '値の設定
        Call Me._G.SetInitValue(Me._Ds)

        '計算処理
        Call Me.AllCalculation(frm)

        '処理終了メッセージの表示
        Call Me._LMFconH.SetMessageG002(frm _
                                        , "計算" _
                                        , String.Concat(LMFControlC.KAKKO_1, frm.lblTitleTripNo.Text, LMFControlC.EQUAL, frm.lblTripNo.TextValue, LMFControlC.KAKKO_2))

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 支払タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinExistChk(ByVal frm As LMF030F) As Boolean

        '横持の場合、スルー
        If ("40").Equals(frm.cmbTariffKbn.SelectedValue.ToString()) = True Then
            Return True
        End If

        '支払運賃計算コンボが"件数加算クリア"の場合、以降のチェックは行わない
        If ("02").Equals(frm.cmbShiharai.SelectedValue) = True Then
            Return True
        End If

        '適用開始日の設定
        Dim chkDate As String = String.Empty
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        With spr.ActiveSheet

            Dim max As Integer = .Rows.Count - 1

            chkDate = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(0, LMF030G.sprDetailDef.NONYUDATE.ColNo)))

            For i As Integer = 0 To max
                If chkDate > DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(0, LMF030G.sprDetailDef.NONYUDATE.ColNo))) Then
                    chkDate = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(0, LMF030G.sprDetailDef.NONYUDATE.ColNo)))
                End If
            Next

        End With


        '取得できない場合、エラー
        Dim tariffCd As String = frm.txtTariffCd.TextValue
        Dim drs As DataRow() = Me._LMFconG.SelectShiharaiTariffListDataRow(tariffCd, String.Empty, chkDate)
        If drs.Length < 1 Then
            Me._LMFconV.SetErrorControl(frm.txtTariffCd)
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage(frm, "E079", New String() {"支払タリフマスタ", frm.txtTariffCd.TextValue})
            MyBase.ShowMessage(frm, "E695", New String() {frm.txtTariffCd.TextValue})
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If
        frm.txtTariffCd.TextValue = drs(0).Item("SHIHARAI_TARIFF_CD").ToString

        Return True

    End Function

    ''' <summary>
    ''' 支払横持タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsYokoTariffExistChk(ByVal frm As LMF030F) As Boolean

        With frm

            '横持以外の場合、スルー
            If ("40").Equals(frm.cmbTariffKbn.SelectedValue.ToString()) = False Then
                Return True
            End If

            '支払運賃計算コンボが"件数加算クリア"の場合、以降のチェックは行わない
            If ("02").Equals(frm.cmbShiharai.SelectedValue) = True Then
                Return True
            End If

            '取得できない場合、エラー
            Dim tariffCd As String = frm.txtTariffCd.TextValue

            '存在チェック(横持ちタリフマスタ)
            Dim nrsbrcd As String = Convert.ToString(frm.cmbEigyo.SelectedValue)
            Dim drs As DataRow() = Me._LMFconG.SelectShiharaiYokoTariffListDataRow(nrsbrcd, tariffCd)
            If drs.Length < 1 Then
                Me._LMFconV.SetErrorControl(frm.txtTariffCd)
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage(frm, "E079", New String() {"支払横持タリフマスタ", frm.txtTariffCd.TextValue})
                MyBase.ShowMessage(frm, "E696", New String() {frm.txtTariffCd.TextValue})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If
            frm.txtTariffCd.TextValue = drs(0).Item("YOKO_TARIFF_CD").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' 支払割増タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsETariffExistChk(ByVal frm As LMF030F) As Boolean

        With frm

            '支払運賃計算コンボが"件数加算クリア"の場合、以降のチェックは行わない
            If ("02").Equals(frm.cmbShiharai.SelectedValue) = True OrElse _
                String.IsNullOrEmpty(frm.txtExtcCd.TextValue) = True Then
                Return True
            End If

            Dim nrsbrcd As String = Convert.ToString(frm.cmbEigyo.SelectedValue)

            '取得できない場合、エラー
            Dim tariffCd As String = frm.txtExtcCd.TextValue
            Dim drs As DataRow() = Me._LMFconG.SelectExtcShiharaiListDataRow(nrsbrcd, tariffCd, String.Empty)
            If drs.Length < 1 Then
                Me._LMFconV.SetErrorControl(frm.txtExtcCd)
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage(frm, "E079", New String() {"支払割増タリフマスタ", frm.txtExtcCd.TextValue})
                MyBase.ShowMessage(frm, "E697", New String() {frm.txtExtcCd.TextValue})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If
            frm.txtExtcCd.TextValue = drs(0).Item("EXTC_TARIFF_CD").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' タリフ分類区分変更時
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub tariffKbnSelected(ByVal frm As LMF030F)

        'タリフ分類区分のロック設定
        Call Me._G.SetTariffKbn()

    End Sub
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMF030F, ByVal e As FormClosingEventArgs)

        '編集モード以外なら処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False Then
            Exit Sub
        End If

        'メッセージの表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes '「はい」押下時

                '保存処理
                If Me.SaveTripItemData(frm, LMF030C.ActionType.CLOSE) = False Then
                    e.Cancel = True
                End If

            Case MsgBoxResult.Cancel '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMF030F, ByVal objNm As String, ByVal actionType As LMF030C.ActionType) As Boolean

        With frm

            Dim rtnResult As Boolean = False
            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            Dim hikaku As String = "40"
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

            'SHINOHARA 要望番号513 カッコ内actionTypeを追加
            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtCarNo.Name

                    rtnResult = Me.SetReturnCarPop(frm, actionType)

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    rtnResult = Me.SetReturnUnsocoPop(frm, actionType)

                Case .txtDriverCd.Name

                    rtnResult = Me.SetReturnDriverPop(frm, actionType)

                    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                Case .txtTariffCd.Name
                    '横持ちの場合
                    If hikaku.Equals(.cmbTariffKbn.SelectedValue) = True Then

                        '横持ちタリフ
                        rtnResult = Me.SetReturnYokoTariffPop(frm, actionType)

                    Else

                        'タリフコード
                        rtnResult = Me.SetReturnTariffPop(frm, actionType)

                    End If

                Case .txtExtcCd.Name

                    rtnResult = Me.SetReturnExtcPop(frm, actionType)
                    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

            End Select

            '処理終了アクション
            Call Me.EndAction(frm)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 車輌Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCarPop(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowCarPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ170C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtCarNo.TextValue = dr.Item("CAR_NO").ToString()
                Dim drs As DataRow() = Me._LMFconG.SelectKbnListDataRow(dr.Item("CAR_TP_KB").ToString(), LMKbnConst.KBN_S023)
                Dim type As String = String.Empty
                If 0 < drs.Length Then
                    type = drs(0).Item("KBN_NM1").ToString()
                End If
                .lblCarType.TextValue = type
                .lblCarKey.TextValue = dr.Item("CAR_KEY").ToString()
                .numOndoMx.Value = dr.Item("ONDO_MX").ToString()
                .numOndoMm.Value = dr.Item("ONDO_MM").ToString()
                .numLoadWt.Value = dr.Item("LOAD_WT").ToString()
                .lblSyakenTruck.TextValue = dr.Item("INSPC_DATE_TRUCK").ToString()
                .lblSyakenTrailer.TextValue = dr.Item("INSPC_DATE_TRAILER").ToString()
                .cmbJshaKbn.SelectedValue = dr.Item("JSHA_KB").ToString()

                '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 Start
                .txtUnsocoCd.TextValue = dr.Item("UNSOCO_CD").ToString()        '運送会社CD
                .txtUnsocoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()   '運送会社BR_CD
                .lblUnsocoNm.TextValue = dr.Item("UNSOCO_NM").ToString()        '運送会社名

                '2022.09.06 追加START
                '車輌マスタ情報設定
                SetCarInfoData(frm)
                '2022.09.06 追加END

                '----------------------------------
                '直近の運送情報から乗務員を取得
                '----------------------------------
                Dim setDs As DataSet = Me._Ds.Copy()
                setDs.Clear()           '値のクリア

                Dim setDtIn As DataTable = setDs.Tables("LMF030IN")
                setDtIn.Rows.Add()      'LMF030INに１行追加

                'NRS_BR_CD、CAR_KEYをLMF030INに設定
                setDtIn.Rows(0).Item("NRS_BR_CD") = dr.Item("NRS_BR_CD")
                setDtIn.Rows(0).Item("CAR_KEY") = dr.Item("CAR_KEY")

                '直近の運送情報から乗務員を取得
                setDs = Me._LMFconH.ServerAccess(setDs, "SelectDriverData")

                If setDs.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows.Count > 0 Then
                    '運送情報が存在する場合は取得した値をセット
                    .txtDriverCd.TextValue = setDs.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0).Item("DRIVER_CD").ToString
                    .lblDriverNm.TextValue = setDs.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0).Item("DRIVER_NM").ToString
                Else
                    '運送情報が存在しない場合は空セット
                    .txtDriverCd.TextValue = String.Empty
                    .lblDriverNm.TextValue = String.Empty
                End If

                '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 End

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 車輌マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCarPopup(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ170DS()
        Dim dt As DataTable = ds.Tables(LMZ170C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF030C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("CAR_NO") = frm.txtCarNo.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ170", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 運送会社Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsocoPop(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowUnsocoPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtUnsocoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtUnsocoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblUnsocoNm.TextValue = Me._LMFconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), Space(1))

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnsocoPopup(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF030C.ActionType.ENTER Then
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
    ''' 乗務員Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDriverPop(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowDriverPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ160C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtDriverCd.TextValue = dr.Item("DRIVER_CD").ToString()
                .lblDriverNm.TextValue = dr.Item("DRIVER_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 乗務員マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowDriverPopup(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ160DS()
        Dim dt As DataTable = ds.Tables(LMZ160C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF030C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("DRIVER_CD") = frm.txtDriverCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ160", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 運送(大)Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsoLPop(ByVal frm As LMF030F, ByVal ds As DataSet) As DataSet

        Dim prm As LMFormData = Me.ShowUnsoLPopup(frm)

        '戻り値がない場合、スルー
        If prm.ReturnFlg = False Then
            Return ds
        End If

        '戻り分、レコード追加
        Dim prmDt As DataTable = prm.ParamDataSet.Tables(LMF200C.TABLE_NM_OUT)
        Dim prmDr As DataRow = Nothing
        Dim max As Integer = prmDt.Rows.Count - 1
        Dim dt As DataTable = ds.Tables(LMF030C.TABLE_NM_UNSO_L)
        Dim drs As DataRow() = Nothing

        If -1 < max Then

            '納入日を運行日に設定
            frm.lblTripDate.TextValue = DateFormatUtility.EditSlash(prmDt.Rows(0).Item("ARR_PLAN_DATE").ToString())

            For i As Integer = 0 To max

                prmDr = prmDt.Rows(i)
                drs = dt.Select(Me.SetUnsoLSql(prmDr))

                If drs.Length < 1 Then

                    'まだ設定していないレコードの場合、そのまま設定
                    dt.ImportRow(prmDt.Rows(i))

                    '更新フラグは初期値
                    dt.Rows(dt.Rows.Count - 1).Item("UP_KBN") = LMConst.FLG.OFF

                Else

                    'すでにレコードが存在する場合、削除フラグをOFFに設定
                    drs(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                End If

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運送(大)マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnsoLPopup(ByVal frm As LMF030F) As LMFormData

        Dim ds As DataSet = New LMF200DS()
        Dim dt As DataTable = ds.Tables(LMF200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim unsoDt As DataTable = Me._Ds.Tables(LMF030C.TABLE_NM_UNSO_L)
        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("ARR_PLAN_DATE_FROM") = DateFormatUtility.DeleteSlash(frm.lblTripDate.TextValue)

            Dim spr As SheetView = frm.sprDetail.ActiveSheet
            If 0 < spr.Rows.Count Then

                Dim recNo As Integer = Convert.ToInt32(Me._LMFconG.GetCellValue(spr.Cells(0, LMF030G.sprDetailDef.REC_NO.ColNo)))
                Dim setDr As DataRow = unsoDt.Rows(recNo)

                .Item("UNSO_CD") = setDr.Item("UNSO_CD").ToString()
                .Item("UNSO_BR_CD") = setDr.Item("UNSO_BR_CD").ToString()
                .Item("UNSO_NM") = Me._LMFconG.EditConcatData(setDr.Item("UNSO_NM").ToString(), setDr.Item("UNSO_BR_NM").ToString(), String.Empty)

            End If

        End With
        dt.Rows.Add(dr)

        'LMF030にある運送(大)の情報を設定
        Dim setDt As DataTable = ds.Tables(LMF200C.F_UNSO_L)
        Dim max As Integer = unsoDt.Rows.Count - 1
        For i As Integer = 0 To max
            setDt.ImportRow(unsoDt.Rows(i))
        Next

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMF200")

    End Function

    ''' <summary>
    ''' 行追加時の検索条件
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoLSql(ByVal dr As DataRow) As String

        Return String.Concat(" UNSO_NO_L = '", dr.Item("UNSO_NO_L").ToString(), "' ")

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowTariffPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ290C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtTariffCd.TextValue = dr.Item("SHIHARAI_TARIFF_CD").ToString()
                .lblTariffNm.TextValue = dr.Item("SHIHARAI_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowTariffPopup(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ290DS()
        Dim dt As DataTable = ds.Tables(LMZ290C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If actionType = LMF030C.ActionType.ENTER Then
                .Item("SHIHARAI_TARIFF_CD") = frm.txtTariffCd.TextValue
            End If
            'START YANAI 要望番号1424 支払処理
            .Item("STR_DATE") = DateFormatUtility.DeleteSlash(frm.lblTripDate.TextValue)
            'END YANAI 要望番号1424 支払処理

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ290", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 支払横持タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnYokoTariffPop(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowYokoTariffPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ320C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtTariffCd.TextValue = dr.Item("YOKO_TARIFF_CD").ToString()
                .lblTariffNm.TextValue = dr.Item("YOKO_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払横持タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ320DS()
        Dim dt As DataTable = ds.Tables(LMZ320C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If actionType = LMF030C.ActionType.ENTER Then
                .Item("YOKO_TARIFF_CD") = frm.txtTariffCd.TextValue
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
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnExtcPop(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowExtcPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ300C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtExtcCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblExtcNm.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowExtcPopup(ByVal frm As LMF030F, ByVal actionType As LMF030C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ300DS()
        Dim dt As DataTable = ds.Tables(LMZ300C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            If actionType = LMF030C.ActionType.ENTER Then
                .Item("EXTC_TARIFF_CD") = frm.txtExtcCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ300", "", Me._PopupSkipFlg)

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal frm As LMF030F, ByVal ds As DataSet) As Boolean

        Return Me.ActionData(frm, ds, LMF030C.ACTION_ID_HAITA_CHK)

    End Function

    ''' <summary>
    ''' 運行データキャンセル可否チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetTripStatusData(ByVal frm As LMF030F, ByVal ds As DataSet) As Boolean

        If frm.lblTripNo.TextValue = String.Empty Then

            Return True

        Else

            'パラメータ設定
            Dim ds10 As DataSet = New LMF010DS()
            Dim dt As DataTable = ds10.Tables(LMF010C.TABLE_NM_ITEM)
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr.Item("TRIP_NO") = frm.lblTripNo.TextValue
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
    Private Function DeleteAction(ByVal frm As LMF030F, ByVal ds As DataSet) As Boolean

        Return Me.ActionData(frm, ds, LMF030C.ACTION_ID_DELETE)

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function TripSaveData(ByVal frm As LMF030F, ByVal ds As DataSet) As Boolean

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Dim actionId As String = LMF030C.ACTION_ID_INIT_SAVE
        If RecordStatus.NOMAL_REC.Equals(frm.lblSituation.RecordStatus) = True Then
            actionId = LMF030C.ACTION_ID_EDIT_SAVE
        End If

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, actionId, rtnDs) = False Then
            Return False
        End If

        '値の設定
        Me._Ds = rtnDs

        Return True

    End Function

    '要望番号2063 追加START 2015.05.27
    ''' <summary>
    ''' 手配作成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CreateTehaiData(ByVal frm As LMF030F, ByVal ds As DataSet) As Boolean

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Dim actionId As String = LMF030C.ACTION_ID_INIT_CREATE
        If RecordStatus.NOMAL_REC.Equals(frm.lblSituation.RecordStatus) = True Then
            actionId = LMF030C.ACTION_ID_INIT_CREATE
        End If

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, actionId, rtnDs) = False Then
            Return False
        End If

        '値の設定
        Me._Ds = rtnDs

        Return True

    End Function
    '要望番号2063 追加END 2015.05.27

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 計算時の保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShiharaiKeisanSaveData(ByVal frm As LMF030F, ByVal ds As DataSet) As Boolean

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Dim actionId As String = LMF030C.ACTION_ID_KEISAN_SAVE

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, actionId, rtnDs) = False Then
            Return False
        End If

        '値の設定
        Me._Ds = rtnDs

        Return True

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    'START KIM 要望番号1485 支払い関連修正
    ''' <summary>
    ''' 支払先設定処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SaveShiharaiTo(ByVal frm As LMF030F, ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMF030C.TABLE_NM_SHIHARAI)
        Dim inDt As DataTable = ds.Tables(LMF030C.TABLE_NM_UNSO_L)
        Dim dr As DataRow = Nothing
        Dim unsoDr() As DataRow = Nothing
        Dim strShiharaiCd As String = String.Empty

        '運送会社マスタに設定されている支払先コードを設定する
        'unsoDr = Me._LMFconG.SelectUnsocoListDataRow(frm.txtUnsocoCd.TextValue, frm.txtUnsocoBrCd.TextValue)
        '20160928 要番2622 tsunehira add
        unsoDr = Me._LMFconG.SelectUnsocoListDataRow(frm.cmbEigyo.SelectedValue.ToString, frm.txtUnsocoCd.TextValue, frm.txtUnsocoBrCd.TextValue)

        If unsoDr IsNot Nothing AndAlso unsoDr.Length > 0 Then
            'マスタの値を設定
            strShiharaiCd = unsoDr(0).Item("SHIHARAITO_CD").ToString
        End If

        For i As Integer = 0 To inDt.Rows.Count - 1
            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr.Item("UNSO_NO_L") = inDt.Rows(i).Item("UNSO_NO_L")
            dr.Item("SHIHARAITO_CD") = strShiharaiCd
            dt.Rows.Add(dr)
        Next

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Dim actionId As String = LMF030C.ACTION_ID_SHIHARAISAKI_SAVE

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, actionId, rtnDs) = False Then
            Return False
        End If

        'START KIM 要望番号1524 2012/10/18
        '値の設定
        'Me._Ds = rtnDs
        'END KIM 要望番号1524 2012/10/18

        Return True

    End Function
    'END KIM 要望番号1485 支払い関連修正

    ''' <summary>
    ''' サーバアクセス(チェック有)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <param name="rtnDs">戻りDataSet 初期値 = Nothing</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ActionData(ByVal frm As LMF030F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        'サーバアクセス
        rtnDs = Me._LMFconH.ServerAccess(ds, actionId)

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMF030F)

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
    Private Sub EndAction(ByVal frm As LMF030F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' メインテーブル行削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <remarks></remarks>
    Private Function DeleteTableData(ByVal frm As LMF030F, ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMF030C.TABLE_NM_UNSO_L)
        Dim max As Integer = arr.Count - 1

        '削除のみのループ
        For i As Integer = max To 0 Step -1

            '行削除処理
            Call Me.DeleteRowData(dt.Rows(Convert.ToInt32(Me._LMFconG.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMF030G.sprDetailDef.REC_NO.ColNo)))))

        Next

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        '行削除時に支払運賃再計算を行うようにするため、運送会社OLDをクリアし、無条件で再計算するようにする
        frm.txtUnsocoCdOld.TextValue = String.Empty
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        Return True

    End Function

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <remarks></remarks>
    Private Sub DeleteRowData(ByVal dr As DataRow)

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        'If LMConst.FLG.ON.Equals(dr.Item("UP_KBN").ToString()) = True Then
        '    '削除フラグをON
        '    dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON
        'Else
        '    '行自体を削除
        '    dr.Delete()
        'End If
        '○行自体を削除すると、「行削除→保存」の流れの時、行削除した支払運賃の更新が行われないため、削除フラグをONにする
        '削除フラグをON
        dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    End Sub

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMF030F)

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
    Private Sub SetGMessage(ByVal frm As LMF030F)

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
    Private Function SetData(ByVal frm As LMF030F, ByVal ds As DataSet, ByVal motoHaisoKbn As String) As DataSet
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        'Private Function SetData(ByVal frm As LMF030F, ByVal ds As DataSet, ByVal motoHaisoKbn As String) As Boolean
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End

        '運送(特大)データの設定
        ds = Me.SetUnsoLLData(frm, ds)

        '運送(大)データの設定
        ds = Me.SetUnsoLData(frm, ds, motoHaisoKbn)

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        '支払運賃データの取得（行追加時の対応） 
        'サーバアクセス
        'Dim rtnDs As DataSet = ds.Copy
        If Me.ActionData(frm, ds, "SelectShiharaiData", ds) = False Then
            Return ds
        End If
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

        '支払運賃データの設定
        ds = Me.SetShiharaiData(frm, ds, False)
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        'Return True
        Return ds
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

    End Function

    '要望番号2063 追加START 2015.05.27
    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetTehaiData(ByVal frm As LMF030F, ByVal ds As DataSet, ByVal motoHaisoKbn As String, ByVal actionType As LMF030C.ActionType) As DataSet

        '運送(特大)データの設定
        ds = Me.SetUnsoLLData(frm, ds, actionType)

        '運送(大)データの設定
        ds = Me.SetUnsoLData(frm, ds, motoHaisoKbn, actionType)

        Return ds

    End Function
    '要望番号2063 追加END 2015.05.27

    '要望番号2063 修正START 2015.05.27
    ''' <summary>
    ''' 運送(特大)データの設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoLLData(ByVal frm As LMF030F, ByVal ds As DataSet, Optional ByVal actionType As LMF030C.ActionType = LMF030C.ActionType.SAVE) As DataSet
        '要望番号2063 修正END 2015.05.27

        Dim dr As DataRow = ds.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0)
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("TRIP_NO") = .lblTripNo.TextValue
            dr.Item("TRIP_DATE") = DateFormatUtility.DeleteSlash(.lblTripDate.TextValue)
            dr.Item("BIN_KB") = .cmbBinKbn.SelectedValue
            dr.Item("CAR_NO") = .txtCarNo.TextValue
            dr.Item("CAR_TP_NM") = .lblCarType.TextValue
            dr.Item("CAR_KEY") = .lblCarKey.TextValue
            dr.Item("ONDO_MM") = .numOndoMx.Value
            dr.Item("ONDO_MX") = .numOndoMm.Value
            dr.Item("UNSOCO_CD") = .txtUnsocoCd.TextValue
            dr.Item("UNSOCO_BR_CD") = .txtUnsocoBrCd.TextValue
            dr.Item("JSHA_KB") = .cmbJshaKbn.SelectedValue
            dr.Item("HAISO_KB") = .cmbHaiso.SelectedValue
            dr.Item("DRIVER_CD") = .txtDriverCd.TextValue
            dr.Item("DRIVER_NM") = .lblDriverNm.TextValue
            dr.Item("REMARK") = .txtRem.TextValue
            dr.Item("LOAD_WT") = .numLoadWt.Value
            dr.Item("UNSO_ONDO") = .numUnsoOndo.Value
            dr.Item("PAY_UNCHIN") = .numPayAmt.Value
            dr.Item("UNSO_PKG_NB") = .numUnsoNb.Value
            dr.Item("UNSO_WT") = .numTripWt.Value
            dr.Item("DECI_UNCHIN") = .numRevUnchin.Value
            '要望番号2063 追加START 2015.05.27
            If actionType = LMF030C.ActionType.TEHAICREATE Then
                '手配種別を運送LLにセット
                dr.Item("TEHAI_SYUBETSU") = .cmbTehaisyubetsu.SelectedValue
            Else
                dr.Item("TEHAI_SYUBETSU") = .lblTehaisyubetsu.textvalue
            End If
            '要望番号2063 追加END 2015.05.27

        End With

        Return ds

    End Function

    '要望番号2063 修正START 2015.05.27
    ''' <summary>
    ''' 運送(大)データの設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="motoHaisoKbn">初期表示の配送区分</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoLData(ByVal frm As LMF030F, ByVal ds As DataSet, ByVal motoHaisoKbn As String, Optional ByVal actionType As LMF030C.ActionType = LMF030C.ActionType.SAVE) As DataSet
        '要望番号2063 修正END 2015.05.27

        '要望番号:1242 terakawa 2012.07.05 Start
        '新規の場合、スルー
        'If RecordStatus.NEW_REC.Equals(frm.lblSituation.RecordStatus) = True Then
        '    Return ds
        'End If
        '要望番号:1242 terakawa 2012.07.05 End

        Dim dt As DataTable = ds.Tables(LMF030C.TABLE_NM_UNSO_L)

        '要望番号2063 追加START 2015.05.27
        Dim drLL As DataRow = ds.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0)
        '要望番号2063 追加END 2015.05.27

        Dim dr As DataRow = Nothing
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim tripNo As String = frm.lblTripNo.TextValue
        Dim motoColNm As String = Me._V.SetTripColNm(motoHaisoKbn)
        Dim colNm As String = Me._V.SetTripColNm(frm.cmbHaiso.SelectedValue.ToString())

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        '運行の運送会社コードの支払タリフを取得
        Dim unsocoDr() As DataRow = Nothing
        'unsocoDr = Me._LMFconG.SelectUnsocoListDataRow(frm.txtUnsocoCd.TextValue, frm.txtUnsocoBrCd.TextValue)
        '20160928 要番2622 tsunehira add
        unsocoDr = Me._LMFconG.SelectUnsocoListDataRow(frm.cmbEigyo.SelectedValue.ToString, frm.txtUnsocoCd.TextValue, frm.txtUnsocoBrCd.TextValue)
        Dim sSHIHARAI_TARIFF_CD As String = unsocoDr(0).Item("UNCHIN_TARIFF_CD").ToString
        Dim sSHIHARAI_ETARIFF_CD As String = unsocoDr(0).Item("EXTC_TARIFF_CD").ToString
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End


        Dim max As Integer = dt.Rows.Count - 1

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        '新規の場合、削除レコードを取り除く('新規登録時の削除データは何も処理を行わない)
        If RecordStatus.NEW_REC.Equals(frm.lblSituation.RecordStatus) = True Then
            For i As Integer = 0 To max
                '削除の場合、削除レコードを取り除く
                If LMConst.FLG.ON.Equals(dt.Rows(i).Item("SYS_DEL_FLG").ToString()) = True Then
                    dt.Rows(i).Delete()
                    i = i - 1 '行を取り除いたので同一行を再びチェック
                End If

                '行を取り除いた場合があり得るので、最大を超えないよう判断
                If dt.Rows.Count - 1 = i Then
                    Exit For
                End If
            Next

            '行数再取得
            max = dt.Rows.Count - 1
        End If
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

        For i As Integer = 0 To max

            '設定先DataRow
            dr = dt.Rows(i)

            '要望番号:1242 terakawa 2012.07.05 Start
            '新規の場合、スルー
            If RecordStatus.NEW_REC.Equals(frm.lblSituation.RecordStatus) = False Then

                '要望番号2063 修正START 2015.05.27
                '削除の場合、運行番号のクリア
                If LMConst.FLG.ON.Equals(dr.Item("SYS_DEL_FLG").ToString()) = True AndAlso _
                   actionType <> LMF030C.ActionType.TEHAICREATE Then
                    'If LMConst.FLG.ON.Equals(dr.Item("SYS_DEL_FLG").ToString()) = True Then

                    '運行番号を削除運行番号に退避
                    If String.IsNullOrEmpty(drLL("TEHAI_SYUBETSU").ToString) = False Then
                        dr("DEL_TRIP_NO") = dr("TRIP_NO").ToString()
                    End If
                    '要望番号2063 修正END 2015.05.27

                    '運行番号のクリア処理
                    Call Me.SetTripNo(dr, String.Empty, colNm, motoColNm, LMConst.FLG.OFF)

                    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
                    '削除時は運送の運送会社の支払タリフをセット
                    'unsocoDr = Me._LMFconG.SelectUnsocoListDataRow(dr.Item("UNSO_CD").ToString, dr.Item("UNSO_BR_CD").ToString)
                    '20160928 要番2622 tsunehira add
                    unsocoDr = Me._LMFconG.SelectUnsocoListDataRow(dr.Item("NRS_BR_CD").ToString, dr.Item("UNSO_CD").ToString, dr.Item("UNSO_BR_CD").ToString)
                    If Not unsocoDr Is Nothing AndAlso unsocoDr.Count <> 0 Then
                        dr.Item("SHIHARAI_TARIFF_CD") = unsocoDr(0).Item("UNCHIN_TARIFF_CD").ToString
                        dr.Item("SHIHARAI_ETARIFF_CD") = unsocoDr(0).Item("EXTC_TARIFF_CD").ToString
                        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End
                    End If
                    Continue For

                    '要望番号2063 追加START 2015.05.27
                ElseIf LMConst.FLG.OFF.Equals(dr.Item("SYS_DEL_FLG").ToString()) = True AndAlso _
                   actionType = LMF030C.ActionType.SAVE AndAlso _
                   String.IsNullOrEmpty(dr.Item("TRIP_NO").ToString()) = True Then

                    If String.IsNullOrEmpty(drLL("TEHAI_SYUBETSU").ToString) = False Then
                        dr("DEL_TRIP_NO") = tripNo
                    End If
                    '要望番号2063 追加END 2015.05.27

                End If

                '運行番号の設定
                Call Me.SetTripNo(dr, tripNo, colNm, motoColNm, LMConst.FLG.ON)
            End If

            '便区分の設定
            dr.Item("BIN_KB") = frm.cmbBinKbn.SelectedValue

            '運送会社コードの設定
            dr.Item("UNSO_CD") = frm.txtUnsocoCd.TextValue
            dr.Item("UNSO_BR_CD") = frm.txtUnsocoBrCd.TextValue


            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
            '運行の運送会社コードの支払タリフを使用
            dr.Item("SHIHARAI_TARIFF_CD") = sSHIHARAI_TARIFF_CD
            dr.Item("SHIHARAI_ETARIFF_CD") = sSHIHARAI_ETARIFF_CD
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End


            '要望番号:1242 terakawa 2012.07.05 End
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送(大)の運行番号設定処理
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="tripNo">運行番号</param>
    ''' <param name="motoColNm">列名</param>
    ''' <param name="setFlg">設定するかのフラグ　0：設定しない　1：設定する</param>
    ''' <remarks></remarks>
    Private Sub SetTripNo(ByVal dr As DataRow _
                          , ByVal tripNo As String _
                          , ByVal colNm As String _
                          , ByVal motoColNm As String _
                          , ByVal setFlg As String _
                          )

        '中継配送しない場合、運行番号をクリア
        If LMFControlC.FLG_OFF.Equals(dr.Item("TYUKEI_HAISO_FLG").ToString()) = True Then

            dr.Item("TRIP_NO") = tripNo

        Else

            '前回の配送区分が設定されている場合、初期化
            If String.IsNullOrEmpty(motoColNm) = False Then

                dr.Item(motoColNm) = String.Empty

            End If

            'セットフラグがOnの場合、設定
            If LMConst.FLG.ON.Equals(setFlg) = True Then

                dr.Item(colNm) = tripNo

            End If

        End If

    End Sub

    ''' <summary>
    ''' 削除処理時の値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetDeleteData(ByVal frm As LMF030F, ByVal ds As DataSet) As Boolean

        '運送(大)に設定してある運行番号をクリア
        Dim dt As DataTable = ds.Tables(LMF030C.TABLE_NM_UNSO_L)
        Dim max As Integer = dt.Rows.Count - 1
        Dim motoColNm As String = Me._V.SetTripColNm(frm.cmbHaiso.SelectedValue.ToString())

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        '運行の運送会社コードの支払タリフを取得
        Dim unsocoDr() As DataRow = Nothing
        Dim dr As DataRow = Nothing
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

        For i As Integer = 0 To max

            '運行番号のクリア処理
            Call Me.SetTripNo(dt.Rows(i), String.Empty, String.Empty, motoColNm, LMConst.FLG.OFF)

            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
            '削除時は運送の運送会社の支払タリフをセット
            dr = dt.Rows(i)
            'unsocoDr = Me._LMFconG.SelectUnsocoListDataRow(dr.Item("UNSO_CD").ToString, dr.Item("UNSO_BR_CD").ToString)
            '20160928 要番2622 tsunehira add
            unsocoDr = Me._LMFconG.SelectUnsocoListDataRow(dr.Item("NRS_BR_CD").ToString, dr.Item("UNSO_CD").ToString, dr.Item("UNSO_BR_CD").ToString)
            dr.Item("SHIHARAI_TARIFF_CD") = unsocoDr(0).Item("UNCHIN_TARIFF_CD").ToString
            dr.Item("SHIHARAI_ETARIFF_CD") = unsocoDr(0).Item("EXTC_TARIFF_CD").ToString
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

            '要望番号2063 追加START 2015.05.27
            '運行番号を削除運行番号に退避
            If String.IsNullOrEmpty(frm.lblTehaisyubetsu.TextValue) = False Then
                dr.Item("DEL_TRIP_NO") = frm.lblTripNo.TextValue
            End If
            '要望番号2063 追加END 2015.05.27

        Next

        Return True

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 計算時のInDataSetの設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetInKeisan(ByVal frm As LMF030F, ByVal ds As DataSet, ByVal newUpdDate As String, ByVal newUpdTime As String) As DataSet

        Dim dt As DataTable = ds.Tables(LMF030C.TABLE_NM_IN_KEISAN)
        Dim dr As DataRow = dt.NewRow()
        Dim unsoDr() As DataRow = Nothing

        Dim unchin As Decimal = 0
        Dim soJuryo As Decimal = 0
        Dim juryo As Decimal = 0
        Dim value As Decimal = 0
        'START YANAI 要望番号1424 支払処理
        'Dim maxDeciUnchin As Decimal = 0
        Dim maxKyori As Decimal = 0
        Dim deciUnchin As Decimal = 0
        'END YANAI 要望番号1424 支払処理

        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1

        With spr.ActiveSheet

            For i As Integer = 0 To max
                dr = dt.NewRow()

                dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(i, LMF030G.sprDetailDef.UNSO_NO.ColNo))
                dr.Item("TRIP_NO") = frm.lblTripNo.TextValue
                dr.Item("ROW_NO") = i

                unsoDr = Me._Ds.Tables(LMF030C.TABLE_NM_UNSO_L).Select(String.Concat("UNSO_NO_L = '", dr.Item("UNSO_NO_L").ToString, "'"))
                If unsoDr.Length > 0 Then
                    dr.Item("SYS_UPD_DATE") = unsoDr(0).Item("SYS_UPD_DATE")
                    dr.Item("SYS_UPD_TIME") = unsoDr(0).Item("SYS_UPD_TIME")
                End If
                dr.Item("NEW_SYS_UPD_DATE") = newUpdDate
                dr.Item("NEW_SYS_UPD_TIME") = newUpdTime
                dr.Item("SHIHARAI_KEISAN_KB") = frm.cmbShiharai.SelectedValue

                '運送会社マスタに設定されている支払先コードを設定する
                'unsoDr = Me._LMFconG.SelectUnsocoListDataRow(frm.txtUnsocoCd.TextValue, frm.txtUnsocoBrCd.TextValue)
                '20160928 要番2622 tsunehira add
                unsoDr = Me._LMFconG.SelectUnsocoListDataRow(frm.cmbEigyo.SelectedValue.ToString, frm.txtUnsocoCd.TextValue, frm.txtUnsocoBrCd.TextValue)

                If unsoDr.Length > 0 Then
                    'マスタの値を設定
                    dr.Item("SHIHARAITO_CD") = unsoDr(0).Item("SHIHARAITO_CD").ToString
                End If

                If ("01").Equals(frm.cmbShiharai.SelectedValue) = True Then
                    '件数加算の場合
                    dr.Item("SHIHARAI_TARIFF_CD") = frm.txtTariffCd.TextValue
                    dr.Item("SHIHARAI_ETARIFF_CD") = frm.txtExtcCd.TextValue
                    dr.Item("SHIHARAI_UNSO_WT") = frm.numShiharaiWt.Value
                    dr.Item("SHIHARAI_COUNT") = frm.numKensu.Value
                    dr.Item("SHIHARAI_UNCHIN") = "0"
                    dr.Item("SHIHARAI_TARIFF_BUNRUI_KB") = frm.cmbTariffKbn.SelectedValue

                    'DECI_UNCHINのMAX値を求める
                    unsoDr = Me._Ds.Tables(LMF030C.TABLE_NM_SHIHARAI).Select(String.Concat("UNSO_NO_L = '", dr.Item("UNSO_NO_L").ToString, "'"))
                    If unsoDr.Length > 0 Then
                        'START YANAI 要望番号1424 支払処理
                        'If Convert.ToDecimal(unsoDr(0).Item("DECI_UNCHIN").ToString) > maxDeciUnchin Then
                        '    maxDeciUnchin = Convert.ToDecimal(unsoDr(0).Item("DECI_UNCHIN").ToString)
                        'End If
                        If Convert.ToDecimal(unsoDr(0).Item("SHIHARAI_KYORI").ToString) > maxKyori Then
                            maxKyori = Convert.ToDecimal(unsoDr(0).Item("SHIHARAI_KYORI").ToString)
                            deciUnchin = Convert.ToDecimal(unsoDr(0).Item("DECI_UNCHIN").ToString)
                        ElseIf Convert.ToDecimal(unsoDr(0).Item("SHIHARAI_KYORI").ToString) = maxKyori Then
                            If Convert.ToDecimal(unsoDr(0).Item("DECI_UNCHIN").ToString) > deciUnchin Then
                                deciUnchin = Convert.ToDecimal(unsoDr(0).Item("DECI_UNCHIN").ToString)
                            End If
                        End If
                        'END YANAI 要望番号1424 支払処理

                        dr.Item("DECI_WT") = unsoDr(0).Item("DECI_WT").ToString
                        dr.Item("SHIHARAI_WT") = unsoDr(0).Item("SHIHARAI_WT").ToString
                        soJuryo = soJuryo + Convert.ToDecimal(unsoDr(0).Item("SHIHARAI_WT").ToString) 'DECI_WTにはまとめ時、親に合計が設定され、子供はまとめ前の値が設定されているため、おかしくなってしまう

                        dr.Item("SHIHARAI_GROUP_NO") = unsoDr(0).Item("SHIHARAI_GROUP_NO")
                        dr.Item("SHIHARAI_GROUP_NO_M") = unsoDr(0).Item("SHIHARAI_GROUP_NO_M")
                    End If

                ElseIf ("02").Equals(frm.cmbShiharai.SelectedValue) = True Then
                    '件数加算クリアの場合
                    dr.Item("SHIHARAI_TARIFF_CD") = String.Empty
                    dr.Item("SHIHARAI_ETARIFF_CD") = String.Empty
                    dr.Item("SHIHARAI_UNSO_WT") = "0"
                    dr.Item("SHIHARAI_COUNT") = "0"
                    dr.Item("SHIHARAI_UNCHIN") = "0"
                    dr.Item("SHIHARAI_TARIFF_BUNRUI_KB") = String.Empty
                    dr.Item("DECI_WT") = "0"
                    dr.Item("SHIHARAI_WT") = "0"
                    dr.Item("SHIHARAI_GROUP_NO") = String.Empty
                    dr.Item("SHIHARAI_GROUP_NO_M") = String.Empty
                End If

                dt.Rows.Add(dr)

            Next

        End With

        If ("01").Equals(frm.cmbShiharai.SelectedValue) = True Then
            Dim shiharaiUnchin As Decimal = 0
            '①件数加算の場合、支払金額の計算をする
            unsoDr = dt.Select(String.Empty, "UNSO_NO_L")
            If unsoDr.Length > 0 Then
                If ("0").Equals(frm.numKensu.TextValue) = True Then
                    '0だと、計算時に-1をして、マイナスになってしまうため
                    frm.numKensu.Value = 1
                End If
                'START YANAI 要望番号1424 支払処理
                'shiharaiUnchin = maxDeciUnchin + _
                '                Convert.ToDecimal(frm.numKingaku.Value) * _
                '                (Convert.ToDecimal(frm.numKensu.Value) - 1)
                shiharaiUnchin = deciUnchin + _
                                Convert.ToDecimal(frm.numKingaku.Value) * _
                                (Convert.ToDecimal(frm.numKensu.Value) - 1)
                'END YANAI 要望番号1424 支払処理
                unsoDr(0).Item("SHIHARAI_UNCHIN") = Convert.ToString(shiharaiUnchin)
            End If


            '②運賃を重量で按分する
            For i As Integer = 0 To max
                '重量の設定
                juryo = Convert.ToDecimal(Me._LMFconG.FormatNumValue(unsoDr(i).Item("SHIHARAI_WT").ToString()))

                '運賃の按分
                value = Me.AnbunData(shiharaiUnchin, soJuryo, juryo)
                unchin += value
                unsoDr(i).Item("DECI_UNCHIN") = value

            Next


            '③残額調整
            unsoDr(0).Item("DECI_UNCHIN") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(unsoDr(0).Item("DECI_UNCHIN").ToString())) + shiharaiUnchin - unchin


            '④まとめデータの場合、親に子の金額を加算。子は0を設定。
            Dim grpDr() As DataRow = Nothing
            For i As Integer = 0 To max
                If String.IsNullOrEmpty(unsoDr(i).Item("SHIHARAI_GROUP_NO").ToString) = False AndAlso _
                    ((unsoDr(i).Item("UNSO_NO_L").ToString).Equals(unsoDr(i).Item("SHIHARAI_GROUP_NO").ToString) = False ) Then
                    '親データを探して、加算する
                    grpDr = dt.Select(String.Concat("UNSO_NO_L = '", unsoDr(i).Item("SHIHARAI_GROUP_NO").ToString, "'"))
                    If grpDr.Length > 0 Then
                        grpDr(0).Item("DECI_UNCHIN") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("DECI_UNCHIN").ToString) + Convert.ToDouble(unsoDr(i).Item("DECI_UNCHIN").ToString))

                        unsoDr(i).Item("DECI_UNCHIN") = "0"
                    End If

                End If
            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 按分計算
    ''' </summary>
    ''' <param name="unchin">運賃</param>
    ''' <param name="soJuryo">総重量</param>
    ''' <param name="juryo">重量</param>
    ''' <returns>按分結果</returns>
    ''' <remarks>通常レコードの場合、自レコードの重量と総重量は同値になるので計算してよい</remarks>
    Private Function AnbunData(ByVal unchin As Decimal, ByVal soJuryo As Decimal, ByVal juryo As Decimal) As Decimal

        '総重量がZeroの場合、割当て無し
        If soJuryo = 0 Then
            Return 0
        End If

        '運賃 * 自レコードの重量 / 総重量(切捨て)
        Return System.Math.Floor(unchin * juryo / soJuryo)

    End Function

    ''' <summary>
    ''' 支払運賃データの設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiData(ByVal frm As LMF030F, ByVal ds As DataSet, ByVal deleteActionFlg As Boolean) As DataSet

        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMF810DS()
        Dim prmDt As DataTable = prmDs.Tables("LMF810UNCHIN_CALC_IN")
        Dim prmDr As DataRow = prmDt.NewRow()

        Dim dt As DataTable = ds.Tables(LMF030C.TABLE_NM_SHIHARAI)
        Dim dr As DataRow = Nothing
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim unsoLdr() As DataRow = Nothing
        Dim unsocoDr() As DataRow = Nothing
        Dim unchinTariffDr() As DataRow = Nothing

        Dim max As Integer = dt.Rows.Count - 1

        Dim deciKyori As String = String.Empty

        If deleteActionFlg = False Then
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
            ''保存時、運送会社が変わってない場合は、計算しない
            'If (frm.txtUnsocoCd.TextValue).Equals(frm.txtUnsocoCdOld.TextValue) = True AndAlso _
            '    (frm.txtUnsocoBrCd.TextValue).Equals(frm.txtUnsocoBrCdOld.TextValue) = True Then
            '    Return ds
            'End If
            '保存時、運送会社が変わってない　かつ ぎょうがへんしゅうされていない場合は、計算しない
            If ((frm.txtUnsocoCd.TextValue).Equals(frm.txtUnsocoCdOld.TextValue) = True AndAlso _
                (frm.txtUnsocoBrCd.TextValue).Equals(frm.txtUnsocoBrCdOld.TextValue) = True) AndAlso _
                (frm.txtIsEditRowFlg.TextValue).Equals("0") = True Then
                Return ds
            End If

            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End
        End If

        For i As Integer = 0 To max
            unsoLdr = ds.Tables(LMF030C.TABLE_NM_UNSO_L).Select(String.Concat("UNSO_NO_L = '", dt.Rows(i).Item("UNSO_NO_L").ToString, "'"))

            'データテーブルのクリア
            prmDt.Clear()
            prmDr = prmDt.NewRow()

            With prmDr

                '運賃計算プログラムのINパラメータ記入
                .Item("ACTION_FLG") = LMFControlC.FLG_OFF
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()

                '運賃計算プログラムのINパラメータ記入
                .Item("ACTION_FLG") = LMFControlC.FLG_OFF
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()

                '削除されたレコードの場合は運送(大)、削除されてないレコードの場合は運行
                If (LMConst.FLG.ON).Equals(unsoLdr(0).Item("SYS_DEL_FLG").ToString) = True Then
                    .Item("CUST_CD_L") = unsoLdr(0).Item("UNSO_CD").ToString()
                    .Item("CUST_CD_M") = unsoLdr(0).Item("UNSO_BR_CD").ToString()
                Else
                    .Item("CUST_CD_L") = frm.txtUnsocoCd.TextValue
                    .Item("CUST_CD_M") = frm.txtUnsocoBrCd.TextValue
                End If

                If deleteActionFlg = True Then
                    '削除処理時は問答無用で運送(大)の運送コードを設定
                    .Item("CUST_CD_L") = unsoLdr(0).Item("UNSO_CD").ToString()
                    .Item("CUST_CD_M") = unsoLdr(0).Item("UNSO_BR_CD").ToString()
                End If
                'unsocoDr = Me._LMFconG.SelectUnsocoListDataRow(.Item("CUST_CD_L").ToString, .Item("CUST_CD_M").ToString)
                '20160928 要番2622 tsunehira add
                unsocoDr = Me._LMFconG.SelectUnsocoListDataRow(.Item("NRS_BR_CD").ToString, .Item("CUST_CD_L").ToString, .Item("CUST_CD_M").ToString)

                .Item("DEST_CD") = unsoLdr(0).Item("DEST_CD").ToString()
                .Item("DEST_JIS") = unsoLdr(0).Item("DEST_JIS_CD").ToString()
                .Item("ARR_PLAN_DATE") = unsoLdr(0).Item("ARR_PLAN_DATE").ToString()
                .Item("UNSO_PKG_NB") = unsoLdr(0).Item("UNSO_PKG_NB").ToString()
                .Item("NB_UT") = dt.Rows(i).Item("SHIHARAI_PKG_UT").ToString
                .Item("UNSO_WT") = unsoLdr(0).Item("UNSO_WT").ToString()
                .Item("UNSO_ONDO_KB") = unsoLdr(0).Item("UNSO_ONDO_KB").ToString
                .Item("TARIFF_BUNRUI_KB") = dt.Rows(i).Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString
                .Item("VCLE_KB") = dt.Rows(i).Item("SHIHARAI_SYARYO_KB").ToString
                .Item("MOTO_DATA_KB") = unsoLdr(0).Item("MOTO_DATA_KB").ToString()

                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
                'If String.IsNullOrEmpty(unsocoDr(0).Item("UNCHIN_TARIFF_CD").ToString) = False Then
                '    .Item("SHIHARAI_TARIFF_CD") = unsocoDr(0).Item("UNCHIN_TARIFF_CD").ToString
                '    .Item("SHIHARAI_ETARIFF_CD") = unsocoDr(0).Item("EXTC_TARIFF_CD").ToString
                'Else
                '    .Item("SHIHARAI_TARIFF_CD") = dt.Rows(i).Item("SHIHARAI_TARIFF_CD").ToString
                '    .Item("SHIHARAI_ETARIFF_CD") = dt.Rows(i).Item("SHIHARAI_ETARIFF_CD").ToString
                'End If
                .Item("SHIHARAI_TARIFF_CD") = unsocoDr(0).Item("UNCHIN_TARIFF_CD").ToString
                .Item("SHIHARAI_ETARIFF_CD") = unsocoDr(0).Item("EXTC_TARIFF_CD").ToString
                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start

                .Item("UNSO_TTL_QT") = "0"
                .Item("SIZE_KB") = dt.Rows(i).Item("SIZE_KB").ToString()
                .Item("UNSO_DATE") = unsoLdr(0).Item("ARR_PLAN_DATE").ToString()
                .Item("CARGO_KB") = ""
                .Item("CAR_TP") = "00"
                .Item("WT_LV") = 0

                deciKyori = dt.Rows(i).Item("DECI_KYORI").ToString()
                unchinTariffDr = Me._LMFconG.SelectUnchinTariffListDataRow(unsocoDr(0).Item("UNCHIN_TARIFF_CD").ToString, String.Empty, unsoLdr(0).Item("ARR_PLAN_DATE").ToString())
                If 0 < unchinTariffDr.Length Then
                    If (LMFControlC.TABTP_KOKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) OrElse _
                        (LMFControlC.TABTP_TAKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) OrElse _
                        (LMFControlC.TABTP_JYUKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) Then
                        If String.IsNullOrEmpty(dr.Item("DEST_JIS_CD").ToString) = False Then
                            deciKyori = Mid(dr.Item("DEST_JIS_CD").ToString, 1, 2)
                        Else
                            deciKyori = "0"
                        End If
                    End If
                End If
                .Item("KYORI") = deciKyori
                .Item("DANGER_KB") = dt.Rows(i).Item("SHIHARAI_DANGER_KB").ToString()
                .Item("GOODS_CD_NRS") = ""


                'パラムに検索条件の追加
                prmDt.Rows.Add(prmDr)
                prm.ParamDataSet = prmDs

                '運賃計算プログラムの呼び出し
                LMFormNavigate.NextFormNavigate(Me, "LMF810", prm)

                '計算結果をOutのテーブルに設定
                Dim rtnDs As DataSet = prm.ParamDataSet
                Dim outTbl As DataTable = rtnDs.Tables("LMF810UNCHIN_CALC_OUT")

                'LMF810RESULTからエラーメッセージを取得する
                Dim errDr As DataRow = rtnDs.Tables("LMF810RESULT").Rows(0)
                Dim hantei As String = String.Empty

                Select Case errDr.Item("STATUS").ToString()

                    Case "00"

                        '運賃取得時のみ画面へ適用する
                        Dim outDr As DataRow = outTbl.Rows(0)

                        '運賃の設定
                        dt.Rows(i).Item("DECI_CITY_EXTC") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("CITY_EXTC").ToString()))
                        dt.Rows(i).Item("DECI_WINT_EXTC") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("WINT_EXTC").ToString()))
                        dt.Rows(i).Item("DECI_RELY_EXTC") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("RELY_EXTC").ToString()))
                        dt.Rows(i).Item("DECI_TOLL") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("TOLL").ToString()))
                        dt.Rows(i).Item("DECI_INSU") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("INSU").ToString()))
                        dt.Rows(i).Item("DECI_UNCHIN") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.FormatNumValue(outDr.Item("UNCHIN").ToString())))

                        dt.Rows(i).Item("SHIHARAI_TARIFF_CD") = .Item("SHIHARAI_TARIFF_CD").ToString
                        dt.Rows(i).Item("SHIHARAI_ETARIFF_CD") = .Item("SHIHARAI_ETARIFF_CD").ToString

                        '画面の値を反映
                        dt.Rows(i).Item("DECI_NG_NB") = unsoLdr(0).Item("UNSO_PKG_NB").ToString()
                        dt.Rows(i).Item("DECI_KYORI") = dt.Rows(i).Item("DECI_KYORI")
                        dt.Rows(i).Item("DECI_WT") = unsoLdr(0).Item("UNSO_WT").ToString()
                    Case Else

                        '運賃の設定
                        dt.Rows(i).Item("DECI_CITY_EXTC") = "0"
                        dt.Rows(i).Item("DECI_WINT_EXTC") = "0"
                        dt.Rows(i).Item("DECI_RELY_EXTC") = "0"
                        dt.Rows(i).Item("DECI_TOLL") = "0"
                        dt.Rows(i).Item("DECI_INSU") = "0"
                        dt.Rows(i).Item("DECI_UNCHIN") = "0"

                        dt.Rows(i).Item("SHIHARAI_TARIFF_CD") = .Item("SHIHARAI_TARIFF_CD").ToString
                        dt.Rows(i).Item("SHIHARAI_ETARIFF_CD") = .Item("SHIHARAI_ETARIFF_CD").ToString

                        '画面の値を反映
                        dt.Rows(i).Item("DECI_NG_NB") = unsoLdr(0).Item("UNSO_PKG_NB").ToString()
                        dt.Rows(i).Item("DECI_KYORI") = dt.Rows(i).Item("DECI_KYORI")
                        dt.Rows(i).Item("DECI_WT") = unsoLdr(0).Item("UNSO_WT").ToString()

                End Select

            End With

        Next

        Return ds

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    '2022.09.06 追加START
    ''' <summary>
    ''' 車輌マスタ情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCarInfoData(ByVal frm As LMF030F)

        Dim carKey As String = frm.lblCarKey.TextValue

        '値がない場合、スルー
        If String.IsNullOrEmpty(carKey) Then
            Return
        End If

        Dim inDs As New LMF030DS()
        Dim inDt As DataTable = inDs.Tables(LMF030C.TABLE_NM_IN_CAR)
        Dim inRow As DataRow

        'IN情報設定
        inRow = inDt.NewRow
        inRow("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
        inRow("CAR_KEY") = carKey
        inDt.Rows.Add(inRow)

        'データ取得
        Dim rtnDs As DataSet = Me._LMFconH.ServerAccess(inDs, LMF030C.ACTION_ID_CAR_SELECT)

        '取得データを設定
        Dim outSubDt As DataTable = rtnDs.Tables(LMF030C.TABLE_NM_OUT_CAR)

        'キーが一致するレコードを特定
        Dim rows As DataRow() = outSubDt.Select($"CAR_KEY = '{carKey}'")
        If rows.Length < 1 Then
            '1日料金
            frm.numPayAmt.Value = 0
        Else
            '1日料金
            frm.numPayAmt.Value = rows(0).Item("DAY_UNCHIN").ToString()
        End If

    End Sub
    '2022.09.06 追加END

#End Region

#Region "計算"

    ''' <summary>
    ''' 全行計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function AllCalculation(ByVal frm As LMF030F) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail

        With spr.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            Dim kosu As Decimal = 0
            Dim juryo As Decimal = 0
            Dim unchin As Decimal = 0
            For i As Integer = 0 To max

                '個数の合計
                kosu += Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(i, LMF030G.sprDetailDef.KOSU.ColNo))))

                '重量の合計
                juryo += Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(i, LMF030G.sprDetailDef.JURYO.ColNo))))

                '運賃の合計
                unchin += Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(.Cells(i, LMF030G.sprDetailDef.UNCHIN.ColNo))))

            Next

            '個数の上限チェック
            If Me._V.IsCalcOver(kosu.ToString(), LMFControlC.MIN_0, LMFControlC.MAX_KETA, frm.lblTitleUnsoNb.Text) = False Then
                frm.numUnsoNb.Value = LMFControlC.MAX_KETA
                Return False
            End If

            '重量の上限チェック
            If Me._V.IsCalcOver(unchin.ToString(), LMFControlC.MIN_0, LMFControlC.MAX_KETA_DEC.ToString(), frm.lblTitleTripWt.Text) = False Then
                frm.numTripWt.Value = LMFControlC.MAX_KETA_DEC
                Return False
            End If

            '運賃の上限チェック
            If Me._V.IsCalcOver(unchin.ToString(), LMFControlC.MIN_0, LMFControlC.MAX_KETA, frm.lblTitleRevUnchin.Text) = False Then
                frm.numRevUnchin.Value = LMFControlC.MAX_KETA
                Return False
            End If

            frm.numUnsoNb.Value = kosu
            frm.numTripWt.Value = juryo
            frm.numRevUnchin.Value = unchin

        End With

        Return True

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMF030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ShiftEditMode(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMF030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.DeleteAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMF030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMF030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveTripItemData(frm, LMF030C.ActionType.SAVE)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMF030F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMF030F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームのボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub LMF030F_KeyDown(ByVal frm As LMF030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 行追加ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnRowAdd_Click(ByVal frm As LMF030F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.AddUnsoLData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 行削除ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnRowDel_Click(ByVal frm As LMF030F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.DelUnsoLData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '要望番号2063 追加START 2015.05.27
    ''' <summary>
    ''' 手配作成ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnTehaiCreate_Click(ByVal frm As LMF030F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CreateTehaiData(frm, LMF030C.ActionType.TEHAICREATE)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    '要望番号2063 追加END 2015.05.27

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_CellDoubleClick(ByVal frm As LMF030F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 計算ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnShiharaiKeisan_Click(ByVal frm As LMF030F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ShiharaiKeisan(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' タリフ分類区分変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub cmbTariffKbn_Selected(ByVal frm As LMF030F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.tariffKbnSelected(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
