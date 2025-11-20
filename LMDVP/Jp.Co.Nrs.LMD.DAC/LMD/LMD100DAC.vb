' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD100    : 在庫テーブル照会
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMD100DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD100DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(ZAI.NRS_BR_CD)		     AS SELECT_CNT                " & vbNewLine

    'START YANAI 要望番号499
    '''' <summary>
    '''' 検索SELECT句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                             " & vbNewLine _
    '                                        & " ISNULL(GDS.GOODS_CD_CUST,'')         AS GOODS_CD_CUST             " & vbNewLine _
    '                                        & ",ZAI.LOT_NO                           AS LOT_NO                    " & vbNewLine _
    '                                        & ",ZAI.IRIME                            AS IRIME                     " & vbNewLine _
    '                                        & ",ISNULL(GDS.STD_IRIME_UT,'')          AS IRIME_UT                  " & vbNewLine _
    '                                        & ",ISNULL((SELECT                                                    " & vbNewLine _
    '                                        & "       KBN1.KBN_NM1                                                " & vbNewLine _
    '                                        & "  FROM $LM_MST$..Z_KBN KBN1                                        " & vbNewLine _
    '                                        & "  WHERE KBN1.KBN_GROUP_CD = 'I001'                                 " & vbNewLine _
    '                                        & "  AND   KBN1.KBN_CD = GDS.STD_IRIME_UT                             " & vbNewLine _
    '                                        & "   ),'')   AS IRIME_UT_NM                                          " & vbNewLine _
    '                                        & ",ZAI.ALLOC_CAN_NB                     AS ALLOC_CAN_NB              " & vbNewLine _
    '                                        & ",ISNULL(GDS.NB_UT,'')                 AS NB_UT                     " & vbNewLine _
    '                                        & ",ISNULL((SELECT                                                    " & vbNewLine _
    '                                        & "       KBN2.KBN_NM1                                                " & vbNewLine _
    '                                        & " FROM $LM_MST$..Z_KBN KBN2                                         " & vbNewLine _
    '                                        & " WHERE KBN2.KBN_GROUP_CD = 'K002'                                  " & vbNewLine _
    '                                        & " AND   KBN2.KBN_CD = GDS.NB_UT                                     " & vbNewLine _
    '                                        & "  ),'')    AS NB_UT_NM                                             " & vbNewLine _
    '                                        & ",GDS.PKG_NB                           AS PKG_NB                    " & vbNewLine _
    '                                        & ",ISNULL(GDS.PKG_UT,'')                AS PKG_UT                    " & vbNewLine _
    '                                        & ",ISNULL((SELECT                                                    " & vbNewLine _
    '                                        & "       KBN3.KBN_NM1                                                " & vbNewLine _
    '                                        & "  FROM $LM_MST$..Z_KBN KBN3                                        " & vbNewLine _
    '                                        & "  WHERE KBN3.KBN_GROUP_CD = 'N001'                                 " & vbNewLine _
    '                                        & "  AND   KBN3.KBN_CD = GDS.PKG_UT                                   " & vbNewLine _
    '                                        & "   ),'')   AS PKG_UT_NM                                            " & vbNewLine _
    '                                        & ",ZAI.REMARK                           AS REMARK                    " & vbNewLine _
    '                                        & ",ZAI.REMARK_OUT                       AS REMARK_OUT                " & vbNewLine _
    '                                        & ",ISNULL((SELECT                                                    " & vbNewLine _
    '                                        & "       KBN4.KBN_NM1                                                " & vbNewLine _
    '                                        & "  FROM $LM_MST$..Z_KBN KBN4                                        " & vbNewLine _
    '                                        & "  WHERE KBN4.KBN_GROUP_CD = 'Z001'                                 " & vbNewLine _
    '                                        & "  AND   KBN4.KBN_CD = ZAI.TAX_KB                                   " & vbNewLine _
    '                                        & "   ),'')   AS TAX_KB_NM                                            " & vbNewLine _
    '                                        & ",ISNULL(MAX(GDS.HIKIATE_ALERT_YN),'') AS HIKIATE_ALERT_YN          " & vbNewLine _
    '                                        & ",ISNULL((SELECT                                                    " & vbNewLine _
    '                                        & "       KBN5.KBN_NM1                                                " & vbNewLine _
    '                                        & "  FROM $LM_MST$..Z_KBN KBN5                                        " & vbNewLine _
    '                                        & "  WHERE KBN5.KBN_GROUP_CD = 'U009'                                 " & vbNewLine _
    '                                        & "  AND   KBN5.KBN_CD = GDS.HIKIATE_ALERT_YN                         " & vbNewLine _
    '                                        & "   ),'')   AS HIKIATE_ALERT_NM                                     " & vbNewLine _
    '                                        & ",ZAI.SMPL_FLAG                        AS SMPL                      " & vbNewLine _
    '                                         & ",ZAI.ZAI_REC_NO                       AS ZAI_REC_NO                " & vbNewLine _
    '                                        & ",ZAI.TOU_NO                           AS TOU_NO                    " & vbNewLine _
    '                                        & ",ZAI.SITU_NO                          AS SITU_NO                   " & vbNewLine _
    '                                        & ",ZAI.ZONE_CD                          AS ZONE_CD                   " & vbNewLine _
    '                                        & ",ZAI.LOCA                             AS LOCA                      " & vbNewLine _
    '                                        & ",ZAI.CUST_CD_L                        AS CUST_CD_L                 " & vbNewLine _
    '                                        & ",ZAI.CUST_CD_M                        AS CUST_CD_M                 " & vbNewLine _
    '                                        & ",ZAI.GOODS_CD_NRS                     AS GOODS_CD_NRS              " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_L                        AS INKA_NO_L                 " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_M                        AS INKA_NO_M                 " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_S                        AS INKA_NO_S                 " & vbNewLine _
    '                                        & ",ZAI.ALLOC_PRIORITY                   AS ALLOC_PRIORITY            " & vbNewLine _
    '                                        & ",ZAI.RSV_NO                           AS RSV_NO                    " & vbNewLine _
    '                                        & ",ZAI.SERIAL_NO                        AS SERIAL_NO                 " & vbNewLine _
    '                                        & ",ZAI.HOKAN_YN                         AS HOKAN_YN                  " & vbNewLine _
    '                                        & ",ZAI.TAX_KB                           AS TAX_KB                    " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_1                  AS GOODS_COND_KB_1           " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_2                  AS GOODS_COND_KB_2           " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_3                  AS GOODS_COND_KB_3           " & vbNewLine _
    '                                        & ",ZAI.OFB_KB                           AS OFB_KB                    " & vbNewLine _
    '                                        & ",ZAI.SPD_KB                           AS SPD_KB                    " & vbNewLine _
    '                                        & ",ZAI.PORA_ZAI_NB                      AS PORA_ZAI_NB               " & vbNewLine _
    '                                        & ",ZAI.ALCTD_NB                         AS ALCTD_NB                  " & vbNewLine _
    '                                        & ",ZAI.PORA_ZAI_QT                      AS PORA_ZAI_QT               " & vbNewLine _
    '                                        & ",ZAI.ALCTD_QT                         AS ALCTD_QT                  " & vbNewLine _
    '                                        & ",ZAI.ALLOC_CAN_QT                     AS ALLOC_CAN_QT              " & vbNewLine _
    '                                        & ",ZAI.INKO_DATE                        AS INKO_DATE                 " & vbNewLine _
    '                                        & ",ZAI.INKO_PLAN_DATE                   AS INKO_PLAN_DATE            " & vbNewLine _
    '                                        & ",ZAI.ZERO_FLAG                        AS ZERO_FLAG                 " & vbNewLine _
    '                                        & ",ZAI.LT_DATE                          AS LT_DATE                   " & vbNewLine _
    '                                        & ",ZAI.GOODS_CRT_DATE                   AS GOODS_CRT_DATE            " & vbNewLine _
    '                                        & ",ZAI.DEST_CD_P                        AS DEST_CD                   " & vbNewLine _
    '                                        & ",ZAI.SMPL_FLAG                        AS SMPL_FLAG                 " & vbNewLine _
    '                                        & ",ZAI.NRS_BR_CD                        AS NRS_BR_CD                 " & vbNewLine _
    '                                        & ",ZAI.WH_CD                            AS WH_CD                     " & vbNewLine _
    '                                        & ",GDS.CUST_CD_S                        AS CUST_CD_S                 " & vbNewLine _
    '                                        & ",GDS.CUST_CD_SS                       AS CUST_CD_SS                " & vbNewLine _
    '                                        & ",GDS.SEARCH_KEY_1                    AS SEARCH_KEY_1              " & vbNewLine _
    '                                        & ",GDS.SEARCH_KEY_2                    AS SEARCH_KEY_2              " & vbNewLine _
    '                                        & ",GDS.CUST_COST_CD1                   AS CUST_COST_CD1             " & vbNewLine _
    '                                        & ",GDS.CUST_COST_CD2                   AS CUST_COST_CD2             " & vbNewLine _
    '                                        & ",GDS.JAN_CD                          AS JAN_CD                    " & vbNewLine _
    '                                        & ",GDS.GOODS_NM_1                      AS GOODS_NM_1                " & vbNewLine _
    '                                        & ",GDS.GOODS_NM_2                      AS GOODS_NM_2                " & vbNewLine _
    '                                        & ",GDS.GOODS_NM_3                      AS GOODS_NM_3                " & vbNewLine _
    '                                        & ",GDS.UP_GP_CD_1                      AS UP_GP_CD_1                " & vbNewLine _
    '                                        & ",GDS.SHOBO_CD                        AS SHOBO_CD                  " & vbNewLine _
    '                                        & ",GDS.KIKEN_KB                        AS KIKEN_KB                  " & vbNewLine _
    '                                        & ",GDS.UN                              AS UN                        " & vbNewLine _
    '                                        & ",GDS.PG_KB                           AS PG_KB                     " & vbNewLine _
    '                                        & ",GDS.CLASS_1                         AS CLASS_1                   " & vbNewLine _
    '                                        & ",GDS.CLASS_2                         AS CLASS_2                   " & vbNewLine _
    '                                        & ",GDS.CLASS_3                         AS CLASS_3                   " & vbNewLine _
    '                                        & ",GDS.CHEM_MTRL_KB                    AS CHEM_MTRL_KB              " & vbNewLine _
    '                                        & ",GDS.DOKU_KB                         AS DOKU_KB                   " & vbNewLine _
    '                                        & ",GDS.GAS_KANRI_KB                    AS GAS_KANRI_KB              " & vbNewLine _
    '                                        & ",GDS.ONDO_KB                         AS ONDO_KB                   " & vbNewLine _
    '                                        & ",GDS.UNSO_ONDO_KB                    AS UNSO_ONDO_KB              " & vbNewLine _
    '                                        & ",GDS.ONDO_MX                         AS ONDO_MX                   " & vbNewLine _
    '                                        & ",GDS.ONDO_MM                         AS ONDO_MM                   " & vbNewLine _
    '                                        & ",GDS.ONDO_STR_DATE                   AS ONDO_STR_DATE             " & vbNewLine _
    '                                        & ",GDS.ONDO_END_DATE                   AS ONDO_END_DATE             " & vbNewLine _
    '                                        & ",GDS.ONDO_UNSO_STR_DATE              AS ONDO_UNSO_STR_DATE        " & vbNewLine _
    '                                        & ",GDS.ONDO_UNSO_END_DATE              AS ONDO_UNSO_END_DATE        " & vbNewLine _
    '                                        & ",GDS.KYOKAI_GOODS_KB                 AS KYOKAI_GOODS_KB           " & vbNewLine _
    '                                        & ",GDS.ALCTD_KB                        AS ALCTD_KB                  " & vbNewLine _
    '                                        & ",GDS.PLT_PER_PKG_UT                  AS PLT_PER_PKG_UT            " & vbNewLine _
    '                                        & ",GDS.STD_IRIME_NB                    AS STD_IRIME_NB              " & vbNewLine _
    '                                        & ",GDS.STD_IRIME_UT                    AS STD_IRIME_UT              " & vbNewLine _
    '                                        & ",GDS.STD_WT_KGS                      AS STD_WT_KGS                " & vbNewLine _
    '                                        & ",GDS.STD_CBM                         AS STD_CBM                   " & vbNewLine _
    '                                        & ",GDS.INKA_KAKO_SAGYO_KB_1            AS INKA_KAKO_SAGYO_KB_1      " & vbNewLine _
    '                                        & ",GDS.INKA_KAKO_SAGYO_KB_2            AS INKA_KAKO_SAGYO_KB_2      " & vbNewLine _
    '                                        & ",GDS.INKA_KAKO_SAGYO_KB_3            AS INKA_KAKO_SAGYO_KB_3      " & vbNewLine _
    '                                        & ",GDS.INKA_KAKO_SAGYO_KB_4            AS INKA_KAKO_SAGYO_KB_4      " & vbNewLine _
    '                                        & ",GDS.INKA_KAKO_SAGYO_KB_5            AS INKA_KAKO_SAGYO_KB_5      " & vbNewLine _
    '                                        & ",GDS.OUTKA_KAKO_SAGYO_KB_1           AS OUTKA_KAKO_SAGYO_KB_1     " & vbNewLine _
    '                                        & ",GDS.OUTKA_KAKO_SAGYO_KB_2           AS OUTKA_KAKO_SAGYO_KB_2     " & vbNewLine _
    '                                        & ",GDS.OUTKA_KAKO_SAGYO_KB_3           AS OUTKA_KAKO_SAGYO_KB_3     " & vbNewLine _
    '                                        & ",GDS.OUTKA_KAKO_SAGYO_KB_4           AS OUTKA_KAKO_SAGYO_KB_4     " & vbNewLine _
    '                                        & ",GDS.OUTKA_KAKO_SAGYO_KB_5           AS OUTKA_KAKO_SAGYO_KB_5     " & vbNewLine _
    '                                        & ",GDS.PKG_SAGYO                       AS PKG_SAGYO                 " & vbNewLine _
    '                                        & ",GDS.TARE_YN                         AS TARE_YN                   " & vbNewLine _
    '                                        & ",GDS.SP_NHS_YN                       AS SP_NHS_YN                 " & vbNewLine _
    '                                        & ",GDS.COA_YN                          AS COA_YN                    " & vbNewLine _
    '                                        & ",GDS.LOT_CTL_KB                      AS LOT_CTL_KB                " & vbNewLine _
    '                                        & ",GDS.LT_DATE_CTL_KB                  AS LT_DATE_CTL_KB            " & vbNewLine _
    '                                        & ",GDS.CRT_DATE_CTL_KB                 AS CRT_DATE_CTL_KB           " & vbNewLine _
    '                                        & ",GDS.DEF_SPD_KB                      AS DEF_SPD_KB                " & vbNewLine _
    '                                        & ",GDS.KITAKU_AM_UT_KB                 AS KITAKU_AM_UT_KB           " & vbNewLine _
    '                                        & ",GDS.KITAKU_GOODS_UP                 AS KITAKU_GOODS_UP           " & vbNewLine _
    '                                        & ",GDS.ORDER_KB                        AS ORDER_KB                  " & vbNewLine _
    '                                        & ",GDS.ORDER_NB                        AS ORDER_NB                  " & vbNewLine _
    '                                        & ",GDS.SHIP_CD_L                       AS SHIP_CD_L                 " & vbNewLine _
    '                                        & ",GDS.SKYU_MEI_YN                     AS SKYU_MEI_YN               " & vbNewLine _
    '                                        & ",GDS.OUTKA_ATT                       AS OUTKA_ATT                 " & vbNewLine _
    '                                        & ",GDS.PRINT_NB                        AS PRINT_NB                  " & vbNewLine _
    '                                        & ",GDS.CONSUME_PERIOD_DATE             AS CONSUME_PERIOD_DATE       " & vbNewLine _
    '                                        & ",ZAI.DEST_CD_P                       AS DEST_CD                   " & vbNewLine _
    '                                        & ",MDEST.DEST_NM                       AS DEST_NM                   " & vbNewLine _
    '                                        & ",IDO.IDO_DATE                        AS IDO_DATE                  " & vbNewLine _
    '                                        & ",INL.HOKAN_STR_DATE                  AS HOKAN_STR_DATE            " & vbNewLine _
    '                                        & ",GDS.SIZE_KB                         AS SIZE_KB                   " & vbNewLine
    ''' <summary>
    ''' 検索SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                             " & vbNewLine _
                                            & " ISNULL(GDS.GOODS_CD_CUST,'')         AS GOODS_CD_CUST             " & vbNewLine _
                                            & ",ZAI.LOT_NO                           AS LOT_NO                    " & vbNewLine _
                                            & ",ZAI.IRIME                            AS IRIME                     " & vbNewLine _
                                            & ",ISNULL(GDS.STD_IRIME_UT,'')          AS IRIME_UT                  " & vbNewLine _
                                            & ",ISNULL((SELECT                                                    " & vbNewLine _
                                            & "       KBN1.KBN_NM1                                                " & vbNewLine _
                                            & "  FROM $LM_MST$..Z_KBN KBN1                                        " & vbNewLine _
                                            & "  WHERE KBN1.KBN_GROUP_CD = 'I001'                                 " & vbNewLine _
                                            & "  AND   KBN1.KBN_CD = GDS.STD_IRIME_UT                             " & vbNewLine _
                                            & "   ),'')   AS IRIME_UT_NM                                          " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_NB                     AS ALLOC_CAN_NB              " & vbNewLine _
                                            & ",ISNULL(GDS.NB_UT,'')                 AS NB_UT                     " & vbNewLine _
                                            & ",ISNULL((SELECT                                                    " & vbNewLine _
                                            & "       KBN2.KBN_NM1                                                " & vbNewLine _
                                            & " FROM $LM_MST$..Z_KBN KBN2                                         " & vbNewLine _
                                            & " WHERE KBN2.KBN_GROUP_CD = 'K002'                                  " & vbNewLine _
                                            & " AND   KBN2.KBN_CD = GDS.NB_UT                                     " & vbNewLine _
                                            & "  ),'')    AS NB_UT_NM                                             " & vbNewLine _
                                            & ",GDS.PKG_NB                           AS PKG_NB                    " & vbNewLine _
                                            & ",ISNULL(GDS.PKG_UT,'')                AS PKG_UT                    " & vbNewLine _
                                            & ",ISNULL((SELECT                                                    " & vbNewLine _
                                            & "       KBN3.KBN_NM1                                                " & vbNewLine _
                                            & "  FROM $LM_MST$..Z_KBN KBN3                                        " & vbNewLine _
                                            & "  WHERE KBN3.KBN_GROUP_CD = 'N001'                                 " & vbNewLine _
                                            & "  AND   KBN3.KBN_CD = GDS.PKG_UT                                   " & vbNewLine _
                                            & "   ),'')   AS PKG_UT_NM                                            " & vbNewLine _
                                            & ",ZAI.REMARK                           AS REMARK                    " & vbNewLine _
                                            & ",ZAI.REMARK_OUT                       AS REMARK_OUT                " & vbNewLine _
                                            & ",ISNULL((SELECT                                                    " & vbNewLine _
                                            & "       KBN4.KBN_NM1                                                " & vbNewLine _
                                            & "  FROM $LM_MST$..Z_KBN KBN4                                        " & vbNewLine _
                                            & "  WHERE KBN4.KBN_GROUP_CD = 'Z001'                                 " & vbNewLine _
                                            & "  AND   KBN4.KBN_CD = ZAI.TAX_KB                                   " & vbNewLine _
                                            & "   ),'')   AS TAX_KB_NM                                            " & vbNewLine _
                                            & ",ISNULL(MAX(GDS.HIKIATE_ALERT_YN),'') AS HIKIATE_ALERT_YN          " & vbNewLine _
                                            & ",ISNULL((SELECT                                                    " & vbNewLine _
                                            & "       KBN5.KBN_NM1                                                " & vbNewLine _
                                            & "  FROM $LM_MST$..Z_KBN KBN5                                        " & vbNewLine _
                                            & "  WHERE KBN5.KBN_GROUP_CD = 'U009'                                 " & vbNewLine _
                                            & "  AND   KBN5.KBN_CD = GDS.HIKIATE_ALERT_YN                         " & vbNewLine _
                                            & "   ),'')   AS HIKIATE_ALERT_NM                                     " & vbNewLine _
                                            & ",ZAI.SMPL_FLAG                        AS SMPL                      " & vbNewLine _
                                             & ",ZAI.ZAI_REC_NO                       AS ZAI_REC_NO                " & vbNewLine _
                                            & ",ZAI.TOU_NO                           AS TOU_NO                    " & vbNewLine _
                                            & ",ZAI.SITU_NO                          AS SITU_NO                   " & vbNewLine _
                                            & ",ZAI.ZONE_CD                          AS ZONE_CD                   " & vbNewLine _
                                            & ",ZAI.LOCA                             AS LOCA                      " & vbNewLine _
                                            & ",ZAI.CUST_CD_L                        AS CUST_CD_L                 " & vbNewLine _
                                            & ",ZAI.CUST_CD_M                        AS CUST_CD_M                 " & vbNewLine _
                                            & ",ZAI.GOODS_CD_NRS                     AS GOODS_CD_NRS              " & vbNewLine _
                                            & ",ZAI.INKA_NO_L                        AS INKA_NO_L                 " & vbNewLine _
                                            & ",ZAI.INKA_NO_M                        AS INKA_NO_M                 " & vbNewLine _
                                            & ",ZAI.INKA_NO_S                        AS INKA_NO_S                 " & vbNewLine _
                                            & ",ZAI.ALLOC_PRIORITY                   AS ALLOC_PRIORITY            " & vbNewLine _
                                            & ",ZAI.RSV_NO                           AS RSV_NO                    " & vbNewLine _
                                            & ",ZAI.SERIAL_NO                        AS SERIAL_NO                 " & vbNewLine _
                                            & ",ZAI.HOKAN_YN                         AS HOKAN_YN                  " & vbNewLine _
                                            & ",ZAI.TAX_KB                           AS TAX_KB                    " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_1                  AS GOODS_COND_KB_1           " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_2                  AS GOODS_COND_KB_2           " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_3                  AS GOODS_COND_KB_3           " & vbNewLine _
                                            & ",ZAI.OFB_KB                           AS OFB_KB                    " & vbNewLine _
                                            & ",ZAI.SPD_KB                           AS SPD_KB                    " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_NB                      AS PORA_ZAI_NB               " & vbNewLine _
                                            & ",ZAI.ALCTD_NB                         AS ALCTD_NB                  " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_QT                      AS PORA_ZAI_QT               " & vbNewLine _
                                            & ",ZAI.ALCTD_QT                         AS ALCTD_QT                  " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_QT                     AS ALLOC_CAN_QT              " & vbNewLine _
                                            & ",ZAI.INKO_DATE                        AS INKO_DATE                 " & vbNewLine _
                                            & ",ZAI.INKO_PLAN_DATE                   AS INKO_PLAN_DATE            " & vbNewLine _
                                            & ",ZAI.ZERO_FLAG                        AS ZERO_FLAG                 " & vbNewLine _
                                            & ",ZAI.LT_DATE                          AS LT_DATE                   " & vbNewLine _
                                            & ",ZAI.GOODS_CRT_DATE                   AS GOODS_CRT_DATE            " & vbNewLine _
                                            & ",ZAI.DEST_CD_P                        AS DEST_CD                   " & vbNewLine _
                                            & ",ZAI.SMPL_FLAG                        AS SMPL_FLAG                 " & vbNewLine _
                                            & ",ZAI.NRS_BR_CD                        AS NRS_BR_CD                 " & vbNewLine _
                                            & ",ZAI.WH_CD                            AS WH_CD                     " & vbNewLine _
                                            & ",GDS.CUST_CD_S                        AS CUST_CD_S                 " & vbNewLine _
                                            & ",GDS.CUST_CD_SS                       AS CUST_CD_SS                " & vbNewLine _
                                            & ",GDS.SEARCH_KEY_1                    AS SEARCH_KEY_1              " & vbNewLine _
                                            & ",GDS.SEARCH_KEY_2                    AS SEARCH_KEY_2              " & vbNewLine _
                                            & ",GDS.CUST_COST_CD1                   AS CUST_COST_CD1             " & vbNewLine _
                                            & ",GDS.CUST_COST_CD2                   AS CUST_COST_CD2             " & vbNewLine _
                                            & ",GDS.JAN_CD                          AS JAN_CD                    " & vbNewLine _
                                            & ",GDS.GOODS_NM_1                      AS GOODS_NM_1                " & vbNewLine _
                                            & ",GDS.GOODS_NM_2                      AS GOODS_NM_2                " & vbNewLine _
                                            & ",GDS.GOODS_NM_3                      AS GOODS_NM_3                " & vbNewLine _
                                            & ",GDS.UP_GP_CD_1                      AS UP_GP_CD_1                " & vbNewLine _
                                            & ",GDS.SHOBO_CD                        AS SHOBO_CD                  " & vbNewLine _
                                            & ",GDS.KIKEN_KB                        AS KIKEN_KB                  " & vbNewLine _
                                            & ",GDS.UN                              AS UN                        " & vbNewLine _
                                            & ",GDS.PG_KB                           AS PG_KB                     " & vbNewLine _
                                            & ",GDS.CLASS_1                         AS CLASS_1                   " & vbNewLine _
                                            & ",GDS.CLASS_2                         AS CLASS_2                   " & vbNewLine _
                                            & ",GDS.CLASS_3                         AS CLASS_3                   " & vbNewLine _
                                            & ",GDS.CHEM_MTRL_KB                    AS CHEM_MTRL_KB              " & vbNewLine _
                                            & ",GDS.DOKU_KB                         AS DOKU_KB                   " & vbNewLine _
                                            & ",GDS.GAS_KANRI_KB                    AS GAS_KANRI_KB              " & vbNewLine _
                                            & ",GDS.ONDO_KB                         AS ONDO_KB                   " & vbNewLine _
                                            & ",GDS.UNSO_ONDO_KB                    AS UNSO_ONDO_KB              " & vbNewLine _
                                            & ",GDS.ONDO_MX                         AS ONDO_MX                   " & vbNewLine _
                                            & ",GDS.ONDO_MM                         AS ONDO_MM                   " & vbNewLine _
                                            & ",GDS.ONDO_STR_DATE                   AS ONDO_STR_DATE             " & vbNewLine _
                                            & ",GDS.ONDO_END_DATE                   AS ONDO_END_DATE             " & vbNewLine _
                                            & ",GDS.ONDO_UNSO_STR_DATE              AS ONDO_UNSO_STR_DATE        " & vbNewLine _
                                            & ",GDS.ONDO_UNSO_END_DATE              AS ONDO_UNSO_END_DATE        " & vbNewLine _
                                            & ",GDS.KYOKAI_GOODS_KB                 AS KYOKAI_GOODS_KB           " & vbNewLine _
                                            & ",GDS.ALCTD_KB                        AS ALCTD_KB                  " & vbNewLine _
                                            & ",GDS.PLT_PER_PKG_UT                  AS PLT_PER_PKG_UT            " & vbNewLine _
                                            & ",GDS.STD_IRIME_NB                    AS STD_IRIME_NB              " & vbNewLine _
                                            & ",GDS.STD_IRIME_UT                    AS STD_IRIME_UT              " & vbNewLine _
                                            & ",GDS.STD_WT_KGS                      AS STD_WT_KGS                " & vbNewLine _
                                            & ",GDS.STD_CBM                         AS STD_CBM                   " & vbNewLine _
                                            & ",GDS.INKA_KAKO_SAGYO_KB_1            AS INKA_KAKO_SAGYO_KB_1      " & vbNewLine _
                                            & ",GDS.INKA_KAKO_SAGYO_KB_2            AS INKA_KAKO_SAGYO_KB_2      " & vbNewLine _
                                            & ",GDS.INKA_KAKO_SAGYO_KB_3            AS INKA_KAKO_SAGYO_KB_3      " & vbNewLine _
                                            & ",GDS.INKA_KAKO_SAGYO_KB_4            AS INKA_KAKO_SAGYO_KB_4      " & vbNewLine _
                                            & ",GDS.INKA_KAKO_SAGYO_KB_5            AS INKA_KAKO_SAGYO_KB_5      " & vbNewLine _
                                            & ",GDS.OUTKA_KAKO_SAGYO_KB_1           AS OUTKA_KAKO_SAGYO_KB_1     " & vbNewLine _
                                            & ",GDS.OUTKA_KAKO_SAGYO_KB_2           AS OUTKA_KAKO_SAGYO_KB_2     " & vbNewLine _
                                            & ",GDS.OUTKA_KAKO_SAGYO_KB_3           AS OUTKA_KAKO_SAGYO_KB_3     " & vbNewLine _
                                            & ",GDS.OUTKA_KAKO_SAGYO_KB_4           AS OUTKA_KAKO_SAGYO_KB_4     " & vbNewLine _
                                            & ",GDS.OUTKA_KAKO_SAGYO_KB_5           AS OUTKA_KAKO_SAGYO_KB_5     " & vbNewLine _
                                            & ",GDS.PKG_SAGYO                       AS PKG_SAGYO                 " & vbNewLine _
                                            & ",GDS.TARE_YN                         AS TARE_YN                   " & vbNewLine _
                                            & ",GDS.SP_NHS_YN                       AS SP_NHS_YN                 " & vbNewLine _
                                            & ",GDS.COA_YN                          AS COA_YN                    " & vbNewLine _
                                            & ",GDS.LOT_CTL_KB                      AS LOT_CTL_KB                " & vbNewLine _
                                            & ",GDS.LT_DATE_CTL_KB                  AS LT_DATE_CTL_KB            " & vbNewLine _
                                            & ",GDS.CRT_DATE_CTL_KB                 AS CRT_DATE_CTL_KB           " & vbNewLine _
                                            & ",GDS.DEF_SPD_KB                      AS DEF_SPD_KB                " & vbNewLine _
                                            & ",GDS.KITAKU_AM_UT_KB                 AS KITAKU_AM_UT_KB           " & vbNewLine _
                                            & ",GDS.KITAKU_GOODS_UP                 AS KITAKU_GOODS_UP           " & vbNewLine _
                                            & ",GDS.ORDER_KB                        AS ORDER_KB                  " & vbNewLine _
                                            & ",GDS.ORDER_NB                        AS ORDER_NB                  " & vbNewLine _
                                            & ",GDS.SHIP_CD_L                       AS SHIP_CD_L                 " & vbNewLine _
                                            & ",GDS.SKYU_MEI_YN                     AS SKYU_MEI_YN               " & vbNewLine _
                                            & ",GDS.OUTKA_ATT                       AS OUTKA_ATT                 " & vbNewLine _
                                            & ",GDS.PRINT_NB                        AS PRINT_NB                  " & vbNewLine _
                                            & ",GDS.CONSUME_PERIOD_DATE             AS CONSUME_PERIOD_DATE       " & vbNewLine _
                                            & ",ZAI.DEST_CD_P                       AS DEST_CD                   " & vbNewLine _
                                            & ",MDEST.DEST_NM                       AS DEST_NM                   " & vbNewLine _
                                            & ",IDO.IDO_DATE                        AS IDO_DATE                  " & vbNewLine _
                                            & ",INL.HOKAN_STR_DATE                  AS HOKAN_STR_DATE            " & vbNewLine _
                                            & ",INL.OUTKA_FROM_ORD_NO_L             AS OUTKA_FROM_ORD_NO_L       " & vbNewLine _
                                            & ",GDS.SIZE_KB                         AS SIZE_KB                   " & vbNewLine _
                                            & ",GDS.CUST_CD_L                       AS CUST_CD_L_GOODS           " & vbNewLine _
                                            & ",GDS.CUST_CD_M                       AS CUST_CD_M_GOODS           " & vbNewLine _
                                            & ",@ADD_FLG                            AS ADD_FLG                   " & vbNewLine _
                                            & ",KBN1.KBN_NM1 + ' ' + SBO.HINMEI     AS SHOBO_NM                  " & vbNewLine
    'END YANAI 要望番号499


    ''' <summary>
    ''' 検索SELECT句（商品名取得用（届先商品名フラグ = '1'の場合））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GOODSNM_NM1 As String = ",ISNULL(DEST.GOODS_NM,'') + ISNULL(GDS.GOODS_NM_1,'')   AS NM_1    " & vbNewLine


    ''' <summary>
    ''' 検索SELECT句（商品名取得用（届先商品名フラグ != '1'の場合））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NM1 As String = ",ISNULL(GDS.GOODS_NM_1,'')            AS NM_1                       " & vbNewLine

