' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH540    : EDI出荷取消チェックリスト
'  作  成  者       :  寺川徹
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH540DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH540DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       EOUTL.NRS_BR_CD                                  AS NRS_BR_CD " & vbNewLine _
                                            & "      , 'AH'                                             AS PTN_ID    " & vbNewLine _
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
    ''' EDI出荷(大) - EDI出荷(中),荷(大),出荷(中),荷主Ｍ,商品Ｍ,ユーザーＭ,区分Ｍ
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = " FROM                                                               " & vbNewLine _
                                          & " -- EDI出荷(大)                                                     " & vbNewLine _
                                          & "      $LM_TRN$..H_OUTKAEDI_L EOUTL                                  " & vbNewLine _
                                          & "      -- EDI出荷(中)                                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_M EOUTM                  " & vbNewLine _
                                          & "                   ON EOUTM.NRS_BR_CD        = EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND EOUTM.EDI_CTL_NO       = EOUTL.EDI_CTL_NO     " & vbNewLine _
                                          & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
                                          & "      LEFT JOIN (                                                   " & vbNewLine _
                                          & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                          & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                          & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                          & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                          & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.PRINT_TP    = '05'             " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                          & "                   GROUP BY                                         " & vbNewLine _
                                          & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                          & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                          & "                ) HEDIPRINT                                         " & vbNewLine _
                                          & "             ON HEDIPRINT.NRS_BR_CD  = EOUTL.NRS_BR_CD              " & vbNewLine _
                                          & "            AND HEDIPRINT.EDI_CTL_NO = EOUTL.EDI_CTL_NO             " & vbNewLine _
                                          & "--            AND HEDIPRINT.EDI_CTL_NO = CASE WHEN SUBSTRING(EOUTL.FREE_C30,1,3) = '04-'     " & vbNewLine _
                                          & "--                                            THEN SUBSTRING(EOUTL.FREE_C30,4,9)             " & vbNewLine _
                                          & "--                                            ELSE EOUTL.EDI_CTL_NO  END                     " & vbNewLine _
                                          & "      -- 荷主マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                          & "                   ON M_CUST.NRS_BR_CD       = EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_L       = EOUTL.CUST_CD_L      " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_M       = EOUTL.CUST_CD_M      " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                          & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                          & "      -- 商品マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                          & "                   ON M_GOODS.NRS_BR_CD      = EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND M_GOODS.GOODS_CD_NRS   = EOUTM.NRS_GOODS_CD   " & vbNewLine _
                                          & "      -- 帳票パターンマスタ①(EDI出荷の荷主より取得)                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                          & "                   ON M_CUSTRPT1.NRS_BR_CD   = EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_L   = EOUTL.CUST_CD_L      " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_M   = EOUTL.CUST_CD_M      " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.PTN_ID      = 'AH'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR1                           " & vbNewLine _
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
                                          & "                  AND M_CUSTRPT2.PTN_ID      = 'AH'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                          & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                          & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                          & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                          & "                   ON MR3.NRS_BR_CD         =  EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND MR3.PTN_ID             = 'AH'                 " & vbNewLine _
                                          & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                          & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    '''' <summary>                         
    '''' 帳票種別取得用 SELECT句           
    '''' </summary>                        
    '''' <remarks></remarks>
    'Private Const SQL_MPrt_WHERE As String = " WHERE                                  " & vbNewLine _
    '                                       & "       EOUTL.NRS_BR_CD    = @NRS_BR_CD  " & vbNewLine _
    '                                       & "   AND EOUTL.CUST_CD_L    = @CUST_CD_L  " & vbNewLine _
    '                                       & "   AND EOUTL.CUST_CD_M    = @CUST_CD_M  " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                     " & vbNewLine _
     & "  TTL_QUERRY.RPT_ID                                                                           " & vbNewLine _
          & " ,TTL_QUERRY.EDI_OUTKA_NO_L                                                              " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_NO_L                                                                  " & vbNewLine _
          & " ,TTL_QUERRY.CUST_CD_L                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.CUST_CD_M                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.CUST_NM_L                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.CUST_NM_M                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_PLAN_DATE                                                             " & vbNewLine _
          & " ,TTL_QUERRY.ARR_PLAN_DATE                                                               " & vbNewLine _
          & " ,TTL_QUERRY.DEST_CD                                                                     " & vbNewLine _
          & " ,TTL_QUERRY.DEST_NM                                                                     " & vbNewLine _
          & " ,TTL_QUERRY.DEST_AD_1                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.DEST_AD_2                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.DEST_AD_3                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.UNSO_NM                                                                     " & vbNewLine _
          & " ,TTL_QUERRY.BIN_KB                                                                      " & vbNewLine _
          & " ,TTL_QUERRY.EDI_OUTKA_NO_M                                                              " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_NO_M                                                                  " & vbNewLine _
          & " ,TTL_QUERRY.CUST_GOODS_CD                                                               " & vbNewLine _
          & " ,TTL_QUERRY.GOODS_NM                                                                    " & vbNewLine _
          & " ,TTL_QUERRY.NB_UT                                                                       " & vbNewLine _
          & " ,TTL_QUERRY.IRIME                                                                       " & vbNewLine _
          & " ,TTL_QUERRY.IRIME_UT                                                                    " & vbNewLine _
          & " ,TTL_QUERRY.LOT_NO                                                                      " & vbNewLine _
          & " ,TTL_QUERRY.CUST_ORD_NO                                                                 " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_STATE_KB                                                              " & vbNewLine _
          & " ,TTL_QUERRY.TOU_NO                                                                      " & vbNewLine _
          & " ,TTL_QUERRY.SITU_NO                                                                     " & vbNewLine _
          & " ,SUM(TTL_QUERRY.ALCTD_NB_S) AS ALCTD_NB_S                                               " & vbNewLine _
          & " ,TTL_QUERRY.ALCTD_NB_M                                                                  " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_TTL_NB                                                                " & vbNewLine _
          & " ,TTL_QUERRY.MATOME_PRINT_DATE                                                           " & vbNewLine _
          & " ,TTL_QUERRY.MATOME_PRINT_TIME                                                           " & vbNewLine _
          & " FROM                                                                                    " & vbNewLine _
          & " (                                                                                       " & vbNewLine _
          & " SELECT                                                                                  " & vbNewLine _
          & "  QUERRY.RPT_ID                                                                          " & vbNewLine _
          & " ,MIN(QUERRY.EDI_OUTKA_NO_L) AS EDI_OUTKA_NO_L                                           " & vbNewLine _
          & " ,QUERRY.OUTKA_NO_L                                                                      " & vbNewLine _
          & " ,QUERRY.CUST_CD_L                                                                       " & vbNewLine _
          & " ,QUERRY.CUST_CD_M                                                                       " & vbNewLine _
          & " ,QUERRY.CUST_NM_L                                                                       " & vbNewLine _
          & " ,QUERRY.CUST_NM_M                                                                       " & vbNewLine _
          & " ,QUERRY.OUTKA_PLAN_DATE                                                                 " & vbNewLine _
          & " ,QUERRY.ARR_PLAN_DATE                                                                   " & vbNewLine _
          & " ,QUERRY.DEST_CD                                                                         " & vbNewLine _
          & " ,QUERRY.DEST_NM                                                                         " & vbNewLine _
          & " ,QUERRY.DEST_AD_1                                                                       " & vbNewLine _
          & " ,QUERRY.DEST_AD_2                                                                       " & vbNewLine _
          & " ,QUERRY.DEST_AD_3                                                                       " & vbNewLine _
          & " ,QUERRY.UNSO_NM                                                                         " & vbNewLine _
          & " ,QUERRY.BIN_KB                                                                          " & vbNewLine _
          & " ,MIN(QUERRY.EDI_OUTKA_NO_M) AS EDI_OUTKA_NO_M                                           " & vbNewLine _
          & " ,QUERRY.OUTKA_NO_M                                                                      " & vbNewLine _
          & " ,QUERRY.CUST_GOODS_CD                                                                   " & vbNewLine _
          & " ,QUERRY.GOODS_NM                                                                        " & vbNewLine _
          & " ,QUERRY.NB_UT                                                                           " & vbNewLine _
          & " ,QUERRY.IRIME                                                                           " & vbNewLine _
          & " ,QUERRY.IRIME_UT                                                                        " & vbNewLine _
          & " ,QUERRY.LOT_NO                                                                          " & vbNewLine _
          & " ,QUERRY.CUST_ORD_NO                                                                     " & vbNewLine _
          & " ,QUERRY.OUTKA_STATE_KB                                                                  " & vbNewLine _
          & " ,QUERRY.TOU_NO                                                                          " & vbNewLine _
          & " ,QUERRY.SITU_NO                                                                         " & vbNewLine _
          & " ,QUERRY.ALCTD_NB_S                                                                      " & vbNewLine _
          & " ,QUERRY.ALCTD_NB_M                                                                      " & vbNewLine _
          & " ,QUERRY.OUTKA_TTL_NB                                                                    " & vbNewLine _
          & " ,QUERRY.MATOME_PRINT_DATE                                                               " & vbNewLine _
          & " ,QUERRY.MATOME_PRINT_TIME                                                               " & vbNewLine _
          & " FROM                                                                                    " & vbNewLine _
          & " (                                                                                       " & vbNewLine _
          & "SELECT                                                                                   " & vbNewLine _
          & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                 " & vbNewLine _
          & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                 " & vbNewLine _
          & "        ELSE MR3.RPT_ID END                                         AS RPT_ID            " & vbNewLine _
          & "--        , CASE WHEN SUBSTRING(EOUTL.FREE_C30,1,3) = '04-' THEN SUBSTRING(EOUTL.FREE_C30,4,9) " & vbNewLine _
          & "--        ELSE EOUTL.EDI_CTL_NO   END                                 AS EDI_OUTKA_NO_L    " & vbNewLine _
          & "      , EOUTL.EDI_CTL_NO                                            AS EDI_OUTKA_NO_L    " & vbNewLine _
          & "      , EOUTL.OUTKA_CTL_NO                                          AS OUTKA_NO_L        " & vbNewLine _
          & "      , EOUTL.CUST_CD_L                                             AS CUST_CD_L         " & vbNewLine _
          & "      , EOUTL.CUST_CD_M                                             AS CUST_CD_M         " & vbNewLine _
          & "      , M_CUST.CUST_NM_L                                            AS CUST_NM_L         " & vbNewLine _
          & "      , M_CUST.CUST_NM_M                                            AS CUST_NM_M         " & vbNewLine _
          & "      , OUTKAL.OUTKA_PLAN_DATE                                      AS OUTKA_PLAN_DATE   " & vbNewLine _
          & "      , OUTKAL.ARR_PLAN_DATE                                        AS ARR_PLAN_DATE     " & vbNewLine _
          & "      ,CASE OUTKAL.DEST_KB WHEN '00'    THEN M_DEST.DEST_CD                              " & vbNewLine _
          & "                           WHEN '01'    THEN OUTKAL.DEST_CD                              " & vbNewLine _
          & "                           WHEN '02'    THEN EOUTL.DEST_CD                               " & vbNewLine _
          & "                           ELSE EOUTL.DEST_CD                                            " & vbNewLine _
          & "      END                                                           AS DEST_CD           " & vbNewLine _
          & "      ,CASE OUTKAL.DEST_KB WHEN '00'    THEN M_DEST.DEST_NM                              " & vbNewLine _
          & "                           WHEN '01'    THEN OUTKAL.DEST_NM                              " & vbNewLine _
          & "                           WHEN '02'    THEN EOUTL.DEST_NM                               " & vbNewLine _
          & "                           ELSE EOUTL.DEST_NM                                            " & vbNewLine _
          & "      END                                                           AS DEST_NM           " & vbNewLine _
          & "      ,CASE OUTKAL.DEST_KB WHEN '00'    THEN M_DEST.AD_1                                 " & vbNewLine _
          & "                           WHEN '01'    THEN OUTKAL.DEST_AD_1                            " & vbNewLine _
          & "                           WHEN '02'    THEN EOUTL.DEST_AD_1                             " & vbNewLine _
          & "                           ELSE EOUTL.DEST_AD_1                                          " & vbNewLine _
          & "      END                                                           AS DEST_AD_1         " & vbNewLine _
          & "      ,CASE OUTKAL.DEST_KB WHEN '00'    THEN M_DEST.AD_2                                 " & vbNewLine _
          & "                           WHEN '01'    THEN OUTKAL.DEST_AD_2                            " & vbNewLine _
          & "                           WHEN '02'    THEN EOUTL.DEST_AD_2                             " & vbNewLine _
          & "                           ELSE EOUTL.DEST_AD_2                                          " & vbNewLine _
          & "      END                                                           AS DEST_AD_2         " & vbNewLine _
          & "      ,CASE OUTKAL.DEST_KB WHEN '00'    THEN M_DEST.AD_3                                 " & vbNewLine _
          & "                           WHEN '01'    THEN OUTKAL.DEST_AD_3                            " & vbNewLine _
          & "                           WHEN '02'    THEN EOUTL.DEST_AD_3                             " & vbNewLine _
          & "                           ELSE EOUTL.DEST_AD_3                                          " & vbNewLine _
          & "      END                                                           AS DEST_AD_3         " & vbNewLine _
          & "      , M_UNSOCO.UNSOCO_NM                                          AS UNSO_NM           " & vbNewLine _
          & "      , KBN.KBN_NM1                                                 AS BIN_KB            " & vbNewLine _
          & "      , EOUTM.EDI_CTL_NO_CHU                                        AS EDI_OUTKA_NO_M    " & vbNewLine _
          & "      , OUTKAM.OUTKA_NO_M                                           AS OUTKA_NO_M        " & vbNewLine _
          & "      , M_GOODS.GOODS_CD_CUST                                       AS CUST_GOODS_CD     " & vbNewLine _
          & "      , M_GOODS.GOODS_NM_1                                          AS GOODS_NM          " & vbNewLine _
          & "      , M_GOODS.NB_UT                                               AS NB_UT             " & vbNewLine _
          & "      , OUTKAM.IRIME                                                AS IRIME             " & vbNewLine _
          & "      , OUTKAM.IRIME_UT                                             AS IRIME_UT          " & vbNewLine _
          & "      , OUTKAM.LOT_NO                                               AS LOT_NO            " & vbNewLine _
          & "      , OUTKAL.CUST_ORD_NO                                          AS CUST_ORD_NO       " & vbNewLine _
          & "      , ISNULL(OUTKAL.OUTKA_STATE_KB,'')                            AS OUTKA_STATE_KB    " & vbNewLine _
          & "      , OUTKAS.TOU_NO                                               AS TOU_NO            " & vbNewLine _
          & "      , OUTKAS.SITU_NO                                              AS SITU_NO           " & vbNewLine _
          & "      , ISNULL(OUTKAS.ALCTD_NB,'0')                                 AS ALCTD_NB_S        " & vbNewLine _
          & "      , ISNULL(OUTKAM.ALCTD_NB,'0')                                 AS ALCTD_NB_M        " & vbNewLine _
          & "      , ISNULL(OUTKAM.OUTKA_TTL_NB,EOUTM.OUTKA_HASU)                AS OUTKA_TTL_NB      " & vbNewLine _
          & "      , OUTKAL.MATOME_PRINT_DATE                                    AS MATOME_PRINT_DATE " & vbNewLine _
          & "      , OUTKAL.MATOME_PRINT_TIME                                    AS MATOME_PRINT_TIME " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' EDI出荷(大) - EDI出荷(中),出荷(大),出荷(中),運送(大),荷主Ｍ,商品Ｍ,運送会社Ｍ,届先Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM As String = "FROM                                                           " & vbNewLine _
          & "       -- EDI出荷(大)                                                                     " & vbNewLine _
          & "       $LM_TRN$..H_OUTKAEDI_L EOUTL                                                       " & vbNewLine _
          & "       --DIC赤黒                                                                          " & vbNewLine _
          & "       INNER JOIN (                                                                       " & vbNewLine _
          & "                   SELECT kuro.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
          & "                         ,kuro.EDI_CTL_NO AS EDI_CTL_NO                                   " & vbNewLine _
          & "                     FROM $LM_TRN$..H_OUTKAEDI_HED_DIC AS aka                             " & vbNewLine _
          & "                          INNER JOIN $LM_TRN$..H_OUTKAEDI_HED_DIC AS kuro                 " & vbNewLine _
          & "                                  ON kuro.EDI_CTL_NO = aka.UPD_DATE + aka.UPD_TIME        " & vbNewLine

    Private Const SQL_FROM_AFTER As String = "                    WHERE                                 " & vbNewLine _
          & "                          aka.CANCEL_FLG = '1'                                            " & vbNewLine _
          & "                      AND aka.DATA_KB    = '10'                                           " & vbNewLine _
          & "                  ) AKADATE                                                               " & vbNewLine _
          & "              ON AKADATE.NRS_BR_CD  = EOUTL.NRS_BR_CD                                     " & vbNewLine _
          & "             AND AKADATE.EDI_CTL_NO = EOUTL.EDI_CTL_NO                                    " & vbNewLine _
          & "      -- EDI印刷種別テーブル                                                              " & vbNewLine _
          & "      LEFT JOIN (                                                                         " & vbNewLine _
          & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                                 " & vbNewLine _
          & "                       , H_EDI_PRINT.NRS_BR_CD                                            " & vbNewLine _
          & "                       , H_EDI_PRINT.EDI_CTL_NO                                           " & vbNewLine _
          & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                                " & vbNewLine _
          & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                             " & vbNewLine _
          & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                             " & vbNewLine _
          & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                             " & vbNewLine _
          & "                     AND H_EDI_PRINT.PRINT_TP    = '05'                                   " & vbNewLine _
          & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB                              " & vbNewLine _
          & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'                                    " & vbNewLine _
          & "                   GROUP BY                                                               " & vbNewLine _
          & "                         H_EDI_PRINT.NRS_BR_CD                                            " & vbNewLine _
          & "                       , H_EDI_PRINT.EDI_CTL_NO                                           " & vbNewLine _
          & "                ) HEDIPRINT                                                               " & vbNewLine _
          & "             ON HEDIPRINT.NRS_BR_CD  = EOUTL.NRS_BR_CD                                    " & vbNewLine _
          & "--        AND HEDIPRINT.EDI_CTL_NO = CASE WHEN                                              " & vbNewLine _
          & "--                                        SUBSTRING(EOUTL.FREE_C30,1,3) = '04-'             " & vbNewLine _
          & "--                                   THEN SUBSTRING(EOUTL.FREE_C30,4,9)                     " & vbNewLine _
          & "--                                   ELSE EOUTL.EDI_CTL_NO  END                             " & vbNewLine _
          & "          AND HEDIPRINT.EDI_CTL_NO = EOUTL.EDI_CTL_NO                                     " & vbNewLine _
          & "       -- EDI出荷(中)                                                                     " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_M EOUTM                                       " & vbNewLine _
          & "                    ON EOUTM.NRS_BR_CD        = EOUTL.NRS_BR_CD                           " & vbNewLine _
          & "                   AND EOUTM.EDI_CTL_NO       = EOUTL.EDI_CTL_NO                          " & vbNewLine _
          & "       -- 出荷(大)                                                                        " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_TRN$..C_OUTKA_L OUTKAL                                         " & vbNewLine _
          & "                    ON OUTKAL.NRS_BR_CD       = EOUTL.NRS_BR_CD                           " & vbNewLine _
          & "                   AND OUTKAL.OUTKA_NO_L      = EOUTL.OUTKA_CTL_NO                        " & vbNewLine _
          & "       -- 出荷(中)                                                                        " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_TRN$..C_OUTKA_M OUTKAM                                         " & vbNewLine _
          & "                    ON OUTKAM.NRS_BR_CD       = EOUTL.NRS_BR_CD                           " & vbNewLine _
          & "                   AND OUTKAM.OUTKA_NO_L      = EOUTL.OUTKA_CTL_NO                        " & vbNewLine _
          & "                   AND OUTKAM.OUTKA_NO_M      = EOUTM.OUTKA_CTL_NO_CHU                    " & vbNewLine _
          & "       -- 出荷(小)                                                                        " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_TRN$..C_OUTKA_S OUTKAS                                         " & vbNewLine _
          & "                    ON OUTKAS.NRS_BR_CD       = EOUTL.NRS_BR_CD                           " & vbNewLine _
          & "                   AND OUTKAS.OUTKA_NO_L      = EOUTL.OUTKA_CTL_NO                        " & vbNewLine _
          & "                   AND OUTKAS.OUTKA_NO_M      = EOUTM.OUTKA_CTL_NO_CHU                    " & vbNewLine _
          & "       -- 運送(大)                                                                        " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_TRN$..F_UNSO_L UNSOL                                           " & vbNewLine _
          & "                    ON UNSOL.NRS_BR_CD     = EOUTL.NRS_BR_CD                              " & vbNewLine _
          & "                   AND UNSOL.INOUTKA_NO_L  = EOUTL.OUTKA_CTL_NO                           " & vbNewLine _
          & "                   AND EOUTL.OUTKA_CTL_NO     <> ''                                       " & vbNewLine _
          & "       -- 商品マスタ(出荷)                                                                " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                                          " & vbNewLine _
          & "                    ON M_GOODS.NRS_BR_CD      = OUTKAL.NRS_BR_CD                          " & vbNewLine _
          & "                   AND M_GOODS.GOODS_CD_NRS   = OUTKAM.GOODS_CD_NRS                       " & vbNewLine _
          & "       -- 荷主マスタ                                                                      " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                                            " & vbNewLine _
          & "                    ON M_CUST.NRS_BR_CD       = EOUTL.NRS_BR_CD                           " & vbNewLine _
          & "                   AND M_CUST.CUST_CD_L       = EOUTL.CUST_CD_L                           " & vbNewLine _
          & "                   AND M_CUST.CUST_CD_M       = EOUTL.CUST_CD_M                           " & vbNewLine _
          & "                   AND M_CUST.CUST_CD_S       = '00'                                      " & vbNewLine _
          & "                   AND M_CUST.CUST_CD_SS      = '00'                                      " & vbNewLine _
          & "                   AND M_CUST.SYS_DEL_FLG     = '0'                                       " & vbNewLine _
          & "       -- 運送会社マスタ(運送)                                                            " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_UNSOCO M_UNSOCO                                        " & vbNewLine _
          & "                    ON M_UNSOCO.NRS_BR_CD     = UNSOL.NRS_BR_CD                           " & vbNewLine _
          & "                   AND M_UNSOCO.UNSOCO_CD     = UNSOL.UNSO_CD                             " & vbNewLine _
          & "                   AND M_UNSOCO.UNSOCO_BR_CD  = UNSOL.UNSO_BR_CD                          " & vbNewLine _
          & "       -- 届先マスタ(出荷)                                                                " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_DEST M_DEST                                            " & vbNewLine _
          & "                    ON M_DEST.NRS_BR_CD       = OUTKAL.NRS_BR_CD                          " & vbNewLine _
          & "                   AND M_DEST.CUST_CD_L       = OUTKAL.CUST_CD_L                          " & vbNewLine _
          & "                   AND M_DEST.DEST_CD         = OUTKAL.DEST_CD                            " & vbNewLine _
          & "       -- 区分マスタ<U001> 運送便区分                                                     " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..Z_KBN KBN                                                " & vbNewLine _
          & "                    ON KBN.KBN_GROUP_CD      = 'U001'                                     " & vbNewLine _
          & "                   AND KBN.KBN_CD            = UNSOL.BIN_KB                               " & vbNewLine _
          & "                   AND KBN.SYS_DEL_FLG       = '0'                                        " & vbNewLine _
          & "       -- 帳票パターンマスタ①(EDI出荷の荷主より取得)                                     " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                                    " & vbNewLine _
          & "                    ON M_CUSTRPT1.NRS_BR_CD   = EOUTL.NRS_BR_CD                           " & vbNewLine _
          & "                   AND M_CUSTRPT1.CUST_CD_L   = EOUTL.CUST_CD_L                           " & vbNewLine _
          & "                   AND M_CUSTRPT1.CUST_CD_M   = EOUTL.CUST_CD_M                           " & vbNewLine _
          & "                   AND M_CUSTRPT1.CUST_CD_S   = '00'                                      " & vbNewLine _
          & "                   AND M_CUSTRPT1.PTN_ID      = 'AH'                                      " & vbNewLine _
          & "                   AND M_CUSTRPT1.SYS_DEL_FLG = '0'                                       " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                                               " & vbNewLine _
          & "                    ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD                      " & vbNewLine _
          & "                   AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID                         " & vbNewLine _
          & "                   AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD                         " & vbNewLine _
          & "                   AND MR1.SYS_DEL_FLG        = '0'                                       " & vbNewLine _
          & "       -- 帳票パターンマスタ②(商品マスタより)                                            " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                                    " & vbNewLine _
          & "                    ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD                         " & vbNewLine _
          & "                   AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L                         " & vbNewLine _
          & "                   AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M                         " & vbNewLine _
          & "                   AND M_CUSTRPT2.CUST_CD_S   = '00'                                      " & vbNewLine _
          & "                   AND M_CUSTRPT2.PTN_ID      = 'AH'                                      " & vbNewLine _
          & "                   AND M_CUSTRPT2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                                               " & vbNewLine _
          & "                    ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD                      " & vbNewLine _
          & "                   AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID                         " & vbNewLine _
          & "                   AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD                         " & vbNewLine _
          & "                   AND MR2.SYS_DEL_FLG        = '0'                                       " & vbNewLine _
          & "       -- 帳票パターンマスタ③<存在しない場合の帳票パターン取得>                          " & vbNewLine _
          & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR3                                                " & vbNewLine _
          & "                    ON MR3.NRS_BR_CD          =  EOUTL.NRS_BR_CD                          " & vbNewLine _
          & "                   AND MR3.PTN_ID             = 'AH'                                      " & vbNewLine _
          & "                   AND MR3.STANDARD_FLAG      = '01'                                      " & vbNewLine _
          & "                   AND MR3.SYS_DEL_FLG        = '0'                                       " & vbNewLine

    ''' <summary>                             
    ''' 印刷データ抽出用 GROUP BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = ")QUERRY                                                   " & vbNewLine _
          & " GROUP BY                                                                                " & vbNewLine _
          & "  QUERRY.RPT_ID                                                                          " & vbNewLine _
          & " ,QUERRY.OUTKA_NO_L                                                                      " & vbNewLine _
          & " ,QUERRY.CUST_CD_L                                                                       " & vbNewLine _
          & " ,QUERRY.CUST_CD_M                                                                       " & vbNewLine _
          & " ,QUERRY.CUST_NM_L                                                                       " & vbNewLine _
          & " ,QUERRY.CUST_NM_M                                                                       " & vbNewLine _
          & " ,QUERRY.OUTKA_PLAN_DATE                                                                 " & vbNewLine _
          & " ,QUERRY.ARR_PLAN_DATE                                                                   " & vbNewLine _
          & " ,QUERRY.DEST_CD                                                                         " & vbNewLine _
          & " ,QUERRY.DEST_NM                                                                         " & vbNewLine _
          & " ,QUERRY.DEST_AD_1                                                                       " & vbNewLine _
          & " ,QUERRY.DEST_AD_2                                                                       " & vbNewLine _
          & " ,QUERRY.DEST_AD_3                                                                       " & vbNewLine _
          & " ,QUERRY.UNSO_NM                                                                         " & vbNewLine _
          & " ,QUERRY.BIN_KB                                                                          " & vbNewLine _
          & " ,QUERRY.OUTKA_NO_M                                                                      " & vbNewLine _
          & " ,QUERRY.CUST_GOODS_CD                                                                   " & vbNewLine _
          & " ,QUERRY.GOODS_NM                                                                        " & vbNewLine _
          & " ,QUERRY.NB_UT                                                                           " & vbNewLine _
          & " ,QUERRY.IRIME                                                                           " & vbNewLine _
          & " ,QUERRY.IRIME_UT                                                                        " & vbNewLine _
          & " ,QUERRY.LOT_NO                                                                          " & vbNewLine _
          & " ,QUERRY.CUST_ORD_NO                                                                     " & vbNewLine _
          & " ,QUERRY.OUTKA_STATE_KB                                                                  " & vbNewLine _
          & " ,QUERRY.TOU_NO                                                                          " & vbNewLine _
          & " ,QUERRY.SITU_NO                                                                         " & vbNewLine _
          & " ,QUERRY.ALCTD_NB_S                                                                      " & vbNewLine _
          & " ,QUERRY.ALCTD_NB_M                                                                      " & vbNewLine _
          & " ,QUERRY.OUTKA_TTL_NB                                                                    " & vbNewLine _
          & " ,QUERRY.MATOME_PRINT_DATE                                                               " & vbNewLine _
          & " ,QUERRY.MATOME_PRINT_TIME                                                               " & vbNewLine _
          & ")TTL_QUERRY                                                                              " & vbNewLine _
          & " GROUP BY                                                                                " & vbNewLine _
          & "  TTL_QUERRY.RPT_ID                                                                      " & vbNewLine _
          & " ,TTL_QUERRY.EDI_OUTKA_NO_L                                                              " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_NO_L                                                                  " & vbNewLine _
          & " ,TTL_QUERRY.CUST_CD_L                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.CUST_CD_M                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.CUST_NM_L                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.CUST_NM_M                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_PLAN_DATE                                                             " & vbNewLine _
          & " ,TTL_QUERRY.ARR_PLAN_DATE                                                               " & vbNewLine _
          & " ,TTL_QUERRY.DEST_CD                                                                     " & vbNewLine _
          & " ,TTL_QUERRY.DEST_NM                                                                     " & vbNewLine _
          & " ,TTL_QUERRY.DEST_AD_1                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.DEST_AD_2                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.DEST_AD_3                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.UNSO_NM                                                                     " & vbNewLine _
          & " ,TTL_QUERRY.BIN_KB                                                                      " & vbNewLine _
          & " ,TTL_QUERRY.EDI_OUTKA_NO_M                                                              " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_NO_M                                                                  " & vbNewLine _
          & " ,TTL_QUERRY.CUST_GOODS_CD                                                               " & vbNewLine _
          & " ,TTL_QUERRY.GOODS_NM                                                                    " & vbNewLine _
          & " ,TTL_QUERRY.NB_UT                                                                       " & vbNewLine _
          & " ,TTL_QUERRY.IRIME                                                                       " & vbNewLine _
          & " ,TTL_QUERRY.IRIME_UT                                                                    " & vbNewLine _
          & " ,TTL_QUERRY.LOT_NO                                                                      " & vbNewLine _
          & " ,TTL_QUERRY.CUST_ORD_NO                                                                 " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_STATE_KB                                                              " & vbNewLine _
          & " ,TTL_QUERRY.TOU_NO                                                                      " & vbNewLine _
          & " ,TTL_QUERRY.SITU_NO                                                                     " & vbNewLine _
          & " ,TTL_QUERRY.ALCTD_NB_M                                                                  " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_TTL_NB                                                                " & vbNewLine _
          & " ,TTL_QUERRY.MATOME_PRINT_DATE                                                           " & vbNewLine _
          & " ,TTL_QUERRY.MATOME_PRINT_TIME                                                           " & vbNewLine



    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' Notes993ソート順変更
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                                                 " & vbNewLine _
          & "  TTL_QUERRY.CUST_CD_L                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.CUST_CD_M                                                                   " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_STATE_KB                                                              " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_PLAN_DATE                                                             " & vbNewLine _
          & " ,TTL_QUERRY.OUTKA_NO_L                                                                  " & vbNewLine _
          & " , ISNULL(TTL_QUERRY.OUTKA_NO_M,'999')                                                   " & vbNewLine

    ''' <summary>                             
    ''' 元黒データ抽出用SQL
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_MOTODATA As String = " SELECT                                                                                   " & vbNewLine _
                                           & " OYA_EOUTL.NRS_BR_CD                                     AS NRS_BR_CD                   " & vbNewLine _
                                           & "--,CASE WHEN SUBSTRING(OYA_EOUTL.FREE_C30,1,3) = '04-' THEN SUBSTRING(OYA_EOUTL.FREE_C30,4,9) " & vbNewLine _
                                           & "--      ELSE OYA_EOUTL.EDI_CTL_NO  END                     AS MOTO_KURO_OYA_NO          " & vbNewLine _
                                           & ",SUBSTRING(OYA_EOUTL.FREE_C30,4,9)                       AS MOTO_KURO_OYA_NO            " & vbNewLine _
                                           & ",OYA_EOUTL.EDI_CTL_NO                                    AS MOTO_KURO_TAG_NO            " & vbNewLine _
                                           & " -- 元黒データ(まとめデータ親EDI管理番号抽出)：EDI出荷(大)                              " & vbNewLine _
                                           & "FROM $LM_TRN$..H_OUTKAEDI_L OYA_EOUTL                                                   " & vbNewLine _
                                           & "INNER JOIN                                                                              " & vbNewLine _
                                           & "(SELECT                                                                                 " & vbNewLine _
                                           & "       MOTO_EOUTL.NRS_BR_CD                                                             " & vbNewLine _
                                           & "      ,CASE WHEN QUERY.MOTO_KURO_NO <> '' THEN QUERY.MOTO_KURO_NO                       " & vbNewLine _
                                           & "            ELSE QUERY.CANCEL_NO END                     AS MOTO_KURO_DAIHYO_NO         " & vbNewLine _
                                           & " -- 元黒データ：EDI出荷(大)                                                             " & vbNewLine _
                                           & " FROM $LM_TRN$..H_OUTKAEDI_L MOTO_EOUTL                                                 " & vbNewLine _
                                           & " INNER JOIN                                                                             " & vbNewLine _
                                           & " (SELECT                                                                                " & vbNewLine _
                                           & "        CANCEL_EOUTL.NRS_BR_CD                           AS NRS_BR_CD                   " & vbNewLine _
                                           & "       ,CANCEL_EOUTL.FREE_C18                            AS MOTO_KURO_NO                " & vbNewLine _
                                           & "       ,CANCEL_EOUTL.EDI_CTL_NO                          AS CANCEL_NO                   " & vbNewLine _
                                           & "       ,CANCEL_EOUTL.CUST_CD_L                           AS CUST_CD_L                   " & vbNewLine _
                                           & "       ,CANCEL_EOUTL.CUST_CD_M                           AS CUST_CD_M                   " & vbNewLine _
                                           & "       ,CANCEL_EOUTL.EDI_CTL_NO                          AS EDI_CTL_NO                  " & vbNewLine _
                                           & " FROM                                                                                   " & vbNewLine _
                                           & " -- キャンセルデータ：EDI出荷(大)                                                       " & vbNewLine _
                                           & "      $LM_TRN$..H_OUTKAEDI_L CANCEL_EOUTL                                               " & vbNewLine _
                                           & " WHERE                                                                                  " & vbNewLine _
                                           & " CANCEL_EOUTL.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                           & " AND CANCEL_EOUTL.CUST_CD_L = @CUST_CD_L                                                " & vbNewLine _
                                           & " AND CANCEL_EOUTL.CUST_CD_M = @CUST_CD_M                                                " & vbNewLine _
                                           & " AND CANCEL_EOUTL.EDI_CTL_NO = @EDI_CTL_NO) QUERY                                       " & vbNewLine _
                                           & " ON                                                                                     " & vbNewLine _
                                           & " MOTO_EOUTL.NRS_BR_CD = QUERY.NRS_BR_CD                                                 " & vbNewLine _
                                           & " AND                                                                                    " & vbNewLine _
                                           & " MOTO_EOUTL.EDI_CTL_NO = CASE WHEN QUERY.MOTO_KURO_NO <> '' THEN QUERY.MOTO_KURO_NO     " & vbNewLine _
                                           & "                              ELSE QUERY.CANCEL_NO END                                  " & vbNewLine _
                                           & " GROUP BY                                                                               " & vbNewLine _
                                           & " MOTO_EOUTL.NRS_BR_CD                                                                   " & vbNewLine _
                                           & ",CASE WHEN QUERY.MOTO_KURO_NO <> '' THEN QUERY.MOTO_KURO_NO                             " & vbNewLine _
                                           & "      ELSE QUERY.CANCEL_NO END                                                          " & vbNewLine _
                                           & ") QUERY2                                                                                " & vbNewLine _
                                           & " ON                                                                                     " & vbNewLine _
                                           & " OYA_EOUTL.NRS_BR_CD = QUERY2.NRS_BR_CD                                                 " & vbNewLine _
                                           & " AND                                                                                    " & vbNewLine _
                                           & " OYA_EOUTL.EDI_CTL_NO = QUERY2.MOTO_KURO_DAIHYO_NO                                      " & vbNewLine

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
    ''' EDI出荷(大)元黒データ検索(まとめデータの場合は元黒の親データ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)元黒対象データ取得SQLの構築・発行</remarks>
    Private Function SelectEdiMotokuroNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH540DAC.SQL_MOTODATA)      'SQL構築(印刷データ抽出用 SELECT/FROM/GROUP BY句)
        Call Me.SetCanselPrameter(Me._Row)                    '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH540DAC", "SelectEdiMotokuroNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then
            'inTbl.Rows(0)("EDI_CTL_NO") = reader("MOTO_KURO_OYA_NO").ToString().Trim()
            inTbl.Rows(0)("MOTO_KURO_OYA_NO") = reader("MOTO_KURO_OYA_NO").ToString().Trim()
            inTbl.Rows(0)("EDI_CTL_NO") = reader("MOTO_KURO_TAG_NO").ToString().Trim()
            inTbl.Rows(0)("MOTO_KURO_TAG_NO") = reader("MOTO_KURO_TAG_NO").ToString().Trim()
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrintPattern(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '帳票パターン取得の際は、出荷は見ない
        Dim outkaFlg As Boolean = False

        'SQL作成
        Me._StrSql.Append(LMH540DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH540DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        'Me._StrSql.Append(LMH540DAC.SQL_MPrt_WHERE)     'SQL構築(帳票種別用WHERE句)
        Call Me.SetConditionPrintPatternMSQL()          '条件設定
        Call Me.SetConditionMasterSQL(outkaFlg)                 'SQL構築(印刷データ抽出条件設定)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH540DAC", "SelectMPrt", cmd)

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
    ''' EDI出荷(大)・EDI出荷(中)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)・EDI出荷(中)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'EDI出荷取消チェックリストの際は、出荷を見る
        Dim outkaFlg As Boolean = True

        'SQL作成
        Me._StrSql.Append(LMH540DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH540DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句前半)
        Call Me.SetConditionMasterSQL_OUTKAPLANDATE() 'SQL構築(印刷データ抽出条件設定)
        Me._StrSql.Append(LMH540DAC.SQL_FROM_AFTER)  'SQL構築(印刷データ抽出用 FROM句後半)
        Call Me.SetConditionPrintPatternMSQL()       '条件設定
        Call Me.SetConditionMasterSQL(outkaFlg)      'SQL構築(印刷データ抽出条件設定)
        Me._StrSql.Append(LMH540DAC.SQL_GROUP_BY)    'SQL構築(印刷データ抽出用 GROUP BY句)
        Me._StrSql.Append(LMH540DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH540DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング 
        map.Add("RPT_ID", "RPT_ID")
        map.Add("EDI_OUTKA_NO_L", "EDI_OUTKA_NO_L")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("EDI_OUTKA_NO_M", "EDI_OUTKA_NO_M")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("NB_UT", "NB_UT")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ALCTD_NB_S", "ALCTD_NB_S")
        map.Add("ALCTD_NB_M", "ALCTD_NB_M")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("MATOME_PRINT_DATE", "MATOME_PRINT_DATE")
        map.Add("MATOME_PRINT_TIME", "MATOME_PRINT_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH540OUT")

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
            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))

            '出荷予定日(FROM)
            whereStr = .Item("SEARCH_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                whereStr = Mid(whereStr, 1, 4) & "/" & Mid(whereStr, 5, 2) & "/" & Mid(whereStr, 7, 2)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '出荷予定日(TO)
            whereStr = .Item("SEARCH_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                whereStr = Mid(whereStr, 1, 4) & "/" & Mid(whereStr, 5, 2) & "/" & Mid(whereStr, 7, 2)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.CHAR))
            End If
        End With
    End Sub


    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal outkaFlg As Boolean)

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '====== 画面項目 ======'

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" EOUTL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If


            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            'EDI出荷管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.EDI_CTL_NO = @EDI_CTL_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If


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

            ''出荷予定日(FROM)
            'whereStr = .Item("SEARCH_DATE_FROM").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND EOUTL.OUTKA_PLAN_DATE >= @SEARCH_DATE_FROM ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.CHAR))
            'End If

            ''出荷予定日(TO)
            'whereStr = .Item("SEARCH_DATE_TO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND EOUTL.OUTKA_PLAN_DATE <= @SEARCH_DATE_TO ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.CHAR))
            'End If

            '====== 出荷データのみ抽出 ======'
            'EDI出荷取消チェックリストの場合、条件に追加
            If outkaFlg = True Then
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.OUTKA_NO_L IS NOT NULL")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_OUTKAPLANDATE()

        Dim whereStr As String = String.Empty

        With Me._Row
            '====== 画面項目 ======'

            '出荷予定日(FROM)
            whereStr = .Item("SEARCH_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("                                 AND aka.SHUKKA_YMD >= @SEARCH_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
            End If

            '出荷予定日(TO)
            whereStr = .Item("SEARCH_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("                                 AND aka.SHUKKA_YMD <= @SEARCH_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    ''' <summary>
    ''' キャンセルデータパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCanselPrameter(ByVal row As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", row.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", row.Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", row.Item("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", row.Item("EDI_CTL_NO"), DBDataType.CHAR))

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
