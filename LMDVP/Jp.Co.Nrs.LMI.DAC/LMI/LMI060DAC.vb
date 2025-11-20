' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI060  : 三井化学ポリウレタン運賃計算「危険品一割増」処理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI060DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI060DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SELECT"

#Region "運賃再計算方法設定マスタ検索"

#Region "運賃再計算方法設定マスタの検索 SQL SELECT句"

    ''' <summary>
    ''' 運賃再計算方法設定マスタの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_RECAST As String = "SELECT                                                                         " & vbNewLine _
                                               & " RECAST.NRS_BR_CD                              AS NRS_BR_CD                   " & vbNewLine _
                                               & ",RECAST.CUST_CD_L                              AS CUST_CD_L                   " & vbNewLine _
                                               & ",RECAST.CUST_CD_M                              AS CUST_CD_M                   " & vbNewLine _
                                               & ",RECAST.CUST_CD_S                              AS CUST_CD_S                   " & vbNewLine _
                                               & ",RECAST.CUST_CD_SS                             AS CUST_CD_SS                  " & vbNewLine _
                                               & ",RECAST.TARIFF_CD                              AS TARIFF_CD                   " & vbNewLine _
                                               & ",RECAST.TARIFF_KB                              AS TARIFF_KB                   " & vbNewLine _
                                               & ",RECAST.WARIMASHI_NR                           AS WARIMASHI_NR                " & vbNewLine _
                                               & ",RECAST.ROUND_KB                               AS ROUND_KB                    " & vbNewLine _
                                               & ",RECAST.ROUND_UT                               AS ROUND_UT                    " & vbNewLine _
                                               & ",RECAST.KEISAN_KB                              AS KEISAN_KB                   " & vbNewLine _
                                               & ",RECAST.REMARK                                 AS REMARK                      " & vbNewLine _
                                               & ",RECAST.FREE_C01                               AS FREE_C01                    " & vbNewLine _
                                               & ",RECAST.FREE_C02                               AS FREE_C02                    " & vbNewLine _
                                               & ",RECAST.FREE_C03                               AS FREE_C03                    " & vbNewLine _
                                               & ",RECAST.FREE_C04                               AS FREE_C04                    " & vbNewLine _
                                               & ",RECAST.FREE_C05                               AS FREE_C05                    " & vbNewLine _
                                               & ",RECAST.FREE_N01                               AS FREE_N01                    " & vbNewLine _
                                               & ",RECAST.FREE_N02                               AS FREE_N02                    " & vbNewLine _
                                               & ",RECAST.FREE_N03                               AS FREE_N03                    " & vbNewLine _
                                               & ",RECAST.FREE_N04                               AS FREE_N04                    " & vbNewLine _
                                               & ",RECAST.FREE_N05                               AS FREE_N05                    " & vbNewLine _
                                               & ",Z1.KBN_NM1                                    AS ROUND_UT_LEN                " & vbNewLine

#End Region

#Region "運賃再計算方法設定マスタの検索 SQL FROM句"

    ''' <summary>
    ''' 運賃再計算方法設定マスタの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_RECAST As String = "FROM                                                                      " & vbNewLine _
                                                    & "$LM_MST$..M_UNCHIN_RECAST RECAST                                         " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z1                                                       " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z1.KBN_GROUP_CD = 'M003' AND                                             " & vbNewLine _
                                                    & "Z1.KBN_CD = RECAST.ROUND_UT                                              " & vbNewLine

#End Region

#Region "運賃再計算方法設定マスタの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 運賃再計算方法設定マスタの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_RECAST As String = "ORDER BY                                                                " & vbNewLine _
                                                    & " RECAST.NRS_BR_CD                                                       " & vbNewLine _
                                                    & ",RECAST.CUST_CD_L                                                       " & vbNewLine _
                                                    & ",RECAST.CUST_CD_M                                                       " & vbNewLine _
                                                    & ",RECAST.CUST_CD_S                                                       " & vbNewLine _
                                                    & ",RECAST.CUST_CD_SS                                                      " & vbNewLine

#End Region

