' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI190  : ハネウェル管理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI190DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI190DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' INデータテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI190IN"

#Region "荷主明細データ取得"

#Region "荷主明細データ取得 SQL"

#Region "荷主明細データ取得 SQL SELECT句"

    ''' <summary>
    ''' 荷主明細データ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CUSTDETAILS As String = " SELECT                                                                     " & vbNewLine _
                                                   & " CUSTDETAILS.NRS_BR_CD                          AS NRS_BR_CD                " & vbNewLine _
                                                   & ",CUSTDETAILS.CUST_CD                            AS CUST_CD                  " & vbNewLine _
                                                   & ",CUSTDETAILS.CUST_CD_EDA                        AS CUST_CD_EDA              " & vbNewLine _
                                                   & ",CUSTDETAILS.CUST_CLASS                         AS CUST_CLASS               " & vbNewLine _
                                                   & ",CUSTDETAILS.SUB_KB                             AS SUB_KB                   " & vbNewLine _
                                                   & ",CUSTDETAILS.SET_NAIYO                          AS SET_NAIYO                " & vbNewLine _
                                                   & ",CUSTDETAILS.SET_NAIYO_2                        AS SET_NAIYO_2              " & vbNewLine _
                                                   & ",CUSTDETAILS.SET_NAIYO_3                        AS SET_NAIYO_3              " & vbNewLine _
                                                   & ",CUSTDETAILS.REMARK                             AS REMARK                   " & vbNewLine

#End Region

#Region "荷主明細データ取得 SQL FROM句"

    ''' <summary>
    ''' 荷主明細データ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CUSTDETAILS As String = "FROM                                                                  " & vbNewLine _
                                                        & "$LM_MST$..M_CUST_DETAILS CUSTDETAILS                                  " & vbNewLine _
                                                        & " --(2013.08.15) 要望番号2095 追加START                                " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..Z_KBN GSKBN                                      " & vbNewLine _
                                                        & " ON                                                                   " & vbNewLine _
                                                        & "     GSKBN.KBN_GROUP_CD = 'H023'                                      " & vbNewLine _
                                                        & " AND CUSTDETAILS.SET_NAIYO = GSKBN.KBN_CD                             " & vbNewLine _
                                                        & " --(2013.08.15) 要望番号2095 追加END                                  " & vbNewLine

#End Region

#Region "荷主明細データ取得 SQL WHERE句"

    ''' <summary>
    ''' 荷主明細データ取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_CUSTDETAILS As String = "WHERE CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                                         & "  AND CUSTDETAILS.SUB_KB = '44'                                      " & vbNewLine _
                                                         & " --(2013.08.15) 要望番号2095 追加START                               " & vbNewLine _
                                                         & "  AND CUSTDETAILS.SET_NAIYO = @COOLANT_GOODS_KB                      " & vbNewLine _
                                                         & "  AND GSKBN.KBN_CD IS NOT NULL                                       " & vbNewLine _
                                                         & " --(2013.08.15) 要望番号2095 追加END                                 " & vbNewLine

#End Region

#End Region

#End Region

#Region "削除対象データの取得"

#Region "削除対象データの取得 SQL"

    ''' <summary>
    ''' 削除対象データの検索 SQL 長いSQLで分けると、意味がわからなくなりそうなので、あえて分けずに記載
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DELDATA As String = " SELECT                                                                      " & vbNewLine _
                                               & "         MAIN.NRS_BR_CD               AS NRS_BR_CD                           " & vbNewLine _
                                               & "       , MAIN.IOZS_KB                 AS IOZS_KB                             " & vbNewLine _
                                               & "       , MAIN.INOUTKA_NO_L            AS INOUTKA_NO_L                        " & vbNewLine _
                                               & " FROM (                                                                      " & vbNewLine _
                                               & "       --①入荷削除データ                                                    " & vbNewLine _
                                               & "       SELECT                                                                " & vbNewLine _
                                               & "               INKAL1.NRS_BR_CD       AS NRS_BR_CD                           " & vbNewLine _
                                               & "             , '10'                   AS IOZS_KB                             " & vbNewLine _
                                               & "             , INKAL1.INKA_NO_L       AS INOUTKA_NO_L                        " & vbNewLine _
                                               & "         FROM                                                                " & vbNewLine _
                                               & "              $LM_TRN$..B_INKA_L INKAL1                                      " & vbNewLine _
                                               & "        WHERE INKAL1.NRS_BR_CD = @NRS_BR_CD                                  " & vbNewLine _
                                               & "          AND INKAL1.SYS_DEL_FLG = '1'                                       " & vbNewLine _
                                               & "          @CUSTDETAILS_INKA1                                                 " & vbNewLine _
                                               & "       --②出荷削除データ                                                    " & vbNewLine _
                                               & "       UNION ALL                                                             " & vbNewLine _
                                               & "       SELECT                                                                " & vbNewLine _
                                               & "               OUTKAL1.NRS_BR_CD      AS NRS_BR_CD                           " & vbNewLine _
                                               & "             , '20'                   AS IOZS_KB                             " & vbNewLine _
                                               & "             , OUTKAL1.OUTKA_NO_L     AS INOUTKA_NO_L                        " & vbNewLine _
                                               & "         FROM                                                                " & vbNewLine _
                                               & "              $LM_TRN$..C_OUTKA_L OUTKAL1                                    " & vbNewLine _
                                               & "        WHERE OUTKAL1.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                               & "          AND OUTKAL1.SYS_DEL_FLG = '1'                                      " & vbNewLine _
                                               & "          @CUSTDETAILS_OUTKA1                                                " & vbNewLine _
                                               & "       --③入荷 特定期間のデータ(現在～10日前に更新された入荷)               " & vbNewLine _
                                               & "       UNION ALL                                                             " & vbNewLine _
                                               & "       SELECT                                                                " & vbNewLine _
                                               & "               INKAL2.NRS_BR_CD       AS NRS_BR_CD                           " & vbNewLine _
                                               & "             , '10'                   AS IOZS_KB                             " & vbNewLine _
                                               & "             , INKAL2.INKA_NO_L       AS INOUTKA_NO_L                        " & vbNewLine _
                                               & "         FROM                                                                " & vbNewLine _
                                               & "              $LM_TRN$..B_INKA_L INKAL2                                      " & vbNewLine _
                                               & "              INNER JOIN $LM_TRN$..B_INKA_M INKAM                            " & vbNewLine _
                                               & "              ON INKAM.NRS_BR_CD = INKAL2.NRS_BR_CD                          " & vbNewLine _
                                               & "              AND INKAM.INKA_NO_L = INKAL2.INKA_NO_L                         " & vbNewLine _
                                               & "              AND INKAM.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                               & "              INNER JOIN $LM_TRN$..B_INKA_S INKAS                            " & vbNewLine _
                                               & "              ON INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                           " & vbNewLine _
                                               & "              AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                          " & vbNewLine _
                                               & "              AND INKAS.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                               & "        WHERE INKAL2.NRS_BR_CD = @NRS_BR_CD                                  " & vbNewLine _
                                               & "          @CUSTDETAILS_INKA2                                                 " & vbNewLine _
                                               & "          AND (    (DATEDIFF(day,getdate(),INKAL2.SYS_ENT_DATE) >= -10)      " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),INKAM.SYS_ENT_DATE) >= -10)       " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),INKAS.SYS_ENT_DATE) >= -10)       " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),INKAL2.SYS_UPD_DATE) >= -10)      " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),INKAM.SYS_UPD_DATE) >= -10)       " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),INKAS.SYS_UPD_DATE) >= -10)       " & vbNewLine _
                                               & "              )                                                              " & vbNewLine _
                                               & "          AND NOT EXISTS                                                     " & vbNewLine _
                                               & "              (SELECT INKAS2.SERIAL_NO FROM $LM_TRN$..B_INKA_S INKAS2        " & vbNewLine _
                                               & "                WHERE INKAS2.NRS_BR_CD = INKAM.NRS_BR_CD                     " & vbNewLine _
                                               & "                  AND INKAS2.INKA_NO_L = INKAM.INKA_NO_L                     " & vbNewLine _
                                               & "                  AND INKAS2.SERIAL_NO <> ''                                 " & vbNewLine _
                                               & "                  AND INKAS2.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                               & "              )                                                              " & vbNewLine _
                                               & "          AND INKAL2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                               & "       --④出荷 特定期間のデータ(現在～10日前に更新された出荷)               " & vbNewLine _
                                               & "       UNION ALL                                                             " & vbNewLine _
                                               & "       SELECT                                                                " & vbNewLine _
                                               & "               OUTKAL2.NRS_BR_CD      AS NRS_BR_CD                           " & vbNewLine _
                                               & "             , '20'                   AS IOZS_KB                             " & vbNewLine _
                                               & "             , OUTKAL2.OUTKA_NO_L     AS INOUTKA_NO_L                        " & vbNewLine _
                                               & "         FROM                                                                " & vbNewLine _
                                               & "              $LM_TRN$..C_OUTKA_L OUTKAL2                                    " & vbNewLine _
                                               & "              INNER JOIN $LM_TRN$..C_OUTKA_M OUTKAM                          " & vbNewLine _
                                               & "              ON OUTKAM.NRS_BR_CD = OUTKAL2.NRS_BR_CD                        " & vbNewLine _
                                               & "              AND OUTKAM.OUTKA_NO_L = OUTKAL2.OUTKA_NO_L                     " & vbNewLine _
                                               & "              AND OUTKAM.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                               & "              INNER JOIN $LM_TRN$..C_OUTKA_S OUTKAS                          " & vbNewLine _
                                               & "              ON OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                         " & vbNewLine _
                                               & "              AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                      " & vbNewLine _
                                               & "              AND OUTKAS.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                               & "        WHERE OUTKAL2.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                               & "          @CUSTDETAILS_OUTKA2                                                " & vbNewLine _
                                               & "          AND (    (DATEDIFF(day,getdate(),OUTKAL2.SYS_ENT_DATE) >= -10)     " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),OUTKAM.SYS_ENT_DATE) >= -10)      " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),OUTKAS.SYS_ENT_DATE) >= -10)      " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),OUTKAL2.SYS_UPD_DATE) >= -10)     " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),OUTKAM.SYS_UPD_DATE) >= -10)      " & vbNewLine _
                                               & "                OR (DATEDIFF(day,getdate(),OUTKAS.SYS_UPD_DATE) >= -10)      " & vbNewLine _
                                               & "              )                                                              " & vbNewLine _
                                               & "          AND NOT EXISTS                                                     " & vbNewLine _
                                               & "              (SELECT OUTKAS2.SERIAL_NO FROM $LM_TRN$..C_OUTKA_S OUTKAS2     " & vbNewLine _
                                               & "                WHERE OUTKAS2.NRS_BR_CD = OUTKAM.NRS_BR_CD                   " & vbNewLine _
                                               & "                  AND OUTKAS2.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                 " & vbNewLine _
                                               & "                  AND OUTKAS2.SERIAL_NO <> ''                                " & vbNewLine _
                                               & "                  AND OUTKAS2.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                               & "              )                                                              " & vbNewLine _
                                               & "          AND OUTKAL2.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                                               & "      ) MAIN                                                                 " & vbNewLine

#End Region

#End Region

#Region "入荷データ取得"

#Region "入荷データ取得 SQL"

#Region "入荷データ取得 SQL SELECT句"

    ''' <summary>
    ''' 入荷データ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKA As String = " SELECT                                                                     " & vbNewLine _
                                            & " INKAL.NRS_BR_CD                                AS NRS_BR_CD                " & vbNewLine _
                                            & ",CASE                                                                       " & vbNewLine _
                                            & "  WHEN isNull(INKA_KP_WK.SERIAL_NO,'') = ''                                 " & vbNewLine _
                                            & "   THEN INKAS.SERIAL_NO                                                     " & vbNewLine _
                                            & "  ELSE  INKA_KP_WK.SERIAL_NO                                                " & vbNewLine _
                                            & " END                                            AS SERIAL_NO                " & vbNewLine _
                                            & ",INKAL.INKA_DATE                                AS INOUT_DATE               " & vbNewLine _
                                            & ",'10'                                           AS IOZS_KB                  " & vbNewLine _
                                            & ",INKAM.INKA_NO_L                                AS INOUTKA_NO_L             " & vbNewLine _
                                            & ",INKAM.INKA_NO_M                                AS INOUTKA_NO_M             " & vbNewLine _
                                            & ",RIGHT                                                                      " & vbNewLine _
                                            & " ('000'                                                                     " & vbNewLine _
                                            & " +LTRIM(STR(ROW_NUMBER()                                                    " & vbNewLine _
                                            & "  OVER                                                                      " & vbNewLine _
                                            & "   (PARTITION BY                                                            " & vbNewLine _
                                            & "     INKAM.NRS_BR_CD                                                        " & vbNewLine _
                                            & "    ,INKAL.INKA_DATE                                                        " & vbNewLine _
                                            & "    ,INKAM.INKA_NO_L                                                        " & vbNewLine _
                                            & "    ,INKAM.INKA_NO_M                                                        " & vbNewLine _
                                            & "    ,CASE                                                                   " & vbNewLine _
                                            & "      WHEN isNull(INKA_KP_WK.SERIAL_NO,'') = ''                             " & vbNewLine _
                                            & "        THEN INKAS.SERIAL_NO                                                " & vbNewLine _
                                            & "      ELSE  INKA_KP_WK.SERIAL_NO                                            " & vbNewLine _
                                            & "     END                                                                    " & vbNewLine _
                                            & "    ORDER BY                                                                " & vbNewLine _
                                            & "     INKAM.NRS_BR_CD    ASC                                                 " & vbNewLine _
                                            & "    ,INKAL.INKA_DATE    ASC                                                 " & vbNewLine _
                                            & "    ,INKAM.INKA_NO_L    ASC                                                 " & vbNewLine _
                                            & "    ,INKAM.INKA_NO_M    ASC                                                 " & vbNewLine _
                                            & "    ,CASE                                                                   " & vbNewLine _
                                            & "      WHEN isNull(INKA_KP_WK.SERIAL_NO,'') = ''                             " & vbNewLine _
                                            & "        THEN INKAS.SERIAL_NO                                                " & vbNewLine _
                                            & "      ELSE  INKA_KP_WK.SERIAL_NO                                            " & vbNewLine _
                                            & "     END                ASC                                                 " & vbNewLine _
                                            & " ))),3)                                         AS INOUTKA_NO_S             " & vbNewLine _
                                            & ",CASE WHEN '50' = INKAL.INKA_STATE_KB                                       " & vbNewLine _
                                            & "      THEN '1'                                                              " & vbNewLine _
                                            & "      WHEN '90' = INKAL.INKA_STATE_KB                                       " & vbNewLine _
                                            & "      THEN '1'                                                              " & vbNewLine _
                                            & "      ELSE '0'                                                              " & vbNewLine _
                                            & " END                                            AS STATUS                   " & vbNewLine _
                                            & ",INKAL.WH_CD                                    AS WH_CD                    " & vbNewLine _
                                            & ",INKAL.CUST_CD_L                                AS CUST_CD_L                " & vbNewLine _
                                            & ",INKAL.CUST_CD_M                                AS CUST_CD_M                " & vbNewLine _
                                            & ",INKAL.REMARK                                   AS REMARK                   " & vbNewLine _
                                            & ",ISNULL(UNSOL.ORIG_CD,CUSTDTL.SET_NAIYO)        AS TOFROM_CD                " & vbNewLine _
                                            & ",SOKO.WH_NM                                     AS TOFROM_NM                " & vbNewLine _
                                            & ",'0'                                            AS EXP_FLG                  " & vbNewLine _
                                            & ",INKAM.GOODS_CD_NRS                             AS GOODS_CD_NRS             " & vbNewLine _
                                            & ",GOODS.GOODS_CD_CUST                            AS GOODS_CD_CUST            " & vbNewLine _
                                            & ",GOODS.GOODS_NM_1                               AS GOODS_NM                 " & vbNewLine _
                                            & ",INKAM.OUTKA_FROM_ORD_NO_M                      AS CUST_ORD_NO_DTL          " & vbNewLine _
                                            & ",INKAM.BUYER_ORD_NO_M                           AS BUYER_ORD_NO_DTL         " & vbNewLine _
                                            & ",GOODS.CUST_COST_CD1                            AS CYLINDER_TYPE            " & vbNewLine _
                                            & ",GOODS.CUST_COST_CD2                            AS EMPTY_KB                 " & vbNewLine _
                                            & ",'0'                                            AS HAIKI_YN                 " & vbNewLine _
                                            & ",''                                             AS SHIP_CD_L                " & vbNewLine _
                                            & ",''                                             AS SHIP_NM_L                " & vbNewLine _
                                            & ",'0'                                            AS FREE_N01                 " & vbNewLine _
                                            & ",'0'                                            AS FREE_N02                 " & vbNewLine _
                                            & ",'0'                                            AS FREE_N03                 " & vbNewLine _
                                            & ",'0'                                            AS FREE_N04                 " & vbNewLine _
                                            & ",'0'                                            AS FREE_N05                 " & vbNewLine _
                                            & ",''                                             AS FREE_C01                 " & vbNewLine _
                                            & ",''                                             AS FREE_C02                 " & vbNewLine _
                                            & ",''                                             AS FREE_C03                 " & vbNewLine _
                                            & ",''                                             AS FREE_C04                 " & vbNewLine _
                                            & ",''                                             AS FREE_C05                 " & vbNewLine

#End Region

