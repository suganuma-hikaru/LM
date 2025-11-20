' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷編集
'  プログラムID     :  LMC790DAC : 梱包明細
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC790DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC790DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "印刷種別"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	 CL.NRS_BR_CD                                         AS NRS_BR_CD  " & vbNewLine _
                                            & " ,'BR'                                                 AS PTN_ID     " & vbNewLine _
                                            & " ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                   " & vbNewLine _
                                            & "	      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                   " & vbNewLine _
                                            & "	   	  ELSE MR3.PTN_CD END                             AS PTN_CD     " & vbNewLine _
                                            & " ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                   " & vbNewLine _
                                            & "       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                   " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                             AS RPT_ID     " & vbNewLine


#End Region

#Region "SELECT句"

    ' ''' <summary>
    ' ''' 印刷データ抽出用 SELECT句
    ' ''' </summary>
    ' ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                              " & vbNewLine _
                              & "       MAIN.PTN_CD                                        AS PTN_CD                                   " & vbNewLine _
                              & "     , MAIN.RPT_ID                                        AS RPT_ID                                   " & vbNewLine _
                              & "     , MAIN.NRS_BR_CD                                     AS NRS_BR_CD                                " & vbNewLine _
                              & "     , MAIN.OUTKA_NO_L                                    AS OUTKA_NO_L                               " & vbNewLine _
                              & "     , MAIN.JYUCYU_NM                                     AS JYUCYU_NM                                " & vbNewLine _
                              & "     , MAIN.CRT_DATE                                      AS CRT_DATE                                 " & vbNewLine _
                              & "     , MAIN.CUST_ORD_NO                                   AS CUST_ORD_NO                              " & vbNewLine _
                              & "     , MAIN.ARR_PLAN_DATE                                 AS ARR_PLAN_DATE                            " & vbNewLine _
                              & "     , MAIN.DEST_NM                                       AS DEST_NM                                  " & vbNewLine _
                              & "     , MAIN.CASE_NO                                       AS CASE_NO                                  " & vbNewLine _
                              & "     , MAIN.GOODS_NM                                      AS GOODS_NM                                 " & vbNewLine _
                              & "     , MAIN.PKG_NB                                        AS PKG_NB                                   " & vbNewLine _
                              & "     , MAIN.PLT_PER_PKG_UT                                AS PLT_PER_PKG_UT                           " & vbNewLine _
                              & "	  , MAIN.LOT_NO_NB                                     AS LOT_NO_NB                                " & vbNewLine _
                              & "     , SUM(MAIN.ALCTD_NB)                                 AS ALCTD_NB                                 " & vbNewLine _
                              & "     , MAIN.MARK_INFO_1                                   AS MARK_INFO_1                              " & vbNewLine _
                              & "     , MAIN.MARK_INFO_2                                   AS MARK_INFO_2                              " & vbNewLine _
                              & "     , MAIN.MARK_INFO_3                                   AS MARK_INFO_3                              " & vbNewLine _
                              & "     , MAIN.MARK_INFO_4                                   AS MARK_INFO_4                              " & vbNewLine _
                              & "     , MAIN.CASE_NO_FROM                                  AS CASE_NO_FROM                             " & vbNewLine _
                              & "     , MAIN.CASE_NO_TO                                    AS CASE_NO_TO                               " & vbNewLine _
                              & "     , MAIN.CUST_NM_L                                     AS CUST_NM_L                                " & vbNewLine _
                              & "	 , MAIN.GOODS_CD_NRS                                   AS GOODS_CD_NRS                             " & vbNewLine _
                              & "                                                                                                      " & vbNewLine _
                              & "FROM                                                                                                  " & vbNewLine _
                              & "(                                                                                                     " & vbNewLine _
                              & "SELECT                                                                                                " & vbNewLine _
                              & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                                                  " & vbNewLine _
                              & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                                                     " & vbNewLine _
                              & "      ELSE MR3.PTN_CD END                                 AS PTN_CD                                   " & vbNewLine _
                              & "     , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                               " & vbNewLine _
                              & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                     " & vbNewLine _
                              & "      ELSE MR3.RPT_ID END                                 AS RPT_ID                                   " & vbNewLine _
                              & "     ,CL.NRS_BR_CD                                        AS NRS_BR_CD                                " & vbNewLine _
                              & "     ,CL.OUTKA_NO_L                                       AS OUTKA_NO_L                               " & vbNewLine _
                              & "     ,ISNULL(EL.FREE_C03,MDS.DEST_NM)                     AS JYUCYU_NM                                " & vbNewLine _
                              & "     ,ISNULL(EL.CRT_DATE,CL.SYS_ENT_DATE)                 AS CRT_DATE                                 " & vbNewLine _
                              & "     ,CL.CUST_ORD_NO                                      AS CUST_ORD_NO                              " & vbNewLine _
                              & "     ,CL.ARR_PLAN_DATE                                    AS ARR_PLAN_DATE                            " & vbNewLine _
                              & "     ,CL.DEST_NM                                          AS DEST_NM                                  " & vbNewLine _
                              & "     ,MARK_INFO.CASE_NO_FROM                              AS CASE_NO                                  " & vbNewLine _
                              & "     --商品名の変更[①EDIの商品名②届け先の特有名称③商品マスタ-優先度は①～③]                       " & vbNewLine _
                              & "     ,CASE                                                                                            " & vbNewLine _
                              & "       WHEN EM.OUTKA_CTL_NO Is Not Null                                                               " & vbNewLine _
                              & "        THEN EM.GOODS_NM                                                                              " & vbNewLine _
                              & "       WHEN MGDT.SET_NAIYO Is Not Null                                                                " & vbNewLine _
                              & "        THEN MGDT.SET_NAIYO                                                                           " & vbNewLine _
                              & "       ELSE MG.GOODS_NM_2                                                                             " & vbNewLine _
                              & "      END                                                 AS GOODS_NM                                 " & vbNewLine _
                              & "     ,MG.STD_IRIME_NB                                     AS PKG_NB                                   " & vbNewLine _
                              & "     ,MG.PLT_PER_PKG_UT                                   AS PLT_PER_PKG_UT                           " & vbNewLine _
                              & "		  --(2015.08.20)　協立化学 仕様変更START                                                       " & vbNewLine _
                              & "     ,CASE WHEN KBN_L006.KBN_CD IS NULL THEN (SELECT DISTINCT                                         " & vbNewLine _
                              & "         RTRIM(SUBCS.LOT_NO) + ' X '  + RTRIM(convert(char,SUBCS.ALCTD_NB)) + ','                     " & vbNewLine _
                              & "		  FROM $LM_TRN$..C_OUTKA_S SUBCS                                                               " & vbNewLine _
                              & "	   WHERE                                                                                           " & vbNewLine _
                              & "            SUBCS.NRS_BR_CD    = @NRS_BR_CD                                                           " & vbNewLine _
                              & "	   AND   SUBCS.OUTKA_NO_L   = @OUTKA_NO_L                                                          " & vbNewLine _
                              & "	   AND   SUBCS.SYS_DEL_FLG  = '0'                                                                  " & vbNewLine _
                              & "      AND   SUBCS.NRS_BR_CD = SUB_CS.NRS_BR_CD                                                        " & vbNewLine _
                              & "      AND   SUBCS.OUTKA_NO_L = SUB_CS.OUTKA_NO_L                                                      " & vbNewLine _
                              & "       FOR XML PATH('')                                                                               " & vbNewLine _
                              & "       ) ELSE '' END LOT_NO_NB                                                                        " & vbNewLine _
                              & "		  --(2015.08.20)　協立化学 仕様変更END                                                         " & vbNewLine _
                              & "     ,SUB_CS.ALCTD_NB                                     AS ALCTD_NB                                 " & vbNewLine _
                              & "     ,MARK_INFO.MARK_INFO_1                               AS MARK_INFO_1                              " & vbNewLine _
                              & "     ,MARK_INFO.MARK_INFO_2                               AS MARK_INFO_2                              " & vbNewLine _
                              & "     ,MARK_INFO.MARK_INFO_3                               AS MARK_INFO_3                              " & vbNewLine _
                              & "     ,MARK_INFO.MARK_INFO_4                               AS MARK_INFO_4                              " & vbNewLine _
                              & "     ,MARK_INFO.CASE_NO_FROM                              AS CASE_NO_FROM                             " & vbNewLine _
                              & "     ,MARK_INFO.CASE_NO_TO                                AS CASE_NO_TO                               " & vbNewLine _
                              & "     ,MC.CUST_NM_L                                        AS CUST_NM_L                                " & vbNewLine _
                              & "     ,CM.GOODS_CD_NRS                                     AS GOODS_CD_NRS                             " & vbNewLine _
                              & "                                                                                                      " & vbNewLine _
    '                          & "			 CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
    '                          & "				  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                      " & vbNewLine _
    '                          & "				  ELSE MR3.PTN_CD END                           AS PTN_CD          " & vbNewLine _
    '                          & "		   , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
    '                          & "				  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      " & vbNewLine _
    '                          & "				  ELSE MR3.RPT_ID END                           AS RPT_ID          " & vbNewLine _
    '                          & "		   ,CL.NRS_BR_CD                                        AS NRS_BR_CD       " & vbNewLine _
    '                          & "		   ,CL.OUTKA_NO_L                                       AS OUTKA_NO_L      " & vbNewLine _
    '                          & "		   ,ISNULL(EL.FREE_C03,CL.DENP_NO)                      AS JYUCYU_NM       " & vbNewLine _
    '                          & "		   ,ISNULL(EL.CRT_DATE,CL.SYS_ENT_DATE)                 AS CRT_DATE        " & vbNewLine _
    '                          & "		   ,CL.CUST_ORD_NO                                      AS CUST_ORD_NO     " & vbNewLine _
    '                          & "		   ,CL.ARR_PLAN_DATE                                    AS ARR_PLAN_DATE   " & vbNewLine _
    '                          & "		   ,CL.DEST_NM                                          AS DEST_NM         " & vbNewLine _
    '                          & "		   ,CMH.CASE_NO_FROM                                    AS CASE_NO         " & vbNewLine _
    '                          & "		   ,MG.GOODS_NM_1                                       AS GOODS_NM        " & vbNewLine _
    '                          & "		   ,MG.STD_IRIME_NB                                     AS PKG_NB          " & vbNewLine _
    '                          & "		   ,MG.PLT_PER_PKG_UT                                   AS PLT_PER_PKG_UT  " & vbNewLine _
    '                          & "		   ,SUB_CS.LOT_NO  + ' X ' + convert(char,SUB_CS.ALCTD_NB) AS LOT_NO_NB    " & vbNewLine _
    '                          & "		   ,SUB_CS.ALCTD_NB                                     AS ALCTD_NB        " & vbNewLine _
    '                          & "		   ,CMD_001.REMARK_INFO                                 AS MARK_INFO_1     " & vbNewLine _
    '                          & "		   ,CMD_002.REMARK_INFO                                 AS MARK_INFO_2     " & vbNewLine _
    '                          & "		   ,CMD_003.REMARK_INFO                                 AS MARK_INFO_3     " & vbNewLine _
    '                          & "		   ,CMD_004.REMARK_INFO                                 AS MARK_INFO_4     " & vbNewLine _
    '                          & "		   ,CMH.CASE_NO_FROM                                    AS CASE_NO_FROM    " & vbNewLine _
    '                          & "		   ,CMH.CASE_NO_TO                                      AS CASE_NO_TO      " & vbNewLine _
    '                          & "		   ,MC.CUST_NM_L                                        AS CUST_NM_L       " & vbNewLine _
    '                          & "		   ,CM.GOODS_CD_NRS                                     AS GOODS_CD_NRS    " & vbNewLine

