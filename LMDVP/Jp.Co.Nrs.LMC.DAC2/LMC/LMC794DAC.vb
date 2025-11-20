' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC794    : 名鉄・送り状
'  作  成  者       :  tsunehira 
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC794DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC794DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "帳票種別取得用・送り状"

    ''' <summary>
    ''' 帳票種別取得用・送り状
    ''' </summary>
    ''' <remarks></remarks>
#If False Then
    Private Const SQL_SELECT_MPrt_Label As String = "SELECT DISTINCT                                                                                " & vbNewLine _
                                                    & "	UNSOL.NRS_BR_CD                                                     AS NRS_BR_CD            " & vbNewLine _
                                                    & ",'11'                                                                AS PTN_ID               " & vbNewLine _
                                                    & ",CASE  WHEN MRPT_CUST.PTN_CD IS NOT NULL THEN MRPT_CUST.PTN_CD                               " & vbNewLine _
                                                    & "	    WHEN MRPT_ALL.PTN_CD IS NOT NULL THEN MRPT_ALL.PTN_CD                                   " & vbNewLine _
                                                    & "	 	ELSE '' END                                                     AS PTN_CD               " & vbNewLine _
                                                    & ",CASE WHEN MRPT_CUST.PTN_CD IS NOT NULL THEN MRPT_CUST.RPT_ID                                " & vbNewLine _
                                                    & "  	WHEN MRPT_ALL.PTN_CD IS NOT NULL THEN MRPT_ALL.RPT_ID                                   " & vbNewLine _
                                                    & "		ELSE '' END                                                     AS RPT_ID               " & vbNewLine
#Else
    Private Const SQL_SELECT_MPrt_Label As String = "SELECT DISTINCT                                                                                " & vbNewLine _
                                                    & "	UNSOL.NRS_BR_CD                                                     AS NRS_BR_CD            " & vbNewLine _
                                                    & ",'11'                                                                AS PTN_ID               " & vbNewLine _
                                                    & ",CASE WHEN MRPT_CUST.PTN_CD IS NOT NULL THEN MRPT_CUST.PTN_CD           " & vbNewLine _
                                                    & "		 WHEN MRPT_ALL.PTN_CD IS NOT NULL THEN MRPT_ALL.PTN_CD             " & vbNewLine _                                                    
                                                    & "	 	 ELSE MR.PTN_CD END                                  AS PTN_CD     " & vbNewLine _
                                                    & ",CASE WHEN MRPT_CUST.PTN_CD IS NOT NULL THEN MRPT_CUST.RPT_ID           " & vbNewLine _
                                                    & "      WHEN MRPT_ALL.PTN_CD IS NOT NULL THEN MRPT_ALL.RPT_ID             " & vbNewLine _
                                                    & "		 ELSE MR.RPT_ID END                                  AS RPT_ID     " & vbNewLine
#End If
#End Region

#Region "検索処理 SQL"

#Region "SELECT"
    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU As String = " SELECT                                                                                    " & vbNewLine _
                                                       & "  OUTKAL.NRS_BR_CD                                                   AS NRS_BR_CD        " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C10                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C10                                                         " & vbNewLine _
                                                       & "  END                                                                AS UNSOCO_BR_NM     " & vbNewLine _
                                                       & " ,UNSOCO.TEL                                                         AS TEL              " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                                  AS DENPYO_NO        " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PLAN_DATE                                             AS OUTKA_PLAN_DATE  " & vbNewLine _
                                                       & " ,UNSOL.AUTO_DENP_NO                                                 AS AUTO_DENP_NO     " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_CD    " & vbNewLine _
                                                       & " ,NRSBR.NRS_BR_NM                                                    AS NIOKURININ_MEI1  " & vbNewLine _
                                                       & " ,''                                                                 AS NIOKURININ_MEI2  " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.ZIP                                                                     " & vbNewLine _
                                                       & "       ELSE NRSBR.ZIP                                                                    " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_ZIP   " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_1                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_1                                                                   " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_ADD1  " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_2                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_2                                                                   " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_ADD2  " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_3                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_3                                                                   " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_ADD3  " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.TEL                                                                     " & vbNewLine _
                                                       & "       ELSE NRSBR.TEL                                                                    " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_TEL   " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                       & "   END                                                               AS SHIHARAININ_CD   " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST.CUST_DEST_CD                                                            " & vbNewLine _
                                                       & "       ELSE OUTKAL.DEST_CD                                                               " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_CD      " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_NM                                                               " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_NM                                                                 " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.DEST_NM                                                                " & vbNewLine _
                                                       & "       ELSE DEST.DEST_NM                                                                 " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_MEI1    " & vbNewLine _
                                                       & " ,''                                                                 AS NIUKENIN_MEI2    " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN ''                                                                           " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_ZIP                                                                " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.ZIP                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.ZIP                                                                     " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_ZIP     " & vbNewLine _
                                                       & "--,CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "--        THEN OUTKAL.DEST_AD_1                                                            " & vbNewLine _
                                                       & "--      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "--        THEN EDIL.DEST_AD_1                                                              " & vbNewLine _
                                                       & "--      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "--        THEN DEST2.AD_1                                                                  " & vbNewLine _
                                                       & "--      ELSE DEST.AD_1                                                                     " & vbNewLine _
                                                       & "--END                                                                  AS NIUKENIN_ADD1    " & vbNewLine _
                                                       & "--,CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "--        THEN OUTKAL.DEST_AD_2								                               " & vbNewLine _
                                                       & "--      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "--        THEN EDIL.DEST_AD_2								                               " & vbNewLine _
                                                       & "--      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "--        THEN DEST2.AD_2									                               " & vbNewLine _
                                                       & "--      ELSE DEST.AD_2									                                   " & vbNewLine _
                                                       & "--END                                                                  AS NIUKENIN_ADD2    " & vbNewLine _
                                                       & "--,CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "--        THEN OUTKAL.DEST_AD_3                                                            " & vbNewLine _
                                                       & "--      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "--        THEN EDIL.DEST_AD_3                                                              " & vbNewLine _
                                                       & "--      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "--        THEN OUTKAL.DEST_AD_3                                                            " & vbNewLine _
                                                       & "--      ELSE OUTKAL.DEST_AD_3                                                              " & vbNewLine _
                                                       & "--END                                                                  AS NIUKENIN_ADD3    " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_AD_1 + ' ' +                                                     " & vbNewLine _
                                                       & "            OUTKAL.DEST_AD_2 + ' ' +                                                     " & vbNewLine _
                                                       & "            OUTKAL.DEST_AD_3                                                             " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_AD_1 + ' ' +                                                       " & vbNewLine _
                                                       & "            EDIL.DEST_AD_2 + ' ' +                                                       " & vbNewLine _
                                                       & "            EDIL.DEST_AD_3                                                               " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.AD_1 + ' ' +                                                           " & vbNewLine _
                                                       & "            DEST2.AD_2 + ' ' +                                                           " & vbNewLine _
                                                       & "            OUTKAL.DEST_AD_3                                                             " & vbNewLine _
                                                       & "       ELSE DEST.AD_1 + ' ' +                                                            " & vbNewLine _
                                                       & "            DEST.AD_2 + ' ' +                                                            " & vbNewLine _
                                                       & "            OUTKAL.DEST_AD_3                                                             " & vbNewLine _
                                                       & "  END AS NIUKENIN_ADD1                                                                   " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_ADD2                                                                    " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_ADD3                                                                    " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_TEL                                                              " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_TEL                                                                " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.TEL                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.TEL                                                                     " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_TEL     " & vbNewLine _
                                                       & " ,OUTKAL.ARR_PLAN_DATE                                               AS ARR_PLAN_DATE    " & vbNewLine _
                                                       & " ,ZKBN.KBN_NM1                                                       AS ARR_PLAN_TIME    " & vbNewLine _
                                                       & " ,DEST3.DEST_NM                                                      AS SHIP_NM_L        " & vbNewLine _
                                                       & " ,CUST.DENPYO_NM                                                     AS DENPYO_NM        " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C03                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C03                                                         " & vbNewLine _
                                                       & "  END                                                                AS KIJI_1           " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C04                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C04                                                         " & vbNewLine _
                                                       & "  END                                                                AS KIJI_2           " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C05                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C05                                                         " & vbNewLine _
                                                       & "  END                                                                AS KIJI_3           " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C06                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C06                                                         " & vbNewLine _
                                                       & "  END                                                                AS KIJI_4           " & vbNewLine _
                                                       & " ,UNSOL.REMARK                                                       AS KIJI_5           " & vbNewLine _
                                                       & " ,''                                                                 AS KIJI_6           " & vbNewLine _
                                                       & " ,OUTKAL.CUST_ORD_NO                                                 AS KIJI_7           " & vbNewLine _
                                                       & " ,OUTKAL.BUYER_ORD_NO                                                AS KIJI_8           " & vbNewLine _
                                                       & "-- ,''                                                               AS KIJI_8           " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PKG_NB                                                AS KOSU             " & vbNewLine _
                                                       & " ,FLOOR(UNSOL.UNSO_WT)                                               AS JYURYO           " & vbNewLine _
                                                       & " ,'0'                                                                AS YOSEKI           " & vbNewLine _
                                                       & " ,OUTKAL.CUST_CD_L                                                   AS CUST_CD_L        " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.JIS                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.JIS                                                                     " & vbNewLine _
                                                       & "  END                                                                AS JIS              " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.UNCHIN_SEIQTO_CD                                                       " & vbNewLine _
                                                       & "       ELSE DEST.UNCHIN_SEIQTO_CD                                                        " & vbNewLine _
                                                       & "  END                                                                AS UNCHIN_SEIQTO_CD " & vbNewLine _
                                                       & " ,@ROW_NO                                                            AS ROW_NO           " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L                                                     AS CUST_NM_L        " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                                  AS OUTKA_NO_L       " & vbNewLine _
                                                       & " ,MCD.SET_NAIYO											           AS ZIP_PATTERN      " & vbNewLine _
                                                       & " ,UNSOL.UNSO_NO_L                                                    AS UNSO_NO_L        " & vbNewLine _
                                                       & " ,@SYS_DATE                                                          AS SYS_UPD_DATE     " & vbNewLine _
                                                       & " ,@SYS_TIME                                                          AS SYS_UPD_TIME     " & vbNewLine _
                                                       & " ,@UNSO_SYS_UPD_DATE                                                 AS UNSO_SYS_UPD_DATE     " & vbNewLine _
                                                       & " ,@UNSO_SYS_UPD_TIME                                                 AS UNSO_SYS_UPD_TIME     " & vbNewLine _
                                                       & " ,ISNULL( ( SELECT GOODS.GOODS_NM_1 FROM                                                      " & vbNewLine _
                                                       & " 		(SELECT MIN(CM.OUTKA_NO_M) AS OUTKA_NO_M FROM $LM_TRN$..C_OUTKA_M AS CM                 " & vbNewLine _
                                                       & "       WHERE CM.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                                       & "       AND CM.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
                                                       & "       AND CM.OUTKA_NO_L = @OUTKA_NO_L                                                        " & vbNewLine _
                                                       & "       GROUP BY CM.OUTKA_NO_L                                                                 " & vbNewLine _
                                                       & "      ) AS BASE                                                                               " & vbNewLine _
                                                       & "      LEFT JOIN $LM_TRN$..C_OUTKA_M AS CM ON                                                  " & vbNewLine _
                                                       & "      CM.NRS_BR_CD = @NRS_BR_CD                                                               " & vbNewLine _
                                                       & "      AND CM.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine _
                                                       & "      AND CM.OUTKA_NO_M = BASE.OUTKA_NO_M                                                     " & vbNewLine _
                                                       & "      LEFT JOIN $LM_MST$..M_GOODS AS GOODS ON                                                 " & vbNewLine _
                                                       & "      GOODS.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
                                                       & "      AND GOODS.GOODS_CD_NRS = CM.GOODS_CD_NRS),'')  AS GOODS_NM_1                            " & vbNewLine

#End Region