#Region "入荷データ取得 SQL FROM句"

    ''' <summary>
    ''' 入荷データ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_INKA As String = "FROM                                                                  " & vbNewLine _
                                                 & "    (SELECT INKAL2.NRS_BR_CD                                          " & vbNewLine _
                                                 & "           ,INKAL2.INKA_NO_L                                          " & vbNewLine _
                                                 & "       FROM $LM_TRN$..B_INKA_L INKAL2                                 " & vbNewLine _
                                                 & "       INNER JOIN $LM_TRN$..B_INKA_M INKAM2                           " & vbNewLine _
                                                 & "       ON                                                             " & vbNewLine _
                                                 & "       INKAM2.NRS_BR_CD = INKAL2.NRS_BR_CD                            " & vbNewLine _
                                                 & "       AND INKAM2.INKA_NO_L = INKAL2.INKA_NO_L                        " & vbNewLine _
                                                 & "       AND INKAM2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                 & "      WHERE                                                           " & vbNewLine _
                                                 & "       INKAL2.NRS_BR_CD = @NRS_BR_CD                                  " & vbNewLine _
                                                 & "       @CUSTDETAILS_INKA                                              " & vbNewLine _
                                                 & "       AND (    (DATEDIFF(day,getdate(),INKAL2.SYS_ENT_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAM2.SYS_ENT_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAL2.SYS_UPD_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAM2.SYS_UPD_DATE) >= -10)  " & vbNewLine _
                                                 & "           )                                                          " & vbNewLine _
                                                 & "      GROUP BY                                                        " & vbNewLine _
                                                 & "       INKAL2.NRS_BR_CD                                               " & vbNewLine _
                                                 & "      ,INKAL2.INKA_NO_L                                               " & vbNewLine _
                                                 & "    ) AS GET                                                          " & vbNewLine _
                                                 & "    INNER JOIN $LM_TRN$..B_INKA_L INKAL                               " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    INKAL.NRS_BR_CD = GET.NRS_BR_CD                                   " & vbNewLine _
                                                 & "    AND INKAL.INKA_NO_L = GET.INKA_NO_L                               " & vbNewLine _
                                                 & "    AND INKAL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    INNER JOIN $LM_TRN$..B_INKA_M INKAM                               " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                             " & vbNewLine _
                                                 & "    AND INKAM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    LEFT JOIN                                                         " & vbNewLine _
                                                 & "     (                                                                " & vbNewLine _
                                                 & "      SELECT                                                          " & vbNewLine _
                                                 & "           INKAS.NRS_BR_CD            AS NRS_BR_CD                    " & vbNewLine _
                                                 & "          ,INKAS.INKA_NO_L            AS INKA_NO_L                    " & vbNewLine _
                                                 & "          ,INKAS.INKA_NO_M            AS INKA_NO_M                    " & vbNewLine _
                                                 & "          ,IsNull(INKAS.SERIAL_NO,'') AS SERIAL_NO                    " & vbNewLine _
                                                 & "          ,INKAS.SYS_DEL_FLG          AS SYS_DEL_FLG                  " & vbNewLine _
                                                 & "      FROM $LM_TRN$..B_INKA_S AS INKAS                                " & vbNewLine _
                                                 & "      WHERE                                                           " & vbNewLine _
                                                 & "           INKAS.SYS_DEL_FLG    = '0'                                 " & vbNewLine _
                                                 & "       --AND INKAS.SERIAL_NO     <> ''                                " & vbNewLine _
                                                 & "      GROUP BY                                                        " & vbNewLine _
                                                 & "           INKAS.NRS_BR_CD                                            " & vbNewLine _
                                                 & "          ,INKAS.INKA_NO_L                                            " & vbNewLine _
                                                 & "          ,INKAS.INKA_NO_M                                            " & vbNewLine _
                                                 & "          ,INKAS.SERIAL_NO                                            " & vbNewLine _
                                                 & "          ,INKAS.SYS_DEL_FLG                                          " & vbNewLine _
                                                 & "     ) AS INKAS                                                       " & vbNewLine _
                                                 & "    ON  INKAM.NRS_BR_CD = INKAS.NRS_BR_CD                             " & vbNewLine _
                                                 & "    AND INKAM.INKA_NO_L = INKAS.INKA_NO_L                             " & vbNewLine _
                                                 & "    AND INKAM.INKA_NO_M = INKAS.INKA_NO_M                             " & vbNewLine _
                                                 & "    LEFT JOIN　                                                       " & vbNewLine _
                                                 & "　  (SELECT INKA_KP_WK.NRS_BR_CD      AS NRS_BR_CD　                  " & vbNewLine _
                                                 & "　               ,INKA_KP_WK.INKA_NO_L      AS INKA_NO_L              " & vbNewLine _
                                                 & "　   ,INKA_KP_WK.INKA_NO_M      AS INKA_NO_M                          " & vbNewLine _
                                                 & "　   ,INKA_KP_WK.SERIAL_NO      AS SERIAL_NO                          " & vbNewLine _
                                                 & "　   ,IsNull(INS.SERIAL_NO,'')  AS S_JOIN_SERIAL_NO                   " & vbNewLine _
                                                 & "　   ,INKA_KP_WK.SYS_DEL_FLG    AS SYS_DEL_FLG                        " & vbNewLine _
                                                 & "　   FROM $LM_TRN$..B_INKA_KENPIN_WK AS INKA_KP_WK   -- 013118 LM_TRN_10固定修正 " & vbNewLine _
                                                 & "　           LEFT JOIN $LM_TRN$..B_INKA_S    AS INS  -- 013118 LM_TRN_10固定修正 " & vbNewLine _
                                                 & "　         ON INS.SYS_DEL_FLG       = '0'                             " & vbNewLine _
                                                 & "　                AND INKA_KP_WK.NRS_BR_CD  = INS.NRS_BR_CD           " & vbNewLine _
                                                 & "　                AND INKA_KP_WK.INKA_NO_L  = INS.INKA_NO_L           " & vbNewLine _
                                                 & "　                AND INKA_KP_WK.INKA_NO_M  = INS.INKA_NO_M           " & vbNewLine _
                                                 & "　                AND INKA_KP_WK.SERIAL_NO  = INS.SERIAL_NO           " & vbNewLine _
                                                 & "　   WHERE IsNUll(INS.SERIAL_NO,'') = ''                              " & vbNewLine _
                                                 & "　  ) AS INKA_KP_WK                                                   " & vbNewLine _
                                                 & "　   ON INKA_KP_WK.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                 & "　  AND INKAM.NRS_BR_CD = INKA_KP_WK.NRS_BR_CD                        " & vbNewLine _
                                                 & "　  AND INKAM.INKA_NO_L = INKA_KP_WK.INKA_NO_L                        " & vbNewLine _
                                                 & "　  AND INKAM.INKA_NO_M = INKA_KP_WK.INKA_NO_M                        " & vbNewLine _
                                                 & "　  AND INKAS.SERIAL_NO = INKA_KP_WK.S_JOIN_SERIAL_NO                 " & vbNewLine _
                                                 & "    LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    UNSOL.NRS_BR_CD = INKAL.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND UNSOL.INOUTKA_NO_L = INKAL.INKA_NO_L                          " & vbNewLine _
                                                 & "    AND UNSOL.MOTO_DATA_KB = '10'                                     " & vbNewLine _
                                                 & "    AND UNSOL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    INNER JOIN $LM_MST$..M_GOODS GOODS                                " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                       " & vbNewLine _
                                                 & "    LEFT JOIN LM_MST..M_SOKO AS SOKO                                  " & vbNewLine _
                                                 & "    ON SOKO.WH_CD = INKAL.WH_CD                                       " & vbNewLine _
                                                 & "    LEFT JOIN LM_MST..M_DEST AS DEST2                                 " & vbNewLine _
                                                 & "    ON DEST2.NRS_BR_CD = SOKO.NRS_BR_CD                               " & vbNewLine _
                                                 & "    AND DEST2.DEST_CD = SOKO.SOKO_DEST_CD                             " & vbNewLine _
                                                 & "    AND DEST2.CUST_CD_L = 'ZZZZZ'                                     " & vbNewLine _
                                                 & "    LEFT JOIN $LM_MST$..M_CUST_DETAILS AS CUSTDTL                     " & vbNewLine _
                                                 & "    ON INKAL.NRS_BR_CD = CUSTDTL.NRS_BR_CD                            " & vbNewLine _
                                                 & "    AND INKAL.CUST_CD_L = CUSTDTL.CUST_CD                             " & vbNewLine _
                                                 & "    AND CUSTDTL.SUB_KB = '70'                                         " & vbNewLine _
                                                 & "    AND CUSTDTL.SYS_DEL_FLG = '0'                                     " & vbNewLine

#End Region

#Region "入荷データ取得 SQL WHERE句"

    ''' <summary>
    ''' 入荷データ取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_INKA As String = _
                 "WHERE                                                                        " & vbNewLine _
               & "(isNull(INKAS.SERIAL_NO,'') <> '' OR isNull(INKA_KP_WK.SERIAL_NO,'') <> '')  " & vbNewLine _
               & "AND INKAL.INKA_TP = '10'  " & vbNewLine

#End Region

#End Region

#End Region

#Region "出荷データ取得"

#Region "出荷データ取得 SQL"

#Region "出荷データ取得 SQL SELECT句"

    ''' <summary>
    ''' 出荷データ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKA As String = " SELECT                                                                                                               " & vbNewLine _
                                             & " OUTKAL.NRS_BR_CD                               AS NRS_BR_CD                                                          " & vbNewLine _
                                             & ",CASE                                                                                                                 " & vbNewLine _
                                             & "  WHEN ISnULL(CPW.SERIAL_NO,'') = ''                                                                                  " & vbNewLine _
                                             & "   THEN OUTKAS.SERIAL_NO                                                                                              " & vbNewLine _
                                             & "  ELSE  CPW.SERIAL_NO                                                                                                 " & vbNewLine _
                                             & " END                                            AS SERIAL_NO                                                          " & vbNewLine _
                                             & ",OUTKAL.OUTKA_PLAN_DATE                         AS INOUT_DATE                                                         " & vbNewLine _
                                             & ",'20'                                           AS IOZS_KB                                                            " & vbNewLine _
                                             & ",OUTKAM.OUTKA_NO_L                              AS INOUTKA_NO_L                                                       " & vbNewLine _
                                             & ",OUTKAM.OUTKA_NO_M                              AS INOUTKA_NO_M                                                       " & vbNewLine _
                                             & ",RIGHT                                                                                                                " & vbNewLine _
                                             & " ('000'                                                                                                               " & vbNewLine _
                                             & " +LTRIM(STR(ROW_NUMBER()                                                                                              " & vbNewLine _
                                             & "  OVER                                                                                                                " & vbNewLine _
                                             & "   (PARTITION BY                                                                                                      " & vbNewLine _
                                             & "     OUTKAM.NRS_BR_CD                                                                                                 " & vbNewLine _
                                             & "    ,OUTKAL.OUTKA_PLAN_DATE                                                                                           " & vbNewLine _
                                             & "    ,OUTKAM.OUTKA_NO_L                                                                                                " & vbNewLine _
                                             & "    ,OUTKAM.OUTKA_NO_M                                                                                                " & vbNewLine _
                                             & "    ,CASE                                                                                                             " & vbNewLine _
                                             & "      WHEN ISnULL(CPW.SERIAL_NO,'') = ''                                                                              " & vbNewLine _
                                             & "       THEN OUTKAS.SERIAL_NO                                                                                          " & vbNewLine _
                                             & "      ELSE  CPW.SERIAL_NO                                                                                             " & vbNewLine _
                                             & "     END                                                                                                              " & vbNewLine _
                                             & "    ORDER BY                                                                                                          " & vbNewLine _
                                             & "     OUTKAM.NRS_BR_CD        ASC                                                                                      " & vbNewLine _
                                             & "    ,OUTKAL.OUTKA_PLAN_DATE ASC                                                                                       " & vbNewLine _
                                             & "    ,OUTKAM.OUTKA_NO_L      ASC                                                                                       " & vbNewLine _
                                             & "    ,OUTKAM.OUTKA_NO_M      ASC                                                                                       " & vbNewLine _
                                             & "    ,CASE                                                                                                             " & vbNewLine _
                                             & "      WHEN ISnULL(CPW.SERIAL_NO,'') = ''                                                                              " & vbNewLine _
                                             & "       THEN OUTKAS.SERIAL_NO                                                                                          " & vbNewLine _
                                             & "      ELSE  CPW.SERIAL_NO                                                                                             " & vbNewLine _
                                             & "     END                ASC                                                                                           " & vbNewLine _
                                             & " ))),3)                                         AS INOUTKA_NO_S                                                       " & vbNewLine _
                                             & ",CASE WHEN '60' = OUTKAL.OUTKA_STATE_KB                                                                               " & vbNewLine _
                                             & "      THEN '1'                                                                                                        " & vbNewLine _
                                             & "      WHEN '90' = OUTKAL.OUTKA_STATE_KB                                                                               " & vbNewLine _
                                             & "      THEN '1'                                                                                                        " & vbNewLine _
                                             & "      ELSE '0'                                                                                                        " & vbNewLine _
                                             & " END                                            AS STATUS                                                             " & vbNewLine _
                                             & ",OUTKAL.WH_CD                                   AS WH_CD                                                              " & vbNewLine _
                                             & ",OUTKAL.CUST_CD_L                               AS CUST_CD_L                                                          " & vbNewLine _
                                             & ",OUTKAL.CUST_CD_M                               AS CUST_CD_M                                                          " & vbNewLine _
                                             & ",''                                             AS REMARK                                                             " & vbNewLine _
                                             & ",OUTKAL.DEST_CD                                 AS TOFROM_CD                                                          " & vbNewLine _
                                             & ",CASE WHEN OUTKAL.DEST_KB = '00'                                                                                      " & vbNewLine _
                                             & "      THEN DEST1.DEST_NM                                                                                              " & vbNewLine _
                                             & "      ELSE OUTKAL.DEST_NM                                                                                             " & vbNewLine _
                                             & " END                                            AS TOFROM_NM                                                          " & vbNewLine _
                                             & ",'0'                                            AS EXP_FLG                                                            " & vbNewLine _
                                             & ",OUTKAM.GOODS_CD_NRS                            AS GOODS_CD_NRS                                                       " & vbNewLine _
                                             & ",GOODS.GOODS_CD_CUST                            AS GOODS_CD_CUST                                                      " & vbNewLine _
                                             & ",GOODS.GOODS_NM_1                               AS GOODS_NM                                                           " & vbNewLine _
                                             & ",CASE WHEN OUTKAM.CUST_ORD_NO_DTL = '' THEN OUTKAL.CUST_ORD_NO ELSE OUTKAM.CUST_ORD_NO_DTL END AS CUST_ORD_NO_DTL     " & vbNewLine _
                                             & ",CASE WHEN OUTKAM.BUYER_ORD_NO_DTL = '' THEN OUTKAL.BUYER_ORD_NO ELSE OUTKAM.BUYER_ORD_NO_DTL END AS BUYER_ORD_NO_DTL " & vbNewLine _
                                             & ",GOODS.CUST_COST_CD1                            AS CYLINDER_TYPE                                                      " & vbNewLine _
                                             & ",GOODS.CUST_COST_CD2                            AS EMPTY_KB                                                           " & vbNewLine _
                                             & ",'0'                                            AS HAIKI_YN                                                           " & vbNewLine _
                                             & ",CASE WHEN Z1.KBN_GROUP_CD = 'H018'                                                                                   " & vbNewLine _
                                             & "        THEN   ISNULL(SHIPTOCD1.SOLD_TO_CD, OUTKAL.DEST_CD)                                                           " & vbNewLine _
                                             & "        ELSE    ISNULL(SHIPTOCD2.SOLD_TO_CD, OUTKAL.SHIP_CD_L)                                                        " & vbNewLine _
                                             & "END                                            AS SHIP_CD_L                                                           " & vbNewLine _
                                             & ",CASE WHEN Z1.KBN_GROUP_CD = 'H018'                                                                                   " & vbNewLine _
                                             & "        THEN ISNULL(SHIPTOCD1.SOLD_TO_NM, DEST1.DEST_NM)                                                              " & vbNewLine _
                                             & "        ELSE  ISNULL(SHIPTOCD2.SOLD_TO_NM, DEST2.DEST_NM)                                                             " & vbNewLine _
                                             & "END                                             AS SHIP_NM_L                                                          " & vbNewLine _
                                             & ",'0'                                            AS FREE_N01                                                           " & vbNewLine _
                                             & ",'0'                                            AS FREE_N02                                                           " & vbNewLine _
                                             & ",'0'                                            AS FREE_N03                                                           " & vbNewLine _
                                             & ",'0'                                            AS FREE_N04                                                           " & vbNewLine _
                                             & ",'0'                                            AS FREE_N05                                                           " & vbNewLine _
                                             & ",''                                             AS FREE_C01                                                           " & vbNewLine _
                                             & ",''                                             AS FREE_C02                                                           " & vbNewLine _
                                             & ",''                                             AS FREE_C03                                                           " & vbNewLine _
                                             & ",''                                             AS FREE_C04                                                           " & vbNewLine _
                                             & ",''                                             AS FREE_C05                                                           " & vbNewLine

#End Region

#Region "出荷データ取得 SQL FROM句"

    ''' <summary>
    ''' 出荷データ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKA As String = "FROM                                                   " & vbNewLine _
                                                  & "$LM_TRN$..C_OUTKA_L OUTKAL                             " & vbNewLine _
                                                  & " INNER JOIN $LM_TRN$..C_OUTKA_M OUTKAM                 " & vbNewLine _
                                                  & " ON                                                    " & vbNewLine _
                                                  & " OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                   " & vbNewLine _
                                                  & " AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L             " & vbNewLine _
                                                  & " AND OUTKAM.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                                  & " LEFT JOIN                                             " & vbNewLine _
                                                  & "  (SELECT                                              " & vbNewLine _
                                                  & "       NRS_BR_CD                   AS NRS_BR_CD        " & vbNewLine _
                                                  & "      ,OUTKA_NO_L                  AS OUTKA_NO_L       " & vbNewLine _
                                                  & "      ,OUTKA_NO_M                  AS OUTKA_NO_M       " & vbNewLine _
                                                  & "      ,IsNull(SERIAL_NO,'')        AS SERIAL_NO        " & vbNewLine _
                                                  & "      ,SYS_DEL_FLG                 AS SYS_DEL_FLG      " & vbNewLine _
                                                  & "   FROM                                                " & vbNewLine _
                                                  & "       $LM_TRN$..C_OUTKA_S OUTKAS                      " & vbNewLine _
                                                  & "   WHERE                                               " & vbNewLine _
                                                  & "       SYS_DEL_FLG = '0'                               " & vbNewLine _
                                                  & "   --AND SERIAL_NO <>''                                " & vbNewLine _
                                                  & "   GROUP BY                                            " & vbNewLine _
                                                  & "       NRS_BR_CD                                       " & vbNewLine _
                                                  & "      ,OUTKA_NO_L                                      " & vbNewLine _
                                                  & "      ,OUTKA_NO_M                                      " & vbNewLine _
                                                  & "      ,SERIAL_NO                                       " & vbNewLine _
                                                  & "      ,SYS_DEL_FLG                                     " & vbNewLine _
                                                  & "    )               AS OUTKAS                          " & vbNewLine _
                                                  & "  ON                                                   " & vbNewLine _
                                                  & "      OUTKAS.NRS_BR_CD  = OUTKAM.NRS_BR_CD             " & vbNewLine _
                                                  & "  AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L            " & vbNewLine _
                                                  & "  AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M            " & vbNewLine _
                                                  & " LEFT JOIN                                             " & vbNewLine _
                                                  & " (SELECT CPW.NRS_BR_CD             AS NRS_BR_CD        " & vbNewLine _
                                                  & "        ,CPW.OUTKA_NO_L            AS OUTKA_NO_L       " & vbNewLine _
                                                  & "        ,CPW.OUTKA_NO_M            AS OUTKA_NO_M       " & vbNewLine _
                                                  & "        ,CPW.SERIAL_NO             AS SERIAL_NO        " & vbNewLine _
                                                  & "        ,IsNull(OUTS.SERIAL_NO,'') AS S_JOIN_SERIAL_NO " & vbNewLine _
                                                  & "        ,CPW.SYS_DEL_FLG           AS SYS_DEL_FLG      " & vbNewLine _
                                                  & "  FROM                                                 " & vbNewLine _
                                                  & "  $LM_TRN$..C_OUTKA_PICK_WK       AS CPW     -- 013118 LM_TRN_10固定修正 " & vbNewLine _
                                                  & "  LEFT JOIN $LM_TRN$..C_OUTKA_S   AS OUTS    -- 013118 LM_TRN_10固定修正 " & vbNewLine _
                                                  & "        ON CPW.NRS_BR_CD            = OUTS.NRS_BR_CD   " & vbNewLine _
                                                  & "       AND CPW.OUTKA_NO_L           = OUTS.OUTKA_NO_L  " & vbNewLine _
                                                  & "       AND CPW.OUTKA_NO_M           = OUTS.OUTKA_NO_M  " & vbNewLine _
                                                  & "       AND CPW.SERIAL_NO            = OUTS.SERIAL_NO   " & vbNewLine _
                                                  & "   AND OUTS.SYS_DEL_FLG             = '0'              " & vbNewLine _
                                                  & " WHERE IsNull(OUTS.SERIAL_NO,'')    = ''               " & vbNewLine _
                                                  & " ) AS CPW                                              " & vbNewLine _
                                                  & " ON                                                    " & vbNewLine _
                                                  & "     CPW.SYS_DEL_FLG    = '0'                          " & vbNewLine _
                                                  & " AND OUTKAM.NRS_BR_CD   = CPW.NRS_BR_CD                " & vbNewLine _
                                                  & " AND OUTKAM.OUTKA_NO_L  = CPW.OUTKA_NO_L               " & vbNewLine _
                                                  & " AND OUTKAM.OUTKA_NO_M  = CPW.OUTKA_NO_M               " & vbNewLine _
                                                  & " AND OUTKAS.SERIAL_NO   = CPW.S_JOIN_SERIAL_NO         " & vbNewLine _
                                                  & " INNER JOIN $LM_MST$..M_GOODS GOODS                    " & vbNewLine _
                                                  & " ON                                                    " & vbNewLine _
                                                  & " GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                    " & vbNewLine _
                                                  & " AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS          " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_DEST DEST1                      " & vbNewLine _
                                                  & " ON                                                    " & vbNewLine _
                                                  & " DEST1.NRS_BR_CD = OUTKAL.NRS_BR_CD                    " & vbNewLine _
                                                  & " AND DEST1.CUST_CD_L = OUTKAL.CUST_CD_L                " & vbNewLine _
                                                  & " AND DEST1.DEST_CD = OUTKAL.DEST_CD                    " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_DEST DEST2                      " & vbNewLine _
                                                  & " ON                                                    " & vbNewLine _
                                                  & " DEST2.NRS_BR_CD = OUTKAL.NRS_BR_CD                    " & vbNewLine _
                                                  & " AND DEST2.CUST_CD_L = OUTKAL.CUST_CD_L                " & vbNewLine _
                                                  & " AND DEST2.DEST_CD = OUTKAL.SHIP_CD_L                  " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN Z1                          " & vbNewLine _
                                                  & " ON                                                    " & vbNewLine _
                                                  & " Z1.KBN_GROUP_CD = 'H018'                              " & vbNewLine _
                                                  & " AND Z1.KBN_CD = '01'                                  " & vbNewLine _
                                                  & " AND Z1.KBN_NM1 = OUTKAL.SHIP_CD_L                     " & vbNewLine _
                                                  & " LEFT JOIN $LM_TRN$..I_HONEY_SHIPTOCD_CHG SHIPTOCD1    " & vbNewLine _
                                                  & " ON                                                    " & vbNewLine _
                                                  & " SHIPTOCD1.SHIP_TO_CD = OUTKAL.DEST_CD                 " & vbNewLine _
                                                  & " LEFT JOIN $LM_TRN$..I_HONEY_SHIPTOCD_CHG SHIPTOCD2    " & vbNewLine _
                                                  & " ON                                                    " & vbNewLine _
                                                  & " SHIPTOCD2.SHIP_TO_CD = OUTKAL.SHIP_CD_L               " & vbNewLine