#End Region

#Region "FROM句"

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                                                                                        " & vbNewLine _
                              & "		  $LM_TRN$..C_OUTKA_L CL                                                   " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "		  $LM_TRN$..H_OUTKAEDI_L EL                                                " & vbNewLine _
                              & "		  ON                                                                       " & vbNewLine _
                              & "		  CL.NRS_BR_CD = EL.NRS_BR_CD                                              " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CL.OUTKA_NO_L = EL.OUTKA_CTL_NO                                          " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  EL.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                              & "		  --(2015.08.20)　協立化学 仕様変更START                                   " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "		  $LM_MST$..Z_KBN KBN_L006                                                 " & vbNewLine _
                              & "		  ON                                                                       " & vbNewLine _
                              & "		  KBN_L006.KBN_GROUP_CD = 'L006'                                           " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  KBN_L006.KBN_NM1 = ISNULL(EL.NRS_BR_CD,CL.NRS_BR_CD)                     " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  KBN_L006.KBN_NM2 = ISNULL(EL.CUST_CD_L,CL.CUST_CD_L)                     " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  KBN_L006.KBN_NM3 = ISNULL(EL.CUST_CD_M,CL.CUST_CD_M)                     " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  KBN_L006.KBN_NM4 = ISNULL(EL.FREE_C02,CL.SHIP_CD_L)                      " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  KBN_L006.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                              & "		  --(2015.08.20)　協立化学 仕様変更END                                     " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "		  $LM_TRN$..C_OUTKA_M CM                                                   " & vbNewLine _
                              & "		  ON                                                                       " & vbNewLine _
                              & "		  CL.NRS_BR_CD = CM.NRS_BR_CD                                              " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CL.OUTKA_NO_L = CM.OUTKA_NO_L                                            " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "		  (                                                                        " & vbNewLine _
                              & "		   SELECT                                                                  " & vbNewLine _
                              & "				 NRS_BR_CD                                                         " & vbNewLine _
                              & "				,OUTKA_NO_L                                                        " & vbNewLine _
                              & "				,OUTKA_NO_M                                                        " & vbNewLine _
                              & "				,LOT_NO                                                            " & vbNewLine _
                              & "				,SUM(ALCTD_NB) AS ALCTD_NB                                         " & vbNewLine _
                              & "		   FROM                                                                    " & vbNewLine _
                              & "		   $LM_TRN$..C_OUTKA_S CS                                                  " & vbNewLine _
                              & "		   WHERE                                                                   " & vbNewLine _
                              & "		   CS.NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine _
                              & "		   AND                                                                     " & vbNewLine _
                              & "		   CS.OUTKA_NO_L = @OUTKA_NO_L                                             " & vbNewLine _
                              & "		   AND                                                                     " & vbNewLine _
                              & "		   CS.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                              & "		   GROUP BY                                                                " & vbNewLine _
                              & "				 NRS_BR_CD                                                         " & vbNewLine _
                              & "				,OUTKA_NO_L                                                        " & vbNewLine _
                              & "				,OUTKA_NO_M                                                        " & vbNewLine _
                              & "				,LOT_NO                                                            " & vbNewLine _
                              & "		  ) SUB_CS                                                                 " & vbNewLine _
                              & "		  ON                                                                       " & vbNewLine _
                              & "		  CM.NRS_BR_CD = SUB_CS.NRS_BR_CD                                          " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CM.OUTKA_NO_L = SUB_CS.OUTKA_NO_L                                        " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CM.OUTKA_NO_M = SUB_CS.OUTKA_NO_M                                        " & vbNewLine _
                              & "         LEFT JOIN                                                                 " & vbNewLine _
                              & "             (SELECT                                                               " & vbNewLine _
                              & "                   MIN(CMH.OUTKA_NO_L)      AS OUTKA_NO_L                          " & vbNewLine _
                              & "                  ,MIN(CMH.NRS_BR_CD)       AS NRS_BR_CD                           " & vbNewLine _
                              & "                  ,MIN(CMH.CASE_NO_FROM)    AS CASE_NO_FROM                        " & vbNewLine _
                              & "                  ,MIN(CMH.CASE_NO_TO)      AS CASE_NO_TO                          " & vbNewLine _
                              & "                  ,MIN(CMD_001.REMARK_INFO) AS MARK_INFO_1                         " & vbNewLine _
                              & "                  ,MIN(CMD_002.REMARK_INFO) AS MARK_INFO_2                         " & vbNewLine _
                              & "                  ,MIN(CMD_003.REMARK_INFO) AS MARK_INFO_3                         " & vbNewLine _
                              & "                  ,MIN(CMD_004.REMARK_INFO) AS MARK_INFO_4                         " & vbNewLine _
                              & "              FROM                                                                 " & vbNewLine _
                              & "                  (SELECT                                                          " & vbNewLine _
                              & "                        CM.OUTKA_NO_L   AS OUTKA_NO_L                              " & vbNewLine _
                              & "                       ,CM.NRS_BR_CD    AS NRS_BR_CD                               " & vbNewLine _
                              & "                       ,MIN(OUTKA_NO_M) AS OUTKA_NO_M                              " & vbNewLine _
                              & "                   FROM $LM_TRN$..C_OUTKA_M CM                                     " & vbNewLine _
                              & "                  WHERE                                                            " & vbNewLine _
                              & "                        CM.SYS_DEL_FLG      = '0'                                  " & vbNewLine _
                              & "                    AND CM.NRS_BR_CD        = @NRS_BR_CD                           " & vbNewLine _
                              & "                    AND CM.OUTKA_NO_L       = @OUTKA_NO_L                          " & vbNewLine _
                              & "                  GROUP BY                                                         " & vbNewLine _
                              & "                        CM.NRS_BR_CD                                               " & vbNewLine _
                              & "                       ,CM.OUTKA_NO_L                                              " & vbNewLine _
                              & "                  ) CM                                                             " & vbNewLine _
                              & "              LEFT JOIN                                                            " & vbNewLine _
                              & "                   $LM_TRN$..C_MARK_HED CMH                                        " & vbNewLine _
                              & "                ON CM.NRS_BR_CD    = CMH.NRS_BR_CD                                 " & vbNewLine _
                              & "               AND CM.OUTKA_NO_L   = CMH.OUTKA_NO_L                                " & vbNewLine _
                              & "               AND CM.OUTKA_NO_M   = CMH.OUTKA_NO_M                                " & vbNewLine _
                              & "              LEFT JOIN                                                            " & vbNewLine _
                              & "                   $LM_TRN$..C_MARK_DTL CMD_001                                    " & vbNewLine _
                              & "                ON CM.NRS_BR_CD        = CMD_001.NRS_BR_CD                         " & vbNewLine _
                              & "               AND CM.OUTKA_NO_L       = CMD_001.OUTKA_NO_L                        " & vbNewLine _
                              & "               AND CM.OUTKA_NO_M       = CMD_001.OUTKA_NO_M                        " & vbNewLine _
                              & "               AND CMD_001.MARK_EDA    = '001'                                     " & vbNewLine _
                              & "               AND CMD_001.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                              & "              LEFT JOIN                                                            " & vbNewLine _
                              & "                   $LM_TRN$..C_MARK_DTL CMD_002                                    " & vbNewLine _
                              & "                ON CM.NRS_BR_CD        = CMD_002.NRS_BR_CD                         " & vbNewLine _
                              & "               AND CM.OUTKA_NO_L       = CMD_002.OUTKA_NO_L                        " & vbNewLine _
                              & "               AND CM.OUTKA_NO_M       = CMD_002.OUTKA_NO_M                        " & vbNewLine _
                              & "               AND CMD_002.MARK_EDA    = '002'                                     " & vbNewLine _
                              & "               AND CMD_002.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                              & "              LEFT JOIN                                                            " & vbNewLine _
                              & "                   $LM_TRN$..C_MARK_DTL CMD_003                                    " & vbNewLine _
                              & "                ON CM.NRS_BR_CD        = CMD_003.NRS_BR_CD                         " & vbNewLine _
                              & "               AND CM.OUTKA_NO_L       = CMD_003.OUTKA_NO_L                        " & vbNewLine _
                              & "               AND CM.OUTKA_NO_M       = CMD_003.OUTKA_NO_M                        " & vbNewLine _
                              & "               AND CMD_003.MARK_EDA    = '003'                                     " & vbNewLine _
                              & "               AND CMD_003.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                              & "              LEFT JOIN                                                            " & vbNewLine _
                              & "                   $LM_TRN$..C_MARK_DTL CMD_004                                    " & vbNewLine _
                              & "                ON CM.NRS_BR_CD        = CMD_004.NRS_BR_CD                         " & vbNewLine _
                              & "               AND CM.OUTKA_NO_L       = CMD_004.OUTKA_NO_L                        " & vbNewLine _
                              & "               AND CM.OUTKA_NO_M       = CMD_004.OUTKA_NO_M                        " & vbNewLine _
                              & "               AND CMD_004.MARK_EDA    = '004'                                     " & vbNewLine _
                              & "               AND CMD_004.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                              & "         	  ) MARK_INFO                                                           " & vbNewLine _
                              & "          ON CL.NRS_BR_CD  = MARK_INFO.NRS_BR_CD                                   " & vbNewLine _
                              & "         AND CL.OUTKA_NO_L = MARK_INFO.OUTKA_NO_L                                  " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "		  $LM_MST$..M_CUST MC                                                      " & vbNewLine _
                              & "		  ON                                                                       " & vbNewLine _
                              & "		  CL.NRS_BR_CD = MC.NRS_BR_CD                                              " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CL.CUST_CD_L = MC.CUST_CD_L                                              " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CL.CUST_CD_M = MC.CUST_CD_M                                              " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  MC.CUST_CD_S = '00'                                                      " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  MC.CUST_CD_SS = '00'                                                     " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CL.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "		  $LM_MST$..M_GOODS MG                                                       " & vbNewLine _
                              & "		  ON                                                                       " & vbNewLine _
                              & "		  CM.NRS_BR_CD = MG.NRS_BR_CD                                              " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CM.GOODS_CD_NRS = MG.GOODS_CD_NRS                                        " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  MG.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "			  $LM_MST$..M_CUST_RPT MCR1                                            " & vbNewLine _
                              & "		  ON CL.NRS_BR_CD = MCR1.NRS_BR_CD                                         " & vbNewLine _
                              & "		  AND CL.CUST_CD_L = MCR1.CUST_CD_L                                        " & vbNewLine _
                              & "		  AND CL.CUST_CD_M = MCR1.CUST_CD_M                                        " & vbNewLine _
                              & "		  AND MCR1.PTN_ID = '00'                                                   " & vbNewLine _
                              & "		--帳票パターン取得                                                         " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "			  $LM_MST$..M_RPT MR1                                                    " & vbNewLine _
                              & "		  ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                        " & vbNewLine _
                              & "		  AND MR1.PTN_ID = MCR1.PTN_ID                                             " & vbNewLine _
                              & "		  AND MR1.PTN_CD = MCR1.PTN_CD                                             " & vbNewLine _
                              & "		  AND MR1.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                              & "		--商品Mの荷主での荷主帳票パターン取得                                      " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "			  $LM_MST$..M_CUST_RPT MCR2                                              " & vbNewLine _
                              & "		  ON MG.NRS_BR_CD = MCR2.NRS_BR_CD                                         " & vbNewLine _
                              & "		  AND MG.CUST_CD_L = MCR2.CUST_CD_L                                        " & vbNewLine _
                              & "		  AND MG.CUST_CD_M = MCR2.CUST_CD_M                                        " & vbNewLine _
                              & "		  AND MG.CUST_CD_S = MCR2.CUST_CD_S                                        " & vbNewLine _
                              & "		  AND MCR2.PTN_ID = 'BR'                                                   " & vbNewLine _
                              & "		--帳票パターン取得                                                         " & vbNewLine _
                              & "		  LEFT JOIN                                                                " & vbNewLine _
                              & "			 $LM_MST$..M_RPT MR2                                                   " & vbNewLine _
                              & "		  ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                        " & vbNewLine _
                              & "		  AND MR2.PTN_ID = MCR2.PTN_ID                                             " & vbNewLine _
                              & "		  AND MR2.PTN_CD = MCR2.PTN_CD                                             " & vbNewLine _
                              & "		  AND MR2.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                              & "		--存在しない場合の帳票パターン取得                                         " & vbNewLine _
                              & "		  LEFT LOOP JOIN                                                           " & vbNewLine _
                              & "			 $LM_MST$..M_RPT MR3                                                   " & vbNewLine _
                              & "		  ON MR3.NRS_BR_CD = CL.NRS_BR_CD                                          " & vbNewLine _
                              & "		  AND MR3.PTN_ID = 'BR'                                                    " & vbNewLine _
                              & "		  AND MR3.STANDARD_FLAG = '01'                                             " & vbNewLine _
                              & "		  AND MR3.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                              & "         -----------------------2015.08.18 追加START ------------------------     " & vbNewLine _
                              & "         LEFT JOIN                                                                " & vbNewLine _
                              & "         $LM_MST$..M_DEST MDS                                                     " & vbNewLine _
                              & "         ON                                                                       " & vbNewLine _
                              & "         CL.NRS_BR_CD  = MDS.NRS_BR_CD                                            " & vbNewLine _
                              & "         AND                                                                      " & vbNewLine _
                              & "         CL.CUST_CD_L  = MDS.CUST_CD_L                                            " & vbNewLine _
                              & "         AND                                                                      " & vbNewLine _
                              & " --UPD 2019/11/26        CL.SHIP_CD_L  = MDS.DEST_CD                                              " & vbNewLine _
                              & "         CL.DEST_CD    = MDS.DEST_CD                                              " & vbNewLine _
                              & "         AND                                                                      " & vbNewLine _
                              & "         MDS.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                              & "         -----------------------2015.08.18 追加END ------------------------       " & vbNewLine _
                              & "         -----------------------2015.08.10Add-ST ------------------------         " & vbNewLine _
                              & "         LEFT JOIN                                                                " & vbNewLine _
                              & "         $LM_TRN$..H_OUTKAEDI_M EM                                                " & vbNewLine _
                              & "         ON                                                                       " & vbNewLine _
                              & "         CL.NRS_BR_CD   = EM.NRS_BR_CD                                            " & vbNewLine _
                              & "         AND                                                                      " & vbNewLine _
                              & "         CL.OUTKA_NO_L  = EM.OUTKA_CTL_NO                                         " & vbNewLine _
                              & "         AND                                                                      " & vbNewLine _
                              & "         CM.OUTKA_NO_M  = EM.OUTKA_CTL_NO_CHU                                     " & vbNewLine _
                              & "         AND                                                                      " & vbNewLine _
                              & "         EM.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                              & "       --届先明細(特有名称有商品)                                                 " & vbNewLine _
                              & "         LEFT JOIN                                                                " & vbNewLine _
                              & "             (SELECT                                                              " & vbNewLine _
                              & "                   CL.NRS_BR_CD        AS NRS_BR_CD                               " & vbNewLine _
                              & "                  ,CL.OUTKA_NO_L       AS OUTKA_NO_L                              " & vbNewLine _
                              & "                  ,CM.OUTKA_NO_M       AS OUTKA_NO_M                              " & vbNewLine _
                              & "                  ,DEST_DTL.SET_NAIYO  AS GOODS_CD_NRS                            " & vbNewLine _
                              & "              FROM                                                                " & vbNewLine _
                              & "                   $LM_TRN$..C_OUTKA_L CL                                         " & vbNewLine _
                              & "              LEFT JOIN                                                           " & vbNewLine _
                              & "         	     $LM_TRN$..C_OUTKA_M CM                                            " & vbNewLine _
                              & "                ON CM.SYS_DEL_FLG       = '0'                                     " & vbNewLine _
                              & "               AND CL.NRS_BR_CD         = CM.NRS_BR_CD                            " & vbNewLine _
                              & "               AND CL.OUTKA_NO_L        = CM.OUTKA_NO_L                           " & vbNewLine _
                              & "              LEFT JOIN $LM_MST$..M_DEST_DETAILS DEST_DTL                         " & vbNewLine _
                              & "                ON DEST_DTL.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                              & "               AND DEST_DTL.SUB_KB      = '13'                                    " & vbNewLine _
                              & "               AND CL.NRS_BR_CD         = DEST_DTL.NRS_BR_CD                      " & vbNewLine _
                              & "               AND CL.CUST_CD_L         = DEST_DTL.CUST_CD_L                      " & vbNewLine _
                              & "               AND CL.SHIP_CD_L         = DEST_DTL.DEST_CD                        " & vbNewLine _
                              & "              WHERE                                                               " & vbNewLine _
                              & "                   CL.SYS_DEL_FLG       = '0'                                     " & vbNewLine _
                              & "               AND CM.SYS_DEL_FLG       = '0'                                     " & vbNewLine _
                              & "               AND CL.NRS_BR_CD         = @NRS_BR_CD                              " & vbNewLine _
                              & "               AND CL.OUTKA_NO_L        = @OUTKA_NO_L                             " & vbNewLine _
                              & "              GROUP BY                                                            " & vbNewLine _
                              & "                   CL.OUTKA_NO_L                                                  " & vbNewLine _
                              & "                  ,CL.NRS_BR_CD                                                   " & vbNewLine _
                              & "                  ,CM.OUTKA_NO_M                                                  " & vbNewLine _
                              & "                  ,SET_NAIYO                                                      " & vbNewLine _
                              & "             ) DEST_GOODS                                                         " & vbNewLine _
                              & "           ON CL.NRS_BR_CD            = DEST_GOODS.NRS_BR_CD                      " & vbNewLine _
                              & "          AND CL.OUTKA_NO_L           = DEST_GOODS.OUTKA_NO_L                     " & vbNewLine _
                              & "          AND CM.OUTKA_NO_M           = DEST_GOODS.OUTKA_NO_M                     " & vbNewLine _
                              & "         -----------------------2015.08.18 追加START ------------------------     " & vbNewLine _
                              & "          AND CM.GOODS_CD_NRS         = DEST_GOODS.GOODS_CD_NRS                   " & vbNewLine _
                              & "         -----------------------2015.08.18 追加END ------------------------       " & vbNewLine _
                              & "         LEFT JOIN                                                                " & vbNewLine _
                              & "              $LM_MST$..M_GOODS_DETAILS MGDT                                      " & vbNewLine _
                              & "           ON MGDT.SYS_DEL_FLG        = '0'                                       " & vbNewLine _
                              & "          AND MGDT.SUB_KB             = '53'                                      " & vbNewLine _
                              & "          AND DEST_GOODS.NRS_BR_CD    = MGDT.NRS_BR_CD                            " & vbNewLine _
                              & "          AND DEST_GOODS.GOODS_CD_NRS = MGDT.GOODS_CD_NRS                         " & vbNewLine _
                              & "         -----------------------2015.08.10Add-ED ------------------------         " & vbNewLine _
                              & "		  WHERE                                                                    " & vbNewLine _
                              & "		  CL.NRS_BR_CD = @NRS_BR_CD                                                " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CL.OUTKA_NO_L = @OUTKA_NO_L                                              " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CL.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                              & "		  AND                                                                      " & vbNewLine _
                              & "		  CM.SYS_DEL_FLG = '0'                                                     " & vbNewLine
                            


