' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF534DAC : 運賃ﾁｪｯｸﾘｽﾄ(運行基準)
'  作  成  者       :  黎
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF534DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF534DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	      UNSO_LL.NRS_BR_CD                                AS NRS_BR_CD " & vbNewLine _
                                            & "     , 'AZ'                                             AS PTN_ID    " & vbNewLine _
                                            & "     , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "		       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD                                               " & vbNewLine _
                                            & "	 	  END                                              AS PTN_CD    " & vbNewLine _
                                            & "     , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "  	 	   WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID                                               " & vbNewLine _
                                            & "		  END                                              AS RPT_ID    " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "	SELECT																				 " & vbNewLine _
                                            & "	 CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID									 " & vbNewLine _
                                            & "	  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID										 " & vbNewLine _
                                            & "   ELSE MR3.RPT_ID																	 " & vbNewLine _
                                            & "	 END																AS RPT_ID	     " & vbNewLine _
                                            & " ,UNSO_LL.TRIP_NO													AS TRIP_NO	     " & vbNewLine _
                                            & "	,UNSO_LL.UNSOCO_CD													AS UNSOCO_CD     " & vbNewLine _
                                            & "	,VCLE.CAR_NO														AS CAR_NO	     " & vbNewLine _
                                            & "	,UNSO_LL.TRIP_DATE													AS TRIP_DATE     " & vbNewLine _
                                            & "	--,UNSO_L.CUST_CD_L													AS CUST_CD_L     " & vbNewLine _
                                            & "	,UNSO_L.UNSO_NO_L													AS UNSO_NO_L     " & vbNewLine _
                                            & "	,UNSO_L.CUST_REF_NO													AS CUST_REF_NO   " & vbNewLine _
                                            & "	,UNSO_L.DEST_CD														AS DEST_CD		 " & vbNewLine _
                                            & "	--20121001 TEST 追加-(LMF530から追加)------------------------------------------------" & vbNewLine _
                                            & "	,CASE																				 " & vbNewLine _
                                            & "	  WHEN UNSO_L.MOTO_DATA_KB = '20'					AND								 " & vbNewLine _
                                            & "		OUTL.DEST_KB = '01'    THEN OUTL.DEST_NM										 " & vbNewLine _
                                            & "	  WHEN UNSO_L.MOTO_DATA_KB = '20'					AND								 " & vbNewLine _
                                            & "		OUTL.DEST_KB = '02'    THEN EDIL.DEST_NM										 " & vbNewLine _
                                            & "	  WHEN UNSO_L.MOTO_DATA_KB = '10'					AND								 " & vbNewLine _
                                            & "		(DEST03.DEST_NM IS NOT NULL						AND							 	 " & vbNewLine _
                                            & "		DEST03.DEST_NM <> '')  THEN DEST03.DEST_NM										 " & vbNewLine _
                                            & "	  WHEN UNSO_L.MOTO_DATA_KB = '10'					AND							 	 " & vbNewLine _
                                            & "		(DEST04.DEST_NM IS NOT NULL						AND							 	 " & vbNewLine _
                                            & "		DEST04.DEST_NM <>  '') THEN DEST04.DEST_NM										 " & vbNewLine _
                                            & "	  WHEN (DEST.DEST_NM IS NOT NULL					AND								 " & vbNewLine _
                                            & "		DEST.DEST_NM <> '')    THEN DEST.DEST_NM										 " & vbNewLine _
                                            & "	  ELSE DEST2.DEST_NM																 " & vbNewLine _
                                            & "  END																AS DEST_NM	     " & vbNewLine _
                                            & "	--20121001 TEST 追加-(LMF530から追加)------------------------------------------------" & vbNewLine _
                                            & "	,UNCHIN_TRS.UNSO_NO_M												AS UC_UNSO_NO_M  " & vbNewLine _
                                            & "	,UNCHIN_TRS.SEIQ_GROUP_NO											AS SEIQ_GROUP_NO " & vbNewLine _
                                            & "	,UNCHIN_TRS.SEIQ_TARIFF_CD											AS SEIQ_TARIF_CD " & vbNewLine _
                                            & "	,UNCHIN_TRS.SEIQ_KYORI												AS SEIQ_KYORI	 " & vbNewLine _
                                            & "	,UNCHIN_TRS.DECI_UNCHIN												AS DECI_UNCHIN	 " & vbNewLine _
                                            & "	--20121001 TEST 追加-(LMF530から追加)------------------------------------------------" & vbNewLine _
                                            & " ,CASE																				 " & vbNewLine _
                                            & "	  WHEN UNSO_L.MOTO_DATA_KB = '20'					AND								 " & vbNewLine _
                                            & "		OUTL.DEST_KB = '01'	THEN OUTL.DEST_AD_1											 " & vbNewLine _
                                            & "	  WHEN UNSO_L.MOTO_DATA_KB = '20'					AND								 " & vbNewLine _
                                            & "		OUTL.DEST_KB = '02'	THEN EDIL.DEST_AD_1											 " & vbNewLine _
                                            & "	  WHEN UNSO_L.MOTO_DATA_KB = '10'					AND								 " & vbNewLine _
                                            & "		(DEST03.AD_1 IS NOT NULL						AND								 " & vbNewLine _
                                            & "		DEST03.AD_1 <> '')  THEN DEST03.AD_1											 " & vbNewLine _
                                            & "	  ELSE DEST.AD_1																	 " & vbNewLine _
                                            & "	 END																AS DEST_AD_1	 " & vbNewLine _
                                            & "	--20121001 TEST 追加-(LMF530から追加)------------------------------------------------" & vbNewLine _
                                            & "	,UNSO_M.UNSO_NO_M													AS UNSO_NO_M	 " & vbNewLine _
                                            & "	,UNSO_M.GOODS_NM													AS GOODS_NM		 " & vbNewLine _
                                            & "	,UNSO_M.UNSO_TTL_NB													AS UNSO_TTL_NB	 " & vbNewLine _
                                            & "	,UNSO_M.NB_UT														AS NB_UT		 " & vbNewLine _
                                            & "	,UNSO_M.BETU_WT														AS BETU_WT		 " & vbNewLine _
                                            & "	--,UNSOCO_MST.NM													AS UNSO_NO_M     " & vbNewLine _
                                            & "	,UNSOCO_MST.UNSOCO_NM												AS UNSOCO_NM	 " & vbNewLine _
                                            & "	,Z_KBN.KBN_NM1   											        AS BIN_NM        " & vbNewLine _
                                            & "	,UNSO_L.ARR_PLAN_DATE												AS ARR_PLAN_DATE " & vbNewLine _
                                            & "	,UNSO_L.ARR_PLAN_TIME												AS ARR_PLAN_TIME " & vbNewLine