#End Region

#Region "出荷データ取得 SQL WHERE句"

    ''' <summary>
    ''' 出荷データ取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_OUTKA As String = "WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                                   & "  @CUSTDETAILS_OUTKA                                                           " & vbNewLine _
                                                   & "  AND (    (DATEDIFF(day,getdate(),OUTKAL.SYS_ENT_DATE) >= -10)                " & vbNewLine _
                                                   & "        OR (DATEDIFF(day,getdate(),OUTKAM.SYS_ENT_DATE) >= -10)                " & vbNewLine _
                                                   & "        OR (DATEDIFF(day,getdate(),OUTKAL.SYS_UPD_DATE) >= -10)                " & vbNewLine _
                                                   & "        OR (DATEDIFF(day,getdate(),OUTKAM.SYS_UPD_DATE) >= -10)                " & vbNewLine _
                                                   & "      )                                                                        " & vbNewLine _
                                                   & "  AND OUTKAL.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                                   & "  AND((isNull(CPW.SERIAL_NO,'') <> '') OR (isNull(OUTKAS.SERIAL_NO,'') <> '')) " & vbNewLine _
                                                   & "  AND OUTKAL.SYUBETU_KB = '10'                                                 " & vbNewLine

#End Region

#End Region

#End Region

#Region "削除対象データの削除処理"

#Region "削除対象データの削除 SQL"

    ''' <summary>
    ''' 削除対象データの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_DATA As String = "DELETE FROM $LM_TRN$..I_CONT_TRACK                                     " & vbNewLine _
                                            & " WHERE NRS_BR_CD    = @NRS_BR_CD                                       " & vbNewLine _
                                            & "   AND IOZS_KB      = @IOZS_KB                                         " & vbNewLine _
                                            & "   AND INOUTKA_NO_L = @INOUTKA_NO_L                                    " & vbNewLine

#End Region

    'タイムアウトのため修正 2013.02.27
#Region "削除対象データの削除 SQL"

    ''' <summary>
    ''' 削除対象データの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_DATA2 As String = "DELETE TRACK                      " & vbNewLine _
                                             & "FROM $LM_TRN$..I_CONT_TRACK TRACK " & vbNewLine _
                                             & "INNER JOIN(                       " & vbNewLine

    ''' <summary>
    ''' 削除対象データの削除 SQL JOIN句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_JOIN As String = ")TRACK2                                     " & vbNewLine _
                                            & " ON TRACK.NRS_BR_CD = TRACK2.NRS_BR_CD      " & vbNewLine _
                                            & "AND TRACK.IOZS_KB = TRACK2.IOZS_KB          " & vbNewLine _
                                            & "AND TRACK.INOUTKA_NO_L = TRACK2.INOUTKA_NO_L" & vbNewLine


#End Region
    'タイムアウトのため修正 2013.02.27

#End Region

#Region "入荷対象データの削除処理"

#Region "入荷対象データの削除 SQL"

    ''' <summary>
    ''' 入荷対象データの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_INKADATA As String = "DELETE FROM $LM_TRN$..I_CONT_TRACK                                     " & vbNewLine _
                                                & " WHERE NRS_BR_CD    = @NRS_BR_CD                                       " & vbNewLine _
                                                & "   AND SERIAL_NO    = @SERIAL_NO                                       " & vbNewLine _
                                                & "   AND INOUT_DATE   = @INOUT_DATE                                      " & vbNewLine _
                                                & "   AND IOZS_KB      = @IOZS_KB                                         " & vbNewLine _
                                                & "   AND INOUTKA_NO_L = @INOUTKA_NO_L                                    " & vbNewLine _
                                                & "   AND INOUTKA_NO_M = @INOUTKA_NO_M                                    " & vbNewLine _
                                                & "   --AND INOUTKA_NO_S = @INOUTKA_NO_S                                    " & vbNewLine

#End Region

    'タイムアウトのため修正 2013.02.27
#Region "入荷対象データの削除 SQL"

    ''' <summary>
    ''' 入荷対象データの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_INKADATA2 As String = "DELETE TRACK                       " & vbNewLine _
                                                 & " FROM $LM_TRN$..I_CONT_TRACK TRACK " & vbNewLine _
                                                 & "INNER JOIN(                        " & vbNewLine

    ''' <summary>
    ''' 入荷データ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_INKA As String = " SELECT DISTINCT                                                            " & vbNewLine _
                                            & " INKAL.NRS_BR_CD                                AS NRS_BR_CD                " & vbNewLine _
                                            & ",INKAS.SERIAL_NO                                AS SERIAL_NO                " & vbNewLine _
                                            & ",INKAL.INKA_DATE                                AS INOUT_DATE               " & vbNewLine _
                                            & ",'10'                                           AS IOZS_KB                  " & vbNewLine _
                                            & ",INKAS.INKA_NO_L                                AS INOUTKA_NO_L             " & vbNewLine

    ''' <summary>
    ''' 入荷データ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_FROM_INKA As String = "FROM                                                                  " & vbNewLine _
                                                 & "    (SELECT INKAL2.NRS_BR_CD                                          " & vbNewLine _
                                                 & "           ,INKAL2.INKA_NO_L                                          " & vbNewLine _
                                                 & "       FROM $LM_TRN$..B_INKA_L INKAL2                                 " & vbNewLine _
                                                 & "       INNER JOIN $LM_TRN$..B_INKA_M INKAM2                           " & vbNewLine _
                                                 & "       ON                                                             " & vbNewLine _
                                                 & "       INKAM2.NRS_BR_CD = INKAL2.NRS_BR_CD                            " & vbNewLine _
                                                 & "       AND INKAM2.INKA_NO_L = INKAL2.INKA_NO_L                        " & vbNewLine _
                                                 & "       AND INKAM2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                 & "       INNER JOIN $LM_TRN$..B_INKA_S INKAS2                           " & vbNewLine _
                                                 & "       ON                                                             " & vbNewLine _
                                                 & "       INKAS2.NRS_BR_CD = INKAM2.NRS_BR_CD                            " & vbNewLine _
                                                 & "       AND INKAS2.INKA_NO_L = INKAM2.INKA_NO_L                        " & vbNewLine _
                                                 & "       AND INKAS2.INKA_NO_M = INKAM2.INKA_NO_M                        " & vbNewLine _
                                                 & "       AND INKAS2.SERIAL_NO <> ''                                     " & vbNewLine _
                                                 & "       AND INKAS2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                 & "      WHERE                                                           " & vbNewLine _
                                                 & "       INKAL2.NRS_BR_CD = @NRS_BR_CD                                  " & vbNewLine _
                                                 & "       @CUSTDETAILS_INKA                                              " & vbNewLine _
                                                 & "       AND (    (DATEDIFF(day,getdate(),INKAL2.SYS_ENT_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAM2.SYS_ENT_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAS2.SYS_ENT_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAL2.SYS_UPD_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAM2.SYS_UPD_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAS2.SYS_UPD_DATE) >= -10)  " & vbNewLine _
                                                 & "           )                                                          " & vbNewLine _
                                                 & "      GROUP BY                                                        " & vbNewLine _
                                                 & "       INKAL2.NRS_BR_CD                                               " & vbNewLine _
                                                 & "      ,INKAL2.INKA_NO_L                                               " & vbNewLine _
                                                 & "    ) AS GET                                                          " & vbNewLine _
                                                 & "    INNER JOIN $LM_TRN$..B_INKA_L INKAL                               " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    INKAL.NRS_BR_CD = GET.NRS_BR_CD                                   " & vbNewLine _
                                                 & "    AND INKAL.INKA_NO_L = GET.INKA_NO_L                               " & vbNewLine _
                                                 & "    AND INKAL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    INNER JOIN $LM_TRN$..B_INKA_M INKAM                               " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                             " & vbNewLine _
                                                 & "    AND INKAM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    INNER JOIN $LM_TRN$..B_INKA_S INKAS                               " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                             " & vbNewLine _
                                                 & "    AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                             " & vbNewLine _
                                                 & "    AND INKAS.SERIAL_NO <> ''                                         " & vbNewLine _
                                                 & "    AND INKAS.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    UNSOL.NRS_BR_CD = INKAL.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND UNSOL.INOUTKA_NO_L = INKAL.INKA_NO_L                          " & vbNewLine _
                                                 & "    AND UNSOL.MOTO_DATA_KB = '10'                                     " & vbNewLine _
                                                 & "    AND UNSOL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    INNER JOIN $LM_MST$..M_GOODS GOODS                                " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                       " & vbNewLine _
                                                 & "    LEFT JOIN $LM_MST$..M_DEST DEST                                   " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    DEST.NRS_BR_CD = UNSOL.NRS_BR_CD                                  " & vbNewLine _
                                                 & "    AND DEST.CUST_CD_L = UNSOL.CUST_CD_L                              " & vbNewLine _
                                                 & "    AND DEST.DEST_CD = UNSOL.ORIG_CD                                  " & vbNewLine


    ''' <summary>
    ''' 削除対象データの削除 SQL JOIN句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_INKA_JOIN As String = ")TRACK2                                     " & vbNewLine _
                                                 & " ON TRACK.NRS_BR_CD = TRACK2.NRS_BR_CD        " & vbNewLine _
                                                 & "AND TRACK.SERIAL_NO = TRACK2.SERIAL_NO        " & vbNewLine _
                                                 & "AND TRACK.INOUT_DATE = TRACK2.INOUT_DATE      " & vbNewLine _
                                                 & "AND TRACK.IOZS_KB = TRACK2.IOZS_KB            " & vbNewLine _
                                                 & "AND TRACK.INOUTKA_NO_L = TRACK2.INOUTKA_NO_L  " & vbNewLine

#End Region
    'タイムアウトのため修正 2013.02.27

#End Region

#Region "出荷対象データの削除処理"

#Region "出荷対象データの削除 SQL"

    ''' <summary>
    ''' 出荷対象データの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_OUTKADATA As String = "DELETE FROM $LM_TRN$..I_CONT_TRACK                                     " & vbNewLine _
                                                 & " WHERE NRS_BR_CD    = @NRS_BR_CD                                       " & vbNewLine _
                                                 & "   AND SERIAL_NO    = @SERIAL_NO                                       " & vbNewLine _
                                                 & "   AND INOUT_DATE   = @INOUT_DATE                                      " & vbNewLine _
                                                 & "   AND IOZS_KB      = @IOZS_KB                                         " & vbNewLine _
                                                 & "   AND INOUTKA_NO_L = @INOUTKA_NO_L                                    " & vbNewLine _
                                                 & "   AND INOUTKA_NO_M = @INOUTKA_NO_M                                    " & vbNewLine _
                                                 & "  -- AND INOUTKA_NO_S = @INOUTKA_NO_S                                    " & vbNewLine

#End Region

    'タイムアウトのため修正 2013.02.27
#Region "出荷対象データの削除 SQL"

    ''' <summary>
    ''' 出荷対象データの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_OUTKADATA2 As String = "DELETE TRACK                       " & vbNewLine _
                                                  & " FROM $LM_TRN$..I_CONT_TRACK TRACK " & vbNewLine _
                                                  & "INNER JOIN(                        " & vbNewLine

    ''' <summary>
    ''' 出荷データ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_OUTKA As String = " SELECT                                                                     " & vbNewLine _
                                             & " OUTKAL.NRS_BR_CD                               AS NRS_BR_CD                " & vbNewLine _
                                             & ",OUTKAS.SERIAL_NO                               AS SERIAL_NO                " & vbNewLine _
                                             & ",OUTKAL.OUTKA_PLAN_DATE                         AS INOUT_DATE               " & vbNewLine _
                                             & ",'20'                                           AS IOZS_KB                  " & vbNewLine _
                                             & ",OUTKAS.OUTKA_NO_L                              AS INOUTKA_NO_L             " & vbNewLine

    ''' <summary>
    ''' 出荷データ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_FROM_OUTKA As String = "FROM                                                                  " & vbNewLine _
                                                  & "$LM_TRN$..C_OUTKA_L OUTKAL                                            " & vbNewLine _
                                                  & " INNER JOIN $LM_TRN$..C_OUTKA_M OUTKAM                                " & vbNewLine _
                                                  & " ON                                                                   " & vbNewLine _
                                                  & " OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                  " & vbNewLine _
                                                  & " AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                            " & vbNewLine _
                                                  & " AND OUTKAM.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                  & " INNER JOIN $LM_TRN$..C_OUTKA_S OUTKAS                                " & vbNewLine _
                                                  & " ON                                                                   " & vbNewLine _
                                                  & " OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                  " & vbNewLine _
                                                  & " AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                            " & vbNewLine _
                                                  & " AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                            " & vbNewLine _
                                                  & " AND OUTKAS.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                  & " INNER JOIN $LM_MST$..M_GOODS GOODS                                   " & vbNewLine _
                                                  & " ON                                                                   " & vbNewLine _
                                                  & " GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                   " & vbNewLine _
                                                  & " AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                         " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_DEST DEST1                                     " & vbNewLine _
                                                  & " ON                                                                   " & vbNewLine _
                                                  & " DEST1.NRS_BR_CD = OUTKAL.NRS_BR_CD                                   " & vbNewLine _
                                                  & " AND DEST1.CUST_CD_L = OUTKAL.CUST_CD_L                               " & vbNewLine _
                                                  & " AND DEST1.DEST_CD = OUTKAL.DEST_CD                                   " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_DEST DEST2                                     " & vbNewLine _
                                                  & " ON                                                                   " & vbNewLine _
                                                  & " DEST2.NRS_BR_CD = OUTKAL.NRS_BR_CD                                   " & vbNewLine _
                                                  & " AND DEST2.CUST_CD_L = OUTKAL.CUST_CD_L                               " & vbNewLine _
                                                  & " AND DEST2.DEST_CD = OUTKAL.SHIP_CD_L                                 " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN Z1                                         " & vbNewLine _
                                                  & " ON                                                                   " & vbNewLine _
                                                  & " Z1.KBN_GROUP_CD = 'H018'                                             " & vbNewLine _
                                                  & " AND Z1.KBN_CD = '01'                                                 " & vbNewLine _
                                                  & " AND Z1.KBN_NM1 = OUTKAL.SHIP_CD_L                                    " & vbNewLine _
                                                  & " LEFT JOIN $LM_TRN$..I_HONEY_SHIPTOCD_CHG SHIPTOCD1                   " & vbNewLine _
                                                  & " ON                                                                   " & vbNewLine _
                                                  & " SHIPTOCD1.SHIP_TO_CD = OUTKAL.DEST_CD                                " & vbNewLine _
                                                  & " LEFT JOIN $LM_TRN$..I_HONEY_SHIPTOCD_CHG SHIPTOCD2                   " & vbNewLine _
                                                  & " ON                                                                   " & vbNewLine _
                                                  & " SHIPTOCD2.SHIP_TO_CD = OUTKAL.SHIP_CD_L                              " & vbNewLine

    ''' <summary>
    ''' 出荷データ取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WHERE_OUTKA As String = "WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                  " & vbNewLine _
                                                   & "  @CUSTDETAILS_OUTKA                                                 " & vbNewLine _
                                                   & "  AND (    (DATEDIFF(day,getdate(),OUTKAL.SYS_ENT_DATE) >= -10)      " & vbNewLine _
                                                   & "        OR (DATEDIFF(day,getdate(),OUTKAM.SYS_ENT_DATE) >= -10)      " & vbNewLine _
                                                   & "        OR (DATEDIFF(day,getdate(),OUTKAS.SYS_ENT_DATE) >= -10)      " & vbNewLine _
                                                   & "        OR (DATEDIFF(day,getdate(),OUTKAL.SYS_UPD_DATE) >= -10)      " & vbNewLine _
                                                   & "        OR (DATEDIFF(day,getdate(),OUTKAM.SYS_UPD_DATE) >= -10)      " & vbNewLine _
                                                   & "        OR (DATEDIFF(day,getdate(),OUTKAS.SYS_UPD_DATE) >= -10)      " & vbNewLine _
                                                   & "      )                                                              " & vbNewLine _
                                                   & "  AND OUTKAL.SYS_DEL_FLG = '0'                                       " & vbNewLine

    ''' <summary>
    ''' 削除対象データの削除 SQL JOIN句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_OUTKA_JOIN As String = ")TRACK2                                       " & vbNewLine _
                                                  & " ON TRACK.NRS_BR_CD = TRACK2.NRS_BR_CD        " & vbNewLine _
                                                  & "AND TRACK.SERIAL_NO = TRACK2.SERIAL_NO        " & vbNewLine _
                                                  & "AND TRACK.INOUT_DATE = TRACK2.INOUT_DATE      " & vbNewLine _
                                                  & "AND TRACK.IOZS_KB = TRACK2.IOZS_KB            " & vbNewLine _
                                                  & "AND TRACK.INOUTKA_NO_L = TRACK2.INOUTKA_NO_L  " & vbNewLine

#End Region
    'タイムアウトのため修正 2013.02.27

#End Region

#Region "ハネウェル用容器管理データの追加処理"

