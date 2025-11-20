' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI570H : TSMC請求データ検索
'  作  成  者       :  [HORI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI570ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI570H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI570V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI570G

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConV As LMIControlV

    '''' <summary>
    '''' Handler共通クラスを格納するフィールド
    '''' </summary>
    '''' <remarks></remarks>
    Private _LMIConH As LMIControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

    '画面間データを取得する
    Dim prmDs As DataSet

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
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        prmDs = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMI570F = New LMI570F(Me)

        '画面共通クラスの設定
        Me._LMIConG = New LMIControlG(DirectCast(frm, Form))

        'Validateクラスの設定
        Me._LMIConV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI570V(Me, frm, Me._LMIConV)

        'Gamenクラスの設定
        Me._G = New LMI570G(Me, frm, Me._LMIConG)

        'ハンドラー共通クラスの設定
        Me._LMIConH = New LMIControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'EnterKey判定用
        frm.KeyPreview = True

        'フォームの初期化
        Call Me.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID(), MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        Me._G.SetInitValue(frm)

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMI570F)

        '権限・入力チェック
        If Me.IsCheckCall(frm, LMI570C.EventShubetsu.KENSAKU) = False Then
            Exit Sub
        End If

        'スタート処理
        Call Me._LMIConH.StartAction(frm)

        '画面項目全ロック
        MyBase.LockedControls(frm)

        'DataSet設定
        Dim ds As DataSet = New DataSet
        Call Me.SetSearchData(frm)

        'WSA呼出し
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(
            DirectCast(frm, Form),
            "LMI570BLF",
            "SelectListData",
            prmDs,
            Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))),
            1000
            )

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then
            'データテーブルの取得
            Dim dt As DataTable = rtnDs.Tables(LMI570C.TABLE_NM_OUT)
            If "0".Equals(dt.Rows.Count.ToString()) = False Then

                'スプレッドデータをクリアする
                frm.sprMeisai.CrearSpread()

                '取得データをスプレッドに反映
                Call Me._G.SetSelectListData(rtnDs)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G008", New String() {dt.Rows.Count.ToString()})
            Else
                'スプレッドデータをクリアする
                frm.sprMeisai.CrearSpread()
            End If
        End If

        '画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMIConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMI570F, ByVal eventShubetsu As LMI570C.EventShubetsu)

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.MasterSelect(frm, eventShubetsu)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterSelect(ByVal frm As LMI570F, ByVal eventShubetsu As LMI570C.EventShubetsu)

        'スタートアクション
        Call Me._LMIConH.StartAction(frm)

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        '現在フォーカスのコントロール名がNullまたはブランクの場合
        If String.IsNullOrEmpty(objNm) = True Then
            'メッセージの設定
            MyBase.ShowMessage(frm, "G005")
            '全画面ロック解除
            Call MyBase.UnLockedControls(frm)
            '終了アクション
            Call Me._LMIConH.EndAction(frm)
            Exit Sub
        End If
        With frm
            '参照PopUpの判定処理
            Select Case objNm
                Case .txtCustCdL.Name, .txtCustCdM.Name _
                   , .txtCustCdS.Name, .txtCustCdSs.Name                      '荷主コード（L・M・S・Ss）の場合

                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtCustCdM.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtCustCdS.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtCustCdSs.TextValue) = True Then

                        '荷主名称のクリア
                        .lblCustNm.TextValue = String.Empty

                    End If

                    '荷主マスタ
                    Call Me.ShowCustPopup(frm, objNm, prm, eventShubetsu)

                Case .txtSekySaki.Name                                        '請求先コードの場合

                    If String.IsNullOrEmpty(.txtSekySaki.TextValue) = True Then

                        '請求先名称のクリア
                        .lblSeqtoNm.TextValue = String.Empty

                    End If
                    '請求先マスタ
                    Call Me.ShowSekyPopup(frm, objNm, prm, eventShubetsu)

                Case Else
                    '荷主コード(大・中）以外の場合はメッセージ表示
                    MyBase.ShowMessage(frm, "G005")

                    '全画面ロック解除
                    Call MyBase.UnLockedControls(frm)

                    Exit Sub
            End Select
        End With

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了アクション
        Call Me._LMIConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 印刷ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintBtnClick(ByVal frm As LMI570F)

        '権限・入力チェック
        If Me.IsCheckCall(frm, LMI570C.EventShubetsu.PRINT) = False Then
            '終了アクション
            Me._LMIConH.EndAction(frm)
            Exit Sub
        End If

        Me._LMIConH.StartAction(frm)

        'コントロールロック処理
        MyBase.LockedControls(frm)

        'ファイル作成処理
        Me.CreatePrintDataAction(frm)

        MyBase.UnLockedControls(frm)

        '終了アクション
        Me._LMIConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' ファイル作成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CreatePrintDataAction(ByVal frm As LMI570F) As Boolean

        With frm

            'データセットにセット
            Dim ds As DataSet = New LMI580DS()
            Dim dt As DataTable = ds.Tables(LMI580C.TABLE_NM_LMI580SET)
            Dim arr As ArrayList = Me._V.getCheckList()
            For i As Integer = 0 To arr.Count - 1
                Dim dr As DataRow = dt.NewRow
                dr("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString
                dr("JOB_NO") = Me._LMIConV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI570C.SprColumnIndex.JOB_NO))
                dt.Rows.Add(dr)
            Next

            '別プログラムを起動
            Me._LMIConH.FormShow(ds, "LMI580")

            'エラーがある場合、エラーメッセージを設定
            If MyBase.IsMessageExist() = True Then
                MyBase.ShowMessage(frm)
                Return False
            End If

            '完了メッセージを表示
            MyBase.SetMessage("G002", New String() {"印刷", ""})
            Return True

        End With

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI570F) As Boolean

        Return True

    End Function

