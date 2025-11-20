' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH              : EDI
'  プログラムID     :  LMH010HBLC164    : EDI入荷検索:ジョンソンエンドジョンソン(151)から複写・改修
'  EDI荷主ID　　　　:  164              : TSMC
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DAC.LMH010DAC164
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH010BLC164
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH010DAC164 = New LMH010DAC164()

    Private _MstDac As LMH010DAC = New LMH010DAC()

    Private _ChkBlc As LMH010BLC = New LMH010BLC()

    Private _ZaiRecNo As Dictionary(Of Integer, String)

#End Region ' "Field"

#Region "Const"

    ''' <summary>
    ''' 項目名(商品コード,入目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FIELD_NAME_CUST_GOODS_CD_AND_IRIME As String = "荷主商品コード, 入目"

#End Region ' "Const"

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

        'EDI入荷(大)のタリフ設定を行う  => UNCHIN_TP:運賃タイプ[U005]が'90:未定'で固定処理されているためタリフは設定されない
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
            ds = MyBase.CallDAC(Me._Dac, "SelectMatomeTarget", ds)

            If MyBase.GetResultCount = 0 Then
                matomeFlg = False

            ElseIf autoMatomeF.Equals("0") = True Then
                Dim choiceKb As String = Me._ChkBlc.GetWarningChoiceKb(ds.Tables("LMH010_INKAEDI_L"), ds, LMH010BLC.FIL_WID_L001, 0)
                Dim msgArray(5) As String

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'ワーニング画面(LMH070)呼出設定
                    msgArray(1) = "入荷管理番号(大)"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    msgArray(5) = String.Empty
                    ds = Me._ChkBlc.SetWarningL("W184", LMH010BLC.FIL_WID_L001, ds, msgArray, matomesakiInkaNo)
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

                matomesakiInkaNo = Me.matomesakiInkaNo(ds)

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

        'データセット設定(在庫TRS)
        ds = Me.SetDatasetZaiRec(ds)

        'データセット設定処理(受信明細)
        ds = Me.SetDatasetRcvDtl(ds)

        'データセット設定処理(作業)
        ds = Me.SetDatasetSagyo(ds)

        'データセット設定(運送大,中) => UNCHIN_TP:運賃タイプ[U005]が'90:未定'で固定処理されているため運送は設定されない
        If ds.Tables("LMH010_INKAEDI_L").Rows(0)("UNCHIN_TP").ToString() = "10" Then
            ' 運賃タイプ[U005]が日陸手配

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

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

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

        '在庫TRSの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertZaiTrs", ds)

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


#If True Then ' フィルメニッヒセミEDI対応　20161024 added inoue

        ' 日産物流(LMH010DAC04)より移植

        '通常登録されたINKA_NO_Lをまとめチェック用のデータセットに格納
        If matomeFlg = False Then
            Dim dr As DataRow = ds.Tables("LMH010_IKKATUMATOME_CHK").NewRow()
            dr("INKA_NO_L") = ds.Tables("LMH010_B_INKA_L").Rows(0)("INKA_NO_L")
            ds.Tables("LMH010_IKKATUMATOME_CHK").Rows.Add(dr)
        End If

#End If


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
#End Region ' "入荷登録"

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

#End Region ' "タリフ設定処理"

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

#End Region ' "入荷登録処理(運賃作成)"

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

            Return False
        End If

        '入荷日チェック
        If _ChkBlc.InkaDateCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E047", New String() {"入荷日"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)

            Return False
        End If

        '保管料起算日チェック
        If _ChkBlc.HokanStrDateCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E047", New String() {"保管料起算日"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)

            Return False
        End If

        '荷主コードL
        If _ChkBlc.CustCdLCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)

            Return False
        End If

        '荷主コードM
        If _ChkBlc.CustCdLCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)

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

