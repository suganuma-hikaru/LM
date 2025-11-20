' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC550    : 荷札
'  作  成  者       :  [shinohara]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC550DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC550DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "運送"

    ''' <summary>
    ''' 帳票種別取得用(運送テーブル参照)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT_UNSO As String = "SELECT DISTINCT                                                               " & vbNewLine _
                                                 & "       F02_01.NRS_BR_CD                                       AS NRS_BR_CD    " & vbNewLine _
                                                 & "      ,'10'                                                   AS PTN_ID       " & vbNewLine _
                                                 & "      ,CASE WHEN M22_02.PTN_CD IS NOT NULL THEN M22_02.PTN_CD                 " & vbNewLine _
                                                 & "            WHEN M22_01.PTN_CD IS NOT NULL THEN M22_01.PTN_CD                 " & vbNewLine _
                                                 & "            ELSE M22_03.PTN_CD END                            AS PTN_CD       " & vbNewLine _
                                                 & "      ,CASE WHEN M22_02.PTN_CD IS NOT NULL THEN M22_02.RPT_ID                 " & vbNewLine _
                                                 & "            WHEN M22_01.PTN_CD IS NOT NULL THEN M22_01.RPT_ID                 " & vbNewLine _
                                                 & "            ELSE M22_03.RPT_ID END                            AS RPT_ID       " & vbNewLine _
                                                 & "  FROM      $LM_TRN$..F_UNSO_L   F02_01                                       " & vbNewLine


    ''' <summary>
    ''' 運送データ取得1
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNSO_DATA As String = "SELECT DISTINCT                                                                   " & vbNewLine _
                                                 & "   CASE  WHEN M22_02.PTN_CD IS NOT NULL THEN M22_02.RPT_ID                        " & vbNewLine _
                                                 & "         WHEN M22_01.PTN_CD IS NOT NULL THEN M22_01.RPT_ID                        " & vbNewLine _
                                                 & "         ELSE M22_03.RPT_ID END                            AS RPT_ID              " & vbNewLine _
                                                 & "        ,M10_01.DEST_NM                                    AS DEST_NM             " & vbNewLine _
                                                 & "        ,F02_01.UNSO_PKG_NB                                AS OUTKA_PKG_NB        " & vbNewLine _
                                                 & "        ,M10_01.AD_1                                       AS AD_1                " & vbNewLine _
                                                 & "        ,M10_01.AD_2                                       AS AD_2                " & vbNewLine _
                                                 & "        ,F02_01.AD_3                                       AS AD_3                " & vbNewLine _
                                                 & "        ,M07_01.CUST_NM_L                                  AS CUST_NM_L           " & vbNewLine _
                                                 & "        ,M36_01.UNSOCO_NM                                  AS UNSOCO_NM           " & vbNewLine _
                                                 & "        ,F02_01.ARR_PLAN_DATE                              AS ARR_PLAN_DATE       " & vbNewLine _
                                                 & "        ,F02_01.ARR_PLAN_TIME                              AS ARR_PLAN_TIME       " & vbNewLine _
                                                 & "        ,M01_01.NRS_BR_NM                                  AS NRS_BR_NM           " & vbNewLine _
                                                 & "        ,F02_01.UNSO_NO_L                                  AS OUTKA_NO_L          " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                               " & vbNewLine _
                                                 & "--      ,M01_01.TEL                                        AS TEL                 " & vbNewLine _
                                                 & "        ,CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1                  " & vbNewLine _
                                                 & "         ELSE M01_01.TEL                                                          " & vbNewLine _
                                                 & "         END                                               AS TEL                 " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                               " & vbNewLine _
                                                 & "        ,M01_01.FAX                                        AS FAX                 " & vbNewLine _
                                                 & "        ,M07_01.CUST_NM_S                                  AS CUST_NM_S           " & vbNewLine _
                                                 & "        ,F02_01.PC_KB                                      AS PC_KB               " & vbNewLine _
                                                 & "        ,SHIPDEST.DEST_NM                                  AS SHIP_NM_L           " & vbNewLine _
                                                 & "        ,CASE WHEN SHIPDEST.DEST_NM <> ''                                         " & vbNewLine _
                                                 & "                   AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM         " & vbNewLine _
                                                 & "              WHEN DENPYO_NM   <> ''                                              " & vbNewLine _
                                                 & "                   AND M07_01.DENPYO_NM IS NOT NULL   THEN M07_01.DENPYO_NM       " & vbNewLine _
                                                 & "              ELSE M07_01.CUST_NM_L                                               " & vbNewLine _
                                                 & "         END                     AS ATSUKAISYA_NM                                 " & vbNewLine _
                                                 & "        ,M08_01.GOODS_CD_CUST    AS GOODS_CD_CUST                                 " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_1    " & vbNewLine _
                                                 & "              ELSE F03_01.GOODS_NM                                                " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_1                                    " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_2    " & vbNewLine _
                                                 & "              ELSE F03_01.GOODS_NM                                                " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_2                                    " & vbNewLine _
                                                 & "        ,''                      AS LT_DATE                                       " & vbNewLine _
                                                 & "        ,F03_01.LOT_NO           AS LOT_NO                                        " & vbNewLine _
                                                 & "        ,M01_01.AD_1             AS NRS_BR_AD_1                                   " & vbNewLine _
                                                 & "        ,M01_01.AD_2             AS NRS_BR_AD_2                                   " & vbNewLine _
                                                 & "        ,M01_01.AD_3             AS NRS_BR_AD_3                                   " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_3    " & vbNewLine _
                                                 & "              ELSE ''                                                             " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_3                                    " & vbNewLine _
                                                 & "        ,''                      AS GOODS_SYUBETU                                 " & vbNewLine _
                                                 & "        --荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加           " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO          AS SET_NAIYO                                     " & vbNewLine _
                                                 & "        --荷主明細マスタ   LMC552(埼玉浮間用)対応追加                             " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO_2          AS SET_NAIYO_2                                 " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO_3          AS SET_NAIYO_3                                 " & vbNewLine _
                                                 & ",       M10_01.TEL                 AS DEST_TEL                                    " & vbNewLine _
                                                 & ",       F02_01.AUTO_DENP_NO        AS AUTO_DENP_NO         -- ADD 2016/06/29      " & vbNewLine _
                                                 & ",       MSZ.SHIWAKE_CD             AS SHIWAKE_CD           -- ADD 2016/06/29      " & vbNewLine _
                                                 & ",       F02_01.CUST_CD_L           AS CUST_CD_L            -- ADD 2016/06/29      " & vbNewLine _
                                                 & ",       F02_01.CUST_REF_NO         AS CUST_ORD_NO          -- ADD 2016/06/29      " & vbNewLine _
                                                 & ",       F02_01.UNSO_CD             AS UNSO_CD              -- ADD 2017/08/31      " & vbNewLine _
                                                 & ",       F02_01.UNSO_BR_CD          AS UNSO_BR_CD           -- ADD 2017/08/31      " & vbNewLine _
                                                 & ",       F02_01.NRS_BR_CD           AS NRS_BR_CD            -- ADD 2017/08/31      " & vbNewLine _
                                                 & ",       ISNULL(MTOLL.CHAKU_CD,'')  AS CHAKU_CD             -- ADD 2017/10/04      " & vbNewLine _
                                                 & ",       ISNULL(MTOLL.CHAKU_NM,'')  AS CHAKU_NM             -- ADD 2017/10/04      " & vbNewLine




    ''' <summary>
    ''' 運送データ取得4
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_FROM_UNSO_DATA4 As String = "            ,''                                                AS TOP_FLAG     " & vbNewLine _
                                                 & "  FROM       $LM_TRN$..F_UNSO_L F02_01                                        " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_NRS_BR M01_01                                        " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M01_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  M01_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_CUST     M07_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M07_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = M07_01.CUST_CD_L                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_M   = M07_01.CUST_CD_M                            " & vbNewLine _
                                                 & "        AND  M07_01.CUST_CD_S   = '00'                                        " & vbNewLine _
                                                 & "        AND  M07_01.CUST_CD_SS  = '00'                                        " & vbNewLine _
                                                 & "        AND  M07_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_DEST     M10_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M10_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = M10_01.CUST_CD_L                            " & vbNewLine _
                                                 & "        AND  F02_01.DEST_CD     = M10_01.DEST_CD                              " & vbNewLine _
                                                 & "        AND  M10_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_UNSOCO   M36_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M36_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.UNSO_CD     = M36_01.UNSOCO_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.UNSO_BR_CD  = M36_01.UNSOCO_BR_CD                         " & vbNewLine _
                                                 & "        AND  M36_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_DEST   SHIPDEST                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = SHIPDEST.NRS_BR_CD                          " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = SHIPDEST.CUST_CD_L                          " & vbNewLine _
                                                 & "        AND  F02_01.SHIP_CD     = SHIPDEST.DEST_CD                            " & vbNewLine _
                                                 & "        AND  SHIPDEST.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加               " & vbNewLine _
                                                 & "  LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                                   " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = MCD01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = MCD01.CUST_CD                              " & vbNewLine _
                                                 & "        AND  MCD01.SUB_KB = '29'                                             " & vbNewLine _
                                                 & "        AND  MCD01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                           " & vbNewLine _
                                                 & "--区分マスタ                                                                  " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..Z_KBN KB2                                              " & vbNewLine _
                                                 & "         ON  KB2.KBN_GROUP_CD = 'N022'                                        " & vbNewLine _
                                                 & "        AND  KB2.KBN_CD  = '00'                                               " & vbNewLine _
                                                 & "        AND  KB2.KBN_NM2 = F02_01.NRS_BR_CD                                   " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                           " & vbNewLine _
                                                 & "--ADD 2016/06/29 西濃ZIP   西濃対応応追加 Start                                           " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..M_SEINO_ZIP MSZ                                                       " & vbNewLine _
                                                 & "ON  MSZ.ZIP_NO   = M10_01.ZIP                                                                " & vbNewLine _
                                                 & "AND MSZ.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                                 & "--ADD 2016/06/29 西濃ZIP   西濃対応応追加 End                                             " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..M_TOLL MTOLL                                                   " & vbNewLine _
                                                 & "ON  MTOLL.JIS_CD   = SUBSTRING(M10_01.JIS,1,5)                                   " & vbNewLine _
                                                 & "AND MTOLL.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                                 & "--ADD 2017/10/04 M_TOLL  トール対応追加 End                                      " & vbNewLine

    ''' <summary>
    ''' 帳票PTN
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_UNSO_PRT As String = "  LEFT JOIN $LM_TRN$..F_UNSO_M   F03_01                                          " & vbNewLine _
                                              & "         ON F02_01.NRS_BR_CD   = F03_01.NRS_BR_CD                                " & vbNewLine _
                                              & "        AND F02_01.UNSO_NO_L   = F03_01.UNSO_NO_L                                " & vbNewLine _
                                              & "        AND F03_01.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_GOODS    M08_01                                          " & vbNewLine _
                                              & "    ON F02_01.NRS_BR_CD        = M08_01.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND F03_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS                             " & vbNewLine _
                                              & "   AND M08_01.SYS_DEL_FLG      = '0'                                             " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_CUST_RPT M64_01                                          " & vbNewLine _
                                              & "    ON F02_01.NRS_BR_CD        = M64_01.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND F02_01.CUST_CD_L        = M64_01.CUST_CD_L                                " & vbNewLine _
                                              & "   AND F02_01.CUST_CD_M        = M64_01.CUST_CD_M                                " & vbNewLine _
                                              & "   AND M64_01.CUST_CD_S        = '00'                                            " & vbNewLine _
                                              & "   AND M64_01.PTN_ID           = '10'                                            " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_RPT      M22_01                                          " & vbNewLine _
                                              & "    ON M64_01.NRS_BR_CD        = M22_01.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND M64_01.PTN_ID           = M22_01.PTN_ID                                   " & vbNewLine _
                                              & "   AND M64_01.PTN_CD           = M22_01.PTN_CD                                   " & vbNewLine _
                                              & "   AND M22_01.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_CUST_RPT M64_02                                          " & vbNewLine _
                                              & "    ON M08_01.NRS_BR_CD        = M64_02.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND M08_01.CUST_CD_L        = M64_02.CUST_CD_L                                " & vbNewLine _
                                              & "   AND M08_01.CUST_CD_M        = M64_02.CUST_CD_M                                " & vbNewLine _
                                              & "   AND M08_01.CUST_CD_S        = M64_02.CUST_CD_S                                " & vbNewLine _
                                              & "   AND M64_02.PTN_ID           = '10'                                            " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_RPT      M22_02                                          " & vbNewLine _
                                              & "    ON M64_02.NRS_BR_CD        = M22_02.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND M64_02.PTN_ID           = M22_02.PTN_ID                                   " & vbNewLine _
                                              & "   AND M64_02.PTN_CD           = M22_02.PTN_CD                                   " & vbNewLine _
                                              & "   AND M22_02.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_RPT M22_03                                               " & vbNewLine _
                                              & "    ON F02_01.NRS_BR_CD        = M22_03.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND M22_03.PTN_ID           = '10'                                            " & vbNewLine _
                                              & "   AND M22_03.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                              & "   AND M22_03.STANDARD_FLAG    = '01'                                            " & vbNewLine

    ''' <summary>
    ''' 運送抽出条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_UNSO_DATA As String = "      WHERE  F02_01.UNSO_NO_L   = @OUTKA_NO_L                                  " & vbNewLine _
                                                & "        AND  F02_01.SYS_DEL_FLG = '0'                                          " & vbNewLine

    ''' <summary>
    ''' 運送データ取得1
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNSO_DATA_LMC557 As String = "SELECT DISTINCT                                                                   " & vbNewLine _
                                                 & "   CASE  WHEN M22_02.PTN_CD IS NOT NULL THEN M22_02.RPT_ID                        " & vbNewLine _
                                                 & "         WHEN M22_01.PTN_CD IS NOT NULL THEN M22_01.RPT_ID                        " & vbNewLine _
                                                 & "         ELSE M22_03.RPT_ID END                            AS RPT_ID              " & vbNewLine _
                                                 & "        ,M10_01.DEST_NM                                    AS DEST_NM             " & vbNewLine _
                                                 & "        ,F02_01.UNSO_PKG_NB                                AS OUTKA_PKG_NB        " & vbNewLine _
                                                 & "        ,M10_01.AD_1                                       AS AD_1                " & vbNewLine _
                                                 & "        ,M10_01.AD_2                                       AS AD_2                " & vbNewLine _
                                                 & "        ,F02_01.AD_3                                       AS AD_3                " & vbNewLine _
                                                 & "        ,M07_01.CUST_NM_L                                  AS CUST_NM_L           " & vbNewLine _
                                                 & "        ,M36_01.UNSOCO_NM                                  AS UNSOCO_NM           " & vbNewLine _
                                                 & "        ,F02_01.ARR_PLAN_DATE                              AS ARR_PLAN_DATE       " & vbNewLine _
                                                 & "        ,F02_01.ARR_PLAN_TIME                              AS ARR_PLAN_TIME       " & vbNewLine _
                                                 & "        ,M01_01.NRS_BR_NM                                  AS NRS_BR_NM           " & vbNewLine _
                                                 & "        ,F02_01.UNSO_NO_L                                  AS OUTKA_NO_L          " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                               " & vbNewLine _
                                                 & "--      ,M01_01.TEL                                        AS TEL                 " & vbNewLine _
                                                 & "        ,CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1                  " & vbNewLine _
                                                 & "         ELSE M01_01.TEL                                                          " & vbNewLine _
                                                 & "         END                                               AS TEL                 " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                               " & vbNewLine _
                                                 & "        ,M01_01.FAX                                        AS FAX                 " & vbNewLine _
                                                 & "        ,M07_01.CUST_NM_S                                  AS CUST_NM_S           " & vbNewLine _
                                                 & "        ,F02_01.PC_KB                                      AS PC_KB               " & vbNewLine _
                                                 & "        ,SHIPDEST.DEST_NM                                  AS SHIP_NM_L           " & vbNewLine _
                                                 & "        ,CASE WHEN SHIPDEST.DEST_NM <> ''                                         " & vbNewLine _
                                                 & "                   AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM         " & vbNewLine _
                                                 & "              WHEN DENPYO_NM   <> ''                                              " & vbNewLine _
                                                 & "                   AND M07_01.DENPYO_NM IS NOT NULL   THEN M07_01.DENPYO_NM       " & vbNewLine _
                                                 & "              ELSE M07_01.CUST_NM_L                                               " & vbNewLine _
                                                 & "         END                     AS ATSUKAISYA_NM                                 " & vbNewLine _
                                                 & "        ,M08_01.GOODS_CD_CUST    AS GOODS_CD_CUST                                 " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_1    " & vbNewLine _
                                                 & "              ELSE F03_01.GOODS_NM                                                " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_1                                    " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_2    " & vbNewLine _
                                                 & "              ELSE F03_01.GOODS_NM                                                " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_2                                    " & vbNewLine _
                                                 & "        ,''                      AS LT_DATE                                       " & vbNewLine _
                                                 & "        ,F03_01.LOT_NO           AS LOT_NO                                        " & vbNewLine _
                                                 & "        ,M01_01.AD_1             AS NRS_BR_AD_1                                   " & vbNewLine _
                                                 & "        ,M01_01.AD_2             AS NRS_BR_AD_2                                   " & vbNewLine _
                                                 & "        ,M01_01.AD_3             AS NRS_BR_AD_3                                   " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_3    " & vbNewLine _
                                                 & "              ELSE ''                                                             " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_3                                    " & vbNewLine _
                                                 & "        ,''                      AS GOODS_SYUBETU                                 " & vbNewLine _
                                                 & "        --荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加           " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO          AS SET_NAIYO                                     " & vbNewLine _
                                                 & "        --荷主明細マスタ   LMC552(埼玉浮間用)対応追加                             " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO_2          AS SET_NAIYO_2                                 " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO_3          AS SET_NAIYO_3                                 " & vbNewLine _
                                                 & ",       M10_01.TEL                 AS DEST_TEL                                    " & vbNewLine _
                                                 & "        --LMC557 千葉 ITW(荷主大：00555)                                          " & vbNewLine _
                                                 & ",       ''                      AS ALCTD_NB                                       " & vbNewLine _
                                                 & ",       ''                      AS PRINT_SORT                                     " & vbNewLine _
                                                 & ",       F03_01.UNSO_TTL_NB      AS UNSO_TTL_NB                                    " & vbNewLine _
                                                 & ",       ''                      AS CUST_ORD_NO                                    " & vbNewLine





    ''' <summary>
    ''' 運送データ取得4
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_FROM_UNSO_DATA4_LMC557 As String = "            ,''                                                AS TOP_FLAG     " & vbNewLine _
                                                 & "  FROM       $LM_TRN$..F_UNSO_L F02_01                                        " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_NRS_BR M01_01                                        " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M01_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  M01_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_CUST     M07_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M07_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = M07_01.CUST_CD_L                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_M   = M07_01.CUST_CD_M                            " & vbNewLine _
                                                 & "        AND  M07_01.CUST_CD_S   = '00'                                        " & vbNewLine _
                                                 & "        AND  M07_01.CUST_CD_SS  = '00'                                        " & vbNewLine _
                                                 & "        AND  M07_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_DEST     M10_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M10_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = M10_01.CUST_CD_L                            " & vbNewLine _
                                                 & "        AND  F02_01.DEST_CD     = M10_01.DEST_CD                              " & vbNewLine _
                                                 & "        AND  M10_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_UNSOCO   M36_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M36_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.UNSO_CD     = M36_01.UNSOCO_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.UNSO_BR_CD  = M36_01.UNSOCO_BR_CD                         " & vbNewLine _
                                                 & "        AND  M36_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_DEST   SHIPDEST                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = SHIPDEST.NRS_BR_CD                          " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = SHIPDEST.CUST_CD_L                          " & vbNewLine _
                                                 & "        AND  F02_01.SHIP_CD     = SHIPDEST.DEST_CD                            " & vbNewLine _
                                                 & "        AND  SHIPDEST.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加               " & vbNewLine _
                                                 & "  LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                                   " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = MCD01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = MCD01.CUST_CD                              " & vbNewLine _
                                                 & "        AND  MCD01.SUB_KB = '29'                                             " & vbNewLine _
                                                 & "        AND  MCD01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                           " & vbNewLine _
                                                 & "--区分マスタ                                                                  " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..Z_KBN KB2                                              " & vbNewLine _
                                                 & "         ON  KB2.KBN_GROUP_CD = 'N022'                                        " & vbNewLine _
                                                 & "        AND  KB2.KBN_CD  = '00'                                               " & vbNewLine _
                                                 & "        AND  KB2.KBN_NM2 = F02_01.NRS_BR_CD                                   " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                           " & vbNewLine


    ''' <summary>
    ''' 帳票PTN
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_UNSO_PRT_LMC557 As String = "  LEFT JOIN $LM_TRN$..F_UNSO_M   F03_01                                          " & vbNewLine _
                                              & "         ON F02_01.NRS_BR_CD   = F03_01.NRS_BR_CD                                " & vbNewLine _
                                              & "        AND F02_01.UNSO_NO_L   = F03_01.UNSO_NO_L                                " & vbNewLine _
                                              & "        AND F03_01.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_GOODS    M08_01                                          " & vbNewLine _
                                              & "    ON F02_01.NRS_BR_CD        = M08_01.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND F03_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS                             " & vbNewLine _
                                              & "   AND M08_01.SYS_DEL_FLG      = '0'                                             " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_CUST_RPT M64_01                                          " & vbNewLine _
                                              & "    ON F02_01.NRS_BR_CD        = M64_01.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND F02_01.CUST_CD_L        = M64_01.CUST_CD_L                                " & vbNewLine _
                                              & "   AND F02_01.CUST_CD_M        = M64_01.CUST_CD_M                                " & vbNewLine _
                                              & "   AND M64_01.CUST_CD_S        = '00'                                            " & vbNewLine _
                                              & "   AND M64_01.PTN_ID           = '10'                                            " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_RPT      M22_01                                          " & vbNewLine _
                                              & "    ON M64_01.NRS_BR_CD        = M22_01.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND M64_01.PTN_ID           = M22_01.PTN_ID                                   " & vbNewLine _
                                              & "   AND M64_01.PTN_CD           = M22_01.PTN_CD                                   " & vbNewLine _
                                              & "   AND M22_01.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_CUST_RPT M64_02                                          " & vbNewLine _
                                              & "    ON M08_01.NRS_BR_CD        = M64_02.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND M08_01.CUST_CD_L        = M64_02.CUST_CD_L                                " & vbNewLine _
                                              & "   AND M08_01.CUST_CD_M        = M64_02.CUST_CD_M                                " & vbNewLine _
                                              & "   AND M08_01.CUST_CD_S        = M64_02.CUST_CD_S                                " & vbNewLine _
                                              & "   AND M64_02.PTN_ID           = '10'                                            " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_RPT      M22_02                                          " & vbNewLine _
                                              & "    ON M64_02.NRS_BR_CD        = M22_02.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND M64_02.PTN_ID           = M22_02.PTN_ID                                   " & vbNewLine _
                                              & "   AND M64_02.PTN_CD           = M22_02.PTN_CD                                   " & vbNewLine _
                                              & "   AND M22_02.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                              & "  LEFT JOIN $LM_MST$..M_RPT M22_03                                               " & vbNewLine _
                                              & "    ON F02_01.NRS_BR_CD        = M22_03.NRS_BR_CD                                " & vbNewLine _
                                              & "   AND M22_03.PTN_ID           = '10'                                            " & vbNewLine _
                                              & "   AND M22_03.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                              & "   AND M22_03.STANDARD_FLAG    = '01'                                            " & vbNewLine

    ''' <summary>
    ''' 運送抽出条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_UNSO_DATA_LMC557 As String = "      WHERE  F02_01.UNSO_NO_L   = @OUTKA_NO_L                                  " & vbNewLine _
                                                & "        AND  F02_01.SYS_DEL_FLG = '0'                                          " & vbNewLine


#End Region

