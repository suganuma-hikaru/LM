' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI730H : 運賃差分抽出（JXTG）
'  作  成  者       :  katagiri
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI730ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI730H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI730V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI730G

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
    ''' システム日付
    ''' </summary>
    ''' <remarks></remarks>
    Private _NowDate As String = MyBase.GetSystemDateTime(0)

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 検索結果のデータセットを保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _KensakuDs As DataSet

    ''' <summary>
    ''' アクサルタ同送処理フラグ
    ''' </summary>
    ''' <remarks>現在のデータがアクサルタ同送用で検索されたものであればTrue そうでなければFalseが設定される</remarks>
    Private _IsAxaltaDousouProcess As Boolean


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
        Dim frm As LMI730F = New LMI730F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Hnadler共通クラスの設定
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI730V(Me, frm, Me._LMFconV, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMI730G(Me, frm, Me._LMFconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Call Me._G.SetInitValue()

        'メッセージの表示
        Call Me.SetInitMessage(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

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
    Private Sub SelectListData(ByVal frm As LMI730F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI730C.ActionType.KENSAKU)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
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

        Dim rtnDs As DataSet = MyBase.CallWSA(BLF, Me.GetSelectActionId(frm), ds)

        '通常検索の場合
        Dim count As String = String.Empty
        count = MyBase.GetResultCount.ToString()

        '検索処理
        rtnDs = Me.SelectListData(frm, ds, rtnDs, blf, count)
        If rtnDs Is Nothing = True Then
            Exit Sub
        End If

        '検索結果を保持
        Me._KensakuDs = rtnDs.Copy

        '値の再設定
        If Me._G.SetSpread(rtnDs) = False Then
            Me._LMFconV.SetErrMessage("E117", New String() {frm.lblTitleSokei.Text, LMFControlC.MAX_KETA})
        End If

        Dim dt As DataTable = rtnDs.Tables(LMI730C.TABLE_NM_OUT)

        'Spread(フォント色指定)
        Call Me._G.SetSpreadColor(dt)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' バックアップ（運賃・タリフ）処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub BackupUnchin(ByVal frm As LMI730F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI730C.ActionType.BACKUP)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI730G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsBkChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMI730DS()

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetBackUpDataSet(frm, ds, arr)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnchinSaveData(frm, ds, LMI730C.ActionType.BACKUP)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F8ButtonName))

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMI730F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI730C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI730C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMI730C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI730F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMI730C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI730C.ActionType.ENTER)

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
        Call Me.ShowPopupControl(frm, objNm, LMI730C.ActionType.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SaveUnchinItemData(ByVal frm As LMI730F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI730C.ActionType.SAVE)

        '一括変更チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI730G.sprDetailDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsSaveChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF040DS()
        arr = Me.IsSaveSelectChk(frm, ds, arr)

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        If rtnResult = False Then
            If MyBase.IsMessageStoreExist() = True Then
                'EXCEL起動 
                MyBase.MessageStoreDownload(True)
                MyBase.ShowMessage(frm, "E235")
            End If

            '処理終了アクション
            Call Me.EndAction(frm)

        End If

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetSaveDataSet(frm, ds, arr, LMI730C.ActionType.SAVE)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnchinSaveData(frm, ds, LMI730C.ActionType.SAVE)

        '一括更新終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.btnHenko.Text))

        '一覧の更新
        rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMI730F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        If rowNo > 0 Then

            '処理開始アクション
            Call Me.StartAction(frm)

            If Me._V.IsAuthority(LMI730C.ActionType.DOUBLECLICK) = False Then

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            '送信するデータセットに検索条件を設定
            Dim ds As DataSet = Me.SetUnchinPkData(frm, rowNo)
            Dim unsoNo As String = ds.Tables(LMF040C.TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString()
            Dim unsoNoM As String = ds.Tables(LMF040C.TABLE_NM_IN).Rows(0).Item("UNSO_NO_M").ToString()

            '==========================
            'WSAクラス呼出
            '==========================

            '強制実行フラグの設定
            MyBase.SetForceOparation(False)

            '閾値の設定(0より大きければ何でもよい)
            MyBase.SetLimitCount(Me._LMFconG.GetLimitData())

            'Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(MyBase.GetPGID(), LMFControlC.BLF), LMF010C.ACTION_ID_SELECT, ds)
            Dim rtnDs As DataSet = MyBase.CallWSA("LMF040BLF", LMF010C.ACTION_ID_SELECT, ds)

            'エラー判定
            If MyBase.IsMessageExist() = True Then

                'メッセージ設定
                Call Me._LMFconV.SetMstErrMessage("運賃テーブル(Freight Table)", Me._LMFconG.EditConcatData(unsoNo, unsoNoM))

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            'パラメータ設定
            Dim prmDs As DataSet = New LMF050DS()
            Dim prmDt As DataTable = prmDs.Tables(LMF050C.TABLE_NM_IN)
            prmDt.ImportRow(rtnDs.Tables(LMF040C.TABLE_NM_OUT).Rows(0))

            Call Me.OpenUnchinEditGamen(prmDs, RecordStatus.NOMAL_REC)

            '処理終了アクション
            Call Me.EndAction(frm)

        End If

    End Sub

    ''' <summary>
    ''' 変更対象項目 変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub HenkoChangeAction(ByVal frm As LMI730F)

        'ロック制御
        Call Me._G.LockHenkoChangeControl()

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
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMI730F, ByVal objNm As String, ByVal actionType As LMI730C.ActionType) As Boolean

        With frm
            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtTariffCd.Name

                    Call Me.SetReturnTariffPop(frm, objNm, actionType)

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Call Me.SetReturnCustPop(frm, objNm, actionType)

                Case .txtShuseiL.Name, .txtShuseiM.Name, .txtShuseiS.Name, .txtShuseiSS.Name

                    Call Me.SetShuseiReturnPop(frm, objNm, actionType)

                Case .txtDestCd.Name

                    Call Me.SetReturnDestPop(frm, actionType)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="tariffKbn">タリフ分類</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMI730F, ByVal objNm As String, ByVal actionType As LMI730C.ActionType, Optional ByVal tariffKbn As String = "") As Boolean

        Dim prm As LMFormData = Nothing
        Dim tblNm As String = String.Empty
        Dim code As String = String.Empty
        Dim name As String = String.Empty

        With frm

            If LMFControlC.TARIFF_YOKO.Equals(tariffKbn) = True Then

                '横持ちタリフPop
                prm = Me.ShowYokoTariffPopup(frm, objNm, actionType)
                tblNm = LMZ100C.TABLE_NM_OUT
                code = "YOKO_TARIFF_CD"
                name = "YOKO_REM"

            Else

                '運賃タリフPop
                prm = Me.ShowUnchinTariffPopup(frm, objNm, actionType)
                tblNm = LMZ230C.TABLE_NM_OUT
                code = "UNCHIN_TARIFF_CD"
                name = "UNCHIN_TARIFF_REM"

            End If

            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(tblNm).Rows(0)

                Select Case objNm

                    Case .txtTariffCd.Name

                        .txtTariffCd.TextValue = dr.Item(code).ToString()
                        .lblTariffNm.TextValue = dr.Item(name).ToString()

                    Case Else

                        .txtShuseiL.TextValue = dr.Item(code).ToString()

                End Select

                Return True

            End If

        End With

        Return False

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMI730F, ByVal objNm As String, ByVal actionType As LMI730C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        Dim brCd As String = String.Empty
        Dim tariff As String = String.Empty
        Dim csFlg As String = String.Empty

        Select Case objNm

            Case frm.txtTariffCd.Name

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                tariff = frm.txtTariffCd.TextValue
                csFlg = LMConst.FLG.ON

            Case Else

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                tariff = frm.txtShuseiL.TextValue
                csFlg = LMConst.FLG.OFF

        End Select

        With dr
            .Item("NRS_BR_CD") = brCd
            If actionType = LMI730C.ActionType.ENTER Then
                .Item("UNCHIN_TARIFF_CD") = tariff
            End If
            .Item("STR_DATE") = Me._NowDate
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csFlg
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMI730F, ByVal objNm As String, ByVal actionType As LMI730C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        Dim brCd As String = String.Empty
        Dim yoko As String = String.Empty
        Dim csFlg As String = String.Empty

        Select Case objNm

            Case frm.txtTariffCd.Name

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                yoko = frm.txtTariffCd.TextValue
                csFlg = LMConst.FLG.ON

            Case Else

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                yoko = frm.txtShuseiL.TextValue
                csFlg = LMConst.FLG.OFF

        End Select

        With dr
            .Item("NRS_BR_CD") = brCd
            If actionType = LMI730C.ActionType.ENTER Then
                .Item("YOKO_TARIFF_CD") = yoko
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csFlg
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)

    End Function


    ''' <summary>
    ''' 割増タリフPopの戻り値を設定(一括変更部)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnExtcPop2(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowExtcPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtShuseiL.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowExtcPopup(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 距離Popの戻り値を設定(一括変更部)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnKyoriPop(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowKyoriPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ080C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtShuseiL.TextValue = dr.Item("KYORI_CD").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 距離マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowKyoriPopup(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ080DS()
        Dim dt As DataTable = ds.Tables(LMZ080C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            If actionType = LMI730C.ActionType.ENTER Then
                .Item("KYORI_CD") = frm.txtShuseiL.TextValue
            End If
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ080", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 乗務員マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowDriverPopup(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ160DS()
        Dim dt As DataTable = ds.Tables(LMZ160C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ160", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMI730F, ByVal objNm As String, ByVal actionType As LMI730C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, objNm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                Select Case objNm

                    Case .txtCustCdL.Name, .txtCustCdM.Name

                        .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                        .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                        .lblCustNm.TextValue = String.Concat(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString())

                    Case Else

                        .txtShuseiL.TextValue = dr.Item("CUST_CD_L").ToString()
                        .txtShuseiM.TextValue = dr.Item("CUST_CD_M").ToString()
                        .txtShuseiS.TextValue = dr.Item("CUST_CD_S").ToString()
                        .txtShuseiSS.TextValue = dr.Item("CUST_CD_SS").ToString()

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
    Private Function ShowCustPopup(ByVal frm As LMI730F, ByVal objNm As String, ByVal actionType As LMI730C.ActionType) As LMFormData

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

            Case frm.txtCustCdL.Name, frm.txtCustCdM.Name

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                custL = frm.txtCustCdL.TextValue
                custM = frm.txtCustCdM.TextValue
                csCd = LMConst.FLG.ON

            Case Else

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'brCd = LMUserInfoManager.GetNrsBrCd()
                brCd = frm.cmbEigyo.SelectedValue.ToString()
                custL = frm.txtShuseiL.TextValue
                custM = frm.txtShuseiM.TextValue
                custS = frm.txtShuseiS.TextValue
                custSS = frm.txtShuseiSS.TextValue
                csCd = LMConst.FLG.OFF

        End Select

        With dr

            .Item("NRS_BR_CD") = brCd
            If actionType = LMI730C.ActionType.ENTER Then
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

    ''' <summary>
    ''' 請求先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSeiqtoPop(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowSeiqtoPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtShuseiL.TextValue = dr.Item("SEIQTO_CD").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowSeiqtoPopup(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If actionType = LMI730C.ActionType.ENTER Then
                .Item("SEIQTO_CD") = frm.txtShuseiL.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    '''  <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowDestPopup(frm, actionType)
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
    Private Function ShowDestPopup(ByVal frm As LMI730F, ByVal actionType As LMI730C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue

            If actionType = LMI730C.ActionType.ENTER Then
                .Item("DEST_CD") = frm.txtDestCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("RELATION_SHOW_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 修正項目のPop設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetShuseiReturnPop(ByVal frm As LMI730F, ByVal objNm As String, ByVal actionType As LMI730C.ActionType) As Boolean

        SetShuseiReturnPop = False

        Select Case frm.cmbShusei.SelectedValue.ToString()

            Case LMI730C.SHUSEI_SEIQTO

                SetShuseiReturnPop = Me.SetReturnSeiqtoPop(frm, actionType)

            Case LMI730C.SHUSEI_TARIFF

                SetShuseiReturnPop = Me.SetReturnTariffPop(frm, objNm, actionType, LMFControlC.TARIFF_KONSAI)

            Case LMI730C.SHUSEI_YOKO

                SetShuseiReturnPop = Me.SetReturnTariffPop(frm, objNm, actionType, LMFControlC.TARIFF_YOKO)

            Case LMI730C.SHUSEI_CUST

                SetShuseiReturnPop = Me.SetReturnCustPop(frm, objNm, actionType)

            Case LMI730C.SHUSEI_ETARIFF

                SetShuseiReturnPop = Me.SetReturnExtcPop2(frm, actionType)

            Case LMI730C.SHUSEI_KYORI

                SetShuseiReturnPop = Me.SetReturnKyoriPop(frm, actionType)

        End Select

        Return SetShuseiReturnPop

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索部データ)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetConditionDataSet(ByVal frm As LMI730F) As DataSet

        Dim ds As DataSet = New LMI730DS()
        Dim dt As DataTable = ds.Tables(LMI730C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim custDetailsDr() As DataRow = Nothing

        'ヘッダ項目
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("DATE_KBN") = .cmbDateKb.SelectedValue.ToString()
            dr.Item("DATE_FROM") = .imdFrom.TextValue
            dr.Item("DATE_TO") = .imdTo.TextValue
            dr.Item("KAKUTEI_KB") = Me.SetKakutei(frm)
            dr.Item("SEIQ_TARIFF_CD") = .txtTariffCd.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("DEST_CD") = .txtDestCd.TextValue
            custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue.ToString(), "' AND ", _
                                                                                                            "CUST_CD = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                            "SUB_KB = '33'"))
            If custDetailsDr.Length > 0 Then
                dr.Item("MATOME_KB") = custDetailsDr(0).Item("SET_NAIYO").ToString
            Else
                dr.Item("MATOME_KB") = String.Empty
            End If
        End With

        'スプレッド項目
        With frm.sprDetail.ActiveSheet

            dr.Item("CUST_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.CUST_NM.ColNo))
            dr.Item("SEIQTO_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.SEIQTO_CD.ColNo))
            dr.Item("SEIQTO_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.SEIQTO_NM.ColNo))
            dr.Item("DEST_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.DEST_NM.ColNo))
            dr.Item("UNSO_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSO_NM.ColNo))
            dr.Item("UNSOCO_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSOCO_NM.ColNo))
            dr.Item("TAX_KB") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.ZEI_KBN.ColNo))
            dr.Item("SEIQ_GROUP_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.GROUP.ColNo))
            dr.Item("SEIQ_GROUP_NO_M") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.GROUP_M.ColNo))
            dr.Item("REMARK") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.REMARK.ColNo))
            dr.Item("INOUTKA_NO_L") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.KANRI_NO.ColNo))
            dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSO_NO.ColNo))
            dr.Item("UNSO_NO_M") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSO_NO_EDA.ColNo))
            dr.Item("TRIP_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.TRIP_NO.ColNo))
            dr.Item("SYUKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.SHUKA_RELY_POINT.ColNo))
            dr.Item("HAIKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.HAIKA_RELY_POINT.ColNo))
            dr.Item("TRIP_NO_SYUKA") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.TRIP_NO_SHUKA.ColNo))
            dr.Item("TRIP_NO_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.TRIP_NO_CHUKEI.ColNo))
            dr.Item("TRIP_NO_HAIKA") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.TRIP_NO_HAIKA.ColNo))
            dr.Item("UNSOCO_SYUKA") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSOCO_SHUKA.ColNo))
            dr.Item("UNSOCO_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSOCO_CHUKEI.ColNo))
            dr.Item("UNSOCO_HAIKA") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSOCO_HAIKA.ColNo))
            dr.Item("DEST_JIS_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.DEST_JIS_CD.ColNo))
            dr.Item("UNSO_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSO_CD.ColNo))
            dr.Item("UNSO_BR_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSO_BR_CD.ColNo))
            dr.Item("UNSOCO_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSOCO_CD.ColNo))
            dr.Item("UNSOCO_BR_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.UNSOCO_BR_CD.ColNo))
            dr.Item("CUST_REF_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.CUST_REF_NO.ColNo))
            dr.Item("ZBUKA_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.ZBUKA_CD.ColNo))
            dr.Item("ABUKA_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.ABUKA_CD.ColNo))
            dr.Item("BIN_KB") = Me._LMFconG.GetCellValue(.Cells(0, LMI730G.sprDetailDef.BIN_NM.ColNo))
        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃PK設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinPkData(ByVal frm As LMI730F, ByVal rowNo As Integer) As DataSet

        Dim ds As DataSet = New LMF040DS()
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With frm.sprDetail.ActiveSheet

            dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.NRS_BR_CD.ColNo))
            dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.UNSO_NO.ColNo))
            dr.Item("UNSO_NO_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.UNSO_NO_EDA.ColNo))

        End With

        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 更新情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetSaveDataSet(ByVal frm As LMI730F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal actionType As LMI730C.ActionType) As Boolean

        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim rowNo As Integer = 0

        '設定内容
        Dim fixFlg As String = String.Empty
        Dim item As String = String.Empty
        Dim cdL As String = String.Empty
        Dim cdM As String = String.Empty
        Dim cdS As String = String.Empty
        Dim cdSS As String = String.Empty
        Dim calcKbn As String = frm.lblCalcKbn.TextValue

        With frm

            'イベントによる切り分け
            Select Case actionType

                Case LMI730C.ActionType.SAVE

                    item = .cmbShusei.SelectedValue.ToString()
                    cdL = .txtShuseiL.TextValue
                    cdM = .txtShuseiM.TextValue
                    cdS = .txtShuseiS.TextValue
                    cdSS = .txtShuseiSS.TextValue

            End Select

        End With

        '前ゼロ用
        Dim ketasu As Integer = Me._LMFconG.GetLimitData(LMI730C.IKKATU_LMI730, LMKbnConst.KBN_I004).ToString().Length
        Dim keta As String = Me._LMFconG.GetZeroData(ketasu)
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_UNCHIN)
        Dim dr As DataRow = Nothing

        frm.lblSysUpdDate.TextValue = MyBase.GetSystemDateTime(0)
        frm.lblSysUpdTime.TextValue = MyBase.GetSystemDateTime(1)

        Dim custDetailsDr() As DataRow = Nothing

        With spr.ActiveSheet

            For i As Integer = 0 To max

                'インスタンス生成
                dr = dt.NewRow()

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                'スプレッドの値を設定
                dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.UNSO_NO.ColNo))
                dr.Item("UNSO_NO_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.UNSO_NO_EDA.ColNo))
                dr.Item("SEIQ_GROUP_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.GROUP.ColNo))
                dr.Item("SEIQ_GROUP_NO_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.GROUP_M.ColNo))
                dr.Item("SEIQ_TARIFF_BUNRUI_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.TARIFF_KBN.ColNo))
                dr.Item("OUTKA_PLAN_DATE") = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.SHUKKA.ColNo)))
                dr.Item("ARR_PLAN_DATE") = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.NONYU.ColNo)))
                dr.Item("MOTO_DATA_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.MOTO_DATA_KBN.ColNo))
                dr.Item("UNTIN_CALCULATION_KB") = Me.SetCalcKbn(spr, rowNo, actionType, item, calcKbn)
                dr.Item("REMARK") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.REMARK.ColNo))
                dr.Item("VCLE_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.VCLE_KB.ColNo))
                dr.Item("UNSO_ONDO_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.UNSO_ONDO_KB.ColNo))
                dr.Item("SIZE_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.SIZE_KB.ColNo))
                dr.Item("CUST_CD_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.CUST_CD_M.ColNo))
                dr.Item("DEST_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.DEST_CD.ColNo))
                dr.Item("DEST_JIS") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.DEST_JIS_CD.ColNo))
                dr.Item("SEIQTO_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.SEIQTO_CD.ColNo))
                dr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.SYS_UPD_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.SYS_UPD_TIME.ColNo))

                '確定フラグの設定
                dr.Item("SEIQ_FIXED_FLAG") = fixFlg

                '一括変更の値設定
                dr.Item("ITEM_DATA") = item
                dr.Item("CD_L") = cdL
                dr.Item("CD_M") = cdM
                dr.Item("CD_S") = cdS
                dr.Item("CD_SS") = cdSS

                'チェック用
                dr.Item("ROW_NO") = Me._LMFconG.SetMaeZeroData(rowNo.ToString(), ketasu, keta)

                dr.Item("NEW_SYS_UPD_DATE") = frm.lblSysUpdDate.TextValue
                dr.Item("NEW_SYS_UPD_TIME") = frm.lblSysUpdTime.TextValue
                dr.Item("SYS_UPD_FLG") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.SYS_UPD_FLG.ColNo))

                dr.Item("CHECK_FLG") = String.Empty
                dr.Item("MATOME_KB") = String.Empty
                dr.Item("BIN_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.BIN_KB.ColNo))
                dr.Item("ZBUKA_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.ZBUKA_CD.ColNo))
                dr.Item("ABUKA_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.ABUKA_CD.ColNo))
                dr.Item("MINASHI_DEST_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.MINASHI_DEST_CD.ColNo))

                '行追加
                dt.Rows.Add(dr)

            Next

        End With

        Return True

    End Function


    Private Function SetBackUpDataSet(ByVal frm As LMI730F, ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim rowNo As Integer = 0

        Dim dt As DataTable = ds.Tables(LMI730C.TABLE_NM_BKUNCHIN)
        Dim dr As DataRow = Nothing

        Dim custDetailsDr() As DataRow = Nothing

        With spr.ActiveSheet

            For i As Integer = 0 To max

                'インスタンス生成
                dr = dt.NewRow()

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                'スプレッドの値を設定
                dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.UNSO_NO.ColNo))
                dr.Item("TARIFF_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.TARIFF_CD.ColNo))
                dr.Item("UNCHIN") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.UNCHIN.ColNo))

                Dim beforeTariff As String = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.BEFORE_TARIFF_CD.ColNo))
                Dim beforeUnchin As String = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.BEFORE_UNCHIN.ColNo))
                '追加・更新かの判断フラグ
                If (String.IsNullOrEmpty(beforeTariff) AndAlso _
                    beforeUnchin.Equals("0")) Then

                    '追加データ
                    dr.Item("UPD_FLG") = False
                Else

                    '更新データ
                    dr.Item("UPD_FLG") = True

                End If
                '行追加
                dt.Rows.Add(dr)

            Next

        End With

        Return True

    End Function


    ''' <summary>
    ''' 締め日区分の設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="shuseiKbn">修正区分</param>
    ''' <param name="calcKbn">ヘッダ項目の締め日区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCalcKbn(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer, ByVal actionType As LMI730C.ActionType, ByVal shuseiKbn As String, ByVal calcKbn As String) As String

        SetCalcKbn = String.Empty

        With spr.ActiveSheet

            '一括変更以外 または 荷主を更新しない場合、スプレッドの値
            If LMI730C.ActionType.SAVE <> actionType _
                AndAlso LMI730C.SHUSEI_CUST.Equals(shuseiKbn) = False _
                Then
                Return Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo))
            End If

            '隠し項目の値を設定
            Return calcKbn

        End With

    End Function


#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMI730F)

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
    Private Sub EndAction(ByVal frm As LMI730F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub



    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionTyp">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UnchinSaveData(ByVal frm As LMI730F, ByVal ds As DataSet, ByVal actionTyp As LMI730C.ActionType) As Boolean

        Dim msg As String = String.Empty
        Dim actionId As String = String.Empty
        Dim blfName As String = String.Empty
        Select Case actionTyp

            Case LMI730C.ActionType.SAVE

                actionId = LMI730C.ACTION_ID_SAVE
                msg = frm.btnHenko.Text
                blfName = "LMF040BLF"

            Case LMI730C.ActionType.BACKUP

                actionId = LMI730C.ACTION_ID_INSERT
                msg = frm.FunctionKey.F8ButtonName
                blfName = "LMI730BLF"

        End Select

        '確認メッセージ表示
        If Me._LMFconH.SetMessageC001(frm, msg) = False Then
            Return False
        End If

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, actionId, blfName, rtnDs) = False Then
            Return False
        End If

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
    Private Function ActionData(ByVal frm As LMI730F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , ByVal blfName As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        'サーバアクセス
        'rtnDs = Me._LMFconH.ServerAccess(ds, actionId)
        rtnDs = MyBase.CallWSA(blfName, actionId, ds)

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        'エラーが保持されている場合、False
        If MyBase.IsMessageStoreExist = True Then
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
    Private Function SelectListData(ByVal frm As LMI730F _
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
                    rtnDs = MyBase.CallWSA(blf, LMI730C.ACTION_ID_SELECT, ds)

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

    ''' <summary>
    ''' 検索処理のアクションIDを取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>アクションID</returns>
    ''' <remarks></remarks>
    Private Function GetSelectActionId(ByVal frm As LMI730F) As String

        GetSelectActionId = String.Empty


        GetSelectActionId = LMI730C.ACTION_ID_SELECT

        Return GetSelectActionId

    End Function

    ''' <summary>
    ''' 確定有無の条件設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SetKakutei(ByVal frm As LMI730F) As String

        SetKakutei = String.Empty

        With frm

            '未確定検索
            If .optRevMi.Checked = True Then
                SetKakutei = LMFControlC.FLG_OFF
            End If

            '確定済検索
            If .optRevKaku.Checked = True Then
                SetKakutei = LMFControlC.FLG_ON
            End If

        End With

        Return SetKakutei

    End Function