#Region "作成対象データ検索 SQL SELECT句(出荷)"

    '要望番号:1482 KIM 2012/10/10 START
    'START YANAI 要望番号1447 山九（危険品一割増されていない）
    '''' <summary>
    '''' 作成対象データ検索 SQL SELECT句(出荷)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_MAKEDATA_OUTKA As String = "SELECT                                                                 " & vbNewLine _
    '                                           & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
    '                                           & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                   " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS LOGI_CLASS              " & vbNewLine _
    '                                           & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L                " & vbNewLine _
    '                                           & ",OUTKAM.OUTKA_NO_M                             AS INOUTKA_NO_M                " & vbNewLine _
    '                                           & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB                " & vbNewLine _
    '                                           & ",UNSOL.UNSO_CD                                 AS UNSO_CD                     " & vbNewLine _
    '                                           & ",UNSOL.UNSO_BR_CD                              AS UNSO_BR_CD                  " & vbNewLine _
    '                                           & ",UNCHIN.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD              " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_ETARIFF_CD                        AS SEIQ_ETARIFF_CD             " & vbNewLine _
    '                                           & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE             " & vbNewLine _
    '                                           & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE               " & vbNewLine _
    '                                           & ",OUTKAM.CUST_ORD_NO_DTL                        AS CUST_ORD_NO                 " & vbNewLine _
    '                                           & ",OUTKAL.DEST_CD                                AS DEST_CD                     " & vbNewLine _
    '                                           & ",CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                          " & vbNewLine _
    '                                           & "      ELSE DEST.DEST_NM                        END AS DEST_NM                 " & vbNewLine _
    '                                           & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST               " & vbNewLine _
    '                                           & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                  " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.SEIQ_WT,0)                      AS SEIQ_WT                     " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.DECI_UNCHIN,0) +                                               " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_CITY_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_WINT_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_RELY_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_TOLL,0) +                                                 " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_INSU,0)                    AS UNCHIN                      " & vbNewLine _
    '                                           & ",'0'                                           AS ZEIKOMI_UNCHIN              " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.SEIQ_KYORI,0)                   AS SEIQ_KYORI                  " & vbNewLine _
    '                                           & ",''                                            AS REM                         " & vbNewLine _
    '                                           & ",CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_AD_1                        " & vbNewLine _
    '                                           & "      ELSE DEST.AD_1                           END AS DEST_AD_1               " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_L                               AS CUST_CD_L                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_M                               AS CUST_CD_M                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_S                               AS CUST_CD_S                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_SS                              AS CUST_CD_SS                  " & vbNewLine _
    '                                           & ",GOODS.SEARCH_KEY_1                            AS SEARCH_KEY1                 " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_GROUP_NO                          AS SEIQ_GROUP_NO               " & vbNewLine _
    '                                           & ",UNCHIN.REMARK                                 AS REMARK                      " & vbNewLine _
    '                                           & ",CASE WHEN GOODS.DOKU_KB = '01' THEN ''                                       " & vbNewLine _
    '                                           & "      ELSE GOODS.DOKU_KB                       END AS DOKU_KB                 " & vbNewLine _
    '                                           & ",GOODS.SHOBO_CD                                AS SHOBO_CD                    " & vbNewLine _
    '                                           & ",ISNULL(OUTKAM.OUTKA_TTL_QT,0)                 AS WT                          " & vbNewLine _
    '                                           & ",(SELECT COUNT(OUTKAM2.NRS_BR_CD) FROM $LM_TRN$..C_OUTKA_M OUTKAM2            " & vbNewLine _
    '                                           & "  WHERE OUTKAL.NRS_BR_CD = OUTKAM2.NRS_BR_CD AND OUTKAL.OUTKA_NO_L = OUTKAM2.OUTKA_NO_L AND OUTKAM2.SYS_DEL_FLG='0') AS M_CNT " & vbNewLine _
    '                                           & ",DEST.JIS                                      AS JIS                         " & vbNewLine _
    '                                           & ",GOODS.UNSO_ONDO_KB                            AS UNSO_ONDO_KB                " & vbNewLine _
    '                                           & ",GOODS.ONDO_UNSO_STR_DATE                      AS ONDO_UNSO_STR_DATE          " & vbNewLine _
    '                                           & ",GOODS.ONDO_UNSO_END_DATE                      AS ONDO_UNSO_END_DATE          " & vbNewLine _
    '                                           & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS                " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS FREE_C01                " & vbNewLine _
    '                                           & ",''                                            AS FREE_C02                    " & vbNewLine _
    '                                           & ",''                                            AS FREE_C03                    " & vbNewLine _
    '                                           & ",''                                            AS UNCHIN_HOZON                " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS LOGI_CLASS_HOZON        " & vbNewLine
    '''' <summary>
    '''' 作成対象データ検索 SQL SELECT句(出荷)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_MAKEDATA_OUTKA As String = "SELECT                                                                 " & vbNewLine _
    '                                           & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
    '                                           & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                   " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS LOGI_CLASS              " & vbNewLine _
    '                                           & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L                " & vbNewLine _
    '                                           & ",OUTKAM.OUTKA_NO_M                             AS INOUTKA_NO_M                " & vbNewLine _
    '                                           & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB                " & vbNewLine _
    '                                           & ",UNSOL.UNSO_CD                                 AS UNSO_CD                     " & vbNewLine _
    '                                           & ",UNSOL.UNSO_BR_CD                              AS UNSO_BR_CD                  " & vbNewLine _
    '                                           & ",UNCHIN.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD              " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_ETARIFF_CD                        AS SEIQ_ETARIFF_CD             " & vbNewLine _
    '                                           & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE             " & vbNewLine _
    '                                           & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE               " & vbNewLine _
    '                                           & ",CASE WHEN OUTKAM.CUST_ORD_NO_DTL = '' THEN OUTKAL.CUST_ORD_NO                " & vbNewLine _
    '                                           & "      ELSE OUTKAM.CUST_ORD_NO_DTL              END AS CUST_ORD_NO             " & vbNewLine _
    '                                           & ",OUTKAL.DEST_CD                                AS DEST_CD                     " & vbNewLine _
    '                                           & ",CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                          " & vbNewLine _
    '                                           & "      ELSE DEST.DEST_NM                        END AS DEST_NM                 " & vbNewLine _
    '                                           & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST               " & vbNewLine _
    '                                           & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                  " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.SEIQ_WT,0)                      AS SEIQ_WT                     " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.DECI_UNCHIN,0) +                                               " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_CITY_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_WINT_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_RELY_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_TOLL,0) +                                                 " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_INSU,0)                    AS UNCHIN                      " & vbNewLine _
    '                                           & ",'0'                                           AS ZEIKOMI_UNCHIN              " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.SEIQ_KYORI,0)                   AS SEIQ_KYORI                  " & vbNewLine _
    '                                           & ",''                                            AS REM                         " & vbNewLine _
    '                                           & ",CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_AD_1                        " & vbNewLine _
    '                                           & "      ELSE DEST.AD_1                           END AS DEST_AD_1               " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_L                               AS CUST_CD_L                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_M                               AS CUST_CD_M                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_S                               AS CUST_CD_S                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_SS                              AS CUST_CD_SS                  " & vbNewLine _
    '                                           & ",GOODS.SEARCH_KEY_1                            AS SEARCH_KEY1                 " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_GROUP_NO                          AS SEIQ_GROUP_NO               " & vbNewLine _
    '                                           & ",UNCHIN.REMARK                                 AS REMARK                      " & vbNewLine _
    '                                           & ",CASE WHEN GOODS.DOKU_KB = '01' THEN ''                                       " & vbNewLine _
    '                                           & "      ELSE GOODS.DOKU_KB                       END AS DOKU_KB                 " & vbNewLine _
    '                                           & ",GOODS.SHOBO_CD                                AS SHOBO_CD                    " & vbNewLine _
    '                                           & ",ISNULL(OUTKAM.OUTKA_TTL_QT,0)                 AS WT                          " & vbNewLine _
    '                                           & ",(SELECT COUNT(OUTKAM2.NRS_BR_CD) FROM $LM_TRN$..C_OUTKA_M OUTKAM2            " & vbNewLine _
    '                                           & "  WHERE OUTKAL.NRS_BR_CD = OUTKAM2.NRS_BR_CD AND OUTKAL.OUTKA_NO_L = OUTKAM2.OUTKA_NO_L AND OUTKAM2.SYS_DEL_FLG='0') AS M_CNT " & vbNewLine _
    '                                           & ",DEST.JIS                                      AS JIS                         " & vbNewLine _
    '                                           & ",GOODS.UNSO_ONDO_KB                            AS UNSO_ONDO_KB                " & vbNewLine _
    '                                           & ",GOODS.ONDO_UNSO_STR_DATE                      AS ONDO_UNSO_STR_DATE          " & vbNewLine _
    '                                           & ",GOODS.ONDO_UNSO_END_DATE                      AS ONDO_UNSO_END_DATE          " & vbNewLine _
    '                                           & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS                " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS FREE_C01                " & vbNewLine _
    '                                           & ",''                                            AS FREE_C02                    " & vbNewLine _
    '                                           & ",''                                            AS FREE_C03                    " & vbNewLine _
    '                                           & ",''                                            AS UNCHIN_HOZON                " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS LOGI_CLASS_HOZON        " & vbNewLine
    'END YANAI 要望番号1447 山九（危険品一割増されていない）
    Private Const SQL_SELECT_MAKEDATA_OUTKA As String = "SELECT                                                                 " & vbNewLine _
                                               & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
                                               & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                   " & vbNewLine _
                                               & "--,CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                               & " --     WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                               & " --     WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                               & " --     WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                               & " --     ELSE ''                              END AS LOGI_CLASS                  " & vbNewLine _
                                               & ",CASE WHEN  KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                              " & vbNewLine _
                                               & "        THEN   KBN2.KBN_NM6                                                   " & vbNewLine _
                                               & "        ELSE   CASE WHEN  KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                " & vbNewLine _
                                               & "                      THEN KBN3.KBN_NM6                                       " & vbNewLine _
                                               & "                       ELSE ''         END                                    " & vbNewLine _
                                               & " END           AS LOGI_CLASS                                                  " & vbNewLine _
                                               & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L                " & vbNewLine _
                                               & ",OUTKAM.OUTKA_NO_M                             AS INOUTKA_NO_M                " & vbNewLine _
                                               & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB                " & vbNewLine _
                                               & ",UNSOL.UNSO_CD                                 AS UNSO_CD                     " & vbNewLine _
                                               & ",UNSOL.UNSO_BR_CD                              AS UNSO_BR_CD                  " & vbNewLine _
                                               & ",UNCHIN.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
                                               & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD              " & vbNewLine _
                                               & ",UNCHIN.SEIQ_ETARIFF_CD                        AS SEIQ_ETARIFF_CD             " & vbNewLine _
                                               & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE             " & vbNewLine _
                                               & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE               " & vbNewLine _
                                               & ",CASE WHEN OUTKAM.CUST_ORD_NO_DTL = '' THEN OUTKAL.CUST_ORD_NO                " & vbNewLine _
                                               & "      ELSE OUTKAM.CUST_ORD_NO_DTL              END AS CUST_ORD_NO             " & vbNewLine _
                                               & ",OUTKAL.DEST_CD                                AS DEST_CD                     " & vbNewLine _
                                               & ",CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                          " & vbNewLine _
                                               & "      ELSE DEST.DEST_NM                        END AS DEST_NM                 " & vbNewLine _
                                               & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST               " & vbNewLine _
                                               & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                  " & vbNewLine _
                                               & ",ISNULL(UNCHIN.SEIQ_WT,0)                      AS SEIQ_WT                     " & vbNewLine _
                                               & ",ISNULL(UNCHIN.DECI_UNCHIN,0) +                                               " & vbNewLine _
                                               & " ISNULL(UNCHIN.DECI_CITY_EXTC,0) +                                            " & vbNewLine _
                                               & " ISNULL(UNCHIN.DECI_WINT_EXTC,0) +                                            " & vbNewLine _
                                               & " ISNULL(UNCHIN.DECI_RELY_EXTC,0) +                                            " & vbNewLine _
                                               & " ISNULL(UNCHIN.DECI_TOLL,0) +                                                 " & vbNewLine _
                                               & " ISNULL(UNCHIN.DECI_INSU,0)                    AS UNCHIN                      " & vbNewLine _
                                               & ",'0'                                           AS ZEIKOMI_UNCHIN              " & vbNewLine _
                                               & ",ISNULL(UNCHIN.SEIQ_KYORI,0)                   AS SEIQ_KYORI                  " & vbNewLine _
                                               & ",''                                            AS REM                         " & vbNewLine _
                                               & ",CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_AD_1                        " & vbNewLine _
                                               & "      ELSE DEST.AD_1                           END AS DEST_AD_1               " & vbNewLine _
                                               & ",GOODS.CUST_CD_L                               AS CUST_CD_L                   " & vbNewLine _
                                               & ",GOODS.CUST_CD_M                               AS CUST_CD_M                   " & vbNewLine _
                                               & ",GOODS.CUST_CD_S                               AS CUST_CD_S                   " & vbNewLine _
                                               & ",GOODS.CUST_CD_SS                              AS CUST_CD_SS                  " & vbNewLine _
                                               & ",GOODS.SEARCH_KEY_1                            AS SEARCH_KEY1                 " & vbNewLine _
                                               & ",UNCHIN.SEIQ_GROUP_NO                          AS SEIQ_GROUP_NO               " & vbNewLine _
                                               & ",UNCHIN.REMARK                                 AS REMARK                      " & vbNewLine _
                                               & ",CASE WHEN GOODS.DOKU_KB = '01' THEN ''                                       " & vbNewLine _
                                               & "      ELSE GOODS.DOKU_KB                       END AS DOKU_KB                 " & vbNewLine _
                                               & ",GOODS.SHOBO_CD                                AS SHOBO_CD                    " & vbNewLine _
                                               & ",ISNULL(OUTKAM.OUTKA_TTL_QT,0)                 AS WT                          " & vbNewLine _
                                               & ",(SELECT COUNT(OUTKAM2.NRS_BR_CD) FROM $LM_TRN$..C_OUTKA_M OUTKAM2            " & vbNewLine _
                                               & "  WHERE OUTKAL.NRS_BR_CD = OUTKAM2.NRS_BR_CD AND OUTKAL.OUTKA_NO_L = OUTKAM2.OUTKA_NO_L AND OUTKAM2.SYS_DEL_FLG='0') AS M_CNT " & vbNewLine _
                                               & ",DEST.JIS                                      AS JIS                         " & vbNewLine _
                                               & ",GOODS.UNSO_ONDO_KB                            AS UNSO_ONDO_KB                " & vbNewLine _
                                               & ",GOODS.ONDO_UNSO_STR_DATE                      AS ONDO_UNSO_STR_DATE          " & vbNewLine _
                                               & ",GOODS.ONDO_UNSO_END_DATE                      AS ONDO_UNSO_END_DATE          " & vbNewLine _
                                               & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS                " & vbNewLine _
                                               & "--,CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                               & "--      WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                               & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                               & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                               & "--      ELSE ''                              END AS FREE_C01                    " & vbNewLine _
                                               & ",CASE WHEN  KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                              " & vbNewLine _
                                               & "        THEN   KBN2.KBN_NM6                                                   " & vbNewLine _
                                               & "        ELSE   CASE WHEN  KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                " & vbNewLine _
                                               & "                      THEN KBN3.KBN_NM6                                       " & vbNewLine _
                                               & "                       ELSE ''         END                                    " & vbNewLine _
                                               & " END           AS FREE_C01                                                  " & vbNewLine _
                                               & ",''                                            AS FREE_C02                    " & vbNewLine _
                                               & ",''                                            AS FREE_C03                    " & vbNewLine _
                                               & ",''                                            AS UNCHIN_HOZON                " & vbNewLine _
                                               & "--,CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                               & "--      WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                               & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                               & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                               & "--      ELSE ''                              END AS LOGI_CLASS_HOZON            " & vbNewLine _
                                               & ",CASE WHEN  KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                              " & vbNewLine _
                                               & "        THEN   KBN2.KBN_NM6                                                   " & vbNewLine _
                                               & "        ELSE   CASE WHEN  KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                " & vbNewLine _
                                               & "                      THEN KBN3.KBN_NM6                                       " & vbNewLine _
                                               & "                       ELSE ''         END                                    " & vbNewLine _
                                               & " END           AS LOGI_CLASS_HOZON                                                  " & vbNewLine _
                                               & "   ,CASE WHEN GOODS_DETAILS.SUB_KB = '16' AND UNSOCO.TARE_YN = '01'           " & vbNewLine _
                                               & "    THEN ISNULL(GOODS_DETAILS.SET_NAIYO,KBN.VALUE1)                           " & vbNewLine _
                                               & "    ELSE '' END AS FUTAI                                                      " & vbNewLine _
                                               & ",OUTKAM.OUTKA_TTL_NB                            AS TTL_NB                     " & vbNewLine

    ' 要望番号:1482 KIM 2012/10/10 END

#End Region

