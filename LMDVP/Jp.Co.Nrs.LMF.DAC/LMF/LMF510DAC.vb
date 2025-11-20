' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF510DAC : 運賃請求明細
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF510DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF510DAC
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
                                            & ",'41'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

#End Region

#Region "SELECT句"

#Region "SELECT句(印刷データ：標準)"
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
                                          & " , CASE WHEN SUBSTRING(@T_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                          & "        THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRS.NRS_BR_NM)                   " & vbNewLine _
                                          & "        ELSE NRS.NRS_BR_NM                                                  " & vbNewLine _
                                          & "        END  AS NRS_BR_NM                                                   " & vbNewLine _
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
                                          & "--(2012.09.25)要望番号1452 -- START --                                                                            " & vbNewLine _
                                          & " ,  UNCHIN.SEIQTO_CD + UNSO.MOTO_DATA_KB + UNCHIN.CUST_CD_L + UNCHIN.CUST_CD_M                                    " & vbNewLine _
                                          & "  + UNCHIN.CUST_CD_S + UNCHIN.CUST_CD_SS  AS SYS_PAGE_KEY                                                         " & vbNewLine _
                                          & "--(2012.09.25)要望番号1452 --  END  --                                                                            " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_2 IS NOT NULL AND DEST3.AD_2 <> '') THEN DEST3.AD_2           " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_2 IS NOT NULL AND DEST4.AD_2 <> '') THEN DEST4.AD_2           " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN (DEST.AD_2 IS NOT NULL AND DEST.AD_2 <> '') THEN DEST.AD_2                                           " & vbNewLine _
                                          & "        ELSE DEST2.AD_2                                                                                           " & vbNewLine _
                                          & "   END                   AS AD_2                                                                                  " & vbNewLine _
                                          & " , ''             AS OUTPUT_KBN                                                                                   " & vbNewLine _
                                          & " , UNSO.BUY_CHU_NO             AS BUY_CHU_NO                                                                      " & vbNewLine

#End Region

#If True Then    'ADD 2021/05/17 020775   【LMS】ｻｸﾗの運賃明細の変更
#Region "SELECT句(印刷データ：LMF523)"
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA523 As String = " SELECT                                                                   " & vbNewLine _
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
                                          & " , CASE WHEN SUBSTRING(@T_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                          & "        THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRS.NRS_BR_NM)                   " & vbNewLine _
                                          & "        ELSE NRS.NRS_BR_NM                                                  " & vbNewLine _
                                          & "        END  AS NRS_BR_NM                                                   " & vbNewLine _
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
                                          & "--(2012.09.25)要望番号1452 -- START --                                                                            " & vbNewLine _
                                          & " ,  UNCHIN.SEIQTO_CD + UNSO.MOTO_DATA_KB + UNCHIN.CUST_CD_L + UNCHIN.CUST_CD_M                                    " & vbNewLine _
                                          & "  + UNCHIN.CUST_CD_S + UNCHIN.CUST_CD_SS  AS SYS_PAGE_KEY                                                         " & vbNewLine _
                                          & "--(2012.09.25)要望番号1452 --  END  --                                                                            " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_2 IS NOT NULL AND DEST3.AD_2 <> '') THEN DEST3.AD_2           " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_2 IS NOT NULL AND DEST4.AD_2 <> '') THEN DEST4.AD_2           " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN (DEST.AD_2 IS NOT NULL AND DEST.AD_2 <> '') THEN DEST.AD_2                                           " & vbNewLine _
                                          & "        ELSE DEST2.AD_2                                                                                           " & vbNewLine _
                                          & "   END                   AS AD_2                                                                                  " & vbNewLine _
                                          & " , ''             AS OUTPUT_KBN                                                                                   " & vbNewLine _
                                          & " , UNSO.BUY_CHU_NO             AS BUY_CHU_NO                                                                      " & vbNewLine _
                                          & "  ,DENP_NO = Case when UNSO.UNSO_CD = '001' AND UNSO.UNSO_BR_CD = '000'THEN CASE WHEN UNSO.MOTO_DATA_KB = '20'THEN OUTL.DENP_NO" & vbNewLine _
                                          & " 			                                                                      ELSE ''  END                                  " & vbNewLine _
                                          & " 			       ELSE  ''                                                                " & vbNewLine _
                                          & " 		      END                                                                          " & vbNewLine _
                                          & "  ,INOUTKA_NO_L = Case when UNSO.UNSO_CD = '001' AND UNSO.UNSO_BR_CD = '000'THEN UNSO.INOUTKA_NO_L  " & vbNewLine _
                                          & " 			       ELSE  ''                                                                " & vbNewLine _
                                          & " 		      END                                                                          " & vbNewLine


#End Region
#End If

#Region "SELECT句(印刷データ：LMF514用)"
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMF514 As String = " SELECT                                                            " & vbNewLine _
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
                                          & " , CASE WHEN SUBSTRING(@T_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                          & "        THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRS.NRS_BR_NM)                   " & vbNewLine _
                                          & "        ELSE NRS.NRS_BR_NM                                                  " & vbNewLine _
                                          & "        END  AS NRS_BR_NM                                                   " & vbNewLine _
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
                                          & "--(2012.09.25)要望番号1452 -- START --                                                                            " & vbNewLine _
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
                                          & "--(2012.09.25)要望番号1452 --  END  --                                                                            " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_2 IS NOT NULL AND DEST3.AD_2 <> '') THEN DEST3.AD_2           " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_2 IS NOT NULL AND DEST4.AD_2 <> '') THEN DEST4.AD_2           " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN (DEST.AD_2 IS NOT NULL AND DEST.AD_2 <> '') THEN DEST.AD_2                                           " & vbNewLine _
                                          & "        ELSE DEST2.AD_2                                                                                           " & vbNewLine _
                                          & "   END                   AS AD_2                                                                                  " & vbNewLine _
                                          & " , ''             AS OUTPUT_KBN                                                                                   " & vbNewLine _
                                          & " , UNSO.BUY_CHU_NO             AS BUY_CHU_NO                                                                      " & vbNewLine
