' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF700    : 運送チェックリスト
'  作  成  者       :  hori
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF700DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF700DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索"

    ''' <summary>
    ''' 帳票パターン検索
    ''' </summary>
    ''' <remarks>LMF710:立合書（運送）でも利用</remarks>
    Friend Const SQL_SELECT_MPRT As String = "" _
            & "SELECT                                               " & vbNewLine _
            & "   ISNULL(MR2.NRS_BR_CD, MR1.NRS_BR_CD) AS NRS_BR_CD " & vbNewLine _
            & "  ,ISNULL(MR2.PTN_ID, MR1.PTN_ID) AS PTN_ID          " & vbNewLine _
            & "  ,ISNULL(MR2.PTN_CD, MR1.PTN_CD) AS PTN_CD          " & vbNewLine _
            & "  ,ISNULL(MR2.RPT_ID, MR1.RPT_ID) AS RPT_ID          " & vbNewLine _
            & "FROM                                                 " & vbNewLine _
            & "  $LM_TRN$..F_UNSO_L AS USL                          " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "  $LM_MST$..M_RPT AS MR1                             " & vbNewLine _
            & "  ON  MR1.NRS_BR_CD = USL.NRS_BR_CD                  " & vbNewLine _
            & "  AND MR1.PTN_ID = @PTN_ID                           " & vbNewLine _
            & "  AND MR1.STANDARD_FLAG = '01'                       " & vbNewLine _
            & "  AND MR1.SYS_DEL_FLG = '0'                          " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "  $LM_MST$..M_CUST_RPT AS MCR                        " & vbNewLine _
            & "  ON  MCR.NRS_BR_CD = USL.NRS_BR_CD                  " & vbNewLine _
            & "  AND MCR.CUST_CD_L = USL.CUST_CD_L                  " & vbNewLine _
            & "  AND MCR.CUST_CD_M = USL.CUST_CD_M                  " & vbNewLine _
            & "  AND MCR.CUST_CD_S = '00'                           " & vbNewLine _
            & "  AND MCR.PTN_ID = MR1.PTN_ID                        " & vbNewLine _
            & "  AND MCR.SYS_DEL_FLG = '0'                          " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "  $LM_MST$..M_RPT AS MR2                             " & vbNewLine _
            & "  ON  MR2.NRS_BR_CD = MCR.NRS_BR_CD                  " & vbNewLine _
            & "  AND MR2.PTN_ID = MCR.PTN_ID                        " & vbNewLine _
            & "  AND MR2.PTN_CD = MCR.PTN_CD                        " & vbNewLine _
            & "  AND MR2.SYS_DEL_FLG = '0'                          " & vbNewLine _
            & "WHERE                                                " & vbNewLine _
            & "      USL.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
            & "  AND USL.UNSO_NO_L = @UNSO_NO_L                     " & vbNewLine _
            & "  AND USL.SYS_DEL_FLG = '0'                          " & vbNewLine

    ''' <summary>
    ''' 出力対象データ検索
    ''' </summary>
    ''' <remarks>LMF710:立合書（運送）でも利用</remarks>
    Friend Const SQL_SELECT_PRINT_DATA As String = "" _
            & "SELECT                                                                           " & vbNewLine _
            & "   ISNULL(MR2.RPT_ID, MR1.RPT_ID) AS RPT_ID                                      " & vbNewLine _
            & "  ,USL.NRS_BR_CD                                                                 " & vbNewLine _
            & "  ,'' AS PRINT_SORT                                                              " & vbNewLine _
            & "  ,'0' AS TOU_BETU_FLG                                                           " & vbNewLine _
            & "  ,USL.UNSO_NO_L AS OUTKA_NO_L                                                   " & vbNewLine _
            & "  ,USL.DEST_CD                                                                   " & vbNewLine _
            & "  ,DST.DEST_NM                                                                   " & vbNewLine _
            & "  ,DST.AD_1 AS DEST_AD_1                                                         " & vbNewLine _
            & "  ,DST.AD_2 AS DEST_AD_2                                                         " & vbNewLine _
            & "  ,USL.AD_3 AS DEST_AD_3                                                         " & vbNewLine _
            & "  ,DST.TEL AS DEST_TEL                                                           " & vbNewLine _
            & "  ,USL.CUST_CD_L                                                                 " & vbNewLine _
            & "  ,CST.CUST_NM_L                                                                 " & vbNewLine _
            & "  ,CST.CUST_NM_M                                                                 " & vbNewLine _
            & "  ,CST.CUST_NM_S                                                                 " & vbNewLine _
            & "  ,CST.CUST_NM_S AS CUST_NM_S_H                                                  " & vbNewLine _
            & "  ,USL.UNSO_PKG_NB AS OUTKA_PKG_NB                                               " & vbNewLine _
            & "  ,'' AS CUST_ORD_NO                                                             " & vbNewLine _
            & "  ,'' AS BUYER_ORD_NO                                                            " & vbNewLine _
            & "  ,USL.OUTKA_PLAN_DATE                                                           " & vbNewLine _
            & "  ,USL.ARR_PLAN_DATE                                                             " & vbNewLine _
            & "  ,USL.ARR_PLAN_TIME                                                             " & vbNewLine _
            & "  ,USC.UNSOCO_NM                                                                 " & vbNewLine _
            & "  ,'' AS PC_KB                                                                   " & vbNewLine _
            & "  ,'' AS KYORI                                                                   " & vbNewLine _
            & "  ,USL.UNSO_WT                                                                   " & vbNewLine _
            & "  ,'' AS URIG_NM                                                                 " & vbNewLine _
            & "  ,'' AS FREE_C03                                                                " & vbNewLine _
            & "  ,CASE                                                                          " & vbNewLine _
            & "    WHEN ISNULL(GDD.SET_NAIYO, '') = '1' THEN                                    " & vbNewLine _
            & "      CASE                                                                       " & vbNewLine _
            & "        WHEN ISNULL(DSD.SET_NAIYO, '') = '1' THEN USL.REMARK + ' ' + DSD.REMARK  " & vbNewLine _
            & "        ELSE USL.REMARK                                                          " & vbNewLine _
            & "        END                                                                      " & vbNewLine _
            & "    ELSE USL.REMARK                                                              " & vbNewLine _
            & "    END AS REMARK_L                                                              " & vbNewLine _
            & "  ,USL.REMARK AS REMARK_UNSO                                                     " & vbNewLine _
            & "  ,'' AS REMARK_SIJI                                                             " & vbNewLine _
            & "  ,'' AS SAGYO_REC_NO_1                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_CD_1                                                              " & vbNewLine _
            & "  ,'' AS SAGYO_NM_1                                                              " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_L_1                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_L_1                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_REC_NO_2                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_CD_2                                                              " & vbNewLine _
            & "  ,'' AS SAGYO_NM_2                                                              " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_L_2                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_L_2                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_REC_NO_3                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_CD_3                                                              " & vbNewLine _
            & "  ,'' AS SAGYO_NM_3                                                              " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_L_3                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_L_3                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_REC_NO_4                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_CD_4                                                              " & vbNewLine _
            & "  ,'' AS SAGYO_NM_4                                                              " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_L_4                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_L_4                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_REC_NO_5                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_CD_5                                                              " & vbNewLine _
            & "  ,'' AS SAGYO_NM_5                                                              " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_L_5                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_L_5                                                         " & vbNewLine _
            & "  ,USR.USER_NM AS CRT_USER                                                       " & vbNewLine _
            & "  ,USM.UNSO_NO_M AS OUTKA_NO_M                                                   " & vbNewLine _
            & "  ,USM.GOODS_NM                                                                  " & vbNewLine _
            & "  ,'' AS FREE_C08                                                                " & vbNewLine _
            & "  ,USM.IRIME                                                                     " & vbNewLine _
            & "  ,USM.IRIME_UT                                                                  " & vbNewLine _
            & "--20220610 修正  ,USM.PKG_NB AS KONSU                                                           " & vbNewLine _
            & "  ,USM.UNSO_TTL_NB AS KONSU                                                      " & vbNewLine _
            & "  ,USM.HASU                                                                      " & vbNewLine _
            & "  ,USM.UNSO_TTL_NB AS ALCTD_NB                                                   " & vbNewLine _
            & "  ,USM.NB_UT                                                                     " & vbNewLine _
            & "  ,0 AS ALCTD_CAN_NB                                                             " & vbNewLine _
            & "  ,'' AS FREE_C07                                                                " & vbNewLine _
            & "  ,USM.UNSO_TTL_QT AS ALCTD_QT                                                   " & vbNewLine _
            & "  ,0 AS ZAN_KONSU                                                                " & vbNewLine _
            & "  ,0 AS ZAN_HASU                                                                 " & vbNewLine _
            & "  ,'' AS SERIAL_NO                                                               " & vbNewLine _
            & "  ,USM.PKG_NB                                                                    " & vbNewLine _
            & "  ,GOD.PKG_UT                                                                    " & vbNewLine _
            & "  ,'' AS ALCTD_KB                                                                " & vbNewLine _
            & "  ,'' AS ALCTD_CAN_QT                                                            " & vbNewLine _
            & "  ,'' AS REMARK_OUT                                                              " & vbNewLine _
            & "  ,USM.LOT_NO                                                                    " & vbNewLine _
            & "  ,'' AS LT_DATE                                                                 " & vbNewLine _
            & "  ,'' AS INKA_DATE                                                               " & vbNewLine _
            & "  ,'' AS REMARK_S                                                                " & vbNewLine _
            & "  ,'' AS GOODS_COND_NM_1                                                         " & vbNewLine _
            & "  ,'' AS GOODS_COND_NM_2                                                         " & vbNewLine _
            & "  ,GOD.GOODS_CD_CUST AS GOODS_CD_CUST                                            " & vbNewLine _
            & "  ,USM.BETU_WT                                                                   " & vbNewLine _
            & "  ,'' AS CUST_ORD_NO_DTL                                                         " & vbNewLine _
            & "  ,'' AS BUYER_ORD_NO_DTL                                                        " & vbNewLine _
            & "  ,'' AS TOU_NO                                                                  " & vbNewLine _
            & "  ,'' AS SITU_NO                                                                 " & vbNewLine _
            & "  ,'' AS ZONE_CD                                                                 " & vbNewLine _
            & "  ,'' AS LOCA                                                                    " & vbNewLine _
            & "  ,USM.REMARK AS REMARK_M                                                        " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_REC_NO_1                                                      " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_CD_1                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_NM_1                                                          " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_M_1                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_M_1                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_REC_NO_2                                                      " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_CD_2                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_NM_2                                                          " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_M_2                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_M_2                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_REC_NO_3                                                      " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_CD_3                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_NM_3                                                          " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_M_3                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_M_3                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_REC_NO_4                                                      " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_CD_4                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_NM_4                                                          " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_M_4                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_M_4                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_REC_NO_5                                                      " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_CD_5                                                          " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_NM_5                                                          " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_M_5                                                         " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_M_5                                                         " & vbNewLine _
            & "  ,'' AS SAIHAKKO_FLG                                                            " & vbNewLine _
            & "  ,'' AS OYA_CUST_GOODS_CD                                                       " & vbNewLine _
            & "  ,'' AS OYA_GOODS_NM                                                            " & vbNewLine _
            & "  ,'' AS OYA_KATA                                                                " & vbNewLine _
            & "  ,0 AS OYA_OUTKA_TTL_NB                                                         " & vbNewLine _
            & "  ,GDD.SET_NAIYO AS SET_NAIYO                                                    " & vbNewLine _
            & "  ,OUTKA_PLAN_DATE AS OUTKO_DATE                                                 " & vbNewLine _
            & "  ,USC.UNSOCO_BR_NM                                                              " & vbNewLine _
            & "  ,'' AS GOODS_COND_NM_3                                                         " & vbNewLine _
            & "  ,'' AS RPT_FLG                                                                 " & vbNewLine _
            & "  ,'' AS OUTKA_NO_S                                                              " & vbNewLine _
            & "  ,'' AS WH_CD                                                                   " & vbNewLine _
            & "  ,'' AS CUST_NAIYO_1                                                            " & vbNewLine _
            & "  ,'' AS CUST_NAIYO_2                                                            " & vbNewLine _
            & "  ,'' AS CUST_NAIYO_3                                                            " & vbNewLine _
            & "  ,DST.REMARK AS DEST_REMARK                                                     " & vbNewLine _
            & "  ,DST.SALES_CD AS DEST_SALES_CD                                                 " & vbNewLine _
            & "  ,SLS.CUST_NM_L AS DEST_SALES_NM_L                                              " & vbNewLine _
            & "  ,SLS.CUST_NM_M AS DEST_SALES_NM_M                                              " & vbNewLine _
            & "  ,'' AS ALCTD_NB_HEADKEI                                                        " & vbNewLine _
            & "  ,'' AS ALCTD_QT_HEADKEI                                                        " & vbNewLine _
            & "  ,'' AS HINMEI                                                                  " & vbNewLine _
            & "  ,'' AS NISUGATA                                                                " & vbNewLine _
            & "  ,'' AS SHOBO_CD                                                                " & vbNewLine _
            & "  ,'' AS NHS_REMARK                                                              " & vbNewLine _
            & "  ,0 AS SUM_OUTKA_TTL_NB                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_REC_NO_D1                                                     " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_CD_D1                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_NM_D1                                                         " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_M_D1                                                        " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_M_D1                                                        " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_REC_NO_D2                                                     " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_CD_D2                                                         " & vbNewLine _
            & "  ,'' AS SAGYO_MEI_NM_D2                                                         " & vbNewLine _
            & "  ,'' AS REMARK_SIJI_M_D2                                                        " & vbNewLine _
            & "  ,'' AS WH_SAGYO_YN_M_D2                                                        " & vbNewLine _
            & "  ,USL.CUST_REF_NO                                                               " & vbNewLine _
            & "FROM                                                                             " & vbNewLine _
            & "  $LM_TRN$..F_UNSO_L AS USL                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_DEST AS DST                                                        " & vbNewLine _
            & "  ON  DST.NRS_BR_CD = USL.NRS_BR_CD                                              " & vbNewLine _
            & "  AND DST.CUST_CD_L = USL.CUST_CD_L                                              " & vbNewLine _
            & "  AND DST.DEST_CD = USL.DEST_CD                                                  " & vbNewLine _
            & "  AND DST.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_CUST SLS                                                           " & vbNewLine _
            & "  ON  SLS.NRS_BR_CD = DST.NRS_BR_CD                                              " & vbNewLine _
            & "  AND SLS.CUST_CD_L = DST.SALES_CD                                               " & vbNewLine _
            & "  AND SLS.CUST_CD_M = '00'                                                       " & vbNewLine _
            & "  AND SLS.CUST_CD_S = '00'                                                       " & vbNewLine _
            & "  AND SLS.CUST_CD_SS = '00'                                                      " & vbNewLine _
            & "  AND SLS.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_CUST AS CST                                                        " & vbNewLine _
            & "  ON  CST.NRS_BR_CD = USL.NRS_BR_CD                                              " & vbNewLine _
            & "  AND CST.CUST_CD_L = USL.CUST_CD_L                                              " & vbNewLine _
            & "  AND CST.CUST_CD_M = USL.CUST_CD_M                                              " & vbNewLine _
            & "  AND CST.CUST_CD_S = '00'                                                       " & vbNewLine _
            & "  AND CST.CUST_CD_SS = '00'                                                      " & vbNewLine _
            & "  AND CST.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_UNSOCO AS USC                                                      " & vbNewLine _
            & "  ON  USC.NRS_BR_CD = USL.NRS_BR_CD                                              " & vbNewLine _
            & "  AND USC.UNSOCO_CD = USL.UNSO_CD                                                " & vbNewLine _
            & "  AND USC.UNSOCO_BR_CD = USL.UNSO_BR_CD                                          " & vbNewLine _
            & "  AND USC.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_DEST_DETAILS DSD                                                   " & vbNewLine _
            & "  ON  DSD.NRS_BR_CD = USL.NRS_BR_CD                                              " & vbNewLine _
            & "  AND DSD.CUST_CD_L = USL.CUST_CD_L                                              " & vbNewLine _
            & "  AND DSD.DEST_CD = USL.DEST_CD                                                  " & vbNewLine _
            & "  AND DSD.SUB_KB = '04'                                                          " & vbNewLine _
            & "  AND DSD.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..S_USER USR                                                           " & vbNewLine _
            & "  ON  USR.USER_CD = USL.SYS_ENT_USER                                             " & vbNewLine _
            & "  AND USR.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_TRN$..F_UNSO_M AS USM                                                      " & vbNewLine _
            & "  ON  USM.NRS_BR_CD = USL.NRS_BR_CD                                              " & vbNewLine _
            & "  AND USM.UNSO_NO_L = USL.UNSO_NO_L                                              " & vbNewLine _
            & "  AND USM.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_GOODS GOD                                                          " & vbNewLine _
            & "  ON  GOD.NRS_BR_CD = USM.NRS_BR_CD                                              " & vbNewLine _
            & "  AND GOD.GOODS_CD_NRS = USM.GOODS_CD_NRS                                        " & vbNewLine _
            & "  AND GOD.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN (                                                                      " & vbNewLine _
            & "  SELECT                                                                         " & vbNewLine _
            & "     NRS_BR_CD                                                                   " & vbNewLine _
            & "    ,GOODS_CD_NRS                                                                " & vbNewLine _
            & "    ,MAX(SET_NAIYO) AS SET_NAIYO                                                 " & vbNewLine _
            & "  FROM                                                                           " & vbNewLine _
            & "    $LM_MST$..M_GOODS_DETAILS                                                    " & vbNewLine _
            & "  WHERE                                                                          " & vbNewLine _
            & "        SUB_KB = '42'                                                            " & vbNewLine _
            & "    AND SYS_DEL_FLG = '0'                                                        " & vbNewLine _
            & "  GROUP BY                                                                       " & vbNewLine _
            & "     NRS_BR_CD                                                                   " & vbNewLine _
            & "    ,GOODS_CD_NRS                                                                " & vbNewLine _
            & "  ) AS GDD                                                                       " & vbNewLine _
            & "  ON  GDD.NRS_BR_CD = USM.NRS_BR_CD                                              " & vbNewLine _
            & "  AND GDD.GOODS_CD_NRS = USM.GOODS_CD_NRS                                        " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_RPT AS MR1                                                         " & vbNewLine _
            & "  ON  MR1.NRS_BR_CD = USL.NRS_BR_CD                                              " & vbNewLine _
            & "  AND MR1.PTN_ID = @PTN_ID                                                       " & vbNewLine _
            & "  AND MR1.STANDARD_FLAG = '01'                                                   " & vbNewLine _
            & "  AND MR1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_CUST_RPT AS MCR                                                    " & vbNewLine _
            & "  ON  MCR.NRS_BR_CD = USL.NRS_BR_CD                                              " & vbNewLine _
            & "  AND MCR.CUST_CD_L = USL.CUST_CD_L                                              " & vbNewLine _
            & "  AND MCR.CUST_CD_M = USL.CUST_CD_M                                              " & vbNewLine _
            & "  AND MCR.CUST_CD_S = '00'                                                       " & vbNewLine _
            & "  AND MCR.PTN_ID = MR1.PTN_ID                                                    " & vbNewLine _
            & "  AND MCR.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "LEFT JOIN                                                                        " & vbNewLine _
            & "  $LM_MST$..M_RPT AS MR2                                                         " & vbNewLine _
            & "  ON  MR2.NRS_BR_CD = MCR.NRS_BR_CD                                              " & vbNewLine _
            & "  AND MR2.PTN_ID = MCR.PTN_ID                                                    " & vbNewLine _
            & "  AND MR2.PTN_CD = MCR.PTN_CD                                                    " & vbNewLine _
            & "  AND MR2.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "WHERE                                                                            " & vbNewLine _
            & "      USL.NRS_BR_CD = @NRS_BR_CD                                                 " & vbNewLine _
            & "  AND USL.UNSO_NO_L = @UNSO_NO_L                                                 " & vbNewLine _
            & "  AND USL.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
            & "ORDER BY                                                                         " & vbNewLine _
            & "   USL.NRS_BR_CD                                                                 " & vbNewLine _
            & "  ,USL.UNSO_NO_L                                                                 " & vbNewLine _
            & "  ,USM.UNSO_NO_M                                                                 " & vbNewLine

