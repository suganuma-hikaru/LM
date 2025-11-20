' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMHI     : 特定荷主機能
'  プログラムID     :  LMI550H : TSMC在庫照会
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI550ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI550H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI550V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI550G

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
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 初期表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiFlg As Boolean

    ''' <summary>
    ''' 処理モード
    ''' </summary>
    ''' <remarks></remarks>
    Private _Mode As Integer = LMI550C.Mode.INT

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        Me._ShokiFlg = True

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        ' 画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        ' フォームの作成
        Dim frm As LMI550F = New LMI550F(Me)

        ' キーイベントをフォームで受け取る
        frm.KeyPreview = True

        ' Hnadler共通クラスの設定
        Me._LMIconH = New LMIControlH(DirectCast(frm, Form), MyBase.GetPGID())

        ' Validateクラスの設定
        Me._V = New LMI550V(Me, frm)

        ' Gamenクラスの設定
        Me._G = New LMI550G(Me, frm)

        ' G共通クラスの設定
        Me._LMIconG = New LMIControlG(frm)

        ' 処理モードの設定
        Me._Mode = LMI550C.Mode.INT

        ' フォームの初期化
        Call MyBase.InitControl(frm)

        ' 営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbWare)

        ' ファンクションキーの設定
        Call Me._G.SetFunctionKey(Me._Mode)

        ' タブインデックスの設定
        Call Me._G.SetTabIndex()

        ' コントロール個別設定
        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetControl(MyBase.GetPGID(), frm, sysdate(0))

        ' スプレッドの初期設定
        Call Me._G.InitSpread()

        ' メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        ' 画面の入力項目の制御
        Call Me._G.SetControlsStatus(Me._Mode)

        ' フォーカスの設定(ヘッダー部の先頭)
        Call Me._G.SetFocusHeader()

        ' フォームの表示
        frm.Show()

        ' カーソルを元に戻す
        Cursor.Current = Cursors.Default

        ' Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        ' Gamen共通クラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        ' 初期表示フラグの設定
        Me._ShokiFlg = False

    End Sub

#End Region

