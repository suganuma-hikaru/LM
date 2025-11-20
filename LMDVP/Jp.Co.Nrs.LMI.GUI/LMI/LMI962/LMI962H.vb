' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI962H : EDI入荷データ編集
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI962ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI962H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ' 入力チェックで使用するValidateクラスを格納するフィールド
    Private _V As LMI962V

    ' Gamenクラスを格納するフィールド
    Private _G As LMI962G

    ' Validate共通クラスを格納するフィールド
    Private _LMIconV As LMIControlV

    ' Handler共通クラスを格納するフィールド
    Private _LMIconH As LMIControlH

    ' G共通クラスを格納するフィールド
    Private _LMIconG As LMIControlG

    '画面間データの保持
    Private _Ds As DataSet

    'LMZ021表示フラグ
    Private _LMZ021Flg As Boolean

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

        'プライベート変数に格納
        Me._Ds = prmDs

        'フォームの作成
        Dim frm As LMI962F = New LMI962F(Me)

        'Validateクラスの設定
        Me._V = New LMI962V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMI962G(Me, frm)

        Me._LMIconH = New LMIControlH(DirectCast(frm, Form), MyBase.GetPGID)

        Me._LMIconG = New LMIControlG(frm)

        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        'フォームの初期化
        Call Me.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(prmDs)

        'frm値設定
        If Me._G.SetForm(frm, prmDs) = False Then
            Exit Sub
        End If

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G006")

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "内部Method"

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub Touroku(ByVal frm As LMI962F, ByVal ds As DataSet)

        '処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        '項目チェック
        If Me._V.TourokuCheck() = False Then
            Call Me._LMIconH.EndAction(frm, "G006") '終了処理
            Exit Sub
        End If

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"登録"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call Me._LMIconH.EndAction(frm, "G006") '終了処理
            Exit Sub
        End If

        ds = Me.SetDs(frm, ds)

        'ワーニング設定(WARNING_DTL)の初期化
        ds.Tables(LMI962C.TABLE_NM_DTL).Clear()

        '==== WSAクラス呼出 ====
        Dim blcFuncName As String = ""
        Select Case ds.Tables(LMI962C.TABLE_NM_HED).Rows(0).Item("INOUT_KB").ToString
            Case LMI960C.InOutKb.Inka     '入荷登録
                blcFuncName = LMI960C.ACTION_ID_NYUKA_TOUROKU
            Case LMI960C.InOutKb.Outka    '出荷登録
                blcFuncName = LMI960C.ACTION_ID_SHUKKA_TOUROKU
            Case LMI960C.InOutKb.Unso     '運送登録
                blcFuncName = LMI960C.ACTION_ID_UNSO_TOUROKU
        End Select

        Dim rtnDs As DataSet = MyBase.CallWSA("LMI960BLF", blcFuncName, ds)

        Call Me._LMIconH.EndAction(frm, "G006") '終了処理

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            Call Me.InformationClearControl(frm)
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            Select Case ds.Tables(LMI962C.TABLE_NM_HED).Rows(0).Item("INOUT_KB").ToString
                Case LMI960C.InOutKb.Inka     '入荷登録
                    MyBase.ShowMessage(frm, "G002", {"登録", "一部のデータで警告が発生しました。詳細はエクセルを参照してください。"})
                Case LMI960C.InOutKb.Outka, LMI960C.InOutKb.Unso    '出荷登録、運送登録
                    MyBase.ShowMessage(frm, "E235")
            End Select

            'EXCEL起動()
            MyBase.MessageStoreDownload()
        Else
            MyBase.ShowMessage(frm, "G002", New String() {"登録", String.Empty})
        End If

        If rtnDs.Tables(LMI962C.TABLE_NM_DTL).Rows.Count = 0 Then
            'ワーニングがない場合は画面情報をクリア
            Call Me.InformationClearControl(frm)
        Else
            rtnDs.Tables(LMI960C.TABLE_NM_IN).Merge(ds.Tables(LMI960C.TABLE_NM_IN))

            'ワーニングがある場合、画面再設定
            Call Me.InformationSetControl(frm, rtnDs)
        End If

    End Sub

    ''' <summary>
    ''' 登録処理データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDs(ByVal frm As LMI962F, ByVal ds As DataSet) As DataSet

        Dim max As Integer = frm.sprWarning.ActiveSheet.RowCount - 1
        Dim dr As DataRow
        Dim shoriKb As String = String.Empty
        Dim drIn As DataRow()

        With frm.sprWarning.ActiveSheet

            For i As Integer = 0 To max

                shoriKb = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.SHORI.ColNo))

                If Not shoriKb.Equals(LMI962C.SELECT_CANCEL) Then

                    'データセットに設定
                    dr = ds.Tables(LMI962C.TABLE_NM_SHORI).NewRow()
                    dr("CRT_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.CRT_DATE.ColNo))
                    dr("FILE_NAME") = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.FILE_NAME.ColNo))
                    dr("CMD_GYO") = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.CMD_GYO.ColNo))
                    dr("EDI_CTL_NO_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.KANRI_NO_L.ColNo))
                    dr("EDI_CTL_NO_M") = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.KANRI_NO_M.ColNo))
                    dr("EDI_WARNING_ID") = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.EDI_WARNING_ID.ColNo))
                    dr("CHOICE_KB") = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.SHORI.ColNo))
                    dr("MST_VALUE") = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.MASTER_VAL.ColNo))
                    ds.Tables(LMI962C.TABLE_NM_SHORI).Rows.Add(dr)

                Else

                    '処理済みフラグの更新
                    Dim crtDate As String = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.CRT_DATE.ColNo))
                    Dim fileName As String = Me._LMIconV.GetCellValue(.Cells(i, LMI962G.sprWarning.FILE_NAME.ColNo))
                    Dim strFilter As String = String.Concat("HED_CRT_DATE = '", crtDate, "' AND HED_FILE_NAME = '", fileName, "'")
                    drIn = ds.Tables("LMI960SAKUSEI_TARGET").Select(strFilter)
                    drIn(0).Item("SHORIZUMI_FLG") = LMI962C.ShorizumiFlg.Cancel

                End If
            Next

        End With

        Return ds

    End Function

    ''' <summary>
    ''' 画面情報クリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InformationClearControl(ByVal frm As LMI962F)

        frm.sprWarning.CrearSpread()
        Call Me._G.ClearControl()

    End Sub

    ''' <summary>
    ''' 画面情報設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InformationSetControl(ByVal frm As LMI962F, ByVal ds As DataSet)

        'スプレッドの初期設定
        Call Me._G.InitSpread()
        '取得データをSPREADに表示
        Call Me._G.SetSpread(ds)
        '変数に格納
        Me._Ds = ds
        '画面の設定
        Me._G.SetForm(frm, ds)

    End Sub

    ''' <summary>
    ''' 行選択
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowSelection()

        '選択データをデータ部に出力
        Call Me._G.SetDetailData(Me._Ds)

    End Sub

    ''' <summary>
    ''' ダブルクリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>マスタ参照項目の場合、POP呼出</remarks>
    Private Sub DoubleClick(ByVal frm As LMI962F)

        Dim selectRow As Integer = frm.sprWarning.ActiveSheet.ActiveRowIndex()
        'マスタ参照フラグ：ワーニングIDの８文字目
        Dim mstFlg As String = Me._LMIconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMI962G.sprWarning.EDI_WARNING_ID.ColNo)).Substring(LMI962C.WARNING_ID_FMT.MST_FLG.START_IDX, LMI962C.WARNING_ID_FMT.MST_FLG.LEN)
        Dim prm As LMFormData
        Dim dr As DataRow

        Call Me.RowSelection()

        'マスタ参照フラグが"1"の場合、商品マスタ起動
        If mstFlg.Equals(LMI962C.WARNING_ID_FMT.MST_FLG.M_GOODS) Then

            prm = Me.GoodsPop(frm, selectRow)

            If prm.ReturnFlg = True Then
                With frm.sprWarning.ActiveSheet
                    If _LMZ021Flg = True Then
                        dr = prm.ParamDataSet.Tables(LMZ021C.TABLE_NM_OUT).Rows(0)
                    Else
                        dr = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
                    End If
                    .Cells(selectRow, LMI962G.sprWarning.GOODS_NM.ColNo).Value = dr.Item("GOODS_NM_1").ToString() '商品名
                    .Cells(selectRow, LMI962G.sprWarning.MASTER_VAL.ColNo).Value = dr.Item("GOODS_CD_NRS").ToString()
                End With
            End If

            '2012.06.01 ディック届先マスタ対応 追加START
        ElseIf mstFlg.Equals(LMI962C.WARNING_ID_FMT.MST_FLG.M_DEST) = True Then

            prm = Me.DestPop(frm, selectRow)

            If prm.ReturnFlg = True Then
                With frm.sprWarning.ActiveSheet
                    dr = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                    .Cells(selectRow, LMI962G.sprWarning.DEST_NM.ColNo).Value = dr.Item("DEST_NM").ToString()  '商品名
                    .Cells(selectRow, LMI962G.sprWarning.MASTER_VAL.ColNo).Value = dr.Item("DEST_CD").ToString()
                End With
            End If
            '2012.06.01 ディック届先マスタ対応 追加END

        End If

    End Sub

    '2012.06.01 ディック届先マスタ対応 追加START
    ''' <summary>
    ''' 届先マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="selectRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DestPop(ByVal frm As LMI962F, ByVal selectRow As Integer) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = frm.cmbNrsBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
            .Item("EDI_DEST_CD") = Me._LMIconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMI962G.sprWarning.KOMOKU_VAL.ColNo))