#End Region '検索

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region 'Field

#Region "Method"

#Region "検索"

    ''' <summary>
    '''帳票パターン検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF700IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF700DAC.SQL_SELECT_MPRT)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", "DE", DBDataType.CHAR))

        'パラメータ反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ発行
        MyBase.Logger.WriteSQLLog("LMF700DAC", "SelectMPrt", cmd)

        'SQL発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()

        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds

    End Function

    ''' <summary>
    ''' 出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF700IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF700DAC.SQL_SELECT_PRINT_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", "DE", DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ発行
        MyBase.Logger.WriteSQLLog("LMF700DAC", "SelectPrintData", cmd)

        'SQL発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()

        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("TOU_BETU_FLG", "TOU_BETU_FLG")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("PC_KB", "PC_KB")
        map.Add("KYORI", "KYORI")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("URIG_NM", "URIG_NM")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("REMARK_UNSO", "REMARK_UNSO")
        map.Add("REMARK_SIJI", "REMARK_SIJI")
        map.Add("SAGYO_REC_NO_1", "SAGYO_REC_NO_1")
        map.Add("SAGYO_CD_1", "SAGYO_CD_1")
        map.Add("SAGYO_NM_1", "SAGYO_NM_1")
        map.Add("REMARK_SIJI_L_1", "REMARK_SIJI_L_1")
        map.Add("WH_SAGYO_YN_L_1", "WH_SAGYO_YN_L_1")
        map.Add("SAGYO_REC_NO_2", "SAGYO_REC_NO_2")
        map.Add("SAGYO_CD_2", "SAGYO_CD_2")
        map.Add("SAGYO_NM_2", "SAGYO_NM_2")
        map.Add("REMARK_SIJI_L_2", "REMARK_SIJI_L_2")
        map.Add("WH_SAGYO_YN_L_2", "WH_SAGYO_YN_L_2")
        map.Add("SAGYO_REC_NO_3", "SAGYO_REC_NO_3")
        map.Add("SAGYO_CD_3", "SAGYO_CD_3")
        map.Add("SAGYO_NM_3", "SAGYO_NM_3")
        map.Add("REMARK_SIJI_L_3", "REMARK_SIJI_L_3")
        map.Add("WH_SAGYO_YN_L_3", "WH_SAGYO_YN_L_3")
        map.Add("SAGYO_REC_NO_4", "SAGYO_REC_NO_4")
        map.Add("SAGYO_CD_4", "SAGYO_CD_4")
        map.Add("SAGYO_NM_4", "SAGYO_NM_4")
        map.Add("REMARK_SIJI_L_4", "REMARK_SIJI_L_4")
        map.Add("WH_SAGYO_YN_L_4", "WH_SAGYO_YN_L_4")
        map.Add("SAGYO_REC_NO_5", "SAGYO_REC_NO_5")
        map.Add("SAGYO_CD_5", "SAGYO_CD_5")
        map.Add("SAGYO_NM_5", "SAGYO_NM_5")
        map.Add("REMARK_SIJI_L_5", "REMARK_SIJI_L_5")
        map.Add("WH_SAGYO_YN_L_5", "WH_SAGYO_YN_L_5")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("ALCTD_CAN_NB", "ALCTD_CAN_NB")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ZAN_KONSU", "ZAN_KONSU")
        map.Add("ZAN_HASU", "ZAN_HASU")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("REMARK_S", "REMARK_S")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
        map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
        map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
        map.Add("REMARK_SIJI_M_1", "REMARK_SIJI_M_1")
        map.Add("WH_SAGYO_YN_M_1", "WH_SAGYO_YN_M_1")
        map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
        map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
        map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
        map.Add("REMARK_SIJI_M_2", "REMARK_SIJI_M_2")
        map.Add("WH_SAGYO_YN_M_2", "WH_SAGYO_YN_M_2")
        map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
        map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
        map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
        map.Add("REMARK_SIJI_M_3", "REMARK_SIJI_M_3")
        map.Add("WH_SAGYO_YN_M_3", "WH_SAGYO_YN_M_3")
        map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
        map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
        map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
        map.Add("REMARK_SIJI_M_4", "REMARK_SIJI_M_4")
        map.Add("WH_SAGYO_YN_M_4", "WH_SAGYO_YN_M_4")
        map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
        map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
        map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
        map.Add("REMARK_SIJI_M_5", "REMARK_SIJI_M_5")
        map.Add("WH_SAGYO_YN_M_5", "WH_SAGYO_YN_M_5")
        map.Add("SAGYO_MEI_REC_NO_D1", "SAGYO_MEI_REC_NO_D1")
        map.Add("SAGYO_MEI_CD_D1", "SAGYO_MEI_CD_D1")
        map.Add("SAGYO_MEI_NM_D1", "SAGYO_MEI_NM_D1")
        map.Add("REMARK_SIJI_M_D1", "REMARK_SIJI_M_D1")
        map.Add("WH_SAGYO_YN_M_D1", "WH_SAGYO_YN_M_D1")
        map.Add("SAGYO_MEI_REC_NO_D2", "SAGYO_MEI_REC_NO_D2")
        map.Add("SAGYO_MEI_CD_D2", "SAGYO_MEI_CD_D2")
        map.Add("SAGYO_MEI_NM_D2", "SAGYO_MEI_NM_D2")
        map.Add("REMARK_SIJI_M_D2", "REMARK_SIJI_M_D2")
        map.Add("WH_SAGYO_YN_M_D2", "WH_SAGYO_YN_M_D2")
        map.Add("SAIHAKKO_FLG", "SAIHAKKO_FLG")
        map.Add("OYA_CUST_GOODS_CD", "OYA_CUST_GOODS_CD")
        map.Add("OYA_GOODS_NM", "OYA_GOODS_NM")
        map.Add("OYA_KATA", "OYA_KATA")
        map.Add("OYA_OUTKA_TTL_NB", "OYA_OUTKA_TTL_NB")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("CUST_NM_S_H", "CUST_NM_S_H")
        map.Add("RPT_FLG", "RPT_FLG")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_NAIYO_1", "CUST_NAIYO_1")
        map.Add("CUST_NAIYO_2", "CUST_NAIYO_2")
        map.Add("CUST_NAIYO_3", "CUST_NAIYO_3")
        map.Add("DEST_REMARK", "DEST_REMARK")
        map.Add("DEST_SALES_CD", "DEST_SALES_CD")
        map.Add("DEST_SALES_NM_L", "DEST_SALES_NM_L")
        map.Add("DEST_SALES_NM_M", "DEST_SALES_NM_M")
        map.Add("ALCTD_NB_HEADKEI", "ALCTD_NB_HEADKEI")
        map.Add("ALCTD_QT_HEADKEI", "ALCTD_QT_HEADKEI")
        map.Add("HINMEI", "HINMEI")
        map.Add("NISUGATA", "NISUGATA")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("CUST_REF_NO", "CUST_REF_NO")           'add 20220610

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF700OUT")

        Return ds

    End Function

#End Region '検索

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region 'ユーティリティ

#End Region 'Method

End Class

