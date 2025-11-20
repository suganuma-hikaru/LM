' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH550    : EDI出荷伝票(浮間合成用)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH550DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH550DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       HED.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , '86'                                             AS PTN_ID    " & vbNewLine _
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
    ''' 大阪 浮間合成出荷EDI受信データHEAD - 浮間合成出荷EDI受信データDETAIL
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_UKM  HED                            " & vbNewLine _
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
                                          & "                     AND H_EDI_PRINT.PRINT_TP    = '01'             " & vbNewLine _
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
                                          & "      -- 営業所マスタ                                               " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR M_NRS_BR                   " & vbNewLine _
                                          & "                   ON M_NRS_BR.NRS_BR_CD  = HED.NRS_BR_CD           " & vbNewLine _
                                          & "      -- 区分マスタ(送付元取得)                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN Z_KBN1                        " & vbNewLine _
                                          & "                   ON Z_KBN1.KBN_GROUP_CD = 'S077'                  " & vbNewLine _
                                          & "                  AND Z_KBN1.KBN_CD       = HED.NRS_BR_CD           " & vbNewLine _
                                          & "      -- 浮間合成出荷EDI受信データ                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_UKM  DTL             " & vbNewLine _
                                          & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
                                          & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
                                          & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
                                          & "      -- 商品マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                          & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        " & vbNewLine _
                                          & "                  AND M_GOODS.GOODS_CD_NRS   = HED.SAKUIN_CD        " & vbNewLine _
                                          & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                          & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L           " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M           " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.PTN_ID      = '86'                 " & vbNewLine _
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
                                          & "                  AND M_CUSTRPT2.PTN_ID      = '86'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                          & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                          & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                          & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                          & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND MR3.PTN_ID             = '86'                 " & vbNewLine _
                                          & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                          & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                   " & vbNewLine _
                                       & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID  " & vbNewLine _
                                       & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID  " & vbNewLine _
                                       & "        ELSE MR3.RPT_ID END      AS RPT_ID                " & vbNewLine _
                                       & "      , HED.DEL_KB               AS DEL_KB                " & vbNewLine _
                                       & "      , HED.CRT_DATE			   AS CRT_DATE              " & vbNewLine _
                                       & "      , HED.FILE_NAME		       AS FILE_NAME             " & vbNewLine _
                                       & "      , HED.REC_NO			   AS REC_NO                " & vbNewLine _
                                       & "      , HED.NRS_BR_CD		       AS NRS_BR_CD             " & vbNewLine _
                                       & "      , HED.EDI_CTL_NO		   AS OUTKAEDI_NO_L         " & vbNewLine _
                                       & "      , HED.EDI_CTL_NO_CHU	   AS OUTKAEDI_NO_M         " & vbNewLine _
                                       & "      , HED.OUTKA_CTL_NO		   AS OUTKA_NO_L            " & vbNewLine _
                                       & "      , HED.OUTKA_CTL_NO_CHU	   AS OUTKA_NO_M            " & vbNewLine _
                                       & "      , HED.PRTFLG			   AS PRTFLG                " & vbNewLine _
                                       & "      , HED.CANCEL_FLG		   AS CANCEL_FLG            " & vbNewLine _
                                       & "      , HED.TOKUI_CD			   AS TOKUI_CD              " & vbNewLine _
                                       & "      , HED.TOKUI_NM			   AS TOKUI_NM              " & vbNewLine _
                                       & "      , HED.CHUMON_NO_1		   AS CHUMON_NO_1           " & vbNewLine _
                                       & "      , HED.DENPYO_NO		       AS DENPYO_NO             " & vbNewLine _
                                       & "      , HED.OUTKA_BI			   AS OUTKA_BI              " & vbNewLine _
                                       & "      , HED.HINMEI			   AS HINMEI                " & vbNewLine _
                                       & "      , HED.LOT_NO			   AS LOT_NO                " & vbNewLine _
                                       & "      , HED.YOURYO			   AS YOURYO                " & vbNewLine _
                                       & "      , HED.KOSU				   AS KOSU                  " & vbNewLine _
                                       & "      , HED.BASHO			       AS BASHO                 " & vbNewLine _
                                       & "      , HED.SAKUIN_CD		       AS SAKUIN_CD             " & vbNewLine _
                                       & "      , HED.TANTO_CD			   AS TANTO_CD              " & vbNewLine _
                                       & "      , HED.GENKA_BUMON		   AS GENKA_BUMON           " & vbNewLine _
                                       & "      , HED.BIN_NM			   AS BIN_NM                " & vbNewLine _
                                       & "      , HED.TEL_NO			   AS TEL_NO                " & vbNewLine _
                                       & "      , HED.JUSHO			       AS JUSHO                 " & vbNewLine _
                                       & "      , HED.SHIKEN_HYO		   AS SHIKEN_HYO            " & vbNewLine _
                                       & "      , HED.SITEI_DENPYO		   AS SITEI_DENPYO          " & vbNewLine _
                                       & "      , HED.DAIGAEHIN_CD		   AS DAIGAEHIN_CD          " & vbNewLine _
                                       & "      , HED.DAIGAEHIN_NM		   AS DAIGAEHIN_NM          " & vbNewLine _
                                       & "      , HED.HAISO_KB			   AS HAISO_KB              " & vbNewLine _
                                       & "      , HED.DOKUGEKI			   AS DOKUGEKI              " & vbNewLine _
                                       & "      , HED.Z_CD				   AS Z_CD                  " & vbNewLine _
                                       & "      , HED.RUIBETSU			   AS RUIBETSU              " & vbNewLine _
                                       & "      , HED.CUST_CD			   AS CUST_CD               " & vbNewLine _
                                       & "      , HED.YELLOW_CARD_NO	   AS YELLOW_CARD_NO        " & vbNewLine _
                                       & "      , HED.YUBIN_NO			   AS YUBIN_NO              " & vbNewLine _
                                       & "      , HED.CHUMON_NO_2		   AS CHUMON_NO_2           " & vbNewLine _
                                       & "      , HED.IRAI_SAKI		       AS IRAI_SAKI             " & vbNewLine _
                                       & "      , HED.SHISHIN_NO		   AS SHISHIN_NO            " & vbNewLine _
                                       & "      , HED.UN_NO			       AS UN_NO                 " & vbNewLine _
                                       & "      , HED.CHIKU_CD			   AS CHIKU_CD              " & vbNewLine _
                                       & "      , HED.RECORD_STATUS	       AS RECORD_STATUS         " & vbNewLine _
                                       & "      --(2012.11.07)要望番号1564 追加 --- START ---       " & vbNewLine _
                                       & "      , HED.ARR_BI	           AS ARR_BI                " & vbNewLine _
                                       & "      --(2012.11.07)要望番号1564 追加 ---  END  ---       " & vbNewLine _
                                       & "       --【明細01】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '01'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_01                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '01'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_01                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '01'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_01                                " & vbNewLine _
                                       & "       --【明細02】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '02'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_02                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '02'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_02                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '02'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_02                                " & vbNewLine _
                                       & "       --【明細03】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '03'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_03                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '03'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_03                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '03'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_03                                " & vbNewLine _
                                       & "       --【明細04】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '04'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_04                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '04'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_04                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '04'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_04                                " & vbNewLine _
                                       & "       --【明細05】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '05'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_05                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '05'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_05                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '05'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_05                                " & vbNewLine _
                                       & "       --【明細06】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '06'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_06                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '06'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_06                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '06'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_06                                " & vbNewLine _
                                       & "       --【明細07】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '07'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_07                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '07'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_07                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '07'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_07                                " & vbNewLine _
                                       & "       --【明細08】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '08'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_08                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '08'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_08                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '08'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_08                                " & vbNewLine _
                                       & "       --【明細09】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '09'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_09                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '09'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_09                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '09'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_09                                " & vbNewLine _
                                       & "       --【明細10】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '10'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_10                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '10'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_10                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '10'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_10                                " & vbNewLine _
                                       & "       --【明細11】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '11'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_11                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '11'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_11                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '11'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_11                                " & vbNewLine _
                                       & "       --【明細12】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '12'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_12                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '12'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_12                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '12'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_12                                " & vbNewLine _
                                       & "       --【明細13】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '13'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_13                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '13'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_13                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '13'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_13                                " & vbNewLine _
                                       & "       --【明細14】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '14'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_14                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '14'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_14                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '14'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_14                                " & vbNewLine _
                                       & "       --【明細15】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '15'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_15                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '15'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_15                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '15'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_15                                " & vbNewLine _
                                       & "       --【明細16】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '16'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_16                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '16'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_16                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '16'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_16                                " & vbNewLine _
                                       & "       --【明細17】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '17'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_17                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '17'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_17                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '17'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_17                                " & vbNewLine _
                                       & "       --【明細18】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '18'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_18                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '18'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_18                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '18'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_18                                " & vbNewLine _
                                       & "       --【明細19】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '19'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_19                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '19'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_19                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '19'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_19                                " & vbNewLine _
                                       & "       --【明細20】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '20'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_20                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '20'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_20                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '20'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_20                                " & vbNewLine _
                                       & "       --【明細21】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '21'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_21                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '21'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_21                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '21'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_21                                " & vbNewLine _
                                       & "       --【明細22】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '22'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_22                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '22'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_22                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '22'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_22                                " & vbNewLine _
                                       & "       --【明細23】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '23'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_23                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '23'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_23                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '23'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_23                                " & vbNewLine _
                                       & "       --【明細24】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '24'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_24                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '24'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_24                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '24'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_24                                " & vbNewLine _
                                       & "       --【明細25】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '25'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_25                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '25'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_25                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '25'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_25                                " & vbNewLine _
                                       & "       --【明細26】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '26'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_26                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '26'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_26                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '26'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_26                                " & vbNewLine _
                                       & "       --【明細27】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '27'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_27                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '27'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_27                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '27'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_27                                " & vbNewLine _
                                       & "       --【明細28】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '28'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_28                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '28'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_28                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '28'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_28                                " & vbNewLine _
                                       & "       --【明細29】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '29'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_29                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '29'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_29                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '29'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_29                                " & vbNewLine _
                                       & "       --【明細30】                                       " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.SOKUTEICHI                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '30'              " & vbNewLine _
                                       & "        ),'') AS SOKUTEICHI_30                            " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KOUMOKU                       " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '30'              " & vbNewLine _
                                       & "        ),'') AS KOUMOKU_30                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.KIKAKU                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '30'              " & vbNewLine _
                                       & "        ),'') AS KIKAKU_30                                " & vbNewLine _
                                       & "      , ''    AS GYO                                      " & vbNewLine _
                                       & "      , HED.YELLOW_CARD_NO         AS YELLOW_CARD_NO_B    " & vbNewLine _
                                       & "      , HED.SHISHIN_NO		     AS SHISHIN_NO_B        " & vbNewLine _
                                       & "      , HED.UN_NO			         AS UN_NO_B             " & vbNewLine _
                                       & "      , HED.TEL_NO			     AS TEL_NO_B            " & vbNewLine _
                                       & "      , HED.HINMEI			     AS HINMEI_B            " & vbNewLine _
                                       & "      , HED.LOT_NO			     AS LOT_NO_B            " & vbNewLine _
                                       & "      , 0         			     AS SURYO_B             " & vbNewLine _
                                       & "      , HED.OUTKA_BI			     AS OUTKA_BI_B          " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.HAKKO_NO                      " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '01'              " & vbNewLine _
                                       & "        ),'') AS HAKKO_NO                                 " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.TOKUI_NM_2                    " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '01'              " & vbNewLine _
                                       & "        ),'') AS TOKUI_NM_2                               " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.HINBAN                        " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '01'              " & vbNewLine _
                                       & "        ),'') AS HINBAN                                   " & vbNewLine _
                                       & "      , ISNULL((SELECT MDTL.HAKKO_NO                      " & vbNewLine _
                                       & "                  FROM $LM_TRN$..H_OUTKAEDI_DTL_UKM MDTL  " & vbNewLine _
                                       & "                 WHERE MDTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                   AND MDTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                   AND MDTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                   AND MDTL.GYO       = '01'              " & vbNewLine _
                                       & "        ),'') AS HAKKO_NO_B                               " & vbNewLine _
                                       & "      , ''                         AS BARCODE_LEFT        " & vbNewLine _
                                       & "      , ''                         AS BARCODE_RIGHT       " & vbNewLine _
                                       & "      , M_NRS_BR.NRS_BR_NM         AS NRS_BR_NM           " & vbNewLine _
                                       & "      , M_NRS_BR.AD_1              AS NRS_BR_AD_1         " & vbNewLine _
                                       & "      , M_NRS_BR.AD_2              AS NRS_BR_AD_2         " & vbNewLine _
                                       & "      , M_NRS_BR.AD_3              AS NRS_BR_AD_3         " & vbNewLine _
                                       & "      , Z_KBN1.KBN_NM1             AS MOTO_NM             " & vbNewLine _
                                       & "      , Z_KBN1.KBN_NM2             AS MOTO_AD             " & vbNewLine 

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' 大阪 浮間合成出荷EDI受信データHEAD - 浮間合成出荷EDI受信データDETAIL
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_UKM  HED                            " & vbNewLine _
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
                                     & "                     AND H_EDI_PRINT.PRINT_TP    = '01'             " & vbNewLine _
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
                                     & "      -- 営業所マスタ                                               " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR M_NRS_BR                   " & vbNewLine _
                                     & "                   ON M_NRS_BR.NRS_BR_CD  = HED.NRS_BR_CD           " & vbNewLine _
                                     & "      -- 区分マスタ(送付元取得)                                     " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN Z_KBN1                        " & vbNewLine _
                                     & "                   ON Z_KBN1.KBN_GROUP_CD = 'S077'                  " & vbNewLine _
                                     & "                  AND Z_KBN1.KBN_CD       = HED.NRS_BR_CD           " & vbNewLine _
                                     & "      -- 荷主マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                     & "                   ON M_CUST.NRS_BR_CD       = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_L       = @CUST_CD_L           " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_M       = @CUST_CD_M           " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                     & "      -- 商品マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                     & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_GOODS.GOODS_CD_NRS   = HED.SAKUIN_CD        " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                     & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L           " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M           " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.PTN_ID      = '86'                 " & vbNewLine _
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
                                     & "                  AND M_CUSTRPT2.PTN_ID      = '86'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                     & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                     & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                     & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                     & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                     & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND MR3.PTN_ID             = '86'                 " & vbNewLine _
                                     & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                     & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine


    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY            " & vbNewLine _
                                         & "       HED.CRT_DATE  " & vbNewLine _
                                         & "     , HED.FILE_NAME " & vbNewLine _
                                         & "     , HED.REC_NO    " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMH550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH550DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH550DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then    'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出用条件設定)
        End If                                          'Notes 1061 2012/05/15　終了
        'パラメータ設定
        Call Me.SetConditionPrintPatternMSQL(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH550DAC", "SelectMPrt", cmd)

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
    ''' ダウケミEDI受信データ(HEAD)・ダウケミEDI受信データ(DETAIL)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ダウケミEDI受信データ(HEAD)・(DETAIL)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH550DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH550DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then 'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()               '未出力・両方(出力済、未出力併せて)
        End If                                       'Notes 1061 2012/05/15　終了
        'SQL構築(印刷データ抽出用条件設定)
        Me._StrSql.Append(LMH550DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)

        'パラメータ設定
        Call Me.SetConditionPrintPatternMSQL(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH550DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKAEDI_NO_L", "OUTKAEDI_NO_L")
        map.Add("OUTKAEDI_NO_M", "OUTKAEDI_NO_M")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("TOKUI_CD", "TOKUI_CD")
        map.Add("TOKUI_NM", "TOKUI_NM")
        map.Add("CHUMON_NO_1", "CHUMON_NO_1")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("OUTKA_BI", "OUTKA_BI")
        map.Add("HINMEI", "HINMEI")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("YOURYO", "YOURYO")
        map.Add("KOSU", "KOSU")
        map.Add("BASHO", "BASHO")
        map.Add("SAKUIN_CD", "SAKUIN_CD")
        map.Add("TANTO_CD", "TANTO_CD")
        map.Add("GENKA_BUMON", "GENKA_BUMON")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("TEL_NO", "TEL_NO")
        map.Add("JUSHO", "JUSHO")
        map.Add("SHIKEN_HYO", "SHIKEN_HYO")
        map.Add("SITEI_DENPYO", "SITEI_DENPYO")
        map.Add("DAIGAEHIN_CD", "DAIGAEHIN_CD")
        map.Add("DAIGAEHIN_NM", "DAIGAEHIN_NM")
        map.Add("HAISO_KB", "HAISO_KB")
        map.Add("DOKUGEKI", "DOKUGEKI")
        map.Add("Z_CD", "Z_CD")
        map.Add("RUIBETSU", "RUIBETSU")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("YELLOW_CARD_NO", "YELLOW_CARD_NO")
        map.Add("YUBIN_NO", "YUBIN_NO")
        map.Add("CHUMON_NO_2", "CHUMON_NO_2")
        map.Add("IRAI_SAKI", "IRAI_SAKI")
        map.Add("SHISHIN_NO", "SHISHIN_NO")
        map.Add("UN_NO", "UN_NO")
        map.Add("CHIKU_CD", "CHIKU_CD")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("SOKUTEICHI_01", "SOKUTEICHI_01")
        map.Add("KOUMOKU_01", "KOUMOKU_01")
        map.Add("KIKAKU_01", "KIKAKU_01")
        map.Add("SOKUTEICHI_02", "SOKUTEICHI_02")
        map.Add("KOUMOKU_02", "KOUMOKU_02")
        map.Add("KIKAKU_02", "KIKAKU_02")
        map.Add("SOKUTEICHI_03", "SOKUTEICHI_03")
        map.Add("KOUMOKU_03", "KOUMOKU_03")
        map.Add("KIKAKU_03", "KIKAKU_03")
        map.Add("SOKUTEICHI_04", "SOKUTEICHI_04")
        map.Add("KOUMOKU_04", "KOUMOKU_04")
        map.Add("KIKAKU_04", "KIKAKU_04")
        map.Add("SOKUTEICHI_05", "SOKUTEICHI_05")
        map.Add("KOUMOKU_05", "KOUMOKU_05")
        map.Add("KIKAKU_05", "KIKAKU_05")
        map.Add("SOKUTEICHI_06", "SOKUTEICHI_06")
        map.Add("KOUMOKU_06", "KOUMOKU_06")
        map.Add("KIKAKU_06", "KIKAKU_06")
        map.Add("SOKUTEICHI_07", "SOKUTEICHI_07")
        map.Add("KOUMOKU_07", "KOUMOKU_07")
        map.Add("KIKAKU_07", "KIKAKU_07")
        map.Add("SOKUTEICHI_08", "SOKUTEICHI_08")
        map.Add("KOUMOKU_08", "KOUMOKU_08")
        map.Add("KIKAKU_08", "KIKAKU_08")
        map.Add("SOKUTEICHI_09", "SOKUTEICHI_09")
        map.Add("KOUMOKU_09", "KOUMOKU_09")
        map.Add("KIKAKU_09", "KIKAKU_09")
        map.Add("SOKUTEICHI_10", "SOKUTEICHI_10")
        map.Add("KOUMOKU_10", "KOUMOKU_10")
        map.Add("KIKAKU_10", "KIKAKU_10")
        map.Add("SOKUTEICHI_11", "SOKUTEICHI_11")
        map.Add("KOUMOKU_11", "KOUMOKU_11")
        map.Add("KIKAKU_11", "KIKAKU_11")
        map.Add("SOKUTEICHI_12", "SOKUTEICHI_12")
        map.Add("KOUMOKU_12", "KOUMOKU_12")
        map.Add("KIKAKU_12", "KIKAKU_12")
        map.Add("SOKUTEICHI_13", "SOKUTEICHI_13")
        map.Add("KOUMOKU_13", "KOUMOKU_13")
        map.Add("KIKAKU_13", "KIKAKU_13")
        map.Add("SOKUTEICHI_14", "SOKUTEICHI_14")
        map.Add("KOUMOKU_14", "KOUMOKU_14")
        map.Add("KIKAKU_14", "KIKAKU_14")
        map.Add("SOKUTEICHI_15", "SOKUTEICHI_15")
        map.Add("KOUMOKU_15", "KOUMOKU_15")
        map.Add("KIKAKU_15", "KIKAKU_15")
        map.Add("SOKUTEICHI_16", "SOKUTEICHI_16")
        map.Add("KOUMOKU_16", "KOUMOKU_16")
        map.Add("KIKAKU_16", "KIKAKU_16")
        map.Add("SOKUTEICHI_17", "SOKUTEICHI_17")
        map.Add("KOUMOKU_17", "KOUMOKU_17")
        map.Add("KIKAKU_17", "KIKAKU_17")
        map.Add("SOKUTEICHI_18", "SOKUTEICHI_18")
        map.Add("KOUMOKU_18", "KOUMOKU_18")
        map.Add("KIKAKU_18", "KIKAKU_18")
        map.Add("SOKUTEICHI_19", "SOKUTEICHI_19")
        map.Add("KOUMOKU_19", "KOUMOKU_19")
        map.Add("KIKAKU_19", "KIKAKU_19")
        map.Add("SOKUTEICHI_20", "SOKUTEICHI_20")
        map.Add("KOUMOKU_20", "KOUMOKU_20")
        map.Add("KIKAKU_20", "KIKAKU_20")
        map.Add("SOKUTEICHI_21", "SOKUTEICHI_21")
        map.Add("KOUMOKU_21", "KOUMOKU_21")
        map.Add("KIKAKU_21", "KIKAKU_21")
        map.Add("SOKUTEICHI_22", "SOKUTEICHI_22")
        map.Add("KOUMOKU_22", "KOUMOKU_22")
        map.Add("KIKAKU_22", "KIKAKU_22")
        map.Add("SOKUTEICHI_23", "SOKUTEICHI_23")
        map.Add("KOUMOKU_23", "KOUMOKU_23")
        map.Add("KIKAKU_23", "KIKAKU_23")
        map.Add("SOKUTEICHI_24", "SOKUTEICHI_24")
        map.Add("KOUMOKU_24", "KOUMOKU_24")
        map.Add("KIKAKU_24", "KIKAKU_24")
        map.Add("SOKUTEICHI_25", "SOKUTEICHI_25")
        map.Add("KOUMOKU_25", "KOUMOKU_25")
        map.Add("KIKAKU_25", "KIKAKU_25")
        map.Add("SOKUTEICHI_26", "SOKUTEICHI_26")
        map.Add("KOUMOKU_26", "KOUMOKU_26")
        map.Add("KIKAKU_26", "KIKAKU_26")
        map.Add("SOKUTEICHI_27", "SOKUTEICHI_27")
        map.Add("KOUMOKU_27", "KOUMOKU_27")
        map.Add("KIKAKU_27", "KIKAKU_27")
        map.Add("SOKUTEICHI_28", "SOKUTEICHI_28")
        map.Add("KOUMOKU_28", "KOUMOKU_28")
        map.Add("KIKAKU_28", "KIKAKU_28")
        map.Add("SOKUTEICHI_29", "SOKUTEICHI_29")
        map.Add("KOUMOKU_29", "KOUMOKU_29")
        map.Add("KIKAKU_29", "KIKAKU_29")
        map.Add("SOKUTEICHI_30", "SOKUTEICHI_30")
        map.Add("KOUMOKU_30", "KOUMOKU_30")
        map.Add("KIKAKU_30", "KIKAKU_30")
        map.Add("GYO", "GYO")
        map.Add("HAKKO_NO", "HAKKO_NO")
        map.Add("TOKUI_NM_2", "TOKUI_NM_2")
        map.Add("HINBAN", "HINBAN")
        map.Add("YELLOW_CARD_NO_B", "YELLOW_CARD_NO_B")
        map.Add("SHISHIN_NO_B", "SHISHIN_NO_B")
        map.Add("UN_NO_B", "UN_NO_B")
        map.Add("TEL_NO_B", "TEL_NO_B")
        map.Add("HINMEI_B", "HINMEI_B")
        map.Add("LOT_NO_B", "LOT_NO_B")
        map.Add("SURYO_B", "SURYO_B")
        map.Add("OUTKA_BI_B", "OUTKA_BI_B")
        map.Add("HAKKO_NO_B", "HAKKO_NO_B")
        map.Add("BARCODE_LEFT", "BARCODE_LEFT")
        map.Add("BARCODE_RIGHT", "BARCODE_RIGHT")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("NRS_BR_AD_1", "NRS_BR_AD_1")
        map.Add("NRS_BR_AD_2", "NRS_BR_AD_2")
        map.Add("NRS_BR_AD_3", "NRS_BR_AD_3")
        map.Add("MOTO_NM", "MOTO_NM")
        map.Add("MOTO_AD", "MOTO_AD")
        map.Add("ARR_BI", "ARR_BI")     '(2012.11.07)要望番号1564 追加

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH550OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL(ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", Me._Row.Item("INOUT_KB").ToString(), DBDataType.CHAR)) 'Notes1007 2012/05/09 (入出荷区分の変数化対応)

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
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

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

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" HED.NRS_BR_CD = @NRS_BR_CD")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
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

            ''入出荷区分
            'whereStr = .Item("INOUT_KB").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.INOUT_KB = @INOUT_KB")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))
            'End If

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