#Region "再計算"

    ''' <summary>
    ''' 区分名1を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>区分名1</returns>
    ''' <remarks></remarks>
    Private Function GetKbnData(ByVal kbnCd As String, ByVal groupCd As String) As String

        GetKbnData = String.Empty
        Dim drs As DataRow() = Me._LMFconG.SelectKbnListDataRow(kbnCd, groupCd)
        If 0 < drs.Length Then
            GetKbnData = drs(0).Item("KBN_NM1").ToString()
        End If

        Return GetKbnData

    End Function

    ''' <summary>
    ''' 運賃更新処理(再計算時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub UpdUnchinData(ByVal ds As DataSet, ByVal frm As LMI730F)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdUnchinData")

        '==========================
        'WSAクラス呼出
        '==========================
        If _IsAxaltaDousouProcess Then
            ds = MyBase.CallWSA("LMF040BLF", "UpdUnchinDataAxaltaDousou", ds)
        Else
            ds = MyBase.CallWSA("LMF040BLF", "UpdUnchinData", ds)
        End If

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName, ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdUnchinData")

    End Sub

#End Region

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMI730F)

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
    Private Sub SetInitMessage(ByVal frm As LMI730F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region

#Region "別PG起動処理"

    ''' <summary>
    ''' 運賃編集画面を起動
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="recType">レコードタイプ</param>
    ''' <remarks></remarks>
    Private Sub OpenUnchinEditGamen(ByVal ds As DataSet, ByVal recType As String)

        '画面起動
        Call Me._LMFconH.FormShow(ds, "LMF050", recType)

    End Sub

