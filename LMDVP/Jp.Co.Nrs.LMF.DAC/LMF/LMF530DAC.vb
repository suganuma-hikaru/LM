' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF530DAC : 運賃チェックリスト
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF530DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF530DAC
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
                                            & ",'48'                                                     AS PTN_ID    " & vbNewLine _
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
    Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
                                         & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                         & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID             " & vbNewLine _
                                         & "		  ELSE MR3.RPT_ID END                                AS RPT_ID " & vbNewLine _
                                         & " ,UNSO.MOTO_DATA_KB       AS MOTO_DATA_KB" & vbNewLine _
                                         & " ,UNCHIN.CUST_CD_L         AS CUST_CD_L" & vbNewLine _
                                         & " --,CUST.CUST_NM_L           AS CUST_NM_L" & vbNewLine _
                                         & " --,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & " --      ELSE CUST.CUST_NM_M  END AS CUST_NM_M                      " & vbNewLine _
                                         & " --,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & " --      ELSE CUST.CUST_NM_S  END AS CUST_NM_S                      " & vbNewLine _
                                         & " --,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & " --      ELSE CUST.CUST_NM_SS  END AS CUST_NM_SS                      " & vbNewLine _
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
                                         & " ,@T_DATE                  AS T_DATE" & vbNewLine _
                                         & " ,NRS.NRS_BR_NM            AS NRS_BR_NM" & vbNewLine _
                                         & " ,UNSO.OUTKA_PLAN_DATE     AS OUTKA_PLAN_DATE" & vbNewLine _
                                         & " ,UNCHIN.SEIQ_TARIFF_CD    AS SEIQ_TARIFF_CD" & vbNewLine _
                                         & " ,CASE UNSO.TARIFF_BUNRUI_KB WHEN '40' THEN YOKO.YOKO_REM           " & vbNewLine _
                                         & "       ELSE TARIFF.UNCHIN_TARIFF_REM END AS UNCHIN_TARIFF_REM       " & vbNewLine _
                                         & " ,UNSOM.GOODS_NM           AS GOODS_NM" & vbNewLine _
                                         & " ,UNCHIN.DECI_NG_NB        AS DECI_NG_NB" & vbNewLine _
                                         & " ,UNCHIN.DECI_WT           AS DECI_WT" & vbNewLine _
                                         & " ,UNCHIN.DECI_KYORI        AS DECI_KYORI" & vbNewLine _
                                         & " ,UNSOCO.UNSOCO_NM         AS UNSOCO_NM" & vbNewLine _
                                         & "	--(1)				                                                                 " & vbNewLine _
                                         & " ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN DESTOUTKA.JIS       " & vbNewLine _
                                         & "	--(2)				                                                                 " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_JIS_CD    " & vbNewLine _
                                         & " --721 START                                                                          " & vbNewLine _
                                         & " --(3)①                                                                               " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.JIS                   " & vbNewLine _
                                         & " --(3)②                                                                              " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST04.AD_1 IS NOT NULL AND DEST04.AD_1 <> '') THEN DEST04.JIS                   " & vbNewLine _
                                         & " --721 END                                                                          " & vbNewLine _
                                         & " --(4)①                                                                               " & vbNewLine _
                                         & "       WHEN (DEST.JIS IS NOT NULL AND DEST.JIS <> '') THEN DEST.JIS                   " & vbNewLine _
                                         & " --(4)②                                                                               " & vbNewLine _
                                         & "       ELSE DEST2.JIS                                                                  " & vbNewLine _
                                         & "  END                   AS JIS                                                        " & vbNewLine _
                                         & " ,UNCHIN.DECI_UNCHIN       AS DECI_UNCHIN" & vbNewLine _
                                         & " ,UNCHIN.DECI_CITY_EXTC    AS DECI_CITY_EXTC " & vbNewLine _
                                         & " ,UNCHIN.DECI_WINT_EXTC    AS DECI_WINT_EXTC" & vbNewLine _
                                         & " ,UNCHIN.DECI_RELY_EXTC    AS DECI_RELY_EXTC" & vbNewLine _
                                         & " ,UNCHIN.DECI_TOLL         AS DECI_TOLL" & vbNewLine _
                                         & " ,UNCHIN.DECI_INSU         AS DECI_INSU" & vbNewLine _
                                         & " --☆要望番号376関連でコメントアウト開始☆                                                 " & vbNewLine _
                                         & " --,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM            " & vbNewLine _
                                         & " --      WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM           " & vbNewLine _
                                         & " --      ELSE DEST.DEST_NM                                                                  " & vbNewLine _
                                         & " -- END                      AS DEST_NM                                                     " & vbNewLine _
                                         & " --★要望番号376関連でコメントアウト終了★                                                 " & vbNewLine _
                                         & "	--★変更START 要望番号376①★				                                           " & vbNewLine _
                                         & "	--(1)				                                                                 " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM            " & vbNewLine _
                                         & "	--(2)				                                                                   " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM            " & vbNewLine _
                                         & " --721 START                                                                               " & vbNewLine _
                                         & " --(3)①                                                                               " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST03.DEST_NM IS NOT NULL AND DEST03.DEST_NM <> '')  THEN DEST03.DEST_NM " & vbNewLine _
                                         & " --(3)②                                                                            " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST04.DEST_NM IS NOT NULL AND DEST04.DEST_NM <>  '')   THEN DEST04.DEST_NM " & vbNewLine _
                                         & " --721 END                                                                                 " & vbNewLine _
                                         & " --(4)①                                                                               " & vbNewLine _
                                         & "        WHEN (DEST.DEST_NM IS NOT NULL AND DEST.DEST_NM <> '') THEN DEST.DEST_NM           " & vbNewLine _
                                         & " --721 END                                                                                 " & vbNewLine _
                                         & " --(4)②                                                                               " & vbNewLine _
                                         & "        ELSE DEST2.DEST_NM                                                                 " & vbNewLine _
                                         & "   END                   AS DEST_NM                                                        " & vbNewLine _
                                         & "	--★変更END 要望番号376①★				                                        " & vbNewLine _
                                         & " ,UNCHIN.SEIQ_GROUP_NO     AS SEIQ_GROUP_NO" & vbNewLine _
                                         & " ,UNCHIN.SEIQTO_CD         AS SEIQTO_CD" & vbNewLine _
                                         & " ,SEIQTO.SEIQTO_NM         AS SEIQTO_NM" & vbNewLine _
                                         & " --☆要望番号376関連でコメントアウト開始☆                                                 " & vbNewLine _
                                         & " --,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1          " & vbNewLine _
                                         & " --      WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1          " & vbNewLine _
                                         & " --      ELSE DEST.AD_1                                                                     " & vbNewLine _
                                         & " -- END                      AS AD_1                                                        " & vbNewLine _
                                         & " --★要望番号376関連でコメントアウト終了★                                                 " & vbNewLine _
                                         & "	--★変更START 要望番号376①★				                                        " & vbNewLine _
                                         & "	--(1)				                                                                 " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1          " & vbNewLine _
                                         & "	--(2)				                                                                 " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1          " & vbNewLine _
                                         & "	--(3)				                                                                 " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '10'   AND (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.AD_1" & vbNewLine _
                                         & "    --(4)                                                                                 " & vbNewLine _
                                         & "        ELSE DEST.AD_1                                                                    " & vbNewLine _
                                         & "   END                   AS AD_1                                                           " & vbNewLine _
                                         & "	--★変更END 要望番号376①★				                                        " & vbNewLine _
                                         & "	--(1)				                                                                 " & vbNewLine _
                                         & " ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1          " & vbNewLine _
                                         & "	--(2)				                                                                 " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1          " & vbNewLine _
                                         & "	--(3)NULL,「=''」でいいのか確認				                                                                 " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST04.AD_1 IS NOT NULL  AND DEST04.AD_1 <> '') THEN DEST04.AD_1                    " & vbNewLine _
                                         & "    --(4)                                                                                 " & vbNewLine _
                                         & "       ELSE DEST1.AD_1                                                                    " & vbNewLine _
                                         & "  END                      AS AD_1_a                                                      " & vbNewLine _
                                         & " ,UNSO.UNSO_NO_L           AS UNSO_NO_L" & vbNewLine _
                                         & " ,UNSO.INOUTKA_NO_L        AS INOUTKA_NO_L" & vbNewLine _
                                         & " ,UNCHIN.REMARK            AS REMARK" & vbNewLine _
                                         & " ,UNCHIN.SEIQ_FIXED_FLAG   AS SEIQ_FIXED_FLAG" & vbNewLine _
                                         & " ,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & "       ELSE UNCHIN.CUST_CD_M                       " & vbNewLine _
                                         & "  END                      AS CUST_CD_M " & vbNewLine _
                                         & " ,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & "       ELSE UNCHIN.CUST_CD_S                       " & vbNewLine _
                                         & "  END                      AS CUST_CD_S            " & vbNewLine _
                                         & " ,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & "       ELSE UNCHIN.CUST_CD_SS                      " & vbNewLine _
                                         & "  END                      AS CUST_CD_SS           " & vbNewLine _
                                         & " ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD        " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD        " & vbNewLine _
                                         & "       ELSE UNSO.DEST_CD                                                              " & vbNewLine _
                                         & "  END                      AS DEST_CD                                                    " & vbNewLine _
                                         & " ,UNCHIN.NRS_BR_CD         AS NRS_BR_CD" & vbNewLine _
                                         & " ,UNCHIN.SEIQ_GROUP_NO_M   AS SEIQ_GROUP_NO_M" & vbNewLine _
                                         & " ,UNSOM.UNSO_NO_M          AS UNSO_NO_M" & vbNewLine _
                                         & " ,KBN01.KBN_NM1         AS MOTO_DATA_NM" & vbNewLine _
                                         & " --LMF531対応 2012/06/12                                                                                             " & vbNewLine _
                                         & " , UNSO.JIYU_KB          AS JIYU_KB                                                                                  " & vbNewLine _
                                         & " , UNSO.ARR_PLAN_DATE    AS ARR_PLAN_DATE                                                                            " & vbNewLine _
                                         & " , UNSO.ORIG_CD          AS NYUUKA_CD                                                                                " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST03.DEST_NM IS NOT NULL AND DEST03.DEST_NM <> '') THEN DEST03.DEST_NM  " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST04.DEST_NM IS NOT NULL AND DEST04.DEST_NM <> '') THEN DEST04.DEST_NM  " & vbNewLine _
                                         & "        WHEN (DEST03.DEST_NM IS NOT NULL AND DEST03.DEST_NM <> '') THEN DEST03.DEST_NM                               " & vbNewLine _
                                         & "        ELSE DEST04.DEST_NM                                                                                          " & vbNewLine _
                                         & "   END                   AS NYUUKA_NM                                                                                " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.AD_1           " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST04.AD_1 IS NOT NULL AND DEST04.AD_1 <> '') THEN DEST04.AD_1           " & vbNewLine _
                                         & "        WHEN (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.AD_1                                        " & vbNewLine _
                                         & "        ELSE DEST04.AD_1                                                                                             " & vbNewLine _
                                         & "   END                   AS NYUUKA_AD_1                                                                              " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.JIS           " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST04.AD_1 IS NOT NULL AND DEST04.AD_1 <> '') THEN DEST04.JIS           " & vbNewLine _
                                         & "        WHEN (DEST03.JIS IS NOT NULL AND DEST03.JIS <> '') THEN DEST03.JIS                                           " & vbNewLine _
                                         & "   ELSE DEST04.JIS                                                                                                   " & vbNewLine _
                                         & "   END                   AS NYUUKA_JIS                                                                               " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD                                      " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                      " & vbNewLine _
                                         & "   ELSE UNSO.DEST_CD                                                                                                 " & vbNewLine _
                                         & "   END                   AS SYUKKA_CD                                                                                " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                      " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                      " & vbNewLine _
                                         & "        WHEN (DEST.DEST_NM IS NOT NULL AND DEST.DEST_NM <> '') THEN DEST.DEST_NM                                     " & vbNewLine _
                                         & "        ELSE DEST2.DEST_NM                                                                                           " & vbNewLine _
                                         & "  END                   AS SYUKKA_NM                                                                                 " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                    " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                    " & vbNewLine _
                                         & "        WHEN (DEST.AD_1 IS NOT NULL AND DEST.AD_1 <> '') THEN DEST.AD_1                                              " & vbNewLine _
                                         & "        ELSE DEST2.AD_1                                                                                              " & vbNewLine _
                                         & "   END                   AS SYUKKA_AD_1                                                                              " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN DESTOUTKA.JIS                                     " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_JIS_CD                                  " & vbNewLine _
                                         & "        WHEN (DEST.JIS IS NOT NULL AND DEST.JIS <> '') THEN DEST.JIS                                                 " & vbNewLine _
                                         & "        ELSE DEST2.JIS                                                                                               " & vbNewLine _
                                         & "   END                   AS SYUKKA_JIS                                                                               " & vbNewLine

    '& " ,DEST.JIS                 AS JIS" & vbNewLine _
    '& " ,DEST.DEST_NM             AS DEST_NM" & vbNewLine _
    '& " ,DEST.AD_1                AS AD_1" & vbNewLine _
    '& " ,DEST1.AD_1               AS AD_1_a" & vbNewLine _

    'LMF535対応 2018/06/22 Annen add start
    'Memo)
    'SQL_SELECT_DATAのコピー
    '内部で「LMF535対応 2018/06/22 Annen」と記述されているところが、コピー後変更した部分

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMF535 As String = " SELECT                                                        " & vbNewLine _
                                         & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                         & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID             " & vbNewLine _
                                         & "		  ELSE MR3.RPT_ID END                                AS RPT_ID " & vbNewLine _
                                         & " ,UNSO.MOTO_DATA_KB       AS MOTO_DATA_KB" & vbNewLine _
                                         & " ,UNCHIN.CUST_CD_L         AS CUST_CD_L" & vbNewLine _
                                         & " --,CUST.CUST_NM_L           AS CUST_NM_L" & vbNewLine _
                                         & " --,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & " --      ELSE CUST.CUST_NM_M  END AS CUST_NM_M                      " & vbNewLine _
                                         & " --,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & " --      ELSE CUST.CUST_NM_S  END AS CUST_NM_S                      " & vbNewLine _
                                         & " --,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & " --      ELSE CUST.CUST_NM_SS  END AS CUST_NM_SS                      " & vbNewLine _
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
                                         & " ,@T_DATE                  AS T_DATE" & vbNewLine _
                                         & " ,NRS.NRS_BR_NM            AS NRS_BR_NM" & vbNewLine _
                                         & " ,UNSO.OUTKA_PLAN_DATE     AS OUTKA_PLAN_DATE" & vbNewLine _
                                         & " ,UNCHIN.SEIQ_TARIFF_CD    AS SEIQ_TARIFF_CD" & vbNewLine _
                                         & " ,CASE UNSO.TARIFF_BUNRUI_KB WHEN '40' THEN YOKO.YOKO_REM           " & vbNewLine _
                                         & "       ELSE TARIFF.UNCHIN_TARIFF_REM END AS UNCHIN_TARIFF_REM       " & vbNewLine _
                                         & " ,UNSOM.GOODS_NM           AS GOODS_NM" & vbNewLine _
                                         & " --LMF535対応 2018/06/22 Annen upd start " & vbNewLine _
                                         & " --,UNCHIN.DECI_NG_NB        AS DECI_NG_NB" & vbNewLine _
                                         & " ,SUM(UNSOM_KOSU.UNSO_TTL_NB)     AS DECI_NG_NB" & vbNewLine _
                                         & " --LMF535対応 2018/06/22 Annen upd start " & vbNewLine _
                                         & " ,UNCHIN.DECI_WT           AS DECI_WT" & vbNewLine _
                                         & " ,UNCHIN.DECI_KYORI        AS DECI_KYORI" & vbNewLine _
                                         & " ,UNSOCO.UNSOCO_NM         AS UNSOCO_NM" & vbNewLine _
                                         & "	--(1)				                                                                 " & vbNewLine _
                                         & " ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN DESTOUTKA.JIS       " & vbNewLine _
                                         & "	--(2)				                                                                 " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_JIS_CD    " & vbNewLine _
                                         & " --721 START                                                                          " & vbNewLine _
                                         & " --(3)①                                                                               " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.JIS                   " & vbNewLine _
                                         & " --(3)②                                                                              " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST04.AD_1 IS NOT NULL AND DEST04.AD_1 <> '') THEN DEST04.JIS                   " & vbNewLine _
                                         & " --721 END                                                                          " & vbNewLine _
                                         & " --(4)①                                                                               " & vbNewLine _
                                         & "       WHEN (DEST.JIS IS NOT NULL AND DEST.JIS <> '') THEN DEST.JIS                   " & vbNewLine _
                                         & " --(4)②                                                                               " & vbNewLine _
                                         & "       ELSE DEST2.JIS                                                                  " & vbNewLine _
                                         & "  END                   AS JIS                                                        " & vbNewLine _
                                         & " ,UNCHIN.DECI_UNCHIN       AS DECI_UNCHIN" & vbNewLine _
                                         & " ,UNCHIN.DECI_CITY_EXTC    AS DECI_CITY_EXTC " & vbNewLine _
                                         & " ,UNCHIN.DECI_WINT_EXTC    AS DECI_WINT_EXTC" & vbNewLine _
                                         & " ,UNCHIN.DECI_RELY_EXTC    AS DECI_RELY_EXTC" & vbNewLine _
                                         & " ,UNCHIN.DECI_TOLL         AS DECI_TOLL" & vbNewLine _
                                         & " ,UNCHIN.DECI_INSU         AS DECI_INSU" & vbNewLine _
                                         & " --☆要望番号376関連でコメントアウト開始☆                                                 " & vbNewLine _
                                         & " --,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM            " & vbNewLine _
                                         & " --      WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM           " & vbNewLine _
                                         & " --      ELSE DEST.DEST_NM                                                                  " & vbNewLine _
                                         & " -- END                      AS DEST_NM                                                     " & vbNewLine _
                                         & " --★要望番号376関連でコメントアウト終了★                                                 " & vbNewLine _
                                         & "	--★変更START 要望番号376①★				                                           " & vbNewLine _
                                         & "	--(1)				                                                                 " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM            " & vbNewLine _
                                         & "	--(2)				                                                                   " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM            " & vbNewLine _
                                         & " --721 START                                                                               " & vbNewLine _
                                         & " --(3)①                                                                               " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST03.DEST_NM IS NOT NULL AND DEST03.DEST_NM <> '')  THEN DEST03.DEST_NM " & vbNewLine _
                                         & " --(3)②                                                                            " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST04.DEST_NM IS NOT NULL AND DEST04.DEST_NM <>  '')   THEN DEST04.DEST_NM " & vbNewLine _
                                         & " --721 END                                                                                 " & vbNewLine _
                                         & " --(4)①                                                                               " & vbNewLine _
                                         & "        WHEN (DEST.DEST_NM IS NOT NULL AND DEST.DEST_NM <> '') THEN DEST.DEST_NM           " & vbNewLine _
                                         & " --721 END                                                                                 " & vbNewLine _
                                         & " --(4)②                                                                               " & vbNewLine _
                                         & "        ELSE DEST2.DEST_NM                                                                 " & vbNewLine _
                                         & "   END                   AS DEST_NM                                                        " & vbNewLine _
                                         & "	--★変更END 要望番号376①★				                                        " & vbNewLine _
                                         & " ,UNCHIN.SEIQ_GROUP_NO     AS SEIQ_GROUP_NO" & vbNewLine _
                                         & " ,UNCHIN.SEIQTO_CD         AS SEIQTO_CD" & vbNewLine _
                                         & " ,SEIQTO.SEIQTO_NM         AS SEIQTO_NM" & vbNewLine _
                                         & " --☆要望番号376関連でコメントアウト開始☆                                                 " & vbNewLine _
                                         & " --,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1          " & vbNewLine _
                                         & " --      WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1          " & vbNewLine _
                                         & " --      ELSE DEST.AD_1                                                                     " & vbNewLine _
                                         & " -- END                      AS AD_1                                                        " & vbNewLine _
                                         & " --★要望番号376関連でコメントアウト終了★                                                 " & vbNewLine _
                                         & "	--★変更START 要望番号376①★				                                        " & vbNewLine _
                                         & "	--(1)				                                                                 " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1          " & vbNewLine _
                                         & "	--(2)				                                                                 " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1          " & vbNewLine _
                                         & "	--(3)				                                                                 " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '10'   AND (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.AD_1" & vbNewLine _
                                         & "    --(4)                                                                                 " & vbNewLine _
                                         & "        ELSE DEST.AD_1                                                                    " & vbNewLine _
                                         & "   END                   AS AD_1                                                           " & vbNewLine _
                                         & "	--★変更END 要望番号376①★				                                        " & vbNewLine _
                                         & "	--(1)				                                                                 " & vbNewLine _
                                         & " ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1          " & vbNewLine _
                                         & "	--(2)				                                                                 " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1          " & vbNewLine _
                                         & "	--(3)NULL,「=''」でいいのか確認				                                                                 " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST04.AD_1 IS NOT NULL  AND DEST04.AD_1 <> '') THEN DEST04.AD_1                    " & vbNewLine _
                                         & "    --(4)                                                                                 " & vbNewLine _
                                         & "       ELSE DEST1.AD_1                                                                    " & vbNewLine _
                                         & "  END                      AS AD_1_a                                                      " & vbNewLine _
                                         & " ,UNSO.UNSO_NO_L           AS UNSO_NO_L" & vbNewLine _
                                         & " ,UNSO.INOUTKA_NO_L        AS INOUTKA_NO_L" & vbNewLine _
                                         & " ,UNCHIN.REMARK            AS REMARK" & vbNewLine _
                                         & " ,UNCHIN.SEIQ_FIXED_FLAG   AS SEIQ_FIXED_FLAG" & vbNewLine _
                                         & " ,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & "       ELSE UNCHIN.CUST_CD_M                       " & vbNewLine _
                                         & "  END                      AS CUST_CD_M " & vbNewLine _
                                         & " ,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & "       ELSE UNCHIN.CUST_CD_S                       " & vbNewLine _
                                         & "  END                      AS CUST_CD_S            " & vbNewLine _
                                         & " ,CASE WHEN @CUST_CD_M = '' THEN ''          " & vbNewLine _
                                         & "       ELSE UNCHIN.CUST_CD_SS                      " & vbNewLine _
                                         & "  END                      AS CUST_CD_SS           " & vbNewLine _
                                         & " ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD        " & vbNewLine _
                                         & "       WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD        " & vbNewLine _
                                         & "       ELSE UNSO.DEST_CD                                                              " & vbNewLine _
                                         & "  END                      AS DEST_CD                                                    " & vbNewLine _
                                         & " ,UNCHIN.NRS_BR_CD         AS NRS_BR_CD" & vbNewLine _
                                         & " ,UNCHIN.SEIQ_GROUP_NO_M   AS SEIQ_GROUP_NO_M" & vbNewLine _
                                         & " ,UNSOM.UNSO_NO_M          AS UNSO_NO_M" & vbNewLine _
                                         & " ,KBN01.KBN_NM1         AS MOTO_DATA_NM" & vbNewLine _
                                         & " --LMF531対応 2012/06/12                                                                                             " & vbNewLine _
                                         & " , UNSO.JIYU_KB          AS JIYU_KB                                                                                  " & vbNewLine _
                                         & " , UNSO.ARR_PLAN_DATE    AS ARR_PLAN_DATE                                                                            " & vbNewLine _
                                         & " , UNSO.ORIG_CD          AS NYUUKA_CD                                                                                " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST03.DEST_NM IS NOT NULL AND DEST03.DEST_NM <> '') THEN DEST03.DEST_NM  " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST04.DEST_NM IS NOT NULL AND DEST04.DEST_NM <> '') THEN DEST04.DEST_NM  " & vbNewLine _
                                         & "        WHEN (DEST03.DEST_NM IS NOT NULL AND DEST03.DEST_NM <> '') THEN DEST03.DEST_NM                               " & vbNewLine _
                                         & "        ELSE DEST04.DEST_NM                                                                                          " & vbNewLine _
                                         & "   END                   AS NYUUKA_NM                                                                                " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.AD_1           " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '10' AND (DEST04.AD_1 IS NOT NULL AND DEST04.AD_1 <> '') THEN DEST04.AD_1           " & vbNewLine _
                                         & "        WHEN (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.AD_1                                        " & vbNewLine _
                                         & "        ELSE DEST04.AD_1                                                                                             " & vbNewLine _
                                         & "   END                   AS NYUUKA_AD_1                                                                              " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST03.AD_1 IS NOT NULL AND DEST03.AD_1 <> '') THEN DEST03.JIS           " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '10'  AND (DEST04.AD_1 IS NOT NULL AND DEST04.AD_1 <> '') THEN DEST04.JIS           " & vbNewLine _
                                         & "        WHEN (DEST03.JIS IS NOT NULL AND DEST03.JIS <> '') THEN DEST03.JIS                                           " & vbNewLine _
                                         & "   ELSE DEST04.JIS                                                                                                   " & vbNewLine _
                                         & "   END                   AS NYUUKA_JIS                                                                               " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD                                      " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                      " & vbNewLine _
                                         & "   ELSE UNSO.DEST_CD                                                                                                 " & vbNewLine _
                                         & "   END                   AS SYUKKA_CD                                                                                " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                      " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                      " & vbNewLine _
                                         & "        WHEN (DEST.DEST_NM IS NOT NULL AND DEST.DEST_NM <> '') THEN DEST.DEST_NM                                     " & vbNewLine _
                                         & "        ELSE DEST2.DEST_NM                                                                                           " & vbNewLine _
                                         & "  END                   AS SYUKKA_NM                                                                                 " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                    " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                    " & vbNewLine _
                                         & "        WHEN (DEST.AD_1 IS NOT NULL AND DEST.AD_1 <> '') THEN DEST.AD_1                                              " & vbNewLine _
                                         & "        ELSE DEST2.AD_1                                                                                              " & vbNewLine _
                                         & "   END                   AS SYUKKA_AD_1                                                                              " & vbNewLine _
                                         & " , CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN DESTOUTKA.JIS                                     " & vbNewLine _
                                         & "        WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_JIS_CD                                  " & vbNewLine _
                                         & "        WHEN (DEST.JIS IS NOT NULL AND DEST.JIS <> '') THEN DEST.JIS                                                 " & vbNewLine _
                                         & "        ELSE DEST2.JIS                                                                                               " & vbNewLine _
                                         & "   END                   AS SYUKKA_JIS                                                                               " & vbNewLine _
                                         & " --LMF535対応 2018/06/22 Annen upd start                                                                             " & vbNewLine _
                                         & " ,UNSO.UNSO_PKG_NB       AS DECI_PKG_NB                                                                              " & vbNewLine _
                                         & " --LMF535対応 2018/06/22 Annen upd start                                                                             " & vbNewLine

    'LMF535対応 2018/06/22 Annen add end