#Region "SELECT 運送"
    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_UNSO As String = " SELECT                                                                                    " & vbNewLine _
                                                    & "  UNSOL.NRS_BR_CD                                                    AS NRS_BR_CD        " & vbNewLine _
                                                    & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                    & "       THEN OKURIJOCSV.FREE_C10                                                          " & vbNewLine _
                                                    & "       ELSE OKURIJOCSVX.FREE_C10                                                         " & vbNewLine _
                                                    & "  END                                                                AS UNSOCO_BR_NM     " & vbNewLine _
                                                    & " ,UNSOCO.TEL                                                         AS TEL              " & vbNewLine _
                                                    & " ,UNSOL.UNSO_NO_L                                                    AS DENPYO_NO        " & vbNewLine _
                                                    & " ,UNSOL.OUTKA_PLAN_DATE                                              AS OUTKA_PLAN_DATE  " & vbNewLine _
                                                    & " ,UNSOL.AUTO_DENP_NO                                                 AS AUTO_DENP_NO     " & vbNewLine _
                                                    & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                    & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                    & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                    & "  END                                                                AS NIOKURININ_CD    " & vbNewLine _
                                                    & " ,NRSBR.NRS_BR_NM                                                    AS NIOKURININ_MEI1  " & vbNewLine _
                                                    & " ,''                                                                 AS NIOKURININ_MEI2  " & vbNewLine _
                                                    & " ,NRSBR.ZIP                                                          AS NIOKURININ_ZIP   " & vbNewLine _
                                                    & " ,NRSBR.AD_1                                                         AS NIOKURININ_ADD1  " & vbNewLine _
                                                    & " ,NRSBR.AD_2                                                         AS NIOKURININ_ADD2  " & vbNewLine _
                                                    & " ,NRSBR.AD_3                                                         AS NIOKURININ_ADD3  " & vbNewLine _
                                                    & " ,NRSBR.TEL                                                          AS NIOKURININ_TEL   " & vbNewLine _
                                                    & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                    & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                    & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                    & "   END                                                               AS SHIHARAININ_CD   " & vbNewLine _
                                                    & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                    & "       THEN DEST.CUST_DEST_CD                                                            " & vbNewLine _
                                                    & "       ELSE UNSOL.DEST_CD                                                                " & vbNewLine _
                                                    & "  END                                                                AS NIUKENIN_CD      " & vbNewLine _
                                                    & " ,DEST.DEST_NM                                                       AS NIUKENIN_MEI1    " & vbNewLine _
                                                    & " ,''                                                                 AS NIUKENIN_MEI2    " & vbNewLine _
                                                    & " ,DEST.ZIP                                                           AS NIUKENIN_ZIP     " & vbNewLine _
                                                    & " ,DEST.AD_1 + ' ' +  DEST.AD_2 + ' ' + UNSOL.AD_3                    AS NIUKENIN_ADD1    " & vbNewLine _
                                                    & " ,'' AS NIUKENIN_ADD2                                                                    " & vbNewLine _
                                                    & " ,'' AS NIUKENIN_ADD3                                                                    " & vbNewLine _
                                                    & " ,DEST.TEL                                                           AS NIUKENIN_TEL     " & vbNewLine _
                                                    & " ,UNSOL.ARR_PLAN_DATE                                                AS ARR_PLAN_DATE    " & vbNewLine _
                                                    & " ,ZKBN.KBN_NM1                                                       AS ARR_PLAN_TIME    " & vbNewLine _
                                                    & " ,''                                                                 AS SHIP_NM_L        " & vbNewLine _
                                                    & " ,CUST.DENPYO_NM                                                     AS DENPYO_NM        " & vbNewLine _
                                                    & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                    & "       THEN OKURIJOCSV.FREE_C03                                                          " & vbNewLine _
                                                    & "       ELSE OKURIJOCSVX.FREE_C03                                                         " & vbNewLine _
                                                    & "  END                                                                AS KIJI_1           " & vbNewLine _
                                                    & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                    & "       THEN OKURIJOCSV.FREE_C04                                                          " & vbNewLine _
                                                    & "       ELSE OKURIJOCSVX.FREE_C04                                                         " & vbNewLine _
                                                    & "  END                                                                AS KIJI_2           " & vbNewLine _
                                                    & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                    & "       THEN OKURIJOCSV.FREE_C05                                                          " & vbNewLine _
                                                    & "       ELSE OKURIJOCSVX.FREE_C05                                                         " & vbNewLine _
                                                    & "  END                                                                AS KIJI_3           " & vbNewLine _
                                                    & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                    & "       THEN OKURIJOCSV.FREE_C06                                                          " & vbNewLine _
                                                    & "       ELSE OKURIJOCSVX.FREE_C06                                                         " & vbNewLine _
                                                    & "  END                                                                AS KIJI_4           " & vbNewLine _
                                                    & " ,UNSOL.REMARK                                                       AS KIJI_5           " & vbNewLine _
                                                    & " ,''                                                                 AS KIJI_6           " & vbNewLine _
                                                    & " ,''                                                                 AS KIJI_7           " & vbNewLine _
                                                    & " ,''                                                                 AS KIJI_8           " & vbNewLine _
                                                    & " ,UNSOL.UNSO_PKG_NB                                                  AS KOSU             " & vbNewLine _
                                                    & " ,FLOOR(UNSOL.UNSO_WT)                                               AS JYURYO           " & vbNewLine _
                                                    & " ,'0'                                                                AS YOSEKI           " & vbNewLine _
                                                    & " ,UNSOL.CUST_CD_L                                                    AS CUST_CD_L        " & vbNewLine _
                                                    & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                    & "       THEN DEST2.JIS                                                                    " & vbNewLine _
                                                    & "       ELSE DEST.JIS                                                                     " & vbNewLine _
                                                    & "  END                                                                AS JIS              " & vbNewLine _
                                                    & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                    & "       THEN DEST2.UNCHIN_SEIQTO_CD                                                       " & vbNewLine _
                                                    & "       ELSE DEST.UNCHIN_SEIQTO_CD                                                        " & vbNewLine _
                                                    & "  END                                                                AS UNCHIN_SEIQTO_CD " & vbNewLine _
                                                    & " ,@ROW_NO                                                            AS ROW_NO           " & vbNewLine _
                                                    & " ,CUST.CUST_NM_L                                                     AS CUST_NM_L        " & vbNewLine _
                                                    & " ,UNSOL.UNSO_NO_L                                                    AS OUTKA_NO_L       " & vbNewLine _
                                                    & " ,MCD.SET_NAIYO		        							            AS ZIP_PATTERN      " & vbNewLine _
                                                    & " ,UNSOL.UNSO_NO_L                                                    AS UNSO_NO_L        " & vbNewLine _
                                                    & " ,@SYS_DATE                                                          AS SYS_UPD_DATE     " & vbNewLine _
                                                    & " ,@SYS_TIME                                                          AS SYS_UPD_TIME     " & vbNewLine _
                                                    & " ,@UNSO_SYS_UPD_DATE                                                 AS UNSO_SYS_UPD_DATE " & vbNewLine _
                                                    & " ,@UNSO_SYS_UPD_TIME                                                 AS UNSO_SYS_UPD_TIME   " & vbNewLine _
                                                    & " ,''                                                                 AS GOODS_NM_1       " & vbNewLine

#End Region

#Region "SELECT句(廃棄)"
#If False Then

#Region "大阪"
    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_OSAKA As String = " SELECT                                                                                    " & vbNewLine _
                                                       & "  OUTKAL.NRS_BR_CD                                                   AS NRS_BR_CD        " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C10                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C10                                                         " & vbNewLine _
                                                       & "  END                                                                AS UNSOCO_BR_NM     " & vbNewLine _
                                                       & " ,UNSOCO.TEL                                                         AS TEL              " & vbNewLine _
                                                       & " ,UNSOL.AUTO_DENP_NO                                                 AS AUTO_DENP_NO     " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST.CUST_DEST_CD                                                            " & vbNewLine _
                                                       & "       ELSE OUTKAL.DEST_CD                                                               " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_CD      " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_AD_1                                                             " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_AD_1                                                               " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.AD_1                                                                   " & vbNewLine _
                                                       & "       ELSE DEST.AD_1                                                                    " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_ADD1    " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_AD_2                                                             " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_AD_2                                                               " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.AD_2                                                                   " & vbNewLine _
                                                       & "       ELSE DEST.AD_2                                                                    " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_ADD2    " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_AD_3                                                             " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_AD_3                                                               " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.AD_3                                                                   " & vbNewLine _
                                                       & "       ELSE DEST.AD_3                                                                    " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_ADD3    " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_NM                                                               " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_NM                                                                 " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.DEST_NM                                                                " & vbNewLine _
                                                       & "       ELSE DEST.DEST_NM                                                                 " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_MEI1    " & vbNewLine _
                                                       & " ,''                                                                 AS NIUKENIN_MEI2    " & vbNewLine _
                                                       & " ,DEST.ZIP                                                           AS NIUKENIN_ZIP     " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_TEL                                                              " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_TEL                                                                " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.TEL                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.TEL                                                                     " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_TEL     " & vbNewLine _
                                                       & " ,CUST.CUST_CD_L                                                     AS CUST_CD_L        " & vbNewLine _
                                                       & " ,CUST.AD_1                                                          AS NIOKURININ_ADD1  " & vbNewLine _
                                                       & " ,CUST.AD_2                                                          AS NIOKURININ_ADD2  " & vbNewLine _
                                                       & " ,CUST.AD_3                                                          AS NIOKURININ_ADD3  " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L                                                     AS NIOKURININ_MEI1  " & vbNewLine _
                                                       & " ,CUST.CUST_NM_M                                                     AS NIOKURININ_MEI2  " & vbNewLine _
                                                       & " ,CUST.TEL                                                           AS NIOKURININ_TEL   " & vbNewLine _
                                                       & " ,''                                                                 AS OKURIJO_NO       " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                                  AS DENPYO_NO        " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PLAN_DATE                                             AS OUTKA_PLAN_DATE  " & vbNewLine _
                                                       & " ,'1'                                                                AS PRT_CNT          " & vbNewLine _
                                                       & " ,SUM(OUTKAM.OUTKA_TTL_NB)                                           AS KOSU             " & vbNewLine _
                                                       & " ,UNSOL.UNSO_WT                                                      AS JYURYO           " & vbNewLine _
                                                       & " ,''                                                                 AS YOSEKI           " & vbNewLine _
                                                       & " , OUTKAL.ARR_PLAN_DATE                                              AS ARR_PLAN_DATE    " & vbNewLine _
                                                       & " , Z1.KBN_NM1                                                        AS ARR_PLAN_TIME    " & vbNewLine _
                                                       & " ,''                                                                 AS HAITATSU_KBN     " & vbNewLine _
                                                       & " ,''                                                                 AS HAITATSU_TIME_KBN" & vbNewLine _
                                                       & " ,GOODS.GOODS_NM_1                                                   AS KIJI_1           " & vbNewLine _
                                                       & " ,''                                                                 AS KIJI_2           " & vbNewLine _
                                                       & " ,''                                                                 AS KIJI_3           " & vbNewLine _
                                                       & " ,OUTKAL.BUYER_ORD_NO                                                AS KIJI_4           " & vbNewLine _
                                                       & " ,Z1.KBN_NM1                                                         AS KIJI_5           " & vbNewLine _
                                                       & " ,''                                                                 AS KIJI_6           " & vbNewLine _
                                                       & " ,''                                                                 AS KIJI_7           " & vbNewLine _
                                                       & " ,''                                                                 AS KIJI_8           " & vbNewLine _
                                                       & " ,@ROW_NO                                                            AS ROW_NO           " & vbNewLine _
                                                       & " ,''                                                                 AS DENPYO_NM        " & vbNewLine _
                                                       & " ,''                                                                 AS UNCHIN_SEIQTO_CD " & vbNewLine _
                                                       & " ,''                                                                 AS SHIP_NM_L        " & vbNewLine _
                                                       & " ,''                                                                 AS NIOKURININ_CD    " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                       & "   END                                                               AS SHIHARAININ_CD   " & vbNewLine _
                                                       & " ,''                                                                 AS JIS              " & vbNewLine _
                                                       & " ,''                                                                 AS CUST_NM_L        " & vbNewLine _
                                                       & " ,''                                                                 AS NIOKURININ_ZIP   " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                                  AS OUTKA_NO_L       " & vbNewLine _
                                                       & ",MCD.SET_NAIYO											           AS ZIP_PATTERN      " & vbNewLine _
                                                       & ",UNSOL.UNSO_NO_L                                                     AS UNSO_NO_L        " & vbNewLine

#End Region

#Region "群馬"
    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_GUNMA As String = " SELECT                                                                                    " & vbNewLine _
                                                       & " --群馬--                                                                                " & vbNewLine _
                                                       & "  OUTKAL.NRS_BR_CD                                                   AS NRS_BR_CD        " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C10                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C10                                                         " & vbNewLine _
                                                       & "  END                                                                AS UNSOCO_BR_NM     " & vbNewLine _
                                                       & " ,UNSOCO.TEL                                                         AS TEL              " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                                  AS DENPYO_NO        " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PLAN_DATE                                             AS OUTKA_PLAN_DATE  " & vbNewLine _
                                                       & " ,UNSOL.AUTO_DENP_NO                                                 AS AUTO_DENP_NO     " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_CD    " & vbNewLine _
                                                       & " ,NRSBR.NRS_BR_NM                                                    AS NIOKURININ_MEI1  " & vbNewLine _
                                                       & " ,''                                                                 AS NIOKURININ_MEI2  " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.ZIP                                                                     " & vbNewLine _
                                                       & "       ELSE NRSBR.ZIP                                                                    " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_ZIP   " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_1                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_1                                                                   " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_ADD1  " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_2                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_2                                                                   " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_ADD2  " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_3                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_3                                                                   " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_ADD3  " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.TEL                                                                     " & vbNewLine _
                                                       & "       ELSE NRSBR.TEL                                                                    " & vbNewLine _
                                                       & "  END                                                                AS NIOKURININ_TEL   " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                       & "   END                                                               AS SHIHARAININ_CD   " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST.CUST_DEST_CD                                                            " & vbNewLine _
                                                       & "       ELSE OUTKAL.DEST_CD                                                               " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_CD      " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_NM                                                               " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_NM                                                                 " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.DEST_NM                                                                " & vbNewLine _
                                                       & "       ELSE DEST.DEST_NM                                                                 " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_MEI1    " & vbNewLine _
                                                       & " ,''                                                                 AS NIUKENIN_MEI2    " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN ''                                                                           " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_ZIP                                                                " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.ZIP                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.ZIP                                                                     " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_ZIP     " & vbNewLine _
                                                       & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "        THEN OUTKAL.DEST_AD_1                                                            " & vbNewLine _
                                                       & "      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "        THEN EDIL.DEST_AD_1                                                              " & vbNewLine _
                                                       & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "        THEN DEST2.AD_1                                                                  " & vbNewLine _
                                                       & "      ELSE DEST.AD_1                                                                     " & vbNewLine _
                                                       & "END                                                                  AS NIUKENIN_ADD1    " & vbNewLine _
                                                       & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "        THEN OUTKAL.DEST_AD_2								                               " & vbNewLine _
                                                       & "      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "        THEN EDIL.DEST_AD_2								                               " & vbNewLine _
                                                       & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "        THEN DEST2.AD_2									                               " & vbNewLine _
                                                       & "      ELSE DEST.AD_2									                                   " & vbNewLine _
                                                       & "END                                                                  AS NIUKENIN_ADD2    " & vbNewLine _
                                                       & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "        THEN OUTKAL.DEST_AD_3                                                            " & vbNewLine _
                                                       & "      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "        THEN EDIL.DEST_AD_3                                                              " & vbNewLine _
                                                       & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "        THEN OUTKAL.DEST_AD_3                                                            " & vbNewLine _
                                                       & "      ELSE OUTKAL.DEST_AD_3                                                              " & vbNewLine _
                                                       & "END                                                                  AS NIUKENIN_ADD3    " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_TEL                                                              " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_TEL                                                                " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.TEL                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.TEL                                                                     " & vbNewLine _
                                                       & "  END                                                                AS NIUKENIN_TEL     " & vbNewLine _
                                                       & " ,OUTKAL.ARR_PLAN_DATE                                               AS ARR_PLAN_DATE    " & vbNewLine _
                                                       & " ,ZKBN.KBN_NM1                                                       AS ARR_PLAN_TIME    " & vbNewLine _
                                                       & " ,DEST3.DEST_NM                                                      AS SHIP_NM_L        " & vbNewLine _
                                                       & " ,CUST.DENPYO_NM                                                     AS DENPYO_NM        " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C03                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C03                                                         " & vbNewLine _
                                                       & "  END                                                                AS KIJI_1           " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C04                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C04                                                         " & vbNewLine _
                                                       & "  END                                                                AS KIJI_2           " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C05                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C05                                                         " & vbNewLine _
                                                       & "  END                                                                AS KIJI_3           " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C06                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C06                                                         " & vbNewLine _
                                                       & "  END                                                                AS KIJI_4           " & vbNewLine _
                                                       & " ,UNSOL.REMARK                                                       AS KIJI_5           " & vbNewLine _
                                                       & " ,''                                                                 AS KIJI_6           " & vbNewLine _
                                                       & " ,OUTKAL.CUST_ORD_NO                                                 AS KIJI_7           " & vbNewLine _
                                                       & " ,OUTKAL.BUYER_ORD_NO                                                AS KIJI_8           " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PKG_NB                                                AS KOSU             " & vbNewLine _
                                                       & " ,FLOOR(UNSOL.UNSO_WT)                                               AS JYURYO           " & vbNewLine _
                                                       & " ,'0'                                                                AS YOSEKI           " & vbNewLine _
                                                       & " ,OUTKAL.CUST_CD_L                                                   AS CUST_CD_L        " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.JIS                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.JIS                                                                     " & vbNewLine _
                                                       & "  END                                                                AS JIS              " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.UNCHIN_SEIQTO_CD                                                       " & vbNewLine _
                                                       & "       ELSE DEST.UNCHIN_SEIQTO_CD                                                        " & vbNewLine _
                                                       & "  END                                                                AS UNCHIN_SEIQTO_CD " & vbNewLine _
                                                       & " ,@ROW_NO                                                            AS ROW_NO           " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L                                                     AS CUST_NM_L        " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                                  AS OUTKA_NO_L       " & vbNewLine _
                                                       & " ,MCD.SET_NAIYO											           AS ZIP_PATTERN      " & vbNewLine _
                                                       & " ,UNSOL.UNSO_NO_L                                                    AS UNSO_NO_L        " & vbNewLine _
                                                       & " ,UNSOL.SYS_UPD_DATE                                                 AS SYS_UPD_DATE     " & vbNewLine _
                                                       & " ,UNSOL.SYS_UPD_TIME                                                 AS SYS_UPD_TIME     " & vbNewLine

