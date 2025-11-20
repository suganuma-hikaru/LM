' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI530H : セミEDI環境切り替え(丸和物産)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI530ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI530H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI530V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI530G

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

#End Region

#Region "Method"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        ' 自 営業所
        Dim nrsBrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()

        ' 実行可能営業所名
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='", LMI530C.ENABLE_NRS_BR_CD, "'"))(0)
        Dim invDeptNm As String = nrsDr.Item("NRS_BR_NM").ToString()

        Dim ediCustIndex As Integer = LMI530C.ediCustIndex.CSV

        ' カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        ' フォームの作成
        Dim frm As LMI530F = New LMI530F(Me)

        ' Validateクラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        ' Gクラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        ' ハンドラー共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        ' Gamenクラスの設定
        Me._G = New LMI530G(Me, frm)

        ' Validateクラスの設定
        Me._V = New LMI530V(Me, frm, Me._LMIconV, Me._LMIconG)

        ' フォームの初期化
        Call MyBase.InitControl(frm)

        ' キーイベントをフォームで受け取る
        frm.KeyPreview = True

        ' 画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(nrsBrCd)

        ' タブインデックスの設定
        Call Me._G.SetTabIndex()

        ' コントロール個別設定
        Call Me._G.SetControl()

        If nrsBrCd = LMI530C.ENABLE_NRS_BR_CD Then
            Dim ds As DataSet = GetInitData(nrsBrCd)
            If ds.Tables("LMI530OUT").Rows.Count > 0 Then
                Dim ecIdx As Integer
                If Integer.TryParse(ds.Tables("LMI530OUT").Rows(0).Item("EDI_CUST_INDEX").ToString(), ecIdx) Then
                    If ecIdx = LMI530C.ediCustIndex.Excel OrElse ecIdx = LMI530C.ediCustIndex.CSV Then
                        ediCustIndex = ecIdx
                    End If
                End If
            End If
        End If

        ' 初期値設定
        Call Me._G.SetInitValue(ediCustIndex, itemsAdd:=True)

        ' メッセージの表示
        If nrsBrCd = LMI530C.ENABLE_NRS_BR_CD Then
            MyBase.ShowMessage(frm, "G006")
        Else
            MyBase.ShowMessage(frm, "E237", New String() {String.Concat(invDeptNm, "以外")})
        End If

        ' フォームの表示
        frm.Show()

        ' 初期値設定(フォーム表示時に SelectedIndex = 0 に戻ってしまう事象回避のため)
        Call Me._G.SetInitValue(ediCustIndex, itemsAdd:=False)

        ' デザイナ上で設定できない(実行時戻ってしまう)画面幅関連の縮小方向の調整
        Call Me._G.AdjustFormaAndControls()

        ' フォーカスの設定
        Call Me._G.SetFoucus()

        ' カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI530F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.UpdateData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI530F, ByVal e As System.Windows.Forms.KeyEventArgs)

        ' 終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI530F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

#End Region ' "イベント定義(一覧)"

#Region "初期表示用データ取得"

    ''' <summary>
    ''' 初期表示用データ取得
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <returns></returns>
    Private Function GetInitData(ByVal nrsBrCd As String) As DataSet

        Dim ds As DataSet = GetDsInstance()

        ds.Tables("LMI530IN").Clear()
        Dim dr As DataRow = ds.Tables("LMI530IN").NewRow()
        dr.Item("NRS_BR_CD") = nrsBrCd
        ds.Tables("LMI530IN").Rows.Add(dr)

        '==========================
        ' WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMI530BLF", "SelectInitData", ds)

        Return ds

    End Function

#End Region ' "初期表示用データ取得"