#End Region

#Region "GROUP BY句"

    ''' <summary>
    ''' 印刷データ抽出用 GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = ")MAIN           " & vbNewLine _
                              & "       GROUP BY            " & vbNewLine _
                              & "	    MAIN.PTN_CD         " & vbNewLine _
                              & "     , MAIN.RPT_ID         " & vbNewLine _
                              & "     , MAIN.NRS_BR_CD      " & vbNewLine _
                              & "     , MAIN.OUTKA_NO_L     " & vbNewLine _
                              & "     , MAIN.JYUCYU_NM      " & vbNewLine _
                              & "     , MAIN.CRT_DATE       " & vbNewLine _
                              & "     , MAIN.CUST_ORD_NO    " & vbNewLine _
                              & "     , MAIN.ARR_PLAN_DATE  " & vbNewLine _
                              & "     , MAIN.DEST_NM        " & vbNewLine _
                              & "     , MAIN.CASE_NO        " & vbNewLine _
                              & "     , MAIN.GOODS_NM       " & vbNewLine _
                              & "     , MAIN.PKG_NB         " & vbNewLine _
                              & "     , MAIN.PLT_PER_PKG_UT " & vbNewLine _
                              & "	  , MAIN.LOT_NO_NB      " & vbNewLine _
                              & "     , MAIN.MARK_INFO_1    " & vbNewLine _
                              & "     , MAIN.MARK_INFO_2    " & vbNewLine _
                              & "     , MAIN.MARK_INFO_3    " & vbNewLine _
                              & "     , MAIN.MARK_INFO_4    " & vbNewLine _
                              & "     , MAIN.CASE_NO_FROM   " & vbNewLine _
                              & "     , MAIN.CASE_NO_TO     " & vbNewLine _
                              & "     , MAIN.CUST_NM_L      " & vbNewLine _
                              & "	  , MAIN.GOODS_CD_NRS   " & vbNewLine

