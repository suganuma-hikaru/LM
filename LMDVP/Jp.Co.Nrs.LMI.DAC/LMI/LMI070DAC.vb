' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI070  : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI070DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI070DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "変数"

    Dim recType1 As String = "1"
    Dim recType2 As String = "2"
    Dim recType3 As String = "3"
    Dim idCd As String = "5"
    Dim inkaishaCd00464593 As String = "00464593"
    Dim custCd00109 As String = "00109"
    Dim kaishaCd38 As String = "38"
    'Dim kaishaCd22 As String = "22"
    Dim costCd5110 As String = "5110"
    Dim costCd6401 As String = "6401"
    Dim costCd6402 As String = "6402"
    Dim hiyo00239410 As String = "00239410"
    Dim hiyo00464595 As String = "00464595"
    Dim hiyo00763573 As String = "00763573"
    Dim tuka As String = "YEN"
    Dim plus As String = "+"
    Dim yusoType As String = "トラック"
    Dim yusoKb As String = "路線"

#End Region

#Region "ダウケミ請求印刷テーブルの検索"

#Region "ダウケミ請求印刷テーブルの検索 SQL SELECT句"

    ''' <summary>
    ''' ダウケミ請求印刷テーブル検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SEIQPRT As String = " SELECT                                                                     " & vbNewLine _
                                               & " SEIQPRT.NRS_BR_CD                             AS NRS_BR_CD                 " & vbNewLine

#End Region

#Region "ダウケミ請求印刷テーブルの検索 SQL FROM句"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SEIQPRT As String = "FROM                                                                   " & vbNewLine _
                                                    & "$LM_TRN$..I_DOW_SEIQ_PRT SEIQPRT                                       " & vbNewLine

#End Region

#End Region

#Region "ダウケミ請求印刷テーブルの削除"

#Region "ダウケミ請求印刷テーブルの削除 SQL DELETE句"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_SEIQPRT As String = "DELETE FROM $LM_TRN$..I_DOW_SEIQ_PRT " & vbNewLine

#End Region

#End Region

    '#Region "ダウケミ請求明細テーブルの検索"

    '#Region "ダウケミ請求明細テーブルの検索 SQL SELECT句"

    '    ''' <summary>
    '    ''' ダウケミ請求明細テーブル検索 SQL SELECT句
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_SELECT_SEIQMEISAI As String = " SELECT                                                                  " & vbNewLine _
    '                                                  & " SEIQMEISAI.NRS_BR_CD                       AS NRS_BR_CD                 " & vbNewLine

    '#End Region

    '#Region "ダウケミ請求明細テーブルの検索 SQL FROM句"

    '    ''' <summary>
    '    ''' ダウケミ請求明細テーブルの検索 SQL FROM句
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_SELECT_FROM_SEIQMEISAI As String = "FROM                                                                " & vbNewLine _
    '                                                       & "$LM_TRN$..I_DOW_SEIQ_MEISAI SEIQMEISAI                              " & vbNewLine _
    '                                                       & "INNER JOIN                                                          " & vbNewLine _
    '                                                       & "$LM_MST$..M_GOODS GOODS                                             " & vbNewLine _
    '                                                       & "ON                                                                  " & vbNewLine _
    '                                                       & "GOODS.NRS_BR_CD = SEIQMEISAI.NRS_BR_CD                              " & vbNewLine _
    '                                                       & "AND                                                                 " & vbNewLine _
    '                                                       & "GOODS.GOODS_CD_NRS = SEIQMEISAI.NRS_GOODS_CD                        " & vbNewLine

    '#End Region

    '#End Region

    '#Region "ダウケミ請求明細テーブルの削除"

    '#Region "ダウケミ請求明細テーブルの削除 SQL DELETE句"

    '    ''' <summary>
    '    ''' ダウケミ請求明細テーブルの削除 SQL DELETE句
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_DELETE_SEIQMEISAI As String = "DELETE FROM $LM_TRN$..I_DOW_SEIQ_MEISAI                             " & vbNewLine _
    '                                                  & "FROM $LM_TRN$..I_DOW_SEIQ_MEISAI SEIQMEISAI                         " & vbNewLine _
    '                                                  & "LEFT JOIN                                                           " & vbNewLine _
    '                                                  & "$LM_MST$..M_GOODS GOODS                                             " & vbNewLine _
    '                                                  & "ON                                                                  " & vbNewLine _
    '                                                  & "GOODS.NRS_BR_CD = SEIQMEISAI.NRS_BR_CD                              " & vbNewLine _
    '                                                  & "AND                                                                 " & vbNewLine _
    '                                                  & "GOODS.GOODS_CD_NRS = SEIQMEISAI.NRS_GOODS_CD                        " & vbNewLine

    '#End Region

    '#End Region

#Region "保管料・荷役料を作成するためのデータの検索"

#Region "保管料・荷役料を作成するためのデータの検索 SQL SELECT句"

    ''' <summary>
    ''' 保管料・荷役料を作成するためのデータの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HOKANNIYAKU As String = " SELECT                                                                   " & vbNewLine _
                                                 & " MEISAIPRT.NRS_BR_CD                           AS NRS_BR_CD                 " & vbNewLine _
                                                 & ",MEISAIPRT.INV_DATE_FROM                       AS SEIQ_YM                   " & vbNewLine _
                                                 & ",GOODS.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
                                                 & ",GOODS.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
                                                 & ",GOODS.GOODS_CD_NRS                            AS GOODS_CD_NRS              " & vbNewLine _
                                                 & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST             " & vbNewLine _
                                                 & ",GOODS.GOODS_NM_1                              AS GOODS_NM                  " & vbNewLine _
                                                 & ",MEISAIPRT.LOT_NO                              AS LOT_NO                    " & vbNewLine _
                                                 & ",GOODS.JAN_CD                                  AS JAN_CD                    " & vbNewLine _
                                                 & ",MEISAIPRT.KISYZAN_NB1                         AS KURIKOSI_1                " & vbNewLine _
                                                 & ",MEISAIPRT.INKO_NB1                            AS IN_NB_1                   " & vbNewLine _
                                                 & ",MEISAIPRT.OUTKO_NB1                           AS OUT_NB_1                  " & vbNewLine _
                                                 & ",MEISAIPRT.KISYZAN_NB2                         AS ZAN_NB_1                  " & vbNewLine _
                                                 & ",MEISAIPRT.INKO_NB2                            AS IN_NB_2                   " & vbNewLine _
                                                 & ",MEISAIPRT.OUTKO_NB2                           AS OUT_NB_2                  " & vbNewLine _
                                                 & ",MEISAIPRT.KISYZAN_NB3                         AS ZAN_NB_2                  " & vbNewLine _
                                                 & ",MEISAIPRT.INKO_NB3                            AS IN_NB_3                   " & vbNewLine _
                                                 & ",MEISAIPRT.OUTKO_NB3                           AS OUT_NB_3                  " & vbNewLine _
                                                 & ",MEISAIPRT.MATUZAN_NB                          AS ZAN_NB_3                  " & vbNewLine _
                                                 & ",MEISAIPRT.KISYZAN_NB2 + MEISAIPRT.KISYZAN_NB3 + MEISAIPRT.MATUZAN_NB AS SEKI_NB " & vbNewLine _
                                                 & ",MEISAIPRT.INKO_NB_TTL1                        AS IN_NB_TTL                 " & vbNewLine _
                                                 & ",MEISAIPRT.OUTKO_NB_TTL1                       AS OUT_NB_TTL                " & vbNewLine _
                                                 & ",MEISAIPRT.STORAGE1                            AS H_TNK_1                   " & vbNewLine _
                                                 & ",MEISAIPRT.HANDLING_IN1                        AS IN_N_TNK_1                " & vbNewLine _
                                                 & ",MEISAIPRT.HANDLING_OUT1                       AS OUT_N_TNK_1               " & vbNewLine _
                                                 & ",MEISAIPRT.STORAGE_AMO_TTL                     AS H_AM_TTL                  " & vbNewLine _
                                                 & ",MEISAIPRT.HANDLING_IN_AMO_TTL                 AS IN_N_AM_CL                " & vbNewLine _
                                                 & ",MEISAIPRT.HANDLING_OUT_AMO_TTL                AS OUT_N_AM_CL               " & vbNewLine _
                                                 & ",MEISAIPRT.HANDLING_AMO_TTL                    AS N_AM_TTL                  " & vbNewLine

#End Region

#Region "保管料・荷役料を作成するためのデータの検索 SQL FROM句"

    ''' <summary>
    ''' 保管料・荷役料を作成するためのデータの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_HOKANNIYAKU As String = "FROM                                                                 " & vbNewLine _
                                                      & "$LM_TRN$..G_SEKY_MEISAI_PRT MEISAIPRT                                  " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = MEISAIPRT.NRS_BR_CD                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = MEISAIPRT.GOODS_CD_NRS                            " & vbNewLine

#End Region

#Region "保管料・荷役料を作成するためのデータの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 保管料・荷役料を作成するためのデータの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_HOKANNIYAKU As String = "ORDER BY                                                            " & vbNewLine _
                                                      & " GOODS.GOODS_CD_NRS                                                    " & vbNewLine _
                                                      & ",GOODS.GOODS_CD_CUST                                                   " & vbNewLine

#End Region

#End Region

    '#Region "ダウケミ請求明細の新規作成 SQL INSERT句"

    '    ''' <summary>
    '    ''' ダウケミ請求明細新規作成 SQL INSERT句
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_INSERT_SEIQMEISAI As String = "INSERT INTO $LM_TRN$..I_DOW_SEIQ_MEISAI           " & vbNewLine _
    '                                                 & " ( 		                                           " & vbNewLine _
    '                                                 & " NRS_BR_CD,                                        " & vbNewLine _
    '                                                 & " CUST_CD_L,                                        " & vbNewLine _
    '                                                 & " CUST_CD_M,                                        " & vbNewLine _
    '                                                 & " SEIQ_YM,                                          " & vbNewLine _
    '                                                 & " NRS_GOODS_CD,                                     " & vbNewLine _
    '                                                 & " LOT_NO,                                           " & vbNewLine _
    '                                                 & " KURIKOSI_1,                                       " & vbNewLine _
    '                                                 & " IN_NB_1,                                          " & vbNewLine _
    '                                                 & " OUT_NB_1,                                         " & vbNewLine _
    '                                                 & " ZAN_NB_1,                                         " & vbNewLine _
    '                                                 & " IN_NB_2,                                          " & vbNewLine _
    '                                                 & " OUT_NB_2,                                         " & vbNewLine _
    '                                                 & " ZAN_NB_2,                                         " & vbNewLine _
    '                                                 & " IN_NB_3,                                          " & vbNewLine _
    '                                                 & " OUT_NB_3,                                         " & vbNewLine _
    '                                                 & " ZAN_NB_3,                                         " & vbNewLine _
    '                                                 & " SEKI_NB,                                          " & vbNewLine _
    '                                                 & " IN_NB_TTL,                                        " & vbNewLine _
    '                                                 & " OUT_NB_TTL,                                       " & vbNewLine _
    '                                                 & " H_TNK_1,                                          " & vbNewLine _
    '                                                 & " IN_N_TNK_1,                                       " & vbNewLine _
    '                                                 & " OUT_N_TNK_1,                                      " & vbNewLine _
    '                                                 & " H_AM_TTL,                                         " & vbNewLine _
    '                                                 & " IN_N_AM_CL,                                       " & vbNewLine _
    '                                                 & " OUT_N_AM_CL,                                      " & vbNewLine _
    '                                                 & " N_AM_TTL,                                         " & vbNewLine _
    '                                                 & " SYS_ENT_DATE,                                     " & vbNewLine _
    '                                                 & " SYS_ENT_TIME,                                     " & vbNewLine _
    '                                                 & " SYS_ENT_PGID,                                     " & vbNewLine _
    '                                                 & " SYS_ENT_USER,                                     " & vbNewLine _
    '                                                 & " SYS_UPD_DATE,                                     " & vbNewLine _
    '                                                 & " SYS_UPD_TIME,                                     " & vbNewLine _
    '                                                 & " SYS_UPD_PGID,                                     " & vbNewLine _
    '                                                 & " SYS_UPD_USER,                                     " & vbNewLine _
    '                                                 & " SYS_DEL_FLG                                       " & vbNewLine _
    '                                                 & " ) VALUES (                                        " & vbNewLine _
    '                                                 & " @NRS_BR_CD,                                       " & vbNewLine _
    '                                                 & " @CUST_CD_L,                                       " & vbNewLine _
    '                                                 & " @CUST_CD_M,                                       " & vbNewLine _
    '                                                 & " @SEIQ_YM,                                         " & vbNewLine _
    '                                                 & " @NRS_GOODS_CD,                                    " & vbNewLine _
    '                                                 & " @LOT_NO,                                          " & vbNewLine _
    '                                                 & " @KURIKOSI_1,                                      " & vbNewLine _
    '                                                 & " @IN_NB_1,                                         " & vbNewLine _
    '                                                 & " @OUT_NB_1,                                        " & vbNewLine _
    '                                                 & " @ZAN_NB_1,                                        " & vbNewLine _
    '                                                 & " @IN_NB_2,                                         " & vbNewLine _
    '                                                 & " @OUT_NB_2,                                        " & vbNewLine _
    '                                                 & " @ZAN_NB_2,                                        " & vbNewLine _
    '                                                 & " @IN_NB_3,                                         " & vbNewLine _
    '                                                 & " @OUT_NB_3,                                        " & vbNewLine _
    '                                                 & " @ZAN_NB_3,                                        " & vbNewLine _
    '                                                 & " @SEKI_NB,                                         " & vbNewLine _
    '                                                 & " @IN_NB_TTL,                                       " & vbNewLine _
    '                                                 & " @OUT_NB_TTL,                                      " & vbNewLine _
    '                                                 & " @H_TNK_1,                                         " & vbNewLine _
    '                                                 & " @IN_N_TNK_1,                                      " & vbNewLine _
    '                                                 & " @OUT_N_TNK_1,                                     " & vbNewLine _
    '                                                 & " @H_AM_TTL,                                        " & vbNewLine _
    '                                                 & " @IN_N_AM_CL,                                      " & vbNewLine _
    '                                                 & " @OUT_N_AM_CL,                                     " & vbNewLine _
    '                                                 & " @N_AM_TTL,                                        " & vbNewLine _
    '                                                 & " @SYS_ENT_DATE,                                    " & vbNewLine _
    '                                                 & " @SYS_ENT_TIME,                                    " & vbNewLine _
    '                                                 & " @SYS_ENT_PGID,                                    " & vbNewLine _
    '                                                 & " @SYS_ENT_USER,                                    " & vbNewLine _
    '                                                 & " @SYS_UPD_DATE,                                    " & vbNewLine _
    '                                                 & " @SYS_UPD_TIME,                                    " & vbNewLine _
    '                                                 & " @SYS_UPD_PGID,                                    " & vbNewLine _
    '                                                 & " @SYS_UPD_USER,                                    " & vbNewLine _
    '                                                 & " @SYS_DEL_FLG                                      " & vbNewLine _
    '                                                 & " )                                                 " & vbNewLine

    '#End Region

