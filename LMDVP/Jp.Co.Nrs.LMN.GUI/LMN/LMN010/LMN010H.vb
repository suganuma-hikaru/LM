' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMN010H : 出荷データ一覧
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMN010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMN010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMN010G

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' ParameterDS格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

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
        Dim frm As LMN010F = New LMN010F(Me)

        'Validateクラスの設定
        Me._V = New LMN010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMN010G(Me, frm)

        'フォームの初期化
        Call Me.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID())

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '初期値設定
        Me._G.SetInitValue(frm)

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
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMN010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN010C.EventShubetsu.SPREAD_DOUBLE_CLICK) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック()
        If Me._V.IsDoubleClickInputChk(e) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMN010DS()
        Dim dt As DataTable = ds.Tables("LMN010IN")
        Dim dRow As DataRow = dt.NewRow
        With frm.sprDetail.ActiveSheet
            dRow.Item("SCM_CTL_NO_L") = .Cells(e.Row, LMN010G.sprDetailDef.SCM_CTL_NO_L.ColNo).Value
            dRow.Item("CRT_DATE") = .Cells(e.Row, LMN010G.sprDetailDef.CRT_DATE.ColNo).Value
            dRow.Item("FILE_NAME") = .Cells(e.Row, LMN010G.sprDetailDef.FILE_NAME.ColNo).Value
            dRow.Item("REC_NO") = .Cells(e.Row, LMN010G.sprDetailDef.REC_NO.ColNo).Value
            dRow.Item("INSERT_FLG") = .Cells(e.Row, LMN010G.sprDetailDef.INSERT_FLG.ColNo).Value
        End With
        dt.Rows.Add(dRow)

        '存在チェックを行う
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMN010BLF", "CheckNotExistData", ds)

        'メッセージ判定
        If MyBase.IsErrorMessageExist() = False Then

            'パラメータ生成
            Dim prm As LMFormData = New LMFormData()
            Dim prmDs As DataSet = New LMN020DS()
            Dim prmDt As DataTable = prmDs.Tables("LMN020IN")
            Dim prmDr As DataRow = prmDt.NewRow

            With frm.sprDetail.ActiveSheet
                prmDr.Item("SCM_CUST_CD") = frm.cmbCustCd.SelectedValue
                prmDr.Item("SOKO_CD") = .Cells(e.Row, LMN010G.sprDetailDef.SOKO_NM.ColNo).Value
                prmDr.Item("CUST_ORD_NO_L") = .Cells(e.Row, LMN010G.sprDetailDef.CUST_ORD_NO_L.ColNo).Value
                prmDr.Item("SCM_CTL_NO_L") = .Cells(e.Row, LMN010G.sprDetailDef.SCM_CTL_NO_L.ColNo).Value
                prmDr.Item("CRT_DATE") = .Cells(e.Row, LMN010G.sprDetailDef.CRT_DATE.ColNo).Value
                prmDr.Item("FILE_NAME") = .Cells(e.Row, LMN010G.sprDetailDef.FILE_NAME.ColNo).Value
                prmDr.Item("REC_NO") = .Cells(e.Row, LMN010G.sprDetailDef.REC_NO.ColNo).Value
                prmDr.Item("INSERT_FLG") = .Cells(e.Row, LMN010G.sprDetailDef.INSERT_FLG.ColNo).Value
                prmDr.Item("BR_CD") = .Cells(e.Row, LMN010G.sprDetailDef.BR_CD.ColNo).Value
            End With

            prmDt.Rows.Add(prmDr)
            prm.ParamDataSet = prmDs

            '出荷データ詳細(LMN020)を開く
            LMFormNavigate.NextFormNavigate(Me, "LMN020", prm)

        End If

        Me.ShowMessage(frm)

        Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMN010F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMN010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN010C.EventShubetsu.KENSAKU) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック()
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
    Private Sub SetteiEvent(ByVal frm As LMN010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN010C.EventShubetsu.SETTEI) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._V.SprSelectCount()

        '項目チェック
        If Me._V.IsSetteiInputChk(list) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "設定処理") = False Then
            '設定されているメッセージのクリアー
            Call Me.ClearMessageAria(frm)
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMN010DS()
        Call Me.SetDataSetInData_Settei(frm, ds, list)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SetteiData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMN010BLF", "SetteiData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist = True Then
            '返却メッセージを設定
            MyBase.ShowMessage(frm)

            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SetteiData")

        '終了処理
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 送信指示処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SoushinShijiEvent(ByVal frm As LMN010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN010C.EventShubetsu.SOUSHIN_SHIJI) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._V.SprSelectCount()

        '項目チェック
        If Me._V.IsSoushinShijiInputChk(list) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '欠品チェック
        '欠品判定用フラグ(TRUE:欠品有り FALSE:正常)
        Dim KeppinFlg As Boolean = False
        For Each defNo As Integer In list
            If frm.sprDetail.ActiveSheet.Cells(defNo, LMN010G.sprDetailDef.KEPPIN_FLG.ColNo).Text = "欠" Then
                KeppinFlg = True
            End If
        Next
        '欠品有りの場合、確認メッセージ表示
        If KeppinFlg Then
            If MyBase.ShowMessage(frm, "C001", New String() {"欠品となる商品を含むオーダーが存在"}) = MsgBoxResult.Cancel Then
                '終了処理
                Call Me.EndAction(frm)
                Exit Sub
            End If
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "送信指示") = False Then
            '設定されているメッセージのクリアー
            Call Me.ClearMessageAria(frm)
            Call Me.ClearMessageData()
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMN010DS()
        Call Me.SetDataSetInData_SoushinShiji(frm, ds, list)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SoushinShiji")

        '==========================
        'WSAクラス呼出
        '==========================
        '送信指示データ取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMN010BLF", "SoushinShijiSelect", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist = True Then
            '返却メッセージを設定
            MyBase.ShowMessage(frm)

            Call Me.EndAction(frm) '終了処理
            Exit Sub

        Else
            'メッセージエリアの設定
            MyBase.ShowMessage(frm)
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SoushinShiji")

        Call Me.EndAction(frm) '終了処理

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 実績取込処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub JissekiTorikomiEvent(ByVal frm As LMN010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN010C.EventShubetsu.JISSEKI_TORIKOMI) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsJissekiTorikomiInputChk() = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "実績取込") = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMN010DS()
        Call Me.SetDataSetInData_Jisseki(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiTorikomi")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMN010BLF", "JissekiTorikomi", ds)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            '返却メッセージを設定
            MyBase.ShowMessage(frm)

            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"実績取込処理", String.Concat(MyBase.GetResultCount.ToString(), "件")})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiTorikomi")

        Call Me.EndAction(frm) '終了処理

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 実績送信処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub JissekiSoushinEvent(ByVal frm As LMN010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN010C.EventShubetsu.JISSEKI_SOUSHIN) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "実績送信") = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMN010DS()
        Call Me.SetDataSetInData_Jisseki(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiSoushin")

        '==========================
        'WSAクラス呼出
        '==========================
        '出荷日、納入日と実出荷日、納入日が一致しないデータが存在しないかチェック
        ds = MyBase.CallWSA("LMN010BLF", "CheckJissekiSoushin", ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then  '一致しないデータが存在した場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Cancel Then '「いいえ」を選択

                    '実績送信データ取得
                    ds = MyBase.CallWSA("LMN010BLF", "JissekiSoushin", ds)

                    If MyBase.IsErrorMessageExist Then
                        MyBase.ShowMessage(frm)
                    Else
                        'メッセージエリアの設定
                        MyBase.ShowMessage(frm, "G002", New String() {"実績送信処理", String.Concat(MyBase.GetResultCount.ToString(), "件")})
                    End If
                    '実績送信成功時処理を行う
                    Call Me.EndAction(frm)

                Else '「はい」を選択

                    '終了処理を行う
                    Call Me.EndAction(frm)

                End If

            Else
                'メッセージを設定
                MyBase.ShowMessage(frm)

                '実績送信失敗時処理を行う
                Call Me.EndAction(frm)

            End If

        Else

            '実績送信データ取得
            ds = MyBase.CallWSA("LMN010BLF", "JissekiSoushin", ds)

            If MyBase.IsErrorMessageExist Then
                MyBase.ShowMessage(frm)
            Else
                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G002", New String() {"実績送信処理", String.Concat(MyBase.GetResultCount.ToString(), "件")})
            End If
            '実績送信成功時処理を行う
            Call Me.EndAction(frm)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiSoushin")

        Call Me.EndAction(frm) '終了処理

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 欠品照会処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub KeppinShoukaiEvent(ByVal frm As LMN010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN010C.EventShubetsu.KEPPIN_SHOUKAI) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック()
        If Me._V.IsKeppinShoukaiInputChk = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'パラメータ生成
        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMN080DS()
        Dim prmDt As DataTable = prmDs.Tables("LMN080IN")
        Dim prmDr As DataRow = prmDt.NewRow

        prmDr.Item("SCM_CUST_CD") = frm.cmbCustCd.SelectedValue.ToString

        prmDt.Rows.Add(prmDr)
        prm.ParamDataSet = prmDs

        '欠品照会(LMN080)を開く
        LMFormNavigate.NextFormNavigate(Me, "LMN080", prm)

        Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 欠品状態更新処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub UpdKeppinJoutaiEvent(ByVal frm As LMN010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMN010C.EventShubetsu.KEPPIN_JOUTAI_UPD) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsUpdKeppinJoutaiInputChk() = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "欠品状態更新処理") = False Then
            '設定されているメッセージのクリアー
            Call Me.ClearMessageAria(frm)
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMN010DS()
        Call Me.SetDataSetInData_UpdKeppinJoutai(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdKeppinJoutai")

        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMN010BLF", "UpdKeppinJoutai", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist = True Then
            '返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '欠品フラグをSpreadに設定
        Call Me.SuccessUpdKeppinJoutai(frm, ds)

        '終了メッセージ表示
        MyBase.ShowMessage(frm, "G002", New String() {"欠品状態更新処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdKeppinJoutai")

        '終了処理
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMN010F)

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
    Private Sub EndAction(ByVal frm As LMN010F)

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
    Private Sub SelectData(ByVal frm As LMN010F)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        MyBase.SetLimitCount(LMN010C.LIMITED_COUNT)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)


        'DataSet設定
        Dim ds As DataSet = New LMN010DS()
        Call Me.SetDataSetInData_Kensaku(frm, ds)
        '検索条件ステータスが「倉庫指示済」、「実績報告済」の場合、LMS接続情報を取得(実出荷日、実納入日取得用)
        Dim statusKbn As String = frm.cmbStatus.SelectedValue.ToString()
        If statusKbn = LMN010C.KbnCdSokoShijiZumi Or statusKbn = LMN010C.KbnCdJissekiSoushinZumi Then
            Call Me.SetDataSetInData_Jisseki(frm, ds)
        End If

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMN010BLF", "SelectListData", ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then     'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA("LMN010BLF", "SelectListData", ds)

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
    Private Sub SuccessSelect(ByVal frm As LMN010F, ByVal ds As DataSet)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(ds)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G008", New String() {(frm.sprDetail.ActiveSheet.RowCount - 1).ToString()})

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal frm As LMN010F)

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
    Private Function ConfirmMsg(ByVal frm As LMN010F, ByVal msg As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 欠品フラグ取得成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessUpdKeppinJoutai(ByVal frm As LMN010F, ByVal ds As DataSet)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '欠品フラグをSPREADに表示
        Call Me._G.SetSpreadKeppinFlg(ds)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

    End Sub

#End Region '内部メソッド

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">検索条件を格納するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_Kensaku(ByVal frm As LMN010F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMN010C.TABLE_NM_IN).NewRow()

        dr("HED_STATUS_KBN") = frm.cmbStatus.SelectedValue.ToString
        dr("HED_SCM_CUST_CD") = frm.cmbCustCd.SelectedValue.ToString
        dr("HED_EDI_DATE_FROM") = frm.imdEDITorikomiDate_From.TextValue
        dr("HED_EDI_DATE_TO") = frm.imdEDITorikomiDate_To.TextValue
        dr("HED_OUTKA_DATE_FROM") = frm.imdShukkaDate_From.TextValue
        dr("HED_OUTKA_DATE_TO") = frm.imdShukkaDate_To.TextValue
        dr("HED_ARR_DATE_FROM") = frm.imdNounyuDate_From.TextValue
        dr("HED_ARR_DATE_TO") = frm.imdNounyuDate_To.TextValue

        With frm.sprDetail.ActiveSheet

            dr("SOKO_CD") = .Cells(0, LMN010G.sprDetailDef.SOKO_NM.ColNo).Value
            dr("SCM_CUST_CD") = .Cells(0, LMN010G.sprDetailDef.SCM_CUST_CD.ColNo).Value
            dr("CUST_ORD_NO_L") = .Cells(0, LMN010G.sprDetailDef.CUST_ORD_NO_L.ColNo).Value
            dr("MOUSHIOKURI_KBN") = .Cells(0, LMN010G.sprDetailDef.MOUSHIOKURI_KBN.ColNo).Value
            dr("INSERT_FLG") = .Cells(0, LMN010G.sprDetailDef.INSERT_FLG.ColNo).Value
            dr("DEST_NM") = .Cells(0, LMN010G.sprDetailDef.DEST_NM.ColNo).Value
            dr("DEST_AD") = .Cells(0, LMN010G.sprDetailDef.DEST_AD.ColNo).Value
            dr("DEST_ZIP") = .Cells(0, LMN010G.sprDetailDef.DEST_ZIP.ColNo).Value
            dr("REMARK1") = .Cells(0, LMN010G.sprDetailDef.REMARK_1.ColNo).Value
            dr("REMARK2") = .Cells(0, LMN010G.sprDetailDef.REMARK_2.ColNo).Value

        End With

        ds.Tables(LMN010C.TABLE_NM_IN).Rows.Add(dr)
    End Sub

    ''' <summary>
    ''' データセット設定(設定項目格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">設定項目を格納するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_Settei(ByVal frm As LMN010F, ByVal ds As DataSet, ByVal list As ArrayList)

        Dim defNo As Integer

        'listに格納されているインデックスのSpred行をデータセットに格納する
        For Each defNo In list

            Dim dr As DataRow = ds.Tables(LMN010C.TABLE_NM_IN).NewRow()

            dr("HED_SCM_CUST_CD") = frm.cmbCustCd.SelectedValue.ToString
            dr("HED_SOKO_CD") = frm.cmbWare.SelectedValue.ToString
            dr("HED_OUTKA_DATE") = frm.imdShukkaDate.TextValue.ToString
            dr("HED_ARR_DATE") = frm.imdNounyuDate.TextValue.ToString

            With frm.sprDetail.ActiveSheet

                dr("SCM_CTL_NO_L") = .Cells(defNo, LMN010G.sprDetailDef.SCM_CTL_NO_L.ColNo).Value
                dr("SOKO_CD") = .Cells(defNo, LMN010G.sprDetailDef.SOKO_NM.ColNo).Value
                dr("OUTKA_DATE") = .Cells(defNo, LMN010G.sprDetailDef.OUTKA_DATE.ColNo).Value
                dr("ARR_DATE") = .Cells(defNo, LMN010G.sprDetailDef.ARR_DATE.ColNo).Value
                dr("INSERT_FLG") = .Cells(defNo, LMN010G.sprDetailDef.INSERT_FLG.ColNo).Value
                dr("CRT_DATE") = .Cells(defNo, LMN010G.sprDetailDef.CRT_DATE.ColNo).Value
                dr("FILE_NAME") = .Cells(defNo, LMN010G.sprDetailDef.FILE_NAME.ColNo).Value
                dr("REC_NO") = .Cells(defNo, LMN010G.sprDetailDef.REC_NO.ColNo).Value
                dr("HED_BP_SYS_UPD_DATE") = .Cells(defNo, LMN010G.sprDetailDef.HED_BP_SYS_UPD_DATE.ColNo).Value
                dr("HED_BP_SYS_UPD_TIME") = .Cells(defNo, LMN010G.sprDetailDef.HED_BP_SYS_UPD_TIME.ColNo).Value
                dr("L_SYS_UPD_DATE") = .Cells(defNo, LMN010G.sprDetailDef.L_SYS_UPD_DATE.ColNo).Value
                dr("L_SYS_UPD_TIME") = .Cells(defNo, LMN010G.sprDetailDef.L_SYS_UPD_TIME.ColNo).Value

            End With

            ds.Tables(LMN010C.TABLE_NM_IN).Rows.Add(dr)

        Next

    End Sub

    ''' <summary>
    ''' データセット設定(実績取込／送信)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">検索条件を格納するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_Jisseki(ByVal frm As LMN010F, ByVal ds As DataSet)

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

    ''' <summary>
    ''' データセット設定（送信指示）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_SoushinShiji(ByVal frm As LMN010F, ByVal ds As DataSet, ByVal list As ArrayList)

        Dim defNo As Integer

        Dim tempDs As New LMN010DS

        'listに格納されているインデックスのSpred行をデータセットに格納する
        For Each defNo In list

            Dim dr As DataRow = ds.Tables(LMN010C.TABLE_NM_IN).NewRow()
            Dim tempDr As LMN010DS.BR_CD_LISTRow = tempDs.BR_CD_LIST.NewBR_CD_LISTRow
            With frm.sprDetail.ActiveSheet

                dr("SCM_CTL_NO_L") = .Cells(defNo, LMN010G.sprDetailDef.SCM_CTL_NO_L.ColNo).Value
                dr("BR_CD") = .Cells(defNo, LMN010G.sprDetailDef.BR_CD.ColNo).Value
                dr("CRT_DATE") = .Cells(defNo, LMN010G.sprDetailDef.CRT_DATE.ColNo).Value
                dr("FILE_NAME") = .Cells(defNo, LMN010G.sprDetailDef.FILE_NAME.ColNo).Value
                dr("REC_NO") = .Cells(defNo, LMN010G.sprDetailDef.REC_NO.ColNo).Value
                dr("L_SYS_UPD_DATE") = .Cells(defNo, LMN010G.sprDetailDef.L_SYS_UPD_DATE.ColNo).Value
                dr("L_SYS_UPD_TIME") = .Cells(defNo, LMN010G.sprDetailDef.L_SYS_UPD_TIME.ColNo).Value

                tempDr.BR_CD = .Cells(defNo, LMN010G.sprDetailDef.BR_CD.ColNo).Value.ToString()
                tempDr.SCM_CUST_CD = .Cells(defNo, LMN010G.sprDetailDef.SCM_CUST_CD.ColNo).Value.ToString()
            End With

            ds.Tables(LMN010C.TABLE_NM_IN).Rows.Add(dr)
            tempDs.BR_CD_LIST.AddBR_CD_LISTRow(tempDr)

        Next

        Dim sokoDr As DataRow
        Dim tempDrs As LMN010DS.BR_CD_LISTRow() = DirectCast(tempDs.BR_CD_LIST.Select("", "BR_CD ASC , SCM_CUST_CD ASC"), LMN010DS.BR_CD_LISTRow())
        Dim preBR_CD As String = String.Empty
        Dim preSCM_CUST_CD As String = String.Empty
        For Each dr As LMN010DS.BR_CD_LISTRow In tempDrs
            If preBR_CD.Equals(dr.BR_CD) = False OrElse preSCM_CUST_CD.Equals(dr.SCM_CUST_CD) = False Then
                sokoDr = ds.Tables("BR_CD_LIST").NewRow()
                sokoDr("BR_CD") = dr.BR_CD
                sokoDr("SCM_CUST_CD") = dr.SCM_CUST_CD
                ds.Tables("BR_CD_LIST").Rows.Add(sokoDr)

                preBR_CD = dr.BR_CD
                preSCM_CUST_CD = dr.SCM_CUST_CD
            End If
        Next


    End Sub

    ''' <summary>
    ''' データセット設定(欠品状態更新)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">検索条件を格納するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_UpdKeppinJoutai(ByVal frm As LMN010F, ByVal ds As DataSet)

        '************************* 営業所毎接続情報取得 *****************************************************************

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

        '************************* Spreadレコード情報取得 *****************************************************************
        'Spread行表示数取得
        Dim count As Integer = frm.sprDetail.ActiveSheet.RowCount
        'ステータスが「設定済」のレコード情報を取得
        For i As Integer = 1 To count - 1

            If frm.sprDetail.ActiveSheet.Cells(i, LMN010G.sprDetailDef.STATUS_KBN.ColNo).Value.ToString() = "01" Then

                Dim sprDr As DataRow = ds.Tables(LMN010C.TABLE_NM_IN).NewRow()

                With frm.sprDetail.ActiveSheet

                    sprDr("SCM_CTL_NO_L") = .Cells(i, LMN010G.sprDetailDef.SCM_CTL_NO_L.ColNo).Value
                    sprDr("SCM_CUST_CD") = .Cells(i, LMN010G.sprDetailDef.SCM_CUST_CD.ColNo).Value
                    sprDr("BR_CD") = .Cells(i, LMN010G.sprDetailDef.BR_CD.ColNo).Value
                    sprDr("SOKO_CD") = .Cells(i, LMN010G.sprDetailDef.SOKO_NM.ColNo).Value
                    sprDr("OUTKA_DATE") = .Cells(i, LMN010G.sprDetailDef.OUTKA_DATE.ColNo).Value

                End With

                ds.Tables(LMN010C.TABLE_NM_IN).Rows.Add(sprDr)

            End If

        Next

    End Sub

#End Region 'DataSet設定

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SoushinShijiEvent")

        Me.SoushinShijiEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SoushinShijiEvent")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "KeppinShoukaiEvent")

        Me.KeppinShoukaiEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "KeppinShoukaiEvent")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "UpdKeppinJoutai")

        Me.UpdKeppinJoutaiEvent(frm)

        Logger.EndLog(Me.GetType.Name, "UpdKeppinJoutai")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMN010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMN010F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMN010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        If e.Row <> 0 Then

            Logger.StartLog(Me.GetType.Name, "RowSelection")

            '該当データの詳細画面に遷移
            Me.SelectListData(frm, e)

            Logger.EndLog(Me.GetType.Name, "RowSelection")

        End If

    End Sub

    ''' <summary>
    ''' 設定ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub ClickBtnSettei(ByRef frm As LMN010F, ByVal e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SetteiEvent")

        '設定処理
        Me.SetteiEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SetteiEvent")

    End Sub

    ''' <summary>
    ''' 実績取込/実績送信の実行ボタン押下時処理呼び出し(実行処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub JissekiAction(ByRef frm As LMN010F, ByVal e As EventArgs)

        '実績取込処理
        If frm.cmbJissekiAction.SelectedValue.ToString = "00" Then

            MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiTorikomiEvent")

            Me.JissekiTorikomiEvent(frm)

            MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiTorikomiEvent")

        End If

        '実績送信処理
        If frm.cmbJissekiAction.SelectedValue.ToString = "01" Then

            MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiSoushin")

            Me.JissekiSoushinEvent(frm)

            MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiSoushin")

        End If

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class