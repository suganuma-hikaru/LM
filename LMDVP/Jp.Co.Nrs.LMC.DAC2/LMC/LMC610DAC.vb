' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC610    : 出荷報告書
'  作  成  者       :  [KIM]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC610DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC610DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' 検索パターン
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SelectCondition As Integer
        PTN1  '帳票ID取得
        PTN2  'データ検索
    End Enum

    ''' <summary>
    ''' WHERE句設定パターン
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum WhereSelectCondition As Integer
        WPTN1  'データ検索(LMC610)
        WPTN2  'データ検索(LMC611)
    End Enum


    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMC610DAC"

    ''' <summary>
    ''' 帳票パターン取得テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M_RPT As String = "M_RPT"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMC610IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMC610OUT"

    ''' <summary>
    ''' 帳票ID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PTN_ID_LMC610 As String = "12"
    Private Const PTN_ID_LMC611 As String = "13"
    Private Const PTN_ID_LMC612 As String = "78"    '2012.02.28 大阪対応ADD

#End Region '制御用

#Region "SQL"

#Region "帳票ID"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	 @NRS_BR_CD                                          AS NRS_BR_CD    " & vbNewLine _
                                            & "	,@PTN_ID                                             AS PTN_ID       " & vbNewLine _
                                            & "	,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                    " & vbNewLine _
                                            & "	      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                    " & vbNewLine _
                                            & "	      ELSE MR3.PTN_CD END                            AS PTN_CD       " & vbNewLine _
                                            & "	,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                    " & vbNewLine _
                                            & "	      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                            & "	      ELSE MR3.RPT_ID END                            AS RPT_ID       " & vbNewLine

#End Region '帳票ID

#Region "SQL_FROM句"

    ''' <summary>
    ''' 共通FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ALL_FROM As String = "  FROM                                                                                       " & vbNewLine _
                                         & "  --出荷                                                                                     " & vbNewLine _
                                         & "  (                                                                                          " & vbNewLine _
                                         & "    SELECT                                                                                   " & vbNewLine _
                                         & "      OUTKA.OUTKA_NO_L                                                    AS OUTKA_NO_L      " & vbNewLine _
                                         & "    , OUTKA.LOT_NO                                                        AS LOT_NO          " & vbNewLine _
                                         & "    , OUTKA.IRIME                                                         AS IRIME           " & vbNewLine _
                                         & "    , OUTKA.IRIME_UT                                                      AS IRIME_UT        " & vbNewLine _
                                         & "    , OUTKA.REMARK                                                        AS REMARK          " & vbNewLine _
                                         & "    , OUTKA.SERIAL_NO                                                     AS SERIAL_NO       " & vbNewLine _
                                         & "    , OUTKA.GOODS_CD_NRS                                                  AS GOODS_CD_NRS    " & vbNewLine _
                                         & "    , SUM(OUTKA.ALCTD_NB)                                                 AS ALCTD_NB        " & vbNewLine _
                                         & "    , SUM(OUTKA.ALCTD_QT)                                                 AS ALCTD_QT        " & vbNewLine _
                                         & "    , SUM(OUTKA.ALLOC_CAN_QT)                                             AS ALLOC_CAN_QT    " & vbNewLine _
                                         & "    , OUTKA.NRS_BR_CD                                                     AS NRS_BR_CD       " & vbNewLine _
                                         & "    , OUTKA.WH_CD                                                         AS WH_CD           " & vbNewLine _
                                         & "    , OUTKA.OUTKA_DATE                                                    AS OUTKA_DATE      " & vbNewLine _
                                         & "    , OUTKA.CUST_ORD_NO                                                   AS CUST_ORD_NO     " & vbNewLine _
                                         & "    , OUTKA.CUST_CD_L                                                     AS CUST_CD_L       " & vbNewLine _
                                         & "    , OUTKA.CUST_CD_M                                                     AS CUST_CD_M       " & vbNewLine _
                                         & "    , OUTKA.ARR_PLAN_DATE                                                 AS ARR_PLAN_DATE   " & vbNewLine _
                                         & "    , OUTKA.DEST_CD                                                       AS DEST_CD         " & vbNewLine _
                                         & "    , OUTKA.DEST_KB          AS DEST_KB                                          " & vbNewLine _
                                         & "    , OUTKA.DEST_NM          AS DEST_NM                                          " & vbNewLine _
                                         & "    , OUTKA.DEST_AD_1        AS DEST_AD_1                                        " & vbNewLine _
                                         & "    , OUTKA.DEST_AD_2        AS DEST_AD_2                                        " & vbNewLine _
                                         & "    , OUTKA.KBN_NM2          AS KBN_NM2                                          " & vbNewLine _
                                         & "    FROM                                                                                     " & vbNewLine _
                                         & "       (                                                                                     " & vbNewLine _
                                         & "        SELECT                                                                               " & vbNewLine _
                                         & "            OUTKAL.OUTKA_NO_L                                           AS OUTKA_NO_L        " & vbNewLine _
                                         & "--          , OUTKAM.OUTKA_NO_M                                           AS OUTKA_NO_M        " & vbNewLine _
                                         & "          , OUTKAS.LOT_NO                                               AS LOT_NO            " & vbNewLine _
                                         & "          , OUTKAS.IRIME                                                AS IRIME             " & vbNewLine _
                                         & "          , OUTKAM.IRIME_UT                                             AS IRIME_UT          " & vbNewLine _
                                         & "          , OUTKAM.REMARK                                               AS REMARK            " & vbNewLine _
                                         & "          , OUTKAM.SERIAL_NO                                            AS SERIAL_NO         " & vbNewLine _
                                         & "          , OUTKAM.GOODS_CD_NRS                                         AS GOODS_CD_NRS      " & vbNewLine _
                                         & "          , SUM(OUTKAS.ALCTD_NB)                                        AS ALCTD_NB          " & vbNewLine _
                                         & "          , SUM(OUTKAS.ALCTD_QT)                                        AS ALCTD_QT          " & vbNewLine _
                                         & "          , OUTKAS.ZAI_REC_NO                                           AS ZAI_REC_NO        " & vbNewLine _
                                         & "          , MAX(ZAITRS.ALLOC_CAN_QT)                                    AS ALLOC_CAN_QT      " & vbNewLine _
                                         & "          , OUTKAL.NRS_BR_CD                                            AS NRS_BR_CD         " & vbNewLine _
                                         & "          , OUTKAL.WH_CD                                                AS WH_CD        " & vbNewLine _
                                         & "          , OUTKAL.OUTKA_DATE                                      AS OUTKA_DATE        " & vbNewLine _
                                         & "          , OUTKAL.CUST_ORD_NO                                          AS CUST_ORD_NO       " & vbNewLine _
                                         & "          , OUTKAL.CUST_CD_L                                            AS CUST_CD_L         " & vbNewLine _
                                         & "          , OUTKAL.CUST_CD_M                                            AS CUST_CD_M         " & vbNewLine _
                                         & "          , OUTKAL.ARR_PLAN_DATE                                        AS ARR_PLAN_DATE     " & vbNewLine _
                                         & "          , OUTKAL.DEST_CD                                              AS DEST_CD           " & vbNewLine _
                                         & "          , OUTKAL.DEST_KB          AS DEST_KB                                         " & vbNewLine _
                                         & "          , OUTKAL.DEST_NM          AS DEST_NM                                         " & vbNewLine _
                                         & "          , OUTKAL.DEST_AD_1        AS DEST_AD_1                                       " & vbNewLine _
                                         & "          , OUTKAL.DEST_AD_2        AS DEST_AD_2                                       " & vbNewLine _
                                         & "          , ZKBN.KBN_NM2            AS KBN_NM2                                         " & vbNewLine _
                                         & "        FROM                                                                                 " & vbNewLine _
                                         & "        --出荷L                                                                              " & vbNewLine _
                                         & "              (                                                                              " & vbNewLine _
                                         & "                SELECT                                                                       " & vbNewLine _
                                         & "                    NRS_BR_CD        AS NRS_BR_CD                                            " & vbNewLine _
                                         & "                  , WH_CD            AS WH_CD                                                " & vbNewLine _
                                         & "                  , OUTKA_PLAN_DATE       AS OUTKA_DATE                                           " & vbNewLine _
                                         & "                  , CUST_ORD_NO      AS CUST_ORD_NO                                          " & vbNewLine _
                                         & "                  , CUST_CD_L        AS CUST_CD_L                                            " & vbNewLine _
                                         & "                  , CUST_CD_M        AS CUST_CD_M                                            " & vbNewLine _
                                         & "                  , OUTKA_NO_L       AS OUTKA_NO_L                                           " & vbNewLine _
                                         & "                  , ARR_PLAN_DATE    AS ARR_PLAN_DATE                                        " & vbNewLine _
                                         & "                  , DEST_CD          AS DEST_CD                                              " & vbNewLine _
                                         & "                  , DEST_KB          AS DEST_KB                                                " & vbNewLine _
                                         & "                  , DEST_NM          AS DEST_NM                                                " & vbNewLine _
                                         & "                  , DEST_AD_1        AS DEST_AD_1                                              " & vbNewLine _
                                         & "                  , DEST_AD_2        AS DEST_AD_2                                              " & vbNewLine _
                                         & "                  , SYS_DEL_FLG      AS SYS_DEL_FLG                                          " & vbNewLine _
                                         & "               FROM                                                                          " & vbNewLine _
                                         & "                   $LM_TRN$..C_OUTKA_L                                                         " & vbNewLine _
                                         & "               --商品コード以外の抽出条件                                                    " & vbNewLine _
                                         & "               $WHERE1$                                                                      " & vbNewLine _
                                         & "               --ここまで                                                                    " & vbNewLine _
                                         & "               )             OUTKAL                                                          " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "            $LM_TRN$..C_OUTKA_M AS OUTKAM                                                      " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "           OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "           OUTKAM.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "           OUTKAL.OUTKA_NO_L   = OUTKAM.OUTKA_NO_L                                           " & vbNewLine _
                                         & "          --商品ｺｰﾄﾞ条件はここ                                                               " & vbNewLine _
                                         & "          $WHERE2$                                                                           " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "            $LM_TRN$..C_OUTKA_S AS OUTKAS                                                      " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "            OUTKAS.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "            OUTKAM.OUTKA_NO_L   = OUTKAS.OUTKA_NO_L                                          " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "            OUTKAM.OUTKA_NO_M   = OUTKAS.OUTKA_NO_M                                          " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "            OUTKAS.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "           $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                       " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "           ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                             " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZAITRS.CUST_CD_L = OUTKAl.CUST_CD_L                                               " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZAITRS.CUST_CD_M = OUTKAL.CUST_CD_M                                               " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "           $LM_MST$..M_GOODS AS GOODS                                                        " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "           GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                                " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                          " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "           $LM_MST$..Z_KBN AS ZKBN                                                           " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "           GOODS.DOKU_KB = ZKBN.KBN_CD                                                       " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZKBN.KBN_GROUP_CD = 'G001'                                                        " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZKBN.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                         & "          WHERE                                                                              " & vbNewLine _
                                         & "           OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           OUTKAM.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                         & "          $WHERE3$                                                                           " & vbNewLine _
                                         & "         GROUP BY                                                                            " & vbNewLine _
                                         & "           OUTKAL.OUTKA_NO_L                                                                 " & vbNewLine _
                                         & "--         , OUTKAM.OUTKA_NO_M                                                                 " & vbNewLine _
                                         & "         , OUTKAS.ZAI_REC_NO                                                                 " & vbNewLine _
                                         & "         , OUTKAS.LOT_NO                                                                     " & vbNewLine _
                                         & "         , OUTKAS.IRIME                                                                      " & vbNewLine _
                                         & "         , OUTKAM.IRIME_UT                                                                   " & vbNewLine _
                                         & "         , OUTKAM.REMARK                                                                     " & vbNewLine _
                                         & "         , OUTKAM.SERIAL_NO                                                                  " & vbNewLine _
                                         & "         , OUTKAM.GOODS_CD_NRS                                                               " & vbNewLine _
                                         & "         , OUTKAL.NRS_BR_CD                                                                  " & vbNewLine _
                                         & "         , OUTKAL.WH_CD                                                                      " & vbNewLine _
                                         & "         , OUTKAL.OUTKA_DATE                                                                 " & vbNewLine _
                                         & "         , OUTKAL.CUST_ORD_NO                                                                " & vbNewLine _
                                         & "         , OUTKAL.CUST_CD_L                                                                  " & vbNewLine _
                                         & "         , OUTKAL.CUST_CD_M                                                                  " & vbNewLine _
                                         & "         , OUTKAL.ARR_PLAN_DATE                                                              " & vbNewLine _
                                         & "         , OUTKAL.DEST_CD                                                                    " & vbNewLine _
                                         & "         , OUTKAL.DEST_KB                                                             " & vbNewLine _
                                         & "         , OUTKAL.DEST_NM                                                             " & vbNewLine _
                                         & "         , OUTKAL.DEST_AD_1                                                           " & vbNewLine _
                                         & "         , OUTKAL.DEST_AD_2                                                           " & vbNewLine _
                                         & "         , ZKBN.KBN_NM2                                                               " & vbNewLine _
                                         & "       ) OUTKA                                                                               " & vbNewLine _
                                         & "    GROUP BY                                                                                 " & vbNewLine _
                                         & "       OUTKA.OUTKA_NO_L                                                                      " & vbNewLine _
                                         & "     , OUTKA.GOODS_CD_NRS                                                                    " & vbNewLine _
                                         & "     , OUTKA.LOT_NO                                                                          " & vbNewLine _
                                         & "     , OUTKA.IRIME                                                                           " & vbNewLine _
                                         & "     , OUTKA.IRIME_UT                                                                        " & vbNewLine _
                                         & "     , OUTKA.REMARK                                                                          " & vbNewLine _
                                         & "     , OUTKA.SERIAL_NO                                                                       " & vbNewLine _
                                         & "     , OUTKA.NRS_BR_CD                                                                       " & vbNewLine _
                                         & "     , OUTKA.WH_CD                                                                           " & vbNewLine _
                                         & "     , OUTKA.OUTKA_DATE                                                                      " & vbNewLine _
                                         & "     , OUTKA.CUST_ORD_NO                                                                     " & vbNewLine _
                                         & "     , OUTKA.CUST_CD_L                                                                       " & vbNewLine _
                                         & "     , OUTKA.CUST_CD_M                                                                       " & vbNewLine _
                                         & "     , OUTKA.ARR_PLAN_DATE                                                                   " & vbNewLine _
                                         & "     , OUTKA.DEST_CD                                                                         " & vbNewLine _
                                         & "     , OUTKA.DEST_KB                                                              " & vbNewLine _
                                         & "     , OUTKA.DEST_NM                                                              " & vbNewLine _
                                         & "     , OUTKA.DEST_AD_1                                                            " & vbNewLine _
                                         & "     , OUTKA.DEST_AD_2                                                            " & vbNewLine _
                                         & "     , OUTKA.KBN_NM2                                                              " & vbNewLine _
                                         & "  ) MAIN                                                                                     " & vbNewLine _
                                         & "  --商品ﾏｽﾀ(出荷)                                                                            " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_GOODS M_GOODS                                                                   " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   M_GOODS.NRS_BR_CD   = MAIN.NRS_BR_CD                                                          " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MAIN.GOODS_CD_NRS   = M_GOODS.GOODS_CD_NRS                                                " & vbNewLine _
                                         & "  --出荷Lでの荷主帳票ﾊﾟﾀｰﾝ取得                                                               " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_CUST_RPT MCR1                                                                   " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MCR1.NRS_BR_CD = MAIN.NRS_BR_CD                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR1.CUST_CD_L = MAIN.CUST_CD_L                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR1.CUST_CD_M = MAIN.CUST_CD_M                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR1.CUST_CD_S = '00'                                                                     " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR1.PTN_ID    = @PTN_ID                                                                     " & vbNewLine _
                                         & "  --帳票ﾊﾟﾀｰﾝ取得                                                                            " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_RPT MR1                                                                         " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MR1.NRS_BR_CD = MAIN.NRS_BR_CD                                                                " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR1.PTN_ID    = MCR1.PTN_ID                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR1.PTN_CD    = MCR1.PTN_CD                                                               " & vbNewLine _
                                         & "  AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                         & "  --商品Mの荷主での荷主帳票ﾊﾟﾀｰﾝ取得                                                         " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_CUST_RPT MCR2                                                                   " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MCR2.NRS_BR_CD  = MAIN.NRS_BR_CD                                                              " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   M_GOODS.CUST_CD_L = MCR2.CUST_CD_L                                                        " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   M_GOODS.CUST_CD_M = MCR2.CUST_CD_M                                                        " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   M_GOODS.CUST_CD_S = MCR2.CUST_CD_S                                                        " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR2.PTN_ID           = @PTN_ID                                                             " & vbNewLine _
                                         & "  --帳票ﾊﾟﾀｰﾝ取得                                                                            " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_RPT MR2                                                                         " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MR2.NRS_BR_CD = MAIN.NRS_BR_CD                                                                " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR2.PTN_ID    = MCR2.PTN_ID                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR2.PTN_CD    = MCR2.PTN_CD                                                               " & vbNewLine _
                                         & "  AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                         & "  --存在しない場合の帳票ﾊﾟﾀｰﾝ取得                                                            " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_RPT MR3                                                                         " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MR3.NRS_BR_CD     = MAIN.NRS_BR_CD                                                            " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR3.PTN_ID        = @PTN_ID                                                                  " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR3.STANDARD_FLAG = '01'                                                                  " & vbNewLine _
                                         & "   AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine

    Private Const SQL_CUST_S_FROM As String = " FROM                                                                                     " & vbNewLine _
                                            & "      --出荷                                                                              " & vbNewLine _
                                            & "      (                                                                                   " & vbNewLine _
                                            & "        SELECT                                                                            " & vbNewLine _
                                            & "               OUTKA.OUTKA_NO_L         AS OUTKA_NO_L                                     " & vbNewLine _
                                            & "             , OUTKA.LOT_NO             AS LOT_NO                                         " & vbNewLine _
                                            & "             , OUTKA.IRIME              AS IRIME                                          " & vbNewLine _
                                            & "             , OUTKA.IRIME_UT           AS IRIME_UT                                       " & vbNewLine _
                                            & "             , OUTKA.REMARK             AS REMARK                                         " & vbNewLine _
                                            & "             , OUTKA.SERIAL_NO          AS SERIAL_NO                                      " & vbNewLine _
                                            & "             , OUTKA.GOODS_CD_NRS       AS GOODS_CD_NRS                                   " & vbNewLine _
                                            & "             , SUM(OUTKA.ALCTD_NB)      AS ALCTD_NB                                       " & vbNewLine _
                                            & "             , SUM(OUTKA.ALCTD_QT)      AS ALCTD_QT                                       " & vbNewLine _
                                            & "             , SUM(OUTKA.ALLOC_CAN_QT)  AS ALLOC_CAN_QT                                   " & vbNewLine _
                                            & "             , OUTKA.NRS_BR_CD          AS NRS_BR_CD                                      " & vbNewLine _
                                            & "             , OUTKA.WH_CD              AS WH_CD                                          " & vbNewLine _
                                            & "             , OUTKA.OUTKA_DATE         AS OUTKA_DATE                                     " & vbNewLine _
                                            & "             , OUTKA.CUST_ORD_NO        AS CUST_ORD_NO                                    " & vbNewLine _
                                            & "             , OUTKA.CUST_CD_L          AS CUST_CD_L                                      " & vbNewLine _
                                            & "             , OUTKA.CUST_CD_M          AS CUST_CD_M                                      " & vbNewLine _
                                            & "             , OUTKA.ARR_PLAN_DATE      AS ARR_PLAN_DATE                                  " & vbNewLine _
                                            & "             , OUTKA.DEST_CD            AS DEST_CD                                        " & vbNewLine _
                                            & "             , OUTKA.DEST_KB            AS DEST_KB                                        " & vbNewLine _
                                            & "             , OUTKA.DEST_NM            AS DEST_NM                                        " & vbNewLine _
                                            & "             , OUTKA.DEST_AD_1          AS DEST_AD_1                                      " & vbNewLine _
                                            & "             , OUTKA.DEST_AD_2          AS DEST_AD_2                                      " & vbNewLine _
                                            & "             , OUTKA.KBN_NM2            AS KBN_NM2                                        " & vbNewLine _
                                            & "          FROM (                                                                          " & vbNewLine _
                                            & "                 SELECT                                                                   " & vbNewLine _
                                            & "                        OUTKAL.OUTKA_NO_L        AS OUTKA_NO_L                            " & vbNewLine _
                                            & "                      , OUTKAM.OUTKA_NO_M        AS OUTKA_NO_M                            " & vbNewLine _
                                            & "                      , OUTKAS.LOT_NO            AS LOT_NO                                " & vbNewLine _
                                            & "                      , OUTKAS.IRIME             AS IRIME                                 " & vbNewLine _
                                            & "                      , OUTKAM.IRIME_UT          AS IRIME_UT                              " & vbNewLine _
                                            & "                      , OUTKAM.REMARK            AS REMARK                                " & vbNewLine _
                                            & "                      , OUTKAM.SERIAL_NO         AS SERIAL_NO                             " & vbNewLine _
                                            & "                      , OUTKAM.GOODS_CD_NRS      AS GOODS_CD_NRS                          " & vbNewLine _
                                            & "                      , SUM(OUTKAS.ALCTD_NB)     AS ALCTD_NB                              " & vbNewLine _
                                            & "                      , SUM(OUTKAS.ALCTD_QT)     AS ALCTD_QT                              " & vbNewLine _
                                            & "                      , OUTKAS.ZAI_REC_NO        AS ZAI_REC_NO                            " & vbNewLine _
                                            & "                      , MAX(ZAITRS.ALLOC_CAN_QT) AS ALLOC_CAN_QT                          " & vbNewLine _
                                            & "                      , OUTKAL.NRS_BR_CD         AS NRS_BR_CD                             " & vbNewLine _
                                            & "                      , OUTKAL.WH_CD             AS WH_CD                                 " & vbNewLine _
                                            & "                      , OUTKAL.OUTKA_DATE        AS OUTKA_DATE                            " & vbNewLine _
                                            & "                      , OUTKAL.CUST_ORD_NO       AS CUST_ORD_NO                           " & vbNewLine _
                                            & "                      , OUTKAL.CUST_CD_L         AS CUST_CD_L                             " & vbNewLine _
                                            & "                      , OUTKAL.CUST_CD_M         AS CUST_CD_M                             " & vbNewLine _
                                            & "                      , OUTKAL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                         " & vbNewLine _
                                            & "                      , OUTKAL.DEST_CD           AS DEST_CD                               " & vbNewLine _
                                            & "                      , OUTKAL.DEST_KB           AS DEST_KB                               " & vbNewLine _
                                            & "                      , OUTKAL.DEST_NM           AS DEST_NM                               " & vbNewLine _
                                            & "                      , OUTKAL.DEST_AD_1         AS DEST_AD_1                             " & vbNewLine _
                                            & "                      , OUTKAL.DEST_AD_2         AS DEST_AD_2                             " & vbNewLine _
                                            & "                      , ZKBN.KBN_NM2             AS KBN_NM2                               " & vbNewLine _
                                            & "                   FROM                                                                   " & vbNewLine _
                                            & "                        --出荷L                                                           " & vbNewLine _
                                            & "                        (                                                                 " & vbNewLine _
                                            & "                         SELECT                                                           " & vbNewLine _
                                            & "                                NRS_BR_CD           AS NRS_BR_CD                          " & vbNewLine _
                                            & "                              , WH_CD               AS WH_CD                              " & vbNewLine _
                                            & "                              , OUTKA_PLAN_DATE     AS OUTKA_DATE                         " & vbNewLine _
                                            & "                              , CUST_ORD_NO         AS CUST_ORD_NO                        " & vbNewLine _
                                            & "                              , CUST_CD_L           AS CUST_CD_L                          " & vbNewLine _
                                            & "                              , CUST_CD_M           AS CUST_CD_M                          " & vbNewLine _
                                            & "                              , OUTKA_NO_L          AS OUTKA_NO_L                         " & vbNewLine _
                                            & "                              , ARR_PLAN_DATE       AS ARR_PLAN_DATE                      " & vbNewLine _
                                            & "                              , DEST_CD             AS DEST_CD                            " & vbNewLine _
                                            & "                              , DEST_KB             AS DEST_KB                            " & vbNewLine _
                                            & "                              , DEST_NM             AS DEST_NM                            " & vbNewLine _
                                            & "                              , DEST_AD_1           AS DEST_AD_1                          " & vbNewLine _
                                            & "                              , DEST_AD_2           AS DEST_AD_2                          " & vbNewLine _
                                            & "                              , SYS_DEL_FLG         AS SYS_DEL_FLG                        " & vbNewLine _
                                            & "                           FROM $LM_TRN$..C_OUTKA_L                                       " & vbNewLine _
                                            & "                                --商品コード以外の抽出条件                                " & vbNewLine _
                                            & "                                $WHERE1$                                                  " & vbNewLine _
                                            & "                                --ここまで                                                " & vbNewLine _
                                            & "                         ) OUTKAL                                                         " & vbNewLine _
                                            & "                                LEFT JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM                   " & vbNewLine _
                                            & "                                       ON OUTKAM.NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
                                            & "                                      AND OUTKAM.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                            & "                                      AND OUTKAL.OUTKA_NO_L   = OUTKAM.OUTKA_NO_L         " & vbNewLine _
                                            & "                                --商品ｺｰﾄﾞ条件はここ                                      " & vbNewLine _
                                            & "                                $WHERE2$                                                  " & vbNewLine _
                                            & "                                LEFT JOIN $LM_TRN$..C_OUTKA_S AS OUTKAS                   " & vbNewLine _
                                            & "                                       ON OUTKAS.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                            & "                                      AND OUTKAM.OUTKA_NO_L  = OUTKAS.OUTKA_NO_L          " & vbNewLine _
                                            & "                                      AND OUTKAM.OUTKA_NO_M  = OUTKAS.OUTKA_NO_M          " & vbNewLine _
                                            & "                                      AND OUTKAS.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                            & "                                LEFT JOIN $LM_TRN$..D_ZAI_TRS AS ZAITRS                   " & vbNewLine _
                                            & "                                       ON ZAITRS.NRS_BR_CD  = @NRS_BR_CD                  " & vbNewLine _
                                            & "                                      AND ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO           " & vbNewLine _
                                            & "                                      AND ZAITRS.CUST_CD_L  = OUTKAl.CUST_CD_L            " & vbNewLine _
                                            & "                                      AND ZAITRS.CUST_CD_M  = OUTKAL.CUST_CD_M            " & vbNewLine _
                                            & "                                LEFT JOIN $LM_MST$..M_GOODS AS GOODS                      " & vbNewLine _
                                            & "                                       ON GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD              " & vbNewLine _
                                            & "                                      AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS        " & vbNewLine _
                                            & "                                LEFT JOIN $LM_MST$..Z_KBN AS ZKBN                         " & vbNewLine _
                                            & "                                       ON GOODS.DOKU_KB = ZKBN.KBN_CD                     " & vbNewLine _
                                            & "                                      AND ZKBN.KBN_GROUP_CD = 'G001'                      " & vbNewLine _
                                            & "                                      AND ZKBN.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                            & "                  WHERE OUTKAM.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                            & "                    AND OUTKAM.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                            & "                    AND GOODS.CUST_CD_L = @CUST_CD_L                                      " & vbNewLine _
                                            & "                    AND GOODS.CUST_CD_M = @CUST_CD_M                                      " & vbNewLine _
                                            & "                    AND GOODS.CUST_CD_S = @CUST_CD_S                                      " & vbNewLine _
                                            & "                  $WHERE3$                                                                " & vbNewLine _
                                            & "                  GROUP BY                                                                " & vbNewLine _
                                            & "                        OUTKAL.OUTKA_NO_L                                                 " & vbNewLine _
                                            & "                      , OUTKAM.OUTKA_NO_M                                                 " & vbNewLine _
                                            & "                      , OUTKAS.ZAI_REC_NO                                                 " & vbNewLine _
                                            & "                      , OUTKAS.LOT_NO                                                     " & vbNewLine _
                                            & "                      , OUTKAS.IRIME                                                      " & vbNewLine _
                                            & "                      , OUTKAM.IRIME_UT                                                   " & vbNewLine _
                                            & "                      , OUTKAM.REMARK                                                     " & vbNewLine _
                                            & "                      , OUTKAM.SERIAL_NO                                                  " & vbNewLine _
                                            & "                      , OUTKAM.GOODS_CD_NRS                                               " & vbNewLine _
                                            & "                      , OUTKAL.NRS_BR_CD                                                  " & vbNewLine _
                                            & "                      , OUTKAL.WH_CD                                                      " & vbNewLine _
                                            & "                      , OUTKAL.OUTKA_DATE                                                 " & vbNewLine _
                                            & "                      , OUTKAL.CUST_ORD_NO                                                " & vbNewLine _
                                            & "                      , OUTKAL.CUST_CD_L                                                  " & vbNewLine _
                                            & "                      , OUTKAL.CUST_CD_M                                                  " & vbNewLine _
                                            & "                      , OUTKAL.ARR_PLAN_DATE                                              " & vbNewLine _
                                            & "                      , OUTKAL.DEST_CD                                                    " & vbNewLine _
                                            & "                      , OUTKAL.DEST_KB                                                    " & vbNewLine _
                                            & "                      , OUTKAL.DEST_NM                                                    " & vbNewLine _
                                            & "                      , OUTKAL.DEST_AD_1                                                  " & vbNewLine _
                                            & "                      , OUTKAL.DEST_AD_2                                                  " & vbNewLine _
                                            & "                      , ZKBN.KBN_NM2                                                      " & vbNewLine _
                                            & "               ) OUTKA                                                                    " & vbNewLine _
                                            & "        GROUP BY                                                                          " & vbNewLine _
                                            & "              OUTKA.OUTKA_NO_L                                                            " & vbNewLine _
                                            & "            , OUTKA.GOODS_CD_NRS                                                          " & vbNewLine _
                                            & "            , OUTKA.LOT_NO                                                                " & vbNewLine _
                                            & "            , OUTKA.IRIME                                                                 " & vbNewLine _
                                            & "            , OUTKA.IRIME_UT                                                              " & vbNewLine _
                                            & "            , OUTKA.REMARK                                                                " & vbNewLine _
                                            & "            , OUTKA.SERIAL_NO                                                             " & vbNewLine _
                                            & "            , OUTKA.NRS_BR_CD                                                             " & vbNewLine _
                                            & "            , OUTKA.WH_CD                                                                 " & vbNewLine _
                                            & "            , OUTKA.OUTKA_DATE                                                            " & vbNewLine _
                                            & "            , OUTKA.CUST_ORD_NO                                                           " & vbNewLine _
                                            & "            , OUTKA.CUST_CD_L                                                             " & vbNewLine _
                                            & "            , OUTKA.CUST_CD_M                                                             " & vbNewLine _
                                            & "            , OUTKA.ARR_PLAN_DATE                                                         " & vbNewLine _
                                            & "            , OUTKA.DEST_CD                                                               " & vbNewLine _
                                            & "            , OUTKA.DEST_KB                                                               " & vbNewLine _
                                            & "            , OUTKA.DEST_NM                                                               " & vbNewLine _
                                            & "            , OUTKA.DEST_AD_1                                                             " & vbNewLine _
                                            & "            , OUTKA.DEST_AD_2                                                             " & vbNewLine _
                                            & "            , OUTKA.KBN_NM2                                                               " & vbNewLine _
                                            & "   ) MAIN                                                                                 " & vbNewLine _
                                            & "     --商品ﾏｽﾀ(出荷)                                                                      " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_GOODS M_GOODS                                                  " & vbNewLine _
                                            & "            ON M_GOODS.NRS_BR_CD   = MAIN.NRS_BR_CD                                       " & vbNewLine _
                                            & "           AND MAIN.GOODS_CD_NRS   = M_GOODS.GOODS_CD_NRS                                 " & vbNewLine _
                                            & "     --出荷Lでの荷主帳票ﾊﾟﾀｰﾝ取得                                                         " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                  " & vbNewLine _
                                            & "            ON MCR1.NRS_BR_CD = MAIN.NRS_BR_CD                                            " & vbNewLine _
                                            & "           AND MCR1.CUST_CD_L = MAIN.CUST_CD_L                                            " & vbNewLine _
                                            & "           AND MCR1.CUST_CD_M = MAIN.CUST_CD_M                                            " & vbNewLine _
                                            & "           AND MCR1.CUST_CD_S = '00'                                                      " & vbNewLine _
                                            & "           AND MCR1.PTN_ID    = @PTN_ID                                                   " & vbNewLine _
                                            & "     --帳票ﾊﾟﾀｰﾝ取得                                                                      " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_RPT MR1                                                        " & vbNewLine _
                                            & "            ON MR1.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                            & "           AND MR1.PTN_ID    = MCR1.PTN_ID                                                " & vbNewLine _
                                            & "           AND MR1.PTN_CD    = MCR1.PTN_CD                                                " & vbNewLine _
                                            & "           AND MR1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                            & "     --商品Mの荷主での荷主帳票ﾊﾟﾀｰﾝ取得                                                   " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                  " & vbNewLine _
                                            & "            ON MCR2.NRS_BR_CD  = MAIN.NRS_BR_CD                                           " & vbNewLine _
                                            & "           AND M_GOODS.CUST_CD_L = MCR2.CUST_CD_L                                         " & vbNewLine _
                                            & "           AND M_GOODS.CUST_CD_M = MCR2.CUST_CD_M                                         " & vbNewLine _
                                            & "           AND M_GOODS.CUST_CD_S = MCR2.CUST_CD_S                                         " & vbNewLine _
                                            & "           AND MCR2.PTN_ID       = @PTN_ID                                                " & vbNewLine _
                                            & "     --帳票ﾊﾟﾀｰﾝ取得                                                                      " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_RPT MR2                                                        " & vbNewLine _
                                            & "            ON MR2.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                            & "           AND MR2.PTN_ID    = MCR2.PTN_ID                                                " & vbNewLine _
                                            & "           AND MR2.PTN_CD    = MCR2.PTN_CD                                                " & vbNewLine _
                                            & "           AND MR2.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                            & "     --存在しない場合の帳票ﾊﾟﾀｰﾝ取得                                                      " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_RPT MR3                                                        " & vbNewLine _
                                            & "            ON MR3.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                            & "           AND MR3.PTN_ID    = @PTN_ID                                                    " & vbNewLine _
                                            & "           AND MR3.STANDARD_FLAG = '01'                                                   " & vbNewLine _
                                            & "           AND MR3.SYS_DEL_FLG = '0'                                                      " & vbNewLine

