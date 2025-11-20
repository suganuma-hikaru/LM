' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC521    : 出荷指示書印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC521DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC521DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "検索処理 SQL"




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



    ''' <summary>
    ''' データ抽出用FROM句 1
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FIRST As String = "SELECT                                                                                           " & vbNewLine _
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
                                               & " --(2014.12.26) LMC758,759     --  START  --                                                     " & vbNewLine _
                                               & ",MAIN.CUST_AD_1           AS CUST_AD_1                                                           " & vbNewLine _
                                               & ",MAIN.CUST_AD_2           AS CUST_AD_2                                                           " & vbNewLine _
                                               & ",MAIN.CUST_AD_3           AS CUST_AD_3                                                           " & vbNewLine _
                                               & ",MAIN.CUST_TEL            AS CUST_TEL                                                            " & vbNewLine _
                                               & ",MAIN.CUST_FAX            AS CUST_FAX                                                            " & vbNewLine _
                                               & ",MAIN.CUST_ZIP            AS CUST_ZIP                                                            " & vbNewLine _
                                               & ",MAIN.DEST_ZIP            AS DEST_ZIP                                                            " & vbNewLine _
                                               & ",MAIN.NRS_BR_NM           AS NRS_BR_NM                                                           " & vbNewLine _
                                               & ",MAIN.NRS_BR_TEL          AS NRS_BR_TEL                                                          " & vbNewLine _
                                               & ",MAIN.NRS_BR_FAX          AS NRS_BR_FAX                                                          " & vbNewLine _
                                               & ",MAIN.NRS_BR_AD_1         AS NRS_BR_AD_1                                                         " & vbNewLine _
                                               & ",MAIN.NRS_BR_AD_2         AS NRS_BR_AD_2                                                         " & vbNewLine _
                                               & ",MAIN.NRS_BR_AD_3         AS NRS_BR_AD_3                                                         " & vbNewLine _
                                               & ",MAIN.CUST_USER           AS CUST_USER                                                           " & vbNewLine _
                                               & ",MAIN.UNSO_PKG_NB         AS UNSO_PKG_NB                                                         " & vbNewLine _
                                               & ",MAIN.SAP_CREATE_DATE     AS SAP_CREATE_DATE                                                     " & vbNewLine _
                                               & ",MAIN.DEST_DETAIL         AS DEST_DETAIL                                                         " & vbNewLine _
                                               & ",MAIN.SHIP_CD             AS SHIP_CD                                                             " & vbNewLine _
                                               & ",MAIN.NRS_BR_ZIP          AS NRS_BR_ZIP                                                          " & vbNewLine _
                                               & ",MAIN.GOODS_NM_CUST       AS GOODS_NM_CUST                                                       " & vbNewLine _
                                               & ",MAIN.DELI_ATT            AS DELI_ATT                                                            " & vbNewLine _
                                               & "--(2014.12.26) LMC758,759     --  END  --                                                        " & vbNewLine _ 
                                               & "FROM                                                                                             " & vbNewLine _
                                               & "(SELECT                                                                                          " & vbNewLine _
                                               & " CASE WHEN MRC.COMB_RPT_ID IS NOT NULL THEN MRC.COMB_RPT_ID                                                                   " & vbNewLine _
                                               & "      WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                    " & vbNewLine _
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
                                               & " --(2014.12.26) LMC578,579 -- START --                                                                               " & vbNewLine _
                                               & " ,MC.AD_1                AS CUST_AD_1                                                                                " & vbNewLine _
                                               & " ,MC.AD_2                AS CUST_AD_2                                                                                " & vbNewLine _
                                               & " ,MC.AD_3                AS CUST_AD_3                                                                                " & vbNewLine _
                                               & " ,MC.TEL                 AS CUST_TEL                                                                                 " & vbNewLine _
                                               & " ,MC.ZIP                  AS CUST_ZIP                                                                                " & vbNewLine _
                                               & ",MC.FAX                  AS CUST_FAX                                                                                 " & vbNewLine _
                                               & ",CASE WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_ZIP                                                                    " & vbNewLine _
                                               & "         ELSE MDOUT.ZIP                                                                                              " & vbNewLine _
                                               & "  END             AS DEST_ZIP                                                                                        " & vbNewLine _
                                               & " ,NRS_BR.NRS_BR_NM        AS NRS_BR_NM                                                                               " & vbNewLine _
                                               & " ,NRS_BR.TEL              AS NRS_BR_TEL                                                                              " & vbNewLine _
                                               & " ,NRS_BR.FAX              AS NRS_BR_FAX                                                                              " & vbNewLine _
                                               & " ,NRS_BR.AD_1             AS NRS_BR_AD_1                                                                             " & vbNewLine _
                                               & " ,NRS_BR.AD_2             AS NRS_BR_AD_2                                                                             " & vbNewLine _
                                               & " ,NRS_BR.AD_3             AS NRS_BR_AD_3                                                                             " & vbNewLine _
                                               & " ,KBN5.KBN_NM2 + KBN5.KBN_NM3 AS CUST_USER                                                                           " & vbNewLine _
                                               & " ,UL.UNSO_PKG_NB          AS UNSO_PKG_NB                                                                             " & vbNewLine _
                                               & " ,EDIL.FREE_C02           AS SAP_CREATE_DATE                                                                         " & vbNewLine _
                                               & " ,CASE WHEN WEBH.EDIT_REMARK = '' OR WEBH.EDIT_REMARK IS NULL  					               " & vbNewLine _
                                               & "                   THEN MDD2.SET_NAIYO ELSE WEBH.EDIT_REMARK END           AS DEST_DETAIL                            " & vbNewLine _                                         
                                               & " ,OUTL.SHIP_CD_L          AS SHIP_CD                                                                                 " & vbNewLine _                                         
                                               & " ,NRS_BR.ZIP              AS NRS_BR_ZIP                                                                              " & vbNewLine _                                         
                                               & " ,MGD3.REMARK             AS GOODS_NM_CUST                                                                           " & vbNewLine _                                         
                                               & " ,CASE WHEN WEBH.EDIT_NHS_REMARK = '' OR WEBH.EDIT_NHS_REMARK IS NULL  					       " & vbNewLine _
					       & "                   THEN MDOUT.DELI_ATT ELSE WEBH.EDIT_NHS_REMARK END AS DELI_ATT 				       " & vbNewLine _
                                          & "--出荷L                                                                 " & vbNewLine _
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
                                     & "--Webヘッダー                                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..WK_WEB_ORDER_HED WEBH                                   " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = WEBH.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND OUTL.OUTKA_NO_L = WEBH.OUTKA_NO_L                                         " & vbNewLine _
                                     & "AND WEBH.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
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
                                     & "--  2014/12/26 LMC758,759対応追加 拠点M 区分 送付先詳細                      " & vbNewLine _
                                     & "LEFT JOIN                                                                    " & vbNewLine _
                                     & "      $LM_MST$..M_NRS_BR NRS_BR                                                " & vbNewLine _
                                     & "ON OUTL.NRS_BR_CD = NRS_BR.NRS_BR_CD                                         " & vbNewLine _
                                     & "AND NRS_BR.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                     & "LEFT JOIN                                                                    " & vbNewLine _
                                     & "      $LM_MST$..Z_KBN KBN5                                                     " & vbNewLine _
                                     & "ON EDIL.FREE_C01 = KBN5.KBN_NM1                                              " & vbNewLine _
                                     & "AND KBN5.KBN_GROUP_CD = 'A006'                                               " & vbNewLine _
                                     & "AND KBN5.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "LEFT JOIN (SELECT NRS_BR_CD,CUST_CD_L,DEST_CD, MAX(DEST_CD_EDA) as DEST_CD_EDA  ,SET_NAIYO        " & vbNewLine _
                                     & " FROM  $LM_MST$..M_DEST_DETAILS                                               " & vbNewLine _
                                     & "WHERE    SUB_KB = '07'                                                      " & vbNewLine _
                                     & "AND SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                                     & "GROUP BY   NRS_BR_CD,CUST_CD_L,DEST_CD     ,SET_NAIYO                       " & vbNewLine _
                                     & ") MDD2                                                                       " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MDD2.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MDD2.CUST_CD_L                                          " & vbNewLine _
                                     & "AND OUTL.DEST_CD = MDD2.DEST_CD                                              " & vbNewLine _
                                     & " --商品M LMC758                                                                " & vbNewLine _
                                     & "	LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD3                                                    " & vbNewLine _
                                     & "	ON  OUTM.NRS_BR_CD = MGD3.NRS_BR_CD                                              " & vbNewLine _
                                     & "	AND OUTM.GOODS_CD_NRS = MGD3.GOODS_CD_NRS                                              " & vbNewLine _
                                     & "	AND OUTL.SHIP_CD_L = MGD3.SET_NAIYO                                              " & vbNewLine _
                                     & " AND MGD3.SUB_KB ='43' AND MGD3.SYS_DEL_FLG ='0'                                         " & vbNewLine _
                                     & "--  2014/12/26 LMC758,759対応追加 拠点M 区分 送付先詳細                     " & vbNewLine _
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
                                     & "--  2014/12/26 LMC758,759対応追加  帳票コンビマスタ                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT_COMB MRC                                                " & vbNewLine _
                                     & "ON  MRC.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MRC.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
                                     & "AND MRC.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
                                     & "AND MRC.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--  2014/12/26 LMC758,759対応追加 拠点M 区分 送付先詳細                     " & vbNewLine _
                                     & "WHERE                                                                        " & vbNewLine _
                                     & "OUTL.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                    
    ''' <summary>
    ''' データ抽出用FROM句 2
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SECOND As String =  ") MAIN                                        " & vbNewLine _
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
                                         & " --(2012.11.13) LMC537対応 -- STRAT --            " & vbNewLine _
                                         & " ---20141226    LMC758,759  --                    " & vbNewLine _
                                         & "  ,MAIN.CUST_AD_1                                 " & vbNewLine _
                                         & "  ,MAIN.CUST_AD_2                                 " & vbNewLine _
                                         & "  ,MAIN.CUST_AD_3                                 " & vbNewLine _
                                         & "  ,MAIN.CUST_TEL                                   " & vbNewLine _
                                         & "  ,MAIN.CUST_ZIP                                   " & vbNewLine _
                                         & "  ,MAIN.CUST_FAX                                   " & vbNewLine _
                                         & "  ,MAIN.DEST_ZIP                                   " & vbNewLine _
                                         & " ,MAIN.NRS_BR_NM                                   " & vbNewLine _
                                         & " ,MAIN.NRS_BR_TEL                                  " & vbNewLine _
                                         & " ,MAIN.NRS_BR_FAX                                  " & vbNewLine _
                                         & " ,MAIN.NRS_BR_AD_1                                 " & vbNewLine _
                                         & " ,MAIN.NRS_BR_AD_2                                 " & vbNewLine _
                                         & " ,MAIN.NRS_BR_AD_3                                 " & vbNewLine _
                                         & " ,MAIN.CUST_USER                                   " & vbNewLine _
                                         & " ,MAIN.UNSO_PKG_NB                                 " & vbNewLine _
                                         & " ,MAIN.SAP_CREATE_DATE                             " & vbNewLine _
                                         & " ,MAIN.DEST_DETAIL                                 " & vbNewLine _
                                         & " ,MAIN.SHIP_CD                                     " & vbNewLine _
                                         & " ,MAIN.NRS_BR_ZIP                                  " & vbNewLine _
                                         & " ,MAIN.GOODS_NM_CUST                               " & vbNewLine _
                                         & " ,MAIN.DELI_ATT                                    " & vbNewLine
    ''' <summary>
    ''' ORDER BY（①営業所コード、②管理番号L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY1 As String = "ORDER BY                                          " & vbNewLine _
                                         & "     MAIN.NRS_BR_CD                                " & vbNewLine _
                                         & "    ,MAIN.OUTKA_NO_L                               " & vbNewLine

    ''' <summary>
    ''' ORDER BY（⑥管理番号S）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY3 As String = "   ,MAIN.OUTKA_NO_S                               " & vbNewLine

    'ADD 2019/04/08　Start 依頼番号 : 004890   【LMS】群馬_A4納品書の修正_出荷指図書別パターン作成、出荷指図書・納品書に備考と商品注意事項
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_GUNMA As String = "SELECT                                                                                           " & vbNewLine _
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
                                               & ",MAIN.SHOBO_CD            AS SHOBO_CD                                                            " & vbNewLine _
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
                                               & ",RTRIM(OUTS.ZONE_CD)      AS ZONE_CD                                                                                 " & vbNewLine _
                                               & ",OUTS.LOCA         AS LOCA                                                                                           " & vbNewLine _
                                               & ",OUTM.REMARK       AS REMARK_M                                                                                       " & vbNewLine _
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
                                               & " ,MG.SHOBO_CD             AS SHOBO_CD                                                              "

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ADD_SAGYO As String _
        = " , ISNULL(SAGYO_L_1.SAGYO_REC_NO, '') AS SAGYO_REC_NO_1                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_1.SAGYO_CD, '')     AS SAGYO_CD_1                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_1.SAGYO_NM, '')	 AS SAGYO_NM_1                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_2.SAGYO_REC_NO, '') AS SAGYO_REC_NO_2                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_2.SAGYO_CD, '')     AS SAGYO_CD_2                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_2.SAGYO_NM, '')	 AS SAGYO_NM_2                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_3.SAGYO_REC_NO, '') AS SAGYO_REC_NO_3                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_3.SAGYO_CD, '')     AS SAGYO_CD_3                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_3.SAGYO_NM, '')	 AS SAGYO_NM_3                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_4.SAGYO_REC_NO, '') AS SAGYO_REC_NO_4                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_4.SAGYO_CD, '')     AS SAGYO_CD_4                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_4.SAGYO_NM, '')	 AS SAGYO_NM_4                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_5.SAGYO_REC_NO, '') AS SAGYO_REC_NO_5                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_5.SAGYO_CD, '')     AS SAGYO_CD_5                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_5.SAGYO_NM, '')	 AS SAGYO_NM_5                       " & vbNewLine _
        & " , ISNULL(SAGYO_M_1.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_1               " & vbNewLine _
        & " , ISNULL(SAGYO_M_1.SAGYO_CD, '')     AS SAGYO_MEI_CD_1                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_1.SAGYO_NM, '')	 AS SAGYO_MEI_NM_1                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_2.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_2               " & vbNewLine _
        & " , ISNULL(SAGYO_M_2.SAGYO_CD, '')     AS SAGYO_MEI_CD_2                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_2.SAGYO_NM, '')	 AS SAGYO_MEI_NM_2                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_3.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_3               " & vbNewLine _
        & " , ISNULL(SAGYO_M_3.SAGYO_CD, '')     AS SAGYO_MEI_CD_3                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_3.SAGYO_NM, '')	 AS SAGYO_MEI_NM_3                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_4.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_4               " & vbNewLine _
        & " , ISNULL(SAGYO_M_4.SAGYO_CD, '')     AS SAGYO_MEI_CD_4                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_4.SAGYO_NM, '')	 AS SAGYO_MEI_NM_4                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_5.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_5               " & vbNewLine _
        & " , ISNULL(SAGYO_M_5.SAGYO_CD, '')     AS SAGYO_MEI_CD_5                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_5.SAGYO_NM, '')	 AS SAGYO_MEI_NM_5                   " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_GUNMA As String = "--出荷L                                                                 " & vbNewLine _
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
                                     & "AND KBN3.KBN_CD = ZAI.GOODS_COND_KB_1                                        " & vbNewLine _
                                     & "AND KBN3.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(商品状態区分(外観))                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                               " & vbNewLine _
                                     & "ON  KBN4.KBN_GROUP_CD = 'S006'                                               " & vbNewLine _
                                     & "AND KBN4.KBN_CD = ZAI.GOODS_COND_KB_2                                        " & vbNewLine _
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
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                    " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_ADD_SAGYO As String _
        = "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 1) AS SAGYO_L_1                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_1.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_1.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 2) AS SAGYO_L_2                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_2.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_2.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 3) AS SAGYO_L_3                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_3.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_3.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 4) AS SAGYO_L_4                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_4.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_4.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 5) AS SAGYO_L_5                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_5.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_5.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                      AND CM.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 1) AS SAGYO_M_1                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_1.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_1.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_1.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                      AND CM.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 2) AS SAGYO_M_2                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_2.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_2.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_2.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                      AND CM.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 3) AS SAGYO_M_3                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_3.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_3.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_3.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                      AND CM.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 4) AS SAGYO_M_4                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_4.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_4.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_4.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                      AND CM.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.SYS_DEL_FLG  = '0'                                                              " & vbNewLine _
        & "                      AND CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 5) AS SAGYO_M_5                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_5.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_5.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_5.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String _
        = " WHERE                                                                                                       " & vbNewLine _
        & "       OUTL.SYS_DEL_FLG = '0'                                                                                " & vbNewLine


    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_GUNMA As String = ") MAIN                                       " & vbNewLine _
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
                                         & " --(2012.11.13) LMC537対応 -- STRAT --            " & vbNewLine _
                                         & " ,MAIN.SHOBO_CD                                   " & vbNewLine
    'ADD 2019/04/08　End 依頼番号 : 004890   【LMS】群馬_A4納品書の修正_出荷指図書別パターン作成、出荷指図書・納品書に備考と商品注意事項


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
                Me._StrSql.Append(LMC521DAC.SQL_SELECT_MCUST_DETAILS)      'SQL構築(荷主明細マスタ設定値Select句)
                Call Me.setIndataParameter(Me._Row)                        '条件設定
            Case "1"
                'FREE_C03項目使用有無
                Me._StrSql.Append(LMC521DAC.SQL_SELECT_MCUST_DETAILS_FREE) 'SQL構築(荷主明細マスタ設定値Select句)
                Call Me.setIndataParameter(Me._Row)                        '条件設定

                '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- START --
            Case "2"
                'FREE_C03項目使用有無
                Me._StrSql.Append(LMC521DAC.SQL_SELECT_MCUST_DETAILS_CHKLIST) 'SQL構築(荷主明細マスタ設定値Select句)
                Call Me.setIndataParameter(Me._Row)                        '条件設定
                '    '(2012.06.08) Notes№1123 チェックリストは荷主明細マスタの値をセット -- END --
            Case Else

        End Select


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC521DAC", "SelectMCustDetailsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")

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

        'SQL作成



        '2015/1/23
        'SQLを一本化


        Me._StrSql.Append(LMC521DAC.SQL_SELECT_FIRST)   'SQL構築(データ抽出用SELECT句)
        Call Me.SetConditionMasterSQL()                 'SQL構築(条件設定)
        Me._StrSql.Append(LMC521DAC.SQL_SELECT_SECOND)   'SQL構築(データ抽出用SELECT句)
        'WHERE条件は中に記載


        If String.IsNullOrEmpty(setNaiyo) = True Then
            Me._StrSql.Append(LMC521DAC.SQL_ORDER_BY1)         'SQL構築(データ抽出用ORDER BY句)
        Else
            Me._StrSql.Append(LMC521DAC.SQL_ORDER_BY1)         'SQL構築(データ抽出用ORDER BY句)
            Me._StrSql.Append(",MAIN.$SET_NAIYO$")

        End If

        '要望番号:1802（出荷指示書　ソート順にOUTKA_NO_Sも含める) 2013/01/29 本明 Start
        Me._StrSql.Append(LMC521DAC.SQL_ORDER_BY3)         'SQL構築(データ抽出用ORDER BY句)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '2次対応 荷姿並び替え 2012.01.17 START
        '(並び替え)設定値名称設定
        If String.IsNullOrEmpty(setNaiyo) = False Then
            sql = Me.SetNaiyoNm(sql, setNaiyo)
        End If
        '2次対応 荷姿並び替え 2012.01.17 END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC521DAC", "SelectPrintData", cmd)

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
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("CUST_NM_S_H", "CUST_NM_S_H")
        map.Add("RPT_FLG", "RPT_FLG")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_NAIYO_1", "CUST_NAIYO_1")
        map.Add("CUST_NAIYO_2", "CUST_NAIYO_2")
        map.Add("CUST_NAIYO_3", "CUST_NAIYO_3")
        map.Add("DEST_REMARK", "DEST_REMARK")
        map.Add("DEST_SALES_CD", "DEST_SALES_CD")
        map.Add("DEST_SALES_NM_L", "DEST_SALES_NM_L")
        map.Add("DEST_SALES_NM_M", "DEST_SALES_NM_M")
        map.Add("ALCTD_NB_HEADKEI", "ALCTD_NB_HEADKEI")
        map.Add("ALCTD_QT_HEADKEI", "ALCTD_QT_HEADKEI")
        map.Add("HINMEI", "HINMEI")
        map.Add("NISUGATA", "NISUGATA")

        map.Add("CUST_AD_1", "CUST_AD_1")
        map.Add("CUST_AD_2", "CUST_AD_2")
        map.Add("CUST_AD_3", "CUST_AD_3")
        map.Add("CUST_TEL", "CUST_TEL")
        map.Add("SHIP_CD", "SHIP_CD") '
        map.Add("CUST_ZIP", "CUST_ZIP")
        map.Add("CUST_FAX", "CUST_FAX")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("CUST_USER", "CUST_USER")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("NRS_BR_TEL", "NRS_BR_TEL")
        map.Add("NRS_BR_FAX", "NRS_BR_FAX")
        map.Add("SAP_CREATE_DATE", "SAP_CREATE_DATE")
        map.Add("DEST_DETAIL", "DEST_DETAIL")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("NRS_BR_AD_1", "NRS_BR_AD_1")
        map.Add("NRS_BR_AD_2", "NRS_BR_AD_2")
        map.Add("NRS_BR_AD_3", "NRS_BR_AD_3")
        map.Add("NRS_BR_ZIP", "NRS_BR_ZIP")
        map.Add("GOODS_NM_CUST", "GOODS_NM_CUST")
        map.Add("DELI_ATT", "DELI_ATT")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC520OUT")


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

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(" AND OUTL.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '入荷管理番号
            whereStr = .Item("OUTKA_NO_L").ToString()
            Me._StrSql.Append(" AND OUTL.OUTKA_NO_L = @OUTKA_NO_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", whereStr, DBDataType.CHAR))

            'ユーザID
            whereStr = .Item("USER_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))

            '再発行フラグ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", .Item("SAIHAKKO_FLG").ToString(), DBDataType.CHAR))

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