#End Region

#Region "横浜"
    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_YOKOHAMA As String = " SELECT                                                                                 " & vbNewLine _
                                                       & "  OUTKAL.NRS_BR_CD                                                AS NRS_BR_CD           " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C10                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C10                                                         " & vbNewLine _
                                                       & "  END                                                             AS UNSOCO_BR_NM        " & vbNewLine _
                                                       & " ,UNSOCO.TEL                                                      AS TEL                 " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                               AS DENPYO_NO           " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PLAN_DATE                                          AS OUTKA_PLAN_DATE     " & vbNewLine _
                                                       & " ,UNSOL.AUTO_DENP_NO                                              AS AUTO_DENP_NO        " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                       & "  END                                                             AS NIOKURININ_CD       " & vbNewLine _
                                                       & " ,NRSBR.NRS_BR_NM                                                 AS NIOKURININ_MEI1     " & vbNewLine _
                                                       & " ,''                                                              AS NIOKURININ_MEI2     " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.ZIP                                                                     " & vbNewLine _
                                                       & "       ELSE NRSBR.ZIP                                                                    " & vbNewLine _
                                                       & "  END                                                             AS NIOKURININ_ZIP      " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_1                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_1                                                                   " & vbNewLine _
                                                       & "  END                                                             AS NIOKURININ_ADD1     " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_2                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_2                                                                   " & vbNewLine _
                                                       & "  END                                                             AS NIOKURININ_ADD2     " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.AD_3                                                                    " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_3                                                                   " & vbNewLine _
                                                       & "  END                                                             AS NIOKURININ_ADD3     " & vbNewLine _
                                                       & " ,CASE WHEN SOKO.WH_KB = '01'                                                            " & vbNewLine _
                                                       & "       THEN SOKO.TEL                                                                     " & vbNewLine _
                                                       & "       ELSE NRSBR.TEL                                                                    " & vbNewLine _
                                                       & "  END                                                             AS NIOKURININ_TEL      " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                       & "   END                                                            AS SHIHARAININ_CD      " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST.CUST_DEST_CD                                                            " & vbNewLine _
                                                       & "       ELSE OUTKAL.DEST_CD                                                               " & vbNewLine _
                                                       & "  END                                                             AS NIUKENIN_CD         " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_NM                                                               " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_NM                                                                 " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.DEST_NM                                                                " & vbNewLine _
                                                       & "       ELSE DEST.DEST_NM                                                                 " & vbNewLine _
                                                       & "  END                                                             AS NIUKENIN_MEI1       " & vbNewLine _
                                                       & " ,''                                                              AS NIUKENIN_MEI2       " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN ''                                                                           " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_ZIP                                                                " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.ZIP                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.ZIP                                                                     " & vbNewLine _
                                                       & "  END                                                             AS NIUKENIN_ZIP        " & vbNewLine _
                                                       & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "        THEN OUTKAL.DEST_AD_1                                                        	   " & vbNewLine _
                                                       & "      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "        THEN EDIL.DEST_AD_1                                                              " & vbNewLine _
                                                       & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "        THEN DEST2.AD_1                                                                  " & vbNewLine _
                                                       & "      ELSE DEST.AD_1                                                                     " & vbNewLine _
                                                       & "END                                                               AS NIUKENIN_ADD1       " & vbNewLine _
                                                       & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "        THEN OUTKAL.DEST_AD_2								                               " & vbNewLine _
                                                       & "      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "        THEN EDIL.DEST_AD_2								                               " & vbNewLine _
                                                       & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "        THEN DEST2.AD_2									                               " & vbNewLine _
                                                       & "      ELSE DEST.AD_2									                                   " & vbNewLine _
                                                       & "END                                                               AS NIUKENIN_ADD2       " & vbNewLine _
                                                       & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                         " & vbNewLine _
                                                       & "        THEN OUTKAL.DEST_AD_3                                                            " & vbNewLine _
                                                       & "      WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
                                                       & "        THEN EDIL.DEST_AD_3                                                              " & vbNewLine _
                                                       & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                    " & vbNewLine _
                                                       & "        THEN OUTKAL.DEST_AD_3                                                            " & vbNewLine _
                                                       & "      ELSE OUTKAL.DEST_AD_3                                                              " & vbNewLine _
                                                       & "END                                                               AS NIUKENIN_ADD3       " & vbNewLine _
                                                       & " ,CASE WHEN OUTKAL.DEST_KB = '01'                                                        " & vbNewLine _
                                                       & "       THEN OUTKAL.DEST_TEL                                                              " & vbNewLine _
                                                       & "       WHEN OUTKAL.DEST_KB = '02'                                                        " & vbNewLine _
                                                       & "       THEN EDIL.DEST_TEL                                                                " & vbNewLine _
                                                       & "       WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.TEL                                                                    " & vbNewLine _
                                                       & "       ELSE OUTKAL.DEST_TEL                                                              " & vbNewLine _
                                                       & "  END                                                             AS NIUKENIN_TEL        " & vbNewLine _
                                                       & " ,OUTKAL.ARR_PLAN_DATE                                            AS ARR_PLAN_DATE       " & vbNewLine _
                                                       & " ,ZKBN.KBN_NM1                                                    AS ARR_PLAN_TIME       " & vbNewLine _
                                                       & " ,DEST3.DEST_NM                                                   AS SHIP_NM_L           " & vbNewLine _
                                                       & " ,CUST.DENPYO_NM                                                  AS DENPYO_NM           " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C03                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C03                                                         " & vbNewLine _
                                                       & "  END                                                             AS KIJI_1              " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C04                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C04                                                         " & vbNewLine _
                                                       & "  END                                                             AS KIJI_2              " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C05                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C05                                                         " & vbNewLine _
                                                       & "  END                                                             AS KIJI_3              " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C06                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C06                                                         " & vbNewLine _
                                                       & "  END                                                             AS KIJI_4              " & vbNewLine _
                                                       & " ,UNSOL.REMARK                                                    AS KIJI_5              " & vbNewLine _
                                                       & " ,''                                                              AS KIJI_6              " & vbNewLine _
                                                       & " ,OUTKAL.CUST_ORD_NO                                              AS KIJI_7              " & vbNewLine _
                                                       & " ,OUTKAL.BUYER_ORD_NO                                             AS KIJI_8              " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PKG_NB                                             AS KOSU                " & vbNewLine _
                                                       & " ,FLOOR(UNSOL.UNSO_WT)                                            AS JYURYO              " & vbNewLine _
                                                       & " ,'0'                                                             AS YOSEKI              " & vbNewLine _
                                                       & " ,OUTKAL.CUST_CD_L                                                AS CUST_CD_L           " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.JIS                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.JIS                                                                     " & vbNewLine _
                                                       & "  END                                                             AS JIS                 " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.UNCHIN_SEIQTO_CD                                                       " & vbNewLine _
                                                       & "       ELSE DEST.UNCHIN_SEIQTO_CD                                                        " & vbNewLine _
                                                       & "  END                                                             AS UNCHIN_SEIQTO_CD    " & vbNewLine _
                                                       & " ,@ROW_NO                                                         AS ROW_NO              " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L                                                  AS CUST_NM_L           " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                               AS OUTKA_NO_L          " & vbNewLine _
                                                       & " ,MCD.SET_NAIYO											        AS ZIP_PATTERN         " & vbNewLine _
                                                       & " ,UNSOL.UNSO_NO_L                                                 AS UNSO_NO_L           " & vbNewLine


#End Region