#End Region

#Region "SELECT句(印刷データ：LMF515用)"
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMF515 As String = " SELECT                                                            " & vbNewLine _
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
                                          & " , CASE WHEN SUBSTRING(@T_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                          & "        THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRS.NRS_BR_NM)                   " & vbNewLine _
                                          & "        ELSE NRS.NRS_BR_NM                                                  " & vbNewLine _
                                          & "        END  AS NRS_BR_NM                                                   " & vbNewLine _
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
                                          & "--(2012.09.25)要望番号1452 -- START --                                                                            " & vbNewLine _
                                          & " , CASE WHEN UNCHIN.NRS_BR_CD = '30' AND UNCHIN.CUST_CD_L = '00288' THEN                                                      " & vbNewLine _
                                          & "                               UNCHIN.SEIQTO_CD                                                                   " & vbNewLine _
                                          & "                             + UNSO.MOTO_DATA_KB                                                                  " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_L                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_M                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_S                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_SS                                                                  " & vbNewLine _
                                          & "                             + GOODS.SEARCH_KEY_1                                                                 " & vbNewLine _
                                          & "        ELSE                   UNCHIN.SEIQTO_CD                                                                   " & vbNewLine _
                                          & "                             + UNSO.MOTO_DATA_KB                                                                  " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_L                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_M                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_S                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_SS                                                                  " & vbNewLine _
                                          & "   END      AS SYS_PAGE_KEY                                                                                       " & vbNewLine _
                                          & "--(2012.09.25)要望番号1452 --  END  --                                                                            " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_2 IS NOT NULL AND DEST3.AD_2 <> '') THEN DEST3.AD_2           " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_2 IS NOT NULL AND DEST4.AD_2 <> '') THEN DEST4.AD_2           " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN (DEST.AD_2 IS NOT NULL AND DEST.AD_2 <> '') THEN DEST.AD_2                                           " & vbNewLine _
                                          & "        ELSE DEST2.AD_2                                                                                           " & vbNewLine _
                                          & "   END                   AS AD_2                                                                                  " & vbNewLine _
                                          & " , ''             AS OUTPUT_KBN                                                                                   " & vbNewLine _
                                          & " , UNSO.BUY_CHU_NO             AS BUY_CHU_NO                                                                      " & vbNewLine
#End Region

#Region "SELECT句(印刷データ：LMF516用)"
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMF516 As String = " SELECT                                                            " & vbNewLine _
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
                                          & " , CASE WHEN SUBSTRING(@T_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                          & "        THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRS.NRS_BR_NM)                   " & vbNewLine _
                                          & "        ELSE NRS.NRS_BR_NM                                                  " & vbNewLine _
                                          & "        END  AS NRS_BR_NM                                                   " & vbNewLine _
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
                                          & "--(2012.09.25)要望番号1452 -- START --                                                                            " & vbNewLine _
                                          & "  , CASE WHEN UNCHIN.NRS_BR_CD = '10' AND UNCHIN.CUST_CD_L = '00046' AND UNSO.MOTO_DATA_KB = '10' THEN                        " & vbNewLine _
                                          & "                             + UNSO.MOTO_DATA_KB                                                                  " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_L                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_M                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_S                                                                   " & vbNewLine _
                                          & "                             + UNCHIN.CUST_CD_SS                                                                  " & vbNewLine _
                                          & "                             + GOODS.SEARCH_KEY_1                                                                 " & vbNewLine _
                                          & "        WHEN  UNCHIN.NRS_BR_CD = '10' AND UNCHIN.CUST_CD_L = '00046' AND UNSO.MOTO_DATA_KB <> '10' THEN                       " & vbNewLine _
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
                                          & "--(2012.09.25)要望番号1452 --  END  --                                                                            " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_2 IS NOT NULL AND DEST3.AD_2 <> '') THEN DEST3.AD_2           " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_2 IS NOT NULL AND DEST4.AD_2 <> '') THEN DEST4.AD_2           " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN (DEST.AD_2 IS NOT NULL AND DEST.AD_2 <> '') THEN DEST.AD_2                                           " & vbNewLine _
                                          & "        ELSE DEST2.AD_2                                                                                           " & vbNewLine _
                                          & "   END                   AS AD_2                                                                                  " & vbNewLine _
                                          & "--(2012.11.15)LMF516 -- START --                                                                                  " & vbNewLine _
                                          & "--(2012.11.30)要望番号1642 -- START --                                                                            " & vbNewLine _
                                          & "--, CASE WHEN UNCHIN.NRS_BR_CD = '10' AND UNCHIN.CUST_CD_L = '00046' AND UNSO.MOTO_DATA_KB = '10' THEN            " & vbNewLine _
                                          & "--                           '10'                                                                                 " & vbNewLine _
                                          & "--      ELSE                 '20'                                                                                 " & vbNewLine _
                                          & "-- END      AS OUTPUT_KBN                                                                                         " & vbNewLine _
                                          & " , KBN.KBN_NM2                 AS OUTPUT_KBN                                                                      " & vbNewLine _
                                          & "--(2012.11.30)要望番号1642 --  END  --                                                                            " & vbNewLine _
                                          & "--(2012.11.15)LMF516 --  END  --                                                                                  " & vbNewLine _
                                          & " , UNSO.BUY_CHU_NO             AS BUY_CHU_NO                                                                      " & vbNewLine

#End Region

