' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : データ管理サブ
'  プログラムID     :  LMI500DAC : デュポン在庫報告
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI500DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI500DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' 検索パターン
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SelectCondition As Integer
        PTN1
        PTN2
        PTN3
    End Enum

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "CREATE_IN"

    ''' <summary>
    ''' COUNTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_COUNT As String = "COUNT"

    ''' <summary>
    ''' LMI500SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_LMI500SET As String = "LMI500SET"

    ''' <summary>
    ''' LMI501SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_LMI501SET As String = "LMI501SET"

    ''' <summary>
    ''' LMI502SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_LMI502SET As String = "LMI502SET"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMI500DAC"

    ''' <summary>
    ''' 特殊プラントコード
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PLANTCD_SPECIAL As String = "WQ49"

#End Region

#Region "検索処理 SQL"

#Region "月末在庫存在チェック"

    Private Const SQL_SELECT_GETU_DATA As String = "SELECT                                                        " & vbNewLine _
                                                 & " COUNT(MAIN.RIREKI_DATE) AS REC_CNT                           " & vbNewLine _
                                                 & "FROM                                                          " & vbNewLine _
                                                 & "(                                                             " & vbNewLine _
                                                 & "        SELECT                                                " & vbNewLine _
                                                 & "                 D01_01.NRS_BR_CD   AS NRS_BR_CD              " & vbNewLine _
                                                 & "                ,D01_01.CUST_CD_L   AS CUST_CD_L              " & vbNewLine _
                                                 & "                ,D01_01.CUST_CD_M   AS CUST_CD_M              " & vbNewLine _
                                                 & "                ,D05_01.RIREKI_DATE AS RIREKI_DATE            " & vbNewLine _
                                                 & "        FROM       $LM_TRN$..D_ZAI_TRS       D01_01           " & vbNewLine _
                                                 & "        INNER JOIN $LM_TRN$..D_ZAI_ZAN_JITSU D05_01           " & vbNewLine _
                                                 & "           ON D01_01.NRS_BR_CD             = D05_01.NRS_BR_CD " & vbNewLine _
                                                 & "          AND D01_01.ZAI_REC_NO            = D05_01.ZAI_REC_NO" & vbNewLine _
                                                 & "          AND D01_01.SYS_DEL_FLG           = '0'              " & vbNewLine _
                                                 & "        WHERE D01_01.NRS_BR_CD             = @NRS_BR_CD       " & vbNewLine _
                                                 & "          AND D01_01.CUST_CD_L             = @CUST_CD_L       " & vbNewLine _
                                                 & "          AND D01_01.CUST_CD_M             = @CUST_CD_M       " & vbNewLine _
                                                 & "          AND D01_01.SYS_DEL_FLG           = '0'              " & vbNewLine _
                                                 & "          AND D05_01.RIREKI_DATE           = @RIREKI_DATE     " & vbNewLine _
                                                 & "        GROUP BY D01_01.NRS_BR_CD                             " & vbNewLine _
                                                 & "                ,D01_01.CUST_CD_L                             " & vbNewLine _
                                                 & "                ,D01_01.CUST_CD_M                             " & vbNewLine _
                                                 & "                ,D05_01.RIREKI_DATE                           " & vbNewLine _
                                                 & ") MAIN                                                        " & vbNewLine

#End Region

#Region "履歴データ整合性チェック"

    Private Const SQL_SELECT_RIREKI_COUNT As String = "SELECT                                                                              " & vbNewLine _
                                                    & " JITSU.CNT AS JITSU_CNT                                                             " & vbNewLine _
                                                    & ",INKA.CNT  AS INKA_CNT                                                              " & vbNewLine _
                                                    & ",OUTKA.CNT AS OUTKA_CNT                                                             " & vbNewLine _
                                                    & ",IDO.CNT   AS IDO_CNT                                                               " & vbNewLine _
                                                    & "FROM (                                                                              " & vbNewLine _
                                                    & "       SELECT COUNT(D05_01.NRS_BR_CD) AS CNT                                        " & vbNewLine _
                                                    & "         FROM      $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                                 " & vbNewLine _
                                                    & "        INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                 " & vbNewLine _
                                                    & "           ON D05_01.NRS_BR_CD             = D01_01.NRS_BR_CD                       " & vbNewLine _
                                                    & "          AND D05_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                      " & vbNewLine _
                                                    & "          AND D01_01.SYS_DEL_FLG           = '0'                                    " & vbNewLine _
                                                    & "        WHERE D01_01.NRS_BR_CD             = @NRS_BR_CD                             " & vbNewLine _
                                                    & "          AND D01_01.CUST_CD_L             = @CUST_CD_L                             " & vbNewLine _
                                                    & "          AND D01_01.CUST_CD_M             = @CUST_CD_M                             " & vbNewLine _
                                                    & "          AND D05_01.RIREKI_DATE           = @RIREKI_DATE                           " & vbNewLine _
                                                    & "          AND D05_01.SYS_DEL_FLG           = '0'                                    " & vbNewLine _
                                                    & "     ) JITSU                                                                        " & vbNewLine _
                                                    & ",    (                                                                              " & vbNewLine _
                                                    & "       SELECT COUNT(B01_01.NRS_BR_CD) AS CNT                                        " & vbNewLine _
                                                    & "         FROM $LM_TRN$..B_INKA_L          B01_01                                    " & vbNewLine _
                                                    & "        INNER JOIN (                                                                " & vbNewLine _
                                                    & "                      SELECT D05_01.SYS_ENT_DATE + D05_01.SYS_ENT_TIME AS RIREKIMAKE" & vbNewLine _
                                                    & "                            ,D01_01.NRS_BR_CD                          AS NRS_BR_CD " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_L                          AS CUST_CD_L " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_M                          AS CUST_CD_M " & vbNewLine _
                                                    & "                        FROM $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                       " & vbNewLine _
                                                    & "                       INNER JOIN $LM_TRN$..D_ZAI_TRS  D01_01                       " & vbNewLine _
                                                    & "                          ON D05_01.NRS_BR_CD        = D01_01.NRS_BR_CD             " & vbNewLine _
                                                    & "                         AND D05_01.ZAI_REC_NO       = D01_01.ZAI_REC_NO            " & vbNewLine _
                                                    & "                         AND D01_01.SYS_DEL_FLG      = '0'                          " & vbNewLine _
                                                    & "                       WHERE D05_01.RIREKI_DATE      = @RIREKI_DATE                 " & vbNewLine _
                                                    & "                         AND D05_01.SYS_DEL_FLG      = '0'                          " & vbNewLine _
                                                    & "                    GROUP BY D05_01.SYS_ENT_DATE + D05_01.SYS_ENT_TIME              " & vbNewLine _
                                                    & "                            ,D01_01.NRS_BR_CD                                       " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_L                                       " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_M                                       " & vbNewLine _
                                                    & "                   )                    D05_01                                      " & vbNewLine _
                                                    & "           ON B01_01.NRS_BR_CD        = D05_01.NRS_BR_CD                            " & vbNewLine _
                                                    & "          AND B01_01.CUST_CD_L        = D05_01.CUST_CD_L                            " & vbNewLine _
                                                    & "          AND B01_01.CUST_CD_M        = D05_01.CUST_CD_M                            " & vbNewLine _
                                                    & "          AND B01_01.SYS_ENT_DATE                                                   " & vbNewLine _
                                                    & "               + B01_01.SYS_ENT_TIME >= D05_01.RIREKIMAKE                           " & vbNewLine _
                                                    & "        WHERE B01_01.NRS_BR_CD        = @NRS_BR_CD                                  " & vbNewLine _
                                                    & "          AND B01_01.INKA_DATE       <= @RIREKI_DATE                                " & vbNewLine _
                                                    & "          AND B01_01.INKA_DATE       <= @HOUKOKU_DATE                                " & vbNewLine _
                                                    & "          AND B01_01.CUST_CD_L        = @CUST_CD_L                                  " & vbNewLine _
                                                    & "          AND B01_01.CUST_CD_M        = @CUST_CD_M                                  " & vbNewLine _
                                                    & "          AND B01_01.SYS_DEL_FLG      = '0'                                         " & vbNewLine _
                                                    & "          AND B01_01.INKA_STATE_KB   >= '50'                                        " & vbNewLine _
                                                    & "     ) INKA                                                                         " & vbNewLine _
                                                    & ",    (                                                                              " & vbNewLine _
                                                    & "       SELECT COUNT(C01_01.NRS_BR_CD) AS CNT                                        " & vbNewLine _
                                                    & "         FROM $LM_TRN$..C_OUTKA_L         C01_01                                    " & vbNewLine _
                                                    & "        INNER JOIN (                                                                " & vbNewLine _
                                                    & "                      SELECT D05_01.SYS_ENT_DATE + D05_01.SYS_ENT_TIME AS RIREKIMAKE" & vbNewLine _
                                                    & "                            ,D01_01.NRS_BR_CD                          AS NRS_BR_CD " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_L                          AS CUST_CD_L " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_M                          AS CUST_CD_M " & vbNewLine _
                                                    & "                        FROM $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                       " & vbNewLine _
                                                    & "                       INNER JOIN $LM_TRN$..D_ZAI_TRS  D01_01                       " & vbNewLine _
                                                    & "                          ON D05_01.NRS_BR_CD        = D01_01.NRS_BR_CD             " & vbNewLine _
                                                    & "                         AND D05_01.ZAI_REC_NO       = D01_01.ZAI_REC_NO            " & vbNewLine _
                                                    & "                         AND D01_01.SYS_DEL_FLG      = '0'                          " & vbNewLine _
                                                    & "                       WHERE D05_01.RIREKI_DATE      = @RIREKI_DATE                 " & vbNewLine _
                                                    & "                         AND D05_01.SYS_DEL_FLG      = '0'                          " & vbNewLine _
                                                    & "                    GROUP BY D05_01.SYS_ENT_DATE + D05_01.SYS_ENT_TIME              " & vbNewLine _
                                                    & "                            ,D01_01.NRS_BR_CD                                       " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_L                                       " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_M                                       " & vbNewLine _
                                                    & "                   )                    D05_01                                      " & vbNewLine _
                                                    & "           ON C01_01.NRS_BR_CD        = D05_01.NRS_BR_CD                            " & vbNewLine _
                                                    & "          AND C01_01.CUST_CD_L        = D05_01.CUST_CD_L                            " & vbNewLine _
                                                    & "          AND C01_01.CUST_CD_M        = D05_01.CUST_CD_M                            " & vbNewLine _
                                                    & "          AND C01_01.SYS_ENT_DATE                                                   " & vbNewLine _
                                                    & "               + C01_01.SYS_ENT_TIME >= D05_01.RIREKIMAKE                           " & vbNewLine _
                                                    & "        WHERE C01_01.NRS_BR_CD        = @NRS_BR_CD                                  " & vbNewLine _
                                                    & "          AND C01_01.OUTKA_PLAN_DATE <= @RIREKI_DATE                                " & vbNewLine _
                                                    & "          AND C01_01.OUTKA_PLAN_DATE <= @HOUKOKU_DATE                                " & vbNewLine _
                                                    & "          AND C01_01.CUST_CD_L        = @CUST_CD_L                                  " & vbNewLine _
                                                    & "          AND C01_01.CUST_CD_M        = @CUST_CD_M                                  " & vbNewLine _
                                                    & "          AND C01_01.SYS_DEL_FLG      = '0'                                         " & vbNewLine _
                                                    & "          AND C01_01.OUTKA_STATE_KB  >= '60'                                        " & vbNewLine _
                                                    & "     ) OUTKA                                                                        " & vbNewLine _
                                                    & ",    (                                                                              " & vbNewLine _
                                                    & "       SELECT COUNT(D02_01.NRS_BR_CD) AS CNT                                        " & vbNewLine _
                                                    & "         FROM $LM_TRN$..D_IDO_TRS         D02_01                                    " & vbNewLine _
                                                    & "        INNER JOIN (                                                                " & vbNewLine _
                                                    & "                      SELECT D05_01.SYS_ENT_DATE + D05_01.SYS_ENT_TIME AS RIREKIMAKE" & vbNewLine _
                                                    & "                            ,D01_01.NRS_BR_CD                          AS NRS_BR_CD " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_L                          AS CUST_CD_L " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_M                          AS CUST_CD_M " & vbNewLine _
                                                    & "                        FROM $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                       " & vbNewLine _
                                                    & "                       INNER JOIN $LM_TRN$..D_ZAI_TRS  D01_01                       " & vbNewLine _
                                                    & "                          ON D05_01.NRS_BR_CD        = D01_01.NRS_BR_CD             " & vbNewLine _
                                                    & "                         AND D05_01.ZAI_REC_NO       = D01_01.ZAI_REC_NO            " & vbNewLine _
                                                    & "                         AND D01_01.SYS_DEL_FLG      = '0'                          " & vbNewLine _
                                                    & "                       WHERE D05_01.RIREKI_DATE      = @RIREKI_DATE                 " & vbNewLine _
                                                    & "                         AND D05_01.SYS_DEL_FLG      = '0'                          " & vbNewLine _
                                                    & "                    GROUP BY D05_01.SYS_ENT_DATE + D05_01.SYS_ENT_TIME              " & vbNewLine _
                                                    & "                            ,D01_01.NRS_BR_CD                                       " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_L                                       " & vbNewLine _
                                                    & "                            ,D01_01.CUST_CD_M                                       " & vbNewLine _
                                                    & "                   )                    D05_01                                      " & vbNewLine _
                                                    & "           ON D02_01.NRS_BR_CD        = D05_01.NRS_BR_CD                            " & vbNewLine _
                                                    & "          AND D02_01.SYS_ENT_DATE                                                   " & vbNewLine _
                                                    & "               + D02_01.SYS_ENT_TIME >= D05_01.RIREKIMAKE                           " & vbNewLine _
                                                    & "        WHERE D02_01.NRS_BR_CD        = @NRS_BR_CD                                  " & vbNewLine _
                                                    & "          AND D02_01.IDO_DATE        <= @RIREKI_DATE                                " & vbNewLine _
                                                    & "          AND D02_01.IDO_DATE        <= @HOUKOKU_DATE                                " & vbNewLine _
                                                    & "          AND D05_01.CUST_CD_L        = @CUST_CD_L                                  " & vbNewLine _
                                                    & "          AND D05_01.CUST_CD_M        = @CUST_CD_M                                  " & vbNewLine _
                                                    & "          AND D02_01.SYS_DEL_FLG      = '0'                                         " & vbNewLine _
                                                    & "     ) IDO                                                                          " & vbNewLine

#End Region

#Region "Main"

#Region "LMI500"

    Private Const SQL_SELECT_LMI500_001 As String = "SELECT                                                                 " & vbNewLine _
                                                  & " MAIN.GOODS_CD_CUST                                    AS GOODS_CD_CUST" & vbNewLine _
                                                  & ",MAIN.CUST_COST_CD2                                    AS SOURCD       " & vbNewLine _
                                                  & ",MAIN.GMC                                              AS GMC          " & vbNewLine _
                                                  & ",MAIN.GOODS_NM                                         AS GOODS_NM     " & vbNewLine _
                                                  & ",MAIN.REMARK_OUT                                       AS REMARK_OUT   " & vbNewLine _
                                                  & ",MAIN.LOT_NO                                           AS LOT          " & vbNewLine _
                                                  & ",MAIN.GOODS_COND_NM_3                                  AS SERIAL       " & vbNewLine _
                                                  & ",SUM( MAIN.ZAI_NB * 1000 )                             AS ZAI_NB       " & vbNewLine _
                                                  & ",'EA'                                                  AS ZAI_NB_UT    " & vbNewLine _
                                                  & ",SUM( ISNULL(KBN_01.KBN_NM3,'1000.00') * MAIN.ZAI_QT ) AS ZAI_QT       " & vbNewLine _
                                                  & ",CASE WHEN MAIN.STD_IRIME_UT IN ( 'L' , 'PT' , 'QT' , 'GL' )                 " & vbNewLine _
                                                  & "      THEN 'LT'                                                        " & vbNewLine _
                                                  & "      WHEN MAIN.STD_IRIME_UT = 'KG'                                    " & vbNewLine _
                                                  & "      THEN 'KG'                                                        " & vbNewLine _
                                                  & "      ELSE 'EA'                                                        " & vbNewLine _
                                                  & "  END                                                  AS ZAI_QT_UT    " & vbNewLine _
                                                  & ",CASE WHEN MAIN.OFB_KB = '02'                                          " & vbNewLine _
                                                  & "      THEN 'OFFBOOK'                                                   " & vbNewLine _
                                                  & "      ELSE ISNULL(KBN_02.KBN_NM1,'=FREE=')                             " & vbNewLine _
                                                  & "  END                                                  AS JYOTAI       " & vbNewLine _
                                                  & ",MAIN.SEARCH_KEY_1                                     AS FRB          " & vbNewLine _
                                                  & ",MAIN.INKO_DATE                                        AS INKO_DATE    " & vbNewLine _
                                                  & ",MAIN.CUST_CD_S                                        AS CUST_CD_S    " & vbNewLine _
                                                  & " FROM (                                                                " & vbNewLine


    'START YANAI 要望番号953
    'Private Const SQL_SELECT_LMI500_002 As String = "      ) MAIN                                                           " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN   KBN_01                                    " & vbNewLine _
    '                                              & "   ON MAIN.STD_IRIME_UT    = KBN_01.KBN_CD                             " & vbNewLine _
    '                                              & "  AND KBN_01.KBN_GROUP_CD  = 'Y001'                                    " & vbNewLine _
    '                                              & "  AND KBN_01.SYS_DEL_FLG   = '0'                                       " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN   KBN_02                                    " & vbNewLine _
    '                                              & "   ON SUBSTRING(MAIN.GOODS_COND_NM_3,1,1) = KBN_02.KBN_CD                             " & vbNewLine _
    '                                              & "  AND KBN_02.KBN_GROUP_CD  = 'D007'                                    " & vbNewLine _
    '                                              & "  AND KBN_02.SYS_DEL_FLG   = '0'                                       " & vbNewLine _
    '                                              & "WHERE MAIN.CUST_CD_L       = @CUST_CD_L                                " & vbNewLine _
    '                                              & "  AND MAIN.CUST_CD_M       = @CUST_CD_M                                " & vbNewLine _
    '                                              & "  AND MAIN.CUST_CD_S      IN ( '00' , '01' )                           " & vbNewLine _
    '                                              & "GROUP BY MAIN.GOODS_CD_CUST                                            " & vbNewLine _
    '                                              & "        ,MAIN.CUST_COST_CD2                                            " & vbNewLine _
    '                                              & "        ,MAIN.GMC                                                      " & vbNewLine _
    '                                              & "        ,MAIN.GOODS_NM                                                 " & vbNewLine _
    '                                              & "        ,MAIN.REMARK_OUT                                               " & vbNewLine _
    '                                              & "        ,MAIN.LOT_NO                                                   " & vbNewLine _
    '                                              & "        ,MAIN.GOODS_COND_NM_3                                          " & vbNewLine _
    '                                              & "        ,MAIN.STD_IRIME_UT                                             " & vbNewLine _
    '                                              & "        ,MAIN.OFB_KB                                                   " & vbNewLine _
    '                                              & "        ,KBN_02.KBN_NM1                                                " & vbNewLine _
    '                                              & "        ,MAIN.SEARCH_KEY_1                                             " & vbNewLine _
    '                                              & "        ,MAIN.INKO_DATE                                                " & vbNewLine _
    '                                              & "        ,MAIN.CUST_CD_S                                                " & vbNewLine _
    '                                              & "HAVING SUM( MAIN.ZAI_NB ) <> 0                                         " & vbNewLine _
    '                                              & "ORDER BY MAIN.CUST_CD_S                                                " & vbNewLine _
    '                                              & "        ,MAIN.GOODS_CD_CUST                                            " & vbNewLine _
    '                                              & "        ,MAIN.CUST_COST_CD2                                            " & vbNewLine _
    '                                              & "        ,MAIN.GMC                                                      " & vbNewLine _
    '                                              & "        ,MAIN.GOODS_NM                                                 " & vbNewLine
    Private Const SQL_SELECT_LMI500_002 As String = "      ) MAIN                                                           " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN   KBN_01                                    " & vbNewLine _
                                                  & "   ON MAIN.STD_IRIME_UT    = KBN_01.KBN_CD                             " & vbNewLine _
                                                  & "  AND KBN_01.KBN_GROUP_CD  = 'Y001'                                    " & vbNewLine _
                                                  & "  AND KBN_01.SYS_DEL_FLG   = '0'                                       " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN   KBN_02                                    " & vbNewLine _
                                                  & "   ON SUBSTRING(MAIN.GOODS_COND_NM_3,1,1) = KBN_02.KBN_CD                             " & vbNewLine _
                                                  & "  AND KBN_02.KBN_GROUP_CD  = 'D007'                                    " & vbNewLine _
                                                  & "  AND KBN_02.SYS_DEL_FLG   = '0'                                       " & vbNewLine _
                                                  & "WHERE MAIN.CUST_CD_L       = @CUST_CD_L                                " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_M       = @CUST_CD_M                                " & vbNewLine _
                                                  & "GROUP BY MAIN.GOODS_CD_CUST                                            " & vbNewLine _
                                                  & "        ,MAIN.CUST_COST_CD2                                            " & vbNewLine _
                                                  & "        ,MAIN.GMC                                                      " & vbNewLine _
                                                  & "        ,MAIN.GOODS_NM                                                 " & vbNewLine _
                                                  & "        ,MAIN.REMARK_OUT                                               " & vbNewLine _
                                                  & "        ,MAIN.LOT_NO                                                   " & vbNewLine _
                                                  & "        ,MAIN.GOODS_COND_NM_3                                          " & vbNewLine _
                                                  & "        ,MAIN.STD_IRIME_UT                                             " & vbNewLine _
                                                  & "        ,MAIN.OFB_KB                                                   " & vbNewLine _
                                                  & "        ,KBN_02.KBN_NM1                                                " & vbNewLine _
                                                  & "        ,MAIN.SEARCH_KEY_1                                             " & vbNewLine _
                                                  & "        ,MAIN.INKO_DATE                                                " & vbNewLine _
                                                  & "        ,MAIN.CUST_CD_S                                                " & vbNewLine _
                                                  & "HAVING SUM( MAIN.ZAI_NB ) <> 0                                         " & vbNewLine _
                                                  & "ORDER BY MAIN.CUST_CD_S                                                " & vbNewLine _
                                                  & "        ,MAIN.GOODS_CD_CUST                                            " & vbNewLine _
                                                  & "        ,MAIN.CUST_COST_CD2                                            " & vbNewLine _
                                                  & "        ,MAIN.GMC                                                      " & vbNewLine _
                                                  & "        ,MAIN.GOODS_NM                                                 " & vbNewLine
    'END YANAI 要望番号953

#End Region

#Region "LMI501"

    Private Const SQL_SELECT_LMI501_001 As String = "SELECT                                                    " & vbNewLine _
                                                  & " MAIN.WH_CD                        AS      WH_CD          " & vbNewLine _
                                                  & ",MAIN.WH_NM                        AS      WH_NM          " & vbNewLine _
                                                  & ",MAIN.SEIQTO_CD                    AS      SEIQTO_CD      " & vbNewLine _
                                                  & ",MAIN.SEIQTO_NM                    AS      SEIQTO_NM      " & vbNewLine _
                                                  & ",MAIN.CUST_CD_L                    AS      CUST_CD_L      " & vbNewLine _
                                                  & ",MAIN.CUST_CD_M                    AS      CUST_CD_M      " & vbNewLine _
                                                  & ",MAIN.CUST_CD_S                    AS      CUST_CD_S      " & vbNewLine _
                                                  & ",MAIN.CUST_CD_SS                   AS      CUST_CD_SS     " & vbNewLine _
                                                  & ",MAIN.CUST_NM_L                    AS      CUST_NM_L      " & vbNewLine _
                                                  & ",MAIN.CUST_NM_M                    AS      CUST_NM_M      " & vbNewLine _
                                                  & ",MAIN.CUST_NM_S                    AS      CUST_NM_S      " & vbNewLine _
                                                  & ",MAIN.CUST_NM_SS                   AS      CUST_NM_SS     " & vbNewLine _
                                                  & ",MAIN.SEARCH_KEY_1                 AS      SEARCH_KEY_1   " & vbNewLine _
                                                  & ",MAIN.SEARCH_KEY_2                 AS      SEARCH_KEY_2   " & vbNewLine _
                                                  & ",MAIN.CUST_COST_CD1                AS      CUST_COST_CD1  " & vbNewLine _
                                                  & ",MAIN.CUST_COST_CD2                AS      CUST_COST_CD2  " & vbNewLine _
                                                  & ",MAIN.GOODS_CD_CUST                AS      GOODS_CD_NRS   " & vbNewLine _
                                                  & ",MAIN.GOODS_NM                     AS      GOODS_NM       " & vbNewLine _
                                                  & ",MAIN.LOT_NO                       AS      LOT_NO         " & vbNewLine _
                                                  & ",MAIN.SERIAL_NO                    AS      SERIAL_NO      " & vbNewLine _
                                                  & ",MAIN.INKO_DATE                    AS      INKO_DATE      " & vbNewLine _
                                                  & ",MAIN.GOODS_COND_NM_1              AS      GOODS_COND_NM_1" & vbNewLine _
                                                  & ",MAIN.GOODS_COND_NM_2              AS      GOODS_COND_NM_2" & vbNewLine _
                                                  & ",MAIN.GOODS_COND_NM_3              AS      GOODS_COND_NM_3" & vbNewLine _
                                                  & ",CASE WHEN MAIN.OFB_KB = '02'                             " & vbNewLine _
                                                  & "      THEN 'OFFBOOK'                                      " & vbNewLine _
                                                  & "      ELSE ISNULL(KBN_01.KBN_NM1,'=FREE=')                " & vbNewLine _
                                                  & "  END                              AS      GOODS_COND_FREE" & vbNewLine _
                                                  & ",MAIN.OFB_NM                       AS      OFB            " & vbNewLine _
                                                  & ",MAIN.SPD_NM                       AS      SPD            " & vbNewLine _
                                                  & ",MAIN.TAX_NM                       AS      TAX            " & vbNewLine _
                                                  & ",KBN_02.KBN_NM8                    AS      SYOBO          " & vbNewLine _
                                                  & ",KBN_02.KBN_NM9                    AS      SYOBO_SBT      " & vbNewLine _
                                                  & ",MAIN.REMARK_OUT                   AS      REMARK_OUT     " & vbNewLine _
                                                  & ",MAIN.NB_UT                        AS      NB_UT          " & vbNewLine _
                                                  & ",MAIN.IRIME                        AS      IRIME          " & vbNewLine _
                                                  & ",MAIN.STD_IRIME_UT                 AS      STD_IRIME_UT   " & vbNewLine _
                                                  & ",SUM(MAIN.ZAI_NB)                  AS      ZAI_NB         " & vbNewLine _
                                                  & ",SUM(MAIN.ZAI_QT)                  AS      ZAI_QT         " & vbNewLine _
                                                  & ",MAIN.PKG_NB                       AS      PKG_NB         " & vbNewLine _
                                                  & ",MAIN.PKG_UT                       AS      PKG_UT         " & vbNewLine _
                                                  & ",MAIN.REMARK                       AS      REMARK         " & vbNewLine _
                                                  & " FROM (                                                   " & vbNewLine

    Private Const SQL_SELECT_LMI501_002 As String = "      ) MAIN                                              " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN   KBN_01                       " & vbNewLine _
                                                  & "   ON MAIN.GOODS_COND_NM_3 = KBN_01.KBN_CD                " & vbNewLine _
                                                  & "  AND KBN_01.KBN_GROUP_CD  = 'D007'                       " & vbNewLine _
                                                  & "  AND KBN_01.SYS_DEL_FLG   = '0'                          " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN   KBN_02                       " & vbNewLine _
                                                  & "   ON KBN_02.KBN_GROUP_CD  = 'D006'                       " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_L       = KBN_02.KBN_NM2               " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_M       = KBN_02.KBN_NM3               " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_S       = KBN_02.KBN_NM4               " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_SS      = KBN_02.KBN_NM5               " & vbNewLine _
                                                  & "  AND KBN_02.SYS_DEL_FLG   = '0'                          " & vbNewLine

    Private Const SQL_SELECT_LMI501_FUL As String = "INNER JOIN $LM_MST$..Z_KBN   KBN_03                       " & vbNewLine _
                                                  & "   ON KBN_03.KBN_GROUP_CD  = 'D006'                       " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_L       = KBN_03.KBN_NM2               " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_M       = KBN_03.KBN_NM3               " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_S       = KBN_03.KBN_NM4               " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_SS      = KBN_03.KBN_NM5               " & vbNewLine _
                                                  & "  AND KBN_03.SYS_DEL_FLG   = '0'                          " & vbNewLine

    Private Const SQL_SELECT_LMI501_003 As String = "GROUP BY MAIN.WH_CD                                       " & vbNewLine _
                                                  & "        ,MAIN.WH_NM                                       " & vbNewLine _
                                                  & "        ,MAIN.SEIQTO_CD                                   " & vbNewLine _
                                                  & "        ,MAIN.SEIQTO_NM                                   " & vbNewLine _
                                                  & "        ,MAIN.CUST_CD_L                                   " & vbNewLine _
                                                  & "        ,MAIN.CUST_CD_M                                   " & vbNewLine _
                                                  & "        ,MAIN.CUST_CD_S                                   " & vbNewLine _
                                                  & "        ,MAIN.CUST_CD_SS                                  " & vbNewLine _
                                                  & "        ,MAIN.CUST_NM_L                                   " & vbNewLine _
                                                  & "        ,MAIN.CUST_NM_M                                   " & vbNewLine _
                                                  & "        ,MAIN.CUST_NM_S                                   " & vbNewLine _
                                                  & "        ,MAIN.CUST_NM_SS                                  " & vbNewLine _
                                                  & "        ,MAIN.SEARCH_KEY_1                                " & vbNewLine _
                                                  & "        ,MAIN.SEARCH_KEY_2                                " & vbNewLine _
                                                  & "        ,MAIN.CUST_COST_CD1                               " & vbNewLine _
                                                  & "        ,MAIN.CUST_COST_CD2                               " & vbNewLine _
                                                  & "        ,MAIN.GOODS_CD_CUST                               " & vbNewLine _
                                                  & "        ,MAIN.GOODS_NM                                    " & vbNewLine _
                                                  & "        ,MAIN.LOT_NO                                      " & vbNewLine _
                                                  & "        ,MAIN.SERIAL_NO                                   " & vbNewLine _
                                                  & "        ,MAIN.INKO_DATE                                   " & vbNewLine _
                                                  & "        ,MAIN.GOODS_COND_NM_1                             " & vbNewLine _
                                                  & "        ,MAIN.GOODS_COND_NM_2                             " & vbNewLine _
                                                  & "        ,MAIN.GOODS_COND_NM_3                             " & vbNewLine _
                                                  & "        ,MAIN.OFB_KB                                      " & vbNewLine _
                                                  & "        ,KBN_01.KBN_NM1                                   " & vbNewLine _
                                                  & "        ,MAIN.OFB_NM                                      " & vbNewLine _
                                                  & "        ,MAIN.SPD_NM                                      " & vbNewLine _
                                                  & "        ,MAIN.TAX_NM                                      " & vbNewLine _
                                                  & "        ,KBN_02.KBN_NM8                                   " & vbNewLine _
                                                  & "        ,KBN_02.KBN_NM9                                   " & vbNewLine _
                                                  & "        ,MAIN.REMARK_OUT                                  " & vbNewLine _
                                                  & "        ,MAIN.NB_UT                                       " & vbNewLine _
                                                  & "        ,MAIN.IRIME                                       " & vbNewLine _
                                                  & "        ,MAIN.STD_IRIME_UT                                " & vbNewLine _
                                                  & "        ,MAIN.PKG_NB                                      " & vbNewLine _
                                                  & "        ,MAIN.PKG_UT                                      " & vbNewLine _
                                                  & "        ,MAIN.REMARK                                      " & vbNewLine _
                                                  & "ORDER BY MAIN.WH_CD                                       " & vbNewLine _
                                                  & "        ,MAIN.WH_NM                                       " & vbNewLine


#End Region

