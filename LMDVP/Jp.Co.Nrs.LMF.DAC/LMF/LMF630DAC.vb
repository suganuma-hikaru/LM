' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF630DAC : 運賃請求明細書(出荷)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF630DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF630DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	UNCHIN.NRS_BR_CD                                            AS NRS_BR_CD " & vbNewLine _
                                            & ",'AI'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

#End Region

#Region "SELECT句"
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                   " & vbNewLine _
                                          & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                           " & vbNewLine _
                                          & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                        " & vbNewLine _
                                          & "		  ELSE MR3.RPT_ID END                                AS RPT_ID       " & vbNewLine _
                                          & " , UNCHIN.SEIQ_GROUP_NO  AS SEIQ_GROUP_NO                                   " & vbNewLine _
                                          & " , UNCHIN.SEIQTO_CD      AS SEIQTO_CD                                       " & vbNewLine _
                                          & " , SEIQTO.SEIQTO_NM      AS SEIQTO_NM                                       " & vbNewLine _
                                          & " , UNCHIN.CUST_CD_L      AS CUST_CD_L                                       " & vbNewLine _
                                          & " , UNCHIN.CUST_CD_M      AS CUST_CD_M                                       " & vbNewLine _
                                          & " , UNCHIN.CUST_CD_S      AS CUST_CD_S                                       " & vbNewLine _
                                          & " , UNCHIN.CUST_CD_SS     AS CUST_CD_SS                                      " & vbNewLine _
                                          & " --Notes 700 2012/05/24 START                                               " & vbNewLine _
                                          & " --, CUST.CUST_NM_L        AS CUST_NM_L                                       " & vbNewLine _
                                          & " --, CUST.CUST_NM_M        AS CUST_NM_M                                       " & vbNewLine _
                                          & " --, CUST.CUST_NM_S        AS CUST_NM_S                                       " & vbNewLine _
                                          & " --, CUST.CUST_NM_SS       AS CUST_NM_SS                                      " & vbNewLine _
                                          & " ,CASE WHEN (CUST.CUST_NM_L IS NOT NULL AND CUST.CUST_NM_L <> '') THEN CUST.CUST_NM_L " & vbNewLine _
                                          & "       ELSE CUST_F.CUST_NM_L                                                          " & vbNewLine _
                                          & "  END                                                             AS CUST_NM_L        " & vbNewLine _
                                          & " ,CASE WHEN (CUST.CUST_NM_M IS NOT NULL AND CUST.CUST_NM_M <> '') THEN CUST.CUST_NM_M " & vbNewLine _
                                          & "       ELSE CUST_F.CUST_NM_M                                                          " & vbNewLine _
                                          & "  END                                                             AS CUST_NM_M        " & vbNewLine _
                                          & " ,CASE WHEN (CUST.CUST_NM_S IS NOT NULL AND CUST.CUST_NM_S <> '') THEN CUST.CUST_NM_S " & vbNewLine _
                                          & "       ELSE CUST_F.CUST_NM_S                                                          " & vbNewLine _
                                          & "  END                                                             AS CUST_NM_S        " & vbNewLine _
                                          & " ,CASE WHEN (CUST.CUST_NM_SS IS NOT NULL AND CUST.CUST_NM_SS <> '') THEN CUST.CUST_NM_SS  " & vbNewLine _
                                          & "       ELSE CUST_F.CUST_NM_SS                                                             " & vbNewLine _
                                          & "  END                                                             AS CUST_NM_SS           " & vbNewLine _
                                          & " --Notes 700 2012/05/24 END                                                 " & vbNewLine _
                                          & " , @T_DATE               AS T_DATE                                          " & vbNewLine _
                                          & " , NRS.NRS_BR_NM         AS NRS_BR_NM                                       " & vbNewLine _
                                          & " , UNSO.MOTO_DATA_KB     AS MOTO_DATA_KB                                    " & vbNewLine _
                                          & " , UNSO.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                                 " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD            " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD            " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                   " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' THEN UNSO.ORIG_CD                                    " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加  END                                    " & vbNewLine _
                                          & "   ELSE UNSO.DEST_CD                                                                       " & vbNewLine _
                                          & "   END                   AS DEST_CD                                                        " & vbNewLine _
                                          & " --☆要望番号376関連でコメントアウト開始☆                                                 " & vbNewLine _
                                          & " --, CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM          " & vbNewLine _
                                          & " --       WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM          " & vbNewLine _
                                          & " --       ELSE DEST.DEST_NM                                                                " & vbNewLine _
                                          & " --  END                   AS DEST_NM                                                      " & vbNewLine _
                                          & " --, CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1        " & vbNewLine _
                                          & " --       WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1        " & vbNewLine _
                                          & " --       ELSE DEST.AD_1                                                                   " & vbNewLine _
                                          & " --  END                   AS AD_1                                                         " & vbNewLine _
                                          & " --★要望番号376関連でコメントアウト終了★                                                 " & vbNewLine _
                                          & "	--★変更START 要望番号376①★				                                            " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM            " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM            " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                         " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.DEST_NM IS NOT NULL AND DEST3.DEST_NM <> '') THEN DEST3.DEST_NM " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.DEST_NM IS NOT NULL AND DEST4.DEST_NM <> '') THEN DEST4.DEST_NM " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                         " & vbNewLine _
                                          & "        WHEN (DEST.DEST_NM IS NOT NULL AND DEST.DEST_NM <> '') THEN DEST.DEST_NM           " & vbNewLine _
                                          & "        ELSE DEST2.DEST_NM                                                                 " & vbNewLine _
                                          & "   END                   AS DEST_NM                                                        " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1          " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1          " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                         " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_1 IS NOT NULL AND DEST3.AD_1 <> '') THEN DEST3.AD_1          " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_1 IS NOT NULL AND DEST4.AD_1 <> '') THEN DEST4.AD_1          " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                         " & vbNewLine _
                                          & "        WHEN (DEST.AD_1 IS NOT NULL AND DEST.AD_1 <> '') THEN DEST.AD_1                    " & vbNewLine _
                                          & "        ELSE DEST2.AD_1                                                                    " & vbNewLine _
                                          & "   END                   AS AD_1                                                           " & vbNewLine _
                                          & "	--★変更END 要望番号376①★				                                                " & vbNewLine _
                                          & " , UNSOM.GOODS_NM        AS GOODS_NM                                        " & vbNewLine _
                                          & " , UNCHIN.DECI_NG_NB     AS DECI_NG_NB                                      " & vbNewLine _
                                          & " , UNCHIN.DECI_WT        AS DECI_WT                                         " & vbNewLine _
                                          & " , UNCHIN.DECI_UNCHIN    AS DECI_UNCHIN                                     " & vbNewLine _
                                          & " , UNCHIN.DECI_CITY_EXTC AS DECI_CITY_EXTC                                  " & vbNewLine _
                                          & " , UNCHIN.DECI_WINT_EXTC AS DECI_WINT_EXTC                                  " & vbNewLine _
                                          & " , UNCHIN.DECI_RELY_EXTC AS DECI_RELY_EXTC                                  " & vbNewLine _
                                          & " , UNCHIN.DECI_TOLL      AS DECI_TOLL                                       " & vbNewLine _
                                          & " , UNCHIN.DECI_INSU      AS DECI_INSU                                       " & vbNewLine _
                                          & " , UNCHIN.REMARK         AS REMARK                                          " & vbNewLine _
                                          & " , GOODS.CUST_COST_CD2   AS CUST_COST_CD2                                   " & vbNewLine _
                                          & " , GOODS.SEARCH_KEY_1    AS SEARCH_KEY_1                                    " & vbNewLine _
                                          & " , JIS.SHI               AS SHI                                             " & vbNewLine _
                                          & " , UNCHIN.SEIQ_KYORI     AS SEIQ_KYORI                                      " & vbNewLine _
                                          & " , GOODS.GOODS_CD_CUST   AS GOODS_CD_CUST                                   " & vbNewLine _
                                          & " , UNCHIN.SEIQ_PKG_UT    AS SEIQ_PKG_UT                                     " & vbNewLine _
                                          & " , UNCHIN.SEIQ_TARIFF_CD AS SEIQ_TARIFF_CD                                  " & vbNewLine _
                                          & " , UNSO.UNSO_NO_L        AS UNSO_NO_L                                       " & vbNewLine _
                                          & " --LMF513対応 2012/06/12                                                    " & vbNewLine _
                                          & " , UNSO.JIYU_KB          AS JIYU_KB                                         " & vbNewLine _
                                          & " , UNSO.ARR_PLAN_DATE    AS ARR_PLAN_DATE                                   " & vbNewLine _
                                          & " , UNSO.ORIG_CD          AS NYUUKA_CD                                       " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.DEST_NM IS NOT NULL AND DEST3.DEST_NM <> '') THEN DEST3.DEST_NM  " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.DEST_NM IS NOT NULL AND DEST4.DEST_NM <> '') THEN DEST4.DEST_NM  " & vbNewLine _
                                          & "        WHEN (DEST3.DEST_NM IS NOT NULL AND DEST3.DEST_NM <> '') THEN DEST3.DEST_NM                               " & vbNewLine _
                                          & "        ELSE DEST4.DEST_NM                                                                                        " & vbNewLine _
                                          & "   END                   AS NYUUKA_NM                                                                             " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_1 IS NOT NULL AND DEST3.AD_1 <> '') THEN DEST3.AD_1           " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_1 IS NOT NULL AND DEST4.AD_1 <> '') THEN DEST4.AD_1           " & vbNewLine _
                                          & "        WHEN (DEST3.AD_1 IS NOT NULL AND DEST3.AD_1 <> '') THEN DEST3.AD_1                                        " & vbNewLine _
                                          & "        ELSE DEST4.AD_1                                                                                           " & vbNewLine _
                                          & "   END                   AS NYUUKA_AD_1                                                                           " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD                                   " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                   " & vbNewLine _
                                          & "   ELSE UNSO.DEST_CD                                                                                              " & vbNewLine _
                                          & "   END                   AS SYUKKA_CD                                                                             " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                   " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                   " & vbNewLine _
                                          & "        WHEN (DEST.DEST_NM IS NOT NULL AND DEST.DEST_NM <> '') THEN DEST.DEST_NM                                  " & vbNewLine _
                                          & "        ELSE DEST2.DEST_NM                                                                                        " & vbNewLine _
                                          & "  END                   AS SYUKKA_NM                                                                              " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                 " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                 " & vbNewLine _
                                          & "        WHEN (DEST.AD_1 IS NOT NULL AND DEST.AD_1 <> '') THEN DEST.AD_1                                           " & vbNewLine _
                                          & "        ELSE DEST2.AD_1                                                                                           " & vbNewLine _
                                          & "   END                   AS SYUKKA_AD_1                                                                           " & vbNewLine _
                                          & "--(2012.09.21)要望番号1452 -- START --                                                                            " & vbNewLine _
                                          & "  , CASE WHEN UNCHIN.NRS_BR_CD = '30' AND UNCHIN.CUST_CD_L = '00076' AND UNSO.MOTO_DATA_KB = '10' THEN                        " & vbNewLine _
                                          & "                             + UNSO.MOTO_DATA_KB                                                                  " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_L                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_M                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_S                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_SS                                                                  " & vbNewLine _
                                          & "                             + GOODS.SEARCH_KEY_1                                                                 " & vbNewLine _
                                          & "        WHEN  UNCHIN.NRS_BR_CD = '30' AND UNCHIN.CUST_CD_L = '00076' AND UNSO.MOTO_DATA_KB <> '10' THEN                       " & vbNewLine _
                                          & "                             + UNSO.MOTO_DATA_KB                                                                  " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_L                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_M                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_S                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_SS                                                                  " & vbNewLine _
                                          & "        ELSE                   UNCHIN.SEIQTO_CD                                                                   " & vbNewLine _
                                          & "                             + UNSO.MOTO_DATA_KB                                                                  " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_L                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_M                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_S                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_SS                                                                  " & vbNewLine _
                                          & "   END      AS SYS_PAGE_KEY                                                                                       " & vbNewLine _
                                          & "--(2012.09.21)要望番号1452 --  END  --                                                                            " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = "FROM                                                               " & vbNewLine _
                                           & "	$LM_TRN$..F_UNCHIN_TRS AS UNCHIN			               " & vbNewLine _
                                           & "	--運送L				                                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO				       " & vbNewLine _
                                           & "	ON UNCHIN.UNSO_NO_L=UNSO.UNSO_NO_L				           " & vbNewLine _
                                           & "  AND UNSO.SYS_DEL_FLG='0'				                   " & vbNewLine _
                                           & "	--運送Ｍ                                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM                      " & vbNewLine _
                                           & "	 ON UNCHIN.UNSO_NO_L=UNSOM.UNSO_NO_L                       " & vbNewLine _
                                           & " -- Notes【№714】商品名表示不具合対応 不要SQLコメント START " & vbNewLine _
                                           & "	 AND(                                                      " & vbNewLine _
                                           & "	      -- CASE                                              " & vbNewLine _
                                           & "	      -- WHEN UNCHIN.UNSO_NO_M = '000'                     " & vbNewLine _
                                           & "		  -- THEN(                                             " & vbNewLine _
                                           & "			      SELECT MIN(UNSO_NO_M)                        " & vbNewLine _
                                           & "				    FROM $LM_TRN$..F_UNSO_M UNSOM              " & vbNewLine _
                                           & "				   WHERE UNSOM.UNSO_NO_L = UNCHIN.UNSO_NO_L    " & vbNewLine _
                                           & "				     AND UNSOM.SYS_DEL_FLG='0'                 " & vbNewLine _
                                           & "	      --     )                                             " & vbNewLine _
                                           & "	      -- ELSE UNCHIN.UNSO_NO_M                             " & vbNewLine _
                                           & "	      -- END)                                              " & vbNewLine _
                                           & "	     )                                                     " & vbNewLine _
                                           & " -- Notes【№714】商品名表示不具合対応 不要SQLコメント  END  " & vbNewLine _
                                           & "	     = UNSOM.UNSO_NO_M                                     " & vbNewLine _
                                           & "	 AND UNSOM.SYS_DEL_FLG='0'                                 " & vbNewLine _
                                           & "	--出荷L				                                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..C_OUTKA_L AS OUTL				       " & vbNewLine _
                                           & "	ON  OUTL.NRS_BR_CD=UNSO.NRS_BR_CD				           " & vbNewLine _
                                           & "	AND OUTL.OUTKA_NO_L=UNSO.INOUTKA_NO_L			           " & vbNewLine _
                                           & "  AND OUTL.SYS_DEL_FLG='0'				                   " & vbNewLine _
                                           & "	--出荷EDIL			                                       " & vbNewLine _
                                           & "  LEFT JOIN                                                  " & vbNewLine _
                                           & "  (SELECT                                                    " & vbNewLine _
                                           & "    NRS_BR_CD                                                " & vbNewLine _
                                           & "   ,OUTKA_CTL_NO                                             " & vbNewLine _
                                           & "   ,MIN(DEST_CD)    AS DEST_CD                               " & vbNewLine _
                                           & "   ,MIN(DEST_NM)    AS DEST_NM                               " & vbNewLine _
                                           & "   ,MIN(DEST_AD_1)  AS DEST_AD_1                             " & vbNewLine _
                                           & "   ,SYS_DEL_FLG                                              " & vbNewLine _
                                           & "   FROM                                                      " & vbNewLine _
                                           & "    $LM_TRN$..H_OUTKAEDI_L                                   " & vbNewLine _
                                           & "   GROUP BY                                                  " & vbNewLine _
                                           & "    NRS_BR_CD                                                " & vbNewLine _
                                           & "   ,OUTKA_CTL_NO                                             " & vbNewLine _
                                           & "   ,SYS_DEL_FLG                                              " & vbNewLine _
                                           & "   ) EDIL                                                    " & vbNewLine _
                                           & "  ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                        " & vbNewLine _
                                           & "  AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                    " & vbNewLine _
                                           & "  AND EDIL.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                           & "	--請求マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQTO				       " & vbNewLine _
                                           & "	ON UNCHIN.SEIQTO_CD = SEIQTO.SEIQTO_CD				       " & vbNewLine _
                                           & "	AND UNCHIN.NRS_BR_CD = SEIQTO.NRS_BR_CD				       " & vbNewLine _
                                           & "	--商品マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_GOODS AS GOODS				       " & vbNewLine _
                                           & "	ON UNCHIN.NRS_BR_CD=GOODS.NRS_BR_CD				           " & vbNewLine _
                                           & "	AND UNSOM.GOODS_CD_NRS=GOODS.GOODS_CD_NRS				   " & vbNewLine _
                                           & "	--荷主マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST AS CUST				           " & vbNewLine _
                                           & "	ON GOODS.CUST_CD_L=CUST.CUST_CD_L				           " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_M = CUST.CUST_CD_M				       " & vbNewLine _
                                           & "	AND GOODS.NRS_BR_CD=CUST.NRS_BR_CD				           " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_S= CUST.CUST_CD_S				           " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_SS= CUST.CUST_CD_SS				       " & vbNewLine _
                                           & " --荷主マスタ(Notes700)                                      " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_CUST AS CUST_F                      " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD   = CUST_F.NRS_BR_CD                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_L  = CUST_F.CUST_CD_L                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_M  = CUST_F.CUST_CD_M                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_S  = CUST_F.CUST_CD_S                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_SS = CUST_F.CUST_CD_SS                 " & vbNewLine _
                                           & "	--営業所マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_NRS_BR AS NRS				           " & vbNewLine _
                                           & "	ON UNCHIN.NRS_BR_CD = NRS.NRS_BR_CD				           " & vbNewLine _
                                           & "	--届先マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST				           " & vbNewLine _
                                           & "	ON UNSO.DEST_CD   = DEST.DEST_CD				           " & vbNewLine _
                                           & "	AND UNSO.NRS_BR_CD = DEST.NRS_BR_CD				           " & vbNewLine _
                                           & "	AND UNSO.CUST_CD_L = DEST.CUST_CD_L				           " & vbNewLine _
                                           & "	--★追加START 要望番号376②★				               " & vbNewLine _
                                           & "	--届先マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST2				           " & vbNewLine _
                                           & "	ON UNSO.DEST_CD   = DEST2.DEST_CD				           " & vbNewLine _
                                           & "	AND UNSO.NRS_BR_CD = DEST2.NRS_BR_CD				       " & vbNewLine _
                                           & "	AND DEST2.CUST_CD_L = 'ZZZZZ'				               " & vbNewLine _
                                           & "	--★追加END 要望番号376②★				                   " & vbNewLine _
                                           & " -- Notes【№712】元データ区分：入荷での判断を追加 START     " & vbNewLine _
                                           & "	--届先マスタ(通常)                                         " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST3				           " & vbNewLine _
                                           & "	       ON UNSO.ORIG_CD    = DEST3.DEST_CD				   " & vbNewLine _
                                           & "	      AND UNSO.NRS_BR_CD  = DEST3.NRS_BR_CD				   " & vbNewLine _
                                           & "	      AND UNSO.CUST_CD_L  = DEST3.CUST_CD_L				   " & vbNewLine _
                                           & "	--届先マスタ(ZZZZZ対応)				                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST4				           " & vbNewLine _
                                           & "	       ON UNSO.ORIG_CD    = DEST4.DEST_CD				   " & vbNewLine _
                                           & "	      AND UNSO.NRS_BR_CD  = DEST4.NRS_BR_CD				   " & vbNewLine _
                                           & "	      AND DEST4.CUST_CD_L = 'ZZZZZ'				           " & vbNewLine _
                                           & " -- Notes【№712】元データ区分：入荷での判断を追加  END      " & vbNewLine _
                                           & "	--JISマスタ  				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_JIS AS JIS				           " & vbNewLine _
                                           & "	ON JIS.JIS_CD   = DEST.JIS   		    		           " & vbNewLine _
                                           & "--運賃での荷主帳票パターン取得                               " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                          " & vbNewLine _
                                           & "ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                           & "AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                           & "AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                           & "AND '00' = MCR1.CUST_CD_S                                    " & vbNewLine _
                                           & "AND MCR1.PTN_ID = 'AI'                                       " & vbNewLine _
                                           & "--帳票パターン取得                                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR1                                " & vbNewLine _
                                           & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                           " & vbNewLine _
                                           & "AND MR1.PTN_ID = MCR1.PTN_ID                                 " & vbNewLine _
                                           & "AND MR1.PTN_CD = MCR1.PTN_CD                                 " & vbNewLine _
                                           & "AND MR1.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "--商品Mの荷主での荷主帳票パターン取得                        " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                          " & vbNewLine _
                                           & "ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                         " & vbNewLine _
                                           & "AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                         " & vbNewLine _
                                           & "AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                         " & vbNewLine _
                                           & "AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                         " & vbNewLine _
                                           & "AND MCR2.PTN_ID = 'AI'                                       " & vbNewLine _
                                           & "--帳票パターン取得                                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR2                                " & vbNewLine _
                                           & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                           " & vbNewLine _
                                           & "AND MR2.PTN_ID = MCR2.PTN_ID                                 " & vbNewLine _
                                           & "AND MR2.PTN_CD = MCR2.PTN_CD                                 " & vbNewLine _
                                           & "AND MR2.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "--存在しない場合の帳票パターン取得                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR3                                " & vbNewLine _
                                           & "ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                         " & vbNewLine _
                                           & "AND MR3.PTN_ID = 'AI'                                        " & vbNewLine _
                                           & "AND MR3.STANDARD_FLAG = '01'                                 " & vbNewLine _
                                           & "AND MR3.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "	  WHERE UNCHIN.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                                           & "	  AND   UNCHIN.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                           & "    AND   UNCHIN.DECI_UNCHIN <> 0                            " & vbNewLine _
                                           & "    AND   UNCHIN.SEIQ_FIXED_FLAG='01'                        " & vbNewLine _
                                           & "    AND   UNSO.MOTO_DATA_KB = '20'                           " & vbNewLine