#Region "作業料を作成するためのデータの検索"

#Region "作業料を作成するためのデータの検索 SQL SELECT句"

    ''' <summary>
    ''' 作業料を作成するためのデータの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYO As String = " SELECT                                                                         " & vbNewLine _
                                                 & " SAGYO.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
                                                 & ",SAGYO.SAGYO_COMP_DATE                         AS SAGYO_COMP_DATE           " & vbNewLine _
                                                 & ",SAGYO.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
                                                 & ",SAGYO.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
                                                 & ",GOODS.JAN_CD                                  AS JAN_CD                    " & vbNewLine _
                                                 & ",SAGYO.GOODS_NM_NRS                            AS GOODS_NM                  " & vbNewLine _
                                                 & ",SAGYO.SAGYO_GK                                AS SAGYO_GK                  " & vbNewLine _
                                                 & ",SAGYO.SKYU_CHK                                AS SKYU_CHK                  " & vbNewLine _
                                                 & ",SAGYO.SEIQTO_CD                               AS SEIQTO_CD                 " & vbNewLine _
                                                 & ",SAGYO.INOUTKA_NO_LM                           AS INOUTKA_NO_LM             " & vbNewLine _
                                                 & ",CASE WHEN SAGYO.IOZS_KB = '10'                                             " & vbNewLine _
                                                 & "      THEN INKAL.OUTKA_FROM_ORD_NO_L                                        " & vbNewLine _
                                                 & "      WHEN SAGYO.IOZS_KB = '11'                                             " & vbNewLine _
                                                 & "      THEN INKAM.OUTKA_FROM_ORD_NO_M                                        " & vbNewLine _
                                                 & "      WHEN SAGYO.IOZS_KB = '12'                                             " & vbNewLine _
                                                 & "      THEN INKAL.OUTKA_FROM_ORD_NO_L                                        " & vbNewLine _
                                                 & "      WHEN SAGYO.IOZS_KB = '20'                                             " & vbNewLine _
                                                 & "      THEN OUTKAL.CUST_ORD_NO                                               " & vbNewLine _
                                                 & "      WHEN SAGYO.IOZS_KB = '21'                                             " & vbNewLine _
                                                 & "      THEN OUTKAM.CUST_ORD_NO_DTL                                           " & vbNewLine _
                                                 & "      WHEN SAGYO.IOZS_KB = '22'                                             " & vbNewLine _
                                                 & "      THEN OUTKAL.CUST_ORD_NO                                               " & vbNewLine _
                                                 & "      WHEN SAGYO.IOZS_KB = '40'                                             " & vbNewLine _
                                                 & "      THEN UNSOL.BUY_CHU_NO                                                 " & vbNewLine _
                                                 & "      ELSE ''                                                               " & vbNewLine _
                                                 & " END AS CUST_ORD_NO                                                         " & vbNewLine

#End Region

#Region "作業料を作成するためのデータの検索 SQL FROM句"

    ''' <summary>
    ''' 作業料を作成するためのデータの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SAGYO As String = "FROM                                                                       " & vbNewLine _
                                                      & "$LM_TRN$..E_SAGYO SAGYO                                                " & vbNewLine _
                                                      & "LEFT JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = SAGYO.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = SAGYO.GOODS_CD_NRS                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_L INKAL                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAL.NRS_BR_CD = SAGYO.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAL.INKA_NO_L = SUBSTRING(SAGYO.INOUTKA_NO_LM,1,9)                   " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "(SAGYO.IOZS_KB = '10' OR SAGYO.IOZS_KB = '12')                         " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_M INKAM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAM.NRS_BR_CD = SAGYO.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_L = SUBSTRING(SAGYO.INOUTKA_NO_LM,1,9)                   " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.INKA_NO_M = SUBSTRING(SAGYO.INOUTKA_NO_LM,10,3)                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "SAGYO.IOZS_KB = '11'                                                   " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_L OUTKAL                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAL.NRS_BR_CD = SAGYO.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAL.OUTKA_NO_L = SUBSTRING(SAGYO.INOUTKA_NO_LM,1,9)                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "(SAGYO.IOZS_KB = '20' OR SAGYO.IOZS_KB = '22')                         " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAL.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_M OUTKAM                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAM.NRS_BR_CD = SAGYO.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.OUTKA_NO_L = SUBSTRING(SAGYO.INOUTKA_NO_LM,1,9)                 " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.OUTKA_NO_M = SUBSTRING(SAGYO.INOUTKA_NO_LM,10,3)                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "SAGYO.IOZS_KB = '21'                                                   " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..F_UNSO_L UNSOL                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOL.NRS_BR_CD = SAGYO.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOL.INOUTKA_NO_L = SUBSTRING(SAGYO.INOUTKA_NO_LM,1,9)                " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "SAGYO.IOZS_KB = '40'                                                   " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "AND UNSOL.CUST_CD_L = SAGYO.CUST_CD_L                                      " & vbNewLine _
                                                      & "AND UNSOL.CUST_CD_M = SAGYO.CUST_CD_M                                  " & vbNewLine _
                                                      & "AND SAGYO.INOUTKA_NO_LM <> ''                                          " & vbNewLine


#End Region

#Region "作業料を作成するためのデータの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 作業料を作成するためのデータの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_SAGYO As String = "ORDER BY                                                                  " & vbNewLine _
                                                      & " SAGYO.SAGYO_COMP_DATE                                                 " & vbNewLine _
                                                      & ",SAGYO.INOUTKA_NO_LM                                                   " & vbNewLine

#End Region

#End Region

    '#Region "ダウケミ請求運賃テーブルの検索"

    '#Region "ダウケミ請求運賃テーブルの検索 SQL SELECT句"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブル検索 SQL SELECT句
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_SELECT_SEIQUNCHIN As String = " SELECT                                                                     " & vbNewLine _
    '                                                  & " SEIQUNCHIN.NRS_BR_CD                          AS NRS_BR_CD                 " & vbNewLine

    '#End Region

    '#Region "ダウケミ請求運賃テーブルの検索 SQL FROM句"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブルの検索 SQL FROM句
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_SELECT_FROM_SEIQUNCHIN As String = "FROM                                                                   " & vbNewLine _
    '                                                       & "$LM_TRN$..I_DOW_SEIQ_UNCHIN SEIQUNCHIN                                 " & vbNewLine

    '#End Region

    '#End Region

    '#Region "ダウケミ請求運賃テーブルの削除"

    '#Region "ダウケミ請求運賃テーブルの削除 SQL DELETE句"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブルの削除 SQL DELETE句
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_DELETE_SEIQUNCHIN As String = "DELETE FROM $LM_TRN$..I_DOW_SEIQ_UNCHIN " & vbNewLine

    '#End Region

    '#End Region

#Region "運賃を作成するためのデータの検索(出荷)"

#Region "運賃を作成するためのデータの検索(出荷) SQL SELECT句"

    'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
    '''' <summary>
    '''' 運賃を作成するためのデータの検索(出荷) SQL SELECT句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_UNCHIN_OUTKA As String = " SELECT                                                                  " & vbNewLine _
    '                                             & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
    '                                             & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE           " & vbNewLine _
    '                                             & ",UNSOL.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
    '                                             & ",UNSOL.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
    '                                             & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB              " & vbNewLine _
    '                                             & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                 " & vbNewLine _
    '                                             & ",UNSOM.UNSO_NO_M                               AS UNSO_NO_M                 " & vbNewLine _
    '                                             & ",OUTKAL.OUTKA_NO_L                             AS INOUTKA_NO_L              " & vbNewLine _
    '                                             & ",OUTKAM.OUTKA_NO_M                             AS INOUTKA_NO_M              " & vbNewLine _
    '                                             & ",''                                            AS INOUTKA_NO_S              " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
    '                                             & ",CASE WHEN UNCHIN.SEIQ_NG_NB <> '0'                                         " & vbNewLine _
    '                                             & "      THEN UNCHIN.SEIQ_NG_NB                                                " & vbNewLine _
    '                                             & "      ELSE OUTKAM.OUTKA_TTL_NB                                              " & vbNewLine _
    '                                             & " END AS SEIQ_NG_NB                                                          " & vbNewLine _
    '                                             & ",OUTKAM.OUTKA_TTL_NB                           AS OUTKA_TTL_NB              " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
    '                                             & ",UNCHIN.DECI_UNCHIN                            AS DECI_UNCHIN               " & vbNewLine _
    '                                             & ",UNCHIN.DECI_CITY_EXTC                         AS DECI_CITY_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_WINT_EXTC                         AS DECI_WINT_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_RELY_EXTC                         AS DECI_RELY_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_TOLL                              AS DECI_TOLL                 " & vbNewLine _
    '                                             & ",UNCHIN.DECI_INSU                              AS DECI_INSU                 " & vbNewLine _
    '                                             & ",GOODS.STD_WT_KGS                              AS STD_WT_KGS                " & vbNewLine _
    '                                             & ",GOODS.STD_IRIME_NB                            AS STD_IRIME_NB              " & vbNewLine _
    '                                             & ",UNCHIN.REMARK                                 AS UNCHIN_REMARK             " & vbNewLine _
    '                                             & ",UNSOL.REMARK                                  AS UNSOL_REMARK              " & vbNewLine _
    '                                             & ",OUTKAL.DEST_CD                                AS DEST_CD                   " & vbNewLine _
    '                                             & ",CASE WHEN OUTKAL.DEST_KB = '01'                                            " & vbNewLine _
    '                                             & "      THEN OUTKAL.DEST_NM                                                   " & vbNewLine _
    '                                             & "      ELSE DEST.DEST_NM                                                     " & vbNewLine _
    '                                             & " END AS DEST_NM                                                             " & vbNewLine _
    '                                             & ",CASE WHEN OUTKAL.DEST_KB = '01'                                            " & vbNewLine _
    '                                             & "      THEN OUTKAL.DEST_AD_1                                                 " & vbNewLine _
    '                                             & "      ELSE DEST.AD_1                                                        " & vbNewLine _
    '                                             & " END AS DEST_AD_1                                                           " & vbNewLine _
    '                                             & ",CASE WHEN OUTKAL.DEST_KB = '01'                                            " & vbNewLine _
    '                                             & "      THEN OUTKAL.DEST_AD_2                                                 " & vbNewLine _
    '                                             & "      ELSE DEST.AD_2                                                        " & vbNewLine _
    '                                             & " END AS DEST_AD_2                                                           " & vbNewLine _
    '                                             & ",OUTKAL.DEST_AD_3                              AS DEST_AD_3                 " & vbNewLine _
    '                                             & ",DEST.JIS                                      AS JIS                       " & vbNewLine _
    '                                             & ",JIS.KEN                                       AS KEN                       " & vbNewLine _
    '                                             & ",UNSOL.UNSO_CD                                 AS UNSO_CD                   " & vbNewLine _
    '                                             & ",UNSOCO.UNSOCO_NM                              AS UNSOCO_NM                 " & vbNewLine _
    '                                             & ",OUTKAM.GOODS_CD_NRS                           AS GOODS_CD_NRS              " & vbNewLine _
    '                                             & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST             " & vbNewLine _
    '                                             & ",GOODS.GOODS_NM_1                              AS GOODS_NM                  " & vbNewLine _
    '                                             & ",OUTKAM.LOT_NO                                 AS LOT_NO                    " & vbNewLine _
    '                                             & ",GOODS.JAN_CD                                  AS JAN_CD                    " & vbNewLine _
    '                                             & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
    '                                             & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
    '                                             & " UNCHIN.DECI_INSU                              AS DECI_KINGAKU              " & vbNewLine _
    '                                             & ",''                                            AS CAL_WT                    " & vbNewLine _
    '                                             & ",''                                            AS CAL_KINGAKU               " & vbNewLine _
    '                                             & ",''                                            AS CAL_NB                    " & vbNewLine
    ''' <summary>
    ''' 運賃を作成するためのデータの検索(出荷) SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNCHIN_OUTKA As String = " SELECT                                                                  " & vbNewLine _
                                                 & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
                                                 & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                 & ",UNSOL.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
                                                 & ",UNSOL.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
                                                 & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB              " & vbNewLine _
                                                 & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                 " & vbNewLine _
                                                 & ",UNSOM.UNSO_NO_M                               AS UNSO_NO_M                 " & vbNewLine _
                                                 & ",OUTKAL.OUTKA_NO_L                             AS INOUTKA_NO_L              " & vbNewLine _
                                                 & ",OUTKAM.OUTKA_NO_M                             AS INOUTKA_NO_M              " & vbNewLine _
                                                 & ",''                                            AS INOUTKA_NO_S              " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
                                                 & ",CASE WHEN UNCHIN.SEIQ_NG_NB <> '0'                                         " & vbNewLine _
                                                 & "      THEN UNCHIN.SEIQ_NG_NB                                                " & vbNewLine _
                                                 & "      ELSE OUTKAM.OUTKA_TTL_NB                                              " & vbNewLine _
                                                 & " END AS SEIQ_NG_NB                                                          " & vbNewLine _
                                                 & ",OUTKAM.OUTKA_TTL_NB                           AS OUTKA_TTL_NB              " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN                            AS DECI_UNCHIN               " & vbNewLine _
                                                 & ",UNCHIN.DECI_CITY_EXTC                         AS DECI_CITY_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_WINT_EXTC                         AS DECI_WINT_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_RELY_EXTC                         AS DECI_RELY_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_TOLL                              AS DECI_TOLL                 " & vbNewLine _
                                                 & ",UNCHIN.DECI_INSU                              AS DECI_INSU                 " & vbNewLine _
                                                 & ",GOODS.STD_WT_KGS                              AS STD_WT_KGS                " & vbNewLine _
                                                 & ",GOODS.STD_IRIME_NB                            AS STD_IRIME_NB              " & vbNewLine _
                                                 & ",UNCHIN.REMARK                                 AS UNCHIN_REMARK             " & vbNewLine _
                                                 & ",UNSOL.REMARK                                  AS UNSOL_REMARK              " & vbNewLine _
                                                 & ",OUTKAL.DEST_CD                                AS DEST_CD                   " & vbNewLine _
                                                 & ",CASE WHEN OUTKAL.DEST_KB = '01'                                            " & vbNewLine _
                                                 & "      THEN OUTKAL.DEST_NM                                                   " & vbNewLine _
                                                 & "      ELSE DEST.DEST_NM                                                     " & vbNewLine _
                                                 & " END AS DEST_NM                                                             " & vbNewLine _
                                                 & ",CASE WHEN OUTKAL.DEST_KB = '01'                                            " & vbNewLine _
                                                 & "      THEN OUTKAL.DEST_AD_1                                                 " & vbNewLine _
                                                 & "      ELSE DEST.AD_1                                                        " & vbNewLine _
                                                 & " END AS DEST_AD_1                                                           " & vbNewLine _
                                                 & ",CASE WHEN OUTKAL.DEST_KB = '01'                                            " & vbNewLine _
                                                 & "      THEN OUTKAL.DEST_AD_2                                                 " & vbNewLine _
                                                 & "      ELSE DEST.AD_2                                                        " & vbNewLine _
                                                 & " END AS DEST_AD_2                                                           " & vbNewLine _
                                                 & ",OUTKAL.DEST_AD_3                              AS DEST_AD_3                 " & vbNewLine _
                                                 & ",DEST.JIS                                      AS JIS                       " & vbNewLine _
                                                 & ",JIS.KEN                                       AS KEN                       " & vbNewLine _
                                                 & ",UNSOL.UNSO_CD                                 AS UNSO_CD                   " & vbNewLine _
                                                 & ",UNSOCO.UNSOCO_NM                              AS UNSOCO_NM                 " & vbNewLine _
                                                 & ",OUTKAM.GOODS_CD_NRS                           AS GOODS_CD_NRS              " & vbNewLine _
                                                 & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST             " & vbNewLine _
                                                 & ",GOODS.GOODS_NM_1                              AS GOODS_NM                  " & vbNewLine _
                                                 & ",OUTKAM.LOT_NO                                 AS LOT_NO                    " & vbNewLine _
                                                 & ",GOODS.JAN_CD                                  AS JAN_CD                    " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
                                                 & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
                                                 & " UNCHIN.DECI_INSU                              AS DECI_KINGAKU              " & vbNewLine _
                                                 & ",''                                            AS CAL_WT                    " & vbNewLine _
                                                 & ",''                                            AS CAL_KINGAKU               " & vbNewLine _
                                                 & ",''                                            AS CAL_NB                    " & vbNewLine _
                                                 & ",UNCHIN.DECI_WT                                AS DECI_WT                   " & vbNewLine _
                                                 & ",UNSOL.DENP_NO                                AS DENP_NO                   " & vbNewLine _
    'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

