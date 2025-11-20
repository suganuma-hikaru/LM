' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 在庫サブ
'  プログラムID     :  LMD060    : 月末在庫履歴作成
'  作  成  者       :  [kim]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD060DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD060DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' パラメータSettingパターン
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SelectCondition As Integer
        PTN1  '検索
        PTN2  '排他チェック
        PTN3  '削除
        PTN4  '実行（削除）
        PTN5  '実行（データ取得）
        PTN6  '実行（データ登録）
        PTN7  '実行（在庫レコード有無チェック）
    End Enum

    ''' <summary>
    ''' 検索INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMD060IN"

    ''' <summary>
    ''' 検索OUTテーブル(検索結果格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMD060OUT"

    ''' <summary>
    ''' 削除INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN_DEL As String = "LMD060IN_DEL"

    ''' <summary>
    ''' 実行データ登録用テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_INOUT_JIKKOU As String = "LMD060_ZAIJITSU"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMD060DAC"

#End Region '制御用

#Region "排他チェック用 SQL"

    ''' <summary>
    ''' 削除、実行時排他チェック用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_CHECK As String = " SELECT                                                                                " & vbNewLine _
                                            & "      COUNT(MAIN.NRS_BR_CD)             AS REC_CNT                                     " & vbNewLine _
                                            & " FROM                                                                                  " & vbNewLine _
                                            & "   (                                                                                   " & vbNewLine _
                                            & "      SELECT                                                                           " & vbNewLine _
                                            & "           M07.NRS_BR_CD                                               AS NRS_BR_CD    " & vbNewLine _
                                            & "         , M07.CUST_CD_L                                               AS CUST_CD_L    " & vbNewLine _
                                            & "         , M07.CUST_CD_M                                               AS CUST_CD_M    " & vbNewLine _
                                            & "         , SUBSTRING(isNull(MAX(D05.SYS_UPD_DATE_TIME), ''), 1, 8)     AS SYS_UPD_DATE " & vbNewLine _
                                            & "         , SUBSTRING(isNull(MAX(D05.SYS_UPD_DATE_TIME), ''), 9, 9)     AS SYS_UPD_TIME " & vbNewLine _
                                            & "      FROM                                                                             " & vbNewLine _
                                            & "           $LM_MST$..M_CUST M07                                                        " & vbNewLine _
                                            & "      LEFT OUTER JOIN                                                                  " & vbNewLine _
                                            & "           $LM_TRN$..D_ZAI_TRS      D01                                                " & vbNewLine _
                                            & "      ON                                                                               " & vbNewLine _
                                            & "          D01.NRS_BR_CD    = @NRS_BR_CD                                                " & vbNewLine _
                                            & "      AND M07.CUST_CD_L    = D01.CUST_CD_L                                             " & vbNewLine _
                                            & "      AND M07.CUST_CD_M    = D01.CUST_CD_M                                             " & vbNewLine _
                                            & "      AND D01.SYS_DEL_FLG  = '0'                                                       " & vbNewLine _
                                            & "      AND D01.PORA_ZAI_NB  <> 0                                                        " & vbNewLine _
                                            & "      LEFT JOIN                                                                        " & vbNewLine _
                                            & "      (                                                                                " & vbNewLine _
                                            & "        SELECT                                                                         " & vbNewLine _
                                            & "            NRS_BR_CD                                            AS NRS_BR_CD          " & vbNewLine _
                                            & "          , ZAI_REC_NO                                      AS ZAI_REC_NO         " & vbNewLine _
                                            & "          , RIREKI_DATE                                          AS RIREKI_DATE        " & vbNewLine _
                                            & "          , SYS_UPD_DATE + SYS_UPD_TIME                     AS SYS_UPD_DATE_TIME  " & vbNewLine _
                                            & "          , SYS_DEL_FLG                                          AS SYS_DEL_FLG        " & vbNewLine _
                                            & "        FROM                                                                           " & vbNewLine _
                                            & "           $LM_TRN$..D_ZAI_ZAN_JITSU                                                   " & vbNewLine _
                                            & "        WHERE                                                                          " & vbNewLine _
                                            & "            NRS_BR_CD  = @NRS_BR_CD                                                    " & vbNewLine _
                                            & "        GROUP BY                                                                       " & vbNewLine _
                                            & "           NRS_BR_CD, RIREKI_DATE, ZAI_REC_NO , SYS_DEL_FLG  ,SYS_UPD_DATE , SYS_UPD_TIME                                       " & vbNewLine _
                                            & "       ) D05                                                                           " & vbNewLine _
                                            & "      ON                                                                               " & vbNewLine _
                                            & "           D05.NRS_BR_CD   = @NRS_BR_CD                                                " & vbNewLine _
                                            & "       AND D01.ZAI_REC_NO  = D05.ZAI_REC_NO                                            " & vbNewLine _
                                            & "       AND D05.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                                            & "   WHERE                                                                               " & vbNewLine _
                                            & "           M07.NRS_BR_CD    = @NRS_BR_CD                                               " & vbNewLine _
                                            & "       AND M07.CUST_CD_S    = '00'                                                     " & vbNewLine _
                                            & "       AND M07.CUST_CD_SS   = '00'                                                     " & vbNewLine _
                                            & "    GROUP BY                                                                           " & vbNewLine _
                                            & "    	  M07.NRS_BR_CD                                                                   " & vbNewLine _
                                            & "    	, M07.CUST_CD_L                                                                   " & vbNewLine _
                                            & "    	, M07.CUST_CD_M                                                                   " & vbNewLine _
                                            & "   ) MAIN                                                                              " & vbNewLine _
                                            & "   WHERE                                                                               " & vbNewLine _
                                            & "       MAIN.NRS_BR_CD     = @NRS_BR_CD                                                 " & vbNewLine _
                                            & "   AND MAIN.CUST_CD_L     = @CUST_CD_L                                                 " & vbNewLine _
                                            & "   AND MAIN.CUST_CD_M     = @CUST_CD_M                                                 " & vbNewLine _
                                            & "   AND MAIN.SYS_UPD_DATE  = @SYS_UPD_DATE                                              " & vbNewLine _
                                            & "   AND MAIN.SYS_UPD_TIME  = @SYS_UPD_TIME                                              " & vbNewLine


#End Region '排他チェック用 SQL

#Region "検索処理 SQL"

    '検索SQLメイン（在庫データ取得用_外部）
    Private Const SQL_SELECT_SEARCH_PRE As String = " SELECT                                                    " & vbNewLine _
                                                  & "        MAIN.CUST_CD                       AS CUST_CD      " & vbNewLine _
                                                  & "     ,  MAIN.CUST_NM                       AS CUST_NM      " & vbNewLine _
                                                  & "     ,  MAIN.CLOSE_KB_NM                   AS CLOSE_KB_NM  " & vbNewLine _
                                                  & "     ,  MAIN.TANTO_NM                      AS TANTO_NM     " & vbNewLine _
                                                  & "     ,  MAIN.RIREKI_DATE                   AS RIREKI_DATE  " & vbNewLine _
                                                  & "     ,  MAIN.CUST_CD_L                     AS CUST_CD_L    " & vbNewLine _
                                                  & "     ,  MAIN.CUST_CD_M                     AS CUST_CD_M    " & vbNewLine _
                                                  & "     ,  MAIN.CLOSE_KB                      AS CLOSE_KB     " & vbNewLine _
                                                  & "     ,  MAIN.CULC_DATE                     AS CULC_DATE    " & vbNewLine _
                                                  & "     ,  MAIN.ZAI_REC_NO                    AS ZAI_REC_NO   " & vbNewLine _
                                                  & "     ,  MAX(MAIN.SYS_UPD_DATE) OVER(PARTITION BY  MAIN.CUST_CD)      AS SYS_UPD_DATE " & vbNewLine _
                                                  & "     ,  MAX(MAIN.SYS_UPD_TIME) OVER(PARTITION BY  MAIN.CUST_CD)      AS SYS_UPD_TIME " & vbNewLine _
                                                  & "     ,  MAX(MAIN.ROW_CNT) over()           AS ROW_CNT      " & vbNewLine _
                                                  & "     ,  MAX(MAIN.REC_CNT) over()           AS REC_CNT      " & vbNewLine _
                                                  & " FROM                                                      " & vbNewLine _
                                                  & "  (                                                        " & vbNewLine


    '検索SQLメイン（在庫データ取得用_内部）
    Private Const SQL_SELECT_SEARCH As String = " SELECT                                                                                   " & vbNewLine _
                                              & "    M07.CUST_CD_L + '-' + M07.CUST_CD_M                                AS CUST_CD         " & vbNewLine _
                                              & "   ,M07.CUST_NM_L +  '　' + M07.CUST_NM_M                              AS CUST_NM         " & vbNewLine _
                                              & "   ,Z01.KBN_NM1                                                        AS CLOSE_KB_NM     " & vbNewLine _
                                              & "   ,S01.USER_NM                                                        AS TANTO_NM        " & vbNewLine _
                                              & "   ,isNull(D05.RIREKI_DATE, '')                                        AS RIREKI_DATE     " & vbNewLine _
                                              & "   ,M07.CUST_CD_L                                                      AS CUST_CD_L       " & vbNewLine _
                                              & "   ,M07.CUST_CD_M                                                      AS CUST_CD_M       " & vbNewLine _
                                              & "   ,M06.CLOSE_KB                                                       AS CLOSE_KB        " & vbNewLine _
                                              & "   ,MAX(M07.HOKAN_NIYAKU_CALCULATION)                                  AS CULC_DATE       " & vbNewLine _
                                              & "   ,MAX(D01.ZAI_REC_NO)                                                AS ZAI_REC_NO      " & vbNewLine _
                                              & "   ,SUBSTRING(isNull(MAX(D05.SYS_UPD_DATE_TIME), ''), 1, 8)            AS SYS_UPD_DATE    " & vbNewLine _
                                              & "   ,SUBSTRING(isNull(MAX(D05.SYS_UPD_DATE_TIME), ''), 9, 9)            AS SYS_UPD_TIME    " & vbNewLine _
                                              & "   ,dense_RANK() OVER(ORDER BY M07.CUST_CD_L + M07.CUST_CD_M)          AS ROW_CNT         " & vbNewLine _
                                              & "   ,COUNT(D05.RIREKI_DATE) OVER(PARTITION BY  M07.CUST_CD_L + M07.CUST_CD_M)         AS REC_CNT         " & vbNewLine


    '検索SQLの共通FROM・WHERE句
    Private Const SQL_SELECT_FROM As String = " FROM                                                                        " & vbNewLine _
                                            & "      $LM_MST$..M_CUST M07                                                   " & vbNewLine _
                                            & " LEFT OUTER JOIN                                                             " & vbNewLine _
                                            & "      $LM_TRN$..D_ZAI_TRS D01                                                " & vbNewLine _
                                            & " ON                                                                          " & vbNewLine _
                                            & "      D01.NRS_BR_CD  = @NRS_BR_CD                                            " & vbNewLine _
                                            & "  AND M07.CUST_CD_L  = D01.CUST_CD_L                                         " & vbNewLine _
                                            & "  AND M07.CUST_CD_M  = D01.CUST_CD_M                                         " & vbNewLine _
                                            & "  AND D01.PORA_ZAI_NB <> 0                                                   " & vbNewLine _
                                            & "  AND D01.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                            & " LEFT JOIN                                                                   " & vbNewLine _
                                            & " (                                                                           " & vbNewLine _
                                            & "    SELECT                                                                   " & vbNewLine _
                                            & "        NRS_BR_CD                                       AS NRS_BR_CD         " & vbNewLine _
                                            & "      , ZAI_REC_NO                                      AS ZAI_REC_NO        " & vbNewLine _
                                            & "      , RIREKI_DATE                                     AS RIREKI_DATE       " & vbNewLine _
                                            & "      , SYS_DEL_FLG                                     AS SYS_DEL_FLG       " & vbNewLine _
                                            & "      , SYS_UPD_DATE + SYS_UPD_TIME                     AS SYS_UPD_DATE_TIME " & vbNewLine _
                                            & "    FROM                                                                     " & vbNewLine _
                                            & "       $LM_TRN$..D_ZAI_ZAN_JITSU                                             " & vbNewLine _
                                            & "    WHERE                                                                    " & vbNewLine _
                                            & "        NRS_BR_CD  = @NRS_BR_CD                                              " & vbNewLine _
                                            & "    GROUP BY                                                                 " & vbNewLine _
                                            & "      NRS_BR_CD, ZAI_REC_NO, RIREKI_DATE, SYS_DEL_FLG ,SYS_UPD_DATE , SYS_UPD_TIME                                  " & vbNewLine _
                                            & "    ) D05                                                                    " & vbNewLine _
                                            & "  ON                                                                         " & vbNewLine _
                                            & "       D05.NRS_BR_CD  = @NRS_BR_CD                                           " & vbNewLine _
                                            & "   AND D01.ZAI_REC_NO = D05.ZAI_REC_NO                                       " & vbNewLine _
                                            & "   AND D05.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                   " & vbNewLine _
                                            & "      $LM_MST$..M_SEIQTO M06                                                 " & vbNewLine _
                                            & " ON                                                                          " & vbNewLine _
                                            & "      M06.NRS_BR_CD         = @NRS_BR_CD                                     " & vbNewLine _
                                            & "  AND M07.HOKAN_SEIQTO_CD   = M06.SEIQTO_CD                                  " & vbNewLine _
                                            & " LEFT JOIN                                                                   " & vbNewLine _
                                            & "      $LM_MST$..Z_KBN Z01                                                    " & vbNewLine _
                                            & " ON                                                                          " & vbNewLine _
                                            & "      M06.CLOSE_KB     = Z01.KBN_CD                                          " & vbNewLine _
                                            & "  AND Z01.KBN_GROUP_CD = 'S008'                                              " & vbNewLine _
                                            & " LEFT JOIN                                                                   " & vbNewLine _
                                            & "   ( SELECT                                                                  " & vbNewLine _
                                            & "       M30.CUST_CD_L      AS CUST_CD_L                                       " & vbNewLine _
                                            & "     , M30.CUST_CD_M      AS CUST_CD_M                                       " & vbNewLine _
                                            & "     , MIN(M30.USER_CD)   AS USER_CD                                         " & vbNewLine _
                                            & "   FROM                                                                      " & vbNewLine _
                                            & "     $LM_MST$..M_TCUST M30                                                   " & vbNewLine _
                                            & "   GROUP BY CUST_CD_L, CUST_CD_M                                             " & vbNewLine _
                                            & "   ) M30                                                                     " & vbNewLine _
                                            & " ON                                                                          " & vbNewLine _
                                            & "      M07.CUST_CD_L = M30.CUST_CD_L                                          " & vbNewLine _
                                            & "  AND M07.CUST_CD_M = M30.CUST_CD_M                                          " & vbNewLine _
                                            & " LEFT JOIN                                                                   " & vbNewLine _
                                            & "      $LM_MST$..S_USER S01                                                   " & vbNewLine _
                                            & " ON                                                                          " & vbNewLine _
                                            & "      M30.USER_CD   = S01.USER_CD                                            " & vbNewLine _
                                            & "  AND S01.NRS_BR_CD = @NRS_BR_CD                                             " & vbNewLine _
                                            & " WHERE                                                                       " & vbNewLine _
                                            & "      M07.NRS_BR_CD = @NRS_BR_CD                                             " & vbNewLine _
                                            & "  AND M07.CUST_CD_S  = '00'                                                  " & vbNewLine _
                                            & "  AND M07.CUST_CD_SS = '00'                                                  " & vbNewLine


    '検索SQLメインのORDER BY句
    Private Const SQL_SELECT_ORDERBY As String = " GROUP BY                                     " & vbNewLine _
                                               & "   M07.CUST_CD_L + '-' + M07.CUST_CD_M        " & vbNewLine _
                                               & " , M07.CUST_NM_L + '　' + M07.CUST_NM_M       " & vbNewLine _
                                               & " , Z01.KBN_NM1                                " & vbNewLine _
                                               & " , S01.USER_NM                                " & vbNewLine _
                                               & " , D05.RIREKI_DATE                            " & vbNewLine _
                                               & " , M07.CUST_CD_L                              " & vbNewLine _
                                               & " , M07.CUST_CD_M                              " & vbNewLine _
                                               & " , M06.CLOSE_KB                               " & vbNewLine _
                                               & " ) MAIN                                       " & vbNewLine _
                                               & " GROUP BY                                     " & vbNewLine _
                                               & "    MAIN.CUST_CD                              " & vbNewLine _
                                               & " ,  MAIN.CUST_NM                              " & vbNewLine _
                                               & " ,  MAIN.CLOSE_KB_NM                          " & vbNewLine _
                                               & " ,  MAIN.TANTO_NM                             " & vbNewLine _
                                               & " ,  MAIN.RIREKI_DATE                          " & vbNewLine _
                                               & " ,  MAIN.CUST_CD_L                            " & vbNewLine _
                                               & " ,  MAIN.CUST_CD_M                            " & vbNewLine _
                                               & " ,  MAIN.CLOSE_KB                             " & vbNewLine _
                                               & " ,  MAIN.CULC_DATE                            " & vbNewLine _
                                               & " ,  MAIN.ZAI_REC_NO                           " & vbNewLine _
                                               & " ,  MAIN.SYS_UPD_DATE                         " & vbNewLine _
                                               & " ,  MAIN.SYS_UPD_TIME                         " & vbNewLine _
                                               & " ,  MAIN.ROW_CNT                              " & vbNewLine _
                                               & " ,  MAIN.REC_CNT                              " & vbNewLine _
                                               & " ORDER BY                                     " & vbNewLine _
                                               & "    MAIN.CUST_CD                              " & vbNewLine _
                                               & " ,  MAIN.RIREKI_DATE  desc                    " & vbNewLine

#End Region '検索処理SQL

#Region "実行処理SQL"

    Private Const SQL_INSERT As String = " INSERT INTO $LM_TRN$..D_ZAI_ZAN_JITSU              " & vbNewLine _
                                        & "SELECT                                                                                " & vbNewLine _
                                        & "MAIN1.NRS_BR_CD               AS NRS_BR_CD                                                " & vbNewLine _
                                        & ",MAIN1.ZAI_REC_NO             AS ZAI_REC_NO                                               " & vbNewLine _
                                        & ",@RIREKI_MAKE_DATE            AS RIREKI_DATE                                              " & vbNewLine _
                                        & ",MAIN1.CUST_CD_L          AS CUST_CD_L         " & vbNewLine _
                                        & ",MAIN1.CUST_CD_M          AS CUST_CD_M         " & vbNewLine _
                                        & ",MAIN1.PORA_ZAI_NB            AS PORA_ZAI_NB                                              " & vbNewLine _
                                        & ",0                            AS ALCTD_NB                                                 " & vbNewLine _
                                        & ",MAIN1.PORA_ZAI_NB            AS ALLOC_CAN_NB                                             " & vbNewLine _
                                        & ",MAIN1.PORA_ZAI_QT            AS PORA_ZAI_QT                                              " & vbNewLine _
                                        & ",0                            AS ALCTD_QT                                                 " & vbNewLine _
                                        & ",MAIN1.PORA_ZAI_QT            AS ALLOC_CAN_QT                                             " & vbNewLine _
                                        & " , @SYS_UPD_DATE           " & vbNewLine _
                                        & " , @SYS_UPD_TIME           " & vbNewLine _
                                        & " , @SYS_UPD_PGID           " & vbNewLine _
                                        & " , @SYS_UPD_USER           " & vbNewLine _
                                        & " , @SYS_UPD_DATE           " & vbNewLine _
                                        & " , @SYS_UPD_TIME           " & vbNewLine _
                                        & " , @SYS_UPD_PGID           " & vbNewLine _
                                        & " , @SYS_UPD_USER           " & vbNewLine _
                                        & " , '0'                     " & vbNewLine _
                                        & "FROM                                                                                      " & vbNewLine _
                                        & "  (SELECT                                                                                 " & vbNewLine _
                                        & "     @NRS_BR_CD               AS NRS_BR_CD                                                " & vbNewLine _
                                        & "    ,MAIN2.ZAI_REC_NO         AS ZAI_REC_NO                                               " & vbNewLine _
                                        & "    ,MAIN2.CUST_CD_L          AS CUST_CD_L         " & vbNewLine _
                                        & "    ,MAIN2.CUST_CD_M          AS CUST_CD_M         " & vbNewLine _
                                        & "    ,SUM(PORA_ZAI_NB)         AS PORA_ZAI_NB                                              " & vbNewLine _
                                        & "    ,SUM(PORA_ZAI_QT)         AS PORA_ZAI_QT                                              " & vbNewLine _
                                        & "    FROM                                                                                  " & vbNewLine _
                                        & "      (                                                                                   " & vbNewLine _
                                        & "       --入出荷の履歴                                                                           " & vbNewLine _
                                        & "       SELECT                                                                             " & vbNewLine _
                                        & "        ZAI_REC_NO               AS ZAI_REC_NO                                            " & vbNewLine _
                                        & "       ,CUST_CD_L                AS CUST_CD_L         " & vbNewLine _
                                        & "       ,CUST_CD_M                AS CUST_CD_M         " & vbNewLine _
                                        & "       ,SUM(PORA_ZAI_NB)         AS PORA_ZAI_NB                                           " & vbNewLine _
                                        & "       ,SUM(PORA_ZAI_QT)         AS PORA_ZAI_QT                                           " & vbNewLine _
                                        & "       FROM                                                                               " & vbNewLine _
                                        & "         (--入荷データ(B_INKA_S)                                                               " & vbNewLine _
                                        & "          SELECT                                                                          " & vbNewLine _
                                        & "           ''                                                        AS OUTKA_NO_L        " & vbNewLine _
                                        & "          ,''                                                        AS OUTKA_NO_M        " & vbNewLine _
                                        & "          ,''                                                        AS OUTKA_NO_S        " & vbNewLine _
                                        & "          ,INL1.CUST_CD_L                                            AS CUST_CD_L         " & vbNewLine _
                                        & "          ,INL1.CUST_CD_M                                            AS CUST_CD_M         " & vbNewLine _
                                        & "          ,''                                                        AS CUST_NM_L         " & vbNewLine _
                                        & "          ,INS1.ZAI_REC_NO                                           AS ZAI_REC_NO        " & vbNewLine _
                                        & "          ,INM1.GOODS_CD_NRS                                         AS GOODS_CD_NRS      " & vbNewLine _
                                        & "          ,ISNULL(INS1.LOT_NO, '')                                   AS LOT_NO            " & vbNewLine _
                                        & "          ,ISNULL(INS1.SERIAL_NO, '')                                AS SERIAL_NO         " & vbNewLine _
                                        & "          ,(INS1.KONSU * MG1.PKG_NB) + INS1.HASU                     AS PORA_ZAI_NB       " & vbNewLine _
                                        & "          ,((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME      AS PORA_ZAI_QT       " & vbNewLine _
                                        & "          FROM                                                                            " & vbNewLine _
                                        & "          $LM_TRN$..B_INKA_L INL1                                                           " & vbNewLine _
                                        & "          LEFT JOIN $LM_TRN$..B_INKA_M INM1                                                 " & vbNewLine _
                                        & "          ON  INM1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "          AND INM1.NRS_BR_CD = INL1.NRS_BR_CD                                             " & vbNewLine _
                                        & "          AND INM1.INKA_NO_L = INL1.INKA_NO_L                                             " & vbNewLine _
                                        & "          LEFT JOIN $LM_TRN$..B_INKA_S INS1                                                 " & vbNewLine _
                                        & "          ON  INS1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "          AND INS1.NRS_BR_CD = INM1.NRS_BR_CD                                             " & vbNewLine _
                                        & "          AND INS1.INKA_NO_L = INM1.INKA_NO_L                                             " & vbNewLine _
                                        & "          AND INS1.INKA_NO_M = INM1.INKA_NO_M                                             " & vbNewLine _
                                        & "          LEFT JOIN $LM_MST$..M_GOODS MG1                                                   " & vbNewLine _
                                        & "          ON  MG1.NRS_BR_CD = INL1.NRS_BR_CD                                              " & vbNewLine _
                                        & "          AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS                                        " & vbNewLine _
                                        & "          WHERE                                                                           " & vbNewLine _
                                        & "              INL1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "          AND INL1.NRS_BR_CD = @NRS_BR_CD                                                 " & vbNewLine _
                                        & "          AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')                 " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "          AND INL1.CUST_CD_L = @CUST_CD_L                                                 " & vbNewLine _
                                        & "          AND INL1.CUST_CD_M = @CUST_CD_M                                                 " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "          --在庫移動分を加減算(D_IDO_TRS)                                                          " & vbNewLine _
                                        & "          --移動後                                                                           " & vbNewLine _
                                        & "          UNION ALL                                                                       " & vbNewLine _
                                        & "          SELECT                                                                          " & vbNewLine _
                                        & "           ''                                          AS OUTKA_NO_L                      " & vbNewLine _
                                        & "          ,''                                          AS OUTKA_NO_M                      " & vbNewLine _
                                        & "          ,''                                          AS OUTKA_NO_S                      " & vbNewLine _
                                        & "          ,ZAI3.CUST_CD_L                              AS CUST_CD_L                       " & vbNewLine _
                                        & "          ,ZAI3.CUST_CD_M                              AS CUST_CD_M                       " & vbNewLine _
                                        & "          ,''                                          AS CUST_NM_L                       " & vbNewLine _
                                        & "          ,IDO1.N_ZAI_REC_NO                           AS ZAI_REC_NO                      " & vbNewLine _
                                        & "          ,ZAI3.GOODS_CD_NRS                           AS GOODS_CD_NRS                    " & vbNewLine _
                                        & "          ,ISNULL(ZAI3.LOT_NO, '')                     AS LOT_NO                          " & vbNewLine _
                                        & "          ,ISNULL(ZAI4.SERIAL_NO, '')                  AS SERIAL_NO                       " & vbNewLine _
                                        & "          ,IDO1.N_PORA_ZAI_NB                          AS PORA_ZAI_NB                     " & vbNewLine _
                                        & "          ,IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME           AS PORA_ZAI_QT                     " & vbNewLine _
                                        & "          FROM                                                                            " & vbNewLine _
                                        & "          $LM_TRN$..D_IDO_TRS IDO1                                                          " & vbNewLine _
                                        & "          LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI3                                                " & vbNewLine _
                                        & "          ON  ZAI3.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "          AND ZAI3.NRS_BR_CD = IDO1.NRS_BR_CD                                             " & vbNewLine _
                                        & "          AND ZAI3.ZAI_REC_NO = IDO1.O_ZAI_REC_NO                                         " & vbNewLine _
                                        & "          LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI4                                                " & vbNewLine _
                                        & "          ON  ZAI4.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "          AND ZAI4.NRS_BR_CD = IDO1.NRS_BR_CD                                             " & vbNewLine _
                                        & "          AND ZAI4.ZAI_REC_NO = IDO1.N_ZAI_REC_NO                                         " & vbNewLine _
                                        & "          WHERE                                                                           " & vbNewLine _
                                        & "              IDO1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "          AND IDO1.NRS_BR_CD = @NRS_BR_CD                                                 " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "          AND ZAI3.CUST_CD_L = @CUST_CD_L                                                 " & vbNewLine _
                                        & "          AND ZAI3.CUST_CD_M = @CUST_CD_M                                                 " & vbNewLine _
                                        & "          --移動前                                                                           " & vbNewLine _
                                        & "          UNION ALL                                                                       " & vbNewLine _
                                        & "          SELECT                                                                          " & vbNewLine _
                                        & "           ''                                          AS OUTKA_NO_L                      " & vbNewLine _
                                        & "          ,''                                          AS OUTKA_NO_M                      " & vbNewLine _
                                        & "          ,''                                          AS OUTKA_NO_S                      " & vbNewLine _
                                        & "          ,ZAI5.CUST_CD_L                              AS CUST_CD_L                       " & vbNewLine _
                                        & "          ,ZAI5.CUST_CD_M                              AS CUST_CD_M                       " & vbNewLine _
                                        & "          ,''                                          AS CUST_NM_L                       " & vbNewLine _
                                        & "          ,IDO2.O_ZAI_REC_NO                           AS ZAI_REC_NO                      " & vbNewLine _
                                        & "          ,ZAI5.GOODS_CD_NRS                           AS GOODS_CD_NRS                    " & vbNewLine _
                                        & "          ,ISNULL(ZAI5.LOT_NO, '')                     AS LOT_NO                          " & vbNewLine _
                                        & "          ,ISNULL(ZAI5.SERIAL_NO, '')                  AS SERIAL_NO                       " & vbNewLine _
                                        & "          ,IDO2.N_PORA_ZAI_NB * -1                     AS PORA_ZAI_NB                     " & vbNewLine _
                                        & "          ,IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1      AS PORA_ZAI_QT                     " & vbNewLine _
                                        & "          FROM                                                                            " & vbNewLine _
                                        & "          $LM_TRN$..D_IDO_TRS IDO2                                                          " & vbNewLine _
                                        & "          LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI5                                                " & vbNewLine _
                                        & "          ON  ZAI5.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "          AND ZAI5.NRS_BR_CD = IDO2.NRS_BR_CD                                             " & vbNewLine _
                                        & "          AND ZAI5.ZAI_REC_NO = IDO2.O_ZAI_REC_NO                                         " & vbNewLine _
                                        & "          WHERE                                                                           " & vbNewLine _
                                        & "              IDO2.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                        & "          AND IDO2.NRS_BR_CD = @NRS_BR_CD                                                 " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "          AND ZAI5.CUST_CD_L = @CUST_CD_L                                                 " & vbNewLine _
                                        & "          AND ZAI5.CUST_CD_M = @CUST_CD_M                                                 " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "          --出荷データ(C_OUTKA_S)                                                              " & vbNewLine _
                                        & "          UNION ALL                                                                       " & vbNewLine _
                                        & "          SELECT DISTINCT                                                                 " & vbNewLine _
                                        & "           OUTKA_NO_L                                                                     " & vbNewLine _
                                        & "          ,OUTKA_NO_M                                                                     " & vbNewLine _
                                        & "          ,OUTKA_NO_S                                                                     " & vbNewLine _
                                        & "          ,CUST_CD_L                                                                      " & vbNewLine _
                                        & "          ,CUST_CD_M                                                                      " & vbNewLine _
                                        & "          ,CUST_NM_L                                                                      " & vbNewLine _
                                        & "          ,ZAI_REC_NO                                                                     " & vbNewLine _
                                        & "          ,GOODS_CD_NRS                                                                   " & vbNewLine _
                                        & "          ,LOT_NO                                                                         " & vbNewLine _
                                        & "          ,SERIAL_NO                                                                      " & vbNewLine _
                                        & "          ,PORA_ZAI_NB                                                                    " & vbNewLine _
                                        & "          ,PORA_ZAI_QT                                                                    " & vbNewLine _
                                        & "          FROM                                                                            " & vbNewLine _
                                        & "            (SELECT                                                                       " & vbNewLine _
                                        & "              OUTS.OUTKA_NO_L                   AS OUTKA_NO_L                             " & vbNewLine _
                                        & "            ,OUTS.OUTKA_NO_M                   AS OUTKA_NO_M                              " & vbNewLine _
                                        & "            ,OUTS.OUTKA_NO_S                   AS OUTKA_NO_S                              " & vbNewLine _
                                        & "             ,OUTL.CUST_CD_L                    AS CUST_CD_L                              " & vbNewLine _
                                        & "             ,OUTL.CUST_CD_M                    AS CUST_CD_M                              " & vbNewLine _
                                        & "             ,''                                AS CUST_NM_L                              " & vbNewLine _
                                        & "             ,OUTS.ZAI_REC_NO                   AS ZAI_REC_NO                             " & vbNewLine _
                                        & "             ,OUTM.GOODS_CD_NRS                 AS GOODS_CD_NRS                           " & vbNewLine _
                                        & "             ,ISNULL(OUTS.LOT_NO, '')           AS LOT_NO                                 " & vbNewLine _
                                        & "             ,ISNULL(OUTS.SERIAL_NO, '')        AS SERIAL_NO                              " & vbNewLine _
                                        & "             ,OUTS.ALCTD_NB * -1                AS PORA_ZAI_NB                            " & vbNewLine _
                                        & "             ,OUTS.ALCTD_QT * -1                AS PORA_ZAI_QT                            " & vbNewLine _
                                        & "             FROM                                                                         " & vbNewLine _
                                        & "             $LM_TRN$..C_OUTKA_L OUTL                                                       " & vbNewLine _
                                        & "             LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                             " & vbNewLine _
                                        & "             ON  OUTM.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                        & "             AND OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                        & "             AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                        & "             LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                             " & vbNewLine _
                                        & "             ON  OUTS.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                        & "             AND OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                        & "             AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                        & "             AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                        " & vbNewLine _
                                        & "             WHERE                                                                        " & vbNewLine _
                                        & "                 OUTL.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                        & "             AND OUTM.ALCTD_KB <> '04'                                                    " & vbNewLine _
                                        & "             AND OUTL.OUTKA_STATE_KB !< '60'                                              " & vbNewLine _
                                        & "             AND OUTL.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "          AND OUTL.CUST_CD_L = @CUST_CD_L                                                 " & vbNewLine _
                                        & "          AND OUTL.CUST_CD_M = @CUST_CD_M                                                 " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "            ) BASE3                                                                       " & vbNewLine _
                                        & "         ) BASE4                                                                          " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "         WHERE                                                                            " & vbNewLine _
                                        & "             CUST_CD_L = @CUST_CD_L                                                       " & vbNewLine _
                                        & "         AND CUST_CD_M = @CUST_CD_M                                                       " & vbNewLine _
                                        & "                                                                                          " & vbNewLine _
                                        & "         GROUP BY                                                                         " & vbNewLine _
                                        & "           ZAI_REC_NO                                                                      " & vbNewLine _
                                        & "          ,CUST_CD_L                                                                   " & vbNewLine _
                                        & "          ,CUST_CD_M                                                                   " & vbNewLine _
                                        & "      ) MAIN2                                                                             " & vbNewLine _
                                        & "      GROUP BY                                                                            " & vbNewLine _
                                        & "          MAIN2.ZAI_REC_NO                                                                   " & vbNewLine _
                                        & "         ,MAIN2.CUST_CD_L                                                                   " & vbNewLine _
                                        & "         ,MAIN2.CUST_CD_M                                                                   " & vbNewLine _
                                        & "  ) MAIN1                                                                                 " & vbNewLine _
                                        & "WHERE                                                                                     " & vbNewLine _
                                        & "	  PORA_ZAI_NB <> 0                                                                         " & vbNewLine _
                                        & "ORDER BY                                                                                  " & vbNewLine _
                                        & "   NRS_BR_CD                                                                                " & vbNewLine _
                                        & "  ,ZAI_REC_NO                                                                               " & vbNewLine

#Region "大改修"

    '#Region "登録データ取得"

    '    '実行処理登録データ取得SQL
    '    Private Const SQL_SELECT_JIKKOU As String = "  SELECT                                                                                   " & vbNewLine _
    '                                            & "      MAIN.RIREKI_DATE            AS RIREKI_DATE                                             " & vbNewLine _
    '                                            & "    , MAIN.NRS_BR_CD              AS NRS_BR_CD                                               " & vbNewLine _
    '                                            & "    , MAIN.ZAI_REC_NO             AS ZAI_REC_NO                                              " & vbNewLine _
    '                                            & "    , SUM(MAIN.PORA_ZAI_NB)       AS PORA_ZAI_NB                                             " & vbNewLine _
    '                                            & "    , SUM(MAIN.ALCTD_NB)          AS ALCTD_NB                                                " & vbNewLine _
    '                                            & "    , SUM(MAIN.ALLOC_CAN_NB)      AS ALLOC_CAN_NB                                            " & vbNewLine _
    '                                            & "    , SUM(MAIN.PORA_ZAI_QT)       AS PORA_ZAI_QT                                             " & vbNewLine _
    '                                            & "    , SUM(MAIN.ALCTD_QT)          AS ALCTD_QT                                                " & vbNewLine _
    '                                            & "    , SUM(MAIN.ALLOC_CAN_QT)      AS ALLOC_CAN_QT                                            " & vbNewLine _
    '                                            & "  FROM                                                                                       " & vbNewLine _
    '                                            & "  (                                                                                          " & vbNewLine _
    '                                            & "   SELECT                                                                                    " & vbNewLine _
    '                                            & "       @RIREKI_DATE                                                     AS RIREKI_DATE       " & vbNewLine _
    '                                            & "     , INKA_S.NRS_BR_CD                                                 AS NRS_BR_CD         " & vbNewLine _
    '                                            & "     , INKA_S.ZAI_REC_NO                                                AS ZAI_REC_NO        " & vbNewLine _
    '                                            & "     , SUM(INKA_S.KONSU) * GOODS.PKG_NB +  SUM(INKA_S.HASU)             AS PORA_ZAI_NB       " & vbNewLine _
    '                                            & "     , 0                                                                AS ALCTD_NB          " & vbNewLine _
    '                                            & "     , SUM(INKA_S.KONSU) * GOODS.PKG_NB +  SUM(INKA_S.HASU)             AS ALLOC_CAN_NB      " & vbNewLine _
    '                                            & "     , (SUM(INKA_S.KONSU) * GOODS.PKG_NB +  SUM(INKA_S.HASU)) *  INKA_S.IRIME AS PORA_ZAI_QT " & vbNewLine _
    '                                            & "     , 0                                                                AS ALCTD_QT          " & vbNewLine _
    '                                            & "     , (SUM(INKA_S.KONSU) * GOODS.PKG_NB +  SUM(INKA_S.HASU)) *  INKA_S.IRIME AS ALLOC_CAN_QT " & vbNewLine _
    '                                            & "   FROM                                                                                      " & vbNewLine _
    '                                            & "      $LM_TRN$..B_INKA_S INKA_S                                                              " & vbNewLine _
    '                                            & "      LEFT JOIN                                                                              " & vbNewLine _
    '                                            & "          $LM_TRN$..B_INKA_M INKA_M                                                          " & vbNewLine _
    '                                            & "      ON                                                                                     " & vbNewLine _
    '                                            & "              INKA_M.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
    '                                            & "          AND INKA_M.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    '                                            & "          AND INKA_S.NRS_BR_CD = INKA_M.NRS_BR_CD                                            " & vbNewLine _
    '                                            & "          AND INKA_S.INKA_NO_L = INKA_M.INKA_NO_L                                            " & vbNewLine _
    '                                            & "          AND INKA_S.INKA_NO_M = INKA_M.INKA_NO_M                                            " & vbNewLine _
    '                                            & "      LEFT JOIN                                                                              " & vbNewLine _
    '                                            & "          $LM_TRN$..B_INKA_L INKA_L                                                          " & vbNewLine _
    '                                            & "      ON                                                                                     " & vbNewLine _
    '                                            & "              INKA_L.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
    '                                            & "          AND INKA_L.SYS_DEL_FLG= '0'                                                        " & vbNewLine _
    '                                            & "          AND INKA_L.NRS_BR_CD = INKA_M.NRS_BR_CD                                            " & vbNewLine _
    '                                            & "          AND INKA_L.INKA_NO_L = INKA_M.INKA_NO_L                                            " & vbNewLine _
    '                                            & "      LEFT JOIN                                                                              " & vbNewLine _
    '                                            & "          $LM_MST$..M_GOODS   GOODS                                                          " & vbNewLine _
    '                                            & "      ON                                                                                     " & vbNewLine _
    '                                            & "              GOODS.NRS_BR_CD     = @NRS_BR_CD                                               " & vbNewLine _
    '                                            & "          AND INKA_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                       " & vbNewLine _
    '                                            & "   WHERE                                                                                     " & vbNewLine _
    '                                            & "           INKA_S.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
    '                                            & "       AND INKA_S.ZAI_REC_NO                                                                 " & vbNewLine _
    '                                            & "       IN  (                                                                                 " & vbNewLine _
    '                                            & "            SELECT                                                                           " & vbNewLine _
    '                                            & "                ZAITRS1.ZAI_REC_NO              AS ZAI_REC_NO                                " & vbNewLine _
    '                                            & "            FROM                                                                             " & vbNewLine _
    '                                            & "                $LM_TRN$..D_ZAI_TRS   ZAITRS1                                                " & vbNewLine _
    '                                            & "           WHERE                                                                             " & vbNewLine _
    '                                            & "                   ZAITRS1.NRS_BR_CD   =  @NRS_BR_CD                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.CUST_CD_L   =  @CUST_CD_L                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.CUST_CD_M   =  @CUST_CD_M                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
    '                                            & "           )                                                                                 " & vbNewLine _
    '                                            & "       AND INKA_L.INKA_DATE  <= @RIREKI_MAKE_DATE                                            " & vbNewLine _
    '                                            & "       AND '50' <= INKA_L.INKA_STATE_KB                                                      " & vbNewLine _
    '                                            & "       AND INKA_S.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
    '                                            & "   GROUP BY                                                                                  " & vbNewLine _
    '                                            & "       INKA_S.NRS_BR_CD,INKA_S.ZAI_REC_NO,INKA_S.IRIME,GOODS.PKG_NB                          " & vbNewLine _
    '                                            & "  UNION ALL                                                                                  " & vbNewLine _
    '                                            & "   SELECT                                                                                    " & vbNewLine _
    '                                            & "         @RIREKI_DATE                 AS RIREKI_DATE                                         " & vbNewLine _
    '                                            & "       , OUTKA_S.NRS_BR_CD                AS NRS_BR_CD                                       " & vbNewLine _
    '                                            & "       , OUTKA_S.ZAI_REC_NO               AS ZAI_REC_NO                                      " & vbNewLine _
    '                                            & "       , SUM(OUTKA_S.ALCTD_NB) * -1       AS PORA_ZAI_NB                                     " & vbNewLine _
    '                                            & "       , 0                            AS ALCTD_NB                                            " & vbNewLine _
    '                                            & "       , SUM(OUTKA_S.ALCTD_NB) * -1       AS ALLOC_CAN_NB                                    " & vbNewLine _
    '                                            & "       , SUM(OUTKA_S.ALCTD_QT) * -1       AS PORA_ZAI_QT                                     " & vbNewLine _
    '                                            & "       , 0                            AS ALCTD_QT                                            " & vbNewLine _
    '                                            & "       , SUM(OUTKA_S.ALCTD_QT) * -1       AS ALLOC_CAN_QT                                    " & vbNewLine _
    '                                            & "   FROM                                                                                      " & vbNewLine _
    '                                            & "       $LM_TRN$..C_OUTKA_S   OUTKA_S                                                         " & vbNewLine _
    '                                            & "       LEFT JOIN                                                                             " & vbNewLine _
    '                                            & "           $LM_TRN$..C_OUTKA_M   OUTKA_M                                                     " & vbNewLine _
    '                                            & "       ON                                                                                    " & vbNewLine _
    '                                            & "               OUTKA_M.NRS_BR_CD   = @NRS_BR_CD                                              " & vbNewLine _
    '                                            & "           AND OUTKA_S.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                            & "           AND OUTKA_S.NRS_BR_CD   = OUTKA_M.NRS_BR_CD                                       " & vbNewLine _
    '                                            & "           AND OUTKA_S.OUTKA_NO_L  = OUTKA_M.OUTKA_NO_L                                      " & vbNewLine _
    '                                            & "           AND OUTKA_S.OUTKA_NO_M  = OUTKA_M.OUTKA_NO_M                                      " & vbNewLine _
    '                                            & "       LEFT JOIN                                                                             " & vbNewLine _
    '                                            & "           $LM_TRN$..C_OUTKA_L   OUTKA_L                                                     " & vbNewLine _
    '                                            & "       ON                                                                                    " & vbNewLine _
    '                                            & "               OUTKA_L.NRS_BR_CD   = @NRS_BR_CD                                              " & vbNewLine _
    '                                            & "           AND OUTKA_L.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                            & "           AND OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                                         " & vbNewLine _
    '                                            & "           AND OUTKA_L.OUTKA_NO_L  = OUTKA_M.OUTKA_NO_L                                      " & vbNewLine _
    '                                            & "   WHERE                                                                                     " & vbNewLine _
    '                                            & "           OUTKA_S.NRS_BR_CD       =  @NRS_BR_CD                                             " & vbNewLine _
    '                                            & "       AND OUTKA_S.ZAI_REC_NO                                                                " & vbNewLine _
    '                                            & "          IN (                                                                               " & vbNewLine _
    '                                            & "            SELECT                                                                           " & vbNewLine _
    '                                            & "                ZAITRS1.ZAI_REC_NO              AS ZAI_REC_NO                                " & vbNewLine _
    '                                            & "            FROM                                                                             " & vbNewLine _
    '                                            & "                $LM_TRN$..D_ZAI_TRS   ZAITRS1                                                " & vbNewLine _
    '                                            & "           LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKA_S1                                            " & vbNewLine _
    '                                            & "           ON                                                                                " & vbNewLine _
    '                                            & "                    OUTKA_S1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_S1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "                AND ZAITRS1.NRS_BR_CD = OUTKA_S1.NRS_BR_CD                                   " & vbNewLine _
    '                                            & "                AND ZAITRS1.ZAI_REC_NO = OUTKA_S1.ZAI_REC_NO                                 " & vbNewLine _
    '                                            & "            LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKA_M1                                           " & vbNewLine _
    '                                            & "            ON                                                                               " & vbNewLine _
    '                                            & "                    OUTKA_M1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_M1.ALCTD_KB <> '04'                                                " & vbNewLine _
    '                                            & "                AND OUTKA_M1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "                AND OUTKA_M1.NRS_BR_CD = OUTKA_S1.NRS_BR_CD                                  " & vbNewLine _
    '                                            & "                AND OUTKA_M1.OUTKA_NO_L = OUTKA_S1.OUTKA_NO_L                                " & vbNewLine _
    '                                            & "                AND OUTKA_M1.OUTKA_NO_M = OUTKA_S1.OUTKA_NO_M                                " & vbNewLine _
    '                                            & "            LEFT JOIN $LM_TRN$..C_OUTKA_L OUTKA_L1                                           " & vbNewLine _
    '                                            & "            ON                                                                               " & vbNewLine _
    '                                            & "                    OUTKA_L1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_L1.CUST_CD_L   =  @CUST_CD_L                                       " & vbNewLine _
    '                                            & "                AND OUTKA_L1.CUST_CD_M   =  @CUST_CD_M                                       " & vbNewLine _
    '                                            & "                AND OUTKA_L1.OUTKA_PLAN_DATE <= @RIREKI_MAKE_DATE                            " & vbNewLine _
    '                                            & "                AND '60' <= OUTKA_L1.OUTKA_STATE_KB                                          " & vbNewLine _
    '                                            & "                AND OUTKA_L1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "                AND OUTKA_L1.NRS_BR_CD = OUTKA_M1.NRS_BR_CD                                  " & vbNewLine _
    '                                            & "                AND OUTKA_L1.OUTKA_NO_L = OUTKA_M1.OUTKA_NO_L                                " & vbNewLine _
    '                                            & "           WHERE                                                                             " & vbNewLine _
    '                                            & "                   ZAITRS1.NRS_BR_CD   =  @NRS_BR_CD                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.CUST_CD_L   =  @CUST_CD_L                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.CUST_CD_M   =  @CUST_CD_M                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
    '                                            & "           )                                                                                 " & vbNewLine _
    '                                            & "       AND OUTKA_L.OUTKA_PLAN_DATE <= @RIREKI_MAKE_DATE                                      " & vbNewLine _
    '                                            & "       AND '60' <= OUTKA_L.OUTKA_STATE_KB                                                    " & vbNewLine _
    '                                            & "       AND OUTKA_M.ALCTD_KB <> '04'                                                          " & vbNewLine _
    '                                            & "       AND OUTKA_S.SYS_DEL_FLG     = '0'                                                     " & vbNewLine _
    '                                            & "   GROUP BY                                                                                  " & vbNewLine _
    '                                            & "       OUTKA_S.NRS_BR_CD , OUTKA_S.ZAI_REC_NO                                                " & vbNewLine _
    '                                            & "  UNION ALL                                                                                  " & vbNewLine _
    '                                            & "   SELECT                                                                                    " & vbNewLine _
    '                                            & "         @RIREKI_DATE                                 AS RIREKI_DATE                         " & vbNewLine _
    '                                            & "       , IDO_TRS.NRS_BR_CD                                AS NRS_BR_CD                       " & vbNewLine _
    '                                            & "       , IDO_TRS.O_ZAI_REC_NO                             AS ZAI_REC_NO                      " & vbNewLine _
    '                                            & "       , SUM(IDO_TRS.N_PORA_ZAI_NB) * -1                  AS PORA_ZAI_NB                     " & vbNewLine _
    '                                            & "       , 0                                            AS ALCTD_NB                            " & vbNewLine _
    '                                            & "       , SUM(IDO_TRS.N_PORA_ZAI_NB) * -1                  AS ALLOC_CAN_NB                    " & vbNewLine _
    '                                            & "       , SUM(IDO_TRS.N_PORA_ZAI_NB) * ZAI_TRS.IRIME * -1      AS PORA_ZAI_QT                 " & vbNewLine _
    '                                            & "       , 0                                            AS ALCTD_QT                            " & vbNewLine _
    '                                            & "       , SUM(IDO_TRS.N_PORA_ZAI_NB) * ZAI_TRS.IRIME * -1      AS ALLOC_CAN_QT                " & vbNewLine _
    '                                            & "   FROM                                                                                      " & vbNewLine _
    '                                            & "       $LM_TRN$..D_IDO_TRS   IDO_TRS                                                         " & vbNewLine _
    '                                            & "       LEFT JOIN                                                                             " & vbNewLine _
    '                                            & "           $LM_TRN$..D_ZAI_TRS   ZAI_TRS                                                     " & vbNewLine _
    '                                            & "       ON                                                                                    " & vbNewLine _
    '                                            & "               ZAI_TRS.NRS_BR_CD = @NRS_BR_CD                                                " & vbNewLine _
    '                                            & "           AND ZAI_TRS.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                            & "           AND IDO_TRS.O_ZAI_REC_NO = ZAI_TRS.ZAI_REC_NO                                     " & vbNewLine _
    '                                            & "   WHERE                                                                                     " & vbNewLine _
    '                                            & "           IDO_TRS.NRS_BR_CD    =  @NRS_BR_CD                                                " & vbNewLine _
    '                                            & "       AND IDO_TRS.O_ZAI_REC_NO                                                              " & vbNewLine _
    '                                            & "       IN (                                                                                  " & vbNewLine _
    '                                            & "            SELECT                                                                           " & vbNewLine _
    '                                            & "                ZAITRS1.ZAI_REC_NO              AS ZAI_REC_NO                                " & vbNewLine _
    '                                            & "            FROM                                                                             " & vbNewLine _
    '                                            & "                $LM_TRN$..D_ZAI_TRS   ZAITRS1                                                " & vbNewLine _
    '                                            & "           LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKA_S1                                            " & vbNewLine _
    '                                            & "           ON                                                                                " & vbNewLine _
    '                                            & "                    OUTKA_S1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_S1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "                AND ZAITRS1.NRS_BR_CD = OUTKA_S1.NRS_BR_CD                                   " & vbNewLine _
    '                                            & "                AND ZAITRS1.ZAI_REC_NO = OUTKA_S1.ZAI_REC_NO                                 " & vbNewLine _
    '                                            & "            LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKA_M1                                           " & vbNewLine _
    '                                            & "            ON                                                                               " & vbNewLine _
    '                                            & "                    OUTKA_M1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_M1.ALCTD_KB <> '04'                                                " & vbNewLine _
    '                                            & "                AND OUTKA_M1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "                AND OUTKA_M1.NRS_BR_CD = OUTKA_S1.NRS_BR_CD                                  " & vbNewLine _
    '                                            & "                AND OUTKA_M1.OUTKA_NO_L = OUTKA_S1.OUTKA_NO_L                                " & vbNewLine _
    '                                            & "                AND OUTKA_M1.OUTKA_NO_M = OUTKA_S1.OUTKA_NO_M                                " & vbNewLine _
    '                                            & "            LEFT JOIN $LM_TRN$..C_OUTKA_L OUTKA_L1                                           " & vbNewLine _
    '                                            & "            ON                                                                               " & vbNewLine _
    '                                            & "                    OUTKA_L1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_L1.CUST_CD_L   =  @CUST_CD_L                                       " & vbNewLine _
    '                                            & "                AND OUTKA_L1.CUST_CD_M   =  @CUST_CD_M                                       " & vbNewLine _
    '                                            & "                AND OUTKA_L1.OUTKA_PLAN_DATE <= @RIREKI_MAKE_DATE                            " & vbNewLine _
    '                                            & "                AND '60' <= OUTKA_L1.OUTKA_STATE_KB                                          " & vbNewLine _
    '                                            & "                AND OUTKA_L1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "                AND OUTKA_L1.NRS_BR_CD = OUTKA_M1.NRS_BR_CD                                  " & vbNewLine _
    '                                            & "                AND OUTKA_L1.OUTKA_NO_L = OUTKA_M1.OUTKA_NO_L                                " & vbNewLine _
    '                                            & "           WHERE                                                                             " & vbNewLine _
    '                                            & "                   ZAITRS1.NRS_BR_CD   =  @NRS_BR_CD                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.CUST_CD_L   =  @CUST_CD_L                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.CUST_CD_M   =  @CUST_CD_M                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
    '                                            & "           )                                                                                 " & vbNewLine _
    '                                            & "       AND IDO_TRS.IDO_DATE     <= @RIREKI_MAKE_DATE                                         " & vbNewLine _
    '                                            & "       AND IDO_TRS.SYS_DEL_FLG  = '0'                                                        " & vbNewLine _
    '                                            & "   GROUP BY                                                                                  " & vbNewLine _
    '                                            & "       IDO_TRS.NRS_BR_CD , IDO_TRS.O_ZAI_REC_NO, ZAI_TRS.IRIME                               " & vbNewLine _
    '                                            & "  UNION ALL                                                                                  " & vbNewLine _
    '                                            & "   SELECT                                                                                    " & vbNewLine _
    '                                            & "         @RIREKI_DATE                                 AS RIREKI_DATE                         " & vbNewLine _
    '                                            & "       , IDO_TRS.NRS_BR_CD                                AS NRS_BR_CD                       " & vbNewLine _
    '                                            & "       , IDO_TRS.N_ZAI_REC_NO                             AS ZAI_REC_NO                      " & vbNewLine _
    '                                            & "       , SUM(IDO_TRS.N_PORA_ZAI_NB)                       AS PORA_ZAI_NB                     " & vbNewLine _
    '                                            & "       , 0                                            AS ALCTD_NB                            " & vbNewLine _
    '                                            & "       , SUM(IDO_TRS.N_PORA_ZAI_NB)                       AS ALLOC_CAN_NB                    " & vbNewLine _
    '                                            & "       , SUM(IDO_TRS.N_PORA_ZAI_NB) * ZAI_TRS.IRIME           AS PORA_ZAI_QT                 " & vbNewLine _
    '                                            & "       , 0                                            AS ALCTD_QT                            " & vbNewLine _
    '                                            & "       , SUM(IDO_TRS.N_PORA_ZAI_NB) * ZAI_TRS.IRIME           AS ALLOC_CAN_QT                " & vbNewLine _
    '                                            & "   FROM                                                                                      " & vbNewLine _
    '                                            & "       $LM_TRN$..D_IDO_TRS   IDO_TRS                                                         " & vbNewLine _
    '                                            & "       LEFT JOIN                                                                             " & vbNewLine _
    '                                            & "           $LM_TRN$..D_ZAI_TRS   ZAI_TRS                                                     " & vbNewLine _
    '                                            & "       ON                                                                                    " & vbNewLine _
    '                                            & "               ZAI_TRS.NRS_BR_CD  = @NRS_BR_CD                                               " & vbNewLine _
    '                                            & "           AND ZAI_TRS.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                            & "           AND IDO_TRS.N_ZAI_REC_NO = ZAI_TRS.ZAI_REC_NO                                     " & vbNewLine _
    '                                            & "   WHERE                                                                                     " & vbNewLine _
    '                                            & "               IDO_TRS.NRS_BR_CD    = @NRS_BR_CD                                             " & vbNewLine _
    '                                            & "           AND IDO_TRS.N_ZAI_REC_NO                                                          " & vbNewLine _
    '                                            & "           IN (                                                                              " & vbNewLine _
    '                                            & "            SELECT                                                                           " & vbNewLine _
    '                                            & "                ZAITRS1.ZAI_REC_NO              AS ZAI_REC_NO                                " & vbNewLine _
    '                                            & "            FROM                                                                             " & vbNewLine _
    '                                            & "                $LM_TRN$..D_ZAI_TRS   ZAITRS1                                                " & vbNewLine _
    '                                            & "            LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKA_S1                                           " & vbNewLine _
    '                                            & "            ON                                                                               " & vbNewLine _
    '                                            & "                    OUTKA_S1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_S1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "               AND ZAITRS1.NRS_BR_CD = OUTKA_S1.NRS_BR_CD                                    " & vbNewLine _
    '                                            & "               AND ZAITRS1.ZAI_REC_NO = OUTKA_S1.ZAI_REC_NO                                  " & vbNewLine _
    '                                            & "            LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKA_M1                                           " & vbNewLine _
    '                                            & "            ON                                                                               " & vbNewLine _
    '                                            & "                    OUTKA_M1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_M1.ALCTD_KB <> '04'                                                " & vbNewLine _
    '                                            & "                AND OUTKA_M1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "                AND OUTKA_M1.NRS_BR_CD = OUTKA_S1.NRS_BR_CD                                  " & vbNewLine _
    '                                            & "                AND OUTKA_M1.OUTKA_NO_L = OUTKA_S1.OUTKA_NO_L                                " & vbNewLine _
    '                                            & "                AND OUTKA_M1.OUTKA_NO_M = OUTKA_S1.OUTKA_NO_M                                " & vbNewLine _
    '                                            & "            LEFT JOIN $LM_TRN$..C_OUTKA_L OUTKA_L1                                           " & vbNewLine _
    '                                            & "            ON                                                                               " & vbNewLine _
    '                                            & "                    OUTKA_L1.NRS_BR_CD =@NRS_BR_CD                                           " & vbNewLine _
    '                                            & "                AND OUTKA_L1.CUST_CD_L   =  @CUST_CD_L                                       " & vbNewLine _
    '                                            & "                AND OUTKA_L1.CUST_CD_M   =  @CUST_CD_M                                       " & vbNewLine _
    '                                            & "                AND OUTKA_L1.OUTKA_PLAN_DATE <= @RIREKI_MAKE_DATE                            " & vbNewLine _
    '                                            & "                AND '60' <= OUTKA_L1.OUTKA_STATE_KB                                          " & vbNewLine _
    '                                            & "                AND OUTKA_L1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                            & "                AND OUTKA_L1.NRS_BR_CD = OUTKA_M1.NRS_BR_CD                                  " & vbNewLine _
    '                                            & "                AND OUTKA_L1.OUTKA_NO_L = OUTKA_M1.OUTKA_NO_L                                " & vbNewLine _
    '                                            & "           WHERE                                                                             " & vbNewLine _
    '                                            & "                   ZAITRS1.NRS_BR_CD   =  @NRS_BR_CD                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.CUST_CD_L   =  @CUST_CD_L                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.CUST_CD_M   =  @CUST_CD_M                                         " & vbNewLine _
    '                                            & "               AND ZAITRS1.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
    '                                            & "           )                                                                                 " & vbNewLine _
    '                                            & "           AND IDO_TRS.IDO_DATE <= @RIREKI_MAKE_DATE                                         " & vbNewLine _
    '                                            & "           AND IDO_TRS.SYS_DEL_FLG  = '0'                                                    " & vbNewLine _
    '                                            & "   GROUP BY                                                                                  " & vbNewLine _
    '                                            & "       IDO_TRS.NRS_BR_CD , IDO_TRS.N_ZAI_REC_NO, ZAI_TRS.IRIME                               " & vbNewLine _
    '                                            & "  )  MAIN                                                                                    " & vbNewLine _
    '                                            & "   GROUP BY                                                                                  " & vbNewLine _
    '                                            & "    MAIN.RIREKI_DATE ,MAIN.NRS_BR_CD ,MAIN.ZAI_REC_NO                                        " & vbNewLine _
    '                                            & "   HAVING SUM(MAIN.PORA_ZAI_NB) <> 0                                                         " & vbNewLine

    '#End Region '登録データ取得（実行）

    '#Region "履歴データ削除"

    '    'START YANAI No.107
    '    '''' <summary>
    '    '''' 削除処理 SQL（実行時）
    '    '''' </summary>
    '    '''' <remarks></remarks>
    '    'Private Const SQL_DELETE_JIKKOU As String = "DELETE FROM                               " & vbNewLine _
    '    '                                      & " $LM_TRN$..D_ZAI_ZAN_JITSU                    " & vbNewLine _
    '    '                                      & "WHERE                                         " & vbNewLine _
    '    '                                      & "    NRS_BR_CD = @NRS_BR_CD                    " & vbNewLine _
    '    '                                      & "AND RIREKI_DATE                               " & vbNewLine _
    '    '                                      & "    BETWEEN @RIREKI_MAKE_DATE AND '99999999'  " & vbNewLine _
    '    '                                      & "AND SYS_DEL_FLG ='0'                          " & vbNewLine _
    '    '                                      & "AND                                           " & vbNewLine _
    '    '                                      & " ZAI_REC_NO IN                                " & vbNewLine _
    '    '                                      & "  (                                           " & vbNewLine _
    '    '                                      & "    SELECT D01.ZAI_REC_NO                     " & vbNewLine _
    '    '                                      & "    FROM                                      " & vbNewLine _
    '    '                                      & "    $LM_TRN$..D_ZAI_TRS D01                   " & vbNewLine _
    '    '                                      & "    WHERE                                     " & vbNewLine _
    '    '                                      & "        D01.NRS_BR_CD  = @NRS_BR_CD           " & vbNewLine _
    '    '                                      & "    AND D01.CUST_CD_L = @CUST_CD_L            " & vbNewLine _
    '    '                                      & "    AND D01.CUST_CD_M = @CUST_CD_M            " & vbNewLine _
    '    '                                      & "  )                                           " & vbNewLine
    ''' <summary>
    ''' 削除処理 SQL（実行時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_JIKKOU As String = "DELETE FROM                               " & vbNewLine _
                                          & " $LM_TRN$..D_ZAI_ZAN_JITSU                    " & vbNewLine _
                                          & "WHERE                                         " & vbNewLine _
                                          & "    NRS_BR_CD = @NRS_BR_CD                    " & vbNewLine _
                                          & "AND RIREKI_DATE                               " & vbNewLine _
                                          & "    BETWEEN @RIREKI_MAKE_DATE AND '99999999'  " & vbNewLine _
                                          & "AND                                           " & vbNewLine _
                                          & " ZAI_REC_NO IN                                " & vbNewLine _
                                          & "  (                                           " & vbNewLine _
                                          & "    SELECT D01.ZAI_REC_NO                     " & vbNewLine _
                                          & "    FROM                                      " & vbNewLine _
                                          & "    $LM_TRN$..D_ZAI_TRS D01                   " & vbNewLine _
                                          & "    WHERE                                     " & vbNewLine _
                                          & "        D01.NRS_BR_CD  = @NRS_BR_CD           " & vbNewLine _
                                          & "    AND D01.CUST_CD_L = @CUST_CD_L            " & vbNewLine _
                                          & "    AND D01.CUST_CD_M = @CUST_CD_M            " & vbNewLine _
                                          & "  )                                           " & vbNewLine
    '    'END YANAI No.107



    '#End Region '履歴データ削除（実行）

    '#Region "データ登録"

    '    ''' <summary>
    '    ''' 在庫レコード番号有無チェック用SQL
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_ZAI_REC_NO_CHECK As String = " SELECT                                                       " & vbNewLine _
    '                                                 & "     COUNT(*)             AS REC_CNT                          " & vbNewLine _
    '                                                 & " FROM                                                         " & vbNewLine _
    '                                                 & "      $LM_TRN$..D_ZAI_ZAN_JITSU  D05                          " & vbNewLine _
    '                                                 & " WHERE                                                        " & vbNewLine _
    '                                                 & "     D05.NRS_BR_CD     = @NRS_BR_CD                           " & vbNewLine _
    '                                                 & " AND D05.ZAI_REC_NO    = @ZAI_REC_NO                          " & vbNewLine _
    '                                                 & " AND D05.RIREKI_DATE   = @RIREKI_DATE                         " & vbNewLine

    '    ''' <summary>
    '    ''' 履歴登録用SQL（新規登録）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_INSERT As String = " INSERT INTO               " & vbNewLine _
    '                                       & " $LM_TRN$..D_ZAI_ZAN_JITSU " & vbNewLine _
    '                                       & " (                         " & vbNewLine _
    '                                       & "   RIREKI_DATE             " & vbNewLine _
    '                                       & " , NRS_BR_CD               " & vbNewLine _
    '                                       & " , ZAI_REC_NO              " & vbNewLine _
    '                                       & " , PORA_ZAI_NB             " & vbNewLine _
    '                                       & " , ALCTD_NB                " & vbNewLine _
    '                                       & " , ALLOC_CAN_NB            " & vbNewLine _
    '                                       & " , PORA_ZAI_QT             " & vbNewLine _
    '                                       & " , ALCTD_QT                " & vbNewLine _
    '                                       & " , ALLOC_CAN_QT            " & vbNewLine _
    '                                       & " , SYS_ENT_DATE            " & vbNewLine _
    '                                       & " , SYS_ENT_TIME            " & vbNewLine _
    '                                       & " , SYS_ENT_PGID            " & vbNewLine _
    '                                       & " , SYS_ENT_USER            " & vbNewLine _
    '                                       & " , SYS_UPD_DATE            " & vbNewLine _
    '                                       & " , SYS_UPD_TIME            " & vbNewLine _
    '                                       & " , SYS_UPD_PGID            " & vbNewLine _
    '                                       & " , SYS_UPD_USER            " & vbNewLine _
    '                                       & " , SYS_DEL_FLG             " & vbNewLine _
    '                                       & " )                         " & vbNewLine _
    '                                       & "  VALUES                   " & vbNewLine _
    '                                       & " (                         " & vbNewLine _
    '                                       & "   @RIREKI_DATE            " & vbNewLine _
    '                                       & " , @NRS_BR_CD              " & vbNewLine _
    '                                       & " , @ZAI_REC_NO             " & vbNewLine _
    '                                       & " , @PORA_ZAI_NB            " & vbNewLine _
    '                                       & " , @ALCTD_NB               " & vbNewLine _
    '                                       & " , @ALLOC_CAN_NB           " & vbNewLine _
    '                                       & " , @PORA_ZAI_QT            " & vbNewLine _
    '                                       & " , @ALCTD_QT               " & vbNewLine _
    '                                       & " , @ALLOC_CAN_QT           " & vbNewLine _
    '                                       & " , @SYS_UPD_DATE           " & vbNewLine _
    '                                       & " , @SYS_UPD_TIME           " & vbNewLine _
    '                                       & " , @SYS_UPD_PGID           " & vbNewLine _
    '                                       & " , @SYS_UPD_USER           " & vbNewLine _
    '                                       & " , @SYS_UPD_DATE           " & vbNewLine _
    '                                       & " , @SYS_UPD_TIME           " & vbNewLine _
    '                                       & " , @SYS_UPD_PGID           " & vbNewLine _
    '                                       & " , @SYS_UPD_USER           " & vbNewLine _
    '                                       & " , '0'                     " & vbNewLine _
    '                                       & " )                         " & vbNewLine


    '    ''' <summary>
    '    ''' 履歴登録用SQL（更新）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_UPDATE As String = " UPDATE                            " & vbNewLine _
    '                                       & " $LM_TRN$..D_ZAI_ZAN_JITSU         " & vbNewLine _
    '                                       & " SET                               " & vbNewLine _
    '                                       & "   PORA_ZAI_NB =  @PORA_ZAI_NB     " & vbNewLine _
    '                                       & " , ALCTD_NB =     @ALCTD_NB        " & vbNewLine _
    '                                       & " , ALLOC_CAN_NB = @ALLOC_CAN_NB    " & vbNewLine _
    '                                       & " , PORA_ZAI_QT =  @PORA_ZAI_QT     " & vbNewLine _
    '                                       & " , ALCTD_QT =     @ALCTD_QT        " & vbNewLine _
    '                                       & " , ALLOC_CAN_QT = @ALLOC_CAN_QT    " & vbNewLine _
    '                                       & " , SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
    '                                       & " , SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
    '                                       & " , SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
    '                                       & " , SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
    '                                       & " , SYS_DEL_FLG =  '0'              " & vbNewLine _
    '                                       & " WHERE                             " & vbNewLine _
    '                                       & "     RIREKI_DATE =  @RIREKI_DATE   " & vbNewLine _
    '                                       & " AND NRS_BR_CD   =  @NRS_BR_CD     " & vbNewLine _
    '                                       & " AND ZAI_REC_NO  =  @ZAI_REC_NO    " & vbNewLine


    '#End Region 'データ登録（実行）

#End Region

#End Region '実行処理SQL

#Region "削除処理SQL"

    'START YANAI No.107
    '''' <summary>
    '''' 削除処理 SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_DELETE As String = "UPDATE                                  " & vbNewLine _
    '                                   & " $LM_TRN$..D_ZAI_ZAN_JITSU              " & vbNewLine _
    '                                   & "SET                                     " & vbNewLine _
    '                                   & "  SYS_UPD_DATE = @SYS_UPD_DATE          " & vbNewLine _
    '                                   & " ,SYS_UPD_TIME = @SYS_UPD_TIME          " & vbNewLine _
    '                                   & " ,SYS_UPD_PGID = @SYS_UPD_PGID          " & vbNewLine _
    '                                   & " ,SYS_UPD_USER = @SYS_UPD_USER          " & vbNewLine _
    '                                   & " ,SYS_DEL_FLG = '1'                     " & vbNewLine _
    '                                   & "WHERE                                   " & vbNewLine _
    '                                   & "    NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
    '                                   & " AND                                    " & vbNewLine _
    '                                   & "   (                                    " & vbNewLine _
    '                                   & "       RIREKI_DATE = @RIREKI_DATE       " & vbNewLine _
    '                                   & "    OR RIREKI_DATE < @RIREKI_DATE_FROM  " & vbNewLine _
    '                                   & "    )                                   " & vbNewLine _
    '                                   & "AND SYS_DEL_FLG ='0'                    " & vbNewLine _
    '                                   & "AND                                     " & vbNewLine _
    '                                   & " ZAI_REC_NO IN                          " & vbNewLine _
    '                                   & "  (                                     " & vbNewLine _
    '                                   & "    SELECT D01.ZAI_REC_NO               " & vbNewLine _
    '                                   & "    FROM                                " & vbNewLine _
    '                                   & "    $LM_TRN$..D_ZAI_TRS D01             " & vbNewLine _
    '                                   & "    WHERE                               " & vbNewLine _
    '                                   & "       D01.NRS_BR_CD  = @NRS_BR_CD      " & vbNewLine _
    '                                   & "    AND D01.CUST_CD_L = @CUST_CD_L      " & vbNewLine _
    '                                   & "    AND D01.CUST_CD_M = @CUST_CD_M      " & vbNewLine _
    '                                   & "  )                                     " & vbNewLine
    ''' <summary>
    ''' 削除処理 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "DELETE FROM                             " & vbNewLine _
                                       & " $LM_TRN$..D_ZAI_ZAN_JITSU              " & vbNewLine _
                                       & "WHERE                                   " & vbNewLine _
                                       & "    NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                       & " AND                                    " & vbNewLine _
                                       & "   (                                    " & vbNewLine _
                                       & "       RIREKI_DATE = @RIREKI_DATE       " & vbNewLine _
                                       & "    OR RIREKI_DATE < @RIREKI_DATE_FROM  " & vbNewLine _
                                       & "    )                                   " & vbNewLine _
                                       & "AND                                     " & vbNewLine _
                                       & " ZAI_REC_NO IN                          " & vbNewLine _
                                       & "  (                                     " & vbNewLine _
                                       & "    SELECT D01.ZAI_REC_NO               " & vbNewLine _
                                       & "    FROM                                " & vbNewLine _
                                       & "    $LM_TRN$..D_ZAI_TRS D01             " & vbNewLine _
                                       & "    WHERE                               " & vbNewLine _
                                       & "       D01.NRS_BR_CD  = @NRS_BR_CD      " & vbNewLine _
                                       & "    AND D01.CUST_CD_L = @CUST_CD_L      " & vbNewLine _
                                       & "    AND D01.CUST_CD_M = @CUST_CD_M      " & vbNewLine _
                                       & "  )                                     " & vbNewLine
    'END YANAI No.107


#End Region '削除処理SQL

#End Region 'Const

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
    ''' 在庫履歴データ取得（検索処理）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function SelectAction(ByVal ds As DataSet) As DataSet

        Return Me.GetSelectSearchData(ds, _
                                      LMD060DAC.SQL_SELECT_SEARCH_PRE + LMD060DAC.SQL_SELECT_SEARCH + LMD060DAC.SQL_SELECT_FROM, _
                                      LMD060DAC.SelectCondition.PTN1)

    End Function

    ''' <summary>
    ''' 在庫履歴データ取得（検索処理）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function GetSelectSearchData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMD060DAC.SelectCondition) As DataSet

        Dim str As String() = New String() {"CUST_CD" _
                                            , "CUST_NM" _
                                            , "CLOSE_KB_NM" _
                                            , "CULC_DATE" _
                                            , "TANTO_NM" _
                                            , "RIREKI_DATE" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CLOSE_KB" _
                                            , "ZAI_REC_NO" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "ROW_CNT" _
                                            , "REC_CNT" _
                                            }

        '検索条件データ設定
        Me._Row = ds.Tables(LMD060DAC.TABLE_NM_IN).Rows(0)

        Return Me.SelectListData(ds, LMD060DAC.TABLE_NM_OUT, sql, ptn, str)

    End Function

    ''' <summary>
    ''' WHERE句設定モジュール
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="dr"></param>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSqlWhere(ByVal sql As String, ByVal dr As DataRow, ByVal ptn As LMD060DAC.SelectCondition) As String

        Dim whereStr As StringBuilder = New StringBuilder

        With dr

            'WHERE句作成
            Select Case ptn

                Case LMD060DAC.SelectCondition.PTN1     '検索

                    If String.IsNullOrEmpty(.Item("TANTO_CD").ToString()) = False Then
                        whereStr.Append(" AND M30.USER_CD LIKE @TANTO_CD                                 ")
                        Me._StrSql.Append(vbNewLine)
                    End If

                    If String.IsNullOrEmpty(.Item("CUST_CD_L").ToString()) = False Then
                        whereStr.Append(" AND M07.CUST_CD_L LIKE @CUST_CD_L                              ")
                        Me._StrSql.Append(vbNewLine)
                    End If

                    If String.IsNullOrEmpty(.Item("CUST_CD_M").ToString()) = False Then
                        whereStr.Append(" AND M07.CUST_CD_M LIKE @CUST_CD_M                              ")
                        Me._StrSql.Append(vbNewLine)
                    End If

                    If String.IsNullOrEmpty(.Item("CLOSE_KB").ToString()) = False Then
                        whereStr.Append(" AND M06.CLOSE_KB = @CLOSE_KB                                   ")
                        Me._StrSql.Append(vbNewLine)
                    End If

                    If String.IsNullOrEmpty(.Item("CUST_NM").ToString()) = False Then
                        whereStr.Append(" AND M07.CUST_NM_L + '　' + M07.CUST_NM_M  LIKE @CUST_NM                        ")
                        Me._StrSql.Append(vbNewLine)
                    End If

                    If String.IsNullOrEmpty(.Item("TANTO_NM").ToString()) = False Then
                        whereStr.Append(" AND S01.USER_NM LIKE @TANTO_NM                                 ")
                        Me._StrSql.Append(vbNewLine)
                    End If

            End Select

        End With

        Return String.Concat(sql, whereStr.ToString())

    End Function

#End Region '検索処理

#Region "データチェック（排他チェック）"

    ''' <summary>
    ''' 更新日付を入れた検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectHaitaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMD060DAC.TABLE_NM_IN_DEL)

        For i As Integer = 0 To inTbl.Rows.Count() - 1
            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            If Me.SelectHaitaDataSub(LMD060DAC.SQL_HAITA_CHECK, LMD060DAC.SelectCondition.PTN2) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データ有無確認（排他チェック、データ有無チェックに利用）
    ''' </summary>
    ''' <param name="sql">String</param>
    ''' <param name="pnt">LMD060DAC.SelectCondition</param>
    ''' <returns>rue：データあり　False：データなし</returns>
    ''' <remarks>DataRow単位で処理を行う</remarks>
    Private Function SelectHaitaDataSub(ByVal sql As String, ByVal pnt As LMD060DAC.SelectCondition) As Boolean

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, pnt)
        'Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMD060DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理結果格納変数
        Dim result As Boolean = False

        '排他チェック
        reader.Read()
        Select Case pnt
            Case LMD060DAC.SelectCondition.PTN7
                result = Me.ResultCountChk(Convert.ToInt32(reader("REC_CNT"))) 'データ登録時の分岐チェックの場合
            Case Else
                result = Me.HaitaResultChk(Convert.ToInt32(reader("REC_CNT"))) '上記以外
        End Select
        reader.Close()

        Return result

    End Function

