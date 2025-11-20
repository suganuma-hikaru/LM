' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI580    : TSMC請求明細書
'  作  成  者       :  [HORI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI580DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI580DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 印刷データ検索（ヘッダ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HEAD As String = "" _
        & "SELECT                                                   " & vbNewLine _
        & "   SMT.NRS_BR_CD                                         " & vbNewLine _
        & "  ,NBR.NRS_BR_NM                                         " & vbNewLine _
        & "  ,SMT.SEIQTO_CD                                         " & vbNewLine _
        & "  ,SEI.SEIQTO_NM                                         " & vbNewLine _
        & "  ,SMT.INV_DATE_FROM                                     " & vbNewLine _
        & "  ,SMT.INV_DATE_TO                                       " & vbNewLine _
        & "FROM                                                     " & vbNewLine _
        & "  $LM_TRN$..I_SEKY_MEISAI_TSMC AS SMT                    " & vbNewLine _
        & "LEFT JOIN                                                " & vbNewLine _
        & "  $LM_MST$..M_NRS_BR AS NBR                              " & vbNewLine _
        & "  ON                                                     " & vbNewLine _
        & "    NBR.NRS_BR_CD = SMT.NRS_BR_CD                        " & vbNewLine _
        & "LEFT JOIN                                                " & vbNewLine _
        & "  $LM_MST$..M_SEIQTO AS SEI                              " & vbNewLine _
        & "  ON                                                     " & vbNewLine _
        & "        SEI.NRS_BR_CD = SMT.NRS_BR_CD                    " & vbNewLine _
        & "    AND SEI.SEIQTO_CD = SMT.SEIQTO_CD                    " & vbNewLine _
        & "    AND SEI.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & "WHERE                                                    " & vbNewLine _
        & "      SMT.NRS_BR_CD = @NRS_BR_CD                         " & vbNewLine _
        & "  AND SMT.JOB_NO = @JOB_NO                               " & vbNewLine _
        & "GROUP BY                                                 " & vbNewLine _
        & "   SMT.NRS_BR_CD                                         " & vbNewLine _
        & "  ,NBR.NRS_BR_NM                                         " & vbNewLine _
        & "  ,SMT.SEIQTO_CD                                         " & vbNewLine _
        & "  ,SEI.SEIQTO_NM                                         " & vbNewLine _
        & "  ,SMT.INV_DATE_FROM                                     " & vbNewLine _
        & "  ,SMT.INV_DATE_TO                                       " & vbNewLine

    ''' <summary>
    ''' 印刷データ検索（メイン）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAIN As String = "" _
        & "SELECT                                                                               " & vbNewLine _
        & "   CASE ZA2.LVL21_UT                                                                 " & vbNewLine _
        & "      WHEN 'PAL' THEN CASE WHEN RTRIM(ZA2.PLT_NO) = '' THEN ZA2.CYLINDER_NO ELSE ZA2.PLT_NO END " & vbNewLine _
        & "      WHEN 'CY' THEN ZA2.CYLINDER_NO                                                 " & vbNewLine _
        & "      WHEN 'CTR' THEN ZA2.CYLINDER_NO                                                " & vbNewLine _
        & "      WHEN 'TNK' THEN ZA2.CYLINDER_NO                                                " & vbNewLine _
        & "      ELSE ''                                                                        " & vbNewLine _
        & "      END AS PLT_CYLINDER                                                            " & vbNewLine _
        & "  ,GOD.GOODS_CD_CUST                                                                 " & vbNewLine _
        & "  ,GOD.GOODS_NM_1 AS GOODS_NM                                                        " & vbNewLine _
        & "  ,SMT.LOT_NO AS LOT_NO                                                              " & vbNewLine _
        & "  ,'1' AS QUANTITY                                                                   " & vbNewLine _
        & "  ,ZA2.LVL21_UT AS PACKAGING                                                         " & vbNewLine _
        & "  ,CASE SMT.RETURN_FLAG WHEN '00' THEN 'One way' WHEN '01' THEN 'Round' ELSE '' END AS ROUND_OR_ONE_WAY " & vbNewLine _
        & "  ,KB1.KBN_NM1 AS VENDOR_CD                                                          " & vbNewLine _
        & "  ,KB1.KBN_NM2 AS VENDOR                                                             " & vbNewLine _
        & "  ,CASE WHEN ISNULL(ZA2.INKA_DATE, '') = '' THEN '-'                                 " & vbNewLine _
        & "        ELSE CONVERT(varchar(10), CONVERT(datetime, ZA2.INKA_DATE), 111)             " & vbNewLine _
        & "        END AS INKA_DATE                                                             " & vbNewLine _
        & "  ,CASE WHEN ISNULL(ZA2.OUTKA_PLAN_DATE, '') = '' THEN '-'                           " & vbNewLine _
        & "        ELSE CONVERT(varchar(10), CONVERT(datetime, ZA2.OUTKA_PLAN_DATE), 111)       " & vbNewLine _
        & "        END AS OUTKA_PLAN_DATE                                                       " & vbNewLine _
        & "  ,CASE WHEN ISNULL(ZA2.RTN_INKA_DATE, '') = '' THEN '-'                             " & vbNewLine _
        & "        ELSE CONVERT(varchar(10), CONVERT(datetime, ZA2.RTN_INKA_DATE), 111)         " & vbNewLine _
        & "        END AS RTN_INKA_DATE                                                         " & vbNewLine _
        & "  ,CASE WHEN ISNULL(ZA2.RTN_OUTKA_PLAN_DATE, '') = '' THEN '-'                       " & vbNewLine _
        & "        ELSE CONVERT(varchar(10), CONVERT(datetime, ZA2.RTN_OUTKA_PLAN_DATE), 111)   " & vbNewLine _
        & "        END AS RTN_OUTKA_PLAN_DATE                                                   " & vbNewLine _
        & "  ,DATEDIFF(day, ZA2.INKA_DATE, ZA2.OUTKA_PLAN_DATE) + 1 AS STORAGE_DATE             " & vbNewLine _
        & "  ,SMT.SET_CLC_DATE                                                                  " & vbNewLine _
        & "  ,SMT.OVER_DATE                                                                     " & vbNewLine _
        & "  ,SMT.SET_AMO                                                                       " & vbNewLine _
        & "  ,TNK.HANDLING_IN AS OVER_TANKA                                                     " & vbNewLine _
        & "  ,SMT.SET_OVER_AMO                                                                  " & vbNewLine _
        & "  ,SMT.SET_AMO_TTL                                                                   " & vbNewLine _
        & "  ,ZA2.ASN_NO                                                                        " & vbNewLine _
        & "FROM                                                                                 " & vbNewLine _
        & "   (                                                                                 " & vbNewLine _
        & "    SELECT                                                                           " & vbNewLine _
        & "          *                                                                          " & vbNewLine _
        & "        , ROW_NUMBER() OVER (                                                        " & vbNewLine _
        & "            PARTITION BY                                                             " & vbNewLine _
        & "                  NRS_BR_CD                                                          " & vbNewLine _
        & "                , OUTKA_NO_L                                                         " & vbNewLine _
        & "                , LOT_NO                                                             " & vbNewLine _
        & "                , CYLINDER_NO                                                        " & vbNewLine _
        & "                , PLT_NO                                                             " & vbNewLine _
        & "                , GOODS_CD_NRS                                                       " & vbNewLine _
        & "            ORDER BY                                                                 " & vbNewLine _
        & "                REC_NO                                                               " & vbNewLine _
        & "            ) AS ASN_NO_SEQ                                                          " & vbNewLine _
        & "    FROM                                                                             " & vbNewLine _
        & "        $LM_TRN$..I_SEKY_MEISAI_TSMC                                                 " & vbNewLine _
        & "    WHERE                                                                            " & vbNewLine _
        & "        NRS_BR_CD = @NRS_BR_CD                                                       " & vbNewLine _
        & "    AND JOB_NO = @JOB_NO                                                             " & vbNewLine _
        & "    ) AS SMT                                                                         " & vbNewLine _
        & "LEFT JOIN                                                                            " & vbNewLine _
        & "  $LM_MST$..M_GOODS AS GOD                                                           " & vbNewLine _
        & "  ON                                                                                 " & vbNewLine _
        & "        GOD.NRS_BR_CD = SMT.NRS_BR_CD                                                " & vbNewLine _
        & "    AND GOD.GOODS_CD_NRS = SMT.GOODS_CD_NRS                                          " & vbNewLine _
        & "    AND GOD.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
        & "LEFT JOIN                                                                            " & vbNewLine _
        & "  $LM_MST$..Z_KBN AS KB1                                                             " & vbNewLine _
        & "  ON                                                                                 " & vbNewLine _
        & "        KB1.KBN_GROUP_CD = 'K038'                                                    " & vbNewLine _
        & "    AND KB1.KBN_NM3 = GOD.CUST_CD_S                                                  " & vbNewLine _
        & "    AND KB1.KBN_NM4 = GOD.CUST_CD_SS                                                 " & vbNewLine _
        & "    AND KB1.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
        & "LEFT JOIN                                                                            " & vbNewLine _
        & "  (                                                                                  " & vbNewLine _
        & "    SELECT                                                                           " & vbNewLine _
        & "       NRS_BR_CD                                                                     " & vbNewLine _
        & "      ,CASE RTRIM(PLT_NO) WHEN '' THEN OUTKA_NO_L ELSE '' END AS OUTKA_NO_L          " & vbNewLine _
        & "      ,CYLINDER_NO                                                                   " & vbNewLine _
        & "      ,PLT_NO                                                                        " & vbNewLine _
        & "      ,LAST_INV_DATE                                                                 " & vbNewLine _
        & "      ,ASN_NO                                                                        " & vbNewLine _
        & "      ,MAX(INKA_DATE) AS INKA_DATE                                                   " & vbNewLine _
        & "      ,MAX(OUTKA_PLAN_DATE) AS OUTKA_PLAN_DATE                                       " & vbNewLine _
        & "      ,MAX(RTN_INKA_DATE) AS RTN_INKA_DATE                                           " & vbNewLine _
        & "      ,CASE WHEN MIN(STATUS) = '04' THEN MAX(RTN_OUTKA_PLAN_DATE) ELSE '' END AS RTN_OUTKA_PLAN_DATE " & vbNewLine _
        & "      ,MAX(CASE WHEN RTRIM(LVL2_UT) = '' THEN LVL1_UT ELSE LVL2_UT END) AS LVL21_UT  " & vbNewLine _
        & "    FROM                                                                             " & vbNewLine _
        & "      $LM_TRN$..D_ZAI_TSMC                                                           " & vbNewLine _
        & "    WHERE                                                                            " & vbNewLine _
        & "          /* UP_FLG = '00' 最新でなくとも請求と紐付く在庫を結合する必要あり             " & vbNewLine _
        & "      AND */ SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "    GROUP BY                                                                         " & vbNewLine _
        & "       NRS_BR_CD                                                                     " & vbNewLine _
        & "      ,CASE RTRIM(PLT_NO) WHEN '' THEN OUTKA_NO_L ELSE '' END                        " & vbNewLine _
        & "      ,CYLINDER_NO                                                                   " & vbNewLine _
        & "      ,PLT_NO                                                                        " & vbNewLine _
        & "      ,LAST_INV_DATE                                                                 " & vbNewLine _
        & "      ,ASN_NO                                                                        " & vbNewLine _
        & "  ) AS ZA2                                                                           " & vbNewLine _
        & "  ON                                                                                 " & vbNewLine _
        & "        ZA2.NRS_BR_CD = SMT.NRS_BR_CD                                                " & vbNewLine _
        & "    AND ZA2.OUTKA_NO_L = CASE RTRIM(SMT.PLT_NO) WHEN '' THEN SMT.OUTKA_NO_L ELSE '' END " & vbNewLine _
        & "    AND ZA2.CYLINDER_NO = SMT.CYLINDER_NO                                            " & vbNewLine _
        & "    AND ZA2.PLT_NO = SMT.PLT_NO                                                      " & vbNewLine _
        & "    AND ZA2.LAST_INV_DATE = SMT.INV_DATE_TO                                          " & vbNewLine _
        & "    AND ZA2.ASN_NO = (                                                               " & vbNewLine _
        & "      SELECT TOP 1                                                                   " & vbNewLine _
        & "          ASN_NO                                                                     " & vbNewLine _
        & "      FROM (                                                                         " & vbNewLine _
        & "          SELECT                                                                     " & vbNewLine _
        & "                ZAI_SUBQ.ASN_NO                                                      " & vbNewLine _
        & "              , DENSE_RANK() OVER(                                                   " & vbNewLine _
        & "                  PARTITION BY                                                       " & vbNewLine _
        & "                        ZAI_SUBQ.NRS_BR_CD                                           " & vbNewLine _
        & "                      , ZAI_SUBQ.OUTKA_NO_L                                          " & vbNewLine _
        & "                      , ZAI_SUBQ.LOT_NO                                              " & vbNewLine _
        & "                      , ZAI_SUBQ.CYLINDER_NO                                         " & vbNewLine _
        & "                      , ZAI_SUBQ.PLT_NO                                              " & vbNewLine _
        & "                      , M_GOODS.GOODS_CD_NRS                                         " & vbNewLine _
        & "                  ORDER BY                                                           " & vbNewLine _
        & "                      ZAI_SUBQ.ASN_NO                                                " & vbNewLine _
        & "                  ) AS ASN_NO_SEQ                                                    " & vbNewLine _
        & "          FROM                                                                       " & vbNewLine _
        & "              $LM_TRN$..D_ZAI_TSMC AS ZAI_SUBQ                                       " & vbNewLine _
        & "          LEFT JOIN                                                                  " & vbNewLine _
        & "              $LM_MST$..Z_KBN                                                        " & vbNewLine _
        & "                  ON  Z_KBN.KBN_GROUP_CD = 'K038'                                    " & vbNewLine _
        & "                  AND Z_KBN.KBN_NM1 = ZAI_SUBQ.SUPPLY_CD                             " & vbNewLine _
        & "                  AND Z_KBN.SYS_DEL_FLG = '0'                                        " & vbNewLine _
        & "          LEFT JOIN                                                                  " & vbNewLine _
        & "              $LM_MST$..M_GOODS                                                      " & vbNewLine _
        & "                  ON  M_GOODS.NRS_BR_CD = ZAI_SUBQ.NRS_BR_CD                         " & vbNewLine _
        & "                  AND M_GOODS.CUST_CD_L = ZAI_SUBQ.CUST_CD_L                         " & vbNewLine _
        & "                  AND M_GOODS.CUST_CD_M = ZAI_SUBQ.CUST_CD_M                         " & vbNewLine _
        & "                  AND M_GOODS.CUST_CD_S = Z_KBN.KBN_NM3                              " & vbNewLine _
        & "                  AND M_GOODS.CUST_CD_SS = Z_KBN.KBN_NM4                             " & vbNewLine _
        & "                  AND M_GOODS.GOODS_CD_CUST = ZAI_SUBQ.CUST_GOODS_CD                 " & vbNewLine _
        & "                  AND M_GOODS.SYS_DEL_FLG = '0'                                      " & vbNewLine _
        & "          WHERE                                                                      " & vbNewLine _
        & "              ZAI_SUBQ.NRS_BR_CD = SMT.NRS_BR_CD                                     " & vbNewLine _
        & "          AND ZAI_SUBQ.LOT_NO = SMT.LOT_NO                                           " & vbNewLine _
        & "          AND ZAI_SUBQ.CYLINDER_NO = SMT.CYLINDER_NO                                 " & vbNewLine _
        & "          AND ZAI_SUBQ.PLT_NO = SMT.PLT_NO                                           " & vbNewLine _
        & "          AND M_GOODS.GOODS_CD_NRS = SMT.GOODS_CD_NRS                                " & vbNewLine _
        & "          AND CASE RTRIM(ZAI_SUBQ.PLT_NO) WHEN '' THEN ZAI_SUBQ.OUTKA_NO_L ELSE '' END =  " & vbNewLine _
        & "              CASE RTRIM(SMT.PLT_NO) WHEN '' THEN SMT.OUTKA_NO_L ELSE '' END         " & vbNewLine _
        & "          AND ZAI_SUBQ.LAST_INV_DATE = SMT.INV_DATE_TO                               " & vbNewLine _
        & "          AND ZAI_SUBQ.SYS_DEL_FLG = '0'                                             " & vbNewLine _
        & "          ) ZAI_SUBQ_MAIN                                                            " & vbNewLine _
        & "      WHERE                                                                          " & vbNewLine _
        & "          ZAI_SUBQ_MAIN.ASN_NO_SEQ = SMT.ASN_NO_SEQ                                  " & vbNewLine _
        & "      )                                                                              " & vbNewLine _
        & "  LEFT JOIN                                                                          " & vbNewLine _
        & "    (                                                                                " & vbNewLine _
        & "      SELECT                                                                         " & vbNewLine _
        & "         TN1.NRS_BR_CD                                                               " & vbNewLine _
        & "        ,TN1.CUST_CD_L                                                               " & vbNewLine _
        & "        ,TN1.CUST_CD_M                                                               " & vbNewLine _
        & "        ,TN1.UP_GP_CD_1                                                              " & vbNewLine _
        & "        ,TN1.STR_DATE                                                                " & vbNewLine _
        & "        ,(SELECT                                                                     " & vbNewLine _
        & "            ISNULL(MIN(TN2.STR_DATE), '99999999')                                    " & vbNewLine _
        & "          FROM                                                                       " & vbNewLine _
        & "            $LM_MST$..M_TANKA AS TN2                                                 " & vbNewLine _
        & "          WHERE                                                                      " & vbNewLine _
        & "                TN2.NRS_BR_CD = TN1.NRS_BR_CD                                        " & vbNewLine _
        & "            AND TN2.CUST_CD_L = TN1.CUST_CD_L                                        " & vbNewLine _
        & "            AND TN2.CUST_CD_M = TN1.CUST_CD_M                                        " & vbNewLine _
        & "            AND TN2.UP_GP_CD_1 = TN1.UP_GP_CD_1                                      " & vbNewLine _
        & "            AND TN2.STR_DATE > TN1.STR_DATE                                          " & vbNewLine _
        & "            AND TN2.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "         ) AS NEXT_STR_DATE                                                          " & vbNewLine _
        & "        ,TN1.STORAGE_1                                                               " & vbNewLine _
        & "        ,TN1.STORAGE_2                                                               " & vbNewLine _
        & "        ,TN1.HANDLING_IN                                                             " & vbNewLine _
        & "      FROM                                                                           " & vbNewLine _
        & "        $LM_MST$..M_TANKA AS TN1                                                     " & vbNewLine _
        & "      WHERE                                                                          " & vbNewLine _
        & "        TN1.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
        & "    ) AS TNK                                                                         " & vbNewLine _
        & "    ON                                                                               " & vbNewLine _
        & "          TNK.NRS_BR_CD = GOD.NRS_BR_CD                                              " & vbNewLine _
        & "      AND TNK.CUST_CD_L = GOD.CUST_CD_L                                              " & vbNewLine _
        & "      AND TNK.CUST_CD_M = GOD.CUST_CD_M                                              " & vbNewLine _
        & "      AND TNK.UP_GP_CD_1 = GOD.UP_GP_CD_1                                            " & vbNewLine _
        & "      AND TNK.STR_DATE <= SMT.INV_DATE_TO                                            " & vbNewLine _
        & "      AND TNK.NEXT_STR_DATE > SMT.INV_DATE_TO                                        " & vbNewLine _
        & "WHERE                                                                                " & vbNewLine _
        & "      SMT.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
        & "  AND SMT.JOB_NO = @JOB_NO                                                           " & vbNewLine _
        & "ORDER BY                                                                             " & vbNewLine _
        & "-- TSMC Material Number, Vendor code, 入荷日, 出荷日, 空入荷日, 空出荷日             " & vbNewLine _
        & "  2, 8, 10, 11, 12, 13                                                               " & vbNewLine

    ''' <summary>
    ''' 印刷データ検索（詳細）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DETAIL As String = "" _
        & "SELECT                                                                               " & vbNewLine _
        & "   SMT.PLT_NO                                                                        " & vbNewLine _
        & "  ,ZAI.GRLVL1_PPNID                                                                  " & vbNewLine _
        & "  ,GOD.GOODS_CD_CUST                                                                 " & vbNewLine _
        & "  ,GOD.GOODS_NM_1 AS GOODS_NM                                                        " & vbNewLine _
        & "  ,KB1.KBN_NM1 AS PACKAGING                                                          " & vbNewLine _
        & "  ,CASE SMT.RETURN_FLAG WHEN '00' THEN 'One way' WHEN '01' THEN 'Round' ELSE '' END AS ROUND_OR_ONE_WAY " & vbNewLine _
        & "  ,KB2.KBN_NM1 AS VENDOR_CD                                                          " & vbNewLine _
        & "  ,KB2.KBN_NM2 AS VENDOR                                                             " & vbNewLine _
        & "  ,CASE WHEN ZAI.INKA_DATE = '' THEN '-'                                             " & vbNewLine _
        & "        ELSE CONVERT(varchar(10), CONVERT(datetime, ZAI.INKA_DATE), 111)             " & vbNewLine _
        & "        END AS INKA_DATE                                                             " & vbNewLine _
        & "  ,CASE WHEN ZAI.OUTKA_PLAN_DATE = '' THEN '-'                                       " & vbNewLine _
        & "        ELSE CONVERT(varchar(10), CONVERT(datetime, ZAI.OUTKA_PLAN_DATE), 111)       " & vbNewLine _
        & "        END AS OUTKA_PLAN_DATE                                                       " & vbNewLine _
        & "  ,CASE WHEN ZAI.RTN_INKA_DATE = '' THEN '-'                                         " & vbNewLine _
        & "        ELSE CONVERT(varchar(10), CONVERT(datetime, ZAI.RTN_INKA_DATE), 111)         " & vbNewLine _
        & "        END AS RTN_INKA_DATE                                                         " & vbNewLine _
        & "  ,CASE WHEN ZAI.RTN_OUTKA_PLAN_DATE = '' THEN '-'                                   " & vbNewLine _
        & "        ELSE CONVERT(varchar(10), CONVERT(datetime, ZAI.RTN_OUTKA_PLAN_DATE), 111)   " & vbNewLine _
        & "        END AS RTN_OUTKA_PLAN_DATE                                                   " & vbNewLine _
        & "  ,DATEDIFF(day, ZAI.INKA_DATE, ZAI.OUTKA_PLAN_DATE) + 1 AS STORAGE_DATE             " & vbNewLine _
        & "  ,SMT.SET_CLC_DATE                                                                  " & vbNewLine _
        & "  ,SMT.OVER_DATE                                                                     " & vbNewLine _
        & "  ,SMT.LOT_NO                                                                        " & vbNewLine _
        & "  ,ZAI.ASN_NO                                                                        " & vbNewLine _
        & "FROM                                                                                 " & vbNewLine _
        & "       (                                                                             " & vbNewLine _
        & "        SELECT                                                                       " & vbNewLine _
        & "              *                                                                      " & vbNewLine _
        & "            , ROW_NUMBER() OVER (                                                    " & vbNewLine _
        & "                PARTITION BY                                                         " & vbNewLine _
        & "                      NRS_BR_CD                                                      " & vbNewLine _
        & "                    , OUTKA_NO_L                                                     " & vbNewLine _
        & "                    , LOT_NO                                                         " & vbNewLine _
        & "                    , CYLINDER_NO                                                    " & vbNewLine _
        & "                    , PLT_NO                                                         " & vbNewLine _
        & "                    , GOODS_CD_NRS                                                   " & vbNewLine _
        & "                ORDER BY                                                             " & vbNewLine _
        & "                    REC_NO                                                           " & vbNewLine _
        & "                ) AS ASN_NO_SEQ                                                      " & vbNewLine _
        & "        FROM                                                                         " & vbNewLine _
        & "            $LM_TRN$..I_SEKY_MEISAI_TSMC                                             " & vbNewLine _
        & "        WHERE                                                                        " & vbNewLine _
        & "            NRS_BR_CD = @NRS_BR_CD                                                   " & vbNewLine _
        & "        AND JOB_NO = @JOB_NO                                                         " & vbNewLine _
        & "        ) AS SMT                                                                     " & vbNewLine _
        & "  LEFT JOIN                                                                          " & vbNewLine _
        & "       (                                                                             " & vbNewLine _
        & "        SELECT                                                                       " & vbNewLine _
        & "              D_ZAI_TSMC.NRS_BR_CD                                                   " & vbNewLine _
        & "            , D_ZAI_TSMC.CUST_CD_L                                                   " & vbNewLine _
        & "            , D_ZAI_TSMC.CUST_CD_M                                                   " & vbNewLine _
        & "            , D_ZAI_TSMC.OUTKA_NO_L                                                  " & vbNewLine _
        & "            , D_ZAI_TSMC.INKA_DATE                                                   " & vbNewLine _
        & "            , D_ZAI_TSMC.OUTKA_PLAN_DATE                                             " & vbNewLine _
        & "            , D_ZAI_TSMC.RTN_INKA_DATE                                               " & vbNewLine _
        & "            , D_ZAI_TSMC.RTN_OUTKA_PLAN_DATE                                         " & vbNewLine _
        & "            , D_ZAI_TSMC.LAST_INV_DATE                                               " & vbNewLine _
        & "            , D_ZAI_TSMC.ASN_NO                                                      " & vbNewLine _
        & "            , D_ZAI_TSMC.CYLINDER_NO                                                 " & vbNewLine _
        & "            , D_ZAI_TSMC.PLT_NO                                                      " & vbNewLine _
        & "            , D_ZAI_TSMC.GRLVL1_PPNID                                                " & vbNewLine _
        & "            , D_ZAI_TSMC.SYS_DEL_FLG                                                 " & vbNewLine _
        & "            , M_GOODS.GOODS_CD_NRS                                                   " & vbNewLine _
        & "        FROM                                                                         " & vbNewLine _
        & "            $LM_TRN$..D_ZAI_TSMC                                                     " & vbNewLine _
        & "        LEFT JOIN                                                                    " & vbNewLine _
        & "            LM_MST..Z_KBN                                                            " & vbNewLine _
        & "                ON  Z_KBN.KBN_GROUP_CD = 'K038'                                      " & vbNewLine _
        & "                AND Z_KBN.KBN_NM1 = D_ZAI_TSMC.SUPPLY_CD                             " & vbNewLine _
        & "                AND Z_KBN.SYS_DEL_FLG = '0'                                          " & vbNewLine _
        & "        LEFT JOIN                                                                    " & vbNewLine _
        & "            $LM_MST$..M_GOODS                                                        " & vbNewLine _
        & "                ON  M_GOODS.NRS_BR_CD = D_ZAI_TSMC.NRS_BR_CD                         " & vbNewLine _
        & "                AND M_GOODS.CUST_CD_L = D_ZAI_TSMC.CUST_CD_L                         " & vbNewLine _
        & "                AND M_GOODS.CUST_CD_M = D_ZAI_TSMC.CUST_CD_M                         " & vbNewLine _
        & "                AND M_GOODS.CUST_CD_S = Z_KBN.KBN_NM3                                " & vbNewLine _
        & "                AND M_GOODS.CUST_CD_SS = Z_KBN.KBN_NM4                               " & vbNewLine _
        & "                AND M_GOODS.GOODS_CD_CUST = D_ZAI_TSMC.CUST_GOODS_CD                 " & vbNewLine _
        & "                AND M_GOODS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
        & "        WHERE                                                                        " & vbNewLine _
        & "            M_GOODS.GOODS_CD_NRS IS NOT NULL                                         " & vbNewLine _
        & "        ) AS ZAI                                                                     " & vbNewLine _
        & "    ON                                                                               " & vbNewLine _
        & "          ZAI.NRS_BR_CD = SMT.NRS_BR_CD                                              " & vbNewLine _
        & "      AND CASE RTRIM(ZAI.PLT_NO) WHEN '' THEN ZAI.OUTKA_NO_L ELSE '' END =           " & vbNewLine _
        & "          CASE RTRIM(SMT.PLT_NO) WHEN '' THEN SMT.OUTKA_NO_L ELSE '' END             " & vbNewLine _
        & "      AND ZAI.LAST_INV_DATE = SMT.INV_DATE_TO                                        " & vbNewLine _
        & "      AND ZAI.ASN_NO = (                                                             " & vbNewLine _
        & "        SELECT TOP 1                                                                 " & vbNewLine _
        & "            ASN_NO                                                                   " & vbNewLine _
        & "        FROM (                                                                       " & vbNewLine _
        & "            SELECT                                                                   " & vbNewLine _
        & "                  ZAI_SUBQ.ASN_NO                                                    " & vbNewLine _
        & "                , DENSE_RANK() OVER(                                                 " & vbNewLine _
        & "                    PARTITION BY                                                     " & vbNewLine _
        & "                          ZAI_SUBQ.NRS_BR_CD                                         " & vbNewLine _
        & "                        , ZAI_SUBQ.OUTKA_NO_L                                        " & vbNewLine _
        & "                        , ZAI_SUBQ.LOT_NO                                            " & vbNewLine _
        & "                        , ZAI_SUBQ.CYLINDER_NO                                       " & vbNewLine _
        & "                        , ZAI_SUBQ.PLT_NO                                            " & vbNewLine _
        & "                        , M_GOODS.GOODS_CD_NRS                                       " & vbNewLine _
        & "                    ORDER BY                                                         " & vbNewLine _
        & "                        ZAI_SUBQ.ASN_NO                                              " & vbNewLine _
        & "                    ) AS ASN_NO_SEQ                                                  " & vbNewLine _
        & "            FROM                                                                     " & vbNewLine _
        & "                $LM_TRN$..D_ZAI_TSMC AS ZAI_SUBQ                                     " & vbNewLine _
        & "            LEFT JOIN                                                                " & vbNewLine _
        & "                $LM_MST$..Z_KBN                                                      " & vbNewLine _
        & "                    ON  Z_KBN.KBN_GROUP_CD = 'K038'                                  " & vbNewLine _
        & "                    AND Z_KBN.KBN_NM1 = ZAI_SUBQ.SUPPLY_CD                           " & vbNewLine _
        & "                    AND Z_KBN.SYS_DEL_FLG = '0'                                      " & vbNewLine _
        & "            LEFT JOIN                                                                " & vbNewLine _
        & "                $LM_MST$..M_GOODS                                                    " & vbNewLine _
        & "                    ON  M_GOODS.NRS_BR_CD = ZAI_SUBQ.NRS_BR_CD                       " & vbNewLine _
        & "                    AND M_GOODS.CUST_CD_L = ZAI_SUBQ.CUST_CD_L                       " & vbNewLine _
        & "                    AND M_GOODS.CUST_CD_M = ZAI_SUBQ.CUST_CD_M                       " & vbNewLine _
        & "                    AND M_GOODS.CUST_CD_S = Z_KBN.KBN_NM3                            " & vbNewLine _
        & "                    AND M_GOODS.CUST_CD_SS = Z_KBN.KBN_NM4                           " & vbNewLine _
        & "                    AND M_GOODS.GOODS_CD_CUST = ZAI_SUBQ.CUST_GOODS_CD               " & vbNewLine _
        & "                    AND M_GOODS.SYS_DEL_FLG = '0'                                    " & vbNewLine _
        & "            WHERE                                                                    " & vbNewLine _
        & "                ZAI_SUBQ.NRS_BR_CD = SMT.NRS_BR_CD                                   " & vbNewLine _
        & "            AND ZAI_SUBQ.LOT_NO = SMT.LOT_NO                                         " & vbNewLine _
        & "            AND ZAI_SUBQ.CYLINDER_NO = SMT.CYLINDER_NO                               " & vbNewLine _
        & "            AND ZAI_SUBQ.PLT_NO = SMT.PLT_NO                                         " & vbNewLine _
        & "            AND M_GOODS.GOODS_CD_NRS = SMT.GOODS_CD_NRS                              " & vbNewLine _
        & "            AND CASE RTRIM(ZAI_SUBQ.PLT_NO) WHEN '' THEN ZAI_SUBQ.OUTKA_NO_L ELSE '' END =  " & vbNewLine _
        & "                CASE RTRIM(SMT.PLT_NO) WHEN '' THEN SMT.OUTKA_NO_L ELSE '' END       " & vbNewLine _
        & "            AND ZAI_SUBQ.LAST_INV_DATE = SMT.INV_DATE_TO                             " & vbNewLine _
        & "            AND ZAI_SUBQ.SYS_DEL_FLG = '0'                                           " & vbNewLine _
        & "            ) ZAI_SUBQ_MAIN                                                          " & vbNewLine _
        & "        WHERE                                                                        " & vbNewLine _
        & "            ZAI_SUBQ_MAIN.ASN_NO_SEQ = SMT.ASN_NO_SEQ                                " & vbNewLine _
        & "        )                                                                            " & vbNewLine _
        & "      AND ZAI.CYLINDER_NO = SMT.CYLINDER_NO                                          " & vbNewLine _
        & "      AND ZAI.PLT_NO = SMT.PLT_NO                                                    " & vbNewLine _
        & "      AND ZAI.GOODS_CD_NRS = SMT.GOODS_CD_NRS                                        " & vbNewLine _
        & "      AND ZAI.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN                                                                          " & vbNewLine _
        & "    $LM_MST$..M_GOODS AS GOD                                                         " & vbNewLine _
        & "    ON                                                                               " & vbNewLine _
        & "          GOD.NRS_BR_CD = SMT.NRS_BR_CD                                              " & vbNewLine _
        & "      AND GOD.GOODS_CD_NRS = SMT.GOODS_CD_NRS                                        " & vbNewLine _
        & "      AND GOD.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN                                                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB1                                                           " & vbNewLine _
        & "    ON                                                                               " & vbNewLine _
        & "          KB1.KBN_GROUP_CD = 'K039'                                                  " & vbNewLine _
        & "      AND KB1.KBN_CD = GOD.PKG_UT                                                    " & vbNewLine _
        & "      AND KB1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN                                                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB2                                                           " & vbNewLine _
        & "    ON                                                                               " & vbNewLine _
        & "          KB2.KBN_GROUP_CD = 'K038'                                                  " & vbNewLine _
        & "      AND KB2.KBN_NM3 = GOD.CUST_CD_S                                                " & vbNewLine _
        & "      AND KB2.KBN_NM4 = GOD.CUST_CD_SS                                               " & vbNewLine _
        & "      AND KB2.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "WHERE                                                                                " & vbNewLine _
        & "      SMT.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
        & "  AND SMT.JOB_NO = @JOB_NO                                                           " & vbNewLine _
        & "ORDER BY                                                                             " & vbNewLine _
        & "-- TSMC Material Number, Vendor code, 入荷日, 出荷日, 空入荷日, 空出荷日             " & vbNewLine _
        & "   3, 7, 9, 10, 11, 12                                                               " & vbNewLine _

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
    ''' 印刷データ検索（ヘッダ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataHead(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI580SET")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI580DAC.SQL_SELECT_HEAD)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", Me._Row.Item("JOB_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウト対策
        cmd.CommandTimeout = 6000

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI580DAC", "SelectDataHead", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("INV_DATE_FROM", "INV_DATE_FROM")
        map.Add("INV_DATE_TO", "INV_DATE_TO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI580OUT_1")

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ検索（メイン）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMain(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI580SET")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI580DAC.SQL_SELECT_MAIN)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", Me._Row.Item("JOB_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウト対策
        cmd.CommandTimeout = 6000

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI580DAC", "SelectDataMain", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PLT_CYLINDER", "PLT_CYLINDER")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("QUANTITY", "QUANTITY")
        map.Add("PACKAGING", "PACKAGING")
        map.Add("ROUND_OR_ONE_WAY", "ROUND_OR_ONE_WAY")
        map.Add("VENDOR_CD", "VENDOR_CD")
        map.Add("VENDOR", "VENDOR")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("RTN_INKA_DATE", "RTN_INKA_DATE")
        map.Add("RTN_OUTKA_PLAN_DATE", "RTN_OUTKA_PLAN_DATE")
        map.Add("STORAGE_DATE", "STORAGE_DATE")
        map.Add("SET_CLC_DATE", "SET_CLC_DATE")
        map.Add("OVER_DATE", "OVER_DATE")
        map.Add("SET_AMO", "SET_AMO")
        map.Add("OVER_TANKA", "OVER_TANKA")
        map.Add("SET_OVER_AMO", "SET_OVER_AMO")
        map.Add("SET_AMO_TTL", "SET_AMO_TTL")
        map.Add("ASN_NO", "ASN_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI580OUT_2")

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ検索（詳細）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataDetail(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI580SET")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI580DAC.SQL_SELECT_DETAIL)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", Me._Row.Item("JOB_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウト対策
        cmd.CommandTimeout = 6000

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI580DAC", "SelectDataDetail", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PLT_NO", "PLT_NO")
        map.Add("GRLVL1_PPNID", "GRLVL1_PPNID")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("PACKAGING", "PACKAGING")
        map.Add("ROUND_OR_ONE_WAY", "ROUND_OR_ONE_WAY")
        map.Add("VENDOR_CD", "VENDOR_CD")
        map.Add("VENDOR", "VENDOR")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("RTN_INKA_DATE", "RTN_INKA_DATE")
        map.Add("RTN_OUTKA_PLAN_DATE", "RTN_OUTKA_PLAN_DATE")
        map.Add("STORAGE_DATE", "STORAGE_DATE")
        map.Add("SET_CLC_DATE", "SET_CLC_DATE")
        map.Add("OVER_DATE", "OVER_DATE")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("ASN_NO", "ASN_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI580OUT_3")

        Return ds

    End Function

#End Region

#Region "共通"

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