#Region "出荷"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                                    " & vbNewLine _
                                            & "	OUTL.NRS_BR_CD                                           AS NRS_BR_CD             " & vbNewLine _
                                            & ",'10'                                                     AS PTN_ID                " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                                  " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                                 " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD                " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                  " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                             " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID                " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用1
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                             " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                  " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                  " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID                                                              " & vbNewLine _
                                            & " END                     AS RPT_ID                                                 " & vbNewLine _
                                            & ",OUTL.NRS_BR_CD          AS NRS_BR_CD                                              " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                   " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                   " & vbNewLine _
                                            & "      ELSE DST.DEST_NM                                                             " & vbNewLine _
                                            & " END                     AS DEST_NM                                                " & vbNewLine _
                                            & ",CASE WHEN OUTL.OUTKA_PKG_NB <> 0 THEN OUTL.OUTKA_PKG_NB                           " & vbNewLine _
                                            & "      ELSE SUM(OUTM.OUTKA_M_PKG_NB)                                                " & vbNewLine _
                                            & " END                     AS OUTKA_PKG_NB                                           " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                 " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                 " & vbNewLine _
                                            & "      ELSE DST.AD_1                                                                " & vbNewLine _
                                            & " END                     AS AD_1                                                   " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                            & "      ELSE DST.AD_2                                                                " & vbNewLine _
                                            & " END                     AS AD_2                                                   " & vbNewLine _
                                            & ",OUTL.DEST_AD_3          AS AD_3                                                   " & vbNewLine _
                                            & ",CST.CUST_NM_L           AS CUST_NM_L                                              " & vbNewLine _
                                            & ",UC.UNSOCO_NM            AS UNSOCO_NM                                              " & vbNewLine _
                                            & ",OUTL.ARR_PLAN_DATE      AS ARR_PLAN_DATE                                          " & vbNewLine _
                                            & ",KB1.KBN_NM1             AS ARR_PLAN_TIME                                          " & vbNewLine _
                                            & ",CASE WHEN MCD.SET_NAIYO <> NULL OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO         " & vbNewLine _
                                            & "      ELSE NB.NRS_BR_NM                                                            " & vbNewLine _
                                            & " END                     AS NRS_BR_NM                                              " & vbNewLine _
                                            & ",OUTL.OUTKA_NO_L         AS OUTKA_NO_L                                             " & vbNewLine _
                                            & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                " & vbNewLine _
                                            & "--, CASE WHEN SO.WH_KB = '01' THEN SO.TEL                                          " & vbNewLine _
                                            & "--  ELSE NB.TEL END      AS TEL                                                    " & vbNewLine _
                                            & "  , CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1                        " & vbNewLine _
                                            & "         WHEN SO.WH_KB  = '01' THEN SO.TEL                                         " & vbNewLine _
                                            & "    ELSE NB.TEL                                                                    " & vbNewLine _
                                            & "    END                  AS TEL                                                    " & vbNewLine _
                                            & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                " & vbNewLine _
                                            & ",CASE WHEN SO.WH_KB = '01' THEN SO.FAX                                             " & vbNewLine _
                                            & "      ELSE NB.FAX END               AS FAX                                         " & vbNewLine _
                                            & ",''                      AS TOP_FLAG                                               " & vbNewLine _
                                            & ",CST.CUST_NM_S           AS CUST_NM_S                                              " & vbNewLine _
                                            & ",OUTL.PC_KB              AS PC_KB                                                  " & vbNewLine _
                                            & ",OUTL.CUST_CD_L          AS CUST_CD_L                                              " & vbNewLine _
                                            & ",SHIPDEST.DEST_NM        AS SHIP_NM_L                                              " & vbNewLine _
                                            & ",CASE WHEN SHIPDEST.DEST_NM <> ''                                                  " & vbNewLine _
                                            & "           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM                  " & vbNewLine _
                                            & "      WHEN CST.DENPYO_NM   <> ''                                                   " & vbNewLine _
                                            & "           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                      " & vbNewLine _
                                            & "      ELSE CST.CUST_NM_L                                                           " & vbNewLine _
                                            & " END                         AS ATSUKAISYA_NM                                      " & vbNewLine _
                                            & "--,CASE WHEN SHIPDEST.DEST_NM <> ''                                                " & vbNewLine _
                                            & "--           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM                " & vbNewLine _
                                            & "--      WHEN CST.DENPYO_NM   <> ''                                                 " & vbNewLine _
                                            & "--           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                    " & vbNewLine _
                                            & "--      ELSE CST.CUST_NM_L                                                         " & vbNewLine _
                                            & "-- END                     AS ATSUKAISYA_NM                                        " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST        AS GOODS_CD_CUST                                          " & vbNewLine _
                                            & ",MG.GOODS_NM_1           AS GOODS_NM_1                                             " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                             " & vbNewLine _
                                            & ",CASE WHEN LEN(OUTM.FREE_C01) >= 8 THEN                                            " & vbNewLine _
                                            & "     CASE WHEN ISDATE(SUBSTRING(OUTM.FREE_C01,1,6) + '01') = 1 THEN SUBSTRING(OUTM.FREE_C01,1,6) + '01' " & vbNewLine _
                                            & "     ELSE '' END                                                                   " & vbNewLine _
                                            & " ELSE '' END  AS LT_DATE                                                           " & vbNewLine _
                                            & ",OUTS.LOT_NO             AS LOT_NO                                                 " & vbNewLine _
                                            & ",NB.AD_1                 AS NRS_BR_AD_1                                            " & vbNewLine _
                                            & ",NB.AD_2                 AS NRS_BR_AD_2                                            " & vbNewLine _
                                            & ",NB.AD_3                 AS NRS_BR_AD_3                                            " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                             " & vbNewLine _
                                            & ",MG.GOODS_NM_3           AS GOODS_NM_3                                             " & vbNewLine _
                                            & ",OUTM.FREE_C06           AS GOODS_SYUBETU                                          " & vbNewLine _
                                            & " --荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加                   " & vbNewLine _
                                            & ",MCD01.SET_NAIYO         AS SET_NAIYO                                              " & vbNewLine _
                                            & "--荷主明細マスタ   LMC552(埼玉浮間用)対応追加                                      " & vbNewLine _
                                            & ",MCD01.SET_NAIYO_2       AS SET_NAIYO_2                                            " & vbNewLine _
                                            & ",MCD01.SET_NAIYO_3       AS SET_NAIYO_3                                            " & vbNewLine _
                                            & ",DST.TEL                 AS DEST_TEL                                               " & vbNewLine _
                                            & ",FL.AUTO_DENP_NO         AS AUTO_DENP_NO         -- ADD 2016/06/29                 " & vbNewLine _
                                            & ",MSZ.SHIWAKE_CD          AS SHIWAKE_CD           -- ADD 2016/06/29                 " & vbNewLine _
                                            & ",OUTL.CUST_CD_L          AS CUST_CD_L            -- ADD 2016/06/29                 " & vbNewLine _
                                            & ",OUTL.CUST_ORD_NO        AS CUST_ORD_NO          -- ADD 2016/06/29                 " & vbNewLine _
                                            & ",FL.UNSO_CD             AS UNSO_CD               -- ADD 2017/08/31                 " & vbNewLine _
                                            & ",FL.UNSO_BR_CD          AS UNSO_BR_CD            -- ADD 2017/08/31                 " & vbNewLine _
                                            & ",FL.NRS_BR_CD           AS NRS_BR_CD             -- ADD 2017/08/31                 " & vbNewLine _
                                            & ",ISNULL(MTOLL.CHAKU_CD,'')   AS CHAKU_CD         -- ADD 2017/09/11                 " & vbNewLine _
                                            & ",ISNULL(MTOLL.CHAKU_NM,'')   AS CHAKU_NM         -- ADD 2017/09/11                 " & vbNewLine _
                                            & ",STUFF((SELECT ',' + SUB.OKIBA_KOSU                                                                     " & vbNewLine _
                                            & "          FROM (SELECT S.TOU_NO + RTRIM(S.SITU_NO) + CONVERT(VARCHAR,SUM(S.ALCTD_NB)) AS OKIBA_KOSU " & vbNewLine _
                                            & "                  FROM $LM_TRN$..C_OUTKA_S S                                                            " & vbNewLine _
                                            & "                 WHERE S.NRS_BR_CD = OUTL.NRS_BR_CD                                                     " & vbNewLine _
                                            & "                   AND S.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                   " & vbNewLine _
                                            & "                   AND S.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                            & "                 GROUP BY S.TOU_NO, S.SITU_NO                                                           " & vbNewLine _
                                            & "               ) SUB                                                                                    " & vbNewLine _
                                            & "         ORDER BY SUB.OKIBA_KOSU                                                                        " & vbNewLine _
                                            & "           FOR XML PATH('')                                                                             " & vbNewLine _
                                            & "       )                                                                                                " & vbNewLine _
                                            & "       ,1,1,''                                                                                          " & vbNewLine _
                                            & "      ) AS OKIBA_KOSU_CSV                                                                               " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用(LMC821)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMC821 As String = "SELECT                                                                             " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                  " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                  " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID                                                              " & vbNewLine _
                                            & " END                     AS RPT_ID                                                 " & vbNewLine _
                                            & ",OUTL.NRS_BR_CD          AS NRS_BR_CD                                              " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                   " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                   " & vbNewLine _
                                            & "      ELSE DST.DEST_NM                                                             " & vbNewLine _
                                            & " END                     AS DEST_NM                                                " & vbNewLine _
                                            & ",CASE WHEN OUTL.OUTKA_PKG_NB <> 0 THEN OUTL.OUTKA_PKG_NB                           " & vbNewLine _
                                            & "      ELSE SUM(OUTM.OUTKA_M_PKG_NB)                                                " & vbNewLine _
                                            & " END                     AS OUTKA_PKG_NB                                           " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                 " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                 " & vbNewLine _
                                            & "      ELSE DST.AD_1                                                                " & vbNewLine _
                                            & " END                     AS AD_1                                                   " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                            & "      ELSE DST.AD_2                                                                " & vbNewLine _
                                            & " END                     AS AD_2                                                   " & vbNewLine _
                                            & ",OUTL.DEST_AD_3          AS AD_3                                                   " & vbNewLine _
                                            & ",CST.CUST_NM_L           AS CUST_NM_L                                              " & vbNewLine _
                                            & ",UC.UNSOCO_NM            AS UNSOCO_NM                                              " & vbNewLine _
                                            & ",OUTL.ARR_PLAN_DATE      AS ARR_PLAN_DATE                                          " & vbNewLine _
                                            & ",KB1.KBN_NM1             AS ARR_PLAN_TIME                                          " & vbNewLine _
                                            & ",CASE WHEN MCD.SET_NAIYO <> NULL OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO         " & vbNewLine _
                                            & "      ELSE NB.NRS_BR_NM                                                            " & vbNewLine _
                                            & " END                     AS NRS_BR_NM                                              " & vbNewLine _
                                            & ",OUTL.OUTKA_NO_L         AS OUTKA_NO_L                                             " & vbNewLine _
                                            & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                " & vbNewLine _
                                            & "--, CASE WHEN SO.WH_KB = '01' THEN SO.TEL                                          " & vbNewLine _
                                            & "--  ELSE NB.TEL END      AS TEL                                                    " & vbNewLine _
                                            & "  , CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1                        " & vbNewLine _
                                            & "         WHEN SO.WH_KB  = '01' THEN SO.TEL                                         " & vbNewLine _
                                            & "    ELSE NB.TEL                                                                    " & vbNewLine _
                                            & "    END                  AS TEL                                                    " & vbNewLine _
                                            & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                " & vbNewLine _
                                            & ",CASE WHEN SO.WH_KB = '01' THEN SO.FAX                                             " & vbNewLine _
                                            & "      ELSE NB.FAX END               AS FAX                                         " & vbNewLine _
                                            & ",''                      AS TOP_FLAG                                               " & vbNewLine _
                                            & ",CST.CUST_NM_S           AS CUST_NM_S                                              " & vbNewLine _
                                            & ",OUTL.PC_KB              AS PC_KB                                                  " & vbNewLine _
                                            & ",OUTL.CUST_CD_L          AS CUST_CD_L                                              " & vbNewLine _
                                            & ",SHIPDEST.DEST_NM        AS SHIP_NM_L                                              " & vbNewLine _
                                            & ",CASE WHEN SHIPDEST.DEST_NM <> ''                                                  " & vbNewLine _
                                            & "           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM                  " & vbNewLine _
                                            & "      WHEN CST.DENPYO_NM   <> ''                                                   " & vbNewLine _
                                            & "           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                      " & vbNewLine _
                                            & "      ELSE CST.CUST_NM_L                                                           " & vbNewLine _
                                            & " END                         AS ATSUKAISYA_NM                                      " & vbNewLine _
                                            & "--,CASE WHEN SHIPDEST.DEST_NM <> ''                                                " & vbNewLine _
                                            & "--           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM                " & vbNewLine _
                                            & "--      WHEN CST.DENPYO_NM   <> ''                                                 " & vbNewLine _
                                            & "--           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                    " & vbNewLine _
                                            & "--      ELSE CST.CUST_NM_L                                                         " & vbNewLine _
                                            & "-- END                     AS ATSUKAISYA_NM                                        " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST        AS GOODS_CD_CUST                                          " & vbNewLine _
                                            & ",MG.GOODS_NM_1           AS GOODS_NM_1                                             " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                             " & vbNewLine _
                                            & ",CASE WHEN LEN(OUTM.FREE_C01) >= 8 THEN                                            " & vbNewLine _
                                            & "     CASE WHEN ISDATE(SUBSTRING(OUTM.FREE_C01,1,6) + '01') = 1 THEN SUBSTRING(OUTM.FREE_C01,1,6) + '01' " & vbNewLine _
                                            & "     ELSE '' END                                                                   " & vbNewLine _
                                            & " ELSE '' END  AS LT_DATE                                                           " & vbNewLine _
                                            & ",OUTS.LOT_NO             AS LOT_NO                                                 " & vbNewLine _
                                            & ",NB.AD_1                 AS NRS_BR_AD_1                                            " & vbNewLine _
                                            & ",NB.AD_2                 AS NRS_BR_AD_2                                            " & vbNewLine _
                                            & ",NB.AD_3                 AS NRS_BR_AD_3                                            " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                             " & vbNewLine _
                                            & ",MG.GOODS_NM_3           AS GOODS_NM_3                                             " & vbNewLine _
                                            & ",OUTM.FREE_C06           AS GOODS_SYUBETU                                          " & vbNewLine _
                                            & " --荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加                   " & vbNewLine _
                                            & ",MCD01.SET_NAIYO         AS SET_NAIYO                                              " & vbNewLine _
                                            & "--荷主明細マスタ   LMC552(埼玉浮間用)対応追加                                      " & vbNewLine _
                                            & ",MCD01.SET_NAIYO_2       AS SET_NAIYO_2                                            " & vbNewLine _
                                            & ",MCD01.SET_NAIYO_3       AS SET_NAIYO_3                                            " & vbNewLine _
                                            & ",EDIL.DEST_TEL           AS DEST_TEL                                               " & vbNewLine _
                                            & ",FL.AUTO_DENP_NO         AS AUTO_DENP_NO         -- ADD 2016/06/29                 " & vbNewLine _
                                            & ",MSZ.SHIWAKE_CD          AS SHIWAKE_CD           -- ADD 2016/06/29                 " & vbNewLine _
                                            & ",OUTL.CUST_CD_L          AS CUST_CD_L            -- ADD 2016/06/29                 " & vbNewLine _
                                            & ",OUTL.CUST_ORD_NO        AS CUST_ORD_NO          -- ADD 2016/06/29                 " & vbNewLine _
                                            & ",FL.UNSO_CD             AS UNSO_CD               -- ADD 2017/08/31                 " & vbNewLine _
                                            & ",FL.UNSO_BR_CD          AS UNSO_BR_CD            -- ADD 2017/08/31                 " & vbNewLine _
                                            & ",FL.NRS_BR_CD           AS NRS_BR_CD             -- ADD 2017/08/31                 " & vbNewLine _
                                            & ",ISNULL(MTOLL.CHAKU_CD,'')   AS CHAKU_CD         -- ADD 2017/09/11                 " & vbNewLine _
                                            & ",ISNULL(MTOLL.CHAKU_NM,'')   AS CHAKU_NM         -- ADD 2017/09/11                 " & vbNewLine

    'START YANAI 要望番号1478 一括印刷が遅い
    '''' <summary>
    '''' データ抽出用FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM As String = "FROM                                                                                      " & vbNewLine _
    '                                 & "$LM_TRN$..C_OUTKA_L OUTL                                                                  " & vbNewLine _
    '                                 & "--(2012.09.06)要望番号1412対応  --- START ---                                             " & vbNewLine _
    '                                 & "--出荷EDIL                                                                                " & vbNewLine _
    '                                 & "--LEFT JOIN                                                                               " & vbNewLine _
    '                                 & "--    (                                                                                   " & vbNewLine _
    '                                 & "--      SELECT                                                                            " & vbNewLine _
    '                                 & "--            MIN(DEST_NM)     AS DEST_NM                                                 " & vbNewLine _
    '                                 & "--           ,MIN(DEST_AD_1)     AS DEST_AD_1                                             " & vbNewLine _
    '                                 & "--           ,MIN(DEST_AD_2)   AS DEST_AD_2                                               " & vbNewLine _
    '                                 & "--           ,NRS_BR_CD                                                                   " & vbNewLine _
    '                                 & "--           ,OUTKA_CTL_NO                                                                " & vbNewLine _
    '                                 & "--           ,SHIP_NM_L                                                                   " & vbNewLine _
    '                                 & "--           ,SYS_DEL_FLG                                                                 " & vbNewLine _
    '                                 & "--      FROM                                                                              " & vbNewLine _
    '                                 & "--      $LM_TRN$..H_OUTKAEDI_L                                                            " & vbNewLine _
    '                                 & "--	  GROUP BY			                                                                  " & vbNewLine _
    '                                 & "--	        NRS_BR_CD		                                                              " & vbNewLine _
    '                                 & "--	       ,OUTKA_CTL_NO	                                                              " & vbNewLine _
    '                                 & "--	       ,SHIP_NM_L	                                                                  " & vbNewLine _
    '                                 & "--	       ,SYS_DEL_FLG		                                                              " & vbNewLine _
    '                                 & "--	 ) AS EDIL		                                                                      " & vbNewLine _
    '                                 & "--	ON  EDIL.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    '                                 & "--	AND EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
    '                                 & "--	AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                               " & vbNewLine _
    '                                 & "--下記の内容に変更                                                                        " & vbNewLine _
    '                                 & " LEFT JOIN (                                                                              " & vbNewLine _
    '                                 & "            SELECT                                                                        " & vbNewLine _
    '                                 & "                   NRS_BR_CD                                                              " & vbNewLine _
    '                                 & "                 , EDI_CTL_NO                                                             " & vbNewLine _
    '                                 & "                 , OUTKA_CTL_NO                                                           " & vbNewLine _
    '                                 & "             FROM (                                                                       " & vbNewLine _
    '                                 & "                    SELECT                                                                " & vbNewLine _
    '                                 & "                           EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
    '                                 & "                         , EDIOUTL.EDI_CTL_NO                                             " & vbNewLine _
    '                                 & "                         , EDIOUTL.OUTKA_CTL_NO                                           " & vbNewLine _
    '                                 & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                     " & vbNewLine _
    '                                 & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
    '                                 & "                                                              , EDIOUTL.OUTKA_CTL_NO      " & vbNewLine _
    '                                 & "                                                       ORDER BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
    '                                 & "                                                              , EDIOUTL.EDI_CTL_NO        " & vbNewLine _
    '                                 & "                                                  )                                       " & vbNewLine _
    '                                 & "                           END AS IDX                                                     " & vbNewLine _
    '                                 & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                  " & vbNewLine _
    '                                 & "                          LEFT JOIN $LM_TRN$..C_OUTKA_L C_OUTL                            " & vbNewLine _
    '                                 & "                                 ON C_OUTL.NRS_BR_CD   = EDIOUTL.NRS_BR_CD                " & vbNewLine _
    '                                 & "                                AND C_OUTL.OUTKA_NO_L  = EDIOUTL.OUTKA_CTL_NO             " & vbNewLine _
    '                                 & "                                AND C_OUTL.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                                 & "                     WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
    '                                 & "                       AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                              " & vbNewLine _
    '                                 & "                  ) EBASE                                                                 " & vbNewLine _
    '                                 & "            WHERE EBASE.IDX = 1                                                           " & vbNewLine _
    '                                 & "            ) TOPEDI                                                                      " & vbNewLine _
    '                                 & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
    '                                 & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                          " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                    " & vbNewLine _
    '                                 & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                             " & vbNewLine _
    '                                 & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                            " & vbNewLine _
    '                                 & "--(2012.09.06)要望番号1412対応  ---  END  ---                                             " & vbNewLine _
    '                                 & "	--出荷M                                                            		              " & vbNewLine _
    '                                 & "	LEFT JOIN                                                                  		      " & vbNewLine _
    '                                 & "	(SELECT                                                                               " & vbNewLine _
    '                                 & "	*                                                                                     " & vbNewLine _
    '                                 & "	FROM                                                                                  " & vbNewLine _
    '                                 & "	(SELECT                                                                               " & vbNewLine _
    '                                 & "	   M.NRS_BR_CD                                                                        " & vbNewLine _
    '                                 & "	  ,M.GOODS_CD_NRS		                                                              " & vbNewLine _
    '                                 & "	  ,M.OUTKA_M_PKG_NB                                                                   " & vbNewLine _
    '                                 & "	  ,EDIM.FREE_C01                                                                      " & vbNewLine _
    '                                 & "	  ,EDIM.FREE_C06                                                                      " & vbNewLine _
    '                                 & "	  ,ROW_NUMBER() OVER (PARTITION BY M.NRS_BR_CD ORDER BY M.OUTKA_NO_L,M.OUTKA_NO_M) AS NUM   " & vbNewLine _
    '                                 & "	  FROM                                                                                " & vbNewLine _
    '                                 & "	   $LM_TRN$..C_OUTKA_M M                                                              " & vbNewLine _
    '                                 & "	   LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                              " & vbNewLine _
    '                                 & "	   ON M.NRS_BR_CD = EDIM.NRS_BR_CD                                                    " & vbNewLine _
    '                                 & "	   AND M.OUTKA_NO_L = EDIM.OUTKA_CTL_NO                                               " & vbNewLine _
    '                                 & "	   AND M.OUTKA_NO_M = EDIM.OUTKA_CTL_NO_CHU                                           " & vbNewLine _
    '                                 & "	  WHERE                                                                               " & vbNewLine _
    '                                 & "	     M.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
    '                                 & "	    AND M.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
    '                                 & "	    AND M.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
    '                                 & "	 ) AS BASE                                                                            " & vbNewLine _
    '                                 & "	 WHERE                                                                                " & vbNewLine _
    '                                 & "	 BASE.NUM = 1                                                                         " & vbNewLine _
    '                                 & "	) OUTM                                                                                " & vbNewLine _
    '                                 & "	ON                                                                                    " & vbNewLine _
    '                                 & "	1 = 1                                                                                 " & vbNewLine _
    '                                 & "	--出荷S(LMC551 荷札(日医工用)対応)                                                    " & vbNewLine _
    '                                 & "	LEFT JOIN                                                                  		      " & vbNewLine _
    '                                 & "	(SELECT                                                                               " & vbNewLine _
    '                                 & "	*                                                                                     " & vbNewLine _
    '                                 & "	FROM                                                                                  " & vbNewLine _
    '                                 & "	(SELECT                                                                               " & vbNewLine _
    '                                 & "	        LOT_NO                                                                        " & vbNewLine _
    '                                 & "	      , ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD ORDER BY OUTKA_NO_L,OUTKA_NO_M,OUTKA_NO_S) AS NUM " & vbNewLine _
    '                                 & "	   FROM                                                                               " & vbNewLine _
    '                                 & "	        $LM_TRN$..C_OUTKA_S S                                                         " & vbNewLine _
    '                                 & "	  WHERE                                                                               " & vbNewLine _
    '                                 & "	        S.NRS_BR_CD  = @NRS_BR_CD                                                     " & vbNewLine _
    '                                 & "	    AND S.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
    '                                 & "	    AND SYS_DEL_FLG  = '0'                                                            " & vbNewLine _
    '                                 & "	 ) AS BASES                                                                           " & vbNewLine _
    '                                 & "	 WHERE                                                                                " & vbNewLine _
    '                                 & "	 BASES.NUM = 1                                                                        " & vbNewLine _
    '                                 & "	) OUTS                                                                                " & vbNewLine _
    '                                 & "	ON                                                                                    " & vbNewLine _
    '                                 & "	1 = 1                                                                                 " & vbNewLine _
    '                                 & "--商品M                                                                                   " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_GOODS MG                                                            " & vbNewLine _
    '                                 & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                         " & vbNewLine _
    '                                 & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                   " & vbNewLine _
    '                                 & "--出荷Lでの荷主帳票パターン取得                                                           " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                       " & vbNewLine _
    '                                 & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                       " & vbNewLine _
    '                                 & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                       " & vbNewLine _
    '                                 & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                       " & vbNewLine _
    '                                 & "AND '00' = MCR1.CUST_CD_S                                                                 " & vbNewLine _
    '                                 & "AND MCR1.PTN_ID = '10'                                                                    " & vbNewLine _
    '                                 & "AND MCR1.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    '                                 & "--帳票パターン取得                                                                        " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_RPT MR1                                                             " & vbNewLine _
    '                                 & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                        " & vbNewLine _
    '                                 & "AND MR1.PTN_ID = MCR1.PTN_ID                                                              " & vbNewLine _
    '                                 & "AND MR1.PTN_CD = MCR1.PTN_CD                                                              " & vbNewLine _
    '                                 & "AND MR1.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
    '                                 & "--商品Mの荷主での荷主帳票パターン取得                                                     " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                       " & vbNewLine _
    '                                 & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                         " & vbNewLine _
    '                                 & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                         " & vbNewLine _
    '                                 & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                         " & vbNewLine _
    '                                 & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                         " & vbNewLine _
    '                                 & "AND MCR2.PTN_ID = '10'                                                                    " & vbNewLine _
    '                                 & "AND MCR2.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    '                                 & "--帳票パターン取得                                                                        " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_RPT MR2                                                             " & vbNewLine _
    '                                 & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                        " & vbNewLine _
    '                                 & "AND MR2.PTN_ID = MCR2.PTN_ID                                                              " & vbNewLine _
    '                                 & "AND MR2.PTN_CD = MCR2.PTN_CD                                                              " & vbNewLine _
    '                                 & "AND MR2.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
    '                                 & "--存在しない場合の帳票パターン取得                                                        " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_RPT MR3                                                             " & vbNewLine _
    '                                 & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
    '                                 & "AND MR3.PTN_ID = '10'                                                                     " & vbNewLine _
    '                                 & "AND MR3.STANDARD_FLAG = '01'                                                              " & vbNewLine _
    '                                 & "AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
    '                                 & "--荷主マスタ                                                                              " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_CUST CST                                                            " & vbNewLine _
    '                                 & "ON  CST.NRS_BR_CD = MG.NRS_BR_CD                                                          " & vbNewLine _
    '                                 & "AND CST.CUST_CD_L = MG.CUST_CD_L                                                          " & vbNewLine _
    '                                 & "AND CST.CUST_CD_M = MG.CUST_CD_M                                                          " & vbNewLine _
    '                                 & "AND CST.CUST_CD_S = MG.CUST_CD_S                                                          " & vbNewLine _
    '                                 & "AND CST.CUST_CD_SS = MG.CUST_CD_SS                                                        " & vbNewLine _
    '                                 & "--荷主明細マスタ                                                                          " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                    " & vbNewLine _
    '                                 & "ON  MCD.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
    '                                 & "AND MCD.CUST_CD = OUTL.CUST_CD_L                                                          " & vbNewLine _
    '                                 & "AND MCD.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
    '                                 & "AND MCD.SUB_KB = '11'                                                                     " & vbNewLine _
    '                                 & "AND MCD.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
    '                                 & "--届先マスタ                                                                              " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_DEST DST                                                            " & vbNewLine _
    '                                 & "ON  DST.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
    '                                 & "AND DST.CUST_CD_L = OUTL.CUST_CD_L                                                        " & vbNewLine _
    '                                 & "AND DST.DEST_CD   = OUTL.DEST_CD                                                          " & vbNewLine _
    '                                 & "--届先マスタ（売上先）                                                                    " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_DEST SHIPDEST                                                       " & vbNewLine _
    '                                 & "ON  SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
    '                                 & "AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                                                   " & vbNewLine _
    '                                 & "AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                                                   " & vbNewLine _
    '                                 & "--F運送L                                                                                  " & vbNewLine _
    '                                 & "LEFT JOIN $LM_TRN$..F_UNSO_L FL                                                           " & vbNewLine _
    '                                 & "ON  FL.MOTO_DATA_KB   = '20'                                                              " & vbNewLine _
    '                                 & "AND FL.NRS_BR_CD      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
    '                                 & "AND FL.INOUTKA_NO_L   = OUTL.OUTKA_NO_L                                                   " & vbNewLine _
    '                                 & "AND FL.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
    '                                 & "--運送会社マスタ                                                                          " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_UNSOCO UC                                                           " & vbNewLine _
    '                                 & "ON  UC.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
    '                                 & "AND UC.UNSOCO_CD    = FL.UNSO_CD                                                          " & vbNewLine _
    '                                 & "AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                                                       " & vbNewLine _
    '                                 & "--営業所マスタ                                                                            " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_NRS_BR NB                                                           " & vbNewLine _
    '                                 & "ON  NB.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
    '                                 & "--倉庫マスタ                                                                              " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_SOKO SO                                                             " & vbNewLine _
    '                                 & "ON  SO.WH_CD = OUTL.WH_CD                                                                 " & vbNewLine _
    '                                 & "--区分マスタ                                                                              " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..Z_KBN KB1                                                             " & vbNewLine _
    '                                 & "ON  KB1.KBN_GROUP_CD = 'N010'                                                             " & vbNewLine _
    '                                 & "AND KB1.KBN_CD= OUTL.ARR_PLAN_TIME                                                        " & vbNewLine _
    '                                 & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                       " & vbNewLine _
    '                                 & "--区分マスタ                                                                              " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..Z_KBN KB2                                                             " & vbNewLine _
    '                                 & "  ON KB2.KBN_GROUP_CD = 'N022'                                                            " & vbNewLine _
    '                                 & " AND KB2.KBN_CD       = '00'                                                              " & vbNewLine _
    '                                 & " AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
    '                                 & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                       " & vbNewLine _
    '                                 & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 Start                     " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                                                  " & vbNewLine _
    '                                 & "ON  MCD01.NRS_BR_CD = OUTL.NRS_BR_CD                                                      " & vbNewLine _
    '                                 & "AND MCD01.CUST_CD = OUTL.CUST_CD_L                                                        " & vbNewLine _
    '                                 & "AND MCD01.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
    '                                 & "AND MCD01.SUB_KB = '29'                                                                   " & vbNewLine _
    '                                 & "AND MCD01.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
    '                                 & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 End                       " & vbNewLine _
    '                                 & "WHERE                                                                                     " & vbNewLine _
    '                                 & "    OUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    '                                 & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine

#If False Then ' FROM, WHERE定義分割
    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                                                      " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                                  " & vbNewLine _
                                     & "--(2012.09.06)要望番号1412対応  --- START ---                                             " & vbNewLine _
                                     & "--出荷EDIL                                                                                " & vbNewLine _
                                     & "--LEFT JOIN                                                                               " & vbNewLine _
                                     & "--    (                                                                                   " & vbNewLine _
                                     & "--      SELECT                                                                            " & vbNewLine _
                                     & "--            MIN(DEST_NM)     AS DEST_NM                                                 " & vbNewLine _
                                     & "--           ,MIN(DEST_AD_1)     AS DEST_AD_1                                             " & vbNewLine _
                                     & "--           ,MIN(DEST_AD_2)   AS DEST_AD_2                                               " & vbNewLine _
                                     & "--           ,NRS_BR_CD                                                                   " & vbNewLine _
                                     & "--           ,OUTKA_CTL_NO                                                                " & vbNewLine _
                                     & "--           ,SHIP_NM_L                                                                   " & vbNewLine _
                                     & "--           ,SYS_DEL_FLG                                                                 " & vbNewLine _
                                     & "--      FROM                                                                              " & vbNewLine _
                                     & "--      $LM_TRN$..H_OUTKAEDI_L                                                            " & vbNewLine _
                                     & "--	  GROUP BY			                                                                  " & vbNewLine _
                                     & "--	        NRS_BR_CD		                                                              " & vbNewLine _
                                     & "--	       ,OUTKA_CTL_NO	                                                              " & vbNewLine _
                                     & "--	       ,SHIP_NM_L	                                                                  " & vbNewLine _
                                     & "--	       ,SYS_DEL_FLG		                                                              " & vbNewLine _
                                     & "--	 ) AS EDIL		                                                                      " & vbNewLine _
                                     & "--	ON  EDIL.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                     & "--	AND EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "--	AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                               " & vbNewLine _
                                     & "--下記の内容に変更                                                                        " & vbNewLine _
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
                                     & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                          " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                    " & vbNewLine _
                                     & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                             " & vbNewLine _
                                     & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                            " & vbNewLine _
                                     & "--(2012.09.06)要望番号1412対応  ---  END  ---                                             " & vbNewLine _
                                     & "	--出荷M                                                            		              " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	   M.NRS_BR_CD                                                                        " & vbNewLine _
                                     & "	  ,M.GOODS_CD_NRS		                                                              " & vbNewLine _
                                     & "	  ,M.OUTKA_M_PKG_NB                                                                   " & vbNewLine _
                                     & "	  ,EDIM.FREE_C01                                                                      " & vbNewLine _
                                     & "	  ,EDIM.FREE_C06                                                                      " & vbNewLine _
                                     & "	  ,ROW_NUMBER() OVER (PARTITION BY M.NRS_BR_CD ORDER BY M.OUTKA_NO_L,M.OUTKA_NO_M) AS NUM   " & vbNewLine _
                                     & "	  FROM                                                                                " & vbNewLine _
                                     & "	   $LM_TRN$..C_OUTKA_M M                                                              " & vbNewLine _
                                     & "	   LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                              " & vbNewLine _
                                     & "	   ON M.NRS_BR_CD = EDIM.NRS_BR_CD                                                    " & vbNewLine _
                                     & "	   AND M.OUTKA_NO_L = EDIM.OUTKA_CTL_NO                                               " & vbNewLine _
                                     & "	   AND M.OUTKA_NO_M = EDIM.OUTKA_CTL_NO_CHU                                           " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	     M.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
                                     & "	    AND M.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND M.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "	 ) AS BASE                                                                            " & vbNewLine _
                                     & "	 WHERE                                                                                " & vbNewLine _
                                     & "	 BASE.NUM = 1                                                                         " & vbNewLine _
                                     & "	) OUTM                                                                                " & vbNewLine _
                                     & "	ON                                                                                    " & vbNewLine _
                                     & "	1 = 1                                                                                 " & vbNewLine _
                                     & "	--出荷S(LMC551 荷札(日医工用)対応)                                                    " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	        LOT_NO                                                                        " & vbNewLine _
                                     & "	      , ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD ORDER BY OUTKA_NO_L,OUTKA_NO_M,OUTKA_NO_S) AS NUM " & vbNewLine _
                                     & "	   FROM                                                                               " & vbNewLine _
                                     & "	        $LM_TRN$..C_OUTKA_S S                                                         " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	        S.NRS_BR_CD  = @NRS_BR_CD                                                     " & vbNewLine _
                                     & "	    AND S.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND SYS_DEL_FLG  = '0'                                                            " & vbNewLine _
                                     & "	 ) AS BASES                                                                           " & vbNewLine _
                                     & "	 WHERE                                                                                " & vbNewLine _
                                     & "	 BASES.NUM = 1                                                                        " & vbNewLine _
                                     & "	) OUTS                                                                                " & vbNewLine _
                                     & "	ON                                                                                    " & vbNewLine _
                                     & "	1 = 1                                                                                 " & vbNewLine _
                                     & "--商品M                                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                                            " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                   " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                       " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                       " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                                 " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR1.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                             " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                       " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                         " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR2.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                             " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                                             " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR3.PTN_ID = '10'                                                                     " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                              " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--荷主マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST CST                                                            " & vbNewLine _
                                     & "ON  CST.NRS_BR_CD = MG.NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_L = MG.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_M = MG.CUST_CD_M                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_S = MG.CUST_CD_S                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_SS = MG.CUST_CD_SS                                                        " & vbNewLine _
                                     & "--荷主明細マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                    " & vbNewLine _
                                     & "ON  MCD.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MCD.CUST_CD = OUTL.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND MCD.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
                                     & "AND MCD.SUB_KB = '11'                                                                     " & vbNewLine _
                                     & "AND MCD.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--届先マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST DST                                                            " & vbNewLine _
                                     & "ON  DST.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND DST.CUST_CD_L = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND DST.DEST_CD   = OUTL.DEST_CD                                                          " & vbNewLine _
                                     & "--届先マスタ（売上先）                                                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST SHIPDEST                                                       " & vbNewLine _
                                     & "ON  SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                                                   " & vbNewLine _
                                     & "AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                                                   " & vbNewLine _
                                     & "--F運送L                                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..F_UNSO_L FL                                                           " & vbNewLine _
                                     & "ON  FL.MOTO_DATA_KB   = '20'                                                              " & vbNewLine _
                                     & "AND FL.NRS_BR_CD      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "AND FL.INOUTKA_NO_L   = OUTL.OUTKA_NO_L                                                   " & vbNewLine _
                                     & "AND FL.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                     & "--運送会社マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_UNSOCO UC                                                           " & vbNewLine _
                                     & "ON  UC.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND UC.UNSOCO_CD    = FL.UNSO_CD                                                          " & vbNewLine _
                                     & "AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                                                       " & vbNewLine _
                                     & "--営業所マスタ                                                                            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_NRS_BR NB                                                           " & vbNewLine _
                                     & "ON  NB.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "--倉庫マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SOKO SO                                                             " & vbNewLine _
                                     & "ON  SO.WH_CD = OUTL.WH_CD                                                                 " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB1                                                             " & vbNewLine _
                                     & "ON  KB1.KBN_GROUP_CD = 'N010'                                                             " & vbNewLine _
                                     & "AND KB1.KBN_CD= OUTL.ARR_PLAN_TIME                                                        " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                       " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB2                                                             " & vbNewLine _
                                     & "  ON KB2.KBN_GROUP_CD = 'N022'                                                            " & vbNewLine _
                                     & " AND KB2.KBN_CD       = '00'                                                              " & vbNewLine _
                                     & " AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                       " & vbNewLine _
                                     & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 Start                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                                                  " & vbNewLine _
                                     & "ON  MCD01.NRS_BR_CD = OUTL.NRS_BR_CD                                                      " & vbNewLine _
                                     & "AND MCD01.CUST_CD = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND MCD01.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND MCD01.SUB_KB = '29'                                                                   " & vbNewLine _
                                     & "AND MCD01.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                     & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 End                       " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                       " & vbNewLine _
                                     & "--ADD 2016/06/29 西濃ZIP   西濃対応応追加 Start                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SEINO_ZIP MSZ                                                       " & vbNewLine _
                                     & "ON  MSZ.ZIP_NO   = DST.ZIP                                                                " & vbNewLine _
                                     & "AND MSZ.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--ADD 2016/06/29 西濃ZIP   西濃対応応追加 End                                             " & vbNewLine _
                                     & "WHERE                                                                                     " & vbNewLine _
                                     & "    OUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                           " & vbNewLine _
                                     & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine
    'END YANAI 要望番号1478 一括印刷が遅い
