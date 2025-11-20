' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMK     : 支払サブシステム
'  プログラムID     :  LMK060H : 支払印刷
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMK060ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMK060H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMK060V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMK060G

    ''' <summary>
    ''' パラメータ格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMKconV As LMKControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMKconH As LMKControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMKconG As LMKControlG

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region

#Region "Method"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMK060F = New LMK060F(Me)

        'Validateクラスの設定
        Me._LMKconV = New LMKControlV(Me, DirectCast(frm, Form))

        'Gクラスの設定
        Me._LMKconG = New LMKControlG(DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMKconH = New LMKControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me._LMKconV, Me._LMKconG)

        'Validateクラスの設定
        Me._V = New LMK060V(Me, frm, Me._LMKconV, Me._LMKconG)

        'Gamenクラスの設定
        Me._G = New LMK060G(Me, frm, Me._LMKconG)

        'フォームの初期化
        MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        '支払検索から遷移時のみ初期値設定
        Call Me._G.SetControlPrm(Me._PrmDs, MyBase.RootPGID())

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#Region "イベント定義(一覧)"

#Region "印刷区分値変更"
    Private Sub Print(ByVal frm As LMK060F)

        '処理開始アクション
        Call Me._LMKconH.StartAction(frm)
        '画面全ロック
        MyBase.LockedControls(frm)

        '終了メッセージ表示
        MyBase.ShowMessage(frm, "G007")

        '終了処理
        Call Me.EndAction(frm)

        'ロック制御
        Call Me._G.Locktairff(frm)

    End Sub

#End Region

#Region "マスタ参照"

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMK060F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック()
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMK060C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMK060C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then

            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMK060C.EventShubetsu.MASTEROPEN)

        '終了メッセージ設定
        MyBase.ShowMessage(frm, "G007")

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, True)



    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMK060F, ByVal e As System.Windows.Forms.KeyEventArgs)


        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMK060C.EventShubetsu.MASTEROPEN)

        ''カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMK060C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, eventFlg)

            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMK060C.EventShubetsu.ENTER)

        '終了メッセージ設定
        MyBase.ShowMessage(frm, "G007")

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, eventFlg)

    End Sub

#Region "Pop"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMK060F, ByVal objNm As String, ByVal actionType As LMK060C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMKconH.StartAction(frm)

            '画面全ロック
            MyBase.LockedControls(frm)

            Select Case objNm

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    '運送会社コード
                    Call Me.CustPop(frm, actionType)

                Case .txtShiharaiCd.Name

                    '支払先コード
                    Call Me.SetReturnShiharaitoPop(frm, actionType)
            End Select

        End With

        Return True

    End Function


#End Region

#End Region

