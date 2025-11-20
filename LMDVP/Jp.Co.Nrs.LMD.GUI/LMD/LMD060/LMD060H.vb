' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMD     : 在庫サブシステム
'  プログラムID     :  LMD060H : 
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMD060ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD060H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD060V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD060G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconV As LMDControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconH As LMDControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG

    ''' <summary>
    ''' 検索条件保存データセット
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
        Dim frm As LMD060F = New LMD060F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMDconG = New LMDControlG(Me, sFrom)

        'Validate共通クラスの設定
        Me._LMDconV = New LMDControlV(Me, sFrom)
        'Me._LMDconV = New LMDControlV(Me, sFrom, Me._LMDconG)

        'Hnadler共通クラスの設定
        Me._LMDconH = New LMDControlH(sFrom, MyBase.GetPGID())

        'Validateクラスの設定
        'Me._V = New LMD060V(Me, frm)
        Me._V = New LMD060V(Me, frm, Me._LMDconV)

        'データセットの設定
        Me._Ds = New LMD060DS()

        'Gamenクラスの設定
        Me._G = New LMD060G(Me, frm, Me._LMDconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

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

        '初期値設定(スプレッド)
        Me._G.SetInitValue()

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
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMD060F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMD060F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMD060C.ActionType.KENSAKU)

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
        MyBase.SetLimitCount(Me._LMDconG.GetLimitData())

        '検索条件の設定
        Me._Ds = Me.SetConditionDataSet(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMDControlC.BLF)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMD060C.ACTION_ID_SELECT, Me._Ds)


        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Warningの場合
            If MyBase.IsWarningMessageExist() = True Then

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    'rtnDs = MyBase.CallWSA(blf, LMD060C.ACTION_ID_SELECT_MAIN, Me._Ds)

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '処理終了アクション
                    Call Me.EndAction(frm)
                    Exit Sub

                End If

            Else

                'メッセージエリアの設定(0件エラー)
                MyBase.ShowMessage(frm)

                'スプレッド初期化
                Call Me._G.InitSpread()

            End If

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {rtnDs.Tables(LMD060C.TABLE_NM_OUT).Rows(0)("ROW_CNT").ToString()})

            '検索結果表示（画面描画）
            Call Me._G.SetSpread(rtnDs)

        End If

        'キャッシュから名称取得
        Call SetCachedName(frm)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

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
    Private Sub OpenMasterPop(ByVal frm As LMD060F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMD060C.ActionType.MASTEROPEN)

        'カーソル位置チェック（禁止文字チェックも含まれている）
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMD060C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMD060C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMD060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMD060C.ActionType.ENTER)

        'カーソル位置チェック（禁止文字チェックも含まれている）
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMD060C.ActionType.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMDconH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        If objNm.Equals(frm.txtTantouCd.Name) Then    '担当コードの場合

            Dim userCd As String = frm.txtTantouCd.TextValue().Replace(" ", "")

            If String.IsNullOrEmpty(userCd) = False Then
                Dim drows As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select("USER_CD =" + "'" + userCd + "'")
                If drows.Length() > 0 Then   '名称取得成功時
                    frm.lblTantouNM.TextValue() = drows(0).Item("USER_NM").ToString()
                Else                         '名称取得失敗時
                    frm.lblTantouNM.TextValue() = ""
                End If
            End If

        Else                                          '担当コード以外の場合

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(frm, objNm, LMD060C.ActionType.ENTER)

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMDconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteAction(ByVal frm As LMD060F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMD060C.ActionType.DELETE)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsDeleteChk()

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMDconH.SetMessageC001(frm, Me._LMDconV.SetRepMsgData(frm.FunctionKey.F4ButtonName))

        'Errorありの場合
        If rtnResult = False Then

            'MyBase.ShowMessage(frm)
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '削除条件の設定
        Me._Ds = Me.SetDeleteDataSet(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMDControlC.BLF)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMD060C.ACTION_ID_HAITA, Me._Ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Errorありの場合
            If MyBase.IsErrorMessageExist() = True Then

                MyBase.ShowMessage(frm)
                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

        Else

            'Errorなしの場合、削除処理を行う
            rtnDs = MyBase.CallWSA(blf, LMD060C.ACTION_ID_DELETE, Me._Ds)

            '処理終了メッセージ表示
            MyBase.ShowMessage(frm)

            'メッセージ判定
            'If MyBase.IsMessageExist() = True Then

            '    'Errorありの場合
            '    If MyBase.IsErrorMessageExist() = True Then
            '        MyBase.ShowMessage(frm, "S001", New String() {"削除"})
            '        '処理終了アクション
            '        Call Me.EndAction(frm)
            '        Exit Sub

            '    End If

            'End If

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        'スプレッド初期化
        Call Me._G.InitSpread()

        If 0 < rtnDs.Tables(LMD060C.TABLE_NM_OUT).Rows.Count() Then
            '画面再描画（再検索結果データが0件以上）
            Call Me._G.SetSpread(rtnDs)
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JikkouAction(ByVal frm As LMD060F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMD060C.ActionType.JIKKOU)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsJikkouChk(Convert.ToString(MyBase.GetSystemDateTime(0)))

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMDconH.SetMessageC001(frm, Me._LMDconV.SetRepMsgData(frm.FunctionKey.F11ButtonName))

        'Errorありの場合
        If rtnResult = False Then

            'MyBase.ShowMessage(frm)
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '実行条件の設定
        Me._Ds = Me.SetDeleteDataSet(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JikkouData")

        ''==========================
        ''WSAクラス呼出
        ''==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMDControlC.BLF)
        'Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMD060C.ACTION_ID_HAITA, Me._Ds)
        Dim rtnDs As DataSet

        ''排他チェック後、メッセージ判定
        'If MyBase.IsMessageExist() = True Then

        '    'Errorありの場合
        '    If MyBase.IsErrorMessageExist() = True Then
        '        MyBase.ShowMessage(frm)
        '        '処理終了アクション
        '        Call Me.EndAction(frm)
        '        Exit Sub
        '    End If

        'Else

        'Errorなしの場合、実行処理を行う
        rtnDs = MyBase.CallWSA(BLF, LMD060C.ACTION_ID_JIKKOU, Me._Ds)

        '処理終了メッセージ表示
        MyBase.ShowMessage(frm)

        'メッセージ判定
        'If MyBase.IsMessageExist() = True Then

        '    'Errorありの場合
        '    If MyBase.IsErrorMessageExist() = True Then
        '        MyBase.ShowMessage(frm)
        '        '処理終了アクション
        '        Call Me.EndAction(frm)
        '        Exit Sub
        '    End If

        'End If

        'End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "JikkouData")

        'スプレッド初期化
        Call Me._G.InitSpread()

        If 0 < rtnDs.Tables(LMD060C.TABLE_NM_OUT).Rows.Count() Then
            '画面再描画（再検索結果データが0件以上）
            Call Me._G.SetSpread(rtnDs)
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