#End Region

#Region "内部処理"

    ''' <summary>
    ''' 権限・入力チェック
    ''' </summary>
    ''' <param name="SHUBETSU"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCheckCall(ByVal frm As LMI570F, ByVal SHUBETSU As LMI570C.EventShubetsu _
                                 , Optional ByVal Row As Integer = 0) As Boolean

        'フォームの背景色を初期化する
        Me._G.SetBackColor(frm)

        '背景色クリア
        Me._LMIConG.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(SHUBETSU) = False Then
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck(SHUBETSU) = False Then
            Return False
        End If

        'スプレッド入力チェック
        If SHUBETSU.Equals(LMI570C.EventShubetsu.KENSAKU) = True Then
            If Me._V.IsSpreadInputChk() = False Then
                Return False
            End If
        End If

        Dim arr As ArrayList = Me._V.getCheckList()
        '印刷時のスプレッド入力チェック
        If SHUBETSU.Equals(LMI570C.EventShubetsu.PRINT) = True Then
            If Me._V.IsSpreadInputPrintChk(arr) = False Then
                Return False
            End If
        End If

        '関連チェック
        If SHUBETSU.Equals(LMI570C.EventShubetsu.KENSAKU) = True Then
            If Me._V.IsRelationChk(SHUBETSU) = False Then
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNM"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub ShowCustPopup(ByVal frm As LMI570F, ByVal objNM As String, ByRef prm As LMFormData, ByVal eventShubetsu As LMI570C.EventShubetsu)

        Dim prmDs As DataSet = New LMZ260DS()

        'パラメータ生成
        Dim dr As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow()
        dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString
        If eventShubetsu = LMI570C.EventShubetsu.ENTER Then
            dr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            dr.Item("CUST_CD_S") = frm.txtCustCdS.TextValue
            dr.Item("CUST_CD_SS") = frm.txtCustCdSs.TextValue
        End If
        dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        dr.Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs
        prm.SkipFlg = Me._PopupSkipFlg

        '荷主マスタ照会(LMZ260)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

        '戻り処理
        If prm.ReturnFlg = True Then
            With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                'PopUpから取得したデータをコントロールにセット
                frm.txtCustCdL.TextValue = .Item("CUST_CD_L").ToString()    '荷主コード大
                frm.txtCustCdM.TextValue = .Item("CUST_CD_M").ToString()    '荷主コード中
                frm.txtCustCdS.TextValue = .Item("CUST_CD_S").ToString()    '荷主コード大
                frm.txtCustCdSs.TextValue = .Item("CUST_CD_SS").ToString()
                frm.lblCustNm.TextValue = String.Concat(.Item("CUST_NM_L").ToString(), " " _
                                                        , .Item("CUST_NM_M").ToString(), " " _
                                                        , .Item("CUST_NM_S").ToString(), " " _
                                                        , .Item("CUST_NM_SS").ToString(), " ")
                frm.imdInvDate.TextValue = Me._G.SetControlDate(.Item("HOKAN_NIYAKU_CALCULATION").ToString(), 0)
            End With
        End If

    End Sub

    ''' <summary>
    ''' 請求先マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNM"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub ShowSekyPopup(ByVal frm As LMI570F, ByVal objNM As String, ByRef prm As LMFormData, ByVal eventShubetsu As LMI570C.EventShubetsu)

        Dim prmDs As DataSet = New LMZ220DS()

        'パラメータ生成
        Dim dr As DataRow = prmDs.Tables(LMZ220C.TABLE_NM_IN).NewRow()
        dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString
        If eventShubetsu = LMI570C.EventShubetsu.ENTER Then
            dr.Item("SEIQTO_CD") = frm.txtSekySaki.TextValue
            'dr.Item("SEIQTO_NM") = frm.lblSeqtoNm.TextValue
        End If
        dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        prmDs.Tables(LMZ220C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs
        prm.SkipFlg = Me._PopupSkipFlg

        '荷主マスタ照会(LMZ220)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ220", prm)

        '戻り処理
        If prm.ReturnFlg = True Then
            'PopUpから取得したデータをコントロールにセット
            frm.txtSekySaki.TextValue = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0).Item("SEIQTO_CD").ToString()
            frm.lblSeqtoNm.TextValue = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0).Item("SEIQTO_NM").ToString()
        End If

    End Sub

    ''' <summary>
    ''' EnterKey処理判定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <param name="controlNm"></param>
    ''' <remarks></remarks>
    Private Sub EnterkeyControl(ByRef frm As LMI570F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String, ByVal eventShubetsu As LMI570C.EventShubetsu)

        'マスタ検索フラグ
        Dim MasterFlg As Boolean = False

        If e.KeyCode = Keys.Enter Then
            Select Case controlNm
                Case frm.txtCustCdL.Name, frm.txtCustCdM.Name, frm.txtCustCdS.Name, frm.txtCustCdSs.Name    'カーソルが荷主コード(大、中、小、極小）の場合
                    '荷主コード（大）にデータが入力されていない場合
                    If String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = False OrElse
                    String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = False OrElse
                    String.IsNullOrEmpty(frm.txtCustCdS.TextValue) = False OrElse
                    String.IsNullOrEmpty(frm.txtCustCdSs.TextValue) = False Then

                        MasterFlg = True

                    Else

                        '荷主コードがすべてブランクだったら名称のクリア
                        frm.lblCustNm.TextValue = String.Empty
                    End If

                Case frm.txtSekySaki.Name
                    '請求先コードにデータが入力されていない場合
                    If String.IsNullOrEmpty(frm.txtSekySaki.TextValue) = False Then

                        MasterFlg = True
                    Else
                        '請求先コードがブランクの場合名称のクリア
                        frm.lblSeqtoNm.TextValue = String.Empty
                    End If
                Case Else                      'カーソルが荷主コード、請求先コード以外の場合

                    'EnterKeyによるタブ遷移
                    frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
                    Exit Sub
            End Select

        End If

        'Popup参照判定
        Select Case MasterFlg

            Case True                                      'Trueの場合

                'マスタPopUp参照処理：１件時表示なし
                Me._PopupSkipFlg = False
                Call Me.MasterSelect(frm, eventShubetsu)

            Case False

                'EnterKeyによるタブ遷移
                frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
        End Select

    End Sub

#End Region

#Region "DataSet"

    ''' <summary>
    ''' 検索用データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchData(ByVal frm As LMI570F)

        Dim SEKY_DATE As String = String.Empty         '今回請求日
        Dim SHIMEBI As String = String.Empty           '締日
        Dim USER_FLG As String = String.Empty          '担当分のみ表示フラグ
        Dim MATSUJITU As String = "00"                 '締日区分（末日）
        Dim DTFMT As String = "0000/00/00"             '日付フォーマット
        Dim JIKKOUMODE As String = String.Empty        '実行モード

        'データテーブル
        Me.prmDs = New LMI570DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMI570C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力データ取得
        With frm

            '営業所コード
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue.ToString()

            '荷主コード
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue.ToString()
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue.ToString()
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True Then
                dr.Item("CUST_CD_S") = .txtCustCdS.TextValue.ToString()
                dr.Item("CUST_CD_SS") = .txtCustCdSs.TextValue.ToString()
            End If

            '請求先コード
            dr.Item("SEIQTO_CD") = .txtSekySaki.TextValue.ToString()

            '請求月
            If Not String.IsNullOrEmpty(.imdInvDate.TextValue) Then
                dr.Item("INV_DATE") = Left(.imdInvDate.TextValue, 6)
            End If

            'スプレッド入力データ取得
            With .sprMeisai.ActiveSheet
                dr.Item("SEIQTO_NM") = Me._LMIConV.GetCellValue(.Cells(0, LMI570G.sprMeisaiDef.SEIQTO_NM.ColNo))
            End With

        End With

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI570F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI570F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "ShowMaster")

        Me.OpenMasterPop(frm, LMI570C.EventShubetsu.MASTER)

        Logger.EndLog(Me.GetType.Name, "ShowMaster")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI570F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI570F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' Enterキー押下処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <param name="controlNm"></param>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMI570F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String)

        If e.KeyCode = Keys.Enter Then

            Me.EnterkeyControl(frm, e, controlNm, LMI570C.EventShubetsu.ENTER)

        End If
    End Sub

    ''' <summary>
    ''' 印刷ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI570F, ByVal e As System.EventArgs)

        Call Me.PrintBtnClick(frm)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class