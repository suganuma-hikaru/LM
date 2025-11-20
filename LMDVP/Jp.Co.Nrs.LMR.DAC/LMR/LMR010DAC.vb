' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMR       : 完了
'  プログラムID     :  LMR010    : 完了取込
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMR010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMR010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "SELECT"

#Region "入荷検索"

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    Private Const SQL_GET_TRN_TBL_EXISTS As String = "" _
        & "SELECT                                                       " & vbNewLine _
        & "    CASE WHEN OBJECT_ID('$LM_TRN$..' + @TBL_NM, 'U') IS NULL " & vbNewLine _
        & "        THEN '0'                                             " & vbNewLine _
        & "        ELSE '1'                                             " & vbNewLine _
        & "    END AS TBL_EXISTS                                        " & vbNewLine _
        & ""

    ''' <summary>
    ''' カウント用（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_INKA As String = " SELECT COUNT(CNT.SELECT_CNT) AS SELECT_CNT FROM(SELECT COUNT(INKA_L.NRS_BR_CD)	            AS SELECT_CNT          " & vbNewLine

    'START YANAI 要望番号433
    '''' <summary>
    '''' SELECT（入荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_INKA As String = " SELECT                                                                " & vbNewLine _
    '                                        & " INKA_L.INKA_NO_L                                    AS INOUTKA_NO_L        " & vbNewLine _
    '                                        & ",INKA_L.INKA_DATE                                    AS INOUTKA_DATE        " & vbNewLine _
    '                                        & ",INKA_L.OUTKA_FROM_ORD_NO_L                          AS INOUTKA_ORD_NO      " & vbNewLine _
    '                                        & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
    '                                        & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
    '                                        & ",''                                                  AS DEST_NM             " & vbNewLine _
    '                                        & ",''                                                  AS PKG_NB              " & vbNewLine _
    '                                        & ",INKA_L.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
    '                                        & ",SOKO.INKA_KENPIN_YN                                 AS INKA_KENPIN_YN      " & vbNewLine _
    '                                        & ",SOKO.INKA_KAKUNIN_YN                                AS INKA_KAKUNIN_YN     " & vbNewLine _
    '                                        & ",''                                                  AS OUTKA_KENPIN_YN     " & vbNewLine _
    '                                        & ",''                                                  AS OUTKA_INFO_YN       " & vbNewLine _
    '                                        & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
    '                                        & ",''                                                  AS END_DATE            " & vbNewLine _
    '                                        & ",''                                                  AS NIHUDA_FLAG         " & vbNewLine _
    '                                        & ",SOKO.LOC_MANAGER_YN                                 AS LOC_MANAGER_YN      " & vbNewLine _
    '                                        & ",ISNULL(INKA_S.OKIBA_COUNT,0)                        AS OKIBA_COUNT         " & vbNewLine _
    '                                        & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
    '                                        & ",INKA_L.SYS_UPD_DATE                                 AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",INKA_L.SYS_UPD_TIME                                 AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",''                                                  AS CHK_INKA_STATE_KB   " & vbNewLine _
    '                                        & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine
    'START YANAI 要望番号932
    '''' <summary>
    '''' SELECT（入荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_INKA As String = " SELECT                                                                " & vbNewLine _
    '                                            & " INKA_L.INKA_NO_L                                    AS INOUTKA_NO_L        " & vbNewLine _
    '                                            & ",INKA_L.INKA_DATE                                    AS INOUTKA_DATE        " & vbNewLine _
    '                                            & ",INKA_L.OUTKA_FROM_ORD_NO_L                          AS INOUTKA_ORD_NO      " & vbNewLine _
    '                                            & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
    '                                            & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
    '                                            & ",''                                                  AS DEST_NM             " & vbNewLine _
    '                                            & ",''                                                  AS PKG_NB              " & vbNewLine _
    '                                            & ",INKA_L.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
    '                                            & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
    '                                            & ",SOKO.INKA_KENPIN_YN                                 AS INKA_KENPIN_YN      " & vbNewLine _
    '                                            & ",SOKO.INKA_KAKUNIN_YN                                AS INKA_KAKUNIN_YN     " & vbNewLine _
    '                                            & ",''                                                  AS OUTKA_KENPIN_YN     " & vbNewLine _
    '                                            & ",''                                                  AS OUTKA_INFO_YN       " & vbNewLine _
    '                                            & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
    '                                            & ",''                                                  AS END_DATE            " & vbNewLine _
    '                                            & ",''                                                  AS NIHUDA_FLAG         " & vbNewLine _
    '                                            & ",SOKO.LOC_MANAGER_YN                                 AS LOC_MANAGER_YN      " & vbNewLine _
    '                                            & ",ISNULL(INKA_S.OKIBA_COUNT,0)                        AS OKIBA_COUNT         " & vbNewLine _
    '                                            & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
    '                                            & ",INKA_L.SYS_UPD_DATE                                 AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",INKA_L.SYS_UPD_TIME                                 AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",''                                                  AS CHK_INKA_STATE_KB   " & vbNewLine _
    '                                            & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
    '                                            & ",ISNULL(INKA_S2.OKIBA_COUNT2,0)                      AS OKIBA_COUNT2        " & vbNewLine _
    ''' <summary>
    ''' SELECT（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_INKA As String = " SELECT                                                                    " & vbNewLine _
                                                & " INKA_L.INKA_NO_L                                    AS INOUTKA_NO_L        " & vbNewLine _
                                                & ",INKA_L.INKA_DATE                                    AS INOUTKA_DATE        " & vbNewLine _
                                                & ",INKA_L.OUTKA_FROM_ORD_NO_L                          AS INOUTKA_ORD_NO      " & vbNewLine _
                                                & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
                                                & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
                                                & ",''                                                  AS DEST_NM             " & vbNewLine _
                                                & ",''                                                  AS PKG_NB              " & vbNewLine _
                                                & ",INKA_L.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
                                                & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
                                                & ",SOKO.INKA_KENPIN_YN                                 AS INKA_KENPIN_YN      " & vbNewLine _
                                                & ",SOKO.INKA_KAKUNIN_YN                                AS INKA_KAKUNIN_YN     " & vbNewLine _
                                                & ",''                                                  AS OUTKA_KENPIN_YN     " & vbNewLine _
                                                & ",''                                                  AS OUTKA_INFO_YN       " & vbNewLine _
                                                & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
                                                & ",''                                                  AS END_DATE            " & vbNewLine _
                                                & ",''                                                  AS NIHUDA_FLAG         " & vbNewLine _
                                                & ",SOKO.LOC_MANAGER_YN                                 AS LOC_MANAGER_YN      " & vbNewLine _
                                                & ",ISNULL(INKA_S.OKIBA_COUNT,0)                        AS OKIBA_COUNT         " & vbNewLine _
                                                & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
                                                & ",INKA_L.SYS_UPD_DATE                                 AS SYS_UPD_DATE        " & vbNewLine _
                                                & ",INKA_L.SYS_UPD_TIME                                 AS SYS_UPD_TIME        " & vbNewLine _
                                                & ",''                                                  AS CHK_INKA_STATE_KB   " & vbNewLine _
                                                & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
                                                & ",ISNULL(INKA_S2.OKIBA_COUNT2,0)                      AS OKIBA_COUNT2        " & vbNewLine _
                                                & ",''                                                  AS SCNT                " & vbNewLine _
                                                & ", INKA_L.WH_KENPIN_WK_STATUS                         AS WH_KENPIN_WK_STATUS " & vbNewLine _
    'END YANAI 要望番号932
    'END YANAI 要望番号433

    ''' <summary>
    ''' SELECT 2 TSMC 固有（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_INKA2_TSMC As String = "" _
        & ",ISNULL(                                                              " & vbNewLine _
        & "   (SELECT COUNT(*)                                                   " & vbNewLine _
        & "    FROM                                                              " & vbNewLine _
        & "        $LM_TRN$..D_ZAI_TSMC                                          " & vbNewLine _
        & "    WHERE                                                             " & vbNewLine _
        & "        D_ZAI_TSMC.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
        & "    AND D_ZAI_TSMC.INKA_CTL_NO_L = INKA_L.INKA_NO_L                   " & vbNewLine _
        & "    AND D_ZAI_TSMC.SYS_DEL_FLG = '0'), 0)            AS TSMC_QTY_SUMI " & vbNewLine _
        & ",SUM(CAST(                                                            " & vbNewLine _
        & "        CASE WHEN ISNUMERIC(H_INKAEDI_DTL_TSMC.TSMC_QTY) = 0          " & vbNewLine _
        & "            THEN '0'                                                  " & vbNewLine _
        & "            ELSE ISNULL(H_INKAEDI_DTL_TSMC.TSMC_QTY, '0')             " & vbNewLine _
        & "        END                                                           " & vbNewLine _
        & "        AS NUMERIC(10) ) /* ←CAST */                                 " & vbNewLine _
        & "    ) /* ←SUM */                                    AS TSMC_QTY      " & vbNewLine _
        & ""

    ''' <summary>
    ''' SELECT 2（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_INKA2 As String = "" _
        & ", 0                                                  AS TSMC_QTY_SUMI " & vbNewLine _
        & ", 0                                                  AS TSMC_QTY      " & vbNewLine _
        & ""

    'START YANAI 要望番号433
    '''' <summary>
    '''' FROM（入荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_INKA As String = "FROM                                                      " & vbNewLine _
    '                                            & " $LM_TRN$..B_INKA_L INKA_L                                 " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & "  $LM_MST$..M_CUST    MCUST                                " & vbNewLine _
    '                                            & " ON                                                        " & vbNewLine _
    '                                            & "        MCUST.NRS_BR_CD = INKA_L.NRS_BR_CD                 " & vbNewLine _
    '                                            & " AND    MCUST.CUST_CD_L = INKA_L.CUST_CD_L                 " & vbNewLine _
    '                                            & " AND    MCUST.CUST_CD_M = INKA_L.CUST_CD_M                 " & vbNewLine _
    '                                            & " AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
    '                                            & " AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
    '                                            & " AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                            & " AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & " (SELECT                                                   " & vbNewLine _
    '                                            & "  MTCUST2.CUST_CD_L AS CUST_CD_L                           " & vbNewLine _
    '                                            & " ,MTCUST2.CUST_CD_M AS CUST_CD_M                           " & vbNewLine _
    '                                            & " ,MIN(MTCUST2.USER_CD) AS USER_CD                          " & vbNewLine _
    '                                            & " ,MIN(MTCUST2.USER_CD_EDA) AS USER_CD_EDA                  " & vbNewLine _
    '                                            & "  FROM                                                     " & vbNewLine _
    '                                            & "  $LM_MST$..M_TCUST MTCUST2                                " & vbNewLine _
    '                                            & "  GROUP BY                                                 " & vbNewLine _
    '                                            & "  MTCUST2.CUST_CD_L                                        " & vbNewLine _
    '                                            & " ,MTCUST2.CUST_CD_M                                        " & vbNewLine _
    '                                            & " ) MTCUST                                                  " & vbNewLine _
    '                                            & " ON     MTCUST.CUST_CD_L = INKA_L.CUST_CD_L                " & vbNewLine _
    '                                            & " AND    MTCUST.CUST_CD_M = INKA_L.CUST_CD_M                " & vbNewLine _
    '                                            & " AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                            & " LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
    '                                            & " ON     SUSER.USER_CD = MTCUST.USER_CD                     " & vbNewLine _
    '                                            & " LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
    '                                            & " ON     SOKO.NRS_BR_CD = INKA_L.NRS_BR_CD                  " & vbNewLine _
    '                                            & " AND    SOKO.WH_CD = INKA_L.WH_CD                          " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & " (SELECT                                                   " & vbNewLine _
    '                                            & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT                 " & vbNewLine _
    '                                            & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
    '                                            & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
    '                                            & "  FROM                                                     " & vbNewLine _
    '                                            & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
    '                                            & "  WHERE                                                    " & vbNewLine _
    '                                            & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
    '                                            & "   OR                                                      " & vbNewLine _
    '                                            & "   INKA_S2.SITU_NO = ''                                    " & vbNewLine _
    '                                            & "   OR                                                      " & vbNewLine _
    '                                            & "   INKA_S2.ZONE_CD = '')                                   " & vbNewLine _
    '                                            & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                            & "  GROUP BY                                                 " & vbNewLine _
    '                                            & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
    '                                            & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
    '                                            & " ) INKA_S                                                  " & vbNewLine _
    '                                            & " ON     INKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD                " & vbNewLine _
    '                                            & " AND    INKA_S.INKA_NO_L = INKA_L.INKA_NO_L                " & vbNewLine
    'START YANAI 要望番号824
    '''' <summary>
    '''' FROM（入荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_INKA As String = "FROM                                                      " & vbNewLine _
    '                                                & " $LM_TRN$..B_INKA_L INKA_L                                 " & vbNewLine _
    '                                                & " LEFT JOIN                                                 " & vbNewLine _
    '                                                & "  $LM_MST$..M_CUST    MCUST                                " & vbNewLine _
    '                                                & " ON                                                        " & vbNewLine _
    '                                                & "        MCUST.NRS_BR_CD = INKA_L.NRS_BR_CD                 " & vbNewLine _
    '                                                & " AND    MCUST.CUST_CD_L = INKA_L.CUST_CD_L                 " & vbNewLine _
    '                                                & " AND    MCUST.CUST_CD_M = INKA_L.CUST_CD_M                 " & vbNewLine _
    '                                                & " AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
    '                                                & " AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
    '                                                & " AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                                & " AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                                & " LEFT JOIN                                                 " & vbNewLine _
    '                                                & " (SELECT                                                   " & vbNewLine _
    '                                                & "  MTCUST2.CUST_CD_L AS CUST_CD_L                           " & vbNewLine _
    '                                                & " ,MTCUST2.CUST_CD_M AS CUST_CD_M                           " & vbNewLine _
    '                                                & " ,MIN(MTCUST2.USER_CD) AS USER_CD                          " & vbNewLine _
    '                                                & " ,MIN(MTCUST2.USER_CD_EDA) AS USER_CD_EDA                  " & vbNewLine _
    '                                                & "  FROM                                                     " & vbNewLine _
    '                                                & "  $LM_MST$..M_TCUST MTCUST2                                " & vbNewLine _
    '                                                & "  GROUP BY                                                 " & vbNewLine _
    '                                                & "  MTCUST2.CUST_CD_L                                        " & vbNewLine _
    '                                                & " ,MTCUST2.CUST_CD_M                                        " & vbNewLine _
    '                                                & " ) MTCUST                                                  " & vbNewLine _
    '                                                & " ON     MTCUST.CUST_CD_L = INKA_L.CUST_CD_L                " & vbNewLine _
    '                                                & " AND    MTCUST.CUST_CD_M = INKA_L.CUST_CD_M                " & vbNewLine _
    '                                                & " AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
    '                                                & " ON     SUSER.USER_CD = MTCUST.USER_CD                     " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
    '                                                & " ON     SOKO.NRS_BR_CD = INKA_L.NRS_BR_CD                  " & vbNewLine _
    '                                                & " AND    SOKO.WH_CD = INKA_L.WH_CD                          " & vbNewLine _
    '                                                & " LEFT JOIN                                                 " & vbNewLine _
    '                                                & " (SELECT                                                   " & vbNewLine _
    '                                                & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT                 " & vbNewLine _
    '                                                & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
    '                                                & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
    '                                                & "  FROM                                                     " & vbNewLine _
    '                                                & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
    '                                                & "  WHERE                                                    " & vbNewLine _
    '                                                & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
    '                                                & "   OR                                                      " & vbNewLine _
    '                                                & "   INKA_S2.SITU_NO = ''                                    " & vbNewLine _
    '                                                & "   OR                                                      " & vbNewLine _
    '                                                & "   INKA_S2.ZONE_CD = '')                                   " & vbNewLine _
    '                                                & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                                & "  GROUP BY                                                 " & vbNewLine _
    '                                                & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
    '                                                & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
    '                                                & " ) INKA_S                                                  " & vbNewLine _
    '                                                & " ON     INKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD                " & vbNewLine _
    '                                                & " AND    INKA_S.INKA_NO_L = INKA_L.INKA_NO_L                " & vbNewLine _
    '                                                & " LEFT JOIN                                                 " & vbNewLine _
    '                                                & " (SELECT                                                   " & vbNewLine _
    '                                                & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT2                " & vbNewLine _
    '                                                & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
    '                                                & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
    '                                                & "  FROM                                                     " & vbNewLine _
    '                                                & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
    '                                                & "  WHERE                                                    " & vbNewLine _
    '                                                & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
    '                                                & "   OR                                                      " & vbNewLine _
    '                                                & "   INKA_S2.SITU_NO = '')                                   " & vbNewLine _
    '                                                & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                                & "  GROUP BY                                                 " & vbNewLine _
    '                                                & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
    '                                                & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
    '                                                & " ) INKA_S2                                                 " & vbNewLine _
    '                                                & " ON     INKA_S2.NRS_BR_CD = INKA_L.NRS_BR_CD               " & vbNewLine _
    '                                                & " AND    INKA_S2.INKA_NO_L = INKA_L.INKA_NO_L               " & vbNewLine
    ''' <summary>
    ''' FROM（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_INKA As String = "FROM                                                      " & vbNewLine _
                                                    & " $LM_TRN$..B_INKA_L INKA_L                                 " & vbNewLine _
                                                    & " LEFT JOIN                                                 " & vbNewLine _
                                                    & "  $LM_MST$..M_CUST    MCUST                                " & vbNewLine _
                                                    & " ON                                                        " & vbNewLine _
                                                    & "        MCUST.NRS_BR_CD = INKA_L.NRS_BR_CD                 " & vbNewLine _
                                                    & " AND    MCUST.CUST_CD_L = INKA_L.CUST_CD_L                 " & vbNewLine _
                                                    & " AND    MCUST.CUST_CD_M = INKA_L.CUST_CD_M                 " & vbNewLine _
                                                    & " AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
                                                    & " AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
                                                    & " AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                                    & " AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
                                                    & " ON     SUSER.USER_CD = MCUST.TANTO_CD                     " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
                                                    & " ON     SOKO.NRS_BR_CD = INKA_L.NRS_BR_CD                  " & vbNewLine _
                                                    & " AND    SOKO.WH_CD = INKA_L.WH_CD                          " & vbNewLine _
                                                    & " LEFT JOIN                                                 " & vbNewLine _
                                                    & " (SELECT                                                   " & vbNewLine _
                                                    & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT                 " & vbNewLine _
                                                    & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
                                                    & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
                                                    & "  FROM                                                     " & vbNewLine _
                                                    & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
                                                    & "  WHERE                                                    " & vbNewLine _
                                                    & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
                                                    & "   OR                                                      " & vbNewLine _
                                                    & "   INKA_S2.SITU_NO = ''                                    " & vbNewLine _
                                                    & "   OR                                                      " & vbNewLine _
                                                    & "   INKA_S2.ZONE_CD = '')                                   " & vbNewLine _
                                                    & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                                    & "  GROUP BY                                                 " & vbNewLine _
                                                    & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
                                                    & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
                                                    & " ) INKA_S                                                  " & vbNewLine _
                                                    & " ON     INKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD                " & vbNewLine _
                                                    & " AND    INKA_S.INKA_NO_L = INKA_L.INKA_NO_L                " & vbNewLine _
                                                    & " LEFT JOIN                                                 " & vbNewLine _
                                                    & " (SELECT                                                   " & vbNewLine _
                                                    & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT2                " & vbNewLine _
                                                    & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
                                                    & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
                                                    & "  FROM                                                     " & vbNewLine _
                                                    & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
                                                    & "  WHERE                                                    " & vbNewLine _
                                                    & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
                                                    & "   OR                                                      " & vbNewLine _
                                                    & "   INKA_S2.SITU_NO = '')                                   " & vbNewLine _
                                                    & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                                    & "  GROUP BY                                                 " & vbNewLine _
                                                    & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
                                                    & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
                                                    & " ) INKA_S2                                                 " & vbNewLine _
                                                    & " ON     INKA_S2.NRS_BR_CD = INKA_L.NRS_BR_CD               " & vbNewLine _
                                                    & " AND    INKA_S2.INKA_NO_L = INKA_L.INKA_NO_L               " & vbNewLine
    'END YANAI 要望番号824
    'END YANAI 要望番号433

    ''' <summary>
    ''' FROM 2 TSMC 固有（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_INKA2_TSMC As String = "" _
        & "LEFT JOIN                                                       " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_DTL_TSMC                                " & vbNewLine _
        & "        ON  H_INKAEDI_DTL_TSMC.NRS_BR_CD = @NRS_BR_CD           " & vbNewLine _
        & "        AND H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L = INKA_L.INKA_NO_L " & vbNewLine _
        & "        AND H_INKAEDI_DTL_TSMC.SYS_DEL_FLG = '0'                " & vbNewLine _
        & ""

    'START YANAI 要望番号433
    '''' <summary>
    '''' GROUP BY（入荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_GROUP_BY_INKA As String = "GROUP BY                                     " & vbNewLine _
    '                                     & " INKA_L.INKA_NO_L                                        " & vbNewLine _
    '                                     & ",INKA_L.INKA_DATE                                        " & vbNewLine _
    '                                     & ",INKA_L.OUTKA_FROM_ORD_NO_L                              " & vbNewLine _
    '                                     & ",SUSER.USER_NM                                           " & vbNewLine _
    '                                     & ",MCUST.CUST_NM_L                                         " & vbNewLine _
    '                                     & ",MCUST.CUST_NM_M                                         " & vbNewLine _
    '                                     & ",INKA_L.NRS_BR_CD                                        " & vbNewLine _
    '                                     & ",SOKO.INKA_KENPIN_YN                                     " & vbNewLine _
    '                                     & ",SOKO.INKA_KAKUNIN_YN                                    " & vbNewLine _
    '                                     & ",SOKO.LOC_MANAGER_YN                                     " & vbNewLine _
    '                                     & ",INKA_S.OKIBA_COUNT                                      " & vbNewLine _
    '                                     & ",INKA_L.HOKAN_STR_DATE                                   " & vbNewLine _
    '                                     & ",INKA_L.SYS_UPD_DATE                                     " & vbNewLine _
    '                                     & ",INKA_L.SYS_UPD_TIME                                     " & vbNewLine _
    '                                     & ",MCUST.CUST_CD_L                                         " & vbNewLine
    ''' <summary>
    ''' GROUP BY（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_INKA As String = "GROUP BY                                     " & vbNewLine _
                                             & " INKA_L.INKA_NO_L                                        " & vbNewLine _
                                             & ",INKA_L.INKA_DATE                                        " & vbNewLine _
                                             & ",INKA_L.OUTKA_FROM_ORD_NO_L                              " & vbNewLine _
                                             & ",SUSER.USER_NM                                           " & vbNewLine _
                                             & ",MCUST.CUST_NM_L                                         " & vbNewLine _
                                             & ",MCUST.CUST_NM_M                                         " & vbNewLine _
                                             & ",INKA_L.NRS_BR_CD                                        " & vbNewLine _
                                             & ",SOKO.INKA_KENPIN_YN                                     " & vbNewLine _
                                             & ",SOKO.INKA_KAKUNIN_YN                                    " & vbNewLine _
                                             & ",SOKO.LOC_MANAGER_YN                                     " & vbNewLine _
                                             & ",INKA_S.OKIBA_COUNT                                      " & vbNewLine _
                                             & ",INKA_L.HOKAN_STR_DATE                                   " & vbNewLine _
                                             & ",INKA_L.SYS_UPD_DATE                                     " & vbNewLine _
                                             & ",INKA_L.SYS_UPD_TIME                                     " & vbNewLine _
                                             & ",MCUST.CUST_CD_L                                         " & vbNewLine _
                                             & ",INKA_S2.OKIBA_COUNT2                                    " & vbNewLine _
                                             & ",INKA_L.WH_KENPIN_WK_STATUS                              " & vbNewLine _
        'END YANAI 要望番号433

    ''' <summary>
    ''' ORDER BY（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_INKA As String = "ORDER BY                                     " & vbNewLine _
                                         & " INKA_L.INKA_NO_L                                        " & vbNewLine _
                                         & ",INKA_L.INKA_DATE                                        " & vbNewLine

    ''' <summary>
    ''' COUNT時使用（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_END_INKA As String = ") CNT                                          "


    'START YANAI 要望番号653
    'START YANAI 要望番号932
    '''' <summary>
    '''' SELECT（入荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_INKA_ZAI As String = " SELECT                                                                " & vbNewLine _
    '                                        & " INKA_L.INKA_NO_L                                    AS INOUTKA_NO_L        " & vbNewLine _
    '                                        & ",INKA_L.INKA_DATE                                    AS INOUTKA_DATE        " & vbNewLine _
    '                                        & ",INKA_L.OUTKA_FROM_ORD_NO_L                          AS INOUTKA_ORD_NO      " & vbNewLine _
    '                                        & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
    '                                        & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
    '                                        & ",''                                                  AS DEST_NM             " & vbNewLine _
    '                                        & ",''                                                  AS PKG_NB              " & vbNewLine _
    '                                        & ",INKA_L.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
    '                                        & ",SOKO.INKA_KENPIN_YN                                 AS INKA_KENPIN_YN      " & vbNewLine _
    '                                        & ",SOKO.INKA_KAKUNIN_YN                                AS INKA_KAKUNIN_YN     " & vbNewLine _
    '                                        & ",''                                                  AS OUTKA_KENPIN_YN     " & vbNewLine _
    '                                        & ",''                                                  AS OUTKA_INFO_YN       " & vbNewLine _
    '                                        & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
    '                                        & ",''                                                  AS END_DATE            " & vbNewLine _
    '                                        & ",''                                                  AS NIHUDA_FLAG         " & vbNewLine _
    '                                        & ",SOKO.LOC_MANAGER_YN                                 AS LOC_MANAGER_YN      " & vbNewLine _
    '                                        & ",ISNULL(INKA_S.OKIBA_COUNT,0)                        AS OKIBA_COUNT         " & vbNewLine _
    '                                        & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
    '                                        & ",INKA_L.SYS_UPD_DATE                                 AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",INKA_L.SYS_UPD_TIME                                 AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",''                                                  AS CHK_INKA_STATE_KB   " & vbNewLine _
    '                                        & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
    '                                        & ",ISNULL(INKA_S2.OKIBA_COUNT2,0)                      AS OKIBA_COUNT2        " & vbNewLine _
    '                                        & ",INKA_S3.ZAI_REC_NO                                  AS ZAI_REC_NO          " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_DATE                                    AS ZAI_SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_TIME                                    AS ZAI_SYS_UPD_TIME        " & vbNewLine
    ''' <summary>
    ''' SELECT（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_INKA_ZAI As String = " SELECT                                                            " & vbNewLine _
                                            & " INKA_L.INKA_NO_L                                    AS INOUTKA_NO_L        " & vbNewLine _
                                            & ",INKA_L.INKA_DATE                                    AS INOUTKA_DATE        " & vbNewLine _
                                            & ",INKA_L.OUTKA_FROM_ORD_NO_L                          AS INOUTKA_ORD_NO      " & vbNewLine _
                                            & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
                                            & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
                                            & ",''                                                  AS DEST_NM             " & vbNewLine _
                                            & ",''                                                  AS PKG_NB              " & vbNewLine _
                                            & ",INKA_L.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
                                            & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
                                            & ",SOKO.INKA_KENPIN_YN                                 AS INKA_KENPIN_YN      " & vbNewLine _
                                            & ",SOKO.INKA_KAKUNIN_YN                                AS INKA_KAKUNIN_YN     " & vbNewLine _
                                            & ",''                                                  AS OUTKA_KENPIN_YN     " & vbNewLine _
                                            & ",''                                                  AS OUTKA_INFO_YN       " & vbNewLine _
                                            & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
                                            & ",''                                                  AS END_DATE            " & vbNewLine _
                                            & ",''                                                  AS NIHUDA_FLAG         " & vbNewLine _
                                            & ",SOKO.LOC_MANAGER_YN                                 AS LOC_MANAGER_YN      " & vbNewLine _
                                            & ",ISNULL(INKA_S.OKIBA_COUNT,0)                        AS OKIBA_COUNT         " & vbNewLine _
                                            & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
                                            & ",INKA_L.SYS_UPD_DATE                                 AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",INKA_L.SYS_UPD_TIME                                 AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",''                                                  AS CHK_INKA_STATE_KB   " & vbNewLine _
                                            & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
                                            & ",ISNULL(INKA_S2.OKIBA_COUNT2,0)                      AS OKIBA_COUNT2        " & vbNewLine _
                                            & ",INKA_S3.ZAI_REC_NO                                  AS ZAI_REC_NO          " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                                    AS ZAI_SYS_UPD_DATE    " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                                    AS ZAI_SYS_UPD_TIME    " & vbNewLine _
                                            & ",''                                                  AS SCNT                " & vbNewLine _
                                            & ", CASE WHEN SHOULD_KENPIN_CHK.KBN_GROUP_CD IS NULL                          " & vbNewLine _
                                            & "       THEN 0                                                               " & vbNewLine _
                                            & "       ELSE 1                                                               " & vbNewLine _
                                            & "   END                                               AS CHK_KENPIN_WK_ON    " & vbNewLine _
    'END YANAI 要望番号932
    'START YANAI 要望番号824
    '''' <summary>
    '''' FROM（入荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_INKA_ZAI As String = "FROM                                                      " & vbNewLine _
    '                                            & " $LM_TRN$..B_INKA_L INKA_L                                 " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & "  $LM_MST$..M_CUST    MCUST                                " & vbNewLine _
    '                                            & " ON                                                        " & vbNewLine _
    '                                            & "        MCUST.NRS_BR_CD = INKA_L.NRS_BR_CD                 " & vbNewLine _
    '                                            & " AND    MCUST.CUST_CD_L = INKA_L.CUST_CD_L                 " & vbNewLine _
    '                                            & " AND    MCUST.CUST_CD_M = INKA_L.CUST_CD_M                 " & vbNewLine _
    '                                            & " AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
    '                                            & " AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
    '                                            & " AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                            & " AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & " (SELECT                                                   " & vbNewLine _
    '                                            & "  MTCUST2.CUST_CD_L AS CUST_CD_L                           " & vbNewLine _
    '                                            & " ,MTCUST2.CUST_CD_M AS CUST_CD_M                           " & vbNewLine _
    '                                            & " ,MIN(MTCUST2.USER_CD) AS USER_CD                          " & vbNewLine _
    '                                            & " ,MIN(MTCUST2.USER_CD_EDA) AS USER_CD_EDA                  " & vbNewLine _
    '                                            & "  FROM                                                     " & vbNewLine _
    '                                            & "  $LM_MST$..M_TCUST MTCUST2                                " & vbNewLine _
    '                                            & "  GROUP BY                                                 " & vbNewLine _
    '                                            & "  MTCUST2.CUST_CD_L                                        " & vbNewLine _
    '                                            & " ,MTCUST2.CUST_CD_M                                        " & vbNewLine _
    '                                            & " ) MTCUST                                                  " & vbNewLine _
    '                                            & " ON     MTCUST.CUST_CD_L = INKA_L.CUST_CD_L                " & vbNewLine _
    '                                            & " AND    MTCUST.CUST_CD_M = INKA_L.CUST_CD_M                " & vbNewLine _
    '                                            & " AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                            & " LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
    '                                            & " ON     SUSER.USER_CD = MTCUST.USER_CD                     " & vbNewLine _
    '                                            & " LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
    '                                            & " ON     SOKO.NRS_BR_CD = INKA_L.NRS_BR_CD                  " & vbNewLine _
    '                                            & " AND    SOKO.WH_CD = INKA_L.WH_CD                          " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & " (SELECT                                                   " & vbNewLine _
    '                                            & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT                 " & vbNewLine _
    '                                            & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
    '                                            & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
    '                                            & "  FROM                                                     " & vbNewLine _
    '                                            & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
    '                                            & "  WHERE                                                    " & vbNewLine _
    '                                            & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
    '                                            & "   OR                                                      " & vbNewLine _
    '                                            & "   INKA_S2.SITU_NO = ''                                    " & vbNewLine _
    '                                            & "   OR                                                      " & vbNewLine _
    '                                            & "   INKA_S2.ZONE_CD = '')                                   " & vbNewLine _
    '                                            & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                            & "  GROUP BY                                                 " & vbNewLine _
    '                                            & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
    '                                            & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
    '                                            & " ) INKA_S                                                  " & vbNewLine _
    '                                            & " ON     INKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD                " & vbNewLine _
    '                                            & " AND    INKA_S.INKA_NO_L = INKA_L.INKA_NO_L                " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & " (SELECT                                                   " & vbNewLine _
    '                                            & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT2                " & vbNewLine _
    '                                            & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
    '                                            & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
    '                                            & "  FROM                                                     " & vbNewLine _
    '                                            & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
    '                                            & "  WHERE                                                    " & vbNewLine _
    '                                            & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
    '                                            & "   OR                                                      " & vbNewLine _
    '                                            & "   INKA_S2.SITU_NO = '')                                   " & vbNewLine _
    '                                            & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                            & "  GROUP BY                                                 " & vbNewLine _
    '                                            & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
    '                                            & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
    '                                            & " ) INKA_S2                                                 " & vbNewLine _
    '                                            & " ON     INKA_S2.NRS_BR_CD = INKA_L.NRS_BR_CD               " & vbNewLine _
    '                                            & " AND    INKA_S2.INKA_NO_L = INKA_L.INKA_NO_L               " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & " $LM_TRN$..B_INKA_S INKA_S3                                " & vbNewLine _
    '                                            & " ON     INKA_S3.NRS_BR_CD = INKA_L.NRS_BR_CD               " & vbNewLine _
    '                                            & " AND    INKA_S3.INKA_NO_L = INKA_L.INKA_NO_L               " & vbNewLine _
    '                                            & " AND    INKA_S3.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                            & " LEFT JOIN                                                 " & vbNewLine _
    '                                            & " $LM_TRN$..D_ZAI_TRS ZAI                                   " & vbNewLine _
    '                                            & " ON     ZAI.NRS_BR_CD = INKA_S3.NRS_BR_CD                  " & vbNewLine _
    '                                            & " AND    ZAI.ZAI_REC_NO = INKA_S3.ZAI_REC_NO                " & vbNewLine _
    '                                            & " AND    ZAI.SYS_DEL_FLG = '0'                              " & vbNewLine
    ''' <summary>
    ''' FROM（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_INKA_ZAI As String = "FROM                                                      " & vbNewLine _
                                                & " $LM_TRN$..B_INKA_L INKA_L                                 " & vbNewLine _
                                                & " LEFT JOIN                                                 " & vbNewLine _
                                                & "  $LM_MST$..M_CUST    MCUST                                " & vbNewLine _
                                                & " ON                                                        " & vbNewLine _
                                                & "        MCUST.NRS_BR_CD = INKA_L.NRS_BR_CD                 " & vbNewLine _
                                                & " AND    MCUST.CUST_CD_L = INKA_L.CUST_CD_L                 " & vbNewLine _
                                                & " AND    MCUST.CUST_CD_M = INKA_L.CUST_CD_M                 " & vbNewLine _
                                                & " AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
                                                & " AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
                                                & " AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                                & " AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                                & " LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
                                                & " ON     SUSER.USER_CD = MCUST.TANTO_CD                     " & vbNewLine _
                                                & " LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
                                                & " ON     SOKO.NRS_BR_CD = INKA_L.NRS_BR_CD                  " & vbNewLine _
                                                & " AND    SOKO.WH_CD = INKA_L.WH_CD                          " & vbNewLine _
                                                & " LEFT JOIN                                                 " & vbNewLine _
                                                & " (SELECT                                                   " & vbNewLine _
                                                & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT                 " & vbNewLine _
                                                & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
                                                & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
                                                & "  FROM                                                     " & vbNewLine _
                                                & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
                                                & "  WHERE                                                    " & vbNewLine _
                                                & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
                                                & "   OR                                                      " & vbNewLine _
                                                & "   INKA_S2.SITU_NO = ''                                    " & vbNewLine _
                                                & "   OR                                                      " & vbNewLine _
                                                & "   INKA_S2.ZONE_CD = '')                                   " & vbNewLine _
                                                & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                                & "  GROUP BY                                                 " & vbNewLine _
                                                & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
                                                & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
                                                & " ) INKA_S                                                  " & vbNewLine _
                                                & " ON     INKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD                " & vbNewLine _
                                                & " AND    INKA_S.INKA_NO_L = INKA_L.INKA_NO_L                " & vbNewLine _
                                                & " LEFT JOIN                                                 " & vbNewLine _
                                                & " (SELECT                                                   " & vbNewLine _
                                                & "  COUNT(INKA_S2.NRS_BR_CD)  AS OKIBA_COUNT2                " & vbNewLine _
                                                & " ,INKA_S2.NRS_BR_CD AS NRS_BR_CD                           " & vbNewLine _
                                                & " ,INKA_S2.INKA_NO_L AS INKA_NO_L                           " & vbNewLine _
                                                & "  FROM                                                     " & vbNewLine _
                                                & "  $LM_TRN$..B_INKA_S INKA_S2                               " & vbNewLine _
                                                & "  WHERE                                                    " & vbNewLine _
                                                & "  (INKA_S2.TOU_NO = ''                                     " & vbNewLine _
                                                & "   OR                                                      " & vbNewLine _
                                                & "   INKA_S2.SITU_NO = '')                                   " & vbNewLine _
                                                & "  AND INKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                                & "  GROUP BY                                                 " & vbNewLine _
                                                & "  INKA_S2.NRS_BR_CD                                        " & vbNewLine _
                                                & " ,INKA_S2.INKA_NO_L                                        " & vbNewLine _
                                                & " ) INKA_S2                                                 " & vbNewLine _
                                                & " ON     INKA_S2.NRS_BR_CD = INKA_L.NRS_BR_CD               " & vbNewLine _
                                                & " AND    INKA_S2.INKA_NO_L = INKA_L.INKA_NO_L               " & vbNewLine _
                                                & " LEFT JOIN                                                 " & vbNewLine _
                                                & " $LM_TRN$..B_INKA_S INKA_S3                                " & vbNewLine _
                                                & " ON     INKA_S3.NRS_BR_CD = INKA_L.NRS_BR_CD               " & vbNewLine _
                                                & " AND    INKA_S3.INKA_NO_L = INKA_L.INKA_NO_L               " & vbNewLine _
                                                & " AND    INKA_S3.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                                & " LEFT JOIN                                                 " & vbNewLine _
                                                & " $LM_TRN$..D_ZAI_TRS ZAI                                   " & vbNewLine _
                                                & " ON     ZAI.NRS_BR_CD = INKA_S3.NRS_BR_CD                  " & vbNewLine _
                                                & " AND    ZAI.ZAI_REC_NO = INKA_S3.ZAI_REC_NO                " & vbNewLine _
                                                & " AND    ZAI.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                                & "LEFT JOIN                                                  " & vbNewLine _
                                                & "     (SELECT KBN_GROUP_CD                                  " & vbNewLine _
                                                & "           , KBN_NM1                                       " & vbNewLine _
                                                & "           , KBN_NM2                                       " & vbNewLine _
                                                & "           , KBN_NM3                                       " & vbNewLine _
                                                & "        FROM $LM_MST$..Z_KBN                               " & vbNewLine _
                                                & "       WHERE KBN_GROUP_CD = 'N027'                         " & vbNewLine _
                                                & "         AND SYS_DEL_FLG  = '0'                            " & vbNewLine _
                                                & "       GROUP BY                                            " & vbNewLine _
                                                & "             KBN_GROUP_CD                                  " & vbNewLine _
                                                & "           , KBN_NM1                                       " & vbNewLine _
                                                & "           , KBN_NM2                                       " & vbNewLine _
                                                & "           , KBN_NM3                                       " & vbNewLine _
                                                & "   ) AS SHOULD_KENPIN_CHK                                  " & vbNewLine _
                                                & "  ON SHOULD_KENPIN_CHK.KBN_NM1 = ZAI.NRS_BR_CD             " & vbNewLine _
                                                & " AND SHOULD_KENPIN_CHK.KBN_NM2 = ZAI.WH_CD                 " & vbNewLine _
                                                & " AND SHOULD_KENPIN_CHK.KBN_NM3 = ZAI.TOU_NO                " & vbNewLine

    'END YANAI 要望番号824

    ''' <summary>
    ''' GROUP BY（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_INKA_ZAI As String = "GROUP BY                                     " & vbNewLine _
                                     & " INKA_L.INKA_NO_L                                        " & vbNewLine _
                                     & ",INKA_L.INKA_DATE                                        " & vbNewLine _
                                     & ",INKA_L.OUTKA_FROM_ORD_NO_L                              " & vbNewLine _
                                     & ",SUSER.USER_NM                                           " & vbNewLine _
                                     & ",MCUST.CUST_NM_L                                         " & vbNewLine _
                                     & ",MCUST.CUST_NM_M                                         " & vbNewLine _
                                     & ",INKA_L.NRS_BR_CD                                        " & vbNewLine _
                                     & ",SOKO.INKA_KENPIN_YN                                     " & vbNewLine _
                                     & ",SOKO.INKA_KAKUNIN_YN                                    " & vbNewLine _
                                     & ",SOKO.LOC_MANAGER_YN                                     " & vbNewLine _
                                     & ",INKA_S.OKIBA_COUNT                                      " & vbNewLine _
                                     & ",INKA_L.HOKAN_STR_DATE                                   " & vbNewLine _
                                     & ",INKA_L.SYS_UPD_DATE                                     " & vbNewLine _
                                     & ",INKA_L.SYS_UPD_TIME                                     " & vbNewLine _
                                     & ",MCUST.CUST_CD_L                                         " & vbNewLine _
                                     & ",INKA_S2.OKIBA_COUNT2                                    " & vbNewLine _
                                     & ",INKA_S3.ZAI_REC_NO                                      " & vbNewLine _
                                     & ",ZAI.SYS_UPD_DATE                                        " & vbNewLine _
                                     & ",ZAI.SYS_UPD_TIME                                        " & vbNewLine _
                                     & ",SHOULD_KENPIN_CHK.KBN_GROUP_CD                          " & vbNewLine

    ''' <summary>
    ''' ORDER BY（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_INKA_ZAI As String = "ORDER BY                                     " & vbNewLine _
                                         & " INKA_L.INKA_NO_L                                        " & vbNewLine _
                                         & ",INKA_L.INKA_DATE                                        " & vbNewLine

#End Region

#Region "出荷検索"

    ''' <summary>
    ''' カウント用（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_OUTKA As String = " SELECT COUNT(CNT.SELECT_CNT) AS SELECT_CNT FROM(SELECT COUNT(OUTKA_L.NRS_BR_CD)	            AS SELECT_CNT          " & vbNewLine

    'START YANAI 要望番号433
    '''' <summary>
    '''' SELECT（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_OUTKA As String = " SELECT                                                               " & vbNewLine _
    '                                        & " OUTKA_L.OUTKA_NO_L                                  AS INOUTKA_NO_L        " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_PLAN_DATE                             AS INOUTKA_DATE        " & vbNewLine _
    '                                        & ",OUTKA_L.CUST_ORD_NO                                 AS INOUTKA_ORD_NO      " & vbNewLine _
    '                                        & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
    '                                        & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
    '                                        & ",CASE WHEN OUTKA_L.DEST_KB = '00' THEN MDEST.DEST_NM                        " & vbNewLine _
    '                                        & "      ELSE OUTKA_L.DEST_NM                                                  " & vbNewLine _
    '                                        & " END AS DEST_NM                                                             " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_PKG_NB                                AS PKG_NB              " & vbNewLine _
    '                                        & ",OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
    '                                        & ",''                                                  AS INKA_KENPIN_YN      " & vbNewLine _
    '                                        & ",''                                                  AS INKA_KAKUNIN_YN     " & vbNewLine _
    '                                        & ",SOKO.OUTKA_KENPIN_YN                                AS OUTKA_KENPIN_YN     " & vbNewLine _
    '                                        & ",SOKO.OUTKA_INFO_YN                                  AS OUTKA_INFO_YN       " & vbNewLine _
    '                                        & ",''                                                  AS HOKAN_STR_DATE      " & vbNewLine _
    '                                        & ",OUTKA_L.END_DATE                                    AS END_DATE            " & vbNewLine _
    '                                        & ",OUTKA_L.NIHUDA_FLAG                                 AS NIHUDA_FLAG         " & vbNewLine _
    '                                        & ",''                                                  AS LOC_MANAGER_YN      " & vbNewLine _
    '                                        & ",''                                                  AS OKIBA_COUNT         " & vbNewLine _
    '                                        & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
    '                                        & ",OUTKA_L.SYS_UPD_DATE                                AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",OUTKA_L.SYS_UPD_TIME                                AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",MIN(INKA_L.CHK_INKA_STATE_KB)                       AS CHK_INKA_STATE_KB   " & vbNewLine _
    '                                        & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine
    'START YANAI 要望番号932
    '''' <summary>
    '''' SELECT（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_OUTKA As String = " SELECT                                                               " & vbNewLine _
    '                                            & " OUTKA_L.OUTKA_NO_L                                  AS INOUTKA_NO_L        " & vbNewLine _
    '                                            & ",OUTKA_L.OUTKA_PLAN_DATE                             AS INOUTKA_DATE        " & vbNewLine _
    '                                            & ",OUTKA_L.CUST_ORD_NO                                 AS INOUTKA_ORD_NO      " & vbNewLine _
    '                                            & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
    '                                            & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
    '                                            & ",CASE WHEN OUTKA_L.DEST_KB = '00' THEN MDEST.DEST_NM                        " & vbNewLine _
    '                                            & "      ELSE OUTKA_L.DEST_NM                                                  " & vbNewLine _
    '                                            & " END AS DEST_NM                                                             " & vbNewLine _
    '                                            & ",OUTKA_L.OUTKA_PKG_NB                                AS PKG_NB              " & vbNewLine _
    '                                            & ",OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
    '                                            & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
    '                                            & ",''                                                  AS INKA_KENPIN_YN      " & vbNewLine _
    '                                            & ",''                                                  AS INKA_KAKUNIN_YN     " & vbNewLine _
    '                                            & ",SOKO.OUTKA_KENPIN_YN                                AS OUTKA_KENPIN_YN     " & vbNewLine _
    '                                            & ",SOKO.OUTKA_INFO_YN                                  AS OUTKA_INFO_YN       " & vbNewLine _
    '                                            & ",''                                                  AS HOKAN_STR_DATE      " & vbNewLine _
    '                                            & ",OUTKA_L.END_DATE                                    AS END_DATE            " & vbNewLine _
    '                                            & ",OUTKA_L.NIHUDA_FLAG                                 AS NIHUDA_FLAG         " & vbNewLine _
    '                                            & ",''                                                  AS LOC_MANAGER_YN      " & vbNewLine _
    '                                            & ",''                                                  AS OKIBA_COUNT         " & vbNewLine _
    '                                            & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
    '                                            & ",OUTKA_L.SYS_UPD_DATE                                AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",OUTKA_L.SYS_UPD_TIME                                AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",MIN(INKA_L.CHK_INKA_STATE_KB)                       AS CHK_INKA_STATE_KB   " & vbNewLine _
    '                                            & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
    '                                            & ",''                                                  AS OKIBA_COUNT2        " & vbNewLine _
    ''' <summary>
    ''' SELECT（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_OUTKA As String = " SELECT                                                                   " & vbNewLine _
                                                & " OUTKA_L.OUTKA_NO_L                                  AS INOUTKA_NO_L        " & vbNewLine _
                                                & ",OUTKA_L.OUTKA_PLAN_DATE                             AS INOUTKA_DATE        " & vbNewLine _
                                                & ",OUTKA_L.CUST_ORD_NO                                 AS INOUTKA_ORD_NO      " & vbNewLine _
                                                & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
                                                & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
                                                & ",CASE WHEN OUTKA_L.DEST_KB = '00' THEN MDEST.DEST_NM                        " & vbNewLine _
                                                & "      ELSE OUTKA_L.DEST_NM                                                  " & vbNewLine _
                                                & " END AS DEST_NM                                                             " & vbNewLine _
                                                & ",OUTKA_L.OUTKA_PKG_NB                                AS PKG_NB              " & vbNewLine _
                                                & ",OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
                                                & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
                                                & ",''                                                  AS INKA_KENPIN_YN      " & vbNewLine _
                                                & ",''                                                  AS INKA_KAKUNIN_YN     " & vbNewLine _
                                                & ",SOKO.OUTKA_KENPIN_YN                                AS OUTKA_KENPIN_YN     " & vbNewLine _
                                                & ",SOKO.OUTKA_INFO_YN                                  AS OUTKA_INFO_YN       " & vbNewLine _
                                                & ",''                                                  AS HOKAN_STR_DATE      " & vbNewLine _
                                                & ",OUTKA_L.END_DATE                                    AS END_DATE            " & vbNewLine _
                                                & ",OUTKA_L.NIHUDA_FLAG                                 AS NIHUDA_FLAG         " & vbNewLine _
                                                & ",''                                                  AS LOC_MANAGER_YN      " & vbNewLine _
                                                & ",''                                                  AS OKIBA_COUNT         " & vbNewLine _
                                                & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
                                                & ",OUTKA_L.SYS_UPD_DATE                                AS SYS_UPD_DATE        " & vbNewLine _
                                                & ",OUTKA_L.SYS_UPD_TIME                                AS SYS_UPD_TIME        " & vbNewLine _
                                                & ",MIN(INKA_L.CHK_INKA_STATE_KB)                       AS CHK_INKA_STATE_KB   " & vbNewLine _
                                                & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
                                                & ",''                                                  AS OKIBA_COUNT2        " & vbNewLine _
                                                & "--,ISNULL(OUTKASCNT.SCNT,1)                            AS SCNT                " & vbNewLine _
                                                & "-- 画面.完了種別が '06'(出荷完了) かつ出荷種別区分が '60' (分納) の場合は出荷Sゼロ件を許容する             " & vbNewLine _
                                                & "-- 　⇒OUTKASCNT に該当なし(OUTKASCNT.SCNT IS NULL)  の場合同様に 1を返す                                  " & vbNewLine _
                                                & ",CASE WHEN @KANRYO_SYUBETU /* ←画面.完了種別 */ = '06' /* ←比較用固定値 */ AND OUTKA_L.SYUBETU_KB = '60' " & vbNewLine _
                                                & "    THEN 1                                                                                                 " & vbNewLine _
                                                & "    ELSE ISNULL(OUTKASCNT.SCNT,1)                                                                          " & vbNewLine _
                                                & " END AS SCNT                                                                                               " & vbNewLine _
                                                & ""
    'END YANAI 要望番号932
    'END YANAI 要望番号433

    'START YANAI 要望番号824
    '''' <summary>
    '''' FROM（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_OUTKA As String = "FROM                                             " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ",SUM(OUTKA_M.BACKLOG_NB) AS SUM_BACKLOG_NB          " & vbNewLine _
    '                                    & "FROM                                                " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_L OUTKA_L2                        " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_M OUTKA_M                         " & vbNewLine _
    '                                    & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L2.NRS_BR_CD       " & vbNewLine _
    '                                    & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L2.OUTKA_NO_L     " & vbNewLine _
    '                                    & "AND    OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "AND    OUTKA_L2.SYS_DEL_FLG = '0'                   " & vbNewLine _
    '                                    & "GROUP BY                                            " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ") OUTKA_L                                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_CUST    MCUST                       " & vbNewLine _
    '                                     & "ON     MCUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_M = OUTKA_L.CUST_CD_M                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
    '                                     & "AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN                                                 " & vbNewLine _
    '                                     & "(SELECT                                                   " & vbNewLine _
    '                                     & " MTCUST2.CUST_CD_L AS CUST_CD_L                           " & vbNewLine _
    '                                     & ",MTCUST2.CUST_CD_M AS CUST_CD_M                           " & vbNewLine _
    '                                     & ",MIN(MTCUST2.USER_CD) AS USER_CD                          " & vbNewLine _
    '                                     & ",MIN(MTCUST2.USER_CD_EDA) AS USER_CD_EDA                  " & vbNewLine _
    '                                     & " FROM                                                     " & vbNewLine _
    '                                     & " $LM_MST$..M_TCUST MTCUST2                                " & vbNewLine _
    '                                     & " GROUP BY                                                 " & vbNewLine _
    '                                     & " MTCUST2.CUST_CD_L                                        " & vbNewLine _
    '                                     & ",MTCUST2.CUST_CD_M                                        " & vbNewLine _
    '                                     & ") MTCUST                                                  " & vbNewLine _
    '                                     & "ON     MTCUST.CUST_CD_L = OUTKA_L.CUST_CD_L               " & vbNewLine _
    '                                     & "AND    MTCUST.CUST_CD_M = OUTKA_L.CUST_CD_M               " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
    '                                     & "ON     SUSER.USER_CD = MTCUST.USER_CD                     " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
    '                                     & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD                 " & vbNewLine _
    '                                     & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                         " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_DEST MDEST                          " & vbNewLine _
    '                                     & "ON     MDEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    MDEST.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
    '                                     & "AND    MDEST.DEST_CD = OUTKA_L.DEST_CD                    " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN                                                 " & vbNewLine _
    '                                     & "(SELECT                                                   " & vbNewLine _
    '                                     & "OUTKA_S.OUTKA_NO_L                                        " & vbNewLine _
    '                                     & ",INKA_L.INKA_STATE_KB AS CHK_INKA_STATE_KB                " & vbNewLine _
    '                                     & "FROM  $LM_TRN$..B_INKA_L INKA_L                           " & vbNewLine _
    '                                     & "INNER JOIN $LM_TRN$..C_OUTKA_S OUTKA_S                    " & vbNewLine _
    '                                     & "ON OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L                   " & vbNewLine _
    '                                     & "AND OUTKA_S.SYS_DEL_FLG = '0'                             " & vbNewLine _
    '                                     & "AND OUTKA_S.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
    '                                     & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & " ) INKA_L                                                 " & vbNewLine _
    '                                     & "ON INKA_L.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L                 " & vbNewLine
    'START YANAI 要望番号932
    '''' <summary>
    '''' FROM（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_OUTKA As String = "FROM                                             " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ",SUM(OUTKA_M.BACKLOG_NB) AS SUM_BACKLOG_NB          " & vbNewLine _
    '                                    & "FROM                                                " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_L OUTKA_L2                        " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_M OUTKA_M                         " & vbNewLine _
    '                                    & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L2.NRS_BR_CD       " & vbNewLine _
    '                                    & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L2.OUTKA_NO_L     " & vbNewLine _
    '                                    & "AND    OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "AND    OUTKA_L2.SYS_DEL_FLG = '0'                   " & vbNewLine _
    '                                    & "GROUP BY                                            " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ") OUTKA_L                                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_CUST    MCUST                       " & vbNewLine _
    '                                     & "ON     MCUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_M = OUTKA_L.CUST_CD_M                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
    '                                     & "AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
    '                                     & "ON     SUSER.USER_CD = MCUST.TANTO_CD                     " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
    '                                     & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD                 " & vbNewLine _
    '                                     & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                         " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_DEST MDEST                          " & vbNewLine _
    '                                     & "ON     MDEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    MDEST.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
    '                                     & "AND    MDEST.DEST_CD = OUTKA_L.DEST_CD                    " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN                                                 " & vbNewLine _
    '                                     & "(SELECT                                                   " & vbNewLine _
    '                                     & "OUTKA_S.OUTKA_NO_L                                        " & vbNewLine _
    '                                     & ",INKA_L.INKA_STATE_KB AS CHK_INKA_STATE_KB                " & vbNewLine _
    '                                     & "FROM  $LM_TRN$..B_INKA_L INKA_L                           " & vbNewLine _
    '                                     & "INNER JOIN $LM_TRN$..C_OUTKA_S OUTKA_S                    " & vbNewLine _
    '                                     & "ON OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L                   " & vbNewLine _
    '                                     & "AND OUTKA_S.SYS_DEL_FLG = '0'                             " & vbNewLine _
    '                                     & "AND OUTKA_S.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
    '                                     & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & " ) INKA_L                                                 " & vbNewLine _
    '                                     & "ON INKA_L.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L                 " & vbNewLine
    'START YANAI 要望番号1295 完了画面・検索速度改善
    '''' <summary>
    '''' FROM（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_OUTKA As String = "FROM                                      " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ",SUM(OUTKA_M.BACKLOG_NB) AS SUM_BACKLOG_NB          " & vbNewLine _
    '                                    & "FROM                                                " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_L OUTKA_L2                        " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_M OUTKA_M                         " & vbNewLine _
    '                                    & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L2.NRS_BR_CD       " & vbNewLine _
    '                                    & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L2.OUTKA_NO_L     " & vbNewLine _
    '                                    & "AND    OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "AND    OUTKA_L2.SYS_DEL_FLG = '0'                   " & vbNewLine _
    '                                    & "GROUP BY                                            " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ") OUTKA_L                                           " & vbNewLine _
    '                                    & "LEFT JOIN $LM_MST$..M_CUST    MCUST                 " & vbNewLine _
    '                                    & "ON     MCUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD          " & vbNewLine _
    '                                    & "AND    MCUST.CUST_CD_L = OUTKA_L.CUST_CD_L          " & vbNewLine _
    '                                    & "AND    MCUST.CUST_CD_M = OUTKA_L.CUST_CD_M          " & vbNewLine _
    '                                    & "AND    MCUST.CUST_CD_S = '00'                       " & vbNewLine _
    '                                    & "AND    MCUST.CUST_CD_SS = '00'                      " & vbNewLine _
    '                                    & "AND    MCUST.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
    '                                    & "AND    OUTKA_L.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "LEFT JOIN $LM_MST$..S_USER SUSER                    " & vbNewLine _
    '                                    & "ON     SUSER.USER_CD = MCUST.TANTO_CD               " & vbNewLine _
    '                                    & "LEFT JOIN $LM_MST$..M_SOKO SOKO                     " & vbNewLine _
    '                                    & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD           " & vbNewLine _
    '                                    & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                   " & vbNewLine _
    '                                    & "LEFT JOIN $LM_MST$..M_DEST MDEST                    " & vbNewLine _
    '                                    & "ON     MDEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD          " & vbNewLine _
    '                                    & "AND    MDEST.CUST_CD_L = OUTKA_L.CUST_CD_L          " & vbNewLine _
    '                                    & "AND    MDEST.DEST_CD = OUTKA_L.DEST_CD              " & vbNewLine _
    '                                    & "AND    OUTKA_L.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & "OUTKA_S.OUTKA_NO_L                                  " & vbNewLine _
    '                                    & ",INKA_L.INKA_STATE_KB AS CHK_INKA_STATE_KB          " & vbNewLine _
    '                                    & "FROM  $LM_TRN$..B_INKA_L INKA_L                     " & vbNewLine _
    '                                    & "INNER JOIN $LM_TRN$..C_OUTKA_S OUTKA_S              " & vbNewLine _
    '                                    & "ON OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L             " & vbNewLine _
    '                                    & "AND OUTKA_S.SYS_DEL_FLG = '0'                       " & vbNewLine _
    '                                    & "AND OUTKA_S.NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
    '                                    & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
    '                                    & " ) INKA_L                                           " & vbNewLine _
    '                                    & "ON INKA_L.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L           " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & " OUTKAM.NRS_BR_CD                                   " & vbNewLine _
    '                                    & " ,OUTKAM.OUTKA_NO_L                                 " & vbNewLine _
    '                                    & " ,OUTKAM.OUTKA_NO_M                                 " & vbNewLine _
    '                                    & " ,COUNT(OUTKAS.NRS_BR_CD) AS SCNT                   " & vbNewLine _
    '                                    & " FROM $LM_TRN$..C_OUTKA_M OUTKAM                    " & vbNewLine _
    '                                    & " LEFT JOIN                                          " & vbNewLine _
    '                                    & " $LM_TRN$..C_OUTKA_L OUTKAL                         " & vbNewLine _
    '                                    & " ON                                                 " & vbNewLine _
    '                                    & " OUTKAL.NRS_BR_CD = OUTKAM.NRS_BR_CD                " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAL.OUTKA_NO_L = OUTKAM.OUTKA_NO_L              " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAL.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                    & " LEFT JOIN                                          " & vbNewLine _
    '                                    & " $LM_TRN$..C_OUTKA_S OUTKAS                         " & vbNewLine _
    '                                    & " ON                                                 " & vbNewLine _
    '                                    & " OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L              " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M              " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAS.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                    & " WHERE                                              " & vbNewLine _
    '                                    & " OUTKAM.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                    & " GROUP BY                                           " & vbNewLine _
    '                                    & " OUTKAM.NRS_BR_CD                                   " & vbNewLine _
    '                                    & " ,OUTKAM.OUTKA_NO_L                                 " & vbNewLine _
    '                                    & " ,OUTKAM.OUTKA_NO_M                                 " & vbNewLine _
    '                                    & " ) OUTKASCNT                                        " & vbNewLine _
    '                                    & " ON                                                 " & vbNewLine _
    '                                    & " OUTKASCNT.NRS_BR_CD = OUTKA_L.NRS_BR_CD            " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKASCNT.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L          " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKASCNT.SCNT = '0'                               " & vbNewLine _
    ''' <summary>
    ''' FROM（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKA As String = "FROM                                      " & vbNewLine _
                                        & "(SELECT                                             " & vbNewLine _
                                        & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
                                        & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
                                        & ",OUTKA_L2.SYUBETU_KB                                " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
                                        & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
                                        & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
                                        & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
                                        & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
                                        & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
                                        & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
                                        & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
                                        & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
                                        & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
                                        & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
                                        & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
                                        & ",SUM(OUTKA_M.BACKLOG_NB) AS SUM_BACKLOG_NB          " & vbNewLine _
                                        & "FROM                                                " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L OUTKA_L2                        " & vbNewLine _
                                        & "LEFT JOIN                                           " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_M OUTKA_M                         " & vbNewLine _
                                        & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L2.NRS_BR_CD       " & vbNewLine _
                                        & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L2.OUTKA_NO_L     " & vbNewLine _
                                        & "AND    OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine

    ''' <summary>
    ''' FROM（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKA2 As String = "GROUP BY                                 " & vbNewLine _
                                        & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
                                        & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
                                        & ",OUTKA_L2.SYUBETU_KB                                " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
                                        & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
                                        & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
                                        & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
                                        & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
                                        & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
                                        & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
                                        & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
                                        & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
                                        & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
                                        & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
                                        & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
                                        & ") OUTKA_L                                           " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_CUST    MCUST                 " & vbNewLine _
                                        & "ON     MCUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD          " & vbNewLine _
                                        & "AND    MCUST.CUST_CD_L = OUTKA_L.CUST_CD_L          " & vbNewLine _
                                        & "AND    MCUST.CUST_CD_M = OUTKA_L.CUST_CD_M          " & vbNewLine _
                                        & "AND    MCUST.CUST_CD_S = '00'                       " & vbNewLine _
                                        & "AND    MCUST.CUST_CD_SS = '00'                      " & vbNewLine _
                                        & "AND    OUTKA_L.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..S_USER SUSER                    " & vbNewLine _
                                        & "ON     SUSER.USER_CD = MCUST.TANTO_CD               " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_SOKO SOKO                     " & vbNewLine _
                                        & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD           " & vbNewLine _
                                        & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                   " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_DEST MDEST                    " & vbNewLine _
                                        & "ON     MDEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD          " & vbNewLine _
                                        & "AND    MDEST.CUST_CD_L = OUTKA_L.CUST_CD_L          " & vbNewLine _
                                        & "AND    MDEST.DEST_CD = OUTKA_L.DEST_CD              " & vbNewLine _
                                        & "AND    OUTKA_L.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                        & "LEFT JOIN                                           " & vbNewLine _
                                        & "(SELECT                                             " & vbNewLine _
                                        & "OUTKA_S.OUTKA_NO_L                                  " & vbNewLine _
                                        & ",INKA_L.INKA_STATE_KB AS CHK_INKA_STATE_KB          " & vbNewLine _
                                        & "FROM  $LM_TRN$..B_INKA_L INKA_L                     " & vbNewLine _
                                        & "INNER JOIN $LM_TRN$..C_OUTKA_S OUTKA_S              " & vbNewLine _
                                        & "ON OUTKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD             " & vbNewLine _
                                        & "AND OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L            " & vbNewLine _
                                        & "AND OUTKA_S.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                        & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                        & " ) INKA_L                                           " & vbNewLine _
                                        & "ON INKA_L.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L           " & vbNewLine _
                                        & "LEFT JOIN                                           " & vbNewLine _
                                        & "(SELECT                                             " & vbNewLine _
                                        & " OUTKAM.NRS_BR_CD                                   " & vbNewLine _
                                        & " ,OUTKAM.OUTKA_NO_L                                 " & vbNewLine _
                                        & " ,OUTKAM.OUTKA_NO_M                                 " & vbNewLine _
                                        & " ,COUNT(OUTKAS.NRS_BR_CD) AS SCNT                   " & vbNewLine _
                                        & " FROM $LM_TRN$..C_OUTKA_M OUTKAM                    " & vbNewLine _
                                        & " LEFT JOIN                                          " & vbNewLine _
                                        & " $LM_TRN$..C_OUTKA_L OUTKAL                         " & vbNewLine _
                                        & " ON                                                 " & vbNewLine _
                                        & " OUTKAL.NRS_BR_CD = OUTKAM.NRS_BR_CD                " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAL.OUTKA_NO_L = OUTKAM.OUTKA_NO_L              " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAL.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                        & " LEFT JOIN                                          " & vbNewLine _
                                        & " $LM_TRN$..C_OUTKA_S OUTKAS                         " & vbNewLine _
                                        & " ON                                                 " & vbNewLine _
                                        & " OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L              " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M              " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAS.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                        & " WHERE                                              " & vbNewLine _
                                        & " OUTKAM.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                        & " GROUP BY                                           " & vbNewLine _
                                        & " OUTKAM.NRS_BR_CD                                   " & vbNewLine _
                                        & " ,OUTKAM.OUTKA_NO_L                                 " & vbNewLine _
                                        & " ,OUTKAM.OUTKA_NO_M                                 " & vbNewLine _
                                        & " ) OUTKASCNT                                        " & vbNewLine _
                                        & " ON                                                 " & vbNewLine _
                                        & " OUTKASCNT.NRS_BR_CD = OUTKA_L.NRS_BR_CD            " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKASCNT.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L          " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKASCNT.SCNT = '0'                               " & vbNewLine _
    'END YANAI 要望番号1295 完了画面・検索速度改善
    'END YANAI 要望番号932
    'END YANAI 要望番号824

    'START YANAI 要望番号932
    '''' <summary>
    '''' GROUP BY（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_GROUP_BY_OUTKA As String = "GROUP BY                                    " & vbNewLine _
    '                                     & " OUTKA_L.OUTKA_NO_L                                      " & vbNewLine _
    '                                     & ",OUTKA_L.OUTKA_PLAN_DATE                                 " & vbNewLine _
    '                                     & ",OUTKA_L.CUST_ORD_NO                                     " & vbNewLine _
    '                                     & ",SUSER.USER_NM                                           " & vbNewLine _
    '                                     & ",MCUST.CUST_NM_L                                         " & vbNewLine _
    '                                     & ",MCUST.CUST_NM_M                                         " & vbNewLine _
    '                                     & ",MDEST.DEST_NM                                           " & vbNewLine _
    '                                     & ",OUTKA_L.OUTKA_PKG_NB                                    " & vbNewLine _
    '                                     & ",OUTKA_L.NRS_BR_CD                                       " & vbNewLine _
    '                                     & ",SOKO.OUTKA_KENPIN_YN                                    " & vbNewLine _
    '                                     & ",SOKO.OUTKA_INFO_YN                                      " & vbNewLine _
    '                                     & ",OUTKA_L.END_DATE                                        " & vbNewLine _
    '                                     & ",OUTKA_L.NIHUDA_FLAG                                     " & vbNewLine _
    '                                     & ",OUTKA_L.SYS_UPD_DATE                                    " & vbNewLine _
    '                                     & ",OUTKA_L.SYS_UPD_TIME                                    " & vbNewLine _
    '                                     & ",MCUST.CUST_CD_L                                         " & vbNewLine _
    '                                     & ",OUTKA_L.DEST_KB                                         " & vbNewLine _
    '                                     & ",OUTKA_L.DEST_NM                                         " & vbNewLine
    ''' <summary>
    ''' GROUP BY（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_OUTKA As String = "GROUP BY                                    " & vbNewLine _
                                         & " OUTKA_L.OUTKA_NO_L                                      " & vbNewLine _
                                         & ",OUTKA_L.OUTKA_PLAN_DATE                                 " & vbNewLine _
                                         & ",OUTKA_L.CUST_ORD_NO                                     " & vbNewLine _
                                         & ",SUSER.USER_NM                                           " & vbNewLine _
                                         & ",MCUST.CUST_NM_L                                         " & vbNewLine _
                                         & ",MCUST.CUST_NM_M                                         " & vbNewLine _
                                         & ",MDEST.DEST_NM                                           " & vbNewLine _
                                         & ",OUTKA_L.OUTKA_PKG_NB                                    " & vbNewLine _
                                         & ",OUTKA_L.NRS_BR_CD                                       " & vbNewLine _
                                         & ",SOKO.OUTKA_KENPIN_YN                                    " & vbNewLine _
                                         & ",SOKO.OUTKA_INFO_YN                                      " & vbNewLine _
                                         & ",OUTKA_L.END_DATE                                        " & vbNewLine _
                                         & ",OUTKA_L.NIHUDA_FLAG                                     " & vbNewLine _
                                         & ",OUTKA_L.SYS_UPD_DATE                                    " & vbNewLine _
                                         & ",OUTKA_L.SYS_UPD_TIME                                    " & vbNewLine _
                                         & ",MCUST.CUST_CD_L                                         " & vbNewLine _
                                         & ",OUTKA_L.DEST_KB                                         " & vbNewLine _
                                         & ",OUTKA_L.DEST_NM                                         " & vbNewLine _
                                         & ",OUTKA_L.SYUBETU_KB                                      " & vbNewLine _
                                         & ",OUTKASCNT.SCNT                                          " & vbNewLine
    'END YANAI 要望番号932

    ''' <summary>
    ''' ORDER BY（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_OUTKA As String = "ORDER BY                                    " & vbNewLine _
                                         & " OUTKA_L.OUTKA_NO_L                                      " & vbNewLine _
                                         & ",OUTKA_L.OUTKA_PLAN_DATE                                 " & vbNewLine

    ''' <summary>
    ''' COUNT時使用（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_END_OUTKA As String = ") CNT                                          "

    'START YANAI 20110906 サンプル対応
    '''' <summary>
    '''' SELECT（出荷検索(出荷完了時)）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_OUTKAKANRYO As String = " SELECT                                                         " & vbNewLine _
    '                                        & " OUTKA_S.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_NO_L                                  AS OUTKA_NO_L          " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_NO_M                                  AS OUTKA_NO_M          " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_NO_S                                  AS OUTKA_NO_S          " & vbNewLine _
    '                                        & ",OUTKA_S.ZAI_REC_NO                                  AS ZAI_REC_NO          " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_NB                                    AS ALCTD_NB            " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_QT                                    AS ALCTD_QT            " & vbNewLine _
    ''' <summary>
    ''' SELECT（出荷検索(出荷完了時)）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_OUTKAKANRYO As String = " SELECT                                                         " & vbNewLine _
                                            & " OUTKA_S.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
                                            & ",OUTKA_S.OUTKA_NO_L                                  AS OUTKA_NO_L          " & vbNewLine _
                                            & ",OUTKA_S.OUTKA_NO_M                                  AS OUTKA_NO_M          " & vbNewLine _
                                            & ",OUTKA_S.OUTKA_NO_S                                  AS OUTKA_NO_S          " & vbNewLine _
                                            & ",OUTKA_S.ZAI_REC_NO                                  AS ZAI_REC_NO          " & vbNewLine _
                                            & ",OUTKA_S.ALCTD_NB                                    AS ALCTD_NB            " & vbNewLine _
                                            & ",OUTKA_S.ALCTD_QT                                    AS ALCTD_QT            " & vbNewLine _
                                            & ",OUTKA_M.ALCTD_KB                                    AS ALCTD_KB            " & vbNewLine
    'END YANAI 20110906 サンプル対応

    'START YANAI 20110906 サンプル対応
    '''' <summary>
    '''' FROM（出荷検索(出荷完了時)）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_OUTKAKANRYO As String = "FROM                                       " & vbNewLine _
    '                                     & "$LM_TRN$..C_OUTKA_S OUTKA_S                               " & vbNewLine _
    '                                     & "WHERE  OUTKA_S.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
    '                                     & "AND    OUTKA_S.SYS_DEL_FLG = '0'                          " & vbNewLine
    ''' <summary>
    ''' FROM（出荷検索(出荷完了時)）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKAKANRYO As String = "FROM                                       " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_S OUTKA_S                               " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKA_M                     " & vbNewLine _
                                         & "ON     OUTKA_M.NRS_BR_CD = OUTKA_S.NRS_BR_CD              " & vbNewLine _
                                         & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_S.OUTKA_NO_L            " & vbNewLine _
                                         & "AND    OUTKA_M.OUTKA_NO_M = OUTKA_S.OUTKA_NO_M            " & vbNewLine _
                                         & "AND    OUTKA_M.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                         & "WHERE  OUTKA_S.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                         & "AND    OUTKA_S.SYS_DEL_FLG = '0'                          " & vbNewLine
    'END YANAI 20110906 サンプル対応

    'START YANAI 20110913 小分け対応
    '''' <summary>
    '''' SELECT（在庫検索(出荷完了時)）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_ZAIKANRYO As String = " SELECT                                                           " & vbNewLine _
    '                                        & " ZAITRS.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",ZAITRS.ZAI_REC_NO                                   AS ZAI_REC_NO          " & vbNewLine _
    '                                        & ",ZAITRS.PORA_ZAI_NB                                  AS PORA_ZAI_NB         " & vbNewLine _
    '                                        & ",ZAITRS.ALCTD_NB                                     AS ALCTD_NB            " & vbNewLine _
    '                                        & ",ZAITRS.PORA_ZAI_QT                                  AS PORA_ZAI_QT         " & vbNewLine _
    '                                        & ",ZAITRS.ALCTD_QT                                     AS ALCTD_QT            " & vbNewLine
    ''' <summary>
    ''' SELECT（在庫検索(出荷完了時)）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_ZAIKANRYO As String = " SELECT                                                           " & vbNewLine _
                                            & " ZAITRS.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
                                            & ",ZAITRS.ZAI_REC_NO                                   AS ZAI_REC_NO          " & vbNewLine _
                                            & ",ZAITRS.PORA_ZAI_NB                                  AS PORA_ZAI_NB         " & vbNewLine _
                                            & ",ZAITRS.ALCTD_NB                                     AS ALCTD_NB            " & vbNewLine _
                                            & ",ZAITRS.PORA_ZAI_QT                                  AS PORA_ZAI_QT         " & vbNewLine _
                                            & ",ZAITRS.ALCTD_QT                                     AS ALCTD_QT            " & vbNewLine _
                                            & ",ZAITRS.IRIME                                        AS IRIME               " & vbNewLine
    'END YANAI 20110913 小分け対応

    ''' <summary>
    ''' FROM（在庫検索(出荷完了時)）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ZAIKANRYO As String = "FROM                                         " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS ZAITRS                                " & vbNewLine _
                                         & "WHERE  ZAITRS.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                         & "AND    ZAITRS.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "AND ZAI_REC_NO        IN                                  " & vbNewLine _
                                         & "(                                                         " & vbNewLine _
                                         & "SELECT OUTKA_S.ZAI_REC_NO                                 " & vbNewLine _
                                         & "FROM $LM_TRN$..C_OUTKA_S OUTKA_S                          " & vbNewLine _
                                         & "WHERE                                                     " & vbNewLine _
                                         & "OUTKA_S.NRS_BR_CD       = @NRS_BR_CD                      " & vbNewLine

    ''' <summary>
    ''' FROM（在庫検索(出荷完了時)）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ZAIKANRYO2 As String = "                                            " & vbNewLine _
                                         & "AND OUTKA_S.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                         & ")                                                         " & vbNewLine

    'START YANAI 要望番号653
    'START YANAI 要望番号932
    '''' <summary>
    '''' SELECT（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_OUTKA_ZAI As String = " SELECT                                                               " & vbNewLine _
    '                                        & " OUTKA_L.OUTKA_NO_L                                  AS INOUTKA_NO_L        " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_PLAN_DATE                             AS INOUTKA_DATE        " & vbNewLine _
    '                                        & ",OUTKA_L.CUST_ORD_NO                                 AS INOUTKA_ORD_NO      " & vbNewLine _
    '                                        & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
    '                                        & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
    '                                        & ",CASE WHEN OUTKA_L.DEST_KB = '00' THEN MDEST.DEST_NM                        " & vbNewLine _
    '                                        & "      ELSE OUTKA_L.DEST_NM                                                  " & vbNewLine _
    '                                        & " END AS DEST_NM                                                             " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_PKG_NB                                AS PKG_NB              " & vbNewLine _
    '                                        & ",OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
    '                                        & ",''                                                  AS INKA_KENPIN_YN      " & vbNewLine _
    '                                        & ",''                                                  AS INKA_KAKUNIN_YN     " & vbNewLine _
    '                                        & ",SOKO.OUTKA_KENPIN_YN                                AS OUTKA_KENPIN_YN     " & vbNewLine _
    '                                        & ",SOKO.OUTKA_INFO_YN                                  AS OUTKA_INFO_YN       " & vbNewLine _
    '                                        & ",''                                                  AS HOKAN_STR_DATE      " & vbNewLine _
    '                                        & ",OUTKA_L.END_DATE                                    AS END_DATE            " & vbNewLine _
    '                                        & ",OUTKA_L.NIHUDA_FLAG                                 AS NIHUDA_FLAG         " & vbNewLine _
    '                                        & ",''                                                  AS LOC_MANAGER_YN      " & vbNewLine _
    '                                        & ",''                                                  AS OKIBA_COUNT         " & vbNewLine _
    '                                        & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
    '                                        & ",OUTKA_L.SYS_UPD_DATE                                AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",OUTKA_L.SYS_UPD_TIME                                AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",MIN(INKA_L.CHK_INKA_STATE_KB)                       AS CHK_INKA_STATE_KB   " & vbNewLine _
    '                                        & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
    '                                        & ",''                                                  AS OKIBA_COUNT2        " & vbNewLine _
    '                                        & ",OUTKA_S.ZAI_REC_NO                                     AS ZAI_REC_NO           " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_DATE                                     AS ZAI_SYS_UPD_DATE           " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_TIME                                     AS ZAI_SYS_UPD_TIME           " & vbNewLine
    ''' <summary>
    ''' SELECT（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_OUTKA_ZAI As String = " SELECT                                                           " & vbNewLine _
                                            & " OUTKA_L.OUTKA_NO_L                                  AS INOUTKA_NO_L        " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_PLAN_DATE                             AS INOUTKA_DATE        " & vbNewLine _
                                            & ",OUTKA_L.CUST_ORD_NO                                 AS INOUTKA_ORD_NO      " & vbNewLine _
                                            & ",SUSER.USER_NM                                       AS TANTO_USER          " & vbNewLine _
                                            & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M            AS CUST_NM	           " & vbNewLine _
                                            & ",CASE WHEN OUTKA_L.DEST_KB = '00' THEN MDEST.DEST_NM                        " & vbNewLine _
                                            & "      ELSE OUTKA_L.DEST_NM                                                  " & vbNewLine _
                                            & " END AS DEST_NM                                                             " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_PKG_NB                                AS PKG_NB              " & vbNewLine _
                                            & ",OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
                                            & ",''                                                  AS INOUTKA_STATE_KB    " & vbNewLine _
                                            & ",''                                                  AS INKA_KENPIN_YN      " & vbNewLine _
                                            & ",''                                                  AS INKA_KAKUNIN_YN     " & vbNewLine _
                                            & ",SOKO.OUTKA_KENPIN_YN                                AS OUTKA_KENPIN_YN     " & vbNewLine _
                                            & ",SOKO.OUTKA_INFO_YN                                  AS OUTKA_INFO_YN       " & vbNewLine _
                                            & ",''                                                  AS HOKAN_STR_DATE      " & vbNewLine _
                                            & ",OUTKA_L.END_DATE                                    AS END_DATE            " & vbNewLine _
                                            & ",OUTKA_L.NIHUDA_FLAG                                 AS NIHUDA_FLAG         " & vbNewLine _
                                            & ",''                                                  AS LOC_MANAGER_YN      " & vbNewLine _
                                            & ",''                                                  AS OKIBA_COUNT         " & vbNewLine _
                                            & ",'0'                                                 AS SYORI_FLG           " & vbNewLine _
                                            & ",OUTKA_L.SYS_UPD_DATE                                AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",OUTKA_L.SYS_UPD_TIME                                AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",'' AS CHK_INKA_STATE_KB --MIN(INKA_L.CHK_INKA_STATE_KB) AS CHK_INKA_STATE_KB   " & vbNewLine _
                                            & ",MCUST.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
                                            & ",''                                                  AS OKIBA_COUNT2        " & vbNewLine _
                                            & ",OUTKA_S.ZAI_REC_NO                                  AS ZAI_REC_NO          " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                                    AS ZAI_SYS_UPD_DATE    " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                                    AS ZAI_SYS_UPD_TIME    " & vbNewLine _
                                            & "--,ISNULL(OUTKASCNT.SCNT,1)                            AS SCNT                " & vbNewLine _
                                            & "-- 画面.完了種別が '06'(出荷完了) かつ出荷種別区分が '60' (分納) の場合は出荷Sゼロ件を許容する             " & vbNewLine _
                                            & "-- 　⇒OUTKASCNT に該当なし(OUTKASCNT.SCNT IS NULL)  の場合同様に 1を返す                                  " & vbNewLine _
                                            & ",CASE WHEN @KANRYO_SYUBETU /* ←画面.完了種別 */ = '06' /* ←比較用固定値 */ AND OUTKA_L.SYUBETU_KB = '60' " & vbNewLine _
                                            & "    THEN 1                                                                                                 " & vbNewLine _
                                            & "    ELSE ISNULL(OUTKASCNT.SCNT,1)                                                                          " & vbNewLine _
                                            & " END AS SCNT                                                                                               " & vbNewLine _
                                            & ""
    'END YANAI 要望番号932
    'START YANAI 要望番号824
    '''' <summary>
    '''' FROM（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_OUTKA_ZAI As String = "FROM                                             " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ",SUM(OUTKA_M.BACKLOG_NB) AS SUM_BACKLOG_NB          " & vbNewLine _
    '                                    & "FROM                                                " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_L OUTKA_L2                        " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_M OUTKA_M                         " & vbNewLine _
    '                                    & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L2.NRS_BR_CD       " & vbNewLine _
    '                                    & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L2.OUTKA_NO_L     " & vbNewLine _
    '                                    & "AND    OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "AND    OUTKA_L2.SYS_DEL_FLG = '0'                   " & vbNewLine _
    '                                    & "GROUP BY                                            " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ") OUTKA_L                                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_CUST    MCUST                       " & vbNewLine _
    '                                     & "ON     MCUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_M = OUTKA_L.CUST_CD_M                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
    '                                     & "AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN                                                 " & vbNewLine _
    '                                     & "(SELECT                                                   " & vbNewLine _
    '                                     & " MTCUST2.CUST_CD_L AS CUST_CD_L                           " & vbNewLine _
    '                                     & ",MTCUST2.CUST_CD_M AS CUST_CD_M                           " & vbNewLine _
    '                                     & ",MIN(MTCUST2.USER_CD) AS USER_CD                          " & vbNewLine _
    '                                     & ",MIN(MTCUST2.USER_CD_EDA) AS USER_CD_EDA                  " & vbNewLine _
    '                                     & " FROM                                                     " & vbNewLine _
    '                                     & " $LM_MST$..M_TCUST MTCUST2                                " & vbNewLine _
    '                                     & " GROUP BY                                                 " & vbNewLine _
    '                                     & " MTCUST2.CUST_CD_L                                        " & vbNewLine _
    '                                     & ",MTCUST2.CUST_CD_M                                        " & vbNewLine _
    '                                     & ") MTCUST                                                  " & vbNewLine _
    '                                     & "ON     MTCUST.CUST_CD_L = OUTKA_L.CUST_CD_L               " & vbNewLine _
    '                                     & "AND    MTCUST.CUST_CD_M = OUTKA_L.CUST_CD_M               " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
    '                                     & "ON     SUSER.USER_CD = MTCUST.USER_CD                     " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
    '                                     & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD                 " & vbNewLine _
    '                                     & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                         " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_DEST MDEST                          " & vbNewLine _
    '                                     & "ON     MDEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    MDEST.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
    '                                     & "AND    MDEST.DEST_CD = OUTKA_L.DEST_CD                    " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN                                                 " & vbNewLine _
    '                                     & "(SELECT                                                   " & vbNewLine _
    '                                     & "OUTKA_S.OUTKA_NO_L                                        " & vbNewLine _
    '                                     & ",INKA_L.INKA_STATE_KB AS CHK_INKA_STATE_KB                " & vbNewLine _
    '                                     & "FROM  $LM_TRN$..B_INKA_L INKA_L                           " & vbNewLine _
    '                                     & "INNER JOIN $LM_TRN$..C_OUTKA_S OUTKA_S                    " & vbNewLine _
    '                                     & "ON OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L                   " & vbNewLine _
    '                                     & "AND OUTKA_S.SYS_DEL_FLG = '0'                             " & vbNewLine _
    '                                     & "AND OUTKA_S.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
    '                                     & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & " ) INKA_L                                                 " & vbNewLine _
    '                                     & "ON INKA_L.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L                 " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_S    OUTKA_S                  " & vbNewLine _
    '                                     & "ON     OUTKA_S.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    OUTKA_S.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L                " & vbNewLine _
    '                                     & "AND    OUTKA_S.SYS_DEL_FLG = '0'                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..D_ZAI_TRS    ZAI                  " & vbNewLine _
    '                                     & "ON     ZAI.NRS_BR_CD = OUTKA_S.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    ZAI.ZAI_REC_NO = OUTKA_S.ZAI_REC_NO                " & vbNewLine _
    '                                     & "AND    ZAI.SYS_DEL_FLG = '0'                " & vbNewLine
    'START YANAI 要望番号932
    '''' <summary>
    '''' FROM（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_OUTKA_ZAI As String = "FROM                                             " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ",SUM(OUTKA_M.BACKLOG_NB) AS SUM_BACKLOG_NB          " & vbNewLine _
    '                                    & "FROM                                                " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_L OUTKA_L2                        " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_M OUTKA_M                         " & vbNewLine _
    '                                    & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L2.NRS_BR_CD       " & vbNewLine _
    '                                    & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L2.OUTKA_NO_L     " & vbNewLine _
    '                                    & "AND    OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "AND    OUTKA_L2.SYS_DEL_FLG = '0'                   " & vbNewLine _
    '                                    & "GROUP BY                                            " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ") OUTKA_L                                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_CUST    MCUST                       " & vbNewLine _
    '                                     & "ON     MCUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_M = OUTKA_L.CUST_CD_M                " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_S = '00'                             " & vbNewLine _
    '                                     & "AND    MCUST.CUST_CD_SS = '00'                            " & vbNewLine _
    '                                     & "AND    MCUST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..S_USER SUSER                          " & vbNewLine _
    '                                     & "ON     SUSER.USER_CD = MCUST.TANTO_CD                     " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
    '                                     & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD                 " & vbNewLine _
    '                                     & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                         " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_DEST MDEST                          " & vbNewLine _
    '                                     & "ON     MDEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    MDEST.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
    '                                     & "AND    MDEST.DEST_CD = OUTKA_L.DEST_CD                    " & vbNewLine _
    '                                     & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "LEFT JOIN                                                 " & vbNewLine _
    '                                     & "(SELECT                                                   " & vbNewLine _
    '                                     & "OUTKA_S.OUTKA_NO_L                                        " & vbNewLine _
    '                                     & ",INKA_L.INKA_STATE_KB AS CHK_INKA_STATE_KB                " & vbNewLine _
    '                                     & "FROM  $LM_TRN$..B_INKA_L INKA_L                           " & vbNewLine _
    '                                     & "INNER JOIN $LM_TRN$..C_OUTKA_S OUTKA_S                    " & vbNewLine _
    '                                     & "ON OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L                   " & vbNewLine _
    '                                     & "AND OUTKA_S.SYS_DEL_FLG = '0'                             " & vbNewLine _
    '                                     & "AND OUTKA_S.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
    '                                     & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & " ) INKA_L                                                 " & vbNewLine _
    '                                     & "ON INKA_L.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L                 " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_S    OUTKA_S                  " & vbNewLine _
    '                                     & "ON     OUTKA_S.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    OUTKA_S.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L                " & vbNewLine _
    '                                     & "AND    OUTKA_S.SYS_DEL_FLG = '0'                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..D_ZAI_TRS    ZAI                  " & vbNewLine _
    '                                     & "ON     ZAI.NRS_BR_CD = OUTKA_S.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND    ZAI.ZAI_REC_NO = OUTKA_S.ZAI_REC_NO                " & vbNewLine _
    '                                     & "AND    ZAI.SYS_DEL_FLG = '0'                " & vbNewLine
    'START YANAI 要望番号1295 完了画面・検索速度改善
    '''' <summary>
    '''' FROM（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_OUTKA_ZAI As String = "FROM                                  " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ",SUM(OUTKA_M.BACKLOG_NB) AS SUM_BACKLOG_NB          " & vbNewLine _
    '                                    & "FROM                                                " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_L OUTKA_L2                        " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "$LM_TRN$..C_OUTKA_M OUTKA_M                         " & vbNewLine _
    '                                    & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L2.NRS_BR_CD       " & vbNewLine _
    '                                    & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L2.OUTKA_NO_L     " & vbNewLine _
    '                                    & "AND    OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "AND    OUTKA_L2.SYS_DEL_FLG = '0'                   " & vbNewLine _
    '                                    & "GROUP BY                                            " & vbNewLine _
    '                                    & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
    '                                    & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
    '                                    & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
    '                                    & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
    '                                    & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
    '                                    & ") OUTKA_L                                           " & vbNewLine _
    '                                    & "LEFT JOIN $LM_MST$..M_CUST    MCUST                 " & vbNewLine _
    '                                    & "ON     MCUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD          " & vbNewLine _
    '                                    & "AND    MCUST.CUST_CD_L = OUTKA_L.CUST_CD_L          " & vbNewLine _
    '                                    & "AND    MCUST.CUST_CD_M = OUTKA_L.CUST_CD_M          " & vbNewLine _
    '                                    & "AND    MCUST.CUST_CD_S = '00'                       " & vbNewLine _
    '                                    & "AND    MCUST.CUST_CD_SS = '00'                      " & vbNewLine _
    '                                    & "AND    MCUST.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
    '                                    & "AND    OUTKA_L.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "LEFT JOIN $LM_MST$..S_USER SUSER                    " & vbNewLine _
    '                                    & "ON     SUSER.USER_CD = MCUST.TANTO_CD               " & vbNewLine _
    '                                    & "LEFT JOIN $LM_MST$..M_SOKO SOKO                     " & vbNewLine _
    '                                    & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD           " & vbNewLine _
    '                                    & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                   " & vbNewLine _
    '                                    & "LEFT JOIN $LM_MST$..M_DEST MDEST                    " & vbNewLine _
    '                                    & "ON     MDEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD          " & vbNewLine _
    '                                    & "AND    MDEST.CUST_CD_L = OUTKA_L.CUST_CD_L          " & vbNewLine _
    '                                    & "AND    MDEST.DEST_CD = OUTKA_L.DEST_CD              " & vbNewLine _
    '                                    & "AND    OUTKA_L.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & "OUTKA_S.OUTKA_NO_L                                  " & vbNewLine _
    '                                    & ",INKA_L.INKA_STATE_KB AS CHK_INKA_STATE_KB          " & vbNewLine _
    '                                    & "FROM  $LM_TRN$..B_INKA_L INKA_L                     " & vbNewLine _
    '                                    & "INNER JOIN $LM_TRN$..C_OUTKA_S OUTKA_S              " & vbNewLine _
    '                                    & "ON OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L             " & vbNewLine _
    '                                    & "AND OUTKA_S.SYS_DEL_FLG = '0'                       " & vbNewLine _
    '                                    & "AND OUTKA_S.NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
    '                                    & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
    '                                    & " ) INKA_L                                           " & vbNewLine _
    '                                    & "ON INKA_L.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L           " & vbNewLine _
    '                                    & "LEFT JOIN $LM_TRN$..C_OUTKA_S    OUTKA_S            " & vbNewLine _
    '                                    & "ON     OUTKA_S.NRS_BR_CD = OUTKA_L.NRS_BR_CD        " & vbNewLine _
    '                                    & "AND    OUTKA_S.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L      " & vbNewLine _
    '                                    & "AND    OUTKA_S.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '                                    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS    ZAI                " & vbNewLine _
    '                                    & "ON     ZAI.NRS_BR_CD = OUTKA_S.NRS_BR_CD            " & vbNewLine _
    '                                    & "AND    ZAI.ZAI_REC_NO = OUTKA_S.ZAI_REC_NO          " & vbNewLine _
    '                                    & "AND    ZAI.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                    & "LEFT JOIN                                           " & vbNewLine _
    '                                    & "(SELECT                                             " & vbNewLine _
    '                                    & " OUTKAM.NRS_BR_CD                                   " & vbNewLine _
    '                                    & " ,OUTKAM.OUTKA_NO_L                                 " & vbNewLine _
    '                                    & " ,OUTKAM.OUTKA_NO_M                                 " & vbNewLine _
    '                                    & " ,COUNT(OUTKAS.NRS_BR_CD) AS SCNT                   " & vbNewLine _
    '                                    & " FROM $LM_TRN$..C_OUTKA_M OUTKAM                    " & vbNewLine _
    '                                    & " LEFT JOIN                                          " & vbNewLine _
    '                                    & " $LM_TRN$..C_OUTKA_L OUTKAL                         " & vbNewLine _
    '                                    & " ON                                                 " & vbNewLine _
    '                                    & " OUTKAL.NRS_BR_CD = OUTKAM.NRS_BR_CD                " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAL.OUTKA_NO_L = OUTKAM.OUTKA_NO_L              " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAL.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                    & " LEFT JOIN                                          " & vbNewLine _
    '                                    & " $LM_TRN$..C_OUTKA_S OUTKAS                         " & vbNewLine _
    '                                    & " ON                                                 " & vbNewLine _
    '                                    & " OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L              " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M              " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKAS.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                    & " WHERE                                              " & vbNewLine _
    '                                    & " OUTKAM.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                    & " GROUP BY                                           " & vbNewLine _
    '                                    & " OUTKAM.NRS_BR_CD                                   " & vbNewLine _
    '                                    & " ,OUTKAM.OUTKA_NO_L                                 " & vbNewLine _
    '                                    & " ,OUTKAM.OUTKA_NO_M                                 " & vbNewLine _
    '                                    & " ) OUTKASCNT                                        " & vbNewLine _
    '                                    & " ON                                                 " & vbNewLine _
    '                                    & " OUTKASCNT.NRS_BR_CD = OUTKA_L.NRS_BR_CD            " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKASCNT.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L          " & vbNewLine _
    '                                    & " AND                                                " & vbNewLine _
    '                                    & " OUTKASCNT.SCNT = '0'                               " & vbNewLine _
    ''' <summary>
    ''' FROM（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKA_ZAI As String = "FROM                                  " & vbNewLine _
                                        & "(SELECT                                             " & vbNewLine _
                                        & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
                                        & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
                                        & ",OUTKA_L2.SYUBETU_KB                                " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
                                        & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
                                        & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
                                        & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
                                        & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
                                        & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
                                        & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
                                        & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
                                        & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
                                        & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
                                        & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
                                        & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
                                        & ",SUM(OUTKA_M.BACKLOG_NB) AS SUM_BACKLOG_NB          " & vbNewLine _
                                        & "FROM                                                " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L OUTKA_L2                        " & vbNewLine _
                                        & "LEFT JOIN                                           " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_M OUTKA_M                         " & vbNewLine _
                                        & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L2.NRS_BR_CD       " & vbNewLine _
                                        & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L2.OUTKA_NO_L     " & vbNewLine _
                                        & "AND    OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine

    ''' <summary>
    ''' FROM（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKA_ZAI2 As String = "GROUP BY                             " & vbNewLine _
                                        & "OUTKA_L2.NRS_BR_CD                                  " & vbNewLine _
                                        & ",OUTKA_L2.WH_CD                                     " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_NO_L                                " & vbNewLine _
                                        & ",OUTKA_L2.SYUBETU_KB                                " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_STATE_KB                            " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_PLAN_DATE                           " & vbNewLine _
                                        & ",OUTKA_L2.CUST_ORD_NO                               " & vbNewLine _
                                        & ",OUTKA_L2.CUST_CD_L                                 " & vbNewLine _
                                        & ",OUTKA_L2.CUST_CD_M                                 " & vbNewLine _
                                        & ",OUTKA_L2.DEST_CD                                   " & vbNewLine _
                                        & ",OUTKA_L2.DEST_NM                                   " & vbNewLine _
                                        & ",OUTKA_L2.OUTKA_PKG_NB                              " & vbNewLine _
                                        & ",OUTKA_L2.END_DATE                                  " & vbNewLine _
                                        & ",OUTKA_L2.NIHUDA_FLAG                               " & vbNewLine _
                                        & ",OUTKA_L2.DEST_KB                                   " & vbNewLine _
                                        & ",OUTKA_L2.SYS_UPD_DATE                              " & vbNewLine _
                                        & ",OUTKA_L2.SYS_UPD_TIME                              " & vbNewLine _
                                        & ",OUTKA_L2.SYS_DEL_FLG                               " & vbNewLine _
                                        & ") OUTKA_L                                           " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_CUST    MCUST                 " & vbNewLine _
                                        & "ON     MCUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD          " & vbNewLine _
                                        & "AND    MCUST.CUST_CD_L = OUTKA_L.CUST_CD_L          " & vbNewLine _
                                        & "AND    MCUST.CUST_CD_M = OUTKA_L.CUST_CD_M          " & vbNewLine _
                                        & "AND    MCUST.CUST_CD_S = '00'                       " & vbNewLine _
                                        & "AND    MCUST.CUST_CD_SS = '00'                      " & vbNewLine _
                                        & "AND    OUTKA_L.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..S_USER SUSER                    " & vbNewLine _
                                        & "ON     SUSER.USER_CD = MCUST.TANTO_CD               " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_SOKO SOKO                     " & vbNewLine _
                                        & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD           " & vbNewLine _
                                        & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                   " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_DEST MDEST                    " & vbNewLine _
                                        & "ON     MDEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD          " & vbNewLine _
                                        & "AND    MDEST.CUST_CD_L = OUTKA_L.CUST_CD_L          " & vbNewLine _
                                        & "AND    MDEST.DEST_CD = OUTKA_L.DEST_CD              " & vbNewLine _
                                        & "AND    OUTKA_L.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                        & "--LEFT JOIN                                           " & vbNewLine _
                                        & "--(SELECT                                             " & vbNewLine _
                                        & "--OUTKA_S.OUTKA_NO_L                                  " & vbNewLine _
                                        & "--,INKA_L.INKA_STATE_KB AS CHK_INKA_STATE_KB          " & vbNewLine _
                                        & "--FROM  $LM_TRN$..B_INKA_L INKA_L                     " & vbNewLine _
                                        & "--INNER JOIN $LM_TRN$..C_OUTKA_S OUTKA_S              " & vbNewLine _
                                        & "--ON OUTKA_S.NRS_BR_CD = INKA_L.NRS_BR_CD             " & vbNewLine _
                                        & "--AND OUTKA_S.INKA_NO_L = INKA_L.INKA_NO_L            " & vbNewLine _
                                        & "--AND OUTKA_S.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                        & "--WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                        & "-- ) INKA_L                                           " & vbNewLine _
                                        & "--ON INKA_L.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L           " & vbNewLine _
                                        & "LEFT JOIN $LM_TRN$..C_OUTKA_S    OUTKA_S            " & vbNewLine _
                                        & "ON     OUTKA_S.NRS_BR_CD = OUTKA_L.NRS_BR_CD        " & vbNewLine _
                                        & "AND    OUTKA_S.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L      " & vbNewLine _
                                        & "AND    OUTKA_S.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                        & "LEFT JOIN $LM_TRN$..D_ZAI_TRS    ZAI                " & vbNewLine _
                                        & "ON     ZAI.NRS_BR_CD = OUTKA_S.NRS_BR_CD            " & vbNewLine _
                                        & "AND    ZAI.ZAI_REC_NO = OUTKA_S.ZAI_REC_NO          " & vbNewLine _
                                        & "AND    ZAI.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                        & "LEFT JOIN                                           " & vbNewLine _
                                        & "(SELECT                                             " & vbNewLine _
                                        & " OUTKAM.NRS_BR_CD                                   " & vbNewLine _
                                        & " ,OUTKAM.OUTKA_NO_L                                 " & vbNewLine _
                                        & " ,OUTKAM.OUTKA_NO_M                                 " & vbNewLine _
                                        & " ,COUNT(OUTKAS.NRS_BR_CD) AS SCNT                   " & vbNewLine _
                                        & " FROM $LM_TRN$..C_OUTKA_M OUTKAM                    " & vbNewLine _
                                        & " LEFT JOIN                                          " & vbNewLine _
                                        & " $LM_TRN$..C_OUTKA_L OUTKAL                         " & vbNewLine _
                                        & " ON                                                 " & vbNewLine _
                                        & " OUTKAL.NRS_BR_CD = OUTKAM.NRS_BR_CD                " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAL.OUTKA_NO_L = OUTKAM.OUTKA_NO_L              " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAL.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                        & " LEFT JOIN                                          " & vbNewLine _
                                        & " $LM_TRN$..C_OUTKA_S OUTKAS                         " & vbNewLine _
                                        & " ON                                                 " & vbNewLine _
                                        & " OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L              " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M              " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKAS.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                        & " WHERE                                              " & vbNewLine _
                                        & " OUTKAM.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                        & " GROUP BY                                           " & vbNewLine _
                                        & " OUTKAM.NRS_BR_CD                                   " & vbNewLine _
                                        & " ,OUTKAM.OUTKA_NO_L                                 " & vbNewLine _
                                        & " ,OUTKAM.OUTKA_NO_M                                 " & vbNewLine _
                                        & " ) OUTKASCNT                                        " & vbNewLine _
                                        & " ON                                                 " & vbNewLine _
                                        & " OUTKASCNT.NRS_BR_CD = OUTKA_L.NRS_BR_CD            " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKASCNT.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L          " & vbNewLine _
                                        & " AND                                                " & vbNewLine _
                                        & " OUTKASCNT.SCNT = '0'                               " & vbNewLine
    'END YANAI 要望番号1295 完了画面・検索速度改善
    'END YANAI 要望番号932
    'END YANAI 要望番号824

    'START YANAI 要望番号932
    '''' <summary>
    '''' GROUP BY（出荷検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_GROUP_BY_OUTKA_ZAI As String = "GROUP BY                                    " & vbNewLine _
    '                                     & " OUTKA_L.OUTKA_NO_L                                      " & vbNewLine _
    '                                     & ",OUTKA_L.OUTKA_PLAN_DATE                                 " & vbNewLine _
    '                                     & ",OUTKA_L.CUST_ORD_NO                                     " & vbNewLine _
    '                                     & ",SUSER.USER_NM                                           " & vbNewLine _
    '                                     & ",MCUST.CUST_NM_L                                         " & vbNewLine _
    '                                     & ",MCUST.CUST_NM_M                                         " & vbNewLine _
    '                                     & ",MDEST.DEST_NM                                           " & vbNewLine _
    '                                     & ",OUTKA_L.OUTKA_PKG_NB                                    " & vbNewLine _
    '                                     & ",OUTKA_L.NRS_BR_CD                                       " & vbNewLine _
    '                                     & ",SOKO.OUTKA_KENPIN_YN                                    " & vbNewLine _
    '                                     & ",SOKO.OUTKA_INFO_YN                                      " & vbNewLine _
    '                                     & ",OUTKA_L.END_DATE                                        " & vbNewLine _
    '                                     & ",OUTKA_L.NIHUDA_FLAG                                     " & vbNewLine _
    '                                     & ",OUTKA_L.SYS_UPD_DATE                                    " & vbNewLine _
    '                                     & ",OUTKA_L.SYS_UPD_TIME                                    " & vbNewLine _
    '                                     & ",MCUST.CUST_CD_L                                         " & vbNewLine _
    '                                     & ",OUTKA_L.DEST_KB                                         " & vbNewLine _
    '                                     & ",OUTKA_L.DEST_NM                                         " & vbNewLine _
    '                                     & ",OUTKA_S.ZAI_REC_NO                                         " & vbNewLine _
    '                                     & ",ZAI.SYS_UPD_DATE                                         " & vbNewLine _
    '                                     & ",ZAI.SYS_UPD_TIME                                         " & vbNewLine
    ''' <summary>
    ''' GROUP BY（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_OUTKA_ZAI As String = "GROUP BY                                " & vbNewLine _
                                         & " OUTKA_L.OUTKA_NO_L                                      " & vbNewLine _
                                         & ",OUTKA_L.OUTKA_PLAN_DATE                                 " & vbNewLine _
                                         & ",OUTKA_L.CUST_ORD_NO                                     " & vbNewLine _
                                         & ",SUSER.USER_NM                                           " & vbNewLine _
                                         & ",MCUST.CUST_NM_L                                         " & vbNewLine _
                                         & ",MCUST.CUST_NM_M                                         " & vbNewLine _
                                         & ",MDEST.DEST_NM                                           " & vbNewLine _
                                         & ",OUTKA_L.OUTKA_PKG_NB                                    " & vbNewLine _
                                         & ",OUTKA_L.NRS_BR_CD                                       " & vbNewLine _
                                         & ",SOKO.OUTKA_KENPIN_YN                                    " & vbNewLine _
                                         & ",SOKO.OUTKA_INFO_YN                                      " & vbNewLine _
                                         & ",OUTKA_L.END_DATE                                        " & vbNewLine _
                                         & ",OUTKA_L.NIHUDA_FLAG                                     " & vbNewLine _
                                         & ",OUTKA_L.SYS_UPD_DATE                                    " & vbNewLine _
                                         & ",OUTKA_L.SYS_UPD_TIME                                    " & vbNewLine _
                                         & ",MCUST.CUST_CD_L                                         " & vbNewLine _
                                         & ",OUTKA_L.DEST_KB                                         " & vbNewLine _
                                         & ",OUTKA_L.DEST_NM                                         " & vbNewLine _
                                         & ",OUTKA_L.SYUBETU_KB                                      " & vbNewLine _
                                         & ",OUTKA_S.ZAI_REC_NO                                      " & vbNewLine _
                                         & ",ZAI.SYS_UPD_DATE                                        " & vbNewLine _
                                         & ",ZAI.SYS_UPD_TIME                                        " & vbNewLine _
                                         & ",OUTKASCNT.SCNT                                          " & vbNewLine
    'END YANAI 要望番号932

    ''' <summary>
    ''' ORDER BY（出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_OUTKA_ZAI As String = "ORDER BY                                    " & vbNewLine _
                                         & " OUTKA_L.OUTKA_NO_L                                      " & vbNewLine _
                                         & ",OUTKA_L.OUTKA_PLAN_DATE                                 " & vbNewLine
    'END YANAI 要望番号653

#End Region

#Region "分析票管理マスタ検索"

    ''' <summary>
    ''' カウント用（分析票管理マスタ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_COA As String = " SELECT COUNT(INKA_L.NRS_BR_CD)	                AS SELECT_CNT          " & vbNewLine

    ''' <summary>
    ''' データ抽出用（分析票管理マスタ作成時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_COA As String = " SELECT DISTINCT                                                        " & vbNewLine _
                                            & " INKA_L.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
                                            & ",INKA_M.GOODS_CD_NRS                                 AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",INKA_S.LOT_NO                                       AS LOT_NO              " & vbNewLine _
                                            & ",'ZZZZZZZZZZZZZZZ'                                   AS DEST_CD	           " & vbNewLine _
                                            & "--ADD START 2018/12/18 要望管理003858                                       " & vbNewLine _
                                            & ",''                                                  AS INKA_DATE           " & vbNewLine _
                                            & ",'1'                                                 AS INKA_DATE_VERS_FLG  " & vbNewLine _
                                            & "--ADD END   2018/12/18 要望管理003858                                       " & vbNewLine _
                                            & ",INKA_L.CUST_CD_L                                    AS CUST_CD_L           " & vbNewLine _
                                            & ",INKA_L.CUST_CD_M                                    AS CUST_CD_M           " & vbNewLine _
                                            & ",''                                                  AS COA_LINK            " & vbNewLine _
                                            & ",''                                                  AS COA_NAME            " & vbNewLine _
                                            & ",@SYS_ENT_DATE                                       AS SYS_ENT_DATE        " & vbNewLine _
                                            & ",@SYS_ENT_TIME                                       AS SYS_ENT_TIME        " & vbNewLine _
                                            & ",@SYS_ENT_PGID                                       AS SYS_ENT_PGID        " & vbNewLine _
                                            & ",@SYS_ENT_USER                                       AS SYS_ENT_USER        " & vbNewLine _
                                            & ",@SYS_UPD_DATE                                       AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",@SYS_UPD_TIME                                       AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",@SYS_UPD_PGID                                       AS SYS_UPD_PGID        " & vbNewLine _
                                            & ",@SYS_UPD_USER                                       AS SYS_UPD_USER        " & vbNewLine _
                                            & ",@SYS_DEL_FLG                                        AS SYS_DEL_FLG        " & vbNewLine

    ''' <summary>
    ''' FROM（分析票管理マスタ検索時 + 作成時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_COA As String = "FROM                                               " & vbNewLine _
                                         & "$LM_TRN$..B_INKA_L INKA_L                                 " & vbNewLine _
                                         & "INNER JOIN $LM_TRN$..B_INKA_M INKA_M                      " & vbNewLine _
                                         & "ON     INKA_M.NRS_BR_CD = INKA_L.NRS_BR_CD                " & vbNewLine _
                                         & "AND    INKA_M.INKA_NO_L = INKA_L.INKA_NO_L                " & vbNewLine _
                                         & "AND    INKA_M.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                         & "AND    INKA_M.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "INNER JOIN $LM_TRN$..B_INKA_S INKA_S                      " & vbNewLine _
                                         & "ON     INKA_S.NRS_BR_CD = INKA_M.NRS_BR_CD                " & vbNewLine _
                                         & "AND    INKA_S.INKA_NO_L = INKA_M.INKA_NO_L                " & vbNewLine _
                                         & "AND    INKA_S.INKA_NO_M = INKA_M.INKA_NO_M                " & vbNewLine _
                                         & "AND    INKA_S.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                         & "AND    INKA_S.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "AND    INKA_M.SYS_DEL_FLG = '0'                           " & vbNewLine

    ''' <summary>
    ''' FROM（分析票管理マスタ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_COA2 As String = "INNER JOIN $LM_MST$..M_COA MCOA                   " & vbNewLine _
                                         & "ON     MCOA.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
                                         & "AND    (MCOA.NRS_BR_CD = INKA_L.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    MCOA.GOODS_CD_NRS = INKA_M.GOODS_CD_NRS            " & vbNewLine _
                                         & "AND    MCOA.LOT_NO = INKA_S.LOT_NO                        " & vbNewLine _
                                         & "AND    MCOA.DEST_CD = 'ZZZZZZZZZZZZZZZ')                  " & vbNewLine

    '20160216 要望番号2521 tsunehira add start
    ''' <summary>
    ''' FROM（分析票管理マスタINSERT時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_COA3 As String = "LEFT JOIN  LM_MST..M_COA MCOA                 " & vbNewLine _
                                     & "ON    MCOA.NRS_BR_CD = INKA_L.NRS_BR_CD                   " & vbNewLine _
                                     & "AND   MCOA.GOODS_CD_NRS = INKA_M.GOODS_CD_NRS             " & vbNewLine _
                                     & "AND   MCOA.LOT_NO = INKA_S.LOT_NO                         " & vbNewLine _
                                     & "--ADD START 2018/12/18 要望管理003858                     " & vbNewLine _
                                     & "AND   MCOA.INKA_DATE = ''                                 " & vbNewLine _
                                     & "AND   MCOA.INKA_DATE_VERS_FLG = '1'                       " & vbNewLine _
                                     & "--ADD END   2018/12/18 要望管理003858                     " & vbNewLine _
                                     & "AND   MCOA.CUST_CD_L = INKA_L.CUST_CD_L                   " & vbNewLine _
                                     & "AND   MCOA.CUST_CD_M = INKA_L.CUST_CD_M                   " & vbNewLine _
    '20160216 要望番号2521 tsunehira add end

    ''' <summary>
    ''' FROM（分析票管理マスタ検索時 + 作成時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_COA As String = "WHERE                                             " & vbNewLine _
                                         & "       INKA_L.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                         & "AND    INKA_L.INKA_NO_L = @INOUTKA_NO_L                   " & vbNewLine _
                                         & "AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine

    '20160216 要望番号2521 tsunehira add start
    ''' <summary>
    ''' FROM（分析票管理マスタINSERT時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_COA2 As String = "AND    MCOA.NRS_BR_CD IS NULL                    " & vbNewLine _
    '20160216 要望番号2521 tsunehira add end


#End Region

#Region "荷主明細マスタ検索"

    ''' <summary>
    ''' FROM（分析票管理マスタ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CUSTDETAILS As String = "INNER JOIN $LM_MST$..M_CUST_DETAILS MCUSTDETAILS           " & vbNewLine _
                                                         & "ON     MCUSTDETAILS.NRS_BR_CD = INKA_L.NRS_BR_CD          " & vbNewLine _
                                                         & "AND    MCUSTDETAILS.CUST_CD LIKE INKA_L.CUST_CD_L + '%'   " & vbNewLine _
                                                         & "AND    MCUSTDETAILS.SUB_KB = '17'                         " & vbNewLine _
                                                         & "AND    MCUSTDETAILS.SET_NAIYO = '01'                      " & vbNewLine

#End Region

#Region "小分け出荷検索"

    ''' <summary>
    ''' カウント用（小分け出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_KOWAKE As String = " SELECT COUNT(OUTKA_L.NRS_BR_CD)	            AS SELECT_CNT          " & vbNewLine

    ''' <summary>
    ''' FROM（小分け出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KOWAKE As String = "FROM                                            " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L OUTKA_L                               " & vbNewLine _
                                         & "INNER JOIN $LM_TRN$..C_OUTKA_M OUTKA_M                    " & vbNewLine _
                                         & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L.NRS_BR_CD              " & vbNewLine _
                                         & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L            " & vbNewLine _
                                         & "AND    OUTKA_M.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                         & "AND    OUTKA_M.ALCTD_KB = '03'                            " & vbNewLine _
                                         & "AND    OUTKA_M.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                         & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine

    ''' <summary>
    ''' FROM（小分け出荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_KOWAKE As String = "WHERE                                          " & vbNewLine _
                                         & "       OUTKA_L.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                         & "AND    OUTKA_L.OUTKA_NO_L = @INOUTKA_NO_L                 " & vbNewLine _
                                         & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine

#End Region

#Region "梱包作業検索"

    ''' <summary>
    ''' カウント用（梱包作業検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SAGYO As String = " SELECT COUNT(ESAGYO.NRS_BR_CD)	            AS SELECT_CNT          " & vbNewLine

    ''' <summary>
    ''' データ抽出用（梱包作業作成時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SAGYO As String = " SELECT                                                               " & vbNewLine _
                                            & " OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
                                            & ",@SAGYO_REC_NO                                       AS SAGYO_REC_NO        " & vbNewLine _
                                            & ",'01'                                                AS SAGYO_COMP          " & vbNewLine _
                                            & ",'00'                                                AS SKYU_CHK	           " & vbNewLine _
                                            & ",''                                                  AS SAGYO_SIJI_NO       " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_NO_L + OUTKA_M.OUTKA_NO_M             AS INOUTKA_NO_LM       " & vbNewLine _
                                            & ",OUTKA_L.WH_CD                                       AS WH_CD               " & vbNewLine _
                                            & ",'20'                                                AS IOZS_KB             " & vbNewLine _
                                            & ",GOODS.PKG_SAGYO                                     AS SAGYO_CD            " & vbNewLine _
                                            & ",''                                                  AS SAGYO_NM            " & vbNewLine _
                                            & ",OUTKA_L.CUST_CD_L                                   AS CUST_CD_L           " & vbNewLine _
                                            & ",OUTKA_L.CUST_CD_M                                   AS CUST_CD_M           " & vbNewLine _
                                            & ",OUTKA_L.DEST_CD                                     AS DEST_CD             " & vbNewLine _
                                            & ",''                                                  AS DEST_NM             " & vbNewLine _
                                            & ",OUTKA_M.GOODS_CD_NRS                                AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",GOODS.GOODS_NM_1                                    AS GOODS_NM_NRS        " & vbNewLine _
                                            & ",OUTKA_M.LOT_NO                                      AS LOT_NO              " & vbNewLine _
                                            & ",SAGYO.INV_TANI                                      AS INV_TANI            " & vbNewLine _
                                            & ",'1'                                                 AS SAGYO_NB            " & vbNewLine _
                                            & ",SAGYO.SAGYO_UP                                      AS SAGYO_UP            " & vbNewLine _
                                            & ",'0'                                                 AS SAGYO_GK            " & vbNewLine _
                                            & ",SAGYO.ZEI_KBN                                       AS TAX_KB              " & vbNewLine _
                                            & ",CUST.SAGYO_SEIQTO_CD                                AS SEIQTO_CD           " & vbNewLine _
                                            & ",''                                                  AS REMARK_ZAI          " & vbNewLine _
                                            & ",''                                                  AS REMARK_SKYU         " & vbNewLine _
                                            & ",''                                                  AS SAGYO_COMP_CD       " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_PLAN_DATE                             AS SAGYO_COMP_DATE     " & vbNewLine _
                                            & ",'00'                                                AS DEST_SAGYO_FLG      " & vbNewLine _
                                            & ",@SYS_ENT_DATE                                       AS SYS_ENT_DATE        " & vbNewLine _
                                            & ",@SYS_ENT_TIME                                       AS SYS_ENT_TIME        " & vbNewLine _
                                            & ",@SYS_ENT_PGID                                       AS SYS_ENT_PGID        " & vbNewLine _
                                            & ",@SYS_ENT_USER                                       AS SYS_ENT_USER        " & vbNewLine _
                                            & ",@SYS_UPD_DATE                                       AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",@SYS_UPD_TIME                                       AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",@SYS_UPD_PGID                                       AS SYS_UPD_PGID        " & vbNewLine _
                                            & ",@SYS_UPD_USER                                       AS SYS_UPD_USER        " & vbNewLine _
                                            & ",@SYS_DEL_FLG                                        AS SYS_DEL_FLG        " & vbNewLine

    ''' <summary>
    ''' FROM（梱包作業検索時 + 作成時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SAGYO As String = "FROM                                             " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L OUTKA_L                               " & vbNewLine _
                                         & "INNER JOIN $LM_TRN$..C_OUTKA_M OUTKA_M                    " & vbNewLine _
                                         & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L.NRS_BR_CD              " & vbNewLine _
                                         & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L            " & vbNewLine _
                                         & "AND    OUTKA_M.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                         & "AND    OUTKA_M.ALCTD_KB = '03'                            " & vbNewLine _
                                         & "AND    OUTKA_M.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                         & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                         & "INNER JOIN $LM_MST$..M_GOODS GOODS                        " & vbNewLine _
                                         & "ON     GOODS.NRS_BR_CD = OUTKA_M.NRS_BR_CD                " & vbNewLine _
                                         & "AND    GOODS.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS          " & vbNewLine _
                                         & "AND    GOODS.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                         & "AND    OUTKA_M.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                         & "INNER JOIN $LM_MST$..M_SAGYO SAGYO                        " & vbNewLine _
                                         & "ON     SAGYO.NRS_BR_CD = GOODS.NRS_BR_CD                  " & vbNewLine _
                                         & "AND    SAGYO.SAGYO_CD = GOODS.PKG_SAGYO                   " & vbNewLine _
                                         & "AND    SAGYO.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                         & "INNER JOIN $LM_MST$..M_CUST CUST                          " & vbNewLine _
                                         & "ON     CUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    CUST.CUST_CD_L = OUTKA_L.CUST_CD_L                 " & vbNewLine _
                                         & "AND    CUST.CUST_CD_M = OUTKA_L.CUST_CD_M                 " & vbNewLine _
                                         & "AND    CUST.CUST_CD_S = '00'                              " & vbNewLine _
                                         & "AND    CUST.CUST_CD_SS = '00'                             " & vbNewLine _
                                         & "AND    CUST.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
                                         & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine

    ''' <summary>
    ''' FROM（梱包作業検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SAGYO2 As String = "INNER JOIN $LM_TRN$..E_SAGYO ESAGYO             " & vbNewLine _
                                         & "ON     ESAGYO.NRS_BR_CD = OUTKA_L.NRS_BR_CD               " & vbNewLine _
                                         & "AND    ESAGYO.INOUTKA_NO_LM = OUTKA_L.OUTKA_NO_L + OUTKA_M.OUTKA_NO_M " & vbNewLine _
                                         & "AND    ESAGYO.SAGYO_CD = GOODS.PKG_SAGYO                  " & vbNewLine _
                                         & "AND    ESAGYO.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                         & "AND    ESAGYO.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                         & "AND    OUTKA_M.SYS_DEL_FLG = '0'                          " & vbNewLine


    ''' <summary>
    ''' FROM（梱包作業検索時 + 作成時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_SAGYO As String = "WHERE                                           " & vbNewLine _
                                         & "       OUTKA_L.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                         & "AND    OUTKA_L.OUTKA_NO_L = @INOUTKA_NO_L                 " & vbNewLine _
                                         & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine

#End Region

#Region "他荷主在庫検索"

    ''' <summary>
    ''' カウント用（他荷主在庫検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_OTHER As String = " SELECT COUNT(OUTKA_L.NRS_BR_CD)	            AS SELECT_CNT          " & vbNewLine

    'START YANAI 要望番号589
    '''' <summary>
    '''' データ抽出用（他荷主在庫検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_OTHER As String = " SELECT                                                               " & vbNewLine _
    '                                        & " OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",OUTKA_L.CUST_CD_L                                   AS CUST_CD_L_OUTKA     " & vbNewLine _
    '                                        & ",OUTKA_L.CUST_CD_M                                   AS CUST_CD_M_OUTKA     " & vbNewLine _
    '                                        & ",ZAI.CUST_CD_L                                       AS CUST_CD_L_ZAI       " & vbNewLine _
    '                                        & ",ZAI.CUST_CD_M                                       AS CUST_CD_M_ZAI       " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_NO_L                                  AS OUTKA_NO_L          " & vbNewLine _
    '                                        & ",OUTKA_M.OUTKA_NO_M                                  AS OUTKA_NO_M          " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_NO_S                                  AS OUTKA_NO_S          " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_PLAN_DATE                             AS OUTKA_PLAN_DATE     " & vbNewLine _
    '                                        & ",OUTKA_L.WH_CD                                       AS WH_CD               " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_QT                                    AS ALCTD_QT            " & vbNewLine _
    '                                        & ",GOODS.STD_IRIME_UT                                  AS STD_IRIME_UT        " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_NB                                    AS ALCTD_NB            " & vbNewLine _
    '                                        & ",CUST.HOKAN_FREE_KIKAN                               AS HOKAN_FREE_KIKAN    " & vbNewLine _
    '                                        & ",OUTKA_M.GOODS_CD_NRS                                AS GOODS_CD_NRS        " & vbNewLine _
    '                                        & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
    '                                        & ",OUTKA_S.IRIME                                       AS IRIME               " & vbNewLine _
    '                                        & ",OUTKA_S.BETU_WT                                     AS BETU_WT             " & vbNewLine _
    '                                        & ",OUTKA_S.TOU_NO                                      AS TOU_NO              " & vbNewLine _
    '                                        & ",OUTKA_S.SITU_NO                                     AS SITU_NO             " & vbNewLine _
    '                                        & ",OUTKA_S.ZONE_CD                                     AS ZONE_CD             " & vbNewLine _
    '                                        & ",OUTKA_S.LOCA                                        AS LOCA                " & vbNewLine _
    '                                        & ",OUTKA_S.LOT_NO                                      AS LOT_NO              " & vbNewLine _
    '                                        & ",OUTKA_S.SERIAL_NO                                   AS SERIAL_NO           " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
    '                                        & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
    '                                        & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
    '                                        & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
    '                                        & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
    '                                        & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
    '                                        & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
    '                                        & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
    '                                        & ",GOODS.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
    '                                        & ",OUTKA_L.DENP_NO                                     AS DENP_NO             " & vbNewLine _
    '                                        & ",OUTKA_L.ARR_KANRYO_INFO                             AS ARR_KANRYO_INFO     " & vbNewLine _
    '                                        & ",OUTKA_L.ARR_PLAN_TIME                               AS ARR_PLAN_TIME       " & vbNewLine _
    '                                        & ",OUTKA_L.HOKOKU_DATE                                 AS HOKOKU_DATE         " & vbNewLine _
    '                                        & ",OUTKA_L.SHIP_CD_L                                   AS SHIP_CD_L           " & vbNewLine _
    '                                        & ",OUTKA_L.SHIP_CD_M                                   AS SHIP_CD_M           " & vbNewLine _
    '                                        & ",OUTKA_L.DEST_CD                                     AS DEST_CD             " & vbNewLine _
    '                                        & ",OUTKA_L.DEST_AD_3                                   AS DEST_AD_3           " & vbNewLine _
    '                                        & ",OUTKA_L.DEST_TEL                                    AS DEST_TEL            " & vbNewLine _
    '                                        & ",OUTKA_L.NHS_REMARK                                  AS NHS_REMARK          " & vbNewLine _
    '                                        & ",OUTKA_L.SP_NHS_KB                                   AS SP_NHS_KB           " & vbNewLine _
    '                                        & ",OUTKA_L.COA_YN                                      AS COA_YN              " & vbNewLine _
    '                                        & ",OUTKA_L.CUST_ORD_NO                                 AS CUST_ORD_NO         " & vbNewLine _
    '                                        & ",OUTKA_L.BUYER_ORD_NO                                AS BUYER_ORD_NO        " & vbNewLine _
    '                                        & ",OUTKA_L.REMARK                                      AS OUTKA_L_REMARK      " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_PKG_NB                                AS OUTKA_PKG_NB        " & vbNewLine _
    '                                        & ",OUTKA_L.DENP_YN                                     AS DENP_YN             " & vbNewLine _
    '                                        & ",OUTKA_L.PC_KB                                       AS PC_KB               " & vbNewLine _
    '                                        & ",OUTKA_L.ALL_PRINT_FLAG                              AS ALL_PRINT_FLAG      " & vbNewLine _
    '                                        & ",OUTKA_L.NIHUDA_FLAG                                 AS NIHUDA_FLAG         " & vbNewLine _
    '                                        & ",OUTKA_L.NHS_FLAG                                    AS NHS_FLAG            " & vbNewLine _
    '                                        & ",OUTKA_L.DENP_FLAG                                   AS DENP_FLAG           " & vbNewLine _
    '                                        & ",OUTKA_L.COA_FLAG                                    AS COA_FLAG            " & vbNewLine _
    '                                        & ",OUTKA_L.HOKOKU_FLAG                                 AS HOKOKU_FLAG         " & vbNewLine _
    '                                        & ",OUTKA_L.MATOME_PICK_FLAG                            AS MATOME_PICK_FLAG    " & vbNewLine _
    '                                        & ",OUTKA_L.LAST_PRINT_DATE                             AS LAST_PRINT_DATE     " & vbNewLine _
    '                                        & ",OUTKA_L.LAST_PRINT_TIME                             AS LAST_PRINT_TIME     " & vbNewLine _
    '                                        & ",OUTKA_L.SASZ_USER                                   AS SASZ_USER           " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKO_USER                                  AS OUTKO_USER          " & vbNewLine _
    '                                        & ",OUTKA_L.KEN_USER                                    AS KEN_USER            " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_USER                                  AS OUTKA_USER          " & vbNewLine _
    '                                        & ",OUTKA_L.HOU_USER                                    AS HOU_USER            " & vbNewLine _
    '                                        & ",OUTKA_L.ORDER_TYPE                                  AS ORDER_TYPE          " & vbNewLine _
    '                                        & ",OUTKA_M.EDI_SET_NO                                  AS EDI_SET_NO          " & vbNewLine _
    '                                        & ",OUTKA_M.COA_YN                                      AS COA_YN_M            " & vbNewLine _
    '                                        & ",OUTKA_M.CUST_ORD_NO_DTL                             AS CUST_ORD_NO_DTL     " & vbNewLine _
    '                                        & ",OUTKA_M.BUYER_ORD_NO_DTL                            AS BUYER_ORD_NO_DTL    " & vbNewLine _
    '                                        & ",OUTKA_M.RSV_NO                                      AS RSV_NO              " & vbNewLine _
    '                                        & ",OUTKA_M.ALCTD_KB                                    AS ALCTD_KB            " & vbNewLine _
    '                                        & ",OUTKA_M.OUTKA_PKG_NB                                AS OUTKA_PKG_NB_M      " & vbNewLine _
    '                                        & ",OUTKA_M.IRIME                                       AS IRIME_M             " & vbNewLine _
    '                                        & ",OUTKA_M.IRIME_UT                                    AS IRIME_UT            " & vbNewLine _
    '                                        & ",OUTKA_M.OUTKA_M_PKG_NB                              AS OUTKA_M_PKG_NB      " & vbNewLine _
    '                                        & ",OUTKA_M.REMARK                                      AS OUTKA_M_REMARK      " & vbNewLine _
    '                                        & ",OUTKA_M.SIZE_KB                                     AS SIZE_KB             " & vbNewLine _
    '                                        & ",OUTKA_M.ZAIKO_KB                                    AS ZAIKO_KB            " & vbNewLine _
    '                                        & ",OUTKA_M.SOURCE_CD                                   AS SOURCE_CD           " & vbNewLine _
    '                                        & ",OUTKA_M.YELLOW_CARD                                 AS YELLOW_CARD         " & vbNewLine _
    '                                        & ",OUTKA_M.GOODS_CD_NRS_FROM                           AS GOODS_CD_NRS_FROM   " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_TTL_NB                                AS OUTKA_TTL_NB        " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_TTL_QT                                AS OUTKA_TTL_QT        " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_CAN_NB                                AS ALCTD_CAN_NB        " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_CAN_QT                                AS ALCTD_CAN_QT        " & vbNewLine _
    '                                        & ",OUTKA_S.SMPL_FLAG                                   AS SMPL_FLAG           " & vbNewLine _
    '                                        & ",ZAI.RSV_NO                                          AS RSV_NO_ZAI          " & vbNewLine _
    '                                        & ",ZAI.SERIAL_NO                                       AS SERIAL_NO_ZAI       " & vbNewLine _
    '                                        & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
    '                                        & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
    '                                        & ",ZAI.DEST_CD_P                                       AS DEST_CD_ZAI         " & vbNewLine _
    '                                        & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG_ZAI       " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",OUTKA_S.SYS_UPD_DATE                                AS SYS_UPD_DATE_OUT_S  " & vbNewLine _
    '                                        & ",OUTKA_S.SYS_UPD_TIME                                AS SYS_UPD_TIME_OUT_S  " & vbNewLine _
    '                                        & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine
    'START YANAI 要望番号510
    '''' <summary>
    '''' データ抽出用（他荷主在庫検索時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_OTHER As String = " SELECT                                                               " & vbNewLine _
    '                                        & " OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",OUTKA_L.CUST_CD_L                                   AS CUST_CD_L_OUTKA     " & vbNewLine _
    '                                        & ",OUTKA_L.CUST_CD_M                                   AS CUST_CD_M_OUTKA     " & vbNewLine _
    '                                        & ",ZAI.CUST_CD_L                                       AS CUST_CD_L_ZAI       " & vbNewLine _
    '                                        & ",ZAI.CUST_CD_M                                       AS CUST_CD_M_ZAI       " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_NO_L                                  AS OUTKA_NO_L          " & vbNewLine _
    '                                        & ",OUTKA_M.OUTKA_NO_M                                  AS OUTKA_NO_M          " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_NO_S                                  AS OUTKA_NO_S          " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_PLAN_DATE                             AS OUTKA_PLAN_DATE     " & vbNewLine _
    '                                        & ",OUTKA_L.WH_CD                                       AS WH_CD               " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_QT                                    AS ALCTD_QT            " & vbNewLine _
    '                                        & ",GOODS.STD_IRIME_UT                                  AS STD_IRIME_UT        " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_NB                                    AS ALCTD_NB            " & vbNewLine _
    '                                        & ",CUST.HOKAN_FREE_KIKAN                               AS HOKAN_FREE_KIKAN    " & vbNewLine _
    '                                        & ",OUTKA_M.GOODS_CD_NRS                                AS GOODS_CD_NRS        " & vbNewLine _
    '                                        & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
    '                                        & ",OUTKA_S.IRIME                                       AS IRIME               " & vbNewLine _
    '                                        & ",OUTKA_S.BETU_WT                                     AS BETU_WT             " & vbNewLine _
    '                                        & ",OUTKA_S.TOU_NO                                      AS TOU_NO              " & vbNewLine _
    '                                        & ",OUTKA_S.SITU_NO                                     AS SITU_NO             " & vbNewLine _
    '                                        & ",OUTKA_S.ZONE_CD                                     AS ZONE_CD             " & vbNewLine _
    '                                        & ",OUTKA_S.LOCA                                        AS LOCA                " & vbNewLine _
    '                                        & ",OUTKA_S.LOT_NO                                      AS LOT_NO              " & vbNewLine _
    '                                        & ",OUTKA_S.SERIAL_NO                                   AS SERIAL_NO           " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
    '                                        & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
    '                                        & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
    '                                        & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
    '                                        & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
    '                                        & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
    '                                        & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
    '                                        & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
    '                                        & ",GOODS.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
    '                                        & ",OUTKA_L.DENP_NO                                     AS DENP_NO             " & vbNewLine _
    '                                        & ",OUTKA_L.ARR_KANRYO_INFO                             AS ARR_KANRYO_INFO     " & vbNewLine _
    '                                        & ",OUTKA_L.ARR_PLAN_TIME                               AS ARR_PLAN_TIME       " & vbNewLine _
    '                                        & ",OUTKA_L.HOKOKU_DATE                                 AS HOKOKU_DATE         " & vbNewLine _
    '                                        & ",OUTKA_L.SHIP_CD_L                                   AS SHIP_CD_L           " & vbNewLine _
    '                                        & ",OUTKA_L.SHIP_CD_M                                   AS SHIP_CD_M           " & vbNewLine _
    '                                        & ",OUTKA_L.DEST_CD                                     AS DEST_CD             " & vbNewLine _
    '                                        & ",OUTKA_L.DEST_AD_3                                   AS DEST_AD_3           " & vbNewLine _
    '                                        & ",OUTKA_L.DEST_TEL                                    AS DEST_TEL            " & vbNewLine _
    '                                        & ",OUTKA_L.NHS_REMARK                                  AS NHS_REMARK          " & vbNewLine _
    '                                        & ",OUTKA_L.SP_NHS_KB                                   AS SP_NHS_KB           " & vbNewLine _
    '                                        & ",OUTKA_L.COA_YN                                      AS COA_YN              " & vbNewLine _
    '                                        & ",OUTKA_L.CUST_ORD_NO                                 AS CUST_ORD_NO         " & vbNewLine _
    '                                        & ",OUTKA_L.BUYER_ORD_NO                                AS BUYER_ORD_NO        " & vbNewLine _
    '                                        & ",OUTKA_L.REMARK                                      AS OUTKA_L_REMARK      " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_PKG_NB                                AS OUTKA_PKG_NB        " & vbNewLine _
    '                                        & ",OUTKA_L.DENP_YN                                     AS DENP_YN             " & vbNewLine _
    '                                        & ",OUTKA_L.PC_KB                                       AS PC_KB               " & vbNewLine _
    '                                        & ",OUTKA_L.ALL_PRINT_FLAG                              AS ALL_PRINT_FLAG      " & vbNewLine _
    '                                        & ",OUTKA_L.NIHUDA_FLAG                                 AS NIHUDA_FLAG         " & vbNewLine _
    '                                        & ",OUTKA_L.NHS_FLAG                                    AS NHS_FLAG            " & vbNewLine _
    '                                        & ",OUTKA_L.DENP_FLAG                                   AS DENP_FLAG           " & vbNewLine _
    '                                        & ",OUTKA_L.COA_FLAG                                    AS COA_FLAG            " & vbNewLine _
    '                                        & ",OUTKA_L.HOKOKU_FLAG                                 AS HOKOKU_FLAG         " & vbNewLine _
    '                                        & ",OUTKA_L.MATOME_PICK_FLAG                            AS MATOME_PICK_FLAG    " & vbNewLine _
    '                                        & ",OUTKA_L.LAST_PRINT_DATE                             AS LAST_PRINT_DATE     " & vbNewLine _
    '                                        & ",OUTKA_L.LAST_PRINT_TIME                             AS LAST_PRINT_TIME     " & vbNewLine _
    '                                        & ",OUTKA_L.SASZ_USER                                   AS SASZ_USER           " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKO_USER                                  AS OUTKO_USER          " & vbNewLine _
    '                                        & ",OUTKA_L.KEN_USER                                    AS KEN_USER            " & vbNewLine _
    '                                        & ",OUTKA_L.OUTKA_USER                                  AS OUTKA_USER          " & vbNewLine _
    '                                        & ",OUTKA_L.HOU_USER                                    AS HOU_USER            " & vbNewLine _
    '                                        & ",OUTKA_L.ORDER_TYPE                                  AS ORDER_TYPE          " & vbNewLine _
    '                                        & ",OUTKA_M.EDI_SET_NO                                  AS EDI_SET_NO          " & vbNewLine _
    '                                        & ",OUTKA_M.COA_YN                                      AS COA_YN_M            " & vbNewLine _
    '                                        & ",OUTKA_M.CUST_ORD_NO_DTL                             AS CUST_ORD_NO_DTL     " & vbNewLine _
    '                                        & ",OUTKA_M.BUYER_ORD_NO_DTL                            AS BUYER_ORD_NO_DTL    " & vbNewLine _
    '                                        & ",OUTKA_M.RSV_NO                                      AS RSV_NO              " & vbNewLine _
    '                                        & ",OUTKA_M.ALCTD_KB                                    AS ALCTD_KB            " & vbNewLine _
    '                                        & ",OUTKA_M.OUTKA_PKG_NB                                AS OUTKA_PKG_NB_M      " & vbNewLine _
    '                                        & ",OUTKA_M.IRIME                                       AS IRIME_M             " & vbNewLine _
    '                                        & ",OUTKA_M.IRIME_UT                                    AS IRIME_UT            " & vbNewLine _
    '                                        & ",OUTKA_M.OUTKA_M_PKG_NB                              AS OUTKA_M_PKG_NB      " & vbNewLine _
    '                                        & ",OUTKA_M.REMARK                                      AS OUTKA_M_REMARK      " & vbNewLine _
    '                                        & ",OUTKA_M.SIZE_KB                                     AS SIZE_KB             " & vbNewLine _
    '                                        & ",OUTKA_M.ZAIKO_KB                                    AS ZAIKO_KB            " & vbNewLine _
    '                                        & ",OUTKA_M.SOURCE_CD                                   AS SOURCE_CD           " & vbNewLine _
    '                                        & ",OUTKA_M.YELLOW_CARD                                 AS YELLOW_CARD         " & vbNewLine _
    '                                        & ",OUTKA_M.GOODS_CD_NRS_FROM                           AS GOODS_CD_NRS_FROM   " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_TTL_NB                                AS OUTKA_TTL_NB        " & vbNewLine _
    '                                        & ",OUTKA_S.OUTKA_TTL_QT                                AS OUTKA_TTL_QT        " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_CAN_NB                                AS ALCTD_CAN_NB        " & vbNewLine _
    '                                        & ",OUTKA_S.ALCTD_CAN_QT                                AS ALCTD_CAN_QT        " & vbNewLine _
    '                                        & ",OUTKA_S.SMPL_FLAG                                   AS SMPL_FLAG           " & vbNewLine _
    '                                        & ",ZAI.RSV_NO                                          AS RSV_NO_ZAI          " & vbNewLine _
    '                                        & ",ZAI.SERIAL_NO                                       AS SERIAL_NO_ZAI       " & vbNewLine _
    '                                        & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
    '                                        & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
    '                                        & ",ZAI.DEST_CD_P                                       AS DEST_CD_ZAI         " & vbNewLine _
    '                                        & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG_ZAI       " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",OUTKA_S.SYS_UPD_DATE                                AS SYS_UPD_DATE_OUT_S  " & vbNewLine _
    '                                        & ",OUTKA_S.SYS_UPD_TIME                                AS SYS_UPD_TIME_OUT_S  " & vbNewLine _
    '                                        & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
    '                                        & ",CUST.TAX_KB                                         AS TAX_KB              " & vbNewLine
    ''' <summary>
    ''' データ抽出用（他荷主在庫検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_OTHER As String = " SELECT                                                               " & vbNewLine _
                                            & " OUTKA_L.NRS_BR_CD                                   AS NRS_BR_CD           " & vbNewLine _
                                            & ",OUTKA_L.CUST_CD_L                                   AS CUST_CD_L_OUTKA     " & vbNewLine _
                                            & ",OUTKA_L.CUST_CD_M                                   AS CUST_CD_M_OUTKA     " & vbNewLine _
                                            & ",ZAI.CUST_CD_L                                       AS CUST_CD_L_ZAI       " & vbNewLine _
                                            & ",ZAI.CUST_CD_M                                       AS CUST_CD_M_ZAI       " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_NO_L                                  AS OUTKA_NO_L          " & vbNewLine _
                                            & ",OUTKA_M.OUTKA_NO_M                                  AS OUTKA_NO_M          " & vbNewLine _
                                            & ",OUTKA_S.OUTKA_NO_S                                  AS OUTKA_NO_S          " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_PLAN_DATE                             AS OUTKA_PLAN_DATE     " & vbNewLine _
                                            & ",OUTKA_L.WH_CD                                       AS WH_CD               " & vbNewLine _
                                            & ",OUTKA_S.ALCTD_QT                                    AS ALCTD_QT            " & vbNewLine _
                                            & ",GOODS.STD_IRIME_UT                                  AS STD_IRIME_UT        " & vbNewLine _
                                            & ",OUTKA_S.ALCTD_NB                                    AS ALCTD_NB            " & vbNewLine _
                                            & ",CUST.HOKAN_FREE_KIKAN                               AS HOKAN_FREE_KIKAN    " & vbNewLine _
                                            & ",OUTKA_M.GOODS_CD_NRS                                AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
                                            & ",OUTKA_S.IRIME                                       AS IRIME               " & vbNewLine _
                                            & ",OUTKA_S.BETU_WT                                     AS BETU_WT             " & vbNewLine _
                                            & ",OUTKA_S.TOU_NO                                      AS TOU_NO              " & vbNewLine _
                                            & ",OUTKA_S.SITU_NO                                     AS SITU_NO             " & vbNewLine _
                                            & ",OUTKA_S.ZONE_CD                                     AS ZONE_CD             " & vbNewLine _
                                            & ",OUTKA_S.LOCA                                        AS LOCA                " & vbNewLine _
                                            & ",OUTKA_S.LOT_NO                                      AS LOT_NO              " & vbNewLine _
                                            & ",OUTKA_S.SERIAL_NO                                   AS SERIAL_NO           " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
                                            & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
                                            & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
                                            & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
                                            & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
                                            & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
                                            & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
                                            & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
                                            & ",GOODS.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
                                            & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
                                            & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
                                            & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
                                            & ",OUTKA_L.DENP_NO                                     AS DENP_NO             " & vbNewLine _
                                            & ",OUTKA_L.ARR_KANRYO_INFO                             AS ARR_KANRYO_INFO     " & vbNewLine _
                                            & ",OUTKA_L.ARR_PLAN_TIME                               AS ARR_PLAN_TIME       " & vbNewLine _
                                            & ",OUTKA_L.HOKOKU_DATE                                 AS HOKOKU_DATE         " & vbNewLine _
                                            & ",OUTKA_L.SHIP_CD_L                                   AS SHIP_CD_L           " & vbNewLine _
                                            & ",OUTKA_L.SHIP_CD_M                                   AS SHIP_CD_M           " & vbNewLine _
                                            & ",OUTKA_L.DEST_CD                                     AS DEST_CD             " & vbNewLine _
                                            & ",OUTKA_L.DEST_AD_3                                   AS DEST_AD_3           " & vbNewLine _
                                            & ",OUTKA_L.DEST_TEL                                    AS DEST_TEL            " & vbNewLine _
                                            & ",OUTKA_L.NHS_REMARK                                  AS NHS_REMARK          " & vbNewLine _
                                            & ",OUTKA_L.SP_NHS_KB                                   AS SP_NHS_KB           " & vbNewLine _
                                            & ",OUTKA_L.COA_YN                                      AS COA_YN              " & vbNewLine _
                                            & ",OUTKA_L.CUST_ORD_NO                                 AS CUST_ORD_NO         " & vbNewLine _
                                            & ",OUTKA_L.BUYER_ORD_NO                                AS BUYER_ORD_NO        " & vbNewLine _
                                            & ",OUTKA_L.REMARK                                      AS OUTKA_L_REMARK      " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_PKG_NB                                AS OUTKA_PKG_NB        " & vbNewLine _
                                            & ",OUTKA_L.DENP_YN                                     AS DENP_YN             " & vbNewLine _
                                            & ",OUTKA_L.PC_KB                                       AS PC_KB               " & vbNewLine _
                                            & ",OUTKA_L.ALL_PRINT_FLAG                              AS ALL_PRINT_FLAG      " & vbNewLine _
                                            & ",OUTKA_L.NIHUDA_FLAG                                 AS NIHUDA_FLAG         " & vbNewLine _
                                            & ",OUTKA_L.NHS_FLAG                                    AS NHS_FLAG            " & vbNewLine _
                                            & ",OUTKA_L.DENP_FLAG                                   AS DENP_FLAG           " & vbNewLine _
                                            & ",OUTKA_L.COA_FLAG                                    AS COA_FLAG            " & vbNewLine _
                                            & ",OUTKA_L.HOKOKU_FLAG                                 AS HOKOKU_FLAG         " & vbNewLine _
                                            & ",OUTKA_L.MATOME_PICK_FLAG                            AS MATOME_PICK_FLAG    " & vbNewLine _
                                            & ",OUTKA_L.LAST_PRINT_DATE                             AS LAST_PRINT_DATE     " & vbNewLine _
                                            & ",OUTKA_L.LAST_PRINT_TIME                             AS LAST_PRINT_TIME     " & vbNewLine _
                                            & ",OUTKA_L.SASZ_USER                                   AS SASZ_USER           " & vbNewLine _
                                            & ",OUTKA_L.OUTKO_USER                                  AS OUTKO_USER          " & vbNewLine _
                                            & ",OUTKA_L.KEN_USER                                    AS KEN_USER            " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_USER                                  AS OUTKA_USER          " & vbNewLine _
                                            & ",OUTKA_L.HOU_USER                                    AS HOU_USER            " & vbNewLine _
                                            & ",OUTKA_L.ORDER_TYPE                                  AS ORDER_TYPE          " & vbNewLine _
                                            & ",OUTKA_M.EDI_SET_NO                                  AS EDI_SET_NO          " & vbNewLine _
                                            & ",OUTKA_M.COA_YN                                      AS COA_YN_M            " & vbNewLine _
                                            & ",OUTKA_M.CUST_ORD_NO_DTL                             AS CUST_ORD_NO_DTL     " & vbNewLine _
                                            & ",OUTKA_M.BUYER_ORD_NO_DTL                            AS BUYER_ORD_NO_DTL    " & vbNewLine _
                                            & ",OUTKA_M.RSV_NO                                      AS RSV_NO              " & vbNewLine _
                                            & ",OUTKA_M.ALCTD_KB                                    AS ALCTD_KB            " & vbNewLine _
                                            & ",OUTKA_M.OUTKA_PKG_NB                                AS OUTKA_PKG_NB_M      " & vbNewLine _
                                            & ",OUTKA_M.IRIME                                       AS IRIME_M             " & vbNewLine _
                                            & ",OUTKA_M.IRIME_UT                                    AS IRIME_UT            " & vbNewLine _
                                            & ",OUTKA_M.OUTKA_M_PKG_NB                              AS OUTKA_M_PKG_NB      " & vbNewLine _
                                            & ",OUTKA_M.REMARK                                      AS OUTKA_M_REMARK      " & vbNewLine _
                                            & ",OUTKA_M.SIZE_KB                                     AS SIZE_KB             " & vbNewLine _
                                            & ",OUTKA_M.ZAIKO_KB                                    AS ZAIKO_KB            " & vbNewLine _
                                            & ",OUTKA_M.SOURCE_CD                                   AS SOURCE_CD           " & vbNewLine _
                                            & ",OUTKA_M.YELLOW_CARD                                 AS YELLOW_CARD         " & vbNewLine _
                                            & ",OUTKA_M.GOODS_CD_NRS_FROM                           AS GOODS_CD_NRS_FROM   " & vbNewLine _
                                            & ",OUTKA_S.OUTKA_TTL_NB                                AS OUTKA_TTL_NB        " & vbNewLine _
                                            & ",OUTKA_S.OUTKA_TTL_QT                                AS OUTKA_TTL_QT        " & vbNewLine _
                                            & ",OUTKA_S.ALCTD_CAN_NB                                AS ALCTD_CAN_NB        " & vbNewLine _
                                            & ",OUTKA_S.ALCTD_CAN_QT                                AS ALCTD_CAN_QT        " & vbNewLine _
                                            & ",OUTKA_S.SMPL_FLAG                                   AS SMPL_FLAG           " & vbNewLine _
                                            & ",ZAI.RSV_NO                                          AS RSV_NO_ZAI          " & vbNewLine _
                                            & ",ZAI.SERIAL_NO                                       AS SERIAL_NO_ZAI       " & vbNewLine _
                                            & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
                                            & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
                                            & ",ZAI.DEST_CD_P                                       AS DEST_CD_ZAI         " & vbNewLine _
                                            & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG_ZAI       " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",OUTKA_S.SYS_UPD_DATE                                AS SYS_UPD_DATE_OUT_S  " & vbNewLine _
                                            & ",OUTKA_S.SYS_UPD_TIME                                AS SYS_UPD_TIME_OUT_S  " & vbNewLine _
                                            & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
                                            & ",CUST.TAX_KB                                         AS TAX_KB              " & vbNewLine _
                                            & ",OUTKA_L.DEST_KB                                     AS DEST_KB             " & vbNewLine _
                                            & ",OUTKA_L.DEST_NM                                     AS DEST_NM             " & vbNewLine _
                                            & ",OUTKA_L.DEST_AD_1                                   AS DEST_AD_1           " & vbNewLine _
                                            & ",OUTKA_L.DEST_AD_2                                   AS DEST_AD_2           " & vbNewLine
    'END YANAI 要望番号510
    'END YANAI 要望番号589

    ''' <summary>
    ''' FROM（他荷主在庫検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OTHER As String = "FROM                                             " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L OUTKA_L                               " & vbNewLine _
                                         & "INNER JOIN                                                " & vbNewLine _
                                         & "(                                                         " & vbNewLine _
                                         & "SELECT                                                    " & vbNewLine _
                                         & " OUTKA_S2.NRS_BR_CD                                       " & vbNewLine _
                                         & ",OUTKA_S2.OUTKA_NO_L                                      " & vbNewLine _
                                         & ",OUTKA_S2.OUTKA_NO_M                                      " & vbNewLine _
                                         & ",OUTKA_S2.OUTKA_NO_S                                      " & vbNewLine _
                                         & ",OUTKA_S2.ZAI_REC_NO                                      " & vbNewLine _
                                         & ",OUTKA_S2.ALCTD_QT                                        " & vbNewLine _
                                         & ",OUTKA_S2.ALCTD_NB                                        " & vbNewLine _
                                         & ",OUTKA_S2.IRIME                                           " & vbNewLine _
                                         & ",OUTKA_S2.BETU_WT                                         " & vbNewLine _
                                         & ",OUTKA_S2.TOU_NO                                          " & vbNewLine _
                                         & ",OUTKA_S2.SITU_NO                                         " & vbNewLine _
                                         & ",OUTKA_S2.ZONE_CD                                         " & vbNewLine _
                                         & ",OUTKA_S2.LOCA                                            " & vbNewLine _
                                         & ",OUTKA_S2.LOT_NO                                          " & vbNewLine _
                                         & ",OUTKA_S2.SERIAL_NO                                       " & vbNewLine _
                                         & ",OUTKA_S2.OUTKA_TTL_NB                                    " & vbNewLine _
                                         & ",OUTKA_S2.OUTKA_TTL_QT                                    " & vbNewLine _
                                         & ",OUTKA_S2.ALCTD_CAN_NB                                    " & vbNewLine _
                                         & ",OUTKA_S2.ALCTD_CAN_QT                                    " & vbNewLine _
                                         & ",OUTKA_S2.SMPL_FLAG                                       " & vbNewLine _
                                         & ",OUTKA_S2.SYS_UPD_DATE                                    " & vbNewLine _
                                         & ",OUTKA_S2.SYS_UPD_TIME                                    " & vbNewLine _
                                         & "FROM                                                      " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_S OUTKA_S2                              " & vbNewLine _
                                         & "WHERE OUTKA_S2.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                         & "AND OUTKA_S2.OUTKA_NO_L = @INOUTKA_NO_L                   " & vbNewLine _
                                         & "AND OUTKA_S2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                         & ") OUTKA_S                                                 " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "OUTKA_S.NRS_BR_CD = OUTKA_L.NRS_BR_CD                     " & vbNewLine _
                                         & "AND OUTKA_S.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L               " & vbNewLine _
                                         & "INNER JOIN                                                " & vbNewLine _
                                         & "(                                                         " & vbNewLine _
                                         & "SELECT                                                    " & vbNewLine _
                                         & " ZAI2.NRS_BR_CD                                           " & vbNewLine _
                                         & ",ZAI2.CUST_CD_L                                           " & vbNewLine _
                                         & ",ZAI2.CUST_CD_M                                           " & vbNewLine _
                                         & ",ZAI2.GOODS_COND_KB_1                                     " & vbNewLine _
                                         & ",ZAI2.GOODS_COND_KB_2                                     " & vbNewLine _
                                         & ",ZAI2.GOODS_COND_KB_3                                     " & vbNewLine _
                                         & ",ZAI2.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",ZAI2.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI2.OFB_KB                                              " & vbNewLine _
                                         & ",ZAI2.SPD_KB                                              " & vbNewLine _
                                         & ",ZAI2.REMARK                                              " & vbNewLine _
                                         & ",ZAI2.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI2.ZAI_REC_NO                                          " & vbNewLine _
                                         & ",ZAI2.INKA_NO_L                                           " & vbNewLine _
                                         & ",ZAI2.INKA_NO_M                                           " & vbNewLine _
                                         & ",ZAI2.INKA_NO_S                                           " & vbNewLine _
                                         & ",ZAI2.RSV_NO                                              " & vbNewLine _
                                         & ",ZAI2.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI2.HOKAN_YN                                            " & vbNewLine _
                                         & ",ZAI2.REMARK_OUT                                          " & vbNewLine _
                                         & ",ZAI2.DEST_CD_P                                           " & vbNewLine _
                                         & ",ZAI2.SMPL_FLAG                                           " & vbNewLine _
                                         & ",ZAI2.SYS_UPD_DATE                                        " & vbNewLine _
                                         & ",ZAI2.SYS_UPD_TIME                                        " & vbNewLine _
                                         & "FROM                                                      " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS ZAI2                                  " & vbNewLine _
                                         & "WHERE                                                     " & vbNewLine _
                                         & "ZAI2.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                         & "AND ZAI2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                         & ") ZAI                                                     " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "OUTKA_L.NRS_BR_CD = OUTKA_S.NRS_BR_CD                     " & vbNewLine _
                                         & "AND OUTKA_L.OUTKA_NO_L = OUTKA_S.OUTKA_NO_L               " & vbNewLine _
                                         & "AND OUTKA_S.NRS_BR_CD = ZAI.NRS_BR_CD                     " & vbNewLine _
                                         & "AND OUTKA_S.ZAI_REC_NO = ZAI.ZAI_REC_NO                   " & vbNewLine _
                                         & "AND OUTKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                     " & vbNewLine _
                                         & "AND (OUTKA_L.CUST_CD_L <> ZAI.CUST_CD_L                   " & vbNewLine _
                                         & "OR  OUTKA_L.CUST_CD_M <> ZAI.CUST_CD_M)                   " & vbNewLine _
                                         & "INNER JOIN                                                " & vbNewLine _
                                         & "(                                                         " & vbNewLine _
                                         & "SELECT                                                    " & vbNewLine _
                                         & " OUTKA_M2.NRS_BR_CD                                       " & vbNewLine _
                                         & ",OUTKA_M2.OUTKA_NO_L                                      " & vbNewLine _
                                         & ",OUTKA_M2.OUTKA_NO_M                                      " & vbNewLine _
                                         & ",OUTKA_M2.GOODS_CD_NRS                                    " & vbNewLine _
                                         & ",OUTKA_M2.EDI_SET_NO                                      " & vbNewLine _
                                         & ",OUTKA_M2.COA_YN                                          " & vbNewLine _
                                         & ",OUTKA_M2.CUST_ORD_NO_DTL                                 " & vbNewLine _
                                         & ",OUTKA_M2.BUYER_ORD_NO_DTL                                " & vbNewLine _
                                         & ",OUTKA_M2.RSV_NO                                          " & vbNewLine _
                                         & ",OUTKA_M2.ALCTD_KB                                        " & vbNewLine _
                                         & ",OUTKA_M2.OUTKA_PKG_NB                                    " & vbNewLine _
                                         & ",OUTKA_M2.IRIME                                           " & vbNewLine _
                                         & ",OUTKA_M2.IRIME_UT                                        " & vbNewLine _
                                         & ",OUTKA_M2.OUTKA_M_PKG_NB                                  " & vbNewLine _
                                         & ",OUTKA_M2.REMARK                                          " & vbNewLine _
                                         & ",OUTKA_M2.SIZE_KB                                         " & vbNewLine _
                                         & ",OUTKA_M2.ZAIKO_KB                                        " & vbNewLine _
                                         & ",OUTKA_M2.SOURCE_CD                                       " & vbNewLine _
                                         & ",OUTKA_M2.YELLOW_CARD                                     " & vbNewLine _
                                         & ",OUTKA_M2.GOODS_CD_NRS_FROM                               " & vbNewLine _
                                         & "FROM                                                      " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_M OUTKA_M2                              " & vbNewLine _
                                         & "WHERE OUTKA_M2.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                         & "AND OUTKA_M2.OUTKA_NO_L = @INOUTKA_NO_L                   " & vbNewLine _
                                         & "AND OUTKA_M2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                         & ") OUTKA_M                                                 " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "OUTKA_M.NRS_BR_CD = OUTKA_S.NRS_BR_CD                     " & vbNewLine _
                                         & "AND OUTKA_M.OUTKA_NO_L = OUTKA_S.OUTKA_NO_L               " & vbNewLine _
                                         & "AND OUTKA_M.OUTKA_NO_M = OUTKA_S.OUTKA_NO_M               " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "$LM_MST$..M_GOODS GOODS                                   " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "GOODS.NRS_BR_CD = OUTKA_M.NRS_BR_CD                       " & vbNewLine _
                                         & "AND GOODS.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS             " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "$LM_MST$..M_CUST CUST                                     " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "CUST.NRS_BR_CD = OUTKA_L.NRS_BR_CD                        " & vbNewLine _
                                         & "AND CUST.CUST_CD_L = OUTKA_L.CUST_CD_L                    " & vbNewLine _
                                         & "AND CUST.CUST_CD_M = OUTKA_L.CUST_CD_M                    " & vbNewLine _
                                         & "AND CUST.CUST_CD_S = '00'                                 " & vbNewLine _
                                         & "AND CUST.CUST_CD_SS = '00'                                " & vbNewLine

    ''' <summary>
    ''' FROM（他荷主在庫検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_OTHER As String = "WHERE                                           " & vbNewLine _
                                         & "       OUTKA_L.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                         & "AND    OUTKA_L.OUTKA_NO_L = @INOUTKA_NO_L                 " & vbNewLine _
                                         & "AND    OUTKA_L.SYS_DEL_FLG = '0'                          " & vbNewLine

#End Region

#Region "入荷チェックデータ検索"

    ''' <summary>
    ''' SELECT（入荷チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_INKA As String = " SELECT                                                               " & vbNewLine _
                                            & "  INKA_S.INKA_NO_L                                   AS INKA_NO_L           " & vbNewLine _
                                            & ", INKA_S.ZAI_REC_NO                                  AS ZAI_REC_NO          " & vbNewLine _
                                            & ", INKA_S.LOT_NO                                      AS LOT_NO              " & vbNewLine _
                                            & ", INKA_M.LOT_CTL_KB                                  AS LOT_CTL_KB          " & vbNewLine


    ''' <summary>
    ''' FROM（入荷チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CHECK_INKA As String = "FROM                                                                    " & vbNewLine _
                                                  & "$LM_TRN$..B_INKA_S INKA_S                                                    " & vbNewLine _
                                                  & "  LEFT JOIN                                                                  " & vbNewLine _
                                                  & "   (                                                                         " & vbNewLine _
                                                  & "     SELECT                                                                  " & vbNewLine _
                                                  & "         INKA_M.NRS_BR_CD        AS NRS_BR_CD                                " & vbNewLine _
                                                  & "       , INKA_M.INKA_NO_L        AS INKA_NO_L                                " & vbNewLine _
                                                  & "       , INKA_M.INKA_NO_M        AS INKA_NO_M                                " & vbNewLine _
                                                  & "       , GOODS.LOT_CTL_KB        AS LOT_CTL_KB                               " & vbNewLine _
                                                  & "     FROM                                                                    " & vbNewLine _
                                                  & "       $LM_TRN$..B_INKA_M INKA_M                                             " & vbNewLine _
                                                  & "     LEFT JOIN                                                               " & vbNewLine _
                                                  & "       $LM_MST$..M_GOODS  GOODS                                              " & vbNewLine _
                                                  & "     ON                                                                      " & vbNewLine _
                                                  & "       GOODS.NRS_BR_CD   = @NRS_BR_CD                                        " & vbNewLine _
                                                  & "     AND                                                                     " & vbNewLine _
                                                  & "       INKA_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                              " & vbNewLine _
                                                  & "     WHERE                                                                   " & vbNewLine _
                                                  & "       INKA_M.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                                  & "     AND                                                                     " & vbNewLine _
                                                  & "       INKA_M.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                                  & "   ) INKA_M                                                                  " & vbNewLine _
                                                  & " ON                                                                          " & vbNewLine _
                                                  & "   INKA_S.NRS_BR_CD = INKA_M.NRS_BR_CD                                       " & vbNewLine _
                                                  & " AND                                                                         " & vbNewLine _
                                                  & "   INKA_S.INKA_NO_L = INKA_M.INKA_NO_L                                       " & vbNewLine _
                                                  & " AND                                                                         " & vbNewLine _
                                                  & "   INKA_S.INKA_NO_M = INKA_M.INKA_NO_M                                       " & vbNewLine

    ''' <summary>
    ''' FROM（入荷チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_CHECK_INKA As String = "WHERE                                      " & vbNewLine _
                                         & "       INKA_S.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                         & "AND    INKA_S.INKA_NO_L IN (@INOUTKA_NO_L)                " & vbNewLine _
                                         & "AND    INKA_S.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "AND    (INKA_S.ZAI_REC_NO = ''                            " & vbNewLine _
                                         & "OR     INKA_S.LOT_NO = '')                                " & vbNewLine

    ''' <summary>
    ''' ORDER BY（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_CHECK_INKA As String = "ORDER BY                               " & vbNewLine _
                                         & " INKA_S.INKA_NO_L                                        " & vbNewLine

#End Region

#Region "在庫チェックデータ検索"

    ''' <summary>
    ''' SELECT（入荷チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_ZAI As String = " SELECT                                                               " & vbNewLine _
                                            & " ZAI.INKA_NO_L                                      AS INKA_NO_L           " & vbNewLine _
                                            & ",ZAI.INKO_DATE                                      AS INKO_DATE           " & vbNewLine _
                                            & ",@INOUTKA_NO_L                                      AS INOUTKA_NO_L        " & vbNewLine

    ''' <summary>
    ''' FROM（入荷チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CHECK_ZAI As String = "FROM                                         " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS ZAI                                   " & vbNewLine

    ''' <summary>
    ''' FROM（入荷チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_CHECK_ZAI As String = "WHERE                                      " & vbNewLine _
                                         & "       ZAI.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
                                         & "AND    ZAI.ZAI_REC_NO IN                                 " & vbNewLine _
                                         & "(SELECT OUTKA.ZAI_REC_NO                                 " & vbNewLine _
                                         & " FROM $LM_TRN$..C_OUTKA_S OUTKA                          " & vbNewLine _
                                         & " WHERE OUTKA.OUTKA_NO_L  IN (@INOUTKA_NO_L)              " & vbNewLine _
                                         & " AND OUTKA.SYS_DEL_FLG  = '0'                            " & vbNewLine _
                                         & ")                                                        " & vbNewLine _
                                         & "AND    ZAI.SYS_DEL_FLG = '0'                             " & vbNewLine

    ''' <summary>
    ''' GROUP BY（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_CHECK_ZAI As String = "GROUP BY                                " & vbNewLine _
                                         & " ZAI.INKA_NO_L                                           " & vbNewLine _
                                         & ",ZAI.INKO_DATE                                           " & vbNewLine

    ''' <summary>
    ''' ORDER BY（入荷検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_CHECK_ZAI As String = "ORDER BY                                " & vbNewLine _
                                         & " ZAI.INKA_NO_L                                           " & vbNewLine

#End Region

#Region "入荷進捗区分チェックデータ検索"

    ''' <summary>
    ''' SELECT（入荷進捗区分チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_INKA_STATE As String = " SELECT                                                         " & vbNewLine _
                                            & " INKA_L.INKA_NO_L                                    AS INOUTKA_NO_L        " & vbNewLine _
                                            & ",INKA_L.INKA_STATE_KB                                AS STATE_KB            " & vbNewLine _
                                            & ",SOKO.INKA_UKE_PRT_YN                                AS INKA_UKE_PRT_YN     " & vbNewLine _
                                            & ",SOKO.INKA_KENPIN_YN                                 AS INKA_KENPIN_YN      " & vbNewLine _
                                            & ",SOKO.INKA_KAKUNIN_YN                                AS INKA_KAKUNIN_YN     " & vbNewLine _
                                            & ",SOKO.OUTKA_SASHIZU_PRT_YN                           AS OUTKA_SASHIZU_PRT_YN " & vbNewLine _
                                            & ",SOKO.OUTOKA_KANRYO_YN                               AS OUTOKA_KANRYO_YN    " & vbNewLine _
                                            & ",SOKO.OUTKA_KENPIN_YN                                AS OUTKA_KENPIN_YN     " & vbNewLine _
                                            & ",SOKO.OUTKA_INFO_YN                                  AS OUTKA_INFO_YN       " & vbNewLine _
                                            & ",''                                                  AS BACKLOG_NB          " & vbNewLine

    ''' <summary>
    ''' FROM（入荷進捗区分チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CHECK_INKA_STATE As String = "FROM                                  " & vbNewLine _
                                         & "$LM_TRN$..B_INKA_L INKA_L                                 " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_SOKO    SOKO                        " & vbNewLine _
                                         & "ON     SOKO.NRS_BR_CD = INKA_L.NRS_BR_CD                  " & vbNewLine _
                                         & "AND    SOKO.WH_CD = INKA_L.WH_CD                          " & vbNewLine

#End Region

#Region "出荷進捗区分チェックデータ検索"

    ''' <summary>
    ''' SELECT（出荷進捗区分チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_OUTKA_STATE As String = " SELECT                                                        " & vbNewLine _
                                            & " OUTKA_L.OUTKA_NO_L                                  AS INOUTKA_NO_L        " & vbNewLine _
                                            & ",OUTKA_L.OUTKA_STATE_KB                              AS STATE_KB            " & vbNewLine _
                                            & ",SOKO.INKA_UKE_PRT_YN                                AS INKA_UKE_PRT_YN     " & vbNewLine _
                                            & ",SOKO.INKA_KENPIN_YN                                 AS INKA_KENPIN_YN      " & vbNewLine _
                                            & ",SOKO.INKA_KAKUNIN_YN                                AS INKA_KAKUNIN_YN     " & vbNewLine _
                                            & ",SOKO.OUTKA_SASHIZU_PRT_YN                           AS OUTKA_SASHIZU_PRT_YN " & vbNewLine _
                                            & ",SOKO.OUTOKA_KANRYO_YN                               AS OUTOKA_KANRYO_YN    " & vbNewLine _
                                            & ",SOKO.OUTKA_KENPIN_YN                                AS OUTKA_KENPIN_YN     " & vbNewLine _
                                            & ",SOKO.OUTKA_INFO_YN                                  AS OUTKA_INFO_YN       " & vbNewLine _
                                            & ",SUM(OUTKA_M.BACKLOG_NB)                             AS BACKLOG_NB          " & vbNewLine

    ''' <summary>
    ''' FROM（出荷進捗区分チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CHECK_OUTKA_STATE As String = "FROM                                 " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L OUTKA_L                               " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_SOKO    SOKO                        " & vbNewLine _
                                         & "ON     SOKO.NRS_BR_CD = OUTKA_L.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    SOKO.WH_CD = OUTKA_L.WH_CD                         " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..C_OUTKA_M    OUTKA_M                  " & vbNewLine _
                                         & "ON     OUTKA_M.NRS_BR_CD = OUTKA_L.NRS_BR_CD              " & vbNewLine _
                                         & "AND    OUTKA_M.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L            " & vbNewLine _
                                         & "AND    OUTKA_M.SYS_DEL_FLG = '0'                          " & vbNewLine

    ''' <summary>
    ''' GROUP BY（出荷進捗区分チェックデータ検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_CHECK_OUTKA_STATE As String = "GROUP BY                         " & vbNewLine _
                                         & " OUTKA_L.OUTKA_NO_L                                       " & vbNewLine _
                                         & ",OUTKA_L.OUTKA_STATE_KB                                   " & vbNewLine _
                                         & ",SOKO.INKA_UKE_PRT_YN                                     " & vbNewLine _
                                         & ",SOKO.INKA_KENPIN_YN                                      " & vbNewLine _
                                         & ",SOKO.INKA_KAKUNIN_YN                                     " & vbNewLine _
                                         & ",SOKO.OUTKA_SASHIZU_PRT_YN                                " & vbNewLine _
                                         & ",SOKO.OUTOKA_KANRYO_YN                                    " & vbNewLine _
                                         & ",SOKO.OUTKA_KENPIN_YN                                     " & vbNewLine _
                                         & ",SOKO.OUTKA_INFO_YN                                       " & vbNewLine

#End Region

#Region "M_CUST"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CUST As String = " SELECT                                             " & vbNewLine _
                                            & " CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION " & vbNewLine _
                                            & ",INKAL.INKA_NO_L AS INKA_NO_L                              " & vbNewLine

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_CUST As String = "FROM                                                     " & vbNewLine _
                                         & "$LM_TRN$..B_INKA_L INKAL                                  " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "$LM_TRN$..B_INKA_M INKAM                                  " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                         " & vbNewLine _
                                         & "AND                                                       " & vbNewLine _
                                         & "INKAM.INKA_NO_L = INKAL.INKA_NO_L                         " & vbNewLine _
                                         & "AND                                                       " & vbNewLine _
                                         & "INKAM.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "$LM_MST$..M_GOODS GOODS                                   " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                         " & vbNewLine _
                                         & "AND                                                       " & vbNewLine _
                                         & "GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                   " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "$LM_MST$..M_CUST CUST                                     " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "CUST.NRS_BR_CD = GOODS.NRS_BR_CD                          " & vbNewLine _
                                         & "AND                                                       " & vbNewLine _
                                         & "CUST.CUST_CD_L = GOODS.CUST_CD_L                          " & vbNewLine _
                                         & "AND                                                       " & vbNewLine _
                                         & "CUST.CUST_CD_M = GOODS.CUST_CD_M                          " & vbNewLine _
                                         & "AND                                                       " & vbNewLine _
                                         & "CUST.CUST_CD_S = GOODS.CUST_CD_S                          " & vbNewLine _
                                         & "AND                                                       " & vbNewLine _
                                         & "CUST.CUST_CD_SS = GOODS.CUST_CD_SS                        " & vbNewLine

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_CUST As String = "WHERE                                                " & vbNewLine _
                                         & "    INKAL.NRS_BR_CD             = @NRS_BR_CD               " & vbNewLine _
                                         '& "AND INKAL.INKA_NO_L             IN (@INKA_NO_L)         " & vbNewLine


#End Region

#Region "作業指示検索"

    ''' <summary>
    ''' カウント用（作業指示検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SAGYO_SIJI As String = "" _
    & " SELECT COUNT(CNT.SAGYO_SIJI_NO) AS SELECT_CNT FROM (SELECT SIJI.SAGYO_SIJI_NO  " & vbNewLine


    ''' <summary>
    ''' SELECT（作業指示検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SAGYO_SIJI As String = "" _
    & " SELECT                                                                         " & vbNewLine _
    & " SIJI.SAGYO_SIJI_NO                            AS INOUTKA_NO_L                  " & vbNewLine _
    & ",SIJI.SAGYO_SIJI_DATE                          AS INOUTKA_DATE                  " & vbNewLine _
    & ",''                                            AS INOUTKA_ORD_NO                " & vbNewLine _
    & ",SUSER.USER_NM                                 AS TANTO_USER                    " & vbNewLine _
    & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M      AS CUST_NM                       " & vbNewLine _
    & ",''                                            AS DEST_NM                       " & vbNewLine _
    & ",''                                            AS PKG_NB                        " & vbNewLine _
    & ",SIJI.NRS_BR_CD                                AS NRS_BR_CD                     " & vbNewLine _
    & ",''                                            AS CHK_INKA_STATE_KB             " & vbNewLine _
    & ",SAGYO.CUST_CD_L                               AS CUST_CD_L                     " & vbNewLine _
    & ",''                                            AS SCNT                          " & vbNewLine _
    & ",SIJI.SYS_UPD_DATE                             AS SYS_UPD_DATE                  " & vbNewLine _
    & ",SIJI.SYS_UPD_TIME                             AS SYS_UPD_TIME                  " & vbNewLine

    ''' <summary>
    ''' FROM（作業指示検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SAGYO_SIJI As String = "" _
    & "FROM $LM_TRN$..E_SAGYO_SIJI SIJI                      " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..E_SAGYO SAGYO                     " & vbNewLine _
    & "ON SIJI.SAGYO_SIJI_NO = SAGYO.SAGYO_SIJI_NO           " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_CUST MCUST                      " & vbNewLine _
    & "ON  MCUST.NRS_BR_CD  = SAGYO.NRS_BR_CD                " & vbNewLine _
    & "AND MCUST.CUST_CD_L  = SAGYO.CUST_CD_L                " & vbNewLine _
    & "AND MCUST.CUST_CD_M  = SAGYO.CUST_CD_M                " & vbNewLine _
    & "AND MCUST.CUST_CD_S  = '00'                           " & vbNewLine _
    & "AND MCUST.CUST_CD_SS = '00'                           " & vbNewLine _
    & "LEFT JOIN $LM_MST$..S_USER SUSER                      " & vbNewLine _
    & "ON SUSER.USER_CD = MCUST.TANTO_CD                     " & vbNewLine

    ''' <summary>
    ''' GROUP BY（作業指示検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_SAGYO_SIJI As String = "" _
    & "GROUP BY                                              " & vbNewLine _
    & " SIJI.SAGYO_SIJI_NO                                   " & vbNewLine _
    & ",SIJI.SAGYO_SIJI_DATE                                 " & vbNewLine _
    & ",SUSER.USER_NM                                        " & vbNewLine _
    & ",MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M             " & vbNewLine _
    & ",SAGYO.CUST_CD_L                                      " & vbNewLine _
    & ",SIJI.NRS_BR_CD                                       " & vbNewLine _
    & ",SIJI.SYS_UPD_DATE                                    " & vbNewLine _
    & ",SIJI.SYS_UPD_TIME                                    " & vbNewLine

    ''' <summary>
    ''' ORDER BY（作業指示検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_SAGYO_SIJI As String = "" _
    & "ORDER BY                                              " & vbNewLine _
    & " SIJI.SAGYO_SIJI_NO                                   " & vbNewLine _
    & ",SIJI.SAGYO_SIJI_DATE                                 " & vbNewLine

    ''' <summary>
    ''' COUNT時使用（作業指示検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_END_SAGYO_SIJI As String = ") CNT                                          "

#End Region

#Region "Rapidus次回分納情報取得"

    ''' <summary>
    ''' Rapidus次回分納情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_JIKAI_BUNNOU_INFO As String = "" _
            & "SELECT                                                                            " & vbNewLine _
            & "      C_OUTKA_L.NRS_BR_CD                                                         " & vbNewLine _
            & "    , H_OUTKAEDI_M.EDI_CTL_NO                                                     " & vbNewLine _
            & "    , H_OUTKAEDI_M.EDI_CTL_NO_CHU                                                 " & vbNewLine _
            & "    , H_OUTKAEDI_M.OUTKA_TTL_NB - C_OUTKA_M.OUTKA_TTL_NB AS JIKAI_BUNNOU_NB       " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.CRT_DATE                                                " & vbNewLine _
            & "    , SUBSTRING(H_OUTKAEDI_DTL_RAPI.[FILE_NAME], 1,                               " & vbNewLine _
            & "        CASE                                                                      " & vbNewLine _
            & "            WHEN CHARINDEX(@TEMPLATE_PREFIX, H_OUTKAEDI_DTL_RAPI.[FILE_NAME]) > 0 " & vbNewLine _
            & "            THEN CHARINDEX(@TEMPLATE_PREFIX, H_OUTKAEDI_DTL_RAPI.[FILE_NAME]) - 1 " & vbNewLine _
            & "            ELSE LEN(H_OUTKAEDI_DTL_RAPI.[FILE_NAME])                             " & vbNewLine _
            & "        END) AS FILE_NAME                                                         " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.REC_NO                                                  " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.GYO_NO                                                  " & vbNewLine _
            & "    , CASE WHEN ISNUMERIC(H_OUTKAEDI_DTL_RAPI.RECORD_STATUS) = 1                  " & vbNewLine _
            & "        THEN CAST(H_OUTKAEDI_DTL_RAPI.RECORD_STATUS AS NUMERIC(20))               " & vbNewLine _
            & "        ELSE 1                                                                    " & vbNewLine _
            & "      END + 1 AS JIKAI_BUNNOU_KAISU                                               " & vbNewLine _
            & "    , @TEMPLATE_PREFIX AS TEMPLATE_PREFIX                                         " & vbNewLine _
            & "    , @TEMPLATE_SUFFIX AS TEMPLATE_SUFFIX                                         " & vbNewLine _
            & "FROM                                                                              " & vbNewLine _
            & "    $LM_TRN$..C_OUTKA_L                                                           " & vbNewLine _
            & "LEFT JOIN                                                                         " & vbNewLine _
            & "    $LM_TRN$..C_OUTKA_M                                                           " & vbNewLine _
            & "        ON  C_OUTKA_M.NRS_BR_CD = C_OUTKA_L.NRS_BR_CD                             " & vbNewLine _
            & "        AND C_OUTKA_M.OUTKA_NO_L = C_OUTKA_L.OUTKA_NO_L                           " & vbNewLine _
            & "        AND C_OUTKA_M.SYS_DEL_FLG = '0'                                           " & vbNewLine _
            & "LEFT JOIN                                                                         " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_M                                                        " & vbNewLine _
            & "        ON  H_OUTKAEDI_M.NRS_BR_CD = C_OUTKA_M.NRS_BR_CD                          " & vbNewLine _
            & "        AND H_OUTKAEDI_M.OUTKA_CTL_NO = C_OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
            & "        AND H_OUTKAEDI_M.OUTKA_CTL_NO_CHU = C_OUTKA_M.OUTKA_NO_M                  " & vbNewLine _
            & "        AND H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                        " & vbNewLine _
            & "LEFT JOIN                                                                         " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_DTL_RAPI                                                 " & vbNewLine _
            & "        ON  H_OUTKAEDI_DTL_RAPI.NRS_BR_CD = C_OUTKA_L.NRS_BR_CD                   " & vbNewLine _
            & "        AND H_OUTKAEDI_DTL_RAPI.EDI_CTL_NO = H_OUTKAEDI_M.EDI_CTL_NO              " & vbNewLine _
            & "        AND H_OUTKAEDI_DTL_RAPI.EDI_CTL_NO_CHU = H_OUTKAEDI_M.EDI_CTL_NO_CHU      " & vbNewLine _
            & "        AND H_OUTKAEDI_DTL_RAPI.DEL_KB <> '1'                                     " & vbNewLine _
            & "WHERE                                                                             " & vbNewLine _
            & "    C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
            & "AND C_OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L                                            " & vbNewLine _
            & "AND C_OUTKA_L.SYUBETU_KB = '60' -- 分納                                           " & vbNewLine _
            & "AND H_OUTKAEDI_M.OUTKA_TTL_NB > C_OUTKA_M.OUTKA_TTL_NB                            " & vbNewLine _
            & "AND C_OUTKA_L.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
            & "AND C_OUTKA_M.NRS_BR_CD IS NOT NULL                                               " & vbNewLine _
            & "AND H_OUTKAEDI_M.NRS_BR_CD IS NOT NULL                                            " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.NRS_BR_CD IS NOT NULL                                     " & vbNewLine _
            & ""

#End Region ' "Rapidus次回分納情報取得"

#Region "Rapidus次回分納情報 出荷指示EDIデータ 同一ファイル名件数 取得"

    ''' <summary>
    ''' Rapidus次回分納情報 出荷指示EDIデータ 同一ファイル名件数 取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAME_CRT_DATE_AND_FILE_NAME_CNT As String = "" _
            & "SELECT                                                                                                                                         " & vbNewLine _
            & "    COUNT(*) AS CNT                                                                                                                            " & vbNewLine _
            & "FROM                                                                                                                                           " & vbNewLine _
            & "   (                                                                                                                                           " & vbNewLine _
            & "    SELECT DISTINCT                                                                                                                            " & vbNewLine _
            & "          CRT_DATE                                                                                                                             " & vbNewLine _
            & "        , FILE_NAME                                                                                                                            " & vbNewLine _
            & "    FROM                                                                                                                                       " & vbNewLine _
            & "        $LM_TRN$..H_OUTKAEDI_DTL_RAPI                                                                                                          " & vbNewLine _
            & "    WHERE                                                                                                                                      " & vbNewLine _
            & "        H_OUTKAEDI_DTL_RAPI.CRT_DATE = @CRT_DATE                                                                                               " & vbNewLine _
            & "    AND H_OUTKAEDI_DTL_RAPI.FILE_NAME LIKE (@FILE_NAME + @TEMPLATE_PREFIX + CAST(@JIKAI_BUNNOU_KAISU AS VARCHAR(20)) + @TEMPLATE_SUFFIX) + '%' " & vbNewLine _
            & "    ) CRT_DATE_AND_FILE_NAME                                                                                                                   " & vbNewLine _
            & ""

#End Region ' "Rapidus次回分納情報 出荷指示EDIデータ 同一ファイル名件数 取得"

#End Region

#Region "INSERT"

#Region "M_COA"

    ''' <summary>
    ''' INSERT（M_COA）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_COA As String = "INSERT INTO                                            " & vbNewLine _
                                         & "$LM_MST$..M_COA                                          " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & "--ADD START 2018/12/18 要望管理003858                    " & vbNewLine _
                                         & ",INKA_DATE                                               " & vbNewLine _
                                         & ",INKA_DATE_VERS_FLG                                      " & vbNewLine _
                                         & "--ADD END   2018/12/18 要望管理003858                    " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",COA_LINK                                                " & vbNewLine _
                                         & ",COA_NAME                                                " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")                                                        " & vbNewLine

#End Region

#Region "E_SAGYO"

    ''' <summary>
    ''' INSERT（E_SAGYO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO                                         " & vbNewLine _
                                         & "$LM_TRN$..E_SAGYO                                        " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",SAGYO_REC_NO                                            " & vbNewLine _
                                         & ",SAGYO_COMP                                              " & vbNewLine _
                                         & ",SKYU_CHK                                                " & vbNewLine _
                                         & ",SAGYO_SIJI_NO                                           " & vbNewLine _
                                         & ",INOUTKA_NO_LM                                           " & vbNewLine _
                                         & ",WH_CD                                                   " & vbNewLine _
                                         & ",IOZS_KB                                                 " & vbNewLine _
                                         & ",SAGYO_CD                                                " & vbNewLine _
                                         & ",SAGYO_NM                                                " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & ",DEST_NM                                                 " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",GOODS_NM_NRS                                            " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",INV_TANI                                                " & vbNewLine _
                                         & ",SAGYO_NB                                                " & vbNewLine _
                                         & ",SAGYO_UP                                                " & vbNewLine _
                                         & ",SAGYO_GK                                                " & vbNewLine _
                                         & ",TAX_KB                                                  " & vbNewLine _
                                         & ",SEIQTO_CD                                               " & vbNewLine _
                                         & ",REMARK_ZAI                                              " & vbNewLine _
                                         & ",REMARK_SKYU                                             " & vbNewLine _
                                         & ",SAGYO_COMP_CD                                           " & vbNewLine _
                                         & ",SAGYO_COMP_DATE                                         " & vbNewLine _
                                         & ",DEST_SAGYO_FLG                                          " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")                                                        " & vbNewLine

#End Region

#Region "INKA_L"

    ''' <summary>
    ''' INSERT（INKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURIINKA_L As String = "INSERT INTO $LM_TRN$..B_INKA_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",FURI_NO                      " & vbNewLine _
                                              & ",INKA_TP                      " & vbNewLine _
                                              & ",INKA_KB                      " & vbNewLine _
                                              & ",INKA_STATE_KB                " & vbNewLine _
                                              & ",INKA_DATE                    " & vbNewLine _
                                              & ",WH_CD                        " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",INKA_PLAN_QT                 " & vbNewLine _
                                              & ",INKA_PLAN_QT_UT              " & vbNewLine _
                                              & ",INKA_TTL_NB                  " & vbNewLine _
                                              & ",BUYER_ORD_NO_L               " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_L          " & vbNewLine _
                                              & ",SEIQTO_CD                    " & vbNewLine _
                                              & ",TOUKI_HOKAN_YN               " & vbNewLine _
                                              & ",HOKAN_YN                     " & vbNewLine _
                                              & ",HOKAN_FREE_KIKAN             " & vbNewLine _
                                              & ",HOKAN_STR_DATE               " & vbNewLine _
                                              & ",NIYAKU_YN                    " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",REMARK_OUT                   " & vbNewLine _
                                              & ",CHECKLIST_PRT_DATE           " & vbNewLine _
                                              & ",CHECKLIST_PRT_USER           " & vbNewLine _
                                              & ",UKETSUKELIST_PRT_DATE        " & vbNewLine _
                                              & ",UKETSUKELIST_PRT_USER        " & vbNewLine _
                                              & ",UKETSUKE_DATE                " & vbNewLine _
                                              & ",UKETSUKE_USER                " & vbNewLine _
                                              & ",KEN_DATE                     " & vbNewLine _
                                              & ",KEN_USER                     " & vbNewLine _
                                              & ",INKO_DATE                    " & vbNewLine _
                                              & ",INKO_USER                    " & vbNewLine _
                                              & ",HOUKOKUSYO_PR_DATE           " & vbNewLine _
                                              & ",HOUKOKUSYO_PR_USER           " & vbNewLine _
                                              & ",UNCHIN_TP                    " & vbNewLine _
                                              & ",UNCHIN_KB                    " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@FURI_NO                     " & vbNewLine _
                                              & ",@INKA_TP                     " & vbNewLine _
                                              & ",@INKA_KB                     " & vbNewLine _
                                              & ",@INKA_STATE_KB               " & vbNewLine _
                                              & ",@INKA_DATE                   " & vbNewLine _
                                              & ",@WH_CD                       " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@INKA_PLAN_QT                " & vbNewLine _
                                              & ",@INKA_PLAN_QT_UT             " & vbNewLine _
                                              & ",@INKA_TTL_NB                 " & vbNewLine _
                                              & ",@BUYER_ORD_NO_L              " & vbNewLine _
                                              & ",@OUTKA_FROM_ORD_NO_L         " & vbNewLine _
                                              & ",@SEIQTO_CD                   " & vbNewLine _
                                              & ",@TOUKI_HOKAN_YN              " & vbNewLine _
                                              & ",@HOKAN_YN                    " & vbNewLine _
                                              & ",@HOKAN_FREE_KIKAN            " & vbNewLine _
                                              & ",@HOKAN_STR_DATE              " & vbNewLine _
                                              & ",@NIYAKU_YN                   " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@REMARK_OUT                  " & vbNewLine _
                                              & ",@CHECKLIST_PRT_DATE          " & vbNewLine _
                                              & ",@CHECKLIST_PRT_USER          " & vbNewLine _
                                              & ",@UKETSUKELIST_PRT_DATE       " & vbNewLine _
                                              & ",@UKETSUKELIST_PRT_USER       " & vbNewLine _
                                              & ",@UKETSUKE_DATE               " & vbNewLine _
                                              & ",@UKETSUKE_USER               " & vbNewLine _
                                              & ",@KEN_DATE                    " & vbNewLine _
                                              & ",@KEN_USER                    " & vbNewLine _
                                              & ",@INKO_DATE                   " & vbNewLine _
                                              & ",@INKO_USER                   " & vbNewLine _
                                              & ",@HOUKOKUSYO_PR_DATE          " & vbNewLine _
                                              & ",@HOUKOKUSYO_PR_USER          " & vbNewLine _
                                              & ",@UNCHIN_TP                   " & vbNewLine _
                                              & ",@UNCHIN_KB                   " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "INKA_M"

    ''' <summary>
    ''' INSERT（INKA_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURIINKA_M As String = "INSERT INTO $LM_TRN$..B_INKA_M" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",INKA_NO_M                    " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_M          " & vbNewLine _
                                              & ",BUYER_ORD_NO_M               " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",PRINT_SORT                   " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@INKA_NO_M                   " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@OUTKA_FROM_ORD_NO_M         " & vbNewLine _
                                              & ",@BUYER_ORD_NO_M              " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@PRINT_SORT                  " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "INKA_S"

    ''' <summary>
    ''' INSERT（INKA_S）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURIINKA_S As String = "INSERT INTO $LM_TRN$..B_INKA_S" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",INKA_NO_M                    " & vbNewLine _
                                              & ",INKA_NO_S                    " & vbNewLine _
                                              & ",ZAI_REC_NO                   " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",LOCA                         " & vbNewLine _
                                              & ",TOU_NO                       " & vbNewLine _
                                              & ",SITU_NO                      " & vbNewLine _
                                              & ",ZONE_CD                      " & vbNewLine _
                                              & ",KONSU                        " & vbNewLine _
                                              & ",HASU                         " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",SERIAL_NO                    " & vbNewLine _
                                              & ",GOODS_COND_KB_1              " & vbNewLine _
                                              & ",GOODS_COND_KB_2              " & vbNewLine _
                                              & ",GOODS_COND_KB_3              " & vbNewLine _
                                              & ",GOODS_CRT_DATE               " & vbNewLine _
                                              & ",LT_DATE                      " & vbNewLine _
                                              & ",SPD_KB                       " & vbNewLine _
                                              & ",OFB_KB                       " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",ALLOC_PRIORITY               " & vbNewLine _
                                              & ",REMARK_OUT                   " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@INKA_NO_M                   " & vbNewLine _
                                              & ",@INKA_NO_S                   " & vbNewLine _
                                              & ",@ZAI_REC_NO                  " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@LOCA                        " & vbNewLine _
                                              & ",@TOU_NO                      " & vbNewLine _
                                              & ",@SITU_NO                     " & vbNewLine _
                                              & ",@ZONE_CD                     " & vbNewLine _
                                              & ",@KONSU                       " & vbNewLine _
                                              & ",@HASU                        " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@SERIAL_NO                   " & vbNewLine _
                                              & ",@GOODS_COND_KB_1             " & vbNewLine _
                                              & ",@GOODS_COND_KB_2             " & vbNewLine _
                                              & ",@GOODS_COND_KB_3             " & vbNewLine _
                                              & ",@GOODS_CRT_DATE              " & vbNewLine _
                                              & ",@LT_DATE                     " & vbNewLine _
                                              & ",@SPD_KB                      " & vbNewLine _
                                              & ",@OFB_KB                      " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@ALLOC_PRIORITY              " & vbNewLine _
                                              & ",@REMARK_OUT                  " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "ZAI_TRS"

    ''' <summary>
    ''' INSERT（ZAI_TRS）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURISAKI_ZAI As String = "INSERT INTO $LM_TRN$..D_ZAI_TRS" & vbNewLine _
                                               & "(                              " & vbNewLine _
                                               & " NRS_BR_CD                     " & vbNewLine _
                                               & ",ZAI_REC_NO                    " & vbNewLine _
                                               & ",WH_CD                         " & vbNewLine _
                                               & ",TOU_NO                        " & vbNewLine _
                                               & ",SITU_NO                       " & vbNewLine _
                                               & ",ZONE_CD                       " & vbNewLine _
                                               & ",LOCA                          " & vbNewLine _
                                               & ",LOT_NO                        " & vbNewLine _
                                               & ",CUST_CD_L                     " & vbNewLine _
                                               & ",CUST_CD_M                     " & vbNewLine _
                                               & ",GOODS_CD_NRS                  " & vbNewLine _
                                               & ",INKA_NO_L                     " & vbNewLine _
                                               & ",INKA_NO_M                     " & vbNewLine _
                                               & ",INKA_NO_S                     " & vbNewLine _
                                               & ",ALLOC_PRIORITY                " & vbNewLine _
                                               & ",RSV_NO                        " & vbNewLine _
                                               & ",SERIAL_NO                     " & vbNewLine _
                                               & ",HOKAN_YN                      " & vbNewLine _
                                               & ",TAX_KB                        " & vbNewLine _
                                               & ",GOODS_COND_KB_1               " & vbNewLine _
                                               & ",GOODS_COND_KB_2               " & vbNewLine _
                                               & ",GOODS_COND_KB_3               " & vbNewLine _
                                               & ",OFB_KB                        " & vbNewLine _
                                               & ",SPD_KB                        " & vbNewLine _
                                               & ",REMARK_OUT                    " & vbNewLine _
                                               & ",PORA_ZAI_NB                   " & vbNewLine _
                                               & ",ALCTD_NB                      " & vbNewLine _
                                               & ",ALLOC_CAN_NB                  " & vbNewLine _
                                               & ",IRIME                         " & vbNewLine _
                                               & ",PORA_ZAI_QT                   " & vbNewLine _
                                               & ",ALCTD_QT                      " & vbNewLine _
                                               & ",ALLOC_CAN_QT                  " & vbNewLine _
                                               & ",INKO_DATE                     " & vbNewLine _
                                               & ",INKO_PLAN_DATE                " & vbNewLine _
                                               & ",ZERO_FLAG                     " & vbNewLine _
                                               & ",LT_DATE                       " & vbNewLine _
                                               & ",GOODS_CRT_DATE                " & vbNewLine _
                                               & ",DEST_CD_P                     " & vbNewLine _
                                               & ",REMARK                        " & vbNewLine _
                                               & ",SMPL_FLAG                     " & vbNewLine _
                                               & ",SYS_ENT_DATE                  " & vbNewLine _
                                               & ",SYS_ENT_TIME                  " & vbNewLine _
                                               & ",SYS_ENT_PGID                  " & vbNewLine _
                                               & ",SYS_ENT_USER                  " & vbNewLine _
                                               & ",SYS_UPD_DATE                  " & vbNewLine _
                                               & ",SYS_UPD_TIME                  " & vbNewLine _
                                               & ",SYS_UPD_PGID                  " & vbNewLine _
                                               & ",SYS_UPD_USER                  " & vbNewLine _
                                               & ",SYS_DEL_FLG                   " & vbNewLine _
                                               & " )VALUES(                      " & vbNewLine _
                                               & " @NRS_BR_CD                    " & vbNewLine _
                                               & ",@ZAI_REC_NO                   " & vbNewLine _
                                               & ",@WH_CD                        " & vbNewLine _
                                               & ",@TOU_NO                       " & vbNewLine _
                                               & ",@SITU_NO                      " & vbNewLine _
                                               & ",@ZONE_CD                      " & vbNewLine _
                                               & ",@LOCA                         " & vbNewLine _
                                               & ",@LOT_NO                       " & vbNewLine _
                                               & ",@CUST_CD_L                    " & vbNewLine _
                                               & ",@CUST_CD_M                    " & vbNewLine _
                                               & ",@GOODS_CD_NRS                 " & vbNewLine _
                                               & ",@INKA_NO_L                    " & vbNewLine _
                                               & ",@INKA_NO_M                    " & vbNewLine _
                                               & ",@INKA_NO_S                    " & vbNewLine _
                                               & ",@ALLOC_PRIORITY               " & vbNewLine _
                                               & ",@RSV_NO                       " & vbNewLine _
                                               & ",@SERIAL_NO                    " & vbNewLine _
                                               & ",@HOKAN_YN                     " & vbNewLine _
                                               & ",@TAX_KB                       " & vbNewLine _
                                               & ",@GOODS_COND_KB_1              " & vbNewLine _
                                               & ",@GOODS_COND_KB_2              " & vbNewLine _
                                               & ",@GOODS_COND_KB_3              " & vbNewLine _
                                               & ",@OFB_KB                       " & vbNewLine _
                                               & ",@SPD_KB                       " & vbNewLine _
                                               & ",@REMARK_OUT                   " & vbNewLine _
                                               & ",@PORA_ZAI_NB                  " & vbNewLine _
                                               & ",@ALCTD_NB                     " & vbNewLine _
                                               & ",@ALLOC_CAN_NB                 " & vbNewLine _
                                               & ",@IRIME                        " & vbNewLine _
                                               & ",@PORA_ZAI_QT                  " & vbNewLine _
                                               & ",@ALCTD_QT                     " & vbNewLine _
                                               & ",@ALLOC_CAN_QT                 " & vbNewLine _
                                               & ",@INKO_DATE                    " & vbNewLine _
                                               & ",@INKO_PLAN_DATE               " & vbNewLine _
                                               & ",@ZERO_FLAG                    " & vbNewLine _
                                               & ",@LT_DATE                      " & vbNewLine _
                                               & ",@GOODS_CRT_DATE               " & vbNewLine _
                                               & ",@DEST_CD_P                    " & vbNewLine _
                                               & ",@REMARK                       " & vbNewLine _
                                               & ",@SMPL_FLAG                    " & vbNewLine _
                                               & ",@SYS_ENT_DATE                 " & vbNewLine _
                                               & ",@SYS_ENT_TIME                 " & vbNewLine _
                                               & ",@SYS_ENT_PGID                 " & vbNewLine _
                                               & ",@SYS_ENT_USER                 " & vbNewLine _
                                               & ",@SYS_UPD_DATE                 " & vbNewLine _
                                               & ",@SYS_UPD_TIME                 " & vbNewLine _
                                               & ",@SYS_UPD_PGID                 " & vbNewLine _
                                               & ",@SYS_UPD_USER                 " & vbNewLine _
                                               & ",@SYS_DEL_FLG                  " & vbNewLine _
                                               & ")                              " & vbNewLine

#End Region

#Region "OUTKA_L"

    ''' <summary>
    ''' INSERT（OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURIOUTKA_L As String = "INSERT INTO                                    " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",FURI_NO                                                 " & vbNewLine _
                                         & ",OUTKA_KB                                                " & vbNewLine _
                                         & ",SYUBETU_KB                                              " & vbNewLine _
                                         & ",OUTKA_STATE_KB                                          " & vbNewLine _
                                         & ",OUTKAHOKOKU_YN                                          " & vbNewLine _
                                         & ",PICK_KB                                                 " & vbNewLine _
                                         & ",DENP_NO                                                 " & vbNewLine _
                                         & ",ARR_KANRYO_INFO                                         " & vbNewLine _
                                         & ",WH_CD                                                   " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE                                         " & vbNewLine _
                                         & ",OUTKO_DATE                                              " & vbNewLine _
                                         & ",ARR_PLAN_DATE                                           " & vbNewLine _
                                         & ",ARR_PLAN_TIME                                           " & vbNewLine _
                                         & ",HOKOKU_DATE                                             " & vbNewLine _
                                         & ",TOUKI_HOKAN_YN                                          " & vbNewLine _
                                         & ",END_DATE                                                " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",SHIP_CD_L                                               " & vbNewLine _
                                         & ",SHIP_CD_M                                               " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & ",DEST_AD_3                                               " & vbNewLine _
                                         & ",DEST_TEL                                                " & vbNewLine _
                                         & ",NHS_REMARK                                              " & vbNewLine _
                                         & ",SP_NHS_KB                                               " & vbNewLine _
                                         & ",COA_YN                                                  " & vbNewLine _
                                         & ",CUST_ORD_NO                                             " & vbNewLine _
                                         & ",BUYER_ORD_NO                                            " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",OUTKA_PKG_NB                                            " & vbNewLine _
                                         & ",DENP_YN                                                 " & vbNewLine _
                                         & ",PC_KB                                                   " & vbNewLine _
                                         & ",NIYAKU_YN                                               " & vbNewLine _
                                         & ",ALL_PRINT_FLAG                                          " & vbNewLine _
                                         & ",NIHUDA_FLAG                                             " & vbNewLine _
                                         & ",NHS_FLAG                                                " & vbNewLine _
                                         & ",DENP_FLAG                                               " & vbNewLine _
                                         & ",COA_FLAG                                                " & vbNewLine _
                                         & ",HOKOKU_FLAG                                             " & vbNewLine _
                                         & ",MATOME_PICK_FLAG                                        " & vbNewLine _
                                         & ",LAST_PRINT_DATE                                         " & vbNewLine _
                                         & ",LAST_PRINT_TIME                                         " & vbNewLine _
                                         & ",SASZ_USER                                               " & vbNewLine _
                                         & ",OUTKO_USER                                              " & vbNewLine _
                                         & ",KEN_USER                                                " & vbNewLine _
                                         & ",OUTKA_USER                                              " & vbNewLine _
                                         & ",HOU_USER                                                " & vbNewLine _
                                         & ",ORDER_TYPE                                              " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@FURI_NO                                                " & vbNewLine _
                                         & ",@OUTKA_KB                                               " & vbNewLine _
                                         & ",@SYUBETU_KB                                             " & vbNewLine _
                                         & ",@OUTKA_STATE_KB                                         " & vbNewLine _
                                         & ",@OUTKAHOKOKU_YN                                         " & vbNewLine _
                                         & ",@PICK_KB                                                " & vbNewLine _
                                         & ",@DENP_NO                                                " & vbNewLine _
                                         & ",@ARR_KANRYO_INFO                                        " & vbNewLine _
                                         & ",@WH_CD                                                  " & vbNewLine _
                                         & ",@OUTKA_PLAN_DATE                                        " & vbNewLine _
                                         & ",@OUTKO_DATE                                             " & vbNewLine _
                                         & ",@ARR_PLAN_DATE                                          " & vbNewLine _
                                         & ",@ARR_PLAN_TIME                                          " & vbNewLine _
                                         & ",@HOKOKU_DATE                                            " & vbNewLine _
                                         & ",@TOUKI_HOKAN_YN                                         " & vbNewLine _
                                         & ",@END_DATE                                               " & vbNewLine _
                                         & ",@CUST_CD_L                                              " & vbNewLine _
                                         & ",@CUST_CD_M                                              " & vbNewLine _
                                         & ",@SHIP_CD_L                                              " & vbNewLine _
                                         & ",@SHIP_CD_M                                              " & vbNewLine _
                                         & ",@DEST_CD                                                " & vbNewLine _
                                         & ",@DEST_AD_3                                              " & vbNewLine _
                                         & ",@DEST_TEL                                               " & vbNewLine _
                                         & ",@NHS_REMARK                                             " & vbNewLine _
                                         & ",@SP_NHS_KB                                              " & vbNewLine _
                                         & ",@COA_YN                                                 " & vbNewLine _
                                         & ",@CUST_ORD_NO                                            " & vbNewLine _
                                         & ",@BUYER_ORD_NO                                           " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                                           " & vbNewLine _
                                         & ",@DENP_YN                                                " & vbNewLine _
                                         & ",@PC_KB                                                  " & vbNewLine _
                                         & ",@NIYAKU_YN                                              " & vbNewLine _
                                         & ",@ALL_PRINT_FLAG                                         " & vbNewLine _
                                         & ",@NIHUDA_FLAG                                            " & vbNewLine _
                                         & ",@NHS_FLAG                                               " & vbNewLine _
                                         & ",@DENP_FLAG                                              " & vbNewLine _
                                         & ",@COA_FLAG                                               " & vbNewLine _
                                         & ",@HOKOKU_FLAG                                            " & vbNewLine _
                                         & ",@MATOME_PICK_FLAG                                       " & vbNewLine _
                                         & ",@LAST_PRINT_DATE                                        " & vbNewLine _
                                         & ",@LAST_PRINT_TIME                                        " & vbNewLine _
                                         & ",@SASZ_USER                                              " & vbNewLine _
                                         & ",@OUTKO_USER                                             " & vbNewLine _
                                         & ",@KEN_USER                                               " & vbNewLine _
                                         & ",@OUTKA_USER                                             " & vbNewLine _
                                         & ",@HOU_USER                                               " & vbNewLine _
                                         & ",@ORDER_TYPE                                             " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ")                                                        " & vbNewLine

#End Region

#Region "OUTKA_M"

    ''' <summary>
    ''' INSERT（OUTKA_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURIOUTKA_M As String = "INSERT INTO                                    " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_M                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",OUTKA_NO_M                                              " & vbNewLine _
                                         & ",EDI_SET_NO                                              " & vbNewLine _
                                         & ",COA_YN                                                  " & vbNewLine _
                                         & ",CUST_ORD_NO_DTL                                         " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL                                        " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",RSV_NO                                                  " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",SERIAL_NO                                               " & vbNewLine _
                                         & ",ALCTD_KB                                                " & vbNewLine _
                                         & ",OUTKA_PKG_NB                                            " & vbNewLine _
                                         & ",OUTKA_HASU                                              " & vbNewLine _
                                         & ",OUTKA_QT                                                " & vbNewLine _
                                         & ",OUTKA_TTL_NB                                            " & vbNewLine _
                                         & ",OUTKA_TTL_QT                                            " & vbNewLine _
                                         & ",ALCTD_NB                                                " & vbNewLine _
                                         & ",ALCTD_QT                                                " & vbNewLine _
                                         & ",BACKLOG_NB                                              " & vbNewLine _
                                         & ",BACKLOG_QT                                              " & vbNewLine _
                                         & ",UNSO_ONDO_KB                                            " & vbNewLine _
                                         & ",IRIME                                                   " & vbNewLine _
                                         & ",IRIME_UT                                                " & vbNewLine _
                                         & ",OUTKA_M_PKG_NB                                          " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",SIZE_KB                                                 " & vbNewLine _
                                         & ",ZAIKO_KB                                                " & vbNewLine _
                                         & ",SOURCE_CD                                               " & vbNewLine _
                                         & ",YELLOW_CARD                                             " & vbNewLine _
                                         & ",PRINT_SORT                                              " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@OUTKA_NO_M                                             " & vbNewLine _
                                         & ",@EDI_SET_NO                                             " & vbNewLine _
                                         & ",@COA_YN                                                 " & vbNewLine _
                                         & ",@CUST_ORD_NO_DTL                                        " & vbNewLine _
                                         & ",@BUYER_ORD_NO_DTL                                       " & vbNewLine _
                                         & ",@GOODS_CD_NRS                                           " & vbNewLine _
                                         & ",@RSV_NO                                                 " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",@SERIAL_NO                                              " & vbNewLine _
                                         & ",@ALCTD_KB                                               " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                                           " & vbNewLine _
                                         & ",@OUTKA_HASU                                             " & vbNewLine _
                                         & ",@OUTKA_QT                                               " & vbNewLine _
                                         & ",@OUTKA_TTL_NB                                           " & vbNewLine _
                                         & ",@OUTKA_TTL_QT                                           " & vbNewLine _
                                         & ",@ALCTD_NB                                               " & vbNewLine _
                                         & ",@ALCTD_QT                                               " & vbNewLine _
                                         & ",@BACKLOG_NB                                             " & vbNewLine _
                                         & ",@BACKLOG_QT                                             " & vbNewLine _
                                         & ",@UNSO_ONDO_KB                                           " & vbNewLine _
                                         & ",@IRIME                                                  " & vbNewLine _
                                         & ",@IRIME_UT                                               " & vbNewLine _
                                         & ",@OUTKA_M_PKG_NB                                         " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@SIZE_KB                                                " & vbNewLine _
                                         & ",@ZAIKO_KB                                               " & vbNewLine _
                                         & ",@SOURCE_CD                                              " & vbNewLine _
                                         & ",@YELLOW_CARD                                            " & vbNewLine _
                                         & ",@PRINT_SORT                                             " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ")                                                        " & vbNewLine

#End Region

#Region "OUTKA_S"

    ''' <summary>
    ''' INSERT（OUTKA_S）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURIOUTKA_S As String = "INSERT INTO                                    " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_S                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",OUTKA_NO_M                                              " & vbNewLine _
                                         & ",OUTKA_NO_S                                              " & vbNewLine _
                                         & ",TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",SERIAL_NO                                               " & vbNewLine _
                                         & ",OUTKA_TTL_NB                                            " & vbNewLine _
                                         & ",OUTKA_TTL_QT                                            " & vbNewLine _
                                         & ",ZAI_REC_NO                                              " & vbNewLine _
                                         & ",INKA_NO_L                                               " & vbNewLine _
                                         & ",INKA_NO_M                                               " & vbNewLine _
                                         & ",INKA_NO_S                                               " & vbNewLine _
                                         & ",ZAI_UPD_FLAG                                            " & vbNewLine _
                                         & ",ALCTD_CAN_NB                                            " & vbNewLine _
                                         & ",ALCTD_NB                                                " & vbNewLine _
                                         & ",ALCTD_CAN_QT                                            " & vbNewLine _
                                         & ",ALCTD_QT                                                " & vbNewLine _
                                         & ",IRIME                                                   " & vbNewLine _
                                         & ",BETU_WT                                                 " & vbNewLine _
                                         & ",COA_FLAG                                                " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",SMPL_FLAG                                               " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@OUTKA_NO_M                                             " & vbNewLine _
                                         & ",@OUTKA_NO_S                                             " & vbNewLine _
                                         & ",@TOU_NO                                                 " & vbNewLine _
                                         & ",@SITU_NO                                                " & vbNewLine _
                                         & ",@ZONE_CD                                                " & vbNewLine _
                                         & ",@LOCA                                                   " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",@SERIAL_NO                                              " & vbNewLine _
                                         & ",@OUTKA_TTL_NB                                           " & vbNewLine _
                                         & ",@OUTKA_TTL_QT                                           " & vbNewLine _
                                         & ",@ZAI_REC_NO                                             " & vbNewLine _
                                         & ",@INKA_NO_L                                              " & vbNewLine _
                                         & ",@INKA_NO_M                                              " & vbNewLine _
                                         & ",@INKA_NO_S                                              " & vbNewLine _
                                         & ",@ZAI_UPD_FLAG                                           " & vbNewLine _
                                         & ",@ALCTD_CAN_NB                                           " & vbNewLine _
                                         & ",@ALCTD_NB                                               " & vbNewLine _
                                         & ",@ALCTD_CAN_QT                                           " & vbNewLine _
                                         & ",@ALCTD_QT                                               " & vbNewLine _
                                         & ",@IRIME                                                  " & vbNewLine _
                                         & ",@BETU_WT                                                " & vbNewLine _
                                         & ",@COA_FLAG                                               " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@SMPL_FLAG                                              " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ")                                                        " & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_JIKAI_BUNNOU_OUTKAEDI_DTL_RAPI As String = "" _
            & "INSERT INTO $LM_TRN$..H_OUTKAEDI_DTL_RAPI (                                                                                  " & vbNewLine _
            & "      DEL_KB                                                                                                                 " & vbNewLine _
            & "    , CRT_DATE                                                                                                               " & vbNewLine _
            & "    , FILE_NAME                                                                                                              " & vbNewLine _
            & "    , REC_NO                                                                                                                 " & vbNewLine _
            & "    , GYO_NO                                                                                                                 " & vbNewLine _
            & "    , NRS_BR_CD                                                                                                              " & vbNewLine _
            & "    , EDI_CTL_NO                                                                                                             " & vbNewLine _
            & "    , EDI_CTL_NO_CHU                                                                                                         " & vbNewLine _
            & "    , OUTKA_CTL_NO                                                                                                           " & vbNewLine _
            & "    , OUTKA_CTL_NO_CHU                                                                                                       " & vbNewLine _
            & "    , CUST_CD_L                                                                                                              " & vbNewLine _
            & "    , CUST_CD_M                                                                                                              " & vbNewLine _
            & "    , PRTFLG                                                                                                                 " & vbNewLine _
            & "    , FILE_KBN                                                                                                               " & vbNewLine _
            & "    , TORIHIKI_KBN                                                                                                           " & vbNewLine _
            & "    , CANCEL_KBN                                                                                                             " & vbNewLine _
            & "    , OUTKA_WH_CD                                                                                                            " & vbNewLine _
            & "    , DENP_KBN                                                                                                               " & vbNewLine _
            & "    , DENP_NO                                                                                                                " & vbNewLine _
            & "    , SUPPLIER_REF_NO                                                                                                        " & vbNewLine _
            & "    , P_O_NO                                                                                                                 " & vbNewLine _
            & "    , DEST_CD                                                                                                                " & vbNewLine _
            & "    , DEST_NM1                                                                                                               " & vbNewLine _
            & "    , DEST_NM2                                                                                                               " & vbNewLine _
            & "    , DEST_ADD1                                                                                                              " & vbNewLine _
            & "    , DEST_ADD2                                                                                                              " & vbNewLine _
            & "    , DEST_ADD3                                                                                                              " & vbNewLine _
            & "    , DEST_ADD4                                                                                                              " & vbNewLine _
            & "    , DEST_ADD5                                                                                                              " & vbNewLine _
            & "    , DEST_ZIP                                                                                                               " & vbNewLine _
            & "    , DEST_TEL                                                                                                               " & vbNewLine _
            & "    , SUPPLIER_GOODS_CD                                                                                                      " & vbNewLine _
            & "    , RAPIDUS_GOODS_CD                                                                                                       " & vbNewLine _
            & "    , GOODS_MEI                                                                                                              " & vbNewLine _
            & "    , CASE_NB                                                                                                                " & vbNewLine _
            & "    , BARA_QT                                                                                                                " & vbNewLine _
            & "    , JYURYO                                                                                                                 " & vbNewLine _
            & "    , YOSEKI                                                                                                                 " & vbNewLine _
            & "    , TANI                                                                                                                   " & vbNewLine _
            & "    , NISUGATA                                                                                                               " & vbNewLine _
            & "    , SUPPLIER_LOT_NO                                                                                                        " & vbNewLine _
            & "    , PLT_CNT                                                                                                                " & vbNewLine _
            & "    , YUKO_KIGEN                                                                                                             " & vbNewLine _
            & "    , SEIZO_DATE                                                                                                             " & vbNewLine _
            & "    , DTL_REMARK                                                                                                             " & vbNewLine _
            & "    , DAMAGE_HOLD                                                                                                            " & vbNewLine _
            & "    , RECORD_STATUS                                                                                                          " & vbNewLine _
            & "    , JISSEKI_SHORI_FLG                                                                                                      " & vbNewLine _
            & "    , EDI_USER                                                                                                               " & vbNewLine _
            & "    , EDI_DATE                                                                                                               " & vbNewLine _
            & "    , EDI_TIME                                                                                                               " & vbNewLine _
            & "    , SYS_ENT_DATE                                                                                                           " & vbNewLine _
            & "    , SYS_ENT_TIME                                                                                                           " & vbNewLine _
            & "    , SYS_ENT_PGID                                                                                                           " & vbNewLine _
            & "    , SYS_ENT_USER                                                                                                           " & vbNewLine _
            & "    , SYS_UPD_DATE                                                                                                           " & vbNewLine _
            & "    , SYS_UPD_TIME                                                                                                           " & vbNewLine _
            & "    , SYS_UPD_PGID                                                                                                           " & vbNewLine _
            & "    , SYS_UPD_USER                                                                                                           " & vbNewLine _
            & "    , SYS_DEL_FLG                                                                                                            " & vbNewLine _
            & ")                                                                                                                            " & vbNewLine _
            & "SELECT                                                                                                                       " & vbNewLine _
            & "      H_OUTKAEDI_DTL_RAPI.DEL_KB                                                                                             " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.CRT_DATE                                                                                           " & vbNewLine _
            & "    , @FILE_NAME + @TEMPLATE_PREFIX + CAST(@JIKAI_BUNNOU_KAISU AS VARCHAR(20)) + @TEMPLATE_SUFFIX +                          " & vbNewLine _
            & "      CASE WHEN @SAME_CRT_DATE_AND_FILE_NAME_CNT = 0                                                                         " & vbNewLine _
            & "        THEN ''                                                                                                              " & vbNewLine _
            & "        ELSE '(' + CAST(@SAME_CRT_DATE_AND_FILE_NAME_CNT + 1 AS VARCHAR(5)) + ')'                                            " & vbNewLine _
            & "      END AS FILE_NAME                                                                                                       " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.REC_NO                                                                                             " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.GYO_NO                                                                                             " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.NRS_BR_CD                                                                                          " & vbNewLine _
            & "    , @EDI_CTL_NO_NEW AS EDI_CTL_NO                                                                                          " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.EDI_CTL_NO_CHU                                                                                     " & vbNewLine _
            & "    , (SELECT KBN_NM6 + '00000000' FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'D003' AND KBN_CD = @NRS_BR_CD) AS OUTKA_CTL_NO " & vbNewLine _
            & "    , '000' OUTKA_CTL_NO_CHU                                                                                                 " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.CUST_CD_L                                                                                          " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.CUST_CD_M                                                                                          " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.PRTFLG                                                                                             " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.FILE_KBN                                                                                           " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.TORIHIKI_KBN                                                                                       " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.CANCEL_KBN                                                                                         " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.OUTKA_WH_CD                                                                                        " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DENP_KBN                                                                                           " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DENP_NO                                                                                            " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.SUPPLIER_REF_NO                                                                                    " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.P_O_NO                                                                                             " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_CD                                                                                            " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_NM1                                                                                           " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_NM2                                                                                           " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_ADD1                                                                                          " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_ADD2                                                                                          " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_ADD3                                                                                          " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_ADD4                                                                                          " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_ADD5                                                                                          " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_ZIP                                                                                           " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DEST_TEL                                                                                           " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.SUPPLIER_GOODS_CD                                                                                  " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.RAPIDUS_GOODS_CD                                                                                   " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.GOODS_MEI                                                                                          " & vbNewLine _
            & "    , ROUND(                                                                                                                 " & vbNewLine _
            & "            CASE WHEN H_OUTKAEDI_M.PKG_NB = 0                                                                                " & vbNewLine _
            & "                THEN H_OUTKAEDI_DTL_RAPI.CASE_NB                                                                             " & vbNewLine _
            & "                ELSE                                                                                                         " & vbNewLine _
            & "                    CASE WHEN H_OUTKAEDI_M.PKG_NB = 1                                                                        " & vbNewLine _
            & "                        THEN @JIKAI_BUNNOU_NB                                                                                " & vbNewLine _
            & "                        ELSE FLOOR(@JIKAI_BUNNOU_NB / H_OUTKAEDI_M.PKG_NB)                                                   " & vbNewLine _
            & "                    END                                                                                                      " & vbNewLine _
            & "            END                                                                                                              " & vbNewLine _
            & "         , 0) AS CASE_NB                                                                                                     " & vbNewLine _
            & "    , ROUND(                                                                                                                 " & vbNewLine _
            & "            CASE WHEN H_OUTKAEDI_M.PKG_NB = 1                                                                                " & vbNewLine _
            & "                THEN @JIKAI_BUNNOU_NB * (H_OUTKAEDI_DTL_RAPI.BARA_QT / H_OUTKAEDI_DTL_RAPI.CASE_NB)                          " & vbNewLine _
            & "                ELSE @JIKAI_BUNNOU_NB                                                                                        " & vbNewLine _
            & "            END                                                                                                              " & vbNewLine _
            & "         , 0) AS BARA_QT                                                                                                     " & vbNewLine _
            & "    , ROUND(                                                                                                                 " & vbNewLine _
            & "            CASE WHEN H_OUTKAEDI_M.PKG_NB = 1                                                                                " & vbNewLine _
            & "                THEN @JIKAI_BUNNOU_NB * (H_OUTKAEDI_DTL_RAPI.JYURYO / H_OUTKAEDI_DTL_RAPI.CASE_NB)                           " & vbNewLine _
            & "                ELSE @JIKAI_BUNNOU_NB * (H_OUTKAEDI_DTL_RAPI.JYURYO / H_OUTKAEDI_DTL_RAPI.BARA_QT)                           " & vbNewLine _
            & "            END                                                                                                              " & vbNewLine _
            & "         , 3) AS JYURYO                                                                                                      " & vbNewLine _
            & "    , ROUND(                                                                                                                 " & vbNewLine _
            & "           (H_OUTKAEDI_DTL_RAPI.YOSEKI / H_OUTKAEDI_DTL_RAPI.BARA_QT) *                                                      " & vbNewLine _
            & "                -- ↓ 今回 SELECT 結果の BARA_QT と同じ式                                                                    " & vbNewLine _
            & "                CASE WHEN H_OUTKAEDI_M.PKG_NB = 1                                                                            " & vbNewLine _
            & "                    THEN @JIKAI_BUNNOU_NB * (H_OUTKAEDI_DTL_RAPI.BARA_QT / H_OUTKAEDI_DTL_RAPI.CASE_NB)                      " & vbNewLine _
            & "                    ELSE @JIKAI_BUNNOU_NB                                                                                    " & vbNewLine _
            & "                END                                                                                                          " & vbNewLine _
            & "                -- ↑ 今回 SELECT 結果の BARA_QT と同じ式                                                                    " & vbNewLine _
            & "         , 3) AS YOSEKI                                                                                                      " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.TANI                                                                                               " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.NISUGATA                                                                                           " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.SUPPLIER_LOT_NO                                                                                    " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.PLT_CNT                                                                                            " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.YUKO_KIGEN                                                                                         " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.SEIZO_DATE                                                                                         " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DTL_REMARK                                                                                         " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DAMAGE_HOLD                                                                                        " & vbNewLine _
            & "    , CAST(@JIKAI_BUNNOU_KAISU AS VARCHAR(20)) AS RECORD_STATUS                                                              " & vbNewLine _
            & "    , @JISSEKI_SHORI_FLG  AS JISSEKI_SHORI_FLG                                                                               " & vbNewLine _
            & "    , @EDI_USER           AS EDI_USER                                                                                        " & vbNewLine _
            & "    , @EDI_DATE           AS EDI_DATE                                                                                        " & vbNewLine _
            & "    , @EDI_TIME           AS EDI_TIME                                                                                        " & vbNewLine _
            & "    , @SYS_ENT_DATE       AS SYS_ENT_DATE                                                                                    " & vbNewLine _
            & "    , @SYS_ENT_TIME       AS SYS_ENT_TIME                                                                                    " & vbNewLine _
            & "    , @SYS_ENT_PGID       AS SYS_ENT_PGID                                                                                    " & vbNewLine _
            & "    , @SYS_ENT_USER       AS SYS_ENT_USER                                                                                    " & vbNewLine _
            & "    , @SYS_UPD_DATE       AS SYS_UPD_DATE                                                                                    " & vbNewLine _
            & "    , @SYS_UPD_TIME       AS SYS_UPD_TIME                                                                                    " & vbNewLine _
            & "    , @SYS_UPD_PGID       AS SYS_UPD_PGID                                                                                    " & vbNewLine _
            & "    , @SYS_UPD_USER       AS SYS_UPD_USER                                                                                    " & vbNewLine _
            & "    , @SYS_DEL_FLG        AS SYS_DEL_FLG                                                                                     " & vbNewLine _
            & "FROM                                                                                                                         " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_DTL_RAPI                                                                                            " & vbNewLine _
            & "LEFT JOIN                                                                                                                    " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_M                                                                                                   " & vbNewLine _
            & "        ON  H_OUTKAEDI_M.NRS_BR_CD = H_OUTKAEDI_DTL_RAPI.NRS_BR_CD                                                           " & vbNewLine _
            & "        AND H_OUTKAEDI_M.EDI_CTL_NO = H_OUTKAEDI_DTL_RAPI.EDI_CTL_NO                                                         " & vbNewLine _
            & "        AND H_OUTKAEDI_M.EDI_CTL_NO_CHU = H_OUTKAEDI_DTL_RAPI.EDI_CTL_NO_CHU                                                 " & vbNewLine _
            & "        AND H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                                                                   " & vbNewLine _
            & "WHERE                                                                                                                        " & vbNewLine _
            & "    H_OUTKAEDI_DTL_RAPI.CRT_DATE = @CRT_DATE                                                                                 " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.FILE_NAME = @FILE_NAME                                                                               " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.REC_NO = @REC_NO                                                                                     " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.GYO_NO = @GYO_NO                                                                                     " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.DEL_KB <> '1'                                                                                        " & vbNewLine _
            & ""

#End Region ' "H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報)"

#Region "H_OUTKAEDI_L(次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_L(次回分納情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_JIKAI_BUNNOU_OUTKAEDI_L As String = "" _
            & "INSERT INTO $LM_TRN$..H_OUTKAEDI_L (          " & vbNewLine _
            & "      DEL_KB                                  " & vbNewLine _
            & "    , NRS_BR_CD                               " & vbNewLine _
            & "    , EDI_CTL_NO                              " & vbNewLine _
            & "    , OUTKA_KB                                " & vbNewLine _
            & "    , SYUBETU_KB                              " & vbNewLine _
            & "    , NAIGAI_KB                               " & vbNewLine _
            & "    , OUTKA_STATE_KB                          " & vbNewLine _
            & "    , OUTKAHOKOKU_YN                          " & vbNewLine _
            & "    , PICK_KB                                 " & vbNewLine _
            & "    , WH_CD                                   " & vbNewLine _
            & "    , TOUKI_HOKAN_YN                          " & vbNewLine _
            & "    , CUST_CD_L                               " & vbNewLine _
            & "    , CUST_CD_M                               " & vbNewLine _
            & "    , DEST_CD                                 " & vbNewLine _
            & "    , DEST_NM                                 " & vbNewLine _
            & "    , DEST_ZIP                                " & vbNewLine _
            & "    , DEST_AD_1                               " & vbNewLine _
            & "    , DEST_AD_2                               " & vbNewLine _
            & "    , DEST_AD_3                               " & vbNewLine _
            & "    , DEST_AD_4                               " & vbNewLine _
            & "    , DEST_AD_5                               " & vbNewLine _
            & "    , DEST_TEL                                " & vbNewLine _
            & "    , DEST_FAX                                " & vbNewLine _
            & "    , DEST_JIS_CD                             " & vbNewLine _
            & "    , CUST_ORD_NO                             " & vbNewLine _
            & "    , BUYER_ORD_NO                            " & vbNewLine _
            & "    , UNSO_MOTO_KB                            " & vbNewLine _
            & "    , UNSO_CD                                 " & vbNewLine _
            & "    , UNSO_NM                                 " & vbNewLine _
            & "    , UNSO_BR_CD                              " & vbNewLine _
            & "    , UNSO_BR_NM                              " & vbNewLine _
            & "    , OUT_FLAG                                " & vbNewLine _
            & "    , AKAKURO_KB                              " & vbNewLine _
            & "    , JISSEKI_FLAG                            " & vbNewLine _
            & "    , FREE_C01                                " & vbNewLine _
            & "    , FREE_C29                                " & vbNewLine _
            & "    , FREE_C30                                " & vbNewLine _
            & "    , CRT_USER                                " & vbNewLine _
            & "    , CRT_DATE                                " & vbNewLine _
            & "    , CRT_TIME                                " & vbNewLine _
            & "    , EDIT_FLAG                               " & vbNewLine _
            & "    , MATCHING_FLAG                           " & vbNewLine _
            & "    , SYS_ENT_DATE                            " & vbNewLine _
            & "    , SYS_ENT_TIME                            " & vbNewLine _
            & "    , SYS_ENT_PGID                            " & vbNewLine _
            & "    , SYS_ENT_USER                            " & vbNewLine _
            & "    , SYS_UPD_DATE                            " & vbNewLine _
            & "    , SYS_UPD_TIME                            " & vbNewLine _
            & "    , SYS_UPD_PGID                            " & vbNewLine _
            & "    , SYS_UPD_USER                            " & vbNewLine _
            & "    , SYS_DEL_FLG                             " & vbNewLine _
            & ")                                             " & vbNewLine _
            & "SELECT                                        " & vbNewLine _
            & "      '0' AS DEL_KB                           " & vbNewLine _
            & "    , H_OUTKAEDI_L.NRS_BR_CD                  " & vbNewLine _
            & "    , @EDI_CTL_NO_NEW AS EDI_CTL_NO           " & vbNewLine _
            & "    , '10' AS OUTKA_KB                        " & vbNewLine _
            & "    , '10' AS SYUBETU_KB                      " & vbNewLine _
            & "    , '01' AS NAIGAI_KB                       " & vbNewLine _
            & "    , '10' AS OUTKA_STATE_KB                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.OUTKAHOKOKU_YN             " & vbNewLine _
            & "    , H_OUTKAEDI_L.PICK_KB                    " & vbNewLine _
            & "    , H_OUTKAEDI_L.WH_CD                      " & vbNewLine _
            & "    , '1' AS TOUKI_HOKAN_YN                   " & vbNewLine _
            & "    , H_OUTKAEDI_L.CUST_CD_L                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.CUST_CD_M                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_CD                    " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_NM                    " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_ZIP                   " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_AD_1                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_AD_2                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_AD_3                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_AD_4                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_AD_5                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_TEL                   " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_FAX                   " & vbNewLine _
            & "    , H_OUTKAEDI_L.DEST_JIS_CD                " & vbNewLine _
            & "    , H_OUTKAEDI_L.CUST_ORD_NO                " & vbNewLine _
            & "    , H_OUTKAEDI_L.BUYER_ORD_NO               " & vbNewLine _
            & "    , H_OUTKAEDI_L.UNSO_MOTO_KB               " & vbNewLine _
            & "    , H_OUTKAEDI_L.UNSO_CD                    " & vbNewLine _
            & "    , H_OUTKAEDI_L.UNSO_NM                    " & vbNewLine _
            & "    , H_OUTKAEDI_L.UNSO_BR_CD                 " & vbNewLine _
            & "    , H_OUTKAEDI_L.UNSO_BR_NM                 " & vbNewLine _
            & "    , '0' AS OUT_FLAG                         " & vbNewLine _
            & "    , '0' AS AKAKURO_KB                       " & vbNewLine _
            & "    , '0' AS JISSEKI_FLAG                     " & vbNewLine _
            & "    , H_OUTKAEDI_L.FREE_C01                   " & vbNewLine _
            & "    , '11' AS FREE_C29                        " & vbNewLine _
            & "    , '02' AS FREE_C30                        " & vbNewLine _
            & "    , @CRT_USER     AS CRT_USER               " & vbNewLine _
            & "    , @CRT_DATE     AS CRT_DATE               " & vbNewLine _
            & "    , @CRT_TIME     AS CRT_TIME               " & vbNewLine _
            & "    , H_OUTKAEDI_L.EDIT_FLAG                  " & vbNewLine _
            & "    , H_OUTKAEDI_L.MATCHING_FLAG              " & vbNewLine _
            & "    , @SYS_ENT_DATE AS SYS_ENT_DATE           " & vbNewLine _
            & "    , @SYS_ENT_TIME AS SYS_ENT_TIME           " & vbNewLine _
            & "    , @SYS_ENT_PGID AS SYS_ENT_PGID           " & vbNewLine _
            & "    , @SYS_ENT_USER AS SYS_ENT_USER           " & vbNewLine _
            & "    , @SYS_UPD_DATE AS SYS_UPD_DATE           " & vbNewLine _
            & "    , @SYS_UPD_TIME AS SYS_UPD_TIME           " & vbNewLine _
            & "    , @SYS_UPD_PGID AS SYS_UPD_PGID           " & vbNewLine _
            & "    , @SYS_UPD_USER AS SYS_UPD_USER           " & vbNewLine _
            & "    , @SYS_DEL_FLG  AS SYS_DEL_FLG            " & vbNewLine _
            & "FROM                                          " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_L                    " & vbNewLine _
            & "WHERE                                         " & vbNewLine _
            & "    H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
            & "AND H_OUTKAEDI_L.EDI_CTL_NO = @EDI_CTL_NO     " & vbNewLine _
            & "AND H_OUTKAEDI_L.SYS_DEL_FLG = '0'            " & vbNewLine _
            & ""

#End Region ' "H_OUTKAEDI_L(次回分納情報)"

#Region "H_OUTKAEDI_M(次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_M(次回分納情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_JIKAI_BUNNOU_OUTKAEDI_M As String = "" _
            & "INSERT INTO $LM_TRN$..H_OUTKAEDI_M (                                " & vbNewLine _
            & "      DEL_KB                                                        " & vbNewLine _
            & "    , NRS_BR_CD                                                     " & vbNewLine _
            & "    , EDI_CTL_NO                                                    " & vbNewLine _
            & "    , EDI_CTL_NO_CHU                                                " & vbNewLine _
            & "    , COA_YN                                                        " & vbNewLine _
            & "    , CUST_ORD_NO_DTL                                               " & vbNewLine _
            & "    , BUYER_ORD_NO_DTL                                              " & vbNewLine _
            & "    , CUST_GOODS_CD                                                 " & vbNewLine _
            & "    , NRS_GOODS_CD                                                  " & vbNewLine _
            & "    , GOODS_NM                                                      " & vbNewLine _
            & "    , LOT_NO                                                        " & vbNewLine _
            & "    , ALCTD_KB                                                      " & vbNewLine _
            & "    , OUTKA_PKG_NB                                                  " & vbNewLine _
            & "    , OUTKA_HASU                                                    " & vbNewLine _
            & "    , OUTKA_QT                                                      " & vbNewLine _
            & "    , OUTKA_TTL_NB                                                  " & vbNewLine _
            & "    , OUTKA_TTL_QT                                                  " & vbNewLine _
            & "    , KB_UT                                                         " & vbNewLine _
            & "    , QT_UT                                                         " & vbNewLine _
            & "    , PKG_NB                                                        " & vbNewLine _
            & "    , PKG_UT                                                        " & vbNewLine _
            & "    , ONDO_KB                                                       " & vbNewLine _
            & "    , UNSO_ONDO_KB                                                  " & vbNewLine _
            & "    , IRIME                                                         " & vbNewLine _
            & "    , IRIME_UT                                                      " & vbNewLine _
            & "    , BETU_WT                                                       " & vbNewLine _
            & "    , REMARK                                                        " & vbNewLine _
            & "    , OUT_KB                                                        " & vbNewLine _
            & "    , AKAKURO_KB                                                    " & vbNewLine _
            & "    , JISSEKI_FLAG                                                  " & vbNewLine _
            & "    , CRT_USER                                                      " & vbNewLine _
            & "    , CRT_DATE                                                      " & vbNewLine _
            & "    , CRT_TIME                                                      " & vbNewLine _
            & "    , SYS_ENT_DATE                                                  " & vbNewLine _
            & "    , SYS_ENT_TIME                                                  " & vbNewLine _
            & "    , SYS_ENT_PGID                                                  " & vbNewLine _
            & "    , SYS_ENT_USER                                                  " & vbNewLine _
            & "    , SYS_UPD_DATE                                                  " & vbNewLine _
            & "    , SYS_UPD_TIME                                                  " & vbNewLine _
            & "    , SYS_UPD_PGID                                                  " & vbNewLine _
            & "    , SYS_UPD_USER                                                  " & vbNewLine _
            & "    , SYS_DEL_FLG                                                   " & vbNewLine _
            & ")                                                                   " & vbNewLine _
            & "SELECT                                                              " & vbNewLine _
            & "      '0' AS DEL_KB                                                 " & vbNewLine _
            & "    , H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
            & "    , @EDI_CTL_NO_NEW AS EDI_CTL_NO                                 " & vbNewLine _
            & "    , H_OUTKAEDI_M.EDI_CTL_NO_CHU                                   " & vbNewLine _
            & "    , H_OUTKAEDI_M.COA_YN                                           " & vbNewLine _
            & "    , H_OUTKAEDI_M.CUST_ORD_NO_DTL                                  " & vbNewLine _
            & "    , H_OUTKAEDI_M.BUYER_ORD_NO_DTL                                 " & vbNewLine _
            & "    , H_OUTKAEDI_M.CUST_GOODS_CD                                    " & vbNewLine _
            & "    , H_OUTKAEDI_M.NRS_GOODS_CD                                     " & vbNewLine _
            & "    , H_OUTKAEDI_M.GOODS_NM                                         " & vbNewLine _
            & "    , H_OUTKAEDI_M.LOT_NO                                           " & vbNewLine _
            & "    , '01' AS ALCTD_KB                                              " & vbNewLine _
            & "    , FLOOR(@JIKAI_BUNNOU_NB / H_OUTKAEDI_M.PKG_NB) AS OUTKA_PKG_NB " & vbNewLine _
            & "    , @JIKAI_BUNNOU_NB % H_OUTKAEDI_M.PKG_NB AS OUTKA_HASU          " & vbNewLine _
            & "    , @JIKAI_BUNNOU_NB * H_OUTKAEDI_M.IRIME AS OUTKA_QT             " & vbNewLine _
            & "    , @JIKAI_BUNNOU_NB AS OUTKA_TTL_NB                              " & vbNewLine _
            & "    , @JIKAI_BUNNOU_NB * H_OUTKAEDI_M.IRIME AS OUTKA_TTL_QT         " & vbNewLine _
            & "    , H_OUTKAEDI_M.KB_UT                                            " & vbNewLine _
            & "    , H_OUTKAEDI_M.QT_UT                                            " & vbNewLine _
            & "    , H_OUTKAEDI_M.PKG_NB                                           " & vbNewLine _
            & "    , H_OUTKAEDI_M.PKG_UT                                           " & vbNewLine _
            & "    , H_OUTKAEDI_M.ONDO_KB                                          " & vbNewLine _
            & "    , H_OUTKAEDI_M.UNSO_ONDO_KB                                     " & vbNewLine _
            & "    , H_OUTKAEDI_M.IRIME                                            " & vbNewLine _
            & "    , H_OUTKAEDI_M.IRIME_UT                                         " & vbNewLine _
            & "    , H_OUTKAEDI_M.BETU_WT                                          " & vbNewLine _
            & "    , H_OUTKAEDI_M.REMARK                                           " & vbNewLine _
            & "    , '0' AS OUT_KB                                                 " & vbNewLine _
            & "    , '0' AS AKAKURO_KB                                             " & vbNewLine _
            & "    , '0' AS JISSEKI_FLAG                                           " & vbNewLine _
            & "    , @CRT_USER     AS CRT_USER                                     " & vbNewLine _
            & "    , @CRT_DATE     AS CRT_DATE                                     " & vbNewLine _
            & "    , @CRT_TIME     AS CRT_TIME                                     " & vbNewLine _
            & "    , @SYS_ENT_DATE AS SYS_ENT_DATE                                 " & vbNewLine _
            & "    , @SYS_ENT_TIME AS SYS_ENT_TIME                                 " & vbNewLine _
            & "    , @SYS_ENT_PGID AS SYS_ENT_PGID                                 " & vbNewLine _
            & "    , @SYS_ENT_USER AS SYS_ENT_USER                                 " & vbNewLine _
            & "    , @SYS_UPD_DATE AS SYS_UPD_DATE                                 " & vbNewLine _
            & "    , @SYS_UPD_TIME AS SYS_UPD_TIME                                 " & vbNewLine _
            & "    , @SYS_UPD_PGID AS SYS_UPD_PGID                                 " & vbNewLine _
            & "    , @SYS_UPD_USER AS SYS_UPD_USER                                 " & vbNewLine _
            & "    , @SYS_DEL_FLG  AS SYS_DEL_FLG                                  " & vbNewLine _
            & "FROM                                                                " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_M                                          " & vbNewLine _
            & "WHERE                                                               " & vbNewLine _
            & "    H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
            & "AND H_OUTKAEDI_M.EDI_CTL_NO = @EDI_CTL_NO                           " & vbNewLine _
            & "AND H_OUTKAEDI_M.EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU                   " & vbNewLine _
            & "AND H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                  " & vbNewLine _
            & ""

#End Region ' "H_OUTKAEDI_M(次回分納情報)"

#End Region

#Region "UPDATE"

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine
#End Region

#Region "INKA_L"

    ''' <summary>
    ''' UPDATE（INKA_LのSET句（共通））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_L As String = "UPDATE $LM_TRN$..B_INKA_L SET                " & vbNewLine _
                                              & " INKA_STATE_KB        = @INOUTKA_STATE_KB    " & vbNewLine _
                                              & ",HOKAN_STR_DATE       = @HOKAN_STR_DATE      " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine

    ''' <summary>
    ''' UPDATE（INKA_LのSET句（入荷受付））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_L_UKE As String = ",UKETSUKE_DATE    = @SYS_UPD_DATE        " & vbNewLine _
                                                  & ",UKETSUKE_USER    = @SYS_UPD_USER        " & vbNewLine

    ''' <summary>
    ''' UPDATE（INKA_LのSET句（入荷検品））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_L_KEN As String = ",KEN_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                                  & ",KEN_USER         = @SYS_UPD_USER        " & vbNewLine

    ''' <summary>
    ''' UPDATE（INKA_LのSET句（入庫完了））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_L_KAN As String = ",INKO_DATE        = @SYS_UPD_DATE        " & vbNewLine _
                                                  & ",INKO_USER        = @SYS_UPD_USER        " & vbNewLine

    ''' <summary>
    ''' UPDATE（INKA_LのWHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_L_WHERE As String = "WHERE                                  " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & "AND INKA_NO_L         = @INOUTKA_NO_L        " & vbNewLine

#End Region

#Region "OUTKA_L"

    ''' <summary>
    ''' UPDATE（OUTKA_LのSET句（共通））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET              " & vbNewLine _
                                              & " OUTKA_STATE_KB       = @INOUTKA_STATE_KB    " & vbNewLine _
                                              & ",END_DATE             = @END_DATE            " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine

    ''' <summary>
    ''' UPDATE（OUTKA_LのSET句（出庫完了））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_L_KAN As String = ",OUTKO_USER        = @SYS_UPD_USER       " & vbNewLine

    ''' <summary>
    ''' UPDATE（OUTKA_LのSET句（出荷検品））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_L_KEN As String = ",KEN_USER          = @SYS_UPD_USER       " & vbNewLine

    ''' <summary>
    ''' UPDATE（OUTKA_LのSET句（出荷完了））
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_L_SKAN As String = ",OUTKA_USER       = @SYS_UPD_USER        " & vbNewLine

    ''' <summary>
    ''' UPDATE（OUTKA_LのWHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_L_WHERE As String = "WHERE                                 " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & "AND OUTKA_NO_L        = @INOUTKA_NO_L        " & vbNewLine

#End Region

#Region "ZAI_TRS"

    ''' <summary>
    ''' UPDATE（ZAI_TRSのSET句・WHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_ZAI As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET                  " & vbNewLine _
                                              & " SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & " WHERE                                       " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & "AND INKA_NO_L         = @INOUTKA_NO_L        " & vbNewLine _
                                              & "AND SYS_DEL_FLG       = '0'                  " & vbNewLine

    'START YANAI 要望番号653
    '''' <summary>
    '''' UPDATE（ZAI_TRSのSET句・WHERE句）(入庫完了時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_ZAI_INKOKANRYO As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET       " & vbNewLine _
    '                                          & " INKO_DATE            =                      " & vbNewLine _
    '                                          & " (SELECT                                     " & vbNewLine _
    '                                          & "  INKA_DATE                                  " & vbNewLine _
    '                                          & "  FROM $LM_TRN$..B_INKA_L                    " & vbNewLine _
    '                                          & "  WHERE                                      " & vbNewLine _
    '                                          & "  INKA_NO_L           = @INOUTKA_NO_L        " & vbNewLine _
    '                                          & "  AND SYS_DEL_FLG     = '0'                  " & vbNewLine _
    '                                          & " )                                           " & vbNewLine _
    '                                          & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
    '                                          & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
    '                                          & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
    '                                          & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
    '                                          & " WHERE                                       " & vbNewLine _
    '                                          & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
    '                                          & "AND INKA_NO_L         = @INOUTKA_NO_L        " & vbNewLine _
    '                                          & "AND SYS_DEL_FLG       = '0'                  " & vbNewLine
    ''' <summary>
    ''' UPDATE（ZAI_TRSのSET句・WHERE句）(入庫完了時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_ZAI_INKOKANRYO As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET       " & vbNewLine _
                                                  & " INKO_DATE            =                      " & vbNewLine _
                                                  & " (SELECT                                     " & vbNewLine _
                                                  & "  INKA_DATE                                  " & vbNewLine _
                                                  & "  FROM $LM_TRN$..B_INKA_L                    " & vbNewLine _
                                                  & "  WHERE                                      " & vbNewLine _
                                                  & "   NRS_BR_CD          = @NRS_BR_CD           " & vbNewLine _
                                                  & "  AND INKA_NO_L       = @INOUTKA_NO_L        " & vbNewLine _
                                                  & "  AND SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                                  & " )                                           " & vbNewLine _
                                                  & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                                  & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                                  & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                                  & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                                  & " WHERE                                       " & vbNewLine _
                                                  & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                                  & "AND ZAI_REC_NO        = @ZAI_REC_NO          " & vbNewLine _
                                                  & "AND SYS_UPD_DATE        = @ZAI_SYS_UPD_DATE  " & vbNewLine _
                                                  & "AND SYS_UPD_TIME        = @ZAI_SYS_UPD_TIME  " & vbNewLine _
                                                  & "AND SYS_DEL_FLG       = '0'                  " & vbNewLine
    'END YANAI 要望番号653

    'START YANAI 要望番号653
    '''' <summary>
    '''' UPDATE（ZAI_TRSのSET句・WHERE句）(出荷完了時）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_ZAI_KANRYO As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET                  " & vbNewLine _
    '                                          & " PORA_ZAI_NB          = @PORA_ZAI_NB                " & vbNewLine _
    '                                          & ",ALCTD_NB             = @ALCTD_NB                   " & vbNewLine _
    '                                          & ",PORA_ZAI_QT          = @PORA_ZAI_QT                " & vbNewLine _
    '                                          & ",ALCTD_QT             = @ALCTD_QT                   " & vbNewLine _
    '                                          & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
    '                                          & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
    '                                          & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
    '                                          & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
    '                                          & " WHERE                                       " & vbNewLine _
    '                                          & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
    '                                          & "AND ZAI_REC_NO        = @ZAI_REC_NO          " & vbNewLine _
    '                                          & "AND SYS_DEL_FLG       = '0'                  " & vbNewLine
    ''' <summary>
    ''' UPDATE（ZAI_TRSのSET句・WHERE句）(出荷完了時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_ZAI_KANRYO As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET                  " & vbNewLine _
                                              & " PORA_ZAI_NB          = @PORA_ZAI_NB                " & vbNewLine _
                                              & ",ALCTD_NB             = @ALCTD_NB                   " & vbNewLine _
                                              & ",PORA_ZAI_QT          = @PORA_ZAI_QT                " & vbNewLine _
                                              & ",ALCTD_QT             = @ALCTD_QT                   " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & " WHERE                                       " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & "AND ZAI_REC_NO        = @ZAI_REC_NO          " & vbNewLine _
                                              & "AND SYS_UPD_DATE        = @ZAI_SYS_UPD_DATE  " & vbNewLine _
                                              & "AND SYS_UPD_TIME        = @ZAI_SYS_UPD_TIME  " & vbNewLine _
                                              & "AND SYS_DEL_FLG       = '0'                  " & vbNewLine
    'END YANAI 要望番号653


#End Region

#Region "SAGYO"

    'START YANAI 要望番号968
    '''' <summary>
    '''' UPDATE（SAGYOのSET句・WHERE句）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_SAGYO As String = "UPDATE $LM_TRN$..E_SAGYO SET                  " & vbNewLine _
    '                                          & " SAGYO_COMP           = '01'                 " & vbNewLine _
    '                                          & ",SAGYO_COMP_CD        = @SYS_UPD_USER        " & vbNewLine _
    '                                          & ",SAGYO_COMP_DATE      = @INOUTKA_DATE        " & vbNewLine _
    '                                          & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
    '                                          & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
    '                                          & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
    '                                          & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
    '                                          & " WHERE                                       " & vbNewLine _
    '                                          & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
    '                                          & "AND INOUTKA_NO_LM     LIKE @INOUTKA_NO_L     " & vbNewLine
    'START YANAI 要望番号1031
    '''' <summary>
    '''' UPDATE（SAGYOのSET句・WHERE句）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_SAGYO As String = "UPDATE $LM_TRN$..E_SAGYO SET                  " & vbNewLine _
    '                                          & " SAGYO_COMP           = '01'                 " & vbNewLine _
    '                                          & ",SAGYO_COMP_CD        = @SYS_UPD_USER        " & vbNewLine _
    '                                          & ",SAGYO_COMP_DATE = (CASE WHEN SAGYO_COMP_DATE <> '' THEN SAGYO_COMP_DATE ELSE @SYS_UPD_DATE END) " & vbNewLine _
    '                                          & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
    '                                          & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
    '                                          & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
    '                                          & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
    '                                          & " WHERE                                       " & vbNewLine _
    '                                          & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
    '                                          & "AND INOUTKA_NO_LM     LIKE @INOUTKA_NO_L     " & vbNewLine
    ''' <summary>
    ''' UPDATE（SAGYOのSET句・WHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SAGYO As String = "UPDATE $LM_TRN$..E_SAGYO SET                  " & vbNewLine _
                                              & " SAGYO_COMP           = '01'                 " & vbNewLine _
                                              & ",SAGYO_COMP_CD        = @SYS_UPD_USER        " & vbNewLine _
                                              & ",SAGYO_COMP_DATE = (CASE WHEN SAGYO_COMP_DATE <> '' THEN SAGYO_COMP_DATE ELSE @INOUTKA_DATE END) " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & " WHERE                                       " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & "AND INOUTKA_NO_LM     LIKE @INOUTKA_NO_L     " & vbNewLine
    'END YANAI 要望番号1031
    'END YANAI 要望番号968

    ''' <summary>
    ''' UPDATE（入荷時のSAGYOのSET句・WHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SAGYO_IN As String = "AND IOZS_KB           IN ('10','11')       " & vbNewLine

    ''' <summary>
    ''' UPDATE（出荷時のSAGYOのSET句・WHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SAGYO_OUT As String = "AND IOZS_KB           IN ('20','21')      " & vbNewLine

    ''' <summary>
    ''' UPDATE（SAGYOのSET句・WHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SAGYO_SAGYOSIJI As String = "UPDATE $LM_TRN$..E_SAGYO SET                  " & vbNewLine _
                                              & " SAGYO_COMP           = '01'                 " & vbNewLine _
                                              & ",SAGYO_COMP_CD        = @SYS_UPD_USER        " & vbNewLine _
                                              & ",SAGYO_COMP_DATE = (CASE WHEN SAGYO_COMP_DATE <> '' THEN SAGYO_COMP_DATE ELSE @INOUTKA_DATE END) " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & " WHERE                                       " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & " AND SAGYO_SIJI_NO    = @INOUTKA_NO_L        " & vbNewLine _
                                              & " AND IOZS_KB = '30'                          " & vbNewLine

#End Region

#Region "OUTKA_M(振替)"

    'START YANAI 要望番号510
    '''' <summary>
    '''' UPDATE（OUTKA_MのSET句）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_OUTKA_M_FURI As String = "UPDATE $LM_TRN$..C_OUTKA_M SET         " & vbNewLine _
    '                                          & " GOODS_CD_NRS         = @GOODS_CD_NRS_FROM   " & vbNewLine _
    '                                          & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
    '                                          & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
    '                                          & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
    '                                          & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine

    '''' <summary>
    '''' UPDATE（OUTKA_MのWHERE句）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_OUTKA_M_WHERE_FURI As String = "WHERE                            " & vbNewLine _
    '                                          & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
    '                                          & "AND OUTKA_NO_L        = @OUTKA_NO_L          " & vbNewLine _
    '                                          & "AND OUTKA_NO_M        = @OUTKA_NO_M          " & vbNewLine
    'END YANAI 要望番号510

#End Region

#Region "OUTKA_S(振替)"

    'START YANAI 要望番号510
    '''' <summary>
    '''' UPDATE（OUTKA_SのSET句）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_OUTKA_S_FURI As String = "UPDATE $LM_TRN$..C_OUTKA_S SET         " & vbNewLine _
    '                                          & " ZAI_REC_NO           = @ZAI_REC_NO          " & vbNewLine _
    '                                          & ",INKA_NO_L            = @INKA_NO_L           " & vbNewLine _
    '                                          & ",INKA_NO_M            = @INKA_NO_M           " & vbNewLine _
    '                                          & ",INKA_NO_S            = @INKA_NO_S           " & vbNewLine _
    '                                          & ",ZAI_UPD_FLAG         = @ZAI_UPD_FLAG        " & vbNewLine _
    '                                          & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
    '                                          & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
    '                                          & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
    '                                          & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine
    ''' <summary>
    ''' UPDATE（OUTKA_SのSET句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_S_FURI As String = "UPDATE $LM_TRN$..C_OUTKA_S SET         " & vbNewLine _
                                              & " OUTKA_NO_L           = @OUTKA_NO_L_NEW      " & vbNewLine _
                                              & ",OUTKA_NO_M           = '001'                " & vbNewLine _
                                              & ",OUTKA_NO_S           = '001'                " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine
    'END YANAI 要望番号510

    ''' <summary>
    ''' UPDATE（OUTKA_MのWHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_S_WHERE_FURI As String = "WHERE                            " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & "AND OUTKA_NO_L        = @OUTKA_NO_L          " & vbNewLine _
                                              & "AND OUTKA_NO_M        = @OUTKA_NO_M          " & vbNewLine _
                                              & "AND OUTKA_NO_S        = @OUTKA_NO_S          " & vbNewLine

#End Region

#Region "作業指示"
    ''' <summary>
    ''' UPDATE（SAGYOSIJIのSET句・WHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SAGYOSIJI As String = "UPDATE $LM_TRN$..E_SAGYO_SIJI SET         " & vbNewLine _
                                              & " SAGYO_SIJI_STATUS    = '01'                 " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine

    ''' <summary>
    ''' UPDATE（SAGYOのWHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SAGYOSIJI_WHERE As String = "WHERE                               " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & "AND SAGYO_SIJI_NO     = @INOUTKA_NO_L        " & vbNewLine


#End Region

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

    ''' <summary>
    ''' メッセージ設定判定用フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _msgFlg As Boolean = False

#End Region

#Region "Method"

#Region "検索処理"

#Region "入荷検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行（入荷）</remarks>
    Private Function SelectINKAData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_COUNT_INKA)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_INKA)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereINKA(inTbl)                           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_INKA)  'SQL構築(データ抽出用Group by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_END_INKA)  'SQL構築(データ抽出用)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectINKAData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_TBL_EXISTS")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_GET_TRN_TBL_EXISTS)

        ' パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TBL_NM", Me._Row.Item("TBL_NM").ToString(), DBDataType.NVARCHAR))

        ' スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        Me._Row.Item("TBL_EXISTS") = "0"

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

            ' パラメータの反映
            For Each obj As Object In _SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMR010DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.Read() Then
                    Me._Row.Item("TBL_EXISTS") = Convert.ToString(reader("TBL_EXISTS"))
                End If

            End Using

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListINKAData(ByVal ds As DataSet) As DataSet

        Dim inkaediDtlTsmcExists As Boolean = False
        Dim zaiTsmcExists As Boolean = False
        Dim drExists As DataRow()
        drExists = ds.Tables("LMR010_TBL_EXISTS").Select("TBL_NM = 'H_INKAEDI_DTL_TSMC'")
        If drExists.Count > 0 AndAlso drExists(0).Item("TBL_EXISTS").ToString() = "1" Then
            inkaediDtlTsmcExists = True
        End If
        drExists = ds.Tables("LMR010_TBL_EXISTS").Select("TBL_NM = 'D_ZAI_TSMC'")
        If drExists.Count > 0 AndAlso drExists(0).Item("TBL_EXISTS").ToString() = "1" Then
            zaiTsmcExists = True
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_INKA)      'SQL構築(データ抽出用Select句)
        If inkaediDtlTsmcExists AndAlso zaiTsmcExists Then
            Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_INKA2_TSMC)
        Else
            Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_INKA2)
        End If
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_INKA)      'SQL構築(データ抽出用From句)
        If inkaediDtlTsmcExists AndAlso zaiTsmcExists Then
            Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_INKA2_TSMC)
        End If
        Me.SQLSelectWhereINKA(inTbl)                           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_INKA)  'SQL構築(データ抽出用Group by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_ORDER_BY_INKA)  'SQL構築(データ抽出用Order by句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectListINKAData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("INOUTKA_ORD_NO", "INOUTKA_ORD_NO")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUTKA_STATE_KB", "INOUTKA_STATE_KB")
        map.Add("INKA_KENPIN_YN", "INKA_KENPIN_YN")
        map.Add("INKA_KAKUNIN_YN", "INKA_KAKUNIN_YN")
        map.Add("LOC_MANAGER_YN", "LOC_MANAGER_YN")
        map.Add("OKIBA_COUNT", "OKIBA_COUNT")
        map.Add("HOKAN_STR_DATE", "HOKAN_STR_DATE")
        map.Add("SYORI_FLG", "SYORI_FLG")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("CHK_INKA_STATE_KB", "CHK_INKA_STATE_KB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        'START YANAI 要望番号433
        map.Add("OKIBA_COUNT2", "OKIBA_COUNT2")
        'END YANAI 要望番号433
        'START YANAI 要望番号932
        map.Add("SCNT", "SCNT")
        'END YANAI 要望番号932

        map.Add("WH_KENPIN_WK_STATUS", "WH_KENPIN_WK_STATUS")

        map.Add("TSMC_QTY_SUMI", "TSMC_QTY_SUMI")
        map.Add("TSMC_QTY", "TSMC_QTY")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010INOUT")

        Return ds

    End Function

    'START YANAI 要望番号653
    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListINKAZAIData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_INKA_ZAI)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_INKA_ZAI)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereINKA(inTbl)                           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_INKA_ZAI)  'SQL構築(データ抽出用Group by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_ORDER_BY_INKA_ZAI)  'SQL構築(データ抽出用Order by句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectListINKAZAIData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()


        '取得データの格納先をマッピング
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("INOUTKA_ORD_NO", "INOUTKA_ORD_NO")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUTKA_STATE_KB", "INOUTKA_STATE_KB")
        map.Add("INKA_KENPIN_YN", "INKA_KENPIN_YN")
        map.Add("INKA_KAKUNIN_YN", "INKA_KAKUNIN_YN")
        map.Add("LOC_MANAGER_YN", "LOC_MANAGER_YN")
        map.Add("OKIBA_COUNT", "OKIBA_COUNT")
        map.Add("HOKAN_STR_DATE", "HOKAN_STR_DATE")
        map.Add("SYORI_FLG", "SYORI_FLG")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("CHK_INKA_STATE_KB", "CHK_INKA_STATE_KB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("OKIBA_COUNT2", "OKIBA_COUNT2")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("ZAI_SYS_UPD_DATE", "ZAI_SYS_UPD_DATE")
        map.Add("ZAI_SYS_UPD_TIME", "ZAI_SYS_UPD_TIME")
        'START YANAI 要望番号932
        map.Add("SCNT", "SCNT")
        'END YANAI 要望番号932

        map.Add("CHK_KENPIN_WK_ON", "CHK_KENPIN_WK_ON")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_ZAI")

        Return ds

    End Function
    'END YANAI 要望番号653

    ''' <summary>
    ''' 検索用SQL WHERE句作成(入荷)
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhereINKA(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append("WHERE                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("INKA_L.SYS_DEL_FLG  = '0'                                    ")
        Me._StrSql.Append(vbNewLine)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With _Row

            '完了種別
            whereStr = .Item("KANRYO_SYUBETU").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Select Case whereStr
                    Case "01"   '入荷受付
                        Me._StrSql.Append(" AND (SOKO.INKA_UKE_PRT_YN = '01'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (INKA_L.INKA_STATE_KB = '10'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR   INKA_L.INKA_STATE_KB = '20'))")
                        Me._StrSql.Append(vbNewLine)
                    Case "02"   '入荷検品
                        Me._StrSql.Append(" AND (SOKO.INKA_KENPIN_YN = '01'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (INKA_L.INKA_STATE_KB = '30'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR  (SOKO.INKA_UKE_PRT_YN = '00'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (INKA_L.INKA_STATE_KB = '10'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR   INKA_L.INKA_STATE_KB = '20'))))")
                        Me._StrSql.Append(vbNewLine)
                    Case "03"   '入荷完了
                        Me._StrSql.Append(" AND (INKA_L.INKA_STATE_KB = '40'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR   SOKO.INKA_KENPIN_YN = '00'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (INKA_L.INKA_STATE_KB = '30'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR  (SOKO.INKA_UKE_PRT_YN = '00'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (INKA_L.INKA_STATE_KB = '10'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR   INKA_L.INKA_STATE_KB = '20'))))")
                        Me._StrSql.Append(vbNewLine)
                        'Del Start 2019/10/10 要望管理007373
                        ''Add Start 2019/08/01 要望管理005237
                        'Me._StrSql.Append(" AND INKA_L.STOP_ALLOC <> '1'  -- ADD 2019/08/01 要望管理005237")
                        'Me._StrSql.Append(vbNewLine)
                        ''Add End   2019/08/01 要望管理005237
                        'Del End   2019/10/10 要望管理007373
                End Select
            End If

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '担当者コード
            whereStr = .Item("TANTO_USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'START YANAI 要望番号824
                'Me._StrSql.Append(" AND MTCUST.USER_CD LIKE @TANTO_USER_CD")
                Me._StrSql.Append(" AND SUSER.USER_CD LIKE @TANTO_USER_CD")
                'END YANAI 要望番号824
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主コード
            whereStr = .Item("CUST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKA_L.CUST_CD_L LIKE @CUST_CD")
                Me._StrSql.Append(vbNewLine)
                '要望番号1631 修正START
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
                '要望番号1631 修正END
            End If

            '入出荷日（FROM)
            whereStr = .Item("INOUTKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKA_L.INKA_DATE >= @INOUTKA_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入出荷日（TO)
            whereStr = .Item("INOUTKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKA_L.INKA_DATE <= @INOUTKA_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '管理番号（大）
            whereStr = .Item("INOUTKA_NO_L").ToString()
            If 1 < dt.Rows.Count Then
                '初期検索時のみ、複数件ある場合がある。
                Dim max As Integer = dt.Rows.Count - 1
                For i As Integer = 0 To max
                    whereStr = dt.Rows(i).Item("INOUTKA_NO_L").ToString()
                    If 0 = i Then
                        Me._StrSql.Append(" AND INKA_L.INKA_NO_L IN (")
                        Me._StrSql.Append(vbNewLine)
                    Else
                        Me._StrSql.Append(",")
                        Me._StrSql.Append(vbNewLine)
                    End If
                    Me._StrSql.Append(String.Concat("'", whereStr, "'"))
                    Me._StrSql.Append(vbNewLine)
                Next
                Me._StrSql.Append(")")
                Me._StrSql.Append(vbNewLine)
            Else
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND INKA_L.INKA_NO_L LIKE @INOUTKA_NO_L")
                    Me._StrSql.Append(vbNewLine)
                    '要望番号1631 修正START
                    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
                    '要望番号1631 修正END
                End If
            End If

            'オーダー番号
            whereStr = .Item("INOUTKA_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKA_L.OUTKA_FROM_ORD_NO_L LIKE @INOUTKA_ORD_NO")
                Me._StrSql.Append(vbNewLine)
                '要望番号1631 修正START
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_ORD_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
                '要望番号1631 修正END
            End If

            '担当者
            whereStr = .Item("TANTO_USER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUSER.USER_NM LIKE @TANTO_USER_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                '入荷に届先なし
            End If

        End With

    End Sub

#End Region

#Region "分析票管理マスタ検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行（分析票管理マスタ）</remarks>
    Private Function SelectCOAData(ByVal ds As DataSet) As Integer

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_COUNT_COA)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_COA)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_COA3)     'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_COA)     'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_COA2)     'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetCoaComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectCOAData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim selectCnt As Integer = Convert.ToInt32(reader("SELECT_CNT"))
        reader.Close()
        Return selectCnt

    End Function

#End Region

#Region "分析票管理マスタ検索処理"
    'START YANAI 要望番号743
    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行（荷主明細マスタ）</remarks>
    Private Function SelectCUSTDETAILSData(ByVal ds As DataSet) As Integer

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_COUNT_COA)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_COA)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_CUSTDETAILS) 'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_COA)     'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetCoaComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectCUSTDETAILSData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim selectCnt As Integer = Convert.ToInt32(reader("SELECT_CNT"))
        reader.Close()
        Return selectCnt

    End Function
    'END YANAI 要望番号743
#End Region

#Region "出荷検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行（出荷）</remarks>
    Private Function SelectOUTKAData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'START YANAI 要望番号1295 完了画面・検索速度改善
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'END YANAI 要望番号1295 完了画面・検索速度改善

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_COUNT_OUTKA)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OUTKA)      'SQL構築(データ抽出用From句)
        'START YANAI 要望番号1295 完了画面・検索速度改善
        Me.SQLSelectWhereOUTKANO(inTbl)                                'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OUTKA2)      'SQL構築(データ抽出用From句)
        'END YANAI 要望番号1295 完了画面・検索速度改善
        Me.SQLSelectWhereOUTKA(inTbl)                                'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_OUTKA)  'SQL構築(データ抽出用Group by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_END_OUTKA)  'SQL構築(データ抽出用)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectOUTKAData", cmd)

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
    Private Function SelectListOUTKAData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'START YANAI 要望番号1295 完了画面・検索速度改善
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'END YANAI 要望番号1295 完了画面・検索速度改善

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_OUTKA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OUTKA)      'SQL構築(データ抽出用From句)
        'START YANAI 要望番号1295 完了画面・検索速度改善
        Me.SQLSelectWhereOUTKANO(inTbl)                                'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OUTKA2)      'SQL構築(データ抽出用From句)
        'END YANAI 要望番号1295 完了画面・検索速度改善
        Me.SQLSelectWhereOUTKA(inTbl)                           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_OUTKA)  'SQL構築(データ抽出用Group by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_ORDER_BY_OUTKA)  'SQL構築(データ抽出用Order by句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectListOUTKAData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("INOUTKA_ORD_NO", "INOUTKA_ORD_NO")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUTKA_STATE_KB", "INOUTKA_STATE_KB")
        map.Add("OUTKA_KENPIN_YN", "OUTKA_KENPIN_YN")
        map.Add("OUTKA_INFO_YN", "OUTKA_INFO_YN")
        map.Add("END_DATE", "END_DATE")
        map.Add("NIHUDA_FLAG", "NIHUDA_FLAG")
        map.Add("SYORI_FLG", "SYORI_FLG")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("CHK_INKA_STATE_KB", "CHK_INKA_STATE_KB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        'START YANAI 要望番号932
        map.Add("SCNT", "SCNT")
        'END YANAI 要望番号932

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010INOUT")

        Return ds

    End Function

    'START YANAI 要望番号653
    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListOUTKAZAIData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'START YANAI 要望番号1295 完了画面・検索速度改善
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'END YANAI 要望番号1295 完了画面・検索速度改善

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_OUTKA_ZAI)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OUTKA_ZAI)      'SQL構築(データ抽出用From句)
        'START YANAI 要望番号1295 完了画面・検索速度改善
        Me.SQLSelectWhereOUTKANO(inTbl)                                'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OUTKA_ZAI2)      'SQL構築(データ抽出用From句)
        'END YANAI 要望番号1295 完了画面・検索速度改善
        Me.SQLSelectWhereOUTKA(inTbl)                           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_OUTKA_ZAI)  'SQL構築(データ抽出用Group by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_ORDER_BY_OUTKA_ZAI)  'SQL構築(データ抽出用Order by句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectListOUTKAZAIData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("INOUTKA_ORD_NO", "INOUTKA_ORD_NO")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUTKA_STATE_KB", "INOUTKA_STATE_KB")
        map.Add("OUTKA_KENPIN_YN", "OUTKA_KENPIN_YN")
        map.Add("OUTKA_INFO_YN", "OUTKA_INFO_YN")
        map.Add("END_DATE", "END_DATE")
        map.Add("NIHUDA_FLAG", "NIHUDA_FLAG")
        map.Add("SYORI_FLG", "SYORI_FLG")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("CHK_INKA_STATE_KB", "CHK_INKA_STATE_KB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("ZAI_SYS_UPD_DATE", "ZAI_SYS_UPD_DATE")
        map.Add("ZAI_SYS_UPD_TIME", "ZAI_SYS_UPD_TIME")
        'START YANAI 要望番号932
        map.Add("SCNT", "SCNT")
        'END YANAI 要望番号932

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_ZAI")

        Return ds

    End Function
    'END YANAI 要望番号653

    ''' <summary>
    ''' 検索用SQL WHERE句作成(出荷)
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhereOUTKA(ByVal dt As DataTable)

        'START YANAI 要望番号1295 完了画面・検索速度改善
        ''SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()
        'END YANAI 要望番号1295 完了画面・検索速度改善

        Me._StrSql.Append("WHERE                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTKA_L.SYS_DEL_FLG  = '0'                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND OUTKA_L.SUM_BACKLOG_NB  = 0                                    ")
        Me._StrSql.Append(vbNewLine)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With _Row

            '完了種別
            whereStr = .Item("KANRYO_SYUBETU").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Select Case whereStr
                    Case "04"   '出庫完了
                        Me._StrSql.Append(" AND (SOKO.OUTOKA_KANRYO_YN = '01'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (OUTKA_L.OUTKA_STATE_KB = '30'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR  (SOKO.OUTKA_SASHIZU_PRT_YN = '00'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (OUTKA_L.OUTKA_STATE_KB = '10'))))")
                        Me._StrSql.Append(vbNewLine)
                    Case "05"   '出荷検品
                        Me._StrSql.Append(" AND (SOKO.OUTKA_KENPIN_YN = '01'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (OUTKA_L.OUTKA_STATE_KB = '40'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR (SOKO.OUTOKA_KANRYO_YN = '00'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (OUTKA_L.OUTKA_STATE_KB = '30'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" OR  (SOKO.OUTKA_SASHIZU_PRT_YN = '00'")
                        Me._StrSql.Append(vbNewLine)
                        Me._StrSql.Append(" AND (OUTKA_L.OUTKA_STATE_KB = '10'))))))")
                        Me._StrSql.Append(vbNewLine)
                    Case "06"   '出荷完了

                        Me._StrSql.AppendLine(" AND (")
                        Me._StrSql.AppendLine("           OUTKA_L.OUTKA_STATE_KB = '50' ")
                        Me._StrSql.AppendLine("       OR (SOKO.OUTKA_KENPIN_YN = '00' AND OUTKA_L.OUTKA_STATE_KB = '40') ")
                        Me._StrSql.AppendLine("       OR (SOKO.OUTKA_KENPIN_YN = '00' AND SOKO.OUTOKA_KANRYO_YN = '00' AND OUTKA_L.OUTKA_STATE_KB = '30') ")
                        Me._StrSql.AppendLine("	      OR (SOKO.OUTKA_KENPIN_YN = '00' AND SOKO.OUTOKA_KANRYO_YN = '00' AND SOKO.OUTKA_SASHIZU_PRT_YN = '00' AND OUTKA_L.OUTKA_STATE_KB = '10') ")
                        Me._StrSql.AppendLine("     )")

                End Select
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRYO_SYUBETU", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '担当者コード
            whereStr = .Item("TANTO_USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'START YANAI 要望番号824
                'Me._StrSql.Append(" AND MTCUST.USER_CD LIKE @TANTO_USER_CD")
                Me._StrSql.Append(" AND SUSER.USER_CD LIKE @TANTO_USER_CD")
                'END YANAI 要望番号824
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主コード
            whereStr = .Item("CUST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.CUST_CD_L LIKE @CUST_CD")
                Me._StrSql.Append(vbNewLine)
                '要望番号1631 修正START
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
                '要望番号1631 修正END
            End If

            '入出荷日（FROM)
            whereStr = .Item("INOUTKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.OUTKA_PLAN_DATE >= @INOUTKA_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入出荷日（TO)
            whereStr = .Item("INOUTKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.OUTKA_PLAN_DATE <= @INOUTKA_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '管理番号（大）
            If 1 < dt.Rows.Count Then
                '初期検索時のみ、複数件ある場合がある。
                Dim max As Integer = dt.Rows.Count - 1
                For i As Integer = 0 To max
                    whereStr = dt.Rows(i).Item("INOUTKA_NO_L").ToString()
                    If 0 = i Then
                        Me._StrSql.Append(" AND OUTKA_L.OUTKA_NO_L IN (")
                        Me._StrSql.Append(vbNewLine)
                    Else
                        Me._StrSql.Append(",")
                        Me._StrSql.Append(vbNewLine)
                    End If
                    Me._StrSql.Append(String.Concat("'", whereStr, "'"))
                    Me._StrSql.Append(vbNewLine)
                Next
                Me._StrSql.Append(")")
                Me._StrSql.Append(vbNewLine)
            Else
                whereStr = .Item("INOUTKA_NO_L").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND OUTKA_L.OUTKA_NO_L LIKE @INOUTKA_NO_L")
                    Me._StrSql.Append(vbNewLine)
                    '2012.11.26 要望番号1631 修正START
                    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
                    '2012.11.26 要望番号1631 修正END
                End If
            End If

            'オーダー番号
            whereStr = .Item("INOUTKA_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.CUST_ORD_NO LIKE @INOUTKA_ORD_NO")
                Me._StrSql.Append(vbNewLine)
                '要望番号1631 修正START
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_ORD_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
                '要望番号1631 修正END
            End If

            '担当者
            whereStr = .Item("TANTO_USER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUSER.USER_NM LIKE @TANTO_USER_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCUST.CUST_NM_L + '　' +  MCUST.CUST_NM_M LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (MDEST.DEST_NM LIKE @DEST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR  OUTKA_L.DEST_NM LIKE @DEST_NM)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    'START YANAI 要望番号1295 完了画面・検索速度改善
    ''' <summary>
    ''' 検索用SQL WHERE句作成(出荷)
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhereOUTKANO(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append("WHERE                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTKA_L2.SYS_DEL_FLG  = '0'                                    ")
        Me._StrSql.Append(vbNewLine)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With _Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L2.NRS_BR_CD = @NRS_BR_CD2")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD2", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '管理番号（大）
            If 1 < dt.Rows.Count Then
                '初期検索時のみ、複数件ある場合がある。
                Dim max As Integer = dt.Rows.Count - 1
                For i As Integer = 0 To max
                    whereStr = dt.Rows(i).Item("INOUTKA_NO_L").ToString()
                    If 0 = i Then
                        Me._StrSql.Append(" AND OUTKA_L2.OUTKA_NO_L IN (")
                        Me._StrSql.Append(vbNewLine)
                    Else
                        Me._StrSql.Append(",")
                        Me._StrSql.Append(vbNewLine)
                    End If
                    Me._StrSql.Append(String.Concat("'", whereStr, "'"))
                    Me._StrSql.Append(vbNewLine)
                Next
                Me._StrSql.Append(")")
                Me._StrSql.Append(vbNewLine)
            Else
                whereStr = .Item("INOUTKA_NO_L").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND OUTKA_L2.OUTKA_NO_L LIKE @INOUTKA_NO_L2")
                    Me._StrSql.Append(vbNewLine)
                    '2012.11.26 要望番号1631 修正START
                    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L2", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L2", String.Concat(whereStr, "%"), DBDataType.CHAR))
                    '2012.11.26 要望番号1631 修正END
                End If
            End If

        End With

    End Sub
    'END YANAI 要望番号1295 完了画面・検索速度改善

    ''' <summary>
    ''' 更新データ検索（出荷データ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>更新データ取得SQLの構築・発行</remarks>
    Private Function SelectListOUTKADataKANRYO(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_IN_CHECK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_OUTKAKANRYO)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OUTKAKANRYO)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereOUTKAKANRYO(inTbl)                           'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetCheckComParameter(inTbl, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectListOUTKADataKANRYO", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        'START YANAI 20110906 サンプル対応
        map.Add("ALCTD_KB", "ALCTD_KB")
        'END YANAI 20110906 サンプル対応

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_OUTKAS_UPDDATA")


    End Function

    ''' <summary>
    ''' 更新データ検索（在庫データ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>更新データ取得SQLの構築・発行</remarks>
    Private Function SelectListZAIDataKANRYO(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_IN_CHECK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_ZAIKANRYO)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_ZAIKANRYO)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereOUTKAKANRYO(inTbl)                           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_ZAIKANRYO2)      'SQL構築(データ抽出用From句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetCheckComParameter(inTbl, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectListZAIDataKANRYO", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("ALCTD_QT", "ALCTD_QT")
        'START YANAI 20110913 小分け対応
        map.Add("IRIME", "IRIME")
        'END YANAI 20110913 小分け対応

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_ZAI_UPDDATA")

    End Function

    ''' <summary>
    ''' 検索用SQL WHERE句作成(出荷)
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhereOUTKAKANRYO(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append("AND                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("(                                                          ")
        Me._StrSql.Append(vbNewLine)

        '出荷管理番号(大)はWHERE句にてINを使用しているため、結合処理を行った後に設定
        Dim max As Integer = dt.Rows.Count - 1
        Dim outkanoL As String = String.Empty
        For i As Integer = 0 To max
            If String.IsNullOrEmpty(outkanoL) = False Then
                outkanoL = String.Concat(outkanoL, "OR")
            End If
            outkanoL = String.Concat(outkanoL, " OUTKA_S.OUTKA_NO_L  = '", dt.Rows(i).Item("INOUTKA_NO_L").ToString(), "'")
        Next
        Me._StrSql.Append(outkanoL)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(")")
        Me._StrSql.Append(vbNewLine)

    End Sub

#End Region

#Region "他荷主在庫処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行（他荷主在庫）</remarks>
    Private Function SelectOTHERData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_COUNT_OTHER)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OTHER)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_OTHER)     'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetOtherComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectOTHERData", cmd)

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
    Private Function SelectListOTHERData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_OTHER)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_OTHER)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_OTHER)     'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetOtherComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectListOTHERData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L_OUTKA", "CUST_CD_L_OUTKA")
        map.Add("CUST_CD_M_OUTKA", "CUST_CD_M_OUTKA")
        map.Add("CUST_CD_L_ZAI", "CUST_CD_L_ZAI")
        map.Add("CUST_CD_M_ZAI", "CUST_CD_M_ZAI")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("WH_CD", "WH_CD")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("HOKAN_FREE_KIKAN", "HOKAN_FREE_KIKAN")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("IRIME", "IRIME")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("REMARK", "REMARK")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("DENP_NO", "DENP_NO")
        map.Add("ARR_KANRYO_INFO", "ARR_KANRYO_INFO")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_CD_M", "SHIP_CD_M")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("NHS_REMARK", "NHS_REMARK")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_L_REMARK", "OUTKA_L_REMARK")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("DENP_YN", "DENP_YN")
        map.Add("PC_KB", "PC_KB")
        map.Add("ALL_PRINT_FLAG", "ALL_PRINT_FLAG")
        map.Add("NIHUDA_FLAG", "NIHUDA_FLAG")
        map.Add("NHS_FLAG", "NHS_FLAG")
        map.Add("DENP_FLAG", "DENP_FLAG")
        map.Add("COA_FLAG", "COA_FLAG")
        map.Add("HOKOKU_FLAG", "HOKOKU_FLAG")
        map.Add("MATOME_PICK_FLAG", "MATOME_PICK_FLAG")
        map.Add("LAST_PRINT_DATE", "LAST_PRINT_DATE")
        map.Add("LAST_PRINT_TIME", "LAST_PRINT_TIME")
        map.Add("SASZ_USER", "SASZ_USER")
        map.Add("OUTKO_USER", "OUTKO_USER")
        map.Add("KEN_USER", "KEN_USER")
        map.Add("OUTKA_USER", "OUTKA_USER")
        map.Add("HOU_USER", "HOU_USER")
        map.Add("ORDER_TYPE", "ORDER_TYPE")
        map.Add("EDI_SET_NO", "EDI_SET_NO")
        map.Add("COA_YN_M", "COA_YN_M")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("OUTKA_PKG_NB_M", "OUTKA_PKG_NB_M")
        map.Add("IRIME_M", "IRIME_M")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("OUTKA_M_PKG_NB", "OUTKA_M_PKG_NB")
        map.Add("OUTKA_M_REMARK", "OUTKA_M_REMARK")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("ZAIKO_KB", "ZAIKO_KB")
        map.Add("SOURCE_CD", "SOURCE_CD")
        map.Add("YELLOW_CARD", "YELLOW_CARD")
        map.Add("GOODS_CD_NRS_FROM", "GOODS_CD_NRS_FROM")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("ALCTD_CAN_NB", "ALCTD_CAN_NB")
        map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
        map.Add("SMPL_FLAG", "SMPL_FLAG")
        map.Add("RSV_NO_ZAI", "RSV_NO_ZAI")
        map.Add("SERIAL_NO_ZAI", "SERIAL_NO_ZAI")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("DEST_CD_ZAI", "DEST_CD_ZAI")
        map.Add("SMPL_FLAG_ZAI", "SMPL_FLAG_ZAI")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_DATE_OUT_S", "SYS_UPD_DATE_OUT_S")
        map.Add("SYS_UPD_TIME_OUT_S", "SYS_UPD_TIME_OUT_S")
        map.Add("PKG_NB", "PKG_NB")
        'START YANAI 要望番号589
        map.Add("TAX_KB", "TAX_KB")
        'END YANAI 要望番号589
        'START YANAI 要望番号510
        map.Add("DEST_KB", "DEST_KB")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        'END YANAI 要望番号510

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_FURIKAE")

        Return ds

    End Function


#End Region

#Region "小分け出荷検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectKowakeData(ByVal ds As DataSet) As Integer

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_COUNT_KOWAKE)   'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_KOWAKE)    'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_KOWAKE)   'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetKowakeComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectKowakeData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim selectCnt As Integer = Convert.ToInt32(reader("SELECT_CNT"))
        reader.Close()
        Return selectCnt

    End Function

#End Region

#Region "梱包作業検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectSAGYOData(ByVal ds As DataSet) As Integer

        'START YANAI 要望番号653
        'メッセージ設定フラグ初期化
        Me._msgFlg = False
        'END YANAI 要望番号653

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_COUNT_SAGYO)   'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_SAGYO)    'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_SAGYO2)   'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_SAGYO)   'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetSagyoComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectSAGYOData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim selectCnt As Integer = Convert.ToInt32(reader("SELECT_CNT"))
        reader.Close()
        Return selectCnt

    End Function

#End Region

#Region "入荷チェックデータ検索処理"

    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectCheckINKAData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_IN_CHECK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_CHECK_INKA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_CHECK_INKA) 'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_CHECK_INKA) 'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_ORDER_BY_CHECK_INKA)  'SQL構築(データ抽出用Order by句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetCheckInkaComParameter(inTbl, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectCheckINKAData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LOT_CTL_KB", "LOT_CTL_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_OUT_CHECK")

        Return ds

    End Function

#End Region

#Region "在庫チェックデータ検索処理"

    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectCheckZAIData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_IN_CHECK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_CHECK_ZAI)       'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_CHECK_ZAI)  'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_WHERE_CHECK_ZAI) 'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_CHECK_ZAI) 'SQL構築(データ抽出用Order by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_ORDER_BY_CHECK_ZAI) 'SQL構築(データ抽出用Order by句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetCheckZaiComParameter(inTbl, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectCheckZAIData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_OUT_CHECK")

        Return ds

    End Function

#End Region

#Region "入荷進捗区分チェックデータ検索処理"

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectCheckDataInka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_CHECK_INKA_STATE)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_CHECK_INKA_STATE) 'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereINKASTATE(inTbl)                             'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectCheckDataInka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("STATE_KB", "STATE_KB")
        map.Add("INKA_UKE_PRT_YN", "INKA_UKE_PRT_YN")
        map.Add("INKA_KENPIN_YN", "INKA_KENPIN_YN")
        map.Add("INKA_KAKUNIN_YN", "INKA_KAKUNIN_YN")
        map.Add("OUTKA_SASHIZU_PRT_YN", "OUTKA_SASHIZU_PRT_YN")
        map.Add("OUTOKA_KANRYO_YN", "OUTOKA_KANRYO_YN")
        map.Add("OUTKA_KENPIN_YN", "OUTKA_KENPIN_YN")
        map.Add("OUTKA_INFO_YN", "OUTKA_INFO_YN")
        map.Add("BACKLOG_NB", "BACKLOG_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_STATECHK")

        Return ds

    End Function

    ''' <summary>
    ''' 検索用SQL WHERE句作成(入荷)
    ''' </summary>
    ''' <remarks>検索用SQLの構築</remarks>
    Private Sub SQLSelectWhereINKASTATE(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append("WHERE                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("INKA_L.SYS_DEL_FLG  = '0'                                    ")
        Me._StrSql.Append(vbNewLine)
        'Del Start 2019/10/10 要望管理007373
        ''Add Start 2019/08/01 要望管理005237
        'Me._StrSql.Append(" AND INKA_L.STOP_ALLOC <> '1'  -- ADD 2019/08/01 要望管理005237")
        'Me._StrSql.Append(vbNewLine)
        ''Add End   2019/08/01 要望管理005237
        'Del End   2019/10/10 要望管理007373

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With _Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '管理番号（大）
            whereStr = .Item("INOUTKA_NO_L").ToString()
            If 1 < dt.Rows.Count Then
                '初期検索時のみ、複数件ある場合がある。
                Dim max As Integer = dt.Rows.Count - 1
                For i As Integer = 0 To max
                    whereStr = dt.Rows(i).Item("INOUTKA_NO_L").ToString()
                    If 0 = i Then
                        Me._StrSql.Append(" AND INKA_L.INKA_NO_L IN (")
                        Me._StrSql.Append(vbNewLine)
                    Else
                        Me._StrSql.Append(",")
                        Me._StrSql.Append(vbNewLine)
                    End If
                    Me._StrSql.Append(String.Concat("'", whereStr, "'"))
                    Me._StrSql.Append(vbNewLine)
                Next
                Me._StrSql.Append(")")
                Me._StrSql.Append(vbNewLine)
            Else
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND INKA_L.INKA_NO_L = @INOUTKA_NO_L")
                    Me._StrSql.Append(vbNewLine)
                    '要望番号1631　修正START
                    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", whereStr, DBDataType.NVARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", whereStr, DBDataType.CHAR))
                    '要望番号1631　修正START
                End If
            End If

        End With

    End Sub

#End Region

#Region "出荷進捗区分チェックデータ検索処理"

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectCheckDataOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_CHECK_OUTKA_STATE)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_CHECK_OUTKA_STATE) 'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereOUTKASTATE(inTbl)                             'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_CHECK_OUTKA_STATE) 'SQL構築(データ抽出用Group by句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectCheckDataOutka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("STATE_KB", "STATE_KB")
        map.Add("INKA_UKE_PRT_YN", "INKA_UKE_PRT_YN")
        map.Add("INKA_KENPIN_YN", "INKA_KENPIN_YN")
        map.Add("INKA_KAKUNIN_YN", "INKA_KAKUNIN_YN")
        map.Add("OUTKA_SASHIZU_PRT_YN", "OUTKA_SASHIZU_PRT_YN")
        map.Add("OUTOKA_KANRYO_YN", "OUTOKA_KANRYO_YN")
        map.Add("OUTKA_KENPIN_YN", "OUTKA_KENPIN_YN")
        map.Add("OUTKA_INFO_YN", "OUTKA_INFO_YN")
        map.Add("BACKLOG_NB", "BACKLOG_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_STATECHK")

        Return ds

    End Function

    ''' <summary>
    ''' 検索用SQL WHERE句作成(出荷)
    ''' </summary>
    ''' <remarks>検索用SQLの構築</remarks>
    Private Sub SQLSelectWhereOUTKASTATE(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append("WHERE                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTKA_L.SYS_DEL_FLG  = '0'                                   ")
        Me._StrSql.Append(vbNewLine)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With _Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '管理番号（大）
            whereStr = .Item("INOUTKA_NO_L").ToString()
            If 1 < dt.Rows.Count Then
                '初期検索時のみ、複数件ある場合がある。
                Dim max As Integer = dt.Rows.Count - 1
                For i As Integer = 0 To max
                    whereStr = dt.Rows(i).Item("INOUTKA_NO_L").ToString()
                    If 0 = i Then
                        Me._StrSql.Append(" AND OUTKA_L.OUTKA_NO_L IN (")
                        Me._StrSql.Append(vbNewLine)
                    Else
                        Me._StrSql.Append(",")
                        Me._StrSql.Append(vbNewLine)
                    End If
                    Me._StrSql.Append(String.Concat("'", whereStr, "'"))
                    Me._StrSql.Append(vbNewLine)
                Next
                Me._StrSql.Append(")")
                Me._StrSql.Append(vbNewLine)
            Else
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND OUTKA_L.OUTKA_NO_L = @INOUTKA_NO_L")
                    Me._StrSql.Append(vbNewLine)
                    '要望番号1631 修正START
                    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", whereStr, DBDataType.NVARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", whereStr, DBDataType.CHAR))
                    '要望番号1631 修正START
                End If
            End If

        End With

    End Sub

#End Region

#Region "荷主データ取得処理"

    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_CUST_IN")


        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_CUST)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_FROM_CUST) 'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMR010DAC.SQL_WHERE_CUST) 'SQL構築(データ抽出用Where句)

        Dim max As Integer = inTbl.Rows.Count - 1
        Me._StrSql.Append("AND ( ")
        For i As Integer = 0 To max
            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            If i <> 0 Then
                Me._StrSql.Append(" OR ")

            End If
            Me._StrSql.Append(String.Concat("INKAL.INKA_NO_L = '", Me._Row.Item("INKA_NO_L"), "'"))
        Next
        Me._StrSql.Append(")")

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータ設定
        Call Me.SetCustParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectCustData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("HOKAN_NIYAKU_CALCULATION", "HOKAN_NIYAKU_CALCULATION")
        map.Add("INKA_NO_L", "INKA_NO_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010_CUST")

        Return ds

    End Function

#End Region

#Region "作業検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行（入荷）</remarks>
    Private Function SelectSagyoSijiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_COUNT_SAGYO_SIJI)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_SAGYO_SIJI)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereSagyoSiji(inTbl)                           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_SAGYO_SIJI)  'SQL構築(データ抽出用Group by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_END_SAGYO_SIJI)  'SQL構築(データ抽出用)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectSagyoSijiData", cmd)

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
    Private Function SelectListSagyoSijiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_DATA_SAGYO_SIJI)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_FROM_SAGYO_SIJI)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereSagyoSiji(inTbl)                           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_GROUP_BY_SAGYO_SIJI)  'SQL構築(データ抽出用Group by句)
        Me._StrSql.Append(LMR010DAC.SQL_SELECT_ORDER_BY_SAGYO_SIJI)  'SQL構築(データ抽出用Order by句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "SelectListSagyoSijiData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("INOUTKA_ORD_NO", "INOUTKA_ORD_NO")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CHK_INKA_STATE_KB", "CHK_INKA_STATE_KB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("SCNT", "SCNT")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMR010INOUT")

        Return ds

    End Function

   
    ''' <summary>
    ''' 検索用SQL WHERE句作成(入荷)
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhereSagyoSiji(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append("WHERE                                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SIJI.SYS_DEL_FLG  = '0'                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND SIJI.SAGYO_SIJI_STATUS = '00'")
        Me._StrSql.Append(vbNewLine)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With _Row
            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SIJI.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '担当者コード
            whereStr = .Item("TANTO_USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUSER.USER_CD LIKE @TANTO_USER_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主コード
            whereStr = .Item("CUST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.CUST_CD_L LIKE @CUST_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '入出荷日（FROM)
            whereStr = .Item("INOUTKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SIJI.SAGYO_SIJI_DATE >= @INOUTKA_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入出荷日（TO)
            whereStr = .Item("INOUTKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SIJI.SAGYO_SIJI_DATE <= @INOUTKA_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '管理番号（大）
            whereStr = .Item("INOUTKA_NO_L").ToString()
            If 1 < dt.Rows.Count Then
                '初期検索時のみ、複数件ある場合がある。
                Dim max As Integer = dt.Rows.Count - 1
                For i As Integer = 0 To max
                    whereStr = dt.Rows(i).Item("INOUTKA_NO_L").ToString()
                    If 0 = i Then
                        Me._StrSql.Append(" AND SIJI.SAGYO_SIJI_NO IN (")
                        Me._StrSql.Append(vbNewLine)
                    Else
                        Me._StrSql.Append(",")
                        Me._StrSql.Append(vbNewLine)
                    End If
                    Me._StrSql.Append(String.Concat("'", whereStr, "'"))
                    Me._StrSql.Append(vbNewLine)
                Next
                Me._StrSql.Append(")")
                Me._StrSql.Append(vbNewLine)
            Else
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND SIJI.SAGYO_SIJI_NO LIKE @INOUTKA_NO_L")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
                End If
            End If

            '担当者
            whereStr = .Item("TANTO_USER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUSER.USER_NM LIKE @TANTO_USER_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "Rapidus次回分納情報取得"

    ''' <summary>
    ''' Rapidus次回分納情報取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectJikaiBunnouInfo(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMR010_JIKAI_BUNNOU_IN"
        Const OUT_TBL_NM As String = "LMR010_JIKAI_BUNNOU_OUT"

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMR010DAC.SQL_SELECT_JIKAI_BUNNOU_INFO, inRow.Item("NRS_BR_CD").ToString())

        ' 取得件数
        Dim cnt As Integer = 0

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            ' SQLパラメータの設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@OUTKA_NO_L", inRow.Item("OUTKA_NO_L").ToString(), DBDataType.VARCHAR))
                .Add(MyBase.GetSqlParameter("@TEMPLATE_PREFIX", inRow.Item("TEMPLATE_PREFIX").ToString(), DBDataType.VARCHAR))
                .Add(MyBase.GetSqlParameter("@TEMPLATE_SUFFIX", inRow.Item("TEMPLATE_SUFFIX").ToString(), DBDataType.VARCHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            ' ログ出力
            MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    ' 取得データの格納先のマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    ' DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count()
                End If

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "Rapidus次回分納情報取得"

#Region "Rapidus次回分納情報 出荷指示EDIデータ 同一ファイル名件数 取得"

    ''' <summary>
    ''' Rapidus次回分納情報 出荷指示EDIデータ 同一ファイル名件数 取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectSameCrtDateAndFileNameCnt(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMR010_JIKAI_BUNNOU_OUT"

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMR010DAC.SQL_SELECT_SAME_CRT_DATE_AND_FILE_NAME_CNT, inRow.Item("NRS_BR_CD").ToString())

        ' 取得件数
        Dim cnt As Integer = 0

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            ' SQLパラメータの設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@CRT_DATE", inRow.Item("CRT_DATE").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@FILE_NAME", inRow.Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
                .Add(MyBase.GetSqlParameter("@JIKAI_BUNNOU_KAISU", inRow.Item("JIKAI_BUNNOU_KAISU").ToString(), DBDataType.NUMERIC))
                .Add(MyBase.GetSqlParameter("@TEMPLATE_PREFIX", inRow.Item("TEMPLATE_PREFIX").ToString(), DBDataType.VARCHAR))
                .Add(MyBase.GetSqlParameter("@TEMPLATE_SUFFIX", inRow.Item("TEMPLATE_SUFFIX").ToString(), DBDataType.VARCHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            ' ログ出力
            MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.Read() Then
                    cnt = Convert.ToInt32(reader("CNT"))
                End If

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "Rapidus次回分納情報 出荷指示EDIデータ 同一ファイル名件数 取得"

#End Region

#Region "変更処理"

#Region "Insert"

    ''' <summary>
    ''' 分析票管理マスタ作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ作成SQLの構築・発行</remarks>
    Private Function InsertSaveCoaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_INSERT_COA, _
                                                                       SQL_SELECT_DATA_COA, _
                                                                       SQL_SELECT_FROM_COA, _
                                                                       SQL_SELECT_FROM_COA3, _
                                                                       SQL_SELECT_WHERE_COA, _
                                                                       SQL_SELECT_WHERE_COA2) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetCoaComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveCoaData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"分析票管理マスタ", String.Concat("[入荷管理番号=", Me._Row.Item("INOUTKA_NO_L").ToString(), "]")})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作業レコード作成（梱包作業）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>梱包作業作成SQLの構築・発行</remarks>
    Private Function InsertSaveSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_INSERT_SAGYO, _
                                                                       SQL_SELECT_DATA_SAGYO, _
                                                                       SQL_SELECT_FROM_SAGYO, _
                                                                       SQL_SELECT_WHERE_SAGYO) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        '採番
        Dim num As New NumberMasterUtility
        Dim sagyoRecNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LMR010INOUT").Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetEsagyoComParameter(Me._Row, Me._SqlPrmList, sagyoRecNo)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveSagyoData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"作業レコード:梱包作業作成", String.Concat("[出荷管理番号=", Me._Row.Item("INOUTKA_NO_L").ToString(), "]")})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(大)作成(振替先)（商品がない方）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)作成SQLの構築・発行</remarks>
    Private Function InsertSaveFuriInkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_INKA_L_FURIKAE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMR010DAC.SQL_INSERT_FURIINKA_L _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetFuriInkaLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveFuriInkaLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"入荷(大)データ", "振替処理"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(中)作成(振替先)（商品がない方）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(中)作成SQLの構築・発行</remarks>
    Private Function InsertSaveFuriInkaMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_INKA_M_FURIKAE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMR010DAC.SQL_INSERT_FURIINKA_M _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetFuriInkaMComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveFuriInkaMData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"入荷(中)データ", "振替処理"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(小)作成(振替先)（商品がない方）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(小)作成SQLの構築・発行</remarks>
    Private Function InsertSaveFuriInkaSData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_INKA_S_FURIKAE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMR010DAC.SQL_INSERT_FURIINKA_S _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetFuriInkaSComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveFuriInkaSData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"入荷(小)データ", "振替処理"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ作成(振替先)（商品がない方）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ作成SQLの構築・発行</remarks>
    Private Function InsertSaveFuriSakiZaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_ZAI_FURIKAE_SAKI")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMR010DAC.SQL_INSERT_FURISAKI_ZAI _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetFuriSakiZaiComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveFuriSakiZaiData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"在庫データ", "振替処理(振替先)"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷(大)作成(振替元)（商品がある方）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)作成SQLの構築・発行</remarks>
    Private Function InsertSaveFuriOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_OUTKA_L_FURIKAE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMR010DAC.SQL_INSERT_FURIOUTKA_L _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetFuriOutkaLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveFuriOutkaLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"出荷(大)データ", "振替処理"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷(中)作成(振替元)（商品がある方）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)作成SQLの構築・発行</remarks>
    Private Function InsertSaveFuriOutkaMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_OUTKA_M_FURIKAE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMR010DAC.SQL_INSERT_FURIOUTKA_M _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetFuriOutkaMComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveFuriOutkaMData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"出荷(中)データ", "振替処理"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷(小)作成(振替元)（商品がある方）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(小)作成SQLの構築・発行</remarks>
    Private Function InsertSaveFuriOutkaSData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_OUTKA_S_FURIKAE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMR010DAC.SQL_INSERT_FURIOUTKA_S _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetFuriOutkaSComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "InsertSaveFuriOutkaMData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        If 0 = rtn Then
            '追加失敗時、メッセージを設定
            MyBase.SetMessage("E305", New String() {"出荷(小)データ", "振替処理"})
        End If

        Return ds

    End Function

#End Region

#Region "INSERT(次回分納情報)"

#Region "H_OUTKAEDI_DTL_RAPI"

    ''' <summary>
    ''' H_OUTKAEDI_DTL_RAPI
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertJikaiBunnouOutkaEdiRapi(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMR010_JIKAI_BUNNOU_OUT"

        Dim nrsBrCd As String = ds.Tables(IN_TBL_NM).Rows(0).Item("NRS_BR_CD").ToString()

        ' 登録件数初期化
        Dim cnt As Integer = 0

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMR010DAC.SQL_INSERT_JIKAI_BUNNOU_OUTKAEDI_DTL_RAPI, nrsBrCd)

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            For i As Integer = 0 To ds.Tables(IN_TBL_NM).Rows.Count() - 1
                Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(i)

                ' SQLパラメータの設定
                Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
                Me._SqlPrmList = New ArrayList()
                With Me._SqlPrmList
                    .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    .Add(MyBase.GetSqlParameter("@CRT_DATE", inRow.Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO_NEW", inRow.Item("EDI_CTL_NO_NEW").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@FILE_NAME", inRow.Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@REC_NO", inRow.Item("REC_NO").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@GYO_NO", inRow.Item("GYO_NO").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@JIKAI_BUNNOU_NB", inRow.Item("JIKAI_BUNNOU_NB").ToString(), DBDataType.NUMERIC))
                    .Add(MyBase.GetSqlParameter("@JIKAI_BUNNOU_KAISU", inRow.Item("JIKAI_BUNNOU_KAISU").ToString(), DBDataType.NUMERIC))
                    .Add(MyBase.GetSqlParameter("@TEMPLATE_PREFIX", inRow.Item("TEMPLATE_PREFIX").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@TEMPLATE_SUFFIX", inRow.Item("TEMPLATE_SUFFIX").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@SAME_CRT_DATE_AND_FILE_NAME_CNT", inRow.Item("SAME_CRT_DATE_AND_FILE_NAME_CNT").ToString(), DBDataType.NUMERIC))
                    .Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "1", DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_TIME", updTime, DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
                End With
                Call Me.SetDataInsertParameter(Me._SqlPrmList)
                cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

                ' ログ出力
                MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                cnt += MyBase.GetInsertResult(cmd)

                cmd.Parameters.Clear()

            Next

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "H_OUTKAEDI_DTL_RAPI"

#Region "H_OUTKAEDI_L"

    ''' <summary>
    ''' H_OUTKAEDI_L
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertJikaiBunnouOutkaEdiL(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMR010_JIKAI_BUNNOU_OUT"

        ' Key: 新規採番した EDI管理番号
        Dim ediCtlNoNewSet As New HashSet(Of String)

        Dim nrsBrCd As String = ds.Tables(IN_TBL_NM).Rows(0).Item("NRS_BR_CD").ToString()

        ' 登録件数初期化
        Dim cnt As Integer = 0

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMR010DAC.SQL_INSERT_JIKAI_BUNNOU_OUTKAEDI_L, nrsBrCd)

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            For i As Integer = 0 To ds.Tables(IN_TBL_NM).Rows.Count() - 1
                Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(i)

                ' 新規採番した EDI管理番号 2回目登場以降のスキップ処理
                If ediCtlNoNewSet.Contains(inRow.Item("EDI_CTL_NO_NEW").ToString()) Then
                    Continue For
                End If
                ediCtlNoNewSet.Add(inRow.Item("EDI_CTL_NO_NEW").ToString())

                ' SQLパラメータの設定
                Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
                Me._SqlPrmList = New ArrayList()
                With Me._SqlPrmList
                    .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO", inRow.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO_NEW", inRow.Item("EDI_CTL_NO_NEW").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@CRT_TIME", updTime, DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.VARCHAR))

                End With
                Call Me.SetDataInsertParameter(Me._SqlPrmList)
                cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

                ' ログ出力
                MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                cnt += MyBase.GetInsertResult(cmd)

                cmd.Parameters.Clear()

            Next

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "H_OUTKAEDI_L"

#Region "H_OUTKAEDI_M"

    ''' <summary>
    ''' H_OUTKAEDI_M
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertJikaiBunnouOutkaEdiM(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMR010_JIKAI_BUNNOU_OUT"

        Dim nrsBrCd As String = ds.Tables(IN_TBL_NM).Rows(0).Item("NRS_BR_CD").ToString()

        ' 登録件数初期化
        Dim cnt As Integer = 0

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMR010DAC.SQL_INSERT_JIKAI_BUNNOU_OUTKAEDI_M, nrsBrCd)

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            For i As Integer = 0 To ds.Tables(IN_TBL_NM).Rows.Count() - 1
                Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(i)

                ' SQLパラメータの設定
                Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
                Me._SqlPrmList = New ArrayList()
                With Me._SqlPrmList
                    .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO", inRow.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", inRow.Item("EDI_CTL_NO_CHU").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO_NEW", inRow.Item("EDI_CTL_NO_NEW").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@JIKAI_BUNNOU_NB", inRow.Item("JIKAI_BUNNOU_NB").ToString(), DBDataType.NUMERIC))
                    .Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", "0", DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@CRT_TIME", updTime, DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.VARCHAR))

                End With
                Call Me.SetDataInsertParameter(Me._SqlPrmList)
                cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

                ' ログ出力
                MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                cnt = MyBase.GetInsertResult(cmd)

                cmd.Parameters.Clear()

            Next

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "H_OUTKAEDI_M"

#End Region ' "INSERT(次回分納情報)"

#Region "Update"

#Region "入荷"

    ''' <summary>
    ''' 入荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSaveInkaDataAction(ByVal ds As DataSet) As DataSet

        'メッセージ設定フラグ初期化
        Me._msgFlg = False

        '入荷（大）の更新
        ds = Me.UpdateSaveInkaData(ds)
        If Me._msgFlg = True Then
            Return ds
        End If

        If ("50").Equals(Convert.ToString(Me._Row.Item("INOUTKA_STATE_KB"))) = False Then
            '入庫完了は後で更新するため、ここでは通らないようにする。

            '在庫データの更新
            ds = Me.UpdateSaveZaiData(ds)
            If Me._msgFlg = True Then
                Return ds
            End If

        End If

        If ("50").Equals(Convert.ToString(Me._Row.Item("INOUTKA_STATE_KB"))) = True Then
            '入庫完了の場合のみ

            '在庫レコードの更新
            ds = Me.UpdateSaveZaiDataINKOKANRYO(ds)
            If Me._msgFlg = True Then
                Return ds
            End If

            '作業レコードの更新
            ds = Me.UpdateSaveSagyoData(ds, "0")
            If Me._msgFlg = True Then
                Return ds
            End If

            '分析票管理マスタの検索 
            Dim dsCnt As Integer = Me.SelectCOAData(ds)
            'START YANAI 要望番号743
            '荷主明細マスタの検索
            Dim dsCnt2 As Integer = Me.SelectCUSTDETAILSData(ds)
            'END YANAI 要望番号743

            'START YANAI 要望番号743
            'If 0 = dsCnt Then
            If 0 < dsCnt AndAlso 0 < dsCnt2 Then
                'END YANAI 要望番号743
                '検索結果が0件の時、分析票管理マスタの作成
                ds = Me.InsertSaveCoaData(ds)
            End If

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSaveInkaData(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号653
        'メッセージ設定フラグ初期化
        Me._msgFlg = False
        'END YANAI 要望番号653

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim sqlstr As String = String.Empty
        Select Case Convert.ToString(Me._Row.Item("INOUTKA_STATE_KB"))
            Case "30"   '入荷受付
                sqlstr = SQL_UPDATE_INKA_L_UKE
            Case "40"   '入荷検品
                sqlstr = SQL_UPDATE_INKA_L_KEN
            Case "50"   '入庫完了
                sqlstr = SQL_UPDATE_INKA_L_KAN
        End Select

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_INKA_L, _
                                                                       sqlstr, _
                                                                       SQL_UPDATE_INKA_L_WHERE, _
                                                                       SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetInkaLComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveInkaData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "出荷"

    ''' <summary>
    ''' 出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSaveOutkaDataAction(ByVal ds As DataSet) As DataSet

        '出荷（大）の更新
        ds = Me.UpdateSaveOutkaData(ds)
        If Me._msgFlg = True Then
            Return ds
        End If

        '以降の処理で、出荷(大)のカウントが0件になってしまう可能性があるため、ここで保持
        Dim rtnCnt As Integer = MyBase.GetResultCount()

        If ("60").Equals(Convert.ToString(Me._Row.Item("INOUTKA_STATE_KB"))) = True Then
            '出荷完了の場合のみ

            '作業レコードの更新
            ds = Me.UpdateSaveSagyoData(ds, "1")
            If Me._msgFlg = True Then
                Return ds
            End If

            '在庫レコードの更新
            ds = Me.UpdateSaveZaiDataKanryo(ds)
            If Me._msgFlg = True Then
                Return ds
            End If

            '小分けかどうかの判定
            Dim dsCnt As Integer = Me.SelectKowakeData(ds)

            If 0 < dsCnt Then
                '小分けの場合のみ、以下の処理を実行

                '作業レコードの検索
                dsCnt = Me.SelectSAGYOData(ds)

                If 0 = dsCnt Then
                    '検索結果が0件の時、梱包作業レコードの作成
                    ds = Me.InsertSaveSagyoData(ds)
                End If
            End If

            If Me._msgFlg = True Then
                Return ds
            End If

            '他荷主在庫の検索
            ds = Me.SelectOTHERData(ds)
            If 0 < MyBase.GetResultCount() Then
                '他荷主在庫が存在する場合、振替処理を行う。

                '振替データ作成用のデータ取得
                ds = Me.SelectListOTHERData(ds)

            End If

        End If

        '出荷（大）の更新件数を再セットする。
        MyBase.SetResultCount(rtnCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSaveOutkaData(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号653
        'メッセージ設定フラグ初期化
        Me._msgFlg = False
        'END YANAI 要望番号653

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim sqlstr As String = String.Empty
        Select Case Convert.ToString(Me._Row.Item("INOUTKA_STATE_KB"))
            Case "40"   '出庫完了
                sqlstr = SQL_UPDATE_OUTKA_L_KAN
            Case "50"   '出荷検品
                sqlstr = SQL_UPDATE_OUTKA_L_KEN
            Case "60"   '出荷完了
                sqlstr = SQL_UPDATE_OUTKA_L_SKAN
        End Select

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_OUTKA_L, _
                                                                       sqlstr, _
                                                                       SQL_UPDATE_OUTKA_L_WHERE, _
                                                                       SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveOutkaData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    'START YANAI 要望番号510
    '''' <summary>
    '''' 出荷(中)テーブル更新(振替先)（商品がない方）
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>出荷(中)テーブル更新SQLの構築・発行</remarks>
    'Private Function UpdateSaveFuriOutkaMData(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMR010_OUTKA_M_FURIKAE_SAKI")
    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_OUTKA_M_FURI, _
    '                                                                   SQL_UPDATE_OUTKA_M_WHERE_FURI) _
    '                                                                   , Me._Row.Item("NRS_BR_CD").ToString()))

    '    'パラメータ設定
    '    Call Me.SetFuriOutkaMUpdComParameter(Me._Row, Me._SqlPrmList)
    '    Call Me.SetSysdataParameter(Me._SqlPrmList)

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveFuriOutkaMData", cmd)

    '    'SQLの発行
    '    Me.UpdateResultChk(cmd)

    '    Return ds

    'End Function
    'END YANAI 要望番号510

    ''' <summary>
    ''' 出荷(小)テーブル更新(振替先)（商品がない方）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(小)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSaveFuriOutkaSData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010_OUTKA_S_FURIKAE_SAKI")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_OUTKA_S_FURI, _
                                                                       SQL_UPDATE_OUTKA_S_WHERE_FURI) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetFuriOutkaSUpdComParameter(Me._Row, Me._SqlPrmList, ds.Tables("LMR010_OUTKA_S_FURIKAE").Rows(0))
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveFuriOutkaSData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function


#Region "作業指示"

    ''' <summary>
    ''' 作業指示更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSaveSagyoSijiDataAction(ByVal ds As DataSet) As DataSet

        'メッセージ設定フラグ初期化
        Me._msgFlg = False

        '作業指示の更新
        ds = Me.UpdateSaveSagyoSijiData(ds)
        If Me._msgFlg = True Then
            Return ds
        End If

        '作業レコードの更新
        ds = Me.UpdateSaveSagyoData(ds, "2")
        If Me._msgFlg = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作業指示更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSaveSagyoSijiData(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号653
        'メッセージ設定フラグ初期化
        Me._msgFlg = False
        'END YANAI 要望番号653

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_SAGYOSIJI, _
                                                                       SQL_UPDATE_SAGYOSIJI_WHERE, _
                                                                       SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSagyoSijiParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveSagyoSijiData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region



#End Region

#Region "在庫"

    ''' <summary>
    ''' 在庫データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ更新SQLの構築・発行</remarks>
    Private Function UpdateSaveZaiData(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号653
        'メッセージ設定フラグ初期化
        Me._msgFlg = False
        'END YANAI 要望番号653

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_ZAI) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetZaiComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveZaiData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ更新（入庫完了時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ更新SQLの構築・発行</remarks>
    Private Function UpdateSaveZaiDataINKOKANRYO(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号653
        'メッセージ設定フラグ初期化
        Me._msgFlg = False
        'END YANAI 要望番号653

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_ZAI_INKOKANRYO) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'START YANAI 要望番号653
        ''パラメータ設定
        'Call Me.SetZaiComParameter(Me._Row, Me._SqlPrmList)
        'Call Me.SetSysdataParameter(Me._SqlPrmList)

        ''パラメータの反映
        'For Each obj As Object In Me._SqlPrmList
        '    cmd.Parameters.Add(obj)
        'Next

        'MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveZaiData", cmd)

        ''SQLの発行
        'Me.UpdateResultChk(cmd)
        Dim zaiDr As DataRow() = ds.Tables("LMR010_ZAI").Select(String.Concat("INOUTKA_NO_L = '", Me._Row.Item("INOUTKA_NO_L").ToString(), "'"))
        Dim zaiDr2 As DataRow() = Nothing
        Dim max As Integer = zaiDr.Length - 1
        Dim max2 As Integer = 0
        '更新日時
        Dim updDate As String = MyBase.GetSystemDate()
        Dim updTime As String = MyBase.GetSystemTime()

        For i As Integer = 0 To max

            'メッセージ設定フラグ初期化
            Me._msgFlg = False

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_ZAI_INKOKANRYO) _
                                                                     , Me._Row.Item("NRS_BR_CD").ToString()))
            'パラメータ設定
            Call Me.SetZaiComParameter(Me._Row, Me._SqlPrmList)
            Call Me.SetSysdataPgidParameter(Me._SqlPrmList)
            '更新日時
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", updDate, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", updTime, DBDataType.CHAR))
            '在庫データ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", zaiDr(i).Item("ZAI_REC_NO").ToString, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_SYS_UPD_DATE", zaiDr(i).Item("ZAI_SYS_UPD_DATE").ToString, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_SYS_UPD_TIME", zaiDr(i).Item("ZAI_SYS_UPD_TIME").ToString, DBDataType.CHAR))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveZaiData", cmd)

            'SQLの発行
            Me.UpdateResultChk(cmd)

            If Me._msgFlg = False Then
                zaiDr2 = ds.Tables("LMR010_ZAI").Select(String.Concat("ZAI_REC_NO = '", zaiDr(i).Item("ZAI_REC_NO").ToString(), "'"))
                For j As Integer = 0 To max2
                    zaiDr2(j).Item("ZAI_SYS_UPD_DATE") = updDate
                    zaiDr2(j).Item("ZAI_SYS_UPD_TIME") = updTime
                Next
            End If

        Next
        'END YANAI 要望番号653
        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ更新（出荷完了時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ更新SQLの構築・発行</remarks>
    Private Function UpdateSaveZaiDataKanryo(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号653
        'メッセージ設定フラグ初期化
        Me._msgFlg = False
        'END YANAI 要望番号653

        'DataSetのIN情報を取得
        'START YANAI 要望番号653
        'Dim inTbl As DataTable = ds.Tables("LMR010_ZAI_UPDDATA")
        'Dim max As Integer = inTbl.Rows.Count - 1
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        Dim max As Integer = 0
        'END YANAI 要望番号653
        Dim cmd As SqlCommand = Nothing

        'START YANAI 要望番号653
        Dim inoutDr As DataRow() = Nothing
        Dim zaiDr As DataRow() = Nothing
        Dim zaiSysDr As DataRow() = Nothing
        Dim zaiUpdDr As DataRow() = Nothing
        Dim max2 As Integer = 0
        '更新日時
        Dim updDate As String = MyBase.GetSystemDate()
        Dim updTime As String = MyBase.GetSystemTime()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        zaiDr = ds.Tables("LMR010_ZAI").Select(String.Concat("INOUTKA_NO_L = '", Me._Row.Item("INOUTKA_NO_L").ToString(), "'"))
        max = zaiDr.Length - 1
        'END YANAI 要望番号653

        For i As Integer = 0 To max

            'メッセージ設定フラグ初期化
            Me._msgFlg = False

            'START YANAI 要望番号653
            'INTableの条件rowの格納
            'Me._Row = inTbl.Rows(i)
            'END YANAI 要望番号653

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            cmd = Nothing

            'SQL文のコンパイル
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_ZAI_KANRYO) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
            'START YANAI 要望番号653
            zaiUpdDr = ds.Tables("LMR010_ZAI_UPDDATA").Select(String.Concat("ZAI_REC_NO = '", zaiDr(i).Item("ZAI_REC_NO").ToString(), "'"))
            'END YANAI 要望番号653

            'パラメータ設定
            'START YANAI 要望番号653
            'Call Me.SetZaiComParameter2(Me._Row, Me._SqlPrmList)
            'Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetZaiComParameter2(zaiUpdDr(0), Me._SqlPrmList)
            Call Me.SetSysdataPgidParameter(Me._SqlPrmList)
            '更新日時
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", updDate, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", updTime, DBDataType.CHAR))
            '在庫データ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_SYS_UPD_DATE", zaiDr(i).Item("ZAI_SYS_UPD_DATE").ToString, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_SYS_UPD_TIME", zaiDr(i).Item("ZAI_SYS_UPD_TIME").ToString, DBDataType.CHAR))
            'END YANAI 要望番号653

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveZaiDataKanryo", cmd)

            'START YANAI 要望番号653
            ''SQLの発行
            'MyBase.GetUpdateResult(cmd)
            'SQLの発行
            Me.UpdateResultChk(cmd)

            If Me._msgFlg = False Then
                zaiSysDr = ds.Tables("LMR010_ZAI").Select(String.Concat("ZAI_REC_NO = '", zaiUpdDr(0).Item("ZAI_REC_NO").ToString(), "'"))
                max2 = zaiSysDr.Length - 1
                For j As Integer = 0 To max2
                    zaiSysDr(j).Item("ZAI_SYS_UPD_DATE") = updDate
                    zaiSysDr(j).Item("ZAI_SYS_UPD_TIME") = updTime
                Next
            End If
            'END YANAI 要望番号653

        Next

        Return ds

    End Function

#End Region

#Region "作業"

    ''' <summary>
    ''' 作業レコード更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inoutFlg">0:入荷、1:出荷</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業レコード更新SQLの構築・発行</remarks>
    Private Function UpdateSaveSagyoData(ByVal ds As DataSet, ByVal inoutFlg As String) As DataSet

        'START YANAI 要望番号653
        'メッセージ設定フラグ初期化
        Me._msgFlg = False
        'END YANAI 要望番号653

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMR010INOUT")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Nothing
        If ("0").Equals(inoutFlg) = True Then
            '入荷の場合
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_SAGYO, _
                                                          LMR010DAC.SQL_UPDATE_SAGYO_IN) _
                                                          , Me._Row.Item("NRS_BR_CD").ToString()))
        ElseIf ("1").Equals(inoutFlg) = True Then
            '出荷の場合
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMR010DAC.SQL_UPDATE_SAGYO, _
                                                          LMR010DAC.SQL_UPDATE_SAGYO_OUT) _
                                                          , Me._Row.Item("NRS_BR_CD").ToString()))
        ElseIf ("2").Equals(inoutFlg) = True Then
            '作業の場合
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMR010DAC.SQL_UPDATE_SAGYO_SAGYOSIJI, _
                                                         Me._Row.Item("NRS_BR_CD").ToString()))

        End If

        'パラメータ設定
        'START YANAI 要望番号968
        'Call Me.SetSagyoComParameter(Me._Row, Me._SqlPrmList)

        If ("2").Equals(inoutFlg) = True Then
            Call Me.SetSagyoUpdParameterSagyoSiji(Me._Row, Me._SqlPrmList)
        Else
            Call Me.SetSagyoUpdParameter(Me._Row, Me._SqlPrmList)
        End If
        'END YANAI 要望番号968
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMR010DAC", "UpdateSaveSagyoData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region


#End Region

#End Region

#Region "パラメータ"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    'START YANAI 要望番号653
    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataPgidParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub
    'END YANAI 要望番号653

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 入荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_STATE_KB", .Item("INOUTKA_STATE_KB").ToString(), DBDataType.CHAR))

            '入荷完了の時、保管料起算日が空だったら、保管料起算日に出荷日を設定する。
            If ("50").Equals(.Item("INOUTKA_STATE_KB").ToString) = True AndAlso _
                String.IsNullOrEmpty(.Item("HOKAN_STR_DATE").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", .Item("INOUTKA_DATE").ToString(), DBDataType.CHAR))
            Else
                prmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", .Item("HOKAN_STR_DATE").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_STATE_KB", .Item("INOUTKA_STATE_KB").ToString(), DBDataType.CHAR))

            '出荷完了の時、保管料終算日が空だったら、保管料終算日に出荷日を設定する。
            If ("60").Equals(.Item("INOUTKA_STATE_KB").ToString) = True AndAlso _
                String.IsNullOrEmpty(.Item("END_DATE").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@END_DATE", .Item("INOUTKA_DATE").ToString(), DBDataType.CHAR))
            Else
                prmList.Add(MyBase.GetSqlParameter("@END_DATE", .Item("END_DATE").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 在庫データの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 在庫データの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiComParameter2(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", .Item("PORA_ZAI_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", .Item("ALCTD_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", .Item("PORA_ZAI_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", .Item("ALCTD_QT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' 作業レコードの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", String.Concat(.Item("INOUTKA_NO_L").ToString(), "%"), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE", .Item("INOUTKA_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'START YANAI 要望番号968
    ''' <summary>
    ''' 作業レコードの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoUpdParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", String.Concat(.Item("INOUTKA_NO_L").ToString(), "%"), DBDataType.CHAR))
            'START YANAI 要望番号1031
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE", .Item("INOUTKA_DATE").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号1031

        End With

    End Sub
    'END YANAI 要望番号968

    ''' <summary>
    ''' 作業レコードの更新パラメータ設定(作業指示用)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoUpdParameterSagyoSiji(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_DATE", .Item("INOUTKA_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub
    ''' <summary>
    ''' 分析票管理マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetCoaComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 小分け出荷の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetKowakeComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 梱包作業の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEsagyoComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal sagyoRecNo As String)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", sagyoRecNo, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 他荷主在庫の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOtherComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' INKA_Lの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriInkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TP", .Item("INKA_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_KB", .Item("INKA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", .Item("INKA_PLAN_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", .Item("INKA_PLAN_QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", .Item("INKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", .Item("BUYER_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", .Item("OUTKA_FROM_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))       '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", .Item("HOKAN_FREE_KIKAN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", .Item("HOKAN_STR_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CHECKLIST_PRT_DATE", .Item("CHECKLIST_PRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CHECKLIST_PRT_USER", .Item("CHECKLIST_PRT_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKELIST_PRT_DATE", .Item("UKETSUKELIST_PRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKELIST_PRT_USER", .Item("UKETSUKELIST_PRT_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKE_DATE", .Item("UKETSUKE_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKE_USER", .Item("UKETSUKE_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_DATE", .Item("KEN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", .Item("KEN_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_USER", .Item("INKO_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOUKOKUSYO_PR_DATE", .Item("HOUKOKUSYO_PR_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOUKOKUSYO_PR_USER", .Item("HOUKOKUSYO_PR_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", .Item("UNCHIN_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' INKA_Mの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriInkaMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_M", .Item("OUTKA_FROM_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_M", .Item("BUYER_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' INKA_Sの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriInkaSComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KONSU", Me.FormatNumValue(.Item("KONSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(.Item("HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' ZAI_TRSの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriSakiZaiComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(.Item("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", .Item("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZERO_FLAG", .Item("ZERO_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", .Item("DEST_CD_P").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriOutkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", .Item("OUTKA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", .Item("SYUBETU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", .Item("OUTKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", .Item("OUTKAHOKOKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", .Item("PICK_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", .Item("ARR_KANRYO_INFO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", .Item("OUTKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@END_DATE", .Item("END_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", .Item("SHIP_CD_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", .Item("NHS_REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Item("SP_NHS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", .Item("DENP_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", .Item("ALL_PRINT_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", .Item("NIHUDA_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", .Item("NHS_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", .Item("DENP_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", .Item("COA_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", .Item("HOKOKU_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", .Item("MATOME_PICK_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", .Item("LAST_PRINT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", .Item("LAST_PRINT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SASZ_USER", .Item("SASZ_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", .Item("OUTKO_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", .Item("KEN_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", .Item("OUTKA_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOU_USER", .Item("HOU_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", .Item("ORDER_TYPE").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriOutkaMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", Me.FormatNumValue(.Item("BACKLOG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", Me.FormatNumValue(.Item("BACKLOG_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", Me.FormatNumValue(.Item("OUTKA_M_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", .Item("ZAIKO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", .Item("SOURCE_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", .Item("YELLOW_CARD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.FormatNumValue(.Item("PRINT_SORT").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' 出荷(小)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriOutkaSComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", .Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Item("OUTKA_TTL_QT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_UPD_FLAG", .Item("ZAI_UPD_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_NB", Me.FormatNumValue(.Item("ALCTD_CAN_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_QT", Me.FormatNumValue(.Item("ALCTD_CAN_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", .Item("COA_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' ZAI_TRSの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriMotoZaiComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' 出荷(小)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriSakiOutkaSComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", .Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_UPD_FLAG", .Item("ZAI_UPD_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_NB", Me.FormatNumValue(.Item("ALCTD_CAN_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_QT", Me.FormatNumValue(.Item("ALCTD_CAN_QT").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' CUSTのパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetCustParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 入荷(小)のチェックデータ取得パラメータ設定
    ''' </summary>
    ''' <param name="conditiontbl">DataTable</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetCheckInkaComParameter(ByVal conditiontbl As DataTable, ByVal prmList As ArrayList)

        With conditiontbl

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", conditiontbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '入荷管理番号(大)はWHERE句にてINを使用しているため、結合処理を行った後に設定
            Dim max As Integer = conditiontbl.Rows.Count - 1
            Dim inkaNoL As String = String.Empty

            For i As Integer = 0 To max
                If String.IsNullOrEmpty(inkaNoL) = False Then
                    inkaNoL = String.Concat(inkaNoL, "','")
                End If
                inkaNoL = String.Concat(inkaNoL, conditiontbl.Rows(i).Item("INOUTKA_NO_L").ToString())

            Next
            '要望番号1631 修正START
            'prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", inkaNoL, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", inkaNoL, DBDataType.CHAR))
            '要望番号1631 修正END

        End With

    End Sub

    ''' <summary>
    ''' 在庫データのチェックデータ取得パラメータ設定
    ''' </summary>
    ''' <param name="conditiontbl">DataTable</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetCheckZaiComParameter(ByVal conditiontbl As DataTable, ByVal prmList As ArrayList)

        With conditiontbl

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", conditiontbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '出荷管理番号(大)はWHERE句にてINを使用しているため、結合処理を行った後に設定
            Dim max As Integer = conditiontbl.Rows.Count - 1
            Dim outkaNoL As String = String.Empty

            For i As Integer = 0 To max
                If String.IsNullOrEmpty(outkaNoL) = False Then
                    outkaNoL = String.Concat(outkaNoL, "','")
                End If
                outkaNoL = String.Concat(outkaNoL, conditiontbl.Rows(i).Item("INOUTKA_NO_L").ToString())

            Next
            '要望番号1631 修正START
            'prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", outkaNoL, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", outkaNoL, DBDataType.CHAR))
            '要望番号1631 修正END

        End With

    End Sub

    ''' <summary>
    ''' チェック用データ取得パラメータ設定
    ''' </summary>
    ''' <param name="conditiontbl">DataTable</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetCheckComParameter(ByVal conditiontbl As DataTable, ByVal prmList As ArrayList)

        With conditiontbl

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", conditiontbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            ''出荷管理番号(大)はWHERE句にてINを使用しているため、結合処理を行った後に設定
            'Dim max As Integer = conditiontbl.Rows.Count - 1
            'Dim outkaNoL As String = String.Empty

            'For i As Integer = 0 To max
            '    If String.IsNullOrEmpty(outkaNoL) = False Then
            '        outkaNoL = String.Concat(outkaNoL, "','")
            '    End If
            '    outkaNoL = String.Concat(outkaNoL, conditiontbl.Rows(i).Item("INOUTKA_NO_L").ToString())

            'Next
            'prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", outkaNoL, DBDataType.NVARCHAR))

        End With

    End Sub

    'START YANAI 要望番号510
    '''' <summary>
    '''' 出荷(中)の更新パラメータ設定(振替)
    '''' </summary>
    '''' <param name="conditionRow">DataRow</param>
    '''' <param name="prmList">パラメータ</param>
    '''' <remarks></remarks>
    'Private Sub SetFuriOutkaMUpdComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

    '    With conditionRow

    '        prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
    '        prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
    '        prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
    '        prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))

    '    End With

    'End Sub
    'END YANAI 要望番号510

    'START YANAI 要望番号510
    '''' <summary>
    '''' 出荷(小)の更新パラメータ設定(振替)
    '''' </summary>
    '''' <param name="conditionRow">DataRow</param>
    '''' <param name="prmList">パラメータ</param>
    '''' <remarks></remarks>
    'Private Sub SetFuriOutkaSUpdComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)
    ''' <summary>
    ''' 出荷(小)の更新パラメータ設定(振替)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetFuriOutkaSUpdComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal conditionRow2 As DataRow)
        'END YANAI 要望番号510

        'START YANAI 要望番号510
        'With conditionRow
        With conditionRow2
            'END YANAI 要望番号510

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", .Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))
            'START YANAI 要望番号510
            'prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@ZAI_UPD_FLAG", .Item("ZAI_UPD_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L_NEW", conditionRow.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号510

        End With

    End Sub

    ''' <summary>
    ''' 作業指示更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoSijiParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region

#Region "設定処理"

#Region "変換"

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region ' "変換"

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

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessageStore("00" _
                                   , "E011" _
                                   , _
                                   , Me._Row.Item("RECORD_NO").ToString() _
                                   , "管理番号" _
                                   , Me._Row.Item("INOUTKA_NO_L").ToString())

            'メッセージフラグ設定
            Me._msgFlg = True

            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

End Class