#Region "埼玉"
    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_SAITAMA As String = " SELECT                                                                               " & vbNewLine _
                                                   & "        OUTKAL.NRS_BR_CD                                              AS NRS_BR_CD        " & vbNewLine _
                                                   & "      ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                         " & vbNewLine _
                                                   & "         THEN OKURIJOCSV.FREE_C10                                                         " & vbNewLine _
                                                   & "         ELSE OKURIJOCSVX.FREE_C10                                                        " & vbNewLine _
                                                   & "      END                                                             AS UNSOCO_BR_NM     " & vbNewLine _
                                                   & "      , UNSOCO.TEL                                                    AS TEL              " & vbNewLine _
                                                   & "      , OUTKAL.OUTKA_NO_L                                             AS DENPYO_NO        " & vbNewLine _
                                                   & "      , OUTKAL.OUTKA_PLAN_DATE                                        AS OUTKA_PLAN_DATE  " & vbNewLine _
                                                   & "      ,UNSOL.AUTO_DENP_NO                                             AS AUTO_DENP_NO     " & vbNewLine _
                                                   & "      , CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                        " & vbNewLine _
                                                   & "          THEN OKURIJOCSV.FREE_C02                                                        " & vbNewLine _
                                                   & "          ELSE OKURIJOCSVX.FREE_C02                                                       " & vbNewLine _
                                                   & "      END                                                             AS NIOKURININ_CD    " & vbNewLine _
                                                   & "      , NRSBR.NRS_BR_NM AS NIOKURININ_MEI1                                                " & vbNewLine _
                                                   & "      , '' AS NIOKURININ_MEI2                                                             " & vbNewLine _
                                                   & "      , CASE WHEN SOKO.WH_KB = '01'                                                       " & vbNewLine _
                                                   & "             THEN SOKO.ZIP                                                                " & vbNewLine _
                                                   & "             ELSE NRSBR.ZIP                                                               " & vbNewLine _
                                                   & "        END                                                           AS NIOKURININ_ZIP   " & vbNewLine _
                                                   & "      , CASE WHEN SOKO.WH_KB = '01'                                                       " & vbNewLine _
                                                   & "             THEN SOKO.AD_1                                                               " & vbNewLine _
                                                   & "             ELSE NRSBR.AD_1                                                              " & vbNewLine _
                                                   & "        END                                                           AS NIOKURININ_ADD1  " & vbNewLine _
                                                   & "      , CASE WHEN SOKO.WH_KB = '01'                                                       " & vbNewLine _
                                                   & "             THEN SOKO.AD_2                                                               " & vbNewLine _
                                                   & "             ELSE NRSBR.AD_2                                                              " & vbNewLine _
                                                   & "        END                                                           AS NIOKURININ_ADD2  " & vbNewLine _
                                                   & "      , CASE WHEN SOKO.WH_KB = '01'                                                       " & vbNewLine _
                                                   & "             THEN SOKO.AD_3                                                               " & vbNewLine _
                                                   & "             ELSE NRSBR.AD_3                                                              " & vbNewLine _
                                                   & "        END                                                           AS NIOKURININ_ADD3  " & vbNewLine _
                                                   & "      , CASE WHEN SOKO.WH_KB = '01'                                                       " & vbNewLine _
                                                   & "             THEN SOKO.TEL                                                                " & vbNewLine _
                                                   & "             ELSE NRSBR.TEL                                                               " & vbNewLine _
                                                   & "        END                                                           AS NIOKURININ_TEL   " & vbNewLine _
                                                   & "      ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                         " & vbNewLine _
                                                   & "             THEN OKURIJOCSV.FREE_C02                                                     " & vbNewLine _
                                                   & "             ELSE OKURIJOCSVX.FREE_C02                                                    " & vbNewLine _
                                                   & "        END                                                           AS SHIHARAININ_CD   " & vbNewLine _
                                                   & "      , CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                              " & vbNewLine _
                                                   & "             THEN DEST.CUST_DEST_CD                                                       " & vbNewLine _
                                                   & "             ELSE OUTKAL.DEST_CD                                                          " & vbNewLine _
                                                   & "        END                                                           AS NIUKENIN_CD      " & vbNewLine _
                                                   & "      , CASE WHEN OUTKAL.DEST_KB = '01'                                                   " & vbNewLine _
                                                   & "             THEN OUTKAL.DEST_NM                                                          " & vbNewLine _
                                                   & "             WHEN 0 < LEN(DEST.CUST_DEST_CD)                                              " & vbNewLine _
                                                   & "             THEN DEST2.DEST_NM                                                           " & vbNewLine _
                                                   & "             ELSE DEST.DEST_NM                                                            " & vbNewLine _
                                                   & "        END                                                           AS NIUKENIN_MEI1    " & vbNewLine _
                                                   & "      , ''                                                            AS NIUKENIN_MEI2    " & vbNewLine _
                                                   & "      , CASE WHEN OUTKAL.DEST_KB = '01'                                                   " & vbNewLine _
                                                   & "             THEN ''                                                                      " & vbNewLine _
                                                   & "             WHEN 0 < LEN(DEST.CUST_DEST_CD)                                              " & vbNewLine _
                                                   & "             THEN DEST2.ZIP                                                               " & vbNewLine _
                                                   & "             ELSE DEST.ZIP                                                                " & vbNewLine _
                                                   & "        END                                                           AS NIUKENIN_ZIP     " & vbNewLine _
                                                   & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                          " & vbNewLine _
                                                   & "        THEN OUTKAL.DEST_AD_1                                                             " & vbNewLine _
                                                   & "      WHEN OUTKAL.DEST_KB = '02'                                                          " & vbNewLine _
                                                   & "        THEN EDIL.DEST_AD_1                                                               " & vbNewLine _
                                                   & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                     " & vbNewLine _
                                                   & "        THEN DEST2.AD_1                                                                   " & vbNewLine _
                                                   & "      ELSE DEST.AD_1                                                                      " & vbNewLine _
                                                   & "END                                                                   AS NIUKENIN_ADD1    " & vbNewLine _
                                                   & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                          " & vbNewLine _
                                                   & "        THEN OUTKAL.DEST_AD_2								                                " & vbNewLine _
                                                   & "      WHEN OUTKAL.DEST_KB = '02'                                                          " & vbNewLine _
                                                   & "        THEN EDIL.DEST_AD_2								                                " & vbNewLine _
                                                   & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                     " & vbNewLine _
                                                   & "        THEN DEST2.AD_2									                                " & vbNewLine _
                                                   & "      ELSE DEST.AD_2									                                    " & vbNewLine _
                                                   & "END                                                                   AS NIUKENIN_ADD2    " & vbNewLine _
                                                   & ",CASE WHEN OUTKAL.DEST_KB = '01'                                                          " & vbNewLine _
                                                   & "        THEN OUTKAL.DEST_AD_3                                                             " & vbNewLine _
                                                   & "      WHEN OUTKAL.DEST_KB = '02'                                                          " & vbNewLine _
                                                   & "        THEN EDIL.DEST_AD_3                                                               " & vbNewLine _
                                                   & "      WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                     " & vbNewLine _
                                                   & "        THEN OUTKAL.DEST_AD_3                                                             " & vbNewLine _
                                                   & "      ELSE OUTKAL.DEST_AD_3                                                               " & vbNewLine _
                                                   & "END                                                                   AS NIUKENIN_ADD3    " & vbNewLine _
                                                   & "      , OUTKAL.DEST_TEL                                               AS NIUKENIN_TEL     " & vbNewLine _
                                                   & " ,OUTKAL.ARR_PLAN_DATE                                                AS ARR_PLAN_DATE    " & vbNewLine _
                                                   & " ,ZKBN.KBN_NM1                                                        AS ARR_PLAN_TIME    " & vbNewLine _
                                                   & "      , DEST3.DEST_NM AS SHIP_NM_L                                                        " & vbNewLine _
                                                   & "      , CUST.DENPYO_NM AS DENPYO_NM                                                       " & vbNewLine _
                                                   & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                              " & vbNewLine _
                                                   & "       THEN OKURIJOCSV.FREE_C03                                                           " & vbNewLine _
                                                   & "       ELSE OKURIJOCSVX.FREE_C03                                                          " & vbNewLine _
                                                   & "  END                                                                 AS KIJI_1           " & vbNewLine _
                                                   & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                              " & vbNewLine _
                                                   & "       THEN OKURIJOCSV.FREE_C04                                                           " & vbNewLine _
                                                   & "       ELSE OKURIJOCSVX.FREE_C04                                                          " & vbNewLine _
                                                   & "  END                                                                 AS KIJI_2           " & vbNewLine _
                                                   & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                              " & vbNewLine _
                                                   & "       THEN OKURIJOCSV.FREE_C05                                                           " & vbNewLine _
                                                   & "       ELSE OKURIJOCSVX.FREE_C05                                                          " & vbNewLine _
                                                   & "  END                                                                 AS KIJI_3           " & vbNewLine _
                                                   & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                              " & vbNewLine _
                                                   & "       THEN OKURIJOCSV.FREE_C06                                                           " & vbNewLine _
                                                   & "       ELSE OKURIJOCSVX.FREE_C06                                                          " & vbNewLine _
                                                   & "  END                                                                 AS KIJI_4           " & vbNewLine _
                                                   & "      , UNSOL.REMARK                                                  AS KIJI_5           " & vbNewLine _
                                                   & "      , ''                                                            AS KIJI_6           " & vbNewLine _
                                                   & "      , OUTKAL.CUST_ORD_NO                                            AS KIJI_7           " & vbNewLine _
                                                   & "      , ''                                                            AS KIJI_8           " & vbNewLine _
                                                   & "      , OUTKAL.OUTKA_PKG_NB                                           AS KOSU             " & vbNewLine _
                                                   & "      , FLOOR(UNSOL.UNSO_WT)                                          AS JYURYO           " & vbNewLine _
                                                   & "      , '0'                                                           AS YOSEKI           " & vbNewLine _
                                                   & "      , OUTKAL.CUST_CD_L                                              AS CUST_CD_L        " & vbNewLine _
                                                   & "      , CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                              " & vbNewLine _
                                                   & "             THEN DEST2.JIS                                                               " & vbNewLine _
                                                   & "             ELSE DEST.JIS                                                                " & vbNewLine _
                                                   & "        END                                                           AS JIS              " & vbNewLine _
                                                   & "      , CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                              " & vbNewLine _
                                                   & "             THEN DEST2.UNCHIN_SEIQTO_CD                                                  " & vbNewLine _
                                                   & "             ELSE DEST.UNCHIN_SEIQTO_CD                                                   " & vbNewLine _
                                                   & "        END                                                           AS UNCHIN_SEIQTO_CD " & vbNewLine _
                                                   & "      , @ROW_NO                                                       AS ROW_NO           " & vbNewLine _
                                                   & "      , CUST.CUST_NM_L                                                AS CUST_NM_L        " & vbNewLine _
                                                   & "      , CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                              " & vbNewLine _
                                                   & "             THEN DEST.CUST_DEST_CD                                                       " & vbNewLine _
                                                   & "             ELSE OUTKAL.DEST_CD                                                          " & vbNewLine _
                                                   & "        END                                                           AS DEST_CD          " & vbNewLine _
                                                   & "      ,OUTKAL.OUTKA_PLAN_DATE                                         AS OUTKA_PLAN_DATE  " & vbNewLine _
                                                   & "      ,OUTKAL.OUTKA_NO_L                                              AS OUTKA_NO_L       " & vbNewLine _
                                                   & "      ,MCD.SET_NAIYO							                        AS ZIP_PATTERN      " & vbNewLine _
                                                   & "      ,UNSOL.UNSO_NO_L                                                AS UNSO_NO_L        " & vbNewLine

#End Region
#End If

#End Region