#Region "マスタPOP"

    ''' <summary>
    ''' 運送会社マスタ照会(LMZ250)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CustPop(ByVal frm As LMK060F, ByVal actionType As LMK060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)


        If prm.ReturnFlg = True Then

            '運送会社マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtUnsocoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtUnsocoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblUnsocoNm.TextValue = dr.Item("UNSOCO_NM").ToString()
                .lblUnsocoBrNm.TextValue = dr.Item("UNSOCO_BR_NM").ToString()

            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMK060F, ByVal actionType As LMK060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString()
            If actionType = LMK060C.EventShubetsu.ENTER Then
                .Item("UNSOCO_CD") = frm.txtUnsocoCd.TextValue
                .Item("UNSOCO_BR_CD") = frm.txtUnsocoBrCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        '行追加
        dt.Rows.Add(dr)

        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ250")

    End Function

    ''' <summary>
    ''' 支払マスタ照会(LMZ310)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnShiharaitoPop(ByVal frm As LMK060F, ByVal actionType As LMK060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowShiharaitoPop(frm, actionType)

        If prm.ReturnFlg = True Then
            '支払先マスタ
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ310C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtShiharaiCd.TextValue = dr.Item("SHIHARAITO_CD").ToString()
                .lblShiharaiNm.TextValue = String.Concat(dr.Item("SHIHARAITO_NM").ToString(), "　", dr.Item("SHIHARAITO_BUSYO_NM").ToString())
            End With
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowShiharaitoPop(ByVal frm As LMK060F, ByVal actionType As LMK060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ310DS()
        Dim dt As DataTable = ds.Tables(LMZ310C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString()
            If actionType = LMK060C.EventShubetsu.ENTER Then
                .Item("SHIHARAITO_CD") = frm.txtShiharaiCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        '行追加
        dt.Rows.Add(dr)

        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ310")

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

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function PrintShutu(ByVal frm As LMK060F) As Boolean

        '処理開始アクション
        Call Me._LMKconH.StartAction(frm)

        '画面全ロック
        MyBase.LockedControls(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMK060C.EventShubetsu.PRINT) = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck() = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Return False

        End If

        'データセット
        Dim ds As DataSet = Nothing
        Select Case frm.cmbPrint.SelectedValue.ToString
            Case "01"
                '支払運賃明細
                ds = New LMF600DS()
                Call Me.SetDataSetInData(frm, ds)
            Case "02"
                '支払運賃チェックリスト
                ds = New LMF610DS()
                Call Me.SetDataSetInData(frm, ds)
        End Select

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        'プリント種別の判定
        Dim Print As String = frm.cmbPrint.SelectedValue.ToString
        Dim rtnDs As DataSet = Nothing
        Select Case Print

            Case LMK060C.PRINT_SHIHARAI_MEISAI
                '支払運賃明細
                rtnDs = MyBase.CallWSA(blf, LMK060C.ACTION_ID_PRINT_MEISAI, ds)

            Case LMK060C.PRINT_SHIHARAI_CHECK
                '支払運賃チェックリスト
                rtnDs = MyBase.CallWSA(blf, LMK060C.ACTION_ID_PRINT_CHECK, ds)

        End Select

        'メッセージ判定
        If IsMessageExist() = True Then

            'エラーメッセージ判定
            If MyBase.IsErrorMessageExist() = False Then

                '処理終了アクション
                Call Me.EndAction(frm)
                '印刷処理でエラーメッセージあったらメッセージを表示してG007を設定
                MyBase.ShowMessage(frm)
                MyBase.ShowMessage(frm, "G007")
                Return False

            End If
        End If

        '終了メッセージ表示
        MyBase.SetMessage("G002", New String() {"印刷", ""})

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

        '処理終了アクション
        Call Me.EndAction(frm)

        MyBase.ShowMessage(frm)

        Return True

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し(印刷処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMK060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '印刷処理の呼び出し
        Call Me.PrintShutu(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMK060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'マスタ参照
        Me.OpenMasterPop(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMK060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMK060F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓========================

    ''' <summary>
    ''' 印刷コンボボックス変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub Print(ByVal frm As LMK060F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '印刷コンボボックス変更
        Me.Print(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMK060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'Enterキーイベント
        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#Region "データセット"

    ''' <summary>
    ''' データセット設定(共通)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMK060F, ByVal ds As DataSet) As DataSet

        With frm
            Dim dt As DataTable = Nothing
            Select Case frm.cmbPrint.SelectedValue.ToString
                Case "01"
                    '支払運賃明細
                    dt = ds.Tables(LMK060C.TABLE_NM_LMF600IN)
                Case "02"
                    '支払運賃チェックリスト
                    dt = ds.Tables(LMK060C.TABLE_NM_LMF610IN)
            End Select

            Dim dr As DataRow = dt.NewRow()
            Dim Print As String = .cmbPrint.SelectedValue.ToString

            'データセットに格納
            dr("NRS_BR_CD") = .cmbBr.SelectedValue
            dr("UNSO_CD") = .txtUnsocoCd.TextValue
            dr("UNSO_BR_CD") = .txtUnsocoBrCd.TextValue
            '支払運賃チェックリストの場合は支払先コードをデータセットしない
            If LMK060C.PRINT_SHIHARAI_CHECK.Equals(Print) = False Then
                dr("SHIHARAI_CD") = .txtShiharaiCd.TextValue
            End If
            dr("F_DATE") = .imdOutkaDateFrom.TextValue
            dr("T_DATE") = .imdOutkaDateTo.TextValue

            'データセットの追加
            dt.Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMK060F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterボタンの場合、True</param>
    ''' <remarks></remarks>
    Friend Sub NextFocusedControl(ByVal frm As Form, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

#End Region

#End Region

End Class