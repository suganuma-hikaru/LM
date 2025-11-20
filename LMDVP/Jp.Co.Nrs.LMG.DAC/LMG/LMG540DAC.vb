' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求
'  プログラムID     :  LMG540    : デュポン運賃請求明細書
'  作  成  者       :  [篠原]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG540DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG540DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQ"

    ''' <summary>
    ''' 出力対象帳票パターン取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                          " & vbNewLine _
                                            & "  @NRS_BR_CD     AS NRS_BR_CD                                            " & vbNewLine _
                                            & ", MAIN2.PTN_ID   AS PTN_ID                                               " & vbNewLine _
                                            & ", MAIN2.PTN_CD   AS PTN_CD                                               " & vbNewLine _
                                            & ", MAIN2.RPT_ID   AS RPT_ID                                               " & vbNewLine _
                                            & "FROM (                                                                   " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(MAIN2)★
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAIN2 As String = "SELECT                             " & vbNewLine _
                                            & " --篠原追記                        " & vbNewLine _
                                            & " -- MAIN2.NRS_BR_CD                " & vbNewLine _
                                            & "  MAIN2.PTN_ID                     " & vbNewLine _
                                            & " ,MAIN2.PTN_CD                     " & vbNewLine _
                                            & " --篠原追記終わり                  " & vbNewLine _
                                            & " ,MAIN2.RPT_ID                     " & vbNewLine _
                                            & " ,MAIN2.OFB_KB                     " & vbNewLine _
                                            & " ,MAIN2.DEPART                     " & vbNewLine _
                                            & " ,MAIN2.CUST_CD_L                  " & vbNewLine _
                                            & " ,MAIN2.CUST_CD_M                  " & vbNewLine _
                                            & " ,MAIN2.CUST_CD_S                  " & vbNewLine _
                                            & " ,MAIN2.CUST_CD_SS                 " & vbNewLine _
                                            & " ,MAIN2.CUST_NM_L                  " & vbNewLine _
                                            & " ,MAIN2.CUST_NM_M                  " & vbNewLine _
                                            & " ,MAIN2.CUST_NM_S                  " & vbNewLine _
                                            & " ,MAIN2.CUST_NM_SS                 " & vbNewLine _
                                            & " ,MAIN2.SRC_CD                     " & vbNewLine _
                                            & " ,MAIN2.FRB_CD                     " & vbNewLine _
                                            & " ,MAIN2.OUTKA_PLAN_DATE            " & vbNewLine _
                                            & " ,MAIN2.MOS_NO                     " & vbNewLine _
                                            & " ,MAIN2.DEST_CD                    " & vbNewLine _
                                            & " ,MAIN2.DEST_NM                    " & vbNewLine _
                                            & " ,MAIN2.GOODS_CD_CUST              " & vbNewLine _
                                            & " ,MAIN2.GOODS_NM                   " & vbNewLine _
                                            & " ,MAIN2.DECI_NG_NB                 " & vbNewLine _
                                            & " ,MAIN2.DECI_WT                    " & vbNewLine _
                                            & " ,MAIN2.DECI_UNCHIN                " & vbNewLine _
                                            & " ,MAIN2.SEIQ_TARIFF_CD             " & vbNewLine _
                                            & " ,MAIN2.TARIFF_DISPNM              " & vbNewLine _
                                            & " ,MAIN2.SHI                        " & vbNewLine _
                                            & " ,MAIN2.SEIQ_KYORI                 " & vbNewLine _
                                            & " ,MAIN2.SEIQ_PKG_UT                " & vbNewLine _
                                            & " ,MAIN2.UNSO_NO_L                  " & vbNewLine _
                                            & "--篠原追記                         " & vbNewLine _
                                            & ",MAIN2.MOTO_DATA_KB                " & vbNewLine _
                                            & ",MAIN2.NRS_BR_CD                   " & vbNewLine _
                                            & ",MAIN2.INOUTKA_CTL_NO              " & vbNewLine _
                                            & ",MAIN2.DEST_KB                     " & vbNewLine _
                                            & ",MAIN2.CUST_ORD_NO                 " & vbNewLine _
                                            & ",MAIN2.GOODS_CD_NRS                " & vbNewLine _
                                            & ",MAIN2.SEIQ_NG_NB                  " & vbNewLine _
                                            & ",MAIN2.SEIQ_WT                     " & vbNewLine _
                                            & ",MAIN2.DECI_CITY_EXTC              " & vbNewLine _
                                            & ",MAIN2.DECI_WINT_EXTC              " & vbNewLine _
                                            & ",MAIN2.DECI_RELY_EXTC              " & vbNewLine _
                                            & ",MAIN2.DECI_TOLL                   " & vbNewLine _
                                            & ",MAIN2.DECI_INSU                   " & vbNewLine _
                                            & "FROM                               " & vbNewLine _
                                            & "(                                  " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM(MAIN2)★
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_FROM_MAIN2 As String = "  SELECT                                                     " & vbNewLine _
                                             & "    CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD       " & vbNewLine _
                                             & "   	     ELSE MR2.PTN_CD END       AS PTN_CD               " & vbNewLine _
                                             & "   ,CASE WHEN @PRT_TYPE = '01' THEN '71'                   " & vbNewLine _
                                             & "   	     WHEN @PRT_TYPE = '02' THEN '72' END AS PTN_ID     " & vbNewLine _
                                             & "   ,CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID       " & vbNewLine _
                                             & "   	     ELSE MR2.RPT_ID END       AS RPT_ID               " & vbNewLine _
                                             & "   ,CASE WHEN MCD1.SET_NAIYO = '01' THEN '02'              " & vbNewLine _
                                             & "   	     WHEN BASE.OFB_KB 	 = '02' THEN '02'              " & vbNewLine _
                                             & "   		ELSE '01' END AS OFB_KB                            " & vbNewLine _
                                             & "   ,MCD2.SET_NAIYO AS DEPART                               " & vbNewLine _
                                             & "   ,BASE.CUST_CD_L                                         " & vbNewLine _
                                             & "   ,BASE.CUST_CD_M                                         " & vbNewLine _
                                             & "   ,BASE.CUST_CD_S                                         " & vbNewLine _
                                             & "   ,BASE.CUST_CD_SS                                        " & vbNewLine _
                                             & "   ,MC.CUST_NM_L                                           " & vbNewLine _
                                             & "   ,MC.CUST_NM_M                                           " & vbNewLine _
                                             & "   ,MC.CUST_NM_S                                           " & vbNewLine _
                                             & "   ,CASE                                                   " & vbNewLine _
                                             & "   	WHEN MCD1.SET_NAIYO = '01' OR BASE.OFB_KB = '02' THEN '簿外'                                  " & vbNewLine _
                                             & "   	ELSE MC.CUST_NM_SS END AS CUST_NM_SS                   " & vbNewLine _
                                             & "   ,MG.CUST_COST_CD2 AS SRC_CD                             " & vbNewLine _
                                             & "   ,CASE WHEN MCD2.SET_NAIYO = 'B' AND                     " & vbNewLine _
                                             & "   					 MG.CUST_COST_CD1 = '9988' THEN '9941' " & vbNewLine _
                                             & "         ELSE MG.CUST_COST_CD1 END AS FRB_CD               " & vbNewLine _
                                             & "   ,BASE.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE                " & vbNewLine _
                                             & "   ,BASE.CUST_ORD_NO AS MOS_NO                             " & vbNewLine _
                                             & "   ,BASE.DEST_CD                                           " & vbNewLine _
                                             & "   ,CASE WHEN BASE.DEST_KB IN ('00','99') THEN MD.DEST_NM  " & vbNewLine _
                                             & "         ELSE  BASE.DEST_NM END AS DEST_NM                 " & vbNewLine _
                                             & "   ,MG.GOODS_CD_CUST AS GOODS_CD_CUST                      " & vbNewLine _
                                             & "   ,CASE WHEN BASE.GOODS_NM <> '' THEN BASE.GOODS_NM       " & vbNewLine _
                                             & "         ELSE MG.GOODS_NM_1 END AS GOODS_NM                " & vbNewLine _
                                             & "   ,BASE.SEIQ_NG_NB AS DECI_NG_NB                          " & vbNewLine _
                                             & "   --(2012.09.28)要望番号1305 -- START --                  " & vbNewLine _
                                             & "   --,BASE.SEIQ_WT AS DECI_WT                              " & vbNewLine _
                                             & "   ,BASE.DECI_WT AS DECI_WT                                " & vbNewLine _
                                             & "   --(2012.09.28)要望番号1305 --  END  --                  " & vbNewLine _
                                             & "   ,BASE.DECI_UNCHIN                                       " & vbNewLine _
                                             & "   		 + BASE.DECI_CITY_EXTC                             " & vbNewLine _
                                             & "   		 + BASE.DECI_WINT_EXTC                             " & vbNewLine _
                                             & "   		 + BASE.DECI_RELY_EXTC                             " & vbNewLine _
                                             & "   		 + BASE.DECI_TOLL                                  " & vbNewLine _
                                             & "   		 + BASE.DECI_INSU AS DECI_UNCHIN                   " & vbNewLine _
                                             & "   ,BASE.SEIQ_TARIFF_CD AS SEIQ_TARIFF_CD                  " & vbNewLine _
                                             & "   ,(SELECT KB.KBN_NM2 FROM $LM_MST$..Z_KBN KB               " & vbNewLine _
                                             & "   	WHERE KB.KBN_GROUP_CD = 'D010' AND                     " & vbNewLine _
                                             & "   				KB.KBN_NM1 = BASE.SEIQ_TARIFF_CD)          " & vbNewLine _
                                             & "   	 			AS TARIFF_DISPNM                           " & vbNewLine _
                                             & "   ,MJ.SHI AS SHI                                          " & vbNewLine _
                                             & "-- ,BASE.SEIQ_KYORI  --Notes1413--                         " & vbNewLine _
                                             & "   ,BASE.DECI_KYORI AS SEIQ_KYORI  --Notes1413--           " & vbNewLine _
                                             & "   ,BASE.SEIQ_PKG_UT                                       " & vbNewLine _
                                             & "   ,BASE.UNSO_NO_L                                         " & vbNewLine _
                                             & "   --篠原追記                                              " & vbNewLine _
                                             & "   ,BASE.MOTO_DATA_KB	                                   " & vbNewLine _
                                             & "   ,BASE.NRS_BR_CD	                                       " & vbNewLine _
                                             & "   ,BASE.INOUTKA_CTL_NO	                                   " & vbNewLine _
                                             & "   ,BASE.DEST_KB	                                       " & vbNewLine _
                                             & "   ,BASE.CUST_ORD_NO	                                   " & vbNewLine _
                                             & "   ,BASE.GOODS_CD_NRS	                                   " & vbNewLine _
                                             & "   ,BASE.SEIQ_NG_NB	                                       " & vbNewLine _
                                             & "   ,BASE.SEIQ_WT	                                       " & vbNewLine _
                                             & "   ,BASE.DECI_CITY_EXTC	                                   " & vbNewLine _
                                             & "   ,BASE.DECI_WINT_EXTC	                                   " & vbNewLine _
                                             & "   ,BASE.DECI_RELY_EXTC	                                   " & vbNewLine _
                                             & "   ,BASE.DECI_TOLL	                                       " & vbNewLine _
                                             & "   ,BASE.DECI_INSU	                                       " & vbNewLine _
                                             & "   FROM                                                    " & vbNewLine _
                                             & "   (                                                       " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT(出荷)★
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Const SQL_SELECT_BASE_OUT As String = "   SELECT                              " & vbNewLine _
                                             & "   RANK() OVER (PARTITION BY OL.OUTKA_NO_L ORDER BY OS.OUTKA_NO_M + OS.OUTKA_NO_S ) AS NO " & vbNewLine _
                                             & "   ,UL.MOTO_DATA_KB AS MOTO_DATA_KB  " & vbNewLine _
                                             & "   ,OL.NRS_BR_CD AS NRS_BR_CD        " & vbNewLine _
                                             & "   ,OL.OUTKA_NO_L AS INOUTKA_CTL_NO  " & vbNewLine _
                                             & "   ,UT.UNSO_NO_L                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_L                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_M                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_S                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_SS                    " & vbNewLine _
                                             & "   ,OL.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE   " & vbNewLine _
                                             & "   ,OL.DEST_CD                       " & vbNewLine _
                                             & "   ,OL.DEST_KB                       " & vbNewLine _
                                             & "   ,OL.DEST_NM                       " & vbNewLine _
                                             & "   ,OL.CUST_ORD_NO AS CUST_ORD_NO    " & vbNewLine _
                                             & "   ,ZAI.OFB_KB AS OFB_KB             " & vbNewLine _
                                             & "   ,UT.SEIQ_TARIFF_CD                " & vbNewLine _
                                             & "   ,OM.GOODS_CD_NRS                  " & vbNewLine _
                                             & "   ,'' AS GOODS_NM                   " & vbNewLine _
                                             & "   ,UT.SEIQ_PKG_UT                   " & vbNewLine _
                                             & "--   ,UT.SEIQ_NG_NB  --Notes1290     " & vbNewLine _
                                             & "   ,UT.DECI_NG_NB AS SEIQ_NG_NB   --Notes1290  " & vbNewLine _
                                             & "   ,UT.SEIQ_KYORI                    " & vbNewLine _
                                             & "   ,UT.SEIQ_WT                       " & vbNewLine _
                                             & "   ,UT.DECI_UNCHIN                   " & vbNewLine _
                                             & "   ,UT.DECI_CITY_EXTC                " & vbNewLine _
                                             & "   ,UT.DECI_WINT_EXTC                " & vbNewLine _
                                             & "   ,UT.DECI_RELY_EXTC                " & vbNewLine _
                                             & "   ,UT.DECI_TOLL                     " & vbNewLine _
                                             & "   ,UT.DECI_INSU                     " & vbNewLine _
                                             & "   ,MCD2.SET_NAIYO AS DEPART         " & vbNewLine _
                                             & "--(2012.09.28)要望番号1305 -- START -- " & vbNewLine _
                                             & "   ,UT.DECI_WT                         " & vbNewLine _
                                             & "--(2012.09.28)要望番号1305 --  END  -- " & vbNewLine _
                                             & "   ,UT.DECI_KYORI  --Notes1413--     " & vbNewLine _
                                             & "FROM                                 " & vbNewLine _
                                             & "$LM_TRN$..C_OUTKA_L OL                 " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..F_UNSO_L UL        " & vbNewLine _
                                             & "ON OL.NRS_BR_CD = UL.NRS_BR_CD       " & vbNewLine _
                                             & "AND OL.OUTKA_NO_L = UL.INOUTKA_NO_L  " & vbNewLine _
                                             & "AND UL.MOTO_DATA_KB = '20'           " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT    " & vbNewLine _
                                             & "ON UL.UNSO_NO_L = UT.UNSO_NO_L       " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_M OM       " & vbNewLine _
                                             & "ON OL.NRS_BR_CD = OM.NRS_BR_CD       " & vbNewLine _
                                             & "AND OL.OUTKA_NO_L = OM.OUTKA_NO_L    " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_S OS       " & vbNewLine _
                                             & "ON OL.NRS_BR_CD = OS.NRS_BR_CD       " & vbNewLine _
                                             & "AND OL.OUTKA_NO_L = OS.OUTKA_NO_L    " & vbNewLine _
                                             & "AND OM.OUTKA_NO_M = OS.OUTKA_NO_M    " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI      " & vbNewLine _
                                             & "ON OS.NRS_BR_CD = ZAI.NRS_BR_CD      " & vbNewLine _
                                             & "AND OS.ZAI_REC_NO = ZAI.ZAI_REC_NO   " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2 " & vbNewLine _
                                             & "ON MCD2.NRS_BR_CD = OL.NRS_BR_CD     " & vbNewLine _
                                             & "AND MCD2.CUST_CD =                   " & vbNewLine _
                                             & "(OL.CUST_CD_L +                      " & vbNewLine _
                                             & " UT.CUST_CD_M +                      " & vbNewLine _
                                             & " UT.CUST_CD_S +                      " & vbNewLine _
                                             & " UT.CUST_CD_SS)                      " & vbNewLine _
                                             & "AND MCD2.SUB_KB = '01'               " & vbNewLine _
                                             & "   where                             " & vbNewLine _
                                             & "   OL.SYS_DEL_FLG = '0'              " & vbNewLine _
                                             & "   AND OM.SYS_DEL_FLG = '0'          " & vbNewLine _
                                             & "   AND OS.SYS_DEL_FLG = '0'          " & vbNewLine _
                                             & "   AND ZAI.SYS_DEL_FLG = '0'         " & vbNewLine _
                                             & "   AND UL.SYS_DEL_FLG = '0'          " & vbNewLine _
                                             & "   AND UT.SYS_DEL_FLG = '0'          " & vbNewLine _
                                             & "   AND UT.DECI_UNCHIN <> 0           " & vbNewLine _
                                             & "   AND UT.SEIQ_FIXED_FLAG = '01'     " & vbNewLine _
                                             & "   AND OL.NRS_BR_CD  = @NRS_BR_CD         " & vbNewLine _
                                             & "   AND OL.CUST_CD_L  = @CUST_CD_L     " & vbNewLine _
                                             & "   AND OL.CUST_CD_M  = @CUST_CD_M     " & vbNewLine _
                                             & "   --AND OL.CUST_CD_S  = @CUST_CD_S     " & vbNewLine _
                                             & "   --AND OL.CUST_CD_SS = @CUST_CD_SS    " & vbNewLine _
                                             & "   AND OL.OUTKA_PLAN_DATE >= @F_DATE  " & vbNewLine _
                                             & "   AND OL.OUTKA_PLAN_DATE <= @T_DATE  " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT FROM WHERE (入荷)★
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_IN As String = "   --入荷                    " & vbNewLine _
                                             & "   UNION                      " & vbNewLine _
                                             & "   SELECT                     " & vbNewLine _
                                             & "   RANK() OVER (PARTITION BY INL.INKA_NO_L ORDER BY INS.INKA_NO_M + INS.INKA_NO_S) AS NO " & vbNewLine _
                                             & "   ,UL.MOTO_DATA_KB AS MOTO_DATA_KB                " & vbNewLine _
                                             & "   ,INL.NRS_BR_CD AS NRS_BR_CD                     " & vbNewLine _
                                             & "   ,INL.INKA_NO_L AS INOUTKA_CTL_NO                " & vbNewLine _
                                             & "   ,UT.UNSO_NO_L                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_L                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_M                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_S                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_SS                     " & vbNewLine _
                                             & "   ,INL.INKA_DATE AS OUTKA_PLAN_DATE  " & vbNewLine _
                                             & "   ,UL.ORIG_CD                        " & vbNewLine _
                                             & "   ,'99' AS DEST_KB                   " & vbNewLine _
                                             & "   ,'' AS DEST_NM                     " & vbNewLine _
                                             & "   ,INL.OUTKA_FROM_ORD_NO_L AS CUST_ORD_NO " & vbNewLine _
                                             & "   ,INS.OFB_KB AS OFB_KB                   " & vbNewLine _
                                             & "   ,UT.SEIQ_TARIFF_CD                      " & vbNewLine _
                                             & "   ,INM.GOODS_CD_NRS                       " & vbNewLine _
                                             & "   ,'' AS GOODS_NM                         " & vbNewLine _
                                             & "   ,UT.SEIQ_PKG_UT                         " & vbNewLine _
                                             & "--   ,UT.SEIQ_NG_NB    --Notes1290         " & vbNewLine _
                                             & "   ,UT.DECI_NG_NB AS SEIQ_NG_NB   --Notes1290  " & vbNewLine _
                                             & "   ,UT.SEIQ_KYORI                          " & vbNewLine _
                                             & "   ,UT.SEIQ_WT                             " & vbNewLine _
                                             & "   ,UT.DECI_UNCHIN                         " & vbNewLine _
                                             & "   ,UT.DECI_CITY_EXTC                      " & vbNewLine _
                                             & "   ,UT.DECI_WINT_EXTC                      " & vbNewLine _
                                             & "   ,UT.DECI_RELY_EXTC                      " & vbNewLine _
                                             & "   ,UT.DECI_TOLL                           " & vbNewLine _
                                             & "   ,UT.DECI_INSU                           " & vbNewLine _
                                             & "   ,MCD2.SET_NAIYO AS DEPART               " & vbNewLine _
                                             & "--(2012.09.28)要望番号1305 -- START --     " & vbNewLine _
                                             & "   ,UT.DECI_WT                             " & vbNewLine _
                                             & "--(2012.09.28)要望番号1305 --  END  --     " & vbNewLine _
                                             & "   ,UT.DECI_KYORI  --Notes1413--           " & vbNewLine _
                                             & "   FROM                                    " & vbNewLine _
                                             & "   $LM_TRN$..B_INKA_L INL                    " & vbNewLine _
                                             & "   LEFT JOIN $LM_TRN$..F_UNSO_L UL           " & vbNewLine _
                                             & "   ON INL.NRS_BR_CD = UL.NRS_BR_CD         " & vbNewLine _
                                             & "   AND INL.INKA_NO_L = UL.INOUTKA_NO_L     " & vbNewLine _
                                             & "   AND UL.MOTO_DATA_KB = '10'              " & vbNewLine _
                                             & "   LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT       " & vbNewLine _
                                             & "   ON UL.UNSO_NO_L = UT.UNSO_NO_L          " & vbNewLine _
                                             & "   LEFT JOIN $LM_TRN$..B_INKA_M INM          " & vbNewLine _
                                             & "   ON INL.NRS_BR_CD = INM.NRS_BR_CD        " & vbNewLine _
                                             & "   AND INL.INKA_NO_L = INM.INKA_NO_L       " & vbNewLine _
                                             & "   LEFT JOIN $LM_TRN$..B_INKA_S INS          " & vbNewLine _
                                             & "   ON INM.NRS_BR_CD = INS.NRS_BR_CD        " & vbNewLine _
                                             & "   AND INM.INKA_NO_L = INS.INKA_NO_L       " & vbNewLine _
                                             & "   AND INM.INKA_NO_M = INS.INKA_NO_M       " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2   " & vbNewLine _
                                             & "   ON MCD2.NRS_BR_CD = INL.NRS_BR_CD       " & vbNewLine _
                                             & "   AND MCD2.CUST_CD = (INL.CUST_CD_L +     " & vbNewLine _
                                             & "	    				UT.CUST_CD_M +     " & vbNewLine _
                                             & "						UT.CUST_CD_S +     " & vbNewLine _
                                             & "						UT.CUST_CD_SS)     " & vbNewLine _
                                             & "   AND MCD2.SUB_KB = '01'                  " & vbNewLine _
                                             & "   where                                   " & vbNewLine _
                                             & "   INL.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                             & "   AND INM.SYS_DEL_FLG = '0'               " & vbNewLine _
                                             & "   AND INS.SYS_DEL_FLG = '0'               " & vbNewLine _
                                             & "   AND UL.SYS_DEL_FLG = '0'                " & vbNewLine _
                                             & "   AND UT.SYS_DEL_FLG = '0'                " & vbNewLine _
                                             & "   AND UT.DECI_UNCHIN <> 0                 " & vbNewLine _
                                             & "   AND UT.SEIQ_FIXED_FLAG = '01'           " & vbNewLine _
                                             & "   AND INL.CUST_CD_M  = @CUST_CD_M         " & vbNewLine _
                                             & "   --AND INL.CUST_CD_S  = @CUST_CD_S         " & vbNewLine _
                                             & "   --AND INL.CUST_CD_SS = @CUST_CD_SS        " & vbNewLine _
                                             & "   AND INL.INKA_DATE >= @F_DATE            " & vbNewLine _
                                             & "   AND INL.INKA_DATE <= @T_DATE            " & vbNewLine _
                                             & "   AND INL.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                             & "   AND INL.CUST_CD_L = @CUST_CD_L          " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT FROM WHERE(運送)★
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNSO As String = "   --運送                                                        " & vbNewLine _
                                             & "   UNION                             " & vbNewLine _
                                             & "   SELECT                            " & vbNewLine _
                                             & "   RANK() OVER (PARTITION BY UL.UNSO_NO_L ORDER BY UM.UNSO_NO_M) AS NO" & vbNewLine _
                                             & "   ,UL.MOTO_DATA_KB AS MOTO_DATA_KB  " & vbNewLine _
                                             & "   ,UL.NRS_BR_CD AS NRS_BR_CD        " & vbNewLine _
                                             & "   ,'' AS INOUTKA_CTL_NO             " & vbNewLine _
                                             & "   ,UT.UNSO_NO_L                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_L                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_M                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_S                     " & vbNewLine _
                                             & "   ,UT.CUST_CD_SS                    " & vbNewLine _
                                             & "   ,UL.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE  " & vbNewLine _
                                             & "   ,UL.DEST_CD                       " & vbNewLine _
                                             & "   ,'99' AS DEST_KB                  " & vbNewLine _
                                             & "   ,'' AS DEST_NM                    " & vbNewLine _
                                             & "   ,UL.CUST_REF_NO AS CUST_ORD_NO    " & vbNewLine _
                                             & "   ,'01' AS OFB_KB                   " & vbNewLine _
                                             & "   ,UT.SEIQ_TARIFF_CD                " & vbNewLine _
                                             & "   ,UM.GOODS_CD_NRS                  " & vbNewLine _
                                             & "   ,UM.GOODS_NM AS GOODS_NM          " & vbNewLine _
                                             & "   ,UT.SEIQ_PKG_UT                   " & vbNewLine _
                                             & " --  ,UT.SEIQ_NG_NB    --Notes1290   " & vbNewLine _
                                             & "   ,UT.DECI_NG_NB AS SEIQ_NG_UT   --Notes1290  " & vbNewLine _
                                             & "   ,UT.SEIQ_KYORI                    " & vbNewLine _
                                             & "   ,UT.SEIQ_WT                       " & vbNewLine _
                                             & "   ,UT.DECI_UNCHIN                   " & vbNewLine _
                                             & "   ,UT.DECI_CITY_EXTC                " & vbNewLine _
                                             & "   ,UT.DECI_WINT_EXTC                " & vbNewLine _
                                             & "   ,UT.DECI_RELY_EXTC                " & vbNewLine _
                                             & "   ,UT.DECI_TOLL                     " & vbNewLine _
                                             & "   ,UT.DECI_INSU                     " & vbNewLine _
                                             & "   ,MCD2.SET_NAIYO AS DEPART         " & vbNewLine _
                                             & "--(2012.09.28)要望番号1305 -- START -- " & vbNewLine _
                                             & "   ,UT.DECI_WT                         " & vbNewLine _
                                             & "--(2012.09.28)要望番号1305 --  END  -- " & vbNewLine _
                                             & "   ,UT.DECI_KYORI  --Notes1413--       " & vbNewLine _
                                             & "   FROM                                " & vbNewLine _
                                             & "   $LM_TRN$..F_UNSO_L UL               " & vbNewLine _
                                             & "   LEFT JOIN $LM_TRN$..F_UNSO_M UM     " & vbNewLine _
                                             & "   ON UL.UNSO_NO_L = UM.UNSO_NO_L    " & vbNewLine _
                                             & "   LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT " & vbNewLine _
                                             & "   ON UM.UNSO_NO_L = UT.UNSO_NO_L    " & vbNewLine _
                                             & "   AND UM.UNSO_NO_M = UT.UNSO_NO_M   " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2 " & vbNewLine _
                                             & "ON MCD2.NRS_BR_CD = UL.NRS_BR_CD      " & vbNewLine _
                                             & "AND MCD2.CUST_CD =                    " & vbNewLine _
                                             & "(UT.CUST_CD_L +                       " & vbNewLine _
                                             & " UT.CUST_CD_M +                       " & vbNewLine _
                                             & " UT.CUST_CD_S +                       " & vbNewLine _
                                             & " UT.CUST_CD_SS)                       " & vbNewLine _
                                             & "AND MCD2.SUB_KB = '01'                " & vbNewLine _
                                             & "   where                             " & vbNewLine _
                                             & "   UL.SYS_DEL_FLG = '0'              " & vbNewLine _
                                             & "   AND UT.SYS_DEL_FLG = '0'          " & vbNewLine _
                                             & "   AND UT.DECI_UNCHIN <> 0           " & vbNewLine _
                                             & "   AND UT.SEIQ_FIXED_FLAG = '01'     " & vbNewLine _
                                             & "   AND UL.MOTO_DATA_KB = '40'        " & vbNewLine _
                                             & "   --条件文--------------------------" & vbNewLine _
                                             & "   --NRS_BR_CD  ここに条件を入れる   " & vbNewLine _
                                             & "   --CUST_CD_L  ここに条件を入れる   " & vbNewLine _
                                             & "   --CUST_CD_M  ここに条件を入れる   " & vbNewLine _
                                             & "   --CUST_CD_S  ここに条件を入れる   " & vbNewLine _
                                             & "   --CUST_CD_SS  ここに条件を入れる  " & vbNewLine _
                                             & "   --F_DATE  ここに条件を入れる      " & vbNewLine _
                                             & "   --T_DATE  ここに条件を入れる      " & vbNewLine _
                                             & "   AND UL.NRS_BR_CD  = @NRS_BR_CD        " & vbNewLine _
                                             & "   AND UL.CUST_CD_L  = @CUST_CD_L    " & vbNewLine _
                                             & "   AND UL.CUST_CD_M  = @CUST_CD_M    " & vbNewLine _
                                             & "   --AND UL.CUST_CD_S  = @CUST_CD_S    " & vbNewLine _
                                             & "   --AND UL.CUST_CD_SS = @CUST_CD_SS   " & vbNewLine _
                                             & "   AND UL.OUTKA_PLAN_DATE >= @F_DATE " & vbNewLine _
                                             & "   AND UL.OUTKA_PLAN_DATE <= @T_DATE " & vbNewLine _
                                             & "   ) BASE                            " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句(荷役料)  ★
    ''' </summary>
    ''' <remarks>
    '''   2011/10/17 修正 須賀
    '''      商品コード取得時、削除フラグを抽出条件から除外
    ''' </remarks>
    Private Const SQL_FROM_NIYAKU As String = "   LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD1           " & vbNewLine _
                                             & "   ON MCD1.NRS_BR_CD = BASE.NRS_BR_CD     " & vbNewLine _
                                             & "   AND MCD1.CUST_CD =                     " & vbNewLine _
                                             & "    (BASE.CUST_CD_L                       " & vbNewLine _
                                             & "   + BASE.CUST_CD_M                       " & vbNewLine _
                                             & "   + BASE.CUST_CD_S                       " & vbNewLine _
                                             & "   + BASE.CUST_CD_SS)                     " & vbNewLine _
                                             & "   AND MCD1.SUB_KB = '14'                 " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD2          " & vbNewLine _
                                             & "   ON MCD2.NRS_BR_CD = BASE.NRS_BR_CD     " & vbNewLine _
                                             & "   AND MCD2.CUST_CD =                     " & vbNewLine _
                                             & "   (BASE.CUST_CD_L                        " & vbNewLine _
                                             & "   + BASE.CUST_CD_M                       " & vbNewLine _
                                             & "   + BASE.CUST_CD_S                       " & vbNewLine _
                                             & "   + BASE.CUST_CD_SS)                     " & vbNewLine _
                                             & "   AND MCD2.SUB_KB = '01'                 " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_CUST MC                    " & vbNewLine _
                                             & "   ON BASE.NRS_BR_CD = MC.NRS_BR_CD       " & vbNewLine _
                                             & "   AND BASE.CUST_CD_L = MC.CUST_CD_L      " & vbNewLine _
                                             & "   AND BASE.CUST_CD_M = MC.CUST_CD_M      " & vbNewLine _
                                             & "   AND BASE.CUST_CD_S = MC.CUST_CD_S      " & vbNewLine _
                                             & "   AND BASE.CUST_CD_SS  = MC.CUST_CD_SS   " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$.. M_GOODS MG                   " & vbNewLine _
                                             & "   ON BASE.NRS_BR_CD = MG.NRS_BR_CD       " & vbNewLine _
                                             & "   AND BASE.GOODS_CD_NRS = MG.GOODS_CD_NRS " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_DEST MD                     " & vbNewLine _
                                             & "   ON BASE.NRS_BR_CD = MD.NRS_BR_CD        " & vbNewLine _
                                             & "   AND BASE.CUST_CD_L = MD.CUST_CD_L       " & vbNewLine _
                                             & "   AND BASE.DEST_CD = MD.DEST_CD           " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..   M_JIS MJ                      " & vbNewLine _
                                             & "   ON MD.JIS = MJ.JIS_CD                   " & vbNewLine _
                                             & "   --運賃での荷主帳票パターン取得          " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR1       " & vbNewLine _
                                             & "   ON  BASE.NRS_BR_CD = MCR1.NRS_BR_CD     " & vbNewLine _
                                             & "   AND BASE.CUST_CD_L = MCR1.CUST_CD_L     " & vbNewLine _
                                             & "   AND BASE.CUST_CD_M = MCR1.CUST_CD_M     " & vbNewLine _
                                             & "   AND MCR1.CUST_CD_S = CASE WHEN BASE.CUST_CD_S IS NOT NULL THEN " & vbNewLine _
                                             & "   					    BASE.CUST_CD_S ELSE '00' END              " & vbNewLine _
                                             & "   AND MCR1.PTN_ID =  CASE @PRT_TYPE                              " & vbNewLine _
                                             & "   				WHEN '01' THEN '71'      --通常                   " & vbNewLine _
                                             & "   				WHEN '02' THEN '72' END  --簿外品                 " & vbNewLine _
                                             & "   --帳票パターン取得                        " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_RPT MR1               " & vbNewLine _
                                             & "   ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD        " & vbNewLine _
                                             & "   AND MR1.PTN_ID = MCR1.PTN_ID              " & vbNewLine _
                                             & "   AND MR1.PTN_CD = MCR1.PTN_CD              " & vbNewLine _
                                             & "   AND MR1.SYS_DEL_FLG = '0'                 " & vbNewLine _
                                             & "   --存在しない場合の帳票パターン取得        " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_RPT MR2               " & vbNewLine _
                                             & "   ON  MR2.NRS_BR_CD = BASE.NRS_BR_CD        " & vbNewLine _
                                             & "   AND MR2.PTN_ID =  CASE @PRT_TYPE          " & vbNewLine _
                                             & "   				WHEN '01' THEN '71'          " & vbNewLine _
                                             & "   				WHEN '02' THEN '72' END      " & vbNewLine _
                                             & "   AND MR2.STANDARD_FLAG = '01'              " & vbNewLine _
                                             & "   AND MR2.SYS_DEL_FLG = '0'                 " & vbNewLine _
                                             & "   WHERE                                     " & vbNewLine _
                                             & "   BASE.NO = 1                               " & vbNewLine _
                                             & "   --条件文----------------------------------" & vbNewLine _
                                             & "   --DEPART  ここに条件を入れる              " & vbNewLine _
                                             & "   AND @DEPART = CASE WHEN @DEPART = '' THEN @DEPART " & vbNewLine _
                                             & "                      ELSE RIGHT('00' + MCD2.SET_NAIYO ,2) END " & vbNewLine _
                                             & "   --PRT_TYPE  ここに条件を入れる            " & vbNewLine _
                                             & "   AND BASE.OFB_KB = CASE @PRT_TYPE          " & vbNewLine _
                                             & "   				WHEN '01' THEN '01'          " & vbNewLine _
                                             & "   				WHEN '02' THEN '02' END      " & vbNewLine _
                                             & "   ------------------------------------------" & vbNewLine _
                                             & "   ) MAIN2                                   " & vbNewLine

    Private Const SQL_ORDERBY_BOHIN As String = "   ORDER BY                                  " & vbNewLine _
                                             & "   MAIN2.OFB_KB                              " & vbNewLine _
                                             & "   ,MAIN2.DEPART                             " & vbNewLine _
                                             & "   ,MAIN2.SRC_CD                             " & vbNewLine _
                                             & "   ,MAIN2.FRB_CD                             " & vbNewLine _
                                             & "   ,MAIN2.OUTKA_PLAN_DATE                    " & vbNewLine _
                                             & "   ,MAIN2.MOS_NO                             " & vbNewLine

    Private Const SQL_ORDERBY_BOGAIHIN As String = "   ORDER BY                                  " & vbNewLine _
                                                 & "   MAIN2.OFB_KB                              " & vbNewLine _
                                                 & "   ,MAIN2.DEPART                             " & vbNewLine _
                                                 & "   ,MAIN2.OUTKA_PLAN_DATE                    " & vbNewLine _
                                                 & "   ,MAIN2.MOS_NO                             " & vbNewLine


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

        '☆
        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Dim Ssql As String = String.Empty
        Me._StrSql.Append(LMG540DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select)
        Me._StrSql.Append(LMG540DAC.SQL_FROM_MAIN2)
        Me._StrSql.Append(LMG540DAC.SQL_SELECT_BASE_OUT)
        Me._StrSql.Append(LMG540DAC.SQL_SELECT_IN)
        Me._StrSql.Append(LMG540DAC.SQL_SELECT_UNSO)
        Me._StrSql.Append(LMG540DAC.SQL_FROM_NIYAKU)
        '        Call Me.SetConditionrtDataSQL()
        Call Me.SetConditionSQLparam()
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG540DAC", "SelectMPrt", cmd)

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
    ''' 保管料・荷役料請求明細出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>保管料・荷役料請求明細出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG540IN")
        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Dim Ssql As String = String.Empty
        Me._StrSql.Append(LMG540DAC.SQL_SELECT_MAIN2)
        Me._StrSql.Append(LMG540DAC.SQL_FROM_MAIN2)
        Me._StrSql.Append(LMG540DAC.SQL_SELECT_BASE_OUT)
        Me._StrSql.Append(LMG540DAC.SQL_SELECT_IN)
        Me._StrSql.Append(LMG540DAC.SQL_SELECT_UNSO)
        Me._StrSql.Append(LMG540DAC.SQL_FROM_NIYAKU)
        If "01".Equals(Me._Row.Item("PRT_TYPE").ToString()) = True Then
            '簿品の場合
            Me._StrSql.Append(LMG540DAC.SQL_ORDERBY_BOHIN)
        ElseIf "02".Equals(Me._Row.Item("PRT_TYPE").ToString()) = True Then
            '簿外品の場合
            Me._StrSql.Append(LMG540DAC.SQL_ORDERBY_BOGAIHIN)
        End If
        Call Me.SetConditionSQLparam()
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG540DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("DEPART", "DEPART")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("MOS_NO", "MOS_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("TARIFF_DISPNM", "TARIFF_DISPNM")
        map.Add("SHI", "SHI")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SEIQ_PKG_UT", "SEIQ_PKG_UT")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUTKA_CTL_NO", "INOUTKA_CTL_NO")
        map.Add("DEST_KB", "DEST_KB")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG540OUT")

        Return ds

    End Function


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQLparam()


        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            'USER_CD
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("DEPART").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", whereStr, DBDataType.CHAR))

            whereStr = .Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("PRT_TYPE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TYPE", whereStr, DBDataType.CHAR))
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