#Region "作成対象データ検索 SQL FROM句(出荷)"

    ''' <summary>
    ''' 作成対象データ検索 SQL FROM句(出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MAKEDATA_OUTKA As String = "FROM                                                              " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_L UNSOL                                               " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..F_UNCHIN_TRS UNCHIN                                          " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNCHIN.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.UNSO_NO_L = UNSOL.UNSO_NO_L                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SEIQ_FIXED_FLAG = '01'                                          " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_L OUTKAL                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAL.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAL.OUTKA_NO_L = UNSOL.INOUTKA_NO_L                                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAL.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_M OUTKAM                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                    " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_DEST DEST                                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.DEST_CD = OUTKAL.DEST_CD                                          " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                      " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..M_GOODS_DETAILS GOODS_DETAILS                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS_DETAILS.NRS_BR_CD = GOODS.NRS_BR_CD                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS_DETAILS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                        " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS_DETAILS.SUB_KB = '16'                                            " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..M_UNSOCO UNSOCO                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                 " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..Z_KBN KBN                                                      " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "KBN.KBN_GROUP_CD = 'N001'                                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "KBN.KBN_CD = GOODS.PKG_UT                                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "KBN.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                                      & "LEFT JOIN                                    " & vbNewLine _
                                                      & "LM_MST..Z_KBN   KBN2                       " & vbNewLine _
                                                      & "ON  KBN2.KBN_GROUP_CD = 'M029'               " & vbNewLine _
                                                      & "AND KBN2.KBN_NM1 = @NRS_BR_CD                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM2 = @CUST_CD_L                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM3 = @CUST_CD_M                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM4 = CASE WHEN @CUST_CD_S = ''           " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_S   END          " & vbNewLine _
                                                      & "AND KBN2.KBN_NM5 = CASE WHEN @CUST_CD_SS = ''            " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_SS  END          " & vbNewLine _
                                                      & "AND KBN2.KBN_NM6 = 'A1'                            " & vbNewLine _
                                                      & "--ADD START 2019/9/26 依頼番号:007330              " & vbNewLine _
                                                      & "AND KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD           " & vbNewLine _
                                                      & "--ADD END 2019/9/26 依頼番号:007330                " & vbNewLine _
                                                      & "LEFT JOIN                                    " & vbNewLine _
                                                      & "LM_MST..Z_KBN   KBN3                       " & vbNewLine _
                                                      & "ON  KBN3.KBN_GROUP_CD = 'M029'               " & vbNewLine _
                                                      & "AND KBN3.KBN_NM1 = @NRS_BR_CD                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM2 = @CUST_CD_L                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM3 = @CUST_CD_M                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM4 = CASE WHEN @CUST_CD_S = ''           " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_S   END          " & vbNewLine _
                                                      & "AND KBN3.KBN_NM5 = CASE WHEN @CUST_CD_SS = ''            " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_SS  END          " & vbNewLine _
                                                      & "AND KBN3.KBN_NM6 = 'A6'                            " & vbNewLine _
                                                      & "--ADD START 2019/9/26 依頼番号:007330              " & vbNewLine _
                                                      & "AND KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD           " & vbNewLine _
                                                      & "--ADD END 2019/9/26 依頼番号:007330                " & vbNewLine


#End Region

#Region "作成対象データ検索 SQL SELECT句(入荷)"

    ' 要望番号:1482 KIM 2012/10/10 START

    ''START YANAI 要望番号1447 山九（危険品一割増されていない）
    ''''' <summary>
    ''''' 作成対象データ検索 SQL SELECT句(入荷)
    ''''' </summary>
    ''''' <remarks></remarks>
    ''Private Const SQL_SELECT_MAKEDATA_INKA As String = "SELECT                                                                  " & vbNewLine _
    ''                                           & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
    ''                                           & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                   " & vbNewLine _
    ''                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    ''                                           & "      ELSE 'A6'                                END AS LOGI_CLASS              " & vbNewLine _
    ''                                           & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L                " & vbNewLine _
    ''                                           & ",INKAM.INKA_NO_M                               AS INOUTKA_NO_M                " & vbNewLine _
    ''                                           & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB                " & vbNewLine _
    ''                                           & ",UNSOL.UNSO_CD                                 AS UNSO_CD                     " & vbNewLine _
    ''                                           & ",UNSOL.UNSO_BR_CD                              AS UNSO_BR_CD                  " & vbNewLine _
    ''                                           & ",UNCHIN.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
    ''                                           & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD              " & vbNewLine _
    ''                                           & ",UNCHIN.SEIQ_ETARIFF_CD                        AS SEIQ_ETARIFF_CD             " & vbNewLine _
    ''                                           & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE             " & vbNewLine _
    ''                                           & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE               " & vbNewLine _
    ''                                           & ",INKAL.BUYER_ORD_NO_L                          AS CUST_ORD_NO                 " & vbNewLine _
    ''                                           & ",UNSOL.ORIG_CD                                 AS DEST_CD                     " & vbNewLine _
    ''                                           & ",DEST.DEST_NM                                  AS DEST_NM                     " & vbNewLine _
    ''                                           & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST               " & vbNewLine _
    ''                                           & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                  " & vbNewLine _
    ''                                           & ",ISNULL(UNCHIN.SEIQ_WT,0)                      AS SEIQ_WT                     " & vbNewLine _
    ''                                           & ",ISNULL(UNCHIN.DECI_UNCHIN,0) +                                               " & vbNewLine _
    ''                                           & " ISNULL(UNCHIN.DECI_CITY_EXTC,0) +                                            " & vbNewLine _
    ''                                           & " ISNULL(UNCHIN.DECI_WINT_EXTC,0) +                                            " & vbNewLine _
    ''                                           & " ISNULL(UNCHIN.DECI_RELY_EXTC,0) +                                            " & vbNewLine _
    ''                                           & " ISNULL(UNCHIN.DECI_TOLL,0) +                                                 " & vbNewLine _
    ''                                           & " ISNULL(UNCHIN.DECI_INSU,0)                    AS UNCHIN                      " & vbNewLine _
    ''                                           & ",'0'                                           AS ZEIKOMI_UNCHIN              " & vbNewLine _
    ''                                           & ",ISNULL(UNCHIN.SEIQ_KYORI,0)                   AS SEIQ_KYORI                  " & vbNewLine _
    ''                                           & ",''                                            AS REM                         " & vbNewLine _
    ''                                           & ",DEST.AD_1                                     AS DEST_AD_1                   " & vbNewLine _
    ''                                           & ",GOODS.CUST_CD_L                               AS CUST_CD_L                   " & vbNewLine _
    ''                                           & ",GOODS.CUST_CD_M                               AS CUST_CD_M                   " & vbNewLine _
    ''                                           & ",GOODS.CUST_CD_S                               AS CUST_CD_S                   " & vbNewLine _
    ''                                           & ",GOODS.CUST_CD_SS                              AS CUST_CD_SS                  " & vbNewLine _
    ''                                           & ",GOODS.SEARCH_KEY_1                            AS SEARCH_KEY1                 " & vbNewLine _
    ''                                           & ",UNCHIN.SEIQ_GROUP_NO                          AS SEIQ_GROUP_NO               " & vbNewLine _
    ''                                           & ",UNCHIN.REMARK                                 AS REMARK                      " & vbNewLine _
    ''                                           & ",CASE WHEN GOODS.DOKU_KB = '01' THEN ''                                       " & vbNewLine _
    ''                                           & "      ELSE GOODS.DOKU_KB                       END AS DOKU_KB                 " & vbNewLine _
    ''                                           & ",GOODS.SHOBO_CD                                AS SHOBO_CD                    " & vbNewLine _
    ''                                           & ",ISNULL(UNSOL.UNSO_WT,0)                       AS WT                          " & vbNewLine _
    ''                                           & ",'1'                                           AS M_CNT                       " & vbNewLine _
    ''                                           & ",DEST.JIS                                      AS JIS                         " & vbNewLine _
    ''                                           & ",GOODS.UNSO_ONDO_KB                            AS UNSO_ONDO_KB                " & vbNewLine _
    ''                                           & ",GOODS.ONDO_UNSO_STR_DATE                      AS ONDO_UNSO_STR_DATE          " & vbNewLine _
    ''                                           & ",GOODS.ONDO_UNSO_END_DATE                      AS ONDO_UNSO_END_DATE          " & vbNewLine _
    ''                                           & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS                " & vbNewLine _
    ''                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    ''                                           & "      ELSE 'A6'                                END AS FREE_C01                " & vbNewLine _
    ''                                           & ",''                                            AS FREE_C02                    " & vbNewLine _
    ''                                           & ",''                                            AS FREE_C03                    " & vbNewLine _
    ''                                           & ",''                                            AS UNCHIN_HOZON                " & vbNewLine _
    ''                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    ''                                           & "      ELSE 'A6'                                END AS LOGI_CLASS_HOZON        " & vbNewLine
    '''' <summary>
    '''' 作成対象データ検索 SQL SELECT句(入荷)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_MAKEDATA_INKA As String = "SELECT                                                                  " & vbNewLine _
    '                                           & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
    '                                           & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                   " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS LOGI_CLASS              " & vbNewLine _
    '                                           & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L                " & vbNewLine _
    '                                           & ",INKAM.INKA_NO_M                               AS INOUTKA_NO_M                " & vbNewLine _
    '                                           & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB                " & vbNewLine _
    '                                           & ",UNSOL.UNSO_CD                                 AS UNSO_CD                     " & vbNewLine _
    '                                           & ",UNSOL.UNSO_BR_CD                              AS UNSO_BR_CD                  " & vbNewLine _
    '                                           & ",UNCHIN.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD              " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_ETARIFF_CD                        AS SEIQ_ETARIFF_CD             " & vbNewLine _
    '                                           & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE             " & vbNewLine _
    '                                           & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE               " & vbNewLine _
    '                                           & ",CASE WHEN INKAM.OUTKA_FROM_ORD_NO_M = '' THEN INKAL.OUTKA_FROM_ORD_NO_L      " & vbNewLine _
    '                                           & "      ELSE INKAM.OUTKA_FROM_ORD_NO_M           END AS CUST_ORD_NO             " & vbNewLine _
    '                                           & ",UNSOL.ORIG_CD                                 AS DEST_CD                     " & vbNewLine _
    '                                           & ",DEST.DEST_NM                                  AS DEST_NM                     " & vbNewLine _
    '                                           & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST               " & vbNewLine _
    '                                           & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                  " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.SEIQ_WT,0)                      AS SEIQ_WT                     " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.DECI_UNCHIN,0) +                                               " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_CITY_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_WINT_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_RELY_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_TOLL,0) +                                                 " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_INSU,0)                    AS UNCHIN                      " & vbNewLine _
    '                                           & ",'0'                                           AS ZEIKOMI_UNCHIN              " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.SEIQ_KYORI,0)                   AS SEIQ_KYORI                  " & vbNewLine _
    '                                           & ",''                                            AS REM                         " & vbNewLine _
    '                                           & ",DEST.AD_1                                     AS DEST_AD_1                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_L                               AS CUST_CD_L                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_M                               AS CUST_CD_M                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_S                               AS CUST_CD_S                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_SS                              AS CUST_CD_SS                  " & vbNewLine _
    '                                           & ",GOODS.SEARCH_KEY_1                            AS SEARCH_KEY1                 " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_GROUP_NO                          AS SEIQ_GROUP_NO               " & vbNewLine _
    '                                           & ",UNCHIN.REMARK                                 AS REMARK                      " & vbNewLine _
    '                                           & ",CASE WHEN GOODS.DOKU_KB = '01' THEN ''                                       " & vbNewLine _
    '                                           & "      ELSE GOODS.DOKU_KB                       END AS DOKU_KB                 " & vbNewLine _
    '                                           & ",GOODS.SHOBO_CD                                AS SHOBO_CD                    " & vbNewLine _
    '                                           & ",ISNULL(UNSOL.UNSO_WT,0)                       AS WT                          " & vbNewLine _
    '                                           & ",'1'                                           AS M_CNT                       " & vbNewLine _
    '                                           & ",DEST.JIS                                      AS JIS                         " & vbNewLine _
    '                                           & ",GOODS.UNSO_ONDO_KB                            AS UNSO_ONDO_KB                " & vbNewLine _
    '                                           & ",GOODS.ONDO_UNSO_STR_DATE                      AS ONDO_UNSO_STR_DATE          " & vbNewLine _
    '                                           & ",GOODS.ONDO_UNSO_END_DATE                      AS ONDO_UNSO_END_DATE          " & vbNewLine _
    '                                           & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS                " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS FREE_C01                " & vbNewLine _
    '                                           & ",''                                            AS FREE_C02                    " & vbNewLine _
    '                                           & ",''                                            AS FREE_C03                    " & vbNewLine _
    '                                           & ",''                                            AS UNCHIN_HOZON                " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS LOGI_CLASS_HOZON        " & vbNewLine
    ''END YANAI 要望番号1447 山九（危険品一割増されていない）

    Private Const SQL_SELECT_MAKEDATA_INKA As String = "SELECT                                                                  " & vbNewLine _
                                           & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
                                           & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                   " & vbNewLine _
                                           & "--,CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                           & "--      ELSE ''                              END AS LOGI_CLASS                  " & vbNewLine _
                                           & ",CASE WHEN  KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                              " & vbNewLine _
                                           & "        THEN   KBN2.KBN_NM6                                                   " & vbNewLine _
                                           & "        ELSE   CASE WHEN  KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                " & vbNewLine _
                                           & "                      THEN KBN3.KBN_NM6                                       " & vbNewLine _
                                           & "                       ELSE ''         END                                    " & vbNewLine _
                                           & " END           AS LOGI_CLASS                                                  " & vbNewLine _
                                           & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L                " & vbNewLine _
                                           & ",INKAM.INKA_NO_M                               AS INOUTKA_NO_M                " & vbNewLine _
                                           & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB                " & vbNewLine _
                                           & ",UNSOL.UNSO_CD                                 AS UNSO_CD                     " & vbNewLine _
                                           & ",UNSOL.UNSO_BR_CD                              AS UNSO_BR_CD                  " & vbNewLine _
                                           & ",UNCHIN.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
                                           & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD              " & vbNewLine _
                                           & ",UNCHIN.SEIQ_ETARIFF_CD                        AS SEIQ_ETARIFF_CD             " & vbNewLine _
                                           & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE             " & vbNewLine _
                                           & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE               " & vbNewLine _
                                           & ",CASE WHEN INKAM.OUTKA_FROM_ORD_NO_M = '' THEN INKAL.OUTKA_FROM_ORD_NO_L      " & vbNewLine _
                                           & "      ELSE INKAM.OUTKA_FROM_ORD_NO_M           END AS CUST_ORD_NO             " & vbNewLine _
                                           & ",UNSOL.ORIG_CD                                 AS DEST_CD                     " & vbNewLine _
                                           & ",DEST.DEST_NM                                  AS DEST_NM                     " & vbNewLine _
                                           & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST               " & vbNewLine _
                                           & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                  " & vbNewLine _
                                           & ",ISNULL(UNCHIN.SEIQ_WT,0)                      AS SEIQ_WT                     " & vbNewLine _
                                           & ",ISNULL(UNCHIN.DECI_UNCHIN,0) +                                               " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_CITY_EXTC,0) +                                            " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_WINT_EXTC,0) +                                            " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_RELY_EXTC,0) +                                            " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_TOLL,0) +                                                 " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_INSU,0)                    AS UNCHIN                      " & vbNewLine _
                                           & ",'0'                                           AS ZEIKOMI_UNCHIN              " & vbNewLine _
                                           & ",ISNULL(UNCHIN.SEIQ_KYORI,0)                   AS SEIQ_KYORI                  " & vbNewLine _
                                           & ",''                                            AS REM                         " & vbNewLine _
                                           & ",DEST.AD_1                                     AS DEST_AD_1                   " & vbNewLine _
                                           & ",GOODS.CUST_CD_L                               AS CUST_CD_L                   " & vbNewLine _
                                           & ",GOODS.CUST_CD_M                               AS CUST_CD_M                   " & vbNewLine _
                                           & ",GOODS.CUST_CD_S                               AS CUST_CD_S                   " & vbNewLine _
                                           & ",GOODS.CUST_CD_SS                              AS CUST_CD_SS                  " & vbNewLine _
                                           & ",GOODS.SEARCH_KEY_1                            AS SEARCH_KEY1                 " & vbNewLine _
                                           & ",UNCHIN.SEIQ_GROUP_NO                          AS SEIQ_GROUP_NO               " & vbNewLine _
                                           & ",UNCHIN.REMARK                                 AS REMARK                      " & vbNewLine _
                                           & ",CASE WHEN GOODS.DOKU_KB = '01' THEN ''                                       " & vbNewLine _
                                           & "      ELSE GOODS.DOKU_KB                       END AS DOKU_KB                 " & vbNewLine _
                                           & ",GOODS.SHOBO_CD                                AS SHOBO_CD                    " & vbNewLine _
                                           & ",ISNULL(UNSOL.UNSO_WT,0)                       AS WT                          " & vbNewLine _
                                           & ",'1'                                           AS M_CNT                       " & vbNewLine _
                                           & ",DEST.JIS                                      AS JIS                         " & vbNewLine _
                                           & ",GOODS.UNSO_ONDO_KB                            AS UNSO_ONDO_KB                " & vbNewLine _
                                           & ",GOODS.ONDO_UNSO_STR_DATE                      AS ONDO_UNSO_STR_DATE          " & vbNewLine _
                                           & ",GOODS.ONDO_UNSO_END_DATE                      AS ONDO_UNSO_END_DATE          " & vbNewLine _
                                           & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS                " & vbNewLine _
                                           & "--,CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                           & "--      ELSE ''                              END AS FREE_C01                    " & vbNewLine _
                                           & ",CASE WHEN  KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                              " & vbNewLine _
                                           & "        THEN   KBN2.KBN_NM6                                                   " & vbNewLine _
                                           & "        ELSE   CASE WHEN  KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                " & vbNewLine _
                                           & "                      THEN KBN3.KBN_NM6                                       " & vbNewLine _
                                           & "                       ELSE ''         END                                    " & vbNewLine _
                                           & " END           AS FREE_C01                                                    " & vbNewLine _
                                           & ",''                                            AS FREE_C02                    " & vbNewLine _
                                           & ",''                                            AS FREE_C03                    " & vbNewLine _
                                           & ",''                                            AS UNCHIN_HOZON                " & vbNewLine _
                                           & ",CASE WHEN  KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                              " & vbNewLine _
                                           & "        THEN   KBN2.KBN_NM6                                                   " & vbNewLine _
                                           & "        ELSE   CASE WHEN  KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                " & vbNewLine _
                                           & "                      THEN KBN3.KBN_NM6                                       " & vbNewLine _
                                           & "                       ELSE ''         END                                    " & vbNewLine _
                                           & " END           AS LOGI_CLASS_HOZON                                                    " & vbNewLine _
                                           & "   ,CASE WHEN GOODS_DETAILS.SUB_KB = '16' AND UNSOCO.TARE_YN = '01'           " & vbNewLine _
                                           & "    THEN ISNULL(GOODS_DETAILS.SET_NAIYO,KBN.VALUE1)                           " & vbNewLine _
                                           & "    ELSE '' END AS FUTAI                                                      " & vbNewLine _
                                           & ",UNSOM.UNSO_TTL_NB                            AS TTL_NB                       " & vbNewLine

    ' 要望番号:1482 KIM 2012/10/10 END