#If True Then ' フィルメニッヒ セミEDI対応  20160926 added inoue
            Dim destCd As String = Me._LMIconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMI962G.sprWarning.ADDITIONAL_FIELD_VALUE_1.ColNo))
            If (String.IsNullOrEmpty(destCd) = False) Then
                .Item("DEST_CD") = destCd
            End If
#End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)

        MyBase.ClearMessageData()

        'Pop起動
        Return Me._LMIconH.FormShow(ds, "LMZ210", "", True)

    End Function
    '2012.06.01 ディック届先マスタ対応 追加END

    ''' <summary>
    ''' 商品マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="selectRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GoodsPop(ByVal frm As LMI962F, ByVal selectRow As Integer) As LMFormData

        Dim ds As DataSet
        Dim dt As DataTable
        Dim dr As DataRow

        'LMZ021表示するかどうかのフラグ
        Dim custDetailsDr() As DataRow = Nothing
        custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbNrsBr.SelectedValue.ToString(),
                                                                                                  "' AND CUST_CD = '", frm.txtCustCD_L.TextValue,
                                                                                                  "' AND SUB_KB = '9H'"))
        If custDetailsDr.Length > 0 Then
            If Convert.ToInt32(custDetailsDr(0).Item("SET_NAIYO").ToString) = 1 Then
                _LMZ021Flg = True
            Else
                _LMZ021Flg = False
            End If
        Else
            _LMZ021Flg = False
        End If

        'アクサルタの場合はLMZ021DSを、そうでない場合はLMZ020DSを使用する
        If _LMZ021Flg = True Then
            ds = New LMZ021DS()
            dt = ds.Tables(LMZ021C.TABLE_NM_IN)
        Else
            ds = New LMZ020DS()
            dt = ds.Tables(LMZ020C.TABLE_NM_IN)
        End If
        dr = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = frm.cmbNrsBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
            .Item("CUST_CD_M") = frm.txtCustCD_M.TextValue
            .Item("SEARCH_KEY_2") = Me._LMIconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMI962G.sprWarning.KOMOKU_VAL.ColNo))