#If True Then  'UPD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
    ''' <summary>
    ''' 検索SELECT句（入庫日フラグなしの場合））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NOTINKO_DATE_FLG As String = ",''                        AS INKO_DATE_FLG    " & vbNewLine

    ''' <summary>
    ''' 検索SELECT句（入庫日フラグ 1の場合））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKO_DATE_FLG As String = ",'1'                         AS INKO_DATE_FLG    " & vbNewLine
#End If

    '''' <summary>
    '''' 検索FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM As String = "FROM                                                               " & vbNewLine _
    '                                        & "$LM_TRN$..D_ZAI_TRS ZAI                                            " & vbNewLine _
    '                                        & "LEFT JOIN                                                          " & vbNewLine _
    '                                        & "$LM_MST$..M_GOODS GDS                                              " & vbNewLine _
    '                                        & "ON                                                                 " & vbNewLine _
    '                                        & "    GDS.NRS_BR_CD = ZAI.NRS_BR_CD                                  " & vbNewLine _
    '                                        & "AND GDS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                            " & vbNewLine _
    '                                        & "LEFT JOIN                                                          " & vbNewLine _
    '                                        & "$LM_TRN$..B_INKA_L INL                                             " & vbNewLine _
    '                                        & "ON                                                                 " & vbNewLine _
    '                                        & "    INL.NRS_BR_CD = ZAI.NRS_BR_CD                                  " & vbNewLine _
    '                                        & "AND INL.INKA_NO_L = ZAI.INKA_NO_L                                  " & vbNewLine _
    '                                        & "AND INL.SYS_DEL_FLG = '0'                                          " & vbNewLine _
    '                                        & "LEFT JOIN                                                          " & vbNewLine _
    '                                        & "$LM_MST$..M_DEST MDEST                                             " & vbNewLine _
    '                                        & "ON                                                                 " & vbNewLine _
    '                                        & "    MDEST.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
    '                                        & "AND MDEST.CUST_CD_L = @CUST_CD_L                                   " & vbNewLine _
    '                                        & "AND MDEST.DEST_CD   = ZAI.DEST_CD_P                                " & vbNewLine _
    '                                        & "LEFT JOIN                                                          " & vbNewLine _
    '                                        & "$LM_TRN$..D_IDO_TRS IDO                                            " & vbNewLine _
    '                                        & "ON                                                                 " & vbNewLine _
    '                                        & "    IDO.NRS_BR_CD = ZAI.NRS_BR_CD                                  " & vbNewLine _
    '                                        & "AND IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                              " & vbNewLine _
    '                                        & "AND IDO.SYS_DEL_FLG = '0'                                          " & vbNewLine
    ''' <summary>
    ''' 検索FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                                               " & vbNewLine _
                                            & "$LM_TRN$..D_ZAI_TRS ZAI                                            " & vbNewLine _
                                            & "LEFT JOIN                                                          " & vbNewLine _
                                            & "$LM_TRN$..D_IDO_TRS IDO                                            " & vbNewLine _
                                            & "ON                                                                 " & vbNewLine _
                                            & "    IDO.NRS_BR_CD = ZAI.NRS_BR_CD                                  " & vbNewLine _
                                            & "AND IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                              " & vbNewLine _
                                            & "AND IDO.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                            & "LEFT JOIN                                                          " & vbNewLine _
                                            & "$LM_MST$..M_GOODS GDS                                              " & vbNewLine _
                                            & "ON                                                                 " & vbNewLine _
                                            & "    GDS.NRS_BR_CD = ZAI.NRS_BR_CD                                  " & vbNewLine _
                                            & "AND GDS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                            " & vbNewLine _
                                            & "LEFT JOIN                                                          " & vbNewLine _
                                            & "$LM_TRN$..B_INKA_L INL                                             " & vbNewLine _
                                            & "ON                                                                 " & vbNewLine _
                                            & "    INL.NRS_BR_CD = ZAI.NRS_BR_CD                                  " & vbNewLine _
                                            & "AND INL.INKA_NO_L = ZAI.INKA_NO_L                                  " & vbNewLine _
                                            & "AND INL.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                            & "LEFT JOIN                                                          " & vbNewLine _
                                            & "$LM_MST$..M_DEST MDEST                                             " & vbNewLine _
                                            & "ON                                                                 " & vbNewLine _
                                            & "    MDEST.NRS_BR_CD = ZAI.NRS_BR_CD                                " & vbNewLine _
                                            & "AND MDEST.CUST_CD_L = ZAI.CUST_CD_L                                " & vbNewLine _
                                            & "AND MDEST.DEST_CD   = ZAI.DEST_CD_P                                " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SHOBO SBO                                    " & vbNewLine _
                                            & "ON  SBO.SHOBO_CD    = GDS.SHOBO_CD                                 " & vbNewLine _
                                            & "AND SBO.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                     " & vbNewLine _
                                            & "ON  KBN1.KBN_GROUP_CD = 'S004'                                     " & vbNewLine _
                                            & "AND KBN1.KBN_CD       = SBO.RUI                                    " & vbNewLine _
                                            & "AND KBN1.SYS_DEL_FLG  = '0'                                        " & vbNewLine


    ''' <summary>
    ''' 検索FROM句（届先商品名フラグ = '1'の場合,届先商品名マスタ(M_DESTGOODS)JOIN用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MGOODSDEST As String = "LEFT JOIN                                               " & vbNewLine _
                                                       & "$LM_MST$..M_DESTGOODS DEST                              " & vbNewLine _
                                                       & "ON                                                      " & vbNewLine _
                                                       & "    DEST.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
                                                       & "AND DEST.CUST_CD_L = ZAI.CUST_CD_L                      " & vbNewLine _
                                                       & "AND DEST.CUST_CD_M = ZAI.CUST_CD_M                      " & vbNewLine _
                                                       & "AND DEST.CD = ZAI.DEST_CD_P                             " & vbNewLine _
                                                       & "AND DEST.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                " & vbNewLine

    ''' <summary>
    ''' 検索WHERE句（遷移元画面設定条件）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE As String = "WHERE                                                             " & vbNewLine _
                                             & "    ZAI.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                             & "AND ZAI.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                             & "AND ZAI.WH_CD = @WH_CD                                            " & vbNewLine _
                                             & "AND ZAI.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                             & "AND ZAI.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
                                             & "AND INL.INKA_STATE_KB >= @INKA_STATE_KB                           " & vbNewLine

    ''' <summary>
    ''' 検索WHERE句（遷移元画面設定条件）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE2 As String = "AND ZAI.ALLOC_CAN_NB > 0                                          " & vbNewLine

    ''' <summary>
    ''' 検索ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDERBY As String = "ORDER BY                                                        " & vbNewLine _
                                               & " NM_1                                                           " & vbNewLine _
                                               & ",LOT_NO                                                         " & vbNewLine _
                                               & ",IRIME                                                          " & vbNewLine _
                                               & ",TAX_KB_NM                                                      " & vbNewLine
