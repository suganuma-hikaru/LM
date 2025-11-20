' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC050H : 出荷帳票印刷
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMC050ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC050H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMC050V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMC050G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

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
        Dim frm As LMC050F = New LMC050F(Me)


        Dim sForm As Form = DirectCast(frm, Form)
        'Validate共通クラスの設定
        Me._LMCconV = New LMCControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMCconH = New LMCControlH(DirectCast(frm, Form))

        'Gamen共通クラスの設定
        Me._LMCconG = New LMCControlG(Me, DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMC050V(Me, frm, Me._LMCconV, Me._LMCconG)

        'Gamenクラスの設定
        Me._G = New LMC050G(Me, frm)

        'フォームの初期化
        Call Me.InitControl(frm)

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
        Call Me._G.SetControl(Me.GetPGID(), frm)

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

#Region "マスタ参照"
    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMC050F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック()
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMC050C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMC050C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMC050C.EventShubetsu.MASTEROPEN)

        '終了メッセージ設定
        MyBase.SetMessage("G007")

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
    Private Sub EnterAction(ByVal frm As LMC050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMC050C.EventShubetsu.MASTEROPEN)

        ''カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMC050C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMC050C.EventShubetsu.ENTER)

        '終了メッセージ設定
        MyBase.SetMessage("G007")

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
    Private Function ShowPopupControl(ByVal frm As LMC050F, ByVal objNm As String, ByVal actionType As LMC050C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me.StartAction(frm)


            Select Case objNm

                Case .txtCustCD_L.Name

                    '荷主(大)Lコード
                    Call Me.CustPop(frm, actionType)

                Case .txtCustCD_M.Name
                    '荷主(中)Mコード
                    Call Me.CustPop(frm, actionType)

                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                Case .txtCustCD_S.Name
                    '荷主(小)Sコード
                    Call Me.CustPop(frm, actionType)
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

                Case .txtGoodsCd.Name
                    '商品指定コード
                    Call Me.ShouhinPop(frm, actionType)

            End Select

        End With

        Return True
    End Function


#End Region

#End Region

#Region "マスタPOP"
    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CustPop(ByVal frm As LMC050F, ByVal actionType As LMC050C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)

        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtCustCD_L.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCD_M.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNM_L.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNM_M.TextValue = dr.Item("CUST_NM_M").ToString()
                '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                If .cmbPrint.SelectedValue.ToString.Trim().Equals(LMC050C.PRINT02) = True Then
                    .txtCustCD_S.TextValue = dr.Item("CUST_CD_S").ToString()
                    .lblCustNM_S.TextValue = dr.Item("CUST_NM_S").ToString()
                End If
                '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --
            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMC050F, ByVal actionType As LMC050C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMC050C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
                .Item("CUST_CD_M") = frm.txtCustCD_M.TextValue
                '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                .Item("CUST_CD_S") = frm.txtCustCD_S.TextValue
                '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            If frm.cmbPrint.SelectedValue.ToString.Trim().Equals(LMC050C.PRINT02) = True Then
                '日別出荷報告書の場合、小も可能な区分に変更
                .Item("HYOJI_KBN") = LMZControlC.HYOJI_SS
            End If
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ260")

    End Function

    ''' <summary>
    ''' 商品マスタ照会(LMZ020)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function ShouhinPop(ByVal frm As LMC050F, ByVal actionType As LMC050C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowShouhinPopup(frm, actionType)

        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtGoodsCd.TextValue = dr.Item("GOODS_CD_CUST").ToString()
                .lblGoodsNm.TextValue = dr.Item("GOODS_NM_1").ToString()
            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowShouhinPopup(ByVal frm As LMC050F, ByVal actionType As LMC050C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
            .Item("CUST_CD_M") = frm.txtCustCD_M.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMC050C.EventShubetsu.ENTER Then
                .Item("GOODS_CD_CUST") = frm.txtGoodsCd.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ020")

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

#Region "印刷区分値変更"
    Private Sub Print(ByVal frm As LMC050F)

        'ロック制御
        Call Me._G.Locktairff(frm)

        '終了メッセージ表示
        MyBase.SetMessage("G007")

        '終了処理
        Call Me.EndAction(frm)

    End Sub
#End Region

#End Region

#Region "印刷処理"
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function PrintShutu(ByVal frm As LMC050F) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMC050C.EventShubetsu.PRINT) = False Then

            '画面解除
            MyBase.UnLockedControls(frm)

            'Cursorを元に戻す
            Cursor.Current = Cursors.Default()

            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck() = False Then

            '画面解除
            MyBase.UnLockedControls(frm)

            'Cursorを元に戻す
            Cursor.Current = Cursors.Default()

            Return False

        End If

        'データセット
        Dim rtDs As DataSet = Me.SetDataSetInData(frm, New LMC050DS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs.Merge(New RdPrevInfoDS)

        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        rtDs = MyBase.CallWSA(blf, LMC050C.ACTION_ID_PRINT, rtDs)


        If MyBase.IsMessageStoreExist = True Then
            Call Me.OutputExcel(frm)

        ElseIf IsMessageExist() = True Then

            'エラーメッセージ判定
            If MyBase.IsErrorMessageExist() = True Then

                'エラーメッセージの場合
                MyBase.ShowMessage(frm)

                '画面解除
                MyBase.UnLockedControls(frm)

                'Cursorを元に戻す
                Cursor.Current = Cursors.Default()

                Return False
            Else
                '帳票のエラーメッセージの場合
                MyBase.ShowMessage(frm)
                MyBase.ShowMessage(frm, "G007")
                '処理終了アクション
                '画面解除
                MyBase.UnLockedControls(frm)

                'Cursorを元に戻す
                Cursor.Current = Cursors.Default()



                Return False
            End If

        End If

        '2013.08.22 要望番号2091追加START
        Select Case frm.cmbPrint.SelectedValue.ToString.Trim()

            Case LMC050C.PRINT06

                '終了メッセージ表示
                MyBase.SetMessage("G002", New String() {"報告作成", ""})
                '処理終了アクション
                Call Me.EndAction(frm)
                Return True
            Case Else

        End Select
        '2013.08.22 要望番号2091追加END

        'プレビュー判定 
        Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
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

        '終了メッセージ表示
        MyBase.SetMessage("G002", New String() {"印刷", ""})


        '処理終了アクション
        Call Me.EndAction(frm)

        Return True


    End Function

#End Region

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMC050F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMC050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '印刷処理の呼び出し
        Call Me.PrintShutu(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMC050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.OpenMasterPop(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMC050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMC050F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMC050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 印刷コンボボックス変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub Print(ByVal frm As LMC050F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.Print(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

#Region "フォーカス移動"
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

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMC050F)

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
    Private Sub EndAction(ByVal frm As LMC050F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        MyBase.ShowMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "データセット"
    ''' <summary>
    ''' データセット設定(チェックリスト)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMC050F, ByVal ds As DataSet) As DataSet

        With frm

            Dim Syubetu As String = String.Empty
            Dim dt As DataTable = ds.Tables(LMC050C.TABLE_NM_IN)

            Dim dr As DataRow = dt.NewRow()

            'チェックボックスにチェックが入っている場合
            If .chkFurikae.Checked = True Then
                Syubetu = "50"
            End If

            '検索条件の格納
            dr("PRINT_FLAG") = .cmbPrint.SelectedValue
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr("CUST_CD_L") = .txtCustCD_L.TextValue
            dr("CUST_CD_M") = .txtCustCD_M.TextValue
            dr("OUTKA_PLAN_DATE") = .imdSyukkaDate.TextValue
            dr("SYUBETU_KB") = Syubetu
            dr("SYS_ENT_DATE") = .imdDataInsDate.TextValue
            'dr("PRINT_DATE_FROM") = .imdPrintDate_S.TextValue
            'dr("PRINT_DATE_TO") = .imdPrintDate_E.TextValue

            Dim fDate As String = String.Empty
            Dim tDate As String = String.Empty

            '印刷種別による分岐
            Dim prtSyubetu As String = .cmbPrint.SelectedValue.ToString.Trim()

            Select Case prtSyubetu

                Case LMC050C.PRINT01, LMC050C.PRINT03
                    fDate = String.Concat(.imdSyukkaDate.TextValue.Substring(0, 6), "01")
                    '(2012.02.28) 月次の範囲は、入力月の1日から翌月1日ではなく入力月の月末とする。
                    'tDate = String.Concat(Format(.imdSyukkaDate.Value.AddMonths(1), "yyyyMM"), "01")

                    'fdate当月の末日を求める
                    '当月の末日を求める
                    tDate = Convert.ToString(DateSerial(Convert.ToInt32(Mid(fDate, 1, 4)), Convert.ToInt32(Mid(fDate, 5, 2)), Convert.ToInt32(Mid(fDate, 7, 2))).AddMonths(1))
                    tDate = String.Concat(tDate.Substring(0, 7), "/01")
                    tDate = Convert.ToString(DateSerial(Convert.ToInt32(Mid(tDate, 1, 4)), Convert.ToInt32(Mid(tDate, 6, 2)), Convert.ToInt32(Mid(tDate, 9, 2))).AddDays(-1))
                    tDate = String.Concat( _
                                            Mid(tDate, 1, 4), _
                                            Mid(tDate, 6, 2), _
                                            Mid(tDate, 9, 2) _
                                           )

                Case LMC050C.PRINT02

                    fDate = .imdPrintDate_S.TextValue
                    tDate = .imdPrintDate_E.TextValue
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                    dr("CUST_CD_S") = .txtCustCD_S.TextValue
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --
            End Select

            dr.Item("PRINT_DATE_FROM") = fDate
            dr.Item("PRINT_DATE_TO") = tDate

            dr("GOODS_CD_CUST") = .txtGoodsCd.TextValue

            ds.Tables(LMC050C.TABLE_NM_IN).Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "EXCEL出力処理"

    Private Sub OutputExcel(ByVal frm As LMC050F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub

#End Region

#End Region 'Method

End Class