#End Region 'SQL_FROM句                      

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得SQL（ヘッダ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FIRST As String = "  SELECT                                                                     " & vbNewLine _
                                             & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                         " & vbNewLine _
                                             & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                         " & vbNewLine _
                                             & "         ELSE MR3.RPT_ID END                             AS RPT_ID           " & vbNewLine _
                                             & "  , MAIN.NRS_BR_CD                                       AS NRS_BR_CD        " & vbNewLine _
                                             & "  , M_NRS_BR.NRS_BR_NM                                   AS NRS_BR_NM        " & vbNewLine _
                                             & "  , M_CUST.CUST_NM_L                                     AS CUST_NM_L        " & vbNewLine _
                                             & "  , MAIN.OUTKA_DATE                                      AS OUTKA_DATE       " & vbNewLine _
                                             & "--2018/03/07 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd start " & vbNewLine _
                                             & "  , CASE WHEN M_SOKO.WH_KB = '01' THEN M_SOKO.AD_1                           " & vbNewLine _
                                             & "         ELSE M_SOKO_USER.AD_1 END                       AS NRS_AD_1         " & vbNewLine _
                                             & "  , CASE WHEN M_SOKO.WH_KB = '01' THEN M_SOKO.AD_2                           " & vbNewLine _
                                             & "         ELSE M_SOKO_USER.AD_2 END                       AS NRS_AD_2         " & vbNewLine _
                                             & "  --, CASE WHEN M_SOKO.WH_KB = '01' THEN M_SOKO.AD_1                         " & vbNewLine _
                                             & "  --       ELSE M_NRS_BR.AD_1 END                        AS NRS_AD_1         " & vbNewLine _
                                             & "  --, M_NRS_BR.AD_1                                        AS NRS_AD_1       " & vbNewLine _
                                             & "  --, CASE WHEN M_SOKO.WH_KB = '01' THEN M_SOKO.AD_2                         " & vbNewLine _
                                             & "  --       ELSE M_NRS_BR.AD_2 END                        AS NRS_AD_2         " & vbNewLine _
                                             & "--2018/03/07 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd end  " & vbNewLine _
                                             & "  , M_NRS_BR.AD_2                                        AS NRS_AD_2         " & vbNewLine _
                                             & "  , M_CUST.CUST_NM_M                                     AS CUST_NM_M        " & vbNewLine _
                                             & "  , MAIN.CUST_ORD_NO                                     AS CUST_ORD_NO      " & vbNewLine _
                                             & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_NM                          " & vbNewLine _
                                             & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_NM                     " & vbNewLine _
                                             & "         ELSE M_DEST.DEST_NM                                                 " & vbNewLine _
                                             & "    END                                                  AS DEST_NM          " & vbNewLine _
                                             & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_AD_1                        " & vbNewLine _
                                             & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_AD_1                   " & vbNewLine _
                                             & "         ELSE M_DEST.AD_1                                                    " & vbNewLine _
                                             & "    END                                                  AS DEST_AD_1        " & vbNewLine _
                                             & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_AD_2                        " & vbNewLine _
                                             & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_AD_2                   " & vbNewLine _
                                             & "         ELSE M_DEST.AD_2                                                    " & vbNewLine _
                                             & "    END                                                  AS DEST_AD_2        " & vbNewLine _
                                             & "  , MAIN.CUST_CD_L                                       AS CUST_CD_L        " & vbNewLine _
                                             & "  , MAIN.CUST_CD_M                                       AS CUST_CD_M        " & vbNewLine _
                                             & "  , UNSOCO.UNSOCO_NM                                     AS UNSOCO_NM        " & vbNewLine _
                                             & "  , M_GOODS.GOODS_NM_1                                   AS GOODS_NM_1       " & vbNewLine _
                                             & "  , MAIN.LOT_NO                                          AS LOT_NO           " & vbNewLine _
                                             & "  , MAIN.IRIME                                           AS IRIME            " & vbNewLine _
                                             & "  , MAIN.ALCTD_NB                                        AS ALCTD_NB         " & vbNewLine _
                                             & "  , MAIN.ALCTD_QT                                        AS ALCTD_QT         " & vbNewLine _
                                             & "  , MAIN.IRIME_UT                                        AS IRIME_UT         " & vbNewLine _
                                             & "  , MAIN.OUTKA_NO_L                                      AS OUTKA_NO_L       " & vbNewLine _
                                             & "  , MAIN.REMARK                                          AS REMARK           " & vbNewLine _
                                             & "  --, MAIN.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT     " & vbNewLine _
                                             & "  , ISNULL(SUB.ZANSURYO ,0)                                        AS ALLOC_CAN_QT     " & vbNewLine _
                                             & "  , MAIN.ARR_PLAN_DATE                                   AS ARR_PLAN_DATE    " & vbNewLine _
                                             & "  , M_GOODS.GOODS_CD_NRS                                 AS GOODS_CD_NRS     " & vbNewLine _
                                             & "  --(2012.03.27)Notes№912 商品コード追加 -- START --                        " & vbNewLine _
                                             & "  , M_GOODS.GOODS_CD_CUST                                AS GOODS_CD_CUST    " & vbNewLine _
                                             & "  --(2012.03.27)Notes№912 商品コード追加 --  END  --                        " & vbNewLine _
                                             & "  , MAIN.SERIAL_NO                                       AS SERIAL_NO        " & vbNewLine _
                                             & "  , MAIN.KBN_NM2                                         AS DOKU_NM          " & vbNewLine _
                                             & "  ,@PRINT_DATE_FROM                                      AS PRINT_DATE_FROM  " & vbNewLine _
                                             & "  ,@PRINT_DATE_TO                                        AS PRINT_DATE_TO    " & vbNewLine


    ''' <summary>
    ''' 印刷データ取得時、追加FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ADD_FROM As String = "  --EDI出荷L                                     " & vbNewLine _
                                         & "	LEFT JOIN 				                                 " & vbNewLine _
                                         & "	(SELECT NRS_BR_CD 				                         " & vbNewLine _
                                         & "	,OUTKA_CTL_NO 				                             " & vbNewLine _
                                         & "	,MIN(DEST_NM)     AS DEST_NM 				                                 " & vbNewLine _
                                         & "	,MIN(DEST_AD_1)   AS DEST_AD_1				                                 " & vbNewLine _
                                         & "	,MIN(DEST_AD_2)   AS DEST_AD_2       				                         " & vbNewLine _
                                         & "	,SYS_DEL_FLG				                             " & vbNewLine _
                                         & "	FROM $LM_TRN$..H_OUTKAEDI_L 				             " & vbNewLine _
                                         & "	--AS EDI				                                 " & vbNewLine _
                                         & "    --(2013.01.25)ﾊﾟﾌｫｰﾏﾝｽ向上の為、条件追加 -- START --     " & vbNewLine _
                                         & "    WHERE NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                         & "      AND CUST_CD_L = @CUST_CD_L                             " & vbNewLine _
                                         & "      AND CUST_CD_M = @CUST_CD_M                             " & vbNewLine _
                                         & "    --(2013.01.25)ﾊﾟﾌｫｰﾏﾝｽ向上の為、条件追加 --  END  --     " & vbNewLine _
                                         & "	GROUP BY  				                                 " & vbNewLine _
                                         & "	 OUTKA_CTL_NO				                             " & vbNewLine _
                                         & "	,SYS_DEL_FLG				                             " & vbNewLine _
                                         & "	,NRS_BR_CD ) OUTKAEDIL     				                 " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    OUTKAEDIL.NRS_BR_CD    = MAIN.NRS_BR_CD          " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    OUTKAEDIL.OUTKA_CTL_NO = MAIN.OUTKA_NO_L     " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    OUTKAEDIL.SYS_DEL_FLG       = '0'                 " & vbNewLine _
                                         & "  --届先M                                        " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                " & vbNewLine _
                                         & "    $LM_MST$..M_DEST AS M_DEST                     " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    M_DEST.NRS_BR_CD   = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    M_DEST.CUST_CD_L   =  MAIN.CUST_CD_L             " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    MAIN.DEST_CD       = M_DEST.DEST_CD          " & vbNewLine _
                                         & "  --運送L                                        " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                " & vbNewLine _
                                         & "    $LM_TRN$..F_UNSO_L AS UNSOL                    " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    UNSOL.NRS_BR_CD    = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.INOUTKA_NO_L = MAIN.OUTKA_NO_L         " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.SYS_DEL_FLG  = '0'                     " & vbNewLine _
                                         & "  --運送会社M                                    " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                " & vbNewLine _
                                         & "    $LM_MST$..M_UNSOCO AS UNSOCO                   " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    UNSOCO.NRS_BR_CD   = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.UNSO_CD      = UNSOCO.UNSOCO_CD        " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.UNSO_BR_CD   = UNSOCO.UNSOCO_BR_CD     " & vbNewLine _
                                         & "  --営業所M                                      " & vbNewLine _
                                         & "  LEFT JOIN                                      " & vbNewLine _
                                         & "    $LM_MST$..M_NRS_BR M_NRS_BR                    " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    M_NRS_BR.NRS_BR_CD = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & "  --倉庫M                                        " & vbNewLine _
                                         & "  LEFT JOIN                                      " & vbNewLine _
                                         & "    $LM_MST$..M_SOKO M_SOKO                      " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    M_SOKO.WH_CD = MAIN.WH_CD                    " & vbNewLine _
                                         & "--2018/03/07 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start " & vbNewLine _
                                         & "  --倉庫M(ログインユーザーの倉庫コードから)      " & vbNewLine _
                                         & "  LEFT JOIN                                      " & vbNewLine _
                                         & "   (SELECT M_SOKO.*                              " & vbNewLine _
                                         & "    FROM $LM_MST$..S_USER                        " & vbNewLine _
                                         & "    LEFT JOIN $LM_MST$..M_SOKO                   " & vbNewLine _
                                         & "	ON S_USER.WH_CD = M_SOKO.WH_CD               " & vbNewLine _
                                         & "	WHERE S_USER.USER_CD = @USER_CD)M_SOKO_USER  " & vbNewLine _
                                         & "  ON M_SOKO_USER.NRS_BR_CD = MAIN.NRS_BR_CD      " & vbNewLine _
                                         & "--2018/03/07 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add end  " & vbNewLine _
                                         & "  --荷主M                                        " & vbNewLine _
                                         & "  LEFT JOIN                                      " & vbNewLine _
                                         & "   (                                             " & vbNewLine _
                                         & "     SELECT                                      " & vbNewLine _
                                         & "        CUST_CD_L       AS CUST_CD_L             " & vbNewLine _
                                         & "      , CUST_CD_M       AS CUST_CD_M             " & vbNewLine _
                                         & "      , MIN(CUST_NM_L)  AS CUST_NM_L             " & vbNewLine _
                                         & "      , MIN(CUST_NM_M)  As CUST_NM_M             " & vbNewLine _
                                         & "     FROM                                        " & vbNewLine _
                                         & "       $LM_MST$..M_CUST M_CUST                     " & vbNewLine _
                                         & "     WHERE                                       " & vbNewLine _
                                         & "       M_CUST.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
                                         & "     GROUP BY                                    " & vbNewLine _
                                         & "       CUST_CD_L, CUST_CD_M                      " & vbNewLine _
                                         & "   ) M_CUST                                      " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    MAIN.CUST_CD_L = M_CUST.CUST_CD_L            " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    MAIN.CUST_CD_M = M_CUST.CUST_CD_M            " & vbNewLine _
                                         & "LEFT JOIN                                        " & vbNewLine _
                                         & "	(SELECT                                      " & vbNewLine _
                                         & "	 MAIN2.LOT_NO AS LOT_NO                      " & vbNewLine _
                                         & "	,MAIN2.IRIME AS IRIME                        " & vbNewLine _
                                         & "	,SUM(MAIN2.ZANSURYO) AS ZANSURYO             " & vbNewLine _
                                         & "	,MAIN2.WH_CD AS WH_CD                        " & vbNewLine _
                                         & "	,MAIN2.NRS_BR_CD AS NRS_BR_CD                " & vbNewLine _
                                         & "	,MAIN2.GOODS_CD_NRS AS GOODS_CD_NRS          " & vbNewLine _
                                         & "	FROM (                                       " & vbNewLine _
                                         & "		SELECT                                   " & vbNewLine _
                                         & "			 ZAITRS.LOT_NO AS LOT_NO             " & vbNewLine _
                                         & "			,ZAITRS.IRIME AS IRIME               " & vbNewLine _
                                         & "			,ZAITRS.ALLOC_CAN_QT AS ZANSURYO     " & vbNewLine _
                                         & "			,ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS " & vbNewLine _
                                         & "			,ZAITRS.NRS_BR_CD AS NRS_BR_CD       " & vbNewLine _
                                         & "			,ZAITRS.WH_CD AS WH_CD               " & vbNewLine _
                                         & "		FROM                                     " & vbNewLine _
                                         & "		$LM_TRN$..D_ZAI_TRS AS ZAITRS            " & vbNewLine _
                                         & "		LEFT OUTER JOIN LM_MST..M_GOODS AS GOODS " & vbNewLine _
                                         & "		ON ZAITRS.NRS_BR_CD = GOODS.NRS_BR_CD    " & vbNewLine _
                                         & "		AND ZAITRS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS " & vbNewLine _
                                         & "		WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
                                         & "		AND ZAITRS.PORA_ZAI_NB <> 0              " & vbNewLine _
                                         & "		AND ZAITRS.SYS_DEL_FLG = '0'             " & vbNewLine _
                                         & "		) MAIN2                                  " & vbNewLine _
                                         & "	GROUP BY                                     " & vbNewLine _
                                         & "	MAIN2.LOT_NO                                 " & vbNewLine _
                                         & "    ,MAIN2.IRIME                                 " & vbNewLine _
                                         & "    ,MAIN2.WH_CD                                 " & vbNewLine _
                                         & "    ,MAIN2.NRS_BR_CD                             " & vbNewLine _
                                         & "    ,MAIN2.GOODS_CD_NRS                          " & vbNewLine _
                                         & "	) SUB ON                                     " & vbNewLine _
                                         & " SUB.LOT_NO = MAIN.LOT_NO                        " & vbNewLine _
                                         & " AND SUB.IRIME = MAIN.IRIME                      " & vbNewLine _
                                         & " AND SUB.WH_CD = MAIN.WH_CD                      " & vbNewLine _
                                         & " AND SUB.NRS_BR_CD = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & " AND SUB.GOODS_CD_NRS = MAIN.GOODS_CD_NRS        " & vbNewLine


    ''' <summary>
    ''' ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY " & vbNewLine _
                                         & "    CUST_CD_L, CUST_CD_M, OUTKA_DATE, CUST_ORD_NO, OUTKA_NO_L, GOODS_CD_CUST, LOT_NO, SERIAL_NO" & vbNewLine



    ''' <summary>
    ''' 月次用ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_MONTH As String = "ORDER BY                        " & vbNewLine _
                                         & "            GOODS_NM_1                " & vbNewLine _
                                         & "          , GOODS_CD_NRS              " & vbNewLine _
                                         & "          , OUTKA_DATE                " & vbNewLine _
                                         & "          , LOT_NO                    " & vbNewLine _
                                         & "          , IRIME                     " & vbNewLine _
                                         & "          , CUST_ORD_NO               " & vbNewLine





#End Region '印刷データ取得               