#Else

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_BASE As String _
                                     = "FROM                                                                                      " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                                  " & vbNewLine _
                                     & "--(2012.09.06)要望番号1412対応  --- START ---                                             " & vbNewLine _
                                     & "--出荷EDIL                                                                                " & vbNewLine _
                                     & "--LEFT JOIN                                                                               " & vbNewLine _
                                     & "--    (                                                                                   " & vbNewLine _
                                     & "--      SELECT                                                                            " & vbNewLine _
                                     & "--            MIN(DEST_NM)     AS DEST_NM                                                 " & vbNewLine _
                                     & "--           ,MIN(DEST_AD_1)     AS DEST_AD_1                                             " & vbNewLine _
                                     & "--           ,MIN(DEST_AD_2)   AS DEST_AD_2                                               " & vbNewLine _
                                     & "--           ,NRS_BR_CD                                                                   " & vbNewLine _
                                     & "--           ,OUTKA_CTL_NO                                                                " & vbNewLine _
                                     & "--           ,SHIP_NM_L                                                                   " & vbNewLine _
                                     & "--           ,SYS_DEL_FLG                                                                 " & vbNewLine _
                                     & "--      FROM                                                                              " & vbNewLine _
                                     & "--      $LM_TRN$..H_OUTKAEDI_L                                                            " & vbNewLine _
                                     & "--	  GROUP BY			                                                                  " & vbNewLine _
                                     & "--	        NRS_BR_CD		                                                              " & vbNewLine _
                                     & "--	       ,OUTKA_CTL_NO	                                                              " & vbNewLine _
                                     & "--	       ,SHIP_NM_L	                                                                  " & vbNewLine _
                                     & "--	       ,SYS_DEL_FLG		                                                              " & vbNewLine _
                                     & "--	 ) AS EDIL		                                                                      " & vbNewLine _
                                     & "--	ON  EDIL.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                     & "--	AND EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "--	AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                               " & vbNewLine _
                                     & "--下記の内容に変更                                                                        " & vbNewLine _
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
                                     & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                          " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                    " & vbNewLine _
                                     & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                             " & vbNewLine _
                                     & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                            " & vbNewLine _
                                     & "--(2012.09.06)要望番号1412対応  ---  END  ---                                             " & vbNewLine _
                                     & "	--出荷M                                                            		              " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	   M.NRS_BR_CD                                                                        " & vbNewLine _
                                     & "	  ,M.GOODS_CD_NRS		                                                              " & vbNewLine _
                                     & "	  ,M.OUTKA_M_PKG_NB                                                                   " & vbNewLine _
                                     & "	  ,EDIM.FREE_C01                                                                      " & vbNewLine _
                                     & "	  ,EDIM.FREE_C06                                                                      " & vbNewLine _
                                     & "	  ,ROW_NUMBER() OVER (PARTITION BY M.NRS_BR_CD ORDER BY M.OUTKA_NO_L,M.OUTKA_NO_M) AS NUM   " & vbNewLine _
                                     & "	  FROM                                                                                " & vbNewLine _
                                     & "	   $LM_TRN$..C_OUTKA_M M                                                              " & vbNewLine _
                                     & "	   LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                              " & vbNewLine _
                                     & "	   ON M.NRS_BR_CD = EDIM.NRS_BR_CD                                                    " & vbNewLine _
                                     & "	   AND M.OUTKA_NO_L = EDIM.OUTKA_CTL_NO                                               " & vbNewLine _
                                     & "	   AND M.OUTKA_NO_M = EDIM.OUTKA_CTL_NO_CHU                                           " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	     M.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
                                     & "	    AND M.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND M.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "	 ) AS BASE                                                                            " & vbNewLine _
                                     & "	 WHERE                                                                                " & vbNewLine _
                                     & "	 BASE.NUM = 1                                                                         " & vbNewLine _
                                     & "	) OUTM                                                                                " & vbNewLine _
                                     & "	ON                                                                                    " & vbNewLine _
                                     & "	1 = 1                                                                                 " & vbNewLine _
                                     & "	--出荷S(LMC551 荷札(日医工用)対応)                                                    " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	        LOT_NO                                                                        " & vbNewLine _
                                     & "	      , ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD ORDER BY OUTKA_NO_L,OUTKA_NO_M,OUTKA_NO_S) AS NUM " & vbNewLine _
                                     & "	   FROM                                                                               " & vbNewLine _
                                     & "	        $LM_TRN$..C_OUTKA_S S                                                         " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	        S.NRS_BR_CD  = @NRS_BR_CD                                                     " & vbNewLine _
                                     & "	    AND S.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND SYS_DEL_FLG  = '0'                                                            " & vbNewLine _
                                     & "	 ) AS BASES                                                                           " & vbNewLine _
                                     & "	 WHERE                                                                                " & vbNewLine _
                                     & "	 BASES.NUM = 1                                                                        " & vbNewLine _
                                     & "	) OUTS                                                                                " & vbNewLine _
                                     & "	ON                                                                                    " & vbNewLine _
                                     & "	1 = 1                                                                                 " & vbNewLine _
                                     & "--商品M                                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                                            " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                   " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                       " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                       " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                                 " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR1.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                             " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                       " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                         " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR2.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                             " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                                             " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR3.PTN_ID = '10'                                                                     " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                              " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--荷主マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST CST                                                            " & vbNewLine _
                                     & "ON  CST.NRS_BR_CD = MG.NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_L = MG.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_M = MG.CUST_CD_M                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_S = MG.CUST_CD_S                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_SS = MG.CUST_CD_SS                                                        " & vbNewLine _
                                     & "--荷主明細マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                    " & vbNewLine _
                                     & "ON  MCD.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MCD.CUST_CD = OUTL.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND MCD.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
                                     & "AND MCD.SUB_KB = '11'                                                                     " & vbNewLine _
                                     & "AND MCD.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--届先マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST DST                                                            " & vbNewLine _
                                     & "ON  DST.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND DST.CUST_CD_L = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND DST.DEST_CD   = OUTL.DEST_CD                                                          " & vbNewLine _
                                     & "--届先マスタ（売上先）                                                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST SHIPDEST                                                       " & vbNewLine _
                                     & "ON  SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                                                   " & vbNewLine _
                                     & "AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                                                   " & vbNewLine _
                                     & "--F運送L                                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..F_UNSO_L FL                                                           " & vbNewLine _
                                     & "ON  FL.MOTO_DATA_KB   = '20'                                                              " & vbNewLine _
                                     & "AND FL.NRS_BR_CD      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "AND FL.INOUTKA_NO_L   = OUTL.OUTKA_NO_L                                                   " & vbNewLine _
                                     & "AND FL.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                     & "--運送会社マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_UNSOCO UC                                                           " & vbNewLine _
                                     & "ON  UC.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND UC.UNSOCO_CD    = FL.UNSO_CD                                                          " & vbNewLine _
                                     & "AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                                                       " & vbNewLine _
                                     & "--営業所マスタ                                                                            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_NRS_BR NB                                                           " & vbNewLine _
                                     & "ON  NB.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "--倉庫マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SOKO SO                                                             " & vbNewLine _
                                     & "ON  SO.WH_CD = OUTL.WH_CD                                                                 " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB1                                                             " & vbNewLine _
                                     & "ON  KB1.KBN_GROUP_CD = 'N010'                                                             " & vbNewLine _
                                     & "AND KB1.KBN_CD= OUTL.ARR_PLAN_TIME                                                        " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                       " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB2                                                             " & vbNewLine _
                                     & "  ON KB2.KBN_GROUP_CD = 'N022'                                                            " & vbNewLine _
                                     & " AND KB2.KBN_CD       = '00'                                                              " & vbNewLine _
                                     & " AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                       " & vbNewLine _
                                     & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 Start                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                                                  " & vbNewLine _
                                     & "ON  MCD01.NRS_BR_CD = OUTL.NRS_BR_CD                                                      " & vbNewLine _
                                     & "AND MCD01.CUST_CD = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND MCD01.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND MCD01.SUB_KB = '29'                                                                   " & vbNewLine _
                                     & "AND MCD01.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                     & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 End                       " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                       " & vbNewLine _
                                     & "--ADD 2016/06/29 西濃ZIP   西濃対応応追加 Start                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SEINO_ZIP MSZ                                                       " & vbNewLine _
                                     & "ON  MSZ.ZIP_NO   = DST.ZIP                                                                " & vbNewLine _
                                     & "AND MSZ.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--ADD 2016/06/29 西濃ZIP   西濃対応応追加 End                                             " & vbNewLine _
                                     & "--ADD 2017/09/11 M_TOLL  トール対応追加 Start                                             " & vbNewLine _
                                     & "LEFT JOIN LM_MST..M_TOLL MTOLL                                                            " & vbNewLine _
                                     & "ON  MTOLL.JIS_CD   = SUBSTRING(DST.JIS,1,5)                                               " & vbNewLine _
                                     & "AND MTOLL.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                     & "--ADD 2017/09/11 M_TOLL  トール対応追加 End                                               " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String = "WHERE                                                                                     " & vbNewLine _
                                     & "    OUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                           " & vbNewLine _
                                     & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = SQL_FROM_BASE & SQL_WHERE


#End If