#Region "LMI502"

    Private Const SQL_SELECT_LMI502_001 As String = "SELECT                                                  " & vbNewLine _
                                                  & " @PLANT_CD                         AS      PLANT        " & vbNewLine _
                                                  & ",KBN_01.KBN_NM9                    AS      LOCATION     " & vbNewLine _
                                                  & ",CASE ISNUMERIC(MAIN.GOODS_CD_CUST) WHEN 1 THEN RIGHT('000000000000000000' + MAIN.GOODS_CD_CUST,18) ELSE MAIN.GOODS_CD_CUST END                AS      GOODS_CD_CUST" & vbNewLine _
                                                  & ",MAIN.LOT_NO                       AS      LOT          " & vbNewLine _
                                                  & ",SUM(MAIN.ZAI_NB)                  AS      NB           " & vbNewLine _
                                                  & ",SUM(MAIN.ZAI_QT)                  AS      QT           " & vbNewLine _
                                                  & " FROM (                                                 " & vbNewLine

    'START YANAI 要望番号413
    'Private Const SQL_SELECT_LMI502_002 As String = "      ) MAIN                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN   KBN_01                     " & vbNewLine _
    '                                              & "   ON KBN_01.KBN_GROUP_CD  = 'D006'                     " & vbNewLine _
    '                                              & "  AND MAIN.CUST_CD_L       = KBN_01.KBN_NM2             " & vbNewLine _
    '                                              & "  AND MAIN.CUST_CD_M       = KBN_01.KBN_NM3             " & vbNewLine _
    '                                              & "  AND MAIN.CUST_CD_S       = KBN_01.KBN_NM4             " & vbNewLine _
    '                                              & "  AND MAIN.CUST_CD_SS      = KBN_01.KBN_NM5             " & vbNewLine _
    '                                              & "  AND KBN_01.SYS_DEL_FLG   = '0'                        " & vbNewLine _
    '                                              & "WHERE MAIN.CUST_CD_L       = @CUST_CD_L                 " & vbNewLine _
    '                                              & "  AND MAIN.CUST_CD_M       = @CUST_CD_M                 " & vbNewLine _
    '                                              & "  AND MAIN.CUST_CD_S       = @CUST_CD_S                 " & vbNewLine
    Private Const SQL_SELECT_LMI502_002 As String = "      ) MAIN                                            " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN   KBN_01                     " & vbNewLine _
                                                  & "   ON KBN_01.KBN_GROUP_CD  = 'D006'                     " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_L       = KBN_01.KBN_NM2             " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_M       = KBN_01.KBN_NM3             " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_S       = KBN_01.KBN_NM4             " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_SS      = KBN_01.KBN_NM5             " & vbNewLine _
                                                  & "  AND KBN_01.SYS_DEL_FLG   = '0'                        " & vbNewLine _
                                                  & "WHERE MAIN.CUST_CD_L       = @CUST_CD_L                 " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_M       = @CUST_CD_M                 " & vbNewLine _
                                                  & "  AND MAIN.CUST_CD_S       = @CUST_CD_S                 " & vbNewLine _
                                                  & "  AND KBN_01.KBN_NM1       = @NRS_BR_CD                 " & vbNewLine _
                                                  & "  AND KBN_01.KBN_NM9       IS NOT NULL                  " & vbNewLine
    'END YANAI 要望番号413

    Private Const SQL_SELECT_LMI502_003 As String = "GROUP BY KBN_01.KBN_NM9                                 " & vbNewLine _
                                                  & "        ,MAIN.GOODS_CD_CUST                             " & vbNewLine _
                                                  & "        ,MAIN.LOT_NO                                    " & vbNewLine _
                                                  & "HAVING SUM(MAIN.ZAI_NB) <> 0                            " & vbNewLine _
                                                  & "    OR SUM(MAIN.ZAI_QT) <> 0                            " & vbNewLine _
                                                  & "ORDER BY KBN_01.KBN_NM9                                 " & vbNewLine _
                                                  & "        ,MAIN.GOODS_CD_CUST                             " & vbNewLine _
                                                  & "        ,MAIN.LOT_NO                                    " & vbNewLine

#End Region

#Region "D_ZAI_ZAN_JITSU"

    'START YANAI 要望番号769
    'Private Const SQL_SELECT_ZAI_RIREKI As String = "SELECT                                                                                              " & vbNewLine _
    '                                              & " D05_01.ZAI_REC_NO                 AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                              & ",D01_01.WH_CD                      AS      WH_CD                                                    " & vbNewLine _
    '                                              & ",M03_01.WH_NM                      AS      WH_NM                                                    " & vbNewLine _
    '                                              & ",M07_01.HOKAN_SEIQTO_CD            AS      SEIQTO_CD                                                " & vbNewLine _
    '                                              & ",M06_01.SEIQTO_NM                  AS      SEIQTO_NM                                                " & vbNewLine _
    '                                              & ",D01_01.CUST_CD_L                  AS      CUST_CD_L                                                " & vbNewLine _
    '                                              & ",D01_01.CUST_CD_M                  AS      CUST_CD_M                                                " & vbNewLine _
    '                                              & ",M08_01.CUST_CD_S                  AS      CUST_CD_S                                                " & vbNewLine _
    '                                              & ",M08_01.CUST_CD_SS                 AS      CUST_CD_SS                                               " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_L                  AS      CUST_NM_L                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_M                  AS      CUST_NM_M                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_S                  AS      CUST_NM_S                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_SS                 AS      CUST_NM_SS                                               " & vbNewLine _
    '                                              & ",M08_01.SEARCH_KEY_1               AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                              & ",M08_01.SEARCH_KEY_2               AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                              & ",M08_01.CUST_COST_CD1              AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                              & ",M08_01.CUST_COST_CD2              AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                              & ",M08_01.GOODS_CD_CUST              AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                              & ",M08_01.GOODS_NM_1                 AS      GOODS_NM                                                 " & vbNewLine _
    '                                              & ",D01_01.LOT_NO                     AS      LOT_NO                                                   " & vbNewLine _
    '                                              & ",D01_01.SERIAL_NO                  AS      SERIAL_NO                                                " & vbNewLine _
    '                                              & ",D01_01.INKO_DATE                  AS      INKO_DATE                                                " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_1            AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_2            AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_3            AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                              & ",KBN_01.KBN_NM1                    AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                              & ",KBN_02.KBN_NM1                    AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                              & ",M26_01.JOTAI_NM                   AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                              & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                               " & vbNewLine _
    '                                              & "      THEN D01_01.OFB_KB                                                                            " & vbNewLine _
    '                                              & "      ELSE KBN_03.KBN_NM7                                                                           " & vbNewLine _
    '                                              & "  END                              AS      OFB_KB                                                   " & vbNewLine _
    '                                              & ",KBN_04.KBN_NM1                    AS      OFB_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.SPD_KB                     AS      SPD_KB                                                   " & vbNewLine _
    '                                              & ",KBN_05.KBN_NM1                    AS      SPD_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.TAX_KB                     AS      TAX_KB                                                   " & vbNewLine _
    '                                              & ",KBN_06.KBN_NM1                    AS      TAX_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.REMARK_OUT                 AS      REMARK_OUT                                               " & vbNewLine _
    '                                              & ",ISNULL(M08_01.NB_UT,0)            AS      NB_UT                                            " & vbNewLine _
    '                                              & ",CASE WHEN ABS(D05_01.PORA_ZAI_NB) < D01_01.IRIME                                                   " & vbNewLine _
    '                                              & "      THEN ABS(D05_01.PORA_ZAI_NB)                                                                  " & vbNewLine _
    '                                              & "      ELSE D01_01.IRIME                                                                             " & vbNewLine _
    '                                              & "  END                              AS      IRIME                                                    " & vbNewLine _
    '                                              & ",M08_01.STD_IRIME_UT               AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                              & ",D05_01.PORA_ZAI_NB                AS      ZAI_NB                                                   " & vbNewLine _
    '                                              & ",D05_01.PORA_ZAI_QT                AS      ZAI_QT                                                   " & vbNewLine _
    '                                              & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                              " & vbNewLine _
    '                                              & "      THEN 1                                                                                        " & vbNewLine _
    '                                              & "      ELSE M08_01.PKG_NB                                                                            " & vbNewLine _
    '                                              & "  END                              AS      PKG_NB                                                   " & vbNewLine _
    '                                              & ",M08_01.PKG_UT                     AS      PKG_UT                                                   " & vbNewLine _
    '                                              & ",D01_01.REMARK                     AS      REMARK                                                   " & vbNewLine _
    '                                              & ",M60_01.SET_NAIYO                  AS      GMC                                                      " & vbNewLine _
    '                                              & "FROM (                                                                                              " & vbNewLine _
    '                                              & "       SELECT D05_01.NRS_BR_CD        AS NRS_BR_CD                                                  " & vbNewLine _
    '                                              & "             ,D05_01.ZAI_REC_NO       AS ZAI_REC_NO                                                 " & vbNewLine _
    '                                              & "             ,SUM(D05_01.PORA_ZAI_NB) AS PORA_ZAI_NB                                                " & vbNewLine _
    '                                              & "             ,SUM(D05_01.PORA_ZAI_QT) AS PORA_ZAI_QT                                                " & vbNewLine _
    '                                              & "             ,D05_01.RIREKI_DATE      AS RIREKI_DATE                                                " & vbNewLine _
    '                                              & "         FROM $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                                                      " & vbNewLine _
    '                                              & "        WHERE D05_01.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                                              & "        GROUP BY D05_01.NRS_BR_CD                                                                   " & vbNewLine _
    '                                              & "                ,D05_01.ZAI_REC_NO                                                                  " & vbNewLine _
    '                                              & "                ,D05_01.RIREKI_DATE                                                                 " & vbNewLine _
    '                                              & "      )                              D05_01                                                         " & vbNewLine _
    '                                              & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                         " & vbNewLine _
    '                                              & "   ON D05_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D05_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                              " & vbNewLine _
    '                                              & "  AND D01_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                   " & vbNewLine _
    '                                              & "  AND M03_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                            " & vbNewLine _
    '                                              & "  AND M08_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                               " & vbNewLine _
    '                                              & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                               " & vbNewLine _
    '                                              & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                              " & vbNewLine _
    '                                              & "  AND M07_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                               " & vbNewLine _
    '                                              & "  AND M06_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                                " & vbNewLine _
    '                                              & "  AND M26_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN (                                                                                        " & vbNewLine _
    '                                              & "                           SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                              & "                                 ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                              & "                                 ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                              & "                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                              & "                       INNER JOIN (                                                                 " & vbNewLine _
    '                                              & "                                             SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                              & "                                                   ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                              & "                                                   ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                              & "                                               FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                              & "                                              WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                              & "                                                AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                              & "                                           GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                              & "                                                   ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                              & "                                  )                           M60_02                                " & vbNewLine _
    '                                              & "                               ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                              & "                              AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                              & "                              AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                              & "                            WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                              & "           )                         M60_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                         " & vbNewLine _
    '                                              & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                         " & vbNewLine _
    '                                              & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                         " & vbNewLine _
    '                                              & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                         " & vbNewLine _
    '                                              & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                         " & vbNewLine _
    '                                              & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                                 " & vbNewLine _
    '                                              & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                         " & vbNewLine _
    '                                              & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                          " & vbNewLine _
    '                                              & "           THEN D01_01.OFB_KB                                                                       " & vbNewLine _
    '                                              & "           ELSE KBN_03.KBN_NM7                                                                      " & vbNewLine _
    '                                              & "       END                         = KBN_04.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                         " & vbNewLine _
    '                                              & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                         " & vbNewLine _
    '                                              & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                         " & vbNewLine _
    '                                              & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                         " & vbNewLine _
    '                                              & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                         " & vbNewLine _
    '                                              & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & "WHERE D05_01.NRS_BR_CD             = @NRS_BR_CD                                                     " & vbNewLine _
    '                                              & "  AND   D05_01.RIREKI_DATE           = @RIREKI_DATE                                                   " & vbNewLine
    'START YANAI 要望番号1022
    'Private Const SQL_SELECT_ZAI_RIREKI As String = "SELECT                                                                                              " & vbNewLine _
    '                                              & " D05_01.ZAI_REC_NO                 AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                              & ",D01_01.WH_CD                      AS      WH_CD                                                    " & vbNewLine _
    '                                              & ",M03_01.WH_NM                      AS      WH_NM                                                    " & vbNewLine _
    '                                              & ",M07_01.HOKAN_SEIQTO_CD            AS      SEIQTO_CD                                                " & vbNewLine _
    '                                              & ",M06_01.SEIQTO_NM                  AS      SEIQTO_NM                                                " & vbNewLine _
    '                                              & ",D01_01.CUST_CD_L                  AS      CUST_CD_L                                                " & vbNewLine _
    '                                              & ",D01_01.CUST_CD_M                  AS      CUST_CD_M                                                " & vbNewLine _
    '                                              & ",M08_01.CUST_CD_S                  AS      CUST_CD_S                                                " & vbNewLine _
    '                                              & ",M08_01.CUST_CD_SS                 AS      CUST_CD_SS                                               " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_L                  AS      CUST_NM_L                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_M                  AS      CUST_NM_M                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_S                  AS      CUST_NM_S                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_SS                 AS      CUST_NM_SS                                               " & vbNewLine _
    '                                              & ",M08_01.SEARCH_KEY_1               AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                              & ",M08_01.SEARCH_KEY_2               AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                              & ",M08_01.CUST_COST_CD1              AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                              & ",M08_01.CUST_COST_CD2              AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                              & ",M08_01.GOODS_CD_CUST              AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                              & ",M08_01.GOODS_NM_1                 AS      GOODS_NM                                                 " & vbNewLine _
    '                                              & ",D01_01.LOT_NO                     AS      LOT_NO                                                   " & vbNewLine _
    '                                              & ",D01_01.SERIAL_NO                  AS      SERIAL_NO                                                " & vbNewLine _
    '                                              & ",D01_01.INKO_DATE                  AS      INKO_DATE                                                " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_1            AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_2            AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_3            AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                              & ",KBN_01.KBN_NM1                    AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                              & ",KBN_02.KBN_NM1                    AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                              & ",M26_01.JOTAI_NM                   AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                              & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                               " & vbNewLine _
    '                                              & "      THEN D01_01.OFB_KB                                                                            " & vbNewLine _
    '                                              & "      ELSE KBN_03.KBN_NM7                                                                           " & vbNewLine _
    '                                              & "  END                              AS      OFB_KB                                                   " & vbNewLine _
    '                                              & ",KBN_04.KBN_NM1                    AS      OFB_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.SPD_KB                     AS      SPD_KB                                                   " & vbNewLine _
    '                                              & ",KBN_05.KBN_NM1                    AS      SPD_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.TAX_KB                     AS      TAX_KB                                                   " & vbNewLine _
    '                                              & ",KBN_06.KBN_NM1                    AS      TAX_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.REMARK_OUT                 AS      REMARK_OUT                                               " & vbNewLine _
    '                                              & ",ISNULL(M08_01.NB_UT,0)            AS      NB_UT                                            " & vbNewLine _
    '                                              & ",CASE WHEN ABS(D05_01.PORA_ZAI_NB) < D01_01.IRIME                                                   " & vbNewLine _
    '                                              & "      THEN ABS(D05_01.PORA_ZAI_NB)                                                                  " & vbNewLine _
    '                                              & "      ELSE D01_01.IRIME                                                                             " & vbNewLine _
    '                                              & "  END                              AS      IRIME                                                    " & vbNewLine _
    '                                              & ",M08_01.STD_IRIME_UT               AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                              & ",D05_01.PORA_ZAI_NB                AS      ZAI_NB                                                   " & vbNewLine _
    '                                              & ",D05_01.PORA_ZAI_QT                AS      ZAI_QT                                                   " & vbNewLine _
    '                                              & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                              " & vbNewLine _
    '                                              & "      THEN 1                                                                                        " & vbNewLine _
    '                                              & "      ELSE M08_01.PKG_NB                                                                            " & vbNewLine _
    '                                              & "  END                              AS      PKG_NB                                                   " & vbNewLine _
    '                                              & ",M08_01.PKG_UT                     AS      PKG_UT                                                   " & vbNewLine _
    '                                              & ",D01_01.REMARK                     AS      REMARK                                                   " & vbNewLine _
    '                                              & ",M60_01.SET_NAIYO                  AS      GMC                                                      " & vbNewLine _
    '                                              & "FROM (                                                                                              " & vbNewLine _
    '                                              & "       SELECT D05_01.NRS_BR_CD        AS NRS_BR_CD                                                  " & vbNewLine _
    '                                              & "             ,D05_01.ZAI_REC_NO       AS ZAI_REC_NO                                                 " & vbNewLine _
    '                                              & "             ,SUM(D05_01.PORA_ZAI_NB) AS PORA_ZAI_NB                                                " & vbNewLine _
    '                                              & "             ,SUM(D05_01.PORA_ZAI_QT) AS PORA_ZAI_QT                                                " & vbNewLine _
    '                                              & "             ,D05_01.RIREKI_DATE      AS RIREKI_DATE                                                " & vbNewLine _
    '                                              & "         FROM $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                                                      " & vbNewLine _
    '                                              & "        WHERE D05_01.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                                              & "          AND D05_01.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
    '                                              & "          AND (D05_01.PORA_ZAI_NB <> 0                                                              " & vbNewLine _
    '                                              & "           OR D05_01.PORA_ZAI_QT <> 0)                                                              " & vbNewLine _
    '                                              & "          AND D05_01.RIREKI_DATE = @RIREKI_DATE                                                     " & vbNewLine _
    '                                              & "        GROUP BY D05_01.NRS_BR_CD                                                                   " & vbNewLine _
    '                                              & "                ,D05_01.ZAI_REC_NO                                                                  " & vbNewLine _
    '                                              & "                ,D05_01.RIREKI_DATE                                                                 " & vbNewLine _
    '                                              & "      )                              D05_01                                                         " & vbNewLine _
    '                                              & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                         " & vbNewLine _
    '                                              & "   ON D05_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D05_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                              " & vbNewLine _
    '                                              & "  AND D01_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                   " & vbNewLine _
    '                                              & "  AND M03_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                            " & vbNewLine _
    '                                              & "  AND M08_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                               " & vbNewLine _
    '                                              & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                               " & vbNewLine _
    '                                              & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                              " & vbNewLine _
    '                                              & "  AND M07_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                               " & vbNewLine _
    '                                              & "  AND M06_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                                " & vbNewLine _
    '                                              & "  AND M26_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN (                                                                                        " & vbNewLine _
    '                                              & "                           SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                              & "                                 ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                              & "                                 ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                              & "                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                              & "                       INNER JOIN (                                                                 " & vbNewLine _
    '                                              & "                                             SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                              & "                                                   ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                              & "                                                   ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                              & "                                               FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                              & "                                              WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                              & "                                                AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                              & "                                           GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                              & "                                                   ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                              & "                                  )                           M60_02                                " & vbNewLine _
    '                                              & "                               ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                              & "                              AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                              & "                              AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                              & "                            WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                              & "           )                         M60_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                         " & vbNewLine _
    '                                              & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                         " & vbNewLine _
    '                                              & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                         " & vbNewLine _
    '                                              & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                         " & vbNewLine _
    '                                              & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                         " & vbNewLine _
    '                                              & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                                 " & vbNewLine _
    '                                              & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                         " & vbNewLine _
    '                                              & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                          " & vbNewLine _
    '                                              & "           THEN D01_01.OFB_KB                                                                       " & vbNewLine _
    '                                              & "           ELSE KBN_03.KBN_NM7                                                                      " & vbNewLine _
    '                                              & "       END                         = KBN_04.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                         " & vbNewLine _
    '                                              & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                         " & vbNewLine _
    '                                              & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                         " & vbNewLine _
    '                                              & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                         " & vbNewLine _
    '                                              & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                         " & vbNewLine _
    '                                              & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & "WHERE D05_01.NRS_BR_CD             = @NRS_BR_CD                                                     " & vbNewLine _
    '                                              & "  AND   D05_01.RIREKI_DATE           = @RIREKI_DATE                                                   " & vbNewLine
    'START YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'Private Const SQL_SELECT_ZAI_RIREKI As String = "SELECT                                                                                              " & vbNewLine _
    '                                              & " D05_01.ZAI_REC_NO                 AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                              & ",D01_01.WH_CD                      AS      WH_CD                                                    " & vbNewLine _
    '                                              & ",M03_01.WH_NM                      AS      WH_NM                                                    " & vbNewLine _
    '                                              & ",M07_01.HOKAN_SEIQTO_CD            AS      SEIQTO_CD                                                " & vbNewLine _
    '                                              & ",M06_01.SEIQTO_NM                  AS      SEIQTO_NM                                                " & vbNewLine _
    '                                              & ",D01_01.CUST_CD_L                  AS      CUST_CD_L                                                " & vbNewLine _
    '                                              & ",D01_01.CUST_CD_M                  AS      CUST_CD_M                                                " & vbNewLine _
    '                                              & ",M08_01.CUST_CD_S                  AS      CUST_CD_S                                                " & vbNewLine _
    '                                              & ",M08_01.CUST_CD_SS                 AS      CUST_CD_SS                                               " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_L                  AS      CUST_NM_L                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_M                  AS      CUST_NM_M                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_S                  AS      CUST_NM_S                                                " & vbNewLine _
    '                                              & ",M07_01.CUST_NM_SS                 AS      CUST_NM_SS                                               " & vbNewLine _
    '                                              & ",M08_01.SEARCH_KEY_1               AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                              & ",M08_01.SEARCH_KEY_2               AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                              & ",M08_01.CUST_COST_CD1              AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                              & ",M08_01.CUST_COST_CD2              AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                              & ",M08_01.GOODS_CD_CUST              AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                              & ",M08_01.GOODS_NM_1                 AS      GOODS_NM                                                 " & vbNewLine _
    '                                              & ",D01_01.LOT_NO                     AS      LOT_NO                                                   " & vbNewLine _
    '                                              & ",D01_01.SERIAL_NO                  AS      SERIAL_NO                                                " & vbNewLine _
    '                                              & ",D01_01.INKO_DATE                  AS      INKO_DATE                                                " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_1            AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_2            AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                              & ",D01_01.GOODS_COND_KB_3            AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                              & ",KBN_01.KBN_NM1                    AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                              & ",KBN_02.KBN_NM1                    AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                              & ",M26_01.JOTAI_NM                   AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                              & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                               " & vbNewLine _
    '                                              & "      THEN D01_01.OFB_KB                                                                            " & vbNewLine _
    '                                              & "      ELSE KBN_03.KBN_NM7                                                                           " & vbNewLine _
    '                                              & "  END                              AS      OFB_KB                                                   " & vbNewLine _
    '                                              & ",KBN_04.KBN_NM1                    AS      OFB_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.SPD_KB                     AS      SPD_KB                                                   " & vbNewLine _
    '                                              & ",KBN_05.KBN_NM1                    AS      SPD_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.TAX_KB                     AS      TAX_KB                                                   " & vbNewLine _
    '                                              & ",KBN_06.KBN_NM1                    AS      TAX_NM                                                   " & vbNewLine _
    '                                              & ",D01_01.REMARK_OUT                 AS      REMARK_OUT                                               " & vbNewLine _
    '                                              & ",ISNULL(M08_01.NB_UT,0)            AS      NB_UT                                            " & vbNewLine _
    '                                              & ",CASE WHEN ABS(D05_01.PORA_ZAI_NB) < D01_01.IRIME                                                   " & vbNewLine _
    '                                              & "      THEN ABS(D05_01.PORA_ZAI_NB)                                                                  " & vbNewLine _
    '                                              & "      ELSE D01_01.IRIME                                                                             " & vbNewLine _
    '                                              & "  END                              AS      IRIME                                                    " & vbNewLine _
    '                                              & ",M08_01.STD_IRIME_UT               AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                              & ",D05_01.PORA_ZAI_NB                AS      ZAI_NB                                                   " & vbNewLine _
    '                                              & ",D05_01.PORA_ZAI_QT                AS      ZAI_QT                                                   " & vbNewLine _
    '                                              & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                              " & vbNewLine _
    '                                              & "      THEN 1                                                                                        " & vbNewLine _
    '                                              & "      ELSE M08_01.PKG_NB                                                                            " & vbNewLine _
    '                                              & "  END                              AS      PKG_NB                                                   " & vbNewLine _
    '                                              & ",M08_01.PKG_UT                     AS      PKG_UT                                                   " & vbNewLine _
    '                                              & ",D01_01.REMARK                     AS      REMARK                                                   " & vbNewLine _
    '                                              & ",M60_01.SET_NAIYO                  AS      GMC                                                      " & vbNewLine _
    '                                              & "FROM (                                                                                              " & vbNewLine _
    '                                              & "       SELECT D05_01.NRS_BR_CD        AS NRS_BR_CD                                                  " & vbNewLine _
    '                                              & "             ,D05_01.ZAI_REC_NO       AS ZAI_REC_NO                                                 " & vbNewLine _
    '                                              & "             ,SUM(D05_01.PORA_ZAI_NB) AS PORA_ZAI_NB                                                " & vbNewLine _
    '                                              & "             ,SUM(D05_01.PORA_ZAI_QT) AS PORA_ZAI_QT                                                " & vbNewLine _
    '                                              & "             ,D05_01.RIREKI_DATE      AS RIREKI_DATE                                                " & vbNewLine _
    '                                              & "         FROM $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                                                      " & vbNewLine _
    '                                              & "        WHERE D05_01.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                                              & "          AND D05_01.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
    '                                              & "          AND (D05_01.PORA_ZAI_NB <> 0                                                              " & vbNewLine _
    '                                              & "           OR D05_01.PORA_ZAI_QT <> 0)                                                              " & vbNewLine _
    '                                              & "          AND D05_01.RIREKI_DATE = @RIREKI_DATE                                                     " & vbNewLine _
    '                                              & "        GROUP BY D05_01.NRS_BR_CD                                                                   " & vbNewLine _
    '                                              & "                ,D05_01.ZAI_REC_NO                                                                  " & vbNewLine _
    '                                              & "                ,D05_01.RIREKI_DATE                                                                 " & vbNewLine _
    '                                              & "      )                              D05_01                                                         " & vbNewLine _
    '                                              & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                         " & vbNewLine _
    '                                              & "   ON D05_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D05_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                              " & vbNewLine _
    '                                              & "  AND D01_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                   " & vbNewLine _
    '                                              & "  AND M03_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                               " & vbNewLine _
    '                                              & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                               " & vbNewLine _
    '                                              & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                              " & vbNewLine _
    '                                              & "  AND M07_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                               " & vbNewLine _
    '                                              & "  AND M06_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                                " & vbNewLine _
    '                                              & "  AND M26_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN (                                                                                        " & vbNewLine _
    '                                              & "                           SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                              & "                                 ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                              & "                                 ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                              & "                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                              & "                       INNER JOIN (                                                                 " & vbNewLine _
    '                                              & "                                             SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                              & "                                                   ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                              & "                                                   ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                              & "                                               FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                              & "                                              WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                              & "                                                AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                              & "                                           GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                              & "                                                   ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                              & "                                  )                           M60_02                                " & vbNewLine _
    '                                              & "                               ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                              & "                              AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                              & "                              AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                              & "                            WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                              & "           )                         M60_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                               " & vbNewLine _
    '                                              & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                         " & vbNewLine _
    '                                              & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                         " & vbNewLine _
    '                                              & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                         " & vbNewLine _
    '                                              & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                         " & vbNewLine _
    '                                              & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                         " & vbNewLine _
    '                                              & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                         " & vbNewLine _
    '                                              & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                                 " & vbNewLine _
    '                                              & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                                 " & vbNewLine _
    '                                              & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                         " & vbNewLine _
    '                                              & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                          " & vbNewLine _
    '                                              & "           THEN D01_01.OFB_KB                                                                       " & vbNewLine _
    '                                              & "           ELSE KBN_03.KBN_NM7                                                                      " & vbNewLine _
    '                                              & "       END                         = KBN_04.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                         " & vbNewLine _
    '                                              & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                         " & vbNewLine _
    '                                              & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                         " & vbNewLine _
    '                                              & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                         " & vbNewLine _
    '                                              & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                  " & vbNewLine _
    '                                              & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                         " & vbNewLine _
    '                                              & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
    '                                              & "WHERE D05_01.NRS_BR_CD             = @NRS_BR_CD                                                     " & vbNewLine _
    '                                              & "  AND   D05_01.RIREKI_DATE           = @RIREKI_DATE                                                   " & vbNewLine
    Private Const SQL_SELECT_ZAI_RIREKI As String = "SELECT                                                                                              " & vbNewLine _
                                                  & " D05_01.ZAI_REC_NO                 AS      ZAI_REC_NO                                               " & vbNewLine _
                                                  & ",D01_01.WH_CD                      AS      WH_CD                                                    " & vbNewLine _
                                                  & ",M03_01.WH_NM                      AS      WH_NM                                                    " & vbNewLine _
                                                  & ",M07_01.HOKAN_SEIQTO_CD            AS      SEIQTO_CD                                                " & vbNewLine _
                                                  & ",M06_01.SEIQTO_NM                  AS      SEIQTO_NM                                                " & vbNewLine _
                                                  & ",D01_01.CUST_CD_L                  AS      CUST_CD_L                                                " & vbNewLine _
                                                  & ",D01_01.CUST_CD_M                  AS      CUST_CD_M                                                " & vbNewLine _
                                                  & ",M08_01.CUST_CD_S                  AS      CUST_CD_S                                                " & vbNewLine _
                                                  & ",M08_01.CUST_CD_SS                 AS      CUST_CD_SS                                               " & vbNewLine _
                                                  & ",M07_01.CUST_NM_L                  AS      CUST_NM_L                                                " & vbNewLine _
                                                  & ",M07_01.CUST_NM_M                  AS      CUST_NM_M                                                " & vbNewLine _
                                                  & ",M07_01.CUST_NM_S                  AS      CUST_NM_S                                                " & vbNewLine _
                                                  & ",M07_01.CUST_NM_SS                 AS      CUST_NM_SS                                               " & vbNewLine _
                                                  & ",M08_01.SEARCH_KEY_1               AS      SEARCH_KEY_1                                             " & vbNewLine _
                                                  & ",M08_01.SEARCH_KEY_2               AS      SEARCH_KEY_2                                             " & vbNewLine _
                                                  & ",M08_01.CUST_COST_CD1              AS      CUST_COST_CD1                                            " & vbNewLine _
                                                  & ",M08_01.CUST_COST_CD2              AS      CUST_COST_CD2                                            " & vbNewLine _
                                                  & ",M08_01.GOODS_CD_CUST              AS      GOODS_CD_CUST                                            " & vbNewLine _
                                                  & ",M08_01.GOODS_NM_1                 AS      GOODS_NM                                                 " & vbNewLine _
                                                  & ",D01_01.LOT_NO                     AS      LOT_NO                                                   " & vbNewLine _
                                                  & ",D01_01.SERIAL_NO                  AS      SERIAL_NO                                                " & vbNewLine _
                                                  & ",D01_01.INKO_DATE                  AS      INKO_DATE                                                " & vbNewLine _
                                                  & ",D01_01.GOODS_COND_KB_1            AS      GOODS_COND_KB_1                                          " & vbNewLine _
                                                  & ",D01_01.GOODS_COND_KB_2            AS      GOODS_COND_KB_2                                          " & vbNewLine _
                                                  & ",D01_01.GOODS_COND_KB_3            AS      GOODS_COND_KB_3                                          " & vbNewLine _
                                                  & ",KBN_01.KBN_NM1                    AS      GOODS_COND_NM_1                                          " & vbNewLine _
                                                  & ",KBN_02.KBN_NM1                    AS      GOODS_COND_NM_2                                          " & vbNewLine _
                                                  & ",M26_01.JOTAI_NM                   AS      GOODS_COND_NM_3                                          " & vbNewLine _
                                                  & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                               " & vbNewLine _
                                                  & "      THEN D01_01.OFB_KB                                                                            " & vbNewLine _
                                                  & "      ELSE KBN_03.KBN_NM7                                                                           " & vbNewLine _
                                                  & "  END                              AS      OFB_KB                                                   " & vbNewLine _
                                                  & ",KBN_04.KBN_NM1                    AS      OFB_NM                                                   " & vbNewLine _
                                                  & ",D01_01.SPD_KB                     AS      SPD_KB                                                   " & vbNewLine _
                                                  & ",KBN_05.KBN_NM1                    AS      SPD_NM                                                   " & vbNewLine _
                                                  & ",D01_01.TAX_KB                     AS      TAX_KB                                                   " & vbNewLine _
                                                  & ",KBN_06.KBN_NM1                    AS      TAX_NM                                                   " & vbNewLine _
                                                  & ",D01_01.REMARK_OUT                 AS      REMARK_OUT                                               " & vbNewLine _
                                                  & ",ISNULL(M08_01.NB_UT,0)            AS      NB_UT                                            " & vbNewLine _
                                                  & ",CASE WHEN ABS(D05_01.PORA_ZAI_NB) < D01_01.IRIME                                                   " & vbNewLine _
                                                  & "      THEN ABS(D05_01.PORA_ZAI_NB)                                                                  " & vbNewLine _
                                                  & "      ELSE D01_01.IRIME                                                                             " & vbNewLine _
                                                  & "  END                              AS      IRIME                                                    " & vbNewLine _
                                                  & ",M08_01.STD_IRIME_UT               AS      STD_IRIME_UT                                             " & vbNewLine _
                                                  & ",D05_01.PORA_ZAI_NB                AS      ZAI_NB                                                   " & vbNewLine _
                                                  & ",D05_01.PORA_ZAI_QT                AS      ZAI_QT                                                   " & vbNewLine _
                                                  & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                              " & vbNewLine _
                                                  & "      THEN 1                                                                                        " & vbNewLine _
                                                  & "      ELSE M08_01.PKG_NB                                                                            " & vbNewLine _
                                                  & "  END                              AS      PKG_NB                                                   " & vbNewLine _
                                                  & ",M08_01.PKG_UT                     AS      PKG_UT                                                   " & vbNewLine _
                                                  & ",D01_01.REMARK                     AS      REMARK                                                   " & vbNewLine _
                                                  & ",M60_01.SET_NAIYO                  AS      GMC                                                      " & vbNewLine _
                                                  & "FROM (                                                                                              " & vbNewLine _
                                                  & "       SELECT D05_01.NRS_BR_CD        AS NRS_BR_CD                                                  " & vbNewLine _
                                                  & "             ,D05_01.ZAI_REC_NO       AS ZAI_REC_NO                                                 " & vbNewLine _
                                                  & "             ,SUM(D05_01.PORA_ZAI_NB) AS PORA_ZAI_NB                                                " & vbNewLine _
                                                  & "             ,SUM(D05_01.PORA_ZAI_QT) AS PORA_ZAI_QT                                                " & vbNewLine _
                                                  & "             ,D05_01.RIREKI_DATE      AS RIREKI_DATE                                                " & vbNewLine _
                                                  & "         FROM $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                                                      " & vbNewLine _
                                                  & "        WHERE D05_01.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                                  & "          AND D05_01.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
                                                  & "          AND (D05_01.PORA_ZAI_NB <> 0                                                              " & vbNewLine _
                                                  & "           OR D05_01.PORA_ZAI_QT <> 0)                                                              " & vbNewLine _
                                                  & "          AND D05_01.RIREKI_DATE = @RIREKI_DATE                                                     " & vbNewLine _
                                                  & "        GROUP BY D05_01.NRS_BR_CD                                                                   " & vbNewLine _
                                                  & "                ,D05_01.ZAI_REC_NO                                                                  " & vbNewLine _
                                                  & "                ,D05_01.RIREKI_DATE                                                                 " & vbNewLine _
                                                  & "      )                              D05_01                                                         " & vbNewLine _
                                                  & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                         " & vbNewLine _
                                                  & "   ON D05_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                               " & vbNewLine _
                                                  & "  AND D05_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                              " & vbNewLine _
                                                  & "  AND D01_01.SYS_DEL_FLG           = '0'                                                            " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                         " & vbNewLine _
                                                  & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                   " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                         " & vbNewLine _
                                                  & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                               " & vbNewLine _
                                                  & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                            " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                         " & vbNewLine _
                                                  & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                               " & vbNewLine _
                                                  & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                               " & vbNewLine _
                                                  & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                               " & vbNewLine _
                                                  & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                               " & vbNewLine _
                                                  & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                              " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                         " & vbNewLine _
                                                  & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                               " & vbNewLine _
                                                  & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                               " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                         " & vbNewLine _
                                                  & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                               " & vbNewLine _
                                                  & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                               " & vbNewLine _
                                                  & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                                " & vbNewLine _
                                                  & " LEFT JOIN (                                                                                        " & vbNewLine _
                                                  & "                           SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                                  & "                                 ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                                  & "                                 ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
                                                  & "                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
                                                  & "                       INNER JOIN (                                                                 " & vbNewLine _
                                                  & "                                             SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
                                                  & "                                                   ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
                                                  & "                                                   ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
                                                  & "                                               FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
                                                  & "                                              WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
                                                  & "                                           GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
                                                  & "                                                   ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
                                                  & "                                  )                           M60_02                                " & vbNewLine _
                                                  & "                               ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
                                                  & "                              AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
                                                  & "                              AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
                                                  & "           )                         M60_01                                                         " & vbNewLine _
                                                  & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                               " & vbNewLine _
                                                  & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                            " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                         " & vbNewLine _
                                                  & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                  " & vbNewLine _
                                                  & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                         " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                         " & vbNewLine _
                                                  & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                  " & vbNewLine _
                                                  & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                         " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                         " & vbNewLine _
                                                  & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                         " & vbNewLine _
                                                  & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                                 " & vbNewLine _
                                                  & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                                 " & vbNewLine _
                                                  & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                                 " & vbNewLine _
                                                  & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                                 " & vbNewLine _
                                                  & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                                 " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                         " & vbNewLine _
                                                  & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                          " & vbNewLine _
                                                  & "           THEN D01_01.OFB_KB                                                                       " & vbNewLine _
                                                  & "           ELSE KBN_03.KBN_NM7                                                                      " & vbNewLine _
                                                  & "       END                         = KBN_04.KBN_CD                                                  " & vbNewLine _
                                                  & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                         " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                         " & vbNewLine _
                                                  & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                  " & vbNewLine _
                                                  & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                         " & vbNewLine _
                                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                         " & vbNewLine _
                                                  & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                  " & vbNewLine _
                                                  & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                         " & vbNewLine _
                                                  & "WHERE D05_01.NRS_BR_CD             = @NRS_BR_CD                                                     " & vbNewLine _
                                                  & "  AND   D05_01.RIREKI_DATE           = @RIREKI_DATE                                                   " & vbNewLine
    'END YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'END YANAI 要望番号1022
    'END YANAI 要望番号769

