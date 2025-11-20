' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH573    : 出荷EDI受信一覧表(埼玉_大日精化用)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH573DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH573DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       HED.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , '97'                                             AS PTN_ID    " & vbNewLine _
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
    ''' 埼玉 大日精化EDI受信データ
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_L AS EDIL                               " & vbNewLine _
                                     & "       -- 大日精化ＥＤＩ受信データ(ヘッダー)                        " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_HED_DNS  HED            " & vbNewLine _
                                     & "       ON HED.NRS_BR_CD = EDIL.NRS_BR_CD                             " & vbNewLine _
                                     & "       AND  HED.EDI_CTL_NO  = EDIL.EDI_CTL_NO                        " & vbNewLine _
                                     & "       -- 大日精化ＥＤＩ受信データ(明細)                             " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL              " & vbNewLine _
                                     & "                    ON HED.CRT_DATE  = DTL.CRT_DATE                  " & vbNewLine _
                                     & "                   AND HED.FILE_NAME = DTL.FILE_NAME                 " & vbNewLine _
                                     & "                   AND HED.REC_NO    = DTL.REC_NO                    " & vbNewLine _
                                     & "       -- EDI印刷種別テーブル                                        " & vbNewLine _
                                     & "       LEFT JOIN (                                                   " & vbNewLine _
                                     & "                   SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                     & "                        , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                     & "                        , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                     & "                    WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.PRINT_TP    = '03'             " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                     & "                    GROUP BY                                         " & vbNewLine _
                                     & "                          H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                     & "                        , H_EDI_PRINT.DENPYO_NO                      " & vbNewLine _
                                     & "                 ) HEDIPRINT                                         " & vbNewLine _
                                     & "              ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
                                     & "             AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
                                     & "             AND HEDIPRINT.DENPYO_NO  = HED.DENPYO_NO                " & vbNewLine _
                                     & "       -- 商品マスタ                                                 " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                     & "                    ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                   AND M_GOODS.GOODS_CD_NRS   = HED.SAKUIN_CD        " & vbNewLine _
                                     & "       -- 帳票パターンマスタ①(H_OUTKAEDI_HED_UKMの荷主より取得)     " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                     & "                    ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L           " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M           " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.PTN_ID      = '97'                 " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
                                     & "                    ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD " & vbNewLine _
                                     & "                   AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    " & vbNewLine _
                                     & "                   AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    " & vbNewLine _
                                     & "                   AND MR1.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "       -- 帳票パターンマスタ②(商品マスタより)                       " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               " & vbNewLine _
                                     & "                    ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.PTN_ID      = '97'                 " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                     & "                    ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                     & "                   AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                     & "                   AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                     & "                   AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "       -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                     & "                    ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                     & "                   AND MR3.PTN_ID             = '97'                 " & vbNewLine _
                                     & "                   AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                     & "                   AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_L HOUTKAL                " & vbNewLine _
                                     & "                   ON HED.NRS_BR_CD = HOUTKAL.NRS_BR_CD              " & vbNewLine _
                                     & "                   AND HED.EDI_CTL_NO = HOUTKAL.EDI_CTL_NO           " & vbNewLine
    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                            " & vbNewLine _
                                       & "        MAIN.RPT_ID           AS RPT_ID                            " & vbNewLine _
                                       & "      , MAIN.CRT_DATE        AS CRT_DATE                           " & vbNewLine _
                                       & "      --2015/6/9 削除区分保留追加に伴いCASE文変更                  " & vbNewLine _
                                       & "       , CASE                                                      " & vbNewLine _
                                       & " 	         WHEN  MAIN.DEL_FLG = 'D' THEN   '2'  --キャンセル       " & vbNewLine _
                                       & " 			 --またはOUTL DEL_KB=2                                   " & vbNewLine _
                                       & " 			 WHEN ODELKB = '3' THEN '4'  --保留                      " & vbNewLine _
                                       & "           WHEN MAIN.DEL_KB = '1' AND MAIN.ODELKB = '1'  THEN '3' --EDI削除         " & vbNewLine _
                                       & "           WHEN ISNULL(ODELKB,'') = '' THEN '3'                    " & vbNewLine _
                                       & "           ELSE MAIN.DEL_KB END AS DEL_KB                          " & vbNewLine _
                                       & "      , MAIN.FILE_NAME        AS FILE_NAME                         " & vbNewLine _
                                       & "      , MAIN.REC_NO           AS REC_NO                            " & vbNewLine _
                                       & "      , MAIN.NRS_BR_CD        AS NRS_BR_CD                         " & vbNewLine _
                                       & "      , MAIN.EDI_CTL_NO       AS EDI_CTL_NO                        " & vbNewLine _
                                       & "      , MAIN.OUTKA_CTL_NO     AS OUTKA_CTL_NO                      " & vbNewLine _
                                       & "      , MAIN.CUST_CD_L        AS CUST_CD_L                         " & vbNewLine _
                                       & "      , MAIN.CUST_CD_M        AS CUST_CD_M                         " & vbNewLine _
                                       & "      , MAIN.PRTFLG           AS PRTFLG                            " & vbNewLine _
                                       & "      , MAIN.CANCEL_FLG       AS CANCEL_FLG                        " & vbNewLine _
                                       & "      , MAIN.SHUKKA_BI        AS SHUKKA_BI                         " & vbNewLine _
                                       & "      , MAIN.CHAKU_BI         AS CHAKU_BI                          " & vbNewLine _
                                       & "      , MAIN.TOKUI_CD         AS TOKUI_CD                          " & vbNewLine _
                                       & "      , MAIN.TOKUI_NM         AS TOKUI_NM                          " & vbNewLine _
                                       & "      , MAIN.JUSHO            AS JUSHO                             " & vbNewLine _
                                       & "      , MAIN.TEL_NO           AS TEL_NO                            " & vbNewLine _
                                       & "      , MAIN.CHUMON_NO        AS CHUMON_NO                         " & vbNewLine _
                                       & "      , MAIN.DENPYO_NO        AS DENPYO_NO                         " & vbNewLine _
                                       & "      , MAIN.RENRAKU_JIKOU    AS RENRAKU_JIKOU                     " & vbNewLine _
                                       & "      , MAIN.SAKUIN_CD        AS SAKUIN_CD                         " & vbNewLine _
                                       & "      , MAIN.HINMEI           AS HINMEI                            " & vbNewLine _
                                       & "      , MAIN.HIN_NM           AS HIN_NM                            " & vbNewLine _
                                       & "      , MAIN.YELLOW_NO        AS YELLOW_NO                         " & vbNewLine _
                                       & "      , MAIN.BIKOU            AS BIKOU                             " & vbNewLine _
                                       & "      , MAIN.BUMON            AS BUMON                             " & vbNewLine _
                                       & "      , MAIN.SHIKEN_UMU       AS SHIKEN_UMU                        " & vbNewLine _
                                       & "      , MAIN.SHITEI_UMU       AS SHITEI_UMU                        " & vbNewLine _
                                       & "      , MAIN.YOBI             AS YOBI                              " & vbNewLine _
                                       & "      , MAIN.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                   " & vbNewLine _
                                       & "      , MAIN.ARR_PLAN_DATE    AS ARR_PLAN_DATE                     " & vbNewLine _
                                       & "      , MAIN.POISON_FLG       AS POISON_FLG                        " & vbNewLine _
                                       & "      , MAIN.RECORD_STATUS    AS RECORD_STATUS                     " & vbNewLine _
                                       & "      , MAIN.GYO              AS GYO                               " & vbNewLine _
                                       & "      , MAIN.EDI_CTL_NO_CHU   AS EDI_CTL_NO_CHU                    " & vbNewLine _
                                       & "      , MAIN.OUTKA_CTL_NO_CHU AS OUTKA_CTL_NO_CHU                  " & vbNewLine _
                                       & "      , MAIN.LOT_NO1          AS LOT_NO1                           " & vbNewLine _
                                       & "      , MAIN.LOT_NO2          AS LOT_NO2                           " & vbNewLine _
                                       & "      , MAIN.LOT_NO3          AS LOT_NO3                           " & vbNewLine _
                                       & "      , MAIN.YOURYO1          AS YOURYO1                           " & vbNewLine _
                                       & "      , MAIN.YOURYO2          AS YOURYO2                           " & vbNewLine _
                                       & "      , MAIN.YOURYO3          AS YOURYO3                           " & vbNewLine _
                                       & "      , MAIN.KOSU1            AS KOSU1                             " & vbNewLine _
                                       & "      , MAIN.KOSU2            AS KOSU2                             " & vbNewLine _
                                       & "      , MAIN.KOSU3            AS KOSU3                             " & vbNewLine _
                                       & "      , MAIN.BASHO1           AS BASHO1                            " & vbNewLine _
                                       & "      , MAIN.BASHO2           AS BASHO2                            " & vbNewLine _
                                       & "      , MAIN.BASHO3           AS BASHO3                            " & vbNewLine _
                                       & "      , MAIN.ZANSU            AS ZANSU                             " & vbNewLine _
                                       & "   FROM (                                                          " & vbNewLine _
                                       & "          SELECT CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID  " & vbNewLine _
                                       & "                      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID  " & vbNewLine _
                                       & "                 ELSE MR3.RPT_ID END  AS RPT_ID                    " & vbNewLine _
                                       & "               , HED.DEL_KB           AS DEL_KB                    " & vbNewLine _
                                       & "               , HED.CRT_DATE         AS CRT_DATE                  " & vbNewLine _
                                       & "               , HED.FILE_NAME        AS FILE_NAME                 " & vbNewLine _
                                       & "               , HED.REC_NO           AS REC_NO                    " & vbNewLine _
                                       & "               , HED.NRS_BR_CD        AS NRS_BR_CD                 " & vbNewLine _
                                       & "               , HED.EDI_CTL_NO       AS EDI_CTL_NO                " & vbNewLine _
                                       & "               , HED.OUTKA_CTL_NO     AS OUTKA_CTL_NO              " & vbNewLine _
                                       & "               , HED.CUST_CD_L        AS CUST_CD_L                 " & vbNewLine _
                                       & "               , HED.CUST_CD_M        AS CUST_CD_M                 " & vbNewLine _
                                       & "               , HED.PRTFLG           AS PRTFLG                    " & vbNewLine _
                                       & "               , HED.CANCEL_FLG       AS CANCEL_FLG                " & vbNewLine _
                                       & "               , HED.SHUKKA_BI        AS SHUKKA_BI                 " & vbNewLine _
                                       & "               , HED.CHAKU_BI         AS CHAKU_BI                  " & vbNewLine _
                                       & "               , HED.TOKUI_CD         AS TOKUI_CD                  " & vbNewLine _
                                       & "               , HED.TOKUI_NM         AS TOKUI_NM                  " & vbNewLine _
                                       & "               , HED.JUSHO            AS JUSHO                     " & vbNewLine _
                                       & "               , HED.TEL_NO           AS TEL_NO                    " & vbNewLine _
                                       & "               , HED.CHUMON_NO        AS CHUMON_NO                 " & vbNewLine _
                                       & "               , HED.DENPYO_NO        AS DENPYO_NO                 " & vbNewLine _
                                       & "               , HED.RENRAKU_JIKOU    AS RENRAKU_JIKOU             " & vbNewLine _
                                       & "               , HED.SAKUIN_CD        AS SAKUIN_CD                 " & vbNewLine _
                                       & "               , HED.HINMEI           AS HINMEI                    " & vbNewLine _
                                       & "               , HED.HIN_NM           AS HIN_NM                    " & vbNewLine _
                                       & "               , HED.YELLOW_NO        AS YELLOW_NO                 " & vbNewLine _
                                       & "               , HED.BIKOU            AS BIKOU                     " & vbNewLine _
                                       & "               , HED.BUMON            AS BUMON                     " & vbNewLine _
                                       & "               , HED.SHIKEN_UMU       AS SHIKEN_UMU                " & vbNewLine _
                                       & "               , HED.SHITEI_UMU       AS SHITEI_UMU                " & vbNewLine _
                                       & "               , HED.YOBI             AS YOBI                      " & vbNewLine _
                                       & "               , HED.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE           " & vbNewLine _
                                       & "               , HED.ARR_PLAN_DATE    AS ARR_PLAN_DATE             " & vbNewLine _
                                       & "               , HED.POISON_FLG       AS POISON_FLG                " & vbNewLine _
                                       & "               , HED.RECORD_STATUS    AS RECORD_STATUS             " & vbNewLine _
                                       & "               , MIN(DTL.GYO)         AS GYO                       " & vbNewLine _
                                       & "               , ''                   AS EDI_CTL_NO_CHU            " & vbNewLine _
                                       & "               , ''                   AS OUTKA_CTL_NO_CHU          " & vbNewLine _
                                       & "                --【ロット01】                                     " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.LOT_NO                        " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '01'              " & vbNewLine _
                                       & "                  ),'') AS LOT_NO1                                 " & vbNewLine _
                                       & "                --【ロット02】                                     " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.LOT_NO                        " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '02'              " & vbNewLine _
                                       & "                  ),'') AS LOT_NO2                                 " & vbNewLine _
                                       & "                --【ロット03】                                     " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.LOT_NO                        " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '03'              " & vbNewLine _
                                       & "                  ),'') AS LOT_NO3                                 " & vbNewLine _
                                       & "                --【容量01】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.YOURYO                        " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '01'              " & vbNewLine _
                                       & "                  ),0) AS YOURYO1                                  " & vbNewLine _
                                       & "                --【容量02】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.YOURYO                        " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '02'              " & vbNewLine _
                                       & "                  ),0) AS YOURYO2                                  " & vbNewLine _
                                       & "                --【容量03】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.YOURYO                        " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '03'              " & vbNewLine _
                                       & "                  ),0) AS YOURYO3                                  " & vbNewLine _
                                       & "                --【個数01】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.KOSU                          " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '01'              " & vbNewLine _
                                       & "                  ),0) AS KOSU1                                    " & vbNewLine _
                                       & "                --【個数02】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.KOSU                          " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '02'              " & vbNewLine _
                                       & "                  ),0) AS KOSU2                                    " & vbNewLine _
                                       & "                --【個数03】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.KOSU                          " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '03'              " & vbNewLine _
                                       & "                  ),0) AS KOSU3                                    " & vbNewLine _
                                       & "                --【場所01】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.BASHO                         " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '01'              " & vbNewLine _
                                       & "                  ),'') AS BASHO1                                  " & vbNewLine _
                                       & "                --【場所02】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.BASHO                         " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '02'              " & vbNewLine _
                                       & "                  ),'') AS BASHO2                                  " & vbNewLine _
                                       & "                --【場所03】                                       " & vbNewLine _
                                       & "                , ISNULL((SELECT DTL.BASHO                         " & vbNewLine _
                                       & "                            FROM $LM_TRN$..H_OUTKAEDI_DTL_DNS DTL  " & vbNewLine _
                                       & "                           WHERE DTL.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                       & "                             AND DTL.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                       & "                             AND DTL.REC_NO    = HED.REC_NO        " & vbNewLine _
                                       & "                             AND DTL.GYO       = '03'              " & vbNewLine _
                                       & "                  ),'') AS BASHO3                                  " & vbNewLine _
                                       & "                , 0                    AS ZANSU                    " & vbNewLine _
                                       & "                ,HED.DEL_FLG AS DEL_FLG                            " & vbNewLine _
                                       & "                ,HOUTKAL.DEL_KB AS ODELKB                          "

    ''' <summary>                             
    ''' 印刷データ抽出用 GROUP BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                      " & vbNewLine _
                                         & "          MR1.PTN_CD           " & vbNewLine _
                                         & "        , MR2.PTN_CD           " & vbNewLine _
                                         & "        , MR1.RPT_ID           " & vbNewLine _
                                         & "        , MR2.RPT_ID           " & vbNewLine _
                                         & "        , MR3.RPT_ID           " & vbNewLine _
                                         & "        , HED.DEL_KB           " & vbNewLine _
                                         & "        , HED.CRT_DATE         " & vbNewLine _
                                         & "        , HED.FILE_NAME        " & vbNewLine _
                                         & "        , HED.REC_NO           " & vbNewLine _
                                         & "        , HED.NRS_BR_CD        " & vbNewLine _
                                         & "        , HED.EDI_CTL_NO       " & vbNewLine _
                                         & "        , HED.OUTKA_CTL_NO     " & vbNewLine _
                                         & "        , HED.CUST_CD_L        " & vbNewLine _
                                         & "        , HED.CUST_CD_M        " & vbNewLine _
                                         & "        , HED.PRTFLG           " & vbNewLine _
                                         & "        , HED.CANCEL_FLG       " & vbNewLine _
                                         & "        , HED.SHUKKA_BI        " & vbNewLine _
                                         & "        , HED.CHAKU_BI         " & vbNewLine _
                                         & "        , HED.TOKUI_CD         " & vbNewLine _
                                         & "        , HED.TOKUI_NM         " & vbNewLine _
                                         & "        , HED.JUSHO            " & vbNewLine _
                                         & "        , HED.TEL_NO           " & vbNewLine _
                                         & "        , HED.CHUMON_NO        " & vbNewLine _
                                         & "        , HED.DENPYO_NO        " & vbNewLine _
                                         & "        , HED.RENRAKU_JIKOU    " & vbNewLine _
                                         & "        , HED.SAKUIN_CD        " & vbNewLine _
                                         & "        , HED.HINMEI           " & vbNewLine _
                                         & "        , HED.HIN_NM           " & vbNewLine _
                                         & "        , HED.YELLOW_NO        " & vbNewLine _
                                         & "        , HED.BIKOU            " & vbNewLine _
                                         & "        , HED.BUMON            " & vbNewLine _
                                         & "        , HED.SHIKEN_UMU       " & vbNewLine _
                                         & "        , HED.SHITEI_UMU       " & vbNewLine _
                                         & "        , HED.YOBI             " & vbNewLine _
                                         & "        , HED.OUTKA_PLAN_DATE  " & vbNewLine _
                                         & "        , HED.ARR_PLAN_DATE    " & vbNewLine _
                                         & "        , HED.POISON_FLG       " & vbNewLine _
                                         & "        , HED.RECORD_STATUS    " & vbNewLine _
                                         & "        , HED.DEL_FLG          " & vbNewLine _
                                         & "        , HOUTKAL.DEL_KB       " & vbNewLine _
                                         & "  ) MAIN                       " & vbNewLine

    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY             " & vbNewLine _
                                         & "       MAIN.CRT_DATE  " & vbNewLine _
                                         & "     , MAIN.FILE_NAME " & vbNewLine _
                                         & "     , MAIN.REC_NO    " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMH573IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH573DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH573DAC.SQL_FROM)      		'SQL構築(帳票種別用FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then
            Call Me.SetConditionMasterSQL_OUT()         '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()
        End If
        Call Me.SetConditionPrintPatternMSQL()          '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH573DAC", "SelectMPrt", cmd)

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
    ''' 浮間EDI受信データ(HEAD)・浮間EDI受信データ(DETAIL)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ダウケミEDI受信データ(HEAD)・(DETAIL)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH573IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH573DAC.SQL_SELECT)       'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH573DAC.SQL_FROM)         'SQL構築(印刷データ抽出用 FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then 
            Call Me.SetConditionMasterSQL_OUT()       '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()
            '未出力・両方(出力済、未出力併せて)
        End If
        Call Me.SetConditionPrintPatternMSQL()       '条件設定
        Me._StrSql.Append(LMH573DAC.SQL_GROUP_BY)    'SQL構築(印刷データ抽出用 GROUP BY句)
        Me._StrSql.Append(LMH573DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH573DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("DEL_KB","DEL_KB")
        map.Add("CRT_DATE","CRT_DATE")
        map.Add("FILE_NAME","FILE_NAME")
        map.Add("REC_NO","REC_NO")
        map.Add("NRS_BR_CD","NRS_BR_CD")
        map.Add("EDI_CTL_NO","EDI_CTL_NO")
        map.Add("OUTKA_CTL_NO","OUTKA_CTL_NO")
        map.Add("CUST_CD_L","CUST_CD_L")
        map.Add("CUST_CD_M","CUST_CD_M")
        map.Add("PRTFLG","PRTFLG")
        map.Add("CANCEL_FLG","CANCEL_FLG")
        map.Add("SHUKKA_BI","SHUKKA_BI")
        map.Add("CHAKU_BI","CHAKU_BI")
        map.Add("TOKUI_CD","TOKUI_CD")
        map.Add("TOKUI_NM","TOKUI_NM")
        map.Add("JUSHO","JUSHO")
        map.Add("TEL_NO","TEL_NO")
        map.Add("CHUMON_NO","CHUMON_NO")
        map.Add("DENPYO_NO","DENPYO_NO")
        map.Add("RENRAKU_JIKOU","RENRAKU_JIKOU")
        map.Add("SAKUIN_CD","SAKUIN_CD")
        map.Add("HINMEI","HINMEI")
        map.Add("HIN_NM","HIN_NM")
        map.Add("YELLOW_NO","YELLOW_NO")
        map.Add("BIKOU","BIKOU")
        map.Add("BUMON","BUMON")
        map.Add("SHIKEN_UMU","SHIKEN_UMU")
        map.Add("SHITEI_UMU","SHITEI_UMU")
        map.Add("YOBI","YOBI")
        map.Add("OUTKA_PLAN_DATE","OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE","ARR_PLAN_DATE")
        map.Add("POISON_FLG","POISON_FLG")
        map.Add("RECORD_STATUS","RECORD_STATUS")
        map.Add("GYO","GYO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO_CHU","OUTKA_CTL_NO_CHU")
        map.Add("LOT_NO1","LOT_NO1")
        map.Add("LOT_NO2","LOT_NO2")
        map.Add("LOT_NO3","LOT_NO3")
        map.Add("YOURYO1","YOURYO1")
        map.Add("YOURYO2","YOURYO2")
        map.Add("YOURYO3","YOURYO3")
        map.Add("KOSU1","KOSU1")
        map.Add("KOSU2","KOSU2")
        map.Add("KOSU3","KOSU3")
        map.Add("BASHO1","BASHO1")
        map.Add("BASHO2","BASHO2")
        map.Add("BASHO3","BASHO3")
        map.Add("ZANSU","ZANSU")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH573OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL()

        ''SQLパラメータ初期化(WHERE句で実施しているので、ここではコメント)
        'Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'パラメータ設定
        With Me._Row

            '入出荷区分
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
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
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

            ''プリントフラグ (未出力/出力済の判断をHEDIPRINTのレコード有無で行う)
            '2015/6/9
            '保留の場合は受信伝票は印刷対象としないが
            'EDI受信一覧は印刷対象とする(区分「保留」)
            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                    'Me._StrSql.Append(" AND HED.SYS_DEL_FLG=0")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール(出力済み'Notes1061)
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
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

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

            'プリントフラグ (未出力/出力済の判断をHEDIPRINTのレコード有無で行う)
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
