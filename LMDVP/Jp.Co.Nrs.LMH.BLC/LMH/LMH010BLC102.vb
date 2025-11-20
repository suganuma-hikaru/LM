' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH010    : EDI入荷検索
'  EDI荷主ID　　　　:  102　　　 : ビックケミー(千葉)
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH010BLC102
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH010DAC102 = New LMH010DAC102()

    Private _MstDac As LMH010DAC = New LMH010DAC()

    Private _ChkBlc As LMH010BLC = New LMH010BLC()

    ''' <summary>
    ''' 使用するBLC共通クラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH010BLC = New LMH010BLC()

#End Region

#Region "Const"

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

        '自動まとめフラグ = "0" or "1"の場合、まとめ処理
        Dim autoMatomeF As String = ds.Tables("LMH010INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        Dim matomesakiInkaNo As String = String.Empty
        Dim matomeFlg As Boolean
        Dim UnsoMatomeFlg As Boolean = False

        'まとめ判定
        If autoMatomeF.Equals("0") OrElse autoMatomeF.Equals("1") Then
            'まとめ先データ検索
            ds = MyBase.CallDAC(Me._MstDac, "SelectMatomeTarget", ds)

            If MyBase.GetResultCount = 0 Then
                matomeFlg = False

            ElseIf autoMatomeF.Equals("0") = True Then
                Dim choiceKb As String = Me._ChkBlc.GetWarningChoiceKb(ds.Tables("LMH010_INKAEDI_L"), ds, LMH010BLC.NSN_WID_L001, 0)
                Dim msgArray(5) As String

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'ワーニング画面(LMH070)呼出設定
                    msgArray(1) = "入荷管理番号(大)"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    msgArray(5) = String.Empty
                    ds = Me._ChkBlc.SetWarningL("W184", LMH010BLC.NSN_WID_L001, ds, msgArray, matomesakiInkaNo)
                    Return ds

                ElseIf choiceKb.Equals("01") Then
                    'ワーニングで"はい"を選択時、まとめ処理を行う
                    matomeFlg = True

                ElseIf choiceKb.Equals("02") Then
                    'ワーニングで"いいえ"を選択時、通常登録処理を行う
                    matomeFlg = False

                End If

            ElseIf autoMatomeF.Equals("1") = True Then
                'まとめ処理を行う
                matomeFlg = True

            End If

        End If

        If matomeFlg = True Then
            If MyBase.GetResultCount > 1 Then
                'まとめ先が複数ある場合はエラー
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E429", New String() {matomesakiInkaNo}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        '入荷管理番号(大)の設定
        ds = Me.GetInkaNoL(ds, matomeFlg)

        '入荷管理番号(中)の設定
        ds = Me.GetInkaNoM(ds, matomeFlg)

        Dim eventShubetsu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        'データセット設定処理(入荷L)
        ds = Me.SetDatasetInkaL(ds, matomeFlg)

        'データセット設定処理(入荷M)
        ds = Me.SetDatasetInkaM(ds)

        'データセット設定処理(入荷S)
        ds = Me.SetDatasetInkaS(ds)

        'データセット設定処理(受信明細)
        ds = Me.SetDatasetRcvDtl(ds)

        'データセット設定処理(作業)
        ds = Me.SetDatasetSagyo(ds)

        'データセット設定(運送大,中)
        If ds.Tables("LMH010_INKAEDI_L").Rows(0)("UNCHIN_TP").ToString() = "10" Then
            If matomeFlg = False Then
                '通常登録
                UnsoMatomeFlg = False
            Else
                'まとめ処理
                If IsDBNull(ds.Tables("LMH010_MATOMESAKI").Rows(0)("UNSO_NO_L")) = True Then
                    'まとめ先に運送データが無い場合、運送登録
                    UnsoMatomeFlg = False
                Else
                    'まとめ先に運送データがある場合、運送まとめ
                    ds = MyBase.CallDAC(Me._Dac, "SelectUnsoMatomeTarget", ds)
                    UnsoMatomeFlg = True
                End If
            End If

            ds = Me.SetDatasetUnsoL(ds)
            ds = Me.SetDatasetUnsoM(ds)
            ds = Me.SetdatasetUnsoJyuryo(ds)

            '運送Lの関連チェック
            If Me.UnsoCheck(ds, rowNo, ediCtlNo) = False Then
                Return ds
            End If

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
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        If matomeFlg = False Then
            '入荷Lの作成
            ds = MyBase.CallDAC(Me._Dac, "InsertInkaL", ds)
        Else
            'まとめの場合、更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateInkaLMatome", ds)
        End If

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

            If UnsoMatomeFlg = False Then
                '通常登録処理
                ds = MyBase.CallDAC(Me._Dac, "InsertUnsoL", ds)
            Else
                '運送まとめ処理
                ds = MyBase.CallDAC(Me._Dac, "UpdateUnsoLMatome", ds)
            End If
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH010_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoM", ds)
        End If

        '2015.05.12 ローム　入荷登録一括対応 追加START
        '通常登録されたINKA_NO_Lをまとめチェック用のデータセットに格納
        If matomeFlg = False Then
            Dim dr As DataRow = ds.Tables("LMH010_IKKATUMATOME_CHK").NewRow()
            dr("INKA_NO_L") = ds.Tables("LMH010_B_INKA_L").Rows(0)("INKA_NO_L")
            ds.Tables("LMH010_IKKATUMATOME_CHK").Rows.Add(dr)
        End If
        '2015.05.12 ローム　入荷登録一括対応 追加END

        If matomeFlg = True Then
            'まとめ先EDI入荷(大)の更新(まとめ先EDIデータにまとめ番号を設定)
            ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiEdiLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
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

        ''個数チェック
        'If _ChkBlc.NbCheck(dtEdiM) = False Then
        '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E320", New String() {"マイナスデータ", "入荷登録"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
        '    'MyBase.SetMessage("E320", New String() {"マイナスデータ", "入荷登録"})
        '    Return False
        'End If

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
            Return False
        End If

        '進捗区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N004
        drJudge("KBN_CD") = drEdiL("INKA_STATE_KB")

        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"進捗区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '倉庫
        Call MyBase.CallDAC(Me._MstDac, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主マスタ
        Call MyBase.CallDAC(Me._MstDac, "SelectDataCust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '課税区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_Z001
        drJudge("KBN_CD") = drEdiL("TAX_KB")
        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"課税区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送手配区分
        If String.IsNullOrEmpty(drEdiL("UNCHIN_TP").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
            drJudge("KBN_CD") = drEdiL("UNCHIN_TP")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
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
                Return False
            End If
        End If

        '届先コード
        If String.IsNullOrEmpty(drEdiL("OUTKA_MOTO").ToString()) = False Then
            Call MyBase.CallDAC(Me._MstDac, "SelectDataDest", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"届先コード", "届先マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
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
        Dim msgArray(5) As String
        Dim ediName As String = String.Empty
        Dim ediValue As String = String.Empty
        Dim mustValue As String = String.Empty
        Dim choiceKb As String = String.Empty

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDtM.ImportRow(dtM.Rows(i))
            setDtL.ImportRow(dtL.Rows(0))

            '条件の再設定
            setDtM = Me.SetGoodsCdFromWarning(setDtM, ds, LMH010BLC.NIK_WID_M001)

            '商品マスタ検索（NRS商品コード or 荷主商品コード）
            setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoods", setDs))

            If MyBase.GetResultCount = 0 Then
                '要望番号:1053(入荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                'MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"商品", "商品マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Dim sErrMsg As String = Me._ChkBlc.GetErrMsgE493(setDs)
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                '要望番号:1053(入荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End

                'このレコードのワーニングをクリア
                ds.Tables("WARNING_DTL").Rows.Clear()
                Return False

            ElseIf GetResultCount() > 1 Then

                '入目 + 荷主商品コードで再検索
                setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoods2", setDs))

                If MyBase.GetResultCount = 1 Then
                Else
                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    ds = Me._ChkBlc.SetWarningM("W162", LMH010BLC.NIK_WID_M001, ds, setDs, msgArray)
                    flgWarning = True 'ワーニングフラグをたてて処理続行
                    Continue For
                End If

            End If

            'NRS商品キー
            dtM.Rows(i)("NRS_GOODS_CD") = goodsDt.Rows(0)("GOODS_CD_NRS")

            '①商品名
            dtM.Rows(i)("GOODS_NM") = goodsDt.Rows(0)("GOODS_NM_1")

            '個数単位
            dtM.Rows(i)("NB_UT") = goodsDt.Rows(0)("NB_UT")

            '②包装個数(入数)
            '送られてくる入数と商品マスタの入数が異なる場合はエラー⇒実績の個数計算が異なってしまう為
            'If Convert.ToDecimal(dtM.Rows(i)("PKG_NB")) <> Convert.ToDecimal(goodsDt.Rows(0)("PKG_NB")) Then
            '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E332", New String() {"包装個数", "商品マスタ", "包装個数"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            '    'このレコードのワーニングをクリア
            '    ds.Tables("WARNING_DTL").Rows.Clear()
            '    Return False
            'End If
            dtM.Rows(i)("PKG_NB") = goodsDt.Rows(0)("PKG_NB")

            '包装単位
            dtM.Rows(i)("PKG_UT") = goodsDt.Rows(0)("PKG_UT")

            '入荷包装個数
            dtM.Rows(i)("INKA_PKG_NB") = Math.Floor(Convert.ToDecimal(dtM.Rows(i)("NB")) / Convert.ToDecimal(dtM.Rows(i)("PKG_NB")))

            '端数
            dtM.Rows(i)("HASU") = Convert.ToDecimal(dtM.Rows(i)("NB")) Mod Convert.ToDecimal(dtM.Rows(i)("PKG_NB"))

            '標準入目
            dtM.Rows(i)("STD_IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")

            '標準入目単位
            'If String.IsNullOrEmpty(dtM.Rows(i)("STD_IRIME_UT").ToString()) = True Then
            dtM.Rows(i)("STD_IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")
            'End If

            '③入目
            '入目が特定できていない場合は、強制的に商品マスタの値を設定
            If Convert.ToDecimal(dtM.Rows(i)("IRIME")) = 0 Then
                dtM.Rows(i)("IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            End If
            '受信時にセットした入目と商品マスタの入目が異なる場合はエラー
            If Convert.ToDecimal(dtM.Rows(i)("IRIME")) <> Convert.ToDecimal(goodsDt.Rows(0)("STD_IRIME_NB")) Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E332", New String() {"入目", "商品マスタ", "標準入目"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'このレコードのワーニングをクリア
                ds.Tables("WARNING_DTL").Rows.Clear()
                Return False
            End If

            '入目単位
            dtM.Rows(i)("IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")

            '個別重量
            dtM.Rows(i)("BETU_WT") = goodsDt.Rows(0)("STD_WT_KGS")

            '容積
            dtM.Rows(i)("CBM") = goodsDt.Rows(0)("STD_CBM")

            '温度区分
            dtM.Rows(i)("ONDO_KB") = goodsDt.Rows(0)("ONDO_KB")

            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_1") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_1")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_2") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_2")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_3") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_3")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_4") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_4")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_5") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_5")

            dtM.Rows(i)("STD_WT_KGS") = goodsDt.Rows(0)("STD_WT_KGS")
            dtM.Rows(i)("STD_IRIME_NB") = goodsDt.Rows(0)("STD_IRIME_NB")
            dtM.Rows(i)("TARE_YN") = goodsDt.Rows(0)("TARE_YN")

            '商品明細マスタより置場情報を取得(サブ区分="02")セット内容)

            'dtM.Rows(i)("TOU_NO") = String.Empty
            'dtM.Rows(i)("SITU_NO") = String.Empty
            'dtM.Rows(i)("ZONE_CD") = String.Empty
            'If String.IsNullOrEmpty(dtM.Rows(i)("NRS_GOODS_CD").ToString()) = False Then
            '荷主明細マスタより置場情報を取得(サブ区分="0C")セット内容)
            setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataCustMeisaiOkiba", setDs))
            If MyBase.GetResultCount <> 0 Then
                Dim setOkiba As String() = setDs.Tables("LMH010_M_CUST_DETAILS").Rows(0)("SET_NAIYO").ToString().Trim().Split("-"c)

                '置場情報がある場合置場情報をセット
                If setOkiba.Length > 0 Then
                    dtM.Rows(i)("TOU_NO") = setOkiba(0)
                    dtM.Rows(i)("SITU_NO") = setOkiba(1)
                    dtM.Rows(i)("ZONE_CD") = setOkiba(2)
                End If

            End If
            'End If

        Next

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
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
    Private Function GetInkaNoL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim inkaKanriNo As String = String.Empty
        Dim dr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim nrsBrCd As String = dr("NRS_BR_CD").ToString
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim eventShubetsu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        Dim inkaKanriNoPrm As String = ds.Tables("LMH010INOUT").Rows(0)("INKA_CTL_NO_L").ToString()

        If eventShubetsu.Equals("3") Then
            '紐付け時は入荷管理番号(大)を引数のDataSetから取得
            inkaKanriNo = inkaKanriNoPrm
        ElseIf matomeFlg = True Then
            'まとめ処理の場合はまとめ先データセットから取得
            inkaKanriNo = ds.Tables("LMH010_MATOMESAKI").Rows(0)("INKA_NO_L").ToString()
            dr("FREE_C30") = String.Concat("05-", ds.Tables("LMH010_MATOMESAKI").Rows(0)("EDI_CTL_NO").ToString())
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
    Private Function GetInkaNoM(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim inkaKanriNoM As String = String.Empty
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim himodukeDt As DataTable = ds.Tables("LMH010_HIMODUKE")
        Dim matomesakiDt As DataTable = ds.Tables("LMH010_MATOMESAKI")
        Dim max As Integer = dt.Rows.Count - 1
        Dim eventShubetsu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        If eventShubetsu.Equals("3") Then
            '紐付け時は入荷管理番号(中)を引数のDataSetから取得
            For i As Integer = 0 To max
                inkaKanriNoM = himodukeDt.Rows(i)("HIMODUKE_NO").ToString()
                dt.Rows(i)("INKA_CTL_NO_M") = inkaKanriNoM
            Next

        ElseIf matomeFlg = True Then
            'まとめ処理の場合、まとめ先DataSetから取得
            Dim maxInkaCtlNoM As Integer = Me._MstDac.GetMaxINKA_NO_M(matomesakiDt.Rows(0)("NRS_BR_CD").ToString, matomesakiDt.Rows(0)("INKA_NO_L").ToString)

            Dim inkaCtlNoM As String = String.Empty

            For i As Integer = 0 To max
                inkaKanriNoM = (maxInkaCtlNoM + i + 1).ToString("000")
                dt.Rows(i)("INKA_CTL_NO_M") = inkaKanriNoM
            Next

        Else
            For i As Integer = 0 To max
                inkaKanriNoM = (i + 1).ToString("000")
                dt.Rows(i)("INKA_CTL_NO_M") = inkaKanriNoM
            Next
        End If

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
    Private Function SetDatasetInkaL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim inkaDr As DataRow = ds.Tables("LMH010_B_INKA_L").NewRow()

        Dim ediM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = ediM.Rows.Count - 1
        Dim ediMNb As Long = 0
        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH010_INKAEDI_DTL_BYK").NewRow()

        If matomeFlg = False Then
            '通常入荷登録
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

            For i As Integer = 0 To max
                ediMNb = ediMNb + Convert.ToInt64(ediM.Rows(i)("NB"))
            Next

            inkaDr("INKA_TTL_NB") = ediMNb
            inkaDr("BUYER_ORD_NO_L") = ediDr("BUYER_ORD_NO") '20151106 tsunehira add 'ediDr("EXTERNAL_DELIVERY_ID")
            inkaDr("OUTKA_FROM_ORD_NO_L") = ediDr("OUTKA_FROM_ORD_NO")
            inkaDr("TOUKI_HOKAN_YN") = FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
            inkaDr("HOKAN_STR_DATE") = ediDr("HOKAN_STR_DATE")
            inkaDr("HOKAN_YN") = FormatZero(ediDr("HOKAN_YN").ToString(), 2)
            inkaDr("HOKAN_FREE_KIKAN") = ediDr("HOKAN_FREE_KIKAN")
            inkaDr("NIYAKU_YN") = FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
            inkaDr("TAX_KB") = ediDr("TAX_KB")
            inkaDr("REMARK") = ediDr("REMARK")
            inkaDr("REMARK_OUT") = drEdiRcvDtl("PURCHASING_DOCUMENT") 'ediDr("NYUBAN_L") '20151106 tsunehira add 
            inkaDr("UNCHIN_TP") = ediDr("UNCHIN_TP")
            inkaDr("UNCHIN_KB") = ediDr("UNCHIN_KB")
            inkaDr("SYS_DEL_FLG") = "0"
        Else
            'まとめ処理
            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")

            Dim matomesakiDr As DataRow = ds.Tables("LMH010_MATOMESAKI").Rows(0)

            For i As Integer = 0 To max
                ediMNb = ediMNb + Convert.ToInt64(ediM.Rows(i)("NB"))
            Next

            inkaDr("INKA_TTL_NB") = ediMNb + Convert.ToInt64(matomesakiDr("INKA_TTL_NB"))
            inkaDr("SYS_UPD_DATE") = matomesakiDr("SYS_UPD_DATE")
            inkaDr("SYS_UPD_TIME") = matomesakiDr("SYS_UPD_TIME")
        End If

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
        Dim ediMRow As DataRow = ds.Tables("LMH010_INKAEDI_M").Rows(0)
        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH010_INKAEDI_DTL_BYK").NewRow()

        For i As Integer = 0 To max

            inkaDr = ds.Tables("LMH010_B_INKA_M").NewRow()
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDrL("INKA_CTL_NO_L")
            inkaDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M")
            inkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            inkaDr("OUTKA_FROM_ORD_NO_M") = drEdiRcvDtl("PURCHASING_DOCUMENT") 'ediDr("PURCHASING_DOCUMENT") '20151109 tsunehira add
            inkaDr("BUYER_ORD_NO_M") = ediDr("BUYER_ORD_NO") '20151109 tsunehira add
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
            '要望番号1003 2012.05.08 追加START(商品明細マスタより取得)
            inkaDr("TOU_NO") = ediDr("TOU_NO")
            inkaDr("SITU_NO") = ediDr("SITU_NO")
            inkaDr("ZONE_CD") = ediDr("ZONE_CD")
            '要望番号1003 2012.05.08 追加END
            inkaDr("LOT_NO") = ediDr("LOT_NO")
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

                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
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
        unsoDr("TAX_KB") = "01"
        unsoDr("REMARK") = ediDr("REMARK")
        unsoDr("SEIQ_TARIFF_CD") = ediDr("YOKO_TARIFF_CD")
        unsoDr("AD_3") = ""
        unsoDr("UNSO_TEHAI_KB") = ediDr("UNCHIN_TP")
        unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
        unsoDr("SYS_DEL_FLG") = "0"
        '要望番号:1211(EDI入荷：運送登録時の中継配送フラグ) 2012/06/28 本明 Start
        unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
        '要望番号:1211(EDI入荷：運送登録時の中継配送フラグ) 2012/06/28 本明 End

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

            '条件の設定
            setDt.ImportRow(dt.Rows(i))

            '荷姿(区分マスタ)
            drJudge = setDs.Tables("LMH010_JUDGE").NewRow()
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
            drJudge("KBN_CD") = dt.Rows(i)("PKG_UT")
            setDs.Tables("LMH010_JUDGE").Rows.Add(drJudge)

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

#Region "Method(セミEDI)"

#Region "データセット設定"

#Region "セミEDI時　データセット設定(EDI受信HED・DTL)"

    ''' <summary>
    ''' データセット設定(EDI受信HED・DTL)：セミEDI
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiInkaEdiRcv(ByVal ds As DataSet, ByVal i As Integer) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)

        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH010_INKAEDI_DTL_BYK").NewRow()
        Dim drSetDtl As DataRow = ds.Tables("LMH010_EDI_TORIKOMI_DTL").Rows(i)

        'EDI受信DTL設定
        drEdiRcvDtl("DEL_KB") = "0"
        drEdiRcvDtl("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("FILE_NAME") = drSetDtl("FILE_NAME_RCV")
        drEdiRcvDtl("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")
        drEdiRcvDtl("GYO") = "000"
        drEdiRcvDtl("EDI_CTL_NO") = String.Empty                                                        '後でセット                
        drEdiRcvDtl("EDI_CTL_NO_CHU") = String.Empty                                                    '後でセット
        drEdiRcvDtl("INKA_CTL_NO_L") = String.Concat(drSemiEdiInfo("BR_INITIAL"), "00000000")
        drEdiRcvDtl("INKA_CTL_NO_M") = "000"

        ''荷主コード、倉庫コード振り分け(区分マスタ)
        'ds.Tables("LMH010_JUDGE").Clear()
        'Dim drJudge As DataRow = ds.Tables("LMH010_JUDGE").NewRow()
        'drJudge("KBN_GROUP_CD") = "B027"
        'drJudge("KBN_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_16").ToString().Trim(), 4) 'プラントコード
        'ds.Tables("LMH010_JUDGE").Rows.Add(drJudge)

        'ds = MyBase.CallDAC(Me._Dac, "SelectDataZkbn", ds)

        'If MyBase.GetResultCount = 1 Then
        '    '営業所
        '    drEdiRcvDtl("NRS_BR_CD") = ds.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM2").ToString()
        '    '倉庫コード
        '    drEdiRcvDtl("NRS_WH_CD") = ds.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM5").ToString()
        '    '荷主L
        '    drEdiRcvDtl("CUST_CD_L") = ds.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM3").ToString()
        '    '荷主M
        '    drEdiRcvDtl("CUST_CD_M") = ds.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM4").ToString()
        'Else
        '    drEdiRcvDtl("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")                                           '後でセット 
        '    drEdiRcvDtl("NRS_WH_CD") = drSemiEdiInfo("WH_CD")
        '    drEdiRcvDtl("CUST_CD_L") = String.Empty
        '    drEdiRcvDtl("CUST_CD_M") = String.Empty
        'End If

        drEdiRcvDtl("NRS_BR_CD") = drSetDtl("COLUMN_17").ToString().Trim()
        drEdiRcvDtl("NRS_WH_CD") = drSetDtl("COLUMN_18").ToString().Trim()
        drEdiRcvDtl("CUST_CD_L") = drSetDtl("COLUMN_19").ToString().Trim()
        drEdiRcvDtl("CUST_CD_M") = drSetDtl("COLUMN_20").ToString().Trim()

        '荷主固有データ
        drEdiRcvDtl("DELIVERY") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 10)                     '注文番号
        drEdiRcvDtl("DELIVERY_DATE") = Format(Convert.ToDateTime(drSetDtl("COLUMN_2")), "yyyyMMdd")            '入港日
        drEdiRcvDtl("ITEM_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 6)                      '商品№
        drEdiRcvDtl("MATERIAL") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 18)                     '商品コード
        drEdiRcvDtl("DESCRIPTION") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 80)                 '商品名
        drEdiRcvDtl("DERIVERY_QUANTITY") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 9)            '入荷個数量
        drEdiRcvDtl("SALES_UNIT") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 5)                   '荷姿
        drEdiRcvDtl("NET_WEIGHT") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 12)                  'ネット重量
        drEdiRcvDtl("BUSINESS_UNIT") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 5)                '入り目単位
        drEdiRcvDtl("PICK_STATUS") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 5)                 'ピックステータス
        drEdiRcvDtl("PURCHASING_DOCUMENT") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 10)        '購買文書
        drEdiRcvDtl("REF_ITEM") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 5)                    'REF商品№
        drEdiRcvDtl("EXTERNAL_DELIVERY_ID") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 8)        '外部注文ID
        drEdiRcvDtl("BATCH") = Me._Blc.LeftB(drSetDtl("COLUMN_14").ToString().Trim(), 10)                      'ロット№
        drEdiRcvDtl("VENDOR") = Me._Blc.LeftB(drSetDtl("COLUMN_15").ToString().Trim(), 10)                     '製造供給元
        drEdiRcvDtl("STORAGE_LOCATION") = Me._Blc.LeftB(drSetDtl("COLUMN_16").ToString().Trim(), 4)            'ストレージロケーション
        drEdiRcvDtl("JISSEKI_SHORI_FLG") = "1"                                                                 '実績処理フラグ

        'データセットに設定
        ds.Tables("LMH010_INKAEDI_DTL_BYK").Rows.Add(drEdiRcvDtl)

        Return ds

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI入荷(中))"

    ''' <summary>
    ''' データセット設定(EDI入荷(中)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiInkaEdiM(ByVal setDs As DataSet _
                                    , ByVal sWhcd As String _
                                    , ByVal sCustCdL As String _
                                    , ByVal sCustCdM As String _
                                    , ByVal sNrsGoodsCd As String _
                                    , ByVal sNrsGoodsNm As String _
                                    , ByVal sIrime As String _
                                    , ByVal iMSkipFlg As Integer _
                                    ) As DataSet

        Dim drInkaEdiM As DataRow = setDs.Tables("LMH010_INKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH010_SEMIEDI_INFO").Rows(0)

        drInkaEdiM("DEL_KB") = "0"
        drInkaEdiM("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drInkaEdiM("EDI_CTL_NO") = drRcvEdiDtl("EDI_CTL_NO")
        drInkaEdiM("EDI_CTL_NO_CHU") = drRcvEdiDtl("EDI_CTL_NO_CHU")
        drInkaEdiM("INKA_CTL_NO_L") = String.Empty
        drInkaEdiM("INKA_CTL_NO_M") = String.Empty
        drInkaEdiM("NRS_GOODS_CD") = sNrsGoodsCd
        drInkaEdiM("CUST_GOODS_CD") = drRcvEdiDtl("MATERIAL")
        drInkaEdiM("GOODS_NM") = drRcvEdiDtl("DESCRIPTION")
        drInkaEdiM("NB_UT") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("NB_UT")
        drInkaEdiM("PKG_NB") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("PKG_NB")
        drInkaEdiM("PKG_UT") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("PKG_UT")
        drInkaEdiM("STD_IRIME") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("STD_IRIME_NB")
        drInkaEdiM("STD_IRIME_UT") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("STD_IRIME_UT")
        drInkaEdiM("BETU_WT") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("STD_WT_KGS")
        drInkaEdiM("CBM") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("STD_CBM")
        drInkaEdiM("ONDO_KB") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("ONDO_KB")
        drInkaEdiM("IRIME") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("STD_IRIME_NB")
        drInkaEdiM("IRIME_UT") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("STD_IRIME_UT")
        If iMSkipFlg = 0 Then
            drInkaEdiM("NB") = Convert.ToInt32(drRcvEdiDtl("DERIVERY_QUANTITY").ToString())
            drInkaEdiM("INKA_PKG_NB") = Convert.ToInt32(drRcvEdiDtl("DERIVERY_QUANTITY").ToString())
        End If
        drInkaEdiM("HASU") = 0

        drInkaEdiM("OUTKA_FROM_ORD_NO") = drRcvEdiDtl("PURCHASING_DOCUMENT") '20151109 tsunehira add
        drInkaEdiM("BUYER_ORD_NO") = String.Empty
        drInkaEdiM("REMARK") = String.Empty
        drInkaEdiM("LOT_NO") = drRcvEdiDtl("BATCH")
        drInkaEdiM("SERIAL_NO") = String.Empty

        drInkaEdiM("OUT_KB") = "0"
        drInkaEdiM("AKAKURO_KB") = "0"
        drInkaEdiM("JISSEKI_FLAG") = "0"
        drInkaEdiM("JISSEKI_USER") = String.Empty
        drInkaEdiM("JISSEKI_DATE") = String.Empty
        drInkaEdiM("JISSEKI_TIME") = String.Empty

        drInkaEdiM("FREE_N01") = drRcvEdiDtl("NET_WEIGHT")
        drInkaEdiM("FREE_N02") = 0
        drInkaEdiM("FREE_N03") = 0
        drInkaEdiM("FREE_N04") = 0
        drInkaEdiM("FREE_N05") = 0
        drInkaEdiM("FREE_N06") = 0
        drInkaEdiM("FREE_N07") = 0
        drInkaEdiM("FREE_N08") = 0
        drInkaEdiM("FREE_N09") = 0
        drInkaEdiM("FREE_N10") = 0

        drInkaEdiM("FREE_C01") = drRcvEdiDtl("ITEM_NO")
        drInkaEdiM("FREE_C02") = drRcvEdiDtl("SALES_UNIT")
        drInkaEdiM("FREE_C03") = drRcvEdiDtl("BUSINESS_UNIT")
        drInkaEdiM("FREE_C04") = drRcvEdiDtl("PICK_STATUS")
        drInkaEdiM("FREE_C05") = drRcvEdiDtl("PURCHASING_DOCUMENT")
        drInkaEdiM("FREE_C06") = drRcvEdiDtl("REF_ITEM")
        drInkaEdiM("FREE_C07") = drRcvEdiDtl("VENDOR")
        drInkaEdiM("FREE_C08") = String.Empty
        drInkaEdiM("FREE_C09") = String.Empty
        drInkaEdiM("FREE_C10") = String.Empty
        drInkaEdiM("FREE_C11") = String.Empty
        drInkaEdiM("FREE_C12") = String.Empty
        drInkaEdiM("FREE_C13") = String.Empty
        drInkaEdiM("FREE_C14") = String.Empty
        drInkaEdiM("FREE_C15") = String.Empty
        drInkaEdiM("FREE_C16") = String.Empty
        drInkaEdiM("FREE_C17") = String.Empty
        drInkaEdiM("FREE_C18") = String.Empty
        drInkaEdiM("FREE_C19") = String.Empty
        drInkaEdiM("FREE_C20") = String.Empty
        drInkaEdiM("FREE_C21") = String.Empty
        drInkaEdiM("FREE_C22") = String.Empty
        drInkaEdiM("FREE_C23") = String.Empty
        drInkaEdiM("FREE_C24") = String.Empty
        drInkaEdiM("FREE_C25") = String.Empty
        drInkaEdiM("FREE_C26") = String.Empty
        drInkaEdiM("FREE_C27") = String.Empty
        drInkaEdiM("FREE_C28") = String.Empty
        drInkaEdiM("FREE_C29") = String.Empty
        drInkaEdiM("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH010_INKAEDI_M").Rows.Add(drInkaEdiM)

        Return setDs

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI入荷(大))"

    ''' <summary>
    ''' データセット設定(EDI入荷(大)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiinkaediL(ByVal setDs As DataSet) As DataSet

        Dim drInkaEdiL As DataRow = setDs.Tables("LMH010_INKAEDI_L").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH010_SEMIEDI_INFO").Rows(0)

        Dim nextBussinessDate As DateTime = Me.GetBussinessDay(MyBase.GetSystemDate(), +1, setDs)

        '荷主Index
        Dim ediCustIndex As String = drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString()

        drInkaEdiL("DEL_KB") = "0"
        drInkaEdiL("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drInkaEdiL("EDI_CTL_NO") = drRcvEdiDtl("EDI_CTL_NO")
        drInkaEdiL("INKA_CTL_NO_L") = String.Empty
        drInkaEdiL("INKA_TP") = "10"
        drInkaEdiL("INKA_KB") = "10"
        drInkaEdiL("INKA_STATE_KB") = "10"
        drInkaEdiL("INKA_DATE") = Format(nextBussinessDate, "yyyyMMdd")
        drInkaEdiL("INKA_TIME") = "0900"
        drInkaEdiL("NRS_BR_CD") = drSemiEdiInfo.Item("NRS_BR_CD").ToString()
        drInkaEdiL("NRS_WH_CD") = drRcvEdiDtl("NRS_WH_CD").ToString()
        drInkaEdiL("CUST_CD_L") = drRcvEdiDtl("CUST_CD_L").ToString()
        drInkaEdiL("CUST_CD_M") = drRcvEdiDtl("CUST_CD_M").ToString()
        drInkaEdiL("CUST_NM_L") = String.Empty
        drInkaEdiL("CUST_NM_M") = String.Empty

        drInkaEdiL("INKA_PLAN_QT") = 0
        drInkaEdiL("INKA_PLAN_QT_UT") = String.Empty
        drInkaEdiL("INKA_TTL_NB") = 0


        drInkaEdiL("NAIGAI_KB") = "01"
        drInkaEdiL("BUYER_ORD_NO") = drRcvEdiDtl("EXTERNAL_DELIVERY_ID") '20151105 tsunehira add
        drInkaEdiL("OUTKA_FROM_ORD_NO") = drRcvEdiDtl("DELIVERY")  '20151105 tsunehira add
        drInkaEdiL("SEIQTO_CD") = String.Empty

        drInkaEdiL("TOUKI_HOKAN_YN") = "1"
        drInkaEdiL("HOKAN_YN") = "1"
        drInkaEdiL("HOKAN_FREE_KIKAN") = 0

        drInkaEdiL("HOKAN_STR_DATE") = Format(nextBussinessDate, "yyyyMMdd")
        drInkaEdiL("NIYAKU_YN") = "1"
        drInkaEdiL("TAX_KB") = "01"
        drInkaEdiL("REMARK") = String.Empty '20151105 tsunehira add
        drInkaEdiL("NYUBAN_L") = String.Empty
        drInkaEdiL("UNCHIN_TP") = "90"
        drInkaEdiL("UNCHIN_KB") = String.Empty
        drInkaEdiL("OUTKA_MOTO") = String.Empty
        drInkaEdiL("SYARYO_KB") = String.Empty
        drInkaEdiL("UNSO_ONDO_KB") = String.Empty
        drInkaEdiL("UNSO_CD") = String.Empty
        drInkaEdiL("UNSO_BR_CD") = String.Empty

        drInkaEdiL("UNCHIN") = 0
        drInkaEdiL("YOKO_TARIFF_CD") = String.Empty

        drInkaEdiL("OUT_FLAG") = "0"
        drInkaEdiL("AKAKURO_KB") = "0"
        drInkaEdiL("JISSEKI_FLAG") = "0"
        drInkaEdiL("JISSEKI_USER") = String.Empty
        drInkaEdiL("JISSEKI_DATE") = String.Empty
        drInkaEdiL("JISSEKI_TIME") = String.Empty

        drInkaEdiL("FREE_N01") = 0
        drInkaEdiL("FREE_N02") = 0
        drInkaEdiL("FREE_N03") = 0
        drInkaEdiL("FREE_N04") = 0
        drInkaEdiL("FREE_N05") = 0
        drInkaEdiL("FREE_N06") = 0
        drInkaEdiL("FREE_N07") = 0
        drInkaEdiL("FREE_N08") = 0
        drInkaEdiL("FREE_N09") = 0
        drInkaEdiL("FREE_N10") = 0

        drInkaEdiL("FREE_C01") = String.Empty 'drRcvEdiDtl("DELIVERY")
        drInkaEdiL("FREE_C02") = String.Empty 'drRcvEdiDtl("DELIVERY_DATE")
        drInkaEdiL("FREE_C03") = String.Empty
        drInkaEdiL("FREE_C04") = String.Empty
        drInkaEdiL("FREE_C05") = String.Empty
        drInkaEdiL("FREE_C06") = String.Empty
        drInkaEdiL("FREE_C07") = String.Empty
        drInkaEdiL("FREE_C08") = String.Empty
        drInkaEdiL("FREE_C09") = String.Empty
        drInkaEdiL("FREE_C10") = String.Empty
        drInkaEdiL("FREE_C11") = String.Empty
        drInkaEdiL("FREE_C12") = String.Empty
        drInkaEdiL("FREE_C13") = String.Empty
        drInkaEdiL("FREE_C14") = String.Empty
        drInkaEdiL("FREE_C15") = String.Empty
        drInkaEdiL("FREE_C16") = String.Empty
        drInkaEdiL("FREE_C17") = String.Empty
        drInkaEdiL("FREE_C18") = String.Empty
        drInkaEdiL("FREE_C19") = String.Empty
        drInkaEdiL("FREE_C20") = String.Concat(drSemiEdiInfo.Item("CUST_CD_L").ToString(), drSemiEdiInfo.Item("CUST_CD_M").ToString())
        drInkaEdiL("FREE_C21") = String.Empty
        drInkaEdiL("FREE_C22") = String.Empty
        drInkaEdiL("FREE_C23") = String.Empty
        drInkaEdiL("FREE_C24") = String.Empty
        drInkaEdiL("FREE_C25") = String.Empty
        drInkaEdiL("FREE_C26") = String.Empty
        drInkaEdiL("FREE_C27") = String.Empty
        drInkaEdiL("FREE_C28") = String.Empty
        drInkaEdiL("FREE_C29") = String.Empty
        drInkaEdiL("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH010_INKAEDI_L").Rows.Add(drInkaEdiL)
        Return setDs


    End Function

#End Region

#End Region

#Region "セミEDI時　商品マスタからCustCd等を取得する"

    ' ''' <summary>
    ' ''' 商品マスタからCustCd等を取得する
    ' ''' </summary>
    ' ''' <param name="ds"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetCustCd(ByVal ds As DataSet _
    '                         , ByRef sNrsGoodsCd As String _
    '                         , ByRef sNrsGoodsNm As String _
    '                         , ByRef sIrime As String _
    '                          ) As Integer

    '    Dim dtMstGoods As DataTable = ds.Tables("LMH010_M_GOODS")
    '    Dim iMstGoodsCnt As Integer = dtMstGoods.Rows.Count

    '    Select Case iMstGoodsCnt

    '        Case 0      '商品マスタ取得０件
    '            sNrsGoodsCd = String.Empty
    '            sNrsGoodsNm = String.Empty
    '            sIrime = "0"

    '        Case 1      '商品マスタ取得１件
    '            sNrsGoodsCd = dtMstGoods.Rows(0).Item("GOODS_CD_NRS").ToString
    '            sNrsGoodsNm = dtMstGoods.Rows(0).Item("GOODS_NM_1").ToString
    '            sIrime = dtMstGoods.Rows(0).Item("STD_IRIME_NB").ToString

    '        Case Else   '商品マスタ取得２件以上

    '            '荷主の単一確認
    '            For i As Integer = 1 To iMstGoodsCnt - 1  'Rows(1)から開始'■要望番号:1612（セミEDI 荷主商品コード重複チェックでアベンド) 2012/12/14 本明修正　（iMstGoodsCnt→iMstGoodsCnt-1に修正）
    '                If (dtMstGoods.Rows(i).Item("CUST_CD_L").ToString()).Equals(dtMstGoods.Rows(i - 1).Item("CUST_CD_L").ToString()) _
    '                AndAlso (dtMstGoods.Rows(i).Item("CUST_CD_M").ToString()).Equals(dtMstGoods.Rows(i - 1).Item("CUST_CD_M").ToString()) Then
    '                Else
    '                    Exit For
    '                End If
    '            Next

    '            sNrsGoodsCd = String.Empty
    '            sNrsGoodsNm = String.Empty

    '            '入目の単一確認
    '            For i As Integer = 1 To iMstGoodsCnt - 1  'Rows(1)から開始'■要望番号:1612（セミEDI 荷主商品コード重複チェックでアベンド) 2012/12/14 本明修正　（iMstGoodsCnt→iMstGoodsCnt-1に修正）
    '                If (dtMstGoods.Rows(i).Item("STD_IRIME_NB").ToString()).Equals _
    '                   (dtMstGoods.Rows(i - 1).Item("STD_IRIME_NB").ToString()) Then
    '                    '等しい場合はセットする
    '                    sIrime = dtMstGoods.Rows(i).Item("STD_IRIME_NB").ToString
    '                Else
    '                    '等しくない場合は既定値をセットして抜ける
    '                    sIrime = "0"
    '                    Exit For
    '                End If
    '            Next

    '    End Select

    'End Function

#Region "セミEDI データセット設定(EDI管理番号(大・中))"

    ''' <summary>
    ''' データセット設定(EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNo(ByVal ds As DataSet _
                               , ByVal iDeleteFlg As Integer, ByVal iSkipFlg As Integer, ByVal bSameKeyFlg As Boolean _
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer, ByRef iMSkipFlg As Integer) As DataSet

        Dim dtRcvEdiDtl As DataTable = ds.Tables("LMH010_INKAEDI_DTL_BYK")
        Dim drRcvEdiDtl As DataRow = dtRcvEdiDtl.Rows(0)
        Dim sNrsBrCd As String = drRcvEdiDtl.Item("NRS_BR_CD").ToString()

        '前行とキーが異なる場合　
        If bSameKeyFlg = False Then
            iEdiCtlNoChu = 0    '０クリア    
        End If

        If iMSkipFlg = 0 Then
            'EDI管理番号(中)をカウントアップ
            iEdiCtlNoChu = iEdiCtlNoChu + 1
        End If

        If iSkipFlg = 0 Then
            'キャンセルフラグが０ かつ スキップフラグが０の場合　
            If bSameKeyFlg = False Then
                '前行とキーが異なる場合　
                'EDI管理番号(大)を新規採番してEDI管理番号(中)を"001"採番
                Dim num As New NumberMasterUtility
                sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_INKA_NO_L, Me, sNrsBrCd)
            End If

            'EDI管理番号
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'DTLにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")   'EDI_CHUにセット
            'dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("000")              '行数にもEDI_CHUと同じ値をセット
        Else
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = String.Concat(ds.Tables("LMH010_SEMIEDI_INFO").Rows(0).Item("BR_INITIAL"), "00000000")             'DTLに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = "000"              'EDI_CHUに固定値をセット
        End If

        ''削除EDI管理番号にも設定する(削除フラグが１の場合のみ)
        'If iDeleteFlg = 1 Then
        '    Dim dtRcvHedDel As DataTable = ds.Tables("LMH010_DTL_BYK_CANCELOUT")
        '    Dim drRcvHedDel As DataRow = ds.Tables("LMH010_DTL_BYK_CANCELOUT").Rows(0)

        '    Dim dtRcvDtlDel As DataTable = ds.Tables("LMH010_DTL_BYK_CANCELOUT")
        '    Dim drRcvDtlDel As DataRow = ds.Tables("LMH010_DTL_BYK_CANCELOUT").Rows(0)
        '    dtRcvHedDel.Rows(0).Item("DELETE_EDI_NO") = sEdiCtlNo
        '    dtRcvDtlDel.Rows(0).Item("DELETE_EDI_NO") = sEdiCtlNo
        '    dtRcvDtlDel.Rows(0).Item("DELETE_EDI_NO_CHU") = iEdiCtlNoChu.ToString("000")
        'End If

        Return ds

    End Function

#End Region
#End Region

#Region "画面取込(セミEDI)チェック処理"
    ''' <summary>
    ''' 画面取込(セミEDI)チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomiChk(ByVal ds As DataSet) As DataSet

        Dim dtSemiInfo As DataTable = ds.Tables("LMH010_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_DTL")

        Dim dr As DataRow
        Dim hedDr As DataRow = dtSemiHed.Rows(0)

        Dim max As Integer = dtSemiDtl.Rows.Count - 1
        Dim hedmax As Integer = dtSemiHed.Rows.Count - 1

        Dim ediCustIndex As String = dtSemiInfo.Rows(0).Item("EDI_CUST_INDEX").ToString()

        Dim iRowCnt As Integer = 0


        For i As Integer = 0 To hedmax

            If dtSemiHed.Rows(i).Item("ERR_FLG").ToString.Equals("1") Then
                '最初からエラーフラグが立っている場合（明細件数０件の場合）
                Dim sFileNm As String = dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString()
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E460", , , LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)

            Else

                ' ''対象データのみソートして抜き出す
                ''Dim brCd As String = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString()
                ''Dim uriagesakiCd As String = String.Empty
                ''Dim inkaType As String = String.Empty
                'Dim strSort As String = " COLUMN_1 ASC ,COLUMN_4 ASC , COLUMN_14 ASC "
                ''Dim value As Decimal = 0

                'Dim drSelect As DataRow()

                ' ''営業所コード+荷主コードによって、抽出するデータを切り替える
                ''If ds.Tables("LMH010_Z_KBN").Rows.Count > 0 Then
                ''    uriagesakiCd = ds.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM4").ToString()
                ''    inkaType = ds.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM3").ToString()
                ''    strSort = ds.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM5").ToString()
                ''    value = Convert.ToDecimal(ds.Tables("LMH010_Z_KBN").Rows(0).Item("VALUE1").ToString())

                ''    drSelect = dtSemiDtl.Select(" COLUMN_20 =  '" & inkaType & "' AND " & _
                ''                        " COLUMN_6  > '" & value & "' AND " & _
                ''                        " COLUMN_8  =  '" & uriagesakiCd & "'", _
                ''                        strSort)   'ソート順
                ''Else
                ''    drSelect = dtSemiDtl.Select(String.Empty, String.Empty)

                ''End If

                'drSelect = dtSemiDtl.Select(String.Empty, strSort)

                'If drSelect.Count = 0 Then
                '    '抜き出したデータRowが０件の場合
                '    dtSemiHed.Rows(i).Item("ERR_FLG") = "1" '０件エラーフラグを立てる

                'Else

                '    'SelectしたデータをdtSemiDtlに再セットする
                '    Dim dtSelect As DataTable           'Select後の DataTable を用意
                '    dtSelect = dtSemiDtl.Clone          'Select前テーブルの情報をクローン化
                '    For Each row As DataRow In drSelect
                '        dtSelect.ImportRow(row)         'SelectしたデータRowをクローンにセットする
                '    Next

                '    'dtSemiDtlに再セット（以降の処理はdtSemiDtlで処理されるため）
                '    dtSemiDtl.Clear()
                '    For k As Integer = 0 To dtSelect.Rows.Count - 1
                '        dtSemiDtl.ImportRow(dtSelect.Rows(k))
                '    Next

                'End If

                max = dtSemiDtl.Rows.Count - 1

                For j As Integer = iRowCnt To max

                    dr = dtSemiDtl.Rows(j)
                    If (dr.Item("FILE_NAME_RCV").ToString().Trim()).Equals(dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString().Trim()) = True Then
                        'ヘッダと明細のファイル名称が等しい場合
                        '入力チェック(数値,日付チェック)
                        If Me.TorikomiValChk(dr, ediCustIndex) = False Then
                            '異常の場合
                            '詳細のエラーフラグに"1"をセットする
                            dr.Item("ERR_FLG") = "1"
                            'ヘッダのエラーフラグに"1"をセットする
                            dtSemiHed.Rows(i).Item("ERR_FLG") = "1"
                        Else
                            '正常の場合は処理無し（未処理（:9）の状態を保持するため）
                        End If

                        '別インスタンス
                        Dim setDs As DataSet = ds.Copy()
                        Dim setDt As DataTable = setDs.Tables("LMH010_EDI_TORIKOMI_DTL")
                        Dim setSemiDt As DataTable = setDs.Tables("LMH010_SEMIEDI_INFO")

                        '値のクリア
                        setDs.Clear()

                        '条件の設定
                        setDt.ImportRow(dtSemiDtl.Rows(j))
                        setSemiDt.ImportRow(dtSemiInfo.Rows(0))


                        setDs.Tables("LMH010_JUDGE").Clear()
                        Dim drJudge As DataRow = setDs.Tables("LMH010_JUDGE").NewRow()
                        drJudge("KBN_GROUP_CD") = "B027"
                        drJudge("KBN_CD") = Me._Blc.LeftB(setDt.Rows(0)("COLUMN_16").ToString().Trim(), 4) 'プラントコード
                        setDs.Tables("LMH010_JUDGE").Rows.Add(drJudge)

                        setDs = MyBase.CallDAC(Me._Dac, "SelectDataZkbn", setDs)

                        If MyBase.GetResultCount = 0 Then

                            ''商品マスタ情報を取得する
                            'setDs = MyBase.CallDAC(Me._Dac, "CheckGoods", setDs)
                            '---------------------------------------------------------------------------
                            ' 商品マスタ読込
                            '---------------------------------------------------------------------------
                            '商品マスタ情報を取得する
                            setDs = MyBase.CallDAC(Me._Dac, "SelectMstGoods", setDs)
                            If String.IsNullOrEmpty(dr("COLUMN_19").ToString()) = True Then
                                If setDs.Tables("LMH010_M_GOODS").Rows.Count = 1 Then
                                    dr("COLUMN_19") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("CUST_CD_L").ToString()
                                    dr("COLUMN_20") = setDs.Tables("LMH010_M_GOODS").Rows(0).Item("CUST_CD_M").ToString()
                                End If
                            End If

                            '商品マスタの存在チェック
                            If Me.TorikomiMstChk(dr, setDs) = False Then

                                '異常の場合
                                '詳細のエラーフラグに"1"をセットする
                                dr.Item("ERR_FLG") = "1"
                                'ヘッダのエラーフラグに"1"をセットする
                                dtSemiHed.Rows(i).Item("ERR_FLG") = "1"
                            Else
                                '営業所
                                dr("COLUMN_17") = setSemiDt.Rows(0).Item("NRS_BR_CD").ToString()
                                '倉庫コード
                                dr("COLUMN_18") = setSemiDt.Rows(0).Item("WH_CD").ToString()

                            End If
                        Else

                            '営業所
                            dr("COLUMN_17") = setDs.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM2").ToString()
                            '倉庫コード
                            dr("COLUMN_18") = setDs.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM5").ToString()
                            '荷主L
                            dr("COLUMN_19") = setDs.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM3").ToString()
                            '荷主M
                            dr("COLUMN_20") = setDs.Tables("LMH010_Z_KBN").Rows(0).Item("KBN_NM4").ToString()

                        End If

                    Else
                        'ヘッダと明細のファイル名称が等しくない場合
                        '現在行を保持してループを抜ける()
                        iRowCnt = j
                        Exit For
                    End If
                Next

                'Dim strSort As String = " COLUMN_1 ASC ,COLUMN_4 ASC , COLUMN_14 ASC "
                '20151109 tsunehira add start
                'Dim strSort As String = " COLUMN_1 ASC ,COLUMN_17 ASC , COLUMN_18 ASC , COLUMN_19 ASC ,COLUMN_20 ASC , COLUMN_4 ASC,COLUMN_14 ASC"
                'Dim strSort As String = "COLUMN_1 ASC ,COLUMN_3 ASC "
                '20151109 tsunehira add end
                '20151111 tsunehira add start
                'ソート順・・・オーダー番号→営業所→倉庫→荷主大→荷主小→商品番号
                Dim strSort As String = "COLUMN_1 ASC , COLUMN_17 ASC , COLUMN_18 ASC , COLUMN_19 ASC , COLUMN_20 ASC , COLUMN_3 ASC "
                '20151111 tsunehira add end
                Dim drSelect As DataRow()


                drSelect = dtSemiDtl.Select(String.Empty, strSort)

                If drSelect.Count = 0 Then
                    '抜き出したデータRowが０件の場合
                    dtSemiHed.Rows(i).Item("ERR_FLG") = "1" '０件エラーフラグを立てる

                Else

                    'SelectしたデータをdtSemiDtlに再セットする
                    Dim dtSelect As DataTable           'Select後の DataTable を用意
                    dtSelect = dtSemiDtl.Clone          'Select前テーブルの情報をクローン化
                    For Each row As DataRow In drSelect
                        dtSelect.ImportRow(row)         'SelectしたデータRowをクローンにセットする
                    Next

                    'dtSemiDtlに再セット（以降の処理はdtSemiDtlで処理されるため）
                    dtSemiDtl.Clear()
                    For k As Integer = 0 To dtSelect.Rows.Count - 1
                        dtSemiDtl.ImportRow(dtSelect.Rows(k))
                    Next

                End If

            End If
        Next


        Return ds

    End Function

#End Region

#Region "カラム項目の値・日付チェック"

    ''' <summary>
    ''' 値・日付チェック
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="ediCustIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TorikomiValChk(ByVal dr As DataRow, ByVal ediCustIndex As String) As Boolean

        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        Dim sNum As String = String.Empty
        Dim dNum As Double = 0
        Dim sDate As String = String.Empty
        Dim sMsg As String = String.Empty
        Dim sStr As String = String.Empty

        sMsg = "注文番号(カラム1番目)["
        sStr = dr.Item("COLUMN_1").ToString()
        If sStr.Length > 9 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "商品コード(カラム3番目)["
        sStr = dr.Item("COLUMN_3").ToString().Trim()
        If sStr.Length > 20 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "商品名(カラム4番目)["
        sStr = dr.Item("COLUMN_4").ToString().Trim()
        If sStr.Length > 80 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "入荷個数量(カラム6番目)["
        sNum = dr.Item("COLUMN_6").ToString().Trim()
        If String.IsNullOrEmpty(sNum) = True Then
            '空の場合はゼロをセット
            dr.Item("COLUMN_6") = 0
        Else
            If IsNumeric(sNum) Then
                '数値の場合
                dNum = Convert.ToDouble(sNum)
                If dNum > 999999999.999 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            Else
                '数値でない場合
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            End If
        End If

        sMsg = "外部注文ID(カラム13番目)["
        sDate = dr.Item("COLUMN_13").ToString()
        If sStr.Length > 30 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "ロット№(カラム14番目)["
        sStr = dr.Item("COLUMN_14").ToString().Trim()
        If sStr.Length > 40 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '戻り値設定
        Return bRet

    End Function

#End Region

#Region "商品マスタ"

    ''' <summary>
    ''' マスタ整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TorikomiMstChk(ByVal dr As DataRow, ByVal ds As DataSet) As Boolean

        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        Dim sMsg As String = String.Empty
        Dim sGoodsCd As String = String.Empty
        Dim sGoodsNm As String = String.Empty

        sMsg = "荷主商品コード(カラム4番目)"
        sGoodsCd = dr.Item("COLUMN_4").ToString()      '荷主商品コード
        sGoodsNm = dr.Item("COLUMN_5").ToString()       '商品名

        'If MyBase.GetResultCount() = 0 Then
        If ds.Tables("LMH010_M_GOODS").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E493", New String() {"荷主商品コード", "商品マスタ", String.Concat("荷主商品コード：", sGoodsCd, " 商品名：", sGoodsNm)}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            Return bRet

        ElseIf ds.Tables("LMH010_M_GOODS").Rows.Count > 1 Then
            'ElseIf MyBase.GetResultCount() > 1 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E494", New String() {"荷主商品コード", "商品マスタ", String.Concat("荷主商品コード：", sGoodsCd)}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            Return bRet
        End If

        '戻り値設定
        Return bRet

    End Function

#End Region

#Region "画面取込(セミEDI)データセット＋更新処理"

    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        Dim dtSetHed As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_HED")        '取込Hed
        Dim dtSetDtl As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_DTL")        '取込Dtl
        Dim dtSetRet As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_RET")        '処理件数

        Dim dtRcvDtl As DataTable = ds.Tables("LMH010_INKAEDI_DTL_BYK")         'EDI受信Dtl
        'Dim dtRcvDtlCancel As DataTable = ds.Tables("LMH010_DTL_BYK_CANCELOUT")  'EDI受信Hed

        Dim iCancelCnt As Integer = 0
        Dim iGoodsCnt As Integer = 0

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        Dim sWhcd As String = String.Empty          '倉庫コード     
        Dim sCustCdL As String = String.Empty       '荷主コード大   
        Dim sCustCdM As String = String.Empty       '荷主コード中   
        Dim sNrsGoodsCd As String = String.Empty    '日陸商品コード 
        Dim sNrsGoodsNm As String = String.Empty    '日陸商品名     
        Dim sIrime As String = String.Empty         '入目           

        Dim iAkakuroVal As Integer = 0              '赤黒値    (0:黒、1:赤)         

        Dim iSkipFlg As Integer = 0                 'スキップフラグ     (0:EDI入荷に登録する、  1:EDI入荷に登録しない)
        Dim iDeleteFlg As Integer = 0               '取消フラグ         (0:EDI入荷を削除しない、1:EDI入荷を削除する)
        Dim iMSkipFlg As Integer = 0                'EDI入荷(中)スキップフラグ(0:EDI入荷(中)に登録する、  1:EDI入荷(中)に登録しない)

        Dim iFindRcvEdiFlg As Integer = 0           '削除対象EDI受信データ存在フラグ (0:存在しない、1:存在する)
        Dim iFindINKAEDIFlg As Integer = 0         '削除対象EDI入荷データ存在フラグ (0:存在しない、1:存在する)

        Dim sNowKey As String = String.Empty        'キー項目（Temp用）
        Dim sOldKey As String = String.Empty        'キー項目（前行）
        Dim sNewKey As String = String.Empty        'キー項目（現在行）
        Dim bSameKeyFlg As Boolean = False          '前行とキーが同じ場合True、異なる場合False

        Dim sEdiCtlNo As String = String.Empty      'EDI管理番号
        Dim iEdiCtlNoChu As Integer = 0             'EDI管理番号（中）

        Dim iRcvHedInsCnt As Integer = 0            '書込件数（受信HED）
        Dim iRcvDtlInsCnt As Integer = 0            '書込件数（受信DTL）
        Dim iInHedInsCnt As Integer = 0             '書込件数（入荷EDI(大)）
        Dim iInDtlInsCnt As Integer = 0             '書込件数（入荷EDI(中)）
        Dim iRcvHedCanCnt As Integer = 0            '取消件数（受信HED）
        Dim iRcvDtlCanCnt As Integer = 0            '取消件数（受信Dtl）
        Dim iInHedCanCnt As Integer = 0             '取消件数（入荷EDI(大)）
        Dim iInDtlCanCnt As Integer = 0             '取消件数（入荷EDI(中)）

        Dim bNoErr As Boolean = True                'エラー無しフラグ（True：エラー無し、False：エラー有り）

        Dim preDeliverNo As String = String.Empty
        Dim preGoodsCdCust As String = String.Empty
        Dim preLotNo As String = String.Empty
        Dim addKey As String = String.Empty
        Dim preQty As Integer = 0
        Dim sumQty As Integer = 0

        For i As Integer = 0 To iSetDtlMax

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒EDI受信データセット
            '---------------------------------------------------------------------------
            ds.Tables("LMH010_INKAEDI_DTL_BYK").Clear() '受信DTLをクリア
            ds = Me.SetSemiInkaEdiRcv(ds, i)
            Dim drEdiRcvDtl As DataRow = ds.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0)

            '---------------------------------------------------------------------------
            ' 商品マスタ読込
            '---------------------------------------------------------------------------
            '商品マスタ情報を取得する
            ds = MyBase.CallDAC(Me._Dac, "SelectMstGoods", ds)
            'If String.IsNullOrEmpty(drEdiRcvDtl("CUST_CD_L").ToString()) = True Then
            '    If ds.Tables("LMH010_M_GOODS").Rows.Count > 0 Then
            '        drEdiRcvDtl("CUST_CD_L") = ds.Tables("LMH010_M_GOODS").Rows(0).Item("CUST_CD_L").ToString()
            '        drEdiRcvDtl("CUST_CD_M") = ds.Tables("LMH010_M_GOODS").Rows(0).Item("CUST_CD_M").ToString()
            '    End If
            'End If

            ''取得した商品マスタから荷主コード等を決定する
            'Call Me.GetCustCd(ds, sNrsGoodsCd, sNrsGoodsNm, sIrime)

            ''---------------------------------------------------------------------------
            '' INKA_EDI_Mを検索して、既に同一番号が存在する場合はスルーする
            ''---------------------------------------------------------------------------
            'If Me._Dac.GetEdiMCount(drEdiRcvDtl.Item("NRS_BR_CD").ToString, _
            '                        drEdiRcvDtl.Item("OUTKA_NO").ToString, _
            '                        drEdiRcvDtl.Item("OUTKA_NO_GYO").ToString) > 0 Then
            '    Continue For
            'End If

            '入荷個数量が0.0の場合は取り込まずに次のレコードへ
            If Convert.ToDecimal(drEdiRcvDtl.Item("DERIVERY_QUANTITY").ToString()) = 0 Then
                Continue For
            End If

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            'sNewKey = drEdiRcvDtl.Item("DELIVERY").ToString
            sNewKey = String.Concat(drEdiRcvDtl.Item("DELIVERY").ToString, drEdiRcvDtl.Item("CUST_CD_L").ToString, drEdiRcvDtl.Item("CUST_CD_M").ToString)

            bSameKeyFlg = False
            If i = 0 Then
                '1番目は必ずbSameKeyFlgはFalse
                bSameKeyFlg = False
            Else
                '2番目以降はキーを比較
                If sNewKey.Equals(sOldKey) = True Then
                    'キーが同一の場合
                    bSameKeyFlg = True
                Else
                    'キーが異なる場合
                    bSameKeyFlg = False
                End If
            End If

            'EDI受信データ存在フラグ,EDI入荷データ存在フラグを0にする(初期値)
            iFindRcvEdiFlg = 0
            iFindINKAEDIFlg = 0

            '---------------------------------------------------------------------------
            ' 区分を元に赤黒フラグ、スキップフラグを設定
            '---------------------------------------------------------------------------
            'Select Case drEdiRcvDtl.Item("DATA_KB").ToString

            '    Case "0"    '新規
            '        iAkakuroVal = 0     '黒データ
            '        iSkipFlg = 0        'EDI入荷登録する
            '        iDeleteFlg = 0      'EDI入荷削除しない

            '    Case "1"    '変更
            '        iAkakuroVal = 0     '黒データ
            '        iSkipFlg = 0        'EDI入荷登録する
            '        iDeleteFlg = 1      'EDI入荷削除する

            '    Case "9"    'キャンセル
            '        iAkakuroVal = 1     '赤データ
            '        iSkipFlg = 1        'EDI入荷登録しない
            '        iDeleteFlg = 1      'EDI入荷削除する
            'End Select

            iAkakuroVal = 0     '黒データ
            iSkipFlg = 0        'EDI入荷登録する
            iDeleteFlg = 0      'EDI入荷削除しない

            iMSkipFlg = 0

            '2015.09.11 BYK仕様変更対応START
            '入荷報告データがうまくいかなくなるので、同一DELIVERY№＋MATERIAL＋BATCHではまとめない
            'If addKey.Equals(String.Concat(drEdiRcvDtl("DELIVERY").ToString(), drEdiRcvDtl("MATERIAL").ToString(), drEdiRcvDtl("BATCH").ToString())) = True Then
            '    iMSkipFlg = 1
            'End If
            '2015.09.11 BYK仕様変更対応END

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu, iMSkipFlg)

            ' ''---------------------------------------------------------------------------
            ' '' 削除対象受信EDI存在フラグが"1"の場合、EDI受信データの取消処理を行う
            ' ''---------------------------------------------------------------------------
            ''If iFindRcvEdiFlg = 1 Then

            ''    '削除EDI管理番号に設定する
            ''    Dim drRcvDtlCancel As DataRow = ds.Tables("LMH010_DTL_BYK_CANCELOUT").Rows(0)
            ''    If iSkipFlg = 0 Then    'EDI管理番号が採番された場合
            ''        drRcvDtlCancel.Item("DELETE_EDI_NO") = sEdiCtlNo
            ''        drRcvDtlCancel.Item("DELETE_EDI_NO_CHU") = iEdiCtlNoChu.ToString("000")
            ''    Else
            ''        drRcvDtlCancel.Item("DELETE_EDI_NO") = DEF_CTL_NO
            ''        drRcvDtlCancel.Item("DELETE_EDI_NO_CHU") = "000"
            ''    End If

            ''    'EDI受信(DTL)の削除(論理削除)
            ''    ds = MyBase.CallDAC(Me._Dac, "UpdateDelInkaRcvDtl", ds)
            ''    iRcvDtlCanCnt = iRcvDtlCanCnt + 1
            ''End If

            ' ''---------------------------------------------------------------------------
            ' '' 削除対象入荷EDI存在フラグが"1"の場合、EDI入荷データの削除更新を行う
            ' ''---------------------------------------------------------------------------
            ''If iFindINKAEDIFlg = 1 Then

            ''    'EDI入荷(中)の削除(論理削除)
            ''    ds = MyBase.CallDAC(Me._Dac, "UpdateDelINKAEDIM", ds)
            ''    iOutDtlCanCnt = iOutDtlCanCnt + 1

            ''    '※明細が０件の場合のみEDI入荷(大)を削除する
            ''    Dim drRcvDtlCancel As DataRow = ds.Tables("LMH010_DTL_BYK_CANCELOUT").Rows(0)
            ''    Dim sDeleteNrsBrCd As String = drRcvDtlCancel.Item("NRS_BR_CD").ToString      '営業所コード取得
            ''    Dim sDeleteEdiCtlNo As String = drRcvDtlCancel.Item("EDI_CTL_NO").ToString    'EDI管理番号取得
            ''    If Me._Dac.GetMeisaiCount(sDeleteNrsBrCd, sDeleteEdiCtlNo) = 0 Then '明細が０件の場合
            ''        'EDI入荷(大)の削除(論理削除)
            ''        ds = MyBase.CallDAC(Me._Dac, "UpdateDelINKAEDIL", ds)
            ''        iOutHedCanCnt = iOutHedCanCnt + 1
            ''    End If

            ''End If

            '---------------------------------------------------------------------------
            ' EDI受信データの新規追加
            '---------------------------------------------------------------------------
            '別インスタンス
            Dim setDs As DataSet = ds.Copy()
            Dim setDtlDt As DataTable = setDs.Tables("LMH010_INKAEDI_DTL_BYK")
            setDtlDt.Clear()
            setDtlDt.ImportRow(dtRcvDtl.Rows(0))

            setDtlDt.Rows(0).Item("DEL_KB") = iSkipFlg.ToString         'iSkipFlgを削除区分の値として使用する
            'setDtlDt.Rows(0).Item("CANCEL_FLG") = iAkakuroVal.ToString  '赤黒フラグをセットする

            ' EDI受信データ(DTL) 今回登録分との重複件数取得
            setDs = MyBase.CallDAC(Me._Dac, "SelectInkaediDtlBykDupCnt", setDs)

            If MyBase.GetResultCount() > 0 Then
                Dim fileName As String = setDtlDt.Rows(0).Item("FILE_NAME").ToString()
                Dim recNo As String = dtSetDtl.Rows(i).Item("REC_NO").ToString()
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E513",
                        New String() {"既に同一データ取込済み", "取込が", "取込済みのデータを選択して、EDI取消を行ってください。"},
                        recNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, fileName)
                bNoErr = False
            Else
                ' EDI受信データ(DTL)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertInkaEdiRcvDtl", setDs)
                iRcvDtlInsCnt = iRcvDtlInsCnt + 1
            End If

            '---------------------------------------------------------------------------
            ' スキップフラグが0の場合、EDI入荷データの追加処理を行う
            '---------------------------------------------------------------------------
            If iSkipFlg = 0 Then

                '受信DTL⇒EDI入荷(中)へのデータセット(上記で取得した商品情報も含む)
                setDs = Me.SetSemiInkaEdiM(setDs, sWhcd, sCustCdL, sCustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime, iMSkipFlg)

                '注文番号(DELIVERY)+荷主商品コード(MATERIAL)+LOT_NO(BATCH)
                If iMSkipFlg = 1 Then
                    sumQty = sumQty + preQty
                    Dim maxCnt As Integer = setDs.Tables("LMH010_INKAEDI_M").Rows.Count - 1
                    'setDs.Tables("LMH010_INKAEDI_M").Rows(maxCnt).Item("NB") = Convert.ToInt32(setDs.Tables("LMH010_INKAEDI_M").Rows(maxCnt).Item("NB")) + Convert.ToInt32(setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0).Item("DERIVERY_QUANTITY").ToString())
                    'setDs.Tables("LMH010_INKAEDI_M").Rows(maxCnt).Item("INKA_PKG_NB") = Convert.ToInt32(setDs.Tables("LMH010_INKAEDI_M").Rows(maxCnt).Item("INKA_PKG_NB")) + Convert.ToInt32(setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0).Item("DERIVERY_QUANTITY").ToString())
                    setDs.Tables("LMH010_INKAEDI_M").Rows(maxCnt).Item("NB") = sumQty + Convert.ToInt32(setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0).Item("DERIVERY_QUANTITY").ToString())
                    setDs.Tables("LMH010_INKAEDI_M").Rows(maxCnt).Item("INKA_PKG_NB") = sumQty + Convert.ToInt32(setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0).Item("DERIVERY_QUANTITY").ToString())
                Else
                    sumQty = 0
                End If

                If iMSkipFlg = 1 Then
                    'EDI入荷(中)の更新処理
                    setDs = MyBase.CallDAC(Me._Dac, "UpdateInkaEdiM", setDs)
                Else
                    'EDI入荷(中)の新規追加
                    setDs = MyBase.CallDAC(Me._Dac, "InsertInkaEdiM", setDs)
                    iInDtlInsCnt = iInDtlInsCnt + 1
                End If

                '前行と差異がある場合は、EDI入荷(大)を新規追加
                If bSameKeyFlg = False Then

                    '受信DTL⇒EDI入荷(大)へのデータセット
                    setDs = Me.SetSemiinkaediL(setDs)

                    'EDI入荷(大)の新規追加
                    setDs = MyBase.CallDAC(Me._Dac, "InsertInkaEdiL", setDs)
                    iInHedInsCnt = iInHedInsCnt + 1
                End If

            End If

            'キーを入れ替えるのはiSkipFlgの値で判断する
            '※iSkipFlg = 1の場合、sOldKeyは前行の値である必要があるため 
            If iSkipFlg = 0 Then
                sOldKey = sNewKey   'OldキーにNewキーをセット
            End If

            preQty = Convert.ToInt32(setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0).Item("DERIVERY_QUANTITY").ToString())
            preDeliverNo = setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0).Item("DELIVERY").ToString()
            preGoodsCdCust = setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0).Item("MATERIAL").ToString()
            preLotNo = setDs.Tables("LMH010_INKAEDI_DTL_BYK").Rows(0).Item("BATCH").ToString()

            addKey = String.Concat(preDeliverNo, preGoodsCdCust, preLotNo)

        Next

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
        End If

        '処理件数
        dtSetRet.Rows(0).Item("RCV_HED_INS_CNT") = iRcvHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("IN_HED_INS_CNT") = iInHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("IN_DTL_INS_CNT") = iInDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("IN_HED_CAN_CNT") = iInHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("IN_DTL_CAN_CNT") = iInDtlCanCnt.ToString()

        Return ds

    End Function