#Region "From句・Where句"


    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL FROM・WHERE部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_FROM As String = " FROM                                                                     " & vbNewLine _
                                                       & " $LM_TRN$..C_OUTKA_L OUTKAL                                            " & vbNewLine _
                                                       & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                    " & vbNewLine _
                                                       & "  ON UNSOL.NRS_BR_CD = OUTKAL.NRS_BR_CD                                " & vbNewLine _
                                                       & " AND UNSOL.INOUTKA_NO_L = OUTKAL.OUTKA_NO_L                            " & vbNewLine _
                                                       & " AND UNSOL.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL ON                              " & vbNewLine _
                                                       & " EDIL.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                 " & vbNewLine _
                                                       & " EDIL.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L AND                             " & vbNewLine _
                                                       & " EDIL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_NRS_BR NRSBR ON                                 " & vbNewLine _
                                                       & " NRSBR.NRS_BR_CD = OUTKAL.NRS_BR_CD                                    " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_SOKO SOKO ON                                    " & vbNewLine _
                                                       & " SOKO.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                 " & vbNewLine _
                                                       & " SOKO.WH_CD = OUTKAL.WH_CD                                             " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                    " & vbNewLine _
                                                       & " DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                 " & vbNewLine _
                                                       & " DEST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                 " & vbNewLine _
                                                       & " DEST.DEST_CD = OUTKAL.DEST_CD                                         " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST2 ON                                   " & vbNewLine _
                                                       & " DEST2.NRS_BR_CD = DEST.NRS_BR_CD AND                                  " & vbNewLine _
                                                       & " DEST2.CUST_CD_L = DEST.CUST_CD_L AND                                  " & vbNewLine _
                                                       & " DEST2.DEST_CD = DEST.CUST_DEST_CD                                     " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST3 ON                                   " & vbNewLine _
                                                       & " DEST3.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                " & vbNewLine _
                                                       & " DEST3.CUST_CD_L = OUTKAL.CUST_CD_L AND                                " & vbNewLine _
                                                       & " DEST3.DEST_CD = OUTKAL.SHIP_CD_L                                      " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV ON   --既存マスタJOIN    " & vbNewLine _
                                                       & " OKURIJOCSV.NRS_BR_CD   = UNSOL.NRS_BR_CD AND                          " & vbNewLine _
                                                       & " OKURIJOCSV.UNSOCO_CD   = UNSOL.UNSO_CD   AND                          " & vbNewLine _
                                                       & " OKURIJOCSV.CUST_CD_L   = UNSOL.CUST_CD_L AND                          " & vbNewLine _
                                                       & " OKURIJOCSV.OKURIJO_TP  = '01' AND                                     " & vbNewLine _
                                                       & " OKURIJOCSV.FREE_C11    = UNSOL.UNSO_BR_CD                             " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX ON  --追加マスタJOIN    " & vbNewLine _
                                                       & " OKURIJOCSVX.NRS_BR_CD  = UNSOL.NRS_BR_CD AND                          " & vbNewLine _
                                                       & " OKURIJOCSVX.UNSOCO_CD  = UNSOL.UNSO_CD   AND                          " & vbNewLine _
                                                       & " OKURIJOCSVX.CUST_CD_L  = 'XXXXX'         AND                          " & vbNewLine _
                                                       & " OKURIJOCSVX.OKURIJO_TP = '01'                                         " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                    " & vbNewLine _
                                                       & " CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                 " & vbNewLine _
                                                       & " CUST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                 " & vbNewLine _
                                                       & " CUST.CUST_CD_M = OUTKAL.CUST_CD_M AND                                 " & vbNewLine _
                                                       & " CUST.CUST_CD_S = '00' AND                                             " & vbNewLine _
                                                       & " CUST.CUST_CD_SS = '00'                                                " & vbNewLine _
                                                       & "--荷主によって取扱店が違う場合に使用                                   " & vbNewLine _
                                                       & " LEFT OUTER JOIN                                                       " & vbNewLine _
                                                       & " LM_MST..M_CUST_DETAILS MCD                                            " & vbNewLine _
                                                       & "  ON OUTKAL.NRS_BR_CD = MCD.NRS_BR_CD                                  " & vbNewLine _
                                                       & " AND OUTKAL.CUST_CD_L = MCD.CUST_CD                                    " & vbNewLine _
                                                       & " AND MCD.SUB_KB = '0J'                                                 " & vbNewLine _
                                                       & "LEFT OUTER JOIN                                                        " & vbNewLine _
                                                       & "$LM_MST$..Z_KBN ZKBN                                                   " & vbNewLine _
                                                       & " ON  ZKBN.KBN_GROUP_CD = 'N010'                                        " & vbNewLine _
                                                       & "AND ZKBN.KBN_CD = OUTKAL.ARR_PLAN_TIME                                 " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT OUTER JOIN $LM_MST$..M_UNSO_CUST_RPT MUCR_ALL                     " & vbNewLine _
                                                       & "  ON UNSOL.NRS_BR_CD        = MUCR_ALL.NRS_BR_CD                       " & vbNewLine _
                                                       & " AND UNSOL.UNSO_CD          = MUCR_ALL.UNSOCO_CD                       " & vbNewLine _
                                                       & " AND UNSOL.UNSO_BR_CD       = MUCR_ALL.UNSOCO_BR_CD                    " & vbNewLine _
                                                       & " AND MUCR_ALL.PTN_ID           = '11'                                  " & vbNewLine _
                                                       & " AND UNSOL.PC_KB            = MUCR_ALL.MOTO_TYAKU_KB                   " & vbNewLine _
                                                       & " AND MUCR_ALL.CUST_CD_L        = ''                                    " & vbNewLine _
                                                       & " AND MUCR_ALL.CUST_CD_M        = ''                                    " & vbNewLine _
                                                       & " AND MUCR_ALL.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT OUTER JOIN $LM_MST$..M_RPT      MRPT_ALL                          " & vbNewLine _
                                                       & "  ON MUCR_ALL.NRS_BR_CD        = MRPT_ALL.NRS_BR_CD                    " & vbNewLine _
                                                       & " AND MUCR_ALL.PTN_ID           = MRPT_ALL.PTN_ID                       " & vbNewLine _
                                                       & " AND MUCR_ALL.PTN_CD           = MRPT_ALL.PTN_CD                       " & vbNewLine _
                                                       & " AND MRPT_ALL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT OUTER JOIN $LM_MST$..M_UNSO_CUST_RPT MUCR_CUST                    " & vbNewLine _
                                                       & "  ON UNSOL.NRS_BR_CD        = MUCR_CUST.NRS_BR_CD                      " & vbNewLine _
                                                       & " AND UNSOL.UNSO_CD          = MUCR_CUST.UNSOCO_CD                      " & vbNewLine _
                                                       & " AND UNSOL.UNSO_BR_CD       = MUCR_CUST.UNSOCO_BR_CD                   " & vbNewLine _
                                                       & " AND MUCR_CUST.PTN_ID           = '11'                                 " & vbNewLine _
                                                       & " AND UNSOL.PC_KB            = MUCR_CUST.MOTO_TYAKU_KB                  " & vbNewLine _
                                                       & " AND UNSOL.CUST_CD_L        = MUCR_CUST.CUST_CD_L                      " & vbNewLine _
                                                       & " AND UNSOL.CUST_CD_M        = MUCR_CUST.CUST_CD_M                      " & vbNewLine _
                                                       & " AND MUCR_CUST.SYS_DEL_FLG      = '0'                                  " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT OUTER JOIN $LM_MST$..M_UNSOCO UNSOCO                              " & vbNewLine _
                                                       & "  ON UNSOL.NRS_BR_CD    = UNSOCO.NRS_BR_CD                             " & vbNewLine _
                                                       & " AND UNSOL.UNSO_CD      = UNSOCO.UNSOCO_CD                             " & vbNewLine _
                                                       & " AND UNSOL.UNSO_BR_CD   = UNSOCO.UNSOCO_BR_CD                          " & vbNewLine _
                                                       & " AND UNSOCO.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT OUTER JOIN $LM_MST$..M_RPT MRPT_CUST                              " & vbNewLine _
                                                       & "  ON MUCR_CUST.NRS_BR_CD        = MRPT_CUST.NRS_BR_CD                  " & vbNewLine _
                                                       & " AND MUCR_CUST.PTN_ID           = MRPT_CUST.PTN_ID                     " & vbNewLine _
                                                       & " AND MRPT_CUST.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                       & " AND MUCR_CUST.PTN_CD           = MRPT_CUST.PTN_CD                     " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & " --存在しない場合の帳票パターン取得                                    " & vbNewLine _
                                                       & "LEFT LOOP JOIN LM_MST..M_RPT MR                                        " & vbNewLine _
                                                       & "  ON MR.NRS_BR_CD           = OUTKAL.NRS_BR_CD                         " & vbNewLine _
                                                       & " AND MR.PTN_ID              = '11'                                     " & vbNewLine _
                                                       & " AND MR.STANDARD_FLAG       = '01'                                     " & vbNewLine _
                                                       & " AND MR.SYS_DEL_FLG         = '0'                                      " & vbNewLine _
                                                       & " WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
                                                       & " AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L                                   " & vbNewLine _
                                                       & " AND OUTKAL.SYS_DEL_FLG = '0'                                          " & vbNewLine


    ''' <summary>
    ''' 名鉄運送作成データ検索用SQL FROM・WHERE部　add 2017/03/01
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_UNSO_FROM As String = " FROM                                                                      " & vbNewLine _
                                                        & " $LM_TRN$..F_UNSO_L UNSOL                                                " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_NRS_BR NRSBR ON                                   " & vbNewLine _
                                                        & "       NRSBR.NRS_BR_CD = unsol.NRS_BR_CD                                 " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                      " & vbNewLine _
                                                        & "      DEST.NRS_BR_CD = UNSOL.NRS_BR_CD AND                               " & vbNewLine _
                                                        & "      DEST.CUST_CD_L = UNSOL.CUST_CD_L AND                               " & vbNewLine _
                                                        & "      DEST.DEST_CD   = UNSOL.DEST_CD                                     " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_DEST DEST2 ON                                     " & vbNewLine _
                                                        & "      DEST2.NRS_BR_CD = DEST.NRS_BR_CD AND                               " & vbNewLine _
                                                        & "      DEST2.CUST_CD_L = DEST.CUST_CD_L AND                               " & vbNewLine _
                                                        & "      DEST2.DEST_CD = DEST.CUST_DEST_CD                                  " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV ON   --既存マスタJOIN      " & vbNewLine _
                                                        & "      OKURIJOCSV.NRS_BR_CD   = UNSOL.NRS_BR_CD AND                       " & vbNewLine _
                                                        & "      OKURIJOCSV.UNSOCO_CD   = UNSOL.UNSO_CD   AND                       " & vbNewLine _
                                                        & "      OKURIJOCSV.CUST_CD_L   = UNSOL.CUST_CD_L AND                       " & vbNewLine _
                                                        & "      OKURIJOCSV.OKURIJO_TP  = '01' AND                                  " & vbNewLine _
                                                        & "      OKURIJOCSV.FREE_C11    = UNSOL.UNSO_BR_CD                          " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX ON  --追加マスタJOIN      " & vbNewLine _
                                                        & "      OKURIJOCSVX.NRS_BR_CD  = UNSOL.NRS_BR_CD AND                       " & vbNewLine _
                                                        & "      OKURIJOCSVX.UNSOCO_CD  = UNSOL.UNSO_CD   AND                       " & vbNewLine _
                                                        & "      OKURIJOCSVX.CUST_CD_L  = 'XXXXX'         AND                       " & vbNewLine _
                                                        & "      OKURIJOCSVX.OKURIJO_TP = '01'                                      " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                      " & vbNewLine _
                                                        & "      CUST.NRS_BR_CD = UNSOL.NRS_BR_CD AND                               " & vbNewLine _
                                                        & "      CUST.CUST_CD_L = UNSOL.CUST_CD_L AND                               " & vbNewLine _
                                                        & "      CUST.CUST_CD_M = UNSOL.CUST_CD_M AND                               " & vbNewLine _
                                                        & "      CUST.CUST_CD_S = '00' AND                                          " & vbNewLine _
                                                        & "      CUST.CUST_CD_SS = '00'                                             " & vbNewLine _
                                                        & "--荷主によって取扱店が違う場合に使用                                     " & vbNewLine _
                                                        & " LEFT OUTER JOIN $LM_MST$..M_CUST_DETAILS MCD                            " & vbNewLine _
                                                        & "         ON UNSOL.NRS_BR_CD = MCD.NRS_BR_CD                              " & vbNewLine _
                                                        & "        AND UNSOL.CUST_CD_L = MCD.CUST_CD                                " & vbNewLine _
                                                        & "        AND MCD.SUB_KB = '0J'                                            " & vbNewLine _
                                                        & "LEFT OUTER JOIN                                                          " & vbNewLine _
                                                        & "      $LM_MST$..Z_KBN ZKBN                                               " & vbNewLine _
                                                        & "  ON  ZKBN.KBN_GROUP_CD = 'N010'                                         " & vbNewLine _
                                                        & " AND ZKBN.KBN_CD = UNSOL.ARR_PLAN_TIME                                   " & vbNewLine _
                                                        & "                                                                         " & vbNewLine _
                                                        & " LEFT OUTER JOIN $LM_MST$..M_UNSO_CUST_RPT MUCR_ALL                      " & vbNewLine _
                                                        & "         ON UNSOL.NRS_BR_CD        = MUCR_ALL.NRS_BR_CD                  " & vbNewLine _
                                                        & "        AND UNSOL.UNSO_CD          = MUCR_ALL.UNSOCO_CD                  " & vbNewLine _
                                                        & "        AND UNSOL.UNSO_BR_CD       = MUCR_ALL.UNSOCO_BR_CD               " & vbNewLine _
                                                        & "        AND MUCR_ALL.PTN_ID           = '11'                             " & vbNewLine _
                                                        & "        AND UNSOL.PC_KB            = MUCR_ALL.MOTO_TYAKU_KB              " & vbNewLine _
                                                        & "        AND MUCR_ALL.CUST_CD_L        = ''                               " & vbNewLine _
                                                        & "        AND MUCR_ALL.CUST_CD_M        = ''                               " & vbNewLine _
                                                        & "        AND MUCR_ALL.SYS_DEL_FLG      = '0'                              " & vbNewLine _
                                                        & " LEFT OUTER JOIN $LM_MST$..M_RPT      MRPT_ALL                           " & vbNewLine _
                                                        & "         ON MUCR_ALL.NRS_BR_CD        = MRPT_ALL.NRS_BR_CD               " & vbNewLine _
                                                        & "        AND MUCR_ALL.PTN_ID           = MRPT_ALL.PTN_ID                  " & vbNewLine _
                                                        & "        AND MUCR_ALL.PTN_CD           = MRPT_ALL.PTN_CD                  " & vbNewLine _
                                                        & "        AND MRPT_ALL.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                        & " LEFT OUTER JOIN $LM_MST$..M_UNSO_CUST_RPT MUCR_CUST                     " & vbNewLine _
                                                        & "         ON UNSOL.NRS_BR_CD        = MUCR_CUST.NRS_BR_CD                 " & vbNewLine _
                                                        & "        AND UNSOL.UNSO_CD          = MUCR_CUST.UNSOCO_CD                 " & vbNewLine _
                                                        & "        AND UNSOL.UNSO_BR_CD       = MUCR_CUST.UNSOCO_BR_CD              " & vbNewLine _
                                                        & "        AND MUCR_CUST.PTN_ID           = '11'                            " & vbNewLine _
                                                        & "        AND UNSOL.PC_KB            = MUCR_CUST.MOTO_TYAKU_KB             " & vbNewLine _
                                                        & "        AND UNSOL.CUST_CD_L        = MUCR_CUST.CUST_CD_L                 " & vbNewLine _
                                                        & "        AND UNSOL.CUST_CD_M        = MUCR_CUST.CUST_CD_M                 " & vbNewLine _
                                                        & "        AND MUCR_CUST.SYS_DEL_FLG      = '0'                             " & vbNewLine _
                                                        & " LEFT OUTER JOIN $LM_MST$..M_UNSOCO UNSOCO                               " & vbNewLine _
                                                        & "         ON UNSOL.NRS_BR_CD    = UNSOCO.NRS_BR_CD                        " & vbNewLine _
                                                        & "        AND UNSOL.UNSO_CD      = UNSOCO.UNSOCO_CD                        " & vbNewLine _
                                                        & "        AND UNSOL.UNSO_BR_CD   = UNSOCO.UNSOCO_BR_CD                     " & vbNewLine _
                                                        & "        AND UNSOCO.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                                        & " LEFT OUTER JOIN $LM_MST$..M_RPT MRPT_CUST                               " & vbNewLine _
                                                        & "         ON MUCR_CUST.NRS_BR_CD        = MRPT_CUST.NRS_BR_CD             " & vbNewLine _
                                                        & "        AND MUCR_CUST.PTN_ID           = MRPT_CUST.PTN_ID                " & vbNewLine _
                                                        & "        AND MRPT_CUST.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                        & "        AND MUCR_CUST.PTN_CD           = MRPT_CUST.PTN_CD                " & vbNewLine _
                                                        & " --存在しない場合の帳票パターン取得                                      " & vbNewLine _
                                                        & " LEFT LOOP JOIN $LM_MST$..M_RPT MR                                       " & vbNewLine _
                                                        & "       ON MR.NRS_BR_CD           = UNSOL.NRS_BR_CD                       " & vbNewLine _
                                                        & "      AND MR.PTN_ID              = '11'                                  " & vbNewLine _
                                                        & "      AND MR.STANDARD_FLAG       = '01'                                  " & vbNewLine _
                                                        & "      AND MR.SYS_DEL_FLG         = '0'                                   " & vbNewLine _
                                                        & " WHERE UNSOL.NRS_BR_CD   = @NRS_BR_CD                                    " & vbNewLine _
                                                        & "   AND UNSOL.UNSO_NO_L   = @OUTKA_NO_L                                   " & vbNewLine _
                                                        & "   AND UNSOL.SYS_DEL_FLG = '0'                                           " & vbNewLine