#If True Then   'ADD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない

    Private Const SQL_SELECT_ORDERBY_INKO_DATE As String = "ORDER BY                                                        " & vbNewLine _
                                               & " INKO_DATE                                                      " & vbNewLine _
                                               & ",NM_1                                                           " & vbNewLine _
                                               & ",LOT_NO                                                         " & vbNewLine _
                                               & ",IRIME                                                          " & vbNewLine _
                                               & ",TAX_KB_NM                                                      " & vbNewLine
#End If

    'START YANAI 要望番号499
    '''' <summary>
    '''' 検索GROUPBY BY句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_GROUPBY As String = "GROUP BY           " & vbNewLine _
    '                                           & "   GDS.GOODS_CD_CUST    " & vbNewLine _
    '                                           & "  ,GDS.GOODS_NM_1       " & vbNewLine _
    '                                           & "  ,ZAI.LOT_NO           " & vbNewLine _
    '                                           & "  ,ZAI.IRIME            " & vbNewLine _
    '                                           & "  ,GDS.STD_IRIME_UT     " & vbNewLine _
    '                                           & "  ,GDS.NB_UT            " & vbNewLine _
    '                                           & "  ,GDS.PKG_NB           " & vbNewLine _
    '                                           & "  ,GDS.PKG_UT           " & vbNewLine _
    '                                           & "  ,ZAI.REMARK           " & vbNewLine _
    '                                           & "  ,ZAI.REMARK_OUT       " & vbNewLine _
    '                                           & "  ,ZAI.ALLOC_CAN_NB     " & vbNewLine _
    '                                           & "  ,GDS.HIKIATE_ALERT_YN " & vbNewLine _
    '                                           & "  ,ZAI.SMPL_FLAG        " & vbNewLine _
    '                                           & "  ,ZAI.ZAI_REC_NO       " & vbNewLine _
    '                                           & "  ,ZAI.TOU_NO           " & vbNewLine _
    '                                           & "  ,ZAI.SITU_NO          " & vbNewLine _
    '                                           & "  ,ZAI.ZONE_CD          " & vbNewLine _
    '                                           & "  ,ZAI.LOCA             " & vbNewLine _
    '                                           & "  ,ZAI.CUST_CD_L        " & vbNewLine _
    '                                           & "  ,ZAI.CUST_CD_M        " & vbNewLine _
    '                                           & "  ,ZAI.GOODS_CD_NRS     " & vbNewLine _
    '                                           & "  ,ZAI.INKA_NO_L        " & vbNewLine _
    '                                           & "  ,ZAI.INKA_NO_M        " & vbNewLine _
    '                                           & "  ,ZAI.INKA_NO_S        " & vbNewLine _
    '                                           & "  ,ZAI.ALLOC_PRIORITY   " & vbNewLine _
    '                                           & "  ,ZAI.RSV_NO           " & vbNewLine _
    '                                           & "  ,ZAI.SERIAL_NO        " & vbNewLine _
    '                                           & "  ,ZAI.HOKAN_YN         " & vbNewLine _
    '                                           & "  ,ZAI.TAX_KB           " & vbNewLine _
    '                                           & "  ,ZAI.GOODS_COND_KB_1  " & vbNewLine _
    '                                           & "  ,ZAI.GOODS_COND_KB_2  " & vbNewLine _
    '                                           & "  ,ZAI.GOODS_COND_KB_3  " & vbNewLine _
    '                                           & "  ,ZAI.OFB_KB           " & vbNewLine _
    '                                           & "  ,ZAI.SPD_KB           " & vbNewLine _
    '                                           & "  ,ZAI.PORA_ZAI_NB      " & vbNewLine _
    '                                           & "  ,ZAI.ALCTD_NB         " & vbNewLine _
    '                                           & "  ,ZAI.PORA_ZAI_QT      " & vbNewLine _
    '                                           & "  ,ZAI.ALCTD_QT         " & vbNewLine _
    '                                           & "  ,ZAI.ALLOC_CAN_QT     " & vbNewLine _
    '                                           & "  ,ZAI.INKO_DATE        " & vbNewLine _
    '                                           & "  ,ZAI.INKO_PLAN_DATE   " & vbNewLine _
    '                                           & "  ,ZAI.ZERO_FLAG        " & vbNewLine _
    '                                           & "  ,ZAI.LT_DATE          " & vbNewLine _
    '                                           & "  ,ZAI.GOODS_CRT_DATE   " & vbNewLine _
    '                                           & "  ,ZAI.SMPL_FLAG        " & vbNewLine _
    '                                           & "  ,ZAI.NRS_BR_CD        " & vbNewLine _
    '                                           & "  ,ZAI.WH_CD            " & vbNewLine _
    '                                           & "  ,GDS.CUST_CD_S            " & vbNewLine _
    '                                           & "  ,GDS.CUST_CD_SS           " & vbNewLine _
    '                                           & "  ,GDS.SEARCH_KEY_1         " & vbNewLine _
    '                                           & "  ,GDS.SEARCH_KEY_2         " & vbNewLine _
    '                                           & "  ,GDS.CUST_COST_CD1        " & vbNewLine _
    '                                           & "  ,GDS.CUST_COST_CD2        " & vbNewLine _
    '                                           & "  ,GDS.JAN_CD               " & vbNewLine _
    '                                           & "  ,GDS.GOODS_NM_1           " & vbNewLine _
    '                                           & "  ,GDS.GOODS_NM_2           " & vbNewLine _
    '                                           & "  ,GDS.GOODS_NM_3           " & vbNewLine _
    '                                           & "  ,GDS.UP_GP_CD_1           " & vbNewLine _
    '                                           & "  ,GDS.SHOBO_CD             " & vbNewLine _
    '                                           & "  ,GDS.KIKEN_KB             " & vbNewLine _
    '                                           & "  ,GDS.UN                   " & vbNewLine _
    '                                           & "  ,GDS.PG_KB                " & vbNewLine _
    '                                           & "  ,GDS.CLASS_1              " & vbNewLine _
    '                                           & "  ,GDS.CLASS_2              " & vbNewLine _
    '                                           & "  ,GDS.CLASS_3              " & vbNewLine _
    '                                           & "  ,GDS.CHEM_MTRL_KB         " & vbNewLine _
    '                                           & "  ,GDS.DOKU_KB              " & vbNewLine _
    '                                           & "  ,GDS.GAS_KANRI_KB         " & vbNewLine _
    '                                           & "  ,GDS.ONDO_KB              " & vbNewLine _
    '                                           & "  ,GDS.UNSO_ONDO_KB         " & vbNewLine _
    '                                           & "  ,GDS.ONDO_MX              " & vbNewLine _
    '                                           & "  ,GDS.ONDO_MM              " & vbNewLine _
    '                                           & "  ,GDS.ONDO_STR_DATE        " & vbNewLine _
    '                                           & "  ,GDS.ONDO_END_DATE        " & vbNewLine _
    '                                           & "  ,GDS.ONDO_UNSO_STR_DATE   " & vbNewLine _
    '                                           & "  ,GDS.ONDO_UNSO_END_DATE   " & vbNewLine _
    '                                           & "  ,GDS.KYOKAI_GOODS_KB      " & vbNewLine _
    '                                           & "  ,GDS.ALCTD_KB             " & vbNewLine _
    '                                           & "  ,GDS.PLT_PER_PKG_UT       " & vbNewLine _
    '                                           & "  ,GDS.STD_IRIME_NB         " & vbNewLine _
    '                                           & "  ,GDS.STD_IRIME_UT         " & vbNewLine _
    '                                           & "  ,GDS.STD_WT_KGS           " & vbNewLine _
    '                                           & "  ,GDS.STD_CBM              " & vbNewLine _
    '                                           & "  ,GDS.INKA_KAKO_SAGYO_KB_1 " & vbNewLine _
    '                                           & "  ,GDS.INKA_KAKO_SAGYO_KB_2 " & vbNewLine _
    '                                           & "  ,GDS.INKA_KAKO_SAGYO_KB_3 " & vbNewLine _
    '                                           & "  ,GDS.INKA_KAKO_SAGYO_KB_4 " & vbNewLine _
    '                                           & "  ,GDS.INKA_KAKO_SAGYO_KB_5 " & vbNewLine _
    '                                           & "  ,GDS.OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
    '                                           & "  ,GDS.OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
    '                                           & "  ,GDS.OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
    '                                           & "  ,GDS.OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
    '                                           & "  ,GDS.OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
    '                                           & "  ,GDS.PKG_SAGYO            " & vbNewLine _
    '                                           & "  ,GDS.TARE_YN              " & vbNewLine _
    '                                           & "  ,GDS.SP_NHS_YN            " & vbNewLine _
    '                                           & "  ,GDS.COA_YN               " & vbNewLine _
    '                                           & "  ,GDS.LOT_CTL_KB           " & vbNewLine _
    '                                           & "  ,GDS.LT_DATE_CTL_KB       " & vbNewLine _
    '                                           & "  ,GDS.CRT_DATE_CTL_KB      " & vbNewLine _
    '                                           & "  ,GDS.DEF_SPD_KB           " & vbNewLine _
    '                                           & "  ,GDS.KITAKU_AM_UT_KB      " & vbNewLine _
    '                                           & "  ,GDS.KITAKU_GOODS_UP      " & vbNewLine _
    '                                           & "  ,GDS.ORDER_KB             " & vbNewLine _
    '                                           & "  ,GDS.ORDER_NB             " & vbNewLine _
    '                                           & "  ,GDS.SHIP_CD_L            " & vbNewLine _
    '                                           & "  ,GDS.SKYU_MEI_YN          " & vbNewLine _
    '                                           & "  ,GDS.OUTKA_ATT            " & vbNewLine _
    '                                           & "  ,GDS.PRINT_NB             " & vbNewLine _
    '                                           & "  ,GDS.CONSUME_PERIOD_DATE  " & vbNewLine _
    '                                           & "  ,ZAI.DEST_CD_P            " & vbNewLine _
    '                                           & "  ,MDEST.DEST_NM            " & vbNewLine _
    '                                           & "  ,IDO.IDO_DATE             " & vbNewLine _
    '                                           & "  ,INL.HOKAN_STR_DATE       " & vbNewLine _
    '                                           & "  ,GDS.SIZE_KB              " & vbNewLine
    ''' <summary>
    ''' 検索GROUPBY BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUPBY As String = "GROUP BY           " & vbNewLine _
                                               & "   GDS.GOODS_CD_CUST    " & vbNewLine _
                                               & "  ,GDS.GOODS_NM_1       " & vbNewLine _
                                               & "  ,ZAI.LOT_NO           " & vbNewLine _
                                               & "  ,ZAI.IRIME            " & vbNewLine _
                                               & "  ,GDS.STD_IRIME_UT     " & vbNewLine _
                                               & "  ,GDS.NB_UT            " & vbNewLine _
                                               & "  ,GDS.PKG_NB           " & vbNewLine _
                                               & "  ,GDS.PKG_UT           " & vbNewLine _
                                               & "  ,ZAI.REMARK           " & vbNewLine _
                                               & "  ,ZAI.REMARK_OUT       " & vbNewLine _
                                               & "  ,ZAI.ALLOC_CAN_NB     " & vbNewLine _
                                               & "  ,GDS.HIKIATE_ALERT_YN " & vbNewLine _
                                               & "  ,ZAI.SMPL_FLAG        " & vbNewLine _
                                               & "  ,ZAI.ZAI_REC_NO       " & vbNewLine _
                                               & "  ,ZAI.TOU_NO           " & vbNewLine _
                                               & "  ,ZAI.SITU_NO          " & vbNewLine _
                                               & "  ,ZAI.ZONE_CD          " & vbNewLine _
                                               & "  ,ZAI.LOCA             " & vbNewLine _
                                               & "  ,ZAI.CUST_CD_L        " & vbNewLine _
                                               & "  ,ZAI.CUST_CD_M        " & vbNewLine _
                                               & "  ,ZAI.GOODS_CD_NRS     " & vbNewLine _
                                               & "  ,ZAI.INKA_NO_L        " & vbNewLine _
                                               & "  ,ZAI.INKA_NO_M        " & vbNewLine _
                                               & "  ,ZAI.INKA_NO_S        " & vbNewLine _
                                               & "  ,ZAI.ALLOC_PRIORITY   " & vbNewLine _
                                               & "  ,ZAI.RSV_NO           " & vbNewLine _
                                               & "  ,ZAI.SERIAL_NO        " & vbNewLine _
                                               & "  ,ZAI.HOKAN_YN         " & vbNewLine _
                                               & "  ,ZAI.TAX_KB           " & vbNewLine _
                                               & "  ,ZAI.GOODS_COND_KB_1  " & vbNewLine _
                                               & "  ,ZAI.GOODS_COND_KB_2  " & vbNewLine _
                                               & "  ,ZAI.GOODS_COND_KB_3  " & vbNewLine _
                                               & "  ,ZAI.OFB_KB           " & vbNewLine _
                                               & "  ,ZAI.SPD_KB           " & vbNewLine _
                                               & "  ,ZAI.PORA_ZAI_NB      " & vbNewLine _
                                               & "  ,ZAI.ALCTD_NB         " & vbNewLine _
                                               & "  ,ZAI.PORA_ZAI_QT      " & vbNewLine _
                                               & "  ,ZAI.ALCTD_QT         " & vbNewLine _
                                               & "  ,ZAI.ALLOC_CAN_QT     " & vbNewLine _
                                               & "  ,ZAI.INKO_DATE        " & vbNewLine _
                                               & "  ,ZAI.INKO_PLAN_DATE   " & vbNewLine _
                                               & "  ,ZAI.ZERO_FLAG        " & vbNewLine _
                                               & "  ,ZAI.LT_DATE          " & vbNewLine _
                                               & "  ,ZAI.GOODS_CRT_DATE   " & vbNewLine _
                                               & "  ,ZAI.SMPL_FLAG        " & vbNewLine _
                                               & "  ,ZAI.NRS_BR_CD        " & vbNewLine _
                                               & "  ,ZAI.WH_CD            " & vbNewLine _
                                               & "  ,GDS.CUST_CD_S            " & vbNewLine _
                                               & "  ,GDS.CUST_CD_SS           " & vbNewLine _
                                               & "  ,GDS.SEARCH_KEY_1         " & vbNewLine _
                                               & "  ,GDS.SEARCH_KEY_2         " & vbNewLine _
                                               & "  ,GDS.CUST_COST_CD1        " & vbNewLine _
                                               & "  ,GDS.CUST_COST_CD2        " & vbNewLine _
                                               & "  ,GDS.JAN_CD               " & vbNewLine _
                                               & "  ,GDS.GOODS_NM_1           " & vbNewLine _
                                               & "  ,GDS.GOODS_NM_2           " & vbNewLine _
                                               & "  ,GDS.GOODS_NM_3           " & vbNewLine _
                                               & "  ,GDS.UP_GP_CD_1           " & vbNewLine _
                                               & "  ,GDS.SHOBO_CD             " & vbNewLine _
                                               & "  ,GDS.KIKEN_KB             " & vbNewLine _
                                               & "  ,GDS.UN                   " & vbNewLine _
                                               & "  ,GDS.PG_KB                " & vbNewLine _
                                               & "  ,GDS.CLASS_1              " & vbNewLine _
                                               & "  ,GDS.CLASS_2              " & vbNewLine _
                                               & "  ,GDS.CLASS_3              " & vbNewLine _
                                               & "  ,GDS.CHEM_MTRL_KB         " & vbNewLine _
                                               & "  ,GDS.DOKU_KB              " & vbNewLine _
                                               & "  ,GDS.GAS_KANRI_KB         " & vbNewLine _
                                               & "  ,GDS.ONDO_KB              " & vbNewLine _
                                               & "  ,GDS.UNSO_ONDO_KB         " & vbNewLine _
                                               & "  ,GDS.ONDO_MX              " & vbNewLine _
                                               & "  ,GDS.ONDO_MM              " & vbNewLine _
                                               & "  ,GDS.ONDO_STR_DATE        " & vbNewLine _
                                               & "  ,GDS.ONDO_END_DATE        " & vbNewLine _
                                               & "  ,GDS.ONDO_UNSO_STR_DATE   " & vbNewLine _
                                               & "  ,GDS.ONDO_UNSO_END_DATE   " & vbNewLine _
                                               & "  ,GDS.KYOKAI_GOODS_KB      " & vbNewLine _
                                               & "  ,GDS.ALCTD_KB             " & vbNewLine _
                                               & "  ,GDS.PLT_PER_PKG_UT       " & vbNewLine _
                                               & "  ,GDS.STD_IRIME_NB         " & vbNewLine _
                                               & "  ,GDS.STD_IRIME_UT         " & vbNewLine _
                                               & "  ,GDS.STD_WT_KGS           " & vbNewLine _
                                               & "  ,GDS.STD_CBM              " & vbNewLine _
                                               & "  ,GDS.INKA_KAKO_SAGYO_KB_1 " & vbNewLine _
                                               & "  ,GDS.INKA_KAKO_SAGYO_KB_2 " & vbNewLine _
                                               & "  ,GDS.INKA_KAKO_SAGYO_KB_3 " & vbNewLine _
                                               & "  ,GDS.INKA_KAKO_SAGYO_KB_4 " & vbNewLine _
                                               & "  ,GDS.INKA_KAKO_SAGYO_KB_5 " & vbNewLine _
                                               & "  ,GDS.OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
                                               & "  ,GDS.OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
                                               & "  ,GDS.OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
                                               & "  ,GDS.OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
                                               & "  ,GDS.OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
                                               & "  ,GDS.PKG_SAGYO            " & vbNewLine _
                                               & "  ,GDS.TARE_YN              " & vbNewLine _
                                               & "  ,GDS.SP_NHS_YN            " & vbNewLine _
                                               & "  ,GDS.COA_YN               " & vbNewLine _
                                               & "  ,GDS.LOT_CTL_KB           " & vbNewLine _
                                               & "  ,GDS.LT_DATE_CTL_KB       " & vbNewLine _
                                               & "  ,GDS.CRT_DATE_CTL_KB      " & vbNewLine _
                                               & "  ,GDS.DEF_SPD_KB           " & vbNewLine _
                                               & "  ,GDS.KITAKU_AM_UT_KB      " & vbNewLine _
                                               & "  ,GDS.KITAKU_GOODS_UP      " & vbNewLine _
                                               & "  ,GDS.ORDER_KB             " & vbNewLine _
                                               & "  ,GDS.ORDER_NB             " & vbNewLine _
                                               & "  ,GDS.SHIP_CD_L            " & vbNewLine _
                                               & "  ,GDS.SKYU_MEI_YN          " & vbNewLine _
                                               & "  ,GDS.OUTKA_ATT            " & vbNewLine _
                                               & "  ,GDS.PRINT_NB             " & vbNewLine _
                                               & "  ,GDS.CONSUME_PERIOD_DATE  " & vbNewLine _
                                               & "  ,ZAI.DEST_CD_P            " & vbNewLine _
                                               & "  ,MDEST.DEST_NM            " & vbNewLine _
                                               & "  ,IDO.IDO_DATE             " & vbNewLine _
                                               & "  ,INL.HOKAN_STR_DATE       " & vbNewLine _
                                               & "  ,INL.OUTKA_FROM_ORD_NO_L  " & vbNewLine _
                                               & "  ,GDS.SIZE_KB              " & vbNewLine _
                                               & "  ,GDS.CUST_CD_L            " & vbNewLine _
                                               & "  ,GDS.CUST_CD_M            " & vbNewLine _
                                               & "  ,KBN1.KBN_NM1 + ' ' + SBO.HINMEI " & vbNewLine
    'END YANAI 要望番号499

