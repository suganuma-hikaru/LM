' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI160H : 運賃請求印刷指示(ディック)
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI160ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI160H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI160V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI160G

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
        Dim frm As LMI160F = New LMI160F(Me)

        'Validateクラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        'Gクラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI160G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI160V(Me, frm, Me._LMIconV, Me._LMIconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        '初期値設定(荷主コード)
        Call Me._G.SetControlPrm()

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

#Region "マスタ参照"
    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMI160F)


        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック()
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMI160C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI160C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then

            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMI160C.EventShubetsu.MASTEROPEN)

        'ロック対応  terakawa 2012.07.04 Start
        '終了メッセージ設定
        'MyBase.ShowMessage(frm, "G007")
        'ロック対応  terakawa 2012.07.04 End

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
    Private Sub EnterAction(ByVal frm As LMI160F, ByVal e As System.Windows.Forms.KeyEventArgs)


        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg


        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMI160C.EventShubetsu.MASTEROPEN)

        ''カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI160C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, eventFlg)

            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMI160C.EventShubetsu.ENTER)

        'ロック対応  terakawa 2012.07.04 Start
        '終了メッセージ設定
        'MyBase.ShowMessage(frm, "G007")
        'ロック対応  terakawa 2012.07.04 Start

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
    Private Function ShowPopupControl(ByVal frm As LMI160F, ByVal objNm As String, ByVal actionType As LMI160C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMIconH.StartAction(frm)
            'ロック対応  terakawa 2012.07.04 Start
            '画面全ロック
            'MyBase.LockedControls(frm)
            'ロック対応  terakawa 2012.07.04 End

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    '荷主(大)Lコード
                    Call Me.CustPop(frm, actionType)

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
    Private Function CustPop(ByVal frm As LMI160F, ByVal actionType As LMI160C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)


        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()

            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMI160F, ByVal actionType As LMI160C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMI160C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ260")

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
    Private Function PrintShutu(ByVal frm As LMI160F) As Boolean

        '処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        'ロック対応  terakawa 2012.07.04 Start
        '画面全ロック
        'MyBase.LockedControls(frm)
        'ロック対応  terakawa 2012.07.04 End

        '権限チェック
        If Me._V.IsAuthorityChk(LMI160C.EventShubetsu.PRINT) = False Then

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
        Dim rtDs As DataSet = Me.SetDataSetInDataHantei(frm)

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

            Case LMI160C.BUKA_UNCHIN_SEIKYU
                ' 運賃請求明細書（DIC在庫部課別）
                rtnDs = MyBase.CallWSA(blf, LMI160C.ACTION_ID_BUKA_UNCHIN_SEIKYU, rtDs)

            Case LMI160C.HUTANKA_UNCHIN_SEIKYU

                ' 運賃請求明細書（DIC在庫部課、負担課別）
                rtnDs = MyBase.CallWSA(blf, LMI160C.ACTION_ID_HUTANKA_UNCHIN_SEIKYU, rtDs)

            Case LMI160C.NOUNYU_UNCHIN_SEIKYU _
               , LMI160C.SYAATUKAI_UNCHIN_SEIKYU _
               , LMI160C.YAMATO_UNCHIN_SEIKYU
                ' 運賃請求明細書（DIC在庫部課、負担課、納入日、納入先別）1便・2便、車扱い・特便、ヤマト便共通)
                rtnDs = MyBase.CallWSA(blf, LMI160C.ACTION_ID_NOUNYU_UNCHIN_SEIKYU, rtDs)

        End Select

        'メッセージ判定
        If IsMessageExist() = True Then

            'エラーメッセージ判定
            If MyBase.IsErrorMessageExist() = False Then

                'ロック対応  terakawa 2012.07.04 Start
                ''処理終了アクション
                'Call Me.EndAction(frm)
                '印刷処理でエラーメッセージあったらメッセージを表示してG007を設定
                MyBase.ShowMessage(frm)
                'MyBase.ShowMessage(frm, "G007")
                '処理終了アクション
                Call Me.EndAction(frm)
                'ロック対応  terakawa 2012.07.04 End

                Return False

            End If
        End If

        'ロック対応  terakawa 2012.07.04 Start
        ''終了メッセージ表示
        'MyBase.SetMessage("G002", New String() {"印刷", ""})
        'ロック対応  terakawa 2012.07.04 End

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

        'ロック対応  terakawa 2012.07.04 Start
        '終了メッセージ表示
        MyBase.SetMessage("G002", New String() {"印刷", ""})
        'ロック対応  terakawa 2012.07.04 End

        MyBase.ShowMessage(frm)

        Return True


    End Function

#End Region



#End Region

#Region "イベント振分け"


    ''' <summary>
    ''' F7押下時処理呼び出し(XX処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMI160F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByRef frm As LMI160F, ByVal e As System.Windows.Forms.KeyEventArgs)


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
    Friend Sub FunctionKey12Press(ByRef frm As LMI160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI160F, ByVal e As FormClosingEventArgs)

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
    Friend Sub KeyDown(ByVal frm As LMI160F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'Enterキーイベント
        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#Region "データセット"
    ''' <summary>
    ''' データセット設定(印刷)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInDataHantei(ByVal frm As LMI160F) As DataSet

        With frm

            Dim Print As String = .cmbPrint.SelectedValue.ToString
            Dim ds As DataSet = Nothing

            '印刷種別によってデータセット変更
            Select Case Print
                Case LMI160C.BUKA_UNCHIN_SEIKYU

                    ' 運賃請求明細書（DIC在庫部課別）
                    ds = New LMI590DS
                    Dim dt As DataTable = ds.Tables(LMI160C.TABLE_NM_IN_BUKA)
                    'データセット590IN
                    ds = Me.SetDataSetInData(frm, ds, dt, LMI160C.TABLE_NM_IN_BUKA)

                Case LMI160C.HUTANKA_UNCHIN_SEIKYU

                    ' 運賃請求明細書（DIC在庫部課、負担課別）
                    ds = New LMI591DS
                    Dim dt As DataTable = ds.Tables(LMI160C.TABLE_NM_IN_HUTANKA)
                    'データセット591IN
                    ds = Me.SetDataSetInData(frm, ds, dt, LMI160C.TABLE_NM_IN_HUTANKA)

                Case LMI160C.NOUNYU_UNCHIN_SEIKYU _
                   , LMI160C.SYAATUKAI_UNCHIN_SEIKYU _
                   , LMI160C.YAMATO_UNCHIN_SEIKYU

                    ' 運賃請求明細書（DIC在庫部課、負担課、納入日、納入先別）1便・2便、車扱い・特便共通)
                    ds = New LMI592DS
                    Dim dt As DataTable = ds.Tables(LMI160C.TABLE_NM_IN_NOUNYU)
                    'データセット592IN
                    ds = Me.SetDataSetInData(frm, ds, dt, LMI160C.TABLE_NM_IN_NOUNYU)

            End Select

            Return ds
        End With

    End Function

    ''' <summary>
    ''' データセット設定(印刷)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMI160F, ByVal ds As DataSet, ByVal dt As DataTable, ByVal id As String) As DataSet

        With frm

            Dim Print As String = .cmbPrint.SelectedValue.ToString

            Dim dr As DataRow = dt.NewRow()

            '運賃締め基準の取得
            Dim CustCdL As String = .txtCustCdL.TextValue
            Dim CustCdM As String = .txtCustCdM.TextValue
            'Dim Sime As String = String.Empty

            ''荷主が入力されている場合
            'If String.IsNullOrEmpty(CustCdL) = False Then
            '    '荷主コードでの締め基準の取得
            '    Sime = Me.SimeKijun(CustCdL, CustCdM, "", Sime)
            'End If

            ''データセットに格納
            dr("NRS_BR_CD") = .cmbBr.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue
            dr("CUST_CD_M") = .txtCustCdM.TextValue
            If .optNotKakutei.Checked = True Then
                dr("SEIQ_SYUBETU") = LMI160C.MI_KAKUTEI
            ElseIf .optKakuteiZumi.Checked = True Then
                dr("SEIQ_SYUBETU") = LMI160C.KAKUTEI_ZUMI
            End If
            dr("F_DATE") = .imdNonyuDateFrom.TextValue
            dr("T_DATE") = .imdNonyuDateTo.TextValue
            'dr("UNTIN_CALCULATION_KB") = Sime

            Select Case Print

                Case LMI160C.BUKA_UNCHIN_SEIKYU

                    ' 運賃請求明細書（DIC在庫部課別）
                    dr("PRT_SYUBETU") = LMI160C.BUKA_UNCHIN_SEIKYU

                Case LMI160C.HUTANKA_UNCHIN_SEIKYU

                    ' 運賃請求明細書（DIC在庫部課、負担課別）
                    dr("PRT_SYUBETU") = LMI160C.HUTANKA_UNCHIN_SEIKYU

                Case LMI160C.NOUNYU_UNCHIN_SEIKYU

                    ' 運賃請求明細書（DIC在庫部課、負担課、納入日、納入先別）1便・2便)
                    dr("PRT_SYUBETU") = LMI160C.NOUNYU_UNCHIN_SEIKYU

                Case LMI160C.SYAATUKAI_UNCHIN_SEIKYU
                    'Case LMI160C.NOUNYU_UNCHIN_SEIKYU
                    ' 運賃請求明細書（DIC在庫部課、負担課、納入日、納入先別）車扱い・特便)
                    dr("PRT_SYUBETU") = LMI160C.SYAATUKAI_UNCHIN_SEIKYU

                Case LMI160C.YAMATO_UNCHIN_SEIKYU
                    'Case LMI160C.NOUNYU_UNCHIN_SEIKYU
                    ' 運賃請求明細書（DIC在庫部課、負担課、納入日、納入先別）ヤマト便)
                    dr("PRT_SYUBETU") = LMI160C.YAMATO_UNCHIN_SEIKYU

            End Select

            'データセットの追加
            ds.Tables(id).Rows.Add(dr)

        End With
        Return ds

    End Function

    ''' <summary>
    ''' 運賃締め基準の取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SimeKijun(ByVal CustCdL As String, ByVal CustCdM As String, ByVal Seiq As String, ByVal Sime As String) As String

        Dim drC As DataRow() = Nothing
        Dim count As Integer = 0

        'キャッシュの荷主マスターの値の取得
        drC = Me._V.SelectCustListDataRow(CustCdL, CustCdM, Seiq)

        'データロウのカウントの取得
        count = drC.Count

        'データが1件以上の場合
        If count >= 1 Then

            'データロウの0の運賃締め基準の取得
            Sime = drC(0).Item("UNTIN_CALCULATION_KB").ToString

        End If

        Return Sime
    End Function

#End Region
#Region "ユーティリティ"
    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMI160F)

        'ロック対応  terakawa 2012.07.04 Start
        Call Me._LMIconH.EndAction(frm)

        ''画面解除
        'MyBase.UnLockedControls(frm)

        ''Cursorを元に戻す
        'Cursor.Current = Cursors.Default()

        ''画面の入力項目/ファンクションキーの制御
        'Call Me._G.UnLockedForm()

        'With frm

        '    Me._G.SetLockControl(.cmbPrint, False)
        '    Me._G.SetLockControl(.cmbBr, False)
        '    Me._G.SetLockControl(.txtCustCdL, False)
        '    Me._G.SetLockControl(.txtCustCdM, False)
        '    Me._G.SetLockControl(.optNotKakutei, False)
        '    Me._G.SetLockControl(.optKakuteiZumi, False)
        '    Me._G.SetLockControl(.optAll, False)
        '    Me._G.SetLockControl(.imdNonyuDateFrom, False)
        '    Me._G.SetLockControl(.imdNonyuDateTo, False)

        'End With
        'ロック対応  terakawa 2012.07.04 End

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