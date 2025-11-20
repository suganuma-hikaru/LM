' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  551　　　 : 関塗工(春日部)
'  作  成  者       :  honmyo
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC551
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC551 = New LMH030DAC551()

    ''' <summary>
    ''' 使用するDACクラスの生成(共通DAC)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DacCom As LMH030DAC = New LMH030DAC()

    ''' <summary>
    ''' 使用するBLC共通クラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH030BLC = New LMH030BLC()

#End Region

#Region "関塗工用CONST"

    ''' <summary>
    ''' 関塗工
    ''' </summary>
    ''' <remarks></remarks>
    Public Const WH_CD_KTK As String = "009"                '倉庫コード
    Public Const CUST_CD_L_KTK As String = "10009"          '荷主コード（大）
    Public Const CUST_CD_M_KTK As String = "00"             '荷主コード（中）

    '要望番号:1209(出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
    'Public Const NRS_GOODS_CD_UNSO As String = "B0000999999999999999"           '商品コード（運送）
    Public Const NRS_GOODS_CD_UNSO As String = ""                               '商品コード（運送）空を送る
    '要望番号:1209(出荷EDI→運送登録仕様見直し②商品マスタの存在チェックは外す) 2012/06/28 本明 End
    Public Const IRIME_UNSO As String = "0"                 '入目値（運送）

    ''' <summary>
    ''' その他
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEF_CTL_NO As String = "B00000000"             '管理番号初期値
    Public Const DEF_UNSO_NO_L As String = "01-B00000000"       '運送番号初期値
    Public Const DEF_UNSO_NO_M As String = "01-B00000000000"    '運送番号初期値

#End Region

#Region "Method"

#Region "運送登録処理"
    ''' <summary>
    ''' 運送登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaToroku(ByVal ds As DataSet) As DataSet

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim ediCustIdx As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX").ToString()
        '2012.03.25 大阪対応START
        Dim unsoFlg As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_UNSOFLG").ToString()
        '2012.03.25 大阪対応END

        'EDI出荷(大)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(大)の初期値設定
        ds = Me.SetEdiLShoki(ds)

        'EDI出荷(中)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(大)の初期値設定後の関連チェック
        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'EDI出荷(大)の初期値設定後のDB存在チェック
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        '届先コードの初期値設定
        ds = Me.SetDestCd(ds)

        'EDI出荷(中)の初期値設定後のマスタ存在チェック
        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'EDI出荷(中)の初期値設定後の関連チェック
        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        '出荷管理番号(大)の採番
        ds = Me.GetOutkaNoL(ds)

        ''出荷管理番号(中)の採番
        ds = Me.GetOutkaNoM(ds)


        '注意)運送登録なので、出荷は作成しないがデータセット設定は行う
        '理由)後続の運送設定の際に、出荷(大)で設定された運送個数を使用したい為
        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds)

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds)

        '運送(大,中)データセット設定
        ds = Me.SetDatasetUnsoL(ds)
        ds = Me.SetDatasetUnsoM(ds)

        '運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds)

        '運送登録(通常処理)
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        '届先マスタの自動追加
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
               AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If

        '届先マスタの自動更新
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
           AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_UPDATE_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "UpdateMDestData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
            ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "0"
        End If

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
        End If

        Return ds

    End Function
#End Region