#End Region

#Region "FURI_GOODS"
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FURIGOODS As String = " SELECT                                             " & vbNewLine _
                                            & " GOODS.CUST_CD_L          AS       CUST_CD_L             " & vbNewLine

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_FURIGOODS As String = "FROM                                                  " & vbNewLine _
                                         & "$LM_MST$..M_FURI_GOODS FURIGOODS                            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS GOODS                           " & vbNewLine _
                                         & "ON     FURIGOODS.NRS_BR_CD = GOODS.NRS_BR_CD                " & vbNewLine _
                                         & "AND    FURIGOODS.CD_NRS = GOODS.GOODS_CD_NRS                " & vbNewLine

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_FURIGOODS As String = "WHERE                                                " & vbNewLine _
                                         & "    FURIGOODS.NRS_BR_CD        = @NRS_BR_CD                 " & vbNewLine

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUPBY_FURIGOODS As String = "GROUP BY                                           " & vbNewLine _
                                         & "    GOODS.CUST_CD_L                                         " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMD100IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList()

        '届先商品名フラグ取得
        Dim destGoodsFlg As String = _Row.Item("DEST_GOODS_FLG").ToString()

        '届先商品名フラグの値により作成SQL変更
        If destGoodsFlg = "1" Then

            'SQL作成(商品名 = (M_DESTGOODS)GOODS_NM + (M_GOODS)GOODS_NM_1)
            _StrSql.Append(LMD100DAC.SQL_SELECT_COUNT)              'SQL構築(カウント用SELECT句)
            _StrSql.Append(LMD100DAC.SQL_SELECT_FROM)               'SQL構築(検索FROM句)
            _StrSql.Append(LMD100DAC.SQL_SELECT_FROM_MGOODSDEST)    'SQL構築(検索FROM句届先商品名マスタJOIN用)
            _StrSql.Append(LMD100DAC.SQL_SELECT_WHERE)              'SQL構築(検索WHERE句遷移元画面設定条件)
            'Dim dr As DataRow() = ds.Tables("LMD100_FURI_GOODS").Select(String.Concat("CUST_CD_L = '", _Row.Item("CUST_CD_L").ToString(), "'"))
            'If 0 = dr.Length Then
            _StrSql.Append(LMD100DAC.SQL_SELECT_WHERE2)              'SQL構築(検索WHERE句遷移元画面設定条件)
            'End If

            Call Me.SQLSelectWhere()                                'SQL構築(検索WHERE句Spread検索行条件)
        Else
            'SQL作成
            _StrSql.Append(LMD100DAC.SQL_SELECT_COUNT)              'SQL構築(カウント用SELECT句)
            _StrSql.Append(LMD100DAC.SQL_SELECT_FROM)               'SQL構築(検索FROM句)
            _StrSql.Append(LMD100DAC.SQL_SELECT_WHERE)              'SQL構築(検索WHERE句遷移元画面設定条件)
            'Dim dr As DataRow() = ds.Tables("LMD100_FURI_GOODS").Select(String.Concat("CUST_CD_L = '", _Row.Item("CUST_CD_L").ToString(), "'"))
            'If 0 = dr.Length Then
            _StrSql.Append(LMD100DAC.SQL_SELECT_WHERE2)              'SQL構築(検索WHERE句遷移元画面設定条件)
            'End If
            Call Me.SQLSelectWhere()                                'SQL構築(検索WHERE句Spread検索行条件)
        End If

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'SQLパラメータ設定
        Call Me.SetParamHed()

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD100DAC", "SelectData", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMD100IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList()

        '届先商品名フラグ取得
        Dim destGoodsFlg As String = _Row.Item("DEST_GOODS_FLG").ToString()