#Region "SQL LMC615専用"

    ''' <summary>
    ''' 印刷データ取得SQL（ヘッダ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FIRST_615 As String = " SELECT                                                                          " & vbNewLine _
                                                 & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                         " & vbNewLine _
                                                 & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                         " & vbNewLine _
                                                 & "        ELSE MR3.RPT_ID                                                          " & vbNewLine _
                                                 & "        END                                                  AS RPT_ID           " & vbNewLine _
                                                 & "      , MAIN.NRS_BR_CD                                       AS NRS_BR_CD        " & vbNewLine _
                                                 & "      , M_NRS_BR.NRS_BR_NM                                   AS NRS_BR_NM        " & vbNewLine _
                                                 & "      , MAIN.OUTKA_DATE                                      AS OUTKA_DATE       " & vbNewLine _
                                                 & "      , M_NRS_BR.AD_1                                        AS NRS_AD_1         " & vbNewLine _
                                                 & "      , M_NRS_BR.AD_2                                        AS NRS_AD_2         " & vbNewLine _
                                                 & "      , M_CUST.CUST_NM_L                                     AS CUST_NM_L        " & vbNewLine _
                                                 & "      , M_CUST.CUST_NM_M                                     AS CUST_NM_M        " & vbNewLine _
                                                 & "      , ''                                                   AS CUST_NM_S        " & vbNewLine _
                                                 & "      , MAIN.CUST_ORD_NO                                     AS CUST_ORD_NO      " & vbNewLine _
                                                 & "      , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_CD                          " & vbNewLine _
                                                 & "             WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_CD                     " & vbNewLine _
                                                 & "        ELSE M_DEST.DEST_CD                                                      " & vbNewLine _
                                                 & "        END                                                  AS DEST_CD          " & vbNewLine _
                                                 & "      , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_NM                          " & vbNewLine _
                                                 & "             WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_NM                     " & vbNewLine _
                                                 & "        ELSE M_DEST.DEST_NM                                                      " & vbNewLine _
                                                 & "        END                                                  AS DEST_NM          " & vbNewLine _
                                                 & "      , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_AD_1                        " & vbNewLine _
                                                 & "             WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_AD_1                   " & vbNewLine _
                                                 & "        ELSE M_DEST.AD_1                                                         " & vbNewLine _
                                                 & "        END                                                  AS DEST_AD_1        " & vbNewLine _
                                                 & "      , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_AD_2                        " & vbNewLine _
                                                 & "             WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_AD_2                   " & vbNewLine _
                                                 & "        ELSE M_DEST.AD_2                                                         " & vbNewLine _
                                                 & "        END                                                  AS DEST_AD_2        " & vbNewLine _
                                                 & "      , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_TEL                         " & vbNewLine _
                                                 & "             WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_TEL                    " & vbNewLine _
                                                 & "        ELSE M_DEST.TEL                                                          " & vbNewLine _
                                                 & "        END                                                  AS DEST_TEL         " & vbNewLine _
                                                 & "      , M_DEST.DELI_ATT                                      AS DELI_ATT         " & vbNewLine _
                                                 & "      , MAIN.CUST_CD_L                                       AS CUST_CD_L        " & vbNewLine _
                                                 & "      , MAIN.CUST_CD_M                                       AS CUST_CD_M        " & vbNewLine _
                                                 & "      , MAIN.CUST_CD_S                                       AS CUST_CD_S        " & vbNewLine _
                                                 & "      , MAIN.NHS_REMARK                                      AS NHS_REMARK       " & vbNewLine _
                                                 & "      , UNSOCO.UNSOCO_NM                                     AS UNSOCO_NM        " & vbNewLine _
                                                 & "      , M_GOODS.GOODS_NM_1                                   AS GOODS_NM_1       " & vbNewLine _
                                                 & "      , MAIN.LOT_NO                                          AS LOT_NO           " & vbNewLine _
                                                 & "      , MAIN.IRIME                                           AS IRIME            " & vbNewLine _
                                                 & "      , MAIN.ALCTD_NB                                        AS ALCTD_NB         " & vbNewLine _
                                                 & "      , MAIN.ALCTD_QT                                        AS ALCTD_QT         " & vbNewLine _
                                                 & "      , MAIN.IRIME_UT                                        AS IRIME_UT         " & vbNewLine _
                                                 & "      , MAIN.OUTKA_NO_L                                      AS OUTKA_NO_L       " & vbNewLine _
                                                 & "      , MAIN.REMARK                                          AS REMARK           " & vbNewLine _
                                                 & "      , MAIN.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT     " & vbNewLine _
                                                 & "      , MAIN.ARR_PLAN_DATE                                   AS ARR_PLAN_DATE    " & vbNewLine _
                                                 & "      , M_GOODS.GOODS_CD_NRS                                 AS GOODS_CD_NRS     " & vbNewLine _
                                                 & "      , M_GOODS.GOODS_CD_CUST                                AS GOODS_CD_CUST    " & vbNewLine _
                                                 & "      , M_GOODS.PKG_NB                                       AS PKG_NB           " & vbNewLine _
                                                 & "      , MAIN.SERIAL_NO                                       AS SERIAL_NO        " & vbNewLine _
                                                 & "      , MAIN.KBN_NM2                                         AS DOKU_NM          " & vbNewLine _
                                                 & "      , @PRINT_DATE_FROM                                     AS PRINT_DATE_FROM  " & vbNewLine _
                                                 & "      , @PRINT_DATE_TO                                       AS PRINT_DATE_TO    " & vbNewLine

    ''' <summary>
    ''' 共通FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ALL_FROM_615 As String = "  FROM                                                                              " & vbNewLine _
                                             & "  --出荷                                                                            " & vbNewLine _
                                             & "  (                                                                                 " & vbNewLine _
                                             & "    SELECT                                                                          " & vbNewLine _
                                             & "      OUTKA.OUTKA_NO_L          AS OUTKA_NO_L                                       " & vbNewLine _
                                             & "    , OUTKA.LOT_NO              AS LOT_NO                                           " & vbNewLine _
                                             & "    , OUTKA.IRIME               AS IRIME                                            " & vbNewLine _
                                             & "    , OUTKA.IRIME_UT            AS IRIME_UT                                         " & vbNewLine _
                                             & "    , OUTKA.REMARK              AS REMARK                                           " & vbNewLine _
                                             & "    , OUTKA.SERIAL_NO           AS SERIAL_NO                                        " & vbNewLine _
                                             & "    , OUTKA.GOODS_CD_NRS        AS GOODS_CD_NRS                                     " & vbNewLine _
                                             & "    , SUM(OUTKA.ALCTD_NB)       AS ALCTD_NB                                         " & vbNewLine _
                                             & "    , SUM(OUTKA.ALCTD_QT)       AS ALCTD_QT                                         " & vbNewLine _
                                             & "    , SUM(OUTKA.ALLOC_CAN_QT)   AS ALLOC_CAN_QT                                     " & vbNewLine _
                                             & "    , OUTKA.NRS_BR_CD           AS NRS_BR_CD                                        " & vbNewLine _
                                             & "    , OUTKA.WH_CD               AS WH_CD                                            " & vbNewLine _
                                             & "    , OUTKA.OUTKA_DATE          AS OUTKA_DATE                                       " & vbNewLine _
                                             & "    , OUTKA.CUST_ORD_NO         AS CUST_ORD_NO                                      " & vbNewLine _
                                             & "    , OUTKA.CUST_CD_L           AS CUST_CD_L                                        " & vbNewLine _
                                             & "    , OUTKA.CUST_CD_M           AS CUST_CD_M                                        " & vbNewLine _
                                             & "    , OUTKA.CUST_CD_S           AS CUST_CD_S                                        " & vbNewLine _
                                             & "    , OUTKA.NHS_REMARK          AS NHS_REMARK                                       " & vbNewLine _
                                             & "    , OUTKA.ARR_PLAN_DATE       AS ARR_PLAN_DATE                                    " & vbNewLine _
                                             & "    , OUTKA.DEST_CD             AS DEST_CD                                          " & vbNewLine _
                                             & "    , OUTKA.DEST_KB             AS DEST_KB                                          " & vbNewLine _
                                             & "    , OUTKA.DEST_NM             AS DEST_NM                                          " & vbNewLine _
                                             & "    , OUTKA.DEST_AD_1           AS DEST_AD_1                                        " & vbNewLine _
                                             & "    , OUTKA.DEST_AD_2           AS DEST_AD_2                                        " & vbNewLine _
                                             & "    , OUTKA.KBN_NM2             AS KBN_NM2                                          " & vbNewLine _
                                             & "    , OUTKA.DEST_TEL            AS DEST_TEL                                         " & vbNewLine _
                                             & "    FROM                                                                            " & vbNewLine _
                                             & "       (                                                                            " & vbNewLine _
                                             & "        SELECT                                                                      " & vbNewLine _
                                             & "            OUTKAL.OUTKA_NO_L          AS OUTKA_NO_L                                " & vbNewLine _
                                             & "          , OUTKAM.OUTKA_NO_M          AS OUTKA_NO_M                                " & vbNewLine _
                                             & "          , OUTKAS.LOT_NO              AS LOT_NO                                    " & vbNewLine _
                                             & "          , OUTKAS.IRIME               AS IRIME                                     " & vbNewLine _
                                             & "          , OUTKAM.IRIME_UT            AS IRIME_UT                                  " & vbNewLine _
                                             & "          , OUTKAM.REMARK              AS REMARK                                    " & vbNewLine _
                                             & "          , OUTKAM.SERIAL_NO           AS SERIAL_NO                                 " & vbNewLine _
                                             & "          , OUTKAM.GOODS_CD_NRS        AS GOODS_CD_NRS                              " & vbNewLine _
                                             & "          , SUM(OUTKAS.ALCTD_NB)       AS ALCTD_NB                                  " & vbNewLine _
                                             & "          , SUM(OUTKAS.ALCTD_QT)       AS ALCTD_QT                                  " & vbNewLine _
                                             & "          , OUTKAS.ZAI_REC_NO          AS ZAI_REC_NO                                " & vbNewLine _
                                             & "          , MAX(ZAITRS.ALLOC_CAN_QT)   AS ALLOC_CAN_QT                              " & vbNewLine _
                                             & "          , OUTKAL.NRS_BR_CD           AS NRS_BR_CD                                 " & vbNewLine _
                                             & "          , OUTKAL.WH_CD               AS WH_CD                                     " & vbNewLine _
                                             & "          , OUTKAL.OUTKA_DATE          AS OUTKA_DATE                                " & vbNewLine _
                                             & "          , OUTKAL.CUST_ORD_NO         AS CUST_ORD_NO                               " & vbNewLine _
                                             & "          , OUTKAL.CUST_CD_L           AS CUST_CD_L                                 " & vbNewLine _
                                             & "          , OUTKAL.CUST_CD_M           AS CUST_CD_M                                 " & vbNewLine _
                                             & "          , GOODS.CUST_CD_S            AS CUST_CD_S                                 " & vbNewLine _
                                             & "          , OUTKAL.NHS_REMARK          AS NHS_REMARK                                " & vbNewLine _
                                             & "          , OUTKAL.ARR_PLAN_DATE       AS ARR_PLAN_DATE                             " & vbNewLine _
                                             & "          , OUTKAL.DEST_CD             AS DEST_CD                                   " & vbNewLine _
                                             & "          , OUTKAL.DEST_KB             AS DEST_KB                                   " & vbNewLine _
                                             & "          , OUTKAL.DEST_NM             AS DEST_NM                                   " & vbNewLine _
                                             & "          , OUTKAL.DEST_AD_1           AS DEST_AD_1                                 " & vbNewLine _
                                             & "          , OUTKAL.DEST_AD_2           AS DEST_AD_2                                 " & vbNewLine _
                                             & "          , OUTKAL.DEST_TEL            AS DEST_TEL                                  " & vbNewLine _
                                             & "          , ZKBN.KBN_NM2               AS KBN_NM2                                   " & vbNewLine _
                                             & "        FROM                                                                        " & vbNewLine _
                                             & "        --出荷L                                                                     " & vbNewLine _
                                             & "              (                                                                     " & vbNewLine _
                                             & "                SELECT                                                              " & vbNewLine _
                                             & "                    NRS_BR_CD             AS NRS_BR_CD                              " & vbNewLine _
                                             & "                  , WH_CD                 AS WH_CD                                  " & vbNewLine _
                                             & "                  , OUTKA_PLAN_DATE       AS OUTKA_DATE                             " & vbNewLine _
                                             & "                  , CUST_ORD_NO           AS CUST_ORD_NO                            " & vbNewLine _
                                             & "                  , CUST_CD_L             AS CUST_CD_L                              " & vbNewLine _
                                             & "                  , CUST_CD_M             AS CUST_CD_M                              " & vbNewLine _
                                             & "                  , OUTKA_NO_L            AS OUTKA_NO_L                             " & vbNewLine _
                                             & "                  , ARR_PLAN_DATE         AS ARR_PLAN_DATE                          " & vbNewLine _
                                             & "                  , DEST_CD               AS DEST_CD                                " & vbNewLine _
                                             & "                  , DEST_KB               AS DEST_KB                                " & vbNewLine _
                                             & "                  , DEST_NM               AS DEST_NM                                " & vbNewLine _
                                             & "                  , DEST_AD_1             AS DEST_AD_1                              " & vbNewLine _
                                             & "                  , DEST_AD_2             AS DEST_AD_2                              " & vbNewLine _
                                             & "                  , DEST_TEL              AS DEST_TEL                               " & vbNewLine _
                                             & "                  , NHS_REMARK            AS NHS_REMARK                             " & vbNewLine _
                                             & "                  , SYS_DEL_FLG           AS SYS_DEL_FLG                            " & vbNewLine _
                                             & "               FROM                                                                          " & vbNewLine _
                                             & "                   $LM_TRN$..C_OUTKA_L                                                       " & vbNewLine _
                                             & "               --商品コード以外の抽出条件                                                    " & vbNewLine _
                                             & "               $WHERE1$                                                                      " & vbNewLine _
                                             & "               --ここまで                                                                    " & vbNewLine _
                                             & "               )             OUTKAL                                                          " & vbNewLine _
                                             & "          LEFT JOIN                                                                          " & vbNewLine _
                                             & "            $LM_TRN$..C_OUTKA_M AS OUTKAM                                                      " & vbNewLine _
                                             & "          ON                                                                                 " & vbNewLine _
                                             & "           OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                             & "          AND                                                                                " & vbNewLine _
                                             & "           OUTKAM.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                             & "          AND                                                                                " & vbNewLine _
                                             & "           OUTKAL.OUTKA_NO_L   = OUTKAM.OUTKA_NO_L                                           " & vbNewLine _
                                             & "          --商品ｺｰﾄﾞ条件はここ                                                               " & vbNewLine _
                                             & "          $WHERE2$                                                                           " & vbNewLine _
                                             & "          LEFT JOIN                                                                          " & vbNewLine _
                                             & "            $LM_TRN$..C_OUTKA_S AS OUTKAS                                                      " & vbNewLine _
                                             & "          ON                                                                                 " & vbNewLine _
                                             & "            OUTKAS.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                             & "          AND                                                                                " & vbNewLine _
                                             & "            OUTKAM.OUTKA_NO_L   = OUTKAS.OUTKA_NO_L                                          " & vbNewLine _
                                             & "          AND                                                                                " & vbNewLine _
                                             & "            OUTKAM.OUTKA_NO_M   = OUTKAS.OUTKA_NO_M                                          " & vbNewLine _
                                             & "          AND                                                                                " & vbNewLine _
                                             & "            OUTKAS.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                             & "          LEFT JOIN                                                                          " & vbNewLine _
                                             & "           $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                       " & vbNewLine _
                                             & "          ON                                                                                 " & vbNewLine _
                                             & "           ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                             & "         AND                                                                                 " & vbNewLine _
                                             & "           ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                             " & vbNewLine _
                                             & "         AND                                                                                 " & vbNewLine _
                                             & "           ZAITRS.CUST_CD_L = OUTKAl.CUST_CD_L                                               " & vbNewLine _
                                             & "         AND                                                                                 " & vbNewLine _
                                             & "           ZAITRS.CUST_CD_M = OUTKAL.CUST_CD_M                                               " & vbNewLine _
                                             & "          LEFT JOIN                                                                          " & vbNewLine _
                                             & "           $LM_MST$..M_GOODS AS GOODS                                                        " & vbNewLine _
                                             & "          ON                                                                                 " & vbNewLine _
                                             & "           GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                                " & vbNewLine _
                                             & "         AND                                                                                 " & vbNewLine _
                                             & "           GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                          " & vbNewLine _
                                             & "          LEFT JOIN                                                                          " & vbNewLine _
                                             & "           $LM_MST$..Z_KBN AS ZKBN                                                           " & vbNewLine _
                                             & "          ON                                                                                 " & vbNewLine _
                                             & "           GOODS.DOKU_KB = ZKBN.KBN_CD                                                       " & vbNewLine _
                                             & "         AND                                                                                 " & vbNewLine _
                                             & "           ZKBN.KBN_GROUP_CD = 'G001'                                                        " & vbNewLine _
                                             & "         AND                                                                                 " & vbNewLine _
                                             & "           ZKBN.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                             & "          WHERE                                                                              " & vbNewLine _
                                             & "           OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                             & "         AND                                                                                 " & vbNewLine _
                                             & "           OUTKAM.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                             & "          $WHERE3$                                                                           " & vbNewLine _
                                             & "         GROUP BY                                                                            " & vbNewLine _
                                             & "           OUTKAL.OUTKA_NO_L                                                                 " & vbNewLine _
                                             & "         , OUTKAM.OUTKA_NO_M                                                                 " & vbNewLine _
                                             & "         , OUTKAS.ZAI_REC_NO                                                                 " & vbNewLine _
                                             & "         , OUTKAS.LOT_NO                                                                     " & vbNewLine _
                                             & "         , OUTKAS.IRIME                                                                      " & vbNewLine _
                                             & "         , OUTKAM.IRIME_UT                                                                   " & vbNewLine _
                                             & "         , OUTKAM.REMARK                                                                     " & vbNewLine _
                                             & "         , OUTKAM.SERIAL_NO                                                                  " & vbNewLine _
                                             & "         , OUTKAM.GOODS_CD_NRS                                                               " & vbNewLine _
                                             & "         , OUTKAL.NRS_BR_CD                                                                  " & vbNewLine _
                                             & "         , OUTKAL.WH_CD                                                                      " & vbNewLine _
                                             & "         , OUTKAL.OUTKA_DATE                                                                 " & vbNewLine _
                                             & "         , OUTKAL.CUST_ORD_NO                                                                " & vbNewLine _
                                             & "         , OUTKAL.CUST_CD_L                                                                  " & vbNewLine _
                                             & "         , OUTKAL.CUST_CD_M                                                                  " & vbNewLine _
                                             & "         , GOODS.CUST_CD_S                                                                   " & vbNewLine _
                                             & "         , OUTKAL.NHS_REMARK                                                                 " & vbNewLine _
                                             & "         , OUTKAL.ARR_PLAN_DATE                                                              " & vbNewLine _
                                             & "         , OUTKAL.DEST_CD                                                                    " & vbNewLine _
                                             & "         , OUTKAL.DEST_KB                                                                    " & vbNewLine _
                                             & "         , OUTKAL.DEST_NM                                                                    " & vbNewLine _
                                             & "         , OUTKAL.DEST_AD_1                                                                  " & vbNewLine _
                                             & "         , OUTKAL.DEST_AD_2                                                                  " & vbNewLine _
                                             & "         , OUTKAL.DEST_TEL                                                                   " & vbNewLine _
                                             & "         , ZKBN.KBN_NM2                                                                      " & vbNewLine _
                                             & "       ) OUTKA                                                                               " & vbNewLine _
                                             & "    GROUP BY                                                                                 " & vbNewLine _
                                             & "       OUTKA.OUTKA_NO_L                                                                      " & vbNewLine _
                                             & "     , OUTKA.GOODS_CD_NRS                                                                    " & vbNewLine _
                                             & "     , OUTKA.LOT_NO                                                                          " & vbNewLine _
                                             & "     , OUTKA.IRIME                                                                           " & vbNewLine _
                                             & "     , OUTKA.IRIME_UT                                                                        " & vbNewLine _
                                             & "     , OUTKA.REMARK                                                                          " & vbNewLine _
                                             & "     , OUTKA.SERIAL_NO                                                                       " & vbNewLine _
                                             & "     , OUTKA.NRS_BR_CD                                                                       " & vbNewLine _
                                             & "     , OUTKA.WH_CD                                                                           " & vbNewLine _
                                             & "     , OUTKA.OUTKA_DATE                                                                      " & vbNewLine _
                                             & "     , OUTKA.CUST_ORD_NO                                                                     " & vbNewLine _
                                             & "     , OUTKA.CUST_CD_L                                                                       " & vbNewLine _
                                             & "     , OUTKA.CUST_CD_M                                                                       " & vbNewLine _
                                             & "     , OUTKA.CUST_CD_S                                                                       " & vbNewLine _
                                             & "     , OUTKA.NHS_REMARK                                                                      " & vbNewLine _
                                             & "     , OUTKA.ARR_PLAN_DATE                                                                   " & vbNewLine _
                                             & "     , OUTKA.DEST_CD                                                                         " & vbNewLine _
                                             & "     , OUTKA.DEST_KB                                                                         " & vbNewLine _
                                             & "     , OUTKA.DEST_NM                                                                         " & vbNewLine _
                                             & "     , OUTKA.DEST_AD_1                                                                       " & vbNewLine _
                                             & "     , OUTKA.DEST_AD_2                                                                       " & vbNewLine _
                                             & "     , OUTKA.DEST_TEL                                                                        " & vbNewLine _
                                             & "     , OUTKA.KBN_NM2                                                                         " & vbNewLine _
                                             & "  ) MAIN                                                                                     " & vbNewLine _
                                             & "  --商品ﾏｽﾀ(出荷)                                                                            " & vbNewLine _
                                             & "  LEFT JOIN                                                                                  " & vbNewLine _
                                             & "   $LM_MST$..M_GOODS M_GOODS                                                                 " & vbNewLine _
                                             & "  ON                                                                                         " & vbNewLine _
                                             & "   M_GOODS.NRS_BR_CD   = MAIN.NRS_BR_CD                                                      " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MAIN.GOODS_CD_NRS   = M_GOODS.GOODS_CD_NRS                                                " & vbNewLine _
                                             & "  --出荷Lでの荷主帳票ﾊﾟﾀｰﾝ取得                                                               " & vbNewLine _
                                             & "  LEFT JOIN                                                                                  " & vbNewLine _
                                             & "   $LM_MST$..M_CUST_RPT MCR1                                                                   " & vbNewLine _
                                             & "  ON                                                                                         " & vbNewLine _
                                             & "   MCR1.NRS_BR_CD = MAIN.NRS_BR_CD                                                               " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MCR1.CUST_CD_L = MAIN.CUST_CD_L                                                               " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MCR1.CUST_CD_M = MAIN.CUST_CD_M                                                               " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MCR1.CUST_CD_S = '00'                                                                     " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MCR1.PTN_ID    = @PTN_ID                                                                     " & vbNewLine _
                                             & "  --帳票ﾊﾟﾀｰﾝ取得                                                                            " & vbNewLine _
                                             & "  LEFT JOIN                                                                                  " & vbNewLine _
                                             & "   $LM_MST$..M_RPT MR1                                                                         " & vbNewLine _
                                             & "  ON                                                                                         " & vbNewLine _
                                             & "   MR1.NRS_BR_CD = MAIN.NRS_BR_CD                                                                " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MR1.PTN_ID    = MCR1.PTN_ID                                                               " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MR1.PTN_CD    = MCR1.PTN_CD                                                               " & vbNewLine _
                                             & "  AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                             & "  --商品Mの荷主での荷主帳票ﾊﾟﾀｰﾝ取得                                                         " & vbNewLine _
                                             & "  LEFT JOIN                                                                                  " & vbNewLine _
                                             & "   $LM_MST$..M_CUST_RPT MCR2                                                                   " & vbNewLine _
                                             & "  ON                                                                                         " & vbNewLine _
                                             & "   MCR2.NRS_BR_CD  = MAIN.NRS_BR_CD                                                              " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   M_GOODS.CUST_CD_L = MCR2.CUST_CD_L                                                        " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   M_GOODS.CUST_CD_M = MCR2.CUST_CD_M                                                        " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   M_GOODS.CUST_CD_S = MCR2.CUST_CD_S                                                        " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MCR2.PTN_ID           = @PTN_ID                                                             " & vbNewLine _
                                             & "  --帳票ﾊﾟﾀｰﾝ取得                                                                            " & vbNewLine _
                                             & "  LEFT JOIN                                                                                  " & vbNewLine _
                                             & "   $LM_MST$..M_RPT MR2                                                                         " & vbNewLine _
                                             & "  ON                                                                                         " & vbNewLine _
                                             & "   MR2.NRS_BR_CD = MAIN.NRS_BR_CD                                                                " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MR2.PTN_ID    = MCR2.PTN_ID                                                               " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MR2.PTN_CD    = MCR2.PTN_CD                                                               " & vbNewLine _
                                             & "  AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                             & "  --存在しない場合の帳票ﾊﾟﾀｰﾝ取得                                                            " & vbNewLine _
                                             & "  LEFT JOIN                                                                                  " & vbNewLine _
                                             & "   $LM_MST$..M_RPT MR3                                                                         " & vbNewLine _
                                             & "  ON                                                                                         " & vbNewLine _
                                             & "   MR3.NRS_BR_CD     = MAIN.NRS_BR_CD                                                            " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MR3.PTN_ID        = @PTN_ID                                                                  " & vbNewLine _
                                             & "  AND                                                                                        " & vbNewLine _
                                             & "   MR3.STANDARD_FLAG = '01'                                                                  " & vbNewLine _
                                             & "   AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine


    Private Const SQL_CUST_S_FROM_615 As String = " FROM                                                                                     " & vbNewLine _
                                                & "      --出荷                                                                              " & vbNewLine _
                                                & "      (                                                                                   " & vbNewLine _
                                                & "        SELECT                                                                            " & vbNewLine _
                                                & "               OUTKA.OUTKA_NO_L         AS OUTKA_NO_L                                     " & vbNewLine _
                                                & "             , OUTKA.LOT_NO             AS LOT_NO                                         " & vbNewLine _
                                                & "             , OUTKA.IRIME              AS IRIME                                          " & vbNewLine _
                                                & "             , OUTKA.IRIME_UT           AS IRIME_UT                                       " & vbNewLine _
                                                & "             , OUTKA.REMARK             AS REMARK                                         " & vbNewLine _
                                                & "             , OUTKA.SERIAL_NO          AS SERIAL_NO                                      " & vbNewLine _
                                                & "             , OUTKA.GOODS_CD_NRS       AS GOODS_CD_NRS                                   " & vbNewLine _
                                                & "             , SUM(OUTKA.ALCTD_NB)      AS ALCTD_NB                                       " & vbNewLine _
                                                & "             , SUM(OUTKA.ALCTD_QT)      AS ALCTD_QT                                       " & vbNewLine _
                                                & "             , SUM(OUTKA.ALLOC_CAN_QT)  AS ALLOC_CAN_QT                                   " & vbNewLine _
                                                & "             , OUTKA.NRS_BR_CD          AS NRS_BR_CD                                      " & vbNewLine _
                                                & "             , OUTKA.WH_CD              AS WH_CD                                          " & vbNewLine _
                                                & "             , OUTKA.OUTKA_DATE         AS OUTKA_DATE                                     " & vbNewLine _
                                                & "             , OUTKA.CUST_ORD_NO        AS CUST_ORD_NO                                    " & vbNewLine _
                                                & "             , OUTKA.CUST_CD_L          AS CUST_CD_L                                      " & vbNewLine _
                                                & "             , OUTKA.CUST_CD_M          AS CUST_CD_M                                      " & vbNewLine _
                                                & "             , OUTKA.CUST_CD_S          AS CUST_CD_S                                      " & vbNewLine _
                                                & "             , OUTKA.NHS_REMARK         AS NHS_REMARK                                     " & vbNewLine _
                                                & "             , OUTKA.ARR_PLAN_DATE      AS ARR_PLAN_DATE                                  " & vbNewLine _
                                                & "             , OUTKA.DEST_CD            AS DEST_CD                                        " & vbNewLine _
                                                & "             , OUTKA.DEST_KB            AS DEST_KB                                        " & vbNewLine _
                                                & "             , OUTKA.DEST_NM            AS DEST_NM                                        " & vbNewLine _
                                                & "             , OUTKA.DEST_AD_1          AS DEST_AD_1                                      " & vbNewLine _
                                                & "             , OUTKA.DEST_AD_2          AS DEST_AD_2                                      " & vbNewLine _
                                                & "             , OUTKA.DEST_TEL           AS DEST_TEL                                       " & vbNewLine _
                                                & "             , OUTKA.KBN_NM2            AS KBN_NM2                                        " & vbNewLine _
                                                & "          FROM (                                                                          " & vbNewLine _
                                                & "                 SELECT                                                                   " & vbNewLine _
                                                & "                        OUTKAL.OUTKA_NO_L        AS OUTKA_NO_L                            " & vbNewLine _
                                                & "                      , OUTKAM.OUTKA_NO_M        AS OUTKA_NO_M                            " & vbNewLine _
                                                & "                      , OUTKAS.LOT_NO            AS LOT_NO                                " & vbNewLine _
                                                & "                      , OUTKAS.IRIME             AS IRIME                                 " & vbNewLine _
                                                & "                      , OUTKAM.IRIME_UT          AS IRIME_UT                              " & vbNewLine _
                                                & "                      , OUTKAM.REMARK            AS REMARK                                " & vbNewLine _
                                                & "                      , OUTKAM.SERIAL_NO         AS SERIAL_NO                             " & vbNewLine _
                                                & "                      , OUTKAM.GOODS_CD_NRS      AS GOODS_CD_NRS                          " & vbNewLine _
                                                & "                      , SUM(OUTKAS.ALCTD_NB)     AS ALCTD_NB                              " & vbNewLine _
                                                & "                      , SUM(OUTKAS.ALCTD_QT)     AS ALCTD_QT                              " & vbNewLine _
                                                & "                      , OUTKAS.ZAI_REC_NO        AS ZAI_REC_NO                            " & vbNewLine _
                                                & "                      , MAX(ZAITRS.ALLOC_CAN_QT) AS ALLOC_CAN_QT                          " & vbNewLine _
                                                & "                      , OUTKAL.NRS_BR_CD         AS NRS_BR_CD                             " & vbNewLine _
                                                & "                      , OUTKAL.WH_CD             AS WH_CD                                 " & vbNewLine _
                                                & "                      , OUTKAL.OUTKA_DATE        AS OUTKA_DATE                            " & vbNewLine _
                                                & "                      , OUTKAL.CUST_ORD_NO       AS CUST_ORD_NO                           " & vbNewLine _
                                                & "                      , OUTKAL.CUST_CD_L         AS CUST_CD_L                             " & vbNewLine _
                                                & "                      , OUTKAL.CUST_CD_M         AS CUST_CD_M                             " & vbNewLine _
                                                & "                      , GOODS.CUST_CD_S          AS CUST_CD_S                             " & vbNewLine _
                                                & "                      , OUTKAL.NHS_REMARK        AS NHS_REMARK                            " & vbNewLine _
                                                & "                      , OUTKAL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                         " & vbNewLine _
                                                & "                      , OUTKAL.DEST_CD           AS DEST_CD                               " & vbNewLine _
                                                & "                      , OUTKAL.DEST_KB           AS DEST_KB                               " & vbNewLine _
                                                & "                      , OUTKAL.DEST_NM           AS DEST_NM                               " & vbNewLine _
                                                & "                      , OUTKAL.DEST_AD_1         AS DEST_AD_1                             " & vbNewLine _
                                                & "                      , OUTKAL.DEST_AD_2         AS DEST_AD_2                             " & vbNewLine _
                                                & "                      , OUTKAL.DEST_TEL          AS DEST_TEL                              " & vbNewLine _
                                                & "                      , ZKBN.KBN_NM2             AS KBN_NM2                               " & vbNewLine _
                                                & "                   FROM                                                                   " & vbNewLine _
                                                & "                        --出荷L                                                           " & vbNewLine _
                                                & "                        (                                                                 " & vbNewLine _
                                                & "                         SELECT                                                           " & vbNewLine _
                                                & "                                NRS_BR_CD           AS NRS_BR_CD                          " & vbNewLine _
                                                & "                              , WH_CD               AS WH_CD                              " & vbNewLine _
                                                & "                              , OUTKA_PLAN_DATE     AS OUTKA_DATE                         " & vbNewLine _
                                                & "                              , CUST_ORD_NO         AS CUST_ORD_NO                        " & vbNewLine _
                                                & "                              , CUST_CD_L           AS CUST_CD_L                          " & vbNewLine _
                                                & "                              , CUST_CD_M           AS CUST_CD_M                          " & vbNewLine _
                                                & "                              , OUTKA_NO_L          AS OUTKA_NO_L                         " & vbNewLine _
                                                & "                              , ARR_PLAN_DATE       AS ARR_PLAN_DATE                      " & vbNewLine _
                                                & "                              , DEST_CD             AS DEST_CD                            " & vbNewLine _
                                                & "                              , DEST_KB             AS DEST_KB                            " & vbNewLine _
                                                & "                              , DEST_NM             AS DEST_NM                            " & vbNewLine _
                                                & "                              , DEST_AD_1           AS DEST_AD_1                          " & vbNewLine _
                                                & "                              , DEST_AD_2           AS DEST_AD_2                          " & vbNewLine _
                                                & "                              , DEST_TEL            AS DEST_TEL                           " & vbNewLine _
                                                & "                              , SYS_DEL_FLG         AS SYS_DEL_FLG                        " & vbNewLine _
                                                & "                              , NHS_REMARK          AS NHS_REMARK                         " & vbNewLine _
                                                & "                           FROM $LM_TRN$..C_OUTKA_L                                       " & vbNewLine _
                                                & "                                --商品コード以外の抽出条件                                " & vbNewLine _
                                                & "                                $WHERE1$                                                  " & vbNewLine _
                                                & "                                --ここまで                                                " & vbNewLine _
                                                & "                         ) OUTKAL                                                         " & vbNewLine _
                                                & "                                LEFT JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM                   " & vbNewLine _
                                                & "                                       ON OUTKAM.NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
                                                & "                                      AND OUTKAM.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                                & "                                      AND OUTKAL.OUTKA_NO_L   = OUTKAM.OUTKA_NO_L         " & vbNewLine _
                                                & "                                --商品ｺｰﾄﾞ条件はここ                                      " & vbNewLine _
                                                & "                                $WHERE2$                                                  " & vbNewLine _
                                                & "                                LEFT JOIN $LM_TRN$..C_OUTKA_S AS OUTKAS                   " & vbNewLine _
                                                & "                                       ON OUTKAS.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                                & "                                      AND OUTKAM.OUTKA_NO_L  = OUTKAS.OUTKA_NO_L          " & vbNewLine _
                                                & "                                      AND OUTKAM.OUTKA_NO_M  = OUTKAS.OUTKA_NO_M          " & vbNewLine _
                                                & "                                      AND OUTKAS.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                                & "                                LEFT JOIN $LM_TRN$..D_ZAI_TRS AS ZAITRS                   " & vbNewLine _
                                                & "                                       ON ZAITRS.NRS_BR_CD  = @NRS_BR_CD                  " & vbNewLine _
                                                & "                                      AND ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO           " & vbNewLine _
                                                & "                                      AND ZAITRS.CUST_CD_L  = OUTKAl.CUST_CD_L            " & vbNewLine _
                                                & "                                      AND ZAITRS.CUST_CD_M  = OUTKAL.CUST_CD_M            " & vbNewLine _
                                                & "                                LEFT JOIN $LM_MST$..M_GOODS AS GOODS                      " & vbNewLine _
                                                & "                                       ON GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD              " & vbNewLine _
                                                & "                                      AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS        " & vbNewLine _
                                                & "                                LEFT JOIN $LM_MST$..Z_KBN AS ZKBN                         " & vbNewLine _
                                                & "                                       ON GOODS.DOKU_KB = ZKBN.KBN_CD                     " & vbNewLine _
                                                & "                                      AND ZKBN.KBN_GROUP_CD = 'G001'                      " & vbNewLine _
                                                & "                                      AND ZKBN.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                                & "                  WHERE OUTKAM.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                                & "                    AND OUTKAM.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                                & "                    AND GOODS.CUST_CD_L = @CUST_CD_L                                      " & vbNewLine _
                                                & "                    AND GOODS.CUST_CD_M = @CUST_CD_M                                      " & vbNewLine _
                                                & "                    AND GOODS.CUST_CD_S = @CUST_CD_S                                      " & vbNewLine _
                                                & "                  $WHERE3$                                                                " & vbNewLine _
                                                & "                  GROUP BY                                                                " & vbNewLine _
                                                & "                        OUTKAL.OUTKA_NO_L                                                 " & vbNewLine _
                                                & "                      , OUTKAM.OUTKA_NO_M                                                 " & vbNewLine _
                                                & "                      , OUTKAS.ZAI_REC_NO                                                 " & vbNewLine _
                                                & "                      , OUTKAS.LOT_NO                                                     " & vbNewLine _
                                                & "                      , OUTKAS.IRIME                                                      " & vbNewLine _
                                                & "                      , OUTKAM.IRIME_UT                                                   " & vbNewLine _
                                                & "                      , OUTKAM.REMARK                                                     " & vbNewLine _
                                                & "                      , OUTKAM.SERIAL_NO                                                  " & vbNewLine _
                                                & "                      , OUTKAM.GOODS_CD_NRS                                               " & vbNewLine _
                                                & "                      , OUTKAL.NRS_BR_CD                                                  " & vbNewLine _
                                                & "                      , OUTKAL.WH_CD                                                      " & vbNewLine _
                                                & "                      , OUTKAL.OUTKA_DATE                                                 " & vbNewLine _
                                                & "                      , OUTKAL.CUST_ORD_NO                                                " & vbNewLine _
                                                & "                      , OUTKAL.CUST_CD_L                                                  " & vbNewLine _
                                                & "                      , OUTKAL.CUST_CD_M                                                  " & vbNewLine _
                                                & "                      , GOODS.CUST_CD_S                                                   " & vbNewLine _
                                                & "                      , OUTKAL.NHS_REMARK                                                 " & vbNewLine _
                                                & "                      , OUTKAL.ARR_PLAN_DATE                                              " & vbNewLine _
                                                & "                      , OUTKAL.DEST_CD                                                    " & vbNewLine _
                                                & "                      , OUTKAL.DEST_KB                                                    " & vbNewLine _
                                                & "                      , OUTKAL.DEST_NM                                                    " & vbNewLine _
                                                & "                      , OUTKAL.DEST_AD_1                                                  " & vbNewLine _
                                                & "                      , OUTKAL.DEST_AD_2                                                  " & vbNewLine _
                                                & "                      , OUTKAL.DEST_TEL                                                   " & vbNewLine _
                                                & "                      , ZKBN.KBN_NM2                                                      " & vbNewLine _
                                                & "               ) OUTKA                                                                    " & vbNewLine _
                                                & "        GROUP BY                                                                          " & vbNewLine _
                                                & "              OUTKA.OUTKA_NO_L                                                            " & vbNewLine _
                                                & "            , OUTKA.GOODS_CD_NRS                                                          " & vbNewLine _
                                                & "            , OUTKA.LOT_NO                                                                " & vbNewLine _
                                                & "            , OUTKA.IRIME                                                                 " & vbNewLine _
                                                & "            , OUTKA.IRIME_UT                                                              " & vbNewLine _
                                                & "            , OUTKA.REMARK                                                                " & vbNewLine _
                                                & "            , OUTKA.SERIAL_NO                                                             " & vbNewLine _
                                                & "            , OUTKA.NRS_BR_CD                                                             " & vbNewLine _
                                                & "            , OUTKA.WH_CD                                                                 " & vbNewLine _
                                                & "            , OUTKA.OUTKA_DATE                                                            " & vbNewLine _
                                                & "            , OUTKA.CUST_ORD_NO                                                           " & vbNewLine _
                                                & "            , OUTKA.CUST_CD_L                                                             " & vbNewLine _
                                                & "            , OUTKA.CUST_CD_M                                                             " & vbNewLine _
                                                & "            , OUTKA.CUST_CD_S                                                             " & vbNewLine _
                                                & "            , OUTKA.NHS_REMARK                                                            " & vbNewLine _
                                                & "            , OUTKA.ARR_PLAN_DATE                                                         " & vbNewLine _
                                                & "            , OUTKA.DEST_CD                                                               " & vbNewLine _
                                                & "            , OUTKA.DEST_KB                                                               " & vbNewLine _
                                                & "            , OUTKA.DEST_NM                                                               " & vbNewLine _
                                                & "            , OUTKA.DEST_AD_1                                                             " & vbNewLine _
                                                & "            , OUTKA.DEST_AD_2                                                             " & vbNewLine _
                                                & "            , OUTKA.DEST_TEL                                                              " & vbNewLine _
                                                & "            , OUTKA.KBN_NM2                                                               " & vbNewLine _
                                                & "   ) MAIN                                                                                 " & vbNewLine _
                                                & "     --商品ﾏｽﾀ(出荷)                                                                      " & vbNewLine _
                                                & "     LEFT JOIN $LM_MST$..M_GOODS M_GOODS                                                  " & vbNewLine _
                                                & "            ON M_GOODS.NRS_BR_CD   = MAIN.NRS_BR_CD                                       " & vbNewLine _
                                                & "           AND MAIN.GOODS_CD_NRS   = M_GOODS.GOODS_CD_NRS                                 " & vbNewLine _
                                                & "     --出荷Lでの荷主帳票ﾊﾟﾀｰﾝ取得                                                         " & vbNewLine _
                                                & "     LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                  " & vbNewLine _
                                                & "            ON MCR1.NRS_BR_CD = MAIN.NRS_BR_CD                                            " & vbNewLine _
                                                & "           AND MCR1.CUST_CD_L = MAIN.CUST_CD_L                                            " & vbNewLine _
                                                & "           AND MCR1.CUST_CD_M = MAIN.CUST_CD_M                                            " & vbNewLine _
                                                & "           AND MCR1.CUST_CD_S = '00'                                                      " & vbNewLine _
                                                & "           AND MCR1.PTN_ID    = @PTN_ID                                                   " & vbNewLine _
                                                & "     --帳票ﾊﾟﾀｰﾝ取得                                                                      " & vbNewLine _
                                                & "     LEFT JOIN $LM_MST$..M_RPT MR1                                                        " & vbNewLine _
                                                & "            ON MR1.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                                & "           AND MR1.PTN_ID    = MCR1.PTN_ID                                                " & vbNewLine _
                                                & "           AND MR1.PTN_CD    = MCR1.PTN_CD                                                " & vbNewLine _
                                                & "           AND MR1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                                & "     --商品Mの荷主での荷主帳票ﾊﾟﾀｰﾝ取得                                                   " & vbNewLine _
                                                & "     LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                  " & vbNewLine _
                                                & "            ON MCR2.NRS_BR_CD  = MAIN.NRS_BR_CD                                           " & vbNewLine _
                                                & "           AND M_GOODS.CUST_CD_L = MCR2.CUST_CD_L                                         " & vbNewLine _
                                                & "           AND M_GOODS.CUST_CD_M = MCR2.CUST_CD_M                                         " & vbNewLine _
                                                & "           AND M_GOODS.CUST_CD_S = MCR2.CUST_CD_S                                         " & vbNewLine _
                                                & "           AND MCR2.PTN_ID       = @PTN_ID                                                " & vbNewLine _
                                                & "     --帳票ﾊﾟﾀｰﾝ取得                                                                      " & vbNewLine _
                                                & "     LEFT JOIN $LM_MST$..M_RPT MR2                                                        " & vbNewLine _
                                                & "            ON MR2.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                                & "           AND MR2.PTN_ID    = MCR2.PTN_ID                                                " & vbNewLine _
                                                & "           AND MR2.PTN_CD    = MCR2.PTN_CD                                                " & vbNewLine _
                                                & "           AND MR2.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                                & "     --存在しない場合の帳票ﾊﾟﾀｰﾝ取得                                                      " & vbNewLine _
                                                & "     LEFT JOIN $LM_MST$..M_RPT MR3                                                        " & vbNewLine _
                                                & "            ON MR3.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                                & "           AND MR3.PTN_ID    = @PTN_ID                                                    " & vbNewLine _
                                                & "           AND MR3.STANDARD_FLAG = '01'                                                   " & vbNewLine _
                                                & "           AND MR3.SYS_DEL_FLG = '0'                                                      " & vbNewLine


    ''' <summary>
    ''' 印刷データ取得時、追加FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ADD_FROM_615 As String = "  --EDI出荷L                                                 " & vbNewLine _
                                             & "	LEFT JOIN 				                                 " & vbNewLine _
                                             & "	(SELECT NRS_BR_CD 				                         " & vbNewLine _
                                             & "	,OUTKA_CTL_NO 				                             " & vbNewLine _
                                             & "	,MIN(DEST_CD)     AS DEST_CD                             " & vbNewLine _
                                             & "	,MIN(DEST_TEL)    AS DEST_TEL                            " & vbNewLine _
                                             & "	,MIN(DEST_NM)     AS DEST_NM 				             " & vbNewLine _
                                             & "	,MIN(DEST_AD_1)   AS DEST_AD_1				             " & vbNewLine _
                                             & "	,MIN(DEST_AD_2)   AS DEST_AD_2       				     " & vbNewLine _
                                             & "	,SYS_DEL_FLG				                             " & vbNewLine _
                                             & "	FROM $LM_TRN$..H_OUTKAEDI_L 				             " & vbNewLine _
                                             & "	--AS EDI				                                 " & vbNewLine _
                                             & "    --(2013.01.25)ﾊﾟﾌｫｰﾏﾝｽ向上の為、条件追加 -- START --     " & vbNewLine _
                                             & "    WHERE NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                             & "      AND CUST_CD_L = @CUST_CD_L                             " & vbNewLine _
                                             & "      AND CUST_CD_M = @CUST_CD_M                             " & vbNewLine _
                                             & "    --(2013.01.25)ﾊﾟﾌｫｰﾏﾝｽ向上の為、条件追加 --  END  --     " & vbNewLine _
                                             & "	GROUP BY  				                                 " & vbNewLine _
                                             & "	 OUTKA_CTL_NO				                             " & vbNewLine _
                                             & "	,SYS_DEL_FLG				                             " & vbNewLine _
                                             & "	,NRS_BR_CD ) OUTKAEDIL     				                 " & vbNewLine _
                                             & "  ON                                             " & vbNewLine _
                                             & "    OUTKAEDIL.NRS_BR_CD    = MAIN.NRS_BR_CD          " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    OUTKAEDIL.OUTKA_CTL_NO = MAIN.OUTKA_NO_L     " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    OUTKAEDIL.SYS_DEL_FLG       = '0'                 " & vbNewLine _
                                             & "  --届先M                                        " & vbNewLine _
                                             & "  LEFT OUTER JOIN                                " & vbNewLine _
                                             & "    $LM_MST$..M_DEST AS M_DEST                     " & vbNewLine _
                                             & "  ON                                             " & vbNewLine _
                                             & "    M_DEST.NRS_BR_CD   = MAIN.NRS_BR_CD              " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    M_DEST.CUST_CD_L   =  MAIN.CUST_CD_L             " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    MAIN.DEST_CD       = M_DEST.DEST_CD          " & vbNewLine _
                                             & "  --運送L                                        " & vbNewLine _
                                             & "  LEFT OUTER JOIN                                " & vbNewLine _
                                             & "    $LM_TRN$..F_UNSO_L AS UNSOL                    " & vbNewLine _
                                             & "  ON                                             " & vbNewLine _
                                             & "    UNSOL.NRS_BR_CD    = MAIN.NRS_BR_CD              " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    UNSOL.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    UNSOL.INOUTKA_NO_L = MAIN.OUTKA_NO_L         " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    UNSOL.SYS_DEL_FLG  = '0'                     " & vbNewLine _
                                             & "  --運送会社M                                    " & vbNewLine _
                                             & "  LEFT OUTER JOIN                                " & vbNewLine _
                                             & "    $LM_MST$..M_UNSOCO AS UNSOCO                   " & vbNewLine _
                                             & "  ON                                             " & vbNewLine _
                                             & "    UNSOCO.NRS_BR_CD   = MAIN.NRS_BR_CD              " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    UNSOL.UNSO_CD      = UNSOCO.UNSOCO_CD        " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    UNSOL.UNSO_BR_CD   = UNSOCO.UNSOCO_BR_CD     " & vbNewLine _
                                             & "  --営業所M                                      " & vbNewLine _
                                             & "  LEFT JOIN                                      " & vbNewLine _
                                             & "    $LM_MST$..M_NRS_BR M_NRS_BR                    " & vbNewLine _
                                             & "  ON                                             " & vbNewLine _
                                             & "    M_NRS_BR.NRS_BR_CD = MAIN.NRS_BR_CD              " & vbNewLine _
                                             & "  --倉庫M                                        " & vbNewLine _
                                             & "  LEFT JOIN                                      " & vbNewLine _
                                             & "    $LM_MST$..M_SOKO M_SOKO                      " & vbNewLine _
                                             & "  ON                                             " & vbNewLine _
                                             & "    M_SOKO.WH_CD = MAIN.WH_CD                    " & vbNewLine _
                                             & "  --荷主M                                        " & vbNewLine _
                                             & "  LEFT JOIN                                      " & vbNewLine _
                                             & "   (                                             " & vbNewLine _
                                             & "     SELECT                                      " & vbNewLine _
                                             & "        CUST_CD_L       AS CUST_CD_L             " & vbNewLine _
                                             & "      , CUST_CD_M       AS CUST_CD_M             " & vbNewLine _
                                             & "      , MIN(CUST_NM_L)  AS CUST_NM_L             " & vbNewLine _
                                             & "      , MIN(CUST_NM_M)  As CUST_NM_M             " & vbNewLine _
                                             & "     FROM                                        " & vbNewLine _
                                             & "       $LM_MST$..M_CUST M_CUST                     " & vbNewLine _
                                             & "     WHERE                                       " & vbNewLine _
                                             & "       M_CUST.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
                                             & "     GROUP BY                                    " & vbNewLine _
                                             & "       CUST_CD_L, CUST_CD_M                      " & vbNewLine _
                                             & "   ) M_CUST                                      " & vbNewLine _
                                             & "  ON                                             " & vbNewLine _
                                             & "    MAIN.CUST_CD_L = M_CUST.CUST_CD_L            " & vbNewLine _
                                             & "  AND                                            " & vbNewLine _
                                             & "    MAIN.CUST_CD_M = M_CUST.CUST_CD_M            " & vbNewLine


