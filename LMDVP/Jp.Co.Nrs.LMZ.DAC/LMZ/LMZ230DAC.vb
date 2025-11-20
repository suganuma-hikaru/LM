' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ230DAC : 運賃タリフマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ230DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ230DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用(運賃タリフマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_UNCHIN As String = " SELECT COUNT(UNCHIN.UNCHIN_TARIFF_CD)   AS SELECT_CNT   " & vbNewLine



    ''' <summary>
    ''' カウント用(運賃タリフセットマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_UNCHINSET As String = " SELECT  COUNT(UTCNT.UNCHIN_TARIFF_CD)   AS SELECT_CNT      " & vbNewLine _
                                        & "                  FROM  (                                                  " & vbNewLine



    '■要望暗号833対応 (2012/03/06)START------------------------------------------
    '''' <summary>
    '''' M_UNCHIN_TARIFFデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_UNCHIN As String = " SELECT                                                    " & vbNewLine _
    '                                    & "      UNCHIN.UNCHIN_TARIFF_CD           AS UNCHIN_TARIFF_CD           " & vbNewLine _
    '                                    & "     ,UNCHIN.UNCHIN_TARIFF_REM          AS UNCHIN_TARIFF_REM          " & vbNewLine _
    '                                    & "     ,KBN.KBN_NM1                       AS TABLE_TP_NM                " & vbNewLine _
    '                                    & "     ,UNCHIN.TABLE_TP                   AS TABLE_TP                   " & vbNewLine _
    '                                    & "     ,UNCHIN.STR_DATE                   AS STR_DATE                   " & vbNewLine _
    '                                    & "     ,UNCHIN.NRS_BR_CD                  AS NRS_BR_CD                  " & vbNewLine _
    '                                    & "     ,UNCHIN.DATA_TP                    AS DATA_TP                    " & vbNewLine _
    '                                    & "     ,''                                AS CUST_CD_L                  " & vbNewLine _
    '                                    & "     ,''                                AS CUST_NM_L                  " & vbNewLine _
    '                                    & "     ,''                                AS CUST_CD_M                  " & vbNewLine _
    '                                    & "     ,''                                AS CUST_NM_M                  " & vbNewLine
    ''' <summary>
    ''' M_UNCHIN_TARIFFデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_UNCHIN As String = " SELECT                                                    " & vbNewLine _
                                    & "      UNCHIN.UNCHIN_TARIFF_CD           AS UNCHIN_TARIFF_CD           " & vbNewLine _
                                    & "     ,UNCHIN.UNCHIN_TARIFF_REM          AS UNCHIN_TARIFF_REM          " & vbNewLine _
                                    & "     ,KBN.KBN_NM1                       AS TABLE_TP_NM                " & vbNewLine _
                                    & "     ,UNCHIN.TABLE_TP                   AS TABLE_TP                   " & vbNewLine _
                                    & "     ,UNCHIN.STR_DATE                   AS STR_DATE                   " & vbNewLine _
                                    & "     ,UNCHIN.NRS_BR_CD                  AS NRS_BR_CD                  " & vbNewLine _
                                    & "     ,''                                AS CUST_CD_L                  " & vbNewLine _
                                    & "     ,''                                AS CUST_NM_L                  " & vbNewLine _
                                    & "     ,''                                AS CUST_CD_M                  " & vbNewLine _
                                    & "     ,''                                AS CUST_NM_M                  " & vbNewLine

    '■要望暗号833対応 (2012/03/06)END------------------------------------------

    '■要望暗号833対応 (2012/03/06)START------------------------------------------
    '''' <summary>
    '''' M_UNCHIN_TARIFF_SETデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_UNCHINSET As String = " SELECT                                    " & vbNewLine _
    '                                              & "       UTS.UNCHIN_TARIFF_CD                    " & vbNewLine _
    '                                              & "      ,UTS.UNCHIN_TARIFF_REM                   " & vbNewLine _
    '                                              & "      ,UTS.TABLE_TP_NM                         " & vbNewLine _
    '                                              & "      ,UTS.TABLE_TP                            " & vbNewLine _
    '                                              & "      ,UTS.STR_DATE                            " & vbNewLine _
    '                                              & "      ,UTS.NRS_BR_CD                           " & vbNewLine _
    '                                              & "      ,UTS.DATA_TP                             " & vbNewLine _
    '                                              & "      ,UTS.CUST_CD_L                           " & vbNewLine _
    '                                              & "      ,UTS.CUST_NM_L                           " & vbNewLine _
    '                                              & "      ,UTS.CUST_CD_M                           " & vbNewLine _
    '                                              & "      ,UTS.CUST_NM_M                           " & vbNewLine
    ''' <summary>
    ''' M_UNCHIN_TARIFF_SETデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_UNCHINSET As String = " SELECT                                    " & vbNewLine _
                                              & "       UTS.UNCHIN_TARIFF_CD                    " & vbNewLine _
                                              & "      ,UTS.UNCHIN_TARIFF_REM                   " & vbNewLine _
                                              & "      ,UTS.TABLE_TP_NM                         " & vbNewLine _
                                              & "      ,UTS.TABLE_TP                            " & vbNewLine _
                                              & "      ,UTS.STR_DATE                            " & vbNewLine _
                                              & "      ,UTS.NRS_BR_CD                           " & vbNewLine _
                                              & "      ,UTS.CUST_CD_L                           " & vbNewLine _
                                              & "      ,UTS.CUST_NM_L                           " & vbNewLine _
                                              & "      ,UTS.CUST_CD_M                           " & vbNewLine _
                                              & "      ,UTS.CUST_NM_M                           " & vbNewLine
    '■要望暗号833対応 (2012/03/06)END------------------------------------------

#End Region

#Region "FROM句"

    '要望対応:1248 terakawa 2013.03.21 Start
    '■要望暗号833対応 (2012/03/06)START------------------------------------------
    '''' <summary>
    '''' 運賃タリフマスタ
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_DATA_UNCHIN As String = "FROM                                                                            " & vbNewLine _
    '                                          & "          (SELECT                                                                  " & vbNewLine _
    '                                          & "                        UT.NRS_BR_CD                                               " & vbNewLine _
    '                                          & "                       ,UT.UNCHIN_TARIFF_CD                                        " & vbNewLine _
    '                                          & "                       ,UT.UNCHIN_TARIFF_REM                                       " & vbNewLine _
    '                                          & "                       ,UT.STR_DATE                                                " & vbNewLine _
    '                                          & "                       ,UT.DATA_TP                                                 " & vbNewLine _
    '                                          & "                       ,UT.TABLE_TP                                                " & vbNewLine _
    '                                          & "           FROM                                                                    " & vbNewLine _
    '                                          & "                       $LM_MST$..M_UNCHIN_TARIFF     AS UT                         " & vbNewLine _
    '                                          & "           WHERE                                                                   " & vbNewLine _
    '                                          & "                        UT.SYS_DEL_FLG       =  '0'                                " & vbNewLine _
    '                                          & "             AND        UT.UNCHIN_TARIFF_REM IS NOT NULL                           " & vbNewLine _
    '                                          & "             AND        UT.UNCHIN_TARIFF_REM <> ''                                 " & vbNewLine _
    '                                          & "           GROUP BY                                                                " & vbNewLine _
    '                                          & "                        UT.NRS_BR_CD                                               " & vbNewLine _
    '                                          & "                       ,UT.UNCHIN_TARIFF_CD                                        " & vbNewLine _
    '                                          & "                       ,UT.UNCHIN_TARIFF_REM                                       " & vbNewLine _
    '                                          & "                       ,UT.STR_DATE                                                " & vbNewLine _
    '                                          & "                       ,UT.DATA_TP                                                 " & vbNewLine _
    '                                          & "                       ,UT.TABLE_TP)                  AS UNCHIN                    " & vbNewLine _
    '                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                 AS KBN                       " & vbNewLine _
    '                                          & "             ON UNCHIN.TABLE_TP              = KBN.KBN_CD                          " & vbNewLine _
    '                                          & "            AND KBN.KBN_GROUP_CD             = 'U011'                              " & vbNewLine _
    '                                          & "            AND KBN.SYS_DEL_FLG              = '0'                                 " & vbNewLine _
    '                                          & "WHERE                                                                              " & vbNewLine
    ''' <summary>
    ''' 運賃タリフマスタ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA_UNCHIN As String = "FROM                                                                            " & vbNewLine _
                                          & "          (SELECT                                                                  " & vbNewLine _
                                          & "                        UT.NRS_BR_CD                                               " & vbNewLine _
                                          & "                       ,UT.UNCHIN_TARIFF_CD                                        " & vbNewLine _
                                          & "                       ,UT.UNCHIN_TARIFF_REM                                       " & vbNewLine _
                                          & "                       ,UT.STR_DATE                                                " & vbNewLine _
                                          & "                       ,UT.TABLE_TP                                                " & vbNewLine _
                                          & "           FROM                                                                    " & vbNewLine _
                                          & "                       $LM_MST$..M_UNCHIN_TARIFF     AS UT                         " & vbNewLine _
                                          & "           WHERE                                                                   " & vbNewLine _
                                          & "                        UT.SYS_DEL_FLG       =  '0'                                " & vbNewLine _
                                          & "             AND        UT.UNCHIN_TARIFF_REM IS NOT NULL                           " & vbNewLine _
                                          & "             AND        UT.UNCHIN_TARIFF_REM <> ''                                 " & vbNewLine _
                                          & "           GROUP BY                                                                " & vbNewLine _
                                          & "                        UT.NRS_BR_CD                                               " & vbNewLine _
                                          & "                       ,UT.UNCHIN_TARIFF_CD                                        " & vbNewLine _
                                          & "                       ,UT.UNCHIN_TARIFF_REM                                       " & vbNewLine _
                                          & "                       ,UT.STR_DATE                                                " & vbNewLine _
                                          & "                       ,UT.TABLE_TP)                  AS UNCHIN                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                 AS KBN                       " & vbNewLine _
                                          & "             ON UNCHIN.TABLE_TP              = KBN.KBN_CD                          " & vbNewLine _
                                          & "            AND KBN.KBN_GROUP_CD             = 'U011'                              " & vbNewLine _
                                          & "            AND KBN.SYS_DEL_FLG              = '0'                                 " & vbNewLine

    Private Const SQL_FROM_TUNCHIN_TARIFF As String = "      INNER JOIN $LM_MST$..M_TUNCHIN_TARIFF   AS T_UT                    " & vbNewLine _
                                          & "        ON T_UT.SYS_DEL_FLG              = '0'                                     " & vbNewLine _
                                          & "       AND T_UT.UNCHIN_TARIFF_CD         =  UNCHIN.UNCHIN_TARIFF_CD                " & vbNewLine


    Private Const SQL_WHERE As String = "      WHERE                                                                            " & vbNewLine
    '要望対応:1248 terakawa 2013.03.21 End

    '■要望暗号833対応 (2012/03/06)END------------------------------------------

    '■要望暗号833対応 (2012/03/06)START------------------------------------------
    '''' <summary>
    '''' 運賃タリフセットマスタ
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_DATA_UNCHINSET As String = "FROM                                                                              " & vbNewLine _
    '                                    & "      (SELECT                                                                                 " & vbNewLine _
    '                                    & "       UNCHIN_SET.UNCHIN_TARIFF_CD1      AS UNCHIN_TARIFF_CD                                  " & vbNewLine _
    '                                    & "      ,UNCHIN.UNCHIN_TARIFF_REM          AS UNCHIN_TARIFF_REM                                 " & vbNewLine _
    '                                    & "      ,KBN.KBN_NM1                       AS TABLE_TP_NM                                       " & vbNewLine _
    '                                    & "      ,UNCHIN.TABLE_TP                   AS TABLE_TP                                          " & vbNewLine _
    '                                    & "      ,UNCHIN.STR_DATE                   AS STR_DATE                                          " & vbNewLine _
    '                                    & "      ,UNCHIN_SET.NRS_BR_CD              AS NRS_BR_CD                                         " & vbNewLine _
    '                                    & "      ,UNCHIN.DATA_TP                    AS DATA_TP                                           " & vbNewLine _
    '                                    & "      ,UNCHIN_SET.CUST_CD_L              AS CUST_CD_L                                         " & vbNewLine _
    '                                    & "      ,CUST.CUST_NM_L                    AS CUST_NM_L                                         " & vbNewLine _
    '                                    & "      ,UNCHIN_SET.CUST_CD_M              AS CUST_CD_M                                         " & vbNewLine _
    '                                    & "      ,CUST.CUST_NM_M                    AS CUST_NM_M                                         " & vbNewLine _
    '                                    & "      FROM                                                                                    " & vbNewLine _
    '                                    & "           $LM_MST$..M_UNCHIN_TARIFF_SET             AS UNCHIN_SET                            " & vbNewLine _
    '                                    & "            LEFT OUTER JOIN                                                                   " & vbNewLine _
    '                                    & "          (SELECT                                                                             " & vbNewLine _
    '                                    & "                        UT.NRS_BR_CD                                                          " & vbNewLine _
    '                                    & "                       ,UT.UNCHIN_TARIFF_CD                                                   " & vbNewLine _
    '                                    & "                       ,UT.UNCHIN_TARIFF_REM                                                  " & vbNewLine _
    '                                    & "                       ,UT.STR_DATE                                                           " & vbNewLine _
    '                                    & "                       ,UT.DATA_TP                                                            " & vbNewLine _
    '                                    & "                       ,UT.TABLE_TP                                                           " & vbNewLine _
    '                                    & "           FROM                                                                               " & vbNewLine _
    '                                    & "                       $LM_MST$..M_UNCHIN_TARIFF     AS UT                                    " & vbNewLine _
    '                                    & "           WHERE                                                                              " & vbNewLine _
    '                                    & "                        UT.SYS_DEL_FLG       =  '0'                                           " & vbNewLine _
    '                                    & "             AND        UT.UNCHIN_TARIFF_REM IS NOT NULL                                      " & vbNewLine _
    '                                    & "             AND        UT.UNCHIN_TARIFF_REM <> ''                                            " & vbNewLine _
    '                                    & "           GROUP BY                                                                           " & vbNewLine _
    '                                    & "                        UT.NRS_BR_CD                                                          " & vbNewLine _
    '                                    & "                       ,UT.UNCHIN_TARIFF_CD                                                   " & vbNewLine _
    '                                    & "                       ,UT.UNCHIN_TARIFF_REM                                                  " & vbNewLine _
    '                                    & "                       ,UT.STR_DATE                                                           " & vbNewLine _
    '                                    & "                       ,UT.DATA_TP                                                            " & vbNewLine _
    '                                    & "                       ,UT.TABLE_TP)                  AS UNCHIN                               " & vbNewLine _
    '                                    & "              ON UNCHIN_SET.UNCHIN_TARIFF_CD1 = UNCHIN.UNCHIN_TARIFF_CD                       " & vbNewLine _
    '                                    & "             AND UNCHIN_SET.NRS_BR_CD             = UNCHIN.NRS_BR_CD                          " & vbNewLine _
    '                                    & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                AS KBN                                   " & vbNewLine _
    '                                    & "        ON UNCHIN.TABLE_TP               = KBN.KBN_CD                                         " & vbNewLine _
    '                                    & "       AND KBN.KBN_GROUP_CD              = 'U011'                                             " & vbNewLine _
    '                                    & "       AND KBN.SYS_DEL_FLG               = '0'                                                " & vbNewLine _
    '                                    & "      LEFT OUTER JOIN                                                                         " & vbNewLine _
    '                                    & "                      ( SELECT                                                                " & vbNewLine _
    '                                    & "                            CUST.NRS_BR_CD                                                    " & vbNewLine _
    '                                    & "                           ,CUST.CUST_CD_L                                                    " & vbNewLine _
    '                                    & "                           ,CUST.CUST_NM_L                                                    " & vbNewLine _
    '                                    & "                           ,CUST.CUST_CD_M                                                    " & vbNewLine _
    '                                    & "                           ,CUST.CUST_NM_M                                                    " & vbNewLine _
    '                                    & "                           ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S                                " & vbNewLine _
    '                                    & "                           ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS                               " & vbNewLine _
    '                                    & "                        FROM  $LM_MST$..M_CUST               AS CUST                          " & vbNewLine _
    '                                    & "                        WHERE SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                    & "                        GROUP BY   CUST.NRS_BR_CD                                             " & vbNewLine _
    '                                    & "                                  ,CUST.CUST_CD_L                                             " & vbNewLine _
    '                                    & "                                  ,CUST.CUST_NM_L                                             " & vbNewLine _
    '                                    & "                                  ,CUST.CUST_CD_M                                             " & vbNewLine _
    '                                    & "                                  ,CUST.CUST_NM_M                                             " & vbNewLine _
    '                                    & "                        ) AS CUST                                                             " & vbNewLine _
    '                                    & "             ON UNCHIN_SET.NRS_BR_CD          = CUST.NRS_BR_CD                                     " & vbNewLine _
    '                                    & "            AND UNCHIN_SET.CUST_CD_L          = CUST.CUST_CD_L                                     " & vbNewLine _
    '                                    & "            AND UNCHIN_SET.CUST_CD_M          = CUST.CUST_CD_M                                     " & vbNewLine _
    '                                    & "      WHERE     UNCHIN_SET.SYS_DEL_FLG        = '0'                                                " & vbNewLine _
    '                                    & "      AND       UNCHIN_SET.UNCHIN_TARIFF_CD1 <> ''                                                 " & vbNewLine _
    '                                    & "      UNION                                                                                   " & vbNewLine _
    '                                    & "      SELECT                                                                                  " & vbNewLine _
    '                                    & "       UNCHIN_SET.UNCHIN_TARIFF_CD2      AS UNCHIN_TARIFF_CD                                  " & vbNewLine _
    '                                    & "      ,UNCHIN.UNCHIN_TARIFF_REM          AS UNCHIN_TARIFF_REM                                 " & vbNewLine _
    '                                    & "      ,KBN.KBN_NM1                       AS TABLE_TP_NM                                       " & vbNewLine _
    '                                    & "      ,UNCHIN.TABLE_TP                   AS TABLE_TP                                          " & vbNewLine _
    '                                    & "      ,UNCHIN.STR_DATE                   AS STR_DATE                                          " & vbNewLine _
    '                                    & "      ,UNCHIN_SET.NRS_BR_CD              AS NRS_BR_CD                                         " & vbNewLine _
    '                                    & "      ,UNCHIN.DATA_TP                    AS DATA_TP                                           " & vbNewLine _
    '                                    & "      ,UNCHIN_SET.CUST_CD_L              AS CUST_CD_L                                         " & vbNewLine _
    '                                    & "      ,CUST.CUST_NM_L                    AS CUST_NM_L                                         " & vbNewLine _
    '                                    & "      ,UNCHIN_SET.CUST_CD_M              AS CUST_CD_M                                         " & vbNewLine _
    '                                    & "      ,CUST.CUST_NM_M                    AS CUST_NM_M                                         " & vbNewLine _
    '                                    & "      FROM                                                                                    " & vbNewLine _
    '                                    & "                 $LM_MST$..M_UNCHIN_TARIFF_SET             AS UNCHIN_SET                      " & vbNewLine _
    '                                    & "                  LEFT OUTER JOIN                                                             " & vbNewLine _
    '                                    & "                (SELECT                                                                       " & vbNewLine _
    '                                    & "                              UT.NRS_BR_CD                                                    " & vbNewLine _
    '                                    & "                             ,UT.UNCHIN_TARIFF_CD                                             " & vbNewLine _
    '                                    & "                             ,UT.UNCHIN_TARIFF_REM                                            " & vbNewLine _
    '                                    & "                             ,UT.STR_DATE                                                     " & vbNewLine _
    '                                    & "                             ,UT.DATA_TP                                                      " & vbNewLine _
    '                                    & "                             ,UT.TABLE_TP                                                     " & vbNewLine _
    '                                    & "                 FROM                                                                         " & vbNewLine _
    '                                    & "                             $LM_MST$..M_UNCHIN_TARIFF     AS UT                              " & vbNewLine _
    '                                    & "                 WHERE                                                                        " & vbNewLine _
    '                                    & "                              UT.SYS_DEL_FLG       =  '0'                                     " & vbNewLine _
    '                                    & "                   AND        UT.UNCHIN_TARIFF_REM IS NOT NULL                                " & vbNewLine _
    '                                    & "                   AND        UT.UNCHIN_TARIFF_REM <> ''                                      " & vbNewLine _
    '                                    & "                 GROUP BY                                                                     " & vbNewLine _
    '                                    & "                              UT.NRS_BR_CD                                                    " & vbNewLine _
    '                                    & "                             ,UT.UNCHIN_TARIFF_CD                                             " & vbNewLine _
    '                                    & "                             ,UT.UNCHIN_TARIFF_REM                                            " & vbNewLine _
    '                                    & "                             ,UT.STR_DATE                                                     " & vbNewLine _
    '                                    & "                             ,UT.DATA_TP                                                      " & vbNewLine _
    '                                    & "                             ,UT.TABLE_TP)                  AS UNCHIN                         " & vbNewLine _
    '                                    & "                    ON UNCHIN_SET.UNCHIN_TARIFF_CD2      = UNCHIN.UNCHIN_TARIFF_CD            " & vbNewLine _
    '                                    & "                   AND UNCHIN_SET.NRS_BR_CD              = UNCHIN.NRS_BR_CD                   " & vbNewLine _
    '                                    & "            LEFT OUTER JOIN $LM_MST$..Z_KBN                AS KBN                             " & vbNewLine _
    '                                    & "              ON UNCHIN.TABLE_TP               = KBN.KBN_CD                                   " & vbNewLine _
    '                                    & "             AND KBN.KBN_GROUP_CD              = 'U011'                                       " & vbNewLine _
    '                                    & "             AND KBN.SYS_DEL_FLG               = '0'                                          " & vbNewLine _
    '                                    & "            LEFT OUTER JOIN                                                                   " & vbNewLine _
    '                                    & "                            ( SELECT                                                          " & vbNewLine _
    '                                    & "                                  CUST.NRS_BR_CD                                              " & vbNewLine _
    '                                    & "                                 ,CUST.CUST_CD_L                                              " & vbNewLine _
    '                                    & "                                 ,CUST.CUST_NM_L                                              " & vbNewLine _
    '                                    & "                                 ,CUST.CUST_CD_M                                              " & vbNewLine _
    '                                    & "                                 ,CUST.CUST_NM_M                                              " & vbNewLine _
    '                                    & "                                 ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S                          " & vbNewLine _
    '                                    & "                                 ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS                         " & vbNewLine _
    '                                    & "                              FROM  $LM_MST$..M_CUST               AS CUST                    " & vbNewLine _
    '                                    & "                              WHERE SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                    & "                              GROUP BY   CUST.NRS_BR_CD                                       " & vbNewLine _
    '                                    & "                                        ,CUST.CUST_CD_L                                       " & vbNewLine _
    '                                    & "                                        ,CUST.CUST_NM_L                                       " & vbNewLine _
    '                                    & "                                        ,CUST.CUST_CD_M                                       " & vbNewLine _
    '                                    & "                                        ,CUST.CUST_NM_M                                       " & vbNewLine _
    '                                    & "                              ) AS CUST                                                       " & vbNewLine _
    '                                    & "              ON UNCHIN_SET.NRS_BR_CD          = CUST.NRS_BR_CD                               " & vbNewLine _
    '                                    & "             AND UNCHIN_SET.CUST_CD_L          = CUST.CUST_CD_L                               " & vbNewLine _
    '                                    & "             AND UNCHIN_SET.CUST_CD_M          = CUST.CUST_CD_M                               " & vbNewLine _
    '                                    & "      WHERE      UNCHIN_SET.SYS_DEL_FLG        = '0'                                          " & vbNewLine _
    '                                    & "      AND        UNCHIN_SET.UNCHIN_TARIFF_CD2 <> ''                                           " & vbNewLine _
    '                                    & "      ) AS   UTS                                                                              " & vbNewLine _
    '                                    & "      WHERE                                                                                   " & vbNewLine
    ''' <summary>
    ''' 運賃タリフセットマスタ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA_UNCHINSET As String = "FROM                                                                              " & vbNewLine _
                                    & "      (SELECT                                                                                 " & vbNewLine _
                                    & "       UNCHIN_SET.UNCHIN_TARIFF_CD1      AS UNCHIN_TARIFF_CD                                  " & vbNewLine _
                                    & "      ,UNCHIN.UNCHIN_TARIFF_REM          AS UNCHIN_TARIFF_REM                                 " & vbNewLine _
                                    & "      ,KBN.KBN_NM1                       AS TABLE_TP_NM                                       " & vbNewLine _
                                    & "      ,UNCHIN.TABLE_TP                   AS TABLE_TP                                          " & vbNewLine _
                                    & "      ,UNCHIN.STR_DATE                   AS STR_DATE                                          " & vbNewLine _
                                    & "      ,UNCHIN_SET.NRS_BR_CD              AS NRS_BR_CD                                         " & vbNewLine _
                                    & "      ,UNCHIN_SET.CUST_CD_L              AS CUST_CD_L                                         " & vbNewLine _
                                    & "      ,CUST.CUST_NM_L                    AS CUST_NM_L                                         " & vbNewLine _
                                    & "      ,UNCHIN_SET.CUST_CD_M              AS CUST_CD_M                                         " & vbNewLine _
                                    & "      ,CUST.CUST_NM_M                    AS CUST_NM_M                                         " & vbNewLine _
                                    & "      FROM                                                                                    " & vbNewLine _
                                    & "           $LM_MST$..M_UNCHIN_TARIFF_SET             AS UNCHIN_SET                            " & vbNewLine _
                                    & "            LEFT OUTER JOIN                                                                   " & vbNewLine _
                                    & "          (SELECT                                                                             " & vbNewLine _
                                    & "                        UT.NRS_BR_CD                                                          " & vbNewLine _
                                    & "                       ,UT.UNCHIN_TARIFF_CD                                                   " & vbNewLine _
                                    & "                       ,UT.UNCHIN_TARIFF_REM                                                  " & vbNewLine _
                                    & "                       ,UT.STR_DATE                                                           " & vbNewLine _
                                    & "                       ,UT.TABLE_TP                                                           " & vbNewLine _
                                    & "           FROM                                                                               " & vbNewLine _
                                    & "                       $LM_MST$..M_UNCHIN_TARIFF     AS UT                                    " & vbNewLine _
                                    & "           WHERE                                                                              " & vbNewLine _
                                    & "                        UT.SYS_DEL_FLG       =  '0'                                           " & vbNewLine _
                                    & "             AND        UT.UNCHIN_TARIFF_REM IS NOT NULL                                      " & vbNewLine _
                                    & "             AND        UT.UNCHIN_TARIFF_REM <> ''                                            " & vbNewLine _
                                    & "           GROUP BY                                                                           " & vbNewLine _
                                    & "                        UT.NRS_BR_CD                                                          " & vbNewLine _
                                    & "                       ,UT.UNCHIN_TARIFF_CD                                                   " & vbNewLine _
                                    & "                       ,UT.UNCHIN_TARIFF_REM                                                  " & vbNewLine _
                                    & "                       ,UT.STR_DATE                                                           " & vbNewLine _
                                    & "                       ,UT.TABLE_TP)                  AS UNCHIN                               " & vbNewLine _
                                    & "              ON UNCHIN_SET.UNCHIN_TARIFF_CD1 = UNCHIN.UNCHIN_TARIFF_CD                       " & vbNewLine _
                                    & "             AND UNCHIN_SET.NRS_BR_CD             = UNCHIN.NRS_BR_CD                          " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                AS KBN                                   " & vbNewLine _
                                    & "        ON UNCHIN.TABLE_TP               = KBN.KBN_CD                                         " & vbNewLine _
                                    & "       AND KBN.KBN_GROUP_CD              = 'U011'                                             " & vbNewLine _
                                    & "       AND KBN.SYS_DEL_FLG               = '0'                                                " & vbNewLine _
                                    & "      LEFT OUTER JOIN                                                                         " & vbNewLine _
                                    & "                      ( SELECT                                                                " & vbNewLine _
                                    & "                            CUST.NRS_BR_CD                                                    " & vbNewLine _
                                    & "                           ,CUST.CUST_CD_L                                                    " & vbNewLine _
                                    & "                           ,CUST.CUST_NM_L                                                    " & vbNewLine _
                                    & "                           ,CUST.CUST_CD_M                                                    " & vbNewLine _
                                    & "                           ,CUST.CUST_NM_M                                                    " & vbNewLine _
                                    & "                           ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S                                " & vbNewLine _
                                    & "                           ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS                               " & vbNewLine _
                                    & "                        FROM  $LM_MST$..M_CUST               AS CUST                          " & vbNewLine _
                                    & "                        WHERE SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                    & "                        GROUP BY   CUST.NRS_BR_CD                                             " & vbNewLine _
                                    & "                                  ,CUST.CUST_CD_L                                             " & vbNewLine _
                                    & "                                  ,CUST.CUST_NM_L                                             " & vbNewLine _
                                    & "                                  ,CUST.CUST_CD_M                                             " & vbNewLine _
                                    & "                                  ,CUST.CUST_NM_M                                             " & vbNewLine _
                                    & "                        ) AS CUST                                                             " & vbNewLine _
                                    & "             ON UNCHIN_SET.NRS_BR_CD          = CUST.NRS_BR_CD                                     " & vbNewLine _
                                    & "            AND UNCHIN_SET.CUST_CD_L          = CUST.CUST_CD_L                                     " & vbNewLine _
                                    & "            AND UNCHIN_SET.CUST_CD_M          = CUST.CUST_CD_M                                     " & vbNewLine _
                                    & "      WHERE     UNCHIN_SET.SYS_DEL_FLG        = '0'                                                " & vbNewLine _
                                    & "      AND       UNCHIN_SET.UNCHIN_TARIFF_CD1 <> ''                                                 " & vbNewLine _
                                    & "      UNION                                                                                   " & vbNewLine _
                                    & "      SELECT                                                                                  " & vbNewLine _
                                    & "       UNCHIN_SET.UNCHIN_TARIFF_CD2      AS UNCHIN_TARIFF_CD                                  " & vbNewLine _
                                    & "      ,UNCHIN.UNCHIN_TARIFF_REM          AS UNCHIN_TARIFF_REM                                 " & vbNewLine _
                                    & "      ,KBN.KBN_NM1                       AS TABLE_TP_NM                                       " & vbNewLine _
                                    & "      ,UNCHIN.TABLE_TP                   AS TABLE_TP                                          " & vbNewLine _
                                    & "      ,UNCHIN.STR_DATE                   AS STR_DATE                                          " & vbNewLine _
                                    & "      ,UNCHIN_SET.NRS_BR_CD              AS NRS_BR_CD                                         " & vbNewLine _
                                    & "      ,UNCHIN_SET.CUST_CD_L              AS CUST_CD_L                                         " & vbNewLine _
                                    & "      ,CUST.CUST_NM_L                    AS CUST_NM_L                                         " & vbNewLine _
                                    & "      ,UNCHIN_SET.CUST_CD_M              AS CUST_CD_M                                         " & vbNewLine _
                                    & "      ,CUST.CUST_NM_M                    AS CUST_NM_M                                         " & vbNewLine _
                                    & "      FROM                                                                                    " & vbNewLine _
                                    & "                 $LM_MST$..M_UNCHIN_TARIFF_SET             AS UNCHIN_SET                      " & vbNewLine _
                                    & "                  LEFT OUTER JOIN                                                             " & vbNewLine _
                                    & "                (SELECT                                                                       " & vbNewLine _
                                    & "                              UT.NRS_BR_CD                                                    " & vbNewLine _
                                    & "                             ,UT.UNCHIN_TARIFF_CD                                             " & vbNewLine _
                                    & "                             ,UT.UNCHIN_TARIFF_REM                                            " & vbNewLine _
                                    & "                             ,UT.STR_DATE                                                     " & vbNewLine _
                                    & "                             ,UT.TABLE_TP                                                     " & vbNewLine _
                                    & "                 FROM                                                                         " & vbNewLine _
                                    & "                             $LM_MST$..M_UNCHIN_TARIFF     AS UT                              " & vbNewLine _
                                    & "                 WHERE                                                                        " & vbNewLine _
                                    & "                              UT.SYS_DEL_FLG       =  '0'                                     " & vbNewLine _
                                    & "                   AND        UT.UNCHIN_TARIFF_REM IS NOT NULL                                " & vbNewLine _
                                    & "                   AND        UT.UNCHIN_TARIFF_REM <> ''                                      " & vbNewLine _
                                    & "                 GROUP BY                                                                     " & vbNewLine _
                                    & "                              UT.NRS_BR_CD                                                    " & vbNewLine _
                                    & "                             ,UT.UNCHIN_TARIFF_CD                                             " & vbNewLine _
                                    & "                             ,UT.UNCHIN_TARIFF_REM                                            " & vbNewLine _
                                    & "                             ,UT.STR_DATE                                                     " & vbNewLine _
                                    & "                             ,UT.TABLE_TP)                  AS UNCHIN                         " & vbNewLine _
                                    & "                    ON UNCHIN_SET.UNCHIN_TARIFF_CD2      = UNCHIN.UNCHIN_TARIFF_CD            " & vbNewLine _
                                    & "                   AND UNCHIN_SET.NRS_BR_CD              = UNCHIN.NRS_BR_CD                   " & vbNewLine _
                                    & "            LEFT OUTER JOIN $LM_MST$..Z_KBN                AS KBN                             " & vbNewLine _
                                    & "              ON UNCHIN.TABLE_TP               = KBN.KBN_CD                                   " & vbNewLine _
                                    & "             AND KBN.KBN_GROUP_CD              = 'U011'                                       " & vbNewLine _
                                    & "             AND KBN.SYS_DEL_FLG               = '0'                                          " & vbNewLine _
                                    & "            LEFT OUTER JOIN                                                                   " & vbNewLine _
                                    & "                            ( SELECT                                                          " & vbNewLine _
                                    & "                                  CUST.NRS_BR_CD                                              " & vbNewLine _
                                    & "                                 ,CUST.CUST_CD_L                                              " & vbNewLine _
                                    & "                                 ,CUST.CUST_NM_L                                              " & vbNewLine _
                                    & "                                 ,CUST.CUST_CD_M                                              " & vbNewLine _
                                    & "                                 ,CUST.CUST_NM_M                                              " & vbNewLine _
                                    & "                                 ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S                          " & vbNewLine _
                                    & "                                 ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS                         " & vbNewLine _
                                    & "                              FROM  $LM_MST$..M_CUST               AS CUST                    " & vbNewLine _
                                    & "                              WHERE SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                    & "                              GROUP BY   CUST.NRS_BR_CD                                       " & vbNewLine _
                                    & "                                        ,CUST.CUST_CD_L                                       " & vbNewLine _
                                    & "                                        ,CUST.CUST_NM_L                                       " & vbNewLine _
                                    & "                                        ,CUST.CUST_CD_M                                       " & vbNewLine _
                                    & "                                        ,CUST.CUST_NM_M                                       " & vbNewLine _
                                    & "                              ) AS CUST                                                       " & vbNewLine _
                                    & "              ON UNCHIN_SET.NRS_BR_CD          = CUST.NRS_BR_CD                               " & vbNewLine _
                                    & "             AND UNCHIN_SET.CUST_CD_L          = CUST.CUST_CD_L                               " & vbNewLine _
                                    & "             AND UNCHIN_SET.CUST_CD_M          = CUST.CUST_CD_M                               " & vbNewLine _
                                    & "      WHERE      UNCHIN_SET.SYS_DEL_FLG        = '0'                                          " & vbNewLine _
                                    & "      AND        UNCHIN_SET.UNCHIN_TARIFF_CD2 <> ''                                           " & vbNewLine _
                                    & "      ) AS   UTS                                                                              " & vbNewLine _
                                    & "      WHERE                                                                                   " & vbNewLine
    '■要望暗号833対応 (2012/03/06)END------------------------------------------

#End Region

#Region "GROUP BY"
    '■要望暗号833対応 (2012/03/06)START------------------------------------------
    '''' <summary>
    '''' GROUP BY(運賃タリフセットマスタ)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_GROUP_BY_UNCHINSET As String = "GROUP BY                           " & vbNewLine _
    '                                           & "    UTS.UNCHIN_TARIFF_CD               " & vbNewLine _
    '                                           & "   ,UTS.UNCHIN_TARIFF_REM              " & vbNewLine _
    '                                           & "   ,UTS.TABLE_TP_NM                    " & vbNewLine _
    '                                           & "   ,UTS.TABLE_TP                       " & vbNewLine _
    '                                           & "   ,UTS.STR_DATE                       " & vbNewLine _
    '                                           & "   ,UTS.NRS_BR_CD                      " & vbNewLine _
    '                                           & "   ,UTS.DATA_TP                        " & vbNewLine _
    '                                           & "   ,UTS.CUST_CD_L                      " & vbNewLine _
    '                                           & "   ,UTS.CUST_NM_L                      " & vbNewLine _
    '                                           & "   ,UTS.CUST_CD_M                      " & vbNewLine _
    '                                           & "   ,UTS.CUST_NM_M                      " & vbNewLine
    ''' <summary>
    ''' GROUP BY(運賃タリフセットマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_UNCHINSET As String = "GROUP BY                           " & vbNewLine _
                                               & "    UTS.UNCHIN_TARIFF_CD               " & vbNewLine _
                                               & "   ,UTS.UNCHIN_TARIFF_REM              " & vbNewLine _
                                               & "   ,UTS.TABLE_TP_NM                    " & vbNewLine _
                                               & "   ,UTS.TABLE_TP                       " & vbNewLine _
                                               & "   ,UTS.STR_DATE                       " & vbNewLine _
                                               & "   ,UTS.NRS_BR_CD                      " & vbNewLine _
                                               & "   ,UTS.CUST_CD_L                      " & vbNewLine _
                                               & "   ,UTS.CUST_NM_L                      " & vbNewLine _
                                               & "   ,UTS.CUST_CD_M                      " & vbNewLine _
                                               & "   ,UTS.CUST_NM_M                      " & vbNewLine
    '■要望暗号833対応 (2012/03/06)END------------------------------------------

    ''' <summary>
    ''' 運賃セットマスタカウント用テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_TBLNM As String = "       ) AS UTCNT"

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(運賃タリフマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_UNCHIN As String = "ORDER BY                          " & vbNewLine _
                                            & "    UNCHIN.TABLE_TP                   " & vbNewLine _
                                            & "   ,UNCHIN.UNCHIN_TARIFF_CD           " & vbNewLine
    ''' <summary>
    ''' ORDER BY(運賃タリフセットマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_UNCHINSET As String = "ORDER BY                       " & vbNewLine _
                                               & "    UTS.TABLE_TP                   " & vbNewLine _
                                               & "   ,UTS.UNCHIN_TARIFF_CD           " & vbNewLine

#End Region



#Region "入力チェック"

    ''' <summary>
    ''' 荷主コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_CUST As String = "SELECT                                                                   " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD       AS NRS_BR_CD             " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L       AS CUST_CD_L             " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L       AS CUST_NM_L             " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_M       AS CUST_CD_M             " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_M       AS CUST_NM_M             " & vbNewLine _
                                          & "                        FROM                                              " & vbNewLine _
                                          & "                      ( SELECT                                            " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD                                " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L                                " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L                                " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_M                                " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_M                                " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S            " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS           " & vbNewLine _
                                          & "                        FROM  $LM_MST$..M_CUST               AS CUST      " & vbNewLine _
                                          & "                        WHERE SYS_DEL_FLG = '0'                           " & vbNewLine _
                                          & "                        GROUP BY   CUST.NRS_BR_CD                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_L                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_L                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_M                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_M                         " & vbNewLine _
                                          & "                        ) AS CUST                                         " & vbNewLine _
                                          & "WHERE                                                                     " & vbNewLine


    Private Const SQL_CUST_GROUP As String = "GROUP BY                    CUST.NRS_BR_CD                               " & vbNewLine _
                                          & "                            ,CUST.CUST_CD_L                               " & vbNewLine _
                                          & "                            ,CUST.CUST_NM_L                               " & vbNewLine _
                                          & "                            ,CUST.CUST_CD_M                               " & vbNewLine _
                                          & "                            ,CUST.CUST_NM_M                               " & vbNewLine


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
    ''' 検索件数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ230IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        '要望対応:1248 terakawa 2013.03.21 Start

        If String.IsNullOrEmpty(Me._Row.Item("CUST_CD_L").ToString()) = True Then
            Me._StrSql.Append(LMZ230DAC.SQL_SELECT_COUNT_UNCHIN)     'SQL構築(カウント用Select句:運賃タリフマスタ)
            Me._StrSql.Append(LMZ230DAC.SQL_FROM_DATA_UNCHIN)        'SQL構築(カウント用from句)
            'マイ運賃タリフ区分が"01"の場合、条件追加
            If _Row.Item("MY_UNCHIN_TARIFF_YN").ToString() = "01" Then
                Me._StrSql.Append(LMZ230DAC.SQL_FROM_TUNCHIN_TARIFF) 'SQL構築(カウント用追加from句)
            End If
            Me._StrSql.Append(LMZ230DAC.SQL_WHERE)     'SQL構築(カウント用WHERE句)
            Call Me.SetConditionMasterSQL("UNCHIN")                  '条件設定
            Call Me.SetConditionMasterMyUnchinTariffSQL()            '条件設定
            '要望対応:1248 terakawa 2013.03.21 End
        Else
            Me._StrSql.Append(LMZ230DAC.SQL_SELECT_COUNT_UNCHINSET)   'SQL構築(カウント用Select句:運賃タリフセットマスタ)
            Me._StrSql.Append(LMZ230DAC.SQL_SELECT_DATA_UNCHINSET)
            Me._StrSql.Append(LMZ230DAC.SQL_FROM_DATA_UNCHINSET)      'SQL構築(カウント用from句)
            Call Me.SetConditionMasterSQL("UTS")            '条件設定
            Me._StrSql.Append(LMZ230DAC.SQL_GROUP_BY_UNCHINSET)         'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMZ230DAC.SQL_COUNT_TBLNM)         'SQL構築(カウント用テーブル名(入れ子にしないとカウント取れない))
        End If


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ230DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ230IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Dim sql As String = String.Empty

        'SQL作成
        If String.IsNullOrEmpty(Me._Row.Item("CUST_CD_L").ToString()) = True Then
            sql = Me.SelectUnchinTariff()
        Else
            sql = Me.SelectUnchinTariffSet()
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ230DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("TABLE_TP_NM", "TABLE_TP_NM")
        map.Add("TABLE_TP", "TABLE_TP")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        'map.Add("DATA_TP", "DATA_TP")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_M", "CUST_NM_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ230OUT")

        Return ds

    End Function


    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistCustM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ230IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim sql As String = String.Empty

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        sql = Me.SelecrtCustExistChk()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ230DAC", "CheckExistCustM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_M", "CUST_NM_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ230CUST")

        Return ds

    End Function




    ''' <summary>
    ''' 運賃タリフマスタ検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariff() As String

        'SQL作成
        Me._StrSql.Append(LMZ230DAC.SQL_SELECT_DATA_UNCHIN)    'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ230DAC.SQL_FROM_DATA_UNCHIN)      'SQL構築(データ抽出用from句)
        '要望対応:1248 terakawa 2013.03.21 Start
        'マイ運賃タリフ区分が"01"の場合、条件追加
        If _Row.Item("MY_UNCHIN_TARIFF_YN").ToString() = "01" Then
            Me._StrSql.Append(LMZ230DAC.SQL_FROM_TUNCHIN_TARIFF) 'SQL構築(カウント用追加from句)
        End If
        Me._StrSql.Append(LMZ230DAC.SQL_WHERE)     'SQL構築(カウント用WHERE句)
        '要望対応:1248 terakawa 2013.03.21 End
        Call Me.SetConditionMasterSQL("UNCHIN")                '条件設定
        Call Me.SetConditionMasterMyUnchinTariffSQL()          '条件設定
        Me._StrSql.Append(LMZ230DAC.SQL_ORDER_BY_UNCHIN)       'SQL構築(データ抽出用ORDER BY句)

        Return Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    '''  運賃タリフセットマスタ検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffSet() As String

        'SQL作成
        Me._StrSql.Append(LMZ230DAC.SQL_SELECT_DATA_UNCHINSET)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ230DAC.SQL_FROM_DATA_UNCHINSET)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL("UTS")                        '条件設定
        Me._StrSql.Append(LMZ230DAC.SQL_GROUP_BY_UNCHINSET)         'SQL構築(データ抽出用GROUP BY句)
        Me._StrSql.Append(LMZ230DAC.SQL_ORDER_BY_UNCHINSET)         'SQL構築(データ抽出用ORDER BY句)

        Return Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック用SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelecrtCustExistChk() As String

        'SQL作成
        Me._StrSql.Append(LMZ230DAC.SQL_EXIST_CUST)      'SQL構築(荷主マスタ存在チェック用Select句)
        Call Me.SetCustMasterSQL()                       '条件設定
        Me._StrSql.Append(LMZ230DAC.SQL_CUST_GROUP)      'SQL構築(荷主マスタ存在チェック用Group句)
        Return Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal tblNm As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat(" ", tblNm, ".NRS_BR_CD = @NRS_BR_CD"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat("AND ", tblNm, ".CUST_CD_L = @CUST_CD_L"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

                whereStr = .Item("CUST_CD_M").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(String.Concat("AND ", tblNm, ".CUST_CD_M = @CUST_CD_M"))
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
                Else
                    Me._StrSql.Append(String.Concat("AND ", tblNm, ".CUST_CD_M = ", "'00'"))
                    Me._StrSql.Append(vbNewLine)
                End If
            End If

            whereStr = .Item("UNCHIN_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat("AND ", tblNm, ".UNCHIN_TARIFF_CD LIKE @UNCHIN_TARIFF_CD"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNCHIN_TARIFF_REM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat("AND ", tblNm, ".UNCHIN_TARIFF_REM LIKE @UNCHIN_TARIFF_REM"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_REM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("TABLE_TP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat("AND ", tblNm, ".TABLE_TP = @TABLE_TP"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TABLE_TP", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("STR_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat("AND ", tblNm, ".STR_DATE <= @STR_DATE"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterMyUnchinTariffSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            'マイ運送タリフ区分が"01"の場合、ユーザーIDを条件に設定
            whereStr = .Item("MY_UNCHIN_TARIFF_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False AndAlso _
               whereStr = "01" Then
                Me._StrSql.Append("AND T_UT.USER_CD = @USER_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", GetUserID(), DBDataType.CHAR))
            End If

        End With

    End Sub
    '要望対応:1248 terakawa 2013.03.21 End

    ''' <summary>
    ''' 荷主情報取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCustMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("CUST.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

                whereStr = .Item("CUST_CD_M").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append("AND CUST.CUST_CD_M = @CUST_CD_M")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
                Else
                    Me._StrSql.Append(String.Concat("AND CUST.CUST_CD_M = ", "00"))
                    Me._StrSql.Append(vbNewLine)
                End If
            End If


        End With

    End Sub




#End Region




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

End Class