#End Region

#Region "作成対象データ検索 SQL FROM句(入荷)"

    ''' <summary>
    ''' 作成対象データ検索 SQL FROM句(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MAKEDATA_INKA As String = "FROM                                                               " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_L UNSOL                                               " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_M UNSOM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_M = '001'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..F_UNCHIN_TRS UNCHIN                                          " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNCHIN.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.UNSO_NO_L = UNSOL.UNSO_NO_L                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SEIQ_FIXED_FLAG = '01'                                          " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_L INKAL                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAL.NRS_BR_CD = UNSOL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAL.INKA_NO_L = UNSOL.INOUTKA_NO_L                                   " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_M INKAM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_L = INKAL.INKA_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_M = '001'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_DEST DEST                                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "DEST.NRS_BR_CD = UNSOL.NRS_BR_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.DEST_CD = UNSOL.ORIG_CD                                           " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.CUST_CD_L = UNSOL.CUST_CD_L                                       " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..M_GOODS_DETAILS GOODS_DETAILS                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS_DETAILS.NRS_BR_CD = GOODS.NRS_BR_CD                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS_DETAILS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                        " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS_DETAILS.SUB_KB = '16'                                            " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..M_UNSOCO UNSOCO                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                 " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..Z_KBN KBN                                                      " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "KBN.KBN_GROUP_CD = 'N001'                                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "KBN.KBN_CD = GOODS.PKG_UT                                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "KBN.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                                      & "LEFT JOIN                                    " & vbNewLine _
                                                      & "LM_MST..Z_KBN   KBN2                         " & vbNewLine _
                                                      & "ON  KBN2.KBN_GROUP_CD = 'M029'               " & vbNewLine _
                                                      & "AND KBN2.KBN_NM1 = @NRS_BR_CD                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM2 = @CUST_CD_L                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM3 = @CUST_CD_M                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM4 = CASE WHEN @CUST_CD_S = ''             " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_S   END          " & vbNewLine _
                                                      & "AND KBN2.KBN_NM5 = CASE WHEN @CUST_CD_SS = ''            " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_SS  END          " & vbNewLine _
                                                      & "AND KBN2.KBN_NM6 = 'A1'                           " & vbNewLine _
                                                      & "--ADD START 2019/9/26 依頼番号:007330              " & vbNewLine _
                                                      & "AND KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD           " & vbNewLine _
                                                      & "--ADD END 2019/9/26 依頼番号:007330                " & vbNewLine _
                                                      & "LEFT JOIN                                    " & vbNewLine _
                                                      & "LM_MST..Z_KBN   KBN3                       " & vbNewLine _
                                                      & "ON  KBN3.KBN_GROUP_CD = 'M029'               " & vbNewLine _
                                                      & "AND KBN3.KBN_NM1 = @NRS_BR_CD                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM2 = @CUST_CD_L                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM3 = @CUST_CD_M                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM4 = CASE WHEN @CUST_CD_S = ''           " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_S   END          " & vbNewLine _
                                                      & "AND KBN3.KBN_NM5 = CASE WHEN @CUST_CD_SS = ''            " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_SS  END          " & vbNewLine _
                                                      & "AND KBN3.KBN_NM6 = 'A6'                            " & vbNewLine _
                                                      & "--ADD START 2019/9/26 依頼番号:007330              " & vbNewLine _
                                                      & "AND KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD           " & vbNewLine _
                                                      & "--ADD END 2019/9/26 依頼番号:007330                " & vbNewLine



#End Region

#Region "作成対象データ検索 SQL SELECT句(運送)"

    ' 要望番号:1482 KIM 2012/10/10 START
    '''' <summary>
    '''' 作成対象データ検索 SQL SELECT句(運送)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_MAKEDATA_UNSO As String = "SELECT                                                                  " & vbNewLine _
    '                                           & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
    '                                           & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                   " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS LOGI_CLASS              " & vbNewLine _
    '                                           & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L                " & vbNewLine _
    '                                           & ",UNSOM.UNSO_NO_M                               AS INOUTKA_NO_M                " & vbNewLine _
    '                                           & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB                " & vbNewLine _
    '                                           & ",UNSOL.UNSO_CD                                 AS UNSO_CD                     " & vbNewLine _
    '                                           & ",UNSOL.UNSO_BR_CD                              AS UNSO_BR_CD                  " & vbNewLine _
    '                                           & ",UNCHIN.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD              " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_ETARIFF_CD                        AS SEIQ_ETARIFF_CD             " & vbNewLine _
    '                                           & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE             " & vbNewLine _
    '                                           & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE               " & vbNewLine _
    '                                           & ",UNSOL.CUST_REF_NO                             AS CUST_ORD_NO                 " & vbNewLine _
    '                                           & ",UNSOL.DEST_CD                                 AS DEST_CD                     " & vbNewLine _
    '                                           & ",DEST.DEST_NM                                  AS DEST_NM                     " & vbNewLine _
    '                                           & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST               " & vbNewLine _
    '                                           & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                  " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.SEIQ_WT,0)                      AS SEIQ_WT                     " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.DECI_UNCHIN,0) +                                               " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_CITY_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_WINT_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_RELY_EXTC,0) +                                            " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_TOLL,0) +                                                 " & vbNewLine _
    '                                           & " ISNULL(UNCHIN.DECI_INSU,0)                    AS UNCHIN                      " & vbNewLine _
    '                                           & ",'0'                                           AS ZEIKOMI_UNCHIN              " & vbNewLine _
    '                                           & ",ISNULL(UNCHIN.SEIQ_KYORI,0)                   AS SEIQ_KYORI                  " & vbNewLine _
    '                                           & ",''                                            AS REM                         " & vbNewLine _
    '                                           & ",DEST.AD_1                                     AS DEST_AD_1                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_L                               AS CUST_CD_L                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_M                               AS CUST_CD_M                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_S                               AS CUST_CD_S                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_SS                              AS CUST_CD_SS                  " & vbNewLine _
    '                                           & ",GOODS.SEARCH_KEY_1                            AS SEARCH_KEY1                 " & vbNewLine _
    '                                           & ",UNCHIN.SEIQ_GROUP_NO                          AS SEIQ_GROUP_NO               " & vbNewLine _
    '                                           & ",UNCHIN.REMARK                                 AS REMARK                      " & vbNewLine _
    '                                           & ",CASE WHEN GOODS.DOKU_KB = '01' THEN ''                                       " & vbNewLine _
    '                                           & "      ELSE GOODS.DOKU_KB                       END AS DOKU_KB                 " & vbNewLine _
    '                                           & ",GOODS.SHOBO_CD                                AS SHOBO_CD                    " & vbNewLine _
    '                                           & ",ISNULL(UNSOL.UNSO_WT,0)                       AS WT                          " & vbNewLine _
    '                                           & ",'1'                                           AS M_CNT                       " & vbNewLine _
    '                                           & ",DEST.JIS                                      AS JIS                         " & vbNewLine _
    '                                           & ",GOODS.UNSO_ONDO_KB                            AS UNSO_ONDO_KB                " & vbNewLine _
    '                                           & ",GOODS.ONDO_UNSO_STR_DATE                      AS ONDO_UNSO_STR_DATE          " & vbNewLine _
    '                                           & ",GOODS.ONDO_UNSO_END_DATE                      AS ONDO_UNSO_END_DATE          " & vbNewLine _
    '                                           & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS                " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS FREE_C01                " & vbNewLine _
    '                                           & ",''                                            AS FREE_C02                    " & vbNewLine _
    '                                           & ",''                                            AS FREE_C03                    " & vbNewLine _
    '                                           & ",''                                            AS UNCHIN_HOZON                " & vbNewLine _
    '                                           & ",CASE WHEN UNSOL.UNSO_BR_CD = '99' THEN 'A1'                                  " & vbNewLine _
    '                                           & "      ELSE 'A6'                                END AS LOGI_CLASS_HOZON        " & vbNewLine
    Private Const SQL_SELECT_MAKEDATA_UNSO As String = "SELECT                                                                  " & vbNewLine _
                                           & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
                                           & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                   " & vbNewLine _
                                           & "--,CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                           & "--      ELSE ''                              END AS LOGI_CLASS                  " & vbNewLine _
                                           & ",CASE WHEN  KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                              " & vbNewLine _
                                           & "        THEN   KBN2.KBN_NM6                                                   " & vbNewLine _
                                           & "        ELSE   CASE WHEN  KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                " & vbNewLine _
                                           & "                      THEN KBN3.KBN_NM6                                       " & vbNewLine _
                                           & "                       ELSE ''         END                                    " & vbNewLine _
                                           & " END           AS LOGI_CLASS                                                  " & vbNewLine _
                                           & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L                " & vbNewLine _
                                           & ",UNSOM.UNSO_NO_M                               AS INOUTKA_NO_M                " & vbNewLine _
                                           & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB                " & vbNewLine _
                                           & ",UNSOL.UNSO_CD                                 AS UNSO_CD                     " & vbNewLine _
                                           & ",UNSOL.UNSO_BR_CD                              AS UNSO_BR_CD                  " & vbNewLine _
                                           & ",UNCHIN.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
                                           & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD              " & vbNewLine _
                                           & ",UNCHIN.SEIQ_ETARIFF_CD                        AS SEIQ_ETARIFF_CD             " & vbNewLine _
                                           & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE             " & vbNewLine _
                                           & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE               " & vbNewLine _
                                           & ",UNSOL.CUST_REF_NO                             AS CUST_ORD_NO                 " & vbNewLine _
                                           & ",UNSOL.DEST_CD                                 AS DEST_CD                     " & vbNewLine _
                                           & ",DEST.DEST_NM                                  AS DEST_NM                     " & vbNewLine _
                                           & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST               " & vbNewLine _
                                           & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                  " & vbNewLine _
                                           & ",ISNULL(UNCHIN.SEIQ_WT,0)                      AS SEIQ_WT                     " & vbNewLine _
                                           & ",ISNULL(UNCHIN.DECI_UNCHIN,0) +                                               " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_CITY_EXTC,0) +                                            " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_WINT_EXTC,0) +                                            " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_RELY_EXTC,0) +                                            " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_TOLL,0) +                                                 " & vbNewLine _
                                           & " ISNULL(UNCHIN.DECI_INSU,0)                    AS UNCHIN                      " & vbNewLine _
                                           & ",'0'                                           AS ZEIKOMI_UNCHIN              " & vbNewLine _
                                           & ",ISNULL(UNCHIN.SEIQ_KYORI,0)                   AS SEIQ_KYORI                  " & vbNewLine _
                                           & ",''                                            AS REM                         " & vbNewLine _
                                           & ",DEST.AD_1                                     AS DEST_AD_1                   " & vbNewLine _
                                           & ",GOODS.CUST_CD_L                               AS CUST_CD_L                   " & vbNewLine _
                                           & ",GOODS.CUST_CD_M                               AS CUST_CD_M                   " & vbNewLine _
                                           & ",GOODS.CUST_CD_S                               AS CUST_CD_S                   " & vbNewLine _
                                           & ",GOODS.CUST_CD_SS                              AS CUST_CD_SS                  " & vbNewLine _
                                           & ",GOODS.SEARCH_KEY_1                            AS SEARCH_KEY1                 " & vbNewLine _
                                           & ",UNCHIN.SEIQ_GROUP_NO                          AS SEIQ_GROUP_NO               " & vbNewLine _
                                           & ",UNCHIN.REMARK                                 AS REMARK                      " & vbNewLine _
                                           & ",CASE WHEN GOODS.DOKU_KB = '01' THEN ''                                       " & vbNewLine _
                                           & "      ELSE GOODS.DOKU_KB                       END AS DOKU_KB                 " & vbNewLine _
                                           & ",GOODS.SHOBO_CD                                AS SHOBO_CD                    " & vbNewLine _
                                           & ",ISNULL(UNSOL.UNSO_WT,0)                       AS WT                          " & vbNewLine _
                                           & ",'1'                                           AS M_CNT                       " & vbNewLine _
                                           & ",DEST.JIS                                      AS JIS                         " & vbNewLine _
                                           & ",GOODS.UNSO_ONDO_KB                            AS UNSO_ONDO_KB                " & vbNewLine _
                                           & ",GOODS.ONDO_UNSO_STR_DATE                      AS ONDO_UNSO_STR_DATE          " & vbNewLine _
                                           & ",GOODS.ONDO_UNSO_END_DATE                      AS ONDO_UNSO_END_DATE          " & vbNewLine _
                                           & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS                " & vbNewLine _
                                           & "--,CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                           & "--      ELSE ''                              END AS FREE_C01                    " & vbNewLine _
                                           & ",CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                           & "      WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                           & "      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                           & "      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                           & "      ELSE ''                              END AS FREE_C01                   " & vbNewLine _
                                           & ",''                                            AS FREE_C02                    " & vbNewLine _
                                           & ",''                                            AS FREE_C03                    " & vbNewLine _
                                           & ",''                                            AS UNCHIN_HOZON                " & vbNewLine _
                                           & "--,CASE WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) IN ('M','T') THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN RIGHT(UNCHIN.SEIQ_TARIFF_CD,1) = 'R'        THEN 'A6'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008K_EA'         THEN 'A1'              " & vbNewLine _
                                           & "--      WHEN UNCHIN.SEIQ_TARIFF_CD = '10008KnoEA'        THEN 'A1'              " & vbNewLine _
                                           & "--      ELSE ''                              END AS LOGI_CLASS_HOZON            " & vbNewLine _
                                           & ",CASE WHEN  KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                              " & vbNewLine _
                                           & "        THEN   KBN2.KBN_NM6                                                   " & vbNewLine _
                                           & "        ELSE   CASE WHEN  KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD                " & vbNewLine _
                                           & "                      THEN KBN3.KBN_NM6                                       " & vbNewLine _
                                           & "                       ELSE ''         END                                    " & vbNewLine _
                                           & " END           AS LOGI_CLASS_HOZON                                                  " & vbNewLine _
                                           & "   ,CASE WHEN GOODS_DETAILS.SUB_KB = '16' AND UNSOCO.TARE_YN = '01'           " & vbNewLine _
                                           & "    THEN ISNULL(GOODS_DETAILS.SET_NAIYO,KBN.VALUE1)                           " & vbNewLine _
                                           & "    ELSE '' END AS FUTAI                                                      " & vbNewLine _
                                           & ",UNSOM.UNSO_TTL_NB                            AS TTL_NB                       " & vbNewLine

    ' 要望番号:1482 KIM 2012/10/10 END

