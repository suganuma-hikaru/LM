' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH050H : EDI入荷データ編集
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH050ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH050H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"
    ' 入力チェックで使用するValidateクラスを格納するフィールド
    Private _V As LMH050V

    ' Gamenクラスを格納するフィールド
    Private _G As LMH050G

    ' Validate共通クラスを格納するフィールド
    Private _LMHconV As LMHControlV

    ' Handler共通クラスを格納するフィールド
    Private _LMHconH As LMHControlH

    ' G共通クラスを格納するフィールド
    Private _LMHconG As LMHControlG

    'クリック行番号の保持
    Private _RowNum As Integer

    'Private _PrmId As String

    Private _Ds As DataSet

    'Private _MsgDs As DataSet

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
        Dim frm As LMH050F = New LMH050F(Me)

        'Validateクラスの設定
        Me._V = New LMH050V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMH050G(Me, frm)

        Me._LMHconH = New LMHControlH(DirectCast(frm, Form), MyBase.GetPGID)

        Me._LMHconG = New LMHControlG(frm)

        Me._LMHconV = New LMHControlV(Me, DirectCast(frm, Form), Me._LMHconG)

        'フォームの初期化
        Call Me.InitControl(frm)

        '特殊コントロールの初期化
        With frm
            '届先選択専用表示パネルの位置調整・非表示
            .pnlDest.Location = New Point(5, 335)
            .pnlDest.Size = New Size(992, 213)
            .pnlDest.Visible = False
        End With

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        ''取得データをSPREADに表示
        Call Me._G.SetSpread(prmDs)

        'frm値設定
        If Me._G.SetForm(frm, prmDs) = False Then
            Exit Sub
        End If

        '↓ データ取得の必要があればここにコーディングする。


        '↑ データ取得の必要があればここにコーディングする。

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
    Private Sub Touroku(ByVal frm As LMH050F, ByVal ds As DataSet)

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        '項目チェック
        If Me._V.TourokuCheck() = False Then
            Call Me._LMHconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"登録"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call Me._LMHconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        ds = Me.SetDs(frm, ds)

        '対象BLF取得
        Dim blfNm As String = Me.GetBlf(ds)
        '対象メソッド名取得(BLF)
        Dim methodNM As String = Me.GetMethod(ds)

        Dim dtHed As DataTable = ds.Tables(LMH050C.TABLE_NM_HED)
        Dim dtDtl As DataTable = ds.Tables(LMH050C.TABLE_NM_DTL)

        'ワーニング設定の初期化
        dtDtl.Clear()

        '==== WSAクラス呼出 ====
        ds = MyBase.CallWSA(blfNm, methodNM, ds)

        Call Me._LMHconH.EndAction(frm) '終了処理

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()

            If ds.Tables(LMH050C.TABLE_NM_DTL).Rows.Count = 0 Then
                'エラー行が存在し、ワーニングがない場合は画面情報をクリア
                Call Me.InformationClearControl(frm)
            Else

                'ワーニングがある場合、画面再設定
                Call Me.InformationSetControl(frm, ds)
            End If
            Exit Sub
        End If

        'If dtDtl.Rows.Count = 0 Then
        If ds.Tables(LMH050C.TABLE_NM_DTL).Rows.Count = 0 Then

            Call Me.InformationClearControl(frm)

            '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 Start
            'MyBase.ShowMessage(frm, "G002", New String() {"登録", String.Empty})
            Dim syoriKb As String = ds.Tables(LMH050C.TABLE_NM_HED).Rows(0).Item("SYORI_KB").ToString()

            If syoriKb.Equals(LMH050C.SHORI_UNSO_TOROKU) Then

                '運送登録時メッセージ
                Dim dtCntRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")   '処理件数
                Dim sCnt As String = "(" & dtCntRet.Rows(0).Item("RCV_HED_INS_CNT").ToString & "件)"
                MyBase.ShowMessage(frm, "G002", New String() {"運送登録", sCnt})

            Else
                '通常メッセージ
                MyBase.ShowMessage(frm, "G002", New String() {"登録", String.Empty})
            End If
            '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 End

        Else

            'ワーニングがある場合、画面再設定
            Call Me.InformationSetControl(frm, ds)

        End If

    End Sub

    ''' <summary>
    ''' 登録処理データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDs(ByVal frm As LMH050F, ByVal ds As DataSet) As DataSet

        Dim max As Integer = frm.sprWarning.ActiveSheet.RowCount - 1
        Dim dr As DataRow
        Dim shoriKb As String = String.Empty

        Dim drIn As DataRow()
        Dim intableNm As String = String.Empty

        With frm.sprWarning.ActiveSheet

            For i As Integer = 0 To max

                shoriKb = Me._LMHconV.GetCellValue(.Cells(i, LMH050G.sprWarning.SYORI.ColNo))

                If shoriKb.Equals(LMH050C.SELECT_CANCEL) = False Then
                    dr = ds.Tables(LMH050C.TABLE_NM_SHORI).NewRow()

                    dr("EDI_WARNING_ID") = Me._LMHconV.GetCellValue(.Cells(i, LMH050G.sprWarning.EDI_WARNING_ID.ColNo))
                    dr("EDI_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(i, LMH050G.sprWarning.KANRI_NO_L.ColNo))
                    dr("EDI_CTL_NO_M") = Me._LMHconV.GetCellValue(.Cells(i, LMH050G.sprWarning.KANRI_NO_M.ColNo))
                    dr("CHOICE_KB") = Me._LMHconV.GetCellValue(.Cells(i, LMH050G.sprWarning.SYORI.ColNo))
                    dr("MST_VALUE") = Me._LMHconV.GetCellValue(.Cells(i, LMH050G.sprWarning.MASTA_VAL.ColNo))
                    'データセットに設定
                    ds.Tables(LMH050C.TABLE_NM_SHORI).Rows.Add(dr)
                Else
                    Dim syoriKb As String = ds.Tables(LMH050C.TABLE_NM_HED).Rows(0).Item("SYORI_KB").ToString()
                    Dim ediCtlNoL As String = Me._LMHconV.GetCellValue(.Cells(i, LMH050G.sprWarning.KANRI_NO_L.ColNo))
                    Dim strFilter As String = String.Empty

                    If syoriKb.Equals(LMH050C.SHORI_INKA_TOROKU) Then
                        intableNm = LMH010C.TABLE_NM_IN
                    ElseIf syoriKb.Equals(LMH050C.SHORI_OUTKA_TOROKU) Then
                        intableNm = LMH030C.TABLE_NM_IN
                    ElseIf syoriKb.Equals(LMH050C.SHORI_INKA_HIMODUKE) Then
                        intableNm = LMH010C.TABLE_NM_IN
                    ElseIf syoriKb.Equals(LMH050C.SHORI_OUTKA_HIMODUKE) Then
                        intableNm = LMH030C.TABLE_NM_IN
                        '2011.09.20 追加START
                    ElseIf syoriKb.Equals(LMH050C.SHORI_INKA_JISSEKI) Then
                        intableNm = LMH010C.TABLE_NM_IN
                    ElseIf syoriKb.Equals(LMH050C.SHORI_OUTKA_JISSEKI) Then
                        intableNm = LMH030C.TABLE_NM_IN
                        '2011.09.20 追加END

                        '2012.03.25 大阪対応START
                    ElseIf syoriKb.Equals(LMH050C.SHORI_UNSO_TOROKU) Then
                        intableNm = LMH030C.TABLE_NM_IN
                        '2012.03.25 大阪対応END
                    End If

                    strFilter = String.Concat("EDI_CTL_NO = '", ediCtlNoL, "'")

                    drIn = ds.Tables(intableNm).Select(strFilter)
                    drIn(0).Item("SHORI_FLG") = "01"
                End If
            Next

        End With

        Return ds
    End Function

    ''' <summary>
    ''' BLF名取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBlf(ByVal ds As DataSet) As String

        Dim syoriKb As String = ds.Tables(LMH050C.TABLE_NM_HED).Rows(0).Item("SYORI_KB").ToString()
        Dim drKbn As DataRow() = Me._LMHconV.SelectKBNListDataRow(syoriKb, "E014")
        Dim blf As String = drKbn(0).Item("KBN_NM2").ToString()

        Return blf

    End Function

    ''' <summary>
    ''' メソッド名取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMethod(ByVal ds As DataSet) As String

        Dim syoriKb As String = ds.Tables(LMH050C.TABLE_NM_HED).Rows(0).Item("SYORI_KB").ToString()
        Dim drKbn As DataRow() = Me._LMHconV.SelectKBNListDataRow(syoriKb, "E014")
        Dim methodNm As String = drKbn(0).Item("KBN_NM3").ToString()

        Return methodNm

    End Function

    ''' <summary>
    ''' 画面情報クリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InformationClearControl(ByVal frm As LMH050F)

        frm.sprWarning.CrearSpread()
        Call Me._G.ClearControl()

    End Sub

    ''' <summary>
    ''' 画面情報設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InformationSetControl(ByVal frm As LMH050F, ByVal ds As DataSet)

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
    Private Sub DoubleClick(ByVal frm As LMH050F)

        Dim selectRow As Integer = frm.sprWarning.ActiveSheet.ActiveRowIndex()
        'マスタ参照フラグ：ワーニングIDの８文字目
        Dim mstFlg As String = Me._LMHconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMH050G.sprWarning.EDI_WARNING_ID.ColNo)).Substring(7, 1)
        Dim prm As LMFormData
        Dim dr As DataRow

        Call Me.RowSelection()


        'マスタ参照フラグが"1"の場合商品マスタ起動
        If mstFlg.Equals("1") Then

            prm = Me.GoodsPop(frm, selectRow)

            If prm.ReturnFlg = True Then
                With frm.sprWarning.ActiveSheet
                    '2017.09.26 annen AXL出荷登録改善対応 START
                    If _LMZ021Flg = True Then
                        dr = prm.ParamDataSet.Tables(LMZ021C.TABLE_NM_OUT).Rows(0)
                    Else
                        dr = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
                    End If
                    'dr = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
                    '2017.09.26 annen AXL出荷登録改善対応 END
                    .Cells(selectRow, LMH050G.sprWarning.GOODS_NM.ColNo).Value = dr.Item("GOODS_NM_1").ToString() '商品名
                    .Cells(selectRow, LMH050G.sprWarning.MASTA_VAL.ColNo).Value = dr.Item("GOODS_CD_NRS").ToString()
                End With
            End If
            '2012.06.01 ディック届先マスタ対応 追加START
        ElseIf mstFlg.Equals("2") = True Then
            prm = Me.DestPop(frm, selectRow)

            If prm.ReturnFlg = True Then
                With frm.sprWarning.ActiveSheet
                    dr = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                    .Cells(selectRow, LMH050G.sprWarning.DEST_NM.ColNo).Value = dr.Item("DEST_NM").ToString()  '商品名
                    .Cells(selectRow, LMH050G.sprWarning.MASTA_VAL.ColNo).Value = dr.Item("DEST_CD").ToString()
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
    Private Function DestPop(ByVal frm As LMH050F, ByVal selectRow As Integer) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
            .Item("EDI_DEST_CD") = Me._LMHconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMH050G.sprWarning.KOMOKU_VAL.ColNo))