#Region "ハネウェル用容器管理データの追加 SQL"

    ''' <summary>
    ''' ハネウェル用容器管理データの追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_CONTTRACK As String = "INSERT INTO $LM_TRN$..I_CONT_TRACK                  " & vbNewLine _
                                                 & " ( 		                                            " & vbNewLine _
                                                 & " NRS_BR_CD,                                         " & vbNewLine _
                                                 & " SERIAL_NO,                                         " & vbNewLine _
                                                 & " INOUT_DATE,                                        " & vbNewLine _
                                                 & " IOZS_KB,                                           " & vbNewLine _
                                                 & " INOUTKA_NO_L,                                      " & vbNewLine _
                                                 & " INOUTKA_NO_M,                                      " & vbNewLine _
                                                 & " INOUTKA_NO_S,                                      " & vbNewLine _
                                                 & " STATUS,                                            " & vbNewLine _
                                                 & " WH_CD,                                             " & vbNewLine _
                                                 & " CUST_CD_L,                                         " & vbNewLine _
                                                 & " CUST_CD_M,                                         " & vbNewLine _
                                                 & " REMARK,                                            " & vbNewLine _
                                                 & " TOFROM_CD,                                         " & vbNewLine _
                                                 & " TOFROM_NM,                                         " & vbNewLine _
                                                 & " EXP_FLG,                                           " & vbNewLine _
                                                 & " GOODS_CD_NRS,                                      " & vbNewLine _
                                                 & " GOODS_CD_CUST,                                     " & vbNewLine _
                                                 & " GOODS_NM,                                          " & vbNewLine _
                                                 & " CUST_ORD_NO_DTL,                                   " & vbNewLine _
                                                 & " BUYER_ORD_NO_DTL,                                  " & vbNewLine _
                                                 & " --NB,                                                " & vbNewLine _
                                                 & " --QT,                                                " & vbNewLine _
                                                 & " --LOT_NO,                                            " & vbNewLine _
                                                 & " CYLINDER_TYPE,                                     " & vbNewLine _
                                                 & " EMPTY_KB,                                          " & vbNewLine _
                                                 & " HAIKI_YN,                                          " & vbNewLine _
                                                 & " SHIP_CD_L,                                         " & vbNewLine _
                                                 & " SHIP_NM_L,                                         " & vbNewLine _
                                                 & " FREE_N01,                                          " & vbNewLine _
                                                 & " FREE_N02,                                          " & vbNewLine _
                                                 & " FREE_N03,                                          " & vbNewLine _
                                                 & " FREE_N04,                                          " & vbNewLine _
                                                 & " FREE_N05,                                          " & vbNewLine _
                                                 & " FREE_C01,                                          " & vbNewLine _
                                                 & " FREE_C02,                                          " & vbNewLine _
                                                 & " FREE_C03,                                          " & vbNewLine _
                                                 & " FREE_C04,                                          " & vbNewLine _
                                                 & " FREE_C05,                                          " & vbNewLine _
                                                 & " SYS_ENT_DATE,                                      " & vbNewLine _
                                                 & " SYS_ENT_TIME,                                      " & vbNewLine _
                                                 & " SYS_ENT_PGID,                                      " & vbNewLine _
                                                 & " SYS_ENT_USER,                                      " & vbNewLine _
                                                 & " SYS_UPD_DATE,                                      " & vbNewLine _
                                                 & " SYS_UPD_TIME,                                      " & vbNewLine _
                                                 & " SYS_UPD_PGID,                                      " & vbNewLine _
                                                 & " SYS_UPD_USER,                                      " & vbNewLine _
                                                 & " SYS_DEL_FLG                                        " & vbNewLine _
                                                 & " ) VALUES (                                         " & vbNewLine _
                                                 & " @NRS_BR_CD,                                        " & vbNewLine _
                                                 & " @SERIAL_NO,                                        " & vbNewLine _
                                                 & " @INOUT_DATE,                                       " & vbNewLine _
                                                 & " @IOZS_KB,                                          " & vbNewLine _
                                                 & " @INOUTKA_NO_L,                                     " & vbNewLine _
                                                 & " @INOUTKA_NO_M,                                     " & vbNewLine _
                                                 & " @INOUTKA_NO_S,                                     " & vbNewLine _
                                                 & " @STATUS,                                           " & vbNewLine _
                                                 & " @WH_CD,                                            " & vbNewLine _
                                                 & " @CUST_CD_L,                                        " & vbNewLine _
                                                 & " @CUST_CD_M,                                        " & vbNewLine _
                                                 & " @REMARK,                                           " & vbNewLine _
                                                 & " @TOFROM_CD,                                        " & vbNewLine _
                                                 & " @TOFROM_NM,                                        " & vbNewLine _
                                                 & " @EXP_FLG,                                          " & vbNewLine _
                                                 & " @GOODS_CD_NRS,                                     " & vbNewLine _
                                                 & " @GOODS_CD_CUST,                                    " & vbNewLine _
                                                 & " @GOODS_NM,                                         " & vbNewLine _
                                                 & " @CUST_ORD_NO_DTL,                                  " & vbNewLine _
                                                 & " @BUYER_ORD_NO_DTL,                                 " & vbNewLine _
                                                 & " --@NB,                                               " & vbNewLine _
                                                 & " --@QT,                                               " & vbNewLine _
                                                 & " --@LOT_NO,                                           " & vbNewLine _
                                                 & " @CYLINDER_TYPE,                                    " & vbNewLine _
                                                 & " @EMPTY_KB,                                         " & vbNewLine _
                                                 & " @HAIKI_YN,                                         " & vbNewLine _
                                                 & " @SHIP_CD_L,                                        " & vbNewLine _
                                                 & " @SHIP_NM_L,                                        " & vbNewLine _
                                                 & " @FREE_N01,                                         " & vbNewLine _
                                                 & " @FREE_N02,                                         " & vbNewLine _
                                                 & " @FREE_N03,                                         " & vbNewLine _
                                                 & " @FREE_N04,                                         " & vbNewLine _
                                                 & " @FREE_N05,                                         " & vbNewLine _
                                                 & " @FREE_C01,                                         " & vbNewLine _
                                                 & " @FREE_C02,                                         " & vbNewLine _
                                                 & " @FREE_C03,                                         " & vbNewLine _
                                                 & " @FREE_C04,                                         " & vbNewLine _
                                                 & " @FREE_C05,                                         " & vbNewLine _
                                                 & " @SYS_ENT_DATE,                                     " & vbNewLine _
                                                 & " @SYS_ENT_TIME,                                     " & vbNewLine _
                                                 & " @SYS_ENT_PGID,                                     " & vbNewLine _
                                                 & " @SYS_ENT_USER,                                     " & vbNewLine _
                                                 & " @SYS_UPD_DATE,                                     " & vbNewLine _
                                                 & " @SYS_UPD_TIME,                                     " & vbNewLine _
                                                 & " @SYS_UPD_PGID,                                     " & vbNewLine _
                                                 & " @SYS_UPD_USER,                                     " & vbNewLine _
                                                 & " @SYS_DEL_FLG                                       " & vbNewLine _
                                                 & " )                                                  " & vbNewLine

#End Region

    'タイムアウトのため修正 2013.02.27
#Region "ハネウェル用容器管理データの追加 SQL"

    ''' <summary>
    ''' ハネウェル用容器管理データの追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_CONTTRACK2 As String = "INSERT INTO $LM_TRN$..I_CONT_TRACK                  " & vbNewLine _
                                                 & " ( 		                                            " & vbNewLine _
                                                 & " NRS_BR_CD,                                         " & vbNewLine _
                                                 & " SERIAL_NO,                                         " & vbNewLine _
                                                 & " INOUT_DATE,                                        " & vbNewLine _
                                                 & " IOZS_KB,                                           " & vbNewLine _
                                                 & " INOUTKA_NO_L,                                      " & vbNewLine _
                                                 & " INOUTKA_NO_M,                                      " & vbNewLine _
                                                 & " INOUTKA_NO_S,                                      " & vbNewLine _
                                                 & " STATUS,                                            " & vbNewLine _
                                                 & " WH_CD,                                             " & vbNewLine _
                                                 & " CUST_CD_L,                                         " & vbNewLine _
                                                 & " CUST_CD_M,                                         " & vbNewLine _
                                                 & " REMARK,                                            " & vbNewLine _
                                                 & " TOFROM_CD,                                         " & vbNewLine _
                                                 & " TOFROM_NM,                                         " & vbNewLine _
                                                 & " EXP_FLG,                                           " & vbNewLine _
                                                 & " GOODS_CD_NRS,                                      " & vbNewLine _
                                                 & " GOODS_CD_CUST,                                     " & vbNewLine _
                                                 & " GOODS_NM,                                          " & vbNewLine _
                                                 & " CUST_ORD_NO_DTL,                                   " & vbNewLine _
                                                 & " BUYER_ORD_NO_DTL,                                  " & vbNewLine _
                                                 & " --NB,                                                " & vbNewLine _
                                                 & " --QT,                                                " & vbNewLine _
                                                 & " --LOT_NO,                                            " & vbNewLine _
                                                 & " CYLINDER_TYPE,                                     " & vbNewLine _
                                                 & " EMPTY_KB,                                          " & vbNewLine _
                                                 & " HAIKI_YN,                                          " & vbNewLine _
                                                 & " SHIP_CD_L,                                         " & vbNewLine _
                                                 & " SHIP_NM_L,                                         " & vbNewLine _
                                                 & " FREE_N01,                                          " & vbNewLine _
                                                 & " FREE_N02,                                          " & vbNewLine _
                                                 & " FREE_N03,                                          " & vbNewLine _
                                                 & " FREE_N04,                                          " & vbNewLine _
                                                 & " FREE_N05,                                          " & vbNewLine _
                                                 & " FREE_C01,                                          " & vbNewLine _
                                                 & " FREE_C02,                                          " & vbNewLine _
                                                 & " FREE_C03,                                          " & vbNewLine _
                                                 & " FREE_C04,                                          " & vbNewLine _
                                                 & " FREE_C05,                                          " & vbNewLine _
                                                 & " SYS_ENT_DATE,                                      " & vbNewLine _
                                                 & " SYS_ENT_TIME,                                      " & vbNewLine _
                                                 & " SYS_ENT_PGID,                                      " & vbNewLine _
                                                 & " SYS_ENT_USER,                                      " & vbNewLine _
                                                 & " SYS_UPD_DATE,                                      " & vbNewLine _
                                                 & " SYS_UPD_TIME,                                      " & vbNewLine _
                                                 & " SYS_UPD_PGID,                                      " & vbNewLine _
                                                 & " SYS_UPD_USER,                                      " & vbNewLine _
                                                 & " SYS_DEL_FLG                                        " & vbNewLine _
                                                 & " )                                                  " & vbNewLine

    ''' <summary>
    ''' 入荷データ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKA2 As String = " SELECT DISTINCT                                                           " & vbNewLine _
                                            & " INKAL.NRS_BR_CD                                AS NRS_BR_CD                " & vbNewLine _
                                            & ",INKAS.SERIAL_NO                                AS SERIAL_NO                " & vbNewLine _
                                            & ",INKAL.INKA_DATE                                AS INOUT_DATE               " & vbNewLine _
                                            & ",'10'                                           AS IOZS_KB                  " & vbNewLine _
                                            & ",INKAS.INKA_NO_L                                AS INOUTKA_NO_L             " & vbNewLine _
                                            & ",INKAS.INKA_NO_M                                AS INOUTKA_NO_M             " & vbNewLine _
                                            & ",INKAS.INKA_NO_S                                AS INOUTKA_NO_S             " & vbNewLine _
                                            & ",CASE WHEN '50' = INKAL.INKA_STATE_KB                                       " & vbNewLine _
                                            & "      THEN '1'                                                              " & vbNewLine _
                                            & "      WHEN '90' = INKAL.INKA_STATE_KB                                       " & vbNewLine _
                                            & "      THEN '1'                                                              " & vbNewLine _
                                            & "      ELSE '0'                                                              " & vbNewLine _
                                            & " END                                            AS STATUS                   " & vbNewLine _
                                            & ",INKAL.WH_CD                                    AS WH_CD                    " & vbNewLine _
                                            & ",INKAL.CUST_CD_L                                AS CUST_CD_L                " & vbNewLine _
                                            & ",INKAL.CUST_CD_M                                AS CUST_CD_M                " & vbNewLine _
                                            & ",INKAL.REMARK                                   AS REMARK                   " & vbNewLine _
                                            & ",ISNULL(UNSOL.ORIG_CD,'')                       AS TOFROM_CD                " & vbNewLine _
                                            & ",ISNULL(DEST.DEST_NM,'')                        AS TOFROM_NM                " & vbNewLine _
                                            & ",'0'                                            AS EXP_FLG                  " & vbNewLine _
                                            & ",INKAM.GOODS_CD_NRS                             AS GOODS_CD_NRS             " & vbNewLine _
                                            & ",GOODS.GOODS_CD_CUST                            AS GOODS_CD_CUST            " & vbNewLine _
                                            & ",GOODS.GOODS_NM_1                               AS GOODS_NM                 " & vbNewLine _
                                            & ",INKAM.OUTKA_FROM_ORD_NO_M                      AS CUST_ORD_NO_DTL          " & vbNewLine _
                                            & ",INKAM.BUYER_ORD_NO_M                           AS BUYER_ORD_NO_DTL         " & vbNewLine _
                                            & "--,INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU        AS NB                       " & vbNewLine _
                                            & "--,(INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) * GOODS.STD_IRIME_NB AS QT       " & vbNewLine _
                                            & "--,INKAS.LOT_NO                                   AS LOT_NO                   " & vbNewLine _
                                            & ",GOODS.CUST_COST_CD1                            AS CYLINDER_TYPE            " & vbNewLine _
                                            & ",GOODS.CUST_COST_CD2                            AS EMPTY_KB                 " & vbNewLine _
                                            & ",'0'                                            AS HAIKI_YN                 " & vbNewLine _
                                            & ",''                                             AS SHIP_CD_L                " & vbNewLine _
                                            & ",''                                             AS SHIP_NM_L                " & vbNewLine _
                                            & ",'0'                                            AS FREE_N01                 " & vbNewLine _
                                            & ",'0'                                            AS FREE_N02                 " & vbNewLine _
                                            & ",'0'                                            AS FREE_N03                 " & vbNewLine _
                                            & ",'0'                                            AS FREE_N04                 " & vbNewLine _
                                            & ",'0'                                            AS FREE_N05                 " & vbNewLine _
                                            & ",''                                             AS FREE_C01                 " & vbNewLine _
                                            & ",''                                             AS FREE_C02                 " & vbNewLine _
                                            & ",''                                             AS FREE_C03                 " & vbNewLine _
                                            & ",''                                             AS FREE_C04                 " & vbNewLine _
                                            & ",''                                             AS FREE_C05                 " & vbNewLine

    Private Const SQL_SELECT_SYSTEM As String = ", @SYS_ENT_DATE                               AS SYS_ENT_DATE             " & vbNewLine _
                                              & ", @SYS_ENT_TIME                               AS SYS_ENT_TIME             " & vbNewLine _
                                              & ", @SYS_ENT_PGID                               AS SYS_ENT_PGID             " & vbNewLine _
                                              & ", @SYS_ENT_USER                               AS SYS_ENT_USER             " & vbNewLine _
                                              & ", @SYS_UPD_DATE                               AS SYS_UPD_DATE             " & vbNewLine _
                                              & ", @SYS_UPD_TIME                               AS SYS_UPD_TIME             " & vbNewLine _
                                              & ", @SYS_UPD_PGID                               AS SYS_UPD_PGID             " & vbNewLine _
                                              & ", @SYS_UPD_USER                               AS SYS_UPD_USER             " & vbNewLine _
                                              & ", @SYS_DEL_FLG                                AS SYS_DEL_FLG              " & vbNewLine



    ''' <summary>
    ''' 入荷データ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_INKA2 As String = "FROM                                                                 " & vbNewLine _
                                                 & "    (SELECT INKAL2.NRS_BR_CD                                          " & vbNewLine _
                                                 & "           ,INKAL2.INKA_NO_L                                          " & vbNewLine _
                                                 & "       FROM $LM_TRN$..B_INKA_L INKAL2                                 " & vbNewLine _
                                                 & "       INNER JOIN $LM_TRN$..B_INKA_M INKAM2                           " & vbNewLine _
                                                 & "       ON                                                             " & vbNewLine _
                                                 & "       INKAM2.NRS_BR_CD = INKAL2.NRS_BR_CD                            " & vbNewLine _
                                                 & "       AND INKAM2.INKA_NO_L = INKAL2.INKA_NO_L                        " & vbNewLine _
                                                 & "       AND INKAM2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                 & "       INNER JOIN $LM_TRN$..B_INKA_S INKAS2                           " & vbNewLine _
                                                 & "       ON                                                             " & vbNewLine _
                                                 & "       INKAS2.NRS_BR_CD = INKAM2.NRS_BR_CD                            " & vbNewLine _
                                                 & "       AND INKAS2.INKA_NO_L = INKAM2.INKA_NO_L                        " & vbNewLine _
                                                 & "       AND INKAS2.INKA_NO_M = INKAM2.INKA_NO_M                        " & vbNewLine _
                                                 & "       AND INKAS2.SERIAL_NO <> ''                                     " & vbNewLine _
                                                 & "       AND INKAS2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                 & "      WHERE                                                           " & vbNewLine _
                                                 & "       INKAL2.NRS_BR_CD = @NRS_BR_CD                                  " & vbNewLine _
                                                 & "       @CUSTDETAILS_INKA                                              " & vbNewLine _
                                                 & "       AND (    (DATEDIFF(day,getdate(),INKAL2.SYS_ENT_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAM2.SYS_ENT_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAS2.SYS_ENT_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAL2.SYS_UPD_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAM2.SYS_UPD_DATE) >= -10)  " & vbNewLine _
                                                 & "             OR (DATEDIFF(day,getdate(),INKAS2.SYS_UPD_DATE) >= -10)  " & vbNewLine _
                                                 & "           )                                                          " & vbNewLine _
                                                 & "      GROUP BY                                                        " & vbNewLine _
                                                 & "       INKAL2.NRS_BR_CD                                               " & vbNewLine _
                                                 & "      ,INKAL2.INKA_NO_L                                               " & vbNewLine _
                                                 & "    ) AS GET                                                          " & vbNewLine _
                                                 & "    INNER JOIN $LM_TRN$..B_INKA_L INKAL                               " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    INKAL.NRS_BR_CD = GET.NRS_BR_CD                                   " & vbNewLine _
                                                 & "    AND INKAL.INKA_NO_L = GET.INKA_NO_L                               " & vbNewLine _
                                                 & "    AND INKAL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    INNER JOIN $LM_TRN$..B_INKA_M INKAM                               " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                             " & vbNewLine _
                                                 & "    AND INKAM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    INNER JOIN $LM_TRN$..B_INKA_S INKAS                               " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                             " & vbNewLine _
                                                 & "    AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                             " & vbNewLine _
                                                 & "    AND INKAS.SERIAL_NO <> ''                                         " & vbNewLine _
                                                 & "    AND INKAS.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    UNSOL.NRS_BR_CD = INKAL.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND UNSOL.INOUTKA_NO_L = INKAL.INKA_NO_L                          " & vbNewLine _
                                                 & "    AND UNSOL.MOTO_DATA_KB = '10'                                     " & vbNewLine _
                                                 & "    AND UNSOL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                 & "    INNER JOIN $LM_MST$..M_GOODS GOODS                                " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                 " & vbNewLine _
                                                 & "    AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                       " & vbNewLine _
                                                 & "    LEFT JOIN $LM_MST$..M_DEST DEST                                   " & vbNewLine _
                                                 & "    ON                                                                " & vbNewLine _
                                                 & "    DEST.NRS_BR_CD = UNSOL.NRS_BR_CD                                  " & vbNewLine _
                                                 & "    AND DEST.CUST_CD_L = UNSOL.CUST_CD_L                              " & vbNewLine _
                                                 & "    AND DEST.DEST_CD = UNSOL.ORIG_CD                                  " & vbNewLine


#End Region
    'タイムアウトのため修正 2013.02.27

#End Region

#Region "ハネウェル用データ管理取得ログ作成処理理"

#Region "ハネウェル用データ管理取得ログ作成 SQL"

    ''' <summary>
    ''' ハネウェル用データ管理取得ログ作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_CONTTRACKLOG As String = "INSERT INTO $LM_TRN$..I_CONT_TRACK_LOG               " & vbNewLine _
                                                    & " ( 		                                            " & vbNewLine _
                                                    & " NRS_BR_CD,                                          " & vbNewLine _
                                                    & " STATUS,                                             " & vbNewLine _
                                                    & " R_CNT,                                              " & vbNewLine _
                                                    & " SYS_ENT_DATE,                                       " & vbNewLine _
                                                    & " SYS_ENT_TIME,                                       " & vbNewLine _
                                                    & " SYS_ENT_PGID,                                       " & vbNewLine _
                                                    & " SYS_ENT_USER,                                       " & vbNewLine _
                                                    & " SYS_UPD_DATE,                                       " & vbNewLine _
                                                    & " SYS_UPD_TIME,                                       " & vbNewLine _
                                                    & " SYS_UPD_PGID,                                       " & vbNewLine _
                                                    & " SYS_UPD_USER,                                       " & vbNewLine _
                                                    & " SYS_DEL_FLG                                         " & vbNewLine _
                                                    & " ) VALUES (                                          " & vbNewLine _
                                                    & " @NRS_BR_CD,                                         " & vbNewLine _
                                                    & " @STATUS,                                            " & vbNewLine _
                                                    & " @R_CNT,                                             " & vbNewLine _
                                                    & " @SYS_ENT_DATE,                                      " & vbNewLine _
                                                    & " @SYS_ENT_TIME,                                      " & vbNewLine _
                                                    & " @SYS_ENT_PGID,                                      " & vbNewLine _
                                                    & " @SYS_ENT_USER,                                      " & vbNewLine _
                                                    & " @SYS_UPD_DATE,                                      " & vbNewLine _
                                                    & " @SYS_UPD_TIME,                                      " & vbNewLine _
                                                    & " @SYS_UPD_PGID,                                      " & vbNewLine _
                                                    & " @SYS_UPD_USER,                                      " & vbNewLine _
                                                    & " @SYS_DEL_FLG                                        " & vbNewLine _
                                                    & " )                                                   " & vbNewLine

#End Region

#End Region

#Region "シリンダタイプコンボの値を取得"

#Region "シリンダタイプコンボの値を取得 SQL"

#Region "シリンダタイプコンボの値を取得 SQL SELECT句"

    ''' <summary>
    ''' シリンダタイプコンボの値を取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CYLINDER As String = " SELECT                                                                     " & vbNewLine _
                                                & " CONTTRACK.CYLINDER_TYPE                        AS CYLINDER_TYPE            " & vbNewLine

#End Region

#Region "シリンダタイプコンボの値を取得 SQL FROM句"

    ''' <summary>
    ''' シリンダタイプコンボの値を取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CYLINDER As String = "FROM                                                                   " & vbNewLine _
                                                     & "$LM_TRN$..I_CONT_TRACK CONTTRACK                                       " & vbNewLine

#End Region

#Region "シリンダタイプコンボの値を取得 SQL WHERE句"

    ''' <summary>
    ''' シリンダタイプコンボの値を取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_CYLINDER As String = "WHERE CONTTRACK.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                                      & "  AND CONTTRACK.CYLINDER_TYPE <> ''                                  " & vbNewLine