#End Region

#Region "FROM句"

#Region "FROM句_標準"

    Private Const SQL_FROM As String = "FROM                                                         " & vbNewLine _
                                           & "	  $LM_TRN$..F_UNCHIN_TRS AS UNCHIN            		 " & vbNewLine _
                                           & "	 --運送L                                     		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO         		 " & vbNewLine _
                                           & "	 ON UNCHIN.UNSO_NO_L=UNSO.UNSO_NO_L         		 " & vbNewLine _
                                           & "  AND UNCHIN.CUST_CD_L =UNSO.CUST_CD_L          		 " & vbNewLine _
                                           & "  AND UNCHIN.CUST_CD_M =UNSO.CUST_CD_M          		 " & vbNewLine _
                                           & "	 AND UNSO.SYS_DEL_FLG='0'                 			 " & vbNewLine _
                                           & "	 --運送M                                     		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM         		 " & vbNewLine _
                                           & "	 ON UNCHIN.UNSO_NO_L =UNSOM.UNSO_NO_L     			 " & vbNewLine _
                                           & "	 AND UNCHIN.UNSO_NO_M =UNSOM.UNSO_NO_M     			 " & vbNewLine _
                                           & "	 AND UNSOM.SYS_DEL_FLG='0'                 			 " & vbNewLine _
                                           & "	--出荷L				                                 " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..C_OUTKA_L AS OUTL				 " & vbNewLine _
                                           & "	ON  OUTL.NRS_BR_CD=UNSO.NRS_BR_CD				     " & vbNewLine _
                                           & "	AND OUTL.OUTKA_NO_L=UNSO.INOUTKA_NO_L			     " & vbNewLine _
                                           & "  AND OUTL.SYS_DEL_FLG='0'				             " & vbNewLine _
                                           & "	--出荷EDIL			                                 " & vbNewLine _
                                           & "  LEFT JOIN                                            " & vbNewLine _
                                           & "  (SELECT                                              " & vbNewLine _
                                           & "    NRS_BR_CD                                          " & vbNewLine _
                                           & "   ,OUTKA_CTL_NO                                       " & vbNewLine _
                                           & "   ,MIN(DEST_CD)     AS DEST_CD                        " & vbNewLine _
                                           & "   ,MIN(DEST_NM)     AS DEST_NM                        " & vbNewLine _
                                           & "   ,MIN(DEST_AD_1)   AS DEST_AD_1                      " & vbNewLine _
                                           & "   --20160607 要番2565 tsunehira add                   " & vbNewLine _
                                           & "   --,DEST_JIS_CD                                      " & vbNewLine _
                                           & "   ,MAX(DEST_JIS_CD) AS DEST_JIS_CD                    " & vbNewLine _
                                           & "   --20160607 要番2565 tsunehira end                   " & vbNewLine _
                                           & "   ,SYS_DEL_FLG                                        " & vbNewLine _
                                           & "   FROM                                                " & vbNewLine _
                                           & "    $LM_TRN$..H_OUTKAEDI_L                             " & vbNewLine _
                                           & "   WHERE                                               " & vbNewLine _
                                           & "    NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                           & "    AND CUST_CD_L = @CUST_CD_L                         " & vbNewLine _
                                           & "   GROUP BY                                            " & vbNewLine _
                                           & "    NRS_BR_CD                                          " & vbNewLine _
                                           & "   ,OUTKA_CTL_NO                                       " & vbNewLine _
                                           & "   --,DEST_JIS_CD --20160607 要番2565 tsunehira        " & vbNewLine _
                                           & "   ,SYS_DEL_FLG                                        " & vbNewLine _
                                           & "   ) EDIL                                              " & vbNewLine _
                                           & "  ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                  " & vbNewLine _
                                           & "  AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L              " & vbNewLine _
                                           & "  AND EDIL.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                           & "	  --商品マスタ                                       " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_GOODS AS GOODS                " & vbNewLine _
                                           & "	 ON UNCHIN.NRS_BR_CD=GOODS.NRS_BR_CD                 " & vbNewLine _
                                           & "	 AND UNSOM.GOODS_CD_NRS=GOODS.GOODS_CD_NRS           " & vbNewLine _
                                           & "	 --荷主マスタ                                        " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_CUST AS CUST                  " & vbNewLine _
                                           & "	 ON GOODS.CUST_CD_L=CUST.CUST_CD_L                   " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_M = CUST.CUST_CD_M                " & vbNewLine _
                                           & "	 AND GOODS.NRS_BR_CD=CUST.NRS_BR_CD                  " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_S= CUST.CUST_CD_S                 " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_SS= CUST.CUST_CD_SS               " & vbNewLine _
                                           & "   --荷主マスタ                                        " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_CUST AS CUST_F                " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD   = CUST_F.NRS_BR_CD            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_L  = CUST_F.CUST_CD_L            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_M  = CUST_F.CUST_CD_M            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_S  = CUST_F.CUST_CD_S            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_SS = CUST_F.CUST_CD_SS           " & vbNewLine _
                                           & "	 --営業所マスタ                                      " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_NRS_BR AS NRS                 " & vbNewLine _
                                           & "	 ON UNCHIN.NRS_BR_CD = NRS.NRS_BR_CD                 " & vbNewLine _
                                           & "	 --運賃タリフマスタ                         		 " & vbNewLine _
                                           & "   LEFT JOIN (                                         " & vbNewLine _
                                           & "       SELECT NRS_BR_CD,UNCHIN_TARIFF_CD,UNCHIN_TARIFF_REM,ROW_NUMBER()OVER(PARTITION BY NRS_BR_CD,UNCHIN_TARIFF_CD ORDER BY NRS_BR_CD,UNCHIN_TARIFF_CD,STR_DATE DESC) AS ROWNUM FROM $LM_MST$..M_UNCHIN_TARIFF " & vbNewLine _
                                           & "       WHERE UNCHIN_TARIFF_CD_EDA='000' AND STR_DATE <=@SYS_DATE " & vbNewLine _
                                           & "   )AS TARIFF                                          " & vbNewLine _
                                           & "   ON UNCHIN.SEIQ_TARIFF_CD=TARIFF.UNCHIN_TARIFF_CD    " & vbNewLine _
                                           & "   AND  UNCHIN.NRS_BR_CD=TARIFF.NRS_BR_CD              " & vbNewLine _
                                           & "   AND TARIFF.ROWNUM='1'                               " & vbNewLine _
                                           & "   --横持ちタリフマスタ                                " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_YOKO_TARIFF_HD AS YOKO        " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD=YOKO.NRS_BR_CD                  " & vbNewLine _
                                           & "   AND UNCHIN.SEIQ_TARIFF_CD=YOKO.YOKO_TARIFF_CD       " & vbNewLine _
                                           & "	 --運送会社                                 		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_UNSOCO AS UNSOCO     		 " & vbNewLine _
                                           & "	 ON UNSO.NRS_BR_CD=UNSOCO.NRS_BR_CD         		 " & vbNewLine _
                                           & "	 AND UNSO.UNSO_CD=UNSOCO.UNSOCO_CD         			 " & vbNewLine _
                                           & "	 AND UNSO.UNSO_BR_CD =UNSOCO.UNSOCO_BR_CD    		 " & vbNewLine _
                                           & "	 --届け先マスタ                             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DEST         		 " & vbNewLine _
                                           & "	 ON UNSO.DEST_CD=DEST.DEST_CD             			 " & vbNewLine _
                                           & "	 AND UNSO.NRS_BR_CD =DEST.NRS_BR_CD         		 " & vbNewLine _
                                           & "	 AND UNSO.CUST_CD_L = DEST.CUST_CD_L         		 " & vbNewLine _
                                           & "	--★追加START 要望番号376②★				         " & vbNewLine _
                                           & "	--届先マスタ				                         " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST2				     " & vbNewLine _
                                           & "	ON UNSO.DEST_CD   = DEST2.DEST_CD				     " & vbNewLine _
                                           & "	AND UNSO.NRS_BR_CD = DEST2.NRS_BR_CD				 " & vbNewLine _
                                           & "	AND DEST2.CUST_CD_L = 'ZZZZZ'				         " & vbNewLine _
                                           & "	--★追加END 要望番号376②★				             " & vbNewLine _
                                           & "	 --届け先マスタ（出荷L参照）                   		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DESTOUTKA   			 " & vbNewLine _
                                           & "	 ON OUTL.DEST_CD=DESTOUTKA.DEST_CD             		 " & vbNewLine _
                                           & "	 AND OUTL.NRS_BR_CD =DESTOUTKA.NRS_BR_CD         	 " & vbNewLine _
                                           & "	 AND OUTL.CUST_CD_L = DESTOUTKA.CUST_CD_L         	 " & vbNewLine _
                                           & "	 --請求先マスタ                             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQTO     		 " & vbNewLine _
                                           & "	 ON UNCHIN.SEIQTO_CD = SEIQTO.SEIQTO_CD     		 " & vbNewLine _
                                           & "	 AND UNCHIN.NRS_BR_CD =SEIQTO.NRS_BR_CD     		 " & vbNewLine _
                                           & "	 --届け先マスタ、荷主コード固定             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DEST1         		 " & vbNewLine _
                                           & "	 ON UNSO.DEST_CD=DEST1.DEST_CD             			 " & vbNewLine _
                                           & "	 AND UNCHIN.NRS_BR_CD =DEST1.NRS_BR_CD     			 " & vbNewLine _
                                           & "	 AND 'zzzzz' = DEST1.CUST_CD_L             			 " & vbNewLine _
                                           & "	--運賃での荷主帳票パターン取得                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                  " & vbNewLine _
                                           & "	ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                " & vbNewLine _
                                           & "	AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                " & vbNewLine _
                                           & "	AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                " & vbNewLine _
                                           & "	AND '00' = MCR1.CUST_CD_S                            " & vbNewLine _
                                           & "	AND MCR1.PTN_ID = '48'                               " & vbNewLine _
                                           & "	--帳票パターン取得                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR1                        " & vbNewLine _
                                           & "	ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                   " & vbNewLine _
                                           & "	AND MR1.PTN_ID = MCR1.PTN_ID                         " & vbNewLine _
                                           & "	AND MR1.PTN_CD = MCR1.PTN_CD                         " & vbNewLine _
                                           & "  AND MR1.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                           & "	--商品Mの荷主での荷主帳票パターン取得                " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                  " & vbNewLine _
                                           & "	ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                 " & vbNewLine _
                                           & "	AND MCR2.PTN_ID = '48'                               " & vbNewLine _
                                           & "	--帳票パターン取得                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR2                        " & vbNewLine _
                                           & "	ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                   " & vbNewLine _
                                           & "	AND MR2.PTN_ID = MCR2.PTN_ID                         " & vbNewLine _
                                           & "	AND MR2.PTN_CD = MCR2.PTN_CD                         " & vbNewLine _
                                           & "  AND MR2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                           & "	--存在しない場合の帳票パターン取得                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR3                        " & vbNewLine _
                                           & "	ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                 " & vbNewLine _
                                           & "	AND MR3.PTN_ID = '48'                                " & vbNewLine _
                                           & "	AND MR3.STANDARD_FLAG = '01'                     	 " & vbNewLine _
                                           & "  AND MR3.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                           & " --721 START                                           " & vbNewLine _
                                           & " --元データ区分名取得用(KBN01)                         " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..Z_KBN KBN01                      " & vbNewLine _
                                           & "	ON  KBN01.KBN_GROUP_CD = 'M004'                      " & vbNewLine _
                                           & "	AND KBN01.KBN_CD = UNSO.MOTO_DATA_KB                 " & vbNewLine _
                                           & "  AND KBN01.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                           & "  --① (F_UNSO_L) CUST_CD_Lで届先Mに存在する場合       " & vbNewLine _
                                           & "  LEFT JOIN $LM_MST$..M_DEST AS DEST03                 " & vbNewLine _
                                           & "  ON  UNSO.ORIG_CD   = DEST03.DEST_CD                  " & vbNewLine _
                                           & "  AND UNSO.NRS_BR_CD = DEST03.NRS_BR_CD                " & vbNewLine _
                                           & "  AND UNSO.CUST_CD_L = DEST03.CUST_CD_L                " & vbNewLine _
                                           & "  --②①で存在せず、(M_DEST) CUST_CD_L='ZZZZZ'で届先Mに存在する場合" & vbNewLine _
                                           & "  LEFT JOIN $LM_MST$..M_DEST AS DEST04                 " & vbNewLine _
                                           & "  ON  UNSO.ORIG_CD = DEST04.DEST_CD                    " & vbNewLine _
                                           & "  AND UNSO.NRS_BR_CD = DEST04.NRS_BR_CD                " & vbNewLine _
                                           & "  AND DEST04.CUST_CD_L = 'ZZZZZ'                       " & vbNewLine _
                                           & " --721 END                                             " & vbNewLine _
                                           & "	WHERE                                             	 " & vbNewLine _
                                           & "	UNCHIN.NRS_BR_CD= @NRS_BR_CD                         " & vbNewLine _
                                           & "	AND UNCHIN.SYS_DEL_FLG='0'                           " & vbNewLine _
                                           '& "	AND UNCHIN.SEIQ_FIXED_FLAG='01'                             " & vbNewLine

