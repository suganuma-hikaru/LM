' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF200DAC : 運行未登録運送検索
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF200DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF200DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(SELECT_CNT_UNSO) AS SELECT_CNT FROM(SELECT COUNT(UNSO.UNSO_NO_L)		   AS SELECT_CNT_UNSO ,MIN (UNCHIN.SEIQ_GROUP_NO) AS SEIQ_GROUP_NO,UNSO.UNSO_NO_L  " & vbNewLine

    'START YANAI 要望番号376
    '''' <summary>
    '''' F_UNSO_Lデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT * FROM(SELECT                                                        " & vbNewLine _
    '                                     & "  UNSO.NRS_BR_CD                                    AS  NRS_BR_CD " & vbNewLine _
    '                                     & " ,UNSO.YUSO_BR_CD                                    AS  YUSO_BR_CD " & vbNewLine _
    '                                     & " ,UNSO.UNSO_NO_L                                     AS UNSO_NO_L " & vbNewLine _
    '                                     & " ,UNSO.TRIP_NO                                         AS TRIP_NO " & vbNewLine _
    '                                     & " ,UNSO.TRIP_NO_SYUKA                             AS TRIP_NO_SYUKA " & vbNewLine _
    '                                     & " ,UNSO.TRIP_NO_TYUKEI                           AS TRIP_NO_TYUKEI " & vbNewLine _
    '                                     & " ,UNSO.TRIP_NO_HAIKA                             AS TRIP_NO_HAIKA " & vbNewLine _
    '                                     & " ,UNSO.BIN_KB                                           AS BIN_KB " & vbNewLine _
    '                                     & " ,KBN.KBN_NM1                                           AS BIN_NM " & vbNewLine _
    '                                     & " ,UNSO.AREA_CD                                         AS AREA_CD " & vbNewLine _
    '                                     & " ,AREA.AREA_NM                                         AS AREA_NM " & vbNewLine _
    '                                     & " ,UNSO.ARR_PLAN_DATE                             AS ARR_PLAN_DATE " & vbNewLine _
    '                                     & " ,UNSO.ORIG_CD                                         AS ORIG_CD " & vbNewLine _
    '                                     & " ,DEST.DEST_NM                                         AS ORIG_NM " & vbNewLine _
    '                                     & " ,DEST.AD_1                                       AS ORIG_AD_1 " & vbNewLine _
    '                                     & " ,DEST.JIS                                         AS ORIG_JIS_CD " & vbNewLine _
    '                                     & " ,UNSO.DEST_CD                                        AS DEST_CD " & vbNewLine _
    '                                     & " ,DEST1.DEST_NM                                        AS DEST_NM " & vbNewLine _
    '                                     & " ,DEST1.JIS                                        AS DEST_JIS_CD " & vbNewLine _
    '                                     & " ,DEST1.AD_1                                         AS DEST_AD_1 " & vbNewLine _
    '                                     & " ,UNSO.UNSO_PKG_NB                                 AS UNSO_PKG_NB " & vbNewLine _
    '                                     & " ,UNSO.UNSO_WT                                         AS UNSO_WT " & vbNewLine _
    '                                     & " ,ISNULL(SUM(UNCHIN.DECI_UNCHIN),0)+ISNULL(SUM(UNCHIN.DECI_CITY_EXTC),0)" & vbNewLine _
    '                                     & " +ISNULL(SUM(UNCHIN.DECI_WINT_EXTC),0)+ISNULL(SUM(UNCHIN.DECI_RELY_EXTC),0) +ISNULL(SUM(UNCHIN.DECI_TOLL),0)+ISNULL(SUM(UNCHIN.DECI_INSU),0) AS  UNCHIN        " & vbNewLine _
    '                                     & " ,ISNULL(MAX(UNCHIN.SEIQ_KYORI),0)  AS  SEIQ_KYORI            " & vbNewLine _
    '                                     & " ,MIN (UNCHIN.SEIQ_GROUP_NO)  AS  SEIQ_GROUP_NO  " & vbNewLine _
    '                                     & " ,UNSO.CUST_CD_L                                     AS CUST_CD_L " & vbNewLine _
    '                                     & " ,UNSO.CUST_CD_M                                     AS CUST_CD_M " & vbNewLine _
    '                                     & " ,CUST.CUST_NM_L                                     AS CUST_NM_L " & vbNewLine _
    '                                     & " ,CUST.CUST_NM_M                                     AS CUST_NM_M " & vbNewLine _
    '                                     & " ,UNSO.CUST_REF_NO                                 AS CUST_REF_NO " & vbNewLine _
    '                                     & " ,UNSO.INOUTKA_NO_L                               AS INOUTKA_NO_L " & vbNewLine _
    '                                     & " ,UNSO.MOTO_DATA_KB                               AS MOTO_DATA_KB " & vbNewLine _
    '                                     & " ,KBN1.KBN_NM1                                    AS MOTO_DATA_NM " & vbNewLine _
    '                                     & " ,UNSO.REMARK                                         AS REMARK " & vbNewLine _
    '                                     & " ,UNSO.UNSO_TEHAI_KB                          AS TARIFF_BUNRUI_KB " & vbNewLine _
    '                                     & " ,KBN2.KBN_NM1                                AS TARIFF_BUNRUI_NM " & vbNewLine _
    '                                     & " ,UNSO.UNSO_ONDO_KB                               AS UNSO_ONDO_KB " & vbNewLine _
    '                                     & " ,KBN3.KBN_NM1                                    AS UNSO_ONDO_NM " & vbNewLine _
    '                                     & " ,UNSO.UNSO_CD                                         AS UNSO_CD " & vbNewLine _
    '                                     & " ,UNSO.UNSO_BR_CD                                   AS UNSO_BR_CD " & vbNewLine _
    '                                     & " ,UNSO.TYUKEI_HAISO_FLG                       AS TYUKEI_HAISO_FLG " & vbNewLine _
    '                                     & " ,UNSO.SYUKA_TYUKEI_CD                         AS SYUKA_TYUKEI_CD " & vbNewLine _
    '                                     & " ,JIS1.KEN + JIS1.SHI                                AS SYUKA_TYUKEI_NM " & vbNewLine _
    '                                     & " ,UNSO.HAIKA_TYUKEI_CD                         AS HAIKA_TYUKEI_CD " & vbNewLine _
    '                                     & " ,JIS2.KEN + JIS2.SHI                                AS HAIKA_TYUKEI_NM " & vbNewLine _
    '                                     & " ,UNSOCO.UNSOCO_NM                                     AS UNSO_NM " & vbNewLine _
    '                                     & " ,UNSOCO.UNSOCO_BR_NM                             AS UNSOCO_BR_NM " & vbNewLine _
    '                                     & " ,UNSOCO1.UNSOCO_NM                              AS UNSO_SYUKA_NM " & vbNewLine _
    '                                     & " ,UNSOCO1.UNSOCO_BR_NM                              AS UNSO_SYUKA_BR_NM " & vbNewLine _
    '                                     & " ,UNSOCO2.UNSOCO_NM                             AS UNSO_TYUKEI_NM " & vbNewLine _
    '                                     & " ,UNSOCO2.UNSOCO_BR_NM                             AS UNSO_TYUKEI_BR_NM " & vbNewLine _
    '                                     & " ,UNSOCO3.UNSOCO_NM                              AS UNSO_HAIKA_NM " & vbNewLine _
    '                                     & " ,UNSOCO3.UNSOCO_BR_NM                              AS UNSO_HAIKA_BR_NM " & vbNewLine _
    '                                     & " ,UNSO.SYS_ENT_USER                               AS SYS_ENT_USER " & vbNewLine _
    '                                     & " ,USER1.USER_NM                                AS SYS_ENT_USER_NM " & vbNewLine _
    '                                     & " ,UNSO.SYS_ENT_DATE                               AS SYS_ENT_DATE " & vbNewLine _
    '                                     & " ,UNSO.SYS_UPD_DATE                               AS SYS_UPD_DATE " & vbNewLine _
    '                                     & " ,UNSO.SYS_UPD_TIME                               AS SYS_UPD_TIME " & vbNewLine _
    '                                     & " ,UNSO.SYS_DEL_FLG                                 AS SYS_DEL_FLG " & vbNewLine
    ''' <summary>
    ''' F_UNSO_Lデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT * FROM(SELECT                                                        " & vbNewLine _
                                         & "  UNSO.NRS_BR_CD                                    AS  NRS_BR_CD " & vbNewLine _
                                         & " ,UNSO.YUSO_BR_CD                                    AS  YUSO_BR_CD " & vbNewLine _
                                         & " ,UNSO.UNSO_NO_L                                     AS UNSO_NO_L " & vbNewLine _
                                         & " ,UNSO.TRIP_NO                                         AS TRIP_NO " & vbNewLine _
                                         & " ,UNSO.TRIP_NO_SYUKA                             AS TRIP_NO_SYUKA " & vbNewLine _
                                         & " ,UNSO.TRIP_NO_TYUKEI                           AS TRIP_NO_TYUKEI " & vbNewLine _
                                         & " ,UNSO.TRIP_NO_HAIKA                             AS TRIP_NO_HAIKA " & vbNewLine _
                                         & " ,UNSO.BIN_KB                                           AS BIN_KB " & vbNewLine _
                                         & " ,KBN.KBN_NM1                                           AS BIN_NM " & vbNewLine _
                                         & " ,UNSO.AREA_CD                                         AS AREA_CD " & vbNewLine _
                                         & " ,AREA.AREA_NM                                         AS AREA_NM " & vbNewLine _
                                         & " ,UNSO.ARR_PLAN_DATE                             AS ARR_PLAN_DATE " & vbNewLine _
                                         & " ,UNSO.ORIG_CD                                         AS ORIG_CD " & vbNewLine _
                                         & " ,ISNULL(DEST.DEST_NM,DEST2.DEST_NM)                   AS ORIG_NM " & vbNewLine _
                                         & " ,ISNULL(DEST.AD_1,DEST2.AD_1)                       AS ORIG_AD_1 " & vbNewLine _
                                         & " ,ISNULL(DEST.JIS,DEST2.JIS)                       AS ORIG_JIS_CD " & vbNewLine _
                                         & " ,UNSO.DEST_CD                                         AS DEST_CD " & vbNewLine _
                                         & " ,ISNULL(DEST1.DEST_NM,DEST3.DEST_NM)                  AS DEST_NM " & vbNewLine _
                                         & " ,ISNULL(DEST1.JIS,DEST3.JIS)                      AS DEST_JIS_CD " & vbNewLine _
                                         & " ,ISNULL(DEST1.AD_1,DEST3.AD_1)                      AS DEST_AD_1 " & vbNewLine _
                                         & " ,UNSO.UNSO_PKG_NB                                 AS UNSO_PKG_NB " & vbNewLine _
                                         & " ,UNSO.UNSO_WT                                         AS UNSO_WT " & vbNewLine _
                                         & " ,ISNULL(SUM(UNCHIN.DECI_UNCHIN),0)+ISNULL(SUM(UNCHIN.DECI_CITY_EXTC),0)" & vbNewLine _
                                         & " +ISNULL(SUM(UNCHIN.DECI_WINT_EXTC),0)+ISNULL(SUM(UNCHIN.DECI_RELY_EXTC),0) +ISNULL(SUM(UNCHIN.DECI_TOLL),0)+ISNULL(SUM(UNCHIN.DECI_INSU),0) AS  UNCHIN        " & vbNewLine _
                                         & " ,ISNULL(MAX(UNCHIN.SEIQ_KYORI),0)  AS  SEIQ_KYORI            " & vbNewLine _
                                         & " ,MIN (UNCHIN.SEIQ_GROUP_NO)  AS  SEIQ_GROUP_NO  " & vbNewLine _
                                         & " ,UNSO.CUST_CD_L                                     AS CUST_CD_L " & vbNewLine _
                                         & " ,UNSO.CUST_CD_M                                     AS CUST_CD_M " & vbNewLine _
                                         & " ,CUST.CUST_NM_L                                     AS CUST_NM_L " & vbNewLine _
                                         & " ,CUST.CUST_NM_M                                     AS CUST_NM_M " & vbNewLine _
                                         & " ,UNSO.CUST_REF_NO                                 AS CUST_REF_NO " & vbNewLine _
                                         & " ,UNSO.INOUTKA_NO_L                               AS INOUTKA_NO_L " & vbNewLine _
                                         & " ,UNSO.MOTO_DATA_KB                               AS MOTO_DATA_KB " & vbNewLine _
                                         & " ,KBN1.KBN_NM1                                    AS MOTO_DATA_NM " & vbNewLine _
                                         & " ,UNSO.REMARK                                         AS REMARK " & vbNewLine _
                                         & " ,UNSO.UNSO_TEHAI_KB                          AS TARIFF_BUNRUI_KB " & vbNewLine _
                                         & " ,KBN2.KBN_NM1                                AS TARIFF_BUNRUI_NM " & vbNewLine _
                                         & " ,UNSO.UNSO_ONDO_KB                               AS UNSO_ONDO_KB " & vbNewLine _
                                         & " ,KBN3.KBN_NM1                                    AS UNSO_ONDO_NM " & vbNewLine _
                                         & " ,UNSO.UNSO_CD                                         AS UNSO_CD " & vbNewLine _
                                         & " ,UNSO.UNSO_BR_CD                                   AS UNSO_BR_CD " & vbNewLine _
                                         & " ,UNSO.TYUKEI_HAISO_FLG                       AS TYUKEI_HAISO_FLG " & vbNewLine _
                                         & " ,UNSO.SYUKA_TYUKEI_CD                         AS SYUKA_TYUKEI_CD " & vbNewLine _
                                         & " ,JIS1.KEN + JIS1.SHI                                AS SYUKA_TYUKEI_NM " & vbNewLine _
                                         & " ,UNSO.HAIKA_TYUKEI_CD                         AS HAIKA_TYUKEI_CD " & vbNewLine _
                                         & " ,JIS2.KEN + JIS2.SHI                                AS HAIKA_TYUKEI_NM " & vbNewLine _
                                         & " ,UNSOCO.UNSOCO_NM                                     AS UNSO_NM " & vbNewLine _
                                         & " ,UNSOCO.UNSOCO_BR_NM                             AS UNSOCO_BR_NM " & vbNewLine _
                                         & " ,UNSOCO1.UNSOCO_NM                              AS UNSO_SYUKA_NM " & vbNewLine _
                                         & " ,UNSOCO1.UNSOCO_BR_NM                              AS UNSO_SYUKA_BR_NM " & vbNewLine _
                                         & " ,UNSOCO2.UNSOCO_NM                             AS UNSO_TYUKEI_NM " & vbNewLine _
                                         & " ,UNSOCO2.UNSOCO_BR_NM                             AS UNSO_TYUKEI_BR_NM " & vbNewLine _
                                         & " ,UNSOCO3.UNSOCO_NM                              AS UNSO_HAIKA_NM " & vbNewLine _
                                         & " ,UNSOCO3.UNSOCO_BR_NM                              AS UNSO_HAIKA_BR_NM " & vbNewLine _
                                         & " ,UNSO.SYS_ENT_USER                               AS SYS_ENT_USER " & vbNewLine _
                                         & " ,USER1.USER_NM                                AS SYS_ENT_USER_NM " & vbNewLine _
                                         & " ,UNSO.SYS_ENT_DATE                               AS SYS_ENT_DATE " & vbNewLine _
                                         & " ,UNSO.SYS_UPD_DATE                               AS SYS_UPD_DATE " & vbNewLine _
                                         & " ,UNSO.SYS_UPD_TIME                               AS SYS_UPD_TIME " & vbNewLine _
                                         & " ,UNSO.SYS_DEL_FLG                                 AS SYS_DEL_FLG " & vbNewLine
    'END YANAI 要望番号376

#End Region

#Region "FROM句"

    'START YANAI 要望番号376
    'Private Const SQL_FROM_DATA As String = "FROM                                           " & vbNewLine _
    '                                      & " 	$LM_TRN$..F_UNSO_L AS UNSO	                " & vbNewLine _
    '                                      & "   LEFT OUTER JOIN $LM_TRN$..F_UNSO_LL AS UNSOLL1" & vbNewLine _
    '                                      & " 	ON UNSO.TRIP_NO_SYUKA =UNSOLL1.TRIP_NO	    " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_TRN$..F_UNSO_LL AS UNSOLL2" & vbNewLine _
    '                                      & " 	ON UNSO.TRIP_NO_TYUKEI=UNSOLL2.TRIP_NO	    " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_TRN$..F_UNSO_LL AS UNSOLL3" & vbNewLine _
    '                                      & " 	ON UNSO.TRIP_NO_HAIKA=UNSOLL3.TRIP_NO	    " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN	    " & vbNewLine _
    '                                      & " 	ON UNSO.BIN_KB = KBN.KBN_CD	                " & vbNewLine _
    '                                      & " 	AND KBN.KBN_GROUP_CD = 'U001'	            " & vbNewLine _
    '                                      & " 	AND KBN.SYS_DEL_FLG='0'	                    " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST	" & vbNewLine _
    '                                      & " 	ON UNSO.NRS_BR_CD = DEST.NRS_BR_CD 	        " & vbNewLine _
    '                                      & " 	AND UNSO.CUST_CD_L = DEST.CUST_CD_L 	    " & vbNewLine _
    '                                      & " 	AND UNSO.ORIG_CD =DEST.DEST_CD	            " & vbNewLine _
    '                                      & " 	AND DEST.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN  $LM_MST$..M_DEST AS DEST1  " & vbNewLine _
    '                                      & " 	ON UNSO.NRS_BR_CD = DEST1.NRS_BR_CD 	    " & vbNewLine _
    '                                      & " 	AND UNSO.CUST_CD_L = DEST1.CUST_CD_L 	    " & vbNewLine _
    '                                      & " 	AND UNSO.DEST_CD = DEST1.DEST_CD	        " & vbNewLine _
    '                                      & " 	AND DEST1.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..M_AREA AS AREA	" & vbNewLine _
    '                                      & " 	ON UNSO.NRS_BR_CD = AREA.NRS_BR_CD 	        " & vbNewLine _
    '                                      & " 	AND UNSO.AREA_CD = AREA.AREA_CD 	        " & vbNewLine _
    '                                      & " 	AND DEST1.JIS = AREA.JIS_CD	                " & vbNewLine _
    '                                      & " 	AND AREA.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..M_CUST AS CUST	" & vbNewLine _
    '                                      & " 	ON UNSO.NRS_BR_CD = CUST.NRS_BR_CD 	        " & vbNewLine _
    '                                      & " 	AND UNSO.CUST_CD_L = CUST.CUST_CD_L 	    " & vbNewLine _
    '                                      & " 	AND UNSO.CUST_CD_M = CUST.CUST_CD_M	        " & vbNewLine _
    '                                      & " 	AND CUST.CUST_CD_S = '00'	                " & vbNewLine _
    '                                      & " 	AND CUST.CUST_CD_SS = '00'	                " & vbNewLine _
    '                                      & " 	AND CUST.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN1	    " & vbNewLine _
    '                                      & " 	ON UNSO.MOTO_DATA_KB = KBN1.KBN_CD	        " & vbNewLine _
    '                                      & " 	AND KBN1.KBN_GROUP_CD = 'M004'	            " & vbNewLine _
    '                                      & " 	AND KBN1.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN  $LM_MST$..Z_KBN AS KBN2	" & vbNewLine _
    '                                      & " 	ON UNSO.TARIFF_BUNRUI_KB = KBN2.KBN_CD 	    " & vbNewLine _
    '                                      & " 	AND KBN2.KBN_GROUP_CD = 'T015'	            " & vbNewLine _
    '                                      & " 	AND KBN2.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN3	    " & vbNewLine _
    '                                      & " 	ON UNSO.UNSO_ONDO_KB = KBN3.KBN_CD	        " & vbNewLine _
    '                                      & " 	AND KBN3.KBN_GROUP_CD = 'U006'	            " & vbNewLine _
    '                                      & " 	AND KBN3.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN  $LM_MST$..M_JIS AS JIS1	" & vbNewLine _
    '                                      & " 	ON UNSO.SYUKA_TYUKEI_CD = JIS1.JIS_CD 	    " & vbNewLine _
    '                                      & " 	AND JIS1.SYS_DEL_FLG= '0' 	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..M_JIS AS JIS2	    " & vbNewLine _
    '                                      & " 	ON UNSO.HAIKA_TYUKEI_CD = JIS2.JIS_CD 	    " & vbNewLine _
    '                                      & " 	AND JIS2.SYS_DEL_FLG  = '0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO" & vbNewLine _
    '                                      & " 	ON UNSO.NRS_BR_CD = UNSOCO.NRS_BR_CD 	    " & vbNewLine _
    '                                      & " 	AND UNSO.UNSO_CD = UNSOCO.UNSOCO_CD	        " & vbNewLine _
    '                                      & " 	AND UNSO.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD	" & vbNewLine _
    '                                      & " 	AND UNSOCO.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO1" & vbNewLine _
    '                                      & " 	ON UNSOLL1.NRS_BR_CD = UNSOCO1.NRS_BR_CD 	    " & vbNewLine _
    '                                      & " 	AND UNSOLL1.UNSOCO_CD = UNSOCO1.UNSOCO_CD	        " & vbNewLine _
    '                                      & " 	AND UNSOLL1.UNSOCO_BR_CD = UNSOCO1.UNSOCO_BR_CD	" & vbNewLine _
    '                                      & " 	AND UNSOCO1.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO2" & vbNewLine _
    '                                      & " 	ON UNSOLL2.NRS_BR_CD = UNSOCO2.NRS_BR_CD 	    " & vbNewLine _
    '                                      & " 	AND UNSOLL2.UNSOCO_CD = UNSOCO2.UNSOCO_CD	        " & vbNewLine _
    '                                      & " 	AND UNSOLL2.UNSOCO_BR_CD = UNSOCO2.UNSOCO_BR_CD	" & vbNewLine _
    '                                      & " 	AND UNSOCO2.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO3" & vbNewLine _
    '                                      & " 	ON UNSOLL3.NRS_BR_CD = UNSOCO3.NRS_BR_CD 	    " & vbNewLine _
    '                                      & " 	AND UNSOLL3.UNSOCO_CD = UNSOCO3.UNSOCO_CD	        " & vbNewLine _
    '                                      & " 	AND UNSOLL3.UNSOCO_BR_CD = UNSOCO3.UNSOCO_BR_CD	" & vbNewLine _
    '                                      & " 	AND UNSOCO3.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_MST$..S_USER AS USER1 	" & vbNewLine _
    '                                      & " 	ON UNSO.SYS_ENT_USER = USER1.USER_CD	    " & vbNewLine _
    '                                      & " 	AND USER1.SYS_DEL_FLG='0'	                " & vbNewLine _
    '                                      & " 	LEFT OUTER JOIN $LM_TRN$..F_UNCHIN_TRS AS UNCHIN" & vbNewLine _
    '                                      & " 	ON UNSO.UNSO_NO_L=UNCHIN.UNSO_NO_L	        " & vbNewLine _
    '                                      & " 	AND UNCHIN.SYS_DEL_FLG='0'	                " & vbNewLine
    Private Const SQL_FROM_DATA As String = "FROM                                           " & vbNewLine _
                                          & " 	$LM_TRN$..F_UNSO_L AS UNSO	                " & vbNewLine _
                                          & "   LEFT OUTER JOIN $LM_TRN$..F_UNSO_LL AS UNSOLL1" & vbNewLine _
                                          & " 	ON UNSO.TRIP_NO_SYUKA =UNSOLL1.TRIP_NO	    " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_TRN$..F_UNSO_LL AS UNSOLL2" & vbNewLine _
                                          & " 	ON UNSO.TRIP_NO_TYUKEI=UNSOLL2.TRIP_NO	    " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_TRN$..F_UNSO_LL AS UNSOLL3" & vbNewLine _
                                          & " 	ON UNSO.TRIP_NO_HAIKA=UNSOLL3.TRIP_NO	    " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN	    " & vbNewLine _
                                          & " 	ON UNSO.BIN_KB = KBN.KBN_CD	                " & vbNewLine _
                                          & " 	AND KBN.KBN_GROUP_CD = 'U001'	            " & vbNewLine _
                                          & " 	AND KBN.SYS_DEL_FLG='0'	                    " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST	" & vbNewLine _
                                          & " 	ON UNSO.NRS_BR_CD = DEST.NRS_BR_CD 	        " & vbNewLine _
                                          & " 	AND UNSO.CUST_CD_L = DEST.CUST_CD_L 	    " & vbNewLine _
                                          & " 	AND UNSO.ORIG_CD =DEST.DEST_CD	            " & vbNewLine _
                                          & " 	AND DEST.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN  $LM_MST$..M_DEST AS DEST1  " & vbNewLine _
                                          & " 	ON UNSO.NRS_BR_CD = DEST1.NRS_BR_CD 	    " & vbNewLine _
                                          & " 	AND UNSO.CUST_CD_L = DEST1.CUST_CD_L 	    " & vbNewLine _
                                          & " 	AND UNSO.DEST_CD = DEST1.DEST_CD	        " & vbNewLine _
                                          & " 	AND DEST1.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST2   " & vbNewLine _
                                          & " 	ON UNSO.NRS_BR_CD = DEST2.NRS_BR_CD 	    " & vbNewLine _
                                          & " 	AND 'ZZZZZ' = DEST2.CUST_CD_L 	            " & vbNewLine _
                                          & " 	AND UNSO.ORIG_CD =DEST2.DEST_CD	            " & vbNewLine _
                                          & " 	AND DEST2.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN  $LM_MST$..M_DEST AS DEST3  " & vbNewLine _
                                          & " 	ON UNSO.NRS_BR_CD = DEST3.NRS_BR_CD 	    " & vbNewLine _
                                          & " 	AND 'ZZZZZ' = DEST3.CUST_CD_L 	            " & vbNewLine _
                                          & " 	AND UNSO.DEST_CD = DEST3.DEST_CD	        " & vbNewLine _
                                          & " 	AND DEST3.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_AREA AS AREA	" & vbNewLine _
                                          & " 	ON UNSO.NRS_BR_CD = AREA.NRS_BR_CD 	        " & vbNewLine _
                                          & " 	AND UNSO.AREA_CD = AREA.AREA_CD 	        " & vbNewLine _
                                          & " --要望番号1202 追加START(2012.07.02)          " & vbNewLine _
                                          & "   AND UNSO.BIN_KB = AREA.BIN_KB               " & vbNewLine _
                                          & " --要望番号1202 追加END  (2012.07.02)          " & vbNewLine _
                                          & " 	AND DEST1.JIS = AREA.JIS_CD	                " & vbNewLine _
                                          & " 	AND AREA.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_CUST AS CUST	" & vbNewLine _
                                          & " 	ON UNSO.NRS_BR_CD = CUST.NRS_BR_CD 	        " & vbNewLine _
                                          & " 	AND UNSO.CUST_CD_L = CUST.CUST_CD_L 	    " & vbNewLine _
                                          & " 	AND UNSO.CUST_CD_M = CUST.CUST_CD_M	        " & vbNewLine _
                                          & " 	AND CUST.CUST_CD_S = '00'	                " & vbNewLine _
                                          & " 	AND CUST.CUST_CD_SS = '00'	                " & vbNewLine _
                                          & " 	AND CUST.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN1	    " & vbNewLine _
                                          & " 	ON UNSO.MOTO_DATA_KB = KBN1.KBN_CD	        " & vbNewLine _
                                          & " 	AND KBN1.KBN_GROUP_CD = 'M004'	            " & vbNewLine _
                                          & " 	AND KBN1.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN  $LM_MST$..Z_KBN AS KBN2	" & vbNewLine _
                                          & " 	ON UNSO.TARIFF_BUNRUI_KB = KBN2.KBN_CD 	    " & vbNewLine _
                                          & " 	AND KBN2.KBN_GROUP_CD = 'T015'	            " & vbNewLine _
                                          & " 	AND KBN2.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN3	    " & vbNewLine _
                                          & " 	ON UNSO.UNSO_ONDO_KB = KBN3.KBN_CD	        " & vbNewLine _
                                          & " 	AND KBN3.KBN_GROUP_CD = 'U006'	            " & vbNewLine _
                                          & " 	AND KBN3.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN  $LM_MST$..M_JIS AS JIS1	" & vbNewLine _
                                          & " 	ON UNSO.SYUKA_TYUKEI_CD = JIS1.JIS_CD 	    " & vbNewLine _
                                          & " 	AND JIS1.SYS_DEL_FLG= '0' 	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_JIS AS JIS2	    " & vbNewLine _
                                          & " 	ON UNSO.HAIKA_TYUKEI_CD = JIS2.JIS_CD 	    " & vbNewLine _
                                          & " 	AND JIS2.SYS_DEL_FLG  = '0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO" & vbNewLine _
                                          & " 	ON UNSO.NRS_BR_CD = UNSOCO.NRS_BR_CD 	    " & vbNewLine _
                                          & " 	AND UNSO.UNSO_CD = UNSOCO.UNSOCO_CD	        " & vbNewLine _
                                          & " 	AND UNSO.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD	" & vbNewLine _
                                          & " 	AND UNSOCO.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO1" & vbNewLine _
                                          & " 	ON UNSOLL1.NRS_BR_CD = UNSOCO1.NRS_BR_CD 	    " & vbNewLine _
                                          & " 	AND UNSOLL1.UNSOCO_CD = UNSOCO1.UNSOCO_CD	        " & vbNewLine _
                                          & " 	AND UNSOLL1.UNSOCO_BR_CD = UNSOCO1.UNSOCO_BR_CD	" & vbNewLine _
                                          & " 	AND UNSOCO1.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO2" & vbNewLine _
                                          & " 	ON UNSOLL2.NRS_BR_CD = UNSOCO2.NRS_BR_CD 	    " & vbNewLine _
                                          & " 	AND UNSOLL2.UNSOCO_CD = UNSOCO2.UNSOCO_CD	        " & vbNewLine _
                                          & " 	AND UNSOLL2.UNSOCO_BR_CD = UNSOCO2.UNSOCO_BR_CD	" & vbNewLine _
                                          & " 	AND UNSOCO2.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO3" & vbNewLine _
                                          & " 	ON UNSOLL3.NRS_BR_CD = UNSOCO3.NRS_BR_CD 	    " & vbNewLine _
                                          & " 	AND UNSOLL3.UNSOCO_CD = UNSOCO3.UNSOCO_CD	        " & vbNewLine _
                                          & " 	AND UNSOLL3.UNSOCO_BR_CD = UNSOCO3.UNSOCO_BR_CD	" & vbNewLine _
                                          & " 	AND UNSOCO3.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_MST$..S_USER AS USER1 	" & vbNewLine _
                                          & " 	ON UNSO.SYS_ENT_USER = USER1.USER_CD	    " & vbNewLine _
                                          & " 	AND USER1.SYS_DEL_FLG='0'	                " & vbNewLine _
                                          & " 	LEFT OUTER JOIN $LM_TRN$..F_UNCHIN_TRS AS UNCHIN" & vbNewLine _
                                          & " 	ON UNSO.UNSO_NO_L=UNCHIN.UNSO_NO_L	        " & vbNewLine _
                                          & " 	AND UNCHIN.SYS_DEL_FLG='0'	                " & vbNewLine
    'M_DESTは【M_DESTとM_DEST2】、【M_DEST1とM_DEST3】がペア。
    'END YANAI 要望番号376

#End Region

#Region "GROUP BY"

    'START YANAI 要望番号376
    '''' <summary>
    '''' GROUP BY
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_GROUP_BY As String = " GROUP BY                                                                                                                   " & vbNewLine _
    '                                     & "  UNSO.NRS_BR_CD,UNSO.YUSO_BR_CD,UNSO.UNSO_NO_L,UNSO.TRIP_NO,UNSO.TRIP_NO_SYUKA,UNSO.TRIP_NO_TYUKEI,UNSO.TRIP_NO_HAIKA,UNSO.BIN_KB" & vbNewLine _
    '                                     & "  ,KBN.KBN_NM1,UNSO.AREA_CD,AREA.AREA_NM,UNSO.ARR_PLAN_DATE,UNSO.ORIG_CD,DEST.DEST_NM,DEST.AD_1,DEST.JIS " & vbNewLine _
    '                                     & "  ,UNSO.DEST_CD,DEST1.DEST_NM,DEST1.JIS,DEST1.AD_1,UNSO.UNSO_PKG_NB,UNSO.UNSO_WT,UNSO.CUST_CD_L" & vbNewLine _
    '                                     & "  ,UNSO.CUST_CD_M,CUST.CUST_NM_L,CUST.CUST_NM_M,UNSO.CUST_REF_NO,UNSO.INOUTKA_NO_L,UNSO.MOTO_DATA_KB" & vbNewLine _
    '                                     & "  ,KBN1.KBN_NM1,UNSO.REMARK,UNSO.UNSO_TEHAI_KB,KBN2.KBN_NM1,UNSO.UNSO_ONDO_KB,KBN3.KBN_NM1,UNSO.UNSO_CD" & vbNewLine _
    '                                     & "  ,UNSO.UNSO_BR_CD,UNSO.TYUKEI_HAISO_FLG,UNSO.SYUKA_TYUKEI_CD,JIS1.KEN,JIS1.SHI,UNSO.HAIKA_TYUKEI_CD" & vbNewLine _
    '                                     & "  ,JIS2.KEN,JIS2.SHI,UNSOCO.UNSOCO_NM,UNSOCO1.UNSOCO_NM,UNSOCO2.UNSOCO_NM,UNSOCO3.UNSOCO_NM,UNSO.SYS_ENT_USER" & vbNewLine _
    '                                     & "  ,USER1.USER_NM,UNSO.SYS_ENT_DATE,UNSO.SYS_UPD_DATE,UNSO.SYS_UPD_TIME,UNSO.SYS_DEL_FLG,UNSOCO.UNSOCO_BR_NM,UNSOCO1.UNSOCO_BR_NM,UNSOCO2.UNSOCO_BR_NM,UNSOCO3.UNSOCO_BR_NM)UNSO " & vbNewLine
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                                                                                                                   " & vbNewLine _
                                         & "  UNSO.NRS_BR_CD,UNSO.YUSO_BR_CD,UNSO.UNSO_NO_L,UNSO.TRIP_NO,UNSO.TRIP_NO_SYUKA,UNSO.TRIP_NO_TYUKEI,UNSO.TRIP_NO_HAIKA,UNSO.BIN_KB" & vbNewLine _
                                         & "  ,KBN.KBN_NM1,UNSO.AREA_CD,AREA.AREA_NM,UNSO.ARR_PLAN_DATE,UNSO.ORIG_CD,DEST.DEST_NM,DEST.AD_1,DEST.JIS " & vbNewLine _
                                         & "  ,UNSO.DEST_CD,DEST1.DEST_NM,DEST1.JIS,DEST1.AD_1,UNSO.UNSO_PKG_NB,UNSO.UNSO_WT,UNSO.CUST_CD_L,DEST2.DEST_NM,DEST2.JIS,DEST2.AD_1,DEST3.DEST_NM,DEST3.JIS,DEST3.AD_1" & vbNewLine _
                                         & "  ,UNSO.CUST_CD_M,CUST.CUST_NM_L,CUST.CUST_NM_M,UNSO.CUST_REF_NO,UNSO.INOUTKA_NO_L,UNSO.MOTO_DATA_KB" & vbNewLine _
                                         & "  ,KBN1.KBN_NM1,UNSO.REMARK,UNSO.UNSO_TEHAI_KB,KBN2.KBN_NM1,UNSO.UNSO_ONDO_KB,KBN3.KBN_NM1,UNSO.UNSO_CD" & vbNewLine _
                                         & "  ,UNSO.UNSO_BR_CD,UNSO.TYUKEI_HAISO_FLG,UNSO.SYUKA_TYUKEI_CD,JIS1.KEN,JIS1.SHI,UNSO.HAIKA_TYUKEI_CD" & vbNewLine _
                                         & "  ,JIS2.KEN,JIS2.SHI,UNSOCO.UNSOCO_NM,UNSOCO1.UNSOCO_NM,UNSOCO2.UNSOCO_NM,UNSOCO3.UNSOCO_NM,UNSO.SYS_ENT_USER" & vbNewLine _
                                         & "  ,USER1.USER_NM,UNSO.SYS_ENT_DATE,UNSO.SYS_UPD_DATE,UNSO.SYS_UPD_TIME,UNSO.SYS_DEL_FLG,UNSOCO.UNSOCO_BR_NM,UNSOCO1.UNSOCO_BR_NM,UNSOCO2.UNSOCO_BR_NM,UNSOCO3.UNSOCO_BR_NM)UNSO " & vbNewLine
    'END YANAI 要望番号376

#End Region

#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_COUNT As String = " GROUP BY " & vbNewLine _
                                         & "  UNSO.UNSO_NO_L)UNSO" & vbNewLine



#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                               " & vbNewLine _
                                         & "    UNSO.NRS_BR_CD,UNSO.UNSO_NO_L                   " & vbNewLine

#End Region

#Region "他社倉庫名称取得"

    ''' <summary>
    ''' 他社倉庫名称の取得（出荷）
    ''' </summary>
    Private Const SQL_SELECT_TASYA_WH_NM_OUTKA As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        TOU_SITU.TASYA_WH_NM                                             " & vbNewLine _
        & "   FROM                                                                  " & vbNewLine _
        & "        $LM_TRN$..C_OUTKA_L OUTKA_L                                      " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_TRN$..C_OUTKA_S OUTKA_S                                      " & vbNewLine _
        & "     ON OUTKA_L.NRS_BR_CD                = OUTKA_S.NRS_BR_CD             " & vbNewLine _
        & "    AND OUTKA_L.OUTKA_NO_L               = OUTKA_S.OUTKA_NO_L            " & vbNewLine _
        & "    AND OUTKA_S.SYS_DEL_FLG              = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_TOU_SITU TOU_SITU                                    " & vbNewLine _
        & "     ON OUTKA_L.NRS_BR_CD                = TOU_SITU.NRS_BR_CD            " & vbNewLine _
        & "    AND OUTKA_L.WH_CD                    = TOU_SITU.WH_CD                " & vbNewLine _
        & "    AND OUTKA_S.TOU_NO                   = TOU_SITU.TOU_NO               " & vbNewLine _
        & "    AND OUTKA_S.SITU_NO                  = TOU_SITU.SITU_NO              " & vbNewLine _
        & "    AND TOU_SITU.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "  WHERE OUTKA_L.NRS_BR_CD                = @NRS_BR_CD                    " & vbNewLine _
        & "    AND OUTKA_L.OUTKA_NO_L               = @INOUTKA_NO_L                 " & vbNewLine _
        & "    AND OUTKA_L.SYS_DEL_FLG              = '0'                           " & vbNewLine _
        & "  ORDER BY                                                               " & vbNewLine _
        & "        OUTKA_L.OUTKA_NO_L                                               " & vbNewLine _
        & "      , OUTKA_S.OUTKA_NO_M                                               " & vbNewLine _
        & "      , OUTKA_S.OUTKA_NO_S                                               " & vbNewLine

    ''' <summary>
    ''' 他社倉庫名称の取得（入荷）
    ''' </summary>
    Private Const SQL_SELECT_TASYA_WH_NM_INKA As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        TOU_SITU.TASYA_WH_NM                                             " & vbNewLine _
        & "   FROM                                                                  " & vbNewLine _
        & "        $LM_TRN$..B_INKA_L INKA_L                                        " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_TRN$..B_INKA_S INKA_S                                        " & vbNewLine _
        & "     ON INKA_L.NRS_BR_CD                 = INKA_S.NRS_BR_CD              " & vbNewLine _
        & "    AND INKA_L.INKA_NO_L                 = INKA_S.INKA_NO_L              " & vbNewLine _
        & "    AND INKA_S.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_TOU_SITU TOU_SITU                                    " & vbNewLine _
        & "     ON INKA_L.NRS_BR_CD                 = TOU_SITU.NRS_BR_CD            " & vbNewLine _
        & "    AND INKA_L.WH_CD                     = TOU_SITU.WH_CD                " & vbNewLine _
        & "    AND INKA_S.TOU_NO                    = TOU_SITU.TOU_NO               " & vbNewLine _
        & "    AND INKA_S.SITU_NO                   = TOU_SITU.SITU_NO              " & vbNewLine _
        & "    AND TOU_SITU.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "  WHERE INKA_L.NRS_BR_CD                 = @NRS_BR_CD                    " & vbNewLine _
        & "    AND INKA_L.INKA_NO_L                 = @INOUTKA_NO_L                 " & vbNewLine _
        & "    AND INKA_L.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "  ORDER BY                                                               " & vbNewLine _
        & "        INKA_L.INKA_NO_L                                                 " & vbNewLine _
        & "      , INKA_S.INKA_NO_M                                                 " & vbNewLine _
        & "      , INKA_S.INKA_NO_S                                                 " & vbNewLine

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
    ''' 運送Lテーブル更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送Lテーブル更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF200IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF200DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMF200DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        'LMF030で削除フラグ1のものを検索
        Dim outTbl As DataTable = ds.Tables("F_UNSO_L_IN")
        Call Me.SetConditionBkgContSql(outTbl)

        Me._StrSql.Append(LMF200DAC.SQL_GROUP_BY_COUNT)        'SQL構築(カウント用GROUP_BY句)
        Me.SetConditionWhereSQL()


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF200DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運送Lテーブル更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送Lテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF200IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ設定
        Call Me.SetParamExistChk()

        'SQL作成
        Me._StrSql.Append(LMF200DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF200DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
       
        'LMF030で削除フラグ1のものを検索
        Dim outTbl As DataTable = ds.Tables("F_UNSO_L_IN")
        Call Me.SetConditionBkgContSql(outTbl)
       
        Me._StrSql.Append(LMF200DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        Me.SetConditionWhereSQL()
        Me._StrSql.Append(LMF200DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

  
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF200DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("YUSO_BR_CD", "YUSO_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("TRIP_NO_SYUKA", "TRIP_NO_SYUKA")
        map.Add("TRIP_NO_TYUKEI", "TRIP_NO_TYUKEI")
        map.Add("TRIP_NO_HAIKA", "TRIP_NO_HAIKA")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("AREA_CD", "AREA_CD")
        map.Add("AREA_NM", "AREA_NM")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ORIG_CD", "ORIG_CD")
        map.Add("ORIG_NM", "ORIG_NM")
        map.Add("ORIG_AD_1", "ORIG_AD_1")
        map.Add("ORIG_JIS_CD", "ORIG_JIS_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("UNCHIN", "UNCHIN")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_REF_NO", "CUST_REF_NO")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("MOTO_DATA_NM", "MOTO_DATA_NM")
        map.Add("REMARK", "REMARK")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("TARIFF_BUNRUI_NM", "TARIFF_BUNRUI_NM")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("UNSO_ONDO_NM", "UNSO_ONDO_NM")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("TYUKEI_HAISO_FLG", "TYUKEI_HAISO_FLG")
        map.Add("SYUKA_TYUKEI_CD", "SYUKA_TYUKEI_CD")
        map.Add("SYUKA_TYUKEI_NM", "SYUKA_TYUKEI_NM")
        map.Add("HAIKA_TYUKEI_CD", "HAIKA_TYUKEI_CD")
        map.Add("HAIKA_TYUKEI_NM", "HAIKA_TYUKEI_NM")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("UNSO_SYUKA_NM", "UNSO_SYUKA_NM")
        map.Add("UNSO_SYUKA_BR_NM", "UNSO_SYUKA_BR_NM")
        map.Add("UNSO_TYUKEI_NM", "UNSO_TYUKEI_NM")
        map.Add("UNSO_TYUKEI_BR_NM", "UNSO_TYUKEI_BR_NM")
        map.Add("UNSO_HAIKA_NM", "UNSO_HAIKA_NM")
        map.Add("UNSO_HAIKA_BR_NM", "UNSO_HAIKA_BR_NM")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF200OUT")

        '他社倉庫名称の設定
        ds = SelectTasyaWhNm(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 他社倉庫名称の設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectTasyaWhNm(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF200OUT")

        For Each dr As DataRow In inTbl.Rows
            'SQL格納変数の初期化
            Dim strSql As New StringBuilder()

            If Not String.IsNullOrEmpty(dr.Item("INOUTKA_NO_L").ToString()) Then
                '入出荷管理番号Lに値がある
                If dr.Item("ORIG_CD").ToString() = "999999999999999" Then
                    '出荷から取得
                    strSql.Append(LMF200DAC.SQL_SELECT_TASYA_WH_NM_OUTKA)
                ElseIf dr.Item("DEST_CD").ToString() = "999999999999999" Then
                    '入荷から取得
                    strSql.Append(LMF200DAC.SQL_SELECT_TASYA_WH_NM_INKA)
                End If

                If String.IsNullOrEmpty(strSql.ToString()) Then
                    Continue For
                End If

                'SQLパラメータ設定
                Me._SqlPrmList = New ArrayList()
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dr.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", dr.Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

                'SQL文のコンパイル
                Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(strSql.ToString(), dr.Item("NRS_BR_CD").ToString()))

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMF200DAC", "SelectTasyaWhNm", cmd)

                'テーブルクリア
                ds.Tables("TASYA_WH_NM").Rows.Clear()

                'SQLの発行
                Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)
                    If reader.HasRows Then
                        '取得データの格納先をマッピング
                        Dim map As Hashtable = New Hashtable()
                        For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                            If (ds.Tables("TASYA_WH_NM").Columns.Contains(item)) Then
                                map.Add(item, item)
                            End If
                        Next

                        'DataReader→DataTableへの転記
                        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "TASYA_WH_NM")
                    End If
                End Using

                If ds.Tables("TASYA_WH_NM").Rows.Count > 0 Then
                    dr.Item("TASYA_WH_NM") = ds.Tables("TASYA_WH_NM").Rows(0).Item("TASYA_WH_NM").ToString()
                End If
            End If
        Next

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
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.NRS_BR_CD = @NRS_BR_CD  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("YUSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.YUSO_BR_CD = @YUSO_BR_CD  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.UNSO_CD = @UNSO_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.UNSO_BR_CD = @UNSO_BR_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_NM + '　' + UNSOCO.UNSOCO_BR_NM LIKE @UNSO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("ARR_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.ARR_PLAN_DATE >= @ARR_PLAN_DATE_FROM ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("ARR_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.ARR_PLAN_DATE <= @ARR_PLAN_DATE_TO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("UNSO_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.UNSO_NO_L LIKE @UNSO_NO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If


            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.BIN_KB = @BIN_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TARIFF_BUNRUI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_REF_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.CUST_REF_NO LIKE @CUST_REF_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If


            whereStr = .Item("ORIG_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.DEST_NM LIKE @ORIG_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
#If False Then   'ADD 2022/09/01 032102   【LMS】運行・運送画面の改修要望
                andstr.Append(" DEST1.DEST_NM LIKE @DEST_NO")
#Else
                andstr.Append("( DEST1.DEST_NM LIKE @DEST_NO")
                andstr.Append(" OR DEST1.DEST_NM is null AND DEST3.DEST_NM LIKE @DEST_NO)")

#End If
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("AREA_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" AREA.AREA_NM LIKE @AREA_NM  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("KANRI_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.INOUTKA_NO_L  LIKE @KANRI_NO_L  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If


            whereStr = .Item("CUST_L_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_NM_L + '　' + CUST.CUST_NM_M LIKE @CUST_L_NM  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_L_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.REMARK LIKE @REMARK  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SEIQ_GROUP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("UNSO_ONDO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.UNSO_ONDO_KB = @UNSO_ONDO_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("MOTO_DATA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.MOTO_DATA_KB = @MOTO_DATA_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SYUKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" JIS1.KEN + JIS1.SHI LIKE @SYUKA_TYUKEI_NM  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("HAIKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" JIS2.KEN + JIS2.SHI LIKE @HAIKA_TYUKEI_NM  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("TRIP_NO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.TRIP_NO_SYUKA LIKE @TRIP_NO_SYUKA  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TRIP_NO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.TRIP_NO_TYUKEI LIKE @TRIP_NO_TYUKEI  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TRIP_NO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.TRIP_NO_HAIKA LIKE @TRIP_NO_HAIKA  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("UNSO_SYUKA_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO1.UNSOCO_NM + '　' + UNSOCO1.UNSOCO_BR_NM LIKE @UNSO_SYUKA_NM  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_SYUKA_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSO_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO2.UNSOCO_NM + '　' + UNSOCO2.UNSOCO_BR_NM LIKE @UNSO_TYUKEI_NM  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TYUKEI_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSO_HAIKA_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO3.UNSOCO_NM + '　' + UNSOCO3.UNSOCO_BR_NM LIKE @UNSO_HAIKA_NM  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_HAIKA_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

#If True Then   'ADD 2022/09/01 032102   【LMS】運行・運送画面の改修要望
            whereStr = .Item("DEST_ADD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("( DEST1.AD_1 LIKE @DEST_ADD")
                andstr.Append(" OR DEST1.AD_1 is null AND DEST3.AD_1 LIKE @DEST_ADD)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ADD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

#End If
            '(2013.01.25)要望番号1503 日陸手配のみ抽出 -- START --
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" UNSO.UNSO_TEHAI_KB = '10'  ")
            andstr.Append(vbNewLine)
            '(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub


    Private Sub SetConditionWhereSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SEIQ_GROUP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.SEIQ_GROUP_NO LIKE @SEIQ_GROUP_NO ")
                andstr.Append(vbNewLine)

            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(運送Lテーブル存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NM", .Item("UNSO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE_FROM", .Item("ARR_PLAN_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE_TO", .Item("ARR_PLAN_DATE_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO", .Item("UNSO_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_NM", .Item("ORIG_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NO", .Item("DEST_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_NM", .Item("AREA_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", .Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_L_NM", .Item("CUST_L_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_NM", .Item("SYUKA_TYUKEI_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_NM", .Item("HAIKA_TYUKEI_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_SYUKA_NM", .Item("UNSO_SYUKA_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TYUKEI_NM", .Item("UNSO_TYUKEI_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_HAIKA_NM", .Item("UNSO_HAIKA_NM").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定
    ''' </summary>
    ''' <param name="dt">データテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetConditionBkgContSql(ByVal dt As DataTable)

        Dim dr As DataRow() = dt.Select("SYS_DEL_FLG = '1'")
        Dim max As Integer = dr.Length - 1

        If -1 < max Then

            'Booking編集画面でDelete処理したレコードを再度表示する
            For i As Integer = 0 To max

                Call Me.SetLastJobContData(dr(i), String.Concat("@UNSO_NO_L", i.ToString()))

            Next
        Else

            '中継フラグが00の時TRIP_NO''、中継フラグ01のときTRIP_SYUKA''またはTRIP_NO_TYUKEIまたはTRP_NO_HAIKA''
            _StrSql.Append(" AND ((UNSO.TYUKEI_HAISO_FLG ='00' AND UNSO.TRIP_NO ='') OR (UNSO.TYUKEI_HAISO_FLG='01' AND (UNSO.TRIP_NO_SYUKA='' OR UNSO.TRIP_NO_TYUKEI='' OR UNSO.TRIP_NO_HAIKA='' ))) ")

        End If

    End Sub

    ''' <summary>
    ''' 前回Jobの条件設定
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <param name="str1">パラメータ文字1</param>
    ''' <remarks></remarks>
    Private Sub SetLastJobContData(ByVal dr As DataRow, ByVal str1 As String)

        'LMF030の運送番号L=運送番号L
        _StrSql.Append("                            OR UNSO.UNSO_NO_L         = ")
        _StrSql.Append(str1)
        _StrSql.Append(vbNewLine)
        _SqlPrmList.Add(GetSqlParameter(str1, dr.Item("UNSO_NO_L").ToString, DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