#End Region

#Region "ORDER BY句"

    ''' <summary>
    ''' 印刷データ抽出用 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                 " & vbNewLine _
                                         & " MAIN.NRS_BR_CD           " & vbNewLine _
                                         & ",MAIN.OUTKA_NO_L          " & vbNewLine _
                                         & ",MAIN.GOODS_CD_NRS        " & vbNewLine _
                                         & ",MAIN.LOT_NO_NB           " & vbNewLine


#End Region

#Region "商品明細(SUB_KB = '51')"

    ' ''' <summary>
    ' ''' 印刷データ抽出用 SELECT句
    ' ''' </summary>
    ' ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_GOODS_DETAILS As String = "SELECT                                                                                           " & vbNewLine _
                                          & "    MGD_51.NRS_BR_CD                                                                         AS NRS_BR_CD        " & vbNewLine _
                                          & "   ,MGD_51.GOODS_CD_NRS                                                                      AS GOODS_CD_NRS     " & vbNewLine _
                                          & "   ,MGD_51.GOODS_CD_NRS_EDA                                                                  AS GOODS_CD_NRS_EDA " & vbNewLine _
                                          & "   ,SUBSTRING(ISNULL(MGD_51.SET_NAIYO,''),1,CHARINDEX('-',ISNULL(MGD_51.SET_NAIYO,'')) -1)   AS IRISU            " & vbNewLine _
                                          & "   ,SUBSTRING(ISNULL(MGD_51.SET_NAIYO,''),CHARINDEX('-',ISNULL(MGD_51.SET_NAIYO,'')) +1,40)  AS SIZE_WT          " & vbNewLine _
                                          & "   ,'0'                                                                                      AS DISP_FLG         " & vbNewLine _
                                          & "FROM                                                                                                             " & vbNewLine _
                                          & "$LM_MST$..M_GOODS_DETAILS MGD_51                                                                                 " & vbNewLine _
                                          & "WHERE                                                                                                            " & vbNewLine _
                                          & "MGD_51.NRS_BR_CD = @NRS_BR_CD                                                                                    " & vbNewLine _
                                          & "AND                                                                                                              " & vbNewLine _
                                          & "MGD_51.SUB_KB = '51'                                                                                             " & vbNewLine _
                                          & "AND                                                                                                              " & vbNewLine _
                                          & "MGD_51.GOODS_CD_NRS = @GOODS_CD_NRS                                                                              " & vbNewLine _
                                          & "AND                                                                                                              " & vbNewLine _
                                          & "MGD_51.SYS_DEL_FLG = '0'                                                                                         " & vbNewLine _
                                          & "ORDER BY                                                                                                         " & vbNewLine _
                                          & "    MGD_51.NRS_BR_CD                                                                                             " & vbNewLine _
                                          & "   ,MGD_51.GOODS_CD_NRS                                                                                          " & vbNewLine _
                                          & "   ,CONVERT(int,MGD_51.GOODS_CD_NRS_EDA) DESC                                                                    " & vbNewLine

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

