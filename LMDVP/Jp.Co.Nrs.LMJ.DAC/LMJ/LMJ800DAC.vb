' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ       : システム管理
'  プログラムID     :  LMJ800DAC : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMJ800DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMJ800DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' LMJ010INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMJ010IN"

    ''' <summary>
    ''' LMJ800OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMJ800OUT"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMJ800DAC"

#End Region

#Region "検索処理 SQL"

    Private Const SQL_SELECT_DATA As String = "SELECT                                                                              " & vbNewLine _
                                            & "       @CUST_CD_L                        AS      CUST_CD_L                          " & vbNewLine _
                                            & "      ,@CUST_CD_M                        AS      CUST_CD_M                          " & vbNewLine _
                                            & "      ,M08_01.CUST_CD_S                  AS      CUST_CD_S                          " & vbNewLine _
                                            & "      ,M08_01.CUST_CD_SS                 AS      CUST_CD_SS                         " & vbNewLine _
                                            & "      ,MAIN.GOODS_CD_NRS                 AS      GOODS_CD_NRS                       " & vbNewLine _
                                            & "      ,M08_01.GOODS_CD_CUST              AS      GOODS_CD_CUST                      " & vbNewLine _
                                            & "      ,M08_01.GOODS_NM_1                 AS      GOODS_NM                           " & vbNewLine _
                                            & "      ,MAIN.LOT_NO                       AS      LOT_NO                             " & vbNewLine _
                                            & "      ,MAIN.SERIAL_NO                    AS      SERIAL_NO                          " & vbNewLine _
                                            & "      ,SUM(MAIN.HIKAKU_ZAI_NB)           AS      HIKAKU_ZAI_NB                      " & vbNewLine _
                                            & "      ,SUM(MAIN.HIKAKU_ZAI_QT)           AS      HIKAKU_ZAI_QT                      " & vbNewLine _
                                            & "      ,SUM(MAIN.PORA_ZAI_NB)             AS      PORA_ZAI_NB                        " & vbNewLine _
                                            & "      ,SUM(MAIN.PORA_ZAI_QT)             AS      PORA_ZAI_QT                        " & vbNewLine _
                                            & "      ,SUM(MAIN.EXTRA_ZAI_NB)            AS      EXTRA_ZAI_NB                       " & vbNewLine _
                                            & "  FROM (                                                                            " & vbNewLine _
                                            & "        SELECT                                                                      " & vbNewLine _
                                            & "               G06_01.GOODS_CD_NRS      AS      GOODS_CD_NRS                        " & vbNewLine _
                                            & "              ,G06_01.LOT_NO            AS      LOT_NO                              " & vbNewLine _
                                            & "              ,CASE WHEN @SERIAL_FLG = '0'                                          " & vbNewLine _
                                            & "                    THEN ''                                                         " & vbNewLine _
                                            & "                    ELSE G06_01.SERIAL_NO                                           " & vbNewLine _
                                            & "                END                     AS      SERIAL_NO                           " & vbNewLine _
                                            & "              ,SUM(G06_01.ZAN_NB)       AS      HIKAKU_ZAI_NB                       " & vbNewLine _
                                            & "              ,SUM(G06_01.ZAN_QT)       AS      HIKAKU_ZAI_QT                       " & vbNewLine _
                                            & "              ,0                        AS      PORA_ZAI_NB                         " & vbNewLine _
                                            & "              ,0                        AS      PORA_ZAI_QT                         " & vbNewLine _
                                            & "              ,1                        AS      EXTRA_ZAI_NB                        " & vbNewLine _
                                            & "          FROM      $LM_TRN$..G_ZAIK_ZAN G06_01                                     " & vbNewLine _
                                            & "         INNER JOIN $LM_MST$..M_GOODS M08_01                                        " & vbNewLine _
                                            & "            ON G06_01.NRS_BR_CD     = M08_01.NRS_BR_CD                              " & vbNewLine _
                                            & "           AND G06_01.GOODS_CD_NRS  = M08_01.GOODS_CD_NRS                           " & vbNewLine _
                                            & "           AND M08_01.CUST_CD_L     = @CUST_CD_L                                    " & vbNewLine _
                                            & "           AND M08_01.CUST_CD_M     = @CUST_CD_M                                    " & vbNewLine _
                                            & "           AND M08_01.SYS_DEL_FLG   = '0'                                           " & vbNewLine _
                                            & "         WHERE G06_01.NRS_BR_CD     = @NRS_BR_CD                                    " & vbNewLine _
                                            & "           AND G06_01.INV_DATE_TO   = @SEIKYU_DATE                                  " & vbNewLine _
                                            & "           AND G06_01.SYS_DEL_FLG   = '0'                                           " & vbNewLine _
                                            & "         GROUP BY  G06_01.GOODS_CD_NRS                                              " & vbNewLine _
                                            & "                  ,G06_01.LOT_NO                                                    " & vbNewLine _
                                            & "                  ,G06_01.SERIAL_NO                                                 " & vbNewLine _
                                            & "         UNION ALL                                                                  " & vbNewLine _
                                            & "        SELECT                                                                      " & vbNewLine _
                                            & "               SUB.GOODS_CD_NRS         AS GOODS_CD_NRS                             " & vbNewLine _
                                            & "              ,SUB.LOT_NO               AS LOT_NO                                   " & vbNewLine _
                                            & "              ,SUB.SERIAL_NO            AS SERIAL_NO                                " & vbNewLine _
                                            & "              ,0                        AS HIKAKU_ZAI_NB                            " & vbNewLine _
                                            & "              ,0                        AS HIKAKU_ZAI_QT                            " & vbNewLine _
                                            & "              ,SUM(SUB.PORA_ZAI_NB)     AS PORA_ZAI_NB                              " & vbNewLine _
                                            & "              ,SUM(SUB.PORA_ZAI_QT)     AS PORA_ZAI_QT                              " & vbNewLine _
                                            & "              ,0                        AS EXTRA_ZAI_NB                             " & vbNewLine _
                                            & "          FROM (                                                                    " & vbNewLine _
                                            & "                        SELECT                                                      " & vbNewLine _
                                            & "                               D01_01.GOODS_CD_NRS               AS     GOODS_CD_NRS" & vbNewLine _
                                            & "                              ,D01_01.LOT_NO                     AS     LOT_NO      " & vbNewLine _
                                            & "                              ,CASE WHEN @SERIAL_FLG = '0'                          " & vbNewLine _
                                            & "                                    THEN ''                                         " & vbNewLine _
                                            & "                                    ELSE D01_01.SERIAL_NO                           " & vbNewLine _
                                            & "                                END                              AS     SERIAL_NO   " & vbNewLine _
                                            & "                              ,D05_01.PORA_ZAI_NB                AS     PORA_ZAI_NB " & vbNewLine _
                                            & "                              ,D05_01.PORA_ZAI_QT                AS     PORA_ZAI_QT " & vbNewLine _
                                            & "                          FROM      $LM_TRN$..D_ZAI_ZAN_JITSU D05_01                " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01                " & vbNewLine _
                                            & "                            ON D05_01.NRS_BR_CD             = D01_01.NRS_BR_CD      " & vbNewLine _
                                            & "                           AND D05_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO     " & vbNewLine _
                                            & "                           AND D01_01.CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                            & "                           AND D01_01.CUST_CD_M             = @CUST_CD_M            " & vbNewLine _
                                            & "                           AND D01_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                            & "                         WHERE D05_01.NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                            & "                           AND D05_01.RIREKI_DATE           = @RIREKI_DATE          " & vbNewLine _
                                            & "                           AND D05_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                            & "                         UNION ALL                                                  " & vbNewLine _
                                            & "                        SELECT                                                      " & vbNewLine _
                                            & "                               B02_01.GOODS_CD_NRS               AS     GOODS_CD_NRS" & vbNewLine _
                                            & "                              ,B03_01.LOT_NO                     AS     LOT_NO      " & vbNewLine _
                                            & "                              ,CASE WHEN @SERIAL_FLG = '0'                          " & vbNewLine _
                                            & "                                    THEN ''                                         " & vbNewLine _
                                            & "                                    ELSE B03_01.SERIAL_NO                           " & vbNewLine _
                                            & "                                END                              AS     SERIAL_NO   " & vbNewLine _
                                            & "                              ,( B03_01.KONSU                                       " & vbNewLine _
                                            & "                                  * M08_01.PKG_NB                                   " & vbNewLine _
                                            & "                                  + B03_01.HASU ) * (-1)         AS     PORA_ZAI_NB " & vbNewLine _
                                            & "                              ,( B03_01.KONSU                                       " & vbNewLine _
                                            & "                                  * M08_01.PKG_NB                                   " & vbNewLine _
                                            & "                                  + B03_01.HASU )                                   " & vbNewLine _
                                            & "                                  * B03_01.IRIME * (-1)          AS     PORA_ZAI_QT " & vbNewLine _
                                            & "                          FROM      $LM_TRN$..B_INKA_L B01_01                       " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..B_INKA_M B02_01                       " & vbNewLine _
                                            & "                            ON B01_01.NRS_BR_CD      = B02_01.NRS_BR_CD             " & vbNewLine _
                                            & "                           AND B01_01.INKA_NO_L      = B02_01.INKA_NO_L             " & vbNewLine _
                                            & "                           AND B02_01.SYS_DEL_FLG    = '0'                          " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..B_INKA_S B03_01                       " & vbNewLine _
                                            & "                            ON B02_01.NRS_BR_CD      = B03_01.NRS_BR_CD             " & vbNewLine _
                                            & "                           AND B02_01.INKA_NO_L      = B03_01.INKA_NO_L             " & vbNewLine _
                                            & "                           AND B02_01.INKA_NO_M      = B03_01.INKA_NO_M             " & vbNewLine _
                                            & "                           AND B03_01.SYS_DEL_FLG    = '0'                          " & vbNewLine _
                                            & "                         INNER JOIN $LM_MST$..M_GOODS  M08_01                       " & vbNewLine _
                                            & "                            ON B02_01.NRS_BR_CD      = M08_01.NRS_BR_CD             " & vbNewLine _
                                            & "                           AND B02_01.GOODS_CD_NRS   = M08_01.GOODS_CD_NRS          " & vbNewLine _
                                            & "                           AND M08_01.SYS_DEL_FLG    = '0'                          " & vbNewLine _
                                            & "                         WHERE B01_01.NRS_BR_CD      = @NRS_BR_CD                   " & vbNewLine _
                                            & "                           AND B01_01.INKA_DATE     <= @RIREKI_DATE                 " & vbNewLine _
                                            & "                           AND B01_01.HOKAN_STR_DATE > @RIREKI_DATE                 " & vbNewLine _
                                            & "                           AND B01_01.CUST_CD_L      = @CUST_CD_L                   " & vbNewLine _
                                            & "                           AND B01_01.CUST_CD_M      = @CUST_CD_M                   " & vbNewLine _
                                            & "                           AND B01_01.INKA_STATE_KB >= '50'                         " & vbNewLine _
                                            & "                         UNION ALL                                                  " & vbNewLine _
                                            & "                        SELECT                                                      " & vbNewLine _
                                            & "                               C02_01.GOODS_CD_NRS               AS     GOODS_CD_NRS" & vbNewLine _
                                            & "                              ,C03_01.LOT_NO                     AS     LOT_NO      " & vbNewLine _
                                            & "                              ,CASE WHEN @SERIAL_FLG = '0'                          " & vbNewLine _
                                            & "                                    THEN ''                                         " & vbNewLine _
                                            & "                                    ELSE C03_01.SERIAL_NO                           " & vbNewLine _
                                            & "                                END                              AS     SERIAL_NO   " & vbNewLine _
                                            & "                              ,C03_01.ALCTD_NB                   AS     PORA_ZAI_NB " & vbNewLine _
                                            & "                              ,C03_01.ALCTD_QT                   AS     PORA_ZAI_QT " & vbNewLine _
                                            & "                          FROM      $LM_TRN$..C_OUTKA_L  C01_01                     " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..C_OUTKA_M  C02_01                     " & vbNewLine _
                                            & "                            ON C01_01.NRS_BR_CD        = C02_01.NRS_BR_CD           " & vbNewLine _
                                            & "                           AND C01_01.OUTKA_NO_L       = C02_01.OUTKA_NO_L          " & vbNewLine _
                                            & "                           AND C02_01.SYS_DEL_FLG      = '0'                        " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..C_OUTKA_S  C03_01                     " & vbNewLine _
                                            & "                            ON C02_01.NRS_BR_CD        = C03_01.NRS_BR_CD           " & vbNewLine _
                                            & "                           AND C02_01.OUTKA_NO_L       = C03_01.OUTKA_NO_L          " & vbNewLine _
                                            & "                           AND C02_01.OUTKA_NO_M       = C03_01.OUTKA_NO_M          " & vbNewLine _
                                            & "                           AND C03_01.SYS_DEL_FLG      = '0'                        " & vbNewLine _
                                            & "                         INNER JOIN $LM_MST$..M_GOODS    M08_01                     " & vbNewLine _
                                            & "                            ON C02_01.NRS_BR_CD        = M08_01.NRS_BR_CD           " & vbNewLine _
                                            & "                           AND C02_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS        " & vbNewLine _
                                            & "                           AND M08_01.SYS_DEL_FLG      = '0'                        " & vbNewLine _
                                            & "                         WHERE C01_01.NRS_BR_CD        = @NRS_BR_CD                 " & vbNewLine _
                                            & "                           AND C01_01.OUTKA_PLAN_DATE <= @RIREKI_DATE               " & vbNewLine _
                                            & "                           AND C01_01.END_DATE         > @RIREKI_DATE               " & vbNewLine _
                                            & "                           AND C01_01.CUST_CD_L        = @CUST_CD_L                 " & vbNewLine _
                                            & "                           AND C01_01.CUST_CD_M        = @CUST_CD_M                 " & vbNewLine _
                                            & "                           AND C01_01.OUTKA_STATE_KB  >= '50'                       " & vbNewLine _
                                            & "                         UNION ALL                                                  " & vbNewLine _
                                            & "                        SELECT                                                      " & vbNewLine _
                                            & "                               B02_01.GOODS_CD_NRS               AS     GOODS_CD_NRS" & vbNewLine _
                                            & "                              ,B03_01.LOT_NO                     AS     LOT_NO      " & vbNewLine _
                                            & "                              ,CASE WHEN @SERIAL_FLG = '0'                          " & vbNewLine _
                                            & "                                    THEN ''                                         " & vbNewLine _
                                            & "                                    ELSE B03_01.SERIAL_NO                           " & vbNewLine _
                                            & "                                END                              AS     SERIAL_NO   " & vbNewLine _
                                            & "                              ,( B03_01.KONSU                                       " & vbNewLine _
                                            & "                                  * M08_01.PKG_NB                                   " & vbNewLine _
                                            & "                                  + B03_01.HASU )                AS     PORA_ZAI_NB " & vbNewLine _
                                            & "                              ,( B03_01.KONSU                                       " & vbNewLine _
                                            & "                                  * M08_01.PKG_NB                                   " & vbNewLine _
                                            & "                                  + B03_01.HASU ) * B03_01.IRIME AS     PORA_ZAI_QT " & vbNewLine _
                                            & "                          FROM      $LM_TRN$..B_INKA_L  B01_01                      " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..B_INKA_M  B02_01                      " & vbNewLine _
                                            & "                            ON B01_01.NRS_BR_CD       = B02_01.NRS_BR_CD            " & vbNewLine _
                                            & "                           AND B01_01.INKA_NO_L       = B02_01.INKA_NO_L            " & vbNewLine _
                                            & "                           AND B02_01.SYS_DEL_FLG     = '0'                         " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..B_INKA_S  B03_01                      " & vbNewLine _
                                            & "                            ON B02_01.NRS_BR_CD       = B03_01.NRS_BR_CD            " & vbNewLine _
                                            & "                           AND B02_01.INKA_NO_L       = B03_01.INKA_NO_L            " & vbNewLine _
                                            & "                           AND B02_01.INKA_NO_M       = B03_01.INKA_NO_M            " & vbNewLine _
                                            & "                           AND B03_01.SYS_DEL_FLG     = '0'                         " & vbNewLine _
                                            & "                         INNER JOIN $LM_MST$..M_GOODS   M08_01                      " & vbNewLine _
                                            & "                            ON B02_01.NRS_BR_CD       = M08_01.NRS_BR_CD            " & vbNewLine _
                                            & "                           AND B02_01.GOODS_CD_NRS    = M08_01.GOODS_CD_NRS         " & vbNewLine _
                                            & "                           AND M08_01.SYS_DEL_FLG     = '0'                         " & vbNewLine _
                                            & "                         WHERE B01_01.NRS_BR_CD       = @NRS_BR_CD                  " & vbNewLine _
                                            & "                           AND B01_01.HOKAN_STR_DATE  > @RIREKI_DATE                " & vbNewLine _
                                            & "                           AND B01_01.HOKAN_STR_DATE <= @SEIKYU_DATE                " & vbNewLine _
                                            & "                           AND B01_01.CUST_CD_L       = @CUST_CD_L                  " & vbNewLine _
                                            & "                           AND B01_01.CUST_CD_M       = @CUST_CD_M                  " & vbNewLine _
                                            & "                           AND B01_01.INKA_STATE_KB  >= '50'                        " & vbNewLine _
                                            & "                         UNION ALL                                                  " & vbNewLine _
                                            & "                        SELECT                                                      " & vbNewLine _
                                            & "                               C02_01.GOODS_CD_NRS               AS     GOODS_CD_NRS" & vbNewLine _
                                            & "                              ,C03_01.LOT_NO                     AS     LOT_NO      " & vbNewLine _
                                            & "                              ,CASE WHEN @SERIAL_FLG = '0'                          " & vbNewLine _
                                            & "                                    THEN ''                                         " & vbNewLine _
                                            & "                                    ELSE C03_01.SERIAL_NO                           " & vbNewLine _
                                            & "                                END                              AS     SERIAL_NO   " & vbNewLine _
                                            & "                              ,C03_01.ALCTD_NB * (-1)            AS     PORA_ZAI_NB " & vbNewLine _
                                            & "                              ,C03_01.ALCTD_QT * (-1)            AS     PORA_ZAI_QT " & vbNewLine _
                                            & "                          FROM      $LM_TRN$..C_OUTKA_L  C01_01                     " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..C_OUTKA_M  C02_01                     " & vbNewLine _
                                            & "                            ON C01_01.NRS_BR_CD        = C02_01.NRS_BR_CD           " & vbNewLine _
                                            & "                           AND C01_01.OUTKA_NO_L       = C02_01.OUTKA_NO_L          " & vbNewLine _
                                            & "                           AND C02_01.SYS_DEL_FLG      = '0'                        " & vbNewLine _
                                            & "                         INNER JOIN $LM_TRN$..C_OUTKA_S  C03_01                     " & vbNewLine _
                                            & "                            ON C02_01.NRS_BR_CD        = C03_01.NRS_BR_CD           " & vbNewLine _
                                            & "                           AND C02_01.OUTKA_NO_L       = C03_01.OUTKA_NO_L          " & vbNewLine _
                                            & "                           AND C02_01.OUTKA_NO_M       = C03_01.OUTKA_NO_M          " & vbNewLine _
                                            & "                           AND C03_01.SYS_DEL_FLG      = '0'                        " & vbNewLine _
                                            & "                         INNER JOIN $LM_MST$..M_GOODS    M08_01                     " & vbNewLine _
                                            & "                            ON C02_01.NRS_BR_CD        = M08_01.NRS_BR_CD           " & vbNewLine _
                                            & "                           AND C02_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS        " & vbNewLine _
                                            & "                           AND M08_01.SYS_DEL_FLG      = '0'                        " & vbNewLine _
                                            & "                         WHERE C01_01.NRS_BR_CD        = @NRS_BR_CD                 " & vbNewLine _
                                            & "                           AND C01_01.END_DATE        <= @SEIKYU_DATE               " & vbNewLine _
                                            & "                           AND C01_01.END_DATE         > @RIREKI_DATE               " & vbNewLine _
                                            & "                           AND C01_01.CUST_CD_L        = @CUST_CD_L                 " & vbNewLine _
                                            & "                           AND C01_01.CUST_CD_M        = @CUST_CD_M                 " & vbNewLine _
                                            & "                           AND C01_01.OUTKA_STATE_KB  >= '50'                       " & vbNewLine _
                                            & "               ) SUB                                                                " & vbNewLine _
                                            & "         GROUP BY  SUB.GOODS_CD_NRS                                                 " & vbNewLine _
                                            & "                  ,SUB.LOT_NO                                                       " & vbNewLine _
                                            & "                  ,SUB.SERIAL_NO                                                    " & vbNewLine _
                                            & "       ) MAIN                                                                       " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_GOODS M08_01                                                " & vbNewLine _
                                            & "    ON @NRS_BR_CD           = M08_01.NRS_BR_CD                                      " & vbNewLine _
                                            & "   AND MAIN.GOODS_CD_NRS    = M08_01.GOODS_CD_NRS                                   " & vbNewLine _
                                            & "   AND M08_01.SYS_DEL_FLG   = '0'                                                   " & vbNewLine _
                                            & " GROUP BY M08_01.CUST_CD_S                                                          " & vbNewLine _
                                            & "         ,M08_01.CUST_CD_SS                                                         " & vbNewLine _
                                            & "         ,MAIN.GOODS_CD_NRS                                                         " & vbNewLine _
                                            & "         ,M08_01.GOODS_CD_CUST                                                      " & vbNewLine _
                                            & "         ,M08_01.GOODS_NM_1                                                         " & vbNewLine _
                                            & "         ,MAIN.LOT_NO                                                               " & vbNewLine _
                                            & "         ,MAIN.SERIAL_NO                                                            " & vbNewLine _
                                            & "HAVING    SUM(MAIN.HIKAKU_ZAI_NB) <> SUM(MAIN.PORA_ZAI_NB)                          " & vbNewLine _
                                            & "       OR SUM(MAIN.HIKAKU_ZAI_QT) <> SUM(MAIN.PORA_ZAI_QT)                          " & vbNewLine _
                                            & " ORDER BY M08_01.CUST_CD_S                                                          " & vbNewLine _
                                            & "         ,M08_01.CUST_CD_SS                                                         " & vbNewLine


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
    ''' データ抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMJ800DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ800DAC.SQL_SELECT_DATA)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMJ800DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("HIKAKU_ZAI_NB", "HIKAKU_ZAI_NB")
        map.Add("HIKAKU_ZAI_QT", "HIKAKU_ZAI_QT")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("EXTRA_ZAI_NB", "EXTRA_ZAI_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMJ800DAC.TABLE_NM_OUT)

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

#End Region

#Region "抽出条件"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_FLG", .Item("SERIAL_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIKYU_DATE", .Item("SEIKYU_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", .Item("RIREKI_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

End Class