#End Region

    '---> LMF531対応 2012/06/12
#Region "FROM句_LMF531"

    Private Const SQL_FROM_LMF531 As String = "FROM                                                  " & vbNewLine _
                                           & "	  $LM_TRN$..F_UNCHIN_TRS AS UNCHIN            		 " & vbNewLine _
                                           & "	 --運送L                                     		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO         		 " & vbNewLine _
                                           & "	 ON UNCHIN.UNSO_NO_L=UNSO.UNSO_NO_L         		 " & vbNewLine _
                                           & "  AND UNCHIN.CUST_CD_L =UNSO.CUST_CD_L          		 " & vbNewLine _
                                           & "  AND UNCHIN.CUST_CD_M =UNSO.CUST_CD_M          		 " & vbNewLine _
                                           & "	 AND UNSO.SYS_DEL_FLG='0'                 			 " & vbNewLine _
                                           & "	 --運送M                                     		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM         		 " & vbNewLine _
                                           & "	 ON UNCHIN.UNSO_NO_L =UNSOM.UNSO_NO_L     			 " & vbNewLine _
                                           & "	 AND UNCHIN.UNSO_NO_M =UNSOM.UNSO_NO_M     			 " & vbNewLine _
                                           & "	 AND UNSOM.SYS_DEL_FLG='0'                 			 " & vbNewLine _
                                           & "	--出荷L				                                      " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..C_OUTKA_L AS OUTL				      " & vbNewLine _
                                           & "	ON  OUTL.NRS_BR_CD=UNSO.NRS_BR_CD				          " & vbNewLine _
                                           & "	AND OUTL.OUTKA_NO_L=UNSO.INOUTKA_NO_L			          " & vbNewLine _
                                           & "  AND OUTL.SYS_DEL_FLG='0'				                  " & vbNewLine _
                                           & "	--出荷EDIL			                                      " & vbNewLine _
                                           & "  LEFT JOIN                                                                    " & vbNewLine _
                                           & "  (SELECT                                                                      " & vbNewLine _
                                           & "    NRS_BR_CD                                                                  " & vbNewLine _
                                           & "   ,OUTKA_CTL_NO                                                               " & vbNewLine _
                                           & "   ,MIN(DEST_CD)    AS DEST_CD                                                 " & vbNewLine _
                                           & "   ,MIN(DEST_NM)    AS DEST_NM                                                 " & vbNewLine _
                                           & "   ,MIN(DEST_AD_1)  AS DEST_AD_1                                               " & vbNewLine _
                                           & "   ,DEST_JIS_CD                                                                " & vbNewLine _
                                           & "   ,SYS_DEL_FLG                                                                " & vbNewLine _
                                           & "   FROM                                                                        " & vbNewLine _
                                           & "    $LM_TRN$..H_OUTKAEDI_L                                                     " & vbNewLine _
                                           & "   WHERE                                                                       " & vbNewLine _
                                           & "    NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                           & "    AND CUST_CD_L = @CUST_CD_L                                                 " & vbNewLine _
                                           & "   GROUP BY                                                                    " & vbNewLine _
                                           & "    NRS_BR_CD                                                                  " & vbNewLine _
                                           & "   ,OUTKA_CTL_NO                                                               " & vbNewLine _
                                           & "   ,DEST_JIS_CD                                                                " & vbNewLine _
                                           & "   ,SYS_DEL_FLG                                                                " & vbNewLine _
                                           & "   ) EDIL                                                                      " & vbNewLine _
                                           & "  ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                           & "  AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                      " & vbNewLine _
                                           & "  AND EDIL.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                           & "	  --商品マスタ                                       " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_GOODS AS GOODS                  " & vbNewLine _
                                           & "	 ON UNCHIN.NRS_BR_CD=GOODS.NRS_BR_CD                 " & vbNewLine _
                                           & "	 AND UNSOM.GOODS_CD_NRS=GOODS.GOODS_CD_NRS           " & vbNewLine _
                                           & "	 --荷主マスタ                                        " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_CUST AS CUST                    " & vbNewLine _
                                           & "	 ON GOODS.CUST_CD_L=CUST.CUST_CD_L                   " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_M = CUST.CUST_CD_M                " & vbNewLine _
                                           & "	 AND GOODS.NRS_BR_CD=CUST.NRS_BR_CD                  " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_S= CUST.CUST_CD_S                 " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_SS= CUST.CUST_CD_SS               " & vbNewLine _
                                           & "   --荷主マスタ                                        " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_CUST AS CUST_F                " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD   = CUST_F.NRS_BR_CD            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_L  = CUST_F.CUST_CD_L            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_M  = CUST_F.CUST_CD_M            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_S  = CUST_F.CUST_CD_S            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_SS = CUST_F.CUST_CD_SS           " & vbNewLine _
                                           & "	 --営業所マスタ                                      " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_NRS_BR AS NRS                 " & vbNewLine _
                                           & "	 ON UNCHIN.NRS_BR_CD = NRS.NRS_BR_CD                 " & vbNewLine _
                                           & "	 --運賃タリフマスタ                         		 " & vbNewLine _
                                           & "   LEFT JOIN (                                         " & vbNewLine _
                                           & "       SELECT NRS_BR_CD,UNCHIN_TARIFF_CD,UNCHIN_TARIFF_REM,ROW_NUMBER()OVER(PARTITION BY NRS_BR_CD,UNCHIN_TARIFF_CD ORDER BY NRS_BR_CD,UNCHIN_TARIFF_CD,STR_DATE DESC) AS ROWNUM FROM $LM_MST$..M_UNCHIN_TARIFF " & vbNewLine _
                                           & "       WHERE UNCHIN_TARIFF_CD_EDA='000' AND STR_DATE <=@SYS_DATE " & vbNewLine _
                                           & "   )AS TARIFF                                           " & vbNewLine _
                                           & "   ON UNCHIN.SEIQ_TARIFF_CD=TARIFF.UNCHIN_TARIFF_CD     " & vbNewLine _
                                           & "   AND TARIFF.ROWNUM='1'                                " & vbNewLine _
                                           & "   --横持ちタリフマスタ                                 " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_YOKO_TARIFF_HD AS YOKO           " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD=YOKO.NRS_BR_CD                   " & vbNewLine _
                                           & "   AND UNCHIN.SEIQ_TARIFF_CD=YOKO.YOKO_TARIFF_CD        " & vbNewLine _
                                           & "	 --運送会社                                 		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_UNSOCO AS UNSOCO     			 " & vbNewLine _
                                           & "	 ON UNSO.NRS_BR_CD=UNSOCO.NRS_BR_CD         		 " & vbNewLine _
                                           & "	 AND UNSO.UNSO_CD=UNSOCO.UNSOCO_CD         			 " & vbNewLine _
                                           & "	 AND UNSO.UNSO_BR_CD =UNSOCO.UNSOCO_BR_CD    		 " & vbNewLine _
                                           & "	 --届け先マスタ                             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DEST         			 " & vbNewLine _
                                           & "	 ON UNSO.DEST_CD=DEST.DEST_CD             			 " & vbNewLine _
                                           & "	 AND UNSO.NRS_BR_CD =DEST.NRS_BR_CD         		 " & vbNewLine _
                                           & "	 AND UNSO.CUST_CD_L = DEST.CUST_CD_L         		 " & vbNewLine _
                                           & "	--★追加START 要望番号376②★				         " & vbNewLine _
                                           & "	--届先マスタ				                         " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST2				     " & vbNewLine _
                                           & "	ON UNSO.DEST_CD   = DEST2.DEST_CD				     " & vbNewLine _
                                           & "	AND UNSO.NRS_BR_CD = DEST2.NRS_BR_CD				 " & vbNewLine _
                                           & "	AND DEST2.CUST_CD_L = 'ZZZZZ'				         " & vbNewLine _
                                           & "	--★追加END 要望番号376②★				             " & vbNewLine _
                                           & "	 --届け先マスタ（出荷L参照）                   		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DESTOUTKA   			 " & vbNewLine _
                                           & "	 ON OUTL.DEST_CD=DESTOUTKA.DEST_CD             			 " & vbNewLine _
                                           & "	 AND OUTL.NRS_BR_CD =DESTOUTKA.NRS_BR_CD         		 " & vbNewLine _
                                           & "	 AND OUTL.CUST_CD_L = DESTOUTKA.CUST_CD_L         		 " & vbNewLine _
                                           & "	 --請求先マスタ                             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQTO     			 " & vbNewLine _
                                           & "	 ON UNCHIN.SEIQTO_CD = SEIQTO.SEIQTO_CD     		 " & vbNewLine _
                                           & "	 AND UNCHIN.NRS_BR_CD =SEIQTO.NRS_BR_CD     		 " & vbNewLine _
                                           & "	 --届け先マスタ、荷主コード固定             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DEST1         			 " & vbNewLine _
                                           & "	 ON UNSO.DEST_CD=DEST1.DEST_CD             			 " & vbNewLine _
                                           & "	 AND UNCHIN.NRS_BR_CD =DEST1.NRS_BR_CD     			 " & vbNewLine _
                                           & "	 AND 'zzzzz' = DEST1.CUST_CD_L             			 " & vbNewLine _
                                           & "	--運賃での荷主帳票パターン取得                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                    " & vbNewLine _
                                           & "	ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                " & vbNewLine _
                                           & "	AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                " & vbNewLine _
                                           & "	AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                " & vbNewLine _
                                           & "	AND '00' = MCR1.CUST_CD_S                            " & vbNewLine _
                                           & "	AND MCR1.PTN_ID = '48'                               " & vbNewLine _
                                           & "	--帳票パターン取得                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR1                          " & vbNewLine _
                                           & "	ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                   " & vbNewLine _
                                           & "	AND MR1.PTN_ID = MCR1.PTN_ID                         " & vbNewLine _
                                           & "	AND MR1.PTN_CD = MCR1.PTN_CD                         " & vbNewLine _
                                           & "  AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                           & "	--商品Mの荷主での荷主帳票パターン取得                " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                    " & vbNewLine _
                                           & "	ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                 " & vbNewLine _
                                           & "	AND MCR2.PTN_ID = '48'                               " & vbNewLine _
                                           & "	--帳票パターン取得                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR2                          " & vbNewLine _
                                           & "	ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                   " & vbNewLine _
                                           & "	AND MR2.PTN_ID = MCR2.PTN_ID                         " & vbNewLine _
                                           & "	AND MR2.PTN_CD = MCR2.PTN_CD                         " & vbNewLine _
                                           & "  AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                           & "	--存在しない場合の帳票パターン取得                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR3                          " & vbNewLine _
                                           & "	ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                 " & vbNewLine _
                                           & "	AND MR3.PTN_ID = '48'                                " & vbNewLine _
                                           & "	AND MR3.STANDARD_FLAG = '01'                     	 " & vbNewLine _
                                           & "  AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                           & " --721 START                                " & vbNewLine _
                                           & " --元データ区分名取得用(KBN01)                  " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..Z_KBN KBN01               " & vbNewLine _
                                           & "	ON  KBN01.KBN_GROUP_CD = 'M004'               " & vbNewLine _
                                           & "	AND KBN01.KBN_CD = UNSO.MOTO_DATA_KB          " & vbNewLine _
                                           & "  AND KBN01.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                           & "  --① (F_UNSO_L) CUST_CD_Lで届先Mに存在する場合" & vbNewLine _
                                           & "  LEFT JOIN $LM_MST$..M_DEST AS DEST03          " & vbNewLine _
                                           & "  ON  UNSO.ORIG_CD   = DEST03.DEST_CD               " & vbNewLine _
                                           & "  AND UNSO.NRS_BR_CD = DEST03.NRS_BR_CD         " & vbNewLine _
                                           & "  AND UNSO.CUST_CD_L = DEST03.CUST_CD_L         " & vbNewLine _
                                           & "  --②①で存在せず、(M_DEST) CUST_CD_L='ZZZZZ'で届先Mに存在する場合" & vbNewLine _
                                           & "  LEFT JOIN $LM_MST$..M_DEST AS DEST04          " & vbNewLine _
                                           & "  ON  UNSO.ORIG_CD = DEST04.DEST_CD                 " & vbNewLine _
                                           & "  AND UNSO.NRS_BR_CD = DEST04.NRS_BR_CD         " & vbNewLine _
                                           & "  AND DEST04.CUST_CD_L = 'ZZZZZ'               " & vbNewLine _
                                           & " --721 END                                      " & vbNewLine _
                                           & "	WHERE                                             	 " & vbNewLine _
                                           & "	UNCHIN.NRS_BR_CD= @NRS_BR_CD                       " & vbNewLine _
                                           & "	AND UNCHIN.SYS_DEL_FLG='0'                             " & vbNewLine _
                                           & "    AND UNSO.MOTO_DATA_KB <> '10'                      " & vbNewLine _
                                           & "    AND UNSO.MOTO_DATA_KB <> '20'                      " & vbNewLine _
                                           & "    AND (UNSO.JIYU_KB ='02' OR UNSO.JIYU_KB ='03')     " & vbNewLine