#End Region

#Region "シリンダタイプコンボの値を取得 SQL GROUP BY句"

    ''' <summary>
    ''' シリンダタイプコンボの値を取得 SQL GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_CYLINDER As String = "GROUP BY                                                              " & vbNewLine _
                                                      & " CONTTRACK.CYLINDER_TYPE                                              " & vbNewLine

#End Region

#Region "シリンダタイプコンボの値を取得 SQL ORDER BY句"

    ''' <summary>
    ''' シリンダタイプコンボの値を取得 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_CYLINDER As String = "ORDER BY                                                              " & vbNewLine _
                                                      & " CONTTRACK.CYLINDER_TYPE                                              " & vbNewLine

#End Region

#End Region

#End Region

#Region "在庫場所コンボの値を取得"

#Region "在庫場所コンボの値を取得 SQL"

#Region "在庫場所コンボの値を取得 SQL SELECT句"

    ''' <summary>
    ''' 在庫場所コンボの値を取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TOFROMNM As String = " SELECT                                                                     " & vbNewLine _
                                                & " CONTTRACK.TOFROM_NM                            AS TOFROM_NM                " & vbNewLine

#End Region

#Region "在庫場所コンボの値を取得 SQL FROM句"

    ''' <summary>
    ''' 在庫場所コンボの値を取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_TOFROMNM As String = "FROM                                                                   " & vbNewLine _
                                                     & "$LM_TRN$..I_CONT_TRACK CONTTRACK                                       " & vbNewLine

#End Region

#Region "在庫場所コンボの値を取得 SQL WHERE句"

    ''' <summary>
    ''' 在庫場所コンボの値を取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_TOFROMNM As String = "WHERE CONTTRACK.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                                      & "  AND CONTTRACK.TOFROM_NM <> ''                                      " & vbNewLine

#End Region

#Region "在庫場所コンボの値を取得 SQL GROUP BY句"

    ''' <summary>
    ''' 在庫場所コンボの値を取得 SQL GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_TOFROMNM As String = "GROUP BY                                                              " & vbNewLine _
                                                      & " CONTTRACK.TOFROM_NM                                                  " & vbNewLine

#End Region

#Region "在庫場所コンボの値を取得 SQL ORDER BY句"

    ''' <summary>
    ''' 在庫場所コンボの値を取得 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_TOFROMNM As String = "ORDER BY                                                              " & vbNewLine _
                                                      & " CONTTRACK.TOFROM_NM                                                  " & vbNewLine

#End Region

#End Region

#End Region

#Region "共通検索データ件数取得処理"

#Region "共通検索データ件数取得処理 SQL"

#Region "共通検索データ件数取得処理 SQL SELECT句"

    ''' <summary>
    ''' 共通検索データ件数取得処理 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(*)                                  AS SELECT_CNT        " & vbNewLine

#End Region

#End Region

#End Region

#Region "履歴・廃棄済用検索データ件数取得処理"

#Region "履歴・廃棄済用検索データ件数取得処理 SQL"

#Region "履歴・廃棄済用検索データ件数取得処理 SQL FROM句"

    ''' <summary>
    ''' 履歴・廃棄済用検索データ件数取得処理 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_RIREKIHAIKI As String = _
                " FROM $LM_TRN$..I_CONT_TRACK CONTTRACK            " & vbNewLine _
              & " --(2013.09.11) 要望番号2095 追加START            " & vbNewLine _
              & " LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD           " & vbNewLine _
              & " ON                                               " & vbNewLine _
              & "     MCD.NRS_BR_CD          = CONTTRACK.NRS_BR_CD " & vbNewLine _
              & " AND MCD.CUST_CD            = CONTTRACK.CUST_CD_L " & vbNewLine _
              & " AND MCD.SUB_KB             = '44'                " & vbNewLine _
              & " AND MCD.SET_NAIYO          = @COOLANT_GOODS_KB_N " & vbNewLine _
              & "   LEFT OUTER JOIN $LM_MST$..M_GOODS AS MG         " & vbNewLine _
              & "   ON MG.GOODS_CD_NRS = CONTTRACK.GOODS_CD_NRS   " & vbNewLine _
              & "   AND MG.NRS_BR_CD = CONTTRACK.NRS_BR_CD        " & vbNewLine _
              & " LEFT JOIN $LM_MST$..Z_KBN Z10                    " & vbNewLine _
              & " ON                                               " & vbNewLine _
              & "     Z10.KBN_GROUP_CD       = 'H023'              " & vbNewLine _
              & " --AND Z10.KBN_CD             = MCD.SET_NAIYO       " & vbNewLine _
              & " AND Z10.KBN_CD             = CASE WHEN MG.SEARCH_KEY_1 <> '' THEN MG.SEARCH_KEY_1 ELSE   MCD.SET_NAIYO  END" & vbNewLine _
              & " AND Z10.KBN_CD             = @COOLANT_GOODS_KB_C " & vbNewLine _
              & " --(2013.09.11) 要望番号2095 追加END              " & vbNewLine _
              & "WHERE CONTTRACK.NRS_BR_CD   = @NRS_BR_CD          " & vbNewLine _
              & "  AND CONTTRACK.SYS_DEL_FLG = '0'                 " & vbNewLine _
              & " --(2013.09.11) 要望番号2095 追加START            " & vbNewLine _
              & "  AND Z10.KBN_CD IS NOT NULL                      " & vbNewLine _
              & " --(2013.09.11) 要望番号2095 追加END              " & vbNewLine

#End Region

#End Region

#End Region

#Region "在庫検索データ取得処理"

#Region "在庫検索データ取得処理 SQL"

#Region "在庫検索データ取得処理 SQL SELECT句"

    ''' <summary>
    ''' 在庫検索データ取得処理 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAIKO As String = " SELECT                                                                     " & vbNewLine _
                                             & " CONTTRACK.NRS_BR_CD                            AS NRS_BR_CD                " & vbNewLine _
                                             & ",CONTTRACK.SERIAL_NO                            AS SERIAL_NO                " & vbNewLine _
                                             & ",CONTTRACK.INOUT_DATE                           AS INOUT_DATE               " & vbNewLine _
                                             & ",CONTTRACK.IOZS_KB                              AS IOZS_KB                  " & vbNewLine _
                                             & ",CONTTRACK.INOUTKA_NO_L                         AS INOUTKA_NO_L             " & vbNewLine _
                                             & ",CONTTRACK.INOUTKA_NO_M                         AS INOUTKA_NO_M             " & vbNewLine _
                                             & ",CONTTRACK.INOUTKA_NO_S                         AS INOUTKA_NO_S             " & vbNewLine _
                                             & ",CONTTRACK.STATUS                               AS STATUS                   " & vbNewLine _
                                             & ",CONTTRACK.WH_CD                                AS WH_CD                    " & vbNewLine _
                                             & ",CONTTRACK.CUST_CD_L                            AS CUST_CD_L                " & vbNewLine _
                                             & ",CONTTRACK.CUST_CD_M                            AS CUST_CD_M                " & vbNewLine _
                                             & ",CONTTRACK.REMARK                               AS REMARK                   " & vbNewLine _
                                             & ",CONTTRACK.TOFROM_CD                            AS TOFROM_CD                " & vbNewLine _
                                             & ",CONTTRACK.TOFROM_NM                            AS TOFROM_NM                " & vbNewLine _
                                             & ",CONTTRACK.EXP_FLG                              AS EXP_FLG                  " & vbNewLine _
                                             & ",CONTTRACK.GOODS_CD_NRS                         AS GOODS_CD_NRS             " & vbNewLine _
                                             & ",CONTTRACK.CUST_ORD_NO_DTL                      AS CUST_ORD_NO_DTL          " & vbNewLine _
                                             & ",CONTTRACK.BUYER_ORD_NO_DTL                     AS BUYER_ORD_NO_DTL         " & vbNewLine _
                                             & "--,CONTTRACK.NB                                   AS NB                       " & vbNewLine _
                                             & "--,CONTTRACK.QT                                   AS QT                       " & vbNewLine _
                                             & "--,CONTTRACK.LOT_NO                               AS LOT_NO                   " & vbNewLine _
                                             & ",CONTTRACK.CYLINDER_TYPE                        AS CYLINDER_TYPE            " & vbNewLine _
                                             & ",CONTTRACK.EMPTY_KB                             AS EMPTY_KB                 " & vbNewLine _
                                             & ",CONTTRACK.HAIKI_YN                             AS HAIKI_YN                 " & vbNewLine _
                                             & ",CONTTRACK.SHIP_CD_L                            AS SHIP_CD_L                " & vbNewLine _
                                             & ",CONTTRACK.SHIP_NM_L                            AS SHIP_NM_L                " & vbNewLine _
                                             & ",CONTTRACK.FREE_N01                             AS FREE_N01                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_N02                             AS FREE_N02                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_N03                             AS FREE_N03                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_N04                             AS FREE_N04                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_N05                             AS FREE_N05                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_C01                             AS FREE_C01                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_C02                             AS FREE_C02                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_C03                             AS FREE_C03                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_C04                             AS FREE_C04                 " & vbNewLine _
                                             & ",CONTTRACK.FREE_C05                             AS FREE_C05                 " & vbNewLine _
                                             & ",CONTTRACK.SYS_UPD_DATE                         AS SYS_UPD_DATE             " & vbNewLine _
                                             & ",CONTTRACK.SYS_UPD_TIME                         AS SYS_UPD_TIME             " & vbNewLine _
                                             & ",ISNULL(ALBAS.YOUKI_NO,'')                      AS YOUKI_NO                 " & vbNewLine _
                                             & ",ISNULL(N40.CYLINDER_NO,'')                     AS N40_CYLINDER_NO          " & vbNewLine _
                                             & ",ISNULL(TEIKEN.NEXT_TEST_DATE,'')               AS NEXT_TEST_DATE           " & vbNewLine _
                                             & ",ISNULL(TEIKEN.PROD_DATE,'')                    AS PROD_DATE ----点検開始日ADD 2019/10/29 006786 " & vbNewLine _
                                             & ",DATEDIFF(DAY,CONTTRACK.INOUT_DATE,@SYSDATE)    AS KEIKA_DATE1              " & vbNewLine _
                                             & ",CASE CONTTRACK.IOZS_KB WHEN Z3.KBN_NM2 THEN                                " & vbNewLine _
                                             & "    'IN'                                                                    " & vbNewLine _
                                             & " ELSE                                                                       " & vbNewLine _
                                             & "    'OUT'                                                                   " & vbNewLine _
                                             & " END                                            AS IOZS_KBNM                " & vbNewLine _
                                             & ",CASE CONTTRACK.HAIKI_YN WHEN '1' THEN                                      " & vbNewLine _
                                             & "    '廃棄'                                                                  " & vbNewLine _
                                             & " ELSE                                                                       " & vbNewLine _
                                             & "    ''                                                                      " & vbNewLine _
                                             & " END                                            AS HAIKI_YNNM               " & vbNewLine _
                                             & ",CASE WHEN Z4.KBN_NM2 IS NOT NULL                                           " & vbNewLine _
                                             & "       AND Z4.KBN_NM2 = '00' THEN                                           " & vbNewLine _
                                             & "      CONTTRACK.SHIP_NM_L                                                   " & vbNewLine _
                                             & "      WHEN Z5.KBN_NM2 IS NOT NULL                                           " & vbNewLine _
                                             & "       AND Z5.KBN_NM2 = '00' THEN                                           " & vbNewLine _
                                             & "      CONTTRACK.SHIP_NM_L                                                   " & vbNewLine _
                                             & "      WHEN Z4.KBN_NM2 IS NOT NULL                                           " & vbNewLine _
                                             & "       AND Z4.KBN_NM2 = '01' THEN                                           " & vbNewLine _
                                             & "      Z4.KBN_NM1                                                            " & vbNewLine _
                                             & "      WHEN Z5.KBN_NM2 IS NOT NULL                                           " & vbNewLine _
                                             & "       AND Z5.KBN_NM2 = '01' THEN                                           " & vbNewLine _
                                             & "      Z5.KBN_NM1                                                            " & vbNewLine _
                                             & " --ADD 2019/02/06 依頼番号 : 004485                                         " & vbNewLine _
                                             & "      WHEN Z11.KBN_NM2 IS NOT NULL  then                                    " & vbNewLine _
                                             & "           Z11.KBN_NM6                                                      " & vbNewLine _
                                             & " ELSE                                                                       " & vbNewLine _
                                             & "    ''                                                                      " & vbNewLine _
                                             & " END                                            AS SEIQTO                   " & vbNewLine _
                                             & ",CASE WHEN CONTTRACK.INOUT_DATE < @CHIENSTART_DATE THEN                     " & vbNewLine _
                                             & "      @CHIENSTART_DATE                                                      " & vbNewLine _
                                             & " ELSE                                                                       " & vbNewLine _
                                             & "      CONTTRACK.INOUT_DATE                                                  " & vbNewLine _
                                             & " END                                            AS KEISANSTART_DATE         " & vbNewLine _
                                             & ",CASE WHEN CONTTRACK.INOUT_DATE < @CHIENSTART_DATE THEN                     " & vbNewLine _
                                             & "      DATEDIFF(DAY,@CHIENSTART_DATE,DATEADD(D,1,@KIJUN_DATE))               " & vbNewLine _
                                             & " ELSE                                                                       " & vbNewLine _
                                             & "      DATEDIFF(DAY,CONTTRACK.INOUT_DATE,DATEADD(D,1,@KIJUN_DATE))           " & vbNewLine _
                                             & " END                                            AS KEIKA_DATE2              " & vbNewLine _
                                             & ",CASE WHEN Z6.KBN_NM1 IS NOT NULL THEN                                      " & vbNewLine _
                                             & "      Z7.KBN_NM1                                                            " & vbNewLine _
                                             & " ELSE                                                                       " & vbNewLine _
                                             & "      Z7.KBN_NM2                                                            " & vbNewLine _
                                             & " END                                            AS CHIEN_DATE               " & vbNewLine _
                                             & ",ISNULL(Z8.KBN_CD,'')                           AS CHIEN_KBCD               " & vbNewLine _
                                             & ",CASE WHEN Z8.KBN_CD IS NULL THEN                                           " & vbNewLine _
                                             & "      0                                                                     " & vbNewLine _
                                             & "      WHEN Z8.KBN_CD = '01'                                                 " & vbNewLine _
                                             & "        OR Z8.KBN_CD = '02' THEN                                            " & vbNewLine _
                                             & "      Z9.KBN_NM1                                                            " & vbNewLine _
                                             & "      WHEN Z8.KBN_CD = '03' THEN                                            " & vbNewLine _
                                             & "      Z9.KBN_NM3                                                            " & vbNewLine _
                                             & "      WHEN Z8.KBN_CD = '04' THEN                                            " & vbNewLine _
                                             & "      Z9.KBN_NM4                                                            " & vbNewLine _
                                             & " ELSE                                                                       " & vbNewLine _
                                             & "      0                                                                     " & vbNewLine _
                                             & " END                                            AS CHIEN_MONEY              " & vbNewLine _
                                             & ",CASE WHEN Z8.KBN_CD = '01'                                                 " & vbNewLine _
                                             & "        OR Z8.KBN_CD = '02' THEN                                            " & vbNewLine _
                                             & "      Z9.KBN_NM2                                                            " & vbNewLine _
                                             & " ELSE                                                                       " & vbNewLine _
                                             & "      0                                                                     " & vbNewLine _
                                             & " END                                            AS CHIEN_MONEY2             " & vbNewLine _
                                             & ",CASE WHEN MG.GOODS_CD_CUST = '' THEN CONTTRACK.GOODS_CD_CUST   ELSE  MG.GOODS_CD_CUST END  AS GOODS_CD_CUST --ADD2019/10/31 008262 " & vbNewLine _
                                             & ",CASE WHEN MG.GOODS_NM_1 = '' THEN CONTTRACK.GOODS_NM ELSE MG.GOODS_NM_1 END                AS GOODS_NM      --ADD2019/10/31 008262 " & vbNewLine _
                                             & ",ISNULL(MG.SEARCH_KEY_2,'')                     AS SEARCH_KEY_2             -- ADD 2019/12/10 009849 " & vbNewLine _
                                             & ",ISNULL(INKS.REMARK,'')                         AS REMARK_IN                " & vbNewLine

#End Region

