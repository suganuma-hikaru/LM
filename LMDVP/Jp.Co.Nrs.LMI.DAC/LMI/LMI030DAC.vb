' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI030  : 請求データ作成 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "デュポン請求ファイルの検索"

#Region "デュポン請求ファイルの検索 SQL SELECT句"

    ''' <summary>
    ''' デュポン請求ファイル検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INTERFACE As String = " SELECT                                                                     " & vbNewLine _
                                                 & " INTER.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
                                                 & ",INTER.SEKY_YM                                 AS SEKY_YM                   " & vbNewLine _
                                                 & ",INTER.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
                                                 & ",INTER.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
                                                 & ",INTER.CUST_CD_S                               AS CUST_CD_S                 " & vbNewLine _
                                                 & ",INTER.CUST_CD_SS                              AS CUST_CD_SS                " & vbNewLine _
                                                 & ",INTER.DEPART                                  AS DEPART                    " & vbNewLine _
                                                 & ",INTER.SRC_CD                                  AS SRC_CD                    " & vbNewLine _
                                                 & ",INTER.FRB_CD                                  AS FRB_CD                    " & vbNewLine _
                                                 & ",INTER.MISK_CD                                 AS MISK_CD                   " & vbNewLine _
                                                 & ",INTER.TAX_KB                                  AS TAX_KB                    " & vbNewLine _
                                                 & ",INTER.SOUND                                   AS SOUND                     " & vbNewLine _
                                                 & ",INTER.BOND                                    AS BOND                      " & vbNewLine _
                                                 & ",INTER.TAX                                     AS TAX                       " & vbNewLine _
                                                 & ",INTER.SEKY_KMK                                AS SEKY_KMK                  " & vbNewLine _
                                                 & ",GL.AMOUNT                                     AS AMOUNT                    " & vbNewLine _
                                                 & ",GL.VAT_AMOUNT                                 AS VAT_AMOUNT                " & vbNewLine _
                                                 & ",GL.SOUND                                      AS SOUND_GL                  " & vbNewLine _
                                                 & ",GL.BOND                                       AS BOND_GL                   " & vbNewLine _
                                                 & ",GL.JIDO_FLAG                                  AS JIDO_FLAG                 " & vbNewLine _
                                                 & ",GL.SHUDO_FLAG                                 AS SHUDO_FLAG                " & vbNewLine _
                                                 & ",GL.NRS_BR_CD                                  AS NRS_BR_CD_GL              " & vbNewLine

#End Region

#Region "デュポン請求ファイルの検索 SQL FROM句"

    ''' <summary>
    ''' デュポン請求ファイル検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_INTERFACE As String = "FROM                                                                   " & vbNewLine _
                                                      & "$LM_TRN_DPN$..G_DUPONT_INTERFACE_TRS INTER                                 " & vbNewLine _
                                                      & "LEFT OUTER JOIN                                                        " & vbNewLine _
                                                      & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL GL                                          " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GL.NRS_BR_CD = INTER.NRS_BR_CD                                         " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GL.SEKY_YM = INTER.SEKY_YM                                             " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GL.DEPART = INTER.DEPART                                               " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GL.SEKY_KMK = INTER.SEKY_KMK                                           " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GL.SRC_CD = INTER.SRC_CD                                               " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GL.FRB_CD = INTER.FRB_CD                                               " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GL.MISK_CD = INTER.MISK_CD                                             " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GL.SYS_DEL_FLG = '0'                                                   " & vbNewLine

#End Region

#End Region

#Region "デュポン請求ファイルの削除"

#Region "デュポン請求ファイルの削除 SQL DELETE句"

    ''' <summary>
    ''' デュポン請求ファイル削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_INTERFACE As String = "DELETE FROM $LM_TRN_DPN$..G_DUPONT_INTERFACE_TRS " & vbNewLine

#End Region

#End Region

#Region "デュポン請求GLマスタの更新"

#Region "デュポン請求GLマスタの更新 SQL SET句"

    ''' <summary>
    ''' デュポン請求GLマスタの更新 SQL SET句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SET_GL As String = "UPDATE $LM_TRN_DPN$..G_DUPONT_SEKY_GL SET               " & vbNewLine _
                                              & "       AMOUNT           = @AMOUNT                   " & vbNewLine _
                                              & "     , VAT_AMOUNT       = @VAT_AMOUNT               " & vbNewLine _
                                              & "     , SOUND            = @SOUND                    " & vbNewLine _
                                              & "     , BOND             = @BOND                     " & vbNewLine _
                                              & "     , JIDO_FLAG        = ''                        " & vbNewLine _
                                              & "     , SYS_UPD_DATE     = @SYS_UPD_DATE             " & vbNewLine _
                                              & "     , SYS_UPD_TIME     = @SYS_UPD_TIME             " & vbNewLine _
                                              & "     , SYS_UPD_PGID     = @SYS_UPD_PGID             " & vbNewLine _
                                              & "     , SYS_UPD_USER     = @SYS_UPD_USER             " & vbNewLine

#End Region

#Region "デュポン請求GLマスタの更新 SQL WHERE句"

    ''' <summary>
    ''' デュポン請求GLマスタの更新 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_WHERE_GL As String = " WHERE                                            " & vbNewLine _
                                                 & "      NRS_BR_CD        = @NRS_BR_CD              " & vbNewLine _
                                                 & "  AND SEKY_YM          = @SEKY_YM                " & vbNewLine _
                                                 & "  AND DEPART           = @DEPART                 " & vbNewLine _
                                                 & "  AND SEKY_KMK         = @SEKY_KMK               " & vbNewLine _
                                                 & "  AND FRB_CD           = @FRB_CD                 " & vbNewLine _
                                                 & "  AND SRC_CD           = @SRC_CD                 " & vbNewLine _
                                                 & "  AND MISK_CD          = @MISK_CD                " & vbNewLine

#End Region

#End Region

#Region "デュポン請求GLマスタの削除"

#Region "デュポン請求GLマスタの削除 SQL DELETE句"

    ''' <summary>
    ''' デュポン請求ファイル削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_SEKYGL As String = "DELETE FROM $LM_TRN_DPN$..G_DUPONT_SEKY_GL " & vbNewLine

#End Region

#Region "デュポン請求GLマスタの削除 SQL WHERE句"

    ''' <summary>
    ''' デュポン請求ファイル削除 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WHERE_SEKYGL As String = "WHERE                                      " & vbNewLine _
                                                    & "      NRS_BR_CD      = @NRS_BR_CD          " & vbNewLine _
                                                    & "  AND SEKY_YM        = @SEKY_YM            " & vbNewLine _
                                                    & "  AND DEPART         = @DEPART             " & vbNewLine _
                                                    & "  AND SEKY_KMK       = @SEKY_KMK           " & vbNewLine _
                                                    & "  AND FRB_CD         = @FRB_CD             " & vbNewLine _
                                                    & "  AND SRC_CD         = @SRC_CD             " & vbNewLine _
                                                    & "  AND MISK_CD        = @MISK_CD            " & vbNewLine

#End Region

#End Region

#Region "保管料荷役明細印刷テーブルの検索"

#Region "保管料荷役明細印刷テーブルの検索 SQL SELECT句"

    'START YANAI 要望番号830
    '''' <summary>
    '''' 保管料荷役明細印刷テーブルの検索 SQL SELECT句(保管の場合)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_GMEISAI_HOKAN As String = " SELECT                                                                 " & vbNewLine _
    '                                           & " MEISAI.NRS_BR_CD                                AS NRS_BR_CD                 " & vbNewLine _
    '                                           & ",SUBSTRING(@SKYU_DATE_TO,1,6)                    AS SEKY_YM                   " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_L                                 AS CUST_CD_L                 " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_M                                 AS CUST_CD_M                 " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_S                                 AS CUST_CD_S                 " & vbNewLine _
    '                                           & ",GOODS.CUST_CD_SS                                AS CUST_CD_SS                " & vbNewLine _
    '                                           & ",CASE WHEN CUSTDETAILS.SET_NAIYO IS NULL THEN ''                              " & vbNewLine _
    '                                           & "      ELSE RIGHT('00' + CUSTDETAILS.SET_NAIYO ,2) END AS DEPART               " & vbNewLine _
    '                                           & ",GOODS.CUST_COST_CD2                             AS SRC_CD                    " & vbNewLine _
    '                                           & ",GOODS.CUST_COST_CD1                             AS FRB_CD                    " & vbNewLine _
    '                                           & ",''                                              AS MISK_CD                   " & vbNewLine _
    '                                           & ",MEISAI.TAX_KB                                   AS TAX_KB                    " & vbNewLine _
    '                                           & ",SUM(CASE WHEN '01' = MEISAI.TAX_KB                                               " & vbNewLine _
    '                                           & "      THEN MEISAI.SEKI_ARI_NB1 * MEISAI.STORAGE1 +                            " & vbNewLine _
    '                                           & "           MEISAI.SEKI_ARI_NB2 * MEISAI.STORAGE2 +                            " & vbNewLine _
    '                                           & "           MEISAI.SEKI_ARI_NB3 * MEISAI.STORAGE3                              " & vbNewLine _
    '                                           & "      ELSE '0'                                                                " & vbNewLine _
    '                                           & " END )                                            AS SOUND                     " & vbNewLine _
    '                                           & ",SUM(CASE WHEN '01' <> MEISAI.TAX_KB                                              " & vbNewLine _
    '                                           & "      THEN MEISAI.SEKI_ARI_NB1 * MEISAI.STORAGE1 +                            " & vbNewLine _
    '                                           & "           MEISAI.SEKI_ARI_NB2 * MEISAI.STORAGE2 +                            " & vbNewLine _
    '                                           & "           MEISAI.SEKI_ARI_NB3 * MEISAI.STORAGE3                              " & vbNewLine _
    '                                           & "      ELSE '0'                                                                " & vbNewLine _
    '                                           & " END )                                            AS BOND                      " & vbNewLine _
    '                                           & ",'0'                                             AS TAX                       " & vbNewLine _
    '                                           & ",'01'                                            AS SEKY_KMK                  " & vbNewLine
    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL SELECT句(保管の場合)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GMEISAI_HOKAN As String = " SELECT                                                                 " & vbNewLine _
                                               & " MEISAI.NRS_BR_CD                                AS NRS_BR_CD                 " & vbNewLine _
                                               & ",SUBSTRING(@SKYU_DATE_TO,1,6)                    AS SEKY_YM                   " & vbNewLine _
                                               & ",GOODS.CUST_CD_L                                 AS CUST_CD_L                 " & vbNewLine _
                                               & ",GOODS.CUST_CD_M                                 AS CUST_CD_M                 " & vbNewLine _
                                               & ",GOODS.CUST_CD_S                                 AS CUST_CD_S                 " & vbNewLine _
                                               & ",GOODS.CUST_CD_SS                                AS CUST_CD_SS                " & vbNewLine _
                                               & ",CASE WHEN CUSTDETAILS.SET_NAIYO IS NULL THEN ''                              " & vbNewLine _
                                               & "      ELSE RIGHT('00' + CUSTDETAILS.SET_NAIYO ,2) END AS DEPART               " & vbNewLine _
                                               & ",GOODS.CUST_COST_CD2                             AS SRC_CD                    " & vbNewLine _
                                               & ",GOODS.CUST_COST_CD1                             AS FRB_CD                    " & vbNewLine _
                                               & ",''                                              AS MISK_CD                   " & vbNewLine _
                                               & ",MEISAI.TAX_KB                                   AS TAX_KB                    " & vbNewLine _
                                               & ",SUM(CASE WHEN '01' = MEISAI.TAX_KB                                               " & vbNewLine _
                                               & "      THEN MEISAI.STORAGE_AMO_TTL                                             " & vbNewLine _
                                               & "      ELSE '0'                                                                " & vbNewLine _
                                               & " END )                                            AS SOUND                     " & vbNewLine _
                                               & ",SUM(CASE WHEN '01' <> MEISAI.TAX_KB                                              " & vbNewLine _
                                               & "      THEN MEISAI.STORAGE_AMO_TTL                                             " & vbNewLine _
                                               & "      ELSE '0'                                                                " & vbNewLine _
                                               & " END )                                            AS BOND                      " & vbNewLine _
                                               & ",'0'                                             AS TAX                       " & vbNewLine _
                                               & ",'01'                                            AS SEKY_KMK                  " & vbNewLine
    'END YANAI 要望番号830

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL SELECT句(荷役の場合)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GMEISAI_NIYAKU As String = " SELECT                                                                " & vbNewLine _
                                               & " MEISAI.NRS_BR_CD                                AS NRS_BR_CD                 " & vbNewLine _
                                               & ",SUBSTRING(@SKYU_DATE_TO,1,6)                    AS SEKY_YM                   " & vbNewLine _
                                               & ",GOODS.CUST_CD_L                                 AS CUST_CD_L                 " & vbNewLine _
                                               & ",GOODS.CUST_CD_M                                 AS CUST_CD_M                 " & vbNewLine _
                                               & ",GOODS.CUST_CD_S                                 AS CUST_CD_S                 " & vbNewLine _
                                               & ",GOODS.CUST_CD_SS                                AS CUST_CD_SS                " & vbNewLine _
                                               & ",CASE WHEN CUSTDETAILS.SET_NAIYO IS NULL THEN ''                              " & vbNewLine _
                                               & "      ELSE RIGHT('00' + CUSTDETAILS.SET_NAIYO ,2) END AS DEPART               " & vbNewLine _
                                               & ",GOODS.CUST_COST_CD2                             AS SRC_CD                    " & vbNewLine _
                                               & ",GOODS.CUST_COST_CD1                             AS FRB_CD                    " & vbNewLine _
                                               & ",''                                              AS MISK_CD                   " & vbNewLine _
                                               & ",MEISAI.TAX_KB                                   AS TAX_KB                    " & vbNewLine _
                                               & ",SUM(CASE WHEN '01' = MEISAI.TAX_KB                                           " & vbNewLine _
                                               & "      THEN MEISAI.HANDLING_AMO_TTL                                            " & vbNewLine _
                                               & "      ELSE '0'                                                                " & vbNewLine _
                                               & " END)                                            AS SOUND                     " & vbNewLine _
                                               & ",SUM(CASE WHEN '01' <> MEISAI.TAX_KB                                          " & vbNewLine _
                                               & "      THEN MEISAI.HANDLING_AMO_TTL                                            " & vbNewLine _
                                               & "      ELSE '0'                                                                " & vbNewLine _
                                               & " END)                                            AS BOND                      " & vbNewLine _
                                               & ",'0'                                             AS TAX                       " & vbNewLine _
                                               & ",'02'                                            AS SEKY_KMK                  " & vbNewLine

#End Region

#Region "保管料荷役明細印刷テーブルの検索 SQL FROM句"

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL FROM句1
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_GMEISAI1 As String = "FROM                                                                    " & vbNewLine _
                                                      & "$LM_TRN$..G_SEKY_MEISAI_PRT MEISAI                                         " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = MEISAI.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND GOODS.GOODS_CD_NRS = MEISAI.GOODS_CD_NRS                           " & vbNewLine

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL FROM句2
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_GMEISAI2 As String = "LEFT JOIN                                                               " & vbNewLine _
                                                      & "(SELECT CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,MIN(CUSTDETAILS.CUST_CD_EDA) AS CUST_CD_EDA                    " & vbNewLine _
                                                      & " FROM $LM_MST$..M_CUST_DETAILS CUSTDETAILS                             " & vbNewLine _
                                                      & " WHERE CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                      & "   AND CUSTDETAILS.SUB_KB = '01'                                       " & vbNewLine _
                                                      & " GROUP BY                                                              " & vbNewLine _
                                                      & "        CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & " ) CUSTDETAILS                                                         " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "CUSTDETAILS.NRS_BR_CD = GOODS.NRS_BR_CD                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "CUSTDETAILS.CUST_CD = GOODS.CUST_CD_L +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_M +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_S +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_SS                                 " & vbNewLine

    'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL FROM句2
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_GMEISAI3 As String = "LEFT JOIN                                                               " & vbNewLine _
                                                      & "(SELECT CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,MIN(CUSTDETAILS.CUST_CD_EDA) AS CUST_CD_EDA                    " & vbNewLine _
                                                      & " FROM $LM_MST$..M_CUST_DETAILS CUSTDETAILS                             " & vbNewLine _
                                                      & " WHERE CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                      & "   AND CUSTDETAILS.SUB_KB = '14'                                       " & vbNewLine _
                                                      & " GROUP BY                                                              " & vbNewLine _
                                                      & "        CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & " ) CUSTDETAILS2                                                        " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "CUSTDETAILS2.NRS_BR_CD = GOODS.NRS_BR_CD                               " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "CUSTDETAILS2.CUST_CD = GOODS.CUST_CD_L +                               " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_M +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_S +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_SS                                 " & vbNewLine
    'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_GMEISAI As String = "WHERE                                                                   " & vbNewLine

#End Region

#Region "保管料荷役明細印刷テーブルの検索 SQL GROUP BY句"

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_GMEISAI As String = "GROUP BY                                                                " & vbNewLine _
                                                      & " MEISAI.NRS_BR_CD                                                      " & vbNewLine _
                                                      & ",GOODS.CUST_CD_L                                                       " & vbNewLine _
                                                      & ",GOODS.CUST_CD_M                                                       " & vbNewLine _
                                                      & ",GOODS.CUST_CD_S                                                       " & vbNewLine _
                                                      & ",GOODS.CUST_CD_SS                                                      " & vbNewLine _
                                                      & ",CUSTDETAILS.SET_NAIYO                                                 " & vbNewLine _
                                                      & ",GOODS.CUST_COST_CD2                                                   " & vbNewLine _
                                                      & ",GOODS.CUST_COST_CD1                                                   " & vbNewLine _
                                                      & ",MEISAI.TAX_KB                                                         " & vbNewLine _
                                                      & ",MEISAI.SEKI_ARI_NB1                                                   " & vbNewLine _
                                                      & ",MEISAI.STORAGE1                                                       " & vbNewLine _
                                                      & ",MEISAI.SEKI_ARI_NB2                                                   " & vbNewLine _
                                                      & ",MEISAI.STORAGE2                                                       " & vbNewLine _
                                                      & ",MEISAI.SEKI_ARI_NB3                                                   " & vbNewLine _
                                                      & ",MEISAI.STORAGE3                                                       " & vbNewLine

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_GMEISAI_NIYAKU As String = "GROUP BY                                                                " & vbNewLine _
                                                          & " MEISAI.NRS_BR_CD                                                      " & vbNewLine _
                                                          & ",GOODS.CUST_CD_L                                                       " & vbNewLine _
                                                          & ",GOODS.CUST_CD_M                                                       " & vbNewLine _
                                                          & ",GOODS.CUST_CD_S                                                       " & vbNewLine _
                                                          & ",GOODS.CUST_CD_SS                                                      " & vbNewLine _
                                                          & ",CUSTDETAILS.SET_NAIYO                                                 " & vbNewLine _
                                                          & ",GOODS.CUST_COST_CD2                                                   " & vbNewLine _
                                                          & ",GOODS.CUST_COST_CD1                                                   " & vbNewLine _
                                                          & ",MEISAI.TAX_KB                                                         " & vbNewLine

