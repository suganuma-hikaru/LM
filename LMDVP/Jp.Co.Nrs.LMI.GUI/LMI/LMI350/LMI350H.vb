' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI350H : 保管荷役明細(MT触媒)
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMI350ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI350H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI350F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI350V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI350G

    ''' <summary>
    ''' パラメータ格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconH As LMIControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconG As LMIControlG

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region

#Region "Method"

#Region "初期処理"

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
        Me._Frm = New LMI350F(Me)

        'Validateクラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(Me._Frm, Form), Me._LMIconG)

        'Gクラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(Me._Frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI350G(Me, Me._Frm)

        'Validateクラスの設定
        Me._V = New LMI350V(Me, Me._Frm, Me._LMIconV, Me._LMIconG)

        'フォームの初期化
        Call MyBase.InitControl(Me._Frm)

        'キーイベントをフォームで受け取る
        Me._Frm.KeyPreview = True

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージの表示
        MyBase.ShowMessage(Me._Frm, "G007")

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrintShutu()

        '処理開始アクション
        Call Me._LMIconH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI350C.EventShubetsu.PRINT) = False Then
            Call Me._LMIconH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputCheck() = False Then
            Call Me._LMIconH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'データセット
        Dim rtnDs As DataSet = Me.SetInData()
        rtnDs.Merge(New RdPrevInfoDS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        rtnDs = MyBase.CallWSA(blf, "DoPrint", rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DoPrint")

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._LMIconH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

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

            '終了メッセージ表示
            MyBase.ShowMessage(Me._Frm, "G002", New String() {"印刷", ""})

        Else
            '終了メッセージ表示
            MyBase.ShowMessage(Me._Frm, "E070")

        End If

        '終了処理　
        Call Me._LMIconH.EndAction(Me._Frm)


    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop()

        '背景色クリア
        Me._LMIconG.SetBackColor(Me._Frm)

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI350C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMI350C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        '処理開始アクション：１件時表示あり
        Me._PopupSkipFlg = True
        Me._LMIconH.StartAction(Me._Frm)

        'Pop起動処理
        Call Me.ShowPopupControl(objNm, LMI350C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._LMIconH.EndAction(Me._Frm)

        ''メッセージの表示
        'MyBase.ShowMessage(Me._Frm, "G007")

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal e As System.Windows.Forms.KeyEventArgs)

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI350C.EventShubetsu.ENTER) = False Then
            Call Me._LMIconH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMI350C.EventShubetsu.ENTER) = False Then
            Call Me._LMIconH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
            Exit Sub
        End If

        '処理開始アクション：１件時表示あり
        Me._PopupSkipFlg = False
        Me._LMIconH.StartAction(Me._Frm)

        'Pop起動処理
        Call Me.ShowPopupControl(objNm, LMI350C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMIconH.EndAction(Me._Frm)

    End Sub

#End Region

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String, ByVal actionType As LMI350C.EventShubetsu) As Boolean

        With Me._Frm

            Select Case objNm
                Case .txtCustCdL.Name
                    '荷主マスタ参照POP起動
                    Call Me.CustPop(Me._Frm, actionType)

                Case .txtSeiqCd.Name
                    '請求先マスタ参照POP起動
                    Call Me.SeiqPop(Me._Frm, actionType)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CustPop(ByVal frm As LMI350F, ByVal actionType As LMI350C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)

        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMI350F, ByVal actionType As LMI350C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue

            If actionType = LMI350C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMIconH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 請求先マスタ照会(LMZ220)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function SeiqPop(ByVal frm As LMI350F, ByVal actionType As LMI350C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowSeiqPopup(frm, actionType)

        If prm.ReturnFlg = True Then

            '請求先マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtSeiqCd.TextValue = dr.Item("SEIQTO_CD").ToString()
                .lblSeiqNm.TextValue = dr.Item("SEIQTO_NM").ToString()
            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowSeiqPopup(ByVal frm As LMI350F, ByVal actionType As LMI350C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue

            If actionType = LMI350C.EventShubetsu.ENTER Then
                .Item("SEIQTO_CD") = frm.txtSeiqCd.TextValue
            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMIconH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(作成・印刷処理)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Function SetInData() As DataSet

        With Me._Frm

            Dim ds As DataSet = New LMI680DS
            Dim dt As DataTable = ds.Tables(LMI350C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow
            Dim SeiqDate As String = String.Concat(Left(.imdSeiqDate.TextValue, 6), "01")

            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("CUST_CD_S") = .txtCustCdS.TextValue
            dr.Item("SEIQTO_CD") = .txtSeiqCd.TextValue
            dr.Item("F_DATE") = SeiqDate
            dr.Item("T_DATE") = Convert.ToDateTime(DateFormatUtility.EditSlash(SeiqDate)).AddMonths(1).AddDays(-1).ToString("yyyyMMdd")
            dt.Rows.Add(dr)

            Return ds

        End With

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し(印刷処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMI350F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '印刷処理の呼び出し
        Call Me.PrintShutu()

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI350F, ByVal e As System.Windows.Forms.KeyEventArgs)


        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'マスタ参照
        Me.OpenMasterPop()

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI350F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI350F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMI350F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'Enterキーイベント
        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class