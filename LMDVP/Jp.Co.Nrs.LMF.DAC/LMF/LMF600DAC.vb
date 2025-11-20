' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF600DAC : 支払運賃明細書
'  作  成  者       :  kurihara
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF600DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF600DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	SHIHARAI_TRS.NRS_BR_CD                                   AS NRS_BR_CD " & vbNewLine _
                                            & ",'AF'                                                     AS PTN_ID    " & vbNewLine _
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
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                  " & vbNewLine _
                                          & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                          " & vbNewLine _
                                          & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                       " & vbNewLine _
                                          & "		  ELSE MR3.RPT_ID END                           AS RPT_ID                           " & vbNewLine _
                                          & " , SHIHARAI_TRS.SHIHARAI_GROUP_NO                      AS SHIHARAI_GROUP_NO                " & vbNewLine _
                                          & " , SHIHARAI_TRS.SHIHARAITO_CD                          AS SHIHARAITO_CD                    " & vbNewLine _
                                          & " , SHIHARAITO.SHIHARAITO_NM                            AS SHIHARAITO_NM                    " & vbNewLine _
                                          & " , SHIHARAI_TRS.UNTIN_CALCULATION_KB                   AS UNTIN_CALCULATION_KB             " & vbNewLine _
                                          & " , SHIHARAI_TRS.CUST_CD_L                              AS CUST_CD_L                        " & vbNewLine _
                                          & " , SHIHARAI_TRS.CUST_CD_M                              AS CUST_CD_M                        " & vbNewLine _
                                          & " , SHIHARAI_TRS.CUST_CD_S                              AS CUST_CD_S                        " & vbNewLine _
                                          & " , SHIHARAI_TRS.CUST_CD_SS                             AS CUST_CD_SS                       " & vbNewLine _
                                          & " ,CASE WHEN (CUST.CUST_NM_L IS NOT NULL AND CUST.CUST_NM_L <> '') THEN CUST.CUST_NM_L      " & vbNewLine _
                                          & "       ELSE CUST_F.CUST_NM_L                                                               " & vbNewLine _
                                          & "  END                                                  AS CUST_NM_L                        " & vbNewLine _
                                          & " ,CASE WHEN (CUST.CUST_NM_M IS NOT NULL AND CUST.CUST_NM_M <> '') THEN CUST.CUST_NM_M      " & vbNewLine _
                                          & "       ELSE CUST_F.CUST_NM_M                                                               " & vbNewLine _
                                          & "  END                                                  AS CUST_NM_M                        " & vbNewLine _
                                          & " ,CASE WHEN (CUST.CUST_NM_S IS NOT NULL AND CUST.CUST_NM_S <> '') THEN CUST.CUST_NM_S      " & vbNewLine _
                                          & "       ELSE CUST_F.CUST_NM_S                                                               " & vbNewLine _
                                          & "  END                                                  AS CUST_NM_S                        " & vbNewLine _
                                          & " ,CASE WHEN (CUST.CUST_NM_SS IS NOT NULL AND CUST.CUST_NM_SS <> '') THEN CUST.CUST_NM_SS   " & vbNewLine _
                                          & "       ELSE CUST_F.CUST_NM_SS                                                              " & vbNewLine _
                                          & "  END                                                  AS CUST_NM_SS                       " & vbNewLine _
                                          & " , @T_DATE                                             AS T_DATE                           " & vbNewLine _
                                          & " , NRS.NRS_BR_NM                                       AS NRS_BR_NM                        " & vbNewLine _
                                          & " , UNSO.INOUTKA_NO_L                                   AS INOUTKA_NO_L                     " & vbNewLine _
                                          & " , UNSO.MOTO_DATA_KB                                   AS MOTO_DATA_KB                     " & vbNewLine _
                                          & " , UNSO.OUTKA_PLAN_DATE                                AS OUTKA_PLAN_DATE                  " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD            " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' THEN UNSO.ORIG_CD                                    " & vbNewLine _
                                          & "   ELSE UNSO.DEST_CD                                                                       " & vbNewLine _
                                          & "   END                                                 AS DEST_CD                          " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM            " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.DEST_NM IS NOT NULL AND DEST3.DEST_NM <> '') THEN DEST3.DEST_NM " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.DEST_NM IS NOT NULL AND DEST4.DEST_NM <> '') THEN DEST4.DEST_NM " & vbNewLine _
                                          & "        WHEN (DEST.DEST_NM IS NOT NULL AND DEST.DEST_NM <> '') THEN DEST.DEST_NM           " & vbNewLine _
                                          & "        ELSE DEST2.DEST_NM                                                                 " & vbNewLine _
                                          & "   END                                                 AS DEST_NM                          " & vbNewLine _
                                          & " --(2012.09.21)要望番号1453 ⑨対応 --- START ---                                           " & vbNewLine _
                                          & " , CASE WHEN (DEST.SHIHARAI_AD IS NOT NULL AND DEST.SHIHARAI_AD <> '') THEN DEST.SHIHARAI_AD " & vbNewLine _
                                          & "        ELSE DEST2.SHIHARAI_AD                                                             " & vbNewLine _
                                          & "   END                                                 AS AD_1                             " & vbNewLine _
                                          & " --(2012.09.21)要望番号1453 ⑨対応 --- END ---                                             " & vbNewLine _
                                          & " , UNSOM.GOODS_NM                                      AS GOODS_NM                         " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_NG_NB                             AS DECI_NG_NB                       " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_KYORI                             AS DECI_KYORI                       " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_WT                                AS DECI_WT                          " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_UNCHIN                            AS DECI_UNCHIN                      " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_CITY_EXTC                         AS DECI_CITY_EXTC                   " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_WINT_EXTC                         AS DECI_WINT_EXTC                   " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_RELY_EXTC                         AS DECI_RELY_EXTC                   " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_TOLL                              AS DECI_TOLL                        " & vbNewLine _
                                          & " , SHIHARAI_TRS.DECI_INSU                              AS DECI_INSU                        " & vbNewLine _
                                          & " , SHIHARAI_TRS.REMARK                                 AS REMARK                           " & vbNewLine _
                                          & " , GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1                     " & vbNewLine _
                                          & " , JIS.SHI                                             AS SHI                              " & vbNewLine _
                                          & " , SHIHARAI_TRS.SHIHARAI_TARIFF_CD                     AS SHIHARAI_TARIFF_CD               " & vbNewLine _
                                          & " , SHIHARAI_TRS.SHIHARAI_FIXED_FLAG                    AS SHIHARAI_FIXED_FLAG              " & vbNewLine _
                                          & " , UNSO.UNSO_NO_L                                      AS UNSO_NO_L                        " & vbNewLine _
                                          & " , UNSO.ARR_PLAN_DATE                                  AS ARR_PLAN_DATE                    " & vbNewLine _
                                          & " , ISNULL(UNSOLL.UNSOCO_CD,UNSO.UNSO_CD)               AS UNSO_CD                          " & vbNewLine _
                                          & " , ISNULL(UNSOLL.UNSOCO_BR_CD,UNSO.UNSO_BR_CD)         AS UNSO_BR_CD                       " & vbNewLine _
                                          & " , ISNULL(UNSOCO2.UNSOCO_NM,UNSOCO.UNSOCO_NM)          AS UNSOCO_NM                        " & vbNewLine _
                                          & " , ISNULL(UNSOCO2.UNSOCO_BR_NM,UNSOCO.UNSOCO_BR_NM)    AS UNSOCO_BR_NM                     " & vbNewLine