#End Region

#Region "保管料荷役明細印刷テーブルの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_GMEISAI As String = "ORDER BY                                                                " & vbNewLine _
                                                      & " MEISAI.NRS_BR_CD                                                      " & vbNewLine _
                                                      & ",GOODS.CUST_CD_L                                                       " & vbNewLine _
                                                      & ",GOODS.CUST_CD_M                                                       " & vbNewLine _
                                                      & ",GOODS.CUST_CD_S                                                       " & vbNewLine _
                                                      & ",GOODS.CUST_CD_SS                                                      " & vbNewLine _
                                                      & ",CUSTDETAILS.SET_NAIYO                                                 " & vbNewLine _
                                                      & ",GOODS.CUST_COST_CD2                                                   " & vbNewLine _
                                                      & ",GOODS.CUST_COST_CD1                                                   " & vbNewLine _
                                                      & ",MEISAI.TAX_KB                                                         " & vbNewLine

#End Region

#Region "税率マスタの検索 SQL"

#Region "税率マスタの検索 SQL"

    ''' <summary>
    ''' 税率マスタの検索 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TAX As String = "   SELECT                                                                       " & vbNewLine _
                                            & "    BASE.KBN_CD   AS ZEI_CD                                                     " & vbNewLine _
                                            & "   ,BASE.TAX_RATE AS TAX_RATE                                                   " & vbNewLine _
                                            & "   FROM                                                                         " & vbNewLine _
                                            & "   (                                                                            " & vbNewLine _
                                            & "    SELECT                                                                      " & vbNewLine _
                                            & "    KB.KBN_CD                                                                   " & vbNewLine _
                                            & "   ,TAX.TAX_CD                                                                  " & vbNewLine _
                                            & "   ,TAX.TAX_RATE                                                                " & vbNewLine _
                                            & "   ,rank()OVER(partition by KB.KBN_CD order by TAX.START_DATE DESC) rnk         " & vbNewLine _
                                            & "    FROM $LM_MST$..M_TAX TAX                                                      " & vbNewLine _
                                            & "    LEFT JOIN $LM_MST$..Z_KBN KB                                                  " & vbNewLine _
                                            & "    on KB.KBN_GROUP_CD ='Z001'                                                  " & vbNewLine _
                                            & "    and KB.KBN_NM3 = TAX.TAX_CD                                                 " & vbNewLine _
                                            & "    WHERE                                                                       " & vbNewLine _
                                            & "    TAX.BALANCE_KBN = '01'                                                      " & vbNewLine _
                                            & "    AND TAX.START_DATE <= @INV_DATE_FROM                                            " & vbNewLine _
                                            & "   ) BASE                                                                       " & vbNewLine _
                                            & "   WHERE rnk = 1                                                                " & vbNewLine

#End Region

#End Region

#Region "区分マスタの検索 SQL"

#Region "簿外品条件追加荷主の検索 SQL"

    'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
    '''' <summary>
    '''' 簿外品条件追加荷主の検索 SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_KBN_OFB As String = " SELECT                                                                       " & vbNewLine _
    '                                           & " Z1.KBN_NM1                                    AS KBN_NM1                     " & vbNewLine _
    '                                           & " FROM $LM_MST$..Z_KBN Z1                                                      " & vbNewLine _
    '                                           & " WHERE                                                                        " & vbNewLine _
    '                                           & " Z1.KBN_GROUP_CD = 'B011'                                                     " & vbNewLine _
    '                                           & " AND                                                                          " & vbNewLine _
    '                                           & " Z1.KBN_NM1 = @CUST_CD_L                                                      " & vbNewLine
    'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる

#End Region

#End Region

#End Region