#End Region
    '<--- LMF531対応 2012/06/12

    'LMF535対応 2018/06/22 Annen add start
    'Memo)
    'SQL_FROMのコピー
    '内部で「LMF535対応 2018/06/22 Annen」と記述されているところが、コピー後変更した部分

#Region "LMF535"
    ''' <summary>
    ''' LMF535用のFROM区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_LMF535 As String = "FROM                                                         " & vbNewLine _
                                           & "	  $LM_TRN$..F_UNCHIN_TRS AS UNCHIN            		 " & vbNewLine _
                                           & "	 --運送L                                     		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO         		 " & vbNewLine _
                                           & "	 ON UNCHIN.UNSO_NO_L=UNSO.UNSO_NO_L         		 " & vbNewLine _
                                           & "  AND UNCHIN.CUST_CD_L =UNSO.CUST_CD_L          		 " & vbNewLine _
                                           & "  AND UNCHIN.CUST_CD_M =UNSO.CUST_CD_M          		 " & vbNewLine _
                                           & "	 AND UNSO.SYS_DEL_FLG='0'                 			 " & vbNewLine _
                                           & "	 --運送M                                     		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM         		 " & vbNewLine _
                                           & "	 ON UNCHIN.UNSO_NO_L =UNSOM.UNSO_NO_L     			 " & vbNewLine _
                                           & "	 AND UNCHIN.UNSO_NO_M =UNSOM.UNSO_NO_M     			 " & vbNewLine _
                                           & "	 AND UNSOM.SYS_DEL_FLG='0'                 			 " & vbNewLine _
                                           & " --LMF535対応 2018/06/22 Annen add start               " & vbNewLine _
                                           & "  --運送M(個数算出用)                                  " & vbNewLine _
                                           & "  LEFT JOIN LM_TRN_40..F_UNSO_M AS UNSOM_KOSU          " & vbNewLine _
                                           & "  ON  UNSO.NRS_BR_CD = UNSOM_KOSU.NRS_BR_CD            " & vbNewLine _
                                           & "  AND UNSO.UNSO_NO_L = UNSOM_KOSU.UNSO_NO_L            " & vbNewLine _
                                           & "  AND UNSOM_KOSU.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                           & " --LMF535対応 2018/06/22 Annen add end                 " & vbNewLine _
                                           & "	--出荷L				                                 " & vbNewLine _
                                           & "	LEFT JOIN $LM_TRN$..C_OUTKA_L AS OUTL				 " & vbNewLine _
                                           & "	ON  OUTL.NRS_BR_CD=UNSO.NRS_BR_CD				     " & vbNewLine _
                                           & "	AND OUTL.OUTKA_NO_L=UNSO.INOUTKA_NO_L			     " & vbNewLine _
                                           & "  AND OUTL.SYS_DEL_FLG='0'				             " & vbNewLine _
                                           & "	--出荷EDIL			                                 " & vbNewLine _
                                           & "  LEFT JOIN                                            " & vbNewLine _
                                           & "  (SELECT                                              " & vbNewLine _
                                           & "    NRS_BR_CD                                          " & vbNewLine _
                                           & "   ,OUTKA_CTL_NO                                       " & vbNewLine _
                                           & "   ,MIN(DEST_CD)     AS DEST_CD                        " & vbNewLine _
                                           & "   ,MIN(DEST_NM)     AS DEST_NM                        " & vbNewLine _
                                           & "   ,MIN(DEST_AD_1)   AS DEST_AD_1                      " & vbNewLine _
                                           & "   --20160607 要番2565 tsunehira add                   " & vbNewLine _
                                           & "   --,DEST_JIS_CD                                      " & vbNewLine _
                                           & "   ,MAX(DEST_JIS_CD) AS DEST_JIS_CD                    " & vbNewLine _
                                           & "   --20160607 要番2565 tsunehira end                   " & vbNewLine _
                                           & "   ,SYS_DEL_FLG                                        " & vbNewLine _
                                           & "   FROM                                                " & vbNewLine _
                                           & "    $LM_TRN$..H_OUTKAEDI_L                             " & vbNewLine _
                                           & "   WHERE                                               " & vbNewLine _
                                           & "    NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                           & "    AND CUST_CD_L = @CUST_CD_L                         " & vbNewLine _
                                           & "   GROUP BY                                            " & vbNewLine _
                                           & "    NRS_BR_CD                                          " & vbNewLine _
                                           & "   ,OUTKA_CTL_NO                                       " & vbNewLine _
                                           & "   --,DEST_JIS_CD --20160607 要番2565 tsunehira        " & vbNewLine _
                                           & "   ,SYS_DEL_FLG                                        " & vbNewLine _
                                           & "   ) EDIL                                              " & vbNewLine _
                                           & "  ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                  " & vbNewLine _
                                           & "  AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L              " & vbNewLine _
                                           & "  AND EDIL.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                           & "	  --商品マスタ                                       " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_GOODS AS GOODS                " & vbNewLine _
                                           & "	 ON UNCHIN.NRS_BR_CD=GOODS.NRS_BR_CD                 " & vbNewLine _
                                           & "	 AND UNSOM.GOODS_CD_NRS=GOODS.GOODS_CD_NRS           " & vbNewLine _
                                           & "	 --荷主マスタ                                        " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_CUST AS CUST                  " & vbNewLine _
                                           & "	 ON GOODS.CUST_CD_L=CUST.CUST_CD_L                   " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_M = CUST.CUST_CD_M                " & vbNewLine _
                                           & "	 AND GOODS.NRS_BR_CD=CUST.NRS_BR_CD                  " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_S= CUST.CUST_CD_S                 " & vbNewLine _
                                           & "	 AND GOODS.CUST_CD_SS= CUST.CUST_CD_SS               " & vbNewLine _
                                           & "   --荷主マスタ                                        " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_CUST AS CUST_F                " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD   = CUST_F.NRS_BR_CD            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_L  = CUST_F.CUST_CD_L            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_M  = CUST_F.CUST_CD_M            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_S  = CUST_F.CUST_CD_S            " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_SS = CUST_F.CUST_CD_SS           " & vbNewLine _
                                           & "	 --営業所マスタ                                      " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_NRS_BR AS NRS                 " & vbNewLine _
                                           & "	 ON UNCHIN.NRS_BR_CD = NRS.NRS_BR_CD                 " & vbNewLine _
                                           & "	 --運賃タリフマスタ                         		 " & vbNewLine _
                                           & "   LEFT JOIN (                                         " & vbNewLine _
                                           & "       SELECT NRS_BR_CD,UNCHIN_TARIFF_CD,UNCHIN_TARIFF_REM,ROW_NUMBER()OVER(PARTITION BY NRS_BR_CD,UNCHIN_TARIFF_CD ORDER BY NRS_BR_CD,UNCHIN_TARIFF_CD,STR_DATE DESC) AS ROWNUM FROM $LM_MST$..M_UNCHIN_TARIFF " & vbNewLine _
                                           & "       WHERE UNCHIN_TARIFF_CD_EDA='000' AND STR_DATE <=@SYS_DATE " & vbNewLine _
                                           & "   )AS TARIFF                                          " & vbNewLine _
                                           & "   ON UNCHIN.SEIQ_TARIFF_CD=TARIFF.UNCHIN_TARIFF_CD    " & vbNewLine _
                                           & "   AND  UNCHIN.NRS_BR_CD=TARIFF.NRS_BR_CD              " & vbNewLine _
                                           & "   AND TARIFF.ROWNUM='1'                               " & vbNewLine _
                                           & "   --横持ちタリフマスタ                                " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_YOKO_TARIFF_HD AS YOKO        " & vbNewLine _
                                           & "   ON UNCHIN.NRS_BR_CD=YOKO.NRS_BR_CD                  " & vbNewLine _
                                           & "   AND UNCHIN.SEIQ_TARIFF_CD=YOKO.YOKO_TARIFF_CD       " & vbNewLine _
                                           & "	 --運送会社                                 		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_UNSOCO AS UNSOCO     		 " & vbNewLine _
                                           & "	 ON UNSO.NRS_BR_CD=UNSOCO.NRS_BR_CD         		 " & vbNewLine _
                                           & "	 AND UNSO.UNSO_CD=UNSOCO.UNSOCO_CD         			 " & vbNewLine _
                                           & "	 AND UNSO.UNSO_BR_CD =UNSOCO.UNSOCO_BR_CD    		 " & vbNewLine _
                                           & "	 --届け先マスタ                             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DEST         		 " & vbNewLine _
                                           & "	 ON UNSO.DEST_CD=DEST.DEST_CD             			 " & vbNewLine _
                                           & "	 AND UNSO.NRS_BR_CD =DEST.NRS_BR_CD         		 " & vbNewLine _
                                           & "	 AND UNSO.CUST_CD_L = DEST.CUST_CD_L         		 " & vbNewLine _
                                           & "	--★追加START 要望番号376②★				         " & vbNewLine _
                                           & "	--届先マスタ				                         " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_DEST AS DEST2				     " & vbNewLine _
                                           & "	ON UNSO.DEST_CD   = DEST2.DEST_CD				     " & vbNewLine _
                                           & "	AND UNSO.NRS_BR_CD = DEST2.NRS_BR_CD				 " & vbNewLine _
                                           & "	AND DEST2.CUST_CD_L = 'ZZZZZ'				         " & vbNewLine _
                                           & "	--★追加END 要望番号376②★				             " & vbNewLine _
                                           & "	 --届け先マスタ（出荷L参照）                   		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DESTOUTKA   			 " & vbNewLine _
                                           & "	 ON OUTL.DEST_CD=DESTOUTKA.DEST_CD             		 " & vbNewLine _
                                           & "	 AND OUTL.NRS_BR_CD =DESTOUTKA.NRS_BR_CD         	 " & vbNewLine _
                                           & "	 AND OUTL.CUST_CD_L = DESTOUTKA.CUST_CD_L         	 " & vbNewLine _
                                           & "	 --請求先マスタ                             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQTO     		 " & vbNewLine _
                                           & "	 ON UNCHIN.SEIQTO_CD = SEIQTO.SEIQTO_CD     		 " & vbNewLine _
                                           & "	 AND UNCHIN.NRS_BR_CD =SEIQTO.NRS_BR_CD     		 " & vbNewLine _
                                           & "	 --届け先マスタ、荷主コード固定             		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_DEST AS DEST1         		 " & vbNewLine _
                                           & "	 ON UNSO.DEST_CD=DEST1.DEST_CD             			 " & vbNewLine _
                                           & "	 AND UNCHIN.NRS_BR_CD =DEST1.NRS_BR_CD     			 " & vbNewLine _
                                           & "	 AND 'zzzzz' = DEST1.CUST_CD_L             			 " & vbNewLine _
                                           & "	--運賃での荷主帳票パターン取得                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                  " & vbNewLine _
                                           & "	ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                " & vbNewLine _
                                           & "	AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                " & vbNewLine _
                                           & "	AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                " & vbNewLine _
                                           & "	AND '00' = MCR1.CUST_CD_S                            " & vbNewLine _
                                           & "	AND MCR1.PTN_ID = '48'                               " & vbNewLine _
                                           & "	--帳票パターン取得                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR1                        " & vbNewLine _
                                           & "	ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                   " & vbNewLine _
                                           & "	AND MR1.PTN_ID = MCR1.PTN_ID                         " & vbNewLine _
                                           & "	AND MR1.PTN_CD = MCR1.PTN_CD                         " & vbNewLine _
                                           & "  AND MR1.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                           & "	--商品Mの荷主での荷主帳票パターン取得                " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                  " & vbNewLine _
                                           & "	ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                 " & vbNewLine _
                                           & "	AND MCR2.PTN_ID = '48'                               " & vbNewLine _
                                           & "	--帳票パターン取得                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR2                        " & vbNewLine _
                                           & "	ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                   " & vbNewLine _
                                           & "	AND MR2.PTN_ID = MCR2.PTN_ID                         " & vbNewLine _
                                           & "	AND MR2.PTN_CD = MCR2.PTN_CD                         " & vbNewLine _
                                           & "  AND MR2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                           & "	--存在しない場合の帳票パターン取得                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR3                        " & vbNewLine _
                                           & "	ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                 " & vbNewLine _
                                           & "	AND MR3.PTN_ID = '48'                                " & vbNewLine _
                                           & "	AND MR3.STANDARD_FLAG = '01'                     	 " & vbNewLine _
                                           & "  AND MR3.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                           & " --721 START                                           " & vbNewLine _
                                           & " --元データ区分名取得用(KBN01)                         " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..Z_KBN KBN01                      " & vbNewLine _
                                           & "	ON  KBN01.KBN_GROUP_CD = 'M004'                      " & vbNewLine _
                                           & "	AND KBN01.KBN_CD = UNSO.MOTO_DATA_KB                 " & vbNewLine _
                                           & "  AND KBN01.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                           & "  --① (F_UNSO_L) CUST_CD_Lで届先Mに存在する場合       " & vbNewLine _
                                           & "  LEFT JOIN $LM_MST$..M_DEST AS DEST03                 " & vbNewLine _
                                           & "  ON  UNSO.ORIG_CD   = DEST03.DEST_CD                  " & vbNewLine _
                                           & "  AND UNSO.NRS_BR_CD = DEST03.NRS_BR_CD                " & vbNewLine _
                                           & "  AND UNSO.CUST_CD_L = DEST03.CUST_CD_L                " & vbNewLine _
                                           & "  --②①で存在せず、(M_DEST) CUST_CD_L='ZZZZZ'で届先Mに存在する場合" & vbNewLine _
                                           & "  LEFT JOIN $LM_MST$..M_DEST AS DEST04                 " & vbNewLine _
                                           & "  ON  UNSO.ORIG_CD = DEST04.DEST_CD                    " & vbNewLine _
                                           & "  AND UNSO.NRS_BR_CD = DEST04.NRS_BR_CD                " & vbNewLine _
                                           & "  AND DEST04.CUST_CD_L = 'ZZZZZ'                       " & vbNewLine _
                                           & " --721 END                                             " & vbNewLine _
                                           & "	WHERE                                             	 " & vbNewLine _
                                           & "	UNCHIN.NRS_BR_CD= @NRS_BR_CD                         " & vbNewLine _
                                           & "	AND UNCHIN.SYS_DEL_FLG='0'                           " & vbNewLine