#Region "データ抽出用FROM句(FFEM用)"

    ''' <summary>
    ''' データ抽出用FROM句(FFEM用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_ADD_FJF As String _
        = "LEFT JOIN (                                               " & vbNewLine _
        & "            SELECT                                        " & vbNewLine _
        & "                   NRS_BR_CD         AS NRS_BR_CD         " & vbNewLine _
        & "                 , OUTKA_CTL_NO      AS OUTKA_CTL_NO      " & vbNewLine _
        & "                 , MAX(ZFVYDTEKIYO)  AS ZFVYDTEKIYO       " & vbNewLine _
        & "              FROM  $LM_TRN$..H_INOUTKAEDI_HED_FJF        " & vbNewLine _
        & "             WHERE SYS_DEL_FLG     = '0'                  " & vbNewLine _
        & "               AND DEL_KB          = '0'                  " & vbNewLine _
        & "               AND INOUT_KB        = '0'                  " & vbNewLine _
        & "               AND NRS_BR_CD       = @NRS_BR_CD           " & vbNewLine _
        & "               AND OUTKA_CTL_NO    = @OUTKA_NO_L          " & vbNewLine _
        & "             GROUP BY NRS_BR_CD                           " & vbNewLine _
        & "                    , OUTKA_CTL_NO                        " & vbNewLine _
        & "          ) AS FJF                                        " & vbNewLine _
        & "  ON FJF.NRS_BR_CD    = OUTL.NRS_BR_CD                    " & vbNewLine _
        & " AND FJF.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                   " & vbNewLine


#End Region



    ''' <summary>
    ''' データ抽出用GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                   " & vbNewLine _
                                         & "       MR1.PTN_CD           " & vbNewLine _
                                         & "     , MR2.PTN_CD           " & vbNewLine _
                                         & "     , MR1.RPT_ID           " & vbNewLine _
                                         & "     , MR2.RPT_ID           " & vbNewLine _
                                         & "     , MR3.RPT_ID           " & vbNewLine _
                                         & "     , OUTL.NRS_BR_CD       " & vbNewLine _
                                         & "     , EDIL.OUTKA_CTL_NO    " & vbNewLine _
                                         & "     , OUTL.DEST_NM         " & vbNewLine _
                                         & "     , EDIL.DEST_NM         " & vbNewLine _
                                         & "     , DST.DEST_NM          " & vbNewLine _
                                         & "     , OUTL.OUTKA_PKG_NB    " & vbNewLine _
                                         & "     , OUTM.OUTKA_M_PKG_NB  " & vbNewLine _
                                         & "     , EDIL.OUTKA_CTL_NO    " & vbNewLine _
                                         & "     , OUTL.DEST_AD_1       " & vbNewLine _
                                         & "     , EDIL.DEST_AD_1       " & vbNewLine _
                                         & "     , DST.AD_1             " & vbNewLine _
                                         & "     , OUTL.DEST_AD_2       " & vbNewLine _
                                         & "     , EDIL.DEST_AD_2       " & vbNewLine _
                                         & "     , DST.AD_2             " & vbNewLine _
                                         & "     , OUTL.DEST_AD_3       " & vbNewLine _
                                         & "     , FL.AD_3              " & vbNewLine _
                                         & "     , CST.CUST_NM_L        " & vbNewLine _
                                         & "     , CST.DENPYO_NM        " & vbNewLine _
                                         & "     , UC.UNSOCO_NM         " & vbNewLine _
                                         & "     , OUTL.ARR_PLAN_DATE   " & vbNewLine _
                                         & "     , KB1.KBN_NM1          " & vbNewLine _
                                         & "     , NB.NRS_BR_NM         " & vbNewLine _
                                         & "     , OUTL.OUTKA_NO_L      " & vbNewLine _
                                         & "     , NB.TEL               " & vbNewLine _
                                         & "     , NB.FAX               " & vbNewLine _
                                         & "     , SO.WH_KB             " & vbNewLine _
                                         & "     , SO.TEL               " & vbNewLine _
                                         & "     , SO.FAX               " & vbNewLine _
                                         & "     , SHIPDEST.DEST_NM     " & vbNewLine _
                                         & "     , EDIL.SHIP_NM_L       " & vbNewLine _
                                         & "     , OUTL.DEST_KB         " & vbNewLine _
                                         & "     , CST.CUST_NM_S        " & vbNewLine _
                                         & "     , OUTL.PC_KB           " & vbNewLine _
                                         & "     , OUTL.CUST_CD_L       " & vbNewLine _
                                         & "     , MCD.SET_NAIYO        " & vbNewLine _
                                         & "     , MG.GOODS_CD_CUST     " & vbNewLine _
                                         & "     , MG.GOODS_NM_1        " & vbNewLine _
                                         & "     , MG.GOODS_NM_2        " & vbNewLine _
                                         & "     , MG.GOODS_NM_3        " & vbNewLine _
                                         & "     , OUTM.FREE_C01        " & vbNewLine _
                                         & "     , OUTM.FREE_C06        " & vbNewLine _
                                         & "     , OUTS.LOT_NO          " & vbNewLine _
                                         & "     , NB.AD_1              " & vbNewLine _
                                         & "     , NB.AD_2              " & vbNewLine _
                                         & "     , NB.AD_3              " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO      " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO_2    " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO_3    " & vbNewLine _
                                         & "     , DST.TEL              " & vbNewLine _
                                         & "     , FL.AUTO_DENP_NO  -- ADD 2016/06/29           " & vbNewLine _
                                         & "     , MSZ.SHIWAKE_CD   -- ADD 2016/06/29           " & vbNewLine _
                                         & "     , OUTL.CUST_ORD_NO  -- ADD 2016/06/29          " & vbNewLine _
                                         & "--(2012.08.06) フリーダイヤル出力対応 --- START --- " & vbNewLine _
                                         & "     , KB2.KBN_NM1                                  " & vbNewLine _
                                         & "--(2012.08.06) フリーダイヤル出力対応 ---  END  --- " & vbNewLine _
                                         & "--(2017/08/31) 運送会社CD出力対応 --- START ---     " & vbNewLine _
                                         & "     , FL.UNSO_CD                                   " & vbNewLine _
                                         & "     , FL.UNSO_BR_CD                                " & vbNewLine _
                                         & "     , FL.NRS_BR_CD                                 " & vbNewLine _
                                         & "--(2017.08.31 運送会社CD出力対応 ---  END  ---      " & vbNewLine _
                                         & "--(2017/09/11) トール対応 --- START ---             " & vbNewLine _
                                         & "     , MTOLL.CHAKU_CD                               " & vbNewLine _
                                         & "     , MTOLL.CHAKU_NM                               " & vbNewLine _
                                         & "--(2017.08.31 トール対応 ---  END  ---              " & vbNewLine

    ''' <summary>
    ''' データ抽出用GROUP BY(LMC821)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_LMC821 As String = " GROUP BY                   " & vbNewLine _
                                         & "       MR1.PTN_CD           " & vbNewLine _
                                         & "     , MR2.PTN_CD           " & vbNewLine _
                                         & "     , MR1.RPT_ID           " & vbNewLine _
                                         & "     , MR2.RPT_ID           " & vbNewLine _
                                         & "     , MR3.RPT_ID           " & vbNewLine _
                                         & "     , OUTL.NRS_BR_CD       " & vbNewLine _
                                         & "     , EDIL.OUTKA_CTL_NO    " & vbNewLine _
                                         & "     , OUTL.DEST_NM         " & vbNewLine _
                                         & "     , EDIL.DEST_NM         " & vbNewLine _
                                         & "     , DST.DEST_NM          " & vbNewLine _
                                         & "     , OUTL.OUTKA_PKG_NB    " & vbNewLine _
                                         & "     , OUTM.OUTKA_M_PKG_NB  " & vbNewLine _
                                         & "     , EDIL.OUTKA_CTL_NO    " & vbNewLine _
                                         & "     , OUTL.DEST_AD_1       " & vbNewLine _
                                         & "     , EDIL.DEST_AD_1       " & vbNewLine _
                                         & "     , DST.AD_1             " & vbNewLine _
                                         & "     , OUTL.DEST_AD_2       " & vbNewLine _
                                         & "     , EDIL.DEST_AD_2       " & vbNewLine _
                                         & "     , DST.AD_2             " & vbNewLine _
                                         & "     , OUTL.DEST_AD_3       " & vbNewLine _
                                         & "     , FL.AD_3              " & vbNewLine _
                                         & "     , CST.CUST_NM_L        " & vbNewLine _
                                         & "     , CST.DENPYO_NM        " & vbNewLine _
                                         & "     , UC.UNSOCO_NM         " & vbNewLine _
                                         & "     , OUTL.ARR_PLAN_DATE   " & vbNewLine _
                                         & "     , KB1.KBN_NM1          " & vbNewLine _
                                         & "     , NB.NRS_BR_NM         " & vbNewLine _
                                         & "     , OUTL.OUTKA_NO_L      " & vbNewLine _
                                         & "     , NB.TEL               " & vbNewLine _
                                         & "     , NB.FAX               " & vbNewLine _
                                         & "     , SO.WH_KB             " & vbNewLine _
                                         & "     , SO.TEL               " & vbNewLine _
                                         & "     , SO.FAX               " & vbNewLine _
                                         & "     , SHIPDEST.DEST_NM     " & vbNewLine _
                                         & "     , EDIL.SHIP_NM_L       " & vbNewLine _
                                         & "     , OUTL.DEST_KB         " & vbNewLine _
                                         & "     , CST.CUST_NM_S        " & vbNewLine _
                                         & "     , OUTL.PC_KB           " & vbNewLine _
                                         & "     , OUTL.CUST_CD_L       " & vbNewLine _
                                         & "     , MCD.SET_NAIYO        " & vbNewLine _
                                         & "     , MG.GOODS_CD_CUST     " & vbNewLine _
                                         & "     , MG.GOODS_NM_1        " & vbNewLine _
                                         & "     , MG.GOODS_NM_2        " & vbNewLine _
                                         & "     , MG.GOODS_NM_3        " & vbNewLine _
                                         & "     , OUTM.FREE_C01        " & vbNewLine _
                                         & "     , OUTM.FREE_C06        " & vbNewLine _
                                         & "     , OUTS.LOT_NO          " & vbNewLine _
                                         & "     , NB.AD_1              " & vbNewLine _
                                         & "     , NB.AD_2              " & vbNewLine _
                                         & "     , NB.AD_3              " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO      " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO_2    " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO_3    " & vbNewLine _
                                         & "     , EDIL.DEST_TEL        " & vbNewLine _
                                         & "     , FL.AUTO_DENP_NO  -- ADD 2016/06/29           " & vbNewLine _
                                         & "     , MSZ.SHIWAKE_CD   -- ADD 2016/06/29           " & vbNewLine _
                                         & "     , OUTL.CUST_ORD_NO  -- ADD 2016/06/29          " & vbNewLine _
                                         & "--(2012.08.06) フリーダイヤル出力対応 --- START --- " & vbNewLine _
                                         & "     , KB2.KBN_NM1                                  " & vbNewLine _
                                         & "--(2012.08.06) フリーダイヤル出力対応 ---  END  --- " & vbNewLine _
                                         & "--(2017/08/31) 運送会社CD出力対応 --- START ---     " & vbNewLine _
                                         & "     , FL.UNSO_CD                                   " & vbNewLine _
                                         & "     , FL.UNSO_BR_CD                                " & vbNewLine _
                                         & "     , FL.NRS_BR_CD                                 " & vbNewLine _
                                         & "--(2017.08.31 運送会社CD出力対応 ---  END  ---      " & vbNewLine _
                                         & "--(2017/09/11) トール対応 --- START ---             " & vbNewLine _
                                         & "     , MTOLL.CHAKU_CD                               " & vbNewLine _
                                         & "     , MTOLL.CHAKU_NM                               " & vbNewLine _
                                         & "--(2017.08.31 トール対応 ---  END  ---              " & vbNewLine

    ''' <summary>
    ''' データ抽出用GROUP BY (FFEM用追加)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_ADD_FJF As String = "     , FJF.ZFVYDTEKIYO " & vbNewLine


    ''' <summary>
    ''' ORDER BY（①出荷管理番号L)）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY             " & vbNewLine _
                                         & "      OUTL.NRS_BR_CD " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用1_LMC557向け
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMC557 As String = "SELECT                                                                             " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                  " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                  " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID                                                              " & vbNewLine _
                                            & " END                     AS RPT_ID                                                 " & vbNewLine _
                                            & ",OUTL.NRS_BR_CD          AS NRS_BR_CD                                              " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                   " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                   " & vbNewLine _
                                            & "      ELSE DST.DEST_NM                                                             " & vbNewLine _
                                            & " END                     AS DEST_NM                                                " & vbNewLine _
                                            & ",CASE WHEN OUTL.OUTKA_PKG_NB <> 0 THEN OUTL.OUTKA_PKG_NB                           " & vbNewLine _
                                            & "      ELSE SUM(OUTM.OUTKA_M_PKG_NB)                                                " & vbNewLine _
                                            & " END                     AS OUTKA_PKG_NB                                           " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                 " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                 " & vbNewLine _
                                            & "      ELSE DST.AD_1                                                                " & vbNewLine _
                                            & " END                     AS AD_1                                                   " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                            & "      ELSE DST.AD_2                                                                " & vbNewLine _
                                            & " END                     AS AD_2                                                   " & vbNewLine _
                                            & ",OUTL.DEST_AD_3          AS AD_3                                                   " & vbNewLine _
                                            & ",CST.CUST_NM_L           AS CUST_NM_L                                              " & vbNewLine _
                                            & ",UC.UNSOCO_NM            AS UNSOCO_NM                                              " & vbNewLine _
                                            & ",OUTL.ARR_PLAN_DATE      AS ARR_PLAN_DATE                                          " & vbNewLine _
                                            & ",KB1.KBN_NM1             AS ARR_PLAN_TIME                                          " & vbNewLine _
                                            & ",CASE WHEN MCD.SET_NAIYO <> NULL OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO         " & vbNewLine _
                                            & "      ELSE NB.NRS_BR_NM                                                            " & vbNewLine _
                                            & " END                     AS NRS_BR_NM                                              " & vbNewLine _
                                            & ",OUTL.OUTKA_NO_L         AS OUTKA_NO_L                                             " & vbNewLine _
                                            & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                " & vbNewLine _
                                            & "--, CASE WHEN SO.WH_KB = '01' THEN SO.TEL                                          " & vbNewLine _
                                            & "--  ELSE NB.TEL END      AS TEL                                                    " & vbNewLine _
                                            & "  , CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1                        " & vbNewLine _
                                            & "         WHEN SO.WH_KB  = '01' THEN SO.TEL                                         " & vbNewLine _
                                            & "    ELSE NB.TEL                                                                    " & vbNewLine _
                                            & "    END                  AS TEL                                                    " & vbNewLine _
                                            & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                " & vbNewLine _
                                            & ",CASE WHEN SO.WH_KB = '01' THEN SO.FAX                                             " & vbNewLine _
                                            & "      ELSE NB.FAX END               AS FAX                                         " & vbNewLine _
                                            & ",''                      AS TOP_FLAG                                               " & vbNewLine _
                                            & ",CST.CUST_NM_S           AS CUST_NM_S                                              " & vbNewLine _
                                            & ",OUTL.PC_KB              AS PC_KB                                                  " & vbNewLine _
                                            & ",OUTL.CUST_CD_L          AS CUST_CD_L                                              " & vbNewLine _
                                            & ",SHIPDEST.DEST_NM        AS SHIP_NM_L                                              " & vbNewLine _
                                            & ",CASE WHEN SHIPDEST.DEST_NM <> ''                                                  " & vbNewLine _
                                            & "           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM                  " & vbNewLine _
                                            & "      WHEN CST.DENPYO_NM   <> ''                                                   " & vbNewLine _
                                            & "           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                      " & vbNewLine _
                                            & "      ELSE CST.CUST_NM_L                                                           " & vbNewLine _
                                            & " END                         AS ATSUKAISYA_NM                                      " & vbNewLine _
                                            & "--,CASE WHEN SHIPDEST.DEST_NM <> ''                                                " & vbNewLine _
                                            & "--           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM                " & vbNewLine _
                                            & "--      WHEN CST.DENPYO_NM   <> ''                                                 " & vbNewLine _
                                            & "--           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                    " & vbNewLine _
                                            & "--      ELSE CST.CUST_NM_L                                                         " & vbNewLine _
                                            & "-- END                     AS ATSUKAISYA_NM                                        " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST        AS GOODS_CD_CUST                                          " & vbNewLine _
                                            & ",MG.GOODS_NM_1           AS GOODS_NM_1                                             " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                             " & vbNewLine _
                                            & ",CASE WHEN LEN(OUTM.FREE_C01) >= 8 THEN                                            " & vbNewLine _
                                            & "     CASE WHEN ISDATE(SUBSTRING(OUTM.FREE_C01,1,6) + '01') = 1 THEN SUBSTRING(OUTM.FREE_C01,1,6) + '01' " & vbNewLine _
                                            & "     ELSE '' END                                                                   " & vbNewLine _
                                            & " ELSE '' END  AS LT_DATE                                                           " & vbNewLine _
                                            & ",OUTS.LOT_NO             AS LOT_NO                                                 " & vbNewLine _
                                            & ",NB.AD_1                 AS NRS_BR_AD_1                                            " & vbNewLine _
                                            & ",NB.AD_2                 AS NRS_BR_AD_2                                            " & vbNewLine _
                                            & ",NB.AD_3                 AS NRS_BR_AD_3                                            " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                             " & vbNewLine _
                                            & ",MG.GOODS_NM_3           AS GOODS_NM_3                                             " & vbNewLine _
                                            & ",OUTM.FREE_C06           AS GOODS_SYUBETU                                          " & vbNewLine _
                                            & " --荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加                   " & vbNewLine _
                                            & ",MCD01.SET_NAIYO         AS SET_NAIYO                                              " & vbNewLine _
                                            & "--荷主明細マスタ   LMC552(埼玉浮間用)対応追加                                      " & vbNewLine _
                                            & ",MCD01.SET_NAIYO_2       AS SET_NAIYO_2                                            " & vbNewLine _
                                            & ",MCD01.SET_NAIYO_3       AS SET_NAIYO_3                                            " & vbNewLine _
                                            & ",DST.TEL                 AS DEST_TEL                                               " & vbNewLine _
                                            & "        --LMC557 千葉 ITW(荷主大：00555)                                           " & vbNewLine _
                                            & "--2013/03/15 要望番号1940 START                                                    " & vbNewLine _
                                            & "--,OUTM.ALCTD_NB           AS ALCTD_NB                                               " & vbNewLine _
                                            & ",OUTS.ALCTD_NB           AS ALCTD_NB                                               " & vbNewLine _
                                            & "--2013/03/15 要望番号1940  END                                                     " & vbNewLine _
                                            & ",OUTM.PRINT_SORT         AS PRINT_SORT                                             " & vbNewLine _
                                            & ",''                      AS UNSO_TTL_NB                                            " & vbNewLine _
                                            & ",''                      AS CUST_ORD_NO                                            " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句_LMC557向け
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_LMC557 As String = "FROM                                                                                      " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                                  " & vbNewLine _
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
                                     & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                          " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                    " & vbNewLine _
                                     & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                             " & vbNewLine _
                                     & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                            " & vbNewLine _
                                     & "--(2012.09.06)要望番号1412対応  ---  END  ---                                             " & vbNewLine _
                                     & "	--出荷M                                                            		              " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	   M.NRS_BR_CD                                                                        " & vbNewLine _
                                     & "	  ,M.GOODS_CD_NRS		                                                              " & vbNewLine _
                                     & "	  ,M.OUTKA_M_PKG_NB                                                                   " & vbNewLine _
                                     & "	  ,EDIM.FREE_C01                                                                      " & vbNewLine _
                                     & "	  ,EDIM.FREE_C06                                                                      " & vbNewLine _
                                     & "	  ,ROW_NUMBER() OVER (PARTITION BY M.NRS_BR_CD ORDER BY M.OUTKA_NO_L,M.OUTKA_NO_M) AS NUM   " & vbNewLine _
                                     & "   ,M.ALCTD_NB                                                                            " & vbNewLine _
                                     & "   ,M.OUTKA_NO_L                                                                          " & vbNewLine _
                                     & "   ,M.OUTKA_NO_M                                                                          " & vbNewLine _
                                     & "   ,M.PRINT_SORT                                                                          " & vbNewLine _
                                     & "	  FROM                                                                                " & vbNewLine _
                                     & "	   $LM_TRN$..C_OUTKA_M M                                                              " & vbNewLine _
                                     & "	   LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                              " & vbNewLine _
                                     & "	   ON M.NRS_BR_CD = EDIM.NRS_BR_CD                                                    " & vbNewLine _
                                     & "	   AND M.OUTKA_NO_L = EDIM.OUTKA_CTL_NO                                               " & vbNewLine _
                                     & "	   AND M.OUTKA_NO_M = EDIM.OUTKA_CTL_NO_CHU                                           " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	     M.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
                                     & "	    AND M.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND M.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "	 ) AS BASE                                                                            " & vbNewLine _
                                     & "--	 WHERE                                                                                " & vbNewLine _
                                     & "--	 BASE.NUM = 1                                                                         " & vbNewLine _
                                     & "	) OUTM                                                                                " & vbNewLine _
                                     & "	ON                                                                                    " & vbNewLine _
                                     & "--	1 = 1                                                                                 " & vbNewLine _
                                     & "         OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                " & vbNewLine _
                                     & "	--出荷S(LMC551 荷札(日医工用)対応)                                                    " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	        LOT_NO                                                                        " & vbNewLine _
                                     & "	      , ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD ORDER BY OUTKA_NO_L,OUTKA_NO_M,OUTKA_NO_S) AS NUM " & vbNewLine _
                                     & "          ,OUTKA_NO_M                                                                     " & vbNewLine _
                                     & "          ,OUTKA_NO_S                                                                     " & vbNewLine _
                                     & "--2013/03/15 要望番号1940 START                                                           " & vbNewLine _
                                     & "          ,ALCTD_NB                                                                       " & vbNewLine _
                                     & "--2013/03/15 要望番号1940  END                                                            " & vbNewLine _
                                     & "	   FROM                                                                               " & vbNewLine _
                                     & "	        $LM_TRN$..C_OUTKA_S S                                                         " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	        S.NRS_BR_CD  = @NRS_BR_CD                                                     " & vbNewLine _
                                     & "	    AND S.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND SYS_DEL_FLG  = '0'                                                            " & vbNewLine _
                                     & "	 ) AS BASES                                                                           " & vbNewLine _
                                     & "--	 WHERE                                                                                " & vbNewLine _
                                     & "--	 BASES.NUM = 1                                                                        " & vbNewLine _
                                     & "	) OUTS                                                                                " & vbNewLine _
                                     & " ON OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                     " & vbNewLine _
                                     & "--	1 = 1                                                                                 " & vbNewLine _
                                     & "--商品M                                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                                            " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                   " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                       " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                       " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                                 " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR1.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                             " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                       " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                         " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR2.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                             " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                                             " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR3.PTN_ID = '10'                                                                     " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                              " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--荷主マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST CST                                                            " & vbNewLine _
                                     & "ON  CST.NRS_BR_CD = MG.NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_L = MG.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_M = MG.CUST_CD_M                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_S = MG.CUST_CD_S                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_SS = MG.CUST_CD_SS                                                        " & vbNewLine _
                                     & "--荷主明細マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                    " & vbNewLine _
                                     & "ON  MCD.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MCD.CUST_CD = OUTL.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND MCD.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
                                     & "AND MCD.SUB_KB = '11'                                                                     " & vbNewLine _
                                     & "AND MCD.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--届先マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST DST                                                            " & vbNewLine _
                                     & "ON  DST.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND DST.CUST_CD_L = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND DST.DEST_CD   = OUTL.DEST_CD                                                          " & vbNewLine _
                                     & "--届先マスタ（売上先）                                                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST SHIPDEST                                                       " & vbNewLine _
                                     & "ON  SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                                                   " & vbNewLine _
                                     & "AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                                                   " & vbNewLine _
                                     & "--F運送L                                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..F_UNSO_L FL                                                           " & vbNewLine _
                                     & "ON  FL.MOTO_DATA_KB   = '20'                                                              " & vbNewLine _
                                     & "AND FL.NRS_BR_CD      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "AND FL.INOUTKA_NO_L   = OUTL.OUTKA_NO_L                                                   " & vbNewLine _
                                     & "AND FL.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                     & "--運送会社マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_UNSOCO UC                                                           " & vbNewLine _
                                     & "ON  UC.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND UC.UNSOCO_CD    = FL.UNSO_CD                                                          " & vbNewLine _
                                     & "AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                                                       " & vbNewLine _
                                     & "--営業所マスタ                                                                            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_NRS_BR NB                                                           " & vbNewLine _
                                     & "ON  NB.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "--倉庫マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SOKO SO                                                             " & vbNewLine _
                                     & "ON  SO.WH_CD = OUTL.WH_CD                                                                 " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB1                                                             " & vbNewLine _
                                     & "ON  KB1.KBN_GROUP_CD = 'N010'                                                             " & vbNewLine _
                                     & "AND KB1.KBN_CD= OUTL.ARR_PLAN_TIME                                                        " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                       " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB2                                                             " & vbNewLine _
                                     & "  ON KB2.KBN_GROUP_CD = 'N022'                                                            " & vbNewLine _
                                     & " AND KB2.KBN_CD       = '00'                                                              " & vbNewLine _
                                     & " AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                       " & vbNewLine _
                                     & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 Start                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                                                  " & vbNewLine _
                                     & "ON  MCD01.NRS_BR_CD = OUTL.NRS_BR_CD                                                      " & vbNewLine _
                                     & "AND MCD01.CUST_CD = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND MCD01.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND MCD01.SUB_KB = '29'                                                                   " & vbNewLine _
                                     & "AND MCD01.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                     & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 End                       " & vbNewLine _
                                     & "WHERE                                                                                     " & vbNewLine _
                                     & "    OUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                           " & vbNewLine _
                                     & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine


    ''' <summary>
    ''' データ抽出用GROUP BY_LMC557向け
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_LMC557 As String = " GROUP BY                   " & vbNewLine _
                                         & "       MR1.PTN_CD           " & vbNewLine _
                                         & "     , MR2.PTN_CD           " & vbNewLine _
                                         & "     , MR1.RPT_ID           " & vbNewLine _
                                         & "     , MR2.RPT_ID           " & vbNewLine _
                                         & "     , MR3.RPT_ID           " & vbNewLine _
                                         & "     , OUTL.NRS_BR_CD       " & vbNewLine _
                                         & "     , EDIL.OUTKA_CTL_NO    " & vbNewLine _
                                         & "     , OUTL.DEST_NM         " & vbNewLine _
                                         & "     , EDIL.DEST_NM         " & vbNewLine _
                                         & "     , DST.DEST_NM          " & vbNewLine _
                                         & "     , OUTL.OUTKA_PKG_NB    " & vbNewLine _
                                         & "     , OUTM.OUTKA_M_PKG_NB  " & vbNewLine _
                                         & "     , EDIL.OUTKA_CTL_NO    " & vbNewLine _
                                         & "     , OUTL.DEST_AD_1       " & vbNewLine _
                                         & "     , EDIL.DEST_AD_1       " & vbNewLine _
                                         & "     , DST.AD_1             " & vbNewLine _
                                         & "     , OUTL.DEST_AD_2       " & vbNewLine _
                                         & "     , EDIL.DEST_AD_2       " & vbNewLine _
                                         & "     , DST.AD_2             " & vbNewLine _
                                         & "     , OUTL.DEST_AD_3       " & vbNewLine _
                                         & "     , FL.AD_3              " & vbNewLine _
                                         & "     , CST.CUST_NM_L        " & vbNewLine _
                                         & "     , CST.DENPYO_NM        " & vbNewLine _
                                         & "     , UC.UNSOCO_NM         " & vbNewLine _
                                         & "     , OUTL.ARR_PLAN_DATE   " & vbNewLine _
                                         & "     , KB1.KBN_NM1          " & vbNewLine _
                                         & "     , NB.NRS_BR_NM         " & vbNewLine _
                                         & "     , OUTL.OUTKA_NO_L      " & vbNewLine _
                                         & "     , NB.TEL               " & vbNewLine _
                                         & "     , NB.FAX               " & vbNewLine _
                                         & "     , SO.WH_KB             " & vbNewLine _
                                         & "     , SO.TEL               " & vbNewLine _
                                         & "     , SO.FAX               " & vbNewLine _
                                         & "     , SHIPDEST.DEST_NM     " & vbNewLine _
                                         & "     , EDIL.SHIP_NM_L       " & vbNewLine _
                                         & "     , OUTL.DEST_KB         " & vbNewLine _
                                         & "     , CST.CUST_NM_S        " & vbNewLine _
                                         & "     , OUTL.PC_KB           " & vbNewLine _
                                         & "     , OUTL.CUST_CD_L       " & vbNewLine _
                                         & "     , MCD.SET_NAIYO        " & vbNewLine _
                                         & "     , MG.GOODS_CD_CUST     " & vbNewLine _
                                         & "     , MG.GOODS_NM_1        " & vbNewLine _
                                         & "     , MG.GOODS_NM_2        " & vbNewLine _
                                         & "     , MG.GOODS_NM_3        " & vbNewLine _
                                         & "     , OUTM.FREE_C01        " & vbNewLine _
                                         & "     , OUTM.FREE_C06        " & vbNewLine _
                                         & "     , OUTS.LOT_NO          " & vbNewLine _
                                         & "     , NB.AD_1              " & vbNewLine _
                                         & "     , NB.AD_2              " & vbNewLine _
                                         & "     , NB.AD_3              " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO      " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO_2    " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO_3    " & vbNewLine _
                                         & "     , DST.TEL              " & vbNewLine _
                                         & "--(2012.08.06) フリーダイヤル出力対応 --- START --- " & vbNewLine _
                                         & "     , KB2.KBN_NM1                                  " & vbNewLine _
                                         & "--(2012.08.06) フリーダイヤル出力対応 ---  END  --- " & vbNewLine _
                                         & "        --LMC557 千葉 ITW(荷主大：00555)                                          " & vbNewLine _
                                         & "     , OUTM.ALCTD_NB         " & vbNewLine _
                                         & "     , OUTM.PRINT_SORT       " & vbNewLine _
                                         & "--2013/03/15 要望番号1940 START       " & vbNewLine _
                                         & "     , OUTS.ALCTD_NB         " & vbNewLine _
                                         & "--2013/03/15 要望番号1940  END        " & vbNewLine



    ''' <summary>
    ''' ORDER BY_LMC557向け（①営業所コード②出荷管理番号(大)③印刷順④商品名１⑤ロットＮＯ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMC557 As String = "ORDER BY             " & vbNewLine _
                                         & "       OUTL.NRS_BR_CD  " & vbNewLine _
                                         & "      ,OUTL.OUTKA_NO_L " & vbNewLine _
                                         & "      ,OUTM.PRINT_SORT " & vbNewLine _
                                         & "      ,MG.GOODS_NM_1 " & vbNewLine _
                                         & "      ,OUTS.LOT_NO " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用1
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMC760 As String = "SELECT                                                                             " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                  " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                  " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID                                                              " & vbNewLine _
                                            & " END                     AS RPT_ID                                                 " & vbNewLine _
                                            & ",OUTL.NRS_BR_CD          AS NRS_BR_CD                                              " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                   " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                   " & vbNewLine _
                                            & "      ELSE DST.DEST_NM                                                             " & vbNewLine _
                                            & " END                     AS DEST_NM                                                " & vbNewLine _
                                            & ",CASE WHEN OUTL.OUTKA_PKG_NB <> 0 THEN OUTL.OUTKA_PKG_NB                           " & vbNewLine _
                                            & "      ELSE SUM(OUTM.OUTKA_M_PKG_NB)                                                " & vbNewLine _
                                            & " END                     AS OUTKA_PKG_NB                                           " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                 " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                 " & vbNewLine _
                                            & "      ELSE DST.AD_1                                                                " & vbNewLine _
                                            & " END                     AS AD_1                                                   " & vbNewLine _
                                            & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                            & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                            & "      ELSE DST.AD_2                                                                " & vbNewLine _
                                            & " END                     AS AD_2                                                   " & vbNewLine _
                                            & ",OUTL.DEST_AD_3          AS AD_3                                                   " & vbNewLine _
                                            & ",CST.CUST_NM_L           AS CUST_NM_L                                              " & vbNewLine _
                                            & ",UC.UNSOCO_NM            AS UNSOCO_NM                                              " & vbNewLine _
                                            & ",OUTL.ARR_PLAN_DATE      AS ARR_PLAN_DATE                                          " & vbNewLine _
                                            & ",KB1.KBN_NM1             AS ARR_PLAN_TIME                                          " & vbNewLine _
                                            & ",CASE WHEN MCD.SET_NAIYO <> NULL OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO         " & vbNewLine _
                                            & "      ELSE NB.NRS_BR_NM                                                            " & vbNewLine _
                                            & " END                     AS NRS_BR_NM                                              " & vbNewLine _
                                            & ",OUTL.OUTKA_NO_L         AS OUTKA_NO_L                                             " & vbNewLine _
                                            & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                " & vbNewLine _
                                            & "--, CASE WHEN SO.WH_KB = '01' THEN SO.TEL                                          " & vbNewLine _
                                            & "--  ELSE NB.TEL END      AS TEL                                                    " & vbNewLine _
                                            & "  , CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1                        " & vbNewLine _
                                            & "         WHEN SO.WH_KB  = '01' THEN SO.TEL                                         " & vbNewLine _
                                            & "    ELSE NB.TEL                                                                    " & vbNewLine _
                                            & "    END                  AS TEL                                                    " & vbNewLine _
                                            & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                " & vbNewLine _
                                            & ",CASE WHEN SO.WH_KB = '01' THEN SO.FAX                                             " & vbNewLine _
                                            & "      ELSE NB.FAX END               AS FAX                                         " & vbNewLine _
                                            & ",''                      AS TOP_FLAG                                               " & vbNewLine _
                                            & ",CST.CUST_NM_S           AS CUST_NM_S                                              " & vbNewLine _
                                            & ",OUTL.PC_KB              AS PC_KB                                                  " & vbNewLine _
                                            & ",OUTL.CUST_CD_L          AS CUST_CD_L                                              " & vbNewLine _
                                            & ",SHIPDEST.DEST_NM        AS SHIP_NM_L                                              " & vbNewLine _
                                            & ",CASE WHEN SHIPDEST.DEST_NM <> ''                                                  " & vbNewLine _
                                            & "           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM                  " & vbNewLine _
                                            & "      WHEN CST.DENPYO_NM   <> ''                                                   " & vbNewLine _
                                            & "           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                      " & vbNewLine _
                                            & "      ELSE CST.CUST_NM_L                                                           " & vbNewLine _
                                            & " END                         AS ATSUKAISYA_NM                                      " & vbNewLine _
                                            & "--,CASE WHEN SHIPDEST.DEST_NM <> ''                                                " & vbNewLine _
                                            & "--           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM                " & vbNewLine _
                                            & "--      WHEN CST.DENPYO_NM   <> ''                                                 " & vbNewLine _
                                            & "--           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                    " & vbNewLine _
                                            & "--      ELSE CST.CUST_NM_L                                                         " & vbNewLine _
                                            & "-- END                     AS ATSUKAISYA_NM                                        " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST        AS GOODS_CD_CUST                                          " & vbNewLine _
                                            & ",MG.GOODS_NM_1           AS GOODS_NM_1                                             " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                             " & vbNewLine _
                                            & ",CASE WHEN LEN(OUTM.FREE_C01) >= 8 THEN                                            " & vbNewLine _
                                            & "     CASE WHEN ISDATE(SUBSTRING(OUTM.FREE_C01,1,6) + '01') = 1 THEN SUBSTRING(OUTM.FREE_C01,1,6) + '01' " & vbNewLine _
                                            & "     ELSE '' END                                                                   " & vbNewLine _
                                            & " ELSE '' END  AS LT_DATE                                                           " & vbNewLine _
                                            & ",OUTS.LOT_NO             AS LOT_NO                                                 " & vbNewLine _
                                            & ",NB.AD_1                 AS NRS_BR_AD_1                                            " & vbNewLine _
                                            & ",NB.AD_2                 AS NRS_BR_AD_2                                            " & vbNewLine _
                                            & ",NB.AD_3                 AS NRS_BR_AD_3                                            " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                             " & vbNewLine _
                                            & ",MG.GOODS_NM_3           AS GOODS_NM_3                                             " & vbNewLine _
                                            & ",OUTM.FREE_C06           AS GOODS_SYUBETU                                          " & vbNewLine _
                                            & " --荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加                   " & vbNewLine _
                                            & ",MCD01.SET_NAIYO         AS SET_NAIYO                                              " & vbNewLine _
                                            & "--荷主明細マスタ   LMC552(埼玉浮間用)対応追加                                      " & vbNewLine _
                                            & ",MCD01.SET_NAIYO_2       AS SET_NAIYO_2                                            " & vbNewLine _
                                            & ",MCD01.SET_NAIYO_3       AS SET_NAIYO_3                                            " & vbNewLine _
                                            & ",DST.TEL                 AS DEST_TEL                                               " & vbNewLine _
                                            & ",       ''               AS ALCTD_NB                                               " & vbNewLine _
                                            & ",       ''               AS PRINT_SORT                                             " & vbNewLine _
                                            & ",       ''               AS UNSO_TTL_NB                                            " & vbNewLine _
                                            & ",DST.DEST_CD             AS DEST_CD                                                " & vbNewLine _
                                            & ",ISNULL(DSTD.SET_NAIYO,'') AS AREA_CD                                              " & vbNewLine _
                                            & ",OUTL.DENP_NO            AS SOKO_CD                                                " & vbNewLine _
                                            & ",OUTL.CUST_ORD_NO        AS CUST_ORD_NO                                            " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_LMC760 As String = "FROM                                                                                      " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                                  " & vbNewLine _
                                     & "--(2012.09.06)要望番号1412対応  --- START ---                                             " & vbNewLine _
                                     & "--出荷EDIL                                                                                " & vbNewLine _
                                     & "--LEFT JOIN                                                                               " & vbNewLine _
                                     & "--    (                                                                                   " & vbNewLine _
                                     & "--      SELECT                                                                            " & vbNewLine _
                                     & "--            MIN(DEST_NM)     AS DEST_NM                                                 " & vbNewLine _
                                     & "--           ,MIN(DEST_AD_1)     AS DEST_AD_1                                             " & vbNewLine _
                                     & "--           ,MIN(DEST_AD_2)   AS DEST_AD_2                                               " & vbNewLine _
                                     & "--           ,NRS_BR_CD                                                                   " & vbNewLine _
                                     & "--           ,OUTKA_CTL_NO                                                                " & vbNewLine _
                                     & "--           ,SHIP_NM_L                                                                   " & vbNewLine _
                                     & "--           ,SYS_DEL_FLG                                                                 " & vbNewLine _
                                     & "--      FROM                                                                              " & vbNewLine _
                                     & "--      $LM_TRN$..H_OUTKAEDI_L                                                            " & vbNewLine _
                                     & "--	  GROUP BY			                                                                  " & vbNewLine _
                                     & "--	        NRS_BR_CD		                                                              " & vbNewLine _
                                     & "--	       ,OUTKA_CTL_NO	                                                              " & vbNewLine _
                                     & "--	       ,SHIP_NM_L	                                                                  " & vbNewLine _
                                     & "--	       ,SYS_DEL_FLG		                                                              " & vbNewLine _
                                     & "--	 ) AS EDIL		                                                                      " & vbNewLine _
                                     & "--	ON  EDIL.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                     & "--	AND EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "--	AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                               " & vbNewLine _
                                     & "--下記の内容に変更                                                                        " & vbNewLine _
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
                                     & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                          " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                    " & vbNewLine _
                                     & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                             " & vbNewLine _
                                     & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                            " & vbNewLine _
                                     & "--(2012.09.06)要望番号1412対応  ---  END  ---                                             " & vbNewLine _
                                     & "	--出荷M                                                            		              " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	   M.NRS_BR_CD                                                                        " & vbNewLine _
                                     & "	  ,M.GOODS_CD_NRS		                                                              " & vbNewLine _
                                     & "	  ,M.OUTKA_M_PKG_NB                                                                   " & vbNewLine _
                                     & "	  ,EDIM.FREE_C01                                                                      " & vbNewLine _
                                     & "	  ,EDIM.FREE_C06                                                                      " & vbNewLine _
                                     & "	  ,ROW_NUMBER() OVER (PARTITION BY M.NRS_BR_CD ORDER BY M.OUTKA_NO_L,M.OUTKA_NO_M) AS NUM   " & vbNewLine _
                                     & "	  FROM                                                                                " & vbNewLine _
                                     & "	   $LM_TRN$..C_OUTKA_M M                                                              " & vbNewLine _
                                     & "	   LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                              " & vbNewLine _
                                     & "	   ON M.NRS_BR_CD = EDIM.NRS_BR_CD                                                    " & vbNewLine _
                                     & "	   AND M.OUTKA_NO_L = EDIM.OUTKA_CTL_NO                                               " & vbNewLine _
                                     & "	   AND M.OUTKA_NO_M = EDIM.OUTKA_CTL_NO_CHU                                           " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	     M.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
                                     & "	    AND M.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND M.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "	 ) AS BASE                                                                            " & vbNewLine _
                                     & "	 WHERE                                                                                " & vbNewLine _
                                     & "	 BASE.NUM = 1                                                                         " & vbNewLine _
                                     & "	) OUTM                                                                                " & vbNewLine _
                                     & "	ON                                                                                    " & vbNewLine _
                                     & "	1 = 1                                                                                 " & vbNewLine _
                                     & "	--出荷S(LMC551 荷札(日医工用)対応)                                                    " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	        LOT_NO                                                                        " & vbNewLine _
                                     & "	      , ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD ORDER BY OUTKA_NO_L,OUTKA_NO_M,OUTKA_NO_S) AS NUM " & vbNewLine _
                                     & "	   FROM                                                                               " & vbNewLine _
                                     & "	        $LM_TRN$..C_OUTKA_S S                                                         " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	        S.NRS_BR_CD  = @NRS_BR_CD                                                     " & vbNewLine _
                                     & "	    AND S.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND SYS_DEL_FLG  = '0'                                                            " & vbNewLine _
                                     & "	 ) AS BASES                                                                           " & vbNewLine _
                                     & "	 WHERE                                                                                " & vbNewLine _
                                     & "	 BASES.NUM = 1                                                                        " & vbNewLine _
                                     & "	) OUTS                                                                                " & vbNewLine _
                                     & "	ON                                                                                    " & vbNewLine _
                                     & "	1 = 1                                                                                 " & vbNewLine _
                                     & "--商品M                                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                                            " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                   " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                       " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                       " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                                 " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR1.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                             " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                       " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                         " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR2.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                             " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                                             " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR3.PTN_ID = '10'                                                                     " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                              " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--荷主マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST CST                                                            " & vbNewLine _
                                     & "ON  CST.NRS_BR_CD = MG.NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_L = MG.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_M = MG.CUST_CD_M                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_S = MG.CUST_CD_S                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_SS = MG.CUST_CD_SS                                                        " & vbNewLine _
                                     & "--荷主明細マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                    " & vbNewLine _
                                     & "ON  MCD.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MCD.CUST_CD = OUTL.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND MCD.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
                                     & "AND MCD.SUB_KB = '11'                                                                     " & vbNewLine _
                                     & "AND MCD.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--届先マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST DST                                                            " & vbNewLine _
                                     & "ON  DST.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND DST.CUST_CD_L = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND DST.DEST_CD   = OUTL.DEST_CD                                                          " & vbNewLine _
                                     & "--届先詳細マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST_DETAILS DSTD                                                   " & vbNewLine _
                                     & "ON  DST.NRS_BR_CD = DSTD.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND DST.CUST_CD_L = DSTD.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND DST.DEST_CD   = DSTD.DEST_CD                                                          " & vbNewLine _
                                     & "AND DSTD.SUB_KB = '05'                                                                    " & vbNewLine _
                                     & "--届先マスタ（売上先）                                                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST SHIPDEST                                                       " & vbNewLine _
                                     & "ON  SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                                                   " & vbNewLine _
                                     & "AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                                                   " & vbNewLine _
                                     & "--F運送L                                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..F_UNSO_L FL                                                           " & vbNewLine _
                                     & "ON  FL.MOTO_DATA_KB   = '20'                                                              " & vbNewLine _
                                     & "AND FL.NRS_BR_CD      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "AND FL.INOUTKA_NO_L   = OUTL.OUTKA_NO_L                                                   " & vbNewLine _
                                     & "AND FL.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                     & "--運送会社マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_UNSOCO UC                                                           " & vbNewLine _
                                     & "ON  UC.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND UC.UNSOCO_CD    = FL.UNSO_CD                                                          " & vbNewLine _
                                     & "AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                                                       " & vbNewLine _
                                     & "--営業所マスタ                                                                            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_NRS_BR NB                                                           " & vbNewLine _
                                     & "ON  NB.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "--倉庫マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SOKO SO                                                             " & vbNewLine _
                                     & "ON  SO.WH_CD = OUTL.WH_CD                                                                 " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB1                                                             " & vbNewLine _
                                     & "ON  KB1.KBN_GROUP_CD = 'N010'                                                             " & vbNewLine _
                                     & "AND KB1.KBN_CD= OUTL.ARR_PLAN_TIME                                                        " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                       " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB2                                                             " & vbNewLine _
                                     & "  ON KB2.KBN_GROUP_CD = 'N022'                                                            " & vbNewLine _
                                     & " AND KB2.KBN_CD       = '00'                                                              " & vbNewLine _
                                     & " AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                       " & vbNewLine _
                                     & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 Start                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                                                  " & vbNewLine _
                                     & "ON  MCD01.NRS_BR_CD = OUTL.NRS_BR_CD                                                      " & vbNewLine _
                                     & "AND MCD01.CUST_CD = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND MCD01.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND MCD01.SUB_KB = '29'                                                                   " & vbNewLine _
                                     & "AND MCD01.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                     & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加 End                       " & vbNewLine _
                                     & "WHERE                                                                                     " & vbNewLine _
                                     & "    OUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                           " & vbNewLine _
                                     & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine
    'END YANAI 要望番号1478 一括印刷が遅い

    ''' <summary>
    ''' データ抽出用GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_LMC760 As String = " GROUP BY                   " & vbNewLine _
                                         & "       MR1.PTN_CD           " & vbNewLine _
                                         & "     , MR2.PTN_CD           " & vbNewLine _
                                         & "     , MR1.RPT_ID           " & vbNewLine _
                                         & "     , MR2.RPT_ID           " & vbNewLine _
                                         & "     , MR3.RPT_ID           " & vbNewLine _
                                         & "     , OUTL.NRS_BR_CD       " & vbNewLine _
                                         & "     , EDIL.OUTKA_CTL_NO    " & vbNewLine _
                                         & "     , OUTL.DEST_NM         " & vbNewLine _
                                         & "     , EDIL.DEST_NM         " & vbNewLine _
                                         & "     , DST.DEST_NM          " & vbNewLine _
                                         & "     , OUTL.OUTKA_PKG_NB    " & vbNewLine _
                                         & "     , OUTM.OUTKA_M_PKG_NB  " & vbNewLine _
                                         & "     , EDIL.OUTKA_CTL_NO    " & vbNewLine _
                                         & "     , OUTL.DEST_AD_1       " & vbNewLine _
                                         & "     , EDIL.DEST_AD_1       " & vbNewLine _
                                         & "     , DST.AD_1             " & vbNewLine _
                                         & "     , OUTL.DEST_AD_2       " & vbNewLine _
                                         & "     , EDIL.DEST_AD_2       " & vbNewLine _
                                         & "     , DST.AD_2             " & vbNewLine _
                                         & "     , OUTL.DEST_AD_3       " & vbNewLine _
                                         & "     , FL.AD_3              " & vbNewLine _
                                         & "     , CST.CUST_NM_L        " & vbNewLine _
                                         & "     , CST.DENPYO_NM        " & vbNewLine _
                                         & "     , UC.UNSOCO_NM         " & vbNewLine _
                                         & "     , OUTL.ARR_PLAN_DATE   " & vbNewLine _
                                         & "     , KB1.KBN_NM1          " & vbNewLine _
                                         & "     , NB.NRS_BR_NM         " & vbNewLine _
                                         & "     , OUTL.OUTKA_NO_L      " & vbNewLine _
                                         & "     , NB.TEL               " & vbNewLine _
                                         & "     , NB.FAX               " & vbNewLine _
                                         & "     , SO.WH_KB             " & vbNewLine _
                                         & "     , SO.TEL               " & vbNewLine _
                                         & "     , SO.FAX               " & vbNewLine _
                                         & "     , SHIPDEST.DEST_NM     " & vbNewLine _
                                         & "     , EDIL.SHIP_NM_L       " & vbNewLine _
                                         & "     , OUTL.DEST_KB         " & vbNewLine _
                                         & "     , CST.CUST_NM_S        " & vbNewLine _
                                         & "     , OUTL.PC_KB           " & vbNewLine _
                                         & "     , OUTL.CUST_CD_L       " & vbNewLine _
                                         & "     , MCD.SET_NAIYO        " & vbNewLine _
                                         & "     , MG.GOODS_CD_CUST     " & vbNewLine _
                                         & "     , MG.GOODS_NM_1        " & vbNewLine _
                                         & "     , MG.GOODS_NM_2        " & vbNewLine _
                                         & "     , MG.GOODS_NM_3        " & vbNewLine _
                                         & "     , OUTM.FREE_C01        " & vbNewLine _
                                         & "     , OUTM.FREE_C06        " & vbNewLine _
                                         & "     , OUTS.LOT_NO          " & vbNewLine _
                                         & "     , NB.AD_1              " & vbNewLine _
                                         & "     , NB.AD_2              " & vbNewLine _
                                         & "     , NB.AD_3              " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO      " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO_2    " & vbNewLine _
                                         & "     , MCD01.SET_NAIYO_3    " & vbNewLine _
                                         & "     , DST.TEL              " & vbNewLine _
                                         & "--(2012.08.06) フリーダイヤル出力対応 --- START --- " & vbNewLine _
                                         & "     , KB2.KBN_NM1                                  " & vbNewLine _
                                         & "--(2012.08.06) フリーダイヤル出力対応 ---  END  --- " & vbNewLine _
                                         & "     , DST.DEST_CD                                  " & vbNewLine _
                                         & "     , DSTD.SET_NAIYO                               " & vbNewLine _
                                         & "     , OUTL.DENP_NO                                 " & vbNewLine _
                                         & "     , OUTL.CUST_ORD_NO                             " & vbNewLine

    ''' <summary>
    ''' 運送データ取得1
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNSO_DATA_LMC760 As String = "SELECT DISTINCT                                                                   " & vbNewLine _
                                                 & "   CASE  WHEN M22_02.PTN_CD IS NOT NULL THEN M22_02.RPT_ID                        " & vbNewLine _
                                                 & "         WHEN M22_01.PTN_CD IS NOT NULL THEN M22_01.RPT_ID                        " & vbNewLine _
                                                 & "         ELSE M22_03.RPT_ID END                            AS RPT_ID              " & vbNewLine _
                                                 & "        ,M10_01.DEST_NM                                    AS DEST_NM             " & vbNewLine _
                                                 & "        ,F02_01.UNSO_PKG_NB                                AS OUTKA_PKG_NB        " & vbNewLine _
                                                 & "        ,M10_01.AD_1                                       AS AD_1                " & vbNewLine _
                                                 & "        ,M10_01.AD_2                                       AS AD_2                " & vbNewLine _
                                                 & "        ,F02_01.AD_3                                       AS AD_3                " & vbNewLine _
                                                 & "        ,M07_01.CUST_NM_L                                  AS CUST_NM_L           " & vbNewLine _
                                                 & "        ,M36_01.UNSOCO_NM                                  AS UNSOCO_NM           " & vbNewLine _
                                                 & "        ,F02_01.ARR_PLAN_DATE                              AS ARR_PLAN_DATE       " & vbNewLine _
                                                 & "        ,F02_01.ARR_PLAN_TIME                              AS ARR_PLAN_TIME       " & vbNewLine _
                                                 & "        ,M01_01.NRS_BR_NM                                  AS NRS_BR_NM           " & vbNewLine _
                                                 & "        ,F02_01.UNSO_NO_L                                  AS OUTKA_NO_L          " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                               " & vbNewLine _
                                                 & "--      ,M01_01.TEL                                        AS TEL                 " & vbNewLine _
                                                 & "        ,CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1                  " & vbNewLine _
                                                 & "         ELSE M01_01.TEL                                                          " & vbNewLine _
                                                 & "         END                                               AS TEL                 " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                               " & vbNewLine _
                                                 & "        ,M01_01.FAX                                        AS FAX                 " & vbNewLine _
                                                 & "        ,M07_01.CUST_NM_S                                  AS CUST_NM_S           " & vbNewLine _
                                                 & "        ,F02_01.PC_KB                                      AS PC_KB               " & vbNewLine _
                                                 & "        ,SHIPDEST.DEST_NM                                  AS SHIP_NM_L           " & vbNewLine _
                                                 & "        ,CASE WHEN SHIPDEST.DEST_NM <> ''                                         " & vbNewLine _
                                                 & "                   AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM         " & vbNewLine _
                                                 & "              WHEN DENPYO_NM   <> ''                                              " & vbNewLine _
                                                 & "                   AND M07_01.DENPYO_NM IS NOT NULL   THEN M07_01.DENPYO_NM       " & vbNewLine _
                                                 & "              ELSE M07_01.CUST_NM_L                                               " & vbNewLine _
                                                 & "         END                     AS ATSUKAISYA_NM                                 " & vbNewLine _
                                                 & "        ,M08_01.GOODS_CD_CUST    AS GOODS_CD_CUST                                 " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_1    " & vbNewLine _
                                                 & "              ELSE F03_01.GOODS_NM                                                " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_1                                    " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_2    " & vbNewLine _
                                                 & "              ELSE F03_01.GOODS_NM                                                " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_2                                    " & vbNewLine _
                                                 & "        ,''                      AS LT_DATE                                       " & vbNewLine _
                                                 & "        ,F03_01.LOT_NO           AS LOT_NO                                        " & vbNewLine _
                                                 & "        ,M01_01.AD_1             AS NRS_BR_AD_1                                   " & vbNewLine _
                                                 & "        ,M01_01.AD_2             AS NRS_BR_AD_2                                   " & vbNewLine _
                                                 & "        ,M01_01.AD_3             AS NRS_BR_AD_3                                   " & vbNewLine _
                                                 & "        ,CASE WHEN M08_01.GOODS_CD_CUST <> ''                                     " & vbNewLine _
                                                 & "                   AND M08_01.GOODS_CD_CUST IS NOT NULL THEN M08_01.GOODS_NM_3    " & vbNewLine _
                                                 & "              ELSE ''                                                             " & vbNewLine _
                                                 & "         END                     AS GOODS_NM_3                                    " & vbNewLine _
                                                 & "        ,''                      AS GOODS_SYUBETU                                 " & vbNewLine _
                                                 & "        --荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加           " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO          AS SET_NAIYO                                     " & vbNewLine _
                                                 & "        --荷主明細マスタ   LMC552(埼玉浮間用)対応追加                             " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO_2          AS SET_NAIYO_2                                 " & vbNewLine _
                                                 & ",       MCD01.SET_NAIYO_3          AS SET_NAIYO_3                                 " & vbNewLine _
                                                 & ",       M10_01.TEL                 AS DEST_TEL                                    " & vbNewLine _
                                                 & ",       ''                      AS ALCTD_NB                                       " & vbNewLine _
                                                 & ",       ''                      AS PRINT_SORT                                     " & vbNewLine _
                                                 & ",       ''                      AS UNSO_TTL_NB                                    " & vbNewLine _
                                                 & ",       M10_01.DEST_CD          AS DEST_CD                                        " & vbNewLine _
                                                 & ",       ISNULL(M11_01.SET_NAIYO,'') AS AREA_CD                                    " & vbNewLine _
                                                 & ",       ''                      AS SOKO_CD                                        " & vbNewLine _
                                                 & ",       F02_01.CUST_REF_NO      AS CUST_ORD_NO                                    " & vbNewLine

    ''' <summary>
    ''' 運送データ取得4
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_FROM_UNSO_DATA4_LMC760 As String = "            ,''                                                AS TOP_FLAG     " & vbNewLine _
                                                 & "  FROM       $LM_TRN$..F_UNSO_L F02_01                                        " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_NRS_BR M01_01                                        " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M01_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  M01_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_CUST     M07_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M07_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = M07_01.CUST_CD_L                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_M   = M07_01.CUST_CD_M                            " & vbNewLine _
                                                 & "        AND  M07_01.CUST_CD_S   = '00'                                        " & vbNewLine _
                                                 & "        AND  M07_01.CUST_CD_SS  = '00'                                        " & vbNewLine _
                                                 & "        AND  M07_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_DEST     M10_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M10_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = M10_01.CUST_CD_L                            " & vbNewLine _
                                                 & "        AND  F02_01.DEST_CD     = M10_01.DEST_CD                              " & vbNewLine _
                                                 & "        AND  M10_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_DEST_DETAILS     M11_01                              " & vbNewLine _
                                                 & "         ON  M11_01.NRS_BR_CD   = M10_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  M11_01.CUST_CD_L   = M10_01.CUST_CD_L                            " & vbNewLine _
                                                 & "        AND  M11_01.DEST_CD     = M10_01.DEST_CD                              " & vbNewLine _
                                                 & "        AND  M11_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "        AND  M11_01.SUB_KB = '05'                                             " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_UNSOCO   M36_01                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = M36_01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.UNSO_CD     = M36_01.UNSOCO_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.UNSO_BR_CD  = M36_01.UNSOCO_BR_CD                         " & vbNewLine _
                                                 & "        AND  M36_01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..M_DEST   SHIPDEST                                      " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = SHIPDEST.NRS_BR_CD                          " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = SHIPDEST.CUST_CD_L                          " & vbNewLine _
                                                 & "        AND  F02_01.SHIP_CD     = SHIPDEST.DEST_CD                            " & vbNewLine _
                                                 & "        AND  SHIPDEST.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加               " & vbNewLine _
                                                 & "  LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                                   " & vbNewLine _
                                                 & "         ON  F02_01.NRS_BR_CD   = MCD01.NRS_BR_CD                            " & vbNewLine _
                                                 & "        AND  F02_01.CUST_CD_L   = MCD01.CUST_CD                              " & vbNewLine _
                                                 & "        AND  MCD01.SUB_KB = '29'                                             " & vbNewLine _
                                                 & "        AND  MCD01.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                           " & vbNewLine _
                                                 & "--区分マスタ                                                                  " & vbNewLine _
                                                 & "  LEFT JOIN  $LM_MST$..Z_KBN KB2                                              " & vbNewLine _
                                                 & "         ON  KB2.KBN_GROUP_CD = 'N022'                                        " & vbNewLine _
                                                 & "        AND  KB2.KBN_CD  = '00'                                               " & vbNewLine _
                                                 & "        AND  KB2.KBN_NM2 = F02_01.NRS_BR_CD                                   " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                           " & vbNewLine

#End Region