#End Region

#Region "チェック"

#Region "各処理のチェック"

    ''' <summary>
    ''' 一括変更チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSelectChk(ByVal frm As LMI730F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        With frm

            Dim max As Integer = arr.Count - 1
            Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_ERR)
            Dim dr As DataRow = Nothing
            Dim rtnResult As Boolean = True
            Dim msg As String = Me._LMFconV.SetRepMsgData(.btnHenko.Text)
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0
            Dim cdL As String = .txtShuseiL.TextValue
            Dim cdM As String = .txtShuseiM.TextValue
            Dim shusei As String = .cmbShusei.SelectedValue.ToString()

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                '自営業チェック
                rtnResult = Me.IsMyNrsChk(spr, rowNo, msg)

                '荷主の同値チェック
                rtnResult = rtnResult And Me.IsCustDotiChk(frm, shusei, cdL, cdM, rowNo)

                'タリフマスタの存在チェック
                rtnResult = rtnResult And Me.IsUnchinExistChk(frm, rowNo)

                '割増タリフマスタの存在チェック
                rtnResult = rtnResult And Me.IsETariffExistChk(frm, rowNo)

                'エラーがある場合、DataTableに設定
                Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

            Next

            'エラーになったものを削除
            arr = Me._LMFconV.SetErrDt(dt, arr)

            Return arr

        End With

    End Function