#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                     " & vbNewLine _
                                         & "       UNSO.MOTO_DATA_KB      " & vbNewLine _
                                         & "     , UNCHIN.CUST_CD_L       " & vbNewLine _
                                         & "     , UNCHIN.CUST_CD_M       " & vbNewLine _
                                         & "     , UNCHIN.CUST_CD_S       " & vbNewLine _
                                         & "     , UNCHIN.CUST_CD_SS      " & vbNewLine _
                                         & "     , UNCHIN.SEIQTO_CD       " & vbNewLine _
                                         & "     , GOODS.SEARCH_KEY_1     " & vbNewLine _
                                         & "     , UNSO.OUTKA_PLAN_DATE   " & vbNewLine _
                                         & "     , UNSO.DEST_CD           " & vbNewLine _
                                         & "     , GOODS.GOODS_CD_CUST    " & vbNewLine _
                                         & "     , UNSO.UNSO_NO_L         " & vbNewLine _
                                         & "     , UNCHIN.SEIQ_GROUP_NO   " & vbNewLine
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

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF630DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF630DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF630DAC", "SelectMPrt", cmd)

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
    ''' 運賃テーブル対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF630IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LMF630DAC.SQL_SELECT_DATA) 		'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF630DAC.SQL_FROM)               'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
        Me._StrSql.Append(LMF630DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用OrderBy句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF630DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("T_DATE", "T_DATE")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("AD_1", "AD_1")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("REMARK", "REMARK")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SHI", "SHI")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("SEIQ_PKG_UT", "SEIQ_PKG_UT")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("JIYU_KB", "JIYU_KB")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("NYUUKA_CD", "NYUUKA_CD")
        map.Add("NYUUKA_NM", "NYUUKA_NM")
        map.Add("NYUUKA_AD_1", "NYUUKA_AD_1")
        map.Add("SYUKKA_CD", "SYUKKA_CD")
        map.Add("SYUKKA_NM", "SYUKKA_NM")
        map.Add("SYUKKA_AD_1", "SYUKKA_AD_1")
        map.Add("SYS_PAGE_KEY", "SYS_PAGE_KEY")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF630OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '日付To
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", Me._Row.Item("T_DATE").ToString(), DBDataType.CHAR))

            '荷主コード（大）
            whereStr = Me._Row.Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNCHIN.CUST_CD_L = @CUST_CD_L                     ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = Me._Row.Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNCHIN.CUST_CD_M = @CUST_CD_M                      ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            End If

            '請求先コード
            whereStr = Me._Row.Item("SEIQ_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNCHIN.SEIQTO_CD = @SEIQ_CD                	      ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_CD", Me._Row.Item("SEIQ_CD").ToString(), DBDataType.CHAR))
            End If

            '日付
            Me._StrSql.Append("	   AND ( ( ( (                 	      ")
            Me._StrSql.Append("	           CUST_F.UNTIN_CALCULATION_KB ='01'                        ")

            whereStr = Me._Row.Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNSO.OUTKA_PLAN_DATE >= @F_DATE                    ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", Me._Row.Item("F_DATE").ToString(), DBDataType.CHAR))
            End If

            whereStr = Me._Row.Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNSO.OUTKA_PLAN_DATE <= @T_DATE                    ")
                Me._StrSql.Append(vbNewLine)
            End If

            Me._StrSql.Append("           )                                           ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("	   OR    (                 	                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("	           CUST_F.UNTIN_CALCULATION_KB ='02'                ")
            Me._StrSql.Append(vbNewLine)

            whereStr = Me._Row.Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNSO.ARR_PLAN_DATE >= @F_DATE              ")
                Me._StrSql.Append(vbNewLine)
            End If

            whereStr = Me._Row.Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNSO.ARR_PLAN_DATE <= @T_DATE              ")
                Me._StrSql.Append(vbNewLine)
            End If

            Me._StrSql.Append("           ) )                                         ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("           )                                         ")
            Me._StrSql.Append(vbNewLine)

            Me._StrSql.Append("          )                                         ")
            Me._StrSql.Append(vbNewLine)

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