#End Region

#Region "B_INKA"

    'START YANAI 要望番号769
    'Private Const SQL_SELECT_INKA As String = "SELECT                                                                                            " & vbNewLine _
    '                                        & " B03_01.ZAI_REC_NO               AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                        & ",B01_01.WH_CD                    AS      WH_CD                                                    " & vbNewLine _
    '                                        & ",M03_01.WH_NM                    AS      WH_NM                                                    " & vbNewLine _
    '                                        & ",M07_01.HOKAN_SEIQTO_CD          AS      SEIQTO_CD                                                " & vbNewLine _
    '                                        & ",M06_01.SEIQTO_NM                AS      SEIQTO_NM                                                " & vbNewLine _
    '                                        & ",B01_01.CUST_CD_L                AS      CUST_CD_L                                                " & vbNewLine _
    '                                        & ",B01_01.CUST_CD_M                AS      CUST_CD_M                                                " & vbNewLine _
    '                                        & ",B03_01.CUST_CD_S                AS      CUST_CD_S                                                " & vbNewLine _
    '                                        & ",B03_01.CUST_CD_SS               AS      CUST_CD_SS                                               " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS      CUST_NM_L                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS      CUST_NM_M                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_S                AS      CUST_NM_S                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_SS               AS      CUST_NM_SS                                               " & vbNewLine _
    '                                        & ",B03_01.SEARCH_KEY_1             AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                        & ",B03_01.SEARCH_KEY_2             AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                        & ",B03_01.CUST_COST_CD1            AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                        & ",B03_01.CUST_COST_CD2            AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                        & ",B03_01.GOODS_CD_CUST            AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                        & ",B03_01.GOODS_NM_1               AS      GOODS_NM                                                 " & vbNewLine _
    '                                        & ",B03_01.LOT_NO                   AS      LOT_NO                                                   " & vbNewLine _
    '                                        & ",B03_01.SERIAL_NO                AS      SERIAL_NO                                                " & vbNewLine _
    '                                        & ",D01_01.INKO_DATE                AS      INKO_DATE                                                " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_1          AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_2          AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_3          AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM1                  AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                        & ",M26_01.JOTAI_NM                 AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                        & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                        & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                        & "  END                            AS      OFB_KB                                                   " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS      OFB_NM                                                   " & vbNewLine _
    '                                        & ",B03_01.SPD_KB                   AS      SPD_KB                                                   " & vbNewLine _
    '                                        & ",KBN_05.KBN_NM1                  AS      SPD_NM                                                   " & vbNewLine _
    '                                        & ",B01_01.TAX_KB                   AS      TAX_KB                                                   " & vbNewLine _
    '                                        & ",KBN_06.KBN_NM1                  AS      TAX_NM                                                   " & vbNewLine _
    '                                        & ",B03_01.REMARK_OUT               AS      REMARK_OUT                                               " & vbNewLine _
    '                                        & ",B03_01.NB_UT                    AS      NB_UT                                                    " & vbNewLine _
    '                                        & ",CASE WHEN B03_01.KONSU < D01_01.IRIME                                                            " & vbNewLine _
    '                                        & "      THEN B03_01.KONSU                                                                           " & vbNewLine _
    '                                        & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                        & "  END                            AS      IRIME                                                    " & vbNewLine _
    '                                        & ",B03_01.STD_IRIME_UT             AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                        & ",B03_01.KONSU                    AS      ZAI_NB                                                   " & vbNewLine _
    '                                        & ",B03_01.ZAI_QT                   AS      ZAI_QT                 " & vbNewLine _
    '                                        & ",B03_01.PKG_NB                   AS      PKG_NB                                                   " & vbNewLine _
    '                                        & ",B03_01.PKG_UT                   AS      PKG_UT                                                   " & vbNewLine _
    '                                        & ",RTRIM(B03_01.REMARK)            AS      REMARK                                                   " & vbNewLine _
    '                                        & ",M60_01.SET_NAIYO                AS      GMC                                                      " & vbNewLine _
    '                                        & "FROM (                                                                                            " & vbNewLine _
    '                                        & "       SELECT B03_01.NRS_BR_CD                                          AS NRS_BR_CD              " & vbNewLine _
    '                                        & "             ,B03_01.INKA_NO_L                                          AS INKA_NO_L              " & vbNewLine _
    '                                        & "             ,B03_01.ZAI_REC_NO                                         AS ZAI_REC_NO             " & vbNewLine _
    '                                        & "             ,B03_01.LOT_NO                                             AS LOT_NO                 " & vbNewLine _
    '                                        & "             ,B03_01.SERIAL_NO                                          AS SERIAL_NO              " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_1                                    AS GOODS_COND_KB_1        " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_2                                    AS GOODS_COND_KB_2        " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_3                                    AS GOODS_COND_KB_3        " & vbNewLine _
    '                                        & "             ,B03_01.SPD_KB                                             AS SPD_KB                 " & vbNewLine _
    '                                        & "             ,B03_01.REMARK                                             AS REMARK                 " & vbNewLine _
    '                                        & "             ,MAX(B03_01.REMARK_OUT)                                    AS REMARK_OUT             " & vbNewLine _
    '                                        & "             ,SUM(B03_01.KONSU * ISNULL(M08_01.PKG_NB,0) + B03_01.HASU) AS KONSU                  " & vbNewLine _
    '                                        & "             ,SUM(B03_01.KONSU * ISNULL(M08_01.PKG_NB,0) + B03_01.HASU) * B03_01.IRIME AS ZAI_QT  " & vbNewLine _
    '                                        & "             ,B02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS           " & vbNewLine _
    '                                        & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S              " & vbNewLine _
    '                                        & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS             " & vbNewLine _
    '                                        & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1           " & vbNewLine _
    '                                        & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2           " & vbNewLine _
    '                                        & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1          " & vbNewLine _
    '                                        & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2          " & vbNewLine _
    '                                        & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST          " & vbNewLine _
    '                                        & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1             " & vbNewLine _
    '                                        & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                  " & vbNewLine _
    '                                        & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT           " & vbNewLine _
    '                                        & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                               " & vbNewLine _
    '                                        & "                   THEN 1                                                                         " & vbNewLine _
    '                                        & "                   ELSE M08_01.PKG_NB                                                             " & vbNewLine _
    '                                        & "               END                                                      AS PKG_NB                 " & vbNewLine _
    '                                        & "             ,M08_01.PKG_UT                                             AS PKG_UT                 " & vbNewLine _
    '                                        & "         FROM      $LM_TRN$..B_INKA_S B03_01                                                      " & vbNewLine _
    '                                        & "        INNER JOIN $LM_TRN$..B_INKA_M B02_01                                                      " & vbNewLine _
    '                                        & "           ON B03_01.NRS_BR_CD      = B02_01.NRS_BR_CD                                            " & vbNewLine _
    '                                        & "          AND B03_01.INKA_NO_L      = B02_01.INKA_NO_L                                            " & vbNewLine _
    '                                        & "          AND B03_01.INKA_NO_M      = B02_01.INKA_NO_M                                            " & vbNewLine _
    '                                        & "          AND B02_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "         LEFT JOIN $LM_MST$..M_GOODS  M08_01                                                      " & vbNewLine _
    '                                        & "           ON B03_01.NRS_BR_CD      = M08_01.NRS_BR_CD                                            " & vbNewLine _
    '                                        & "          AND B02_01.GOODS_CD_NRS   = M08_01.GOODS_CD_NRS                                         " & vbNewLine _
    '                                        & "          AND M08_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "        WHERE B03_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "        GROUP BY B03_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.INKA_NO_L                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.ZAI_REC_NO                                                                " & vbNewLine _
    '                                        & "                ,B03_01.LOT_NO                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.SERIAL_NO                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_1                                                           " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_2                                                           " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_3                                                           " & vbNewLine _
    '                                        & "                ,B03_01.SPD_KB                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.REMARK                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.IRIME                                                                     " & vbNewLine _
    '                                        & "                ,B02_01.GOODS_CD_NRS                                                              " & vbNewLine _
    '                                        & "                ,M08_01.CUST_CD_S                                                                 " & vbNewLine _
    '                                        & "                ,M08_01.CUST_CD_SS                                                                " & vbNewLine _
    '                                        & "                ,M08_01.SEARCH_KEY_1                                                              " & vbNewLine _
    '                                        & "                ,M08_01.SEARCH_KEY_2                                                              " & vbNewLine _
    '                                        & "                ,M08_01.CUST_COST_CD1                                                             " & vbNewLine _
    '                                        & "                ,M08_01.CUST_COST_CD2                                                             " & vbNewLine _
    '                                        & "                ,M08_01.GOODS_CD_CUST                                                             " & vbNewLine _
    '                                        & "                ,M08_01.GOODS_NM_1                                                                " & vbNewLine _
    '                                        & "                ,M08_01.PKG_UT                                                                    " & vbNewLine _
    '                                        & "                ,M08_01.STD_IRIME_UT                                                              " & vbNewLine _
    '                                        & "                ,M08_01.PKG_NB                                                                    " & vbNewLine _
    '                                        & "     )                         B03_01                                                             " & vbNewLine _
    '                                        & "INNER JOIN $LM_TRN$..B_INKA_L  B01_01                                                             " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD       = B01_01.NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B03_01.INKA_NO_L       = B01_01.INKA_NO_L                                                   " & vbNewLine _
    '                                        & "  AND B01_01.SYS_DEL_FLG     = '0'                                                                " & vbNewLine _
    '                                        & " LEFT JOIN $LM_TRN$..D_ZAI_TRS D01_01                                                             " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD       = D01_01.NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B03_01.ZAI_REC_NO      = D01_01.ZAI_REC_NO                                                  " & vbNewLine _
    '                                        & "  AND D01_01.SYS_DEL_FLG     = '0'                                                                " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                        & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                        & "  AND B03_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                        & "  AND B03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                        & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                        & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                        & "  AND B03_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                        & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                        & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                        & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                        & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                        & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                        & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                        & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                        & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                        & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                        & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                        & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                        & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                        & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                        & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                        & "                                )                           M60_02                                " & vbNewLine _
    '                                        & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                        & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                        & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                        & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                        & "           )                         M60_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                        & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                        & "   ON B03_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                        & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                        & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                        & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                        & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                        & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                        & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                        & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                        & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                        & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                        & "   ON B03_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                        & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                        & "   ON B01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                        & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & "WHERE B01_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B01_01.INKA_DATE             > @RIREKI_DATE                                                 " & vbNewLine _
    '                                        & "  AND B01_01.INKA_DATE            <= @HOUKOKU_DATE                                " & vbNewLine _
    '                                        & "  AND B01_01.INKA_STATE_KB        >= '50'                                                         " & vbNewLine
    'START YANAI 要望番号1022
    'Private Const SQL_SELECT_INKA As String = "SELECT                                                                                            " & vbNewLine _
    '                                        & " B03_01.ZAI_REC_NO               AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                        & ",B01_01.WH_CD                    AS      WH_CD                                                    " & vbNewLine _
    '                                        & ",M03_01.WH_NM                    AS      WH_NM                                                    " & vbNewLine _
    '                                        & ",M07_01.HOKAN_SEIQTO_CD          AS      SEIQTO_CD                                                " & vbNewLine _
    '                                        & ",M06_01.SEIQTO_NM                AS      SEIQTO_NM                                                " & vbNewLine _
    '                                        & ",B01_01.CUST_CD_L                AS      CUST_CD_L                                                " & vbNewLine _
    '                                        & ",B01_01.CUST_CD_M                AS      CUST_CD_M                                                " & vbNewLine _
    '                                        & ",B03_01.CUST_CD_S                AS      CUST_CD_S                                                " & vbNewLine _
    '                                        & ",B03_01.CUST_CD_SS               AS      CUST_CD_SS                                               " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS      CUST_NM_L                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS      CUST_NM_M                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_S                AS      CUST_NM_S                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_SS               AS      CUST_NM_SS                                               " & vbNewLine _
    '                                        & ",B03_01.SEARCH_KEY_1             AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                        & ",B03_01.SEARCH_KEY_2             AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                        & ",B03_01.CUST_COST_CD1            AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                        & ",B03_01.CUST_COST_CD2            AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                        & ",B03_01.GOODS_CD_CUST            AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                        & ",B03_01.GOODS_NM_1               AS      GOODS_NM                                                 " & vbNewLine _
    '                                        & ",B03_01.LOT_NO                   AS      LOT_NO                                                   " & vbNewLine _
    '                                        & ",B03_01.SERIAL_NO                AS      SERIAL_NO                                                " & vbNewLine _
    '                                        & ",D01_01.INKO_DATE                AS      INKO_DATE                                                " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_1          AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_2          AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_3          AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM1                  AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                        & ",M26_01.JOTAI_NM                 AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                        & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                        & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                        & "  END                            AS      OFB_KB                                                   " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS      OFB_NM                                                   " & vbNewLine _
    '                                        & ",B03_01.SPD_KB                   AS      SPD_KB                                                   " & vbNewLine _
    '                                        & ",KBN_05.KBN_NM1                  AS      SPD_NM                                                   " & vbNewLine _
    '                                        & ",B01_01.TAX_KB                   AS      TAX_KB                                                   " & vbNewLine _
    '                                        & ",KBN_06.KBN_NM1                  AS      TAX_NM                                                   " & vbNewLine _
    '                                        & ",B03_01.REMARK_OUT               AS      REMARK_OUT                                               " & vbNewLine _
    '                                        & ",B03_01.NB_UT                    AS      NB_UT                                                    " & vbNewLine _
    '                                        & ",CASE WHEN B03_01.KONSU < D01_01.IRIME                                                            " & vbNewLine _
    '                                        & "      THEN B03_01.KONSU                                                                           " & vbNewLine _
    '                                        & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                        & "  END                            AS      IRIME                                                    " & vbNewLine _
    '                                        & ",B03_01.STD_IRIME_UT             AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                        & ",B03_01.KONSU                    AS      ZAI_NB                                                   " & vbNewLine _
    '                                        & ",B03_01.ZAI_QT                   AS      ZAI_QT                 " & vbNewLine _
    '                                        & ",B03_01.PKG_NB                   AS      PKG_NB                                                   " & vbNewLine _
    '                                        & ",B03_01.PKG_UT                   AS      PKG_UT                                                   " & vbNewLine _
    '                                        & ",RTRIM(B03_01.REMARK)            AS      REMARK                                                   " & vbNewLine _
    '                                        & ",M60_01.SET_NAIYO                AS      GMC                                                      " & vbNewLine _
    '                                        & "FROM (                                                                                            " & vbNewLine _
    '                                        & "       SELECT B03_01.NRS_BR_CD                                          AS NRS_BR_CD              " & vbNewLine _
    '                                        & "             ,B03_01.INKA_NO_L                                          AS INKA_NO_L              " & vbNewLine _
    '                                        & "             ,B03_01.ZAI_REC_NO                                         AS ZAI_REC_NO             " & vbNewLine _
    '                                        & "             ,B03_01.LOT_NO                                             AS LOT_NO                 " & vbNewLine _
    '                                        & "             ,B03_01.SERIAL_NO                                          AS SERIAL_NO              " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_1                                    AS GOODS_COND_KB_1        " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_2                                    AS GOODS_COND_KB_2        " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_3                                    AS GOODS_COND_KB_3        " & vbNewLine _
    '                                        & "             ,B03_01.SPD_KB                                             AS SPD_KB                 " & vbNewLine _
    '                                        & "             ,B03_01.REMARK                                             AS REMARK                 " & vbNewLine _
    '                                        & "             ,MAX(B03_01.REMARK_OUT)                                    AS REMARK_OUT             " & vbNewLine _
    '                                        & "             ,SUM(B03_01.KONSU * ISNULL(M08_01.PKG_NB,0) + B03_01.HASU) AS KONSU                  " & vbNewLine _
    '                                        & "             ,SUM(B03_01.KONSU * ISNULL(M08_01.PKG_NB,0) + B03_01.HASU) * B03_01.IRIME AS ZAI_QT  " & vbNewLine _
    '                                        & "             ,B02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS           " & vbNewLine _
    '                                        & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S              " & vbNewLine _
    '                                        & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS             " & vbNewLine _
    '                                        & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1           " & vbNewLine _
    '                                        & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2           " & vbNewLine _
    '                                        & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1          " & vbNewLine _
    '                                        & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2          " & vbNewLine _
    '                                        & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST          " & vbNewLine _
    '                                        & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1             " & vbNewLine _
    '                                        & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                  " & vbNewLine _
    '                                        & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT           " & vbNewLine _
    '                                        & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                               " & vbNewLine _
    '                                        & "                   THEN 1                                                                         " & vbNewLine _
    '                                        & "                   ELSE M08_01.PKG_NB                                                             " & vbNewLine _
    '                                        & "               END                                                      AS PKG_NB                 " & vbNewLine _
    '                                        & "             ,M08_01.PKG_UT                                             AS PKG_UT                 " & vbNewLine _
    '                                        & "         FROM      $LM_TRN$..B_INKA_S B03_01                                                      " & vbNewLine _
    '                                        & "        INNER JOIN $LM_TRN$..B_INKA_M B02_01                                                      " & vbNewLine _
    '                                        & "           ON B03_01.NRS_BR_CD      = B02_01.NRS_BR_CD                                            " & vbNewLine _
    '                                        & "          AND B03_01.INKA_NO_L      = B02_01.INKA_NO_L                                            " & vbNewLine _
    '                                        & "          AND B03_01.INKA_NO_M      = B02_01.INKA_NO_M                                            " & vbNewLine _
    '                                        & "          AND B02_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "        INNER JOIN $LM_TRN$..B_INKA_L B01_01                                                      " & vbNewLine _
    '                                        & "           ON B03_01.NRS_BR_CD      = B01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                        & "          AND B03_01.INKA_NO_L      = B01_01.INKA_NO_L                                            " & vbNewLine _
    '                                        & "          AND B01_01.INKA_DATE             > @RIREKI_DATE                                         " & vbNewLine _
    '                                        & "          AND B01_01.INKA_DATE            <= @HOUKOKU_DATE                                        " & vbNewLine _
    '                                        & "          AND B01_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "          AND B01_01.CUST_CD_L       = @CUST_CD_L                                                 " & vbNewLine _
    '                                        & "          AND B01_01.CUST_CD_M       = @CUST_CD_M                                                 " & vbNewLine _
    '                                        & "         LEFT JOIN $LM_MST$..M_GOODS  M08_01                                                      " & vbNewLine _
    '                                        & "           ON B03_01.NRS_BR_CD      = M08_01.NRS_BR_CD                                            " & vbNewLine _
    '                                        & "          AND B02_01.GOODS_CD_NRS   = M08_01.GOODS_CD_NRS                                         " & vbNewLine _
    '                                        & "          AND M08_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "        WHERE B03_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "          AND B03_01.NRS_BR_CD   = @NRS_BR_CD                                                     " & vbNewLine _
    '                                        & "        GROUP BY B03_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.INKA_NO_L                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.ZAI_REC_NO                                                                " & vbNewLine _
    '                                        & "                ,B03_01.LOT_NO                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.SERIAL_NO                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_1                                                           " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_2                                                           " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_3                                                           " & vbNewLine _
    '                                        & "                ,B03_01.SPD_KB                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.REMARK                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.IRIME                                                                     " & vbNewLine _
    '                                        & "                ,B02_01.GOODS_CD_NRS                                                              " & vbNewLine _
    '                                        & "                ,M08_01.CUST_CD_S                                                                 " & vbNewLine _
    '                                        & "                ,M08_01.CUST_CD_SS                                                                " & vbNewLine _
    '                                        & "                ,M08_01.SEARCH_KEY_1                                                              " & vbNewLine _
    '                                        & "                ,M08_01.SEARCH_KEY_2                                                              " & vbNewLine _
    '                                        & "                ,M08_01.CUST_COST_CD1                                                             " & vbNewLine _
    '                                        & "                ,M08_01.CUST_COST_CD2                                                             " & vbNewLine _
    '                                        & "                ,M08_01.GOODS_CD_CUST                                                             " & vbNewLine _
    '                                        & "                ,M08_01.GOODS_NM_1                                                                " & vbNewLine _
    '                                        & "                ,M08_01.PKG_UT                                                                    " & vbNewLine _
    '                                        & "                ,M08_01.STD_IRIME_UT                                                              " & vbNewLine _
    '                                        & "                ,M08_01.PKG_NB                                                                    " & vbNewLine _
    '                                        & "     )                         B03_01                                                             " & vbNewLine _
    '                                        & "INNER JOIN $LM_TRN$..B_INKA_L  B01_01                                                             " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD       = B01_01.NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B03_01.INKA_NO_L       = B01_01.INKA_NO_L                                                   " & vbNewLine _
    '                                        & "  AND B01_01.SYS_DEL_FLG     = '0'                                                                " & vbNewLine _
    '                                        & " LEFT JOIN $LM_TRN$..D_ZAI_TRS D01_01                                                             " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD       = D01_01.NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B03_01.ZAI_REC_NO      = D01_01.ZAI_REC_NO                                                  " & vbNewLine _
    '                                        & "  AND D01_01.SYS_DEL_FLG     = '0'                                                                " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                        & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                        & "  AND B03_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                        & "  AND B03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                        & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                        & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                        & "  AND B03_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                        & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                        & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                        & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                        & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                        & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                        & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                        & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                        & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                        & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                        & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                        & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                        & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                        & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                        & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                        & "                                )                           M60_02                                " & vbNewLine _
    '                                        & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                        & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                        & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                        & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                        & "           )                         M60_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                        & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                        & "   ON B03_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                        & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                        & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                        & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                        & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                        & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                        & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                        & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                        & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                        & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                        & "   ON B03_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                        & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                        & "   ON B01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                        & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & "WHERE B01_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B01_01.INKA_DATE             > @RIREKI_DATE                                                 " & vbNewLine _
    '                                        & "  AND B01_01.INKA_DATE            <= @HOUKOKU_DATE                                " & vbNewLine _
    '                                        & "  AND B01_01.INKA_STATE_KB        >= '50'                                                         " & vbNewLine _
    '                                        & "  AND B01_01.SYS_DEL_FLG          = '0'                                                           " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_L       = @CUST_CD_L                                                         " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_M       = @CUST_CD_M                                                         " & vbNewLine _
    'START YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'Private Const SQL_SELECT_INKA As String = "SELECT                                                                                            " & vbNewLine _
    '                                        & " B03_01.ZAI_REC_NO               AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                        & ",B01_01.WH_CD                    AS      WH_CD                                                    " & vbNewLine _
    '                                        & ",M03_01.WH_NM                    AS      WH_NM                                                    " & vbNewLine _
    '                                        & ",M07_01.HOKAN_SEIQTO_CD          AS      SEIQTO_CD                                                " & vbNewLine _
    '                                        & ",M06_01.SEIQTO_NM                AS      SEIQTO_NM                                                " & vbNewLine _
    '                                        & ",B01_01.CUST_CD_L                AS      CUST_CD_L                                                " & vbNewLine _
    '                                        & ",B01_01.CUST_CD_M                AS      CUST_CD_M                                                " & vbNewLine _
    '                                        & ",B03_01.CUST_CD_S                AS      CUST_CD_S                                                " & vbNewLine _
    '                                        & ",B03_01.CUST_CD_SS               AS      CUST_CD_SS                                               " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS      CUST_NM_L                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS      CUST_NM_M                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_S                AS      CUST_NM_S                                                " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_SS               AS      CUST_NM_SS                                               " & vbNewLine _
    '                                        & ",B03_01.SEARCH_KEY_1             AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                        & ",B03_01.SEARCH_KEY_2             AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                        & ",B03_01.CUST_COST_CD1            AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                        & ",B03_01.CUST_COST_CD2            AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                        & ",B03_01.GOODS_CD_CUST            AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                        & ",B03_01.GOODS_NM_1               AS      GOODS_NM                                                 " & vbNewLine _
    '                                        & ",B03_01.LOT_NO                   AS      LOT_NO                                                   " & vbNewLine _
    '                                        & ",B03_01.SERIAL_NO                AS      SERIAL_NO                                                " & vbNewLine _
    '                                        & ",D01_01.INKO_DATE                AS      INKO_DATE                                                " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_1          AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_2          AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                        & ",B03_01.GOODS_COND_KB_3          AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM1                  AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                        & ",M26_01.JOTAI_NM                 AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                        & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                        & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                        & "  END                            AS      OFB_KB                                                   " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS      OFB_NM                                                   " & vbNewLine _
    '                                        & ",B03_01.SPD_KB                   AS      SPD_KB                                                   " & vbNewLine _
    '                                        & ",KBN_05.KBN_NM1                  AS      SPD_NM                                                   " & vbNewLine _
    '                                        & ",B01_01.TAX_KB                   AS      TAX_KB                                                   " & vbNewLine _
    '                                        & ",KBN_06.KBN_NM1                  AS      TAX_NM                                                   " & vbNewLine _
    '                                        & ",B03_01.REMARK_OUT               AS      REMARK_OUT                                               " & vbNewLine _
    '                                        & ",B03_01.NB_UT                    AS      NB_UT                                                    " & vbNewLine _
    '                                        & ",CASE WHEN B03_01.KONSU < D01_01.IRIME                                                            " & vbNewLine _
    '                                        & "      THEN B03_01.KONSU                                                                           " & vbNewLine _
    '                                        & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                        & "  END                            AS      IRIME                                                    " & vbNewLine _
    '                                        & ",B03_01.STD_IRIME_UT             AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                        & ",B03_01.KONSU                    AS      ZAI_NB                                                   " & vbNewLine _
    '                                        & ",B03_01.ZAI_QT                   AS      ZAI_QT                 " & vbNewLine _
    '                                        & ",B03_01.PKG_NB                   AS      PKG_NB                                                   " & vbNewLine _
    '                                        & ",B03_01.PKG_UT                   AS      PKG_UT                                                   " & vbNewLine _
    '                                        & ",RTRIM(B03_01.REMARK)            AS      REMARK                                                   " & vbNewLine _
    '                                        & ",M60_01.SET_NAIYO                AS      GMC                                                      " & vbNewLine _
    '                                        & "FROM (                                                                                            " & vbNewLine _
    '                                        & "       SELECT B03_01.NRS_BR_CD                                          AS NRS_BR_CD              " & vbNewLine _
    '                                        & "             ,B03_01.INKA_NO_L                                          AS INKA_NO_L              " & vbNewLine _
    '                                        & "             ,B03_01.ZAI_REC_NO                                         AS ZAI_REC_NO             " & vbNewLine _
    '                                        & "             ,B03_01.LOT_NO                                             AS LOT_NO                 " & vbNewLine _
    '                                        & "             ,B03_01.SERIAL_NO                                          AS SERIAL_NO              " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_1                                    AS GOODS_COND_KB_1        " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_2                                    AS GOODS_COND_KB_2        " & vbNewLine _
    '                                        & "             ,B03_01.GOODS_COND_KB_3                                    AS GOODS_COND_KB_3        " & vbNewLine _
    '                                        & "             ,B03_01.SPD_KB                                             AS SPD_KB                 " & vbNewLine _
    '                                        & "             ,B03_01.REMARK                                             AS REMARK                 " & vbNewLine _
    '                                        & "             ,MAX(B03_01.REMARK_OUT)                                    AS REMARK_OUT             " & vbNewLine _
    '                                        & "             ,SUM(B03_01.KONSU * ISNULL(M08_01.PKG_NB,0) + B03_01.HASU) AS KONSU                  " & vbNewLine _
    '                                        & "             ,SUM(B03_01.KONSU * ISNULL(M08_01.PKG_NB,0) + B03_01.HASU) * B03_01.IRIME AS ZAI_QT  " & vbNewLine _
    '                                        & "             ,B02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS           " & vbNewLine _
    '                                        & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S              " & vbNewLine _
    '                                        & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS             " & vbNewLine _
    '                                        & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1           " & vbNewLine _
    '                                        & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2           " & vbNewLine _
    '                                        & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1          " & vbNewLine _
    '                                        & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2          " & vbNewLine _
    '                                        & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST          " & vbNewLine _
    '                                        & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1             " & vbNewLine _
    '                                        & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                  " & vbNewLine _
    '                                        & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT           " & vbNewLine _
    '                                        & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                               " & vbNewLine _
    '                                        & "                   THEN 1                                                                         " & vbNewLine _
    '                                        & "                   ELSE M08_01.PKG_NB                                                             " & vbNewLine _
    '                                        & "               END                                                      AS PKG_NB                 " & vbNewLine _
    '                                        & "             ,M08_01.PKG_UT                                             AS PKG_UT                 " & vbNewLine _
    '                                        & "         FROM      $LM_TRN$..B_INKA_S B03_01                                                      " & vbNewLine _
    '                                        & "        INNER JOIN $LM_TRN$..B_INKA_M B02_01                                                      " & vbNewLine _
    '                                        & "           ON B03_01.NRS_BR_CD      = B02_01.NRS_BR_CD                                            " & vbNewLine _
    '                                        & "          AND B03_01.INKA_NO_L      = B02_01.INKA_NO_L                                            " & vbNewLine _
    '                                        & "          AND B03_01.INKA_NO_M      = B02_01.INKA_NO_M                                            " & vbNewLine _
    '                                        & "          AND B02_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "        INNER JOIN $LM_TRN$..B_INKA_L B01_01                                                      " & vbNewLine _
    '                                        & "           ON B03_01.NRS_BR_CD      = B01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                        & "          AND B03_01.INKA_NO_L      = B01_01.INKA_NO_L                                            " & vbNewLine _
    '                                        & "          AND B01_01.INKA_DATE             > @RIREKI_DATE                                         " & vbNewLine _
    '                                        & "          AND B01_01.INKA_DATE            <= @HOUKOKU_DATE                                        " & vbNewLine _
    '                                        & "          AND B01_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "          AND B01_01.CUST_CD_L       = @CUST_CD_L                                                 " & vbNewLine _
    '                                        & "          AND B01_01.CUST_CD_M       = @CUST_CD_M                                                 " & vbNewLine _
    '                                        & "         LEFT JOIN $LM_MST$..M_GOODS  M08_01                                                      " & vbNewLine _
    '                                        & "           ON B03_01.NRS_BR_CD      = M08_01.NRS_BR_CD                                            " & vbNewLine _
    '                                        & "          AND B02_01.GOODS_CD_NRS   = M08_01.GOODS_CD_NRS                                         " & vbNewLine _
    '                                        & "        WHERE B03_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
    '                                        & "          AND B03_01.NRS_BR_CD   = @NRS_BR_CD                                                     " & vbNewLine _
    '                                        & "        GROUP BY B03_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.INKA_NO_L                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.ZAI_REC_NO                                                                " & vbNewLine _
    '                                        & "                ,B03_01.LOT_NO                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.SERIAL_NO                                                                 " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_1                                                           " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_2                                                           " & vbNewLine _
    '                                        & "                ,B03_01.GOODS_COND_KB_3                                                           " & vbNewLine _
    '                                        & "                ,B03_01.SPD_KB                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.REMARK                                                                    " & vbNewLine _
    '                                        & "                ,B03_01.IRIME                                                                     " & vbNewLine _
    '                                        & "                ,B02_01.GOODS_CD_NRS                                                              " & vbNewLine _
    '                                        & "                ,M08_01.CUST_CD_S                                                                 " & vbNewLine _
    '                                        & "                ,M08_01.CUST_CD_SS                                                                " & vbNewLine _
    '                                        & "                ,M08_01.SEARCH_KEY_1                                                              " & vbNewLine _
    '                                        & "                ,M08_01.SEARCH_KEY_2                                                              " & vbNewLine _
    '                                        & "                ,M08_01.CUST_COST_CD1                                                             " & vbNewLine _
    '                                        & "                ,M08_01.CUST_COST_CD2                                                             " & vbNewLine _
    '                                        & "                ,M08_01.GOODS_CD_CUST                                                             " & vbNewLine _
    '                                        & "                ,M08_01.GOODS_NM_1                                                                " & vbNewLine _
    '                                        & "                ,M08_01.PKG_UT                                                                    " & vbNewLine _
    '                                        & "                ,M08_01.STD_IRIME_UT                                                              " & vbNewLine _
    '                                        & "                ,M08_01.PKG_NB                                                                    " & vbNewLine _
    '                                        & "     )                         B03_01                                                             " & vbNewLine _
    '                                        & "INNER JOIN $LM_TRN$..B_INKA_L  B01_01                                                             " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD       = B01_01.NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B03_01.INKA_NO_L       = B01_01.INKA_NO_L                                                   " & vbNewLine _
    '                                        & "  AND B01_01.SYS_DEL_FLG     = '0'                                                                " & vbNewLine _
    '                                        & " LEFT JOIN $LM_TRN$..D_ZAI_TRS D01_01                                                             " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD       = D01_01.NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B03_01.ZAI_REC_NO      = D01_01.ZAI_REC_NO                                                  " & vbNewLine _
    '                                        & "  AND D01_01.SYS_DEL_FLG     = '0'                                                                " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                        & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                        & "  AND B03_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                        & "  AND B03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                        & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                        & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                        & "   ON B01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                        & "  AND B03_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                        & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                        & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                        & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                        & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                        & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                        & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                        & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                        & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                        & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                        & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                        & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                        & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                        & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                        & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                        & "                                )                           M60_02                                " & vbNewLine _
    '                                        & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                        & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                        & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                        & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                        & "           )                         M60_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                        & "  AND B03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                        & "   ON B03_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                        & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                        & "   ON B03_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                        & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                        & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                        & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                        & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                        & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                        & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                        & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                        & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                        & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                        & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                        & "   ON B03_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                        & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                        & "   ON B01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                        & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                        & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                        & "WHERE B01_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                        & "  AND B01_01.INKA_DATE             > @RIREKI_DATE                                                 " & vbNewLine _
    '                                        & "  AND B01_01.INKA_DATE            <= @HOUKOKU_DATE                                " & vbNewLine _
    '                                        & "  AND B01_01.INKA_STATE_KB        >= '50'                                                         " & vbNewLine _
    '                                        & "  AND B01_01.SYS_DEL_FLG          = '0'                                                           " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_L       = @CUST_CD_L                                                         " & vbNewLine _
    '                                        & "  AND B01_01.CUST_CD_M       = @CUST_CD_M                                                         " & vbNewLine _
    Private Const SQL_SELECT_INKA As String = "SELECT                                                                                            " & vbNewLine _
                                            & " B03_01.ZAI_REC_NO               AS      ZAI_REC_NO                                               " & vbNewLine _
                                            & ",B01_01.WH_CD                    AS      WH_CD                                                    " & vbNewLine _
                                            & ",M03_01.WH_NM                    AS      WH_NM                                                    " & vbNewLine _
                                            & ",M07_01.HOKAN_SEIQTO_CD          AS      SEIQTO_CD                                                " & vbNewLine _
                                            & ",M06_01.SEIQTO_NM                AS      SEIQTO_NM                                                " & vbNewLine _
                                            & ",B01_01.CUST_CD_L                AS      CUST_CD_L                                                " & vbNewLine _
                                            & ",B01_01.CUST_CD_M                AS      CUST_CD_M                                                " & vbNewLine _
                                            & ",B03_01.CUST_CD_S                AS      CUST_CD_S                                                " & vbNewLine _
                                            & ",B03_01.CUST_CD_SS               AS      CUST_CD_SS                                               " & vbNewLine _
                                            & ",M07_01.CUST_NM_L                AS      CUST_NM_L                                                " & vbNewLine _
                                            & ",M07_01.CUST_NM_M                AS      CUST_NM_M                                                " & vbNewLine _
                                            & ",M07_01.CUST_NM_S                AS      CUST_NM_S                                                " & vbNewLine _
                                            & ",M07_01.CUST_NM_SS               AS      CUST_NM_SS                                               " & vbNewLine _
                                            & ",B03_01.SEARCH_KEY_1             AS      SEARCH_KEY_1                                             " & vbNewLine _
                                            & ",B03_01.SEARCH_KEY_2             AS      SEARCH_KEY_2                                             " & vbNewLine _
                                            & ",B03_01.CUST_COST_CD1            AS      CUST_COST_CD1                                            " & vbNewLine _
                                            & ",B03_01.CUST_COST_CD2            AS      CUST_COST_CD2                                            " & vbNewLine _
                                            & ",B03_01.GOODS_CD_CUST            AS      GOODS_CD_CUST                                            " & vbNewLine _
                                            & ",B03_01.GOODS_NM_1               AS      GOODS_NM                                                 " & vbNewLine _
                                            & ",B03_01.LOT_NO                   AS      LOT_NO                                                   " & vbNewLine _
                                            & ",B03_01.SERIAL_NO                AS      SERIAL_NO                                                " & vbNewLine _
                                            & ",D01_01.INKO_DATE                AS      INKO_DATE                                                " & vbNewLine _
                                            & ",B03_01.GOODS_COND_KB_1          AS      GOODS_COND_KB_1                                          " & vbNewLine _
                                            & ",B03_01.GOODS_COND_KB_2          AS      GOODS_COND_KB_2                                          " & vbNewLine _
                                            & ",B03_01.GOODS_COND_KB_3          AS      GOODS_COND_KB_3                                          " & vbNewLine _
                                            & ",KBN_01.KBN_NM1                  AS      GOODS_COND_NM_1                                          " & vbNewLine _
                                            & ",KBN_02.KBN_NM1                  AS      GOODS_COND_NM_2                                          " & vbNewLine _
                                            & ",M26_01.JOTAI_NM                 AS      GOODS_COND_NM_3                                          " & vbNewLine _
                                            & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
                                            & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
                                            & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
                                            & "  END                            AS      OFB_KB                                                   " & vbNewLine _
                                            & ",KBN_04.KBN_NM1                  AS      OFB_NM                                                   " & vbNewLine _
                                            & ",B03_01.SPD_KB                   AS      SPD_KB                                                   " & vbNewLine _
                                            & ",KBN_05.KBN_NM1                  AS      SPD_NM                                                   " & vbNewLine _
                                            & ",B01_01.TAX_KB                   AS      TAX_KB                                                   " & vbNewLine _
                                            & ",KBN_06.KBN_NM1                  AS      TAX_NM                                                   " & vbNewLine _
                                            & ",B03_01.REMARK_OUT               AS      REMARK_OUT                                               " & vbNewLine _
                                            & ",B03_01.NB_UT                    AS      NB_UT                                                    " & vbNewLine _
                                            & ",CASE WHEN B03_01.KONSU < D01_01.IRIME                                                            " & vbNewLine _
                                            & "      THEN B03_01.KONSU                                                                           " & vbNewLine _
                                            & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
                                            & "  END                            AS      IRIME                                                    " & vbNewLine _
                                            & ",B03_01.STD_IRIME_UT             AS      STD_IRIME_UT                                             " & vbNewLine _
                                            & ",B03_01.KONSU                    AS      ZAI_NB                                                   " & vbNewLine _
                                            & ",B03_01.ZAI_QT                   AS      ZAI_QT                 " & vbNewLine _
                                            & ",B03_01.PKG_NB                   AS      PKG_NB                                                   " & vbNewLine _
                                            & ",B03_01.PKG_UT                   AS      PKG_UT                                                   " & vbNewLine _
                                            & ",RTRIM(B03_01.REMARK)            AS      REMARK                                                   " & vbNewLine _
                                            & ",M60_01.SET_NAIYO                AS      GMC                                                      " & vbNewLine _
                                            & "FROM (                                                                                            " & vbNewLine _
                                            & "       SELECT B03_01.NRS_BR_CD                                          AS NRS_BR_CD              " & vbNewLine _
                                            & "             ,B03_01.INKA_NO_L                                          AS INKA_NO_L              " & vbNewLine _
                                            & "             ,B03_01.ZAI_REC_NO                                         AS ZAI_REC_NO             " & vbNewLine _
                                            & "             ,B03_01.LOT_NO                                             AS LOT_NO                 " & vbNewLine _
                                            & "             ,B03_01.SERIAL_NO                                          AS SERIAL_NO              " & vbNewLine _
                                            & "             ,B03_01.GOODS_COND_KB_1                                    AS GOODS_COND_KB_1        " & vbNewLine _
                                            & "             ,B03_01.GOODS_COND_KB_2                                    AS GOODS_COND_KB_2        " & vbNewLine _
                                            & "             ,B03_01.GOODS_COND_KB_3                                    AS GOODS_COND_KB_3        " & vbNewLine _
                                            & "             ,B03_01.SPD_KB                                             AS SPD_KB                 " & vbNewLine _
                                            & "             ,B03_01.REMARK                                             AS REMARK                 " & vbNewLine _
                                            & "             ,MAX(B03_01.REMARK_OUT)                                    AS REMARK_OUT             " & vbNewLine _
                                            & "             ,SUM(B03_01.KONSU * ISNULL(M08_01.PKG_NB,0) + B03_01.HASU) AS KONSU                  " & vbNewLine _
                                            & "             ,SUM(B03_01.KONSU * ISNULL(M08_01.PKG_NB,0) + B03_01.HASU) * B03_01.IRIME AS ZAI_QT  " & vbNewLine _
                                            & "             ,B02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS           " & vbNewLine _
                                            & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S              " & vbNewLine _
                                            & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS             " & vbNewLine _
                                            & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1           " & vbNewLine _
                                            & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2           " & vbNewLine _
                                            & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1          " & vbNewLine _
                                            & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2          " & vbNewLine _
                                            & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST          " & vbNewLine _
                                            & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1             " & vbNewLine _
                                            & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                  " & vbNewLine _
                                            & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT           " & vbNewLine _
                                            & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                               " & vbNewLine _
                                            & "                   THEN 1                                                                         " & vbNewLine _
                                            & "                   ELSE M08_01.PKG_NB                                                             " & vbNewLine _
                                            & "               END                                                      AS PKG_NB                 " & vbNewLine _
                                            & "             ,M08_01.PKG_UT                                             AS PKG_UT                 " & vbNewLine _
                                            & "         FROM      $LM_TRN$..B_INKA_S B03_01                                                      " & vbNewLine _
                                            & "        INNER JOIN $LM_TRN$..B_INKA_M B02_01                                                      " & vbNewLine _
                                            & "           ON B03_01.NRS_BR_CD      = B02_01.NRS_BR_CD                                            " & vbNewLine _
                                            & "          AND B03_01.INKA_NO_L      = B02_01.INKA_NO_L                                            " & vbNewLine _
                                            & "          AND B03_01.INKA_NO_M      = B02_01.INKA_NO_M                                            " & vbNewLine _
                                            & "          AND B02_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
                                            & "        INNER JOIN $LM_TRN$..B_INKA_L B01_01                                                      " & vbNewLine _
                                            & "           ON B03_01.NRS_BR_CD      = B01_01.NRS_BR_CD                                            " & vbNewLine _
                                            & "          AND B03_01.INKA_NO_L      = B01_01.INKA_NO_L                                            " & vbNewLine _
                                            & "          AND B01_01.INKA_DATE             > @RIREKI_DATE                                         " & vbNewLine _
                                            & "          AND B01_01.INKA_DATE            <= @HOUKOKU_DATE                                        " & vbNewLine _
                                            & "          AND B01_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
                                            & "          AND B01_01.CUST_CD_L       = @CUST_CD_L                                                 " & vbNewLine _
                                            & "          AND B01_01.CUST_CD_M       = @CUST_CD_M                                                 " & vbNewLine _
                                            & "         LEFT JOIN $LM_MST$..M_GOODS  M08_01                                                      " & vbNewLine _
                                            & "           ON B03_01.NRS_BR_CD      = M08_01.NRS_BR_CD                                            " & vbNewLine _
                                            & "          AND B02_01.GOODS_CD_NRS   = M08_01.GOODS_CD_NRS                                         " & vbNewLine _
                                            & "        WHERE B03_01.SYS_DEL_FLG    = '0'                                                         " & vbNewLine _
                                            & "          AND B03_01.NRS_BR_CD   = @NRS_BR_CD                                                     " & vbNewLine _
                                            & "        GROUP BY B03_01.NRS_BR_CD                                                                 " & vbNewLine _
                                            & "                ,B03_01.INKA_NO_L                                                                 " & vbNewLine _
                                            & "                ,B03_01.ZAI_REC_NO                                                                " & vbNewLine _
                                            & "                ,B03_01.LOT_NO                                                                    " & vbNewLine _
                                            & "                ,B03_01.SERIAL_NO                                                                 " & vbNewLine _
                                            & "                ,B03_01.GOODS_COND_KB_1                                                           " & vbNewLine _
                                            & "                ,B03_01.GOODS_COND_KB_2                                                           " & vbNewLine _
                                            & "                ,B03_01.GOODS_COND_KB_3                                                           " & vbNewLine _
                                            & "                ,B03_01.SPD_KB                                                                    " & vbNewLine _
                                            & "                ,B03_01.REMARK                                                                    " & vbNewLine _
                                            & "                ,B03_01.IRIME                                                                     " & vbNewLine _
                                            & "                ,B02_01.GOODS_CD_NRS                                                              " & vbNewLine _
                                            & "                ,M08_01.CUST_CD_S                                                                 " & vbNewLine _
                                            & "                ,M08_01.CUST_CD_SS                                                                " & vbNewLine _
                                            & "                ,M08_01.SEARCH_KEY_1                                                              " & vbNewLine _
                                            & "                ,M08_01.SEARCH_KEY_2                                                              " & vbNewLine _
                                            & "                ,M08_01.CUST_COST_CD1                                                             " & vbNewLine _
                                            & "                ,M08_01.CUST_COST_CD2                                                             " & vbNewLine _
                                            & "                ,M08_01.GOODS_CD_CUST                                                             " & vbNewLine _
                                            & "                ,M08_01.GOODS_NM_1                                                                " & vbNewLine _
                                            & "                ,M08_01.PKG_UT                                                                    " & vbNewLine _
                                            & "                ,M08_01.STD_IRIME_UT                                                              " & vbNewLine _
                                            & "                ,M08_01.PKG_NB                                                                    " & vbNewLine _
                                            & "     )                         B03_01                                                             " & vbNewLine _
                                            & "INNER JOIN $LM_TRN$..B_INKA_L  B01_01                                                             " & vbNewLine _
                                            & "   ON B03_01.NRS_BR_CD       = B01_01.NRS_BR_CD                                                   " & vbNewLine _
                                            & "  AND B03_01.INKA_NO_L       = B01_01.INKA_NO_L                                                   " & vbNewLine _
                                            & "  AND B01_01.SYS_DEL_FLG     = '0'                                                                " & vbNewLine _
                                            & " LEFT JOIN $LM_TRN$..D_ZAI_TRS D01_01                                                             " & vbNewLine _
                                            & "   ON B03_01.NRS_BR_CD       = D01_01.NRS_BR_CD                                                   " & vbNewLine _
                                            & "  AND B03_01.ZAI_REC_NO      = D01_01.ZAI_REC_NO                                                  " & vbNewLine _
                                            & "  AND D01_01.SYS_DEL_FLG     = '0'                                                                " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
                                            & "   ON B01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
                                            & "   ON B01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
                                            & "  AND B01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
                                            & "  AND B01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
                                            & "  AND B03_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
                                            & "  AND B03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
                                            & "   ON B03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
                                            & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
                                            & "   ON B01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
                                            & "  AND B01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
                                            & "  AND B03_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
                                            & " LEFT JOIN (                                                                                      " & vbNewLine _
                                            & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                            & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                            & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
                                            & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
                                            & "                     INNER JOIN (                                                                 " & vbNewLine _
                                            & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
                                            & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
                                            & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
                                            & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
                                            & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
                                            & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
                                            & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
                                            & "                                )                           M60_02                                " & vbNewLine _
                                            & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
                                            & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
                                            & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
                                            & "           )                         M60_01                                                       " & vbNewLine _
                                            & "   ON B03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
                                            & "  AND B03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
                                            & "   ON B03_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
                                            & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
                                            & "   ON B03_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
                                            & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
                                            & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
                                            & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
                                            & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
                                            & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
                                            & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
                                            & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
                                            & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
                                            & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
                                            & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
                                            & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
                                            & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
                                            & "   ON B03_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
                                            & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
                                            & "   ON B01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
                                            & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
                                            & "WHERE B01_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
                                            & "  AND B01_01.INKA_DATE             > @RIREKI_DATE                                                 " & vbNewLine _
                                            & "  AND B01_01.INKA_DATE            <= @HOUKOKU_DATE                                " & vbNewLine _
                                            & "  AND B01_01.INKA_STATE_KB        >= '50'                                                         " & vbNewLine _
                                            & "  AND B01_01.CUST_CD_L       = @CUST_CD_L                                                         " & vbNewLine _
                                            & "  AND B01_01.CUST_CD_M       = @CUST_CD_M                                                         " & vbNewLine _
    'END YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'END YANAI 要望番号1022
    'END YANAI 要望番号769

