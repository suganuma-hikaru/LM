'  ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI512DAC : 出荷予定表(JNC)
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI510DAC
''' </summary>
''' <remarks></remarks>
Public Class LMI512DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Class TABLE_NM
        Public Const PRT_IN As String = "LMI512IN"
        Public Const PRT_OUT As String = "LMI512OUT"
        Public Const M_RPT As String = "M_RPT"
    End Class

    ''' <summary>
    ''' 実施区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class JISSI_KBN
        ''' <summary>
        ''' 未
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MI As String = "0"
        ''' <summary>
        ''' 済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SUMI As String = "1"
    End Class

#End Region 'Const

#Region "SQL"

#Region "帳票パターン取得"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "" _
            & " SELECT" & vbNewLine _
            & "   ISNULL(MR2.NRS_BR_CD, MR1.NRS_BR_CD) AS NRS_BR_CD," & vbNewLine _
            & "   ISNULL(MR2.PTN_ID, MR1.PTN_ID) AS PTN_ID," & vbNewLine _
            & "   ISNULL(MR2.PTN_CD, MR1.PTN_CD) AS PTN_CD," & vbNewLine _
            & "   ISNULL(MR2.RPT_ID, MR1.RPT_ID) AS RPT_ID" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_MST$..M_RPT MR1" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_CUST_RPT MCR" & vbNewLine _
            & "   ON  MCR.NRS_BR_CD = MR1.NRS_BR_CD" & vbNewLine _
            & "   AND MCR.CUST_CD_L = @CUST_CD_L" & vbNewLine _
            & "   AND MCR.CUST_CD_M = '00'" & vbNewLine _
            & "   AND MCR.CUST_CD_S = '00'" & vbNewLine _
            & "   AND MCR.PTN_ID = MR1.PTN_ID" & vbNewLine _
            & "   AND MCR.SYS_DEL_FLG = '0'" & vbNewLine _
            & " LEFT JOIN $LM_MST$..M_RPT MR2" & vbNewLine _
            & "   ON  MR2.NRS_BR_CD = MCR.NRS_BR_CD" & vbNewLine _
            & "   AND MR2.PTN_ID = MCR.PTN_ID" & vbNewLine _
            & "   AND MR2.PTN_CD = MCR.PTN_CD" & vbNewLine _
            & "   AND MR2.SYS_DEL_FLG = '0'" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   MR1.NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND MR1.PTN_ID = 'D1'" & vbNewLine _
            & "   AND MR1.STANDARD_FLAG = '01'" & vbNewLine _
            & "   AND MR1.SYS_DEL_FLG = '0'" & vbNewLine

