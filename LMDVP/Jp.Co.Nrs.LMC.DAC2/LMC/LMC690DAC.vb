' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 運送サブシステム
'  プログラムID     :  LMC690DAC : 纏めピッキングリスト
'  作  成  者       :  YAMANAKA
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC690DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC690DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                   " & vbNewLine _
                                            & "	  OUTKA_L.NRS_BR_CD                                 AS NRS_BR_CD  " & vbNewLine _
                                            & " , '15'                                              AS PTN_ID     " & vbNewLine _
                                            & " , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                " & vbNewLine _
                                            & "	 	   WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                " & vbNewLine _
                                            & "	   	   ELSE MR3.PTN_CD END                          AS PTN_CD     " & vbNewLine _
                                            & " , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                            & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                            & "		   ELSE MR3.RPT_ID END                          AS RPT_ID     " & vbNewLine


#End Region

#Region "SELECT句"

    Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
                                            & "	   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID           " & vbNewLine _
                                            & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           " & vbNewLine _
                                            & "         ELSE MR3.RPT_ID END               AS RPT_ID           " & vbNewLine _
                                            & "  , OUTKA_L.NRS_BR_CD                      AS NRS_BR_CD        " & vbNewLine _
                                            & "  , OUTKA_S.TOU_NO                         AS TOU_NO           " & vbNewLine _
                                            & "  , CUST.CUST_NM_L                         AS CUST_NM_L        " & vbNewLine _
                                            & "  , UNSOCO.UNSOCO_NM                       AS UNSOCO_NM        " & vbNewLine _
                                            & "  , OUTKA_L.ARR_PLAN_DATE                  AS ARR_PLAN_DATE    " & vbNewLine _
                                            & "  , NRS_BR.NRS_BR_NM                       AS NRS_BR_NM        " & vbNewLine _
                                            & "  , OUTKA_L.OUTKA_NO_L                     AS OUTKA_NO_L       " & vbNewLine _
                                            & "--  , INKA_L.INKA_DATE                       AS INKA_DATE      " & vbNewLine _
                                            & "  , ZAI_TRS.INKO_DATE                      AS INKO_DATE        " & vbNewLine _
                                            & "  , GOODS.GOODS_CD_CUST                    AS GOODS_CD_CUST    " & vbNewLine _
                                            & "  , GOODS.GOODS_NM_1                       AS GOODS_NM_1       " & vbNewLine _
                                            & "  , OUTKA_S.LOT_NO                         AS LOT_NO           " & vbNewLine _
                                            & "  , OUTKA_S.IRIME                          AS IRIME            " & vbNewLine _
                                            & "  , OUTKA_S.REMARK                         AS REMARK           " & vbNewLine _
                                            & "  , SUM(OUTKA_S.ALCTD_NB)/GOODS.PKG_NB     AS ALCTD_NB         " & vbNewLine _
                                            & "  , GOODS.NB_UT                            AS NB_UT            " & vbNewLine _
                                            & "  , OUTKA_S.SITU_NO                        AS SITU_NO          " & vbNewLine _
                                            & "  , OUTKA_S.ZONE_CD                        AS ZONE_CD          " & vbNewLine _
                                            & "  , OUTKA_S.LOCA                           AS LOCA             " & vbNewLine _
                                            & "  , MIN(OUTKA_S.ALCTD_CAN_NB)/GOODS.PKG_NB AS ZAN_KOSU         " & vbNewLine _
                                            & "  , RIGHT('000000000' + CONVERT(VARCHAR,CONVERT(NUMERIC(10),MIN(OUTKA_S.ALCTD_CAN_NB)/GOODS.PKG_NB)),10) AS ZAN_KOSU_SORT    " & vbNewLine _
                                            & "  , MIN(OUTKA_S.ALCTD_CAN_NB)%GOODS.PKG_NB AS ZAN_HASU         " & vbNewLine _
                                            & "  , DEST.DEST_NM                           AS DEST_NM          " & vbNewLine _
                                            & "  , DEST.AD_1                              AS AD_1             " & vbNewLine _
                                            & "  , OUTKA_L.CUST_ORD_NO                    AS CUST_ORD_NO      " & vbNewLine _
                                            & "  , OUTKA_M.BUYER_ORD_NO_DTL               AS BUYER_ORD_NO     " & vbNewLine _
                                            & "  , CUST.CUST_CD_L                         AS CUST_CD_L        " & vbNewLine _
                                            & " --(2012.08.17)一覧偶数行に背景色対応 --- START ---            " & vbNewLine _
                                            & "  , ROW_NUMBER() OVER(PARTITION BY                             " & vbNewLine _
                                            & "                                OUTKA_S.TOU_NO                 " & vbNewLine _
                                            & "                              , OUTKA_S.SITU_NO                " & vbNewLine _
                                            & "                              , UNSOCO.UNSOCO_NM               " & vbNewLine _
                                            & "                              , CUST.CUST_NM_L                 " & vbNewLine _
                                            & "                              , OUTKA_L.ARR_PLAN_DATE          " & vbNewLine _
                                            & "                          ORDER BY                             " & vbNewLine _
                                            & "                                OUTKA_L.NRS_BR_CD              " & vbNewLine _
                                            & "                              , OUTKA_S.TOU_NO                 " & vbNewLine _
                                            & "                              , OUTKA_S.SITU_NO                " & vbNewLine _
                                            & "                              , UNSOCO.UNSOCO_NM               " & vbNewLine _
                                            & "                              , OUTKA_S.ZONE_CD                " & vbNewLine _
                                            & "                              , OUTKA_S.LOCA                   " & vbNewLine _
                                            & "                              , GOODS.GOODS_CD_CUST            " & vbNewLine _
                                            & "                              , OUTKA_S.LOT_NO                 " & vbNewLine _
                                            & "                              , OUTKA_S.IRIME DESC             " & vbNewLine _
                                            & "                      )                    AS ROW_COUNT        " & vbNewLine _
                                            & " --(2012.08.17)一覧偶数行に背景色対応 ---  END  ---            " & vbNewLine _
                                            & " --(2012.09.11)要望番号1428対応 --- START ---                  " & vbNewLine _
                                            & "  , UNSOCO.UNSOCO_BR_NM                  AS UNSOCO_BR_NM       " & vbNewLine _
                                            & "  , UNSO_L.UNSO_CD                       AS UNSO_CD            " & vbNewLine _
                                            & "  , OUTKA_L.MATOME_PRINT_DATE            AS MATOME_PRINT_DATE  " & vbNewLine _
                                            & "  , OUTKA_L.MATOME_PRINT_TIME            AS MATOME_PRINT_TIME  " & vbNewLine _
                                            & " --(2012.09.11)要望番号1428対応 ---  END  ---                  " & vbNewLine _
                                            & " -- 2012.11.07 yamanaka 千葉_DIC対応 Start --                  " & vbNewLine _
                                            & "  , OUTKA_L.OUTKO_DATE                     AS OUTKO_DATE       " & vbNewLine _
                                            & "  , GOODS.CUST_COST_CD1                    AS CUST_COST_CD1    " & vbNewLine _
                                            & "  , OUTKA_S.ALCTD_QT                       AS ALCTD_QT         " & vbNewLine _
                                            & "  , OUTKA_M.IRIME_UT                       AS IRIME_UT         " & vbNewLine _
                                            & " -- 2012.11.07 yamanaka 千葉_DIC対応 End --                    " & vbNewLine _
                                            & " -- 2013.03.27 s.kobayashi 要望管理1972対応 Start --           " & vbNewLine _
                                            & "  , @USER_NM AS USER_NM                                        " & vbNewLine _
                                            & " -- 2013.03.27 s.kobayashi 要望管理1972対応 End --             " & vbNewLine _
                                            & " -- (2014.10.15)ナフコ対応 Start                               " & vbNewLine _
                                            & "  , ISNULL(MJIS.KEN,'') AS KEN_NM                              " & vbNewLine _
                                            & " -- (2014.10.15)ナフコ対応 E N D                               " & vbNewLine

    Private Const SQL_SELECT_DATA690_1 As String = " SELECT                                                   " & vbNewLine _
                                            & "	   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID           " & vbNewLine _
                                            & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           " & vbNewLine _
                                            & "         ELSE MR3.RPT_ID END               AS RPT_ID           " & vbNewLine _
                                            & "  , OUTKA_L.NRS_BR_CD                      AS NRS_BR_CD        " & vbNewLine _
                                            & "  , OUTKA_S.TOU_NO                         AS TOU_NO           " & vbNewLine _
                                            & "  , CUST.CUST_NM_L                         AS CUST_NM_L        " & vbNewLine _
                                            & "  , UNSOCO.UNSOCO_NM                       AS UNSOCO_NM        " & vbNewLine _
                                            & "  , OUTKA_L.ARR_PLAN_DATE                  AS ARR_PLAN_DATE    " & vbNewLine _
                                            & "  , NRS_BR.NRS_BR_NM                       AS NRS_BR_NM        " & vbNewLine _
                                            & "  , OUTKA_L.OUTKA_NO_L                     AS OUTKA_NO_L       " & vbNewLine _
                                            & "--  , INKA_L.INKA_DATE                       AS INKA_DATE      " & vbNewLine _
                                            & "  , ZAI_TRS.INKO_DATE                      AS INKO_DATE        " & vbNewLine _
                                            & "  , GOODS.GOODS_CD_CUST                    AS GOODS_CD_CUST    " & vbNewLine _
                                            & "  , GOODS.GOODS_NM_1                       AS GOODS_NM_1       " & vbNewLine _
                                            & "  , OUTKA_S.LOT_NO                         AS LOT_NO           " & vbNewLine _
                                            & "  , OUTKA_S.IRIME                          AS IRIME            " & vbNewLine _
                                            & "  , OUTKA_S.REMARK                         AS REMARK           " & vbNewLine _
                                            & "  , SUM(OUTKA_S.ALCTD_NB)/GOODS.PKG_NB     AS ALCTD_NB         " & vbNewLine _
                                            & "  , GOODS.NB_UT                            AS NB_UT            " & vbNewLine _
                                            & "  , OUTKA_S.SITU_NO                        AS SITU_NO          " & vbNewLine _
                                            & "  , OUTKA_S.ZONE_CD                        AS ZONE_CD          " & vbNewLine _
                                            & "  , OUTKA_S.LOCA                           AS LOCA             " & vbNewLine _
                                            & "  , MIN(OUTKA_S.ALCTD_CAN_NB)/GOODS.PKG_NB AS ZAN_KOSU         " & vbNewLine _
                                            & "  , RIGHT('000000000' + CONVERT(VARCHAR,CONVERT(NUMERIC(10),MIN(OUTKA_S.ALCTD_CAN_NB)/GOODS.PKG_NB)),10) AS ZAN_KOSU_SORT    " & vbNewLine _
                                            & "  , MIN(OUTKA_S.ALCTD_CAN_NB)%GOODS.PKG_NB AS ZAN_HASU         " & vbNewLine _
                                            & "  , DEST.DEST_NM                           AS DEST_NM          " & vbNewLine _
                                            & "  , CASE WHEN OUTKA_L.DEST_KB = '00' THEN DEST.AD_1            " & vbNewLine _
                                            & "         WHEN OUTKA_L.DEST_KB = '02' THEN EDIL.DEST_AD_1       " & vbNewLine _
                                            & "    ELSE OUTKA_L.DEST_AD_1 END             AS AD_1             " & vbNewLine _
                                            & "  , OUTKA_L.CUST_ORD_NO                    AS CUST_ORD_NO      " & vbNewLine _
                                            & "  , OUTKA_M.BUYER_ORD_NO_DTL               AS BUYER_ORD_NO     " & vbNewLine _
                                            & "  , CUST.CUST_CD_L                         AS CUST_CD_L        " & vbNewLine _
                                            & " --(2012.08.17)一覧偶数行に背景色対応 --- START ---            " & vbNewLine _
                                            & "  , ROW_NUMBER() OVER(PARTITION BY                             " & vbNewLine _
                                            & "                                OUTKA_S.TOU_NO                 " & vbNewLine _
                                            & "                              , OUTKA_S.SITU_NO                " & vbNewLine _
                                            & "                              , UNSOCO.UNSOCO_NM               " & vbNewLine _
                                            & "                              , CUST.CUST_NM_L                 " & vbNewLine _
                                            & "                              , OUTKA_L.ARR_PLAN_DATE          " & vbNewLine _
                                            & "                          ORDER BY                             " & vbNewLine _
                                            & "                                OUTKA_L.NRS_BR_CD              " & vbNewLine _
                                            & "                              , OUTKA_S.TOU_NO                 " & vbNewLine _
                                            & "                              , OUTKA_S.SITU_NO                " & vbNewLine _
                                            & "                              , UNSOCO.UNSOCO_NM               " & vbNewLine _
                                            & "                              , OUTKA_S.ZONE_CD                " & vbNewLine _
                                            & "                              , OUTKA_S.LOCA                   " & vbNewLine _
                                            & "                              , GOODS.GOODS_CD_CUST            " & vbNewLine _
                                            & "                              , OUTKA_S.LOT_NO                 " & vbNewLine _
                                            & "                              , OUTKA_S.IRIME DESC             " & vbNewLine _
                                            & "                      )                    AS ROW_COUNT        " & vbNewLine _
                                            & " --(2012.08.17)一覧偶数行に背景色対応 ---  END  ---            " & vbNewLine _
                                            & " --(2012.09.11)要望番号1428対応 --- START ---                  " & vbNewLine _
                                            & "  , UNSOCO.UNSOCO_BR_NM                  AS UNSOCO_BR_NM       " & vbNewLine _
                                            & "  , UNSO_L.UNSO_CD                       AS UNSO_CD            " & vbNewLine _
                                            & "  , OUTKA_L.MATOME_PRINT_DATE            AS MATOME_PRINT_DATE  " & vbNewLine _
                                            & "  , OUTKA_L.MATOME_PRINT_TIME            AS MATOME_PRINT_TIME  " & vbNewLine _
                                            & " --(2012.09.11)要望番号1428対応 ---  END  ---                  " & vbNewLine _
                                            & " -- 2012.11.07 yamanaka 千葉_DIC対応 Start --                  " & vbNewLine _
                                            & "  , OUTKA_L.OUTKO_DATE                     AS OUTKO_DATE       " & vbNewLine _
                                            & "  , GOODS.CUST_COST_CD1                    AS CUST_COST_CD1    " & vbNewLine _
                                            & "  , OUTKA_S.ALCTD_QT                       AS ALCTD_QT         " & vbNewLine _
                                            & "  , OUTKA_M.IRIME_UT                       AS IRIME_UT         " & vbNewLine _
                                            & " -- 2012.11.07 yamanaka 千葉_DIC対応 End --                    " & vbNewLine _
                                            & " -- 2013.03.27 s.kobayashi 要望管理1972対応 Start --           " & vbNewLine _
                                            & "  , @USER_NM AS USER_NM                                        " & vbNewLine _
                                            & " -- 2013.03.27 s.kobayashi 要望管理1972対応 End --             " & vbNewLine _
                                            & " -- (2014.10.15)ナフコ対応 Start                               " & vbNewLine _
                                            & "  , ISNULL(MJIS.KEN,'') AS KEN_NM                              " & vbNewLine _
                                            & " -- (2014.10.15)ナフコ対応 E N D                               " & vbNewLine

    Private Const SQL_SELECT_DATA691 As String = "  ----ADD 20220418 027736 LMC691 のため追加   LMC691のため   " & vbNewLine _
                                            & "  , OUTKA_S.SERIAL_NO                      AS SERIAL_NO 　    " & vbNewLine

    Private Const SQL_SELECT_DATA690 As String = "  --20160812  要望番号2610                                     " & vbNewLine _
                                            & "  , GOODS.OUTKA_ATT AS OUTKA_ATT                              " & vbNewLine _
                                            & "  , OUTKA_M.REMARK AS REMARK_M                              " & vbNewLine _
                                            & "  --20160812  要望番号2610                                     " & vbNewLine _
                                            & "  --ADD Start 20191121 20160812  要望番号 007676              " & vbNewLine _
                                            & "  ,DATENAME(WEEKDAY,OUTKA_L.ARR_PLAN_DATE)   AS ARR_PLAN_DATE_DATENAME  " & vbNewLine _
                                            & "  ,DATENAME(WEEKDAY,OUTKA_L.OUTKO_DATE)      AS OUTKO_DATE_DATENAME     " & vbNewLine _
                                            & "  ----ADD End   20191121 20160812  要望番号 007676            " & vbNewLine

    Private Const SQL_SELECT_DATA693 As String = " , TOPEDI.EDI_CTL_NO                    AS EDI_CTL_NO       " & vbNewLine _
                                               & " , KBN.KBN_NM1                          AS KBN_NM1          " & vbNewLine _
                                               & " , OUTKA_S.SERIAL_NO                    AS SERIAL_NO        " & vbNewLine _
                                               & " , GOODS.PKG_NB                         AS PKG_NB           " & vbNewLine _
                                               & " , SUM(OUTKA_S.ALCTD_NB)%GOODS.PKG_NB   AS HASU             " & vbNewLine

    Private Const SQL_SELECT_DATA697 As String = " , OUTKA_L.OUTKA_PLAN_DATE              AS OUTKA_PLAN_DATE  " & vbNewLine _
                                               & " , OUTKA_L.REMARK                       AS REMARK_OUT       " & vbNewLine _
                                               & " , MCD.SET_NAIYO                        AS PIC_FLG          " & vbNewLine _
                                               & " , @ROW_COUNT                           AS ROW_CHECK_COUNT  " & vbNewLine _
                                               & " , ISNULL(ZAI_TRS2.ALLOC_CAN_NB,0)                 AS ALLOC_CAN_NB     " & vbNewLine _
                                               & " , GOODS.PKG_NB                         AS PKG_NB           " & vbNewLine _
                                               & " -- 2013.09.17 Kurihara WIT対応 Start --                    " & vbNewLine _
                                               & "  , OUTKA_S.OUTKA_NO_M AS OUTKA_NO_M                        " & vbNewLine _
                                               & "  , OUTKA_S.OUTKA_NO_S AS OUTKA_NO_S                        " & vbNewLine _
                                               & "  , ZAI_TRS.GOODS_KANRI_NO AS GOODS_KANRI_NO                " & vbNewLine _
                                               & "  , GOODS_DETAILS.SET_NAIYO AS CHK_TANI                     " & vbNewLine _
                                               & "  , TOTAL_PIC_WK.MTM_PICK_NO AS MATOME_PICK_NO              " & vbNewLine _
                                               & "  , CASE                                                    " & vbNewLine _
                                               & "        WHEN C_TOTAL_PIC_WK.GOODS_KANRI_NO IS NULL THEN '0' " & vbNewLine _
                                               & "        ELSE '1'                                            " & vbNewLine _
                                               & "    END AS KENPIN_FLG                                       " & vbNewLine _
                                               & " -- 2013.11.28 Kasama WIT対応 Start --                      " & vbNewLine _
                                               & "  , GOODS.CUST_CD_M                                         " & vbNewLine _
                                               & "  , GOODS.CUST_CD_S                                         " & vbNewLine _
                                               & " -- 2015.12.14 要望番号2471対応                             " & vbNewLine _
                                               & "  , MCDTL.SET_NAIYO_2 AS OUTER_PKG_NM                       " & vbNewLine


    Private Const SQL_SELECT_DATA772 As String = " , GOODS.PKG_NB AS PKG_NB                                  " & vbNewLine _
                                            & "  ,ZAI_TRS.REMARK_OUT AS REMARK_OUT                           " & vbNewLine _
                                            & "  ,ISNULL( KBNS006.KBN_NM1 , '') AS JOTAI_GAISO               " & vbNewLine _
                                            & "  ,SUM(OUTKA_S.ALCTD_NB)%GOODS.PKG_NB AS HASU                 " & vbNewLine _
                                            & "  ,DEST.AD_2 AS AD_2               " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = " FROM                                                            " & vbNewLine _
                                          & "  $LM_TRN$..C_OUTKA_L OUTKA_L                                    " & vbNewLine _
                                          & "  --出荷Ｍ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKA_M                          " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD  = OUTKA_M.NRS_BR_CD               " & vbNewLine _
                                          & "        AND OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L              " & vbNewLine _
                                          & "        AND OUTKA_M.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "  --出荷Ｓ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKA_S                          " & vbNewLine _
                                          & "         ON OUTKA_M.NRS_BR_CD = OUTKA_S.NRS_BR_CD                " & vbNewLine _
                                          & "        AND OUTKA_M.OUTKA_NO_L = OUTKA_S.OUTKA_NO_L              " & vbNewLine _
                                          & "        AND OUTKA_M.OUTKA_NO_M = OUTKA_S.OUTKA_NO_M              " & vbNewLine _
                                          & "        AND OUTKA_S.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "  --入荷Ｌ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..B_INKA_L INKA_L                            " & vbNewLine _
                                          & "         ON OUTKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD                 " & vbNewLine _
                                          & "        AND OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L                 " & vbNewLine _
                                          & "        AND INKA_L.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                          & "  --埼玉BP・カストロール修正対応 yamanaka 2013.01.21 Start       " & vbNewLine _
                                          & "  --在庫                                                         " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI_TRS                          " & vbNewLine _
                                          & "         ON OUTKA_S.NRS_BR_CD = ZAI_TRS.NRS_BR_CD                " & vbNewLine _
                                          & "        AND OUTKA_S.ZAI_REC_NO = ZAI_TRS.ZAI_REC_NO              " & vbNewLine _
                                          & "        AND ZAI_TRS.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "  --埼玉BP・カストロール修正対応 yamanaka 2013.01.21 End         " & vbNewLine _
                                          & "  --運送Ｌ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..F_UNSO_L UNSO_L                            " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                 " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_L = UNSO_L.CUST_CD_L                 " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_M = UNSO_L.CUST_CD_M                 " & vbNewLine _
                                          & "        AND OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L             " & vbNewLine _
                                          & "        AND UNSO_L.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                          & "        AND UNSO_L.MOTO_DATA_KB = '20'                           " & vbNewLine _
                                          & "  --届先マスタ                                                   " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_DEST DEST                                " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD = DEST.NRS_BR_CD                   " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_L = DEST.CUST_CD_L                   " & vbNewLine _
                                          & "        AND OUTKA_L.DEST_CD = DEST.DEST_CD                       " & vbNewLine _
                                          & "  --運送会社マスタ                                               " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                            " & vbNewLine _
                                          & "         ON UNSO_L.NRS_BR_CD = UNSOCO.NRS_BR_CD                  " & vbNewLine _
                                          & "        AND UNSO_L.UNSO_CD = UNSOCO.UNSOCO_CD                    " & vbNewLine _
                                          & "        AND UNSO_L.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD              " & vbNewLine _
                                          & "  --商品マスタ                                                   " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_GOODS GOODS                            " & vbNewLine _
                                          & "         ON OUTKA_M.NRS_BR_CD = GOODS.NRS_BR_CD                  " & vbNewLine _
                                          & "        AND OUTKA_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS            " & vbNewLine _
                                          & "  --荷主マスタ                                                   " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST CUST                                " & vbNewLine _
                                          & "         ON GOODS.NRS_BR_CD = CUST.NRS_BR_CD                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_L = CUST.CUST_CD_L                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_M = CUST.CUST_CD_M                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_S = CUST.CUST_CD_S                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_SS = CUST.CUST_CD_SS                   " & vbNewLine _
                                          & "  --営業所マスタ                                                 " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_NRS_BR NRS_BR                            " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD = NRS_BR.NRS_BR_CD                 " & vbNewLine _
                                          & "  --出荷Ｌでの荷主帳票パターン取得                　             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                            " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD = MCR1.NRS_BR_CD                   " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_L = MCR1.CUST_CD_L                   " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_M = MCR1.CUST_CD_M                   " & vbNewLine _
                                          & "        AND MCR1.CUST_CD_S = '00'                                " & vbNewLine _
                                          & "        AND MCR1.PTN_ID    = '15'                                " & vbNewLine _
                                          & "  --帳票パターン取得                                             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_RPT MR1                                  " & vbNewLine _
                                          & "         ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                          & "        AND MR1.PTN_ID = MCR1.PTN_ID                             " & vbNewLine _
                                          & "        AND MR1.PTN_CD = MCR1.PTN_CD                             " & vbNewLine _
                                          & "        AND MR1.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "  --商品Ｍの荷主での荷主帳票パターン取得                         " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                            " & vbNewLine _
                                          & "         ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                     " & vbNewLine _
                                          & "        AND MCR2.PTN_ID  = '15'                                  " & vbNewLine _
                                          & "  --帳票パターン取得                                             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_RPT MR2                                  " & vbNewLine _
                                          & "         ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                       " & vbNewLine _
                                          & "        AND MR2.PTN_ID = MCR2.PTN_ID                             " & vbNewLine _
                                          & "        AND MR2.PTN_CD = MCR2.PTN_CD                             " & vbNewLine _
                                          & "        AND MR2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "  --存在しない場合の帳票パターン取得                             " & vbNewLine _
                                          & "  LEFT LOOP JOIN $LM_MST$..M_RPT MR3                             " & vbNewLine _
                                          & "         ON MR3.NRS_BR_CD = OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                          & "        AND MR3.PTN_ID = '15'                                    " & vbNewLine _
                                          & "        AND MR3.STANDARD_FLAG = '01'                             " & vbNewLine _
                                          & "        AND MR3.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "  --(2014.10.15)ナフコ対応 START                                 " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_JIS MJIS                                 " & vbNewLine _
                                          & "         ON MJIS.JIS_CD = DEST.JIS                               " & vbNewLine _
                                          & "        AND MJIS.SYS_DEL_FLG = '0'                               " & vbNewLine

    Private Const SQL_FROM_DATA690 As String = " FROM                                                         " & vbNewLine _
                                          & "  $LM_TRN$..C_OUTKA_L OUTKA_L                                    " & vbNewLine _
                                          & "  --出荷Ｍ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKA_M                          " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD  = OUTKA_M.NRS_BR_CD               " & vbNewLine _
                                          & "        AND OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L              " & vbNewLine _
                                          & "        AND OUTKA_M.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "  --出荷Ｓ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKA_S                          " & vbNewLine _
                                          & "         ON OUTKA_M.NRS_BR_CD = OUTKA_S.NRS_BR_CD                " & vbNewLine _
                                          & "        AND OUTKA_M.OUTKA_NO_L = OUTKA_S.OUTKA_NO_L              " & vbNewLine _
                                          & "        AND OUTKA_M.OUTKA_NO_M = OUTKA_S.OUTKA_NO_M              " & vbNewLine _
                                          & "        AND OUTKA_S.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "  LEFT JOIN (                                                    " & vbNewLine _
                                          & "             SELECT                                              " & vbNewLine _
                                          & "                    NRS_BR_CD                                    " & vbNewLine _
                                          & "                  , EDI_CTL_NO                                   " & vbNewLine _
                                          & "                  , OUTKA_CTL_NO                                 " & vbNewLine _
                                          & "              FROM (                                             " & vbNewLine _
                                          & "                     SELECT                                                            " & vbNewLine _
                                          & "                            EDIOUTL.NRS_BR_CD                                          " & vbNewLine _
                                          & "                          , EDIOUTL.EDI_CTL_NO                                         " & vbNewLine _
                                          & "                          , EDIOUTL.OUTKA_CTL_NO                                       " & vbNewLine _
                                          & "                          , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                 " & vbNewLine _
                                          & "                            ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD     " & vbNewLine _
                                          & "                                                               , EDIOUTL.OUTKA_CTL_NO  " & vbNewLine _
                                          & "                                                        ORDER BY EDIOUTL.NRS_BR_CD     " & vbNewLine _
                                          & "                                                               , EDIOUTL.EDI_CTL_NO    " & vbNewLine _
                                          & "                                                   )                                   " & vbNewLine _
                                          & "                            END AS IDX                                                 " & vbNewLine _
                                          & "                      FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL        " & vbNewLine _
                                          & "                     WHERE EDIOUTL.SYS_DEL_FLG  = '0'            " & vbNewLine _
                                          & "                       AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD     " & vbNewLine _
                                          & "                       AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L    " & vbNewLine _
                                          & "                   ) EBASE                                       " & vbNewLine _
                                          & "             WHERE EBASE.IDX = 1                                 " & vbNewLine _
                                          & "             ) TOPEDI                                            " & vbNewLine _
                                          & "         ON TOPEDI.NRS_BR_CD    = OUTKA_L.NRS_BR_CD              " & vbNewLine _
                                          & "        AND TOPEDI.OUTKA_CTL_NO = OUTKA_L.OUTKA_NO_L             " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                          " & vbNewLine _
                                          & "         ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                   " & vbNewLine _
                                          & "        AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                  " & vbNewLine _
                                          & "  --入荷Ｌ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..B_INKA_L INKA_L                            " & vbNewLine _
                                          & "         ON OUTKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD                 " & vbNewLine _
                                          & "        AND OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L                 " & vbNewLine _
                                          & "        AND INKA_L.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                          & "  --埼玉BP・カストロール修正対応 yamanaka 2013.01.21 Start       " & vbNewLine _
                                          & "  --在庫                                                         " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI_TRS                          " & vbNewLine _
                                          & "         ON OUTKA_S.NRS_BR_CD = ZAI_TRS.NRS_BR_CD                " & vbNewLine _
                                          & "        AND OUTKA_S.ZAI_REC_NO = ZAI_TRS.ZAI_REC_NO              " & vbNewLine _
                                          & "        AND ZAI_TRS.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "  --埼玉BP・カストロール修正対応 yamanaka 2013.01.21 End         " & vbNewLine _
                                          & "  --運送Ｌ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..F_UNSO_L UNSO_L                            " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                 " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_L = UNSO_L.CUST_CD_L                 " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_M = UNSO_L.CUST_CD_M                 " & vbNewLine _
                                          & "        AND OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L             " & vbNewLine _
                                          & "        AND UNSO_L.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                          & "        AND UNSO_L.MOTO_DATA_KB = '20'                           " & vbNewLine _
                                          & "  --届先マスタ                                                   " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_DEST DEST                                " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD = DEST.NRS_BR_CD                   " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_L = DEST.CUST_CD_L                   " & vbNewLine _
                                          & "        AND OUTKA_L.DEST_CD = DEST.DEST_CD                       " & vbNewLine _
                                          & "  --運送会社マスタ                                               " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                            " & vbNewLine _
                                          & "         ON UNSO_L.NRS_BR_CD = UNSOCO.NRS_BR_CD                  " & vbNewLine _
                                          & "        AND UNSO_L.UNSO_CD = UNSOCO.UNSOCO_CD                    " & vbNewLine _
                                          & "        AND UNSO_L.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD              " & vbNewLine _
                                          & "  --商品マスタ                                                   " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_GOODS GOODS                            " & vbNewLine _
                                          & "         ON OUTKA_M.NRS_BR_CD = GOODS.NRS_BR_CD                  " & vbNewLine _
                                          & "        AND OUTKA_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS            " & vbNewLine _
                                          & "  --荷主マスタ                                                   " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST CUST                                " & vbNewLine _
                                          & "         ON GOODS.NRS_BR_CD = CUST.NRS_BR_CD                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_L = CUST.CUST_CD_L                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_M = CUST.CUST_CD_M                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_S = CUST.CUST_CD_S                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_SS = CUST.CUST_CD_SS                   " & vbNewLine _
                                          & "  --営業所マスタ                                                 " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_NRS_BR NRS_BR                            " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD = NRS_BR.NRS_BR_CD                 " & vbNewLine _
                                          & "  --出荷Ｌでの荷主帳票パターン取得                　             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                            " & vbNewLine _
                                          & "         ON OUTKA_L.NRS_BR_CD = MCR1.NRS_BR_CD                   " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_L = MCR1.CUST_CD_L                   " & vbNewLine _
                                          & "        AND OUTKA_L.CUST_CD_M = MCR1.CUST_CD_M                   " & vbNewLine _
                                          & "        AND MCR1.CUST_CD_S = '00'                                " & vbNewLine _
                                          & "        AND MCR1.PTN_ID    = '15'                                " & vbNewLine _
                                          & "  --帳票パターン取得                                             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_RPT MR1                                  " & vbNewLine _
                                          & "         ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                          & "        AND MR1.PTN_ID = MCR1.PTN_ID                             " & vbNewLine _
                                          & "        AND MR1.PTN_CD = MCR1.PTN_CD                             " & vbNewLine _
                                          & "        AND MR1.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "  --商品Ｍの荷主での荷主帳票パターン取得                         " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                            " & vbNewLine _
                                          & "         ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                     " & vbNewLine _
                                          & "        AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                     " & vbNewLine _
                                          & "        AND MCR2.PTN_ID  = '15'                                  " & vbNewLine _
                                          & "  --帳票パターン取得                                             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_RPT MR2                                  " & vbNewLine _
                                          & "         ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                       " & vbNewLine _
                                          & "        AND MR2.PTN_ID = MCR2.PTN_ID                             " & vbNewLine _
                                          & "        AND MR2.PTN_CD = MCR2.PTN_CD                             " & vbNewLine _
                                          & "        AND MR2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "  --存在しない場合の帳票パターン取得                             " & vbNewLine _
                                          & "  LEFT LOOP JOIN $LM_MST$..M_RPT MR3                             " & vbNewLine _
                                          & "         ON MR3.NRS_BR_CD = OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                          & "        AND MR3.PTN_ID = '15'                                    " & vbNewLine _
                                          & "        AND MR3.STANDARD_FLAG = '01'                             " & vbNewLine _
                                          & "        AND MR3.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "  --(2014.10.15)ナフコ対応 START                                 " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_JIS MJIS                                 " & vbNewLine _
                                          & "         ON MJIS.JIS_CD = DEST.JIS                               " & vbNewLine _
                                          & "        AND MJIS.SYS_DEL_FLG = '0'                               " & vbNewLine

    Private Const SQL_FROM_DATA693 As String = " FROM                                                                                     " & vbNewLine _
                                             & "  $LM_TRN$..C_OUTKA_L OUTKA_L                                                             " & vbNewLine _
                                             & "  --出荷Ｍ                                                                                " & vbNewLine _
                                             & "  LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKA_M                                                   " & vbNewLine _
                                             & "         ON OUTKA_L.NRS_BR_CD  = OUTKA_M.NRS_BR_CD                                        " & vbNewLine _
                                             & "        AND OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                                       " & vbNewLine _
                                             & "        AND OUTKA_M.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                             & "  --出荷Ｓ                                                                                " & vbNewLine _
                                             & "  LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKA_S                                                   " & vbNewLine _
                                             & "         ON OUTKA_M.NRS_BR_CD = OUTKA_S.NRS_BR_CD                                         " & vbNewLine _
                                             & "        AND OUTKA_M.OUTKA_NO_L = OUTKA_S.OUTKA_NO_L                                       " & vbNewLine _
                                             & "        AND OUTKA_M.OUTKA_NO_M = OUTKA_S.OUTKA_NO_M                                       " & vbNewLine _
                                             & "        AND OUTKA_S.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                             & "  --出荷ＥＤＩ                                                                            " & vbNewLine _
                                             & " LEFT JOIN (                                                                              " & vbNewLine _
                                             & "            SELECT                                                                        " & vbNewLine _
                                             & "                   NRS_BR_CD                                                              " & vbNewLine _
                                             & "                 , EDI_CTL_NO                                                             " & vbNewLine _
                                             & "                 , OUTKA_CTL_NO                                                           " & vbNewLine _
                                             & "             FROM (                                                                       " & vbNewLine _
                                             & "                    SELECT                                                                " & vbNewLine _
                                             & "                           EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
                                             & "                         , EDIOUTL.EDI_CTL_NO                                             " & vbNewLine _
                                             & "                         , EDIOUTL.OUTKA_CTL_NO                                           " & vbNewLine _
                                             & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                     " & vbNewLine _
                                             & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
                                             & "                                                              , EDIOUTL.OUTKA_CTL_NO      " & vbNewLine _
                                             & "                                                       ORDER BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
                                             & "                                                              , EDIOUTL.EDI_CTL_NO        " & vbNewLine _
                                             & "                                                  )                                       " & vbNewLine _
                                             & "                           END AS IDX                                                     " & vbNewLine _
                                             & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                  " & vbNewLine _
                                             & "                          LEFT JOIN $LM_TRN$..C_OUTKA_L C_OUTL                            " & vbNewLine _
                                             & "                                 ON C_OUTL.NRS_BR_CD   = EDIOUTL.NRS_BR_CD                " & vbNewLine _
                                             & "                                AND C_OUTL.OUTKA_NO_L  = EDIOUTL.OUTKA_CTL_NO             " & vbNewLine _
                                             & "                                AND C_OUTL.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                             & "                     WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                             & "                       AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                              " & vbNewLine _
                                             & "	                   AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                             " & vbNewLine _
                                             & "                  ) EBASE                                                                 " & vbNewLine _
                                             & "            WHERE EBASE.IDX = 1                                                           " & vbNewLine _
                                             & "            ) TOPEDI                                                                      " & vbNewLine _
                                             & "        ON TOPEDI.NRS_BR_CD    = OUTKA_L.NRS_BR_CD                                        " & vbNewLine _
                                             & "       AND TOPEDI.OUTKA_CTL_NO = OUTKA_L.OUTKA_NO_L                                       " & vbNewLine _
                                             & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                    " & vbNewLine _
                                             & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                             " & vbNewLine _
                                             & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                            " & vbNewLine _
                                             & "  --入荷Ｌ                                                                                " & vbNewLine _
                                             & "  LEFT JOIN $LM_TRN$..B_INKA_L INKA_L                                                     " & vbNewLine _
                                             & "         ON OUTKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD                                          " & vbNewLine _
                                             & "        AND OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L                                          " & vbNewLine _
                                             & "        AND INKA_L.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                             & "  --在庫                                                                                  " & vbNewLine _
                                             & "  LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI_TRS                                                   " & vbNewLine _
                                             & "         ON OUTKA_S.NRS_BR_CD = ZAI_TRS.NRS_BR_CD                                         " & vbNewLine _
                                             & "        AND OUTKA_S.ZAI_REC_NO = ZAI_TRS.ZAI_REC_NO                                       " & vbNewLine _
                                             & "        AND ZAI_TRS.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                             & "  --運送Ｌ                                                                                " & vbNewLine _
                                             & "  LEFT JOIN $LM_TRN$..F_UNSO_L UNSO_L                                                     " & vbNewLine _
                                             & "         ON OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                                          " & vbNewLine _
                                             & "        AND OUTKA_L.CUST_CD_L = UNSO_L.CUST_CD_L                                          " & vbNewLine _
                                             & "        AND OUTKA_L.CUST_CD_M = UNSO_L.CUST_CD_M                                          " & vbNewLine _
                                             & "        AND OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                                      " & vbNewLine _
                                             & "        AND UNSO_L.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                             & "        AND UNSO_L.MOTO_DATA_KB = '20'                                                    " & vbNewLine _
                                             & "  --届先マスタ                                                                            " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_DEST DEST                                                         " & vbNewLine _
                                             & "         ON OUTKA_L.NRS_BR_CD = DEST.NRS_BR_CD                                            " & vbNewLine _
                                             & "        AND OUTKA_L.CUST_CD_L = DEST.CUST_CD_L                                            " & vbNewLine _
                                             & "        AND OUTKA_L.DEST_CD = DEST.DEST_CD                                                " & vbNewLine _
                                             & "  --運送会社マスタ                                                                        " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                                                     " & vbNewLine _
                                             & "         ON UNSO_L.NRS_BR_CD = UNSOCO.NRS_BR_CD                                           " & vbNewLine _
                                             & "        AND UNSO_L.UNSO_CD = UNSOCO.UNSOCO_CD                                             " & vbNewLine _
                                             & "        AND UNSO_L.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                       " & vbNewLine _
                                             & "  --商品マスタ                                                                            " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_GOODS GOODS                                                     " & vbNewLine _
                                             & "         ON OUTKA_M.NRS_BR_CD = GOODS.NRS_BR_CD                                           " & vbNewLine _
                                             & "        AND OUTKA_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                     " & vbNewLine _
                                             & "  --荷主マスタ                                                                            " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_CUST CUST                                                         " & vbNewLine _
                                             & "         ON GOODS.NRS_BR_CD = CUST.NRS_BR_CD                                              " & vbNewLine _
                                             & "        AND GOODS.CUST_CD_L = CUST.CUST_CD_L                                              " & vbNewLine _
                                             & "        AND GOODS.CUST_CD_M = CUST.CUST_CD_M                                              " & vbNewLine _
                                             & "        AND GOODS.CUST_CD_S = CUST.CUST_CD_S                                              " & vbNewLine _
                                             & "        AND GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                            " & vbNewLine _
                                             & "  --区分マスタ                                                                            " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..Z_KBN KBN                                                           " & vbNewLine _
                                             & "         ON KBN.KBN_GROUP_CD = 'U001'                                                     " & vbNewLine _
                                             & "        AND UNSO_L.BIN_KB = KBN.KBN_CD                                                    " & vbNewLine _
                                             & "  --営業所マスタ                                                                          " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_NRS_BR NRS_BR                                                     " & vbNewLine _
                                             & "         ON OUTKA_L.NRS_BR_CD = NRS_BR.NRS_BR_CD                                          " & vbNewLine _
                                             & "  --出荷Ｌでの荷主帳票パターン取得                　                                      " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                     " & vbNewLine _
                                             & "         ON OUTKA_L.NRS_BR_CD = MCR1.NRS_BR_CD                                            " & vbNewLine _
                                             & "        AND OUTKA_L.CUST_CD_L = MCR1.CUST_CD_L                                            " & vbNewLine _
                                             & "        AND OUTKA_L.CUST_CD_M = MCR1.CUST_CD_M                                            " & vbNewLine _
                                             & "        AND MCR1.CUST_CD_S = '00'                                                         " & vbNewLine _
                                             & "        AND MCR1.PTN_ID    = '15'                                                         " & vbNewLine _
                                             & "  --帳票パターン取得                                                                      " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_RPT MR1                                                           " & vbNewLine _
                                             & "         ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                " & vbNewLine _
                                             & "        AND MR1.PTN_ID = MCR1.PTN_ID                                                      " & vbNewLine _
                                             & "        AND MR1.PTN_CD = MCR1.PTN_CD                                                      " & vbNewLine _
                                             & "        AND MR1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                             & "  --商品Ｍの荷主での荷主帳票パターン取得                                                  " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                     " & vbNewLine _
                                             & "         ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                              " & vbNewLine _
                                             & "        AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                                              " & vbNewLine _
                                             & "        AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                                              " & vbNewLine _
                                             & "        AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                                              " & vbNewLine _
                                             & "        AND MCR2.PTN_ID  = '15'                                                           " & vbNewLine _
                                             & "  --帳票パターン取得                                                                      " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_RPT MR2                                                           " & vbNewLine _
                                             & "         ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                " & vbNewLine _
                                             & "        AND MR2.PTN_ID = MCR2.PTN_ID                                                      " & vbNewLine _
                                             & "        AND MR2.PTN_CD = MCR2.PTN_CD                                                      " & vbNewLine _
                                             & "        AND MR2.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                             & "  --存在しない場合の帳票パターン取得                                                      " & vbNewLine _
                                             & "  LEFT LOOP JOIN $LM_MST$..M_RPT MR3                                                      " & vbNewLine _
                                             & "         ON MR3.NRS_BR_CD = OUTKA_L.NRS_BR_CD                                             " & vbNewLine _
                                             & "        AND MR3.PTN_ID = '15'                                                             " & vbNewLine _
                                             & "        AND MR3.STANDARD_FLAG = '01'                                                      " & vbNewLine _
                                             & "        AND MR3.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                             & "  --(2014.10.15)ナフコ対応 START                                                          " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_JIS MJIS                                                          " & vbNewLine _
                                             & "         ON MJIS.JIS_CD = DEST.JIS                                                        " & vbNewLine _
                                             & "        AND MJIS.SYS_DEL_FLG = '0'                                                        " & vbNewLine

    Private Const SQL_FROM_DATA697 As String = "LEFT JOIN (                                                                                                        " & vbNewLine _
                                             & "           SELECT                                                                                                  " & vbNewLine _
                                             & "             NRS_BR_CD   AS NRS_BR_CD                                                                               " & vbNewLine _
                                             & "           , CUST_CD     AS CUST_CD                                                                                 " & vbNewLine _
                                             & "           , CUST_CLASS  AS CUST_CLASS                                                                             " & vbNewLine _
                                             & "           , SET_NAIYO   AS SET_NAIYO                                                                              " & vbNewLine _
                                             & "           , SET_NAIYO_2 AS SET_NAIYO_2                                                                            " & vbNewLine _
                                             & "           , SET_NAIYO_3 AS SET_NAIYO_3                                                                            " & vbNewLine _
                                             & "           FROM $LM_MST$..M_CUST_DETAILS                                                                           " & vbNewLine _
                                             & "           WHERE                                                                                                   " & vbNewLine _
                                             & "               SUB_KB ='51'                                                                                        " & vbNewLine _
                                             & "           AND SYS_DEL_FLG ='0'                                                                                    " & vbNewLine _
                                             & "          ) MCD                                                                                                    " & vbNewLine _
                                             & "      ON CUST.NRS_BR_CD = MCD.NRS_BR_CD                                                                            " & vbNewLine _
                                             & "     AND (CASE WHEN MCD.CUST_CLASS='00' THEN  CUST.CUST_CD_L                                                       " & vbNewLine _
                                             & "               WHEN MCD.CUST_CLASS='01' THEN (CUST.CUST_CD_L + CUST.CUST_CD_M)                                     " & vbNewLine _
                                             & "               WHEN MCD.CUST_CLASS='02' THEN (CUST.CUST_CD_L + CUST.CUST_CD_M + CUST.CUST_CD_S)                    " & vbNewLine _
                                             & "               WHEN MCD.CUST_CLASS='03' THEN (CUST.CUST_CD_L + CUST.CUST_CD_M + CUST.CUST_CD_S + CUST.CUST_CD_SS)  " & vbNewLine _
                                             & "          END                                                                                                      " & vbNewLine _
                                             & "         ) = MCD.CUST_CD                                                                                           " & vbNewLine _
                                             & "  --在庫                                                                                                           " & vbNewLine _
                                             & "  LEFT JOIN (SELECT                                                                                                " & vbNewLine _
                                             & "               NRS_BR_CD,GOODS_CD_NRS,LOT_NO,IRIME,TOU_NO,SITU_NO,ZONE_CD,LOCA                                     " & vbNewLine _
                                             & "               ,GOODS_COND_KB_1,GOODS_COND_KB_2,REMARK_OUT,REMARK,LT_DATE,OFB_KB                                   " & vbNewLine _
                                             & "               ,SPD_KB,RSV_NO,SERIAL_NO,GOODS_CRT_DATE,ALLOC_PRIORITY,SUM(ALLOC_CAN_NB) AS ALLOC_CAN_NB            " & vbNewLine _
                                             & "             FROM $LM_TRN$..D_ZAI_TRS ZAI_TRS2                                                                     " & vbNewLine _
                                             & "             WHERE ZAI_TRS2.NRS_BR_CD = @NRS_BR_CD                                                                 " & vbNewLine _
                                             & "               AND ALLOC_CAN_NB <> 0 AND SYS_DEL_FLG='0'                                                           " & vbNewLine _
                                             & "             GROUP BY                                                                                              " & vbNewLine _
                                             & "               NRS_BR_CD,GOODS_CD_NRS,LOT_NO,IRIME,TOU_NO,SITU_NO,ZONE_CD,LOCA                                     " & vbNewLine _
                                             & "               ,GOODS_COND_KB_1,GOODS_COND_KB_2,REMARK_OUT,REMARK,LT_DATE,OFB_KB                                   " & vbNewLine _
                                             & "               ,SPD_KB,RSV_NO,SERIAL_NO,GOODS_CRT_DATE,ALLOC_PRIORITY                                              " & vbNewLine _
                                             & "             ) ZAI_TRS2                                                                                            " & vbNewLine _
                                             & "              ON ZAI_TRS.NRS_BR_CD = ZAI_TRS2.NRS_BR_CD                                                            " & vbNewLine _
                                             & "             AND ZAI_TRS.GOODS_CD_NRS = ZAI_TRS2.GOODS_CD_NRS                                                      " & vbNewLine _
                                             & "             AND ZAI_TRS.LOT_NO = ZAI_TRS2.LOT_NO                                                                  " & vbNewLine _
                                             & "             AND ZAI_TRS.IRIME = ZAI_TRS2.IRIME                                                                    " & vbNewLine _
                                             & "             AND ZAI_TRS.TOU_NO = ZAI_TRS2.TOU_NO                                                                  " & vbNewLine _
                                             & "             AND ZAI_TRS.SITU_NO = ZAI_TRS2.SITU_NO                                                                " & vbNewLine _
                                             & "             AND ZAI_TRS.ZONE_CD = ZAI_TRS2.ZONE_CD                                                                " & vbNewLine _
                                             & "             AND ZAI_TRS.LOCA = ZAI_TRS2.LOCA                                                                      " & vbNewLine _
                                             & "             AND ZAI_TRS.GOODS_COND_KB_1 = ZAI_TRS2.GOODS_COND_KB_1                                                " & vbNewLine _
                                             & "             AND ZAI_TRS.GOODS_COND_KB_2 = ZAI_TRS2.GOODS_COND_KB_2                                                " & vbNewLine _
                                             & "             AND ZAI_TRS.REMARK_OUT = ZAI_TRS2.REMARK_OUT                                                          " & vbNewLine _
                                             & "             AND ZAI_TRS.REMARK = ZAI_TRS2.REMARK                                                                  " & vbNewLine _
                                             & "             AND ZAI_TRS.LT_DATE = ZAI_TRS2.LT_DATE                                                                " & vbNewLine _
                                             & "             AND ZAI_TRS.OFB_KB = ZAI_TRS2.OFB_KB                                                                  " & vbNewLine _
                                             & "             AND ZAI_TRS.SPD_KB = ZAI_TRS2.SPD_KB                                                                  " & vbNewLine _
                                             & "             AND ZAI_TRS.RSV_NO = ZAI_TRS2.RSV_NO                                                                  " & vbNewLine _
                                             & "             AND ZAI_TRS.SERIAL_NO = ZAI_TRS2.SERIAL_NO                                                            " & vbNewLine _
                                             & "             AND ZAI_TRS.GOODS_CRT_DATE = ZAI_TRS2.GOODS_CRT_DATE                                                  " & vbNewLine _
                                             & "             AND ZAI_TRS.ALLOC_PRIORITY = ZAI_TRS2.ALLOC_PRIORITY                                                  " & vbNewLine _
                                             & "  --商品詳細                                                                                                       " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODS_DETAILS                                                                " & vbNewLine _
                                             & "         ON OUTKA_M.NRS_BR_CD = GOODS_DETAILS.NRS_BR_CD                                                            " & vbNewLine _
                                             & "        AND OUTKA_M.GOODS_CD_NRS = GOODS_DETAILS.GOODS_CD_NRS                                                      " & vbNewLine _
                                             & "        AND GOODS_DETAILS.SUB_KB = '41'                                                                            " & vbNewLine _
                                             & "        AND GOODS_DETAILS.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                                             & "  --纏めピッキングWK(纏めピック番号抽出用)                                                                          " & vbNewLine _
                                             & "LEFT JOIN                                                                                                         " & vbNewLine _
                                             & "    (                                                                                                             " & vbNewLine _
                                             & "        SELECT                                                                                                    " & vbNewLine _
                                             & "              C_OUTKA_L.NRS_BR_CD                                                                                 " & vbNewLine _
                                             & "            , C_OUTKA_L.CUST_CD_L                                                                                 " & vbNewLine _
                                             & "            , C_OUTKA_L.OUTKA_PLAN_DATE                                                                           " & vbNewLine _
                                             & "            , C_OUTKA_S.TOU_NO                                                                                    " & vbNewLine _
                                             & "            , C_OUTKA_S.SITU_NO                                                                                   " & vbNewLine _
                                             & "            , C_OUTKA_S.ZONE_CD                                                                                   " & vbNewLine _
                                             & "            , C_OUTKA_S.LOCA                                                                                      " & vbNewLine _
                                             & "            , C_TOTAL_PIC_WK.MTM_PICK_NO                                                                          " & vbNewLine _
                                             & "        FROM                                                                                                      " & vbNewLine _
                                             & "            $LM_TRN$..C_TOTAL_PIC_WK                                                                              " & vbNewLine _
                                             & "        LEFT OUTER JOIN                                                                                           " & vbNewLine _
                                             & "            $LM_TRN$..C_OUTKA_L                                                                                   " & vbNewLine _
                                             & "        ON                                                                                                        " & vbNewLine _
                                             & "                C_TOTAL_PIC_WK.NRS_BR_CD = C_OUTKA_L.NRS_BR_CD                                                    " & vbNewLine _
                                             & "            AND C_TOTAL_PIC_WK.OUTKA_NO_L = C_OUTKA_L.OUTKA_NO_L                                                  " & vbNewLine _
                                             & "        LEFT OUTER JOIN                                                                                           " & vbNewLine _
                                             & "            $LM_TRN$..C_OUTKA_S                                                                                   " & vbNewLine _
                                             & "        ON                                                                                                        " & vbNewLine _
                                             & "                C_TOTAL_PIC_WK.NRS_BR_CD = C_OUTKA_S.NRS_BR_CD                                                    " & vbNewLine _
                                             & "            AND C_TOTAL_PIC_WK.OUTKA_NO_L = C_OUTKA_S.OUTKA_NO_L                                                  " & vbNewLine _
                                             & "            AND C_TOTAL_PIC_WK.OUTKA_NO_M = C_OUTKA_S.OUTKA_NO_M                                                  " & vbNewLine _
                                             & "            AND C_TOTAL_PIC_WK.OUTKA_NO_S = C_OUTKA_S.OUTKA_NO_S                                                  " & vbNewLine _
                                             & "        WHERE                                                                                                     " & vbNewLine _
                                             & "                C_TOTAL_PIC_WK.NRS_BR_CD = @NRS_BR_CD                                                             " & vbNewLine _
                                             & "            AND C_TOTAL_PIC_WK.OUTKA_NO_L = @OUTKA_NO_L                                                           " & vbNewLine _
                                             & "            AND C_TOTAL_PIC_WK.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                             & "            AND C_OUTKA_L.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
                                             & "            AND C_OUTKA_S.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
                                             & "        GROUP BY                                                                                                  " & vbNewLine _
                                             & "              C_OUTKA_L.NRS_BR_CD                                                                                 " & vbNewLine _
                                             & "            , C_OUTKA_L.CUST_CD_L                                                                                 " & vbNewLine _
                                             & "            , C_OUTKA_L.OUTKA_PLAN_DATE                                                                           " & vbNewLine _
                                             & "            , C_OUTKA_S.TOU_NO                                                                                    " & vbNewLine _
                                             & "            , C_OUTKA_S.SITU_NO                                                                                   " & vbNewLine _
                                             & "            , C_OUTKA_S.ZONE_CD                                                                                   " & vbNewLine _
                                             & "            , C_OUTKA_S.LOCA                                                                                      " & vbNewLine _
                                             & "            , C_TOTAL_PIC_WK.MTM_PICK_NO                                                                          " & vbNewLine _
                                             & "    ) TOTAL_PIC_WK                                                                                                " & vbNewLine _
                                             & "    ON                                                                                                            " & vbNewLine _
                                             & "            OUTKA_L.NRS_BR_CD = TOTAL_PIC_WK.NRS_BR_CD                                                            " & vbNewLine _
                                             & "        AND CUST.CUST_CD_L = TOTAL_PIC_WK.CUST_CD_L                                                               " & vbNewLine _
                                             & "        AND OUTKA_L.OUTKA_PLAN_DATE = TOTAL_PIC_WK.OUTKA_PLAN_DATE                                                " & vbNewLine _
                                             & "        AND OUTKA_S.TOU_NO = TOTAL_PIC_WK.TOU_NO                                                                  " & vbNewLine _
                                             & "        AND OUTKA_S.SITU_NO = TOTAL_PIC_WK.SITU_NO                                                                " & vbNewLine _
                                             & "        AND OUTKA_S.ZONE_CD = TOTAL_PIC_WK.ZONE_CD                                                                " & vbNewLine _
                                             & "        AND OUTKA_S.LOCA = TOTAL_PIC_WK.LOCA                                                                      " & vbNewLine _
                                             & "  --纏めピッキングWK                                                                                              " & vbNewLine _
                                             & "  LEFT JOIN                                                                                                       " & vbNewLine _
                                             & "      $LM_TRN$..C_TOTAL_PIC_WK                                                                                    " & vbNewLine _
                                             & "  ON                                                                                                              " & vbNewLine _
                                             & "          OUTKA_S.NRS_BR_CD = C_TOTAL_PIC_WK.NRS_BR_CD                                                            " & vbNewLine _
                                             & "      AND OUTKA_S.OUTKA_NO_L = C_TOTAL_PIC_WK.OUTKA_NO_L                                                          " & vbNewLine _
                                             & "      AND OUTKA_S.OUTKA_NO_M = C_TOTAL_PIC_WK.OUTKA_NO_M                                                          " & vbNewLine _
                                             & "      AND OUTKA_S.OUTKA_NO_S = C_TOTAL_PIC_WK.OUTKA_NO_S                                                          " & vbNewLine _
                                             & "      AND C_TOTAL_PIC_WK.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                                             & "  -- 外装 要望番号2471対応 added 2015.12.14                                                                       " & vbNewLine _
                                             & "  LEFT JOIN                                                                                                       " & vbNewLine _
                                             & "      $LM_MST$..M_CUST_DETAILS MCDTL                                                                                " & vbNewLine _
                                             & "  ON                                                                                                              " & vbNewLine _
                                             & "          MCDTL.NRS_BR_CD    = GOODS.NRS_BR_CD                                                                    " & vbNewLine _
                                             & "      AND MCDTL.CUST_CD      = (CASE WHEN MCDTL.CUST_CLASS='00' THEN  GOODS.CUST_CD_L                                                          " & vbNewLine _
                                             & "                                     WHEN MCDTL.CUST_CLASS='01' THEN (GOODS.CUST_CD_L + GOODS.CUST_CD_M)                                       " & vbNewLine _
                                             & "                                     WHEN MCDTL.CUST_CLASS='02' THEN (GOODS.CUST_CD_L + GOODS.CUST_CD_M + GOODS.CUST_CD_S)                     " & vbNewLine _
                                             & "                                     WHEN MCDTL.CUST_CLASS='03' THEN (GOODS.CUST_CD_L + GOODS.CUST_CD_M + GOODS.CUST_CD_S + GOODS.CUST_CD_SS)  " & vbNewLine _
                                             & "                                END)                                                                        " & vbNewLine _
                                             & "      AND MCDTL.SET_NAIYO    = GOODS.OUTER_PKG                                                              " & vbNewLine _
                                             & "      AND MCDTL.SUB_KB       = '0F'                                                                         " & vbNewLine _
                                             & "      AND MCDTL.SYS_DEL_FLG  = '0'                                                                          " & vbNewLine

    Private Const SQL_FROM_DATA772 As String = "  --20160104 状態外装	                                                                                          " & vbNewLine _
                                            & "  LEFT JOIN LM_MST..Z_KBN KBNS006                                                                                  " & vbNewLine _
                                            & "         ON KBNS006.KBN_GROUP_CD = 'S006'                                                                          " & vbNewLine _
                                            & "		 AND  KBNS006.KBN_CD = ZAI_TRS.GOODS_COND_KB_2                                                                " & vbNewLine _
                                            & "		 AND KBNS006.SYS_DEL_FLG = '0'                                                                                " & vbNewLine


#End Region

#Region "GROUP BY句"

    Private Const SQL_GROUP_DATA As String = "  GROUP BY                                                      " & vbNewLine _
                                           & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID            " & vbNewLine _
                                           & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID            " & vbNewLine _
                                           & "         ELSE MR3.RPT_ID END                                    " & vbNewLine _
                                           & "  , OUTKA_L.NRS_BR_CD                                           " & vbNewLine _
                                           & "  , OUTKA_S.TOU_NO                                              " & vbNewLine _
                                           & "  , CUST.CUST_NM_L                                              " & vbNewLine _
                                           & "  , UNSOCO.UNSOCO_NM                                            " & vbNewLine _
                                           & "  , OUTKA_L.ARR_PLAN_DATE                                       " & vbNewLine _
                                           & "  , NRS_BR.NRS_BR_NM                                            " & vbNewLine _
                                           & "  , OUTKA_L.OUTKA_NO_L                                          " & vbNewLine _
                                           & "  , ZAI_TRS.INKO_DATE                                           " & vbNewLine _
                                           & "  , GOODS.GOODS_CD_CUST                                         " & vbNewLine _
                                           & "  , GOODS.GOODS_NM_1                                            " & vbNewLine _
                                           & "  , OUTKA_S.LOT_NO                                              " & vbNewLine _
                                           & "  , OUTKA_S.IRIME                                               " & vbNewLine _
                                           & "  , OUTKA_S.REMARK                                              " & vbNewLine _
                                           & "  , OUTKA_S.ALCTD_NB                                            " & vbNewLine _
                                           & "  , GOODS.NB_UT                                                 " & vbNewLine _
                                           & "  , OUTKA_S.SITU_NO                                             " & vbNewLine _
                                           & "  , OUTKA_S.ZONE_CD                                             " & vbNewLine _
                                           & "  , OUTKA_S.LOCA                                                " & vbNewLine _
                                           & "  , OUTKA_S.ALCTD_CAN_NB                                        " & vbNewLine _
                                           & "  , GOODS.PKG_NB                                                " & vbNewLine _
                                           & "  , DEST.DEST_NM                                                " & vbNewLine _
                                           & "  , DEST.AD_1                                                   " & vbNewLine _
                                           & "  , OUTKA_L.CUST_ORD_NO                                         " & vbNewLine _
                                           & "  , OUTKA_M.BUYER_ORD_NO_DTL                                    " & vbNewLine _
                                           & "  , CUST.CUST_CD_L                                              " & vbNewLine _
                                           & " --(2012.09.11)要望番号1428対応 --- START ---                   " & vbNewLine _
                                           & "  , UNSOCO.UNSOCO_BR_NM                                         " & vbNewLine _
                                           & "  , UNSO_L.UNSO_CD                                              " & vbNewLine _
                                           & "  , OUTKA_L.MATOME_PRINT_DATE                                   " & vbNewLine _
                                           & "  , OUTKA_L.MATOME_PRINT_TIME                                   " & vbNewLine _
                                           & " --(2012.09.11)要望番号1428対応 ---  END  ---                   " & vbNewLine _
                                           & " -- 2012.11.07 yamanaka 千葉_DIC対応 Start --                   " & vbNewLine _
                                           & "  , OUTKA_L.OUTKO_DATE                                          " & vbNewLine _
                                           & "  , GOODS.CUST_COST_CD1                                         " & vbNewLine _
                                           & "  , OUTKA_S.ALCTD_QT                                            " & vbNewLine _
                                           & "  , OUTKA_M.IRIME_UT                                            " & vbNewLine _
                                           & " -- 2012.11.07 yamanaka 千葉_DIC対応 End --                     " & vbNewLine _
                                           & " -- 2014.10.15 ナフコ 対応 START --                             " & vbNewLine _
                                           & "  , MJIS.KEN                                                    " & vbNewLine _
                                           & " -- 2014.10.15 ナフコ 対応  End --                              " & vbNewLine

    Private Const SQL_GROUP_DATA690_1 As String = "  GROUP BY                                                 " & vbNewLine _
                                           & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID            " & vbNewLine _
                                           & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID            " & vbNewLine _
                                           & "         ELSE MR3.RPT_ID END                                    " & vbNewLine _
                                           & "  , OUTKA_L.NRS_BR_CD                                           " & vbNewLine _
                                           & "  , OUTKA_S.TOU_NO                                              " & vbNewLine _
                                           & "  , CUST.CUST_NM_L                                              " & vbNewLine _
                                           & "  , UNSOCO.UNSOCO_NM                                            " & vbNewLine _
                                           & "  , OUTKA_L.ARR_PLAN_DATE                                       " & vbNewLine _
                                           & "  , NRS_BR.NRS_BR_NM                                            " & vbNewLine _
                                           & "  , OUTKA_L.OUTKA_NO_L                                          " & vbNewLine _
                                           & "  , ZAI_TRS.INKO_DATE                                           " & vbNewLine _
                                           & "  , GOODS.GOODS_CD_CUST                                         " & vbNewLine _
                                           & "  , GOODS.GOODS_NM_1                                            " & vbNewLine _
                                           & "  , OUTKA_S.LOT_NO                                              " & vbNewLine _
                                           & "  , OUTKA_S.IRIME                                               " & vbNewLine _
                                           & "  , OUTKA_S.REMARK                                              " & vbNewLine _
                                           & "  , OUTKA_S.ALCTD_NB                                            " & vbNewLine _
                                           & "  , GOODS.NB_UT                                                 " & vbNewLine _
                                           & "  , OUTKA_S.SITU_NO                                             " & vbNewLine _
                                           & "  , OUTKA_S.ZONE_CD                                             " & vbNewLine _
                                           & "  , OUTKA_S.LOCA                                                " & vbNewLine _
                                           & "  , OUTKA_S.ALCTD_CAN_NB                                        " & vbNewLine _
                                           & "  , GOODS.PKG_NB                                                " & vbNewLine _
                                           & "  , DEST.DEST_NM                                                " & vbNewLine _
                                           & "  , CASE WHEN OUTKA_L.DEST_KB = '00' THEN DEST.AD_1             " & vbNewLine _
                                           & "         WHEN OUTKA_L.DEST_KB = '02' THEN EDIL.DEST_AD_1        " & vbNewLine _
                                           & "    ELSE OUTKA_L.DEST_AD_1 END                                  " & vbNewLine _
                                           & "  , OUTKA_L.CUST_ORD_NO                                         " & vbNewLine _
                                           & "  , OUTKA_M.BUYER_ORD_NO_DTL                                    " & vbNewLine _
                                           & "  , CUST.CUST_CD_L                                              " & vbNewLine _
                                           & " --(2012.09.11)要望番号1428対応 --- START ---                   " & vbNewLine _
                                           & "  , UNSOCO.UNSOCO_BR_NM                                         " & vbNewLine _
                                           & "  , UNSO_L.UNSO_CD                                              " & vbNewLine _
                                           & "  , OUTKA_L.MATOME_PRINT_DATE                                   " & vbNewLine _
                                           & "  , OUTKA_L.MATOME_PRINT_TIME                                   " & vbNewLine _
                                           & " --(2012.09.11)要望番号1428対応 ---  END  ---                   " & vbNewLine _
                                           & " -- 2012.11.07 yamanaka 千葉_DIC対応 Start --                   " & vbNewLine _
                                           & "  , OUTKA_L.OUTKO_DATE                                          " & vbNewLine _
                                           & "  , GOODS.CUST_COST_CD1                                         " & vbNewLine _
                                           & "  , OUTKA_S.ALCTD_QT                                            " & vbNewLine _
                                           & "  , OUTKA_M.IRIME_UT                                            " & vbNewLine _
                                           & " -- 2012.11.07 yamanaka 千葉_DIC対応 End --                     " & vbNewLine _
                                           & " -- 2014.10.15 ナフコ 対応 START --                             " & vbNewLine _
                                           & "  , MJIS.KEN                                                    " & vbNewLine _
                                           & " -- 2014.10.15 ナフコ 対応  End --                              " & vbNewLine

    Private Const SQL_GROUP_DATA691 As String = "  ----ADD 20220418 027736 LMC691 のため追加   LMC691のため  " & vbNewLine _
                                           & "  , OUTKA_S.SERIAL_NO                                            " & vbNewLine

    Private Const SQL_GROUP_DATA690 As String = "  --20160812  要望番号2610                                     " & vbNewLine _
                                           & "  , GOODS.OUTKA_ATT                               " & vbNewLine _
                                           & "  , OUTKA_M.REMARK                                " & vbNewLine


    Private Const SQL_GROUP_DATA693 As String = "  , TOPEDI.EDI_CTL_NO                                        " & vbNewLine _
                                              & "  , KBN.KBN_NM1                                              " & vbNewLine _
                                              & "  , OUTKA_S.SERIAL_NO                                        " & vbNewLine

    Private Const SQL_GROUP_DATA697 As String = "  , OUTKA_L.OUTKA_PLAN_DATE                                  " & vbNewLine _
                                              & "  , OUTKA_L.REMARK                                           " & vbNewLine _
                                              & "  , MCD.SET_NAIYO                                            " & vbNewLine _
                                              & "  , ISNULL(ZAI_TRS2.ALLOC_CAN_NB,0)                          " & vbNewLine _
                                              & "  , OUTKA_S.OUTKA_NO_M                                       " & vbNewLine _
                                              & "  , OUTKA_S.OUTKA_NO_S                                       " & vbNewLine _
                                              & "  , ZAI_TRS.GOODS_KANRI_NO                                   " & vbNewLine _
                                              & "  , GOODS_DETAILS.SET_NAIYO                                  " & vbNewLine _
                                              & "  , TOTAL_PIC_WK.MTM_PICK_NO                                 " & vbNewLine _
                                              & "  , C_TOTAL_PIC_WK.GOODS_KANRI_NO                            " & vbNewLine _
                                              & "  , GOODS.CUST_CD_M                                          " & vbNewLine _
                                              & "  , GOODS.CUST_CD_S                                          " & vbNewLine _
                                              & " -- 2015.12.14 要望番号2471対応                              " & vbNewLine _
                                              & "  , MCDTL.SET_NAIYO_2                                        " & vbNewLine

    Private Const SQL_GROUP_DATA772 As String = "  , GOODS.PKG_NB                                             " & vbNewLine _
                                            & "  ,ZAI_TRS.REMARK_OUT                                          " & vbNewLine _
                                            & "  ,KBNS006.KBN_NM1                                             " & vbNewLine _
                                            & "  ,DEST.AD_2                " & vbNewLine

#End Region

#Region "ORDER BY句"

    '(2012.08.16)仕様変更 --- START ---
    'Private Const SQL_ORDER_DATA As String = "  ORDER BY                              " & vbNewLine _
    '                                       & "   OUTKA_S.TOU_NO                       " & vbNewLine _
    '                                       & "  ,OUTKA_S.SITU_NO                      " & vbNewLine _
    '                                       & "  ,OUTKA_S.ZONE_CD                      " & vbNewLine _
    '                                       & "  ,OUTKA_S.LOCA                         " & vbNewLine

    Private Const SQL_ORDER_DATA As String = "  ORDER BY                                 " & vbNewLine _
                                           & "        OUTKA_S.TOU_NO                     " & vbNewLine _
                                           & "      , OUTKA_S.SITU_NO                    " & vbNewLine _
                                           & "--(2012.09.11)要望番号1428対応 -- START -- " & vbNewLine _
                                           & "      , CUST.CUST_NM_L                     " & vbNewLine _
                                           & "      , UNSO_L.UNSO_CD                     " & vbNewLine _
                                           & "--    , UNSOCO.UNSOCO_NM                   " & vbNewLine _
                                           & "--(2012.09.11)要望番号1428対応 --  END  -- " & vbNewLine _
                                           & "      , OUTKA_S.ZONE_CD                    " & vbNewLine _
                                           & "      , OUTKA_S.LOCA                       " & vbNewLine _
                                           & "      , GOODS.GOODS_CD_CUST                " & vbNewLine _
                                           & "      , OUTKA_S.LOT_NO                     " & vbNewLine _
                                           & "      , OUTKA_S.IRIME                      " & vbNewLine
    '(2012.08.16)仕様変更 ---  END  ---

    '(2014.10.15)ナフコ対応 --- START ---
    Private Const SQL_ORDER_DATA_771 As String = "  ORDER BY                             " & vbNewLine _
                                           & "        ISNULL(MJIS.KEN,'')                " & vbNewLine _
                                           & "      , DEST.AD_1                          " & vbNewLine _
                                           & "      , OUTKA_S.TOU_NO                     " & vbNewLine _
                                           & "      , OUTKA_S.SITU_NO                    " & vbNewLine _
                                           & "--(2012.09.11)要望番号1428対応 -- START -- " & vbNewLine _
                                           & "      , CUST.CUST_NM_L                     " & vbNewLine _
                                           & "      , UNSO_L.UNSO_CD                     " & vbNewLine _
                                           & "--    , UNSOCO.UNSOCO_NM                   " & vbNewLine _
                                           & "--(2012.09.11)要望番号1428対応 --  END  -- " & vbNewLine _
                                           & "      , OUTKA_S.ZONE_CD                    " & vbNewLine _
                                           & "      , OUTKA_S.LOCA                       " & vbNewLine _
                                           & "      , GOODS.GOODS_CD_CUST                " & vbNewLine _
                                           & "      , OUTKA_S.LOT_NO                     " & vbNewLine _
                                           & "      , OUTKA_S.IRIME                      " & vbNewLine
    '(2014.10.15)ナフコ対応 ---  END  ---
#End Region

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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC690IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC690DAC.SQL_SELECT_MPrt)      'SQL構築(Select句)
        Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA)        'SQL構築(From句)
        Call SetSQLWhereDATA(inTbl.Rows(0))               '条件設定(Where句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC690DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds


    End Function

    ''' <summary>
    ''' 印刷対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC690IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '2012.12.10 yamanaka Start
        '判断用フラグ
        Dim rptFlg As String = ds.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString()
        '2012.12.10 yamanaka End

        '2012.11.13 yamanaka Start
        'SQL作成
        If rptFlg.Equals("LMC693") = True OrElse rptFlg.Equals("LMC770") = True Then
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA)          'SQL構築 SELECT句
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA693)       'SQL構築 SELECT2句
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA693)         'SQL構築 FROM句
            Call SetSQLWhereDATA(inTbl.Rows(0))                   '条件設定
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA)           'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA693)        'SQL構築 GROUP BY2句
            Me._StrSql.Append(LMC690DAC.SQL_ORDER_DATA)           'SQL構築 ORDER BY句

            'BP・カストロール対応 yamanaka 2012.12.05 Start
        ElseIf rptFlg.Equals("LMC697") = True Then
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA)          'SQL構築 SELECT句
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA697)       'SQL構築 SELECT句2
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA)            'SQL構築 FROM句
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA697)         'SQL構築 FROM句2
            Call SetSQLWhereDATA(inTbl.Rows(0), rptFlg)           '条件設定
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA)           'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA697)        'SQL構築 GROUP BY句2
            Me._StrSql.Append(LMC690DAC.SQL_ORDER_DATA)           'SQL構築 ORDER BY句
            'BP・カストロール対応 yamanaka 2012.12.05 End

            '(2014.10.15)ナフコ対応 --- START ---
        ElseIf rptFlg.Equals("LMC771") = True Then
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA)          'SQL構築 SELECT句
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA)            'SQL構築 FROM句
            Call SetSQLWhereDATA(inTbl.Rows(0))                   '条件設定
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA)           'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_ORDER_DATA_771)       'SQL構築 ORDER BY句
            '(2014.10.15)ナフコ対応 --- E N D ---
            '(2016/01/04)ストラタシスジャパン --- START --
            '20160128 日医工追加
        ElseIf rptFlg.Equals("LMC772") = True OrElse rptFlg.Equals("LMC773") = True Then
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA)          'SQL構築 SELECT句
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA772)          'SQL構築 SELECT句2
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA)            'SQL構築 FROM句
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA772)            'SQL構築 FROM句2
            Call SetSQLWhereDATA(inTbl.Rows(0))                   '条件設定
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA)           'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA772)           'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_ORDER_DATA)       'SQL構築 ORDER BY句
            '(2016/01/047)ストラタシスジャパン --- E N D ---
            '20160128 日医工追加 END

            '2016/10/13 群馬DIC商品注意事項追加について分離
        ElseIf rptFlg.Equals("LMC690") = True Then
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA690_1)     'SQL構築 SELECT句
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA690)       'SQL構築 SELECT句2
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA690)         'SQL構築 FROM句
            Call SetSQLWhereDATA(inTbl.Rows(0))                   '条件設定
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA690_1)      'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA690)        'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_ORDER_DATA)           'SQL構築 ORDER BY句
            '2016/10/13 群馬DIC商品注意事項追加について分離

        ElseIf rptFlg.Equals("LMC691") = True Then
            'UPD 2022/04/18　027736 LMC691のためにSERIAL_NOを追加
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA)          'SQL構築 SELECT句
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA691)       'SQL構築 SELECT句2
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA)            'SQL構築 FROM句
            Call SetSQLWhereDATA(inTbl.Rows(0))                   '条件設定
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA)           'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA691)        'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_ORDER_DATA)           'SQL構築 ORDER BY句

        Else
            Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA)          'SQL構築 SELECT句
            Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA)            'SQL構築 FROM句
            Call SetSQLWhereDATA(inTbl.Rows(0))                   '条件設定
            Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA)           'SQL構築 GROUP BY句
            Me._StrSql.Append(LMC690DAC.SQL_ORDER_DATA)           'SQL構築 ORDER BY句

        End If

        'Me._StrSql.Append(LMC690DAC.SQL_SELECT_DATA)          'SQL構築 SELECT句
        'Me._StrSql.Append(LMC690DAC.SQL_FROM_DATA)            'SQL構築 FROM句
        'Call SetSQLWhereDATA(inTbl.Rows(0))                   '条件設定
        'Me._StrSql.Append(LMC690DAC.SQL_GROUP_DATA)           'SQL構築 GROUP BY句
        'Me._StrSql.Append(LMC690DAC.SQL_ORDER_DATA)           'SQL構築 ORDER BY句
        '2012.11.07 yamanaka End

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC690DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("REMARK", "REMARK")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("ZAN_KOSU", "ZAN_KOSU")
        map.Add("ZAN_KOSU_SORT", "ZAN_KOSU_SORT")
        map.Add("ZAN_HASU", "ZAN_HASU")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("AD_1", "AD_1")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("ROW_COUNT", "ROW_COUNT")                   '(2012.08.17) 一覧偶数行に背景色対応
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")             '(2012.09.11) 要望番号1248：運送会社支店名の表示対応
        map.Add("UNSO_CD", "UNSO_CD")                       '(2012.09.11) 要望番号1248：改頁条件変更対応
        map.Add("MATOME_PRINT_DATE", "MATOME_PRINT_DATE")   '(2012.09.11) 要望番号1248：印刷日時の表示
        map.Add("MATOME_PRINT_TIME", "MATOME_PRINT_TIME")   '(2012.09.11) 要望番号1248：印刷日時の表示
        map.Add("OUTKO_DATE", "OUTKO_DATE")                 '2012.11.07 yamanaka : 千葉DIC対応
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")           '2012.11.07 yamanaka : 千葉DIC対応
        map.Add("ALCTD_QT", "ALCTD_QT")                     '2012.11.07 yamanaka : 千葉DIC対応
        map.Add("IRIME_UT", "IRIME_UT")                     '2012.11.07 yamanaka : 千葉DIC対応
        map.Add("USER_NM", "USER_NM")                       '2013.03.27 kobayashi: 要望管理1972


        If ds.Tables("M_RPT").Rows(0).Item("RPT_ID").Equals("LMC693") = True OrElse
            ds.Tables("M_RPT").Rows(0).Item("RPT_ID").Equals("LMC770") = True Then
            map.Add("EDI_CTL_NO", "EDI_CTL_NO")             '2012.11.13 yamanaka : 千葉東ﾚ対応
            map.Add("KBN_NM1", "KBN_NM1")                   '2012.11.13 yamanaka : 千葉東ﾚ対応
            map.Add("SERIAL_NO", "SERIAL_NO")                   '2012.11.13 yamanaka : 千葉東ﾚ対応
            map.Add("PKG_NB", "PKG_NB")                   '2012.11.13 yamanaka : 千葉東ﾚ対応
            map.Add("HASU", "HASU")                   '2012.11.13 yamanaka : 千葉東ﾚ対応

        ElseIf ds.Tables("M_RPT").Rows(0).Item("RPT_ID").Equals("LMC691") = True Then
            map.Add("SERIAL_NO", "SERIAL_NO")   '2022/04/18 027736 

        ElseIf ds.Tables("M_RPT").Rows(0).Item("RPT_ID").Equals("LMC697") = True Then

            map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")   '2012.12.05 yamanaka : 埼玉BP・カストロール対応
            map.Add("REMARK_OUT", "REMARK_OUT")             '2012.12.05 yamanaka : 埼玉BP・カストロール対応
            map.Add("PIC_FLG", "PIC_FLG")                   '2012.12.05 yamanaka : 埼玉BP・カストロール対応
            map.Add("ROW_CHECK_COUNT", "ROW_CHECK_COUNT")   '2012.12.10 yamanaka : 埼玉BP・カストロール対応
            map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")         '2012.12.10 yamanaka : 埼玉BP・カストロール対応
            map.Add("PKG_NB", "PKG_NB")                     '2013.02.07 s.koba : 埼玉BP・カストロール対応
            map.Add("OUTKA_NO_M", "OUTKA_NO_M")             '2013.09.17 kurihara : WIT対応
            map.Add("OUTKA_NO_S", "OUTKA_NO_S")             '2013.09.17 kurihara : WIT対応
            map.Add("GOODS_KANRI_NO", "GOODS_KANRI_NO")     '2013.09.17 kurihara : WIT対応
            map.Add("CHK_TANI", "CHK_TANI")                 '2013.09.17 kurihara : WIT対応
            map.Add("MATOME_PICK_NO", "MATOME_PICK_NO")     '2013.09.17 kurihara : WIT対応
            map.Add("KENPIN_FLG", "KENPIN_FLG")             '2013.11.14 kasama : WIT対応
            map.Add("CUST_CD_M", "CUST_CD_M")             '2013.11.29 kasama : WIT対応
            map.Add("CUST_CD_S", "CUST_CD_S")             '2013.11.29 kasama : WIT対応

