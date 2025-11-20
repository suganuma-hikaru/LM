' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMN080H : 欠品警告
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN080ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMN080H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMN080V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMN080G

    ''' <summary>
    ''' ParameterDS格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' 欠品明細データ格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _MeiDs As DataSet

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
        Me._PrmDs = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMN080F = New LMN080F(Me)

        'Validateクラスの設定
        Me._V = New LMN080V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMN080G(Me, frm)

        'フォームの初期化
        Call Me.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID())

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitWareSpread()

        Call Me._G.InitItemSpread()

        '初期値設定
        Me._G.SetInitValue(frm, Me._PrmDs)

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

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
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub KensakuEvent(ByVal frm As LMN080F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN080C.EventShubetsu.KENSAKU) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック()
        If Me._V.IsKensakuInputChk = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '明細データ保存フィールドの初期化
        Me._MeiDs = Nothing

        '検索処理を行う
        Call Me.Kensaku(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMN080F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        '権限チェック
        If Me._V.IsAuthorityChk(LMN080C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック()
        If Me._V.IsDoubleClickInputChk(e) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '明細SPREADの初期化
        Call Me._G.InitItemSpread()
        '表示メッセージの初期化
        MyBase.ClearMessageAria(frm)

        '明細データ表示を行う
        '明細データ取得
        Dim ds As DataSet = Me._MeiDs.Copy
        '選択された倉庫コードを取得
        Dim sokoCd As String = frm.sprSokoDetail.ActiveSheet.Cells(e.Row, LMN080G.sprWareDetailDef.WARE_CD.ColNo).Text
        '取得明細データより先頭倉庫コード明細データを取得
        Dim meiTableName As String = "LMN080OUT_M"
        Dim meiDs As DataSet = New LMN080DS()
        Dim meiDt As DataTable = meiDs.Tables(meiTableName)
        '取得条件設定
        Dim filter As String = String.Concat("SOKO_CD = '", sokoCd, "'")
        '明細データ取得
        Dim meiDr As DataRow() = ds.Tables(meiTableName).Select(filter)
        'データセットに設定
        Dim meiNum As Integer = meiDr.Length
        If meiNum > 0 Then
            For i As Integer = 0 To meiNum - 1
                meiDt.ImportRow(meiDr(i))
            Next
            '明細部表示設定
            frm.cmbWare.SelectedValue = sokoCd
            frm.LmImNumber1.TextValue = frm.sprSokoDetail.ActiveSheet.Cells(e.Row, LMN080G.sprWareDetailDef.KEPPIN_HIN_NUM.ColNo).Text

            '明細データをSPREDに表示
            Call Me._G.SetItemSpread(meiDs)

        End If

        '終了処理
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMN080F)

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
    Private Sub EndAction(ByVal frm As LMN080F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub Kensaku(ByVal frm As LMN080F)

        'DataSet設定
        Dim ds As DataSet = New LMN080DS()
        Call Me.SetDataSetInData_Kensaku(frm, ds)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()
        frm.sprSokoDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Kensaku")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMN080BLF", "Kensaku", ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G007")

            '検索失敗時共通処理を行う
            Call Me.FailureSelect(frm)

        Else

            '検索成功時共通処理を行う
            Call Me.SuccessSelect(frm, rtnDs)

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Kensaku")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMN080F, ByVal ds As DataSet)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetWareSpread(ds)

        '欠品明細データをフィールド変数に保存
        Me._MeiDs = ds.Copy

        '倉庫SPREADの先頭に表示されている倉庫コードを取得
        Dim sokoCd As String = ds.Tables("LMN080OUT_L").Rows(0).Item("SOKO_CD").ToString()
        '取得明細データより先頭倉庫コード明細データを取得
        Dim meiTableName As String = "LMN080OUT_M"
        Dim meiDs As DataSet = New LMN080DS()
        Dim meiDt As DataTable = meiDs.Tables(meiTableName)
        '取得条件設定
        Dim filter As String = String.Concat("SOKO_CD = '", sokoCd, "'")
        '明細データ取得
        Dim meiDr As DataRow() = ds.Tables(meiTableName).Select(filter)
        'データセットに設定
        Dim meiNum As Integer = meiDr.Length
        If meiNum > 0 Then
            For i As Integer = 0 To meiNum - 1
                meiDt.ImportRow(meiDr(i))
            Next
            '明細部表示設定
            frm.cmbWare.SelectedValue = sokoCd
            frm.LmImNumber1.TextValue = ds.Tables("LMN080OUT_L").Rows(0).Item("KEPPIN_NB").ToString()

            '明細データをSPREDに表示
            Call Me._G.SetItemSpread(meiDs)

        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G008", New String() {ds.Tables("LMN080OUT_L").Rows.Count.ToString()})

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal frm As LMN080F)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMN080F) As Boolean

        Return True

    End Function


