' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB060H : 入庫連絡票
'  作  成  者       :  hojo
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMB060ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB060H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB060V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB060G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconV As LMBControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconH As LMBControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconG As LMBControlG

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' サーバ日時を格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _SysData(2) As String

    ''' <summary>
    ''' サーバ日時を取得・設定
    ''' </summary>
    ''' <param name="index">
    ''' 0：サーバ日付
    ''' 1：サーバ時間
    ''' </param>
    ''' <value>サーバ日時用プロパティ</value>
    ''' <returns>truckNmのコントロール</returns>
    ''' <remarks></remarks>
    Private Property SysData(ByVal index As LMB060C.SysData) As String
        Get
            Return _SysData(index)
        End Get
        Set(ByVal value As String)
            _SysData(index) = value
        End Set
    End Property
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
        Dim frm As LMB060F = New LMB060F(Me)

        'Validate共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMBconV = New LMBControlV(Me, sForm)

        'Hnadler共通クラスの設定
        Me._LMBconH = New LMBControlH(sForm, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMBconG = New LMBControlG(sForm)

        'Gamenクラスの設定
        Me._G = New LMB060G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMB060V(Me, frm, Me._LMBconV)


        'フォームの初期化
        Call Me.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB060C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID())

        ''メッセージの表示
        Me.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

        'コントロールの初期値設定
        Dim sysData As String() = MyBase.GetSystemDateTime()
        Me.SysData(LMB060C.SysData.YYYYMMDD) = sysData(0)
        Me.SysData(LMB060C.SysData.HHMMSSsss) = sysData(1)
        Me._G.InitControl(frm, Convert.ToString(Me.SysData(LMB060C.SysData.YYYYMMDD)))

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
    Private Sub OpenMasterPop(ByVal frm As LMB060F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック()
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMB060C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMB060C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then

            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMB060C.EventShubetsu.MASTEROPEN)

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
    Private Sub EnterAction(ByVal frm As LMB060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMB060C.EventShubetsu.MASTEROPEN)

        ''カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMB060C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMB060C.EventShubetsu.ENTER)

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
    Private Function ShowPopupControl(ByVal frm As LMB060F, ByVal objNm As String, ByVal actionType As LMB060C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me.StartAction(frm)


            Select Case objNm

                Case .txtCustCD_L.Name

                    '荷主(大)Lコード
                    'START YANAI 要望番号481
                    'Call Me.CustPop(frm)
                    Call Me.CustPop(frm, actionType)
                    'END YANAI 要望番号481

                Case .txtCustCD_M.Name
                    '荷主(中)Mコード
                    'START YANAI 要望番号481
                    'Call Me.CustPop(frm)
                    Call Me.CustPop(frm, actionType)
                    'END YANAI 要望番号481


            End Select

        End With

        Return True
    End Function


#End Region

#End Region

#Region "マスタPOP"
    'START YANAI 要望番号481
    '''' <summary>
    '''' 荷主マスタ照会(LMZ260)参照
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    '''' 
    'Private Function CustPop(ByVal frm As LMB060F) As Boolean
    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CustPop(ByVal frm As LMB060F, ByVal actionType As LMB060C.EventShubetsu) As Boolean
        'END YANAI 要望番号481

        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowCustPopup(frm)
        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)
        'END YANAI 要望番号481

        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtCustCD_L.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCD_M.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNM_L.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNM_M.TextValue = dr.Item("CUST_NM_M").ToString()

            End With


        End If

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 荷主マスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowCustPopup(ByVal frm As LMB060F) As LMFormData
    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMB060F, ByVal actionType As LMB060C.EventShubetsu) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START YANAI 要望番号481
            '.Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
            '.Item("CUST_CD_M") = frm.txtCustCD_M.TextValue
            If (LMB060C.EventShubetsu.ENTER).Equals(actionType) = True Then
                .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
                .Item("CUST_CD_M") = frm.txtCustCD_M.TextValue
            End If
            'END YANAI 要望番号481
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
    Private Function PrintAction(ByVal frm As LMB060F) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMB060C.EventShubetsu.PRINT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

        'エラーがある場合、終了
        If rtnResult = False Then

            '画面解除
            MyBase.UnLockedControls(frm)

            'Cursorを元に戻す
            Cursor.Current = Cursors.Default()

            Return rtnResult

        End If

        'データセットの設定
        Dim rtDs As DataSet = Me.SetDataSetInData(frm, New LMB060DS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintAction")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs.Merge(New RdPrevInfoDS)
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        rtDs = MyBase.CallWSA(blf, LMB060C.ACTION_ID_PRINT, rtDs)
        If IsMessageExist() = True Then

            Me.EndAction(frm)
            ''メッセージの表示
            Me.ShowMessage(frm, "G007")
            Return False

        End If

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
        MyBase.SetMessage("G062")

        '処理終了アクション
        Call Me.EndAction(frm)

        Return True


    End Function

#End Region

#Region "画面の終了"
    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMB060F) As Boolean

        Return True

    End Function
#End Region

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMB060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '印刷処理の呼び出し
        Call Me.PrintAction(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMB060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'マスタ参照処理の呼び出し
        Call Me.OpenMasterPop(frm)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMB060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMB060F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMB060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

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
    Private Sub StartAction(ByVal frm As LMB060F)

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
    Private Sub EndAction(ByVal frm As LMB060F)

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
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMB060F, ByVal ds As DataSet) As DataSet

        With frm


            '入荷日の値を取得
            Dim dt As DataTable = ds.Tables(LMB060C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow()

            '印刷種別による分岐
            Dim prtSyubetu As String = .cmbPrint.SelectedValue.ToString.Trim()

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("WH_CD") = .cmbSoko.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCD_L.TextValue.Trim()
            dr.Item("CUST_CD_M") = .txtCustCD_M.TextValue.Trim()

            dr.Item("INKA_TP") = "10"
            dr.Item("INKA_KB") = "10"
            dr.Item("INKA_STATE_KB") = "10"
            dr.Item("TOUKI_HOKAN_YN") = "01"
            dr.Item("HOKAN_YN") = "01"
            dr.Item("NIYAKU_YN") = "01"
            dr.Item("TAX_KB") = "01"
            dr.Item("UNCHIN_TP") = "90"
            dr.Item("WH_TAB_STATUS") = "00"
            dr.Item("WH_TAB_YN") = "00"
            dr.Item("WH_TAB_IMP_YN") = "00"
            dr.Item("HOKAN_FREE_KIKAN") = "0"
            dr.Item("INKA_PLAN_QT") = "0.000"
            dr.Item("INKA_TTL_NB") = "0"

            dr.Item("INKA_NO_L") = ""
            dr.Item("INKA_DATE") = .imdNyukaDate.TextValue

            ds.Tables(LMB060C.TABLE_NM_IN).Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class