#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = "	FROM $LM_TRN$..F_UNSO_LL UNSO_LL													 " & vbNewLine _
                                            & "	LEFT OUTER JOIN $LM_MST$..M_DRIVER DRIVER_MST									ON	 " & vbNewLine _
                                            & "	 UNSO_LL.DRIVER_CD = DRIVER_MST.DRIVER_CD											 " & vbNewLine _
                                            & "	 INNER JOIN $LM_MST$..M_NRS_BR NRS_BR_MST										ON	 " & vbNewLine _
                                            & "	 NRS_BR_MST.SYS_DEL_FLG              = '0'			AND								 " & vbNewLine _
                                            & "	 UNSO_LL.NRS_BR_CD = NRS_BR_MST.NRS_BR_CD											 " & vbNewLine _
                                            & "	LEFT OUTER JOIN $LM_TRN$..F_UNSO_L UNSO_L										ON	 " & vbNewLine _
                                            & "	 UNSO_LL.NRS_BR_CD = UNSO_L.NRS_BR_CD				AND								 " & vbNewLine _
                                            & "	 UNSO_LL.TRIP_NO = UNSO_L.TRIP_NO													 " & vbNewLine _
                                            & "	LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UNCHIN_TRS										ON	 " & vbNewLine _
                                            & "	 UNSO_L.NRS_BR_CD = UNCHIN_TRS.NRS_BR_CD			AND								 " & vbNewLine _
                                            & "	 UNSO_L.UNSO_NO_L = UNCHIN_TRS.UNSO_NO_L											 " & vbNewLine _
                                            & "	LEFT OUTER JOIN $LM_TRN$..F_UNSO_M UNSO_M										ON	 " & vbNewLine _
                                            & "	 UNSO_L.NRS_BR_CD = UNSO_M.NRS_BR_CD				AND								 " & vbNewLine _
                                            & "	 UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L												 " & vbNewLine _
                                            & "	LEFT OUTER JOIN $LM_MST$..M_UNSOCO UNSOCO_MST									ON	 " & vbNewLine _
                                            & "	 UNSO_LL.NRS_BR_CD = UNSOCO_MST.NRS_BR_CD			AND								 " & vbNewLine _
                                            & "	 UNSO_LL.UNSOCO_CD = UNSOCO_MST.UNSOCO_CD			AND								 " & vbNewLine _
                                            & "	 UNSO_LL.UNSOCO_BR_CD = UNSOCO_MST.UNSOCO_BR_CD										 " & vbNewLine _
                                            & "      --区分Ｍ(運送便区分)--20121122LMF620より抜粋追加                                " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN Z_KBN                                                 ON   " & vbNewLine _
                                            & "  Z_KBN.KBN_GROUP_CD = 'U001'                        AND                              " & vbNewLine _
                                            & "  Z_KBN.KBN_CD = UNSO_L.BIN_KB                                                        " & vbNewLine _
                                            & "	--20121001 TEST 追加-(LMF530から追加)------------------------------------------------" & vbNewLine _
                                            & "	--届け先マスタ																		 " & vbNewLine _
                                            & "	LEFT JOIN $LM_MST$..M_DEST											AS DEST		ON	 " & vbNewLine _
                                            & "	 UNSO_L.DEST_CD=DEST.DEST_CD						AND								 " & vbNewLine _
                                            & "	 UNSO_L.NRS_BR_CD =DEST.NRS_BR_CD					AND								 " & vbNewLine _
                                            & "	 UNSO_L.CUST_CD_L = DEST.CUST_CD_L													 " & vbNewLine _
                                            & "	--届先マスタ																		 " & vbNewLine _
                                            & "	LEFT JOIN $LM_MST$..M_DEST											AS DEST2	ON	 " & vbNewLine _
                                            & "	 UNSO_L.DEST_CD   = DEST2.DEST_CD					AND								 " & vbNewLine _
                                            & "	 UNSO_L.NRS_BR_CD = DEST2.NRS_BR_CD					AND								 " & vbNewLine _
                                            & "	 DEST2.CUST_CD_L = 'ZZZZZ'															 " & vbNewLine _
                                            & "	--① (F_UNSO_L) CUST_CD_Lで届先Mに存在する場合										 " & vbNewLine _
                                            & "	LEFT JOIN $LM_MST$..M_DEST											AS DEST03	ON	 " & vbNewLine _
                                            & "	 UNSO_L.ORIG_CD   = DEST03.DEST_CD					AND								 " & vbNewLine _
                                            & "	 UNSO_L.NRS_BR_CD = DEST03.NRS_BR_CD				AND								 " & vbNewLine _
                                            & "	 UNSO_L.CUST_CD_L = DEST03.CUST_CD_L												 " & vbNewLine _
                                            & "	--②①で存在せず、(M_DEST) CUST_CD_L='ZZZZZ'で届先Mに存在する場合					 " & vbNewLine _
                                            & "	LEFT JOIN $LM_MST$..M_DEST											AS DEST04	ON	 " & vbNewLine _
                                            & "	 UNSO_L.ORIG_CD = DEST04.DEST_CD					AND								 " & vbNewLine _
                                            & "	 UNSO_L.NRS_BR_CD = DEST04.NRS_BR_CD				AND								 " & vbNewLine _
                                            & "	 DEST04.CUST_CD_L = 'ZZZZZ'															 " & vbNewLine _
                                            & "	--出荷L																				 " & vbNewLine _
                                            & "	LEFT JOIN $LM_TRN$..C_OUTKA_L 										AS OUTL 	ON	 " & vbNewLine _
                                            & "	 OUTL.NRS_BR_CD=UNSO_L.NRS_BR_CD					AND								 " & vbNewLine _
                                            & "	 OUTL.OUTKA_NO_L=UNSO_L.INOUTKA_NO_L				AND								 " & vbNewLine _
                                            & "	 UNSO_L.MOTO_DATA_KB = '20'				            AND								 " & vbNewLine _
                                            & "	 OUTL.SYS_DEL_FLG='0'																 " & vbNewLine _
                                            & "	--出荷EDIL																			 " & vbNewLine _
                                            & "	LEFT JOIN																			 " & vbNewLine _
                                            & "	 (SELECT																			 " & vbNewLine _
                                            & "	 	NRS_BR_CD																		 " & vbNewLine _
                                            & "	 	,OUTKA_CTL_NO												   AS OUTKA_CTL_NO	 " & vbNewLine _
                                            & "	 	,CUST_CD_L													   AS CUST_CD_L		 " & vbNewLine _
                                            & "	 	,DEST_CD													   AS DEST_CD		 " & vbNewLine _
                                            & "	 	,DEST_NM													   AS DEST_NM		 " & vbNewLine _
                                            & "	 	,DEST_AD_1													   AS DEST_AD_1	     " & vbNewLine _
                                            & "	 	,DEST_JIS_CD												   AS DEST_JIS_CD	 " & vbNewLine _
                                            & "	 	,SYS_DEL_FLG												   AS SYS_DEL_FLG	 " & vbNewLine _
                                            & "	 	FROM																			 " & vbNewLine _
                                            & "	 	$LM_TRN$..H_OUTKAEDI_L															 " & vbNewLine _
                                            & "	  WHERE																				 " & vbNewLine _
                                            & "	 	NRS_BR_CD = @NRS_BR_CD							   								 " & vbNewLine _
                                            & "	 	GROUP BY																		 " & vbNewLine _
                                            & "	 	NRS_BR_CD																		 " & vbNewLine _
                                            & "	 	,OUTKA_CTL_NO																	 " & vbNewLine _
                                            & "	 	,CUST_CD_L																	     " & vbNewLine _
                                            & "	 	,DEST_CD													   		             " & vbNewLine _
                                            & "	 	,DEST_NM													   		             " & vbNewLine _
                                            & "	 	,DEST_AD_1													   	                 " & vbNewLine _
                                            & "	 	,DEST_JIS_CD																	 " & vbNewLine _
                                            & "	 	,SYS_DEL_FLG																	 " & vbNewLine _
                                            & "  ) EDIL																			ON	 " & vbNewLine _
                                            & "	    EDIL.NRS_BR_CD = OUTL.NRS_BR_CD					AND								 " & vbNewLine _
                                            & "	 	EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L				AND								 " & vbNewLine _
                                            & "	 	EDIL.CUST_CD_L = OUTL.CUST_CD_L				    AND								 " & vbNewLine _
                                            & "	 	EDIL.SYS_DEL_FLG = '0'															 " & vbNewLine _
                                            & "	LEFT JOIN $LM_MST$..M_VCLE VCLE         										ON	 " & vbNewLine _
                                            & "	 	UNSO_LL.NRS_BR_CD             = VCLE.NRS_BR_CD	AND								 " & vbNewLine _
                                            & "	 	UNSO_LL.CAR_KEY               = VCLE.CAR_KEY	AND								 " & vbNewLine _
                                            & "	 	VCLE.SYS_DEL_FLG              = '0'											 	 " & vbNewLine _
                                            & "  --商品マスタ                                         								 " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_GOODS                          								 " & vbNewLine _
                                            & "         ON M_GOODS.NRS_BR_CD = UNSO_M.NRS_BR_CD       								 " & vbNewLine _
                                            & "        AND M_GOODS.GOODS_CD_NRS = UNSO_M.GOODS_CD_NRS 								 " & vbNewLine _
                                            & "  --運送Lでの荷主帳票パターン取得                      								 " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                  								 " & vbNewLine _
                                            & "         ON MCR1.NRS_BR_CD = UNSO_L.NRS_BR_CD          								 " & vbNewLine _
                                            & "	     AND MCR1.CUST_CD_L = UNSO_L.CUST_CD_L            								 " & vbNewLine _
                                            & "	     AND MCR1.CUST_CD_M = UNSO_L.CUST_CD_M            								 " & vbNewLine _
                                            & "	     AND MCR1.CUST_CD_S = '00'                        								 " & vbNewLine _
                                            & "	     AND MCR1.PTN_ID   = 'AZ'                        								 " & vbNewLine _
                                            & "  --帳票パターン取得                                   								 " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_RPT MR1                        								 " & vbNewLine _
                                            & "	      ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD               								 " & vbNewLine _
                                            & "	     AND MR1.PTN_ID    = MCR1.PTN_ID                  								 " & vbNewLine _
                                            & "	     AND MR1.PTN_CD    = MCR1.PTN_CD                  								 " & vbNewLine _
                                            & "        AND MR1.SYS_DEL_FLG = '0'                      								 " & vbNewLine _
                                            & " --商品Mの荷主での荷主帳票パターン取得              								     " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                  								 " & vbNewLine _
                                            & "	      ON MCR2.NRS_BR_CD = M_GOODS.NRS_BR_CD          								 " & vbNewLine _
                                            & "	     AND MCR2.CUST_CD_L = M_GOODS.CUST_CD_L          								 " & vbNewLine _
                                            & "	     AND MCR2.CUST_CD_M = M_GOODS.CUST_CD_M         		 						 " & vbNewLine _
                                            & "	     AND MCR2.CUST_CD_S = M_GOODS.CUST_CD_S          								 " & vbNewLine _
                                            & "	     AND MCR2.PTN_ID  = 'AZ'                         								 " & vbNewLine _
                                            & " --帳票パターン取得                                   								 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_RPT MR2                        								 " & vbNewLine _
                                            & "	      ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD              								 " & vbNewLine _
                                            & "	     AND MR2.PTN_ID    = MCR2.PTN_ID                 								 " & vbNewLine _
                                            & "	     AND MR2.PTN_CD    = MCR2.PTN_CD                 								 " & vbNewLine _
                                            & "        AND MR2.SYS_DEL_FLG = '0'                     								 " & vbNewLine _
                                            & " --存在しない場合の帳票パターン取得                   								 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_RPT MR3                        								 " & vbNewLine _
                                            & "	      ON MR3.NRS_BR_CD     = UNSO_L.NRS_BR_CD        								 " & vbNewLine _
                                            & "	     AND MR3.PTN_ID        = 'AZ'                    								 " & vbNewLine _
                                            & "	     AND MR3.STANDARD_FLAG = '01'                    								 " & vbNewLine _
                                            & "      AND MR3.SYS_DEL_FLG = '0'                        								 " & vbNewLine


