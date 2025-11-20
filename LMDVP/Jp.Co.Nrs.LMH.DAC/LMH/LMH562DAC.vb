' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH562    : EDI入出荷受信帳票(日産物流)
'  作  成  者       :  黎
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH562DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH562DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       HED.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , 'AM'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine
    ''' <summary>
    ''' 帳票種別取得用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' ダウケミEDI受信データHEAD - ダウケミEDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_NSN  HED                              " & vbNewLine _
                                            & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
                                            & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
                                            & "      LEFT JOIN (                                                   " & vbNewLine _
                                            & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                            & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                            & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                            & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                            & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                            & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                            & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                            & "                     AND H_EDI_PRINT.PRINT_TP    = '02'             " & vbNewLine _
                                            & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                            & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                            & "                   GROUP BY                                         " & vbNewLine _
                                            & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                            & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                            & "                ) HEDIPRINT                                         " & vbNewLine _
                                            & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
                                            & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
                                            & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                            & "      -- 日産物流EDI受信データ                                      " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN  DTL             " & vbNewLine _
                                            & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
                                            & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
                                            & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
                                            & "      -- 商品マスタ                                                 " & vbNewLine _
                                            & "     LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                      " & vbNewLine _
                                            & "     ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD                      " & vbNewLine _
                                            & " --20121105品名改修版(顧客側品名コード判断)開始                     " & vbNewLine _
                                            & "     AND M_GOODS.GOODS_CD_CUST = CASE WHEN HED.HINSHU_CD + HED.HINMEI_CD LIKE '17%'   " & vbNewLine _
                                            & "     THEN HED.HINSHU_CD + HED.HINMEI_CD + HED.GRADE								     " & vbNewLine _
                                            & "     ELSE HED.HINSHU_CD + HED.HINMEI_CD											     " & vbNewLine _
                                            & "     END																			     " & vbNewLine _
                                            & " --20121105品名改修版(顧客側品名コード判断)終了                     " & vbNewLine _
                                            & "      -- 帳票パターンマスタ①(H_OUTKAEDI_HED_NSNの荷主より取得)     " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                            & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L           " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.CUST_CD_M   = '00'                 " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.PTN_ID      = 'AM'                 " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
                                            & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD " & vbNewLine _
                                            & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    " & vbNewLine _
                                            & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    " & vbNewLine _
                                            & "                  AND MR1.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                            & "      -- 帳票パターンマスタ②(商品マスタより)                       " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               " & vbNewLine _
                                            & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    " & vbNewLine _
                                            & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    " & vbNewLine _
                                            & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    " & vbNewLine _
                                            & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 " & vbNewLine _
                                            & "                  AND M_CUSTRPT2.PTN_ID      = 'AM'                 " & vbNewLine _
                                            & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                            & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                            & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                            & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                            & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                            & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                            & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                            & "                  AND MR3.PTN_ID             = 'AM'                 " & vbNewLine _
                                            & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                            & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                   " & vbNewLine _
                                            & "       HED.NRS_BR_CD     =  @NRS_BR_CD  " & vbNewLine _
                                            & "   AND  '00145'  =  @CUST_CD_L          " & vbNewLine _
                                            & "   AND  '00'     =  @CUST_CD_M          " & vbNewLine
    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                     " & vbNewLine _
                                        & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID   " & vbNewLine _
                                        & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   " & vbNewLine _
                                        & "        ELSE MR3.RPT_ID END        AS RPT_ID               " & vbNewLine _
                                        & "      , HED.DEL_KB                 AS H_DEL_KB             " & vbNewLine _
                                        & "      , HED.CRT_DATE               AS CRT_DATE             " & vbNewLine _
                                        & "      , HED.FILE_NAME              AS FILE_NAME            " & vbNewLine _
                                        & "      , HED.REC_NO                 AS REC_NO               " & vbNewLine _
                                        & "      , HED.NRS_BR_CD              AS NRS_BR_CD            " & vbNewLine _
                                        & "      , HED.EDI_CTL_NO             AS EDI_CTL_NO           " & vbNewLine _
                                        & "      , HED.SHORI_KB               AS SHORI_KB             " & vbNewLine _
                                        & "      , HED.CUST_NM                AS CUST_NM              " & vbNewLine _
                                        & "      , HED.CUST_SHOZOKU           AS CUST_SHOZOKU         " & vbNewLine _
                                        & "      , HED.CUST_TANTO_NM          AS CUST_TANTO_NM        " & vbNewLine _
                                        & "      , HED.EIGYO_SASHIZU          AS EIGYO_SASHIZU        " & vbNewLine _
                                        & "      , HED.KOJO_KANRI_NO          AS KOJO_KANRI_NO        " & vbNewLine _
                                        & "      , HED.SHUKKA_DATE            AS SHUKKA_DATE          " & vbNewLine _
                                        & "      , HED.NONYU_DATE             AS NONYU_DATE           " & vbNewLine _
                                        & "      , HED.NONYU_TIME             AS NONYU_TIME           " & vbNewLine _
                                        & "      , HED.HINSHU_CD              AS HINSHU_CD            " & vbNewLine _
                                        & "      , HED.HINMEI_CD              AS HINMEI_CD            " & vbNewLine _
                                        & "      , HED.GRADE                  AS GRADE                " & vbNewLine _
                                        & "      , HED.SHANAI_HINMEI          AS SHANAI_HINMEI        " & vbNewLine _
                                        & "      , HED.HINMEI                 AS HINMEI               " & vbNewLine _
                                        & "      , HED.NISUGATA_NM            AS NISUGATA_NM          " & vbNewLine _
                                        & "      , HED.YORYO                  AS YORYO                " & vbNewLine _
                                        & "      , HED.TAN_I                  AS TAN_I                " & vbNewLine _
                                        & "      , HED.KOSU                   AS KOSU                 " & vbNewLine _
                                        & "      , HED.SURYO                  AS SURYO                " & vbNewLine _
                                        & "      , HED.TORIHIKI_NO            AS TORIHIKI_NO          " & vbNewLine _
                                        & "      , HED.DEST_CD                AS DEST_CD              " & vbNewLine _
                                        & "      , HED.DEST_NM                AS DEST_NM              " & vbNewLine _
                                        & "      , HED.DEST_SHOZOKU           AS DEST_SHOZOKU         " & vbNewLine _
                                        & "      , HED.DEST_AD_1              AS DEST_AD_1            " & vbNewLine _
                                        & "      , HED.DEST_AD_2              AS DEST_AD_2            " & vbNewLine _
                                        & "      , HED.DEST_ZIP               AS DEST_ZIP             " & vbNewLine _
                                        & "      , HED.DEST_TEL               AS DEST_TEL             " & vbNewLine _
                                        & "      , HED.SHUKKA_NM              AS SHUKKA_NM            " & vbNewLine _
                                        & "      , HED.YUSO_NM                AS YUSO_NM              " & vbNewLine _
                                        & "      , HED.COMMENT                AS COMMENT              " & vbNewLine _
                                        & "      , HED.DENP_COMMENT           AS DENP_COMMENT         " & vbNewLine _
                                        & "      , HED.HAISHA_COMMENT         AS HAISHA_COMMENT       " & vbNewLine _
                                        & "      , HED.NRS_BUNSEKI            AS NRS_BUNSEKI          " & vbNewLine _
                                        & "      , HED.NRS_NONYU              AS NRS_NONYU            " & vbNewLine _
                                        & "      , HED.EDI_DATE               AS EDI_DATE             " & vbNewLine _
                                        & "      , HED.EDI_TIME               AS EDI_TIME             " & vbNewLine _
                                        & " 	 ,CASE WHEN G01.LOT_NO01 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G01.LOT_NO01                                    " & vbNewLine _
                                        & "      END AS LOT_NO01                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G01.KOSU_SURYO01 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G01.KOSU_SURYO01                                " & vbNewLine _
                                        & "      END AS KOSU_SURYO01                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G02.LOT_NO02 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G02.LOT_NO02                                    " & vbNewLine _
                                        & " 	 END AS LOT_NO02                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G02.KOSU_SURYO02 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G02.KOSU_SURYO02                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO02                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G03.LOT_NO03 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G03.LOT_NO03                                    " & vbNewLine _
                                        & " 	 END AS LOT_NO03                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G03.KOSU_SURYO03 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G03.KOSU_SURYO03                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO03                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G04.LOT_NO04 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G04.LOT_NO04                                    " & vbNewLine _
                                        & "    	 END AS LOT_NO04                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G04.KOSU_SURYO04 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G04.KOSU_SURYO04                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO04                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G05.LOT_NO05 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G05.LOT_NO05                                    " & vbNewLine _
                                        & " 	 END AS LOT_NO05                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G05.KOSU_SURYO05 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G05.KOSU_SURYO05                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO05                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G06.LOT_NO06 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G06.LOT_NO06                                    " & vbNewLine _
                                        & " 	 END AS LOT_NO06                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G06.KOSU_SURYO06 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G06.KOSU_SURYO06                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO06                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G07.LOT_NO07 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G07.LOT_NO07                                    " & vbNewLine _
                                        & " 	 END AS LOT_NO07                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G07.KOSU_SURYO07 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G07.KOSU_SURYO07                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO07                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G08.LOT_NO08 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G08.LOT_NO08                                    " & vbNewLine _
                                        & " 	 END AS LOT_NO08                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G08.KOSU_SURYO08 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G08.KOSU_SURYO08                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO08                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G09.LOT_NO09 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G09.LOT_NO09                                    " & vbNewLine _
                                        & " 	 END AS LOT_NO09                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G09.KOSU_SURYO09 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G09.KOSU_SURYO09                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO09                                  " & vbNewLine _
                                        & " 	 ,CASE WHEN G10.LOT_NO10 IS NULL THEN ''              " & vbNewLine _
                                        & " 	 ELSE G10.LOT_NO10                                    " & vbNewLine _
                                        & " 	 END AS LOT_NO10                                      " & vbNewLine _
                                        & " 	 ,CASE WHEN G10.KOSU_SURYO10 IS NULL THEN 0           " & vbNewLine _
                                        & " 	 ELSE G10.KOSU_SURYO10                                " & vbNewLine _
                                        & " 	 END AS KOSU_SURYO10                                  " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' 日産物流EDI受信データHEAD - 日産物流EDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_NSN  HED                                       " & vbNewLine _
                                    & " --【Notes】№1007/1008対応 --- START ---                                       " & vbNewLine _
                                    & "      -- EDI印刷種別テーブル                                                    " & vbNewLine _
                                    & "      LEFT JOIN (                                                               " & vbNewLine _
                                    & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                       " & vbNewLine _
                                    & "                       , H_EDI_PRINT.NRS_BR_CD                                  " & vbNewLine _
                                    & "                       , H_EDI_PRINT.EDI_CTL_NO                                 " & vbNewLine _
                                    & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                      " & vbNewLine _
                                    & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                   " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                   " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                   " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.PRINT_TP    = '02'                         " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB                    " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                    & "                   GROUP BY                                                     " & vbNewLine _
                                    & "                         H_EDI_PRINT.NRS_BR_CD                                  " & vbNewLine _
                                    & "                       , H_EDI_PRINT.EDI_CTL_NO                                 " & vbNewLine _
                                    & "                ) HEDIPRINT                                                     " & vbNewLine _
                                    & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                            " & vbNewLine _
                                    & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO                           " & vbNewLine _
                                    & " --【Notes】№1007/1008対応 ---  END  ---                                       " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN  DTL                         " & vbNewLine _
                                    & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                              " & vbNewLine _
                                    & "                  AND DTL.FILE_NAME = HED.FILE_NAME                             " & vbNewLine _
                                    & "                  AND DTL.REC_NO    = HED.REC_NO                                " & vbNewLine _
                                    & "	--本家本元 LEFT OUTER JOIN 01--                                                " & vbNewLine _
                                    & "		LEFT OUTER JOIN                                                            " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO01 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO01   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED                               	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON                 " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE                                            " & vbNewLine _
                                    & "		AND                                                                        " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME                                          " & vbNewLine _
                                    & "		AND                                                                    	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO                                                " & vbNewLine _
                                    & "		WHERE                                                                  	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND                                                                    	   " & vbNewLine _
                                    & "			DTL.GYO = '01'                                                         " & vbNewLine _
                                    & "			) 															AS G01 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G01.CRT_DATE                                            " & vbNewLine _
                                    & "		AND                                                                    	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G01.FILE_NAME                                          " & vbNewLine _
                                    & "		AND  																   	   " & vbNewLine _
                                    & "			HED.REC_NO = G01.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 02--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO02 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO02  											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON  			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE  										   " & vbNewLine _
                                    & "		AND 																   	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																   	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND   																 	   " & vbNewLine _
                                    & "			DTL.GYO = '02'   													   " & vbNewLine _
                                    & "			) 															AS G02 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G02.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																   	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G02.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																 	   " & vbNewLine _
                                    & "			HED.REC_NO = G02.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 03--  										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN   														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO03									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO03  											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON  			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND  																   	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME 										   " & vbNewLine _
                                    & "		AND  																   	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE  																   	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND    																   	   " & vbNewLine _
                                    & "			DTL.GYO = '03'														   " & vbNewLine _
                                    & "			) 															AS G03 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G03.CRT_DATE   									 	   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G03.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G03.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 04--    										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE  									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME  										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO  											   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO04  									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO04   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME    									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE    															   	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			DTL.GYO = '04'   													   " & vbNewLine _
                                    & "			) 															AS G04 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G04.CRT_DATE   										   " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G04.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G04.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 05--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO05 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO05  											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  								   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND  																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND  	  																   " & vbNewLine _
                                    & "			DTL.GYO = '05' 														   " & vbNewLine _
                                    & "			) 															AS G05 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G05.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G05.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G05.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 06--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO06 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO06   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							 	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															  	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND 	   																   " & vbNewLine _
                                    & "			DTL.GYO = '06' 														   " & vbNewLine _
                                    & "			) 															AS G06 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G06.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G06.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G06.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 07--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO07 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO07   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  						   		   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND   	 																   " & vbNewLine _
                                    & "			DTL.GYO = '07'   													   " & vbNewLine _
                                    & "			) 															AS G07 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G07.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G07.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G07.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 08--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE  									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 	 									   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO  											   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO08  									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO08   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO    											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			DTL.GYO = '08'  													   " & vbNewLine _
                                    & "			) 															AS G08 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G08.CRT_DATE   										   " & vbNewLine _
                                    & "		AND  																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G08.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G08.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 09--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO09									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO09    										   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   	  																   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE    															   	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			DTL.GYO = '09'   													   " & vbNewLine _
                                    & "			) 															AS G09 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G09.CRT_DATE   										   " & vbNewLine _
                                    & "		AND  																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G09.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G09.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 10--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO10 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO10  											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON    			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME  									   	   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			(HED.CRT_DATE >= @CRT_DATE_FROM AND HED.CRT_DATE <= @CRT_DATE_TO)      " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			DTL.GYO = '10'   													   " & vbNewLine _
                                    & "			) 															AS G10 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G10.CRT_DATE   										   " & vbNewLine _
                                    & "		AND 																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G10.FILE_NAME 										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G10.REC_NO                                                " & vbNewLine _
                                    & "      -- 荷主マスタ                                                 			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       			   " & vbNewLine _
                                    & "                   ON M_CUST.NRS_BR_CD       = HED.NRS_BR_CD        			   " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_L       = @CUST_CD_L           			   " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_M       = @CUST_CD_M           			   " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_S       = '00'                 			   " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_SS      = '00'                 			   " & vbNewLine _
                                    & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  			   " & vbNewLine _
                                    & "      -- 商品マスタ                                                 			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     			   " & vbNewLine _
                                    & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        			   " & vbNewLine _
                                    & "                  AND M_GOODS.GOODS_CD_NRS   = HED.HINMEI_CD        			   " & vbNewLine _
                                    & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_NSNの荷主より取得)   			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               			   " & vbNewLine _
                                    & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L           			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M           			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.PTN_ID      = 'AM'                 			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          			   " & vbNewLine _
                                    & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD 			   " & vbNewLine _
                                    & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    			   " & vbNewLine _
                                    & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    			   " & vbNewLine _
                                    & "                  AND MR1.SYS_DEL_FLG        = '0'                  			   " & vbNewLine _
                                    & "      -- 帳票パターンマスタ②(商品マスタより)                       			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               			   " & vbNewLine _
                                    & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.PTN_ID      = 'AM'                 			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          			   " & vbNewLine _
                                    & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD 			   " & vbNewLine _
                                    & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    			   " & vbNewLine _
                                    & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    			   " & vbNewLine _
                                    & "                  AND MR2.SYS_DEL_FLG        = '0'                  			   " & vbNewLine _
                                    & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           			   " & vbNewLine _
                                    & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       			   " & vbNewLine _
                                    & "                  AND MR3.PTN_ID             = 'AM'                 			   " & vbNewLine _
                                    & "                  AND MR3.STANDARD_FLAG      = '01'                 			   " & vbNewLine _
                                    & "                  AND MR3.SYS_DEL_FLG        = '0'                  			   " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 FROM句再印刷用
    ''' </summary>
    ''' <remarks>
    ''' 日産物流EDI受信データHEAD - 日産物流EDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM_OUT As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_NSN  HED                                   " & vbNewLine _
                                    & " --【Notes】№1007/1008対応 --- START ---                                       " & vbNewLine _
                                    & "      -- EDI印刷種別テーブル                                                    " & vbNewLine _
                                    & "      LEFT JOIN (                                                               " & vbNewLine _
                                    & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                       " & vbNewLine _
                                    & "                       , H_EDI_PRINT.NRS_BR_CD                                  " & vbNewLine _
                                    & "                       , H_EDI_PRINT.EDI_CTL_NO                                 " & vbNewLine _
                                    & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                      " & vbNewLine _
                                    & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                   " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                   " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                   " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.PRINT_TP    = '02'                         " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB                    " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                    & "                   GROUP BY                                                     " & vbNewLine _
                                    & "                         H_EDI_PRINT.NRS_BR_CD                                  " & vbNewLine _
                                    & "                       , H_EDI_PRINT.EDI_CTL_NO                                 " & vbNewLine _
                                    & "                ) HEDIPRINT                                                     " & vbNewLine _
                                    & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                            " & vbNewLine _
                                    & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO                           " & vbNewLine _
                                    & " --【Notes】№1007/1008対応 ---  END  ---                                       " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN  DTL                         " & vbNewLine _
                                    & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                              " & vbNewLine _
                                    & "                  AND DTL.FILE_NAME = HED.FILE_NAME                             " & vbNewLine _
                                    & "                  AND DTL.REC_NO    = HED.REC_NO                                " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 01--                                            " & vbNewLine _
                                    & "		LEFT OUTER JOIN                                                            " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO01 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO01   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED                               	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON                 " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE                                            " & vbNewLine _
                                    & "		AND                                                                        " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME                                          " & vbNewLine _
                                    & "		AND                                                                    	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO                                                " & vbNewLine _
                                    & "		WHERE                                                                  	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO     									   " & vbNewLine _
                                    & "		AND                                                                    	   " & vbNewLine _
                                    & "			DTL.GYO = '01'                                                         " & vbNewLine _
                                    & "			) 															AS G01 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G01.CRT_DATE                                            " & vbNewLine _
                                    & "		AND                                                                    	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G01.FILE_NAME                                          " & vbNewLine _
                                    & "		AND  																   	   " & vbNewLine _
                                    & "			HED.REC_NO = G01.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 02--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO02 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO02  											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON  			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE  										   " & vbNewLine _
                                    & "		AND 																   	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																   	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO    									   " & vbNewLine _
                                    & "		AND   																 	   " & vbNewLine _
                                    & "			DTL.GYO = '02'   													   " & vbNewLine _
                                    & "			) 															AS G02 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G02.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																   	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G02.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																 	   " & vbNewLine _
                                    & "			HED.REC_NO = G02.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 03--  										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN   														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO03									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO03  											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON  			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND  																   	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME 										   " & vbNewLine _
                                    & "		AND  																   	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE  																   	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO	      								   " & vbNewLine _
                                    & "		AND    																   	   " & vbNewLine _
                                    & "			DTL.GYO = '03'														   " & vbNewLine _
                                    & "			) 															AS G03 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G03.CRT_DATE   									 	   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G03.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G03.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 04--    										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE  									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME  										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO  											   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO04  									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO04   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME    									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE    															   	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO	      								   " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			DTL.GYO = '04'   													   " & vbNewLine _
                                    & "			) 															AS G04 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G04.CRT_DATE   										   " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G04.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G04.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 05--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO05 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO05  											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  								   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND  																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO	      								   " & vbNewLine _
                                    & "		AND  	  																   " & vbNewLine _
                                    & "			DTL.GYO = '05' 														   " & vbNewLine _
                                    & "			) 															AS G05 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G05.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G05.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G05.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 06--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO06 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO06   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							 	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															  	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO	      								   " & vbNewLine _
                                    & "		AND 	   																   " & vbNewLine _
                                    & "			DTL.GYO = '06' 														   " & vbNewLine _
                                    & "			) 															AS G06 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G06.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G06.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G06.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 07--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO07 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO07   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  						   		   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO	      								   " & vbNewLine _
                                    & "		AND   	 																   " & vbNewLine _
                                    & "			DTL.GYO = '07'   													   " & vbNewLine _
                                    & "			) 															AS G07 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G07.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G07.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G07.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 08--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE  									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 	 									   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO  											   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO08  									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO08   											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO    											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO	      								   " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			DTL.GYO = '08'  													   " & vbNewLine _
                                    & "			) 															AS G08 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G08.CRT_DATE   										   " & vbNewLine _
                                    & "		AND  																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G08.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G08.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 09--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO09									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO09    										   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON   			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   	  																   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE    															   	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO	      								   " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			DTL.GYO = '09'   													   " & vbNewLine _
                                    & "			) 															AS G09 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G09.CRT_DATE   										   " & vbNewLine _
                                    & "		AND  																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G09.FILE_NAME   									   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G09.REC_NO   											   " & vbNewLine _
                                    & "		--本家本元 LEFT OUTER JOIN 10--   										   " & vbNewLine _
                                    & "		LEFT OUTER JOIN    														   " & vbNewLine _
                                    & "			(SELECT HED.CRT_DATE AS CRT_DATE 									   " & vbNewLine _
                                    & "			, HED.FILE_NAME AS FILE_NAME 										   " & vbNewLine _
                                    & "			, HED.REC_NO AS REC_NO 												   " & vbNewLine _
                                    & "			, DTL.KOSU_SURYO AS KOSU_SURYO10 									   " & vbNewLine _
                                    & "			, DTL.LOT_NO AS LOT_NO10  											   " & vbNewLine _
                                    & "		FROM $LM_TRN$..H_OUTKAEDI_HED_NSN AS HED  							   	   " & vbNewLine _
                                    & "			LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_NSN AS DTL ON    			   " & vbNewLine _
                                    & "			HED.CRT_DATE = DTL.CRT_DATE   										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = DTL.FILE_NAME  									   	   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = DTL.REC_NO   											   " & vbNewLine _
                                    & "		WHERE   															   	   " & vbNewLine _
                                    & "			HED.EDI_CTL_NO = @EDI_CTL_NO      									   " & vbNewLine _
                                    & "		AND    																	   " & vbNewLine _
                                    & "			DTL.GYO = '10'   													   " & vbNewLine _
                                    & "			) 															AS G10 ON  " & vbNewLine _
                                    & "			HED.CRT_DATE = G10.CRT_DATE   										   " & vbNewLine _
                                    & "		AND 																	   " & vbNewLine _
                                    & "			HED.FILE_NAME = G10.FILE_NAME 										   " & vbNewLine _
                                    & "		AND   																	   " & vbNewLine _
                                    & "			HED.REC_NO = G10.REC_NO                                                " & vbNewLine _
                                    & "      -- 荷主マスタ                                                 			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       			   " & vbNewLine _
                                    & "                   ON M_CUST.NRS_BR_CD       = HED.NRS_BR_CD        			   " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_L       = @CUST_CD_L           			   " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_M       = @CUST_CD_M           			   " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_S       = '00'                 			   " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_SS      = '00'                 			   " & vbNewLine _
                                    & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  			   " & vbNewLine _
                                    & "      -- 商品マスタ                                                 			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     			   " & vbNewLine _
                                    & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        			   " & vbNewLine _
                                    & "                  AND M_GOODS.GOODS_CD_NRS   = HED.HINMEI_CD        			   " & vbNewLine _
                                    & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_NSNの荷主より取得)   			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               			   " & vbNewLine _
                                    & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L           			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M           			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.PTN_ID      = 'AM'                 			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          			   " & vbNewLine _
                                    & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD 			   " & vbNewLine _
                                    & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    			   " & vbNewLine _
                                    & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    			   " & vbNewLine _
                                    & "                  AND MR1.SYS_DEL_FLG        = '0'                  			   " & vbNewLine _
                                    & "      -- 帳票パターンマスタ②(商品マスタより)                       			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               			   " & vbNewLine _
                                    & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.PTN_ID      = 'AM'                 			   " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          			   " & vbNewLine _
                                    & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD 			   " & vbNewLine _
                                    & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    			   " & vbNewLine _
                                    & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    			   " & vbNewLine _
                                    & "                  AND MR2.SYS_DEL_FLG        = '0'                  			   " & vbNewLine _
                                    & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   			   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           			   " & vbNewLine _
                                    & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       			   " & vbNewLine _
                                    & "                  AND MR3.PTN_ID             = 'AM'                 			   " & vbNewLine _
                                    & "                  AND MR3.STANDARD_FLAG      = '01'                 			   " & vbNewLine _
                                    & "                  AND MR3.SYS_DEL_FLG        = '0'                  			   " & vbNewLine


    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY            " & vbNewLine _
                                         & "       HED.CRT_DATE  " & vbNewLine _
                                         & "     , HED.FILE_NAME " & vbNewLine _
                                         & "     , HED.REC_NO    " & vbNewLine _
                                         & "     , DTL.GYO       " & vbNewLine


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

    ''' <summary>
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrintPattern(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH562IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH562DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH562DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        '(2012.03.19) WHERE条件を帳票取得時と同じにする -- START --
        'Me._StrSql.Append(LMH562DAC.SQL_MPrt_WHERE)     'SQL構築(帳票種別用WHERE句)
        'Call Me.SetConditionPrintPatternMSQL()          '条件設定
        If Me._Row.Item("PRTFLG").ToString = "1" Then    'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出条件設定) '未出力・両方(出力済、未出力併せて)
        End If                                          'Notes 1061 2012/05/15　終了
        'Call Me.SetConditionPrintPatternMSQL()          '条件設定 Notes1061
        '(2012.03.19) WHERE条件を帳票取得時と同じにする --  END  --

        ''追加(Notes_1007 2012/05/09)
        'Call Me.SetConditionPrintPatternMSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH562DAC", "SelectMPrt", cmd)

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
    ''' 日産物流EDI受信データ(HEAD)・日産物流EDI受信データ(DETAIL)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>日産物流EDI受信データ(HEAD)・(DETAIL)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH562IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH562DAC.SQL_SELECT)           'SQL構築(印刷データ抽出用 SELECT句)

        If Me._Row.Item("PRTFLG").ToString = "1" Then     'SQL構築(印刷データ抽出用 FROM句)
            Me._StrSql.Append(LMH562DAC.SQL_FROM_OUT)        '出力済の場合
            'Call Me.SetConditionPrintPatternMSQL()          '条件設定
        Else
            Me._StrSql.Append(LMH562DAC.SQL_FROM)            '未出力の場合
        End If

        If Me._Row.Item("PRTFLG").ToString = "1" Then   'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()              '出力済の場合
            'Call Me.SetConditionPrintPatternMSQL()          '条件設定
        Else
            Call Me.SetConditionMasterSQL()               'SQL構築(印刷データ抽出条件設定) '未出力・両方(出力済、未出力併せて)
        End If                                          'Notes 1061 2012/05/15　終了
        Me._StrSql.Append(LMH562DAC.SQL_ORDER_BY)         'SQL構築(印刷データ抽出用 ORDER BY句)

        'Call Me.SetConditionPrintPatternMSQL()         '条件設定 Notes1061

        ''追加(Notes_1007 2012/05/09)
        'Call Me.SetConditionPrintPatternMSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH562DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        'map.Add("H_DEL_KB", "H_DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        ' map.Add("NRS_WH_CD", "NRS_WH_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("SHORI_KB", "SHORI_KB")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("CUST_SHOZOKU", "CUST_SHOZOKU")
        map.Add("CUST_TANTO_NM", "CUST_TANTO_NM")
        map.Add("EIGYO_SASHIZU", "EIGYO_SASHIZU")
        map.Add("KOJO_KANRI_NO", "KOJO_KANRI_NO")
        map.Add("SHUKKA_DATE", "SHUKKA_DATE")
        map.Add("NONYU_DATE", "NONYU_DATE")
        map.Add("NONYU_TIME", "NONYU_TIME")
        map.Add("HINSHU_CD", "HINSHU_CD")
        map.Add("HINMEI_CD", "HINMEI_CD")
        map.Add("GRADE", "GRADE")
        map.Add("SHANAI_HINMEI", "SHANAI_HINMEI")
        map.Add("HINMEI", "HINMEI")
        map.Add("NISUGATA_NM", "NISUGATA_NM")
        map.Add("YORYO", "YORYO")
        map.Add("TAN_I", "TAN_I")
        map.Add("KOSU", "KOSU")
        map.Add("SURYO", "SURYO")
        map.Add("TORIHIKI_NO", "TORIHIKI_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_SHOZOKU", "DEST_SHOZOKU")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("SHUKKA_NM", "SHUKKA_NM")
        map.Add("YUSO_NM", "YUSO_NM")
        map.Add("COMMENT", "COMMENT")
        map.Add("DENP_COMMENT", "DENP_COMMENT")
        map.Add("HAISHA_COMMENT", "HAISHA_COMMENT")
        map.Add("NRS_BUNSEKI", "NRS_BUNSEKI")
        map.Add("NRS_NONYU", "NRS_NONYU")
        map.Add("EDI_DATE", "EDI_DATE")
        map.Add("EDI_TIME", "EDI_TIME")
        map.Add("LOT_NO01", "LOT_NO01")
        map.Add("KOSU_SURYO01", "KOSU_SURYO01")
        map.Add("LOT_NO02", "LOT_NO02")
        map.Add("KOSU_SURYO02", "KOSU_SURYO02")
        map.Add("LOT_NO03", "LOT_NO03")
        map.Add("KOSU_SURYO03", "KOSU_SURYO03")
        map.Add("LOT_NO04", "LOT_NO04")
        map.Add("KOSU_SURYO04", "KOSU_SURYO04")
        map.Add("LOT_NO05", "LOT_NO05")
        map.Add("KOSU_SURYO05", "KOSU_SURYO05")
        map.Add("LOT_NO06", "LOT_NO06")
        map.Add("KOSU_SURYO06", "KOSU_SURYO06")
        map.Add("LOT_NO07", "LOT_NO07")
        map.Add("KOSU_SURYO07", "KOSU_SURYO07")
        map.Add("LOT_NO08", "LOT_NO08")
        map.Add("KOSU_SURYO08", "KOSU_SURYO08")
        map.Add("LOT_NO09", "LOT_NO09")
        map.Add("KOSU_SURYO09", "KOSU_SURYO09")
        map.Add("LOT_NO10", "LOT_NO10")
        map.Add("KOSU_SURYO10", "KOSU_SURYO10")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH562OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
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

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(FROM)
            whereStr = .Item("CRT_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CRT_DATE >= @CRT_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("CRT_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CRT_DATE <= @CRT_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '---START---20121106追加：同一内容レコードの無駄表示排除開始▼▼▼
            Me._StrSql.Append(" AND DTL.GYO = '01'")
            Me._StrSql.Append(vbNewLine)
            '▲▲▲20121106追加：同一内容レコードの無駄表示排除終了---TOEND---

            '---START---20121115追加：レコードC00000000データ表示排除開始▼▼▼--2012/12/19検索データの有効範囲萎縮原因のためコメントアウト
            'Me._StrSql.Append(" AND HED.EDI_CTL_NO != 'C00000000'")
            'Me._StrSql.Append(vbNewLine)
            '▲▲▲20121115追加：レコードC00000000データ表示排除終了---TOEND-----2012/12/19検索データの有効範囲萎縮原因のためコメントアウト


            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            'プリントフラグ
            'whereStr = .Item("PRTFLG").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.PRTFLG = @PRTFLG")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            'End If

            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う ---  END  ---

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール(出力済 Notes 1061 2012/05/15 新設)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_OUT()

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
                Me._StrSql.Append(" HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            'End If

            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.INOUT_KB = @INOUT_KB")
            Me._StrSql.Append(vbNewLine) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR)) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)
            'End If

            'EDI出荷管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.EDI_CTL_NO = @EDI_CTL_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If

            '---START---20121106追加：同一内容レコードの無駄表示排除開始▼▼▼
            Me._StrSql.Append(" AND DTL.GYO = '01'")
            Me._StrSql.Append(vbNewLine)
            '▲▲▲20121106追加：同一内容レコードの無駄表示排除終了---TOEND---

            '---START---20121115追加：レコードC00000000データ表示排除開始▼▼▼
            Me._StrSql.Append(" AND HED.EDI_CTL_NO != 'C00000000'")
            Me._StrSql.Append(vbNewLine)
            '▲▲▲20121115追加：レコードC00000000データ表示排除終了---TOEND---

            '未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            'プリントフラグ
            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            '未出力/出力済の判断をHEDIPRINTのレコード有無で行う ---  END  ---

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
