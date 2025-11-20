' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI040  : 請求データ作成 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI040DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "SELECT"
#Region "データの検索 SQL SELECT句(COUNT)"

    ''' <summary>
    ''' データの検索 SQL SELECT句(COUNT)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU_COUNT As String = " SELECT COUNT(GL.NRS_BR_CD)		            AS SELECT_CNT           " & vbNewLine

#End Region

#Region "データの検索 SQL SELECT句"

    'START YANAI 要望番号830
    '''' <summary>
    '''' データの検索 SQL SELECT句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_KENSAKU As String = "SELECT                                                                        " & vbNewLine _
    '                                           & " GL.NRS_BR_CD                                  AS NRS_BR_CD                   " & vbNewLine _
    '                                           & ",GL.SEKY_YM                                    AS SEKY_YM                     " & vbNewLine _
    '                                           & ",GL.DEPART                                     AS DEPART                      " & vbNewLine _
    '                                           & ",Z1.KBN_NM1                                    AS DEPART_NM                   " & vbNewLine _
    '                                           & ",GL.SEKY_KMK                                   AS SEKY_KMK                    " & vbNewLine _
    '                                           & ",Z2.KBN_NM1                                    AS SEKY_KMK_NM                 " & vbNewLine _
    '                                           & ",GL.FRB_CD                                     AS FRB_CD                      " & vbNewLine _
    '                                           & ",GL.SRC_CD                                     AS SRC_CD                      " & vbNewLine _
    '                                           & ",GL.COST_CENTER                                AS COST_CENTER                 " & vbNewLine _
    '                                           & ",GL.MISK_CD                                    AS MISK_CD                     " & vbNewLine _
    '                                           & ",GL.DEST_CTY                                   AS DEST_CTY                    " & vbNewLine _
    '                                           & ",Z3.KBN_NM1                                    AS MISK_NM                     " & vbNewLine _
    '                                           & ",GL.AMOUNT                                     AS AMOUNT                      " & vbNewLine _
    '                                           & ",GL.VAT_AMOUNT                                 AS VAT_AMOUNT                  " & vbNewLine _
    '                                           & ",GL.SOUND                                      AS SOUND                       " & vbNewLine _
    '                                           & ",GL.BOND                                       AS BOND                        " & vbNewLine _
    '                                           & ",GL.JIDO_FLAG                                  AS JIDO_FLAG                   " & vbNewLine _
    '                                           & ",GL.SHUDO_FLAG                                 AS SHUDO_FLAG                  " & vbNewLine _
    '                                           & ",GL.SYS_ENT_DATE                               AS SYS_ENT_DATE                " & vbNewLine _
    '                                           & ",USER1.USER_NM                                 AS SYS_ENT_USER_NM             " & vbNewLine _
    '                                           & ",GL.SYS_UPD_DATE                               AS SYS_UPD_DATE                " & vbNewLine _
    '                                           & ",GL.SYS_UPD_TIME                               AS SYS_UPD_TIME                " & vbNewLine _
    '                                           & ",USER2.USER_NM                                 AS SYS_UPD_USER_NM             " & vbNewLine _
    '                                           & ",GL.SYS_DEL_FLG                                AS SYS_DEL_FLG                 " & vbNewLine _
    '                                           & ",Z4.KBN_NM1                                    AS SYS_DEL_NM                  " & vbNewLine
    ''' <summary>
    ''' データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU As String = "SELECT                                                                        " & vbNewLine _
                                               & " GL.NRS_BR_CD                                  AS NRS_BR_CD                   " & vbNewLine _
                                               & ",GL.SEKY_YM                                    AS SEKY_YM                     " & vbNewLine _
                                               & ",GL.DEPART                                     AS DEPART                      " & vbNewLine _
                                               & ",Z1.KBN_NM1                                    AS DEPART_NM                   " & vbNewLine _
                                               & ",GL.SEKY_KMK                                   AS SEKY_KMK                    " & vbNewLine _
                                               & ",Z2.KBN_NM1                                    AS SEKY_KMK_NM                 " & vbNewLine _
                                               & ",GL.FRB_CD                                     AS FRB_CD                      " & vbNewLine _
                                               & ",GL.SRC_CD                                     AS SRC_CD                      " & vbNewLine _
                                               & ",GL.COST_CENTER                                AS COST_CENTER                 " & vbNewLine _
                                               & ",GL.MISK_CD                                    AS MISK_CD                     " & vbNewLine _
                                               & ",GL.DEST_CTY                                   AS DEST_CTY                    " & vbNewLine _
                                               & ",Z3.KBN_NM1                                    AS MISK_NM                     " & vbNewLine _
                                               & ",GL.AMOUNT                                     AS AMOUNT                      " & vbNewLine _
                                               & ",GL.VAT_AMOUNT                                 AS VAT_AMOUNT                  " & vbNewLine _
                                               & ",GL.SOUND                                      AS SOUND                       " & vbNewLine _
                                               & ",GL.BOND                                       AS BOND                        " & vbNewLine _
                                               & ",GL.JIDO_FLAG                                  AS JIDO_FLAG                   " & vbNewLine _
                                               & ",GL.SHUDO_FLAG                                 AS SHUDO_FLAG                  " & vbNewLine _
                                               & ",GL.SYS_ENT_DATE                               AS SYS_ENT_DATE                " & vbNewLine _
                                               & ",USER1.USER_NM                                 AS SYS_ENT_USER_NM             " & vbNewLine _
                                               & ",GL.SYS_UPD_DATE                               AS SYS_UPD_DATE                " & vbNewLine _
                                               & ",GL.SYS_UPD_TIME                               AS SYS_UPD_TIME                " & vbNewLine _
                                               & ",USER2.USER_NM                                 AS SYS_UPD_USER_NM             " & vbNewLine _
                                               & ",GL.SYS_DEL_FLG                                AS SYS_DEL_FLG                 " & vbNewLine _
                                               & ",Z4.KBN_NM1                                    AS SYS_DEL_NM                  " & vbNewLine _
                                               & ",NRSBR.NRS_BR_NM                               AS NRS_BR_NM                   " & vbNewLine _
                                               & ",Z5.KBN_NM2                                    AS JIDO_FLAG_NM                " & vbNewLine _
                                               & ",Z6.KBN_NM2                                    AS SHUDO_FLAG_NM               " & vbNewLine
    'END YANAI 要望番号830