#Region "LMF660 ゴージョー用SQL ALL"
    Private Const SQL_SELECT_DATA_660 As String = " SELECT                                                                   " & vbNewLine _
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
                                      & " , CASE WHEN SUBSTRING(@T_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                      & "        THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRS.NRS_BR_NM)                   " & vbNewLine _
                                      & "        ELSE NRS.NRS_BR_NM                                                  " & vbNewLine _
                                      & "        END  AS NRS_BR_NM                                                   " & vbNewLine _
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
                                      & "--(2012.09.25)要望番号1452 -- START --                                                                            " & vbNewLine _
                                      & " ,  UNCHIN.SEIQTO_CD + UNSO.MOTO_DATA_KB + UNCHIN.CUST_CD_L + UNCHIN.CUST_CD_M                                    " & vbNewLine _
                                      & "  + UNCHIN.CUST_CD_S + UNCHIN.CUST_CD_SS  AS SYS_PAGE_KEY                                                         " & vbNewLine _
                                      & "--(2012.09.25)要望番号1452 --  END  --                                                                            " & vbNewLine _
                                      & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                      & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                      & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                      & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_2 IS NOT NULL AND DEST3.AD_2 <> '') THEN DEST3.AD_2           " & vbNewLine _
                                      & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_2 IS NOT NULL AND DEST4.AD_2 <> '') THEN DEST4.AD_2           " & vbNewLine _
                                      & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                      & "        WHEN (DEST.AD_2 IS NOT NULL AND DEST.AD_2 <> '') THEN DEST.AD_2                                           " & vbNewLine _
                                      & "        ELSE DEST2.AD_2                                                                                           " & vbNewLine _
                                      & "   END                   AS AD_2                                                                                  " & vbNewLine _
                                      & " , ''             AS OUTPUT_KBN                                                                                   " & vbNewLine _
                                      & " , UNSO.BUY_CHU_NO             AS BUY_CHU_NO                                                                      " & vbNewLine _
                                      & " --2015/1/19 ゴージョー取引先≪発注元追加                                                                         " & vbNewLine _
                                      & " , OUTL.REMARK             AS OUTLREMARK                                                                         " & vbNewLine _
                                        & "FROM                                                               " & vbNewLine _
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
                                         & "   ,MIN(DEST_AD_2)  AS DEST_AD_2                             " & vbNewLine _
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
                                         & "   LEFT JOIN $LM_MST$..M_CUST  AS CUST_F                     " & vbNewLine _
                                         & "   ON UNCHIN.NRS_BR_CD   = CUST_F.NRS_BR_CD                  " & vbNewLine _
                                         & "   AND UNCHIN.CUST_CD_L  = CUST_F.CUST_CD_L                  " & vbNewLine _
                                         & "   AND UNCHIN.CUST_CD_M  = CUST_F.CUST_CD_M                  " & vbNewLine _
                                         & "   AND UNCHIN.CUST_CD_S  = CUST_F.CUST_CD_S                  " & vbNewLine _
                                         & "   AND UNCHIN.CUST_CD_SS = CUST_F.CUST_CD_SS                 " & vbNewLine _
                                         & "	--★請求マスタ(Notes1774)追加                              " & vbNewLine _
                                         & "	LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQTO_CLK                 " & vbNewLine _
                                         & "	ON   CUST_F.UNCHIN_SEIQTO_CD  = SEIQTO_CLK.SEIQTO_CD       " & vbNewLine _
                                         & "	AND  CUST_F.NRS_BR_CD         = SEIQTO_CLK.NRS_BR_CD       " & vbNewLine _
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
                                         & "--(2012.11.30)要望番号1642 -- START --                       " & vbNewLine _
                                         & "	--区分マスタ（元データ区分の取得）※ LMF516対応            " & vbNewLine _
                                         & "	LEFT JOIN $LM_MST$..Z_KBN AS KBN				           " & vbNewLine _
                                         & "	ON KBN.KBN_GROUP_CD = 'M004'			                   " & vbNewLine _
                                         & " AND KBN.KBN_CD       = UNSO.MOTO_DATA_KB                    " & vbNewLine _
                                         & " AND KBN.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                         & "--(2012.11.30)要望番号1642 --  END  --                       " & vbNewLine _
                                         & "--運賃での荷主帳票パターン取得                               " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                          " & vbNewLine _
                                         & "ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                         & "AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                         & "AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                         & "AND '00' = MCR1.CUST_CD_S                                    " & vbNewLine _
                                         & "AND MCR1.PTN_ID = '41'                                       " & vbNewLine _
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
                                         & "AND MCR2.PTN_ID = '41'                                       " & vbNewLine _
                                         & "--帳票パターン取得                                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_RPT MR2                                " & vbNewLine _
                                         & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                           " & vbNewLine _
                                         & "AND MR2.PTN_ID = MCR2.PTN_ID                                 " & vbNewLine _
                                         & "AND MR2.PTN_CD = MCR2.PTN_CD                                 " & vbNewLine _
                                         & "AND MR2.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                         & "--存在しない場合の帳票パターン取得                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_RPT MR3                                " & vbNewLine _
                                         & "ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                         " & vbNewLine _
                                         & "AND MR3.PTN_ID = '41'                                        " & vbNewLine _
                                         & "AND MR3.STANDARD_FLAG = '01'                                 " & vbNewLine _
                                         & "AND MR3.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                         & "--2015/1/19 ゴージョー取引先追加                             " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST_DETAILS MDD                       " & vbNewLine _
                                         & "ON  UNSO.NRS_BR_CD = MDD.NRS_BR_CD                           " & vbNewLine _
                                         & "AND UNSO.CUST_CD_L = MDD.CUST_CD_L                           " & vbNewLine _
                                         & "AND UNSO.DEST_CD = MDD.DEST_CD                               " & vbNewLine _
                                         & "AND MDD.SUB_KB = '08'                                        " & vbNewLine _
                                         & "AND MDD.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                         & "--2015/1/19 ゴージョー取引先追加                             " & vbNewLine _
                                         & "LEFT JOIN                                                    " & vbNewLine _
                                         & "    LM_MST..Z_KBN RPT_CHG_START_YM                           " & vbNewLine _
                                         & "        ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'           " & vbNewLine _
                                         & "        AND RPT_CHG_START_YM.KBN_CD       = '01'             " & vbNewLine _
                                         & "        AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'              " & vbNewLine _
                                         & "LEFT JOIN                                                    " & vbNewLine _
                                         & "    LM_MST..Z_KBN OLD_NRS_BR_NM                              " & vbNewLine _
                                         & "        ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'              " & vbNewLine _
                                         & "        AND OLD_NRS_BR_NM.KBN_CD       =  UNCHIN.NRS_BR_CD   " & vbNewLine _
                                         & "        AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                 " & vbNewLine _
                                         & "	  WHERE UNCHIN.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                                         & "	  AND   UNCHIN.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "    AND   UNCHIN.SEIQ_FIXED_FLAG='01'                        " & vbNewLine _
                                         & "    AND   UNCHIN.DECI_UNCHIN <> 0                            " & vbNewLine


