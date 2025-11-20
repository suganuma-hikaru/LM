' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタメンテ
'  プログラムID     :  LMM350H : 初期出荷元マスタ
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI

''' <summary>
''' LMM350ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM350H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM350V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM350G

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConH As LMMControlH


    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConV As LMMControlV


    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG


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

        '***** 必要であれば、画面間データを取得する,又はパラメータをフィールドに格納する *****
        '画面間データを取得する
        ' Dim prmDs As DataSet = prm.ParamDataSet
        'Me._PrmDs = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMM350F = New LMM350F(Me)

        'Validateクラスの設定
        Me._V = New LMM350V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMM350G(Me, frm)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sFrom, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'フォームの初期化
        MyBase.InitControl(frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

        '↓ データ取得の必要があればここにコーディングする。

        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM350F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM350C.EventShubetsu.KENSAKU) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '検索処理を行う
        Call Me.SelectData(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 設定処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ClickSetteiBtn(ByVal frm As LMM350F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM350C.EventShubetsu.SETTEI) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._V.SprSelectCount()

        '項目チェック
        If Me._V.IsSetteiInputChk(list.Count) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "設定処理") = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM350DS()
        Call Me.SetDataSetInUpdate(frm, ds, list)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SetteiData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM350BLF", "SetteiData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist = True Then
            '返却メッセージを設定
            MyBase.ShowMessage(frm)

            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'TODO:削除予定 キャッシュ再取得(マスタメンテ画面のみ)
        'Call MyBase.LMCacheMasterData(LMConst.CacheTBL.M_DEFAULT_SOK)

        '再検索を行う
        Call Me.SelectDataAgain(frm)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SetteiData")

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMM350F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM350C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM350C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMM350F)

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
    Private Sub EndAction(ByVal frm As LMM350F)

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
    Private Sub SelectData(ByVal frm As LMM350F)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        MyBase.SetLimitCount(LMM350C.LIMITED_COUNT)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Me._FindDs = New LMM350DS()
        Call SetDataSetInData(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM350BLF", "SelectListData", Me._FindDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then     'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA("LMM350BLF", "SelectListData", Me._FindDs)

                    '検索成功時共通処理を行う
                    Call Me.SuccessSelect(frm, rtnDs)

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '検索失敗時共通処理を行う
                    Call Me.FailureSelect(frm)
                End If
            Else

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                '検索失敗時共通処理を行う
                Call Me.FailureSelect(frm)
            End If
        Else

            '検索成功時共通処理を行う
            Call Me.SuccessSelect(frm, rtnDs)
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 再検索処理を行う
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectDataAgain(ByVal frm As LMM350F)

        '強制実行フラグの設定
        MyBase.SetForceOparation(True)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM350BLF", "SelectListData", Me._FindDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm)
            '検索失敗時共通処理を行う
            Call Me.FailureSelect(frm)
        Else

            '検索成功時共通処理を行う
            Call Me.SuccessSelect(frm, rtnDs)

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G015", New String() {""})

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMM350F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM350C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Me._CntSelect = MyBase.GetResultCount.ToString()

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal frm As LMM350F)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal frm As LMM350F, ByVal msg As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
            Return False
        End If

        Return True

    End Function

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM350F)

        Dim dr As DataRow = Me._FindDs.Tables(LMM350C.TABLE_NM_IN).NewRow()

        dr.Item("SCM_CUST_CD") = frm.cmbCustCd.SelectedValue
        dr.Item("UN_LINK_FLG") = frm.chkSoukoMisettei.GetBinaryValue
        dr.Item("ZIP_NO") = frm.txtDestZip.TextValue
        dr.Item("BR_CD") = LMUserInfoManager.GetNrsBrCd()    'ログインユーザの営業所設定

        With frm.sprDetail.ActiveSheet

            dr.Item("JIS_CD") = Me._V.GetCellValue(.Cells(0, LMM350G.sprDetailDef.JIS_CD.ColNo))
            dr.Item("JIS_NM") = Me._V.GetCellValue(.Cells(0, LMM350G.sprDetailDef.JIS_NM.ColNo))
            dr.Item("WH_CD") = Me._V.GetCellValue(.Cells(0, LMM350G.sprDetailDef.SOKO_NM.ColNo))

            Me._FindDs.Tables(LMM350C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(設定内容格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">登録/更新内容を格納するデータセット</param>
    ''' <param name="list">登録/更新対象行を格納しているリスト</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInUpdate(ByVal frm As LMM350F, ByVal ds As DataSet, ByVal list As ArrayList)

        Dim dr As DataRow = Nothing
        Dim dt As DataTable = ds.Tables(LMM350C.TABLE_NM_UPDATE)
        Dim jisColNo As Integer = LMM350G.sprDetailDef.JIS_CD.ColNo
        Dim updFlgCloNo As Integer = LMM350G.sprDetailDef.UPD_FLG.ColNo
        Dim updDtCloNo As Integer = LMM350G.sprDetailDef.SYS_UPD_DATE.ColNo
        Dim updTmColNo As Integer = LMM350G.sprDetailDef.SYS_UPD_TIME.ColNo
        Dim rowIndex As Integer = 0

        Dim max As Integer = list.Count - 1
        For i As Integer = 0 To max

            rowIndex = Convert.ToInt32(list(i))

            dr = dt.NewRow()

            dr.Item("SCM_CUST_CD") = Me._FindDs.Tables(LMM350C.TABLE_NM_IN).Rows(0).Item("SCM_CUST_CD")
            dr.Item("WH_CD") = frm.cmbSoko.SelectedValue

            With frm.sprDetail.ActiveSheet

                dr.Item("JIS_CD") = Me._V.GetCellValue(.Cells(rowIndex, jisColNo))
                dr.Item("UPD_FLG") = Me._V.GetCellValue(.Cells(rowIndex, updFlgCloNo))
                dr.Item("SYS_UPD_DATE") = Me._V.GetCellValue(.Cells(rowIndex, updDtCloNo))
                dr.Item("SYS_UPD_TIME") = Me._V.GetCellValue(.Cells(rowIndex, updTmColNo))
                dr.Item("BR_CD") = LMUserInfoManager.GetNrsBrCd()    'ログインユーザの営業所設定

            End With

            dt.Rows.Add(dr)

        Next

    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM350F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListEvent")

        '検索処理
        Call Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM350F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM350F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓========================

    ''' <summary>
    ''' 設定ボタン押下時の処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks></remarks>
    Friend Sub ClickSetteiBtn(ByRef frm As LMM350F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClickSetteiBtn")

        '設定処理
        Call Me.ClickSetteiBtn(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClickSetteiBtn")

    End Sub
    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM350F_KeyDown(ByVal frm As LMM350F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM350F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM350F_KeyDown")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class