#End Region

#Region "作成対象データ検索 SQL FROM句(運送)"

    ''' <summary>
    ''' 作成対象データ検索 SQL FROM句(運送)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MAKEDATA_UNSO As String = "FROM                                                               " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_L UNSOL                                               " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_M UNSOM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_M = '001'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..F_UNCHIN_TRS UNCHIN                                          " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNCHIN.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.UNSO_NO_L = UNSOL.UNSO_NO_L                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SEIQ_FIXED_FLAG = '01'                                          " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_DEST DEST                                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "DEST.NRS_BR_CD = UNSOL.NRS_BR_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.DEST_CD = UNSOL.DEST_CD                                           " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.CUST_CD_L = UNSOL.CUST_CD_L                                       " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = UNSOM.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..M_GOODS_DETAILS GOODS_DETAILS                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS_DETAILS.NRS_BR_CD = GOODS.NRS_BR_CD                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS_DETAILS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                        " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS_DETAILS.SUB_KB = '16'                                            " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..M_UNSOCO UNSOCO                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                 " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "LM_MST..Z_KBN KBN                                                      " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "KBN.KBN_GROUP_CD = 'N001'                                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "KBN.KBN_CD = GOODS.PKG_UT                                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "KBN.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                                      & "LEFT JOIN                                    " & vbNewLine _
                                                      & "LM_MST..Z_KBN   KBN2                       " & vbNewLine _
                                                      & "ON  KBN2.KBN_GROUP_CD = 'M029'               " & vbNewLine _
                                                      & "AND KBN2.KBN_NM1 = @NRS_BR_CD                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM2 = @CUST_CD_L                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM3 = @CUST_CD_M                " & vbNewLine _
                                                      & "AND KBN2.KBN_NM4 = CASE WHEN @CUST_CD_S = ''             " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_S   END          " & vbNewLine _
                                                      & "AND KBN2.KBN_NM5 = CASE WHEN @CUST_CD_SS = ''            " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_SS  END          " & vbNewLine _
                                                      & "AND KBN2.KBN_NM6 = 'A1'                            " & vbNewLine _
                                                      & "--ADD START 2019/9/26 依頼番号:007330              " & vbNewLine _
                                                      & "AND KBN2.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD           " & vbNewLine _
                                                      & "--ADD END 2019/9/26 依頼番号:007330                " & vbNewLine _
                                                      & "LEFT JOIN                                    " & vbNewLine _
                                                      & "LM_MST..Z_KBN   KBN3                       " & vbNewLine _
                                                      & "ON  KBN3.KBN_GROUP_CD = 'M029'               " & vbNewLine _
                                                      & "AND KBN3.KBN_NM1 = @NRS_BR_CD                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM2 = @CUST_CD_L                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM3 = @CUST_CD_M                " & vbNewLine _
                                                      & "AND KBN3.KBN_NM4 = CASE WHEN @CUST_CD_S = ''           " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_S   END          " & vbNewLine _
                                                      & "AND KBN3.KBN_NM5 = CASE WHEN @CUST_CD_SS = ''            " & vbNewLine _
                                                      & "                         THEN  ''                        " & vbNewLine _
                                                      & "                         ELSE  @CUST_CD_SS  END          " & vbNewLine _
                                                      & "AND KBN3.KBN_NM6 = 'A6'                            " & vbNewLine _
                                                      & "--ADD START 2019/9/26 依頼番号:007330              " & vbNewLine _
                                                      & "AND KBN3.KBN_NM7 = UNCHIN.SEIQ_TARIFF_CD           " & vbNewLine _
                                                      & "--ADD END 2019/9/26 依頼番号:007330                " & vbNewLine


