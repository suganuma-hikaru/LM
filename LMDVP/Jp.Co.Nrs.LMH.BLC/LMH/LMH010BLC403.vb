' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH010    : EDI入荷検索
'  EDI荷主ID　　　　:  403　　　 : デュポン(横浜)
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH010BLC403
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH010DAC403 = New LMH010DAC403()

    Private _MstDac As LMH010DAC = New LMH010DAC()

    Private _ChkBlc As LMH010BLC = New LMH010BLC()

    '要望番号419 2012.01.26 START
    Private _SetWarningFlg As Boolean = False
    '要望番号419 2012.01.26 END

#End Region

#Region "Method"

#Region "入荷登録処理"

#Region "入荷登録"
    ''' <summary>
    ''' 入荷登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaToroku(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'EDI入荷(大)の取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH010_INKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH010_INKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(大)のタリフ設定を行う
        ds = Me.SetTariff(ds)

        'EDI入荷(大)の関連チェックを行う
        If Me.InkaLKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'EDI入荷(中)の関連チェックを行う
        If Me.InkaMKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'DB存在チェック(大)
        If Me.DbCheckL(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        '商品マスタ値取得、EDI入荷(中)編集
        If Me.SetGoodsMst(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If


        '風袋の取得
        If Me.SetPkgUtZkbn(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        ''EDI入荷(中)のチェック(標準入目)を行う
        'If Me.InkaMIrimeCheck(ds, rowNo, ediCtlNo) = False Then
        '    Return ds
        'End If

        '入荷管理番号(大)の設定
        ds = Me.GetInkaNoL(ds)

        '入荷管理番号(中)の設定
        ds = Me.GetInkaNoM(ds)

        Dim eventShubetsu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        'データセット設定処理(入荷L)
        ds = Me.SetDatasetInkaL(ds)

        'データセット設定処理(入荷M)
        ds = Me.SetDatasetInkaM(ds)

        'データセット設定処理(入荷S)
        ds = Me.SetDatasetInkaS(ds)

        'データセット設定処理(受信ヘッダ)
        ds = Me.SetDatasetRcvHed(ds)

        'データセット設定処理(受信明細)
        ds = Me.SetDatasetRcvDtl(ds)

        'データセット設定処理(作業)
        ds = Me.SetDatasetSagyo(ds)

        'データセット設定(運送大,中)
        If ds.Tables("LMH010_INKAEDI_L").Rows(0)("UNCHIN_TP").ToString() = "10" Then
            ds = Me.SetDatasetUnsoL(ds)
            ds = Me.SetDatasetUnsoM(ds)
            ds = Me.SetdatasetUnsoJyuryo(ds)

            '▼▼▼要望番号:466
            '運送Lの関連チェック
            If Me.UnsoCheck(ds, rowNo, ediCtlNo) = False Then
                Return ds
            End If
            '▲▲▲要望番号:466

        End If

        'タブレット項目初期値設定
        ds = SetDatasetInkaLTabletData(ds)

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '受信ヘッダの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        '入荷Lの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaL", ds)

        '入荷Mの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaM", ds)

        '入荷Sの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaS", ds)

        '作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH010_E_SAGYO").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertSagyo", ds)
        End If

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH010_UNSO_L").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoL", ds)
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH010_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoM", ds)
        End If

        Return ds

    End Function
#End Region

#Region "タリフ設定処理"
    ''' <summary>
    ''' タリフ設定処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTariff(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim yokoTariff As String = dr.Item("YOKO_TARIFF_CD").ToString()
        Dim freeC29 As String = dr.Item("FREE_C29").ToString()
        Dim unchinTp As String = dr.Item("UNCHIN_TP").ToString()
        Dim unchinKb As String = dr.Item("UNCHIN_KB").ToString()
        Dim yokoTariffCd As String = String.Empty

        If String.IsNullOrEmpty(freeC29) = True Then
            freeC29 = "0"
        End If

        '日陸手配かつタリフ分類区分が空の場合、マスタ値を入れる
        If unchinTp = "10" AndAlso String.IsNullOrEmpty(unchinKb) = True Then
            ds = MyBase.CallDAC(Me._MstDac, "SelectDataTariffBunrui", ds)
        End If

        '横持ちタリフが空もしくはFREE_C29の1文字目が"0"または空の場合で
        '日陸手配かつ横持ちの場合はマスタ値を入れる
        If String.IsNullOrEmpty(yokoTariff) OrElse freeC29.Substring(0, 1) = "0" Then

            If unchinTp = "10" AndAlso unchinKb = "40" Then
                ds = MyBase.CallDAC(Me._MstDac, "SelectDataUnchinTariffSet", ds)
            End If

        End If

        Return ds

    End Function

#End Region

#Region "入荷登録処理(運賃作成)"

    ''' <summary>
    ''' 入荷登録処理(運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnchinSakusei(ByVal ds As DataSet) As DataSet

        '運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertUnchinData", ds)

        Return ds

    End Function

#End Region

#Region "入荷登録関連チェック"

    ''' <summary>
    ''' 入荷登録関連チェック(EDI_L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaLKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'EDI管理番号(大)のチェック
        If _ChkBlc.EdiLCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "入荷管理番号"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E349", New String() {"EDIデータ", "入荷管理番号"})
            Return False
        End If

        '入荷日チェック
        If _ChkBlc.InkaDateCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E047", New String() {"入荷日"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"入荷日"})
            Return False
        End If

        '保管料起算日チェック
        If _ChkBlc.HokanStrDateCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E047", New String() {"保管料起算日"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"保管料起算日"})
            Return False
        End If

        '荷主コードL
        If _ChkBlc.CustCdLCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(大)"})
            Return False
        End If

        '荷主コードM
        If _ChkBlc.CustCdLCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(中)"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入荷登録関連チェック(EDI_M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaMKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean


        Dim dtEdiM As DataTable = ds.Tables("LMH010_INKAEDI_M")

        '赤黒区分チェク
        If _ChkBlc.AkakuroCheck(dtEdiM) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "入荷登録"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"赤データ", "入荷登録"})
            Return False
        End If

        '個数チェック
        If _ChkBlc.NbCheck(dtEdiM) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E320", New String() {"マイナスデータ", "入荷登録"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"マイナスデータ", "入荷登録"})
            Return False
        End If

        '商品チェック
        If _ChkBlc.GoodsCdCheck(dtEdiM) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"マイナスデータ", "入荷登録"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入荷登録入目チェック(EDI_M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaMIrimeCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean


        Dim dtEdiM As DataTable = ds.Tables("LMH010_INKAEDI_M")

        '標準入目チェック
        If _ChkBlc.StdIrimeCheck(dtEdiM) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E333", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E333")
            Return False
        End If

        Return True

    End Function

    '▼▼▼要望番号:466
    ''' <summary>
    ''' 入荷登録関連チェック(運送)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="rowNo"></param>
    ''' <param name="ediCtlNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnsoCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtUnsoL As DataTable = ds.Tables("LMH010_UNSO_L")

        '運送重量チェック
        If _ChkBlc.UnsoJuryoCheck(dtUnsoL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E424", New String() {LMH010BLC.MAX_UNSOWT, "入荷登録"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function
    '▲▲▲要望番号:466

#End Region

#Region "入荷登録DB存在チェック(大)"
    Private Function DbCheckL(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim drJudge As DataRow = ds.Tables("LMH010_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)

        '入荷種別
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N007
        drJudge("KBN_CD") = drEdiL("INKA_TP")

        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"入荷種別", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"入荷種別", "区分マスタ"})
            Return False
        End If

        '入荷区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N006
        drJudge("KBN_CD") = drEdiL("INKA_KB")

        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"入荷区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"入荷区分", "区分マスタ"})
            Return False
        End If

        '進捗区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N004
        drJudge("KBN_CD") = drEdiL("INKA_STATE_KB")

        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"進捗区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"進捗区分", "区分マスタ"})
            Return False
        End If

        '倉庫
        Call MyBase.CallDAC(Me._MstDac, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"倉庫コード", "倉庫マスタ"})
            Return False
        End If

        '荷主マスタ
        Call MyBase.CallDAC(Me._MstDac, "SelectDataCust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"荷主コード", "荷主マスタ"})
            Return False
        End If

        '課税区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_Z001
        drJudge("KBN_CD") = drEdiL("TAX_KB")
        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"課税区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"課税区分", "区分マスタ"})
            Return False
        End If

        '運送手配区分
        If String.IsNullOrEmpty(drEdiL("UNCHIN_TP").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
            drJudge("KBN_CD") = drEdiL("UNCHIN_TP")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"運送手配区分", "区分マスタ"})
                Return False
            End If
        End If

        'タリフ分類区分
        If String.IsNullOrEmpty(drEdiL("UNCHIN_KB").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
            drJudge("KBN_CD") = drEdiL("UNCHIN_KB")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"タリフ分類区分", "区分マスタ"})
                Return False
            End If
        End If

        '届先コード
        If String.IsNullOrEmpty(drEdiL("OUTKA_MOTO").ToString()) = False Then
            Call MyBase.CallDAC(Me._MstDac, "SelectDataDest", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"届先コード", "届先マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"届先コード", "届先マスタ"})
                Return False
            End If
        End If

        '車両区分
        If String.IsNullOrEmpty(drEdiL("SYARYO_KB").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
            drJudge("KBN_CD") = drEdiL("SYARYO_KB")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"車両区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"車両区分", "区分マスタ"})
                Return False
            End If
        End If

        '運送温度区分
        If String.IsNullOrEmpty(drEdiL("UNSO_ONDO_KB").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U006
            drJudge("KBN_CD") = drEdiL("UNSO_ONDO_KB")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"運送温度区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"運送温度区分", "区分マスタ"})
                Return False
            End If
        End If

        '運送会社コード
        If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = False OrElse String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = False Then

            If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = True Then
                drEdiL("UNSO_CD") = String.Empty
            End If

            If String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = True Then
                drEdiL("UNSO_BR_CD") = String.Empty
            End If

            Call MyBase.CallDAC(Me._MstDac, "SelectDataUnsoco", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"運送会社コード", "運送会社マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"運送会社コード", "運送会社マスタ"})
                Return False
            End If
        End If

        'タリフコード
        If String.IsNullOrEmpty(drEdiL("YOKO_TARIFF_CD").ToString()) = False Then

            Dim unchinKB As String = drEdiL("UNCHIN_KB").ToString()

            Select Case unchinKB
                Case "10", "20", "50"

                    Call MyBase.CallDAC(Me._MstDac, "SelectDataUnchinTariff", ds)

                Case "40"

                    Call MyBase.CallDAC(Me._MstDac, "SelectDataYokoTariff", ds)

            End Select

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"タリフコード", "マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"タリフコード", "マスタ"})
                Return False
            End If

        End If

        Dim drIn As DataRow = ds.Tables("LMH010INOUT").Rows(0)

        'オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL("OUTKA_FROM_ORD_NO").ToString()) = False Then

            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
                Call MyBase.CallDAC(Me._MstDac, "SelectOrderCheckData", ds)
                If MyBase.GetResultCount > 0 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E377", New String() {"入荷データ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("E377", New String() {"入荷データ"})
                    Return False
                End If

            End If
        End If

        Return True

    End Function
#End Region

#Region "入荷登録マスタ存在チェック(中)"
    Private Function SetGoodsMst(ByRef ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim max As Integer = dtM.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtM As DataTable = setDs.Tables("LMH010_INKAEDI_M")
        Dim setDtL As DataTable = setDs.Tables("LMH010_INKAEDI_L")
        Dim goodsDt As DataTable = setDs.Tables("LMH010_M_GOODS")

        Dim flgWarning As Boolean = False 'ワーニングフラグ
        Dim flgReturnSet As Boolean = False 'EDI入荷データ戻しフラグ
        Dim msgArray(5) As String

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDtM.ImportRow(dtM.Rows(i))
            setDtL.ImportRow(dtL.Rows(0))

            '条件の再設定
            setDtM = Me.SetGoodsCdFromWarning(setDtM, ds, LMH010BLC.DUPONT_WID_M001)

            '商品マスタ検索（NRS商品コード or 荷主商品コード）
            setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoods", setDs))

            If MyBase.GetResultCount = 0 Then
                '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                'MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"商品", "商品マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Dim sErrMsg As String = Me._ChkBlc.GetErrMsgE493(setDs)
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                'このレコードのワーニングをクリア
                ds.Tables("WARNING_DTL").Rows.Clear()
                Return False

            ElseIf GetResultCount() > 1 Then

                '入目 + 荷主商品コードで再検索
                setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoods2", setDs))

                If MyBase.GetResultCount = 1 Then
                Else
                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    ds = Me._ChkBlc.SetWarningM("W162", LMH010BLC.DUPONT_WID_M001, ds, setDs, msgArray)
                    'MyBase.SetMessage("W162")
                    flgWarning = True 'ワーニングフラグをたてて処理続行
                    Continue For
                End If

                '要望番号419 2012.02.02 追加START
            ElseIf GetResultCount() = 1 Then

                If Me._SetWarningFlg = False AndAlso String.IsNullOrEmpty(dtM.Rows(i)("NRS_GOODS_CD").ToString()) = True AndAlso _
                   Convert.ToDecimal(dtM.Rows(i)("IRIME")).Equals(Convert.ToDecimal(goodsDt.Rows(0)("STD_IRIME_NB"))) = False Then
                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    ds = Me._ChkBlc.SetWarningM("W192", LMH010BLC.DUPONT_WID_M001, ds, setDs, msgArray)
                    flgWarning = True 'ワーニングフラグをたてて処理続行
                    Continue For
                End If
                '要望番号419 2012.02.02 追加END

            End If

            'NRS商品キー
            dtM.Rows(i)("NRS_GOODS_CD") = goodsDt.Rows(0)("GOODS_CD_NRS")

            '商品名
            dtM.Rows(i)("GOODS_NM") = goodsDt.Rows(0)("GOODS_NM_1")

            '個数単位
            dtM.Rows(i)("NB_UT") = goodsDt.Rows(0)("NB_UT")

            '包装個数(入数)
            Dim edimPkgNb As Decimal = Convert.ToDecimal(dtM.Rows(i)("PKG_NB"))
            dtM.Rows(i)("PKG_NB") = goodsDt.Rows(0)("PKG_NB")

            '包装単位
            dtM.Rows(i)("PKG_UT") = goodsDt.Rows(0)("PKG_UT")

            '入荷包装個数
            dtM.Rows(i)("INKA_PKG_NB") = Math.Floor(Convert.ToDecimal(dtM.Rows(i)("NB")) / Convert.ToDecimal(dtM.Rows(i)("PKG_NB")))

            '端数
            dtM.Rows(i)("HASU") = Convert.ToDecimal(dtM.Rows(i)("NB")) Mod Convert.ToDecimal(dtM.Rows(i)("PKG_NB"))

            'EDI入荷(中)の個数
            Dim ediNb As Decimal = Convert.ToDecimal(dtM.Rows(i)("NB"))

            '標準入目
            '要望番号419 2012.01.26 START
            'EDIワーニング画面より入目違いの商品選択を行い、EDI入荷(中)入目と商品Mの標準入目に差異がある場合
            '⇒個数,端数,入数の再計算処理を行う(デュポンのみ)
            If Me._SetWarningFlg = True AndAlso Convert.ToDecimal(dtM.Rows(i)("STD_IRIME")).Equals(Convert.ToDecimal(goodsDt.Rows(0)("STD_IRIME_NB"))) = False Then
                dtM.Rows(i)("NB") = Convert.ToDecimal(dtM.Rows(i)("IRIME")) * Convert.ToDecimal(dtM.Rows(i)("NB")) / Convert.ToDecimal(goodsDt.Rows(0)("STD_IRIME_NB"))
                dtM.Rows(i)("HASU") = Convert.ToDecimal(dtM.Rows(i)("NB")) Mod Convert.ToDecimal(dtM.Rows(i)("PKG_NB"))
                dtM.Rows(i)("INKA_PKG_NB") = Math.Floor(Convert.ToDecimal(dtM.Rows(i)("NB")) / Convert.ToDecimal(dtM.Rows(i)("PKG_NB")))

                '個数端数再計算時のチェック
                If ChkReCalc(dtM.Rows(i), rowNo, ediCtlNo) = False Then

                    '個数・端数再計算時に割り切れない場合は、元のEDI入荷(中)の値に戻す
                    dtM.Rows(i)("NB") = ediNb
                    dtM.Rows(i)("HASU") = Convert.ToDecimal(dtM.Rows(i)("NB")) Mod edimPkgNb
                    dtM.Rows(i)("INKA_PKG_NB") = Math.Floor(Convert.ToDecimal(dtM.Rows(i)("NB")) / edimPkgNb)
                    flgReturnSet = True
                Else
                    dtM.Rows(i)("STD_IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
                End If

            ElseIf Convert.ToDecimal(dtM.Rows(i)("STD_IRIME")) = 0 Then
                dtM.Rows(i)("STD_IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            End If

            '標準入目単位
            If Me._SetWarningFlg = True OrElse String.IsNullOrEmpty(dtM.Rows(i)("STD_IRIME_UT").ToString()) = True Then
                dtM.Rows(i)("STD_IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")
            End If

            '入目
            '※注意 個数再計算で割り切れなかった場合は、元のEDI入荷(中)の入目を使用する(flgReturnSet)
            If (Me._SetWarningFlg = True AndAlso flgReturnSet = False) OrElse Convert.ToDecimal(dtM.Rows(i)("IRIME")) = 0 Then
                dtM.Rows(i)("IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            End If

            '入目単位
            If Me._SetWarningFlg = True OrElse String.IsNullOrEmpty(dtM.Rows(i)("IRIME_UT").ToString()) = True Then
                dtM.Rows(i)("IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")
            End If

            '個別重量
            If Me._SetWarningFlg = True OrElse Convert.ToDecimal(dtM.Rows(i)("BETU_WT")) = 0 Then
                dtM.Rows(i)("BETU_WT") = goodsDt.Rows(0)("STD_WT_KGS")
            End If

            '容積
            If Me._SetWarningFlg = True OrElse Convert.ToDecimal(dtM.Rows(i)("CBM")) = 0 Then
                dtM.Rows(i)("CBM") = goodsDt.Rows(0)("STD_CBM")
            End If

            '温度区分
            If Me._SetWarningFlg = True OrElse String.IsNullOrEmpty(dtM.Rows(i)("ONDO_KB").ToString()) = True Then
                dtM.Rows(i)("ONDO_KB") = goodsDt.Rows(0)("ONDO_KB")
            End If
            '要望番号419 2012.01.26 END

            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_1") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_1")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_2") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_2")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_3") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_3")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_4") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_4")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_5") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_5")

            dtM.Rows(i)("STD_WT_KGS") = goodsDt.Rows(0)("STD_WT_KGS")
            dtM.Rows(i)("STD_IRIME_NB") = goodsDt.Rows(0)("STD_IRIME_NB")
            dtM.Rows(i)("TARE_YN") = goodsDt.Rows(0)("TARE_YN")

            '要望番号1003 2012.05.08 追加START
            '商品明細マスタより置場情報を取得(サブ区分="02")セット内容)

            dtM.Rows(i)("TOU_NO") = String.Empty
            dtM.Rows(i)("SITU_NO") = String.Empty
            dtM.Rows(i)("ZONE_CD") = String.Empty
            If String.IsNullOrEmpty(dtM.Rows(i)("NRS_GOODS_CD").ToString()) = False Then
                '商品明細マスタの取得
                setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoodsMeisaiOkiba", setDs))
                If MyBase.GetResultCount <> 0 Then
                    Dim setOkiba As String = setDs.Tables("LMH010_M_GOODS_DETAILS").Rows(0)("SET_NAIYO").ToString()
                    '置場情報が5または6Byte取得できた時のみ置場情報をセット
                    If setOkiba.Length = 6 OrElse setOkiba.Length = 5 Then
                        dtM.Rows(i)("TOU_NO") = setOkiba.Substring(0, 2)
                        dtM.Rows(i)("SITU_NO") = setOkiba.Substring(2, 2)
                        If setOkiba.Length = 6 Then
                            dtM.Rows(i)("ZONE_CD") = setOkiba.Substring(4, 2)
                        ElseIf setOkiba.Length = 5 Then
                            dtM.Rows(i)("ZONE_CD") = setOkiba.Substring(4, 1)
                        End If
                    End If
                End If
            End If
            '要望番号1003 2012.05.08 追加END

        Next

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If

        Return True

    End Function

    '要望番号419
    ''' <summary>
    ''' 再計算値チェック
    ''' </summary>
    ''' <param name="drM"></param>
    ''' <returns></returns>
    ''' <remarks>再計算した値をチェックしエラーの場合、Excel設定してFalseを返す</remarks>
    Private Function ChkReCalc(ByVal drM As DataRow, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim nb As Double = Convert.ToDouble(drM.Item("NB").ToString())

        '個数が整数でない場合のチェック
        If nb - Math.Floor(nb) <> 0 Then
            '再計算エラーの場合でも、EDI入荷(中)データの個数を使用するのでエラーにはしない
            'MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E436", New String() {"個数"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Dim hasu As Double = Convert.ToDouble(drM.Item("HASU").ToString())

        '端数が整数でない場合のチェック
        If hasu - Math.Floor(hasu) <> 0 Then
            '再計算エラーの場合でも、EDI入荷(中)データの端数を使用するのでエラーにはしない
            'MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E436", New String() {"端数"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "入荷管理番号(大)取得"
    ''' <summary>
    ''' 入荷管理番号(大)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoL(ByVal ds As DataSet) As DataSet

        Dim inkaKanriNo As String = String.Empty
        Dim dr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim nrsBrCd As String = dr("NRS_BR_CD").ToString
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim eventShubetsu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        Dim inkaKanriNoPrm As String = ds.Tables("LMH010INOUT").Rows(0)("INKA_CTL_NO_L").ToString()

        ''入荷管理番号(大)をマスタから取得
        'inkaKanriNo = NumberMasterUtility.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, nrsBrCd)

        If eventShubetsu.Equals("3") Then
            '紐付け時は入荷管理番号(大)を引数のDataSetから取得
            inkaKanriNo = inkaKanriNoPrm
        Else
            '入荷登録時は入荷管理番号(大)をマスタから取得
            Dim num As New NumberMasterUtility
            inkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, nrsBrCd)
        End If

        '入荷管理番号(大)をEDI入荷(大)に格納
        dr("INKA_CTL_NO_L") = inkaKanriNo

        '入荷管理番号(大)をEDI入荷(中)に格納
        For i As Integer = 0 To max

            dt.Rows(i)("INKA_CTL_NO_L") = inkaKanriNo

        Next

        Return ds

    End Function
#End Region

#Region "入荷管理番号(中)取得"
    ''' <summary>
    ''' 入荷管理番号(中)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoM(ByVal ds As DataSet) As DataSet

        Dim inkaKanriNo As String = String.Empty
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            inkaKanriNo = (i + 1).ToString("000")
            dt.Rows(i)("INKA_CTL_NO_M") = inkaKanriNo

        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(入荷L)"
    ''' <summary>
    ''' データセット設定(入荷L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim inkaDr As DataRow = ds.Tables("LMH010_B_INKA_L").NewRow()

        inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")
        inkaDr("INKA_TP") = ediDr("INKA_TP")
        inkaDr("INKA_KB") = ediDr("INKA_KB")
        inkaDr("INKA_STATE_KB") = ediDr("INKA_STATE_KB")
        inkaDr("INKA_DATE") = ediDr("INKA_DATE")
        inkaDr("NRS_WH_CD") = ediDr("NRS_WH_CD")
        inkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
        inkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
        inkaDr("INKA_PLAN_QT") = ediDr("INKA_PLAN_QT")
        inkaDr("INKA_PLAN_QT_UT") = ediDr("INKA_PLAN_QT_UT")
        'inkaDr("INKA_TTL_NB") = ediDr("INKA_TTL_NB")

        Dim ediM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = ediM.Rows.Count - 1
        Dim ediMNb As Long = 0

        For i As Integer = 0 To max
            ediMNb = ediMNb + Convert.ToInt64(ediM.Rows(i)("NB"))
        Next

        inkaDr("INKA_TTL_NB") = ediMNb
        inkaDr("BUYER_ORD_NO_L") = ediDr("BUYER_ORD_NO")
        inkaDr("OUTKA_FROM_ORD_NO_L") = ediDr("OUTKA_FROM_ORD_NO")
        inkaDr("TOUKI_HOKAN_YN") = FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
        'デュポン専用(保管料起算日は空をセット)
        inkaDr("HOKAN_STR_DATE") = String.Empty
        'inkaDr("HOKAN_STR_DATE") = ediDr("HOKAN_STR_DATE")
        inkaDr("HOKAN_YN") = FormatZero(ediDr("HOKAN_YN").ToString(), 2)
        inkaDr("HOKAN_FREE_KIKAN") = ediDr("HOKAN_FREE_KIKAN")
        inkaDr("NIYAKU_YN") = FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
        inkaDr("TAX_KB") = ediDr("TAX_KB")
        inkaDr("REMARK") = ediDr("REMARK")
        inkaDr("REMARK_OUT") = ediDr("NYUBAN_L")
        inkaDr("UNCHIN_TP") = ediDr("UNCHIN_TP")
        inkaDr("UNCHIN_KB") = ediDr("UNCHIN_KB")
        inkaDr("SYS_DEL_FLG") = "0"

        'データセットに設定
        ds.Tables("LMH010_B_INKA_L").Rows.Add(inkaDr)

        Return ds

    End Function
#End Region

#Region "データセット設定(入荷M)"
    ''' <summary>
    ''' データセット設定(入荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim inkaDr As DataRow
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1
        Dim ediDrL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)

        For i As Integer = 0 To max

            inkaDr = ds.Tables("LMH010_B_INKA_M").NewRow()
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDrL("INKA_CTL_NO_L")
            inkaDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M")
            inkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            inkaDr("OUTKA_FROM_ORD_NO_M") = ediDr("OUTKA_FROM_ORD_NO")
            inkaDr("BUYER_ORD_NO_M") = ediDr("BUYER_ORD_NO")
            inkaDr("REMARK") = ediDr("REMARK")
            inkaDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH010_B_INKA_M").Rows.Add(inkaDr)
        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(入荷S)"
    ''' <summary>
    ''' データセット設定(入荷S)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaS(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim inkaDr As DataRow
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1

        For i As Integer = 0 To max

            inkaDr = ds.Tables("LMH010_B_INKA_S").NewRow()
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")
            inkaDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M")
            inkaDr("INKA_NO_S") = "001"
            inkaDr("LOT_NO") = ediDr("LOT_NO")
            '要望番号1003 2012.05.08 追加START(商品明細マスタより取得)
            inkaDr("TOU_NO") = ediDr("TOU_NO")
            inkaDr("SITU_NO") = ediDr("SITU_NO")
            inkaDr("ZONE_CD") = ediDr("ZONE_CD")
            '要望番号1003 2012.05.08 追加END
            inkaDr("KONSU") = ediDr("INKA_PKG_NB")
            inkaDr("HASU") = ediDr("HASU")
            inkaDr("IRIME") = ediDr("IRIME")
            inkaDr("BETU_WT") = ediDr("BETU_WT")
            inkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            inkaDr("SPD_KB") = "01"
            inkaDr("OFB_KB") = "01"
            inkaDr("ALLOC_PRIORITY") = "10"
            inkaDr("SYS_DEL_FLG") = ediDr("SYS_DEL_FLG")

            'データセットに設定
            ds.Tables("LMH010_B_INKA_S").Rows.Add(inkaDr)
        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(受信ヘッダ)"
    ''' <summary>
    ''' データセット設定(受信ヘッダ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetRcvHed(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim inDr As DataRow = ds.Tables("LMH010INOUT").Rows(0)
        Dim rcvDr As DataRow = ds.Tables("LMH010_RCV_HED").NewRow()

        rcvDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        rcvDr("INKA_CTL_NO_L") = ediDr("INKA_CTL_NO_L")
        rcvDr("EDI_CTL_NO") = ediDr("EDI_CTL_NO")
        rcvDr("CUST_CD_L") = ediDr("CUST_CD_L")
        rcvDr("CUST_CD_M") = ediDr("CUST_CD_M")
        rcvDr("SYS_UPD_DATE") = inDr("RCV_UPD_DATE")
        rcvDr("SYS_UPD_TIME") = inDr("RCV_UPD_TIME")
        rcvDr("SYS_DEL_FLG") = "0"

        'データセットに設定
        ds.Tables("LMH010_RCV_HED").Rows.Add(rcvDr)

        Return ds

    End Function
#End Region

#Region "データセット設定(受信明細)"
    ''' <summary>
    ''' データセット設定(受信明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetRcvDtl(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim rcvDr As DataRow

        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)
            rcvDr = ds.Tables("LMH010_RCV_DTL").NewRow()

            rcvDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            rcvDr("EDI_CTL_NO") = ediDr("EDI_CTL_NO")
            rcvDr("EDI_CTL_NO_CHU") = ediDr("EDI_CTL_NO_CHU")
            rcvDr("INKA_CTL_NO_L") = ediDr("INKA_CTL_NO_L")
            rcvDr("INKA_CTL_NO_M") = ediDr("INKA_CTL_NO_M")
            rcvDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH010_RCV_DTL").Rows.Add(rcvDr)
        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(作業)"
    ''' <summary>
    ''' データセット設定(作業)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetSagyo(ByVal ds As DataSet) As DataSet

        Dim ediDrM As DataRow
        Dim sagyoDr As DataRow
        Dim ediDrL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1
        Dim nrsBrCd As String = ds.Tables("LMH010INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim sagyoCD As String = String.Empty
        Dim num As New NumberMasterUtility

        For i As Integer = 0 To max

            ediDrM = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            For j As Integer = 1 To 5

                sagyoCD = ediDrM(String.Concat("INKA_KAKO_SAGYO_KB_", j)).ToString

                If String.IsNullOrEmpty(sagyoCD) = False Then

                    sagyoDr = ds.Tables("LMH010_E_SAGYO").NewRow()

                    '2012.03.08 要望番号859 START
                    'sagyoDr("SAGYO_COMP") = "0"
                    'sagyoDr("SKYU_CHK") = "0"
                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    '2012.03.08 要望番号859 END
                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
                    sagyoDr("INOUTKA_NO_LM") = String.Concat(ediDrM("INKA_CTL_NO_L"), ediDrM("INKA_CTL_NO_M"))
                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
                    sagyoDr("WH_CD") = ediDrL("NRS_WH_CD")
                    sagyoDr("IOZS_KB") = "11"
                    sagyoDr("SAGYO_CD") = sagyoCD
                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
                    sagyoDr("DEST_CD") = String.Empty
                    sagyoDr("DEST_NM") = String.Empty
                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
                    sagyoDr("DEST_SAGYO_FLG") = "00"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH010_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next
        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(運送L)"
    ''' <summary>
    ''' データセット設定(運送)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim unsoDr As DataRow = ds.Tables("LMH010_UNSO_L").NewRow()
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim nrsBrCd As String = ds.Tables("LMH010INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility

        unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        '新規採番
        unsoDr("UNSO_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
        unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
        unsoDr("INOUTKA_NO_L") = ediDr("INKA_CTL_NO_L")
        unsoDr("UNSO_CD") = ediDr("UNSO_CD")
        unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
        unsoDr("BIN_KB") = "01"
        unsoDr("JIYU_KB") = "02"
        unsoDr("OUTKA_PLAN_DATE") = ediDr("INKA_DATE")
        unsoDr("ARR_PLAN_DATE") = ediDr("INKA_DATE")

        unsoDr("NRS_WH_CD") = ediDr("NRS_WH_CD")
        unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
        unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")
        unsoDr("CUST_REF_NO") = ediDr("OUTKA_FROM_ORD_NO")
        unsoDr("ORIG_CD") = ediDr("OUTKA_MOTO")
        'unsoDr("DEST_CD") = ""                                  'マスタ値

        '運送梱包個数の計算
        Dim unsoPkgNb As Long = 0

        For i As Integer = 0 To dt.Rows.Count - 1

            unsoPkgNb = unsoPkgNb + Convert.ToInt64(dt.Rows(i).Item("INKA_PKG_NB"))
            If Convert.ToInt64(dt.Rows(i).Item("HASU")) > 0 Then
                unsoPkgNb = unsoPkgNb + 1
            End If

        Next

        unsoDr("UNSO_PKG_NB") = unsoPkgNb                              '集計値
        unsoDr("UNSO_WT") = ""
        unsoDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
        unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNCHIN_KB")
        unsoDr("VCLE_KB") = ediDr("SYARYO_KB")
        unsoDr("MOTO_DATA_KB") = "10"
        '▼▼▼要望番号:602
        unsoDr("TAX_KB") = "01"
        '▲▲▲要望番号:602
        unsoDr("REMARK") = ediDr("REMARK")
        unsoDr("SEIQ_TARIFF_CD") = ediDr("YOKO_TARIFF_CD")
        unsoDr("AD_3") = ""
        unsoDr("UNSO_TEHAI_KB") = ediDr("UNCHIN_TP")
        unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
        unsoDr("SYS_DEL_FLG") = "0"
        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 Start
        unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 End

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso _
           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

            '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
            ds = MyBase.CallDAC(Me._MstDac, "SelectListDataShiharaiTariff", ds)
            Dim unsocoMDr As DataRow = ds.Tables("LMH010_SHIHARAI_TARIFF").Rows(0)

            If MyBase.GetResultCount > 0 Then
                unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
            End If

        End If
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        'データセットに設定
        ds.Tables("LMH010_UNSO_L").Rows.Add(unsoDr)

        Return ds

    End Function
#End Region

#Region "データセット設定(運送M)"
    ''' <summary>
    ''' データセット設定(運送M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim unsoMDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim unsoLDr As DataRow = ds.Tables("LMH010_UNSO_L").Rows(0)
        Dim unsoTtlQt As Decimal = 0

        Dim max As Integer = dt.Rows.Count - 1

        Dim stdWtKgs As Decimal = 0
        Dim irime As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim nisugata As Decimal = 0
        Dim inkaTtlNb As Decimal = 0

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)
            unsoMDr = ds.Tables("LMH010_UNSO_M").NewRow()

            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")
            unsoMDr("UNSO_NO_M") = ediDr("INKA_CTL_NO_M")
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
            unsoMDr("UNSO_TTL_NB") = ediDr("INKA_PKG_NB")
            unsoMDr("NB_UT") = ediDr("NB_UT")
            unsoTtlQt = Convert.ToDecimal(ediDr("IRIME")) * Convert.ToInt64(ediDr("NB"))
            unsoMDr("UNSO_TTL_QT") = unsoTtlQt
            unsoMDr("QT_UT") = ediDr("STD_IRIME_UT")
            unsoMDr("HASU") = ediDr("HASU")
            unsoMDr("IRIME") = ediDr("IRIME")
            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
            irime = Convert.ToDecimal(ediDr("IRIME"))
            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If

            If ediDr("TARE_YN").Equals("01") = False Then

                unsoMDr("BETU_WT") = stdWtKgs * irime / stdIrimeNb

            Else

                unsoMDr("BETU_WT") = stdWtKgs * irime / stdIrimeNb + nisugata

            End If

            unsoMDr("PKG_NB") = ediDr("PKG_NB")
            unsoMDr("LOT_NO") = ediDr("LOT_NO")
            unsoMDr("REMARK") = ediDr("REMARK")
            unsoMDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH010_UNSO_M").Rows.Add(unsoMDr)
        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(運送L：運送重量)"
    ''' <summary>
    ''' データセット設定(運送L：運送重量)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetdatasetUnsoJyuryo(ByVal ds As DataSet) As DataSet

        Dim unsoLDr As DataRow = ds.Tables("LMH010_UNSO_L").Rows(0)
        Dim unsoMDr As DataRow
        Dim ediMDr As DataRow
        Dim unsoJyuryo As Decimal = 0
        Dim max As Integer = ds.Tables("LMH010_UNSO_M").Rows.Count - 1

        For i As Integer = 0 To max

            unsoMDr = ds.Tables("LMH010_UNSO_M").Rows(i)
            ediMDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * Convert.ToDecimal(ediMDr("NB")) + unsoJyuryo

        Next

        unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
        unsoLDr("NB_UT") = ds.Tables("LMH010_INKAEDI_M").Rows(0)("NB_UT")

        Return ds

    End Function

#End Region

#Region "データセット設定(タブレット項目の初期値設定)"
    ''' <summary>
    ''' データセット設定(タブレット項目の初期値設定)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaLTabletData(ByVal ds As DataSet) As DataSet

        Dim drJudge As DataRow = ds.Tables("LMH010_JUDGE").Rows(0)
        Dim drInkaL As DataRow = ds.Tables("LMH010_B_INKA_L").Rows(0)
        Dim tabletYn As String = LMH010BLC.WH_TAB_YN_NO

        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_B007
        drJudge("KBN_CD") = drInkaL("NRS_BR_CD")
        drJudge("VALUE1") = "1.000"

        Call MyBase.CallDAC(Me._MstDac, "SelectDataTabletYN", ds)

        If MyBase.GetResultCount > 0 Then
            tabletYn = LMH010BLC.WH_TAB_YN_YES
        End If

        For Each dr As DataRow In ds.Tables("LMH010_B_INKA_L").Rows
            dr.Item("WH_TAB_STATUS") = LMH010BLC.WH_TAB_STATUS_UNPROCESSED
            dr.Item("WH_TAB_YN") = tabletYn
            dr.Item("WH_TAB_IMP_YN") = LMH010BLC.WH_TAB_IMP_YN_NO
        Next

        Return ds

    End Function
#End Region

#Region "風袋重量の取得"
    ''' <summary>
    ''' 風袋重量の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetPkgUtZkbn(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim drJudge As DataRow

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LMH010_INKAEDI_M")

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '▼▼▼
            '荷姿(区分マスタ)
            'drJudge = ds.Tables("LMH010_JUDGE").NewRow()
            'drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
            'drJudge("KBN_CD") = dt.Rows(i)("PKG_UT")
            'ds.Tables("LMH010_JUDGE").Rows.Add(drJudge)
            '▲▲▲

            '条件の設定
            setDt.ImportRow(dt.Rows(i))

            '▼▼▼
            '荷姿(区分マスタ)
            drJudge = setDs.Tables("LMH010_JUDGE").NewRow()
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
            drJudge("KBN_CD") = dt.Rows(i)("PKG_UT")
            setDs.Tables("LMH010_JUDGE").Rows.Add(drJudge)
            '▲▲▲

            '商品マスタ
            If dt.Rows(i)("TARE_YN").Equals("01") Then

                setDs = MyBase.CallDAC(Me._MstDac, "SelectDataPkgUtZkbn", setDs)

                If String.IsNullOrEmpty(setDt.Rows(0)("NISUGATA").ToString()) = False Then

                    dt.Rows(i)("NISUGATA") = setDt.Rows(0)("NISUGATA")
                Else
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If
            End If

        Next

        Return True
    End Function

#End Region

#End Region

#Region "実績作成処理"
    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        '送信データ作成用情報取得
        ds = MyBase.CallDAC(Me._Dac, "SelectSend", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
            Exit Function
        End If

        '送信データの更新
        ds = MyBase.CallDAC(Me._Dac, "InsertSend", ds)

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '受信ヘッダの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateInkaL", ds)

        Return ds

    End Function

#End Region

#Region "紐付け処理"
    ''' <summary>
    ''' 紐付け処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Himoduke(ByVal ds As DataSet) As DataSet

        '紐付けフラグの設定
        ds = Me.SetHimodukeFlg(ds)

        '受信HEDデータセット
        ds = Me.SetDatasetRcvHed(ds)

        '受信DTLデータセット
        ds = Me.SetDatasetRcvDtl(ds)

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '受信ヘッダの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds
    End Function

    ''' <summary>
    ''' 紐付けフラグの設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHimodukeFlg(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)

        dr.Item("MATCHING_FLAG") = "01"

        Return ds

    End Function

#End Region

#Region "実績取消処理"
    ''' <summary>
    ''' 実績取消処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '受信ヘッダの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds

    End Function

#End Region

#Region "EDI取消"
    Private Function EdiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '受信ヘッダの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds
    End Function

#End Region

#Region " EDI取消⇒未登録, 報告用EDI取消"
    ''' <summary>
    ''' EDI取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>EDI入荷(中), 受信ヘッダ, 受信明細の削除フラグ変更</remarks>
    Private Function EdiOperation(ByVal ds As DataSet) As DataSet

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '受信ヘッダの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds

    End Function
#End Region

#Region "実績作成済⇒実績未, 実績送信済⇒実績未"
    ''' <summary>
    ''' 実績作成済⇒実績未, 実績送信済⇒実績未
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiOperation2(ByVal ds As DataSet) As DataSet

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        'EDI送信の更新
        ds = MyBase.CallDAC(Me._Dac, "DeleteSend", ds)

        '入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateInkaL", ds)

        Return ds

    End Function

#End Region

#Region "実績送信済⇒送信未"
    ''' <summary>
    ''' 実績送信済⇒送信未
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiOperation3(ByVal ds As DataSet) As DataSet


        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        'EDI送信の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateSend", ds)

        Return ds

    End Function

#End Region

#Region "入荷取消⇒未登録"
    ''' <summary>
    ''' 入荷取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Mitouroku(ByVal ds As DataSet) As DataSet

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds

    End Function

#End Region

#Region "左埋処理"
    ''' <summary>
    ''' 0埋処理
    ''' </summary>
    ''' <param name="val">対象文字列</param>
    ''' <param name="keta">0埋後の桁数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatZero(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta, "0"c)

        Return val

    End Function

    ''' <summary>
    ''' スペース埋処理
    ''' </summary>
    ''' <param name="val"></param>
    ''' <param name="keta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatSpace(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta)

        Return val

    End Function


#End Region

    ''' <summary>
    ''' 条件の再設定
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>条件の再設定(ワーニング画面よりNRS商品コードが設定されている場合はそのNRS商品コードを使う)</remarks>
    Private Function SetGoodsCdFromWarning(ByVal setDt As DataTable, ByVal ds As DataSet, ByVal warningId As String) As DataTable

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(0)("EDI_CTL_NO").ToString()
        Dim ediCtlNoM As String = setDt.Rows(0)("EDI_CTL_NO_CHU").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow

        Me._SetWarningFlg = False

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return setDt
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) _
                                                                AndAlso ediCtlNoM.Equals(dr("EDI_CTL_NO_M")) Then
                'ワーニング処理設定の値を反映
                setDt.Rows(0).Item("NRS_GOODS_CD") = dr.Item("MST_VALUE")

                Me._SetWarningFlg = True

            End If

        Next

        Return setDt
    End Function

#End Region

End Class