#Region "大阪 (廃棄)"
#If False Then
    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL FROM・WHERE部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_MEITETU_OSAKA As String = " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                        " & vbNewLine _
                                                       & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                                 " & vbNewLine _
                                                       & " OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                                 " & vbNewLine _
                                                       & " OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L AND                                               " & vbNewLine _
                                                       & " OUTKAM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                                       & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL ON                                                " & vbNewLine _
                                                       & " EDIL.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                                   " & vbNewLine _
                                                       & " EDIL.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L AND                                               " & vbNewLine _
                                                       & " EDIL.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                                      " & vbNewLine _
                                                       & " DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                                   " & vbNewLine _
                                                       & " DEST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                                   " & vbNewLine _
                                                       & " DEST.DEST_CD = OUTKAL.DEST_CD                                                           " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST2 ON                                                     " & vbNewLine _
                                                       & " DEST2.NRS_BR_CD = DEST.NRS_BR_CD AND                                                    " & vbNewLine _
                                                       & " DEST2.CUST_CD_L = DEST.CUST_CD_L AND                                                    " & vbNewLine _
                                                       & " DEST2.DEST_CD = DEST.CUST_DEST_CD                                                       " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                                      " & vbNewLine _
                                                       & " CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                                   " & vbNewLine _
                                                       & " CUST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                                   " & vbNewLine _
                                                       & " CUST.CUST_CD_M = OUTKAL.CUST_CD_M AND                                                   " & vbNewLine _
                                                       & " CUST.CUST_CD_S = '00' AND                                                               " & vbNewLine _
                                                       & " CUST.CUST_CD_SS = '00'                                                                  " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                    " & vbNewLine _
                                                       & " OUTKAM.NRS_BR_CD =GOODS.NRS_BR_CD AND                                                   " & vbNewLine _
                                                       & " OUTKAM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                                " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..Z_KBN Z1 ON                                                         " & vbNewLine _
                                                       & " Z1.KBN_GROUP_CD = 'N010' AND                                                            " & vbNewLine _
                                                       & " OUTKAL.ARR_PLAN_TIME = Z1.KBN_CD                                                        " & vbNewLine _
                                                       & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL ON                                                   " & vbNewLine _
                                                       & " UNSOL.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                                  " & vbNewLine _
                                                       & " UNSOL.INOUTKA_NO_L = OUTKAL.OUTKA_NO_L AND                                              " & vbNewLine _
                                                       & " UNSOL.MOTO_DATA_KB = '20' AND                                                           " & vbNewLine _
                                                       & " UNSOL.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                                       & "LEFT OUTER JOIN                                                                          " & vbNewLine _
                                                       & "$LM_MST$..M_UNSOCO UNSOCO                                                                " & vbNewLine _
                                                       & "ON  UNSOL.NRS_BR_CD    = UNSOCO.NRS_BR_CD                                                " & vbNewLine _
                                                       & "AND UNSOL.UNSO_CD      = UNSOCO.UNSOCO_CD                                                " & vbNewLine _
                                                       & "AND UNSOL.UNSO_BR_CD   = UNSOCO.UNSOCO_BR_CD                                             " & vbNewLine _
                                                       & "AND UNSOCO.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                                       & "                                                                                         " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV ON   --既存マスタJOIN    " & vbNewLine _
                                                       & " OKURIJOCSV.NRS_BR_CD   = UNSOL.NRS_BR_CD AND                          " & vbNewLine _
                                                       & " OKURIJOCSV.UNSOCO_CD   = UNSOL.UNSO_CD   AND                          " & vbNewLine _
                                                       & " OKURIJOCSV.CUST_CD_L   = UNSOL.CUST_CD_L AND                          " & vbNewLine _
                                                       & " OKURIJOCSV.OKURIJO_TP  = '01'                                         " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX ON  --追加マスタJOIN    " & vbNewLine _
                                                       & " OKURIJOCSVX.NRS_BR_CD  = UNSOL.NRS_BR_CD AND                          " & vbNewLine _
                                                       & " OKURIJOCSVX.UNSOCO_CD  = UNSOL.UNSO_CD   AND                          " & vbNewLine _
                                                       & " OKURIJOCSVX.CUST_CD_L  = 'XXXXX'         AND                          " & vbNewLine _
                                                       & " OKURIJOCSVX.OKURIJO_TP = '01'                                         " & vbNewLine _
                                                       & "--荷主によって取扱店が違う場合に使用                                   " & vbNewLine _
                                                       & " LEFT OUTER JOIN                                                       " & vbNewLine _
                                                       & " LM_MST..M_CUST_DETAILS MCD                                            " & vbNewLine _
                                                       & " ON  OUTKAL.NRS_BR_CD = MCD.NRS_BR_CD                                  " & vbNewLine _
                                                       & " AND OUTKAL.CUST_CD_L = MCD.CUST_CD                                    " & vbNewLine _
                                                       & " AND MCD.SUB_KB = '0J'                                                 " & vbNewLine _
                                                       & "LEFT OUTER JOIN                                                        " & vbNewLine _
                                                       & "$LM_MST$..Z_KBN ZKBN                                                   " & vbNewLine _
                                                       & "ON  ZKBN.KBN_GROUP_CD = 'N010'                                         " & vbNewLine _
                                                       & "AND ZKBN.KBN_CD = OUTKAL.ARR_PLAN_TIME                                 " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT JOIN $LM_MST$..M_UNSO_CUST_RPT MUCR_ALL                           " & vbNewLine _
                                                       & "  ON UNSOL.NRS_BR_CD        = MUCR_ALL.NRS_BR_CD                       " & vbNewLine _
                                                       & " AND UNSOL.UNSO_CD          = MUCR_ALL.UNSOCO_CD                       " & vbNewLine _
                                                       & " AND UNSOL.UNSO_BR_CD       = MUCR_ALL.UNSOCO_BR_CD                    " & vbNewLine _
                                                       & " AND MUCR_ALL.PTN_ID           = '11'                                  " & vbNewLine _
                                                       & " AND UNSOL.PC_KB            = MUCR_ALL.MOTO_TYAKU_KB                   " & vbNewLine _
                                                       & " AND MUCR_ALL.CUST_CD_L        = ''                                    " & vbNewLine _
                                                       & " AND MUCR_ALL.CUST_CD_M        = ''                                    " & vbNewLine _
                                                       & " AND MUCR_ALL.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT JOIN $LM_MST$..M_RPT      MRPT_ALL                                " & vbNewLine _
                                                       & "  ON MUCR_ALL.NRS_BR_CD        = MRPT_ALL.NRS_BR_CD                    " & vbNewLine _
                                                       & " AND MUCR_ALL.PTN_ID           = MRPT_ALL.PTN_ID                       " & vbNewLine _
                                                       & " AND MUCR_ALL.PTN_CD           = MRPT_ALL.PTN_CD                       " & vbNewLine _
                                                       & " AND MRPT_ALL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT JOIN $LM_MST$..M_UNSO_CUST_RPT MUCR_CUST                          " & vbNewLine _
                                                       & "  ON UNSOL.NRS_BR_CD        = MUCR_CUST.NRS_BR_CD                      " & vbNewLine _
                                                       & " AND UNSOL.UNSO_CD          = MUCR_CUST.UNSOCO_CD                      " & vbNewLine _
                                                       & " AND UNSOL.UNSO_BR_CD       = MUCR_CUST.UNSOCO_BR_CD                   " & vbNewLine _
                                                       & " AND MUCR_CUST.PTN_ID           = '11'                                 " & vbNewLine _
                                                       & " AND UNSOL.PC_KB            = MUCR_CUST.MOTO_TYAKU_KB                  " & vbNewLine _
                                                       & " AND UNSOL.CUST_CD_L        = MUCR_CUST.CUST_CD_L                      " & vbNewLine _
                                                       & " AND UNSOL.CUST_CD_M        = MUCR_CUST.CUST_CD_M                      " & vbNewLine _
                                                       & " AND MUCR_CUST.SYS_DEL_FLG      = '0'                                  " & vbNewLine _
                                                       & "                                                                       " & vbNewLine _
                                                       & "LEFT JOIN $LM_MST$..M_RPT      MRPT_CUST                               " & vbNewLine _
                                                       & "  ON MUCR_CUST.NRS_BR_CD        = MRPT_CUST.NRS_BR_CD                  " & vbNewLine _
                                                       & " AND MUCR_CUST.PTN_ID           = MRPT_CUST.PTN_ID                     " & vbNewLine _
                                                       & " AND MRPT_CUST.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                       & " AND MUCR_CUST.PTN_CD           = MRPT_CUST.PTN_CD                     " & vbNewLine _
                                                       & " WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
                                                       & " AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L                                   " & vbNewLine _
                                                       & " AND OUTKAL.SYS_DEL_FLG = '0'                                          " & vbNewLine
#End If

#End Region

#End Region

#Region "GROUP BY句"

    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL GROUP BY部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_GROUPBY As String = " GROUP BY                                                     " & vbNewLine _
                                                       & "  OUTKAL.NRS_BR_CD                                            " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                           " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PLAN_DATE                                      " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C01                                         " & vbNewLine _
                                                       & " ,NRSBR.NRS_BR_NM                                             " & vbNewLine _
                                                       & " ,NRSBR.ZIP                                                   " & vbNewLine _
                                                       & " ,NRSBR.AD_1                                                  " & vbNewLine _
                                                       & " ,NRSBR.AD_2                                                  " & vbNewLine _
                                                       & " ,NRSBR.AD_3                                                  " & vbNewLine _
                                                       & " ,NRSBR.TEL                                                   " & vbNewLine _
                                                       & " ,SOKO.ZIP                                                    " & vbNewLine _
                                                       & " ,SOKO.AD_1                                                   " & vbNewLine _
                                                       & " ,SOKO.AD_2                                                   " & vbNewLine _
                                                       & " ,SOKO.AD_3                                                   " & vbNewLine _
                                                       & " ,SOKO.TEL                                                    " & vbNewLine _
                                                       & " ,SOKO.WH_KB                                                  " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C02                                         " & vbNewLine _
                                                       & " ,DEST.CUST_DEST_CD                                           " & vbNewLine _
                                                       & " ,OUTKAL.DEST_CD                                              " & vbNewLine _
                                                       & " ,DEST2.DEST_NM                                               " & vbNewLine _
                                                       & " ,DEST.DEST_NM                                                " & vbNewLine _
                                                       & " ,OUTKAL.DEST_NM                                              " & vbNewLine _
                                                       & " ,EDIL.DEST_NM                                                " & vbNewLine _
                                                       & " ,DEST2.ZIP                                                   " & vbNewLine _
                                                       & " ,DEST.ZIP                                                    " & vbNewLine _
                                                       & " ,EDIL.DEST_ZIP                                               " & vbNewLine _
                                                       & " ,DEST2.AD_1                                                  " & vbNewLine _
                                                       & " ,DEST.AD_1                                                   " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_1                                            " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_1                                              " & vbNewLine _
                                                       & " ,DEST2.AD_2                                                  " & vbNewLine _
                                                       & " ,DEST.AD_2                                                   " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_2                                            " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_2                                              " & vbNewLine _
                                                       & " ,OUTKAL.DEST_KB                                              " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_3                                            " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_3                                              " & vbNewLine _
                                                       & " ,DEST2.TEL                                                   " & vbNewLine _
                                                       & " ,DEST.TEL                                                    " & vbNewLine _
                                                       & " ,OUTKAL.DEST_TEL                                             " & vbNewLine _
                                                       & " ,EDIL.DEST_TEL                                               " & vbNewLine _
                                                       & " ,OUTKAL.ARR_PLAN_DATE                                        " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C03                                         " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C04                                         " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C05                                         " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C06                                         " & vbNewLine _
                                                       & " ,UNSOL.REMARK                                                " & vbNewLine _
                                                       & " ,OUTKAL.CUST_ORD_NO                                          " & vbNewLine _
                                                       & " ,UNSOL.UNSO_WT                                               " & vbNewLine _
                                                       & " ,OUTKAL.CUST_CD_L                                            " & vbNewLine _
                                                       & " ,DEST2.JIS                                                   " & vbNewLine _
                                                       & " ,DEST.JIS                                                    " & vbNewLine _
                                                       & " ,DEST3.DEST_NM                                               " & vbNewLine _
                                                       & " ,CUST.DENPYO_NM                                              " & vbNewLine _
                                                       & " ,DEST2.UNCHIN_SEIQTO_CD                                      " & vbNewLine _
                                                       & " ,DEST.UNCHIN_SEIQTO_CD                                       " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_DATE                                         " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_TIME                                         " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PKG_NB                                         " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L                                              " & vbNewLine _
                                                       & " ,OKURIJOCSV.NRS_BR_CD                                        " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C01                                        " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C02                                        " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C03                                        " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C04                                        " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C05                                        " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C06                                        " & vbNewLine _
                                                       & " ,OUTKAL.BUYER_ORD_NO                                         " & vbNewLine _
                                                       & " ,MCD.SET_NAIYO                                               " & vbNewLine _
                                                       & " ,UNSOCO_BR_NM									            " & vbNewLine _
                                                       & " ,UNSOCO.TEL										            " & vbNewLine _
                                                       & " ,UNSOL.AUTO_DENP_NO                                          " & vbNewLine _
                                                       & " ,ZKBN.KBN_NM1                                                " & vbNewLine _
                                                       & " ,UNSOL.UNSO_NO_L                                             " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C10                                         " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C10                                        " & vbNewLine _
                                                       & " ,UNSOL.SYS_UPD_DATE                                          " & vbNewLine _
                                                       & " ,UNSOL.SYS_UPD_TIME                                          " & vbNewLine


#Region "大阪(廃棄)"
#If False Then
    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL GROUPBY部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUPBY_MEITETU_OSAKA As String = " GROUP BY                                                      " & vbNewLine _
                                                       & "  OUTKAL.NRS_BR_CD                                            " & vbNewLine _
                                                       & " ,DEST.CUST_DEST_CD                                           " & vbNewLine _
                                                       & " ,OUTKAL.DEST_CD                                              " & vbNewLine _
                                                       & " ,OUTKAL.DEST_KB                                              " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_1                                            " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_1                                              " & vbNewLine _
                                                       & " ,DEST2.AD_1                                                  " & vbNewLine _
                                                       & " ,DEST.AD_1                                                   " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_2                                            " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_2                                              " & vbNewLine _
                                                       & " ,DEST2.AD_2                                                  " & vbNewLine _
                                                       & " ,DEST.AD_2                                                   " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_3                                            " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_3                                              " & vbNewLine _
                                                       & " ,DEST2.AD_3                                                  " & vbNewLine _
                                                       & " ,DEST.AD_3                                                   " & vbNewLine _
                                                       & " ,OUTKAL.DEST_NM                                              " & vbNewLine _
                                                       & " ,EDIL.DEST_NM                                                " & vbNewLine _
                                                       & " ,DEST2.DEST_NM                                               " & vbNewLine _
                                                       & " ,DEST.DEST_NM                                                " & vbNewLine _
                                                       & " ,OUTKAL.DEST_TEL                                             " & vbNewLine _
                                                       & " ,EDIL.DEST_TEL                                               " & vbNewLine _
                                                       & " ,DEST2.TEL                                                   " & vbNewLine _
                                                       & " ,DEST.TEL                                                    " & vbNewLine _
                                                       & " ,CUST.CUST_CD_L                                              " & vbNewLine _
                                                       & " ,CUST.AD_1                                                   " & vbNewLine _
                                                       & " ,CUST.AD_2                                                   " & vbNewLine _
                                                       & " ,CUST.AD_3                                                   " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L                                              " & vbNewLine _
                                                       & " ,CUST.CUST_NM_M                                              " & vbNewLine _
                                                       & " ,CUST.TEL                                                    " & vbNewLine _
                                                       & " ,OUTKAL.ARR_PLAN_DATE                                        " & vbNewLine _
                                                       & " ,GOODS.GOODS_NM_1                                            " & vbNewLine _
                                                       & " ,OUTKAL.BUYER_ORD_NO                                         " & vbNewLine _
                                                       & " ,Z1.KBN_NM1                                                  " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                           " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_DATE                                         " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_TIME                                         " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PLAN_DATE                                      " & vbNewLine _
                                                       & " ,UNSOL.UNSO_WT                                               " & vbNewLine _
                                                       & " ,DEST.ZIP                                                    " & vbNewLine _
                                                       & " ,MCD.SET_NAIYO                                               " & vbNewLine _
                                                       & " ,UNSOCO_BR_NM									            " & vbNewLine _
                                                       & " ,UNSOCO.TEL										            " & vbNewLine _
                                                       & " ,UNSOL.AUTO_DENP_NO                                          " & vbNewLine _
                                                       & " ,UNSOL.UNSO_NO_L                                             " & vbNewLine _
                                                       & " ,DENPYO_NM                                                   " & vbNewLine _
                                                       & " ,OKURIJOCSV.NRS_BR_CD                                        " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C02                                         " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C02                                        " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C10                                         " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C10                                        " & vbNewLine _
                                                       & " ,OUTKAM.OUTKA_NO_M                                           " & vbNewLine
#End If
#End Region

#End Region

#End Region

#Region "更新 SQL"

#Region "名鉄送り状作成"

    Private Const SQL_UPDATE_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET                  " & vbNewLine _
                                             & " DENP_FLAG         = '01'                         " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                             & "WHERE SYS_DEL_FLG  = '0'                          " & vbNewLine _
                                             & "  AND NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
                                             & "  AND OUTKA_NO_L   = @DENPYO_NO                   " & vbNewLine _
                                             & "  AND SYS_UPD_DATE = @UPD_DATE                    " & vbNewLine _
                                             & "  AND SYS_UPD_TIME = @UPD_TIME                    " & vbNewLine