#End Region

#If True Then   'ADD 2020/06/18 013381　【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
#Region "SELECT句(印刷データ：LMF522用)"
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMF522 As String = " SELECT                                                                   " & vbNewLine _
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
                                          & " , CASE WHEN SUBSTRING(@T_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                          & "        THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRS.NRS_BR_NM)                   " & vbNewLine _
                                          & "        ELSE NRS.NRS_BR_NM                                                  " & vbNewLine _
                                          & "        END  AS NRS_BR_NM                                                   " & vbNewLine _
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
                                          & " --2, Case When UNCHIN.DECI_UNCHIN <> 0 Then UNCHIN.REMARK                     " & vbNewLine _
                                          & " --2       Else CASE WHEN UNSO.MOTO_DATA_KB = '10' THEN  ISNULL(INKAL.OUTKA_FROM_ORD_NO_L,'') " & vbNewLine _
                                          & " --2                 WHEN UNSO.MOTO_DATA_KB = '20' THEN  ISNULL(OUTL.CUST_ORD_NO,'')          " & vbNewLine _
                                          & " --2                 ELSE ''                       End                         " & vbNewLine _
                                          & " --2  End                                               As REMARK              " & vbNewLine _
                                          & "--1 ,CASE WHEN UNSO.MOTO_DATA_KB = '10' THEN  ISNULL(INKAL.OUTKA_FROM_ORD_NO_L,'') " & vbNewLine _
                                          & "--1       WHEN UNSO.MOTO_DATA_KB = '20' THEN  ISNULL(OUTL.CUST_ORD_NO,'')          " & vbNewLine _
                                          & "--1       ELSE ''                                                                  " & vbNewLine _
                                          & "--1   End                                               As REMARK                  " & vbNewLine _
                                          & " , GOODS.CUST_COST_CD2   As CUST_COST_CD2                                   " & vbNewLine _
                                          & " , GOODS.SEARCH_KEY_1    As SEARCH_KEY_1                                    " & vbNewLine _
                                          & " , JIS.SHI               As SHI                                             " & vbNewLine _
                                          & " , UNCHIN.SEIQ_KYORI     As SEIQ_KYORI                                      " & vbNewLine _
                                          & " , GOODS.GOODS_CD_CUST   As GOODS_CD_CUST                                   " & vbNewLine _
                                          & " , UNCHIN.SEIQ_PKG_UT    As SEIQ_PKG_UT                                     " & vbNewLine _
                                          & " , UNCHIN.SEIQ_TARIFF_CD As SEIQ_TARIFF_CD                                  " & vbNewLine _
                                          & " , UNSO.UNSO_NO_L        As UNSO_NO_L                                       " & vbNewLine _
                                          & " --LMF513対応 2012/06/12                                                    " & vbNewLine _
                                          & " , UNSO.JIYU_KB          As JIYU_KB                                         " & vbNewLine _
                                          & " , UNSO.ARR_PLAN_DATE    As ARR_PLAN_DATE                                   " & vbNewLine _
                                          & " , UNSO.ORIG_CD          As NYUUKA_CD                                       " & vbNewLine _
                                          & " , Case When UNSO.MOTO_DATA_KB = '10' AND (DEST3.DEST_NM IS NOT NULL AND DEST3.DEST_NM <> '') THEN DEST3.DEST_NM  " & vbNewLine _
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
                                          & "--(2012.09.25)要望番号1452 -- START --                                                                            " & vbNewLine _
                                          & " ,  UNCHIN.SEIQTO_CD + UNSO.MOTO_DATA_KB + UNCHIN.CUST_CD_L + UNCHIN.CUST_CD_M                                    " & vbNewLine _
                                          & "  + UNCHIN.CUST_CD_S + UNCHIN.CUST_CD_SS  AS SYS_PAGE_KEY                                                         " & vbNewLine _
                                          & "--(2012.09.25)要望番号1452 --  END  --                                                                            " & vbNewLine _
                                          & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                 " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                 " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST3.AD_2 IS NOT NULL AND DEST3.AD_2 <> '') THEN DEST3.AD_2           " & vbNewLine _
                                          & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST4.AD_2 IS NOT NULL AND DEST4.AD_2 <> '') THEN DEST4.AD_2           " & vbNewLine _
                                          & " -- Notes【№712】元データ区分：入荷での判断を追加 START                                                          " & vbNewLine _
                                          & "        WHEN (DEST.AD_2 IS NOT NULL AND DEST.AD_2 <> '') THEN DEST.AD_2                                           " & vbNewLine _
                                          & "        ELSE DEST2.AD_2                                                                                           " & vbNewLine _
                                          & "   END                   AS AD_2                                                                                  " & vbNewLine _
                                          & " , ''             AS OUTPUT_KBN                                                                                   " & vbNewLine _
                                          & " , UNSO.BUY_CHU_NO             AS BUY_CHU_NO                                                                      " & vbNewLine

#End Region