#End Region
    'LMF535対応 2018/06/22 Annen add end


#End Region

#Region "GROUP BY"

    'LMF535対応 2018/06/22 Annen add start

    ''' <summary>
    ''' LMF535用の GROUP BY 区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_LMF535 As String = "GROUP BY                                     " & vbNewLine _
                                         & "	 MR1.RPT_ID                                     " & vbNewLine _
                                         & "    ,MR1.PTN_CD                                     " & vbNewLine _
                                         & "    ,MR2.RPT_ID                                     " & vbNewLine _
                                         & "    ,MR2.PTN_CD                                     " & vbNewLine _
                                         & "	,MR3.RPT_ID                                     " & vbNewLine _
                                         & "	,UNSO.MOTO_DATA_KB                              " & vbNewLine _
                                         & "	,UNCHIN.CUST_CD_L                               " & vbNewLine _
                                         & "	,CUST.CUST_NM_L                                 " & vbNewLine _
                                         & "    ,CUST_F.CUST_NM_L                               " & vbNewLine _
                                         & "	,CUST.CUST_NM_M                                 " & vbNewLine _
                                         & "    ,CUST_F.CUST_NM_M                               " & vbNewLine _
                                         & "    ,CUST.CUST_NM_S                                 " & vbNewLine _
                                         & "	,CUST_F.CUST_NM_S                               " & vbNewLine _
                                         & "	,CUST.CUST_NM_SS                                " & vbNewLine _
                                         & "	,CUST_F.CUST_NM_SS                              " & vbNewLine _
                                         & "	,NRS.NRS_BR_NM                                  " & vbNewLine _
                                         & "	,UNSO.OUTKA_PLAN_DATE                           " & vbNewLine _
                                         & "	,UNCHIN.SEIQ_TARIFF_CD                          " & vbNewLine _
                                         & "	,UNSO.TARIFF_BUNRUI_KB                          " & vbNewLine _
                                         & "	,YOKO.YOKO_REM                                  " & vbNewLine _
                                         & "	,TARIFF.UNCHIN_TARIFF_REM                       " & vbNewLine _
                                         & "	,UNSOM.GOODS_NM                                 " & vbNewLine _
                                         & "	,UNSO.UNSO_PKG_NB                               " & vbNewLine _
                                         & "	,UNCHIN.DECI_WT                                 " & vbNewLine _
                                         & "	,UNCHIN.DECI_KYORI                              " & vbNewLine _
                                         & "	,UNSOCO.UNSOCO_NM                               " & vbNewLine _
                                         & "	,UNSO.MOTO_DATA_KB                              " & vbNewLine _
                                         & "	,OUTL.DEST_KB                                   " & vbNewLine _
                                         & "    ,DESTOUTKA.JIS                                  " & vbNewLine _
                                         & "	,EDIL.DEST_JIS_CD                               " & vbNewLine _
                                         & "	,DEST03.AD_1                                    " & vbNewLine _
                                         & "	,DEST03.JIS                                     " & vbNewLine _
                                         & "	,DEST04.AD_1                                    " & vbNewLine _
                                         & "	,DEST04.JIS                                     " & vbNewLine _
                                         & "	,DEST.JIS                                       " & vbNewLine _
                                         & "	,DEST2.JIS                                      " & vbNewLine _
                                         & "	,UNCHIN.DECI_UNCHIN                             " & vbNewLine _
                                         & "	,UNCHIN.DECI_CITY_EXTC                          " & vbNewLine _
                                         & "	,UNCHIN.DECI_WINT_EXTC                          " & vbNewLine _
                                         & "	,UNCHIN.DECI_RELY_EXTC                          " & vbNewLine _
                                         & "	,UNCHIN.DECI_TOLL                               " & vbNewLine _
                                         & "	,UNCHIN.DECI_INSU                               " & vbNewLine _
                                         & "    ,OUTL.DEST_NM                                   " & vbNewLine _
                                         & "	,EDIL.DEST_NM                                   " & vbNewLine _
                                         & "	,DEST03.DEST_NM                                 " & vbNewLine _
                                         & "	,DEST04.DEST_NM                                 " & vbNewLine _
                                         & "    ,DEST.DEST_NM                                   " & vbNewLine _
                                         & "	,DEST2.DEST_NM                                  " & vbNewLine _
                                         & "	,UNCHIN.SEIQ_GROUP_NO                           " & vbNewLine _
                                         & "	,UNCHIN.SEIQTO_CD                               " & vbNewLine _
                                         & "	,SEIQTO.SEIQTO_NM                               " & vbNewLine _
                                         & "	,OUTL.DEST_AD_1                                 " & vbNewLine _
                                         & "	,EDIL.DEST_AD_1                                 " & vbNewLine _
                                         & "	,DEST.AD_1                                      " & vbNewLine _
                                         & "	,DEST1.AD_1                                     " & vbNewLine _
                                         & "	,UNSO.UNSO_NO_L                                 " & vbNewLine _
                                         & "	,UNSO.INOUTKA_NO_L                              " & vbNewLine _
                                         & "	,UNCHIN.REMARK                                  " & vbNewLine _
                                         & "	,UNCHIN.SEIQ_FIXED_FLAG                         " & vbNewLine _
                                         & "	,UNCHIN.CUST_CD_M                               " & vbNewLine _
                                         & "	,UNCHIN.CUST_CD_S                               " & vbNewLine _
                                         & "	,UNCHIN.CUST_CD_SS                              " & vbNewLine _
                                         & "	,OUTL.DEST_CD                                   " & vbNewLine _
                                         & "	,EDIL.DEST_CD                                   " & vbNewLine _
                                         & "	,UNSO.DEST_CD                                   " & vbNewLine _
                                         & "	,UNCHIN.NRS_BR_CD                               " & vbNewLine _
                                         & "	,UNCHIN.SEIQ_GROUP_NO_M                         " & vbNewLine _
                                         & "	,UNSOM.UNSO_NO_M                                " & vbNewLine _
                                         & "	,KBN01.KBN_NM1                                  " & vbNewLine _
                                         & "    ,UNSO.JIYU_KB                                   " & vbNewLine _
                                         & "	,UNSO.ARR_PLAN_DATE                             " & vbNewLine _
                                         & "	,UNSO.ORIG_CD                                   " & vbNewLine _
                                         & "	,DEST2.AD_1                                     " & vbNewLine

    'LMF535対応 2018/06/22 Annen add end

