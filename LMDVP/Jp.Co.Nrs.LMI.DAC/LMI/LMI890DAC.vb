' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI890  : NRC出荷／回収情報Excel作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI890DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI890DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "Excel出力データの検索"

#Region "NRC回収テーブルから出荷実績取得"

#Region "NRC回収テーブルから出荷実績取得 SQL"

#Region "NRC回収テーブルから出荷実績取得 SQL SELECT句"

    ''' <summary>
    ''' NRC回収テーブルから出荷実績取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SHUKKA As String = " SELECT                                                                     " & vbNewLine _
                                              & " CUST.CUST_NM_L                               AS CUST_NM_L                  " & vbNewLine _
                                              & ",OUTKAL.ARR_PLAN_DATE                         AS ARR_PLAN_DATE              " & vbNewLine _
                                              & ",''                                           AS TAIRYU                     " & vbNewLine _
                                              & ",OUTKAL.CUST_ORD_NO                           AS CUST_ORD_NO                " & vbNewLine _
                                              & ",OUTKAL.BUYER_ORD_NO                          AS BUYER_ORD_NO               " & vbNewLine _
                                              & ",ISNULL(Z1.KBN_NM2, Z2.KBN_NM2)               AS GOODS_NM                   " & vbNewLine _
                                              & ",Z3.KBN_NM2 + NRC.SERIAL_NO                   AS SERIAL_NO                  " & vbNewLine _
                                              & ",CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                          " & vbNewLine _
                                              & "      ELSE OUTKAL.DEST_NM                                                   " & vbNewLine _
                                              & " END AS DEST_NM                                                             " & vbNewLine _
                                              & ",ISNULL(DEST2.DEST_NM,'')                     AS SHIP_NM                    " & vbNewLine _
                                              & ",CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.AD_1 + DEST.AD_2                 " & vbNewLine _
                                              & "      ELSE OUTKAL.DEST_AD_1 + OUTKAL.DEST_AD_2                              " & vbNewLine _
                                              & " END AS DEST_AD                                                             " & vbNewLine _
                                              & ",OUTKAL.CUST_CD_L                             AS CUST_CD_L                  " & vbNewLine _
                                              & ",OUTKAL.DEST_CD                               AS DEST_CD                    " & vbNewLine _
                                              & ",OUTKAL.SHIP_CD_L                             AS SHIP_CD_L                  " & vbNewLine _
                                              & ",NRC.HOKOKU_DATE                              AS HOKOKU_DATE                " & vbNewLine

#End Region