#Region "イベントコントロール"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI550C.EventShubetsu, ByVal frm As LMI550F)

        ' 画面初期化処理中は抜ける
        If Me._ShokiFlg Then
            Exit Sub
        End If

        ' 権限チェック
        If Not Me._V.IsAuthorityChk(eventShubetsu) Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        ' 処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        ' イベント種別による分岐
        Select Case eventShubetsu
            Case LMI550C.EventShubetsu.SEARCH
                ' 検索

                ' 入力チェック（単項目チェック）
                If Not Me._V.IsSearchSingleCheck(Me._G) Then
                    Call EndAction2(frm)
                    Exit Sub
                End If

                ' 入力チェック（関連チェック）
                If Not Me._V.IsSearchKanrenCheck() Then
                    Call EndAction2(frm)
                    Exit Sub
                End If

                If Me._Mode = LMI550C.Mode.EDT Then
                    If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                        Call EndAction2(frm, "G003")
                        Exit Sub
                    End If
                End If

                ' 検索処理
                Call Me.Search(frm)

        End Select

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMI550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        ' 編集アクション
        EditAction(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        ' 検索処理
        Call ActionControl(LMI550C.EventShubetsu.SEARCH, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        ' 保存アクション
        SaveAction(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        ' 終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' Enter 押下
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    Friend Sub FormKeyDownEnter(ByRef frm As LMI550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Dim controlNm As String = String.Empty

        controlNm = frm.FocusedControlName()

        '　荷主コード入力コントロールでEnterキーを押下時
        If (frm.FocusedControlName() = "txtCustCdL" OrElse frm.FocusedControlName() = "txtCustCdM") Then

            Dim custCdL As String = ""
            Dim custCdM As String = ""
            If String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = False Then
                custCdL = frm.txtCustCdL.TextValue
            End If
            If String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = False Then
                custCdM = frm.txtCustCdM.TextValue
            End If

            ' 荷主コード/荷主名 (大/中) 初期値設定
            Me._G.SetInitControlCust(frm, custCdL, custCdM)

            ' Tabキーが押された時と同じ動作をする。
            frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
        End If

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI550F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        ' 終了処理  
        Call Me.CloseFormEvent(frm, e)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' SPREADのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMI550F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

#End Region

#Region "共通処理"

    ''' <summary>
    ''' 編集アクション
    ''' </summary>
    ''' <param name="frm"></param>
    Private Sub EditAction(ByVal frm As LMI550F)

        Dim i As Integer

        ' 処理モードの設定
        Me._Mode = LMI550C.Mode.EDT

        ' ファンクションキーの設定
        Call Me._G.SetFunctionKey(Me._Mode)

        For i = 1 To frm.sprDetail.ActiveSheet.Rows.Count - 1
            Call _G.SetSpreadEdit(Me._Mode, i, False)
        Next

        Call MyBase.ShowMessage(frm, "G003")

    End Sub

    ''' <summary>
    ''' 保存アクション
    ''' </summary>
    ''' <param name="frm"></param>
    Private Function SaveAction(ByVal frm As LMI550F) As Boolean

        Dim ds As DataSet = New LMI550DS()
        Dim dr As DataRow

        Dim stockTypeMae As String
        Dim stockTypeAto As String

        Dim i As Integer
        Dim newDateTimeDict As Dictionary(Of String, Integer)
        Dim tsmcRecNo As String

        ' 「編集」と「保存」ボタンは権限のあるユーザーのみ有効となるため権限チェックは行わない。

        For row As Integer = 1 To frm.sprDetail.ActiveSheet.Rows.Count - 1

            stockTypeMae = Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(row, _G.sprDetailDef.STOCK_TYPE.ColNo)).Trim()
            stockTypeAto = Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(row, _G.sprDetailDef.STOCK_TYPE_NM.ColNo)).Trim()
            If stockTypeMae = stockTypeAto Then
                Continue For
            End If

            dr = ds.Tables(LMI550C.TABLE_NM.IN_SEARCH).NewRow()

            ' 営業所コード
            dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue()
            ' TSMC在庫番号
            dr.Item("TSMC_REC_NO") = Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(row, _G.sprDetailDef.TSMC_REC_NO.ColNo)).Trim()
            ' 検査状態
            dr.Item("STOCK_TYPE_NM") = Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(row, _G.sprDetailDef.STOCK_TYPE_NM.ColNo)).Trim()
            ' 更新日
            dr.Item("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(row, _G.sprDetailDef.SYS_UPD_DATE.ColNo)).Trim()
            ' 更新時刻
            dr.Item("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(row, _G.sprDetailDef.SYS_UPD_TIME.ColNo)).Trim()

            ds.Tables(LMI550C.TABLE_NM.IN_SEARCH).Rows.Add(dr)
        Next

        If ds.Tables(LMI550C.TABLE_NM.IN_SEARCH).Rows.Count() = 0 Then
            MyBase.ShowMessage(frm, "G033", New String() {"変更行が"})
            Return True
        End If

        ' 処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        ds = MyBase.CallWSA("LMI550BLF", "UpdateZaiTsmc", ds)

        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            ' 処理終了アクション
            Call Me._LMIconH.EndAction(frm)
            Return False
        End If

        ' 更新後に再取得したタイムスタンプ項目の設定
        '   DataTable → Dictionary(Key: TSMC在庫番号, Value: DataTable の INDEX)
        newDateTimeDict = New Dictionary(Of String, Integer)
        For i = 0 To ds.Tables(LMI550C.TABLE_NM.OUT).Rows.Count() - 1
            dr = ds.Tables(LMI550C.TABLE_NM.OUT).Rows(i)
            newDateTimeDict(dr.Item("TSMC_REC_NO").ToString()) = i
        Next
        For row As Integer = 1 To frm.sprDetail.ActiveSheet.Rows.Count - 1
            tsmcRecNo = Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(row, _G.sprDetailDef.TSMC_REC_NO.ColNo)).Trim()
            If Not newDateTimeDict.Keys.Contains(tsmcRecNo) Then
                Continue For
            End If
            ' 画面明細の TSMC在庫番号 Key に持つ Dictionary が存在する場合
            ' Dictionary の Value(DataTable の INDEX) で 更新後に再取得したタイムスタンプ項目の DataRow を取り出し
            ' 画面明細のタイムスタンプ項目(非表示項目)に(再)設定する
            i = newDateTimeDict(tsmcRecNo)
            dr = ds.Tables(LMI550C.TABLE_NM.OUT).Rows(i)
            frm.sprDetail.SetCellValue(row, _G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
            frm.sprDetail.SetCellValue(row, _G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())

            ' 画面明細の検査状態(コード)(非表示項目)には、検査状態の更新値(表示項目)を設定する
            stockTypeAto = Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(row, _G.sprDetailDef.STOCK_TYPE_NM.ColNo)).Trim()
            frm.sprDetail.SetCellValue(row, _G.sprDetailDef.STOCK_TYPE.ColNo, stockTypeAto)
        Next

        ' 処理終了アクション
        Call EndAction2(frm)

        Dim svRow As Integer = frm.sprDetail.ActiveSheet.ActiveRowIndex
        Dim svCol As Integer = frm.sprDetail.ActiveSheet.ActiveColumnIndex
        frm.sprDetail.ActiveSheet.SetActiveCell(0, svCol)

        ' 処理モードの設定
        Me._Mode = LMI550C.Mode.REF

        ' ファンクションキーの設定
        Call Me._G.SetFunctionKey(Me._Mode)

        For i = 1 To frm.sprDetail.ActiveSheet.Rows.Count - 1
            Call _G.SetSpreadEdit(Me._Mode, i, True)
        Next

        frm.sprDetail.ActiveSheet.SetActiveCell(svRow, svCol)

        Return True

    End Function

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="msgClear"></param>
    ''' <remarks>LMIControlHのEndAction後にメッセージクリアロジックを付加</remarks>
    Private Sub EndAction2(ByVal frm As Form, Optional ByVal id As String = "G007", Optional ByVal msgClear As Boolean = False)

        ' 処理終了アクション
        Call Me._LMIconH.EndAction(frm, id)

        ' メッセージをクリア
        If msgClear Then
            MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))
        End If

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal frm As LMI550F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If Me._Mode <> LMI550C.Mode.EDT Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes
                '「はい」押下時

                If Not Me.SaveAction(frm) Then

                    e.Cancel = True

                End If


            Case MsgBoxResult.Cancel
                '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

#End Region ' "共通処理"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="previewFlg">True:印刷プレビューを表示中</param>
    ''' <remarks></remarks>
    Private Sub Search(ByVal frm As LMI550F, Optional ByVal previewFlg As Boolean = False)

        ' 強制実行フラグをオフ
        MyBase.SetForceOparation(False)

        ' スプレッドシート初期化
        frm.sprDetail.CrearSpread()

        ' 検索条件の DataSet への設定
        Dim rtDs As DataSet = New LMI550DS()
        If Not Me.SearchSetDataIn(frm, rtDs) Then
            MyBase.ShowMessage(frm, "E361")
            Me._LMIconV.SetErrorControl(frm.cmbEigyo)
            Exit Sub
        End If

        ' WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Search")
        Dim rtnDs As DataSet = Me._LMIconH.CallWSAAction(
                DirectCast(frm, Form),
                "LMI550BLF",
                "Search",
                rtDs,
                Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))),
                Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))
        MyBase.Logger.EndLog(MyBase.GetType.Name, "Search")

        ' 検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then
            Call Me.SuccessSelect(frm, rtnDs)
        End If

        ' 処理モードの設定
        Me._Mode = LMI550C.Mode.REF

        ' ロックを解除する
        Call Me._G.UnLockedForm(Me._Mode)

        ' フォーカスの設定
        If Not previewFlg Then
            '印刷プレビュー表示中以外
            Call Me._G.SetFocusDetail()
        End If

        ' 処理終了アクション
        Call EndAction2(frm)

        ' 終了処理アクション後の画面制御
        Call _G.SetControlsStatus(Me._Mode)
        For i As Integer = 1 To frm.sprDetail.ActiveSheet.Rows.Count - 1
            Call _G.SetSpreadEdit(LMI550C.Mode.REF, i, True)
        Next

    End Sub

    ''' <summary>
    ''' 検索処理：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SearchSetDataIn(ByVal frm As LMI550F, ByRef rtDs As DataSet) As Boolean

        Dim dr As DataRow = rtDs.Tables(LMI550C.TABLE_NM.IN_SEARCH).NewRow()

        'ヘッダー部
        With frm

            ' 営業所コード
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue

            ' 倉庫コード
            dr("WH_CD") = .cmbWare.SelectedValue

            ' 日付種類
            dr("SEARCH_DATE_KBN") = .cmbSearchDate.SelectedValue
            ' 日付FROM
            dr("SEARCH_DATE_FROM") = .imdSearchDateFrom.TextValue
            ' 日付TO
            dr("SEARCH_DATE_TO") = .imdSearchDateTo.TextValue

            ' 未請求
            dr("CHK_MISEIKYU") = If(.chkMiSeikyu.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)

            ' 荷主コード(大)
            dr("CUST_CD_L") = .txtCustCdL.TextValue.Trim()
            ' 荷主コード(中)
            dr("CUST_CD_M") = .txtCustCdM.TextValue.Trim()

            ' 在庫
            dr("CHK_ZAI") = If(.chkZai.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
            ' 出荷
            dr("CHK_OUTKA") = If(.chkOutKa.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
            ' 空在庫
            dr("CHK_RE_ZAI") = If(.chkReZai.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
            ' 空出荷
            dr("CHK_RE_OUTKA") = If(.chkReOutKa.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
            ' 返品出荷
            dr("CHK_HENPIN_OUTKA") = If(.chkHenpinOutKa.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)

            ' 経過日数FROM
            dr("NUM_KEIKA_FROM") = .numKeikaFrom.TextValue
            ' 経過日数TO
            dr("NUM_KEIKA_TO") = .numKeikaTo.TextValue

        End With

        'スプレッドシート
        With frm.sprDetail.ActiveSheet

            ' 商品コード
            dr("CUST_GOODS_CD") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.CUST_GOODS_CD.ColNo)).Trim()
            ' 商品名
            dr("GOODS_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.GOODS_NM.ColNo)).Trim()
            ' 荷姿
            dr("LVL1_UT") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.LVL1_UT.ColNo)).Trim()
            ' ロット№
            dr("LOT_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.LOT_NO.ColNo)).Trim()
            ' 最新区分
            dr("UP_FLG_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.UP_FLG_NM.ColNo)).Trim()
            ' 回収区分
            dr("RETURN_FLAG_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.RETURN_FLAG_NM.ColNo)).Trim()
            ' サプライヤーコード
            dr("SUPPLY_CD") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.SUPPLY_CD.ColNo)).Trim()
            ' ASN No.
            dr("ASN_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.ASN_NO.ColNo)).Trim()
            ' TSMC在庫番号
            dr("TSMC_REC_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.TSMC_REC_NO.ColNo)).Trim()
            ' 個品ラベル
            dr("GRLVL1_PPNID") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.GRLVL1_PPNID.ColNo)).Trim()
            ' パレットNo.
            dr("PLT_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.PLT_NO.ColNo)).Trim()
            ' 出庫パレットNo.
            dr("DEPLT_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.DEPLT_NO.ColNo)).Trim()
            ' シリアルNo.
            dr("LV2_SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.LV2_SERIAL_NO.ColNo)).Trim()
            ' 容器番号
            dr("CYLINDER_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.CYLINDER_NO.ColNo)).Trim()
            ' 検査状態
            dr("STOCK_TYPE_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.STOCK_TYPE_NM.ColNo)).Trim()
            ' 検査番号1
            dr("LVL1_CHECK") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.LVL1_CHECK.ColNo)).Trim()
            ' 検査番号2
            dr("LVL2_CHECK") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.LVL2_CHECK.ColNo)).Trim()
            ' 入荷送信実績
            dr("JISSEKI_SHORI_FLG_IN") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.JISSEKI_SHORI_FLG_IN_NM.ColNo)).Trim()
            ' 出荷送信実績
            dr("JISSEKI_SHORI_FLG_OUT") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.JISSEKI_SHORI_FLG_OUT_NM.ColNo)).Trim()
            ' 棟番号
            dr("TOU_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.TOU_NO.ColNo)).Trim()
            ' 室番号
            dr("SITU_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.SITU_NO.ColNo)).Trim()
            ' 棟室名
            dr("TOU_SITU_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.TOU_SITU_NM.ColNo)).Trim()
            ' ZONEコード
            dr("ZONE_CD") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.ZONE_CD.ColNo)).Trim()
            ' ZONE名称
            dr("ZONE_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.ZONE_NM.ColNo)).Trim()
            ' ロケーション
            dr("LOCA") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.LOCA.ColNo)).Trim()

        End With

        rtDs.Tables(LMI550C.TABLE_NM.IN_SEARCH).Rows.Add(dr)

        Return True

    End Function

#End Region ' "検索処理"

#Region "画面データ取得成功時"

    ''' <summary>
    ''' 画面データ取得成功時
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">取得結果DataSet</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMI550F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMI550C.TABLE_NM.OUT)

        ' 画面解除
        Call MyBase.UnLockedControls(frm)

        ' スプレッドに取得データをセット
        Call Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        ' データテーブルのカウントを設定
        Dim cnt As Integer = dt.Rows.Count()

        ' カウントが0件以上の時メッセージの上書き
        If cnt > 0 Then
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})
        End If

    End Sub

#End Region

#End Region 'Method

End Class