#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                             " & vbNewLine _
                                         & "     UNCHIN.NRS_BR_CD                                " & vbNewLine _
                                         & "    ,UNSO.MOTO_DATA_KB                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                               " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                                " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE                            " & vbNewLine _
                                         & "    ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD        " & vbNewLine _
                                         & "          WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD        " & vbNewLine _
                                         & "          ELSE UNSO.DEST_CD END                                                          " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO                            " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO_M                          " & vbNewLine _
                                         & "    ,UNSO.UNSO_NO_L                                  " & vbNewLine _
                                         & "    ,UNSOM.UNSO_NO_M                                 " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "ORDER BY                                             " & vbNewLine _
                                         & "     UNCHIN.NRS_BR_CD                                " & vbNewLine _
                                         & "    ,UNSO.MOTO_DATA_KB                                " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE                            " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                               " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                                " & vbNewLine _
                                         & "    ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD        " & vbNewLine _
                                         & "          WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD        " & vbNewLine _
                                         & "          ELSE UNSO.DEST_CD END                                                          " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO                            " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO_M                          " & vbNewLine _
                                         & "    ,UNSO.UNSO_NO_L                                  " & vbNewLine _
                                         & "    ,UNSOM.UNSO_NO_M                                 " & vbNewLine


    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF532 As String = "ORDER BY                                             " & vbNewLine _
                                         & "     UNCHIN.NRS_BR_CD                                " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_FIXED_FLAG                          " & vbNewLine _
                                         & "    ,UNSO.MOTO_DATA_KB                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                               " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                                " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE                            " & vbNewLine _
                                         & "    ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD        " & vbNewLine _
                                         & "          WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD        " & vbNewLine _
                                         & "          ELSE UNSO.DEST_CD END                                                          " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO                            " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO_M                          " & vbNewLine _
                                         & "    ,UNSO.UNSO_NO_L                                  " & vbNewLine _
                                         & "    ,UNSOM.UNSO_NO_M                                 " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMF532_2 As String = "ORDER BY                                             " & vbNewLine _
                                         & "     UNCHIN.NRS_BR_CD                                " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_FIXED_FLAG                          " & vbNewLine _
                                         & "    ,UNSO.MOTO_DATA_KB                                " & vbNewLine _
                                         & "    ,UNSO.OUTKA_PLAN_DATE                            " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_L                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_M                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_S                                " & vbNewLine _
                                         & "    ,UNCHIN.CUST_CD_SS                               " & vbNewLine _
                                         & "    ,UNCHIN.SEIQTO_CD                                " & vbNewLine _
                                         & "    ,CASE WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '01' THEN OUTL.DEST_CD        " & vbNewLine _
                                         & "          WHEN UNSO.MOTO_DATA_KB = '20' AND OUTL.DEST_KB = '02' THEN EDIL.DEST_CD        " & vbNewLine _
                                         & "          ELSE UNSO.DEST_CD END                                                          " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO                            " & vbNewLine _
                                         & "    ,UNCHIN.SEIQ_GROUP_NO_M                          " & vbNewLine _
                                         & "    ,UNSO.UNSO_NO_L                                  " & vbNewLine _
                                         & "    ,UNSOM.UNSO_NO_M                                 " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMF530IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Dim count As Integer = 0

        'SQL作成
        Me._StrSql.Append(LMF530DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF530DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL(count)                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF530DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMF530IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")
        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Dim count As Integer = 0
        'SQL作成
        '---> LMF531対応 2012/06/12
        'Me._StrSql.Append(LMF530DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        'Me._StrSql.Append(LMF530DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        'Call Me.SetConditionMasterSQL(count)                   '条件設定
        'count = count + 1                                      '次にパラーメータセットしないため  
        'Me._StrSql.Append(LMF530DAC.SQL_FROM_2)                'SQL構築(データ抽出用From句)    
        'Me._StrSql.Append(LMF530DAC.SQL_FROM_1)                'SQL構築(データ抽出用From句)
        'Call Me.SetConditionMasterSQL(count)                   '条件設定

        Select Case rptId
            Case "LMF531"
                Me._StrSql.Append(LMF530DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF530DAC.SQL_FROM_LMF531)      'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL(count)                   '条件設定
                'LMF535対応 2018/06/22 Annen Add Start
            Case "LMF535"
                Me._StrSql.Append(LMF530DAC.SQL_SELECT_DATA_LMF535)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF530DAC.SQL_FROM_LMF535)             'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL(count)                     '条件設定
                Me._StrSql.Append(LMF530DAC.SQL_GROUP_BY_LMF535)        'SQL構築(データ抽出用Group By句)
                'LMF535対応 2018/06/22 Annen Add End
            Case Else
                Me._StrSql.Append(LMF530DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF530DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL(count)                   '条件設定
        End Select
        '<--- LMF531対応 2012/06/12

        Select Case rptId
            Case "LMF532"
                If System.String.IsNullOrEmpty(Me._Row.Item("CUST_CD_M").ToString) Then
                    Me._StrSql.Append(LMF530DAC.SQL_ORDER_BY_LMF532_2)

                Else
                    Me._StrSql.Append(LMF530DAC.SQL_ORDER_BY_LMF532)

                End If
            Case Else
                If System.String.IsNullOrEmpty(Me._Row.Item("CUST_CD_M").ToString) Then
                    Me._StrSql.Append(LMF530DAC.SQL_ORDER_BY2)

                Else
                    Me._StrSql.Append(LMF530DAC.SQL_ORDER_BY)

                End If

        End Select

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF530DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("T_DATE", "T_DATE")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("JIS", "JIS")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("AD_1", "AD_1")
        map.Add("AD_1_a", "AD_1_a")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("REMARK", "REMARK")
        map.Add("SEIQ_FIXED_FLAG", "SEIQ_FIXED_FLAG")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEIQ_GROUP_NO_M", "SEIQ_GROUP_NO_M")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("MOTO_DATA_NM", "MOTO_DATA_NM")
        '--LMF531対応 2012/06/12
        map.Add("JIYU_KB", "JIYU_KB")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("NYUUKA_CD", "NYUUKA_CD")
        map.Add("NYUUKA_NM", "NYUUKA_NM")
        map.Add("NYUUKA_AD_1", "NYUUKA_AD_1")
        map.Add("NYUUKA_JIS", "NYUUKA_JIS")
        map.Add("SYUKKA_CD", "SYUKKA_CD")
        map.Add("SYUKKA_NM", "SYUKKA_NM")
        map.Add("SYUKKA_AD_1", "SYUKKA_AD_1")
        map.Add("SYUKKA_JIS", "SYUKKA_JIS")
        'LMF535対応 2018/06/22 Annen Add Start
        If rptId.Equals("LMF535") Then
            map.Add("DECI_PKG_NB", "DECI_PKG_NB")
        End If
        'LMF535対応 2018/06/22 Annen Add End

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF530OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal count As Integer)



        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            'countが1の場合は処理しない
            If count = 0 Then

                '営業所コード
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

                '日付のToを取得
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", Me._Row.Item("T_DATE").ToString(), DBDataType.CHAR))

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

            End If

            '荷主コード（大）
            whereStr = Me._Row.Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNCHIN.CUST_CD_L = @CUST_CD_L                     ")
                Me._StrSql.Append(vbNewLine)

                'countが1の場合は処理しない
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                End If

            End If

            '荷主コード（中）
            whereStr = Me._Row.Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNCHIN.CUST_CD_M = @CUST_CD_M                      ")
                Me._StrSql.Append(vbNewLine)
            End If
            'countが1の場合は処理しない
            If count = 0 Then

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))

            End If


            '日付
            Me._StrSql.Append("	   AND ( (                 	      ")
            Me._StrSql.Append("	           @UNTIN_CALCULATION_KB ='01'                        ")

            whereStr = Me._Row.Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNSO.OUTKA_PLAN_DATE >= @F_DATE                    ")
                Me._StrSql.Append(vbNewLine)

                'countが1の場合は処理しない
                If count = 0 Then

                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", Me._Row.Item("F_DATE").ToString(), DBDataType.CHAR))

                End If

            End If

            whereStr = Me._Row.Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND UNSO.OUTKA_PLAN_DATE <= @T_DATE                    ")
                Me._StrSql.Append(vbNewLine)
                '  Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", Me._Row.Item("T_DATE").ToString(), DBDataType.CHAR))
            End If

            Me._StrSql.Append("           )                                           ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("	   OR    (                 	                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("	           @UNTIN_CALCULATION_KB ='02'                ")
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
            Me._StrSql.Append("	   AND UNCHIN.UNTIN_CALCULATION_KB = @UNTIN_CALCULATION_KB  ")
            Me._StrSql.Append(vbNewLine)

            'countが1の場合は処理しない
            If count = 0 Then

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", Me._Row.Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))

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