#If True Then   'ADD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
        '入庫日フラグ取得
        Dim inko_dateFlg As String = _Row.Item("INKO_DATE_FLG").ToString()
#End If
        '届先商品名フラグの値により作成SQL変更
        If destGoodsFlg = "1" Then

            'SQL作成(商品名 = (M_DESTGOODS)GOODS_NM + (M_GOODS)GOODS_NM_1)
            _StrSql.Append(LMD100DAC.SQL_SELECT_DATA)               'SQL構築(検索SELECT句)
            _StrSql.Append(LMD100DAC.SQL_SELECT_GOODSNM_NM1)        'SQL構築(検索SELECT句商品名取得用（届先商品名フラグ = '1'の場合))
#If True Then  'ADD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
            If inko_dateFlg = "1" Then
                _StrSql.Append(LMD100DAC.SQL_SELECT_INKO_DATE_FLG)
            Else
                _StrSql.Append(LMD100DAC.SQL_SELECT_NOTINKO_DATE_FLG)
            End If
#End If
            _StrSql.Append(LMD100DAC.SQL_SELECT_FROM)               'SQL構築(検索FROM句)
            _StrSql.Append(LMD100DAC.SQL_SELECT_FROM_MGOODSDEST)    'SQL構築(検索FROM句届先商品名マスタJOIN用)
            _StrSql.Append(LMD100DAC.SQL_SELECT_WHERE)              'SQL構築(検索WHERE句遷移元画面設定条件)
            _StrSql.Append(LMD100DAC.SQL_SELECT_WHERE2)              'SQL構築(検索WHERE句遷移元画面設定条件)
            Call Me.SQLSelectWhere()                                'SQL構築(検索WHERE句Spread検索行条件)
            _StrSql.Append(LMD100DAC.SQL_SELECT_GROUPBY)            'SQL構築(検索GROUP句)
