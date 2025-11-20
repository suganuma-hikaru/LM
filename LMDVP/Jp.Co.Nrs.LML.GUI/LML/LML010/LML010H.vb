' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : 協力会社管理
'  プログラムID     :  LML010H : 協力会社
'  作  成  者       :  [大極]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LML010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LML010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LML010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LML010G


    Private _ConG As LMLControlG

    Private _ConH As LMLControlH

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

        'フォームの作成
        Dim frm As LML010F = New LML010F(Me)

        Me._ConG = New LMLControlG(DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LML010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LML010G(Me, frm, Me._ConG)

        'フォームの初期化
        Call Me.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(False)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '↓ データ取得の必要があればここにコーディングする。
        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        Me.ShowMessage(frm, "G006")

        '画面の入力項目の制御
        Call _G.SetControlsStatus(False)

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
    ''' 画面の遷移イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SelectionEvent(ByVal frm As LML010F)

        '処理開始アクション
        Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LML010C.EventShubetsu.SENTAKU) = False Then
            '処理終了アクション
            Me.EndAction(frm)
            Exit Sub
        End If

        '単項目チェック
        If Me._V.IsInputCheck() = False Then
            Me.ShowMessage(frm, "E199", New String() {"荷主コード"})
            '処理終了アクション
            Me.EndAction(frm)
            Exit Sub
        End If

        'T007 該当区分コード　内容取得
        'コンボボックスより区分CDを取得
        Dim Kbnt007Cd As String = frm.CmbCustNm.SelectedValue.ToString()

        Dim sql As String = String.Concat("KBN_GROUP_CD = 'L007' ", " AND KBN_CD = '", Kbnt007Cd, "' ")
        Dim kbnt007Dr() As DataRow = Nothing
        kbnt007Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat(sql))

        'コンボボックスより区分CDを取得
        Dim KbnCd As String = frm.CmbShori.SelectedValue.ToString()


        '画面IDの取得
        'Dim formId As String = Me.GetShori(KbnCd)

        'T008 該当区分コード　内容取得
        'sql = String.Concat("KBN_GROUP_CD = 'L008' ", " AND KBN_NM5 = '", KbnCd, "' ")
        sql = String.Concat("KBN_GROUP_CD = 'L008' ", " AND KBN_CD = '", KbnCd, "' ")
        Dim kbnDr As DataRow() = Nothing
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat(sql))

        '区分の日陸営業所とログインの営業所チェック
        'ログインユーザーの営業所CD
        Dim NrsBrCd As String = LMUserInfoManager.GetNrsBrCd

        If kbnDr(0).Item("KBN_NM3").Equals(NrsBrCd) = False Then
            Me.ShowMessage(frm, "E223", New String() {"対象の営業所が違うので処理"})
            Me.EndAction(frm)
            Exit Sub
        End If

        '----1:作業コードが設定されていないものがあったらNG (作業金額が0円以外が対象


        '-----2:作業コードがダブっていたらＮＧ


        Dim ds As DataSet = New LML010DS()
        Dim dt As DataTable = ds.Tables("LML010IN")
        Dim dr As DataRow = dt.NewRow()

        dr("KBN_GROUP_CD") = kbnDr(0).Item("KBN_GROUP_CD")
        dr("KBN_CD") = kbnDr(0).Item("KBN_CD")
        dr("NRS_CUST_NM") = kbnDr(0).Item("KBN_NM2")
        dr("NRS_CUST_CD") = kbnDr(0).Item("KBN_NM1")
        dr("NRS_BR_CD") = kbnDr(0).Item("KBN_NM3")
        dr("NRS_TRN_NM") = kbnDr(0).Item("KBN_NM4")
        dr("NRS_WH_CD") = kbnt007Dr(0).Item("KBN_NM2")
        dr("NRS_TOU_NO") = kbnt007Dr(0).Item("KBN_NM4")
        dr("NRS_SITU_NO") = kbnt007Dr(0).Item("KBN_NM5")
        dr("NRS_ZONE_CD") = kbnt007Dr(0).Item("KBN_NM6")
        dr("PC_CUST_CD") = kbnDr(0).Item("KBN_NM6")
        dr("PC_CUST_NM") = kbnDr(0).Item("KBN_NM7")
        dr("PC_BR_CD") = kbnDr(0).Item("KBN_NM8")
        dr("PC_TRN_NM") = kbnDr(0).Item("KBN_NM9")

        ds.Tables("LML010IN").Rows.Add(dr)

        Dim wkDs As DataSet = ds.Copy()
        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LML010BLF", "Sagyo_CHK1", ds)

        'Dim rtnDs As DataSet = Me.MyBase.CallWSA("LMB020BLF", actionStr, ds).CallWSAAction(DirectCast(frm, Form) _
        '                                                 , "LMB010BLF", "SelectListData", rtDs _
        '                                                 , lc, mc)
        If ds.Tables("SAGYO_CHK").Rows.Count > 0 Then
            '1:作業コードが設定されていないものがあったらNG (作業金額が0円以外が対象
            Dim sMSG As String = String.Empty
            For i As Integer = 0 To ds.Tables("SAGYO_CHK").Rows.Count - 1
                sMSG = sMSG & String.Concat(vbCr, ds.Tables("SAGYO_CHK").Rows(i).Item("SAGYO_CD").ToString())
            Next
            Me.ShowMessage(frm, "E03A", New String() {String.Concat(sMSG, vbCr, "が、協力会社作業に設定されていません。")})
            '処理終了アクション
            Me.EndAction(frm)
            Exit Sub

        End If

        'ds 再セット
        ds = wkDs.Copy
        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LML010BLF", "Sagyo_CHK2", ds)

        If ds.Tables("SAGYO_CHK").Rows.Count > 0 Then
            '12:作業コードがダブっていたらＮＧ
            Dim sMSG As String = String.Empty
            For i As Integer = 0 To ds.Tables("SAGYO_CHK").Rows.Count - 1
                sMSG = sMSG & String.Concat(vbCr, ds.Tables("SAGYO_CHK").Rows(i).Item("SAGYO_CD").ToString(),
                            " ", ds.Tables("SAGYO_CHK").Rows(i).Item("DABURI").ToString(), " 件")
            Next
            Me.ShowMessage(frm, "E03A", New String() {String.Concat(sMSG, vbCr, "が、協力会社作業にダブりがあります。")})
            '処理終了アクション
            Me.EndAction(frm)
            Exit Sub

        End If

        'データ削除・作成処理
        'ds 再セット
        ds = wkDs.Copy
        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LML010BLF", "Data_creat", ds)
        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
        Else
            MyBase.ShowMessage(frm, "G035", New String() {"実行", String.Empty})

        End If

        '処理終了アクション
        Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' コンボボックスチェンジイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ChangeCombobBox(ByVal frm As LML010F)

        '処理開始アクション
        Me.StartAction(frm)

        If frm.CmbCustNm.SelectedValue().Equals("") Then
            Me._G.ClearComboBoxShori()

            '画面の入力項目の制御
            Call _G.SetControlsStatus(False)

            'ファンクションキーの設定
            Call _G.SetFunctionKey(False)

            'メッセージの表示
            Me.ShowMessage(frm, "G006")

            '処理終了アクション
            Me.EndAction(frm)

            Exit Sub
        End If
        Dim CustNm As String = frm.CmbCustNm.SelectedValue.ToString()

        If CustNm.Length = 1 Then
            CustNm = String.Concat("0", CustNm)
        End If

        Me._G.SetComboBoxShori(CustNm)

        'メッセージの表示
        Me.ShowMessage(frm, "G006")

        '処理終了アクション
        Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LML010F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LML010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FormSelection")

        Call Me.SelectionEvent(frm)

        Logger.EndLog(Me.GetType.Name, "FormSelection")

    End Sub

    '''' <summary>
    '''' F10押下時処理呼び出し
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <param name="e">ファンクションキーイベント</param>
    '''' <remarks></remarks>
    'Friend Sub FunctionKey10Press(ByRef frm As LML010F, ByVal e As System.Windows.Forms.KeyEventArgs)

    '    Logger.StartLog(Me.GetType.Name, "FormSelection")

    '    Call Me.SelectionEvent(frm)

    '    Logger.EndLog(Me.GetType.Name, "FormSelection")

    'End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LML010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LML010F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' コンボボックスチェンジイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub ChangeCombo(ByRef frm As LML010F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "ChangeCombobBox")

        Me.ChangeCombobBox(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "内部Method"

    ''' <summary>
    ''' 画面ID取得
    ''' </summary>
    ''' <param name="kbncd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetShori(ByVal kbncd As String) As String

        '画面名より画面IDを取得する
        Dim sql As String = String.Concat("KBN_GROUP_CD = 'L007' ", " AND KBN_CD = '", kbncd, "' ")

            'キャッシュの検索
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sql)

        '画面IDの取得
        Return dr(0).Item(New String() {"KBN_NM3"}(0)).ToString()

    End Function

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As Form)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As Form)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '内部Method

#End Region 'Method

End Class