#End If
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
                                           & "   ,MIN(DEST_AD_2)  AS DEST_AD_2                             " & vbNewLine _
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
                                           & "   LEFT JOIN $LM_MST$..M_CUST  AS CUST_F                     " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD   = CUST_F.NRS_BR_CD                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_L  = CUST_F.CUST_CD_L                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_M  = CUST_F.CUST_CD_M                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_S  = CUST_F.CUST_CD_S                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_SS = CUST_F.CUST_CD_SS                 " & vbNewLine _
                                           & "	--★請求マスタ(Notes1774)追加                              " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQTO_CLK                 " & vbNewLine _
                                           & "	ON   CUST_F.UNCHIN_SEIQTO_CD  = SEIQTO_CLK.SEIQTO_CD       " & vbNewLine _
                                           & "	AND  CUST_F.NRS_BR_CD         = SEIQTO_CLK.NRS_BR_CD       " & vbNewLine _
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
                                           & "--(2012.11.30)要望番号1642 -- START --                       " & vbNewLine _
                                           & "	--区分マスタ（元データ区分の取得）※ LMF516対応            " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..Z_KBN AS KBN				           " & vbNewLine _
                                           & "	ON KBN.KBN_GROUP_CD = 'M004'			                   " & vbNewLine _
                                           & " AND KBN.KBN_CD       = UNSO.MOTO_DATA_KB                    " & vbNewLine _
                                           & " AND KBN.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                           & "--(2012.11.30)要望番号1642 --  END  --                       " & vbNewLine _
                                           & "--運賃での荷主帳票パターン取得                               " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                          " & vbNewLine _
                                           & "ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                           & "AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                           & "AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                           & "AND '00' = MCR1.CUST_CD_S                                    " & vbNewLine _
                                           & "AND MCR1.PTN_ID = '41'                                       " & vbNewLine _
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
                                           & "AND MCR2.PTN_ID = '41'                                       " & vbNewLine _
                                           & "--帳票パターン取得                                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR2                                " & vbNewLine _
                                           & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                           " & vbNewLine _
                                           & "AND MR2.PTN_ID = MCR2.PTN_ID                                 " & vbNewLine _
                                           & "AND MR2.PTN_CD = MCR2.PTN_CD                                 " & vbNewLine _
                                           & "AND MR2.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "--存在しない場合の帳票パターン取得                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR3                                " & vbNewLine _
                                           & "ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                         " & vbNewLine _
                                           & "AND MR3.PTN_ID = '41'                                        " & vbNewLine _
                                           & "AND MR3.STANDARD_FLAG = '01'                                 " & vbNewLine _
                                           & "AND MR3.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "LEFT JOIN                                                    " & vbNewLine _
                                           & "    LM_MST..Z_KBN RPT_CHG_START_YM                           " & vbNewLine _
                                           & "        ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'           " & vbNewLine _
                                           & "        AND RPT_CHG_START_YM.KBN_CD       = '01'             " & vbNewLine _
                                           & "        AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'              " & vbNewLine _
                                           & "LEFT JOIN                                                    " & vbNewLine _
                                           & "    LM_MST..Z_KBN OLD_NRS_BR_NM                              " & vbNewLine _
                                           & "        ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'              " & vbNewLine _
                                           & "        AND OLD_NRS_BR_NM.KBN_CD       =  UNCHIN.NRS_BR_CD   " & vbNewLine _
                                           & "        AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                 " & vbNewLine _
                                           & "	  WHERE UNCHIN.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                                           & "	  AND   UNCHIN.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                           & "    AND   UNCHIN.SEIQ_FIXED_FLAG='01'                        " & vbNewLine

    Private Const SQL_WHERE_UNCHIN_NOT_0 As String = "    AND   UNCHIN.DECI_UNCHIN <> 0                            " & vbNewLine
#If True Then   'ADD 2020/06/18 013381　【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
    Private Const SQL_WHERE_UNCHIN_NOT_0_LMF522 As String = "    AND  (   ( UNCHIN.DECI_UNCHIN = 0 AND UNCHIN.SEIQ_GROUP_NO <> '' ) " & vbNewLine _
                                                          & "	       OR ( UNCHIN.DECI_UNCHIN <> 0  )   )                          " & vbNewLine