#End Region 'イベント定義(一覧)

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMD060F)

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
    Private Sub EndAction(ByVal frm As LMD060F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMD060F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetInitMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' 初期メッセージ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetInitMessage(ByVal frm As LMD060F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMD060F, ByVal objNm As String, ByVal actionType As LMD060C.ActionType) As Boolean

        With frm

            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name
                    'コードが両方空だった場合は名称をクリアする
                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                    And String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        .lblCustNm.TextValue = String.Empty
                    End If

                    Call Me.SetReturnCustPop(frm, actionType)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actinType">アクションタイプ</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMD060F, ByVal actinType As LMD060C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actinType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNm.TextValue = String.Concat(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString())

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actinType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMD060F, ByVal actinType As LMD060C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
            Dim custCdL As String = String.Empty
            Dim custCdM As String = String.Empty
            Dim csFlg As String = LMConst.FLG.ON

            Select Case actinType

                Case LMD060C.ActionType.MASTEROPEN, LMD060C.ActionType.ENTER

                    custCdL = frm.txtCustCdL.TextValue
                    custCdM = frm.txtCustCdM.TextValue

            End Select

            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If actinType = LMD060C.ActionType.ENTER Then
                .Item("CUST_CD_L") = custCdL
                .Item("CUST_CD_M") = custCdM
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S

        End With

        '行追加
        dt.Rows.Add(dr)

        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return PopFormShow(prm, "LMZ260")

    End Function

    ''' <summary>
    ''' Pop起動処理
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <param name="id">画面ID</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function PopFormShow(ByVal prm As LMFormData, ByVal id As String) As LMFormData

        LMFormNavigate.NextFormNavigate(Me, id, prm)
        Return prm

    End Function

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMD060F)

        With frm

            '荷主名称
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = False Then
                If String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                    .txtCustCdM.TextValue = "00"
                End If
                .lblCustNm.TextValue = GetCachedCust(.txtCustCdL.TextValue, .txtCustCdM.TextValue, "00", "00")
            End If

            '担当者名称
            If String.IsNullOrEmpty(.txtTantouCd.TextValue) = False Then
                .lblTantouNM.TextValue = GetCachedUser(.txtTantouCd.TextValue)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String) As String

        Dim dr As DataRow() = Nothing

        '荷主名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                           "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("CUST_NM_L").ToString
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' ユーザーキャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedUser(ByVal userCd As String) As String

        Dim dr As DataRow() = Nothing

        'ユーザー名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat( _
                                                                            "USER_CD = '", userCd, "' AND " _
                                                                          , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("USER_NM").ToString
        End If

        Return String.Empty

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' 削除・実行条件データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks>削除。実行時利用するINデータセットを作成</remarks>
    Private Function SetDeleteDataSet(ByVal frm As LMD060F) As DataSet

        'INデータセット初期化
        Me._Ds.Tables(LMD060C.TABLE_NM_IN_DEL).Clear()   'LMD060IN_DEL

        'スプレッド項目
        With frm.sprCreate.ActiveSheet

            For i As Integer = 1 To .RowCount() - 1

                If Convert.ToString(.Cells(i, LMD060G.sprCreateDef.DEF.ColNo).Value) = True.ToString Then
                    'スプレッドのチェックリストをデータセットに格納

                    Dim dr As DataRow = Me._Ds.Tables(LMD060C.TABLE_NM_IN_DEL).NewRow()

                    'ヘッダ項目
                    With frm
                        dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue()
                        dr.Item("RIREKI_MAKE_DATE") = .imdZaiRirekiDate.TextValue()
                    End With

                    'スプレッド項目
                    dr.Item("ZAI_REC_NO") = Me._LMDconG.GetCellValue(.Cells(i, LMD060G.sprCreateDef.ZAI_REC_NO_1.ColNo))
                    dr.Item("RIREKI_DATE") = Me._LMDconG.GetCellValue(.Cells(i, LMD060G.sprCreateDef.RIREKI_1.ColNo)).Replace("/", "")
                    dr.Item("CUST_CD_L") = Me._LMDconG.GetCellValue(.Cells(i, LMD060G.sprCreateDef.CUST_CD_L.ColNo))
                    dr.Item("CUST_CD_M") = Me._LMDconG.GetCellValue(.Cells(i, LMD060G.sprCreateDef.CUST_CD_M.ColNo))
                    dr.Item("SYS_UPD_DATE") = Me._LMDconG.GetCellValue(.Cells(i, LMD060G.sprCreateDef.SYS_UPD_DATE.ColNo))
                    dr.Item("SYS_UPD_TIME") = Me._LMDconG.GetCellValue(.Cells(i, LMD060G.sprCreateDef.SYS_UPD_TIME.ColNo))

                    '行追加
                    Me._Ds.Tables(LMD060C.TABLE_NM_IN_DEL).Rows.Add(dr)

                End If

            Next

        End With

        Return Me._Ds

    End Function

    ''' <summary>
    ''' データセット設定(検索条件データ)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks>検索処理時利用するINデータセット</remarks>
    Private Function SetConditionDataSet(ByVal frm As LMD060F) As DataSet

        Me._Ds.Tables(LMD060C.TABLE_NM_IN).Clear()   'LMD060IN
        Dim dr As DataRow = Me._Ds.Tables(LMD060C.TABLE_NM_IN).NewRow()

        'ヘッダ項目
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("TANTO_CD") = .txtTantouCd.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("CLOSE_KB") = .cmbSimebi.SelectedValue

        End With

        'スプレッド項目
        With frm.sprCreate.ActiveSheet

            dr.Item("CUST_NM") = Me._LMDconG.GetCellValue(.Cells(0, LMD060G.sprCreateDef.CUST_NM.ColNo))
            dr.Item("TANTO_NM") = Me._LMDconG.GetCellValue(.Cells(0, LMD060G.sprCreateDef.TANTO_NM.ColNo))

        End With

        '行追加
        Me._Ds.Tables(LMD060C.TABLE_NM_IN).Rows.Add(dr)

        Return Me._Ds

    End Function

#End Region 'DataSet設定

#Region "イベント振分け"

    ''' <summary>
    ''' F4押下時処理呼び出し（削除）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMD060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey4Press")

        Call Me.DeleteAction(frm) '削除処理

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey4Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMD060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Call Me.SelectListData(frm) '検索処理

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し（マスタ参照）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMD060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(実行処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMD060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey11Press")

        Call Me.JikkouAction(frm) '実行処理

        Logger.EndLog(Me.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMD060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMD060F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMD060F_KeyDown(ByVal frm As LMD060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        If e.KeyCode.Equals(Keys.Enter) = False Then

        End If

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMD060F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMD060F_KeyDown")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class