#End Region 'SQL_LMC615専用                      

#Region "SQL LMC616専用"
    ''' <summary>
    ''' 印刷データ取得SQL（ヘッダ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FIRST_616 As String = "  SELECT                                                                     " & vbNewLine _
                                             & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                         " & vbNewLine _
                                             & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                         " & vbNewLine _
                                             & "         ELSE MR3.RPT_ID END                             AS RPT_ID           " & vbNewLine _
                                             & "  , MAIN.NRS_BR_CD                                       AS NRS_BR_CD        " & vbNewLine _
                                             & "  , M_NRS_BR.NRS_BR_NM                                   AS NRS_BR_NM        " & vbNewLine _
                                             & "  , M_CUST.CUST_NM_L                                     AS CUST_NM_L        " & vbNewLine _
                                             & "  , MAIN.OUTKA_DATE                                      AS OUTKA_DATE       " & vbNewLine _
                                             & "  --, CASE WHEN M_SOKO.WH_KB = '01' THEN M_SOKO.AD_1                         " & vbNewLine _
                                             & "  --       ELSE M_NRS_BR.AD_1 END                        AS NRS_AD_1         " & vbNewLine _
                                             & "  , M_SOKO.AD_1                                          AS NRS_AD_1         " & vbNewLine _
                                             & " -- , CASE WHEN M_SOKO.WH_KB = '01' THEN M_SOKO.AD_2                         " & vbNewLine _
                                             & " --        ELSE M_NRS_BR.AD_2 END                        AS NRS_AD_2         " & vbNewLine _
                                             & "  , M_SOKO.AD_2                                          AS NRS_AD_2         " & vbNewLine _
                                             & "  , M_CUST.CUST_NM_M                                     AS CUST_NM_M        " & vbNewLine _
                                             & "  , MAIN.CUST_ORD_NO                                     AS CUST_ORD_NO      " & vbNewLine _
                                             & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_NM                          " & vbNewLine _
                                             & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_NM                     " & vbNewLine _
                                             & "         ELSE M_DEST.DEST_NM                                                 " & vbNewLine _
                                             & "    END                                                  AS DEST_NM          " & vbNewLine _
                                             & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_AD_1                        " & vbNewLine _
                                             & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_AD_1                   " & vbNewLine _
                                             & "         ELSE M_DEST.AD_1                                                    " & vbNewLine _
                                             & "    END                                                  AS DEST_AD_1        " & vbNewLine _
                                             & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_AD_2                        " & vbNewLine _
                                             & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_AD_2                   " & vbNewLine _
                                             & "         ELSE M_DEST.AD_2                                                    " & vbNewLine _
                                             & "    END                                                  AS DEST_AD_2        " & vbNewLine _
                                             & "  , MAIN.CUST_CD_L                                       AS CUST_CD_L        " & vbNewLine _
                                             & "  , MAIN.CUST_CD_M                                       AS CUST_CD_M        " & vbNewLine _
                                             & "  , UNSOCO.UNSOCO_NM                                     AS UNSOCO_NM        " & vbNewLine _
                                             & "  , M_GOODS.GOODS_NM_1                                   AS GOODS_NM_1       " & vbNewLine _
                                             & "  , MAIN.LOT_NO                                          AS LOT_NO           " & vbNewLine _
                                             & "  , MAIN.IRIME                                           AS IRIME            " & vbNewLine _
                                             & "  , MAIN.ALCTD_NB                                        AS ALCTD_NB         " & vbNewLine _
                                             & "  , MAIN.ALCTD_QT                                        AS ALCTD_QT         " & vbNewLine _
                                             & "  , MAIN.IRIME_UT                                        AS IRIME_UT         " & vbNewLine _
                                             & "  , MAIN.OUTKA_NO_L                                      AS OUTKA_NO_L       " & vbNewLine _
                                             & "  , MAIN.REMARK                                          AS REMARK           " & vbNewLine _
                                             & "  --, MAIN.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT     " & vbNewLine _
                                             & "  , SUB.ZANSURYO                                         AS ALLOC_CAN_QT     " & vbNewLine _
                                             & "  , MAIN.ARR_PLAN_DATE                                   AS ARR_PLAN_DATE    " & vbNewLine _
                                             & "  , M_GOODS.GOODS_CD_NRS                                 AS GOODS_CD_NRS     " & vbNewLine _
                                             & "  --(2012.03.27)Notes№912 商品コード追加 -- START --                        " & vbNewLine _
                                             & "  , M_GOODS.GOODS_CD_CUST                                AS GOODS_CD_CUST    " & vbNewLine _
                                             & "  --(2012.03.27)Notes№912 商品コード追加 --  END  --                        " & vbNewLine _
                                             & "  , MAIN.SERIAL_NO                                       AS SERIAL_NO        " & vbNewLine _
                                             & "  , MAIN.KBN_NM2                                         AS DOKU_NM          " & vbNewLine _
                                             & "  ,@PRINT_DATE_FROM                                      AS PRINT_DATE_FROM  " & vbNewLine _
                                             & "  ,@PRINT_DATE_TO                                        AS PRINT_DATE_TO    " & vbNewLine