#End Region

#Region "チェックMethod"

    ''' <summary>
    ''' 自営業チェック
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMyNrsChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer, ByVal msg As String) As Boolean

        ''自営業でない場合、エラー
        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI730G.sprDetailDef.NRS_BR_CD.ColNo))) = False Then
        '    Return Me.SetIkkatuErrData("E178", New String() {Me._LMFconV.SetRepMsgData(msg)}, rowNo)
        'End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主コードの同値チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="shusei">変更対象</param>
    ''' <param name="cdL">(大)コード</param>
    ''' <param name="cdM">(中)コード</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustDotiChk(ByVal frm As LMI730F, ByVal shusei As String, ByVal cdL As String, ByVal cdM As String, ByVal rowNo As Integer) As Boolean

        With frm.sprDetail.ActiveSheet

            '変更対象が荷主でない場合、スルー
            If LMI730C.SHUSEI_CUST.Equals(shusei) = False Then
                Return True
            End If

            '荷主(大)コードが違う場合、エラー
            If cdL.Equals(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.CUST_CD_L.ColNo))) = False Then
                Return Me.SetIkkatuErrData("E227", New String() {LMI730C.CUST_CD}, rowNo)
                'END YANAI 要望番号498
            End If

            '荷主(中)コードが違う場合、エラー
            If cdM.Equals(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI730G.sprDetailDef.CUST_CD_M.ColNo))) = False Then
                Return Me.SetIkkatuErrData("E227", New String() {LMI730C.CUST_CD}, rowNo)
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinExistChk(ByVal frm As LMI730F, ByVal rowNo As Integer) As Boolean

        With frm

            '一括変更がタリフコードでない場合、スルー
            If LMI730C.SHUSEI_TARIFF.Equals(.cmbShusei.SelectedValue.ToString()) = False Then
                Return True
            End If

            '適用開始日の設定
            Dim chkDate As String = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMI730G.sprDetailDef.SHUKKA.ColNo)))
            If LMFControlC.CALC_NYUKA.Equals(Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMI730G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo))) = True Then
                chkDate = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMI730G.sprDetailDef.NONYU.ColNo)))
            End If

            '取得できない場合、エラー
            Dim tariffCd As String = .txtShuseiL.TextValue
            Dim drs As DataRow() = Me._LMFconG.SelectUnchinTariffListDataRow(tariffCd, String.Empty, chkDate)
            If drs.Length < 1 Then
                Return Me.SetIkkatuErrData("E079", New String() {"運賃タリフマスタ", tariffCd}, rowNo)
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 割増タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsETariffExistChk(ByVal frm As LMI730F, ByVal rowNo As Integer) As Boolean

        With frm

            '一括変更が割増タリフコードでない場合、スルー
            If LMI730C.SHUSEI_ETARIFF.Equals(.cmbShusei.SelectedValue.ToString()) = False Then
                Return True
            End If

            '適用開始日の設定
            Dim nrsbrcd As String = Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMI730G.sprDetailDef.NRS_BR_CD.ColNo))

            '取得できない場合、エラー
            Dim tariffCd As String = .txtShuseiL.TextValue
            Dim drs As DataRow() = Me._LMFconG.SelectExtcUnchinListDataRow(nrsbrcd, tariffCd, String.Empty)
            If drs.Length < 1 Then
                Return Me.SetIkkatuErrData("E079", New String() {"割増運賃タリフマスタ", tariffCd}, rowNo)
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 一括更新時のメッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetIkkatuErrData(ByVal id As String, ByVal rowNo As Integer) As Boolean

        MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, id, , rowNo.ToString())
        Return False

    End Function

    ''' <summary>
    ''' 一括更新時のメッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetIkkatuErrData(ByVal id As String, ByVal msg As String(), ByVal rowNo As Integer) As Boolean

        MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, id, msg, rowNo.ToString())
        Return False

    End Function

#End Region

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMI730F, ByVal e As System.Windows.Forms.KeyEventArgs)


    End Sub

    Friend Sub FunctionKey8Press(ByVal frm As LMI730F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.BackupUnchin(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub


    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI730F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByVal frm As LMI730F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByVal frm As LMI730F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI730F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_CellDoubleClick(ByVal frm As LMI730F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 変更項目の値変更時に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbShusei_SelectedValueChanged(ByVal frm As LMI730F, ByVal e As System.EventArgs)

        Call Me.HenkoChangeAction(frm)

    End Sub

    ''' <summary>
    ''' ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub btnHenko_Click(ByVal frm As LMI730F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveUnchinItemData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI730F_KeyDown(ByVal frm As LMI730F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