#Region "運賃テーブルの検索"

    'START YANAI 要望番号830
    '''' <summary>
    '''' 運賃テーブルの検索 SQL SELECT句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FUnchin_SELECT_MAIN As String = "    SELECT                                                                                                                        " & vbNewLine _
    '                                                        & "        MAIN2.NRS_BR_CD                                                                                                           " & vbNewLine _
    '                                                        & "       ,MAIN2.SEKY_YM                                                                                                              " & vbNewLine _
    '                                                        & "       ,MAIN2.CUST_CD_L                                                                                                           " & vbNewLine _
    '                                                        & "       ,MAIN2.CUST_CD_M                                                                                                           " & vbNewLine _
    '                                                        & "       ,MAIN2.CUST_CD_S                                                                                                           " & vbNewLine _
    '                                                        & "       ,MAIN2.CUST_CD_SS                                                                                                          " & vbNewLine _
    '                                                        & "       ,MAIN2.DEPART                                                                                                              " & vbNewLine _
    '                                                        & "       ,MAIN2.SRC_CD                                                                                                              " & vbNewLine _
    '                                                        & "       ,MAIN2.FRB_CD                                                                                                              " & vbNewLine _
    '                                                        & "       ,'' AS MISK_CD                                                                                                              " & vbNewLine _
    '                                                        & "       ,MAIN2.TAX_KB                                                                                                              " & vbNewLine _
    '                                                        & "       ,CASE WHEN MAIN2.TAX_KB IN ('01','04') THEN SUM(MAIN2.DECI_UNCHIN)                                                                                      " & vbNewLine _
    '                                                        & "             ELSE 0 END  AS SOUND                                                                                     " & vbNewLine _
    '                                                        & "       ,CASE WHEN MAIN2.TAX_KB IN ('02','03') THEN SUM(MAIN2.DECI_UNCHIN)                                                                                      " & vbNewLine _
    '                                                        & "             ELSE 0 END  AS BOND                                                                                     " & vbNewLine _
    '                                                        & "       ,'0' AS TAX                                                                                                              " & vbNewLine _
    '                                                        & "       ,'03' AS SEKY_KMK                                                                                                              " & vbNewLine _
    '                                                        & "    FROM                                                                                                                          " & vbNewLine _
    '                                                        & "    (                                                                                                                             " & vbNewLine _
    '                                                        & "       SELECT                                                                                                                     " & vbNewLine _
    '                                                        & "          BASE.NRS_BR_CD                                                                                                        " & vbNewLine _
    '                                                        & "          ,BASE.SEKY_YM                                                                                               " & vbNewLine _
    '                                                        & "          ,RIGHT('00' + MCD2.SET_NAIYO,2) AS DEPART                                                                                               " & vbNewLine _
    '                                                        & "          ,BASE.CUST_CD_L                                                                                                         " & vbNewLine _
    '                                                        & "          ,BASE.CUST_CD_M                                                                                                         " & vbNewLine _
    '                                                        & "          ,BASE.CUST_CD_S                                                                                                         " & vbNewLine _
    '                                                        & "          ,BASE.CUST_CD_SS                                                                                                        " & vbNewLine _
    '                                                        & "          ,MG.CUST_COST_CD2 AS SRC_CD                                                                                             " & vbNewLine _
    '                                                        & "          ,CASE WHEN MCD2.SET_NAIYO = 'B' AND                                                                                     " & vbNewLine _
    '                                                        & "                MG.CUST_COST_CD1 = '9988' THEN '9941'                                                                             " & vbNewLine _
    '                                                        & "                ELSE MG.CUST_COST_CD1 END AS FRB_CD                                                                               " & vbNewLine _
    '                                                        & "          ,BASE.DECI_UNCHIN                                                                                                       " & vbNewLine _
    '                                                        & "             + BASE.DECI_CITY_EXTC                                                                                                " & vbNewLine _
    '                                                        & "             + BASE.DECI_WINT_EXTC                                                                                                " & vbNewLine _
    '                                                        & "             + BASE.DECI_RELY_EXTC                                                                                                " & vbNewLine _
    '                                                        & "             + BASE.DECI_TOLL                                                                                                     " & vbNewLine _
    '                                                        & "             + BASE.DECI_INSU AS DECI_UNCHIN                                                                                      " & vbNewLine _
    '                                                        & "          ,BASE.TAX_KB                                                                                                            " & vbNewLine _
    '                                                        & "       FROM                                                                                                                       " & vbNewLine _
    '                                                        & "          (                                                                                                                       " & vbNewLine
    ''' <summary>
    ''' 運賃テーブルの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FUnchin_SELECT_MAIN As String = "    SELECT                                                                                                                        " & vbNewLine _
                                                            & "        MAIN2.NRS_BR_CD                                                                                                           " & vbNewLine _
                                                            & "       ,MAIN2.SEKY_YM                                                                                                              " & vbNewLine _
                                                            & "       ,MAIN2.CUST_CD_L                                                                                                           " & vbNewLine _
                                                            & "       ,MAIN2.CUST_CD_M                                                                                                           " & vbNewLine _
                                                            & "       ,MAIN2.CUST_CD_S                                                                                                           " & vbNewLine _
                                                            & "       ,MAIN2.CUST_CD_SS                                                                                                          " & vbNewLine _
                                                            & "       ,MAIN2.DEPART                                                                                                              " & vbNewLine _
                                                            & "       ,MAIN2.SRC_CD                                                                                                              " & vbNewLine _
                                                            & "       ,MAIN2.FRB_CD                                                                                                              " & vbNewLine _
                                                            & "       ,'' AS MISK_CD                                                                                                              " & vbNewLine _
                                                            & "       ,MAIN2.TAX_KB                                                                                                              " & vbNewLine _
                                                            & "       ,CASE WHEN MAIN2.TAX_KB IN ('01','04') THEN SUM(MAIN2.DECI_UNCHIN)                                                                                      " & vbNewLine _
                                                            & "             ELSE 0 END  AS SOUND                                                                                     " & vbNewLine _
                                                            & "       ,CASE WHEN MAIN2.TAX_KB IN ('02','03') THEN SUM(MAIN2.DECI_UNCHIN)                                                                                      " & vbNewLine _
                                                            & "             ELSE 0 END  AS BOND                                                                                     " & vbNewLine _
                                                            & "       ,'0' AS TAX                                                                                                              " & vbNewLine _
                                                            & "       ,'03' AS SEKY_KMK                                                                                                              " & vbNewLine _
                                                            & "       ,MAIN2.SEIQ_FIXED_FLAG                                                                                                              " & vbNewLine _
                                                            & "    FROM                                                                                                                          " & vbNewLine _
                                                            & "    (                                                                                                                             " & vbNewLine _
                                                            & "       SELECT                                                                                                                     " & vbNewLine _
                                                            & "          BASE.NRS_BR_CD                                                                                                        " & vbNewLine _
                                                            & "          ,BASE.SEKY_YM                                                                                               " & vbNewLine _
                                                            & "          ,RIGHT('00' + MCD2.SET_NAIYO,2) AS DEPART                                                                                               " & vbNewLine _
                                                            & "          ,BASE.CUST_CD_L                                                                                                         " & vbNewLine _
                                                            & "          ,BASE.CUST_CD_M                                                                                                         " & vbNewLine _
                                                            & "          ,BASE.CUST_CD_S                                                                                                         " & vbNewLine _
                                                            & "          ,BASE.CUST_CD_SS                                                                                                        " & vbNewLine _
                                                            & "          ,MG.CUST_COST_CD2 AS SRC_CD                                                                                             " & vbNewLine _
                                                            & "          ,CASE WHEN MCD2.SET_NAIYO = 'B' AND                                                                                     " & vbNewLine _
                                                            & "                MG.CUST_COST_CD1 = '9988' THEN '9941'                                                                             " & vbNewLine _
                                                            & "                ELSE MG.CUST_COST_CD1 END AS FRB_CD                                                                               " & vbNewLine _
                                                            & "          ,BASE.DECI_UNCHIN                                                                                                       " & vbNewLine _
                                                            & "             + BASE.DECI_CITY_EXTC                                                                                                " & vbNewLine _
                                                            & "             + BASE.DECI_WINT_EXTC                                                                                                " & vbNewLine _
                                                            & "             + BASE.DECI_RELY_EXTC                                                                                                " & vbNewLine _
                                                            & "             + BASE.DECI_TOLL                                                                                                     " & vbNewLine _
                                                            & "             + BASE.DECI_INSU AS DECI_UNCHIN                                                                                      " & vbNewLine _
                                                            & "          ,BASE.TAX_KB                                                                                                            " & vbNewLine _
                                                            & "          ,BASE.SEIQ_FIXED_FLAG                                                                                                   " & vbNewLine _
                                                            & "       FROM                                                                                                                       " & vbNewLine _
                                                            & "          (                                                                                                                       " & vbNewLine
    'END YANAI 要望番号830

    'START YANAI 要望番号830
    'Private Const SQL_SELECT_FUnchin_SELECT_OUTKA As String = "           SELECT                                                                                                                  " & vbNewLine _
    '                                                        & "             RANK() OVER (PARTITION BY OL.OUTKA_NO_L ORDER BY OL.OUTKA_NO_L,OS.OUTKA_NO_M + OS.OUTKA_NO_S ) AS NO                 " & vbNewLine _
    '                                                        & "             ,OL.NRS_BR_CD AS NRS_BR_CD                                                                                           " & vbNewLine _
    '                                                        & "             ,SUBSTRING(OL.OUTKA_PLAN_DATE,1,6) AS SEKY_YM                                                                                           " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_L                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_M                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_S                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_SS                                                                                                       " & vbNewLine _
    '                                                        & "             ,ZAI.OFB_KB AS OFB_KB                                                                                                " & vbNewLine _
    '                                                        & "             ,OM.GOODS_CD_NRS                                                                                                     " & vbNewLine _
    '                                                        & "             ,UT.DECI_UNCHIN                                                                                                      " & vbNewLine _
    '                                                        & "             ,UT.DECI_CITY_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_WINT_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_RELY_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_TOLL                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.DECI_INSU                                                                                                        " & vbNewLine _
    '                                                        & "             ,MCD2.SET_NAIYO AS DEPART                                                                                            " & vbNewLine _
    '                                                        & "             ,UT.TAX_KB                                                                                                           " & vbNewLine _
    '                                                        & "          FROM                                                                                                                    " & vbNewLine _
    '                                                        & "             $LM_TRN$..C_OUTKA_L OL                                                                                                 " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                                        " & vbNewLine _
    '                                                        & "             ON OL.NRS_BR_CD = UL.NRS_BR_CD                                                                                       " & vbNewLine _
    '                                                        & "             AND OL.OUTKA_NO_L = UL.INOUTKA_NO_L                                                                                  " & vbNewLine _
    '                                                        & "             AND UL.MOTO_DATA_KB = '20'                                                                                           " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT                                                                                    " & vbNewLine _
    '                                                        & "             ON UL.UNSO_NO_L = UT.UNSO_NO_L                                                                                       " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..C_OUTKA_M OM                                                                                       " & vbNewLine _
    '                                                        & "             ON OL.NRS_BR_CD = OM.NRS_BR_CD                                                                                       " & vbNewLine _
    '                                                        & "             AND OL.OUTKA_NO_L = OM.OUTKA_NO_L                                                                                    " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..C_OUTKA_S OS                                                                                       " & vbNewLine _
    '                                                        & "             ON OL.NRS_BR_CD = OS.NRS_BR_CD                                                                                       " & vbNewLine _
    '                                                        & "             AND OL.OUTKA_NO_L = OS.OUTKA_NO_L                                                                                    " & vbNewLine _
    '                                                        & "             AND OM.OUTKA_NO_M = OS.OUTKA_NO_M                                                                                    " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                                                      " & vbNewLine _
    '                                                        & "             ON OS.NRS_BR_CD = ZAI.NRS_BR_CD                                                                                      " & vbNewLine _
    '                                                        & "             AND OS.ZAI_REC_NO = ZAI.ZAI_REC_NO                                                                                   " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2                                                                                " & vbNewLine _
    '                                                        & "             ON MCD2.NRS_BR_CD = OL.NRS_BR_CD                                                                                     " & vbNewLine _
    '                                                        & "             AND MCD2.CUST_CD =                                                                                                   " & vbNewLine _
    '                                                        & "             (OL.CUST_CD_L +                                                                                                      " & vbNewLine _
    '                                                        & "              UT.CUST_CD_M +                                                                                                      " & vbNewLine _
    '                                                        & "              UT.CUST_CD_S +                                                                                                      " & vbNewLine _
    '                                                        & "              UT.CUST_CD_SS)                                                                                                      " & vbNewLine _
    '                                                        & "             AND MCD2.SUB_KB = '01'                                                                                               " & vbNewLine _
    '                                                        & "          WHERE                                                                                                                   " & vbNewLine _
    '                                                        & "             OL.SYS_DEL_FLG = '0'                                                                                                 " & vbNewLine _
    '                                                        & "             AND OM.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
    '                                                        & "             AND OS.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
    '                                                        & "             AND ZAI.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
    '                                                        & "             AND UL.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
    '                                                        & "             AND UT.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
    '                                                        & "             AND UT.DECI_UNCHIN <> 0                                                                                              " & vbNewLine _
    '                                                        & "             AND UT.SEIQ_FIXED_FLAG = '01'                                                                                        " & vbNewLine
    Private Const SQL_SELECT_FUnchin_SELECT_OUTKA As String = "           SELECT                                                                                                                  " & vbNewLine _
                                                            & "             RANK() OVER (PARTITION BY OL.OUTKA_NO_L ORDER BY OL.OUTKA_NO_L,OS.OUTKA_NO_M + OS.OUTKA_NO_S ) AS NO                 " & vbNewLine _
                                                            & "             ,OL.NRS_BR_CD AS NRS_BR_CD                                                                                           " & vbNewLine _
                                                            & "             ,SUBSTRING(OL.OUTKA_PLAN_DATE,1,6) AS SEKY_YM                                                                                           " & vbNewLine _
                                                            & "             ,UT.CUST_CD_L                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_M                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_S                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_SS                                                                                                       " & vbNewLine _
                                                            & "             ,ZAI.OFB_KB AS OFB_KB                                                                                                " & vbNewLine _
                                                            & "             ,OM.GOODS_CD_NRS                                                                                                     " & vbNewLine _
                                                            & "             ,UT.DECI_UNCHIN                                                                                                      " & vbNewLine _
                                                            & "             ,UT.DECI_CITY_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_WINT_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_RELY_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_TOLL                                                                                                        " & vbNewLine _
                                                            & "             ,UT.DECI_INSU                                                                                                        " & vbNewLine _
                                                            & "             ,MCD2.SET_NAIYO AS DEPART                                                                                            " & vbNewLine _
                                                            & "             ,UT.TAX_KB                                                                                                           " & vbNewLine _
                                                            & "             ,UT.SEIQ_FIXED_FLAG                                                                                                  " & vbNewLine _
                                                            & "          FROM                                                                                                                    " & vbNewLine _
                                                            & "             $LM_TRN$..C_OUTKA_L OL                                                                                                 " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                                        " & vbNewLine _
                                                            & "             ON OL.NRS_BR_CD = UL.NRS_BR_CD                                                                                       " & vbNewLine _
                                                            & "             AND OL.OUTKA_NO_L = UL.INOUTKA_NO_L                                                                                  " & vbNewLine _
                                                            & "             AND UL.MOTO_DATA_KB = '20'                                                                                           " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT                                                                                    " & vbNewLine _
                                                            & "             ON UL.UNSO_NO_L = UT.UNSO_NO_L                                                                                       " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..C_OUTKA_M OM                                                                                       " & vbNewLine _
                                                            & "             ON OL.NRS_BR_CD = OM.NRS_BR_CD                                                                                       " & vbNewLine _
                                                            & "             AND OL.OUTKA_NO_L = OM.OUTKA_NO_L                                                                                    " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..C_OUTKA_S OS                                                                                       " & vbNewLine _
                                                            & "             ON OL.NRS_BR_CD = OS.NRS_BR_CD                                                                                       " & vbNewLine _
                                                            & "             AND OL.OUTKA_NO_L = OS.OUTKA_NO_L                                                                                    " & vbNewLine _
                                                            & "             AND OM.OUTKA_NO_M = OS.OUTKA_NO_M                                                                                    " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                                                      " & vbNewLine _
                                                            & "             ON OS.NRS_BR_CD = ZAI.NRS_BR_CD                                                                                      " & vbNewLine _
                                                            & "             AND OS.ZAI_REC_NO = ZAI.ZAI_REC_NO                                                                                   " & vbNewLine _
                                                            & "             LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2                                                                                " & vbNewLine _
                                                            & "             ON MCD2.NRS_BR_CD = OL.NRS_BR_CD                                                                                     " & vbNewLine _
                                                            & "             AND MCD2.CUST_CD =                                                                                                   " & vbNewLine _
                                                            & "             (OL.CUST_CD_L +                                                                                                      " & vbNewLine _
                                                            & "              UT.CUST_CD_M +                                                                                                      " & vbNewLine _
                                                            & "              UT.CUST_CD_S +                                                                                                      " & vbNewLine _
                                                            & "              UT.CUST_CD_SS)                                                                                                      " & vbNewLine _
                                                            & "             AND MCD2.SUB_KB = '01'                                                                                               " & vbNewLine _
                                                            & "          WHERE                                                                                                                   " & vbNewLine _
                                                            & "             OL.SYS_DEL_FLG = '0'                                                                                                 " & vbNewLine _
                                                            & "             AND OM.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
                                                            & "             AND OS.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
                                                            & "             AND ZAI.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
                                                            & "             AND UL.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
                                                            & "             AND UT.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
                                                            & "             AND UT.DECI_UNCHIN <> 0                                                                                              " & vbNewLine _
    'END YANAI 要望番号830

    'START YANAI 要望番号830
    'Private Const SQL_SELECT_FUnchin_SELECT_INKA As String = "             --入荷                                                                                                                 " & vbNewLine _
    '                                                        & "          UNION ALL                                                                                                               " & vbNewLine _
    '                                                        & "          SELECT                                                                                                                  " & vbNewLine _
    '                                                        & "             RANK() OVER (PARTITION BY INL.INKA_NO_L ORDER BY INS.INKA_NO_M + INS.INKA_NO_S) AS NO                                " & vbNewLine _
    '                                                        & "             ,INL.NRS_BR_CD AS NRS_BR_CD                                                                                          " & vbNewLine _
    '                                                        & "             ,SUBSTRING(INL.INKA_DATE,1,6) AS SEKY_YM                                                                                           " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_L                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_M                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_S                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_SS                                                                                                       " & vbNewLine _
    '                                                        & "             ,INS.OFB_KB AS OFB_KB                                                                                                " & vbNewLine _
    '                                                        & "             ,INM.GOODS_CD_NRS                                                                                                    " & vbNewLine _
    '                                                        & "             ,UT.DECI_UNCHIN                                                                                                      " & vbNewLine _
    '                                                        & "             ,UT.DECI_CITY_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_WINT_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_RELY_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_TOLL                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.DECI_INSU                                                                                                        " & vbNewLine _
    '                                                        & "             ,MCD2.SET_NAIYO AS DEPART                                                                                            " & vbNewLine _
    '                                                        & "             ,UT.TAX_KB                                                                                                           " & vbNewLine _
    '                                                        & "          FROM                                                                                                                    " & vbNewLine _
    '                                                        & "             $LM_TRN$..B_INKA_L INL                                                                                                 " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                                        " & vbNewLine _
    '                                                        & "             ON INL.NRS_BR_CD = UL.NRS_BR_CD                                                                                      " & vbNewLine _
    '                                                        & "             AND INL.INKA_NO_L = UL.INOUTKA_NO_L                                                                                  " & vbNewLine _
    '                                                        & "             AND UL.MOTO_DATA_KB = '10'                                                                                           " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT                                                                                    " & vbNewLine _
    '                                                        & "             ON UL.UNSO_NO_L = UT.UNSO_NO_L                                                                                       " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..B_INKA_M INM                                                                                       " & vbNewLine _
    '                                                        & "             ON INL.NRS_BR_CD = INM.NRS_BR_CD                                                                                     " & vbNewLine _
    '                                                        & "             AND INL.INKA_NO_L = INM.INKA_NO_L                                                                                    " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..B_INKA_S INS                                                                                       " & vbNewLine _
    '                                                        & "             ON INM.NRS_BR_CD = INS.NRS_BR_CD                                                                                     " & vbNewLine _
    '                                                        & "             AND INM.INKA_NO_L = INS.INKA_NO_L                                                                                    " & vbNewLine _
    '                                                        & "             AND INM.INKA_NO_M = INS.INKA_NO_M                                                                                    " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2                                                                                " & vbNewLine _
    '                                                        & "             ON MCD2.NRS_BR_CD = INL.NRS_BR_CD                                                                                    " & vbNewLine _
    '                                                        & "             AND MCD2.CUST_CD = (INL.CUST_CD_L +                                                                                  " & vbNewLine _
    '                                                        & "                                 UT.CUST_CD_M +                                                                                   " & vbNewLine _
    '                                                        & "                                 UT.CUST_CD_S +                                                                                   " & vbNewLine _
    '                                                        & "                                 UT.CUST_CD_SS)                                                                                   " & vbNewLine _
    '                                                        & "             AND MCD2.SUB_KB = '01'                                                                                               " & vbNewLine _
    '                                                        & "          WHERE                                                                                                                   " & vbNewLine _
    '                                                        & "             INL.SYS_DEL_FLG = '0'                                                                                                " & vbNewLine _
    '                                                        & "             AND INM.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
    '                                                        & "             AND INS.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
    '                                                        & "             AND UL.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
    '                                                        & "             AND UT.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
    '                                                        & "             AND UT.DECI_UNCHIN <> 0                                                                                              " & vbNewLine _
    '                                                        & "             AND UT.SEIQ_FIXED_FLAG = '01'                                                                                        " & vbNewLine _
    '                                                        & "             AND UL.MOTO_DATA_KB = '40'                                                                                           " & vbNewLine
    Private Const SQL_SELECT_FUnchin_SELECT_INKA As String = "             --入荷                                                                                                                 " & vbNewLine _
                                                            & "          UNION ALL                                                                                                               " & vbNewLine _
                                                            & "          SELECT                                                                                                                  " & vbNewLine _
                                                            & "             RANK() OVER (PARTITION BY INL.INKA_NO_L ORDER BY INS.INKA_NO_M + INS.INKA_NO_S) AS NO                                " & vbNewLine _
                                                            & "             ,INL.NRS_BR_CD AS NRS_BR_CD                                                                                          " & vbNewLine _
                                                            & "             ,SUBSTRING(INL.INKA_DATE,1,6) AS SEKY_YM                                                                                           " & vbNewLine _
                                                            & "             ,UT.CUST_CD_L                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_M                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_S                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_SS                                                                                                       " & vbNewLine _
                                                            & "             ,INS.OFB_KB AS OFB_KB                                                                                                " & vbNewLine _
                                                            & "             ,INM.GOODS_CD_NRS                                                                                                    " & vbNewLine _
                                                            & "             ,UT.DECI_UNCHIN                                                                                                      " & vbNewLine _
                                                            & "             ,UT.DECI_CITY_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_WINT_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_RELY_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_TOLL                                                                                                        " & vbNewLine _
                                                            & "             ,UT.DECI_INSU                                                                                                        " & vbNewLine _
                                                            & "             ,MCD2.SET_NAIYO AS DEPART                                                                                            " & vbNewLine _
                                                            & "             ,UT.TAX_KB                                                                                                           " & vbNewLine _
                                                            & "             ,UT.SEIQ_FIXED_FLAG                                                                                                  " & vbNewLine _
                                                            & "          FROM                                                                                                                    " & vbNewLine _
                                                            & "             $LM_TRN$..B_INKA_L INL                                                                                                 " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                                        " & vbNewLine _
                                                            & "             ON INL.NRS_BR_CD = UL.NRS_BR_CD                                                                                      " & vbNewLine _
                                                            & "             AND INL.INKA_NO_L = UL.INOUTKA_NO_L                                                                                  " & vbNewLine _
                                                            & "             AND UL.MOTO_DATA_KB = '10'                                                                                           " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT                                                                                    " & vbNewLine _
                                                            & "             ON UL.UNSO_NO_L = UT.UNSO_NO_L                                                                                       " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..B_INKA_M INM                                                                                       " & vbNewLine _
                                                            & "             ON INL.NRS_BR_CD = INM.NRS_BR_CD                                                                                     " & vbNewLine _
                                                            & "             AND INL.INKA_NO_L = INM.INKA_NO_L                                                                                    " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..B_INKA_S INS                                                                                       " & vbNewLine _
                                                            & "             ON INM.NRS_BR_CD = INS.NRS_BR_CD                                                                                     " & vbNewLine _
                                                            & "             AND INM.INKA_NO_L = INS.INKA_NO_L                                                                                    " & vbNewLine _
                                                            & "             AND INM.INKA_NO_M = INS.INKA_NO_M                                                                                    " & vbNewLine _
                                                            & "             LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2                                                                                " & vbNewLine _
                                                            & "             ON MCD2.NRS_BR_CD = INL.NRS_BR_CD                                                                                    " & vbNewLine _
                                                            & "             AND MCD2.CUST_CD = (INL.CUST_CD_L +                                                                                  " & vbNewLine _
                                                            & "                                 UT.CUST_CD_M +                                                                                   " & vbNewLine _
                                                            & "                                 UT.CUST_CD_S +                                                                                   " & vbNewLine _
                                                            & "                                 UT.CUST_CD_SS)                                                                                   " & vbNewLine _
                                                            & "             AND MCD2.SUB_KB = '01'                                                                                               " & vbNewLine _
                                                            & "          WHERE                                                                                                                   " & vbNewLine _
                                                            & "             INL.SYS_DEL_FLG = '0'                                                                                                " & vbNewLine _
                                                            & "             AND INM.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
                                                            & "             AND INS.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
                                                            & "             AND UL.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
                                                            & "             AND UT.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
                                                            & "             AND UT.DECI_UNCHIN <> 0                                                                                              " & vbNewLine _
                                                            & "             AND UL.MOTO_DATA_KB = '10'                                                                                           " & vbNewLine
    'END YANAI 要望番号830

    'START YANAI 要望番号830
    'Private Const SQL_SELECT_FUnchin_SELECT_UNSO As String = "          --運送                                                                                                                    " & vbNewLine _
    '                                                        & "          UNION ALL                                                                                                               " & vbNewLine _
    '                                                        & "          SELECT                                                                                                                  " & vbNewLine _
    '                                                        & "             RANK() OVER (PARTITION BY UL.UNSO_NO_L ORDER BY UM.UNSO_NO_M) AS NO                                                  " & vbNewLine _
    '                                                        & "             ,UL.NRS_BR_CD AS NRS_BR_CD                                                                                           " & vbNewLine _
    '                                                        & "             ,SUBSTRING(UL.OUTKA_PLAN_DATE,1,6) AS SEKY_YM                                                                                           " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_L                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_M                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_S                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.CUST_CD_SS                                                                                                       " & vbNewLine _
    '                                                        & "             ,'01' AS OFB_KB                                                                                                      " & vbNewLine _
    '                                                        & "             ,UM.GOODS_CD_NRS                                                                                                     " & vbNewLine _
    '                                                        & "             ,UT.DECI_UNCHIN                                                                                                      " & vbNewLine _
    '                                                        & "             ,UT.DECI_CITY_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_WINT_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_RELY_EXTC                                                                                                   " & vbNewLine _
    '                                                        & "             ,UT.DECI_TOLL                                                                                                        " & vbNewLine _
    '                                                        & "             ,UT.DECI_INSU                                                                                                        " & vbNewLine _
    '                                                        & "             ,MCD2.SET_NAIYO AS DEPART                                                                                            " & vbNewLine _
    '                                                        & "             ,UT.TAX_KB                                                                                                           " & vbNewLine _
    '                                                        & "          FROM                                                                                                                    " & vbNewLine _
    '                                                        & "             $LM_TRN$..F_UNSO_L UL                                                                                                  " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..F_UNSO_M UM                                                                                        " & vbNewLine _
    '                                                        & "             ON UL.UNSO_NO_L = UM.UNSO_NO_L                                                                                       " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT                                                                                    " & vbNewLine _
    '                                                        & "             ON UM.UNSO_NO_L = UT.UNSO_NO_L                                                                                       " & vbNewLine _
    '                                                        & "             AND UM.UNSO_NO_M = UT.UNSO_NO_M                                                                                      " & vbNewLine _
    '                                                        & "             LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2                                                                                " & vbNewLine _
    '                                                        & "             ON MCD2.NRS_BR_CD = UL.NRS_BR_CD                                                                                     " & vbNewLine _
    '                                                        & "             AND MCD2.CUST_CD =                                                                                                   " & vbNewLine _
    '                                                        & "             (UT.CUST_CD_L +                                                                                                      " & vbNewLine _
    '                                                        & "              UT.CUST_CD_M +                                                                                                      " & vbNewLine _
    '                                                        & "              UT.CUST_CD_S +                                                                                                      " & vbNewLine _
    '                                                        & "              UT.CUST_CD_SS)                                                                                                      " & vbNewLine _
    '                                                        & "             AND MCD2.SUB_KB = '01'                                                                                               " & vbNewLine _
    '                                                        & "          WHERE                                                                                                                   " & vbNewLine _
    '                                                        & "             UL.SYS_DEL_FLG = '0'                                                                                                 " & vbNewLine _
    '                                                        & "             AND UT.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
    '                                                        & "             AND UL.MOTO_DATA_KB = '40'                                                                                             " & vbNewLine _
    '                                                        & "             AND UT.DECI_UNCHIN <> 0                                                                                              " & vbNewLine _
    '                                                        & "             AND UT.SEIQ_FIXED_FLAG = '01'                                                                                        " & vbNewLine
    Private Const SQL_SELECT_FUnchin_SELECT_UNSO As String = "          --運送                                                                                                                    " & vbNewLine _
                                                            & "          UNION ALL                                                                                                               " & vbNewLine _
                                                            & "          SELECT                                                                                                                  " & vbNewLine _
                                                            & "             RANK() OVER (PARTITION BY UL.UNSO_NO_L ORDER BY UM.UNSO_NO_M) AS NO                                                  " & vbNewLine _
                                                            & "             ,UL.NRS_BR_CD AS NRS_BR_CD                                                                                           " & vbNewLine _
                                                            & "             ,SUBSTRING(UL.OUTKA_PLAN_DATE,1,6) AS SEKY_YM                                                                                           " & vbNewLine _
                                                            & "             ,UT.CUST_CD_L                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_M                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_S                                                                                                        " & vbNewLine _
                                                            & "             ,UT.CUST_CD_SS                                                                                                       " & vbNewLine _
                                                            & "             ,'01' AS OFB_KB                                                                                                      " & vbNewLine _
                                                            & "             ,UM.GOODS_CD_NRS                                                                                                     " & vbNewLine _
                                                            & "             ,UT.DECI_UNCHIN                                                                                                      " & vbNewLine _
                                                            & "             ,UT.DECI_CITY_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_WINT_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_RELY_EXTC                                                                                                   " & vbNewLine _
                                                            & "             ,UT.DECI_TOLL                                                                                                        " & vbNewLine _
                                                            & "             ,UT.DECI_INSU                                                                                                        " & vbNewLine _
                                                            & "             ,MCD2.SET_NAIYO AS DEPART                                                                                            " & vbNewLine _
                                                            & "             ,UT.TAX_KB                                                                                                           " & vbNewLine _
                                                            & "             ,UT.SEIQ_FIXED_FLAG                                                                                                  " & vbNewLine _
                                                            & "          FROM                                                                                                                    " & vbNewLine _
                                                            & "             $LM_TRN$..F_UNSO_L UL                                                                                                  " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..F_UNSO_M UM                                                                                        " & vbNewLine _
                                                            & "             ON UL.UNSO_NO_L = UM.UNSO_NO_L                                                                                       " & vbNewLine _
                                                            & "             LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT                                                                                    " & vbNewLine _
                                                            & "             ON UM.UNSO_NO_L = UT.UNSO_NO_L                                                                                       " & vbNewLine _
                                                            & "             AND UM.UNSO_NO_M = UT.UNSO_NO_M                                                                                      " & vbNewLine _
                                                            & "             LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2                                                                                " & vbNewLine _
                                                            & "             ON MCD2.NRS_BR_CD = UL.NRS_BR_CD                                                                                     " & vbNewLine _
                                                            & "             AND MCD2.CUST_CD =                                                                                                   " & vbNewLine _
                                                            & "             (UT.CUST_CD_L +                                                                                                      " & vbNewLine _
                                                            & "              UT.CUST_CD_M +                                                                                                      " & vbNewLine _
                                                            & "              UT.CUST_CD_S +                                                                                                      " & vbNewLine _
                                                            & "              UT.CUST_CD_SS)                                                                                                      " & vbNewLine _
                                                            & "             AND MCD2.SUB_KB = '01'                                                                                               " & vbNewLine _
                                                            & "          WHERE                                                                                                                   " & vbNewLine _
                                                            & "             UL.SYS_DEL_FLG = '0'                                                                                                 " & vbNewLine _
                                                            & "             AND UT.SYS_DEL_FLG = '0'                                                                                             " & vbNewLine _
                                                            & "             AND UL.MOTO_DATA_KB = '40'                                                                                             " & vbNewLine _
                                                            & "             AND UT.DECI_UNCHIN <> 0                                                                                              " & vbNewLine _
    'END YANAI 要望番号830

    Private Const SQL_SELECT_FUnchin_JOIN As String = "          ) BASE                                                                                                                  " & vbNewLine _
                                                    & "             LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD1                                                                                " & vbNewLine _
                                                    & "             ON MCD1.NRS_BR_CD = BASE.NRS_BR_CD                                                                                   " & vbNewLine _
                                                    & "             AND MCD1.CUST_CD =                                                                                                   " & vbNewLine _
                                                    & "                (BASE.CUST_CD_L                                                                                                   " & vbNewLine _
                                                    & "                + BASE.CUST_CD_M                                                                                                  " & vbNewLine _
                                                    & "                + BASE.CUST_CD_S                                                                                                  " & vbNewLine _
                                                    & "                + BASE.CUST_CD_SS)                                                                                                " & vbNewLine _
                                                    & "             AND MCD1.SUB_KB = '14'                                                                                               " & vbNewLine _
                                                    & "             LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2                                                                                " & vbNewLine _
                                                    & "             ON MCD2.NRS_BR_CD = BASE.NRS_BR_CD                                                                                   " & vbNewLine _
                                                    & "             AND MCD2.CUST_CD =                                                                                                   " & vbNewLine _
                                                    & "                (BASE.CUST_CD_L                                                                                                   " & vbNewLine _
                                                    & "                + BASE.CUST_CD_M                                                                                                  " & vbNewLine _
                                                    & "                + BASE.CUST_CD_S                                                                                                  " & vbNewLine _
                                                    & "                + BASE.CUST_CD_SS)                                                                                                " & vbNewLine _
                                                    & "             AND MCD2.SUB_KB = '01'                                                                                               " & vbNewLine _
                                                    & "             LEFT JOIN $LM_MST$.. M_GOODS MG                                                                                        " & vbNewLine _
                                                    & "             ON BASE.NRS_BR_CD = MG.NRS_BR_CD                                                                                     " & vbNewLine _
                                                    & "             AND BASE.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                              " & vbNewLine _
                                                    & "          WHERE                                                                                                                   " & vbNewLine _
                                                    & "             BASE.NO = 1                                                                                                          " & vbNewLine _
                                                    & "             AND (BASE.OFB_KB = '01' AND ISNULL(MCD1.SET_NAIYO,'') <> '01')                                                                                               " & vbNewLine

    'START YANAI 要望番号830
    'Private Const SQL_SELECT_FUnchin_GROUPBY As String = "    ) MAIN2                                                                                                                       " & vbNewLine _
    '                                                      & "    GROUP BY                                                                                                                      " & vbNewLine _
    '                                                      & "        MAIN2.NRS_BR_CD                                                                                                           " & vbNewLine _
    '                                                      & "       ,MAIN2.SEKY_YM                                                                                                              " & vbNewLine _
    '                                                      & "       ,MAIN2.DEPART                                                                                                              " & vbNewLine _
    '                                                      & "       ,MAIN2.CUST_CD_L                                                                                                           " & vbNewLine _
    '                                                      & "       ,MAIN2.CUST_CD_M                                                                                                           " & vbNewLine _
    '                                                      & "       ,MAIN2.CUST_CD_S                                                                                                           " & vbNewLine _
    '                                                      & "       ,MAIN2.CUST_CD_SS                                                                                                          " & vbNewLine _
    '                                                      & "       ,MAIN2.SRC_CD                                                                                                              " & vbNewLine _
    '                                                      & "       ,MAIN2.FRB_CD                                                                                                              " & vbNewLine _
    '                                                      & "       ,MAIN2.TAX_KB                                                                                                              " & vbNewLine _
    '                                                      & "    ORDER BY                                                                                                                      " & vbNewLine _
    '                                                      & "        MAIN2.SEKY_YM                                                                                                              " & vbNewLine _
    '                                                      & "       ,MAIN2.NRS_BR_CD                                                                                                           " & vbNewLine _
    '                                                      & "       ,MAIN2.DEPART                                                                                                              " & vbNewLine _
    '                                                      & "       ,MAIN2.CUST_CD_L                                                                                                           " & vbNewLine _
    '                                                      & "       ,MAIN2.CUST_CD_M                                                                                                           " & vbNewLine _
    '                                                      & "       ,MAIN2.CUST_CD_S                                                                                                           " & vbNewLine _
    '                                                      & "       ,MAIN2.CUST_CD_SS                                                                                                          " & vbNewLine _
    '                                                      & "       ,MAIN2.SRC_CD                                                                                                              " & vbNewLine _
    '                                                      & "       ,MAIN2.FRB_CD                                                                                                              " & vbNewLine
    Private Const SQL_SELECT_FUnchin_GROUPBY As String = "    ) MAIN2                                                                                                                       " & vbNewLine _
                                                          & "    GROUP BY                                                                                                                      " & vbNewLine _
                                                          & "        MAIN2.NRS_BR_CD                                                                                                           " & vbNewLine _
                                                          & "       ,MAIN2.SEKY_YM                                                                                                              " & vbNewLine _
                                                          & "       ,MAIN2.DEPART                                                                                                              " & vbNewLine _
                                                          & "       ,MAIN2.CUST_CD_L                                                                                                           " & vbNewLine _
                                                          & "       ,MAIN2.CUST_CD_M                                                                                                           " & vbNewLine _
                                                          & "       ,MAIN2.CUST_CD_S                                                                                                           " & vbNewLine _
                                                          & "       ,MAIN2.CUST_CD_SS                                                                                                          " & vbNewLine _
                                                          & "       ,MAIN2.SRC_CD                                                                                                              " & vbNewLine _
                                                          & "       ,MAIN2.FRB_CD                                                                                                              " & vbNewLine _
                                                          & "       ,MAIN2.TAX_KB                                                                                                              " & vbNewLine _
                                                          & "       ,MAIN2.SEIQ_FIXED_FLAG                                                                                                     " & vbNewLine _
                                                          & "    ORDER BY                                                                                                                      " & vbNewLine _
                                                          & "        MAIN2.SEKY_YM                                                                                                              " & vbNewLine _
                                                          & "       ,MAIN2.NRS_BR_CD                                                                                                           " & vbNewLine _
                                                          & "       ,MAIN2.DEPART                                                                                                              " & vbNewLine _
                                                          & "       ,MAIN2.CUST_CD_L                                                                                                           " & vbNewLine _
                                                          & "       ,MAIN2.CUST_CD_M                                                                                                           " & vbNewLine _
                                                          & "       ,MAIN2.CUST_CD_S                                                                                                           " & vbNewLine _
                                                          & "       ,MAIN2.CUST_CD_SS                                                                                                          " & vbNewLine _
                                                          & "       ,MAIN2.SRC_CD                                                                                                              " & vbNewLine _
                                                          & "       ,MAIN2.FRB_CD                                                                                                              " & vbNewLine
    'END YANAI 要望番号830