#End Region ' "入荷登録関連チェック"

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
#End Region ' "入荷登録DB存在チェック(大)"

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
        Dim choiceKb As String = String.Empty

        ' デフォルトロケーションの取得
        Dim touNo As String = ""
        Dim situNo As String = ""
        Dim zoneCd As String = ""
        Dim dsKbn As DataSet = ds.Clone()
        Dim dtInkaediL As DataTable = dsKbn.Tables("LMH010_INKAEDI_L")
        Dim drInkaediL As DataRow = dtInkaediL.NewRow()
        drInkaediL.Item("NRS_BR_CD") = dtL.Rows(0).Item("NRS_BR_CD")
        dtInkaediL.Rows.Add(drInkaediL)
        Dim dtKbnIn As DataTable = dsKbn.Tables("LMH010_Z_KBN_IN")
        Dim dtKbnOut As DataTable = dsKbn.Tables("LMH010_Z_KBN_OUT")
        Dim drKbnIn As DataRow = dtKbnIn.NewRow
        drKbnIn.Item("KBN_GROUP_CD") = "L005"
        drKbnIn.Item("KBN_CD") = dtL.Rows(0).Item("NRS_BR_CD")
        drKbnIn.Item("KBN_NM1") = dtL.Rows(0).Item("CUST_CD_L")
        drKbnIn.Item("KBN_NM2") = dtL.Rows(0).Item("CUST_CD_M")
        dtKbnIn.Rows.Add(drKbnIn)
        dsKbn = MyBase.CallDAC(Me._MstDac, "SelectZKbnHanyo", dsKbn)
        If dtKbnOut.Rows.Count >= 1 Then
            touNo = dtKbnOut.Rows(0).Item("KBN_NM3").ToString()
            situNo = dtKbnOut.Rows(0).Item("KBN_NM4").ToString()
            zoneCd = dtKbnOut.Rows(0).Item("KBN_NM5").ToString()
        End If

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDtM.ImportRow(dtM.Rows(i))
            setDtL.ImportRow(dtL.Rows(0))

            '条件の再設定
            setDtM = Me.SetGoodsCdFromWarning(setDtM, ds, LMH010BLC.FIL_WID_M001)

            '商品マスタ検索（NRS商品コード or 荷主商品コード）
            setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoods", setDs))

            If MyBase.GetResultCount = 0 Then

                Dim sErrMsg As String = Me._ChkBlc.GetErrMsgE493(setDs)
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)

                'このレコードのワーニングをクリア
                ds.Tables("WARNING_DTL").Rows.Clear()

                Return False

            ElseIf GetResultCount() > 1 Then

                '入目 + 荷主商品コードで再検索
                setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoods2", setDs))

                If MyBase.GetResultCount <> 1 Then
#If False Then ' フィルメニッヒ セミEDI対応  20160912 changed inoue
                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    ds = Me._ChkBlc.SetWarningM("W162", LMH010BLC.NIK_WID_M001, ds, setDs, msgArray)
#Else

                    Dim warningId As String = LMH010BLC.FIL_WID_M001
                    Dim goodsRow As DataRow = dtM.Rows(i)

                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    ds = Me._ChkBlc.SetWarningM2("W162" _
                                               , warningId _
                                               , ds _
                                               , setDs _
                                               , msgArray _
                                               , FIELD_NAME_CUST_GOODS_CD_AND_IRIME _
                                               , goodsRow.Item("CUST_GOODS_CD").ToString() _
                                               , "" _
                                               , goodsRow.Item("IRIME").ToString())