#Region "在庫検索データ取得処理 SQL FROM句"

    ''' <summary>
    ''' 在庫検索データ取得処理 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ZAIKO As String = "FROM                                                                   " & vbNewLine _
                                                  & "$LM_TRN$..I_CONT_TRACK CONTTRACK                                       " & vbNewLine _
                                                  & " INNER JOIN                                                            " & vbNewLine _
                                                  & " (SELECT CONTTRACK2.SERIAL_NO,                                         " & vbNewLine _
                                                  & "         MAX(CONTTRACK2.INOUT_DATE) AS MAX_DATE                        " & vbNewLine _
                                                  & "  FROM                                                                 " & vbNewLine _
                                                  & "  $LM_TRN$..I_CONT_TRACK CONTTRACK2                                    " & vbNewLine _
                                                  & "  WHERE                                                                " & vbNewLine _
                                                  & "  --CONTTRACK2.NRS_BR_CD = @NRS_BR_CD AND                              " & vbNewLine _
                                                  & "      CONTTRACK2.INOUT_DATE <= @KIJUN_DATE                             " & vbNewLine _
                                                  & "  --AND CONTTRACK2.HAIKI_YN <> '1'                                     " & vbNewLine _
                                                  & "  AND CONTTRACK2.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                                  & "  GROUP BY                                                             " & vbNewLine _
                                                  & "  CONTTRACK2.SERIAL_NO) AS SUB_CONT_TRACK                              " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " SUB_CONT_TRACK.MAX_DATE = CONTTRACK.INOUT_DATE                        " & vbNewLine _
                                                  & " AND SUB_CONT_TRACK.SERIAL_NO = CONTTRACK.SERIAL_NO                    " & vbNewLine _
                                                  & " LEFT JOIN $LM_TRN$..I_HONEY_ALBAS_CHG ALBAS                           " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " ALBAS.NRS_BR_CD = CONTTRACK.NRS_BR_CD                                 " & vbNewLine _
                                                  & " AND ALBAS.LABEL_NO = SUBSTRING(CONTTRACK.SERIAL_NO,1,3)               " & vbNewLine _
                                                  & " AND ALBAS.SYS_DEL_FLG= '0'                                            " & vbNewLine _
                                                  & " LEFT JOIN $LM_TRN$..I_HON_TEIKEN TEIKEN                               " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " TEIKEN.NRS_BR_CD = CONTTRACK.NRS_BR_CD                                " & vbNewLine _
                                                  & " AND TEIKEN.SERIAL_NO = CONTTRACK.SERIAL_NO                            " & vbNewLine _
                                                  & " AND TEIKEN.SYS_DEL_FLG= '0'                                           " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN Z1                                          " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z1.KBN_GROUP_CD = 'G009'                                              " & vbNewLine _
                                                  & " AND Z1.KBN_CD = '01'                                                  " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN Z2                                          " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z2.KBN_GROUP_CD = 'G009'                                              " & vbNewLine _
                                                  & " AND Z2.KBN_CD = '02'                                                  " & vbNewLine _
                                                  & " LEFT JOIN LM_MST..Z_KBN Z3                                            " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z3.KBN_GROUP_CD = 'M004'                                              " & vbNewLine _
                                                  & " AND Z3.KBN_CD = '10'                                                  " & vbNewLine _
                                                  & " LEFT JOIN LM_MST..Z_KBN Z4                                            " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z4.KBN_GROUP_CD = 'S083'                                              " & vbNewLine _
                                                  & "-- AND (  Z4.KBN_NM1 LIKE '%' + CONTTRACK.SHIP_NM_L + '%'              " & vbNewLine _
                                                  & "--     OR Z4.KBN_NM1 LIKE '%' + CONTTRACK.TOFROM_NM + '%')             " & vbNewLine _
                                                  & " AND CONTTRACK.SHIP_NM_L LIKE '%' + Z4.KBN_NM1 + '%'                   " & vbNewLine _
                                                  & " LEFT JOIN LM_MST..Z_KBN Z5                                            " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z5.KBN_GROUP_CD = 'S083'                                              " & vbNewLine _
                                                  & "-- AND (  Z4.KBN_NM1 LIKE '%' + CONTTRACK.SHIP_NM_L + '%'              " & vbNewLine _
                                                  & "--     OR Z4.KBN_NM1 LIKE '%' + CONTTRACK.TOFROM_NM + '%')             " & vbNewLine _
                                                  & " AND CONTTRACK.TOFROM_NM LIKE '%' + Z5.KBN_NM1 + '%'                   " & vbNewLine _
                                                  & "--ADD 2019/02/06 依頼番号 : 004485                                     " & vbNewLine _
                                                  & " LEFT JOIN LM_MST..Z_KBN Z11                                           " & vbNewLine _
                                                  & "   ON                                                                  " & vbNewLine _
                                                  & "       Z11.KBN_GROUP_CD = 'S083'                                       " & vbNewLine _
                                                  & "   AND Z11.KBN_NM2 = '99'                                              " & vbNewLine _
                                                  & "   AND CONTTRACK.TOFROM_NM LIKE '%' + Z11.KBN_NM5 + '%'                " & vbNewLine _
                                                  & " LEFT JOIN LM_MST..Z_KBN Z6                                            " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z6.KBN_GROUP_CD = 'C015'                                              " & vbNewLine _
                                                  & " AND Z6.KBN_NM1 = CONTTRACK.CYLINDER_TYPE                              " & vbNewLine _
                                                  & " LEFT JOIN LM_MST..Z_KBN Z7                                            " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z7.KBN_GROUP_CD = 'C016'                                              " & vbNewLine _
                                                  & "-- AND Z6.KBN_NM1 = CONTTRACK.CYLINDER_TYPE                            " & vbNewLine _
                                                  & " LEFT JOIN LM_MST..Z_KBN Z8                                            " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z8.KBN_GROUP_CD = 'C017'                                              " & vbNewLine _
                                                  & " AND Z8.KBN_NM1 = CONTTRACK.CYLINDER_TYPE                              " & vbNewLine _
                                                  & " LEFT JOIN LM_MST..Z_KBN Z9                                            " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & " Z9.KBN_GROUP_CD = 'C018'                                              " & vbNewLine _
                                                  & " --(2013.08.15) 要望番号2095 追加START                                 " & vbNewLine _
                                                  & "   LEFT OUTER JOIN $LM_MST$..M_GOODS AS MG                             " & vbNewLine _
                                                  & "   ON MG.GOODS_CD_NRS = CONTTRACK.GOODS_CD_NRS                         " & vbNewLine _
                                                  & "   AND MG.NRS_BR_CD = CONTTRACK.NRS_BR_CD                              " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & "     MCD.NRS_BR_CD = CONTTRACK.NRS_BR_CD                               " & vbNewLine _
                                                  & " AND MCD.CUST_CD   = CONTTRACK.CUST_CD_L                               " & vbNewLine _
                                                  & " AND MCD.SUB_KB    = '44'                                              " & vbNewLine _
                                                  & " AND MCD.SET_NAIYO = @COOLANT_GOODS_KB_N                               " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN Z10                                         " & vbNewLine _
                                                  & " ON                                                                    " & vbNewLine _
                                                  & "     Z10.KBN_GROUP_CD = 'H023'                                         " & vbNewLine _
                                                  & " --AND Z10.KBN_CD   = MCD.SET_NAIYO                                      " & vbNewLine _
                                                  & " AND Z10.KBN_CD    = CASE WHEN MG.SEARCH_KEY_1 <> '' THEN MG.SEARCH_KEY_1 ELSE   MCD.SET_NAIYO  END" & vbNewLine _
                                                  & " AND Z10.KBN_CD   = @COOLANT_GOODS_KB_C                                " & vbNewLine _
                                                  & " --(2013.08.15) 要望番号2095 追加END                                   " & vbNewLine _
                                                  & " LEFT JOIN $LM_TRN$..I_HONEY_N40_CHG  N40                              " & vbNewLine _
                                                  & "   ON N40.NRS_BR_CD = CONTTRACK.NRS_BR_CD                              " & vbNewLine _
                                                  & "  AND N40.SERIAL_NO = CONTTRACK.SERIAL_NO                              " & vbNewLine _
                                                  & "  AND N40.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                                  & " LEFT JOIN $LM_TRN$..B_INKA_S  INKS                                    " & vbNewLine _
                                                  & "   ON INKS.NRS_BR_CD = CONTTRACK.NRS_BR_CD                             " & vbNewLine _
                                                  & "  AND INKS.INKA_NO_L = CONTTRACK.INOUTKA_NO_L                          " & vbNewLine _
                                                  & "  AND INKS.INKA_NO_M = CONTTRACK.INOUTKA_NO_M                          " & vbNewLine _
                                                  & "  AND INKS.INKA_NO_S = CONTTRACK.INOUTKA_NO_S                          " & vbNewLine _
                                                  & "  AND INKS.SYS_DEL_FLG = '0'                                           " & vbNewLine

#End Region

#Region "在庫検索データ取得処理 SQL WHERE句"

    ''' <summary>
    ''' 在庫検索データ取得処理 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_ZAIKO As String = "WHERE CONTTRACK.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                                   & "  AND CONTTRACK.HAIKI_YN <> '1'                                      " & vbNewLine _
                                                   & "  AND CONTTRACK.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                                   & "  AND CONTTRACK.INOUT_DATE <= @KIJUN_DATE                            " & vbNewLine _
                                                   & "  --(2013.08.15) 要望番号2095 追加START                              " & vbNewLine _
                                                   & "  AND Z10.KBN_CD IS NOT NULL                                         " & vbNewLine _
                                                   & "  --(2013.08.15) 要望番号2095 追加END                                " & vbNewLine _
                                                   & "AND (CONTTRACK.GOODS_CD_CUST LIKE CASE WHEN MCD.SET_NAIYO_2 = '1'    " & vbNewLine _
                                                   & "                                 THEN Z1.KBN_NM1 + '%' ELSE '%%' END " & vbNewLine _
                                                   & "OR CONTTRACK.GOODS_CD_CUST LIKE CASE WHEN MCD.SET_NAIYO_2 = '1'      " & vbNewLine _
                                                   & "                                 THEN Z2.KBN_NM1 + '%' ELSE '%%' END)" & vbNewLine

#End Region

#Region "在庫検索データ取得処理 SQL GROUP BY句"

    ''' <summary>
    ''' 在庫検索データ取得処理 SQL GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_ZAIKO As String = "GROUP BY                                                              " & vbNewLine _
                                                   & " CONTTRACK.NRS_BR_CD                                                  " & vbNewLine _
                                                   & ",CONTTRACK.SERIAL_NO                                                  " & vbNewLine _
                                                   & ",CONTTRACK.INOUT_DATE                                                 " & vbNewLine _
                                                   & ",CONTTRACK.IOZS_KB                                                    " & vbNewLine _
                                                   & ",CONTTRACK.INOUTKA_NO_L                                               " & vbNewLine _
                                                   & ",CONTTRACK.INOUTKA_NO_M                                               " & vbNewLine _
                                                   & ",CONTTRACK.INOUTKA_NO_S                                               " & vbNewLine _
                                                   & ",CONTTRACK.STATUS                                                     " & vbNewLine _
                                                   & ",CONTTRACK.WH_CD                                                      " & vbNewLine _
                                                   & ",CONTTRACK.CUST_CD_L                                                  " & vbNewLine _
                                                   & ",CONTTRACK.CUST_CD_M                                                  " & vbNewLine _
                                                   & ",CONTTRACK.REMARK                                                     " & vbNewLine _
                                                   & ",CONTTRACK.TOFROM_CD                                                  " & vbNewLine _
                                                   & ",CONTTRACK.TOFROM_NM                                                  " & vbNewLine _
                                                   & ",CONTTRACK.EXP_FLG                                                    " & vbNewLine _
                                                   & ",CONTTRACK.GOODS_CD_NRS                                               " & vbNewLine _
                                                   & ",CONTTRACK.CUST_ORD_NO_DTL                                            " & vbNewLine _
                                                   & ",CONTTRACK.BUYER_ORD_NO_DTL                                           " & vbNewLine _
                                                   & "--,CONTTRACK.NB                                                       " & vbNewLine _
                                                   & "--,CONTTRACK.QT                                                       " & vbNewLine _
                                                   & "--,CONTTRACK.LOT_NO                                                   " & vbNewLine _
                                                   & ",CONTTRACK.CYLINDER_TYPE                                              " & vbNewLine _
                                                   & ",CONTTRACK.EMPTY_KB                                                   " & vbNewLine _
                                                   & ",CONTTRACK.HAIKI_YN                                                   " & vbNewLine _
                                                   & ",CONTTRACK.SHIP_CD_L                                                  " & vbNewLine _
                                                   & ",CONTTRACK.SHIP_NM_L                                                  " & vbNewLine _
                                                   & ",CONTTRACK.FREE_N01                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_N02                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_N03                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_N04                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_N05                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_C01                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_C02                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_C03                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_C04                                                   " & vbNewLine _
                                                   & ",CONTTRACK.FREE_C05                                                   " & vbNewLine _
                                                   & ",CONTTRACK.SYS_UPD_DATE                                               " & vbNewLine _
                                                   & ",CONTTRACK.SYS_UPD_TIME                                               " & vbNewLine _
                                                   & ",ISNULL(ALBAS.YOUKI_NO,'')                                            " & vbNewLine _
                                                   & ",ISNULL(N40.CYLINDER_NO,'')                                           " & vbNewLine _
                                                   & ",ISNULL(TEIKEN.NEXT_TEST_DATE,'')                                     " & vbNewLine _
                                                   & ",Z3.KBN_NM2                                                           " & vbNewLine _
                                                   & ",Z4.KBN_NM1                                                           " & vbNewLine _
                                                   & ",Z4.KBN_NM2                                                           " & vbNewLine _
                                                   & ",Z5.KBN_NM1                                                           " & vbNewLine _
                                                   & ",Z5.KBN_NM2                                                           " & vbNewLine _
                                                   & ",Z6.KBN_NM1                                                           " & vbNewLine _
                                                   & ",Z7.KBN_NM1                                                           " & vbNewLine _
                                                   & ",Z7.KBN_NM2                                                           " & vbNewLine _
                                                   & ",Z8.KBN_CD                                                            " & vbNewLine _
                                                   & ",Z9.KBN_NM1                                                           " & vbNewLine _
                                                   & ",Z9.KBN_NM2                                                           " & vbNewLine _
                                                   & ",Z9.KBN_NM3                                                           " & vbNewLine _
                                                   & ",Z9.KBN_NM4                                                           " & vbNewLine _
                                                   & ",Z11.KBN_NM2          --ADD 2019/02/06 依頼番号 : 004485              " & vbNewLine _
                                                   & ",Z11.KBN_NM6          --ADD 2019/02/06 依頼番号 : 004485              " & vbNewLine _
                                                   & ",TEIKEN.PROD_DATE     --ADD 2019/10/28 依頼番号 : 006786              " & vbNewLine _
                                                   & ",MG.GOODS_CD_CUST     --ADD 2019/10/31 依頼番号 : 008262              " & vbNewLine _
                                                   & ",MG.GOODS_NM_1        --ADD 2019/10/31 依頼番号 : 008262              " & vbNewLine _
                                                   & ",CONTTRACK.GOODS_CD_CUST  --ADD 2019/11/13 依頼番号 : 008262          " & vbNewLine _
                                                   & ",CONTTRACK.GOODS_NM       --ADD 2019/11/13 依頼番号 : 008262          " & vbNewLine _
                                                   & ",MG.SEARCH_KEY_2      --ADD 2019/12/10 依頼番号 : 009849              " & vbNewLine _
                                                   & ",INKS.REMARK                                                          " & vbNewLine

#End Region

#Region "在庫検索データ取得処理 SQL ORDER BY句"

    ''' <summary>
    ''' 在庫検索データ取得処理 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_ZAIKO As String = "ORDER BY                                                              " & vbNewLine _
                                                   & " CONTTRACK.SERIAL_NO                                                  " & vbNewLine _
                                                   & ",CONTTRACK.IOZS_KB DESC                                               " & vbNewLine

#End Region

#End Region

#End Region

#Region "履歴・廃棄済検索データ取得処理"

#Region "履歴・廃棄済検索データ取得処理 SQL SELECT句"

    ''' <summary>
    ''' 履歴・廃棄済検索データ取得処理 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_RIREKI_HAIKI As String = " SELECT                                                            " & vbNewLine _
                                              & "       CONTTRACK.NRS_BR_CD                   AS NRS_BR_CD                " & vbNewLine _
                                              & "      ,CONTTRACK.SERIAL_NO                   AS SERIAL_NO                " & vbNewLine _
                                              & "      ,CONTTRACK.INOUT_DATE                  AS INOUT_DATE               " & vbNewLine _
                                              & "      ,CONTTRACK.IOZS_KB                     AS IOZS_KB                  " & vbNewLine _
                                              & "      ,CONTTRACK.INOUTKA_NO_L                AS INOUTKA_NO_L             " & vbNewLine _
                                              & "      ,CONTTRACK.INOUTKA_NO_M                AS INOUTKA_NO_M             " & vbNewLine _
                                              & "      ,CONTTRACK.INOUTKA_NO_S                AS INOUTKA_NO_S             " & vbNewLine _
                                              & "      ,CONTTRACK.STATUS                      AS STATUS                   " & vbNewLine _
                                              & "      ,CONTTRACK.WH_CD                       AS WH_CD                    " & vbNewLine _
                                              & "      ,CONTTRACK.CUST_CD_L                   AS CUST_CD_L                " & vbNewLine _
                                              & "      ,CONTTRACK.CUST_CD_M                   AS CUST_CD_M                " & vbNewLine _
                                              & "      ,CONTTRACK.REMARK                      AS REMARK                   " & vbNewLine _
                                              & "      ,CONTTRACK.TOFROM_CD                   AS TOFROM_CD                " & vbNewLine _
                                              & "      ,CONTTRACK.TOFROM_NM                   AS TOFROM_NM                " & vbNewLine _
                                              & "      ,CONTTRACK.EXP_FLG                     AS EXP_FLG                  " & vbNewLine _
                                              & "      ,CONTTRACK.GOODS_CD_NRS                AS GOODS_CD_NRS             " & vbNewLine _
                                              & "      ,CONTTRACK.CUST_ORD_NO_DTL             AS CUST_ORD_NO_DTL          " & vbNewLine _
                                              & "      ,CONTTRACK.BUYER_ORD_NO_DTL            AS BUYER_ORD_NO_DTL         " & vbNewLine _
                                              & "      --,CONTTRACK.NB                          AS NB                       " & vbNewLine _
                                              & "      --,CONTTRACK.QT                          AS QT                       " & vbNewLine _
                                              & "      --,CONTTRACK.LOT_NO                      AS LOT_NO                   " & vbNewLine _
                                              & "      ,CONTTRACK.CYLINDER_TYPE               AS CYLINDER_TYPE            " & vbNewLine _
                                              & "      ,CONTTRACK.EMPTY_KB                    AS EMPTY_KB                 " & vbNewLine _
                                              & "      ,CONTTRACK.HAIKI_YN                    AS HAIKI_YN                 " & vbNewLine _
                                              & "      ,CONTTRACK.SHIP_CD_L                   AS SHIP_CD_L                " & vbNewLine _
                                              & "      ,CONTTRACK.SHIP_NM_L                   AS SHIP_NM_L                " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_N01                    AS FREE_N01                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_N02                    AS FREE_N02                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_N03                    AS FREE_N03                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_N04                    AS FREE_N04                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_N05                    AS FREE_N05                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_C01                    AS FREE_C01                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_C02                    AS FREE_C02                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_C03                    AS FREE_C03                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_C04                    AS FREE_C04                 " & vbNewLine _
                                              & "      ,CONTTRACK.FREE_C05                    AS FREE_C05                 " & vbNewLine _
                                              & "      ,CONTTRACK.SYS_UPD_DATE                AS SYS_UPD_DATE             " & vbNewLine _
                                              & "      ,CONTTRACK.SYS_UPD_TIME                AS SYS_UPD_TIME             " & vbNewLine _
                                              & "      ,''                                    AS YOUKI_NO                 " & vbNewLine _
                                              & "      ,''                                    AS N40_CYLINDER_NO          " & vbNewLine _
                                              & "      ,''                                    AS NEXT_TEST_DATE           " & vbNewLine _
                                              & "      ,''                                    AS PROD_DATE ----点検開始日ADD 2019/10/29 006786 " & vbNewLine _
                                              & "      ,''                                    AS GOODS_CD_CUST --ADD 2019/10/31 008262 " & vbNewLine _
                                              & "      ,''                                    AS GOODS_NM      --ADD 2019/10/31 008262 " & vbNewLine _
                                              & "      ,''                                    AS SEARCH_KEY_2  --ADD 2019/12/10 009849 " & vbNewLine _
                                              & "      ,''                                    AS KEIKA_DATE1              " & vbNewLine _
                                              & "      ,''                                    AS IOZS_KBNM                " & vbNewLine _
                                              & "      ,''                                    AS HAIKI_YNNM               " & vbNewLine _
                                              & "      ,''                                    AS SEIQTO                   " & vbNewLine _
                                              & "      ,''                                    AS KEISANSTART_DATE         " & vbNewLine _
                                              & "      ,''                                    AS KEIKA_DATE2              " & vbNewLine _
                                              & "      ,''                                    AS CHIEN_DATE               " & vbNewLine _
                                              & "      ,''                                    AS CHIEN_KBCD               " & vbNewLine _
                                              & "      ,''                                    AS CHIEN_MONEY              " & vbNewLine _
                                              & "      ,''                                    AS CHIEN_MONEY2             " & vbNewLine _
                                              & "      ,''                                    AS REMARK_IN                 " & vbNewLine _
                                              & " FROM $LM_TRN$..I_CONT_TRACK CONTTRACK                                   " & vbNewLine _
                                              & " --(2013.08.15) 要望番号2095 追加START                                   " & vbNewLine _
                                              & " LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                  " & vbNewLine _
                                              & " ON                                                                      " & vbNewLine _
                                              & "     MCD.NRS_BR_CD = CONTTRACK.NRS_BR_CD                                 " & vbNewLine _
                                              & " AND MCD.CUST_CD   = CONTTRACK.CUST_CD_L                                 " & vbNewLine _
                                              & " AND MCD.SUB_KB    = '44'                                                " & vbNewLine _
                                              & " AND MCD.SET_NAIYO = @COOLANT_GOODS_KB_N                                 " & vbNewLine _
                                              & "   LEFT OUTER JOIN $LM_MST$..M_GOODS AS MG                              " & vbNewLine _
                                              & "   ON MG.GOODS_CD_NRS = CONTTRACK.GOODS_CD_NRS                          " & vbNewLine _
                                              & "   AND MG.NRS_BR_CD = CONTTRACK.NRS_BR_CD                               " & vbNewLine _
                                              & " LEFT JOIN $LM_MST$..Z_KBN GSKBN                                         " & vbNewLine _
                                              & " ON                                                                      " & vbNewLine _
                                              & "     GSKBN.KBN_GROUP_CD = 'H023'                                         " & vbNewLine _
                                              & " AND GSKBN.KBN_CD         = CASE WHEN MG.SEARCH_KEY_1 <> '' THEN MG.SEARCH_KEY_1 ELSE   MCD.SET_NAIYO  END" & vbNewLine _
                                              & " --AND GSKBN.KBN_CD   = MCD.SET_NAIYO                                      " & vbNewLine _
                                              & " AND GSKBN.KBN_CD   = @COOLANT_GOODS_KB_C                                " & vbNewLine _
                                              & " --(2013.08.15) 要望番号2095 追加END                                     " & vbNewLine _
                                              & "WHERE CONTTRACK.NRS_BR_CD   = @NRS_BR_CD                                 " & vbNewLine _
                                              & "  AND CONTTRACK.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                              & "  --(2013.08.15) 要望番号2095 追加START                                  " & vbNewLine _
                                              & "  AND GSKBN.KBN_CD IS NOT NULL                                           " & vbNewLine _
                                              & "  --(2013.08.15) 要望番号2095 追加END                                    " & vbNewLine



#End Region

#Region "履歴・廃棄済検索データ取得処理 SQL ORDER BY句"

    ''' <summary>
    ''' 履歴・廃棄済検索データ取得処理 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_RIREKI_HAIKI As String = " ORDER BY                                        " & vbNewLine _
                                                          & "    CONTTRACK.SERIAL_NO                          " & vbNewLine _
                                                          & "   ,CONTTRACK.INOUT_DATE DESC                    " & vbNewLine _
                                                          & "   ,CONTTRACK.IOZS_KB                            " & vbNewLine


#End Region

#End Region