#Region "運賃テーブルの検索 SQL FROM句"

#End Region

#Region "運賃テーブルの検索 SQL GROUP BY句"

#End Region

#Region "運賃テーブルの検索 SQL ORDER BY句"

#End Region

#End Region

#Region "デュポン請求ファイルの新規作成"

#Region "デュポン請求ファイルの新規作成 SQL INSERT句"

    ''' <summary>
    ''' デュポン請求ファイル新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INTERFACE As String = "INSERT INTO $LM_TRN_DPN$..G_DUPONT_INTERFACE_TRS       " & vbNewLine _
                                                 & " ( 		                                           " & vbNewLine _
                                                 & " NRS_BR_CD,                                        " & vbNewLine _
                                                 & " SEKY_YM,                                          " & vbNewLine _
                                                 & " CUST_CD_L,                                        " & vbNewLine _
                                                 & " CUST_CD_M,                                        " & vbNewLine _
                                                 & " CUST_CD_S,                                        " & vbNewLine _
                                                 & " CUST_CD_SS,                                       " & vbNewLine _
                                                 & " DEPART,                                           " & vbNewLine _
                                                 & " SEKY_KMK,                                         " & vbNewLine _
                                                 & " FRB_CD,                                           " & vbNewLine _
                                                 & " SRC_CD,                                           " & vbNewLine _
                                                 & " MISK_CD,                                          " & vbNewLine _
                                                 & " TAX_KB,                                           " & vbNewLine _
                                                 & " SOUND,                                            " & vbNewLine _
                                                 & " BOND,                                             " & vbNewLine _
                                                 & " TAX,                                              " & vbNewLine _
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
                                                 & " @SEKY_YM,                                         " & vbNewLine _
                                                 & " @CUST_CD_L,                                       " & vbNewLine _
                                                 & " @CUST_CD_M,                                       " & vbNewLine _
                                                 & " @CUST_CD_S,                                       " & vbNewLine _
                                                 & " @CUST_CD_SS,                                      " & vbNewLine _
                                                 & " @DEPART,                                          " & vbNewLine _
                                                 & " @SEKY_KMK,                                        " & vbNewLine _
                                                 & " @FRB_CD,                                          " & vbNewLine _
                                                 & " @SRC_CD,                                          " & vbNewLine _
                                                 & " @MISK_CD,                                         " & vbNewLine _
                                                 & " @TAX_KB,                                          " & vbNewLine _
                                                 & " @SOUND,                                           " & vbNewLine _
                                                 & " @BOND,                                            " & vbNewLine _
                                                 & " @TAX,                                             " & vbNewLine _
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

#Region "デュポン請求GLマスタの検索"

#Region "デュポン請求GLマスタの検索 SQL SELECT句"

    ''' <summary>
    ''' デュポン請求GLマスタ検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SEKYGL As String = " SELECT                                                                        " & vbNewLine _
                                              & " GL.NRS_BR_CD                                     AS NRS_BR_CD                 " & vbNewLine _
                                              & ",GL.SEKY_YM                                       AS SEKY_YM                   " & vbNewLine _
                                              & ",GL.DEPART                                        AS DEPART                    " & vbNewLine _
                                              & ",GL.SEKY_KMK                                      AS SEKY_KMK                  " & vbNewLine _
                                              & ",GL.FRB_CD                                        AS FRB_CD                    " & vbNewLine _
                                              & ",GL.SRC_CD                                        AS SRC_CD                    " & vbNewLine _
                                              & ",GL.COST_CENTER                                   AS COST_CENTER               " & vbNewLine _
                                              & ",GL.MISK_CD                                       AS MISK_CD                   " & vbNewLine _
                                              & ",GL.DEST_CTY                                      AS DEST_CTY                  " & vbNewLine _
                                              & ",GL.AMOUNT                                        AS AMOUNT                    " & vbNewLine _
                                              & ",GL.VAT_AMOUNT                                    AS VAT_AMOUNT                " & vbNewLine _
                                              & ",GL.SOUND                                         AS SOUND                     " & vbNewLine _
                                              & ",GL.BOND                                          AS BOND                      " & vbNewLine _
                                              & ",GL.JIDO_FLAG                                     AS JIDO_FLAG                 " & vbNewLine _
                                              & ",GL.SHUDO_FLAG                                    AS SHUDO_FLAG                " & vbNewLine

#End Region

#Region "デュポン請求GLマスタの検索 SQL FROM句"

    ''' <summary>
    ''' デュポン請求GLマスタの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SEKYGL As String = "FROM                                                                      " & vbNewLine _
                                                   & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL GL                                             " & vbNewLine

#End Region

#Region "デュポン請求ファイルの検索 SQL WHERE句"

    ''' <summary>
    ''' デュポン請求ファイル検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_SEKYGL As String = "WHERE                                                                    " & vbNewLine _
                                                    & "GL.NRS_BR_CD = @NRS_BR_CD                                                " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "GL.SEKY_YM = @SEKY_YM                                                    " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "GL.DEPART = @DEPART                                                      " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "GL.SEKY_KMK = @SEKY_KMK                                                  " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "GL.SRC_CD = @SRC_CD                                                      " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "GL.FRB_CD = @FRB_CD                                                      " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "GL.MISK_CD = @MISK_CD                                                    " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "GL.SYS_DEL_FLG = '0'                                                     " & vbNewLine

#End Region

#End Region

#Region "デュポン請求GLマスタの新規作成"

#Region "デュポン請求GLマスタの新規作成 SQL INSERT句"

    ''' <summary>
    ''' デュポン請求GLマスタ新規追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SEKYGL As String = "INSERT INTO $LM_TRN_DPN$..G_DUPONT_SEKY_GL                " & vbNewLine _
                                              & " ( 		                                           " & vbNewLine _
                                              & " NRS_BR_CD,                                           " & vbNewLine _
                                              & " SEKY_YM,                                             " & vbNewLine _
                                              & " DEPART,                                              " & vbNewLine _
                                              & " SEKY_KMK,                                            " & vbNewLine _
                                              & " FRB_CD,                                              " & vbNewLine _
                                              & " SRC_CD,                                              " & vbNewLine _
                                              & " COST_CENTER,                                         " & vbNewLine _
                                              & " MISK_CD,                                             " & vbNewLine _
                                              & " AMOUNT,                                              " & vbNewLine _
                                              & " VAT_AMOUNT,                                          " & vbNewLine _
                                              & " SOUND,                                               " & vbNewLine _
                                              & " BOND,                                                " & vbNewLine _
                                              & " JIDO_FLAG,                                           " & vbNewLine _
                                              & " SHUDO_FLAG,                                          " & vbNewLine _
                                              & " SYS_ENT_DATE,                                        " & vbNewLine _
                                              & " SYS_ENT_TIME,                                        " & vbNewLine _
                                              & " SYS_ENT_PGID,                                        " & vbNewLine _
                                              & " SYS_ENT_USER,                                        " & vbNewLine _
                                              & " SYS_UPD_DATE,                                        " & vbNewLine _
                                              & " SYS_UPD_TIME,                                        " & vbNewLine _
                                              & " SYS_UPD_PGID,                                        " & vbNewLine _
                                              & " SYS_UPD_USER,                                        " & vbNewLine _
                                              & " SYS_DEL_FLG                                          " & vbNewLine _
                                              & " ) VALUES (                                           " & vbNewLine _
                                              & " @NRS_BR_CD,                                          " & vbNewLine _
                                              & " @SEKY_YM,                                            " & vbNewLine _
                                              & " @DEPART,                                             " & vbNewLine _
                                              & " @SEKY_KMK,                                           " & vbNewLine _
                                              & " @FRB_CD,                                             " & vbNewLine _
                                              & " @SRC_CD,                                             " & vbNewLine _
                                              & " @COST_CENTER,                                        " & vbNewLine _
                                              & " @MISK_CD,                                            " & vbNewLine _
                                              & " @AMOUNT,                                             " & vbNewLine _
                                              & " @VAT_AMOUNT,                                         " & vbNewLine _
                                              & " @SOUND,                                              " & vbNewLine _
                                              & " @BOND,                                               " & vbNewLine _
                                              & " @JIDO_FLAG,                                          " & vbNewLine _
                                              & " @SHUDO_FLAG,                                         " & vbNewLine _
                                              & " @SYS_ENT_DATE,                                       " & vbNewLine _
                                              & " @SYS_ENT_TIME,                                       " & vbNewLine _
                                              & " @SYS_ENT_PGID,                                       " & vbNewLine _
                                              & " @SYS_ENT_USER,                                       " & vbNewLine _
                                              & " @SYS_UPD_DATE,                                       " & vbNewLine _
                                              & " @SYS_UPD_TIME,                                       " & vbNewLine _
                                              & " @SYS_UPD_PGID,                                       " & vbNewLine _
                                              & " @SYS_UPD_USER,                                       " & vbNewLine _
                                              & " @SYS_DEL_FLG                                         " & vbNewLine _
                                              & " )                                                    " & vbNewLine

#End Region

#End Region

#Region "デュポン請求GLマスタの更新2"

#Region "デュポン請求GLマスタの更新2 SQL SET句"

    ''' <summary>
    ''' デュポン請求GLマスタの更新2 SQL SET句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SET_GL2 As String = "UPDATE $LM_TRN_DPN$..G_DUPONT_SEKY_GL SET               " & vbNewLine _
                                               & "       AMOUNT           = @AMOUNT                   " & vbNewLine _
                                               & "     , VAT_AMOUNT       = @VAT_AMOUNT               " & vbNewLine _
                                               & "     , SOUND            = @SOUND                    " & vbNewLine _
                                               & "     , BOND             = @BOND                     " & vbNewLine _
                                               & "     , JIDO_FLAG        = @JIDO_FLAG                " & vbNewLine _
                                               & "     , SYS_UPD_DATE     = @SYS_UPD_DATE             " & vbNewLine _
                                               & "     , SYS_UPD_TIME     = @SYS_UPD_TIME             " & vbNewLine _
                                               & "     , SYS_UPD_PGID     = @SYS_UPD_PGID             " & vbNewLine _
                                               & "     , SYS_UPD_USER     = @SYS_UPD_USER             " & vbNewLine

#End Region

#Region "デュポン請求GLマスタの更新2 SQL WHERE句"

    ''' <summary>
    ''' デュポン請求GLマスタの更新2 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_WHERE_GL2 As String = " WHERE                                            " & vbNewLine _
                                                 & "      NRS_BR_CD        = @NRS_BR_CD               " & vbNewLine _
                                                 & "  AND SEKY_YM          = @SEKY_YM                 " & vbNewLine _
                                                 & "  AND DEPART           = @DEPART                  " & vbNewLine _
                                                 & "  AND SEKY_KMK         = @SEKY_KMK                " & vbNewLine _
                                                 & "  AND FRB_CD           = @FRB_CD                  " & vbNewLine _
                                                 & "  AND SRC_CD           = @SRC_CD                  " & vbNewLine _
                                                 & "  AND MISK_CD          = @MISK_CD                 " & vbNewLine

#End Region

#End Region

#Region "EXCEL出力データの検索"