#End Region

#Region "C_OUTKA"

    'START YANAI 要望番号769
    'Private Const SQL_SELECT_OUTKA As String = "SELECT                                                                                           " & vbNewLine _
    '                                         & " C03_01.ZAI_REC_NO              AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                         & ",C01_01.WH_CD                   AS      WH_CD                                                    " & vbNewLine _
    '                                         & ",M03_01.WH_NM                   AS      WH_NM                                                    " & vbNewLine _
    '                                         & ",M07_01.HOKAN_SEIQTO_CD         AS      SEIQTO_CD                                                " & vbNewLine _
    '                                         & ",M06_01.SEIQTO_NM               AS      SEIQTO_NM                                                " & vbNewLine _
    '                                         & ",C01_01.CUST_CD_L               AS      CUST_CD_L                                                " & vbNewLine _
    '                                         & ",C01_01.CUST_CD_M               AS      CUST_CD_M                                                " & vbNewLine _
    '                                         & ",C03_01.CUST_CD_S               AS      CUST_CD_S                                                " & vbNewLine _
    '                                         & ",C03_01.CUST_CD_SS              AS      CUST_CD_SS                                               " & vbNewLine _
    '                                         & ",M07_01.CUST_NM_L               AS      CUST_NM_L                                                " & vbNewLine _
    '                                         & ",M07_01.CUST_NM_M               AS      CUST_NM_M                                                " & vbNewLine _
    '                                         & ",M07_01.CUST_NM_S               AS      CUST_NM_S                                                " & vbNewLine _
    '                                         & ",M07_01.CUST_NM_SS              AS      CUST_NM_SS                                               " & vbNewLine _
    '                                         & ",C03_01.SEARCH_KEY_1            AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                         & ",C03_01.SEARCH_KEY_2            AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                         & ",C03_01.CUST_COST_CD1           AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                         & ",C03_01.CUST_COST_CD2           AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                         & ",C03_01.GOODS_CD_CUST           AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                         & ",C03_01.GOODS_NM_1              AS      GOODS_NM                                                 " & vbNewLine _
    '                                         & ",C03_01.LOT_NO                  AS      LOT_NO                                                   " & vbNewLine _
    '                                         & ",C03_01.SERIAL_NO               AS      SERIAL_NO                                                " & vbNewLine _
    '                                         & ",D01_01.INKO_DATE               AS      INKO_DATE                                                " & vbNewLine _
    '                                         & ",D01_01.GOODS_COND_KB_1         AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                         & ",D01_01.GOODS_COND_KB_2         AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                         & ",D01_01.GOODS_COND_KB_3         AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                         & ",KBN_01.KBN_NM1                 AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                         & ",KBN_02.KBN_NM1                 AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                         & ",M26_01.JOTAI_NM                AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                         & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                            " & vbNewLine _
    '                                         & "      THEN D01_01.OFB_KB                                                                         " & vbNewLine _
    '                                         & "      ELSE KBN_03.KBN_NM7                                                                        " & vbNewLine _
    '                                         & "  END                           AS      OFB_KB                                                   " & vbNewLine _
    '                                         & ",KBN_04.KBN_NM1                 AS      OFB_NM                                                   " & vbNewLine _
    '                                         & ",D01_01.SPD_KB                  AS      SPD_KB                                                   " & vbNewLine _
    '                                         & ",KBN_05.KBN_NM1                 AS      SPD_NM                                                   " & vbNewLine _
    '                                         & ",''                             AS      TAX_KB                                                   " & vbNewLine _
    '                                         & ",''                             AS      TAX_NM                                                   " & vbNewLine _
    '                                         & ",D01_01.REMARK_OUT              AS      REMARK_OUT                                               " & vbNewLine _
    '                                         & ",C03_01.NB_UT                   AS      NB_UT                                                    " & vbNewLine _
    '                                         & ",CASE WHEN ABS(C03_01.ALCTD_NB) < D01_01.IRIME                                                   " & vbNewLine _
    '                                         & "      THEN ABS(C03_01.ALCTD_NB)                                                                  " & vbNewLine _
    '                                         & "      ELSE D01_01.IRIME                                                                          " & vbNewLine _
    '                                         & "  END                           AS      IRIME                                                    " & vbNewLine _
    '                                         & ",C03_01.STD_IRIME_UT            AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                         & ",C03_01.ALCTD_NB                AS      ZAI_NB                                                   " & vbNewLine _
    '                                         & ",C03_01.ALCTD_QT                AS      ZAI_QT                                                   " & vbNewLine _
    '                                         & ",C03_01.PKG_NB                  AS      PKG_NB                                                   " & vbNewLine _
    '                                         & ",C03_01.PKG_UT                  AS      PKG_UT                                                   " & vbNewLine _
    '                                         & ",RTRIM(C03_01.REMARK)           AS      REMARK                                                   " & vbNewLine _
    '                                         & ",M60_01.SET_NAIYO               AS      GMC                                                      " & vbNewLine _
    '                                         & "FROM (                                                                                           " & vbNewLine _
    '                                         & "       SELECT C03_01.NRS_BR_CD                                                                   " & vbNewLine _
    '                                         & "             ,C03_01.OUTKA_NO_L                                                                  " & vbNewLine _
    '                                         & "             ,C03_01.ZAI_REC_NO                                         AS ZAI_REC_NO            " & vbNewLine _
    '                                         & "             ,C03_01.LOT_NO                                             AS LOT_NO                " & vbNewLine _
    '                                         & "             ,C03_01.REMARK                                             AS REMARK                " & vbNewLine _
    '                                         & "             ,C03_01.SERIAL_NO                                          AS SERIAL_NO             " & vbNewLine _
    '                                         & "             ,-1 * SUM(C03_01.ALCTD_NB)                                 AS ALCTD_NB              " & vbNewLine _
    '                                         & "             ,-1 * SUM(C03_01.ALCTD_QT)                                 AS ALCTD_QT              " & vbNewLine _
    '                                         & "             ,C02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS          " & vbNewLine _
    '                                         & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S             " & vbNewLine _
    '                                         & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS            " & vbNewLine _
    '                                         & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1          " & vbNewLine _
    '                                         & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2          " & vbNewLine _
    '                                         & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1         " & vbNewLine _
    '                                         & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2         " & vbNewLine _
    '                                         & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST         " & vbNewLine _
    '                                         & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1            " & vbNewLine _
    '                                         & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                 " & vbNewLine _
    '                                         & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT          " & vbNewLine _
    '                                         & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                              " & vbNewLine _
    '                                         & "                   THEN 1                                                                        " & vbNewLine _
    '                                         & "                   ELSE M08_01.PKG_NB                                                            " & vbNewLine _
    '                                         & "               END                                                      AS PKG_NB                " & vbNewLine _
    '                                         & "             ,M08_01.PKG_UT                                             AS PKG_UT                " & vbNewLine _
    '                                         & "         FROM      $LM_TRN$..C_OUTKA_S C03_01                                                    " & vbNewLine _
    '                                         & "        INNER JOIN $LM_TRN$..C_OUTKA_M C02_01                                                    " & vbNewLine _
    '                                         & "           ON C03_01.NRS_BR_CD       = C02_01.NRS_BR_CD                                          " & vbNewLine _
    '                                         & "          AND C03_01.OUTKA_NO_L      = C02_01.OUTKA_NO_L                                         " & vbNewLine _
    '                                         & "          AND C03_01.OUTKA_NO_M      = C02_01.OUTKA_NO_M                                         " & vbNewLine _
    '                                         & "          AND C02_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                         & "        INNER JOIN $LM_MST$..M_GOODS   M08_01                                                    " & vbNewLine _
    '                                         & "           ON C03_01.NRS_BR_CD       = M08_01.NRS_BR_CD                                          " & vbNewLine _
    '                                         & "          AND C02_01.GOODS_CD_NRS    = M08_01.GOODS_CD_NRS                                       " & vbNewLine _
    '                                         & "          AND M08_01.ALCTD_KB       <> '04'                                                      " & vbNewLine _
    '                                         & "          AND M08_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                         & "        WHERE C03_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                         & "        GROUP BY C03_01.NRS_BR_CD                                                                " & vbNewLine _
    '                                         & "                ,C03_01.OUTKA_NO_L                                                               " & vbNewLine _
    '                                         & "                ,C03_01.ZAI_REC_NO                                                               " & vbNewLine _
    '                                         & "                ,C03_01.LOT_NO                                                                   " & vbNewLine _
    '                                         & "                ,C03_01.SERIAL_NO                                                                " & vbNewLine _
    '                                         & "                ,C03_01.REMARK                                                                   " & vbNewLine _
    '                                         & "                ,C02_01.GOODS_CD_NRS                                                             " & vbNewLine _
    '                                         & "                ,M08_01.CUST_CD_S                                                                " & vbNewLine _
    '                                         & "                ,M08_01.CUST_CD_SS                                                               " & vbNewLine _
    '                                         & "                ,M08_01.SEARCH_KEY_1                                                             " & vbNewLine _
    '                                         & "                ,M08_01.SEARCH_KEY_2                                                             " & vbNewLine _
    '                                         & "                ,M08_01.CUST_COST_CD1                                                            " & vbNewLine _
    '                                         & "                ,M08_01.CUST_COST_CD2                                                            " & vbNewLine _
    '                                         & "                ,M08_01.GOODS_CD_CUST                                                            " & vbNewLine _
    '                                         & "                ,M08_01.GOODS_NM_1                                                               " & vbNewLine _
    '                                         & "                ,M08_01.STD_IRIME_UT                                                             " & vbNewLine _
    '                                         & "                ,M08_01.PKG_NB                                                                   " & vbNewLine _
    '                                         & "                ,M08_01.PKG_UT                                                                   " & vbNewLine _
    '                                         & "     )                               C03_01                                                      " & vbNewLine _
    '                                         & "INNER JOIN $LM_TRN$..C_OUTKA_L       C01_01                                                      " & vbNewLine _
    '                                         & "   ON C03_01.NRS_BR_CD             = C01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                         & "  AND C03_01.OUTKA_NO_L            = C01_01.OUTKA_NO_L                                           " & vbNewLine _
    '                                         & "  AND C01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_TRN$..D_ZAI_TRS      D01_01                                                       " & vbNewLine _
    '                                         & "   ON C03_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                         & "  AND C03_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                           " & vbNewLine _
    '                                         & "  AND D01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                      " & vbNewLine _
    '                                         & "   ON C01_01.WH_CD                 = M03_01.WH_CD                                                " & vbNewLine _
    '                                         & "  AND M03_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                      " & vbNewLine _
    '                                         & "   ON C01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                            " & vbNewLine _
    '                                         & "  AND C01_01.CUST_CD_L             = M07_01.CUST_CD_L                                            " & vbNewLine _
    '                                         & "  AND C01_01.CUST_CD_M             = M07_01.CUST_CD_M                                            " & vbNewLine _
    '                                         & "  AND C03_01.CUST_CD_S             = M07_01.CUST_CD_S                                            " & vbNewLine _
    '                                         & "  AND C03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                           " & vbNewLine _
    '                                         & "  AND M07_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                      " & vbNewLine _
    '                                         & "   ON C03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                            " & vbNewLine _
    '                                         & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                            " & vbNewLine _
    '                                         & "  AND M06_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                      " & vbNewLine _
    '                                         & "   ON C01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                            " & vbNewLine _
    '                                         & "  AND C01_01.CUST_CD_L             = M26_01.CUST_CD_L                                            " & vbNewLine _
    '                                         & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                             " & vbNewLine _
    '                                         & "  AND M26_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN (                                                                                     " & vbNewLine _
    '                                         & "                        SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                         & "                              ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                         & "                              ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                         & "                          FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                         & "                    INNER JOIN (                                                                 " & vbNewLine _
    '                                         & "                                          SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                         & "                                                ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                         & "                                                ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                         & "                                            FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                         & "                                           WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                         & "                                             AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                         & "                                        GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                         & "                                                ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                         & "                               )                           M60_02                                " & vbNewLine _
    '                                         & "                            ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                         & "                           AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                         & "                           AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                         & "                         WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                         & "           )                         M60_01                                                      " & vbNewLine _
    '                                         & "   ON C03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                            " & vbNewLine _
    '                                         & "  AND C03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                      " & vbNewLine _
    '                                         & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                               " & vbNewLine _
    '                                         & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                      " & vbNewLine _
    '                                         & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                      " & vbNewLine _
    '                                         & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                               " & vbNewLine _
    '                                         & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                      " & vbNewLine _
    '                                         & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                      " & vbNewLine _
    '                                         & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                      " & vbNewLine _
    '                                         & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                              " & vbNewLine _
    '                                         & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                              " & vbNewLine _
    '                                         & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                              " & vbNewLine _
    '                                         & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                              " & vbNewLine _
    '                                         & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                              " & vbNewLine _
    '                                         & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                      " & vbNewLine _
    '                                         & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                       " & vbNewLine _
    '                                         & "           THEN D01_01.OFB_KB                                                                    " & vbNewLine _
    '                                         & "           ELSE KBN_03.KBN_NM7                                                                   " & vbNewLine _
    '                                         & "       END                         = KBN_04.KBN_CD                                               " & vbNewLine _
    '                                         & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                      " & vbNewLine _
    '                                         & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                      " & vbNewLine _
    '                                         & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                               " & vbNewLine _
    '                                         & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                      " & vbNewLine _
    '                                         & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                         & "WHERE C01_01.NRS_BR_CD             = @NRS_BR_CD                                                  " & vbNewLine _
    '                                         & "  AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                                " & vbNewLine _
    '                                         & "  AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                " & vbNewLine _
    '                                         & "  AND C01_01.OUTKA_STATE_KB       >= '60'                                                        " & vbNewLine
    'START YANAI 要望番号953
    'Private Const SQL_SELECT_OUTKA As String = "SELECT                                                                                           " & vbNewLine _
    '                                     & " C03_01.ZAI_REC_NO              AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                     & ",C01_01.WH_CD                   AS      WH_CD                                                    " & vbNewLine _
    '                                     & ",M03_01.WH_NM                   AS      WH_NM                                                    " & vbNewLine _
    '                                     & ",M07_01.HOKAN_SEIQTO_CD         AS      SEIQTO_CD                                                " & vbNewLine _
    '                                     & ",M06_01.SEIQTO_NM               AS      SEIQTO_NM                                                " & vbNewLine _
    '                                     & ",C01_01.CUST_CD_L               AS      CUST_CD_L                                                " & vbNewLine _
    '                                     & ",C01_01.CUST_CD_M               AS      CUST_CD_M                                                " & vbNewLine _
    '                                     & ",C03_01.CUST_CD_S               AS      CUST_CD_S                                                " & vbNewLine _
    '                                     & ",C03_01.CUST_CD_SS              AS      CUST_CD_SS                                               " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_L               AS      CUST_NM_L                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_M               AS      CUST_NM_M                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_S               AS      CUST_NM_S                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_SS              AS      CUST_NM_SS                                               " & vbNewLine _
    '                                     & ",C03_01.SEARCH_KEY_1            AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                     & ",C03_01.SEARCH_KEY_2            AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                     & ",C03_01.CUST_COST_CD1           AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                     & ",C03_01.CUST_COST_CD2           AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                     & ",C03_01.GOODS_CD_CUST           AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                     & ",C03_01.GOODS_NM_1              AS      GOODS_NM                                                 " & vbNewLine _
    '                                     & ",C03_01.LOT_NO                  AS      LOT_NO                                                   " & vbNewLine _
    '                                     & ",C03_01.SERIAL_NO               AS      SERIAL_NO                                                " & vbNewLine _
    '                                     & ",D01_01.INKO_DATE               AS      INKO_DATE                                                " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_1         AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_2         AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_3         AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                     & ",KBN_01.KBN_NM1                 AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                     & ",KBN_02.KBN_NM1                 AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                     & ",M26_01.JOTAI_NM                AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                     & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                            " & vbNewLine _
    '                                     & "      THEN D01_01.OFB_KB                                                                         " & vbNewLine _
    '                                     & "      ELSE KBN_03.KBN_NM7                                                                        " & vbNewLine _
    '                                     & "  END                           AS      OFB_KB                                                   " & vbNewLine _
    '                                     & ",KBN_04.KBN_NM1                 AS      OFB_NM                                                   " & vbNewLine _
    '                                     & ",D01_01.SPD_KB                  AS      SPD_KB                                                   " & vbNewLine _
    '                                     & ",KBN_05.KBN_NM1                 AS      SPD_NM                                                   " & vbNewLine _
    '                                     & ",''                             AS      TAX_KB                                                   " & vbNewLine _
    '                                     & ",''                             AS      TAX_NM                                                   " & vbNewLine _
    '                                     & ",D01_01.REMARK_OUT              AS      REMARK_OUT                                               " & vbNewLine _
    '                                     & ",C03_01.NB_UT                   AS      NB_UT                                                    " & vbNewLine _
    '                                     & ",CASE WHEN ABS(C03_01.ALCTD_NB) < D01_01.IRIME                                                   " & vbNewLine _
    '                                     & "      THEN ABS(C03_01.ALCTD_NB)                                                                  " & vbNewLine _
    '                                     & "      ELSE D01_01.IRIME                                                                          " & vbNewLine _
    '                                     & "  END                           AS      IRIME                                                    " & vbNewLine _
    '                                     & ",C03_01.STD_IRIME_UT            AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                     & ",C03_01.ALCTD_NB                AS      ZAI_NB                                                   " & vbNewLine _
    '                                     & ",C03_01.ALCTD_QT                AS      ZAI_QT                                                   " & vbNewLine _
    '                                     & ",C03_01.PKG_NB                  AS      PKG_NB                                                   " & vbNewLine _
    '                                     & ",C03_01.PKG_UT                  AS      PKG_UT                                                   " & vbNewLine _
    '                                     & ",RTRIM(C03_01.REMARK)           AS      REMARK                                                   " & vbNewLine _
    '                                     & ",M60_01.SET_NAIYO               AS      GMC                                                      " & vbNewLine _
    '                                     & "FROM (                                                                                           " & vbNewLine _
    '                                     & "       SELECT C03_01.NRS_BR_CD                                                                   " & vbNewLine _
    '                                     & "             ,C03_01.OUTKA_NO_L                                                                  " & vbNewLine _
    '                                     & "             ,C03_01.ZAI_REC_NO                                         AS ZAI_REC_NO            " & vbNewLine _
    '                                     & "             ,C03_01.LOT_NO                                             AS LOT_NO                " & vbNewLine _
    '                                     & "             ,C03_01.REMARK                                             AS REMARK                " & vbNewLine _
    '                                     & "             ,C03_01.SERIAL_NO                                          AS SERIAL_NO             " & vbNewLine _
    '                                     & "             ,-1 * SUM(C03_01.ALCTD_NB)                                 AS ALCTD_NB              " & vbNewLine _
    '                                     & "             ,-1 * SUM(C03_01.ALCTD_QT)                                 AS ALCTD_QT              " & vbNewLine _
    '                                     & "             ,C02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS          " & vbNewLine _
    '                                     & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S             " & vbNewLine _
    '                                     & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS            " & vbNewLine _
    '                                     & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1          " & vbNewLine _
    '                                     & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2          " & vbNewLine _
    '                                     & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1         " & vbNewLine _
    '                                     & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2         " & vbNewLine _
    '                                     & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST         " & vbNewLine _
    '                                     & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1            " & vbNewLine _
    '                                     & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                 " & vbNewLine _
    '                                     & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT          " & vbNewLine _
    '                                     & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                              " & vbNewLine _
    '                                     & "                   THEN 1                                                                        " & vbNewLine _
    '                                     & "                   ELSE M08_01.PKG_NB                                                            " & vbNewLine _
    '                                     & "               END                                                      AS PKG_NB                " & vbNewLine _
    '                                     & "             ,M08_01.PKG_UT                                             AS PKG_UT                " & vbNewLine _
    '                                     & "         FROM      $LM_TRN$..C_OUTKA_S C03_01                                                    " & vbNewLine _
    '                                     & "        INNER JOIN $LM_TRN$..C_OUTKA_M C02_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = C02_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_L      = C02_01.OUTKA_NO_L                                         " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_M      = C02_01.OUTKA_NO_M                                         " & vbNewLine _
    '                                     & "          AND C02_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "        INNER JOIN $LM_TRN$..C_OUTKA_L C01_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = C01_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_L      = C01_01.OUTKA_NO_L                                         " & vbNewLine _
    '                                     & "          AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                        " & vbNewLine _
    '                                     & "          AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                       " & vbNewLine _
    '                                     & "          AND C01_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "          AND C01_01.CUST_CD_L       = @CUST_CD_L                                                " & vbNewLine _
    '                                     & "          AND C01_01.CUST_CD_M       = @CUST_CD_M                                                " & vbNewLine _
    '                                     & "        INNER JOIN $LM_MST$..M_GOODS   M08_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = M08_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C02_01.GOODS_CD_NRS    = M08_01.GOODS_CD_NRS                                       " & vbNewLine _
    '                                     & "          AND M08_01.ALCTD_KB       <> '04'                                                      " & vbNewLine _
    '                                     & "          AND M08_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "          AND M08_01.CUST_CD_L       = @CUST_CD_L                                                " & vbNewLine _
    '                                     & "          AND M08_01.CUST_CD_M       = @CUST_CD_M                                                " & vbNewLine _
    '                                     & "          AND M08_01.CUST_CD_S      IN ( '00' , '01' )                                           " & vbNewLine _
    '                                     & "        WHERE C03_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "          AND C03_01.NRS_BR_CD       = @NRS_BR_CD                                                " & vbNewLine _
    '                                     & "        GROUP BY C03_01.NRS_BR_CD                                                                " & vbNewLine _
    '                                     & "                ,C03_01.OUTKA_NO_L                                                               " & vbNewLine _
    '                                     & "                ,C03_01.ZAI_REC_NO                                                               " & vbNewLine _
    '                                     & "                ,C03_01.LOT_NO                                                                   " & vbNewLine _
    '                                     & "                ,C03_01.SERIAL_NO                                                                " & vbNewLine _
    '                                     & "                ,C03_01.REMARK                                                                   " & vbNewLine _
    '                                     & "                ,C02_01.GOODS_CD_NRS                                                             " & vbNewLine _
    '                                     & "                ,M08_01.CUST_CD_S                                                                " & vbNewLine _
    '                                     & "                ,M08_01.CUST_CD_SS                                                               " & vbNewLine _
    '                                     & "                ,M08_01.SEARCH_KEY_1                                                             " & vbNewLine _
    '                                     & "                ,M08_01.SEARCH_KEY_2                                                             " & vbNewLine _
    '                                     & "                ,M08_01.CUST_COST_CD1                                                            " & vbNewLine _
    '                                     & "                ,M08_01.CUST_COST_CD2                                                            " & vbNewLine _
    '                                     & "                ,M08_01.GOODS_CD_CUST                                                            " & vbNewLine _
    '                                     & "                ,M08_01.GOODS_NM_1                                                               " & vbNewLine _
    '                                     & "                ,M08_01.STD_IRIME_UT                                                             " & vbNewLine _
    '                                     & "                ,M08_01.PKG_NB                                                                   " & vbNewLine _
    '                                     & "                ,M08_01.PKG_UT                                                                   " & vbNewLine _
    '                                     & "     )                               C03_01                                                      " & vbNewLine _
    '                                     & "INNER JOIN $LM_TRN$..C_OUTKA_L       C01_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = C01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.OUTKA_NO_L            = C01_01.OUTKA_NO_L                                           " & vbNewLine _
    '                                     & "  AND C01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_TRN$..D_ZAI_TRS      D01_01                                                       " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                           " & vbNewLine _
    '                                     & "  AND D01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.WH_CD                 = M03_01.WH_CD                                                " & vbNewLine _
    '                                     & "  AND M03_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L             = M07_01.CUST_CD_L                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_M             = M07_01.CUST_CD_M                                            " & vbNewLine _
    '                                     & "  AND C03_01.CUST_CD_S             = M07_01.CUST_CD_S                                            " & vbNewLine _
    '                                     & "  AND C03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                           " & vbNewLine _
    '                                     & "  AND M07_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                            " & vbNewLine _
    '                                     & "  AND M06_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L             = M26_01.CUST_CD_L                                            " & vbNewLine _
    '                                     & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                             " & vbNewLine _
    '                                     & "  AND M26_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN (                                                                                     " & vbNewLine _
    '                                     & "                        SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                     & "                              ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                     & "                              ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                     & "                          FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                     & "                    INNER JOIN (                                                                 " & vbNewLine _
    '                                     & "                                          SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                     & "                                                ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                     & "                                                ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                     & "                                            FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                     & "                                           WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                     & "                                             AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                     & "                                        GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                     & "                                                ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                     & "                               )                           M60_02                                " & vbNewLine _
    '                                     & "                            ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                     & "                           AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                     & "                           AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                     & "                         WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                     & "           )                         M60_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                      " & vbNewLine _
    '                                     & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                      " & vbNewLine _
    '                                     & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                      " & vbNewLine _
    '                                     & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                      " & vbNewLine _
    '                                     & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                      " & vbNewLine _
    '                                     & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                      " & vbNewLine _
    '                                     & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                              " & vbNewLine _
    '                                     & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                      " & vbNewLine _
    '                                     & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                       " & vbNewLine _
    '                                     & "           THEN D01_01.OFB_KB                                                                    " & vbNewLine _
    '                                     & "           ELSE KBN_03.KBN_NM7                                                                   " & vbNewLine _
    '                                     & "       END                         = KBN_04.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                      " & vbNewLine _
    '                                     & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                      " & vbNewLine _
    '                                     & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                      " & vbNewLine _
    '                                     & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & "WHERE C01_01.NRS_BR_CD             = @NRS_BR_CD                                                  " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                                " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_STATE_KB       >= '60'                                                        " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L       = @CUST_CD_L                                                         " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_M       = @CUST_CD_M                                                         " & vbNewLine _
    'START YANAI 要望番号1022
    'Private Const SQL_SELECT_OUTKA As String = "SELECT                                                                                           " & vbNewLine _
    '                                     & " C03_01.ZAI_REC_NO              AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                     & ",C01_01.WH_CD                   AS      WH_CD                                                    " & vbNewLine _
    '                                     & ",M03_01.WH_NM                   AS      WH_NM                                                    " & vbNewLine _
    '                                     & ",M07_01.HOKAN_SEIQTO_CD         AS      SEIQTO_CD                                                " & vbNewLine _
    '                                     & ",M06_01.SEIQTO_NM               AS      SEIQTO_NM                                                " & vbNewLine _
    '                                     & ",C01_01.CUST_CD_L               AS      CUST_CD_L                                                " & vbNewLine _
    '                                     & ",C01_01.CUST_CD_M               AS      CUST_CD_M                                                " & vbNewLine _
    '                                     & ",C03_01.CUST_CD_S               AS      CUST_CD_S                                                " & vbNewLine _
    '                                     & ",C03_01.CUST_CD_SS              AS      CUST_CD_SS                                               " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_L               AS      CUST_NM_L                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_M               AS      CUST_NM_M                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_S               AS      CUST_NM_S                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_SS              AS      CUST_NM_SS                                               " & vbNewLine _
    '                                     & ",C03_01.SEARCH_KEY_1            AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                     & ",C03_01.SEARCH_KEY_2            AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                     & ",C03_01.CUST_COST_CD1           AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                     & ",C03_01.CUST_COST_CD2           AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                     & ",C03_01.GOODS_CD_CUST           AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                     & ",C03_01.GOODS_NM_1              AS      GOODS_NM                                                 " & vbNewLine _
    '                                     & ",C03_01.LOT_NO                  AS      LOT_NO                                                   " & vbNewLine _
    '                                     & ",C03_01.SERIAL_NO               AS      SERIAL_NO                                                " & vbNewLine _
    '                                     & ",D01_01.INKO_DATE               AS      INKO_DATE                                                " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_1         AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_2         AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_3         AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                     & ",KBN_01.KBN_NM1                 AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                     & ",KBN_02.KBN_NM1                 AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                     & ",M26_01.JOTAI_NM                AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                     & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                            " & vbNewLine _
    '                                     & "      THEN D01_01.OFB_KB                                                                         " & vbNewLine _
    '                                     & "      ELSE KBN_03.KBN_NM7                                                                        " & vbNewLine _
    '                                     & "  END                           AS      OFB_KB                                                   " & vbNewLine _
    '                                     & ",KBN_04.KBN_NM1                 AS      OFB_NM                                                   " & vbNewLine _
    '                                     & ",D01_01.SPD_KB                  AS      SPD_KB                                                   " & vbNewLine _
    '                                     & ",KBN_05.KBN_NM1                 AS      SPD_NM                                                   " & vbNewLine _
    '                                     & ",''                             AS      TAX_KB                                                   " & vbNewLine _
    '                                     & ",''                             AS      TAX_NM                                                   " & vbNewLine _
    '                                     & ",D01_01.REMARK_OUT              AS      REMARK_OUT                                               " & vbNewLine _
    '                                     & ",C03_01.NB_UT                   AS      NB_UT                                                    " & vbNewLine _
    '                                     & ",CASE WHEN ABS(C03_01.ALCTD_NB) < D01_01.IRIME                                                   " & vbNewLine _
    '                                     & "      THEN ABS(C03_01.ALCTD_NB)                                                                  " & vbNewLine _
    '                                     & "      ELSE D01_01.IRIME                                                                          " & vbNewLine _
    '                                     & "  END                           AS      IRIME                                                    " & vbNewLine _
    '                                     & ",C03_01.STD_IRIME_UT            AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                     & ",C03_01.ALCTD_NB                AS      ZAI_NB                                                   " & vbNewLine _
    '                                     & ",C03_01.ALCTD_QT                AS      ZAI_QT                                                   " & vbNewLine _
    '                                     & ",C03_01.PKG_NB                  AS      PKG_NB                                                   " & vbNewLine _
    '                                     & ",C03_01.PKG_UT                  AS      PKG_UT                                                   " & vbNewLine _
    '                                     & ",RTRIM(C03_01.REMARK)           AS      REMARK                                                   " & vbNewLine _
    '                                     & ",M60_01.SET_NAIYO               AS      GMC                                                      " & vbNewLine _
    '                                     & "FROM (                                                                                           " & vbNewLine _
    '                                     & "       SELECT C03_01.NRS_BR_CD                                                                   " & vbNewLine _
    '                                     & "             ,C03_01.OUTKA_NO_L                                                                  " & vbNewLine _
    '                                     & "             ,C03_01.ZAI_REC_NO                                         AS ZAI_REC_NO            " & vbNewLine _
    '                                     & "             ,C03_01.LOT_NO                                             AS LOT_NO                " & vbNewLine _
    '                                     & "             ,C03_01.REMARK                                             AS REMARK                " & vbNewLine _
    '                                     & "             ,C03_01.SERIAL_NO                                          AS SERIAL_NO             " & vbNewLine _
    '                                     & "             ,-1 * SUM(C03_01.ALCTD_NB)                                 AS ALCTD_NB              " & vbNewLine _
    '                                     & "             ,-1 * SUM(C03_01.ALCTD_QT)                                 AS ALCTD_QT              " & vbNewLine _
    '                                     & "             ,C02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS          " & vbNewLine _
    '                                     & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S             " & vbNewLine _
    '                                     & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS            " & vbNewLine _
    '                                     & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1          " & vbNewLine _
    '                                     & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2          " & vbNewLine _
    '                                     & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1         " & vbNewLine _
    '                                     & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2         " & vbNewLine _
    '                                     & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST         " & vbNewLine _
    '                                     & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1            " & vbNewLine _
    '                                     & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                 " & vbNewLine _
    '                                     & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT          " & vbNewLine _
    '                                     & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                              " & vbNewLine _
    '                                     & "                   THEN 1                                                                        " & vbNewLine _
    '                                     & "                   ELSE M08_01.PKG_NB                                                            " & vbNewLine _
    '                                     & "               END                                                      AS PKG_NB                " & vbNewLine _
    '                                     & "             ,M08_01.PKG_UT                                             AS PKG_UT                " & vbNewLine _
    '                                     & "         FROM      $LM_TRN$..C_OUTKA_S C03_01                                                    " & vbNewLine _
    '                                     & "        INNER JOIN $LM_TRN$..C_OUTKA_M C02_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = C02_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_L      = C02_01.OUTKA_NO_L                                         " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_M      = C02_01.OUTKA_NO_M                                         " & vbNewLine _
    '                                     & "          AND C02_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "        INNER JOIN $LM_TRN$..C_OUTKA_L C01_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = C01_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_L      = C01_01.OUTKA_NO_L                                         " & vbNewLine _
    '                                     & "          AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                        " & vbNewLine _
    '                                     & "          AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                       " & vbNewLine _
    '                                     & "          AND C01_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "          AND C01_01.CUST_CD_L       = @CUST_CD_L                                                " & vbNewLine _
    '                                     & "          AND C01_01.CUST_CD_M       = @CUST_CD_M                                                " & vbNewLine _
    '                                     & "        INNER JOIN $LM_MST$..M_GOODS   M08_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = M08_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C02_01.GOODS_CD_NRS    = M08_01.GOODS_CD_NRS                                       " & vbNewLine _
    '                                     & "          AND M08_01.ALCTD_KB       <> '04'                                                      " & vbNewLine _
    '                                     & "          AND M08_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "          AND M08_01.CUST_CD_L       = @CUST_CD_L                                                " & vbNewLine _
    '                                     & "          AND M08_01.CUST_CD_M       = @CUST_CD_M                                                " & vbNewLine _
    '                                     & "        WHERE C03_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "          AND C03_01.NRS_BR_CD       = @NRS_BR_CD                                                " & vbNewLine _
    '                                     & "        GROUP BY C03_01.NRS_BR_CD                                                                " & vbNewLine _
    '                                     & "                ,C03_01.OUTKA_NO_L                                                               " & vbNewLine _
    '                                     & "                ,C03_01.ZAI_REC_NO                                                               " & vbNewLine _
    '                                     & "                ,C03_01.LOT_NO                                                                   " & vbNewLine _
    '                                     & "                ,C03_01.SERIAL_NO                                                                " & vbNewLine _
    '                                     & "                ,C03_01.REMARK                                                                   " & vbNewLine _
    '                                     & "                ,C02_01.GOODS_CD_NRS                                                             " & vbNewLine _
    '                                     & "                ,M08_01.CUST_CD_S                                                                " & vbNewLine _
    '                                     & "                ,M08_01.CUST_CD_SS                                                               " & vbNewLine _
    '                                     & "                ,M08_01.SEARCH_KEY_1                                                             " & vbNewLine _
    '                                     & "                ,M08_01.SEARCH_KEY_2                                                             " & vbNewLine _
    '                                     & "                ,M08_01.CUST_COST_CD1                                                            " & vbNewLine _
    '                                     & "                ,M08_01.CUST_COST_CD2                                                            " & vbNewLine _
    '                                     & "                ,M08_01.GOODS_CD_CUST                                                            " & vbNewLine _
    '                                     & "                ,M08_01.GOODS_NM_1                                                               " & vbNewLine _
    '                                     & "                ,M08_01.STD_IRIME_UT                                                             " & vbNewLine _
    '                                     & "                ,M08_01.PKG_NB                                                                   " & vbNewLine _
    '                                     & "                ,M08_01.PKG_UT                                                                   " & vbNewLine _
    '                                     & "     )                               C03_01                                                      " & vbNewLine _
    '                                     & "INNER JOIN $LM_TRN$..C_OUTKA_L       C01_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = C01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.OUTKA_NO_L            = C01_01.OUTKA_NO_L                                           " & vbNewLine _
    '                                     & "  AND C01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_TRN$..D_ZAI_TRS      D01_01                                                       " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                           " & vbNewLine _
    '                                     & "  AND D01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.WH_CD                 = M03_01.WH_CD                                                " & vbNewLine _
    '                                     & "  AND M03_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L             = M07_01.CUST_CD_L                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_M             = M07_01.CUST_CD_M                                            " & vbNewLine _
    '                                     & "  AND C03_01.CUST_CD_S             = M07_01.CUST_CD_S                                            " & vbNewLine _
    '                                     & "  AND C03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                           " & vbNewLine _
    '                                     & "  AND M07_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                            " & vbNewLine _
    '                                     & "  AND M06_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L             = M26_01.CUST_CD_L                                            " & vbNewLine _
    '                                     & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                             " & vbNewLine _
    '                                     & "  AND M26_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN (                                                                                     " & vbNewLine _
    '                                     & "                        SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                     & "                              ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                     & "                              ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                     & "                          FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                     & "                    INNER JOIN (                                                                 " & vbNewLine _
    '                                     & "                                          SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                     & "                                                ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                     & "                                                ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                     & "                                            FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                     & "                                           WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                     & "                                             AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                     & "                                        GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                     & "                                                ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                     & "                               )                           M60_02                                " & vbNewLine _
    '                                     & "                            ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                     & "                           AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                     & "                           AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                     & "                         WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                     & "           )                         M60_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                      " & vbNewLine _
    '                                     & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                      " & vbNewLine _
    '                                     & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                      " & vbNewLine _
    '                                     & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                      " & vbNewLine _
    '                                     & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                      " & vbNewLine _
    '                                     & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                      " & vbNewLine _
    '                                     & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                              " & vbNewLine _
    '                                     & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                      " & vbNewLine _
    '                                     & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                       " & vbNewLine _
    '                                     & "           THEN D01_01.OFB_KB                                                                    " & vbNewLine _
    '                                     & "           ELSE KBN_03.KBN_NM7                                                                   " & vbNewLine _
    '                                     & "       END                         = KBN_04.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                      " & vbNewLine _
    '                                     & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                      " & vbNewLine _
    '                                     & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                      " & vbNewLine _
    '                                     & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & "WHERE C01_01.NRS_BR_CD             = @NRS_BR_CD                                                  " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                                " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_STATE_KB       >= '60'                                                        " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L       = @CUST_CD_L                                                         " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_M       = @CUST_CD_M                                                         " & vbNewLine _
    'START YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'Private Const SQL_SELECT_OUTKA As String = "SELECT                                                                                           " & vbNewLine _
    '                                     & " C03_01.ZAI_REC_NO              AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                     & ",C01_01.WH_CD                   AS      WH_CD                                                    " & vbNewLine _
    '                                     & ",M03_01.WH_NM                   AS      WH_NM                                                    " & vbNewLine _
    '                                     & ",M07_01.HOKAN_SEIQTO_CD         AS      SEIQTO_CD                                                " & vbNewLine _
    '                                     & ",M06_01.SEIQTO_NM               AS      SEIQTO_NM                                                " & vbNewLine _
    '                                     & ",C01_01.CUST_CD_L               AS      CUST_CD_L                                                " & vbNewLine _
    '                                     & ",C01_01.CUST_CD_M               AS      CUST_CD_M                                                " & vbNewLine _
    '                                     & ",C03_01.CUST_CD_S               AS      CUST_CD_S                                                " & vbNewLine _
    '                                     & ",C03_01.CUST_CD_SS              AS      CUST_CD_SS                                               " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_L               AS      CUST_NM_L                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_M               AS      CUST_NM_M                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_S               AS      CUST_NM_S                                                " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_SS              AS      CUST_NM_SS                                               " & vbNewLine _
    '                                     & ",C03_01.SEARCH_KEY_1            AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                     & ",C03_01.SEARCH_KEY_2            AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                     & ",C03_01.CUST_COST_CD1           AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                     & ",C03_01.CUST_COST_CD2           AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                     & ",C03_01.GOODS_CD_CUST           AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                     & ",C03_01.GOODS_NM_1              AS      GOODS_NM                                                 " & vbNewLine _
    '                                     & ",C03_01.LOT_NO                  AS      LOT_NO                                                   " & vbNewLine _
    '                                     & ",C03_01.SERIAL_NO               AS      SERIAL_NO                                                " & vbNewLine _
    '                                     & ",D01_01.INKO_DATE               AS      INKO_DATE                                                " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_1         AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_2         AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                     & ",D01_01.GOODS_COND_KB_3         AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                     & ",KBN_01.KBN_NM1                 AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                     & ",KBN_02.KBN_NM1                 AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                     & ",M26_01.JOTAI_NM                AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                     & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                            " & vbNewLine _
    '                                     & "      THEN D01_01.OFB_KB                                                                         " & vbNewLine _
    '                                     & "      ELSE KBN_03.KBN_NM7                                                                        " & vbNewLine _
    '                                     & "  END                           AS      OFB_KB                                                   " & vbNewLine _
    '                                     & ",KBN_04.KBN_NM1                 AS      OFB_NM                                                   " & vbNewLine _
    '                                     & ",D01_01.SPD_KB                  AS      SPD_KB                                                   " & vbNewLine _
    '                                     & ",KBN_05.KBN_NM1                 AS      SPD_NM                                                   " & vbNewLine _
    '                                     & ",''                             AS      TAX_KB                                                   " & vbNewLine _
    '                                     & ",''                             AS      TAX_NM                                                   " & vbNewLine _
    '                                     & ",D01_01.REMARK_OUT              AS      REMARK_OUT                                               " & vbNewLine _
    '                                     & ",C03_01.NB_UT                   AS      NB_UT                                                    " & vbNewLine _
    '                                     & ",CASE WHEN ABS(C03_01.ALCTD_NB) < D01_01.IRIME                                                   " & vbNewLine _
    '                                     & "      THEN ABS(C03_01.ALCTD_NB)                                                                  " & vbNewLine _
    '                                     & "      ELSE D01_01.IRIME                                                                          " & vbNewLine _
    '                                     & "  END                           AS      IRIME                                                    " & vbNewLine _
    '                                     & ",C03_01.STD_IRIME_UT            AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                     & ",C03_01.ALCTD_NB                AS      ZAI_NB                                                   " & vbNewLine _
    '                                     & ",C03_01.ALCTD_QT                AS      ZAI_QT                                                   " & vbNewLine _
    '                                     & ",C03_01.PKG_NB                  AS      PKG_NB                                                   " & vbNewLine _
    '                                     & ",C03_01.PKG_UT                  AS      PKG_UT                                                   " & vbNewLine _
    '                                     & ",RTRIM(C03_01.REMARK)           AS      REMARK                                                   " & vbNewLine _
    '                                     & ",M60_01.SET_NAIYO               AS      GMC                                                      " & vbNewLine _
    '                                     & "FROM (                                                                                           " & vbNewLine _
    '                                     & "       SELECT C03_01.NRS_BR_CD                                                                   " & vbNewLine _
    '                                     & "             ,C03_01.OUTKA_NO_L                                                                  " & vbNewLine _
    '                                     & "             ,C03_01.ZAI_REC_NO                                         AS ZAI_REC_NO            " & vbNewLine _
    '                                     & "             ,C03_01.LOT_NO                                             AS LOT_NO                " & vbNewLine _
    '                                     & "             ,C03_01.REMARK                                             AS REMARK                " & vbNewLine _
    '                                     & "             ,C03_01.SERIAL_NO                                          AS SERIAL_NO             " & vbNewLine _
    '                                     & "             ,-1 * SUM(C03_01.ALCTD_NB)                                 AS ALCTD_NB              " & vbNewLine _
    '                                     & "             ,-1 * SUM(C03_01.ALCTD_QT)                                 AS ALCTD_QT              " & vbNewLine _
    '                                     & "             ,C02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS          " & vbNewLine _
    '                                     & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S             " & vbNewLine _
    '                                     & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS            " & vbNewLine _
    '                                     & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1          " & vbNewLine _
    '                                     & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2          " & vbNewLine _
    '                                     & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1         " & vbNewLine _
    '                                     & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2         " & vbNewLine _
    '                                     & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST         " & vbNewLine _
    '                                     & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1            " & vbNewLine _
    '                                     & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                 " & vbNewLine _
    '                                     & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT          " & vbNewLine _
    '                                     & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                              " & vbNewLine _
    '                                     & "                   THEN 1                                                                        " & vbNewLine _
    '                                     & "                   ELSE M08_01.PKG_NB                                                            " & vbNewLine _
    '                                     & "               END                                                      AS PKG_NB                " & vbNewLine _
    '                                     & "             ,M08_01.PKG_UT                                             AS PKG_UT                " & vbNewLine _
    '                                     & "         FROM      $LM_TRN$..C_OUTKA_S C03_01                                                    " & vbNewLine _
    '                                     & "        INNER JOIN $LM_TRN$..C_OUTKA_M C02_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = C02_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_L      = C02_01.OUTKA_NO_L                                         " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_M      = C02_01.OUTKA_NO_M                                         " & vbNewLine _
    '                                     & "          AND C02_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "        INNER JOIN $LM_TRN$..C_OUTKA_L C01_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = C01_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C03_01.OUTKA_NO_L      = C01_01.OUTKA_NO_L                                         " & vbNewLine _
    '                                     & "          AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                        " & vbNewLine _
    '                                     & "          AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                       " & vbNewLine _
    '                                     & "          AND C01_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "          AND C01_01.CUST_CD_L       = @CUST_CD_L                                                " & vbNewLine _
    '                                     & "          AND C01_01.CUST_CD_M       = @CUST_CD_M                                                " & vbNewLine _
    '                                     & "        INNER JOIN $LM_MST$..M_GOODS   M08_01                                                    " & vbNewLine _
    '                                     & "           ON C03_01.NRS_BR_CD       = M08_01.NRS_BR_CD                                          " & vbNewLine _
    '                                     & "          AND C02_01.GOODS_CD_NRS    = M08_01.GOODS_CD_NRS                                       " & vbNewLine _
    '                                     & "          AND M08_01.ALCTD_KB       <> '04'                                                      " & vbNewLine _
    '                                     & "          AND M08_01.CUST_CD_L       = @CUST_CD_L                                                " & vbNewLine _
    '                                     & "          AND M08_01.CUST_CD_M       = @CUST_CD_M                                                " & vbNewLine _
    '                                     & "        WHERE C03_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
    '                                     & "          AND C03_01.NRS_BR_CD       = @NRS_BR_CD                                                " & vbNewLine _
    '                                     & "        GROUP BY C03_01.NRS_BR_CD                                                                " & vbNewLine _
    '                                     & "                ,C03_01.OUTKA_NO_L                                                               " & vbNewLine _
    '                                     & "                ,C03_01.ZAI_REC_NO                                                               " & vbNewLine _
    '                                     & "                ,C03_01.LOT_NO                                                                   " & vbNewLine _
    '                                     & "                ,C03_01.SERIAL_NO                                                                " & vbNewLine _
    '                                     & "                ,C03_01.REMARK                                                                   " & vbNewLine _
    '                                     & "                ,C02_01.GOODS_CD_NRS                                                             " & vbNewLine _
    '                                     & "                ,M08_01.CUST_CD_S                                                                " & vbNewLine _
    '                                     & "                ,M08_01.CUST_CD_SS                                                               " & vbNewLine _
    '                                     & "                ,M08_01.SEARCH_KEY_1                                                             " & vbNewLine _
    '                                     & "                ,M08_01.SEARCH_KEY_2                                                             " & vbNewLine _
    '                                     & "                ,M08_01.CUST_COST_CD1                                                            " & vbNewLine _
    '                                     & "                ,M08_01.CUST_COST_CD2                                                            " & vbNewLine _
    '                                     & "                ,M08_01.GOODS_CD_CUST                                                            " & vbNewLine _
    '                                     & "                ,M08_01.GOODS_NM_1                                                               " & vbNewLine _
    '                                     & "                ,M08_01.STD_IRIME_UT                                                             " & vbNewLine _
    '                                     & "                ,M08_01.PKG_NB                                                                   " & vbNewLine _
    '                                     & "                ,M08_01.PKG_UT                                                                   " & vbNewLine _
    '                                     & "     )                               C03_01                                                      " & vbNewLine _
    '                                     & "INNER JOIN $LM_TRN$..C_OUTKA_L       C01_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = C01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.OUTKA_NO_L            = C01_01.OUTKA_NO_L                                           " & vbNewLine _
    '                                     & "  AND C01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_TRN$..D_ZAI_TRS      D01_01                                                       " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                           " & vbNewLine _
    '                                     & "  AND D01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.WH_CD                 = M03_01.WH_CD                                                " & vbNewLine _
    '                                     & "  AND M03_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L             = M07_01.CUST_CD_L                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_M             = M07_01.CUST_CD_M                                            " & vbNewLine _
    '                                     & "  AND C03_01.CUST_CD_S             = M07_01.CUST_CD_S                                            " & vbNewLine _
    '                                     & "  AND C03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                           " & vbNewLine _
    '                                     & "  AND M07_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                            " & vbNewLine _
    '                                     & "  AND M06_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                      " & vbNewLine _
    '                                     & "   ON C01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L             = M26_01.CUST_CD_L                                            " & vbNewLine _
    '                                     & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                             " & vbNewLine _
    '                                     & "  AND M26_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN (                                                                                     " & vbNewLine _
    '                                     & "                        SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                     & "                              ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                     & "                              ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                     & "                          FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                     & "                    INNER JOIN (                                                                 " & vbNewLine _
    '                                     & "                                          SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                     & "                                                ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                     & "                                                ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                     & "                                            FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                     & "                                           WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                     & "                                             AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                     & "                                        GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                     & "                                                ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                     & "                               )                           M60_02                                " & vbNewLine _
    '                                     & "                            ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                     & "                           AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                     & "                           AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                     & "                         WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                     & "           )                         M60_01                                                      " & vbNewLine _
    '                                     & "   ON C03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                            " & vbNewLine _
    '                                     & "  AND C03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                      " & vbNewLine _
    '                                     & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                      " & vbNewLine _
    '                                     & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                      " & vbNewLine _
    '                                     & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                      " & vbNewLine _
    '                                     & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                      " & vbNewLine _
    '                                     & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                      " & vbNewLine _
    '                                     & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                              " & vbNewLine _
    '                                     & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                              " & vbNewLine _
    '                                     & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                      " & vbNewLine _
    '                                     & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                       " & vbNewLine _
    '                                     & "           THEN D01_01.OFB_KB                                                                    " & vbNewLine _
    '                                     & "           ELSE KBN_03.KBN_NM7                                                                   " & vbNewLine _
    '                                     & "       END                         = KBN_04.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                      " & vbNewLine _
    '                                     & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                      " & vbNewLine _
    '                                     & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                               " & vbNewLine _
    '                                     & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                      " & vbNewLine _
    '                                     & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
    '                                     & "WHERE C01_01.NRS_BR_CD             = @NRS_BR_CD                                                  " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                                " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                " & vbNewLine _
    '                                     & "  AND C01_01.OUTKA_STATE_KB       >= '60'                                                        " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_L       = @CUST_CD_L                                                         " & vbNewLine _
    '                                     & "  AND C01_01.CUST_CD_M       = @CUST_CD_M                                                         " & vbNewLine _
    Private Const SQL_SELECT_OUTKA As String = "SELECT                                                                                           " & vbNewLine _
                                     & " C03_01.ZAI_REC_NO              AS      ZAI_REC_NO                                               " & vbNewLine _
                                     & ",C01_01.WH_CD                   AS      WH_CD                                                    " & vbNewLine _
                                     & ",M03_01.WH_NM                   AS      WH_NM                                                    " & vbNewLine _
                                     & ",M07_01.HOKAN_SEIQTO_CD         AS      SEIQTO_CD                                                " & vbNewLine _
                                     & ",M06_01.SEIQTO_NM               AS      SEIQTO_NM                                                " & vbNewLine _
                                     & ",C01_01.CUST_CD_L               AS      CUST_CD_L                                                " & vbNewLine _
                                     & ",C01_01.CUST_CD_M               AS      CUST_CD_M                                                " & vbNewLine _
                                     & ",C03_01.CUST_CD_S               AS      CUST_CD_S                                                " & vbNewLine _
                                     & ",C03_01.CUST_CD_SS              AS      CUST_CD_SS                                               " & vbNewLine _
                                     & ",M07_01.CUST_NM_L               AS      CUST_NM_L                                                " & vbNewLine _
                                     & ",M07_01.CUST_NM_M               AS      CUST_NM_M                                                " & vbNewLine _
                                     & ",M07_01.CUST_NM_S               AS      CUST_NM_S                                                " & vbNewLine _
                                     & ",M07_01.CUST_NM_SS              AS      CUST_NM_SS                                               " & vbNewLine _
                                     & ",C03_01.SEARCH_KEY_1            AS      SEARCH_KEY_1                                             " & vbNewLine _
                                     & ",C03_01.SEARCH_KEY_2            AS      SEARCH_KEY_2                                             " & vbNewLine _
                                     & ",C03_01.CUST_COST_CD1           AS      CUST_COST_CD1                                            " & vbNewLine _
                                     & ",C03_01.CUST_COST_CD2           AS      CUST_COST_CD2                                            " & vbNewLine _
                                     & ",C03_01.GOODS_CD_CUST           AS      GOODS_CD_CUST                                            " & vbNewLine _
                                     & ",C03_01.GOODS_NM_1              AS      GOODS_NM                                                 " & vbNewLine _
                                     & ",C03_01.LOT_NO                  AS      LOT_NO                                                   " & vbNewLine _
                                     & ",C03_01.SERIAL_NO               AS      SERIAL_NO                                                " & vbNewLine _
                                     & ",D01_01.INKO_DATE               AS      INKO_DATE                                                " & vbNewLine _
                                     & ",D01_01.GOODS_COND_KB_1         AS      GOODS_COND_KB_1                                          " & vbNewLine _
                                     & ",D01_01.GOODS_COND_KB_2         AS      GOODS_COND_KB_2                                          " & vbNewLine _
                                     & ",D01_01.GOODS_COND_KB_3         AS      GOODS_COND_KB_3                                          " & vbNewLine _
                                     & ",KBN_01.KBN_NM1                 AS      GOODS_COND_NM_1                                          " & vbNewLine _
                                     & ",KBN_02.KBN_NM1                 AS      GOODS_COND_NM_2                                          " & vbNewLine _
                                     & ",M26_01.JOTAI_NM                AS      GOODS_COND_NM_3                                          " & vbNewLine _
                                     & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                            " & vbNewLine _
                                     & "      THEN D01_01.OFB_KB                                                                         " & vbNewLine _
                                     & "      ELSE KBN_03.KBN_NM7                                                                        " & vbNewLine _
                                     & "  END                           AS      OFB_KB                                                   " & vbNewLine _
                                     & ",KBN_04.KBN_NM1                 AS      OFB_NM                                                   " & vbNewLine _
                                     & ",D01_01.SPD_KB                  AS      SPD_KB                                                   " & vbNewLine _
                                     & ",KBN_05.KBN_NM1                 AS      SPD_NM                                                   " & vbNewLine _
                                     & ",''                             AS      TAX_KB                                                   " & vbNewLine _
                                     & ",''                             AS      TAX_NM                                                   " & vbNewLine _
                                     & ",D01_01.REMARK_OUT              AS      REMARK_OUT                                               " & vbNewLine _
                                     & ",C03_01.NB_UT                   AS      NB_UT                                                    " & vbNewLine _
                                     & ",CASE WHEN ABS(C03_01.ALCTD_NB) < D01_01.IRIME                                                   " & vbNewLine _
                                     & "      THEN ABS(C03_01.ALCTD_NB)                                                                  " & vbNewLine _
                                     & "      ELSE D01_01.IRIME                                                                          " & vbNewLine _
                                     & "  END                           AS      IRIME                                                    " & vbNewLine _
                                     & ",C03_01.STD_IRIME_UT            AS      STD_IRIME_UT                                             " & vbNewLine _
                                     & ",C03_01.ALCTD_NB                AS      ZAI_NB                                                   " & vbNewLine _
                                     & ",C03_01.ALCTD_QT                AS      ZAI_QT                                                   " & vbNewLine _
                                     & ",C03_01.PKG_NB                  AS      PKG_NB                                                   " & vbNewLine _
                                     & ",C03_01.PKG_UT                  AS      PKG_UT                                                   " & vbNewLine _
                                     & ",RTRIM(C03_01.REMARK)           AS      REMARK                                                   " & vbNewLine _
                                     & ",M60_01.SET_NAIYO               AS      GMC                                                      " & vbNewLine _
                                     & "FROM (                                                                                           " & vbNewLine _
                                     & "       SELECT C03_01.NRS_BR_CD                                                                   " & vbNewLine _
                                     & "             ,C03_01.OUTKA_NO_L                                                                  " & vbNewLine _
                                     & "             ,C03_01.ZAI_REC_NO                                         AS ZAI_REC_NO            " & vbNewLine _
                                     & "             ,C03_01.LOT_NO                                             AS LOT_NO                " & vbNewLine _
                                     & "             ,C03_01.REMARK                                             AS REMARK                " & vbNewLine _
                                     & "             ,C03_01.SERIAL_NO                                          AS SERIAL_NO             " & vbNewLine _
                                     & "             ,-1 * SUM(C03_01.ALCTD_NB)                                 AS ALCTD_NB              " & vbNewLine _
                                     & "             ,-1 * SUM(C03_01.ALCTD_QT)                                 AS ALCTD_QT              " & vbNewLine _
                                     & "             ,C02_01.GOODS_CD_NRS                                       AS GOODS_CD_NRS          " & vbNewLine _
                                     & "             ,M08_01.CUST_CD_S                                          AS CUST_CD_S             " & vbNewLine _
                                     & "             ,M08_01.CUST_CD_SS                                         AS CUST_CD_SS            " & vbNewLine _
                                     & "             ,M08_01.SEARCH_KEY_1                                       AS SEARCH_KEY_1          " & vbNewLine _
                                     & "             ,M08_01.SEARCH_KEY_2                                       AS SEARCH_KEY_2          " & vbNewLine _
                                     & "             ,M08_01.CUST_COST_CD1                                      AS CUST_COST_CD1         " & vbNewLine _
                                     & "             ,M08_01.CUST_COST_CD2                                      AS CUST_COST_CD2         " & vbNewLine _
                                     & "             ,M08_01.GOODS_CD_CUST                                      AS GOODS_CD_CUST         " & vbNewLine _
                                     & "             ,M08_01.GOODS_NM_1                                         AS GOODS_NM_1            " & vbNewLine _
                                     & "             ,MAX(ISNULL(M08_01.NB_UT,0))                               AS NB_UT                 " & vbNewLine _
                                     & "             ,M08_01.STD_IRIME_UT                                       AS STD_IRIME_UT          " & vbNewLine _
                                     & "             ,CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                              " & vbNewLine _
                                     & "                   THEN 1                                                                        " & vbNewLine _
                                     & "                   ELSE M08_01.PKG_NB                                                            " & vbNewLine _
                                     & "               END                                                      AS PKG_NB                " & vbNewLine _
                                     & "             ,M08_01.PKG_UT                                             AS PKG_UT                " & vbNewLine _
                                     & "         FROM      $LM_TRN$..C_OUTKA_S C03_01                                                    " & vbNewLine _
                                     & "        INNER JOIN $LM_TRN$..C_OUTKA_M C02_01                                                    " & vbNewLine _
                                     & "           ON C03_01.NRS_BR_CD       = C02_01.NRS_BR_CD                                          " & vbNewLine _
                                     & "          AND C03_01.OUTKA_NO_L      = C02_01.OUTKA_NO_L                                         " & vbNewLine _
                                     & "          AND C03_01.OUTKA_NO_M      = C02_01.OUTKA_NO_M                                         " & vbNewLine _
                                     & "          AND C02_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
                                     & "        INNER JOIN $LM_TRN$..C_OUTKA_L C01_01                                                    " & vbNewLine _
                                     & "           ON C03_01.NRS_BR_CD       = C01_01.NRS_BR_CD                                          " & vbNewLine _
                                     & "          AND C03_01.OUTKA_NO_L      = C01_01.OUTKA_NO_L                                         " & vbNewLine _
                                     & "          AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                        " & vbNewLine _
                                     & "          AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                       " & vbNewLine _
                                     & "          AND C01_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
                                     & "          AND C01_01.CUST_CD_L       = @CUST_CD_L                                                " & vbNewLine _
                                     & "          AND C01_01.CUST_CD_M       = @CUST_CD_M                                                " & vbNewLine _
                                     & "        INNER JOIN $LM_MST$..M_GOODS   M08_01                                                    " & vbNewLine _
                                     & "           ON C03_01.NRS_BR_CD       = M08_01.NRS_BR_CD                                          " & vbNewLine _
                                     & "          AND C02_01.GOODS_CD_NRS    = M08_01.GOODS_CD_NRS                                       " & vbNewLine _
                                     & "          AND M08_01.ALCTD_KB       <> '04'                                                      " & vbNewLine _
                                     & "          AND M08_01.CUST_CD_L       = @CUST_CD_L                                                " & vbNewLine _
                                     & "          AND M08_01.CUST_CD_M       = @CUST_CD_M                                                " & vbNewLine _
                                     & "        WHERE C03_01.SYS_DEL_FLG     = '0'                                                       " & vbNewLine _
                                     & "          AND C03_01.NRS_BR_CD       = @NRS_BR_CD                                                " & vbNewLine _
                                     & "        GROUP BY C03_01.NRS_BR_CD                                                                " & vbNewLine _
                                     & "                ,C03_01.OUTKA_NO_L                                                               " & vbNewLine _
                                     & "                ,C03_01.ZAI_REC_NO                                                               " & vbNewLine _
                                     & "                ,C03_01.LOT_NO                                                                   " & vbNewLine _
                                     & "                ,C03_01.SERIAL_NO                                                                " & vbNewLine _
                                     & "                ,C03_01.REMARK                                                                   " & vbNewLine _
                                     & "                ,C02_01.GOODS_CD_NRS                                                             " & vbNewLine _
                                     & "                ,M08_01.CUST_CD_S                                                                " & vbNewLine _
                                     & "                ,M08_01.CUST_CD_SS                                                               " & vbNewLine _
                                     & "                ,M08_01.SEARCH_KEY_1                                                             " & vbNewLine _
                                     & "                ,M08_01.SEARCH_KEY_2                                                             " & vbNewLine _
                                     & "                ,M08_01.CUST_COST_CD1                                                            " & vbNewLine _
                                     & "                ,M08_01.CUST_COST_CD2                                                            " & vbNewLine _
                                     & "                ,M08_01.GOODS_CD_CUST                                                            " & vbNewLine _
                                     & "                ,M08_01.GOODS_NM_1                                                               " & vbNewLine _
                                     & "                ,M08_01.STD_IRIME_UT                                                             " & vbNewLine _
                                     & "                ,M08_01.PKG_NB                                                                   " & vbNewLine _
                                     & "                ,M08_01.PKG_UT                                                                   " & vbNewLine _
                                     & "     )                               C03_01                                                      " & vbNewLine _
                                     & "INNER JOIN $LM_TRN$..C_OUTKA_L       C01_01                                                      " & vbNewLine _
                                     & "   ON C03_01.NRS_BR_CD             = C01_01.NRS_BR_CD                                            " & vbNewLine _
                                     & "  AND C03_01.OUTKA_NO_L            = C01_01.OUTKA_NO_L                                           " & vbNewLine _
                                     & "  AND C01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..D_ZAI_TRS      D01_01                                                       " & vbNewLine _
                                     & "   ON C03_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                            " & vbNewLine _
                                     & "  AND C03_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO                                           " & vbNewLine _
                                     & "  AND D01_01.SYS_DEL_FLG           = '0'                                                         " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                      " & vbNewLine _
                                     & "   ON C01_01.WH_CD                 = M03_01.WH_CD                                                " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                      " & vbNewLine _
                                     & "   ON C01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                            " & vbNewLine _
                                     & "  AND C01_01.CUST_CD_L             = M07_01.CUST_CD_L                                            " & vbNewLine _
                                     & "  AND C01_01.CUST_CD_M             = M07_01.CUST_CD_M                                            " & vbNewLine _
                                     & "  AND C03_01.CUST_CD_S             = M07_01.CUST_CD_S                                            " & vbNewLine _
                                     & "  AND C03_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                           " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                      " & vbNewLine _
                                     & "   ON C03_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                            " & vbNewLine _
                                     & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                            " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                      " & vbNewLine _
                                     & "   ON C01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                            " & vbNewLine _
                                     & "  AND C01_01.CUST_CD_L             = M26_01.CUST_CD_L                                            " & vbNewLine _
                                     & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                             " & vbNewLine _
                                     & " LEFT JOIN (                                                                                     " & vbNewLine _
                                     & "                        SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                     & "                              ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                     & "                              ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
                                     & "                          FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
                                     & "                    INNER JOIN (                                                                 " & vbNewLine _
                                     & "                                          SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
                                     & "                                                ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
                                     & "                                                ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
                                     & "                                            FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
                                     & "                                           WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
                                     & "                                        GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
                                     & "                                                ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
                                     & "                               )                           M60_02                                " & vbNewLine _
                                     & "                            ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
                                     & "                           AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
                                     & "                           AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
                                     & "           )                         M60_01                                                      " & vbNewLine _
                                     & "   ON C03_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                            " & vbNewLine _
                                     & "  AND C03_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                         " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                      " & vbNewLine _
                                     & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                               " & vbNewLine _
                                     & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                      " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                      " & vbNewLine _
                                     & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                               " & vbNewLine _
                                     & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                      " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                      " & vbNewLine _
                                     & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                      " & vbNewLine _
                                     & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                              " & vbNewLine _
                                     & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                              " & vbNewLine _
                                     & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                              " & vbNewLine _
                                     & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                              " & vbNewLine _
                                     & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                              " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                      " & vbNewLine _
                                     & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                       " & vbNewLine _
                                     & "           THEN D01_01.OFB_KB                                                                    " & vbNewLine _
                                     & "           ELSE KBN_03.KBN_NM7                                                                   " & vbNewLine _
                                     & "       END                         = KBN_04.KBN_CD                                               " & vbNewLine _
                                     & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                      " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                      " & vbNewLine _
                                     & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                               " & vbNewLine _
                                     & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                      " & vbNewLine _
                                     & "WHERE C01_01.NRS_BR_CD             = @NRS_BR_CD                                                  " & vbNewLine _
                                     & "  AND C01_01.OUTKA_PLAN_DATE       > @RIREKI_DATE                                                " & vbNewLine _
                                     & "  AND C01_01.OUTKA_PLAN_DATE      <= @HOUKOKU_DATE                                " & vbNewLine _
                                     & "  AND C01_01.OUTKA_STATE_KB       >= '60'                                                        " & vbNewLine _
                                     & "  AND C01_01.CUST_CD_L       = @CUST_CD_L                                                         " & vbNewLine _
                                     & "  AND C01_01.CUST_CD_M       = @CUST_CD_M                                                         " & vbNewLine _
    'END YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'END YANAI 要望番号1022
    'END YANAI 要望番号953
    'END YANAI 要望番号769