#If True Then

    Private Const SQL_UPDATE_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET                    " & vbNewLine _
                                         & " AUTO_DENP_NO      = @AUTO_DENP_NO                    " & vbNewLine _
                                         & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                         & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                         & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                         & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                         & "WHERE SYS_DEL_FLG  = '0'                              " & vbNewLine _
                                         & "  AND NRS_BR_CD    = @NRS_BR_CD                       " & vbNewLine _
                                         & "  AND UNSO_NO_L    = @UNSO_NO_L                       " & vbNewLine _
                                         & "  AND SYS_UPD_DATE = @UPD_DATE                        " & vbNewLine _
                                         & "  AND SYS_UPD_TIME = @UPD_TIME                        " & vbNewLine


#End If
#End Region

#End Region

#Region "名鉄郵便番号・区分値取得"

    Private Const SQL_MEITETU_ZIP As String = "SELECT *                                           " & vbNewLine _
                                            & "FROM                                               " & vbNewLine _
                                            & "     LM_MST..M_MEITETSU_ZIP MEITETSU               " & vbNewLine _
                                            & "LEFT OUTER JOIN LM_MST..Z_KBN KBN                  " & vbNewLine _
                                            & "ON 	KBN_GROUP_CD = 'U031'                         " & vbNewLine _
                                            & "AND KBN.KBN_CD = @NRS_BR_CD                        " & vbNewLine _
                                            & "WHERE                                              " & vbNewLine _
                                            & "     MEITETSU.ZIP_NO = @ZIP_NO                     " & vbNewLine

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

#Region "出力対象帳票パターン取得処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt_Label(ByVal ds As DataSet) As DataSet

#If False Then
        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC794IN")
#Else
        Dim inTbl As DataTable = ds.Tables("LMC794OUT")

#End If
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MPrt_Label)               'SQL構築(帳票種別用Select句)  
        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_FROM)             'SQL構築(帳票種別用From句)
        Call setSQLSelect()                                              '条件設定
#If False Then
        Select Case Me._Row.Item("NRS_BR_CD").ToString()
            Case "20"
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MPrt_Label)
                Me._StrSql.Append(LMC794DAC.SQL_FROM_MEITETU_OSAKA)
            Case "10", "30", "40", "50", "60"
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MPrt_Label)
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_FROM)
        End Select
#End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC794DAC", "SelectMPrt_Label", cmd)

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
    Private Function SelectMPrt_LabelUnso(ByVal ds As DataSet) As DataSet

        Dim inTbl As DataTable = ds.Tables("LMC794OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MPrt_Label)               'SQL構築(帳票種別用Select句)  
        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_UNSO_FROM)        'SQL構築(帳票種別用From句)
        Call setSQLSelect()                                              '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC794DAC", "SelectMPrt_Label", cmd)

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
#End Region

#Region "名鉄送り状作成対象検索"

    ''' <summary>
    ''' 名鉄送り状作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>名鉄送り状作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMeitetuLabel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC794IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
#If False Then
        'SQL作成
        'Select文の選択（拠点別）
        Select Case Me._Row.Item("NRS_BR_CD").ToString()
            Case "20"
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_OSAKA)
                Me._StrSql.Append(LMC794DAC.SQL_FROM_MEITETU_OSAKA)
                Me._StrSql.Append(LMC794DAC.SQL_GROUPBY_MEITETU_OSAKA)
            Case "30"
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_GUNMA)
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_FROM)
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_GROUPBY)
            Case "10", "40", "60"
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_YOKOHAMA)
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_FROM)
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_GROUPBY)
            Case "50"
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_SAITAMA)
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_FROM)
                Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_GROUPBY)
        End Select
#Else
        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU)
        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_FROM)
        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_GROUPBY)
#End If


        Call setSQLSelect()                   '条件設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))
#If True Then
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_SYS_UPD_DATE", Me._Row("UNSO_SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_SYS_UPD_TIME", Me._Row("UNSO_SYS_UPD_TIME"), DBDataType.CHAR))
#End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC794DAC", "SelectMeitetuLabel", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        'map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("TEL", "TEL")
        map.Add("AUTO_DENP_NO", "AUTO_DENP_NO")
        map.Add("KIJI_1", "KIJI_1")
        map.Add("KIJI_2", "KIJI_2")
        map.Add("KIJI_3", "KIJI_3")
        map.Add("KIJI_4", "KIJI_4")
        map.Add("KIJI_5", "KIJI_5")
        map.Add("KIJI_6", "KIJI_6")
        map.Add("KIJI_7", "KIJI_7")
        map.Add("KIJI_8", "KIJI_8")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("SHIHARAININ_CD", "SHIHARAININ_CD")
        map.Add("NIUKENIN_CD", "NIUKENIN_CD")
        map.Add("NIUKENIN_ZIP", "NIUKENIN_ZIP")
        map.Add("NIUKENIN_ADD1", "NIUKENIN_ADD1")
        map.Add("NIUKENIN_ADD2", "NIUKENIN_ADD2")
        map.Add("NIUKENIN_ADD3", "NIUKENIN_ADD3")
        map.Add("NIUKENIN_TEL", "NIUKENIN_TEL")
        map.Add("NIUKENIN_MEI1", "NIUKENIN_MEI1")
        map.Add("NIUKENIN_MEI2", "NIUKENIN_MEI2")
        map.Add("NIOKURININ_CD", "NIOKURININ_CD")
        map.Add("NIOKURININ_ZIP", "NIOKURININ_ZIP")
        map.Add("NIOKURININ_ADD1", "NIOKURININ_ADD1")
        map.Add("NIOKURININ_ADD2", "NIOKURININ_ADD2")
        map.Add("NIOKURININ_ADD3", "NIOKURININ_ADD3")
        map.Add("NIOKURININ_TEL", "NIOKURININ_TEL")
        map.Add("NIOKURININ_MEI1", "NIOKURININ_MEI1")
        map.Add("NIOKURININ_MEI2", "NIOKURININ_MEI2")
        map.Add("KOSU", "KOSU")
        map.Add("JYURYO", "JYURYO")
        map.Add("YOSEKI", "YOSEKI")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("DENPYO_NM", "DENPYO_NM")
        map.Add("JIS", "JIS")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("ZIP_PATTERN", "ZIP_PATTERN")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
#If True Then
        map.Add("UNSO_SYS_UPD_DATE", "UNSO_SYS_UPD_DATE")
        map.Add("UNSO_SYS_UPD_TIME", "UNSO_SYS_UPD_TIME")
#End If
        map.Add("GOODS_NM_1", "GOODS_NM_1")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC794OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC794OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

#Region "名鉄運送送り状作成対象検索"

    ''' <summary>
    ''' 名鉄運送送り状作成対象検索 ADD 2017/03/01
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>名鉄送り状作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMeitetuLabelUnso(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC794IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_UNSO)
        Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_UNSO_FROM)
        ''Me._StrSql.Append(LMC794DAC.SQL_SELECT_MEITETU_GROUPBY)

        Call setSQLSelect()                   '条件設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_SYS_UPD_DATE", Me._Row("UNSO_SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_SYS_UPD_TIME", Me._Row("UNSO_SYS_UPD_TIME"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC794DAC", "SelectMeitetuLabelUnso", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        'map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("TEL", "TEL")
        map.Add("AUTO_DENP_NO", "AUTO_DENP_NO")
        map.Add("KIJI_1", "KIJI_1")
        map.Add("KIJI_2", "KIJI_2")
        map.Add("KIJI_3", "KIJI_3")
        map.Add("KIJI_4", "KIJI_4")
        map.Add("KIJI_5", "KIJI_5")
        map.Add("KIJI_6", "KIJI_6")
        map.Add("KIJI_7", "KIJI_7")
        map.Add("KIJI_8", "KIJI_8")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("SHIHARAININ_CD", "SHIHARAININ_CD")
        map.Add("NIUKENIN_CD", "NIUKENIN_CD")
        map.Add("NIUKENIN_ZIP", "NIUKENIN_ZIP")
        map.Add("NIUKENIN_ADD1", "NIUKENIN_ADD1")
        map.Add("NIUKENIN_ADD2", "NIUKENIN_ADD2")
        map.Add("NIUKENIN_ADD3", "NIUKENIN_ADD3")
        map.Add("NIUKENIN_TEL", "NIUKENIN_TEL")
        map.Add("NIUKENIN_MEI1", "NIUKENIN_MEI1")
        map.Add("NIUKENIN_MEI2", "NIUKENIN_MEI2")
        map.Add("NIOKURININ_CD", "NIOKURININ_CD")
        map.Add("NIOKURININ_ZIP", "NIOKURININ_ZIP")
        map.Add("NIOKURININ_ADD1", "NIOKURININ_ADD1")
        map.Add("NIOKURININ_ADD2", "NIOKURININ_ADD2")
        map.Add("NIOKURININ_ADD3", "NIOKURININ_ADD3")
        map.Add("NIOKURININ_TEL", "NIOKURININ_TEL")
        map.Add("NIOKURININ_MEI1", "NIOKURININ_MEI1")
        map.Add("NIOKURININ_MEI2", "NIOKURININ_MEI2")
        map.Add("KOSU", "KOSU")
        map.Add("JYURYO", "JYURYO")
        map.Add("YOSEKI", "YOSEKI")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("DENPYO_NM", "DENPYO_NM")
        map.Add("JIS", "JIS")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("ZIP_PATTERN", "ZIP_PATTERN")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
#If True Then
        map.Add("UNSO_SYS_UPD_DATE", "UNSO_SYS_UPD_DATE")
        map.Add("UNSO_SYS_UPD_TIME", "UNSO_SYS_UPD_TIME")
#End If
        map.Add("GOODS_NM_1", "GOODS_NM_1")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC794OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC794OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function