#If False Then  'UPD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
            _StrSql.Append(LMD100DAC.SQL_SELECT_ORDERBY)            'SQL構築(検索ORDER BY句)
#Else
            If inko_dateFlg = "1" Then
                _StrSql.Append(LMD100DAC.SQL_SELECT_ORDERBY_INKO_DATE)  'SQL構築(検索ORDER BY句)
            Else
                _StrSql.Append(LMD100DAC.SQL_SELECT_ORDERBY)            'SQL構築(検索ORDER BY句)
            End If
#End If
        Else
            'SQL作成
            _StrSql.Append(LMD100DAC.SQL_SELECT_DATA)               'SQL構築(検索SELECT句)
            _StrSql.Append(LMD100DAC.SQL_SELECT_NM1)                'SQL構築(検索SELECT句商品名取得用（届先商品名フラグ != '1'の場合))
#If True Then  'ADD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
            If inko_dateFlg = "1" Then
                _StrSql.Append(LMD100DAC.SQL_SELECT_INKO_DATE_FLG)
            Else
                _StrSql.Append(LMD100DAC.SQL_SELECT_NOTINKO_DATE_FLG)
            End If
#End If
            _StrSql.Append(LMD100DAC.SQL_SELECT_FROM)               'SQL構築(検索FROM句)
            _StrSql.Append(LMD100DAC.SQL_SELECT_WHERE)              'SQL構築(検索WHERE句遷移元画面設定条件)
            _StrSql.Append(LMD100DAC.SQL_SELECT_WHERE2)              'SQL構築(検索WHERE句遷移元画面設定条件)
            Call Me.SQLSelectWhere()                                'SQL構築(検索WHERE句Spread検索行条件)
            _StrSql.Append(LMD100DAC.SQL_SELECT_GROUPBY)            'SQL構築(検索GROUP句)
