' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC830    : 日立物流出荷音声データCSVマスタ
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC830DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC830DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks>
    ''' SQL修正20120918 速度改善①作業が不要のため削除②棟室集計のSQLを修正③ZAI_TRSの条件をGroup前へ一部移動
    ''' </remarks>
    Private Const SQL_SELECT_DIC_CSV As String = "SELECT                                                                                                             " & vbNewLine _
                                               & "       @COMPNAME                     AS COMPNAME  --ログインユーザーの端末名                                       " & vbNewLine _
                                               & "     , MAIN.RPT_ID	               AS RPT_ID    --帳票ＩＤ                                                       " & vbNewLine _
                                               & "     , CASE WHEN MAIN.LOC_MANAGER_YN <>'00'                                                                        " & vbNewLine _
                                               & "             AND MAIN.TOU_HAN_HD_CNT <>'1'                                                                         " & vbNewLine _
                                               & "             AND MAIN.TOUHAN_SASHIZU_YN ='01'                                                                      " & vbNewLine _
                                               & "             AND @TOU_HAN_FLG = '01' THEN MAIN.TOU_HD_2                                                            " & vbNewLine _
                                               & "            WHEN MAIN.LOC_MANAGER_YN <>'00'                                                                        " & vbNewLine _
                                               & "             AND MAIN.TOU_HAN_HD_CNT <>'1'                                                                         " & vbNewLine _
                                               & "             AND MAIN.TOUHAN_SASHIZU_YN ='01'                                                                      " & vbNewLine _
                                               & "             AND @TOU_HAN_FLG <> '01' THEN '00'                                                                    " & vbNewLine _
                                               & "            WHEN MAIN.LOC_MANAGER_YN <>'00'                                                                        " & vbNewLine _
                                               & "             AND MAIN.TOU_HAN_HD_CNT <>'1'                                                                         " & vbNewLine _
                                               & "             AND MAIN.TOUHAN_SASHIZU_YN <>'01'                                                                     " & vbNewLine _
                                               & "             AND (MAIN.NRS_BR_CD ='50' AND MAIN.CUST_NAIYO ='01')THEN MAIN.TOU_HD_2                                " & vbNewLine _
                                               & "            WHEN MAIN.LOC_MANAGER_YN <>'00'                                                                        " & vbNewLine _
                                               & "             AND MAIN.TOU_HAN_HD_CNT <>'1'                                                                         " & vbNewLine _
                                               & "             AND MAIN.TOUHAN_SASHIZU_YN <>'01'                                                                     " & vbNewLine _
                                               & "             AND NOT(MAIN.NRS_BR_CD ='50' AND MAIN.CUST_NAIYO ='01')THEN '00'                                      " & vbNewLine _
                                               & "            WHEN MAIN.TOU_HAN_HD_CNT ='1' THEN MAIN.TOU_HD_2                                                       " & vbNewLine _
                                               & "            ELSE '00'                                                                                              " & vbNewLine _
                                               & "       END                           AS TOU_HD                                                                     " & vbNewLine _
                                               & "     , CASE WHEN MAIN.LOC_MANAGER_YN <>'00'                                                                        " & vbNewLine _
                                               & "             AND MAIN.TOU_HAN_HD_CNT <>'1'                                                                         " & vbNewLine _
                                               & "             AND MAIN.TOUHAN_SASHIZU_YN ='01'                                                                      " & vbNewLine _
                                               & "             AND @TOU_HAN_FLG = '01' THEN MAIN.HAN_HD_2                                                            " & vbNewLine _
                                               & "            WHEN MAIN.LOC_MANAGER_YN <>'00'                                                                        " & vbNewLine _
                                               & "             AND MAIN.TOU_HAN_HD_CNT <>'1'                                                                         " & vbNewLine _
                                               & "             AND MAIN.TOUHAN_SASHIZU_YN ='01'                                                                      " & vbNewLine _
                                               & "             AND @TOU_HAN_FLG <> '01' THEN '00'                                                                    " & vbNewLine _
                                               & "            WHEN MAIN.LOC_MANAGER_YN <>'00'                                                                        " & vbNewLine _
                                               & "             AND MAIN.TOU_HAN_HD_CNT <>'1'                                                                         " & vbNewLine _
                                               & "             AND MAIN.TOUHAN_SASHIZU_YN <>'01'                                                                     " & vbNewLine _
                                               & "             AND (MAIN.NRS_BR_CD ='50' AND MAIN.CUST_NAIYO ='01')THEN MAIN.HAN_HD_2                                " & vbNewLine _
                                               & "            WHEN MAIN.LOC_MANAGER_YN <>'00'                                                                        " & vbNewLine _
                                               & "             AND MAIN.TOU_HAN_HD_CNT <>'1'                                                                         " & vbNewLine _
                                               & "             AND MAIN.TOUHAN_SASHIZU_YN <>'01'                                                                     " & vbNewLine _
                                               & "             AND NOT(MAIN.NRS_BR_CD ='50'AND MAIN.CUST_NAIYO ='01')THEN '00'                                       " & vbNewLine _
                                               & "            WHEN MAIN.TOU_HAN_HD_CNT ='1' THEN MAIN.HAN_HD_2                                                       " & vbNewLine _
                                               & "            ELSE '00'                                                                                              " & vbNewLine _
                                               & "       END                           AS HAN_HD                                                                     " & vbNewLine _
                                               & "     , MAIN.OUTKA_NO_L               AS OUTKA_NO_L                                                                 " & vbNewLine _
                                               & "     , MAIN.OUTKA_NO_M               AS OUTKA_NO_M                                                                 " & vbNewLine _
                                               & "     , MIN(MAIN.OUTKA_NO_S)          AS OUTKA_NO_S                                                                 " & vbNewLine _
                                               & "     , MAIN.EDI_CTL_NO_CHU           AS EDI_CTL_NO_CHU                                                             " & vbNewLine _
                                               & "     , CASE WHEN MAIN.EDI_SET_NO = '' THEN MAIN.EDI_CTL_NO_CHU                                                     " & vbNewLine _
                                               & "            ELSE MAIN.EDI_SET_NO                                                                                   " & vbNewLine _
                                               & "       END                           AS EDI_SET_NO                                                                 " & vbNewLine _
                                               & "     , MAIN.NRS_BR_CD                AS NRS_BR_CD                                                                  " & vbNewLine _
                                               & "     , MAIN.NRS_BR_NM                AS NRS_BR_NM                                                                  " & vbNewLine _
                                               & "     , MAIN.WH_CD                    AS WH_CD                                                                      " & vbNewLine _
                                               & "     , MAIN.WH_NM                    AS WH_NM                                                                      " & vbNewLine _
                                               & "     , MAIN.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE                                                            " & vbNewLine _
                                               & "     , ''                            AS OUTKA_PLAN_TIME                                                            " & vbNewLine _
                                               & "     , MAIN.OUTKO_DATE               AS OUTKO_DATE                                                                 " & vbNewLine _
                                               & "     , ''                            AS OUTKO_TIME                                                                 " & vbNewLine _
                                               & "     , MAIN.ARR_PLAN_DATE            AS ARR_PLAN_DATE                                                              " & vbNewLine _
                                               & "     , MAIN.ARR_PLAN_TIME            AS ARR_PLAN_TIME                                                              " & vbNewLine _
                                               & "     , MAIN.CUST_CD_L                AS CUST_CD_L                                                                  " & vbNewLine _
                                               & "     , MAIN.CUST_CD_M                AS CUST_CD_M                                                                  " & vbNewLine _
                                               & "     , MAIN.CUST_NM_L                AS CUST_NM_L                                                                  " & vbNewLine _
                                               & "     , MAIN.CUST_NM_M                AS CUST_NM_M                                                                  " & vbNewLine _
                                               & "     , MAIN.AD_1                     AS AD_1                                                                       " & vbNewLine _
                                               & "     , MAIN.AD_2                     AS AD_2                                                                       " & vbNewLine _
                                               & "     , MAIN.AD_3                     AS AD_3                                                                       " & vbNewLine _
                                               & "     , ''                            AS AD_4                                                                       " & vbNewLine _
                                               & "     , ''                            AS AD_5                                                                       " & vbNewLine _
                                               & "     , MAIN.TEL                      AS TEL                                                                        " & vbNewLine _
                                               & "     , MAIN.FAX                      AS FAX                                                                        " & vbNewLine _
                                               & "     , MAIN.PIC                      AS NRS_BR_PIC_NM                                                              " & vbNewLine _
                                               & "     , MAIN.URIG_NM                  AS URIG_NM                                                                    " & vbNewLine _
                                               & "     , MAIN.DEST_CD                  AS DEST_CD                                                                    " & vbNewLine _
                                               & "     , MAIN.DEST_NM                  AS DEST_NM                                                                    " & vbNewLine _
                                               & "     , MAIN.DEST_AD_1                AS DEST_AD_1                                                                  " & vbNewLine _
                                               & "     , MAIN.DEST_AD_2                AS DEST_AD_2                                                                  " & vbNewLine _
                                               & "     , MAIN.DEST_AD_3                AS DEST_AD_3                                                                  " & vbNewLine _
                                               & "     , MAIN.KANA_NM                  AS KANA_NM                                                                    " & vbNewLine _
                                               & "     , ''                            AS DEST_AD_5                                                                  " & vbNewLine _
                                               & "     , MAIN.DEST_TEL                 AS DEST_TEL                                                                   " & vbNewLine _
                                               & "     , ''                            AS S_TOU_NO                                                                   " & vbNewLine _
                                               & "     , ''                            AS S_SITU_NO                                                                  " & vbNewLine _
                                               & "     , ''                            AS S_ZONE_CD                                                                  " & vbNewLine _
                                               & "     , ''                            AS S_NM                                                                       " & vbNewLine _
                                               & "     , MAIN.CUST_ORD_NO_DTL          AS CUST_ORD_NO_DTL                                                            " & vbNewLine _
                                               & "     , MAIN.BUYER_ORD_NO_DTL         AS BUYER_ORD_NO_DTL                                                           " & vbNewLine _
                                               & "     , MAIN.UNSOCO_CD                AS UNSOCO_CD                                                                  " & vbNewLine _
                                               & "     , MAIN.UNSOCO_NM                AS UNSOCO_NM                                                                  " & vbNewLine _
                                               & "     , MAIN.PC_KB                    AS PC_KB                                                                      " & vbNewLine _
                                               & "     , MAIN.GOODS_CD_CUST            AS GOODS_CD_CUST                                                              " & vbNewLine _
                                               & "     , MAIN.GOODS_NM                 AS GOODS_NM                                                                   " & vbNewLine _
                                               & "     , MAIN.LOT_NO                   AS LOT_NO                                                                     " & vbNewLine _
                                               & "     , MAIN.LT_DATE                  AS LT_DATE                                                                    " & vbNewLine _
                                               & "     , MAIN.SERIAL_NO                AS SERIAL_NO                                                                  " & vbNewLine _
                                               & "     , SUM(MAIN.ALCTD_NB) over(partition by MAIN.NRS_BR_CD,MAIN.OUTKA_NO_L) AS SUM_ALCTD_NB                        " & vbNewLine _
                                               & "     , MAIN.ALCTD_KB                 AS ALCTD_KB                                                                   " & vbNewLine _
                                               & "     , MAIN.OUTKA_PKG_NB             AS OUTKA_PKG_NB                                                               " & vbNewLine _
                                               & "     , MAIN.OUTKA_HASU               AS OUTKA_HASU                                                                 " & vbNewLine _
                                               & "     , MAIN.OUTKA_PKG_NB_S           AS OUTKA_PKG_NB_S                                                             " & vbNewLine _
                                               & "     , MAIN.OUTKA_HASU_S             AS OUTKA_HASU_S                                                               " & vbNewLine _
                                               & "     , MAIN.OUTKA_QT                 AS OUTKA_QT                                                                   " & vbNewLine _
                                               & "     , MAIN.OUTKA_TTL_NB             AS OUTKA_TTL_NB                                                               " & vbNewLine _
                                               & "     , MAIN.OUTKA_TTL_QT             AS OUTKA_TTL_QT                                                               " & vbNewLine _
                                               & "     , SUM(MAIN.ALCTD_NB) over(partition by MAIN.NRS_BR_CD,MAIN.OUTKA_NO_L,MAIN.OUTKA_NO_M) AS ALCTD_NB            " & vbNewLine _
                                               & "     , SUM(MAIN.ALCTD_QT) over(partition by MAIN.NRS_BR_CD,MAIN.OUTKA_NO_L,MAIN.OUTKA_NO_M) AS ALCTD_QT            " & vbNewLine _
                                               & "     , MAIN.PORA_ZAI_NB              AS PORA_ZAI_NB                                                                " & vbNewLine _
                                               & "     , MAIN.PORA_ZAI_QT              AS PORA_ZAI_QT                                                                " & vbNewLine _
                                               & "     , MAIN.NB_UT                    AS NB_UT                                                                      " & vbNewLine _
                                               & "     , MAIN.IRIME_UT                 AS QT_UT                                                                      " & vbNewLine _
                                               & "     , MAIN.PKG_NB                   AS PKG_NB                                                                     " & vbNewLine _
                                               & "     , MAIN.PKG_UT                   AS PKG_UT                                                                     " & vbNewLine _
                                               & "     , MAIN.IRIME                    AS IRIME                                                                      " & vbNewLine _
                                               & "     , MAIN.IRIME_UT                 AS IRIME_UT                                                                   " & vbNewLine _
                                               & "     , MAIN.ZAI_REC_NO               AS ZAI_REC_NO                                                                 " & vbNewLine _
                                               & "     , MAIN.TOU_NO                   AS TOU_NO                                                                     " & vbNewLine _
                                               & "     , MAIN.SITU_NO                  AS SITU_NO                                                                    " & vbNewLine _
                                               & "     , RTRIM(MAIN.ZONE_CD)           AS ZONE_CD                                                                    " & vbNewLine _
                                               & "     , MAIN.LOCA                     AS LOCA                                                                       " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_REC_NO_1                                                         " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_CD_1                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_RYAK_NM                                                          " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_NM_1                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_REC_NO_2                                                         " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_CD_2                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_NM_2                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_REC_NO_3                                                         " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_CD_3                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_NM_3                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_REC_NO_4                                                         " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_CD_4                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_NM_4                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_REC_NO_5                                                         " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_CD_5                                                             " & vbNewLine _
                                               & "     , ''                            AS SAGYO_MEI_NM_5                                                             " & vbNewLine _
                                               & "     , MAIN.RPT_FLG                  AS RPT_FLG                                                                    " & vbNewLine _
                                               & "     , MAIN.HAN_DTL                  AS HAN_DTL                                                                    " & vbNewLine _
                                               & "     , MAIN.REMARK_M                 AS REMARK_M                                                                   " & vbNewLine _
                                               & "     , MAIN.CUST_ORD_NO              AS CUST_ORD_NO                                                                " & vbNewLine _
                                               & "     , MAIN.BUYER_ORD_NO             AS BUYER_ORD_NO                                                               " & vbNewLine _
                                               & "     , MAIN.REMARK_L                 AS REMARK_L                                                                   " & vbNewLine _
                                               & "     , MAIN.GOODS_COND_NM_1          AS GOODS_COND_NM_1                                                            " & vbNewLine _
                                               & "     , MAIN.GOODS_COND_NM_2          AS GOODS_COND_NM_2                                                            " & vbNewLine _
                                               & "     , MAIN.REMARK_ZAI               AS REMARK_ZAI                                                                 " & vbNewLine _
                                               & "     , CASE WHEN MAIN.PKG_NB = 1 THEN MAIN.ALLOC_CAN_NB                                                            " & vbNewLine _
                                               & "            WHEN MAIN.PKG_NB = 0 THEN 0                                                                            " & vbNewLine _
                                               & "            ELSE MAIN.ALLOC_CAN_NB / MAIN.PKG_NB                                                                   " & vbNewLine _
                                               & "       END                           AS ZAN_KONSU                                                                  " & vbNewLine _
                                               & "     , CASE WHEN MAIN.PKG_NB = 1 THEN 0                                                                            " & vbNewLine _
                                               & "            WHEN MAIN.PKG_NB = 0 THEN 0                                                                            " & vbNewLine _
                                               & "            ELSE MAIN.ALLOC_CAN_NB % MAIN.PKG_NB                                                                   " & vbNewLine _
                                               & "       END                           AS ZAN_HASU                                                                   " & vbNewLine _
                                               & "     , MAIN.INKO_DATE                AS INKO_DATE                                                                  " & vbNewLine _
                                               & "     , CASE WHEN MAIN.PKG_NB = 1 THEN MAIN.HASU_Z_ALL_CAL                                                          " & vbNewLine _
                                               & "            WHEN MAIN.PKG_NB = 0 THEN 0                                                                            " & vbNewLine _
                                               & "            ELSE MAIN.HASU_Z_ALL_CAL / MAIN.PKG_NB                                                                 " & vbNewLine _
                                               & "       END                           AS PKG_NB_Z_ALL                                                               " & vbNewLine _
                                               & "     , CASE WHEN MAIN.PKG_NB = 1 THEN 0                                                                            " & vbNewLine _
                                               & "            WHEN MAIN.PKG_NB = 0 THEN 0                                                                            " & vbNewLine _
                                               & "            ELSE MAIN.HASU_Z_ALL_CAL % MAIN.PKG_NB                                                                 " & vbNewLine _
                                               & "      END                           AS HASU_Z_ALL                                                                  " & vbNewLine _
                                               & "     , CASE WHEN MAIN.PKG_NB = 1 THEN MIN(MAIN.ALCTD_CAN_NB)                                                       " & vbNewLine _
                                               & "            WHEN MAIN.PKG_NB = 0 THEN 0                                                                            " & vbNewLine _
                                               & "            ELSE MIN(MAIN.HASU_S_CAL) / MAIN.PKG_NB                                                                " & vbNewLine _
                                               & "       END                           AS PKG_NB_S                                                                   " & vbNewLine _
                                               & "     , CASE WHEN MAIN.PKG_NB = 1 THEN 0                                                                            " & vbNewLine _
                                               & "            WHEN MAIN.PKG_NB = 0 THEN 0                                                                            " & vbNewLine _
                                               & "            ELSE MIN(MAIN.HASU_S_CAL) % MAIN.PKG_NB                                                                " & vbNewLine _
                                               & "       END                           AS HASU_S                                                                     " & vbNewLine _
                                               & "     , MAIN.UNSO_WT                  AS UNSO_WT                                                                    " & vbNewLine _
                                               & "     , MAIN.REMARK_OUT               AS REMARK_OUT                                                                 " & vbNewLine _
                                               & "     , MAIN.KYORI                    AS KYORI                                                                      " & vbNewLine _
                                               & "     , MIN(MAIN.ALCTD_CAN_QT)        AS ALCTD_CAN_QT                                                               " & vbNewLine _
                                               & "     , MAIN.NHS_REMARK               AS NHS_REMARK                                                                 " & vbNewLine _
                                               & "     , MAIN.REMARK_UNSO              AS REMARK_UNSO                                                                " & vbNewLine _
                                               & "     , CASE WHEN MAIN.FREE_N01 < 1 THEN 1                                                                          " & vbNewLine _
                                               & "            ELSE MAIN.FREE_N01                                                                                     " & vbNewLine _
                                               & "       END                           AS FREE_N01                                                                   " & vbNewLine _
                                               & "     , SUM(MAIN.ALCTD_NB) over(partition by MAIN.NRS_BR_CD,MAIN.OUTKA_NO_L) AS FREE_N02                            " & vbNewLine _
                                               & "     , MAIN.FREE_N03                 AS FREE_N03                                                                   " & vbNewLine _
                                               & "     , MAIN.FREE_N04                 AS FREE_N04                                                                   " & vbNewLine _
                                               & "     , '0'                           AS FREE_N05                                                                   " & vbNewLine _
                                               & "     , '0'                           AS FREE_N06                                                                   " & vbNewLine _
                                               & "     , '0'                           AS FREE_N07                                                                   " & vbNewLine _
                                               & "     , '0'                           AS FREE_N08                                                                   " & vbNewLine _
                                               & "     , '0'                           AS FREE_N09                                                                   " & vbNewLine _
                                               & "     , '0'                           AS FREE_N10                                                                   " & vbNewLine _
                                               & "     , MAIN.GOODS_CD_NRS             AS FREE_C01                                                                   " & vbNewLine _
                                               & "     , MAIN.SHIP_CD_L                AS FREE_C02                                                                   " & vbNewLine _
                                               & "     , MAIN.UNSOCO_BR_CD             AS FREE_C03                                                                   " & vbNewLine _
                                               & "     , ''                            AS FREE_C04                                                                   " & vbNewLine _
                                               & "     , MAIN.UNSO_TEHAI_KB            AS FREE_C05                                                                   " & vbNewLine _
                                               & "     , MAIN.SET_NAIYO                AS FREE_C06                                                                   " & vbNewLine _
                                               & "     , ''                            AS FREE_C07                                                                   " & vbNewLine _
                                               & "     , MAIN.GOODS_COND_NM_3          AS FREE_C08                                                                   " & vbNewLine _
                                               & "     , MAIN.CUST_NM_S                AS FREE_C09                                                                   " & vbNewLine _
                                               & "     , MAIN.JIS                      AS FREE_C10                                                                   " & vbNewLine _
                                               & "     , MAIN.CRT_USER                 AS CRT_USER                                                                   " & vbNewLine _
                                               & "     , @SYS_DATE                     AS SYS_DATE                                                                   " & vbNewLine _
                                               & "     , @SYS_TIME                     AS SYS_TIME                                                                   " & vbNewLine
    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ検索用SQL FROM部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DIC_CSV_FROM As String = "FROM                                                                                                          " & vbNewLine _
                                                    & "(SELECT                                                                                                       " & vbNewLine _
                                                    & "       CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                       " & vbNewLine _
                                                    & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                       " & vbNewLine _
                                                    & "            ELSE MR3.RPT_ID                                                                                   " & vbNewLine _
                                                    & "       END                           AS RPT_ID                                                                " & vbNewLine _
                                                    & "     , OUTL.NRS_BR_CD                AS NRS_BR_CD                                                             " & vbNewLine _
                                                    & "     , OUTL.OUTKA_NO_L               AS OUTKA_NO_L                                                            " & vbNewLine _
                                                    & "     , CASE WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                                        " & vbNewLine _
                                                    & "            ELSE OUTL.DEST_CD                                                                                 " & vbNewLine _
                                                    & "       END                           AS DEST_CD                                                               " & vbNewLine _
                                                    & "     , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                        " & vbNewLine _
                                                    & "            WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                        " & vbNewLine _
                                                    & "            ELSE MDOUT.DEST_NM                                                                                " & vbNewLine _
                                                    & "       END                           AS DEST_NM                                                               " & vbNewLine _
                                                    & "     , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                      " & vbNewLine _
                                                    & "            WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                      " & vbNewLine _
                                                    & "            ELSE MDOUT.AD_1                                                                                   " & vbNewLine _
                                                    & "       END                           AS DEST_AD_1                                                             " & vbNewLine _
                                                    & "     , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                      " & vbNewLine _
                                                    & "            WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                      " & vbNewLine _
                                                    & "            ELSE MDOUT.AD_2                                                                                   " & vbNewLine _
                                                    & "       END                           AS DEST_AD_2                                                             " & vbNewLine _
                                                    & "     , OUTL.DEST_AD_3                AS DEST_AD_3                                                             " & vbNewLine _
                                                    & "     , MDOUT.KANA_NM                 AS KANA_NM                                                               " & vbNewLine _
                                                    & "     , OUTL.DEST_TEL                 AS DEST_TEL                                                              " & vbNewLine _
                                                    & "     , OUTL.CUST_CD_L                AS CUST_CD_L                                                             " & vbNewLine _
                                                    & "     , OUTL.CUST_CD_M                AS CUST_CD_M                                                             " & vbNewLine _
                                                    & "     , MC.CUST_NM_L                  AS CUST_NM_L                                                             " & vbNewLine _
                                                    & "     , MC.CUST_NM_M                  AS CUST_NM_M                                                             " & vbNewLine _
                                                    & "     , MC.CUST_NM_S                  AS CUST_NM_S                                                             " & vbNewLine _
                                                    & "     , MC.AD_1                       AS AD_1                                                                  " & vbNewLine _
                                                    & "     , MC.AD_2                       AS AD_2                                                                  " & vbNewLine _
                                                    & "     , MC.AD_3                       AS AD_3                                                                  " & vbNewLine _
                                                    & "     , MC.TEL                        AS TEL                                                                   " & vbNewLine _
                                                    & "     , MC.FAX                        AS FAX                                                                   " & vbNewLine _
                                                    & "     , MC.PIC                        AS PIC                                                                   " & vbNewLine _
                                                    & "     , OUTL.PC_KB                    AS PC_KB                                                                 " & vbNewLine _
                                                    & "     , OUTL.OUTKA_PKG_NB             AS OUTKA_PKG_NB_L                                                        " & vbNewLine _
                                                    & "     , CASE WHEN ISNULL(MG.PKG_NB,0) <= 1 THEN ISNULL(OUTM.ALCTD_NB,0)                                        " & vbNewLine _
                                                    & "            WHEN ISNULL(MG.PKG_NB,0) = 0 THEN 0                                                               " & vbNewLine _
                                                    & "            ELSE ISNULL(OUTM.ALCTD_NB,0) / ISNULL(MG.PKG_NB,0)                                                " & vbNewLine _
                                                    & "       END                           AS OUTKA_PKG_NB                                                          " & vbNewLine _
                                                    & "     , CASE WHEN ISNULL(MG.PKG_NB,0) <= 1 THEN 0                                                              " & vbNewLine _
                                                    & "            WHEN ISNULL(MG.PKG_NB,0) = 0 THEN 0                                                               " & vbNewLine _
                                                    & "            ELSE ISNULL(OUTM.ALCTD_NB,0) % ISNULL(MG.PKG_NB,0)                                                " & vbNewLine _
                                                    & "       END                           AS OUTKA_HASU                                                            " & vbNewLine _
                                                    & "     , CASE WHEN ISNULL(MG.PKG_NB,0) <= 1 THEN ISNULL(OUTS.ALCTD_NB,0)                                        " & vbNewLine _
                                                    & "            WHEN ISNULL(MG.PKG_NB,0) = 0 THEN 0                                                               " & vbNewLine _
                                                    & "            ELSE ISNULL(OUTS.ALCTD_NB,0) / ISNULL(MG.PKG_NB,0)                                                " & vbNewLine _
                                                    & "       END                           AS OUTKA_PKG_NB_S                                                        " & vbNewLine _
                                                    & "     , CASE WHEN ISNULL(MG.PKG_NB,0) <= 1 THEN 0                                                              " & vbNewLine _
                                                    & "            WHEN ISNULL(MG.PKG_NB,0) = 0 THEN 0                                                               " & vbNewLine _
                                                    & "            ELSE ISNULL(OUTS.ALCTD_NB,0) % ISNULL(MG.PKG_NB,0)                                                " & vbNewLine _
                                                    & "       END                           AS OUTKA_HASU_S                                                          " & vbNewLine _
                                                    & "     , OUTM.OUTKA_QT                 AS OUTKA_QT                                                              " & vbNewLine _
                                                    & "     , OUTM.OUTKA_TTL_NB             AS OUTKA_TTL_NB                                                          " & vbNewLine _
                                                    & "     , OUTM.OUTKA_TTL_QT             AS OUTKA_TTL_QT                                                          " & vbNewLine _
                                                    & "     , OUTM.BACKLOG_NB               AS BACKLOG_NB                                                            " & vbNewLine _
                                                    & "     , OUTM.BACKLOG_QT               AS BACKLOG_QT                                                            " & vbNewLine _
                                                    & "     , ZAI.PORA_ZAI_NB               AS PORA_ZAI_NB                                                           " & vbNewLine _
                                                    & "     , ZAI.PORA_ZAI_QT               AS PORA_ZAI_QT                                                           " & vbNewLine _
                                                    & "     , OUTL.CUST_ORD_NO              AS CUST_ORD_NO                                                           " & vbNewLine _
                                                    & "     , OUTL.BUYER_ORD_NO             AS BUYER_ORD_NO                                                          " & vbNewLine _
                                                    & "     , OUTL.OUTKO_DATE               AS OUTKO_DATE                                                            " & vbNewLine _
                                                    & "     , OUTL.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE                                                       " & vbNewLine _
                                                    & "     , OUTL.ARR_PLAN_DATE            AS ARR_PLAN_DATE                                                         " & vbNewLine _
                                                    & "     , KBN1.KBN_NM1                  AS ARR_PLAN_TIME                                                         " & vbNewLine _
                                                    & "     , MUCO.UNSOCO_NM                AS UNSOCO_NM                                                             " & vbNewLine _
                                                    & "     , MUCO.UNSOCO_BR_NM             AS UNSOCO_BR_NM                                                          " & vbNewLine _
                                                    & "     , CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                   " & vbNewLine _
                                                    & "            WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD < MSO.JIS_CD) THEN MKY3.KYORI  " & vbNewLine _
                                                    & "            WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD < EDIL.DEST_JIS_CD) THEN MKY4.KYORI  " & vbNewLine _
                                                    & "            WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                  " & vbNewLine _
                                                    & "            WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS < MSO.JIS_CD) THEN MKY1.KYORI        " & vbNewLine _
                                                    & "            WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD < MDOUT.JIS) THEN MKY2.KYORI        " & vbNewLine _
                                                    & "            ELSE 0                                                                                            " & vbNewLine _
                                                    & "       END                           AS KYORI                                                                 " & vbNewLine _
                                                    & "     , UL.UNSO_WT                    AS UNSO_WT                                                               " & vbNewLine _
                                                    & "     , MDOUTU.DEST_NM                AS URIG_NM                                                               " & vbNewLine _
                                                    & "     , OUTL.REMARK                   AS REMARK_L                                                              " & vbNewLine _
                                                    & "     , UL.REMARK                     AS REMARK_UNSO                                                           " & vbNewLine _
                                                    & "     , '0'                           AS SAIHAKKO_FLG                                                          " & vbNewLine _
                                                    & "     , CASE WHEN MGD.SET_NAIYO = '1' THEN ''                                                                  " & vbNewLine _
                                                    & "            WHEN INL.INKA_STATE_KB < '50' THEN INL.INKA_DATE                                                  " & vbNewLine _
                                                    & "            WHEN INL.INKA_STATE_KB >= '50' THEN ZAI.INKO_DATE                                                 " & vbNewLine _
                                                    & "            ELSE ''                                                                                           " & vbNewLine _
                                                    & "       END                           AS INKO_DATE                                                             " & vbNewLine _
                                                    & "     , ISNULL(ZAI2.ALLOC_CAN_NB,0)   AS HASU_Z_ALL_CAL                                                        " & vbNewLine _
                                                    & "     , CASE WHEN MG.PKG_NB = 1 THEN 0                                                                         " & vbNewLine _
                                                    & "            ELSE OUTS.ALCTD_CAN_NB                                                                            " & vbNewLine _
                                                    & "       END                           AS HASU_S_CAL                                                            " & vbNewLine _
                                                    & "     , MUSER.USER_NM                 AS CRT_USER                                                              " & vbNewLine _
                                                    & "     , OUTM.OUTKA_NO_M               AS OUTKA_NO_M                                                            " & vbNewLine _
                                                    & "     , MG.GOODS_NM_1                 AS GOODS_NM                                                              " & vbNewLine _
                                                    & "     , OUTS.IRIME                    AS IRIME                                                                 " & vbNewLine _
                                                    & "     , MG.STD_IRIME_UT               AS IRIME_UT                                                              " & vbNewLine _
                                                    & "     , OUTS.ALCTD_NB                 AS ALCTD_NB                                                              " & vbNewLine _
                                                    & "     , MG.NB_UT                      AS NB_UT                                                                 " & vbNewLine _
                                                    & "     , OUTS.ALCTD_CAN_NB             AS ALCTD_CAN_NB                                                          " & vbNewLine _
                                                    & "     , OUTS.ALCTD_QT                 AS ALCTD_QT                                                              " & vbNewLine _
                                                    & "     , OUTS.SERIAL_NO                AS SERIAL_NO                                                             " & vbNewLine _
                                                    & "     , MG.PKG_NB                     AS PKG_NB                                                                " & vbNewLine _
                                                    & "     , MG.PKG_UT                     AS PKG_UT                                                                " & vbNewLine _
                                                    & "     , OUTM.ALCTD_KB                 AS ALCTD_KB                                                              " & vbNewLine _
                                                    & "     , OUTS.ALCTD_CAN_QT             AS ALCTD_CAN_QT                                                          " & vbNewLine _
                                                    & "     , OUTS.LOT_NO                   AS LOT_NO                                                                " & vbNewLine _
                                                    & "     , OUTS.OUTKA_NO_S               AS OUTKA_NO_S                                                            " & vbNewLine _
                                                    & "     , INS.LT_DATE                   AS LT_DATE                                                               " & vbNewLine _
                                                    & "     , ZAI.REMARK                    AS REMARK_ZAI                                                            " & vbNewLine _
                                                    & "     , KBN3.KBN_NM1                  AS GOODS_COND_NM_1                                                       " & vbNewLine _
                                                    & "     , KBN4.KBN_NM1                  AS GOODS_COND_NM_2                                                       " & vbNewLine _
                                                    & "     , MG.GOODS_CD_CUST              AS GOODS_CD_CUST                                                         " & vbNewLine _
                                                    & "     , OUTS.BETU_WT                  AS BETU_WT                                                               " & vbNewLine _
                                                    & "     , ZAI.REMARK_OUT                AS REMARK_OUT                                                            " & vbNewLine _
                                                    & "     , OUTM.CUST_ORD_NO_DTL          AS CUST_ORD_NO_DTL                                                       " & vbNewLine _
                                                    & "     , OUTS.TOU_NO                   AS TOU_NO                                                                " & vbNewLine _
                                                    & "     , OUTS.SITU_NO                  AS SITU_NO                                                               " & vbNewLine _
                                                    & "     , RTRIM(OUTS.ZONE_CD)           AS ZONE_CD                                                               " & vbNewLine _
                                                    & "     , OUTS.LOCA                     AS LOCA                                                                  " & vbNewLine _
                                                    & "     , OUTS.ZAI_REC_NO               AS ZAI_REC_NO                                                            " & vbNewLine _
                                                    & "     , OUTM.REMARK                   AS REMARK_M                                                              " & vbNewLine _
                                                    & "     , OUTL.NHS_REMARK               AS NHS_REMARK                                                            " & vbNewLine _
                                                    & "     , EDIM.EDI_CTL_NO_CHU           AS EDI_CTL_NO_CHU                                                        " & vbNewLine _
                                                    & "     , OUTM.EDI_SET_NO               AS EDI_SET_NO                                                            " & vbNewLine _
                                                    & "     , CASE WHEN OUTL.OUTKA_STATE_KB = '10' THEN '0'                                                          " & vbNewLine _
                                                    & "            ELSE '1'                                                                                          " & vbNewLine _
                                                    & "       END                           AS RPT_FLG                                                               " & vbNewLine _
                                                    & "     , TSI.HAN                       AS HAN_DTL                                                               " & vbNewLine _
                                                    & "     , OUTL.WH_CD                    AS WH_CD                                                                 " & vbNewLine _
                                                    & "     , MSO.WH_NM                     AS WH_NM                                                                 " & vbNewLine _
                                                    & "     , MSO.LOC_MANAGER_YN            AS LOC_MANAGER_YN                                                        " & vbNewLine _
                                                    & "     , MSO.TOUHAN_SASHIZU_YN         AS TOUHAN_SASHIZU_YN                                                     " & vbNewLine _
                                                    & "     , NRSV.NRS_BR_NM                AS NRS_BR_NM                                                             " & vbNewLine _
                                                    & "     , MUCO.UNSOCO_CD                AS UNSOCO_CD                                                             " & vbNewLine _
                                                    & "     , MUCO.UNSOCO_BR_CD             AS UNSOCO_BR_CD                                                          " & vbNewLine _
                                                    & "     , OUTM.BUYER_ORD_NO_DTL         AS BUYER_ORD_NO_DTL                                                      " & vbNewLine _
                                                    & "     , MDOUT.JIS                     AS JIS                                                                   " & vbNewLine _
                                                    & "     , OUTM.GOODS_CD_NRS             AS GOODS_CD_NRS                                                          " & vbNewLine _
                                                    & "     , UL.UNSO_TEHAI_KB              AS UNSO_TEHAI_KB                                                         " & vbNewLine _
                                                    & "     , OUTL.SHIP_CD_L                AS SHIP_CD_L                                                             " & vbNewLine _
                                                    & "     , ISNULL(MCC.JOTAI_NM,'')       AS GOODS_COND_NM_3                                                       " & vbNewLine _
                                                    & "     , ISNULL(MGD.SET_NAIYO,'')      AS SET_NAIYO                                                             " & vbNewLine _
                                                    & "     , CASE WHEN ISNULL(MG.STD_IRIME_NB,0) = 0 THEN ISNULL(OUTM.ALCTD_NB,0) * ISNULL(MG.STD_WT_KGS,0)         " & vbNewLine _
                                                    & "            ELSE Round((ISNULL(MG.STD_WT_KGS,0) / ISNULL(MG.STD_IRIME_NB,0)) *  ISNULL(OUTM.ALCTD_QT,0),0)    " & vbNewLine _
                                                    & "       END                           AS FREE_N01                                                              " & vbNewLine _
                                                    & "     , KBN5.VALUE1                   AS FREE_N03                                                              " & vbNewLine _
                                                    & "     , OUTL.MATOME_PICK_FLAG         AS FREE_N04                                                              " & vbNewLine _
                                                    & "     , TSIG.TOU_NO                   AS TOU_HD_2                                                              " & vbNewLine _
                                                    & "     , TSIG.HAN                      AS HAN_HD_2                                                              " & vbNewLine _
                                                    & "     , CASE WHEN 1 = (SELECT count(*)                                                                         " & vbNewLine _
                                                    & "                      FROM(                                                                                   " & vbNewLine _
                                                    & "                      SELECT DISTINCT C.TOU_NO,T.HAN                                                          " & vbNewLine _
                                                    & "                      FROM $LM_TRN$..C_OUTKA_S C                                                              " & vbNewLine _
                                                    & "                      LEFT JOIN $LM_TRN$..C_OUTKA_L L                                                         " & vbNewLine _
                                                    & "                             ON C.NRS_BR_CD = L.NRS_BR_CD                                                     " & vbNewLine _
                                                    & "                            AND C.OUTKA_NO_L = L.OUTKA_NO_L                                                   " & vbNewLine _
                                                    & "                      LEFT JOIN $LM_MST$..M_TOU_SITU T                                                        " & vbNewLine _
                                                    & "                             ON C.NRS_BR_CD = T.NRS_BR_CD                                                     " & vbNewLine _
                                                    & "                            AND C.TOU_NO = T.TOU_NO                                                           " & vbNewLine _
                                                    & "                            AND C.SITU_NO = T.SITU_NO                                                         " & vbNewLine _
                                                    & "                      WHERE C.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                                    & "                        AND C.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
                                                    & "                        AND T.WH_CD = L.WH_CD                                                                 " & vbNewLine _
                                                    & "                        AND C.OUTKA_NO_L = @OUTKA_NO_L                                                        " & vbNewLine _
                                                    & "                      GROUP BY C.TOU_NO,T.HAN                                                                 " & vbNewLine _
                                                    & "                      ) t)                                                                                    " & vbNewLine _
                                                    & "            THEN '1'                                                                                          " & vbNewLine _
                                                    & "            ELSE '0'                                                                                          " & vbNewLine _
                                                    & "       END                           AS TOU_HAN_HD_CNT                                                        " & vbNewLine _
                                                    & "     , MCD.CUST_NAIYO_1              AS CUST_NAIYO                                                            " & vbNewLine _
                                                    & "     , ISNULL(ZAI.ALLOC_CAN_NB,0)    AS ALLOC_CAN_NB                                                          " & vbNewLine _
                                                    & " --出荷L                                                                                                      " & vbNewLine _
                                                    & " FROM $LM_TRN$..C_OUTKA_L OUTL                                                                                " & vbNewLine _
                                                    & " --トランザクションテーブル                                                                                   " & vbNewLine _
                                                    & " --出荷M                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                                           " & vbNewLine _
                                                    & "   ON OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                                                         " & vbNewLine _
                                                    & "  AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                       " & vbNewLine _
                                                    & "  AND OUTM.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine _
                                                    & " --出荷M(中MIN)                                                                                               " & vbNewLine _
                                                    & " LEFT JOIN                                                                                                    " & vbNewLine _
                                                    & " (SELECT                                                                                                      " & vbNewLine _
                                                    & "         NRS_BR_CD                                                                                            " & vbNewLine _
                                                    & "       , OUTKA_NO_L                                                                                           " & vbNewLine _
                                                    & "       , MIN(OUTKA_NO_M) AS  OUTKA_NO_M                                                                       " & vbNewLine _
                                                    & "  FROM $LM_TRN$..C_OUTKA_M WHERE SYS_DEL_FLG ='0'                                                             " & vbNewLine _
                                                    & "  GROUP BY NRS_BR_CD     , OUTKA_NO_L) OUTM_MIN                                                               " & vbNewLine _
                                                    & "        ON OUTM_MIN.NRS_BR_CD = OUTL.NRS_BR_CD                                                                " & vbNewLine _
                                                    & "       AND OUTM_MIN.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                              " & vbNewLine _
                                                    & " --出荷M(中MIN)                                                                                               " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM2                                                                          " & vbNewLine _
                                                    & "        ON OUTM2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                   " & vbNewLine _
                                                    & "       AND OUTM2.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                 " & vbNewLine _
                                                    & "       AND OUTM2.OUTKA_NO_M = OUTM_MIN.OUTKA_NO_M                                                             " & vbNewLine _
                                                    & "       AND OUTM2.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                    & " --出荷S                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                                           " & vbNewLine _
                                                    & "        ON OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                    " & vbNewLine _
                                                    & "       AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                  " & vbNewLine _
                                                    & "       AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                                  " & vbNewLine _
                                                    & "       AND OUTS.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & "  -- --出荷EDIL                                                                                               " & vbNewLine _
                                                    & " LEFT JOIN                                                                                                    " & vbNewLine _
                                                    & " (SELECT *                                                                                                    " & vbNewLine _
                                                    & "  FROM                                                                                                        " & vbNewLine _
                                                    & "  (SELECT                                                                                                     " & vbNewLine _
                                                    & "         EL.NRS_BR_CD                                                                                         " & vbNewLine _
                                                    & "       , EL.OUTKA_CTL_NO                                                                                      " & vbNewLine _
                                                    & "       , EL.EDI_CTL_NO                                                                                        " & vbNewLine _
                                                    & "       , CASE WHEN EL.OUTKA_CTL_NO = '' THEN 1                                                                " & vbNewLine _
                                                    & "         ELSE ROW_NUMBER() OVER (PARTITION BY EL.NRS_BR_CD,EL.OUTKA_CTL_NO ORDER BY EL.NRS_BR_CD,EDI_CTL_NO)  " & vbNewLine _
                                                    & "         END AS IDX                                                                                           " & vbNewLine _
                                                    & "   FROM $LM_TRN$..H_OUTKAEDI_L EL                                                                             " & vbNewLine _
                                                    & "   WHERE                                                                                                      " & vbNewLine _
                                                    & "         EL.SYS_DEL_FLG = '0'                                                                                 " & vbNewLine _
                                                    & "     AND EL.NRS_BR_CD = @NRS_BR_CD                                                                            " & vbNewLine _
                                                    & "     AND EL.OUTKA_CTL_NO = @OUTKA_NO_L) EBASE                                                                 " & vbNewLine _
                                                    & "  WHERE EBASE.IDX = 1) TOPEDI                                                                                 " & vbNewLine _
                                                    & "     ON TOPEDI.NRS_BR_CD = OUTL.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "    AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                                                 " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                                        " & vbNewLine _
                                                    & "        ON TOPEDI.NRS_BR_CD = EDIL.NRS_BR_CD                                                                  " & vbNewLine _
                                                    & "       AND TOPEDI.EDI_CTL_NO = EDIL.EDI_CTL_NO                                                                " & vbNewLine _
                                                    & " ----出荷EDIM                                                                                                 " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                                                        " & vbNewLine _
                                                    & "        ON EDIM.NRS_BR_CD = EDIL.NRS_BR_CD                                                                    " & vbNewLine _
                                                    & "       AND EDIM.OUTKA_CTL_NO = OUTM.OUTKA_NO_L                                                                " & vbNewLine _
                                                    & "       AND EDIM.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M                                                            " & vbNewLine _
                                                    & "       AND EDIM.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --入荷L                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..B_INKA_L INL                                                                             " & vbNewLine _
                                                    & "        ON INL.NRS_BR_CD = OUTS.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "       AND INL.INKA_NO_L = OUTS.INKA_NO_L                                                                     " & vbNewLine _
                                                    & "       AND INL.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --入荷S                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..B_INKA_S INS                                                                             " & vbNewLine _
                                                    & "        ON INS.NRS_BR_CD = OUTS.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "       AND INS.INKA_NO_L = OUTS.INKA_NO_L                                                                     " & vbNewLine _
                                                    & "       AND INS.INKA_NO_M = OUTS.INKA_NO_M                                                                     " & vbNewLine _
                                                    & "       AND INS.INKA_NO_S = OUTS.INKA_NO_S                                                                     " & vbNewLine _
                                                    & "       AND INS.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --運送L                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                              " & vbNewLine _
                                                    & "        ON UL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                      " & vbNewLine _
                                                    & "       AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                                                  " & vbNewLine _
                                                    & "       AND UL.MOTO_DATA_KB = '20'                                                                             " & vbNewLine _
                                                    & "       AND UL.SYS_DEL_FLG = '0'                                                                               " & vbNewLine _
                                                    & " --#####################################                                                                      " & vbNewLine _
                                                    & " --運送M                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..F_UNSO_M UM                                                                              " & vbNewLine _
                                                    & "        ON UM.NRS_BR_CD = UL.NRS_BR_CD                                                                        " & vbNewLine _
                                                    & "       AND UM.UNSO_NO_L = UL.UNSO_NO_L                                                                        " & vbNewLine _
                                                    & "       AND UM.SYS_DEL_FLG = '0'                                                                               " & vbNewLine _
                                                    & " --#####################################                                                                      " & vbNewLine _
                                                    & " --在庫レコード                                                                                               " & vbNewLine _
                                                    & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                                            " & vbNewLine _
                                                    & "        ON ZAI.NRS_BR_CD = OUTS.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "       AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                                                   " & vbNewLine _
                                                    & "       AND ZAI.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --#####################################                                                                      " & vbNewLine _
                                                    & " ----棟室マスタ(集計バージョン)                                                                               " & vbNewLine _
                                                    & " LEFT JOIN                                                                                                    " & vbNewLine _
                                                    & " (SELECT DISTINCT C.TOU_NO,T.HAN,T.WH_CD                                                                      " & vbNewLine _
                                                    & "  FROM $LM_TRN$..C_OUTKA_S C                                                                                  " & vbNewLine _
                                                    & "  LEFT JOIN $LM_MST$..M_TOU_SITU T                                                                            " & vbNewLine _
                                                    & "         ON C.NRS_BR_CD = T.NRS_BR_CD                                                                         " & vbNewLine _
                                                    & "        AND C.TOU_NO = T.TOU_NO                                                                               " & vbNewLine _
                                                    & "        AND C.SITU_NO = T.SITU_NO                                                                             " & vbNewLine _
                                                    & "  WHERE C.NRS_BR_CD =@NRS_BR_CD                                                                               " & vbNewLine _
                                                    & "    AND T.WH_CD =@WH_CD                                                                                       " & vbNewLine _
                                                    & "    AND C.OUTKA_NO_L =@OUTKA_NO_L                                                                             " & vbNewLine _
                                                    & "    AND C.SYS_DEL_FLG ='0'                                                                                    " & vbNewLine _
                                                    & "  GROUP BY C.TOU_NO,T.HAN,T.WH_CD) TSIG                                                                       " & vbNewLine _
                                                    & "        ON TSIG.WH_CD = OUTL.WH_CD                                                                            " & vbNewLine _
                                                    & "       AND TSIG.TOU_NO = OUTS.TOU_NO                                                                          " & vbNewLine _
                                                    & " ----#####################################                                                                    " & vbNewLine _
                                                    & " --マスタテーブル                                                                                             " & vbNewLine _
                                                    & " --商品M                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_GOODS MG                                                                               " & vbNewLine _
                                                    & "        ON MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                                      " & vbNewLine _
                                                    & "       AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                                " & vbNewLine _
                                                    & " --商品DetlM                                                                                                  " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                                                                      " & vbNewLine _
                                                    & "        ON MGD.NRS_BR_CD = MG.NRS_BR_CD                                                                       " & vbNewLine _
                                                    & "       AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                 " & vbNewLine _
                                                    & "       AND MGD.SUB_KB = '09'                                                                                  " & vbNewLine _
                                                    & " --#####################################                                                                      " & vbNewLine _
                                                    & " --棟室M                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_TOU_SITU TSI                                                                           " & vbNewLine _
                                                    & "        ON TSI.WH_CD = OUTL.WH_CD                                                                             " & vbNewLine _
                                                    & "       AND TSI.TOU_NO = OUTS.TOU_NO                                                                           " & vbNewLine _
                                                    & "       AND TSI.SITU_NO = OUTS.SITU_NO                                                                         " & vbNewLine _
                                                    & " --在庫レコード(商品、ロット、置場等で集計)                                                                   " & vbNewLine _
                                                    & " LEFT JOIN                                                                                                    " & vbNewLine _
                                                    & " (SELECT                                                                                                      " & vbNewLine _
                                                    & "         SUM(ALLOC_CAN_NB) AS ALLOC_CAN_NB                                                                    " & vbNewLine _
                                                    & "       , SUM(ALLOC_CAN_QT) AS ALLOC_CAN_QT                                                                    " & vbNewLine _
                                                    & "       , NRS_BR_CD                                                                                            " & vbNewLine _
                                                    & "       , WH_CD                                                                                                " & vbNewLine _
                                                    & "       , TOU_NO                                                                                               " & vbNewLine _
                                                    & "       , SITU_NO                                                                                              " & vbNewLine _
                                                    & "       , ZONE_CD                                                                                              " & vbNewLine _
                                                    & "       , LOCA                                                                                                 " & vbNewLine _
                                                    & "       , LOT_NO                                                                                               " & vbNewLine _
                                                    & "       , GOODS_CD_NRS                                                                                         " & vbNewLine _
                                                    & "       , SERIAL_NO                                                                                            " & vbNewLine _
                                                    & "       , GOODS_COND_KB_1                                                                                      " & vbNewLine _
                                                    & "       , GOODS_COND_KB_2                                                                                      " & vbNewLine _
                                                    & "       , GOODS_COND_KB_3                                                                                      " & vbNewLine _
                                                    & "       , REMARK_OUT                                                                                           " & vbNewLine _
                                                    & "       , IRIME                                                                                                " & vbNewLine _
                                                    & "       , INKO_DATE                                                                                            " & vbNewLine _
                                                    & "       , INKO_PLAN_DATE                                                                                       " & vbNewLine _
                                                    & "       , LT_DATE                                                                                              " & vbNewLine _
                                                    & "       , REMARK                                                                                               " & vbNewLine _
                                                    & "       , SYS_DEL_FLG                                                                                          " & vbNewLine _
                                                    & "  FROM $LM_TRN$..D_ZAI_TRS                                                                                    " & vbNewLine _
                                                    & "  WHERE                                                                                                       " & vbNewLine _
                                                    & "        ALLOC_CAN_NB > 0                                                                                      " & vbNewLine _
                                                    & "    AND SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                                                    & "  GROUP BY NRS_BR_CD                                                                                          " & vbNewLine _
                                                    & "         , WH_CD                                                                                              " & vbNewLine _
                                                    & "         , TOU_NO                                                                                             " & vbNewLine _
                                                    & "         , SITU_NO                                                                                            " & vbNewLine _
                                                    & "         , ZONE_CD                                                                                            " & vbNewLine _
                                                    & "         , LOCA                                                                                               " & vbNewLine _
                                                    & "         , LOT_NO                                                                                             " & vbNewLine _
                                                    & "         , GOODS_CD_NRS                                                                                       " & vbNewLine _
                                                    & "         , SERIAL_NO                                                                                          " & vbNewLine _
                                                    & "         , GOODS_COND_KB_1                                                                                    " & vbNewLine _
                                                    & "         , GOODS_COND_KB_2                                                                                    " & vbNewLine _
                                                    & "         , GOODS_COND_KB_3                                                                                    " & vbNewLine _
                                                    & "         , REMARK_OUT                                                                                         " & vbNewLine _
                                                    & "         , IRIME                                                                                              " & vbNewLine _
                                                    & "         , INKO_DATE                                                                                          " & vbNewLine _
                                                    & "         , INKO_PLAN_DATE                                                                                     " & vbNewLine _
                                                    & "         , LT_DATE                                                                                            " & vbNewLine _
                                                    & "         , REMARK                                                                                             " & vbNewLine _
                                                    & "         , SYS_DEL_FLG) ZAI2                                                                                  " & vbNewLine _
                                                    & "        ON ZAI2.NRS_BR_CD = ZAI.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "       AND ZAI2.WH_CD = ZAI.WH_CD                                                                             " & vbNewLine _
                                                    & "       AND ZAI2.TOU_NO = ZAI.TOU_NO                                                                           " & vbNewLine _
                                                    & "       AND ZAI2.SITU_NO = ZAI.SITU_NO                                                                         " & vbNewLine _
                                                    & "       AND ZAI2.ZONE_CD = ZAI.ZONE_CD                                                                         " & vbNewLine _
                                                    & "       AND ZAI2.LOCA = ZAI.LOCA                                                                               " & vbNewLine _
                                                    & "       AND ZAI2.LOT_NO = ZAI.LOT_NO                                                                           " & vbNewLine _
                                                    & "       AND ZAI2.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                                                               " & vbNewLine _
                                                    & "       AND ZAI2.SERIAL_NO = ZAI.SERIAL_NO                                                                     " & vbNewLine _
                                                    & "       AND ZAI2.GOODS_COND_KB_1 = ZAI.GOODS_COND_KB_1                                                         " & vbNewLine _
                                                    & "       AND ZAI2.GOODS_COND_KB_2 = ZAI.GOODS_COND_KB_2                                                         " & vbNewLine _
                                                    & "       AND ZAI2.GOODS_COND_KB_3 = ZAI.GOODS_COND_KB_3                                                         " & vbNewLine _
                                                    & "       AND ZAI2.REMARK_OUT = ZAI.REMARK_OUT                                                                   " & vbNewLine _
                                                    & "       AND ZAI2.IRIME = ZAI.IRIME                                                                             " & vbNewLine _
                                                    & "       AND ZAI2.REMARK = ZAI.REMARK                                                                           " & vbNewLine _
                                                    & "       AND LEN(ZAI2.REMARK) = LEN(ZAI.REMARK)                                                                 " & vbNewLine _
                                                    & "       AND (ZAI2.INKO_DATE =                                                                                  " & vbNewLine _
                                                    & "           (CASE WHEN ISNULL(MGD.SET_NAIYO,'') <> '1'                                                         " & vbNewLine _
                                                    & "                  AND ZAI.INKO_DATE <> '' THEN ZAI.INKO_DATE                                                  " & vbNewLine _
                                                    & "                 ELSE ZAI.INKO_PLAN_DATE                                                                      " & vbNewLine _
                                                    & "            END)                                                                                              " & vbNewLine _
                                                    & "        OR (ZAI2.INKO_DATE = '' OR ZAI2.INKO_DATE IS NULL)                                                    " & vbNewLine _
                                                    & "       AND ZAI2.INKO_PLAN_DATE =                                                                              " & vbNewLine _
                                                    & "           (CASE WHEN ISNULL(MGD.SET_NAIYO,'') <> '1'                                                         " & vbNewLine _
                                                    & "                  AND ZAI.INKO_DATE <> '' THEN ZAI.INKO_DATE                                                  " & vbNewLine _
                                                    & "                 ELSE ZAI.INKO_PLAN_DATE                                                                      " & vbNewLine _
                                                    & "            END))                                                                                             " & vbNewLine _
                                                    & "       AND ZAI2.LT_DATE =                                                                                     " & vbNewLine _
                                                    & "          (CASE WHEN ISNULL(MGD.SET_NAIYO,'') <> '1'                                                          " & vbNewLine _
                                                    & "                 AND ZAI.LT_DATE <> '' THEN ZAI.LT_DATE                                                       " & vbNewLine _
                                                    & "                ELSE ''                                                                                       " & vbNewLine _
                                                    & "           END)                                                                                               " & vbNewLine _
                                                    & " --#####################################                                                                      " & vbNewLine _
                                                    & " --商品M(MIN)                                                                                                 " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                                                      " & vbNewLine _
                                                    & "        ON M_GOODS_MIN.NRS_BR_CD      = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                                    & "       AND M_GOODS_MIN.GOODS_CD_NRS   = OUTM2.GOODS_CD_NRS                                                    " & vbNewLine _
                                                    & " --荷主M(商品M経由)                                                                                           " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_CUST MC                                                                                " & vbNewLine _
                                                    & "        ON MC.NRS_BR_CD = MG.NRS_BR_CD                                                                        " & vbNewLine _
                                                    & "       AND MC.CUST_CD_L = MG.CUST_CD_L                                                                        " & vbNewLine _
                                                    & "       AND MC.CUST_CD_M = MG.CUST_CD_M                                                                        " & vbNewLine _
                                                    & "       AND MC.CUST_CD_S = MG.CUST_CD_S                                                                        " & vbNewLine _
                                                    & "       AND MC.CUST_CD_SS = MG.CUST_CD_SS                                                                      " & vbNewLine _
                                                    & "       AND MC.SYS_DEL_FLG = '0'                                                                               " & vbNewLine _
                                                    & " --荷主M(商品M経由) 最小の出荷(中)で抽出                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_CUST MC2                                                                               " & vbNewLine _
                                                    & "        ON MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                              " & vbNewLine _
                                                    & "       AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                              " & vbNewLine _
                                                    & "       AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                              " & vbNewLine _
                                                    & "       AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                              " & vbNewLine _
                                                    & "       AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                            " & vbNewLine _
                                                    & "       AND MC2.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --荷主明細                                                                                                   " & vbNewLine _
                                                    & " LEFT JOIN                                                                                                    " & vbNewLine _
                                                    & " (SELECT                                                                                                      " & vbNewLine _
                                                    & "         NRS_BR_CD                                                                                            " & vbNewLine _
                                                    & "       , CUST_CD,CUST_CLASS                                                                                   " & vbNewLine _
                                                    & "       , SET_NAIYO AS CUST_NAIYO_1                                                                            " & vbNewLine _
                                                    & "       , SET_NAIYO_2 AS CUST_NAIYO_2                                                                          " & vbNewLine _
                                                    & "       , SET_NAIYO_3 AS CUST_NAIYO_3                                                                          " & vbNewLine _
                                                    & "  FROM $LM_MST$..M_CUST_DETAILS                                                                               " & vbNewLine _
                                                    & "  WHERE SUB_KB ='40'                                                                                          " & vbNewLine _
                                                    & "    AND SYS_DEL_FLG ='0') MCD                                                                                 " & vbNewLine _
                                                    & "        ON MC.NRS_BR_CD = MCD.NRS_BR_CD                                                                       " & vbNewLine _
                                                    & "       AND (CASE WHEN MCD.CUST_CLASS = '00' THEN MC.CUST_CD_L                                                 " & vbNewLine _
                                                    & "                 WHEN MCD.CUST_CLASS = '01' THEN (MC.CUST_CD_L + MC.CUST_CD_M)                                " & vbNewLine _
                                                    & "                 WHEN MCD.CUST_CLASS = '02' THEN (MC.CUST_CD_L + MC.CUST_CD_M + MC.CUST_CD_S)                 " & vbNewLine _
                                                    & "                 WHEN MCD.CUST_CLASS = '03' THEN (MC.CUST_CD_L + MC.CUST_CD_M + MC.CUST_CD_S + MC.CUST_CD_SS) " & vbNewLine _
                                                    & "            END) = MCD.CUST_CD                                                                                " & vbNewLine _
                                                    & " --届先M(届先取得)(出荷L参照)                                                                                 " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_DEST MDOUT                                                                             " & vbNewLine _
                                                    & "        ON MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                                                   " & vbNewLine _
                                                    & "       AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                                                   " & vbNewLine _
                                                    & "       AND MDOUT.DEST_CD = OUTL.DEST_CD                                                                       " & vbNewLine _
                                                    & "       AND MDOUT.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                    & " --届先M(売上先取得)(出荷L参照)                                                                               " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_DEST MDOUTU                                                                            " & vbNewLine _
                                                    & "        ON MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                                                  " & vbNewLine _
                                                    & "       AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                                                  " & vbNewLine _
                                                    & "       AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                                                    " & vbNewLine _
                                                    & "       AND MDOUTU.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                                                    & " --届先M(届先取得)(出荷EDIL参照)                                                                              " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_DEST MDEDI                                                                             " & vbNewLine _
                                                    & "        ON MDEDI.NRS_BR_CD = EDIL.NRS_BR_CD                                                                   " & vbNewLine _
                                                    & "       AND MDEDI.CUST_CD_L = EDIL.CUST_CD_L                                                                   " & vbNewLine _
                                                    & "       AND MDEDI.DEST_CD = EDIL.DEST_CD                                                                       " & vbNewLine _
                                                    & "       AND MDEDI.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                    & " --届先M(納品書荷主名義名取得)(届先M参照)                                                                     " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_CUST MC_SALES                                                                          " & vbNewLine _
                                                    & "        ON MC_SALES.NRS_BR_CD  = MDOUT.NRS_BR_CD                                                              " & vbNewLine _
                                                    & "       AND MC_SALES.CUST_CD_L  = MDOUT.SALES_CD                                                               " & vbNewLine _
                                                    & "       AND MC_SALES.CUST_CD_M  = '00'                                                                         " & vbNewLine _
                                                    & "       AND MC_SALES.CUST_CD_S  = '00'                                                                         " & vbNewLine _
                                                    & "       AND MC_SALES.CUST_CD_SS = '00'                                                                         " & vbNewLine _
                                                    & " --運送会社M                                                                                                  " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                                                            " & vbNewLine _
                                                    & "        ON MUCO.NRS_BR_CD = UL.NRS_BR_CD                                                                      " & vbNewLine _
                                                    & "       AND MUCO.UNSOCO_CD = UL.UNSO_CD                                                                        " & vbNewLine _
                                                    & "       AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                                                  " & vbNewLine _
                                                    & "       AND MUCO.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --倉庫M                                                                                                      " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_SOKO MSO                                                                               " & vbNewLine _
                                                    & "        ON MSO.WH_CD = OUTL.WH_CD                                                                             " & vbNewLine _
                                                    & "       AND MSO.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --距離程M(M_DEST.JIS < M_SOKO.JIS_CD)(出荷L参照)                                                             " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_KYORI MKY1                                                                             " & vbNewLine _
                                                    & "        ON MKY1.NRS_BR_CD = OUTL.NRS_BR_CD                                                                    " & vbNewLine _
                                                    & "       AND MKY1.KYORI_CD = MC.BETU_KYORI_CD                                                                   " & vbNewLine _
                                                    & "       AND MKY1.ORIG_JIS_CD = MDOUT.JIS                                                                       " & vbNewLine _
                                                    & "       AND MKY1.DEST_JIS_CD = MSO.JIS_CD                                                                      " & vbNewLine _
                                                    & "       AND MKY1.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --距離程M(M_SOKO.JIS_CD < M_DEST.JIS)(出荷L参照)                                                             " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_KYORI MKY2                                                                             " & vbNewLine _
                                                    & "        ON MKY2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                    " & vbNewLine _
                                                    & "       AND MKY2.KYORI_CD = MC.BETU_KYORI_CD                                                                   " & vbNewLine _
                                                    & "       AND MKY2.ORIG_JIS_CD = MSO.JIS_CD                                                                      " & vbNewLine _
                                                    & "       AND MKY2.DEST_JIS_CD = MDOUT.JIS                                                                       " & vbNewLine _
                                                    & "       AND MKY2.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --距離程M(H_OUTKAEDI_L.DEST_JIS_CD < M_SOKO.JIS_CD)(出荷EDIL参照)                                            " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_KYORI MKY3                                                                             " & vbNewLine _
                                                    & "        ON MKY3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                    " & vbNewLine _
                                                    & "       AND MKY3.KYORI_CD = MC.BETU_KYORI_CD                                                                   " & vbNewLine _
                                                    & "       AND MKY3.ORIG_JIS_CD = EDIL.DEST_JIS_CD                                                                " & vbNewLine _
                                                    & "       AND MKY3.DEST_JIS_CD = MSO.JIS_CD                                                                      " & vbNewLine _
                                                    & "       AND MKY3.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --距離程M(M_SOKO.JIS_CD < H_OUTKAEDI_L.DEST_JIS_CD)(出荷EDIL参照)                                            " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_KYORI MKY4                                                                             " & vbNewLine _
                                                    & "        ON MKY4.NRS_BR_CD = OUTL.NRS_BR_CD                                                                    " & vbNewLine _
                                                    & "       AND MKY4.KYORI_CD = MC.BETU_KYORI_CD                                                                   " & vbNewLine _
                                                    & "       AND MKY4.ORIG_JIS_CD = MSO.JIS_CD                                                                      " & vbNewLine _
                                                    & "       AND MKY4.DEST_JIS_CD = EDIL.DEST_JIS_CD                                                                " & vbNewLine _
                                                    & "       AND MKY4.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --ユーザM                                                                                                    " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..S_USER MUSER                                                                             " & vbNewLine _
                                                    & "        ON MUSER.USER_CD = OUTL.SYS_ENT_USER                                                                  " & vbNewLine _
                                                    & "       AND MUSER.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                    & " --営業所M                                                                                                    " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_NRS_BR NRSV                                                                            " & vbNewLine _
                                                    & "        ON NRSV.NRS_BR_CD   = OUTL.NRS_BR_CD                                                                  " & vbNewLine _
                                                    & "       AND NRSV.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --区分M(納入予定区分)                                                                                        " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                               " & vbNewLine _
                                                    & "        ON KBN1.KBN_GROUP_CD = 'N010'                                                                         " & vbNewLine _
                                                    & "       AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                                                   " & vbNewLine _
                                                    & "       AND KBN1.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --区分M(元着払区分)                                                                                          " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN KBN2                                                                               " & vbNewLine _
                                                    & "        ON KBN2.KBN_GROUP_CD = 'M001'                                                                         " & vbNewLine _
                                                    & "       AND KBN2.KBN_CD = OUTL.PC_KB                                                                           " & vbNewLine _
                                                    & "       AND KBN2.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --区分M(商品状態区分(中身))                                                                                  " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN KBN3                                                                               " & vbNewLine _
                                                    & "        ON KBN3.KBN_GROUP_CD = 'S005'                                                                         " & vbNewLine _
                                                    & "       AND KBN3.KBN_CD = INS.GOODS_COND_KB_1                                                                  " & vbNewLine _
                                                    & "       AND KBN3.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --区分M(商品状態区分(外観))                                                                                  " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN KBN4                                                                               " & vbNewLine _
                                                    & "        ON KBN4.KBN_GROUP_CD = 'S006'                                                                         " & vbNewLine _
                                                    & "       AND KBN4.KBN_CD = INS.GOODS_COND_KB_2                                                                  " & vbNewLine _
                                                    & "       AND KBN4.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                    & " --荷主状態(商品状態荷主)                                                                                     " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_CUSTCOND MCC                                                                           " & vbNewLine _
                                                    & "        ON MCC.NRS_BR_CD = OUTL.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "       AND MCC.CUST_CD_L = OUTL.CUST_CD_L                                                                     " & vbNewLine _
                                                    & "       AND MCC.JOTAI_CD   = ZAI.GOODS_COND_KB_3                                                               " & vbNewLine _
                                                    & "       AND MCC.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --出荷Lでの荷主帳票パターン取得                                                                              " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                          " & vbNewLine _
                                                    & "        ON OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                    " & vbNewLine _
                                                    & "       AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                                    " & vbNewLine _
                                                    & "       AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                                    " & vbNewLine _
                                                    & "       AND '00' = MCR1.CUST_CD_S                                                                              " & vbNewLine _
                                                    & "       AND MCR1.PTN_ID = @RPT_FLG                                                                             " & vbNewLine _
                                                    & " --帳票パターン取得                                                                                           " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_RPT MR1                                                                                " & vbNewLine _
                                                    & "        ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "       AND MR1.PTN_ID = MCR1.PTN_ID                                                                           " & vbNewLine _
                                                    & "       AND MR1.PTN_CD = MCR1.PTN_CD                                                                           " & vbNewLine _
                                                    & "       AND MR1.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --商品Mの荷主での荷主帳票パターン取得                                                                        " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                          " & vbNewLine _
                                                    & "        ON MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                      " & vbNewLine _
                                                    & "       AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                      " & vbNewLine _
                                                    & "       AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                      " & vbNewLine _
                                                    & "       AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                      " & vbNewLine _
                                                    & "       AND MCR2.PTN_ID = @RPT_FLG                                                                             " & vbNewLine _
                                                    & " --帳票パターン取得                                                                                           " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_RPT MR2                                                                                " & vbNewLine _
                                                    & "        ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "       AND MR2.PTN_ID = MCR2.PTN_ID                                                                           " & vbNewLine _
                                                    & "       AND MR2.PTN_CD = MCR2.PTN_CD                                                                           " & vbNewLine _
                                                    & "       AND MR2.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --存在しない場合の帳票パターン取得                                                                           " & vbNewLine _
                                                    & " LEFT LOOP JOIN $LM_MST$..M_RPT MR3                                                                           " & vbNewLine _
                                                    & "        ON MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                     " & vbNewLine _
                                                    & "       AND MR3.PTN_ID = @RPT_FLG                                                                              " & vbNewLine _
                                                    & "       AND MR3.STANDARD_FLAG = '01'                                                                           " & vbNewLine _
                                                    & "       AND MR3.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --日陸拠点_群馬向け                                                                                          " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_NRS_BR NRS                                                                             " & vbNewLine _
                                                    & "        ON NRS.NRS_BR_CD   = OUTL.NRS_BR_CD                                                                   " & vbNewLine _
                                                    & "       AND NRS.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                                    & " --区分M(風袋重量)                                                                                            " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN KBN5                                                                               " & vbNewLine _
                                                    & "        ON KBN5.KBN_GROUP_CD = 'N001'                                                                         " & vbNewLine _
                                                    & "       AND KBN5.KBN_CD = MG.PKG_UT                                                                            " & vbNewLine _
                                                    & "       AND KBN5.SYS_DEL_FLG = '0') MAIN                                                                       " & vbNewLine

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ検索用SQL WHERE部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DIC_CSV_WHERE As String = "WHERE                                                                                                        " & vbNewLine _
                                                     & "      MAIN.NRS_BR_CD = @NRS_BR_CD                                                                            " & vbNewLine _
                                                     & "  AND MAIN.OUTKA_NO_L = @OUTKA_NO_L                                                                          " & vbNewLine

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ検索用SQL GROUP BY部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DIC_CSV_GROUPBY As String = "GROUP BY                                                                                                   " & vbNewLine _
                                                       & "         MAIN.RPT_ID                                                                                       " & vbNewLine _
                                                       & "       , MAIN.TOU_HD_2                                                                                     " & vbNewLine _
                                                       & "       , MAIN.HAN_HD_2                                                                                     " & vbNewLine _
                                                       & "       , MAIN.OUTKA_NO_L                                                                                   " & vbNewLine _
                                                       & "       , MAIN.OUTKA_NO_M                                                                                   " & vbNewLine _
                                                       & "       , MAIN.EDI_CTL_NO_CHU                                                                               " & vbNewLine _
                                                       & "       , MAIN.EDI_SET_NO                                                                                   " & vbNewLine _
                                                       & "       , MAIN.NRS_BR_CD                                                                                    " & vbNewLine _
                                                       & "       , MAIN.NRS_BR_NM                                                                                    " & vbNewLine _
                                                       & "       , MAIN.WH_CD                                                                                        " & vbNewLine _
                                                       & "       , MAIN.WH_NM                                                                                        " & vbNewLine _
                                                       & "       , MAIN.OUTKA_PLAN_DATE                                                                              " & vbNewLine _
                                                       & "       , MAIN.OUTKO_DATE                                                                                   " & vbNewLine _
                                                       & "       , MAIN.ARR_PLAN_DATE                                                                                " & vbNewLine _
                                                       & "       , MAIN.ARR_PLAN_TIME                                                                                " & vbNewLine _
                                                       & "       , MAIN.CUST_CD_L                                                                                    " & vbNewLine _
                                                       & "       , MAIN.CUST_CD_M                                                                                    " & vbNewLine _
                                                       & "       , MAIN.CUST_NM_L                                                                                    " & vbNewLine _
                                                       & "       , MAIN.CUST_NM_M                                                                                    " & vbNewLine _
                                                       & "       , MAIN.AD_1                                                                                         " & vbNewLine _
                                                       & "       , MAIN.AD_2                                                                                         " & vbNewLine _
                                                       & "       , MAIN.AD_3                                                                                         " & vbNewLine _
                                                       & "       , MAIN.TEL                                                                                          " & vbNewLine _
                                                       & "       , MAIN.FAX                                                                                          " & vbNewLine _
                                                       & "       , MAIN.PIC                                                                                          " & vbNewLine _
                                                       & "       , MAIN.URIG_NM                                                                                      " & vbNewLine _
                                                       & "       , MAIN.DEST_CD                                                                                      " & vbNewLine _
                                                       & "       , MAIN.DEST_NM                                                                                      " & vbNewLine _
                                                       & "       , MAIN.DEST_AD_1                                                                                    " & vbNewLine _
                                                       & "       , MAIN.DEST_AD_2                                                                                    " & vbNewLine _
                                                       & "       , MAIN.DEST_AD_3                                                                                    " & vbNewLine _
                                                       & "       , MAIN.KANA_NM                                                                                      " & vbNewLine _
                                                       & "       , MAIN.DEST_TEL                                                                                     " & vbNewLine _
                                                       & "       , MAIN.CUST_ORD_NO_DTL                                                                              " & vbNewLine _
                                                       & "       , MAIN.BUYER_ORD_NO_DTL                                                                             " & vbNewLine _
                                                       & "       , MAIN.UNSOCO_CD                                                                                    " & vbNewLine _
                                                       & "       , MAIN.UNSOCO_NM                                                                                    " & vbNewLine _
                                                       & "       , MAIN.PC_KB                                                                                        " & vbNewLine _
                                                       & "       , MAIN.GOODS_CD_CUST                                                                                " & vbNewLine _
                                                       & "       , MAIN.GOODS_NM                                                                                     " & vbNewLine _
                                                       & "       , MAIN.LOT_NO                                                                                       " & vbNewLine _
                                                       & "       , MAIN.LT_DATE                                                                                      " & vbNewLine _
                                                       & "       , MAIN.SERIAL_NO                                                                                    " & vbNewLine _
                                                       & "       , MAIN.ALCTD_KB                                                                                     " & vbNewLine _
                                                       & "       , MAIN.OUTKA_PKG_NB                                                                                 " & vbNewLine _
                                                       & "       , MAIN.OUTKA_HASU                                                                                   " & vbNewLine _
                                                       & "       , MAIN.OUTKA_PKG_NB_S                                                                               " & vbNewLine _
                                                       & "       , MAIN.OUTKA_HASU_S                                                                                 " & vbNewLine _
                                                       & "       , MAIN.OUTKA_QT                                                                                     " & vbNewLine _
                                                       & "       , MAIN.OUTKA_TTL_NB                                                                                 " & vbNewLine _
                                                       & "       , MAIN.OUTKA_TTL_QT                                                                                 " & vbNewLine _
                                                       & "       , MAIN.ALCTD_NB                                                                                     " & vbNewLine _
                                                       & "       , MAIN.ALCTD_QT                                                                                     " & vbNewLine _
                                                       & "       , MAIN.PORA_ZAI_NB                                                                                  " & vbNewLine _
                                                       & "       , MAIN.PORA_ZAI_QT                                                                                  " & vbNewLine _
                                                       & "       , MAIN.NB_UT                                                                                        " & vbNewLine _
                                                       & "       , MAIN.IRIME_UT                                                                                     " & vbNewLine _
                                                       & "       , MAIN.PKG_NB                                                                                       " & vbNewLine _
                                                       & "       , MAIN.PKG_UT                                                                                       " & vbNewLine _
                                                       & "       , MAIN.IRIME                                                                                        " & vbNewLine _
                                                       & "       , MAIN.ZAI_REC_NO                                                                                   " & vbNewLine _
                                                       & "       , MAIN.TOU_NO                                                                                       " & vbNewLine _
                                                       & "       , MAIN.SITU_NO                                                                                      " & vbNewLine _
                                                       & "       , MAIN.ZONE_CD                                                                                      " & vbNewLine _
                                                       & "       , MAIN.LOCA                                                                                         " & vbNewLine _
                                                       & "       , MAIN.RPT_FLG                                                                                      " & vbNewLine _
                                                       & "       , MAIN.HAN_DTL                                                                                      " & vbNewLine _
                                                       & "       , MAIN.REMARK_M                                                                                     " & vbNewLine _
                                                       & "       , MAIN.CUST_ORD_NO                                                                                  " & vbNewLine _
                                                       & "       , MAIN.BUYER_ORD_NO                                                                                 " & vbNewLine _
                                                       & "       , MAIN.REMARK_L                                                                                     " & vbNewLine _
                                                       & "       , MAIN.GOODS_COND_NM_1                                                                              " & vbNewLine _
                                                       & "       , MAIN.GOODS_COND_NM_2                                                                              " & vbNewLine _
                                                       & "       , MAIN.REMARK_ZAI                                                                                   " & vbNewLine _
                                                       & "       , MAIN.ALLOC_CAN_NB                                                                                 " & vbNewLine _
                                                       & "       , MAIN.INKO_DATE                                                                                    " & vbNewLine _
                                                       & "       , MAIN.HASU_Z_ALL_CAL                                                                               " & vbNewLine _
                                                       & "       , MAIN.HASU_S_CAL                                                                                   " & vbNewLine _
                                                       & "       , MAIN.UNSO_WT                                                                                      " & vbNewLine _
                                                       & "       , MAIN.REMARK_OUT                                                                                   " & vbNewLine _
                                                       & "       , MAIN.KYORI                                                                                        " & vbNewLine _
                                                       & "       , MAIN.NHS_REMARK                                                                                   " & vbNewLine _
                                                       & "       , MAIN.REMARK_UNSO                                                                                  " & vbNewLine _
                                                       & "       , CASE WHEN MAIN.FREE_N01 < 1 THEN 1                                                                " & vbNewLine _
                                                       & "              ELSE MAIN.FREE_N01                                                                           " & vbNewLine _
                                                       & "         END                                                                                               " & vbNewLine _
                                                       & "       , MAIN.FREE_N03                                                                                     " & vbNewLine _
                                                       & "       , MAIN.FREE_N04                                                                                     " & vbNewLine _
                                                       & "       , MAIN.GOODS_CD_NRS                                                                                 " & vbNewLine _
                                                       & "       , MAIN.SHIP_CD_L                                                                                    " & vbNewLine _
                                                       & "       , MAIN.UNSOCO_BR_CD                                                                                 " & vbNewLine _
                                                       & "       , MAIN.UNSO_TEHAI_KB                                                                                " & vbNewLine _
                                                       & "       , MAIN.SET_NAIYO                                                                                    " & vbNewLine _
                                                       & "       , MAIN.GOODS_COND_NM_3                                                                              " & vbNewLine _
                                                       & "       , MAIN.CUST_NM_S                                                                                    " & vbNewLine _
                                                       & "       , MAIN.JIS                                                                                          " & vbNewLine _
                                                       & "       , MAIN.CRT_USER                                                                                     " & vbNewLine _
                                                       & "       , MAIN.LOC_MANAGER_YN                                                                               " & vbNewLine _
                                                       & "       , MAIN.TOUHAN_SASHIZU_YN                                                                            " & vbNewLine _
                                                       & "       , MAIN.TOU_HAN_HD_CNT                                                                               " & vbNewLine _
                                                       & "       , MAIN.CUST_NAIYO                                                                                   " & vbNewLine

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ検索用SQL ORDER BY部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DIC_CSV_ORDERBY As String = "ORDER BY                                                                                                   " & vbNewLine _
                                                       & "         MAIN.NRS_BR_CD                                                                                    " & vbNewLine _
                                                       & "       , MAIN.OUTKA_NO_L                                                                                   " & vbNewLine _
                                                       & "       , MAIN.OUTKA_NO_M                                                                                   " & vbNewLine _
                                                       & "       , MAIN.TOU_NO                                                                                       " & vbNewLine _
                                                       & "       , MAIN.SITU_NO                                                                                      " & vbNewLine _
                                                       & "       , MAIN.ZONE_CD                                                                                      " & vbNewLine _
                                                       & "       , MAIN.LOCA                                                                                         " & vbNewLine _
                                                       & "       , MAIN.GOODS_NM                                                                                     " & vbNewLine _
                                                       & "       , MAIN.PKG_NB                                                                                       " & vbNewLine