#End Region

#Region "運賃を作成するためのデータの検索(出荷) SQL FROM句"

    ''' <summary>
    ''' 運賃を作成するためのデータの検索(出荷) SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_UNCHIN_OUTKA As String = "FROM                                                                " & vbNewLine _
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
                                                      & "$LM_TRN$..F_UNSO_M UNSOM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
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
                                                      & "OUTKAM.OUTKA_NO_M = UNSOM.UNSO_NO_M                                    " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                               " & vbNewLine _
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
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_UNSOCO UNSOCO                                              " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                 " & vbNewLine

#End Region

#Region "運賃を作成するためのデータの検索(出荷) SQL ORDER BY句"

    ''' <summary>
    ''' 運賃を作成するためのデータの検索(出荷) SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_UNCHIN_OUTKA As String = "ORDER BY                                                           " & vbNewLine _
                                                      & " UNSOL.OUTKA_PLAN_DATE                                                 " & vbNewLine _
                                                      & ",OUTKAL.OUTKA_NO_L                                                     " & vbNewLine _
                                                      & ",OUTKAM.OUTKA_NO_M DESC                                                " & vbNewLine

#End Region

#End Region

#Region "運賃を作成するためのデータの検索(入荷)"

#Region "運賃を作成するためのデータの検索(入荷) SQL SELECT句"

    'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
    '''' <summary>
    '''' 運賃を作成するためのデータの検索(入荷) SQL SELECT句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_UNCHIN_INKA As String = " SELECT                                                                   " & vbNewLine _
    '                                             & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
    '                                             & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE           " & vbNewLine _
    '                                             & ",UNSOL.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
    '                                             & ",UNSOL.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
    '                                             & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB              " & vbNewLine _
    '                                             & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                 " & vbNewLine _
    '                                             & ",UNSOM.UNSO_NO_M                               AS UNSO_NO_M                 " & vbNewLine _
    '                                             & ",INKAL.INKA_NO_L                               AS INOUTKA_NO_L              " & vbNewLine _
    '                                             & ",INKAM.INKA_NO_M                               AS INOUTKA_NO_M              " & vbNewLine _
    '                                             & ",INKAS.INKA_NO_S                               AS INOUTKA_NO_S              " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
    '                                             & ",CASE WHEN UNCHIN.SEIQ_NG_NB <> '0'                                         " & vbNewLine _
    '                                             & "      THEN UNCHIN.SEIQ_NG_NB                                                " & vbNewLine _
    '                                             & "      ELSE (INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU                        " & vbNewLine _
    '                                             & " END AS SEIQ_NG_NB                                                          " & vbNewLine _
    '                                             & ",''                                            AS OUTKA_TTL_NB              " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
    '                                             & ",UNCHIN.DECI_UNCHIN                            AS DECI_UNCHIN               " & vbNewLine _
    '                                             & ",UNCHIN.DECI_CITY_EXTC                         AS DECI_CITY_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_WINT_EXTC                         AS DECI_WINT_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_RELY_EXTC                         AS DECI_RELY_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_TOLL                              AS DECI_TOLL                 " & vbNewLine _
    '                                             & ",UNCHIN.DECI_INSU                              AS DECI_INSU                 " & vbNewLine _
    '                                             & ",GOODS.STD_WT_KGS                              AS STD_WT_KGS                " & vbNewLine _
    '                                             & ",GOODS.STD_IRIME_NB                            AS STD_IRIME_NB              " & vbNewLine _
    '                                             & ",UNCHIN.REMARK                                 AS UNCHIN_REMARK             " & vbNewLine _
    '                                             & ",''                                            AS UNSOL_REMARK              " & vbNewLine _
    '                                             & ",''                                            AS DEST_CD                   " & vbNewLine _
    '                                             & ",''                                            AS DEST_NM                   " & vbNewLine _
    '                                             & ",''                                            AS DEST_AD_1                 " & vbNewLine _
    '                                             & ",''                                            AS DEST_AD_2                 " & vbNewLine _
    '                                             & ",''                                            AS DEST_AD_3                 " & vbNewLine _
    '                                             & ",''                                            AS JIS                       " & vbNewLine _
    '                                             & ",''                                            AS KEN                       " & vbNewLine _
    '                                             & ",UNSOL.UNSO_CD                                 AS UNSO_CD                   " & vbNewLine _
    '                                             & ",UNSOCO.UNSOCO_NM                              AS UNSOCO_NM                 " & vbNewLine _
    '                                             & ",INKAM.GOODS_CD_NRS                            AS GOODS_CD_NRS              " & vbNewLine _
    '                                             & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST             " & vbNewLine _
    '                                             & ",GOODS.GOODS_NM_1                              AS GOODS_NM                  " & vbNewLine _
    '                                             & ",INKAS.LOT_NO                                  AS LOT_NO                    " & vbNewLine _
    '                                             & ",GOODS.JAN_CD                                  AS JAN_CD                    " & vbNewLine _
    '                                             & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
    '                                             & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
    '                                             & " UNCHIN.DECI_INSU                              AS DECI_KINGAKU              " & vbNewLine _
    '                                             & ",INKAS.BETU_WT                                 AS CAL_WT                    " & vbNewLine _
    '                                             & ",''                                            AS CAL_KINGAKU               " & vbNewLine _
    '                                             & ",(INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU     AS CAL_NB                    " & vbNewLine
    ''' <summary>
    ''' 運賃を作成するためのデータの検索(入荷) SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNCHIN_INKA As String = " SELECT                                                                   " & vbNewLine _
                                                 & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
                                                 & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                 & ",UNSOL.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
                                                 & ",UNSOL.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
                                                 & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB              " & vbNewLine _
                                                 & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                 " & vbNewLine _
                                                 & ",UNSOM.UNSO_NO_M                               AS UNSO_NO_M                 " & vbNewLine _
                                                 & ",INKAL.INKA_NO_L                               AS INOUTKA_NO_L              " & vbNewLine _
                                                 & ",INKAM.INKA_NO_M                               AS INOUTKA_NO_M              " & vbNewLine _
                                                 & ",INKAS.INKA_NO_S                               AS INOUTKA_NO_S              " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
                                                 & ",CASE WHEN UNCHIN.SEIQ_NG_NB <> '0'                                         " & vbNewLine _
                                                 & "      THEN UNCHIN.SEIQ_NG_NB                                                " & vbNewLine _
                                                 & "      ELSE (INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU                        " & vbNewLine _
                                                 & " END AS SEIQ_NG_NB                                                          " & vbNewLine _
                                                 & ",''                                            AS OUTKA_TTL_NB              " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN                            AS DECI_UNCHIN               " & vbNewLine _
                                                 & ",UNCHIN.DECI_CITY_EXTC                         AS DECI_CITY_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_WINT_EXTC                         AS DECI_WINT_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_RELY_EXTC                         AS DECI_RELY_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_TOLL                              AS DECI_TOLL                 " & vbNewLine _
                                                 & ",UNCHIN.DECI_INSU                              AS DECI_INSU                 " & vbNewLine _
                                                 & ",GOODS.STD_WT_KGS                              AS STD_WT_KGS                " & vbNewLine _
                                                 & ",GOODS.STD_IRIME_NB                            AS STD_IRIME_NB              " & vbNewLine _
                                                 & ",UNCHIN.REMARK                                 AS UNCHIN_REMARK             " & vbNewLine _
                                                 & ",''                                            AS UNSOL_REMARK              " & vbNewLine _
                                                 & ",''                                            AS DEST_CD                   " & vbNewLine _
                                                 & ",''                                            AS DEST_NM                   " & vbNewLine _
                                                 & ",''                                            AS DEST_AD_1                 " & vbNewLine _
                                                 & ",''                                            AS DEST_AD_2                 " & vbNewLine _
                                                 & ",''                                            AS DEST_AD_3                 " & vbNewLine _
                                                 & ",''                                            AS JIS                       " & vbNewLine _
                                                 & ",''                                            AS KEN                       " & vbNewLine _
                                                 & ",UNSOL.UNSO_CD                                 AS UNSO_CD                   " & vbNewLine _
                                                 & ",UNSOCO.UNSOCO_NM                              AS UNSOCO_NM                 " & vbNewLine _
                                                 & ",INKAM.GOODS_CD_NRS                            AS GOODS_CD_NRS              " & vbNewLine _
                                                 & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST             " & vbNewLine _
                                                 & ",GOODS.GOODS_NM_1                              AS GOODS_NM                  " & vbNewLine _
                                                 & ",INKAS.LOT_NO                                  AS LOT_NO                    " & vbNewLine _
                                                 & ",GOODS.JAN_CD                                  AS JAN_CD                    " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
                                                 & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
                                                 & " UNCHIN.DECI_INSU                              AS DECI_KINGAKU              " & vbNewLine _
                                                 & ",INKAS.BETU_WT                                 AS CAL_WT                    " & vbNewLine _
                                                 & ",''                                            AS CAL_KINGAKU               " & vbNewLine _
                                                 & ",(INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU     AS CAL_NB                    " & vbNewLine _
                                                 & ",UNCHIN.DECI_WT                                AS DECI_WT                   " & vbNewLine _
    'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