#End Region '印刷データ取得 LMC616専用

#Region "印刷データ取得 LMC619専用"



    ''' <summary>
    ''' 印刷データ取得SQL（ヘッダ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FIRST_619 As String = "  SELECT                                                                               " & vbNewLine _
                                                 & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                   " & vbNewLine _
                                                 & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                   " & vbNewLine _
                                                 & "         ELSE MR3.RPT_ID END                             AS RPT_ID                     " & vbNewLine _
                                                 & "  , MAIN.NRS_BR_CD                                       AS NRS_BR_CD                  " & vbNewLine _
                                                 & "  , M_NRS_BR.NRS_BR_NM                                   AS NRS_BR_NM                  " & vbNewLine _
                                                 & "  , M_CUST.CUST_NM_L                                     AS CUST_NM_L                  " & vbNewLine _
                                                 & "  , MAIN.OUTKA_DATE                                      AS OUTKA_DATE                 " & vbNewLine _
                                                 & "  , M_SOKO.AD_1                                          AS NRS_AD_1                   " & vbNewLine _
                                                 & "  , M_SOKO.AD_2                                          AS NRS_AD_2                   " & vbNewLine _
                                                 & "  , M_CUST.CUST_NM_M                                     AS CUST_NM_M                  " & vbNewLine _
                                                 & "  , MAIN.CUST_ORD_NO                                     AS CUST_ORD_NO                " & vbNewLine _
                                                 & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_NM                                    " & vbNewLine _
                                                 & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_NM                               " & vbNewLine _
                                                 & "         ELSE M_DEST.DEST_NM                                                           " & vbNewLine _
                                                 & "    END                                                  AS DEST_NM                    " & vbNewLine _
                                                 & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_AD_1                                  " & vbNewLine _
                                                 & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_AD_1                             " & vbNewLine _
                                                 & "         ELSE M_DEST.AD_1                                                              " & vbNewLine _
                                                 & "    END                                                  AS DEST_AD_1                  " & vbNewLine _
                                                 & "  , CASE WHEN MAIN.DEST_KB = '01' THEN MAIN.DEST_AD_2                                  " & vbNewLine _
                                                 & "         WHEN MAIN.DEST_KB = '02' THEN OUTKAEDIL.DEST_AD_2                             " & vbNewLine _
                                                 & "         ELSE M_DEST.AD_2                                                              " & vbNewLine _
                                                 & "    END                                                  AS DEST_AD_2                  " & vbNewLine _
                                                 & "  , MAIN.CUST_CD_L                                       AS CUST_CD_L                  " & vbNewLine _
                                                 & "  , MAIN.CUST_CD_M                                       AS CUST_CD_M                  " & vbNewLine _
                                                 & "  , UNSOCO.UNSOCO_NM                                     AS UNSOCO_NM                  " & vbNewLine _
                                                 & "  , M_GOODS.GOODS_NM_1                                   AS GOODS_NM_1                 " & vbNewLine _
                                                 & "  , MAIN.LOT_NO                                          AS LOT_NO                     " & vbNewLine _
                                                 & "  , MAIN.IRIME                                           AS IRIME                      " & vbNewLine _
                                                 & "  , MAIN.ALCTD_NB                                        AS ALCTD_NB                   " & vbNewLine _
                                                 & "  , MAIN.ALCTD_QT                                        AS ALCTD_QT                   " & vbNewLine _
                                                 & "  , MAIN.IRIME_UT                                        AS IRIME_UT                   " & vbNewLine _
                                                 & "  , MAIN.OUTKA_NO_L                                      AS OUTKA_NO_L                 " & vbNewLine _
                                                 & "  , MAIN.REMARK                                          AS REMARK                     " & vbNewLine _
                                                 & "  , ISNULL(SUB.ZANSURYO ,0)                              AS ALLOC_CAN_QT               " & vbNewLine _
                                                 & "  , MAIN.ARR_PLAN_DATE                                   AS ARR_PLAN_DATE              " & vbNewLine _
                                                 & "  , M_GOODS.GOODS_CD_NRS                                 AS GOODS_CD_NRS               " & vbNewLine _
                                                 & "  , M_GOODS.GOODS_CD_CUST                                AS GOODS_CD_CUST              " & vbNewLine _
                                                 & "  , MAIN.SERIAL_NO                                       AS SERIAL_NO                  " & vbNewLine _
                                                 & "  , MAIN.KBN_NM2                                         AS DOKU_NM                    " & vbNewLine _
                                                 & "  ,@PRINT_DATE_FROM                                      AS PRINT_DATE_FROM            " & vbNewLine _
                                                 & "  ,@PRINT_DATE_TO                                        AS PRINT_DATE_TO              " & vbNewLine





    ''' <summary>
    ''' LMC619FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ALL_FROM_619 As String = "  FROM                                                                                       " & vbNewLine _
                                         & "  --出荷                                                                                     " & vbNewLine _
                                         & "  (                                                                                          " & vbNewLine _
                                         & "    SELECT                                                                                   " & vbNewLine _
                                         & "      OUTKA.OUTKA_NO_L                                                    AS OUTKA_NO_L      " & vbNewLine _
                                         & "    , OUTKA.LOT_NO                                                        AS LOT_NO          " & vbNewLine _
                                         & "    , OUTKA.IRIME                                                         AS IRIME           " & vbNewLine _
                                         & "    , OUTKA.IRIME_UT                                                      AS IRIME_UT        " & vbNewLine _
                                         & "    , OUTKA.REMARK                                                        AS REMARK          " & vbNewLine _
                                         & "    , OUTKA.SERIAL_NO                                                     AS SERIAL_NO       " & vbNewLine _
                                         & "    , OUTKA.GOODS_CD_NRS                                                  AS GOODS_CD_NRS    " & vbNewLine _
                                         & "    , SUM(OUTKA.ALCTD_NB)                                                 AS ALCTD_NB        " & vbNewLine _
                                         & "    , SUM(OUTKA.ALCTD_QT)                                                 AS ALCTD_QT        " & vbNewLine _
                                         & "    , SUM(OUTKA.ALLOC_CAN_QT)                                             AS ALLOC_CAN_QT    " & vbNewLine _
                                         & "    , OUTKA.NRS_BR_CD                                                     AS NRS_BR_CD       " & vbNewLine _
                                         & "    , OUTKA.WH_CD                                                         AS WH_CD           " & vbNewLine _
                                         & "    , OUTKA.OUTKA_DATE                                                    AS OUTKA_DATE      " & vbNewLine _
                                         & "    , OUTKA.CUST_ORD_NO                                                   AS CUST_ORD_NO     " & vbNewLine _
                                         & "    , OUTKA.CUST_CD_L                                                     AS CUST_CD_L       " & vbNewLine _
                                         & "    , OUTKA.CUST_CD_M                                                     AS CUST_CD_M       " & vbNewLine _
                                         & "    , OUTKA.ARR_PLAN_DATE                                                 AS ARR_PLAN_DATE   " & vbNewLine _
                                         & "    , OUTKA.DEST_CD                                                       AS DEST_CD         " & vbNewLine _
                                         & "    , OUTKA.DEST_KB          AS DEST_KB                                          " & vbNewLine _
                                         & "    , OUTKA.DEST_NM          AS DEST_NM                                          " & vbNewLine _
                                         & "    , OUTKA.DEST_AD_1        AS DEST_AD_1                                        " & vbNewLine _
                                         & "    , OUTKA.DEST_AD_2        AS DEST_AD_2                                        " & vbNewLine _
                                         & "    , OUTKA.KBN_NM2          AS KBN_NM2                                          " & vbNewLine _
                                         & "    --20170427                                                       " & vbNewLine _
                                         & "    , OUTKA.GOODS_COND_KB_1   AS GOODS_COND_KB_1                                          " & vbNewLine _
                                         & "    , OUTKA.GOODS_COND_KB_2   AS GOODS_COND_KB_2                                        " & vbNewLine _
                                         & "    , OUTKA.GOODS_COND_KB_3   AS GOODS_COND_KB_3                                        " & vbNewLine _
                                         & "    --20170427                                                        " & vbNewLine _
                                         & "    FROM                                                                                     " & vbNewLine _
                                         & "       (                                                                                     " & vbNewLine _
                                         & "        SELECT                                                                               " & vbNewLine _
                                         & "            OUTKAL.OUTKA_NO_L                                           AS OUTKA_NO_L        " & vbNewLine _
                                         & "--          , OUTKAM.OUTKA_NO_M                                           AS OUTKA_NO_M        " & vbNewLine _
                                         & "          , OUTKAS.LOT_NO                                               AS LOT_NO            " & vbNewLine _
                                         & "          , OUTKAS.IRIME                                                AS IRIME             " & vbNewLine _
                                         & "          , OUTKAM.IRIME_UT                                             AS IRIME_UT          " & vbNewLine _
                                         & "          , OUTKAM.REMARK                                               AS REMARK            " & vbNewLine _
                                         & "          , OUTKAM.SERIAL_NO                                            AS SERIAL_NO         " & vbNewLine _
                                         & "          , OUTKAM.GOODS_CD_NRS                                         AS GOODS_CD_NRS      " & vbNewLine _
                                         & "          , SUM(OUTKAS.ALCTD_NB)                                        AS ALCTD_NB          " & vbNewLine _
                                         & "          , SUM(OUTKAS.ALCTD_QT)                                        AS ALCTD_QT          " & vbNewLine _
                                         & "          , OUTKAS.ZAI_REC_NO                                           AS ZAI_REC_NO        " & vbNewLine _
                                         & "          , MAX(ZAITRS.ALLOC_CAN_QT)                                    AS ALLOC_CAN_QT      " & vbNewLine _
                                         & "          , OUTKAL.NRS_BR_CD                                            AS NRS_BR_CD         " & vbNewLine _
                                         & "          , OUTKAL.WH_CD                                                AS WH_CD        " & vbNewLine _
                                         & "          , OUTKAL.OUTKA_DATE                                      AS OUTKA_DATE        " & vbNewLine _
                                         & "          , OUTKAL.CUST_ORD_NO                                          AS CUST_ORD_NO       " & vbNewLine _
                                         & "          , OUTKAL.CUST_CD_L                                            AS CUST_CD_L         " & vbNewLine _
                                         & "          , OUTKAL.CUST_CD_M                                            AS CUST_CD_M         " & vbNewLine _
                                         & "          , OUTKAL.ARR_PLAN_DATE                                        AS ARR_PLAN_DATE     " & vbNewLine _
                                         & "          , OUTKAL.DEST_CD                                              AS DEST_CD           " & vbNewLine _
                                         & "          , OUTKAL.DEST_KB          AS DEST_KB                                         " & vbNewLine _
                                         & "          , OUTKAL.DEST_NM          AS DEST_NM                                         " & vbNewLine _
                                         & "          , OUTKAL.DEST_AD_1        AS DEST_AD_1                                       " & vbNewLine _
                                         & "          , OUTKAL.DEST_AD_2        AS DEST_AD_2                                       " & vbNewLine _
                                         & "          , ZKBN.KBN_NM2            AS KBN_NM2                                         " & vbNewLine _
                                         & "          --20170427                                         " & vbNewLine _
                                         & "          , ZAITRS.GOODS_COND_KB_1   AS GOODS_COND_KB_1                                         " & vbNewLine _
                                         & "          , ZAITRS.GOODS_COND_KB_2   AS GOODS_COND_KB_2                                         " & vbNewLine _
                                         & "          , ZAITRS.GOODS_COND_KB_3   AS GOODS_COND_KB_3                                                    " & vbNewLine _
                                         & "          --20170427                                         " & vbNewLine _
                                         & "        FROM                                                                                 " & vbNewLine _
                                         & "        --出荷L                                                                              " & vbNewLine _
                                         & "              (                                                                              " & vbNewLine _
                                         & "                SELECT                                                                       " & vbNewLine _
                                         & "                    NRS_BR_CD        AS NRS_BR_CD                                            " & vbNewLine _
                                         & "                  , WH_CD            AS WH_CD                                                " & vbNewLine _
                                         & "                  , OUTKA_PLAN_DATE       AS OUTKA_DATE                                           " & vbNewLine _
                                         & "                  , CUST_ORD_NO      AS CUST_ORD_NO                                          " & vbNewLine _
                                         & "                  , CUST_CD_L        AS CUST_CD_L                                            " & vbNewLine _
                                         & "                  , CUST_CD_M        AS CUST_CD_M                                            " & vbNewLine _
                                         & "                  , OUTKA_NO_L       AS OUTKA_NO_L                                           " & vbNewLine _
                                         & "                  , ARR_PLAN_DATE    AS ARR_PLAN_DATE                                        " & vbNewLine _
                                         & "                  , DEST_CD          AS DEST_CD                                              " & vbNewLine _
                                         & "                  , DEST_KB          AS DEST_KB                                                " & vbNewLine _
                                         & "                  , DEST_NM          AS DEST_NM                                                " & vbNewLine _
                                         & "                  , DEST_AD_1        AS DEST_AD_1                                              " & vbNewLine _
                                         & "                  , DEST_AD_2        AS DEST_AD_2                                              " & vbNewLine _
                                         & "                  , SYS_DEL_FLG      AS SYS_DEL_FLG                                          " & vbNewLine _
                                         & "               FROM                                                                          " & vbNewLine _
                                         & "                   $LM_TRN$..C_OUTKA_L                                                         " & vbNewLine _
                                         & "               --商品コード以外の抽出条件                                                    " & vbNewLine _
                                         & "               $WHERE1$                                                                      " & vbNewLine _
                                         & "               --ここまで                                                                    " & vbNewLine _
                                         & "               )             OUTKAL                                                          " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "            $LM_TRN$..C_OUTKA_M AS OUTKAM                                                      " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "           OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "           OUTKAM.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "           OUTKAL.OUTKA_NO_L   = OUTKAM.OUTKA_NO_L                                           " & vbNewLine _
                                         & "          --商品ｺｰﾄﾞ条件はここ                                                               " & vbNewLine _
                                         & "          $WHERE2$                                                                           " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "            $LM_TRN$..C_OUTKA_S AS OUTKAS                                                      " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "            OUTKAS.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "            OUTKAM.OUTKA_NO_L   = OUTKAS.OUTKA_NO_L                                          " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "            OUTKAM.OUTKA_NO_M   = OUTKAS.OUTKA_NO_M                                          " & vbNewLine _
                                         & "          AND                                                                                " & vbNewLine _
                                         & "            OUTKAS.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "           $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                       " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "           ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                             " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZAITRS.CUST_CD_L = OUTKAl.CUST_CD_L                                               " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZAITRS.CUST_CD_M = OUTKAL.CUST_CD_M                                               " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "           $LM_MST$..M_GOODS AS GOODS                                                        " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "           GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                                " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                          " & vbNewLine _
                                         & "          LEFT JOIN                                                                          " & vbNewLine _
                                         & "           $LM_MST$..Z_KBN AS ZKBN                                                           " & vbNewLine _
                                         & "          ON                                                                                 " & vbNewLine _
                                         & "           GOODS.DOKU_KB = ZKBN.KBN_CD                                                       " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZKBN.KBN_GROUP_CD = 'G001'                                                        " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           ZKBN.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                         & "          WHERE                                                                              " & vbNewLine _
                                         & "           OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                         & "         AND                                                                                 " & vbNewLine _
                                         & "           OUTKAM.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                         & "          $WHERE3$                                                                           " & vbNewLine _
                                         & "         GROUP BY                                                                            " & vbNewLine _
                                         & "           OUTKAL.OUTKA_NO_L                                                                 " & vbNewLine _
                                         & "--         , OUTKAM.OUTKA_NO_M                                                                 " & vbNewLine _
                                         & "         , OUTKAS.ZAI_REC_NO                                                                 " & vbNewLine _
                                         & "         , OUTKAS.LOT_NO                                                                     " & vbNewLine _
                                         & "         , OUTKAS.IRIME                                                                      " & vbNewLine _
                                         & "         , OUTKAM.IRIME_UT                                                                   " & vbNewLine _
                                         & "         , OUTKAM.REMARK                                                                     " & vbNewLine _
                                         & "         , OUTKAM.SERIAL_NO                                                                  " & vbNewLine _
                                         & "         , OUTKAM.GOODS_CD_NRS                                                               " & vbNewLine _
                                         & "         , OUTKAL.NRS_BR_CD                                                                  " & vbNewLine _
                                         & "         , OUTKAL.WH_CD                                                                      " & vbNewLine _
                                         & "         , OUTKAL.OUTKA_DATE                                                                 " & vbNewLine _
                                         & "         , OUTKAL.CUST_ORD_NO                                                                " & vbNewLine _
                                         & "         , OUTKAL.CUST_CD_L                                                                  " & vbNewLine _
                                         & "         , OUTKAL.CUST_CD_M                                                                  " & vbNewLine _
                                         & "         , OUTKAL.ARR_PLAN_DATE                                                              " & vbNewLine _
                                         & "         , OUTKAL.DEST_CD                                                                    " & vbNewLine _
                                         & "         , OUTKAL.DEST_KB                                                             " & vbNewLine _
                                         & "         , OUTKAL.DEST_NM                                                             " & vbNewLine _
                                         & "         , OUTKAL.DEST_AD_1                                                           " & vbNewLine _
                                         & "         , OUTKAL.DEST_AD_2                                                           " & vbNewLine _
                                         & "         , ZKBN.KBN_NM2                                                               " & vbNewLine _
                                         & "         --20170427                                                               " & vbNewLine _
                                         & "         ,ZAITRS.GOODS_COND_KB_1                                                               " & vbNewLine _
                                         & "         ,ZAITRS.GOODS_COND_KB_2                                                               " & vbNewLine _
                                         & "         ,ZAITRS.GOODS_COND_KB_3                                                               " & vbNewLine _
                                         & "         --20170427                                                               " & vbNewLine _
                                         & "       ) OUTKA                                                                               " & vbNewLine _
                                         & "    GROUP BY                                                                                 " & vbNewLine _
                                         & "       OUTKA.OUTKA_NO_L                                                                      " & vbNewLine _
                                         & "     , OUTKA.GOODS_CD_NRS                                                                    " & vbNewLine _
                                         & "     , OUTKA.LOT_NO                                                                          " & vbNewLine _
                                         & "     , OUTKA.IRIME                                                                           " & vbNewLine _
                                         & "     , OUTKA.IRIME_UT                                                                        " & vbNewLine _
                                         & "     , OUTKA.REMARK                                                                          " & vbNewLine _
                                         & "     , OUTKA.SERIAL_NO                                                                       " & vbNewLine _
                                         & "     , OUTKA.NRS_BR_CD                                                                       " & vbNewLine _
                                         & "     , OUTKA.WH_CD                                                                           " & vbNewLine _
                                         & "     , OUTKA.OUTKA_DATE                                                                      " & vbNewLine _
                                         & "     , OUTKA.CUST_ORD_NO                                                                     " & vbNewLine _
                                         & "     , OUTKA.CUST_CD_L                                                                       " & vbNewLine _
                                         & "     , OUTKA.CUST_CD_M                                                                       " & vbNewLine _
                                         & "     , OUTKA.ARR_PLAN_DATE                                                                   " & vbNewLine _
                                         & "     , OUTKA.DEST_CD                                                                         " & vbNewLine _
                                         & "     , OUTKA.DEST_KB                                                              " & vbNewLine _
                                         & "     , OUTKA.DEST_NM                                                              " & vbNewLine _
                                         & "     , OUTKA.DEST_AD_1                                                            " & vbNewLine _
                                         & "     , OUTKA.DEST_AD_2                                                            " & vbNewLine _
                                         & "     , OUTKA.KBN_NM2                                                              " & vbNewLine _
                                         & "     --20170427                                                              " & vbNewLine _
                                         & "     , OUTKA.GOODS_COND_KB_1                                                              " & vbNewLine _
                                         & "     , OUTKA.GOODS_COND_KB_2                                                              " & vbNewLine _
                                         & "     , OUTKA.GOODS_COND_KB_3                                                              " & vbNewLine _
                                         & "     --20170427                                                              " & vbNewLine _
                                         & "  ) MAIN                                                                                     " & vbNewLine _
                                         & "  --商品ﾏｽﾀ(出荷)                                                                            " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_GOODS M_GOODS                                                                   " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   M_GOODS.NRS_BR_CD   = MAIN.NRS_BR_CD                                                          " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MAIN.GOODS_CD_NRS   = M_GOODS.GOODS_CD_NRS                                                " & vbNewLine _
                                         & "  --出荷Lでの荷主帳票ﾊﾟﾀｰﾝ取得                                                               " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_CUST_RPT MCR1                                                                   " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MCR1.NRS_BR_CD = MAIN.NRS_BR_CD                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR1.CUST_CD_L = MAIN.CUST_CD_L                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR1.CUST_CD_M = MAIN.CUST_CD_M                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR1.CUST_CD_S = '00'                                                                     " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR1.PTN_ID    = @PTN_ID                                                                     " & vbNewLine _
                                         & "  --帳票ﾊﾟﾀｰﾝ取得                                                                            " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_RPT MR1                                                                         " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MR1.NRS_BR_CD = MAIN.NRS_BR_CD                                                                " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR1.PTN_ID    = MCR1.PTN_ID                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR1.PTN_CD    = MCR1.PTN_CD                                                               " & vbNewLine _
                                         & "  AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                         & "  --商品Mの荷主での荷主帳票ﾊﾟﾀｰﾝ取得                                                         " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_CUST_RPT MCR2                                                                   " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MCR2.NRS_BR_CD  = MAIN.NRS_BR_CD                                                              " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   M_GOODS.CUST_CD_L = MCR2.CUST_CD_L                                                        " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   M_GOODS.CUST_CD_M = MCR2.CUST_CD_M                                                        " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   M_GOODS.CUST_CD_S = MCR2.CUST_CD_S                                                        " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MCR2.PTN_ID           = @PTN_ID                                                             " & vbNewLine _
                                         & "  --帳票ﾊﾟﾀｰﾝ取得                                                                            " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_RPT MR2                                                                         " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MR2.NRS_BR_CD = MAIN.NRS_BR_CD                                                                " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR2.PTN_ID    = MCR2.PTN_ID                                                               " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR2.PTN_CD    = MCR2.PTN_CD                                                               " & vbNewLine _
                                         & "  AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                         & "  --存在しない場合の帳票ﾊﾟﾀｰﾝ取得                                                            " & vbNewLine _
                                         & "  LEFT JOIN                                                                                  " & vbNewLine _
                                         & "   $LM_MST$..M_RPT MR3                                                                         " & vbNewLine _
                                         & "  ON                                                                                         " & vbNewLine _
                                         & "   MR3.NRS_BR_CD     = MAIN.NRS_BR_CD                                                            " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR3.PTN_ID        = @PTN_ID                                                                  " & vbNewLine _
                                         & "  AND                                                                                        " & vbNewLine _
                                         & "   MR3.STANDARD_FLAG = '01'                                                                  " & vbNewLine _
                                         & "   AND MR3.SYS_DEL_FLG = '0'                                                                 " & vbNewLine

    Private Const SQL_CUST_S_FROM_619 As String = " FROM                                                                                     " & vbNewLine _
                                            & "      --出荷                                                                              " & vbNewLine _
                                            & "      (                                                                                   " & vbNewLine _
                                            & "        SELECT                                                                            " & vbNewLine _
                                            & "               OUTKA.OUTKA_NO_L         AS OUTKA_NO_L                                     " & vbNewLine _
                                            & "             , OUTKA.LOT_NO             AS LOT_NO                                         " & vbNewLine _
                                            & "             , OUTKA.IRIME              AS IRIME                                          " & vbNewLine _
                                            & "             , OUTKA.IRIME_UT           AS IRIME_UT                                       " & vbNewLine _
                                            & "             , OUTKA.REMARK             AS REMARK                                         " & vbNewLine _
                                            & "             , OUTKA.SERIAL_NO          AS SERIAL_NO                                      " & vbNewLine _
                                            & "             , OUTKA.GOODS_CD_NRS       AS GOODS_CD_NRS                                   " & vbNewLine _
                                            & "             , SUM(OUTKA.ALCTD_NB)      AS ALCTD_NB                                       " & vbNewLine _
                                            & "             , SUM(OUTKA.ALCTD_QT)      AS ALCTD_QT                                       " & vbNewLine _
                                            & "             , SUM(OUTKA.ALLOC_CAN_QT)  AS ALLOC_CAN_QT                                   " & vbNewLine _
                                            & "             , OUTKA.NRS_BR_CD          AS NRS_BR_CD                                      " & vbNewLine _
                                            & "             , OUTKA.WH_CD              AS WH_CD                                          " & vbNewLine _
                                            & "             , OUTKA.OUTKA_DATE         AS OUTKA_DATE                                     " & vbNewLine _
                                            & "             , OUTKA.CUST_ORD_NO        AS CUST_ORD_NO                                    " & vbNewLine _
                                            & "             , OUTKA.CUST_CD_L          AS CUST_CD_L                                      " & vbNewLine _
                                            & "             , OUTKA.CUST_CD_M          AS CUST_CD_M                                      " & vbNewLine _
                                            & "             , OUTKA.ARR_PLAN_DATE      AS ARR_PLAN_DATE                                  " & vbNewLine _
                                            & "             , OUTKA.DEST_CD            AS DEST_CD                                        " & vbNewLine _
                                            & "             , OUTKA.DEST_KB            AS DEST_KB                                        " & vbNewLine _
                                            & "             , OUTKA.DEST_NM            AS DEST_NM                                        " & vbNewLine _
                                            & "             , OUTKA.DEST_AD_1          AS DEST_AD_1                                      " & vbNewLine _
                                            & "             , OUTKA.DEST_AD_2          AS DEST_AD_2                                      " & vbNewLine _
                                            & "             , OUTKA.KBN_NM2            AS KBN_NM2                                        " & vbNewLine _
                                            & "              --20170427                                                       " & vbNewLine _
                                            & "             , OUTKA.GOODS_COND_KB_1   AS GOODS_COND_KB_1                                          " & vbNewLine _
                                            & "             , OUTKA.GOODS_COND_KB_2   AS GOODS_COND_KB_2                                        " & vbNewLine _
                                            & "             , OUTKA.GOODS_COND_KB_3   AS GOODS_COND_KB_3                                        " & vbNewLine _
                                            & "              --20170427                                                        " & vbNewLine _
                                            & "          FROM (                                                                          " & vbNewLine _
                                            & "                 SELECT                                                                   " & vbNewLine _
                                            & "                        OUTKAL.OUTKA_NO_L        AS OUTKA_NO_L                            " & vbNewLine _
                                            & "                      , OUTKAM.OUTKA_NO_M        AS OUTKA_NO_M                            " & vbNewLine _
                                            & "                      , OUTKAS.LOT_NO            AS LOT_NO                                " & vbNewLine _
                                            & "                      , OUTKAS.IRIME             AS IRIME                                 " & vbNewLine _
                                            & "                      , OUTKAM.IRIME_UT          AS IRIME_UT                              " & vbNewLine _
                                            & "                      , OUTKAM.REMARK            AS REMARK                                " & vbNewLine _
                                            & "                      , OUTKAM.SERIAL_NO         AS SERIAL_NO                             " & vbNewLine _
                                            & "                      , OUTKAM.GOODS_CD_NRS      AS GOODS_CD_NRS                          " & vbNewLine _
                                            & "                      , SUM(OUTKAS.ALCTD_NB)     AS ALCTD_NB                              " & vbNewLine _
                                            & "                      , SUM(OUTKAS.ALCTD_QT)     AS ALCTD_QT                              " & vbNewLine _
                                            & "                      , OUTKAS.ZAI_REC_NO        AS ZAI_REC_NO                            " & vbNewLine _
                                            & "                      , MAX(ZAITRS.ALLOC_CAN_QT) AS ALLOC_CAN_QT                          " & vbNewLine _
                                            & "                      , OUTKAL.NRS_BR_CD         AS NRS_BR_CD                             " & vbNewLine _
                                            & "                      , OUTKAL.WH_CD             AS WH_CD                                 " & vbNewLine _
                                            & "                      , OUTKAL.OUTKA_DATE        AS OUTKA_DATE                            " & vbNewLine _
                                            & "                      , OUTKAL.CUST_ORD_NO       AS CUST_ORD_NO                           " & vbNewLine _
                                            & "                      , OUTKAL.CUST_CD_L         AS CUST_CD_L                             " & vbNewLine _
                                            & "                      , OUTKAL.CUST_CD_M         AS CUST_CD_M                             " & vbNewLine _
                                            & "                      , OUTKAL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                         " & vbNewLine _
                                            & "                      , OUTKAL.DEST_CD           AS DEST_CD                               " & vbNewLine _
                                            & "                      , OUTKAL.DEST_KB           AS DEST_KB                               " & vbNewLine _
                                            & "                      , OUTKAL.DEST_NM           AS DEST_NM                               " & vbNewLine _
                                            & "                      , OUTKAL.DEST_AD_1         AS DEST_AD_1                             " & vbNewLine _
                                            & "                      , OUTKAL.DEST_AD_2         AS DEST_AD_2                             " & vbNewLine _
                                            & "                      , ZKBN.KBN_NM2             AS KBN_NM2                               " & vbNewLine _
                                            & "                      --20170427                                         " & vbNewLine _
                                            & "                      , ZAITRS.GOODS_COND_KB_1   AS GOODS_COND_KB_1                                         " & vbNewLine _
                                            & "                      , ZAITRS.GOODS_COND_KB_2   AS GOODS_COND_KB_2                                         " & vbNewLine _
                                            & "                      , ZAITRS.GOODS_COND_KB_3   AS GOODS_COND_KB_3                                                     " & vbNewLine _
                                            & "                      --20170427                                         " & vbNewLine _
                                            & "                   FROM                                                                   " & vbNewLine _
                                            & "                        --出荷L                                                           " & vbNewLine _
                                            & "                        (                                                                 " & vbNewLine _
                                            & "                         SELECT                                                           " & vbNewLine _
                                            & "                                NRS_BR_CD           AS NRS_BR_CD                          " & vbNewLine _
                                            & "                              , WH_CD               AS WH_CD                              " & vbNewLine _
                                            & "                              , OUTKA_PLAN_DATE     AS OUTKA_DATE                         " & vbNewLine _
                                            & "                              , CUST_ORD_NO         AS CUST_ORD_NO                        " & vbNewLine _
                                            & "                              , CUST_CD_L           AS CUST_CD_L                          " & vbNewLine _
                                            & "                              , CUST_CD_M           AS CUST_CD_M                          " & vbNewLine _
                                            & "                              , OUTKA_NO_L          AS OUTKA_NO_L                         " & vbNewLine _
                                            & "                              , ARR_PLAN_DATE       AS ARR_PLAN_DATE                      " & vbNewLine _
                                            & "                              , DEST_CD             AS DEST_CD                            " & vbNewLine _
                                            & "                              , DEST_KB             AS DEST_KB                            " & vbNewLine _
                                            & "                              , DEST_NM             AS DEST_NM                            " & vbNewLine _
                                            & "                              , DEST_AD_1           AS DEST_AD_1                          " & vbNewLine _
                                            & "                              , DEST_AD_2           AS DEST_AD_2                          " & vbNewLine _
                                            & "                              , SYS_DEL_FLG         AS SYS_DEL_FLG                        " & vbNewLine _
                                            & "                           FROM $LM_TRN$..C_OUTKA_L                                       " & vbNewLine _
                                            & "                                --商品コード以外の抽出条件                                " & vbNewLine _
                                            & "                                $WHERE1$                                                  " & vbNewLine _
                                            & "                                --ここまで                                                " & vbNewLine _
                                            & "                         ) OUTKAL                                                         " & vbNewLine _
                                            & "                                LEFT JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM                   " & vbNewLine _
                                            & "                                       ON OUTKAM.NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
                                            & "                                      AND OUTKAM.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                            & "                                      AND OUTKAL.OUTKA_NO_L   = OUTKAM.OUTKA_NO_L         " & vbNewLine _
                                            & "                                --商品ｺｰﾄﾞ条件はここ                                      " & vbNewLine _
                                            & "                                $WHERE2$                                                  " & vbNewLine _
                                            & "                                LEFT JOIN $LM_TRN$..C_OUTKA_S AS OUTKAS                   " & vbNewLine _
                                            & "                                       ON OUTKAS.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                            & "                                      AND OUTKAM.OUTKA_NO_L  = OUTKAS.OUTKA_NO_L          " & vbNewLine _
                                            & "                                      AND OUTKAM.OUTKA_NO_M  = OUTKAS.OUTKA_NO_M          " & vbNewLine _
                                            & "                                      AND OUTKAS.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                            & "                                LEFT JOIN $LM_TRN$..D_ZAI_TRS AS ZAITRS                   " & vbNewLine _
                                            & "                                       ON ZAITRS.NRS_BR_CD  = @NRS_BR_CD                  " & vbNewLine _
                                            & "                                      AND ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO           " & vbNewLine _
                                            & "                                      AND ZAITRS.CUST_CD_L  = OUTKAl.CUST_CD_L            " & vbNewLine _
                                            & "                                      AND ZAITRS.CUST_CD_M  = OUTKAL.CUST_CD_M            " & vbNewLine _
                                            & "                                LEFT JOIN $LM_MST$..M_GOODS AS GOODS                      " & vbNewLine _
                                            & "                                       ON GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD              " & vbNewLine _
                                            & "                                      AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS        " & vbNewLine _
                                            & "                                LEFT JOIN $LM_MST$..Z_KBN AS ZKBN                         " & vbNewLine _
                                            & "                                       ON GOODS.DOKU_KB = ZKBN.KBN_CD                     " & vbNewLine _
                                            & "                                      AND ZKBN.KBN_GROUP_CD = 'G001'                      " & vbNewLine _
                                            & "                                      AND ZKBN.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                            & "                  WHERE OUTKAM.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                            & "                    AND OUTKAM.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                            & "                    AND GOODS.CUST_CD_L = @CUST_CD_L                                      " & vbNewLine _
                                            & "                    AND GOODS.CUST_CD_M = @CUST_CD_M                                      " & vbNewLine _
                                            & "                    AND GOODS.CUST_CD_S = @CUST_CD_S                                      " & vbNewLine _
                                            & "                  $WHERE3$                                                                " & vbNewLine _
                                            & "                  GROUP BY                                                                " & vbNewLine _
                                            & "                        OUTKAL.OUTKA_NO_L                                                 " & vbNewLine _
                                            & "                      , OUTKAM.OUTKA_NO_M                                                 " & vbNewLine _
                                            & "                      , OUTKAS.ZAI_REC_NO                                                 " & vbNewLine _
                                            & "                      , OUTKAS.LOT_NO                                                     " & vbNewLine _
                                            & "                      , OUTKAS.IRIME                                                      " & vbNewLine _
                                            & "                      , OUTKAM.IRIME_UT                                                   " & vbNewLine _
                                            & "                      , OUTKAM.REMARK                                                     " & vbNewLine _
                                            & "                      , OUTKAM.SERIAL_NO                                                  " & vbNewLine _
                                            & "                      , OUTKAM.GOODS_CD_NRS                                               " & vbNewLine _
                                            & "                      , OUTKAL.NRS_BR_CD                                                  " & vbNewLine _
                                            & "                      , OUTKAL.WH_CD                                                      " & vbNewLine _
                                            & "                      , OUTKAL.OUTKA_DATE                                                 " & vbNewLine _
                                            & "                      , OUTKAL.CUST_ORD_NO                                                " & vbNewLine _
                                            & "                      , OUTKAL.CUST_CD_L                                                  " & vbNewLine _
                                            & "                      , OUTKAL.CUST_CD_M                                                  " & vbNewLine _
                                            & "                      , OUTKAL.ARR_PLAN_DATE                                              " & vbNewLine _
                                            & "                      , OUTKAL.DEST_CD                                                    " & vbNewLine _
                                            & "                      , OUTKAL.DEST_KB                                                    " & vbNewLine _
                                            & "                      , OUTKAL.DEST_NM                                                    " & vbNewLine _
                                            & "                      , OUTKAL.DEST_AD_1                                                  " & vbNewLine _
                                            & "                      , OUTKAL.DEST_AD_2                                                  " & vbNewLine _
                                            & "                      , ZKBN.KBN_NM2                                                      " & vbNewLine _
                                            & "                      --20170427                                                               " & vbNewLine _
                                            & "                      ,ZAITRS.GOODS_COND_KB_1                                                               " & vbNewLine _
                                            & "                      ,ZAITRS.GOODS_COND_KB_2                                                               " & vbNewLine _
                                            & "                      ,ZAITRS.GOODS_COND_KB_3                                                               " & vbNewLine _
                                            & "         --20170427                                                               " & vbNewLine _
                                            & "               ) OUTKA                                                                    " & vbNewLine _
                                            & "        GROUP BY                                                                          " & vbNewLine _
                                            & "              OUTKA.OUTKA_NO_L                                                            " & vbNewLine _
                                            & "            , OUTKA.GOODS_CD_NRS                                                          " & vbNewLine _
                                            & "            , OUTKA.LOT_NO                                                                " & vbNewLine _
                                            & "            , OUTKA.IRIME                                                                 " & vbNewLine _
                                            & "            , OUTKA.IRIME_UT                                                              " & vbNewLine _
                                            & "            , OUTKA.REMARK                                                                " & vbNewLine _
                                            & "            , OUTKA.SERIAL_NO                                                             " & vbNewLine _
                                            & "            , OUTKA.NRS_BR_CD                                                             " & vbNewLine _
                                            & "            , OUTKA.WH_CD                                                                 " & vbNewLine _
                                            & "            , OUTKA.OUTKA_DATE                                                            " & vbNewLine _
                                            & "            , OUTKA.CUST_ORD_NO                                                           " & vbNewLine _
                                            & "            , OUTKA.CUST_CD_L                                                             " & vbNewLine _
                                            & "            , OUTKA.CUST_CD_M                                                             " & vbNewLine _
                                            & "            , OUTKA.ARR_PLAN_DATE                                                         " & vbNewLine _
                                            & "            , OUTKA.DEST_CD                                                               " & vbNewLine _
                                            & "            , OUTKA.DEST_KB                                                               " & vbNewLine _
                                            & "            , OUTKA.DEST_NM                                                               " & vbNewLine _
                                            & "            , OUTKA.DEST_AD_1                                                             " & vbNewLine _
                                            & "            , OUTKA.DEST_AD_2                                                             " & vbNewLine _
                                            & "            , OUTKA.KBN_NM2                                                               " & vbNewLine _
                                            & "            --20170427                                                              " & vbNewLine _
                                            & "            , OUTKA.GOODS_COND_KB_1                                                              " & vbNewLine _
                                            & "            , OUTKA.GOODS_COND_KB_2                                                              " & vbNewLine _
                                            & "            , OUTKA.GOODS_COND_KB_3                                                              " & vbNewLine _
                                            & "            --20170427                                                              " & vbNewLine _
                                            & "   ) MAIN                                                                                 " & vbNewLine _
                                            & "     --商品ﾏｽﾀ(出荷)                                                                      " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_GOODS M_GOODS                                                  " & vbNewLine _
                                            & "            ON M_GOODS.NRS_BR_CD   = MAIN.NRS_BR_CD                                       " & vbNewLine _
                                            & "           AND MAIN.GOODS_CD_NRS   = M_GOODS.GOODS_CD_NRS                                 " & vbNewLine _
                                            & "     --出荷Lでの荷主帳票ﾊﾟﾀｰﾝ取得                                                         " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                  " & vbNewLine _
                                            & "            ON MCR1.NRS_BR_CD = MAIN.NRS_BR_CD                                            " & vbNewLine _
                                            & "           AND MCR1.CUST_CD_L = MAIN.CUST_CD_L                                            " & vbNewLine _
                                            & "           AND MCR1.CUST_CD_M = MAIN.CUST_CD_M                                            " & vbNewLine _
                                            & "           AND MCR1.CUST_CD_S = '00'                                                      " & vbNewLine _
                                            & "           AND MCR1.PTN_ID    = @PTN_ID                                                   " & vbNewLine _
                                            & "     --帳票ﾊﾟﾀｰﾝ取得                                                                      " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_RPT MR1                                                        " & vbNewLine _
                                            & "            ON MR1.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                            & "           AND MR1.PTN_ID    = MCR1.PTN_ID                                                " & vbNewLine _
                                            & "           AND MR1.PTN_CD    = MCR1.PTN_CD                                                " & vbNewLine _
                                            & "           AND MR1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                            & "     --商品Mの荷主での荷主帳票ﾊﾟﾀｰﾝ取得                                                   " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                  " & vbNewLine _
                                            & "            ON MCR2.NRS_BR_CD  = MAIN.NRS_BR_CD                                           " & vbNewLine _
                                            & "           AND M_GOODS.CUST_CD_L = MCR2.CUST_CD_L                                         " & vbNewLine _
                                            & "           AND M_GOODS.CUST_CD_M = MCR2.CUST_CD_M                                         " & vbNewLine _
                                            & "           AND M_GOODS.CUST_CD_S = MCR2.CUST_CD_S                                         " & vbNewLine _
                                            & "           AND MCR2.PTN_ID       = @PTN_ID                                                " & vbNewLine _
                                            & "     --帳票ﾊﾟﾀｰﾝ取得                                                                      " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_RPT MR2                                                        " & vbNewLine _
                                            & "            ON MR2.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                            & "           AND MR2.PTN_ID    = MCR2.PTN_ID                                                " & vbNewLine _
                                            & "           AND MR2.PTN_CD    = MCR2.PTN_CD                                                " & vbNewLine _
                                            & "           AND MR2.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                            & "     --存在しない場合の帳票ﾊﾟﾀｰﾝ取得                                                      " & vbNewLine _
                                            & "     LEFT JOIN $LM_MST$..M_RPT MR3                                                        " & vbNewLine _
                                            & "            ON MR3.NRS_BR_CD = MAIN.NRS_BR_CD                                             " & vbNewLine _
                                            & "           AND MR3.PTN_ID    = @PTN_ID                                                    " & vbNewLine _
                                            & "           AND MR3.STANDARD_FLAG = '01'                                                   " & vbNewLine _
                                            & "           AND MR3.SYS_DEL_FLG = '0'                                                      " & vbNewLine




    ''' <summary>
    ''' 印刷データ取得時、追加FROM句(LMC619)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ADD_FROM_619 As String = "  --EDI出荷L                                     " & vbNewLine _
                                         & "	LEFT JOIN 				                                 " & vbNewLine _
                                         & "	(SELECT NRS_BR_CD 				                         " & vbNewLine _
                                         & "	,OUTKA_CTL_NO 				                             " & vbNewLine _
                                         & "	,MIN(DEST_NM)     AS DEST_NM 				                                 " & vbNewLine _
                                         & "	,MIN(DEST_AD_1)   AS DEST_AD_1				                                 " & vbNewLine _
                                         & "	,MIN(DEST_AD_2)   AS DEST_AD_2       				                         " & vbNewLine _
                                         & "	,SYS_DEL_FLG				                             " & vbNewLine _
                                         & "	FROM $LM_TRN$..H_OUTKAEDI_L 				             " & vbNewLine _
                                         & "	--AS EDI				                                 " & vbNewLine _
                                         & "    --(2013.01.25)ﾊﾟﾌｫｰﾏﾝｽ向上の為、条件追加 -- START --     " & vbNewLine _
                                         & "    WHERE NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                         & "      AND CUST_CD_L = @CUST_CD_L                             " & vbNewLine _
                                         & "      AND CUST_CD_M = @CUST_CD_M                             " & vbNewLine _
                                         & "    --(2013.01.25)ﾊﾟﾌｫｰﾏﾝｽ向上の為、条件追加 --  END  --     " & vbNewLine _
                                         & "	GROUP BY  				                                 " & vbNewLine _
                                         & "	 OUTKA_CTL_NO				                             " & vbNewLine _
                                         & "	,SYS_DEL_FLG				                             " & vbNewLine _
                                         & "	,NRS_BR_CD ) OUTKAEDIL     				                 " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    OUTKAEDIL.NRS_BR_CD    = MAIN.NRS_BR_CD          " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    OUTKAEDIL.OUTKA_CTL_NO = MAIN.OUTKA_NO_L     " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    OUTKAEDIL.SYS_DEL_FLG       = '0'                 " & vbNewLine _
                                         & "  --届先M                                        " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                " & vbNewLine _
                                         & "    $LM_MST$..M_DEST AS M_DEST                     " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    M_DEST.NRS_BR_CD   = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    M_DEST.CUST_CD_L   =  MAIN.CUST_CD_L             " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    MAIN.DEST_CD       = M_DEST.DEST_CD          " & vbNewLine _
                                         & "  --運送L                                        " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                " & vbNewLine _
                                         & "    $LM_TRN$..F_UNSO_L AS UNSOL                    " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    UNSOL.NRS_BR_CD    = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.INOUTKA_NO_L = MAIN.OUTKA_NO_L         " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.SYS_DEL_FLG  = '0'                     " & vbNewLine _
                                         & "  --運送会社M                                    " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                " & vbNewLine _
                                         & "    $LM_MST$..M_UNSOCO AS UNSOCO                   " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    UNSOCO.NRS_BR_CD   = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.UNSO_CD      = UNSOCO.UNSOCO_CD        " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    UNSOL.UNSO_BR_CD   = UNSOCO.UNSOCO_BR_CD     " & vbNewLine _
                                         & "  --営業所M                                      " & vbNewLine _
                                         & "  LEFT JOIN                                      " & vbNewLine _
                                         & "    $LM_MST$..M_NRS_BR M_NRS_BR                    " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    M_NRS_BR.NRS_BR_CD = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & "  --倉庫M                                        " & vbNewLine _
                                         & "  LEFT JOIN                                      " & vbNewLine _
                                         & "    $LM_MST$..M_SOKO M_SOKO                      " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    M_SOKO.WH_CD = MAIN.WH_CD                    " & vbNewLine _
                                         & "  --荷主M                                        " & vbNewLine _
                                         & "  LEFT JOIN                                      " & vbNewLine _
                                         & "   (                                             " & vbNewLine _
                                         & "     SELECT                                      " & vbNewLine _
                                         & "        CUST_CD_L       AS CUST_CD_L             " & vbNewLine _
                                         & "      , CUST_CD_M       AS CUST_CD_M             " & vbNewLine _
                                         & "      , MIN(CUST_NM_L)  AS CUST_NM_L             " & vbNewLine _
                                         & "      , MIN(CUST_NM_M)  As CUST_NM_M             " & vbNewLine _
                                         & "     FROM                                        " & vbNewLine _
                                         & "       $LM_MST$..M_CUST M_CUST                     " & vbNewLine _
                                         & "     WHERE                                       " & vbNewLine _
                                         & "       M_CUST.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
                                         & "     GROUP BY                                    " & vbNewLine _
                                         & "       CUST_CD_L, CUST_CD_M                      " & vbNewLine _
                                         & "   ) M_CUST                                      " & vbNewLine _
                                         & "  ON                                             " & vbNewLine _
                                         & "    MAIN.CUST_CD_L = M_CUST.CUST_CD_L            " & vbNewLine _
                                         & "  AND                                            " & vbNewLine _
                                         & "    MAIN.CUST_CD_M = M_CUST.CUST_CD_M            " & vbNewLine _
                                         & "LEFT JOIN                                        " & vbNewLine _
                                         & "	(                                      " & vbNewLine _
                                         & "  ----20170412 在庫証明書より移行----------------------------------------------------------------------- " & vbNewLine _
                                         & "  SELECT                                                                                                                                              " & vbNewLine _
                                         & "  ZAITRS.NRS_BR_CD                AS NRS_BR_CD                                                                                                       " & vbNewLine _
                                         & "  ,BASE10.LOT_NO AS LOT_NO                                                                                                                                                     " & vbNewLine _
                                         & "  ,SOKO.WH_CD AS WH_CD                                                                                                                                " & vbNewLine _
                                         & "  ,BASE10.GOODS_CD_NRS                                                                                                                     " & vbNewLine _
                                         & "  ,SUM(BASE10.PORA_ZAI_QT) AS ZANSURYO                                                                                                                       " & vbNewLine _
                                         & "  ,ZAITRS.IRIME AS IRIME_NB                                                                                                                           " & vbNewLine _
                                         & "  --20170427                                                                                                                           " & vbNewLine _
                                         & "  ,ZAITRS.GOODS_COND_KB_1 AS GOODS_COND_KB_1                                                                                                                          " & vbNewLine _
                                         & "  ,ZAITRS.GOODS_COND_KB_2 AS GOODS_COND_KB_2                                                                                                                           " & vbNewLine _
                                         & "  ,ZAITRS.GOODS_COND_KB_3 AS GOODS_COND_KB_3                                                                                                                          " & vbNewLine _
                                         & "  --20170427                                                                                                                           " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  (                                                                                                                                                   " & vbNewLine _
                                         & "  SELECT                                                                                                                                              " & vbNewLine _
                                         & "  *                                                                                                                                                   " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  (SELECT                                                                                                                                             " & vbNewLine _
                                         & "  CUST_CD_L                AS CUST_CD_L                                                                                                               " & vbNewLine _
                                         & "  ,CUST_CD_M                AS CUST_CD_M                                                                                                              " & vbNewLine _
                                         & "  ,CUST_NM_L                AS CUST_NM_L                                                                                                              " & vbNewLine _
                                         & "  ,ZAI_REC_NO               AS ZAI_REC_NO                                                                                                             " & vbNewLine _
                                         & "  ,GOODS_CD_NRS             AS GOODS_CD_NRS                                                                                                           " & vbNewLine _
                                         & "  ,GOODS_CD_CUST          AS GOODS_CD_CUST                                                                                                            " & vbNewLine _
                                         & "  ,LOT_NO                   AS LOT_NO                                                                                                                 " & vbNewLine _
                                         & "  ,SERIAL_NO                AS SERIAL_NO                                                                                                              " & vbNewLine _
                                         & "  ,0                        AS HIKAKU_ZAI_NB                                                                                                          " & vbNewLine _
                                         & "  ,SUM(PORA_ZAI_NB)         AS PORA_ZAI_NB                                                                                                            " & vbNewLine _
                                         & "  ,0                        AS EXTRA_ZAI_NB                                                                                                           " & vbNewLine _
                                         & "  ,0                        AS HIKAKU_ZAI_QT                                                                                                          " & vbNewLine _
                                         & "  ,SUM(PORA_ZAI_QT)         AS PORA_ZAI_QT                                                                                                            " & vbNewLine _
                                         & "  ,0                        AS EXTRA_ZAI_QT                                                                                                           " & vbNewLine _
                                         & "  ,NRS_BR_CD                AS NRS_BR_CD                                                                                                              " & vbNewLine _
                                         & "  --月末在庫履歴(D_ZAI_ZAN_JITSU)                                                                                                                           " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  (SELECT                                                                                                                                             " & vbNewLine _
                                         & "  ''                                AS OUTKA_NO_L                                                                                                     " & vbNewLine _
                                         & "  ,''                                AS OUTKA_NO_M                                                                                                    " & vbNewLine _
                                         & "  ,''                                AS OUTKA_NO_S                                                                                                    " & vbNewLine _
                                         & "  ,ZAI2.CUST_CD_L                    AS CUST_CD_L                                                                                                     " & vbNewLine _
                                         & "  ,ZAI2.CUST_CD_M                    AS CUST_CD_M                                                                                                     " & vbNewLine _
                                         & "  ,''                                AS CUST_NM_L                                                                                                     " & vbNewLine _
                                         & "  ,ZAN.ZAI_REC_NO                    AS ZAI_REC_NO                                                                                                    " & vbNewLine _
                                         & "  ,ZAI2.GOODS_CD_NRS                 AS GOODS_CD_NRS                                                                                                  " & vbNewLine _
                                         & "  ,MG.GOODS_CD_CUST                  AS GOODS_CD_CUST                                                                                                 " & vbNewLine _
                                         & "  ,ISNULL(ZAI2.LOT_NO, '')           AS LOT_NO                                                                                                        " & vbNewLine _
                                         & "  ,ISNULL(ZAI2.SERIAL_NO, '')        AS SERIAL_NO                                                                                                     " & vbNewLine _
                                         & "  ,ZAN.PORA_ZAI_NB                   AS PORA_ZAI_NB                                                                                                   " & vbNewLine _
                                         & "  ,ZAN.PORA_ZAI_QT                   AS PORA_ZAI_QT                                                                                                   " & vbNewLine _
                                         & "  ,ZAN.NRS_BR_CD                     AS NRS_BR_CD                                                                                                     " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  $LM_TRN$..D_ZAI_ZAN_JITSU ZAN                                                                                                                         " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI2                                                                                                                    " & vbNewLine _
                                         & "  ON  ZAI2.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                         & "  AND ZAI2.NRS_BR_CD = ZAN.NRS_BR_CD                                                                                                                  " & vbNewLine _
                                         & "  AND ZAI2.ZAI_REC_NO = ZAN.ZAI_REC_NO                                                                                                                " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..B_INKA_L INL2                                                                                                                     " & vbNewLine _
                                         & "  ON  INL2.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                         & "  AND INL2.NRS_BR_CD = ZAI2.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  AND INL2.INKA_NO_L = ZAI2.INKA_NO_L                                                                                                                 " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                        " & vbNewLine _
                                         & "  ON  ZAI2.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                   " & vbNewLine _
                                         & "  AND ZAI2.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                             " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  WHERE                                                                                                                                               " & vbNewLine _
                                         & "  ZAN.SYS_DEL_FLG = '0'                                                                                                                               " & vbNewLine _
                                         & "  AND ZAN.NRS_BR_CD = @NRS_BR_CD                                                                                                                      " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND ZAI2.CUST_CD_L = @CUST_CD_L                                                                                                                     " & vbNewLine _
                                         & "  AND ZAI2.CUST_CD_M = @CUST_CD_M                                                                                                                     " & vbNewLine _
                                         & "  AND ZAN.RIREKI_DATE <= @PRINT_DATE_TO                                                                                                                  " & vbNewLine _
                                         & "  AND ZAN.RIREKI_DATE >= ZAN.RIREKI_DATE                                                             " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND INL2.INKA_STATE_KB !< '50'                                                                                                                      " & vbNewLine _
                                         & "  AND (ZAN.PORA_ZAI_NB <> 0 OR ZAN.PORA_ZAI_QT <> 0)                                                                                                  " & vbNewLine _
                                         & "  --入荷データ(B_INKA_S)                                                                                                                                   " & vbNewLine _
                                         & "  UNION ALL                                                                                                                                           " & vbNewLine _
                                         & "  SELECT                                                                                                                                              " & vbNewLine _
                                         & "  ''                                                        AS OUTKA_NO_L                                                                             " & vbNewLine _
                                         & "  ,''                                                        AS OUTKA_NO_M                                                                            " & vbNewLine _
                                         & "  ,''                                                        AS OUTKA_NO_S                                                                            " & vbNewLine _
                                         & "  ,INL1.CUST_CD_L                                            AS CUST_CD_L                                                                             " & vbNewLine _
                                         & "  ,INL1.CUST_CD_M                                            AS CUST_CD_M                                                                             " & vbNewLine _
                                         & "  ,''                                                        AS CUST_NM_L                                                                             " & vbNewLine _
                                         & "  ,INS1.ZAI_REC_NO                                           AS ZAI_REC_NO                                                                            " & vbNewLine _
                                         & "  ,INM1.GOODS_CD_NRS                                         AS GOODS_CD_NRS                                                                          " & vbNewLine _
                                         & "  ,MG1.GOODS_CD_CUST                     AS GOODS_CD_CUST                                                                                             " & vbNewLine _
                                         & "  ,ISNULL(INS1.LOT_NO, '')                                   AS LOT_NO                                                                                " & vbNewLine _
                                         & "  ,ISNULL(INS1.SERIAL_NO, '')                                AS SERIAL_NO                                                                             " & vbNewLine _
                                         & "  ,(INS1.KONSU * MG1.PKG_NB) + INS1.HASU                     AS PORA_ZAI_NB                                                                           " & vbNewLine _
                                         & "  ,((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME      AS PORA_ZAI_QT                                                                           " & vbNewLine _
                                         & "  ,INS1.NRS_BR_CD                     AS NRS_BR_CD                                                                                                    " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  $LM_TRN$..B_INKA_L INL1                                                                                                                               " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..B_INKA_M INM1                                                                                                                     " & vbNewLine _
                                         & "  ON  INM1.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                         & "  AND INM1.NRS_BR_CD = INL1.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  AND INM1.INKA_NO_L = INL1.INKA_NO_L                                                                                                                 " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..B_INKA_S INS1                                                                                                                     " & vbNewLine _
                                         & "  ON  INS1.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                         & "  AND INS1.NRS_BR_CD = INL1.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  AND INS1.INKA_NO_L = INL1.INKA_NO_L                                                                                                                 " & vbNewLine _
                                         & "  AND INS1.INKA_NO_M = INM1.INKA_NO_M                                                                                                                 " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_GOODS MG1                                                                                                                       " & vbNewLine _
                                         & "  ON  MG1.NRS_BR_CD = INL1.NRS_BR_CD                                                                                                                  " & vbNewLine _
                                         & "  AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS                                                                                                            " & vbNewLine _
                                         & "  WHERE                                                                                                                                               " & vbNewLine _
                                         & "  INL1.SYS_DEL_FLG = '0'                                                                                                                              " & vbNewLine _
                                         & "  AND INL1.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                         & "  AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')                                                                                     " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND INL1.CUST_CD_L = @CUST_CD_L                                                                                                                     " & vbNewLine _
                                         & "  AND INL1.CUST_CD_M = @CUST_CD_M                                                                                                                     " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND INL1.INKA_DATE <= @PRINT_DATE_TO                                                                                                                   " & vbNewLine _
                                         & "  AND INL1.INKA_DATE >=  INL1.INKA_DATE                                                               " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND (INL1.INKA_DATE > '00000000' OR INL1.INKA_STATE_KB < '50')                                                                                      " & vbNewLine _
                                         & "  --在庫移動分を加減算(D_IDO_TRS)                                                                                                                              " & vbNewLine _
                                         & "  --移動後                                                                                                                                               " & vbNewLine _
                                         & "  UNION ALL                                                                                                                                           " & vbNewLine _
                                         & "  SELECT                                                                                                                                              " & vbNewLine _
                                         & "  ''                                          AS OUTKA_NO_L                                                                                           " & vbNewLine _
                                         & "  ,''                                          AS OUTKA_NO_M                                                                                          " & vbNewLine _
                                         & "  ,''                                          AS OUTKA_NO_S                                                                                          " & vbNewLine _
                                         & "  ,ZAI3.CUST_CD_L                              AS CUST_CD_L                                                                                           " & vbNewLine _
                                         & "  ,ZAI3.CUST_CD_M                              AS CUST_CD_M                                                                                           " & vbNewLine _
                                         & "  ,''                                          AS CUST_NM_L                                                                                           " & vbNewLine _
                                         & "  ,IDO1.N_ZAI_REC_NO                           AS ZAI_REC_NO                                                                                          " & vbNewLine _
                                         & "  ,ZAI3.GOODS_CD_NRS                           AS GOODS_CD_NRS                                                                                        " & vbNewLine _
                                         & "  ,MG.GOODS_CD_CUST AS GOODS_CD_CUST                                                                                                                  " & vbNewLine _
                                         & "  ,ISNULL(ZAI3.LOT_NO, '')                     AS LOT_NO                                                                                              " & vbNewLine _
                                         & "  ,ISNULL(ZAI3.SERIAL_NO, '')                  AS SERIAL_NO                                                                                           " & vbNewLine _
                                         & "  ,IDO1.N_PORA_ZAI_NB                          AS PORA_ZAI_NB                                                                                         " & vbNewLine _
                                         & "  ,IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME           AS PORA_ZAI_QT                                                                                         " & vbNewLine _
                                         & "  ,IDO1.NRS_BR_CD                     AS NRS_BR_CD                                                                                                    " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  $LM_TRN$..D_IDO_TRS IDO1                                                                                                                              " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI3                                                                                                                    " & vbNewLine _
                                         & "  ON  ZAI3.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                         & "  AND ZAI3.NRS_BR_CD = IDO1.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  AND ZAI3.ZAI_REC_NO = IDO1.N_ZAI_REC_NO                                                                                                             " & vbNewLine _
                                         & "  --LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI4                                                                                                                    " & vbNewLine _
                                         & "  --ON  ZAI4.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                         & "  --AND ZAI4.NRS_BR_CD = IDO1.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  --AND ZAI4.ZAI_REC_NO = IDO1.N_ZAI_REC_NO                                                                                                             " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                        " & vbNewLine _
                                         & "  ON  ZAI3.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                   " & vbNewLine _
                                         & "  AND ZAI3.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                             " & vbNewLine _
                                         & "  WHERE                                                                                                                                               " & vbNewLine _
                                         & "  IDO1.SYS_DEL_FLG = '0'                                                                                                                              " & vbNewLine _
                                         & "  AND IDO1.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND ZAI3.CUST_CD_L = @CUST_CD_L                                                                                                                     " & vbNewLine _
                                         & "  AND ZAI3.CUST_CD_M = @CUST_CD_M                                                                                                                     " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND IDO1.IDO_DATE <= @PRINT_DATE_TO                                                                                                                    " & vbNewLine _
                                         & "  AND IDO1.IDO_DATE >= IDO1.IDO_DATE                                                                 " & vbNewLine _
                                         & "  --移動前                                                                                                                                               " & vbNewLine _
                                         & "  UNION ALL                                                                                                                                           " & vbNewLine _
                                         & "  SELECT                                                                                                                                              " & vbNewLine _
                                         & "  ''                                          AS OUTKA_NO_L                                                                                           " & vbNewLine _
                                         & "  ,''                                          AS OUTKA_NO_M                                                                                          " & vbNewLine _
                                         & "  ,''                                          AS OUTKA_NO_S                                                                                          " & vbNewLine _
                                         & "  ,ZAI5.CUST_CD_L                              AS CUST_CD_L                                                                                           " & vbNewLine _
                                         & "  ,ZAI5.CUST_CD_M                              AS CUST_CD_M                                                                                           " & vbNewLine _
                                         & "  ,''                                          AS CUST_NM_L                                                                                           " & vbNewLine _
                                         & "  ,IDO2.O_ZAI_REC_NO                           AS ZAI_REC_NO                                                                                          " & vbNewLine _
                                         & "  ,ZAI5.GOODS_CD_NRS                           AS GOODS_CD_NRS                                                                                        " & vbNewLine _
                                         & "  ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST                                                                                                  " & vbNewLine _
                                         & "  ,ISNULL(ZAI5.LOT_NO, '')                     AS LOT_NO                                                                                              " & vbNewLine _
                                         & "  ,ISNULL(ZAI5.SERIAL_NO, '')                  AS SERIAL_NO                                                                                           " & vbNewLine _
                                         & "  ,IDO2.N_PORA_ZAI_NB * -1                     AS PORA_ZAI_NB                                                                                         " & vbNewLine _
                                         & "  ,IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1      AS PORA_ZAI_QT                                                                                         " & vbNewLine _
                                         & "  ,IDO2.NRS_BR_CD                     AS NRS_BR_CD                                                                                                    " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  $LM_TRN$..D_IDO_TRS IDO2                                                                                                                              " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI5                                                                                                                    " & vbNewLine _
                                         & "  ON  ZAI5.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                         & "  AND ZAI5.NRS_BR_CD = IDO2.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  AND ZAI5.ZAI_REC_NO = IDO2.O_ZAI_REC_NO                                                                                                             " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                        " & vbNewLine _
                                         & "  ON  ZAI5.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                   " & vbNewLine _
                                         & "  AND ZAI5.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                             " & vbNewLine _
                                         & "  WHERE                                                                                                                                               " & vbNewLine _
                                         & "  IDO2.SYS_DEL_FLG = '0'                                                                                                                              " & vbNewLine _
                                         & "  AND IDO2.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND ZAI5.CUST_CD_L = @CUST_CD_L                                                                                                                   " & vbNewLine _
                                         & "  AND ZAI5.CUST_CD_M = @CUST_CD_M                                                                                                                    " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND IDO2.IDO_DATE <= @PRINT_DATE_TO                                                                                                                    " & vbNewLine _
                                         & "  AND IDO2.IDO_DATE >= IDO2.IDO_DATE                                                                 " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  --出荷データ(C_OUTKA_S)                                                                                                                                  " & vbNewLine _
                                         & "  UNION ALL                                                                                                                                           " & vbNewLine _
                                         & "  SELECT                                                                                                                                     " & vbNewLine _
                                         & "  OUTKA_NO_L                                                                                                                                          " & vbNewLine _
                                         & "  ,OUTKA_NO_M                                                                                                                                         " & vbNewLine _
                                         & "  ,OUTKA_NO_S                                                                                                                                         " & vbNewLine _
                                         & "  ,CUST_CD_L                                                                                                                                          " & vbNewLine _
                                         & "  ,CUST_CD_M                                                                                                                                          " & vbNewLine _
                                         & "  ,CUST_NM_L                                                                                                                                          " & vbNewLine _
                                         & "  ,ZAI_REC_NO                                                                                                                                         " & vbNewLine _
                                         & "  ,GOODS_CD_NRS                                                                                                                                       " & vbNewLine _
                                         & "  ,GOODS_CD_CUST                                                                                                                                      " & vbNewLine _
                                         & "  ,LOT_NO                                                                                                                                             " & vbNewLine _
                                         & "  ,SERIAL_NO                                                                                                                                          " & vbNewLine _
                                         & "  ,PORA_ZAI_NB                                                                                                                                        " & vbNewLine _
                                         & "  ,PORA_ZAI_QT                                                                                                                                        " & vbNewLine _
                                         & "  ,NRS_BR_CD                                                                                                                                          " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  (SELECT                                                                                                                                             " & vbNewLine _
                                         & "  OUTS.OUTKA_NO_L                   AS OUTKA_NO_L                                                                                                     " & vbNewLine _
                                         & "  ,OUTS.OUTKA_NO_M                   AS OUTKA_NO_M                                                                                                    " & vbNewLine _
                                         & "  ,OUTS.OUTKA_NO_S                   AS OUTKA_NO_S                                                                                                    " & vbNewLine _
                                         & "  ,OUTL.CUST_CD_L                    AS CUST_CD_L                                                                                                     " & vbNewLine _
                                         & "  ,OUTL.CUST_CD_M                    AS CUST_CD_M                                                                                                     " & vbNewLine _
                                         & "  ,''                                AS CUST_NM_L                                                                                                     " & vbNewLine _
                                         & "  ,OUTS.ZAI_REC_NO                   AS ZAI_REC_NO                                                                                                    " & vbNewLine _
                                         & "  ,OUTM.GOODS_CD_NRS                 AS GOODS_CD_NRS                                                                                                  " & vbNewLine _
                                         & "  ,MG.GOODS_CD_CUST               AS GOODS_CD_CUST                                                                                                    " & vbNewLine _
                                         & "  ,ISNULL(OUTS.LOT_NO, '')           AS LOT_NO                                                                                                        " & vbNewLine _
                                         & "  ,ISNULL(OUTS.SERIAL_NO, '')        AS SERIAL_NO                                                                                                     " & vbNewLine _
                                         & "  ,OUTS.ALCTD_NB * -1                AS PORA_ZAI_NB                                                                                                   " & vbNewLine _
                                         & "  ,OUTS.ALCTD_QT * -1                AS PORA_ZAI_QT                                                                                                   " & vbNewLine _
                                         & "  ,OUTS.NRS_BR_CD                     AS NRS_BR_CD                                                                                                    " & vbNewLine _
                                         & "  FROM                                                                                                                                                " & vbNewLine _
                                         & "  $LM_TRN$..C_OUTKA_L OUTL                                                                                                                              " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                                                                                    " & vbNewLine _
                                         & "  ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                                               " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                                                                                    " & vbNewLine _
                                         & "  ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                                               " & vbNewLine _
                                         & "  AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                                                                               " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                        " & vbNewLine _
                                         & "  ON  OUTM.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                   " & vbNewLine _
                                         & "  AND OUTM.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                             " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  WHERE                                                                                                                                               " & vbNewLine _
                                         & "  OUTL.SYS_DEL_FLG = '0' AND OUTM.SYS_DEL_FLG = '0' AND OUTS.SYS_DEL_FLG = '0'                                                                                                                            " & vbNewLine _
                                         & "  AND OUTM.ALCTD_KB <> '04'                                                                                                                           " & vbNewLine _
                                         & "  AND OUTL.OUTKA_STATE_KB !< '60'                                                                                                                     " & vbNewLine _
                                         & "  AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  AND OUTL.CUST_CD_L = @CUST_CD_L                                                                                                                     " & vbNewLine _
                                         & "  AND OUTL.CUST_CD_M = @CUST_CD_M                                                                                                                     " & vbNewLine _
                                         & "  AND OUTL.OUTKA_PLAN_DATE <= @PRINT_DATE_TO                                                                                                             " & vbNewLine _
                                         & "  AND OUTL.OUTKA_PLAN_DATE >= OUTL.OUTKA_PLAN_DATE                                                   " & vbNewLine _
                                         & "  ) BASE3                                                                                                                                             " & vbNewLine _
                                         & "  ) BASE4                                                                                                                                             " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  WHERE                                                                                                                                               " & vbNewLine _
                                         & "  CUST_CD_L = @CUST_CD_L                                                                                                                              " & vbNewLine _
                                         & "  AND CUST_CD_M = @CUST_CD_M                                                                                                                          " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  GROUP BY                                                                                                                                            " & vbNewLine _
                                         & "  CUST_CD_L                                                                                                                                           " & vbNewLine _
                                         & "  ,CUST_CD_M                                                                                                                                          " & vbNewLine _
                                         & "  ,CUST_NM_L                                                                                                                                          " & vbNewLine _
                                         & "  ,ZAI_REC_NO                                                                                                                                         " & vbNewLine _
                                         & "  ,GOODS_CD_NRS                                                                                                                                       " & vbNewLine _
                                         & "  ,GOODS_CD_CUST                                                                                                                                      " & vbNewLine _
                                         & "  ,LOT_NO                                                                                                                                             " & vbNewLine _
                                         & "  ,SERIAL_NO                                                                                                                                          " & vbNewLine _
                                         & "  ,NRS_BR_CD                                                                                                                                          " & vbNewLine _
                                         & "  ) BASE                                                                                                                                              " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  WHERE                                                                                                                                               " & vbNewLine _
                                         & "  BASE.PORA_ZAI_NB <> 0                                                                                                                               " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  )                                                                                                                                                   " & vbNewLine _
                                         & "  BASE10                                                                                                                                              " & vbNewLine _
                                         & "  LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                                                                                                  " & vbNewLine _
                                         & "  ON ZAITRS.NRS_BR_CD = BASE10.NRS_BR_CD                                                                                                              " & vbNewLine _
                                         & "  AND ZAITRS.ZAI_REC_NO = BASE10.ZAI_REC_NO                                                                                                           " & vbNewLine _
                                         & "  AND ZAITRS.SYS_DEL_FLG = '0'                                                                                                                        " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                        " & vbNewLine _
                                         & "  ON  ZAITRS.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                         & "  AND ZAITRS.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                           " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_CUST AS CUST                                                                                                                    " & vbNewLine _
                                         & "  ON MG.NRS_BR_CD = CUST.NRS_BR_CD                                                                                                                    " & vbNewLine _
                                         & "  AND MG.CUST_CD_L = CUST.CUST_CD_L                                                                                                                   " & vbNewLine _
                                         & "  AND MG.CUST_CD_M = CUST.CUST_CD_M                                                                                                                   " & vbNewLine _
                                         & "  AND MG.CUST_CD_S = CUST.CUST_CD_S                                                                                                                   " & vbNewLine _
                                         & "  AND MG.CUST_CD_SS = CUST.CUST_CD_SS                                                                                                                 " & vbNewLine _
                                         & "                                                                                                                                                      " & vbNewLine _
                                         & "  LEFT JOIN                                                                                                                                           " & vbNewLine _
                                         & "  $LM_MST$..M_SEIQTO SEIQ ON                                                                                                                            " & vbNewLine _
                                         & "  SEIQ.SEIQTO_CD = CUST.HOKAN_SEIQTO_CD                                                                                                               " & vbNewLine _
                                         & "  AND SEIQ.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                         & "  LEFT JOIN                                                                                                                                           " & vbNewLine _
                                         & "  $LM_MST$..M_NRS_BR NRS ON                                                                                                                             " & vbNewLine _
                                         & "  NRS.NRS_BR_CD = @NRS_BR_CD                                                                                                                          " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..M_CUSTCOND COND ON                                                                                                                          " & vbNewLine _
                                         & "  COND.NRS_BR_CD = @NRS_BR_CD                                                                                                                         " & vbNewLine _
                                         & "  AND COND.CUST_CD_L = BASE10.CUST_CD_L                                                                                                               " & vbNewLine _
                                         & "  AND COND.JOTAI_CD = ZAITRS.GOODS_COND_KB_3                                                                                                          " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..Z_KBN KBN2 ON                                                                                                                               " & vbNewLine _
                                         & "  KBN2.KBN_GROUP_CD = 'Z001'                                                                                                                          " & vbNewLine _
                                         & "  AND KBN2.KBN_CD = ZAITRS.TAX_KB                                                                                                                     " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..M_SOKO SOKO ON                                                                                                                              " & vbNewLine _
                                         & "  SOKO.NRS_BR_CD = @NRS_BR_CD                                                                                                                         " & vbNewLine _
                                         & "  AND SOKO.WH_CD = ZAITRS.WH_CD                                                                                                                       " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..M_CUST_DETAILS CUST_D ON                                                                                                                  " & vbNewLine _
                                         & "  CUST_D.NRS_BR_CD   = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                         & "  AND CUST_D.CUST_CD = BASE10.CUST_CD_L + BASE10.CUST_CD_M                                                                                            " & vbNewLine _
                                         & "  AND CUST_D.SUB_KB  = '09'                                                                                                                           " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..M_CUST_DETAILS MCD ON                                                                                                                     " & vbNewLine _
                                         & "  MCD.NRS_BR_CD   = @NRS_BR_CD                                                                                                                        " & vbNewLine _
                                         & "  AND MCD.CUST_CD = BASE10.CUST_CD_L + BASE10.CUST_CD_M + MG.CUST_CD_S                                                                                " & vbNewLine _
                                         & "  AND MCD.SUB_KB  = '15'                                                                                                                              " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..M_CUST_DETAILS MCD2 ON                                                                                                                     " & vbNewLine _
                                         & "  MCD2.NRS_BR_CD   = @NRS_BR_CD                                                                                                                        " & vbNewLine _
                                         & "  AND MCD2.CUST_CD = BASE10.CUST_CD_L                                                                                                                  " & vbNewLine _
                                         & "  AND MCD2.SUB_KB  = '18'                                                                                                                              " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..Z_KBN KBN3 ON                                                                                                                               " & vbNewLine _
                                         & "  KBN3.KBN_GROUP_CD = 'S005'                                                                                                                          " & vbNewLine _
                                         & "  AND KBN3.KBN_CD = ZAITRS.GOODS_COND_KB_1                                                                                                            " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..Z_KBN KBN4 ON                                                                                                                               " & vbNewLine _
                                         & "  KBN4.KBN_GROUP_CD = 'S006'                                                                                                                          " & vbNewLine _
                                         & "  AND KBN4.KBN_CD = ZAITRS.GOODS_COND_KB_2                                                                                                            " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..Z_KBN KBN5 ON                                                                                                                               " & vbNewLine _
                                         & "  KBN5.KBN_GROUP_CD = 'B002'                                                                                                                          " & vbNewLine _
                                         & "  AND KBN5.KBN_CD = ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                         & "  --(2012.12.13)要望番号1657対応 -- START --                                                                                                 " & vbNewLine _
                                         & "  LEFT OUTER JOIN                                                                                                                                     " & vbNewLine _
                                         & "  $LM_MST$..Z_KBN KBN6 ON                                                                                                                             " & vbNewLine _
                                         & "  KBN6.KBN_GROUP_CD = 'T021'                                                                                                                          " & vbNewLine _
                                         & "  AND KBN6.KBN_CD = ZAITRS.TOU_NO                                                                                                                     " & vbNewLine _
                                         & "  --(2012.12.13)要望番号1657対応 --  END  --                                                                                                          " & vbNewLine _
                                         & "  --在庫の荷主での荷主帳票パターン取得                                                                                                                  " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                                                   " & vbNewLine _
                                         & "  ON  MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                         & "  AND MCR1.CUST_CD_L = @CUST_CD_L                                                                                                                     " & vbNewLine _
                                         & "  AND MCR1.CUST_CD_M = @CUST_CD_M                                                                                                                     " & vbNewLine _
                                         & "  AND MCR1.PTN_ID = @PTN_ID                                                                                                                              " & vbNewLine _
                                         & "  AND MCR1.CUST_CD_S = '00'                                                                                                                           " & vbNewLine _
                                         & "  --帳票パターン取得                                                                                                                                          " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                                         " & vbNewLine _
                                         & "  ON  MR1.NRS_BR_CD = @NRS_BR_CD                                                                                                                      " & vbNewLine _
                                         & "  AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                                        " & vbNewLine _
                                         & "  AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                                        " & vbNewLine _
                                         & "  AND MR1.SYS_DEL_FLG = '0'                                                                                                                           " & vbNewLine _
                                         & "  --商品Mの荷主での荷主帳票パターン取得                                                                                                                                " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                                                   " & vbNewLine _
                                         & "  ON  MCR2.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                         & "  AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                                                   " & vbNewLine _
                                         & "  AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                                                   " & vbNewLine _
                                         & "  AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                                                   " & vbNewLine _
                                         & "  AND MCR2.PTN_ID = @PTN_ID                                                                                                                              " & vbNewLine _
                                         & "  --帳票パターン取得                                                                                                                                          " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                                         " & vbNewLine _
                                         & "  ON  MR2.NRS_BR_CD = @NRS_BR_CD                                                                                                                      " & vbNewLine _
                                         & "  AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                                        " & vbNewLine _
                                         & "  AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                                        " & vbNewLine _
                                         & "  AND MR2.SYS_DEL_FLG = '0'                                                                                                                           " & vbNewLine _
                                         & "  --存在しない場合の帳票パターン取得                                                                                                                                  " & vbNewLine _
                                         & "  LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                                         " & vbNewLine _
                                         & "  ON  MR3.NRS_BR_CD = @NRS_BR_CD                                                                                                                      " & vbNewLine _
                                         & "  AND MR3.PTN_ID = @PTN_ID                                                                                                                               " & vbNewLine _
                                         & "  AND MR3.STANDARD_FLAG = '01'                                                                                                                        " & vbNewLine _
                                         & "  AND MR3.SYS_DEL_FLG = '0'                                                                                                                           " & vbNewLine _
                                         & "  GROUP BY                                                                                                              " & vbNewLine _
                                         & "   --20170427 GROUP BY 修正                                                                                                             " & vbNewLine _
                                         & "    ZAITRS.NRS_BR_CD                                                                                                             " & vbNewLine _
                                         & "  , BASE10.LOT_NO                                                                                                            " & vbNewLine _
                                         & "  , SOKO.WH_CD                                                                                                             " & vbNewLine _
                                         & "  , BASE10.GOODS_CD_NRS                                                                                                             " & vbNewLine _
                                         & "  , ZAITRS.IRIME                                                                                                             " & vbNewLine _
                                         & "  , ZAITRS.GOODS_COND_KB_1                                                                                                             " & vbNewLine _
                                         & "  , ZAITRS.GOODS_COND_KB_2                                                                                                             " & vbNewLine _
                                         & "  , ZAITRS.GOODS_COND_KB_3                                                                                                             " & vbNewLine _
                                         & "   --20170427 GROUP BY 修正                                                                                                             " & vbNewLine _
                                         & "  ---20170412 在庫証明書より移行-----------------------------------------------------------------------------------------------------------" & vbNewLine _
                                         & "	) SUB ON                                     " & vbNewLine _
                                         & " SUB.LOT_NO = MAIN.LOT_NO                        " & vbNewLine _
                                         & " AND SUB.IRIME_NB = MAIN.IRIME                      " & vbNewLine _
                                         & " AND SUB.WH_CD = MAIN.WH_CD                      " & vbNewLine _
                                         & " AND SUB.NRS_BR_CD = MAIN.NRS_BR_CD              " & vbNewLine _
                                         & " AND SUB.GOODS_CD_NRS = MAIN.GOODS_CD_NRS        " & vbNewLine _
                                         & "  --20170427                                     " & vbNewLine _
                                         & " AND SUB.GOODS_COND_KB_1 = MAIN.GOODS_COND_KB_1              " & vbNewLine _
                                         & " AND SUB.GOODS_COND_KB_2 = MAIN.GOODS_COND_KB_2              " & vbNewLine _
                                         & " AND SUB.GOODS_COND_KB_3 = MAIN.GOODS_COND_KB_3              " & vbNewLine _
                                         & "  --20170427                                     " & vbNewLine

#End Region '印刷データ取得 LMC619専用

#End Region 'SQL                             

#End Region 'Const                           

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "PTN_ID" _
                                            , "PTN_CD" _
                                            , "RPT_ID" _
                                            }
        Dim ptn As LMC610DAC.SelectCondition = SelectCondition.PTN1
        Return Me.SelectListData(ds, LMC610DAC.TABLE_NM_M_RPT, Me.CreateSql(ds, ptn), ptn, str)

    End Function

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPrtData(ByVal ds As DataSet) As DataSet

        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        Dim str As String() = New String() {"RPT_ID" _
                                           , "NRS_BR_CD" _
                                           , "NRS_BR_NM" _
                                           , "CUST_NM_L" _
                                           , "OUTKA_DATE" _
                                           , "NRS_AD_1" _
                                           , "NRS_AD_2" _
                                           , "CUST_NM_M" _
                                           , "CUST_ORD_NO" _
                                           , "DEST_NM" _
                                           , "DEST_AD_1" _
                                           , "DEST_AD_2" _
                                           , "CUST_CD_L" _
                                           , "CUST_CD_M" _
                                           , "UNSOCO_NM" _
                                           , "GOODS_NM_1" _
                                           , "LOT_NO" _
                                           , "IRIME" _
                                           , "ALCTD_NB" _
                                           , "ALCTD_QT" _
                                           , "IRIME_UT" _
                                           , "OUTKA_NO_L" _
                                           , "REMARK" _
                                           , "ALLOC_CAN_QT" _
                                           , "ARR_PLAN_DATE" _
                                           , "GOODS_CD_NRS" _
                                           , "GOODS_CD_CUST" _
                                           , "SERIAL_NO" _
                                           , "DOKU_NM" _
                                           , "PRINT_DATE_FROM" _
                                           , "PRINT_DATE_TO" _
                                           }

        Dim str615 As String() = New String() {"RPT_ID" _
                                             , "NRS_BR_CD" _
                                             , "NRS_BR_NM" _
                                             , "CUST_NM_L" _
                                             , "OUTKA_DATE" _
                                             , "NRS_AD_1" _
                                             , "NRS_AD_2" _
                                             , "CUST_NM_M" _
                                             , "CUST_ORD_NO" _
                                             , "DEST_NM" _
                                             , "DEST_AD_1" _
                                             , "DEST_AD_2" _
                                             , "CUST_CD_L" _
                                             , "CUST_CD_M" _
                                             , "UNSOCO_NM" _
                                             , "GOODS_NM_1" _
                                             , "LOT_NO" _
                                             , "IRIME" _
                                             , "ALCTD_NB" _
                                             , "ALCTD_QT" _
                                             , "IRIME_UT" _
                                             , "OUTKA_NO_L" _
                                             , "REMARK" _
                                             , "ALLOC_CAN_QT" _
                                             , "ARR_PLAN_DATE" _
                                             , "GOODS_CD_NRS" _
                                             , "GOODS_CD_CUST" _
                                             , "SERIAL_NO" _
                                             , "DOKU_NM" _
                                             , "PRINT_DATE_FROM" _
                                             , "PRINT_DATE_TO" _
                                             , "CUST_CD_S" _
                                             , "CUST_NM_S" _
                                             , "DEST_CD" _
                                             , "DEST_TEL" _
                                             , "NHS_REMARK" _
                                             , "PKG_NB" _
                                             , "DELI_ATT" _
                                             }

        Dim ptn As LMC610DAC.SelectCondition = SelectCondition.PTN2

        If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC615" Then
            'LMC615 BP専用出荷報告書の場合、str615を使用
            Return Me.SelectListData(ds, LMC610DAC.TABLE_NM_OUT, Me.CreateSql(ds, ptn), ptn, str615)
        End If

        Return Me.SelectListData(ds, LMC610DAC.TABLE_NM_OUT, Me.CreateSql(ds, ptn), ptn, str)

    End Function

#End Region '検索処理

#Region "ユーティリティ"

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
    ''' データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal ptn As LMC610DAC.SelectCondition, ByVal str As String()) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMC610DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'cmd.CommandTimeout = 6000

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMC610DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, tblNm)

    End Function

    ''' <summary>
    ''' whereパターン取得
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetWherePtn(ByVal dr As DataRow) As LMC610DAC.WhereSelectCondition

        Dim wptn As LMC610DAC.WhereSelectCondition = Nothing
        Dim outkaNoL As String = dr.Item("OUTKA_NO_L").ToString

        If String.IsNullOrEmpty(outkaNoL) = False Then
            wptn = WhereSelectCondition.WPTN1
        Else
            wptn = WhereSelectCondition.WPTN2
        End If

        Return wptn

    End Function

    ''' <summary>
    ''' SQL作成
    ''' </summary>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSql(ByVal ds As DataSet, ByVal ptn As LMC610DAC.SelectCondition) As String

        Dim sql As StringBuilder = New StringBuilder()
        Dim wptn As LMC610DAC.WhereSelectCondition = Me.GetWherePtn(ds.Tables(LMC610DAC.TABLE_NM_IN).Rows(0))

        Select Case ptn

            Case SelectCondition.PTN1
                sql.Append(LMC610DAC.SQL_SELECT_MPrt)
                '(2013.01.22)検索条件 荷主コード(小)の追加対応 -- START --
                If ds.Tables("LMC610IN").Rows(0).Item("CUST_CD_S").ToString.Trim.Equals("") = True Then
                    sql.Append(LMC610DAC.SQL_ALL_FROM)
                Else
                    '検索条件に荷主コード(小)有り
                    sql.Append(LMC610DAC.SQL_CUST_S_FROM)
                End If
                '(2013.01.22)検索条件 荷主コード(小)の追加対応 --  END  --
                'sql.Append(Me.CreateWhereSQL(ds, wptn))

            Case SelectCondition.PTN2
                Select Case ds.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString

                    '(2013.01.28)BP専用出荷報告書対応 -- START --
                    Case "LMC615"
                        'BP専用
                        sql.Append(LMC610DAC.SQL_SELECT_FIRST_615)
                        If ds.Tables("LMC610IN").Rows(0).Item("CUST_CD_S").ToString.Trim.Equals("") = True Then
                            sql.Append(LMC610DAC.SQL_ALL_FROM_615)
                        Else
                            '検索条件に荷主コード(小)有り
                            sql.Append(LMC610DAC.SQL_CUST_S_FROM_615)
                        End If
                        sql.Append(LMC610DAC.SQL_ADD_FROM_615)
                        sql.Append(LMC610DAC.SQL_ORDER_BY)
                        '(2013.01.28)BP専用出荷報告書対応 --  END  --

                        '(2014.06.05)営業所→倉庫コードから住所を表示　要望管理2196対応 -- START --
                    Case "LMC616"
                        sql.Append(LMC610DAC.SQL_SELECT_FIRST_616)
                        '(2013.01.22)検索条件 荷主コード(小)の追加対応 -- START --
                        If ds.Tables("LMC610IN").Rows(0).Item("CUST_CD_S").ToString.Trim.Equals("") = True Then
                            sql.Append(LMC610DAC.SQL_ALL_FROM)
                        Else
                            '検索条件に荷主コード(小)有り
                            sql.Append(LMC610DAC.SQL_CUST_S_FROM)
                        End If
                        sql.Append(LMC610DAC.SQL_ADD_FROM)
                        sql.Append(LMC610DAC.SQL_ORDER_BY)          'SQL構築(データ抽出用ORDER BY句)
                        '(2014.06.05)営業所→倉庫コードから住所を表示　要望管理2196対応 --  END  --
                    Case "LMC619"
                        '20170412 残数を出荷当日残数とする修正
                        sql.Append(LMC610DAC.SQL_SELECT_FIRST_619)
                        '(2013.01.22)検索条件 荷主コード(小)の追加対応 -- START --
                        If ds.Tables("LMC610IN").Rows(0).Item("CUST_CD_S").ToString.Trim.Equals("") = True Then
                            sql.Append(LMC610DAC.SQL_ALL_FROM_619)
                        Else
                            '検索条件に荷主コード(小)有り
                            sql.Append(LMC610DAC.SQL_CUST_S_FROM_619)
                        End If
                        sql.Append(LMC610DAC.SQL_ADD_FROM_619)
                        sql.Append(LMC610DAC.SQL_ORDER_BY)          'SQL構築(データ抽出用ORDER BY句)
                        '20170412 残数を出荷当日残数とする修正
                    Case Else
                        sql.Append(LMC610DAC.SQL_SELECT_FIRST)
                        '(2013.01.22)検索条件 荷主コード(小)の追加対応 -- START --
                        If ds.Tables("LMC610IN").Rows(0).Item("CUST_CD_S").ToString.Trim.Equals("") = True Then
                            sql.Append(LMC610DAC.SQL_ALL_FROM)
                        Else
                            '検索条件に荷主コード(小)有り
                            sql.Append(LMC610DAC.SQL_CUST_S_FROM)
                        End If
                        sql.Append(LMC610DAC.SQL_ADD_FROM)
                        '(2013.01.22)検索条件 荷主コード(小)の追加対応 --  END  --

                        'sql.Append(Me.CreateWhereSQL(ds, wptn))
                        'sql.Append(LMC610DAC.SQL_ORDER_BY)
                        Select Case ds.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString
                            Case "LMC612"
                                sql.Append(LMC610DAC.SQL_ORDER_BY_MONTH)    'SQL構築(データ抽出用ORDER BY句)
                            Case Else
                                sql.Append(LMC610DAC.SQL_ORDER_BY)          'SQL構築(データ抽出用ORDER BY句)
                        End Select
                End Select

        End Select

        Return Me.CreateWhereSQL(ds, wptn, sql.ToString())

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMC610DAC.SelectCondition)

        Dim wptn As LMC610DAC.WhereSelectCondition = Me.GetWherePtn(dr)

        With dr

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Select Case wptn

                Case LMC610DAC.WhereSelectCondition.WPTN1 'LMC610

                    prmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMC610DAC.PTN_ID_LMC610, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PRINT_DATE_FROM", .Item("PRINT_DATE_FROM").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PRINT_DATE_TO", .Item("PRINT_DATE_TO").ToString(), DBDataType.CHAR))
                    '(2013.01.28)ﾊﾟﾌｫｰﾏﾝｽ向上の為に追加した項目 -- START --
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                    '(2013.01.28)ﾊﾟﾌｫｰﾏﾝｽ向上の為に追加した項目 --  END  --
                    '2018/03/07 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start
                    prmList.Add(MyBase.GetSqlParameter("@USER_CD", MyBase.GetUserID, DBDataType.NVARCHAR))
                    '2018/03/07 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add end

                Case LMC610DAC.WhereSelectCondition.WPTN2 'LMC611

                    '2012.02.28 大阪対応START
                    '日別出荷報告書の場合のパターンコード
                    If .Item("PRINT_FLAG").ToString().Equals("02") = True Then
                        prmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMC610DAC.PTN_ID_LMC611, DBDataType.CHAR))
                        '出荷報告書(月次)の場合のパターンコード
                    ElseIf .Item("PRINT_FLAG").ToString().Equals("03") = True Then
                        prmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMC610DAC.PTN_ID_LMC612, DBDataType.CHAR))
                    End If
                    '2012.02.28 大阪対応END
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", .Item("SYUBETU_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PRINT_DATE_FROM", .Item("PRINT_DATE_FROM").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PRINT_DATE_TO", .Item("PRINT_DATE_TO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
                    '(2013.01.22)検索条件 荷主コード(小)の追加対応 -- START --
                    If .Item("CUST_CD_S").ToString().Trim.Equals("") = False Then
                        prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
                    End If
                    '(2013.01.22)検索条件 荷主コード(小)の追加対応 --  END  --
                    '2018/03/07 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start
                    prmList.Add(MyBase.GetSqlParameter("@USER_CD", MyBase.GetUserID, DBDataType.NVARCHAR))
                    '2018/03/07 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add end
            End Select

        End With

    End Sub

    ''' <summary>
    ''' WHERE句作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateWhereSQL(ByVal ds As DataSet, ByVal ptn As LMC610DAC.WhereSelectCondition, ByVal sql As String) As String

        Dim whereSql1 As StringBuilder = New StringBuilder
        Dim whereSql2 As StringBuilder = New StringBuilder
        Dim whereSql3 As StringBuilder = New StringBuilder

        whereSql1.Append("WHERE")
        whereSql1.Append(vbNewLine)
        whereSql1.Append("  NRS_BR_CD = @NRS_BR_CD ")
        whereSql1.Append(vbNewLine)
        whereSql1.Append("  AND SYS_DEL_FLG = '0'")

        whereSql2.Append("")

        whereSql3.Append("")

        With ds.Tables(LMC610DAC.TABLE_NM_IN)

            Select Case ptn

                Case WhereSelectCondition.WPTN1

                    whereSql1.Append(" AND OUTKA_NO_L IN (")
                    whereSql1.Append(vbNewLine)

                    Dim no As ArrayList = New ArrayList()
                    Dim max As Integer = .Rows.Count - 1

                    If 0 <= max Then
                        For i As Integer = 0 To max
                            If i = max Then
                                whereSql1.Append(String.Concat("'" & .Rows(i).Item("OUTKA_NO_L").ToString() & "'", " ) "))
                            Else
                                whereSql1.Append(String.Concat("'" & .Rows(i).Item("OUTKA_NO_L").ToString() & "'", " , "))
                            End If
                        Next
                        whereSql1.Append(vbNewLine)
                    End If

                Case WhereSelectCondition.WPTN2

                    If String.IsNullOrEmpty(.Rows(0).Item("CUST_CD_L").ToString()) = False Then
                        whereSql1.Append(" AND CUST_CD_L = @CUST_CD_L ")
                        whereSql1.Append(vbNewLine)
                    End If

                    If String.IsNullOrEmpty(.Rows(0).Item("CUST_CD_M").ToString()) = False Then
                        whereSql1.Append(" AND CUST_CD_M = @CUST_CD_M ")
                        whereSql1.Append(vbNewLine)
                    End If

                    'If String.IsNullOrEmpty(.Rows(0).Item("PRINT_DATE_FROM").ToString()) = False Then
                    '    whereSql1.Append(" AND @PRINT_DATE_FROM <= OUTKA_DATE ")
                    '    whereSql1.Append(vbNewLine)
                    'End If

                    'If String.IsNullOrEmpty(.Rows(0).Item("PRINT_DATE_TO").ToString()) = False Then
                    '    whereSql1.Append(" AND OUTKA_DATE <= @PRINT_DATE_TO ")
                    '    whereSql1.Append(vbNewLine)
                    'End If

                    If String.IsNullOrEmpty(.Rows(0).Item("PRINT_DATE_FROM").ToString()) = False Then
                        If String.IsNullOrEmpty(.Rows(0).Item("PRINT_DATE_TO").ToString()) = False Then
                            whereSql1.Append(" AND OUTKA_PLAN_DATE BETWEEN @PRINT_DATE_FROM AND @PRINT_DATE_TO")
                            whereSql1.Append(vbNewLine)
                        End If
                    End If

                    If .Rows(0).Item("SYUBETU_KB").ToString.Equals("50") = False Then
                        whereSql1.Append(" AND SYUBETU_KB <> '50' ")
                        whereSql1.Append(vbNewLine)
                    End If

                    If String.IsNullOrEmpty(.Rows(0).Item("GOODS_CD_CUST").ToString()) = False Then
                        whereSql3.Append(" AND GOODS.GOODS_CD_CUST = @GOODS_CD_CUST")
                        whereSql3.Append(vbNewLine)
                    End If

            End Select

        End With

        sql = sql.Replace("$WHERE1$", whereSql1.ToString())
        sql = sql.Replace("$WHERE2$", whereSql2.ToString())
        sql = sql.Replace("$WHERE3$", whereSql3.ToString())

        Return sql

    End Function

#End Region 'ユーティリティ

#End Region 'Method

End Class