#End Region

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRINT As String = "" _
            & " SELECT" & vbNewLine _
            & "   HED.DEL_KB," & vbNewLine _
            & "   HED.CRT_DATE," & vbNewLine _
            & "   HED.FILE_NAME," & vbNewLine _
            & "   HED.REC_NO," & vbNewLine _
            & "   HED.NRS_BR_CD," & vbNewLine _
            & "   HED.INOUT_KB," & vbNewLine _
            & "   HED.EDI_CTL_NO," & vbNewLine _
            & "   HED.INKA_CTL_NO_L," & vbNewLine _
            & "   HED.OUTKA_CTL_NO," & vbNewLine _
            & "   HED.CUST_CD_L," & vbNewLine _
            & "   HED.CUST_CD_M," & vbNewLine _
            & "   HED.PRTFLG," & vbNewLine _
            & "   HED.CANCEL_FLG," & vbNewLine _
            & "   HED.NRS_TANTO," & vbNewLine _
            & "   HED.DATA_KIND," & vbNewLine _
            & "   HED.SEND_CODE," & vbNewLine _
            & "   HED.SR_DEN_NO," & vbNewLine _
            & "   HED.HIS_NO," & vbNewLine _
            & "   HED.PROC_YMD," & vbNewLine _
            & "   HED.PROC_TIME," & vbNewLine _
            & "   HED.PROC_NO," & vbNewLine _
            & "   HED.SEND_DEN_YMD," & vbNewLine _
            & "   HED.SEND_DEN_TIME," & vbNewLine _
            & "   HED.BPID_KKN," & vbNewLine _
            & "   HED.BPID_SUB_KKN," & vbNewLine _
            & "   HED.BPID_HAN," & vbNewLine _
            & "   HED.RCV_COMP_CD," & vbNewLine _
            & "   HED.SND_COMP_CD," & vbNewLine _
            & "   HED.RB_KBN," & vbNewLine _
            & "   HED.MOD_KBN," & vbNewLine _
            & "   HED.DATA_KBN," & vbNewLine _
            & "   HED.FAX_KBN," & vbNewLine _
            & "   HED.OUTKA_REQ_KBN," & vbNewLine _
            & "   HED.INKA_P_KBN," & vbNewLine _
            & "   HED.OUTKA_SEPT_KBN," & vbNewLine _
            & "   HED.EM_OUTKA_KBN," & vbNewLine _
            & "   HED.UNSO_ROUTE_P," & vbNewLine _
            & "   HED.UNSO_ROUTE_A," & vbNewLine _
            & "   HED.CAR_KIND_P," & vbNewLine _
            & "   HED.CAR_KIND_A," & vbNewLine _
            & "   HED.CAR_NO_P," & vbNewLine _
            & "   HED.CAR_NO_A," & vbNewLine _
            & "   HED.COMBI_NO_P," & vbNewLine _
            & "   HED.COMBI_NO_A," & vbNewLine _
            & "   HED.UNSO_REQ_YN," & vbNewLine _
            & "   HED.DEST_CHK_KBN," & vbNewLine _
            & "   HED.INKO_DATE_P," & vbNewLine _
            & "   HED.INKO_DATE_A," & vbNewLine _
            & "   HED.INKO_TIME," & vbNewLine _
            & "   HED.OUTKA_DATE_P," & vbNewLine _
            & "   HED.OUTKA_DATE_A," & vbNewLine _
            & "   HED.OUTKA_TIME_E," & vbNewLine _
            & "   HED.CARGO_BKG_DATE_P," & vbNewLine _
            & "   HED.CARGO_BKG_DATE_A," & vbNewLine _
            & "   HED.ARRIVAL_DATE_P," & vbNewLine _
            & "   HED.ARRIVAL_DATE_A," & vbNewLine _
            & "   HED.ARRIVAL_TIME," & vbNewLine _
            & "   HED.UNSO_DATE," & vbNewLine _
            & "   HED.UNSO_TIME," & vbNewLine _
            & "   HED.ZAI_RPT_DATE," & vbNewLine _
            & "   HED.BAILER_CD," & vbNewLine _
            & "   HED.BAILER_NM," & vbNewLine _
            & "   HED.BAILER_BU_CD," & vbNewLine _
            & "   HED.BAILER_BU_NM," & vbNewLine _
            & "   HED.SHIPPER_CD," & vbNewLine _
            & "   HED.SHIPPER_NM," & vbNewLine _
            & "   HED.SHIPPER_BU_CD," & vbNewLine _
            & "   HED.SHIPPER_BU_NM," & vbNewLine _
            & "   HED.CONSIGNEE_CD," & vbNewLine _
            & "   HED.CONSIGNEE_NM," & vbNewLine _
            & "   HED.CONSIGNEE_BU_CD," & vbNewLine _
            & "   HED.CONSIGNEE_BU_NM," & vbNewLine _
            & "   HED.SOKO_PROV_CD," & vbNewLine _
            & "   HED.SOKO_PROV_NM," & vbNewLine _
            & "   HED.UNSO_PROV_CD," & vbNewLine _
            & "   HED.UNSO_PROV_NM," & vbNewLine _
            & "   HED.ACT_UNSO_CD," & vbNewLine _
            & "   HED.UNSO_TF_KBN," & vbNewLine _
            & "   HED.UNSO_F_KBN," & vbNewLine _
            & "   HED.DEST_CD," & vbNewLine _
            & "   HED.DEST_NM," & vbNewLine _
            & "   HED.DEST_BU_CD," & vbNewLine _
            & "   HED.DEST_BU_NM," & vbNewLine _
            & "   HED.DEST_AD_CD," & vbNewLine _
            & "   HED.DEST_AD_NM," & vbNewLine _
            & "   HED.DEST_YB_NO," & vbNewLine _
            & "   HED.DEST_TEL_NO," & vbNewLine _
            & "   HED.DEST_FAX_NO," & vbNewLine _
            & "   HED.DELIVERY_NM," & vbNewLine _
            & "   HED.DELIVERY_SAGYO," & vbNewLine _
            & "   HED.ORDER_NO," & vbNewLine _
            & "   HED.JYUCHU_NO," & vbNewLine _
            & "   HED.PRI_SHOP_CD," & vbNewLine _
            & "   HED.PRI_SHOP_NM," & vbNewLine _
            & "   HED.INV_REM_NM," & vbNewLine _
            & "   HED.INV_REM_KANA," & vbNewLine _
            & "   HED.DEN_NO," & vbNewLine _
            & "   HED.MEI_DEN_NO," & vbNewLine _
            & "   HED.OUTKA_POSI_CD," & vbNewLine _
            & "   HED.OUTKA_POSI_NM," & vbNewLine _
            & "   HED.OUTKA_POSI_BU_CD_PA," & vbNewLine _
            & "   HED.OUTKA_POSI_BU_CD_NAIBU," & vbNewLine _
            & "   HED.OUTKA_POSI_BU_NM_PA," & vbNewLine _
            & "   HED.OUTKA_POSI_BU_NM_NAIBU," & vbNewLine _
            & "   HED.OUTKA_POSI_AD_CD_PA," & vbNewLine _
            & "   HED.OUTKA_POSI_AD_NM_PA," & vbNewLine _
            & "   HED.OUTKA_POSI_TEL_NO_PA," & vbNewLine _
            & "   HED.OUTKA_POSI_FAX_NO_PA," & vbNewLine _
            & "   HED.UNSO_JURYO," & vbNewLine _
            & "   HED.UNSO_JURYO_FLG," & vbNewLine _
            & "   HED.UNIT_LOAD_CD," & vbNewLine _
            & "   HED.UNIT_LOAD_SU," & vbNewLine _
            & "   HED.REMARK," & vbNewLine _
            & "   HED.REMARK_KANA," & vbNewLine _
            & "   HED.HARAI_KBN," & vbNewLine _
            & "   HED.DATA_TYPE," & vbNewLine _
            & "   HED.RTN_FLG," & vbNewLine _
            & "   HED.SND_CANCEL_FLG," & vbNewLine _
            & "   HED.OLD_DATA_FLG," & vbNewLine _
            & "   @PRINT_NO AS PRINT_NO," & vbNewLine _
            & "   HED.NRS_SYS_FLG," & vbNewLine _
            & "   HED.OLD_SYS_FLG," & vbNewLine _
            & "   HED.RTN_FILE_DATE," & vbNewLine _
            & "   HED.RTN_FILE_TIME," & vbNewLine _
            & "   HED.RECORD_STATUS," & vbNewLine _
            & "   HED.DELETE_USER," & vbNewLine _
            & "   HED.DELETE_DATE," & vbNewLine _
            & "   HED.DELETE_TIME," & vbNewLine _
            & "   HED.DELETE_EDI_NO," & vbNewLine _
            & "   HED.PRT_USER," & vbNewLine _
            & "   HED.PRT_DATE," & vbNewLine _
            & "   HED.PRT_TIME," & vbNewLine _
            & "   HED.EDI_USER," & vbNewLine _
            & "   HED.EDI_DATE," & vbNewLine _
            & "   HED.EDI_TIME," & vbNewLine _
            & "   HED.OUTKA_USER," & vbNewLine _
            & "   HED.OUTKA_DATE," & vbNewLine _
            & "   HED.OUTKA_TIME," & vbNewLine _
            & "   HED.INKA_USER," & vbNewLine _
            & "   HED.INKA_DATE," & vbNewLine _
            & "   HED.INKA_TIME," & vbNewLine _
            & "   HED.UPD_USER," & vbNewLine _
            & "   HED.UPD_DATE," & vbNewLine _
            & "   HED.UPD_TIME," & vbNewLine _
            & "   MEI.GYO," & vbNewLine _
            & "   MEI.EDI_CTL_NO_CHU," & vbNewLine _
            & "   MEI.INKA_CTL_NO_M," & vbNewLine _
            & "   MEI.OUTKA_CTL_NO_CHU," & vbNewLine _
            & "   MEI.MEI_NO_P," & vbNewLine _
            & "   MEI.MEI_NO_A," & vbNewLine _
            & "   MEI.JYUCHU_GOODS_CD," & vbNewLine _
            & "   MEI.GOODS_NM," & vbNewLine _
            & "   MEI.GOODS_KANA1," & vbNewLine _
            & "   MEI.GOODS_KANA2," & vbNewLine _
            & "   MEI.NISUGATA_CD," & vbNewLine _
            & "   MEI.IRISUU," & vbNewLine _
            & "   MEI.LOT_NO_P," & vbNewLine _
            & "   MEI.LOT_NO_A," & vbNewLine _
            & "   MEI.SURY_TANI_CD," & vbNewLine _
            & "   MEI.SURY_REQ," & vbNewLine _
            & "   MEI.SURY_RPT," & vbNewLine _
            & "   MEI.MEI_REM1," & vbNewLine _
            & "   MEI.MEI_REM2," & vbNewLine _
            & "   MEI.JISSEKI_SHORI_FLG," & vbNewLine _
            & "   MEI.JISSEKI_USER," & vbNewLine _
            & "   MEI.JISSEKI_DATE," & vbNewLine _
            & "   MEI.JISSEKI_TIME," & vbNewLine _
            & "   MEI.SEND_USER," & vbNewLine _
            & "   MEI.SEND_DATE," & vbNewLine _
            & "   MEI.SEND_TIME," & vbNewLine _
            & "   MEI.DELETE_EDI_NO_CHU," & vbNewLine _
            & "   UHD.EDI_CTL_NO AS UNSO_EDI_CTL_NO," & vbNewLine _
            & "   UHD.REMARK AS UNSO_REMARK," & vbNewLine _
            & "   UHD.REMARK_KANA AS UNSO_REMARK_KANA," & vbNewLine _
            & "   UHD.CAR_NO_P AS UNSO_CAR_NO_P," & vbNewLine _
            & "   UHD.CAR_NO_A AS UNSO_CAR_NO_A," & vbNewLine _
            & "   UHD.DELIVERY_NM AS UNSO_DELIVERY_NM," & vbNewLine _
            & "   UHD.DELIVERY_SAGYO AS UNSO_DELIVERY_SAGYO," & vbNewLine _
            & "   BO1.OUTKA_POSI_BU_NM_RYAK AS UNSO_OUTKA_POSI_BU_NM_RYAK," & vbNewLine _
            & "   KB1.KBN_NM1 AS ACT_UNSO_NM," & vbNewLine _
            & "   KB2.KBN_NM1 AS UNSO_ROUTE_NM" & vbNewLine _
            & "    , CASE WHEN LAST_REC.REC_KEY IS NULL THEN 0 ELSE 1 END AS LAST_REC_CNT                 " & vbNewLine _
            & "    , LAST_REC.OUTKA_DATE_A                                AS LAST_REC_OUTKA_DATE_A        " & vbNewLine _
            & "    , LAST_REC.ARRIVAL_DATE_A                              AS LAST_REC_ARRIVAL_DATE_A      " & vbNewLine _
            & "    , LAST_REC.DELIVERY_SAGYO                              AS LAST_REC_DELIVERY_SAGYO      " & vbNewLine _
            & "    , LAST_REC.ORDER_NO                                    AS LAST_REC_ORDER_NO            " & vbNewLine _
            & "    , LAST_REC.JYUCHU_NO                                   AS LAST_REC_JYUCHU_NO           " & vbNewLine _
            & "    , LAST_REC.OUTKA_POSI_BU_CD_PA                         AS LAST_REC_OUTKA_POSI_BU_CD_PA " & vbNewLine _
            & "    , LAST_REC.UNSO_ROUTE_A                                AS LAST_REC_UNSO_ROUTE_A        " & vbNewLine _
            & "    , LAST_REC.GOODS_KANA1                                 AS LAST_REC_GOODS_KANA1         " & vbNewLine _
            & "    , LAST_REC.GOODS_KANA2                                 AS LAST_REC_GOODS_KANA2         " & vbNewLine _
            & "    , LAST_REC.IRISUU                                      AS LAST_REC_IRISUU              " & vbNewLine _
            & "    , LAST_REC.LOT_NO_A                                    AS LAST_REC_LOT_NO_A            " & vbNewLine _
            & "    , LAST_REC.SURY_RPT                                    AS LAST_REC_SURY_RPT            " & vbNewLine _
            & "    , LAST_REC.REC_KEY                                     AS LAST_REC_REC_KEY             " & vbNewLine _
            & "    , LAST_REC.SR_DEN_NO                                   AS LAST_REC_SR_DEN_NO           " & vbNewLine _
            & "    , LAST_REC.EDI_CTL_NO                                  AS LAST_REC_EDI_CTL_NO          " & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC HED" & vbNewLine _
            & " INNER JOIN" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_DTL_JNC MEI" & vbNewLine _
            & "   ON   MEI.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  MEI.NRS_BR_CD = HED.NRS_BR_CD" & vbNewLine _
            & "   AND  MEI.EDI_CTL_NO = HED.EDI_CTL_NO" & vbNewLine _
            & "   AND  MEI.INOUT_KB = HED.INOUT_KB" & vbNewLine _
            & " LEFT JOIN (" & vbNewLine _
            & "   SELECT *" & vbNewLine _
            & "   FROM" & vbNewLine _
            & "     $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & "   WHERE" & vbNewLine _
            & "     SYS_DEL_FLG = '0'" & vbNewLine _
            & "     AND DATA_KIND = '3001'" & vbNewLine _
            & "     AND MOD_KBN NOT IN ('E', 'L')" & vbNewLine _
            & "     AND OLD_DATA_FLG = ''" & vbNewLine _
            & "   ) UHD" & vbNewLine _
            & "   ON  UHD.NRS_BR_CD = HED.NRS_BR_CD" & vbNewLine _
            & "   AND UHD.SR_DEN_NO = HED.SR_DEN_NO" & vbNewLine _
            & "   AND UHD.RB_KBN = HED.RB_KBN" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_BO_MST_JNC BO1" & vbNewLine _
            & "   ON   BO1.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  BO1.OUTKA_POSI_BU_CD = HED.OUTKA_POSI_BU_CD_PA" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB1" & vbNewLine _
            & "   ON   KB1.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB1.KBN_GROUP_CD = 'J016'" & vbNewLine _
            & "   AND  KB1.KBN_NM2 = HED.ACT_UNSO_CD" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB2" & vbNewLine _
            & "   ON   KB2.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB2.KBN_GROUP_CD = 'J015'" & vbNewLine _
            & "   AND  KB2.KBN_NM2 = HED.UNSO_ROUTE_A" & vbNewLine _
            & " LEFT JOIN                                                                         " & vbNewLine _
            & "   (                                                                               " & vbNewLine _
            & "    SELECT                                                                         " & vbNewLine _
            & "                H_INOUTKAEDI_HED_JNC.CRT_DATE                                      " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_HED_JNC.FILE_NAME                                     " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_HED_JNC.REC_NO                                        " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_HED_JNC.EDI_CTL_NO                                    " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_DTL_JNC.GYO                                           " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_DTL_JNC.EDI_CTL_NO_CHU AS REC_KEY                     " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.OUTKA_DATE_A                                        " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.ARRIVAL_DATE_A                                      " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.DELIVERY_SAGYO                                      " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.ORDER_NO                                            " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.JYUCHU_NO                                           " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.OUTKA_POSI_BU_CD_PA                                 " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.UNSO_ROUTE_A                                        " & vbNewLine _
            & "        , H_INOUTKAEDI_DTL_JNC.GOODS_KANA1                                         " & vbNewLine _
            & "        , H_INOUTKAEDI_DTL_JNC.GOODS_KANA2                                         " & vbNewLine _
            & "        , H_INOUTKAEDI_DTL_JNC.IRISUU                                              " & vbNewLine _
            & "        , H_INOUTKAEDI_DTL_JNC.LOT_NO_A                                            " & vbNewLine _
            & "        , H_INOUTKAEDI_DTL_JNC.SURY_RPT                                            " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.SR_DEN_NO                                           " & vbNewLine _
            & "        , H_INOUTKAEDI_HED_JNC.EDI_CTL_NO                                          " & vbNewLine _
            & "    FROM                                                                           " & vbNewLine _
            & "        $LM_TRN$..H_INOUTKAEDI_HED_JNC                                             " & vbNewLine _
            & "    LEFT JOIN                                                                      " & vbNewLine _
            & "        $LM_TRN$..H_INOUTKAEDI_DTL_JNC                                             " & vbNewLine _
            & "            ON   H_INOUTKAEDI_DTL_JNC.SYS_DEL_FLG = '0'                            " & vbNewLine _
            & "            AND  H_INOUTKAEDI_DTL_JNC.NRS_BR_CD = H_INOUTKAEDI_HED_JNC.NRS_BR_CD   " & vbNewLine _
            & "            AND  H_INOUTKAEDI_DTL_JNC.EDI_CTL_NO = H_INOUTKAEDI_HED_JNC.EDI_CTL_NO " & vbNewLine _
            & "            AND  H_INOUTKAEDI_DTL_JNC.INOUT_KB = H_INOUTKAEDI_HED_JNC.INOUT_KB     " & vbNewLine _
            & "    WHERE                                                                          " & vbNewLine _
            & "    -- 画面検索時固定条件                                                          " & vbNewLine _
            & "        H_INOUTKAEDI_HED_JNC.SYS_DEL_FLG = '0'                                     " & vbNewLine _
            & "    AND H_INOUTKAEDI_HED_JNC.OLD_DATA_FLG = ''                                     " & vbNewLine _
            & "    -- 画面「入出庫区分 出庫」                                                     " & vbNewLine _
            & "    AND H_INOUTKAEDI_HED_JNC.DATA_KIND = '4001'                                    " & vbNewLine _
            & "    -- 画面「消 × を非表示」                                                      " & vbNewLine _
            & "    AND NOT H_INOUTKAEDI_HED_JNC.MOD_KBN = '3'                                     " & vbNewLine _
            & "    AND NOT H_INOUTKAEDI_HED_JNC.MOD_KBN = 'E'                                     " & vbNewLine _
            & "    -- 画面「赤データ非表示」                                                      " & vbNewLine _
            & "    AND NOT H_INOUTKAEDI_HED_JNC.RB_KBN = '1'                                      " & vbNewLine _
            & "    ) LAST_REC                                                                     " & vbNewLine _
            & "        ON   LAST_REC.REC_KEY =                                                    " & vbNewLine _
            & "   (                                                                               " & vbNewLine _
            & "    SELECT TOP 1                                                                   " & vbNewLine _
            & "                H_INOUTKAEDI_HED_JNC.CRT_DATE                                      " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_HED_JNC.FILE_NAME                                     " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_HED_JNC.REC_NO                                        " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_HED_JNC.EDI_CTL_NO                                    " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_DTL_JNC.GYO                                           " & vbNewLine _
            & "        + ',' + H_INOUTKAEDI_DTL_JNC.EDI_CTL_NO_CHU AS REC_KEY                     " & vbNewLine _
            & "    FROM                                                                           " & vbNewLine _
            & "        $LM_TRN$..H_INOUTKAEDI_HED_JNC                                             " & vbNewLine _
            & "    LEFT JOIN                                                                      " & vbNewLine _
            & "        $LM_TRN$..H_INOUTKAEDI_DTL_JNC                                             " & vbNewLine _
            & "            ON   H_INOUTKAEDI_DTL_JNC.SYS_DEL_FLG = '0'                            " & vbNewLine _
            & "            AND  H_INOUTKAEDI_DTL_JNC.NRS_BR_CD = H_INOUTKAEDI_HED_JNC.NRS_BR_CD   " & vbNewLine _
            & "            AND  H_INOUTKAEDI_DTL_JNC.EDI_CTL_NO = H_INOUTKAEDI_HED_JNC.EDI_CTL_NO " & vbNewLine _
            & "            AND  H_INOUTKAEDI_DTL_JNC.INOUT_KB = H_INOUTKAEDI_HED_JNC.INOUT_KB     " & vbNewLine _
            & "    WHERE                                                                          " & vbNewLine _
            & "    -- 画面検索時固定条件                                                          " & vbNewLine _
            & "        H_INOUTKAEDI_HED_JNC.SYS_DEL_FLG = '0'                                     " & vbNewLine _
            & "    AND H_INOUTKAEDI_HED_JNC.OLD_DATA_FLG = ''                                     " & vbNewLine _
            & "    -- 画面「入出庫区分 出庫」                                                     " & vbNewLine _
            & "    AND H_INOUTKAEDI_HED_JNC.DATA_KIND = '4001'                                    " & vbNewLine _
            & "    -- 画面「消 × を非表示」                                                      " & vbNewLine _
            & "    AND NOT H_INOUTKAEDI_HED_JNC.MOD_KBN = '3'                                     " & vbNewLine _
            & "    AND NOT H_INOUTKAEDI_HED_JNC.MOD_KBN = 'E'                                     " & vbNewLine _
            & "    -- 画面「赤データ非表示」                                                      " & vbNewLine _
            & "    AND NOT H_INOUTKAEDI_HED_JNC.RB_KBN = '1'                                      " & vbNewLine _
            & "    AND H_INOUTKAEDI_HED_JNC.SR_DEN_NO = HED.SR_DEN_NO                             " & vbNewLine _
            & "    AND(    H_INOUTKAEDI_HED_JNC.CRT_DATE <> HED.CRT_DATE                          " & vbNewLine _
            & "        OR  H_INOUTKAEDI_HED_JNC.FILE_NAME <> HED.FILE_NAME                        " & vbNewLine _
            & "        OR  H_INOUTKAEDI_HED_JNC.REC_NO <> HED.REC_NO                              " & vbNewLine _
            & "        OR  H_INOUTKAEDI_HED_JNC.EDI_CTL_NO <> HED.EDI_CTL_NO)                     " & vbNewLine _
            & "    AND H_INOUTKAEDI_DTL_JNC.GYO = MEI.GYO                                         " & vbNewLine _
            & "    AND H_INOUTKAEDI_HED_JNC.EDI_CTL_NO < HED.EDI_CTL_NO                           " & vbNewLine _
            & "    ORDER BY                                                                       " & vbNewLine _
            & "        H_INOUTKAEDI_HED_JNC.EDI_CTL_NO DESC                                       " & vbNewLine _
            & "    )                                                                              " & vbNewLine _
            & ""