#End Region

#End Region

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

        '受信DTLデータセット
        ds = Me.SetDatasetRcvDtl(ds)

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

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

            End If

        Next

        Return setDt

    End Function

#End Region

    '2015.08.24 BYK　セミEDI対応START
#Region "営業日取得"
    ''' <summary>
    ''' 営業日取得
    ''' </summary>
    ''' <param name="sStartDay"></param>
    ''' <param name="iBussinessDays"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBussinessDay(ByVal sStartDay As String, ByVal iBussinessDays As Integer, ByVal ds As DataSet) As DateTime
        'sStartDate     ：基準日（YYYYMMDD形式）
        'iBussinessDays ：基準日からの営業日数（前々営業日の場合は-2、前営業日の場合は-1、翌営業日の場合は+1、翌々営業日の場合は+2）
        '戻り値         ：求めた営業日（YYYY/MM/DD形式）

        'スラッシュを付加して日付型に変更
        Dim dBussinessDate As DateTime = Convert.ToDateTime((Convert.ToInt32(sStartDay)).ToString("0000/00/00"))

        For i As Integer = 1 To System.Math.Abs(iBussinessDays)  'マイナス値に対応するため絶対値指定

            '基準日からの営業日数分、Doループを繰り返す
            Do
                '日付加算
                If iBussinessDays > 0 Then
                    dBussinessDate = dBussinessDate.AddDays(1)      '翌営業日
                Else
                    dBussinessDate = dBussinessDate.AddDays(-1)     '前営業日
                End If

                If Weekday(dBussinessDate) = 1 OrElse Weekday(dBussinessDate) = 7 Then
                Else
                    '土日でない場合

                    '該当する日付が休日マスタに存在するか？
                    ds.Tables("LMH010_M_HOL").Clear()
                    Dim drHol As DataRow = ds.Tables("LMH010_M_HOL").NewRow()
                    drHol("HOL") = Format(dBussinessDate, "yyyyMMdd")
                    ds.Tables("LMH010_M_HOL").Rows.Add(drHol)
                    ds = MyBase.CallDAC(Me._MstDac, "SelectMHolList", ds)
                    If MyBase.GetResultCount = 0 Then
                        '休日マスタに存在しない場合、dBussinessDateが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return dBussinessDate

    End Function

#End Region
    '2015.08.24 BYK　セミEDI対応END

End Class