#Region "NRC回収テーブルから出荷実績取得 SQL FROM句"

    ''' <summary>
    ''' NRC回収テーブルから出荷実績取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SHUKKA As String = "FROM                                                                    " & vbNewLine _
                                                   & "$LM_TRN$..C_OUTKA_L OUTKAL                                              " & vbNewLine _
                                                   & "INNER JOIN                                                              " & vbNewLine _
                                                   & "$LM_TRN$..I_NRC_KAISHU_TBL NRC                                          " & vbNewLine _
                                                   & "ON                                                                      " & vbNewLine _
                                                   & "NRC.NRS_BR_CD = OUTKAL.NRS_BR_CD                                        " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "NRC.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                      " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "NRC.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                                   & "LEFT JOIN                                                               " & vbNewLine _
                                                   & "$LM_MST$..M_CUST CUST                                                   " & vbNewLine _
                                                   & "ON                                                                      " & vbNewLine _
                                                   & "CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                       " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "CUST.CUST_CD_L = OUTKAL.CUST_CD_L                                       " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "CUST.CUST_CD_M = OUTKAL.CUST_CD_M                                       " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "CUST.CUST_CD_S = '00'                                                   " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "CUST.CUST_CD_SS = '00'                                                  " & vbNewLine _
                                                   & "LEFT JOIN                                                               " & vbNewLine _
                                                   & "$LM_MST$..Z_KBN Z1                                                      " & vbNewLine _
                                                   & "ON                                                                      " & vbNewLine _
                                                   & "Z1.KBN_GROUP_CD = 'G008'                                                " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "Z1.KBN_CD = '01'                                                        " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "Z1.KBN_NM1 = SUBSTRING(NRC.SERIAL_NO,1,1)                               " & vbNewLine _
                                                   & "LEFT JOIN                                                               " & vbNewLine _
                                                   & "$LM_MST$..Z_KBN Z2                                                      " & vbNewLine _
                                                   & "ON                                                                      " & vbNewLine _
                                                   & "Z2.KBN_GROUP_CD = 'G008'                                                " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "Z2.KBN_CD = '02'                                                        " & vbNewLine _
                                                   & "LEFT JOIN                                                               " & vbNewLine _
                                                   & "$LM_MST$..Z_KBN Z3                                                      " & vbNewLine _
                                                   & "ON                                                                      " & vbNewLine _
                                                   & "Z3.KBN_GROUP_CD = 'S081'                                                " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "Z3.KBN_CD = '01'                                                        " & vbNewLine _
                                                   & "LEFT JOIN                                                               " & vbNewLine _
                                                   & "$LM_MST$..M_DEST DEST                                                   " & vbNewLine _
                                                   & "ON                                                                      " & vbNewLine _
                                                   & "DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                       " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                       " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "DEST.DEST_CD = OUTKAL.DEST_CD                                           " & vbNewLine _
                                                   & "LEFT JOIN                                                               " & vbNewLine _
                                                   & "$LM_MST$..M_DEST DEST2                                                  " & vbNewLine _
                                                   & "ON                                                                      " & vbNewLine _
                                                   & "DEST2.NRS_BR_CD = OUTKAL.NRS_BR_CD                                      " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "DEST2.CUST_CD_L = OUTKAL.CUST_CD_L                                      " & vbNewLine _
                                                   & "AND                                                                     " & vbNewLine _
                                                   & "DEST2.DEST_CD = OUTKAL.SHIP_CD_L                                        " & vbNewLine

#End Region