#End Region

#Region "D_IDO_TRS_MOTO"

    'START YANAI 要望番号769
    'Private Const SQL_SELECT_ZAI_IDO_MOTO As String = "SELECT                                                                                            " & vbNewLine _
    '                                                & " D02_01.O_ZAI_REC_NO             AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                                & ",D01_01.WH_CD                    AS      WH_CD                                                    " & vbNewLine _
    '                                                & ",M03_01.WH_NM                    AS      WH_NM                                                    " & vbNewLine _
    '                                                & ",M07_01.HOKAN_SEIQTO_CD          AS      SEIQTO_CD                                                " & vbNewLine _
    '                                                & ",M06_01.SEIQTO_NM                AS      SEIQTO_NM                                                " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_L                AS      CUST_CD_L                                                " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_M                AS      CUST_CD_M                                                " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_S                AS      CUST_CD_S                                                " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_SS               AS      CUST_CD_SS                                               " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_L                AS      CUST_NM_L                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_M                AS      CUST_NM_M                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_S                AS      CUST_NM_S                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_SS               AS      CUST_NM_SS                                               " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_1             AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_2             AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD1            AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD2            AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                                & ",M08_01.GOODS_CD_CUST            AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                                & ",M08_01.GOODS_NM_1               AS      GOODS_NM                                                 " & vbNewLine _
    '                                                & ",D01_01.LOT_NO                   AS      LOT_NO                                                   " & vbNewLine _
    '                                                & ",D01_01.SERIAL_NO                AS      SERIAL_NO                                                " & vbNewLine _
    '                                                & ",D01_01.INKO_DATE                AS      INKO_DATE                                                " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_1          AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_2          AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_3          AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                                & ",KBN_01.KBN_NM1                  AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                                & ",KBN_02.KBN_NM1                  AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                                & ",M26_01.JOTAI_NM                 AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                                & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                                & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                                & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                                & "  END                            AS      OFB_KB                                                   " & vbNewLine _
    '                                                & ",KBN_04.KBN_NM1                  AS      OFB_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.SPD_KB                   AS      SPD_KB                                                   " & vbNewLine _
    '                                                & ",KBN_05.KBN_NM1                  AS      SPD_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.TAX_KB                   AS      TAX_KB                                                   " & vbNewLine _
    '                                                & ",KBN_06.KBN_NM1                  AS      TAX_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.REMARK_OUT               AS      REMARK_OUT                                               " & vbNewLine _
    '                                                & ",M08_01.NB_UT                    AS      NB_UT                                                    " & vbNewLine _
    '                                                & ",CASE WHEN ABS(D02_01.PORA_ZAI_NB) < D01_01.IRIME                                                 " & vbNewLine _
    '                                                & "      THEN ABS(D02_01.PORA_ZAI_NB)                                                                " & vbNewLine _
    '                                                & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                                & "  END                            AS      IRIME                                                    " & vbNewLine _
    '                                                & ",M08_01.STD_IRIME_UT             AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_NB              AS      ZAI_NB                                                   " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_QT              AS      ZAI_QT                                                   " & vbNewLine _
    '                                                & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                            " & vbNewLine _
    '                                                & "      THEN 1                                                                                      " & vbNewLine _
    '                                                & "      ELSE M08_01.PKG_NB                                                                          " & vbNewLine _
    '                                                & "  END                            AS      PKG_NB                                                   " & vbNewLine _
    '                                                & ",M08_01.PKG_UT                   AS      PKG_UT                                                   " & vbNewLine _
    '                                                & ",RTRIM(D01_01.REMARK)            AS      REMARK                                                   " & vbNewLine _
    '                                                & ",M60_01.SET_NAIYO                AS      GMC                                                      " & vbNewLine _
    '                                                & "FROM (                                                                                            " & vbNewLine _
    '                                                & "       SELECT D02_01.NRS_BR_CD                                AS NRS_BR_CD                        " & vbNewLine _
    '                                                & "             ,D02_01.O_ZAI_REC_NO                             AS O_ZAI_REC_NO                     " & vbNewLine _
    '                                                & "             ,D02_01.IDO_DATE                                 AS IDO_DATE                         " & vbNewLine _
    '                                                & "             ,-1 * SUM(D02_01.N_PORA_ZAI_NB)                  AS PORA_ZAI_NB                      " & vbNewLine _
    '                                                & "             ,-1 * SUM(D02_01.N_PORA_ZAI_NB * D02_01.O_IRIME) AS PORA_ZAI_QT                      " & vbNewLine _
    '                                                & "         FROM $LM_TRN$..D_IDO_TRS D02_01                                                          " & vbNewLine _
    '                                                & "        WHERE D02_01.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    '                                                & "        GROUP BY D02_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                                & "                ,D02_01.O_ZAI_REC_NO                                                              " & vbNewLine _
    '                                                & "                ,D02_01.IDO_DATE                                                                  " & vbNewLine _
    '                                                & "    )                                D02_01                                                       " & vbNewLine _
    '                                                & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                       " & vbNewLine _
    '                                                & "   ON D02_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D02_01.O_ZAI_REC_NO          = D01_01.ZAI_REC_NO                                            " & vbNewLine _
    '                                                & "  AND D01_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                                & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & "  AND M08_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                                & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                                & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                                & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                                & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                                & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                                & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                                & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                                & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                                & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                                & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                                & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                                & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                                & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                                & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                                & "                                )                           M60_02                                " & vbNewLine _
    '                                                & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                                & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                                & "           )                         M60_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                                & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                                & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                                & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                                & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                                & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                                & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                                & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                                & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                                & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                                & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                                & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                                & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                                & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                                & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & "WHERE D02_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE              > @RIREKI_DATE                                                 " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                                " & vbNewLine
    'START YANAI 要望番号1022
    'Private Const SQL_SELECT_ZAI_IDO_MOTO As String = "SELECT                                                                                            " & vbNewLine _
    '                                                & " D02_01.O_ZAI_REC_NO             AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                                & ",D01_01.WH_CD                    AS      WH_CD                                                    " & vbNewLine _
    '                                                & ",M03_01.WH_NM                    AS      WH_NM                                                    " & vbNewLine _
    '                                                & ",M07_01.HOKAN_SEIQTO_CD          AS      SEIQTO_CD                                                " & vbNewLine _
    '                                                & ",M06_01.SEIQTO_NM                AS      SEIQTO_NM                                                " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_L                AS      CUST_CD_L                                                " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_M                AS      CUST_CD_M                                                " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_S                AS      CUST_CD_S                                                " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_SS               AS      CUST_CD_SS                                               " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_L                AS      CUST_NM_L                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_M                AS      CUST_NM_M                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_S                AS      CUST_NM_S                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_SS               AS      CUST_NM_SS                                               " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_1             AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_2             AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD1            AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD2            AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                                & ",M08_01.GOODS_CD_CUST            AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                                & ",M08_01.GOODS_NM_1               AS      GOODS_NM                                                 " & vbNewLine _
    '                                                & ",D01_01.LOT_NO                   AS      LOT_NO                                                   " & vbNewLine _
    '                                                & ",D01_01.SERIAL_NO                AS      SERIAL_NO                                                " & vbNewLine _
    '                                                & ",D01_01.INKO_DATE                AS      INKO_DATE                                                " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_1          AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_2          AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_3          AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                                & ",KBN_01.KBN_NM1                  AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                                & ",KBN_02.KBN_NM1                  AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                                & ",M26_01.JOTAI_NM                 AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                                & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                                & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                                & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                                & "  END                            AS      OFB_KB                                                   " & vbNewLine _
    '                                                & ",KBN_04.KBN_NM1                  AS      OFB_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.SPD_KB                   AS      SPD_KB                                                   " & vbNewLine _
    '                                                & ",KBN_05.KBN_NM1                  AS      SPD_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.TAX_KB                   AS      TAX_KB                                                   " & vbNewLine _
    '                                                & ",KBN_06.KBN_NM1                  AS      TAX_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.REMARK_OUT               AS      REMARK_OUT                                               " & vbNewLine _
    '                                                & ",M08_01.NB_UT                    AS      NB_UT                                                    " & vbNewLine _
    '                                                & ",CASE WHEN ABS(D02_01.PORA_ZAI_NB) < D01_01.IRIME                                                 " & vbNewLine _
    '                                                & "      THEN ABS(D02_01.PORA_ZAI_NB)                                                                " & vbNewLine _
    '                                                & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                                & "  END                            AS      IRIME                                                    " & vbNewLine _
    '                                                & ",M08_01.STD_IRIME_UT             AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_NB              AS      ZAI_NB                                                   " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_QT              AS      ZAI_QT                                                   " & vbNewLine _
    '                                                & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                            " & vbNewLine _
    '                                                & "      THEN 1                                                                                      " & vbNewLine _
    '                                                & "      ELSE M08_01.PKG_NB                                                                          " & vbNewLine _
    '                                                & "  END                            AS      PKG_NB                                                   " & vbNewLine _
    '                                                & ",M08_01.PKG_UT                   AS      PKG_UT                                                   " & vbNewLine _
    '                                                & ",RTRIM(D01_01.REMARK)            AS      REMARK                                                   " & vbNewLine _
    '                                                & ",M60_01.SET_NAIYO                AS      GMC                                                      " & vbNewLine _
    '                                                & "FROM (                                                                                            " & vbNewLine _
    '                                                & "       SELECT D02_01.NRS_BR_CD                                AS NRS_BR_CD                        " & vbNewLine _
    '                                                & "             ,D02_01.O_ZAI_REC_NO                             AS O_ZAI_REC_NO                     " & vbNewLine _
    '                                                & "             ,D02_01.IDO_DATE                                 AS IDO_DATE                         " & vbNewLine _
    '                                                & "             ,-1 * SUM(D02_01.N_PORA_ZAI_NB)                  AS PORA_ZAI_NB                      " & vbNewLine _
    '                                                & "             ,-1 * SUM(D02_01.N_PORA_ZAI_NB * D02_01.O_IRIME) AS PORA_ZAI_QT                      " & vbNewLine _
    '                                                & "         FROM $LM_TRN$..D_IDO_TRS D02_01                                                          " & vbNewLine _
    '                                                & "        WHERE D02_01.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    '                                                & "          AND D02_01.N_PORA_ZAI_NB <> 0                                                           " & vbNewLine _
    '                                                & "          AND D02_01.NRS_BR_CD             = @NRS_BR_CD                                           " & vbNewLine _
    '                                                & "          AND D02_01.IDO_DATE              > @RIREKI_DATE                                         " & vbNewLine _
    '                                                & "          AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                        " & vbNewLine _
    '                                                & "        GROUP BY D02_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                                & "                ,D02_01.O_ZAI_REC_NO                                                              " & vbNewLine _
    '                                                & "                ,D02_01.IDO_DATE                                                                  " & vbNewLine _
    '                                                & "    )                                D02_01                                                       " & vbNewLine _
    '                                                & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                       " & vbNewLine _
    '                                                & "   ON D02_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D02_01.O_ZAI_REC_NO          = D01_01.ZAI_REC_NO                                            " & vbNewLine _
    '                                                & "  AND D01_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                                & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & "  AND M08_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                                & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                                & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                                & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                                & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                                & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                                & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                                & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                                & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                                & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                                & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                                & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                                & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                                & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                                & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                                & "                                )                           M60_02                                " & vbNewLine _
    '                                                & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                                & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                                & "           )                         M60_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                                & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                                & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                                & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                                & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                                & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                                & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                                & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                                & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                                & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                                & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                                & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                                & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                                & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                                & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & "WHERE D02_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE              > @RIREKI_DATE                                                 " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                                " & vbNewLine
    'START YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'Private Const SQL_SELECT_ZAI_IDO_MOTO As String = "SELECT                                                                                            " & vbNewLine _
    '                                                & " D02_01.O_ZAI_REC_NO             AS      ZAI_REC_NO                                               " & vbNewLine _
    '                                                & ",D01_01.WH_CD                    AS      WH_CD                                                    " & vbNewLine _
    '                                                & ",M03_01.WH_NM                    AS      WH_NM                                                    " & vbNewLine _
    '                                                & ",M07_01.HOKAN_SEIQTO_CD          AS      SEIQTO_CD                                                " & vbNewLine _
    '                                                & ",M06_01.SEIQTO_NM                AS      SEIQTO_NM                                                " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_L                AS      CUST_CD_L                                                " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_M                AS      CUST_CD_M                                                " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_S                AS      CUST_CD_S                                                " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_SS               AS      CUST_CD_SS                                               " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_L                AS      CUST_NM_L                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_M                AS      CUST_NM_M                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_S                AS      CUST_NM_S                                                " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_SS               AS      CUST_NM_SS                                               " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_1             AS      SEARCH_KEY_1                                             " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_2             AS      SEARCH_KEY_2                                             " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD1            AS      CUST_COST_CD1                                            " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD2            AS      CUST_COST_CD2                                            " & vbNewLine _
    '                                                & ",M08_01.GOODS_CD_CUST            AS      GOODS_CD_CUST                                            " & vbNewLine _
    '                                                & ",M08_01.GOODS_NM_1               AS      GOODS_NM                                                 " & vbNewLine _
    '                                                & ",D01_01.LOT_NO                   AS      LOT_NO                                                   " & vbNewLine _
    '                                                & ",D01_01.SERIAL_NO                AS      SERIAL_NO                                                " & vbNewLine _
    '                                                & ",D01_01.INKO_DATE                AS      INKO_DATE                                                " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_1          AS      GOODS_COND_KB_1                                          " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_2          AS      GOODS_COND_KB_2                                          " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_3          AS      GOODS_COND_KB_3                                          " & vbNewLine _
    '                                                & ",KBN_01.KBN_NM1                  AS      GOODS_COND_NM_1                                          " & vbNewLine _
    '                                                & ",KBN_02.KBN_NM1                  AS      GOODS_COND_NM_2                                          " & vbNewLine _
    '                                                & ",M26_01.JOTAI_NM                 AS      GOODS_COND_NM_3                                          " & vbNewLine _
    '                                                & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                                & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                                & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                                & "  END                            AS      OFB_KB                                                   " & vbNewLine _
    '                                                & ",KBN_04.KBN_NM1                  AS      OFB_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.SPD_KB                   AS      SPD_KB                                                   " & vbNewLine _
    '                                                & ",KBN_05.KBN_NM1                  AS      SPD_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.TAX_KB                   AS      TAX_KB                                                   " & vbNewLine _
    '                                                & ",KBN_06.KBN_NM1                  AS      TAX_NM                                                   " & vbNewLine _
    '                                                & ",D01_01.REMARK_OUT               AS      REMARK_OUT                                               " & vbNewLine _
    '                                                & ",M08_01.NB_UT                    AS      NB_UT                                                    " & vbNewLine _
    '                                                & ",CASE WHEN ABS(D02_01.PORA_ZAI_NB) < D01_01.IRIME                                                 " & vbNewLine _
    '                                                & "      THEN ABS(D02_01.PORA_ZAI_NB)                                                                " & vbNewLine _
    '                                                & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                                & "  END                            AS      IRIME                                                    " & vbNewLine _
    '                                                & ",M08_01.STD_IRIME_UT             AS      STD_IRIME_UT                                             " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_NB              AS      ZAI_NB                                                   " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_QT              AS      ZAI_QT                                                   " & vbNewLine _
    '                                                & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                            " & vbNewLine _
    '                                                & "      THEN 1                                                                                      " & vbNewLine _
    '                                                & "      ELSE M08_01.PKG_NB                                                                          " & vbNewLine _
    '                                                & "  END                            AS      PKG_NB                                                   " & vbNewLine _
    '                                                & ",M08_01.PKG_UT                   AS      PKG_UT                                                   " & vbNewLine _
    '                                                & ",RTRIM(D01_01.REMARK)            AS      REMARK                                                   " & vbNewLine _
    '                                                & ",M60_01.SET_NAIYO                AS      GMC                                                      " & vbNewLine _
    '                                                & "FROM (                                                                                            " & vbNewLine _
    '                                                & "       SELECT D02_01.NRS_BR_CD                                AS NRS_BR_CD                        " & vbNewLine _
    '                                                & "             ,D02_01.O_ZAI_REC_NO                             AS O_ZAI_REC_NO                     " & vbNewLine _
    '                                                & "             ,D02_01.IDO_DATE                                 AS IDO_DATE                         " & vbNewLine _
    '                                                & "             ,-1 * SUM(D02_01.N_PORA_ZAI_NB)                  AS PORA_ZAI_NB                      " & vbNewLine _
    '                                                & "             ,-1 * SUM(D02_01.N_PORA_ZAI_NB * D02_01.O_IRIME) AS PORA_ZAI_QT                      " & vbNewLine _
    '                                                & "         FROM $LM_TRN$..D_IDO_TRS D02_01                                                          " & vbNewLine _
    '                                                & "        WHERE D02_01.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    '                                                & "          AND D02_01.N_PORA_ZAI_NB <> 0                                                           " & vbNewLine _
    '                                                & "          AND D02_01.NRS_BR_CD             = @NRS_BR_CD                                           " & vbNewLine _
    '                                                & "          AND D02_01.IDO_DATE              > @RIREKI_DATE                                         " & vbNewLine _
    '                                                & "          AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                        " & vbNewLine _
    '                                                & "        GROUP BY D02_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                                & "                ,D02_01.O_ZAI_REC_NO                                                              " & vbNewLine _
    '                                                & "                ,D02_01.IDO_DATE                                                                  " & vbNewLine _
    '                                                & "    )                                D02_01                                                       " & vbNewLine _
    '                                                & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                       " & vbNewLine _
    '                                                & "   ON D02_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D02_01.O_ZAI_REC_NO          = D01_01.ZAI_REC_NO                                            " & vbNewLine _
    '                                                & "  AND D01_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                                & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                                & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                                & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                                & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                                & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                                & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                                & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                                & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                                & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                                & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                                & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                                & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                                & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                                & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                                & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                                & "                                )                           M60_02                                " & vbNewLine _
    '                                                & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                                & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                                & "           )                         M60_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                                & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                                & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                                & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                                & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                                & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                                & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                                & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                                & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                                & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                                & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                                & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                                & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                                & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                                & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & "WHERE D02_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE              > @RIREKI_DATE                                                 " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                                " & vbNewLine
    Private Const SQL_SELECT_ZAI_IDO_MOTO As String = "SELECT                                                                                            " & vbNewLine _
                                                    & " D02_01.O_ZAI_REC_NO             AS      ZAI_REC_NO                                               " & vbNewLine _
                                                    & ",D01_01.WH_CD                    AS      WH_CD                                                    " & vbNewLine _
                                                    & ",M03_01.WH_NM                    AS      WH_NM                                                    " & vbNewLine _
                                                    & ",M07_01.HOKAN_SEIQTO_CD          AS      SEIQTO_CD                                                " & vbNewLine _
                                                    & ",M06_01.SEIQTO_NM                AS      SEIQTO_NM                                                " & vbNewLine _
                                                    & ",D01_01.CUST_CD_L                AS      CUST_CD_L                                                " & vbNewLine _
                                                    & ",D01_01.CUST_CD_M                AS      CUST_CD_M                                                " & vbNewLine _
                                                    & ",M08_01.CUST_CD_S                AS      CUST_CD_S                                                " & vbNewLine _
                                                    & ",M08_01.CUST_CD_SS               AS      CUST_CD_SS                                               " & vbNewLine _
                                                    & ",M07_01.CUST_NM_L                AS      CUST_NM_L                                                " & vbNewLine _
                                                    & ",M07_01.CUST_NM_M                AS      CUST_NM_M                                                " & vbNewLine _
                                                    & ",M07_01.CUST_NM_S                AS      CUST_NM_S                                                " & vbNewLine _
                                                    & ",M07_01.CUST_NM_SS               AS      CUST_NM_SS                                               " & vbNewLine _
                                                    & ",M08_01.SEARCH_KEY_1             AS      SEARCH_KEY_1                                             " & vbNewLine _
                                                    & ",M08_01.SEARCH_KEY_2             AS      SEARCH_KEY_2                                             " & vbNewLine _
                                                    & ",M08_01.CUST_COST_CD1            AS      CUST_COST_CD1                                            " & vbNewLine _
                                                    & ",M08_01.CUST_COST_CD2            AS      CUST_COST_CD2                                            " & vbNewLine _
                                                    & ",M08_01.GOODS_CD_CUST            AS      GOODS_CD_CUST                                            " & vbNewLine _
                                                    & ",M08_01.GOODS_NM_1               AS      GOODS_NM                                                 " & vbNewLine _
                                                    & ",D01_01.LOT_NO                   AS      LOT_NO                                                   " & vbNewLine _
                                                    & ",D01_01.SERIAL_NO                AS      SERIAL_NO                                                " & vbNewLine _
                                                    & ",D01_01.INKO_DATE                AS      INKO_DATE                                                " & vbNewLine _
                                                    & ",D01_01.GOODS_COND_KB_1          AS      GOODS_COND_KB_1                                          " & vbNewLine _
                                                    & ",D01_01.GOODS_COND_KB_2          AS      GOODS_COND_KB_2                                          " & vbNewLine _
                                                    & ",D01_01.GOODS_COND_KB_3          AS      GOODS_COND_KB_3                                          " & vbNewLine _
                                                    & ",KBN_01.KBN_NM1                  AS      GOODS_COND_NM_1                                          " & vbNewLine _
                                                    & ",KBN_02.KBN_NM1                  AS      GOODS_COND_NM_2                                          " & vbNewLine _
                                                    & ",M26_01.JOTAI_NM                 AS      GOODS_COND_NM_3                                          " & vbNewLine _
                                                    & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
                                                    & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
                                                    & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
                                                    & "  END                            AS      OFB_KB                                                   " & vbNewLine _
                                                    & ",KBN_04.KBN_NM1                  AS      OFB_NM                                                   " & vbNewLine _
                                                    & ",D01_01.SPD_KB                   AS      SPD_KB                                                   " & vbNewLine _
                                                    & ",KBN_05.KBN_NM1                  AS      SPD_NM                                                   " & vbNewLine _
                                                    & ",D01_01.TAX_KB                   AS      TAX_KB                                                   " & vbNewLine _
                                                    & ",KBN_06.KBN_NM1                  AS      TAX_NM                                                   " & vbNewLine _
                                                    & ",D01_01.REMARK_OUT               AS      REMARK_OUT                                               " & vbNewLine _
                                                    & ",M08_01.NB_UT                    AS      NB_UT                                                    " & vbNewLine _
                                                    & ",CASE WHEN ABS(D02_01.PORA_ZAI_NB) < D01_01.IRIME                                                 " & vbNewLine _
                                                    & "      THEN ABS(D02_01.PORA_ZAI_NB)                                                                " & vbNewLine _
                                                    & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
                                                    & "  END                            AS      IRIME                                                    " & vbNewLine _
                                                    & ",M08_01.STD_IRIME_UT             AS      STD_IRIME_UT                                             " & vbNewLine _
                                                    & ",D02_01.PORA_ZAI_NB              AS      ZAI_NB                                                   " & vbNewLine _
                                                    & ",D02_01.PORA_ZAI_QT              AS      ZAI_QT                                                   " & vbNewLine _
                                                    & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                            " & vbNewLine _
                                                    & "      THEN 1                                                                                      " & vbNewLine _
                                                    & "      ELSE M08_01.PKG_NB                                                                          " & vbNewLine _
                                                    & "  END                            AS      PKG_NB                                                   " & vbNewLine _
                                                    & ",M08_01.PKG_UT                   AS      PKG_UT                                                   " & vbNewLine _
                                                    & ",RTRIM(D01_01.REMARK)            AS      REMARK                                                   " & vbNewLine _
                                                    & ",M60_01.SET_NAIYO                AS      GMC                                                      " & vbNewLine _
                                                    & "FROM (                                                                                            " & vbNewLine _
                                                    & "       SELECT D02_01.NRS_BR_CD                                AS NRS_BR_CD                        " & vbNewLine _
                                                    & "             ,D02_01.O_ZAI_REC_NO                             AS O_ZAI_REC_NO                     " & vbNewLine _
                                                    & "             ,D02_01.IDO_DATE                                 AS IDO_DATE                         " & vbNewLine _
                                                    & "             ,-1 * SUM(D02_01.N_PORA_ZAI_NB)                  AS PORA_ZAI_NB                      " & vbNewLine _
                                                    & "             ,-1 * SUM(D02_01.N_PORA_ZAI_NB * D02_01.ZAIK_IRIME) AS PORA_ZAI_QT                      " & vbNewLine _
                                                    & "         FROM $LM_TRN$..D_IDO_TRS D02_01                                                          " & vbNewLine _
                                                    & "        WHERE D02_01.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                                    & "          AND D02_01.N_PORA_ZAI_NB <> 0                                                           " & vbNewLine _
                                                    & "          AND D02_01.NRS_BR_CD             = @NRS_BR_CD                                           " & vbNewLine _
                                                    & "          AND D02_01.IDO_DATE              > @RIREKI_DATE                                         " & vbNewLine _
                                                    & "          AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                        " & vbNewLine _
                                                    & "        GROUP BY D02_01.NRS_BR_CD                                                                 " & vbNewLine _
                                                    & "                ,D02_01.O_ZAI_REC_NO                                                              " & vbNewLine _
                                                    & "                ,D02_01.IDO_DATE                                                                  " & vbNewLine _
                                                    & "    )                                D02_01                                                       " & vbNewLine _
                                                    & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                       " & vbNewLine _
                                                    & "   ON D02_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                             " & vbNewLine _
                                                    & "  AND D02_01.O_ZAI_REC_NO          = D01_01.ZAI_REC_NO                                            " & vbNewLine _
                                                    & "  AND D01_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
                                                    & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                       " & vbNewLine _
                                                    & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                             " & vbNewLine _
                                                    & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                          " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
                                                    & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
                                                    & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
                                                    & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
                                                    & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
                                                    & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
                                                    & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
                                                    & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
                                                    & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
                                                    & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
                                                    & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
                                                    & " LEFT JOIN (                                                                                      " & vbNewLine _
                                                    & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                                    & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                                    & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
                                                    & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
                                                    & "                     INNER JOIN (                                                                 " & vbNewLine _
                                                    & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
                                                    & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
                                                    & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
                                                    & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
                                                    & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
                                                    & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
                                                    & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
                                                    & "                                )                           M60_02                                " & vbNewLine _
                                                    & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
                                                    & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
                                                    & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
                                                    & "           )                         M60_01                                                       " & vbNewLine _
                                                    & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
                                                    & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
                                                    & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
                                                    & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
                                                    & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
                                                    & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
                                                    & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
                                                    & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
                                                    & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
                                                    & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
                                                    & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
                                                    & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
                                                    & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
                                                    & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
                                                    & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
                                                    & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
                                                    & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
                                                    & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
                                                    & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
                                                    & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
                                                    & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
                                                    & "WHERE D02_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
                                                    & "  AND D02_01.IDO_DATE              > @RIREKI_DATE                                                 " & vbNewLine _
                                                    & "  AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                                " & vbNewLine
    'END YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'END YANAI 要望番号1022
    'END YANAI 要望番号769