#If True Then ' フィルメニッヒ セミEDI対応  20160926 added inoue
            Dim destCd As String = Me._LMHconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMH050G.sprWarning.ADDITIONAL_FIELD_VALUE_1.ColNo))
            If (String.IsNullOrEmpty(destCd) = False) Then
                .Item("DEST_CD") = destCd
            End If
#End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        MyBase.ClearMessageData()

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ210", "", True)

    End Function
    '2012.06.01 ディック届先マスタ対応 追加END

    ''' <summary>
    ''' 商品マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="selectRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GoodsPop(ByVal frm As LMH050F, ByVal selectRow As Integer) As LMFormData

        '2017.09.26 annen AXL出荷登録改善対応 START
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dr As DataRow
        Dim custDetailsDr() As DataRow = Nothing

        'LMZ021表示するかどうかのフラグ
        custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbNrsBr.SelectedValue.ToString(), _
                                                                                                "' AND CUST_CD = '", frm.txtCustCD_L.TextValue, _
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
        'Dim ds As DataSet = New LMZ020DS()
        'Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        'Dim dr As DataRow = dt.NewRow()
        '2017.09.26 annen AXL出荷登録改善対応 END

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
            .Item("CUST_CD_M") = frm.txtCustCD_M.TextValue

            .Item("GOODS_CD_CUST") = Me._LMHconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMH050G.sprWarning.KOMOKU_VAL.ColNo))