#End Region

#Region "データの検索 SQL FROM句"

    'START YANAI 要望番号830
    '''' <summary>
    '''' データの検索 SQL FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_FROM_KENSAKU As String = "FROM                                                                     " & vbNewLine _
    '                                                & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL GL                                            " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..S_USER USER1                                                   " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "USER1.USER_CD = GL.SYS_ENT_USER                                          " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..S_USER USER2                                                   " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "USER2.USER_CD = GL.SYS_UPD_USER                                          " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..Z_KBN Z1                                                       " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "Z1.KBN_CD = GL.DEPART                                                    " & vbNewLine _
    '                                                & "AND                                                                      " & vbNewLine _
    '                                                & "Z1.KBN_GROUP_CD = 'Z009'                                                 " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..Z_KBN Z2                                                       " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "Z2.KBN_CD = GL.SEKY_KMK                                                  " & vbNewLine _
    '                                                & "AND                                                                      " & vbNewLine _
    '                                                & "Z2.KBN_GROUP_CD = 'S029'                                                 " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..Z_KBN Z3                                                       " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "Z3.KBN_CD = GL.MISK_CD                                                   " & vbNewLine _
    '                                                & "AND                                                                      " & vbNewLine _
    '                                                & "Z3.KBN_GROUP_CD = 'M013'                                                 " & vbNewLine _
    '                                                & "LEFT JOIN                                                                " & vbNewLine _
    '                                                & "$LM_MST$..Z_KBN Z4                                                       " & vbNewLine _
    '                                                & "ON                                                                       " & vbNewLine _
    '                                                & "Z4.KBN_CD = GL.SYS_DEL_FLG                                               " & vbNewLine _
    '                                                & "AND                                                                      " & vbNewLine _
    '                                                & "Z4.KBN_GROUP_CD = 'S051'                                                 " & vbNewLine
    ''' <summary>
    ''' データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENSAKU As String = "FROM                                                                     " & vbNewLine _
                                                    & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL GL                                            " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..S_USER USER1                                                   " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "USER1.USER_CD = GL.SYS_ENT_USER                                          " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..S_USER USER2                                                   " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "USER2.USER_CD = GL.SYS_UPD_USER                                          " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z1                                                       " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z1.KBN_CD = GL.DEPART                                                    " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "Z1.KBN_GROUP_CD = 'Z009'                                                 " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z2                                                       " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z2.KBN_CD = GL.SEKY_KMK                                                  " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "Z2.KBN_GROUP_CD = 'S029'                                                 " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z3                                                       " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z3.KBN_CD = GL.MISK_CD                                                   " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "Z3.KBN_GROUP_CD = 'M013'                                                 " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z4                                                       " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z4.KBN_CD = GL.SYS_DEL_FLG                                               " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "Z4.KBN_GROUP_CD = 'S051'                                                 " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..M_NRS_BR NRSBR                                                 " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "NRSBR.NRS_BR_CD = GL.NRS_BR_CD                                           " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z5                                                       " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z5.KBN_CD = GL.JIDO_FLAG                                                  " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "Z5.KBN_GROUP_CD = 'U009'                                                 " & vbNewLine _
                                                    & "LEFT JOIN                                                                " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z6                                                       " & vbNewLine _
                                                    & "ON                                                                       " & vbNewLine _
                                                    & "Z6.KBN_CD = GL.SHUDO_FLAG                                                  " & vbNewLine _
                                                    & "AND                                                                      " & vbNewLine _
                                                    & "Z6.KBN_GROUP_CD = 'U009'                                                 " & vbNewLine _
   'END YANAI 要望番号830

#End Region

#Region "データの検索 SQL ORDER BY句"

    ''' <summary>
    ''' データの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_KENSAKU As String = "ORDER BY                                                                " & vbNewLine _
                                                     & " GL.NRS_BR_CD                                                           " & vbNewLine _
                                                     & ",GL.SEKY_YM                                                             " & vbNewLine _
                                                     & ",GL.DEPART                                                              " & vbNewLine _
                                                     & ",GL.SEKY_KMK                                                            " & vbNewLine _
                                                     & ",GL.FRB_CD                                                              " & vbNewLine _
                                                     & ",GL.SRC_CD                                                              " & vbNewLine

#End Region

#Region "データ重複検索 SQL FROM句"

    ''' <summary>
    ''' データ重複検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_INSERT As String = "FROM                                                                     " & vbNewLine _
                                                    & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL GL                                           " & vbNewLine

#End Region