#Region "詰め合わせ"
    ''' <summary>
    ''' LMC551（日医工用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LMC551 As String = "SELECT                                                                " & vbNewLine _
                                        & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                           " & vbNewLine _
                                        & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                           " & vbNewLine _
                                        & "      ELSE MR3.RPT_ID                                                       " & vbNewLine _
                                        & " END                     AS RPT_ID                                          " & vbNewLine _
                                        & ",OUTL.NRS_BR_CD          AS NRS_BR_CD                                       " & vbNewLine _
                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                            " & vbNewLine _
                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                            " & vbNewLine _
                                        & "      ELSE DST.DEST_NM                                                      " & vbNewLine _
                                        & " END                     AS DEST_NM                                         " & vbNewLine _
                                        & ",CASE WHEN OUTL.OUTKA_PKG_NB <> 0 THEN OUTL.OUTKA_PKG_NB                    " & vbNewLine _
                                        & "      ELSE SUM(OUTM.OUTKA_M_PKG_NB)                                         " & vbNewLine _
                                        & " END                     AS OUTKA_PKG_NB                                    " & vbNewLine _
                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                          " & vbNewLine _
                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                          " & vbNewLine _
                                        & "      ELSE DST.AD_1                                                         " & vbNewLine _
                                        & " END                     AS AD_1                                            " & vbNewLine _
                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                          " & vbNewLine _
                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                          " & vbNewLine _
                                        & "      ELSE DST.AD_2                                                         " & vbNewLine _
                                        & " END                     AS AD_2                                            " & vbNewLine _
                                        & ",OUTL.DEST_AD_3          AS AD_3                                            " & vbNewLine _
                                        & ",CST.CUST_NM_L           AS CUST_NM_L                                       " & vbNewLine _
                                        & ",UC.UNSOCO_NM            AS UNSOCO_NM                                       " & vbNewLine _
                                        & ",OUTL.ARR_PLAN_DATE      AS ARR_PLAN_DATE                                   " & vbNewLine _
                                        & ",KB1.KBN_NM1             AS ARR_PLAN_TIME                                   " & vbNewLine _
                                        & ",CASE WHEN MCD.SET_NAIYO <> NULL OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO  " & vbNewLine _
                                        & "      ELSE NB.NRS_BR_NM                                                     " & vbNewLine _
                                        & " END                     AS NRS_BR_NM                                       " & vbNewLine _
                                        & ",OUTL.OUTKA_NO_L         AS OUTKA_NO_L                                      " & vbNewLine _
                                        & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                         " & vbNewLine _
                                        & "--, CASE WHEN SO.WH_KB = '01' THEN SO.TEL                                   " & vbNewLine _
                                        & "--  ELSE NB.TEL END      AS TEL                                             " & vbNewLine _
                                        & "  , CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1                 " & vbNewLine _
                                        & "         WHEN SO.WH_KB  = '01' THEN SO.TEL                                  " & vbNewLine _
                                        & "    ELSE NB.TEL                                                             " & vbNewLine _
                                        & "    END                  AS TEL                                             " & vbNewLine _
                                        & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                         " & vbNewLine _
                                        & ",CASE WHEN SO.WH_KB = '01' THEN SO.FAX                                      " & vbNewLine _
                                        & "      ELSE NB.FAX END               AS FAX                                  " & vbNewLine _
                                        & ",''                      AS TOP_FLAG                                        " & vbNewLine _
                                        & ",CST.CUST_NM_S           AS CUST_NM_S                                       " & vbNewLine _
                                        & ",OUTL.PC_KB              AS PC_KB                                           " & vbNewLine _
                                        & ",OUTL.CUST_CD_L          AS CUST_CD_L                                       " & vbNewLine _
                                        & ",SHIPDEST.DEST_NM        AS SHIP_NM_L                                       " & vbNewLine _
                                        & ",CASE WHEN SHIPDEST.DEST_NM <> ''                                           " & vbNewLine _
                                        & "           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM           " & vbNewLine _
                                        & "      WHEN CST.DENPYO_NM   <> ''                                            " & vbNewLine _
                                        & "           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM               " & vbNewLine _
                                        & "      ELSE CST.CUST_NM_L                                                    " & vbNewLine _
                                        & " END                         AS ATSUKAISYA_NM                               " & vbNewLine _
                                        & ",CASE WHEN SHIPDEST.DEST_NM <> ''                                           " & vbNewLine _
                                        & "           AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM           " & vbNewLine _
                                        & "      WHEN CST.DENPYO_NM   <> ''                                            " & vbNewLine _
                                        & "           AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM               " & vbNewLine _
                                        & "      ELSE CST.CUST_NM_L                                                    " & vbNewLine _
                                        & " END                     AS ATSUKAISYA_NM                                   " & vbNewLine _
                                        & ",''                      AS GOODS_CD_CUST                                   " & vbNewLine _
                                        & ",''                      AS GOODS_NM_1                                      " & vbNewLine _
                                        & ",''                      AS GOODS_NM_2                                      " & vbNewLine _
                                        & ",''                      AS LT_DATE                                         " & vbNewLine _
                                        & ",''                      AS LOT_NO                                          " & vbNewLine _
                                        & ",NB.AD_1                 AS NRS_BR_AD_1                                     " & vbNewLine _
                                        & ",NB.AD_2                 AS NRS_BR_AD_2                                     " & vbNewLine _
                                        & ",NB.AD_3                 AS NRS_BR_AD_3                                     " & vbNewLine _
                                        & ",''                      AS GOODS_NM_3                                      " & vbNewLine _
                                        & ",''                      AS GOODS_SYUBETU                                   " & vbNewLine _
                                        & "--荷主明細マスタ   LMC552(埼玉[50]_日本ファイン[00000])対応追加             " & vbNewLine _
                                        & ",''                      AS SET_NAIYO                                       " & vbNewLine _
                                        & "--荷主明細マスタ   LMC553(埼玉浮間用)対応追加                               " & vbNewLine _
                                        & ",''                      AS SET_NAIYO_2                                     " & vbNewLine _
                                        & ",''                      AS SET_NAIYO_3                                     " & vbNewLine _
                                        & ",''                      AS DEST_TEL                                        " & vbNewLine _
                                        & ",FL.AUTO_DENP_NO         AS AUTO_DENP_NO           --ADD 2016/08/29         " & vbNewLine _
                                        & ",ISNULL(MSZ.SHIWAKE_CD,'')  AS SHIWAKE_CD          --UPD 2016/08/29         " & vbNewLine _
                                        & ",''                      AS CUST_CD_L              --ADD 2016/06/29         " & vbNewLine _
                                        & ",''                      AS CUST_ORD_NO            --ADD 2016/06/29         " & vbNewLine _
                                        & ",FL.UNSO_CD              AS UNSO_CD                --ADD 2017/08/31         " & vbNewLine _
                                        & ",FL.UNSO_BR_CD           AS UNSO_BR_CD             --ADD 2017/08/31         " & vbNewLine _
                                        & ",FL.NRS_BR_CD            AS NRS_BR_CD              --ADD 2017/08/31         " & vbNewLine



    ''' <summary>
    ''' データ抽出用FROM句 LMC551用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_LMC551 As String = "FROM                                                                               " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                                  " & vbNewLine _
                                     & "--出荷EDIL                                                                                " & vbNewLine _
                                     & "--LEFT JOIN                                                                               " & vbNewLine _
                                     & "--    (                                                                                   " & vbNewLine _
                                     & "--      SELECT                                                                            " & vbNewLine _
                                     & "--            MIN(DEST_NM)     AS DEST_NM                                                 " & vbNewLine _
                                     & "--           ,MIN(DEST_AD_1)     AS DEST_AD_1                                             " & vbNewLine _
                                     & "--           ,MIN(DEST_AD_2)   AS DEST_AD_2                                               " & vbNewLine _
                                     & "--           ,NRS_BR_CD                                                                   " & vbNewLine _
                                     & "--           ,OUTKA_CTL_NO                                                                " & vbNewLine _
                                     & "--       ,SHIP_NM_L                                                                       " & vbNewLine _
                                     & "--         ,SYS_DEL_FLG                                                                   " & vbNewLine _
                                     & "--	  FROM 		                                                                          " & vbNewLine _
                                     & "--		$LM_TRN$..H_OUTKAEDI_L		                                                      " & vbNewLine _
                                     & "--	  GROUP BY			                                                                  " & vbNewLine _
                                     & "--	        NRS_BR_CD		                                                              " & vbNewLine _
                                     & "--	       ,OUTKA_CTL_NO	                                                              " & vbNewLine _
                                     & "--	       ,SHIP_NM_L	                                                                  " & vbNewLine _
                                     & "--	       ,SYS_DEL_FLG		                                                              " & vbNewLine _
                                     & "--	 ) AS EDIL		                                                                      " & vbNewLine _
                                     & "--	ON  EDIL.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                     & "--	AND EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "--	AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                               " & vbNewLine _
                                     & "--下記の内容に変更                                                                        " & vbNewLine _
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
                                     & "                  ) EBASE                                                                 " & vbNewLine _
                                     & "            WHERE EBASE.IDX = 1                                                           " & vbNewLine _
                                     & "            ) TOPEDI                                                                      " & vbNewLine _
                                     & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                          " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                    " & vbNewLine _
                                     & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                             " & vbNewLine _
                                     & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                            " & vbNewLine _
                                     & "--(2012.09.06)要望番号1412対応  ---  END  ---                                             " & vbNewLine _
                                     & "	--出荷M                                                            		              " & vbNewLine _
                                     & "	LEFT JOIN                                                                  		      " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	*                                                                                     " & vbNewLine _
                                     & "	FROM                                                                                  " & vbNewLine _
                                     & "	(SELECT                                                                               " & vbNewLine _
                                     & "	   NRS_BR_CD                                                                          " & vbNewLine _
                                     & "	  ,GOODS_CD_NRS		                                                                  " & vbNewLine _
                                     & "	  ,OUTKA_M_PKG_NB                                                                     " & vbNewLine _
                                     & "	  ,OUTKA_NO_L                                                                         " & vbNewLine _
                                     & "	  ,OUTKA_NO_M                                                                         " & vbNewLine _
                                     & "	  ,ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD ORDER BY OUTKA_NO_L,OUTKA_NO_M) AS NUM   " & vbNewLine _
                                     & "	  FROM                                                                                " & vbNewLine _
                                     & "	   $LM_TRN$..C_OUTKA_M M                                                              " & vbNewLine _
                                     & "	  WHERE                                                                               " & vbNewLine _
                                     & "	     M.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
                                     & "	    AND M.OUTKA_NO_L = @OUTKA_NO_L                                                    " & vbNewLine _
                                     & "	    AND SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                     & "	 ) AS BASE                                                                            " & vbNewLine _
                                     & "	 WHERE                                                                                " & vbNewLine _
                                     & "	 BASE.NUM = 1                                                                         " & vbNewLine _
                                     & "	) OUTM                                                                                " & vbNewLine _
                                     & "	ON                                                                                    " & vbNewLine _
                                     & "	1 = 1                                                              		              " & vbNewLine _
                                     & "--商品M                                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                                            " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                   " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                       " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                       " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                       " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                                 " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR1.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                             " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                       " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                         " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                         " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '10'                                                                    " & vbNewLine _
                                     & "AND MCR2.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "--帳票パターン取得                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                             " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                              " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                              " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                                             " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MR3.PTN_ID = '10'                                                                     " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                              " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--荷主マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST CST                                                            " & vbNewLine _
                                     & "ON  CST.NRS_BR_CD = MG.NRS_BR_CD                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_L = MG.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_M = MG.CUST_CD_M                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_S = MG.CUST_CD_S                                                          " & vbNewLine _
                                     & "AND CST.CUST_CD_SS = MG.CUST_CD_SS                                                        " & vbNewLine _
                                     & "--荷主明細マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                    " & vbNewLine _
                                     & "ON  MCD.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND MCD.CUST_CD = OUTL.CUST_CD_L                                                          " & vbNewLine _
                                     & "AND MCD.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
                                     & "AND MCD.SUB_KB = '11'                                                                     " & vbNewLine _
                                     & "AND MCD.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                     & "--届先マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST DST                                                            " & vbNewLine _
                                     & "ON  DST.NRS_BR_CD = OUTL.NRS_BR_CD                                                        " & vbNewLine _
                                     & "AND DST.CUST_CD_L = OUTL.CUST_CD_L                                                        " & vbNewLine _
                                     & "AND DST.DEST_CD   = OUTL.DEST_CD                                                          " & vbNewLine _
                                     & "--届先マスタ（売上先）                                                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST SHIPDEST                                                       " & vbNewLine _
                                     & "ON  SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                     & "AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                                                   " & vbNewLine _
                                     & "AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                                                   " & vbNewLine _
                                     & "--F運送L                                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..F_UNSO_L FL                                                           " & vbNewLine _
                                     & "ON  FL.MOTO_DATA_KB   = '20'                                                              " & vbNewLine _
                                     & "AND FL.NRS_BR_CD      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "AND FL.INOUTKA_NO_L   = OUTL.OUTKA_NO_L                                                   " & vbNewLine _
                                     & "AND FL.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                     & "--運送会社マスタ                                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_UNSOCO UC                                                           " & vbNewLine _
                                     & "ON  UC.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "AND UC.UNSOCO_CD    = FL.UNSO_CD                                                          " & vbNewLine _
                                     & "AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                                                       " & vbNewLine _
                                     & "--営業所マスタ                                                                            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_NRS_BR NB                                                           " & vbNewLine _
                                     & "ON  NB.NRS_BR_CD = OUTL.NRS_BR_CD                                                         " & vbNewLine _
                                     & "--倉庫マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SOKO SO                                                             " & vbNewLine _
                                     & "ON  SO.WH_CD = OUTL.WH_CD                                                                 " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB1                                                             " & vbNewLine _
                                     & "ON  KB1.KBN_GROUP_CD = 'N010'                                                             " & vbNewLine _
                                     & "AND KB1.KBN_CD= OUTL.ARR_PLAN_TIME                                                        " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                                       " & vbNewLine _
                                     & "--区分マスタ                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KB2                                                             " & vbNewLine _
                                     & "  ON KB2.KBN_GROUP_CD = 'N022'                                                            " & vbNewLine _
                                     & " AND KB2.KBN_CD       = '00'                                                              " & vbNewLine _
                                     & " AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                                       " & vbNewLine _
                                     & "--ADD 2016/08/29 西濃ZIP   西濃対応応追加 Start                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SEINO_ZIP MSZ                                                       " & vbNewLine _
                                     & "  ON MSZ.ZIP_NO   = DST.ZIP                                                               " & vbNewLine _
                                     & " AND MSZ.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "WHERE                                                                                     " & vbNewLine _
                                     & "    OUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                     & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine _
                                     & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                           " & vbNewLine
    ''' <summary>
    ''' データ抽出用GROUP BY  LMC551用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_LMC551 As String = " GROUP BY  MR1.PTN_CD              " & vbNewLine _
                                                & " ,MR2.PTN_CD                       " & vbNewLine _
                                                & " ,MR1.RPT_ID                       " & vbNewLine _
                                                & " ,MR2.RPT_ID                       " & vbNewLine _
                                                & " ,MR3.RPT_ID                       " & vbNewLine _
                                                & " ,OUTL.NRS_BR_CD                   " & vbNewLine _
                                                & " ,EDIL.OUTKA_CTL_NO                " & vbNewLine _
                                                & " ,OUTL.DEST_NM                     " & vbNewLine _
                                                & " ,EDIL.DEST_NM                     " & vbNewLine _
                                                & " ,DST.DEST_NM                      " & vbNewLine _
                                                & " ,OUTL.OUTKA_PKG_NB                " & vbNewLine _
                                                & " --,OUTM.OUTKA_M_PKG_NB              " & vbNewLine _
                                                & " ,EDIL.OUTKA_CTL_NO                " & vbNewLine _
                                                & " ,OUTL.DEST_AD_1                   " & vbNewLine _
                                                & " ,EDIL.DEST_AD_1                   " & vbNewLine _
                                                & " ,DST.AD_1                         " & vbNewLine _
                                                & " ,OUTL.DEST_AD_2                   " & vbNewLine _
                                                & " ,EDIL.DEST_AD_2                   " & vbNewLine _
                                                & " ,DST.AD_2                         " & vbNewLine _
                                                & " ,OUTL.DEST_AD_3                   " & vbNewLine _
                                                & " ,FL.AD_3                          " & vbNewLine _
                                                & " ,CST.CUST_NM_L                    " & vbNewLine _
                                                & " ,CST.DENPYO_NM                    " & vbNewLine _
                                                & " ,UC.UNSOCO_NM                     " & vbNewLine _
                                                & " ,OUTL.ARR_PLAN_DATE               " & vbNewLine _
                                                & " ,KB1.KBN_NM1                      " & vbNewLine _
                                                & " ,NB.NRS_BR_NM                     " & vbNewLine _
                                                & " ,OUTL.OUTKA_NO_L                  " & vbNewLine _
                                                & " ,NB.TEL                           " & vbNewLine _
                                                & " ,NB.FAX                           " & vbNewLine _
                                                & " ,SO.WH_KB                         " & vbNewLine _
                                                & " ,SO.TEL                           " & vbNewLine _
                                                & " ,SO.FAX                           " & vbNewLine _
                                                & " ,SHIPDEST.DEST_NM                 " & vbNewLine _
                                                & " ,EDIL.SHIP_NM_L                   " & vbNewLine _
                                                & " ,OUTL.DEST_KB                     " & vbNewLine _
                                                & " ,CST.CUST_NM_S                    " & vbNewLine _
                                                & " ,OUTL.PC_KB                       " & vbNewLine _
                                                & " ,OUTL.CUST_CD_L                   " & vbNewLine _
                                                & " ,MCD.SET_NAIYO                    " & vbNewLine _
                                                & " --,MG.GOODS_CD_CUST                 " & vbNewLine _
                                                & " --,MG.GOODS_NM_1                    " & vbNewLine _
                                                & " --,MG.GOODS_NM_2                    " & vbNewLine _
                                                & " ,NB.AD_1                          " & vbNewLine _
                                                & " ,NB.AD_2                          " & vbNewLine _
                                                & " ,NB.AD_3                          " & vbNewLine _
                                                & "--(2012.08.06) フリーダイヤル出力対応 --- START --- " & vbNewLine _
                                                & "            , KB2.KBN_NM1                           " & vbNewLine _
                                                & "--(2012.08.06) フリーダイヤル出力対応 ---  END  --- " & vbNewLine _
                                                & " ,MSZ.SHIWAKE_CD        --ADD 2016/08/29            " & vbNewLine _
                                                & " ,FL.AUTO_DENP_NO      --ADD 2016/08/29             " & vbNewLine _
                                                & " ,FL.UNSO_CD           --ADD 2017/08/31             " & vbNewLine _
                                                & " ,FL.UNSO_BR_CD        --ADD 2017/08/31             " & vbNewLine _
                                                & " ,FL.NRS_BR_CD         --ADD 2017/08/31             " & vbNewLine







#End Region