#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = "FROM                                                               " & vbNewLine _
                                           & "	$LM_TRN$..F_SHIHARAI_TRS AS SHIHARAI_TRS                   " & vbNewLine _
                                           & "	--運送L				                                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO				       " & vbNewLine _
                                           & "	ON SHIHARAI_TRS.UNSO_NO_L=UNSO.UNSO_NO_L			       " & vbNewLine _
                                           & "  AND UNSO.SYS_DEL_FLG='0'				                   " & vbNewLine _
                                           & "	--運送LL				                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..F_UNSO_LL AS UNSOLL				       " & vbNewLine _
                                           & "	ON UNSOLL.TRIP_NO=UNSO.TRIP_NO			                   " & vbNewLine _
                                           & "  AND UNSOLL.SYS_DEL_FLG='0'				                   " & vbNewLine _
                                           & "	--運送Ｍ                                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM                      " & vbNewLine _
                                           & "	 ON SHIHARAI_TRS.UNSO_NO_L=UNSOM.UNSO_NO_L                 " & vbNewLine _
                                           & "	 AND(                                                      " & vbNewLine _
                                           & "			SELECT MIN(UNSO_NO_M)                              " & vbNewLine _
                                           & "			   FROM $LM_TRN$..F_UNSO_M UNSOM                   " & vbNewLine _
                                           & "			   WHERE UNSOM.UNSO_NO_L = SHIHARAI_TRS.UNSO_NO_L  " & vbNewLine _
                                           & "			     AND UNSOM.SYS_DEL_FLG='0'                     " & vbNewLine _
                                           & "	     )                                                     " & vbNewLine _
                                           & "	     = UNSOM.UNSO_NO_M                                     " & vbNewLine _
                                           & "	 AND UNSOM.SYS_DEL_FLG='0'                                 " & vbNewLine _
                                           & "	--出荷L				                                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..C_OUTKA_L AS OUTL				       " & vbNewLine _
                                           & "	ON  OUTL.NRS_BR_CD=UNSO.NRS_BR_CD				           " & vbNewLine _
                                           & "	AND OUTL.OUTKA_NO_L=UNSO.INOUTKA_NO_L			           " & vbNewLine _
                                           & "  AND UNSO.MOTO_DATA_KB='20'				                           " & vbNewLine _
                                           & "  AND OUTL.SYS_DEL_FLG='0'				                   " & vbNewLine _
                                           & "	--支払先マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_SHIHARAITO AS SHIHARAITO			   " & vbNewLine _
                                           & "	ON SHIHARAI_TRS.NRS_BR_CD = SHIHARAITO.NRS_BR_CD		   " & vbNewLine _
                                           & "	AND SHIHARAI_TRS.SHIHARAITO_CD = SHIHARAITO.SHIHARAITO_CD   " & vbNewLine _
                                           & "	--商品マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_GOODS AS GOODS				       " & vbNewLine _
                                           & "	ON SHIHARAI_TRS.NRS_BR_CD=GOODS.NRS_BR_CD				   " & vbNewLine _
                                           & "	AND UNSOM.GOODS_CD_NRS=GOODS.GOODS_CD_NRS				   " & vbNewLine _
                                           & "	--荷主マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST AS CUST				           " & vbNewLine _
                                           & "	ON GOODS.NRS_BR_CD=CUST.NRS_BR_CD				           " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_L=CUST.CUST_CD_L				           " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_M = CUST.CUST_CD_M				       " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_S= CUST.CUST_CD_S				           " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_SS= CUST.CUST_CD_SS				       " & vbNewLine _
                                           & " --荷主マスタ(Notes700)                                      " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_CUST AS CUST_F                      " & vbNewLine _
                                           & "   ON SHIHARAI_TRS.NRS_BR_CD   = CUST_F.NRS_BR_CD            " & vbNewLine _
                                           & "   AND SHIHARAI_TRS.CUST_CD_L  = CUST_F.CUST_CD_L            " & vbNewLine _
                                           & "   AND SHIHARAI_TRS.CUST_CD_M  = CUST_F.CUST_CD_M            " & vbNewLine _
                                           & "   AND SHIHARAI_TRS.CUST_CD_S  = CUST_F.CUST_CD_S            " & vbNewLine _
                                           & "   AND SHIHARAI_TRS.CUST_CD_SS = CUST_F.CUST_CD_SS           " & vbNewLine _
                                           & "	--営業所マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_NRS_BR AS NRS				           " & vbNewLine _
                                           & "	ON SHIHARAI_TRS.NRS_BR_CD = NRS.NRS_BR_CD				   " & vbNewLine _
                                           & "	--届先マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST				           " & vbNewLine _
                                           & "	ON UNSO.NRS_BR_CD = DEST.NRS_BR_CD				           " & vbNewLine _
                                           & "	AND UNSO.CUST_CD_L = DEST.CUST_CD_L				           " & vbNewLine _
                                           & "	AND UNSO.DEST_CD   = DEST.DEST_CD				           " & vbNewLine _
                                           & "	--届先マスタ				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST2				           " & vbNewLine _
                                           & "	ON UNSO.NRS_BR_CD = DEST2.NRS_BR_CD				       " & vbNewLine _
                                           & "	AND DEST2.CUST_CD_L = 'ZZZZZ'				               " & vbNewLine _
                                           & "	AND UNSO.DEST_CD   = DEST2.DEST_CD				           " & vbNewLine _
                                           & "	--届先マスタ(通常)                                         " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST3				           " & vbNewLine _
                                           & "	       ON UNSO.NRS_BR_CD  = DEST3.NRS_BR_CD				   " & vbNewLine _
                                           & "	      AND UNSO.CUST_CD_L  = DEST3.CUST_CD_L				   " & vbNewLine _
                                           & "	      AND UNSO.ORIG_CD    = DEST3.DEST_CD				   " & vbNewLine _
                                           & "	--届先マスタ(ZZZZZ対応)				                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST4				           " & vbNewLine _
                                           & "	       ON UNSO.NRS_BR_CD  = DEST4.NRS_BR_CD				   " & vbNewLine _
                                           & "	      AND DEST4.CUST_CD_L = 'ZZZZZ'				           " & vbNewLine _
                                           & "	      AND UNSO.ORIG_CD    = DEST4.DEST_CD				   " & vbNewLine _
                                           & "	--JISマスタ  				                               " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_JIS AS JIS				           " & vbNewLine _
                                           & "	ON JIS.JIS_CD   = DEST.JIS   		    		           " & vbNewLine _
                                           & "	-- 運送会社マスタ               	    		           " & vbNewLine _
                                           & "	LEFT  JOIN $LM_MST$..M_UNSOCO  AS UNSOCO   		           " & vbNewLine _
                                           & "	ON  UNSO.NRS_BR_CD    = UNSOCO.NRS_BR_CD   		           " & vbNewLine _
                                           & "	AND  UNSO.UNSO_CD     = UNSOCO.UNSOCO_CD   		           " & vbNewLine _
                                           & "	AND  UNSO.UNSO_BR_CD  = UNSOCO.UNSOCO_BR_CD		           " & vbNewLine _
                                           & "	AND  UNSO.SYS_DEL_FLG = '0'   		    		           " & vbNewLine _
                                           & "	-- 運送会社マスタ（運行)           	    		           " & vbNewLine _
                                           & "	LEFT  JOIN $LM_MST$..M_UNSOCO  AS UNSOCO2   		       " & vbNewLine _
                                           & "	ON  UNSOLL.NRS_BR_CD    = UNSOCO2.NRS_BR_CD   		       " & vbNewLine _
                                           & "	AND  UNSOLL.UNSOCO_CD     = UNSOCO2.UNSOCO_CD   		   " & vbNewLine _
                                           & "	AND  UNSOLL.UNSOCO_BR_CD  = UNSOCO2.UNSOCO_BR_CD		   " & vbNewLine _
                                           & "--支払運賃での荷主帳票パターン取得                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                          " & vbNewLine _
                                           & "ON  SHIHARAI_TRS.NRS_BR_CD = MCR1.NRS_BR_CD                  " & vbNewLine _
                                           & "AND SHIHARAI_TRS.CUST_CD_L = MCR1.CUST_CD_L                  " & vbNewLine _
                                           & "AND SHIHARAI_TRS.CUST_CD_M = MCR1.CUST_CD_M                  " & vbNewLine _
                                           & "AND '00' = MCR1.CUST_CD_S                                    " & vbNewLine _
                                           & "AND MCR1.PTN_ID = 'AF'                                       " & vbNewLine _
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
                                           & "AND MCR2.PTN_ID = 'AF'                                       " & vbNewLine _
                                           & "--帳票パターン取得                                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR2                                " & vbNewLine _
                                           & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                           " & vbNewLine _
                                           & "AND MR2.PTN_ID = MCR2.PTN_ID                                 " & vbNewLine _
                                           & "AND MR2.PTN_CD = MCR2.PTN_CD                                 " & vbNewLine _
                                           & "AND MR2.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "--存在しない場合の帳票パターン取得                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR3                                " & vbNewLine _
                                           & "ON  MR3.NRS_BR_CD = SHIHARAI_TRS.NRS_BR_CD                   " & vbNewLine _
                                           & "AND MR3.PTN_ID = 'AF'                                        " & vbNewLine _
                                           & "AND MR3.STANDARD_FLAG = '01'                                 " & vbNewLine _
                                           & "AND MR3.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "	  WHERE SHIHARAI_TRS.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                           & "	  AND   SHIHARAI_TRS.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                           & "    AND   SHIHARAI_TRS.DECI_UNCHIN <> 0                      " & vbNewLine _
                                           & "    AND   SHIHARAI_TRS.SHIHARAI_FIXED_FLAG = '01'            " & vbNewLine _
                                           & "--(2012.09.20)要望番号：1453 抽出条件追加 --- START ---      " & vbNewLine _
                                           & "    AND   UNSOLL.TRIP_NO IS NOT NULL                         " & vbNewLine _
                                           & "--(2012.09.20)要望番号：1453 抽出条件追加 ---  END  ---      " & vbNewLine