#End Region 'データチェック（排他チェック）

#Region "実行処理"

    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>実行処理コントロール</remarks>
    Private Function JikkouAction(ByVal ds As DataSet) As DataSet

        '更新件数初期化
        MyBase.SetResultCount(0)

        'DataSetのIN情報を取得
        Dim inTblPre As DataTable = ds.Tables(LMD060DAC.TABLE_NM_IN_DEL)
        '容赦なく消して問答無用で入れるように変更

        '①削除処理
        '指定日より後は消す
        '指定日より2ヶ月以上前は消す

        '②作成処理


        For i As Integer = 0 To inTblPre.Rows.Count() - 1

            'データ検索・削除に利用する条件格納
            Me._Row = inTblPre.Rows(i)

            '実行処理
            ds = Me.JikkouActionSub(ds)

        Next
        MyBase.SetResultCount(inTblPre.Rows.Count())

        MyBase.SetMessage("G035", New String() {"実行", "処理件数：" + MyBase.GetResultCount().ToString() + "件"})

        Return ds

    End Function

    ''' <summary>
    ''' 実行処理（詳細アクション）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JikkouActionSub(ByVal ds As DataSet) As DataSet

        '対象データ取得データセット初期化
        ds.Tables(LMD060DAC.TABLE_NM_INOUT_JIKKOU).Clear()

        ''データ取得
        'ds = Me.GetSelectJikkouData(ds, LMD060DAC.SQL_SELECT_JIKKOU, LMD060DAC.SelectCondition.PTN5)

        '削除処理
        Call Me.DeleteDataSub(LMD060DAC.SelectCondition.PTN4)

        'データ登録
        Me.UpdateInsertData()

        ''データ登録
        'Dim inTbl As DataTable = ds.Tables(LMD060DAC.TABLE_NM_INOUT_JIKKOU)

        ''登録対象データがない場合は、戻る
        'If inTbl.Rows.Count() < 1 Then
        '    Return ds
        'End If

        'For i As Integer = 0 To inTbl.Rows.Count() - 1

        '    Me._Row = inTbl.Rows(i)
        '    '対象データ有無によって分岐
        '    MyBase.SetResultCount(MyBase.GetResultCount() + Me.UpdateInsertData(Me.IsExistZaiRecNo()))
        '    'If Me.UpdateInsertData(Me.IsExistZaiRecNo()) = False Then
        '    '    '更新処理失敗時
        '    '    Return ds
        '    'End If
        'Next

        Return ds
    End Function

    ''' <summary>
    ''' 実行処理データ取得（検索処理）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function GetSelectJikkouData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMD060DAC.SelectCondition) As DataSet

        Dim str As String() = New String() {"RIREKI_DATE" _
                                            , "NRS_BR_CD" _
                                            , "ZAI_REC_NO" _
                                            , "PORA_ZAI_NB" _
                                            , "ALCTD_NB" _
                                            , "ALLOC_CAN_NB" _
                                            , "PORA_ZAI_QT" _
                                            , "ALCTD_QT" _
                                            , "ALLOC_CAN_QT" _
                                            }

        Return Me.SelectListData(ds, LMD060DAC.TABLE_NM_INOUT_JIKKOU, sql, ptn, str)

    End Function

    ''' <summary>
    ''' データ登録処理（UPDATE or INSERT）
    ''' </summary>
    ''' <returns>True：正常終了 False：異常終了</returns>
    ''' <remarks>UPDATE, INSERT SQL文発行</remarks>
    Private Function UpdateInsertData() As Integer

        'SQL文格納変数
        Dim cmd As SqlCommand = New SqlCommand

        'SQL文のコンパイル
        'insert
        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMD060DAC.SQL_INSERT, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, LMD060DAC.SelectCondition.PTN6)

        'タイムアウトの設定
        cmd.CommandTimeout = 1200

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMD060DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Return MyBase.GetInsertResult(cmd)
        
    End Function

#End Region '実行処理

#Region "削除処理"

    ''' <summary>
    ''' 在庫履歴データの論理削除処理コントロール
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        MyBase.SetResultCount(0)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMD060DAC.TABLE_NM_IN_DEL)

        '削除処理実行
        For i As Integer = 0 To inTbl.Rows.Count() - 1

            Me._Row = inTbl.Rows(i)

            '削除処理
            MyBase.SetResultCount(MyBase.GetResultCount() + Me.DeleteDataSub(LMD060DAC.SelectCondition.PTN3))

        Next

        MyBase.SetMessage("G035", New String() {"削除", "処理件数：" + MyBase.GetResultCount().ToString() + "件"})

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理（詳細）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>For文の中で繰り返される削除処理</remarks>
    Private Function DeleteDataSub(ByVal ptn As LMD060DAC.SelectCondition) As Integer

        'SQL文格納変数
        Dim cmd As SqlCommand = New SqlCommand

        'SQL文のコンパイル
        Select Case ptn

            Case LMD060DAC.SelectCondition.PTN3   '削除
                cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMD060DAC.SQL_DELETE, Me._Row.Item("NRS_BR_CD").ToString()))

            Case LMD060DAC.SelectCondition.PTN4   '実行（削除）
                cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMD060DAC.SQL_DELETE_JIKKOU, Me._Row.Item("NRS_BR_CD").ToString()))

        End Select

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMD060DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Return MyBase.GetUpdateResult(cmd)

    End Function

#End Region '削除処理

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNmOut">OUTデータテーブル名</param>
    ''' <param name="sql">発行SQL文</param>
    ''' <param name="ptn">パラメータ種別</param>
    ''' <param name="str">マッピングCOLデータ</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNmOut As String, ByVal sql As String, ByVal ptn As LMD060DAC.SelectCondition, ByVal str As String()) As DataSet
        '    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNmIn As String, ByVal tblNmOut As String, ByVal sql As String, ByVal ptn As LMD060DAC.SelectCondition, ByVal str As String()) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        '検索時のみ、WHERE句以下を設定
        If ptn <> LMD060DAC.SelectCondition.PTN5 Then

            'WHERE句設定
            sql = Me.CreateSqlWhere(sql, Me._Row, ptn)
            sql = String.Concat(sql, LMD060DAC.SQL_SELECT_ORDERBY)

        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        cmd.CommandTimeout = 1200

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMD060DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        'OUTデータセットの初期化
        ds.Tables(tblNmOut).Clear()

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, tblNmOut)

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.HaitaResultChk(MyBase.GetUpdateResult(cmd))

    End Function

    ''' <summary>
    ''' Insert文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function InsertResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.HaitaResultChk(MyBase.GetInsertResult(cmd))

    End Function

    ''' <summary>
    ''' 検索結果チェック（0件チェック）
    ''' </summary>
    ''' <param name="cnt">検索件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectResultChk(ByVal cnt As Integer) As Boolean

        '判定
        If cnt < 1 Then
            MyBase.SetMessage("G001")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 排他チェック（更新件数チェックにも利用）
    ''' </summary>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function HaitaResultChk(ByVal cnt As Integer) As Boolean

        '判定
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索結果チェック（0件チェック）エラーメッセージ設定なし
    ''' </summary>
    ''' <param name="cnt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ResultCountChk(ByVal cnt As Integer) As Boolean
        '判定
        If cnt < 1 Then
            Return False
        End If
        Return True
    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks>PN1：検索　PN2：排他チェック　PN3</remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMD060DAC.SelectCondition)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            'パラメータ設定
            Select Case ptn

                Case LMD060DAC.SelectCondition.PTN1     '検索

                    prmList.Add(MyBase.GetSqlParameter("@TANTO_CD", String.Concat(.Item("TANTO_CD").ToString(), "%"), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(.Item("CUST_CD_L").ToString(), "%"), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(.Item("CUST_CD_M").ToString(), "%"), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CLOSE_KB", String.Concat(.Item("CLOSE_KB").ToString()), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", .Item("CUST_NM").ToString(), "%"), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TANTO_NM", String.Concat("%", .Item("TANTO_NM").ToString(), "%"), DBDataType.NVARCHAR))

                Case LMD060DAC.SelectCondition.PTN2   '排他チェック

                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

                Case LMD060DAC.SelectCondition.PTN3, LMD060DAC.SelectCondition.PTN4    '削除、実行（削除）

                    '共通パラメータ
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
 
                    Select Case ptn

                        Case LMD060DAC.SelectCondition.PTN3   '削除のみ（論理削除）
                            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
                            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
                            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

                            prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE_FROM", DateAdd(DateInterval.Month, -2, Convert.ToDateTime(Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(MyBase.GetSystemDate()))).ToString("yyyyMMdd"), DBDataType.CHAR))
                            prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", .Item("RIREKI_DATE").ToString(), DBDataType.CHAR))

                        Case LMD060DAC.SelectCondition.PTN4   '実行（削除）のみ（物理削除）
                            prmList.Add(MyBase.GetSqlParameter("@RIREKI_MAKE_DATE", .Item("RIREKI_MAKE_DATE").ToString(), DBDataType.CHAR))
                            prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", .Item("RIREKI_MAKE_DATE").ToString(), DBDataType.CHAR))

                    End Select

                Case LMD060DAC.SelectCondition.PTN5   '実行（データ取得）

                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@RIREKI_MAKE_DATE", .Item("RIREKI_MAKE_DATE").ToString(), DBDataType.CHAR))
                    Dim tmpdate As Date = Date.Parse(Format(Convert.ToInt32(.Item("RIREKI_MAKE_DATE").ToString()), "0000/00/00"))
                    prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE_FROM", DateAdd(DateInterval.Month, -2, tmpdate).ToString("yyyyMMdd"), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", .Item("RIREKI_MAKE_DATE").ToString(), DBDataType.CHAR))

                Case LMD060DAC.SelectCondition.PTN6  '実行（データ登録）

                    prmList.Add(MyBase.GetSqlParameter("@RIREKI_MAKE_DATE", .Item("RIREKI_MAKE_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

                Case LMD060DAC.SelectCondition.PTN7  '実行（データ有無チェック）

                    prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", .Item("RIREKI_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))

            End Select

        End With

    End Sub

#End Region

End Class