#End Region
    '▼▼▼2012/11/21コメントアウト開始
#Region "GROUP BY句"
    '''' <summary>
    '''' GROUP BY
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_GROUP_BY As String = " GROUP BY                  " & vbNewLine _
    '                                     & "       MR1.PTN_CD          " & vbNewLine _
    '                                     & "     , MR2.PTN_CD          " & vbNewLine _
    '                                     & "     , MR1.RPT_ID          " & vbNewLine _
    '                                     & "     , MR2.RPT_ID          " & vbNewLine _
    '                                     & "     , MR3.RPT_ID          " & vbNewLine _
    '                                     & "     , UNSO_LL.TRIP_NO     " & vbNewLine _
    '                                     & "     , M_VCLE.CAR_NO       " & vbNewLine _
    '                                     & "     , UNSO_LL.TRIP_DATE   " & vbNewLine _
    '                                     & "     , UNSO_L.BIN_KB       " & vbNewLine _
    '                                     & "     , M_CUST.CUST_NM_L    " & vbNewLine _
    '                                     & "     , UNSO_L.REMARK       " & vbNewLine _
    '                                     & "     , M_DRIVER.DRIVER_NM  " & vbNewLine _
    '                                     & "     , Z_KBN.KBN_NM1       " & vbNewLine _
    '                                     & "     , M_NRS_BR.NRS_BR_NM  " & vbNewLine _
    '                                     & "     , UNSO_L.MOTO_DATA_KB " & vbNewLine _
    '                                     & "     , M_DEST_N.DEST_NM    " & vbNewLine _
    '                                     & "     , M_DEST_S.DEST_NM    " & vbNewLine



