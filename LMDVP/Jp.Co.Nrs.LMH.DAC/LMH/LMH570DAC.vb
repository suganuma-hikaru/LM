' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH570    : 出荷EDI受信一覧表(ＤＩＣ用)
'  作  成  者       :  篠原将文
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH570DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH570DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       HED.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , '83'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

    '--------------SHINODA 要望管理2259対応 START--------------------
    'Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_DIC  HED                            " & vbNewLine _
    '                                      & "      -- ＤＩＣ受信データ                                           " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_DIC  DTL             " & vbNewLine _
    '                                      & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
    '                                      & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
    '                                      & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
    '                                      & "                  AND DTL.FILE_DIST = HED.FILE_DIST                 " & vbNewLine _
    '                                      & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
    '                                      & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
    '                                      & "      LEFT JOIN (                                                   " & vbNewLine _
    '                                      & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
    '                                      & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
    '                                      & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
    '                                      & "                  -- 要望番号1077 2012.05.29 伝票№追加 -- START -- " & vbNewLine _
    '                                      & "                       , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
    '                                      & "                  -- 要望番号1077 2012.05.29 伝票№追加 --  END  -- " & vbNewLine _
    '                                      & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
    '                                      & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
    '                                      & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
    '                                      & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
    '                                      & "                     AND H_EDI_PRINT.PRINT_TP    = '03'             " & vbNewLine _
    '                                      & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
    '                                      & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
    '                                      & "                   GROUP BY                                         " & vbNewLine _
    '                                      & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
    '                                      & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
    '                                      & "                  -- 要望番号1077 2012.05.29 伝票№追加 -- START -- " & vbNewLine _
    '                                      & "                       , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
    '                                      & "                  -- 要望番号1077 2012.05.29 伝票№追加 --  END  -- " & vbNewLine _
    '                                      & "                ) HEDIPRINT                                         " & vbNewLine _
    '                                      & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
    '                                      & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
    '                                      & "            -- 要望番号1077 2012.05.29 伝票№追加 -- START --       " & vbNewLine _
    '                                      & "            AND HEDIPRINT.DENPYO_NO  = HED.DENPYO_NO                " & vbNewLine _
    '                                      & "            -- 要望番号1077 2012.05.29 伝票№追加 --  END  --       " & vbNewLine _
    '                                      & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
    '                                      & "      -- 商品マスタ                                                 " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
    '                                      & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        " & vbNewLine _
    '                                      & "                  AND M_GOODS.GOODS_CD_NRS   = HED.SEIHIN_CD        " & vbNewLine _
    '                                      & "      -- 帳票パターンマスタ①(OUTKAEDI_HED_DICの荷主より取得)       " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
    '                                      & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT1.CUST_CD_L   = HED.CUST_CD_L        " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT1.CUST_CD_M   = HED.CUST_CD_M        " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT1.PTN_ID      = '83'                 " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
    '                                      & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD " & vbNewLine _
    '                                      & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    " & vbNewLine _
    '                                      & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    " & vbNewLine _
    '                                      & "                  AND MR1.SYS_DEL_FLG        = '0'                  " & vbNewLine _
    '                                      & "      -- 帳票パターンマスタ②(商品マスタより)                       " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               " & vbNewLine _
    '                                      & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT2.PTN_ID      = '83'                 " & vbNewLine _
    '                                      & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
    '                                      & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
    '                                      & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
    '                                      & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
    '                                      & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
    '                                      & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
    '                                      & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
    '                                      & "                  AND MR3.PTN_ID             = '83'                 " & vbNewLine _
    '                                      & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
    '                                      & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..H_INOUTKAEDI_HED_DIC_NEW  HED                            " & vbNewLine _
                                          & "      -- ＤＩＣ受信データ                                           " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..H_INOUTKAEDI_DTL_DIC_NEW  DTL             " & vbNewLine _
                                          & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
                                          & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
                                          & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
                                          & "                  AND DTL.NRS_BR_CD = HED.NRS_BR_CD                 " & vbNewLine _
                                          & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
                                          & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
                                          & "      LEFT JOIN (                                                   " & vbNewLine _
                                          & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                          & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                          & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                          & "                  -- 要望番号1077 2012.05.29 伝票№追加 -- START -- " & vbNewLine _
                                          & "                       , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
                                          & "                  -- 要望番号1077 2012.05.29 伝票№追加 --  END  -- " & vbNewLine _
                                          & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                          & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.PRINT_TP    = '03'             " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                          & "                   GROUP BY                                         " & vbNewLine _
                                          & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                          & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                          & "                  -- 要望番号1077 2012.05.29 伝票№追加 -- START -- " & vbNewLine _
                                          & "                       , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
                                          & "                  -- 要望番号1077 2012.05.29 伝票№追加 --  END  -- " & vbNewLine _
                                          & "                ) HEDIPRINT                                         " & vbNewLine _
                                          & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
                                          & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
                                          & "            -- 要望番号1077 2012.05.29 伝票№追加 -- START --       " & vbNewLine _
                                          & "            AND HEDIPRINT.DENPYO_NO  = HED.DENPYO_NO                " & vbNewLine _
                                          & "            -- 要望番号1077 2012.05.29 伝票№追加 --  END  --       " & vbNewLine _
                                          & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                          & "      -- 商品マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                          & "                   ON M_GOODS.NRS_BR_CD      = DTL.NRS_BR_CD        " & vbNewLine _
                                          & "                  AND M_GOODS.GOODS_CD_NRS   = DTL.DIC_HIN_CD       " & vbNewLine _
                                          & "      -- 帳票パターンマスタ①(OUTKAEDI_HED_DICの荷主より取得)       " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                          & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_L   = HED.CUST_CD_L        " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_M   = HED.CUST_CD_M        " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.PTN_ID      = '83'                 " & vbNewLine _
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
                                          & "                  AND M_CUSTRPT2.PTN_ID      = '83'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                          & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                          & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                          & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                          & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND MR3.PTN_ID             = '83'                 " & vbNewLine _
                                          & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                          & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    '--------------SHINODA 要望管理2259対応 E N D--------------------
    ''' <summary>
    ''' 帳票種別取得用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                     " & vbNewLine _
                                           & "       HED.NRS_BR_CD     =  @NRS_BR_CD     " & vbNewLine _
                                           & "   AND HED.CUST_CD_L     =  @CUST_CD_L     " & vbNewLine _
                                           & "   AND HED.CUST_CD_M     =  @CUST_CD_M     " & vbNewLine

    '--------------SHINODA 要望管理2259対応 START--------------------
    'Private Const SQL_SELECT As String = " SELECT                                                    " & vbNewLine _
    '                                   & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID   " & vbNewLine _
    '                                   & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   " & vbNewLine _
    '                                   & "        ELSE MR3.RPT_ID END        AS RPT_ID               " & vbNewLine _
    '                                   & "      ,HED.DEL_KB                  AS L_DEL_KB             " & vbNewLine _
    '                                   & "      ,HED.CRT_DATE                AS CRT_DATE             " & vbNewLine _
    '                                   & "      ,HED.FILE_NAME               AS FILE_NAME            " & vbNewLine _
    '                                   & "      ,HED.REC_NO                  AS REC_NO               " & vbNewLine _
    '                                   & "      ,HED.FILE_DIST               AS FILE_DIST            " & vbNewLine _
    '                                   & "      ,HED.NRS_BR_CD               AS NRS_BR_CD            " & vbNewLine _
    '                                   & "      ,HED.EDI_CTL_NO              AS OUTKAEDI_NO_L        " & vbNewLine _
    '                                   & "      ,HED.OUTKA_CTL_NO            AS OUTKA_NO_L           " & vbNewLine _
    '                                   & "      ,HED.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
    '                                   & "      ,HED.CUST_CD_M               AS CUST_CD_M            " & vbNewLine _
    '                                   & "      ,M_CUST.CUST_NM_L            AS CUST_NM_L            " & vbNewLine _
    '                                   & "      ,M_CUST.CUST_NM_M            AS CUST_NM_M            " & vbNewLine _
    '                                   & "      ,HED.PRTFLG                  AS PRTFLG               " & vbNewLine _
    '                                   & "      ,HED.CANCEL_FLG              AS CANCEL_FLG 		     " & vbNewLine _
    '                                   & "      ,HED.DENSO_NO			     AS	DENSO_NO	 		 " & vbNewLine _
    '                                   & "      ,HED.DATA_KB			     AS	DATA_KB	 			 " & vbNewLine _
    '                                   & "      ,HED.NONYU_CD			     AS	NONYU_CD			 " & vbNewLine _
    '                                   & "      ,HED.NONYU_NM_KB		     AS	NONYU_NM_KB			 " & vbNewLine _
    '                                   & "      ,HED.NONYU_NM_KANA		     AS	NONYU_NM_KANA		 " & vbNewLine _
    '                                   & "      ,HED.NONYU_NM_KANJI		     AS	NONYU_NM_KANJI		 " & vbNewLine _
    '                                   & "      ,HED.NONYU_JUSHO_KB		     AS	NONYU_JUSHO_KB		 " & vbNewLine _
    '                                   & "      ,HED.NONYU_JUSHO_KANA	     AS	NONYU_JUSHO_KANA	 " & vbNewLine _
    '                                   & "      ,HED.NONYU_JUSHO_KANJI	     AS	NONYU_JUSHO_KANJI	 " & vbNewLine _
    '                                   & "      ,HED.ATSUKA_BUKA_CD		     AS	ATSUKA_BUKA_CD	 	 " & vbNewLine _
    '                                   & "      ,HED.ZAIKO_BUKA_CD		     AS	ZAIKO_BUKA_CD	 	 " & vbNewLine _
    '                                   & "      ,HED.YAKUJO_NO			     AS	YAKUJO_NO	 		 " & vbNewLine _
    '                                   & "      ,HED.DENPYO_NO			     AS	DENPYO_NO	 		 " & vbNewLine _
    '                                   & "      ,HED.CHIKU_CD			     AS	CHIKU_CD	 		 " & vbNewLine _
    '                                   & "      ,HED.YUSO_CD			     AS	YUSO_CD	 			 " & vbNewLine _
    '                                   & "      ,HED.SHUKKA_YMD			     AS	SHUKKA_YMD	 		 " & vbNewLine _
    '                                   & "      ,HED.KINKYU_FLG			     AS	KINKYU_FLG	 		 " & vbNewLine _
    '                                   & "      ,HED.SEIHIN_CD			     AS	SEIHIN_CD			 " & vbNewLine _
    '                                   & "      ,HED.NISUGATA_CD		     AS	NISUGATA_CD	 		 " & vbNewLine _
    '                                   & "      ,HED.NOUKI				     AS	NOUKI	 			 " & vbNewLine _
    '                                   & "      ,HED.SHANAI_MSG_KANJI	     AS	SHANAI_MSG_KANJI	 " & vbNewLine _
    '                                   & "      ,HED.NOUNYU_TEL_NO		     AS	NOUNYU_TEL_NO	 	 " & vbNewLine _
    '                                   & "      ,HED.TAN_I				     AS	TAN_I	 			 " & vbNewLine _
    '                                   & "      ,HED.KANSAN_RITSU		     AS	KANSAN_RITSU	 	 " & vbNewLine _
    '                                   & "      ,HED.KANSAN_SURYO		     AS	KANSAN_SURYO		 " & vbNewLine _
    '                                   & "      ,HED.ROUTE_NO			     AS	ROUTE_NO	 		 " & vbNewLine _
    '                                   & "      ,HED.BIN_KB				     AS	BIN_KB				 " & vbNewLine _
    '                                   & "      ,HED.YOBI				     AS	YOBI	 			 " & vbNewLine _
    '                                   & "      ,HED.HANBAI_TANKA		     AS	HANBAI_TANKA		 " & vbNewLine _
    '                                   & "      ,HED.HANBAI_KINGAKU		     AS	HANBAI_KINGAKU		 " & vbNewLine _
    '                                   & "      ,HED.SHOUHIZEIGAKU		     AS	SHOUHIZEIGAKU		 " & vbNewLine _
    '                                   & "      ,HED.HAKKO_SHORIBI		     AS	HAKKO_SHORIBI		 " & vbNewLine _
    '                                   & "      ,HED.HAKKO_JIKAN		     AS	HAKKO_JIKAN			 " & vbNewLine _
    '                                   & "      ,HED.FURIKAE_SEIHIN_CD	     AS	FURIKAE_SEIHIN_CD	 " & vbNewLine _
    '                                   & "      ,HED.HINMEI				     AS	HINMEI	 			 " & vbNewLine _
    '                                   & "      ,HED.KITSUKE_MSG		     AS	KITSUKE_MSG	 		 " & vbNewLine _
    '                                   & "      ,HED.RECORD_STATUS		     AS	L_RECORD_STATUS		 " & vbNewLine _
    '                                   & "      ,DTL.DEL_KB				     AS	M_DEL_KB	 	     " & vbNewLine _
    '                                   & "      ,DTL.GYO				     AS	GYO	 			     " & vbNewLine _
    '                                   & "      ,DTL.EDI_CTL_NO_CHU	         AS	OUTKAEDI_NO_M	     " & vbNewLine _
    '                                   & "      ,DTL.OUTKA_CTL_NO_CHU      	 AS	OUTKA_NO_M		     " & vbNewLine _
    '                                   & "      ,DTL.LOCATION			   	 AS	LOCATION             " & vbNewLine _
    '                                   & "      ,DTL.IRIME				   	 AS	IRIME	 		     " & vbNewLine _
    '                                   & "      ,DTL.LOT_NO				   	 AS	LOT_NO	 		     " & vbNewLine _
    '                                   & "      ,DTL.KOSU				   	 AS	KOSU			     " & vbNewLine _
    '                                   & "      ,DTL.DIC_ZANSU			   	 AS	DIC_ZANSU		     " & vbNewLine _
    '                                   & "      ,DTL.RECORD_STATUS	     	 AS	M_RECORD_STATUS	     " & vbNewLine _
    '                                   & "      ,''                      	 AS	MESSAGE_01    	     " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                    " & vbNewLine _
                                       & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID   " & vbNewLine _
                                       & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   " & vbNewLine _
                                       & "        ELSE MR3.RPT_ID END        AS RPT_ID               " & vbNewLine _
                                       & "      ,HED.DEL_KB                  AS L_DEL_KB             " & vbNewLine _
                                       & "      ,HED.CRT_DATE                AS CRT_DATE             " & vbNewLine _
                                       & "      ,HED.FILE_NAME               AS FILE_NAME            " & vbNewLine _
                                       & "      ,HED.REC_NO                  AS REC_NO               " & vbNewLine _
                                       & "      ,''                          AS FILE_DIST            " & vbNewLine _
                                       & "      ,HED.NRS_BR_CD               AS NRS_BR_CD            " & vbNewLine _
                                       & "      ,HED.EDI_CTL_NO              AS OUTKAEDI_NO_L        " & vbNewLine _
                                       & "      ,HED.OUTKA_CTL_NO            AS OUTKA_NO_L           " & vbNewLine _
                                       & "      ,HED.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
                                       & "      ,HED.CUST_CD_M               AS CUST_CD_M            " & vbNewLine _
                                       & "      ,M_CUST.CUST_NM_L            AS CUST_NM_L            " & vbNewLine _
                                       & "      ,M_CUST.CUST_NM_M            AS CUST_NM_M            " & vbNewLine _
                                       & "      ,HED.PRTFLG                  AS PRTFLG               " & vbNewLine _
                                       & "      ,HED.AKAKURO_KBN             AS CANCEL_FLG 		     " & vbNewLine _
                                       & "      ,''			     AS	DENSO_NO	 		 " & vbNewLine _
                                       & "      ,HED.DATA_KBN			     AS	DATA_KB	 			 " & vbNewLine _
                                       & "      ,HED.NONYU_CD			     AS	NONYU_CD			 " & vbNewLine _
                                       & "      ,'1'		                 AS	NONYU_NM_KB			 " & vbNewLine _
                                       & "      ,HED.NONYU_NM   		     AS	NONYU_NM_KANA		 " & vbNewLine _
                                       & "      ,HED.NONYU_NM   		     AS	NONYU_NM_KANJI		 " & vbNewLine _
                                       & "      ,'1'            		     AS	NONYU_JUSHO_KB		 " & vbNewLine _
                                       & "      ,HED.NONYU_ADD      	     AS	NONYU_JUSHO_KANA	 " & vbNewLine _
                                       & "      ,HED.NONYU_ADD      	     AS	NONYU_JUSHO_KANJI	 " & vbNewLine _
                                       & "      ,''             		     AS	ATSUKA_BUKA_CD	 	 " & vbNewLine _
                                       & "      ,''             		     AS	ZAIKO_BUKA_CD	 	 " & vbNewLine _
                                       & "      ,HED.JUCHU_DENP_NO         	 AS	YAKUJO_NO	 		 " & vbNewLine _
                                       & "      ,HED.DENPYO_NO			     AS	DENPYO_NO	 		 " & vbNewLine _
                                       & "      ,''         			     AS	CHIKU_CD	 		 " & vbNewLine _
                                       & "      ,LEFT(DTL.DIC_UNSO_CD,2)     AS	YUSO_CD	 			 " & vbNewLine _
                                       & "      ,HED.OUTKA_PLAN_DATE	     AS	SHUKKA_YMD	 		 " & vbNewLine _
                                       & "      ,'0'        			     AS	KINKYU_FLG	 		 " & vbNewLine _
                                       & "      ,DTL.DIC_HIN_CD			     AS	SEIHIN_CD			 " & vbNewLine _
                                       & "      ,''             		     AS	NISUGATA_CD	 		 " & vbNewLine _
                                       & "      ,HED.ARR_PLAN_DATE		     AS	NOUKI	 			 " & vbNewLine _
                                       & "      ,DTL.OUTKA_SHIJI_MSG   	     AS	SHANAI_MSG_KANJI	 " & vbNewLine _
                                       & "      ,HED.NONYU_TEL 		     AS	NOUNYU_TEL_NO	 	 " & vbNewLine _
                                       & "      ,''     				     AS	TAN_I	 			 " & vbNewLine _
                                       & "      ,''             		     AS	KANSAN_RITSU	 	 " & vbNewLine _
                                       & "      ,''             		     AS	KANSAN_SURYO		 " & vbNewLine _
                                       & "      ,''         			     AS	ROUTE_NO	 		 " & vbNewLine _
                                       & "      ,''     				     AS	BIN_KB				 " & vbNewLine _
                                       & "      ,''     				     AS	YOBI	 			 " & vbNewLine _
                                       & "      ,''             		     AS	HANBAI_TANKA		 " & vbNewLine _
                                       & "      ,''             		     AS	HANBAI_KINGAKU		 " & vbNewLine _
                                       & "      ,''             		     AS	SHOUHIZEIGAKU		 " & vbNewLine _
                                       & "      ,HED.EDI_DATE   		     AS	HAKKO_SHORIBI		 " & vbNewLine _
                                       & "      ,HED.EDI_TIME   		     AS	HAKKO_JIKAN			 " & vbNewLine _
                                       & "      ,''                 	     AS	FURIKAE_SEIHIN_CD	 " & vbNewLine _
                                       & "      ,DTL.DIC_HIN_NM			     AS	HINMEI	 			 " & vbNewLine _
                                       & "      ,''             		     AS	KITSUKE_MSG	 		 " & vbNewLine _
                                       & "      ,''             		     AS	L_RECORD_STATUS		 " & vbNewLine _
                                       & "      ,DTL.DEL_KB				     AS	M_DEL_KB	 	     " & vbNewLine _
                                       & "      ,DTL.GYO				     AS	GYO	 			     " & vbNewLine _
                                       & "      ,DTL.EDI_CTL_NO_CHU	         AS	OUTKAEDI_NO_M	     " & vbNewLine _
                                       & "      ,DTL.OUTKA_CTL_NO_CHU      	 AS	OUTKA_NO_M		     " & vbNewLine _
                                       & "      ,''         			   	 AS	LOCATION             " & vbNewLine _
                                       & "      ,DTL.IRIME				   	 AS	IRIME	 		     " & vbNewLine _
                                       & "      ,DTL.LOT_NO_1			   	 AS	LOT_NO	 		     " & vbNewLine _
                                       & "      ,DTL.NB 				   	 AS	KOSU			     " & vbNewLine _
                                       & "      ,'0'			   	         AS	DIC_ZANSU		     " & vbNewLine _
                                       & "      ,''             	     	 AS	M_RECORD_STATUS	     " & vbNewLine _
                                       & "      ,''                      	 AS	MESSAGE_01    	     " & vbNewLine

    '--------------SHINODA 要望管理2259対応 E N D--------------------

    '--------------SHINODA 要望管理2259対応 START--------------------
    'Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_DIC  HED                            " & vbNewLine _
    '                                 & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_DIC  DTL             " & vbNewLine _
    '                                 & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
    '                                 & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
    '                                 & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
    '                                 & "--20120711 Notes1258対応開始                                        " & vbNewLine _
    '                                 & "                  AND DTL.FILE_DIST = HED.FILE_DIST                 " & vbNewLine _
    '                                 & "--20120711 Notes1258対応終了                                        " & vbNewLine _
    '                                 & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
    '                                 & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
    '                                 & "      LEFT JOIN (                                                   " & vbNewLine _
    '                                 & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
    '                                 & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
    '                                 & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
    '                                 & "                  -- 要望番号1077 2012.05.29 伝票№追加 -- START -- " & vbNewLine _
    '                                 & "                       , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
    '                                 & "                  -- 要望番号1077 2012.05.29 伝票№追加 --  END  -- " & vbNewLine _
    '                                 & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
    '                                 & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
    '                                 & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
    '                                 & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
    '                                 & "                     AND H_EDI_PRINT.PRINT_TP    = '03'             " & vbNewLine _
    '                                 & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
    '                                 & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
    '                                 & "                   GROUP BY                                         " & vbNewLine _
    '                                 & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
    '                                 & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
    '                                 & "                  -- 要望番号1077 2012.05.29 伝票№追加 -- START -- " & vbNewLine _
    '                                 & "                       , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
    '                                 & "                  -- 要望番号1077 2012.05.29 伝票№追加 --  END  -- " & vbNewLine _
    '                                 & "                ) HEDIPRINT                                         " & vbNewLine _
    '                                 & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
    '                                 & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
    '                                 & "            -- 要望番号1077 2012.05.29 伝票№追加 -- START --       " & vbNewLine _
    '                                 & "            AND HEDIPRINT.DENPYO_NO  = HED.DENPYO_NO                " & vbNewLine _
    '                                 & "            -- 要望番号1077 2012.05.29 伝票№追加 --  END  --       " & vbNewLine _
    '                                 & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
    '                                 & "      -- 荷主マスタ                                                 " & vbNewLine _
    '                                 & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
    '                                 & "                   ON M_CUST.NRS_BR_CD       = HED.NRS_BR_CD        " & vbNewLine _
    '                                 & "                  AND M_CUST.CUST_CD_L       = HED.CUST_CD_L        " & vbNewLine _
    '                                 & "                  AND M_CUST.CUST_CD_M       = HED.CUST_CD_M        " & vbNewLine _
    '                                 & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
    '                                 & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
    '                                 & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
    '                                 & "      -- 商品マスタ                                                 " & vbNewLine _
    '                                 & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
    '                                 & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        " & vbNewLine _
    '                                 & "                  AND M_GOODS.GOODS_CD_NRS   = HED.SEIHIN_CD        " & vbNewLine _
    '                                 & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)   " & vbNewLine _
    '                                 & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
    '                                 & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT1.CUST_CD_L   = HED.CUST_CD_L        " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT1.CUST_CD_M   = HED.CUST_CD_M        " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT1.PTN_ID      = '83'                 " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
    '                                 & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
    '                                 & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD " & vbNewLine _
    '                                 & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    " & vbNewLine _
    '                                 & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    " & vbNewLine _
    '                                 & "                  AND MR1.SYS_DEL_FLG        = '0'                  " & vbNewLine _
    '                                 & "      -- 帳票パターンマスタ②(商品マスタより)                       " & vbNewLine _
    '                                 & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               " & vbNewLine _
    '                                 & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT2.PTN_ID      = '83'                 " & vbNewLine _
    '                                 & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
    '                                 & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
    '                                 & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
    '                                 & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
    '                                 & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
    '                                 & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
    '                                 & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
    '                                 & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
    '                                 & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
    '                                 & "                  AND MR3.PTN_ID             = '83'                 " & vbNewLine _
    '                                 & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
    '                                 & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine
    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' ＤＩＣEDI受信データHEAD - ＤＩＣEDI受信データDETAIL,荷主Ｍ
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_INOUTKAEDI_HED_DIC_NEW  HED                      " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..H_INOUTKAEDI_DTL_DIC_NEW  DTL       " & vbNewLine _
                                     & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
                                     & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
                                     & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
                                     & "                  AND DTL.NRS_BR_CD = HED.NRS_BR_CD                 " & vbNewLine _
                                     & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
                                     & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
                                     & "      LEFT JOIN (                                                   " & vbNewLine _
                                     & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                     & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                     & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                     & "                  -- 要望番号1077 2012.05.29 伝票№追加 -- START -- " & vbNewLine _
                                     & "                       , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
                                     & "                  -- 要望番号1077 2012.05.29 伝票№追加 --  END  -- " & vbNewLine _
                                     & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                     & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.PRINT_TP    = '03'             " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                     & "                   GROUP BY                                         " & vbNewLine _
                                     & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                     & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                     & "                  -- 要望番号1077 2012.05.29 伝票№追加 -- START -- " & vbNewLine _
                                     & "                       , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
                                     & "                  -- 要望番号1077 2012.05.29 伝票№追加 --  END  -- " & vbNewLine _
                                     & "                ) HEDIPRINT                                         " & vbNewLine _
                                     & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
                                     & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
                                     & "            -- 要望番号1077 2012.05.29 伝票№追加 -- START --       " & vbNewLine _
                                     & "            AND HEDIPRINT.DENPYO_NO  = HED.DENPYO_NO                " & vbNewLine _
                                     & "            -- 要望番号1077 2012.05.29 伝票№追加 --  END  --       " & vbNewLine _
                                     & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                     & "      -- 荷主マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                     & "                   ON M_CUST.NRS_BR_CD       = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_L       = HED.CUST_CD_L        " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_M       = HED.CUST_CD_M        " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                     & "      -- 商品マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                     & "                   ON M_GOODS.NRS_BR_CD      = DTL.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_GOODS.GOODS_CD_NRS   = DTL.DIC_HIN_CD       " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                     & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_L   = HED.CUST_CD_L        " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_M   = HED.CUST_CD_M        " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.PTN_ID      = '83'                 " & vbNewLine _
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
                                     & "                  AND M_CUSTRPT2.PTN_ID      = '83'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                     & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                     & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                     & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                     & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                     & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND MR3.PTN_ID             = '83'                 " & vbNewLine _
                                     & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                     & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine
    '--------------SHINODA 要望管理2259対応 E N D--------------------

    '--------------SHINODA 要望管理2259対応 START--------------------
    'Private Const SQL_ORDER_BY As String = " ORDER BY                  " & vbNewLine _
    '                                    & "       HED.DENPYO_NO       " & vbNewLine _
    '                                    & "     , HED.EDI_CTL_NO      " & vbNewLine _
    '                                    & "     , HED.CANCEL_FLG DESC " & vbNewLine _
    '                                    & "     , HED.REC_NO          " & vbNewLine _
    '                                    & "     , DTL.GYO             " & vbNewLine
    '大阪対応(第2ソートにCANCEL_FLGL、第3ソートにEDI管理番号L)　20120329 Start
    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                  " & vbNewLine _
                                         & "       HED.DENPYO_NO       " & vbNewLine _
                                         & "     , HED.EDI_CTL_NO      " & vbNewLine _
                                         & "     , HED.AKAKURO_KBN DESC " & vbNewLine _
                                         & "     , HED.REC_NO          " & vbNewLine _
                                         & "     , DTL.GYO             " & vbNewLine
    '大阪対応(第2ソートにCANCEL_FLGLL、第3ソートにEDI管理番号L)　20120329 End
    '--------------SHINODA 要望管理2259対応 E N D--------------------

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
        Dim inTbl As DataTable = ds.Tables("LMH570IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH570DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH570DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        '(2012.03.19) WHERE条件を帳票取得時と同じにする -- START --
        'Me._StrSql.Append(LMH570DAC.SQL_MPrt_WHERE)     'SQL構築(帳票種別用WHERE句)
        Call Me.SetConditionPrintPatternMSQL()          '条件設定
        If Me._Row.Item("PRTFLG").ToString = "1" Then    'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出条件設定)
        End If                                          'Notes 1061 2012/05/15　終了
        '(2012.03.19) WHERE条件を帳票取得時と同じにする --  END  --

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH570DAC", "SelectMPrt", cmd)

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
    ''' ＤＩＣEDI受信データ(HEAD)・ＤＩＣEDI受信データ(DETAIL)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ＤＩＣEDI受信データ(HEAD)・(DETAIL)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH570IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH570DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH570DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句)
        Call Me.SetConditionPrintPatternMSQL()          '条件設定
        If Me._Row.Item("PRTFLG").ToString = "1" Then 'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()               '未出力・両方(出力済、未出力併せて)
        End If                                       'Notes 1061 2012/05/15　終了
        Me._StrSql.Append(LMH570DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH570DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("L_DEL_KB", "L_DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("FILE_DIST", "FILE_DIST")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKAEDI_NO_L", "OUTKAEDI_NO_L")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("DENSO_NO", "DENSO_NO")
        map.Add("DATA_KB", "DATA_KB")
        map.Add("NONYU_CD", "NONYU_CD")
        map.Add("NONYU_NM_KB", "NONYU_NM_KB")
        map.Add("NONYU_NM_KANA", "NONYU_NM_KANA")
        map.Add("NONYU_NM_KANJI", "NONYU_NM_KANJI")
        map.Add("NONYU_JUSHO_KB", "NONYU_JUSHO_KB")
        map.Add("NONYU_JUSHO_KANA", "NONYU_JUSHO_KANA")
        map.Add("NONYU_JUSHO_KANJI", "NONYU_JUSHO_KANJI")
        map.Add("ATSUKA_BUKA_CD", "ATSUKA_BUKA_CD")
        map.Add("ZAIKO_BUKA_CD", "ZAIKO_BUKA_CD")
        map.Add("YAKUJO_NO", "YAKUJO_NO")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("CHIKU_CD", "CHIKU_CD")
        map.Add("YUSO_CD", "YUSO_CD")
        map.Add("SHUKKA_YMD", "SHUKKA_YMD")
        map.Add("KINKYU_FLG", "KINKYU_FLG")
        map.Add("SEIHIN_CD", "SEIHIN_CD")
        map.Add("NISUGATA_CD", "NISUGATA_CD")
        map.Add("NOUKI", "NOUKI")
        map.Add("SHANAI_MSG_KANJI", "SHANAI_MSG_KANJI")
        map.Add("NOUNYU_TEL_NO", "NOUNYU_TEL_NO")
        map.Add("TAN_I", "TAN_I")
        map.Add("KANSAN_RITSU", "KANSAN_RITSU")
        map.Add("KANSAN_SURYO", "KANSAN_SURYO")
        map.Add("ROUTE_NO", "ROUTE_NO")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("YOBI", "YOBI")
        map.Add("HANBAI_TANKA", "HANBAI_TANKA")
        map.Add("HANBAI_KINGAKU", "HANBAI_KINGAKU")
        map.Add("SHOUHIZEIGAKU", "SHOUHIZEIGAKU")
        map.Add("HAKKO_SHORIBI", "HAKKO_SHORIBI")
        map.Add("HAKKO_JIKAN", "HAKKO_JIKAN")
        map.Add("FURIKAE_SEIHIN_CD", "FURIKAE_SEIHIN_CD")
        map.Add("HINMEI", "HINMEI")
        map.Add("KITSUKE_MSG", "KITSUKE_MSG")
        map.Add("L_RECORD_STATUS", "L_RECORD_STATUS")
        map.Add("M_DEL_KB", "M_DEL_KB")
        map.Add("GYO", "GYO")
        map.Add("OUTKAEDI_NO_M", "OUTKAEDI_NO_M")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("LOCATION", "LOCATION")
        map.Add("IRIME", "IRIME")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("KOSU", "KOSU")
        map.Add("DIC_ZANSU", "DIC_ZANSU")
        map.Add("M_RECORD_STATUS", "M_RECORD_STATUS")
        map.Add("MESSAGE_01", "MESSAGE_01")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH570OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'パラメータ設定
        With Me._Row

            ''営業所コード
            'whereStr = .Item("NRS_BR_CD").ToString()
            'Me._StrSql.Append(vbNewLine)
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            ''荷主コード(大)
            'whereStr = .Item("CUST_CD_L").ToString()
            'Me._StrSql.Append(vbNewLine)
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            ''荷主コード(中)
            'whereStr = .Item("CUST_CD_M").ToString()
            'Me._StrSql.Append(vbNewLine)
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            '入出荷区分(Notes1007 2012/05/09)
            whereStr = .Item("INOUT_KB").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        ''SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine) 'Notes1061 
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR)) 'Notes1061 
            End If

            ''倉庫コード
            'whereStr = .Item("WH_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.NRS_WH_CD = @WH_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            'End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR)) 'Notes1061 
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR)) 'Notes1061 
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

            '2018/03/09 001040 20180201【LMS】出荷EDI受信一覧表_大阪日立物流・入荷が混ざる 対応 Annen add start 
            whereStr = .Item("INOUT_KB").ToString()
            Me._StrSql.Append(" AND HED.INOUT_KB = @INOUT_KB_EXTRACTION")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB_EXTRACTION", whereStr, DBDataType.CHAR))
            '2018/03/09 001040 20180201【LMS】出荷EDI受信一覧表_大阪日立物流・入荷が混ざる 対応 Annen add end 

            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            ''プリントフラグ
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

        ''SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR)) 'Notes1061 
            'End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR)) 'Notes1061 
            'End If

            '要望番号1077 2012.05.29 抽出条件からEDI出荷管理番号削除、伝票№追加 --- START ---
            ''EDI出荷管理番号
            'whereStr = .Item("EDI_CTL_NO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.EDI_CTL_NO = @EDI_CTL_NO ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            'End If

            '伝票№(オーダー№)
            whereStr = .Item("DENPYO_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.DENPYO_NO = @DENPYO_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", whereStr, DBDataType.NVARCHAR))
            End If
            '要望番号1077 2012.05.29 抽出条件からEDI出荷管理番号削除、伝票№追加 ---  END  ---

            'SHINODA 2014/12/12 要望管理2259対応 END 
            '約定№
            'whereStr = .Item("YAKUJO_NO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.YAKUJO_NO = @YAKUJO_NO ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YAKUJO_NO", whereStr, DBDataType.NVARCHAR))
            'End If
            'SHINODA 2014/12/12 要望管理2259対応 END

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
