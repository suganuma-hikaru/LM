' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD590    : 入出荷履歴表
'  作  成  者       :  [sagawa]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD590DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD590DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' 帳票種別取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_MPRT As String = String.Empty

    ''' <summary>
    ''' 印刷データ取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_SELECT As String = String.Empty

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

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用SQL作成
    ''' </summary>
    ''' <param name="dateFrom"></param>
    ''' <param name="dateTo"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlMprt(ByVal dateFrom As String, ByVal dateTo As String)

        SQL_MPRT = "SELECT DISTINCT                                                                                            " & vbNewLine _
                 & " MAIN.NRS_BR_CD                                       AS NRS_BR_CD                                         " & vbNewLine _
                 & ",'33'                                                 AS PTN_ID                                            " & vbNewLine _
                 & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                                                          " & vbNewLine _
                 & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                                                          " & vbNewLine _
                 & "      ELSE MR3.PTN_CD                                                                                      " & vbNewLine _
                 & " END                                                  AS PTN_CD                                            " & vbNewLine _
                 & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                          " & vbNewLine _
                 & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                          " & vbNewLine _
                 & "      ELSE MR3.RPT_ID                                                                                      " & vbNewLine _
                 & " END                                                  AS RPT_ID                                            " & vbNewLine _
                 & "FROM                                                                                                       " & vbNewLine _
                 & "	 --入荷データ                                                                                          " & vbNewLine _
                 & "	(SELECT                                                                                                " & vbNewLine _
                 & " 	  INKAL.NRS_BR_CD              AS NRS_BR_CD                                                            " & vbNewLine _
                 & " 	 ,INKAM.GOODS_CD_NRS           AS GOODS_CD_NRS                                                         " & vbNewLine _
                 & " 	 FROM                                                                                                  " & vbNewLine _
                 & " 	 --入荷データL                                                                                         " & vbNewLine _
                 & " 	 $LM_TRN$..B_INKA_L INKAL                                                                              " & vbNewLine _
                 & " 	 --入荷データM                                                                                         " & vbNewLine _
                 & " 	 LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                                    " & vbNewLine _
                 & " 	 ON  INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                                 " & vbNewLine _
                 & " 	 AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                                 " & vbNewLine _
                 & " 	 AND INKAM.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                 & " 	 WHERE                                                                                                 " & vbNewLine _
                 & " 	     INKAL.NRS_BR_CD = @NRS_BR_CD                                                                      " & vbNewLine _
                 & " 	 AND INKAL.CUST_CD_L = @CUST_CD_L                                                                      " & vbNewLine _
                 & " 	 AND INKAL.CUST_CD_M = @CUST_CD_M                                                                      " & vbNewLine
        If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
            SQL_MPRT = SQL_MPRT & vbNewLine _
                 & " 	 AND INKAL.INKA_DATE >= @DATE_FROM                                                                     " & vbNewLine _
                 & " 	 AND INKAL.INKA_DATE <= @DATE_TO                                                                       " & vbNewLine
        ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
            SQL_MPRT = SQL_MPRT & vbNewLine _
                 & " 	 AND INKAL.INKA_DATE >= @DATE_FROM                                                                     " & vbNewLine
        ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
            SQL_MPRT = SQL_MPRT & vbNewLine _
                 & " 	 AND INKAL.INKA_DATE <= @DATE_TO                                                                       " & vbNewLine
        Else
            '日付条件なし
        End If
        SQL_MPRT = SQL_MPRT & vbNewLine _
                 & " 	 AND INKAL.FURI_NO = ''                                                                                " & vbNewLine _
                 & " 	 AND INKAL.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                 & "                                                                                                           " & vbNewLine _
                 & "	 --出荷データ                                                                                          " & vbNewLine _
                 & " 	 UNION ALL                                                                                             " & vbNewLine _
                 & " 	 SELECT                                                                                                " & vbNewLine _
                 & " 	  OUTKAL.NRS_BR_CD             AS NRS_BR_CD                                                            " & vbNewLine _
                 & " 	 ,OUTKAM.GOODS_CD_NRS          AS GOODS_CD_NRS                                                         " & vbNewLine _
                 & " 	 FROM                                                                                                  " & vbNewLine _
                 & " 	 --出荷データL                                                                                         " & vbNewLine _
                 & " 	 $LM_TRN$..C_OUTKA_L OUTKAL                                                                            " & vbNewLine _
                 & " 	 --出荷データM                                                                                         " & vbNewLine _
                 & " 	 LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM                                                                  " & vbNewLine _
                 & " 	 ON  OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                               " & vbNewLine _
                 & " 	 AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                                             " & vbNewLine _
                 & " 	 AND OUTKAM.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
                 & " 	 WHERE                                                                                                 " & vbNewLine _
                 & " 	     OUTKAL.NRS_BR_CD = @NRS_BR_CD                                                                     " & vbNewLine _
                 & " 	 AND OUTKAL.CUST_CD_L = @CUST_CD_L                                                                     " & vbNewLine _
                 & " 	 AND OUTKAL.CUST_CD_M = @CUST_CD_M                                                                     " & vbNewLine
        If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
            SQL_MPRT = SQL_MPRT & vbNewLine _
                 & " 	 AND OUTKAL.OUTKA_PLAN_DATE >= @DATE_FROM                                                              " & vbNewLine _
                 & " 	 AND OUTKAL.OUTKA_PLAN_DATE <= @DATE_TO                                                                " & vbNewLine
        ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
            SQL_MPRT = SQL_MPRT & vbNewLine _
                 & " 	 AND OUTKAL.OUTKA_PLAN_DATE >= @DATE_FROM                                                              " & vbNewLine
        ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
            SQL_MPRT = SQL_MPRT & vbNewLine _
                 & " 	 AND OUTKAL.OUTKA_PLAN_DATE <= @DATE_TO                                                                " & vbNewLine
        Else
            '日付条件なし
        End If
        SQL_MPRT = SQL_MPRT & vbNewLine _
                 & " 	 AND OUTKAL.FURI_NO = ''                                                                               " & vbNewLine _
                 & " 	 AND OUTKAL.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
                 & "                                                                                                           " & vbNewLine
        '在庫残数は出力日付FROMが空でない場合のみ取得する
        If String.IsNullOrEmpty(dateFrom) = False Then
            SQL_MPRT = SQL_MPRT & vbNewLine _
                 & "	 --在庫残数                                                                                            " & vbNewLine _
                 & "     UNION ALL                                                                                             " & vbNewLine _
                 & "     SELECT                                                                                                " & vbNewLine _
                 & " 	  BASE.NRS_BR_CD                AS NRS_BR_CD                                                           " & vbNewLine _
                 & " 	 ,BASE.GOODS_CD_NRS             AS GOODS_CD_NRS                                                        " & vbNewLine _
                 & " 	 FROM                                                                                                  " & vbNewLine _
                 & " 		(SELECT                                                                                            " & vbNewLine _
                 & " 		  ZAN.NRS_BR_CD                AS NRS_BR_CD                                                        " & vbNewLine _
                 & " 		 ,ZAN.GOODS_CD_NRS             AS GOODS_CD_NRS                                                     " & vbNewLine _
                 & " 		 ,SUM(ZAN.INKA_QT)             AS INKA_QT                                                          " & vbNewLine _
                 & " 		 FROM                                                                                              " & vbNewLine _
                 & " 		 	(--入荷数取得                                                                                  " & vbNewLine _
                 & " 		 	 SELECT                                                                                        " & vbNewLine _
                 & " 		 	  INKAL2.NRS_BR_CD                                              AS NRS_BR_CD                   " & vbNewLine _
                 & " 		 	 ,INKAM2.GOODS_CD_NRS                                           AS GOODS_CD_NRS                " & vbNewLine _
                 & " 		 	 ,(INKAS2.KONSU * GOODS2.PKG_NB + INKAS2.HASU) * INKAS2.IRIME   AS INKA_QT                     " & vbNewLine _
                 & " 		 	 FROM                                                                                          " & vbNewLine _
                 & " 		 	 --入荷データL                                                                                 " & vbNewLine _
                 & " 		 	 $LM_TRN$..B_INKA_L INKAL2                                                                     " & vbNewLine _
                 & " 		 	 --入荷データM                                                                                 " & vbNewLine _
                 & " 		 	 LEFT JOIN $LM_TRN$..B_INKA_M INKAM2                                                           " & vbNewLine _
                 & " 		 	 ON  INKAM2.NRS_BR_CD = INKAL2.NRS_BR_CD                                                       " & vbNewLine _
                 & " 		 	 AND INKAM2.INKA_NO_L = INKAL2.INKA_NO_L                                                       " & vbNewLine _
                 & " 		 	 AND INKAM2.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                 & " 		 	 --入荷データS                                                                                 " & vbNewLine _
                 & " 		 	 LEFT JOIN $LM_TRN$..B_INKA_S INKAS2                                                           " & vbNewLine _
                 & " 		 	 ON  INKAS2.NRS_BR_CD = INKAM2.NRS_BR_CD                                                       " & vbNewLine _
                 & " 		 	 AND INKAS2.INKA_NO_L = INKAM2.INKA_NO_L                                                       " & vbNewLine _
                 & " 		 	 AND INKAS2.INKA_NO_M = INKAM2.INKA_NO_M                                                       " & vbNewLine _
                 & " 		 	 AND INKAS2.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                 & " 		 	 --商品マスタ                                                                                  " & vbNewLine _
                 & " 		 	 LEFT JOIN $LM_MST$..M_GOODS GOODS2                                                            " & vbNewLine _
                 & " 		 	 ON  GOODS2.NRS_BR_CD = INKAM2.NRS_BR_CD                                                       " & vbNewLine _
                 & " 		 	 AND GOODS2.CUST_CD_L = INKAL2.CUST_CD_L                                                       " & vbNewLine _
                 & " 		 	 AND GOODS2.CUST_CD_M = INKAL2.CUST_CD_M                                                       " & vbNewLine _
                 & " 		 	 AND GOODS2.GOODS_CD_NRS = INKAM2.GOODS_CD_NRS                                                 " & vbNewLine _
                 & " 		 	 WHERE                                                                                         " & vbNewLine _
                 & " 		 	     INKAL2.NRS_BR_CD = @NRS_BR_CD                                                             " & vbNewLine _
                 & " 		 	 AND INKAL2.CUST_CD_L = @CUST_CD_L                                                             " & vbNewLine _
                 & " 		 	 AND INKAL2.CUST_CD_M = @CUST_CD_M                                                             " & vbNewLine _
                 & " 		 	 AND INKAL2.INKA_STATE_KB >= '50'                                                              " & vbNewLine _
                 & " 		 	 AND INKAS2.ZAI_REC_NO <> ''        --高速化                                                   " & vbNewLine _
                 & " 		 	 AND INKAL2.INKA_DATE < @DATE_FROM                                                             " & vbNewLine _
                 & " 		 	 AND INKAL2.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                 & "                                                                                                           " & vbNewLine _
                 & "			 --出荷数取得                                                                                  " & vbNewLine _
                 & "		 	 UNION ALL                                                                                     " & vbNewLine _
                 & "		 	 SELECT                                                                                        " & vbNewLine _
                 & "		 	  OUTKAL2.NRS_BR_CD         AS NRS_BR_CD                                                       " & vbNewLine _
                 & "		 	 ,OUTKAM2.GOODS_CD_NRS      AS GOODS_CD_NRS                                                    " & vbNewLine _
                 & "		 	 ,OUTKAM2.ALCTD_QT * -1     AS INKA_QT                                                         " & vbNewLine _
                 & "		 	 --出荷データL                                                                                 " & vbNewLine _
                 & "		 	 FROM                                                                                          " & vbNewLine _
                 & "		 	 $LM_TRN$..C_OUTKA_L OUTKAL2                                                                   " & vbNewLine _
                 & "		 	 --出荷データM                                                                                 " & vbNewLine _
                 & "		 	 LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM2                                                         " & vbNewLine _
                 & "		 	 ON  OUTKAM2.NRS_BR_CD = OUTKAL2.NRS_BR_CD                                                     " & vbNewLine _
                 & "		 	 AND OUTKAM2.OUTKA_NO_L = OUTKAL2.OUTKA_NO_L                                                   " & vbNewLine _
                 & "		 	 AND OUTKAM2.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                 & "		 	 WHERE                                                                                         " & vbNewLine _
                 & "		 	     OUTKAL2.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
                 & "		 	 AND OUTKAL2.CUST_CD_L = @CUST_CD_L                                                            " & vbNewLine _
                 & "		 	 AND OUTKAL2.CUST_CD_M = @CUST_CD_M                                                            " & vbNewLine _
                 & "		 	 AND OUTKAL2.OUTKA_STATE_KB >= '60'                                                            " & vbNewLine _
                 & "		 	 AND OUTKAL2.OUTKA_PLAN_DATE < @DATE_FROM                                                      " & vbNewLine _
                 & "		 	 AND OUTKAL2.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                 & "	 	) ZAN                                                                                              " & vbNewLine _
                 & "	 	GROUP BY                                                                                           " & vbNewLine _
                 & "	 	 ZAN.NRS_BR_CD                                                                                     " & vbNewLine _
                 & "	 	,ZAN.GOODS_CD_NRS                                                                                  " & vbNewLine _
                 & " 	) BASE                                                                                                 " & vbNewLine _
                 & " 	WHERE                                                                                                  " & vbNewLine _
                 & " 	BASE.INKA_QT > 0                                                                                       " & vbNewLine
        End If
        SQL_MPRT = SQL_MPRT & vbNewLine _
                 & "                                                                                                           " & vbNewLine _
                 & ") MAIN                                                                                                     " & vbNewLine _
                 & "--商品マスタ                                                                                               " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_GOODS MGOODS                                                                         " & vbNewLine _
                 & "ON  MGOODS.NRS_BR_CD = MAIN.NRS_BR_CD                                                                      " & vbNewLine _
                 & "AND MGOODS.GOODS_CD_NRS = MAIN.GOODS_CD_NRS                                                                " & vbNewLine _
                 & "--荷主マスタ                                                                                               " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST MCUST                                                                           " & vbNewLine _
                 & "ON  MCUST.NRS_BR_CD = MGOODS.NRS_BR_CD                                                                     " & vbNewLine _
                 & "AND MCUST.CUST_CD_L = MGOODS.CUST_CD_L                                                                     " & vbNewLine _
                 & "AND MCUST.CUST_CD_M = MGOODS.CUST_CD_M                                                                     " & vbNewLine _
                 & "AND MCUST.CUST_CD_S = MGOODS.CUST_CD_S                                                                     " & vbNewLine _
                 & "AND MCUST.CUST_CD_SS = MGOODS.CUST_CD_SS                                                                   " & vbNewLine _
                 & "--荷主帳票パターン取得                                                                                     " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                        " & vbNewLine _
                 & "ON  MAIN.NRS_BR_CD = MCR1.NRS_BR_CD                                                                        " & vbNewLine _
                 & "AND @CUST_CD_L = MCR1.CUST_CD_L                                                                            " & vbNewLine _
                 & "AND @CUST_CD_M = MCR1.CUST_CD_M                                                                            " & vbNewLine _
                 & "AND '00' = MCR1.CUST_CD_S                                                                                  " & vbNewLine _
                 & "AND MCR1.PTN_ID = '33'                                                                                     " & vbNewLine _
                 & "--帳票パターン取得                                                                                         " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                              " & vbNewLine _
                 & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                         " & vbNewLine _
                 & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                               " & vbNewLine _
                 & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                               " & vbNewLine _
                 & "AND MR1.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine _
                 & "--商品Mの荷主での荷主帳票パターン取得                                                                      " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                        " & vbNewLine _
                 & "ON  MGOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                                                      " & vbNewLine _
                 & "AND MGOODS.CUST_CD_L = MCR2.CUST_CD_L                                                                      " & vbNewLine _
                 & "AND MGOODS.CUST_CD_M = MCR2.CUST_CD_M                                                                      " & vbNewLine _
                 & "AND MGOODS.CUST_CD_S = MCR2.CUST_CD_S                                                                      " & vbNewLine _
                 & "AND MCR2.PTN_ID = '33'                                                                                     " & vbNewLine _
                 & "--帳票パターン取得                                                                                         " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                              " & vbNewLine _
                 & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                         " & vbNewLine _
                 & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                               " & vbNewLine _
                 & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                               " & vbNewLine _
                 & "AND MR2.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine _
                 & "--存在しない場合の帳票パターン取得                                                                         " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                              " & vbNewLine _
                 & "ON  MR3.NRS_BR_CD = MAIN.NRS_BR_CD                                                                         " & vbNewLine _
                 & "AND MR3.PTN_ID = '33'                                                                                      " & vbNewLine _
                 & "AND MR3.STANDARD_FLAG = '01'                                                                               " & vbNewLine _
                 & "AND MR3.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine

    End Sub

    ''' <summary>
    ''' 印刷データ取得用SQL作成
    ''' </summary>
    ''' <param name="dateFrom"></param>
    ''' <param name="dateTo"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlSelect(ByVal dateFrom As String, ByVal dateTo As String, ByVal rptId As String)

        SQL_SELECT = "SELECT                                                                                                     " & vbNewLine _
                  & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                          " & vbNewLine _
                  & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                          " & vbNewLine _
                  & "      ELSE MR3.RPT_ID                                                                                      " & vbNewLine _
                  & " END                     AS RPT_ID                                                                         " & vbNewLine _
                  & ",MAIN.DATA_KBN           AS DATA_KBN                                                                       " & vbNewLine _
                  & ",MAIN.NRS_BR_CD          AS NRS_BR_CD                                                                      " & vbNewLine _
                  & ",@DATE_FROM              AS DATE_FROM                                                                      " & vbNewLine _
                  & ",@DATE_TO                AS DATE_TO                                                                        " & vbNewLine _
                  & ",MAIN.CUST_CD_L          AS CUST_CD_L                                                                      " & vbNewLine _
                  & ",MAIN.CUST_CD_M          AS CUST_CD_M                                                                      " & vbNewLine _
                  & ",MCUST.CUST_CD_S         AS CUST_CD_S                                                                      " & vbNewLine _
                  & ",MCUST.CUST_CD_SS        AS CUST_CD_SS                                                                     " & vbNewLine _
                  & ",MCUST.CUST_NM_L         AS CUST_NM_L                                                                      " & vbNewLine _
                  & ",MCUST.CUST_NM_M         AS CUST_NM_M                                                                      " & vbNewLine _
                  & ",MCUST.CUST_NM_S         AS CUST_NM_S                                                                      " & vbNewLine _
                  & ",MCUST.CUST_NM_SS        AS CUST_NM_SS                                                                     " & vbNewLine _
                  & ",MAIN.GOODS_CD_NRS       AS GOODS_CD_NRS                                                                   " & vbNewLine _
                  & ",MGOODS.GOODS_CD_CUST    AS GOODS_CD_CUST                                                                  " & vbNewLine _
                  & ",MGOODS.CUST_COST_CD2    AS CUST_COST_CD2                                                                  " & vbNewLine _
                  & ",MGOODS.GOODS_NM_1       AS GOODS_NM                                                                       " & vbNewLine _
                  & ",MAIN.INOUTKA_DATE       AS INOUTKA_DATE                                                                   " & vbNewLine _
                  & ",MAIN.CUST_ORD_NO        AS CUST_ORD_NO                                                                    " & vbNewLine _
                  & ",MAIN.BUYER_ORD_NO       AS BUYER_ORD_NO                                                                   " & vbNewLine _
                  & ",MAIN.CUST_ORD_NO_DTL    AS CUST_ORD_NO_DTL                                                                " & vbNewLine _
                  & ",MAIN.BUYER_ORD_NO_DTL   AS BUYER_ORD_NO_DTL                                                               " & vbNewLine _
                  & ",MAIN.REMARK_OUT         AS REMARK_OUT                                                                     " & vbNewLine _
                  & ",MAIN.LOT_NO             AS LOT_NO                                                                         " & vbNewLine _
                  & ",MAIN.INKA_NB            AS INKA_NB                                                                        " & vbNewLine _
                  & ",MAIN.INKA_QT            AS INKA_QT                                                                        " & vbNewLine _
                  & ",MAIN.OUTKA_NB           AS OUTKA_NB                                                                       " & vbNewLine _
                  & ",MAIN.OUTKA_QT           AS OUTKA_QT                                                                       " & vbNewLine _
                  & ",MGOODS.NB_UT            AS NB_UT                                                                          " & vbNewLine _
                  & ",MGOODS.STD_IRIME_UT     AS IRIME_UT                                                                       " & vbNewLine _
                  & ",MAIN.DEST_CD            AS DEST_CD                                                                        " & vbNewLine _
                  & ",MAIN.DEST_NM            AS DEST_NM                                                                        " & vbNewLine _
                  & "FROM                                                                                                       " & vbNewLine _
                  & "        (--入荷データ                                                                                      " & vbNewLine _
                  & "         SELECT                                                                                            " & vbNewLine _
                  & "          INKAL.NRS_BR_CD                                           AS NRS_BR_CD                           " & vbNewLine _
                  & "         ,'1'                                                       AS DATA_KBN                            " & vbNewLine _
                  & "         ,INKAL.CUST_CD_L                                           AS CUST_CD_L                           " & vbNewLine _
                  & "         ,INKAL.CUST_CD_M                                           AS CUST_CD_M                           " & vbNewLine _
                  & "         ,INKAM.GOODS_CD_NRS                                        AS GOODS_CD_NRS                        " & vbNewLine _
                  & "         ,INKAL.INKA_DATE                                           AS INOUTKA_DATE                        " & vbNewLine _
                  & "         ,INKAL.OUTKA_FROM_ORD_NO_L                                 AS CUST_ORD_NO                         " & vbNewLine _
                  & "         ,INKAL.BUYER_ORD_NO_L                                      AS BUYER_ORD_NO                        " & vbNewLine _
                  & "         ,INKAM.OUTKA_FROM_ORD_NO_M                                 AS CUST_ORD_NO_DTL                     " & vbNewLine _
                  & "         ,INKAM.BUYER_ORD_NO_M                                      AS BUYER_ORD_NO_DTL                    " & vbNewLine _
                  & "         ,INKAS.REMARK_OUT                                          AS REMARK_OUT                          " & vbNewLine _
                  & "         ,INKAS.LOT_NO                                              AS LOT_NO                              " & vbNewLine _
                  & "         ,INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU                   AS INKA_NB                             " & vbNewLine _
                  & "         ,(INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) * INKAS.IRIME   AS INKA_QT                             " & vbNewLine _
                  & "         ,0                                                         AS OUTKA_NB                            " & vbNewLine _
                  & "         ,0                                                         AS OUTKA_QT                            " & vbNewLine _
                  & "         ,UNSOL.ORIG_CD                                             AS DEST_CD                             " & vbNewLine _
                  & "         ,DEST.DEST_NM                                              AS DEST_NM                             " & vbNewLine _
                  & "         FROM                                                                                              " & vbNewLine _
                  & "         --入荷データL                                                                                     " & vbNewLine _
                  & "         $LM_TRN$..B_INKA_L INKAL                                                                          " & vbNewLine _
                  & "         --入荷データM                                                                                     " & vbNewLine _
                  & "         LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                                " & vbNewLine _
                  & "         ON  INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                             " & vbNewLine _
                  & "         AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                             " & vbNewLine _
                  & "         AND INKAM.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
                  & "         --入荷データS                                                                                     " & vbNewLine _
                  & "         LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                                                " & vbNewLine _
                  & "         ON  INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                             " & vbNewLine _
                  & "         AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                             " & vbNewLine _
                  & "         AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                             " & vbNewLine _
                  & "         AND INKAS.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
                  & "         --運送L                                                                                           " & vbNewLine _
                  & "         LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                                                " & vbNewLine _
                  & "         ON  UNSOL.NRS_BR_CD = INKAL.NRS_BR_CD                                                             " & vbNewLine _
                  & "         AND UNSOL.INOUTKA_NO_L = INKAL.INKA_NO_L                                                          " & vbNewLine _
                  & "         AND UNSOL.MOTO_DATA_KB = '10'                                                                     " & vbNewLine _
                  & "         AND UNSOL.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
                  & "         --商品マスタ                                                                                      " & vbNewLine _
                  & "         LEFT JOIN $LM_MST$..M_GOODS GOODS                                                                 " & vbNewLine _
                  & "         ON  GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                                             " & vbNewLine _
                  & "         AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                                       " & vbNewLine _
                  & "         --届先マスタ                                                                                      " & vbNewLine _
                  & "         LEFT JOIN $LM_MST$..M_DEST DEST                                                                   " & vbNewLine _
                  & "         ON  DEST.NRS_BR_CD = UNSOL.NRS_BR_CD                                                              " & vbNewLine _
                  & "         AND DEST.CUST_CD_L = UNSOL.CUST_CD_L                                                              " & vbNewLine _
                  & "         AND DEST.DEST_CD = UNSOL.ORIG_CD                                                                  " & vbNewLine _
                  & "         WHERE                                                                                             " & vbNewLine _
                  & "             INKAL.NRS_BR_CD = @NRS_BR_CD                                                                  " & vbNewLine _
                  & "         AND INKAL.CUST_CD_L = @CUST_CD_L                                                                  " & vbNewLine _
                  & "         AND INKAL.CUST_CD_M = @CUST_CD_M                                                                  " & vbNewLine
        If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
            SQL_SELECT = SQL_SELECT & vbNewLine _
                  & " 	      AND INKAL.INKA_DATE >= @DATE_FROM                                                                 " & vbNewLine _
                  & " 	      AND INKAL.INKA_DATE <= @DATE_TO                                                                   " & vbNewLine
        ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
            SQL_SELECT = SQL_SELECT & vbNewLine _
                  & " 	      AND INKAL.INKA_DATE >= @DATE_FROM                                                                 " & vbNewLine
        ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
            SQL_SELECT = SQL_SELECT & vbNewLine _
                  & " 	      AND INKAL.INKA_DATE <= @DATE_TO                                                                   " & vbNewLine
        Else
            '日付条件なし
        End If
        SQL_SELECT = SQL_SELECT & vbNewLine _
                  & "         AND INKAL.FURI_NO = ''                                                                            " & vbNewLine _
                  & "         AND INKAL.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
                  & "                                                                                                           " & vbNewLine _
                  & "         --出荷データ                                                                                      " & vbNewLine _
                  & "         UNION ALL                                                                                         " & vbNewLine _
                  & "         SELECT                                                                                            " & vbNewLine _
                  & "          OUTKAL.NRS_BR_CD                                       AS NRS_BR_CD                              " & vbNewLine _
                  & "         ,'2'                                                    AS DATA_KBN                               " & vbNewLine _
                  & "         ,OUTKAL.CUST_CD_L                                       AS CUST_CD_L                              " & vbNewLine _
                  & "         ,OUTKAL.CUST_CD_M                                       AS CUST_CD_M                              " & vbNewLine _
                  & "         ,OUTKAM.GOODS_CD_NRS                                    AS GOODS_CD_NRS                           " & vbNewLine _
                  & "         ,OUTKAL.OUTKA_PLAN_DATE                                 AS INOUTKA_DATE                           " & vbNewLine _
                  & "         ,OUTKAL.CUST_ORD_NO                                     AS CUST_ORD_NO                            " & vbNewLine _
                  & "         ,OUTKAL.BUYER_ORD_NO                                    AS BUYER_ORD_NO                           " & vbNewLine _
                  & "         ,OUTKAM.CUST_ORD_NO_DTL                                 AS CUST_ORD_NO_DTL                        " & vbNewLine _
                  & "         ,OUTKAM.BUYER_ORD_NO_DTL                                AS BUYER_ORD_NO_DTL                       " & vbNewLine _
                  & "         ,ZAITRS.REMARK_OUT                                      AS REMARK_OUT                             " & vbNewLine _
                  & "         ,OUTKAS.LOT_NO                                          AS LOT_NO                                 " & vbNewLine _
                  & "         ,0                                                      AS INKA_NB                                " & vbNewLine _
                  & "         ,0                                                      AS INKA_QT                                " & vbNewLine _
                  & "         ,OUTKAS.ALCTD_NB                                        AS OUTKA_NB                               " & vbNewLine _
                  & "         ,OUTKAS.ALCTD_QT                                        AS OUTKA_QT                               " & vbNewLine _
                  & "         ,CASE WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_CD                                                " & vbNewLine _
                  & "               ELSE OUTKAL.DEST_CD                                                                         " & vbNewLine _
                  & "          END                                                    AS DEST_CD                                " & vbNewLine _
                  & "         ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                                              " & vbNewLine _
                  & "               WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_NM                                                " & vbNewLine _
                  & "               ELSE DEST.DEST_NM                                                                           " & vbNewLine _
                  & "          END                                                    AS DEST_NM                                " & vbNewLine _
                  & "         FROM                                                                                              " & vbNewLine _
                  & "         --出荷データL                                                                                     " & vbNewLine _
                  & "         $LM_TRN$..C_OUTKA_L OUTKAL                                                                        " & vbNewLine _
                  & "         --出荷データM                                                                                     " & vbNewLine _
                  & "         LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM                                                              " & vbNewLine _
                  & "         ON  OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                           " & vbNewLine _
                  & "         AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                                         " & vbNewLine _
                  & "         AND OUTKAM.SYS_DEL_FLG = '0'                                                                      " & vbNewLine _
                  & "         --出荷データS                                                                                     " & vbNewLine _
                  & "         LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKAS                                                              " & vbNewLine _
                  & "         ON  OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                                           " & vbNewLine _
                  & "         AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                                         " & vbNewLine _
                  & "         AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                                         " & vbNewLine _
                  & "         AND OUTKAS.SYS_DEL_FLG = '0'                                                                      " & vbNewLine _
                  & "         --出荷EDIデータL                                                                                  " & vbNewLine _
                  & "         LEFT JOIN                                                                                         " & vbNewLine _
                  & "         (SELECT                                                                                           " & vbNewLine _
                  & "           NRS_BR_CD                                                                                       " & vbNewLine _
                  & "          ,OUTKA_CTL_NO                                                                                    " & vbNewLine _
                  & "          ,MIN(DEST_CD)   AS DEST_CD                                                                       " & vbNewLine _
                  & "          ,MIN(DEST_NM)   AS DEST_NM                                                                       " & vbNewLine _
                  & "          ,SYS_DEL_FLG                                                                                     " & vbNewLine _
                  & "          FROM                                                                                             " & vbNewLine _
                  & "          $LM_TRN$..H_OUTKAEDI_L                                                                           " & vbNewLine _
                  & "          GROUP BY                                                                                         " & vbNewLine _
                  & "           NRS_BR_CD                                                                                       " & vbNewLine _
                  & "          ,OUTKA_CTL_NO                                                                                    " & vbNewLine _
                  & "          ,SYS_DEL_FLG                                                                                     " & vbNewLine _
                  & "         ) EDIL                                                                                            " & vbNewLine _
                  & "         ON  EDIL.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                             " & vbNewLine _
                  & "         AND EDIL.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L                                                         " & vbNewLine _
                  & "         AND EDIL.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                  & "         --在庫データ                                                                                      " & vbNewLine _
                  & "         LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                                              " & vbNewLine _
                  & "         ON  ZAITRS.NRS_BR_CD = OUTKAS.NRS_BR_CD                                                           " & vbNewLine _
                  & "         AND ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                                         " & vbNewLine _
                  & "         AND ZAITRS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS  --高速化                                           " & vbNewLine _
                  & "         AND ZAITRS.SYS_DEL_FLG = '0'                                                                      " & vbNewLine _
                  & "         --届先マスタ                                                                                      " & vbNewLine _
                  & "         LEFT JOIN $LM_MST$..M_DEST DEST                                                                   " & vbNewLine _
                  & "         ON  DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                             " & vbNewLine _
                  & "         AND DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                                             " & vbNewLine _
                  & "         AND DEST.DEST_CD = OUTKAL.DEST_CD                                                                 " & vbNewLine _
                  & "         WHERE                                                                                             " & vbNewLine _
                  & "             OUTKAL.NRS_BR_CD = @NRS_BR_CD                                                                 " & vbNewLine _
                  & "         AND OUTKAL.CUST_CD_L = @CUST_CD_L                                                                 " & vbNewLine _
                  & "         AND OUTKAL.CUST_CD_M = @CUST_CD_M                                                                 " & vbNewLine
        If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then
            SQL_SELECT = SQL_SELECT & vbNewLine _
                  & " 	      AND OUTKAL.OUTKA_PLAN_DATE >= @DATE_FROM                                                          " & vbNewLine _
                  & " 	      AND OUTKAL.OUTKA_PLAN_DATE <= @DATE_TO                                                            " & vbNewLine
        ElseIf String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = True Then
            SQL_SELECT = SQL_SELECT & vbNewLine _
                  & " 	      AND OUTKAL.OUTKA_PLAN_DATE >= @DATE_FROM                                                          " & vbNewLine
        ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = False Then
            SQL_SELECT = SQL_SELECT & vbNewLine _
                  & " 	      AND OUTKAL.OUTKA_PLAN_DATE <= @DATE_TO                                                            " & vbNewLine
        Else
            '日付条件なし
        End If
        SQL_SELECT = SQL_SELECT & vbNewLine _
                  & "         AND OUTKAL.FURI_NO = ''                                                                           " & vbNewLine _
                  & "         AND OUTKAL.SYS_DEL_FLG = '0'                                                                      " & vbNewLine _
                  & "                                                                                                           " & vbNewLine
        '在庫残数は出力日付FROMが空でない場合のみ取得する
        If String.IsNullOrEmpty(dateFrom) = False Then
            '出力帳票がLMD591（デュポン専用）の場合
            If rptId = "LMD591" Then
                SQL_SELECT = SQL_SELECT & vbNewLine _
                  & "--在庫残数                                                                                                 " & vbNewLine _
                  & "UNION ALL                                                                                                  " & vbNewLine _
                  & "SELECT                                                                                                     " & vbNewLine _
                  & " BASE.NRS_BR_CD                AS NRS_BR_CD                                                                " & vbNewLine _
                  & ",'3'                           AS DATA_KBN                                                                 " & vbNewLine _
                  & ",BASE.CUST_CD_L                AS CUST_CD_L                                                                " & vbNewLine _
                  & ",BASE.CUST_CD_M                AS CUST_CD_M                                                                " & vbNewLine _
                  & ",BASE.GOODS_CD_NRS             AS GOODS_CD_NRS                                                             " & vbNewLine _
                  & ",@PRE_DATE_FROM                AS INOUTKA_DATE                                                             " & vbNewLine _
                  & ",BASE.CUST_ORD_NO              AS CUST_ORD_NO                                                              " & vbNewLine _
                  & ",BASE.BUYER_ORD_NO             AS BUYER_ORD_NO                                                             " & vbNewLine _
                  & ",''                            AS CUST_ORD_NO_DTL                                                          " & vbNewLine _
                  & ",''                            AS BUYER_ORD_NO_DTL                                                         " & vbNewLine _
                  & ",BASE.REMARK_OUT               AS REMARK_OUT                                                               " & vbNewLine _
                  & ",BASE.LOT_NO                   AS LOT_NO                                                                   " & vbNewLine _
                  & ",BASE.INKA_NB                  AS INKA_NB                                                                  " & vbNewLine _
                  & ",BASE.INKA_QT                  AS INKA_QT                                                                  " & vbNewLine _
                  & ",0                             AS OUTKA_NB                                                                 " & vbNewLine _
                  & ",0                             AS OUTKA_QT                                                                 " & vbNewLine _
                  & ",''                            AS DEST_CD                                                                  " & vbNewLine _
                  & ",''                            AS DEST_NM                                                                  " & vbNewLine _
                  & "FROM                                                                                                       " & vbNewLine _
                  & "       (SELECT                                                                                             " & vbNewLine _
                  & "         MIN(ZAN.NRS_BR_CD)            AS NRS_BR_CD                                                        " & vbNewLine _
                  & "        ,MIN(ZAN.CUST_CD_L)            AS CUST_CD_L                                                        " & vbNewLine _
                  & "        ,MIN(ZAN.CUST_CD_M)            AS CUST_CD_M                                                        " & vbNewLine _
                  & "        ,ZAN.GOODS_CD_NRS              AS GOODS_CD_NRS                                                     " & vbNewLine _
                  & "        ,MAX(ZAN.CUST_ORD_NO)          AS CUST_ORD_NO                                                      " & vbNewLine _
                  & "        ,MAX(ZAN.BUYER_ORD_NO)         AS BUYER_ORD_NO                                                     " & vbNewLine _
                  & "        ,MAX(ZAN.REMARK_OUT)           AS REMARK_OUT                                                       " & vbNewLine _
                  & "        ,ZAN.LOT_NO                    AS LOT_NO                                                           " & vbNewLine _
                  & "        ,SUM(ZAN.INKA_NB)              AS INKA_NB                                                          " & vbNewLine _
                  & "        ,SUM(ZAN.INKA_QT)              AS INKA_QT                                                          " & vbNewLine _
                  & "        ,ZAN.DEST_CD                   AS DEST_CD                                                          " & vbNewLine _
                  & "        ,MAX(ZAN.DEST_NM)              AS DEST_NM                                                          " & vbNewLine _
                  & "        FROM                                                                                               " & vbNewLine _
                  & "               (--入出荷の履歴                                                                             " & vbNewLine _
                  & "                SELECT                                                                                     " & vbNewLine _
                  & "                 NRS_BR_CD                AS NRS_BR_CD                                                     " & vbNewLine _
                  & "                ,CUST_CD_L                AS CUST_CD_L                                                     " & vbNewLine _
                  & "                ,CUST_CD_M                AS CUST_CD_M                                                     " & vbNewLine _
                  & "                ,ZAI_REC_NO               AS ZAI_REC_NO                                                    " & vbNewLine _
                  & "                ,GOODS_CD_NRS             AS GOODS_CD_NRS                                                  " & vbNewLine _
                  & "                ,MAX(CUST_ORD_NO)         AS CUST_ORD_NO                                                   " & vbNewLine _
                  & "                ,MAX(BUYER_ORD_NO)        AS BUYER_ORD_NO                                                  " & vbNewLine _
                  & "                ,MAX(REMARK_OUT)          AS REMARK_OUT                                                    " & vbNewLine _
                  & "                ,LOT_NO                   AS LOT_NO                                                        " & vbNewLine _
                  & "                ,SUM(PORA_ZAI_NB)         AS INKA_NB                                                       " & vbNewLine _
                  & "                ,SUM(PORA_ZAI_QT)         AS INKA_QT                                                       " & vbNewLine _
                  & "                ,DEST_CD                  AS DEST_CD                                                       " & vbNewLine _
                  & "                ,MAX(DEST_NM)             AS DEST_NM                                                       " & vbNewLine _
                  & "                FROM                                                                                       " & vbNewLine _
                  & "                       (--入荷データ(B_INKA_S)                                                             " & vbNewLine _
                  & "                        SELECT                                                                             " & vbNewLine _
                  & "                         ''                                                        AS OUTKA_NO_L           " & vbNewLine _
                  & "                        ,''                                                        AS OUTKA_NO_M           " & vbNewLine _
                  & "                        ,''                                                        AS OUTKA_NO_S           " & vbNewLine _
                  & "                        ,INL1.NRS_BR_CD                                            AS NRS_BR_CD            " & vbNewLine _
                  & "                        ,INL1.CUST_CD_L                                            AS CUST_CD_L            " & vbNewLine _
                  & "                        ,INL1.CUST_CD_M                                            AS CUST_CD_M            " & vbNewLine _
                  & "                        ,INS1.ZAI_REC_NO                                           AS ZAI_REC_NO           " & vbNewLine _
                  & "                        ,INS1.REMARK_OUT                                           AS REMARK_OUT           " & vbNewLine _
                  & "                        ,INM1.GOODS_CD_NRS                                         AS GOODS_CD_NRS         " & vbNewLine _
                  & "                        ,INL1.OUTKA_FROM_ORD_NO_L                                  AS CUST_ORD_NO          " & vbNewLine _
                  & "                        ,INL1.BUYER_ORD_NO_L                                       AS BUYER_ORD_NO         " & vbNewLine _
                  & "                        ,ISNULL(INS1.LOT_NO, '')                                   AS LOT_NO               " & vbNewLine _
                  & "                        ,(INS1.KONSU * MG1.PKG_NB) + INS1.HASU                     AS PORA_ZAI_NB          " & vbNewLine _
                  & "                        ,((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME      AS PORA_ZAI_QT          " & vbNewLine _
                  & "                        ,ISNULL(UNSOL1.ORIG_CD, '')                                AS DEST_CD              " & vbNewLine _
                  & "                        ,ISNULL(DEST1.DEST_NM, '')                                 AS DEST_NM              " & vbNewLine _
                  & "                        FROM                                                                               " & vbNewLine _
                  & "                        $LM_TRN$..B_INKA_L INL1                                                            " & vbNewLine _
                  & "                        LEFT JOIN $LM_TRN$..B_INKA_M INM1                                                  " & vbNewLine _
                  & "                        ON  INM1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND INM1.NRS_BR_CD = INL1.NRS_BR_CD                                                " & vbNewLine _
                  & "                        AND INM1.INKA_NO_L = INL1.INKA_NO_L                                                " & vbNewLine _
                  & "                        LEFT JOIN $LM_TRN$..B_INKA_S INS1                                                  " & vbNewLine _
                  & "                        ON  INS1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND INS1.NRS_BR_CD = INL1.NRS_BR_CD                                                " & vbNewLine _
                  & "                        AND INS1.INKA_NO_L = INL1.INKA_NO_L                                                " & vbNewLine _
                  & "                        AND INS1.INKA_NO_M = INM1.INKA_NO_M                                                " & vbNewLine _
                  & "                        LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL1                                                " & vbNewLine _
                  & "                        ON  UNSOL1.NRS_BR_CD = INL1.NRS_BR_CD                                              " & vbNewLine _
                  & "                        AND UNSOL1.INOUTKA_NO_L = INL1.INKA_NO_L                                           " & vbNewLine _
                  & "                        AND UNSOL1.MOTO_DATA_KB = '10'                                                     " & vbNewLine _
                  & "                        AND UNSOL1.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                  & "                        LEFT JOIN $LM_MST$..M_GOODS MG1                                                    " & vbNewLine _
                  & "                        ON  MG1.NRS_BR_CD = INL1.NRS_BR_CD                                                 " & vbNewLine _
                  & "                        AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS                                           " & vbNewLine _
                  & "                        LEFT JOIN $LM_MST$..M_DEST DEST1                                                   " & vbNewLine _
                  & "                        ON  DEST1.NRS_BR_CD = UNSOL1.NRS_BR_CD                                             " & vbNewLine _
                  & "                        AND DEST1.CUST_CD_L = UNSOL1.CUST_CD_L                                             " & vbNewLine _
                  & "                        AND DEST1.DEST_CD = UNSOL1.ORIG_CD                                                 " & vbNewLine _
                  & "                        WHERE                                                                              " & vbNewLine _
                  & "                            INL1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND INL1.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                  & "                        AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')                    " & vbNewLine _
                  & "                        AND INL1.CUST_CD_L = @CUST_CD_L                                                    " & vbNewLine _
                  & "                        AND INL1.CUST_CD_M = @CUST_CD_M                                                    " & vbNewLine _
                  & "                        AND INL1.OUTKA_FROM_ORD_NO_L NOT LIKE 'ZZZ%'                                       " & vbNewLine _
                  & "                        AND (INL1.INKA_DATE < @DATE_FROM OR INL1.INKA_STATE_KB < '50')                     " & vbNewLine _
                  & "                                                                                                           " & vbNewLine _
                  & "                        --在庫移動分を加減算(D_IDO_TRS)                                                    " & vbNewLine _
                  & "                        --移動後                                                                           " & vbNewLine _
                  & "                        UNION ALL                                                                          " & vbNewLine _
                  & "                        SELECT                                                                             " & vbNewLine _
                  & "                         ''                                          AS OUTKA_NO_L                         " & vbNewLine _
                  & "                        ,''                                          AS OUTKA_NO_M                         " & vbNewLine _
                  & "                        ,''                                          AS OUTKA_NO_S                         " & vbNewLine _
                  & "                        ,ZAI1.NRS_BR_CD                              AS NRS_BR_CD                          " & vbNewLine _
                  & "                        ,ZAI1.CUST_CD_L                              AS CUST_CD_L                          " & vbNewLine _
                  & "                        ,ZAI1.CUST_CD_M                              AS CUST_CD_M                          " & vbNewLine _
                  & "                        ,IDO1.N_ZAI_REC_NO                           AS ZAI_REC_NO                         " & vbNewLine _
                  & "                        ,''                                          AS REMARK_OUT                         " & vbNewLine _
                  & "                        ,ZAI1.GOODS_CD_NRS                           AS GOODS_CD_NRS                       " & vbNewLine _
                  & "                        ,''                                          AS CUST_ORD_NO                        " & vbNewLine _
                  & "                        ,''                                          AS BUYER_ORD_NO                       " & vbNewLine _
                  & "                        ,ISNULL(ZAI1.LOT_NO, '')                     AS LOT_NO                             " & vbNewLine _
                  & "                        ,IDO1.N_PORA_ZAI_NB                          AS PORA_ZAI_NB                        " & vbNewLine _
                  & "                        ,IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME        AS PORA_ZAI_QT                        " & vbNewLine _
                  & "                        ,''                                          AS DEST_CD                            " & vbNewLine _
                  & "                        ,''                                          AS DEST_NM                            " & vbNewLine _
                  & "                        FROM                                                                               " & vbNewLine _
                  & "                        $LM_TRN$..D_IDO_TRS IDO1                                                           " & vbNewLine _
                  & "                        LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI1                                                 " & vbNewLine _
                  & "                        ON  ZAI1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND ZAI1.NRS_BR_CD = IDO1.NRS_BR_CD                                                " & vbNewLine _
                  & "                        AND ZAI1.ZAI_REC_NO = IDO1.O_ZAI_REC_NO                                            " & vbNewLine _
                  & "                        WHERE                                                                              " & vbNewLine _
                  & "                            IDO1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND IDO1.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                  & "                        AND ZAI1.CUST_CD_L = @CUST_CD_L                                                    " & vbNewLine _
                  & "                        AND ZAI1.CUST_CD_M = @CUST_CD_M                                                    " & vbNewLine _
                  & "                        AND IDO1.IDO_DATE < @DATE_FROM                                                     " & vbNewLine _
                  & "                                                                                                           " & vbNewLine _
                  & "                        --移動前                                                                           " & vbNewLine _
                  & "                        UNION ALL                                                                          " & vbNewLine _
                  & "                        SELECT                                                                             " & vbNewLine _
                  & "                         ''                                          AS OUTKA_NO_L                         " & vbNewLine _
                  & "                        ,''                                          AS OUTKA_NO_M                         " & vbNewLine _
                  & "                        ,''                                          AS OUTKA_NO_S                         " & vbNewLine _
                  & "                        ,ZAI2.NRS_BR_CD                              AS NRS_BR_CD                          " & vbNewLine _
                  & "                        ,ZAI2.CUST_CD_L                              AS CUST_CD_L                          " & vbNewLine _
                  & "                        ,ZAI2.CUST_CD_M                              AS CUST_CD_M                          " & vbNewLine _
                  & "                        ,IDO2.O_ZAI_REC_NO                           AS ZAI_REC_NO                         " & vbNewLine _
                  & "                        ,''                                          AS REMARK_OUT                         " & vbNewLine _
                  & "                        ,ZAI2.GOODS_CD_NRS                           AS GOODS_CD_NRS                       " & vbNewLine _
                  & "                        ,''                                          AS CUST_ORD_NO                        " & vbNewLine _
                  & "                        ,''                                          AS BUYER_ORD_NO                       " & vbNewLine _
                  & "                        ,ISNULL(ZAI2.LOT_NO, '')                     AS LOT_NO                             " & vbNewLine _
                  & "                        ,IDO2.N_PORA_ZAI_NB * -1                     AS PORA_ZAI_NB                        " & vbNewLine _
                  & "                        ,IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1   AS PORA_ZAI_QT                        " & vbNewLine _
                  & "                        ,''                                          AS DEST_CD                            " & vbNewLine _
                  & "                        ,''                                          AS DEST_NM                            " & vbNewLine _
                  & "                        FROM                                                                               " & vbNewLine _
                  & "                        $LM_TRN$..D_IDO_TRS IDO2                                                           " & vbNewLine _
                  & "                        LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI2                                                 " & vbNewLine _
                  & "                        ON  ZAI2.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND ZAI2.NRS_BR_CD = IDO2.NRS_BR_CD                                                " & vbNewLine _
                  & "                        AND ZAI2.ZAI_REC_NO = IDO2.O_ZAI_REC_NO                                            " & vbNewLine _
                  & "                        WHERE                                                                              " & vbNewLine _
                  & "                            IDO2.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND IDO2.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                  & "                        AND ZAI2.CUST_CD_L = @CUST_CD_L                                                    " & vbNewLine _
                  & "                        AND ZAI2.CUST_CD_M = @CUST_CD_M                                                    " & vbNewLine _
                  & "                        AND IDO2.IDO_DATE < @DATE_FROM                                                     " & vbNewLine _
                  & "                                                                                                           " & vbNewLine _
                  & "                        --出荷データ(C_OUTKA_S)                                                            " & vbNewLine _
                  & "                        UNION ALL                                                                          " & vbNewLine _
                  & "                        SELECT DISTINCT                                                                    " & vbNewLine _
                  & "                         OUTKA_NO_L                                                                        " & vbNewLine _
                  & "                        ,OUTKA_NO_M                                                                        " & vbNewLine _
                  & "                        ,OUTKA_NO_S                                                                        " & vbNewLine _
                  & "                        ,NRS_BR_CD                                                                         " & vbNewLine _
                  & "                        ,CUST_CD_L                                                                         " & vbNewLine _
                  & "                        ,CUST_CD_M                                                                         " & vbNewLine _
                  & "                        ,ZAI_REC_NO                                                                        " & vbNewLine _
                  & "                        ,REMARK_OUT                                                                        " & vbNewLine _
                  & "                        ,GOODS_CD_NRS                                                                      " & vbNewLine _
                  & "                        ,CUST_ORD_NO                                                                       " & vbNewLine _
                  & "                        ,BUYER_ORD_NO                                                                      " & vbNewLine _
                  & "                        ,LOT_NO                                                                            " & vbNewLine _
                  & "                        ,PORA_ZAI_NB                                                                       " & vbNewLine _
                  & "                        ,PORA_ZAI_QT                                                                       " & vbNewLine _
                  & "                        ,DEST_CD                                                                           " & vbNewLine _
                  & "                        ,DEST_NM                                                                           " & vbNewLine _
                  & "                        FROM                                                                               " & vbNewLine _
                  & "                               (SELECT                                                                     " & vbNewLine _
                  & "                                 OUTS.OUTKA_NO_L                   AS OUTKA_NO_L                           " & vbNewLine _
                  & "                                ,OUTS.OUTKA_NO_M                   AS OUTKA_NO_M                           " & vbNewLine _
                  & "                                ,OUTS.OUTKA_NO_S                   AS OUTKA_NO_S                           " & vbNewLine _
                  & "                                ,OUTL.NRS_BR_CD                    AS NRS_BR_CD                            " & vbNewLine _
                  & "                                ,OUTL.CUST_CD_L                    AS CUST_CD_L                            " & vbNewLine _
                  & "                                ,OUTL.CUST_CD_M                    AS CUST_CD_M                            " & vbNewLine _
                  & "                                ,OUTS.ZAI_REC_NO                   AS ZAI_REC_NO                           " & vbNewLine _
                  & "                                ,''                                AS REMARK_OUT                           " & vbNewLine _
                  & "                                ,OUTM.GOODS_CD_NRS                 AS GOODS_CD_NRS                         " & vbNewLine _
                  & "                                ,INKAL2.OUTKA_FROM_ORD_NO_L        AS CUST_ORD_NO                          " & vbNewLine _
                  & "                                ,INKAL2.BUYER_ORD_NO_L             AS BUYER_ORD_NO                         " & vbNewLine _
                  & "                                ,ISNULL(OUTS.LOT_NO, '')           AS LOT_NO                               " & vbNewLine _
                  & "                                ,OUTS.ALCTD_NB * -1                AS PORA_ZAI_NB                          " & vbNewLine _
                  & "                                ,OUTS.ALCTD_QT * -1                AS PORA_ZAI_QT                          " & vbNewLine _
                  & "                                ,ISNULL(UNSOL2.ORIG_CD, '')        AS DEST_CD                              " & vbNewLine _
                  & "                                ,''                                AS DEST_NM                              " & vbNewLine _
                  & "                                FROM                                                                       " & vbNewLine _
                  & "                                $LM_TRN$..C_OUTKA_L OUTL                                                   " & vbNewLine _
                  & "                                LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                         " & vbNewLine _
                  & "                                ON  OUTM.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                  & "                                AND OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                  & "                                AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                      " & vbNewLine _
                  & "                                LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                         " & vbNewLine _
                  & "                                ON  OUTS.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                  & "                                AND OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                  & "                                AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                      " & vbNewLine _
                  & "                                AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                      " & vbNewLine _
                  & "                                LEFT JOIN $LM_TRN$..B_INKA_L INKAL2                                        " & vbNewLine _
                  & "                                ON  INKAL2.NRS_BR_CD = OUTS.NRS_BR_CD                                      " & vbNewLine _
                  & "                                AND INKAL2.INKA_NO_L = OUTS.INKA_NO_L                                      " & vbNewLine _
                  & "                                AND INKAL2.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                  & "                                LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL2                                        " & vbNewLine _
                  & "                                ON  UNSOL2.NRS_BR_CD = INKAL2.NRS_BR_CD                                    " & vbNewLine _
                  & "                                AND UNSOL2.INOUTKA_NO_L = INKAL2.INKA_NO_L                                 " & vbNewLine _
                  & "                                AND UNSOL2.MOTO_DATA_KB = '10'                                             " & vbNewLine _
                  & "                                AND UNSOL2.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                  & "                                WHERE                                                                      " & vbNewLine _
                  & "                                    OUTL.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                  & "                                AND OUTM.ALCTD_KB <> '04'                                                  " & vbNewLine _
                  & "                                AND OUTL.OUTKA_STATE_KB !< '60'                                            " & vbNewLine _
                  & "                                AND OUTL.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                  & "                                AND OUTL.CUST_CD_L = @CUST_CD_L                                            " & vbNewLine _
                  & "                                AND OUTL.CUST_CD_M = @CUST_CD_M                                            " & vbNewLine _
                  & "                                AND OUTL.CUST_ORD_NO NOT LIKE 'ZZZ%'                                       " & vbNewLine _
                  & "                                AND OUTL.OUTKA_PLAN_DATE < @DATE_FROM                                      " & vbNewLine _
                  & "                               ) OUT                                                                       " & vbNewLine _
                  & "                       ) RIREKI                                                                            " & vbNewLine _
                  & "                WHERE                                                                                      " & vbNewLine _
                  & "                    CUST_CD_L = @CUST_CD_L                                                                 " & vbNewLine _
                  & "                AND CUST_CD_M = @CUST_CD_M                                                                 " & vbNewLine _
                  & "                GROUP BY                                                                                   " & vbNewLine _
                  & "                 NRS_BR_CD                                                                                 " & vbNewLine _
                  & "                ,CUST_CD_L                                                                                 " & vbNewLine _
                  & "                ,CUST_CD_M                                                                                 " & vbNewLine _
                  & "                ,ZAI_REC_NO                                                                                " & vbNewLine _
                  & "                ,GOODS_CD_NRS                                                                              " & vbNewLine _
                  & "                ,LOT_NO                                                                                    " & vbNewLine _
                  & "                ,DEST_CD                                                                                   " & vbNewLine _
                  & "               ) ZAN                                                                                       " & vbNewLine _
                  & "        GROUP BY                                                                                           " & vbNewLine _
                  & "         ZAN.GOODS_CD_NRS                                                                                  " & vbNewLine _
                  & "        ,ZAN.LOT_NO                                                                                        " & vbNewLine _
                  & "        ,ZAN.DEST_CD                                                                                       " & vbNewLine _
                  & "       ) BASE                                                                                              " & vbNewLine _
                  & "WHERE                                                                                                      " & vbNewLine _
                  & "BASE.INKA_QT > 0                                                                                           " & vbNewLine

            Else 'それ以外の場合（標準）
                SQL_SELECT = SQL_SELECT & vbNewLine _
                  & "--在庫残数                                                                                                 " & vbNewLine _
                  & "UNION ALL                                                                                                  " & vbNewLine _
                  & "SELECT                                                                                                     " & vbNewLine _
                  & " BASE.NRS_BR_CD                AS NRS_BR_CD                                                                " & vbNewLine _
                  & ",'3'                           AS DATA_KBN                                                                 " & vbNewLine _
                  & ",BASE.CUST_CD_L                AS CUST_CD_L                                                                " & vbNewLine _
                  & ",BASE.CUST_CD_M                AS CUST_CD_M                                                                " & vbNewLine _
                  & ",BASE.GOODS_CD_NRS             AS GOODS_CD_NRS                                                             " & vbNewLine _
                  & ",@PRE_DATE_FROM                AS INOUTKA_DATE                                                             " & vbNewLine _
                  & ",''                            AS CUST_ORD_NO                                                              " & vbNewLine _
                  & ",''                            AS BUYER_ORD_NO                                                             " & vbNewLine _
                  & ",''                            AS CUST_ORD_NO_DTL                                                          " & vbNewLine _
                  & ",''                            AS BUYER_ORD_NO_DTL                                                         " & vbNewLine _
                  & ",BASE.REMARK_OUT               AS REMARK_OUT                                                               " & vbNewLine _
                  & ",BASE.LOT_NO                   AS LOT_NO                                                                   " & vbNewLine _
                  & ",BASE.INKA_NB                  AS INKA_NB                                                                  " & vbNewLine _
                  & ",BASE.INKA_QT                  AS INKA_QT                                                                  " & vbNewLine _
                  & ",0                             AS OUTKA_NB                                                                 " & vbNewLine _
                  & ",0                             AS OUTKA_QT                                                                 " & vbNewLine _
                  & ",''                            AS DEST_CD                                                                  " & vbNewLine _
                  & ",''                            AS DEST_NM                                                                  " & vbNewLine _
                  & "FROM                                                                                                       " & vbNewLine _
                  & "       (SELECT                                                                                             " & vbNewLine _
                  & "         MIN(ZAN.NRS_BR_CD)            AS NRS_BR_CD                                                        " & vbNewLine _
                  & "        ,MIN(ZAN.CUST_CD_L)            AS CUST_CD_L                                                        " & vbNewLine _
                  & "        ,MIN(ZAN.CUST_CD_M)            AS CUST_CD_M                                                        " & vbNewLine _
                  & "        ,ZAN.GOODS_CD_NRS              AS GOODS_CD_NRS                                                     " & vbNewLine _
                  & "        ,MAX(ZAN.REMARK_OUT)           AS REMARK_OUT                                                       " & vbNewLine _
                  & "        ,ZAN.LOT_NO                    AS LOT_NO                                                           " & vbNewLine _
                  & "        ,SUM(ZAN.INKA_NB)              AS INKA_NB                                                          " & vbNewLine _
                  & "        ,SUM(ZAN.INKA_QT)              AS INKA_QT                                                          " & vbNewLine _
                  & "        FROM                                                                                               " & vbNewLine _
                  & "               (--入出荷の履歴                                                                             " & vbNewLine _
                  & "                SELECT                                                                                     " & vbNewLine _
                  & "                 NRS_BR_CD                AS NRS_BR_CD                                                     " & vbNewLine _
                  & "                ,CUST_CD_L                AS CUST_CD_L                                                     " & vbNewLine _
                  & "                ,CUST_CD_M                AS CUST_CD_M                                                     " & vbNewLine _
                  & "                ,ZAI_REC_NO               AS ZAI_REC_NO                                                    " & vbNewLine _
                  & "                ,MAX(REMARK_OUT)          AS REMARK_OUT                                                    " & vbNewLine _
                  & "                ,GOODS_CD_NRS             AS GOODS_CD_NRS                                                  " & vbNewLine _
                  & "                ,LOT_NO                   AS LOT_NO                                                        " & vbNewLine _
                  & "                ,SUM(PORA_ZAI_NB)         AS INKA_NB                                                       " & vbNewLine _
                  & "                ,SUM(PORA_ZAI_QT)         AS INKA_QT                                                       " & vbNewLine _
                  & "                FROM                                                                                       " & vbNewLine _
                  & "                       (--入荷データ(B_INKA_S)                                                             " & vbNewLine _
                  & "                        SELECT                                                                             " & vbNewLine _
                  & "                         ''                                                        AS OUTKA_NO_L           " & vbNewLine _
                  & "                        ,''                                                        AS OUTKA_NO_M           " & vbNewLine _
                  & "                        ,''                                                        AS OUTKA_NO_S           " & vbNewLine _
                  & "                        ,INL1.NRS_BR_CD                                            AS NRS_BR_CD            " & vbNewLine _
                  & "                        ,INL1.CUST_CD_L                                            AS CUST_CD_L            " & vbNewLine _
                  & "                        ,INL1.CUST_CD_M                                            AS CUST_CD_M            " & vbNewLine _
                  & "                        ,INS1.ZAI_REC_NO                                           AS ZAI_REC_NO           " & vbNewLine _
                  & "                        ,INS1.REMARK_OUT                                           AS REMARK_OUT           " & vbNewLine _
                  & "                        ,INM1.GOODS_CD_NRS                                         AS GOODS_CD_NRS         " & vbNewLine _
                  & "                        ,ISNULL(INS1.LOT_NO, '')                                   AS LOT_NO               " & vbNewLine _
                  & "                        ,(INS1.KONSU * MG1.PKG_NB) + INS1.HASU                     AS PORA_ZAI_NB          " & vbNewLine _
                  & "                        ,((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME      AS PORA_ZAI_QT          " & vbNewLine _
                  & "                        FROM                                                                               " & vbNewLine _
                  & "                        $LM_TRN$..B_INKA_L INL1                                                            " & vbNewLine _
                  & "                        LEFT JOIN $LM_TRN$..B_INKA_M INM1                                                  " & vbNewLine _
                  & "                        ON  INM1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND INM1.NRS_BR_CD = INL1.NRS_BR_CD                                                " & vbNewLine _
                  & "                        AND INM1.INKA_NO_L = INL1.INKA_NO_L                                                " & vbNewLine _
                  & "                        LEFT JOIN $LM_TRN$..B_INKA_S INS1                                                  " & vbNewLine _
                  & "                        ON  INS1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND INS1.NRS_BR_CD = INL1.NRS_BR_CD                                                " & vbNewLine _
                  & "                        AND INS1.INKA_NO_L = INL1.INKA_NO_L                                                " & vbNewLine _
                  & "                        AND INS1.INKA_NO_M = INM1.INKA_NO_M                                                " & vbNewLine _
                  & "                        LEFT JOIN $LM_MST$..M_GOODS MG1                                                    " & vbNewLine _
                  & "                        ON  MG1.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                  & "                        AND MG1.NRS_BR_CD = INL1.NRS_BR_CD                                                 " & vbNewLine _
                  & "                        AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS                                           " & vbNewLine _
                  & "                        WHERE                                                                              " & vbNewLine _
                  & "                            INL1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        AND INL1.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                  & "                        AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')                    " & vbNewLine _
                  & "                        AND INL1.CUST_CD_L = @CUST_CD_L                                                    " & vbNewLine _
                  & "                        AND INL1.CUST_CD_M = @CUST_CD_M                                                    " & vbNewLine _
                  & "                        AND (INL1.INKA_DATE < @DATE_FROM OR INL1.INKA_STATE_KB < '50')                     " & vbNewLine _
                  & "                                                                                                           " & vbNewLine _
                  & "                        --在庫移動分を加減算(D_IDO_TRS)                                                    " & vbNewLine _
                  & "                        --移動後                                                                           " & vbNewLine _
                  & "                        --UNION ALL                                                                          " & vbNewLine _
                  & "                        --SELECT                                                                             " & vbNewLine _
                  & "                        -- ''                                          AS OUTKA_NO_L                         " & vbNewLine _
                  & "                        --,''                                          AS OUTKA_NO_M                         " & vbNewLine _
                  & "                        --,''                                          AS OUTKA_NO_S                         " & vbNewLine _
                  & "                        --,ZAI1.NRS_BR_CD                              AS NRS_BR_CD                          " & vbNewLine _
                  & "                       -- ,ZAI1.CUST_CD_L                              AS CUST_CD_L                          " & vbNewLine _
                  & "                        --,ZAI1.CUST_CD_M                              AS CUST_CD_M                          " & vbNewLine _
                  & "                        --,IDO1.N_ZAI_REC_NO                           AS ZAI_REC_NO                         " & vbNewLine _
                  & "                        --,''                                          AS REMARK_OUT                         " & vbNewLine _
                  & "                        --,ZAI1.GOODS_CD_NRS                           AS GOODS_CD_NRS                       " & vbNewLine _
                  & "                        --,ISNULL(ZAI1.LOT_NO, '')                     AS LOT_NO                             " & vbNewLine _
                  & "                        --,IDO1.N_PORA_ZAI_NB                          AS PORA_ZAI_NB                        " & vbNewLine _
                  & "                        --,IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME        AS PORA_ZAI_QT                        " & vbNewLine _
                  & "                        --FROM                                                                               " & vbNewLine _
                  & "                        --$LM_TRN$..D_IDO_TRS IDO1                                                           " & vbNewLine _
                  & "                        --LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI1                                                 " & vbNewLine _
                  & "                        --ON  ZAI1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        --AND ZAI1.NRS_BR_CD = IDO1.NRS_BR_CD                                                " & vbNewLine _
                  & "                        --AND ZAI1.ZAI_REC_NO = IDO1.O_ZAI_REC_NO                                            " & vbNewLine _
                  & "                        --WHERE                                                                              " & vbNewLine _
                  & "                        --    IDO1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        --AND IDO1.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                  & "                        --AND ZAI1.CUST_CD_L = @CUST_CD_L                                                    " & vbNewLine _
                  & "                        --AND ZAI1.CUST_CD_M = @CUST_CD_M                                                    " & vbNewLine _
                  & "                        --AND IDO1.IDO_DATE < @DATE_FROM                                                     " & vbNewLine _
                  & "                         --                                                                                  " & vbNewLine _
                  & "                        --移動前                                                                           " & vbNewLine _
                  & "                        --UNION ALL                                                                          " & vbNewLine _
                  & "                        --SELECT                                                                             " & vbNewLine _
                  & "                        -- ''                                          AS OUTKA_NO_L                         " & vbNewLine _
                  & "                        --,''                                          AS OUTKA_NO_M                         " & vbNewLine _
                  & "                        --,''                                          AS OUTKA_NO_S                         " & vbNewLine _
                  & "                        --,ZAI2.NRS_BR_CD                              AS NRS_BR_CD                          " & vbNewLine _
                  & "                        --,ZAI2.CUST_CD_L                              AS CUST_CD_L                          " & vbNewLine _
                  & "                        --,ZAI2.CUST_CD_M                              AS CUST_CD_M                          " & vbNewLine _
                  & "                        --,IDO2.O_ZAI_REC_NO                           AS ZAI_REC_NO                         " & vbNewLine _
                  & "                        --,''                                          AS REMARK_OUT                         " & vbNewLine _
                  & "                        --,ZAI2.GOODS_CD_NRS                           AS GOODS_CD_NRS                       " & vbNewLine _
                  & "                        --,ISNULL(ZAI2.LOT_NO, '')                     AS LOT_NO                             " & vbNewLine _
                  & "                        --,IDO2.N_PORA_ZAI_NB * -1                     AS PORA_ZAI_NB                        " & vbNewLine _
                  & "                        --,IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1   AS PORA_ZAI_QT                        " & vbNewLine _
                  & "                        --FROM                                                                               " & vbNewLine _
                  & "                        --$LM_TRN$..D_IDO_TRS IDO2                                                           " & vbNewLine _
                  & "                        --LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI2                                                 " & vbNewLine _
                  & "                        --ON  ZAI2.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        --AND ZAI2.NRS_BR_CD = IDO2.NRS_BR_CD                                                " & vbNewLine _
                  & "                        --AND ZAI2.ZAI_REC_NO = IDO2.O_ZAI_REC_NO                                            " & vbNewLine _
                  & "                        --WHERE                                                                              " & vbNewLine _
                  & "                        --    IDO2.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                  & "                        --AND IDO2.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                  & "                        --AND ZAI2.CUST_CD_L = @CUST_CD_L                                                    " & vbNewLine _
                  & "                        --AND ZAI2.CUST_CD_M = @CUST_CD_M                                                    " & vbNewLine _
                  & "                        --AND IDO2.IDO_DATE < @DATE_FROM                                                     " & vbNewLine _
                  & "                                                                                                           " & vbNewLine _
                  & "                        --出荷データ(C_OUTKA_S)                                                            " & vbNewLine _
                  & "                        UNION ALL                                                                          " & vbNewLine _
                  & "                        SELECT                                                                     " & vbNewLine _
                  & "                         OUTKA_NO_L                                                                        " & vbNewLine _
                  & "                        ,OUTKA_NO_M                                                                        " & vbNewLine _
                  & "                        ,OUTKA_NO_S                                                                        " & vbNewLine _
                  & "                        ,NRS_BR_CD                                                                         " & vbNewLine _
                  & "                        ,CUST_CD_L                                                                         " & vbNewLine _
                  & "                        ,CUST_CD_M                                                                         " & vbNewLine _
                  & "                        ,ZAI_REC_NO                                                                        " & vbNewLine _
                  & "                        ,REMARK_OUT                                                                        " & vbNewLine _
                  & "                        ,GOODS_CD_NRS                                                                      " & vbNewLine _
                  & "                        ,LOT_NO                                                                            " & vbNewLine _
                  & "                        ,PORA_ZAI_NB                                                                       " & vbNewLine _
                  & "                        ,PORA_ZAI_QT                                                                       " & vbNewLine _
                  & "                        FROM                                                                               " & vbNewLine _
                  & "                               (SELECT                                                                     " & vbNewLine _
                  & "                                 OUTS.OUTKA_NO_L                   AS OUTKA_NO_L                           " & vbNewLine _
                  & "                                ,OUTS.OUTKA_NO_M                   AS OUTKA_NO_M                           " & vbNewLine _
                  & "                                ,OUTS.OUTKA_NO_S                   AS OUTKA_NO_S                           " & vbNewLine _
                  & "                                ,OUTL.NRS_BR_CD                    AS NRS_BR_CD                            " & vbNewLine _
                  & "                                ,OUTL.CUST_CD_L                    AS CUST_CD_L                            " & vbNewLine _
                  & "                                ,OUTL.CUST_CD_M                    AS CUST_CD_M                            " & vbNewLine _
                  & "                                ,OUTS.ZAI_REC_NO                   AS ZAI_REC_NO                           " & vbNewLine _
                  & "                                ,''                                AS REMARK_OUT                           " & vbNewLine _
                  & "                                ,OUTM.GOODS_CD_NRS                 AS GOODS_CD_NRS                         " & vbNewLine _
                  & "                                ,ISNULL(OUTS.LOT_NO, '')           AS LOT_NO                               " & vbNewLine _
                  & "                                ,OUTS.ALCTD_NB * -1                AS PORA_ZAI_NB                          " & vbNewLine _
                  & "                                ,OUTS.ALCTD_QT * -1                AS PORA_ZAI_QT                          " & vbNewLine _
                  & "                                FROM                                                                       " & vbNewLine _
                  & "                                $LM_TRN$..C_OUTKA_L OUTL                                                   " & vbNewLine _
                  & "                                LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                         " & vbNewLine _
                  & "                                ON  OUTM.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                  & "                                AND OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                  & "                                AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                      " & vbNewLine _
                  & "                                LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                         " & vbNewLine _
                  & "                                ON  OUTS.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                  & "                                AND OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                  & "                                AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                      " & vbNewLine _
                  & "                                AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                      " & vbNewLine _
                  & "                                WHERE                                                                      " & vbNewLine _
                  & "                                    OUTL.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                  & "                                AND OUTM.ALCTD_KB <> '04'                                                  " & vbNewLine _
                  & "                                AND OUTL.OUTKA_STATE_KB !< '60'                                            " & vbNewLine _
                  & "                                AND OUTL.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                  & "                                AND OUTL.CUST_CD_L = @CUST_CD_L                                            " & vbNewLine _
                  & "                                AND OUTL.CUST_CD_M = @CUST_CD_M                                            " & vbNewLine _
                  & "                                AND OUTL.OUTKA_PLAN_DATE < @DATE_FROM                                      " & vbNewLine _
                  & "                               ) OUT                                                                       " & vbNewLine _
                  & "                       ) RIREKI                                                                            " & vbNewLine _
                  & "                WHERE                                                                                      " & vbNewLine _
                  & "                    CUST_CD_L = @CUST_CD_L                                                                 " & vbNewLine _
                  & "                AND CUST_CD_M = @CUST_CD_M                                                                 " & vbNewLine _
                  & "                GROUP BY                                                                                   " & vbNewLine _
                  & "                 NRS_BR_CD                                                                                 " & vbNewLine _
                  & "                ,CUST_CD_L                                                                                 " & vbNewLine _
                  & "                ,CUST_CD_M                                                                                 " & vbNewLine _
                  & "                ,ZAI_REC_NO                                                                                " & vbNewLine _
                  & "                ,GOODS_CD_NRS                                                                              " & vbNewLine _
                  & "                ,LOT_NO                                                                                    " & vbNewLine _
                  & "               ) ZAN                                                                                       " & vbNewLine _
                  & "        GROUP BY                                                                                           " & vbNewLine _
                  & "         ZAN.GOODS_CD_NRS                                                                                  " & vbNewLine _
                  & "        ,ZAN.LOT_NO                                                                                        " & vbNewLine _
                  & "       ) BASE                                                                                              " & vbNewLine _
                  & "WHERE                                                                                                      " & vbNewLine _
                  & "BASE.INKA_QT > 0                                                                                           " & vbNewLine _
                  & "                                                                                                           " & vbNewLine
            End If
        End If
        SQL_SELECT = SQL_SELECT & vbNewLine _
                      & "                                                                                                           " & vbNewLine _
                      & ") MAIN                                                                                                     " & vbNewLine _
                      & "--商品マスタ                                                                                               " & vbNewLine _
                      & "LEFT JOIN $LM_MST$..M_GOODS MGOODS                                                                         " & vbNewLine _
                      & "ON  MGOODS.NRS_BR_CD = MAIN.NRS_BR_CD                                                                      " & vbNewLine _
                      & "AND MGOODS.GOODS_CD_NRS = MAIN.GOODS_CD_NRS                                                                " & vbNewLine _
                      & "--荷主マスタ                                                                                               " & vbNewLine _
                      & "LEFT JOIN $LM_MST$..M_CUST MCUST                                                                           " & vbNewLine _
                      & "ON  MCUST.NRS_BR_CD = MGOODS.NRS_BR_CD                                                                     " & vbNewLine _
                      & "AND MCUST.CUST_CD_L = MGOODS.CUST_CD_L                                                                     " & vbNewLine _
                      & "AND MCUST.CUST_CD_M = MGOODS.CUST_CD_M                                                                     " & vbNewLine _
                      & "AND MCUST.CUST_CD_S = MGOODS.CUST_CD_S                                                                     " & vbNewLine _
                      & "AND MCUST.CUST_CD_SS = MGOODS.CUST_CD_SS                                                                   " & vbNewLine _
                      & "--荷主帳票パターン取得                                                                                     " & vbNewLine _
                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                        " & vbNewLine _
                      & "ON  MAIN.NRS_BR_CD = MCR1.NRS_BR_CD                                                                        " & vbNewLine _
                      & "AND MAIN.CUST_CD_L = MCR1.CUST_CD_L                                                                        " & vbNewLine _
                      & "AND MAIN.CUST_CD_M = MCR1.CUST_CD_M                                                                        " & vbNewLine _
                      & "AND '00' = MCR1.CUST_CD_S                                                                                  " & vbNewLine _
                      & "AND MCR1.PTN_ID = '33'                                                                                     " & vbNewLine _
                      & "--帳票パターン取得                                                                                         " & vbNewLine _
                      & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                              " & vbNewLine _
                      & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                         " & vbNewLine _
                      & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                               " & vbNewLine _
                      & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                               " & vbNewLine _
                      & "AND MR1.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine _
                      & "--商品Mの荷主での荷主帳票パターン取得                                                                      " & vbNewLine _
                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                        " & vbNewLine _
                      & "ON  MGOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                                                      " & vbNewLine _
                      & "AND MGOODS.CUST_CD_L = MCR2.CUST_CD_L                                                                      " & vbNewLine _
                      & "AND MGOODS.CUST_CD_M = MCR2.CUST_CD_M                                                                      " & vbNewLine _
                      & "AND MGOODS.CUST_CD_S = MCR2.CUST_CD_S                                                                      " & vbNewLine _
                      & "AND MCR2.PTN_ID = '33'                                                                                     " & vbNewLine _
                      & "--帳票パターン取得                                                                                         " & vbNewLine _
                      & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                              " & vbNewLine _
                      & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                         " & vbNewLine _
                      & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                               " & vbNewLine _
                      & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                               " & vbNewLine _
                      & "AND MR2.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine _
                      & "--存在しない場合の帳票パターン取得                                                                         " & vbNewLine _
                      & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                              " & vbNewLine _
                      & "ON  MR3.NRS_BR_CD = MAIN.NRS_BR_CD                                                                         " & vbNewLine _
                      & "AND MR3.PTN_ID = '33'                                                                                      " & vbNewLine _
                      & "AND MR3.STANDARD_FLAG = '01'                                                                               " & vbNewLine _
                      & "AND MR3.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine _
                      & "ORDER BY                                                                                                   " & vbNewLine _
                      & " MGOODS.GOODS_CD_CUST                                                                                      " & vbNewLine _
                      & ",MAIN.INOUTKA_DATE                                                                                         " & vbNewLine _
                      & ",MAIN.LOT_NO                                                                                               " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMD590IN")
        'IN情報の出力日付を取得
        Dim dateFrom As String = inTbl.Rows(0).Item("DATE_FROM").ToString()
        Dim dateTo As String = inTbl.Rows(0).Item("DATE_TO").ToString()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.CreateSqlMprt(dateFrom, dateTo)             'SQL文作成
        Me._StrSql.Append(SQL_MPRT)                         'SQL構築
        Call Me.SetConditionMasterSQL(sqlMprt)              '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD590DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMD590IN")
        'IN情報の出力日付を取得
        Dim dateFrom As String = inTbl.Rows(0).Item("DATE_FROM").ToString()
        Dim dateTo As String = inTbl.Rows(0).Item("DATE_TO").ToString()
        '出力帳票種別を取得
        Dim rptId As String = ds.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.CreateSqlSelect(dateFrom, dateTo, rptId)        'SQL文作成
        Me._StrSql.Append(SQL_SELECT)                           'SQL構築
        Call Me.SetConditionMasterSQL(sqlOut)                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD590DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("DATE_FROM", "DATE_FROM")
        map.Add("DATE_TO", "DATE_TO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("INKA_NB", "INKA_NB")
        map.Add("INKA_QT", "INKA_QT")
        map.Add("OUTKA_NB", "OUTKA_NB")
        map.Add("OUTKA_QT", "OUTKA_QT")
        map.Add("NB_UT", "NB_UT")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD590OUT")

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

            '出力日付FROM
            whereStr = .Item("DATE_FROM").ToString()
            '印刷データ取得時の場合は、常に設定
            If String.IsNullOrEmpty(whereStr) = False OrElse sqlType = sqlOut Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))

                '印刷データ取得時かつ出力日付FROMが空でない場合、在庫残数データのINOUTKA_DATEに前日の日付を設定する
                If sqlType = sqlOut AndAlso String.IsNullOrEmpty(whereStr) = False Then
                    '出力日付FROMの前日の日付を取得
                    Dim preDate As String = Convert.ToDateTime(Date.Parse(Format(Convert.ToInt32(whereStr), "0000/00/00"))).AddDays(-1).ToString("yyyyMMdd")
                    '在庫残数データのINOUTKA_DATEには出力日付FROMの前日の日付を設定
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRE_DATE_FROM", preDate, DBDataType.CHAR))
                End If

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