#End Region

#End Region

#Region "MCPU運賃チェック検索"

#Region "MCPU運賃チェックの検索 SQL SELECT句"

    ''' <summary>
    ''' 運賃再計算方法設定マスタの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MCPU_COUNT As String = " SELECT COUNT(MCPU.NRS_BR_CD)		            AS SELECT_CNT           " & vbNewLine

#End Region

#Region "MCPU運賃チェックの検索 SQL FROM句"

    ''' <summary>
    ''' MCPU運賃チェックの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MCPU As String = "FROM                                                                      " & vbNewLine _
                                                    & "$LM_TRN$..I_MCPU_UNCHIN_CHK MCPU                                       " & vbNewLine

#End Region

#End Region

#Region "割増運賃マスタ検索"

#Region "割増運賃マスタ検索の検索 SQL SELECT句"

    ''' <summary>
    ''' 割増運賃マスタ検索の検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EXTC_UNCHIN As String = "SELECT                                                                    " & vbNewLine _
                                               & " EXTC.NRS_BR_CD                                AS NRS_BR_CD                   " & vbNewLine _
                                               & ",EXTC.EXTC_TARIFF_CD                           AS EXTC_TARIFF_CD              " & vbNewLine _
                                               & ",EXTC.JIS_CD                                   AS JIS_CD                      " & vbNewLine _
                                               & ",EXTC.EXTC_TARIFF_REM                          AS EXTC_TARIFF_REM             " & vbNewLine _
                                               & ",EXTC.WINT_KIKAN_FROM                          AS WINT_KIKAN_FROM             " & vbNewLine _
                                               & ",EXTC.WINT_KIKAN_TO                            AS WINT_KIKAN_TO               " & vbNewLine _
                                               & ",EXTC.WINT_EXTC_YN                             AS WINT_EXTC_YN                " & vbNewLine _
                                               & ",EXTC.CITY_EXTC_YN                             AS CITY_EXTC_YN                " & vbNewLine _
                                               & ",EXTC.RELY_EXTC_YN                             AS RELY_EXTC_YN                " & vbNewLine _
                                               & ",EXTC.FRRY_EXTC_YN                             AS FRRY_EXTC_YN                " & vbNewLine _
                                               & ",EXTC.FRRY_EXTC_10KG                           AS FRRY_EXTC_10KG              " & vbNewLine

#End Region

#Region "割増運賃マスタ検索の検索 SQL FROM句"

    ''' <summary>
    ''' 割増運賃マスタ検索の検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_EXTC_UNCHIN As String = "FROM                                                                 " & vbNewLine _
                                                        & "$LM_MST$..M_EXTC_UNCHIN EXTC                                         " & vbNewLine

#End Region

#Region "割増運賃マスタ検索の検索 SQL WHERE句"

    ''' <summary>
    ''' 割増運賃マスタ検索の検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_EXTC_UNCHIN As String = "WHERE                                                               " & vbNewLine _
                                                         & "EXTC.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                                         & "AND                                                                 " & vbNewLine _
                                                         & "EXTC.WINT_EXTC_YN <> '00'                                           " & vbNewLine _
                                                         & "AND                                                                 " & vbNewLine _
                                                         & "EXTC.WINT_KIKAN_FROM <> ''                                          " & vbNewLine _
                                                         & "AND                                                                 " & vbNewLine _
                                                         & "EXTC.WINT_KIKAN_TO <> ''                                            " & vbNewLine

#End Region

#End Region

#Region "税率取得"

