' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD550    : 不動在庫リスト
'  作  成  者       :  [sagawa]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD550DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD550DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' 帳票種別取得用SQL（荷動きあり）
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_MPRT_IDO_ARI As String = String.Empty

    ''' <summary>
    ''' 帳票種別取得用SQL（荷動きなし）
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_MPRT_IDO_NASHI As String = String.Empty

    ''' <summary>
    ''' 印刷データ取得用SQL（荷動きあり）
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_SELECT_IDO_ARI As String = String.Empty

    ''' <summary>
    ''' 印刷データ取得用SQL（荷動きなし）
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_SELECT_IDO_NASHI As String = String.Empty

    ''' <summary>
    ''' SQL条件設定用(帳票種別取得時設定用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const sqlMprt As String = "0"

    ''' <summary>
    ''' SQL条件設定用(印刷データ取得時設定用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const sqlOut As String = "1"

    ''' <summary>
    ''' 在庫基準種別（入庫日基準）
    ''' </summary>
    ''' <remarks></remarks>
    Private DATE_FLG_INKO_DATE As String = "01"

    ''' <summary>
    ''' 在庫基準種別（出荷日基準）
    ''' </summary>
    ''' <remarks></remarks>
    Private DATE_FLG_OUTKA_DATE As String = "02"

    ''' <summary>
    ''' 在庫基準種別（入庫日、出荷日両方）
    ''' </summary>
    ''' <remarks></remarks>
    Private DATE_FLG_INOUT_DATE As String = "03"

    ''' <summary>
    ''' 荷動き・単位種別（荷動きあり）
    ''' </summary>
    ''' <remarks></remarks>
    Private IDO_ARI As String = "01"

    ''' <summary>
    ''' 荷動き・単位種別（荷動きなし、商品単位）
    ''' </summary>
    ''' <remarks></remarks>
    Private IDO_NASHI_GOODS As String = "02"

    ''' <summary>
    ''' 荷動き・単位種別（荷動きあり、商品・LOT単位）
    ''' </summary>
    ''' <remarks></remarks>
    Private IDO_NASHI_GOODS_LOT As String = "03"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用SQL作成（荷動きあり）
    ''' </summary>
    ''' <param name="dateFrom"></param>
    ''' <param name="dateTo"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlMprtIdoAri(ByVal dateFlg As String, ByVal dateFrom As String, ByVal dateTo As String)

        SQL_MPRT_IDO_ARI = "SELECT DISTINCT                                                              " & vbNewLine _
                 & " MAIN.NRS_BR_CD                                       AS NRS_BR_CD           " & vbNewLine _
                 & ",'30'                                                 AS PTN_ID              " & vbNewLine _
                 & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                            " & vbNewLine _
                 & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                            " & vbNewLine _
                 & "      ELSE MR3.PTN_CD                                                        " & vbNewLine _
                 & " END                                                  AS PTN_CD              " & vbNewLine _
                 & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                            " & vbNewLine _
                 & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                            " & vbNewLine _
                 & "      ELSE MR3.RPT_ID                                                        " & vbNewLine _
                 & " END                                                  AS RPT_ID              " & vbNewLine _
                 & "FROM                                                                         " & vbNewLine _
                 & "        (SELECT                                                              " & vbNewLine _
                 & "          ZAI.NRS_BR_CD                       AS NRS_BR_CD                   " & vbNewLine _
                 & "         ,ZAI.CUST_CD_L                       AS CUST_CD_L                   " & vbNewLine _
                 & "         ,ZAI.CUST_CD_M                       AS CUST_CD_M                   " & vbNewLine _
                 & "         ,ZAI.INKO_DATE                       AS INKO_DATE                   " & vbNewLine _
                 & "         ,ZAI.GOODS_CD_NRS                    AS GOODS_CD_NRS                " & vbNewLine _
                 & "         ,ISNULL(MAX(OUTL.OUTKO_DATE), '')    AS LAST_OUTKO_DATE             " & vbNewLine _
                 & "         FROM                                                                " & vbNewLine _
                 & "         --在庫データ                                                        " & vbNewLine _
                 & "         $LM_TRN$..D_ZAI_TRS ZAI                                             " & vbNewLine _
                 & "         --入荷データL                                                       " & vbNewLine _
                 & "         LEFT JOIN $LM_TRN$..B_INKA_L INL                                    " & vbNewLine _
                 & "         ON  INL.NRS_BR_CD = ZAI.NRS_BR_CD                                   " & vbNewLine _
                 & "         AND INL.INKA_NO_L = ZAI.INKA_NO_L                                   " & vbNewLine _
                 & "         AND INL.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                 & "         --出荷データS                                                       " & vbNewLine _
                 & "         LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                  " & vbNewLine _
                 & "         ON  OUTS.NRS_BR_CD = ZAI.NRS_BR_CD                                  " & vbNewLine _
                 & "         AND OUTS.ZAI_REC_NO = ZAI.ZAI_REC_NO                                " & vbNewLine _
                 & "         AND OUTS.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                 & "         --出荷データL                                                       " & vbNewLine _
                 & "         LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL                                  " & vbNewLine _
                 & "         ON  OUTL.NRS_BR_CD = OUTS.NRS_BR_CD                                 " & vbNewLine _
                 & "         AND OUTL.OUTKA_NO_L = OUTS.OUTKA_NO_L                               " & vbNewLine _
                 & "         AND OUTL.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                 & "         WHERE                                                               " & vbNewLine _
                 & "             ZAI.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                 & "         AND INL.INKA_STATE_KB >= '50'                                       " & vbNewLine _
                 & "         AND ZAI.CUST_CD_L = @CUST_CD_L                                      " & vbNewLine _
                 & "         AND ZAI.CUST_CD_M = @CUST_CD_M                                      " & vbNewLine _
                 & "         AND ZAI.PORA_ZAI_NB > 0                                             " & vbNewLine _
                 & "         AND ZAI.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                 & "         GROUP BY                                                            " & vbNewLine _
                 & "          ZAI.NRS_BR_CD                                                      " & vbNewLine _
                 & "         ,ZAI.CUST_CD_L                                                      " & vbNewLine _
                 & "         ,ZAI.CUST_CD_M                                                      " & vbNewLine _
                 & "         ,ZAI.INKO_DATE                                                      " & vbNewLine _
                 & "         ,ZAI.GOODS_CD_NRS                                                   " & vbNewLine _
                 & "         ) MAIN                                                              " & vbNewLine _
                 & "--商品マスタ                                                                 " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_GOODS MGOODS                                           " & vbNewLine _
                 & "ON  MGOODS.NRS_BR_CD = MAIN.NRS_BR_CD                                        " & vbNewLine _
                 & "AND MGOODS.GOODS_CD_NRS = MAIN.GOODS_CD_NRS                                  " & vbNewLine _
                 & "--荷主帳票パターン取得                                                       " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                          " & vbNewLine _
                 & "ON  MAIN.NRS_BR_CD = MCR1.NRS_BR_CD                                          " & vbNewLine _
                 & "AND MAIN.CUST_CD_L = MCR1.CUST_CD_L                                          " & vbNewLine _
                 & "AND MAIN.CUST_CD_M = MCR1.CUST_CD_M                                          " & vbNewLine _
                 & "AND '00' = MCR1.CUST_CD_S                                                    " & vbNewLine _
                 & "AND MCR1.PTN_ID = '30'                                                       " & vbNewLine _
                 & "--帳票パターン取得                                                           " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR1                                                " & vbNewLine _
                 & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                           " & vbNewLine _
                 & "AND MR1.PTN_ID = MCR1.PTN_ID                                                 " & vbNewLine _
                 & "AND MR1.PTN_CD = MCR1.PTN_CD                                                 " & vbNewLine _
                 & "AND MR1.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                 & "--商品Mの荷主での荷主帳票パターン取得                                        " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                          " & vbNewLine _
                 & "ON  MGOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                        " & vbNewLine _
                 & "AND MGOODS.CUST_CD_L = MCR2.CUST_CD_L                                        " & vbNewLine _
                 & "AND MGOODS.CUST_CD_M = MCR2.CUST_CD_M                                        " & vbNewLine _
                 & "AND MGOODS.CUST_CD_S = MCR2.CUST_CD_S                                        " & vbNewLine _
                 & "AND MCR2.PTN_ID = '30'                                                       " & vbNewLine _
                 & "--帳票パターン取得                                                           " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR2                                                " & vbNewLine _
                 & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                 & "AND MR2.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
                 & "AND MR2.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
                 & "AND MR2.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                 & "--存在しない場合の帳票パターン取得                                           " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR3                                                " & vbNewLine _
                 & "ON  MR3.NRS_BR_CD = MAIN.NRS_BR_CD                                           " & vbNewLine _
                 & "AND MR3.PTN_ID = '30'                                                        " & vbNewLine _
                 & "AND MR3.STANDARD_FLAG = '01'                                                 " & vbNewLine _
                 & "AND MR3.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                 & "WHERE                                                                        " & vbNewLine
        If dateFlg = DATE_FLG_INKO_DATE Then '入庫日基準
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    INKO_DATE <> ''                                                          " & vbNewLine _
                 & "AND INKO_DATE >= @DATE_FROM                                                  " & vbNewLine _
                 & "AND INKO_DATE <= @DATE_TO                                                    " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    INKO_DATE <> ''                                                          " & vbNewLine _
                 & "AND INKO_DATE >= @DATE_FROM                                                  " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    INKO_DATE <> ''                                                          " & vbNewLine _
                 & "AND INKO_DATE <= @DATE_TO                                                    " & vbNewLine
            Else
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    INKO_DATE <> ''                                                          " & vbNewLine
            End If
        ElseIf dateFlg = DATE_FLG_OUTKA_DATE Then '出荷日基準
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    LAST_OUTKO_DATE <> ''                                                    " & vbNewLine _
                 & "AND LAST_OUTKO_DATE >= @DATE_FROM                                            " & vbNewLine _
                 & "AND LAST_OUTKO_DATE <= @DATE_TO                                              " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    LAST_OUTKO_DATE <> ''                                                    " & vbNewLine _
                 & "AND LAST_OUTKO_DATE >= @DATE_FROM                                            " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    LAST_OUTKO_DATE <> ''                                                    " & vbNewLine _
                 & "AND LAST_OUTKO_DATE <= @DATE_TO                                              " & vbNewLine
            Else
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    LAST_OUTKO_DATE <> ''                                                    " & vbNewLine
            End If
        ElseIf dateFlg = DATE_FLG_INOUT_DATE Then '入庫日、出荷日両方
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "   (INKO_DATE <> ''                                                          " & vbNewLine _
                 & "AND INKO_DATE >= @DATE_FROM                                                  " & vbNewLine _
                 & "AND INKO_DATE <= @DATE_TO)                                                   " & vbNewLine _
                 & "OR (LAST_OUTKO_DATE <> ''                                                    " & vbNewLine _
                 & "AND LAST_OUTKO_DATE >= @DATE_FROM                                            " & vbNewLine _
                 & "AND LAST_OUTKO_DATE <= @DATE_TO)                                             " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "   (INKO_DATE <> ''                                                          " & vbNewLine _
                 & "AND INKO_DATE >= @DATE_FROM)                                                 " & vbNewLine _
                 & "OR (LAST_OUTKO_DATE <> ''                                                    " & vbNewLine _
                 & "AND LAST_OUTKO_DATE >= @DATE_FROM)                                           " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "   (INKO_DATE <> ''                                                          " & vbNewLine _
                 & "AND INKO_DATE <= @DATE_TO)                                                   " & vbNewLine _
                 & "OR (LAST_OUTKO_DATE <> ''                                                    " & vbNewLine _
                 & "AND LAST_OUTKO_DATE <= @DATE_TO)                                             " & vbNewLine
            Else
                SQL_MPRT_IDO_ARI = SQL_MPRT_IDO_ARI & vbNewLine _
                 & "    INKO_DATE <> ''                                                          " & vbNewLine _
                 & "OR  LAST_OUTKO_DATE <> ''                                                    " & vbNewLine
            End If
        Else
            '条件なし
        End If

    End Sub


    ''' <summary>
    ''' 帳票種別取得用SQL作成（荷動きなし）
    ''' </summary>
    ''' <param name="dateFrom"></param>
    ''' <param name="dateTo"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlMprtIdoNashi(ByVal dateFlg As String, ByVal dateFrom As String, ByVal dateTo As String)

        SQL_MPRT_IDO_NASHI = "SELECT DISTINCT                                                                                  " & vbNewLine _
                           & " @NRS_BR_CD                                          AS NRS_BR_CD                                " & vbNewLine _
                           & ",'30'                                                AS PTN_ID                                   " & vbNewLine _
                           & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                                                " & vbNewLine _
                           & "WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                                                      " & vbNewLine _
                           & "ELSE MR3.PTN_CD                                                                                  " & vbNewLine _
                           & "END                                                  AS PTN_CD                                   " & vbNewLine _
                           & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                " & vbNewLine _
                           & "WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                      " & vbNewLine _
                           & "ELSE MR3.RPT_ID                                                                                  " & vbNewLine _
                           & "END                                                  AS RPT_ID                                   " & vbNewLine _
                           & "FROM                                                                                             " & vbNewLine _
                           & "        (SELECT                                                                                  " & vbNewLine _
                           & "          MAIN2.GOODS_CD_NRS                                                                     " & vbNewLine _
                           & "         FROM                                                                                    " & vbNewLine _
                           & "                (SELECT                                                                          " & vbNewLine _
                           & "                  MAIN1.GOODS_CD_NRS                                                             " & vbNewLine _
                           & "                 ,MAX(CASE WHEN (                                                                " & vbNewLine
        If dateFlg = DATE_FLG_INKO_DATE Then '入庫日基準
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE >= @DATE_FROM                                                                      " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE >= @DATE_FROM                                                                      " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine
            Else
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine
            End If
        ElseIf dateFlg = DATE_FLG_OUTKA_DATE Then '出荷日基準
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine _
                           & "AND LAST_OUTKO_DATE <= @DATE_TO                                                                  " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE <= @DATE_TO                                                                  " & vbNewLine
            Else
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine
            End If
        ElseIf dateFlg = DATE_FLG_INOUT_DATE Then '入庫日、出荷日両方
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "   (INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE >= @DATE_FROM)                                                                     " & vbNewLine _
                           & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine _
                           & "AND LAST_OUTKO_DATE <= @DATE_TO)                                                                 " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "   (INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE >= @DATE_FROM)                                                                     " & vbNewLine _
                           & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE >= @DATE_FROM)                                                               " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "   (INKO_DATE <> '')                                                                             " & vbNewLine _
                           & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE <= @DATE_TO)                                                                 " & vbNewLine
            Else
                SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "OR  LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine
            End If
        Else
            '条件なし
        End If
        SQL_MPRT_IDO_NASHI = SQL_MPRT_IDO_NASHI & vbNewLine _
                           & "                         ) THEN 1 ELSE 0                                                         " & vbNewLine _
                           & "                      END) AS CNT                                                                " & vbNewLine _
                           & "                 FROM                                                                            " & vbNewLine _
                           & "                        (SELECT                                                                  " & vbNewLine _
                           & "                          ZAI.NRS_BR_CD                       AS NRS_BR_CD                       " & vbNewLine _
                           & "                         ,ZAI.ZAI_REC_NO                      AS ZAI_REC_NO                      " & vbNewLine _
                           & "                         ,ZAI.CUST_CD_L                       AS CUST_CD_L                       " & vbNewLine _
                           & "                         ,ZAI.CUST_CD_M                       AS CUST_CD_M                       " & vbNewLine _
                           & "                         ,ZAI.INKO_DATE                       AS INKO_DATE                       " & vbNewLine _
                           & "                         ,ZAI.GOODS_CD_NRS                    AS GOODS_CD_NRS                    " & vbNewLine _
                           & "                         ,ZAI.INKA_NO_L                       AS INKA_NO_L                       " & vbNewLine _
                           & "                         ,ZAI.LOT_NO                          AS LOT_NO                          " & vbNewLine _
                           & "                         ,ISNULL(MAX(OUTL.OUTKO_DATE), '')    AS LAST_OUTKO_DATE                 " & vbNewLine _
                           & "                         FROM                                                                    " & vbNewLine _
                           & "                         $LM_TRN$..D_ZAI_TRS ZAI                                                 " & vbNewLine _
                           & "                         LEFT JOIN $LM_TRN$..B_INKA_L INL                                        " & vbNewLine _
                           & "                         ON  INL.NRS_BR_CD = ZAI.NRS_BR_CD                                       " & vbNewLine _
                           & "                         AND INL.INKA_NO_L = ZAI.INKA_NO_L                                       " & vbNewLine _
                           & "                         AND INL.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                           & "                         LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                      " & vbNewLine _
                           & "                         ON  OUTS.NRS_BR_CD = ZAI.NRS_BR_CD                                      " & vbNewLine _
                           & "                         AND OUTS.ZAI_REC_NO = ZAI.ZAI_REC_NO                                    " & vbNewLine _
                           & "                         AND OUTS.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                           & "                         LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL                                      " & vbNewLine _
                           & "                         ON  OUTL.NRS_BR_CD = OUTS.NRS_BR_CD                                     " & vbNewLine _
                           & "                         AND OUTL.OUTKA_NO_L = OUTS.OUTKA_NO_L                                   " & vbNewLine _
                           & "                         AND OUTL.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                           & "                         WHERE                                                                   " & vbNewLine _
                           & "                         ZAI.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
                           & "                         AND                                                                     " & vbNewLine _
                           & "                         INL.INKA_STATE_KB >= '50'                                               " & vbNewLine _
                           & "                         AND                                                                     " & vbNewLine _
                           & "                         ZAI.CUST_CD_L = @CUST_CD_L                                              " & vbNewLine _
                           & "                         AND                                                                     " & vbNewLine _
                           & "                         ZAI.CUST_CD_M = @CUST_CD_M                                              " & vbNewLine _
                           & "                         AND                                                                     " & vbNewLine _
                           & "                         ZAI.PORA_ZAI_NB > 0                                                     " & vbNewLine _
                           & "                         AND                                                                     " & vbNewLine _
                           & "                         ZAI.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                           & "                         GROUP BY                                                                " & vbNewLine _
                           & "                          ZAI.NRS_BR_CD                                                          " & vbNewLine _
                           & "                         ,ZAI.ZAI_REC_NO                                                         " & vbNewLine _
                           & "                         ,ZAI.CUST_CD_L                                                          " & vbNewLine _
                           & "                         ,ZAI.CUST_CD_M                                                          " & vbNewLine _
                           & "                         ,ZAI.INKO_DATE                                                          " & vbNewLine _
                           & "                         ,ZAI.GOODS_CD_NRS                                                       " & vbNewLine _
                           & "                         ,ZAI.INKA_NO_L                                                          " & vbNewLine _
                           & "                         ,ZAI.LOT_NO                                                             " & vbNewLine _
                           & "                        ) MAIN1                                                                  " & vbNewLine _
                           & "                 GROUP BY                                                                        " & vbNewLine _
                           & "                  MAIN1.GOODS_CD_NRS                                                             " & vbNewLine _
                           & "                ) MAIN2                                                                          " & vbNewLine _
                           & "         WHERE                                                                                   " & vbNewLine _
                           & "         MAIN2.CNT = 0                                                                           " & vbNewLine _
                           & "        ) MAIN3                                                                                  " & vbNewLine _
                           & "--商品マスタ                                                                                     " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_GOODS MGOODS                                                               " & vbNewLine _
                           & "ON  MGOODS.NRS_BR_CD = @NRS_BR_CD                                                                " & vbNewLine _
                           & "AND MGOODS.GOODS_CD_NRS = MAIN3.GOODS_CD_NRS                                                     " & vbNewLine _
                           & "--荷主マスタ                                                                                     " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_CUST MCUST                                                                 " & vbNewLine _
                           & "ON  MCUST.NRS_BR_CD = MGOODS.NRS_BR_CD                                                           " & vbNewLine _
                           & "AND MCUST.CUST_CD_L = MGOODS.CUST_CD_L                                                           " & vbNewLine _
                           & "AND MCUST.CUST_CD_M = MGOODS.CUST_CD_M                                                           " & vbNewLine _
                           & "AND MCUST.CUST_CD_S = MGOODS.CUST_CD_S                                                           " & vbNewLine _
                           & "AND MCUST.CUST_CD_SS = MGOODS.CUST_CD_SS                                                         " & vbNewLine _
                           & "--荷主帳票パターン取得                                                                           " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                              " & vbNewLine _
                           & "ON  MGOODS.NRS_BR_CD = MCR1.NRS_BR_CD                                                            " & vbNewLine _
                           & "AND @CUST_CD_L = MCR1.CUST_CD_L                                                                  " & vbNewLine _
                           & "AND @CUST_CD_M = MCR1.CUST_CD_M                                                                  " & vbNewLine _
                           & "AND '00' = MCR1.CUST_CD_S                                                                        " & vbNewLine _
                           & "AND MCR1.PTN_ID = '30'                                                                           " & vbNewLine _
                           & "--帳票パターン取得                                                                               " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                    " & vbNewLine _
                           & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                               " & vbNewLine _
                           & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                     " & vbNewLine _
                           & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                     " & vbNewLine _
                           & "AND MR1.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                           & "--商品Mの荷主での荷主帳票パターン取得                                                            " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                              " & vbNewLine _
                           & "ON  MGOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                                            " & vbNewLine _
                           & "AND MGOODS.CUST_CD_L = MCR2.CUST_CD_L                                                            " & vbNewLine _
                           & "AND MGOODS.CUST_CD_M = MCR2.CUST_CD_M                                                            " & vbNewLine _
                           & "AND MGOODS.CUST_CD_S = MCR2.CUST_CD_S                                                            " & vbNewLine _
                           & "AND MCR2.PTN_ID = '30'                                                                           " & vbNewLine _
                           & "--帳票パターン取得                                                                               " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                    " & vbNewLine _
                           & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                               " & vbNewLine _
                           & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                     " & vbNewLine _
                           & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                     " & vbNewLine _
                           & "AND MR2.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                           & "--存在しない場合の帳票パターン取得                                                               " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                    " & vbNewLine _
                           & "ON  MR3.NRS_BR_CD = MGOODS.NRS_BR_CD                                                             " & vbNewLine _
                           & "AND MR3.PTN_ID = '30'                                                                            " & vbNewLine _
                           & "AND MR3.STANDARD_FLAG = '01'                                                                     " & vbNewLine _
                           & "AND MR3.SYS_DEL_FLG = '0'                                                                        " & vbNewLine

    End Sub



    ''' <summary>
    ''' 印刷データ取得用SQL作成（荷動きあり）
    ''' </summary>
    ''' <param name="dateFrom"></param>
    ''' <param name="dateTo"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlSelectIdoAri(ByVal dateFlg As String, ByVal dateFrom As String, ByVal dateTo As String)

        SQL_SELECT_IDO_ARI = "SELECT                                                                                           " & vbNewLine _
                           & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                " & vbNewLine _
                           & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                " & vbNewLine _
                           & "      ELSE MR3.RPT_ID                                                                            " & vbNewLine _
                           & " END                       AS RPT_ID                                                             " & vbNewLine _
                           & ",MAIN.NRS_BR_CD            AS NRS_BR_CD                                                          " & vbNewLine _
                           & ",MBR.NRS_BR_NM             AS NRS_BR_NM                                                          " & vbNewLine _
                           & ",@DATE_FLG                 AS DATE_FLG                                                           " & vbNewLine _
                           & ",@IDO_TANI_FLG             AS IDO_TANI_FLG                                                       " & vbNewLine _
                           & ",MAIN.CUST_CD_L            AS CUST_CD_L                                                          " & vbNewLine _
                           & ",MAIN.CUST_CD_M            AS CUST_CD_M                                                          " & vbNewLine _
                           & ",MCUST.CUST_NM_L           AS CUST_NM_L                                                          " & vbNewLine _
                           & ",MCUST.CUST_NM_M           AS CUST_NM_M                                                          " & vbNewLine _
                           & ",@DATE_FROM                AS DATE_FROM                                                          " & vbNewLine _
                           & ",@DATE_TO                  AS DATE_TO                                                            " & vbNewLine _
                           & ",MAIN.INKO_DATE            AS INKO_DATE                                                          " & vbNewLine _
                           & ",MAIN.GOODS_CD_NRS         AS GOODS_CD_NRS                                                       " & vbNewLine _
                           & ",MGOODS.GOODS_CD_CUST      AS GOODS_CD_CUST                                                      " & vbNewLine _
                           & ",MGOODS.GOODS_NM_1         AS GOODS_NM                                                           " & vbNewLine _
                           & ",MAIN.INKA_NO_L            AS INKA_NO_L                                                          " & vbNewLine _
                           & ",MAIN.LOT_NO               AS LOT_NO                                                             " & vbNewLine _
                           & ",MAIN.PORA_ZAI_NB          AS PORA_ZAI_NB                                                        " & vbNewLine _
                           & ",MGOODS.NB_UT              AS NB_UT                                                              " & vbNewLine _
                           & ",MAIN.PORA_ZAI_QT          AS PORA_ZAI_QT                                                        " & vbNewLine _
                           & ",MGOODS.STD_IRIME_UT       AS IRIME_UT                                                           " & vbNewLine _
                           & ",MAIN.LAST_OUTKO_DATE      AS LAST_OUTKO_DATE                                                    " & vbNewLine _
                           & "FROM                                                                                             " & vbNewLine _
                           & "        (SELECT                                                                                  " & vbNewLine _
                           & "          ZAI.NRS_BR_CD                       AS NRS_BR_CD                                       " & vbNewLine _
                           & "         ,ZAI.CUST_CD_L                       AS CUST_CD_L                                       " & vbNewLine _
                           & "         ,ZAI.CUST_CD_M                       AS CUST_CD_M                                       " & vbNewLine _
                           & "         ,ZAI.INKO_DATE                       AS INKO_DATE                                       " & vbNewLine _
                           & "         ,ZAI.GOODS_CD_NRS                    AS GOODS_CD_NRS                                    " & vbNewLine _
                           & "         ,ZAI.INKA_NO_L                       AS INKA_NO_L                                       " & vbNewLine _
                           & "         ,ZAI.LOT_NO                          AS LOT_NO                                          " & vbNewLine _
                           & "         ,ZAI.PORA_ZAI_NB                     AS PORA_ZAI_NB                                     " & vbNewLine _
                           & "         ,ZAI.PORA_ZAI_QT                     AS PORA_ZAI_QT                                     " & vbNewLine _
                           & "         ,ISNULL(MAX(OUTL.OUTKO_DATE), '')    AS LAST_OUTKO_DATE                                 " & vbNewLine _
                           & "         FROM                                                                                    " & vbNewLine _
                           & "         --在庫データ                                                                            " & vbNewLine _
                           & "         $LM_TRN$..D_ZAI_TRS ZAI                                                                 " & vbNewLine _
                           & "         --入荷データL                                                                           " & vbNewLine _
                           & "         LEFT JOIN $LM_TRN$..B_INKA_L INL                                                        " & vbNewLine _
                           & "         ON  INL.NRS_BR_CD = ZAI.NRS_BR_CD                                                       " & vbNewLine _
                           & "         AND INL.INKA_NO_L = ZAI.INKA_NO_L                                                       " & vbNewLine _
                           & "         AND INL.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                           & "         --出荷データS                                                                           " & vbNewLine _
                           & "         LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                      " & vbNewLine _
                           & "         ON  OUTS.NRS_BR_CD = ZAI.NRS_BR_CD                                                      " & vbNewLine _
                           & "         AND OUTS.ZAI_REC_NO = ZAI.ZAI_REC_NO                                                    " & vbNewLine _
                           & "         AND OUTS.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                           & "         --出荷データL                                                                           " & vbNewLine _
                           & "         LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL                                                      " & vbNewLine _
                           & "         ON  OUTL.NRS_BR_CD = OUTS.NRS_BR_CD                                                     " & vbNewLine _
                           & "         AND OUTL.OUTKA_NO_L = OUTS.OUTKA_NO_L                                                   " & vbNewLine _
                           & "         AND OUTL.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                           & "         WHERE                                                                                   " & vbNewLine _
                           & "             ZAI.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
                           & "         AND INL.INKA_STATE_KB >= '50'                                                           " & vbNewLine _
                           & "         AND ZAI.CUST_CD_L = @CUST_CD_L                                                          " & vbNewLine _
                           & "         AND ZAI.CUST_CD_M = @CUST_CD_M                                                          " & vbNewLine _
                           & "         AND ZAI.PORA_ZAI_NB > 0                                                                 " & vbNewLine _
                           & "         AND ZAI.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                           & "         GROUP BY                                                                                " & vbNewLine _
                           & "          ZAI.NRS_BR_CD                                                                          " & vbNewLine _
                           & "         ,ZAI.CUST_CD_L                                                                          " & vbNewLine _
                           & "         ,ZAI.CUST_CD_M                                                                          " & vbNewLine _
                           & "         ,ZAI.INKO_DATE                                                                          " & vbNewLine _
                           & "         ,ZAI.GOODS_CD_NRS                                                                       " & vbNewLine _
                           & "         ,ZAI.INKA_NO_L                                                                          " & vbNewLine _
                           & "         ,ZAI.LOT_NO                                                                             " & vbNewLine _
                           & "         ,ZAI.PORA_ZAI_NB                                                                        " & vbNewLine _
                           & "         ,ZAI.PORA_ZAI_QT                                                                        " & vbNewLine _
                           & "         ) MAIN                                                                                  " & vbNewLine _
                           & "--営業所マスタ                                                                                   " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_NRS_BR MBR                                                                 " & vbNewLine _
                           & "ON  MBR.NRS_BR_CD = MAIN.NRS_BR_CD                                                               " & vbNewLine _
                           & "--商品マスタ                                                                                     " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_GOODS MGOODS                                                               " & vbNewLine _
                           & "ON  MGOODS.NRS_BR_CD = MAIN.NRS_BR_CD                                                            " & vbNewLine _
                           & "AND MGOODS.GOODS_CD_NRS = MAIN.GOODS_CD_NRS                                                      " & vbNewLine _
                           & "--荷主マスタ                                                                                     " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_CUST MCUST                                                                 " & vbNewLine _
                           & "ON  MCUST.NRS_BR_CD = MGOODS.NRS_BR_CD                                                           " & vbNewLine _
                           & "AND MCUST.CUST_CD_L = MGOODS.CUST_CD_L                                                           " & vbNewLine _
                           & "AND MCUST.CUST_CD_M = MGOODS.CUST_CD_M                                                           " & vbNewLine _
                           & "AND MCUST.CUST_CD_S = MGOODS.CUST_CD_S                                                           " & vbNewLine _
                           & "AND MCUST.CUST_CD_SS = MGOODS.CUST_CD_SS                                                         " & vbNewLine _
                           & "--荷主帳票パターン取得                                                                           " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                              " & vbNewLine _
                           & "ON  MAIN.NRS_BR_CD = MCR1.NRS_BR_CD                                                              " & vbNewLine _
                           & "AND MAIN.CUST_CD_L = MCR1.CUST_CD_L                                                              " & vbNewLine _
                           & "AND MAIN.CUST_CD_M = MCR1.CUST_CD_M                                                              " & vbNewLine _
                           & "AND '00' = MCR1.CUST_CD_S                                                                        " & vbNewLine _
                           & "AND MCR1.PTN_ID = '30'                                                                           " & vbNewLine _
                           & "--帳票パターン取得                                                                               " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                    " & vbNewLine _
                           & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                               " & vbNewLine _
                           & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                     " & vbNewLine _
                           & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                     " & vbNewLine _
                           & "AND MR1.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                           & "--商品Mの荷主での荷主帳票パターン取得                                                            " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                              " & vbNewLine _
                           & "ON  MGOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                                            " & vbNewLine _
                           & "AND MGOODS.CUST_CD_L = MCR2.CUST_CD_L                                                            " & vbNewLine _
                           & "AND MGOODS.CUST_CD_M = MCR2.CUST_CD_M                                                            " & vbNewLine _
                           & "AND MGOODS.CUST_CD_S = MCR2.CUST_CD_S                                                            " & vbNewLine _
                           & "AND MCR2.PTN_ID = '30'                                                                           " & vbNewLine _
                           & "--帳票パターン取得                                                                               " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                    " & vbNewLine _
                           & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                               " & vbNewLine _
                           & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                     " & vbNewLine _
                           & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                     " & vbNewLine _
                           & "AND MR2.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                           & "--存在しない場合の帳票パターン取得                                                               " & vbNewLine _
                           & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                    " & vbNewLine _
                           & "ON  MR3.NRS_BR_CD = MAIN.NRS_BR_CD                                                               " & vbNewLine _
                           & "AND MR3.PTN_ID = '30'                                                                            " & vbNewLine _
                           & "AND MR3.STANDARD_FLAG = '01'                                                                     " & vbNewLine _
                           & "AND MR3.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                           & "WHERE                                                                                            " & vbNewLine
        If dateFlg = DATE_FLG_INKO_DATE Then '入庫日基準
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE >= @DATE_FROM                                                                      " & vbNewLine _
                           & "AND INKO_DATE <= @DATE_TO                                                                        " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE >= @DATE_FROM                                                                      " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE <= @DATE_TO                                                                        " & vbNewLine
            Else
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine
            End If
        ElseIf dateFlg = DATE_FLG_OUTKA_DATE Then '出荷日基準
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine _
                           & "AND LAST_OUTKO_DATE <= @DATE_TO                                                                  " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE <= @DATE_TO                                                                  " & vbNewLine
            Else
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine
            End If
        ElseIf dateFlg = DATE_FLG_INOUT_DATE Then '入庫日、出荷日両方
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "   (INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE >= @DATE_FROM                                                                      " & vbNewLine _
                           & "AND INKO_DATE <= @DATE_TO)                                                                       " & vbNewLine _
                           & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine _
                           & "AND LAST_OUTKO_DATE <= @DATE_TO)                                                                 " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "   (INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE >= @DATE_FROM)                                                                     " & vbNewLine _
                           & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE >= @DATE_FROM)                                                               " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "   (INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "AND INKO_DATE <= @DATE_TO)                                                                       " & vbNewLine _
                           & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                           & "AND LAST_OUTKO_DATE <= @DATE_TO)                                                                 " & vbNewLine
            Else
                SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                           & "OR  LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine
            End If
        Else
            '条件なし
        End If
        SQL_SELECT_IDO_ARI = SQL_SELECT_IDO_ARI & vbNewLine _
                           & "ORDER BY                                                                                         " & vbNewLine _
                           & " MAIN.INKO_DATE                                                                                  " & vbNewLine _
                           & ",MGOODS.GOODS_NM_1                                                                               " & vbNewLine

    End Sub


    ''' <summary>
    ''' 印刷データ取得用SQL作成（荷動きなし）
    ''' </summary>
    ''' <param name="dateFrom"></param>
    ''' <param name="dateTo"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlSelectIdoNashi(ByVal idoTaniFlg As String, ByVal dateFlg As String, ByVal dateFrom As String, ByVal dateTo As String)

        SQL_SELECT_IDO_NASHI = "SELECT                                                                                           " & vbNewLine _
                             & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                " & vbNewLine _
                             & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                " & vbNewLine _
                             & "      ELSE MR3.RPT_ID                                                                            " & vbNewLine _
                             & " END                       AS RPT_ID                                                             " & vbNewLine _
                             & ",MAIN.NRS_BR_CD            AS NRS_BR_CD                                                          " & vbNewLine _
                             & ",MBR.NRS_BR_NM             AS NRS_BR_NM                                                          " & vbNewLine _
                             & ",@DATE_FLG                 AS DATE_FLG                                                           " & vbNewLine _
                             & ",@IDO_TANI_FLG             AS IDO_TANI_FLG                                                       " & vbNewLine _
                             & ",MAIN.CUST_CD_L            AS CUST_CD_L                                                          " & vbNewLine _
                             & ",MAIN.CUST_CD_M            AS CUST_CD_M                                                          " & vbNewLine _
                             & ",MCUST.CUST_NM_L           AS CUST_NM_L                                                          " & vbNewLine _
                             & ",MCUST.CUST_NM_M           AS CUST_NM_M                                                          " & vbNewLine _
                             & ",@DATE_FROM                AS DATE_FROM                                                          " & vbNewLine _
                             & ",@DATE_TO                  AS DATE_TO                                                            " & vbNewLine _
                             & ",MAIN.INKO_DATE            AS INKO_DATE                                                          " & vbNewLine _
                             & ",MAIN.GOODS_CD_NRS         AS GOODS_CD_NRS                                                       " & vbNewLine _
                             & ",MGOODS.GOODS_CD_CUST      AS GOODS_CD_CUST                                                      " & vbNewLine _
                             & ",MGOODS.GOODS_NM_1         AS GOODS_NM                                                           " & vbNewLine _
                             & ",MAIN.INKA_NO_L            AS INKA_NO_L                                                          " & vbNewLine _
                             & ",MAIN.LOT_NO               AS LOT_NO                                                             " & vbNewLine _
                             & ",MAIN.PORA_ZAI_NB          AS PORA_ZAI_NB                                                        " & vbNewLine _
                             & ",MGOODS.NB_UT              AS NB_UT                                                              " & vbNewLine _
                             & ",MAIN.PORA_ZAI_QT          AS PORA_ZAI_QT                                                        " & vbNewLine _
                             & ",MGOODS.STD_IRIME_UT       AS IRIME_UT                                                           " & vbNewLine _
                             & ",MAIN.LAST_OUTKO_DATE      AS LAST_OUTKO_DATE                                                    " & vbNewLine _
                             & "FROM                                                                                             " & vbNewLine _
                             & "        (SELECT                                                                                  " & vbNewLine _
                             & "          ZAI.NRS_BR_CD                      AS NRS_BR_CD                                        " & vbNewLine _
                             & "         ,ZAI.ZAI_REC_NO                     AS ZAI_REC_NO                                       " & vbNewLine _
                             & "         ,ZAI.CUST_CD_L                      AS CUST_CD_L                                        " & vbNewLine _
                             & "         ,ZAI.CUST_CD_M                      AS CUST_CD_M                                        " & vbNewLine _
                             & "         ,ZAI.INKO_DATE                      AS INKO_DATE                                        " & vbNewLine _
                             & "         ,ZAI.GOODS_CD_NRS                   AS GOODS_CD_NRS                                     " & vbNewLine _
                             & "         ,ZAI.INKA_NO_L                      AS INKA_NO_L                                        " & vbNewLine _
                             & "         ,ZAI.LOT_NO                         AS LOT_NO                                           " & vbNewLine _
                             & "         ,ZAI.PORA_ZAI_NB                    AS PORA_ZAI_NB                                      " & vbNewLine _
                             & "         ,ZAI.PORA_ZAI_QT                    AS PORA_ZAI_QT                                      " & vbNewLine _
                             & "         ,ISNULL(MAX(OUTL.OUTKO_DATE), '')   AS LAST_OUTKO_DATE                                  " & vbNewLine _
                             & "         FROM                                                                                    " & vbNewLine _
                             & "                (SELECT                                                                          " & vbNewLine

        '荷動き単位設定
        If idoTaniFlg = IDO_NASHI_GOODS Then  '商品単位
            SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                  GOODS2.GOODS_CD_NRS                                                            " & vbNewLine
        Else                                  '商品・ロット単位
            SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                  GOODS2.GOODS_CD_NRS                                                            " & vbNewLine _
                             & "                 ,GOODS2.LOT_NO                                                                  " & vbNewLine
        End If

        SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                 FROM                                                                            " & vbNewLine _
                             & "                        (SELECT                                                                  " & vbNewLine

        '荷動き単位設定
        If idoTaniFlg = IDO_NASHI_GOODS Then  '商品単位
            SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                          MAIN2.GOODS_CD_NRS                                                     " & vbNewLine
        Else                                  '商品・ロット単位
            SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                          MAIN2.GOODS_CD_NRS                                                     " & vbNewLine _
                             & "                         ,MAIN2.LOT_NO                                                           " & vbNewLine
        End If

        SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                         ,MAX(CASE WHEN (                                                        " & vbNewLine

        '在庫基準日設定
        If dateFlg = DATE_FLG_INKO_DATE Then '入庫日基準
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                             & "AND INKO_DATE >= @DATE_FROM                                                                      " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                             & "AND INKO_DATE >= @DATE_FROM                                                                      " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    INKO_DATE <> ''                                                                              " & vbNewLine
            Else
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    INKO_DATE <> ''                                                                              " & vbNewLine
            End If
        ElseIf dateFlg = DATE_FLG_OUTKA_DATE Then '出荷日基準
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                             & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine _
                             & "AND LAST_OUTKO_DATE <= @DATE_TO                                                                  " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                             & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                             & "AND LAST_OUTKO_DATE <= @DATE_TO                                                                  " & vbNewLine
            Else
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine
            End If
        ElseIf dateFlg = DATE_FLG_INOUT_DATE Then '入庫日、出荷日両方
            If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "   (INKO_DATE <> ''                                                                              " & vbNewLine _
                             & "AND INKO_DATE >= @DATE_FROM)                                                                     " & vbNewLine _
                             & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                             & "AND LAST_OUTKO_DATE >= @DATE_FROM                                                                " & vbNewLine _
                             & "AND LAST_OUTKO_DATE <= @DATE_TO)                                                                 " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "   (INKO_DATE <> ''                                                                              " & vbNewLine _
                             & "AND INKO_DATE >= @DATE_FROM)                                                                     " & vbNewLine _
                             & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                             & "AND LAST_OUTKO_DATE >= @DATE_FROM)                                                               " & vbNewLine
            ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "   (INKO_DATE <> '')                                                                             " & vbNewLine _
                             & "OR (LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine _
                             & "AND LAST_OUTKO_DATE <= @DATE_TO)                                                                 " & vbNewLine
            Else
                SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "    INKO_DATE <> ''                                                                              " & vbNewLine _
                             & "OR  LAST_OUTKO_DATE <> ''                                                                        " & vbNewLine
            End If
        Else
            '条件なし
        End If

        SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                                        ) THEN 1 ELSE 0                                          " & vbNewLine _
                             & "                              END) AS CNT                                                        " & vbNewLine _
                             & "                         FROM                                                                    " & vbNewLine _
                             & "                                (SELECT                                                          " & vbNewLine _
                             & "                                  ZAI.NRS_BR_CD                     AS NRS_BR_CD                 " & vbNewLine _
                             & "                                 ,ZAI.ZAI_REC_NO                    AS ZAI_REC_NO                " & vbNewLine _
                             & "                                 ,ZAI.CUST_CD_L                     AS CUST_CD_L                 " & vbNewLine _
                             & "                                 ,ZAI.CUST_CD_M                     AS CUST_CD_M                 " & vbNewLine _
                             & "                                 ,ZAI.INKO_DATE                     AS INKO_DATE                 " & vbNewLine _
                             & "                                 ,ZAI.GOODS_CD_NRS                  AS GOODS_CD_NRS              " & vbNewLine _
                             & "                                 ,ZAI.INKA_NO_L                     AS INKA_NO_L                 " & vbNewLine _
                             & "                                 ,ZAI.LOT_NO                        AS LOT_NO                    " & vbNewLine _
                             & "                                 ,ISNULL(MAX(OUTL.OUTKO_DATE), '')  AS LAST_OUTKO_DATE           " & vbNewLine _
                             & "                                 FROM                                                            " & vbNewLine _
                             & "                                 --在庫データ                                                    " & vbNewLine _
                             & "                                 $LM_TRN$..D_ZAI_TRS ZAI                                         " & vbNewLine _
                             & "                                 --入荷データL                                                   " & vbNewLine _
                             & "                                 LEFT JOIN $LM_TRN$..B_INKA_L INL                                " & vbNewLine _
                             & "                                 ON  INL.NRS_BR_CD = ZAI.NRS_BR_CD                               " & vbNewLine _
                             & "                                 AND INL.INKA_NO_L = ZAI.INKA_NO_L                               " & vbNewLine _
                             & "                                 AND INL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                             & "                                 --出荷データS                                                   " & vbNewLine _
                             & "                                 LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                              " & vbNewLine _
                             & "                                 ON  OUTS.NRS_BR_CD = ZAI.NRS_BR_CD                              " & vbNewLine _
                             & "                                 AND OUTS.ZAI_REC_NO = ZAI.ZAI_REC_NO                            " & vbNewLine _
                             & "                                 AND OUTS.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                             & "                                 --出荷データL                                                   " & vbNewLine _
                             & "                                 LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL                              " & vbNewLine _
                             & "                                 ON  OUTL.NRS_BR_CD = OUTS.NRS_BR_CD                             " & vbNewLine _
                             & "                                 AND OUTL.OUTKA_NO_L = OUTS.OUTKA_NO_L                           " & vbNewLine _
                             & "                                 AND OUTL.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                             & "                                 WHERE                                                           " & vbNewLine _
                             & "                                 ZAI.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                             & "                                 AND                                                             " & vbNewLine _
                             & "                                 INL.INKA_STATE_KB >= '50'                                       " & vbNewLine _
                             & "                                 AND                                                             " & vbNewLine _
                             & "                                 ZAI.CUST_CD_L = @CUST_CD_L                                      " & vbNewLine _
                             & "                                 AND                                                             " & vbNewLine _
                             & "                                 ZAI.CUST_CD_M = @CUST_CD_M                                      " & vbNewLine _
                             & "                                 AND                                                             " & vbNewLine _
                             & "                                 ZAI.PORA_ZAI_NB > 0                                             " & vbNewLine _
                             & "                                 AND                                                             " & vbNewLine _
                             & "                                 ZAI.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                             & "                                 GROUP BY                                                        " & vbNewLine _
                             & "                                  ZAI.NRS_BR_CD                                                  " & vbNewLine _
                             & "                                 ,ZAI.ZAI_REC_NO                                                 " & vbNewLine _
                             & "                                 ,ZAI.CUST_CD_L                                                  " & vbNewLine _
                             & "                                 ,ZAI.CUST_CD_M                                                  " & vbNewLine _
                             & "                                 ,ZAI.INKO_DATE                                                  " & vbNewLine _
                             & "                                 ,ZAI.GOODS_CD_NRS                                               " & vbNewLine _
                             & "                                 ,ZAI.INKA_NO_L                                                  " & vbNewLine _
                             & "                                 ,ZAI.LOT_NO                                                     " & vbNewLine _
                             & "                                ) MAIN2                                                          " & vbNewLine _
                             & "                         GROUP BY                                                                " & vbNewLine
        '荷動き単位設定
        If idoTaniFlg = IDO_NASHI_GOODS Then  '商品単位
            SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                          MAIN2.GOODS_CD_NRS                                                     " & vbNewLine
        Else                                  '商品・ロット単位
            SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                          MAIN2.GOODS_CD_NRS                                                     " & vbNewLine _
                             & "                         ,MAIN2.LOT_NO                                                           " & vbNewLine
        End If

        SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "                        ) GOODS2                                                                 " & vbNewLine _
                             & "                 WHERE                                                                           " & vbNewLine _
                             & "                 GOODS2.CNT = 0                                                                  " & vbNewLine _
                             & "                ) GOODS                                                                          " & vbNewLine _
                             & "         --在庫データ                                                                            " & vbNewLine _
                             & "         ,$LM_TRN$..D_ZAI_TRS ZAI                                                                " & vbNewLine _
                             & "         --入荷データL                                                                           " & vbNewLine _
                             & "         LEFT JOIN $LM_TRN$..B_INKA_L INL                                                        " & vbNewLine _
                             & "         ON  INL.NRS_BR_CD = ZAI.NRS_BR_CD                                                       " & vbNewLine _
                             & "         AND INL.INKA_NO_L = ZAI.INKA_NO_L                                                       " & vbNewLine _
                             & "         AND INL.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                             & "         --出荷データS                                                                           " & vbNewLine _
                             & "         LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                      " & vbNewLine _
                             & "         ON  OUTS.NRS_BR_CD = ZAI.NRS_BR_CD                                                      " & vbNewLine _
                             & "         AND OUTS.ZAI_REC_NO = ZAI.ZAI_REC_NO                                                    " & vbNewLine _
                             & "         AND OUTS.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                             & "         --出荷データL                                                                           " & vbNewLine _
                             & "         LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL                                                      " & vbNewLine _
                             & "         ON  OUTL.NRS_BR_CD = OUTS.NRS_BR_CD                                                     " & vbNewLine _
                             & "         AND OUTL.OUTKA_NO_L = OUTS.OUTKA_NO_L                                                   " & vbNewLine _
                             & "         AND OUTL.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                             & "         WHERE                                                                                   " & vbNewLine
        '荷動き単位設定
        If idoTaniFlg = IDO_NASHI_GOODS Then  '商品単位
            SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "             ZAI.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                               " & vbNewLine
        Else                                  '商品・ロット単位
            SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "             ZAI.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                               " & vbNewLine _
                             & "         AND ZAI.LOT_NO = GOODS.LOT_NO                                                           " & vbNewLine
        End If
        SQL_SELECT_IDO_NASHI = SQL_SELECT_IDO_NASHI & vbNewLine _
                             & "         AND                                                                                     " & vbNewLine _
                             & "         ZAI.PORA_ZAI_NB > 0                                                                     " & vbNewLine _
                             & "         AND                                                                                     " & vbNewLine _
                             & "         INL.INKA_STATE_KB >= '50'                                                               " & vbNewLine _
                             & "         AND                                                                                     " & vbNewLine _
                             & "         ZAI.SYS_DEL_FLG = '0'                                                                   " & vbNewLine _
                             & "         GROUP BY                                                                                " & vbNewLine _
                             & "          ZAI.NRS_BR_CD                                                                          " & vbNewLine _
                             & "         ,ZAI.ZAI_REC_NO                                                                         " & vbNewLine _
                             & "         ,ZAI.CUST_CD_L                                                                          " & vbNewLine _
                             & "         ,ZAI.CUST_CD_M                                                                          " & vbNewLine _
                             & "         ,ZAI.INKO_DATE                                                                          " & vbNewLine _
                             & "         ,ZAI.GOODS_CD_NRS                                                                       " & vbNewLine _
                             & "         ,ZAI.INKA_NO_L                                                                          " & vbNewLine _
                             & "         ,ZAI.LOT_NO                                                                             " & vbNewLine _
                             & "         ,ZAI.PORA_ZAI_NB                                                                        " & vbNewLine _
                             & "         ,ZAI.PORA_ZAI_QT                                                                        " & vbNewLine _
                             & "        ) MAIN                                                                                   " & vbNewLine _
                             & "--営業所マスタ                                                                                   " & vbNewLine _
                             & "LEFT JOIN $LM_MST$..M_NRS_BR MBR                                                                 " & vbNewLine _
                             & "ON  MBR.NRS_BR_CD = MAIN.NRS_BR_CD                                                               " & vbNewLine _
                             & "--商品マスタ                                                                                     " & vbNewLine _
                             & "LEFT JOIN $LM_MST$..M_GOODS MGOODS                                                               " & vbNewLine _
                             & "ON  MGOODS.NRS_BR_CD = MAIN.NRS_BR_CD                                                            " & vbNewLine _
                             & "AND MGOODS.GOODS_CD_NRS = MAIN.GOODS_CD_NRS                                                      " & vbNewLine _
                             & "--荷主マスタ                                                                                     " & vbNewLine _
                             & "LEFT JOIN $LM_MST$..M_CUST MCUST                                                                 " & vbNewLine _
                             & "ON  MCUST.NRS_BR_CD = MGOODS.NRS_BR_CD                                                           " & vbNewLine _
                             & "AND MCUST.CUST_CD_L = MGOODS.CUST_CD_L                                                           " & vbNewLine _
                             & "AND MCUST.CUST_CD_M = MGOODS.CUST_CD_M                                                           " & vbNewLine _
                             & "AND MCUST.CUST_CD_S = MGOODS.CUST_CD_S                                                           " & vbNewLine _
                             & "AND MCUST.CUST_CD_SS = MGOODS.CUST_CD_SS                                                         " & vbNewLine _
                             & "--荷主帳票パターン取得                                                                           " & vbNewLine _
                             & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                              " & vbNewLine _
                             & "ON  MAIN.NRS_BR_CD = MCR1.NRS_BR_CD                                                              " & vbNewLine _
                             & "AND MAIN.CUST_CD_L = MCR1.CUST_CD_L                                                              " & vbNewLine _
                             & "AND MAIN.CUST_CD_M = MCR1.CUST_CD_M                                                              " & vbNewLine _
                             & "AND '00' = MCR1.CUST_CD_S                                                                        " & vbNewLine _
                             & "AND MCR1.PTN_ID = '30'                                                                           " & vbNewLine _
                             & "--帳票パターン取得                                                                               " & vbNewLine _
                             & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                    " & vbNewLine _
                             & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                               " & vbNewLine _
                             & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                     " & vbNewLine _
                             & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                     " & vbNewLine _
                             & "AND MR1.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                             & "--商品Mの荷主での荷主帳票パターン取得                                                            " & vbNewLine _
                             & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                              " & vbNewLine _
                             & "ON  MGOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                                            " & vbNewLine _
                             & "AND MGOODS.CUST_CD_L = MCR2.CUST_CD_L                                                            " & vbNewLine _
                             & "AND MGOODS.CUST_CD_M = MCR2.CUST_CD_M                                                            " & vbNewLine _
                             & "AND MGOODS.CUST_CD_S = MCR2.CUST_CD_S                                                            " & vbNewLine _
                             & "AND MCR2.PTN_ID = '30'                                                                           " & vbNewLine _
                             & "--帳票パターン取得                                                                               " & vbNewLine _
                             & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                    " & vbNewLine _
                             & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                               " & vbNewLine _
                             & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                     " & vbNewLine _
                             & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                     " & vbNewLine _
                             & "AND MR2.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                             & "--存在しない場合の帳票パターン取得                                                               " & vbNewLine _
                             & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                    " & vbNewLine _
                             & "ON  MR3.NRS_BR_CD = MAIN.NRS_BR_CD                                                               " & vbNewLine _
                             & "AND MR3.PTN_ID = '30'                                                                            " & vbNewLine _
                             & "AND MR3.STANDARD_FLAG = '01'                                                                     " & vbNewLine _
                             & "AND MR3.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                             & "ORDER BY                                                                                         " & vbNewLine _
                             & " MAIN.INKO_DATE                                                                                  " & vbNewLine _
                             & ",MGOODS.GOODS_NM_1                                                                               " & vbNewLine

    End Sub

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
        Dim inTbl As DataTable = ds.Tables("LMD550IN")
        'IN情報の出力日付を取得
        Dim dateFrom As String = inTbl.Rows(0).Item("DATE_FROM").ToString()        '印刷範囲From
        Dim dateTo As String = inTbl.Rows(0).Item("DATE_TO").ToString()            '印刷範囲To
        'IN情報の出力条件を取得
        Dim dateFlg As String = inTbl.Rows(0).Item("DATE_FLG").ToString()          '在庫基準
        Dim idoTaniFlg As String = inTbl.Rows(0).Item("IDO_TANI_FLG").ToString()   '荷動き・単位

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If idoTaniFlg = IDO_ARI Then
            '荷動きありの場合
            Call Me.CreateSqlMprtIdoAri(dateFlg, dateFrom, dateTo)      'SQL文作成
            Me._StrSql.Append(SQL_MPRT_IDO_ARI)                         'SQL構築
            Call Me.SetConditionMasterSQL(sqlMprt)                      '条件設定
        Else
            '荷動きなしの場合
            Call Me.CreateSqlMprtIdoNashi(dateFlg, dateFrom, dateTo)    'SQL文作成
            Me._StrSql.Append(SQL_MPRT_IDO_NASHI)                       'SQL構築
            Call Me.SetConditionMasterSQL(sqlMprt)                      '条件設定
        End If
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD550DAC", "SelectMPrt", cmd)

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
    ''' 在庫テーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD550IN")
        'IN情報の出力日付を取得
        Dim dateFrom As String = inTbl.Rows(0).Item("DATE_FROM").ToString()
        Dim dateTo As String = inTbl.Rows(0).Item("DATE_TO").ToString()
        'IN情報の出力条件を取得
        Dim dateFlg As String = inTbl.Rows(0).Item("DATE_FLG").ToString()          '在庫基準
        Dim idoTaniFlg As String = inTbl.Rows(0).Item("IDO_TANI_FLG").ToString()   '荷動き・単位

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If idoTaniFlg = IDO_ARI Then
            '荷動きありSQL作成
            Call Me.CreateSqlSelectIdoAri(dateFlg, dateFrom, dateTo)                    'SQL文作成
            Me._StrSql.Append(SQL_SELECT_IDO_ARI)                                       'SQL構築
            Call Me.SetConditionMasterSQL(sqlOut)                                       '条件設定
        Else
            '荷動きなしSQL作成
            Call Me.CreateSqlSelectIdoNashi(idoTaniFlg, dateFlg, dateFrom, dateTo)      'SQL文作成
            Me._StrSql.Append(SQL_SELECT_IDO_NASHI)                                     'SQL構築
            Call Me.SetConditionMasterSQL(sqlOut)                                       '条件設定

        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD550DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("DATE_FLG", "DATE_FLG")
        map.Add("IDO_TANI_FLG", "IDO_TANI_FLG")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("DATE_FROM", "DATE_FROM")
        map.Add("DATE_TO", "DATE_TO")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("LAST_OUTKO_DATE", "LAST_OUTKO_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD550OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal sqlType As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '荷主コード大
            whereStr = .Item("CUST_CD_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            '荷主コード中
            whereStr = .Item("CUST_CD_M").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            '在庫基準
            whereStr = .Item("DATE_FLG").ToString()
            '印刷データ取得時に設定
            If sqlType = sqlOut Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FLG", whereStr, DBDataType.CHAR))
            End If

            '荷動き・単位
            whereStr = .Item("IDO_TANI_FLG").ToString()
            '印刷データ取得時に設定
            If sqlType = sqlOut Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IDO_TANI_FLG", whereStr, DBDataType.CHAR))
            End If

            '出力日付FROM
            whereStr = .Item("DATE_FROM").ToString()
            '印刷データ取得時の場合は、常に設定
            If String.IsNullOrEmpty(whereStr) = False OrElse sqlType = sqlOut Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '出力日付TO
            whereStr = .Item("DATE_TO").ToString()
            '印刷データ取得時の場合は、常に設定
            If String.IsNullOrEmpty(whereStr) = False OrElse sqlType = sqlOut Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

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