#Region "データ重複検索 SQL WHERE句"

    ''' <summary>
    ''' データ重複検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_INSERT As String = "WHERE                                                                   " & vbNewLine _
                                                    & "GL.NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine _
                                                    & "AND                                                                     " & vbNewLine _
                                                    & "GL.SEKY_YM = @SEKY_YM                                                   " & vbNewLine _
                                                    & "AND                                                                     " & vbNewLine _
                                                    & "GL.DEPART = @DEPART                                                     " & vbNewLine _
                                                    & "AND                                                                     " & vbNewLine _
                                                    & "GL.SEKY_KMK = @SEKY_KMK                                                 " & vbNewLine _
                                                    & "AND                                                                     " & vbNewLine _
                                                    & "GL.FRB_CD = @FRB_CD                                                     " & vbNewLine _
                                                    & "AND                                                                     " & vbNewLine _
                                                    & "GL.SRC_CD = @SRC_CD                                                     " & vbNewLine _
                                                    & "AND                                                                     " & vbNewLine _
                                                    & "GL.COST_CENTER = @COST_CENTER                                           " & vbNewLine _
                                                    & "AND                                                                     " & vbNewLine _
                                                    & "GL.MISK_CD = @MISK_CD                                                   " & vbNewLine

#End Region

#Region "CSV出力データの検索(運賃データ送信ファイル作成) SQL SELECT句(出荷)"

    ''' <summary>
    ''' CSV出力データの検索(運賃データ送信ファイル作成) SQL SELECT句(出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CsvUNCHIN_OUTKA As String = "SELECT                                                                " & vbNewLine _
                                                       & " OUTKAL.CUST_ORD_NO                    AS CUST_ORD_NO                 " & vbNewLine _
                                                       & ",GOODS.GOODS_CD_CUST                   AS GOODS_CD_CUST               " & vbNewLine _
                                                       & ",UNCHIN.DECI_UNCHIN +                                                 " & vbNewLine _
                                                       & " UNCHIN.DECI_CITY_EXTC +                                              " & vbNewLine _
                                                       & " UNCHIN.DECI_WINT_EXTC +                                              " & vbNewLine _
                                                       & " UNCHIN.DECI_RELY_EXTC +                                              " & vbNewLine _
                                                       & " UNCHIN.DECI_TOLL +                                                   " & vbNewLine _
                                                       & " UNCHIN.DECI_INSU                      AS DECI_UNCHIN                 " & vbNewLine

#End Region

#Region "CSV出力データの検索(運賃データ送信ファイル作成)SQL FROM句(出荷)"

    ''' <summary>
    ''' CSV出力データの検索(運賃データ送信ファイル作成) SQL FROM句(出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CsvUNCHIN_OUTKA As String = "FROM                                                             " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_L OUTKAL                                             " & vbNewLine _
                                                      & "LEFT JOIN                                                              " & vbNewLine _
                                                      & "$LM_TRN$..C_OUTKA_M OUTKAM                                             " & vbNewLine _
                                                      & "ON                                                                     " & vbNewLine _
                                                      & "OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                    " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                  " & vbNewLine _
                                                      & "AND                                                                    " & vbNewLine _
                                                      & "OUTKAM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
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
                                                      & "GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                               " & vbNewLine

#End Region

#Region "CSV出力データの検索(運賃データ送信ファイル作成) SQL SELECT句(入荷)"

    ''' <summary>
    ''' CSV出力データの検索(運賃データ送信ファイル作成) SQL SELECT句(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CsvUNCHIN_INKA As String = "SELECT                                                                 " & vbNewLine _
                                                       & " ''                                    AS CUST_ORD_NO                 " & vbNewLine _
                                                       & ",GOODS.GOODS_CD_CUST                   AS GOODS_CD_CUST               " & vbNewLine _
                                                       & ",UNCHIN.DECI_UNCHIN +                                                 " & vbNewLine _
                                                       & " UNCHIN.DECI_CITY_EXTC +                                              " & vbNewLine _
                                                       & " UNCHIN.DECI_WINT_EXTC +                                              " & vbNewLine _
                                                       & " UNCHIN.DECI_RELY_EXTC +                                              " & vbNewLine _
                                                       & " UNCHIN.DECI_TOLL +                                                   " & vbNewLine _
                                                       & " UNCHIN.DECI_INSU                      AS DECI_UNCHIN                 " & vbNewLine

#End Region

#Region "CSV出力データの検索(運賃データ送信ファイル作成)SQL FROM句(入荷)"

    ''' <summary>
    ''' CSV出力データの検索(運賃データ送信ファイル作成) SQL FROM句(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CsvUNCHIN_INKA As String = "FROM                                                          " & vbNewLine _
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
                                                  & "GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                " & vbNewLine

#End Region

#Region "CSV出力データの検索(請求データ送信ファイル作成) SQL SELECT句"

    ''' <summary>
    ''' CSV出力データの検索(請求データ送信ファイル作成) SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CsvGL As String = "SELECT                                                                " & vbNewLine _
                                             & " GL.FRB_CD                             AS FRB_CD                      " & vbNewLine _
                                             & ",GL.SRC_CD                             AS SRC_CD                      " & vbNewLine _
                                             & ",GL.AMOUNT                             AS AMOUNT                      " & vbNewLine _
                                             & ",GL.VAT_AMOUNT                         AS VAT_AMOUNT                  " & vbNewLine

#End Region

#Region "CSV出力データの検索(請求データ送信ファイル作成)SQL FROM句"

    ''' <summary>
    ''' CSV出力データの検索(請求データ送信ファイル作成) SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CsvGL As String = "FROM                                                             " & vbNewLine _
                                                  & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL GL                                    " & vbNewLine

#End Region

#Region "CSV出力データの検索 SQL UNION句"

    ''' <summary>
    ''' CSV出力データの検索 SQL UNION句(
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNION_CSV As String = "UNION                                                                     " & vbNewLine

#End Region

#Region "CSV出力データの検索(運賃データ送信ファイル作成) SQL SELECT句(運賃)"

    ''' <summary>
    ''' CSV出力データの検索(運賃データ送信ファイル作成) SQL SELECT句(運賃)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CsvUNCHIN_UNCHIN As String = "SELECT                                                               " & vbNewLine _
                                                       & " ''                                    AS CUST_ORD_NO                 " & vbNewLine _
                                                       & ",ISNULL(GOODS1.GOODS_CD_CUST,GOODS2.GOODS_CD_CUST) AS GOODS_CD_CUST   " & vbNewLine _
                                                       & ",UNCHIN.DECI_UNCHIN                    AS DECI_UNCHIN                 " & vbNewLine

#End Region

#Region "CSV出力データの検索(運賃データ送信ファイル作成)SQL FROM句(運賃)"

    ''' <summary>
    ''' CSV出力データの検索(運賃データ送信ファイル作成) SQL FROM句(運賃)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CsvUNCHIN_UNCHIN As String = "FROM                                                        " & vbNewLine _
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
                                                  & "INKAM.INKA_NO_M = '001'                                                " & vbNewLine _
                                                  & "AND                                                                    " & vbNewLine _
                                                  & "INKAM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                  & "LEFT JOIN                                                              " & vbNewLine _
                                                  & "$LM_MST$..M_GOODS GOODS1                                               " & vbNewLine _
                                                  & "ON                                                                     " & vbNewLine _
                                                  & "GOODS1.NRS_BR_CD = INKAM.NRS_BR_CD                                     " & vbNewLine _
                                                  & "AND                                                                    " & vbNewLine _
                                                  & "GOODS1.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                               " & vbNewLine _
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
                                                  & "OUTKAM.OUTKA_NO_M = '001'                                              " & vbNewLine _
                                                  & "AND                                                                    " & vbNewLine _
                                                  & "OUTKAM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                  & "LEFT JOIN                                                              " & vbNewLine _
                                                  & "$LM_MST$..M_GOODS GOODS2                                               " & vbNewLine _
                                                  & "ON                                                                     " & vbNewLine _
                                                  & "GOODS2.NRS_BR_CD = OUTKAM.NRS_BR_CD                                    " & vbNewLine _
                                                  & "AND                                                                    " & vbNewLine _
                                                  & "GOODS2.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                              " & vbNewLine

#End Region

#Region "CSV出力データの検索(FPDEデータファイル作成) SQL SELECT句"

    ''' <summary>
    ''' CSV出力データの検索(FPDEデータファイル作成) SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CsvFPDE As String = "SELECT                                                                " & vbNewLine _
                                               & " GL.NRS_BR_CD                          AS NRS_BR_CD                   " & vbNewLine _
                                               & ",GL.SEKY_YM                            AS SEKY_YM                     " & vbNewLine _
                                               & ",GL.DEPART                             AS DEPART                      " & vbNewLine _
                                               & ",GL.SEKY_KMK                           AS SEKY_KMK                    " & vbNewLine _
                                               & ",Z1.KBN_NM2                            AS ACCOUNT                     " & vbNewLine _
                                               & ",GL.FRB_CD                             AS FRB_CD                      " & vbNewLine _
                                               & ",GL.SRC_CD                             AS SRC_CD                      " & vbNewLine _
                                               & ",Z2.KBN_NM2                            AS SRC_NO                      " & vbNewLine _
                                               & ",GL.COST_CENTER                        AS COST_CENTER                 " & vbNewLine _
                                               & ",GL.MISK_CD                            AS MISK_CD                     " & vbNewLine _
                                               & ",GL.DEST_CTY                           AS DEST_CTY                    " & vbNewLine _
                                               & ",GL.AMOUNT                             AS AMOUNT                      " & vbNewLine _
                                               & ",GL.VAT_AMOUNT                         AS VAT_AMOUNT                  " & vbNewLine _
                                               & ",GL.SOUND                              AS SOUND                       " & vbNewLine _
                                               & ",GL.BOND                               AS BOND                        " & vbNewLine _
                                               & ",GL.JIDO_FLAG                          AS JIDO_FLAG                   " & vbNewLine _
                                               & ",GL.SHUDO_FLAG                         AS SHUDO_FLAG                  " & vbNewLine

    ''' <summary>
    ''' CSV出力データの検索(FPDEデータファイルアクサルタ用作成) SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CsvFPDE_AKUSARUTA As String = "SELECT                                                                " & vbNewLine _
                                               & " GL.NRS_BR_CD                          AS NRS_BR_CD                   " & vbNewLine _
                                               & ",GL.SEKY_YM                            AS SEKY_YM                     " & vbNewLine _
                                               & ",GL.DEPART                             AS DEPART                      " & vbNewLine _
                                               & ",GL.SEKY_KMK                           AS SEKY_KMK                    " & vbNewLine _
                                               & ",Z1.KBN_NM3                            AS ACCOUNT                     " & vbNewLine _
                                               & ",GL.FRB_CD                             AS FRB_CD                      " & vbNewLine _
                                               & ",GL.SRC_CD                             AS SRC_CD                      " & vbNewLine _
                                               & ",Z2.KBN_NM2                            AS SRC_NO                      " & vbNewLine _
                                               & ",GL.COST_CENTER                        AS COST_CENTER                 " & vbNewLine _
                                               & ",GL.MISK_CD                            AS MISK_CD                     " & vbNewLine _
                                               & ",GL.DEST_CTY                           AS DEST_CTY                    " & vbNewLine _
                                               & ",GL.AMOUNT                             AS AMOUNT                      " & vbNewLine _
                                               & ",GL.VAT_AMOUNT                         AS VAT_AMOUNT                  " & vbNewLine _
                                               & ",GL.SOUND                              AS SOUND                       " & vbNewLine _
                                               & ",GL.BOND                               AS BOND                        " & vbNewLine _
                                               & ",GL.JIDO_FLAG                          AS JIDO_FLAG                   " & vbNewLine _
                                               & ",GL.SHUDO_FLAG                         AS SHUDO_FLAG                  " & vbNewLine

#End Region

#Region "CSV出力データの検索(FPDEデータファイル作成)SQL FROM句"

    ''' <summary>
    ''' CSV出力データの検索(FPDEデータファイル作成) SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CsvFPDE As String = "FROM                                                             " & vbNewLine _
                                                    & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL GL                                " & vbNewLine _
                                                    & "LEFT JOIN                                                        " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z1                                               " & vbNewLine _
                                                    & "ON                                                               " & vbNewLine _
                                                    & "Z1.KBN_CD = GL.SEKY_KMK                                          " & vbNewLine _
                                                    & "AND                                                              " & vbNewLine _
                                                    & "Z1.KBN_GROUP_CD = 'S029'                                         " & vbNewLine _
                                                    & "LEFT JOIN                                                        " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN Z2                                               " & vbNewLine _
                                                    & "ON                                                               " & vbNewLine _
                                                    & "Z2.KBN_CD = GL.SRC_CD                                            " & vbNewLine _
                                                    & "AND                                                              " & vbNewLine _
                                                    & "Z2.KBN_GROUP_CD = 'S028'                                         " & vbNewLine _
                                                    & "LEFT JOIN $LM_MST$..Z_KBN Z3                                     " & vbNewLine _
                                                    & "ON  Z3.KBN_GROUP_CD  = 'Z009'                                    " & vbNewLine _
                                                    & "AND Z3.KBN_CD        =  GL.DEPART                                " & vbNewLine _
                                                    & "LEFT JOIN $LM_MST$..Z_KBN Z4       		                        " & vbNewLine _
                                                    & "ON  Z4.KBN_GROUP_CD  = 'D016'         		                    " & vbNewLine _
                                                    & "AND Z4.KBN_CD        =  Z3.KBN_NM3         		                " & vbNewLine

#End Region

#Region "CSV出力データの検索(FPDEデータファイル作成)SQL ORDER BY句"

    ''' <summary>
    ''' CSV出力データの検索(FPDEデータファイル作成) SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_CsvFPDE As String = "ORDER BY                                                        " & vbNewLine _
                                                     & " Z1.KBN_NM2                                                     " & vbNewLine _
                                                     & ",GL.DEPART                                                      " & vbNewLine _
                                                     & ",GL.SRC_CD                                                      " & vbNewLine _
                                                     & ",GL.FRB_CD                                                      " & vbNewLine

#End Region

#End Region

#Region "INSERT"

#Region "G_DUPONT_SEKY_GL"

    ''' <summary>
    ''' INSERT（G_DUPONT_SEKY_GL）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_GL As String = "INSERT INTO                                             " & vbNewLine _
                                         & "$LM_TRN_DPN$..G_DUPONT_SEKY_GL                               " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",SEKY_YM                                                 " & vbNewLine _
                                         & ",DEPART                                                  " & vbNewLine _
                                         & ",SEKY_KMK                                                " & vbNewLine _
                                         & ",FRB_CD                                                  " & vbNewLine _
                                         & ",SRC_CD                                                  " & vbNewLine _
                                         & ",COST_CENTER                                             " & vbNewLine _
                                         & ",MISK_CD                                                 " & vbNewLine _
                                         & ",DEST_CTY                                                " & vbNewLine _
                                         & ",AMOUNT                                                  " & vbNewLine _
                                         & ",VAT_AMOUNT                                              " & vbNewLine _
                                         & ",SOUND                                                   " & vbNewLine _
                                         & ",BOND                                                    " & vbNewLine _
                                         & ",JIDO_FLAG                                               " & vbNewLine _
                                         & ",SHUDO_FLAG                                              " & vbNewLine _
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
                                         & ",@SEKY_YM                                                " & vbNewLine _
                                         & ",@DEPART                                                 " & vbNewLine _
                                         & ",@SEKY_KMK                                               " & vbNewLine _
                                         & ",@FRB_CD                                                 " & vbNewLine _
                                         & ",@SRC_CD                                                 " & vbNewLine _
                                         & ",@COST_CENTER                                            " & vbNewLine _
                                         & ",@MISK_CD                                                " & vbNewLine _
                                         & ",@DEST_CTY                                               " & vbNewLine _
                                         & ",@AMOUNT                                                 " & vbNewLine _
                                         & ",@VAT_AMOUNT                                             " & vbNewLine _
                                         & ",@SOUND                                                  " & vbNewLine _
                                         & ",@BOND                                                   " & vbNewLine _
                                         & ",@JIDO_FLAG                                              " & vbNewLine _
                                         & ",@SHUDO_FLAG                                             " & vbNewLine _
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

#End Region

#Region "UPDATE"

#Region "G_DUPONT_SEKY_GL"

    ''' <summary>
    ''' UPDATE（G_DUPONT_SEKY_GL）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_GL As String = "UPDATE $LM_TRN_DPN$..G_DUPONT_SEKY_GL SET                   " & vbNewLine _
                                         & " AMOUNT = AMOUNT + @AMOUNT                               " & vbNewLine _
                                         & ",VAT_AMOUNT  = VAT_AMOUNT + @VAT_AMOUNT                  " & vbNewLine _
                                         & ",SOUND = SOUND + @SOUND                                  " & vbNewLine _
                                         & ",BOND = BOND + @BOND                                     " & vbNewLine _
                                         & ",SHUDO_FLAG = '01'                            " & vbNewLine _
                                         & ",SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
                                         & ",SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
                                         & ",SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
                                         & ",SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
                                         & "WHERE                                                    " & vbNewLine _
                                         & "NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "SEKY_YM = @SEKY_YM                                       " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "DEPART = @DEPART                                         " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "SEKY_KMK = @SEKY_KMK                                     " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "FRB_CD = @FRB_CD                                         " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "SRC_CD = @SRC_CD                                         " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "COST_CENTER = @COST_CENTER                               " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "MISK_CD = @MISK_CD                                       " & vbNewLine

#End Region

#End Region

#Region "DELETE"

#Region "G_DUPONT_SEKY_GL"

    Private Const SQL_DELETE_GL As String = "DELETE FROM $LM_TRN_DPN$..G_DUPONT_SEKY_GL    " & vbNewLine _
                                          & "WHERE   NRS_BR_CD   = @NRS_BR_CD          " & vbNewLine _
                                          & "  AND   SEKY_YM   = @SEKY_YM_OLD          " & vbNewLine _
                                          & "  AND   DEPART   = @DEPART_OLD            " & vbNewLine _
                                          & "  AND   SEKY_KMK   = @SEKY_KMK_OLD        " & vbNewLine _
                                          & "  AND   FRB_CD   = @FRB_CD_OLD            " & vbNewLine _
                                          & "  AND   SRC_CD   = @SRC_CD_OLD            " & vbNewLine _
                                          & "  AND   COST_CENTER   = @COST_CENTER_OLD  " & vbNewLine _
                                          & "  AND   MISK_CD   = @MISK_CD_OLD          " & vbNewLine

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

#End Region

#Region "Method"

#Region "SQLメイン処理"

#Region "SELECT"

    ''' <summary>
    ''' データの検索(件数)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_KENSAKU_COUNT)       'SQL構築(Select句)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_KENSAKU)        'SQL構築(From句)
        Call Me.SetSQLWhereSelectData(inTbl.Rows(0))                '条件設定

        'スキーマ名設定
        'START YANAI 要望番号830
        'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString())
        'END YANAI 要望番号830

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_KENSAKU)        'SQL構築(Select句)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_KENSAKU)   'SQL構築(From句)
        Call Me.SetSQLWhereSelectData(inTbl.Rows(0))           '条件設定
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_ORDER_KENSAKU)  'SQL構築(Where句)

        'スキーマ名設定
        'START YANAI 要望番号830
        'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString())
        'END YANAI 要望番号830

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("DEPART", "DEPART")
        map.Add("DEPART_NM", "DEPART_NM")
        map.Add("SEKY_KMK", "SEKY_KMK")
        map.Add("SEKY_KMK_NM", "SEKY_KMK_NM")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("COST_CENTER", "COST_CENTER")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("MISK_NM", "MISK_NM")
        map.Add("DEST_CTY", "DEST_CTY")
        map.Add("AMOUNT", "AMOUNT")
        map.Add("VAT_AMOUNT", "VAT_AMOUNT")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("JIDO_FLAG", "JIDO_FLAG")
        map.Add("SHUDO_FLAG", "SHUDO_FLAG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        'START YANAI 要望番号830
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("JIDO_FLAG_NM", "JIDO_FLAG_NM")
        map.Add("SHUDO_FLAG_NM", "SHUDO_FLAG_NM")
        'END YANAI 要望番号830

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI040OUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' データ重複検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_KENSAKU_COUNT)       'SQL構築(Select句)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_INSERT)         'SQL構築(From句)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_WHERE_INSERT)        'SQL構築(From句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetGLSelectComParameter(inTbl.Rows(0))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "SelectInsertData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' CSV出力データの検索(運賃データ送信ファイル作成)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCsvDataUNCHIN(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_CsvUNCHIN_OUTKA)        'SQL構築(Select句 出荷)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_CsvUNCHIN_OUTKA)   'SQL構築(From句 出荷)
        Call Me.SetSQLWhereCsvDataUNCHIN_OUTKA(inTbl.Rows(0))          '条件設定(出荷)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_UNION_CSV)              'SQL構築(UNION句)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_CsvUNCHIN_INKA)         'SQL構築(Select句 入荷)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_CsvUNCHIN_INKA)    'SQL構築(From句 入荷)
        Call Me.SetSQLWhereCsvDataUNCHIN_INKA(inTbl.Rows(0))           '条件設定(入荷)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_UNION_CSV)              'SQL構築(UNION句)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_CsvUNCHIN_UNCHIN)       'SQL構築(Select句 運賃)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_CsvUNCHIN_UNCHIN)  'SQL構築(From句 運賃)
        Call Me.SetSQLWhereCsvDataUNCHIN_UNCHIN(inTbl.Rows(0))         '条件設定(運賃)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "SelectCsvDataUNCHIN", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI040_UNCHINOUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' CSV出力データの検索(請求データ送信ファイル作成)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCsvDataGL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_CsvGL)        'SQL構築(Select句)
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_CsvGL)   'SQL構築(From句)
        Call Me.SetSQLWhereCsvDataGL(inTbl.Rows(0))          '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "SelectCsvDataGL", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("FRB_CD", "FRB_CD")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("AMOUNT", "AMOUNT")
        map.Add("VAT_AMOUNT", "VAT_AMOUNT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI040_GL")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' CSV出力データの検索(FPDEデータファイル作成)(非課税)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCsvDataFPDE_HIKAZEI(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        If inTbl.Rows(0).Item("SEIQTO_KBN").ToString() = "01" Then
            'アクサルタ
            Me._StrSql.Append(LMI040DAC.SQL_SELECT_CsvFPDE_AKUSARUTA)        'SQL構築(Select句)
        Else
            'デュポン
            Me._StrSql.Append(LMI040DAC.SQL_SELECT_CsvFPDE)        'SQL構築(Select句)
        End If

        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_CsvFPDE)   'SQL構築(From句)
        Call Me.SetSQLWhereCsvDataFPDE_HIKAZEI(inTbl.Rows(0))  '条件設定
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_ORDER_CsvFPDE)  'SQL構築(From句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "SelectCsvDataFPDE_HIKAZEI", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("DEPART", "DEPART")
        map.Add("SEKY_KMK", "SEKY_KMK")
        map.Add("ACCOUNT", "ACCOUNT")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("SRC_NO", "SRC_NO")
        map.Add("COST_CENTER", "COST_CENTER")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("DEST_CTY", "DEST_CTY")
        map.Add("AMOUNT", "AMOUNT")
        map.Add("VAT_AMOUNT", "VAT_AMOUNT")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("JIDO_FLAG", "JIDO_FLAG")
        map.Add("SHUDO_FLAG", "SHUDO_FLAG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI040_FPDE_GL_HIKAZEI")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' CSV出力データの検索(FPDEデータファイル作成)(課税)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCsvDataFPDE_KAZEI(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        If inTbl.Rows(0).Item("SEIQTO_KBN").ToString() = "01" Then
            'アクサルタ
            Me._StrSql.Append(LMI040DAC.SQL_SELECT_CsvFPDE_AKUSARUTA)        'SQL構築(Select句)
        Else
            'デュポン
            Me._StrSql.Append(LMI040DAC.SQL_SELECT_CsvFPDE)        'SQL構築(Select句)
        End If

        Me._StrSql.Append(LMI040DAC.SQL_SELECT_FROM_CsvFPDE)   'SQL構築(From句)
        Call Me.SetSQLWhereCsvDataFPDE_KAZEI(inTbl.Rows(0))    '条件設定
        Me._StrSql.Append(LMI040DAC.SQL_SELECT_ORDER_CsvFPDE)  'SQL構築(From句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "SelectCsvDataFPDE_KAZEI", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("DEPART", "DEPART")
        map.Add("SEKY_KMK", "SEKY_KMK")
        map.Add("ACCOUNT", "ACCOUNT")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("SRC_NO", "SRC_NO")
        map.Add("COST_CENTER", "COST_CENTER")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("DEST_CTY", "DEST_CTY")
        map.Add("AMOUNT", "AMOUNT")
        map.Add("VAT_AMOUNT", "VAT_AMOUNT")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("JIDO_FLAG", "JIDO_FLAG")
        map.Add("SHUDO_FLAG", "SHUDO_FLAG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI040_FPDE_GL_KAZEI")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "INSERT"