#If False Then  'UPD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
            _StrSql.Append(LMD100DAC.SQL_SELECT_ORDERBY)            'SQL構築(検索ORDER BY句)
#Else
            If inko_dateFlg = "1" Then
                _StrSql.Append(LMD100DAC.SQL_SELECT_ORDERBY_INKO_DATE)  'SQL構築(検索ORDER BY句)
            Else
                _StrSql.Append(LMD100DAC.SQL_SELECT_ORDERBY)            'SQL構築(検索ORDER BY句)
            End If
#End If
        End If

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'SQLパラメータ設定
        Call Me.SetParamHed()

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD100DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("NM_1", "NM_1")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("IRIME_UT_NM", "IRIME_UT_NM")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("PKG_UT_NM", "PKG_UT_NM")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("HIKIATE_ALERT_NM", "HIKIATE_ALERT_NM")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("SMPL", "SMPL")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
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
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ALLOC_CAN_QT", "ALLOC_CAN_QT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("INKO_PLAN_DATE", "INKO_PLAN_DATE")
        map.Add("ZERO_FLAG", "ZERO_FLAG")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("SMPL_FLAG", "SMPL_FLAG")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("GOODS_NM_3", "GOODS_NM_3")
        map.Add("UP_GP_CD_1", "UP_GP_CD_1")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("KIKEN_KB", "KIKEN_KB")
        map.Add("UN", "UN")
        map.Add("PG_KB", "PG_KB")
        map.Add("CLASS_1", "CLASS_1")
        map.Add("CLASS_2", "CLASS_2")
        map.Add("CLASS_3", "CLASS_3")
        map.Add("CHEM_MTRL_KB", "CHEM_MTRL_KB")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("GAS_KANRI_KB", "GAS_KANRI_KB")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("ONDO_UNSO_STR_DATE", "ONDO_UNSO_STR_DATE")
        map.Add("ONDO_UNSO_END_DATE", "ONDO_UNSO_END_DATE")
        map.Add("KYOKAI_GOODS_KB", "KYOKAI_GOODS_KB")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("PLT_PER_PKG_UT", "PLT_PER_PKG_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("STD_CBM", "STD_CBM")
        map.Add("INKA_KAKO_SAGYO_KB_1", "INKA_KAKO_SAGYO_KB_1")
        map.Add("INKA_KAKO_SAGYO_KB_2", "INKA_KAKO_SAGYO_KB_2")
        map.Add("INKA_KAKO_SAGYO_KB_3", "INKA_KAKO_SAGYO_KB_3")
        map.Add("INKA_KAKO_SAGYO_KB_4", "INKA_KAKO_SAGYO_KB_4")
        map.Add("INKA_KAKO_SAGYO_KB_5", "INKA_KAKO_SAGYO_KB_5")
        map.Add("OUTKA_KAKO_SAGYO_KB_1", "OUTKA_KAKO_SAGYO_KB_1")
        map.Add("OUTKA_KAKO_SAGYO_KB_2", "OUTKA_KAKO_SAGYO_KB_2")
        map.Add("OUTKA_KAKO_SAGYO_KB_3", "OUTKA_KAKO_SAGYO_KB_3")
        map.Add("OUTKA_KAKO_SAGYO_KB_4", "OUTKA_KAKO_SAGYO_KB_4")
        map.Add("OUTKA_KAKO_SAGYO_KB_5", "OUTKA_KAKO_SAGYO_KB_5")
        map.Add("PKG_SAGYO", "PKG_SAGYO")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("SP_NHS_YN", "SP_NHS_YN")
        map.Add("COA_YN", "COA_YN")
        map.Add("LOT_CTL_KB", "LOT_CTL_KB")
        map.Add("LT_DATE_CTL_KB", "LT_DATE_CTL_KB")
        map.Add("CRT_DATE_CTL_KB", "CRT_DATE_CTL_KB")
        map.Add("DEF_SPD_KB", "DEF_SPD_KB")
        map.Add("KITAKU_AM_UT_KB", "KITAKU_AM_UT_KB")
        map.Add("KITAKU_GOODS_UP", "KITAKU_GOODS_UP")
        map.Add("ORDER_KB", "ORDER_KB")
        map.Add("ORDER_NB", "ORDER_NB")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SKYU_MEI_YN", "SKYU_MEI_YN")
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("PRINT_NB", "PRINT_NB")
        map.Add("CONSUME_PERIOD_DATE", "CONSUME_PERIOD_DATE")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("SIZE_KB", "SIZE_KB")
        'START YANAI 要望番号499
        map.Add("CUST_CD_L_GOODS", "CUST_CD_L_GOODS")
        map.Add("CUST_CD_M_GOODS", "CUST_CD_M_GOODS")
        map.Add("ADD_FLG", "ADD_FLG")
        'END YANAI 要望番号499
        map.Add("SHOBO_NM", "SHOBO_NM")
        map.Add("INKO_DATE_FLG", "INKO_DATE_FLG")   'ADD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD100OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 振替先商品対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替先商品対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectFuriGoodsData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD100_FURI_GOODS_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD100DAC.SQL_SELECT_FURIGOODS)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMD100DAC.SQL_FROM_FURIGOODS)       'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMD100DAC.SQL_WHERE_FURIGOODS)      'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMD100DAC.SQL_GROUPBY_FURIGOODS)    'SQL構築(データ抽出用GroupBy句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの設定
        Call Me.SetSelectFuriGoodsParam(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD100DAC", "SelectFuriGoodsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD100_FURI_GOODS")

    End Function

    ''' <summary>
    ''' 検索WHERE句作成(Spread検索行条件)
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhere()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With _Row

            '荷主商品コード
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GDS.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                _StrSql.Append(vbNewLine)
                'START YANAI 要望番号886
                '_SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号886
            End If

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GDS.GOODS_NM_1 LIKE @GOODS_NM")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'ロットNo.
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.LOT_NO LIKE @LOT_NO")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '入目
            whereStr = .Item("IRIME").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.IRIME = @IRIME")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", whereStr, DBDataType.NUMERIC))
            End If

            '入目単位
            whereStr = .Item("IRIME_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GDS.STD_IRIME_UT = @IRIME_UT")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", whereStr, DBDataType.CHAR))
            End If

            '個数単位
            whereStr = .Item("NB_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GDS.NB_UT = @NB_UT")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", whereStr, DBDataType.CHAR))
            End If

            '備考小（社内）
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.REMARK LIKE @REMARK")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '備考小（社外）
            whereStr = .Item("REMARK_OUT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.REMARK_OUT LIKE @REMARK_OUT")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.TAX_KB = @TAX_KB")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", whereStr, DBDataType.CHAR))
            End If

            '引当注意品
            whereStr = .Item("HIKIATE_ALERT_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GDS.HIKIATE_ALERT_YN = @HIKIATE_ALERT_YN")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@HIKIATE_ALERT_YN", whereStr, DBDataType.CHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND (MDEST.DEST_NM LIKE @DEST_NM")
                _StrSql.Append(" OR ZAI.DEST_CD_P = '')                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            Else
                _StrSql.Append(" AND ZAI.DEST_CD_P = ''                                      ")
                _StrSql.Append(vbNewLine)
            End If

            'オーダー番号
            whereStr = .Item("OUTKA_FROM_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND INL.OUTKA_FROM_ORD_NO_L LIKE @OUTKA_FROM_ORD_NO_L")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(ヘッダー部)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHed()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            '2014.09.11 追加START
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ADD_FLG", .Item("ADD_FLG").ToString(), DBDataType.CHAR))
            '2014.09.11 追加END

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(振替商品マスタ存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectFuriGoodsParam(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

End Class