#Region "EDI_Lの初期値設定(運送登録処理)"
    ''' <summary>
    ''' EDI_Lの初期設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>各マスタ値を取得しEDI_Lの初期設定をする</remarks>
    Private Function SetEdiLShoki(ByVal ds As DataSet) As DataSet

        '荷主M取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectMcustOutkaToroku", ds)

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim mCustDr As DataRow = ds.Tables("LMH030_M_CUST").Rows(0)
        Dim mDestDr As DataRow = Nothing
        Dim mDestFlgYN As Boolean = False      '届先マスタ有無フラグ

        '届先M取得
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse _
            String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString().Trim()) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
        End If

        If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
            mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
            mDestFlgYN = True
        End If

        '出荷区分
        If String.IsNullOrEmpty(ediDr("OUTKA_KB").ToString().Trim()) = True Then
            ediDr("OUTKA_KB") = "10"
        End If

        '出荷種別区分
        If String.IsNullOrEmpty(ediDr("SYUBETU_KB").ToString().Trim()) = True Then
            ediDr("SYUBETU_KB") = "10"
        End If

        '出荷先国内・輸出
        If String.IsNullOrEmpty(ediDr("NAIGAI_KB").ToString().Trim()) = True Then
            ediDr("NAIGAI_KB") = "01"
        End If

        '作業進捗区分
        If String.IsNullOrEmpty(ediDr("OUTKA_STATE_KB").ToString().Trim()) = True Then
            ediDr("OUTKA_STATE_KB") = "10"
        End If

        '出荷報告有無
        If String.IsNullOrEmpty(ediDr("OUTKAHOKOKU_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(mCustDr("OUTKA_RPT_YN").ToString().Trim()) = False Then
                ediDr("OUTKAHOKOKU_YN") = Right(mCustDr("OUTKA_RPT_YN").ToString().Trim(), 1)
            Else
                ediDr("OUTKAHOKOKU_YN") = "0"
            End If
        End If

        'ピッキングリスト区分
        If String.IsNullOrEmpty(ediDr("PICK_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("PICK_KB") = mDestDr("PICK_KB").ToString().Trim()
            Else
                ediDr("PICK_KB") = "01"
            End If
        End If

        '出庫日
        If String.IsNullOrEmpty(ediDr("OUTKO_DATE").ToString().Trim()) = True Then
            ediDr("OUTKO_DATE") = ediDr("OUTKA_PLAN_DATE")
        End If

        '当期保管料負担有無
        If String.IsNullOrEmpty(ediDr("TOUKI_HOKAN_YN").ToString().Trim()) = True Then
            ediDr("TOUKI_HOKAN_YN") = "1"
        End If

        '不具合暫定対応 START
        '荷主名(大)
        If String.IsNullOrEmpty(ediDr("CUST_NM_L").ToString().Trim()) = True Then
            ediDr("CUST_NM_L") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_L").ToString()
        End If

        '荷主名(中)
        If String.IsNullOrEmpty(ediDr("CUST_NM_M").ToString().Trim()) = True Then
            ediDr("CUST_NM_M") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_M").ToString()
        End If
        '不具合暫定対応 END

        '荷送人名(大)
        If String.IsNullOrEmpty(ediDr("SHIP_CD_L").ToString().Trim()) = True Then
            ediDr("SHIP_NM_L") = ""
        Else
            'DACで値セットを行う
            '2012.02.25 大阪対応 START
            'ds = MyBase.CallDAC(Me._DacCom, "SetShipNmL", ds)
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdestShip", ds)
            If ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count <> 0 Then
                ediDr("SHIP_NM_L") = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("DEST_NM").ToString().Trim()
            End If
            '2012.02.25 大阪対応 END
        End If

        '指定納品書区分
        If String.IsNullOrEmpty(ediDr("SP_NHS_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("SP_NHS_KB") = mDestDr("SP_NHS_KB").ToString().Trim()
            End If
        End If

        '追加箇所 20120222
        '分析票添付区分
        If String.IsNullOrEmpty(ediDr("COA_YN").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("COA_YN") = mDestDr("COA_YN").ToString().Trim().Substring(1, 1)
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 Start
            Else
                '届先マスタに存在しない場合、自動追加の値と同値をセットする
                ediDr("COA_YN") = "0"  'SetInsMDestFromDestの値と一致させる事！（荷主により値が異なるため）
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 End
            End If
        End If
        '追加箇所 20120222

        '運送手配区分
        If String.IsNullOrEmpty(ediDr("UNSO_MOTO_KB").ToString().Trim()) = True Then
            ediDr("UNSO_MOTO_KB") = mCustDr("UNSO_TEHAI_KB").ToString().Trim()
        End If

        '便区分
        If String.IsNullOrEmpty(ediDr("BIN_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("BIN_KB").ToString().Trim()) = False Then
                    ediDr("BIN_KB") = mDestDr("BIN_KB")
                Else
                    ediDr("BIN_KB") = "01"
                End If
            Else
                ediDr("BIN_KB") = "01"
            End If
        End If

        '運送会社コード
        '運送会社支店コード
        '空の場合は届先マスタの値を設定、届先Mが空の場合は荷主マスタの値を設定
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = True AndAlso _
           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = True Then

            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("SP_UNSO_CD").ToString().Trim()) = False Then
                    ediDr("UNSO_CD") = mDestDr("SP_UNSO_CD").ToString().Trim()
                    ediDr("UNSO_BR_CD") = mDestDr("SP_UNSO_BR_CD").ToString().Trim()
                Else
                    ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                    ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
                End If
            Else
                ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
            End If

        End If

        'タリフ分類区分
        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        '割増タリフコード(割増運賃タリフマスタ)
        'DACで値セットを行う
        '2012.03.06 大阪対応 START
        '(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        '①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)
        'タリフセットマスタの取得(運賃タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)
        '2012.03.06 大阪対応 END

        '2012.03.06 大阪対応 START
        'タリフセットマスタの取得(割増タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)
        '2012.03.06 大阪対応 END


        '配送時注意事項
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
        Else
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False AndAlso _
                   String.IsNullOrEmpty(ediDr("UNSO_ATT").ToString().Trim()) = True Then

                    ediDr("UNSO_ATT") = mDestDr("DELI_ATT").ToString().Trim()
                Else
                    If InStr(ediDr("UNSO_ATT").ToString().Trim(), mDestDr("DELI_ATT").ToString().Trim()) > 0 Then
                    Else
                        '2012.03.02 大阪対応START
                        ediDr("UNSO_ATT") = Me._Blc.LeftB(String.Concat(ediDr("UNSO_ATT").ToString() & Strings.Space(2), mDestDr("DELI_ATT").ToString().Trim()), 100)
                        '2012.03.02 大阪対応END
                    End If
                End If

            End If

        End If

        '送り状作成有無
        If String.IsNullOrEmpty(ediDr("DENP_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso _
                String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = False Then
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                ''運送会社マスタの存在チェック
                'ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoco", ds)
                '運送会社荷主別送り状マスタの存在チェック
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoCustRpt", ds)
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                If MyBase.GetResultCount = 0 Then
                    ediDr("DENP_YN") = "0"
                Else
                    ediDr("DENP_YN") = "1"
                End If
            Else
                ediDr("DENP_YN") = "0"
            End If

        End If

        '元着払区分
        If String.IsNullOrEmpty(ediDr("PC_KB").ToString().Trim()) = True Then
            ediDr("PC_KB") = "01"
        End If

        '追加箇所 20120222
        '運賃請求有無
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse _
           (ediDr("UNSO_MOTO_KB").ToString()).Equals("40") = True Then
            ediDr("UNCHIN_YN") = "1"
        Else
            ediDr("UNCHIN_YN") = "0"
        End If
        '追加箇所 20120222

        '荷役料有無
        If String.IsNullOrEmpty(ediDr("NIYAKU_YN").ToString().Trim()) = True Then
            ediDr("NIYAKU_YN") = "1"
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 届先コード設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>EDIデータの届先コードが空の場合、届先マスタの値を設定する
    ''' この設定はDB存在チェック後に行う</remarks>
    Private Function SetDestCd(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        '届先コード
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                '2012.04.04 要望番号943 修正START
                ediDr("DEST_CD") = ds.Tables("LMH030_M_DEST").Rows(0)("DEST_CD").ToString().Trim()
                '2012.04.04 要望番号943 修正END
            End If
        End If

        Return ds

    End Function

#End Region

#Region "EDI出荷(中)の初期値設定"

    ''' <summary>
    ''' EDI出荷(中)の初期値設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function EdiMDefaultSet(ByVal ds As DataSet, ByVal setDs As DataSet, _
                                    ByVal count As Integer, ByVal unsodata As String, _
                                    ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)
        Dim compareWarningFlg As Boolean = False

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)

        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then

            '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
            'ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
            'mGoodsDr("COA_YN")が２文字の場合のみSubstring(1, 1)を行う（SubstringでAbendするため）
            If Len(mGoodsDr("COA_YN").ToString()) = 2 Then
                ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
            Else
                ediMDr("COA_YN") = String.Empty
            End If
            '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し②商品マスタの存在チェックは外す) 2012/06/28 本明 End

        End If

            '荷主注文番号(明細単位)
            If String.IsNullOrEmpty(ediMDr("CUST_ORD_NO_DTL").ToString()) = True Then
                ediMDr("CUST_ORD_NO_DTL") = ediLDr("CUST_ORD_NO")
            End If

            '買主注文番号(明細単位)
            If String.IsNullOrEmpty(ediMDr("BUYER_ORD_NO_DTL").ToString()) = True Then
                ediMDr("BUYER_ORD_NO_DTL") = ediLDr("BUYER_ORD_NO")
            End If

            '商品KEY
            ediMDr("NRS_GOODS_CD") = mGoodsDr("GOODS_CD_NRS")

            '商品名
            If unsodata.Equals("01") = False Then
                ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1")
            End If

            '引当単位区分
            If String.IsNullOrEmpty(ediMDr("ALCTD_KB").ToString()) = True Then
                If String.IsNullOrEmpty(mGoodsDr("ALCTD_KB").ToString()) = False Then

                    ediMDr("ALCTD_KB") = mGoodsDr("ALCTD_KB")
                Else
                    ediMDr("ALCTD_KB") = "01"
                End If
            End If

            '2012.03.20 JC大秦化工(注意事項)
            '関塗工(運送データ)の場合以下の４項目は値の入れ替えはしない
            '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
            '個数単位
            '数量単位
            '包装個数
            '包装単位
            '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

            '温度区分
            ediMDr("ONDO_KB") = mGoodsDr("ONDO_KB")

            '運送温度区分
            If String.IsNullOrEmpty(ediMDr("UNSO_ONDO_KB").ToString()) = True Then

                If (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) < (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) _
                AndAlso ((ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4)) < (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) _
                OrElse (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) < (ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4))) Then
                    ediMDr("UNSO_ONDO_KB") = "90"
                Else
                    ediMDr("UNSO_ONDO_KB") = mGoodsDr("UNSO_ONDO_KB")
                End If

                If (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) < (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) _
                AndAlso ((ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4)) < (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) _
                OrElse (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) < (ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4))) Then
                    ediMDr("UNSO_ONDO_KB") = "90"
                Else
                    ediMDr("UNSO_ONDO_KB") = mGoodsDr("UNSO_ONDO_KB")
                End If
            Else
                '運送温度区分(区分マスタ)
                drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U006
                drJudge("KBN_CD") = ediMDr("UNSO_ONDO_KB")
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

                If MyBase.GetResultCount = 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送温度区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If

            End If


            If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
            AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then
                ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
            End If

            '入目単位
            If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then
                ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
            Else
                If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                    '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合はエラー
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If
            End If

            '出荷包装個数
            '出荷端数
            Dim pkgNb As Double = Convert.ToDouble(ediMDr("PKG_NB"))
            Dim outkaPkgNb As Double = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
            Dim outkaHasu As Double = Convert.ToDouble(ediMDr("OUTKA_HASU"))
            Dim alctdKb As String = ediMDr("ALCTD_KB").ToString
            Dim irime As Double = Convert.ToDouble(ediMDr("IRIME"))
            Dim outkaTtlQt As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_QT"))

            Select Case alctdKb

                Case "01"
                    If 1 < pkgNb Then

                        '2012.06.18 修正START
                        ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                        ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                        ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime
                        '2012.06.18 修正END

                    Else

                        '2012.06.18 修正START
                        '運送EDIの場合、商品マスタに存在する汎用商品は入数１なので、PKG_NBは計算に含めない
                        ediMDr("OUTKA_PKG_NB") = outkaPkgNb + outkaHasu
                        ediMDr("OUTKA_HASU") = 0
                        ediMDr("OUTKA_TTL_QT") = (outkaPkgNb + outkaHasu) * irime
                        '2012.06.18 修正END

                    End If

                    'If 1 < pkgNb Then
                    '    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                    '    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                    'Else
                    '    ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                    '    ediMDr("OUTKA_HASU") = 0
                    'End If

                    'ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime

                Case "02"
                    ediMDr("OUTKA_PKG_NB") = 0
                    If outkaTtlQt Mod irime = 0 Then
                        ediMDr("OUTKA_HASU") = outkaTtlQt / irime
                    Else
                        ediMDr("OUTKA_HASU") = Math.Floor(outkaTtlQt / irime) + 1
                    End If

                    ediMDr("OUTKA_TTL_NB") = ediMDr("OUTKA_HASU")

                Case "03"
                    ediMDr("OUTKA_PKG_NB") = 0
                    ediMDr("OUTKA_HASU") = 0
                    ediMDr("OUTKA_TTL_NB") = 0

                Case Else

            End Select

            '出荷数量
            ediMDr("OUTKA_QT") = ediMDr("OUTKA_TTL_QT")

            '個別重量(KGS)

            '要望番号:1209(出荷EDI→運送登録仕様見直し②重量がマイナスの場合はエラーとする) 2012/06/28 本明 Start
            Dim dBetuWt As Double = Convert.ToDouble(ediMDr("BETU_WT").ToString)
            If dBetuWt < 0 Then
                '重量がマイナスの場合はエラーとする
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E185", New String() {"個別重量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
            '要望番号:1209(出荷EDI→運送登録仕様見直し②重量がマイナスの場合はエラーとする) 2012/06/28 本明 End

            If unsodata.Equals("01") = False Then
                ediMDr("BETU_WT") = mGoodsDr("STD_WT_KGS")
            End If

            '出荷時加工作業区分1-5
            ediMDr("OUTKA_KAKO_SAGYO_KB_1") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_1")
            ediMDr("OUTKA_KAKO_SAGYO_KB_2") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_2")
            ediMDr("OUTKA_KAKO_SAGYO_KB_3") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_3")
            ediMDr("OUTKA_KAKO_SAGYO_KB_4") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_4")
            ediMDr("OUTKA_KAKO_SAGYO_KB_5") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_5")

            'ワーニングが存在する場合はここでの判定はFalseで返す
            '(関塗工は現状ワーニング設定なし)
            If compareWarningFlg = True Then
                Return False
            End If

            Return True

    End Function

#End Region

#Region "入力チェック(運送登録処理)"

#Region "EDI出荷(大)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------

        '出荷管理番号
        If Me._Blc.OutkaCtlNoCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "出荷管理番号"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷報告有無
        If Me._Blc.OutkaHokokuYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"出荷報告有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷予定日
        If Me._Blc.OutkaPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出庫日
        If Me._Blc.OutkoDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷予定日+出庫日
        If Me._Blc.OutkaPlanLargeSmallCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E166", New String() {"出荷予定日", "出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '納入予定日
        If Me._Blc.arrPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"納入予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷報告日
        If Me._Blc.HokokuDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷報告日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主コード(大)
        If Me._Blc.CustCdLCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主コード(中)
        If Me._Blc.CustCdMCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '送り状作成有無
        If Me._Blc.DenpYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"送り状作成有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "EDI出荷(大)のDAC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLDbExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------
        ''オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then

            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
                Call MyBase.CallDAC(Me._DacCom, "SelectOrderCheckData", ds)
                If MyBase.GetResultCount > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If

            End If

        End If

        '出荷区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S014
        drJudge("KBN_CD") = drEdiL("OUTKA_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷種別区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S020
        drJudge("KBN_CD") = drEdiL("SYUBETU_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷種別区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '作業進捗区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S010
        drJudge("KBN_CD") = drEdiL("OUTKA_STATE_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"作業進捗区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        'ピッキングリスト区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_P001
        drJudge("KBN_CD") = drEdiL("PICK_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"ピッキングリスト区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '倉庫コード(倉庫マスタ)
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '納入予定時刻(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N010
        drJudge("KBN_CD") = drEdiL("ARR_PLAN_TIME")

        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"納入予定時刻", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '荷主コード(荷主マスタ)
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataMcust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送元区分(区分マスタ) 注)値は運送手配区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
        drJudge("KBN_CD") = drEdiL("UNSO_MOTO_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送手配区分(区分マスタ) 注)値はタリフ分類区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
        drJudge("KBN_CD") = drEdiL("UNSO_TEHAI_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '車輌区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
        drJudge("KBN_CD") = drEdiL("SYARYO_KB")
        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"車輌区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '便区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U001
        drJudge("KBN_CD") = drEdiL("BIN_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"便区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送会社コード
        If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = False OrElse String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = False Then

            If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = True Then
                drEdiL("UNSO_CD") = String.Empty
            End If

            If String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = True Then
                drEdiL("UNSO_BR_CD") = String.Empty
            End If

            Call MyBase.CallDAC(Me._DacCom, "SelectDataUnsoco", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送会社コード", "運送会社マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        Dim unchinTariffCd As String = String.Empty
        unchinTariffCd = drEdiL("UNCHIN_TARIFF_CD").ToString()
        Dim unsoTehaiKb As String = String.Empty
        unsoTehaiKb = drEdiL("UNSO_TEHAI_KB").ToString()

        If String.IsNullOrEmpty(unchinTariffCd) = True Then

        Else

            If unsoTehaiKb.Equals("40") = True Then
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMyokoTariffHd", ds)
            Else
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMunchinTariff", ds)
            End If

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '割増運賃タリフコード(割増運賃タリフマスタ)
        Dim extcTariffCd As String = String.Empty
        extcTariffCd = drEdiL("EXTC_TARIFF_CD").ToString()
        If String.IsNullOrEmpty(extcTariffCd) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMextcUnchin", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '元着払い区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_M001
        drJudge("KBN_CD") = drEdiL("PC_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"元着払い区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '-------------------------------------------------------------------------------------
        '●荷主固有チェック(関塗工専用)
        '-------------------------------------------------------------------------------------
        Dim flgWarning As Boolean = False

        '届先マスタ存在チェック
        Dim destCd As String = drEdiL("DEST_CD").ToString()         '届先コード
        Dim ediDestCd As String = drEdiL("EDI_DEST_CD").ToString()  'EDI届先コード
        Dim workDestCd As String = String.Empty                     '検索する届先コード格納変数
        Dim workDestString As String = String.Empty                 '"届先コード"or"EDI届先コード"
        Dim dtMS As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")


        'DEST_CDが空の場合、EDI_DEST_CDを使う
        If String.IsNullOrEmpty(destCd) = False Then
            workDestCd = destCd
            workDestString = "届先コード"
        ElseIf String.IsNullOrEmpty(ediDestCd) = False Then
            workDestCd = ediDestCd
            workDestString = "EDI届先コード"
        Else
        End If

        Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        If mDestCount = 1 Then
            '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェックとZIPコードのマスタ存在チェック
            If Me.DestCompareCheck(ds, rowNo, ediCtlNo) = False Then

                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    '整合性チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    '整合性チェックでワーニングがあった場合は、flgWarning=True
                    flgWarning = True
                End If
            End If

        ElseIf mDestCount = 0 Then
            '0件の場合、ZIPコードのマスタ存在チェックを行い、届先マスタの更新をする
            'JISマスタに存在しない場合、エラー
            'JISマスタに存在するが、JISが空の場合、ワーニング
            If Me.ZipCompareCheck(ds, rowNo, ediCtlNo, workDestCd, workDestString) = False Then
                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    'チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
                    flgWarning = True
                End If
            End If

        Else
            '複数件の場合、エラー
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If


        Return True

    End Function

#End Region

#Region "EDI出荷(中)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(中)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '引当単位区分
        If Me._Blc.AlctdKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"引当単位区分"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '温度区分 + 便区分
        If Me._Blc.OndoBinKbCheck(dtL, dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E352", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷端数
        If Me._Blc.OutkaHasuCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "運送登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '赤黒区分
        If Me._Blc.AkakuroKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "運送登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "EDI出荷(中)のDAC側でのチェック + 初期値設定"

    ''' <summary>
    ''' EDI出荷(中)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMMasterExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim max As Integer = dtM.Rows.Count - 1
        Dim unsoData As String = String.Empty
        Dim custGoodsCd As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        Dim dtGooDs As DataTable = setDs.Tables("LMH030_M_GOODS")

        Dim flgWarning As Boolean = False

        For i As Integer = 0 To max

            custGoodsCd = dtM.Rows(i)("CUST_GOODS_CD").ToString()

            If String.IsNullOrEmpty(dtL.Rows(0).Item("FREE_C30").ToString()) = False Then
                unsoData = dtL.Rows(0).Item("FREE_C30").ToString().Substring(0, 2)
            End If

            If String.IsNullOrEmpty(custGoodsCd) = False Then

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtL.ImportRow(dtL.Rows(0))
                setDtM.ImportRow(dtM.Rows(i))

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.KTK_WID_M001, 0)

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                '基本的に関塗工の場合は、汎用商品コードの商品キーが入ってくる為１件になる。
                If MyBase.GetResultCount = 0 Then

                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
                    '商品マスタが存在しない場合はダミーで商品データセットを作成する
                    setDs = Me._Blc.SetDummyGoodsM(ds, setDs, i)
                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 End

                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start（以下コメント化）
                    ''要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    ''MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    ''要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    'Return False
                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 End（以上コメント化）


                ElseIf GetResultCount() > 1 Then

                    '入目 + 荷主商品コードで再検索
                    setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsIrimeOutka", setDs))

                    If MyBase.GetResultCount = 1 Then
                    Else
                        '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                        '注意!!! セットメッセージは消してよいのか判断がつかないので調査する
                        'MyBase.SetMessage("W162")
                        msgArray(1) = String.Empty
                        msgArray(2) = String.Empty
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty

                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.KTK_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    '関塗工は現段階ではワーニングはないが、共通のロジックを組み込む為入れておく
                    Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                    If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                        '整合性チェックでエラーがあった場合は処理終了
                        Return False
                    Else
                        '整合性チェックでワーニングがあった場合は、flgWarning=True
                        flgWarning = True
                    End If

                End If

                '運送重量取得用項目をデータセット(EDI出荷(中))に格納
                If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then
                    Return False
                End If

            Else
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        Next

        '----------------------------------------------------------------------------------------------------------
        'ワーニングがある場合はマスタから商品が選択できていない為、処理をつづけるとデータによってはアベンドする。
        'そのため中データのループが終わり、ワーニングがある（flgWarning=True）場合は処理を終了させる
        '-----------------------------------------------------------------------------------------------------------
        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If

        Return True

    End Function

#End Region

#Region "届先マスタチェック(関塗工)"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMZip As DataTable = ds.Tables("LMH030_M_ZIP")

        Dim mSysDelF As String = dtMdest.Rows(0).Item("SYS_DEL_FLG").ToString()
        Dim mDestNm As String = dtMdest.Rows(0).Item("DEST_NM").ToString()
        Dim mAd1 As String = dtMdest.Rows(0).Item("AD_1").ToString()
        Dim mAd2 As String = dtMdest.Rows(0).Item("AD_2").ToString()
        Dim mAd3 As String = dtMdest.Rows(0).Item("AD_3").ToString()
        Dim mZip As String = dtMdest.Rows(0).Item("ZIP").ToString()
        Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()
        Dim mAdAll As String = String.Concat(mAd1, mAd2, mAd3)
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediDestCd As String = dtEdi.Rows(0)("DEST_CD").ToString()
        Dim ediDestNm As String = dtEdi.Rows(0)("DEST_NM").ToString()
        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediTel As String = dtEdi.Rows(0)("DEST_TEL").ToString()
        Dim ediADD As String = dtEdi.Rows(0)("DEST_AD_1").ToString()
        Dim ediFreeC21 As String = dtEdi.Rows(0)("FREE_C21").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '現LMSでチェックコメントアウトの為コメント化
        mDestNm = Me.SpaceCutChk(mDestNm)
        ediDestNm = Me.SpaceCutChk(ediDestNm)

        '届先名称(マスタ値が完全一致でなければワーニング)
        If mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L008, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.KTK_WID_L008, ds, msgArray, ediDestNm, mDestNm)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        '現LMSでチェックコメントアウトの為コメント化
        'FREE_C21:届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediFreeC21) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediFreeC21 = SpaceCutChk(ediFreeC21)
            If mAdAll.Equals(ediFreeC21) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L009, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.KTK_WID_L009, ds, msgArray, ediFreeC21, mAdAll)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
                    dtMdest.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
                    dtMdest.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If
            End If

        End If

        '届先郵便番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediZip) = True Then
            'チェックなし
        Else
            If mZip.Equals(Replace(ediZip, "-", String.Empty)) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L001, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先郵便番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "郵便番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.KTK_WID_L001, ds, msgArray, ediZip, mZip)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If


        '届先電話番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediTel) = True Then
            'チェックなし
        Else
            If mTel.Equals(ediTel) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L002, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.KTK_WID_L002, ds, msgArray, ediTel, mTel)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '2012.03.28 要望番号948 修正START
        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先JISコード", "JISマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "郵便番号マスタ"

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し運送登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If
            '2012.03.01 大阪対応END
        End If

        'Else
        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先住所１＋届先住所２＋届先住所３", "JISマスタ", "県＋市"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "JISマスタ"

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し運送登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If
            '2012.03.01 大阪対応END

        End If
        '2012.03.28 要望番号948 修正END


        If String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = False Then
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードに値がある場合、更新ワーニング
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L003, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
                ds = Me._Blc.SetComWarningL("W197", LMH030BLC.KTK_WID_L003, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = ediDestJisCd
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If


        ElseIf String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = True Then
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードが空の場合、処理続行確認
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L004, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                ds = Me._Blc.SetComWarningL("W188", LMH030BLC.KTK_WID_L004, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

            End If

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = False AndAlso ediDestJisCd.Equals(mJis) = False Then
            'EDIのJISコードが空でなくEDIのJISコードと届先マスタのJISコードに差異がある場合
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L005, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W187", LMH030BLC.KTK_WID_L005, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = ediDestJisCd
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            ElseIf choiceKb.Equals("02") = True Then
                'ワーニングで"いいえ"を選択時
            End If

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = True AndAlso mZipJis.Equals(mJis) = False Then
            'EDIのJISコードが空でJISマスタ(郵便番号マスタ)のJISコードと届先マスタのJISコードに差異がある場合
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L006, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = String.Concat(warningString, "から取得したJISコード")
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = warningString
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W187", LMH030BLC.KTK_WID_L006, ds, msgArray, mZipJis, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = mZipJis
                dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis '追加箇所 20110222
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            ElseIf choiceKb.Equals("02") = True Then
                'ワーニングで"いいえ"を選択時
            End If

        End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        '関塗工共通入替え項目
        '届先JISコード
        If String.IsNullOrEmpty(ediDestJisCd) = True Then
            dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis
        End If

        Return True

    End Function

#End Region

#Region "SPACE除去 + 文字変換"
    ''' <summary>
    ''' SPACE除去 + 文字変換
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SpaceCutChk(ByVal chkFld As String) As String

        chkFld = Replace(Trim(chkFld), Space(1), String.Empty)
        chkFld = Replace(chkFld, "　", String.Empty)
        chkFld = StrConv(chkFld, VbStrConv.Wide)

        Return chkFld

    End Function

#End Region


#Region "届先マスタ追加時チェック"
    ''' <summary>
    ''' 届先マスタ追加時チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ZipCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, ByVal workDestCd As String, ByVal workDestString As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMJis As DataTable = ds.Tables("LMH030_M_JIS")
        Dim drEdiL As DataRow = dtEdi.Rows(0)

        'Dim mZip As String = dtMdest.Rows(0).Item("ZIP").ToString()
        'Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        'Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '2012.03.28 要望番号948 修正START
        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先JISコード", "JISマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "郵便番号マスタ"

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し運送登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If
            '2012.03.01 大阪対応END
        End If

        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then
            'Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先住所１＋届先住所２＋届先住所３", "JISマスタ", "県＋市"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "JISマスタ"

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し運送登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If
            '2012.03.01 大阪対応END

        End If
        '2012.03.28 要望番号948 修正END

        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L007, 0)

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            If String.IsNullOrEmpty(mZipJis) = False Then
                msgArray(4) = String.Empty
            Else
                msgArray(4) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
            End If


            ds = Me._Blc.SetComWarningL("W186", LMH030BLC.KTK_WID_L007, ds, msgArray, workDestCd, String.Empty) '追加箇所 20110222

            compareWarningFlg = True
            'Return True

        ElseIf choiceKb.Equals("01") = True Then
            'ワーニングで"はい"を選択時
            Dim drMD As DataRow = dtMdest.NewRow()
            drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMD("DEST_CD") = workDestCd
            drMD("EDI_CD") = workDestCd
            If String.IsNullOrEmpty(drEdiL("DEST_NM").ToString()) = False Then
                drMD("DEST_NM") = drEdiL("DEST_NM").ToString()
            End If
            drMD("ZIP") = Replace(drEdiL("DEST_ZIP").ToString(), "-", String.Empty)
            drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
            drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
            drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
            drMD("COA_YN") = "00"
            drMD("TEL") = drEdiL("DEST_TEL").ToString()
            drMD("JIS") = mZipJis
            drMD("PICK_KB") = "01"
            drMD("BIN_KB") = "01"
            'マスタ自動追加対象フラグ
            drMD("MST_INSERT_FLG") = "1"
            dtMdest.Rows.Add(drMD)

        End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "ワーニング処理(EDI(大)届先)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestWarningChoiceKb(ByVal setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(count)("EDI_CTL_NO").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) Then
                'ワーニング処理設定の値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()
                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region

#Region "ワーニング処理(EDI(中)商品)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetGoodsWarningChoiceKb(ByRef setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(0)("EDI_CTL_NO").ToString()
        Dim ediCtlNoM As String = setDt.Rows(count)("EDI_CTL_NO_CHU").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty
        Dim mstFlg As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) _
                                                                AndAlso ediCtlNoM.Equals(dr("EDI_CTL_NO_M")) Then
                'ワーニング画面の処理区分値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()

                mstFlg = warningId.Substring(7, 1)

                Select Case mstFlg
                    Case "1"
                        'ワーニング処理設定の値を反映
                        setDt.Rows(0).Item("NRS_GOODS_CD") = dr.Item("MST_VALUE")
                    Case Else

                End Select

                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region

#End Region

#Region "データセット設定"

#Region "データセット設定(運送番号)"

    ''' <summary>
    ''' データセット設定(運送番号)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoL(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Dim max As Integer = dt.Rows.Count - 1

        '運送登録の場合
        Dim num As New NumberMasterUtility
        dr("FREE_C30") = String.Concat("01-", num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd))

        Return ds

    End Function

#End Region

#End Region

#Region "データセット設定(出荷管理番号M)"
    ''' <summary>
    ''' 出荷管理番号(中)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoM(ByVal ds As DataSet) As DataSet

        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim nrsBrCd As String = dtEdiM.Rows(0).ToString
        Dim max As Integer = dtEdiM.Rows.Count - 1

        Dim unsoNoL As String = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C30").ToString()
        Dim unsoNoM As String = String.Empty

        '運送登録の場合
        For i As Integer = 0 To max
            unsoNoM = (i + 1).ToString("000")
            dtEdiM.Rows(i)("FREE_C30") = String.Concat(unsoNoL, unsoNoM)
        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷L)"
    ''' <summary>
    ''' データセット設定(出荷L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim outkaDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").NewRow()
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

            '通常登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_KB") = ediDr("OUTKA_KB")
            outkaDr("SYUBETU_KB") = ediDr("SYUBETU_KB")
            outkaDr("OUTKA_STATE_KB") = ediDr("OUTKA_STATE_KB")
            outkaDr("OUTKAHOKOKU_YN") = Me._Blc.FormatZero(ediDr("OUTKAHOKOKU_YN").ToString(), 2)
            outkaDr("PICK_KB") = ediDr("PICK_KB")
            outkaDr("DENP_NO") = String.Empty
            outkaDr("ARR_KANRYO_INFO") = String.Empty
            outkaDr("WH_CD") = ediDr("WH_CD")
            outkaDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
            outkaDr("OUTKO_DATE") = ediDr("OUTKO_DATE")
            outkaDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
            outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
            outkaDr("HOKOKU_DATE") = ediDr("HOKOKU_DATE")
            outkaDr("TOUKI_HOKAN_YN") = Me._Blc.FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
            outkaDr("END_DATE") = String.Empty
            outkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
            outkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
            outkaDr("SHIP_CD_L") = ediDr("SHIP_CD_L")
            outkaDr("SHIP_CD_M") = String.Empty

            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                Dim destMDr As DataRow = ds.Tables("LMH030_M_DEST").Rows(0)
                outkaDr("DEST_CD") = destMDr("DEST_CD")
                outkaDr("DEST_AD_3") = destMDr("AD_3")
                outkaDr("DEST_TEL") = destMDr("TEL")
            Else
                outkaDr("DEST_CD") = ediDr("DEST_CD")
                outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
                outkaDr("DEST_TEL") = ediDr("DEST_TEL")
            End If

            outkaDr("NHS_REMARK") = String.Empty
            outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
            outkaDr("COA_YN") = Me._Blc.FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
            outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")
            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
            outkaDr("DENP_YN") = Me._Blc.FormatZero(ediDr("DENP_YN").ToString(), 2)
            outkaDr("PC_KB") = ediDr("PC_KB")
            outkaDr("UNCHIN_YN") = Me._Blc.FormatZero(ediDr("UNCHIN_YN").ToString(), 2)
            outkaDr("NIYAKU_YN") = Me._Blc.FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
            outkaDr("ALL_PRINT_FLAG") = "00"
            outkaDr("NIHUDA_FLAG") = "00"
            outkaDr("NHS_FLAG") = "00"
            outkaDr("DENP_FLAG") = "00"
            outkaDr("COA_FLAG") = "00"
            outkaDr("HOKOKU_FLAG") = "00"
            outkaDr("MATOME_PICK_FLAG") = "00"
            outkaDr("LAST_PRINT_DATE") = String.Empty
            outkaDr("LAST_PRINT_TIME") = String.Empty
            outkaDr("SASZ_USER") = String.Empty
            outkaDr("OUTKO_USER") = String.Empty
            outkaDr("KEN_USER") = String.Empty
            outkaDr("OUTKA_USER") = String.Empty
            outkaDr("HOU_USER") = String.Empty
            outkaDr("ORDER_TYPE") = String.Empty
            outkaDr("SYS_DEL_FLG") = "0"
            outkaDr("DEST_KB") = "02"
            outkaDr("DEST_NM") = ediDr("DEST_NM")
            outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
            outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        'データセットに設定
        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷包装個数)"
    Private Function SumPkgNb(ByVal dt As DataTable) As Double

        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Double = 0
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0

        For i As Integer = 0 To max

            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)

            End If

            sumNb = sumNb + Convert.ToDouble(dt.Rows(i)("OUTKA_PKG_NB"))

            If 0 = calcPkgModNb Then
            Else
                sumNb = sumNb + 1
            End If

        Next

        Return sumNb

    End Function
#End Region

#Region "データセット設定(出荷M)"
    ''' <summary>
    ''' データセット設定(出荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim outkaDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty
        Dim SetNo As String = String.Empty
        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim max As Integer = dt.Rows.Count - 1
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0


        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            outkaDr = ds.Tables("LMH030_C_OUTKA_M").NewRow()

            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)
            End If

            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediLDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_NO_M") = ediDr("OUTKA_CTL_NO_CHU")
            If ediDr("SET_KB").ToString = "2" Then
                outkaDr("EDI_SET_NO") = ediDr("FREE_C10")
            Else
                outkaDr("EDI_SET_NO") = String.Empty
            End If
            outkaDr("COA_YN") = Me._Blc.FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            outkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            outkaDr("RSV_NO") = ediDr("RSV_NO")
            outkaDr("LOT_NO") = ediDr("LOT_NO")
            outkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            outkaDr("ALCTD_KB") = ediDr("ALCTD_KB")
            outkaDr("OUTKA_PKG_NB") = ediDr("PKG_NB")
            outkaDr("OUTKA_HASU") = ediDr("OUTKA_HASU")
            outkaDr("OUTKA_QT") = ediDr("OUTKA_QT")
            outkaDr("OUTKA_TTL_NB") = ediDr("OUTKA_TTL_NB")
            outkaDr("OUTKA_TTL_QT") = ediDr("OUTKA_TTL_QT")
            outkaDr("ALCTD_NB") = 0
            outkaDr("ALCTD_QT") = 0
            outkaDr("BACKLOG_NB") = ediDr("OUTKA_TTL_NB")
            outkaDr("BACKLOG_QT") = ediDr("OUTKA_TTL_QT")
            outkaDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            outkaDr("IRIME") = ediDr("IRIME")
            outkaDr("IRIME_UT") = ediDr("IRIME_UT")

            If Convert.ToInt64(dt.Rows(i)("PKG_NB")) = 0 Then
                outkaDr("OUTKA_M_PKG_NB") = 0
            Else
                If 0 = calcPkgModNb Then
                    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb
                Else
                    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb + 1
                End If
            End If

            If Convert.ToInt64(outkaDr("OUTKA_M_PKG_NB")) > 999 Then
                outkaDr("OUTKA_M_PKG_NB") = 1
            End If

            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("SIZE_KB") = String.Empty
            outkaDr("ZAIKO_KB") = String.Empty
            outkaDr("SOURCE_CD") = String.Empty
            outkaDr("YELLOW_CARD") = String.Empty
            outkaDr("PRINT_SORT") = "99"
            outkaDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH030_C_OUTKA_M").Rows.Add(outkaDr)

        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(EDI受信DTL)"
    ''' <summary>
    ''' データセット設定(EDI受信テーブル(DTL))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvDtl(ByVal ds As DataSet) As DataSet

        Dim rcvDr As DataRow
        Dim outkaedimDr As DataRow
        Dim outkaedilDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim outkaCtlNoChu As String = String.Empty

        For i As Integer = 0 To max

            outkaedimDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            rcvDr = ds.Tables("LMH030_EDI_RCV_DTL").NewRow()

            rcvDr("NRS_BR_CD") = outkaedilDr("NRS_BR_CD")
            rcvDr("EDI_CTL_NO") = outkaedilDr("EDI_CTL_NO")
            rcvDr("EDI_CTL_NO_CHU") = outkaedimDr("EDI_CTL_NO_CHU")
            rcvDr("OUTKA_CTL_NO") = outkaedimDr("OUTKA_CTL_NO")
            rcvDr("OUTKA_CTL_NO_CHU") = outkaedimDr("OUTKA_CTL_NO_CHU").ToString()
            rcvDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH030_EDI_RCV_DTL").Rows.Add(rcvDr)

        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(運送L)"
    ''' <summary>
    ''' データセット設定(運送L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim unsoDr As DataRow = ds.Tables("LMH030_UNSO_L").NewRow()
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim outkaLDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim ediMCntDr As DataRow

        Dim num As New NumberMasterUtility

        '通常登録
        unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")

        '関塗工の場合は運送登録なので、前の処理でFREE_C30で取得した運送番号を使用
        unsoDr("UNSO_NO_L") = ediDr("FREE_C30").ToString().Substring(3, 9)

        unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
        unsoDr("WH_CD") = ediDr("WH_CD")
        '関塗工の場合は運送登録なので、出荷管理番号は空
        unsoDr("INOUTKA_NO_L") = String.Empty
        unsoDr("TRIP_NO") = String.Empty
        unsoDr("UNSO_CD") = ediDr("UNSO_CD")
        unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
        unsoDr("BIN_KB") = ediDr("BIN_KB")
        unsoDr("JIYU_KB") = String.Empty

        '関塗工の場合は荷主注文番号をセット
        unsoDr("DENP_NO") = ediDr("CUST_ORD_NO")
        unsoDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
        unsoDr("OUTKA_PLAN_TIME") = String.Empty
        unsoDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
        unsoDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
        unsoDr("ARR_ACT_TIME") = String.Empty
        unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
        unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")

        Dim max As Integer = ds.Tables("LMH030_OUTKAEDI_M").Rows.Count - 1

        For i As Integer = 0 To max
            ediMCntDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            If InStr(unsoDr("CUST_REF_NO").ToString().Trim(), ediMCntDr("CUST_ORD_NO_DTL").ToString().Trim()) = 0 Then
                unsoDr("CUST_REF_NO") = Me._Blc.LeftB(Trim(String.Concat(unsoDr("CUST_REF_NO").ToString(), Space(2), ediMCntDr("CUST_ORD_NO_DTL").ToString())), 30)
            End If
        Next

        unsoDr("SHIP_CD") = ediDr("SHIP_CD_L")
        unsoDr("DEST_CD") = ediDr("DEST_CD")
        unsoDr("UNSO_PKG_NB") = outkaLDr("OUTKA_PKG_NB")
        'unsoDr("NB_UT") = ediDr("NB_UT") '運送Mで取得の為ここではコメント
        unsoDr("UNSO_WT") = 0             '運送Mの集計値
        unsoDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")
        '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 Start
        'unsoDr("PC_KB") = ediDr("PICK_KB")
        unsoDr("PC_KB") = ediDr("PC_KB")
        '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 End
        unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNSO_TEHAI_KB")
        unsoDr("VCLE_KB") = ediDr("SYARYO_KB")

        'ディック共同配送の場合は運送登録なので、元データ区分が変わる
        unsoDr("MOTO_DATA_KB") = "40"

        unsoDr("TAX_KB") = "01" '課税区分は"01"(課税)固定とする
        unsoDr("REMARK") = Me._Blc.LeftB(Trim(String.Concat(ediDr("REMARK").ToString(), Space(2), ediDr("UNSO_ATT").ToString())), 100)
        unsoDr("SEIQ_TARIFF_CD") = ediDr("UNCHIN_TARIFF_CD")
        unsoDr("SEIQ_ETARIFF_CD") = ediDr("EXTC_TARIFF_CD")
        unsoDr("AD_3") = outkaLDr("DEST_AD_3")

        unsoDr("UNSO_TEHAI_KB") = ediDr("UNSO_MOTO_KB")
        unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
        unsoDr("AREA_CD") = String.Empty
        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 Start
        'unsoDr("TYUKEI_HAISO_FLG") = String.Empty
        unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 End
        unsoDr("SYUKA_TYUKEI_CD") = String.Empty
        unsoDr("HAIKA_TYUKEI_CD") = String.Empty
        unsoDr("TRIP_NO_SYUKA") = String.Empty
        unsoDr("TRIP_NO_TYUKEI") = String.Empty
        unsoDr("TRIP_NO_HAIKA") = String.Empty


        'START UMANO 要望番号1302 支払運賃に伴う修正。
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso _
           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

            '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
            ds = MyBase.CallDAC(Me._DacCom, "SelectListDataShiharaiTariff", ds)
            Dim unsocoMDr As DataRow = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows(0)

            If MyBase.GetResultCount > 0 Then
                unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
            End If

        End If
        'END UMANO 要望番号1302 支払運賃に伴う修正。


        'データセットに設定
        ds.Tables("LMH030_UNSO_L").Rows.Add(unsoDr)

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
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)

        Dim stdWtKgs As Decimal = 0
        Dim irime As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim nisugata As Decimal = 0
        Dim outkaTtlNb As Decimal = 0

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            unsoMDr = ds.Tables("LMH030_UNSO_M").NewRow()

            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")

            '関塗工の場合は運送登録なので、出荷管理番号(中)が存在しないので採番する
            '運送登録処理の場合
            unsoMDr("UNSO_NO_M") = (i + 1).ToString("000")
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
            unsoMDr("NB_UT") = ediDr("KB_UT")
            unsoMDr("UNSO_TTL_QT") = ediDr("OUTKA_TTL_QT")
            unsoMDr("QT_UT") = ediDr("QT_UT")
            unsoMDr("HASU") = ediDr("OUTKA_HASU")
            unsoMDr("ZAI_REC_NO") = String.Empty

            '関塗工の場合は運送登録なので、運送温度区分が"90"の場合は入替
            If (ediDr("UNSO_ONDO_KB").ToString()).Equals("90") = True Then
                unsoMDr("UNSO_ONDO_KB") = ediDr("ONDO_KB")
            Else
                unsoMDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            End If

            unsoMDr("IRIME") = ediDr("IRIME")
            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
            irime = Convert.ToDecimal(ediDr("IRIME"))
            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If
            outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))

            '2012.06.19 関塗工　新旧不具合対応
            'If ediDr("TARE_YN").Equals("01") = False Then
            '    unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb)

            'Else
            '    unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata)

            'End If

            '運送EDI(関塗工)の場合、個別重量はEDI出荷(中)の個別重量をセット
            unsoMDr("BETU_WT") = ediDr("BETU_WT")
            '2012.06.19 関塗工　新旧不具合対応

            unsoMDr("SIZE_KB") = String.Empty
            unsoMDr("ZBUKA_CD") = String.Empty
            unsoMDr("ABUKA_CD") = String.Empty
            unsoMDr("PKG_NB") = ediDr("PKG_NB")
            unsoMDr("LOT_NO") = ediDr("LOT_NO")
            unsoMDr("REMARK") = ediDr("REMARK")

            'データセットに設定
            ds.Tables("LMH030_UNSO_M").Rows.Add(unsoMDr)
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

        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim unsoJyuryo As Decimal = 0
        Dim matomeUnsoJyuryo As Decimal = 0

        'まとめ(運送Mデータの運送重量合算)

        unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
        unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
        unsoLDr("NB_UT") = ediMDr("KB_UT")
        Return ds

    End Function

    ''' <summary>
    ''' 運送重量再計算処理(運送EDI荷主：関塗工の場合は入数は計算式に含まない)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCalcJyuryo(ByVal ds As DataSet, ByVal tblNm As String) As Decimal

        Dim unsoMDr As DataRow
        Dim unsoJyuryo As Decimal = 0
        Dim NB As Decimal = 0
        Dim max As Integer = ds.Tables(tblNm).Rows.Count - 1

        For i As Integer = 0 To max

            unsoMDr = ds.Tables(tblNm).Rows(i)

            '運送M個数の算出（梱数 * 入数 + 端数）
            '2012.06.13 修正START 関塗工の場合　取込時に入数が 0セットされるので、入数は計算式に含まない 
            'NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) * Convert.ToDecimal(unsoMDr("PKG_NB")) + Convert.ToDecimal(unsoMDr("HASU"))
            NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) + Convert.ToDecimal(unsoMDr("HASU"))
            '2012.06.13 修正END

            unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * NB + unsoJyuryo

        Next

        Return unsoJyuryo

    End Function
#End Region

#Region "データセット設定(EDI出荷M：運送重量必要項目)"
    ''' <summary>
    ''' データセット設定(EDI出荷M：運送重量必要項目)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiMUnsoJyuryo(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal count As Integer, _
                                              ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)
        Dim drJudge As DataRow

        '標準重量
        ediMDr("STD_WT_KGS") = mGoodsDr("STD_WT_KGS")
        '標準入目
        ediMDr("STD_IRIME_NB") = mGoodsDr("STD_IRIME_NB")
        '風袋加算フラグ
        ediMDr("TARE_YN") = mGoodsDr("TARE_YN")

        If String.IsNullOrEmpty(ediMDr("PKG_UT").ToString()) = False Then

            '荷姿(区分マスタ)
            drJudge = ds.Tables("LMH030_JUDGE").Rows(0)
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
            drJudge("KBN_CD") = ediMDr("PKG_UT")

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataPkgUtZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

            Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)
            '風袋重量
            ediMDr("NISUGATA") = zkbnDr("NISUGATA")

        End If

        Return True

    End Function

#End Region

#End Region

#Region "運送登録処理(運賃作成)"

    ''' <summary>
    ''' 運送登録処理(運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnchinSakusei(ByVal ds As DataSet) As DataSet

        '運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertUnchinData", ds)

        Return ds

    End Function

#End Region

#Region "Method"

#Region "データセット設定"

#Region "セミEDI時　データセット設定(EDI受信HED・DTL)"

    ''' <summary>
    ''' データセット設定(EDI受信HED・DTL)：セミEDI
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiRcv(ByVal ds As DataSet, ByVal i As Integer) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        'Dim drEdiRcvHed As DataRow = ds.Tables("LMH030_OUTKAEDI_HED_KTK").NewRow()
        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_OUTKAEDI_DTL_KTK").NewRow()
        Dim drSetDtl As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)

        'EDI受信HED設定
        'drEdiRcvHed("DEL_KB") = "0"                                                                 '削除区分
        'drEdiRcvHed("CRT_DATE") = MyBase.GetSystemDate()                                            'データ受信日
        'drEdiRcvHed("FILE_NAME") = drSetDtl("FILE_NAME_OPE")                                        '受信ファイル名
        'drEdiRcvHed("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")               '受信ファイル行数
        'drEdiRcvHed("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")                                       '営業所コード
        'drEdiRcvHed("EDI_CTL_NO") = String.Empty                                                    'ＥＤＩ管理番号
        'drEdiRcvHed("OUTKA_CTL_NO") = DEF_CTL_NO                                                    '出荷管理番号
        'drEdiRcvHed("CUST_CD_L") = String.Empty                                                     '荷主コード（大）
        'drEdiRcvHed("CUST_CD_M") = String.Empty                                                     '荷主コード（中）
        'drEdiRcvHed("PRTFLG") = "0"                                                                 'プリントフラグ
        'drEdiRcvHed("CANCEL_FLG") = String.Empty                                                    'キャンセルフラグ

        'drEdiRcvHed("HAKKO_KB_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 6)       '発行区分名
        'drEdiRcvHed("OUT_KB_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 8)         '出力区分名
        'drEdiRcvHed("JIGYO_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 3)          '事業所コード
        'drEdiRcvHed("JIGYO_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 40)         '事業所名
        'drEdiRcvHed("SOUKO_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 9)          '倉庫／仕入先コード
        'drEdiRcvHed("SOUKO_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 40)         '倉庫名／仕入先名
        'drEdiRcvHed("OUTKA_BI") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 8)          '出荷日
        'drEdiRcvHed("NONYU_ZIP") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 8)         '納入先郵便番号
        'drEdiRcvHed("NONYU_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 9)          '納入先コード
        'drEdiRcvHed("NONYU_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 40)        '納入先名
        'drEdiRcvHed("NONYU_AD_1") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 40)      '納入先住所１
        'drEdiRcvHed("NONYU_AD_2") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 40)      '納入先住所２
        'drEdiRcvHed("NONYU_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 20)       '納入先電話番号
        'drEdiRcvHed("TORIKESHI_KB_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_14").ToString().Trim(), 8)  '取消区分名
        'drEdiRcvHed("DENP_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_15").ToString().Trim(), 10)         '受注伝票番号
        'drEdiRcvHed("JISSEKI_SHORI_FLG") = "1"                                                      '実績処理フラグ

        'EDI受信DTL設定
        drEdiRcvDtl("DEL_KB") = "0"
        drEdiRcvDtl("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("FILE_NAME") = drSetDtl("FILE_NAME_OPE")
        drEdiRcvDtl("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")
        drEdiRcvDtl("GYO") = String.Empty
        drEdiRcvDtl("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")                                           '後でセット 
        drEdiRcvDtl("EDI_CTL_NO") = String.Empty                                                        '後でセット                
        drEdiRcvDtl("EDI_CTL_NO_CHU") = String.Empty                                                    '後でセット
        drEdiRcvDtl("OUTKA_CTL_NO") = DEF_CTL_NO
        drEdiRcvDtl("OUTKA_CTL_NO_CHU") = "000"
        'drEdiRcvDtl("CUST_CD_L") = String.Empty                                                       '荷主コード（大）
        'drEdiRcvDtl("CUST_CD_M") = String.Empty                                                       '荷主コード（中）
        drEdiRcvDtl("PRTFLG") = "0"                                                                     'プリントフラグ
        drEdiRcvDtl("CANCEL_FLG") = "0"                                                                 'キャンセルフラグ

        drEdiRcvDtl("UNSO_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 2)               '運送コード
        drEdiRcvDtl("NINUSHI_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 6)            '受信荷主コード
        drEdiRcvDtl("SHORI_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 10)           '処理日
        drEdiRcvDtl("RENBAN") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 4)                '連番
        drEdiRcvDtl("SHORI_TIME") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 8)            '処理時間
        drEdiRcvDtl("JUCHU_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 6)              '受注№
        drEdiRcvDtl("JUCHU_MEI_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 2)          '受注明細№
        drEdiRcvDtl("SHUUKA_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 10)          '集荷日
        drEdiRcvDtl("SHUUKA_TIME_FLG") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 1)       '集荷時間フラグ
        drEdiRcvDtl("SHUUKA_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 12)          '集荷先電話番号
        drEdiRcvDtl("HAISO_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 10)          '配送日
        drEdiRcvDtl("HAISO_TIME_FLG") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 1)       '配送時間フラグ
        drEdiRcvDtl("HAISO_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 12)           '配送先電話番号
        drEdiRcvDtl("HAISO_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_14").ToString().Trim(), 60)            '配送先名
        drEdiRcvDtl("HAISO_ZIP") = Me._Blc.LeftB(drSetDtl("COLUMN_15").ToString().Trim(), 8)            '配送先郵便番号
        drEdiRcvDtl("HAISO_AD_1") = Me._Blc.LeftB(drSetDtl("COLUMN_16").ToString().Trim(), 40)          '配送先住所１
        drEdiRcvDtl("HAISO_AD_2") = Me._Blc.LeftB(drSetDtl("COLUMN_17").ToString().Trim(), 40)          '配送先住所２
        drEdiRcvDtl("GOODS_CD_1") = Me._Blc.LeftB(drSetDtl("COLUMN_18").ToString().Trim(), 6)           '商品コード１
        drEdiRcvDtl("GOODS_CD_2") = Me._Blc.LeftB(drSetDtl("COLUMN_19").ToString().Trim(), 2)           '商品コード２
        drEdiRcvDtl("GOODS_NM_1") = Me._Blc.LeftB(drSetDtl("COLUMN_20").ToString().Trim(), 60)          '商品名１
        drEdiRcvDtl("GOODS_NM_2") = Me._Blc.LeftB(drSetDtl("COLUMN_21").ToString().Trim(), 60)          '商品名２
        drEdiRcvDtl("NISUGATA") = Me._Blc.LeftB(drSetDtl("COLUMN_22").ToString().Trim(), 6)             '荷姿
        drEdiRcvDtl("SURYO") = Me._Blc.LeftB(drSetDtl("COLUMN_23").ToString().Trim(), 4)                '数量
        drEdiRcvDtl("TAN_I") = Me._Blc.LeftB(drSetDtl("COLUMN_24").ToString().Trim(), 4)                '単位
        drEdiRcvDtl("UNSO_TTL_WT") = Me._Blc.LeftB(drSetDtl("COLUMN_25").ToString().Trim(), 5)          '運送総重量
        drEdiRcvDtl("UNSU") = Me._Blc.LeftB(drSetDtl("COLUMN_26").ToString().Trim(), 8)                 '運数
        drEdiRcvDtl("BIKOU") = Me._Blc.LeftB(drSetDtl("COLUMN_27").ToString().Trim(), 50)               '備考
        drEdiRcvDtl("CHUBAN") = Me._Blc.LeftB(drSetDtl("COLUMN_28").ToString().Trim(), 10)              '注番
        drEdiRcvDtl("OEM_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_29").ToString().Trim(), 6)               'ＯＥＭコード
        drEdiRcvDtl("CHARTER_FLG") = Me._Blc.LeftB(drSetDtl("COLUMN_30").ToString().Trim(), 1)          'チャーターフラグ
        drEdiRcvDtl("UNSU_KETTEI_1") = Me._Blc.LeftB(drSetDtl("COLUMN_31").ToString().Trim(), 1)        '運数決定項目１
        drEdiRcvDtl("UNSU_KETTEI_2") = Me._Blc.LeftB(drSetDtl("COLUMN_32").ToString().Trim(), 1)        '運数決定項目２
        drEdiRcvDtl("UNSU_KETTEI_3") = Me._Blc.LeftB(drSetDtl("COLUMN_33").ToString().Trim(), 1)        '運数決定項目３
        drEdiRcvDtl("DENP_KB") = Me._Blc.LeftB(drSetDtl("COLUMN_34").ToString().Trim(), 1)              '伝票区分
        drEdiRcvDtl("UNSO_KAISHA_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_35").ToString().Trim(), 1)       '運送会社コード

        drEdiRcvDtl("JISSEKI_SHORI_FLG") = "1"                                                          '実績処理フラグ

        'データセットに設定
        'ds.Tables("LMH030_OUTKAEDI_HED_KTK").Rows.Add(drEdiRcvHed)
        ds.Tables("LMH030_OUTKAEDI_DTL_KTK").Rows.Add(drEdiRcvDtl)

        Return ds

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(中))"

    ''' <summary>
    ''' データセット設定(EDI出荷(中)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiM(ByVal setDs As DataSet _
                                    , ByVal sWhcd As String _
                                    , ByVal sCustCdL As String _
                                    , ByVal sCustCdM As String _
                                    , ByVal sNrsGoodsCd As String _
                                    , ByVal sNrsGoodsNm As String _
                                    , ByVal sIrime As String _
                                    ) As DataSet

        Dim drOutkaEdiM As DataRow = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_OUTKAEDI_DTL_KTK").Rows(0)
        'Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_OUTKAEDI_HED_KTK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        drOutkaEdiM("DEL_KB") = "0"
        drOutkaEdiM("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drOutkaEdiM("EDI_CTL_NO") = drRcvEdiDtl("EDI_CTL_NO")
        drOutkaEdiM("EDI_CTL_NO_CHU") = drRcvEdiDtl("EDI_CTL_NO_CHU")
        drOutkaEdiM("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiM("OUTKA_CTL_NO_CHU") = String.Empty
        drOutkaEdiM("COA_YN") = String.Empty
        drOutkaEdiM("CUST_ORD_NO_DTL") = String.Concat(drRcvEdiDtl("JUCHU_NO").ToString, "-", drRcvEdiDtl("JUCHU_MEI_NO").ToString)
        drOutkaEdiM("BUYER_ORD_NO_DTL") = drRcvEdiDtl("CHUBAN")
        drOutkaEdiM("CUST_GOODS_CD") = String.Concat(Me._Blc.FormatZero(drRcvEdiDtl("GOODS_CD_1").ToString, 8) _
                                                   , Me._Blc.FormatZero(drRcvEdiDtl("GOODS_CD_2").ToString, 2))

        drOutkaEdiM("NRS_GOODS_CD") = NRS_GOODS_CD_UNSO
        drOutkaEdiM("GOODS_NM") = drRcvEdiDtl("GOODS_NM_1")

        drOutkaEdiM("RSV_NO") = String.Empty
        drOutkaEdiM("LOT_NO") = String.Empty
        drOutkaEdiM("SERIAL_NO") = String.Empty
        drOutkaEdiM("ALCTD_KB") = "01"

        drOutkaEdiM("OUTKA_PKG_NB") = 0             '出荷包装個数

        Dim iSuryou As Integer = Convert.ToInt32(drRcvEdiDtl("SURYO").ToString)
        drOutkaEdiM("OUTKA_HASU") = iSuryou         '出荷端数
        drOutkaEdiM("OUTKA_QT") = 0                 '出荷数量
        drOutkaEdiM("OUTKA_TTL_NB") = iSuryou       '出荷総個数


        '荷姿から入目、入目単位を求める
        Dim sNISUGATA As String = drRcvEdiDtl("NISUGATA").ToString
        'Dim sIrime As String = String.Empty
        Dim sIrimeUt As String = String.Empty

        If sNISUGATA = "" Then    '指定なし
            sIrime = "0"
            sIrimeUt = ""
        ElseIf IsNumeric(sNISUGATA) = True Then  '数値のみ
            sIrime = sNISUGATA
            sIrimeUt = "NO"
        Else
            sIrime = Left(sNISUGATA, Len(sNISUGATA) - 1)
            sIrimeUt = Right(sNISUGATA, 1)
            If IsNumeric(sIrime) = True Then '単位は１桁
            Else
                sIrime = Left(sNISUGATA, Len(sNISUGATA) - 2)
                sIrimeUt = Right(sNISUGATA, 2)
                If IsNumeric(sIrime) = True Then '単位は２桁
                Else  '数値なし
                    sIrime = "0"
                    sIrimeUt = sNISUGATA
                End If
            End If
        End If

        Dim dTtlQt As Double = 0
        dTtlQt = Convert.ToDouble(sIrime) * iSuryou
        drOutkaEdiM("OUTKA_TTL_QT") = dTtlQt    '出荷総数量

        '数量単位
        Select Case drRcvEdiDtl("TAN_I").ToString
            Case "個"
                drOutkaEdiM("KB_UT") = "KE"
            Case "缶"
                drOutkaEdiM("KB_UT") = "CC"
            Case "函"
                drOutkaEdiM("KB_UT") = "BX"
            Case "枚"
                drOutkaEdiM("KB_UT") = "SH"
            Case "DM"
                drOutkaEdiM("KB_UT") = "DR"
            Case Else
                drOutkaEdiM("KB_UT") = String.Empty
        End Select


        drOutkaEdiM("QT_UT") = String.Empty
        drOutkaEdiM("PKG_NB") = 0
        drOutkaEdiM("PKG_UT") = drOutkaEdiM("KB_UT")    '個数単位と同じ
        drOutkaEdiM("ONDO_KB") = String.Empty
        drOutkaEdiM("UNSO_ONDO_KB") = String.Empty

        drOutkaEdiM("IRIME") = sIrime                   '求めた入目

        '入目単位 
        Select Case sIrimeUt
            Case "K", "KG"
                drOutkaEdiM("IRIME_UT") = "KG"
            Case "G"
                drOutkaEdiM("IRIME_UT") = "G"
            Case "L"
                drOutkaEdiM("IRIME_UT") = "L"
            Case "ML"
                drOutkaEdiM("IRIME_UT") = "ML"
            Case ""
                drOutkaEdiM("IRIME_UT") = ""
            Case Else
                drOutkaEdiM("IRIME_UT") = "NO"
        End Select


        '個別重量
        Dim dTtlWt As Double = Convert.ToDouble(drRcvEdiDtl("UNSO_TTL_WT").ToString)
        If iSuryou = 0 Then
            drOutkaEdiM("BETU_WT") = 0
        Else
            drOutkaEdiM("BETU_WT") = dTtlWt / iSuryou
        End If

        '注意事項 
        drOutkaEdiM("REMARK") = _Blc.LeftB(String.Concat(drRcvEdiDtl("NISUGATA").ToString, "  ", drRcvEdiDtl("GOODS_NM_2").ToString), 100)


        drOutkaEdiM("OUT_KB") = "0"
        drOutkaEdiM("AKAKURO_KB") = drRcvEdiDtl("CANCEL_FLG")
        drOutkaEdiM("JISSEKI_FLAG") = "0"
        drOutkaEdiM("JISSEKI_USER") = String.Empty
        drOutkaEdiM("JISSEKI_DATE") = String.Empty
        drOutkaEdiM("JISSEKI_TIME") = String.Empty
        drOutkaEdiM("SET_KB") = ""
        drOutkaEdiM("FREE_N01") = drRcvEdiDtl("SURYO")
        drOutkaEdiM("FREE_N02") = dTtlWt
        drOutkaEdiM("FREE_N03") = drRcvEdiDtl("UNSU")
        drOutkaEdiM("FREE_N04") = 0
        drOutkaEdiM("FREE_N05") = 0

        If iSuryou = 0 Then
            drOutkaEdiM("FREE_N06") = 0
        Else
            drOutkaEdiM("FREE_N06") = dTtlWt * 1000 / iSuryou
        End If
        drOutkaEdiM("FREE_N07") = sIrime
        drOutkaEdiM("FREE_N08") = 0
        drOutkaEdiM("FREE_N09") = 0
        drOutkaEdiM("FREE_N10") = 0

        drOutkaEdiM("FREE_C01") = drRcvEdiDtl("JUCHU_NO")
        drOutkaEdiM("FREE_C02") = drRcvEdiDtl("JUCHU_MEI_NO")
        drOutkaEdiM("FREE_C03") = drRcvEdiDtl("GOODS_CD_1")
        drOutkaEdiM("FREE_C04") = drRcvEdiDtl("GOODS_CD_2")
        drOutkaEdiM("FREE_C05") = drRcvEdiDtl("GOODS_NM_1")
        drOutkaEdiM("FREE_C06") = drRcvEdiDtl("GOODS_NM_2")
        drOutkaEdiM("FREE_C07") = drRcvEdiDtl("NISUGATA")
        drOutkaEdiM("FREE_C08") = drRcvEdiDtl("TAN_I")
        drOutkaEdiM("FREE_C09") = drRcvEdiDtl("CHUBAN")
        drOutkaEdiM("FREE_C10") = drRcvEdiDtl("OEM_CD")
        drOutkaEdiM("FREE_C11") = drRcvEdiDtl("CHARTER_FLG")
        drOutkaEdiM("FREE_C12") = drRcvEdiDtl("UNSU_KETTEI_1")
        drOutkaEdiM("FREE_C13") = drRcvEdiDtl("UNSU_KETTEI_2")
        drOutkaEdiM("FREE_C14") = drRcvEdiDtl("UNSU_KETTEI_3")
        drOutkaEdiM("FREE_C15") = drRcvEdiDtl("DENP_KB")
        drOutkaEdiM("FREE_C16") = drRcvEdiDtl("UNSO_KAISHA_CD")
        drOutkaEdiM("FREE_C17") = String.Empty
        drOutkaEdiM("FREE_C18") = String.Empty
        drOutkaEdiM("FREE_C19") = String.Empty
        drOutkaEdiM("FREE_C20") = String.Empty
        drOutkaEdiM("FREE_C21") = String.Empty
        drOutkaEdiM("FREE_C22") = String.Empty
        drOutkaEdiM("FREE_C23") = String.Empty
        drOutkaEdiM("FREE_C24") = String.Empty
        drOutkaEdiM("FREE_C25") = String.Empty
        drOutkaEdiM("FREE_C26") = String.Empty
        drOutkaEdiM("FREE_C27") = String.Empty
        drOutkaEdiM("FREE_C28") = String.Empty
        drOutkaEdiM("FREE_C29") = String.Empty

        '運送管理番号の既定値（DTL）
        drOutkaEdiM("FREE_C30") = DEF_UNSO_NO_M

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_M").Rows.Add(drOutkaEdiM)

        Return setDs

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(大))"

    ''' <summary>
    ''' データセット設定(EDI出荷(大)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiL(ByVal setDs As DataSet _
                                    , ByVal sWhCd As String _
                                    , ByVal sCustCdL As String _
                                    , ByVal sCustCdM As String _
                                    , ByVal sNrsGoodsCd As String _
                                    , ByVal sNrsGoodsNm As String _
                                    , ByVal sIrime As String _
                                    ) As DataSet

        Dim drOutkaEdiL As DataRow = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        'Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_OUTKAEDI_HED_KTK").Rows(0)
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_OUTKAEDI_DTL_KTK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        '荷主Index
        Dim ediCustIndex As String = drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString()

        drOutkaEdiL("DEL_KB") = "0"
        drOutkaEdiL("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drOutkaEdiL("EDI_CTL_NO") = drRcvEdiDtl("EDI_CTL_NO")
        drOutkaEdiL("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiL("OUTKA_KB") = "10"
        drOutkaEdiL("SYUBETU_KB") = "10"
        drOutkaEdiL("NAIGAI_KB") = String.Empty
        drOutkaEdiL("OUTKA_STATE_KB") = "10"
        drOutkaEdiL("OUTKAHOKOKU_YN") = "0"
        drOutkaEdiL("PICK_KB") = "01"
        drOutkaEdiL("NRS_BR_NM") = String.Empty
        drOutkaEdiL("WH_CD") = sWhCd
        drOutkaEdiL("WH_NM") = String.Empty

        '出荷予定日
        If String.IsNullOrEmpty(drRcvEdiDtl("SHUUKA_DATE").ToString) Then
            drOutkaEdiL("OUTKA_PLAN_DATE") = MyBase.GetSystemDate() 'サーバー日付
        Else
            drOutkaEdiL("OUTKA_PLAN_DATE") = CDate(drRcvEdiDtl("SHUUKA_DATE")).ToString("yyyyMMdd")
        End If

        '出庫日(出荷予定日と同日)
        drOutkaEdiL("OUTKO_DATE") = drOutkaEdiL("OUTKA_PLAN_DATE")

        '納入予定日
        If String.IsNullOrEmpty(drRcvEdiDtl("HAISO_DATE").ToString) Then
            drOutkaEdiL("ARR_PLAN_DATE") = MyBase.GetSystemDate() 'サーバー日付
        Else
            drOutkaEdiL("ARR_PLAN_DATE") = CDate(drRcvEdiDtl("HAISO_DATE")).ToString("yyyyMMdd")
        End If

        '納入予定時刻
        drOutkaEdiL("ARR_PLAN_TIME") = String.Empty
        drOutkaEdiL("HOKOKU_DATE") = String.Empty

        '当期保管料負担有無
        drOutkaEdiL("TOUKI_HOKAN_YN") = String.Empty
        drOutkaEdiL("CUST_CD_L") = sCustCdL
        drOutkaEdiL("CUST_CD_M") = sCustCdM
        drOutkaEdiL("CUST_NM_L") = String.Empty
        drOutkaEdiL("CUST_NM_M") = String.Empty
        drOutkaEdiL("SHIP_CD_L") = String.Empty
        drOutkaEdiL("SHIP_CD_M") = String.Empty
        drOutkaEdiL("SHIP_NM_L") = String.Empty
        drOutkaEdiL("SHIP_NM_M") = String.Empty
        drOutkaEdiL("EDI_DEST_CD") = drRcvEdiDtl("HAISO_TEL").ToString.Replace("-", "")
        drOutkaEdiL("DEST_CD") = drOutkaEdiL("EDI_DEST_CD")
        drOutkaEdiL("DEST_NM") = Me._Blc.LeftB(drRcvEdiDtl("HAISO_NM").ToString, 60)
        drOutkaEdiL("DEST_ZIP") = drRcvEdiDtl("HAISO_ZIP").ToString.Replace("-", "")
        drOutkaEdiL("DEST_AD_1") = Me._Blc.LeftB(drRcvEdiDtl("HAISO_AD_1").ToString(), 40)
        drOutkaEdiL("DEST_AD_2") = Me._Blc.LeftB(drRcvEdiDtl("HAISO_AD_2").ToString(), 40)
        drOutkaEdiL("DEST_AD_3") = String.Empty
        drOutkaEdiL("DEST_AD_4") = String.Empty
        drOutkaEdiL("DEST_AD_5") = String.Empty
        drOutkaEdiL("DEST_TEL") = drRcvEdiDtl("HAISO_TEL")
        drOutkaEdiL("DEST_FAX") = String.Empty
        drOutkaEdiL("DEST_MAIL") = String.Empty
        drOutkaEdiL("DEST_JIS_CD") = String.Empty
        drOutkaEdiL("SP_NHS_KB") = String.Empty
        drOutkaEdiL("COA_YN") = String.Empty
        drOutkaEdiL("CUST_ORD_NO") = drRcvEdiDtl("JUCHU_NO")
        drOutkaEdiL("BUYER_ORD_NO") = drRcvEdiDtl("CHUBAN")

        drOutkaEdiL("UNSO_MOTO_KB") = "10"
        drOutkaEdiL("UNSO_TEHAI_KB") = String.Empty
        drOutkaEdiL("SYARYO_KB") = String.Empty
        drOutkaEdiL("BIN_KB") = String.Empty
        drOutkaEdiL("UNSO_CD") = String.Empty
        drOutkaEdiL("UNSO_NM") = String.Empty
        drOutkaEdiL("UNSO_BR_CD") = String.Empty
        drOutkaEdiL("UNSO_BR_NM") = String.Empty
        drOutkaEdiL("UNCHIN_TARIFF_CD") = String.Empty
        drOutkaEdiL("EXTC_TARIFF_CD") = String.Empty

        ''注意事項
        drOutkaEdiL("REMARK") = String.Empty
        drOutkaEdiL("UNSO_ATT") = drRcvEdiDtl("BIKOU")
        drOutkaEdiL("DENP_YN") = "1"
        drOutkaEdiL("PC_KB") = String.Empty
        drOutkaEdiL("UNCHIN_YN") = "1"
        drOutkaEdiL("NIYAKU_YN") = String.Empty
        drOutkaEdiL("OUT_FLAG") = "0"
        drOutkaEdiL("AKAKURO_KB") = drRcvEdiDtl("CANCEL_FLG")
        drOutkaEdiL("JISSEKI_FLAG") = "0"
        drOutkaEdiL("JISSEKI_USER") = String.Empty
        drOutkaEdiL("JISSEKI_DATE") = String.Empty
        drOutkaEdiL("JISSEKI_TIME") = String.Empty

        drOutkaEdiL("FREE_N01") = 0
        drOutkaEdiL("FREE_N02") = 0
        drOutkaEdiL("FREE_N03") = 0
        drOutkaEdiL("FREE_N04") = 0
        drOutkaEdiL("FREE_N05") = 0
        drOutkaEdiL("FREE_N06") = 0
        drOutkaEdiL("FREE_N07") = 0
        drOutkaEdiL("FREE_N08") = 0
        drOutkaEdiL("FREE_N09") = 0
        drOutkaEdiL("FREE_N10") = 0

        drOutkaEdiL("FREE_C01") = drRcvEdiDtl("UNSO_CD")
        drOutkaEdiL("FREE_C02") = drRcvEdiDtl("NINUSHI_CD")
        drOutkaEdiL("FREE_C03") = drRcvEdiDtl("SHORI_DATE")
        drOutkaEdiL("FREE_C04") = drRcvEdiDtl("RENBAN")
        drOutkaEdiL("FREE_C05") = drRcvEdiDtl("SHORI_TIME")
        drOutkaEdiL("FREE_C06") = drRcvEdiDtl("JUCHU_NO")
        drOutkaEdiL("FREE_C07") = drRcvEdiDtl("SHUUKA_DATE")
        drOutkaEdiL("FREE_C08") = drRcvEdiDtl("SHUUKA_TIME_FLG")
        drOutkaEdiL("FREE_C09") = drRcvEdiDtl("SHUUKA_TEL")
        drOutkaEdiL("FREE_C10") = drRcvEdiDtl("HAISO_DATE")
        drOutkaEdiL("FREE_C11") = drRcvEdiDtl("HAISO_TIME_FLG")
        drOutkaEdiL("FREE_C12") = drRcvEdiDtl("BIKOU")
        drOutkaEdiL("FREE_C13") = String.Empty
        drOutkaEdiL("FREE_C14") = String.Empty
        drOutkaEdiL("FREE_C15") = String.Empty
        drOutkaEdiL("FREE_C16") = String.Empty
        drOutkaEdiL("FREE_C17") = String.Empty
        drOutkaEdiL("FREE_C18") = String.Empty
        drOutkaEdiL("FREE_C19") = String.Empty
        drOutkaEdiL("FREE_C20") = CUST_CD_L_KTK & CUST_CD_M_KTK
        drOutkaEdiL("FREE_C21") = drRcvEdiDtl("HAISO_AD_1").ToString & drRcvEdiDtl("HAISO_AD_2").ToString
        drOutkaEdiL("FREE_C22") = drRcvEdiDtl("HAISO_AD_1").ToString & drRcvEdiDtl("HAISO_AD_2").ToString
        drOutkaEdiL("FREE_C23") = String.Empty
        drOutkaEdiL("FREE_C24") = String.Empty
        drOutkaEdiL("FREE_C25") = String.Empty
        drOutkaEdiL("FREE_C26") = String.Empty
        drOutkaEdiL("FREE_C27") = String.Empty
        drOutkaEdiL("FREE_C28") = String.Empty
        drOutkaEdiL("FREE_C29") = "00"
        '運送管理番号の既定値（HED）
        drOutkaEdiL("FREE_C30") = DEF_UNSO_NO_L

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutkaEdiL)
        Return setDs


    End Function

#End Region

#End Region

#Region "セミEDI時　商品マスタからCustCd等を取得する"

    ''' <summary>
    ''' 商品マスタからCustCd等を取得する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCustCd(ByVal ds As DataSet _
                             , ByRef sCustCdL As String _
                             , ByRef sCustCdM As String _
                             , ByRef sNrsGoodsCd As String _
                             , ByRef sNrsGoodsNm As String _
                             , ByRef sIrime As String _
                              ) As Integer

        Dim dtMstGoods As DataTable = ds.Tables("LMH030_M_GOODS")
        Dim iMstGoodsCnt As Integer = dtMstGoods.Rows.Count


        Select Case iMstGoodsCnt

            Case 0      '商品マスタ取得０件
                '荷主は関塗工とする
                sCustCdL = CUST_CD_L_KTK
                sCustCdM = CUST_CD_M_KTK
                sNrsGoodsCd = String.Empty
                sNrsGoodsNm = String.Empty
                sIrime = "0"

            Case 1      '商品マスタ取得１件
                '荷主は商品マスタから取得する
                sCustCdL = dtMstGoods.Rows(0).Item("CUST_CD_L").ToString
                sCustCdM = dtMstGoods.Rows(0).Item("CUST_CD_M").ToString
                sNrsGoodsCd = dtMstGoods.Rows(0).Item("GOODS_CD_NRS").ToString
                sNrsGoodsNm = dtMstGoods.Rows(0).Item("GOODS_NM_1").ToString
                sIrime = dtMstGoods.Rows(0).Item("STD_IRIME_NB").ToString

            Case Else   '商品マスタ取得２件以上

                '荷主の単一確認
                For i As Integer = 1 To iMstGoodsCnt - 1  'Rows(1)から開始'■要望番号:1612（セミEDI 荷主商品コード重複チェックでアベンド) 2012/12/14 本明修正　（iMstGoodsCnt→iMstGoodsCnt-1に修正）
                    If (dtMstGoods.Rows(i).Item("CUST_CD_L").ToString()).Equals(dtMstGoods.Rows(i - 1).Item("CUST_CD_L").ToString()) _
                    AndAlso (dtMstGoods.Rows(i).Item("CUST_CD_M").ToString()).Equals(dtMstGoods.Rows(i - 1).Item("CUST_CD_M").ToString()) Then
                        '等しい場合はセットする
                        sCustCdL = dtMstGoods.Rows(i).Item("CUST_CD_L").ToString
                        sCustCdM = dtMstGoods.Rows(i).Item("CUST_CD_M").ToString
                    Else
                        '等しくない場合は既定値をセットして抜ける
                        '荷主は関塗工とする
                        sCustCdL = CUST_CD_L_KTK
                        sCustCdM = CUST_CD_M_KTK
                        Exit For
                    End If
                Next

                sNrsGoodsCd = String.Empty
                sNrsGoodsNm = String.Empty

                '入目の単一確認
                For i As Integer = 1 To iMstGoodsCnt - 1  'Rows(1)から開始'■要望番号:1612（セミEDI 荷主商品コード重複チェックでアベンド) 2012/12/14 本明修正　（iMstGoodsCnt→iMstGoodsCnt-1に修正）
                    If (dtMstGoods.Rows(i).Item("STD_IRIME_NB").ToString()).Equals _
                       (dtMstGoods.Rows(i - 1).Item("STD_IRIME_NB").ToString()) Then
                        '等しい場合はセットする
                        sIrime = dtMstGoods.Rows(i).Item("STD_IRIME_NB").ToString
                    Else
                        '等しくない場合は既定値をセットして抜ける
                        sIrime = "0"
                        Exit For
                    End If
                Next

        End Select

    End Function

#Region "セミEDI データセット設定(EDI管理番号(大・中))"

    ''' <summary>
    ''' データセット設定(EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNo(ByVal ds As DataSet _
                               , ByVal iDeleteFlg As Integer, ByVal iCancelFlg As Integer, ByVal iSkipFlg As Integer, ByVal bSameKeyFlg As Boolean _
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer) As DataSet

        Dim dtRcvEdiHed As DataTable = ds.Tables("LMH030_OUTKAEDI_HED_KTK")
        'Dim drRcvEdiHed As DataRow = ds.Tables("LMH030_OUTKAEDI_HED_KTK").Rows(0)
        Dim dtRcvEdiDtl As DataTable = ds.Tables("LMH030_OUTKAEDI_DTL_KTK")
        Dim drRcvEdiDtl As DataRow = ds.Tables("LMH030_OUTKAEDI_DTL_KTK").Rows(0)
        Dim sNrsBrCd As String = ds.Tables("LMH030_OUTKAEDI_DTL_KTK").Rows(0).Item("NRS_BR_CD").ToString()


        '前行とキーが異なる場合　
        If bSameKeyFlg = False Then
            iEdiCtlNoChu = 0    '０クリア    
        End If

        'EDI管理番号(中)をカウントアップ
        iEdiCtlNoChu = iEdiCtlNoChu + 1



        If iCancelFlg = 0 AndAlso iSkipFlg = 0 Then
            'キャンセルフラグが０ かつ スキップフラグが０の場合　
            If bSameKeyFlg = False Then
                '前行とキーが異なる場合　
                'EDI管理番号(大)を新規採番してEDI管理番号(中)を"001"採番
                Dim num As New NumberMasterUtility
                sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, sNrsBrCd)
            End If

            '登録用EDI管理番号
            'dtRcvEdiHed.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'HEDにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'DTLにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")   'EDI_CHUにセット
            dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("00")               '行数にもEDI_CHUと同じ値をセット
        Else
            'dtRcvEdiHed.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'HEDに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'DTLに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = "000"              'EDI_CHUに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("00")   '行数にはカウントアップした値を入れる
        End If

        ''削除EDI管理番号にも設定する(削除フラグが１の場合のみ)
        'If iDeleteFlg = 1 Then
        '    Dim dtRcvHedDel As DataTable = ds.Tables("LMH030_HED_KTK_CANCELOUT")
        '    Dim drRcvHedDel As DataRow = ds.Tables("LMH030_DTL_KTK_CANCELOUT").Rows(0)

        '    Dim dtRcvDtlDel As DataTable = ds.Tables("LMH030_DTL_KTK_CANCELOUT")
        '    Dim drRcvDtlDel As DataRow = ds.Tables("LMH030_DTL_KTK_CANCELOUT").Rows(0)
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

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")

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
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E460", , , LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)

            Else

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
                    Else
                        'ヘッダと明細のファイル名称が等しくない場合
                        '現在行を保持してループを抜ける()
                        iRowCnt = j
                        Exit For
                    End If

                Next


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

        '処理日(カラム3番目)
        Dim sDate As String = dr.Item("COLUMN_3").ToString()
        If IsDate(sDate) = True Then
            'ここではyyyyMMdd形式にしない（EDI_DTLにそのまま保持したいので）
            'dr.Item("COLUMN_3") = CDate(sDate).ToString("yyyyMMdd")     'yyyyMMdd形式でセット
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("処理日(カラム3番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷日(カラム8番目)
        sDate = dr.Item("COLUMN_8").ToString()
        If IsDate(sDate) = True Then
            'ここではyyyyMMdd形式にしない（EDI_DTLにそのまま保持したいので）
            'dr.Item("COLUMN_8") = CDate(sDate).ToString("yyyyMMdd")     'yyyyMMdd形式でセット
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("出荷日(カラム8番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '配送日(カラム11番目)
        sDate = dr.Item("COLUMN_11").ToString()
        If IsDate(sDate) = True Then
            'ここではyyyyMMdd形式にしない（EDI_DTLにそのまま保持したいので）
            'dr.Item("COLUMN_11") = CDate(sDate).ToString("yyyyMMdd")    'yyyyMMdd形式でセット
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("配送日(カラム11番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '数量(カラム23番目)
        Dim sQT As String = dr.Item("COLUMN_23").ToString().Trim()
        If String.IsNullOrEmpty(sQT) = True Then
            '空の場合はゼロをセット
            dr.Item("COLUMN_23") = 0
        Else
            If IsNumeric(sQT) Then
                '数値の場合
                Dim dQT As Double = Convert.ToDouble(sQT)
                If dQT > 9999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("数量(カラム23番目)[", dQT.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            Else
                '数値でない場合
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("数量(カラム23番目)[", sQT, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            End If
        End If

        '運送総重量(カラム25番目)
        Dim sUnsoWt As String = dr.Item("COLUMN_25").ToString().Trim()
        If String.IsNullOrEmpty(sUnsoWt) = True Then
            '空の場合はゼロをセット
            dr.Item("COLUMN_25") = 0
        Else
            If IsNumeric(sUnsoWt) Then
                '数値の場合
                Dim dUnsoWt As Double = Convert.ToDouble(sUnsoWt)
                If dUnsoWt > 9999.9 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("運送総重量(カラム25番目)[", dUnsoWt.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            Else
                '数値でない場合
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("運送総重量(カラム25番目)[", sUnsoWt.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            End If
        End If

        '運数(カラム26番目)
        Dim sUnsu As String = dr.Item("COLUMN_26").ToString().Trim()
        If String.IsNullOrEmpty(sUnsu) = True Then
            '空の場合はゼロをセット
            dr.Item("COLUMN_26") = 0
        Else
            If IsNumeric(sUnsu) Then
                '数値の場合
                Dim dUnsu As Double = Convert.ToDouble(sUnsu)
                If dUnsu > 99999.999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("運数(カラム26番目)[", dUnsu.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            Else
                '数値でない場合
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("運数(カラム26番目)[", sUnsu.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            End If
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

        Dim dtSetHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")        '取込Hed
        Dim dtSetDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")        '取込Dtl
        Dim dtSetRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")        '処理件数

        'Dim dtRcvHed As DataTable = ds.Tables("LMH030_OUTKAEDI_HED_KTK")         'EDI受信Hed
        Dim dtRcvDtl As DataTable = ds.Tables("LMH030_OUTKAEDI_DTL_KTK")         'EDI受信Dtl
        'Dim dtRcvHedCancel As DataTable = ds.Tables("LMH030_HED_KTK_CANCELOUT")  'EDI受信Hed

        Dim iCancelCnt As Integer = 0
        Dim iGoodsCnt As Integer = 0

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        Dim sWhcd As String = String.Empty          '倉庫コード     
        Dim sCustCdL As String = String.Empty       '荷主コード大   （※商品コードから取得する）
        Dim sCustCdM As String = String.Empty       '荷主コード中   （※商品コードから取得する）
        Dim sNrsGoodsCd As String = String.Empty    '日陸商品コード （※商品コードから取得する）
        Dim sNrsGoodsNm As String = String.Empty    '日陸商品名     （※商品コードから取得する）
        Dim sIrime As String = String.Empty         '入目           （※商品コードから取得する）

        Dim iDeleteFlg As Integer = 0               '取消フラグ         (現行LMに合わせた)
        Dim iCancelFlg As Integer = 0               'キャンセルフラグ   (現行LMに合わせた)
        Dim iAkakuroFlg As Integer = 0              '赤黒フラグ         (現行LMに合わせた)
        Dim iSkipFlg As Integer = 0                 'スキップフラグ     (現行LMに合わせた)
        Dim iHakkoFlg As Integer = 0                '発行フラグ         (現行LMに合わせた)

        Dim sNowKey As String = String.Empty        'キー項目（Temp用）
        Dim sOldKey As String = String.Empty        'キー項目（前行）
        Dim sNewKey As String = String.Empty        'キー項目（現在行）
        Dim bSameKeyFlg As Boolean = False          '前行とキーが同じ場合True、異なる場合False

        Dim sEdiCtlNo As String = String.Empty      'EDI管理番号
        Dim iEdiCtlNoChu As Integer = 0             'EDI管理番号（中）

        Dim iRcvHedInsCnt As Integer = 0            '書込件数（受信HED）
        Dim iRcvDtlInsCnt As Integer = 0            '書込件数（受信DTL）
        Dim iOutHedInsCnt As Integer = 0            '書込件数（出荷EDI(大)）
        Dim iOutDtlInsCnt As Integer = 0            '書込件数（出荷EDI(中)）
        Dim iRcvHedCanCnt As Integer = 0            '取消件数（受信HED）
        Dim iRcvDtlCanCnt As Integer = 0            '取消件数（受信Dtl）
        Dim iOutHedCanCnt As Integer = 0            '取消件数（出荷EDI(大)）
        Dim iOutDtlCanCnt As Integer = 0            '取消件数（出荷EDI(中)）


        Dim bNoErr As Boolean = True                'エラー無しフラグ（True：エラー無し、False：エラー有り）


        For i As Integer = 0 To iSetDtlMax

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒受信HED/DTLへのデータセット
            '---------------------------------------------------------------------------
            'ds.Tables("LMH030_OUTKAEDI_HED_KTK").Clear() '受信HEDをクリア
            ds.Tables("LMH030_OUTKAEDI_DTL_KTK").Clear() '受信DTLをクリア
            ds = Me.SetSemiOutkaEdiRcv(ds, i)

            'Dim drEdiRcvHed As DataRow = ds.Tables("LMH030_OUTKAEDI_HED_KTK").Rows(0)
            Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_OUTKAEDI_DTL_KTK").Rows(0)


            '---------------------------------------------------------------------------
            ' 倉庫/仕入先コードから荷主コード、商品コードを設定
            '---------------------------------------------------------------------------
            sWhcd = WH_CD_KTK
            sCustCdL = CUST_CD_L_KTK
            sCustCdM = CUST_CD_M_KTK
            sNrsGoodsCd = NRS_GOODS_CD_UNSO
            sNrsGoodsNm = String.Empty
            sIrime = IRIME_UNSO

            ''Select Case drEdiRcvHed.Item("SOUKO_CD").ToString
            ''    Case SOUKO_CD_TAITAI     '運送指示データの場合
            ''        '大泰化工扱いとする
            ''        sWhcd = WH_CD_TAITAI
            ''        sCustCdL = CUST_CD_L_TAITAI
            ''        sCustCdM = CUST_CD_M_TAITAI
            ''        sNrsGoodsCd = NRS_GOODS_CD_UNSO
            ''        sNrsGoodsNm = String.Empty
            ''        sIrime = "0"

            ''    Case Else

            ''        '関塗工またはアイカ工業
            ''        sWhcd = WH_CD_KTK

            ''        '商品マスタ情報を取得する
            ''        ds = MyBase.CallDAC(Me._Dac, "SelectMstGoods", ds)

            ''        '取得した商品マスタから荷主コード等を決定する
            ''        Call Me.GetCustCd(ds, sCustCdL, sCustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime)
            ''End Select

            ' ''決定した荷主コードをdrEdiRcvHed、drEdiRcvDtlにセットする
            ''drEdiRcvHed.Item("CUST_CD_L") = sCustCdL
            ''drEdiRcvHed.Item("CUST_CD_M") = sCustCdM
            ''drEdiRcvDtl.Item("CUST_CD_L") = sCustCdL
            ''drEdiRcvDtl.Item("CUST_CD_M") = sCustCdM

            '---------------------------------------------------------------------------
            ' 取消区分名を元に赤黒フラグ、スキップフラグを設定
            '---------------------------------------------------------------------------
            ''Select Case drEdiRcvHed.Item("TORIKESHI_KB_NM").ToString

            ''    Case "売上済"
            ''        If Convert.ToInt32(drEdiRcvDtl.Item("QT")) >= 0 Then
            ''            iAkakuroFlg = 0
            ''        Else
            ''            iAkakuroFlg = 1
            ''        End If

            ''        iSkipFlg = 1

            ''    Case "取消伝票"
            ''        If Convert.ToInt32(drEdiRcvDtl.Item("QT")) >= 0 Then
            ''            iAkakuroFlg = 0
            ''        Else
            ''            iAkakuroFlg = 1
            ''        End If

            ''        iSkipFlg = 0

            ''    Case Else
            ''        iAkakuroFlg = 0
            ''        iSkipFlg = 0
            ''End Select

            ' ''決定した赤黒フラグをdrEdiRcvHedにセットする
            ''drEdiRcvHed.Item("CANCEL_FLG") = iAkakuroFlg.ToString

            '---------------------------------------------------------------------------
            ' 発行区分名を元に発行フラグを設定
            '---------------------------------------------------------------------------
            ''Select Case drEdiRcvHed.Item("HAKKO_KB_NM").ToString
            ''    Case "再発行"
            ''        iHakkoFlg = 1
            ''    Case Else
            ''        iHakkoFlg = 0
            ''End Select

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            sNewKey = drEdiRcvDtl.Item("JUCHU_NO").ToString

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

            '---------------------------------------------------------------------------
            ' スキップフラグが０かつ前行同一フラグがfalseの場合
            ' 受注伝票番号を元に取消データの確認処理を行う
            '---------------------------------------------------------------------------
            ''If iSkipFlg = 0 AndAlso bSameKeyFlg = False Then

            ''    '取消フラグ,キャンセルフラグを0にする(初期値)
            ''    iDeleteFlg = 0
            ''    iCancelFlg = 0

            ''    '受信DTL取消データ取得処理
            ''    ds.Tables("LMH030_HED_KTK_CANCELOUT").Clear()    '取得用DSをクリア
            ''    ds = MyBase.CallDAC(Me._Dac, "SelectOutkaEdiRcvCancel", ds)

            ''    'データ取得できた場合
            ''    If MyBase.GetResultCount > 0 Then
            ''        '※直近のレコードで判断（SQL内でDESCされているので１件目のレコード）
            ''        Dim drRcvHedCancel As DataRow = ds.Tables("LMH030_HED_KTK_CANCELOUT").Rows(0)
            ''        '取得したデータのキャンセルフラグを設定
            ''        Dim sRcvHedCancelFlg As String = drRcvHedCancel.Item("CANCEL_FLG").ToString
            ''        '発行フラグ、取得データのキャンセルフラグ、赤黒フラグを元に継続処理判断
            ''        Dim sKeyFlg As String = String.Concat(iHakkoFlg.ToString, sRcvHedCancelFlg, iAkakuroFlg.ToString)
            ''        Select Case sKeyFlg
            ''            Case "000", "011"   '重複受信エラー
            ''                If Left(sNewKey, 32) = Left(sOldKey, 32) Then
            ''                    'メッセージ出力　出荷受信データ伝票番号重複（アイカ工業分混在）
            ''                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {"（アイカ工業分混在）"}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
            ''                Else
            ''                    'メッセージ出力　出荷受信データ伝票番号重複
            ''                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {""}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
            ''                End If
            ''                'エラーフラグ設定してからループを出る
            ''                bNoErr = False
            ''                Exit For

            ''            Case "100", "111"    '再発行かつ既に同内容処理済み
            ''                'ファイル名が同一の場合
            ''                If drRcvHedCancel.Item("FILE_NAME").ToString.Equals(drEdiRcvHed.Item("FILE_NAME").ToString) Then
            ''                    'メッセージ出力　出荷受信データ伝票番号重複
            ''                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {""}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
            ''                    'エラーフラグ設定してからループを出る
            ''                    bNoErr = False
            ''                    Exit For
            ''                Else
            ''                    'スキップフラグを１にして処理続行
            ''                    '※受信DTL,HEDは更新、EDI出荷L,Mは更新しない
            ''                    iSkipFlg = 1
            ''                End If

            ''            Case "001", "101"    '赤データのため削除処理 
            ''                iDeleteFlg = 1  '取消フラグを1にする

            ''                '取得したデータの出荷管理番号が(DEF_EDI_CTL_NO)(出荷未登録の場合)　
            ''                If Right((drRcvHedCancel.Item("OUTKA_CTL_NO").ToString()), 8).Equals("00000000") = True Then
            ''                    'EDI出荷（大・中）を削除するためキャンセルフラグを"1"にする
            ''                    iCancelFlg = 1
            ''                End If
            ''        End Select
            ''    End If
            ''End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iCancelFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)

            '---------------------------------------------------------------------------
            ' 取消フラグが"1"の場合、受信データの取消処理を行う
            '---------------------------------------------------------------------------
            ''If iDeleteFlg = 1 Then

            ''    '削除EDI管理番号に設定する
            ''    Dim drRcvHedCancel As DataRow = ds.Tables("LMH030_HED_KTK_CANCELOUT").Rows(0)
            ''    If String.IsNullOrEmpty(sEdiCtlNo) Then
            ''        drRcvHedCancel.Item("DELETE_EDI_NO") = DEF_CTL_NO
            ''        '※DELETE_EDI_NO_CHU項目が存在しないのでDELETE_USER項目にDELETE_EDI_NO_CHUをセットする
            ''        drRcvHedCancel.Item("DELETE_USER") = "000"
            ''    Else
            ''        drRcvHedCancel.Item("DELETE_EDI_NO") = sEdiCtlNo
            ''        '※DELETE_EDI_NO_CHU項目が存在しないのでDELETE_USER項目にDELETE_EDI_NO_CHUをセットする
            ''        drRcvHedCancel.Item("DELETE_USER") = iEdiCtlNoChu.ToString("000")
            ''    End If

            ''    'EDI受信(DTL)の削除(論理削除)
            ''    ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaRcvDtl", ds)
            ''    iRcvDtlCanCnt = iRcvDtlCanCnt + 1

            ''    'EDI受信(HED)の削除(論理削除)
            ''    ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaRcvHed", ds)
            ''    iRcvHedCanCnt = iRcvHedCanCnt + 1

            ''    '---------------------------------------------------------------------------
            ''    ' キャンセルフラグが"1"の場合、EDI出荷データの削除更新を行う
            ''    '---------------------------------------------------------------------------
            ''    If iCancelFlg = 1 Then

            ''        'EDI出荷(大)の削除(論理削除)
            ''        ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiL", ds)
            ''        iOutHedCanCnt = iOutHedCanCnt + 1

            ''        'EDI出荷(中)の削除(論理削除)
            ''        ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiM", ds)
            ''        iOutDtlCanCnt = iOutDtlCanCnt + 1

            ''    End If

            ''End If


            '別インスタンス
            Dim setDs As DataSet = ds.Copy()

            'Dim setHedDt As DataTable = setDs.Tables("LMH030_OUTKAEDI_HED_KTK")
            Dim setDtlDt As DataTable = setDs.Tables("LMH030_OUTKAEDI_DTL_KTK")

            'setHedDt.Clear()
            setDtlDt.Clear()

            'setHedDt.ImportRow(dtRcvHed.Rows(0))
            setDtlDt.ImportRow(dtRcvDtl.Rows(0))

            '---------------------------------------------------------------------------
            ' EDI受信データの新規追加
            '---------------------------------------------------------------------------
            ' EDI受信データ(DTL)の新規追加

            'iSkipFlgを削除区分の値として使用する
            If iSkipFlg = 0 Then
                setDtlDt.Rows(0).Item("DEL_KB") = "0"
            Else
                setDtlDt.Rows(0).Item("DEL_KB") = "1"
            End If

            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvDtl", setDs)
            iRcvDtlInsCnt = iRcvDtlInsCnt + 1

            ''If bSameKeyFlg = False Then
            ''    ' EDI受信データ(HED)の新規追加

            ''    'iSkipFlgを削除区分の値として使用する
            ''    If iSkipFlg = 0 Then
            ''        setHedDt.Rows(0).Item("DEL_KB") = "0"
            ''    Else
            ''        setHedDt.Rows(0).Item("DEL_KB") = "1"
            ''    End If

            ''    setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvHed", setDs)
            ''    iRcvHedInsCnt = iRcvHedInsCnt + 1
            ''End If

            '---------------------------------------------------------------------------
            ' キャンセルフラグが0かつスキップフラグが0の場合、EDI出荷データの追加処理を行う
            '---------------------------------------------------------------------------
            If iCancelFlg = 0 AndAlso iSkipFlg = 0 Then

                '受信DTL⇒EDI出荷(中)へのデータセット(上記で取得した商品情報も含む)
                setDs = Me.SetSemiOutkaEdiM(setDs, sWhcd, sCustCdL, sCustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime)

                'EDI出荷(中)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                iOutDtlInsCnt = iOutDtlInsCnt + 1

                '前行と差異がある場合は、EDI出荷(大)を新規追加
                If bSameKeyFlg = False Then
                    '受信DTL⇒EDI出荷(大)へのデータセット
                    setDs = Me.SetSemiOutkaEdiL(setDs, sWhcd, sCustCdL, sCustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime)

                    'EDI出荷(大)の新規追加
                    setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", setDs)
                    iOutHedInsCnt = iOutHedInsCnt + 1
                End If

            End If

            'キーを入れ替えるのはiSkipFlgの値で判断する
            '※iSkipFlg = 1の場合、sOldKeyは前行の値である必要があるため 
            If iSkipFlg = 0 Then
                sOldKey = sNewKey   'OldキーにNewキーをセット

            End If

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
        dtSetRet.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

    End Function

#End Region

#End Region

End Class
