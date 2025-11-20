' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH590    : EDI荷札(千葉・東レダウ)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH590DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH590DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       EDI_L.NRS_BR_CD                                  AS NRS_BR_CD " & vbNewLine _
                                            & "      , '10'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 荷札_東レダウ印刷データ抽出用 MAIN句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MAIN As String = " SELECT                                                                      " & vbNewLine _
                                     & " MAIN.RPT_ID                                                                 " & vbNewLine _
                                     & ",MAIN.NRS_BR_CD                                                              " & vbNewLine _
                                     & ",MAIN.DEST_NM                                                                " & vbNewLine _
                                     & ",SUM(MAIN.OUTKA_PKG_NB)  AS OUTKA_PKG_NB                                     " & vbNewLine _
                                     & ",MAIN.AD_1                                                                   " & vbNewLine _
                                     & ",MAIN.AD_2                                                                   " & vbNewLine _
                                     & ",MAIN.AD_3                                                                   " & vbNewLine _
                                     & ",MAIN.CUST_NM_L                                                              " & vbNewLine _
                                     & ",MAIN.UNSOCO_NM                                                              " & vbNewLine _
                                     & ",MAIN.ARR_PLAN_DATE                                                          " & vbNewLine _
                                     & ",MAIN.ARR_PLAN_TIME                                                          " & vbNewLine _
                                     & ",MAIN.NRS_BR_NM                                                              " & vbNewLine _
                                     & ",MAIN.OUTKA_NO_L                                                             " & vbNewLine _
                                     & ",MAIN.TEL                                                                    " & vbNewLine _
                                     & ",MAIN.FAX                                                                    " & vbNewLine _
                                     & ",MAIN.TOP_FLAG                                                               " & vbNewLine _
                                     & ",MAIN.CUST_NM_S                                                              " & vbNewLine _
                                     & ",MAIN.PC_KB                                                                  " & vbNewLine _
                                     & ",MAIN.CUST_CD_L                                                              " & vbNewLine _
                                     & ",MAIN.SHIP_NM_L                                                              " & vbNewLine _
                                     & ",MAIN.ATSUKAISYA_NM                                                          " & vbNewLine _
                                     & ",''                       AS GOODS_CD_CUST                                   " & vbNewLine _
                                     & ",''                       AS GOODS_NM_1                                      " & vbNewLine _
                                     & ",''                       AS GOODS_NM_2                                      " & vbNewLine _
                                     & ",''                       AS GOODS_NM_3                                      " & vbNewLine _
                                     & ",MAIN.LT_DATE                                                                " & vbNewLine _
                                     & ",''                       AS LOT_NO                                          " & vbNewLine _
                                     & ",MAIN.NRS_BR_AD_1                                                            " & vbNewLine _
                                     & ",MAIN.NRS_BR_AD_2                                                            " & vbNewLine _
                                     & ",MAIN.NRS_BR_AD_3                                                            " & vbNewLine _
                                     & ",MAIN.GOODS_SYUBETU                                                          " & vbNewLine _
                                     & ",MAIN.SET_NAIYO                                                              " & vbNewLine _
                                     & ",MAIN.SET_NAIYO_2                                                            " & vbNewLine _
                                     & ",MAIN.SET_NAIYO_3                                                            " & vbNewLine _
                                     & ",MAIN.DEST_TEL                                                               " & vbNewLine _
                                     & ",MAIN.ALCTD_NB                                                               " & vbNewLine _
                                     & ",MAIN.PRINT_SORT                                                             " & vbNewLine _
                                     & ",MAIN.UNSO_TTL_NB                                                            " & vbNewLine _
                                     & " FROM (                                                                      " & vbNewLine


    ''' <summary>
    ''' 荷札_東レダウ印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                                            " & vbNewLine _
                                        & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                 " & vbNewLine _
                                        & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                 " & vbNewLine _
                                        & "      ELSE MR3.RPT_ID                                                             " & vbNewLine _
                                        & " END                                AS RPT_ID                                     " & vbNewLine _
                                        & ",TOR_HED.NRS_BR_CD                  AS NRS_BR_CD                                  " & vbNewLine _
                                        & ",ISNULL(TOR_HED.SHUKKA_NM,'')       AS DEST_NM                                    " & vbNewLine _
                                        & ",ISNULL(TOR_DTL.KOSU,0)             AS OUTKA_PKG_NB                               " & vbNewLine _
                                        & ",ISNULL(TOR_HED.SHUKKA_JUSHO_1,'')  AS AD_1                                       " & vbNewLine _
                                        & ",ISNULL(TOR_HED.SHUKKA_JUSHO_2,'')  AS AD_2                                       " & vbNewLine _
                                        & ",ISNULL(TOR_HED.SHUKKA_JUSHO_3,'')  AS AD_3                                       " & vbNewLine _
                                        & ",CST.CUST_NM_L                      AS CUST_NM_L                                  " & vbNewLine _
                                        & ",''                                 AS UNSOCO_NM                                  " & vbNewLine _
                                        & ",TOR_HED.NOUKI                      AS ARR_PLAN_DATE                              " & vbNewLine _
                                        & ",''                                 AS ARR_PLAN_TIME                              " & vbNewLine _
                                        & ",CASE WHEN MCD.SET_NAIYO <> NULL OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO        " & vbNewLine _
                                        & "      ELSE '' END                   AS NRS_BR_NM                                  " & vbNewLine _
                                        & ",ISNULL(TOR_HED.OUTKA_CTL_NO,'')    AS OUTKA_NO_L                                 " & vbNewLine _
                                        & ",''                                 AS TEL                                        " & vbNewLine _
                                        & ",''                                 AS FAX                                        " & vbNewLine _
                                        & ",''                                 AS TOP_FLAG                                   " & vbNewLine _
                                        & ",''                                 AS CUST_NM_S                                  " & vbNewLine _
                                        & ",''                                 AS PC_KB                                      " & vbNewLine _
                                        & ",ISNULL(TOR_HED.CUST_CD_L,'')       AS CUST_CD_L                                  " & vbNewLine _
                                        & ",ISNULL(TOR_HED.SHUKKA_CD,'')       AS SHIP_NM_L                                  " & vbNewLine _
                                        & ",''                                 AS ATSUKAISYA_NM                              " & vbNewLine _
                                        & ",MG.GOODS_CD_CUST                   AS GOODS_CD_CUST                              " & vbNewLine _
                                        & ",MG.GOODS_NM_1                      AS GOODS_NM_1                                 " & vbNewLine _
                                        & ",MG.GOODS_NM_2                      AS GOODS_NM_2                                 " & vbNewLine _
                                        & ",MG.GOODS_NM_3                      AS GOODS_NM_3                                 " & vbNewLine _
                                        & ",ISNULL(TOR_HED.EDI_CTL_NO,'')      AS LT_DATE                                    " & vbNewLine _
                                        & ",EDI_M.LOT_NO                       AS LOT_NO                                     " & vbNewLine _
                                        & ",SO.AD_1                            AS NRS_BR_AD_1                                " & vbNewLine _
                                        & ",SO.AD_2                            AS NRS_BR_AD_2                                " & vbNewLine _
                                        & ",SO.AD_3                            AS NRS_BR_AD_3                                " & vbNewLine _
                                        & ",''                                 AS GOODS_SYUBETU                              " & vbNewLine _
                                        & ",ISNULL(TOR_HED.SHUKKA_NO,'')       AS SET_NAIYO                                  " & vbNewLine _
                                        & ",CASE WHEN SUBMST.SHIWAKE IS NULL THEN ''                                         " & vbNewLine _
                                        & "      WHEN SUBMST.ZMCNT > 1       THEN ''                                         " & vbNewLine _
                                        & "      WHEN SUBMST.ZMCNT = 1       THEN SUBMST.SHIWAKE                             " & vbNewLine _
                                        & "	  ELSE  '' END                  AS SET_NAIYO_2                                   " & vbNewLine _
                                        & ",CASE WHEN SUBMST.CHAKU IS NULL THEN ''                                           " & vbNewLine _
                                        & "      WHEN SUBMST.ZMCNT > 1       THEN ''                                         " & vbNewLine _
                                        & "      WHEN SUBMST.ZMCNT = 1       THEN SUBMST.CHAKU                               " & vbNewLine _
                                        & "	  ELSE  '' END                  AS SET_NAIYO_3                                   " & vbNewLine _
                                        & ",''                                 AS DEST_TEL                                   " & vbNewLine _
                                        & ",''                                 AS ALCTD_NB                                   " & vbNewLine _
                                        & ",''                                 AS PRINT_SORT                                 " & vbNewLine _
                                        & ",''                                 AS UNSO_TTL_NB                                " & vbNewLine
    ''' <summary>
    ''' 荷札_東レダウ印刷データ抽出用 FROM句
    ''' </summary>
    Private Const SQL_FROM As String = "  　FROM 　　　　　　　　　　　　　　　　　　　　　　　                              " & vbNewLine _
                                        & "--EDI出荷(大)                                                                     " & vbNewLine _
                                        & "  $LM_TRN$..H_OUTKAEDI_L EDI_L                                                    " & vbNewLine _
                                        & " LEFT OUTER JOIN                                                                  " & vbNewLine _
                                        & "--EDI出荷(中)                                                                     " & vbNewLine _
                                        & "  $LM_TRN$..H_OUTKAEDI_M EDI_M                                                    " & vbNewLine _
                                        & "ON  EDI_L.NRS_BR_CD   = EDI_M.NRS_BR_CD                                           " & vbNewLine _
                                        & "AND EDI_L.EDI_CTL_NO  = EDI_M.EDI_CTL_NO                                          " & vbNewLine _
                                        & "AND EDI_M.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                                        & " LEFT OUTER JOIN                                                                  " & vbNewLine _
                                        & "--出荷(大)                                                                        " & vbNewLine _
                                        & "  $LM_TRN$..C_OUTKA_L OUT_L                                                       " & vbNewLine _
                                        & "ON  EDI_L.NRS_BR_CD    = OUT_L.NRS_BR_CD                                          " & vbNewLine _
                                        & "AND EDI_L.OUTKA_CTL_NO = OUT_L.OUTKA_NO_L                                         " & vbNewLine _
                                        & "AND OUT_L.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                        & " LEFT OUTER JOIN                                                                  " & vbNewLine _
                                        & "--運送(大)                                                                        " & vbNewLine _
                                        & "  $LM_TRN$..F_UNSO_L UNSO_L                                                       " & vbNewLine _
                                        & "ON  OUT_L.NRS_BR_CD     = UNSO_L.NRS_BR_CD                                        " & vbNewLine _
                                        & "AND OUT_L.OUTKA_NO_L    = UNSO_L.INOUTKA_NO_L                                     " & vbNewLine _
                                        & "AND UNSO_L.MOTO_DATA_KB = '20'                                                    " & vbNewLine _
                                        & "AND UNSO_L.SYS_DEL_FLG  = '0'                                                     " & vbNewLine _
                                        & " LEFT OUTER JOIN                                                                  " & vbNewLine _
                                        & "--東レEDI受信(HED)                                                                " & vbNewLine _
                                        & "  (SELECT                                                                         " & vbNewLine _
                                        & "      NRS_BR_CD                                                                   " & vbNewLine _
                                        & "     ,EDI_CTL_NO                                                                  " & vbNewLine _
                                        & "		,OUTKA_CTL_NO                                                                " & vbNewLine _
                                        & "     ,CRT_DATE                                                                    " & vbNewLine _
                                        & "     ,FILE_NAME                                                                   " & vbNewLine _
                                        & "     ,REC_NO                                                                      " & vbNewLine _
                                        & "		,CUST_CD_L                                                                   " & vbNewLine _
                                        & "		,CUST_CD_M                                                                   " & vbNewLine _
                                        & "		,SHUKKA_NO                                                                   " & vbNewLine _
                                        & "		,SHUKKA_CD                                                                   " & vbNewLine _
                                        & "		,SHUKKA_NM                                                                   " & vbNewLine _
                                        & "		,SHUKKA_BI                                                                   " & vbNewLine _
                                        & "	    ,RTRIM(LTRIM(SHUKKA_JUSHO_1))   AS SHUKKA_JUSHO_1                            " & vbNewLine _
                                        & "      ,RTRIM(LTRIM(SHUKKA_JUSHO_2))   AS SHUKKA_JUSHO_2                           " & vbNewLine _
                                        & "		,SHUKKA_JUSHO_3                                                              " & vbNewLine _
                                        & "		,NOUKI                                                                       " & vbNewLine _
                                        & "		,SYS_DEL_FLG                                                                 " & vbNewLine _
                                        & "     FROM $LM_TRN$..H_OUTKAEDI_HED_TOR                                            " & vbNewLine _
                                        & "	 WHERE                                                                           " & vbNewLine _
                                        & "           DEL_KB = '0'                                                           " & vbNewLine _
                                        & "       AND SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "	 GROUP BY                                                                        " & vbNewLine _
                                        & "      NRS_BR_CD                                                                   " & vbNewLine _
                                        & "     ,EDI_CTL_NO                                                                  " & vbNewLine _
                                        & "		,OUTKA_CTL_NO                                                                " & vbNewLine _
                                        & "     ,CRT_DATE                                                                    " & vbNewLine _
                                        & "     ,FILE_NAME                                                                   " & vbNewLine _
                                        & "     ,REC_NO                                                                      " & vbNewLine _
                                        & "		,CUST_CD_L                                                                   " & vbNewLine _
                                        & "		,CUST_CD_M                                                                   " & vbNewLine _
                                        & "		,SHUKKA_NO                                                                   " & vbNewLine _
                                        & "		,SHUKKA_CD                                                                   " & vbNewLine _
                                        & "		,SHUKKA_NM                                                                   " & vbNewLine _
                                        & "		,SHUKKA_BI                                                                   " & vbNewLine _
                                        & "	    ,RTRIM(LTRIM(SHUKKA_JUSHO_1))                                                " & vbNewLine _
                                        & "     ,RTRIM(LTRIM(SHUKKA_JUSHO_2))                                                " & vbNewLine _
                                        & "		,SHUKKA_JUSHO_3                                                              " & vbNewLine _
                                        & "		,NOUKI                                                                       " & vbNewLine _
                                        & "		,SYS_DEL_FLG                                                                 " & vbNewLine _
                                        & "	 )TOR_HED                                                                        " & vbNewLine _
                                        & "ON  EDI_L.NRS_BR_CD = TOR_HED.NRS_BR_CD                                           " & vbNewLine _
                                        & "AND EDI_L.EDI_CTL_NO = TOR_HED.EDI_CTL_NO                                         " & vbNewLine _
                                        & " LEFT OUTER JOIN                                                                  " & vbNewLine _
                                        & "	(                                                                                " & vbNewLine _
                                        & "		SELECT                                                                       " & vbNewLine _
                                        & "			 SUB_SUBMST.KEN          AS KEN                                          " & vbNewLine _
                                        & "			,SUB_SUBMST.MACHI        AS MACHI                                        " & vbNewLine _
                                        & "			,SUB_SUBMST.SHIWAKE      AS SHIWAKE                                      " & vbNewLine _
                                        & "			,SUB_SUBMST.CHAKU        AS CHAKU                                        " & vbNewLine _
                                        & "			,SUB_SUBMST.ZMCNT        AS ZMCNT                                        " & vbNewLine _
                                        & "         FROM                                                                     " & vbNewLine _
                                        & "               (                                                                  " & vbNewLine _
                                        & "                SELECT                                                            " & vbNewLine _
                                        & "                      SEINO.KEN     AS KEN                                        " & vbNewLine _
                                        & "                     ,SEINO.MACHI   AS MACHI                                      " & vbNewLine _
                                        & "                     ,CASE WHEN COUNT(*) = 1 THEN MIN(SEINO.SHIWAKE)              " & vbNewLine _
                                        & "					       ELSE CASE WHEN COUNT(*) > 1 THEN '複数'                   " & vbNewLine _
                                        & "						        END                                                  " & vbNewLine _
                                        & "						   END AS SHIWAKE                                            " & vbNewLine _
                                        & "                     ,CASE WHEN COUNT(*) = 1 THEN MIN(SEINO.CHAKU)                " & vbNewLine _
                                        & "					       ELSE CASE WHEN COUNT(*) > 1 THEN '複数'                   " & vbNewLine _
                                        & "						        END                                                  " & vbNewLine _
                                        & "						   END AS CHAKU                                              " & vbNewLine _
                                        & "                     ,COUNT(*) AS ZMCNT                                           " & vbNewLine _
                                        & "                 FROM                                                             " & vbNewLine _
                                        & "				     (                                                               " & vbNewLine _
                                        & "					  SELECT                                                         " & vbNewLine _
                                        & "					        SUB_SEINO.KEN_K           AS KEN                         " & vbNewLine _
                                        & "					       ,SUB_SEINO.CITY_K          AS MACHI                       " & vbNewLine _
                                        & "					       ,SUB_SEINO.SHIWAKE_CD      AS SHIWAKE                     " & vbNewLine _
                                        & "					       ,SUB_SEINO.CHAKU_CD        AS CHAKU                       " & vbNewLine _
                                        & "					    FROM $LM_MST$..M_SEINO AS SUB_SEINO                          " & vbNewLine _
                                        & "					    WHERE                                                        " & vbNewLine _
                                        & "					         SUB_SEINO.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
                                        & "					     AND SUB_SEINO.CUST_CD_L = @CUST_CD_L                        " & vbNewLine _
                                        & "					     AND SUB_SEINO.CUST_CD_M = @CUST_CD_M                        " & vbNewLine _
                                        & "					     AND SUB_SEINO.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                        & "					GROUP BY                                                         " & vbNewLine _
                                        & "					        SUB_SEINO.KEN_K                                          " & vbNewLine _
                                        & "					       ,SUB_SEINO.CITY_K                                         " & vbNewLine _
                                        & "					       ,SUB_SEINO.SHIWAKE_CD                                     " & vbNewLine _
                                        & "					       ,SUB_SEINO.CHAKU_CD                                       " & vbNewLine _
                                        & "					 ) SEINO                                                         " & vbNewLine _
                                        & "                 GROUP BY                                                         " & vbNewLine _
                                        & "                         SEINO.KEN                                                " & vbNewLine _
                                        & "                        ,SEINO.MACHI                                              " & vbNewLine _
                                        & "                         HAVING COUNT(*) = 1                                      " & vbNewLine _
                                        & "                 ) SUB_SUBMST                                                     " & vbNewLine _
                                        & "		GROUP BY                                                                     " & vbNewLine _
                                        & "			     SUB_SUBMST.KEN                                                      " & vbNewLine _
                                        & "			    ,SUB_SUBMST.MACHI                                                    " & vbNewLine _
                                        & "			    ,SUB_SUBMST.SHIWAKE                                                  " & vbNewLine _
                                        & "			    ,SUB_SUBMST.CHAKU                                                    " & vbNewLine _
                                        & "			    ,SUB_SUBMST.ZMCNT                                                    " & vbNewLine _
                                        & "                 ) SUBMST                                                         " & vbNewLine _
                                        & "        ON  TOR_HED.SHUKKA_JUSHO_1 =  SUBMST.KEN                                  " & vbNewLine _
                                        & "       AND  TOR_HED.SHUKKA_JUSHO_2 LIKE  SUBMST.MACHI+'%'                         " & vbNewLine _
                                        & "--東レEDI受信(DTL)                                                                " & vbNewLine _
                                        & " LEFT OUTER JOIN                                                                  " & vbNewLine _
                                        & "               (SELECT                                                            " & vbNewLine _
                                        & "               	     NRS_BR_CD                                                   " & vbNewLine _
                                        & "                     ,CRT_DATE                                                    " & vbNewLine _
                                        & "                     ,FILE_NAME                                                   " & vbNewLine _
                                        & "                     ,REC_NO                                                      " & vbNewLine _
                                        & "               	    ,EDI_CTL_NO                                                  " & vbNewLine _
                                        & "               	    ,EDI_CTL_NO_CHU                                              " & vbNewLine _
                                        & "               	    ,SUM(KOSU) AS KOSU                                           " & vbNewLine _
                                        & "		                ,SYS_DEL_FLG                                                 " & vbNewLine _
                                        & "               	FROM                                                             " & vbNewLine _
                                        & "               	    $LM_TRN$..H_OUTKAEDI_DTL_TOR                                 " & vbNewLine _
                                        & "               	WHERE                                                            " & vbNewLine _
                                        & "               	    DEL_KB = '0'                                                 " & vbNewLine _
                                        & "               	    AND SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                        & "               	    AND NRS_BR_CD    = @NRS_BR_CD                                " & vbNewLine _
                                        & "               	GROUP BY                                                         " & vbNewLine _
                                        & "                      NRS_BR_CD                                                   " & vbNewLine _
                                        & "                     ,CRT_DATE                                                    " & vbNewLine _
                                        & "                     ,FILE_NAME                                                   " & vbNewLine _
                                        & "                     ,REC_NO                                                      " & vbNewLine _
                                        & "               	    ,EDI_CTL_NO                                                  " & vbNewLine _
                                        & "               	    ,EDI_CTL_NO_CHU                                              " & vbNewLine _
                                        & "		                ,SYS_DEL_FLG                                                 " & vbNewLine _
                                        & "                   )                                                              " & vbNewLine _
                                        & "                 AS TOR_DTL                                                       " & vbNewLine _
                                        & "        ON TOR_HED.CRT_DATE = TOR_DTL.CRT_DATE                                    " & vbNewLine _
                                        & "       AND TOR_HED.FILE_NAME = TOR_DTL.FILE_NAME                                  " & vbNewLine _
                                        & "       AND TOR_HED.REC_NO = TOR_DTL.REC_NO                                        " & vbNewLine _
                                        & "--EDILでの荷主帳票パターン取得                                                    " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_CUST_RPT MCR1                                         " & vbNewLine _
                                        & "ON  EDI_L.NRS_BR_CD = MCR1.NRS_BR_CD                                              " & vbNewLine _
                                        & "AND EDI_L.CUST_CD_L = MCR1.CUST_CD_L                                              " & vbNewLine _
                                        & "AND EDI_L.CUST_CD_M = MCR1.CUST_CD_M                                              " & vbNewLine _
                                        & "AND '00' = MCR1.CUST_CD_S                                                         " & vbNewLine _
                                        & "AND MCR1.PTN_ID = '10'                                                            " & vbNewLine _
                                        & "AND MCR1.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                        & "--商品マスタ                                                                      " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_GOODS MG                                              " & vbNewLine _
                                        & "ON  MG.NRS_BR_CD = EDI_M.NRS_BR_CD                                                " & vbNewLine _
                                        & "AND MG.GOODS_CD_NRS = EDI_M.NRS_GOODS_CD                                          " & vbNewLine _
                                        & "AND MG.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                        & "--帳票パターン取得                                                                " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_RPT MR1                                               " & vbNewLine _
                                        & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                " & vbNewLine _
                                        & "AND MR1.PTN_ID = MCR1.PTN_ID                                                      " & vbNewLine _
                                        & "AND MR1.PTN_CD = MCR1.PTN_CD                                                      " & vbNewLine _
                                        & "AND MR1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                        & "--商品Mの荷主での荷主帳票パターン取得                                             " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_CUST_RPT MCR2                                         " & vbNewLine _
                                        & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                 " & vbNewLine _
                                        & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                 " & vbNewLine _
                                        & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                 " & vbNewLine _
                                        & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                 " & vbNewLine _
                                        & "AND MCR2.PTN_ID = '10'                                                            " & vbNewLine _
                                        & "AND MCR2.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                        & "--帳票パターン取得                                                                " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_RPT MR2                                               " & vbNewLine _
                                        & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                " & vbNewLine _
                                        & "AND MR2.PTN_ID = MCR2.PTN_ID                                                      " & vbNewLine _
                                        & "AND MR2.PTN_CD = MCR2.PTN_CD                                                      " & vbNewLine _
                                        & "AND MR2.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                        & "--存在しない場合の帳票パターン取得                                                " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_RPT MR3                                               " & vbNewLine _
                                        & "ON  MR3.NRS_BR_CD = TOR_HED.NRS_BR_CD                                             " & vbNewLine _
                                        & "AND MR3.PTN_ID = '10'                                                             " & vbNewLine _
                                        & "AND MR3.STANDARD_FLAG = '01'                                                      " & vbNewLine _
                                        & "AND MR3.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                        & "--荷主マスタ                                                                      " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_CUST CST                                              " & vbNewLine _
                                        & "ON  CST.NRS_BR_CD = MG.NRS_BR_CD                                                  " & vbNewLine _
                                        & "AND CST.CUST_CD_L = MG.CUST_CD_L                                                  " & vbNewLine _
                                        & "AND CST.CUST_CD_M = MG.CUST_CD_M                                                  " & vbNewLine _
                                        & "AND CST.CUST_CD_S = MG.CUST_CD_S                                                  " & vbNewLine _
                                        & "AND CST.CUST_CD_SS = MG.CUST_CD_SS                                                " & vbNewLine _
                                        & "--荷主明細マスタ                                                                  " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_CUST_DETAILS MCD                                      " & vbNewLine _
                                        & "ON  MCD.NRS_BR_CD = TOR_HED.NRS_BR_CD                                             " & vbNewLine _
                                        & "AND MCD.CUST_CD = TOR_HED.CUST_CD_L                                               " & vbNewLine _
                                        & "AND MCD.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                        & "AND MCD.SUB_KB = '94'                                                             " & vbNewLine _
                                        & "AND MCD.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                        & "--届先マスタ                                                                      " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_DEST DST                                              " & vbNewLine _
                                        & "ON  DST.NRS_BR_CD = TOR_HED.NRS_BR_CD                                             " & vbNewLine _
                                        & "AND DST.CUST_CD_L = TOR_HED.CUST_CD_L                                             " & vbNewLine _
                                        & "AND DST.DEST_CD   = TOR_HED.SHUKKA_CD                                             " & vbNewLine _
                                        & "--営業所マスタ                                                                    " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_NRS_BR NB                                             " & vbNewLine _
                                        & "ON  NB.NRS_BR_CD = TOR_HED.NRS_BR_CD                                              " & vbNewLine _
                                        & "--倉庫マスタ                                                                      " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_MST$..M_SOKO SO                                               " & vbNewLine _
                                        & "ON  SO.WH_CD = EDI_L.WH_CD                                                        " & vbNewLine _
                                        & "                                                                                  " & vbNewLine _
                                        & "-- EDI印刷種別テーブル                                                            " & vbNewLine _
                                        & "LEFT OUTER JOIN $LM_TRN$..H_EDI_PRINT EDI_PRT                                     " & vbNewLine _
                                        & "       ON EDI_PRT.NRS_BR_CD = TOR_HED.NRS_BR_CD                                   " & vbNewLine _
                                        & "      AND EDI_PRT.EDI_CTL_NO = TOR_HED.EDI_CTL_NO                                 " & vbNewLine _
                                        & "      AND EDI_PRT.INOUT_KB = '0'                                                  " & vbNewLine _
                                        & "LEFT JOIN (                                                                       " & vbNewLine _
                                        & "            SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                               " & vbNewLine _
                                        & "                 , H_EDI_PRINT.NRS_BR_CD                                          " & vbNewLine _
                                        & "                 , H_EDI_PRINT.EDI_CTL_NO                                         " & vbNewLine _
                                        & "                 , H_EDI_PRINT.DENPYO_NO                                          " & vbNewLine _
                                        & "              FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                              " & vbNewLine _
                                        & "             WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                           " & vbNewLine _
                                        & "               AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                           " & vbNewLine _
                                        & "               AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                           " & vbNewLine _
                                        & "               AND H_EDI_PRINT.PRINT_TP    = '15'                                 " & vbNewLine _
                                        & "               AND H_EDI_PRINT.INOUT_KB    = '0'                                  " & vbNewLine _
                                        & "               AND H_EDI_PRINT.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                        & "             GROUP BY                                                             " & vbNewLine _
                                        & "                   H_EDI_PRINT.NRS_BR_CD                                          " & vbNewLine _
                                        & "                 , H_EDI_PRINT.EDI_CTL_NO                                         " & vbNewLine _
                                        & "                 , H_EDI_PRINT.DENPYO_NO                                          " & vbNewLine _
                                        & "          ) HEDIPRINT                                                             " & vbNewLine _
                                        & "       ON HEDIPRINT.NRS_BR_CD  = TOR_HED.NRS_BR_CD                                " & vbNewLine _
                                        & "      AND HEDIPRINT.EDI_CTL_NO = TOR_HED.EDI_CTL_NO                               " & vbNewLine _
                                        & "      AND HEDIPRINT.DENPYO_NO  = TOR_HED.SHUKKA_NO                                " & vbNewLine

    ''' <summary>                                                                                                                 
    ''' データ抽出用GROUP BY句                                                                                                      
    ''' </summary>                                                                                                                
    ''' <remarks></remarks>                                                                                                       
    Private Const SQL_GROUP_BY As String = " GROUP BY                                                                        " & vbNewLine _
                                        & "       MR1.PTN_CD                                                                 " & vbNewLine _
                                        & "     , MR2.PTN_CD                                                                 " & vbNewLine _
                                        & "     , MR1.RPT_ID                                                                 " & vbNewLine _
                                        & "     , MR2.RPT_ID                                                                 " & vbNewLine _
                                        & "     , MR3.RPT_ID                                                                 " & vbNewLine _
                                        & "     , TOR_HED.NRS_BR_CD                                                          " & vbNewLine _
                                        & "     , TOR_HED.SHUKKA_NM                                                          " & vbNewLine _
                                        & "     , TOR_DTL.KOSU                                                               " & vbNewLine _
                                        & "     , TOR_HED.SHUKKA_JUSHO_1                                                     " & vbNewLine _
                                        & "     , TOR_HED.SHUKKA_JUSHO_2                                                     " & vbNewLine _
                                        & "     , TOR_HED.SHUKKA_JUSHO_3                                                     " & vbNewLine _
                                        & "     , CST.CUST_NM_L                                                              " & vbNewLine _
                                        & "     , TOR_HED.NOUKI                                                              " & vbNewLine _
                                        & "     , CASE WHEN MCD.SET_NAIYO <> NULL OR MCD.SET_NAIYO <> '' THEN MCD.SET_NAIYO  " & vbNewLine _
                                        & "            ELSE '' END                                                           " & vbNewLine _
                                        & "     , TOR_HED.OUTKA_CTL_NO                                                       " & vbNewLine _
                                        & "     , TOR_HED.CUST_CD_L                                                          " & vbNewLine _
                                        & "     , TOR_HED.SHUKKA_CD                                                          " & vbNewLine _
                                        & "     , MG.GOODS_CD_CUST                                                           " & vbNewLine _
                                        & "     , MG.GOODS_NM_1                                                              " & vbNewLine _
                                        & "     , MG.GOODS_NM_2                                                              " & vbNewLine _
                                        & "     , MG.GOODS_NM_3                                                              " & vbNewLine _
                                        & "     , TOR_HED.EDI_CTL_NO                                                         " & vbNewLine _
                                        & "     , EDI_M.LOT_NO                                                               " & vbNewLine _
                                        & "     , SO.AD_1                                                                    " & vbNewLine _
                                        & "     , SO.AD_2                                                                    " & vbNewLine _
                                        & "     , SO.AD_3                                                                    " & vbNewLine _
                                        & "     , TOR_HED.SHUKKA_NO                                                          " & vbNewLine _
                                        & "     ,CASE WHEN SUBMST.SHIWAKE IS NULL THEN ''                                    " & vbNewLine _
                                        & "           WHEN SUBMST.ZMCNT > 1       THEN ''                                    " & vbNewLine _
                                        & "           WHEN SUBMST.ZMCNT = 1       THEN SUBMST.SHIWAKE                        " & vbNewLine _
                                        & "	       ELSE  '' END                                                              " & vbNewLine _
                                        & "     ,CASE WHEN SUBMST.CHAKU IS NULL THEN ''                                      " & vbNewLine _
                                        & "           WHEN SUBMST.ZMCNT > 1       THEN ''                                    " & vbNewLine _
                                        & "           WHEN SUBMST.ZMCNT = 1       THEN SUBMST.CHAKU                          " & vbNewLine _
                                        & "	       ELSE  '' END                                                              " & vbNewLine

    ''' <summary>
    ''' 荷札_東レダウ印刷データ抽出用 MAIN句GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MAIN_GROUP_BY As String = " )MAIN                                                                      " & vbNewLine _
                                              & " GROUP BY                                                                    " & vbNewLine _
                                              & "  MAIN.RPT_ID                                                                " & vbNewLine _
                                              & ",MAIN.NRS_BR_CD                                                              " & vbNewLine _
                                              & ",MAIN.DEST_NM                                                                " & vbNewLine _
                                              & ",MAIN.AD_1                                                                   " & vbNewLine _
                                              & ",MAIN.AD_2                                                                   " & vbNewLine _
                                              & ",MAIN.AD_3                                                                   " & vbNewLine _
                                              & ",MAIN.CUST_NM_L                                                              " & vbNewLine _
                                              & ",MAIN.UNSOCO_NM                                                              " & vbNewLine _
                                              & ",MAIN.ARR_PLAN_DATE                                                          " & vbNewLine _
                                              & ",MAIN.ARR_PLAN_TIME                                                          " & vbNewLine _
                                              & ",MAIN.NRS_BR_NM                                                              " & vbNewLine _
                                              & ",MAIN.OUTKA_NO_L                                                             " & vbNewLine _
                                              & ",MAIN.TEL                                                                    " & vbNewLine _
                                              & ",MAIN.FAX                                                                    " & vbNewLine _
                                              & ",MAIN.TOP_FLAG                                                               " & vbNewLine _
                                              & ",MAIN.CUST_NM_S                                                              " & vbNewLine _
                                              & ",MAIN.PC_KB                                                                  " & vbNewLine _
                                              & ",MAIN.CUST_CD_L                                                              " & vbNewLine _
                                              & ",MAIN.SHIP_NM_L                                                              " & vbNewLine _
                                              & ",MAIN.ATSUKAISYA_NM                                                          " & vbNewLine _
                                              & ",MAIN.LT_DATE                                                                " & vbNewLine _
                                              & ",MAIN.NRS_BR_AD_1                                                            " & vbNewLine _
                                              & ",MAIN.NRS_BR_AD_2                                                            " & vbNewLine _
                                              & ",MAIN.NRS_BR_AD_3                                                            " & vbNewLine _
                                              & ",MAIN.GOODS_SYUBETU                                                          " & vbNewLine _
                                              & ",MAIN.SET_NAIYO                                                              " & vbNewLine _
                                              & ",MAIN.SET_NAIYO_2                                                            " & vbNewLine _
                                              & ",MAIN.SET_NAIYO_3                                                            " & vbNewLine _
                                              & ",MAIN.DEST_TEL                                                               " & vbNewLine _
                                              & ",MAIN.ALCTD_NB                                                               " & vbNewLine _
                                              & ",MAIN.PRINT_SORT                                                             " & vbNewLine _
                                              & ",MAIN.UNSO_TTL_NB                                                            " & vbNewLine


    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句(MAIN)           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                                                                        " & vbNewLine _
                                         & "       MAIN.SET_NAIYO                                                         " & vbNewLine


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
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrintPattern(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH590IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH590DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH590DAC.SQL_FROM)           'SQL構築(帳票種別用FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then   'SQL構築(印刷データ抽出用条件設定)
            Call Me.SetConditionMasterSQL_OUT()         '出力済
        Else
            Call Me.SetConditionMasterSQL()             '未出力・両方(出力済、未出力併せて)
        End If
        'パラメータ設定
        Call Me.SetConditionPrintPatternMSQL(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH590DAC", "SelectMPrt", cmd)

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
    ''' 日興産業EDI対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>日興産業EDI出荷 対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH590IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH590DAC.SQL_MAIN)           'SQL構築(印刷データ抽出用 MAIN句)
        Me._StrSql.Append(LMH590DAC.SQL_SELECT)         'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH590DAC.SQL_FROM)           'SQL構築(印刷データ抽出用 FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then   'SQL構築(印刷データ抽出条件)
            Call Me.SetConditionMasterSQL_OUT()         '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()             '未出力・両方(出力済、未出力併せて)
        End If

        'SQL構築(印刷データ抽出用条件設定)
        Me._StrSql.Append(LMH590DAC.SQL_GROUP_BY)       'SQL構築(帳票種別用GROUP BY句)
        Me._StrSql.Append(LMH590DAC.SQL_MAIN_GROUP_BY)       'SQL構築(帳票種別用GROUP BY句)
        Me._StrSql.Append(LMH590DAC.SQL_ORDER_BY)       'SQL構築(印刷データ抽出用 ORDER BY句)

        'パラメータ設定
        Call Me.SetConditionPrintPatternMSQL(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH590DAC", "SelectPrintData", cmd)

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
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")
        map.Add("SET_NAIYO_3", "SET_NAIYO_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH590OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL(ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", Me._Row.Item("INOUT_KB").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール(出力済)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_OUT()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" TOR_HED.NRS_BR_CD = @NRS_BR_CD")
            End If

            '伝票№(オーダー№)
            whereStr = .Item("DENPYO_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TOR_HED.SHUKKA_NO = @DENPYO_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", whereStr, DBDataType.NVARCHAR))
            End If

            'プリントフラグ
            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))

            'その他WHERE句条件
            Me._StrSql.Append(" AND TOR_HED.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND OUT_L.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND OUT_L.OUTKA_STATE_KB > '40' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND UNSO_L.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND EXISTS(SELECT ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" COUNT(KBN_U030.KBN_CD) ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" FROM $LM_MST$..Z_KBN KBN_U030 ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" KBN_U030.KBN_GROUP_CD = 'U030' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND KBN_U030.KBN_NM1 = UNSO_L.NRS_BR_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND KBN_U030.KBN_NM2 = UNSO_L.UNSO_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND KBN_U030.KBN_NM3 = UNSO_L.UNSO_BR_CD) ")

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" TOR_HED.NRS_BR_CD = @NRS_BR_CD")
            End If

            'EDI出荷予定日(FROM)
            whereStr = .Item("OUTKA_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TOR_HED.SHUKKA_BI >= @OUTKA_PLAN_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            'EDI出荷予定日(TO)
            whereStr = .Item("OUTKA_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TOR_HED.SHUKKA_BI <= @OUTKA_PLAN_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

            'プリントフラグ
            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))

            'その他WHERE句条件
            Me._StrSql.Append(" AND TOR_HED.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND OUT_L.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND OUT_L.OUTKA_STATE_KB > '40' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND UNSO_L.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND EXISTS(SELECT ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" COUNT(KBN_U030.KBN_CD) ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" FROM $LM_MST$..Z_KBN KBN_U030 ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" KBN_U030.KBN_GROUP_CD = 'U030' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND KBN_U030.KBN_NM1 = UNSO_L.NRS_BR_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND KBN_U030.KBN_NM2 = UNSO_L.UNSO_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND KBN_U030.KBN_NM3 = UNSO_L.UNSO_BR_CD) ")

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