#If True Then ' フィルメニッヒ セミEDI対応  20160912 added inoue
            Dim irime As String = Me._LMHconV.GetCellValue(frm.sprWarning.ActiveSheet.Cells(selectRow, LMH050G.sprWarning.ADDITIONAL_FIELD_VALUE_1.ColNo))
            If (String.IsNullOrEmpty(irime) = False) Then
                .Item("IRIME") = irime
            End If
#End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            '001595 【LMS】横浜DSP五協_セミEDI_追加改修(PS吉房) アクサルタのとき実行時エラーで落ちる対応 2018/06/22 Annen upd start
            'Memo)
            '上のロジックでアクサルタのときはLMZ021DSを生成しているが、データセット「LMZ021DS」、データテーブル「LMZ021IN」には項目「EDI_INOUT_KB」が
            '存在しないため実行時エラーが発生していた。アクサルタのときは項目「EDI_INOUT_KB」を使用して
            'その後でデータ抽出条件になることはない（阿達さん談）とのことなので、
            '単純にアクサルタの場合はこの項目に値を設定しないように改修した。
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
            '001595 【LMS】横浜DSP五協_セミEDI_追加改修(PS吉房) アクサルタのとき実行時エラーで落ちる対応 2018/06/22 Annen upd end

        End With
        dt.Rows.Add(dr)

        MyBase.ClearMessageData()

        '2017.09.26 annen AXL出荷登録改善対応 START
        If _LMZ021Flg = True Then
            'Pop起動(アクサルタ専用)
            Return Me._LMHconH.FormShow(ds, "LMZ021", "", True)
        Else
            'Pop起動
            Return Me._LMHconH.FormShow(ds, "LMZ020", "", True)
        End If
        'Return Me._LMHconH.FormShow(ds, "LMZ020", "", True)
        '2017.09.26 annen AXL出荷登録改善対応 END
    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMH050F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMH050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMH050F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprCellDoubleClick")

        'ダブルクリックアクション処理
        Call Me.DoubleClick(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprCellDoubleClick")

    End Sub

    Friend Sub sprWarningEnter(ByVal frm As LMH050F)

        Call Me.RowSelection()

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprWarningLeaveCell(ByVal frm As LMH050F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        'MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        'Call Me.SprCellLeave(frm, e)

        'MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method


#Region "メッセージ取得"


    Private Function GetLMCachedDataTableMessage() As DataSet

        Dim ds As DataSet = New LMH999DS()

        '==== WSAクラス呼出 ====
        ds = MyBase.CallWSA("LMH050BLF", "GetSMessage", ds)

        'Return ds.Tables("S_MESSAGE")

        Return ds

    End Function


#End Region


End Class