#Region "纏め荷札"

    '@(2012.06.05) 纏め荷札対応 --- START ---

    ''' <summary>
    ''' 纏め荷札条件データ抽出SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MATOME As String = " SELECT                                         " & vbNewLine _
                                              & "        OUTL.NRS_BR_CD       AS NRS_BR_CD       " & vbNewLine _
                                              & "      , OUTL.CUST_CD_L       AS CUST_CD_L       " & vbNewLine _
                                              & "      , OUTL.CUST_CD_M       AS CUST_CD_M       " & vbNewLine _
                                              & "      , OUTL.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE " & vbNewLine _
                                              & "      , OUTL.ARR_PLAN_DATE   AS ARR_PLAN_DATE   " & vbNewLine _
                                              & "      , UNSOL.UNSO_CD        AS UNSO_CD         " & vbNewLine _
                                              & "      , UNSOL.UNSO_BR_CD     AS UNSO_BR_CD      " & vbNewLine _
                                              & "      , OUTL.DEST_CD         AS DEST_CD         " & vbNewLine

    '要望番号1961 20130322 まとめ荷札対応(BPC対応) 修正START
    Private Const SQL_SELECT_MATOME_CUST_DEST As String = " SELECT                           " & vbNewLine _
                                          & "        OUTL.NRS_BR_CD       AS NRS_BR_CD       " & vbNewLine _
                                          & "      , OUTL.CUST_CD_L       AS CUST_CD_L       " & vbNewLine _
                                          & "      , OUTL.CUST_CD_M       AS CUST_CD_M       " & vbNewLine _
                                          & "      , OUTL.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE " & vbNewLine _
                                          & "      , OUTL.ARR_PLAN_DATE   AS ARR_PLAN_DATE   " & vbNewLine _
                                          & "      , UNSOL.UNSO_CD        AS UNSO_CD         " & vbNewLine _
                                          & "      , UNSOL.UNSO_BR_CD     AS UNSO_BR_CD      " & vbNewLine _
                                          & "      , MDEST.CUST_DEST_CD   AS DEST_CD         " & vbNewLine
    '要望番号1961 20130322 まとめ荷札対応(BPC対応) 修正END


    ''' <summary>
    ''' 纏め荷札条件データ抽出FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_MATOME As String = "  FROM $LM_TRN$..C_OUTKA_L OUTL                       " & vbNewLine _
                                            & "       LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL             " & vbNewLine _
                                            & "              ON OUTL.NRS_BR_CD  = UNSOL.NRS_BR_CD    " & vbNewLine _
                                            & "             AND OUTL.OUTKA_NO_L = UNSOL.INOUTKA_NO_L " & vbNewLine _
                                            & " WHERE OUTL.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                            & "   AND OUTL.OUTKA_NO_L  = @OUTKA_NO_L                 " & vbNewLine

    '要望番号1961 20130322 まとめ荷札対応(BPC対応) 修正START
    Private Const SQL_FROM_MATOME_CUST_DEST As String = "  FROM $LM_TRN$..C_OUTKA_L OUTL                       " & vbNewLine _
                                        & "       LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL             " & vbNewLine _
                                        & "              ON OUTL.NRS_BR_CD  = UNSOL.NRS_BR_CD    " & vbNewLine _
                                        & "             AND OUTL.OUTKA_NO_L = UNSOL.INOUTKA_NO_L " & vbNewLine _
                                        & "       LEFT JOIN $LM_MST$..M_DEST MDEST               " & vbNewLine _
                                        & "              ON OUTL.NRS_BR_CD  = MDEST.NRS_BR_CD    " & vbNewLine _
                                        & "             AND OUTL.CUST_CD_L = MDEST.CUST_CD_L     " & vbNewLine _
                                        & "             AND OUTL.DEST_CD = MDEST.DEST_CD         " & vbNewLine _
                                        & " WHERE OUTL.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                        & "   AND OUTL.OUTKA_NO_L  = @OUTKA_NO_L                 " & vbNewLine
    '要望番号1961 20130322 まとめ荷札対応(BPC対応) 修正END


    ''' <summary>
    ''' 纏め荷札データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks>@(2012.06.05)纏め荷札対応</remarks>
    Private Const SQL_SELECT_DATE_MATOME As String = " SELECT                                         " & vbNewLine _
                                               & "            MAIN.RPT_ID            AS RPT_ID        " & vbNewLine _
                                               & "          , MAIN.NRS_BR_CD         AS NRS_BR_CD     " & vbNewLine _
                                               & "          , MAIN.DEST_NM           AS DEST_NM       " & vbNewLine _
                                               & "          , SUM(MAIN.OUTKA_PKG_NB) AS OUTKA_PKG_NB  " & vbNewLine _
                                               & "          , MAIN.AD_1              AS AD_1          " & vbNewLine _
                                               & "          , MAIN.AD_2              AS AD_2          " & vbNewLine _
                                               & "          , MAIN.AD_3              AS AD_3          " & vbNewLine _
                                               & "          , MAIN.CUST_NM_L         AS CUST_NM_L     " & vbNewLine _
                                               & "          , MAIN.UNSOCO_NM         AS UNSOCO_NM     " & vbNewLine _
                                               & "          , MAIN.ARR_PLAN_DATE     AS ARR_PLAN_DATE " & vbNewLine _
                                               & "          , MAIN.ARR_PLAN_TIME     AS ARR_PLAN_TIME " & vbNewLine _
                                               & "          , MAIN.NRS_BR_NM         AS NRS_BR_NM     " & vbNewLine _
                                               & "          --(2012.06.18)要望番号1167 -- START --    " & vbNewLine _
                                               & "          -- MAIN.OUTKA_NO_L       AS OUTKA_NO_L    " & vbNewLine _
                                               & "          , MIN(MAIN.OUTKA_NO_L)   AS OUTKA_NO_L    " & vbNewLine _
                                               & "          --(2012.06.18)要望番号1167 --  END  --    " & vbNewLine _
                                               & "          , MAIN.TEL               AS TEL           " & vbNewLine _
                                               & "          , MAIN.FAX               AS FAX           " & vbNewLine _
                                               & "          , MAIN.TOP_FLAG          AS TOP_FLAG      " & vbNewLine _
                                               & "          , MAIN.CUST_NM_S         AS CUST_NM_S     " & vbNewLine _
                                               & "          , MAIN.PC_KB             AS PC_KB         " & vbNewLine _
                                               & "          , MAIN.CUST_CD_L         AS CUST_CD_L     " & vbNewLine _
                                               & "          , MAIN.SHIP_NM_L         AS SHIP_NM_L     " & vbNewLine _
                                               & "          , MAIN.ATSUKAISYA_NM     AS ATSUKAISYA_NM " & vbNewLine _
                                               & "          , MAIN.GOODS_CD_CUST     AS GOODS_CD_CUST " & vbNewLine _
                                               & "          , MAIN.LT_DATE           AS LT_DATE       " & vbNewLine _
                                               & "          , MAIN.LOT_NO            AS LOT_NO        " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_1        AS GOODS_NM_1    " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_2        AS GOODS_NM_2    " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_3        AS GOODS_NM_3    " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_1       AS NRS_BR_AD_1   " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_2       AS NRS_BR_AD_2   " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_3       AS NRS_BR_AD_3   " & vbNewLine _
                                               & "          , MAIN.GOODS_SYUBETU     AS GOODS_SYUBETU " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO         AS SET_NAIYO     " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO_2       AS SET_NAIYO_2   " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO_3       AS SET_NAIYO_3   " & vbNewLine _
                                               & "          , MAIN.DEST_TEL          AS DEST_TEL      " & vbNewLine

    '要望番号1961 20130322 まとめ荷札対応(BPC対応) 追加START
    '2018/12/14 LMS_大阪トール_まとめ送状・荷札導入対応 Annen add start
    Private Const SQL_SELECT_DATE_MATOME_TALL As String = " SELECT                                         " & vbNewLine _
                                               & "            MAIN.RPT_ID            AS RPT_ID        " & vbNewLine _
                                               & "          , MAIN.NRS_BR_CD         AS NRS_BR_CD     " & vbNewLine _
                                               & "          , MAIN.DEST_NM           AS DEST_NM       " & vbNewLine _
                                               & "          , SUM(MAIN.OUTKA_PKG_NB) AS OUTKA_PKG_NB  " & vbNewLine _
                                               & "          , MAIN.AD_1              AS AD_1          " & vbNewLine _
                                               & "          , MAIN.AD_2              AS AD_2          " & vbNewLine _
                                               & "          , MAIN.AD_3              AS AD_3          " & vbNewLine _
                                               & "          , MAIN.CUST_NM_L         AS CUST_NM_L     " & vbNewLine _
                                               & "          , MAIN.UNSOCO_NM         AS UNSOCO_NM     " & vbNewLine _
                                               & "          , MAIN.ARR_PLAN_DATE     AS ARR_PLAN_DATE " & vbNewLine _
                                               & "          , MAIN.ARR_PLAN_TIME     AS ARR_PLAN_TIME " & vbNewLine _
                                               & "          , MAIN.NRS_BR_NM         AS NRS_BR_NM     " & vbNewLine _
                                               & "          -- MAIN.OUTKA_NO_L       AS OUTKA_NO_L    " & vbNewLine _
                                               & "          , MIN(MAIN.OUTKA_NO_L)   AS OUTKA_NO_L    " & vbNewLine _
                                               & "          , MAIN.TEL               AS TEL           " & vbNewLine _
                                               & "          , MAIN.FAX               AS FAX           " & vbNewLine _
                                               & "          , MAIN.TOP_FLAG          AS TOP_FLAG      " & vbNewLine _
                                               & "          , MAIN.CUST_NM_S         AS CUST_NM_S     " & vbNewLine _
                                               & "          , MAIN.PC_KB             AS PC_KB         " & vbNewLine _
                                               & "          , MAIN.CUST_CD_L         AS CUST_CD_L     " & vbNewLine _
                                               & "          , MAIN.SHIP_NM_L         AS SHIP_NM_L     " & vbNewLine _
                                               & "          , MAIN.ATSUKAISYA_NM     AS ATSUKAISYA_NM " & vbNewLine _
                                               & "          , MAIN.GOODS_CD_CUST     AS GOODS_CD_CUST " & vbNewLine _
                                               & "          , MAIN.LT_DATE           AS LT_DATE       " & vbNewLine _
                                               & "          , MAIN.LOT_NO            AS LOT_NO        " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_1        AS GOODS_NM_1    " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_2        AS GOODS_NM_2    " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_3        AS GOODS_NM_3    " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_1       AS NRS_BR_AD_1   " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_2       AS NRS_BR_AD_2   " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_3       AS NRS_BR_AD_3   " & vbNewLine _
                                               & "          , MAIN.GOODS_SYUBETU     AS GOODS_SYUBETU " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO         AS SET_NAIYO     " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO_2       AS SET_NAIYO_2   " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO_3       AS SET_NAIYO_3   " & vbNewLine _
                                               & "          , MAIN.DEST_TEL          AS DEST_TEL      " & vbNewLine _
                                               & "          ,ISNULL(MAIN.CHAKU_CD,'')   AS CHAKU_CD   " & vbNewLine _
                                               & "          ,ISNULL(MAIN.CHAKU_NM,'')   AS CHAKU_NM   " & vbNewLine

    '2018/12/14 LMS_大阪トール_まとめ送状・荷札導入対応 Annen add end

    ''' <summary>
    ''' 纏め荷札データBP抽出用 SELECT句
    ''' </summary>
    ''' <remarks>@(2012.06.05)纏め荷札対応</remarks>
    Private Const SQL_SELECT_DATE_MATOME_CUST As String = " SELECT                                    " & vbNewLine _
                                               & "            MAIN.RPT_ID            AS RPT_ID        " & vbNewLine _
                                               & "          , MAIN.NRS_BR_CD         AS NRS_BR_CD     " & vbNewLine _
                                               & "          , MAX(MAIN.DEST_NM)      AS DEST_NM       " & vbNewLine _
                                               & "          , SUM(MAIN.OUTKA_PKG_NB) AS OUTKA_PKG_NB  " & vbNewLine _
                                               & "          , MAX(MAIN.AD_1)         AS AD_1          " & vbNewLine _
                                               & "          , MAX(MAIN.AD_2)         AS AD_2          " & vbNewLine _
                                               & "          , MAX(MAIN.AD_3)         AS AD_3          " & vbNewLine _
                                               & "          , MAIN.CUST_NM_L         AS CUST_NM_L     " & vbNewLine _
                                               & "          , MAIN.UNSOCO_NM         AS UNSOCO_NM     " & vbNewLine _
                                               & "          , MAIN.ARR_PLAN_DATE     AS ARR_PLAN_DATE " & vbNewLine _
                                               & "          , MAIN.ARR_PLAN_TIME     AS ARR_PLAN_TIME " & vbNewLine _
                                               & "          , MAIN.NRS_BR_NM         AS NRS_BR_NM     " & vbNewLine _
                                               & "          --(2012.06.18)要望番号1167 -- START --    " & vbNewLine _
                                               & "          -- MAIN.OUTKA_NO_L       AS OUTKA_NO_L    " & vbNewLine _
                                               & "          , MIN(MAIN.OUTKA_NO_L)   AS OUTKA_NO_L    " & vbNewLine _
                                               & "          --(2012.06.18)要望番号1167 --  END  --    " & vbNewLine _
                                               & "          , MAIN.TEL               AS TEL           " & vbNewLine _
                                               & "          , MAIN.FAX               AS FAX           " & vbNewLine _
                                               & "          , MAIN.TOP_FLAG          AS TOP_FLAG      " & vbNewLine _
                                               & "          , MAIN.CUST_NM_S         AS CUST_NM_S     " & vbNewLine _
                                               & "          , MAIN.PC_KB             AS PC_KB         " & vbNewLine _
                                               & "          , MAIN.CUST_CD_L         AS CUST_CD_L     " & vbNewLine _
                                               & "          , MAIN.SHIP_NM_L         AS SHIP_NM_L     " & vbNewLine _
                                               & "          , MAIN.ATSUKAISYA_NM     AS ATSUKAISYA_NM " & vbNewLine _
                                               & "          , MAIN.GOODS_CD_CUST     AS GOODS_CD_CUST " & vbNewLine _
                                               & "          , MAIN.LT_DATE           AS LT_DATE       " & vbNewLine _
                                               & "          , MAIN.LOT_NO            AS LOT_NO        " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_1        AS GOODS_NM_1    " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_2        AS GOODS_NM_2    " & vbNewLine _
                                               & "          , MAIN.GOODS_NM_3        AS GOODS_NM_3    " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_1       AS NRS_BR_AD_1   " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_2       AS NRS_BR_AD_2   " & vbNewLine _
                                               & "          , MAIN.NRS_BR_AD_3       AS NRS_BR_AD_3   " & vbNewLine _
                                               & "          , MAIN.GOODS_SYUBETU     AS GOODS_SYUBETU " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO         AS SET_NAIYO     " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO_2       AS SET_NAIYO_2   " & vbNewLine _
                                               & "          , MAIN.SET_NAIYO_3       AS SET_NAIYO_3   " & vbNewLine _
                                               & "          , MAX(MAIN.DEST_TEL)     AS DEST_TEL      " & vbNewLine


    ''' <summary>
    ''' 纏め荷札データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>@(2012.06.05)纏め荷札対応</remarks>
    Private Const SQL_FROM_DATE_MATOME_1 As String = " FROM                                                                          " & vbNewLine _
                                                 & "      (                                                                        " & vbNewLine _
                                                 & "       SELECT CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                 " & vbNewLine _
                                                 & "                   WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                                 & "              ELSE MR3.RPT_ID                                                  " & vbNewLine _
                                                 & "              END                     AS RPT_ID                                " & vbNewLine _
                                                 & "            , OUTL.NRS_BR_CD          AS NRS_BR_CD                             " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                  " & vbNewLine _
                                                 & "                   WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                  " & vbNewLine _
                                                 & "              ELSE DST.DEST_NM                                                 " & vbNewLine _
                                                 & "              END                     AS DEST_NM                               " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.OUTKA_PKG_NB <> 0 THEN OUTL.OUTKA_PKG_NB          " & vbNewLine _
                                                 & "              ELSE SUM(OUTM.OUTKA_M_PKG_NB)                                    " & vbNewLine _
                                                 & "              END                     AS OUTKA_PKG_NB                          " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                " & vbNewLine _
                                                 & "                   WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                " & vbNewLine _
                                                 & "              ELSE DST.AD_1                                                    " & vbNewLine _
                                                 & "              END                     AS AD_1                                  " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                " & vbNewLine _
                                                 & "              WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                     " & vbNewLine _
                                                 & "              ELSE DST.AD_2                                                    " & vbNewLine _
                                                 & "              END                     AS AD_2                                  " & vbNewLine _
                                                 & "            , OUTL.DEST_AD_3          AS AD_3                                  " & vbNewLine _
                                                 & "            , CST.CUST_NM_L           AS CUST_NM_L                             " & vbNewLine _
                                                 & "            , UC.UNSOCO_NM            AS UNSOCO_NM                             " & vbNewLine _
                                                 & "            , OUTL.ARR_PLAN_DATE      AS ARR_PLAN_DATE                         " & vbNewLine _
                                                 & "            , KB1.KBN_NM1             AS ARR_PLAN_TIME                         " & vbNewLine _
                                                 & "            , CASE WHEN MCD.SET_NAIYO <> NULL                                  " & vbNewLine _
                                                 & "                     OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO                 " & vbNewLine _
                                                 & "              ELSE NB.NRS_BR_NM                                                " & vbNewLine _
                                                 & "              END                     AS NRS_BR_NM                             " & vbNewLine _
                                                 & "            --(2012.06.18)要望番号1167 -- START --                             " & vbNewLine _
                                                 & "            --,''                     AS OUTKA_NO_L                            " & vbNewLine _
                                                 & "            , OUTL.OUTKA_NO_L         AS OUTKA_NO_L                            " & vbNewLine _
                                                 & "            --(2012.06.18)要望番号1167 --  END  --                             " & vbNewLine _
                                                 & "            --(2012.08.06) フリーダイヤル出力対応 --- START ---                " & vbNewLine _
                                                 & "            --, CASE WHEN SO.WH_KB = '01' THEN SO.TEL                          " & vbNewLine _
                                                 & "            --  ELSE NB.TEL END      AS TEL                                    " & vbNewLine _
                                                 & "              , CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1        " & vbNewLine _
                                                 & "                     WHEN SO.WH_KB  = '01' THEN SO.TEL                         " & vbNewLine _
                                                 & "                ELSE NB.TEL                                                    " & vbNewLine _
                                                 & "                END                  AS TEL                                    " & vbNewLine _
                                                 & "            --(2012.08.06) フリーダイヤル出力対応 ---  END  ---                " & vbNewLine _
                                                 & "            , CASE WHEN SO.WH_KB = '01' THEN SO.FAX                            " & vbNewLine _
                                                 & "              ELSE NB.FAX END         AS FAX                                   " & vbNewLine _
                                                 & "            , ''                      AS TOP_FLAG                              " & vbNewLine _
                                                 & "            , ''                      AS CUST_NM_S                             " & vbNewLine _
                                                 & "            , OUTL.PC_KB              AS PC_KB                                 " & vbNewLine _
                                                 & "            , OUTL.CUST_CD_L          AS CUST_CD_L                             " & vbNewLine _
                                                 & "            , ''                      AS SHIP_NM_L                             " & vbNewLine _
                                                 & "            , CASE WHEN SHIPDEST.DEST_NM <> ''                                 " & vbNewLine _
                                                 & "                        AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM " & vbNewLine _
                                                 & "                   WHEN CST.DENPYO_NM    <> ''                                 " & vbNewLine _
                                                 & "                        AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM     " & vbNewLine _
                                                 & "              ELSE CST.CUST_NM_L                                               " & vbNewLine _
                                                 & "              END                     AS ATSUKAISYA_NM                         " & vbNewLine _
                                                 & "            , ''                      AS LT_DATE                               " & vbNewLine _
                                                 & "            , ''                      AS GOODS_CD_CUST                         " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_1                            " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_2                            " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_3                            " & vbNewLine _
                                                 & "            , ''                      AS LOT_NO                                " & vbNewLine _
                                                 & "            , NB.AD_1                 AS NRS_BR_AD_1                           " & vbNewLine _
                                                 & "            , NB.AD_2                 AS NRS_BR_AD_2                           " & vbNewLine _
                                                 & "            , NB.AD_3                 AS NRS_BR_AD_3                           " & vbNewLine _
                                                 & "            , ''                      AS GOODS_SYUBETU                         " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO         AS SET_NAIYO                             " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_2       AS SET_NAIYO_2                           " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_3       AS SET_NAIYO_3                           " & vbNewLine _
                                                 & "            , DST.TEL                 AS DEST_TEL                              " & vbNewLine _
                                                 & "         FROM                                                                  " & vbNewLine _
                                                 & "              $LM_TRN$..C_OUTKA_L OUTL                                         " & vbNewLine _
                                                 & "              --出荷EDI(大)                                                    " & vbNewLine _
                                                 & "              --LEFT JOIN                                                      " & vbNewLine _
                                                 & "              --          (                                                    " & vbNewLine _
                                                 & "              --            SELECT MIN(DEST_NM)   AS DEST_NM                   " & vbNewLine _
                                                 & "              --                 , MIN(DEST_AD_1) AS DEST_AD_1                 " & vbNewLine _
                                                 & "              --                 , MIN(DEST_AD_2) AS DEST_AD_2                 " & vbNewLine _
                                                 & "              --                 , NRS_BR_CD      AS NRS_BR_CD                 " & vbNewLine _
                                                 & "              --                 , OUTKA_CTL_NO   AS OUTKA_CTL_NO              " & vbNewLine _
                                                 & "              --                 , SHIP_NM_L      AS SHIP_NM_L                 " & vbNewLine _
                                                 & "              --                 , SYS_DEL_FLG    AS SYS_DEL_FLG               " & vbNewLine _
                                                 & "              --              FROM $LM_TRN$..H_OUTKAEDI_L                      " & vbNewLine _
                                                 & "              --             GROUP BY                                          " & vbNewLine _
                                                 & "              --                   NRS_BR_CD                                   " & vbNewLine _
                                                 & "              --                 , OUTKA_CTL_NO                                " & vbNewLine _
                                                 & "              --                 , SHIP_NM_L                                   " & vbNewLine _
                                                 & "              --                 , SYS_DEL_FLG                                 " & vbNewLine _
                                                 & "              --          ) AS EDIL                                            " & vbNewLine _
                                                 & "              --       ON EDIL.SYS_DEL_FLG  = '0'                              " & vbNewLine _
                                                 & "              --      AND EDIL.NRS_BR_CD    = OUTL.NRS_BR_CD                   " & vbNewLine _
                                                 & "              --      AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                  " & vbNewLine _
                                                 & "              --下記の内容に変更                                                                  " & vbNewLine _
                                                 & "              LEFT JOIN (                                                                         " & vbNewLine _
                                                 & "                         SELECT                                                                   " & vbNewLine _
                                                 & "                                NRS_BR_CD                                                         " & vbNewLine _
                                                 & "                              , EDI_CTL_NO                                                        " & vbNewLine _
                                                 & "                              , OUTKA_CTL_NO                                                      " & vbNewLine _
                                                 & "                          FROM (                                                                  " & vbNewLine _
                                                 & "                                 SELECT                                                           " & vbNewLine _
                                                 & "                                        EDIOUTL.NRS_BR_CD                                         " & vbNewLine _
                                                 & "                                      , EDIOUTL.EDI_CTL_NO                                        " & vbNewLine _
                                                 & "                                      , EDIOUTL.OUTKA_CTL_NO                                      " & vbNewLine _
                                                 & "                                      , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                " & vbNewLine _
                                                 & "                                        ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD    " & vbNewLine _
                                                 & "                                                                           , EDIOUTL.OUTKA_CTL_NO " & vbNewLine _
                                                 & "                                                                    ORDER BY EDIOUTL.NRS_BR_CD    " & vbNewLine _
                                                 & "                                                                           , EDIOUTL.EDI_CTL_NO   " & vbNewLine _
                                                 & "                                                               )                                  " & vbNewLine _
                                                 & "                                        END AS IDX                                                " & vbNewLine _
                                                 & "                                  FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                             " & vbNewLine _
                                                 & "                                       LEFT JOIN $LM_TRN$..C_OUTKA_L C_OUTL                       " & vbNewLine _
                                                 & "                                              ON C_OUTL.NRS_BR_CD   = EDIOUTL.NRS_BR_CD           " & vbNewLine _
                                                 & "                                             AND C_OUTL.OUTKA_NO_L  = EDIOUTL.OUTKA_CTL_NO        " & vbNewLine _
                                                 & "                                             AND C_OUTL.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                                 & "                                 WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                 " & vbNewLine _
                                                 & "                                   AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                          " & vbNewLine _
                                                 & "                               ) EBASE                                                            " & vbNewLine _
                                                 & "                         WHERE EBASE.IDX = 1                                                      " & vbNewLine _
                                                 & "                         ) TOPEDI                                                                 " & vbNewLine _
                                                 & "                     ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                      " & vbNewLine _
                                                 & "                    AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                               " & vbNewLine _
                                                 & "                     ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                        " & vbNewLine _
                                                 & "                    AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                       " & vbNewLine _
                                                 & "--(2012.09.06)要望番号1412対応  ---  END  ---                                                     " & vbNewLine _
                                                 & "              --出荷(中)                                                       " & vbNewLine _
                                                 & "              LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                               " & vbNewLine _
                                                 & "                     ON OUTM.NRS_BR_CD    = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND OUTM.OUTKA_NO_L   = OUTL.OUTKA_NO_L                    " & vbNewLine _
                                                 & "                    AND OUTM.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                                 & "              --商品M                                                          " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_GOODS MG                                   " & vbNewLine _
                                                 & "                     ON MG.NRS_BR_CD    = OUTM.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                    " & vbNewLine _
                                                 & "              --出荷Lでの荷主帳票パターン取得                                  " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                              " & vbNewLine _
                                                 & "                     ON OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                                 & "                    AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                                 & "                    AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                                 & "                    AND '00' = MCR1.CUST_CD_S                                  " & vbNewLine _
                                                 & "                    AND MCR1.PTN_ID      = '10'                                " & vbNewLine _
                                                 & "                    AND MCR1.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                                 & "              --帳票パターン取得                                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR1                                    " & vbNewLine _
                                                 & "                     ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MR1.PTN_ID    = MCR1.PTN_ID                            " & vbNewLine _
                                                 & "                    AND MR1.PTN_CD    = MCR1.PTN_CD                            " & vbNewLine _
                                                 & "                    AND MR1.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --商品Mの荷主での荷主帳票パターン取得                            " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                              " & vbNewLine _
                                                 & "                     ON MG.NRS_BR_CD = MCR2.NRS_BR_CD                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_L = MCR2.CUST_CD_L                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_M = MCR2.CUST_CD_M                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_S = MCR2.CUST_CD_S                          " & vbNewLine _
                                                 & "                    AND MCR2.PTN_ID  = '10'                                    " & vbNewLine _
                                                 & "                    AND MCR2.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                                 & "              --帳票パターン取得                                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR2                                    " & vbNewLine _
                                                 & "                     ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MR2.PTN_ID    = MCR2.PTN_ID                            " & vbNewLine _
                                                 & "                    AND MR2.PTN_CD    = MCR2.PTN_CD                            " & vbNewLine _
                                                 & "                    AND MR2.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --存在しない場合の帳票パターン取得                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR3                                    " & vbNewLine _
                                                 & "                     ON MR3.NRS_BR_CD     = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND MR3.PTN_ID        = '10'                               " & vbNewLine _
                                                 & "                    AND MR3.STANDARD_FLAG = '01'                               " & vbNewLine _
                                                 & "                    AND MR3.SYS_DEL_FLG   = '0'                                " & vbNewLine _
                                                 & "              --荷主マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST CST                                   " & vbNewLine _
                                                 & "                     ON CST.NRS_BR_CD  = MG.NRS_BR_CD                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_L  = MG.CUST_CD_L                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_M  = MG.CUST_CD_M                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_S  = MG.CUST_CD_S                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_SS = MG.CUST_CD_SS                         " & vbNewLine _
                                                 & "              --荷主明細マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                           " & vbNewLine _
                                                 & "                     ON MCD.NRS_BR_CD   = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND MCD.CUST_CD     = OUTL.CUST_CD_L                       " & vbNewLine _
                                                 & "                    AND MCD.NRS_BR_CD   = @NRS_BR_CD                           " & vbNewLine _
                                                 & "                    AND MCD.SUB_KB      = '11'                                 " & vbNewLine _
                                                 & "                    AND MCD.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --届先マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_DEST DST                                   " & vbNewLine _
                                                 & "                     ON DST.NRS_BR_CD = OUTL.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND DST.CUST_CD_L = OUTL.CUST_CD_L                         " & vbNewLine _
                                                 & "                    AND DST.DEST_CD   = OUTL.DEST_CD                           " & vbNewLine _
                                                 & "              --届先マスタ（売上先）                                           " & vbNewLine _
                                                 & "                    LEFT JOIN $LM_MST$..M_DEST SHIPDEST                        " & vbNewLine _
                                                 & "                     ON SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                    " & vbNewLine _
                                                 & "                    AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                    " & vbNewLine _
                                                 & "                    AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                    " & vbNewLine _
                                                 & "              --F運送L                                                         " & vbNewLine _
                                                 & "              LEFT JOIN $LM_TRN$..F_UNSO_L FL                                  " & vbNewLine _
                                                 & "                     ON FL.MOTO_DATA_KB = '20'                                 " & vbNewLine _
                                                 & "                    AND FL.NRS_BR_CD    = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND FL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                      " & vbNewLine _
                                                 & "                    AND FL.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                                 & "              --運送会社マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_UNSOCO UC                                  " & vbNewLine _
                                                 & "                     ON UC.NRS_BR_CD    = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND UC.UNSOCO_CD    = FL.UNSO_CD                           " & vbNewLine _
                                                 & "                    AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                        " & vbNewLine _
                                                 & "              --営業所マスタ                                                   " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_NRS_BR NB                                  " & vbNewLine _
                                                 & "                     ON NB.NRS_BR_CD = OUTL.NRS_BR_CD                          " & vbNewLine _
                                                 & "              --倉庫マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_SOKO SO                                    " & vbNewLine _
                                                 & "                     ON SO.WH_CD = OUTL.WH_CD                                  " & vbNewLine _
                                                 & "              --区分マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..Z_KBN KB1                                    " & vbNewLine _
                                                 & "                     ON KB1.KBN_GROUP_CD = 'N010'                              " & vbNewLine _
                                                 & "                    AND KB1.KBN_CD = OUTL.ARR_PLAN_TIME                        " & vbNewLine _
                                                 & "              --(2012.08.06) フリーダイヤル出力対応 --- START ---              " & vbNewLine _
                                                 & "              --区分マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..Z_KBN KB2                                    " & vbNewLine _
                                                 & "                ON KB2.KBN_GROUP_CD = 'N022'                                   " & vbNewLine _
                                                 & "               AND KB2.KBN_CD       = '00'                                     " & vbNewLine _
                                                 & "               AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                           " & vbNewLine _
                                                 & "              --(2012.08.06) フリーダイヤル出力対応 ---  END  ---              " & vbNewLine _
                                                 & "              --荷主明細マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                         " & vbNewLine _
                                                 & "                     ON MCD01.NRS_BR_CD   = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND MCD01.CUST_CD     = OUTL.CUST_CD_L                     " & vbNewLine _
                                                 & "                    AND MCD01.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MCD01.SUB_KB      = '29'                               " & vbNewLine _
                                                 & "                    AND MCD01.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                                 & "        WHERE OUTL.SYS_DEL_FLG     = '0'                                       " & vbNewLine _
                                                 & "          AND OUTL.NRS_BR_CD       = @NRS_BR_CD                                " & vbNewLine _
                                                 & "          AND OUTL.CUST_CD_L       = @CUST_CD_L                                " & vbNewLine _
                                                 & "          AND OUTL.CUST_CD_M       = @CUST_CD_M                                " & vbNewLine _
                                                 & "          AND OUTL.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE                          " & vbNewLine _
                                                 & "          AND OUTL.ARR_PLAN_DATE   = @ARR_PLAN_DATE                            " & vbNewLine _
                                                 & "          AND FL.UNSO_CD           = @UNSO_CD                                  " & vbNewLine _
                                                 & "          AND FL.UNSO_BR_CD        = @UNSO_BR_CD                               " & vbNewLine


    '2018/01/23 LMS_大阪トール_まとめ送状・荷札導入対応 Annen add start
    Private Const SQL_FROM_DATE_MATOME_1_TALL As String = " FROM                                                                          " & vbNewLine _
                                                 & "      (                                                                        " & vbNewLine _
                                                 & "       SELECT CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                 " & vbNewLine _
                                                 & "                   WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                                 & "              ELSE MR3.RPT_ID                                                  " & vbNewLine _
                                                 & "              END                     AS RPT_ID                                " & vbNewLine _
                                                 & "            , OUTL.NRS_BR_CD          AS NRS_BR_CD                             " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                  " & vbNewLine _
                                                 & "                   WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                  " & vbNewLine _
                                                 & "              ELSE DST.DEST_NM                                                 " & vbNewLine _
                                                 & "              END                     AS DEST_NM                               " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.OUTKA_PKG_NB <> 0 THEN OUTL.OUTKA_PKG_NB          " & vbNewLine _
                                                 & "              ELSE SUM(OUTM.OUTKA_M_PKG_NB)                                    " & vbNewLine _
                                                 & "              END                     AS OUTKA_PKG_NB                          " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                " & vbNewLine _
                                                 & "                   WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                " & vbNewLine _
                                                 & "              ELSE DST.AD_1                                                    " & vbNewLine _
                                                 & "              END                     AS AD_1                                  " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                " & vbNewLine _
                                                 & "              WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                     " & vbNewLine _
                                                 & "              ELSE DST.AD_2                                                    " & vbNewLine _
                                                 & "              END                     AS AD_2                                  " & vbNewLine _
                                                 & "            , OUTL.DEST_AD_3          AS AD_3                                  " & vbNewLine _
                                                 & "            , CST.CUST_NM_L           AS CUST_NM_L                             " & vbNewLine _
                                                 & "            , UC.UNSOCO_NM            AS UNSOCO_NM                             " & vbNewLine _
                                                 & "            , OUTL.ARR_PLAN_DATE      AS ARR_PLAN_DATE                         " & vbNewLine _
                                                 & "            , KB1.KBN_NM1             AS ARR_PLAN_TIME                         " & vbNewLine _
                                                 & "            , CASE WHEN MCD.SET_NAIYO <> NULL                                  " & vbNewLine _
                                                 & "                     OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO                 " & vbNewLine _
                                                 & "              ELSE NB.NRS_BR_NM                                                " & vbNewLine _
                                                 & "              END                     AS NRS_BR_NM                             " & vbNewLine _
                                                 & "            --(2012.06.18)要望番号1167 -- START --                             " & vbNewLine _
                                                 & "            --,''                     AS OUTKA_NO_L                            " & vbNewLine _
                                                 & "            , OUTL.OUTKA_NO_L         AS OUTKA_NO_L                            " & vbNewLine _
                                                 & "            --(2012.06.18)要望番号1167 --  END  --                             " & vbNewLine _
                                                 & "            --(2012.08.06) フリーダイヤル出力対応 --- START ---                " & vbNewLine _
                                                 & "            --, CASE WHEN SO.WH_KB = '01' THEN SO.TEL                          " & vbNewLine _
                                                 & "            --  ELSE NB.TEL END      AS TEL                                    " & vbNewLine _
                                                 & "              , CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1        " & vbNewLine _
                                                 & "                     WHEN SO.WH_KB  = '01' THEN SO.TEL                         " & vbNewLine _
                                                 & "                ELSE NB.TEL                                                    " & vbNewLine _
                                                 & "                END                  AS TEL                                    " & vbNewLine _
                                                 & "            --(2012.08.06) フリーダイヤル出力対応 ---  END  ---                " & vbNewLine _
                                                 & "            , CASE WHEN SO.WH_KB = '01' THEN SO.FAX                            " & vbNewLine _
                                                 & "              ELSE NB.FAX END         AS FAX                                   " & vbNewLine _
                                                 & "            , ''                      AS TOP_FLAG                              " & vbNewLine _
                                                 & "            , ''                      AS CUST_NM_S                             " & vbNewLine _
                                                 & "            , OUTL.PC_KB              AS PC_KB                                 " & vbNewLine _
                                                 & "            , OUTL.CUST_CD_L          AS CUST_CD_L                             " & vbNewLine _
                                                 & "            , ''                      AS SHIP_NM_L                             " & vbNewLine _
                                                 & "            , CASE WHEN SHIPDEST.DEST_NM <> ''                                 " & vbNewLine _
                                                 & "                        AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM " & vbNewLine _
                                                 & "                   WHEN CST.DENPYO_NM    <> ''                                 " & vbNewLine _
                                                 & "                        AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM     " & vbNewLine _
                                                 & "              ELSE CST.CUST_NM_L                                               " & vbNewLine _
                                                 & "              END                     AS ATSUKAISYA_NM                         " & vbNewLine _
                                                 & "            , ''                      AS LT_DATE                               " & vbNewLine _
                                                 & "            , ''                      AS GOODS_CD_CUST                         " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_1                            " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_2                            " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_3                            " & vbNewLine _
                                                 & "            , ''                      AS LOT_NO                                " & vbNewLine _
                                                 & "            , NB.AD_1                 AS NRS_BR_AD_1                           " & vbNewLine _
                                                 & "            , NB.AD_2                 AS NRS_BR_AD_2                           " & vbNewLine _
                                                 & "            , NB.AD_3                 AS NRS_BR_AD_3                           " & vbNewLine _
                                                 & "            , ''                      AS GOODS_SYUBETU                         " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO         AS SET_NAIYO                             " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_2       AS SET_NAIYO_2                           " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_3       AS SET_NAIYO_3                           " & vbNewLine _
                                                 & "            , DST.TEL                 AS DEST_TEL                              " & vbNewLine _
                                                 & "            , MTOLL.CHAKU_CD		  AS CHAKU_CD                              " & vbNewLine _
                                                 & "            , MTOLL.CHAKU_NM          AS CHAKU_NM                              " & vbNewLine _
                                                 & "         FROM                                                                  " & vbNewLine _
                                                 & "              $LM_TRN$..C_OUTKA_L OUTL                                         " & vbNewLine _
                                                 & "              --出荷EDI(大)                                                    " & vbNewLine _
                                                 & "              --LEFT JOIN                                                      " & vbNewLine _
                                                 & "              --          (                                                    " & vbNewLine _
                                                 & "              --            SELECT MIN(DEST_NM)   AS DEST_NM                   " & vbNewLine _
                                                 & "              --                 , MIN(DEST_AD_1) AS DEST_AD_1                 " & vbNewLine _
                                                 & "              --                 , MIN(DEST_AD_2) AS DEST_AD_2                 " & vbNewLine _
                                                 & "              --                 , NRS_BR_CD      AS NRS_BR_CD                 " & vbNewLine _
                                                 & "              --                 , OUTKA_CTL_NO   AS OUTKA_CTL_NO              " & vbNewLine _
                                                 & "              --                 , SHIP_NM_L      AS SHIP_NM_L                 " & vbNewLine _
                                                 & "              --                 , SYS_DEL_FLG    AS SYS_DEL_FLG               " & vbNewLine _
                                                 & "              --              FROM $LM_TRN$..H_OUTKAEDI_L                      " & vbNewLine _
                                                 & "              --             GROUP BY                                          " & vbNewLine _
                                                 & "              --                   NRS_BR_CD                                   " & vbNewLine _
                                                 & "              --                 , OUTKA_CTL_NO                                " & vbNewLine _
                                                 & "              --                 , SHIP_NM_L                                   " & vbNewLine _
                                                 & "              --                 , SYS_DEL_FLG                                 " & vbNewLine _
                                                 & "              --          ) AS EDIL                                            " & vbNewLine _
                                                 & "              --       ON EDIL.SYS_DEL_FLG  = '0'                              " & vbNewLine _
                                                 & "              --      AND EDIL.NRS_BR_CD    = OUTL.NRS_BR_CD                   " & vbNewLine _
                                                 & "              --      AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                  " & vbNewLine _
                                                 & "              --下記の内容に変更                                                                  " & vbNewLine _
                                                 & "              LEFT JOIN (                                                                         " & vbNewLine _
                                                 & "                         SELECT                                                                   " & vbNewLine _
                                                 & "                                NRS_BR_CD                                                         " & vbNewLine _
                                                 & "                              , EDI_CTL_NO                                                        " & vbNewLine _
                                                 & "                              , OUTKA_CTL_NO                                                      " & vbNewLine _
                                                 & "                          FROM (                                                                  " & vbNewLine _
                                                 & "                                 SELECT                                                           " & vbNewLine _
                                                 & "                                        EDIOUTL.NRS_BR_CD                                         " & vbNewLine _
                                                 & "                                      , EDIOUTL.EDI_CTL_NO                                        " & vbNewLine _
                                                 & "                                      , EDIOUTL.OUTKA_CTL_NO                                      " & vbNewLine _
                                                 & "                                      , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                " & vbNewLine _
                                                 & "                                        ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD    " & vbNewLine _
                                                 & "                                                                           , EDIOUTL.OUTKA_CTL_NO " & vbNewLine _
                                                 & "                                                                    ORDER BY EDIOUTL.NRS_BR_CD    " & vbNewLine _
                                                 & "                                                                           , EDIOUTL.EDI_CTL_NO   " & vbNewLine _
                                                 & "                                                               )                                  " & vbNewLine _
                                                 & "                                        END AS IDX                                                " & vbNewLine _
                                                 & "                                  FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                             " & vbNewLine _
                                                 & "                                       LEFT JOIN $LM_TRN$..C_OUTKA_L C_OUTL                       " & vbNewLine _
                                                 & "                                              ON C_OUTL.NRS_BR_CD   = EDIOUTL.NRS_BR_CD           " & vbNewLine _
                                                 & "                                             AND C_OUTL.OUTKA_NO_L  = EDIOUTL.OUTKA_CTL_NO        " & vbNewLine _
                                                 & "                                             AND C_OUTL.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                                 & "                                 WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                 " & vbNewLine _
                                                 & "                                   AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                          " & vbNewLine _
                                                 & "                               ) EBASE                                                            " & vbNewLine _
                                                 & "                         WHERE EBASE.IDX = 1                                                      " & vbNewLine _
                                                 & "                         ) TOPEDI                                                                 " & vbNewLine _
                                                 & "                     ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                      " & vbNewLine _
                                                 & "                    AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                               " & vbNewLine _
                                                 & "                     ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                        " & vbNewLine _
                                                 & "                    AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                       " & vbNewLine _
                                                 & "--(2012.09.06)要望番号1412対応  ---  END  ---                                                     " & vbNewLine _
                                                 & "              --出荷(中)                                                       " & vbNewLine _
                                                 & "              LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                               " & vbNewLine _
                                                 & "                     ON OUTM.NRS_BR_CD    = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND OUTM.OUTKA_NO_L   = OUTL.OUTKA_NO_L                    " & vbNewLine _
                                                 & "                    AND OUTM.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                                 & "              --商品M                                                          " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_GOODS MG                                   " & vbNewLine _
                                                 & "                     ON MG.NRS_BR_CD    = OUTM.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                    " & vbNewLine _
                                                 & "              --出荷Lでの荷主帳票パターン取得                                  " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                              " & vbNewLine _
                                                 & "                     ON OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                                 & "                    AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                                 & "                    AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                                 & "                    AND '00' = MCR1.CUST_CD_S                                  " & vbNewLine _
                                                 & "                    AND MCR1.PTN_ID      = '10'                                " & vbNewLine _
                                                 & "                    AND MCR1.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                                 & "              --帳票パターン取得                                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR1                                    " & vbNewLine _
                                                 & "                     ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MR1.PTN_ID    = MCR1.PTN_ID                            " & vbNewLine _
                                                 & "                    AND MR1.PTN_CD    = MCR1.PTN_CD                            " & vbNewLine _
                                                 & "                    AND MR1.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --商品Mの荷主での荷主帳票パターン取得                            " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                              " & vbNewLine _
                                                 & "                     ON MG.NRS_BR_CD = MCR2.NRS_BR_CD                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_L = MCR2.CUST_CD_L                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_M = MCR2.CUST_CD_M                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_S = MCR2.CUST_CD_S                          " & vbNewLine _
                                                 & "                    AND MCR2.PTN_ID  = '10'                                    " & vbNewLine _
                                                 & "                    AND MCR2.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                                 & "              --帳票パターン取得                                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR2                                    " & vbNewLine _
                                                 & "                     ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MR2.PTN_ID    = MCR2.PTN_ID                            " & vbNewLine _
                                                 & "                    AND MR2.PTN_CD    = MCR2.PTN_CD                            " & vbNewLine _
                                                 & "                    AND MR2.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --存在しない場合の帳票パターン取得                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR3                                    " & vbNewLine _
                                                 & "                     ON MR3.NRS_BR_CD     = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND MR3.PTN_ID        = '10'                               " & vbNewLine _
                                                 & "                    AND MR3.STANDARD_FLAG = '01'                               " & vbNewLine _
                                                 & "                    AND MR3.SYS_DEL_FLG   = '0'                                " & vbNewLine _
                                                 & "              --荷主マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST CST                                   " & vbNewLine _
                                                 & "                     ON CST.NRS_BR_CD  = MG.NRS_BR_CD                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_L  = MG.CUST_CD_L                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_M  = MG.CUST_CD_M                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_S  = MG.CUST_CD_S                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_SS = MG.CUST_CD_SS                         " & vbNewLine _
                                                 & "              --荷主明細マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                           " & vbNewLine _
                                                 & "                     ON MCD.NRS_BR_CD   = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND MCD.CUST_CD     = OUTL.CUST_CD_L                       " & vbNewLine _
                                                 & "                    AND MCD.NRS_BR_CD   = @NRS_BR_CD                           " & vbNewLine _
                                                 & "                    AND MCD.SUB_KB      = '11'                                 " & vbNewLine _
                                                 & "                    AND MCD.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --届先マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_DEST DST                                   " & vbNewLine _
                                                 & "                     ON DST.NRS_BR_CD = OUTL.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND DST.CUST_CD_L = OUTL.CUST_CD_L                         " & vbNewLine _
                                                 & "                    AND DST.DEST_CD   = OUTL.DEST_CD                           " & vbNewLine _
                                                 & "              --届先マスタ（売上先）                                           " & vbNewLine _
                                                 & "                    LEFT JOIN $LM_MST$..M_DEST SHIPDEST                        " & vbNewLine _
                                                 & "                     ON SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                    " & vbNewLine _
                                                 & "                    AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                    " & vbNewLine _
                                                 & "                    AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                    " & vbNewLine _
                                                 & "              --F運送L                                                         " & vbNewLine _
                                                 & "              LEFT JOIN $LM_TRN$..F_UNSO_L FL                                  " & vbNewLine _
                                                 & "                     ON FL.MOTO_DATA_KB = '20'                                 " & vbNewLine _
                                                 & "                    AND FL.NRS_BR_CD    = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND FL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                      " & vbNewLine _
                                                 & "                    AND FL.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                                 & "              --運送会社マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_UNSOCO UC                                  " & vbNewLine _
                                                 & "                     ON UC.NRS_BR_CD    = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND UC.UNSOCO_CD    = FL.UNSO_CD                           " & vbNewLine _
                                                 & "                    AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                        " & vbNewLine _
                                                 & "              --営業所マスタ                                                   " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_NRS_BR NB                                  " & vbNewLine _
                                                 & "                     ON NB.NRS_BR_CD = OUTL.NRS_BR_CD                          " & vbNewLine _
                                                 & "              --倉庫マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_SOKO SO                                    " & vbNewLine _
                                                 & "                     ON SO.WH_CD = OUTL.WH_CD                                  " & vbNewLine _
                                                 & "              --区分マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..Z_KBN KB1                                    " & vbNewLine _
                                                 & "                     ON KB1.KBN_GROUP_CD = 'N010'                              " & vbNewLine _
                                                 & "                    AND KB1.KBN_CD = OUTL.ARR_PLAN_TIME                        " & vbNewLine _
                                                 & "              --(2012.08.06) フリーダイヤル出力対応 --- START ---              " & vbNewLine _
                                                 & "              --区分マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..Z_KBN KB2                                    " & vbNewLine _
                                                 & "                ON KB2.KBN_GROUP_CD = 'N022'                                   " & vbNewLine _
                                                 & "               AND KB2.KBN_CD       = '00'                                     " & vbNewLine _
                                                 & "               AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                           " & vbNewLine _
                                                 & "              --(2012.08.06) フリーダイヤル出力対応 ---  END  ---              " & vbNewLine _
                                                 & "              --荷主明細マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                         " & vbNewLine _
                                                 & "                     ON MCD01.NRS_BR_CD   = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND MCD01.CUST_CD     = OUTL.CUST_CD_L                     " & vbNewLine _
                                                 & "                    AND MCD01.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MCD01.SUB_KB      = '29'                               " & vbNewLine _
                                                 & "                    AND MCD01.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_TOLL MTOLL                                 " & vbNewLine _
                                                 & "              ON  MTOLL.JIS_CD   = SUBSTRING(SHIPDEST.JIS,1,5)                 " & vbNewLine _
                                                 & "              AND MTOLL.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                                                 & "        WHERE OUTL.SYS_DEL_FLG     = '0'                                       " & vbNewLine _
                                                 & "          AND OUTL.NRS_BR_CD       = @NRS_BR_CD                                " & vbNewLine _
                                                 & "          AND OUTL.CUST_CD_L       = @CUST_CD_L                                " & vbNewLine _
                                                 & "          AND OUTL.CUST_CD_M       = @CUST_CD_M                                " & vbNewLine _
                                                 & "          AND OUTL.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE                          " & vbNewLine _
                                                 & "          AND OUTL.ARR_PLAN_DATE   = @ARR_PLAN_DATE                            " & vbNewLine _
                                                 & "          AND FL.UNSO_CD           = @UNSO_CD                                  " & vbNewLine _
                                                 & "          AND FL.UNSO_BR_CD        = @UNSO_BR_CD                               " & vbNewLine
    '2018/01/23 LMS_大阪トール_まとめ送状・荷札導入対応 Annen add end

    ''' <summary>
    ''' 纏め荷札データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>@(2012.06.05)纏め荷札対応</remarks>
    Private Const SQL_FROM_DATE_MATOME_CUST_1 As String = " FROM                                                                          " & vbNewLine _
                                                 & "      (                                                                        " & vbNewLine _
                                                 & "       SELECT CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                 " & vbNewLine _
                                                 & "                   WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                                 & "              ELSE MR3.RPT_ID                                                  " & vbNewLine _
                                                 & "              END                     AS RPT_ID                                " & vbNewLine _
                                                 & "            , OUTL.NRS_BR_CD          AS NRS_BR_CD                             " & vbNewLine _
                                                 & "            , MAX(DST_2.DEST_NM)      AS DEST_NM                               " & vbNewLine _
                                                 & "            , CASE WHEN OUTL.OUTKA_PKG_NB <> 0 THEN OUTL.OUTKA_PKG_NB          " & vbNewLine _
                                                 & "              ELSE SUM(OUTM.OUTKA_M_PKG_NB)                                    " & vbNewLine _
                                                 & "              END                     AS OUTKA_PKG_NB                          " & vbNewLine _
                                                 & "            , MAX(DST_2.AD_1)         AS AD_1                                  " & vbNewLine _
                                                 & "            , MAX(DST_2.AD_2)         AS AD_2                                  " & vbNewLine _
                                                 & "            , MAX(DST_2.AD_3)         AS AD_3                                  " & vbNewLine _
                                                 & "--            , OUTL.DEST_AD_3          AS AD_3                                  " & vbNewLine _
                                                 & "            , CST.CUST_NM_L           AS CUST_NM_L                             " & vbNewLine _
                                                 & "            , UC.UNSOCO_NM            AS UNSOCO_NM                             " & vbNewLine _
                                                 & "            , OUTL.ARR_PLAN_DATE      AS ARR_PLAN_DATE                         " & vbNewLine _
                                                 & "            , KB1.KBN_NM1             AS ARR_PLAN_TIME                         " & vbNewLine _
                                                 & "            , CASE WHEN MCD.SET_NAIYO <> NULL                                  " & vbNewLine _
                                                 & "                     OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO                 " & vbNewLine _
                                                 & "              ELSE NB.NRS_BR_NM                                                " & vbNewLine _
                                                 & "              END                     AS NRS_BR_NM                             " & vbNewLine _
                                                 & "            --(2012.06.18)要望番号1167 -- START --                             " & vbNewLine _
                                                 & "            --,''                     AS OUTKA_NO_L                            " & vbNewLine _
                                                 & "            , OUTL.OUTKA_NO_L         AS OUTKA_NO_L                            " & vbNewLine _
                                                 & "            --(2012.06.18)要望番号1167 --  END  --                             " & vbNewLine _
                                                 & "            --(2012.08.06) フリーダイヤル出力対応 --- START ---                " & vbNewLine _
                                                 & "            --, CASE WHEN SO.WH_KB = '01' THEN SO.TEL                          " & vbNewLine _
                                                 & "            --  ELSE NB.TEL END      AS TEL                                    " & vbNewLine _
                                                 & "              , CASE WHEN ISNULL(KB2.KBN_NM1,'') <> '' THEN KB2.KBN_NM1        " & vbNewLine _
                                                 & "                     WHEN SO.WH_KB  = '01' THEN SO.TEL                         " & vbNewLine _
                                                 & "                ELSE NB.TEL                                                    " & vbNewLine _
                                                 & "                END                  AS TEL                                    " & vbNewLine _
                                                 & "            --(2012.08.06) フリーダイヤル出力対応 ---  END  ---                " & vbNewLine _
                                                 & "            , CASE WHEN SO.WH_KB = '01' THEN SO.FAX                            " & vbNewLine _
                                                 & "              ELSE NB.FAX END         AS FAX                                   " & vbNewLine _
                                                 & "            , ''                      AS TOP_FLAG                              " & vbNewLine _
                                                 & "            , ''                      AS CUST_NM_S                             " & vbNewLine _
                                                 & "            , OUTL.PC_KB              AS PC_KB                                 " & vbNewLine _
                                                 & "            , OUTL.CUST_CD_L          AS CUST_CD_L                             " & vbNewLine _
                                                 & "            , ''                      AS SHIP_NM_L                             " & vbNewLine _
                                                 & "            , CASE WHEN SHIPDEST.DEST_NM <> ''                                 " & vbNewLine _
                                                 & "                        AND SHIPDEST.DEST_NM IS NOT NULL THEN SHIPDEST.DEST_NM " & vbNewLine _
                                                 & "                   WHEN CST.DENPYO_NM    <> ''                                 " & vbNewLine _
                                                 & "                        AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM     " & vbNewLine _
                                                 & "              ELSE CST.CUST_NM_L                                               " & vbNewLine _
                                                 & "              END                     AS ATSUKAISYA_NM                         " & vbNewLine _
                                                 & "            , ''                      AS LT_DATE                               " & vbNewLine _
                                                 & "            , ''                      AS GOODS_CD_CUST                         " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_1                            " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_2                            " & vbNewLine _
                                                 & "            , ''                      AS GOODS_NM_3                            " & vbNewLine _
                                                 & "            , ''                      AS LOT_NO                                " & vbNewLine _
                                                 & "            , NB.AD_1                 AS NRS_BR_AD_1                           " & vbNewLine _
                                                 & "            , NB.AD_2                 AS NRS_BR_AD_2                           " & vbNewLine _
                                                 & "            , NB.AD_3                 AS NRS_BR_AD_3                           " & vbNewLine _
                                                 & "            , ''                      AS GOODS_SYUBETU                         " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO         AS SET_NAIYO                             " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_2       AS SET_NAIYO_2                           " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_3       AS SET_NAIYO_3                           " & vbNewLine _
                                                 & "            , MAX(DST_2.TEL)          AS DEST_TEL                              " & vbNewLine _
                                                 & "         FROM                                                                  " & vbNewLine _
                                                 & "              $LM_TRN$..C_OUTKA_L OUTL                                         " & vbNewLine _
                                                 & "--(2012.09.06)要望番号1412対応  ---  END  ---                                                     " & vbNewLine _
                                                 & "              --出荷(中)                                                       " & vbNewLine _
                                                 & "              LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                               " & vbNewLine _
                                                 & "                     ON OUTM.NRS_BR_CD    = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND OUTM.OUTKA_NO_L   = OUTL.OUTKA_NO_L                    " & vbNewLine _
                                                 & "                    AND OUTM.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                                 & "              --商品M                                                          " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_GOODS MG                                   " & vbNewLine _
                                                 & "                     ON MG.NRS_BR_CD    = OUTM.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                    " & vbNewLine _
                                                 & "              --出荷Lでの荷主帳票パターン取得                                  " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                              " & vbNewLine _
                                                 & "                     ON OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                                 & "                    AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                                 & "                    AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                                 & "                    AND '00' = MCR1.CUST_CD_S                                  " & vbNewLine _
                                                 & "                    AND MCR1.PTN_ID      = '10'                                " & vbNewLine _
                                                 & "                    AND MCR1.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                                 & "              --帳票パターン取得                                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR1                                    " & vbNewLine _
                                                 & "                     ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MR1.PTN_ID    = MCR1.PTN_ID                            " & vbNewLine _
                                                 & "                    AND MR1.PTN_CD    = MCR1.PTN_CD                            " & vbNewLine _
                                                 & "                    AND MR1.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --商品Mの荷主での荷主帳票パターン取得                            " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                              " & vbNewLine _
                                                 & "                     ON MG.NRS_BR_CD = MCR2.NRS_BR_CD                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_L = MCR2.CUST_CD_L                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_M = MCR2.CUST_CD_M                          " & vbNewLine _
                                                 & "                    AND MG.CUST_CD_S = MCR2.CUST_CD_S                          " & vbNewLine _
                                                 & "                    AND MCR2.PTN_ID  = '10'                                    " & vbNewLine _
                                                 & "                    AND MCR2.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                                 & "              --帳票パターン取得                                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR2                                    " & vbNewLine _
                                                 & "                     ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MR2.PTN_ID    = MCR2.PTN_ID                            " & vbNewLine _
                                                 & "                    AND MR2.PTN_CD    = MCR2.PTN_CD                            " & vbNewLine _
                                                 & "                    AND MR2.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --存在しない場合の帳票パターン取得                               " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_RPT MR3                                    " & vbNewLine _
                                                 & "                     ON MR3.NRS_BR_CD     = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND MR3.PTN_ID        = '10'                               " & vbNewLine _
                                                 & "                    AND MR3.STANDARD_FLAG = '01'                               " & vbNewLine _
                                                 & "                    AND MR3.SYS_DEL_FLG   = '0'                                " & vbNewLine _
                                                 & "              --荷主マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST CST                                   " & vbNewLine _
                                                 & "                     ON CST.NRS_BR_CD  = MG.NRS_BR_CD                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_L  = MG.CUST_CD_L                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_M  = MG.CUST_CD_M                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_S  = MG.CUST_CD_S                          " & vbNewLine _
                                                 & "                    AND CST.CUST_CD_SS = MG.CUST_CD_SS                         " & vbNewLine _
                                                 & "              --荷主明細マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                           " & vbNewLine _
                                                 & "                     ON MCD.NRS_BR_CD   = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND MCD.CUST_CD     = OUTL.CUST_CD_L                       " & vbNewLine _
                                                 & "                    AND MCD.NRS_BR_CD   = @NRS_BR_CD                           " & vbNewLine _
                                                 & "                    AND MCD.SUB_KB      = '11'                                 " & vbNewLine _
                                                 & "                    AND MCD.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "              --届先マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_DEST DST                                   " & vbNewLine _
                                                 & "                     ON DST.NRS_BR_CD = OUTL.NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND DST.CUST_CD_L = OUTL.CUST_CD_L                         " & vbNewLine _
                                                 & "                    AND DST.DEST_CD   = OUTL.DEST_CD                           " & vbNewLine _
                                                 & "              --届先マスタ(表示項目用)                                         " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_DEST DST_2                                 " & vbNewLine _
                                                 & "                     ON DST.NRS_BR_CD    = DST_2.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND DST.CUST_CD_L    = DST_2.CUST_CD_L                     " & vbNewLine _
                                                 & "                    AND DST.CUST_DEST_CD = DST_2.DEST_CD                       " & vbNewLine _
                                                 & "                    AND DST_2.DEST_CD    = @DEST_CD                            " & vbNewLine _
                                                 & "              --届先マスタ（売上先）                                           " & vbNewLine _
                                                 & "                    LEFT JOIN $LM_MST$..M_DEST SHIPDEST                        " & vbNewLine _
                                                 & "                     ON SHIPDEST.NRS_BR_CD = OUTL.NRS_BR_CD                    " & vbNewLine _
                                                 & "                    AND SHIPDEST.CUST_CD_L = OUTL.CUST_CD_L                    " & vbNewLine _
                                                 & "                    AND SHIPDEST.DEST_CD   = OUTL.SHIP_CD_L                    " & vbNewLine _
                                                 & "              --F運送L                                                         " & vbNewLine _
                                                 & "              LEFT JOIN $LM_TRN$..F_UNSO_L FL                                  " & vbNewLine _
                                                 & "                     ON FL.MOTO_DATA_KB = '20'                                 " & vbNewLine _
                                                 & "                    AND FL.NRS_BR_CD    = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND FL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                      " & vbNewLine _
                                                 & "                    AND FL.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                                 & "              --運送会社マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_UNSOCO UC                                  " & vbNewLine _
                                                 & "                     ON UC.NRS_BR_CD    = OUTL.NRS_BR_CD                       " & vbNewLine _
                                                 & "                    AND UC.UNSOCO_CD    = FL.UNSO_CD                           " & vbNewLine _
                                                 & "                    AND UC.UNSOCO_BR_CD = FL.UNSO_BR_CD                        " & vbNewLine _
                                                 & "              --営業所マスタ                                                   " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_NRS_BR NB                                  " & vbNewLine _
                                                 & "                     ON NB.NRS_BR_CD = OUTL.NRS_BR_CD                          " & vbNewLine _
                                                 & "              --倉庫マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_SOKO SO                                    " & vbNewLine _
                                                 & "                     ON SO.WH_CD = OUTL.WH_CD                                  " & vbNewLine _
                                                 & "              --区分マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..Z_KBN KB1                                    " & vbNewLine _
                                                 & "                     ON KB1.KBN_GROUP_CD = 'N010'                              " & vbNewLine _
                                                 & "                    AND KB1.KBN_CD = OUTL.ARR_PLAN_TIME                        " & vbNewLine _
                                                 & "              --(2012.08.06) フリーダイヤル出力対応 --- START ---              " & vbNewLine _
                                                 & "              --区分マスタ                                                     " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..Z_KBN KB2                                    " & vbNewLine _
                                                 & "                ON KB2.KBN_GROUP_CD = 'N022'                                   " & vbNewLine _
                                                 & "               AND KB2.KBN_CD       = '00'                                     " & vbNewLine _
                                                 & "               AND KB2.KBN_NM2      = OUTL.NRS_BR_CD                           " & vbNewLine _
                                                 & "              --(2012.08.06) フリーダイヤル出力対応 ---  END  ---              " & vbNewLine _
                                                 & "              --荷主明細マスタ                                                 " & vbNewLine _
                                                 & "              LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD01                         " & vbNewLine _
                                                 & "                     ON MCD01.NRS_BR_CD   = OUTL.NRS_BR_CD                     " & vbNewLine _
                                                 & "                    AND MCD01.CUST_CD     = OUTL.CUST_CD_L                     " & vbNewLine _
                                                 & "                    AND MCD01.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                                 & "                    AND MCD01.SUB_KB      = '29'                               " & vbNewLine _
                                                 & "                    AND MCD01.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                                 & "        WHERE OUTL.SYS_DEL_FLG     = '0'                                       " & vbNewLine _
                                                 & "          AND OUTL.NRS_BR_CD       = @NRS_BR_CD                                " & vbNewLine _
                                                 & "          AND OUTL.CUST_CD_L       = @CUST_CD_L                                " & vbNewLine _
                                                 & "          AND OUTL.CUST_CD_M       = @CUST_CD_M                                " & vbNewLine _
                                                 & "          AND OUTL.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE                          " & vbNewLine _
                                                 & "          AND OUTL.ARR_PLAN_DATE   = @ARR_PLAN_DATE                            " & vbNewLine _
                                                 & "          AND FL.UNSO_CD           = @UNSO_CD                                  " & vbNewLine _
                                                 & "          AND FL.UNSO_BR_CD        = @UNSO_BR_CD                               " & vbNewLine


    Private Const SQL_FROM_DATE_MATOME_2 As String = " AND OUTL.DEST_CD         = @DEST_CD                                         " & vbNewLine

    Private Const SQL_FROM_DATE_MATOME_CUST_2 As String = " AND DST.CUST_DEST_CD         = @DEST_CD                                  " & vbNewLine

    Private Const SQL_FROM_DATE_MATOME_3 As String = " GROUP BY                                                                      " & vbNewLine _
                                                 & "              MR1.PTN_CD                                                       " & vbNewLine _
                                                 & "            , MR2.PTN_CD                                                       " & vbNewLine _
                                                 & "            , MR1.RPT_ID                                                       " & vbNewLine _
                                                 & "            , MR2.RPT_ID                                                       " & vbNewLine _
                                                 & "            , MR3.RPT_ID                                                       " & vbNewLine _
                                                 & "            , OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                                 & "            , EDIL.OUTKA_CTL_NO                                                " & vbNewLine _
                                                 & "            , OUTL.DEST_NM                                                     " & vbNewLine _
                                                 & "            , EDIL.DEST_NM                                                     " & vbNewLine _
                                                 & "            , DST.DEST_NM                                                      " & vbNewLine _
                                                 & "            , OUTL.OUTKA_PKG_NB                                                " & vbNewLine _
                                                 & "            --, OUTM.OUTKA_M_PKG_NB                                            " & vbNewLine _
                                                 & "            , EDIL.OUTKA_CTL_NO                                                " & vbNewLine _
                                                 & "            , OUTL.DEST_AD_1                                                   " & vbNewLine _
                                                 & "            , EDIL.DEST_AD_1                                                   " & vbNewLine _
                                                 & "            , DST.AD_1                                                         " & vbNewLine _
                                                 & "            , OUTL.DEST_AD_2                                                   " & vbNewLine _
                                                 & "            , EDIL.DEST_AD_2                                                   " & vbNewLine _
                                                 & "            , DST.AD_2                                                         " & vbNewLine _
                                                 & "            , OUTL.DEST_AD_3                                                   " & vbNewLine _
                                                 & "            , FL.AD_3                                                          " & vbNewLine _
                                                 & "            , CST.CUST_NM_L                                                    " & vbNewLine _
                                                 & "            , CST.DENPYO_NM                                                    " & vbNewLine _
                                                 & "            , UC.UNSOCO_NM                                                     " & vbNewLine _
                                                 & "            , OUTL.ARR_PLAN_DATE                                               " & vbNewLine _
                                                 & "            , KB1.KBN_NM1                                                      " & vbNewLine _
                                                 & "            , NB.NRS_BR_NM                                                     " & vbNewLine _
                                                 & "            , OUTL.OUTKA_NO_L                                                  " & vbNewLine _
                                                 & "            , NB.TEL                                                           " & vbNewLine _
                                                 & "            , NB.FAX                                                           " & vbNewLine _
                                                 & "            , SO.WH_KB                                                         " & vbNewLine _
                                                 & "            , SO.TEL                                                           " & vbNewLine _
                                                 & "            , SO.FAX                                                           " & vbNewLine _
                                                 & "            , SHIPDEST.DEST_NM                                                 " & vbNewLine _
                                                 & "            , EDIL.SHIP_NM_L                                                   " & vbNewLine _
                                                 & "            , OUTL.DEST_KB                                                     " & vbNewLine _
                                                 & "            , CST.CUST_NM_S                                                    " & vbNewLine _
                                                 & "            , OUTL.PC_KB                                                       " & vbNewLine _
                                                 & "            , OUTL.CUST_CD_L                                                   " & vbNewLine _
                                                 & "            , MCD.SET_NAIYO                                                    " & vbNewLine _
                                                 & "            --, MG.GOODS_CD_CUST                                               " & vbNewLine _
                                                 & "            --, MG.GOODS_NM_1                                                  " & vbNewLine _
                                                 & "            --, MG.GOODS_NM_2                                                  " & vbNewLine _
                                                 & "            --, MG.GOODS_NM_3                                                  " & vbNewLine _
                                                 & "            , NB.AD_1                                                          " & vbNewLine _
                                                 & "            , NB.AD_2                                                          " & vbNewLine _
                                                 & "            , NB.AD_3                                                          " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO                                                  " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_2                                                " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_3                                                " & vbNewLine _
                                                 & "            , DST.TEL                                                          " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                            " & vbNewLine _
                                                 & "            , KB2.KBN_NM1                                                      " & vbNewLine _
                                                 & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                            " & vbNewLine _
                                                 & "      ) MAIN                                                                   " & vbNewLine

    '2018/01/23 LMS_大阪トール_まとめ送状・荷札導入対応 Annen add start
    Private Const SQL_FROM_DATE_MATOME_3_TALL As String = " GROUP BY                                                               " & vbNewLine _
                                                 & "              MR1.PTN_CD                                                       " & vbNewLine _
                                                 & "            , MR2.PTN_CD                                                       " & vbNewLine _
                                                 & "            , MR1.RPT_ID                                                       " & vbNewLine _
                                                 & "            , MR2.RPT_ID                                                       " & vbNewLine _
                                                 & "            , MR3.RPT_ID                                                       " & vbNewLine _
                                                 & "            , OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                                 & "            , EDIL.OUTKA_CTL_NO                                                " & vbNewLine _
                                                 & "            , OUTL.DEST_NM                                                     " & vbNewLine _
                                                 & "            , EDIL.DEST_NM                                                     " & vbNewLine _
                                                 & "            , DST.DEST_NM                                                      " & vbNewLine _
                                                 & "            , OUTL.OUTKA_PKG_NB                                                " & vbNewLine _
                                                 & "            , EDIL.OUTKA_CTL_NO                                                " & vbNewLine _
                                                 & "            , OUTL.DEST_AD_1                                                   " & vbNewLine _
                                                 & "            , EDIL.DEST_AD_1                                                   " & vbNewLine _
                                                 & "            , DST.AD_1                                                         " & vbNewLine _
                                                 & "            , OUTL.DEST_AD_2                                                   " & vbNewLine _
                                                 & "            , EDIL.DEST_AD_2                                                   " & vbNewLine _
                                                 & "            , DST.AD_2                                                         " & vbNewLine _
                                                 & "            , OUTL.DEST_AD_3                                                   " & vbNewLine _
                                                 & "            , FL.AD_3                                                          " & vbNewLine _
                                                 & "            , CST.CUST_NM_L                                                    " & vbNewLine _
                                                 & "            , CST.DENPYO_NM                                                    " & vbNewLine _
                                                 & "            , UC.UNSOCO_NM                                                     " & vbNewLine _
                                                 & "            , OUTL.ARR_PLAN_DATE                                               " & vbNewLine _
                                                 & "            , KB1.KBN_NM1                                                      " & vbNewLine _
                                                 & "            , NB.NRS_BR_NM                                                     " & vbNewLine _
                                                 & "            , OUTL.OUTKA_NO_L                                                  " & vbNewLine _
                                                 & "            , NB.TEL                                                           " & vbNewLine _
                                                 & "            , NB.FAX                                                           " & vbNewLine _
                                                 & "            , SO.WH_KB                                                         " & vbNewLine _
                                                 & "            , SO.TEL                                                           " & vbNewLine _
                                                 & "            , SO.FAX                                                           " & vbNewLine _
                                                 & "            , SHIPDEST.DEST_NM                                                 " & vbNewLine _
                                                 & "            , EDIL.SHIP_NM_L                                                   " & vbNewLine _
                                                 & "            , OUTL.DEST_KB                                                     " & vbNewLine _
                                                 & "            , CST.CUST_NM_S                                                    " & vbNewLine _
                                                 & "            , OUTL.PC_KB                                                       " & vbNewLine _
                                                 & "            , OUTL.CUST_CD_L                                                   " & vbNewLine _
                                                 & "            , MCD.SET_NAIYO                                                    " & vbNewLine _
                                                 & "            , NB.AD_1                                                          " & vbNewLine _
                                                 & "            , NB.AD_2                                                          " & vbNewLine _
                                                 & "            , NB.AD_3                                                          " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO                                                  " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_2                                                " & vbNewLine _
                                                 & "            , MCD01.SET_NAIYO_3                                                " & vbNewLine _
                                                 & "            , DST.TEL                                                          " & vbNewLine _
                                                 & "            , KB2.KBN_NM1                                                      " & vbNewLine _
                                                 & "            , MTOLL.CHAKU_CD                                                   " & vbNewLine _
                                                 & "            , MTOLL.CHAKU_NM                                                   " & vbNewLine _
                                                 & "      ) MAIN                                                                   " & vbNewLine
    '2018/01/23 LMS_大阪トール_まとめ送状・荷札導入対応 Annen add end


    '要望番号1961 20130322 まとめ荷札対応(BPC対応) 追加START
    Private Const SQL_FROM_DATE_MATOME_CUST_3 As String = " GROUP BY                                                                      " & vbNewLine _
                                             & "              MR1.PTN_CD                                                       " & vbNewLine _
                                             & "            , MR2.PTN_CD                                                       " & vbNewLine _
                                             & "            , MR1.RPT_ID                                                       " & vbNewLine _
                                             & "            , MR2.RPT_ID                                                       " & vbNewLine _
                                             & "            , MR3.RPT_ID                                                       " & vbNewLine _
                                             & "            , OUTL.NRS_BR_CD                                                   " & vbNewLine _
                                             & "            --, EDIL.OUTKA_CTL_NO                                                " & vbNewLine _
                                             & "            --, OUTL.DEST_NM                                                     " & vbNewLine _
                                             & "            --, EDIL.DEST_NM                                                     " & vbNewLine _
                                             & "            --, DST.DEST_NM                                                      " & vbNewLine _
                                             & "            , OUTL.OUTKA_PKG_NB                                                " & vbNewLine _
                                             & "            --, OUTM.OUTKA_M_PKG_NB                                            " & vbNewLine _
                                             & "            --, EDIL.OUTKA_CTL_NO                                                " & vbNewLine _
                                             & "            --, OUTL.DEST_AD_1                                                   " & vbNewLine _
                                             & "            --, EDIL.DEST_AD_1                                                   " & vbNewLine _
                                             & "            --, DST.AD_1                                                         " & vbNewLine _
                                             & "            --, OUTL.DEST_AD_2                                                   " & vbNewLine _
                                             & "            --, EDIL.DEST_AD_2                                                   " & vbNewLine _
                                             & "            --, DST.AD_2                                                         " & vbNewLine _
                                             & "            --, OUTL.DEST_AD_3                                                   " & vbNewLine _
                                             & "            --, FL.AD_3                                                          " & vbNewLine _
                                             & "            , CST.CUST_NM_L                                                    " & vbNewLine _
                                             & "            , CST.DENPYO_NM                                                    " & vbNewLine _
                                             & "            , UC.UNSOCO_NM                                                     " & vbNewLine _
                                             & "            , OUTL.ARR_PLAN_DATE                                               " & vbNewLine _
                                             & "            , KB1.KBN_NM1                                                      " & vbNewLine _
                                             & "            , NB.NRS_BR_NM                                                     " & vbNewLine _
                                             & "            , OUTL.OUTKA_NO_L                                                  " & vbNewLine _
                                             & "            , NB.TEL                                                           " & vbNewLine _
                                             & "            , NB.FAX                                                           " & vbNewLine _
                                             & "            , SO.WH_KB                                                         " & vbNewLine _
                                             & "            , SO.TEL                                                           " & vbNewLine _
                                             & "            , SO.FAX                                                           " & vbNewLine _
                                             & "            , SHIPDEST.DEST_NM                                                 " & vbNewLine _
                                             & "            --, EDIL.SHIP_NM_L                                                   " & vbNewLine _
                                             & "            , OUTL.DEST_KB                                                     " & vbNewLine _
                                             & "            , CST.CUST_NM_S                                                    " & vbNewLine _
                                             & "            , OUTL.PC_KB                                                       " & vbNewLine _
                                             & "            , OUTL.CUST_CD_L                                                   " & vbNewLine _
                                             & "            , MCD.SET_NAIYO                                                    " & vbNewLine _
                                             & "            --, MG.GOODS_CD_CUST                                               " & vbNewLine _
                                             & "            --, MG.GOODS_NM_1                                                  " & vbNewLine _
                                             & "            --, MG.GOODS_NM_2                                                  " & vbNewLine _
                                             & "            --, MG.GOODS_NM_3                                                  " & vbNewLine _
                                             & "            , NB.AD_1                                                          " & vbNewLine _
                                             & "            , NB.AD_2                                                          " & vbNewLine _
                                             & "            , NB.AD_3                                                          " & vbNewLine _
                                             & "            , MCD01.SET_NAIYO                                                  " & vbNewLine _
                                             & "            , MCD01.SET_NAIYO_2                                                " & vbNewLine _
                                             & "            , MCD01.SET_NAIYO_3                                                " & vbNewLine _
                                             & "            --, DST.TEL                                                          " & vbNewLine _
                                             & "--(2012.08.06) フリーダイヤル出力対応 --- START ---                            " & vbNewLine _
                                             & "            , KB2.KBN_NM1                                                      " & vbNewLine _
                                             & "--(2012.08.06) フリーダイヤル出力対応 ---  END  ---                            " & vbNewLine _
                                             & "      ) MAIN                                                                   " & vbNewLine

    ''' <summary>
    ''' 纏め荷札データ抽出用 GROUP BY句
    ''' </summary>
    ''' <remarks>@(2012.06.05) 纏め荷札対応</remarks>
    Private Const SQL_GROUP_BY_MATOME As String = " GROUP BY                  " & vbNewLine _
                                                & "       MAIN.RPT_ID         " & vbNewLine _
                                                & "     , MAIN.NRS_BR_CD      " & vbNewLine _
                                                & "     , MAIN.DEST_NM        " & vbNewLine _
                                                & "     , MAIN.AD_1           " & vbNewLine _
                                                & "     , MAIN.AD_2           " & vbNewLine _
                                                & "     , MAIN.AD_3           " & vbNewLine _
                                                & "     , MAIN.CUST_NM_L      " & vbNewLine _
                                                & "     , MAIN.UNSOCO_NM      " & vbNewLine _
                                                & "     , MAIN.ARR_PLAN_DATE  " & vbNewLine _
                                                & "     , MAIN.ARR_PLAN_TIME  " & vbNewLine _
                                                & "     , MAIN.NRS_BR_NM      " & vbNewLine _
                                                & "--(2012.06.18)要望番号1167 START " & vbNewLine _
                                                & "     --, MAIN.OUTKA_NO_L   " & vbNewLine _
                                                & "--(2012.06.18)要望番号1167 END   " & vbNewLine _
                                                & "     , MAIN.TEL            " & vbNewLine _
                                                & "     , MAIN.FAX            " & vbNewLine _
                                                & "     , MAIN.TOP_FLAG       " & vbNewLine _
                                                & "     , MAIN.CUST_NM_S      " & vbNewLine _
                                                & "     , MAIN.PC_KB          " & vbNewLine _
                                                & "     , MAIN.CUST_CD_L      " & vbNewLine _
                                                & "     , MAIN.SHIP_NM_L      " & vbNewLine _
                                                & "     , MAIN.ATSUKAISYA_NM  " & vbNewLine _
                                                & "     , MAIN.GOODS_CD_CUST  " & vbNewLine _
                                                & "     , MAIN.LT_DATE        " & vbNewLine _
                                                & "     , MAIN.LOT_NO         " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_1     " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_2     " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_3     " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_1    " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_2    " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_3    " & vbNewLine _
                                                & "     , MAIN.GOODS_SYUBETU  " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO      " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO_2    " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO_3    " & vbNewLine _
                                                & "     , MAIN.DEST_TEL       " & vbNewLine

    '2018/01/24 LMS_大阪トール_まとめ送状・荷札導入対応 Annen add start
    Private Const SQL_GROUP_BY_MATOME_TALL As String = " GROUP BY                  " & vbNewLine _
                                                & "       MAIN.RPT_ID         " & vbNewLine _
                                                & "     , MAIN.NRS_BR_CD      " & vbNewLine _
                                                & "     , MAIN.DEST_NM        " & vbNewLine _
                                                & "     , MAIN.AD_1           " & vbNewLine _
                                                & "     , MAIN.AD_2           " & vbNewLine _
                                                & "     , MAIN.AD_3           " & vbNewLine _
                                                & "     , MAIN.CUST_NM_L      " & vbNewLine _
                                                & "     , MAIN.UNSOCO_NM      " & vbNewLine _
                                                & "     , MAIN.ARR_PLAN_DATE  " & vbNewLine _
                                                & "     , MAIN.ARR_PLAN_TIME  " & vbNewLine _
                                                & "     , MAIN.NRS_BR_NM      " & vbNewLine _
                                                & "--(2012.06.18)要望番号1167 START " & vbNewLine _
                                                & "     --, MAIN.OUTKA_NO_L   " & vbNewLine _
                                                & "--(2012.06.18)要望番号1167 END   " & vbNewLine _
                                                & "     , MAIN.TEL            " & vbNewLine _
                                                & "     , MAIN.FAX            " & vbNewLine _
                                                & "     , MAIN.TOP_FLAG       " & vbNewLine _
                                                & "     , MAIN.CUST_NM_S      " & vbNewLine _
                                                & "     , MAIN.PC_KB          " & vbNewLine _
                                                & "     , MAIN.CUST_CD_L      " & vbNewLine _
                                                & "     , MAIN.SHIP_NM_L      " & vbNewLine _
                                                & "     , MAIN.ATSUKAISYA_NM  " & vbNewLine _
                                                & "     , MAIN.GOODS_CD_CUST  " & vbNewLine _
                                                & "     , MAIN.LT_DATE        " & vbNewLine _
                                                & "     , MAIN.LOT_NO         " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_1     " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_2     " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_3     " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_1    " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_2    " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_3    " & vbNewLine _
                                                & "     , MAIN.GOODS_SYUBETU  " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO      " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO_2    " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO_3    " & vbNewLine _
                                                & "     , MAIN.DEST_TEL       " & vbNewLine _
                                                & "     , MAIN.CHAKU_CD       " & vbNewLine _
                                                & "     , MAIN.CHAKU_NM       " & vbNewLine

    '2018/01/24 LMS_大阪トール_まとめ送状・荷札導入対応 Annen add end

    '要望番号1961 20130322 まとめ荷札対応(BPC対応) 追加START
    ''' <summary>
    ''' 纏め荷札データ抽出用 GROUP BY句
    ''' </summary>
    ''' <remarks>@(2012.06.05) 纏め荷札対応</remarks>
    Private Const SQL_GROUP_BY_MATOME_CUST As String = " GROUP BY                  " & vbNewLine _
                                                & "       MAIN.RPT_ID         " & vbNewLine _
                                                & "     , MAIN.NRS_BR_CD      " & vbNewLine _
                                                & "     --, MAIN.DEST_NM        " & vbNewLine _
                                                & "     --, MAIN.AD_1           " & vbNewLine _
                                                & "     --, MAIN.AD_2           " & vbNewLine _
                                                & "     --, MAIN.AD_3           " & vbNewLine _
                                                & "     , MAIN.CUST_NM_L      " & vbNewLine _
                                                & "     , MAIN.UNSOCO_NM      " & vbNewLine _
                                                & "     , MAIN.ARR_PLAN_DATE  " & vbNewLine _
                                                & "     , MAIN.ARR_PLAN_TIME  " & vbNewLine _
                                                & "     , MAIN.NRS_BR_NM      " & vbNewLine _
                                                & "--(2012.06.18)要望番号1167 START " & vbNewLine _
                                                & "     --, MAIN.OUTKA_NO_L   " & vbNewLine _
                                                & "--(2012.06.18)要望番号1167 END   " & vbNewLine _
                                                & "     , MAIN.TEL            " & vbNewLine _
                                                & "     , MAIN.FAX            " & vbNewLine _
                                                & "     , MAIN.TOP_FLAG       " & vbNewLine _
                                                & "     , MAIN.CUST_NM_S      " & vbNewLine _
                                                & "     , MAIN.PC_KB          " & vbNewLine _
                                                & "     , MAIN.CUST_CD_L      " & vbNewLine _
                                                & "     , MAIN.SHIP_NM_L      " & vbNewLine _
                                                & "     , MAIN.ATSUKAISYA_NM  " & vbNewLine _
                                                & "     , MAIN.GOODS_CD_CUST  " & vbNewLine _
                                                & "     , MAIN.LT_DATE        " & vbNewLine _
                                                & "     , MAIN.LOT_NO         " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_1     " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_2     " & vbNewLine _
                                                & "     , MAIN.GOODS_NM_3     " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_1    " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_2    " & vbNewLine _
                                                & "     , MAIN.NRS_BR_AD_3    " & vbNewLine _
                                                & "     , MAIN.GOODS_SYUBETU  " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO      " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO_2    " & vbNewLine _
                                                & "     , MAIN.SET_NAIYO_3    " & vbNewLine _
                                                & "     --, MAIN.DEST_TEL       " & vbNewLine

    ''' <summary>
    ''' 纏め荷札データ抽出用 ORDER BY句
    ''' </summary>
    ''' <remarks>@(2012.06.05) 纏め荷札対応</remarks>
    Private Const SQL_ORDER_BY_MATOME As String = " ORDER BY                  " & vbNewLine _
                                                & "       MAIN.NRS_BR_CD           " & vbNewLine _
                                                & "     , MAIN.CUST_CD_L           " & vbNewLine _
                                                & "     , MAIN.ARR_PLAN_DATE       " & vbNewLine