#Region "更新"

    Private Function UpdateData(ByVal frm As LMI530F) As Boolean

        ' 自 営業所
        Dim nrsBrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()

        ' 処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        ' 権限チェック
        If Me._V.IsAuthorityChk() = False Then

            ' 処理終了アクション
            Call Me._LMIconH.EndAction(frm)
            Return False
        End If

        ' 入力チェック
        If Me._V.IsInputCheck() = False Then

            ' 処理終了アクション
            Call Me._LMIconH.EndAction(frm)
            Return False

        End If

        ' 引数設定
        Dim ds As DataSet = GetDsInstance()
        ds.Tables("LMI530IN").Clear()
        Dim dr As DataRow = ds.Tables("LMI530IN").NewRow()

        dr.Item("NRS_BR_CD") = nrsBrCd
        Dim ediCustIndex As Integer = LMI530C.ediCustIndex.CSV
        With frm
            If .cmbSelectKb.SelectedText = LMI530C.cmbSelectKbItems.CSV Then
                ediCustIndex = LMI530C.ediCustIndex.CSV
            ElseIf .cmbSelectKb.SelectedText = LMI530C.cmbSelectKbItems.Excel Then
                ediCustIndex = LMI530C.ediCustIndex.Excel
            End If
        End With
        dr.Item("EDI_CUST_INDEX") = ediCustIndex.ToString()

        ds.Tables("LMI530IN").Rows.Add(dr)

        ' ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateData")

        '==========================
        ' WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMI530BLF", "UpdateData", ds)

        ' エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)

            ' 処理終了アクション
            Call Me._LMIconH.EndAction(frm)

            Return False
        End If

        ' キャッシュ最新化
        ' EDI対象荷主マスタ
        MyBase.LMCacheMasterData(LMConst.CacheTBL.EDI_CUST)
        ' セミEDI情報設定マスタ
        MyBase.LMCacheMasterData(LMConst.CacheTBL.SEMIEDI_INFO_STATE)

        ' メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"セミEDI環境切り替え処理", ""})

        ' 処理終了アクション
        Call Me._LMIconH.EndAction(frm)

        ' ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateData")

        ''Call debugSub1(nrsBrCd)
        ''Call debugSub2(nrsBrCd)

        Return True

    End Function

#End Region ' "更新"

#Region "デバッグサポート"

    ''Private Sub debugSub1(ByVal nrsBrCd As String)

    ''    Dim brCd As String = nrsBrCd
    ''    Dim inoutKb As String = "0"
    ''    Dim whCd As String = "400"
    ''    Dim custCdL As String = "00330"
    ''    Dim custCdM As String = "00"
    ''    Dim where As String = ""
    ''    where = String.Concat(where, "INOUT_KB = ", " '", inoutKb, "'")
    ''    where = String.Concat(where, " AND ", "WH_CD = ", " '", whCd, "'")
    ''    where = String.Concat(where, " AND ", "NRS_BR_CD = ", " '", brCd, "'")
    ''    where = String.Concat(where, " AND ", "CUST_CD_L = ", " '", custCdL, "'")
    ''    where = String.Concat(where, " AND ", "CUST_CD_M = ", " '", custCdM, "'")
    ''    where = String.Concat(where, " AND ", "SYS_DEL_FLG = '0'")
    ''    Dim drEdiCust As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(where)

    ''End Sub

    ''Private Sub debugSub2(ByVal nrsBrCd As String)

    ''    Dim brCd As String = nrsBrCd
    ''    Dim whCd As String = "400"
    ''    Dim custCdL As String = "00330"
    ''    Dim custCdM As String = "00"
    ''    Dim inoutKb As String = "20"
    ''    Dim where As String = ""
    ''    where = String.Concat(where, "WH_CD = ", " '", whCd, "'")
    ''    where = String.Concat(where, " AND ", "NRS_BR_CD = ", " '", brCd, "'")
    ''    where = String.Concat(where, " AND ", "CUST_CD_L = ", " '", custCdL, "'")
    ''    where = String.Concat(where, " AND ", "CUST_CD_M = ", " '", custCdM, "'")
    ''    where = String.Concat(where, " AND ", "INOUT_KB = ", " '", inoutKb, "'")
    ''    where = String.Concat(where, " AND ", "SYS_DEL_FLG = '0'")
    ''    Dim drSemiediInfoState As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEMIEDI_INFO_STATE).Select(where)

    ''End Sub

#End Region ' "デバッグサポート"

#Region "共通処理"

    Private Function GetDsInstance() As DataSet

        Return New LMI530DS()

    End Function

#End Region

#End Region ' "Method"

End Class