#End Region

#Region "プリントフラグ更新"

    ''' <summary>
    ''' プリントフラグ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_PRTFLG As String = "" _
        & " UPDATE $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
        & " SET" & vbNewLine _
        & "   PRTFLG = @PRTFLG," & vbNewLine _
        & "   PRINT_NO = @PRINT_NO," & vbNewLine _
        & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
        & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
        & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
        & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
        & " WHERE" & vbNewLine _
        & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
        & "   AND SR_DEN_NO = @SR_DEN_NO" & vbNewLine _
        & "   AND DATA_KIND = @DATA_KIND" & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'" & vbNewLine _
        & "   AND MOD_KBN NOT IN ('E', 'L')" & vbNewLine _
        & "   AND OLD_DATA_FLG = ''" & vbNewLine _
        & "   AND PRTFLG <> '1'" & vbNewLine

#End Region

#End Region 'SQL

#Region "Field"

    ''' <summary>
    ''' DataTableの行抜き出し
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region 'Field

#Region "Method"

#Region "共通処理"

    ''' <summary>
    ''' 共通処理：スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#Region "帳票パターン取得"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.PRT_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI512DAC.SQL_SELECT_MPRT)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        If reader.HasRows() Then
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("PTN_ID", "PTN_ID")
            map.Add("PTN_CD", "PTN_CD")
            map.Add("RPT_ID", "RPT_ID")
            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.M_RPT)
        End If

        reader.Close()

        Return ds

    End Function