#End Region
    '▲▲▲2012/11/21コメントアウト終了
#Region "ORDER BY句"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "	ORDER BY																			 " & vbNewLine _
                                            & "	 UNSO_LL.TRIP_DATE   ASC,															 " & vbNewLine _
                                            & "	 VCLE.CAR_NO         ASC,															 " & vbNewLine _
                                            & "	 UNSO_LL.TRIP_NO     ASC,															 " & vbNewLine _
                                            & "	 UNSO_L.DEST_CD      ASC,															 " & vbNewLine _
                                            & "	 UNSO_L.UNSO_NO_L    ASC															 " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMF534IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF534DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF534DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF534DAC", "SelectMPrt", cmd)

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
    ''' 運行情報データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運行データ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF534IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMF534DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用SELECT句)
        Me._StrSql.Append(LMF534DAC.SQL_FROM)             'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)

        '2012/11/21検索条件から除外
        ' Me._StrSql.Append(LMF534DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)

        Me._StrSql.Append(LMF534DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER By句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF534DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("TRIP_DATE", "TRIP_DATE")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("CAR_NO", "CAR_NO")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("CUST_REF_NO", "CUST_REF_NO")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("SEIQ_TARIF_CD", "SEIQ_TARIF_CD")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UC_UNSO_NO_M", "UC_UNSO_NO_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF534OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
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
                Me._StrSql.Append(" UNSO_LL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO_LL.TRIP_NO = @TRIP_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", whereStr, DBDataType.CHAR))
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