#Region "印刷処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC790IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC790DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC790DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Call Me.setIndataParameter(Me._Row)               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC790DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        '処理件数の設定
        If ds.Tables("M_RPT").Rows.Count < 1 Then
            MyBase.SetMessage("G021")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 印刷対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC790IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LMC790DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMC790DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC790DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GroupBy句)
        Me._StrSql.Append(LMC790DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)
        Call Me.setIndataParameter(Me._Row)               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC790DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("JYUCYU_NM", "JYUCYU_NM")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("CASE_NO", "CASE_NO")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PLT_PER_PKG_UT", "PLT_PER_PKG_UT")
        map.Add("LOT_NO_NB", "LOT_NO_NB")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("MARK_INFO_1", "MARK_INFO_1")
        map.Add("MARK_INFO_2", "MARK_INFO_2")
        map.Add("MARK_INFO_3", "MARK_INFO_3")
        map.Add("MARK_INFO_4", "MARK_INFO_4")
        map.Add("CASE_NO_FROM", "CASE_NO_FROM")
        map.Add("CASE_NO_TO", "CASE_NO_TO")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC790OUT")

        Return ds

    End Function


    ''' <summary>
    ''' 商品明細マスタ(サイズ・重量取得)処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectGoodsDetails(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim outTbl As DataTable = ds.Tables("LMC790OUT")

        'INTableの条件rowの格納
        Me._Row = outTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC790DAC.SQL_SELECT_FROM_GOODS_DETAILS)
        Call Me.setGoodsParameter(Me._Row)               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC790DAC", "SelectGoodsDetails", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_NRS_EDA", "GOODS_CD_NRS_EDA")
        map.Add("IRISU", "IRISU")
        map.Add("SIZE_WT", "SIZE_WT")
        map.Add("DISP_FLG", "DISP_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GOODS_SIZE_WT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setIndataParameter(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setgoodsParameter(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))

        End With

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

End Class