#End Region

#Region "運賃を作成するためのデータの検索(入荷) SQL FROM句"

    ''' <summary>
    ''' 運賃を作成するためのデータの検索(入荷) SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_UNCHIN_INKA As String = "FROM                                                                 " & vbNewLine _
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
                                                      & "$LM_TRN$..F_UNSO_M UNSOM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
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
                                                      & "INKAM.INKA_NO_M = UNSOM.UNSO_NO_M                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_TRN$..B_INKA_S INKAS                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAS.INKA_NO_L = INKAM.INKA_NO_L                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAS.INKA_NO_M = INKAM.INKA_NO_M                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "INKAS.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_UNSOCO UNSOCO                                              " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                 " & vbNewLine

#End Region

#Region "運賃を作成するためのデータの検索(入荷) SQL ORDER BY句"

    ''' <summary>
    ''' 運賃を作成するためのデータの検索(入荷) SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_UNCHIN_INKA As String = "ORDER BY                                                            " & vbNewLine _
                                                      & " UNSOL.OUTKA_PLAN_DATE                                                 " & vbNewLine _
                                                      & ",INKAL.INKA_NO_L                                                       " & vbNewLine _
                                                      & ",INKAM.INKA_NO_M DESC                                                  " & vbNewLine _
                                                      & ",INKAS.INKA_NO_S DESC                                                  " & vbNewLine

#End Region

#End Region

#Region "運賃を作成するためのデータの検索(運賃)"

#Region "運賃を作成するためのデータの検索(運賃) SQL SELECT句"

    'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
    '''' <summary>
    '''' 運賃を作成するためのデータの検索(運賃) SQL SELECT句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_UNCHIN As String = " SELECT                                                                        " & vbNewLine _
    '                                             & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
    '                                             & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE           " & vbNewLine _
    '                                             & ",UNSOL.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
    '                                             & ",UNSOL.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
    '                                             & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB              " & vbNewLine _
    '                                             & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                 " & vbNewLine _
    '                                             & ",UNSOM.UNSO_NO_M                               AS UNSO_NO_M                 " & vbNewLine _
    '                                             & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L              " & vbNewLine _
    '                                             & ",''                                            AS INOUTKA_NO_M              " & vbNewLine _
    '                                             & ",''                                            AS INOUTKA_NO_S              " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
    '                                             & ",CASE WHEN UNCHIN.SEIQ_NG_NB <> '0'                                         " & vbNewLine _
    '                                             & "      THEN UNCHIN.SEIQ_NG_NB                                                " & vbNewLine _
    '                                             & "      ELSE UNSOL.UNSO_PKG_NB                                                " & vbNewLine _
    '                                             & " END AS SEIQ_NG_NB                                                          " & vbNewLine _
    '                                             & ",''                                            AS OUTKA_TTL_NB              " & vbNewLine _
    '                                             & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
    '                                             & ",UNCHIN.DECI_UNCHIN                            AS DECI_UNCHIN               " & vbNewLine _
    '                                             & ",UNCHIN.DECI_CITY_EXTC                         AS DECI_CITY_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_WINT_EXTC                         AS DECI_WINT_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_RELY_EXTC                         AS DECI_RELY_EXTC            " & vbNewLine _
    '                                             & ",UNCHIN.DECI_TOLL                              AS DECI_TOLL                 " & vbNewLine _
    '                                             & ",UNCHIN.DECI_INSU                              AS DECI_INSU                 " & vbNewLine _
    '                                             & ",GOODS.STD_WT_KGS                              AS STD_WT_KGS                " & vbNewLine _
    '                                             & ",GOODS.STD_IRIME_NB                            AS STD_IRIME_NB              " & vbNewLine _
    '                                             & ",UNCHIN.REMARK                                 AS UNCHIN_REMARK             " & vbNewLine _
    '                                             & ",''                                            AS UNSOL_REMARK              " & vbNewLine _
    '                                             & ",UNSOL.DEST_CD                                 AS DEST_CD                   " & vbNewLine _
    '                                             & ",DEST.DEST_NM                                  AS DEST_NM                   " & vbNewLine _
    '                                             & ",DEST.AD_1                                     AS DEST_AD_1                 " & vbNewLine _
    '                                             & ",DEST.AD_2                                     AS DEST_AD_2                 " & vbNewLine _
    '                                             & ",DEST.AD_3                                     AS DEST_AD_3                 " & vbNewLine _
    '                                             & ",DEST.JIS                                      AS JIS                       " & vbNewLine _
    '                                             & ",JIS.KEN                                       AS KEN                       " & vbNewLine _
    '                                             & ",UNSOL.UNSO_CD                                 AS UNSO_CD                   " & vbNewLine _
    '                                             & ",UNSOCO.UNSOCO_NM                              AS UNSOCO_NM                 " & vbNewLine _
    '                                             & ",UNSOM.GOODS_CD_NRS                            AS GOODS_CD_NRS              " & vbNewLine _
    '                                             & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST             " & vbNewLine _
    '                                             & ",GOODS.GOODS_NM_1                              AS GOODS_NM                  " & vbNewLine _
    '                                             & ",''                                            AS LOT_NO                    " & vbNewLine _
    '                                             & ",GOODS.JAN_CD                                  AS JAN_CD                    " & vbNewLine _
    '                                             & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
    '                                             & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
    '                                             & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
    '                                             & " UNCHIN.DECI_INSU                              AS DECI_KINGAKU              " & vbNewLine _
    '                                             & ",UNSOM.BETU_WT                                 AS CAL_WT                    " & vbNewLine _
    '                                             & ",''                                            AS CAL_KINGAKU               " & vbNewLine _
    '                                             & ",UNSOL.UNSO_PKG_NB                             AS CAL_NB                    " & vbNewLine
    ''' <summary>
    ''' 運賃を作成するためのデータの検索(運賃) SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNCHIN As String = " SELECT                                                                        " & vbNewLine _
                                                 & " UNSOL.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
                                                 & ",UNSOL.OUTKA_PLAN_DATE                         AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                 & ",UNSOL.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
                                                 & ",UNSOL.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
                                                 & ",UNSOL.MOTO_DATA_KB                            AS MOTO_DATA_KB              " & vbNewLine _
                                                 & ",UNSOL.UNSO_NO_L                               AS UNSO_NO_L                 " & vbNewLine _
                                                 & ",UNSOM.UNSO_NO_M                               AS UNSO_NO_M                 " & vbNewLine _
                                                 & ",UNSOL.INOUTKA_NO_L                            AS INOUTKA_NO_L              " & vbNewLine _
                                                 & ",''                                            AS INOUTKA_NO_M              " & vbNewLine _
                                                 & ",''                                            AS INOUTKA_NO_S              " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_KYORI                             AS SEIQ_KYORI                " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_TARIFF_CD                         AS SEIQ_TARIFF_CD            " & vbNewLine _
                                                 & ",CASE WHEN UNCHIN.SEIQ_NG_NB <> '0'                                         " & vbNewLine _
                                                 & "      THEN UNCHIN.SEIQ_NG_NB                                                " & vbNewLine _
                                                 & "      ELSE UNSOL.UNSO_PKG_NB                                                " & vbNewLine _
                                                 & " END AS SEIQ_NG_NB                                                          " & vbNewLine _
                                                 & ",''                                            AS OUTKA_TTL_NB              " & vbNewLine _
                                                 & ",UNCHIN.SEIQ_WT                                AS SEIQ_WT                   " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN                            AS DECI_UNCHIN               " & vbNewLine _
                                                 & ",UNCHIN.DECI_CITY_EXTC                         AS DECI_CITY_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_WINT_EXTC                         AS DECI_WINT_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_RELY_EXTC                         AS DECI_RELY_EXTC            " & vbNewLine _
                                                 & ",UNCHIN.DECI_TOLL                              AS DECI_TOLL                 " & vbNewLine _
                                                 & ",UNCHIN.DECI_INSU                              AS DECI_INSU                 " & vbNewLine _
                                                 & ",GOODS.STD_WT_KGS                              AS STD_WT_KGS                " & vbNewLine _
                                                 & ",GOODS.STD_IRIME_NB                            AS STD_IRIME_NB              " & vbNewLine _
                                                 & ",UNCHIN.REMARK                                 AS UNCHIN_REMARK             " & vbNewLine _
                                                 & ",''                                            AS UNSOL_REMARK              " & vbNewLine _
                                                 & ",UNSOL.DEST_CD                                 AS DEST_CD                   " & vbNewLine _
                                                 & ",DEST.DEST_NM                                  AS DEST_NM                   " & vbNewLine _
                                                 & ",DEST.AD_1                                     AS DEST_AD_1                 " & vbNewLine _
                                                 & ",DEST.AD_2                                     AS DEST_AD_2                 " & vbNewLine _
                                                 & ",DEST.AD_3                                     AS DEST_AD_3                 " & vbNewLine _
                                                 & ",DEST.JIS                                      AS JIS                       " & vbNewLine _
                                                 & ",JIS.KEN                                       AS KEN                       " & vbNewLine _
                                                 & ",UNSOL.UNSO_CD                                 AS UNSO_CD                   " & vbNewLine _
                                                 & ",UNSOCO.UNSOCO_NM                              AS UNSOCO_NM                 " & vbNewLine _
                                                 & ",UNSOM.GOODS_CD_NRS                            AS GOODS_CD_NRS              " & vbNewLine _
                                                 & ",GOODS.GOODS_CD_CUST                           AS GOODS_CD_CUST             " & vbNewLine _
                                                 & ",GOODS.GOODS_NM_1                              AS GOODS_NM                  " & vbNewLine _
                                                 & ",''                                            AS LOT_NO                    " & vbNewLine _
                                                 & ",GOODS.JAN_CD                                  AS JAN_CD                    " & vbNewLine _
                                                 & ",UNCHIN.DECI_UNCHIN +                                                       " & vbNewLine _
                                                 & " UNCHIN.DECI_CITY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_WINT_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_RELY_EXTC +                                                    " & vbNewLine _
                                                 & " UNCHIN.DECI_TOLL +                                                         " & vbNewLine _
                                                 & " UNCHIN.DECI_INSU                              AS DECI_KINGAKU              " & vbNewLine _
                                                 & ",UNSOM.BETU_WT                                 AS CAL_WT                    " & vbNewLine _
                                                 & ",''                                            AS CAL_KINGAKU               " & vbNewLine _
                                                 & ",UNSOL.UNSO_PKG_NB                             AS CAL_NB                    " & vbNewLine _
                                                 & ",UNCHIN.DECI_WT                                AS DECI_WT                   " & vbNewLine _
                                                 & ",UNSOL.DENP_NO                                 AS DENP_NO                   " & vbNewLine _
    'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

#End Region

#Region "運賃を作成するためのデータの検索(運賃) SQL FROM句"

    'START YANAI 要望番号961
    '''' <summary>
    '''' 運賃を作成するためのデータの検索(運賃) SQL FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_UNCHIN As String = "FROM                                                                      " & vbNewLine _
    '                                                  & "$LM_TRN$..F_UNSO_L UNSOL                                               " & vbNewLine _
    '                                                  & "INNER JOIN                                                             " & vbNewLine _
    '                                                  & "$LM_TRN$..F_UNCHIN_TRS UNCHIN                                          " & vbNewLine _
    '                                                  & "ON                                                                     " & vbNewLine _
    '                                                  & "UNCHIN.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "UNCHIN.UNSO_NO_L = UNSOL.UNSO_NO_L                                     " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "UNCHIN.SEIQ_FIXED_FLAG = '01'                                          " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "UNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                                  & "INNER JOIN                                                             " & vbNewLine _
    '                                                  & "$LM_TRN$..F_UNSO_M UNSOM                                               " & vbNewLine _
    '                                                  & "ON                                                                     " & vbNewLine _
    '                                                  & "UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                      " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                      " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "UNSOM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
    '                                                  & "INNER JOIN                                                             " & vbNewLine _
    '                                                  & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
    '                                                  & "ON                                                                     " & vbNewLine _
    '                                                  & "GOODS.NRS_BR_CD = UNSOM.NRS_BR_CD                                      " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "GOODS.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                " & vbNewLine _
    '                                                  & "LEFT JOIN                                                              " & vbNewLine _
    '                                                  & "$LM_MST$..M_DEST DEST                                                  " & vbNewLine _
    '                                                  & "ON                                                                     " & vbNewLine _
    '                                                  & "DEST.NRS_BR_CD = UNSOL.NRS_BR_CD                                       " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "DEST.CUST_CD_L = UNSOL.CUST_CD_L                                       " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "DEST.DEST_CD = UNSOL.DEST_CD                                           " & vbNewLine _
    '                                                  & "LEFT JOIN                                                              " & vbNewLine _
    '                                                  & "$LM_MST$..M_JIS JIS                                                    " & vbNewLine _
    '                                                  & "ON                                                                     " & vbNewLine _
    '                                                  & "JIS.JIS_CD = DEST.JIS                                                  " & vbNewLine _
    '                                                  & "LEFT JOIN                                                              " & vbNewLine _
    '                                                  & "$LM_MST$..M_UNSOCO UNSOCO                                              " & vbNewLine _
    '                                                  & "ON                                                                     " & vbNewLine _
    '                                                  & "UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                       " & vbNewLine _
    '                                                  & "AND                                                                    " & vbNewLine _
    '                                                  & "UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                 " & vbNewLine
    ''' <summary>
    ''' 運賃を作成するためのデータの検索(運賃) SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_UNCHIN As String = "FROM                                                                      " & vbNewLine _
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
                                                      & "$LM_TRN$..F_UNSO_M UNSOM                                               " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOM.NRS_BR_CD = UNCHIN.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_L = UNCHIN.UNSO_NO_L                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.UNSO_NO_M = UNCHIN.UNSO_NO_M                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "INNER JOIN                                                             " & vbNewLine _
                                                      & "$LM_MST$..M_GOODS GOODS                                                " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "GOODS.NRS_BR_CD = UNSOM.NRS_BR_CD                                      " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "GOODS.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                " & vbNewLine _
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
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_MST$..M_UNSOCO UNSOCO                                              " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                     " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                       " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                 " & vbNewLine
    'END YANAI 要望番号961