#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
            map.Add("OUTER_PKG_NM", "OUTER_PKG_NM")             '
#End If
            '(2014.10.15)ナフコ対応 --- START ---
        ElseIf ds.Tables("M_RPT").Rows(0).Item("RPT_ID").Equals("LMC771") = True Then
            map.Add("KEN_NM", "KEN_NM")
            '(2014.10.15)ナフコ対応 --- E N D ---
            '20160104 ストラタシスジャパン
        ElseIf ds.Tables("M_RPT").Rows(0).Item("RPT_ID").Equals("LMC772") = True OrElse
            ds.Tables("M_RPT").Rows(0).Item("RPT_ID").Equals("LMC773") = True Then
            map.Add("KEN_NM", "KEN_NM")
            map.Add("PKG_NB", "PKG_NB")
            map.Add("REMARK_OUT", "REMARK_OUT")
            map.Add("JOTAI_GAISO", "JOTAI_GAISO")
            map.Add("HASU", "HASU")
            map.Add("AD_2", "AD_2")
            '20160104 ストラタシスジャパン
            '20160926 群馬-DIC要望番号：2610
        ElseIf ds.Tables("M_RPT").Rows(0).Item("RPT_ID").Equals("LMC690") = True Then
            map.Add("OUTKA_ATT", "OUTKA_ATT")                       '20160812 要望番号2610　：群馬DIC　adachi
            map.Add("REMARK_M", "REMARK_M")                       '20161020 要望番号2610　：群馬DIC　adachi
            '20160926 群馬-DIC要望番号：2610
            map.Add("ARR_PLAN_DATE_DATENAME", "ARR_PLAN_DATE_DATENAME")             'ADD 2019/11/21  007676
            map.Add("OUTKO_DATE_DATENAME", "OUTKO_DATE_DATENAME")                   'ADD 2019/11/21  007676
        End If

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC690OUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereDATA(ByVal inTblRow As DataRow, Optional ByVal rptFlg As String = "")

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("OUTKA_L.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '(2013.03.08)要望番号:1939 予定入力済は出力対象外 -- START --
            Me._StrSql.Append(" AND OUTKA_L.OUTKA_STATE_KB <> '10'")
            Me._StrSql.Append(vbNewLine)
            '(2013.03.08)要望番号:1939 予定入力済は出力対象外 --  END  --

            '埼玉BP・カストロール修正対応 yamanaka 2013.01.21 Start
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                'ピック区分
                If whereStr.Equals("30") = False Then
                    Me._StrSql.Append(" AND OUTKA_L.PICK_KB = 02")
                    Me._StrSql.Append(vbNewLine)

                End If

                '営業所コード
                Me._StrSql.Append(" AND OUTKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            End If

            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND OUTKA_L.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            'End If
            '埼玉BP・カストロール修正対応 yamanaka 2013.01.21 Start

            '出荷管理番号（大）
            whereStr = .Item("OUTKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

            End If

            '2012.12.10 yamanaka : 埼玉BP・カストロール対応 Start
            '行数設定
            If rptFlg.Equals("LMC697") = True Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_COUNT", Me._Row("ROW_COUNT"), DBDataType.CHAR))
            End If
            '2012.12.10 yamanaka : 埼玉BP・カストロール対応 End


            '2013.03.27 s.kobayashi : 要望管理1972対応 対応 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_NM", MyBase.GetUserName(), DBDataType.NVARCHAR))
            '2013.03.27 s.kobayashi : 要望管理1972対応 対応 End

        End With

    End Sub


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

#End Region

#End Region

#End Region

End Class