#If True Then ' フィルメニッヒ セミEDI対応  20160912 added inoue
            Dim irime As String = Me._LMIconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMI962G.sprWarning.ADDITIONAL_FIELD_VALUE_1.ColNo))
            If (String.IsNullOrEmpty(irime) = False) Then
                .Item("IRIME") = irime
            End If
#End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            If _LMZ021Flg = True Then
                'LMZ021でDSP用で検索するかどうかのフラグ
                custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbNrsBr.SelectedValue.ToString(), _
                                                                                                          "' AND CUST_CD = '", frm.txtCustCD_L.TextValue, _
                                                                                                          "' AND SUB_KB = '9I'"))
                If custDetailsDr.Length > 0 Then
                    If Convert.ToInt32(custDetailsDr(0).Item("SET_NAIYO").ToString) = 1 Then
                        .Item("DSP_FLG") = "1"
                    Else
                        .Item("DSP_FLG") = String.Empty
                    End If
                Else
                    .Item("DSP_FLG") = String.Empty
                End If
            Else
                Select Case frm.cmbShubetu.SelectedValue.ToString
                    Case LMZ020C.SYORI_KB.OUTKA_TOUROKU
                        .Item("EDI_INOUT_KB") = LMZ020C.INOUT_KB.OUTKA
                    Case Else
                        .Item("EDI_INOUT_KB") = String.Empty
                End Select
            End If
        End With

        dt.Rows.Add(dr)

        MyBase.ClearMessageData()

        If _LMZ021Flg = True Then
            'Pop起動(アクサルタ専用)
            Return Me._LMIconH.FormShow(ds, "LMZ021", "", True)
        Else
            'Pop起動
            Return Me._LMIconH.FormShow(ds, "LMZ020", "", True)
        End If

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI962F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        Me.Touroku(frm, _Ds)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI962F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' 
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMI962F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprCellDoubleClick")

        'ダブルクリックアクション処理
        Call Me.DoubleClick(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprCellDoubleClick")

    End Sub

    Friend Sub sprWarningEnter(ByVal frm As LMI962F)

        Call Me.RowSelection()

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprWarningLeaveCell(ByVal frm As LMI962F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        'MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        'MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