#End Region

#End Region 'イベント定義(一覧)

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">検索条件を格納するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_Kensaku(ByVal frm As LMN080F, ByVal ds As DataSet)

        '画面荷主コード取得
        Dim inDr As DataRow = ds.Tables(LMN080C.TABLE_NM_IN).NewRow()

        inDr("SCM_CUST_CD") = frm.cmbCustCd.SelectedValue.ToString()

        ds.Tables(LMN080C.TABLE_NM_IN).Rows.Add(inDr)

        '営業所接続情報取得
        '区分マスタ検索処理
        Dim filter As String = String.Empty
        '接続営業所コードとLMS側荷主コード取得
        filter = String.Concat("KBN_GROUP_CD = 'S033' AND KBN_NM3 = '", frm.cmbCustCd.SelectedValue, "'")
        Dim getDr_S033 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
        Dim getDrNum As Integer = getDr_S033.Length - 1
        For i As Integer = 0 To getDrNum

            '営業所毎情報の格納テーブル
            Dim dr As DataRow = ds.Tables("BR_CD_LIST").NewRow()

            dr.Item("BR_CD") = getDr_S033(i).Item("KBN_NM4").ToString()
            dr.Item("SCM_CUST_CD") = getDr_S033(i).Item("KBN_NM3").ToString()
            dr.Item("LMS_CUST_CD") = getDr_S033(i).Item("KBN_NM5").ToString()

            '接続先名称と移行フラグの取得
            filter = String.Concat("KBN_GROUP_CD = 'L001' AND KBN_NM3 = '", getDr_S033(i).Item("KBN_NM4").ToString(), "'")
            Dim getDr_L001 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)

            dr.Item("SV1_CONNECT_NM") = getDr_L001(0).Item("KBN_NM7").ToString()
            dr.Item("SV2_CONNECT_NM") = getDr_L001(0).Item("KBN_NM5").ToString()

            dr.Item("IKO_FLG") = getDr_L001(0).Item("KBN_NM4").ToString()

            'DB参照先取得
            filter = String.Concat("KBN_GROUP_CD = 'D003' AND KBN_NM1 = '", getDr_S033(i).Item("KBN_NM4").ToString(), "'")
            Dim getDr_D003 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)

            If String.IsNullOrEmpty(getDr_D003(0).Item("KBN_NM3").ToString()) Then
                dr.Item("SV1_LM_MST") = getDr_D003(0).Item("KBN_NM4").ToString()
            Else
                dr.Item("SV1_LM_MST") = getDr_D003(0).Item("KBN_NM3").ToString()
            End If
            dr.Item("SV1_LM_TRN") = getDr_D003(0).Item("KBN_NM4").ToString()
            dr.Item("SV2_LM_MST") = getDr_D003(0).Item("KBN_NM7").ToString()
            dr.Item("SV2_LM_TRN") = getDr_D003(0).Item("KBN_NM8").ToString()

            ds.Tables("BR_CD_LIST").Rows.Add(dr)

        Next

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Import JDE")

        ''Import JDE
        'Me.sendPrint(frm, e)

        Logger.EndLog(Me.GetType.Name, "Import JDE")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.KensakuEvent(frm)

        Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMN080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMN080F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMN080F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        '明細データの表示処理
        Me.SelectListData(frm, e)

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class