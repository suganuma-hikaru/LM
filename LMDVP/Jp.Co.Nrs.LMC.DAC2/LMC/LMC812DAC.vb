' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC812    : 出荷指示書印刷
'  作  成  者       :  [Tsunehira]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC812DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC812DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                         & "	OUTL.NRS_BR_CD                                           AS NRS_BR_CD " & vbNewLine _
                                         & ",'05'                                                     AS PTN_ID    " & vbNewLine _
                                         & " ,CASE      WHEN MRC.COMB_PTN_CD IS NOT NULL THEN MRC.COMB_PTN_CD               " & vbNewLine _
                                         & "           WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                         & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                         & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                         & ",CASE  WHEN MRC.COMB_RPT_ID IS NOT NULL THEN MRC.COMB_RPT_ID                      " & vbNewLine _
                                         & "      WHEN MR2.RPT_ID IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                         & "  		  WHEN MR1.RPT_ID IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                         & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 棟番号取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TouNo As String = "SELECT DISTINCT                                                         " & vbNewLine _
                                             & " OUTL.NRS_BR_CD  AS NRS_BR_CD                                            " & vbNewLine _
                                             & ",OUTS.TOU_NO     AS TOU_NO                                               " & vbNewLine _
                                             & "FROM                                                                    " & vbNewLine _
                                             & "$LM_TRN$..C_OUTKA_L OUTL                                                  " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                        " & vbNewLine _
                                             & "ON  OUTS.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                             & "AND OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                     " & vbNewLine _
                                             & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                     " & vbNewLine _
                                             & "WHERE                                                                   " & vbNewLine _
                                             & "OUTL.SYS_DEL_FLG = '0'                                                  " & vbNewLine

    '2次対応  荷姿並び替え 2012.01.17 START
    ''' <summary>
    ''' 設定値(荷主明細マスタ)取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MCUST_DETAILS As String = "SELECT                           " & vbNewLine _
                                             & " SET_NAIYO    AS SET_NAIYO               " & vbNewLine _
                                             & "FROM                                     " & vbNewLine _
                                             & "$LM_MST$..M_CUST_DETAILS MCD             " & vbNewLine _
                                             & "RIGHT JOIN                               " & vbNewLine _
                                             & "(SELECT                                  " & vbNewLine _
                                             & " CUST_CD_L                               " & vbNewLine _
                                             & " FROM $LM_TRN$..C_OUTKA_L                " & vbNewLine _
                                             & " WHERE                                   " & vbNewLine _
                                             & " C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD        " & vbNewLine _
                                             & " AND C_OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L  " & vbNewLine _
                                             & " ) CL                                    " & vbNewLine _
                                             & "ON MCD.CUST_CD = CL.CUST_CD_L            " & vbNewLine _
                                             & "WHERE                                    " & vbNewLine _
                                             & "MCD.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                             & "AND MCD.SUB_KB = '10'                    " & vbNewLine

    '2次対応  荷姿並び替え 2012.01.17 END

    '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
    ''' <summary>
    ''' 設定値(荷主明細マスタ)取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MCUST_DETAILS_FREE As String = "SELECT                        " & vbNewLine _
                                                & " SET_NAIYO    AS SET_NAIYO              " & vbNewLine _
                                                & "FROM                                    " & vbNewLine _
                                                & "$LM_MST$..M_CUST_DETAILS MCD            " & vbNewLine _
                                                & "RIGHT JOIN                              " & vbNewLine _
                                                & "(SELECT                                 " & vbNewLine _
                                                & " CUST_CD_L                              " & vbNewLine _
                                                & " FROM $LM_TRN$..C_OUTKA_L               " & vbNewLine _
                                                & " WHERE                                  " & vbNewLine _
                                                & " C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
                                                & " AND C_OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L " & vbNewLine _
                                                & " ) CL                                   " & vbNewLine _
                                                & "ON MCD.CUST_CD = CL.CUST_CD_L           " & vbNewLine _
                                                & "WHERE                                   " & vbNewLine _
                                                & "MCD.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                                & "AND MCD.SUB_KB = '25'                   " & vbNewLine
    '(2012.04.10) Notes№962 FREE_C03使用有無対応 --  END  --


    '2次対応  荷姿並び替え 2012.01.17 END

    '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- START --
    ''' <summary>
    ''' 設定値(荷主明細マスタ)取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MCUST_DETAILS_CHKLIST As String = "SELECT                     " & vbNewLine _
                                                & " ISNULL(SET_NAIYO,1)    AS SET_NAIYO    " & vbNewLine _
                                                & "FROM                                    " & vbNewLine _
                                                & "$LM_MST$..M_CUST_DETAILS MCD            " & vbNewLine _
                                                & "RIGHT JOIN                              " & vbNewLine _
                                                & "(SELECT                                 " & vbNewLine _
                                                & " CUST_CD_L                              " & vbNewLine _
                                                & " FROM $LM_TRN$..C_OUTKA_L               " & vbNewLine _
                                                & " WHERE                                  " & vbNewLine _
                                                & " C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
                                                & " AND C_OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L " & vbNewLine _
                                                & " ) CL                                   " & vbNewLine _
                                                & "ON MCD.CUST_CD = CL.CUST_CD_L           " & vbNewLine _
                                                & "AND MCD.NRS_BR_CD = @NRS_BR_CD          " & vbNewLine _
                                                & "AND MCD.SUB_KB = '31'                   " & vbNewLine
    '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- END --


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                                           " & vbNewLine _
                                               & " MAIN.RPT_ID		               AS RPT_ID                                                           " & vbNewLine _
                                               & ",MAIN.NRS_BR_CD		               AS NRS_BR_CD                                                     " & vbNewLine _
                                               & ",MAIN.PRINT_SORT	                AS PRINT_SORT                                                   " & vbNewLine _
                                               & ",MAIN.TOU_BETU_FLG	                AS TOU_BETU_FLG                                               " & vbNewLine _
                                               & ",MAIN.OUTKA_NO_L	                AS OUTKA_NO_L                                                   " & vbNewLine _
                                               & ",MAIN.DEST_CD		                 AS DEST_CD                                                       " & vbNewLine _
                                               & ",MAIN.DEST_NM		                 AS DEST_NM                                                       " & vbNewLine _
                                               & ",MAIN.DEST_AD_1		                 AS DEST_AD_1                                                   " & vbNewLine _
                                               & ",MAIN.DEST_AD_2		                 AS DEST_AD_2                                                   " & vbNewLine _
                                               & ",MAIN.DEST_AD_3		                  AS DEST_AD_3                                                  " & vbNewLine _
                                               & ",MAIN.DEST_TEL		                 AS DEST_TEL                                                     " & vbNewLine _
                                               & ",MAIN.CUST_CD_L		                 AS CUST_CD_L                                                   " & vbNewLine _
                                               & ",MAIN.CUST_NM_L		                 AS CUST_NM_L                                                   " & vbNewLine _
                                               & ",MAIN.CUST_NM_M		                  AS CUST_NM_M                                                  " & vbNewLine _
                                               & ",MAIN.CUST_NM_S		                  AS CUST_NM_S                                                  " & vbNewLine _
                                               & ",MAIN.CUST_NM_S_H		                  AS CUST_NM_S_H                                                  " & vbNewLine _
                                               & ",MAIN.OUTKA_PKG_NB	                  AS OUTKA_PKG_NB                                             " & vbNewLine _
                                               & ",MAIN.CUST_ORD_NO                   	AS CUST_ORD_NO                                              " & vbNewLine _
                                               & ",MAIN.BUYER_ORD_NO	                  AS BUYER_ORD_NO                                             " & vbNewLine _
                                               & ",MAIN.OUTKA_PLAN_DATE	               AS OUTKA_PLAN_DATE                                          " & vbNewLine _
                                               & ",MAIN.ARR_PLAN_DATE	                  AS ARR_PLAN_DATE                                           " & vbNewLine _
                                               & ",MAIN.ARR_PLAN_TIME	                  AS ARR_PLAN_TIME                                           " & vbNewLine _
                                               & ",MAIN.UNSOCO_NM		                  AS UNSOCO_NM                                                  " & vbNewLine _
                                               & ",MAIN.PC_KB		                      AS PC_KB                                                      " & vbNewLine _
                                               & ",MAIN.KYORI		                       AS KYORI                                                     " & vbNewLine _
                                               & ",MAIN.UNSO_WT                      		AS UNSO_WT                                                  " & vbNewLine _
                                               & ",MAIN.URIG_NM	                       	AS URIG_NM                                                 " & vbNewLine _
                                               & ",MAIN.FREE_C03	                     	AS FREE_C03                                                 " & vbNewLine _
                                               & ",MAIN.REMARK_L	                       	AS REMARK_L                                               " & vbNewLine _
                                               & ",MAIN.REMARK_UNSO                   	AS REMARK_UNSO                                              " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_1                	AS SAGYO_REC_NO_1                                           " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_1	                 AS SAGYO_CD_1                                                  " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_1	                  AS SAGYO_NM_1                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_2                	AS SAGYO_REC_NO_2                                           " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_2	                  AS SAGYO_CD_2                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_2	                  AS SAGYO_NM_2                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_3	              AS SAGYO_REC_NO_3                                             " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_3	                  AS SAGYO_CD_3                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_3	                  AS SAGYO_NM_3                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_4	                AS SAGYO_REC_NO_4                                           " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_4	                    AS SAGYO_CD_4                                               " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_4                      	AS SAGYO_NM_4                                             " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_5	               AS SAGYO_REC_NO_5                                            " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_5	                   AS SAGYO_CD_5                                                " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_5	                   AS SAGYO_NM_5                                                " & vbNewLine _
                                               & ",MAIN.CRT_USER	                      	AS CRT_USER                                                " & vbNewLine _
                                               & ",MAIN.OUTKA_NO_M	                    AS OUTKA_NO_M                                               " & vbNewLine _
                                               & ",MAIN.GOODS_NM	                    	AS GOODS_NM                                                  " & vbNewLine _
                                               & ",MAIN.FREE_C08	                    	AS FREE_C08                                                  " & vbNewLine _
                                               & ",MAIN.IRIME		                       AS IRIME                                                     " & vbNewLine _
                                               & ",MAIN.IRIME_UT		                   AS IRIME_UT                                                   " & vbNewLine _
                                               & ",SUM(MAIN.ALCTD_NB) / MAIN.PKG_NB      AS KONSU                                                  " & vbNewLine _
                                               & ",SUM(MAIN.ALCTD_NB) % MAIN.PKG_NB      AS HASU                                                   " & vbNewLine _
                                               & ",SUM(MAIN.ALCTD_NB)                    AS ALCTD_NB                                               " & vbNewLine _
                                               & ",MAIN.NB_UT			                   AS NB_UT                                                        " & vbNewLine _
                                               & ",MIN(MAIN.ALCTD_CAN_NB)                AS ALCTD_CAN_NB                                           " & vbNewLine _
                                               & ",MAIN.FREE_C07			               AS FREE_C07                                                      " & vbNewLine _
                                               & ",SUM(MAIN.ALCTD_QT)                    AS ALCTD_QT                                               " & vbNewLine _
                                               & ",MIN(MAIN.ALCTD_CAN_NB) / MAIN.PKG_NB  AS ZAN_KONSU                                              " & vbNewLine _
                                               & ",MIN(MAIN.ALCTD_CAN_NB) % MAIN.PKG_NB  AS ZAN_HASU                                               " & vbNewLine _
                                               & ",MAIN.SERIAL_NO		         AS SERIAL_NO                                                           " & vbNewLine _
                                               & ",MAIN.PKG_NB	         	 AS PKG_NB                                                                " & vbNewLine _
                                               & ",MAIN.PKG_UT        		 AS PKG_UT                                                                 " & vbNewLine _
                                               & ",MAIN.ALCTD_KB		        AS ALCTD_KB                                                              " & vbNewLine _
                                               & ",MIN(MAIN.ALCTD_CAN_QT)     AS ALCTD_CAN_QT                                                      " & vbNewLine _
                                               & ",MAIN.REMARK_OUT			AS REMARK_OUT                                                                 " & vbNewLine _
                                               & ",MAIN.LOT_NO		        AS LOT_NO                                                                  " & vbNewLine _
                                               & ",MAIN.LT_DATE		        AS LT_DATE                                                                " & vbNewLine _
                                               & ",MAIN.INKA_DATE 		        AS INKA_DATE                                                            " & vbNewLine _
                                               & ",MAIN.REMARK_S		        AS REMARK_S                                                              " & vbNewLine _
                                               & ",MAIN.GOODS_COND_NM_1     	AS GOODS_COND_NM_1                                                    " & vbNewLine _
                                               & ",MAIN.GOODS_COND_NM_2	    AS GOODS_COND_NM_2                                                     " & vbNewLine _
                                               & ",MAIN.GOODS_CD_CUST      	AS GOODS_CD_CUST                                                       " & vbNewLine _
                                               & ",MAIN.BETU_WT		        AS BETU_WT                                                                " & vbNewLine _
                                               & ",MAIN.CUST_ORD_NO_DTL	    AS CUST_ORD_NO_DTL                                                     " & vbNewLine _
                                               & ",MAIN.TOU_NO		        AS TOU_NO                                                                  " & vbNewLine _
                                               & ",MAIN.SITU_NO		        AS SITU_NO                                                                " & vbNewLine _
                                               & ",RTRIM(MAIN.ZONE_CD)       AS ZONE_CD                                                                " & vbNewLine _
                                               & ",MAIN.LOCA		            AS LOCA                                                                  " & vbNewLine _
                                               & ",MAIN.REMARK_M		      AS REMARK_M                                                                " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_1	AS SAGYO_MEI_REC_NO_1                                                   " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_1		AS SAGYO_MEI_CD_1                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_1		AS SAGYO_MEI_NM_1                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_2	AS SAGYO_MEI_REC_NO_2                                                   " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_2		AS SAGYO_MEI_CD_2                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_2		AS SAGYO_MEI_NM_2                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_3	AS SAGYO_MEI_REC_NO_3                                                   " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_3		AS SAGYO_MEI_CD_3                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_3		AS SAGYO_MEI_NM_3                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_4	AS SAGYO_MEI_REC_NO_4                                                   " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_4		AS SAGYO_MEI_CD_4                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_4		AS SAGYO_MEI_NM_4                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_5	AS SAGYO_MEI_REC_NO_5                                                  " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_5		AS SAGYO_MEI_CD_5                                                      " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_5		AS SAGYO_MEI_NM_5                                                      " & vbNewLine _
                                               & ",MAIN.SAIHAKKO_FLG		AS SAIHAKKO_FLG                                                        " & vbNewLine _
                                               & ",MAIN.OYA_CUST_GOODS_CD   AS OYA_CUST_GOODS_CD                                                   " & vbNewLine _
                                               & ",MAIN.OYA_GOODS_NM		AS OYA_GOODS_NM                                                        " & vbNewLine _
                                               & ",MAIN.OYA_KATA		    AS OYA_KATA                                                            " & vbNewLine _
                                               & ",MAIN.OYA_OUTKA_TTL_NB    AS OYA_OUTKA_TTL_NB                                                    " & vbNewLine _
                                               & ",MAIN.SET_NAIYO AS SET_NAIYO                                                                     " & vbNewLine _
                                               & " --(2012.03.03) 出庫日、運送会社支店名 追加 LMC526対応 -- STRAT --                               " & vbNewLine _
                                               & ",MAIN.OUTKO_DATE          AS OUTKO_DATE                                                          " & vbNewLine _
                                               & ",MAIN.UNSOCO_BR_NM		AS UNSOCO_BR_NM                                                        " & vbNewLine _
                                               & " --(2012.03.03) 出庫日、運送会社支店名 追加 LMC526対応 --  END  --                               " & vbNewLine _
                                               & ",MAIN.GOODS_COND_NM_3	    AS GOODS_COND_NM_3                                                     " & vbNewLine _
                                               & ",MAIN.RPT_FLG             AS RPT_FLG      --20120313                                             " & vbNewLine _
                                               & ",MAIN.OUTKA_NO_S          AS OUTKA_NO_S   --20120511 LMC528対応                                  " & vbNewLine _
                                               & ",MAIN.WH_CD               AS WH_CD        --20120528                                             " & vbNewLine _
                                               & ",MAIN.CUST_NAIYO_1        AS CUST_NAIYO_1 --20120528                                             " & vbNewLine _
                                               & ",MAIN.CUST_NAIYO_2        AS CUST_NAIYO_2 --20120528                                             " & vbNewLine _
                                               & ",MAIN.CUST_NAIYO_3        AS CUST_NAIYO_3 --20120528                                             " & vbNewLine _
                                               & " --(2012.06.09) 要望番号1123対応 -- STRAT --                                                     " & vbNewLine _
                                               & ",MAIN.DEST_REMARK         AS DEST_REMARK                                                         " & vbNewLine _
                                               & ",MAIN.DEST_SALES_CD		AS DEST_SALES_CD                                                       " & vbNewLine _
                                               & ",MAIN.DEST_SALES_NM_L		AS DEST_SALES_NM_L                                                     " & vbNewLine _
                                               & ",MAIN.DEST_SALES_NM_M		AS DEST_SALES_NM_M                                                     " & vbNewLine _
                                               & " --(2012.06.09) 要望番号1123対応 --  END  --                                                     " & vbNewLine _
                                               & " --(2012.11.13) LMC537対応 -- STRAT --                                                           " & vbNewLine _
                                               & ",''        	            AS ALCTD_NB_HEADKEI                                                    " & vbNewLine _
                                               & ",''        	            AS ALCTD_QT_HEADKEI                                                    " & vbNewLine _
                                               & ",''        	            AS HINMEI                                                              " & vbNewLine _
                                               & ",''        	            AS NISUGATA                                                            " & vbNewLine _
                                               & " --(2012.11.13) LMC537対応 --  END  --                                                           " & vbNewLine _
                                               & " --(2015.10.27) シンガポール対応 --  START  --                                                   " & vbNewLine _
                                               & " ,MAIN.GOODS_NM_2 AS GOODS_NM_2                                                                  " & vbNewLine _
                                               & " ,MAIN.SEARCH_KEY_1 AS SEARCH_KEY_1                                                                        " & vbNewLine _
                                               & " --(2015.10.25) シンガポール対応 --  END  --                                                     " & vbNewLine _
                                               & " --(2016.01.08) シンガポール対応 --  START  --                                                   " & vbNewLine _
                                               & " ,MAIN.INNER_PKG_NB      AS INNER_PKG_NB                                                     " & vbNewLine _
                                               & " ,SUM(MAIN.ALCTD_NB) OVER(PARTITION BY MAIN.OUTKA_NO_L,MAIN.OUTKA_NO_M) AS S_TOTAL               " & vbNewLine _
                                               & " --(2016.01.08) シンガポール対応 --  END  --                                                     " & vbNewLine _
                                               & "  -- 輸出情報追加 START                                                                          " & vbNewLine _
                                               & "  ,EXPORT.SHIP_NM                                     AS SHIP_NM                                 " & vbNewLine _
                                               & "  ,EXPORT.DESTINATION                                 AS DESTINATION                             " & vbNewLine _
                                               & "  ,EXPORT.BOOKING_NO                                  AS BOOKING_NO                              " & vbNewLine _
                                               & "  ,EXPORT.VOYAGE_NO                                   AS VOYAGE_NO                               " & vbNewLine _
                                               & "  ,EXPORT.SHIPPER_CD                                  AS SHIPPER_CD                              " & vbNewLine _
                                               & "  ,EXPORT_DEST.DEST_NM                                AS SHIPPER_NM                              " & vbNewLine _
                                               & "  ,EXPORT.CONT_LOADING_DATE                           AS CONT_LOADING_DATE                       " & vbNewLine _
                                               & "  ,EXPORT.STORAGE_TEST_DATE                           AS STORAGE_TEST_DATE                       " & vbNewLine _
                                               & "  ,EXPORT.STORAGE_TEST_TIME                           AS STORAGE_TEST_TIME                       " & vbNewLine _
                                               & "  ,EXPORT.DEPARTURE_DATE                              AS DEPARTURE_DATE                          " & vbNewLine _
                                               & "  ,EXPORT.CONTAINER_NO                                AS CONTAINER_NO                            " & vbNewLine _
                                               & "  ,EXPORT.CONTAINER_NM                                AS CONTAINER_NM                            " & vbNewLine _
                                               & "  ,EXPORT.CONTAINER_SIZE                              AS CONTAINER_SIZE                          " & vbNewLine _
                                               & "  ,EXPORT_KBN.KBN_NM1                                 AS CONTAINER_SIZE_NM                       " & vbNewLine _
                                               & "  -- 輸出情報追加 END                                                                            " & vbNewLine _
                                               & "  ,MAIN.UN                                            AS UN                                      " & vbNewLine _
                                               & "  ,MAIN.OUTKA_ATT                                     AS OUTKA_ATT                               " & vbNewLine _
                                               & "FROM                                                                                             " & vbNewLine _
                                               & "(SELECT                                                                                          " & vbNewLine _
                                               & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                " & vbNewLine _
                                               & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                " & vbNewLine _
                                               & "      ELSE MR3.RPT_ID                                                                            " & vbNewLine _
                                               & " END              AS RPT_ID                                                                      " & vbNewLine _
                                               & ",OUTL.NRS_BR_CD   AS NRS_BR_CD                                                                   " & vbNewLine _
                                               & ",OUTM.PRINT_SORT  AS PRINT_SORT                                                                  " & vbNewLine _
                                               & ",'0'  AS TOU_BETU_FLG                                                                            " & vbNewLine _
                                               & ",OUTL.OUTKA_NO_L  AS OUTKA_NO_L                                                                  " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                                           " & vbNewLine _
                                               & "      ELSE OUTL.DEST_CD                                                                                              " & vbNewLine _
                                               & " END              AS DEST_CD                                                                                         " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                                               " & vbNewLine _
                                               & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                                     " & vbNewLine _
                                               & "      ELSE MDOUT.DEST_NM                                                                                                       " & vbNewLine _
                                               & " END              AS DEST_NM                                                                                                   " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                                             " & vbNewLine _
                                               & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                                   " & vbNewLine _
                                               & "      ELSE MDOUT.AD_1                                                                                                          " & vbNewLine _
                                               & " END              AS DEST_AD_1                                                                                                 " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                                             " & vbNewLine _
                                               & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                                   " & vbNewLine _
                                               & "      ELSE MDOUT.AD_2                                                                                                          " & vbNewLine _
                                               & " END              AS DEST_AD_2                                                                                                 " & vbNewLine _
                                               & ",OUTL.DEST_AD_3   AS DEST_AD_3                                                                                       " & vbNewLine _
                                               & ",OUTL.DEST_TEL    AS DEST_TEL                                                                                        " & vbNewLine _
                                               & ",OUTL.CUST_CD_L     AS CUST_CD_L                                                                                     " & vbNewLine _
                                               & ",MC.CUST_NM_L                   AS CUST_NM_L                                                              " & vbNewLine _
                                               & ",MC.CUST_NM_M                   AS CUST_NM_M                                                              " & vbNewLine _
                                               & ",MC.CUST_NM_S                   AS CUST_NM_S                                                              " & vbNewLine _
                                               & ",MC2.CUST_NM_S                  AS CUST_NM_S_H                                                              " & vbNewLine _
                                               & ",OUTL.OUTKA_PKG_NB   AS OUTKA_PKG_NB                                                                                 " & vbNewLine _
                                               & ",OUTL.CUST_ORD_NO    AS CUST_ORD_NO                                                                                  " & vbNewLine _
                                               & ",OUTL.BUYER_ORD_NO   AS BUYER_ORD_NO                                                                                 " & vbNewLine _
                                               & " --(2012.03.03) 出庫日追加 LMC526対応 -- START --                                                                    " & vbNewLine _
                                               & ",OUTL.OUTKO_DATE        AS OUTKO_DATE                                                                                " & vbNewLine _
                                               & " --(2012.03.03) 出庫日追加 LMC526対応 --  END  --                                                                    " & vbNewLine _
                                               & ",OUTL.OUTKA_PLAN_DATE   AS OUTKA_PLAN_DATE                                                                           " & vbNewLine _
                                               & ",OUTL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                                                                             " & vbNewLine _
                                               & ",KBN1.KBN_NM1               AS ARR_PLAN_TIME                                                                         " & vbNewLine _
                                               & ",MUCO.UNSOCO_NM             AS UNSOCO_NM                                                                             " & vbNewLine _
                                               & " --(2012.03.03) 運送会社支店名 追加 LMC526対応 -- STRAT --                                                           " & vbNewLine _
                                               & ",MUCO.UNSOCO_BR_NM          AS UNSOCO_BR_NM                                                                          " & vbNewLine _
                                               & " --(2012.03.03) 運送会社支店名 追加 LMC526対応 --  END  --                                                           " & vbNewLine _
                                               & ",OUTL.PC_KB                 AS PC_KB                                                                                 " & vbNewLine _
                                               & ",CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                      " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD <= MSO.JIS_CD) THEN MKY3.KYORI     " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD <= EDIL.DEST_JIS_CD) THEN MKY4.KYORI     " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                          " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS <= MSO.JIS_CD) THEN MKY1.KYORI                " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD <= MDOUT.JIS) THEN MKY2.KYORI                " & vbNewLine _
                                               & "      ELSE 0                                                                                                         " & vbNewLine _
                                               & " END             AS KYORI                                                                                            " & vbNewLine _
                                               & ",UL.UNSO_WT      AS UNSO_WT                                                                                          " & vbNewLine _
                                               & ",MDOUTU.DEST_NM  AS URIG_NM                                                                                          " & vbNewLine _
                                               & ",CASE WHEN EDIL.OUTKA_CTL_NO IS NOT NULL THEN EDIL.FREE_C03                                                          " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END             AS FREE_C03                                                                                         " & vbNewLine _
                                               & "--,OUTL.REMARK     AS REMARK_L                                                                                         " & vbNewLine _
                                               & ",CASE WHEN MGD2.SET_NAIYO = 1 THEN                                                                                   " & vbNewLine _
                                               & "    CASE WHEN MDD.SET_NAIYO = 1 THEN                                                                                 " & vbNewLine _
                                               & "        OUTL.REMARK + ' ' + MDD.REMARK                                                                               " & vbNewLine _
                                               & "    ELSE OUTL.REMARK                                                                                                 " & vbNewLine _
                                               & "    END                                                                                                              " & vbNewLine _
                                               & " ELSE OUTL.REMARK                                                                                                    " & vbNewLine _
                                               & " END AS REMARK_L                                                                                                     " & vbNewLine _
                                               & ",UL.REMARK       AS REMARK_UNSO                                                                                      " & vbNewLine _
                                               & ",@SAIHAKKO_FLG     AS SAIHAKKO_FLG                                                                                   " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_REC_NO_1                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_CD_1                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_NM_1                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_REC_NO_2                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_CD_2                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_NM_2                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_REC_NO_3                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_CD_3                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_NM_3                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_REC_NO_4                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_CD_4                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_NM_4                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_REC_NO_5                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_CD_5                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_NM_5                                                                               " & vbNewLine _
                                               & ",MUSER.USER_NM      AS CRT_USER                                                                                      " & vbNewLine _
                                               & ",OUTM.OUTKA_NO_M   AS OUTKA_NO_M                                                                                     " & vbNewLine _
                                               & ",MG.GOODS_NM_1     AS GOODS_NM                                                                                       " & vbNewLine _
                                               & " --(2015.10.27) シンガポール対応 --  START  --                                                                       " & vbNewLine _ 
                                               & ",MG.SEARCH_KEY_1 AS SEARCH_KEY_1                                                                                     " & vbNewLine _
                                               & ",MG.GOODS_NM_2 AS GOODS_NM_2                                                                                         " & vbNewLine _
                                               & "--(2015.10.27) シンガポール対応 --  END  --                                                                          " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C08                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS FREE_C08                                                                                       " & vbNewLine _
                                               & ",OUTS.IRIME        AS IRIME                                                                                          " & vbNewLine _
                                               & ",MG.STD_IRIME_UT   AS IRIME_UT                                                                                       " & vbNewLine _
                                               & ",OUTS.ALCTD_NB     AS ALCTD_NB                                                                                       " & vbNewLine _
                                               & ",MG.NB_UT          AS NB_UT                                                                                          " & vbNewLine _
                                               & ",OUTS.ALCTD_CAN_NB    AS ALCTD_CAN_NB                                                                                " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C07                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS FREE_C07                                                                                       " & vbNewLine _
                                               & ",OUTS.ALCTD_QT     AS ALCTD_QT                                                                                       " & vbNewLine _
                                               & ",OUTS.SERIAL_NO    AS SERIAL_NO                                                                                      " & vbNewLine _
                                               & ",MG.PKG_NB         AS PKG_NB                                                                                         " & vbNewLine _
                                               & ",MG.PKG_UT         AS PKG_UT                                                                                         " & vbNewLine _
                                               & ",OUTM.ALCTD_KB       AS ALCTD_KB                                                                                       " & vbNewLine _
                                               & ",OUTS.ALCTD_CAN_QT AS ALCTD_CAN_QT                                                                                   " & vbNewLine _
                                               & ",ZAI.REMARK_OUT    AS REMARK_OUT                                                                                     " & vbNewLine _
                                               & ",OUTS.LOT_NO       AS LOT_NO                                                                                         " & vbNewLine _
                                               & ",OUTS.OUTKA_NO_S   AS OUTKA_NO_S                  --20120511 LMC528対応                                              " & vbNewLine _
                                               & ",INS.LT_DATE       AS LT_DATE                                                                                        " & vbNewLine _
                                               & ",CASE WHEN INL.INKA_STATE_KB < '50' THEN INL.INKA_DATE                                                                         " & vbNewLine _
                                               & "      ELSE ZAI.INKO_DATE                                                                                                       " & vbNewLine _
                                               & " END                    AS INKA_DATE                                                                                           " & vbNewLine _
                                               & ",OUTS.REMARK       AS REMARK_S                                                                                       " & vbNewLine _
                                               & ",KBN3.KBN_NM1                 AS GOODS_COND_NM_1                                                                     " & vbNewLine _
                                               & ",KBN4.KBN_NM1                 AS GOODS_COND_NM_2                                                                     " & vbNewLine _
                                               & ",MG.GOODS_CD_CUST      AS GOODS_CD_CUST                                                                              " & vbNewLine _
                                               & ",OUTS.BETU_WT      AS BETU_WT                                                                                        " & vbNewLine _
                                               & ",OUTM.CUST_ORD_NO_DTL         AS CUST_ORD_NO_DTL                                                                     " & vbNewLine _
                                               & ",OUTS.TOU_NO       AS TOU_NO                                                                                         " & vbNewLine _
                                               & ",OUTS.SITU_NO      AS SITU_NO                                                                                        " & vbNewLine _
                                               & ",RTRIM(OUTS.ZONE_CD)      AS ZONE_CD                                                                                        " & vbNewLine _
                                               & ",OUTS.LOCA         AS LOCA                                                                                           " & vbNewLine _
                                               & ",OUTM.REMARK       AS REMARK_M                                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_REC_NO_1                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_CD_1                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_NM_1                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_REC_NO_2                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_CD_2                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_NM_2                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_REC_NO_3                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_CD_3                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_NM_3                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_REC_NO_4                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_CD_4                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_NM_4                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_REC_NO_5                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_CD_5                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_NM_5                                                                           " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C11                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS OYA_CUST_GOODS_CD                                                                              " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C12                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS OYA_GOODS_NM                                                                                   " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C13                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS OYA_KATA                                                                                       " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_N02                                                            " & vbNewLine _
                                               & "      ELSE 0                                                                                                         " & vbNewLine _
                                               & " END               AS OYA_OUTKA_TTL_NB                                                                               " & vbNewLine _
                                               & " ,MGD.SET_NAIYO     AS SET_NAIYO                                                                                     " & vbNewLine _
                                               & " ,'' AS RPT_FLG  --20120313                                                                                          " & vbNewLine _
                                               & " ,ISNULL(MCC.JOTAI_NM,'') AS GOODS_COND_NM_3  --20120313                                                             " & vbNewLine _
                                               & " ,OUTL.WH_CD AS WH_CD                  --20120528                                                                    " & vbNewLine _
                                               & " ,MCD.SET_NAIYO    AS CUST_NAIYO_1     --20120528                                                                    " & vbNewLine _
                                               & " ,MCD.SET_NAIYO_2  AS CUST_NAIYO_2     --20120528                                                                    " & vbNewLine _
                                               & " ,MCD.SET_NAIYO_3  AS CUST_NAIYO_3     --20120528                                                                    " & vbNewLine _
                                               & " --(2012.06.09) 要望番号1123対応 -- STRAT --                                                                         " & vbNewLine _
                                               & " ,MDOUT.REMARK            AS DEST_REMARK                                                                             " & vbNewLine _                                                            
                                               & " ,MDOUT.SALES_CD          AS DEST_SALES_CD                                                                           " & vbNewLine _             
                                               & " ,MC_SALES.CUST_NM_L      AS DEST_SALES_NM_L                                                                         " & vbNewLine _                                                                                    
                                               & " ,MC_SALES.CUST_NM_M      AS DEST_SALES_NM_M                                                                         " & vbNewLine _ 
                                               & " --(2012.06.09) 要望番号1123対応 --  END  --                                                                         " & vbNewLine _
                                               & " --(2016.01.08) シンガポール対応 --  START  --                                                                       " & vbNewLine _ 
                                               & " ,MG.INNER_PKG_NB       AS INNER_PKG_NB                                                                              " & vbNewLine _
                                               & " --(2016.01.08) シンガポール対応 --  END  --                                                                         " & vbNewLine _
                                               & "   ,MG.UN                 AS UN                                                                                      " & vbNewLine _
                                               & "   ,MG.OUTKA_ATT          AS OUTKA_ATT                                                                               " & vbNewLine                                               

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "--出荷L                                                                      " & vbNewLine _
                                     & "FROM                                                                         " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                     " & vbNewLine _
                                     & "--トランザクションテーブル                                                   " & vbNewLine _
                                     & "--出荷M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                           " & vbNewLine _
                                     & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                     & "AND OUTM.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                     & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                     & "LEFT OUTER JOIN                                                                                                                " & vbNewLine _
                                     & "       (SELECT                                                                                                                 " & vbNewLine _
                                     & "           NRS_BR_CD                                                                                                           " & vbNewLine _
                                     & "           ,OUTKA_NO_L                                                                                                         " & vbNewLine _
                                     & "           ,MIN(OUTKA_NO_M) AS  OUTKA_NO_M                                                                                     " & vbNewLine _
                                     & "       FROM $LM_TRN$..C_OUTKA_M WHERE SYS_DEL_FLG ='0'                                                                         " & vbNewLine _
                                     & "       GROUP BY NRS_BR_CD,OUTKA_NO_L) OUTM_MIN                                                                                 " & vbNewLine _
                                     & "       ON OUTM_MIN.NRS_BR_CD        = OUTL.NRS_BR_CD                                                                           " & vbNewLine _
                                     & "       AND OUTM_MIN.OUTKA_NO_L      = OUTL.OUTKA_NO_L                                                                          " & vbNewLine _
                                     & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM2                                                                                            " & vbNewLine _
                                     & "ON  OUTM2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                     & "AND OUTM2.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                         " & vbNewLine _
                                     & "AND OUTM2.OUTKA_NO_M = OUTM_MIN.OUTKA_NO_M                                                                                     " & vbNewLine _
                                     & "AND OUTM2.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                     & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                     & "--出荷S                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                             " & vbNewLine _
                                     & "ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                     & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                        " & vbNewLine _
                                     & "AND OUTS.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--(2012.09.11)要望番号1412対応  --- START ---                                " & vbNewLine _
                                     & "--出荷EDIL                                                                   " & vbNewLine _
                                     & "--LEFT JOIN                                                                  " & vbNewLine _
                                     & "--(                                                                          " & vbNewLine _
                                     & "--SELECT                                                                     " & vbNewLine _
                                     & "-- NRS_BR_CD                                                                 " & vbNewLine _
                                     & "--,OUTKA_CTL_NO                                                              " & vbNewLine _
                                     & "--,CUST_CD_L                                                                 " & vbNewLine _
                                     & "--,SHIP_NM_L                                                                 " & vbNewLine _
                                     & "--,MIN(DEST_CD)    AS DEST_CD                                                " & vbNewLine _
                                     & "--,MIN(DEST_NM)    AS DEST_NM                                                " & vbNewLine _
                                     & "--,MIN(DEST_AD_1)  AS DEST_AD_1                                              " & vbNewLine _
                                     & "--,MIN(DEST_AD_2)  AS DEST_AD_2                                              " & vbNewLine _
                                     & "--,DEST_JIS_CD                                                               " & vbNewLine _
                                     & "--,FREE_C03                                                                  " & vbNewLine _
                                     & "--,SYS_DEL_FLG                                                               " & vbNewLine _
                                     & "--FROM                                                                       " & vbNewLine _
                                     & "--$LM_TRN$..H_OUTKAEDI_L                                                     " & vbNewLine _
                                     & "--GROUP BY                                                                   " & vbNewLine _
                                     & "-- NRS_BR_CD                                                                 " & vbNewLine _
                                     & "--,OUTKA_CTL_NO                                                              " & vbNewLine _
                                     & "--,CUST_CD_L                                                                 " & vbNewLine _
                                     & "--,SHIP_NM_L                                                                 " & vbNewLine _
                                     & "--,DEST_JIS_CD                                                               " & vbNewLine _
                                     & "--,FREE_C03                                                                  " & vbNewLine _
                                     & "--,SYS_DEL_FLG                                                               " & vbNewLine _
                                     & "--) EDIL                                                                     " & vbNewLine _
                                     & "--ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                                     & "--AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                    " & vbNewLine _
                                     & "--AND EDIL.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                     & "--下記の内容に変更                                                           " & vbNewLine _
                                     & " LEFT JOIN (                                                                 " & vbNewLine _
                                     & "            SELECT                                                           " & vbNewLine _
                                     & "                   NRS_BR_CD                                                 " & vbNewLine _
                                     & "                 , EDI_CTL_NO                                                " & vbNewLine _
                                     & "                 , OUTKA_CTL_NO                                              " & vbNewLine _
                                     & "             FROM (                                                          " & vbNewLine _
                                     & "                    SELECT                                                            " & vbNewLine _
                                     & "                           EDIOUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "                         , EDIOUTL.EDI_CTL_NO                                         " & vbNewLine _
                                     & "                         , EDIOUTL.OUTKA_CTL_NO                                       " & vbNewLine _
                                     & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                 " & vbNewLine _
                                     & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD     " & vbNewLine _
                                     & "                                                              , EDIOUTL.OUTKA_CTL_NO  " & vbNewLine _
                                     & "                                                       ORDER BY EDIOUTL.NRS_BR_CD     " & vbNewLine _
                                     & "                                                              , EDIOUTL.EDI_CTL_NO    " & vbNewLine _
                                     & "                                                  )                                   " & vbNewLine _
                                     & "                           END AS IDX                                                 " & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                     " & vbNewLine _
                                     & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                         " & vbNewLine _
                                     & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                  " & vbNewLine _
                                     & "                      AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                 " & vbNewLine _
                                     & "                  ) EBASE                                                    " & vbNewLine _
                                     & "            WHERE EBASE.IDX = 1                                              " & vbNewLine _
                                     & "            ) TOPEDI                                                         " & vbNewLine _
                                     & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                              " & vbNewLine _
                                     & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                             " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                       " & vbNewLine _
                                     & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                " & vbNewLine _
                                     & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                               " & vbNewLine _
                                     & "--(2012.09.11)要望番号1412対応  ---  END  ---                                " & vbNewLine _
                                     & "----出荷EDIM                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                          " & vbNewLine _
                                     & "ON  EDIM.NRS_BR_CD = OUTM.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND EDIM.OUTKA_CTL_NO = OUTM.OUTKA_NO_L                                      " & vbNewLine _
                                     & "AND EDIM.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M                                  " & vbNewLine _
                                     & "AND EDIM.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--入荷L                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..B_INKA_L INL                                               " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND INL.INKA_NO_L = OUTS.INKA_NO_L                                           " & vbNewLine _
                                     & "AND INL.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--入荷S                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                               " & vbNewLine _
                                     & "ON  INS.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_L = OUTS.INKA_NO_L                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_M = OUTS.INKA_NO_M                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_S = OUTS.INKA_NO_S                                           " & vbNewLine _
                                     & "AND INS.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--運送L                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                " & vbNewLine _
                                     & "ON  UL.NRS_BR_CD = OUTL.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                     & "AND UL.MOTO_DATA_KB = '20'                                                   " & vbNewLine _
                                     & "AND UL.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                     & "--在庫レコード                                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                " & vbNewLine _
                                     & "ON  ZAI.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                         " & vbNewLine _
                                     & "AND ZAI.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--マスタテーブル                                                             " & vbNewLine _
                                     & "--商品M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                                 " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                      " & vbNewLine _
                                     & "--商品DetlM                                                                  " & vbNewLine _
                                     & "LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO         " & vbNewLine _
                                     & "   FROM $LM_MST$..M_GOODS_DETAILS  WHERE SUB_KB ='07' AND SYS_DEL_FLG ='0'   " & vbNewLine _
                                     & "   GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD                                    " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MGD.NRS_BR_CD                                             " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = MGD.GOODS_CD_NRS                                       " & vbNewLine _
                                     & "LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO         " & vbNewLine _
                                     & "   FROM LM_MST..M_GOODS_DETAILS  WHERE SUB_KB ='42' AND SYS_DEL_FLG ='0'     " & vbNewLine _
                                     & "   GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD2                                   " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MGD2.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = MGD2.GOODS_CD_NRS                                      " & vbNewLine _
                                     & "LEFT JOIN LM_MST..M_DEST_DETAILS MDD                                         " & vbNewLine _
                                     & "ON  MDD.NRS_BR_CD = EDIL.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MDD.CUST_CD_L = EDIL.CUST_CD_L                                           " & vbNewLine _
                                     & "AND MDD.DEST_CD = EDIL.DEST_CD                                               " & vbNewLine _
                                     & "AND MDD.SUB_KB = '04'                                                        " & vbNewLine _
                                     & "AND MDD.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--★★★追加開始--------------------------------                               " & vbNewLine _
                                     & "--商品M(MIN)                                                                 " & vbNewLine _
                                     & "LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                " & vbNewLine _
                                     & "ON M_GOODS_MIN.NRS_BR_CD      = OUTL.NRS_BR_CD                               " & vbNewLine _
                                     & "AND M_GOODS_MIN.GOODS_CD_NRS   = OUTM2.GOODS_CD_NRS                          " & vbNewLine _
                                     & "--★★★追加終了--------------------------------                               " & vbNewLine _
                                     & "--荷主M(商品M経由)                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST MC                                                " & vbNewLine _
                                     & "ON  MC.NRS_BR_CD = MG.NRS_BR_CD                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_L = MG.CUST_CD_L                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_M = MG.CUST_CD_M                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_S = MG.CUST_CD_S                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                            " & vbNewLine _
                                     & "AND MC.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                     & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                     & "--荷主M(商品M経由) 最小の出荷(中)で抽出                                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST MC2                                                                                                 " & vbNewLine _
                                     & "ON  MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                                                      " & vbNewLine _
                                     & "AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                                                      " & vbNewLine _
                                     & "AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                                                      " & vbNewLine _
                                     & "AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                                                      " & vbNewLine _
                                     & "AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                                                    " & vbNewLine _
                                     & "AND MC2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                     & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                     & "--届先M(届先取得)(出荷L参照)                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST MDOUT                                             " & vbNewLine _
                                     & "ON  MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                         " & vbNewLine _
                                     & "AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                         " & vbNewLine _
                                     & "AND MDOUT.DEST_CD = OUTL.DEST_CD                                             " & vbNewLine _
                                     & "AND MDOUT.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "--届先M(売上先取得)(出荷L参照)                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST MDOUTU                                            " & vbNewLine _
                                     & "ON  MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                                     & "AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                        " & vbNewLine _
                                     & "AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                          " & vbNewLine _
                                     & "AND MDOUTU.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                     & "--届先M(届先取得)(出荷EDIL参照)                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST MDEDI                                             " & vbNewLine _
                                     & "ON  MDEDI.NRS_BR_CD = EDIL.NRS_BR_CD                                         " & vbNewLine _
                                     & "AND MDEDI.CUST_CD_L = EDIL.CUST_CD_L                                         " & vbNewLine _
                                     & "AND MDEDI.DEST_CD = EDIL.DEST_CD                                             " & vbNewLine _
                                     & "AND MDEDI.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "--【要望番号1123】埼玉対応 --- START ---                                     " & vbNewLine _
                                     & "--届先M(納品書荷主名義名取得)(届先M参照)                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST MC_SALES                                          " & vbNewLine _
                                     & "  ON MC_SALES.NRS_BR_CD  = MDOUT.NRS_BR_CD                                   " & vbNewLine _
                                     & " AND MC_SALES.CUST_CD_L  = MDOUT.SALES_CD                                    " & vbNewLine _
                                     & " AND MC_SALES.CUST_CD_M  = '00'                                              " & vbNewLine _
                                     & " AND MC_SALES.CUST_CD_S  = '00'                                              " & vbNewLine _
                                     & " AND MC_SALES.CUST_CD_SS = '00'                                              " & vbNewLine _
                                     & "--【要望番号1123】埼玉対応 ---  END  ---                                     " & vbNewLine _
                                     & "--運送会社M                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                            " & vbNewLine _
                                     & "ON  MUCO.NRS_BR_CD = UL.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MUCO.UNSOCO_CD = UL.UNSO_CD                                              " & vbNewLine _
                                     & "AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                        " & vbNewLine _
                                     & "AND MUCO.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--倉庫M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SOKO MSO                                               " & vbNewLine _
                                     & "ON  MSO.WH_CD = OUTL.WH_CD                                                   " & vbNewLine _
                                     & "AND MSO.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--2014/5/16Start s.kobayashi NotesNo.2183                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN11                                              " & vbNewLine _
                                     & "ON  KBN11.KBN_GROUP_CD = 'U028'  --追加                                      " & vbNewLine _
                                     & "AND UL.SEIQ_TARIFF_CD = KBN11.KBN_NM1                                        " & vbNewLine _
                                     & "--2014/5/16End s.kobayashi NotesNo.2183                                     " & vbNewLine _
                                     & "--距離程M(M_DEST.JIS < M_SOKO.JIS_CD)(出荷L参照)                             " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_KYORI MKY1                                             " & vbNewLine _
                                     & "ON  MKY1.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND MKY1.KYORI_CD = MC.BETU_KYORI_CD                                         " & vbNewLine _
                                     & "AND MKY1.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                      " & vbNewLine _
                                     & "AND MKY1.ORIG_JIS_CD = MDOUT.JIS                                             " & vbNewLine _
                                     & "AND MKY1.DEST_JIS_CD = MSO.JIS_CD                                            " & vbNewLine _
                                     & "AND MKY1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--距離程M(M_SOKO.JIS_CD < M_DEST.JIS)(出荷L参照)                             " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_KYORI MKY2                                               " & vbNewLine _
                                     & "ON  MKY2.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND MKY2.KYORI_CD = MC.BETU_KYORI_CD                                         " & vbNewLine _
                                     & "AND MKY2.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                      " & vbNewLine _
                                     & "AND MKY2.ORIG_JIS_CD = MSO.JIS_CD                                            " & vbNewLine _
                                     & "AND MKY2.DEST_JIS_CD = MDOUT.JIS                                             " & vbNewLine _
                                     & "AND MKY2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--距離程M(H_OUTKAEDI_L.DEST_JIS_CD < M_SOKO.JIS_CD)(出荷EDIL参照)            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_KYORI MKY3                                               " & vbNewLine _
                                     & "ON  MKY3.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND MKY3.KYORI_CD = MC.BETU_KYORI_CD                                         " & vbNewLine _
                                     & "AND MKY3.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                      " & vbNewLine _
                                     & "AND MKY3.ORIG_JIS_CD = EDIL.DEST_JIS_CD                                      " & vbNewLine _
                                     & "AND MKY3.DEST_JIS_CD = MSO.JIS_CD                                            " & vbNewLine _
                                     & "AND MKY3.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--距離程M(M_SOKO.JIS_CD < H_OUTKAEDI_L.DEST_JIS_CD)(出荷EDIL参照)            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_KYORI MKY4                                               " & vbNewLine _
                                     & "ON  MKY4.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND MKY4.KYORI_CD = MC.BETU_KYORI_CD                                         " & vbNewLine _
                                     & "AND MKY4.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                      " & vbNewLine _
                                     & "AND MKY4.ORIG_JIS_CD = MSO.JIS_CD                                            " & vbNewLine _
                                     & "AND MKY4.DEST_JIS_CD = EDIL.DEST_JIS_CD                                      " & vbNewLine _
                                     & "AND MKY4.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--ユーザM                                                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..S_USER MUSER                                             " & vbNewLine _
                                     & "ON MUSER.USER_CD = OUTL.SYS_ENT_USER                                         " & vbNewLine _
                                     & "AND MUSER.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "--区分M(納入予定区分)                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                               " & vbNewLine _
                                     & "ON  KBN1.KBN_GROUP_CD = 'N010'                                               " & vbNewLine _
                                     & "AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                         " & vbNewLine _
                                     & "AND KBN1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(元着払区分)                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                               " & vbNewLine _
                                     & "ON  KBN2.KBN_GROUP_CD = 'M001'                                               " & vbNewLine _
                                     & "AND KBN2.KBN_CD = OUTL.PC_KB                                                 " & vbNewLine _
                                     & "AND KBN2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(商品状態区分(中身))                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                               " & vbNewLine _
                                     & "ON  KBN3.KBN_GROUP_CD = 'S005'                                               " & vbNewLine _
                                     & "AND KBN3.KBN_CD = INS.GOODS_COND_KB_1                                        " & vbNewLine _
                                     & "AND KBN3.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(商品状態区分(外観))                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                               " & vbNewLine _
                                     & "ON  KBN4.KBN_GROUP_CD = 'S006'                                               " & vbNewLine _
                                     & "AND KBN4.KBN_CD = INS.GOODS_COND_KB_2                                        " & vbNewLine _
                                     & "AND KBN4.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--荷主状態(商品状態荷主)                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUSTCOND MCC                                           " & vbNewLine _
                                     & "ON  MCC.NRS_BR_CD = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MCC.CUST_CD_L = OUTL.CUST_CD_L                                           " & vbNewLine _
                                     & "--(2012.06.11) 要望番号1130 --- START ---                                    " & vbNewLine _
                                     & "--AND MCC.JOTAI_CD = INS.GOODS_COND_KB_3                                     " & vbNewLine _
                                     & "AND MCC.JOTAI_CD   = ZAI.GOODS_COND_KB_3                                     " & vbNewLine _
                                     & "--(2012.06.11) 要望番号1130 ---  END  ---                                    " & vbNewLine _
                                     & "AND MCC.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                          " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                          " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                    " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '05'                                                       " & vbNewLine _
                                     & "--帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                 " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                 " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                            " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                            " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '05'                                                       " & vbNewLine _
                                     & "--帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                  " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                           " & vbNewLine _
                                     & "LEFT LOOP JOIN $LM_MST$..M_RPT MR3                                                  " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR3.PTN_ID = '05'                                                        " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                 " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--荷主明細マスタ  出荷指示書の表記  ロジコネット/ゴードー                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                       " & vbNewLine _
                                     & "       ON MCD.NRS_BR_CD    = OUTL.NRS_BR_CD                                  " & vbNewLine _
                                     & "      AND MCD.CUST_CD      = OUTL.CUST_CD_L                                  " & vbNewLine _
                                     & "--      AND MCD.CUST_CD_EDA  = OUTL.CUST_CD_M                                  " & vbNewLine _
                                     & "      AND MCD.SUB_KB       = '28'                                            " & vbNewLine _
                                     & "      AND MCD.SYS_DEL_FLG  = '0'                                             " & vbNewLine _
                                     & "--  2014/12/26 LMC758,759対応追加  帳票コンビマスタ                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT_COMB MRC                                                " & vbNewLine _
                                     & "ON  MRC.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MRC.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
                                     & "AND MRC.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
                                     & "AND MRC.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--  2014/12/26 LMC758,759対応追加 拠点M 区分 送付先詳細                     " & vbNewLine _
                                     & "WHERE                                                                        " & vbNewLine _
                                     & "OUTL.SYS_DEL_FLG = '0'                                                       " & vbNewLine





    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = ") MAIN                                            " & vbNewLine _
                                         & "-- 輸出情報追加 START                                      " & vbNewLine _
                                         & "--輸出データL                                              " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..C_EXPORT_L     EXPORT                  " & vbNewLine _
                                         & "ON  EXPORT.NRS_BR_CD   = MAIN.NRS_BR_CD                    " & vbNewLine _
                                         & "AND EXPORT.OUTKA_NO_L  = MAIN.OUTKA_NO_L                   " & vbNewLine _
                                         & "AND EXPORT.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                         & "--届先マスタ(Shipper名)                                    " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST         EXPORT_DEST             " & vbNewLine _
                                         & "       ON EXPORT.NRS_BR_CD         = EXPORT_DEST.NRS_BR_CD " & vbNewLine _
                                         & "      AND MAIN.CUST_CD_L           = EXPORT_DEST.CUST_CD_L " & vbNewLine _
                                         & "      AND EXPORT.SHIPPER_CD        = EXPORT_DEST.DEST_CD   " & vbNewLine _
                                         & "      AND EXPORT_DEST.SYS_DEL_FLG  = '0'                   " & vbNewLine _
                                         & "--区分マスタ(コンテナサイズ名)                             " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN          EXPORT_KBN              " & vbNewLine _
                                         & "       ON EXPORT_KBN.KBN_GROUP_CD = 'C023'                 " & vbNewLine _
                                         & "      AND EXPORT_KBN.KBN_CD       = EXPORT.CONTAINER_SIZE  " & vbNewLine _
                                         & "      AND EXPORT_KBN.SYS_DEL_FLG  = '0'                    " & vbNewLine _
                                         & "-- 輸出情報追加 END                                        " & vbNewLine _
                                         & "GROUP BY                                          " & vbNewLine _
                                         & " MAIN.RPT_ID                                      " & vbNewLine _
                                         & ",MAIN.NRS_BR_CD                                   " & vbNewLine _
                                         & ",MAIN.PRINT_SORT                                  " & vbNewLine _
                                         & ",MAIN.TOU_BETU_FLG                                " & vbNewLine _
                                         & ",MAIN.OUTKA_NO_L                                  " & vbNewLine _
                                         & ",MAIN.DEST_CD                                     " & vbNewLine _
                                         & ",MAIN.DEST_NM                                     " & vbNewLine _
                                         & ",MAIN.DEST_AD_1                                   " & vbNewLine _
                                         & ",MAIN.DEST_AD_2                                   " & vbNewLine _
                                         & ",MAIN.DEST_AD_3                                   " & vbNewLine _
                                         & ",MAIN.DEST_TEL                                    " & vbNewLine _
                                         & ",MAIN.CUST_CD_L                                   " & vbNewLine _
                                         & ",MAIN.CUST_NM_L                                   " & vbNewLine _
                                         & ",MAIN.CUST_NM_M                                   " & vbNewLine _
                                         & ",MAIN.CUST_NM_S                                   " & vbNewLine _
                                         & ",MAIN.CUST_NM_S_H                                 " & vbNewLine _
                                         & ",MAIN.OUTKA_PKG_NB                                " & vbNewLine _
                                         & ",MAIN.CUST_ORD_NO                                 " & vbNewLine _
                                         & ",MAIN.BUYER_ORD_NO                                " & vbNewLine _
                                         & "--(2012.03.03)出庫日追加 LMC526対応 -- START --   " & vbNewLine _
                                         & ",MAIN.OUTKO_DATE                                  " & vbNewLine _
                                         & "--(2012.03.03)出庫日追加 LMC526対応 --  END  --   " & vbNewLine _
                                         & ",MAIN.OUTKA_PLAN_DATE                             " & vbNewLine _
                                         & ",MAIN.ARR_PLAN_DATE                               " & vbNewLine _
                                         & ",MAIN.ARR_PLAN_TIME                               " & vbNewLine _
                                         & ",MAIN.UNSOCO_NM                                   " & vbNewLine _
                                         & "--(2012.03.03)運送会社支店名追加 LMC526対応 -- START -- " & vbNewLine _
                                         & ",MAIN.UNSOCO_BR_NM                                      " & vbNewLine _
                                         & "--(2012.03.03)運送会社支店名追加 LMC526対応 --  END  -- " & vbNewLine _
                                         & ",MAIN.PC_KB                                       " & vbNewLine _
                                         & ",MAIN.KYORI                                       " & vbNewLine _
                                         & ",MAIN.UNSO_WT                                     " & vbNewLine _
                                         & ",MAIN.URIG_NM                                     " & vbNewLine _
                                         & ",MAIN.FREE_C03                                    " & vbNewLine _
                                         & ",MAIN.REMARK_L                                    " & vbNewLine _
                                         & ",MAIN.REMARK_UNSO                                 " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_1                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_1                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_1                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_2                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_2                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_2                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_3                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_3                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_3                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_4                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_4                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_4                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_5                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_5                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_5                                  " & vbNewLine _
                                         & ",MAIN.CRT_USER                                    " & vbNewLine _
                                         & ",MAIN.OUTKA_NO_M                                  " & vbNewLine _
                                         & ",MAIN.GOODS_NM                                    " & vbNewLine _
                                         & ",MAIN.FREE_C08                                    " & vbNewLine _
                                         & ",MAIN.IRIME                                       " & vbNewLine _
                                         & ",MAIN.IRIME_UT                                    " & vbNewLine _
                                         & ",MAIN.NB_UT                                       " & vbNewLine _
                                         & ",MAIN.FREE_C07                                    " & vbNewLine _
                                         & ",MAIN.SERIAL_NO                                   " & vbNewLine _
                                         & ",MAIN.PKG_NB                                      " & vbNewLine _
                                         & ",MAIN.PKG_UT                                      " & vbNewLine _
                                         & ",MAIN.ALCTD_KB                                    " & vbNewLine _
                                         & ",MAIN.REMARK_OUT                                  " & vbNewLine _
                                         & ",MAIN.LOT_NO                                      " & vbNewLine _
                                         & ",MAIN.LT_DATE                                     " & vbNewLine _
                                         & ",MAIN.INKA_DATE                                   " & vbNewLine _
                                         & ",MAIN.REMARK_S                                    " & vbNewLine _
                                         & ",MAIN.GOODS_COND_NM_1                             " & vbNewLine _
                                         & ",MAIN.GOODS_COND_NM_2                             " & vbNewLine _
                                         & ",MAIN.GOODS_CD_CUST                               " & vbNewLine _
                                         & ",MAIN.BETU_WT                                     " & vbNewLine _
                                         & ",MAIN.CUST_ORD_NO_DTL                             " & vbNewLine _
                                         & ",MAIN.TOU_NO                                      " & vbNewLine _
                                         & ",MAIN.SITU_NO                                     " & vbNewLine _
                                         & ",MAIN.ZONE_CD                                     " & vbNewLine _
                                         & ",MAIN.LOCA                                        " & vbNewLine _
                                         & ",MAIN.REMARK_M                                    " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_1                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_1                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_1                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_2                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_2                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_2                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_3                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_3                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_3                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_4                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_4                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_4                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_5                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_5                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_5                              " & vbNewLine _
                                         & ",MAIN.SAIHAKKO_FLG                                " & vbNewLine _
                                         & ",MAIN.OYA_CUST_GOODS_CD                           " & vbNewLine _
                                         & ",MAIN.OYA_GOODS_NM		                          " & vbNewLine _
                                         & ",MAIN.OYA_KATA		                              " & vbNewLine _
                                         & ",MAIN.OYA_OUTKA_TTL_NB                            " & vbNewLine _
                                         & ",MAIN.SET_NAIYO                                   " & vbNewLine _
                                         & ",MAIN.GOODS_COND_NM_3                             " & vbNewLine _
                                         & ",MAIN.RPT_FLG                                     " & vbNewLine _
                                         & ",MAIN.OUTKA_NO_S    --20120511 LMC528対応         " & vbNewLine _
                                         & ",MAIN.WH_CD                                       " & vbNewLine _
                                         & ",MAIN.CUST_NAIYO_1                                " & vbNewLine _
                                         & ",MAIN.CUST_NAIYO_2                                " & vbNewLine _
                                         & ",MAIN.CUST_NAIYO_3                                " & vbNewLine _
                                         & " --(2012.06.09) 要望番号1123対応 -- STRAT --      " & vbNewLine _
                                         & ",MAIN.DEST_REMARK                                 " & vbNewLine _
                                         & ",MAIN.DEST_SALES_CD		                          " & vbNewLine _
                                         & ",MAIN.DEST_SALES_NM_L                             " & vbNewLine _
                                         & ",MAIN.DEST_SALES_NM_M                             " & vbNewLine _
                                         & " --(2012.06.09) 要望番号1123対応 --  END  --      " & vbNewLine _
                                         & " --(2015.10.27) シンガポール対応 -- STRAT --      " & vbNewLine _
                                         & ",MAIN.GOODS_NM_2                                  " & vbNewLine _
                                         & ",MAIN.SEARCH_KEY_1                                          " & vbNewLine _
                                         & " --(2015.10.27) シンガポール対応 --  END  --      " & vbNewLine _
                                         & " --(2016.01.08) シンガポール対応 --  START  --    " & vbNewLine _
                                         & " ,MAIN.INNER_PKG_NB                               " & vbNewLine _
                                         & " ,MAIN.ALCTD_NB                                   " & vbNewLine _
                                         & " --(2016.01.08) シンガポール対応 --  END  --      " & vbNewLine _
                                         & ",EXPORT.SHIP_NM                                   " & vbNewLine _
                                         & ",EXPORT.DESTINATION                               " & vbNewLine _
                                         & ",EXPORT.BOOKING_NO                                " & vbNewLine _
                                         & ",EXPORT.VOYAGE_NO                                 " & vbNewLine _
                                         & ",EXPORT.SHIPPER_CD                                " & vbNewLine _
                                         & ",EXPORT_DEST.DEST_NM                              " & vbNewLine _
                                         & ",EXPORT.CONT_LOADING_DATE                         " & vbNewLine _
                                         & ",EXPORT.STORAGE_TEST_DATE                         " & vbNewLine _
                                         & ",EXPORT.STORAGE_TEST_TIME                         " & vbNewLine _
                                         & ",EXPORT.DEPARTURE_DATE                            " & vbNewLine _
                                         & ",EXPORT.CONTAINER_NO                              " & vbNewLine _
                                         & ",EXPORT.CONTAINER_NM                              " & vbNewLine _
                                         & ",EXPORT.CONTAINER_SIZE                            " & vbNewLine _
                                         & ",EXPORT_KBN.KBN_NM1                               " & vbNewLine _
                                         & ",MAIN.UN                                          " & vbNewLine _
                                         & ",MAIN.OUTKA_ATT                                   " & vbNewLine


    '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_FREE As String = "SELECT                                                                                           " & vbNewLine _
                                               & " MAIN.RPT_ID		               AS RPT_ID                                                           " & vbNewLine _
                                               & ",MAIN.NRS_BR_CD		               AS NRS_BR_CD                                                     " & vbNewLine _
                                               & ",MAIN.PRINT_SORT	                AS PRINT_SORT                                                   " & vbNewLine _
                                               & ",MAIN.TOU_BETU_FLG	                AS TOU_BETU_FLG                                               " & vbNewLine _
                                               & ",MAIN.OUTKA_NO_L	                AS OUTKA_NO_L                                                   " & vbNewLine _
                                               & ",MAIN.DEST_CD		                 AS DEST_CD                                                       " & vbNewLine _
                                               & ",MAIN.DEST_NM		                 AS DEST_NM                                                       " & vbNewLine _
                                               & ",MAIN.DEST_AD_1		                 AS DEST_AD_1                                                   " & vbNewLine _
                                               & ",MAIN.DEST_AD_2		                 AS DEST_AD_2                                                   " & vbNewLine _
                                               & ",MAIN.DEST_AD_3		                  AS DEST_AD_3                                                  " & vbNewLine _
                                               & ",MAIN.DEST_TEL		                 AS DEST_TEL                                                     " & vbNewLine _
                                               & ",MAIN.CUST_CD_L		                 AS CUST_CD_L                                                   " & vbNewLine _
                                               & ",MAIN.CUST_NM_L		                 AS CUST_NM_L                                                   " & vbNewLine _
                                               & ",MAIN.CUST_NM_M		                AS CUST_NM_M                                                  " & vbNewLine _
                                               & ",MAIN.CUST_NM_S		                AS CUST_NM_S                                                  " & vbNewLine _
                                               & ",MAIN.CUST_NM_S_H		                AS CUST_NM_S_H                                                  " & vbNewLine _
                                               & ",MAIN.OUTKA_PKG_NB	                AS OUTKA_PKG_NB                                             " & vbNewLine _
                                               & ",MAIN.CUST_ORD_NO                   	AS CUST_ORD_NO                                              " & vbNewLine _
                                               & ",MAIN.BUYER_ORD_NO	                AS BUYER_ORD_NO                                             " & vbNewLine _
                                               & ",MAIN.OUTKA_PLAN_DATE	                AS OUTKA_PLAN_DATE                                          " & vbNewLine _
                                               & ",MAIN.ARR_PLAN_DATE	                AS ARR_PLAN_DATE                                           " & vbNewLine _
                                               & ",MAIN.ARR_PLAN_TIME	                AS ARR_PLAN_TIME                                           " & vbNewLine _
                                               & ",MAIN.UNSOCO_NM		                AS UNSOCO_NM                                                  " & vbNewLine _
                                               & ",MAIN.PC_KB		                    AS PC_KB                                                      " & vbNewLine _
                                               & ",MAIN.KYORI		                    AS KYORI                                                     " & vbNewLine _
                                               & ",MAIN.UNSO_WT                      	AS UNSO_WT                                                  " & vbNewLine _
                                               & ",MAIN.URIG_NM	                       	AS URIG_NM                                                 " & vbNewLine _
                                               & ",''        	                     	AS FREE_C03                                                 " & vbNewLine _
                                               & ",MAIN.REMARK_L	                    AS REMARK_L                                               " & vbNewLine _
                                               & ",MAIN.REMARK_UNSO                   	AS REMARK_UNSO                                              " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_1                	AS SAGYO_REC_NO_1                                           " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_1	                    AS SAGYO_CD_1                                                  " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_1	                  AS SAGYO_NM_1                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_2                	AS SAGYO_REC_NO_2                                           " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_2	                  AS SAGYO_CD_2                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_2	                  AS SAGYO_NM_2                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_3	              AS SAGYO_REC_NO_3                                             " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_3	                  AS SAGYO_CD_3                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_3	                  AS SAGYO_NM_3                                                 " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_4	                AS SAGYO_REC_NO_4                                           " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_4	                    AS SAGYO_CD_4                                               " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_4                      	AS SAGYO_NM_4                                             " & vbNewLine _
                                               & ",MAIN.SAGYO_REC_NO_5	               AS SAGYO_REC_NO_5                                            " & vbNewLine _
                                               & ",MAIN.SAGYO_CD_5	                   AS SAGYO_CD_5                                                " & vbNewLine _
                                               & ",MAIN.SAGYO_NM_5	                   AS SAGYO_NM_5                                                " & vbNewLine _
                                               & ",MAIN.CRT_USER	                      	AS CRT_USER                                                " & vbNewLine _
                                               & ",MAIN.OUTKA_NO_M	                    AS OUTKA_NO_M                                               " & vbNewLine _
                                               & ",MAIN.GOODS_NM	                    	AS GOODS_NM                                                  " & vbNewLine _
                                               & ",MAIN.FREE_C08	                    	AS FREE_C08                                                  " & vbNewLine _
                                               & ",MAIN.IRIME		                       AS IRIME                                                     " & vbNewLine _
                                               & ",MAIN.IRIME_UT		                   AS IRIME_UT                                                   " & vbNewLine _
                                               & ",SUM(MAIN.ALCTD_NB) / MAIN.PKG_NB      AS KONSU                                                  " & vbNewLine _
                                               & ",SUM(MAIN.ALCTD_NB) % MAIN.PKG_NB      AS HASU                                                   " & vbNewLine _
                                               & ",SUM(MAIN.ALCTD_NB)                    AS ALCTD_NB                                               " & vbNewLine _
                                               & ",MAIN.NB_UT			                   AS NB_UT                                                        " & vbNewLine _
                                               & ",MIN(MAIN.ALCTD_CAN_NB)                AS ALCTD_CAN_NB                                           " & vbNewLine _
                                               & ",MAIN.FREE_C07			               AS FREE_C07                                                      " & vbNewLine _
                                               & ",SUM(MAIN.ALCTD_QT)                    AS ALCTD_QT                                               " & vbNewLine _
                                               & ",MIN(MAIN.ALCTD_CAN_NB) / MAIN.PKG_NB  AS ZAN_KONSU                                              " & vbNewLine _
                                               & ",MIN(MAIN.ALCTD_CAN_NB) % MAIN.PKG_NB  AS ZAN_HASU                                               " & vbNewLine _
                                               & ",MAIN.SERIAL_NO		         AS SERIAL_NO                                                           " & vbNewLine _
                                               & ",MAIN.PKG_NB	         	 AS PKG_NB                                                                " & vbNewLine _
                                               & ",MAIN.PKG_UT        		 AS PKG_UT                                                                 " & vbNewLine _
                                               & ",MAIN.ALCTD_KB		        AS ALCTD_KB                                                              " & vbNewLine _
                                               & ",MIN(MAIN.ALCTD_CAN_QT)     AS ALCTD_CAN_QT                                                      " & vbNewLine _
                                               & ",MAIN.REMARK_OUT			AS REMARK_OUT                                                                 " & vbNewLine _
                                               & ",MAIN.LOT_NO		        AS LOT_NO                                                                  " & vbNewLine _
                                               & ",MAIN.LT_DATE		        AS LT_DATE                                                                " & vbNewLine _
                                               & ",MAIN.INKA_DATE 		        AS INKA_DATE                                                            " & vbNewLine _
                                               & ",MAIN.REMARK_S		        AS REMARK_S                                                              " & vbNewLine _
                                               & ",MAIN.GOODS_COND_NM_1     	AS GOODS_COND_NM_1                                                    " & vbNewLine _
                                               & ",MAIN.GOODS_COND_NM_2	    AS GOODS_COND_NM_2                                                     " & vbNewLine _
                                               & ",MAIN.GOODS_CD_CUST      	AS GOODS_CD_CUST                                                       " & vbNewLine _
                                               & ",MAIN.BETU_WT		        AS BETU_WT                                                                " & vbNewLine _
                                               & ",MAIN.CUST_ORD_NO_DTL	    AS CUST_ORD_NO_DTL                                                     " & vbNewLine _
                                               & ",MAIN.TOU_NO		        AS TOU_NO                                                                  " & vbNewLine _
                                               & ",MAIN.SITU_NO		        AS SITU_NO                                                                " & vbNewLine _
                                               & ",RTRIM(MAIN.ZONE_CD)       AS ZONE_CD                                                                " & vbNewLine _
                                               & ",MAIN.LOCA		            AS LOCA                                                                  " & vbNewLine _
                                               & ",MAIN.REMARK_M		    AS REMARK_M                                                                " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_1	AS SAGYO_MEI_REC_NO_1                                                   " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_1		AS SAGYO_MEI_CD_1                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_1		AS SAGYO_MEI_NM_1                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_2	AS SAGYO_MEI_REC_NO_2                                                   " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_2		AS SAGYO_MEI_CD_2                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_2		AS SAGYO_MEI_NM_2                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_3	AS SAGYO_MEI_REC_NO_3                                                   " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_3		AS SAGYO_MEI_CD_3                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_3		AS SAGYO_MEI_NM_3                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_4	AS SAGYO_MEI_REC_NO_4                                                   " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_4		AS SAGYO_MEI_CD_4                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_4		AS SAGYO_MEI_NM_4                                                          " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_REC_NO_5	AS SAGYO_MEI_REC_NO_5                                                  " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_CD_5		AS SAGYO_MEI_CD_5                                                      " & vbNewLine _
                                               & ",MAIN.SAGYO_MEI_NM_5		AS SAGYO_MEI_NM_5                                                      " & vbNewLine _
                                               & ",MAIN.SAIHAKKO_FLG		AS SAIHAKKO_FLG                                                        " & vbNewLine _
                                               & ",MAIN.OYA_CUST_GOODS_CD   AS OYA_CUST_GOODS_CD                                                   " & vbNewLine _
                                               & ",MAIN.OYA_GOODS_NM		AS OYA_GOODS_NM                                                        " & vbNewLine _
                                               & ",MAIN.OYA_KATA		    AS OYA_KATA                                                            " & vbNewLine _
                                               & ",MAIN.OYA_OUTKA_TTL_NB    AS OYA_OUTKA_TTL_NB                                                    " & vbNewLine _
                                               & ",MAIN.SET_NAIYO AS SET_NAIYO                                                                     " & vbNewLine _
                                               & " --(2012.03.03) 出庫日、運送会社支店名 追加 LMC526対応 -- STRAT --                               " & vbNewLine _
                                               & ",MAIN.OUTKO_DATE          AS OUTKO_DATE                                                          " & vbNewLine _
                                               & ",MAIN.UNSOCO_BR_NM		AS UNSOCO_BR_NM                                                        " & vbNewLine _
                                               & " --(2012.03.03) 出庫日、運送会社支店名 追加 LMC526対応 --  END  --                               " & vbNewLine _
                                               & ",MAIN.GOODS_COND_NM_3	    AS GOODS_COND_NM_3                                                     " & vbNewLine _
                                               & ",MAIN.RPT_FLG             AS RPT_FLG           --20120313                                        " & vbNewLine _
                                               & ",MAIN.OUTKA_NO_S          AS OUTKA_NO_S        --20120511 LMC528対応                             " & vbNewLine _
                                               & ",MAIN.WH_CD               AS WH_CD             --20120528                                        " & vbNewLine _
                                               & ",MAIN.CUST_NAIYO_1        AS CUST_NAIYO_1      --20120528                                        " & vbNewLine _
                                               & ",MAIN.CUST_NAIYO_2        AS CUST_NAIYO_2      --20120528                                        " & vbNewLine _
                                               & ",MAIN.CUST_NAIYO_3        AS CUST_NAIYO_3      --20120528                                        " & vbNewLine _
                                               & " --(2012.06.09) 要望番号1123対応 -- STRAT --                                                     " & vbNewLine _
                                               & ",MAIN.DEST_REMARK         AS DEST_REMARK                                                         " & vbNewLine _
                                               & ",MAIN.DEST_SALES_CD		AS DEST_SALES_CD                                                       " & vbNewLine _
                                               & ",MAIN.DEST_SALES_NM_L		AS DEST_SALES_NM_L                                                     " & vbNewLine _
                                               & ",MAIN.DEST_SALES_NM_M		AS DEST_SALES_NM_M                                                     " & vbNewLine _
                                               & " --(2012.06.09) 要望番号1123対応 --  END  --                                                     " & vbNewLine _
                                               & " --(2012.11.13) LMC537対応 -- STRAT --                                                           " & vbNewLine _
                                               & ",''        	            AS ALCTD_NB_HEADKEI                                                    " & vbNewLine _
                                               & ",''        	            AS ALCTD_QT_HEADKEI                                                    " & vbNewLine _
                                               & ",MAIN.HINMEI 	            AS HINMEI                                                              " & vbNewLine _
                                               & ",MAIN.NISUGATA            AS NISUGATA                                                            " & vbNewLine _
                                               & " --(2012.11.13) LMC537対応 --  END  --                                                           " & vbNewLine _
                                               & " --(2015.10.27) シンガポール対応 --  START  --                                                   " & vbNewLine _
                                               & " ,MAIN.GOODS_NM_2 AS GOODS_NM_2                                                                  " & vbNewLine _
                                               & " ,MAIN.SEARCH_KEY_1 AS SEARCH_KEY_1                                                                        " & vbNewLine _
                                               & " --(2015.10.25) シンガポール対応 --  END  --                                                     " & vbNewLine _
                                               & "FROM                                                                                             " & vbNewLine _
                                               & "(SELECT                                                                                          " & vbNewLine _
                                               & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                    " & vbNewLine _
                                               & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                    " & vbNewLine _
                                               & "      ELSE MR3.RPT_ID                                                                                                " & vbNewLine _
                                               & " END              AS RPT_ID                                                                                          " & vbNewLine _
                                               & ",OUTL.NRS_BR_CD   AS NRS_BR_CD                                                                                       " & vbNewLine _
                                               & ",OUTM.PRINT_SORT  AS PRINT_SORT                                                                                      " & vbNewLine _
                                               & ",'0'  AS TOU_BETU_FLG                                                                                                " & vbNewLine _
                                               & ",OUTL.OUTKA_NO_L  AS OUTKA_NO_L                                                                                      " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                                           " & vbNewLine _
                                               & "      ELSE OUTL.DEST_CD                                                                                              " & vbNewLine _
                                               & " END              AS DEST_CD                                                                                         " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                                               " & vbNewLine _
                                               & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                                     " & vbNewLine _
                                               & "      ELSE MDOUT.DEST_NM                                                                                                       " & vbNewLine _
                                               & " END              AS DEST_NM                                                                                                   " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                                             " & vbNewLine _
                                               & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                                   " & vbNewLine _
                                               & "      ELSE MDOUT.AD_1                                                                                                          " & vbNewLine _
                                               & " END              AS DEST_AD_1                                                                                                 " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                                             " & vbNewLine _
                                               & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                                   " & vbNewLine _
                                               & "      ELSE MDOUT.AD_2                                                                                                          " & vbNewLine _
                                               & " END              AS DEST_AD_2                                                                                                 " & vbNewLine _
                                               & ",OUTL.DEST_AD_3   AS DEST_AD_3                                                                                       " & vbNewLine _
                                               & ",OUTL.DEST_TEL    AS DEST_TEL                                                                                        " & vbNewLine _
                                               & ",OUTL.CUST_CD_L     AS CUST_CD_L                                                                                     " & vbNewLine _
                                               & ",MC.CUST_NM_L                   AS CUST_NM_L                                                              " & vbNewLine _
                                               & ",MC.CUST_NM_M                   AS CUST_NM_M                                                              " & vbNewLine _
                                               & ",MC.CUST_NM_S                   AS CUST_NM_S                                                              " & vbNewLine _
                                               & ",MC2.CUST_NM_S                  AS CUST_NM_S_H                                                              " & vbNewLine _
                                               & ",OUTL.OUTKA_PKG_NB   AS OUTKA_PKG_NB                                                                                 " & vbNewLine _
                                               & ",OUTL.CUST_ORD_NO    AS CUST_ORD_NO                                                                                  " & vbNewLine _
                                               & ",OUTL.BUYER_ORD_NO   AS BUYER_ORD_NO                                                                                 " & vbNewLine _
                                               & " --(2012.03.03) 出庫日追加 LMC526対応 -- START --                                                                    " & vbNewLine _
                                               & ",OUTL.OUTKO_DATE        AS OUTKO_DATE                                                                                " & vbNewLine _
                                               & " --(2012.03.03) 出庫日追加 LMC526対応 --  END  --                                                                    " & vbNewLine _
                                               & ",OUTL.OUTKA_PLAN_DATE   AS OUTKA_PLAN_DATE                                                                           " & vbNewLine _
                                               & ",OUTL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                                                                             " & vbNewLine _
                                               & ",KBN1.KBN_NM1               AS ARR_PLAN_TIME                                                                         " & vbNewLine _
                                               & ",MUCO.UNSOCO_NM             AS UNSOCO_NM                                                                             " & vbNewLine _
                                               & " --(2012.03.03) 運送会社支店名 追加 LMC526対応 -- STRAT --                                                           " & vbNewLine _
                                               & ",MUCO.UNSOCO_BR_NM          AS UNSOCO_BR_NM                                                                          " & vbNewLine _
                                               & " --(2012.03.03) 運送会社支店名 追加 LMC526対応 --  END  --                                                           " & vbNewLine _
                                               & ",OUTL.PC_KB                 AS PC_KB                                                                                 " & vbNewLine _
                                               & ",CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                      " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD <= MSO.JIS_CD) THEN MKY3.KYORI     " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD <= EDIL.DEST_JIS_CD) THEN MKY4.KYORI     " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                          " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS <= MSO.JIS_CD) THEN MKY1.KYORI                " & vbNewLine _
                                               & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD <= MDOUT.JIS) THEN MKY2.KYORI                " & vbNewLine _
                                               & "      ELSE 0                                                                                                         " & vbNewLine _
                                               & " END             AS KYORI                                                                                            " & vbNewLine _
                                               & ",UL.UNSO_WT      AS UNSO_WT                                                                                          " & vbNewLine _
                                               & ",MDOUTU.DEST_NM  AS URIG_NM                                                                                          " & vbNewLine _
                                               & ",''              AS FREE_C03                                                                                         " & vbNewLine _
                                               & "--,OUTL.REMARK     AS REMARK_L                                                                                         " & vbNewLine _
                                               & ",CASE WHEN MGD2.SET_NAIYO = 1 THEN                                                                                   " & vbNewLine _
                                               & "    CASE WHEN MDD.SET_NAIYO = 1 THEN                                                                                 " & vbNewLine _
                                               & "        OUTL.REMARK + ' ' + MDD.REMARK                                                                               " & vbNewLine _
                                               & "    ELSE OUTL.REMARK                                                                                                 " & vbNewLine _
                                               & "    END                                                                                                              " & vbNewLine _
                                               & " ELSE OUTL.REMARK                                                                                                    " & vbNewLine _
                                               & " END AS REMARK_L                                                                                                     " & vbNewLine _
                                               & ",UL.REMARK       AS REMARK_UNSO                                                                                      " & vbNewLine _
                                               & ",@SAIHAKKO_FLG     AS SAIHAKKO_FLG                                                                                   " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_REC_NO_1                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_CD_1                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_NM_1                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_REC_NO_2                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_CD_2                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_NM_2                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_REC_NO_3                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_CD_3                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_NM_3                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_REC_NO_4                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_CD_4                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_NM_4                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_REC_NO_5                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_CD_5                                                                               " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_NM_5                                                                               " & vbNewLine _
                                               & ",MUSER.USER_NM      AS CRT_USER                                                                                      " & vbNewLine _
                                               & ",OUTM.OUTKA_NO_M   AS OUTKA_NO_M                                                                                     " & vbNewLine _
    					                       & "-- 追加開始 --- 2014.08.01 kikuchi                                                                                   " & vbNewLine _                                                                                   
	    				                       & ",CASE WHEN CUD.SUB_KB = '79' THEN EDIM.GOODS_NM                                                                      " & vbNewLine _
		    			                       & " ELSE MG.GOODS_NM_1                                                                                                  " & vbNewLine _
			    		                       & " END AS GOODS_NM                                                                                                     " & vbNewLine _
				    	                       & "-- 追加終了 ---                                                                                                      " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C08                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS FREE_C08                                                                                       " & vbNewLine _
                                               & "--,OUTS.IRIME        AS IRIME                                                                                        " & vbNewLine _
                                               & ",CASE WHEN OUTS.OUTKA_NO_S IS NULL THEN OUTM.IRIME                                                                   " & vbNewLine _
                                               & "      ELSE OUTS.IRIME END     AS IRIME                                                                               " & vbNewLine _
                                               & ",MG.STD_IRIME_UT   AS IRIME_UT                                                                                       " & vbNewLine _
                                               & "--,OUTS.ALCTD_NB     AS ALCTD_NB                                                                                     " & vbNewLine _
                                               & ",CASE WHEN OUTS.OUTKA_NO_S IS NULL THEN OUTM.BACKLOG_NB                                                              " & vbNewLine _
                                               & "      ELSE OUTS.ALCTD_NB END     AS ALCTD_NB                                                                         " & vbNewLine _
                                               & ",MG.NB_UT          AS NB_UT                                                                                          " & vbNewLine _
                                               & ",OUTS.ALCTD_CAN_NB    AS ALCTD_CAN_NB                                                                                " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C07                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS FREE_C07                                                                                       " & vbNewLine _
                                               & "--,OUTS.ALCTD_QT     AS ALCTD_QT                                                                                     " & vbNewLine _
                                               & ",CASE WHEN OUTS.OUTKA_NO_S IS NULL THEN OUTM.BACKLOG_QT                                                              " & vbNewLine _
                                               & "      ELSE OUTS.ALCTD_QT END     AS ALCTD_QT                                                                         " & vbNewLine _
                                               & ",OUTS.SERIAL_NO    AS SERIAL_NO                                                                                      " & vbNewLine _
                                               & ",MG.PKG_NB         AS PKG_NB                                                                                         " & vbNewLine _
                                               & ",MG.PKG_UT         AS PKG_UT                                                                                         " & vbNewLine _
                                               & ",OUTM.ALCTD_KB       AS ALCTD_KB                                                                                       " & vbNewLine _
                                               & ",OUTS.ALCTD_CAN_QT AS ALCTD_CAN_QT                                                                                   " & vbNewLine _
                                               & ",ZAI.REMARK_OUT    AS REMARK_OUT                                                                                     " & vbNewLine _
                                               & ",OUTS.LOT_NO       AS LOT_NO                                                                                         " & vbNewLine _
                                               & ",OUTS.OUTKA_NO_S   AS OUTKA_NO_S                  --20120511 LMC528対応                                              " & vbNewLine _
                                               & ",INS.LT_DATE       AS LT_DATE                                                                                        " & vbNewLine _
                                               & ",CASE WHEN INL.INKA_STATE_KB < '50' THEN INL.INKA_DATE                                                                         " & vbNewLine _
                                               & "      ELSE ZAI.INKO_DATE                                                                                                       " & vbNewLine _
                                               & " END                    AS INKA_DATE                                                                                           " & vbNewLine _
                                               & ",OUTS.REMARK       AS REMARK_S                                                                                       " & vbNewLine _
                                               & ",KBN3.KBN_NM1                 AS GOODS_COND_NM_1                                                                     " & vbNewLine _
                                               & ",KBN4.KBN_NM1                 AS GOODS_COND_NM_2                                                                     " & vbNewLine _
                                               & ",MG.GOODS_CD_CUST      AS GOODS_CD_CUST                                                                              " & vbNewLine _
                                               & ",OUTS.BETU_WT      AS BETU_WT                                                                                        " & vbNewLine _
                                               & ",OUTM.CUST_ORD_NO_DTL         AS CUST_ORD_NO_DTL                                                                     " & vbNewLine _
                                               & ",OUTS.TOU_NO       AS TOU_NO                                                                                         " & vbNewLine _
                                               & ",OUTS.SITU_NO      AS SITU_NO                                                                                        " & vbNewLine _
                                               & ",RTRIM(OUTS.ZONE_CD)      AS ZONE_CD                                                                                        " & vbNewLine _
                                               & ",OUTS.LOCA         AS LOCA                                                                                           " & vbNewLine _
                                               & ",OUTM.REMARK       AS REMARK_M                                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_REC_NO_1                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_CD_1                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_NM_1                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_REC_NO_2                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_CD_2                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_NM_2                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_REC_NO_3                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_CD_3                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_NM_3                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_REC_NO_4                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_CD_4                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_NM_4                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_REC_NO_5                                                                       " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_CD_5                                                                           " & vbNewLine _
                                               & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
                                               & " FROM                                                                                                                " & vbNewLine _
                                               & "(SELECT                                                                                                              " & vbNewLine _
                                               & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
                                               & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
                                               & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
                                               & " ) AS BASE                                                                                                           " & vbNewLine _
                                               & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_NM_5                                                                           " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C11                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS OYA_CUST_GOODS_CD                                                                              " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C12                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS OYA_GOODS_NM                                                                                   " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_C13                                                            " & vbNewLine _
                                               & "      ELSE ''                                                                                                        " & vbNewLine _
                                               & " END               AS OYA_KATA                                                                                       " & vbNewLine _
                                               & ",CASE WHEN EDIM.EDI_CTL_NO IS NOT NULL THEN EDIM.FREE_N02                                                            " & vbNewLine _
                                               & "      ELSE 0                                                                                                         " & vbNewLine _
                                               & " END               AS OYA_OUTKA_TTL_NB                                                                               " & vbNewLine _
                                               & " ,MGD.SET_NAIYO     AS SET_NAIYO                                                                                     " & vbNewLine _
                                               & " ,'' AS RPT_FLG  --20120313                                                                                          " & vbNewLine _
                                               & " ,ISNULL(MCC.JOTAI_NM,'') AS GOODS_COND_NM_3  --20120313                                                             " & vbNewLine _
                                               & " ,OUTL.WH_CD AS WH_CD                  --20120528                                                                    " & vbNewLine _
                                               & " ,MCD.CUST_NAIYO_1 AS CUST_NAIYO_1     --20120528                                                                    " & vbNewLine _
                                               & " ,MCD.CUST_NAIYO_2 AS CUST_NAIYO_2     --20120528                                                                    " & vbNewLine _
                                               & " ,MCD.CUST_NAIYO_3 AS CUST_NAIYO_3     --20120528                                                                    " & vbNewLine _
                                               & " --(2012.06.09) 要望番号1123対応 -- STRAT --                                                                         " & vbNewLine _
                                               & " ,MDOUT.REMARK            AS DEST_REMARK                                                                             " & vbNewLine _                                                            
                                               & " ,MDOUT.SALES_CD          AS DEST_SALES_CD                                                                           " & vbNewLine _             
                                               & " ,MC_SALES.CUST_NM_L      AS DEST_SALES_NM_L                                                                         " & vbNewLine _
                                               & " ,MC_SALES.CUST_NM_M      AS DEST_SALES_NM_M                                                                         " & vbNewLine _
                                               & " --(2012.06.09) 要望番号1123対応 --  END  --                                                                         " & vbNewLine _
                                               & " --(2012.11.13) LMC537対応 -- STRAT --                                                                               " & vbNewLine _
                                               & " ,MGD_00.HINMEI           AS HINMEI                                                                                  " & vbNewLine _  
                                               & " ,MGD_01.NISUGATA         AS NISUGATA                                                                                " & vbNewLine _
                                               & " --(2012.11.13) LMC537対応 -- STRAT --                                                                               " & vbNewLine _
                                               & " --(2015.10.27) シンガポール対応 --  START  --                                                                       " & vbNewLine _ 
                                               & ",MG.SEARCH_KEY_1 AS SEARCH_KEY_1                                                                                     " & vbNewLine _
	    				                       & ",CASE WHEN CUD.SUB_KB = '79' THEN ''                                                                                 " & vbNewLine _
		    			                       & " ELSE MG.GOODS_NM_2                                                                                                  " & vbNewLine _
			    		                       & " END AS GOODS_NM_2                                                                                                   " & vbNewLine _
                                               & "--(2015.10.27) シンガポール対応 --  END  --                                                                          " & vbNewLine _

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_FREE As String = "--出荷L                                                                 " & vbNewLine _
                                     & "FROM                                                                         " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                     " & vbNewLine _
                                     & "--トランザクションテーブル                                                   " & vbNewLine _
                                     & "--出荷M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                           " & vbNewLine _
                                     & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                     & "AND OUTM.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                     & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                     & "LEFT OUTER JOIN                                                                                                                " & vbNewLine _
                                     & "       (SELECT                                                                                                                 " & vbNewLine _
                                     & "           NRS_BR_CD                                                                                                           " & vbNewLine _
                                     & "           ,OUTKA_NO_L                                                                                                         " & vbNewLine _
                                     & "           ,MIN(OUTKA_NO_M) AS  OUTKA_NO_M                                                                                     " & vbNewLine _
                                     & "       FROM $LM_TRN$..C_OUTKA_M WHERE SYS_DEL_FLG ='0'                                                                         " & vbNewLine _
                                     & "       GROUP BY NRS_BR_CD,OUTKA_NO_L) OUTM_MIN                                                                                 " & vbNewLine _
                                     & "       ON OUTM_MIN.NRS_BR_CD        = OUTL.NRS_BR_CD                                                                           " & vbNewLine _
                                     & "       AND OUTM_MIN.OUTKA_NO_L      = OUTL.OUTKA_NO_L                                                                          " & vbNewLine _
                                     & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM2                                                                                            " & vbNewLine _
                                     & "ON  OUTM2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                     & "AND OUTM2.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                         " & vbNewLine _
                                     & "AND OUTM2.OUTKA_NO_M = OUTM_MIN.OUTKA_NO_M                                                                                     " & vbNewLine _
                                     & "AND OUTM2.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                     & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                     & "-- 追加開始 --- 2014.08.01 kikuchi                                                                                             " & vbNewLine _
                                     & "LEFT JOIN LM_MST..M_CUST_DETAILS CUD                                                                                           " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = CUD.NRS_BR_CD                                                                                             " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = CUD.CUST_CD                                                                                               " & vbNewLine _
                                     & "AND CUD.SUB_KB = '79'                                                                                                          " & vbNewLine _
                                     & "AND CUD.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                     & "-- 追加終了 ---                                                                                                                " & vbNewLine _
                                     & "--出荷S                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                           " & vbNewLine _
                                     & "ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                     & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                        " & vbNewLine _
                                     & "AND OUTS.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--出荷EDIL                                                                   " & vbNewLine _
                                     & "--(2012.09.11)要望番号1412対応  --- START ---                                " & vbNewLine _
                                     & "--出荷EDIL                                                                   " & vbNewLine _
                                     & "--LEFT JOIN                                                                  " & vbNewLine _
                                     & "--(                                                                          " & vbNewLine _
                                     & "--SELECT                                                                     " & vbNewLine _
                                     & "-- NRS_BR_CD                                                                 " & vbNewLine _
                                     & "--,OUTKA_CTL_NO                                                              " & vbNewLine _
                                     & "--,CUST_CD_L                                                                 " & vbNewLine _
                                     & "--,SHIP_NM_L                                                                 " & vbNewLine _
                                     & "--,MIN(DEST_CD)    AS DEST_CD                                                " & vbNewLine _
                                     & "--,MIN(DEST_NM)    AS DEST_NM                                                " & vbNewLine _
                                     & "--,MIN(DEST_AD_1)  AS DEST_AD_1                                              " & vbNewLine _
                                     & "--,MIN(DEST_AD_2)  AS DEST_AD_2                                              " & vbNewLine _
                                     & "--,DEST_JIS_CD                                                               " & vbNewLine _
                                     & "--,FREE_C03                                                                  " & vbNewLine _
                                     & "--,SYS_DEL_FLG                                                               " & vbNewLine _
                                     & "--FROM                                                                       " & vbNewLine _
                                     & "--$LM_TRN$..H_OUTKAEDI_L                                                     " & vbNewLine _
                                     & "--GROUP BY                                                                   " & vbNewLine _
                                     & "-- NRS_BR_CD                                                                 " & vbNewLine _
                                     & "--,OUTKA_CTL_NO                                                              " & vbNewLine _
                                     & "--,CUST_CD_L                                                                 " & vbNewLine _
                                     & "--,SHIP_NM_L                                                                 " & vbNewLine _
                                     & "--,DEST_JIS_CD                                                               " & vbNewLine _
                                     & "--,FREE_C03                                                                  " & vbNewLine _
                                     & "--,SYS_DEL_FLG                                                               " & vbNewLine _
                                     & "--) EDIL                                                                     " & vbNewLine _
                                     & "--ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                                     & "--AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                    " & vbNewLine _
                                     & "--AND EDIL.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                     & "--下記の内容に変更                                                           " & vbNewLine _
                                     & " LEFT JOIN (                                                                 " & vbNewLine _
                                     & "            SELECT                                                           " & vbNewLine _
                                     & "                   NRS_BR_CD                                                 " & vbNewLine _
                                     & "                 , EDI_CTL_NO                                                " & vbNewLine _
                                     & "                 , OUTKA_CTL_NO                                              " & vbNewLine _
                                     & "             FROM (                                                          " & vbNewLine _
                                     & "                    SELECT                                                            " & vbNewLine _
                                     & "                           EDIOUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "                         , EDIOUTL.EDI_CTL_NO                                         " & vbNewLine _
                                     & "                         , EDIOUTL.OUTKA_CTL_NO                                       " & vbNewLine _
                                     & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                 " & vbNewLine _
                                     & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD     " & vbNewLine _
                                     & "                                                              , EDIOUTL.OUTKA_CTL_NO  " & vbNewLine _
                                     & "                                                       ORDER BY EDIOUTL.NRS_BR_CD     " & vbNewLine _
                                     & "                                                              , EDIOUTL.EDI_CTL_NO    " & vbNewLine _
                                     & "                                                  )                                   " & vbNewLine _
                                     & "                           END AS IDX                                                 " & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                     " & vbNewLine _
                                     & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                         " & vbNewLine _
                                     & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                  " & vbNewLine _
                                     & "                      AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                 " & vbNewLine _
                                     & "                  ) EBASE                                                    " & vbNewLine _
                                     & "            WHERE EBASE.IDX = 1                                              " & vbNewLine _
                                     & "            ) TOPEDI                                                         " & vbNewLine _
                                     & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                              " & vbNewLine _
                                     & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                             " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                       " & vbNewLine _
                                     & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                " & vbNewLine _
                                     & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                               " & vbNewLine _
                                     & "--(2012.09.11)要望番号1412対応  ---  END  ---                                " & vbNewLine _
                                     & "----出荷EDIM                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                          " & vbNewLine _
                                     & "ON  EDIM.NRS_BR_CD = OUTM.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND EDIM.OUTKA_CTL_NO = OUTM.OUTKA_NO_L                                      " & vbNewLine _
                                     & "AND EDIM.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M                                  " & vbNewLine _
                                     & "AND EDIM.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--入荷L                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..B_INKA_L INL                                               " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND INL.INKA_NO_L = OUTS.INKA_NO_L                                           " & vbNewLine _
                                     & "AND INL.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--入荷S                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                               " & vbNewLine _
                                     & "ON  INS.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_L = OUTS.INKA_NO_L                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_M = OUTS.INKA_NO_M                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_S = OUTS.INKA_NO_S                                           " & vbNewLine _
                                     & "AND INS.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--運送L                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                " & vbNewLine _
                                     & "ON  UL.NRS_BR_CD = OUTL.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                     & "AND UL.MOTO_DATA_KB = '20'                                                   " & vbNewLine _
                                     & "AND UL.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                     & "--在庫レコード                                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                " & vbNewLine _
                                     & "ON  ZAI.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                         " & vbNewLine _
                                     & "AND ZAI.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--マスタテーブル                                                             " & vbNewLine _
                                     & "--商品M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                                 " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                      " & vbNewLine _
                                     & "--商品DetlM                                                                  " & vbNewLine _
                                     & "LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO         " & vbNewLine _
                                     & "   FROM $LM_MST$..M_GOODS_DETAILS  WHERE SUB_KB ='07' AND SYS_DEL_FLG ='0'   " & vbNewLine _
                                     & "   GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD                                    " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MGD.NRS_BR_CD                                             " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = MGD.GOODS_CD_NRS                                       " & vbNewLine _
                                     & "LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO         " & vbNewLine _
                                     & "   FROM LM_MST..M_GOODS_DETAILS  WHERE SUB_KB ='42' AND SYS_DEL_FLG ='0'     " & vbNewLine _
                                     & "   GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD2                                   " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MGD2.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = MGD2.GOODS_CD_NRS                                      " & vbNewLine _
                                     & "LEFT JOIN LM_MST..M_DEST_DETAILS MDD                                         " & vbNewLine _
                                     & "ON  MDD.NRS_BR_CD = EDIL.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MDD.CUST_CD_L = EDIL.CUST_CD_L                                           " & vbNewLine _
                                     & "AND MDD.DEST_CD = EDIL.DEST_CD                                               " & vbNewLine _
                                     & "AND MDD.SUB_KB = '04'                                                        " & vbNewLine _
                                     & "AND MDD.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--★★★追加開始--------------------------------                               " & vbNewLine _
                                     & "--商品M(MIN)                                                                 " & vbNewLine _
                                     & "LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                " & vbNewLine _
                                     & "ON M_GOODS_MIN.NRS_BR_CD      = OUTL.NRS_BR_CD                               " & vbNewLine _
                                     & "AND M_GOODS_MIN.GOODS_CD_NRS   = OUTM2.GOODS_CD_NRS                          " & vbNewLine _
                                     & "--★★★追加終了--------------------------------                             " & vbNewLine _
                                     & "--荷主M(商品M経由)                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST MC                                                " & vbNewLine _
                                     & "ON  MC.NRS_BR_CD = MG.NRS_BR_CD                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_L = MG.CUST_CD_L                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_M = MG.CUST_CD_M                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_S = MG.CUST_CD_S                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                            " & vbNewLine _
                                     & "AND MC.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                     & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                     & "--荷主M(商品M経由) 最小の出荷(中)で抽出                                                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST MC2                                                                                                       " & vbNewLine _
                                     & "ON  MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                                                            " & vbNewLine _
                                     & "AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                                                            " & vbNewLine _
                                     & "AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                                                            " & vbNewLine _
                                     & "AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                                                            " & vbNewLine _
                                     & "AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                                                          " & vbNewLine _
                                     & "AND MC2.SYS_DEL_FLG = '0'                                                                                                            " & vbNewLine _
                                     & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                     & "--■■■追加開始--------------------------------                                                                                     " & vbNewLine _
                                     & "--荷主明細                                                                                                                           " & vbNewLine _
                                     & "LEFT JOIN (SELECT NRS_BR_CD,CUST_CD,CUST_CLASS,SET_NAIYO AS CUST_NAIYO_1,SET_NAIYO_2 AS CUST_NAIYO_2,SET_NAIYO_3 AS CUST_NAIYO_3     " & vbNewLine _
                                     & "   FROM $LM_MST$..M_CUST_DETAILS  WHERE SUB_KB ='28' AND SYS_DEL_FLG ='0'                                                            " & vbNewLine _
                                     & " ) MCD                                                                                                                               " & vbNewLine _
                                     & "ON  MC.NRS_BR_CD = MCD.NRS_BR_CD                                                                                                     " & vbNewLine _
                                     & "AND                                                                                                                                  " & vbNewLine _
                                     & "(CASE WHEN MCD.CUST_CLASS = '00' THEN MC.CUST_CD_L                                                                                   " & vbNewLine _
                                     & "      WHEN MCD.CUST_CLASS = '01' THEN (MC.CUST_CD_L + MC.CUST_CD_M)                                                                  " & vbNewLine _
                                     & "      WHEN MCD.CUST_CLASS = '02' THEN (MC.CUST_CD_L + MC.CUST_CD_M + MC.CUST_CD_S)                                                   " & vbNewLine _
                                     & "      WHEN MCD.CUST_CLASS = '03' THEN (MC.CUST_CD_L + MC.CUST_CD_M + MC.CUST_CD_S + MC.CUST_CD_SS)                                   " & vbNewLine _
                                     & "      END                                                                    " & vbNewLine _
                                     & ") = MCD.CUST_CD                                                              " & vbNewLine _
                                     & "--■■■追加終了--------------------------------                             " & vbNewLine _
                                     & "--届先M(届先取得)(出荷L参照)                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST MDOUT                                             " & vbNewLine _
                                     & "ON  MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                         " & vbNewLine _
                                     & "AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                         " & vbNewLine _
                                     & "AND MDOUT.DEST_CD = OUTL.DEST_CD                                             " & vbNewLine _
                                     & "AND MDOUT.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "--届先M(売上先取得)(出荷L参照)                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST MDOUTU                                            " & vbNewLine _
                                     & "ON  MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                                     & "AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                        " & vbNewLine _
                                     & "AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                          " & vbNewLine _
                                     & "AND MDOUTU.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                     & "--届先M(届先取得)(出荷EDIL参照)                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST MDEDI                                             " & vbNewLine _
                                     & "ON  MDEDI.NRS_BR_CD = EDIL.NRS_BR_CD                                         " & vbNewLine _
                                     & "AND MDEDI.CUST_CD_L = EDIL.CUST_CD_L                                         " & vbNewLine _
                                     & "AND MDEDI.DEST_CD = EDIL.DEST_CD                                             " & vbNewLine _
                                     & "AND MDEDI.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "--【要望番号1123】埼玉対応 --- START ---                                     " & vbNewLine _
                                     & "--届先M(納品書荷主名義名取得)(届先M参照)                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST MC_SALES                                          " & vbNewLine _
                                     & "  ON MC_SALES.NRS_BR_CD  = MDOUT.NRS_BR_CD                                   " & vbNewLine _
                                     & " AND MC_SALES.CUST_CD_L  = MDOUT.SALES_CD                                    " & vbNewLine _
                                     & " AND MC_SALES.CUST_CD_M  = '00'                                              " & vbNewLine _
                                     & " AND MC_SALES.CUST_CD_S  = '00'                                              " & vbNewLine _
                                     & " AND MC_SALES.CUST_CD_SS = '00'                                              " & vbNewLine _
                                     & "--【要望番号1123】埼玉対応 ---  END  ---                                     " & vbNewLine _
                                     & "--運送会社M                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                            " & vbNewLine _
                                     & "ON  MUCO.NRS_BR_CD = UL.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MUCO.UNSOCO_CD = UL.UNSO_CD                                              " & vbNewLine _
                                     & "AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                        " & vbNewLine _
                                     & "AND MUCO.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--倉庫M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SOKO MSO                                               " & vbNewLine _
                                     & "ON  MSO.WH_CD = OUTL.WH_CD                                                   " & vbNewLine _
                                     & "AND MSO.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--2014/5/16Start s.kobayashi NotesNo.2183                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN11                                              " & vbNewLine _
                                     & "ON  KBN11.KBN_GROUP_CD = 'U028'  --追加                                      " & vbNewLine _
                                     & "AND UL.SEIQ_TARIFF_CD = KBN11.KBN_NM1                                        " & vbNewLine _
                                     & "--2014/5/16End s.kobayashi NotesNo.2183                                     " & vbNewLine _
                                     & "--距離程M(M_DEST.JIS < M_SOKO.JIS_CD)(出荷L参照)                             " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_KYORI MKY1                                             " & vbNewLine _
                                     & "ON  MKY1.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND MKY1.KYORI_CD = MC.BETU_KYORI_CD                                         " & vbNewLine _
                                     & "AND MKY1.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                      " & vbNewLine _
                                     & "AND MKY1.ORIG_JIS_CD = MDOUT.JIS                                             " & vbNewLine _
                                     & "AND MKY1.DEST_JIS_CD = MSO.JIS_CD                                            " & vbNewLine _
                                     & "AND MKY1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--距離程M(M_SOKO.JIS_CD < M_DEST.JIS)(出荷L参照)                             " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_KYORI MKY2                                             " & vbNewLine _
                                     & "ON  MKY2.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND MKY2.KYORI_CD = MC.BETU_KYORI_CD                                         " & vbNewLine _
                                     & "AND MKY2.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                      " & vbNewLine _
                                     & "AND MKY2.ORIG_JIS_CD = MSO.JIS_CD                                            " & vbNewLine _
                                     & "AND MKY2.DEST_JIS_CD = MDOUT.JIS                                             " & vbNewLine _
                                     & "AND MKY2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--距離程M(H_OUTKAEDI_L.DEST_JIS_CD < M_SOKO.JIS_CD)(出荷EDIL参照)            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_KYORI MKY3                                             " & vbNewLine _
                                     & "ON  MKY3.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND MKY3.KYORI_CD = MC.BETU_KYORI_CD                                         " & vbNewLine _
                                     & "AND MKY3.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                      " & vbNewLine _
                                     & "AND MKY3.ORIG_JIS_CD = EDIL.DEST_JIS_CD                                      " & vbNewLine _
                                     & "AND MKY3.DEST_JIS_CD = MSO.JIS_CD                                            " & vbNewLine _
                                     & "AND MKY3.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--距離程M(M_SOKO.JIS_CD < H_OUTKAEDI_L.DEST_JIS_CD)(出荷EDIL参照)            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_KYORI MKY4                                             " & vbNewLine _
                                     & "ON  MKY4.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND MKY4.KYORI_CD = MC.BETU_KYORI_CD                                         " & vbNewLine _
                                     & "AND MKY4.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                      " & vbNewLine _
                                     & "AND MKY4.ORIG_JIS_CD = MSO.JIS_CD                                            " & vbNewLine _
                                     & "AND MKY4.DEST_JIS_CD = EDIL.DEST_JIS_CD                                      " & vbNewLine _
                                     & "AND MKY4.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--ユーザM                                                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..S_USER MUSER                                             " & vbNewLine _
                                     & "ON MUSER.USER_CD = OUTL.SYS_ENT_USER                                            " & vbNewLine _
                                     & "AND MUSER.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "--区分M(納入予定区分)                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                               " & vbNewLine _
                                     & "ON  KBN1.KBN_GROUP_CD = 'N010'                                               " & vbNewLine _
                                     & "AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                         " & vbNewLine _
                                     & "AND KBN1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(元着払区分)                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                               " & vbNewLine _
                                     & "ON  KBN2.KBN_GROUP_CD = 'M001'                                               " & vbNewLine _
                                     & "AND KBN2.KBN_CD = OUTL.PC_KB                                                 " & vbNewLine _
                                     & "AND KBN2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(商品状態区分(中身))                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                               " & vbNewLine _
                                     & "ON  KBN3.KBN_GROUP_CD = 'S005'                                               " & vbNewLine _
                                     & "AND KBN3.KBN_CD = INS.GOODS_COND_KB_1                                        " & vbNewLine _
                                     & "AND KBN3.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(商品状態区分(外観))                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                               " & vbNewLine _
                                     & "ON  KBN4.KBN_GROUP_CD = 'S006'                                               " & vbNewLine _
                                     & "AND KBN4.KBN_CD = INS.GOODS_COND_KB_2                                        " & vbNewLine _
                                     & "AND KBN4.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--荷主状態(商品状態荷主)                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUSTCOND MCC                                           " & vbNewLine _
                                     & "ON  MCC.NRS_BR_CD = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MCC.CUST_CD_L = OUTL.CUST_CD_L                                           " & vbNewLine _
                                     & "--(2012.06.11) 要望番号1130 --- START ---                                    " & vbNewLine _
                                     & "--AND MCC.JOTAI_CD = INS.GOODS_COND_KB_3                                     " & vbNewLine _
                                     & "AND MCC.JOTAI_CD   = ZAI.GOODS_COND_KB_3                                     " & vbNewLine _
                                     & "--(2012.06.11) 要望番号1130 ---  END  ---                                    " & vbNewLine _
                                     & "AND MCC.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & " --(2012.11.13) LMC537対応 -- STRAT --                                       " & vbNewLine _
                                     & "LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS HINMEI            " & vbNewLine _
                                     & "     FROM $LM_MST$..M_GOODS_DETAILS  WHERE GOODS_CD_NRS_EDA = '02' AND SUB_KB ='18' AND SYS_DEL_FLG ='0'  " & vbNewLine _
                                     & "     GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD_00                               " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MGD_00.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = MGD_00.GOODS_CD_NRS                                    " & vbNewLine _
                                     & "LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS NISUGATA          " & vbNewLine _
                                     & "     FROM $LM_MST$..M_GOODS_DETAILS  WHERE GOODS_CD_NRS_EDA = '03' AND SUB_KB ='19' AND SYS_DEL_FLG ='0'  " & vbNewLine _
                                     & "     GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD_01                               " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MGD_01.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = MGD_01.GOODS_CD_NRS                                    " & vbNewLine _
                                     & " --(2012.11.13) LMC537対応 -- STRAT --                                       " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                          " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                          " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                    " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '05'                                                       " & vbNewLine _
                                     & "--帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                 " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                 " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                          " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                            " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '05'                                                       " & vbNewLine _
                                     & "--帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                           " & vbNewLine _
                                     & "LEFT LOOP JOIN $LM_MST$..M_RPT MR3                                           " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR3.PTN_ID = '05'                                                        " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                 " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "WHERE                                                                        " & vbNewLine _
                                     & "OUTL.SYS_DEL_FLG = '0'                                                       " & vbNewLine


    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_FREE As String = ") MAIN                                       " & vbNewLine _
                                         & "GROUP BY                                          " & vbNewLine _
                                         & " MAIN.RPT_ID                                      " & vbNewLine _
                                         & ",MAIN.NRS_BR_CD                                   " & vbNewLine _
                                         & ",MAIN.PRINT_SORT                                  " & vbNewLine _
                                         & ",MAIN.TOU_BETU_FLG                                " & vbNewLine _
                                         & ",MAIN.OUTKA_NO_L                                  " & vbNewLine _
                                         & ",MAIN.DEST_CD                                     " & vbNewLine _
                                         & ",MAIN.DEST_NM                                     " & vbNewLine _
                                         & ",MAIN.DEST_AD_1                                   " & vbNewLine _
                                         & ",MAIN.DEST_AD_2                                   " & vbNewLine _
                                         & ",MAIN.DEST_AD_3                                   " & vbNewLine _
                                         & ",MAIN.DEST_TEL                                    " & vbNewLine _
                                         & ",MAIN.CUST_CD_L                                   " & vbNewLine _
                                         & ",MAIN.CUST_NM_L                                   " & vbNewLine _
                                         & ",MAIN.CUST_NM_M                                   " & vbNewLine _
                                         & ",MAIN.CUST_NM_S                                   " & vbNewLine _
                                         & ",MAIN.CUST_NM_S_H                                 " & vbNewLine _
                                         & ",MAIN.OUTKA_PKG_NB                                " & vbNewLine _
                                         & ",MAIN.CUST_ORD_NO                                 " & vbNewLine _
                                         & ",MAIN.BUYER_ORD_NO                                " & vbNewLine _
                                         & "--(2012.03.03)出庫日追加 LMC526対応 -- START --   " & vbNewLine _
                                         & ",MAIN.OUTKO_DATE                                  " & vbNewLine _
                                         & "--(2012.03.03)出庫日追加 LMC526対応 --  END  --   " & vbNewLine _
                                         & ",MAIN.OUTKA_PLAN_DATE                             " & vbNewLine _
                                         & ",MAIN.ARR_PLAN_DATE                               " & vbNewLine _
                                         & ",MAIN.ARR_PLAN_TIME                               " & vbNewLine _
                                         & ",MAIN.UNSOCO_NM                                   " & vbNewLine _
                                         & "--(2012.03.03)運送会社支店名追加 LMC526対応 -- START -- " & vbNewLine _
                                         & ",MAIN.UNSOCO_BR_NM                                      " & vbNewLine _
                                         & "--(2012.03.03)運送会社支店名追加 LMC526対応 --  END  -- " & vbNewLine _
                                         & ",MAIN.PC_KB                                       " & vbNewLine _
                                         & ",MAIN.KYORI                                       " & vbNewLine _
                                         & ",MAIN.UNSO_WT                                     " & vbNewLine _
                                         & ",MAIN.URIG_NM                                     " & vbNewLine _
                                         & ",MAIN.FREE_C03                                    " & vbNewLine _
                                         & ",MAIN.REMARK_L                                    " & vbNewLine _
                                         & ",MAIN.REMARK_UNSO                                 " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_1                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_1                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_1                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_2                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_2                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_2                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_3                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_3                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_3                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_4                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_4                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_4                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_REC_NO_5                              " & vbNewLine _
                                         & ",MAIN.SAGYO_CD_5                                  " & vbNewLine _
                                         & ",MAIN.SAGYO_NM_5                                  " & vbNewLine _
                                         & ",MAIN.CRT_USER                                    " & vbNewLine _
                                         & ",MAIN.OUTKA_NO_M                                  " & vbNewLine _
                                         & ",MAIN.GOODS_NM                                    " & vbNewLine _
                                         & ",MAIN.FREE_C08                                    " & vbNewLine _
                                         & ",MAIN.IRIME                                       " & vbNewLine _
                                         & ",MAIN.IRIME_UT                                    " & vbNewLine _
                                         & ",MAIN.NB_UT                                       " & vbNewLine _
                                         & ",MAIN.FREE_C07                                    " & vbNewLine _
                                         & ",MAIN.SERIAL_NO                                   " & vbNewLine _
                                         & ",MAIN.PKG_NB                                      " & vbNewLine _
                                         & ",MAIN.PKG_UT                                      " & vbNewLine _
                                         & ",MAIN.ALCTD_KB                                    " & vbNewLine _
                                         & ",MAIN.REMARK_OUT                                  " & vbNewLine _
                                         & ",MAIN.LOT_NO                                      " & vbNewLine _
                                         & ",MAIN.LT_DATE                                     " & vbNewLine _
                                         & ",MAIN.INKA_DATE                                   " & vbNewLine _
                                         & ",MAIN.REMARK_S                                    " & vbNewLine _
                                         & ",MAIN.GOODS_COND_NM_1                             " & vbNewLine _
                                         & ",MAIN.GOODS_COND_NM_2                             " & vbNewLine _
                                         & ",MAIN.GOODS_CD_CUST                               " & vbNewLine _
                                         & ",MAIN.BETU_WT                                     " & vbNewLine _
                                         & ",MAIN.CUST_ORD_NO_DTL                             " & vbNewLine _
                                         & ",MAIN.TOU_NO                                      " & vbNewLine _
                                         & ",MAIN.SITU_NO                                     " & vbNewLine _
                                         & ",MAIN.ZONE_CD                                     " & vbNewLine _
                                         & ",MAIN.LOCA                                        " & vbNewLine _
                                         & ",MAIN.REMARK_M                                    " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_1                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_1                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_1                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_2                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_2                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_2                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_3                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_3                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_3                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_4                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_4                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_4                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_REC_NO_5                          " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_CD_5                              " & vbNewLine _
                                         & ",MAIN.SAGYO_MEI_NM_5                              " & vbNewLine _
                                         & ",MAIN.SAIHAKKO_FLG                                " & vbNewLine _
                                         & ",MAIN.OYA_CUST_GOODS_CD                           " & vbNewLine _
                                         & ",MAIN.OYA_GOODS_NM		                          " & vbNewLine _
                                         & ",MAIN.OYA_KATA		                              " & vbNewLine _
                                         & ",MAIN.OYA_OUTKA_TTL_NB                            " & vbNewLine _
                                         & ",MAIN.SET_NAIYO                                   " & vbNewLine _
                                         & ",MAIN.GOODS_COND_NM_3                             " & vbNewLine _
                                         & ",MAIN.RPT_FLG                                     " & vbNewLine _
                                         & ",MAIN.OUTKA_NO_S      --20120511 LMC528対応       " & vbNewLine _
                                         & ",MAIN.WH_CD           --20120528                  " & vbNewLine _
                                         & ",MAIN.CUST_NAIYO_1    --20120528                  " & vbNewLine _
                                         & ",MAIN.CUST_NAIYO_2    --20120528                  " & vbNewLine _
                                         & ",MAIN.CUST_NAIYO_3    --20120528                  " & vbNewLine _
                                         & " --(2012.06.09) 要望番号1123対応 -- STRAT --      " & vbNewLine _
                                         & ",MAIN.DEST_REMARK                                 " & vbNewLine _
                                         & ",MAIN.DEST_SALES_CD		                          " & vbNewLine _
                                         & ",MAIN.DEST_SALES_NM_L                             " & vbNewLine _
                                         & ",MAIN.DEST_SALES_NM_M                             " & vbNewLine _
                                         & " --(2012.06.09) 要望番号1123対応 --  END  --      " & vbNewLine _
                                         & " --(2012.11.13) LMC537対応 -- STRAT --            " & vbNewLine _
                                         & " ,MAIN.HINMEI                                     " & vbNewLine _
                                         & " ,MAIN.NISUGATA                                   " & vbNewLine _
                                         & " --(2012.11.13) LMC537対応 -- STRAT --            " & vbNewLine


    '(2012.04.10) Notes№962 FREE_C03使用有無対応 --  END  --



    '2次対応  荷姿並び替え 2012.01.17 START

    ''' <summary>
    ''' ORDER BY（①営業所コード、②管理番号L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY1 As String = "ORDER BY                                          " & vbNewLine _
                                         & "     MAIN.NRS_BR_CD                                " & vbNewLine _
                                         & "    ,MAIN.OUTKA_NO_L                               " & vbNewLine

    ''' <summary>
    ''' ORDER BY（③印刷順番、④管理番号M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "   ,MAIN.PRINT_SORT                               " & vbNewLine _
                                         & "    ,MAIN.OUTKA_NO_M                               " & vbNewLine

    '要望番号:1802（出荷指示書　ソート順にOUTKA_NO_Sも含める) 2013/01/29 本明 Start
    ''' <summary>
    ''' ORDER BY（⑥管理番号S）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY3 As String = "   ,MAIN.OUTKA_NO_S                               " & vbNewLine
    '要望番号:1802（出荷指示書　ソート順にOUTKA_NO_Sも含める) 2013/01/29 本明 End



    '2次対応  荷姿並び替え 2012.01.17 END


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
        Dim inTbl As DataTable = ds.Tables("LMC520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC812DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC812DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'MyBase.Logger.WriteSQLLog("LMC520DAC", "SelectMPrt", cmd)

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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectTouNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC812DAC.SQL_SELECT_TouNo)      'SQL構築(帳票種別用Select句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'MyBase.Logger.WriteSQLLog("LMC520DAC", "SelectTouNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("TOU_NO", "TOU_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "TOU_NO_LIST")

        Return ds

    End Function

    '2次対応 荷姿並び替え 2012.01.17 START

    ''' <summary>
    '''荷主明細マスタ(設定値)取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks>荷主明細マスタ取得SQLの構築・発行</remarks>
    Private Function SelectMCustDetailsData(ByVal ds As DataSet, ByVal SyoriKBN As String) As DataSet

        'INTableの条件rowの格納
        Me._Row = ds.Tables("LMC520IN").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
        'SQL作成
        Select Case SyoriKBN
            Case "0"
                '荷姿並び順
                Me._StrSql.Append(LMC812DAC.SQL_SELECT_MCUST_DETAILS)      'SQL構築(荷主明細マスタ設定値Select句)
                Call Me.setIndataParameter(Me._Row)                        '条件設定
            Case "1"
                'FREE_C03項目使用有無
                Me._StrSql.Append(LMC812DAC.SQL_SELECT_MCUST_DETAILS_FREE) 'SQL構築(荷主明細マスタ設定値Select句)
                Call Me.setIndataParameter(Me._Row)                        '条件設定

                '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- START --
            Case "2"
                'FREE_C03項目使用有無
                Me._StrSql.Append(LMC812DAC.SQL_SELECT_MCUST_DETAILS_CHKLIST) 'SQL構築(荷主明細マスタ設定値Select句)
                Call Me.setIndataParameter(Me._Row)                        '条件設定
                '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- END --
            Case Else

        End Select
        'Me._StrSql.Append(LMC520DAC.SQL_SELECT_MCUST_DETAILS)      'SQL構築(荷主明細マスタ設定値Select句)
        'Call Me.setIndataParameter(Me._Row)                        '条件設定

        '(2012.04.10) Notes№962 FREE_C03使用有無対応 --  END  --

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'MyBase.Logger.WriteSQLLog("LMC520DAC", "SelectMCustDetailsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")

        'ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SET_NAIYO")
        Select Case SyoriKBN
            Case "0"
                '荷姿並び順
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SET_NAIYO")
            Case "1"
                'FREE_C03項目使用有無
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SET_NAIYO_FREE")

                '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- START --
            Case "2"
                'FREE_C03項目使用有無
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SET_NAIYO_CHKLIST")
                '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- END --
            Case Else

        End Select

        reader.Close()

        Return ds

    End Function

    '2次対応 荷姿並び替え 2012.01.17 END

    ''' <summary>
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC520IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")    '(2012.07.25) コンソリ業務対応 [追加]

        '2次対応 荷姿並び替え 2012.01.17 START
        '荷主明細マスタの設定値取得
        Dim setNaiyo As String = String.Empty
        'Me.SelectMCustDetailsData(inTbl, setNaiyo)
        Me.SelectMCustDetailsData(ds, "0")
        'Me.SelectMCustDetailsData(ds)
        If ds.Tables("SET_NAIYO").Rows.Count > 0 Then
            setNaiyo = ds.Tables("SET_NAIYO").Rows(0)("SET_NAIYO").ToString()
        End If
        '2次対応 荷姿並び替え 2012.01.17 END

        '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
        Dim FreeC03_Umu As String = String.Empty
        Me.SelectMCustDetailsData(ds, "1")
        If ds.Tables("SET_NAIYO_FREE").Rows.Count > 0 Then
            FreeC03_Umu = ds.Tables("SET_NAIYO_FREE").Rows(0)("SET_NAIYO").ToString()
        End If
        '(2012.04.10) Notes№962 FREE_C03使用有無対応 --  END  --

        '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- START --
        ds = Me.SelectMCustDetailsData(ds, "2")
        '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- START --

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMC812DAC.SQL_SELECT_DATA)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMC812DAC.SQL_FROM)            'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMC812DAC.SQL_GROUP_BY)        'SQL構築(データ抽出用From句)
        'Call Me.setIndataParameter(Me._Row)
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '2次対応 荷姿並び替え 2012.01.17 START
        '(並び替え)設定値名称設定
        'If String.IsNullOrEmpty(setNaiyo) = False Then
        '    sql = Me.SetNaiyoNm(sql, setNaiyo)
        'End If
        ''2次対応 荷姿並び替え 2012.01.17 END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC520DAC", "SelectPrintData", cmd)

        '障害対応・・ひとまず無制限
        cmd.CommandTimeout = 0

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("TOU_BETU_FLG", "TOU_BETU_FLG")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("PC_KB", "PC_KB")
        map.Add("KYORI", "KYORI")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("URIG_NM", "URIG_NM")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("REMARK_UNSO", "REMARK_UNSO")
        map.Add("SAGYO_REC_NO_1", "SAGYO_REC_NO_1")
        map.Add("SAGYO_CD_1", "SAGYO_CD_1")
        map.Add("SAGYO_NM_1", "SAGYO_NM_1")
        map.Add("SAGYO_REC_NO_2", "SAGYO_REC_NO_2")
        map.Add("SAGYO_CD_2", "SAGYO_CD_2")
        map.Add("SAGYO_NM_2", "SAGYO_NM_2")
        map.Add("SAGYO_REC_NO_3", "SAGYO_REC_NO_3")
        map.Add("SAGYO_CD_3", "SAGYO_CD_3")
        map.Add("SAGYO_NM_3", "SAGYO_NM_3")
        map.Add("SAGYO_REC_NO_4", "SAGYO_REC_NO_4")
        map.Add("SAGYO_CD_4", "SAGYO_CD_4")
        map.Add("SAGYO_NM_4", "SAGYO_NM_4")
        map.Add("SAGYO_REC_NO_5", "SAGYO_REC_NO_5")
        map.Add("SAGYO_CD_5", "SAGYO_CD_5")
        map.Add("SAGYO_NM_5", "SAGYO_NM_5")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("ALCTD_CAN_NB", "ALCTD_CAN_NB")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ZAN_KONSU", "ZAN_KONSU")
        map.Add("ZAN_HASU", "ZAN_HASU")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("REMARK_S", "REMARK_S")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
        map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
        map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
        map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
        map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
        map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
        map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
        map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
        map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
        map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
        map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
        map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
        map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
        map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
        map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
        map.Add("SAIHAKKO_FLG", "SAIHAKKO_FLG")
        map.Add("OYA_CUST_GOODS_CD", "OYA_CUST_GOODS_CD")
        map.Add("OYA_GOODS_NM", "OYA_GOODS_NM")
        map.Add("OYA_KATA", "OYA_KATA")
        map.Add("OYA_OUTKA_TTL_NB", "OYA_OUTKA_TTL_NB")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("OUTKO_DATE", "OUTKO_DATE")               '(2012.03.03) LMC526対応 出庫日 追加
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")           '(2012.03.03) LMC526対応 運送会社支店名 追加
        map.Add("CUST_NM_S_H", "CUST_NM_S_H")
        map.Add("RPT_FLG", "RPT_FLG")                     '(2012.03.13) LMC527対応
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")     '(2012.03.13) LMC527対応
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")               '(2012.05.11) LMC528対応 出荷管理番号S 追加
        map.Add("WH_CD", "WH_CD")                         '(2012.05.29) LMC529対応
        map.Add("CUST_NAIYO_1", "CUST_NAIYO_1")           '(2012.05.29) LMC529対応
        map.Add("CUST_NAIYO_2", "CUST_NAIYO_2")           '(2012.05.29) LMC529対応
        map.Add("CUST_NAIYO_3", "CUST_NAIYO_3")           '(2012.05.29) LMC529対応
        map.Add("DEST_REMARK", "DEST_REMARK")             '(2012.06.09) LMC529対応 要望番号1123 届先Ｍ 備考追加
        map.Add("DEST_SALES_CD", "DEST_SALES_CD")         '(2012.06.09) LMC529対応 要望番号1123 届先Ｍ 納品書荷主名義コード 追加
        map.Add("DEST_SALES_NM_L", "DEST_SALES_NM_L")     '(2012.06.09) LMC529対応 要望番号1123 届先Ｍ 納品書荷主名義の名称 追加
        map.Add("DEST_SALES_NM_M", "DEST_SALES_NM_M")     '(2012.06.09) LMC529対応 要望番号1123 届先Ｍ 納品書荷主名義の名称 追加
        map.Add("ALCTD_NB_HEADKEI", "ALCTD_NB_HEADKEI")   '(2012.11.13) LMC537対応
        map.Add("ALCTD_QT_HEADKEI", "ALCTD_QT_HEADKEI")   '(2012.11.13) LMC537対応
        map.Add("HINMEI", "HINMEI")                       '(2012.11.13) LMC537対応
        map.Add("NISUGATA", "NISUGATA")                   '(2012.11.13) LMC537対応
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")                               '20151027 シンガポール対応
        map.Add("GOODS_NM_2", "GOODS_NM_2")              '20151027 シンガポール対応

        map.Add("INNER_PKG_NB", "INNER_PKG_NB")           '2016.01.08 シンガポール対応
        map.Add("S_TOTAL", "S_TOTAL")                     '2016.01.08 シンガポール対応
        '輸出情報追加 START
        map.Add("SHIP_NM", "SHIP_NM")
        map.Add("DESTINATION", "DESTINATION")
        map.Add("BOOKING_NO", "BOOKING_NO")
        map.Add("VOYAGE_NO", "VOYAGE_NO")
        map.Add("SHIPPER_CD", "SHIPPER_CD")
        map.Add("SHIPPER_NM", "SHIPPER_NM")
        map.Add("CONT_LOADING_DATE", "CONT_LOADING_DATE")
        map.Add("STORAGE_TEST_DATE", "STORAGE_TEST_DATE")
        map.Add("STORAGE_TEST_TIME", "STORAGE_TEST_TIME")
        map.Add("DEPARTURE_DATE", "DEPARTURE_DATE")
        map.Add("CONTAINER_NO", "CONTAINER_NO")
        map.Add("CONTAINER_NM", "CONTAINER_NM")
        map.Add("CONTAINER_SIZE", "CONTAINER_SIZE")
        map.Add("CONTAINER_SIZE_NM", "CONTAINER_SIZE_NM")
        '輸出情報追加 END
        map.Add("UN", "UN")
        map.Add("OUTKA_ATT", "OUTKA_ATT")

        '2016.01.08 シンガポール対応
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC812OUT")
        'ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC520OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            'whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(" AND OUTL.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.CHAR))

            '入荷管理番号
            'whereStr = .Item("OUTKA_NO_L").ToString()
            Me._StrSql.Append(" AND OUTL.OUTKA_NO_L = @OUTKA_NO_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L"), DBDataType.CHAR))

            'ユーザID
            'whereStr = .Item("USER_CD").ToString()
            If IsDBNull(.Item("USER_CD")) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", .Item("USER_CD"), DBDataType.CHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", "", DBDataType.CHAR))
            End If

            '再発行フラグ
            ''whereStr = .Item("SAIHAKKO_FLG").ToString()
            ''Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))
            If IsDBNull(.Item("SAIHAKKO_FLG")) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", .Item("SAIHAKKO_FLG"), DBDataType.CHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", "", DBDataType.CHAR))
            End If



        End With

    End Sub

    ''' <summary>
    '''  LMC520INパラメータ設定
    ''' </summary>
    ''' <remarks>荷主明細マスタ存在抽出用SQLの構築</remarks>
    Private Sub setIndataParameter(ByVal _Row As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", _Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", _Row("OUTKA_NO_L"), DBDataType.CHAR))

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

    ''' <summary>
    ''' 設定値名称設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetNaiyoNm(ByVal sql As String, ByVal setNaiyo As String) As String

        sql = sql.Replace("$SET_NAIYO$", setNaiyo)

        Return sql

    End Function

#End Region

#End Region

#End Region

End Class