#End Region

#Region "運賃を作成するためのデータの検索(運賃) SQL ORDER BY句"

    ''' <summary>
    ''' 運賃を作成するためのデータの検索(運賃) SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_UNCHIN As String = "ORDER BY                                                                 " & vbNewLine _
                                                      & " UNSOL.OUTKA_PLAN_DATE                                                 " & vbNewLine _
                                                      & ",UNSOL.UNSO_NO_L                                                       " & vbNewLine _
                                                      & ",UNSOM.UNSO_NO_M                                                       " & vbNewLine 

#End Region

#End Region

    '#Region "ダウケミ請求運賃テーブルの新規作成 SQL INSERT句"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブル新規作成 SQL INSERT句
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_INSERT_SEIQUNCHIN As String = "INSERT INTO $LM_TRN$..I_DOW_SEIQ_UNCHIN           " & vbNewLine _
    '                                                 & " ( 		                                           " & vbNewLine _
    '                                                 & " UNSO_NO,                                          " & vbNewLine _
    '                                                 & " UNSO_NO_M,                                        " & vbNewLine _
    '                                                 & " NRS_BR_CD,                                        " & vbNewLine _
    '                                                 & " SEIQ_YM,                                          " & vbNewLine _
    '                                                 & " CUST_CD_L,                                        " & vbNewLine _
    '                                                 & " CUST_CD_M,                                        " & vbNewLine _
    '                                                 & " MOTO_DATA_KB,                                     " & vbNewLine _
    '                                                 & " INOUTKA_CTL_NO,                                   " & vbNewLine _
    '                                                 & " INOUTKA_CTL_NO_M,                                 " & vbNewLine _
    '                                                 & " INOUTKA_CTL_NO_S,                                 " & vbNewLine _
    '                                                 & " OUTKA_PLAN_DATE,                                  " & vbNewLine _
    '                                                 & " SEIQ_KYORI,                                       " & vbNewLine _
    '                                                 & " SEIQ_TARIFF_CD,                                   " & vbNewLine _
    '                                                 & " SEIQ_NG_NB,                                       " & vbNewLine _
    '                                                 & " SEIQ_WT,                                          " & vbNewLine _
    '                                                 & " DECI_KINGAKU,                                     " & vbNewLine _
    '                                                 & " CAL_NB,                                           " & vbNewLine _
    '                                                 & " CAL_WT,                                           " & vbNewLine _
    '                                                 & " CAL_IRIME,                                        " & vbNewLine _
    '                                                 & " CAL_KINGAKU,                                      " & vbNewLine _
    '                                                 & " ORD_NO,                                           " & vbNewLine _
    '                                                 & " SHIP_NO,                                          " & vbNewLine _
    '                                                 & " DEST_CD,                                          " & vbNewLine _
    '                                                 & " DEST_NM,                                          " & vbNewLine _
    '                                                 & " DEST_AD,                                          " & vbNewLine _
    '                                                 & " DEST_JIS_CD,                                      " & vbNewLine _
    '                                                 & " DEST_KEN,                                         " & vbNewLine _
    '                                                 & " UNSO_CD,                                          " & vbNewLine _
    '                                                 & " GOODS_CD_NRS,                                     " & vbNewLine _
    '                                                 & " GOODS_NM,                                         " & vbNewLine _
    '                                                 & " LOT_NO,                                           " & vbNewLine _
    '                                                 & " GMID,                                             " & vbNewLine _
    '                                                 & " YUKO_KIGEN,                                       " & vbNewLine _
    '                                                 & " ORD_ITEM_NO,                                      " & vbNewLine _
    '                                                 & " YUSO_TYPE,                                        " & vbNewLine _
    '                                                 & " YUSO_KB,                                          " & vbNewLine _
    '                                                 & " SYS_ENT_DATE,                                     " & vbNewLine _
    '                                                 & " SYS_ENT_TIME,                                     " & vbNewLine _
    '                                                 & " SYS_ENT_PGID,                                     " & vbNewLine _
    '                                                 & " SYS_ENT_USER,                                     " & vbNewLine _
    '                                                 & " SYS_UPD_DATE,                                     " & vbNewLine _
    '                                                 & " SYS_UPD_TIME,                                     " & vbNewLine _
    '                                                 & " SYS_UPD_PGID,                                     " & vbNewLine _
    '                                                 & " SYS_UPD_USER,                                     " & vbNewLine _
    '                                                 & " SYS_DEL_FLG                                       " & vbNewLine _
    '                                                 & " ) VALUES (                                        " & vbNewLine _
    '                                                 & " @UNSO_NO,                                         " & vbNewLine _
    '                                                 & " @UNSO_NO_M,                                       " & vbNewLine _
    '                                                 & " @NRS_BR_CD,                                       " & vbNewLine _
    '                                                 & " @SEIQ_YM,                                         " & vbNewLine _
    '                                                 & " @CUST_CD_L,                                       " & vbNewLine _
    '                                                 & " @CUST_CD_M,                                       " & vbNewLine _
    '                                                 & " @MOTO_DATA_KB,                                    " & vbNewLine _
    '                                                 & " @INOUTKA_CTL_NO,                                  " & vbNewLine _
    '                                                 & " @INOUTKA_CTL_NO_M,                                " & vbNewLine _
    '                                                 & " @INOUTKA_CTL_NO_S,                                " & vbNewLine _
    '                                                 & " @OUTKA_PLAN_DATE,                                 " & vbNewLine _
    '                                                 & " @SEIQ_KYORI,                                      " & vbNewLine _
    '                                                 & " @SEIQ_TARIFF_CD,                                  " & vbNewLine _
    '                                                 & " @SEIQ_NG_NB,                                      " & vbNewLine _
    '                                                 & " @SEIQ_WT,                                         " & vbNewLine _
    '                                                 & " @DECI_KINGAKU,                                    " & vbNewLine _
    '                                                 & " @CAL_NB,                                          " & vbNewLine _
    '                                                 & " @CAL_WT,                                          " & vbNewLine _
    '                                                 & " @CAL_IRIME,                                       " & vbNewLine _
    '                                                 & " @CAL_KINGAKU,                                     " & vbNewLine _
    '                                                 & " @ORD_NO,                                          " & vbNewLine _
    '                                                 & " @SHIP_NO,                                         " & vbNewLine _
    '                                                 & " @DEST_CD,                                         " & vbNewLine _
    '                                                 & " @DEST_NM,                                         " & vbNewLine _
    '                                                 & " @DEST_AD,                                         " & vbNewLine _
    '                                                 & " @DEST_JIS_CD,                                     " & vbNewLine _
    '                                                 & " @DEST_KEN,                                        " & vbNewLine _
    '                                                 & " @UNSO_CD,                                         " & vbNewLine _
    '                                                 & " @GOODS_CD_NRS,                                    " & vbNewLine _
    '                                                 & " @GOODS_NM,                                        " & vbNewLine _
    '                                                 & " @LOT_NO,                                          " & vbNewLine _
    '                                                 & " @GMID,                                            " & vbNewLine _
    '                                                 & " @YUKO_KIGEN,                                      " & vbNewLine _
    '                                                 & " @ORD_ITEM_NO,                                     " & vbNewLine _
    '                                                 & " @YUSO_TYPE,                                       " & vbNewLine _
    '                                                 & " @YUSO_KB,                                         " & vbNewLine _
    '                                                 & " @SYS_ENT_DATE,                                    " & vbNewLine _
    '                                                 & " @SYS_ENT_TIME,                                    " & vbNewLine _
    '                                                 & " @SYS_ENT_PGID,                                    " & vbNewLine _
    '                                                 & " @SYS_ENT_USER,                                    " & vbNewLine _
    '                                                 & " @SYS_UPD_DATE,                                    " & vbNewLine _
    '                                                 & " @SYS_UPD_TIME,                                    " & vbNewLine _
    '                                                 & " @SYS_UPD_PGID,                                    " & vbNewLine _
    '                                                 & " @SYS_UPD_USER,                                    " & vbNewLine _
    '                                                 & " @SYS_DEL_FLG                                      " & vbNewLine _
    '                                                 & " )                                                 " & vbNewLine

    '#End Region

#Region "ダウケミ請求印刷テーブルの新規作成 SQL INSERT句"

    ''' <summary>
    ''' ダウケミ請求印刷テーブル新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SEIQPRT As String = "INSERT INTO $LM_TRN$..I_DOW_SEIQ_PRT                 " & vbNewLine _
                                                 & " ( 		                                           " & vbNewLine _
                                                 & " NRS_BR_CD,                                        " & vbNewLine _
                                                 & " SEIQ_YM,                                          " & vbNewLine _
                                                 & " CUST_CD_L,                                        " & vbNewLine _
                                                 & " CUST_CD_M,                                        " & vbNewLine _
                                                 & " REC_TYPE,                                         " & vbNewLine _
                                                 & " ID,                                               " & vbNewLine _
                                                 & " SHORI_YM,                                         " & vbNewLine _
                                                 & " IN_KAISHA,                                        " & vbNewLine _
                                                 & " KAISHA_CD,                                        " & vbNewLine _
                                                 & " DV_NO,                                            " & vbNewLine _
                                                 & " GMID,                                             " & vbNewLine _
                                                 & " GOODS_NM,                                         " & vbNewLine _
                                                 & " COST,                                             " & vbNewLine _
                                                 & " HIYO,                                             " & vbNewLine _
                                                 & " TUKA,                                             " & vbNewLine _
                                                 & " GAKU,                                             " & vbNewLine _
                                                 & " FUGO,                                             " & vbNewLine _
                                                 & " HASSEI_YM,                                        " & vbNewLine _
                                                 & " SHIP_NO,                                          " & vbNewLine _
                                                 & " WT,                                               " & vbNewLine _
                                                 & " KYORI,                                            " & vbNewLine _
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
                                                 & " @SEIQ_YM,                                         " & vbNewLine _
                                                 & " @CUST_CD_L,                                       " & vbNewLine _
                                                 & " @CUST_CD_M,                                       " & vbNewLine _
                                                 & " @REC_TYPE,                                        " & vbNewLine _
                                                 & " @ID,                                              " & vbNewLine _
                                                 & " @SHORI_YM,                                        " & vbNewLine _
                                                 & " @IN_KAISHA,                                       " & vbNewLine _
                                                 & " @KAISHA_CD,                                       " & vbNewLine _
                                                 & " @DV_NO,                                           " & vbNewLine _
                                                 & " @GMID,                                            " & vbNewLine _
                                                 & " @GOODS_NM,                                        " & vbNewLine _
                                                 & " @COST,                                            " & vbNewLine _
                                                 & " @HIYO,                                            " & vbNewLine _
                                                 & " @TUKA,                                            " & vbNewLine _
                                                 & " @GAKU,                                            " & vbNewLine _
                                                 & " @FUGO,                                            " & vbNewLine _
                                                 & " @HASSEI_YM,                                       " & vbNewLine _
                                                 & " @SHIP_NO,                                         " & vbNewLine _
                                                 & " @WT,                                              " & vbNewLine _
                                                 & " @KYORI,                                           " & vbNewLine _
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

#End Region

#Region "Method"

#Region "SQLメイン処理"