#Region "税マスタ検索の検索 SQL SELECT句"

    ''' <summary>
    ''' 税マスタ検索の検索 SQL 
    '''     ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TAX As String = "SELECT                                           " & vbNewLine _
                                        & " TAX.START_DATE     AS START_DATE                " & vbNewLine _
                                        & ",TAX.TAX_CD         AS TAX_CD                    " & vbNewLine _
                                        & ",TAX.TAX_RATE       AS TAX_RATE                  " & vbNewLine _
                                        & "FROM                                             " & vbNewLine _
                                        & " (SELECT                                         " & vbNewLine _
                                        & "	 MAX(START_DATE) AS START_DATE                  " & vbNewLine _
                                        & "         , TAX3.TAX_CD     AS TAX_CD             " & vbNewLine _
                                        & "   FROM                                          " & vbNewLine _
                                        & "      $LM_MST$..M_TAX TAX3                       " & vbNewLine _
                                        & "    WHERE                                        " & vbNewLine _
                                        & "            TAX3.START_DATE <= @DATE_FROM        " & vbNewLine _
                                        & "     AND TAX3.TAX_CD      = 'JPS'                " & vbNewLine _
                                        & "   GROUP BY                                      " & vbNewLine _
                                        & "       TAX3.TAX_CD) TAX2                         " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_TAX  AS TAX ON             " & vbNewLine _
                                        & "      TAX.START_DATE = TAX2.START_DATE           " & vbNewLine _
                                        & "  AND TAX.TAX_CD     = TAX2.TAX_CD               " & vbNewLine

#End Region

#End Region


#Region "SQL UNION句"

    ''' <summary>
    ''' SQL UNION句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNION As String = "UNION                                                                     " & vbNewLine

#End Region

#End Region

#Region "INSERT"

#Region "MCPU運賃チェックの新規作成"

#Region "MCPU運賃チェックの新規作成 SQL INSERT句"

    ''' <summary>
    ''' MCPU運賃チェックの新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_MCPU As String = "INSERT INTO $LM_TRN$..I_MCPU_UNCHIN_CHK                 " & vbNewLine _
                                                 & " ( 		                                           " & vbNewLine _
                                                 & " NRS_BR_CD,                                        " & vbNewLine _
                                                 & " UNSO_NO_L,                                        " & vbNewLine _
                                                 & " LOGI_CLASS,                                       " & vbNewLine _
                                                 & " INOUTKA_NO_L,                                     " & vbNewLine _
                                                 & " INOUTKA_NO_M,                                     " & vbNewLine _
                                                 & " MOTO_DATA_KB,                                     " & vbNewLine _
                                                 & " UNSOCO_CD,                                        " & vbNewLine _
                                                 & " UNSOCO_BR_CD,                                     " & vbNewLine _
                                                 & " SEIQ_CD,                                          " & vbNewLine _
                                                 & " SEIQ_TARIFF_CD,                                   " & vbNewLine _
                                                 & " SEIQ_ETARIFF_CD,                                  " & vbNewLine _
                                                 & " OUTKA_PLAN_DATE,                                  " & vbNewLine _
                                                 & " ARR_PLAN_DATE,                                    " & vbNewLine _
                                                 & " CUST_ORD_NO,                                      " & vbNewLine _
                                                 & " DEST_CD,                                          " & vbNewLine _
                                                 & " GOODS_CD_NRS,                                     " & vbNewLine _
                                                 & " GOODS_CD_CUST,                                    " & vbNewLine _
                                                 & " SEIQ_WT,                                          " & vbNewLine _
                                                 & " DECI_UNCHIN,                                      " & vbNewLine _
                                                 & " ZEIKOMI_UNCHIN,                                   " & vbNewLine _
                                                 & " SEIQ_KYORI,                                       " & vbNewLine _
                                                 & " WT,                                               " & vbNewLine _
                                                 & " REMARK,                                           " & vbNewLine _
                                                 & " SEARCH_KEY1,                                      " & vbNewLine _
                                                 & " CUST_CD_L,                                        " & vbNewLine _
                                                 & " CUST_CD_M,                                        " & vbNewLine _
                                                 & " CUST_CD_S,                                        " & vbNewLine _
                                                 & " CUST_CD_SS,                                       " & vbNewLine _
                                                 & " FREE_C01,                                         " & vbNewLine _
                                                 & " FREE_C02,                                         " & vbNewLine _
                                                 & " FREE_C03,                                         " & vbNewLine _
                                                 & " SYS_ENT_DATE,                                     " & vbNewLine _
                                                 & " SYS_ENT_TIME,                                     " & vbNewLine _
                                                 & " SYS_ENT_PGID,                                     " & vbNewLine _
                                                 & " SYS_ENT_USER,                                     " & vbNewLine _
                                                 & " SYS_UPD_DATE,                                     " & vbNewLine _
                                                 & " SYS_UPD_TIME,                                     " & vbNewLine _
                                                 & " SYS_UPD_PGID,                                     " & vbNewLine _
                                                 & " SYS_UPD_USER,                                     " & vbNewLine _
                                                 & " SYS_DEL_FLG                                       " & vbNewLine _
                                                 & " ) VALUES (                                        " & vbNewLine _
                                                 & " @NRS_BR_CD,                                       " & vbNewLine _
                                                 & " @UNSO_NO_L,                                       " & vbNewLine _
                                                 & " @LOGI_CLASS,                                      " & vbNewLine _
                                                 & " @INOUTKA_NO_L,                                    " & vbNewLine _
                                                 & " @INOUTKA_NO_M,                                    " & vbNewLine _
                                                 & " @MOTO_DATA_KB,                                    " & vbNewLine _
                                                 & " @UNSOCO_CD,                                       " & vbNewLine _
                                                 & " @UNSOCO_BR_CD,                                    " & vbNewLine _
                                                 & " @SEIQ_CD,                                         " & vbNewLine _
                                                 & " @SEIQ_TARIFF_CD,                                  " & vbNewLine _
                                                 & " @SEIQ_ETARIFF_CD,                                 " & vbNewLine _
                                                 & " @OUTKA_PLAN_DATE,                                 " & vbNewLine _
                                                 & " @ARR_PLAN_DATE,                                   " & vbNewLine _
                                                 & " @CUST_ORD_NO,                                     " & vbNewLine _
                                                 & " @DEST_CD,                                         " & vbNewLine _
                                                 & " @GOODS_CD_NRS,                                    " & vbNewLine _
                                                 & " @GOODS_CD_CUST,                                   " & vbNewLine _
                                                 & " @SEIQ_WT,                                         " & vbNewLine _
                                                 & " @DECI_UNCHIN,                                     " & vbNewLine _
                                                 & " @ZEIKOMI_UNCHIN,                                  " & vbNewLine _
                                                 & " @SEIQ_KYORI,                                      " & vbNewLine _
                                                 & " @WT,                                              " & vbNewLine _
                                                 & " @REMARK,                                          " & vbNewLine _
                                                 & " @SEARCH_KEY1,                                     " & vbNewLine _
                                                 & " @CUST_CD_L,                                       " & vbNewLine _
                                                 & " @CUST_CD_M,                                       " & vbNewLine _
                                                 & " @CUST_CD_S,                                       " & vbNewLine _
                                                 & " @CUST_CD_SS,                                      " & vbNewLine _
                                                 & " @FREE_C01,                                        " & vbNewLine _
                                                 & " @FREE_C02,                                        " & vbNewLine _
                                                 & " @FREE_C03,                                        " & vbNewLine _
                                                 & " @SYS_ENT_DATE,                                    " & vbNewLine _
                                                 & " @SYS_ENT_TIME,                                    " & vbNewLine _
                                                 & " @SYS_ENT_PGID,                                    " & vbNewLine _
                                                 & " @SYS_ENT_USER,                                    " & vbNewLine _
                                                 & " @SYS_UPD_DATE,                                    " & vbNewLine _
                                                 & " @SYS_UPD_TIME,                                    " & vbNewLine _
                                                 & " @SYS_UPD_PGID,                                    " & vbNewLine _
                                                 & " @SYS_UPD_USER,                                    " & vbNewLine _
                                                 & " @SYS_DEL_FLG                                      " & vbNewLine _
                                                 & " )                                                 " & vbNewLine

#End Region

#End Region

#End Region

#Region "DELETE"

#Region "MCPU運賃チェック削除"

    Private Const SQL_DELETE_MCPU As String = "DELETE FROM $LM_TRN$..I_MCPU_UNCHIN_CHK " & vbNewLine

#End Region

#End Region

#End Region

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

#End Region

#Region "Method"

#Region "SQLメイン処理"

#Region "運賃再計算方法設定マスタ検索"

    ''' <summary>
    ''' 運賃再計算方法設定マスタの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinRecastData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI060IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI060DAC.SQL_SELECT_RECAST)        'SQL構築(Select句)
        Me._StrSql.Append(LMI060DAC.SQL_SELECT_FROM_RECAST)   'SQL構築(From句)
        Call Me.SetSQLWhereRECAST(inTbl.Rows(0))              '条件設定
        Me._StrSql.Append(LMI060DAC.SQL_SELECT_ORDER_RECAST)  'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI060DAC", "SelectUnchinRecastData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("TARIFF_CD", "TARIFF_CD")
        map.Add("TARIFF_KB", "TARIFF_KB")
        map.Add("WARIMASHI_NR", "WARIMASHI_NR")
        map.Add("ROUND_KB", "ROUND_KB")
        map.Add("ROUND_UT", "ROUND_UT")
        map.Add("KEISAN_KB", "KEISAN_KB")
        map.Add("REMARK", "REMARK")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("ROUND_UT_LEN", "ROUND_UT_LEN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI060OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "MCPU運賃チェック検索"

    ''' <summary>
    ''' MCPU運賃チェックの件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MakeDataCHK(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI060IN")

        '要望番号:1482 KIM 2012/10/10 START

        ''SQL格納変数の初期化
        'Me._StrSql = New StringBuilder()

        ''SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()

        ''SQL作成
        'Me._StrSql.Append(LMI060DAC.SQL_SELECT_MCPU_COUNT) 'SQL構築(Select句)
        'Me._StrSql.Append(LMI060DAC.SQL_SELECT_FROM_MCPU)  'SQL構築(From句)
        'Call Me.SetSQLWhereMCPU(inTbl.Rows(0))             '条件設定

        ''スキーマ名設定
        'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ''SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ''パラメータの反映
        'For Each obj As Object In Me._SqlPrmList
        '    cmd.Parameters.Add(obj)
        'Next

        'MyBase.Logger.WriteSQLLog("LMI060DAC", "MakeDataCHK", cmd)

        ''SQLの発行
        'Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''処理件数の設定
        'reader.Read()
        'MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        'reader.Close()
        'Return ds

        Dim max As Integer = inTbl.Rows.Count - 1

        '件数格納変数
        Dim cnt As Integer = 0

        For i As Integer = 0 To max

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQL作成
            Me._StrSql.Append(LMI060DAC.SQL_SELECT_MCPU_COUNT) 'SQL構築(Select句)
            Me._StrSql.Append(LMI060DAC.SQL_SELECT_FROM_MCPU)  'SQL構築(From句)
            Call Me.SetSQLWhereMCPU(inTbl.Rows(i))             '条件設定

            'スキーマ名設定
            Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(i).Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI060DAC", "MakeDataCHK", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            '処理件数の設定
            reader.Read()
            cnt = cnt + Convert.ToInt32(reader("SELECT_CNT"))
            reader.Close()

        Next

        MyBase.SetResultCount(cnt)

        Return ds

        '要望番号:1482 KIM 2012/10/10 END

    End Function

#End Region

#Region "MCPU運賃チェック削除"

    ''' <summary>
    ''' MCPU運賃チェック削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>I_MCPU_UNCHIN_CHK削除SQLの構築・発行</remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI060IN")
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQL作成
            Me._StrSql.Append(LMI060DAC.SQL_DELETE_MCPU)       'SQL構築(DELETE句)
            '要望番号:1482 KIM 2012/10/10 START
            Call Me.SetSQLWhereDeleteMCPU(inTbl.Rows(i))       '条件設定

            'スキーマ名設定
            Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(i).Item("NRS_BR_CD").ToString())

            '要望番号:1482 KIM 2012/10/10 END

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI060DAC", "DeleteData", cmd)

            'SQLの発行
            Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "作成対象データ検索"

    ''' <summary>
    ''' 作成対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMakeData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI060IN")

        '要望番号:1482 KIM 2012/10/10 START
        Dim sagyoDs As DataSet = ds.Copy()
        Dim preCustCd As String = String.Empty
        Dim nowCustCd As String = String.Empty

        For i As Integer = 0 To inTbl.Rows.Count - 1

            nowCustCd = String.Concat(inTbl.Rows(i).Item("CUST_CD_L"), inTbl.Rows(i).Item("CUST_CD_M"), inTbl.Rows(i).Item("CUST_CD_S"), inTbl.Rows(i).Item("CUST_CD_SS"))
            If nowCustCd.Equals(preCustCd) = True Then
                Continue For
            End If
            preCustCd = nowCustCd
            '要望番号:1482 KIM 2012/10/10 END

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQL作成
            Me._StrSql.Append(LMI060DAC.SQL_SELECT_MAKEDATA_OUTKA)        'SQL構築(Select句)(出荷)
            Me._StrSql.Append(LMI060DAC.SQL_SELECT_FROM_MAKEDATA_OUTKA)   'SQL構築(From句)(出荷)

            '要望番号:1482 KIM 2012/10/10 START
            'Call Me.SetSQLWhereMakeData(inTbl.Rows(0), "20")              '条件設定(出荷)
            Call Me.SetSQLWhereMakeData(inTbl.Rows(i), "20")              '条件設定(出荷)
            '要望番号:1482 KIM 2012/10/10 END

            Me._StrSql.Append(LMI060DAC.SQL_SELECT_UNION)                 'SQL構築(Union句)
            Me._StrSql.Append(LMI060DAC.SQL_SELECT_MAKEDATA_INKA)         'SQL構築(Select句)(入荷)
            Me._StrSql.Append(LMI060DAC.SQL_SELECT_FROM_MAKEDATA_INKA)    'SQL構築(From句)(入荷)

            '要望番号:1482 KIM 2012/10/10 START
            'Call Me.SetSQLWhereMakeData(inTbl.Rows(0), "10")              '条件設定(入荷)
            Call Me.SetSQLWhereMakeData(inTbl.Rows(i), "10")              '条件設定(入荷)
            '要望番号:1482 KIM 2012/10/10 END

            Me._StrSql.Append(LMI060DAC.SQL_SELECT_UNION)                 'SQL構築(Union句)
            Me._StrSql.Append(LMI060DAC.SQL_SELECT_MAKEDATA_UNSO)         'SQL構築(Select句)(運送)
            Me._StrSql.Append(LMI060DAC.SQL_SELECT_FROM_MAKEDATA_UNSO)    'SQL構築(From句)(運送)

            '要望番号:1482 KIM 2012/10/10 START
            'Call Me.SetSQLWhereMakeData(inTbl.Rows(0), "40")              '条件設定(運送)
            Call Me.SetSQLWhereMakeData(inTbl.Rows(i), "40")              '条件設定(運送)

            'パラメータの設定
            'Call SetMakeDataParameter(inTbl.Rows(0), Me._SqlPrmList)      '条件設定
            Call SetMakeDataParameter(inTbl.Rows(i), Me._SqlPrmList)      '条件設定

            'スキーマ名設定
            'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())
            Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(i).Item("NRS_BR_CD").ToString())
            '要望番号:1482 KIM 2012/10/10 END

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI060DAC", "SelectMakeData", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            'DataReader→DataTableへの転記
            Dim map As Hashtable = New Hashtable()

            '取得データの格納先をマッピング
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("UNSO_NO_L", "UNSO_NO_L")
            map.Add("LOGI_CLASS", "LOGI_CLASS")
            map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
            map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
            map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
            map.Add("UNSO_CD", "UNSO_CD")
            map.Add("UNSO_BR_CD", "UNSO_BR_CD")
            map.Add("SEIQTO_CD", "SEIQTO_CD")
            map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
            map.Add("SEIQ_ETARIFF_CD", "SEIQ_ETARIFF_CD")
            map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
            map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
            map.Add("CUST_ORD_NO", "CUST_ORD_NO")
            map.Add("DEST_CD", "DEST_CD")
            map.Add("DEST_NM", "DEST_NM")
            map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
            map.Add("GOODS_NM_1", "GOODS_NM_1")
            map.Add("SEIQ_WT", "SEIQ_WT")
            map.Add("UNCHIN", "UNCHIN")
            map.Add("ZEIKOMI_UNCHIN", "ZEIKOMI_UNCHIN")
            map.Add("SEIQ_KYORI", "SEIQ_KYORI")
            map.Add("REM", "REM")
            map.Add("DEST_AD_1", "DEST_AD_1")
            map.Add("CUST_CD_L", "CUST_CD_L")
            map.Add("CUST_CD_M", "CUST_CD_M")
            map.Add("CUST_CD_S", "CUST_CD_S")
            map.Add("CUST_CD_SS", "CUST_CD_SS")
            map.Add("SEARCH_KEY1", "SEARCH_KEY1")
            map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
            map.Add("REMARK", "REMARK")
            map.Add("DOKU_KB", "DOKU_KB")
            map.Add("SHOBO_CD", "SHOBO_CD")
            map.Add("WT", "WT")
            map.Add("M_CNT", "M_CNT")
            map.Add("JIS", "JIS")
            map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
            map.Add("ONDO_UNSO_STR_DATE", "ONDO_UNSO_STR_DATE")
            map.Add("ONDO_UNSO_END_DATE", "ONDO_UNSO_END_DATE")
            map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
            map.Add("FREE_C01", "FREE_C01")
            map.Add("FREE_C02", "FREE_C02")
            map.Add("FREE_C03", "FREE_C03")
            map.Add("UNCHIN_HOZON", "UNCHIN_HOZON")
            map.Add("LOGI_CLASS_HOZON", "LOGI_CLASS_HOZON")
            map.Add("FUTAI", "FUTAI")
            map.Add("TTL_NB", "TTL_NB")     'ADD 2019/05/15 004183

            '要望番号:1482 KIM 2012/10/10 START
            'ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI060INOUT")
            sagyoDs = MyBase.SetSelectResultToDataSet(map, sagyoDs, reader, "LMI060INOUT")
            ds.Tables("LMI060INOUT").Merge(sagyoDs.Tables("LMI060INOUT"))
            '要望番号:1482 KIM 2012/10/10 END

            reader.Close()

            '要望番号:1482 KIM 2012/10/10 START
        Next
        '要望番号:1482 KIM 2012/10/10 END

        Return ds

    End Function

#End Region

#Region "MCPU運賃チェックの新規追加"

    ''' <summary>
    ''' MCPU運賃チェックの新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MakeData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI060INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI060DAC.SQL_INSERT_MCPU)         'SQL構築(INSERT句)

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
            Call Me.SetMCPUInsParameter(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI060DAC", "MakeData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "割増運賃マスタ検索"

    ''' <summary>
    ''' 割増運賃マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExtcUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI060IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI060DAC.SQL_SELECT_EXTC_UNCHIN)        'SQL構築(Select句)
        Me._StrSql.Append(LMI060DAC.SQL_SELECT_FROM_EXTC_UNCHIN)   'SQL構築(From句)
        Me._StrSql.Append(LMI060DAC.SQL_SELECT_WHERE_EXTC_UNCHIN)  'SQL構築(Where句)

        'パラメータの設定
        Call SetNrsBrCdParameter(inTbl.Rows(0), Me._SqlPrmList)       '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI060DAC", "SelectExtcUnchin", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")
        map.Add("WINT_KIKAN_FROM", "WINT_KIKAN_FROM")
        map.Add("WINT_KIKAN_TO", "WINT_KIKAN_TO")
        map.Add("WINT_EXTC_YN", "WINT_EXTC_YN")
        map.Add("CITY_EXTC_YN", "CITY_EXTC_YN")
        map.Add("RELY_EXTC_YN", "RELY_EXTC_YN")
        map.Add("FRRY_EXTC_YN", "FRRY_EXTC_YN")
        map.Add("FRRY_EXTC_10KG", "FRRY_EXTC_10KG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "EXTC_UNCHIN")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "税率取得"

#Region "M_TAX取得"

    ''' <summary>
    ''' M_TAX検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTax(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI060IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI060DAC.SQL_SELECT_TAX)

        'パラメータの設定
        Call SetNrsBrCdParameter(inTbl.Rows(0), Me._SqlPrmList)       '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", inTbl.Rows(0).Item("DATE_FROM").ToString, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI060DAC", "SelectTax", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("START_DATE", "START_DATE")
        map.Add("TAX_CD", "TAX_CD")
        map.Add("TAX_RATE", "TAX_RATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI060TAX")

        reader.Close()

        Return ds

    End Function

#End Region

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 運賃再計算方法設定マスタ検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereRECAST(ByVal dr As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        Me._StrSql.Append(" WHERE 1 = 1")
        Me._StrSql.Append(vbNewLine)

        With dr

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND RECAST.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 MCPU運賃チェック検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereMCPU(ByVal dr As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        Me._StrSql.Append(" WHERE 1 = 1")
        Me._StrSql.Append(vbNewLine)

        With dr

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.CUST_CD_SS = @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            End If
            'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.CUST_CD_S = @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
            End If

            'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
            ''荷主コード(極小)
            'whereStr = .Item("CUST_CD_SS").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND MCPU.CUST_CD_SS = @CUST_CD_SS")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            'End If
            'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

            '期間From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '期間To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(削除時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereDeleteMCPU(ByVal dr As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        Me._StrSql.Append(" WHERE 1 = 1")
        Me._StrSql.Append(vbNewLine)

        With dr

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_SS = @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            End If
            'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_S = @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
            End If

            'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
            ''荷主コード(極小)
            'whereStr = .Item("CUST_CD_SS").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND CUST_CD_SS = @CUST_CD_SS")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            'End If
            'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

            '期間From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '期間To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 MCPU運賃チェックの新規追加"

    ''' <summary>
    ''' MCPU運賃チェックの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMCPUInsParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOGI_CLASS", .Item("LOGI_CLASS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_M", .Item("INOUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ZEIKOMI_UNCHIN", .Item("ZEIKOMI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@WT", .Item("WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY1", .Item("SEARCH_KEY1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 作成対象データの検索(条件設定)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereMakeData(ByVal inTblRow As DataRow, ByVal motoDataKbn As String)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("UNSOL.SYS_DEL_FLG = '0'                                        ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(String.Concat(" AND UNSOL.MOTO_DATA_KB = '", motoDataKbn, "'    "))
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
            End If

            'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_SS = @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
            End If

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_S = @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
            End If
            'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

            '出荷日From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
            End If

            '出荷日To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 作成対象データの検索(パラメータ設定)"

    ''' <summary>
    ''' 作成対象データの検索のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMakeDataParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim whereStr As String = String.Empty

        With conditionRow

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", "", DBDataType.CHAR))
            End If

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", "", DBDataType.CHAR))

            End If
            'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

            '出荷日From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '出荷日To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

#If True Then       'ADD 2019/03/14 依頼番号 : 004183   【LMS】特定荷主機能_群馬大阪BC_三井化学 運賃請求1割増機能_計算差異 
            'タリフコード
            whereStr = .Item("TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_CD", whereStr, DBDataType.CHAR))
            End If
#End If
        End With

    End Sub

#End Region

#Region "SQL条件設定 営業所コード"

    ''' <summary>
    ''' 営業所コードのパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetNrsBrCdParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

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
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class