#End Region

#Region "千葉LMC557(ITW:荷主コード(大)00555)"

    Private Const SQL_SELECT_ADD_LMC557 As String = "        --LMC557 千葉 ITW(荷主大：00555)                                         " & vbNewLine _
                                                 & ",       ''                      AS ALCTD_NB                                       " & vbNewLine _
                                                 & ",       ''                      AS PRINT_SORT                                     " & vbNewLine _
                                                 & ",       ''                      AS UNSO_TTL_NB                                    " & vbNewLine

#End Region

#Region "新標準対応 "

    Private Const SQL_SELECT_ADD_LMC820 As String = "        --LMC820 新標準対応)                                                      " & vbNewLine _
                                                 & ",       ''                      AS AUTO_DENP_NO                                   " & vbNewLine _
                                                 & ",       ''                      AS SHIWAKE_CD                                     " & vbNewLine _
                                                 & ",       ''                      AS CUST_CD_L                                      " & vbNewLine _
                                                 & ",       ''                      AS UNSO_CD             --ADD 2017.08.31           " & vbNewLine _
                                                 & ",       ''                      AS UNSO_BR_CD          --ADD 2017.08.31           " & vbNewLine _
                                                 & ",       ''                      AS NRS_BR_CD           --ADD 2017.08.31           " & vbNewLine


    Private Const SQL_SELECT_ADD_LMC820_2 As String = "        --LMC820 新標準対応)                                                      " & vbNewLine _
                                                 & ",       ''                      AS AUTO_DENP_NO                                   " & vbNewLine _
                                                 & ",       ''                      AS SHIWAKE_CD                                     " & vbNewLine _
                                                 & ",       ''                      AS CUST_ORD_NO                                    " & vbNewLine _
                                                 & ",       ''                      AS UNSO_CD             --ADD 2017.08.31           " & vbNewLine _
                                                 & ",       ''                      AS UNSO_BR_CD          --ADD 2017.08.31           " & vbNewLine _
                                                 & ",       ''                      AS NRS_BR_CD           --ADD 2017.08.31           " & vbNewLine