#End If
    '& "	   AND UNCHIN.CUST_CD_L=@CUST_CD_L                  	  " & vbNewLine _
    '& "	   AND UNCHIN.CUST_CD_M=@CUST_CD_M                        " & vbNewLine _
    '& "	   AND UNCHIN.SEIQTO_CD=@SEIQ_CD                  	      " & vbNewLine _
    '& "	  AND ((@UNTIN_CALCULATION_KB ='01'                       " & vbNewLine _
    '& "	  AND UNSO.OUTKA_PLAN_DATE >=@F_DATE                 	  " & vbNewLine _
    '& "	  AND UNSO.OUTKA_PLAN_DATE <=@T_DATE)                     " & vbNewLine _
    '& "	   OR                                                     " & vbNewLine _
    '& "	   ( @UNTIN_CALCULATION_KB ='02'                          " & vbNewLine _
    '& "	   AND (UNSO.ARR_PLAN_DATE >=@F_DATE                  	  " & vbNewLine _
    '& "	   AND UNSO.ARR_PLAN_DATE <=@T_DATE)))                    " & vbNewLine _
    '& "	   AND UNCHIN.UNTIN_CALCULATION_KB=@UNTIN_CALCULATION_KB  " & vbNewLine _
    '& "	   AND UNCHIN.SYS_DEL_FLG='0'                             " & vbNewLine


    '---> LMF531対応 2012/06/12 
    Private Const SQL_FROM_LMF513 As String = "FROM                                                    " & vbNewLine _
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
                                       & "   ,MIN(DEST_AD_2)  AS DEST_AD_2                             " & vbNewLine _
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
                                       & "AND MCR1.PTN_ID = '41'                                       " & vbNewLine _
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
                                       & "AND MCR2.PTN_ID = '41'                                       " & vbNewLine _
                                       & "--帳票パターン取得                                           " & vbNewLine _
                                       & "LEFT JOIN $LM_MST$..M_RPT MR2                                " & vbNewLine _
                                       & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                           " & vbNewLine _
                                       & "AND MR2.PTN_ID = MCR2.PTN_ID                                 " & vbNewLine _
                                       & "AND MR2.PTN_CD = MCR2.PTN_CD                                 " & vbNewLine _
                                       & "AND MR2.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                       & "--存在しない場合の帳票パターン取得                           " & vbNewLine _
                                       & "LEFT JOIN $LM_MST$..M_RPT MR3                                " & vbNewLine _
                                       & "ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                         " & vbNewLine _
                                       & "AND MR3.PTN_ID = '41'                                        " & vbNewLine _
                                       & "AND MR3.STANDARD_FLAG = '01'                                 " & vbNewLine _
                                       & "AND MR3.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                       & "LEFT JOIN                                                    " & vbNewLine _
                                       & "    LM_MST..Z_KBN RPT_CHG_START_YM                           " & vbNewLine _
                                       & "        ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'           " & vbNewLine _
                                       & "        AND RPT_CHG_START_YM.KBN_CD       = '01'             " & vbNewLine _
                                       & "        AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'              " & vbNewLine _
                                       & "LEFT JOIN                                                    " & vbNewLine _
                                       & "    LM_MST..Z_KBN OLD_NRS_BR_NM                              " & vbNewLine _
                                       & "        ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'              " & vbNewLine _
                                       & "        AND OLD_NRS_BR_NM.KBN_CD       =  UNCHIN.NRS_BR_CD   " & vbNewLine _
                                       & "        AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                 " & vbNewLine _
                                       & "	  WHERE UNCHIN.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                                       & "	  AND   UNCHIN.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                       & "    AND   UNCHIN.DECI_UNCHIN <> 0                            " & vbNewLine _
                                       & "    AND   UNCHIN.SEIQ_FIXED_FLAG='01'                        " & vbNewLine _
                                       & "    AND UNSO.MOTO_DATA_KB <> '10'                            " & vbNewLine _
                                       & "    AND UNSO.MOTO_DATA_KB <> '20'                            " & vbNewLine _
                                       & "    AND (UNSO.JIYU_KB ='02' OR UNSO.JIYU_KB ='03')           " & vbNewLine
    '<--- LMF531対応 2012/06/12
#If True Then   'ADD 2020/06/18 013381　【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
    Private Const SQL_FROM_LMF522 As String = "FROM                                                               " & vbNewLine _
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
                                           & "   ,MIN(DEST_AD_2)  AS DEST_AD_2                             " & vbNewLine _
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
                                           & "	--入荷L				                                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..B_INKA_L AS INKAL 				       " & vbNewLine _
                                           & "	ON  INKAL.NRS_BR_CD   =UNSO.NRS_BR_CD				       " & vbNewLine _
                                           & "	AND INKAL.INKA_NO_L   =UNSO.INOUTKA_NO_L			       " & vbNewLine _
                                           & "  AND INKAL.SYS_DEL_FLG ='0'				                   " & vbNewLine _
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
                                           & "   LEFT JOIN $LM_MST$..M_CUST  AS CUST_F                     " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD   = CUST_F.NRS_BR_CD                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_L  = CUST_F.CUST_CD_L                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_M  = CUST_F.CUST_CD_M                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_S  = CUST_F.CUST_CD_S                  " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_SS = CUST_F.CUST_CD_SS                 " & vbNewLine _
                                           & "	--★請求マスタ(Notes1774)追加                              " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQTO_CLK                 " & vbNewLine _
                                           & "	ON   CUST_F.UNCHIN_SEIQTO_CD  = SEIQTO_CLK.SEIQTO_CD       " & vbNewLine _
                                           & "	AND  CUST_F.NRS_BR_CD         = SEIQTO_CLK.NRS_BR_CD       " & vbNewLine _
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
                                           & "--(2012.11.30)要望番号1642 -- START --                       " & vbNewLine _
                                           & "	--区分マスタ（元データ区分の取得）※ LMF516対応            " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..Z_KBN AS KBN				           " & vbNewLine _
                                           & "	ON KBN.KBN_GROUP_CD = 'M004'			                   " & vbNewLine _
                                           & " AND KBN.KBN_CD       = UNSO.MOTO_DATA_KB                    " & vbNewLine _
                                           & " AND KBN.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                           & "--(2012.11.30)要望番号1642 --  END  --                       " & vbNewLine _
                                           & "--運賃での荷主帳票パターン取得                               " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                          " & vbNewLine _
                                           & "ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                           & "AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                           & "AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                           & "AND '00' = MCR1.CUST_CD_S                                    " & vbNewLine _
                                           & "AND MCR1.PTN_ID = '41'                                       " & vbNewLine _
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
                                           & "AND MCR2.PTN_ID = '41'                                       " & vbNewLine _
                                           & "--帳票パターン取得                                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR2                                " & vbNewLine _
                                           & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                           " & vbNewLine _
                                           & "AND MR2.PTN_ID = MCR2.PTN_ID                                 " & vbNewLine _
                                           & "AND MR2.PTN_CD = MCR2.PTN_CD                                 " & vbNewLine _
                                           & "AND MR2.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "--存在しない場合の帳票パターン取得                           " & vbNewLine _
                                           & "LEFT JOIN $LM_MST$..M_RPT MR3                                " & vbNewLine _
                                           & "ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                         " & vbNewLine _
                                           & "AND MR3.PTN_ID = '41'                                        " & vbNewLine _
                                           & "AND MR3.STANDARD_FLAG = '01'                                 " & vbNewLine _
                                           & "AND MR3.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                           & "LEFT JOIN                                                    " & vbNewLine _
                                           & "    LM_MST..Z_KBN RPT_CHG_START_YM                           " & vbNewLine _
                                           & "        ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'           " & vbNewLine _
                                           & "        AND RPT_CHG_START_YM.KBN_CD       = '01'             " & vbNewLine _
                                           & "        AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'              " & vbNewLine _
                                           & "LEFT JOIN                                                    " & vbNewLine _
                                           & "    LM_MST..Z_KBN OLD_NRS_BR_NM                              " & vbNewLine _
                                           & "        ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'              " & vbNewLine _
                                           & "        AND OLD_NRS_BR_NM.KBN_CD       =  UNCHIN.NRS_BR_CD   " & vbNewLine _
                                           & "        AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                 " & vbNewLine _
                                           & "	  WHERE UNCHIN.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                                           & "	  AND   UNCHIN.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                           & "    AND   UNCHIN.SEIQ_FIXED_FLAG='01'                        " & vbNewLine

