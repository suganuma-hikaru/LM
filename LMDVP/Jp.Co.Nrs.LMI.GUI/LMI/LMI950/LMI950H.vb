' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI950H : 運賃データ確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI950ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI950H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI950V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI950G

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
        Dim frm As LMI950F = New LMI950F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Hnadler共通クラスの設定
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI950V(Me, frm, Me._LMFconV, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMI950G(Me, frm, Me._LMFconG)

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

        '初期値設定
        Call Me._G.SetInitValue()

        'メッセージの表示
        Call Me.SetInitMessage(frm)

        'フォームの表示
        frm.Show()

        'フォーカスの設定
        Call Me._G.SetFoucus()

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
    Private Sub SelectListData(ByVal frm As LMI950F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI950C.ActionType.KENSAKU)

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

        '値の設定
        Me._G.SetSpread(rtnDs) 

        '色の設定
        Call Me._G.SetSpreadColor()

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JissekiSakusei(ByVal frm As LMI950F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI950C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI950G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsTargetValid(frm, arr)

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
        rtnResult = rtnResult AndAlso Me.InsertSendUnchin(frm, ds, LMI950C.ActionType.LOOPEDIT)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F1ButtonName))

        '一覧の更新
        rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr)

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
    Private Sub OpenMasterPop(ByVal frm As LMI950F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI950C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI950C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMI950C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI950F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMI950C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI950C.ActionType.ENTER)

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
        Call Me.ShowPopupControl(frm, objNm, LMI950C.ActionType.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

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
    Private Function ShowPopupControl(ByVal frm As LMI950F, ByVal objNm As String, ByVal actionType As LMI950C.ActionType) As Boolean

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
    Private Function SetReturnCustPop(ByVal frm As LMI950F, ByVal objNm As String, ByVal actionType As LMI950C.ActionType) As Boolean

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
    Private Function ShowCustPopup(ByVal frm As LMI950F, ByVal objNm As String, ByVal actionType As LMI950C.ActionType) As LMFormData

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
            If actionType = LMI950C.ActionType.ENTER Then
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
    Private Function SetConditionDataSet(ByVal frm As LMI950F) As DataSet

        Dim ds As DataSet = New LMI950DS()
        Dim dt As DataTable = ds.Tables(LMI950C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'ヘッダ項目
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("OUTKA_DATE") = .imdOutkaDate.TextValue

        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 実績作成対象情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetSendUnchinTarget(ByVal frm As LMI950F, ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim rowNo As Integer = 0

        Dim dt As DataTable = ds.Tables(LMI950C.TABLE_NM_SENDUNCHIN_TARGET)
        Dim dr As DataRow = Nothing

        With spr.ActiveSheet

            For i As Integer = 0 To max

                'インスタンス生成
                dr = dt.NewRow()

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                'スプレッドの値を設定
                dr.Item("KOJO_KANRI_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.KOJO_KANRI_NO.ColNo))
                dr.Item("OUTKA_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.OUTKA_NO_L.ColNo))
                dr.Item("CRT_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.CRT_DATE.ColNo))
                dr.Item("FILE_NAME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.FILE_NAME.ColNo))
                dr.Item("REC_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.REC_NO.ColNo))
                dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.UNSO_NO_L.ColNo))
                dr.Item("UNSO_L_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.UNSO_L_UPD_DATE.ColNo))
                dr.Item("UNSO_L_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.UNSO_L_UPD_TIME.ColNo))
                dr.Item("UNCHIN_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.UNCHIN_UPD_DATE.ColNo))
                dr.Item("UNCHIN_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI950G.sprDetailDef.UNCHIN_UPD_TIME.ColNo))

                '行追加
                dt.Rows.Add(dr)

            Next

        End With

        Return True

    End Function

#End Region 'DataSet設定

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMI950F)

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
    Private Sub EndAction(ByVal frm As LMI950F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionTyp">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertSendUnchin(ByVal frm As LMI950F, ByVal ds As DataSet, ByVal actionTyp As LMI950C.ActionType) As Boolean

        Dim msg As String = String.Empty
        Dim actionId As String = String.Empty
        Dim blfName As String = String.Empty
        Select Case actionTyp

            Case LMI950C.ActionType.LOOPEDIT

                actionId = LMI950C.ACTION_ID_INSERT_SENDUNCHIN
                msg = frm.FunctionKey.F1ButtonName
                blfName = "LMI950BLF"

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
    Private Function ActionData(ByVal frm As LMI950F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , ByVal blfName As String _
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
    Private Function SelectListData(ByVal frm As LMI950F _
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
                    rtnDs = MyBase.CallWSA(blf, LMI950C.ACTION_ID_SELECT, ds)

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
    Private Function GetSelectActionId(ByVal frm As LMI950F) As String

        GetSelectActionId = String.Empty

        GetSelectActionId = LMI950C.ACTION_ID_SELECT

        Return GetSelectActionId

    End Function

#End Region 'ユーティリティ

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMI950F)

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
    Private Sub SetInitMessage(ByVal frm As LMI950F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region 'メッセージ設定

#Region "チェック"

#Region "各処理のチェック"

    ''' <summary>
    ''' 対象データチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTargetValid(ByVal frm As LMI950F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0
            Dim unchinZeroExists As Boolean = False

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI950G.sprDetailDef.SEND.ColNo)) = "済" Then
                    '既に送信済の場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"既に送信済みの行が選択されている", "、実績作成", ""})
                    Return False
                End If

                If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI950G.sprDetailDef.KOJO_KANRI_NO.ColNo))) Then
                    '工場管理番号は必須
                    MyBase.ShowMessage(frm, "E428", New String() {"工場管理番号のない行が選択されている", "、実績作成", ""})
                    Return False
                End If

                '実績送信は請求書に合わせて確定運賃を送る
                'If CDec(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI950G.sprDetailDef.HOUKOKU_UNCHIN.ColNo))) = CDec(0) Then
                If CDec(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI950G.sprDetailDef.UNCHIN.ColNo))) = CDec(0) Then
                    '運賃0の行があるか
                    unchinZeroExists = True
                End If

            Next

            If unchinZeroExists Then
                '運賃0が1行でもある場合、ワーニング表示
                'If MyBase.ShowMessage(frm, "W217", New String() {"報告運賃が0円の行が選択されています。実績作成", ""}) = MsgBoxResult.Cancel Then
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
    Friend Sub FunctionKey1Press(ByVal frm As LMI950F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.JissekiSakusei(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI950F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByVal frm As LMI950F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI950F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI950F_KeyDown(ByVal frm As LMI950F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
