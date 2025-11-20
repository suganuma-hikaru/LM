' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB560    : 運送保険
'  作  成  者       :  daikoku
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB560DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB560DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "   INKAL.NRS_BR_CD                                  AS NRS_BR_CD  " & vbNewLine _
                                            & "  ,'DC'                                             AS PTN_ID     " & vbNewLine _
                                            & "  ,'00'                                             AS PTN_CD     " & vbNewLine _
                                            & "  ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
                                            & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           " & vbNewLine _
                                            & "            ELSE MR3.RPT_ID                                       " & vbNewLine _
                                            & "   END                                              AS RPT_ID     " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用SELECT区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "	 SELECT                                                        	 " & vbNewLine _
                                        & "	   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                	 " & vbNewLine _
                                        & "	            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           	 " & vbNewLine _
                                        & "	            ELSE MR3.RPT_ID                                       	 " & vbNewLine _
                                        & "	   END                                               AS RPT_ID    	 " & vbNewLine _
                                        & "	  ,INKAL.INKA_DATE                                   AS INKA_DATE    " & vbNewLine _
                                        & "--	  ,GOODS.GOODS_NM_1                                  AS GOODS_NM_1   " & vbNewLine _
                                        & "	  ,INKAM_MATOME.GOODS_NM_1                           AS GOODS_NM_1   " & vbNewLine _
                                        & "	  ,ISNULL(CUST.CUST_NM_L,'')                         AS CUST_NM_L    " & vbNewLine _
                                        & "	  --①数量　⇒　（数量/標準数量）×標準重量に変更                    " & vbNewLine _
                                        & "	--    ,((ISNULL(INKAS.KONSU , 0)                                     	 " & vbNewLine _
                                        & "	--      *  ISNULL(GOODS.PKG_NB , 0)                                  	 " & vbNewLine _
                                        & "	--      +  ISNULL(INKAS.HASU   , 0))                                 	 " & vbNewLine _
                                        & "	--      *  ISNULL(INKAS.IRIME  , 0))                                 	 " & vbNewLine _
                                        & "	--      * GOODS.STD_WT_KGS                                           	 " & vbNewLine _
                                        & "	--      / GOODS.STD_IRIME_NB                                        	 " & vbNewLine _
                                        & "	  ,INKAM_MATOME.SUM_JURYO_M AS SUM_JURYO_M  --重量	                     " & vbNewLine _
                                        & "--	  ,GOODS.KITAKU_GOODS_UP               AS KITAKU_GOODS_UP    --単価  " & vbNewLine _
                                        & "	  ,INKAM_MATOME.KITAKU_GOODS_UP        AS KITAKU_GOODS_UP    --単価  " & vbNewLine _
                                        & "	  ,ISNULL(KBN1.KBN_NM2,0)              AS HOKENRITSU         --保険料率    " & vbNewLine _
                                        & "--	  ,ROUND(((CASE WHEN  GOODS.STD_IRIME_NB = GOODS.STD_WT_KGS         THEN INKAM_MATOME.SUM_JURYO_M        	 " & vbNewLine _
                                        & "--	                ELSE INKAM_MATOME.SUM_JURYO_M / ISNULL(GOODS.STD_IRIME_NB,0) * ISNULL(GOODS.STD_WT_KGS,0) 	 " & vbNewLine _
                                        & "--	                END * GOODS.KITAKU_GOODS_UP ) * KBN1.KBN_NM2),0)      AS HOKENRYO          --保険料    	     " & vbNewLine _
                                        & "--   ,ROUND(((((INKAM_MATOME.SUM_JURYO_M  + (CASE WHEN GOODS.TARE_YN  = '01'                                            " & vbNewLine _
                                        & "--                                        THEN INKAM_MATOME.SUM_KONSU * KBN3.VALUE1         　                         " & vbNewLine _
                                        & "--                                        ELSE 0 END))   * GOODS.KITAKU_GOODS_UP )) * KBN1.KBN_NM2),0)      AS HOKENRYO          --保険料    	     " & vbNewLine _
                                        & "	  ,ROUND(ROUND(GOODS.KITAKU_GOODS_UP * (INKAM_MATOME.SUM_JURYO_M) / 1000, 0) * 1000 * KBN1.KBN_NM2, 0) AS HOKENRYO --保険料 " & vbNewLine _
                                        & "	  ,M_DEST.DEST_NM                                        AS WH_NM  " & vbNewLine _
                                        & "	  ,M_DEST.AD_1 + M_DEST.AD_2 + M_DEST.AD_3                                          AS BRAD_1 " & vbNewLine _
                                        & "	  ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき	           " & vbNewLine _
                                        & "	             THEN MTS.TASYA_AD_1 + MTS.TASYA_AD_2 + MTS.TASYA_AD_3	                               " & vbNewLine _
                                        & "	             ELSE SOKO.AD_1 + SOKO.AD_2 + SOKO.AD_3 	                                   " & vbNewLine _
                                        & "	     END   AS DEST_ADD 	                                           " & vbNewLine _
                                        & "	 ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき	           " & vbNewLine _
                                        & "	             THEN MTS.TASYA_WH_NM	                               " & vbNewLine _
                                        & "	             ELSE SOKO.WH_NM 	                                   " & vbNewLine _
                                        & "	   END   AS DEST_NM  	                                           " & vbNewLine _
                                        & "	  ,UNSOL.REMARK                        AS REMARK             --備考                  " & vbNewLine _
                                        & "	  ,INKAM_MATOME.INKA_NO_L              AS INKA_NO_L         --入荷管理番号L        	 " & vbNewLine _
                                        & "	  ,INKAM_MATOME.INKA_NO_M              AS INKA_NO_M         --入荷管理番号M        	 " & vbNewLine _
                                        & "	  ,ISNULL(KBN2.KBN_NM1,'')             AS KITAKU_GOODS_UPNM  --寄託価格単位区分名  	 " & vbNewLine _

    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "	 FROM $LM_TRN$..B_INKA_L INKAL                               	                 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..M_CUST  CUST                            	                 " & vbNewLine _
                                        & "	      ON CUST.NRS_BR_CD   = INKAL.NRS_BR_CD                   	                 " & vbNewLine _
                                        & "	      AND CUST.CUST_CD_L   = INKAL.CUST_CD_L                                 	 " & vbNewLine _
                                        & "	      AND CUST.CUST_CD_M   = INKAL.CUST_CD_M                                	 " & vbNewLine _
                                        & "	      AND CUST.CUST_CD_S   = '00'                                           	 " & vbNewLine _
                                        & "	      AND CUST.CUST_CD_SS  = '00'      	                                         " & vbNewLine _
                                        & "	 LEFT JOIN $LM_TRN$..B_INKA_M INKAM                         	                 " & vbNewLine _
                                        & "	     ON INKAM.NRS_BR_CD   = INKAL.NRS_BR_CD                                   	 " & vbNewLine _
                                        & "	     AND INKAM.INKA_NO_L  = INKAL.INKA_NO_L                                   	 " & vbNewLine _
                                        & "	     AND INKAM.SYS_DEL_FLG = '0' 	                                             " & vbNewLine _
                                        & "	 LEFT JOIN (	                                                                 " & vbNewLine _
                                        & "	             SELECT 	                                                         " & vbNewLine _
                                        & "	                   INKAM.INKA_NO_L	                                             " & vbNewLine _
                                        & "                ,INKAM.INKA_NO_M	                                             " & vbNewLine _
                                        & "	--                ,SUM((ISNULL(INKAS.KONSU , 0)                                  	 " & vbNewLine _
                                        & "	--                                           *  ISNULL(GOODS.PKG_NB , 0)         	 " & vbNewLine _
                                        & "	--                  +  ISNULL(INKAS.HASU   , 0))                                 	 " & vbNewLine _
                                        & "	--                 *  ISNULL(INKAS.IRIME  , 0))                                 	 " & vbNewLine _
                                        & "	--                * GOODS.STD_WT_KGS                                           	 " & vbNewLine _
                                        & "	--               / GOODS.STD_IRIME_NB                                        	     " & vbNewLine _
                                        & "	--                                                     AS      SUM_JURYO_M  	     " & vbNewLine _
                                        & "	             ,SUM(FLOOR((INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU)                 " & vbNewLine _
                                        & "	                * INKAS.IRIME                                                     " & vbNewLine _
                                        & "	                * GOODS.STD_WT_KGS                                                " & vbNewLine _
                                        & "	                / GOODS.STD_IRIME_NB * 1000) / 1000)      AS SUM_JURYO_M         " & vbNewLine _
                                        & "             ,SUM(INKAS.KONSU)                             AS SUM_KONSU           " & vbNewLine _
                                        & "	            ,GOODS.GOODS_NM_1                                                    " & vbNewLine _
                                        & "	            ,GOODS.KITAKU_GOODS_UP                                               " & vbNewLine _
                                        & "	           FROM  $LM_TRN$..B_INKA_M INKAM	                                     " & vbNewLine _
                                        & "	           LEFT JOIN $LM_TRN$..B_INKA_S INKAS                         	         " & vbNewLine _
                                        & "	                      ON INKAS.NRS_BR_CD   = INKAM.NRS_BR_CD                   	 " & vbNewLine _
                                        & "	                  AND INKAS.INKA_NO_L = INKAM.INKA_NO_L  	                     " & vbNewLine _
                                        & "	                 AND INKAS.INKA_NO_M  = INKAM.INKA_NO_M  	                     " & vbNewLine _
                                        & "	                 AND INKAS.SYS_DEL_FLG = '0'                                	 " & vbNewLine _
                                        & "	             LEFT JOIN $LM_MST$..M_GOODS GOODS                              	 " & vbNewLine _
                                        & "	                   ON GOODS.NRS_BR_CD     = INKAM.NRS_BR_CD                  	 " & vbNewLine _
                                        & "	                AND GOODS.GOODS_CD_NRS  = INKAM.GOODS_CD_NRS                	 " & vbNewLine _
                                        & "	                AND GOODS.SYS_DEL_FLG   = '0'	                                 " & vbNewLine _
                                        & "	             WHERE	                                                             " & vbNewLine _
                                        & "	                         INKAM.NRS_BR_CD   = @NRS_BR_CD          ----------        	 " & vbNewLine _
                                        & "	                AND INKAM.INKA_NO_L  = INKAM.INKA_NO_L                      	 " & vbNewLine _
                                        & "	               AND INKAM.INKA_NO_M  =INKAM.INKA_NO_M	                         " & vbNewLine _
                                        & "	               AND INKAM.SYS_DEL_FLG = '0'	                                     " & vbNewLine _
                                        & "	               AND GOODS.UNSO_HOKEN_YN = '01'	--運送保険あり　                  " & vbNewLine _
                                        & "	            GROUP BY 	                                                         " & vbNewLine _
                                        & "	                  INKAM.INKA_NO_L	                                             " & vbNewLine _
                                        & "	                 ,INKAM.INKA_NO_M	                                             " & vbNewLine _
                                        & "	                ,GOODS.STD_WT_KGS                                           	 " & vbNewLine _
                                        & "	                ,GOODS.STD_IRIME_NB	                                             " & vbNewLine _
                                        & "	                ,GOODS.GOODS_NM_1                                                " & vbNewLine _
                                        & "	                ,GOODS.KITAKU_GOODS_UP                                           " & vbNewLine _
                                        & "	            ) AS INKAM_MATOME            	                                     " & vbNewLine _
                                        & "	        ON   INKAM_MATOME.INKA_NO_L = INKAL.INKA_NO_L	                         " & vbNewLine _
                                        & "	        AND  INKAM_MATOME.INKA_NO_M = INKAM.INKA_NO_M                            " & vbNewLine _
                                        & "	    LEFT JOIN   $LM_TRN$..B_INKA_S INKAS	                                     " & vbNewLine _
                                        & "	         ON	                                                                     " & vbNewLine _
                                        & "	             INKAS.NRS_BR_CD   = @NRS_BR_CD          ----------                  " & vbNewLine _
                                        & "	         AND INKAS.INKA_NO_L  = INKAM.INKA_NO_L                               	 " & vbNewLine _
                                        & "	         AND INKAS.INKA_NO_M  = INKAM.INKA_NO_M	                                 " & vbNewLine _
                                        & "	         AND INKAS.SYS_DEL_FLG = '0' 	                                         " & vbNewLine _
                                        & "	   LEFT JOIN $LM_MST$..M_TOU_SITU MTS                         	 " & vbNewLine _
                                        & "	         ON MTS.WH_CD   = INKAL.WH_CD                        	 " & vbNewLine _
                                        & "	      AND MTS.TOU_NO  = INKAS.TOU_NO    	                     " & vbNewLine _
                                        & "	     AND MTS.SITU_NO  = INKAS.SITU_NO    	                     " & vbNewLine _
                                        & "	     AND MTS.SYS_DEL_FLG = '0' 	                                 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..M_GOODS GOODS                             	 " & vbNewLine _
                                        & "	       ON GOODS.NRS_BR_CD     = INKAM.NRS_BR_CD                  " & vbNewLine _
                                        & "	    AND GOODS.GOODS_CD_NRS  = INKAM.GOODS_CD_NRS               	 " & vbNewLine _
                                        & "	    AND GOODS.SYS_DEL_FLG   = '0'                            	 " & vbNewLine _
                                        & "	    AND GOODS.UNSO_HOKEN_YN = '01'   --運送保険有り時        	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..Z_KBN  KBN1                           	     " & vbNewLine _
                                        & "	       ON KBN1.KBN_GROUP_CD  = 'H027'                          	 " & vbNewLine _
                                        & "	     AND KBN1.KBN_NM1       = INKAL.NRS_BR_CD                 	 " & vbNewLine _
                                        & "	     AND KBN1.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..Z_KBN  KBN2                              	 " & vbNewLine _
                                        & "	   ON KBN2.KBN_GROUP_CD  = 'T003'                            	 " & vbNewLine _
                                        & "	  AND KBN2.KBN_CD        = GOODS.KITAKU_AM_UT_KB             	 " & vbNewLine _
                                        & "	  AND KBN2.SYS_DEL_FLG   = '0'                               	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..Z_KBN  KBN3                              	 " & vbNewLine _
                                        & "	   ON KBN3.KBN_GROUP_CD  = 'N001'                            	 " & vbNewLine _
                                        & "	  AND KBN3.KBN_CD        = GOODS.PKG_UT                          " & vbNewLine _
                                        & "	  AND KBN3.SYS_DEL_FLG   = '0'                               	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..M_SOKO SOKO                           	     " & vbNewLine _
                                        & "	   ON SOKO.NRS_BR_CD   = INKAL.NRS_BR_CD                     	 " & vbNewLine _
                                        & "	  AND SOKO.WH_CD       = INKAL.WH_CD                         	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..M_NRS_BR   BR                          	 " & vbNewLine _
                                        & "	   ON BR.NRS_BR_CD   = INKAL.NRS_BR_CD                       	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                          	 " & vbNewLine _
                                        & "	   ON UNSOL.NRS_BR_CD   = INKAL.NRS_BR_CD                    	 " & vbNewLine _
                                        & "	  AND UNSOL.INOUTKA_NO_L  = INKAL.INKA_NO_L  	                 " & vbNewLine _
                                        & "	  AND  UNSOL.MOTO_DATA_KB = '10' 	                             " & vbNewLine _
                                        & "	  AND UNSOL.SYS_DEL_FLG = '0'                                	 " & vbNewLine _
                                        & "	 LEFT JOIN                                                       " & vbNewLine _
                                        & "	   $LM_MST$..M_DEST                       M_DEST               	 " & vbNewLine _
                                        & "	ON                                                               " & vbNewLine _
                                        & "	     INKAL.NRS_BR_CD = M_DEST.NRS_BR_CD                        	 " & vbNewLine _
                                        & "	 AND INKAL.CUST_CD_L = M_DEST.CUST_CD_L                          " & vbNewLine _
                                        & "	 AND UNSOL.ORIG_CD   = M_DEST.DEST_CD   	                     " & vbNewLine _
                                        & "	LEFT JOIN $LM_MST$..M_SOKO               M_SOKO                	 " & vbNewLine _
                                        & "	ON                                                             	 " & vbNewLine _
                                        & "	M_SOKO.NRS_BR_CD = INKAL.NRS_BR_CD                            	 " & vbNewLine _
                                        & "	AND                                                           	 " & vbNewLine _
                                        & "	M_SOKO.WH_CD = INKAL.WH_CD                                     	 " & vbNewLine _
                                        & "	  --荷主での荷主帳票パターン取得                           	     " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                       	 " & vbNewLine _
                                        & "	   ON MCR1.NRS_BR_CD = INKAL.NRS_BR_CD                       	 " & vbNewLine _
                                        & "	  AND MCR1.CUST_CD_L = INKAL.CUST_CD_L                       	 " & vbNewLine _
                                        & "	  AND MCR1.CUST_CD_M = INKAL.CUST_CD_M                       	 " & vbNewLine _
                                        & "	  AND MCR1.CUST_CD_S = '00'                                	 " & vbNewLine _
                                        & "	  AND MCR1.PTN_ID    = 'DC'                                	 " & vbNewLine _
                                        & "	  --帳票パターン取得                                       	 " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_RPT MR1                            	 " & vbNewLine _
                                        & "	   ON MR1.NRS_BR_CD   = INKAL.NRS_BR_CD                    	 " & vbNewLine _
                                        & "	  AND MR1.PTN_ID      = 'DC'                               	 " & vbNewLine _
                                        & "	  AND MR1.SYS_DEL_FLG = '0'                                	 " & vbNewLine _
                                        & "	  --商品Mの荷主での荷主帳票パターン取得                    	 " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                      	 " & vbNewLine _
                                        & "	    ON MCR2.NRS_BR_CD = GOODS.NRS_BR_CD                    	 " & vbNewLine _
                                        & "	   AND MCR2.CUST_CD_L = GOODS.CUST_CD_L                    	 " & vbNewLine _
                                        & "	   AND MCR2.CUST_CD_M = GOODS.CUST_CD_M                    	 " & vbNewLine _
                                        & "	   AND MCR2.CUST_CD_S = GOODS.CUST_CD_S                    	 " & vbNewLine _
                                        & "	   AND MCR2.PTN_ID    = 'DC'                               	 " & vbNewLine _
                                        & "	  --帳票パターン取得                                       	 " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_RPT MR2                            	 " & vbNewLine _
                                        & "	    ON MR2.NRS_BR_CD     = MCR2.NRS_BR_CD                  	 " & vbNewLine _
                                        & "	   AND MR2.PTN_ID        = MCR2.PTN_ID                     	 " & vbNewLine _
                                        & "	   AND MR2.PTN_CD        = MCR2.PTN_CD                     	 " & vbNewLine _
                                        & "	   AND MR2.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & "	  --存在しない場合の帳票パターン取得                       	 " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_RPT MR3                            	 " & vbNewLine _
                                        & "	    ON MR3.NRS_BR_CD     = INKAL.NRS_BR_CD                   " & vbNewLine _
                                        & "	   AND MR3.PTN_ID        = 'DC'                            	 " & vbNewLine _
                                        & "	   AND MR3.STANDARD_FLAG = '01'                            	 " & vbNewLine _
                                        & "	   AND MR3.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & "	 WHERE INKAL.SYS_DEL_FLG = '0'                             	 " & vbNewLine _
                                        & "	   AND INKAL.NRS_BR_CD   =  @NRS_BR_CD               	     " & vbNewLine _
                                        & "	   AND INKAL.INKA_NO_L   =  @INKA_NO_L                    	 " & vbNewLine _
                                        & "	   AND GOODS.UNSO_HOKEN_YN = '01'                          	 " & vbNewLine _
                                        & "	   AND ((@UNSO_TEHAI_CHK = '1'                              	         " & vbNewLine _
                                        & "	       AND UNSOL.UNSO_TEHAI_KB =  '10')   --日陸手配のみ　 	 " & vbNewLine _
                                        & "	     OR (@UNSO_TEHAI_CHK <> '1' ))                             	         " & vbNewLine _



    ''' <summary>
    ''' ORDER BY（①営業所コード、②管理番号L、、③管理番号M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "	ORDER BY                     	 " & vbNewLine _
                                        & "	      INKAM.NRS_BR_CD          	 " & vbNewLine _
                                        & "	    , INKAM.INKA_NO_L         	 " & vbNewLine _
                                        & "	    , INKAM.INKA_NO_M   	     " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMB560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB560DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMB560DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.setIndataParameter(Me._Row)               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB560DAC", "SelectMPrt", cmd)

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
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB560DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMB560DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.setIndataParameter(Me._Row)               '条件設定
        Me._StrSql.Append(LMB560DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB560DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("SUM_JURYO_M", "SUM_JURYO_M")
        map.Add("KITAKU_GOODS_UP", "KITAKU_GOODS_UP")
        map.Add("HOKENRITSU", "HOKENRITSU")
        map.Add("HOKENRYO", "HOKENRYO")
        map.Add("WH_NM", "WH_NM")
        map.Add("BRAD_1", "BRAD_1")
        map.Add("DEST_ADD", "DEST_ADD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("REMARK", "REMARK")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("KITAKU_GOODS_UPNM", "KITAKU_GOODS_UPNM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB560OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setIndataParameter(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_CHK", .Item("UNSO_TEHAI_CHK").ToString(), DBDataType.CHAR))

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