#Region "ダウケミ請求印刷テーブルの検索"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_SEIQPRT)       'SQL構築(Select句)
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_FROM_SEIQPRT)  'SQL構築(From句)
        Call SetSQLWhereSEIQPRT(inTbl.Rows(0))                '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI070DAC", "SelectSeiqPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI070OUT_SEIQPRT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "ダウケミ請求印刷テーブルの物理削除"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSeiqPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_DELETE_SEIQPRT)      'SQL構築(Delete句)
        Call Me.SetSQLWhereSEIQPRT(inTbl.Rows(0))            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm((Me._StrSql.ToString).Replace("SEIQPRT.", "").ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI070DAC", "DeleteSeiqPrt", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

    '#Region "ダウケミ請求明細テーブルの検索"

    '    ''' <summary>
    '    ''' ダウケミ請求明細テーブルの検索
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function SelectSeiqMeisai(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMI070IN")

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL作成
    '        Me._StrSql.Append(LMI070DAC.SQL_SELECT_SEIQMEISAI)       'SQL構築(Select句)
    '        Me._StrSql.Append(LMI070DAC.SQL_SELECT_FROM_SEIQMEISAI)  'SQL構築(From句)
    '        Call Me.SetSQLWhereSEIQMEISAI1(inTbl.Rows(0))           '条件設定
    '        Call Me.SetSQLWhereSEIQMEISAI2(inTbl.Rows(0))           '条件設定

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteSQLLog("LMI070DAC", "SelectSeiqMeisai", cmd)

    '        'SQLの発行
    '        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '        'DataReader→DataTableへの転記
    '        Dim map As Hashtable = New Hashtable()

    '        '取得データの格納先をマッピング
    '        map.Add("NRS_BR_CD", "NRS_BR_CD")

    '        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI070OUT_SEIQMEISAI")

    '        reader.Close()

    '        Return ds

    '    End Function

    '#End Region

    '#Region "ダウケミ請求明細テーブルの物理削除"

    '    ''' <summary>
    '    ''' ダウケミ請求明細テーブルの物理削除
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function DeleteSeiqMeisai(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMI070IN")

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL作成
    '        Me._StrSql.Append(LMI070DAC.SQL_DELETE_SEIQMEISAI)      'SQL構築(Delete句)
    '        Call Me.SetSQLWhereSEIQMEISAI1(inTbl.Rows(0))           '条件設定
    '        Call Me.SetSQLWhereSEIQMEISAI2(inTbl.Rows(0))           '条件設定

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteSQLLog("LMI070DAC", "DeleteSeiqMeisai", cmd)

    '        'SQLの発行
    '        Me.UpdateResultChk(cmd)

    '        Return ds

    '    End Function

    '#End Region

#Region "保管料・荷役料を作成するためのデータの検索"

    ''' <summary>
    ''' 保管料・荷役料を作成するためのデータの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectHokanNiyaku(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_HOKANNIYAKU)       'SQL構築(Select句)
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_FROM_HOKANNIYAKU)  'SQL構築(From句)
        Call Me.SetSQLWhereHokanNiyaku1(inTbl.Rows(0))            '条件設定
        Call Me.SetSQLWhereHokanNiyaku2(inTbl.Rows(0))            '条件設定
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_ORDER_HOKANNIYAKU) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI070DAC", "SelectHokanNiyaku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEIQ_YM", "SEIQ_YM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("KURIKOSI_1", "KURIKOSI_1")
        map.Add("IN_NB_1", "IN_NB_1")
        map.Add("OUT_NB_1", "OUT_NB_1")
        map.Add("ZAN_NB_1", "ZAN_NB_1")
        map.Add("IN_NB_2", "IN_NB_2")
        map.Add("OUT_NB_2", "OUT_NB_2")
        map.Add("ZAN_NB_2", "ZAN_NB_2")
        map.Add("IN_NB_3", "IN_NB_3")
        map.Add("OUT_NB_3", "OUT_NB_3")
        map.Add("ZAN_NB_3", "ZAN_NB_3")
        map.Add("SEKI_NB", "SEKI_NB")
        map.Add("IN_NB_TTL", "IN_NB_TTL")
        map.Add("OUT_NB_TTL", "OUT_NB_TTL")
        map.Add("H_TNK_1", "H_TNK_1")
        map.Add("IN_N_TNK_1", "IN_N_TNK_1")
        map.Add("OUT_N_TNK_1", "OUT_N_TNK_1")
        map.Add("H_AM_TTL", "H_AM_TTL")
        map.Add("IN_N_AM_CL", "IN_N_AM_CL")
        map.Add("OUT_N_AM_CL", "OUT_N_AM_CL")
        map.Add("N_AM_TTL", "N_AM_TTL")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI070INOUT_HOKANNIYAKU")

        reader.Close()

        Return ds

    End Function

#End Region

    '#Region "ダウケミ請求明細の新規追加"

    '    ''' <summary>
    '    ''' ダウケミ請求明細の新規追加
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function InsertSeiqMeisai(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_HOKANNIYAKU")

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL作成
    '        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQMEISAI)         'SQL構築(INSERT句)

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        Dim max As Integer = inTbl.Rows.Count - 1
    '        For i As Integer = 0 To max

    '            'パラメータの初期化
    '            cmd.Parameters.Clear()

    '            'SQLパラメータ初期化
    '            Me._SqlPrmList = New ArrayList()

    '            'SQLパラメータ（個別項目）設定
    '            Call Me.SetSeiqMeisaiParameter(inTbl.Rows(i), Me._SqlPrmList)

    '            'SQLパラメータ（システム項目）設定
    '            Call Me.SetParamCommonSystemIns()

    '            'パラメータの反映
    '            For Each obj As Object In Me._SqlPrmList
    '                cmd.Parameters.Add(obj)
    '            Next

    '            MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqMeisai", cmd)

    '            'SQLの発行
    '            MyBase.GetInsertResult(cmd)

    '        Next

    '        Return ds

    '    End Function

    '#End Region

#Region "ダウケミ請求印刷テーブルの新規追加(保管料)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの新規追加(保管料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSeiqPrtHokan(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_HOKANNIYAKU")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQPRT)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            If 0 < Convert.ToDecimal(inTbl.Rows(i)("H_AM_TTL").ToString) = True Then

                'パラメータの初期化
                cmd.Parameters.Clear()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ（個別項目）設定
                Call Me.SetSeiqPrtParameterHokan(inTbl.Rows(i), Me._SqlPrmList)

                'SQLパラメータ（システム項目）設定
                Call Me.SetParamCommonSystemIns()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqPrtHokan", cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

            End If

        Next

        Return ds

    End Function

#End Region

#Region "ダウケミ請求印刷テーブルの新規追加(荷役料)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの新規追加(荷役料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSeiqPrtNiyaku(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_HOKANNIYAKU")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQPRT)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            If 0 < Convert.ToDecimal(inTbl.Rows(i)("N_AM_TTL").ToString) = True Then

                'パラメータの初期化
                cmd.Parameters.Clear()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ（個別項目）設定
                Call Me.SetSeiqPrtParameterNiyaku(inTbl.Rows(i), Me._SqlPrmList)

                'SQLパラメータ（システム項目）設定
                Call Me.SetParamCommonSystemIns()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqPrtNiyaku", cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

            End If

        Next

        Return ds

    End Function

#End Region

#Region "作業を作成するためのデータの検索"

    ''' <summary>
    ''' 作業を作成するためのデータの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_SAGYO)       'SQL構築(Select句)
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_FROM_SAGYO)  'SQL構築(From句)
        Call Me.SetSQLWhereSagyo1(inTbl.Rows(0))             '条件設定
        Call Me.SetSQLWhereSagyo2(inTbl.Rows(0))             '条件設定
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_ORDER_SAGYO) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI070DAC", "SelectSagyo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_COMP_DATE", "SAGYO_COMP_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SAGYO_GK", "SAGYO_GK")
        map.Add("SKYU_CHK", "SKYU_CHK")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("INOUTKA_NO_LM", "INOUTKA_NO_LM")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI070INOUT_SAGYO")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "ダウケミ請求印刷テーブルの新規追加(作業料)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの新規追加(作業料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSeiqPrtSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQPRT)         'SQL構築(INSERT句)

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
            Call Me.SetSeiqPrtParameterSagyo(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqPrtSagyo", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

    '#Region "ダウケミ請求運賃テーブルの検索"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブルの検索
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function SelectSeiqUnchin(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMI070IN")

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL作成
    '        Me._StrSql.Append(LMI070DAC.SQL_SELECT_SEIQUNCHIN)       'SQL構築(Select句)
    '        Me._StrSql.Append(LMI070DAC.SQL_SELECT_FROM_SEIQUNCHIN)  'SQL構築(From句)
    '        Call SetSQLWhereSEIQUNCHIN(inTbl.Rows(0))                '条件設定

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteSQLLog("LMI070DAC", "SelectSeiqUnchin", cmd)

    '        'SQLの発行
    '        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '        'DataReader→DataTableへの転記
    '        Dim map As Hashtable = New Hashtable()

    '        '取得データの格納先をマッピング
    '        map.Add("NRS_BR_CD", "NRS_BR_CD")

    '        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI070OUT_SEIQUNCHIN")

    '        reader.Close()

    '        Return ds

    '    End Function

    '#End Region

    '#Region "ダウケミ請求運賃テーブルの物理削除"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブルの物理削除
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function DeleteSeiqUnchin(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMI070IN")

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL作成
    '        Me._StrSql.Append(LMI070DAC.SQL_DELETE_SEIQUNCHIN)      'SQL構築(Delete句)
    '        Call SetSQLWhereSEIQUNCHIN(inTbl.Rows(0))               '条件設定

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm((Me._StrSql.ToString).Replace("SEIQUNCHIN.", "").ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteSQLLog("LMI070DAC", "DeleteSeiqUnchin", cmd)

    '        'SQLの発行
    '        Me.UpdateResultChk(cmd)

    '        Return ds

    '    End Function

    '#End Region

#Region "運賃を作成するためのデータの検索(出荷)"

    ''' <summary>
    ''' 運賃を作成するためのデータの検索(出荷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_UNCHIN_OUTKA)       'SQL構築(Select句)
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_FROM_UNCHIN_OUTKA)  'SQL構築(From句)
        Call Me.SetSQLWhereUnchinOutka(inTbl.Rows(0))                    '条件設定
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_ORDER_UNCHIN_OUTKA) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI070DAC", "SelectUnchinOutka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("UNCHIN_REMARK", "UNCHIN_REMARK")
        map.Add("UNSOL_REMARK", "UNSOL_REMARK")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("JIS", "JIS")
        map.Add("KEN", "KEN")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("DECI_KINGAKU", "DECI_KINGAKU")
        map.Add("CAL_WT", "CAL_WT")
        map.Add("CAL_KINGAKU", "CAL_KINGAKU")
        map.Add("CAL_NB", "CAL_NB")
        'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
        map.Add("DECI_WT", "DECI_WT")
        'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
        map.Add("DENP_NO", "DENP_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI070INOUT_UNCHIN")

        reader.Close()

        Return ds

    End Function

#End Region

    '#Region "ダウケミ請求運賃の新規追加(出荷)"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃の新規追加(出荷)
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function InsertSeiqUnchinOutka(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_UNCHIN")

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL作成
    '        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQUNCHIN)         'SQL構築(INSERT句)

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        Dim max As Integer = inTbl.Rows.Count - 1
    '        For i As Integer = 0 To max

    '            If 0 < Convert.ToDecimal(inTbl.Rows(i)("DECI_KINGAKU").ToString) = True Then

    '                'パラメータの初期化
    '                cmd.Parameters.Clear()

    '                'SQLパラメータ初期化
    '                Me._SqlPrmList = New ArrayList()

    '                'SQLパラメータ（個別項目）設定
    '                Call Me.SetSeiqUnchinOutkaParameter(inTbl.Rows(i), Me._SqlPrmList)

    '                'SQLパラメータ（システム項目）設定
    '                Call Me.SetParamCommonSystemIns()

    '                'パラメータの反映
    '                For Each obj As Object In Me._SqlPrmList
    '                    cmd.Parameters.Add(obj)
    '                Next

    '                MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqUnchinOutka", cmd)

    '                'SQLの発行
    '                MyBase.GetInsertResult(cmd)

    '            End If

    '        Next

    '        Return ds

    '    End Function

    '#End Region

#Region "ダウケミ請求印刷テーブルの新規追加(運賃)(出荷)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの新規追加(運賃)(出荷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSeiqPrtUnchinOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_UNCHIN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQPRT)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            If ("01").Equals(inTbl.Rows(i)("OUTKA_NO_M_FIRST_FLG").ToString) = True AndAlso _
                0 < Convert.ToDecimal(inTbl.Rows(i)("DECI_KINGAKU").ToString) = True Then

                'パラメータの初期化
                cmd.Parameters.Clear()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ（個別項目）設定
                Call Me.SetSeiqPrtParameterUnchinOutka(inTbl.Rows(i), Me._SqlPrmList)

                'SQLパラメータ（システム項目）設定
                Call Me.SetParamCommonSystemIns()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqPrtUnchinOutka", cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

            End If

        Next

        Return ds

    End Function

#End Region

#Region "運賃を作成するためのデータの検索(入荷)"

    ''' <summary>
    ''' 運賃を作成するためのデータの検索(入荷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinInka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_UNCHIN_INKA)       'SQL構築(Select句)
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_FROM_UNCHIN_INKA)  'SQL構築(From句)
        Call Me.SetSQLWhereUnchinInka(inTbl.Rows(0))                    '条件設定
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_ORDER_UNCHIN_INKA) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI070DAC", "SelectUnchinInka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("UNCHIN_REMARK", "UNCHIN_REMARK")
        map.Add("UNSOL_REMARK", "UNSOL_REMARK")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("JIS", "JIS")
        map.Add("KEN", "KEN")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("DECI_KINGAKU", "DECI_KINGAKU")
        map.Add("CAL_WT", "CAL_WT")
        map.Add("CAL_KINGAKU", "CAL_KINGAKU")
        map.Add("CAL_NB", "CAL_NB")
        'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
        map.Add("DECI_WT", "DECI_WT")
        'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI070INOUT_UNCHIN")

        reader.Close()

        Return ds

    End Function

#End Region

    '#Region "ダウケミ請求運賃の新規追加(入荷)"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃の新規追加(入荷)
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function InsertSeiqUnchinInka(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_UNCHIN")

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL作成
    '        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQUNCHIN)         'SQL構築(INSERT句)

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        Dim max As Integer = inTbl.Rows.Count - 1
    '        For i As Integer = 0 To max

    '            'パラメータの初期化
    '            cmd.Parameters.Clear()

    '            'SQLパラメータ初期化
    '            Me._SqlPrmList = New ArrayList()

    '            'SQLパラメータ（個別項目）設定
    '            Call Me.SetSeiqUnchinInkaParameter(inTbl.Rows(i), Me._SqlPrmList)

    '            'SQLパラメータ（システム項目）設定
    '            Call Me.SetParamCommonSystemIns()

    '            'パラメータの反映
    '            For Each obj As Object In Me._SqlPrmList
    '                cmd.Parameters.Add(obj)
    '            Next

    '            MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqUnchinInka", cmd)

    '            'SQLの発行
    '            MyBase.GetInsertResult(cmd)

    '        Next

    '        Return ds

    '    End Function

    '#End Region

#Region "ダウケミ請求印刷テーブルの新規追加(運賃)(入荷)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの新規追加(運賃)(入荷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSeiqPrtUnchinInka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_UNCHIN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQPRT)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            If ("01").Equals(inTbl.Rows(i)("OUTKA_NO_M_FIRST_FLG").ToString) = True Then

                'パラメータの初期化
                cmd.Parameters.Clear()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ（個別項目）設定
                Call Me.SetSeiqPrtParameterUnchinInka(inTbl.Rows(i), Me._SqlPrmList)

                'SQLパラメータ（システム項目）設定
                Call Me.SetParamCommonSystemIns()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqPrtUnchinInka", cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

            End If

        Next

        Return ds

    End Function

#End Region

#Region "運賃を作成するためのデータの検索(運賃)"

    ''' <summary>
    ''' 運賃を作成するためのデータの検索(運賃)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_UNCHIN)       'SQL構築(Select句)
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_FROM_UNCHIN)  'SQL構築(From句)
        Call Me.SetSQLWhereUnchin(inTbl.Rows(0))                    '条件設定
        Me._StrSql.Append(LMI070DAC.SQL_SELECT_ORDER_UNCHIN) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI070DAC", "SelectUnchin", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("UNCHIN_REMARK", "UNCHIN_REMARK")
        map.Add("UNSOL_REMARK", "UNSOL_REMARK")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("JIS", "JIS")
        map.Add("KEN", "KEN")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("DECI_KINGAKU", "DECI_KINGAKU")
        map.Add("CAL_WT", "CAL_WT")
        map.Add("CAL_KINGAKU", "CAL_KINGAKU")
        map.Add("CAL_NB", "CAL_NB")
        'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
        map.Add("DECI_WT", "DECI_WT")
        'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
        map.Add("DENP_NO", "DENP_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI070INOUT_UNCHIN")

        reader.Close()

        Return ds

    End Function