#Region "廃棄・廃棄解除"

    ''' <summary>
    ''' 廃棄・廃棄解除処理時のUPDATE文（I_CONT_TRACK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HAIKI As String = " UPDATE $LM_TRN$..I_CONT_TRACK SET        " & vbNewLine _
                                             & "      HAIKI_YN         = @HAIKI_YN        " & vbNewLine _
                                             & "     ,SYS_UPD_DATE     = @SYS_UPD_DATE    " & vbNewLine _
                                             & "     ,SYS_UPD_TIME     = @SYS_UPD_TIME    " & vbNewLine _
                                             & "     ,SYS_UPD_PGID     = @SYS_UPD_PGID    " & vbNewLine _
                                             & "     ,SYS_UPD_USER     = @SYS_UPD_USER    " & vbNewLine _
                                             & "WHERE NRS_BR_CD        = @NRS_BR_CD       " & vbNewLine _
                                             & "  AND SERIAL_NO        = @SERIAL_NO       " & vbNewLine _
                                             & "  AND INOUT_DATE       = @INOUT_DATE      " & vbNewLine _
                                             & "  AND IOZS_KB          = @IOZS_KB         " & vbNewLine _
                                             & "  AND INOUTKA_NO_L     = @INOUTKA_NO_L    " & vbNewLine _
                                             & "  AND INOUTKA_NO_M     = @INOUTKA_NO_M    " & vbNewLine _
                                             & "  AND INOUTKA_NO_S     = @INOUTKA_NO_S    " & vbNewLine _
                                             & "  AND SYS_UPD_DATE     = @GUI_SYS_UPD_DATE" & vbNewLine _
                                             & "  AND SYS_UPD_TIME     = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "保存"

    ''' <summary>
    ''' 保存処理時のUPDATE文（I_CONT_TRACK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HOZON As String = " UPDATE $LM_TRN$..I_CONT_TRACK SET        " & vbNewLine _
                                             & "      SHIP_CD_L        = @SHIP_CD_L       " & vbNewLine _
                                             & "     ,SHIP_NM_L        = @SHIP_NM_L       " & vbNewLine _
                                             & "     ,FREE_N01         = '1'              " & vbNewLine _
                                             & "     ,SYS_UPD_DATE     = @SYS_UPD_DATE    " & vbNewLine _
                                             & "     ,SYS_UPD_TIME     = @SYS_UPD_TIME    " & vbNewLine _
                                             & "     ,SYS_UPD_PGID     = @SYS_UPD_PGID    " & vbNewLine _
                                             & "     ,SYS_UPD_USER     = @SYS_UPD_USER    " & vbNewLine _
                                             & "WHERE NRS_BR_CD        = @NRS_BR_CD       " & vbNewLine _
                                             & "  AND SERIAL_NO        = @SERIAL_NO       " & vbNewLine _
                                             & "  AND INOUT_DATE       = @INOUT_DATE      " & vbNewLine _
                                             & "  AND IOZS_KB          = @IOZS_KB         " & vbNewLine _
                                             & "  AND INOUTKA_NO_L     = @INOUTKA_NO_L    " & vbNewLine _
                                             & "  AND INOUTKA_NO_M     = @INOUTKA_NO_M    " & vbNewLine _
                                             & "  AND INOUTKA_NO_S     = @INOUTKA_NO_S    " & vbNewLine _
                                             & "  AND SYS_UPD_DATE     = @GUI_SYS_UPD_DATE" & vbNewLine _
                                             & "  AND SYS_UPD_TIME     = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "N40コード取込"

    ''' <summary>
    ''' ハネウェルN40変換マスタ更新（Merge文）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MERGE_I_HONEY_N40_CHG As String =
          "MERGE INTO                                                " & vbNewLine _
        & "    $LM_TRN$..I_HONEY_N40_CHG                             " & vbNewLine _
        & "USING                                                     " & vbNewLine _
        & "    (SELECT                                               " & vbNewLine _
        & "          @NRS_BR_CD    AS NRS_BR_CD                      " & vbNewLine _
        & "         ,@SERIAL_NO    AS SERIAL_NO                      " & vbNewLine _
        & "         ,@CYLINDER_NO  AS CYLINDER_NO                    " & vbNewLine _
        & "         ,@SYS_ENT_DATE AS SYS_ENT_DATE                   " & vbNewLine _
        & "         ,@SYS_ENT_TIME AS SYS_ENT_TIME                   " & vbNewLine _
        & "         ,@SYS_ENT_PGID AS SYS_ENT_PGID                   " & vbNewLine _
        & "         ,@SYS_ENT_USER AS SYS_ENT_USER                   " & vbNewLine _
        & "         ,@SYS_UPD_DATE AS SYS_UPD_DATE                   " & vbNewLine _
        & "         ,@SYS_UPD_TIME AS SYS_UPD_TIME                   " & vbNewLine _
        & "         ,@SYS_UPD_PGID AS SYS_UPD_PGID                   " & vbNewLine _
        & "         ,@SYS_UPD_USER AS SYS_UPD_USER                   " & vbNewLine _
        & "         ,@SYS_DEL_FLG  AS SYS_DEL_FLG                    " & vbNewLine _
        & "    ) IN_DATA                                             " & vbNewLine _
        & "ON                                                        " & vbNewLine _
        & "    (                                                     " & vbNewLine _
        & "         I_HONEY_N40_CHG.NRS_BR_CD = IN_DATA.NRS_BR_CD    " & vbNewLine _
        & "     AND I_HONEY_N40_CHG.SERIAL_NO = IN_DATA.SERIAL_NO    " & vbNewLine _
        & "    )                                                     " & vbNewLine _
        & "WHEN MATCHED THEN                                         " & vbNewLine _
        & "    UPDATE                                                " & vbNewLine _
        & "       SET CYLINDER_NO  = IN_DATA.CYLINDER_NO             " & vbNewLine _
        & "          ,SYS_UPD_DATE = IN_DATA.SYS_UPD_DATE            " & vbNewLine _
        & "          ,SYS_UPD_TIME = IN_DATA.SYS_UPD_TIME            " & vbNewLine _
        & "          ,SYS_UPD_PGID = IN_DATA.SYS_UPD_PGID            " & vbNewLine _
        & "          ,SYS_UPD_USER = IN_DATA.SYS_UPD_USER            " & vbNewLine _
        & "          ,SYS_DEL_FLG  = IN_DATA.SYS_DEL_FLG             " & vbNewLine _
        & "WHEN NOT MATCHED THEN                                     " & vbNewLine _
        & "    INSERT (                                              " & vbNewLine _
        & "         NRS_BR_CD                                        " & vbNewLine _
        & "        ,SERIAL_NO                                        " & vbNewLine _
        & "        ,CYLINDER_NO                                      " & vbNewLine _
        & "        ,SYS_ENT_DATE                                     " & vbNewLine _
        & "        ,SYS_ENT_TIME                                     " & vbNewLine _
        & "        ,SYS_ENT_PGID                                     " & vbNewLine _
        & "        ,SYS_ENT_USER                                     " & vbNewLine _
        & "        ,SYS_UPD_DATE                                     " & vbNewLine _
        & "        ,SYS_UPD_TIME                                     " & vbNewLine _
        & "        ,SYS_UPD_PGID                                     " & vbNewLine _
        & "        ,SYS_UPD_USER                                     " & vbNewLine _
        & "        ,SYS_DEL_FLG                                      " & vbNewLine _
        & "    ) VALUES (                                            " & vbNewLine _
        & "         IN_DATA.NRS_BR_CD                                " & vbNewLine _
        & "        ,IN_DATA.SERIAL_NO                                " & vbNewLine _
        & "        ,IN_DATA.CYLINDER_NO                              " & vbNewLine _
        & "        ,IN_DATA.SYS_ENT_DATE                             " & vbNewLine _
        & "        ,IN_DATA.SYS_ENT_TIME                             " & vbNewLine _
        & "        ,IN_DATA.SYS_ENT_PGID                             " & vbNewLine _
        & "        ,IN_DATA.SYS_ENT_USER                             " & vbNewLine _
        & "        ,IN_DATA.SYS_UPD_DATE                             " & vbNewLine _
        & "        ,IN_DATA.SYS_UPD_TIME                             " & vbNewLine _
        & "        ,IN_DATA.SYS_UPD_PGID                             " & vbNewLine _
        & "        ,IN_DATA.SYS_UPD_USER                             " & vbNewLine _
        & "        ,IN_DATA.SYS_DEL_FLG                              " & vbNewLine _
        & "    )                                                     " & vbNewLine _
        & ";                                                         " & vbNewLine


    ''' <summary>
    ''' ハネウェルN40変換マスタ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_I_HONEY_N40_CHG As String =
          "SELECT CYLINDER_NO                  " & vbNewLine _
        & "  FROM $LM_TRN$..I_HONEY_N40_CHG    " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "   AND SERIAL_NO = @SERIAL_NO       " & vbNewLine

    ''' <summary>
    ''' ハネウェルN40変換マスタ更新（Update文）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_I_HONEY_N40_CHG As String =
          "UPDATE $LM_TRN$..I_HONEY_N40_CHG       " & vbNewLine _
        & "   SET CYLINDER_NO  = @CYLINDER_NO     " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
        & "      ,SYS_DEL_FLG  = '0'              " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD          " & vbNewLine _
        & "   AND SERIAL_NO = @SERIAL_NO          " & vbNewLine

    ''' <summary>
    ''' ハネウェルN40変換マスタ登録（Insert文）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_I_HONEY_N40_CHG As String =
          "INSERT INTO $LM_TRN$..I_HONEY_N40_CHG (    " & vbNewLine _
        & "     NRS_BR_CD                             " & vbNewLine _
        & "    ,SERIAL_NO                             " & vbNewLine _
        & "    ,CYLINDER_NO                           " & vbNewLine _
        & "    ,SYS_ENT_DATE                          " & vbNewLine _
        & "    ,SYS_ENT_TIME                          " & vbNewLine _
        & "    ,SYS_ENT_PGID                          " & vbNewLine _
        & "    ,SYS_ENT_USER                          " & vbNewLine _
        & "    ,SYS_UPD_DATE                          " & vbNewLine _
        & "    ,SYS_UPD_TIME                          " & vbNewLine _
        & "    ,SYS_UPD_PGID                          " & vbNewLine _
        & "    ,SYS_UPD_USER                          " & vbNewLine _
        & "    ,SYS_DEL_FLG                           " & vbNewLine _
        & ") VALUES (                                 " & vbNewLine _
        & "     @NRS_BR_CD                            " & vbNewLine _
        & "    ,@SERIAL_NO                            " & vbNewLine _
        & "    ,@CYLINDER_NO                          " & vbNewLine _
        & "    ,@SYS_ENT_DATE                         " & vbNewLine _
        & "    ,@SYS_ENT_TIME                         " & vbNewLine _
        & "    ,@SYS_ENT_PGID                         " & vbNewLine _
        & "    ,@SYS_ENT_USER                         " & vbNewLine _
        & "    ,@SYS_UPD_DATE                         " & vbNewLine _
        & "    ,@SYS_UPD_TIME                         " & vbNewLine _
        & "    ,@SYS_UPD_PGID                         " & vbNewLine _
        & "    ,@SYS_UPD_USER                         " & vbNewLine _
        & "    ,@SYS_DEL_FLG                          " & vbNewLine _
        & ")                                          " & vbNewLine

#End Region 'N40コード取込

#End Region  'Const

#Region "Field"

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

#End Region  'Field

#Region "Method"

#Region "SQLメイン処理"

    ' ********** データ取得処理 **********

#Region "荷主明細データ取得"

    ''' <summary>
    ''' 荷主明細データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustDetailsData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_CUSTDETAILS)       'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_CUSTDETAILS)  'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_WHERE_CUSTDETAILS) 'SQL構築(Where句)

        'パラメータの設定
        Call SetSQLCustDetailsParameter(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectCustDetailsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("CUST_CD_EDA", "CUST_CD_EDA")
        map.Add("CUST_CLASS", "CUST_CLASS")
        map.Add("SUB_KB", "SUB_KB")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")
        map.Add("SET_NAIYO_3", "SET_NAIYO_3")
        map.Add("REMARK", "REMARK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190OUT_CUSTDETAILS")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "削除対象データ取得"

    ''' <summary>
    ''' 削除対象データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_DELDATA)       'SQL構築(Select句)

        'パラメータの設定
        Call SetSQLDelDataParameter(ds)           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータの設定
        sql = SetSQLDelDataParameter2(ds, sql)        '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectDeleteData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190INOUT_DEL_GET")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "入荷データ取得"

    ''' <summary>
    ''' 入荷データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_INKA)       'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_INKA)  'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_WHERE_INKA) 'SQL構築(Where句)

        'パラメータの設定
        Call SetSQLInkaDataParameter(ds)           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータの設定
        sql = SetSQLInkaDataParameter2(ds, sql)   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectInkaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INOUT_DATE", "INOUT_DATE")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")
        map.Add("STATUS", "STATUS")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("REMARK", "REMARK")
        map.Add("TOFROM_CD", "TOFROM_CD")
        map.Add("TOFROM_NM", "TOFROM_NM")
        map.Add("EXP_FLG", "EXP_FLG")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        'map.Add("NB", "NB")
        'map.Add("QT", "QT")
        'map.Add("LOT_NO", "LOT_NO")
        map.Add("CYLINDER_TYPE", "CYLINDER_TYPE")
        map.Add("EMPTY_KB", "EMPTY_KB")
        map.Add("HAIKI_YN", "HAIKI_YN")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190INOUT_INKA_GET")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "出荷データ取得"

    ''' <summary>
    ''' 出荷データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_OUTKA)        'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_OUTKA)   'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_WHERE_OUTKA)  'SQL構築(Where句)

        'パラメータの設定
        Call SetSQLOutkaDataParameter(ds)           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータの設定
        sql = SetSQLOutkaDataParameter2(ds, sql)    '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトのため修正 2013.02.28
        'タイムアウトの設定
        cmd.CommandTimeout = 120
        'タイムアウトのため修正 2013.02.28

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectOutkaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INOUT_DATE", "INOUT_DATE")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")
        map.Add("STATUS", "STATUS")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("REMARK", "REMARK")
        map.Add("TOFROM_CD", "TOFROM_CD")
        map.Add("TOFROM_NM", "TOFROM_NM")
        map.Add("EXP_FLG", "EXP_FLG")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        'map.Add("NB", "NB")
        'map.Add("QT", "QT")
        'map.Add("LOT_NO", "LOT_NO")
        map.Add("CYLINDER_TYPE", "CYLINDER_TYPE")
        map.Add("EMPTY_KB", "EMPTY_KB")
        map.Add("HAIKI_YN", "HAIKI_YN")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190INOUT_OUTKA_GET")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "削除対象データの削除処理"

    ''' <summary>
    ''' 削除対象データの削除処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190INOUT_DEL_GET")

        If inTbl.Rows.Count = 0 Then
            Return ds
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_DATA)      'SQL構築(Delete句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの設定
            Call SetSQLDeleteContTrackParameter(inTbl.Rows(i))           '条件設定

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI190DAC", "DeleteContTrackData", cmd)

            'SQLの発行
            Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

    'タイムアウトのため修正 2013.02.28
    ''' <summary>
    ''' 削除対象データの削除処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_DATA2)     'SQL構築(Delete句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_DELDATA)   'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_JOIN)      'SQL構築(Join句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータの設定
        Call SetSQLDelDataParameter(ds)
        sql = SetSQLDelDataParameter2(ds, sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定
        cmd.CommandTimeout = 120

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "DeleteContTrackData", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
    'タイムアウトのため修正 2013.02.28

#End Region

#Region "入荷対象データの削除処理"

    ''' <summary>
    ''' 入荷対象データの削除処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackInkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190INOUT_INKA_GET")

        If inTbl.Rows.Count = 0 Then
            Return ds
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_INKADATA)      'SQL構築(Delete句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの設定
            Call SetSQLInkaDeleteContTrackParameter(inTbl.Rows(i))           '条件設定

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI190DAC", "DeleteContTrackInkaData", cmd)

            'SQLの発行
            Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

    'タイムアウトのため修正 2013.02.28
    ''' <summary>
    ''' 入荷対象データの削除処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackInkaData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_INKADATA2)     'SQL構築(Delete句)
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_INKA)          'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_FROM_INKA)     'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_INKA_JOIN)     'SQL構築(Join句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータの設定
        Call SetSQLInkaDataParameter(ds)
        sql = Me.SetSQLInkaDataParameter2(ds, sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定
        cmd.CommandTimeout = 120

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "DeleteContTrackInkaData", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
    'タイムアウトのため修正 2013.02.28

#End Region

#Region "出荷対象データの削除処理"

    ''' <summary>
    ''' 出荷対象データの削除処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190INOUT_OUTKA_GET")

        If inTbl.Rows.Count = 0 Then
            Return ds
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_OUTKADATA)      'SQL構築(Delete句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの設定
            Call SetSQLOutkaDeleteContTrackParameter(inTbl.Rows(i))           '条件設定

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI190DAC", "DeleteContTrackOutkaData", cmd)

            'SQLの発行
            Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

    'タイムアウトのため修正 2013.02.28
    ''' <summary>
    ''' 出荷対象データの削除処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackOutkaData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_OUTKADATA2)     'SQL構築(Delete句)
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_OUTKA)          'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_FROM_OUTKA)     'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_WHERE_OUTKA)    'SQL構築(Where句)
        Me._StrSql.Append(LMI190DAC.SQL_DELETE_JOIN)           'SQL構築(Join句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータの設定
        Call SetSQLOutkaDataParameter(ds)
        sql = SetSQLOutkaDataParameter2(ds, sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定
        cmd.CommandTimeout = 120

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "DeleteContTrackOutkaData", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
    'タイムアウトのため修正 2013.02.28

#End Region

#Region "入荷対象データの追加処理"

    ''' <summary>
    '''入荷対象データの追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertContTrackInkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190INOUT_INKA_GET")

        If inTbl.Rows.Count = 0 Then
            Return ds
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_INSERT_CONTTRACK)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetSQLInsertContTrackParameter(inTbl.Rows(i))

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'MyBase.Logger.WriteSQLLog("LMI190DAC", "InsertContTrackInkaData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

    'タイムアウトのため修正 2013.02.28
    ''' <summary>
    '''入荷対象データの追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertContTrackInkaData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_INSERT_CONTTRACK2)         'SQL構築(INSERT句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_INKA2)              'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_SYSTEM)             'SQL構築(System句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_INKA2)         'SQL構築(From句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータの設定
        Call SetSQLInkaDataParameter(ds)
        sql = Me.SetSQLInkaDataParameter2(ds, sql)
        Call Me.SetParamCommonSystemIns()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定
        cmd.CommandTimeout = 120

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'MyBase.Logger.WriteSQLLog("LMI190DAC", "InsertContTrackInkaData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function
    'タイムアウトのため修正 2013.02.28

#End Region

#Region "出荷対象データの追加処理"

    ''' <summary>
    '''出荷対象データの追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertContTrackOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190INOUT_OUTKA_GET")

        If inTbl.Rows.Count = 0 Then
            Return ds
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_INSERT_CONTTRACK)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetSQLInsertContTrackParameter(inTbl.Rows(i))

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'MyBase.Logger.WriteSQLLog("LMI190DAC", "InsertContTrackOutkaData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

    'タイムアウトのため修正 2013.02.28
    ''' <summary>
    '''出荷対象データの追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertContTrackOutkaData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_INSERT_CONTTRACK2)   'SQL構築(INSERT句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_OUTKA)        'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_SYSTEM)       'SQL構築(System句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_OUTKA)   'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_WHERE_OUTKA)  'SQL構築(Where句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'パラメータの設定
        Call SetSQLOutkaDataParameter(ds)
        sql = SetSQLOutkaDataParameter2(ds, sql)
        Call Me.SetParamCommonSystemIns()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定
        cmd.CommandTimeout = 120

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'MyBase.Logger.WriteSQLLog("LMI190DAC", "InsertContTrackOutkaData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function
    'タイムアウトのため修正 2013.02.28

#End Region