#End Region

#Region "出荷取消検索SQL"

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ出荷取消検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DIC_CSV_DEL As String = "SELECT                                                                                                    " & vbNewLine _
                                                   & " @COMPNAME                        AS COMPNAME                                                             " & vbNewLine _
                                                   & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                         " & vbNewLine _
                                                   & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                         " & vbNewLine _
                                                   & "      ELSE MR3.RPT_ID                                                                                     " & vbNewLine _
                                                   & " END                              AS RPT_ID                                                               " & vbNewLine _
                                                   & ",OUTS.TOU_NO                      AS TOU_HD                                                               " & vbNewLine _
                                                   & ",TOSI.HAN                         AS HAN_HD                                                               " & vbNewLine _
                                                   & ",OUTL.OUTKA_NO_L                  AS OUTKA_NO_L                                                           " & vbNewLine _
                                                   & ",OUTM.OUTKA_NO_M                  AS OUTKA_NO_M                                                           " & vbNewLine _
                                                   & ",OUTS.OUTKA_NO_S                  AS OUTKA_NO_S                                                           " & vbNewLine _
                                                   & ",''                               AS EDI_CTL_NO_CHU                                                       " & vbNewLine _
                                                   & ",''                               AS EDI_SET_NO                                                           " & vbNewLine _
                                                   & ",OUTL.NRS_BR_CD                   AS NRS_BR_CD                                                            " & vbNewLine _
                                                   & ",NRSV.NRS_BR_NM                   AS NRS_BR_NM                                                            " & vbNewLine _
                                                   & ",OUTL.WH_CD                       AS WH_CD                                                                " & vbNewLine _
                                                   & ",MSO.WH_NM                        AS WH_NM                                                                " & vbNewLine _
                                                   & ",OUTL.OUTKA_PLAN_DATE             AS OUTKA_PLAN_DATE                                                      " & vbNewLine _
                                                   & ",''                               AS OUTKA_PLAN_TIME                                                      " & vbNewLine _
                                                   & ",OUTL.OUTKO_DATE                  AS OUTKO_DATE                                                           " & vbNewLine _
                                                   & ",''                               AS OUTKO_TIME                                                           " & vbNewLine _
                                                   & ",OUTL.ARR_PLAN_DATE               AS ARR_PLAN_DATE                                                        " & vbNewLine _
                                                   & ",KBN1.KBN_NM1                     AS ARR_PLAN_TIME                                                        " & vbNewLine _
                                                   & ",OUTL.CUST_CD_L                   AS CUST_CD_L                                                            " & vbNewLine _
                                                   & ",OUTL.CUST_CD_M                   AS CUST_CD_M                                                            " & vbNewLine _
                                                   & ",MC.CUST_NM_L                     AS CUST_NM_L                                                            " & vbNewLine _
                                                   & ",MC.CUST_NM_M                     AS CUST_NM_M                                                            " & vbNewLine _
                                                   & ",MC.AD_1                          AS AD_1                                                                 " & vbNewLine _
                                                   & ",MC.AD_2                          AS AD_2                                                                 " & vbNewLine _
                                                   & ",MC.AD_3                          AS AD_3                                                                 " & vbNewLine _
                                                   & ",''                               AS AD_4                                                                 " & vbNewLine _
                                                   & ",''                               AS AD_5                                                                 " & vbNewLine _
                                                   & ",MC.TEL                           AS TEL                                                                  " & vbNewLine _
                                                   & ",MC.FAX                           AS FAX                                                                  " & vbNewLine _
                                                   & ",MC.PIC                           AS NRS_BR_PIC_NM                                                        " & vbNewLine _
                                                   & ",MDOUTU.DEST_NM                   AS URIG_NM                                                              " & vbNewLine _
                                                   & ",OUTL.DEST_CD                     AS DEST_CD                                                              " & vbNewLine _
                                                   & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                          " & vbNewLine _
                                                   & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                          " & vbNewLine _
                                                   & "      ELSE MDOUT.DEST_NM                                                                                  " & vbNewLine _
                                                   & " END                              AS DEST_NM                                                              " & vbNewLine _
                                                   & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                        " & vbNewLine _
                                                   & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                        " & vbNewLine _
                                                   & "      ELSE MDOUT.AD_1                                                                                     " & vbNewLine _
                                                   & " END                              AS DEST_AD_1                                                            " & vbNewLine _
                                                   & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                        " & vbNewLine _
                                                   & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                        " & vbNewLine _
                                                   & "      ELSE MDOUT.AD_2                                                                                     " & vbNewLine _
                                                   & " END                              AS DEST_AD_2                                                            " & vbNewLine _
                                                   & ",OUTL.DEST_AD_3                   AS DEST_AD_3                                                            " & vbNewLine _
                                                   & ",MDOUT.KANA_NM                    AS KANA_NM                                                              " & vbNewLine _
                                                   & ",''                               AS DEST_AD_5                                                            " & vbNewLine _
                                                   & ",OUTL.DEST_TEL                    AS DEST_TEL                                                             " & vbNewLine _
                                                   & ",''                               AS S_TOU_NO                                                             " & vbNewLine _
                                                   & ",''                               AS S_SITU_NO                                                            " & vbNewLine _
                                                   & ",''                               AS S_ZONE_CD                                                            " & vbNewLine _
                                                   & ",''                               AS S_NM                                                                 " & vbNewLine _
                                                   & ",OUTM.CUST_ORD_NO_DTL             AS CUST_ORD_NO_DTL                                                      " & vbNewLine _
                                                   & ",OUTM.BUYER_ORD_NO_DTL            AS BUYER_ORD_NO_DTL                                                     " & vbNewLine _
                                                   & ",MUCO.UNSOCO_CD                   AS UNSOCO_CD                                                            " & vbNewLine _
                                                   & ",MUCO.UNSOCO_NM                   AS UNSOCO_NM                                                            " & vbNewLine _
                                                   & ",OUTL.PC_KB                       AS PC_KB                                                                " & vbNewLine _
                                                   & ",MG.GOODS_CD_CUST                 AS GOODS_CD_CUST                                                        " & vbNewLine _
                                                   & ",MG.GOODS_NM_1                    AS GOODS_NM                                                             " & vbNewLine _
                                                   & ",OUTS.LOT_NO                      AS LOT_NO                                                               " & vbNewLine _
                                                   & ",INS.LT_DATE                      AS LT_DATE                                                              " & vbNewLine _
                                                   & ",OUTS.SERIAL_NO                   AS SERIAL_NO                                                            " & vbNewLine _
                                                   & ",OUTM_G.OUTKA_TTL_NB              AS OUTKA_KNP_NB                                                         " & vbNewLine _
                                                   & ",OUTM.ALCTD_KB                    AS ALCTD_KB                                                             " & vbNewLine _
                                                   & ",OUTM_G.OUTKA_TTL_NB              AS OUTKA_PKG_NB                                                         " & vbNewLine _
                                                   & ",OUTS.ALCTD_NB                    AS OUTKA_HASU                                                           " & vbNewLine _
                                                   & ", CASE WHEN ISNULL(MG.PKG_NB,0) = 1 THEN 0                                                                " & vbNewLine _
                                                   & "       WHEN ISNULL(MG.PKG_NB,0) = 0 THEN 0                                                                " & vbNewLine _
                                                   & "       ELSE ISNULL(OUTS.ALCTD_NB,0) / ISNULL(MG.PKG_NB,0)                                                 " & vbNewLine _
                                                   & "  END                             AS OUTKA_PKG_NB_S                                                       " & vbNewLine _
                                                   & ", CASE WHEN ISNULL(MG.PKG_NB,0) = 1 THEN ISNULL(OUTS.ALCTD_NB,0)                                          " & vbNewLine _
                                                   & "       WHEN ISNULL(MG.PKG_NB,0) = 0 THEN ISNULL(OUTS.ALCTD_NB,0)                                          " & vbNewLine _
                                                   & "       ELSE ISNULL(OUTS.ALCTD_NB,0) % ISNULL(MG.PKG_NB,0)                                                 " & vbNewLine _
                                                   & "  END                             AS OUTKA_HASU_S                                                         " & vbNewLine _
                                                   & ",OUTM.OUTKA_QT                    AS OUTKA_QT                                                             " & vbNewLine _
                                                   & ",OUTM.OUTKA_TTL_NB                AS OUTKA_TTL_NB                                                         " & vbNewLine _
                                                   & ",OUTM.OUTKA_TTL_QT                AS OUTKA_TTL_QT                                                         " & vbNewLine _
                                                   & ",OUTS.ALCTD_NB                    AS ALCTD_NB                                                             " & vbNewLine _
                                                   & ",OUTS.ALCTD_QT                    AS ALCTD_QT                                                             " & vbNewLine _
                                                   & ",OUTM.BACKLOG_NB                  AS BACKLOG_NB                                                           " & vbNewLine _
                                                   & ",OUTM.BACKLOG_QT                  AS BACKLOG_QT                                                           " & vbNewLine _
                                                   & ",MG.NB_UT                         AS NB_UT                                                                " & vbNewLine _
                                                   & ",MG.STD_IRIME_UT                  AS QT_UT                                                                " & vbNewLine _
                                                   & ",MG.PKG_NB                        AS PKG_NB                                                               " & vbNewLine _
                                                   & ",MG.PKG_UT                        AS PKG_UT                                                               " & vbNewLine _
                                                   & ",OUTS.IRIME                       AS IRIME                                                                " & vbNewLine _
                                                   & ",MG.STD_IRIME_UT                  AS IRIME_UT                                                             " & vbNewLine _
                                                   & ",OUTS.ZAI_REC_NO                  AS ZAI_REC_NO                                                           " & vbNewLine _
                                                   & ",OUTS.TOU_NO                      AS TOU_NO                                                               " & vbNewLine _
                                                   & ",OUTS.SITU_NO                     AS SITU_NO                                                              " & vbNewLine _
                                                   & ",RTRIM(OUTS.ZONE_CD)              AS ZONE_CD                                                              " & vbNewLine _
                                                   & ",OUTS.LOCA                        AS LOCA                                                                 " & vbNewLine _
                                                   & ",'1'                              AS RPT_FLG                                                              " & vbNewLine _
                                                   & ",ISNULL(TOSI.HAN,'')              AS HAN_DTL                                                              " & vbNewLine _
                                                   & ",OUTM.REMARK                      AS REMARK_M                                                             " & vbNewLine _
                                                   & ",OUTL.CUST_ORD_NO                 AS CUST_ORD_NO                                                          " & vbNewLine _
                                                   & ",OUTL.BUYER_ORD_NO                AS BUYER_ORD_NO                                                         " & vbNewLine _
                                                   & ",OUTL.REMARK                      AS REMARK_L                                                             " & vbNewLine _
                                                   & ",ISNULL(ZAI.GOODS_COND_KB_1,'')   AS GOODS_COND_KB_1                                                      " & vbNewLine _
                                                   & ",ISNULL(ZAI.GOODS_COND_KB_2,'')   AS GOODS_COND_KB_2                                                      " & vbNewLine _
                                                   & ",ISNULL(ZAI.REMARK,'')            AS REMARK_ZAI                                                           " & vbNewLine _
                                                   & ", CASE WHEN ISNULL(MG.PKG_NB,0) = 1 THEN 0                                                                " & vbNewLine _
                                                   & "       WHEN ISNULL(MG.PKG_NB,0) = 0 THEN 0                                                                " & vbNewLine _
                                                   & "       ELSE ISNULL((ZAI.ALLOC_CAN_NB + OUTS.ALCTD_NB),0) / ISNULL(MG.PKG_NB,0)                            " & vbNewLine _
                                                   & "  END                             AS PKG_NB_Z                                                             " & vbNewLine _
                                                   & ", CASE WHEN ISNULL(MG.PKG_NB,0) = 1 THEN ISNULL((ZAI.ALLOC_CAN_NB + OUTS.ALCTD_NB),0)                     " & vbNewLine _
                                                   & "       WHEN ISNULL(MG.PKG_NB,0) = 0 THEN ISNULL((ZAI.ALLOC_CAN_NB + OUTS.ALCTD_NB),0)                     " & vbNewLine _
                                                   & "       ELSE ISNULL((ZAI.ALLOC_CAN_NB + OUTS.ALCTD_NB),0) % ISNULL(MG.PKG_NB,0)                            " & vbNewLine _
                                                   & "  END                             AS HASU_Z                                                               " & vbNewLine _
                                                   & ", CASE WHEN ISNULL(ZAI.INKO_DATE,'') = '' THEN ISNULL(ZAI.INKO_PLAN_DATE,'')                              " & vbNewLine _
                                                   & "       ELSE ISNULL(ZAI.INKO_DATE,'')                                                                      " & vbNewLine _
                                                   & "  END                             AS INKO_DATE                                                            " & vbNewLine _
                                                   & ",0                                AS PKG_NB_Z_ALL                                                         " & vbNewLine _
                                                   & ",0                                AS HASU_Z_ALL                                                           " & vbNewLine _
                                                   & ",0                                AS PKG_NB_S                                                             " & vbNewLine _
                                                   & ",0                                AS HASU_S                                                               " & vbNewLine _
                                                   & ",0                                AS UNSO_WT                                                              " & vbNewLine _
                                                   & ",''                               AS REMARK_OUT                                                           " & vbNewLine _
                                                   & ",0                                AS UNSO_KYORI                                                           " & vbNewLine _
                                                   & ",0                                AS ALCTD_CAN_QT                                                         " & vbNewLine _
                                                   & ",''                               AS NHS_REMARK                                                           " & vbNewLine _
                                                   & ",''                               AS REMARK_UNSO                                                          " & vbNewLine _
                                                   & ",0                                AS FREE_N01                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N02                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N03                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N04                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N05                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N06                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N07                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N08                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N09                                                             " & vbNewLine _
                                                   & ",0                                AS FREE_N10                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C01                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C02                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C03                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C04                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C05                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C06                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C07                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C08                                                             " & vbNewLine _
                                                   & ",''                               AS FREE_C09                                                             " & vbNewLine _
                                                   & ",MDOUT.JIS                        AS FREE_C10                                                             " & vbNewLine _
                                                   & ",MUSER.USER_NM                    AS CRT_USER                                                             " & vbNewLine _
                                                   & ",@SYS_DATE                        AS SYS_DATE                                                             " & vbNewLine _
                                                   & ",@SYS_TIME                        AS SYS_TIME                                                             " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_REC_NO_1                                                   " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_CD_1                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_NM_1                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_REC_NO_2                                                   " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_CD_2                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_NM_2                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_REC_NO_3                                                   " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_CD_3                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_NM_3                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_REC_NO_4                                                   " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_CD_4                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_NM_4                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_REC_NO_5                                                   " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_CD_5                                                       " & vbNewLine _
                                                   & ",''                               AS SAGYO_MEI_NM_5                                                       " & vbNewLine

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ出荷取消検索用SQL FROM部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DIC_CSV_DEL_FROM As String = "--出荷L                                                                                              " & vbNewLine _
                                                        & "FROM                                                                                                 " & vbNewLine _
                                                        & "$LM_TRN$..C_OUTKA_L OUTL                                                                             " & vbNewLine _
                                                        & "--トランザクションテーブル                                                                           " & vbNewLine _
                                                        & "--出荷M                                                                                              " & vbNewLine _
                                                        & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                                   " & vbNewLine _
                                                        & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                                                  " & vbNewLine _
                                                        & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                " & vbNewLine _
                                                        & "--出荷S                                                                                              " & vbNewLine _
                                                        & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                                   " & vbNewLine _
                                                        & "ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                  " & vbNewLine _
                                                        & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                " & vbNewLine _
                                                        & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                                " & vbNewLine _
                                                        & "--出荷EDIL                                                                                           " & vbNewLine _
                                                        & "LEFT JOIN (                                                                                          " & vbNewLine _
                                                        & "           SELECT                                                                                    " & vbNewLine _
                                                        & "                  NRS_BR_CD                                                                          " & vbNewLine _
                                                        & "                , EDI_CTL_NO                                                                         " & vbNewLine _
                                                        & "                , OUTKA_CTL_NO                                                                       " & vbNewLine _
                                                        & "            FROM (                                                                                   " & vbNewLine _
                                                        & "                   SELECT                                                                            " & vbNewLine _
                                                        & "                          EDIOUTL.NRS_BR_CD                                                          " & vbNewLine _
                                                        & "                        , EDIOUTL.EDI_CTL_NO                                                         " & vbNewLine _
                                                        & "                        , EDIOUTL.OUTKA_CTL_NO                                                       " & vbNewLine _
                                                        & "                        , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                                 " & vbNewLine _
                                                        & "                          ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD                     " & vbNewLine _
                                                        & "                                                             , EDIOUTL.OUTKA_CTL_NO                  " & vbNewLine _
                                                        & "                                                      ORDER BY EDIOUTL.NRS_BR_CD                     " & vbNewLine _
                                                        & "                                                             , EDIOUTL.EDI_CTL_NO                    " & vbNewLine _
                                                        & "                                                 )                                                   " & vbNewLine _
                                                        & "                          END AS IDX                                                                 " & vbNewLine _
                                                        & "                    FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                              " & vbNewLine _
                                                        & "                   WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                                  " & vbNewLine _
                                                        & "                     AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                                           " & vbNewLine _
                                                        & "                     AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                                          " & vbNewLine _
                                                        & "                 ) EBASE                                                                             " & vbNewLine _
                                                        & "           WHERE EBASE.IDX = 1                                                                       " & vbNewLine _
                                                        & "           ) TOPEDI                                                                                  " & vbNewLine _
                                                        & "       ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                                       " & vbNewLine _
                                                        & "      AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                                      " & vbNewLine _
                                                        & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                                " & vbNewLine _
                                                        & "       ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                                         " & vbNewLine _
                                                        & "      AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                                        " & vbNewLine _
                                                        & "--入荷L                                                                                              " & vbNewLine _
                                                        & "LEFT JOIN $LM_TRN$..B_INKA_L INL                                                                     " & vbNewLine _
                                                        & "ON  INL.NRS_BR_CD = OUTS.NRS_BR_CD                                                                   " & vbNewLine _
                                                        & "AND INL.INKA_NO_L = OUTS.INKA_NO_L                                                                   " & vbNewLine _
                                                        & "--入荷S                                                                                              " & vbNewLine _
                                                        & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                                                     " & vbNewLine _
                                                        & "ON  INS.NRS_BR_CD = OUTS.NRS_BR_CD                                                                   " & vbNewLine _
                                                        & "AND INS.INKA_NO_L = OUTS.INKA_NO_L                                                                   " & vbNewLine _
                                                        & "AND INS.INKA_NO_M = OUTS.INKA_NO_M                                                                   " & vbNewLine _
                                                        & "AND INS.INKA_NO_S = OUTS.INKA_NO_S                                                                   " & vbNewLine _
                                                        & "--運送L                                                                                              " & vbNewLine _
                                                        & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                      " & vbNewLine _
                                                        & "ON  UL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                    " & vbNewLine _
                                                        & "AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                                                " & vbNewLine _
                                                        & "AND UL.MOTO_DATA_KB = '20'                                                                           " & vbNewLine _
                                                        & "--#####################################                                                              " & vbNewLine _
                                                        & "--出荷M(出荷総個数合計)                                                                              " & vbNewLine _
                                                        & "LEFT JOIN                                                                                            " & vbNewLine _
                                                        & "(                                                                                                    " & vbNewLine _
                                                        & "SELECT                                                                                               " & vbNewLine _
                                                        & " NRS_BR_CD                                                                                           " & vbNewLine _
                                                        & ",OUTKA_NO_L                                                                                          " & vbNewLine _
                                                        & ",SUM(OUTKA_TTL_NB)   AS OUTKA_TTL_NB                                                                 " & vbNewLine _
                                                        & ",SYS_DEL_FLG                                                                                         " & vbNewLine _
                                                        & "FROM                                                                                                 " & vbNewLine _
                                                        & "$LM_TRN$..C_OUTKA_M                                                                                  " & vbNewLine _
                                                        & "WHERE                                                                                                " & vbNewLine _
                                                        & "NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                                        & "AND OUTKA_NO_L = @OUTKA_NO_L                                                                         " & vbNewLine _
                                                        & "AND SYS_DEL_FLG = '0'                                                                                " & vbNewLine _
                                                        & "GROUP BY                                                                                             " & vbNewLine _
                                                        & " NRS_BR_CD                                                                                           " & vbNewLine _
                                                        & ",OUTKA_NO_L                                                                                          " & vbNewLine _
                                                        & ",SYS_DEL_FLG                                                                                         " & vbNewLine _
                                                        & ") OUTM_G                                                                                             " & vbNewLine _
                                                        & "ON  OUTM_G.NRS_BR_CD = OUTL.NRS_BR_CD                                                                " & vbNewLine _
                                                        & "AND OUTM_G.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                              " & vbNewLine _
                                                        & "AND OUTM_G.SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                                                        & "--#####################################                                                              " & vbNewLine _
                                                        & "--マスタテーブル                                                                                     " & vbNewLine _
                                                        & "--商品M                                                                                              " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                       " & vbNewLine _
                                                        & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                                    " & vbNewLine _
                                                        & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                              " & vbNewLine _
                                                        & "AND MG.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                        & "--荷主M(商品M経由)                                                                                   " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_CUST MC                                                                        " & vbNewLine _
                                                        & "ON  MC.NRS_BR_CD = MG.NRS_BR_CD                                                                      " & vbNewLine _
                                                        & "AND MC.CUST_CD_L = MG.CUST_CD_L                                                                      " & vbNewLine _
                                                        & "AND MC.CUST_CD_M = MG.CUST_CD_M                                                                      " & vbNewLine _
                                                        & "AND MC.CUST_CD_S = MG.CUST_CD_S                                                                      " & vbNewLine _
                                                        & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                                                    " & vbNewLine _
                                                        & "AND MC.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                                        & "--届先M(届先取得)(出荷L参照)                                                                         " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_DEST MDOUT                                                                     " & vbNewLine _
                                                        & "ON  MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                                                 " & vbNewLine _
                                                        & "AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                                                 " & vbNewLine _
                                                        & "AND MDOUT.DEST_CD = OUTL.DEST_CD                                                                     " & vbNewLine _
                                                        & "AND MDOUT.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
                                                        & "--届先M(売上先取得)(出荷L参照)                                                                       " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_DEST MDOUTU                                                                    " & vbNewLine _
                                                        & "ON  MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                                                " & vbNewLine _
                                                        & "AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                                                " & vbNewLine _
                                                        & "AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                                                  " & vbNewLine _
                                                        & "AND MDOUTU.SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                                                        & "--運送会社M                                                                                          " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                                                    " & vbNewLine _
                                                        & "ON  MUCO.NRS_BR_CD = UL.NRS_BR_CD                                                                    " & vbNewLine _
                                                        & "AND MUCO.UNSOCO_CD = UL.UNSO_CD                                                                      " & vbNewLine _
                                                        & "AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                                                " & vbNewLine _
                                                        & "AND MUCO.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                                                        & "--倉庫M                                                                                              " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_SOKO MSO                                                                       " & vbNewLine _
                                                        & "ON  MSO.WH_CD = OUTL.WH_CD                                                                           " & vbNewLine _
                                                        & "AND MSO.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                        & "--ユーザM                                                                                            " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..S_USER MUSER                                                                     " & vbNewLine _
                                                        & "ON MUSER.USER_CD = OUTM.SYS_UPD_USER                                                                 " & vbNewLine _
                                                        & "AND MUSER.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
                                                        & "--区分M(納入予定区分)                                                                                " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                       " & vbNewLine _
                                                        & "ON  KBN1.KBN_GROUP_CD = 'N010'                                                                       " & vbNewLine _
                                                        & "AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                                                 " & vbNewLine _
                                                        & "AND KBN1.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                                                        & "--区分M(元着払区分)                                                                                  " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                                       " & vbNewLine _
                                                        & "ON  KBN2.KBN_GROUP_CD = 'M001'                                                                       " & vbNewLine _
                                                        & "AND KBN2.KBN_CD = OUTL.PC_KB                                                                         " & vbNewLine _
                                                        & "AND KBN2.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                                                        & "--区分M(商品状態区分(中身))                                                                          " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                                       " & vbNewLine _
                                                        & "ON  KBN3.KBN_GROUP_CD = 'S005'                                                                       " & vbNewLine _
                                                        & "AND KBN3.KBN_CD = INS.GOODS_COND_KB_1                                                                " & vbNewLine _
                                                        & "AND KBN3.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                                                        & "--区分M(商品状態区分(外観))                                                                          " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                                                       " & vbNewLine _
                                                        & "ON  KBN4.KBN_GROUP_CD = 'S006'                                                                       " & vbNewLine _
                                                        & "AND KBN4.KBN_CD = INS.GOODS_COND_KB_2                                                                " & vbNewLine _
                                                        & "AND KBN4.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                                                        & "--出荷Lでの荷主帳票パターン取得                                                                      " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                  " & vbNewLine _
                                                        & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                  " & vbNewLine _
                                                        & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                                  " & vbNewLine _
                                                        & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                                  " & vbNewLine _
                                                        & "AND '00' = MCR1.CUST_CD_S                                                                            " & vbNewLine _
                                                        & "AND MCR1.PTN_ID = @RPT_FLG                                                                           " & vbNewLine _
                                                        & "--帳票パターン取得                                                                                   " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                        " & vbNewLine _
                                                        & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                   " & vbNewLine _
                                                        & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                         " & vbNewLine _
                                                        & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                         " & vbNewLine _
                                                        & "AND MR1.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                        & "--商品Mの荷主での荷主帳票パターン取得                                                                " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                  " & vbNewLine _
                                                        & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                    " & vbNewLine _
                                                        & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                    " & vbNewLine _
                                                        & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                    " & vbNewLine _
                                                        & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                    " & vbNewLine _
                                                        & "AND MCR2.PTN_ID = @RPT_FLG                                                                           " & vbNewLine _
                                                        & "--帳票パターン取得                                                                                   " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                        " & vbNewLine _
                                                        & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                   " & vbNewLine _
                                                        & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                         " & vbNewLine _
                                                        & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                         " & vbNewLine _
                                                        & "AND MR2.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                        & "--在庫データ(D_ZAI_TRS)取得                                                                          " & vbNewLine _
                                                        & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                                    " & vbNewLine _
                                                        & "ON  ZAI.NRS_BR_CD  = OUTS.NRS_BR_CD                                                                  " & vbNewLine _
                                                        & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                                                 " & vbNewLine _
                                                        & "AND ZAI.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                        & "--存在しない場合の帳票パターン取得                                                                   " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                        " & vbNewLine _
                                                        & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                   " & vbNewLine _
                                                        & "AND MR3.PTN_ID = @RPT_FLG                                                                            " & vbNewLine _
                                                        & "AND MR3.STANDARD_FLAG = '01'                                                                         " & vbNewLine _
                                                        & "AND MR3.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                        & "--#####################################                                                              " & vbNewLine _
                                                        & "--棟室M                                                                                              " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_TOU_SITU TOSI                                                                  " & vbNewLine _
                                                        & "ON  TOSI.WH_CD = OUTL.WH_CD                                                                          " & vbNewLine _
                                                        & "AND TOSI.TOU_NO = OUTS.TOU_NO                                                                        " & vbNewLine _
                                                        & "AND TOSI.SITU_NO = OUTS.SITU_NO                                                                      " & vbNewLine _
                                                        & "--営業所M                                                                                            " & vbNewLine _
                                                        & "LEFT JOIN $LM_MST$..M_NRS_BR NRSV                                                                    " & vbNewLine _
                                                        & "ON NRSV.NRS_BR_CD   = OUTL.NRS_BR_CD                                                                 " & vbNewLine _
                                                        & "AND NRSV.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                                                        & "--#####################################                                                              " & vbNewLine

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ出荷取消検索用SQL WHERE部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DIC_CSV_DEL_WHERE As String = "WHERE                                                                                               " & vbNewLine _
                                                         & "1 = 1                                                                                               " & vbNewLine _
                                                         & " AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                                    " & vbNewLine _
                                                         & " AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                                  " & vbNewLine

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成データ出荷取消検索用SQL ORDER_BY部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DIC_CSV_DEL_ORDERBY As String = "ORDER BY                                                                                          " & vbNewLine _
                                                           & "     OUTL.NRS_BR_CD                                                                               " & vbNewLine _
                                                           & "    ,OUTL.OUTKA_NO_L                                                                              " & vbNewLine _
                                                           & "    ,OUTM.OUTKA_NO_M                                                                              " & vbNewLine _
                                                           & "    ,OUTS.OUTKA_NO_S                                                                              " & vbNewLine
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

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>日立物流出荷音声データCSV作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectCSV(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV)
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV_FROM)
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV_WHERE)
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV_GROUPBY)
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV_ORDERBY)

        'パラメータ設定
        Call setParameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC830DAC", "SelectCSV", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("COMPNAME", "COMPNAME")
        map.Add("RPT_ID", "RPT_ID")
        map.Add("TOU_HD", "TOU_HD")
        map.Add("HAN_HD", "HAN_HD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("EDI_SET_NO", "EDI_SET_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKA_PLAN_TIME", "OUTKA_PLAN_TIME")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKO_TIME", "OUTKO_TIME")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("AD_4", "AD_4")
        map.Add("AD_5", "AD_5")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("NRS_BR_PIC_NM", "NRS_BR_PIC_NM")
        map.Add("URIG_NM", "URIG_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("KANA_NM", "KANA_NM")
        map.Add("DEST_AD_5", "DEST_AD_5")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("S_TOU_NO", "S_TOU_NO")
        map.Add("S_SITU_NO", "S_SITU_NO")
        map.Add("S_ZONE_CD", "S_ZONE_CD")
        map.Add("S_NM", "S_NM")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("PC_KB", "PC_KB")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("SUM_ALCTD_NB", "SUM_ALCTD_NB")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_HASU", "OUTKA_HASU")
        map.Add("OUTKA_PKG_NB_S", "OUTKA_PKG_NB_S")
        map.Add("OUTKA_HASU_S", "OUTKA_HASU_S")
        map.Add("OUTKA_QT", "OUTKA_QT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("NB_UT", "NB_UT")
        map.Add("QT_UT", "QT_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
        map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
        map.Add("SAGYO_MEI_RYAK_NM", "SAGYO_MEI_RYAK_NM")
        map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
        map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
        map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
        map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
        map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
        map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
        map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
        map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
        map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
        map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
        map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
        map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
        map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
        map.Add("RPT_FLG", "RPT_FLG")
        map.Add("HAN_DTL", "HAN_DTL")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("REMARK_ZAI", "REMARK_ZAI")
        map.Add("ZAN_KONSU", "ZAN_KONSU")
        map.Add("ZAN_HASU", "ZAN_HASU")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("PKG_NB_Z_ALL", "PKG_NB_Z_ALL")
        map.Add("HASU_Z_ALL", "HASU_Z_ALL")
        map.Add("PKG_NB_S", "PKG_NB_S")
        map.Add("HASU_S", "HASU_S")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("KYORI", "KYORI")
        map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
        map.Add("NHS_REMARK", "NHS_REMARK")
        map.Add("REMARK_UNSO", "REMARK_UNSO")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC830OUT_CSV")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成対象出荷取消検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>日立物流出荷音声データCSV作成対象データ出荷取消検索件数取得SQLの構築・発行</remarks>
    Private Function SelectDelCSV(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim max As Integer = ds.Tables("LMC830IN").Rows.Count - 1
        Dim OutNoM As String = String.Empty
        'SQL作成
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV_DEL)
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV_DEL_FROM)
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV_DEL_WHERE)
        Me._StrSql.Append(" AND OUTM.OUTKA_NO_M IN (")
        For i As Integer = 0 To max
            OutNoM = ds.Tables("LMC830IN").Rows(i).Item("OUTKA_NO_M").ToString()
            Me._StrSql.Append("'" & OutNoM & "'")
            If i = max Then
                Me._StrSql.Append(")" & vbNewLine)
            Else
                Me._StrSql.Append(",")
            End If
        Next
        Me._StrSql.Append(LMC830DAC.SQL_SELECT_DIC_CSV_DEL_ORDERBY)

        'パラメータ設定
        Call setParameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC830DAC", "SelectCSV", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("COMPNAME", "COMPNAME")
        map.Add("RPT_ID", "RPT_ID")
        map.Add("TOU_HD", "TOU_HD")
        map.Add("HAN_HD", "HAN_HD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("EDI_SET_NO", "EDI_SET_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKA_PLAN_TIME", "OUTKA_PLAN_TIME")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKO_TIME", "OUTKO_TIME")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("AD_4", "AD_4")
        map.Add("AD_5", "AD_5")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("NRS_BR_PIC_NM", "NRS_BR_PIC_NM")
        map.Add("URIG_NM", "URIG_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("KANA_NM", "KANA_NM")
        map.Add("DEST_AD_5", "DEST_AD_5")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("S_TOU_NO", "S_TOU_NO")
        map.Add("S_SITU_NO", "S_SITU_NO")
        map.Add("S_ZONE_CD", "S_ZONE_CD")
        map.Add("S_NM", "S_NM")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("PC_KB", "PC_KB")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("OUTKA_KNP_NB", "OUTKA_KNP_NB")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_HASU", "OUTKA_HASU")
        map.Add("OUTKA_PKG_NB_S", "OUTKA_PKG_NB_S")
        map.Add("OUTKA_HASU_S", "OUTKA_HASU_S")
        map.Add("OUTKA_QT", "OUTKA_QT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("BACKLOG_NB", "BACKLOG_NB")
        map.Add("BACKLOG_QT", "BACKLOG_QT")
        map.Add("NB_UT", "NB_UT")
        map.Add("QT_UT", "QT_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
        map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
        map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
        map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
        map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
        map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
        map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
        map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
        map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
        map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
        map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
        map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
        map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
        map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
        map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
        map.Add("RPT_FLG", "RPT_FLG")
        map.Add("HAN_DTL", "HAN_DTL")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("REMARK_ZAI", "REMARK_ZAI")
        map.Add("PKG_NB_Z", "PKG_NB_Z")
        map.Add("HASU_Z", "HASU_Z")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("PKG_NB_Z_ALL", "PKG_NB_Z_ALL")
        map.Add("HASU_Z_ALL", "HASU_Z_ALL")
        map.Add("PKG_NB_S", "PKG_NB_S")
        map.Add("HASU_S", "HASU_S")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("UNSO_KYORI", "UNSO_KYORI")
        map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
        map.Add("NHS_REMARK", "NHS_REMARK")
        map.Add("REMARK_UNSO", "REMARK_UNSO")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC830OUT_CSV_DEL")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "設定処理"

#Region "SQL"

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

    ''' <summary>
    '''  パラメータ設定モジュール
    ''' </summary>
    ''' <remarks>検索用SQLの構築</remarks>
    Private Sub setParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row("WH_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_HAN_FLG", Me._Row("TOU_HAN_FLG"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMPNAME", Me._Row("COMPNAME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_FLG", Me._Row("RPT_FLG"), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