#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                " & vbNewLine _
                                         & "     SHIHARAI_TRS.SHIHARAITO_CD         " & vbNewLine _
                                         & "    ,ISNULL(UNSOLL.UNSOCO_CD,UNSO.UNSO_CD)                        " & vbNewLine _
                                         & "    ,ISNULL(UNSOLL.UNSOCO_BR_CD,UNSO.UNSO_BR_CD)                    " & vbNewLine _
                                         & "    ,UNSO.ARR_PLAN_DATE                 " & vbNewLine _
                                         & "    ,DEST_NM                            " & vbNewLine _
                                         & "    ,SHIHARAI_TRS.SHIHARAI_GROUP_NO     " & vbNewLine _
                                         & "    ,SHIHARAI_TRS.DECI_UNCHIN           " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMF600IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF600DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF600DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF600DAC", "SelectMPrt", cmd)

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
    ''' 支払運賃テーブル対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF600IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMF600DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF600DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMF600DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF600DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("SHIHARAI_GROUP_NO", "SHIHARAI_GROUP_NO")
        map.Add("SHIHARAITO_CD", "SHIHARAITO_CD")
        map.Add("SHIHARAITO_NM", "SHIHARAITO_NM")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
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
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("AD_1", "AD_1")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("REMARK", "REMARK")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SHI", "SHI")
        map.Add("SHIHARAI_TARIFF_CD", "SHIHARAI_TARIFF_CD")
        map.Add("SHIHARAI_FIXED_FLAG", "SHIHARAI_FIXED_FLAG")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF600OUT")

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

            '運送会社コード
            whereStr = Me._Row.Item("UNSO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND ISNULL(UNSOLL.UNSOCO_CD,UNSO.UNSO_CD) = @UNSO_CD                     ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me._Row.Item("UNSO_CD").ToString(), DBDataType.CHAR))
            End If

            '運送会社支店コード
            whereStr = Me._Row.Item("UNSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND ISNULL(UNSOLL.UNSOCO_BR_CD,UNSO.UNSO_BR_CD) = @UNSO_BR_CD                      ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me._Row.Item("UNSO_BR_CD").ToString(), DBDataType.CHAR))
            End If

            '支払先コード
            whereStr = Me._Row.Item("SHIHARAI_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND SHIHARAI_TRS.SHIHARAITO_CD = @SHIHARAI_CD                	      ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_CD", Me._Row.Item("SHIHARAI_CD").ToString(), DBDataType.CHAR))
            End If

            '日付
            '(2012.08.24) 要望番号1373 納入日で抽出 --- START ----
            whereStr = Me._Row.Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNSO.ARR_PLAN_DATE >= @F_DATE                    ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", Me._Row.Item("F_DATE").ToString(), DBDataType.CHAR))
            End If

            whereStr = Me._Row.Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNSO.ARR_PLAN_DATE <= @T_DATE                    ")
                Me._StrSql.Append(vbNewLine)
            End If

            'Me._StrSql.Append("	   AND ((                 	                            ")
            'Me._StrSql.Append(vbNewLine)
            'Me._StrSql.Append("	           SHIHARAI_TRS.UNTIN_CALCULATION_KB ='01'      ")
            'Me._StrSql.Append(vbNewLine)

            'whereStr = Me._Row.Item("F_DATE").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("	   AND UNSO.OUTKA_PLAN_DATE >= @F_DATE              ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", Me._Row.Item("F_DATE").ToString(), DBDataType.CHAR))
            'End If

            'whereStr = Me._Row.Item("T_DATE").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("	   AND UNSO.OUTKA_PLAN_DATE <= @T_DATE              ")
            '    Me._StrSql.Append("           )                                            ")
            '    Me._StrSql.Append(vbNewLine)
            'End If

            'Me._StrSql.Append("	   OR (                	                                ")
            'Me._StrSql.Append(vbNewLine)


            'Me._StrSql.Append("	           SHIHARAI_TRS.UNTIN_CALCULATION_KB ='02'      ")
            'Me._StrSql.Append(vbNewLine)
            'whereStr = Me._Row.Item("F_DATE").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("	   AND UNSO.ARR_PLAN_DATE >= @F_DATE                ")
            '    Me._StrSql.Append(vbNewLine)
            'End If

            'whereStr = Me._Row.Item("T_DATE").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("	   AND UNSO.ARR_PLAN_DATE <= @T_DATE                ")
            '    Me._StrSql.Append("           )                                            ")
            '    Me._StrSql.Append(vbNewLine)
            'End If

            'Me._StrSql.Append("	   OR (                	                                ")
            'Me._StrSql.Append(vbNewLine)


            'Me._StrSql.Append("	           SHIHARAI_TRS.UNTIN_CALCULATION_KB =''        ")
            'Me._StrSql.Append(vbNewLine)
            'Me._StrSql.Append("	    AND ((                	                            ")
            'Me._StrSql.Append(vbNewLine)

            'whereStr = Me._Row.Item("F_DATE").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("	       UNSO.OUTKA_PLAN_DATE >= @F_DATE              ")
            '    Me._StrSql.Append(vbNewLine)
            'End If

            'whereStr = Me._Row.Item("T_DATE").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("	   AND UNSO.OUTKA_PLAN_DATE <= @T_DATE              ")
            '    Me._StrSql.Append("           )                                            ")
            '    Me._StrSql.Append(vbNewLine)
            'End If

            'Me._StrSql.Append(" 	   OR (                	                                ")
            'Me._StrSql.Append(vbNewLine)

            'whereStr = Me._Row.Item("F_DATE").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("	       UNSO.ARR_PLAN_DATE >= @F_DATE                ")
            '    Me._StrSql.Append(vbNewLine)
            'End If

            'whereStr = Me._Row.Item("T_DATE").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("	   AND UNSO.ARR_PLAN_DATE <= @T_DATE                ")
            '    Me._StrSql.Append("           ))                                           ")
            '    Me._StrSql.Append(vbNewLine)
            'End If

            'Me._StrSql.Append("	   ))                 	                                ")
            'Me._StrSql.Append(vbNewLine)

            '(2012.08.24) 要望番号1373 納入日で抽出 --- END  ----
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