#End If
#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                " & vbNewLine _
                                         & "     UNSO.MOTO_DATA_KB                  " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                  " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE               " & vbNewLine _
                                         & "    ,UNSO.DEST_CD                       " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO               " & vbNewLine

#If True Then   'ADD 2021/05/17 020775   【LMS】ｻｸﾗの運賃明細の変更
    ''' <summary>
    ''' ORDER BY LMF523用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY523 As String = "ORDER BY                                " & vbNewLine _
                                         & "     UNSO.MOTO_DATA_KB                  " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                  " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE               " & vbNewLine _
                                         & "    ,UNSO.DEST_CD                       " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO               " & vbNewLine _
                                         & "    ,OUTL.DENP_NO                       " & vbNewLine
#End If

    ''' <summary>
    ''' ORDER BY LMF513用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF513 As String = "ORDER BY                         " & vbNewLine _
                                         & "     UNSO.MOTO_DATA_KB                  " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                  " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_TARIFF_CD              " & vbNewLine _
                                         & "    ,GOODS.SEARCH_KEY_1                 " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE               " & vbNewLine _
                                         & "    ,UNSO.DEST_CD                       " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO               " & vbNewLine

    ' 要望番号：1187 yamanaka 2012.7.4 Start
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF509 As String = "ORDER BY                         " & vbNewLine _
                                         & "     UNSO.MOTO_DATA_KB                  " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                  " & vbNewLine _
                                         & "    ,GOODS.SEARCH_KEY_1                 " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE               " & vbNewLine _
                                         & "    ,UNSO.DEST_CD                       " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO               " & vbNewLine
    '要望番号：1187 yamanaka 2012.7.4 End

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF514 As String = "ORDER BY                         " & vbNewLine _
                                         & "     UNSO.MOTO_DATA_KB                  " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                  " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                   " & vbNewLine _
                                         & "    ,GOODS.SEARCH_KEY_1                 " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE               " & vbNewLine _
                                         & "    ,UNSO.DEST_CD                       " & vbNewLine _
                                         & "    ,UNSO.UNSO_NO_L                     " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO               " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF515 As String = "ORDER BY                         " & vbNewLine _
                                         & "     UNSO.MOTO_DATA_KB                  " & vbNewLine _
                                         & "    ,GOODS.SEARCH_KEY_1                 " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                  " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE               " & vbNewLine _
                                         & "    ,UNSO.DEST_CD                       " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO               " & vbNewLine



    '(2012.09.14) 要望番号1309 荷主コード(小)･(極小)がない用 --- START ---
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF507 As String = "ORDER BY                         " & vbNewLine _
                                                & "      UNSO.MOTO_DATA_KB          " & vbNewLine _
                                                & "    , UNCHIN.SEIQTO_CD           " & vbNewLine _
                                                & "    , UNCHIN.CUST_CD_L           " & vbNewLine _
                                                & "    , UNCHIN.CUST_CD_M           " & vbNewLine _
                                                & "    , UNSO.OUTKA_PLAN_DATE       " & vbNewLine _
                                                & "    , UNSO.DEST_CD               " & vbNewLine _
                                                & "    , UNCHIN.SEIQ_GROUP_NO       " & vbNewLine
    '(2012.09.14) 要望番号1309 荷主コード(小)･(極小)がない用 ---  END  ---


    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF516 As String = "ORDER BY                         " & vbNewLine _
                                         & "     OUTPUT_KBN                         " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                  " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE               " & vbNewLine _
                                         & "    ,UNSO.DEST_CD                       " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO               " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF517 As String = "ORDER BY                         " & vbNewLine _
                                                & "      UNSO.MOTO_DATA_KB          " & vbNewLine _
                                                & "    , UNCHIN.SEIQTO_CD           " & vbNewLine _
                                                & "    , UNCHIN.CUST_CD_L           " & vbNewLine _
                                                & "    , UNCHIN.CUST_CD_M           " & vbNewLine _
                                                & "    , UNCHIN.CUST_CD_S           " & vbNewLine _
                                                & "    , UNCHIN.CUST_CD_SS          " & vbNewLine _
                                                & "    , UNSO.ARR_PLAN_DATE         " & vbNewLine _
                                                & "    , UNSO.OUTKA_PLAN_DATE       " & vbNewLine _
                                                & "    , UNSO.DEST_CD               " & vbNewLine _
                                                & "    , UNCHIN.SEIQ_GROUP_NO       " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF506 As String = "ORDER BY                                " & vbNewLine _
                                                & "     UNSO.MOTO_DATA_KB ASC              " & vbNewLine _
                                                & "    ,UNCHIN.SEIQTO_CD  ASC              " & vbNewLine _
                                                & "    ,UNCHIN.CUST_CD_L  ASC              " & vbNewLine _
                                                & "    ,UNCHIN.CUST_CD_M  ASC              " & vbNewLine _
                                                & "    ,UNCHIN.CUST_CD_S  ASC              " & vbNewLine _
                                                & "    ,UNCHIN.CUST_CD_SS ASC              " & vbNewLine _
                                                & "    ,UNSO.OUTKA_PLAN_DATE ASC           " & vbNewLine _
                                                & "    ,UNSO.ORIG_CD  ASC  --NYUUKA_CD     " & vbNewLine _
                                                & "    ,UNSO.DEST_CD  ASC                  " & vbNewLine _
                                                & "    ,GOODS.SEARCH_KEY_1  ASC            " & vbNewLine _
                                                & "    ,UNCHIN.DECI_WT      ASC            " & vbNewLine _
                                                & "    ,UNCHIN.DECI_UNCHIN  ASC            " & vbNewLine _
                                                & "    ,GOODS.GOODS_CD_CUST ASC            " & vbNewLine