#End Region

#Region "D_IDO_TRS_SAKI"

    'START YANAI 要望番号769
    'Private Const SQL_SELECT_ZAI_IDO_SAKI As String = "SELECT                                                                                            " & vbNewLine _
    '                                                & " D02_01.N_ZAI_REC_NO                     AS      ZAI_REC_NO                                       " & vbNewLine _
    '                                                & ",D01_01.WH_CD                            AS      WH_CD                                            " & vbNewLine _
    '                                                & ",M03_01.WH_NM                            AS      WH_NM                                            " & vbNewLine _
    '                                                & ",M07_01.HOKAN_SEIQTO_CD                  AS      SEIQTO_CD                                        " & vbNewLine _
    '                                                & ",M06_01.SEIQTO_NM                        AS      SEIQTO_NM                                        " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_L                        AS      CUST_CD_L                                        " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_M                        AS      CUST_CD_M                                        " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_S                        AS      CUST_CD_S                                        " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_SS                       AS      CUST_CD_SS                                       " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_L                        AS      CUST_NM_L                                        " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_M                        AS      CUST_NM_M                                        " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_S                        AS      CUST_NM_S                                        " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_SS                       AS      CUST_NM_SS                                       " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_1                     AS      SEARCH_KEY_1                                     " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_2                     AS      SEARCH_KEY_2                                     " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD1                    AS      CUST_COST_CD1                                    " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD2                    AS      CUST_COST_CD2                                    " & vbNewLine _
    '                                                & ",M08_01.GOODS_CD_CUST                    AS      GOODS_CD_CUST                                    " & vbNewLine _
    '                                                & ",M08_01.GOODS_NM_1                       AS      GOODS_NM                                         " & vbNewLine _
    '                                                & ",D01_01.LOT_NO                           AS      LOT_NO                                           " & vbNewLine _
    '                                                & ",D01_01.SERIAL_NO                        AS      SERIAL_NO                                        " & vbNewLine _
    '                                                & ",D01_01.INKO_DATE                        AS      INKO_DATE                                        " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_1                  AS      GOODS_COND_KB_1                                  " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_2                  AS      GOODS_COND_KB_2                                  " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_3                  AS      GOODS_COND_KB_3                                  " & vbNewLine _
    '                                                & ",KBN_01.KBN_NM1                          AS      GOODS_COND_NM_1                                  " & vbNewLine _
    '                                                & ",KBN_02.KBN_NM1                          AS      GOODS_COND_NM_2                                  " & vbNewLine _
    '                                                & ",M26_01.JOTAI_NM                         AS      GOODS_COND_NM_3                                  " & vbNewLine _
    '                                                & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                                & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                                & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                                & "  END                                    AS      OFB_KB                                           " & vbNewLine _
    '                                                & ",KBN_04.KBN_NM1                          AS      OFB_NM                                           " & vbNewLine _
    '                                                & ",D01_01.SPD_KB                           AS      SPD_KB                                           " & vbNewLine _
    '                                                & ",KBN_05.KBN_NM1                          AS      SPD_NM                                           " & vbNewLine _
    '                                                & ",D01_01.TAX_KB                           AS      TAX_KB                                           " & vbNewLine _
    '                                                & ",KBN_06.KBN_NM1                          AS      TAX_NM                                           " & vbNewLine _
    '                                                & ",D01_01.REMARK_OUT                       AS      REMARK_OUT                                       " & vbNewLine _
    '                                                & ",M08_01.NB_UT                            AS      NB_UT                                            " & vbNewLine _
    '                                                & ",CASE WHEN ABS(D02_01.PORA_ZAI_NB) < D01_01.IRIME                                                 " & vbNewLine _
    '                                                & "      THEN ABS(D02_01.PORA_ZAI_NB)                                                                " & vbNewLine _
    '                                                & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                                & "  END                                    AS      IRIME                                            " & vbNewLine _
    '                                                & ",M08_01.STD_IRIME_UT                     AS      STD_IRIME_UT                                     " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_NB                      AS      ZAI_NB                                           " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_QT                      AS      ZAI_QT                                           " & vbNewLine _
    '                                                & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                            " & vbNewLine _
    '                                                & "      THEN 1                                                                                      " & vbNewLine _
    '                                                & "      ELSE M08_01.PKG_NB                                                                          " & vbNewLine _
    '                                                & "  END                                    AS      PKG_NB                                           " & vbNewLine _
    '                                                & ",M08_01.PKG_UT                           AS      PKG_UT                                           " & vbNewLine _
    '                                                & ",RTRIM(D01_01.REMARK)                    AS      REMARK                                           " & vbNewLine _
    '                                                & ",M60_01.SET_NAIYO                        AS      GMC                                              " & vbNewLine _
    '                                                & "FROM (                                                                                            " & vbNewLine _
    '                                                & "       SELECT D02_01.NRS_BR_CD                         AS NRS_BR_CD                               " & vbNewLine _
    '                                                & "             ,D02_01.N_ZAI_REC_NO                      AS N_ZAI_REC_NO                            " & vbNewLine _
    '                                                & "             ,D02_01.IDO_DATE                          AS IDO_DATE                                " & vbNewLine _
    '                                                & "             ,SUM(D02_01.N_PORA_ZAI_NB)                AS PORA_ZAI_NB                             " & vbNewLine _
    '                                                & "             ,SUM(D02_01.N_PORA_ZAI_NB * D02_01.O_IRIME) AS PORA_ZAI_QT                           " & vbNewLine _
    '                                                & "         FROM $LM_TRN$..D_IDO_TRS D02_01                                                          " & vbNewLine _
    '                                                & "        WHERE D02_01.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    '                                                & "        GROUP BY D02_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                                & "                ,D02_01.N_ZAI_REC_NO                                                              " & vbNewLine _
    '                                                & "                ,D02_01.IDO_DATE                                                                  " & vbNewLine _
    '                                                & "                ,D02_01.O_IRIME                                                                   " & vbNewLine _
    '                                                & "    )                                D02_01                                                       " & vbNewLine _
    '                                                & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                       " & vbNewLine _
    '                                                & "   ON D02_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D02_01.N_ZAI_REC_NO          = D01_01.ZAI_REC_NO                                            " & vbNewLine _
    '                                                & "  AND D01_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                                & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & "  AND M08_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                                & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                                & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                                & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                                & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                                & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                                & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                                & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                                & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                                & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                                & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                                & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                                & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                                & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                                & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                                & "                                )                           M60_02                                " & vbNewLine _
    '                                                & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                                & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                                & "           )                         M60_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                                & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                                & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                                & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                                & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                                & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                                & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                                & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                                & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                                & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                                & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                                & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                                & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                                & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                                & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & "WHERE D02_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE              > @RIREKI_DATE                                                 " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                                " & vbNewLine
    'START YANAI 要望番号1022
    'Private Const SQL_SELECT_ZAI_IDO_SAKI As String = "SELECT                                                                                            " & vbNewLine _
    '                                                & " D02_01.N_ZAI_REC_NO                     AS      ZAI_REC_NO                                       " & vbNewLine _
    '                                                & ",D01_01.WH_CD                            AS      WH_CD                                            " & vbNewLine _
    '                                                & ",M03_01.WH_NM                            AS      WH_NM                                            " & vbNewLine _
    '                                                & ",M07_01.HOKAN_SEIQTO_CD                  AS      SEIQTO_CD                                        " & vbNewLine _
    '                                                & ",M06_01.SEIQTO_NM                        AS      SEIQTO_NM                                        " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_L                        AS      CUST_CD_L                                        " & vbNewLine _
    '                                                & ",D01_01.CUST_CD_M                        AS      CUST_CD_M                                        " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_S                        AS      CUST_CD_S                                        " & vbNewLine _
    '                                                & ",M08_01.CUST_CD_SS                       AS      CUST_CD_SS                                       " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_L                        AS      CUST_NM_L                                        " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_M                        AS      CUST_NM_M                                        " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_S                        AS      CUST_NM_S                                        " & vbNewLine _
    '                                                & ",M07_01.CUST_NM_SS                       AS      CUST_NM_SS                                       " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_1                     AS      SEARCH_KEY_1                                     " & vbNewLine _
    '                                                & ",M08_01.SEARCH_KEY_2                     AS      SEARCH_KEY_2                                     " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD1                    AS      CUST_COST_CD1                                    " & vbNewLine _
    '                                                & ",M08_01.CUST_COST_CD2                    AS      CUST_COST_CD2                                    " & vbNewLine _
    '                                                & ",M08_01.GOODS_CD_CUST                    AS      GOODS_CD_CUST                                    " & vbNewLine _
    '                                                & ",M08_01.GOODS_NM_1                       AS      GOODS_NM                                         " & vbNewLine _
    '                                                & ",D01_01.LOT_NO                           AS      LOT_NO                                           " & vbNewLine _
    '                                                & ",D01_01.SERIAL_NO                        AS      SERIAL_NO                                        " & vbNewLine _
    '                                                & ",D01_01.INKO_DATE                        AS      INKO_DATE                                        " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_1                  AS      GOODS_COND_KB_1                                  " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_2                  AS      GOODS_COND_KB_2                                  " & vbNewLine _
    '                                                & ",D01_01.GOODS_COND_KB_3                  AS      GOODS_COND_KB_3                                  " & vbNewLine _
    '                                                & ",KBN_01.KBN_NM1                          AS      GOODS_COND_NM_1                                  " & vbNewLine _
    '                                                & ",KBN_02.KBN_NM1                          AS      GOODS_COND_NM_2                                  " & vbNewLine _
    '                                                & ",M26_01.JOTAI_NM                         AS      GOODS_COND_NM_3                                  " & vbNewLine _
    '                                                & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                                & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                                & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                                & "  END                                    AS      OFB_KB                                           " & vbNewLine _
    '                                                & ",KBN_04.KBN_NM1                          AS      OFB_NM                                           " & vbNewLine _
    '                                                & ",D01_01.SPD_KB                           AS      SPD_KB                                           " & vbNewLine _
    '                                                & ",KBN_05.KBN_NM1                          AS      SPD_NM                                           " & vbNewLine _
    '                                                & ",D01_01.TAX_KB                           AS      TAX_KB                                           " & vbNewLine _
    '                                                & ",KBN_06.KBN_NM1                          AS      TAX_NM                                           " & vbNewLine _
    '                                                & ",D01_01.REMARK_OUT                       AS      REMARK_OUT                                       " & vbNewLine _
    '                                                & ",M08_01.NB_UT                            AS      NB_UT                                            " & vbNewLine _
    '                                                & ",CASE WHEN ABS(D02_01.PORA_ZAI_NB) < D01_01.IRIME                                                 " & vbNewLine _
    '                                                & "      THEN ABS(D02_01.PORA_ZAI_NB)                                                                " & vbNewLine _
    '                                                & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                                & "  END                                    AS      IRIME                                            " & vbNewLine _
    '                                                & ",M08_01.STD_IRIME_UT                     AS      STD_IRIME_UT                                     " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_NB                      AS      ZAI_NB                                           " & vbNewLine _
    '                                                & ",D02_01.PORA_ZAI_QT                      AS      ZAI_QT                                           " & vbNewLine _
    '                                                & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                            " & vbNewLine _
    '                                                & "      THEN 1                                                                                      " & vbNewLine _
    '                                                & "      ELSE M08_01.PKG_NB                                                                          " & vbNewLine _
    '                                                & "  END                                    AS      PKG_NB                                           " & vbNewLine _
    '                                                & ",M08_01.PKG_UT                           AS      PKG_UT                                           " & vbNewLine _
    '                                                & ",RTRIM(D01_01.REMARK)                    AS      REMARK                                           " & vbNewLine _
    '                                                & ",M60_01.SET_NAIYO                        AS      GMC                                              " & vbNewLine _
    '                                                & "FROM (                                                                                            " & vbNewLine _
    '                                                & "       SELECT D02_01.NRS_BR_CD                         AS NRS_BR_CD                               " & vbNewLine _
    '                                                & "             ,D02_01.N_ZAI_REC_NO                      AS N_ZAI_REC_NO                            " & vbNewLine _
    '                                                & "             ,D02_01.IDO_DATE                          AS IDO_DATE                                " & vbNewLine _
    '                                                & "             ,SUM(D02_01.N_PORA_ZAI_NB)                AS PORA_ZAI_NB                             " & vbNewLine _
    '                                                & "             ,SUM(D02_01.N_PORA_ZAI_NB * D02_01.O_IRIME) AS PORA_ZAI_QT                           " & vbNewLine _
    '                                                & "         FROM $LM_TRN$..D_IDO_TRS D02_01                                                          " & vbNewLine _
    '                                                & "        WHERE D02_01.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    '                                                & "          AND D02_01.N_PORA_ZAI_NB <> 0                                                           " & vbNewLine _
    '                                                & "          AND D02_01.NRS_BR_CD             = @NRS_BR_CD                                           " & vbNewLine _
    '                                                & "          AND D02_01.IDO_DATE              > @RIREKI_DATE                                         " & vbNewLine _
    '                                                & "          AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                        " & vbNewLine _
    '                                                & "        GROUP BY D02_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                                & "                ,D02_01.N_ZAI_REC_NO                                                              " & vbNewLine _
    '                                                & "                ,D02_01.IDO_DATE                                                                  " & vbNewLine _
    '                                                & "                ,D02_01.O_IRIME                                                                   " & vbNewLine _
    '                                                & "    )                                D02_01                                                       " & vbNewLine _
    '                                                & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                       " & vbNewLine _
    '                                                & "   ON D02_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D02_01.N_ZAI_REC_NO          = D01_01.ZAI_REC_NO                                            " & vbNewLine _
    '                                                & "  AND D01_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                                & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & "  AND M08_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                                & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                                & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                                & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                                & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                                & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                                & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                                & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                                & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                                & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                                & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                                & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                                & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                                & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                                & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                                & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                                & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                                & "                                )                           M60_02                                " & vbNewLine _
    '                                                & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                                & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                                & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                                & "           )                         M60_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                                & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                                & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                                & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                                & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                                & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                                & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                                & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                                & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                                & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                                & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                                & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                                & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                                & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                                & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                                & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                                & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                                & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                & "WHERE D02_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE              > @RIREKI_DATE                                                 " & vbNewLine _
    '                                                & "  AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                                " & vbNewLine
    'START YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'Private Const SQL_SELECT_ZAI_IDO_SAKI As String = "SELECT                                                                                            " & vbNewLine _
    '                                                    & " D02_01.N_ZAI_REC_NO                     AS      ZAI_REC_NO                                       " & vbNewLine _
    '                                                    & ",D01_01.WH_CD                            AS      WH_CD                                            " & vbNewLine _
    '                                                    & ",M03_01.WH_NM                            AS      WH_NM                                            " & vbNewLine _
    '                                                    & ",M07_01.HOKAN_SEIQTO_CD                  AS      SEIQTO_CD                                        " & vbNewLine _
    '                                                    & ",M06_01.SEIQTO_NM                        AS      SEIQTO_NM                                        " & vbNewLine _
    '                                                    & ",D01_01.CUST_CD_L                        AS      CUST_CD_L                                        " & vbNewLine _
    '                                                    & ",D01_01.CUST_CD_M                        AS      CUST_CD_M                                        " & vbNewLine _
    '                                                    & ",M08_01.CUST_CD_S                        AS      CUST_CD_S                                        " & vbNewLine _
    '                                                    & ",M08_01.CUST_CD_SS                       AS      CUST_CD_SS                                       " & vbNewLine _
    '                                                    & ",M07_01.CUST_NM_L                        AS      CUST_NM_L                                        " & vbNewLine _
    '                                                    & ",M07_01.CUST_NM_M                        AS      CUST_NM_M                                        " & vbNewLine _
    '                                                    & ",M07_01.CUST_NM_S                        AS      CUST_NM_S                                        " & vbNewLine _
    '                                                    & ",M07_01.CUST_NM_SS                       AS      CUST_NM_SS                                       " & vbNewLine _
    '                                                    & ",M08_01.SEARCH_KEY_1                     AS      SEARCH_KEY_1                                     " & vbNewLine _
    '                                                    & ",M08_01.SEARCH_KEY_2                     AS      SEARCH_KEY_2                                     " & vbNewLine _
    '                                                    & ",M08_01.CUST_COST_CD1                    AS      CUST_COST_CD1                                    " & vbNewLine _
    '                                                    & ",M08_01.CUST_COST_CD2                    AS      CUST_COST_CD2                                    " & vbNewLine _
    '                                                    & ",M08_01.GOODS_CD_CUST                    AS      GOODS_CD_CUST                                    " & vbNewLine _
    '                                                    & ",M08_01.GOODS_NM_1                       AS      GOODS_NM                                         " & vbNewLine _
    '                                                    & ",D01_01.LOT_NO                           AS      LOT_NO                                           " & vbNewLine _
    '                                                    & ",D01_01.SERIAL_NO                        AS      SERIAL_NO                                        " & vbNewLine _
    '                                                    & ",D01_01.INKO_DATE                        AS      INKO_DATE                                        " & vbNewLine _
    '                                                    & ",D01_01.GOODS_COND_KB_1                  AS      GOODS_COND_KB_1                                  " & vbNewLine _
    '                                                    & ",D01_01.GOODS_COND_KB_2                  AS      GOODS_COND_KB_2                                  " & vbNewLine _
    '                                                    & ",D01_01.GOODS_COND_KB_3                  AS      GOODS_COND_KB_3                                  " & vbNewLine _
    '                                                    & ",KBN_01.KBN_NM1                          AS      GOODS_COND_NM_1                                  " & vbNewLine _
    '                                                    & ",KBN_02.KBN_NM1                          AS      GOODS_COND_NM_2                                  " & vbNewLine _
    '                                                    & ",M26_01.JOTAI_NM                         AS      GOODS_COND_NM_3                                  " & vbNewLine _
    '                                                    & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
    '                                                    & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
    '                                                    & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
    '                                                    & "  END                                    AS      OFB_KB                                           " & vbNewLine _
    '                                                    & ",KBN_04.KBN_NM1                          AS      OFB_NM                                           " & vbNewLine _
    '                                                    & ",D01_01.SPD_KB                           AS      SPD_KB                                           " & vbNewLine _
    '                                                    & ",KBN_05.KBN_NM1                          AS      SPD_NM                                           " & vbNewLine _
    '                                                    & ",D01_01.TAX_KB                           AS      TAX_KB                                           " & vbNewLine _
    '                                                    & ",KBN_06.KBN_NM1                          AS      TAX_NM                                           " & vbNewLine _
    '                                                    & ",D01_01.REMARK_OUT                       AS      REMARK_OUT                                       " & vbNewLine _
    '                                                    & ",M08_01.NB_UT                            AS      NB_UT                                            " & vbNewLine _
    '                                                    & ",CASE WHEN ABS(D02_01.PORA_ZAI_NB) < D01_01.IRIME                                                 " & vbNewLine _
    '                                                    & "      THEN ABS(D02_01.PORA_ZAI_NB)                                                                " & vbNewLine _
    '                                                    & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
    '                                                    & "  END                                    AS      IRIME                                            " & vbNewLine _
    '                                                    & ",M08_01.STD_IRIME_UT                     AS      STD_IRIME_UT                                     " & vbNewLine _
    '                                                    & ",D02_01.PORA_ZAI_NB                      AS      ZAI_NB                                           " & vbNewLine _
    '                                                    & ",D02_01.PORA_ZAI_QT                      AS      ZAI_QT                                           " & vbNewLine _
    '                                                    & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                            " & vbNewLine _
    '                                                    & "      THEN 1                                                                                      " & vbNewLine _
    '                                                    & "      ELSE M08_01.PKG_NB                                                                          " & vbNewLine _
    '                                                    & "  END                                    AS      PKG_NB                                           " & vbNewLine _
    '                                                    & ",M08_01.PKG_UT                           AS      PKG_UT                                           " & vbNewLine _
    '                                                    & ",RTRIM(D01_01.REMARK)                    AS      REMARK                                           " & vbNewLine _
    '                                                    & ",M60_01.SET_NAIYO                        AS      GMC                                              " & vbNewLine _
    '                                                    & "FROM (                                                                                            " & vbNewLine _
    '                                                    & "       SELECT D02_01.NRS_BR_CD                         AS NRS_BR_CD                               " & vbNewLine _
    '                                                    & "             ,D02_01.N_ZAI_REC_NO                      AS N_ZAI_REC_NO                            " & vbNewLine _
    '                                                    & "             ,D02_01.IDO_DATE                          AS IDO_DATE                                " & vbNewLine _
    '                                                    & "             ,SUM(D02_01.N_PORA_ZAI_NB)                AS PORA_ZAI_NB                             " & vbNewLine _
    '                                                    & "             ,SUM(D02_01.N_PORA_ZAI_NB * D02_01.O_IRIME) AS PORA_ZAI_QT                           " & vbNewLine _
    '                                                    & "         FROM $LM_TRN$..D_IDO_TRS D02_01                                                          " & vbNewLine _
    '                                                    & "        WHERE D02_01.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    '                                                    & "          AND D02_01.N_PORA_ZAI_NB <> 0                                                           " & vbNewLine _
    '                                                    & "          AND D02_01.NRS_BR_CD             = @NRS_BR_CD                                           " & vbNewLine _
    '                                                    & "          AND D02_01.IDO_DATE              > @RIREKI_DATE                                         " & vbNewLine _
    '                                                    & "          AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                        " & vbNewLine _
    '                                                    & "        GROUP BY D02_01.NRS_BR_CD                                                                 " & vbNewLine _
    '                                                    & "                ,D02_01.N_ZAI_REC_NO                                                              " & vbNewLine _
    '                                                    & "                ,D02_01.IDO_DATE                                                                  " & vbNewLine _
    '                                                    & "                ,D02_01.O_IRIME                                                                   " & vbNewLine _
    '                                                    & "    )                                D02_01                                                       " & vbNewLine _
    '                                                    & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                       " & vbNewLine _
    '                                                    & "   ON D02_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                    & "  AND D02_01.N_ZAI_REC_NO          = D01_01.ZAI_REC_NO                                            " & vbNewLine _
    '                                                    & "  AND D01_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
    '                                                    & "  AND M03_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                    & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                    & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
    '                                                    & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
    '                                                    & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
    '                                                    & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
    '                                                    & "  AND M07_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                    & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
    '                                                    & "  AND M06_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                    & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
    '                                                    & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
    '                                                    & "  AND M26_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN (                                                                                      " & vbNewLine _
    '                                                    & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
    '                                                    & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                                    & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
    '                                                    & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
    '                                                    & "                     INNER JOIN (                                                                 " & vbNewLine _
    '                                                    & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
    '                                                    & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
    '                                                    & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
    '                                                    & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
    '                                                    & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
    '                                                    & "                                              AND M60_01.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                                    & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
    '                                                    & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
    '                                                    & "                                )                           M60_02                                " & vbNewLine _
    '                                                    & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
    '                                                    & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
    '                                                    & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
    '                                                    & "                          WHERE   M60_01.SYS_DEL_FLG      = '0'                                   " & vbNewLine _
    '                                                    & "           )                         M60_01                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
    '                                                    & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
    '                                                    & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
    '                                                    & "  AND KBN_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
    '                                                    & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
    '                                                    & "  AND KBN_02.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
    '                                                    & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
    '                                                    & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
    '                                                    & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
    '                                                    & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
    '                                                    & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
    '                                                    & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
    '                                                    & "  AND KBN_03.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
    '                                                    & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
    '                                                    & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
    '                                                    & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
    '                                                    & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
    '                                                    & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
    '                                                    & "  AND KBN_04.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
    '                                                    & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
    '                                                    & "  AND KBN_05.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
    '                                                    & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
    '                                                    & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
    '                                                    & "  AND KBN_06.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
    '                                                    & "WHERE D02_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
    '                                                    & "  AND D02_01.IDO_DATE              > @RIREKI_DATE                                                 " & vbNewLine _
    '                                                    & "  AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                                " & vbNewLine
    Private Const SQL_SELECT_ZAI_IDO_SAKI As String = "SELECT                                                                                            " & vbNewLine _
                                                        & " D02_01.N_ZAI_REC_NO                     AS      ZAI_REC_NO                                       " & vbNewLine _
                                                        & ",D01_01.WH_CD                            AS      WH_CD                                            " & vbNewLine _
                                                        & ",M03_01.WH_NM                            AS      WH_NM                                            " & vbNewLine _
                                                        & ",M07_01.HOKAN_SEIQTO_CD                  AS      SEIQTO_CD                                        " & vbNewLine _
                                                        & ",M06_01.SEIQTO_NM                        AS      SEIQTO_NM                                        " & vbNewLine _
                                                        & ",D01_01.CUST_CD_L                        AS      CUST_CD_L                                        " & vbNewLine _
                                                        & ",D01_01.CUST_CD_M                        AS      CUST_CD_M                                        " & vbNewLine _
                                                        & ",M08_01.CUST_CD_S                        AS      CUST_CD_S                                        " & vbNewLine _
                                                        & ",M08_01.CUST_CD_SS                       AS      CUST_CD_SS                                       " & vbNewLine _
                                                        & ",M07_01.CUST_NM_L                        AS      CUST_NM_L                                        " & vbNewLine _
                                                        & ",M07_01.CUST_NM_M                        AS      CUST_NM_M                                        " & vbNewLine _
                                                        & ",M07_01.CUST_NM_S                        AS      CUST_NM_S                                        " & vbNewLine _
                                                        & ",M07_01.CUST_NM_SS                       AS      CUST_NM_SS                                       " & vbNewLine _
                                                        & ",M08_01.SEARCH_KEY_1                     AS      SEARCH_KEY_1                                     " & vbNewLine _
                                                        & ",M08_01.SEARCH_KEY_2                     AS      SEARCH_KEY_2                                     " & vbNewLine _
                                                        & ",M08_01.CUST_COST_CD1                    AS      CUST_COST_CD1                                    " & vbNewLine _
                                                        & ",M08_01.CUST_COST_CD2                    AS      CUST_COST_CD2                                    " & vbNewLine _
                                                        & ",M08_01.GOODS_CD_CUST                    AS      GOODS_CD_CUST                                    " & vbNewLine _
                                                        & ",M08_01.GOODS_NM_1                       AS      GOODS_NM                                         " & vbNewLine _
                                                        & ",D01_01.LOT_NO                           AS      LOT_NO                                           " & vbNewLine _
                                                        & ",D01_01.SERIAL_NO                        AS      SERIAL_NO                                        " & vbNewLine _
                                                        & ",D01_01.INKO_DATE                        AS      INKO_DATE                                        " & vbNewLine _
                                                        & ",D01_01.GOODS_COND_KB_1                  AS      GOODS_COND_KB_1                                  " & vbNewLine _
                                                        & ",D01_01.GOODS_COND_KB_2                  AS      GOODS_COND_KB_2                                  " & vbNewLine _
                                                        & ",D01_01.GOODS_COND_KB_3                  AS      GOODS_COND_KB_3                                  " & vbNewLine _
                                                        & ",KBN_01.KBN_NM1                          AS      GOODS_COND_NM_1                                  " & vbNewLine _
                                                        & ",KBN_02.KBN_NM1                          AS      GOODS_COND_NM_2                                  " & vbNewLine _
                                                        & ",M26_01.JOTAI_NM                         AS      GOODS_COND_NM_3                                  " & vbNewLine _
                                                        & ",CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                             " & vbNewLine _
                                                        & "      THEN D01_01.OFB_KB                                                                          " & vbNewLine _
                                                        & "      ELSE KBN_03.KBN_NM7                                                                         " & vbNewLine _
                                                        & "  END                                    AS      OFB_KB                                           " & vbNewLine _
                                                        & ",KBN_04.KBN_NM1                          AS      OFB_NM                                           " & vbNewLine _
                                                        & ",D01_01.SPD_KB                           AS      SPD_KB                                           " & vbNewLine _
                                                        & ",KBN_05.KBN_NM1                          AS      SPD_NM                                           " & vbNewLine _
                                                        & ",D01_01.TAX_KB                           AS      TAX_KB                                           " & vbNewLine _
                                                        & ",KBN_06.KBN_NM1                          AS      TAX_NM                                           " & vbNewLine _
                                                        & ",D01_01.REMARK_OUT                       AS      REMARK_OUT                                       " & vbNewLine _
                                                        & ",M08_01.NB_UT                            AS      NB_UT                                            " & vbNewLine _
                                                        & ",CASE WHEN ABS(D02_01.PORA_ZAI_NB) < D01_01.IRIME                                                 " & vbNewLine _
                                                        & "      THEN ABS(D02_01.PORA_ZAI_NB)                                                                " & vbNewLine _
                                                        & "      ELSE D01_01.IRIME                                                                           " & vbNewLine _
                                                        & "  END                                    AS      IRIME                                            " & vbNewLine _
                                                        & ",M08_01.STD_IRIME_UT                     AS      STD_IRIME_UT                                     " & vbNewLine _
                                                        & ",D02_01.PORA_ZAI_NB                      AS      ZAI_NB                                           " & vbNewLine _
                                                        & ",D02_01.PORA_ZAI_QT                      AS      ZAI_QT                                           " & vbNewLine _
                                                        & ",CASE WHEN ISNULL(M08_01.PKG_NB,0) = 0                                                            " & vbNewLine _
                                                        & "      THEN 1                                                                                      " & vbNewLine _
                                                        & "      ELSE M08_01.PKG_NB                                                                          " & vbNewLine _
                                                        & "  END                                    AS      PKG_NB                                           " & vbNewLine _
                                                        & ",M08_01.PKG_UT                           AS      PKG_UT                                           " & vbNewLine _
                                                        & ",RTRIM(D01_01.REMARK)                    AS      REMARK                                           " & vbNewLine _
                                                        & ",M60_01.SET_NAIYO                        AS      GMC                                              " & vbNewLine _
                                                        & "FROM (                                                                                            " & vbNewLine _
                                                        & "       SELECT D02_01.NRS_BR_CD                         AS NRS_BR_CD                               " & vbNewLine _
                                                        & "             ,D02_01.N_ZAI_REC_NO                      AS N_ZAI_REC_NO                            " & vbNewLine _
                                                        & "             ,D02_01.IDO_DATE                          AS IDO_DATE                                " & vbNewLine _
                                                        & "             ,SUM(D02_01.N_PORA_ZAI_NB)                AS PORA_ZAI_NB                             " & vbNewLine _
                                                        & "             ,SUM(D02_01.N_PORA_ZAI_NB * D02_01.ZAIK_IRIME) AS PORA_ZAI_QT                           " & vbNewLine _
                                                        & "         FROM $LM_TRN$..D_IDO_TRS D02_01                                                          " & vbNewLine _
                                                        & "        WHERE D02_01.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                                        & "          AND D02_01.N_PORA_ZAI_NB <> 0                                                           " & vbNewLine _
                                                        & "          AND D02_01.NRS_BR_CD             = @NRS_BR_CD                                           " & vbNewLine _
                                                        & "          AND D02_01.IDO_DATE              > @RIREKI_DATE                                         " & vbNewLine _
                                                        & "          AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                        " & vbNewLine _
                                                        & "        GROUP BY D02_01.NRS_BR_CD                                                                 " & vbNewLine _
                                                        & "                ,D02_01.N_ZAI_REC_NO                                                              " & vbNewLine _
                                                        & "                ,D02_01.IDO_DATE                                                                  " & vbNewLine _
                                                        & "                ,D02_01.ZAIK_IRIME                                                                   " & vbNewLine _
                                                        & "    )                                D02_01                                                       " & vbNewLine _
                                                        & "INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                                                       " & vbNewLine _
                                                        & "   ON D02_01.NRS_BR_CD             = D01_01.NRS_BR_CD                                             " & vbNewLine _
                                                        & "  AND D02_01.N_ZAI_REC_NO          = D01_01.ZAI_REC_NO                                            " & vbNewLine _
                                                        & "  AND D01_01.SYS_DEL_FLG           = '0'                                                          " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_SOKO          M03_01                                                       " & vbNewLine _
                                                        & "   ON D01_01.WH_CD                 = M03_01.WH_CD                                                 " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                       " & vbNewLine _
                                                        & "   ON D01_01.NRS_BR_CD             = M08_01.NRS_BR_CD                                             " & vbNewLine _
                                                        & "  AND D01_01.GOODS_CD_NRS          = M08_01.GOODS_CD_NRS                                          " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_CUST          M07_01                                                       " & vbNewLine _
                                                        & "   ON D01_01.NRS_BR_CD             = M07_01.NRS_BR_CD                                             " & vbNewLine _
                                                        & "  AND D01_01.CUST_CD_L             = M07_01.CUST_CD_L                                             " & vbNewLine _
                                                        & "  AND D01_01.CUST_CD_M             = M07_01.CUST_CD_M                                             " & vbNewLine _
                                                        & "  AND M08_01.CUST_CD_S             = M07_01.CUST_CD_S                                             " & vbNewLine _
                                                        & "  AND M08_01.CUST_CD_SS            = M07_01.CUST_CD_SS                                            " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                                                       " & vbNewLine _
                                                        & "   ON D01_01.NRS_BR_CD             = M06_01.NRS_BR_CD                                             " & vbNewLine _
                                                        & "  AND M07_01.HOKAN_SEIQTO_CD       = M06_01.SEIQTO_CD                                             " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..M_CUSTCOND      M26_01                                                       " & vbNewLine _
                                                        & "   ON D01_01.NRS_BR_CD             = M26_01.NRS_BR_CD                                             " & vbNewLine _
                                                        & "  AND D01_01.CUST_CD_L             = M26_01.CUST_CD_L                                             " & vbNewLine _
                                                        & "  AND D01_01.GOODS_COND_KB_3       = M26_01.JOTAI_CD                                              " & vbNewLine _
                                                        & " LEFT JOIN (                                                                                      " & vbNewLine _
                                                        & "                         SELECT M60_01.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                                        & "                               ,M60_01.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                                        & "                               ,M60_01.SET_NAIYO    AS SET_NAIYO                                  " & vbNewLine _
                                                        & "                           FROM $LM_MST$..M_GOODS_DETAILS M60_01                                  " & vbNewLine _
                                                        & "                     INNER JOIN (                                                                 " & vbNewLine _
                                                        & "                                           SELECT M60_01.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
                                                        & "                                                 ,M60_01.GOODS_CD_NRS          AS GOODS_CD_NRS    " & vbNewLine _
                                                        & "                                                 ,MIN(M60_01.GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA" & vbNewLine _
                                                        & "                                             FROM $LM_MST$..M_GOODS_DETAILS M60_01                " & vbNewLine _
                                                        & "                                            WHERE M60_01.SUB_KB      = '01'                       " & vbNewLine _
                                                        & "                                         GROUP BY M60_01.NRS_BR_CD                                " & vbNewLine _
                                                        & "                                                 ,M60_01.GOODS_CD_NRS                             " & vbNewLine _
                                                        & "                                )                           M60_02                                " & vbNewLine _
                                                        & "                             ON   M60_01.NRS_BR_CD        = M60_02.NRS_BR_CD                      " & vbNewLine _
                                                        & "                            AND   M60_01.GOODS_CD_NRS     = M60_02.GOODS_CD_NRS                   " & vbNewLine _
                                                        & "                            AND   M60_01.GOODS_CD_NRS_EDA = M60_02.GOODS_CD_NRS_EDA               " & vbNewLine _
                                                        & "           )                         M60_01                                                       " & vbNewLine _
                                                        & "   ON D01_01.NRS_BR_CD             = M60_01.NRS_BR_CD                                             " & vbNewLine _
                                                        & "  AND D01_01.GOODS_CD_NRS          = M60_01.GOODS_CD_NRS                                          " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                                                       " & vbNewLine _
                                                        & "   ON D01_01.GOODS_COND_KB_1       = KBN_01.KBN_CD                                                " & vbNewLine _
                                                        & "  AND KBN_01.KBN_GROUP_CD          = 'S005'                                                       " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                                                       " & vbNewLine _
                                                        & "   ON D01_01.GOODS_COND_KB_2       = KBN_02.KBN_CD                                                " & vbNewLine _
                                                        & "  AND KBN_02.KBN_GROUP_CD          = 'S006'                                                       " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                                                       " & vbNewLine _
                                                        & "   ON KBN_03.KBN_GROUP_CD          = 'D006'                                                       " & vbNewLine _
                                                        & "  AND M07_01.NRS_BR_CD             = KBN_03.KBN_NM1                                               " & vbNewLine _
                                                        & "  AND M07_01.CUST_CD_L             = KBN_03.KBN_NM2                                               " & vbNewLine _
                                                        & "  AND M07_01.CUST_CD_M             = KBN_03.KBN_NM3                                               " & vbNewLine _
                                                        & "  AND M07_01.CUST_CD_S             = KBN_03.KBN_NM4                                               " & vbNewLine _
                                                        & "  AND M07_01.CUST_CD_SS            = KBN_03.KBN_NM5                                               " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                                                       " & vbNewLine _
                                                        & "   ON CASE WHEN RTRIM(KBN_03.KBN_NM7) = ''                                                        " & vbNewLine _
                                                        & "           THEN D01_01.OFB_KB                                                                     " & vbNewLine _
                                                        & "           ELSE KBN_03.KBN_NM7                                                                    " & vbNewLine _
                                                        & "       END                         = KBN_04.KBN_CD                                                " & vbNewLine _
                                                        & "  AND KBN_04.KBN_GROUP_CD          = 'B002'                                                       " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                                                       " & vbNewLine _
                                                        & "   ON D01_01.SPD_KB                = KBN_05.KBN_CD                                                " & vbNewLine _
                                                        & "  AND KBN_05.KBN_GROUP_CD          = 'H003'                                                       " & vbNewLine _
                                                        & " LEFT JOIN $LM_MST$..Z_KBN           KBN_06                                                       " & vbNewLine _
                                                        & "   ON D01_01.TAX_KB                = KBN_06.KBN_CD                                                " & vbNewLine _
                                                        & "  AND KBN_06.KBN_GROUP_CD          = 'Z001'                                                       " & vbNewLine _
                                                        & "WHERE D02_01.NRS_BR_CD             = @NRS_BR_CD                                                   " & vbNewLine _
                                                        & "  AND D02_01.IDO_DATE              > @RIREKI_DATE                                                 " & vbNewLine _
                                                        & "  AND D02_01.IDO_DATE             <= @HOUKOKU_DATE                                                " & vbNewLine
    'END YANAI 要望番号1052 デュポン月末在庫作成にてスタンドックス⇔塗料で混在してしまっている
    'END YANAI 要望番号1022
    'END YANAI 要望番号769