#Region "NRC回収テーブルから出荷実績取得 SQL WHERE句"

    ''' <summary>
    ''' NRC回収テーブルから出荷実績取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_SHUKKA As String = "WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                                    & "  AND (OUTKAL.ARR_PLAN_DATE >= @HOKOKU_DATE_FROM                       " & vbNewLine _
                                                    & "  AND OUTKAL.ARR_PLAN_DATE <= @HOKOKU_DATE_TO)                         " & vbNewLine _
                                                    & "  AND OUTKAL.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                    & "--(2014.01.21)要望番号2144 追加START                                   " & vbNewLine _
                                                    & "  AND EXISTS (SELECT * FROM $LM_MST$..Z_KBN Z4                         " & vbNewLine _
                                                    & "  WHERE                                                                " & vbNewLine _
                                                    & "  Z4.KBN_GROUP_CD = 'F010'                                             " & vbNewLine _
                                                    & "  AND Z4.KBN_NM1 = @NRS_BR_CD                                          " & vbNewLine _
                                                    & "  AND Z4.KBN_NM2 = OUTKAL.CUST_CD_L)                                   " & vbNewLine _
                                                    & "--(2014.01.21)要望番号2144 追加END                                     " & vbNewLine

#End Region

#Region "NRC回収テーブルから出荷実績取得 SQL ORDER句"

    ''' <summary>
    ''' NRC回収テーブルから出荷実績取得 SQL ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_SHUKKA As String = "ORDER BY                                                               " & vbNewLine _
                                                    & "  OUTKAL.CUST_CD_L                                                     " & vbNewLine _
                                                    & " ,OUTKAL.ARR_PLAN_DATE                                                 " & vbNewLine _
                                                    & " ,ISNULL(Z1.KBN_NM2, Z2.KBN_NM2)                                       " & vbNewLine _
                                                    & " ,CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                    " & vbNewLine _
                                                    & "       ELSE OUTKAL.DEST_NM                                             " & vbNewLine _
                                                    & "  END                                                                  " & vbNewLine _
                                                    & " ,Z3.KBN_NM2 + NRC.SERIAL_NO                                           " & vbNewLine

#End Region

#End Region

#End Region

#Region "NRC回収テーブルから返送実績取得"

#Region "NRC回収テーブルから返送実績取得 SQL"

#Region "NRC回収テーブルから返送実績取得 SQL SELECT句"

    ''' <summary>
    ''' NRC回収テーブルから返送実績取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HENSO As String = " SELECT                                                                     " & vbNewLine _
                                             & " CUST.CUST_NM_L                               AS CUST_NM_L                  " & vbNewLine _
                                             & ",OUTKAL.ARR_PLAN_DATE                         AS ARR_PLAN_DATE              " & vbNewLine _
                                             & ",DATEDIFF(DAY, OUTKAL.ARR_PLAN_DATE, NRC.HOKOKU_DATE) AS TAIRYU             " & vbNewLine _
                                             & ",OUTKAL.CUST_ORD_NO                           AS CUST_ORD_NO                " & vbNewLine _
                                             & ",OUTKAL.BUYER_ORD_NO                          AS BUYER_ORD_NO               " & vbNewLine _
                                             & ",ISNULL(Z1.KBN_NM2, Z2.KBN_NM2)               AS GOODS_NM                   " & vbNewLine _
                                             & ",Z3.KBN_NM2 + NRC.SERIAL_NO                   AS SERIAL_NO                  " & vbNewLine _
                                             & ",CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                          " & vbNewLine _
                                             & "      ELSE OUTKAL.DEST_NM                                                   " & vbNewLine _
                                             & " END AS DEST_NM                                                             " & vbNewLine _
                                             & ",ISNULL(DEST2.DEST_NM,'')                     AS SHIP_NM                    " & vbNewLine _
                                             & ",CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.AD_1 + DEST.AD_2                 " & vbNewLine _
                                             & "      ELSE OUTKAL.DEST_AD_1 + OUTKAL.DEST_AD_2                              " & vbNewLine _
                                             & " END AS DEST_AD                                                             " & vbNewLine _
                                             & ",OUTKAL.CUST_CD_L                             AS CUST_CD_L                  " & vbNewLine _
                                             & ",OUTKAL.DEST_CD                               AS DEST_CD                    " & vbNewLine _
                                             & ",OUTKAL.SHIP_CD_L                             AS SHIP_CD_L                  " & vbNewLine _
                                             & ",NRC.HOKOKU_DATE                              AS HOKOKU_DATE                " & vbNewLine

#End Region

#Region "NRC回収テーブルから返送実績取得 SQL FROM句"

    ''' <summary>
    ''' NRC回収テーブルから返送実績取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_HENSO As String = "FROM                                                                    " & vbNewLine _
                                                  & "$LM_TRN$..C_OUTKA_L OUTKAL                                              " & vbNewLine _
                                                  & "INNER JOIN                                                              " & vbNewLine _
                                                  & "$LM_TRN$..I_NRC_KAISHU_TBL NRC                                          " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "NRC.NRS_BR_CD = OUTKAL.NRS_BR_CD                                        " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "NRC.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                      " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "NRC.OUTKA_NO_L <> '999999999'                                           " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "(NRC.HOKOKU_DATE >= @HOKOKU_DATE_FROM                                   " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "NRC.HOKOKU_DATE <= @HOKOKU_DATE_TO)                                     " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "NRC.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..M_CUST CUST                                                   " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_L = OUTKAL.CUST_CD_L                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_M = OUTKAL.CUST_CD_M                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_S = '00'                                                   " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_SS = '00'                                                  " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN Z1                                                      " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "Z1.KBN_GROUP_CD = 'G008'                                                " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "Z1.KBN_CD = '01'                                                        " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "Z1.KBN_NM1 = SUBSTRING(NRC.SERIAL_NO,1,1)                               " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN Z2                                                      " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "Z2.KBN_GROUP_CD = 'G008'                                                " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "Z2.KBN_CD = '02'                                                        " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN Z3                                                      " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "Z3.KBN_GROUP_CD = 'S081'                                                " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "Z3.KBN_CD = '01'                                                        " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..M_DEST DEST                                                   " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "DEST.DEST_CD = OUTKAL.DEST_CD                                           " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..M_DEST DEST2                                                  " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "DEST2.NRS_BR_CD = OUTKAL.NRS_BR_CD                                      " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "DEST2.CUST_CD_L = OUTKAL.CUST_CD_L                                      " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "DEST2.DEST_CD = OUTKAL.SHIP_CD_L                                        " & vbNewLine

#End Region

#Region "NRC回収テーブルから返送実績取得 SQL WHERE句"

    ''' <summary>
    ''' NRC回収テーブルから返送実績取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_HENSO As String = "WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                                   & "  AND OUTKAL.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                   & "--(2014.01.21)要望番号2144 追加START                                   " & vbNewLine _
                                                   & "  AND EXISTS (SELECT * FROM $LM_MST$..Z_KBN Z4                         " & vbNewLine _
                                                   & "  WHERE                                                                " & vbNewLine _
                                                   & "  Z4.KBN_GROUP_CD = 'F010'                                             " & vbNewLine _
                                                   & "  AND Z4.KBN_NM1 = @NRS_BR_CD                                          " & vbNewLine _
                                                   & "  AND Z4.KBN_NM2 = OUTKAL.CUST_CD_L)                                   " & vbNewLine _
                                                   & "--(2014.01.21)要望番号2144 追加END                                     " & vbNewLine

#End Region

#Region "NRC回収テーブルから返送実績取得 SQL ORDER句"

    ''' <summary>
    ''' NRC回収テーブルから返送実績取得 SQL ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_HENSO As String = "ORDER BY                                                               " & vbNewLine _
                                                   & "  OUTKAL.CUST_CD_L                                                     " & vbNewLine _
                                                   & " ,OUTKAL.ARR_PLAN_DATE                                                 " & vbNewLine _
                                                   & " ,ISNULL(Z1.KBN_NM2, Z2.KBN_NM2)                                       " & vbNewLine _
                                                   & " ,CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                    " & vbNewLine _
                                                   & "       ELSE OUTKAL.DEST_NM                                             " & vbNewLine _
                                                   & "  END                                                                  " & vbNewLine _
                                                   & " ,Z3.KBN_NM2 + NRC.SERIAL_NO                                           " & vbNewLine

#End Region

#End Region

#End Region

#Region "NRC回収テーブルから長期未返却取得"

#Region "NRC回収テーブルから長期未返却取得 SQL"

#Region "NRC回収テーブルから長期未返却取得 SQL SELECT句"

    ''' <summary>
    ''' NRC回収テーブルから長期未返却取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MIHEN As String = " SELECT                                                                     " & vbNewLine _
                                             & " CUST.CUST_NM_L                               AS CUST_NM_L                  " & vbNewLine _
                                             & ",OUTKAL.ARR_PLAN_DATE                         AS ARR_PLAN_DATE              " & vbNewLine _
                                             & ",DATEDIFF(DAY, OUTKAL.ARR_PLAN_DATE, @HOKOKU_DATE_TO) AS TAIRYU             " & vbNewLine _
                                             & ",OUTKAL.CUST_ORD_NO                           AS CUST_ORD_NO                " & vbNewLine _
                                             & ",OUTKAL.BUYER_ORD_NO                          AS BUYER_ORD_NO               " & vbNewLine _
                                             & ",ISNULL(Z1.KBN_NM2, Z2.KBN_NM2)               AS GOODS_NM                   " & vbNewLine _
                                             & ",Z3.KBN_NM2 + NRC.SERIAL_NO                   AS SERIAL_NO                  " & vbNewLine _
                                             & ",CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                          " & vbNewLine _
                                             & "      ELSE OUTKAL.DEST_NM                                                   " & vbNewLine _
                                             & " END AS DEST_NM                                                             " & vbNewLine _
                                             & ",ISNULL(DEST2.DEST_NM,'')                     AS SHIP_NM                    " & vbNewLine _
                                             & ",CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.AD_1 + DEST.AD_2                 " & vbNewLine _
                                             & "      ELSE OUTKAL.DEST_AD_1 + OUTKAL.DEST_AD_2                              " & vbNewLine _
                                             & " END AS DEST_AD                                                             " & vbNewLine _
                                             & ",OUTKAL.CUST_CD_L                             AS CUST_CD_L                  " & vbNewLine _
                                             & ",OUTKAL.DEST_CD                               AS DEST_CD                    " & vbNewLine _
                                             & ",OUTKAL.SHIP_CD_L                             AS SHIP_CD_L                  " & vbNewLine _
                                             & ",NRC.HOKOKU_DATE                              AS HOKOKU_DATE                " & vbNewLine

#End Region

#Region "NRC回収テーブルから長期未返却取得 SQL FROM句"

    ''' <summary>
    ''' NRC回収テーブルから長期未返却取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MIHEN As String = "FROM                                                                    " & vbNewLine _
                                                  & "$LM_TRN$..C_OUTKA_L OUTKAL                                              " & vbNewLine _
                                                  & "INNER JOIN                                                              " & vbNewLine _
                                                  & "$LM_TRN$..I_NRC_KAISHU_TBL NRC                                          " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "NRC.NRS_BR_CD = OUTKAL.NRS_BR_CD                                        " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "NRC.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                      " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "NRC.HOKOKU_DATE = ''                                                    " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "NRC.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..M_CUST CUST                                                   " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_L = OUTKAL.CUST_CD_L                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_M = OUTKAL.CUST_CD_M                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_S = '00'                                                   " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_SS = '00'                                                  " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN Z1                                                      " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "Z1.KBN_GROUP_CD = 'G008'                                                " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "Z1.KBN_CD = '01'                                                        " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "Z1.KBN_NM1 = SUBSTRING(NRC.SERIAL_NO,1,1)                               " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN Z2                                                      " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "Z2.KBN_GROUP_CD = 'G008'                                                " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "Z2.KBN_CD = '02'                                                        " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN Z3                                                      " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "Z3.KBN_GROUP_CD = 'S081'                                                " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "Z3.KBN_CD = '01'                                                        " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..M_DEST DEST                                                   " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "DEST.DEST_CD = OUTKAL.DEST_CD                                           " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..M_DEST DEST2                                                  " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "DEST2.NRS_BR_CD = OUTKAL.NRS_BR_CD                                      " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "DEST2.CUST_CD_L = OUTKAL.CUST_CD_L                                      " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "DEST2.DEST_CD = OUTKAL.SHIP_CD_L                                        " & vbNewLine

#End Region

#Region "NRC回収テーブルから長期未返却取得 SQL WHERE句"

    ''' <summary>
    ''' NRC回収テーブルから長期未返却取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_MIHEN As String = "WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                                   & "  AND CONVERT(VARCHAR,DATEADD(DAY, 60 ,OUTKAL.ARR_PLAN_DATE),112) <= @HOKOKU_DATE_TO " & vbNewLine _
                                                   & "  AND OUTKAL.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                                   & "--(2014.01.21)要望番号2144 追加START                                   " & vbNewLine _
                                                   & "  AND EXISTS (SELECT * FROM $LM_MST$..Z_KBN Z4                         " & vbNewLine _
                                                   & "  WHERE                                                                " & vbNewLine _
                                                   & "  Z4.KBN_GROUP_CD = 'F010'                                             " & vbNewLine _
                                                   & "  AND Z4.KBN_NM1 = @NRS_BR_CD                                          " & vbNewLine _
                                                   & "  AND Z4.KBN_NM2 = OUTKAL.CUST_CD_L)                                   " & vbNewLine _
                                                   & "--(2014.01.21)要望番号2144 追加END                                     " & vbNewLine

#End Region

#Region "NRC回収テーブルから長期未返却取得 SQL ORDER句"

    ''' <summary>
    ''' NRC回収テーブルから長期未返却取得 SQL ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_MIHEN As String = "ORDER BY                                                               " & vbNewLine _
                                                   & "  OUTKAL.CUST_CD_L                                                     " & vbNewLine _
                                                   & " ,OUTKAL.ARR_PLAN_DATE                                                 " & vbNewLine _
                                                   & " ,ISNULL(Z1.KBN_NM2, Z2.KBN_NM2)                                       " & vbNewLine _
                                                   & " ,CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                    " & vbNewLine _
                                                   & "       ELSE OUTKAL.DEST_NM                                             " & vbNewLine _
                                                   & "  END                                                                  " & vbNewLine _
                                                   & " ,Z3.KBN_NM2 + NRC.SERIAL_NO                                           " & vbNewLine

#End Region

#End Region

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

#Region "NRC回収テーブルから出荷実績取得"

    ''' <summary>
    ''' NRC回収テーブルから出荷実績取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExcelShukka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI890IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_SHUKKA)       'SQL構築(Select句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_FROM_SHUKKA)  'SQL構築(From句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_WHERE_SHUKKA) 'SQL構築(Where句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_ORDER_SHUKKA) 'SQL構築(Order句)

        'パラメータの設定
        Call SetSQLExcelParameter(inTbl.Rows(0), Me._SqlPrmList)           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI890DAC", "SelectExcelShukka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("TAIRYU", "TAIRYU")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("SHIP_NM", "SHIP_NM")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI890OUT_EXCEL_SHUKKA")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "NRC回収テーブルから返送実績取得"

    ''' <summary>
    ''' NRC回収テーブルから返送実績取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExcelHenso(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI890IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_HENSO)       'SQL構築(Select句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_FROM_HENSO)  'SQL構築(From句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_WHERE_HENSO) 'SQL構築(Where句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_ORDER_HENSO) 'SQL構築(Order句)

        'パラメータの設定
        Call SetSQLExcelParameter(inTbl.Rows(0), Me._SqlPrmList)           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI890DAC", "SelectExcelHenso", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("TAIRYU", "TAIRYU")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("SHIP_NM", "SHIP_NM")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI890OUT_EXCEL_HENSO")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "NRC回収テーブルから長期未返却取得"

    ''' <summary>
    ''' NRC回収テーブルから長期未返却取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExcelMihen(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI890IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_MIHEN)       'SQL構築(Select句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_FROM_MIHEN)  'SQL構築(From句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_WHERE_MIHEN) 'SQL構築(Where句)
        Me._StrSql.Append(LMI890DAC.SQL_SELECT_ORDER_MIHEN) 'SQL構築(Order句)

        'パラメータの設定
        Call SetSQLExcelParameter(inTbl.Rows(0), Me._SqlPrmList)           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI890DAC", "SelectExcelMihen", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("TAIRYU", "TAIRYU")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("SHIP_NM", "SHIP_NM")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI890OUT_EXCEL_MIHEN")

        reader.Close()

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 Excel出力情報取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLExcelParameter(ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE_FROM", .Item("HOKOKU_DATE_FROM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE_TO", .Item("HOKOKU_DATE_TO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

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