#Region "G_DUPONT_SEKY_GL新規登録"

    ''' <summary>
    ''' G_DUPONT_SEKY_GL新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>G_DUPONT_SEKY_GL新規登録SQLの構築・発行</remarks>
    Private Function InsertGLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI040DAC.SQL_INSERT_GL, inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetGLComParameter(inTbl.Rows(0))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "InsertGLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "UPDATE"

#Region "デュポン請求GLマスタの更新"

    ''' <summary>
    ''' デュポン請求GLマスタの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateGLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI040DAC.SQL_UPDATE_GL)         'SQL構築(SET句)

        'パラメータ設定
        Call Me.SetGLUpdParameter(inTbl.Rows(0))
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "UpdateGLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "DELETE"

#Region "編集保存時のG_DUPONT_SEKY_GL削除"

    ''' <summary>
    ''' 編集保存時のG_DUPONT_SEKY_GL削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>G_DUPONT_SEKY_GL削除SQLの構築・発行</remarks>
    Private Function EditDelData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI040DAC.SQL_DELETE_GL, inTbl.Rows(0).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString()))

        'パラメータ設定
        Call Me.SetGLEditDelParameter(inTbl.Rows(0))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI040DAC", "EditDelData", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "削除のG_DUPONT_SEKY_GL削除"

    ''' <summary>
    ''' G_DUPONT_SEKY_GL削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>G_DUPONT_SEKY_GL削除SQLの構築・発行</remarks>
    Private Function DeleteGLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI040IN")
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI040DAC.SQL_DELETE_GL, inTbl.Rows(i).Item("NRS_BR_CD").ToString(), inTbl.Rows(0).Item("MAIN_BR").ToString()))

            'パラメータ設定
            Call Me.SetGLEditDelParameter(inTbl.Rows(i))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI040DAC", "DeleteGLData", cmd)

            'SQLの発行
            Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