#End Region

#Region "UNION"

    Private Const SQL_UNION As String = "UNION ALL" & vbNewLine

#End Region

#End Region

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As DataRow

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
    ''' 月末在庫存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGetuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI500DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql, LMI500DAC.SelectCondition.PTN1)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI500DAC.SQL_SELECT_GETU_DATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI500DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)
        cmd.CommandTimeout = 200
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 履歴データ整合性チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectRirekiCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI500DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql, LMI500DAC.SelectCondition.PTN1)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI500DAC.SQL_SELECT_RIREKI_COUNT, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI500DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)
        cmd.CommandTimeout = 200
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JITSU_CNT", "JITSU_CNT")
        map.Add("INKA_CNT", "INKA_CNT")
        map.Add("OUTKA_CNT", "OUTKA_CNT")
        map.Add("IDO_CNT", "IDO_CNT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI500DAC.TABLE_NM_COUNT)

        Return ds

    End Function

    ''' <summary>
    ''' 日次在庫報告用データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNitijiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI500DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql, LMI500DAC.SelectCondition.PTN1)

        'SQL構築
        Me._StrSql.Append(LMI500DAC.SQL_SELECT_LMI500_001)
        Call Me.SetUnionSql(Me._StrSql)
        Me._StrSql.Append(LMI500DAC.SQL_SELECT_LMI500_002)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI500DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)
        cmd.CommandTimeout = 200
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("SOURCD", "SOURCD")
        map.Add("GMC", "GMC")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("LOT", "LOT")
        map.Add("SERIAL", "SERIAL")
        map.Add("ZAI_NB", "ZAI_NB")
        map.Add("ZAI_NB_UT", "ZAI_NB_UT")
        map.Add("ZAI_QT", "ZAI_QT")
        map.Add("ZAI_QT_UT", "ZAI_QT_UT")
        map.Add("JYOTAI", "JYOTAI")
        map.Add("FRB", "FRB")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("CUST_CD_S", "CUST_CD_S")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI500DAC.TABLE_NM_LMI500SET)

        Return ds

    End Function

    ''' <summary>
    ''' 在庫証明書作成データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI500DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL構築
        Me._StrSql.Append(LMI500DAC.SQL_SELECT_LMI501_001)
        Call Me.SetUnionSql(Me._StrSql)
        Me._StrSql.Append(LMI500DAC.SQL_SELECT_LMI501_002)

        'パラメータ + SQL設定
        Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql, LMI500DAC.SelectCondition.PTN2)

        Me._StrSql.Append(LMI500DAC.SQL_SELECT_LMI501_003)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI500DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)
        cmd.CommandTimeout = 200
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
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
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("GOODS_COND_FREE", "GOODS_COND_FREE")
        map.Add("OFB", "OFB")
        map.Add("SPD", "SPD")
        map.Add("TAX", "TAX")
        map.Add("SYOBO", "SYOBO")
        map.Add("SYOBO_SBT", "SYOBO_SBT")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("NB_UT", "NB_UT")
        map.Add("IRIME", "IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("ZAI_NB", "ZAI_NB")
        map.Add("ZAI_QT", "ZAI_QT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("REMARK", "REMARK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI500DAC.TABLE_NM_LMI501SET)

        Return ds

    End Function

    ''' <summary>
    ''' SFTPデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSftpData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI500DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL構築
        Me._StrSql.Append(LMI500DAC.SQL_SELECT_LMI502_001)
        Call Me.SetUnionSql(Me._StrSql)
        Me._StrSql.Append(LMI500DAC.SQL_SELECT_LMI502_002)

        'パラメータ + SQL構築
        Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql, LMI500DAC.SelectCondition.PTN3)
        Me._StrSql.Append(LMI500DAC.SQL_SELECT_LMI502_003)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI500DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)
        cmd.CommandTimeout = 200
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PLANT", "PLANT")
        map.Add("LOCATION", "LOCATION")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("LOT", "LOT")
        map.Add("NB", "NB")
        map.Add("QT", "QT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI500DAC.TABLE_NM_LMI502SET)

        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名取得
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

    ''' <summary>
    ''' 各SQLをUNIONした文字列を形成
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetUnionSql(ByVal sql As StringBuilder)

        sql.Append(LMI500DAC.SQL_SELECT_ZAI_RIREKI)
        sql.Append(LMI500DAC.SQL_UNION)
        sql.Append(LMI500DAC.SQL_SELECT_INKA)
        sql.Append(LMI500DAC.SQL_UNION)
        sql.Append(LMI500DAC.SQL_SELECT_OUTKA)
        sql.Append(LMI500DAC.SQL_UNION)
        sql.Append(LMI500DAC.SQL_SELECT_ZAI_IDO_MOTO)
        sql.Append(LMI500DAC.SQL_UNION)
        sql.Append(LMI500DAC.SQL_SELECT_ZAI_IDO_SAKI)

    End Sub

#End Region

#Region "抽出条件"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">抽出パターン</param>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder, ByVal ptn As LMI500DAC.SelectCondition)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", .Item("RIREKI_DATE").ToString(), DBDataType.CHAR))
            Dim custL As String = .Item("CUST_CD_L").ToString()
            Dim custM As String = .Item("CUST_CD_M").ToString()
            Dim custS As String = .Item("CUST_CD_S").ToString()

            'TODO:ハードコーディング

            prmList.Add(MyBase.GetSqlParameter("@HOUKOKU_DATE", .Item("REPORT_DATE"), DBDataType.CHAR))

            Select Case ptn

                Case LMI500DAC.SelectCondition.PTN1

                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", custL, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", custM, DBDataType.CHAR))

                Case LMI500DAC.SelectCondition.PTN2

                    '荷主コードが空の場合、全デュポン荷主を取得
                    If String.IsNullOrEmpty(custL) = True Then
                        sql.Append(LMI500DAC.SQL_SELECT_LMI501_FUL)
                    Else

                        sql.Append("WHERE MAIN.CUST_CD_L       = @CUST_CD_L                                ")
                        sql.Append(vbNewLine)
                        sql.Append("  AND MAIN.CUST_CD_M       = @CUST_CD_M                                ")
                        sql.Append(vbNewLine)
                        prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", custL, DBDataType.CHAR))
                        prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", custM, DBDataType.CHAR))

                        If String.IsNullOrEmpty(custS) = False Then
                            sql.Append("  AND MAIN.CUST_CD_S       = @CUST_CD_S                                ")
                            sql.Append(vbNewLine)
                            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", custS, DBDataType.CHAR))
                        End If

                    End If

                Case LMI500DAC.SelectCondition.PTN3

                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", custL, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", custM, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", custS, DBDataType.CHAR))
                    Dim plantCd As String = .Item("PLANT_CD").ToString()
                    prmList.Add(MyBase.GetSqlParameter("@PLANT_CD", plantCd, DBDataType.CHAR))

                    'START YANAI 要望番号953
                    'WQ49の場合
                    'If LMI500DAC.PLANTCD_SPECIAL.Equals(plantCd) = True Then

                    '    sql.Append("  AND MAIN.OFB_KB          = '01'                                      ")
                    '    sql.Append(vbNewLine)

                    'End If
                    If ("01").Equals(.Item("OFB_KB").ToString()) = True Then

                        sql.Append("  AND MAIN.OFB_KB          = '01'                                      ")
                        sql.Append(vbNewLine)

                    End If
                    'END YANAI 要望番号953

            End Select

        End With

    End Sub

#End Region

#End Region

End Class