#If True Then   'ADD 2020/06/18 013381　【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
    ''' <summary>
    ''' ORDER BY LMF522
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF522 As String = "ORDER BY                                " & vbNewLine _
                                         & "     UNSO.MOTO_DATA_KB                  " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                   " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                  " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE               " & vbNewLine _
                                         & "    ,UNSO.DEST_CD                       " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO               " & vbNewLine _
                                         & "    ,UNCHIN.DECI_UNCHIN DESC --降順     " & vbNewLine

#End If

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
        Dim inTbl As DataTable = ds.Tables("LMF510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF510DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF510DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMF510IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Select Case rptId
            Case "LMF513"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM_LMF513)      'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

                '要望番号：1187 yamanaka 2012.7.4 Start
            Case "LMF509"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY_LMF509)  'SQL構築(データ抽出用OrderBy句)
                '要望番号：1187 yamanaka 2012.7.4 End

            Case "LMF514"
                '(2012.09.25)要望番号1452 -- START --
                'Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA)       'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA_LMF514) 'SQL構築(データ抽出用Select句)
                '(2012.09.25)要望番号1452 --  END  --
                Me._StrSql.Append(LMF510DAC.SQL_FROM)               'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY_LMF514)    'SQL構築(データ抽出用OrderBy句)

            Case "LMF515"
                '(2012.09.25)要望番号1452 -- START --
                'Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA)       'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA_LMF515) 'SQL構築(データ抽出用Select句)
                '(2012.09.25)要望番号1452 --  END  --
                Me._StrSql.Append(LMF510DAC.SQL_FROM)               'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY_LMF515)    'SQL構築(データ抽出用OrderBy句)

                '(2012.09.14) 要望番号1309 荷主コード(小)･(極小)がない用 --- START ---
            Case "LMF507"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY_LMF507)  'SQL構築(データ抽出用OrderBy句)
                '(2012.09.14) 要望番号1309 荷主コード(小)･(極小)がない用 ---  END  ---

            Case "LMF516"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA_LMF516) 'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM)               'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY_LMF516)         'SQL構築(データ抽出用OrderBy句)

            Case "LMF517"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY_LMF517)  'SQL構築(データ抽出用OrderBy句)

            Case "LMF506"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY_LMF506)  'SQL構築(データ抽出用OrderBy句)
                '2015/1/23 ゴージョー用
            Case "LMF660"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA_660)      'SQL構築
                Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

#If True Then   'ADD 2020/06/18 013381　【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
            Case "LMF522"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA_LMF522)        'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM_LMF522)               'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0_LMF522) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                            'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY_LMF522)           'SQL構築(データ抽出用OrderBy句)

#End If
#If True Then   'ADD 2021/05/17 020775   【LMS】ｻｸﾗの運賃明細の変更
            Case "LMF523"
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA523)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY523)         'SQL構築(データ抽出用OrderBy句)

#End If

            Case Else
                Me._StrSql.Append(LMF510DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
                Me._StrSql.Append(LMF510DAC.SQL_WHERE_UNCHIN_NOT_0) 'SQL構築(データ抽出用Where 句)
                Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
                Me._StrSql.Append(LMF510DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

        End Select

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF510DAC", "SelectPrintData", cmd)

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
        '--LMF513対応 2012/06/12
        map.Add("JIYU_KB", "JIYU_KB")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("NYUUKA_CD", "NYUUKA_CD")
        map.Add("NYUUKA_NM", "NYUUKA_NM")
        map.Add("NYUUKA_AD_1", "NYUUKA_AD_1")
        map.Add("SYUKKA_CD", "SYUKKA_CD")
        map.Add("SYUKKA_NM", "SYUKKA_NM")
        map.Add("SYUKKA_AD_1", "SYUKKA_AD_1")
        '(2012.09.21)要望番号1452 -- START --
        map.Add("SYS_PAGE_KEY", "SYS_PAGE_KEY")
        '(2012.09.21)要望番号1452 --  END  --
        '(2012.11.15)LMH516 start
        map.Add("AD_2", "AD_2")
        map.Add("OUTPUT_KBN", "OUTPUT_KBN")
        '(2012.11.15)LMH516 end
        '(2012.11.20)LMH517 start
        map.Add("BUY_CHU_NO", "BUY_CHU_NO")
        '(2012.11.20)LMH517 end
        Select rptId
            Case "LMF660"
                '2015/1/19 ゴージョー取引先（発注元）追加
                map.Add("OUTLREMARK", "OUTLREMARK")
                '2015/1/19 ゴージョー取引先（発注元）追加

#If True Then   'ADD 2021/05/17 020775   【LMS】ｻｸﾗの運賃明細の変更
            Case "LMF523"
                map.Add("DENP_NO", "DENP_NO")
                map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
#End If
        End Select
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF510OUT")

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
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", Me._Row.Item("T_DATE").ToString(), DBDataType.CHAR))
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

            'UNTIN_CALCULATION_KB
            Me._StrSql.Append(vbNewLine)

            Me._StrSql.Append("           )                                         ")
            Me._StrSql.Append(vbNewLine)

            Me._StrSql.Append("          )                                         ")
            Me._StrSql.Append(vbNewLine)
            '締日区分 2013.02.28 / Notes1774　開始
            whereStr = Me._Row.Item("CLOSE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND SEIQTO_CLK.CLOSE_KB = @CLOSE_KB                	      ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLOSE_KB", Me._Row.Item("CLOSE_KB").ToString(), DBDataType.CHAR))
            End If
            '締日区分 2013.02.28 / Notes1774　終了

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