#End Region

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.PRT_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI512DAC.SQL_SELECT_PRINT)

        '条件およびパラメータの設定
        Call Me.SelectPrintDataSetCondition()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("NRS_TANTO", "NRS_TANTO")
        map.Add("DATA_KIND", "DATA_KIND")
        map.Add("SEND_CODE", "SEND_CODE")
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("HIS_NO", "HIS_NO")
        map.Add("PROC_YMD", "PROC_YMD")
        map.Add("PROC_TIME", "PROC_TIME")
        map.Add("PROC_NO", "PROC_NO")
        map.Add("SEND_DEN_YMD", "SEND_DEN_YMD")
        map.Add("SEND_DEN_TIME", "SEND_DEN_TIME")
        map.Add("BPID_KKN", "BPID_KKN")
        map.Add("BPID_SUB_KKN", "BPID_SUB_KKN")
        map.Add("BPID_HAN", "BPID_HAN")
        map.Add("RCV_COMP_CD", "RCV_COMP_CD")
        map.Add("SND_COMP_CD", "SND_COMP_CD")
        map.Add("RB_KBN", "RB_KBN")
        map.Add("MOD_KBN", "MOD_KBN")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("FAX_KBN", "FAX_KBN")
        map.Add("OUTKA_REQ_KBN", "OUTKA_REQ_KBN")
        map.Add("INKA_P_KBN", "INKA_P_KBN")
        map.Add("OUTKA_SEPT_KBN", "OUTKA_SEPT_KBN")
        map.Add("EM_OUTKA_KBN", "EM_OUTKA_KBN")
        map.Add("UNSO_ROUTE_P", "UNSO_ROUTE_P")
        map.Add("UNSO_ROUTE_A", "UNSO_ROUTE_A")
        map.Add("CAR_KIND_P", "CAR_KIND_P")
        map.Add("CAR_KIND_A", "CAR_KIND_A")
        map.Add("CAR_NO_P", "CAR_NO_P")
        map.Add("CAR_NO_A", "CAR_NO_A")
        map.Add("COMBI_NO_P", "COMBI_NO_P")
        map.Add("COMBI_NO_A", "COMBI_NO_A")
        map.Add("UNSO_REQ_YN", "UNSO_REQ_YN")
        map.Add("DEST_CHK_KBN", "DEST_CHK_KBN")
        map.Add("INKO_DATE_P", "INKO_DATE_P")
        map.Add("INKO_DATE_A", "INKO_DATE_A")
        map.Add("INKO_TIME", "INKO_TIME")
        map.Add("OUTKA_DATE_P", "OUTKA_DATE_P")
        map.Add("OUTKA_DATE_A", "OUTKA_DATE_A")
        map.Add("OUTKA_TIME_E", "OUTKA_TIME_E")
        map.Add("CARGO_BKG_DATE_P", "CARGO_BKG_DATE_P")
        map.Add("CARGO_BKG_DATE_A", "CARGO_BKG_DATE_A")
        map.Add("ARRIVAL_DATE_P", "ARRIVAL_DATE_P")
        map.Add("ARRIVAL_DATE_A", "ARRIVAL_DATE_A")
        map.Add("ARRIVAL_TIME", "ARRIVAL_TIME")
        map.Add("UNSO_DATE", "UNSO_DATE")
        map.Add("UNSO_TIME", "UNSO_TIME")
        map.Add("ZAI_RPT_DATE", "ZAI_RPT_DATE")
        map.Add("BAILER_CD", "BAILER_CD")
        map.Add("BAILER_NM", "BAILER_NM")
        map.Add("BAILER_BU_CD", "BAILER_BU_CD")
        map.Add("BAILER_BU_NM", "BAILER_BU_NM")
        map.Add("SHIPPER_CD", "SHIPPER_CD")
        map.Add("SHIPPER_NM", "SHIPPER_NM")
        map.Add("SHIPPER_BU_CD", "SHIPPER_BU_CD")
        map.Add("SHIPPER_BU_NM", "SHIPPER_BU_NM")
        map.Add("CONSIGNEE_CD", "CONSIGNEE_CD")
        map.Add("CONSIGNEE_NM", "CONSIGNEE_NM")
        map.Add("CONSIGNEE_BU_CD", "CONSIGNEE_BU_CD")
        map.Add("CONSIGNEE_BU_NM", "CONSIGNEE_BU_NM")
        map.Add("SOKO_PROV_CD", "SOKO_PROV_CD")
        map.Add("SOKO_PROV_NM", "SOKO_PROV_NM")
        map.Add("UNSO_PROV_CD", "UNSO_PROV_CD")
        map.Add("UNSO_PROV_NM", "UNSO_PROV_NM")
        map.Add("ACT_UNSO_CD", "ACT_UNSO_CD")
        map.Add("UNSO_TF_KBN", "UNSO_TF_KBN")
        map.Add("UNSO_F_KBN", "UNSO_F_KBN")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_BU_CD", "DEST_BU_CD")
        map.Add("DEST_BU_NM", "DEST_BU_NM")
        map.Add("DEST_AD_CD", "DEST_AD_CD")
        map.Add("DEST_AD_NM", "DEST_AD_NM")
        map.Add("DEST_YB_NO", "DEST_YB_NO")
        map.Add("DEST_TEL_NO", "DEST_TEL_NO")
        map.Add("DEST_FAX_NO", "DEST_FAX_NO")
        map.Add("DELIVERY_NM", "DELIVERY_NM")
        map.Add("DELIVERY_SAGYO", "DELIVERY_SAGYO")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("JYUCHU_NO", "JYUCHU_NO")
        map.Add("PRI_SHOP_CD", "PRI_SHOP_CD")
        map.Add("PRI_SHOP_NM", "PRI_SHOP_NM")
        map.Add("INV_REM_NM", "INV_REM_NM")
        map.Add("INV_REM_KANA", "INV_REM_KANA")
        map.Add("DEN_NO", "DEN_NO")
        map.Add("MEI_DEN_NO", "MEI_DEN_NO")
        map.Add("OUTKA_POSI_CD", "OUTKA_POSI_CD")
        map.Add("OUTKA_POSI_NM", "OUTKA_POSI_NM")
        map.Add("OUTKA_POSI_BU_CD_PA", "OUTKA_POSI_BU_CD_PA")
        map.Add("OUTKA_POSI_BU_CD_NAIBU", "OUTKA_POSI_BU_CD_NAIBU")
        map.Add("OUTKA_POSI_BU_NM_PA", "OUTKA_POSI_BU_NM_PA")
        map.Add("OUTKA_POSI_BU_NM_NAIBU", "OUTKA_POSI_BU_NM_NAIBU")
        map.Add("OUTKA_POSI_AD_CD_PA", "OUTKA_POSI_AD_CD_PA")
        map.Add("OUTKA_POSI_AD_NM_PA", "OUTKA_POSI_AD_NM_PA")
        map.Add("OUTKA_POSI_TEL_NO_PA", "OUTKA_POSI_TEL_NO_PA")
        map.Add("OUTKA_POSI_FAX_NO_PA", "OUTKA_POSI_FAX_NO_PA")
        map.Add("UNSO_JURYO", "UNSO_JURYO")
        map.Add("UNSO_JURYO_FLG", "UNSO_JURYO_FLG")
        map.Add("UNIT_LOAD_CD", "UNIT_LOAD_CD")
        map.Add("UNIT_LOAD_SU", "UNIT_LOAD_SU")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_KANA", "REMARK_KANA")
        map.Add("HARAI_KBN", "HARAI_KBN")
        map.Add("DATA_TYPE", "DATA_TYPE")
        map.Add("RTN_FLG", "RTN_FLG")
        map.Add("SND_CANCEL_FLG", "SND_CANCEL_FLG")
        map.Add("OLD_DATA_FLG", "OLD_DATA_FLG")
        map.Add("PRINT_NO", "PRINT_NO")
        map.Add("NRS_SYS_FLG", "NRS_SYS_FLG")
        map.Add("OLD_SYS_FLG", "OLD_SYS_FLG")
        map.Add("RTN_FILE_DATE", "RTN_FILE_DATE")
        map.Add("RTN_FILE_TIME", "RTN_FILE_TIME")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("DELETE_USER", "DELETE_USER")
        map.Add("DELETE_DATE", "DELETE_DATE")
        map.Add("DELETE_TIME", "DELETE_TIME")
        map.Add("DELETE_EDI_NO", "DELETE_EDI_NO")
        map.Add("PRT_USER", "PRT_USER")
        map.Add("PRT_DATE", "PRT_DATE")
        map.Add("PRT_TIME", "PRT_TIME")
        map.Add("EDI_USER", "EDI_USER")
        map.Add("EDI_DATE", "EDI_DATE")
        map.Add("EDI_TIME", "EDI_TIME")
        map.Add("OUTKA_USER", "OUTKA_USER")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("OUTKA_TIME", "OUTKA_TIME")
        map.Add("INKA_USER", "INKA_USER")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("INKA_TIME", "INKA_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("GYO", "GYO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_M", "INKA_CTL_NO_M")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("MEI_NO_P", "MEI_NO_P")
        map.Add("MEI_NO_A", "MEI_NO_A")
        map.Add("JYUCHU_GOODS_CD", "JYUCHU_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("GOODS_KANA1", "GOODS_KANA1")
        map.Add("GOODS_KANA2", "GOODS_KANA2")
        map.Add("NISUGATA_CD", "NISUGATA_CD")
        map.Add("IRISUU", "IRISUU")
        map.Add("LOT_NO_P", "LOT_NO_P")
        map.Add("LOT_NO_A", "LOT_NO_A")
        map.Add("SURY_TANI_CD", "SURY_TANI_CD")
        map.Add("SURY_REQ", "SURY_REQ")
        map.Add("SURY_RPT", "SURY_RPT")
        map.Add("MEI_REM1", "MEI_REM1")
        map.Add("MEI_REM2", "MEI_REM2")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("SEND_USER", "SEND_USER")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("DELETE_EDI_NO_CHU", "DELETE_EDI_NO_CHU")
        map.Add("UNSO_EDI_CTL_NO", "UNSO_EDI_CTL_NO")
        map.Add("UNSO_REMARK", "UNSO_REMARK")
        map.Add("UNSO_REMARK_KANA", "UNSO_REMARK_KANA")
        map.Add("UNSO_CAR_NO_P", "UNSO_CAR_NO_P")
        map.Add("UNSO_CAR_NO_A", "UNSO_CAR_NO_A")
        map.Add("UNSO_DELIVERY_NM", "UNSO_DELIVERY_NM")
        map.Add("UNSO_DELIVERY_SAGYO", "UNSO_DELIVERY_SAGYO")
        map.Add("UNSO_OUTKA_POSI_BU_NM_RYAK", "UNSO_OUTKA_POSI_BU_NM_RYAK")
        map.Add("ACT_UNSO_NM", "ACT_UNSO_NM")
        map.Add("UNSO_ROUTE_NM", "UNSO_ROUTE_NM")
        map.Add("LAST_REC_CNT", "LAST_REC_CNT")
        map.Add("LAST_REC_OUTKA_DATE_A", "LAST_REC_OUTKA_DATE_A")
        map.Add("LAST_REC_ARRIVAL_DATE_A", "LAST_REC_ARRIVAL_DATE_A")
        map.Add("LAST_REC_DELIVERY_SAGYO", "LAST_REC_DELIVERY_SAGYO")
        map.Add("LAST_REC_ORDER_NO", "LAST_REC_ORDER_NO")
        map.Add("LAST_REC_JYUCHU_NO", "LAST_REC_JYUCHU_NO")
        map.Add("LAST_REC_OUTKA_POSI_BU_CD_PA", "LAST_REC_OUTKA_POSI_BU_CD_PA")
        map.Add("LAST_REC_UNSO_ROUTE_A", "LAST_REC_UNSO_ROUTE_A")
        map.Add("LAST_REC_GOODS_KANA1", "LAST_REC_GOODS_KANA1")
        map.Add("LAST_REC_GOODS_KANA2", "LAST_REC_GOODS_KANA2")
        map.Add("LAST_REC_IRISUU", "LAST_REC_IRISUU")
        map.Add("LAST_REC_LOT_NO_A", "LAST_REC_LOT_NO_A")
        map.Add("LAST_REC_SURY_RPT", "LAST_REC_SURY_RPT")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.PRT_OUT)

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ取得：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectPrintDataSetCondition()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '抽出項目
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_NO", .Item("PRINT_NO").ToString(), DBDataType.CHAR))
        End With

        '条件
        Me._StrSql.Append(" WHERE" & vbNewLine)
        With Me._Row
            '固定条件
            Me._StrSql.Append(" HED.SYS_DEL_FLG = '0'" & vbNewLine)
            Me._StrSql.Append(" AND HED.MOD_KBN NOT IN ('E', 'L')" & vbNewLine)
            Me._StrSql.Append(" AND HED.OLD_DATA_FLG = ''" & vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.NRS_BR_CD = @NRS_BR_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'データ種別
            whereStr = .Item("DATA_KIND").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.DATA_KIND = @DATA_KIND" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", whereStr, DBDataType.CHAR))
            End If

            '送受信伝票番号
            whereStr = .Item("SR_DEN_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.SR_DEN_NO = @SR_DEN_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", whereStr, DBDataType.CHAR))
            End If
        End With

        '並び
        Me._StrSql.Append(" ORDER BY" & vbNewLine)
        Me._StrSql.Append(" HED.EDI_CTL_NO," & vbNewLine)
        Me._StrSql.Append(" MEI.EDI_CTL_NO_CHU" & vbNewLine)

    End Sub

#End Region

#Region "プリントフラグ更新"

    ''' <summary>
    ''' プリントフラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrtFlg(ByVal ds As DataSet) As DataSet

        For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.PRT_IN).Rows.Count - 1
            'DataSetのIN情報を取得
            Dim inTbl As DataTable = ds.Tables(TABLE_NM.PRT_IN)

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(dtIdx)

            'SQLの作成
            Me._StrSql = New StringBuilder()
            Me._StrSql.Append(LMI512DAC.SQL_UPDATE_PRTFLG)

            'SQLのコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの設定
            Me._SqlPrmList = New ArrayList()
            With Me._Row
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", JISSI_KBN.SUMI, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_NO", .Item("PRINT_NO").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", .Item("SR_DEN_NO").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
            End With

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        Next

        Return ds

    End Function

#End Region

#End Region 'Method

End Class