#End Region

    '#Region "ダウケミ請求運賃の新規追加(運賃)"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃の新規追加(運賃)
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function InsertSeiqUnchin(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_UNCHIN")

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL作成
    '        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQUNCHIN)         'SQL構築(INSERT句)

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        Dim max As Integer = inTbl.Rows.Count - 1
    '        For i As Integer = 0 To max

    '            'パラメータの初期化
    '            cmd.Parameters.Clear()

    '            'SQLパラメータ初期化
    '            Me._SqlPrmList = New ArrayList()

    '            'SQLパラメータ（個別項目）設定
    '            Call Me.SetSeiqUnchinParameter(inTbl.Rows(i), Me._SqlPrmList)

    '            'SQLパラメータ（システム項目）設定
    '            Call Me.SetParamCommonSystemIns()

    '            'パラメータの反映
    '            For Each obj As Object In Me._SqlPrmList
    '                cmd.Parameters.Add(obj)
    '            Next

    '            MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqUnchin", cmd)

    '            'SQLの発行
    '            MyBase.GetInsertResult(cmd)

    '        Next

    '        Return ds

    '    End Function

    '#End Region

#Region "ダウケミ請求印刷テーブルの新規追加(運賃)(運賃)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの新規追加(運賃)(運賃)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSeiqPrtUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI070INOUT_UNCHIN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI070DAC.SQL_INSERT_SEIQPRT)         'SQL構築(INSERT句)

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
            Call Me.SetSeiqPrtParameterUnchin(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC010DAC", "InsertSeiqPrtUnchin", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 ダウケミ請求印刷テーブルの検索・削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSEIQPRT(ByVal inTblRow As DataRow)

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
                Me._StrSql.Append(" SEIQPRT.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQPRT.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQPRT.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '年月From
            whereStr = Mid(.Item("DATE_FROM").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQPRT.SEIQ_YM >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '年月To
            whereStr = Mid(.Item("DATE_TO").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQPRT.SEIQ_YM <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'レコード種類1
            strSqlAppend = String.Empty
            whereStr = .Item("MAKE_KB").ToString()
            If ("01").Equals(.Item("MAKE_KB").ToString()) = True OrElse _
                ("02").Equals(.Item("MAKE_KB").ToString()) = True Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " SEIQPRT.REC_TYPE = '1'")
            End If

            If ("01").Equals(.Item("MAKE_KB").ToString()) = True OrElse _
                ("03").Equals(.Item("MAKE_KB").ToString()) = True Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " SEIQPRT.REC_TYPE = '2'")
            End If

            If ("01").Equals(.Item("MAKE_KB").ToString()) = True OrElse _
                ("04").Equals(.Item("MAKE_KB").ToString()) = True Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " SEIQPRT.REC_TYPE = '3'")
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

#Region "SQL条件設定 ダウケミ請求明細テーブルの検索・削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSEIQMEISAI1(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

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

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSEIQMEISAI2(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" SEIQMEISAI.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '年月From
            whereStr = Mid(.Item("DATE_FROM").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQMEISAI.SEIQ_YM >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '年月To
            whereStr = Mid(.Item("DATE_TO").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQMEISAI.SEIQ_YM <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "保管料・荷役料を作成するためのデータの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereHokanNiyaku1(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

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

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereHokanNiyaku2(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("MEISAIPRT.SYS_DEL_FLG = '0'                                    ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MEISAIPRT.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '年月From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MEISAIPRT.INV_DATE_FROM >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '年月To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MEISAIPRT.INV_DATE_FROM <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

    '#Region "SQL条件設定 ダウケミ請求明細の新規追加"

    '    ''' <summary>
    '    ''' ダウケミ請求明細の更新パラメータ設定
    '    ''' </summary>
    '    ''' <param name="conditionRow">DataRow</param>
    '    ''' <param name="prmList">パラメータ</param>
    '    ''' <remarks></remarks>
    '    Private Sub SetSeiqMeisaiParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

    '        With conditionRow

    '            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("SEIQ_YM").ToString(), 1, 6), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@KURIKOSI_1", .Item("KURIKOSI_1").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@IN_NB_1", .Item("IN_NB_1").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@OUT_NB_1", .Item("OUT_NB_1").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@ZAN_NB_1", .Item("ZAN_NB_1").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@IN_NB_2", .Item("IN_NB_2").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@OUT_NB_2", .Item("OUT_NB_2").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@ZAN_NB_2", .Item("ZAN_NB_2").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@IN_NB_3", .Item("IN_NB_3").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@OUT_NB_3", .Item("OUT_NB_3").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@ZAN_NB_3", .Item("ZAN_NB_3").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@SEKI_NB", .Item("SEKI_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@IN_NB_TTL", .Item("IN_NB_TTL").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@OUT_NB_TTL", .Item("OUT_NB_TTL").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@H_TNK_1", .Item("H_TNK_1").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@IN_N_TNK_1", .Item("IN_N_TNK_1").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@OUT_N_TNK_1", .Item("OUT_N_TNK_1").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@H_AM_TTL", .Item("H_AM_TTL").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@IN_N_AM_CL", .Item("IN_N_AM_CL").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@OUT_N_AM_CL", .Item("OUT_N_AM_CL").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@N_AM_TTL", .Item("N_AM_TTL").ToString(), DBDataType.NUMERIC))

    '        End With

    '    End Sub

    '#End Region

#Region "SQL条件設定 ダウケミ請求印刷テーブルの新規追加(保管料)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSeiqPrtParameterHokan(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("SEIQ_YM").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_TYPE", recType1, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ID", idCd, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_YM", Mid(.Item("SEIQ_YM").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IN_KAISHA", inkaishaCd00464593, DBDataType.CHAR))
            'If (custCd00109).Equals(.Item("CUST_CD_L").ToString()) = True Then
            prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd38, DBDataType.NVARCHAR))
            'Else
            'prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd22, DBDataType.NVARCHAR))
            'End If
            prmList.Add(MyBase.GetSqlParameter("@DV_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COST", costCd6401, DBDataType.NVARCHAR))
            'If ("20").Equals(.Item("NRS_BR_CD").ToString()) = True Then
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
            'Else
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00763573, DBDataType.NVARCHAR))
            'End If
            Select Case .Item("NRS_BR_CD").ToString
                Case "20" ''大阪
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
                Case "40" ''横浜
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00239410, DBDataType.NVARCHAR))
            End Select
            prmList.Add(MyBase.GetSqlParameter("@TUKA", tuka, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GAKU", MaeCoverData(Convert.ToString(System.Math.Floor(Convert.ToDecimal(.Item("H_AM_TTL").ToString()))), "0", 9), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FUGO", plus, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASSEI_YM", Mid(.Item("SEIQ_YM").ToString(), 1, 6), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WT", "0", DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", "0", DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "SQL条件設定 ダウケミ請求印刷テーブルの新規追加(荷役料)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSeiqPrtParameterNiyaku(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("SEIQ_YM").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_TYPE", recType1, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ID", idCd, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_YM", Mid(.Item("SEIQ_YM").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IN_KAISHA", inkaishaCd00464593, DBDataType.CHAR))
            'If (custCd00109).Equals(.Item("CUST_CD_L").ToString()) = True Then
            prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd38, DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@DV_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COST", costCd6402, DBDataType.NVARCHAR))
            'If ("20").Equals(.Item("NRS_BR_CD").ToString()) = True Then
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
            'Else
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00763573, DBDataType.NVARCHAR))
            'End If
            Select Case .Item("NRS_BR_CD").ToString
                Case "20" ''大阪
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
                Case "40" ''横浜
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00239410, DBDataType.NVARCHAR))
            End Select
            prmList.Add(MyBase.GetSqlParameter("@TUKA", tuka, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GAKU", MaeCoverData(Convert.ToString(System.Math.Floor(Convert.ToDecimal(.Item("N_AM_TTL").ToString()))), "0", 9), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FUGO", plus, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASSEI_YM", Mid(.Item("SEIQ_YM").ToString(), 1, 6), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WT", "0", DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", "0", DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "作業料を作成するためのデータの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSagyo1(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            '年月From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
            End If

            '年月To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSagyo2(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("SAGYO.SYS_DEL_FLG = '0'                                        ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND SAGYO.SKYU_CHK = '01'                                      ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND SAGYO.SEIQTO_CD <> ''                                      ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '年月From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.SAGYO_COMP_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '年月To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.SAGYO_COMP_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 ダウケミ請求印刷テーブルの新規追加(作業料)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSeiqPrtParameterSagyo(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim cutStr() As String

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("SAGYO_COMP_DATE").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_TYPE", recType2, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ID", idCd, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_YM", Mid(.Item("SAGYO_COMP_DATE").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IN_KAISHA", inkaishaCd00464593, DBDataType.CHAR))
            'If (custCd00109).Equals(.Item("CUST_CD_L").ToString()) = True Then
            prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd38, DBDataType.NVARCHAR))
            'Else
            'prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd22, DBDataType.NVARCHAR))
            'End If

            ReDim cutStr(0)
            cutStr = Me.stringCut(.Item("CUST_ORD_NO").ToString(), 9, 1)
            prmList.Add(MyBase.GetSqlParameter("@DV_NO", cutStr(0).ToString, DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COST", costCd6402, DBDataType.NVARCHAR))
            'If ("20").Equals(.Item("NRS_BR_CD").ToString()) = True Then
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
            'Else
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00763573, DBDataType.NVARCHAR))
            'End If
            Select Case .Item("NRS_BR_CD").ToString
                Case "20" ''大阪
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
                Case "40" ''横浜
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00239410, DBDataType.NVARCHAR))
            End Select
            prmList.Add(MyBase.GetSqlParameter("@TUKA", tuka, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GAKU", MaeCoverData(Convert.ToString(System.Math.Floor(Convert.ToDecimal(.Item("SAGYO_GK").ToString()))), "0", 9), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FUGO", plus, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASSEI_YM", Mid(.Item("SAGYO_COMP_DATE").ToString(), 1, 6), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WT", "0", DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", "0", DBDataType.NUMERIC))

        End With

    End Sub

#End Region

    '#Region "SQL条件設定 ダウケミ請求運賃テーブルの検索・削除"

    '    ''' <summary>
    '    ''' 条件文・パラメータ設定モジュール
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub SetSQLWhereSEIQUNCHIN(ByVal inTblRow As DataRow)

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        '検索条件部に入力された条件とパラメータ設定
    '        Dim whereStr As String = String.Empty
    '        Dim strSqlAppend As String = String.Empty
    '        With inTblRow

    '            Me._StrSql.Append("WHERE                                                          ")
    '            Me._StrSql.Append(vbNewLine)

    '            '営業所
    '            whereStr = .Item("NRS_BR_CD").ToString()
    '            If String.IsNullOrEmpty(whereStr) = False Then
    '                Me._StrSql.Append(" SEIQUNCHIN.NRS_BR_CD = @NRS_BR_CD")
    '                Me._StrSql.Append(vbNewLine)
    '                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
    '            End If

    '            '荷主コード(大)
    '            whereStr = .Item("CUST_CD_L").ToString()
    '            If String.IsNullOrEmpty(whereStr) = False Then
    '                Me._StrSql.Append(" AND SEIQUNCHIN.CUST_CD_L = @CUST_CD_L")
    '                Me._StrSql.Append(vbNewLine)
    '                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
    '            End If

    '            '荷主コード(中)
    '            whereStr = .Item("CUST_CD_M").ToString()
    '            If String.IsNullOrEmpty(whereStr) = False Then
    '                Me._StrSql.Append(" AND SEIQUNCHIN.CUST_CD_M = @CUST_CD_M")
    '                Me._StrSql.Append(vbNewLine)
    '                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
    '            End If

    '            '年月From
    '            whereStr = Mid(.Item("DATE_FROM").ToString(), 1, 6)
    '            If String.IsNullOrEmpty(whereStr) = False Then
    '                Me._StrSql.Append(" AND SEIQUNCHIN.SEIQ_YM >= @DATE_FROM")
    '                Me._StrSql.Append(vbNewLine)
    '                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
    '            End If

    '            '年月To
    '            whereStr = Mid(.Item("DATE_TO").ToString(), 1, 6)
    '            If String.IsNullOrEmpty(whereStr) = False Then
    '                Me._StrSql.Append(" AND SEIQUNCHIN.SEIQ_YM <= @DATE_TO")
    '                Me._StrSql.Append(vbNewLine)
    '                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
    '            End If

    '        End With

    '    End Sub

    '#End Region

#Region "運賃を作成するためのデータの検索(出荷)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereUnchinOutka(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("UNSOL.SYS_DEL_FLG = '0'                                        ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND UNSOL.MOTO_DATA_KB = '20'                                  ")
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
                Me._StrSql.Append(" AND UNSOL.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '年月From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '年月To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

    '#Region "SQL条件設定 ダウケミ請求運賃テーブルの新規追加(運賃)(出荷)"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブルの更新パラメータ設定
    '    ''' </summary>
    '    ''' <param name="conditionRow">DataRow</param>
    '    ''' <param name="prmList">パラメータ</param>
    '    ''' <remarks></remarks>
    '    Private Sub SetSeiqUnchinOutkaParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

    '        Dim cutStr() As String

    '        With conditionRow

    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO_M", .Item("INOUTKA_NO_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO_S", .Item("INOUTKA_NO_S").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", .Item("SEIQ_NG_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@DECI_KINGAKU", .Item("DECI_KINGAKU").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_WT", .Item("CAL_WT").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_IRIME", .Item("STD_IRIME_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_KINGAKU", .Item("CAL_KINGAKU").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@ORD_NO", .Item("UNCHIN_REMARK").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", .Item("UNSOL_REMARK").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))

    '            ReDim cutStr(0)
    '            cutStr = Me.stringCut(String.Concat(.Item("DEST_AD_1").ToString(), .Item("DEST_AD_2").ToString(), .Item("DEST_AD_3").ToString()), 100, 1)
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_AD", cutStr(0).ToString, DBDataType.NVARCHAR))

    '            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", .Item("JIS").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_KEN", .Item("KEN").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUKO_KIGEN", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@ORD_ITEM_NO", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUSO_TYPE", yusoType, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUSO_KB", yusoKb, DBDataType.NVARCHAR))

    '        End With

    '    End Sub

    '#End Region

#Region "SQL条件設定 ダウケミ請求印刷テーブルの新規追加(運賃)(出荷)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSeiqPrtParameterUnchinOutka(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim cutStr() As String

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_TYPE", recType3, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ID", idCd, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IN_KAISHA", inkaishaCd00464593, DBDataType.CHAR))
            'If (custCd00109).Equals(.Item("CUST_CD_L").ToString()) = True Then
            prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd38, DBDataType.NVARCHAR))
            'Else
            'prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd22, DBDataType.NVARCHAR))
            'End If

            ReDim cutStr(0)
            cutStr = Me.stringCut(.Item("UNCHIN_REMARK").ToString(), 9, 1)
            prmList.Add(MyBase.GetSqlParameter("@DV_NO", cutStr(0).ToString, DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COST", costCd5110, DBDataType.NVARCHAR))
            'If ("20").Equals(.Item("NRS_BR_CD").ToString()) = True Then
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
            'Else
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00763573, DBDataType.NVARCHAR))
            'End If
            Select Case .Item("NRS_BR_CD").ToString
                Case "20" ''大阪
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
                Case "40" ''横浜
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00239410, DBDataType.NVARCHAR))
            End Select
            prmList.Add(MyBase.GetSqlParameter("@TUKA", tuka, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GAKU", MaeCoverData(Convert.ToString(System.Math.Floor(Convert.ToDecimal(.Item("DECI_KINGAKU").ToString()))), "0", 9), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FUGO", plus, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASSEI_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.NVARCHAR))

            ReDim cutStr(0)
            cutStr = Me.stringCut(.Item("UNSOL_REMARK").ToString(), 16, 1)
            'prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", Left(cutStr(0).ToString, 8), DBDataType.NVARCHAR))
            '
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", .Item("DENP_NO").ToString, DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "運賃を作成するためのデータの検索(入荷)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereUnchinInka(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("UNSOL.SYS_DEL_FLG = '0'                                        ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND UNSOL.MOTO_DATA_KB = '10'                                  ")
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
                Me._StrSql.Append(" AND UNSOL.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '年月From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '年月To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

    '#Region "SQL条件設定 ダウケミ請求運賃テーブルの新規追加(運賃)(入荷)"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブルの更新パラメータ設定
    '    ''' </summary>
    '    ''' <param name="conditionRow">DataRow</param>
    '    ''' <param name="prmList">パラメータ</param>
    '    ''' <remarks></remarks>
    '    Private Sub SetSeiqUnchinInkaParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

    '        With conditionRow

    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO_M", .Item("INOUTKA_NO_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO_S", .Item("INOUTKA_NO_S").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", .Item("SEIQ_NG_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@DECI_KINGAKU", .Item("DECI_KINGAKU").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_NB", .Item("CAL_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_WT", .Item("CAL_WT").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_IRIME", .Item("STD_IRIME_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_KINGAKU", .Item("CAL_KINGAKU").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@ORD_NO", .Item("UNCHIN_REMARK").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_AD", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_KEN", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUKO_KIGEN", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@ORD_ITEM_NO", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUSO_TYPE", yusoType, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUSO_KB", yusoKb, DBDataType.NVARCHAR))

    '        End With

    '    End Sub

    '#End Region

#Region "SQL条件設定 ダウケミ請求印刷テーブルの新規追加(運賃)(入荷)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSeiqPrtParameterUnchinInka(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim cutStr() As String

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_TYPE", recType3, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ID", idCd, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IN_KAISHA", inkaishaCd00464593, DBDataType.CHAR))
            'If (custCd00109).Equals(.Item("CUST_CD_L").ToString()) = True Then
            prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd38, DBDataType.NVARCHAR))
            'Else
            'prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd22, DBDataType.NVARCHAR))
            'End If

            ReDim cutStr(0)
            cutStr = Me.stringCut(.Item("UNCHIN_REMARK").ToString(), 9, 1)
            prmList.Add(MyBase.GetSqlParameter("@DV_NO", cutStr(0).ToString, DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COST", costCd5110, DBDataType.NVARCHAR))
            'If ("20").Equals(.Item("NRS_BR_CD").ToString()) = True Then
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
            'Else
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00763573, DBDataType.NVARCHAR))
            'End If
            Select Case .Item("NRS_BR_CD").ToString
                Case "20" ''大阪
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
                Case "40" ''横浜
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00239410, DBDataType.NVARCHAR))
            End Select
            prmList.Add(MyBase.GetSqlParameter("@TUKA", tuka, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GAKU", MaeCoverData(Convert.ToString(System.Math.Floor(Convert.ToDecimal(.Item("DECI_KINGAKU").ToString()))), "0", 9), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FUGO", plus, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASSEI_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "運賃を作成するためのデータの検索(運賃)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereUnchin(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("UNSOL.SYS_DEL_FLG = '0'                                        ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND UNSOL.MOTO_DATA_KB = '40'                                  ")
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
                Me._StrSql.Append(" AND UNSOL.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '年月From
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '年月To
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

    '#Region "SQL条件設定 ダウケミ請求運賃テーブルの新規追加(運賃)(運賃)"

    '    ''' <summary>
    '    ''' ダウケミ請求運賃テーブルの更新パラメータ設定
    '    ''' </summary>
    '    ''' <param name="conditionRow">DataRow</param>
    '    ''' <param name="prmList">パラメータ</param>
    '    ''' <remarks></remarks>
    '    Private Sub SetSeiqUnchinParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

    '        Dim cutStr() As String

    '        With conditionRow

    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO_M", String.Empty, DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_CTL_NO_S", String.Empty, DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", .Item("SEIQ_NG_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@DECI_KINGAKU", .Item("DECI_KINGAKU").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_NB", .Item("CAL_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_WT", .Item("CAL_WT").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_IRIME", .Item("STD_IRIME_NB").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@CAL_KINGAKU", .Item("CAL_KINGAKU").ToString(), DBDataType.NUMERIC))
    '            prmList.Add(MyBase.GetSqlParameter("@ORD_NO", .Item("UNCHIN_REMARK").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))

    '            ReDim cutStr(0)
    '            cutStr = Me.stringCut(String.Concat(.Item("DEST_AD_1").ToString(), .Item("DEST_AD_2").ToString(), .Item("DEST_AD_3").ToString()), 100, 1)
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_AD", cutStr(0).ToString, DBDataType.NVARCHAR))

    '            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", .Item("JIS").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@DEST_KEN", .Item("KEN").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUKO_KIGEN", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@ORD_ITEM_NO", String.Empty, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUSO_TYPE", yusoType, DBDataType.NVARCHAR))
    '            prmList.Add(MyBase.GetSqlParameter("@YUSO_KB", yusoKb, DBDataType.NVARCHAR))

    '        End With

    '    End Sub

    '#End Region

#Region "SQL条件設定 ダウケミ請求印刷テーブルの新規追加(運賃)(運賃)"

    ''' <summary>
    ''' ダウケミ請求印刷テーブルの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSeiqPrtParameterUnchin(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim cutStr() As String

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_TYPE", recType3, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ID", idCd, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IN_KAISHA", inkaishaCd00464593, DBDataType.CHAR))
            'If (custCd00109).Equals(.Item("CUST_CD_L").ToString()) = True Then
            prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd38, DBDataType.NVARCHAR))
            'Else
            'prmList.Add(MyBase.GetSqlParameter("@KAISHA_CD", kaishaCd22, DBDataType.NVARCHAR))
            'End If

            ReDim cutStr(0)
            cutStr = Me.stringCut(.Item("UNCHIN_REMARK").ToString(), 9, 1)
            prmList.Add(MyBase.GetSqlParameter("@DV_NO", cutStr(0).ToString, DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@GMID", MaeCoverData(Mid(.Item("JAN_CD").ToString(), 1, 8), "0", 8), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COST", costCd5110, DBDataType.NVARCHAR))
            'If ("20").Equals(.Item("NRS_BR_CD").ToString()) = True Then
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
            'Else
            '    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00763573, DBDataType.NVARCHAR))
            'End If
            Select Case .Item("NRS_BR_CD").ToString
                Case "20" ''大阪
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00464595, DBDataType.NVARCHAR))
                Case "40" ''横浜
                    prmList.Add(MyBase.GetSqlParameter("@HIYO", hiyo00239410, DBDataType.NVARCHAR))
            End Select
            prmList.Add(MyBase.GetSqlParameter("@TUKA", tuka, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GAKU", MaeCoverData(Convert.ToString(System.Math.Floor(Convert.ToDecimal(.Item("DECI_KINGAKU").ToString()))), "0", 9), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FUGO", plus, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASSEI_YM", Mid(.Item("OUTKA_PLAN_DATE").ToString(), 1, 6), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NO", .Item("DENP_NO").ToString, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))

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

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#Region "後埋め設定"

    ''' <summary>
    ''' 後埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">後埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>後埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function AtoCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value, value2)
        Next

        Return value

    End Function

#End Region

#Region "文字分割"

    ''' <summary>
    ''' 文字分割
    ''' </summary>
    ''' <param name="inStr">分割対象文字</param>
    ''' <param name="inByte">分割単位バイト数</param>
    ''' <param name="inCnt">分割する数</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Function stringCut(ByVal inStr As String, ByVal inByte As Integer, ByVal inCnt As Integer) As String()

        Dim newCnt As Integer = inCnt - 1
        Dim newByte As Integer = inByte - 1
        Dim oldStr(newCnt) As String
        Dim newStr(newCnt) As String
        Dim byteCnt As Integer = 1

        For i As Integer = 0 To newCnt
            For j As Integer = 0 To newByte
                oldStr(i) = String.Concat(oldStr(i), Mid(inStr, byteCnt, 1))
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(oldStr(i)) <= newByte + 1 Then
                    newStr(i) = oldStr(i)
                    byteCnt = byteCnt + 1
                Else
                    Exit For
                End If
            Next
        Next

        Return newStr

    End Function

#End Region

#End Region

End Class