#Region "ハネウェル用データ管理取得ログ作成処理"

    ''' <summary>
    '''ハネウェル用データ管理取得ログ作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertLogData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190IN_LOG")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_INSERT_CONTTRACKLOG)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ（個別項目）設定
        Call Me.SetSQLInsertContTrackLogParameter(inTbl.Rows(0))

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'MyBase.Logger.WriteSQLLog("LMI190DAC", "InsertLogData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "シリンダタイプコンボの値を取得"

    ''' <summary>
    ''' シリンダタイプコンボの値を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCylinder(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_CYLINDER)       'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_CYLINDER)  'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_WHERE_CYLINDER) 'SQL構築(Where句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_GROUP_CYLINDER) 'SQL構築(Group句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_ORDER_CYLINDER) 'SQL構築(Order句)

        'パラメータの設定
        Call SetSQLCylinderParameter(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectCylinder", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CYLINDER_TYPE", "CYLINDER_TYPE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "在庫場所コンボの値を取得"

    ''' <summary>
    ''' 在庫場所コンボの値を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectToFromNm(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_TOFROMNM)       'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_TOFROMNM)  'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_WHERE_TOFROMNM) 'SQL構築(Where句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_GROUP_TOFROMNM) 'SQL構築(Group句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_ORDER_TOFROMNM) 'SQL構築(Order句)

        'パラメータの設定
        Call SetSQLToFromNmParameter(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectToFromNm", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("TOFROM_NM", "TOFROM_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190OUT")

        reader.Close()

        Return ds

    End Function

#End Region

    ' ********** 検索処理 **********

#Region "在庫検索データ件数取得処理"

    ''' <summary>
    ''' 在庫検索データ件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountZaiko(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_COUNT)       'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_ZAIKO)  'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_WHERE_ZAIKO) 'SQL構築(Where句)
        Call Me.SetSQLWhereZaiko(inTbl.Rows(0))             'SQL構築(追加EWhere句 & パラメータ設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectKensakuCountZaiko", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "在庫検索データ取得処理"

    ''' <summary>
    ''' 在庫検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuZaikoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_ZAIKO)       'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_ZAIKO)  'SQL構築(From句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_WHERE_ZAIKO) 'SQL構築(Where句)
        Call Me.SetSQLWhereZaiko(inTbl.Rows(0))             'SQL構築(追加EWhere句 & パラメータ設定)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_GROUP_ZAIKO) 'SQL構築(Group句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_ORDER_ZAIKO) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectKensakuZaikoData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = Me.GetLMI190OUTMap()
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "履歴検索データ件数取得処理"

    ''' <summary>
    ''' 履歴検索データ件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountRireki(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_COUNT)              'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_RIREKIHAIKI)   'SQL構築(From句)
        Call Me.SetSQLWhereRirekiHaiki(inTbl.Rows(0))              'SQL構築(追加EWhere句 & パラメータ設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectKensakuCountRireki", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "履歴検索データ取得処理"

    ''' <summary>
    ''' 履歴検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuRirekiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_RIREKI_HAIKI)       'SQL構築(Select句)
        Call Me.SetSQLWhereRirekiHaiki(inTbl.Rows(0))              'SQL構築(追加EWhere句 & パラメータ設定)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_ORDER_RIREKI_HAIKI) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectKensakuRirekiData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = Me.GetLMI190OUTMap()

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "廃棄済検索データ件数取得処理"

    ''' <summary>
    ''' 廃棄済検索データ件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountHaiki(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_COUNT)              'SQL構築(Select句)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_FROM_RIREKIHAIKI)   'SQL構築(From句)
        Me._StrSql.Append(" AND CONTTRACK.HAIKI_YN = '1' ")
        Call Me.SetSQLWhereRirekiHaiki(inTbl.Rows(0))              'SQL構築(追加EWhere句 & パラメータ設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectKensakuCountHaiki", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "廃棄済検索データ取得処理"

    ''' <summary>
    ''' 廃棄済検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuHaikiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_RIREKI_HAIKI)       'SQL構築(Select句)
        Me._StrSql.Append(" AND CONTTRACK.HAIKI_YN = '1' ")
        Call Me.SetSQLWhereRirekiHaiki(inTbl.Rows(0))              'SQL構築(追加EWhere句 & パラメータ設定)
        Me._StrSql.Append(LMI190DAC.SQL_SELECT_ORDER_RIREKI_HAIKI) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "SelectKensakuRirekiData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = Me.GetLMI190OUTMap()

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190OUT")

        reader.Close()

        Return ds

    End Function

#End Region


    ' ********** 廃棄処理 **********

#Region "廃棄処理"

    ''' <summary>
    ''' 廃棄処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHaikiData(ByVal ds As DataSet) As DataSet

        Return Me.UpdateHaikiDataMain(ds, "1")

    End Function

    ''' <summary>
    ''' 廃棄解除処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHaikiKaijoData(ByVal ds As DataSet) As DataSet

        Return Me.UpdateHaikiDataMain(ds, "0")

    End Function

    ''' <summary>
    ''' 廃棄・廃棄解除処理実行
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="haikiYn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateHaikiDataMain(ByVal ds As DataSet, ByVal haikiYn As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        If inTbl.Rows.Count = 0 Then
            Return ds
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_UPDATE_HAIKI)      'SQL構築(Update句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの設定（廃棄区分以外）
            Call Me.SetSQLUpdateContTrackParameter(inTbl.Rows(i))
            Call Me.SetSysdataParameter()

            'パラメータの設定（廃棄区分）
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAIKI_YN", haikiYn, DBDataType.NVARCHAR))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                '排他エラー
                Return ds
            End If

        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "UpdateHaikiDataMain", cmd)

        Return ds

    End Function

#End Region

    ' ********** 保存処理 **********

#Region "保存処理"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateHozonData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        If inTbl.Rows.Count = 0 Then
            Return ds
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI190DAC.SQL_UPDATE_HOZON)      'SQL構築(Update句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの設定（共通部）
            Call Me.SetSQLUpdateContTrackParameter(inTbl.Rows(i))
            Call Me.SetSysdataParameter()

            'パラメータの設定（共通部以外）
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", inTbl.Rows(i).Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", inTbl.Rows(i).Item("SHIP_NM_L").ToString(), DBDataType.NVARCHAR))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                '排他エラー
                Return ds
            End If

        Next

        MyBase.Logger.WriteSQLLog("LMI190DAC", "UpdateHozonData", cmd)

        Return ds

    End Function

#End Region

    ' ********** N40コード取込処理 **********

#Region "N40コード取込処理"

    ''' <summary>
    ''' ハネウェルN40変換マスタ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MergeHoneyN40Chg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190IN_N40_CHG")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI190DAC.SQL_MERGE_I_HONEY_N40_CHG, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ格納変数の初期化
        Me._SqlPrmList = New ArrayList()

        Dim rowCntTotal As Integer = 0
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()
            Me._SqlPrmList.Clear()

            'パラメータの設定
            Call Me.SetSQLHoneyN40ChgParameter(inTbl.Rows(i))
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Dim rowCnt As Integer = MyBase.GetUpdateResult(cmd)
            rowCntTotal += rowCnt

            If rowCnt < 1 Then
                MyBase.SetMessage("E262", {"再度、実行してください。"}) '対象データが他のユーザーによって変更されていたため、処理できませんでした。[%1]
                MyBase.SetResultCount(rowCntTotal)
                Return ds
            End If

        Next

        MyBase.SetResultCount(rowCntTotal)

        Return ds

    End Function


    ''' <summary>
    ''' ハネウェルN40変換マスタ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectHoneyN40Chg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190IN_N40_CHG")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI190DAC.SQL_SELECT_I_HONEY_N40_CHG, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの設定
        With inTbl.Rows(0)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
        End With

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CYLINDER_NO", "CYLINDER_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI190OUT_N40_CHG")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルN40変換マスタ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateHoneyN40Chg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190IN_N40_CHG")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI190DAC.SQL_UPDATE_I_HONEY_N40_CHG, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ格納変数の初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの設定
        Call Me.SetSQLHoneyN40ChgParameter(inTbl.Rows(0))
        Call Me.SetSysdataParameter()

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rowCnt As Integer = MyBase.GetUpdateResult(cmd)

        If rowCnt < 1 Then
            MyBase.SetMessage("E262", {"再度、実行してください。"}) '対象データが他のユーザーによって変更されていたため、処理できませんでした。[%1]
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルN40変換マスタ登録処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertHoneyN40Chg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI190IN_N40_CHG")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI190DAC.SQL_INSERT_I_HONEY_N40_CHG, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ格納変数の初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの設定
        Call Me.SetSQLHoneyN40ChgParameter(inTbl.Rows(0))
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region 'N40コード取込処理

#End Region 'SQLメイン処理

#Region "SQL条件設定"

#Region "SQL条件設定 荷主データ取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLCustDetailsParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            '(2013.08.15) 要望番号2095 追加START 
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.NVARCHAR))
            '(2013.08.15) 要望番号2095 追加END

        End With

    End Sub

#End Region

#Region "SQL条件設定 削除対象データ取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLDelDataParameter(ByVal ds As DataSet)

        Dim inTblRow As DataRow = ds.Tables(TABLE_NM_IN).Rows(0)

        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSQLDelDataParameter2(ByVal ds As DataSet, ByVal sql As String) As String

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        '荷主明細
        Dim max As Integer = ds.Tables("LMI190OUT_CUSTDETAILS").Rows.Count - 1
        Dim custCd As String = String.Empty
        For i As Integer = 0 To max
            If String.IsNullOrEmpty(custCd) = False Then
                custCd = String.Concat(custCd, ", ")
            End If
            'custCd = String.Concat(custCd, ds.Tables("LMI190OUT_CUSTDETAILS").Rows(i).Item("CUST_CD").ToString)
            custCd = String.Concat(custCd, "'", ds.Tables("LMI190OUT_CUSTDETAILS").Rows(i).Item("CUST_CD").ToString, "'")
        Next

        whereStr = String.Empty
        If String.IsNullOrEmpty(custCd) = False Then
            '①入荷削除データ
            whereStr = String.Concat("AND INKAL1.CUST_CD_L IN(", custCd, ") ")
        End If
        sql = sql.Replace("@CUSTDETAILS_INKA1", whereStr)

        whereStr = String.Empty
        If String.IsNullOrEmpty(custCd) = False Then
            '②出荷削除データ
            whereStr = String.Concat("AND OUTKAL1.CUST_CD_L IN(", custCd, ") ")
        End If
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUSTDETAILS_OUTKA1", whereStr, DBDataType.CHAR))
        sql = sql.Replace("@CUSTDETAILS_OUTKA1", whereStr)

        whereStr = String.Empty
        If String.IsNullOrEmpty(custCd) = False Then
            '③入荷 特定期間のデータ
            whereStr = String.Concat("AND INKAL2.CUST_CD_L IN(", custCd, ") ")
        End If
        sql = sql.Replace("@CUSTDETAILS_INKA2", whereStr)

        whereStr = String.Empty
        If String.IsNullOrEmpty(custCd) = False Then
            '④出荷 特定期間のデータ
            whereStr = String.Concat("AND OUTKAL2.CUST_CD_L IN(", custCd, ") ")
        End If
        sql = sql.Replace("@CUSTDETAILS_OUTKA2", whereStr)

        Return sql

    End Function

#End Region

#Region "SQL条件設定 入荷データ取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLInkaDataParameter(ByVal ds As DataSet)

        Dim inTblRow As DataRow = ds.Tables(TABLE_NM_IN).Rows(0)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSQLInkaDataParameter2(ByVal ds As DataSet, ByVal sql As String) As String

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        '荷主明細
        Dim max As Integer = ds.Tables("LMI190OUT_CUSTDETAILS").Rows.Count - 1
        Dim custCd As String = String.Empty
        For i As Integer = 0 To max
            If String.IsNullOrEmpty(custCd) = False Then
                custCd = String.Concat(custCd, ", ")
            End If
            'custCd = String.Concat(custCd, ds.Tables("LMI190OUT_CUSTDETAILS").Rows(i).Item("CUST_CD").ToString)
            custCd = String.Concat(custCd, "'", ds.Tables("LMI190OUT_CUSTDETAILS").Rows(i).Item("CUST_CD").ToString, "'")
        Next

        whereStr = String.Empty
        If String.IsNullOrEmpty(custCd) = False Then
            whereStr = String.Concat("AND INKAL2.CUST_CD_L IN(", custCd, ") ")
        End If
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUSTDETAILS_INKA", whereStr, DBDataType.CHAR))
        sql = sql.Replace("@CUSTDETAILS_INKA", whereStr)

        Return sql

    End Function

#End Region

#Region "SQL条件設定 出荷データ取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLOutkaDataParameter(ByVal ds As DataSet)

        Dim inTblRow As DataRow = ds.Tables(TABLE_NM_IN).Rows(0)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSQLOutkaDataParameter2(ByVal ds As DataSet, ByVal sql As String) As String

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        '荷主明細
        Dim max As Integer = ds.Tables("LMI190OUT_CUSTDETAILS").Rows.Count - 1
        Dim custCd As String = String.Empty
        For i As Integer = 0 To max
            If String.IsNullOrEmpty(custCd) = False Then
                custCd = String.Concat(custCd, ", ")
            End If
            'custCd = String.Concat(custCd, ds.Tables("LMI190OUT_CUSTDETAILS").Rows(i).Item("CUST_CD").ToString)
            custCd = String.Concat(custCd, "'", ds.Tables("LMI190OUT_CUSTDETAILS").Rows(i).Item("CUST_CD").ToString, "'")
        Next

        whereStr = String.Empty
        If String.IsNullOrEmpty(custCd) = False Then
            whereStr = String.Concat("AND OUTKAL.CUST_CD_L IN(", custCd, ") ")
        End If
        sql = sql.Replace("@CUSTDETAILS_OUTKA", whereStr)

        Return sql

    End Function

#End Region

#Region "SQL条件設定 削除対象データ削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLDeleteContTrackParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 入荷対象データ削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLInkaDeleteContTrackParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_DATE", .Item("INOUT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_M", .Item("INOUTKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_S", .Item("INOUTKA_NO_S").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 出荷対象データ削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLOutkaDeleteContTrackParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_DATE", .Item("INOUT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_M", .Item("INOUTKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_S", .Item("INOUTKA_NO_S").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 ハネウェル用容器管理データ追加"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLInsertContTrackParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_DATE", .Item("INOUT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_M", .Item("INOUTKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_S", .Item("INOUTKA_NO_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATUS", .Item("STATUS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOFROM_CD", .Item("TOFROM_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOFROM_NM", .Item("TOFROM_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXP_FLG", .Item("EXP_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB", .Item("NB").ToString(), DBDataType.NUMERIC))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@QT", .Item("QT").ToString(), DBDataType.NUMERIC))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CYLINDER_TYPE", .Item("CYLINDER_TYPE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EMPTY_KB", .Item("EMPTY_KB").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAIKI_YN", .Item("HAIKI_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", .Item("SHIP_NM_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N01", .Item("FREE_N01").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N02", .Item("FREE_N02").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N03", .Item("FREE_N03").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N04", .Item("FREE_N04").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N05", .Item("FREE_N05").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 ハネウェル用容器管理データログ追加"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLInsertContTrackLogParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATUS", .Item("STATUS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@R_CNT", .Item("R_CNT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "SQL条件設定 シリンダタイプコンボの値を取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLCylinderParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 在庫場所コンボの値を取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLToFromNmParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 I_CONT_TRACK更新（廃棄処理・保存処理）"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLUpdateContTrackParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_DATE", .Item("INOUT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_M", .Item("INOUTKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_S", .Item("INOUTKA_NO_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))


        End With

    End Sub

#End Region

#Region "SQL条件設定 N40コード取込処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（ハネウェルN40変換マスタ更新処理）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLHoneyN40ChgParameter(ByVal inTblRow As DataRow)

        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CYLINDER_NO", .Item("CYLINDER_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

    ' ********** 検索処理 **********

#Region "SQL条件設定 在庫検索データ取得処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZaiko(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With inTblRow

            '必須パラメータ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIJUN_DATE", .Item("KIJUN_DATE").ToString(), DBDataType.CHAR))
            '(2013.08.15) 要望番号2095 追加START
            '冷媒商品
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB_N", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB_C", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.CHAR))
            '(2013.08.15) 要望番号2095 追加END

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.SERIAL_NO LIKE @SERIAL_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'シリンダタイプ
            whereStr = .Item("CYLINDER_TYPE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.CYLINDER_TYPE = @CYLINDER_TYPE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CYLINDER_TYPE", whereStr, DBDataType.NVARCHAR))
            End If

            '在庫場所
            whereStr = .Item("TOFROM_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.TOFROM_NM LIKE @TOFROM_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOFROM_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '経過日数
            whereStr = .Item("KEIKA_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND DATEDIFF(day,CONTTRACK.INOUT_DATE,getdate()) >= @KEIKA_DATE")
                Me._StrSql.Append(" AND DATEDIFF(day,CONTTRACK.INOUT_DATE,getdate()) >= CONVERT(INT,CASE WHEN LEN(@KEIKA_DATE) < 4 THEN @KEIKA_DATE ELSE SUBSTRING(@KEIKA_DATE,1,1) + SUBSTRING(@KEIKA_DATE,3,3) END)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEIKA_DATE", whereStr, DBDataType.CHAR))
            End If

            '空・実入り
            whereStr = .Item("EMPTY_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.EMPTY_KB = @EMPTY_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EMPTY_KB", whereStr, DBDataType.NVARCHAR))
            End If

            '入出在その他区分
            whereStr = .Item("IOZS_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.IOZS_KB = @IOZS_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", whereStr, DBDataType.CHAR))
            End If

            '遅延金制度開始日
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHIENSTART_DATE", .Item("CHIENSTART_DATE").ToString(), DBDataType.CHAR))

            'SYSDATE
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYSDATE", .Item("SYSDATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 履歴・廃棄済検索データ取得処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereRirekiHaiki(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With inTblRow

            '必須パラメータ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '(2013.08.15) 要望番号2095 追加START
            '冷媒商品
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB_N", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB_C", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.CHAR))
            '(2013.08.15) 要望番号2095 追加END

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.SERIAL_NO LIKE @SERIAL_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'シリンダタイプ
            whereStr = .Item("CYLINDER_TYPE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.CYLINDER_TYPE = @CYLINDER_TYPE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CYLINDER_TYPE", whereStr, DBDataType.NVARCHAR))
            End If

            '在庫場所
            whereStr = .Item("TOFROM_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.TOFROM_NM LIKE @TOFROM_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOFROM_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '移動日From
            whereStr = .Item("IDO_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND @IDO_DATE_FROM <= CONTTRACK.INOUT_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IDO_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            '移動日To
            whereStr = .Item("IDO_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CONTTRACK.INOUT_DATE <= @IDO_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IDO_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter()

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter()

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

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

#End Region 'SQL条件設定

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
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索結果項目マッピング
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLMI190OUTMap() As Hashtable

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INOUT_DATE", "INOUT_DATE")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")
        map.Add("STATUS", "STATUS")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("REMARK", "REMARK")
        map.Add("TOFROM_CD", "TOFROM_CD")
        map.Add("TOFROM_NM", "TOFROM_NM")
        map.Add("EXP_FLG", "EXP_FLG")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        'map.Add("NB", "NB")
        'map.Add("QT", "QT")
        'map.Add("LOT_NO", "LOT_NO")
        map.Add("CYLINDER_TYPE", "CYLINDER_TYPE")
        map.Add("EMPTY_KB", "EMPTY_KB")
        map.Add("HAIKI_YN", "HAIKI_YN")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("YOUKI_NO", "YOUKI_NO")
        map.Add("N40_CYLINDER_NO", "N40_CYLINDER_NO")
        map.Add("NEXT_TEST_DATE", "NEXT_TEST_DATE")
        map.Add("KEIKA_DATE1", "KEIKA_DATE1")
        map.Add("IOZS_KBNM", "IOZS_KBNM")
        map.Add("HAIKI_YNNM", "HAIKI_YNNM")
        map.Add("SEIQTO", "SEIQTO")
        map.Add("KEISANSTART_DATE", "KEISANSTART_DATE")
        map.Add("KEIKA_DATE2", "KEIKA_DATE2")
        map.Add("CHIEN_DATE", "CHIEN_DATE")
        map.Add("CHIEN_KBCD", "CHIEN_KBCD")
        map.Add("CHIEN_MONEY", "CHIEN_MONEY")
        map.Add("CHIEN_MONEY2", "CHIEN_MONEY2")
        map.Add("PROD_DATE", "PROD_DATE")           'ADD 2019/10/28 006786
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")   'ADD 2019/10/31 008262
        map.Add("GOODS_NM", "GOODS_NM")             'ADD 2019/10/31 008262
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")     'ADD 2019/12/10 009849
        map.Add("REMARK_IN", "REMARK_IN")

        Return map

    End Function

#End Region

#End Region 'Method

End Class