#End If
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
            dtM.Rows(i)("STD_IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")

            '③入目
            '入目が特定できていない場合は、強制的に商品マスタの値を設定
            If Convert.ToDecimal(dtM.Rows(i)("IRIME")) = 0 Then
                dtM.Rows(i)("IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            End If

            '受信時にセットした入目と商品マスタの入目が異なる場合はエラー
            If Convert.ToDecimal(dtM.Rows(i)("IRIME")) <> Convert.ToDecimal(goodsDt.Rows(0)("STD_IRIME_NB")) _
                AndAlso flgWarning = True Then
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

            dtM.Rows(i)("TOU_NO") = touNo
            dtM.Rows(i)("SITU_NO") = situNo
            dtM.Rows(i)("ZONE_CD") = zoneCd

        Next

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If

        Return True

    End Function

#End Region ' "入荷登録マスタ存在チェック(中)"

#Region "入荷管理番号(大)取得"

    ''' <summary>
    ''' 入荷管理番号(大)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getinkanol(ByVal ds As dataset, ByVal matomeflg As Boolean) As dataset

        Dim inkakanrino As String = String.empty
        Dim dr As datarow = ds.tables("lmh010_inkaedi_l").rows(0)
        Dim nrsbrcd As String = dr("nrs_br_cd").tostring
        Dim dt As datatable = ds.tables("lmh010_inkaedi_m")
        Dim max As Integer = dt.rows.count - 1
        Dim eventshubetsu As String = ds.tables("lmh010_judge").rows(0)("event_shubetsu").tostring()
        Dim inkakanrinoprm As String = ds.tables("lmh010inout").rows(0)("inka_ctl_no_l").tostring()

        If eventshubetsu.equals("3") Then
            '紐付け時は入荷管理番号(大)を引数のdatasetから取得
            inkakanrino = inkakanrinoprm
        ElseIf matomeflg = True Then
            'まとめ処理の場合はまとめ先データセットから取得
            inkakanrino = ds.tables("lmh010_matomesaki").rows(0)("inka_no_l").tostring()
            dr("free_c30") = String.concat("05-", ds.tables("lmh010_matomesaki").rows(0)("edi_ctl_no").tostring())
        Else
            '入荷登録時は入荷管理番号(大)をマスタから取得
            Dim num As New numbermasterutility
            inkakanrino = num.getautocode(numbermasterutility.numberkbn.inka_no_l, Me, nrsbrcd)
        End If

        '入荷管理番号(大)をedi入荷(大)に格納
        dr("inka_ctl_no_l") = inkakanrino

        '入荷管理番号(大)をedi入荷(中)に格納
        For i As Integer = 0 To max
            dt.rows(i)("inka_ctl_no_l") = inkakanrino
        Next

        Return ds

    End Function

#End Region ' "入荷管理番号(大)取得"

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

#End Region ' "入荷管理番号(中)取得"

#Region "まとめ先複数件の時入荷管理番号取得"

    ''' <summary>
    ''' まとめ先入荷管理番号の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function matomesakiInkaNo(ByVal ds As DataSet) As String

        Dim max As Integer = ds.Tables("LMH010_MATOMESAKI").Rows.Count - 1
        Dim concatInkaNo As String = String.Empty
        Dim matomeInkaNo As String = String.Empty

        For i As Integer = 0 To max

            'まとめ先出荷管理番号の取得
            matomeInkaNo = ds.Tables("LMH010_MATOMESAKI").Rows(i)("INKA_NO_L").ToString
            If i = 0 Then
                concatInkaNo = matomeInkaNo
            ElseIf i > 0 Then
                concatInkaNo = String.Concat(concatInkaNo, ",", matomeInkaNo)
            End If

        Next

        Return concatInkaNo

    End Function

#End Region ' "まとめ先複数件の時入荷管理番号取得"


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
        For i As Integer = 0 To max
            ediMNb = ediMNb + Convert.ToInt64(ediM.Rows(i)("NB"))
        Next

        If matomeFlg = False Then
            '通常入荷登録
            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")
            inkaDr("INKA_TP") = ediDr("INKA_TP")
            inkaDr("INKA_KB") = ediDr("INKA_KB")
            inkaDr("INKA_STATE_KB") = "40"
            inkaDr("INKA_DATE") = ediDr("INKA_DATE")
            inkaDr("NRS_WH_CD") = ediDr("NRS_WH_CD")
            inkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
            inkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
            inkaDr("INKA_PLAN_QT") = ediDr("INKA_PLAN_QT")
            inkaDr("INKA_PLAN_QT_UT") = ediDr("INKA_PLAN_QT_UT")


            inkaDr("INKA_TTL_NB") = ediMNb
            inkaDr("BUYER_ORD_NO_L") = ediDr("BUYER_ORD_NO")
            inkaDr("OUTKA_FROM_ORD_NO_L") = ediDr("OUTKA_FROM_ORD_NO")
            inkaDr("TOUKI_HOKAN_YN") = FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
            inkaDr("HOKAN_STR_DATE") = ediDr("HOKAN_STR_DATE")
            inkaDr("HOKAN_YN") = FormatZero(ediDr("HOKAN_YN").ToString(), 2)
            inkaDr("HOKAN_FREE_KIKAN") = ediDr("HOKAN_FREE_KIKAN")
            inkaDr("NIYAKU_YN") = FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
            inkaDr("TAX_KB") = ediDr("TAX_KB")
            inkaDr("REMARK") = ediDr("REMARK")
            inkaDr("REMARK_OUT") = ediDr("NYUBAN_L")
            inkaDr("UNCHIN_TP") = ediDr("UNCHIN_TP")
            inkaDr("UNCHIN_KB") = ediDr("UNCHIN_KB")
            inkaDr("SYS_DEL_FLG") = "0"
        Else
            'まとめ処理
            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")

            Dim matomesakiDr As DataRow = ds.Tables("LMH010_MATOMESAKI").Rows(0)



            inkaDr("INKA_TTL_NB") = ediMNb + Convert.ToInt64(matomesakiDr("INKA_TTL_NB"))
            inkaDr("SYS_UPD_DATE") = matomesakiDr("SYS_UPD_DATE")
            inkaDr("SYS_UPD_TIME") = matomesakiDr("SYS_UPD_TIME")
        End If

        'データセットに設定
        ds.Tables("LMH010_B_INKA_L").Rows.Add(inkaDr)

        Return ds

    End Function

#End Region ' "データセット設定(入荷L)"

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

#End Region ' "データセット設定(入荷M)"

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
#If True Then   'ADD 2020/06/08 007999 
        Dim JJ_KBN_NM3 As String = ds.Tables("LMH010INOUT").Rows(0).Item("JJ_KBN_NM3").ToString()
#End If
        ' 在庫番号採番用
        Dim num As New NumberMasterUtility
        _ZaiRecNo = New Dictionary(Of Integer, String)

        For i As Integer = 0 To max

            inkaDr = ds.Tables("LMH010_B_INKA_S").NewRow()
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")
            inkaDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M")
            inkaDr("INKA_NO_S") = "001"
            inkaDr("ZAI_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, ediDr("NRS_BR_CD").ToString())
            _ZaiRecNo(i) = inkaDr("ZAI_REC_NO").ToString()
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
#If True Then   'ADD 2020/06/08 007999 
            inkaDr("GOODS_COND_KB_3") = JJ_KBN_NM3.ToString
#End If

            'データセットに設定
            ds.Tables("LMH010_B_INKA_S").Rows.Add(inkaDr)
        Next

        Return ds

    End Function

#End Region ' "データセット設定(入荷S)"

#Region "データセット設定(在庫TRS)"

    ''' <summary>
    ''' データセット設定(在庫TRS)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetZaiRec(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim zaiDr As DataRow
        Dim drL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1

        For i As Integer = 0 To max
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            '在庫TRS
            zaiDr = ds.Tables("LMH010_ZAI_TRS").NewRow()
            zaiDr("NRS_BR_CD") = ediDr("NRS_BR_CD").ToString()
            zaiDr("ZAI_REC_NO") = _ZaiRecNo(i)
            zaiDr("WH_CD") = drL("NRS_WH_CD").ToString
            zaiDr("TOU_NO") = ediDr("TOU_NO").ToString()
            zaiDr("SITU_NO") = ediDr("SITU_NO").ToString()
            zaiDr("ZONE_CD") = ediDr("ZONE_CD").ToString()
            zaiDr("LOCA") = String.Empty
            zaiDr("LOT_NO") = ediDr("LOT_NO").ToString()
            zaiDr("CUST_CD_L") = drL("CUST_CD_L").ToString()
            zaiDr("CUST_CD_M") = drL("CUST_CD_M").ToString()
            zaiDr("GOODS_KANRI_NO") = String.Empty
            zaiDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD").ToString()
            zaiDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L").ToString()
            zaiDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M").ToString()
            zaiDr("INKA_NO_S") = "001"
            zaiDr("ALLOC_PRIORITY") = "10"
            zaiDr("RSV_NO") = String.Empty
            zaiDr("SERIAL_NO") = ediDr("SERIAL_NO").ToString()
            zaiDr("HOKAN_YN") = FormatZero(drL("HOKAN_YN").ToString(), 2)
            zaiDr("TAX_KB") = drL("TAX_KB").ToString()
            zaiDr("GOODS_COND_KB_1") = String.Empty
            zaiDr("GOODS_COND_KB_2") = String.Empty
            zaiDr("GOODS_COND_KB_3") = String.Empty
            zaiDr("OFB_KB") = "01"
            zaiDr("SPD_KB") = "01"
            zaiDr("REMARK_OUT") = String.Empty
            zaiDr("PORA_ZAI_NB") = ediDr("NB").ToString()
            zaiDr("ALCTD_NB") = "0"
            zaiDr("ALLOC_CAN_NB") = ediDr("NB").ToString()
            zaiDr("IRIME") = ediDr("IRIME").ToString()
            Dim irime As Double = 0
            Dim inkaNb As Integer = 0
            Double.TryParse(ediDr("IRIME").ToString(), irime)
            Integer.TryParse(ediDr("NB").ToString(), inkaNb)
            zaiDr("PORA_ZAI_QT") = CStr(inkaNb * irime)
            zaiDr("ALCTD_QT") = "0"
            zaiDr("ALLOC_CAN_QT") = CStr(inkaNb * irime)
            zaiDr("INKO_DATE") = MyBase.GetSystemDate
            zaiDr("INKO_PLAN_DATE") = MyBase.GetSystemDate
            zaiDr("ZERO_FLAG") = String.Empty
            zaiDr("GOODS_CRT_DATE") = String.Empty
            zaiDr("DEST_CD_P") = String.Empty
            zaiDr("REMARK") = String.Empty
            zaiDr("SMPL_FLAG") = "00"
            zaiDr("SYS_DEL_FLG") = ediDr("SYS_DEL_FLG")

            'データセットに設定
            ds.Tables("LMH010_ZAI_TRS").Rows.Add(zaiDr)

        Next

        Return ds

    End Function

#End Region ' "データセット設定(在庫TRS)"

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

#End Region ' "データセット設定(受信明細)"

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

#End Region ' "データセット設定(作業)"

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
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso
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

#End Region ' "データセット設定(運送L)"

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

            If ediDr("TARE_YN").Equals("01") = False Then   ' 風袋加算フラグ

                unsoMDr("BETU_WT") = stdWtKgs * irime / stdIrimeNb

            Else
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb) + nisugata

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

#End Region ' "データセット設定(運送M)"

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

#End Region ' "データセット設定(運送L：運送重量)"

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

#End Region ' "データセット設定(タブレット項目の初期値設定)"

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
            If dt.Rows(i)("TARE_YN").Equals("01") Then  ' 風袋加算フラグON

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

#End Region ' "風袋重量の取得"

#End Region ' "入荷登録処理"

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

#End Region ' "紐付け処理"

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


#End Region ' "左埋処理"

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

#End Region ' "Method"

End Class