#Region "EXCEL出力データの検索 SQL SELECT句(出荷)"

    ''' <summary>
    ''' EXCEL出力データの検索 SQL SELECT句(出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EXCEL_OUTKA As String = " SELECT                                                                   " & vbNewLine _
                                                 & " UNCHIN.CUST_CD_L                              AS CUST_CD_L                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_M                              AS CUST_CD_M                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_S                              AS CUST_CD_S                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_SS                             AS CUST_CD_SS                " & vbNewLine _
                                                 & ",CUSTDETAILS.SET_NAIYO                         AS DEPART                    " & vbNewLine _
                                                 & ",LTRIM(RTRIM(GOODS.CUST_COST_CD1))             AS FRB_CD                    " & vbNewLine _
                                                 & ",LTRIM(RTRIM(GOODS.CUST_COST_CD2))             AS SRC_CD                    " & vbNewLine _
                                                 & ",OUTKAL.OUTKA_PLAN_DATE                        AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                 & ",OUTKAL.ARR_PLAN_DATE                          AS ARR_PLAN_DATE             " & vbNewLine _
                                                 & ",UPPER(OUTKAL.CUST_ORD_NO)                     AS CUST_ORD_NO               " & vbNewLine _
                                                 & ",OUTKAL.DEST_CD                                AS DEST_CD                   " & vbNewLine _
                                                 & ",GOODS.GOODS_CD_CUST                           AS CUST_GOODS_CD             " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_NG_NB                             AS SEIQ_NG_NB                " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_PKG_UT                            AS SEIQ_PKG_UT               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
                                                 & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
                                                 & " UNCHIN.DECI_INSU                              AS DECI_UNCHIN               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
                                                 & ",UNCHIN.UNSO_NO_L + '-' + UNCHIN.UNSO_NO_M     AS UNSO_NO                   " & vbNewLine _
                                                 & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_CTL_NO            " & vbNewLine _
                                                 & ",DEST.JIS                                      AS DEST_JIS_CD               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
                                                 & ",JIS.SHI                                       AS SHI                       " & vbNewLine _
                                                 & ",OUTKAL.OUTKA_PKG_NB                           AS OUTKA_PKG_NB              " & vbNewLine _
                                                 & ",DEST.SP_UNSO_CD                               AS UNSO_CD                   " & vbNewLine _
                                                 & ",OUTKAL.ORDER_TYPE                             AS TRS                       " & vbNewLine _
                                                 & ",''                                            AS ZAI_REC_NO                " & vbNewLine _
                                                 & ",ZAI.OFB_KB                                    AS OFB_KB                    " & vbNewLine _
                                                 & ",ISNULL(Z1.KBN_NM1,UNCHIN.SEIQ_PKG_UT)         AS DUPONT_PKG_UT             " & vbNewLine _
                                                 & ",''                                            AS PRT_TYPE1                 " & vbNewLine _
                                                 & ",''                                            AS PRT_TYPE2                 " & vbNewLine _
                                                 & ",CASE WHEN OUTKAL.OUTKA_PKG_NB < 10                                         " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      ELSE '0'                                                              " & vbNewLine _
                                                 & " END                                           AS PRT_TYPE3                 " & vbNewLine _
                                                 & ",CASE WHEN SUBSTRING(CUSTDETAILS.SET_NAIYO,1,1) = 'A'                       " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      WHEN SUBSTRING(CUSTDETAILS.SET_NAIYO,1,1) = 'F'                       " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      ELSE '0'                                                              " & vbNewLine _
                                                 & " END                                           AS PRT_TYPE4                 " & vbNewLine _
                                                 & ",CASE WHEN UNCHIN.DECI_UNCHIN > 0                                           " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      ELSE '0'                                                              " & vbNewLine _
                                                 & " END                                           AS PRT_TYPE5                 " & vbNewLine _
                                                 & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                " & vbNewLine _
                                                 & ",DEST.DEST_NM                                  AS DEST_NM                   " & vbNewLine

#End Region

#Region "EXCEL出力データの検索 SQL FROM句(出荷)"

    ''' <summary>
    ''' EXCEL出力データの検索 SQL FROM句(出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_EXCEL_OUTKA As String = "FROM                                                                 " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_L OUTKAL                                             " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_M OUTKAM                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                    " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.OUTKA_NO_M =                                                    " & vbNewLine _
                                                      & "(SELECT MIN(OUTKAM2.OUTKA_NO_M)                                        " & vbNewLine _
                                                      & " FROM $LM_TRN$..C_OUTKA_M OUTKAM2                                      " & vbNewLine _
                                                      & " WHERE                                                                 " & vbNewLine _
                                                      & " OUTKAM2.NRS_BR_CD = OUTKAL.NRS_BR_CD                                  " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " OUTKAM2.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " OUTKAM2.SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_S OUTKAS                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                    " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAS.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAS.OUTKA_NO_S =                                                    " & vbNewLine _
                                                      & "(SELECT MIN(OUTKAS2.OUTKA_NO_S)                                        " & vbNewLine _
                                                      & " FROM $LM_TRN$..C_OUTKA_S OUTKAS2                                      " & vbNewLine _
                                                      & " WHERE                                                                 " & vbNewLine _
                                                      & " OUTKAS2.NRS_BR_CD = OUTKAM.NRS_BR_CD                                  " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " OUTKAS2.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " OUTKAS2.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " OUTKAS2.SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..D_ZAI_TRS ZAI                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "ZAI.NRS_BR_CD = OUTKAS.NRS_BR_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "ZAI.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "ZAI.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_L UNSOL                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOL.NRS_BR_CD = OUTKAL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOL.INOUTKA_NO_L = OUTKAL.OUTKA_NO_L                                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOL.MOTO_DATA_KB = '20'                                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "RIGHT JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..F_UNCHIN_TRS UNCHIN                                          " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNCHIN.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.UNSO_NO_L = UNSOL.UNSO_NO_L                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.DECI_UNCHIN > 0                                                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "(SELECT CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,MIN(CUSTDETAILS.CUST_CD_EDA) AS CUST_CD_EDA                    " & vbNewLine _
                                                      & " FROM $LM_MST$..M_CUST_DETAILS CUSTDETAILS                             " & vbNewLine _
                                                      & " WHERE CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                      & "   AND CUSTDETAILS.SUB_KB = '01'                              " & vbNewLine _
                                                      & " GROUP BY                                                              " & vbNewLine _
                                                      & "        CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & " ) CUSTDETAILS                                                         " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "CUSTDETAILS.NRS_BR_CD = GOODS.NRS_BR_CD                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "CUSTDETAILS.CUST_CD = GOODS.CUST_CD_L +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_M +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_S +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_SS                                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "(CUSTDETAILS.SET_NAIYO = 'A'                                           " & vbNewLine _
                                                      & " OR                                                                    " & vbNewLine _
                                                      & " CUSTDETAILS.SET_NAIYO = 'F')                                          " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_DEST DEST                                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.DEST_CD = OUTKAL.DEST_CD                                          " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_JIS JIS                                                    " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "JIS.JIS_CD = DEST.JIS                                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.DEST_CD = OUTKAL.DEST_CD                                          " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..Z_KBN Z1                                                     " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "Z1.KBN_CD = GOODS.CUST_COST_CD2                                        " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "Z1.KBN_GROUP_CD = 'D002'                                               " & vbNewLine

#End Region

#Region "EXCEL出力データの検索 SQL SELECT句(入荷)"

    ''' <summary>
    ''' EXCEL出力データの検索 SQL SELECT句(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EXCEL_INKA As String = " SELECT                                                                    " & vbNewLine _
                                                 & " UNCHIN.CUST_CD_L                              AS CUST_CD_L                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_M                              AS CUST_CD_M                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_S                              AS CUST_CD_S                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_SS                             AS CUST_CD_SS                " & vbNewLine _
                                                 & ",CUSTDETAILS.SET_NAIYO                         AS DEPART                    " & vbNewLine _
                                                 & ",LTRIM(RTRIM(GOODS.CUST_COST_CD1))             AS FRB_CD                    " & vbNewLine _
                                                 & ",LTRIM(RTRIM(GOODS.CUST_COST_CD2))             AS SRC_CD                    " & vbNewLine _
                                                 & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                 & ",INKAL.INKA_DATE                               AS ARR_PLAN_DATE             " & vbNewLine _
                                                 & ",''                                            AS CUST_ORD_NO               " & vbNewLine _
                                                 & ",UNSOL.DEST_CD                                 AS DEST_CD                   " & vbNewLine _
                                                 & ",GOODS.GOODS_CD_CUST                           AS CUST_GOODS_CD             " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_NG_NB                             AS SEIQ_NG_NB                " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_PKG_UT                            AS SEIQ_PKG_UT               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
                                                 & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
                                                 & " UNCHIN.DECI_INSU                              AS DECI_UNCHIN               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
                                                 & ",UNCHIN.UNSO_NO_L + '-' + UNCHIN.UNSO_NO_M     AS UNSO_NO                   " & vbNewLine _
                                                 & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_CTL_NO            " & vbNewLine _
                                                 & ",''                                            AS DEST_JIS_CD               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
                                                 & ",''                                            AS SHI                       " & vbNewLine _
                                                 & ",'0'                                           AS OUTKA_PKG_NB              " & vbNewLine _
                                                 & ",''                                            AS UNSO_CD                   " & vbNewLine _
                                                 & ",''                                            AS TRS                       " & vbNewLine _
                                                 & ",''                                            AS ZAI_REC_NO                " & vbNewLine _
                                                 & ",ZAI.OFB_KB                                    AS OFB_KB                    " & vbNewLine _
                                                 & ",ISNULL(Z1.KBN_NM1,UNCHIN.SEIQ_PKG_UT)         AS DUPONT_PKG_UT             " & vbNewLine _
                                                 & ",''                                            AS PRT_TYPE1                 " & vbNewLine _
                                                 & ",''                                            AS PRT_TYPE2                 " & vbNewLine _
                                                 & ",'0'                                           AS PRT_TYPE3                 " & vbNewLine _
                                                 & ",CASE WHEN SUBSTRING(CUSTDETAILS.SET_NAIYO,1,1) = 'A'                       " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      WHEN SUBSTRING(CUSTDETAILS.SET_NAIYO,1,1) = 'F'                       " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      ELSE '0'                                                              " & vbNewLine _
                                                 & " END                                           AS PRT_TYPE4                 " & vbNewLine _
                                                 & ",CASE WHEN UNCHIN.DECI_UNCHIN > 0                                           " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      ELSE '0'                                                              " & vbNewLine _
                                                 & " END                                           AS PRT_TYPE5                 " & vbNewLine _
                                                 & ",GOODS.GOODS_NM_1                              AS GOODS_NM_1                " & vbNewLine _
                                                 & ",DEST.DEST_NM                                  AS DEST_NM                   " & vbNewLine

#End Region

#Region "EXCEL出力データの検索 SQL FROM句(入荷)"

    ''' <summary>
    ''' EXCEL出力データの検索 SQL FROM句(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_EXCEL_INKA As String = "FROM                                                                  " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_L INKAL                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_M INKAM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_L = INKAL.INKA_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_M = '001'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_M =                                                      " & vbNewLine _
                                                      & "(SELECT MIN(INKAM2.INKA_NO_M)                                          " & vbNewLine _
                                                      & " FROM $LM_TRN$..B_INKA_M INKAM2                                        " & vbNewLine _
                                                      & " WHERE                                                                 " & vbNewLine _
                                                      & " INKAM2.NRS_BR_CD = INKAL.NRS_BR_CD                                    " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " INKAM2.INKA_NO_L = INKAL.INKA_NO_L                                    " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " INKAM2.SYS_DEL_FLG = '0')                                             " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_S INKAS                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAS.INKA_NO_L = INKAM.INKA_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAS.INKA_NO_M = '001'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAS.INKA_NO_S = '001'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAS.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAS.INKA_NO_S =                                                      " & vbNewLine _
                                                      & "(SELECT MIN(INKAS2.INKA_NO_S)                                          " & vbNewLine _
                                                      & " FROM $LM_TRN$..B_INKA_S INKAS2                                        " & vbNewLine _
                                                      & " WHERE                                                                 " & vbNewLine _
                                                      & " INKAS2.NRS_BR_CD = INKAM.NRS_BR_CD                                    " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " INKAS2.INKA_NO_L = INKAM.INKA_NO_L                                    " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " INKAS2.INKA_NO_M = INKAM.INKA_NO_M                                    " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " INKAS2.SYS_DEL_FLG = '0')                                             " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..D_ZAI_TRS ZAI                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "ZAI.NRS_BR_CD = INKAS.NRS_BR_CD                                        " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "ZAI.ZAI_REC_NO = INKAS.ZAI_REC_NO                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "ZAI.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_L UNSOL                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOL.NRS_BR_CD = INKAL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOL.INOUTKA_NO_L = INKAL.INKA_NO_L                                   " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOL.MOTO_DATA_KB = '10'                                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "RIGHT JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..F_UNCHIN_TRS UNCHIN                                          " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNCHIN.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.UNSO_NO_L = UNSOL.UNSO_NO_L                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.DECI_UNCHIN > 0                                                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "(SELECT CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,MIN(CUSTDETAILS.CUST_CD_EDA) AS CUST_CD_EDA                    " & vbNewLine _
                                                      & " FROM $LM_MST$..M_CUST_DETAILS CUSTDETAILS                             " & vbNewLine _
                                                      & " WHERE CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                      & "   AND CUSTDETAILS.SUB_KB = '01'                              " & vbNewLine _
                                                      & " GROUP BY                                                              " & vbNewLine _
                                                      & "        CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & " ) CUSTDETAILS                                                         " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "CUSTDETAILS.NRS_BR_CD = GOODS.NRS_BR_CD                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "CUSTDETAILS.CUST_CD = GOODS.CUST_CD_L +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_M +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_S +                                " & vbNewLine _
                                                      & "                      GOODS.CUST_CD_SS                                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "(CUSTDETAILS.SET_NAIYO = 'A'                                           " & vbNewLine _
                                                      & " OR                                                                    " & vbNewLine _
                                                      & " CUSTDETAILS.SET_NAIYO = 'F')                                          " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_DEST DEST                                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "DEST.NRS_BR_CD = UNSOL.NRS_BR_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.CUST_CD_L = UNSOL.CUST_CD_L                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.DEST_CD = UNSOL.DEST_CD                                           " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..Z_KBN Z1                                                     " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "Z1.KBN_CD = GOODS.CUST_COST_CD2                                        " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "Z1.KBN_GROUP_CD = 'D002'                                               " & vbNewLine

#End Region

#Region "EXCEL出力データの検索 SQL SELECT句(運賃)"

    ''' <summary>
    ''' EXCEL出力データの検索 SQL SELECT句(運賃)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EXCEL_UNCHIN As String = " SELECT                                                                  " & vbNewLine _
                                                 & " UNCHIN.CUST_CD_L                              AS CUST_CD_L                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_M                              AS CUST_CD_M                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_S                              AS CUST_CD_S                 " & vbNewLine _
                                                 & ",UNCHIN.CUST_CD_SS                             AS CUST_CD_SS                " & vbNewLine _
                                                 & ",ISNULL(CUSTDETAILS1.SET_NAIYO,CUSTDETAILS2.SET_NAIYO) AS DEPART            " & vbNewLine _
                                                 & ",LTRIM(RTRIM(ISNULL(GOODS1.CUST_COST_CD1,GOODS2.CUST_COST_CD1))) AS FRB_CD  " & vbNewLine _
                                                 & ",LTRIM(RTRIM(ISNULL(GOODS1.CUST_COST_CD2,GOODS2.CUST_COST_CD2))) AS SRC_CD  " & vbNewLine _
                                                 & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                 & ",UNSOL.ARR_PLAN_DATE                           AS ARR_PLAN_DATE             " & vbNewLine _
                                                 & ",''                                            AS CUST_ORD_NO               " & vbNewLine _
                                                 & ",UNSOL.DEST_CD                                 AS DEST_CD                   " & vbNewLine _
                                                 & ",ISNULL(GOODS1.GOODS_CD_CUST,GOODS2.GOODS_CD_CUST) AS CUST_GOODS_CD         " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_NG_NB                             AS SEIQ_NG_NB                " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_PKG_UT                            AS SEIQ_PKG_UT               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
                                                 & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
                                                 & " UNCHIN.DECI_INSU                              AS DECI_UNCHIN               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
                                                 & ",UNCHIN.UNSO_NO_L + '-' + UNCHIN.UNSO_NO_M     AS UNSO_NO                   " & vbNewLine _
                                                 & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_CTL_NO            " & vbNewLine _
                                                 & ",DEST.JIS                                      AS DEST_JIS_CD               " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
                                                 & ",JIS.SHI                                       AS SHI                       " & vbNewLine _
                                                 & ",UNSOM.UNSO_TTL_NB                             AS OUTKA_PKG_NB              " & vbNewLine _
                                                 & ",''                                            AS UNSO_CD                   " & vbNewLine _
                                                 & ",''                                            AS TRS                       " & vbNewLine _
                                                 & ",''                                            AS ZAI_REC_NO                " & vbNewLine _
                                                 & ",''                                            AS OFB_KB                    " & vbNewLine _
                                                 & ",ISNULL(ISNULL(Z1.KBN_NM1,Z2.KBN_NM1),UNCHIN.SEIQ_PKG_UT) AS DUPONT_PKG_UT  " & vbNewLine _
                                                 & ",''                                            AS PRT_TYPE1                 " & vbNewLine _
                                                 & ",''                                            AS PRT_TYPE2                 " & vbNewLine _
                                                 & ", '0'                                          AS PRT_TYPE3                 " & vbNewLine _
                                                 & ",CASE WHEN SUBSTRING(ISNULL(CUSTDETAILS1.SET_NAIYO,CUSTDETAILS2.SET_NAIYO),1,1) = 'A' " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      WHEN SUBSTRING(ISNULL(CUSTDETAILS1.SET_NAIYO,CUSTDETAILS2.SET_NAIYO),1,1) = 'F' " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      ELSE '0'                                                              " & vbNewLine _
                                                 & " END                                           AS PRT_TYPE4                 " & vbNewLine _
                                                 & ",CASE WHEN UNCHIN.DECI_UNCHIN > 0                                           " & vbNewLine _
                                                 & "      THEN '1'                                                              " & vbNewLine _
                                                 & "      ELSE '0'                                                              " & vbNewLine _
                                                 & " END                                           AS PRT_TYPE5                 " & vbNewLine _
                                                 & ",ISNULL(GOODS1.GOODS_NM_1,GOODS2.GOODS_NM_1)   AS GOODS_NM_1                " & vbNewLine _
                                                 & ",DEST.DEST_NM                                  AS DEST_NM                   " & vbNewLine

#End Region

#Region "EXCEL出力データの検索 SQL FROM句(運賃)"

    ''' <summary>
    ''' EXCEL出力データの検索 SQL FROM句(運賃)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_EXCEL_UNCHIN As String = "FROM                                                                " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_L UNSOL                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_M UNSOM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_M = '001'                                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "RIGHT JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..F_UNCHIN_TRS UNCHIN                                          " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNCHIN.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.UNSO_NO_L = UNSOL.UNSO_NO_L                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.DECI_UNCHIN > 0                                                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_L INKAL                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAL.NRS_BR_CD = UNSOL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAL.INKA_NO_L = UNSOL.INOUTKA_NO_L                                   " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_M INKAM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_L = INKAL.INKA_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_M =                                                      " & vbNewLine _
                                                      & "(SELECT MIN(INKAM2.INKA_NO_M)                                          " & vbNewLine _
                                                      & " FROM $LM_TRN$..B_INKA_M INKAM2                                        " & vbNewLine _
                                                      & " WHERE                                                                 " & vbNewLine _
                                                      & " INKAM2.NRS_BR_CD = INKAL.NRS_BR_CD                                    " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " INKAM2.INKA_NO_L = INKAL.INKA_NO_L                                    " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " INKAM2.SYS_DEL_FLG = '0')                                             " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS1                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS1.NRS_BR_CD = INKAM.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS1.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "(SELECT CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,MIN(CUSTDETAILS.CUST_CD_EDA) AS CUST_CD_EDA                    " & vbNewLine _
                                                      & " FROM $LM_MST$..M_CUST_DETAILS CUSTDETAILS                             " & vbNewLine _
                                                      & " WHERE CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                      & "   AND CUSTDETAILS.SUB_KB = '01'                              " & vbNewLine _
                                                      & " GROUP BY                                                              " & vbNewLine _
                                                      & "        CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & " ) CUSTDETAILS1                                                        " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "CUSTDETAILS1.NRS_BR_CD = GOODS1.NRS_BR_CD                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "CUSTDETAILS1.CUST_CD = GOODS1.CUST_CD_L +                              " & vbNewLine _
                                                      & "                      GOODS1.CUST_CD_M +                               " & vbNewLine _
                                                      & "                      GOODS1.CUST_CD_S +                               " & vbNewLine _
                                                      & "                      GOODS1.CUST_CD_SS                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "(CUSTDETAILS1.SET_NAIYO = 'A'                                          " & vbNewLine _
                                                      & " OR                                                                    " & vbNewLine _
                                                      & " CUSTDETAILS1.SET_NAIYO = 'F')                                         " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_L OUTKAL                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAL.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAL.OUTKA_NO_L = UNSOL.INOUTKA_NO_L                                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAL.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_M OUTKAM                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                    " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.OUTKA_NO_M =                                                    " & vbNewLine _
                                                      & "(SELECT MIN(OUTKAM2.OUTKA_NO_M)                                        " & vbNewLine _
                                                      & " FROM $LM_TRN$..C_OUTKA_M OUTKAM2                                      " & vbNewLine _
                                                      & " WHERE                                                                 " & vbNewLine _
                                                      & " OUTKAM2.NRS_BR_CD = OUTKAL.NRS_BR_CD                                  " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " OUTKAM2.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                " & vbNewLine _
                                                      & " AND                                                                   " & vbNewLine _
                                                      & " OUTKAM2.SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS2                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS2.NRS_BR_CD = OUTKAM.NRS_BR_CD                                    " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS2.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                              " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "(SELECT CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,MIN(CUSTDETAILS.CUST_CD_EDA) AS CUST_CD_EDA                    " & vbNewLine _
                                                      & " FROM $LM_MST$..M_CUST_DETAILS CUSTDETAILS                             " & vbNewLine _
                                                      & " WHERE CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                      & "   AND CUSTDETAILS.SUB_KB = '01'                              " & vbNewLine _
                                                      & " GROUP BY                                                              " & vbNewLine _
                                                      & "        CUSTDETAILS.NRS_BR_CD                                          " & vbNewLine _
                                                      & "       ,CUSTDETAILS.CUST_CD                                            " & vbNewLine _
                                                      & "       ,CUSTDETAILS.SET_NAIYO                                          " & vbNewLine _
                                                      & " ) CUSTDETAILS2                                                        " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "CUSTDETAILS2.NRS_BR_CD = GOODS2.NRS_BR_CD                              " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "CUSTDETAILS2.CUST_CD = GOODS2.CUST_CD_L +                              " & vbNewLine _
                                                      & "                      GOODS2.CUST_CD_M +                               " & vbNewLine _
                                                      & "                      GOODS2.CUST_CD_S +                               " & vbNewLine _
                                                      & "                      GOODS2.CUST_CD_SS                                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "(CUSTDETAILS2.SET_NAIYO = 'A'                                          " & vbNewLine _
                                                      & " OR                                                                    " & vbNewLine _
                                                      & " CUSTDETAILS2.SET_NAIYO = 'F')                                         " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_DEST DEST                                                  " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "DEST.NRS_BR_CD = UNSOL.NRS_BR_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.CUST_CD_L = UNSOL.CUST_CD_L                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.DEST_CD = UNSOL.DEST_CD                                           " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_JIS JIS                                                    " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "JIS.JIS_CD = DEST.JIS                                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "DEST.DEST_CD = UNSOL.DEST_CD                                           " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..Z_KBN Z1                                                     " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "Z1.KBN_CD = GOODS1.CUST_COST_CD2                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "Z1.KBN_GROUP_CD = 'D002'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..Z_KBN Z2                                                     " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "Z2.KBN_CD = GOODS2.CUST_COST_CD2                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "Z2.KBN_GROUP_CD = 'D002'                                               " & vbNewLine

#End Region

#Region "EXCEL出力データの検索 SQL ORDER BY句"

    ''' <summary>
    ''' EXCEL出力データの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_EXCEL As String = "ORDER BY                                                                  " & vbNewLine _
                                                   & " OUTKA_PLAN_DATE                                                          " & vbNewLine _
                                                   & ",CUST_ORD_NO                                                              " & vbNewLine

#End Region

#Region "EXCEL出力データの検索 SQL UNION句"

    ''' <summary>
    ''' EXCEL出力データの検索 SQL UNION句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNION_EXCEL As String = "UNION                                                                     " & vbNewLine

#End Region

#End Region

#Region "データの検索"

#Region "データの検索 SQL SELECT句(COUNT)"

    ''' <summary>
    ''' データの検索 SQL SELECT句(COUNT)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU_COUNT As String = " SELECT COUNT(Z1.KBN_NM1)		            AS SELECT_CNT           " & vbNewLine

#End Region

#Region "データの検索 SQL SELECT句"

    'START YANAI 要望番号830
    '''' <summary>
    '''' データの検索 SQL SELECT句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_KENSAKU As String = "SELECT                                                                        " & vbNewLine _
    '                                           & " Z1.KBN_NM1                                    AS NRS_BR_CD                   " & vbNewLine _
    '                                           & ",Z1.KBN_NM2                                    AS CUST_CD_L                   " & vbNewLine _
    '                                           & ",Z1.KBN_NM3                                    AS CUST_CD_M                   " & vbNewLine _
    '                                           & ",Z1.KBN_NM4                                    AS CUST_CD_S                   " & vbNewLine _
    '                                           & ",Z1.KBN_NM5                                    AS CUST_CD_SS                  " & vbNewLine _
    '                                           & ",MCD.SET_NAIYO                                 AS DEPART                      " & vbNewLine _
    '                                           & ",Z2.KBN_NM1                                    AS DEPART_NM                   " & vbNewLine _
    '                                           & ",INTER.SEKY_YM                                 AS SEKY_YM                     " & vbNewLine
    ''' <summary>
    ''' データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU As String = "SELECT                                                                        " & vbNewLine _
                                               & " Z1.KBN_NM1                                    AS NRS_BR_CD                   " & vbNewLine _
                                               & ",Z1.KBN_NM2                                    AS CUST_CD_L                   " & vbNewLine _
                                               & ",Z1.KBN_NM3                                    AS CUST_CD_M                   " & vbNewLine _
                                               & ",Z1.KBN_NM4                                    AS CUST_CD_S                   " & vbNewLine _
                                               & ",Z1.KBN_NM5                                    AS CUST_CD_SS                  " & vbNewLine _
                                               & ",CUST.CUST_NM_L + ' ' + CUST.CUST_NM_M + ' ' + CUST.CUST_NM_S + ' ' + CUST.CUST_NM_SS                                AS CUST_NM                    " & vbNewLine _
                                               & ",MCD.SET_NAIYO                                 AS DEPART                      " & vbNewLine _
                                               & ",Z2.KBN_NM1                                    AS DEPART_NM                   " & vbNewLine _
                                               & ",INTER2.SEKY_YM                                 AS SEKY_YM                     " & vbNewLine _
                                               & ",CUST.TANTO_CD                                 AS TANTO_CD                    " & vbNewLine _
                                               & ",SUSER.USER_NM                                 AS TANTO_NM                    " & vbNewLine _
                                               & ",CASE WHEN GL.NRS_BR_CD IS NOT NULL                                           " & vbNewLine _
                                               & "      THEN '02'                                                               " & vbNewLine _
                                               & "      WHEN  MPBASE.NRS_BR_CD IS NOT NULL                                      " & vbNewLine _
                                               & "      THEN '01'                                                               " & vbNewLine _
                                               & "      ELSE '00'                                                               " & vbNewLine _
                                               & " END AS SHINCHOKU                                                             " & vbNewLine _
                                               & ",''                                            AS SHINCHOKU_KB                " & vbNewLine _
                                               & ",INTER.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
    'END YANAI 要望番号830

#End Region

#Region "データの検索 SQL FROM句"

    'START YANAI 要望番号830
    '''' <summary>
    '''' データの検索 SQL FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_KENSAKU As String = "FROM                                                                     " & vbNewLine _
    '                                                & "$LM_MST$..Z_KBN Z1                                                       " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..M_NRS_BR NRS_BR                                                " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "NRS_BR.NRS_BR_CD = Z1.KBN_NM1                                            " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..M_CUST_DETAILS MCD                                             " & vbNewLine _
    '                                                & "ON Z1.KBN_NM1 = MCD.NRS_BR_CD                                            " & vbNewLine _
    '                                                & "AND Z1.KBN_NM2 + Z1.KBN_NM3 + Z1.KBN_NM4 + Z1.KBN_NM5 = MCD.CUST_CD      " & vbNewLine _
    '                                                & "AND MCD.SUB_KB = '01'                                                    " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..Z_KBN Z2                                                       " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "Z2.KBN_GROUP_CD = 'Z009' AND                                             " & vbNewLine _
    '                                                & "Z2.KBN_CD = '0' + MCD.SET_NAIYO                                          " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_TRN_DPN$..G_DUPONT_INTERFACE_TRS INTER                                   " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "Z1.KBN_NM1 = INTER.NRS_BR_CD AND                                         " & vbNewLine _
    '                                                & "Z1.KBN_NM2 = INTER.CUST_CD_L AND                                         " & vbNewLine _
    '                                                & "Z1.KBN_NM3 = INTER.CUST_CD_M AND                                         " & vbNewLine _
    '                                                & "Z1.KBN_NM4 = INTER.CUST_CD_S AND                                         " & vbNewLine _
    '                                                & "Z1.KBN_NM5 = INTER.CUST_CD_SS                                            " & vbNewLine
    ''' <summary>
    ''' データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENSAKU As String = "FROM                                                                     " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z1                                                       " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..M_NRS_BR NRS_BR                                                " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "NRS_BR.NRS_BR_CD = Z1.KBN_NM1                                            " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..M_CUST_DETAILS MCD                                             " & vbNewLine _
                                                    & "ON Z1.KBN_NM1 = MCD.NRS_BR_CD                                            " & vbNewLine _
                                                    & "AND Z1.KBN_NM2 + Z1.KBN_NM3 + Z1.KBN_NM4 + Z1.KBN_NM5 = MCD.CUST_CD      " & vbNewLine _
                                                    & "AND MCD.SUB_KB = '01'                                                    " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z2                                                       " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z2.KBN_GROUP_CD = 'Z009' AND                                             " & vbNewLine _
                                                    & "Z2.KBN_CD = '0' + MCD.SET_NAIYO                                          " & vbNewLine _
                                                    & "LEFT JOIN                                           " & vbNewLine _
                                                    & "(SELECT INTER.NRS_BR_CD ,INTER.CUST_CD_L ,INTER.CUST_CD_M ,INTER.CUST_CD_S ,INTER.CUST_CD_SS  ,INTER.SEKY_YM                                          " & vbNewLine _
                                                    & "FROM $LM_TRN_DPN$..G_DUPONT_INTERFACE_TRS INTER                                         " & vbNewLine _
                                                    & "WHERE SEKY_YM >= @SKYU_DATE_FROM AND SEKY_YM <= @SKYU_DATE_TO                           " & vbNewLine _
                                                    & "UNION                                          " & vbNewLine _
                                                    & "SELECT MG.NRS_BR_CD,MG.CUST_CD_L,MG.CUST_CD_M,MG.CUST_CD_S,MG.CUST_CD_SS,SUBSTRING(MP.INV_DATE_FROM,1,6) AS SEKY_YM                                         " & vbNewLine _
                                                    & "FROM $LM_TRN$..G_SEKY_MEISAI_PRT MP                                                                               " & vbNewLine _
                                                    & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                  " & vbNewLine _
                                                    & "ON MP.NRS_BR_CD = MG.NRS_BR_CD                                                    " & vbNewLine _
                                                    & "AND MP.GOODS_CD_NRS = MG.GOODS_CD_NRS                                  " & vbNewLine _
                                                    & "WHERE                                                                  " & vbNewLine _
                                                    & "MP.SYS_DEL_FLG = '0'                                           " & vbNewLine

    ''' <summary>
    ''' データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENSAKU2 As String = "GROUP BY                                                                                                          " & vbNewLine _
                                                    & "MG.NRS_BR_CD,MG.CUST_CD_L,MG.CUST_CD_M,MG.CUST_CD_S,MG.CUST_CD_SS,SUBSTRING(MP.INV_DATE_FROM,1,6)                                         " & vbNewLine _
                                                    & "                                         " & vbNewLine _
                                                    & ") INTER2                                         " & vbNewLine _
                                                    & "ON                                                                                                       " & vbNewLine _
                                                    & "Z1.KBN_NM1 = INTER2.NRS_BR_CD AND                                                                                  " & vbNewLine _
                                                    & "Z1.KBN_NM2 = INTER2.CUST_CD_L AND                                                                                  " & vbNewLine _
                                                    & "Z1.KBN_NM3 = INTER2.CUST_CD_M AND                                                                                  " & vbNewLine _
                                                    & "Z1.KBN_NM4 = INTER2.CUST_CD_S AND                                                                                  " & vbNewLine _
                                                    & "Z1.KBN_NM5 = INTER2.CUST_CD_SS                                                                                     " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_TRN_DPN$..G_DUPONT_INTERFACE_TRS INTER                                   " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z1.KBN_NM1 = INTER.NRS_BR_CD AND                                         " & vbNewLine _
                                                    & "Z1.KBN_NM2 = INTER.CUST_CD_L AND                                         " & vbNewLine _
                                                    & "Z1.KBN_NM3 = INTER.CUST_CD_M AND                                         " & vbNewLine _
                                                    & "Z1.KBN_NM4 = INTER.CUST_CD_S AND                                         " & vbNewLine _
                                                    & "Z1.KBN_NM5 = INTER.CUST_CD_SS AND                                           " & vbNewLine _
                                                    & "INTER.SEKY_YM = INTER2.SEKY_YM                                            " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..M_CUST CUST                                                    " & vbNewLine _
                                                    & "ON CUST.NRS_BR_CD = Z1.KBN_NM1                                           " & vbNewLine _
                                                    & "AND CUST.CUST_CD_L = Z1.KBN_NM2                                          " & vbNewLine _
                                                    & "AND CUST.CUST_CD_M = Z1.KBN_NM3                                          " & vbNewLine _
                                                    & "AND CUST.CUST_CD_S = Z1.KBN_NM4                                          " & vbNewLine _
                                                    & "AND CUST.CUST_CD_SS = Z1.KBN_NM5                                          " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..S_USER SUSER                                                   " & vbNewLine _
                                                    & "ON SUSER.USER_CD = CUST.TANTO_CD                                         " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL GL                                            " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "GL.NRS_BR_CD = INTER.NRS_BR_CD AND                                       " & vbNewLine _
                                                    & "GL.SEKY_YM = INTER.SEKY_YM AND                                           " & vbNewLine _
                                                    & "GL.DEPART = INTER.DEPART AND                                             " & vbNewLine _
                                                    & "GL.SEKY_KMK = INTER.SEKY_KMK AND                                         " & vbNewLine _
                                                    & "GL.FRB_CD = INTER.FRB_CD AND                                             " & vbNewLine _
                                                    & "GL.SRC_CD = INTER.SRC_CD AND                                             " & vbNewLine _
                                                    & "GL.MISK_CD = INTER.MISK_CD AND                                           " & vbNewLine _
                                                    & "GL.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                                    & "LEFT JOIN (                                                              " & vbNewLine _
                                                    & "SELECT MG.NRS_BR_CD,MG.CUST_CD_L,MG.CUST_CD_M,MG.CUST_CD_S,MG.CUST_CD_SS,SUBSTRING(MP.INV_DATE_FROM,1,6) AS SEKY_YM" & vbNewLine _
                                                    & "FROM $LM_TRN$..G_SEKY_MEISAI_PRT MP                                      " & vbNewLine _
                                                    & "LEFT JOIN $LM_MST$..M_GOODS MG                                           " & vbNewLine _
                                                    & "ON MP.NRS_BR_CD = MG.NRS_BR_CD                                           " & vbNewLine _
                                                    & "AND MP.GOODS_CD_NRS = MG.GOODS_CD_NRS                                    " & vbNewLine _
                                                    & "WHERE                                                                    " & vbNewLine _
                                                    & "MP.SYS_DEL_FLG = '0'                                                     " & vbNewLine

    ''' <summary>
    ''' データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENSAKU3 As String = "GROUP BY                                                                 " & vbNewLine _
                                                    & "MG.NRS_BR_CD,MG.CUST_CD_L,MG.CUST_CD_M,MG.CUST_CD_S,MG.CUST_CD_SS,SUBSTRING(MP.INV_DATE_FROM,1,6)" & vbNewLine _
                                                    & ") MPBASE                                                                 " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z1.KBN_NM1 = MPBASE.NRS_BR_CD AND                                        " & vbNewLine _
                                                    & "Z1.KBN_NM2 = MPBASE.CUST_CD_L AND                                        " & vbNewLine _
                                                    & "Z1.KBN_NM3 = MPBASE.CUST_CD_M AND                                        " & vbNewLine _
                                                    & "Z1.KBN_NM4 = MPBASE.CUST_CD_S AND                                        " & vbNewLine _
                                                    & "Z1.KBN_NM5 = MPBASE.CUST_CD_SS                                           " & vbNewLine    'END YANAI 要望番号830

#End Region

#Region "データの検 SQL GROUP BY句"

    'START YANAI 要望番号830
    '''' <summary>
    '''' データの検 SQL GROUP BY句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_GROUP_KENSAKU As String = "GROUP BY                                                                " & vbNewLine _
    '                                           & " Z1.KBN_NM1                                                                   " & vbNewLine _
    '                                           & ",Z1.KBN_NM2                                                                   " & vbNewLine _
    '                                           & ",Z1.KBN_NM3                                                                   " & vbNewLine _
    '                                           & ",Z1.KBN_NM4                                                                   " & vbNewLine _
    '                                           & ",Z1.KBN_NM5                                                                   " & vbNewLine _
    '                                           & ",MCD.SET_NAIYO                                                                " & vbNewLine _
    '                                           & ",Z2.KBN_NM1                                                                   " & vbNewLine _
    '                                           & ",INTER.SEKY_YM                                                                " & vbNewLine
    ''' <summary>
    ''' データの検 SQL GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_KENSAKU As String = "GROUP BY                                                                " & vbNewLine _
                                               & " Z1.KBN_NM1                                                                   " & vbNewLine _
                                               & ",Z1.KBN_NM2                                                                   " & vbNewLine _
                                               & ",Z1.KBN_NM3                                                                   " & vbNewLine _
                                               & ",Z1.KBN_NM4                                                                   " & vbNewLine _
                                               & ",Z1.KBN_NM5                                                                   " & vbNewLine _
                                               & ",MCD.SET_NAIYO                                                                " & vbNewLine _
                                               & ",Z2.KBN_NM1                                                                   " & vbNewLine _
                                               & ",CUST.TANTO_CD                                                                " & vbNewLine _
                                               & ",SUSER.USER_NM                                                                " & vbNewLine _
                                               & ",GL.NRS_BR_CD                                                                 " & vbNewLine _
                                               & ",INTER2.SEKY_YM                                                                " & vbNewLine _
                                               & ",MPBASE.NRS_BR_CD                                                             " & vbNewLine _
                                               & ",CUST.CUST_NM_L                                                               " & vbNewLine _
                                               & ",CUST.CUST_NM_M                                                               " & vbNewLine _
                                               & ",CUST.CUST_NM_S                                                               " & vbNewLine _
                                               & ",CUST.CUST_NM_SS                                                              " & vbNewLine _
                                               & ",INTER.NRS_BR_CD                                                              " & vbNewLine

    'END YANAI 要望番号830

#End Region

#Region "データの検 SQL ORDER BY句"

    ''' <summary>
    ''' データの検 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_KENSAKU As String = "ORDER BY                                                                " & vbNewLine _
                                                     & " Z1.KBN_NM1                                                             " & vbNewLine _
                                                     & ",Z1.KBN_NM2                                                             " & vbNewLine _
                                                     & ",Z1.KBN_NM3                                                             " & vbNewLine _
                                                     & ",Z1.KBN_NM4                                                             " & vbNewLine _
                                                     & ",Z1.KBN_NM5                                                             " & vbNewLine _
                                                     & ",INTER2.SEKY_YM                                                          " & vbNewLine

#End Region

#End Region

#Region "運賃データ用元区分"

    Private Const UNCHIN_MOTO_INKA As String = "10"
    Private Const UNCHIN_MOTO_OUTKA As String = "20"
    Private Const UNCHIN_MOTO_UNSO As String = "40"
    Private Const UNCHIN_MOTO_BASE As String = "00"

#End Region

#Region "荷主明細の検索"

    'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
#Region "データの検索 SQL SELECT句(COUNT)"

    ''' <summary>
    ''' データの検索 SQL SELECT句(COUNT)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CUSTDETAILS_COUNT As String = " SELECT COUNT(CUSTDETAILS.NRS_BR_CD) AS SELECT_CNT           " & vbNewLine

#End Region

#Region "データの検索 SQL FROM句"

    ''' <summary>
    ''' データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CUSTDETAILS As String = "FROM                                                                     " & vbNewLine _
                                                        & "$LM_MST$..M_CUST_DETAILS CUSTDETAILS                                     " & vbNewLine _
                                                        & "WHERE                                                                    " & vbNewLine _
                                                        & "      CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                                        & "  AND CUSTDETAILS.CUST_CD LIKE @CUST_CD                                   " & vbNewLine _
                                                        & "  AND CUSTDETAILS.SUB_KB    = '01'                                       " & vbNewLine _
                                                        & "  AND CUSTDETAILS.SYS_DEL_FLG = '0'                                      " & vbNewLine


#End Region
    'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
#End Region

#End Region

#Region "Field"

    '''' <summary>
    '''' 検索条件設定用
    '''' </summary>
    '''' <remarks></remarks>
    'Private _Row As Data.DataRow

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

    Private setWhereBr As Boolean
    Private setWhereCustL As Boolean
    Private setWhereCustM As Boolean
    Private setWhereCustS As Boolean
    Private setWhereCustSS As Boolean
    Private setWhereDateFrom As Boolean
    Private setWhereDateTo As Boolean
    Private setWhereDepart As Boolean


#End Region

#Region "Method"

#Region "SQLメイン処理"

#Region "デュポン請求ファイルの検索"

    ''' <summary>
    ''' デュポン請求ファイルの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInterFace(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_INTERFACE)      'SQL構築(Select句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_INTERFACE) 'SQL構築(From句)
        Call Me.SetSQLWhereInterface(inTbl.Rows(0))            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectInterFace", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("DEPART", "DEPART")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("TAX", "TAX")
        map.Add("SEKY_KMK", "SEKY_KMK")
        map.Add("AMOUNT", "AMOUNT")
        map.Add("VAT_AMOUNT", "VAT_AMOUNT")
        map.Add("SOUND_GL", "SOUND_GL")
        map.Add("BOND_GL", "BOND_GL")
        map.Add("JIDO_FLAG", "JIDO_FLAG")
        map.Add("SHUDO_FLAG", "SHUDO_FLAG")
        map.Add("NRS_BR_CD_GL", "NRS_BR_CD_GL")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT_SELECT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "デュポン請求ファイルの物理削除"

    ''' <summary>
    ''' デュポン請求ファイルの物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteInterFace(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_DELETE_INTERFACE)      'SQL構築(Delete句)
        Call Me.SetSQLWhereInterface(inTbl.Rows(0))            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm((Me._StrSql.ToString).Replace("INTER.", "").ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "DeleteInterFace", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "デュポン請求GLマスタの更新"

    ''' <summary>
    ''' デュポン請求GLマスタの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSekyGL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030OUT_SELECT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_UPDATE_SET_GL)         'SQL構築(SET句)
        Me._StrSql.Append(LMI030DAC.SQL_UPDATE_WHERE_GL)       'SQL構築(WHERE句)

        'パラメータ設定
        Call Me.SetSekyGLUpdParameter1(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "UpdateSekyGL", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "デュポン請求GLマスタの物理削除"

    ''' <summary>
    ''' デュポン請求GLマスタの物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSekyGL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030OUT_SELECT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_DELETE_SEKYGL)         'SQL構築(DELETE句)
        Me._StrSql.Append(LMI030DAC.SQL_DELETE_WHERE_SEKYGL)   'SQL構築(WHERE句)

        'パラメータ設定
        Call Me.SetSekyGLUpdParameter1(inTbl.Rows(0), Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "DeleteSekyGL", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "保管料荷役明細印刷テーブルの検索(保管の場合)"

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索(保管の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGMeisaiHOKAN(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        ''簿外品区分検索対象荷主の取得
        'rtnDs = Me.SelectKbnOfb(ds)
        'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_GMEISAI_HOKAN)             'SQL構築(Select句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_GMEISAI1)             'SQL構築(From句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_GMEISAI2)             'SQL構築(From句)
        'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_GMEISAI3)             'SQL構築(From句)
        'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_WHERE_GMEISAI)             'SQL構築(Where句)
        Call Me.SetSQLWhereGMeisai(rtnDs, inTbl.Rows(0))                  '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_GROUP_GMEISAI)             'SQL構築(Group by句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_ORDER_GMEISAI)             'SQL構築(Order by句)

        'パラメータ設定
        'Call Me.SetGMeisaSelectParameter(inTbl.Rows(0), Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectGMeisai", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("DEPART", "DEPART")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("TAX", "TAX")
        map.Add("SEKY_KMK", "SEKY_KMK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT_SELECT")

        Dim max As Integer = ds.Tables("LMI030OUT_SELECT").Rows.Count - 1
        Dim sound As Decimal = 0
        Dim taxRate As Decimal = 0

        '結果を見た限り税額はSUMしてからの税額なので、処理箇所変更
        'TAXだけ取得しておく
        rtnDs = Me.SelectTax(ds)
        If 0 < rtnDs.Tables("LMI030OUT_TAX").Rows.Count Then
            ds.Tables("LMI030OUT_TAX").Clear()
            For Each row As DataRow In rtnDs.Tables("LMI030OUT_TAX").Rows
                ds.Tables("LMI030OUT_TAX").ImportRow(row)
            Next
        End If
        'For i As Integer = 0 To max
        '    '税額の再計算処理を行う。

        '    sound = Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("SOUND").ToString)
        '    If sound <> 0 Then
        '        '税率マスタの取得
        '        rtnDs = Me.SelectTax(ds, i)

        '        If 0 < rtnDs.Tables("LMI030OUT_TAX").Rows.Count Then
        '            '再計算
        '            taxRate = Convert.ToDecimal(rtnDs.Tables("LMI030OUT_TAX").Rows(0).Item("TAX_RATE").ToString)
        '            sound = Math.Round(sound * taxRate / (taxRate + 1), 0)
        '            'sound = sound * Convert.ToDecimal(rtnDs.Tables("LMI030OUT_TAX").Rows(0).Item("TAX_RATE").ToString) + Convert.ToDecimal(0.001)
        '            ds.Tables("LMI030OUT_SELECT").Rows(i).Item("TAX") = Convert.ToString(sound)
        '        End If
        '    End If
        'Next

        reader.Close()

        Return ds

    End Function

#End Region

#Region "保管料荷役明細印刷テーブルの検索(荷役の場合)"

    ''' <summary>
    ''' 保管料荷役明細印刷テーブルの検索(荷役の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGMeisaiNIYAKU(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        ''簿外品区分検索対象荷主の取得
        'rtnDs = Me.SelectKbnOfb(ds)
        'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_GMEISAI_NIYAKU)            'SQL構築(Select句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_GMEISAI1)             'SQL構築(From句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_GMEISAI2)             'SQL構築(From句)
        'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_GMEISAI3)             'SQL構築(From句)
        'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_WHERE_GMEISAI)             'SQL構築(Where句)
        Call Me.SetSQLWhereGMeisai(rtnDs, inTbl.Rows(0))                       '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_GROUP_GMEISAI_NIYAKU)      'SQL構築(Group by句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_ORDER_GMEISAI)             'SQL構築(Order by句)

        'パラメータ設定
        'Call Me.SetGMeisaSelectParameter(inTbl.Rows(0), Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectGMeisai", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("DEPART", "DEPART")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("TAX", "TAX")
        map.Add("SEKY_KMK", "SEKY_KMK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT_SELECT")

        '結果を見た限り税額はSUMしてからの税額なので、処理箇所変更
        'TAXだけ取得しておく
        rtnDs = Me.SelectTax(ds)
        If 0 < rtnDs.Tables("LMI030OUT_TAX").Rows.Count Then
            ds.Tables("LMI030OUT_TAX").Clear()
            For Each row As DataRow In rtnDs.Tables("LMI030OUT_TAX").Rows
                ds.Tables("LMI030OUT_TAX").ImportRow(row)
            Next
        End If
        'Dim max As Integer = ds.Tables("LMI030OUT_SELECT").Rows.Count - 1
        'Dim sound As Decimal = 0
        'For i As Integer = 0 To max
        '    '税額の再計算処理を行う。

        '    sound = Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("SOUND").ToString)
        '    If sound <> 0 Then
        '        '税率マスタの取得
        '        rtnDs = Me.SelectTax(ds, i)

        '        If 0 < rtnDs.Tables("LMI030OUT_TAX").Rows.Count Then
        '            taxRate = Convert.ToDecimal(rtnDs.Tables("LMI030OUT_TAX").Rows(0).Item("TAX_RATE").ToString)
        '            sound = Math.Round(sound * taxRate / (taxRate + 1), 0)
        '            'sound = sound * Convert.ToDecimal(rtnDs.Tables("LMI030OUT_TAX").Rows(0).Item("TAX_RATE").ToString) + Convert.ToDecimal(0.001)
        '            ds.Tables("LMI030OUT_SELECT").Rows(i).Item("TAX") = Convert.ToString(sound)
        '        End If
        '    End If
        'Next

        reader.Close()

        Return ds

    End Function

#End Region

#Region "運賃テーブルの検索(運賃の場合)"

    ''' <summary>
    ''' 運賃テーブルの検索(運賃の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectFUnchin(ByVal ds As DataSet) As DataSet

        'init
        setWhereBr = False
        setWhereCustL = False
        setWhereCustM = False
        setWhereCustS = False
        setWhereCustSS = False
        setWhereDateFrom = False
        setWhereDateTo = False
        setWhereDepart = False

        Dim rtnDs As DataSet = Nothing

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        ''簿外品区分検索対象荷主の取得
        'rtnDs = Me.SelectKbnOfb(ds)
        'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FUnchin_SELECT_MAIN)            'SQL構築(Select句)
        '出荷データ
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FUnchin_SELECT_OUTKA)             'SQL構築(From句)
        Call Me.SetSQLWhereFUnchin(rtnDs, inTbl.Rows(0), UNCHIN_MOTO_OUTKA)                       '条件設定
        '入荷データ
        Me._StrSql.Append(SQL_SELECT_FUnchin_SELECT_INKA)                 'SQL構築(From句)
        Call Me.SetSQLWhereFUnchin(rtnDs, inTbl.Rows(0), UNCHIN_MOTO_INKA)                       '条件設定
        Me._StrSql.Append(SQL_SELECT_FUnchin_SELECT_UNSO)                 'SQL構築(From句)
        '運送データ
        Call Me.SetSQLWhereFUnchin(rtnDs, inTbl.Rows(0), UNCHIN_MOTO_UNSO)                       '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FUnchin_JOIN)            'SQL構築(Select句)

        Call Me.SetSQLWhereFUnchin(rtnDs, inTbl.Rows(0), UNCHIN_MOTO_BASE)                       '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FUnchin_GROUPBY)            'SQL構築(Select句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectFUnchin", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("DEPART", "DEPART")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("TAX", "TAX")
        map.Add("SEKY_KMK", "SEKY_KMK")
        'START YANAI 要望番号830
        map.Add("SEIQ_FIXED_FLAG", "SEIQ_FIXED_FLAG")
        'END YANAI 要望番号830

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT_SELECT")

        '結果を見た限り税額はSUMしてからの税額なので、処理箇所変更
        'TAXだけ取得しておく
        rtnDs = Me.SelectTax(ds)
        If 0 < rtnDs.Tables("LMI030OUT_TAX").Rows.Count Then
            ds.Tables("LMI030OUT_TAX").Clear()
            For Each row As DataRow In rtnDs.Tables("LMI030OUT_TAX").Rows
                ds.Tables("LMI030OUT_TAX").ImportRow(row)
            Next
        End If

        reader.Close()

        Return ds

    End Function

#End Region

#Region "デュポン請求ファイルの新規追加"

    ''' <summary>
    ''' デュポン請求ファイルの新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertInterFace(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030OUT_SELECT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_INSERT_INTERFACE)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetInterFaceInsParameter(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertOutKaS", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "デュポン請求GLマスタの検索"

    ''' <summary>
    ''' デュポン請求GLマスタの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSekyGL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030OUT_SELECT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_SEKYGL)           'SQL構築(Select句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_SEKYGL)      'SQL構築(From句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_WHERE_SEKYGL)     'SQL構築(Where句)
        Call Me.SetSekyGLSelectParameter(inTbl.Rows(0), Me._SqlPrmList) 'パラメータ設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectSekyGL", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("DEPART", "DEPART")
        map.Add("SEKY_KMK", "SEKY_KMK")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("COST_CENTER", "COST_CENTER")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("DEST_CTY", "DEST_CTY")
        map.Add("AMOUNT", "AMOUNT")
        map.Add("VAT_AMOUNT", "VAT_AMOUNT")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("JIDO_FLAG", "JIDO_FLAG")
        map.Add("SHUDO_FLAG", "SHUDO_FLAG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT_SELECT_GL")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "デュポン請求GLマスタの新規追加"

    ''' <summary>
    ''' デュポン請求GLマスタの新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSekyGL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030OUT_SELECT_GL")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_INSERT_SEKYGL)               'SQL構築(INSERT句)

        'SQLパラメータ（個別項目）設定
        Call Me.SetSekyGLInsParameter(inTbl.Rows(0), Me._SqlPrmList)
        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "InsertSekyGL", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "デュポン請求GLマスタの更新2"

    ''' <summary>
    ''' デュポン請求GLマスタの更新2
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSekyGL2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030OUT_SELECT_GL")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_UPDATE_SET_GL2)         'SQL構築(SET句)
        Me._StrSql.Append(LMI030DAC.SQL_UPDATE_WHERE_GL2)       'SQL構築(WHERE句)

        'パラメータ設定
        Call Me.SetSekyGLUpdParameter2(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "UpdateSekyGL2", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "区分マスタの検索"

    'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
    '''' <summary>
    '''' 簿外品検索条件追加対象荷主の検索
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks></remarks>
    'Private Function SelectKbnOfb(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMI030IN")

    '    'SQL格納変数の初期化
    '    Me._StrSql = New StringBuilder()

    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    '区分マスタ値を取得し、簿外品条件追加対象荷主なのか確認
    '    'SQL作成
    '    Me._StrSql.Append(LMI030DAC.SQL_SELECT_KBN_OFB)             'SQL構築

    '    'パラメータ設定
    '    Call Me.SetKbnOfbSelectParameter(inTbl.Rows(0), Me._SqlPrmList)

    '    'スキーマ名設定
    '    Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectKbnOfb", cmd)

    '    'SQLの発行
    '    Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '    'DataReader→DataTableへの転記
    '    Dim map As Hashtable = New Hashtable()

    '    '取得データの格納先をマッピング
    '    map.Add("KBN_NM1", "KBN_NM1")

    '    Dim rtnDs As DataSet = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT_KBN")

    '    Return rtnDs

    'End Function
    'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる

#End Region

#Region "税率マスタの検索"

    ''' <summary>
    ''' 税率マスタの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTax(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '区分マスタ値を取得し、簿外品条件追加対象荷主なのか確認
        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_TAX)             'SQL構築

        'パラメータ設定
        Call Me.SetTaxSelectParameter(ds, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectTax", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ZEI_CD", "ZEI_CD")
        map.Add("TAX_RATE", "TAX_RATE")

        Dim rtnDs As DataSet = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT_TAX")

        Return rtnDs.Copy

    End Function

#End Region

#Region "EXCEL出力データの検索"

    ''' <summary>
    ''' EXCEL出力データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExcel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_EXCEL_OUTKA)       'SQL構築(Select句 出荷)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_EXCEL_OUTKA)  'SQL構築(From句 出荷)
        Call Me.SetSQLWhereExcelOutka(inTbl.Rows(0))              '条件設定(出荷)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_UNION_EXCEL)       'SQL構築(UNION句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_EXCEL_INKA)        'SQL構築(Select句 入荷)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_EXCEL_INKA)   'SQL構築(From句 入荷)
        Call Me.SetSQLWhereExcelInka(inTbl.Rows(0))               '条件設定(入荷)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_UNION_EXCEL)       'SQL構築(UNION句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_EXCEL_UNCHIN)      'SQL構築(Select句 運賃)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_EXCEL_UNCHIN) 'SQL構築(From句 運賃)
        Call Me.SetSQLWhereExcelUnchin(inTbl.Rows(0))             '条件設定(運賃)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_ORDER_EXCEL)       'SQL構築(ORDER句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectExcel", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("DEPART", "DEPART")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("SEIQ_PKG_UT", "SEIQ_PKG_UT")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("UNSO_NO", "UNSO_NO")
        map.Add("INOUTKA_CTL_NO", "INOUTKA_CTL_NO")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SHI", "SHI")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("TRS", "TRS")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("DUPONT_PKG_UT", "DUPONT_PKG_UT")
        map.Add("PRT_TYPE1", "PRT_TYPE1")
        map.Add("PRT_TYPE2", "PRT_TYPE2")
        map.Add("PRT_TYPE3", "PRT_TYPE3")
        map.Add("PRT_TYPE4", "PRT_TYPE4")
        map.Add("PRT_TYPE5", "PRT_TYPE5")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("DEST_NM", "DEST_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT_EXCEL")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "データの検索(件数)"

    ''' <summary>
    ''' データの検索(件数)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_KENSAKU_COUNT)       'SQL構築(Select句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_KENSAKU)        'SQL構築(From句)
        'START YANAI 要望番号830
        Call Me.SetSQLWhereSelectData3(inTbl.Rows(0))                               '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_KENSAKU2)        'SQL構築(From句)
        Call Me.SetSQLWhereSelectData3(inTbl.Rows(0))                               '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_KENSAKU3)        'SQL構築(From句)
        'END YANAI 要望番号830
        Call Me.SetSQLWhereSelectData(inTbl.Rows(0))                               '条件設定
        Call Me.SetSQLWhereSelectData2(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_GROUP_KENSAKU)       'SQL構築(Group句)

        'スキーマ名設定
        'START YANAI 要望番号830
        'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())
        'END YANAI 要望番号830

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        Dim readerCnt As Integer = 0
        Do While reader.Read
            readerCnt = readerCnt + 1
        Loop
        reader.Close()

        '件数設定
        MyBase.SetResultCount(readerCnt)
        Return ds

    End Function

#End Region

#Region "データの検索"

    ''' <summary>
    ''' データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_KENSAKU)        'SQL構築(Select句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_KENSAKU)   'SQL構築(From句)
        'START YANAI 要望番号830
        Call Me.SetSQLWhereSelectData3(inTbl.Rows(0))                               '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_KENSAKU2)        'SQL構築(From句)
        Call Me.SetSQLWhereSelectData3(inTbl.Rows(0))                               '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_KENSAKU3)        'SQL構築(From句)
        'END YANAI 要望番号830
        Call Me.SetSQLWhereSelectData(inTbl.Rows(0))                               '条件設定
        Call Me.SetSQLWhereSelectData2(inTbl.Rows(0))           '条件設定
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_GROUP_KENSAKU)  'SQL構築(Group句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_ORDER_KENSAKU)  'SQL構築(Order句)

        'スキーマ名設定
        'START YANAI 要望番号830
        'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())
        'END YANAI 要望番号830

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("DEPART", "DEPART")
        map.Add("DEPART_NM", "DEPART_NM")
        map.Add("SEKY_YM", "SEKY_YM")
        'START YANAI 要望番号830
        map.Add("CUST_NM", "CUST_NM")
        map.Add("TANTO_CD", "TANTO_CD")
        map.Add("TANTO_NM", "TANTO_NM")
        map.Add("SHINCHOKU_KB", "SHINCHOKU_KB")
        map.Add("SHINCHOKU", "SHINCHOKU")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        'END YANAI 要望番号830

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI030OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "荷主明細の検索(件数)"

    'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
    ''' <summary>
    ''' 荷主明細の検索(件数)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustDetails(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI030IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_CUSTDETAILS_COUNT)       'SQL構築(Select句)
        Me._StrSql.Append(LMI030DAC.SQL_SELECT_FROM_CUSTDETAILS)        'SQL構築(From句)

        'パラメータ設定
        Call Me.SetCustDetailsSelectParameter(ds, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI030IN").Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI030DAC", "SelectCustDetails", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '件数設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function
    'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 デュポン請求ファイルの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereInterface(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" INTER.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INTER.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INTER.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INTER.CUST_CD_SS LIKE @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INTER.CUST_CD_S LIKE @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '請求年月From
            whereStr = Mid(.Item("SKYU_DATE_FROM").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INTER.SEKY_YM >= @SKYU_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '請求年月To
            whereStr = Mid(.Item("SKYU_DATE_TO").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INTER.SEKY_YM <= @SKYU_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '事業部
            whereStr = .Item("DEPART").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INTER.DEPART = @DEPART")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", whereStr, DBDataType.CHAR))
            End If

            '請求項目1
            strSqlAppend = String.Empty
            whereStr = .Item("SEKY_KMK1").ToString()
            If ("00").Equals(.Item("SEKY_KMK1").ToString()) = False Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " INTER.SEKY_KMK = @SEKY_KMK1")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK1", whereStr, DBDataType.CHAR))
            End If

            '請求項目2
            whereStr = .Item("SEKY_KMK2").ToString()
            If ("00").Equals(.Item("SEKY_KMK2").ToString()) = False Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " INTER.SEKY_KMK = @SEKY_KMK2")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK2", whereStr, DBDataType.CHAR))
            End If

            '請求項目3
            whereStr = .Item("SEKY_KMK3").ToString()
            If ("00").Equals(.Item("SEKY_KMK3").ToString()) = False Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " INTER.SEKY_KMK = @SEKY_KMK3")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK3", whereStr, DBDataType.CHAR))
            End If

            '請求項目1、2、3の設定
            If String.IsNullOrEmpty(strSqlAppend) = False Then
                strSqlAppend = String.Concat(strSqlAppend, " )")
                Me._StrSql.Append(strSqlAppend)
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 デュポン請求GLマスタの更新"

    ''' <summary>
    ''' デュポン請求GLマスタの更新パラメータ設定1
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSekyGLUpdParameter1(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AMOUNT", .Item("AMOUNT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@VAT_AMOUNT", .Item("VAT_AMOUNT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOUND", .Item("SOUND").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BOND", .Item("BOND").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' デュポン請求GLマスタの更新パラメータ設定2
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSekyGLUpdParameter2(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AMOUNT", .Item("AMOUNT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@VAT_AMOUNT", .Item("VAT_AMOUNT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOUND", .Item("SOUND").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BOND", .Item("BOND").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIDO_FLAG", .Item("JIDO_FLAG").ToString(), DBDataType.CHAR))

        End With

    End Sub
#End Region

#Region "SQL条件設定 デュポン請求GLマスタの削除"

    ''' <summary>
    ''' デュポン請求GLマスタの削除パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSekyGLDelParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", .Item("OUTKAHOKOKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 保管荷役明細印刷テーブルの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereGMeisai(ByVal ds As DataSet, ByVal dr As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty

        With dr

            '営業所 --必須
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(" MEISAI.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '請求日From
            whereStr = .Item("SKYU_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MEISAI.INV_DATE_FROM >= @SKYU_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '請求日To
            whereStr = .Item("SKYU_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MEISAI.INV_DATE_TO <= @SKYU_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_SS = @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_S = @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
            End If

            '事業部
            whereStr = .Item("DEPART").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND RIGHT('00' + CUSTDETAILS.SET_NAIYO,2) = @DEPART")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", whereStr, DBDataType.CHAR))
            End If

        End With

        'START YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        'With ds.Tables("LMI030OUT_KBN")

        '    If 0 < .Rows.Count Then

        '        '荷主コード(大)の値によって、簿外品区分を条件に追加
        '        whereStr = .Rows(0).Item("KBN_NM1").ToString()
        '        If String.IsNullOrEmpty(whereStr) = False Then
        '            Me._StrSql.Append(" AND MEISAI.OFB_KB = '01'")
        '            Me._StrSql.Append(vbNewLine)
        '        End If

        '    End If

        'End With
        '簿外品区分
        'upd koba 20120605
        Me._StrSql.Append(" AND (MEISAI.OFB_KB = '01' AND ISNULL(CUSTDETAILS2.SET_NAIYO,'') <> '01')")
        'Me._StrSql.Append(" AND ((ISNULL(CUSTDETAILS2.SET_NAIYO,'') = '01' AND MEISAI.OFB_KB = '01')")
        'Me._StrSql.Append(vbNewLine)
        'Me._StrSql.Append(" OR (ISNULL(CUSTDETAILS2.SET_NAIYO,'') <> '01' AND MEISAI.OFB_KB <> '01'))")
        Me._StrSql.Append(vbNewLine)
        'END YANAI 要望番号1043 デュポン請求データ作成で簿外品荷主のデータも取込んでいる
        'SHINODA ADD 要望管理2256 Start
        Me._StrSql.Append("AND  MEISAI.SEKY_FLG = '00' ")
        Me._StrSql.Append(vbNewLine)
        'SHINODA ADD 要望管理2256 E n d

    End Sub

    ''' <summary>
    ''' 保管荷役明細印刷テーブルの検索
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetGMeisaSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_TO", .Item("SKYU_DATE_TO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="dr"></param>
    ''' <param name="motoDataKb">10入荷、20出荷、40運送</param>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereFUnchin(ByVal ds As DataSet, ByVal dr As DataRow, ByVal motoDataKb As String)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereColNM As String = String.Empty
        Dim strSqlAppend As String = String.Empty

        With dr

            '営業所 --必須
            whereStr = .Item("NRS_BR_CD").ToString()
            whereColNM = String.Empty
            Select Case motoDataKb
                Case UNCHIN_MOTO_INKA
                    whereColNM = "INL.NRS_BR_CD"
                Case UNCHIN_MOTO_OUTKA
                    whereColNM = "OL.NRS_BR_CD"
                Case UNCHIN_MOTO_UNSO
                    whereColNM = "UL.NRS_BR_CD"
            End Select
            If String.IsNullOrEmpty(whereStr) = False AndAlso String.IsNullOrEmpty(whereColNM) = False Then
                Me._StrSql.Append(String.Concat(" AND ", whereColNM, " = @NRS_BR_CD"))
                Me._StrSql.Append(vbNewLine)
                If setWhereBr = False Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
                    setWhereBr = True
                End If
            End If

            '荷主コード大
            whereStr = .Item("CUST_CD_L").ToString()
            whereColNM = String.Empty
            Select Case motoDataKb
                Case UNCHIN_MOTO_INKA
                    whereColNM = "INL.CUST_CD_L"
                Case UNCHIN_MOTO_OUTKA
                    whereColNM = "OL.CUST_CD_L"
                Case UNCHIN_MOTO_UNSO
                    whereColNM = "UL.CUST_CD_L"
            End Select
            If String.IsNullOrEmpty(whereStr) = False AndAlso String.IsNullOrEmpty(whereColNM) = False Then
                Me._StrSql.Append(String.Concat(" AND ", whereColNM, " = @CUST_CD_L"))
                Me._StrSql.Append(vbNewLine)
                If setWhereCustL = False Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
                    setWhereCustL = True
                End If
            End If

            '荷主コード中
            whereStr = .Item("CUST_CD_M").ToString()
            whereColNM = String.Empty
            Select Case motoDataKb
                Case UNCHIN_MOTO_INKA
                    whereColNM = "INL.CUST_CD_M"
                Case UNCHIN_MOTO_OUTKA
                    whereColNM = "OL.CUST_CD_M"
                Case UNCHIN_MOTO_UNSO
                    whereColNM = "UL.CUST_CD_M"
            End Select
            If String.IsNullOrEmpty(whereStr) = False AndAlso String.IsNullOrEmpty(whereColNM) = False Then
                Me._StrSql.Append(String.Concat(" AND ", whereColNM, " = @CUST_CD_M"))
                Me._StrSql.Append(vbNewLine)
                If setWhereCustM = False Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
                    setWhereCustM = True
                End If
            End If

            '請求日From
            whereStr = .Item("SKYU_DATE_FROM").ToString()
            whereColNM = String.Empty
            Select Case motoDataKb
                Case UNCHIN_MOTO_INKA
                    whereColNM = "INL.INKA_DATE"
                Case UNCHIN_MOTO_OUTKA
                    whereColNM = "OL.OUTKA_PLAN_DATE"
                Case UNCHIN_MOTO_UNSO
                    whereColNM = "UL.OUTKA_PLAN_DATE"
            End Select
            If String.IsNullOrEmpty(whereStr) = False AndAlso String.IsNullOrEmpty(whereColNM) = False Then
                Me._StrSql.Append(String.Concat(" AND ", whereColNM, " >= @SKYU_DATE_FROM"))
                Me._StrSql.Append(vbNewLine)
                If setWhereDateFrom = False Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_FROM", whereStr, DBDataType.CHAR))
                    setWhereDateFrom = True
                End If

            End If

            '請求日To
            whereStr = .Item("SKYU_DATE_TO").ToString()
            whereColNM = String.Empty
            Select Case motoDataKb
                Case UNCHIN_MOTO_INKA
                    whereColNM = "INL.INKA_DATE"
                Case UNCHIN_MOTO_OUTKA
                    whereColNM = "OL.OUTKA_PLAN_DATE"
                Case UNCHIN_MOTO_UNSO
                    whereColNM = "UL.OUTKA_PLAN_DATE"
            End Select
            If String.IsNullOrEmpty(whereStr) = False AndAlso String.IsNullOrEmpty(whereColNM) = False Then
                Me._StrSql.Append(String.Concat(" AND ", whereColNM, " <= @SKYU_DATE_TO"))
                Me._StrSql.Append(vbNewLine)
                If setWhereDateTo = False Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_TO", whereStr, DBDataType.CHAR))
                    setWhereDateTo = True
                End If

            End If

            '荷主極小　ベースで抽出
            whereStr = .Item("CUST_CD_SS").ToString()
            whereColNM = String.Empty
            Select Case motoDataKb
                Case UNCHIN_MOTO_BASE
                    whereColNM = "BASE.CUST_CD_SS"
            End Select
            If String.IsNullOrEmpty(whereStr) = False AndAlso String.IsNullOrEmpty(whereColNM) = False Then
                Me._StrSql.Append(String.Concat(" AND ", whereColNM, " = @CUST_CD_SS"))
                Me._StrSql.Append(vbNewLine)
                If setWhereCustSS = False Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
                    setWhereCustSS = True
                End If

            End If

            '荷主小　ベースで抽出
            whereStr = .Item("CUST_CD_S").ToString()
            whereColNM = String.Empty
            Select Case motoDataKb
                Case UNCHIN_MOTO_BASE
                    whereColNM = "BASE.CUST_CD_S"
            End Select
            If String.IsNullOrEmpty(whereStr) = False AndAlso String.IsNullOrEmpty(whereColNM) = False Then
                Me._StrSql.Append(String.Concat(" AND ", whereColNM, " = @CUST_CD_S"))
                Me._StrSql.Append(vbNewLine)
                If setWhereCustS = False Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
                    setWhereCustS = True
                End If

            End If

            '事業部
            whereStr = .Item("DEPART").ToString()
            whereColNM = String.Empty
            Select Case motoDataKb
                Case UNCHIN_MOTO_BASE
                    whereColNM = "RIGHT('00' + MCD2.SET_NAIYO ,2)"
            End Select
            If String.IsNullOrEmpty(whereStr) = False AndAlso String.IsNullOrEmpty(whereColNM) = False Then
                Me._StrSql.Append(String.Concat(" AND ", whereColNM, " = @DEPART"))
                Me._StrSql.Append(vbNewLine)
                If setWhereDepart = False Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", whereStr, DBDataType.CHAR))
                    setWhereDepart = True
                End If

            End If


        End With

    End Sub

#Region "区分マスタの検索（簿外品検索条件追加対象荷主）"

    ''' <summary>
    ''' 区分マスタの検索パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetKbnOfbSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 税率マスタの検索"

    ''' <summary>
    ''' 税率マスタの検索パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetTaxSelectParameter(ByVal ds As DataSet, ByVal prmList As ArrayList)

        With ds

            prmList.Add(MyBase.GetSqlParameter("@INV_DATE_FROM", .Tables("LMI030IN").Rows(0).Item("SKYU_DATE_FROM").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#Region "SQL条件設定 デュポン請求ファイルの新規追加"

    ''' <summary>
    ''' デュポン請求ファイルの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInterFaceInsParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOUND", .Item("SOUND").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BOND", .Item("BOND").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX", .Item("TAX").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 デュポン請求GLマスタの検索"

    ''' <summary>
    ''' デュポン請求GLマスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSekyGLSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 デュポン請求GLマスタの新規"

    ''' <summary>
    ''' デュポン請求GLマスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSekyGLInsParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COST_CENTER", .Item("COST_CENTER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CTY", .Item("DEST_CTY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AMOUNT", .Item("AMOUNT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@VAT_AMOUNT", .Item("VAT_AMOUNT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOUND", .Item("SOUND").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BOND", .Item("BOND").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIDO_FLAG", .Item("JIDO_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHUDO_FLAG", .Item("SHUDO_FLAG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 EXCEL出力データの検索(出荷)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereExcelOutka(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("OUTKAL.SYS_DEL_FLG = '0'                                       ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAL.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAL.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '出荷日From
            whereStr = .Item("SKYU_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAL.OUTKA_PLAN_DATE >= @SKYU_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '出荷日To
            whereStr = .Item("SKYU_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAL.OUTKA_PLAN_DATE <= @SKYU_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 EXCEL出力データの検索(入荷)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereExcelInka(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("INKAL.SYS_DEL_FLG = '0'                                        ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKAL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKAL.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKAL.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '入荷(予定)日From
            whereStr = .Item("SKYU_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKAL.INKA_DATE >= @SKYU_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入荷(予定)日To
            whereStr = .Item("SKYU_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKAL.INKA_DATE <= @SKYU_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 EXCEL出力データの検索(運賃)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereExcelUnchin(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("UNSOL.SYS_DEL_FLG = '0'                                        ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND UNSOL.MOTO_DATA_KB = '40'                                 ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '出荷予定日From
            whereStr = .Item("SKYU_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE >= @SKYU_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '出荷予定日To
            whereStr = .Item("SKYU_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE <= @SKYU_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSelectData(ByVal dr As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty

        With dr

            '請求年月From
            whereStr = Mid(.Item("SKYU_DATE_FROM").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MPBASE.SEKY_YM >= @SKYU_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '請求年月To
            whereStr = Mid(.Item("SKYU_DATE_TO").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MPBASE.SEKY_YM <= @SKYU_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSelectData2(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE Z1.KBN_GROUP_CD = 'D006'                                    ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            'START YANAI 要望番号830
            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND Z1.KBN_NM1 = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND Z1.KBN_NM1 = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If
            'END YANAI 要望番号830

            '事業部
            whereStr = .Item("DEPART").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND RIGHT('00' + MCD.SET_NAIYO ,2) = @DEPART")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND Z1.KBN_NM2 LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND Z1.KBN_NM3 LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND Z1.KBN_NM5 LIKE @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND Z1.KBN_NM4 LIKE @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

        End With

    End Sub

    'START YANAI 要望番号830
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSelectData3(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MG.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MG.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MG.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
            End If

            '請求年月From
            whereStr = Mid(.Item("SKYU_DATE_FROM").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(MP.INV_DATE_FROM,1,6) >= @SKYU_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
            End If

            '請求年月To
            whereStr = Mid(.Item("SKYU_DATE_TO").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(MP.INV_DATE_FROM,1,6) <= @SKYU_DATE_TO")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub
    'END YANAI 要望番号830

#End Region

#Region "SQL条件設定 荷主明細の検索"

    'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
    ''' <summary>
    ''' 税率マスタの検索パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetCustDetailsSelectParameter(ByVal ds As DataSet, ByVal prmList As ArrayList)

        With ds

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMI030IN").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(.Tables("LMI030IN").Rows(0).Item("CUST_CD_L").ToString(), _
                                                                         .Tables("LMI030IN").Rows(0).Item("CUST_CD_M").ToString(), _
                                                                         .Tables("LMI030IN").Rows(0).Item("CUST_CD_S").ToString(), _
                                                                         .Tables("LMI030IN").Rows(0).Item("CUST_CD_SS").ToString(), "%"), DBDataType.CHAR))

        End With

    End Sub
    'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

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
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String, ByVal mainBrCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        'デュポン業務主営業所トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN_DPN$", MyBase.GetDatabaseName(mainBrCd, DBKbn.TRN))

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