#End Region

#End Region

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSelectData(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("1 = 1                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND GL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '請求月
            whereStr = .Item("SEKY_YM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GL.SEKY_YM = @SEKY_YM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", whereStr, DBDataType.CHAR))
            End If

            '事業部
            whereStr = .Item("DEPART").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GL.DEPART = @DEPART")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", whereStr, DBDataType.CHAR))
            End If

            '請求項目
            whereStr = .Item("SEKY_KMK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GL.SEKY_KMK = @SEKY_KMK")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", whereStr, DBDataType.CHAR))
            End If

            'ミスク
            whereStr = .Item("MISK_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If ("00").Equals(whereStr) = True Then
                    '通常の場合
                    Me._StrSql.Append(" AND GL.MISK_CD <> '**' AND GL.MISK_CD <> '##'")
                End If
                If ("01").Equals(whereStr) = True Then
                    'ミスクの場合
                    Me._StrSql.Append(" AND GL.MISK_CD = '**'")
                End If
                If ("02").Equals(whereStr) = True Then
                    '名変の場合
                    Me._StrSql.Append(" AND GL.MISK_CD = '##'")
                End If
                Me._StrSql.Append(vbNewLine)
            End If

            'START YANAI 要望番号830
            'FRBコード
            whereStr = .Item("FRB_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GL.FRB_CD LIKE @FRB_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRB_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'SRCコード
            whereStr = .Item("SRC_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GL.SRC_CD LIKE @SRC_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SRC_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'コストセンター
            whereStr = .Item("COST_CENTER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GL.COST_CENTER LIKE @COST_CENTER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COST_CENTER", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'コストセンター
            whereStr = .Item("DEST_CTY").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GL.DEST_CTY LIKE @DEST_CTY")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CTY", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '作成日
            whereStr = .Item("SYS_ENT_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(GL.SYS_ENT_DATE,1,6) = @SYS_ENT_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", whereStr, DBDataType.CHAR))
            End If
            'END YANAI 要望番号830

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(CSV出力データの検索(運賃データ送信ファイル作成)(出荷))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereCsvDataUNCHIN_OUTKA(ByVal inTblRow As DataRow)

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

            ''営業所
            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("  AND OUTKAL.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If

            '請求月
            whereStr = .Item("SEKY_YM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(OUTKAL.OUTKA_PLAN_DATE,1,6) = @SEKY_YM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(CSV出力データの検索(運賃データ送信ファイル作成)(入荷))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereCsvDataUNCHIN_INKA(ByVal inTblRow As DataRow)

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

            ''営業所
            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("  AND INKAL.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If

            '請求月
            whereStr = .Item("SEKY_YM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(INKAL.INKA_DATE,1,6) = @SEKY_YM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(CSV出力データの検索(運賃データ送信ファイル作成)(運賃))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereCsvDataUNCHIN_UNCHIN(ByVal inTblRow As DataRow)

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

            ''営業所
            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("  AND UNSOL.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If

            '請求月
            whereStr = .Item("SEKY_YM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(UNSOL.OUTKA_PLAN_DATE,1,6) = @SEKY_YM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(CSV出力データの検索(請求データ送信ファイル作成))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereCsvDataGL(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("GL.SYS_DEL_FLG = '0'                                           ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND GL.SEKY_KMK = '00'                                        ")
            Me._StrSql.Append(vbNewLine)

            ''営業所
            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("  AND GL.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If

            '請求月
            whereStr = .Item("SEKY_YM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(GL.SEKY_YM,1,6) = @SEKY_YM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(CSV出力データの検索(FPDEデータファイル作成(非課税)))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereCsvDataFPDE_HIKAZEI(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("GL.SYS_DEL_FLG = '0'                                           ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND GL.SEKY_KMK <> '00'                                       ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND GL.BOND <> 0    --UPD 2020/3/17 010119 '>'→'<>'          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND Z4.KBN_CD = @SEIQTO_KBN")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_KBN", .Item("SEIQTO_KBN").ToString(), DBDataType.CHAR))

            ''営業所
            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("  AND GL.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If

            '請求月
            whereStr = .Item("SEKY_YM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(GL.SEKY_YM,1,6) = @SEKY_YM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(CSV出力データの検索(FPDEデータファイル作成(課税)))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereCsvDataFPDE_KAZEI(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("GL.SYS_DEL_FLG = '0'                                           ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND GL.SEKY_KMK <> '00'                                       ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND GL.SOUND <> 0    --UPD 2020/3/17 010119 '>'→'<>'         ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND Z4.KBN_CD = @SEIQTO_KBN")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_KBN", .Item("SEIQTO_KBN").ToString(), DBDataType.CHAR))

            ''営業所
            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("  AND GL.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If

            '請求月
            whereStr = .Item("SEKY_YM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(GL.SEKY_YM,1,6) = @SEKY_YM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", whereStr, DBDataType.CHAR))
            End If

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

#Region "保存時共通"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", systemUserID, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetSysdataParameter(prmList, dataSetNm)

    End Sub

#End Region

#Region "新規保存時"

    ''' <summary>
    ''' データ重複検索のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetGLSelectComParameter(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COST_CENTER", .Item("COST_CENTER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' G_DUPONT_SEKY_GLの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetGLComParameter(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COST_CENTER", .Item("COST_CENTER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CTY", .Item("DEST_CTY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AMOUNT", Me.FormatNumValue(.Item("AMOUNT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VAT_AMOUNT", Me.FormatNumValue(.Item("VAT_AMOUNT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOUND", Me.FormatNumValue(.Item("SOUND").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BOND", Me.FormatNumValue(.Item("BOND").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIDO_FLAG", .Item("JIDO_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHUDO_FLAG", .Item("SHUDO_FLAG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "編集保存時"

    ''' <summary>
    ''' G_DUPONT_SEKY_GLの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetGLUpdParameter(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", .Item("SEKY_YM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", .Item("DEPART").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", .Item("SEKY_KMK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRB_CD", .Item("FRB_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SRC_CD", .Item("SRC_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COST_CENTER", .Item("COST_CENTER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISK_CD", .Item("MISK_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AMOUNT", Me.FormatNumValue(.Item("AMOUNT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VAT_AMOUNT", Me.FormatNumValue(.Item("VAT_AMOUNT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOUND", Me.FormatNumValue(.Item("SOUND").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BOND", Me.FormatNumValue(.Item("BOND").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "削除時"

    ''' <summary>
    ''' G_DUPONT_SEKY_GLの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetGLEditDelParameter(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM_OLD", .Item("SEKY_YM_OLD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART_OLD", .Item("DEPART_OLD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK_OLD", .Item("SEKY_KMK_OLD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRB_CD_OLD", .Item("FRB_CD_OLD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SRC_CD_OLD", .Item("SRC_CD_OLD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COST_CENTER_OLD", .Item("COST_CENTER_OLD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISK_CD_OLD", .Item("MISK_CD_OLD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

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

End Class
