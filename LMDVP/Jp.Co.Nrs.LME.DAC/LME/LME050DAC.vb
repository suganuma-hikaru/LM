' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : 作業
'  プログラムID     :  LME050    : 作業個数引当
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LME050DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LME050DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

    ''' <summary>
    ''' カウント用（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(ZAI.ZAI_REC_NO)		            AS SELECT_CNT       " & vbNewLine

    ''' <summary>
    ''' 在庫データ データ抽出用（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                              " & vbNewLine _
                                            & " ZAI.NRS_BR_CD                                       AS NRS_BR_CD           " & vbNewLine _
                                            & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
                                            & ",ZAI.WH_CD                                           AS WH_CD     	       " & vbNewLine _
                                            & ",ZAI.TOU_NO                                          AS TOU_NO              " & vbNewLine _
                                            & ",ZAI.SITU_NO                                         AS SITU_NO	           " & vbNewLine _
                                            & ",ZAI.ZONE_CD                                         AS ZONE_CD             " & vbNewLine _
                                            & ",ZAI.LOCA                                            AS LOCA                " & vbNewLine _
                                            & ",ZAI.LOT_NO                                          AS LOT_NO              " & vbNewLine _
                                            & ",ZAI.CUST_CD_L                                       AS CUST_CD_L           " & vbNewLine _
                                            & ",ZAI.CUST_CD_M                                       AS CUST_CD_M           " & vbNewLine _
                                            & ",ZAI.GOODS_CD_NRS                                    AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
                                            & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
                                            & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
                                            & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
                                            & ",ZAI.RSV_NO                                          AS RSV_NO              " & vbNewLine _
                                            & ",ZAI.SERIAL_NO                                       AS SERIAL_NO           " & vbNewLine _
                                            & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
                                            & ",ZAI.TAX_KB                                          AS TAX_KB              " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
                                            & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
                                            & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
                                            & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_NB                                     AS PORA_ZAI_NB         " & vbNewLine _
                                            & ",ZAI.ALCTD_NB                                        AS ALCTD_NB            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB        " & vbNewLine _
                                            & ",ZAI.IRIME                                           AS IRIME               " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_QT                                     AS PORA_ZAI_QT         " & vbNewLine _
                                            & ",ZAI.ALCTD_QT                                        AS ALCTD_QT            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT        " & vbNewLine _
                                            & ",ZAI.INKO_DATE                                       AS INKO_DATE           " & vbNewLine _
                                            & ",ZAI.INKO_PLAN_DATE                                  AS INKO_PLAN_DATE      " & vbNewLine _
                                            & ",ZAI.ZERO_FLAG                                       AS ZERO_FLAG           " & vbNewLine _
                                            & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
                                            & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
                                            & ",ZAI.DEST_CD_P                                       AS DEST_CD_P           " & vbNewLine _
                                            & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
                                            & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG           " & vbNewLine _
                                            & ",Z1.KBN_NM1                                          AS GOODS_COND_NM_1     " & vbNewLine _
                                            & ",Z2.KBN_NM1                                          AS GOODS_COND_NM_2     " & vbNewLine _
                                            & ",CUSTCOND.JOTAI_NM                                   AS GOODS_COND_NM_3     " & vbNewLine _
                                            & ",Z3.KBN_NM1                                          AS ALLOC_PRIORITY_NM   " & vbNewLine _
                                            & ",Z4.KBN_NM1                                          AS OFB_KB_NM           " & vbNewLine _
                                            & ",Z5.KBN_NM1                                          AS SPD_KB_NM           " & vbNewLine _
                                            & ",Z6.KBN_NM1                                          AS HIKIATE_ALERT_NM    " & vbNewLine _
                                            & ",Z7.KBN_NM1                                          AS TAX_KB_NM           " & vbNewLine _
                                            & ",Z8.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
                                            & ",Z9.KBN_NM1                                          AS IRIME_UT_NM         " & vbNewLine _
                                            & ",GOODS.CONSUME_PERIOD_DATE                           AS CONSUME_PERIOD_DATE " & vbNewLine _
                                            & ",''                                                  AS BUYER_ORD_NO_DTL    " & vbNewLine _
                                            & ",DEST.DEST_NM                                        AS DEST_NM             " & vbNewLine _
                                            & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
                                            & ",GOODS.GOODS_NM_1                                    AS GOODS_NM_1          " & vbNewLine _
                                            & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
                                            & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
                                            & ",GOODS.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
                                            & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
                                            & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
                                            & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
                                            & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
                                            & ",GOODS.HIKIATE_ALERT_YN                              AS HIKIATE_ALERT_YN    " & vbNewLine _
                                            & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
                                            & ",GOODS.STD_IRIME_UT                                  AS STD_IRIME_UT        " & vbNewLine _
                                            & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
                                            & ",INKA_L.INKA_DATE                                    AS INKA_DATE           " & vbNewLine _
                                            & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
                                            & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",''                                                  AS GOODS_CD_NRS_FROM   " & vbNewLine _
                                            & ",IDO.IDO_DATE                                        AS IDO_DATE            " & vbNewLine _
                                            & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
                                            & ",GOODS.COA_YN                                        AS COA_YN              " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_1                         AS OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_2                         AS OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_3                         AS OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_4                         AS OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_5                         AS OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
                                            & ",INKA_L.INKA_STATE_KB                                AS INKA_STATE_KB       " & vbNewLine _
                                            & ",CASE WHEN ZAI.INKO_DATE IS NULL OR ZAI.INKO_DATE = ''                      " & vbNewLine _
                                            & "      THEN '1'                                                              " & vbNewLine _
                                            & "      ELSE '0'                                                              " & vbNewLine _
                                            & " END                                                 AS YOJITU              " & vbNewLine _
                                            & ",GOODS.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
                                            & ",GOODS.CUST_CD_L                                     AS CUST_CD_L_GOODS     " & vbNewLine _
                                            & ",GOODS.CUST_CD_M                                     AS CUST_CD_M_GOODS     " & vbNewLine _
                                            & ",CASE WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO <> ''             " & vbNewLine _
                                            & "      THEN INKA_L.INKA_DATE                                                 " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO = ''              " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO <> ''             " & vbNewLine _
                                            & "      THEN INKA_L.INKA_DATE                                                 " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO = ''              " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      ELSE INKA_L.INKA_DATE                                                 " & vbNewLine _
                                            & " END                                                 AS INKA_DATE2          " & vbNewLine _
                                            & ",GOODSDETAILS.SET_NAIYO                              AS INKA_DATE_KANRI_KB  " & vbNewLine

    ''' <summary>
    ''' FROM（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                            " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSTCOND                " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = CUSTCOND.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = CUSTCOND.CUST_CD_L                 " & vbNewLine _
                                         & "AND    ZAI.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                    " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S005'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                    " & vbNewLine _
                                         & "AND    Z2.KBN_GROUP_CD = 'S006'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     ZAI.ALLOC_PRIORITY = Z3.KBN_CD                     " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'W001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z4                           " & vbNewLine _
                                         & "ON     ZAI.OFB_KB = Z4.KBN_CD                             " & vbNewLine _
                                         & "AND    Z4.KBN_GROUP_CD = 'B002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z5                           " & vbNewLine _
                                         & "ON     ZAI.SPD_KB = Z5.KBN_CD                             " & vbNewLine _
                                         & "AND    Z5.KBN_GROUP_CD = 'H003'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS  GOODS                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = GOODS.NRS_BR_CD                    " & vbNewLine _
                                         & "AND    ZAI.GOODS_CD_NRS = GOODS.GOODS_CD_NRS              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z6                           " & vbNewLine _
                                         & "ON     GOODS.HIKIATE_ALERT_YN = Z6.KBN_CD                 " & vbNewLine _
                                         & "AND    Z6.KBN_GROUP_CD = 'U009'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN AS Z7                           " & vbNewLine _
                                         & "ON     ZAI.TAX_KB  = Z7.KBN_CD                            " & vbNewLine _
                                         & "AND    Z7.KBN_GROUP_CD = 'Z001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z8                           " & vbNewLine _
                                         & "ON     GOODS.NB_UT = Z8.KBN_CD                            " & vbNewLine _
                                         & "AND    Z8.KBN_GROUP_CD = 'K002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z9                           " & vbNewLine _
                                         & "ON     GOODS.STD_IRIME_UT  = Z9.KBN_CD                    " & vbNewLine _
                                         & "AND    Z9.KBN_GROUP_CD = 'I001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST    DEST                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = DEST.NRS_BR_CD                     " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = DEST.CUST_CD_L                     " & vbNewLine _
                                         & "AND    ZAI.DEST_CD_P = DEST.DEST_CD                       " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L AS INKA_L                    " & vbNewLine _
                                         & "ON  INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
                                         & "AND INKA_L.INKA_NO_L = ZAI.INKA_NO_L                      " & vbNewLine _
                                         & "AND INKA_L.SYS_DEL_FLG = 0                                " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS IDO                         " & vbNewLine _
                                         & "ON  IDO.NRS_BR_CD = ZAI.NRS_BR_CD                         " & vbNewLine _
                                         & "AND IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                     " & vbNewLine _
                                         & "AND IDO.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                         & "AND IDO.REC_NO <> ''                                      " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODSDETAILS          " & vbNewLine _
                                         & "ON  GOODSDETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                " & vbNewLine _
                                         & "AND GOODSDETAILS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS          " & vbNewLine _
                                         & "AND GOODSDETAILS.SUB_KB = '09'                            " & vbNewLine

    ''' <summary>
    ''' GROUP BY（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY As String = "GROUP BY                                          " & vbNewLine _
                                     & " ZAI.NRS_BR_CD                                                  " & vbNewLine _
                                     & ",ZAI.ZAI_REC_NO                                                 " & vbNewLine _
                                     & ",ZAI.WH_CD                                                      " & vbNewLine _
                                     & ",ZAI.TOU_NO                                                     " & vbNewLine _
                                     & ",ZAI.SITU_NO                                                    " & vbNewLine _
                                     & ",ZAI.ZONE_CD                                                    " & vbNewLine _
                                     & ",ZAI.LOCA                                                       " & vbNewLine _
                                     & ",ZAI.LOT_NO                                                     " & vbNewLine _
                                     & ",ZAI.CUST_CD_L                                                  " & vbNewLine _
                                     & ",ZAI.CUST_CD_M                                                  " & vbNewLine _
                                     & ",ZAI.GOODS_CD_NRS                                               " & vbNewLine _
                                     & ",ZAI.INKA_NO_L                                                  " & vbNewLine _
                                     & ",ZAI.INKA_NO_M                                                  " & vbNewLine _
                                     & ",ZAI.INKA_NO_S                                                  " & vbNewLine _
                                     & ",ZAI.ALLOC_PRIORITY                                             " & vbNewLine _
                                     & ",ZAI.RSV_NO                                                     " & vbNewLine _
                                     & ",ZAI.SERIAL_NO                                                  " & vbNewLine _
                                     & ",ZAI.HOKAN_YN                                                   " & vbNewLine _
                                     & ",ZAI.TAX_KB                                                     " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_1                                            " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_2                                            " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_3                                            " & vbNewLine _
                                     & ",ZAI.OFB_KB                                                     " & vbNewLine _
                                     & ",ZAI.SPD_KB                                                     " & vbNewLine _
                                     & ",ZAI.REMARK_OUT                                                 " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_NB                                                " & vbNewLine _
                                     & ",ZAI.ALCTD_NB                                                   " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_NB                                               " & vbNewLine _
                                     & ",ZAI.IRIME                                                      " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_QT                                                " & vbNewLine _
                                     & ",ZAI.ALCTD_QT                                                   " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_QT                                               " & vbNewLine _
                                     & ",ZAI.INKO_DATE                                                  " & vbNewLine _
                                     & ",ZAI.INKO_PLAN_DATE                                             " & vbNewLine _
                                     & ",ZAI.ZERO_FLAG                                                  " & vbNewLine _
                                     & ",ZAI.LT_DATE                                                    " & vbNewLine _
                                     & ",ZAI.GOODS_CRT_DATE                                             " & vbNewLine _
                                     & ",ZAI.DEST_CD_P                                                  " & vbNewLine _
                                     & ",ZAI.REMARK                                                     " & vbNewLine _
                                     & ",ZAI.SMPL_FLAG                                                  " & vbNewLine _
                                     & ",Z1.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z2.KBN_NM1                                                     " & vbNewLine _
                                     & ",CUSTCOND.JOTAI_NM                                              " & vbNewLine _
                                     & ",Z3.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z4.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z5.KBN_NM1                                                     " & vbNewLine _
                                     & ",GOODS.CONSUME_PERIOD_DATE                                      " & vbNewLine _
                                     & ",Z6.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z7.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z8.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z9.KBN_NM1                                                     " & vbNewLine _
                                     & ",DEST.DEST_NM                                                   " & vbNewLine _
                                     & ",GOODS.GOODS_CD_CUST                                            " & vbNewLine _
                                     & ",GOODS.GOODS_NM_1                                               " & vbNewLine _
                                     & ",GOODS.OUTKA_ATT                                                " & vbNewLine _
                                     & ",GOODS.SEARCH_KEY_1                                             " & vbNewLine _
                                     & ",GOODS.UNSO_ONDO_KB                                             " & vbNewLine _
                                     & ",GOODS.PKG_UT                                                   " & vbNewLine _
                                     & ",GOODS.STD_IRIME_NB                                             " & vbNewLine _
                                     & ",GOODS.STD_WT_KGS                                               " & vbNewLine _
                                     & ",GOODS.TARE_YN                                                  " & vbNewLine _
                                     & ",GOODS.HIKIATE_ALERT_YN                                         " & vbNewLine _
                                     & ",GOODS.PKG_NB                                                   " & vbNewLine _
                                     & ",GOODS.STD_IRIME_UT                                             " & vbNewLine _
                                     & ",GOODS.NB_UT                                                    " & vbNewLine _
                                     & ",INKA_L.INKA_DATE                                               " & vbNewLine _
                                     & ",GOODS.CUST_CD_S                                                " & vbNewLine _
                                     & ",GOODS.CUST_CD_SS                                               " & vbNewLine _
                                     & ",ZAI.SYS_UPD_DATE                                               " & vbNewLine _
                                     & ",ZAI.SYS_UPD_TIME                                               " & vbNewLine _
                                     & ",IDO.IDO_DATE                                                   " & vbNewLine _
                                     & ",INKA_L.INKO_DATE                                               " & vbNewLine _
                                     & ",INKA_L.HOKAN_STR_DATE                                          " & vbNewLine _
                                     & ",GOODS.COA_YN                                                   " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_1                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_2                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_3                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_4                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_5                                    " & vbNewLine _
                                     & ",INKA_L.INKA_STATE_KB                                           " & vbNewLine _
                                     & ",GOODS.SIZE_KB                                                  " & vbNewLine _
                                     & ",GOODS.CUST_CD_L                                                " & vbNewLine _
                                     & ",GOODS.CUST_CD_M                                                " & vbNewLine _
                                     & ",INKA_L.FURI_NO                                                 " & vbNewLine _
                                     & ",GOODSDETAILS.SET_NAIYO                                         " & vbNewLine

    ''' <summary>
    ''' ORDER BY（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY As String = "ORDER BY                                          " & vbNewLine _
                                         & " ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine

#End Region

#End Region

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

    ''' <summary>
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクションスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME050IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        'SQL作成
        _StrSql.Append(LME050DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        _StrSql.Append(LME050DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME050DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME050IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        'SQL作成
        _StrSql.Append(LME050DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        _StrSql.Append(LME050DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)
        _StrSql.Append(LME050DAC.SQL_SELECT_GROUP_BY)  'SQL構築(データ抽出用GROUP BY句)
        _StrSql.Append(LME050DAC.SQL_SELECT_ORDER_BY)  'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)
        '20111001----------------------------
        cmd.CommandTimeout = 6000
        '------------------------------------
        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME050DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("IRIME", "IRIME")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ALLOC_CAN_QT", "ALLOC_CAN_QT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("INKO_PLAN_DATE", "INKO_PLAN_DATE")
        map.Add("ZERO_FLAG", "ZERO_FLAG")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("DEST_CD_P", "DEST_CD_P")
        map.Add("REMARK", "REMARK")
        map.Add("SMPL_FLAG", "SMPL_FLAG")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("ALLOC_PRIORITY_NM", "ALLOC_PRIORITY_NM")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("SPD_KB_NM", "SPD_KB_NM")
        map.Add("HIKIATE_ALERT_NM", "HIKIATE_ALERT_NM")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("IRIME_UT_NM", "IRIME_UT_NM")
        map.Add("CONSUME_PERIOD_DATE", "CONSUME_PERIOD_DATE")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("IDO_DATE", "IDO_DATE")
        map.Add("HOKAN_STR_DATE", "HOKAN_STR_DATE")
        map.Add("COA_YN", "COA_YN")
        map.Add("OUTKA_KAKO_SAGYO_KB_1", "OUTKA_KAKO_SAGYO_KB_1")
        map.Add("OUTKA_KAKO_SAGYO_KB_2", "OUTKA_KAKO_SAGYO_KB_2")
        map.Add("OUTKA_KAKO_SAGYO_KB_3", "OUTKA_KAKO_SAGYO_KB_3")
        map.Add("OUTKA_KAKO_SAGYO_KB_4", "OUTKA_KAKO_SAGYO_KB_4")
        map.Add("OUTKA_KAKO_SAGYO_KB_5", "OUTKA_KAKO_SAGYO_KB_5")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("CUST_CD_L_GOODS", "CUST_CD_L_GOODS")
        map.Add("CUST_CD_M_GOODS", "CUST_CD_M_GOODS")
        map.Add("INKA_DATE2", "INKA_DATE2")
        map.Add("INKA_DATE_KANRI_KB", "INKA_DATE_KANRI_KB")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LME050OUT_ZAI")

        Return ds

    End Function

    ''' <summary>
    ''' 検索用SQL WHERE句作成
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhere()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList()

        _StrSql.Append("WHERE                                                        ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append("ZAI.SYS_DEL_FLG = '0'                                        ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append(" AND ZAI.ALLOC_CAN_NB > 0                                    ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append(" AND ZAI.ZAI_REC_NO <> ''                                    ")
        _StrSql.Append(vbNewLine)

        With _Row
            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.NRS_BR_CD = @NRS_BR_CD                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.WH_CD = @WH_CD                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.CUST_CD_L = @CUST_CD_L                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.CUST_CD_M = @CUST_CD_M                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品コード（商品キー）
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_CD_NRS = @GOODS_CD_NRS                        ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品コード（商品コード）
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GOODS.GOODS_CD_CUST = @GOODS_CD_CUST                        ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat(whereStr), DBDataType.NVARCHAR))
            End If

            '商品名（商品名）
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GOODS.GOODS_NM_1 = @GOODS_NM                        ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat(whereStr), DBDataType.NVARCHAR))
            End If

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.SERIAL_NO LIKE @SERIAL_NO                           ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '予約番号
            whereStr = .Item("RSV_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.RSV_NO = @RSV_NO                                    ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", String.Concat(whereStr), DBDataType.NVARCHAR))
            End If

            'ロット番号
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.LOT_NO LIKE @LOT_NO                                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '入目
            whereStr = .Item("IRIME").ToString()
            If String.IsNullOrEmpty(whereStr) = False AndAlso 0 < Convert.ToDouble(whereStr) Then
                _StrSql.Append(" AND ZAI.IRIME = @IRIME                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '棟
            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.TOU_NO LIKE @TOU_NO                                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '室
            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.SITU_NO LIKE @SITU_NO                               ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ゾーン
            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.ZONE_CD LIKE @ZONE_CD                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ロケーション
            whereStr = .Item("LOCA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.LOCA LIKE @LOCA                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品状態区分1
            whereStr = .Item("GOODS_COND_KB_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_1 = @GOODS_COND_KB_1                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品状態区分2
            whereStr = .Item("GOODS_COND_KB_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_2 = @GOODS_COND_KB_2                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品状態区分3
            whereStr = .Item("GOODS_COND_KB_3").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_3 = @GOODS_COND_KB_3                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", String.Concat(whereStr), DBDataType.CHAR))
            End If

            'REMARK
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.REMARK LIKE @REMARK                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '簿外品区分
            whereStr = .Item("OFB_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.OFB_KB = @OFB_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '保留品区分
            whereStr = .Item("SPD_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.SPD_KB = @SPD_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            'REMARK_OUT
            whereStr = .Item("REMARK_OUT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.REMARK_OUT LIKE @REMARK_OUT                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '在庫レコード番号
            whereStr = .Item("ZAI_REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.ZAI_REC_NO LIKE @ZAI_REC_NO                        ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '課税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.TAX_KB = @TAX_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '引当注意品
            whereStr = .Item("HIKIATE_ALERT_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GOODS.HIKIATE_ALERT_YN = @HIKIATE_ALERT_YN                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@HIKIATE_ALERT_YN", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND (DEST.DEST_NM LIKE @DEST_NM                                   ")
                _StrSql.Append(" OR ZAI.DEST_CD_P = '')                                      ")
                _StrSql.Append(vbNewLine)
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            Else
                _StrSql.Append(" AND ZAI.DEST_CD_P = ''                                      ")
                _StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

#End Region

#Region "変更処理"


#End Region

#Region "設定処理"

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

#End Region

End Class