#End Region

#Region "FFEM用"

    ''' <summary>
    ''' 印刷データ抽出用(FFEM用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ADD_FJF As String _
        = "  , CASE WHEN (CHARINDEX('|', FJF.ZFVYDTEKIYO) = 0)                                            " & vbNewLine _
        & "            OR CHARINDEX('|', FJF.ZFVYDTEKIYO, CHARINDEX('|', FJF.ZFVYDTEKIYO) + 1) = 0        " & vbNewLine _
        & "         THEN ''                                                                               " & vbNewLine _
        & "         ELSE LEFT(FJF.ZFVYDTEKIYO, CHARINDEX('|', FJF.ZFVYDTEKIYO) - 1)                       " & vbNewLine _
        & "    END AS AD_4                                                                                " & vbNewLine _
        & "  , CASE WHEN (CHARINDEX('|', FJF.ZFVYDTEKIYO) = 0)                                            " & vbNewLine _
        & "            OR CHARINDEX('|', FJF.ZFVYDTEKIYO, CHARINDEX('|', FJF.ZFVYDTEKIYO) + 1) = 0        " & vbNewLine _
        & "         THEN ''                                                                               " & vbNewLine _
        & "         ELSE SUBSTRING(FJF.ZFVYDTEKIYO                                                        " & vbNewLine _
        & "                      , CHARINDEX('|', FJF.ZFVYDTEKIYO) + 1                                    " & vbNewLine _
        & "                      , CHARINDEX('|', FJF.ZFVYDTEKIYO, CHARINDEX('|', FJF.ZFVYDTEKIYO) + 1)   " & vbNewLine _
        & "                      - CHARINDEX('|', FJF.ZFVYDTEKIYO) - 1 )                                  " & vbNewLine _
        & "    END AS AD_5                                                                                " & vbNewLine

#End Region


#Region "荷札MAX出力枚数 Notes1126"

    ''' <summary>
    ''' 荷札MAX出力枚数取得SQL文
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_TAG_MAX As String = " SELECT                                         " & vbNewLine _
                                        & "       KBN.KBN_NM1 AS PRINT_MAX                 " & vbNewLine _
                                        & "       FROM $LM_MST$..Z_KBN KBN                 " & vbNewLine _
                                        & " WHERE                                          " & vbNewLine _
                                        & "       KBN.KBN_GROUP_CD = 'N021'                " & vbNewLine _
                                        & "   AND KBN.KBN_NM2      = @NRS_BR_CD            " & vbNewLine _
                                        & "   AND KBN.SYS_DEL_FLG  = '0'                   " & vbNewLine

#End Region

#Region "特殊帳票マスタカウント"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TOKUSHUMPrt As String = "SELECT distinct NRS_BR_CD        " & vbNewLine _
                                                    & "      ,PTN_ID                    " & vbNewLine _
                                                    & "      ,PTN_CD                    " & vbNewLine _
                                                    & "      ,RPT_ID                    " & vbNewLine _
                                                    & "  FROM $LM_MST$..M_RPT           " & vbNewLine _
                                                    & "where                              " & vbNewLine _
                                                    & "NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
                                                    & "and PTN_ID = '10'                  " & vbNewLine _
                                                    & "and  RPT_ID = @RPT_ID             " & vbNewLine _
                                                    & "and sys_del_flg = '0'              " & vbNewLine
#End Region

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    '@(2012.06.05) 纏め荷札対応 --- START ---
    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MRow As Data.DataRow
    '@(2012.06.05) 纏め荷札対応 ---  END  ---

    '(2012.06.08) Notes1126 荷札MAX出力枚数対応  --- START ---
    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _tagRow As Data.DataRow
    '(2012.06.08) Notes1126 荷札MAX出力枚数対応  ---  END  ---

    ''' <summary>
    ''' M_RPT格納用
    ''' </summary>
    ''' <remarks></remarks>
    Private _rptRow As Data.DataRow

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
        Dim inTbl As DataTable = ds.Tables("LMC550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ''SQL作成(フラグ = '1'の場合、運送データ)
        'If LMConst.FLG.ON.Equals(Me._Row.Item("PTN_FLAG").ToString()) = True Then
        '    Me._StrSql.Append(LMC550DAC.SQL_SELECT_MPRT_UNSO)
        '    Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_PRT)
        '    Me._StrSql.Append(LMC550DAC.SQL_WHERE_UNSO_DATA)
        'Else
        '    Me._StrSql.Append(LMC550DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        '    Me._StrSql.Append(LMC550DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        'End If

        'SQL作成(フラグ = '0'の場合、出荷データ。フラグ = '1'の場合、運送データ。フラグ = '2'の場合、詰め合せデータ。)
        Select Case Me._Row.Item("PTN_FLAG").ToString
            Case "0" '出荷データ
                Me._StrSql.Append(LMC550DAC.SQL_SELECT_MPrt)
                Me._StrSql.Append(LMC550DAC.SQL_FROM)

            Case "1" '運送データ
                Me._StrSql.Append(LMC550DAC.SQL_SELECT_MPRT_UNSO)
                Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_PRT)
                Me._StrSql.Append(LMC550DAC.SQL_WHERE_UNSO_DATA)

            Case "2" '詰め合わせデータ
                Me._StrSql.Append(LMC550DAC.SQL_SELECT_MPrt)
                Me._StrSql.Append(LMC550DAC.SQL_FROM)

            Case Else '出荷データとする
                Me._StrSql.Append(LMC550DAC.SQL_SELECT_MPrt)
                Me._StrSql.Append(LMC550DAC.SQL_FROM)

        End Select

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetOutkaParameter(inTbl, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC550DAC", "SelectMPrt", cmd)

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
    ''' 荷札データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷札データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC550IN")
        Dim matTbl As DataTable = ds.Tables("LMC550_MATOME")

        '帳票パターン取得(LMC557)
        Dim rptTbl As DataTable = ds.Tables("M_RPT")
        Me._rptRow = rptTbl.Rows(0)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        '@(2012.06.05) 纏め荷札対応 --- START ---
        If Me._Row.Item("MATOME_FLG").Equals("1") = True Then
            Me._MRow = matTbl.Rows(0)
        End If
        '@(2012.06.05) 纏め荷札対応 ---  END  ---

        '営業所CDの取得
        Dim NrsBrCd As String = Me._Row.Item("NRS_BR_CD").ToString()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'CHAKU_CD,CHAKU_NMが無い場合TRUEとし、マッピングにADDしない
        Dim chakuNoAddFlg As Boolean = False
        Dim useSQL_SELECT_DATA As Boolean = False

        'LMC557(千葉：ITW 荷主コード00555対応)
        If Me._rptRow.Item("RPT_ID").ToString = "LMC557" Then

            'SQL作成(フラグ = '0'の場合、出荷データ。フラグ = '1'の場合、運送データ。)
            Select Case Me._Row.Item("PTN_FLAG").ToString
                Case "0" '出荷データ
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA_LMC557)        'SQL構築(データ抽出用SELECT句)
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820)         'ADD 2016/06/29 西濃対応追加分
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_LMC557)               'SQL構築(データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_LMC557)           'SQL構築(データ抽出用GROUP BY句)
                    Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY_LMC557)           'SQL構築(データ抽出用ORDER BY句)

                Case "1" '運送データ
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_UNSO_DATA_LMC557)   'SQL構築(運送データ用SELECT句)
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820)         'ADD 2016/06/29 西濃対応追加分
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_DATA4_LMC557)    'SQL構築(運送データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_PRT_LMC557)      'SQL構築(運送データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_WHERE_UNSO_DATA_LMC557)    'SQL構築(運送データ抽出用ORDER BY句)

                Case Else '出荷データとする
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA_LMC557)        'SQL構築(データ抽出用SELECT句)
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820)         'ADD 2016/06/29 西濃対応追加分
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_LMC557)               'SQL構築(データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_LMC557)           'SQL構築(データ抽出用GROUP BY句)
                    Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY_LMC557)           'SQL構築(データ抽出用ORDER BY句)

            End Select


        ElseIf Me._rptRow.Item("RPT_ID").ToString = "LMC760" OrElse Me._rptRow.Item("RPT_ID").ToString = "LMC808" Then
            'LMC760(千葉：アクタス 荷主コード00750対応)
            'SQL作成(フラグ = '0'の場合、出荷データ。フラグ = '1'の場合、運送データ。)
            Select Case Me._Row.Item("PTN_FLAG").ToString
                Case "0" '出荷データ
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA_LMC760)        'SQL構築(データ抽出用SELECT句)
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820)         'ADD 2016/06/29 西濃対応追加分
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_LMC760)               'SQL構築(データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_LMC760)           'SQL構築(データ抽出用GROUP BY句)
                    Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)

                Case "1" '運送データ
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_UNSO_DATA_LMC760)   'SQL構築(運送データ用SELECT句)
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820)         'ADD 2016/06/29 西濃対応追加分
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_DATA4_LMC760)    'SQL構築(運送データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_PRT)      'SQL構築(運送データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_WHERE_UNSO_DATA)    'SQL構築(運送データ抽出用ORDER BY句)

                Case Else '出荷データとする
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA_LMC760)        'SQL構築(データ抽出用SELECT句)
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820)         'ADD 2016/06/29 西濃対応追加分
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_LMC760)               'SQL構築(データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_LMC760)           'SQL構築(データ抽出用GROUP BY句)
                    Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)

            End Select


        ElseIf Me._rptRow.Item("RPT_ID").ToString = "LMC763" OrElse _
               Me._rptRow.Item("RPT_ID").ToString = "LMC821" Then

            ' FFEM専用


            'SQL作成(フラグ = '0'の場合、出荷データ。フラグ = '1'の場合、運送データ。フラグ = '2'の場合、詰め合せデータ。)
            Select Case Me._Row.Item("PTN_FLAG").ToString
                Case "0" '出荷データ
                    If Me._rptRow.Item("RPT_ID").ToString = "LMC821" Then
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA_LMC821) 'SQL構築(データ抽出用SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_FJF)     'SQL構築(FFEM用の項目を追加)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_BASE)          'SQL構築(データ抽出用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_ADD_FJF)       'SQL構築(FFEM用追加項目)
                        Me._StrSql.Append(LMC550DAC.SQL_WHERE)              'SQL構築(FFEM用追加項目)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_LMC821)    'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_ADD_FJF)   'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                    Else
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA)        'SQL構築(データ抽出用SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_FJF)     'SQL構築(FFEM用の項目を追加)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_BASE)          'SQL構築(データ抽出用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_ADD_FJF)       'SQL構築(FFEM用追加項目)
                        Me._StrSql.Append(LMC550DAC.SQL_WHERE)              'SQL構築(FFEM用追加項目)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY)           'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_ADD_FJF)   'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                        useSQL_SELECT_DATA = True
                    End If

                Case "1" '運送データ
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_UNSO_DATA)   'SQL構築(運送データ用SELECT句)
                    Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_DATA4)    'SQL構築(運送データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_PRT)      'SQL構築(運送データ抽出用FROM句)
                    Me._StrSql.Append(LMC550DAC.SQL_WHERE_UNSO_DATA)    'SQL構築(運送データ抽出用ORDER BY句)

                Case Else '出荷データとする
                    If Me._rptRow.Item("RPT_ID").ToString = "LMC821" Then
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA_LMC821) 'SQL構築(データ抽出用SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_FJF)     'SQL構築(FFEM用の項目を追加)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_BASE)          'SQL構築(データ抽出用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_ADD_FJF)       'SQL構築(FFEM用追加項目)
                        Me._StrSql.Append(LMC550DAC.SQL_WHERE)              'SQL構築(FFEM用追加項目)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_LMC821)    'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_ADD_FJF)   'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                    Else
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA)        'SQL構築(データ抽出用SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_FJF)     'SQL構築(FFEM用の項目を追加)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_BASE)          'SQL構築(データ抽出用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_ADD_FJF)       'SQL構築(FFEM用追加項目)
                        Me._StrSql.Append(LMC550DAC.SQL_WHERE)              'SQL構築(FFEM用追加項目)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY)           'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_ADD_FJF)   'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                        useSQL_SELECT_DATA = True
                    End If

            End Select

        Else
            '@(2012.06.05) 纏め荷札対応 --- START ---
            If Me._Row.Item("MATOME_FLG").Equals("1") = True Then

                '要望番号1961 20130322 まとめ荷札対応(BPC対応) 修正START
                Select Case Me._Row.Item("MATOME_DEST_KBN").ToString()
                    Case "00"
                        '2018/01/25 LMS_大阪トール_まとめ送状・荷札導入対応 Annen upd start
                        If Me._rptRow.Item("RPT_ID").ToString = "LMC855" OrElse _
                           Me._rptRow.Item("RPT_ID").ToString = "LMC558" Then
                            Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATE_MATOME_TALL)      'SQL構築(纏め荷札用データ抽出SELECT句)
                            Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)
                            Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820_2)          'ADD 2016/06/29 新標準追加分
                            Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_1_TALL)      'SQL構築(纏め荷札用データ抽出FROM句)
                            Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_2)           'SQL構築(纏め荷札用データ抽出FROM句)
                            Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_3_TALL)      'SQL構築(纏め荷札用データ抽出FROM句)
                            Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_MATOME_TALL)         'SQL構築(纏め荷札用データ抽出GROUP BY句)
                        Else
                            Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATE_MATOME)           'SQL構築(纏め荷札用データ抽出SELECT句)
                            Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)
                            Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820_2)          'ADD 2016/06/29 新標準追加分
                            Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_1)           'SQL構築(纏め荷札用データ抽出FROM句)
                            Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_2)           'SQL構築(纏め荷札用データ抽出FROM句)
                            Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_3)           'SQL構築(纏め荷札用データ抽出FROM句)
                            Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_MATOME)              'SQL構築(纏め荷札用データ抽出GROUP BY句)

                            chakuNoAddFlg = True

                        End If
                        '2018/01/25 LMS_大阪トール_まとめ送状・荷札導入対応 Annen upd end
                    Case "01"
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATE_MATOME_CUST)      'SQL構築(纏め荷札BP用データ抽出SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820_2)           'ADD 2016/06/29 新標準応追加分
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_CUST_1)      'SQL構築(纏め荷札BP用データ抽出FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_CUST_2)      'SQL構築(纏め荷札BP用データ抽出FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_CUST_3)      'SQL構築(纏め荷札BP用データ抽出FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_MATOME_CUST)         'SQL構築(纏め荷札BP用データ抽出GROUP BY句)

                        chakuNoAddFlg = True

                    Case Else
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATE_MATOME)           'SQL構築(纏め荷札用データ抽出SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC820_2)          'ADD 2016/06/29 新標準対応追加分
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_1)           'SQL構築(纏め荷札用データ抽出FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_2)           'SQL構築(纏め荷札用データ抽出FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_DATE_MATOME_3)           'SQL構築(纏め荷札用データ抽出FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_MATOME)              'SQL構築(纏め荷札用データ抽出GROUP BY句)

                        chakuNoAddFlg = True

                End Select
                '要望番号1961 20130322 まとめ荷札対応(BPC対応) 修正END

                Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY_MATOME)            'SQL構築(纏め荷札用データ抽出ORDER BY句)

            Else
                'SQL作成(フラグ = '0'の場合、出荷データ。フラグ = '1'の場合、運送データ。フラグ = '2'の場合、詰め合せデータ。)
                'If LMConst.FLG.ON.Equals(Me._Row.Item("PTN_FLAG").ToString()) = True Then
                Select Case Me._Row.Item("PTN_FLAG").ToString
                    Case "0" '出荷データ
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA)        'SQL構築(データ抽出用SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM)               'SQL構築(データ抽出用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY)           'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                        useSQL_SELECT_DATA = True

                    Case "1" '運送データ
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_UNSO_DATA)   'SQL構築(運送データ用SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_DATA4)    'SQL構築(運送データ抽出用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_PRT)      'SQL構築(運送データ抽出用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_WHERE_UNSO_DATA)    'SQL構築(運送データ抽出用ORDER BY句)

                    Case "2" '詰め合わせデータ
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_LMC551)      'SQL構築(LMC551用SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM_LMC551)        'SQL構築(LMC551用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_LMC551)    'SQL構築(LMC551用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)

                        chakuNoAddFlg = True

                    Case Else '出荷データとする
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA)        'SQL構築(データ抽出用SELECT句)
                        Me._StrSql.Append(LMC550DAC.SQL_SELECT_ADD_LMC557)  'SQL構築(LMC557用に増やされたDSに空白を渡す)
                        Me._StrSql.Append(LMC550DAC.SQL_FROM)               'SQL構築(データ抽出用FROM句)
                        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY)           'SQL構築(データ抽出用GROUP BY句)
                        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                        useSQL_SELECT_DATA = True

                End Select

            End If

        End If

        ''SQL作成(フラグ = '0'の場合、出荷データ。フラグ = '1'の場合、運送データ。フラグ = '2'の場合、詰め合せデータ。)
        ''If LMConst.FLG.ON.Equals(Me._Row.Item("PTN_FLAG").ToString()) = True Then
        'Select Case Me._Row.Item("PTN_FLAG").ToString
        '    Case "0" '出荷データ
        '        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        '        Me._StrSql.Append(LMC550DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        '        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        '        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        '    Case "1" '運送データ
        '        Me._StrSql.Append(LMC550DAC.SQL_SELECT_UNSO_DATA) 'SQL構築(運送データ用Select句)
        '        Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_DATA4)  'SQL構築(運送データ抽出用From句)
        '        Me._StrSql.Append(LMC550DAC.SQL_FROM_UNSO_PRT)    'SQL構築(運送データ抽出用From句)
        '        Me._StrSql.Append(LMC550DAC.SQL_WHERE_UNSO_DATA)  'SQL構築(運送データ抽出用ORDER BY句)

        '    Case "2" '詰め合わせデータ
        '        Me._StrSql.Append(LMC550DAC.SQL_SELECT_LMC551)    'SQL構築(LMC551用Select句)
        '        Me._StrSql.Append(LMC550DAC.SQL_FROM_LMC551)      'SQL構築(LMC551用From句)
        '        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY_LMC551)  'SQL構築(LMC551用GROUP BY句)
        '        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        '    Case Else '出荷データとする
        '        Me._StrSql.Append(LMC550DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        '        Me._StrSql.Append(LMC550DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        '        Me._StrSql.Append(LMC550DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        '        Me._StrSql.Append(LMC550DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'End Select

        '@(2012.06.05) 纏め荷札対応 ---  END  ---

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        '@(2012.06.05) 纏め荷札対応 --- START ---
        If Me._Row.Item("MATOME_FLG").Equals("1") = True Then
            Call Me.SetOutkaParameterMatome(matTbl, Me._SqlPrmList)
        Else
            Call Me.SetOutkaParameter(inTbl, Me._SqlPrmList)
        End If
        'パラメータ設定
        'Call Me.SetOutkaParameter(inTbl, Me._SqlPrmList)

        '@(2012.06.05) 纏め荷札対応 ---  END  ---

        'パラメータの反映()
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC550DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("TOP_FLAG", "TOP_FLAG")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("PC_KB", "PC_KB")
        map.Add("ATSUKAISYA_NM", "ATSUKAISYA_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("NRS_BR_AD_1", "NRS_BR_AD_1")
        map.Add("NRS_BR_AD_2", "NRS_BR_AD_2")
        map.Add("NRS_BR_AD_3", "NRS_BR_AD_3")
        map.Add("GOODS_NM_3", "GOODS_NM_3")
        map.Add("GOODS_SYUBETU", "GOODS_SYUBETU")
        'LMC552(日本ファイン[00000])対応追加
        map.Add("SET_NAIYO", "SET_NAIYO")
        'LMC553(埼玉浮間用)対応追加
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")
        map.Add("SET_NAIYO_3", "SET_NAIYO_3")
        map.Add("DEST_TEL", "DEST_TEL")
        'LMC557千葉(荷主大：00555)
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")

        'アクタス専用荷札の場合
        If Me._rptRow.Item("RPT_ID").ToString = "LMC760" OrElse Me._rptRow.Item("RPT_ID").ToString = "LMC808" Then
            map.Add("DEST_CD", "DEST_CD")
            map.Add("AREA_CD", "AREA_CD")
            map.Add("SOKO_CD", "SOKO_CD")
            'map.Add("CUST_ORD_NO", "CUST_ORD_NO")  '下に移動
        End If

        'ADD 2016/06/29 新汎用版対応
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("AUTO_DENP_NO", "AUTO_DENP_NO")
        map.Add("SHIWAKE_CD", "SHIWAKE_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")

        If Me._rptRow.Item("RPT_ID").ToString = "LMC763" OrElse _
           Me._rptRow.Item("RPT_ID").ToString = "LMC821" Then

            map.Add("AD_4", "AD_4")
            map.Add("AD_5", "AD_5")

        End If

        'ADD 2017/08/31 運送会社追加版対応
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")

        'ADD 2017/09/11 トール対応
        '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen upd start
        If Me._rptRow.Item("RPT_ID").ToString = "LMC820" OrElse _
            Me._rptRow.Item("RPT_ID").ToString = "LMC849" OrElse _
            Me._rptRow.Item("RPT_ID").ToString = "LMC855" OrElse _
            Me._rptRow.Item("RPT_ID").ToString = "LMC558" OrElse _
            Me._rptRow.Item("RPT_ID").ToString = "LMC824" OrElse _ 
            Me._rptRow.Item("RPT_ID").ToString = "LMC860" Then

            'TRUEの場合はマッピングにADDしない
            If chakuNoAddFlg = False Then
                map.Add("CHAKU_CD", "CHAKU_CD")
                map.Add("CHAKU_NM", "CHAKU_NM")
            End If
        End If
        'If Me._rptRow.Item("RPT_ID").ToString = "LMC820" OrElse _
        '    Me._rptRow.Item("RPT_ID").ToString = "LMC849" OrElse _
        '    Me._rptRow.Item("RPT_ID").ToString = "LMC855" OrElse _
        '    Me._rptRow.Item("RPT_ID").ToString = "LMC824" Then
        '    map.Add("CHAKU_CD", "CHAKU_CD")
        '    map.Add("CHAKU_NM", "CHAKU_NM")
        'End If
        '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen upd end

        If useSQL_SELECT_DATA Then
            map.Add("OKIBA_KOSU_CSV", "OKIBA_KOSU_CSV")
        End If


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC550OUT")

        Return ds

    End Function

    '@(2012.06.05) 纏め荷札対応 --- START ---
    ''' <summary>
    ''' 纏め条件の取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMatome(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Select Case Me._Row.Item("MATOME_DEST_KBN").ToString()

            Case "00"
                Me._StrSql.Append(LMC550DAC.SQL_SELECT_MATOME)
                Me._StrSql.Append(LMC550DAC.SQL_FROM_MATOME)

            Case "01"
                Me._StrSql.Append(LMC550DAC.SQL_SELECT_MATOME_CUST_DEST)
                Me._StrSql.Append(LMC550DAC.SQL_FROM_MATOME_CUST_DEST)

            Case Else
                Me._StrSql.Append(LMC550DAC.SQL_SELECT_MATOME)
                Me._StrSql.Append(LMC550DAC.SQL_FROM_MATOME)

        End Select


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetOutkaParameter(inTbl, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC550DAC", "SelectMatome", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("DEST_CD", "DEST_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC550_MATOME")

        Return ds

    End Function
    '@(2012.06.05) 纏め荷札対応 ---  END ---

    '(2012.06.08) 荷札MAX出力枚数対応 --- START ---
    ''' <summary>
    ''' 荷札MAX出力枚数の取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTagMax(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMC550DAC.SQL_TAG_MAX)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetOutkaParameter(inTbl, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC550DAC", "SqlTagMax", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PRINT_MAX", "PRINT_MAX")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC550_KBN")

        Return ds

    End Function
    '@(2012.06.05) 纏め荷札対応 ---  END ---


    ''' <summary>
    ''' 在庫データのチェックデータ取得パラメータ設定
    ''' </summary>
    ''' <param name="conditiontbl">DataTable</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaParameter(ByVal conditiontbl As DataTable, ByVal prmList As ArrayList)


        '2011/09/20 SBS)佐川 複数IN条件対応はBLCで行っているため削除
        'With conditiontbl

        '    '出荷管理番号(大)はWHERE句にてINを使用しているため、結合処理を行った後に設定
        '    Dim max As Integer = conditiontbl.Rows.Count - 1
        '    Dim outkaNoL As String = String.Empty

        '    For i As Integer = 0 To max
        '        If String.IsNullOrEmpty(outkaNoL) = False Then
        '            outkaNoL = String.Concat(outkaNoL, ",")
        '        End If
        '        outkaNoL = String.Concat(outkaNoL, conditiontbl.Rows(0).Item("OUTKA_NO_L").ToString())

        '    Next
        '    prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", outkaNoL, DBDataType.CHAR))

        'End With

        Dim whereStr As String = String.Empty

        '出荷管理番号(大)
        whereStr = Me._Row.Item("OUTKA_NO_L").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", whereStr, DBDataType.CHAR))

        '営業所
        whereStr = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

    End Sub

    '@(2012.06.05) 纏め荷札対応 --- START ---
    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOutkaParameterMatome(ByVal conditiontbl As DataTable, ByVal prmList As ArrayList)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._MRow

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            '出荷日
            whereStr = .Item("OUTKA_PLAN_DATE").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", whereStr, DBDataType.CHAR))

            '納入日
            whereStr = .Item("ARR_PLAN_DATE").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", whereStr, DBDataType.CHAR))

            'START YANAI 要望番号1478 一括印刷が遅い
            ''運送会社コード
            'whereStr = .Item("UNSO_CD").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", whereStr, DBDataType.NVARCHAR))
            '運送会社コード
            whereStr = .Item("UNSO_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", whereStr, DBDataType.NVARCHAR))
            'END YANAI 要望番号1478 一括印刷が遅い

            'START YANAI 要望番号1478 一括印刷が遅い
            ''運送会社支店コード
            'whereStr = .Item("UNSO_BR_CD").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", whereStr, DBDataType.NVARCHAR))
            '運送会社支店コード
            whereStr = .Item("UNSO_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", whereStr, DBDataType.NVARCHAR))
            'END YANAI 要望番号1478 一括印刷が遅い

            'START YANAI 要望番号1478 一括印刷が遅い
            ''届先コード
            'whereStr = .Item("DEST_CD").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", whereStr, DBDataType.NVARCHAR))
            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", whereStr, DBDataType.NVARCHAR))
            'END YANAI 要望番号1478 一括印刷が遅い

        End With

    End Sub
    '@(2012.06.05) 纏め荷札対応 ---  END  ---

    '(2012.06.08)  荷札MAX出力枚数対応 --- START ---
    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOutkaParameterTagMax(ByVal conditiontbl As DataTable, ByVal prmList As ArrayList)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._tagRow

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

        End With

    End Sub
    '@(2012.06.08)  荷札MAX出力枚数対応 ---  END  ---

    ''' <summary>
    ''' 帳票パターンマスタ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectTokushuMRpt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMC550DAC.SQL_SELECT_TOKUSHUMPrt)     'SQL構築(カウント用Select句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_ID", Me._Row.Item("TOKUSHU_RPT_ID").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC550DAC", "SelectTokushuMRpt", cmd)

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