#End Region
#Region "名鉄郵便番号"
    ''' <summary>
    ''' 名鉄送り状作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>名鉄送り状作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMeitetuZip(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC794OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMC794DAC.SQL_MEITETU_ZIP)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        '郵便番号にハイフンが使用されていた場合、検索に使えないので削除する
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", Me._Row("NIUKENIN_ZIP").ToString.Replace("-", ""), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC794DAC", "SelectMeitetuZip", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("MEITETSU_KEY", "MEITETSU_KEY")
        map.Add("ZIP_NO", "ZIP_NO")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("CHAKUTI_CD_1", "CHAKUTI_CD_1")
        map.Add("SHIWAKE_NO_1", "SHIWAKE_NO_1")
        map.Add("CHAKUTEN_CD_1", "CHAKUTEN_CD_1")
        map.Add("CHAKUTEN_NM_1", "CHAKUTEN_NM_1")
        map.Add("CHAKUTI_CD_2", "CHAKUTI_CD_2")
        map.Add("SHIWAKE_NO_2", "SHIWAKE_NO_2")
        map.Add("CHAKUTEN_CD_2", "CHAKUTEN_CD_2")
        map.Add("CHAKUTEN_NM_2", "CHAKUTEN_NM_2")
        map.Add("CHAKUTI_CD_3", "CHAKUTI_CD_3")
        map.Add("SHIWAKE_NO_3", "SHIWAKE_NO_3")
        map.Add("CHAKUTEN_CD_3", "CHAKUTEN_CD_3")
        map.Add("CHAKUTEN_NM_3", "CHAKUTEN_NM_3")
        map.Add("CHAKUTI_CD_4", "CHAKUTI_CD_4")
        map.Add("SHIWAKE_NO_4", "SHIWAKE_NO_4")
        map.Add("CHAKUTEN_CD_4", "CHAKUTEN_CD_4")
        map.Add("CHAKUTEN_NM_4", "CHAKUTEN_NM_4")
        map.Add("CHAKUTI_CD_5", "CHAKUTI_CD_5")
        map.Add("SHIWAKE_NO_5", "SHIWAKE_NO_5")
        map.Add("CHAKUTEN_CD_5", "CHAKUTEN_CD_5")
        map.Add("CHAKUTEN_NM_5", "CHAKUTEN_NM_5")
        map.Add("CHAKUTI_CD_6", "CHAKUTI_CD_6")
        map.Add("SHIWAKE_NO_6", "SHIWAKE_NO_6")
        map.Add("CHAKUTEN_CD_6", "CHAKUTEN_CD_6")
        map.Add("CHAKUTEN_NM_6", "CHAKUTEN_NM_6")
        map.Add("CHAKUTI_CD_7", "CHAKUTI_CD_7")
        map.Add("SHIWAKE_NO_7", "SHIWAKE_NO_7")
        map.Add("CHAKUTEN_CD_7", "CHAKUTEN_CD_7")
        map.Add("CHAKUTEN_NM_7", "CHAKUTEN_NM_7")
        map.Add("CHAKUTI_CD_8", "CHAKUTI_CD_8")
        map.Add("SHIWAKE_NO_8", "SHIWAKE_NO_8")
        map.Add("CHAKUTEN_CD_8", "CHAKUTEN_CD_8")
        map.Add("CHAKUTEN_NM_8", "CHAKUTEN_NM_8")
        map.Add("CHAKUTI_CD_9", "CHAKUTI_CD_9")
        map.Add("SHIWAKE_NO_9", "SHIWAKE_NO_9")
        map.Add("CHAKUTEN_CD_9", "CHAKUTEN_CD_9")
        map.Add("CHAKUTEN_NM_9", "CHAKUTEN_NM_9")
        map.Add("CHAKUTI_CD_10", "CHAKUTI_CD_10")
        map.Add("SHIWAKE_NO_10", "SHIWAKE_NO_10")
        map.Add("CHAKUTEN_CD_10", "CHAKUTEN_CD_10")
        map.Add("CHAKUTEN_NM_10", "CHAKUTEN_NM_10")
        map.Add("CHAKUTI_CD_11", "CHAKUTI_CD_11")
        map.Add("SHIWAKE_NO_11", "SHIWAKE_NO_11")
        map.Add("CHAKUTEN_CD_11", "CHAKUTEN_CD_11")
        map.Add("CHAKUTEN_NM_11", "CHAKUTEN_NM_11")
        map.Add("CHAKUTI_CD_12", "CHAKUTI_CD_12")
        map.Add("SHIWAKE_NO_12", "SHIWAKE_NO_12")
        map.Add("CHAKUTEN_CD_12", "CHAKUTEN_CD_12")
        map.Add("CHAKUTEN_NM_12", "CHAKUTEN_NM_12")
        map.Add("CHAKUTI_CD_13", "CHAKUTI_CD_13")
        map.Add("SHIWAKE_NO_13", "SHIWAKE_NO_13")
        map.Add("CHAKUTEN_CD_13", "CHAKUTEN_CD_13")
        map.Add("CHAKUTEN_NM_13", "CHAKUTEN_NM_13")
        map.Add("CHAKUTI_CD_14", "CHAKUTI_CD_14")
        map.Add("SHIWAKE_NO_14", "SHIWAKE_NO_14")
        map.Add("CHAKUTEN_CD_14", "CHAKUTEN_CD_14")
        map.Add("CHAKUTEN_NM_14", "CHAKUTEN_NM_14")
        map.Add("CHAKUTI_CD_15", "CHAKUTI_CD_15")
        map.Add("SHIWAKE_NO_15", "SHIWAKE_NO_15")
        map.Add("CHAKUTEN_CD_15", "CHAKUTEN_CD_15")
        map.Add("CHAKUTEN_NM_15", "CHAKUTEN_NM_15")
        map.Add("CHAKUTI_CD_16", "CHAKUTI_CD_16")
        map.Add("SHIWAKE_NO_16", "SHIWAKE_NO_16")
        map.Add("CHAKUTEN_CD_16", "CHAKUTEN_CD_16")
        map.Add("CHAKUTEN_NM_16", "CHAKUTEN_NM_16")
        map.Add("CHAKUTI_CD_17", "CHAKUTI_CD_17")
        map.Add("SHIWAKE_NO_17", "SHIWAKE_NO_17")
        map.Add("CHAKUTEN_CD_17", "CHAKUTEN_CD_17")
        map.Add("CHAKUTEN_NM_17", "CHAKUTEN_NM_17")
        map.Add("CHAKUTI_CD_18", "CHAKUTI_CD_18")
        map.Add("SHIWAKE_NO_18", "SHIWAKE_NO_18")
        map.Add("CHAKUTEN_CD_18", "CHAKUTEN_CD_18")
        map.Add("CHAKUTEN_NM_18", "CHAKUTEN_NM_18")
        map.Add("CHAKUTI_CD_19", "CHAKUTI_CD_19")
        map.Add("SHIWAKE_NO_19", "SHIWAKE_NO_19")
        map.Add("CHAKUTEN_CD_19", "CHAKUTEN_CD_19")
        map.Add("CHAKUTEN_NM_19", "CHAKUTEN_NM_19")
        map.Add("CHAKUTI_CD_20", "CHAKUTI_CD_20")
        map.Add("SHIWAKE_NO_20", "SHIWAKE_NO_20")
        map.Add("CHAKUTEN_CD_20", "CHAKUTEN_CD_20")
        map.Add("CHAKUTEN_NM_20", "CHAKUTEN_NM_20")
        map.Add("CHAKUTI_CD_21", "CHAKUTI_CD_21")
        map.Add("SHIWAKE_NO_21", "SHIWAKE_NO_21")
        map.Add("CHAKUTEN_CD_21", "CHAKUTEN_CD_21")
        map.Add("CHAKUTEN_NM_21", "CHAKUTEN_NM_21")
        map.Add("CHAKUTI_CD_22", "CHAKUTI_CD_22")
        map.Add("SHIWAKE_NO_22", "SHIWAKE_NO_22")
        map.Add("CHAKUTEN_CD_22", "CHAKUTEN_CD_22")
        map.Add("CHAKUTEN_NM_22", "CHAKUTEN_NM_22")
        map.Add("CHAKUTI_CD_23", "CHAKUTI_CD_23")
        map.Add("SHIWAKE_NO_23", "SHIWAKE_NO_23")
        map.Add("CHAKUTEN_CD_23", "CHAKUTEN_CD_23")
        map.Add("CHAKUTEN_NM_23", "CHAKUTEN_NM_23")
        map.Add("CHAKUTI_CD_24", "CHAKUTI_CD_24")
        map.Add("SHIWAKE_NO_24", "SHIWAKE_NO_24")
        map.Add("CHAKUTEN_CD_24", "CHAKUTEN_CD_24")
        map.Add("CHAKUTEN_NM_24", "CHAKUTEN_NM_24")
        map.Add("CHAKUTI_CD_25", "CHAKUTI_CD_25")
        map.Add("SHIWAKE_NO_25", "SHIWAKE_NO_25")
        map.Add("CHAKUTEN_CD_25", "CHAKUTEN_CD_25")
        map.Add("CHAKUTEN_NM_25", "CHAKUTEN_NM_25")
        map.Add("CHAKUTI_CD_26", "CHAKUTI_CD_26")
        map.Add("SHIWAKE_NO_26", "SHIWAKE_NO_26")
        map.Add("CHAKUTEN_CD_26", "CHAKUTEN_CD_26")
        map.Add("CHAKUTEN_NM_26", "CHAKUTEN_NM_26")
        map.Add("CHAKUTI_CD_27", "CHAKUTI_CD_27")
        map.Add("SHIWAKE_NO_27", "SHIWAKE_NO_27")
        map.Add("CHAKUTEN_CD_27", "CHAKUTEN_CD_27")
        map.Add("CHAKUTEN_NM_27", "CHAKUTEN_NM_27")
        map.Add("CHAKUTI_CD_28", "CHAKUTI_CD_28")
        map.Add("SHIWAKE_NO_28", "SHIWAKE_NO_28")
        map.Add("CHAKUTEN_CD_28", "CHAKUTEN_CD_28")
        map.Add("CHAKUTEN_NM_28", "CHAKUTEN_NM_28")
        map.Add("CHAKUTI_CD_29", "CHAKUTI_CD_29")
        map.Add("SHIWAKE_NO_29", "SHIWAKE_NO_29")
        map.Add("CHAKUTEN_CD_29", "CHAKUTEN_CD_29")
        map.Add("CHAKUTEN_NM_29", "CHAKUTEN_NM_29")
        map.Add("CHAKUTI_CD_30", "CHAKUTI_CD_30")
        map.Add("SHIWAKE_NO_30", "SHIWAKE_NO_30")
        map.Add("CHAKUTEN_CD_30", "CHAKUTEN_CD_30")
        map.Add("CHAKUTEN_NM_30", "CHAKUTEN_NM_30")
        map.Add("CHAKUTI_CD_31", "CHAKUTI_CD_31")
        map.Add("SHIWAKE_NO_31", "SHIWAKE_NO_31")
        map.Add("CHAKUTEN_CD_31", "CHAKUTEN_CD_31")
        map.Add("CHAKUTEN_NM_31", "CHAKUTEN_NM_31")
        map.Add("CHAKUTI_CD_32", "CHAKUTI_CD_32")
        map.Add("SHIWAKE_NO_32", "SHIWAKE_NO_32")
        map.Add("CHAKUTEN_CD_32", "CHAKUTEN_CD_32")
        map.Add("CHAKUTEN_NM_32", "CHAKUTEN_NM_32")
        map.Add("CHAKUTI_CD_33", "CHAKUTI_CD_33")
        map.Add("SHIWAKE_NO_33", "SHIWAKE_NO_33")
        map.Add("CHAKUTEN_CD_33", "CHAKUTEN_CD_33")
        map.Add("CHAKUTEN_NM_33", "CHAKUTEN_NM_33")
        map.Add("CHAKUTI_CD_34", "CHAKUTI_CD_34")
        map.Add("SHIWAKE_NO_34", "SHIWAKE_NO_34")
        map.Add("CHAKUTEN_CD_34", "CHAKUTEN_CD_34")
        map.Add("CHAKUTEN_NM_34", "CHAKUTEN_NM_34")
        map.Add("CHAKUTI_CD_35", "CHAKUTI_CD_35")
        map.Add("SHIWAKE_NO_35", "SHIWAKE_NO_35")
        map.Add("CHAKUTEN_CD_35", "CHAKUTEN_CD_35")
        map.Add("CHAKUTEN_NM_35", "CHAKUTEN_NM_35")
        map.Add("CHAKUTI_CD_36", "CHAKUTI_CD_36")
        map.Add("SHIWAKE_NO_36", "SHIWAKE_NO_36")
        map.Add("CHAKUTEN_CD_36", "CHAKUTEN_CD_36")
        map.Add("CHAKUTEN_NM_36", "CHAKUTEN_NM_36")
        map.Add("CHAKUTI_CD_37", "CHAKUTI_CD_37")
        map.Add("SHIWAKE_NO_37", "SHIWAKE_NO_37")
        map.Add("CHAKUTEN_CD_37", "CHAKUTEN_CD_37")
        map.Add("CHAKUTEN_NM_37", "CHAKUTEN_NM_37")
        map.Add("CHAKUTI_CD_38", "CHAKUTI_CD_38")
        map.Add("SHIWAKE_NO_38", "SHIWAKE_NO_38")
        map.Add("CHAKUTEN_CD_38", "CHAKUTEN_CD_38")
        map.Add("CHAKUTEN_NM_38", "CHAKUTEN_NM_38")
        map.Add("CHAKUTI_CD_39", "CHAKUTI_CD_39")
        map.Add("SHIWAKE_NO_39", "SHIWAKE_NO_39")
        map.Add("CHAKUTEN_CD_39", "CHAKUTEN_CD_39")
        map.Add("CHAKUTEN_NM_39", "CHAKUTEN_NM_39")
        map.Add("CHAKUTI_CD_40", "CHAKUTI_CD_40")
        map.Add("SHIWAKE_NO_40", "SHIWAKE_NO_40")
        map.Add("CHAKUTEN_CD_40", "CHAKUTEN_CD_40")
        map.Add("CHAKUTEN_NM_40", "CHAKUTEN_NM_40")
        map.Add("CHAKUTI_CD_41", "CHAKUTI_CD_41")
        map.Add("SHIWAKE_NO_41", "SHIWAKE_NO_41")
        map.Add("CHAKUTEN_CD_41", "CHAKUTEN_CD_41")
        map.Add("CHAKUTEN_NM_41", "CHAKUTEN_NM_41")
        map.Add("CHAKUTI_CD_42", "CHAKUTI_CD_42")
        map.Add("SHIWAKE_NO_42", "SHIWAKE_NO_42")
        map.Add("CHAKUTEN_CD_42", "CHAKUTEN_CD_42")
        map.Add("CHAKUTEN_NM_42", "CHAKUTEN_NM_42")
        map.Add("CHAKUTI_CD_43", "CHAKUTI_CD_43")
        map.Add("SHIWAKE_NO_43", "SHIWAKE_NO_43")
        map.Add("CHAKUTEN_CD_43", "CHAKUTEN_CD_43")
        map.Add("CHAKUTEN_NM_43", "CHAKUTEN_NM_43")
        map.Add("CHAKUTI_CD_44", "CHAKUTI_CD_44")
        map.Add("SHIWAKE_NO_44", "SHIWAKE_NO_44")
        map.Add("CHAKUTEN_CD_44", "CHAKUTEN_CD_44")
        map.Add("CHAKUTEN_NM_44", "CHAKUTEN_NM_44")
        map.Add("CHAKUTI_CD_45", "CHAKUTI_CD_45")
        map.Add("SHIWAKE_NO_45", "SHIWAKE_NO_45")
        map.Add("CHAKUTEN_CD_45", "CHAKUTEN_CD_45")
        map.Add("CHAKUTEN_NM_45", "CHAKUTEN_NM_45")
        map.Add("CHAKUTI_CD_46", "CHAKUTI_CD_46")
        map.Add("SHIWAKE_NO_46", "SHIWAKE_NO_46")
        map.Add("CHAKUTEN_CD_46", "CHAKUTEN_CD_46")
        map.Add("CHAKUTEN_NM_46", "CHAKUTEN_NM_46")
        map.Add("CHAKUTI_CD_47", "CHAKUTI_CD_47")
        map.Add("SHIWAKE_NO_47", "SHIWAKE_NO_47")
        map.Add("CHAKUTEN_CD_47", "CHAKUTEN_CD_47")
        map.Add("CHAKUTEN_NM_47", "CHAKUTEN_NM_47")
        map.Add("CHAKUTI_CD_48", "CHAKUTI_CD_48")
        map.Add("SHIWAKE_NO_48", "SHIWAKE_NO_48")
        map.Add("CHAKUTEN_CD_48", "CHAKUTEN_CD_48")
        map.Add("CHAKUTEN_NM_48", "CHAKUTEN_NM_48")
        map.Add("CHAKUTI_CD_49", "CHAKUTI_CD_49")
        map.Add("SHIWAKE_NO_49", "SHIWAKE_NO_49")
        map.Add("CHAKUTEN_CD_49", "CHAKUTEN_CD_49")
        map.Add("CHAKUTEN_NM_49", "CHAKUTEN_NM_49")
        map.Add("CHAKUTI_CD_50", "CHAKUTI_CD_50")
        map.Add("SHIWAKE_NO_50", "SHIWAKE_NO_50")
        map.Add("CHAKUTEN_CD_50", "CHAKUTEN_CD_50")
        map.Add("CHAKUTEN_NM_50", "CHAKUTEN_NM_50")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("KBN_NM2", "KBN_NM2")
        map.Add("KBN_NM3", "KBN_NM3")
        map.Add("KBN_NM4", "KBN_NM4")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "MEITETSU_ZIP")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("MEITETSU_ZIP").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

    ''' <summary>
    ''' 出荷Lテーブル更新（名鉄送り状作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateMeitetuLabel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC794OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", Me._Row("DENPYO_NO").ToString(), DBDataType.CHAR))
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC794DAC.SQL_UPDATE_OUTKA_L, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC794DAC", "UpdateMeitetuLabel", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function


    ''' <summary>
    ''' 出荷Lテーブル更新（名鉄送り状作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC794IN_UPDATE_UNSO_L").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row("UNSO_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_NO", Me._Row("AUTO_DENP_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me._Row("UNSO_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me._Row("UNSO_SYS_UPD_TIME").ToString(), DBDataType.CHAR))


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC794DAC.SQL_UPDATE_UNSO_L, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC794DAC", "UpdateUnsoL", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

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
    '''  パラメータ設定モジュール（出荷検索）
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region


#End Region

End Class
