' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ       : システム管理
'  プログラムID     :  LMJ020    : 未使用荷主データ退避
'  作  成  者       :  [kobayashi]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMJ020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMJ020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    Private Const LM_ARCHIVE As String = "LM_ARCHIVE"

    Private Const TBLNM_TBLIN As String = "LMJ020IN"
    Private Const TBLNM_TARGET_TABLES As String = "TargetTables"
    Private Const TBLNM_J_DATA_ESC_LOG_HEAD As String = "LMJ020IN_J_DATA_ESC_LOG_HEAD"
    Private Const TBLNM_J_DATA_ESC_LOG_DTL As String = "LMJ020IN_J_DATA_ESC_LOG_DTL"

    Private Const KBN_PROCESS_TAIHI As String = "00"
    Private Const KBN_PROCESS_MODOSHI As String = "01"

    Private Const KBN_JOKYO_PROCESSING As String = "00"   '処理中
    Private Const KBN_JOKYO_PROCESSED As String = "00"    '処理済
    Private Const KBN_KEKKA_SUCCESS As String = "00"      '正常終了
    Private Const KBN_KEKKA_ERROR As String = "90"        '異常終了
    Private Const KBN_TIIMING_CHECK As String = "00"   '入力チェック処理
    Private Const KBN_TIIMING_MST As String = "10"     'LM_MST退避処理
    Private Const KBN_TIIMING_TRN As String = "20"     'LM_TRN退避処理
    Private Const KBN_TIIMING_DEL As String = "30"     '元データ削除処理
    Private Const KBN_TIIMING_ENDPROCESS As String = "40" '処理終了

    Private Const RTN_STS_SUCCESS As Integer = 0
    Private Const RTN_STS_ERROR As Integer = -1
    Private Const RTN_STS_NOTHING As Integer = -2
    Private Const RTN_STS_DELPROCESS As Integer = -3

    Private Const MSG_SUCCESS_INSERT As String = "テーブルが正常に終了しました。"
    Private Const MSG_PREDELETE_OK As String = "入力チェックＯＫ。過去の退避失敗時分のデータを削除しました。"
    Private Const MSG_PREDELETE_NG As String = "入力チェックＮＧ。移行元データが０件にも関わらず、退避データに値が存在します。"
    Private Const MSG_DELETE_OK As String = "入力チェックＯＫ。テーブルの削除が可能です。"
    Private Const MSG_DELETE_NG As String = "入力チェックＮＧ。退避テーブルと本テーブルの件数が一致しませんでした。"
    Private Const MSG_ERR_INSERT As String = "テーブルの更新に失敗しました。"
    Private Const MSG_SUCCESS_DELETE As String = "データの削除をしました。"

    Private Enum DeleteCheckMode
        PreCheck = 0
        DelCheck
    End Enum



#Region "データ退避検索処理 SQL"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(MAIN.CUST_CD_L)		            AS SELECT_CNT       " & vbNewLine

    Private Const SQL_SELECT_DATA_TAIHI As String = "    SELECT                                                                                                      " & vbNewLine _
                                            & "           MAIN.NRS_BR_CD        AS NRS_BR_CD                                                                   " & vbNewLine _
                                            & "          ,MAIN.CUST_CD_L        AS CUST_CD_L                                                                   " & vbNewLine _
                                            & "          ,MAIN.CUST_NM_L        AS CUST_NM_L                                                                   " & vbNewLine _
                                            & "          ,MAIN.LAST_UPD_DATE    AS LAST_UPD_DATE                                                               " & vbNewLine _
                                            & "          ,MAIN.USER_NM          AS TANTO_USER                                                                  " & vbNewLine _
                                            & "          ,''                    AS TAIHI_DATE                                                                  " & vbNewLine _
                                            & "          ,''                    AS TAIHI_USER                                                                  " & vbNewLine _
                                            & "          ,@PROCESS_KB           AS PROCESS_KB                                                                  " & vbNewLine

    Private Const SQL_FROM_TAIHI As String = "    FROM (                                                                                                             " & vbNewLine _
                                    & "    SELECT DISTINCT                                                                                                     " & vbNewLine _
                                    & "           MC.NRS_BR_CD                                                                                                 " & vbNewLine _
                                    & "          ,MC.CUST_CD_L                                                                                                 " & vbNewLine _
                                    & "          ,MC.CUST_NM_L                                                                                                 " & vbNewLine _
                                    & "          ,SU.USER_NM                                                                                                   " & vbNewLine _
                                    & "          ,ZAI.QT                                                                                                       " & vbNewLine _
                                    & "          ,CASE WHEN ZAI.SYS_UPD_DATE > UNSO.SYS_UPD_DATE THEN ZAI.SYS_UPD_DATE                                         " & vbNewLine _
                                    & "                ELSE UNSO.SYS_UPD_DATE END LAST_UPD_DATE                                                                " & vbNewLine _
                                    & "      FROM [$LM_MST$].[dbo].[M_CUST] MC                                                                                 " & vbNewLine _
                                    & "    --在庫データ                                                                                                             " & vbNewLine _
                                    & "      LEFT JOIN                                                                                                         " & vbNewLine _
                                    & "    (SELECT ZAI.NRS_BR_CD,ZAI.CUST_CD_L,SUM(ZAI.PORA_ZAI_QT) QT,MAX(ZAI.SYS_UPD_DATE) SYS_UPD_DATE FROM                 " & vbNewLine _
                                    & "    $LM_TRN$..D_ZAI_TRS ZAI WHERE ZAI.SYS_DEL_FLG='0'                                                                                            " & vbNewLine _
                                    & "    GROUP BY ZAI.NRS_BR_CD,ZAI.CUST_CD_L                                                                                " & vbNewLine _
                                    & "    ) ZAI                                                                                                               " & vbNewLine _
                                    & "    ON ZAI.NRS_BR_CD = MC.NRS_BR_CD AND ZAI.CUST_CD_L= MC.CUST_CD_L                                                     " & vbNewLine _
                                    & "    --運送データ                                                                                                             " & vbNewLine _
                                    & "      LEFT JOIN                                                                                                         " & vbNewLine _
                                    & "    (SELECT UNSO.NRS_BR_CD,UNSO.CUST_CD_L,MAX(UNSO.SYS_UPD_DATE) SYS_UPD_DATE FROM                                      " & vbNewLine _
                                    & "    $LM_TRN$..F_UNSO_L UNSO  WHERE UNSO.SYS_DEL_FLG='0'                                                                                            " & vbNewLine _
                                    & "    GROUP BY UNSO.NRS_BR_CD,UNSO.CUST_CD_L                                                                              " & vbNewLine _
                                    & "    ) UNSO                                                                                                              " & vbNewLine _
                                    & "    ON UNSO.NRS_BR_CD = MC.NRS_BR_CD AND UNSO.CUST_CD_L= MC.CUST_CD_L                                                   " & vbNewLine _
                                    & "    --ユーザマスタ                                                                                                            " & vbNewLine _
                                    & "    LEFT JOIN $LM_MST$..S_USER SU                                                                                       " & vbNewLine _
                                    & "    ON SU.USER_CD = MC.TANTO_CD                                                                                         " & vbNewLine _
                                    & "    where                                                                                                               " & vbNewLine _
                                    & "    (ZAI.QT = 0 OR ZAI.QT IS NULL)                                                                                                         " & vbNewLine _
                                    & "    and MC.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
                                    & "    and MC.BACKUP_FLG = '00'                                                                                           " & vbNewLine _
                                    & "    ) MAIN                                                                                                              " & vbNewLine

    ''' <summary>
    ''' データ抽出用WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String = "WHERE                                                                            " & vbNewLine _
                                      & "  MAIN.NRS_BR_CD = @NRS_BR_CD                                                      " & vbNewLine


    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     MAIN.NRS_BR_CD                                " & vbNewLine _
                                         & "    ,MAIN.CUST_CD_L                                " & vbNewLine

#End Region

#Region "データ戻し検索処理　SQL"

    Private Const SQL_SELECT_DATA_MODOSHI As String = "    SELECT                                                                                                                                                                " & vbNewLine _
                                                    & "           MAIN.NRS_BR_CD        AS NRS_BR_CD                                                                                                                             " & vbNewLine _
                                                    & "          ,MAIN.CUST_CD_L        AS CUST_CD_L                                                                                                                             " & vbNewLine _
                                                    & "          ,MAIN.CUST_NM_L        AS CUST_NM_L                                                                                                                             " & vbNewLine _
                                                    & "          ,MAIN.LAST_UPD_DATE    AS LAST_UPD_DATE                                                                                                                         " & vbNewLine _
                                                    & "          ,MAIN.USER_NM          AS TANTO_USER                                                                                                                            " & vbNewLine _
                                                    & "          ,PROC_DATE             AS TAIHI_DATE                                                                                                                            " & vbNewLine _
                                                    & "          ,MAIN.TAIHI_USER       AS TAIHI_USER                                                                                                                            " & vbNewLine _
                                                    & "          ,'01'           AS PROCESS_KB                                                                                                                                   " & vbNewLine

    Private Const SQL_FROM_MODOSHI As String = "    FROM (                                                                                                                                                                " & vbNewLine _
                                             & "    SELECT DISTINCT                                                                                                                                                       " & vbNewLine _
                                             & "           MC.NRS_BR_CD                                                                                                                                                   " & vbNewLine _
                                             & "          ,MC.CUST_CD_L                                                                                                                                                   " & vbNewLine _
                                             & "          ,MC.CUST_NM_L                                                                                                                                                   " & vbNewLine _
                                             & "          ,SU.USER_NM                                                                                                                                                     " & vbNewLine _
                                             & "          ,ZAI.QT                                                                                                                                                         " & vbNewLine _
                                             & "          ,CASE WHEN ZAI.SYS_UPD_DATE > UNSO.SYS_UPD_DATE THEN ZAI.SYS_UPD_DATE                                                                                           " & vbNewLine _
                                             & "                ELSE UNSO.SYS_UPD_DATE END LAST_UPD_DATE                                                                                                                  " & vbNewLine _
                                             & "          ,PROC_DATE                                                                                                                                                      " & vbNewLine _
                                             & "          ,SU2.USER_NM AS TAIHI_USER                                                                                                                                      " & vbNewLine _
                                             & "      FROM $LM_MST$..M_CUST MC                                                                                                                                     " & vbNewLine _
                                             & "      LEFT JOIN (select NRS_BR_CD,CUST_CD_L,PROC_DATE,SYS_ENT_USER                                                                                                        " & vbNewLine _
                                             & "                 FROM (SELECT NRS_BR_CD,CUST_CD_L,PROC_DATE,SYS_ENT_USER,ROW_NUMBER()OVER(PARTITION BY NRS_BR_CD,CUST_CD_L ORDER BY PROC_DATE) AS SORT                    " & vbNewLine _
                                             & "                       FROM $LM_MST$..J_DATA_ESC_LOG_HEAD WHERE EXEC_RESULT_KB = '00' AND EXEC_STATE_KB = '01'                                                             " & vbNewLine _
                                             & "                       ) BASE                                                                                                                                             " & vbNewLine _
                                             & "                 WHERE SORT = 1                                                                                                                                           " & vbNewLine _
                                             & "      ) LOGH                                                                                                                                                              " & vbNewLine _
                                             & "      ON MC.NRS_BR_CD = LOGH.NRS_BR_CD                                                                                                                                    " & vbNewLine _
                                             & "      AND MC.CUST_CD_L = LOGH.CUST_CD_L                                                                                                                                   " & vbNewLine _
                                             & "    --在庫データ                                                                                                                                                               " & vbNewLine _
                                             & "      LEFT JOIN                                                                                                                                                           " & vbNewLine _
                                             & "    (SELECT ZAI.NRS_BR_CD,ZAI.CUST_CD_L,SUM(ZAI.PORA_ZAI_QT) QT,MAX(ZAI.SYS_UPD_DATE) SYS_UPD_DATE FROM                                                                   " & vbNewLine _
                                             & "    $LM_ESCAPE_TRN$..D_ZAI_TRS ZAI WHERE ZAI.SYS_DEL_FLG='0'                                                                                                                   " & vbNewLine _
                                             & "    GROUP BY ZAI.NRS_BR_CD,ZAI.CUST_CD_L                                                                                                                                  " & vbNewLine _
                                             & "    ) ZAI                                                                                                                                                                 " & vbNewLine _
                                             & "    ON ZAI.NRS_BR_CD = MC.NRS_BR_CD AND ZAI.CUST_CD_L= MC.CUST_CD_L                                                                                                       " & vbNewLine _
                                             & "    --運送データ                                                                                                                                                               " & vbNewLine _
                                             & "      LEFT JOIN                                                                                                                                                           " & vbNewLine _
                                             & "    (SELECT UNSO.NRS_BR_CD,UNSO.CUST_CD_L,MAX(UNSO.SYS_UPD_DATE) SYS_UPD_DATE FROM                                                                                        " & vbNewLine _
                                             & "    $LM_ESCAPE_TRN$..F_UNSO_L UNSO  WHERE UNSO.SYS_DEL_FLG='0'                                                                                                                 " & vbNewLine _
                                             & "    GROUP BY UNSO.NRS_BR_CD,UNSO.CUST_CD_L                                                                                                                                " & vbNewLine _
                                             & "    ) UNSO                                                                                                                                                                " & vbNewLine _
                                             & "    ON UNSO.NRS_BR_CD = MC.NRS_BR_CD AND UNSO.CUST_CD_L= MC.CUST_CD_L                                                                                                     " & vbNewLine _
                                             & "      --ユーザマスタ                                                                                                                                                            " & vbNewLine _
                                             & "    LEFT JOIN $LM_MST$..S_USER SU                                                                                                                                           " & vbNewLine _
                                             & "    ON SU.USER_CD = MC.TANTO_CD                                                                                                                                           " & vbNewLine _
                                             & "      --ユーザマスタ                                                                                                                                                            " & vbNewLine _
                                             & "    LEFT JOIN $LM_MST$..S_USER SU2                                                                                                                                          " & vbNewLine _
                                             & "    ON SU2.USER_CD = LOGH.SYS_ENT_USER                                                                                                                                     " & vbNewLine _
                                             & "    where                                                                                                                                                                 " & vbNewLine _
                                             & "        MC.SYS_DEL_FLG = '1'                                                                                                                                              " & vbNewLine _
                                             & "    and MC.BACKUP_FLG IN ('01','02')                                                                                                                                              " & vbNewLine _
                                             & "    ) MAIN                                                                                                                                                                " & vbNewLine

#End Region

#Region "入力チェック"

    Private Const SQL_SELECT_COUNT_CHECK As String = "SELECT COUNT(*) AS SELECT_CNT            " & vbNewLine _
                                             & "FROM $LM_MST$..M_CUST                          " & vbNewLine _
                                             & "WHERE NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
                                             & "AND CUST_CD_L   = @CUST_CD_L                   " & vbNewLine _
                                             & "AND CUST_CD_M   = '00'                         " & vbNewLine _
                                             & "AND CUST_CD_S   = '00'                         " & vbNewLine _
                                             & "AND CUST_CD_SS  = '00'                         " & vbNewLine

    Private Const SQL_WHERE_CHECK_TAIHI As String = "AND (BACKUP_FLG IN ('01','02')                           " & vbNewLine _
                                                    & "     OR SYS_DEL_FLG = '1')                           " & vbNewLine

    Private Const SQL_WHERE_CHECK_MODOSHI As String = "AND (BACKUP_FLG = '00'                           " & vbNewLine _
                                                    & "    OR SYS_DEL_FLG = '0')                           " & vbNewLine

    Private Const SQL_SELECT_COUNT_CHECK_ZAIKO As String = "SELECT ISNULL(ROUND(SUM(PORA_ZAI_QT)+0.9,0,1),0) AS SELECT_CNT                   " & vbNewLine _
                                             & "FROM $LM_TRN$..D_ZAI_TRS                             " & vbNewLine _
                                             & "WHERE NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
                                             & "AND CUST_CD_L = @CUST_CD_L                           " & vbNewLine _
                                             & "AND SYS_DEL_FLG = '0'                                " & vbNewLine

#End Region

#Region "Log SQL"

    Private Const SQL_INSERT_J_DATA_ESC_LOG_HEAD As String = "INSERT INTO                              " & vbNewLine _
                                                            & "$LM_MST$..J_DATA_ESC_LOG_HEAD           " & vbNewLine _
                                                            & "(                                       " & vbNewLine _
                                                            & "     BATCH_NO                           " & vbNewLine _
                                                            & "    ,NRS_BR_CD                          " & vbNewLine _
                                                            & "    ,OPE_USER_CD                        " & vbNewLine _
                                                            & "    ,SYORI_KB                           " & vbNewLine _
                                                            & "    ,PROC_DATE                          " & vbNewLine _
                                                            & "    ,CUST_CD_L                          " & vbNewLine _
                                                            & "    ,LAST_UPD_DATE                      " & vbNewLine _
                                                            & "    ,EXEC_STATE_KB                      " & vbNewLine _
                                                            & "    ,EXEC_RESULT_KB                     " & vbNewLine _
                                                            & "    ,MESSAGE                            " & vbNewLine _
                                                            & "    ,EXEC_TIMING_KB                     " & vbNewLine _
                                                            & "    ,EXEC_START_DATE                    " & vbNewLine _
                                                            & "    ,EXEC_START_TIME                    " & vbNewLine _
                                                            & "    ,EXEC_END_DATE                      " & vbNewLine _
                                                            & "    ,EXEC_END_TIME                      " & vbNewLine _
                                                            & "    ,SYS_ENT_DATE                       " & vbNewLine _
                                                            & "    ,SYS_ENT_TIME                       " & vbNewLine _
                                                            & "    ,SYS_ENT_PGID                       " & vbNewLine _
                                                            & "    ,SYS_ENT_USER                       " & vbNewLine _
                                                            & "    ,SYS_UPD_DATE                       " & vbNewLine _
                                                            & "    ,SYS_UPD_TIME                       " & vbNewLine _
                                                            & "    ,SYS_UPD_PGID                       " & vbNewLine _
                                                            & "    ,SYS_UPD_USER                       " & vbNewLine _
                                                            & "    ,SYS_DEL_FLG                        " & vbNewLine _
                                                            & "    )VALUES(                            " & vbNewLine _
                                                            & "     @BATCH_NO                          " & vbNewLine _
                                                            & "    ,@NRS_BR_CD                         " & vbNewLine _
                                                            & "    ,@OPE_USER_CD                       " & vbNewLine _
                                                            & "    ,@SYORI_KB                          " & vbNewLine _
                                                            & "    ,@PROC_DATE                         " & vbNewLine _
                                                            & "    ,@CUST_CD_L                         " & vbNewLine _
                                                            & "    ,@LAST_UPD_DATE                     " & vbNewLine _
                                                            & "    ,@EXEC_STATE_KB                     " & vbNewLine _
                                                            & "    ,@EXEC_RESULT_KB                    " & vbNewLine _
                                                            & "    ,@MESSAGE                           " & vbNewLine _
                                                            & "    ,@EXEC_TIMING_KB                    " & vbNewLine _
                                                            & "    ,@EXEC_START_DATE                   " & vbNewLine _
                                                            & "    ,@EXEC_START_TIME                   " & vbNewLine _
                                                            & "    ,@EXEC_END_DATE                     " & vbNewLine _
                                                            & "    ,@EXEC_END_TIME                     " & vbNewLine _
                                                            & "    ,@SYS_ENT_DATE                      " & vbNewLine _
                                                            & "    ,@SYS_ENT_TIME                      " & vbNewLine _
                                                            & "    ,@SYS_ENT_PGID                      " & vbNewLine _
                                                            & "    ,@SYS_ENT_USER                      " & vbNewLine _
                                                            & "    ,@SYS_UPD_DATE                      " & vbNewLine _
                                                            & "    ,@SYS_UPD_TIME                      " & vbNewLine _
                                                            & "    ,@SYS_UPD_PGID                      " & vbNewLine _
                                                            & "    ,@SYS_UPD_USER                      " & vbNewLine _
                                                            & "    ,@SYS_DEL_FLG                       " & vbNewLine _
                                                            & "    )                                   " & vbNewLine

    Private Const SQL_INSERT_J_DATA_ESC_LOG_DTL As String = "INSERT INTO                              " & vbNewLine _
                                                        & "$LM_MST$..J_DATA_ESC_LOG_DTL           " & vbNewLine _
                                                        & "(                                       " & vbNewLine _
                                                        & "     BATCH_NO                           " & vbNewLine _
                                                        & "    ,NRS_BR_CD                          " & vbNewLine _
                                                        & "    ,OPE_USER_CD                        " & vbNewLine _
                                                        & "    ,SYORI_KB                           " & vbNewLine _
                                                        & "    ,REC_NO                          " & vbNewLine _
                                                        & "    ,PG_ID                          " & vbNewLine _
                                                        & "    ,CNT                      " & vbNewLine _
                                                        & "    ,MESSAGE                      " & vbNewLine _
                                                        & "    ,SYS_ENT_DATE                       " & vbNewLine _
                                                        & "    ,SYS_ENT_TIME                       " & vbNewLine _
                                                        & "    ,SYS_ENT_PGID                       " & vbNewLine _
                                                        & "    ,SYS_ENT_USER                       " & vbNewLine _
                                                        & "    ,SYS_UPD_DATE                       " & vbNewLine _
                                                        & "    ,SYS_UPD_TIME                       " & vbNewLine _
                                                        & "    ,SYS_UPD_PGID                       " & vbNewLine _
                                                        & "    ,SYS_UPD_USER                       " & vbNewLine _
                                                        & "    ,SYS_DEL_FLG                        " & vbNewLine _
                                                        & "    )VALUES(                            " & vbNewLine _
                                                        & "     @BATCH_NO                          " & vbNewLine _
                                                        & "    ,@NRS_BR_CD                         " & vbNewLine _
                                                        & "    ,@OPE_USER_CD                       " & vbNewLine _
                                                        & "    ,@SYORI_KB                          " & vbNewLine _
                                                        & "    ,@REC_NO                         " & vbNewLine _
                                                        & "    ,@PG_ID                         " & vbNewLine _
                                                        & "    ,@CNT                     " & vbNewLine _
                                                        & "    ,@MESSAGE                     " & vbNewLine _
                                                        & "    ,@SYS_ENT_DATE                      " & vbNewLine _
                                                        & "    ,@SYS_ENT_TIME                      " & vbNewLine _
                                                        & "    ,@SYS_ENT_PGID                      " & vbNewLine _
                                                        & "    ,@SYS_ENT_USER                      " & vbNewLine _
                                                        & "    ,@SYS_UPD_DATE                      " & vbNewLine _
                                                        & "    ,@SYS_UPD_TIME                      " & vbNewLine _
                                                        & "    ,@SYS_UPD_PGID                      " & vbNewLine _
                                                        & "    ,@SYS_UPD_USER                      " & vbNewLine _
                                                        & "    ,@SYS_DEL_FLG                       " & vbNewLine _
                                                        & "    )                                   " & vbNewLine

    ''' <summary>
    ''' UPDATE（J_DATA_ESC_LOG_HEAD）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_J_DATA_ESC_LOG_HEAD As String = "UPDATE $LM_MST$..J_DATA_ESC_LOG_HEAD SET           " & vbNewLine _
                                                            & "     EXEC_STATE_KB = @EXEC_STATE_KB               " & vbNewLine _
                                                            & "    ,EXEC_RESULT_KB = @EXEC_RESULT_KB             " & vbNewLine _
                                                            & "    ,MESSAGE = @MESSAGE                           " & vbNewLine _
                                                            & "    ,EXEC_TIMING_KB = @EXEC_TIMING_KB             " & vbNewLine _
                                                            & "    ,EXEC_END_DATE = @EXEC_END_DATE               " & vbNewLine _
                                                            & "    ,EXEC_END_TIME = @EXEC_END_TIME               " & vbNewLine _
                                                            & "    ,SYS_UPD_DATE = @SYS_UPD_DATE                 " & vbNewLine _
                                                            & "    ,SYS_UPD_TIME = @SYS_UPD_TIME                 " & vbNewLine _
                                                            & "    ,SYS_UPD_PGID = @SYS_UPD_PGID                 " & vbNewLine _
                                                            & "    ,SYS_UPD_USER = @SYS_UPD_USER                 " & vbNewLine _
                                                            & "WHERE                                             " & vbNewLine _
                                                            & "    BATCH_NO = @BATCH_NO                          " & vbNewLine _
                                                            & "AND NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
                                                            & "AND OPE_USER_CD = @OPE_USER_CD                    " & vbNewLine _
                                                            & "AND SYORI_KB = @SYORI_KB                          " & vbNewLine


    ''' <summary>
    ''' UPDATE（M_CUST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_M_CUST As String = "UPDATE $LM_MST$..M_CUST SET                               " & vbNewLine _
                                                            & "     BACKUP_FLG = CASE WHEN @BACKUP_FLG = '00' THEN @BACKUP_FLG               " & vbNewLine _
                                                            & "                       ELSE CASE WHEN SYS_DEL_FLG = '0' THEN '01'     " & vbNewLine _
                                                            & "                                 ELSE '02' END  " & vbNewLine _
                                                            & "                  END                       " & vbNewLine _
                                                            & "    ,SYS_UPD_DATE = @SYS_UPD_DATE           " & vbNewLine _
                                                            & "    ,SYS_UPD_TIME = @SYS_UPD_TIME           " & vbNewLine _
                                                            & "    ,SYS_UPD_PGID = @SYS_UPD_PGID           " & vbNewLine _
                                                            & "    ,SYS_UPD_USER = @SYS_UPD_USER           " & vbNewLine _
                                                            & "    ,SYS_DEL_FLG = CASE WHEN BACKUP_FLG = '02' THEN '1'              " & vbNewLine _
                                                            & "                        ELSE @SYS_DEL_FLG END " & vbNewLine _
                                                            & "WHERE                                       " & vbNewLine _
                                                            & "    NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
                                                            & "AND CUST_CD_L = @CUST_CD_L                  " & vbNewLine

#End Region

#Region "LM_TABLES"

    ''' <summary>
    ''' 移行テーブル（LM_TABLES)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TARGET_TABLES
        NON = 0
        M_COA
        M_COACONFIG
        M_CUSTCOND
        M_DEST
        M_DEST_DETAILS
        M_DESTGOODS
        M_FURI_GOODS
        M_GOODS
        M_GOODS_DETAILS
        M_SAGYO
        M_TANKA
        M_UNCHIN_TARIFF_SET
        B_INKA_L
        B_INKA_M
        B_INKA_S
        C_OUTKA_L
        C_OUTKA_M
        C_OUTKA_S
        D_IDO_HANDY
        D_IDO_TRS
        D_WK_ZAI_PRT
        D_ZAI_SHOGOH
        D_ZAI_SHOGOH_CUST
        D_ZAI_SHOGOH_CUST_SUM
        D_ZAI_TRS
        D_ZAI_ZAN_JITSU
        E_SAGYO
        E_SAGYO_SIJI
        F_UNCHIN_TRS
        F_UNSO_L
        F_UNSO_M
        G_SEKY_MEISAI
        G_SEKY_MEISAI_PRT
        G_SEKY_TBL
        'G_TANKA_WK
        G_ZAIK_ZAN
        'H_EDI_GOODSREP_TBL
        'H_EDI_PRINT
        'H_GOODS_EDI_BP
        'H_INKAEDI_DTL_BP
        'H_INKAEDI_DTL_DPN
        'H_INKAEDI_DTL_NCGO
        'H_INKAEDI_DTL_NIK
        'H_INKAEDI_DTL_NISSAN
        'H_INKAEDI_DTL_NSN
        'H_INKAEDI_DTL_UKM
        'H_INKAEDI_HED_BP
        'H_INKAEDI_HED_DPN
        'H_INKAEDI_HED_NCGO
        'H_INKAEDI_HED_NSN
        H_INKAEDI_L
        H_INKAEDI_M
        'H_INOUTKAEDI_DTL_DIC_NEW
        'H_INOUTKAEDI_DTL_DOW
        'H_INOUTKAEDI_DTL_FJF
        'H_INOUTKAEDI_DTL_M3PL
        'H_INOUTKAEDI_DTL_SMK
        'H_INOUTKAEDI_DTL_TOHO
        'H_INOUTKAEDI_HED_DIC_NEW
        'H_INOUTKAEDI_HED_DOW
        'H_INOUTKAEDI_HED_FJF
        'H_INOUTKAEDI_HED_SMK
        'H_INOUTKAEDI_HED_TOHO
        'H_NRSBIN_DIC
        'H_NRSBIN_TOR
        'H_NRSCUST_DIC
        'H_NRSGOODS_DIC
        'H_NRSGOODS_DNS
        'H_OUTKA_L_BP
        'H_OUTKAEDI_DTL_ASH
        'H_OUTKAEDI_DTL_BP
        'H_OUTKAEDI_DTL_BYK
        'H_OUTKAEDI_DTL_DIC
        'H_OUTKAEDI_DTL_DNS
        'H_OUTKAEDI_DTL_DPN
        'H_OUTKAEDI_DTL_DSP
        'H_OUTKAEDI_DTL_DSPAH
        'H_OUTKAEDI_DTL_GODO
        'H_OUTKAEDI_DTL_HON
        'H_OUTKAEDI_DTL_JC
        'H_OUTKAEDI_DTL_JT
        'H_OUTKAEDI_DTL_KTK
        'H_OUTKAEDI_DTL_LNZ
        'H_OUTKAEDI_DTL_MHM
        'H_OUTKAEDI_DTL_NCGO
        'H_OUTKAEDI_DTL_NIK
        'H_OUTKAEDI_DTL_NKS
        'H_OUTKAEDI_DTL_NSN
        'H_OUTKAEDI_DTL_OTK
        'H_OUTKAEDI_DTL_SFJ
        'H_OUTKAEDI_DTL_SNK
        'H_OUTKAEDI_DTL_SNZ
        'H_OUTKAEDI_DTL_TOR
        'H_OUTKAEDI_DTL_UKM
        'H_OUTKAEDI_HED_ASH
        'H_OUTKAEDI_HED_BP
        'H_OUTKAEDI_HED_DIC
        'H_OUTKAEDI_HED_DNS
        'H_OUTKAEDI_HED_DPN
        'H_OUTKAEDI_HED_GODO
        'H_OUTKAEDI_HED_HON
        'H_OUTKAEDI_HED_JC
        'H_OUTKAEDI_HED_NCGO
        'H_OUTKAEDI_HED_NSN
        'H_OUTKAEDI_HED_OTK
        'H_OUTKAEDI_HED_SFJ
        'H_OUTKAEDI_HED_TOR
        'H_OUTKAEDI_HED_UKM
        H_OUTKAEDI_L
        'H_OUTKAEDI_L_PRT_LNZ
        H_OUTKAEDI_M
        'H_OUTKAEDI_M_PRT_LNZ
        'H_SENDEDI_BP
        'H_SENDINEDI_DPN
        'H_SENDINEDI_NCGO
        'H_SENDINEDI_NIK
        'H_SENDINEDI_NSN
        'H_SENDINEDI_UTI
        'H_SENDINOUTEDI_DOW
        'H_SENDINOUTEDI_UKM
        'H_SENDMONTHLY_SNZ
        'H_SENDOUTEDI_ASH
        'H_SENDOUTEDI_DPN
        'H_SENDOUTEDI_NCGO
        'H_SENDOUTEDI_NIK
        'H_SENDOUTEDI_NSN
        'H_SENDOUTEDI_SFJ
        'H_SENDOUTEDI_SNK
        'H_UNSOCO_EDI
        'H_ZAIKO_EDI_BP
        'I_CONT_TRACK
        'I_CONT_TRACK_LOG
        'I_DOW_SEIQ_PRT
        'I_HAISO_UNCHIN_TRS
        'I_HIKITORI_UNCHIN_MEISAI
        'I_HON_TEIKEN
        'I_HONEY_ALBAS_CHG
        'I_HONEY_SHIPTOCD_CHG
        'I_MCPU_UNCHIN_CHK
        'I_NRC_KAISHU_TBL
        'I_UKI_BUNRUI_MST
        'I_UKI_HOKOKU
        'I_UKI_ZAIKO
        'I_UKI_ZAIKO_SUM
        'I_UKIMA_SEKY_MEISAI
        'I_YOKO_UNCHIN_TRS
        'I_YUSO_R
        'I_YUSO_R_SUM
        'M_BYK_GOODS
        'M_CHOKUSO_NIK
        'M_HINMOKU_FJF
        'M_HINMOKU_TRM
        'M_KOKYAKU_TRM
        'M_SEHIN_NIK
        'M_SET_GOODS_LNZ
        'M_TOKUI_FJF
        'M_TOKUI_NIK

    End Enum
#End Region

#Region "InsertIntoSQL"

    Private Const SQL_INSERTINTO As String = "Insert into  "
    Private Const SQL_PREDELCHECK As String = "SELECT CASE WHEN MOTO.CNT <> 0 AND BK.CNT <> 0 THEN 1 WHEN MOTO.CNT = 0 AND BK.CNT <> 0  THEN -1 ELSE 0 END AS SUCCESS_FLG FROM "
    Private Const SQL_DELETE As String = "DELETE "
    Private Const SQL_DELCHECK As String = "SELECT CASE WHEN MOTO.CNT = BK.CNT THEN 1 ELSE 0 END AS SUCCESS_FLG FROM "

#Region "LM_MST"

    Private Const SQL_M_COA_TBLNM As String = "$LM_ESCAPE_MST$..M_COA "
    Private Const SQL_M_COA_SELECT As String = "SELECT L.* "
    Private Const SQL_M_COA_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_COA_FROM As String = "FROM $LM_MST$..M_COA L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_COACONFIG_TBLNM As String = "$LM_ESCAPE_MST$..M_COACONFIG "
    Private Const SQL_M_COACONFIG_SELECT As String = "SELECT L.* "
    Private Const SQL_M_COACONFIG_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_COACONFIG_FROM As String = "FROM $LM_MST$..M_COACONFIG L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_CUSTCOND_TBLNM As String = "$LM_ESCAPE_MST$..M_CUSTCOND "
    Private Const SQL_M_CUSTCOND_SELECT As String = "SELECT L.* "
    Private Const SQL_M_CUSTCOND_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_CUSTCOND_FROM As String = "FROM $LM_MST$..M_CUSTCOND L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_DEST_TBLNM As String = "$LM_ESCAPE_MST$..M_DEST "
    Private Const SQL_M_DEST_SELECT As String = "SELECT L.* "
    Private Const SQL_M_DEST_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_DEST_FROM As String = "FROM $LM_MST$..M_DEST L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_DEST_DETAILS_TBLNM As String = "$LM_ESCAPE_MST$..M_DEST_DETAILS "
    Private Const SQL_M_DEST_DETAILS_SELECT As String = "SELECT L.* "
    Private Const SQL_M_DEST_DETAILS_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_DEST_DETAILS_FROM As String = "FROM $LM_MST$..M_DEST_DETAILS L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_DESTGOODS_TBLNM As String = "$LM_ESCAPE_MST$..M_DESTGOODS "
    Private Const SQL_M_DESTGOODS_SELECT As String = "SELECT L.* "
    Private Const SQL_M_DESTGOODS_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_DESTGOODS_FROM As String = "FROM $LM_MST$..M_DESTGOODS L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_FURI_GOODS_TBLNM As String = "$LM_ESCAPE_MST$..M_FURI_GOODS "
    Private Const SQL_M_FURI_GOODS_SELECT As String = "SELECT M.* "
    Private Const SQL_M_FURI_GOODS_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_FURI_GOODS_FROM As String = "FROM $LM_MST$..M_FURI_GOODS M LEFT JOIN $LM_MST$..M_GOODS L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.GOODS_CD_NRS = M.CD_NRS " & _
                                                    "LEFT JOIN $LM_MST$..M_GOODS L2 ON L2.NRS_BR_CD = M.NRS_BR_CD AND L2.GOODS_CD_NRS = M.CD_NRS_TO WHERE M.NRS_BR_CD = @NRS_BR_CD AND (L.CUST_CD_L = @CUST_CD_L OR L2.CUST_CD_L = @CUST_CD_L)"

    Private Const SQL_M_GOODS_TBLNM As String = "$LM_ESCAPE_MST$..M_GOODS "
    Private Const SQL_M_GOODS_SELECT As String = "SELECT L.* "
    Private Const SQL_M_GOODS_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_GOODS_FROM As String = "FROM $LM_MST$..M_GOODS L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_GOODS_DETAILS_TBLNM As String = "$LM_ESCAPE_MST$..M_GOODS_DETAILS "
    Private Const SQL_M_GOODS_DETAILS_SELECT As String = "SELECT M.* "
    Private Const SQL_M_GOODS_DETAILS_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_GOODS_DETAILS_FROM As String = "FROM $LM_MST$..M_GOODS_DETAILS M LEFT JOIN $LM_MST$..M_GOODS L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.GOODS_CD_NRS = M.GOODS_CD_NRS WHERE M.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_SAGYO_TBLNM As String = "$LM_ESCAPE_MST$..M_SAGYO "
    Private Const SQL_M_SAGYO_SELECT As String = "SELECT L.* "
    Private Const SQL_M_SAGYO_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_SAGYO_FROM As String = "FROM $LM_MST$..M_SAGYO L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_TANKA_TBLNM As String = "$LM_ESCAPE_MST$..M_TANKA "
    Private Const SQL_M_TANKA_SELECT As String = "SELECT L.* "
    Private Const SQL_M_TANKA_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_TANKA_FROM As String = "FROM $LM_MST$..M_TANKA L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_M_UNCHIN_TARIFF_SET_TBLNM As String = "$LM_ESCAPE_MST$..M_UNCHIN_TARIFF_SET "
    Private Const SQL_M_UNCHIN_TARIFF_SET_SELECT As String = "SELECT L.* "
    Private Const SQL_M_UNCHIN_TARIFF_SET_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_M_UNCHIN_TARIFF_SET_FROM As String = "FROM $LM_MST$..M_UNCHIN_TARIFF_SET L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

#End Region

#Region "LM_TRN"

#Region "B_入荷"

    Private Const SQL_B_INKA_L_TBLNM As String = " $LM_ESCAPE_TRN$..B_INKA_L "
    Private Const SQL_B_INKA_L_SELECT As String = "SELECT L.* "
    Private Const SQL_B_INKA_L_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_B_INKA_L_FROM As String = "FROM $LM_TRN$..B_INKA_L L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_B_INKA_M_TBLNM As String = " $LM_ESCAPE_TRN$..B_INKA_M "
    Private Const SQL_B_INKA_M_SELECT As String = "SELECT M.* "
    Private Const SQL_B_INKA_M_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_B_INKA_M_FROM As String = "FROM $LM_TRN$..B_INKA_M M LEFT JOIN $LM_TRN$..B_INKA_L L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.INKA_NO_L = M.INKA_NO_L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_B_INKA_S_TBLNM As String = " $LM_ESCAPE_TRN$..B_INKA_S "
    Private Const SQL_B_INKA_S_SELECT As String = "SELECT S.* "
    Private Const SQL_B_INKA_S_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_B_INKA_S_FROM As String = "FROM $LM_TRN$..B_INKA_S S LEFT JOIN $LM_TRN$..B_INKA_L L ON L.NRS_BR_CD = S.NRS_BR_CD AND L.INKA_NO_L = S.INKA_NO_L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

#End Region

#Region "C_出荷"

    Private Const SQL_C_OUTKA_L_TBLNM As String = "$LM_ESCAPE_TRN$..C_OUTKA_L "
    Private Const SQL_C_OUTKA_L_SELECT As String = "SELECT L.* "
    Private Const SQL_C_OUTKA_L_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_C_OUTKA_L_FROM As String = "FROM $LM_TRN$..C_OUTKA_L L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_C_OUTKA_M_TBLNM As String = "$LM_ESCAPE_TRN$..C_OUTKA_M "
    Private Const SQL_C_OUTKA_M_SELECT As String = "SELECT M.* "
    Private Const SQL_C_OUTKA_M_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_C_OUTKA_M_FROM As String = "FROM $LM_TRN$..C_OUTKA_M M LEFT JOIN $LM_TRN$..C_OUTKA_L L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.OUTKA_NO_L = M.OUTKA_NO_L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_C_OUTKA_S_TBLNM As String = "$LM_ESCAPE_TRN$..C_OUTKA_S "
    Private Const SQL_C_OUTKA_S_SELECT As String = "SELECT S.* "
    Private Const SQL_C_OUTKA_S_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_C_OUTKA_S_FROM As String = "FROM $LM_TRN$..C_OUTKA_S S LEFT JOIN $LM_TRN$..C_OUTKA_L L ON L.NRS_BR_CD = S.NRS_BR_CD AND L.OUTKA_NO_L = S.OUTKA_NO_L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

#End Region

#Region "D_在庫"

    Private Const SQL_D_ZAI_TRS_TBLNM As String = "$LM_ESCAPE_TRN$..D_ZAI_TRS "
    Private Const SQL_D_ZAI_TRS_SELECT As String = "SELECT L.* "
    Private Const SQL_D_ZAI_TRS_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_D_ZAI_TRS_FROM As String = "FROM $LM_TRN$..D_ZAI_TRS L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_D_IDO_TRS_TBLNM As String = "$LM_ESCAPE_TRN$..D_IDO_TRS "
    Private Const SQL_D_IDO_TRS_SELECT As String = "SELECT M.* "
    Private Const SQL_D_IDO_TRS_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_D_IDO_TRS_FROM As String = "FROM $LM_TRN$..D_IDO_TRS M LEFT JOIN $LM_TRN$..D_ZAI_TRS L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.ZAI_REC_NO = M.O_ZAI_REC_NO WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_D_IDO_HANDY_TBLNM As String = "$LM_ESCAPE_TRN$..D_IDO_HANDY "
    Private Const SQL_D_IDO_HANDY_SELECT As String = "SELECT M.* "
    Private Const SQL_D_IDO_HANDY_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_D_IDO_HANDY_FROM As String = "FROM $LM_TRN$..D_IDO_HANDY M LEFT JOIN $LM_TRN$..D_ZAI_TRS L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.ZAI_REC_NO = M.ZAI_REC_NO WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_D_WK_ZAI_PRT_TBLNM As String = "$LM_ESCAPE_TRN$..D_WK_ZAI_PRT "
    Private Const SQL_D_WK_ZAI_PRT_SELECT As String = "SELECT L.* "
    Private Const SQL_D_WK_ZAI_PRT_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_D_WK_ZAI_PRT_FROM As String = "FROM $LM_TRN$..D_WK_ZAI_PRT L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_D_ZAI_SHOGOH_TBLNM As String = "$LM_ESCAPE_TRN$..D_ZAI_SHOGOH "
    Private Const SQL_D_ZAI_SHOGOH_SELECT As String = "SELECT L.* "
    Private Const SQL_D_ZAI_SHOGOH_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_D_ZAI_SHOGOH_FROM As String = "FROM $LM_TRN$..D_ZAI_SHOGOH L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_D_ZAI_SHOGOH_CUST_TBLNM As String = "$LM_ESCAPE_TRN$..D_ZAI_SHOGOH_CUST "
    Private Const SQL_D_ZAI_SHOGOH_CUST_SELECT As String = "SELECT L.* "
    Private Const SQL_D_ZAI_SHOGOH_CUST_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_D_ZAI_SHOGOH_CUST_FROM As String = "FROM $LM_TRN$..D_ZAI_SHOGOH_CUST L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_D_ZAI_SHOGOH_CUST_SUM_TBLNM As String = "$LM_ESCAPE_TRN$..D_ZAI_SHOGOH_CUST_SUM "
    Private Const SQL_D_ZAI_SHOGOH_CUST_SUM_SELECT As String = "SELECT L.* "
    Private Const SQL_D_ZAI_SHOGOH_CUST_SUM_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_D_ZAI_SHOGOH_CUST_SUM_FROM As String = "FROM $LM_TRN$..D_ZAI_SHOGOH_CUST_SUM L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_D_ZAI_ZAN_JITSU_TBLNM As String = "$LM_ESCAPE_TRN$..D_ZAI_ZAN_JITSU "
    Private Const SQL_D_ZAI_ZAN_JITSU_SELECT As String = "SELECT L.* "
    Private Const SQL_D_ZAI_ZAN_JITSU_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_D_ZAI_ZAN_JITSU_FROM As String = "FROM $LM_TRN$..D_ZAI_ZAN_JITSU L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

#End Region

#Region "E_作業"

    Private Const SQL_E_SAGYO_TBLNM As String = "$LM_ESCAPE_TRN$..E_SAGYO "
    Private Const SQL_E_SAGYO_SELECT As String = "SELECT L.* "
    Private Const SQL_E_SAGYO_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_E_SAGYO_FROM As String = "FROM $LM_TRN$..E_SAGYO L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_E_SAGYO_SIJI_TBLNM As String = "$LM_ESCAPE_TRN$..E_SAGYO_SIJI "
    Private Const SQL_E_SAGYO_SIJI_SELECT As String = "SELECT M.* "
    Private Const SQL_E_SAGYO_SIJI_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_E_SAGYO_SIJI_FROM As String = "FROM $LM_TRN$..E_SAGYO_SIJI M LEFT JOIN $LM_TRN$..E_SAGYO L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.SAGYO_SIJI_NO = M.SAGYO_SIJI_NO WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

#End Region

#Region "F_運送"

    Private Const SQL_F_UNSO_L_TBLNM As String = "$LM_ESCAPE_TRN$..F_UNSO_L "
    Private Const SQL_F_UNSO_L_SELECT As String = "SELECT L.* "
    Private Const SQL_F_UNSO_L_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_F_UNSO_L_FROM As String = "FROM $LM_TRN$..F_UNSO_L L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_F_UNSO_M_TBLNM As String = " $LM_ESCAPE_TRN$..F_UNSO_M "
    Private Const SQL_F_UNSO_M_SELECT As String = "SELECT M.* "
    Private Const SQL_F_UNSO_M_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_F_UNSO_M_FROM As String = "FROM $LM_TRN$..F_UNSO_M M LEFT JOIN $LM_TRN$..F_UNSO_L L ON L.UNSO_NO_L = M.UNSO_NO_L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_F_UNCHIN_TRS_TBLNM As String = "$LM_ESCAPE_TRN$..F_UNCHIN_TRS "
    Private Const SQL_F_UNCHIN_TRS_SELECT As String = "SELECT L.* "
    Private Const SQL_F_UNCHIN_TRS_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_F_UNCHIN_TRS_FROM As String = "FROM $LM_TRN$..F_UNCHIN_TRS L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

#End Region

#Region "G_請求"

    Private Const SQL_G_SEKY_MEISAI_TBLNM As String = "$LM_ESCAPE_TRN$..G_SEKY_MEISAI "
    Private Const SQL_G_SEKY_MEISAI_SELECT As String = "SELECT M.* "
    Private Const SQL_G_SEKY_MEISAI_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_G_SEKY_MEISAI_FROM As String = "FROM $LM_TRN$..G_SEKY_MEISAI M LEFT JOIN $LM_MST$..M_GOODS L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.GOODS_CD_NRS = M.GOODS_CD_NRS WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_G_SEKY_MEISAI_PRT_TBLNM As String = "$LM_ESCAPE_TRN$..G_SEKY_MEISAI_PRT "
    Private Const SQL_G_SEKY_MEISAI_PRT_SELECT As String = "SELECT M.* "
    Private Const SQL_G_SEKY_MEISAI_PRT_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_G_SEKY_MEISAI_PRT_FROM As String = "FROM $LM_TRN$..G_SEKY_MEISAI_PRT M LEFT JOIN $LM_MST$..M_GOODS L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.GOODS_CD_NRS = M.GOODS_CD_NRS WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_G_SEKY_TBL_TBLNM As String = "$LM_ESCAPE_TRN$..G_SEKY_TBL "
    Private Const SQL_G_SEKY_TBL_SELECT As String = "SELECT M.* "
    Private Const SQL_G_SEKY_TBL_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_G_SEKY_TBL_FROM As String = "FROM $LM_TRN$..G_SEKY_TBL M LEFT JOIN $LM_MST$..M_GOODS L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.GOODS_CD_NRS = M.GOODS_CD_NRS WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_G_ZAIK_ZAN_TBLNM As String = "$LM_ESCAPE_TRN$..G_ZAIK_ZAN "
    Private Const SQL_G_ZAIK_ZAN_SELECT As String = "SELECT M.* "
    Private Const SQL_G_ZAIK_ZAN_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_G_ZAIK_ZAN_FROM As String = "FROM $LM_TRN$..G_ZAIK_ZAN M LEFT JOIN $LM_MST$..M_GOODS L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.GOODS_CD_NRS = M.GOODS_CD_NRS WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

#End Region

#Region "E_EDI"

    Private Const SQL_H_INKAEDI_L_TBLNM As String = " $LM_ESCAPE_TRN$..H_INKAEDI_L "
    Private Const SQL_H_INKAEDI_L_SELECT As String = "SELECT L.* "
    Private Const SQL_H_INKAEDI_L_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_H_INKAEDI_L_FROM As String = "FROM $LM_TRN$..H_INKAEDI_L L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_H_INKAEDI_M_TBLNM As String = " $LM_ESCAPE_TRN$..H_INKAEDI_M "
    Private Const SQL_H_INKAEDI_M_SELECT As String = "SELECT M.* "
    Private Const SQL_H_INKAEDI_M_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_H_INKAEDI_M_FROM As String = "FROM $LM_TRN$..H_INKAEDI_M M LEFT JOIN $LM_TRN$..H_INKAEDI_L L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.EDI_CTL_NO = M.EDI_CTL_NO WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_H_OUTKAEDI_L_TBLNM As String = " $LM_ESCAPE_TRN$..H_OUTKAEDI_L "
    Private Const SQL_H_OUTKAEDI_L_SELECT As String = "SELECT L.* "
    Private Const SQL_H_OUTKAEDI_L_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_H_OUTKAEDI_L_FROM As String = "FROM $LM_TRN$..H_OUTKAEDI_L L WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

    Private Const SQL_H_OUTKAEDI_M_TBLNM As String = " $LM_ESCAPE_TRN$..H_OUTKAEDI_M "
    Private Const SQL_H_OUTKAEDI_M_SELECT As String = "SELECT M.* "
    Private Const SQL_H_OUTKAEDI_M_SELECT_CNT As String = "SELECT COUNT(*) AS CNT "
    Private Const SQL_H_OUTKAEDI_M_FROM As String = "FROM $LM_TRN$..H_OUTKAEDI_M M LEFT JOIN $LM_TRN$..H_OUTKAEDI_L L ON L.NRS_BR_CD = M.NRS_BR_CD AND L.EDI_CTL_NO = M.EDI_CTL_NO WHERE L.NRS_BR_CD = @NRS_BR_CD AND L.CUST_CD_L = @CUST_CD_L"

#End Region


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

#Region "検索・更新処理"

    ''' <summary>
    ''' 荷主マスタ検索対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタLテーブル更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ020DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        If KBN_PROCESS_TAIHI.Equals(Me._Row.Item("PROCESS_KB").ToString()) = True Then
            Me._StrSql.Append(LMJ020DAC.SQL_FROM_TAIHI)             'SQL構築(データ抽出用From句)
            Me._StrSql.Append(LMJ020DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
            Call Me.SetConditionMasterSQL(True)                   '条件設定
        Else
            Me._StrSql.Append(LMJ020DAC.SQL_FROM_MODOSHI)             'SQL構築(データ抽出用From句)
            Me._StrSql.Append(LMJ020DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
            Call Me.SetConditionMasterSQL(True)                   '条件設定
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定をなしにする
        cmd.CommandTimeout = 0

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        If KBN_PROCESS_TAIHI.Equals(Me._Row.Item("PROCESS_KB").ToString()) = True Then
            Me._StrSql.Append(LMJ020DAC.SQL_SELECT_DATA_TAIHI)      'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMJ020DAC.SQL_FROM_TAIHI)             'SQL構築(データ抽出用From句)
            Me._StrSql.Append(LMJ020DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
            Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
            Me._StrSql.Append(LMJ020DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)
        Else
            Me._StrSql.Append(LMJ020DAC.SQL_SELECT_DATA_MODOSHI)      'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMJ020DAC.SQL_FROM_MODOSHI)             'SQL構築(データ抽出用From句)
            Me._StrSql.Append(LMJ020DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
            Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
            Me._StrSql.Append(LMJ020DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定をなしにする
        cmd.CommandTimeout = 0

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("LAST_UPD_DATE", "LAST_UPD_DATE")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("TAIHI_DATE", "TAIHI_DATE")
        map.Add("TAIHI_USER", "TAIHI_USER")
        map.Add("PROCESS_KB", "PROCESS_KB")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMJ020OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(Optional ByVal cntSQLFlg As Boolean = False)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            If cntSQLFlg = False Then
                '処理区分
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROCESS_KB", .Item("PROCESS_KB").ToString(), DBDataType.CHAR))
            End If

            '営業所
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '荷主コード大
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MAIN.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主名大
            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MAIN.CUST_NM_L LIKE @CUST_NM_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '最終更新日
            whereStr = .Item("LAST_UPD_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (MAIN.LAST_UPD_DATE <= @LAST_UPD_DATE OR MAIN.LAST_UPD_DATE IS Null)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_UPD_DATE", whereStr, DBDataType.CHAR))
            End If

            '担当者
            whereStr = .Item("TANTO_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MAIN.USER_NM LIKE @TANTO_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub


#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 入力チェック（荷主が移行可能かどうか）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InputCheckCUST(ByVal ds As DataSet) As DataSet

        Return Me.SelectCountCheckCust(ds)

    End Function


    ''' <summary>
    ''' 荷主整合性チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタLテーブル更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectCountCheckCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ020DAC.SQL_SELECT_COUNT_CHECK)     'SQL構築(カウント用Select句)
        If KBN_PROCESS_TAIHI.Equals(Me._Row("PROCESS_KB").ToString()) = True Then
            Me._StrSql.Append(LMJ020DAC.SQL_WHERE_CHECK_TAIHI)            'SQL構築(データ抽出用Where句)
        Else
            Me._StrSql.Append(LMJ020DAC.SQL_WHERE_CHECK_MODOSHI)            'SQL構築(データ抽出用Where句)
        End If
        '営業所
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定をなしにする
        cmd.CommandTimeout = 0

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", "SelectCountCheckCust", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 入力チェック（在庫が存在するか）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InputCheckZAIKO(ByVal ds As DataSet) As DataSet

        Return Me.SelectCountCheckZAIKO(ds)

    End Function

    ''' <summary>
    ''' 在庫数チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタLテーブル更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectCountCheckZAIKO(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ020DAC.SQL_SELECT_COUNT_CHECK_ZAIKO)     'SQL構築(カウント用Select句)
        '営業所
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", "SelectCountCheckZAIKO", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

#Region "元データ削除チェック"

    ''' <summary>
    ''' 無駄無駄データ削除SQL
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeletePreData(ByVal ds As DataSet) As DataSet

        Dim rtnCount As Integer = 0

        For Each row As DataRow In ds.Tables(TBLNM_TARGET_TABLES).Rows

            rtnCount = Me.DeletePreCheckSQLCreate(ds, Me.GetTableNM(row("TableNM").ToString()))
            If rtnCount = RTN_STS_ERROR Then
                Me.SetDtlLog(ds, row("TableNM").ToString())
                'エラーの場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_PREDELETE_NG
                'ヘッダも更新
                ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_TIMING_KB") = KBN_TIIMING_CHECK
                ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_RESULT_KB") = KBN_KEKKA_ERROR
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
                Me.SetMSGStore("E547", New String() {MSG_PREDELETE_NG}, ds.Tables(TBLNM_TBLIN).Rows(0).Item("CUST_CD_L").ToString())
                Exit For
            ElseIf rtnCount = RTN_STS_SUCCESS Or rtnCount = RTN_STS_NOTHING Then
                '削除データがないので何もしない
                Continue For
            End If

            rtnCount = Me.DeleteModoshiData(ds, Me.GetTableNM(row("TableNM").ToString()))
            'ログ出力
            If rtnCount <> RTN_STS_ERROR And rtnCount <> RTN_STS_NOTHING And rtnCount <> 0 Then
                Me.SetDtlLog(ds, row("TableNM").ToString())
                '正常の場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("CNT") = rtnCount
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_PREDELETE_OK
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 無駄無駄データ削除SQL
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>各種テーブル削除SQLの構築・発行</remarks>
    Private Function DeletePreCheckSQLCreate(ByVal ds As DataSet, ByVal tgtTbl As TARGET_TABLES) As Integer

        If TARGET_TABLES.NON = tgtTbl = True Then
            Return RTN_STS_NOTHING
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me.GetDelecteCheckSQL(tgtTbl, DeleteCheckMode.PreCheck), Me._Row.Item("NRS_BR_CD").ToString()))

        'タイムアウトの設定をなしにする
        cmd.CommandTimeout = 0

        'パラメータ設定
        'Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetTablesComParameter(Me._Row, Me._SqlPrmList, tgtTbl)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", String.Concat("DeleteCheck_", tgtTbl.ToString()), cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        Dim successFlg As Integer
        reader.Read()
        successFlg = Convert.ToInt32(reader("SUCCESS_FLG"))
        reader.Close()

        If successFlg = -1 Then
            Return RTN_STS_ERROR
        ElseIf successFlg = 1 Then
            Return RTN_STS_DELPROCESS
        Else
            Return RTN_STS_SUCCESS
        End If

    End Function


#End Region

#End Region

#Region "退避処理"

#Region "Insert"

    ''' <summary>
    ''' データ退避処理（退避先への登録）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function EscapeData(ByVal ds As DataSet) As DataSet

        Dim rtnCount As Integer = 0

        For Each row As DataRow In ds.Tables(TBLNM_TARGET_TABLES).Rows

            rtnCount = Me.InsertEscapeData(ds, Me.GetTableNM(row("TableNM").ToString()))

            'ログ出力
            If rtnCount = RTN_STS_ERROR Then
                Me.SetDtlLog(ds, row("TableNM").ToString())
                'エラーの場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_ERR_INSERT
                'ヘッダも更新
                If "M".Equals(row("TableIDGroup").ToString()) = True Then
                    ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_TIMING_KB") = KBN_TIIMING_MST
                Else
                    ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_TIMING_KB") = KBN_TIIMING_TRN
                End If
                ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_RESULT_KB") = KBN_KEKKA_ERROR
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
                Exit For
            ElseIf rtnCount = RTN_STS_NOTHING Then
                'スルー
            Else
                Me.SetDtlLog(ds, row("TableNM").ToString())
                '正常の場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("CNT") = rtnCount
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_SUCCESS_INSERT
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 退避データのINSERT
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>各種テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertEscapeData(ByVal ds As DataSet, ByVal tgtTbl As TARGET_TABLES) As Integer

        If TARGET_TABLES.NON = tgtTbl = True Then
            Return RTN_STS_NOTHING
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me.GetInsertSQL(tgtTbl), Me._Row.Item("NRS_BR_CD").ToString()))

        '件数が膨大なため、タイムアウトを設定しない
        cmd.CommandTimeout = 30000

        'パラメータ設定
        'Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetTablesComParameter(Me._Row, Me._SqlPrmList, tgtTbl)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", String.Concat("Insert", tgtTbl.ToString()), cmd)

        'SQLの発行
        Return MyBase.GetInsertResult(cmd)

    End Function


    ''' <summary>
    ''' InsertSQL取得
    ''' </summary>
    ''' <param name="tbtTbl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInsertSQL(ByVal tbtTbl As TARGET_TABLES) As String

        Dim insertSQL As String = String.Empty

        'もう１つ先　DACで判定する
        Select Case tbtTbl
            'LM_MST
            Case TARGET_TABLES.M_COA
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_COA_TBLNM, SQL_M_COA_SELECT, SQL_M_COA_FROM)

            Case TARGET_TABLES.M_COACONFIG
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_COACONFIG_TBLNM, SQL_M_COACONFIG_SELECT, SQL_M_COACONFIG_FROM)

            Case TARGET_TABLES.M_CUSTCOND
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_CUSTCOND_TBLNM, SQL_M_CUSTCOND_SELECT, SQL_M_CUSTCOND_FROM)

            Case TARGET_TABLES.M_DEST
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_DEST_TBLNM, SQL_M_DEST_SELECT, SQL_M_DEST_FROM)

            Case TARGET_TABLES.M_DEST_DETAILS
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_DEST_DETAILS_TBLNM, SQL_M_DEST_DETAILS_SELECT, SQL_M_DEST_DETAILS_FROM)

            Case TARGET_TABLES.M_DESTGOODS
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_DESTGOODS_TBLNM, SQL_M_DESTGOODS_SELECT, SQL_M_DESTGOODS_FROM)

            Case TARGET_TABLES.M_FURI_GOODS
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_FURI_GOODS_TBLNM, SQL_M_FURI_GOODS_SELECT, SQL_M_FURI_GOODS_FROM)

            Case TARGET_TABLES.M_GOODS
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_GOODS_TBLNM, SQL_M_GOODS_SELECT, SQL_M_GOODS_FROM)

            Case TARGET_TABLES.M_GOODS_DETAILS
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_GOODS_DETAILS_TBLNM, SQL_M_GOODS_DETAILS_SELECT, SQL_M_GOODS_DETAILS_FROM)

            Case TARGET_TABLES.M_SAGYO
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_SAGYO_TBLNM, SQL_M_SAGYO_SELECT, SQL_M_SAGYO_FROM)

            Case TARGET_TABLES.M_TANKA
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_TANKA_TBLNM, SQL_M_TANKA_SELECT, SQL_M_TANKA_FROM)

            Case TARGET_TABLES.M_UNCHIN_TARIFF_SET
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_M_UNCHIN_TARIFF_SET_TBLNM, SQL_M_UNCHIN_TARIFF_SET_SELECT, SQL_M_UNCHIN_TARIFF_SET_FROM)


                'LM_TRN
            Case TARGET_TABLES.B_INKA_L
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_B_INKA_L_TBLNM, SQL_B_INKA_L_SELECT, SQL_B_INKA_L_FROM)

            Case TARGET_TABLES.B_INKA_M
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_B_INKA_M_TBLNM, SQL_B_INKA_M_SELECT, SQL_B_INKA_M_FROM)

            Case TARGET_TABLES.B_INKA_S
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_B_INKA_S_TBLNM, SQL_B_INKA_S_SELECT, SQL_B_INKA_S_FROM)

            Case TARGET_TABLES.C_OUTKA_L
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_C_OUTKA_L_TBLNM, SQL_C_OUTKA_L_SELECT, SQL_C_OUTKA_L_FROM)

            Case TARGET_TABLES.C_OUTKA_M
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_C_OUTKA_M_TBLNM, SQL_C_OUTKA_M_SELECT, SQL_C_OUTKA_M_FROM)

            Case TARGET_TABLES.C_OUTKA_S
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_C_OUTKA_S_TBLNM, SQL_C_OUTKA_S_SELECT, SQL_C_OUTKA_S_FROM)

            Case TARGET_TABLES.D_IDO_HANDY
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_D_IDO_HANDY_TBLNM, SQL_D_IDO_HANDY_SELECT, SQL_D_IDO_HANDY_FROM)

            Case TARGET_TABLES.D_IDO_TRS
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_D_IDO_TRS_TBLNM, SQL_D_IDO_TRS_SELECT, SQL_D_IDO_TRS_FROM)

            Case TARGET_TABLES.D_WK_ZAI_PRT
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_D_WK_ZAI_PRT_TBLNM, SQL_D_WK_ZAI_PRT_SELECT, SQL_D_WK_ZAI_PRT_FROM)

            Case TARGET_TABLES.D_ZAI_SHOGOH
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_D_ZAI_SHOGOH_TBLNM, SQL_D_ZAI_SHOGOH_SELECT, SQL_D_ZAI_SHOGOH_FROM)

            Case TARGET_TABLES.D_ZAI_SHOGOH_CUST
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_D_ZAI_SHOGOH_CUST_TBLNM, SQL_D_ZAI_SHOGOH_CUST_SELECT, SQL_D_ZAI_SHOGOH_CUST_FROM)

            Case TARGET_TABLES.D_ZAI_SHOGOH_CUST_SUM
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_D_ZAI_SHOGOH_CUST_SUM_TBLNM, SQL_D_ZAI_SHOGOH_CUST_SUM_SELECT, SQL_D_ZAI_SHOGOH_CUST_SUM_FROM)

            Case TARGET_TABLES.D_ZAI_TRS
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_D_ZAI_TRS_TBLNM, SQL_D_ZAI_TRS_SELECT, SQL_D_ZAI_TRS_FROM)

            Case TARGET_TABLES.D_ZAI_ZAN_JITSU
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_D_ZAI_ZAN_JITSU_TBLNM, SQL_D_ZAI_ZAN_JITSU_SELECT, SQL_D_ZAI_ZAN_JITSU_FROM)

            Case TARGET_TABLES.E_SAGYO
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_E_SAGYO_TBLNM, SQL_E_SAGYO_SELECT, SQL_E_SAGYO_FROM)

            Case TARGET_TABLES.E_SAGYO_SIJI
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_E_SAGYO_SIJI_TBLNM, SQL_E_SAGYO_SIJI_SELECT, SQL_E_SAGYO_SIJI_FROM)

            Case TARGET_TABLES.F_UNCHIN_TRS
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_F_UNCHIN_TRS_TBLNM, SQL_F_UNCHIN_TRS_SELECT, SQL_F_UNCHIN_TRS_FROM)

            Case TARGET_TABLES.F_UNSO_L
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_F_UNSO_L_TBLNM, SQL_F_UNSO_L_SELECT, SQL_F_UNSO_L_FROM)

            Case TARGET_TABLES.F_UNSO_M
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_F_UNSO_M_TBLNM, SQL_F_UNSO_M_SELECT, SQL_F_UNSO_M_FROM)

            Case TARGET_TABLES.G_SEKY_MEISAI
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_G_SEKY_MEISAI_TBLNM, SQL_G_SEKY_MEISAI_SELECT, SQL_G_SEKY_MEISAI_FROM)

            Case TARGET_TABLES.G_SEKY_MEISAI_PRT
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_G_SEKY_MEISAI_PRT_TBLNM, SQL_G_SEKY_MEISAI_PRT_SELECT, SQL_G_SEKY_MEISAI_PRT_FROM)

            Case TARGET_TABLES.G_SEKY_TBL
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_G_SEKY_TBL_TBLNM, SQL_G_SEKY_TBL_SELECT, SQL_G_SEKY_TBL_FROM)

            Case TARGET_TABLES.G_ZAIK_ZAN
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_G_ZAIK_ZAN_TBLNM, SQL_G_ZAIK_ZAN_SELECT, SQL_G_ZAIK_ZAN_FROM)

            Case TARGET_TABLES.H_INKAEDI_L
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_H_INKAEDI_L_TBLNM, SQL_H_INKAEDI_L_SELECT, SQL_H_INKAEDI_L_FROM)

            Case TARGET_TABLES.H_INKAEDI_M
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_H_INKAEDI_M_TBLNM, SQL_H_INKAEDI_M_SELECT, SQL_H_INKAEDI_M_FROM)

            Case TARGET_TABLES.H_OUTKAEDI_L
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_H_OUTKAEDI_L_TBLNM, SQL_H_OUTKAEDI_L_SELECT, SQL_H_OUTKAEDI_L_FROM)

            Case TARGET_TABLES.H_OUTKAEDI_M
                insertSQL = String.Concat(SQL_INSERTINTO, SQL_H_OUTKAEDI_M_TBLNM, SQL_H_OUTKAEDI_M_SELECT, SQL_H_OUTKAEDI_M_FROM)

        End Select

        Return insertSQL

    End Function

    ''' <summary>
    ''' SelectSQL取得
    ''' </summary>
    ''' <param name="tbtTbl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDelecteCheckSQL(ByVal tbtTbl As TARGET_TABLES, ByVal mode As DeleteCheckMode) As String

        Dim selectSQL As String = String.Empty
        Dim selectMain As String = String.Empty

        If mode = DeleteCheckMode.DelCheck Then
            selectMain = SQL_DELCHECK
        Else
            selectMain = SQL_PREDELCHECK
        End If

        'もう１つ先　DACで判定する
        Select Case tbtTbl
            ''LM_MST
            Case TARGET_TABLES.M_COA
                selectSQL = String.Concat(selectMain, "(", SQL_M_COA_SELECT_CNT, Me.SetSchemaNm(SQL_M_COA_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_COA_SELECT_CNT, Me.SetSchemaNm(SQL_M_COA_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_COACONFIG
                selectSQL = String.Concat(selectMain, "(", SQL_M_COACONFIG_SELECT_CNT, Me.SetSchemaNm(SQL_M_COACONFIG_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_COACONFIG_SELECT_CNT, Me.SetSchemaNm(SQL_M_COACONFIG_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_CUSTCOND
                selectSQL = String.Concat(selectMain, "(", SQL_M_CUSTCOND_SELECT_CNT, Me.SetSchemaNm(SQL_M_CUSTCOND_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_CUSTCOND_SELECT_CNT, Me.SetSchemaNm(SQL_M_CUSTCOND_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_DEST
                selectSQL = String.Concat(selectMain, "(", SQL_M_DEST_SELECT_CNT, Me.SetSchemaNm(SQL_M_DEST_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_DEST_SELECT_CNT, Me.SetSchemaNm(SQL_M_DEST_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_DEST_DETAILS
                selectSQL = String.Concat(selectMain, "(", SQL_M_DEST_DETAILS_SELECT_CNT, Me.SetSchemaNm(SQL_M_DEST_DETAILS_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_DEST_DETAILS_SELECT_CNT, Me.SetSchemaNm(SQL_M_DEST_DETAILS_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_DESTGOODS
                selectSQL = String.Concat(selectMain, "(", SQL_M_DESTGOODS_SELECT_CNT, Me.SetSchemaNm(SQL_M_DESTGOODS_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_DESTGOODS_SELECT_CNT, Me.SetSchemaNm(SQL_M_DESTGOODS_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_FURI_GOODS
                selectSQL = String.Concat(selectMain, "(", SQL_M_FURI_GOODS_SELECT_CNT, Me.SetSchemaNm(SQL_M_FURI_GOODS_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_FURI_GOODS_SELECT_CNT, Me.SetSchemaNm(SQL_M_FURI_GOODS_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_GOODS
                selectSQL = String.Concat(selectMain, "(", SQL_M_GOODS_SELECT_CNT, Me.SetSchemaNm(SQL_M_GOODS_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_GOODS_SELECT_CNT, Me.SetSchemaNm(SQL_M_GOODS_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_GOODS_DETAILS
                selectSQL = String.Concat(selectMain, "(", SQL_M_GOODS_DETAILS_SELECT_CNT, Me.SetSchemaNm(SQL_M_GOODS_DETAILS_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_GOODS_DETAILS_SELECT_CNT, Me.SetSchemaNm(SQL_M_GOODS_DETAILS_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_SAGYO
                selectSQL = String.Concat(selectMain, "(", SQL_M_SAGYO_SELECT_CNT, Me.SetSchemaNm(SQL_M_SAGYO_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_SAGYO_SELECT_CNT, Me.SetSchemaNm(SQL_M_SAGYO_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_TANKA
                selectSQL = String.Concat(selectMain, "(", SQL_M_TANKA_SELECT_CNT, Me.SetSchemaNm(SQL_M_TANKA_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_TANKA_SELECT_CNT, Me.SetSchemaNm(SQL_M_TANKA_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.M_UNCHIN_TARIFF_SET
                selectSQL = String.Concat(selectMain, "(", SQL_M_UNCHIN_TARIFF_SET_SELECT_CNT, Me.SetSchemaNm(SQL_M_UNCHIN_TARIFF_SET_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_M_UNCHIN_TARIFF_SET_SELECT_CNT, Me.SetSchemaNm(SQL_M_UNCHIN_TARIFF_SET_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")


                'LM_TRN
            Case TARGET_TABLES.B_INKA_L
                selectSQL = String.Concat(selectMain, "(", SQL_B_INKA_L_SELECT_CNT, Me.SetSchemaNm(SQL_B_INKA_L_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_B_INKA_L_SELECT_CNT, Me.SetSchemaNm(SQL_B_INKA_L_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.B_INKA_M
                selectSQL = String.Concat(selectMain, "(", SQL_B_INKA_M_SELECT_CNT, Me.SetSchemaNm(SQL_B_INKA_M_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_B_INKA_M_SELECT_CNT, Me.SetSchemaNm(SQL_B_INKA_M_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.B_INKA_S
                selectSQL = String.Concat(selectMain, "(", SQL_B_INKA_S_SELECT_CNT, Me.SetSchemaNm(SQL_B_INKA_S_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_B_INKA_S_SELECT_CNT, Me.SetSchemaNm(SQL_B_INKA_S_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.C_OUTKA_L
                selectSQL = String.Concat(selectMain, "(", SQL_C_OUTKA_L_SELECT_CNT, Me.SetSchemaNm(SQL_C_OUTKA_L_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_C_OUTKA_L_SELECT_CNT, Me.SetSchemaNm(SQL_C_OUTKA_L_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.C_OUTKA_M
                selectSQL = String.Concat(selectMain, "(", SQL_C_OUTKA_M_SELECT_CNT, Me.SetSchemaNm(SQL_C_OUTKA_M_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_C_OUTKA_M_SELECT_CNT, Me.SetSchemaNm(SQL_C_OUTKA_M_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.C_OUTKA_S
                selectSQL = String.Concat(selectMain, "(", SQL_C_OUTKA_S_SELECT_CNT, Me.SetSchemaNm(SQL_C_OUTKA_S_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_C_OUTKA_S_SELECT_CNT, Me.SetSchemaNm(SQL_C_OUTKA_S_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.D_IDO_HANDY
                selectSQL = String.Concat(selectMain, "(", SQL_D_IDO_HANDY_SELECT_CNT, Me.SetSchemaNm(SQL_D_IDO_HANDY_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_D_IDO_HANDY_SELECT_CNT, Me.SetSchemaNm(SQL_D_IDO_HANDY_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.D_IDO_TRS
                selectSQL = String.Concat(selectMain, "(", SQL_D_IDO_TRS_SELECT_CNT, Me.SetSchemaNm(SQL_D_IDO_TRS_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_D_IDO_TRS_SELECT_CNT, Me.SetSchemaNm(SQL_D_IDO_TRS_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.D_WK_ZAI_PRT
                selectSQL = String.Concat(selectMain, "(", SQL_D_WK_ZAI_PRT_SELECT_CNT, Me.SetSchemaNm(SQL_D_WK_ZAI_PRT_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_D_WK_ZAI_PRT_SELECT_CNT, Me.SetSchemaNm(SQL_D_WK_ZAI_PRT_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.D_ZAI_SHOGOH
                selectSQL = String.Concat(selectMain, "(", SQL_D_ZAI_SHOGOH_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_SHOGOH_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_D_ZAI_SHOGOH_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_SHOGOH_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.D_ZAI_SHOGOH_CUST
                selectSQL = String.Concat(selectMain, "(", SQL_D_ZAI_SHOGOH_CUST_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_SHOGOH_CUST_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_D_ZAI_SHOGOH_CUST_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_SHOGOH_CUST_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.D_ZAI_SHOGOH_CUST_SUM
                selectSQL = String.Concat(selectMain, "(", SQL_D_ZAI_SHOGOH_CUST_SUM_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_SHOGOH_CUST_SUM_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_D_ZAI_SHOGOH_CUST_SUM_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_SHOGOH_CUST_SUM_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.D_ZAI_TRS
                selectSQL = String.Concat(selectMain, "(", SQL_D_ZAI_TRS_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_TRS_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_D_ZAI_TRS_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_TRS_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.D_ZAI_ZAN_JITSU
                selectSQL = String.Concat(selectMain, "(", SQL_D_ZAI_ZAN_JITSU_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_ZAN_JITSU_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_D_ZAI_ZAN_JITSU_SELECT_CNT, Me.SetSchemaNm(SQL_D_ZAI_ZAN_JITSU_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.E_SAGYO
                selectSQL = String.Concat(selectMain, "(", SQL_E_SAGYO_SELECT_CNT, Me.SetSchemaNm(SQL_E_SAGYO_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_E_SAGYO_SELECT_CNT, Me.SetSchemaNm(SQL_E_SAGYO_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.E_SAGYO_SIJI
                selectSQL = String.Concat(selectMain, "(", SQL_E_SAGYO_SIJI_SELECT_CNT, Me.SetSchemaNm(SQL_E_SAGYO_SIJI_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_E_SAGYO_SIJI_SELECT_CNT, Me.SetSchemaNm(SQL_E_SAGYO_SIJI_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.F_UNCHIN_TRS
                selectSQL = String.Concat(selectMain, "(", SQL_F_UNCHIN_TRS_SELECT_CNT, Me.SetSchemaNm(SQL_F_UNCHIN_TRS_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_F_UNCHIN_TRS_SELECT_CNT, Me.SetSchemaNm(SQL_F_UNCHIN_TRS_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.F_UNSO_L
                selectSQL = String.Concat(selectMain, "(", SQL_F_UNSO_L_SELECT_CNT, Me.SetSchemaNm(SQL_F_UNSO_L_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_F_UNSO_L_SELECT_CNT, Me.SetSchemaNm(SQL_F_UNSO_L_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.F_UNSO_M
                selectSQL = String.Concat(selectMain, "(", SQL_F_UNSO_M_SELECT_CNT, Me.SetSchemaNm(SQL_F_UNSO_M_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_F_UNSO_M_SELECT_CNT, Me.SetSchemaNm(SQL_F_UNSO_M_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.G_SEKY_MEISAI
                selectSQL = String.Concat(selectMain, "(", SQL_G_SEKY_MEISAI_SELECT_CNT, Me.SetSchemaNm(SQL_G_SEKY_MEISAI_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_G_SEKY_MEISAI_SELECT_CNT, Me.SetSchemaNm(SQL_G_SEKY_MEISAI_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.G_SEKY_MEISAI_PRT
                selectSQL = String.Concat(selectMain, "(", SQL_G_SEKY_MEISAI_PRT_SELECT_CNT, Me.SetSchemaNm(SQL_G_SEKY_MEISAI_PRT_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_G_SEKY_MEISAI_PRT_SELECT_CNT, Me.SetSchemaNm(SQL_G_SEKY_MEISAI_PRT_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.G_SEKY_TBL
                selectSQL = String.Concat(selectMain, "(", SQL_G_SEKY_TBL_SELECT_CNT, Me.SetSchemaNm(SQL_G_SEKY_TBL_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_G_SEKY_TBL_SELECT_CNT, Me.SetSchemaNm(SQL_G_SEKY_TBL_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.G_ZAIK_ZAN
                selectSQL = String.Concat(selectMain, "(", SQL_G_ZAIK_ZAN_SELECT_CNT, Me.SetSchemaNm(SQL_G_ZAIK_ZAN_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_G_ZAIK_ZAN_SELECT_CNT, Me.SetSchemaNm(SQL_G_ZAIK_ZAN_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.H_INKAEDI_L
                selectSQL = String.Concat(selectMain, "(", SQL_H_INKAEDI_L_SELECT_CNT, Me.SetSchemaNm(SQL_H_INKAEDI_L_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_H_INKAEDI_L_SELECT_CNT, Me.SetSchemaNm(SQL_H_INKAEDI_L_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.H_INKAEDI_M
                selectSQL = String.Concat(selectMain, "(", SQL_H_INKAEDI_M_SELECT_CNT, Me.SetSchemaNm(SQL_H_INKAEDI_M_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_H_INKAEDI_M_SELECT_CNT, Me.SetSchemaNm(SQL_H_INKAEDI_M_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.H_OUTKAEDI_L
                selectSQL = String.Concat(selectMain, "(", SQL_H_OUTKAEDI_L_SELECT_CNT, Me.SetSchemaNm(SQL_H_OUTKAEDI_L_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_H_OUTKAEDI_L_SELECT_CNT, Me.SetSchemaNm(SQL_H_OUTKAEDI_L_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")

            Case TARGET_TABLES.H_OUTKAEDI_M
                selectSQL = String.Concat(selectMain, "(", SQL_H_OUTKAEDI_M_SELECT_CNT, Me.SetSchemaNm(SQL_H_OUTKAEDI_M_FROM, Me._Row.Item("NRS_BR_CD").ToString()), ") MOTO ,(", SQL_H_OUTKAEDI_M_SELECT_CNT, Me.SetSchemaNm(SQL_H_OUTKAEDI_M_FROM, Me._Row.Item("NRS_BR_CD").ToString(), True), ") BK ")


        End Select

        Return selectSQL

    End Function

    ''' <summary>
    ''' DeleteSQL取得
    ''' </summary>
    ''' <param name="tbtTbl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDelecteSQL(ByVal tbtTbl As TARGET_TABLES) As String

        Dim deleteSQL As String = String.Empty

        'もう１つ先　DACで判定する
        Select Case tbtTbl
            ''LM_MST
            Case TARGET_TABLES.M_COA
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_COA_TBLNM, SQL_M_COA_FROM)

            Case TARGET_TABLES.M_COACONFIG
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_COACONFIG_TBLNM, SQL_M_COACONFIG_FROM)

            Case TARGET_TABLES.M_CUSTCOND
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_CUSTCOND_TBLNM, SQL_M_CUSTCOND_FROM)

            Case TARGET_TABLES.M_DEST
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_DEST_TBLNM, SQL_M_DEST_FROM)

            Case TARGET_TABLES.M_DEST_DETAILS
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_DEST_DETAILS_TBLNM, SQL_M_DEST_DETAILS_FROM)

            Case TARGET_TABLES.M_DESTGOODS
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_DESTGOODS_TBLNM, SQL_M_DESTGOODS_FROM)

            Case TARGET_TABLES.M_FURI_GOODS
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_FURI_GOODS_TBLNM, SQL_M_FURI_GOODS_FROM)

            Case TARGET_TABLES.M_GOODS
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_GOODS_TBLNM, SQL_M_GOODS_FROM)

            Case TARGET_TABLES.M_GOODS_DETAILS
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_GOODS_DETAILS_TBLNM, SQL_M_GOODS_DETAILS_FROM)

            Case TARGET_TABLES.M_SAGYO
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_SAGYO_TBLNM, SQL_M_SAGYO_FROM)

            Case TARGET_TABLES.M_TANKA
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_TANKA_TBLNM, SQL_M_TANKA_FROM)

            Case TARGET_TABLES.M_UNCHIN_TARIFF_SET
                deleteSQL = String.Concat(SQL_DELETE, SQL_M_UNCHIN_TARIFF_SET_TBLNM, SQL_M_UNCHIN_TARIFF_SET_FROM)


                'LM_TRN
            Case TARGET_TABLES.B_INKA_L
                deleteSQL = String.Concat(SQL_DELETE, SQL_B_INKA_L_TBLNM, SQL_B_INKA_L_FROM)

            Case TARGET_TABLES.B_INKA_M
                deleteSQL = String.Concat(SQL_DELETE, SQL_B_INKA_M_TBLNM, SQL_B_INKA_M_FROM)

            Case TARGET_TABLES.B_INKA_S
                deleteSQL = String.Concat(SQL_DELETE, SQL_B_INKA_S_TBLNM, SQL_B_INKA_S_FROM)
            Case TARGET_TABLES.C_OUTKA_L
                deleteSQL = String.Concat(SQL_DELETE, SQL_C_OUTKA_L_TBLNM, SQL_C_OUTKA_L_FROM)

            Case TARGET_TABLES.C_OUTKA_M
                deleteSQL = String.Concat(SQL_DELETE, SQL_C_OUTKA_M_TBLNM, SQL_C_OUTKA_M_FROM)

            Case TARGET_TABLES.C_OUTKA_S
                deleteSQL = String.Concat(SQL_DELETE, SQL_C_OUTKA_S_TBLNM, SQL_C_OUTKA_S_FROM)

            Case TARGET_TABLES.D_IDO_HANDY
                deleteSQL = String.Concat(SQL_DELETE, SQL_D_IDO_HANDY_TBLNM, SQL_D_IDO_HANDY_FROM)

            Case TARGET_TABLES.D_IDO_TRS
                deleteSQL = String.Concat(SQL_DELETE, SQL_D_IDO_TRS_TBLNM, SQL_D_IDO_TRS_FROM)

            Case TARGET_TABLES.D_WK_ZAI_PRT
                deleteSQL = String.Concat(SQL_DELETE, SQL_D_WK_ZAI_PRT_TBLNM, SQL_D_WK_ZAI_PRT_FROM)

            Case TARGET_TABLES.D_ZAI_SHOGOH
                deleteSQL = String.Concat(SQL_DELETE, SQL_D_ZAI_SHOGOH_TBLNM, SQL_D_ZAI_SHOGOH_FROM)

            Case TARGET_TABLES.D_ZAI_SHOGOH_CUST
                deleteSQL = String.Concat(SQL_DELETE, SQL_D_ZAI_SHOGOH_CUST_TBLNM, SQL_D_ZAI_SHOGOH_CUST_FROM)

            Case TARGET_TABLES.D_ZAI_SHOGOH_CUST_SUM
                deleteSQL = String.Concat(SQL_DELETE, SQL_D_ZAI_SHOGOH_CUST_SUM_TBLNM, SQL_D_ZAI_SHOGOH_CUST_SUM_FROM)

            Case TARGET_TABLES.D_ZAI_TRS
                deleteSQL = String.Concat(SQL_DELETE, SQL_D_ZAI_TRS_TBLNM, SQL_D_ZAI_TRS_FROM)

            Case TARGET_TABLES.D_ZAI_ZAN_JITSU
                deleteSQL = String.Concat(SQL_DELETE, SQL_D_ZAI_ZAN_JITSU_TBLNM, SQL_D_ZAI_ZAN_JITSU_FROM)

            Case TARGET_TABLES.E_SAGYO
                deleteSQL = String.Concat(SQL_DELETE, SQL_E_SAGYO_TBLNM, SQL_E_SAGYO_FROM)

            Case TARGET_TABLES.E_SAGYO_SIJI
                deleteSQL = String.Concat(SQL_DELETE, SQL_E_SAGYO_SIJI_TBLNM, SQL_E_SAGYO_SIJI_FROM)

            Case TARGET_TABLES.F_UNCHIN_TRS
                deleteSQL = String.Concat(SQL_DELETE, SQL_F_UNCHIN_TRS_TBLNM, SQL_F_UNCHIN_TRS_FROM)

            Case TARGET_TABLES.F_UNSO_L
                deleteSQL = String.Concat(SQL_DELETE, SQL_F_UNSO_L_TBLNM, SQL_F_UNSO_L_FROM)

            Case TARGET_TABLES.F_UNSO_M
                deleteSQL = String.Concat(SQL_DELETE, SQL_F_UNSO_M_TBLNM, SQL_F_UNSO_M_FROM)

            Case TARGET_TABLES.G_SEKY_MEISAI
                deleteSQL = String.Concat(SQL_DELETE, SQL_G_SEKY_MEISAI_TBLNM, SQL_G_SEKY_MEISAI_FROM)

            Case TARGET_TABLES.G_SEKY_MEISAI_PRT
                deleteSQL = String.Concat(SQL_DELETE, SQL_G_SEKY_MEISAI_PRT_TBLNM, SQL_G_SEKY_MEISAI_PRT_FROM)

            Case TARGET_TABLES.G_SEKY_TBL
                deleteSQL = String.Concat(SQL_DELETE, SQL_G_SEKY_TBL_TBLNM, SQL_G_SEKY_TBL_FROM)

            Case TARGET_TABLES.G_ZAIK_ZAN
                deleteSQL = String.Concat(SQL_DELETE, SQL_G_ZAIK_ZAN_TBLNM, SQL_G_ZAIK_ZAN_FROM)

            Case TARGET_TABLES.H_INKAEDI_L
                deleteSQL = String.Concat(SQL_DELETE, SQL_H_INKAEDI_L_TBLNM, SQL_H_INKAEDI_L_FROM)

            Case TARGET_TABLES.H_INKAEDI_M
                deleteSQL = String.Concat(SQL_DELETE, SQL_H_INKAEDI_M_TBLNM, SQL_H_INKAEDI_M_FROM)

            Case TARGET_TABLES.H_OUTKAEDI_L
                deleteSQL = String.Concat(SQL_DELETE, SQL_H_OUTKAEDI_L_TBLNM, SQL_H_OUTKAEDI_L_FROM)

            Case TARGET_TABLES.H_OUTKAEDI_M
                deleteSQL = String.Concat(SQL_DELETE, SQL_H_OUTKAEDI_M_TBLNM, SQL_H_OUTKAEDI_M_FROM)

        End Select

        Return deleteSQL

    End Function

#End Region

#Region "対象テーブル取得"

    ''' <summary>
    ''' 対象テーブル
    ''' </summary>
    ''' <param name="tableNM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTableNM(ByVal tableNM As String) As TARGET_TABLES

        Dim tgtTbl As TARGET_TABLES = TARGET_TABLES.NON

        Select Case tableNM
            'LM_MST
            Case TARGET_TABLES.M_COA.ToString()
                tgtTbl = TARGET_TABLES.M_COA

            Case TARGET_TABLES.M_COACONFIG.ToString()
                tgtTbl = TARGET_TABLES.M_COACONFIG

            Case TARGET_TABLES.M_CUSTCOND.ToString()
                tgtTbl = TARGET_TABLES.M_CUSTCOND

            Case TARGET_TABLES.M_DEST.ToString()
                tgtTbl = TARGET_TABLES.M_DEST

            Case TARGET_TABLES.M_DEST_DETAILS.ToString()
                tgtTbl = TARGET_TABLES.M_DEST_DETAILS

            Case TARGET_TABLES.M_DESTGOODS.ToString()
                tgtTbl = TARGET_TABLES.M_DESTGOODS

            Case TARGET_TABLES.M_FURI_GOODS.ToString()
                tgtTbl = TARGET_TABLES.M_FURI_GOODS

            Case TARGET_TABLES.M_GOODS.ToString()
                tgtTbl = TARGET_TABLES.M_GOODS

            Case TARGET_TABLES.M_GOODS_DETAILS.ToString()
                tgtTbl = TARGET_TABLES.M_GOODS_DETAILS

            Case TARGET_TABLES.M_SAGYO.ToString()
                tgtTbl = TARGET_TABLES.M_SAGYO

            Case TARGET_TABLES.M_TANKA.ToString()
                tgtTbl = TARGET_TABLES.M_TANKA

            Case TARGET_TABLES.M_UNCHIN_TARIFF_SET.ToString()
                tgtTbl = TARGET_TABLES.M_UNCHIN_TARIFF_SET

            Case TARGET_TABLES.B_INKA_L.ToString()
                tgtTbl = TARGET_TABLES.B_INKA_L

            Case TARGET_TABLES.B_INKA_M.ToString()
                tgtTbl = TARGET_TABLES.B_INKA_M

            Case TARGET_TABLES.B_INKA_S.ToString()
                tgtTbl = TARGET_TABLES.B_INKA_S

            Case TARGET_TABLES.C_OUTKA_L.ToString()
                tgtTbl = TARGET_TABLES.C_OUTKA_L

            Case TARGET_TABLES.C_OUTKA_M.ToString()
                tgtTbl = TARGET_TABLES.C_OUTKA_M

            Case TARGET_TABLES.C_OUTKA_S.ToString()
                tgtTbl = TARGET_TABLES.C_OUTKA_S

            Case TARGET_TABLES.D_IDO_HANDY.ToString()
                tgtTbl = TARGET_TABLES.D_IDO_HANDY

            Case TARGET_TABLES.D_IDO_TRS.ToString()
                tgtTbl = TARGET_TABLES.D_IDO_TRS

            Case TARGET_TABLES.D_WK_ZAI_PRT.ToString()
                tgtTbl = TARGET_TABLES.D_WK_ZAI_PRT

            Case TARGET_TABLES.D_ZAI_SHOGOH.ToString()
                tgtTbl = TARGET_TABLES.D_ZAI_SHOGOH

            Case TARGET_TABLES.D_ZAI_SHOGOH_CUST.ToString()
                tgtTbl = TARGET_TABLES.D_ZAI_SHOGOH_CUST

            Case TARGET_TABLES.D_ZAI_SHOGOH_CUST_SUM.ToString()
                tgtTbl = TARGET_TABLES.D_ZAI_SHOGOH_CUST_SUM

            Case TARGET_TABLES.D_ZAI_TRS.ToString()
                tgtTbl = TARGET_TABLES.D_ZAI_TRS

            Case TARGET_TABLES.D_ZAI_ZAN_JITSU.ToString()
                tgtTbl = TARGET_TABLES.D_ZAI_ZAN_JITSU

            Case TARGET_TABLES.E_SAGYO.ToString()
                tgtTbl = TARGET_TABLES.E_SAGYO

            Case TARGET_TABLES.E_SAGYO_SIJI.ToString()
                tgtTbl = TARGET_TABLES.E_SAGYO_SIJI

            Case TARGET_TABLES.F_UNCHIN_TRS.ToString()
                tgtTbl = TARGET_TABLES.F_UNCHIN_TRS

            Case TARGET_TABLES.F_UNSO_L.ToString()
                tgtTbl = TARGET_TABLES.F_UNSO_L

            Case TARGET_TABLES.F_UNSO_M.ToString()
                tgtTbl = TARGET_TABLES.F_UNSO_M

            Case TARGET_TABLES.G_SEKY_MEISAI.ToString()
                tgtTbl = TARGET_TABLES.G_SEKY_MEISAI

            Case TARGET_TABLES.G_SEKY_MEISAI_PRT.ToString()
                tgtTbl = TARGET_TABLES.G_SEKY_MEISAI_PRT

            Case TARGET_TABLES.G_SEKY_TBL.ToString()
                tgtTbl = TARGET_TABLES.G_SEKY_TBL

            Case TARGET_TABLES.G_ZAIK_ZAN.ToString()
                tgtTbl = TARGET_TABLES.G_ZAIK_ZAN

                ' ''    'Case TARGET_TABLES.H_EDI_GOODSREP_TBL.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_EDI_GOODSREP_TBL

                ' ''    'Case TARGET_TABLES.H_EDI_PRINT.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_EDI_PRINT

                ' ''    'Case TARGET_TABLES.H_GOODS_EDI_BP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_GOODS_EDI_BP

                ' ''    'Case TARGET_TABLES.H_INKAEDI_DTL_BP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_DTL_BP

                ' ''    'Case TARGET_TABLES.H_INKAEDI_DTL_DPN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_DTL_DPN

                ' ''    'Case TARGET_TABLES.H_INKAEDI_DTL_NCGO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_DTL_NCGO

                ' ''    'Case TARGET_TABLES.H_INKAEDI_DTL_NIK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_DTL_NIK

                ' ''    'Case TARGET_TABLES.H_INKAEDI_DTL_NISSAN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_DTL_NISSAN

                ' ''    'Case TARGET_TABLES.H_INKAEDI_DTL_NSN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_DTL_NSN

                ' ''    'Case TARGET_TABLES.H_INKAEDI_DTL_UKM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_DTL_UKM

                ' ''    'Case TARGET_TABLES.H_INKAEDI_HED_BP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_HED_BP

                ' ''    'Case TARGET_TABLES.H_INKAEDI_HED_DPN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_HED_DPN

                ' ''    'Case TARGET_TABLES.H_INKAEDI_HED_NCGO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_HED_NCGO

                ' ''    'Case TARGET_TABLES.H_INKAEDI_HED_NSN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INKAEDI_HED_NSN

            Case TARGET_TABLES.H_INKAEDI_L.ToString()
                tgtTbl = TARGET_TABLES.H_INKAEDI_L

            Case TARGET_TABLES.H_INKAEDI_M.ToString()
                tgtTbl = TARGET_TABLES.H_INKAEDI_M

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_DTL_DIC_NEW.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_DTL_DIC_NEW

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_DTL_DOW.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_DTL_DOW

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_DTL_FJF.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_DTL_FJF

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_DTL_M3PL.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_DTL_M3PL

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_DTL_SMK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_DTL_SMK

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_DTL_TOHO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_DTL_TOHO

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_HED_DIC_NEW.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_HED_DIC_NEW

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_HED_DOW.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_HED_DOW

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_HED_FJF.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_HED_FJF

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_HED_SMK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_HED_SMK

                ' ''    'Case TARGET_TABLES.H_INOUTKAEDI_HED_TOHO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_INOUTKAEDI_HED_TOHO

                ' ''    'Case TARGET_TABLES.H_NRSBIN_DIC.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_NRSBIN_DIC

                ' ''    'Case TARGET_TABLES.H_NRSBIN_TOR.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_NRSBIN_TOR

                ' ''    'Case TARGET_TABLES.H_NRSCUST_DIC.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_NRSCUST_DIC

                ' ''    'Case TARGET_TABLES.H_NRSGOODS_DIC.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_NRSGOODS_DIC

                ' ''    'Case TARGET_TABLES.H_NRSGOODS_DNS.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_NRSGOODS_DNS

                ' ''    'Case TARGET_TABLES.H_OUTKA_L_BP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKA_L_BP

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_ASH.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_ASH

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_BP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_BP

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_BYK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_BYK

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_DIC.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_DIC

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_DNS.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_DNS

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_DPN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_DPN

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_DSP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_DSP

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_DSPAH.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_DSPAH

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_GODO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_GODO

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_HON.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_HON

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_JC.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_JC

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_JT.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_JT

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_KTK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_KTK

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_LNZ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_LNZ

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_MHM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_MHM

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_NCGO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_NCGO

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_NIK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_NIK

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_NKS.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_NKS

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_NSN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_NSN

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_OTK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_OTK

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_SFJ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_SFJ

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_SNK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_SNK

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_SNZ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_SNZ

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_TOR.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_TOR

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_DTL_UKM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_DTL_UKM

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_ASH.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_ASH

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_BP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_BP

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_DIC.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_DIC

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_DNS.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_DNS

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_DPN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_DPN

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_GODO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_GODO

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_HON.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_HON

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_JC.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_JC

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_NCGO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_NCGO

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_NSN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_NSN

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_OTK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_OTK

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_SFJ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_SFJ

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_TOR.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_TOR

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_HED_UKM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_HED_UKM

            Case TARGET_TABLES.H_OUTKAEDI_L.ToString()
                tgtTbl = TARGET_TABLES.H_OUTKAEDI_L

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_L_PRT_LNZ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_L_PRT_LNZ

            Case TARGET_TABLES.H_OUTKAEDI_M.ToString()
                tgtTbl = TARGET_TABLES.H_OUTKAEDI_M

                ' ''    'Case TARGET_TABLES.H_OUTKAEDI_M_PRT_LNZ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_OUTKAEDI_M_PRT_LNZ

                ' ''    'Case TARGET_TABLES.H_SENDEDI_BP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDEDI_BP

                ' ''    'Case TARGET_TABLES.H_SENDINEDI_DPN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDINEDI_DPN

                ' ''    'Case TARGET_TABLES.H_SENDINEDI_NCGO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDINEDI_NCGO

                ' ''    'Case TARGET_TABLES.H_SENDINEDI_NIK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDINEDI_NIK

                ' ''    'Case TARGET_TABLES.H_SENDINEDI_NSN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDINEDI_NSN

                ' ''    'Case TARGET_TABLES.H_SENDINEDI_UTI.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDINEDI_UTI

                ' ''    'Case TARGET_TABLES.H_SENDINOUTEDI_DOW.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDINOUTEDI_DOW

                ' ''    'Case TARGET_TABLES.H_SENDINOUTEDI_UKM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDINOUTEDI_UKM

                ' ''    'Case TARGET_TABLES.H_SENDMONTHLY_SNZ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDMONTHLY_SNZ

                ' ''    'Case TARGET_TABLES.H_SENDOUTEDI_ASH.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDOUTEDI_ASH

                ' ''    'Case TARGET_TABLES.H_SENDOUTEDI_DPN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDOUTEDI_DPN

                ' ''    'Case TARGET_TABLES.H_SENDOUTEDI_NCGO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDOUTEDI_NCGO

                ' ''    'Case TARGET_TABLES.H_SENDOUTEDI_NIK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDOUTEDI_NIK

                ' ''    'Case TARGET_TABLES.H_SENDOUTEDI_NSN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDOUTEDI_NSN

                ' ''    'Case TARGET_TABLES.H_SENDOUTEDI_SFJ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDOUTEDI_SFJ

                ' ''    'Case TARGET_TABLES.H_SENDOUTEDI_SNK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_SENDOUTEDI_SNK

                ' ''    'Case TARGET_TABLES.H_UNSOCO_EDI.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_UNSOCO_EDI

                ' ''    'Case TARGET_TABLES.H_ZAIKO_EDI_BP.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.H_ZAIKO_EDI_BP

                ' ''    'Case TARGET_TABLES.I_CONT_TRACK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_CONT_TRACK

                ' ''    'Case TARGET_TABLES.I_CONT_TRACK_LOG.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_CONT_TRACK_LOG

                ' ''    'Case TARGET_TABLES.I_DOW_SEIQ_PRT.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_DOW_SEIQ_PRT

                ' ''    'Case TARGET_TABLES.I_HAISO_UNCHIN_TRS.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_HAISO_UNCHIN_TRS

                ' ''    'Case TARGET_TABLES.I_HIKITORI_UNCHIN_MEISAI.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_HIKITORI_UNCHIN_MEISAI

                ' ''    'Case TARGET_TABLES.I_HON_TEIKEN.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_HON_TEIKEN

                ' ''    'Case TARGET_TABLES.I_HONEY_ALBAS_CHG.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_HONEY_ALBAS_CHG

                ' ''    'Case TARGET_TABLES.I_HONEY_SHIPTOCD_CHG.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_HONEY_SHIPTOCD_CHG

                ' ''    'Case TARGET_TABLES.I_MCPU_UNCHIN_CHK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_MCPU_UNCHIN_CHK

                ' ''    'Case TARGET_TABLES.I_NRC_KAISHU_TBL.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_NRC_KAISHU_TBL

                ' ''    'Case TARGET_TABLES.I_UKI_BUNRUI_MST.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_UKI_BUNRUI_MST

                ' ''    'Case TARGET_TABLES.I_UKI_HOKOKU.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_UKI_HOKOKU

                ' ''    'Case TARGET_TABLES.I_UKI_ZAIKO.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_UKI_ZAIKO

                ' ''    'Case TARGET_TABLES.I_UKI_ZAIKO_SUM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_UKI_ZAIKO_SUM

                ' ''    'Case TARGET_TABLES.I_UKIMA_SEKY_MEISAI.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_UKIMA_SEKY_MEISAI

                ' ''    'Case TARGET_TABLES.I_YOKO_UNCHIN_TRS.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_YOKO_UNCHIN_TRS

                ' ''    'Case TARGET_TABLES.I_YUSO_R.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_YUSO_R

                ' ''    'Case TARGET_TABLES.I_YUSO_R_SUM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.I_YUSO_R_SUM

                ' ''    'Case TARGET_TABLES.INST_TEST.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.INST_TEST

                ' ''    'Case TARGET_TABLES.M_BYK_GOODS.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_BYK_GOODS

                ' ''    'Case TARGET_TABLES.M_CHOKUSO_NIK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_CHOKUSO_NIK

                ' ''    'Case TARGET_TABLES.M_HINMOKU_FJF.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_HINMOKU_FJF

                ' ''    'Case TARGET_TABLES.M_HINMOKU_TRM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_HINMOKU_TRM

                ' ''    'Case TARGET_TABLES.M_KOKYAKU_TRM.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_KOKYAKU_TRM

                ' ''    'Case TARGET_TABLES.M_SEHIN_NIK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_SEHIN_NIK

                ' ''    'Case TARGET_TABLES.M_SET_GOODS_LNZ.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_SET_GOODS_LNZ

                ' ''    'Case TARGET_TABLES.M_TOKUI_FJF.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_TOKUI_FJF

                ' ''    'Case TARGET_TABLES.M_TOKUI_NIK.ToString()
                ' ''    '    tgtTbl = TARGET_TABLES.M_TOKUI_NIK


            Case Else
                tgtTbl = TARGET_TABLES.NON
        End Select

        Return tgtTbl

    End Function

#End Region

#Region "元データ削除チェック"

    ''' <summary>
    ''' データ退避処理（元データ削除チェック）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteDataCheck(ByVal ds As DataSet) As DataSet

        Dim rtnCount As Integer = 0

        For Each row As DataRow In ds.Tables(TBLNM_TARGET_TABLES).Rows

            rtnCount = Me.DeleteCheckSQLCreate(ds, Me.GetTableNM(row("TableNM").ToString()))

            'ログ出力
            If rtnCount = RTN_STS_ERROR Then
                Me.SetDtlLog(ds, row("TableNM").ToString())
                'エラーの場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_DELETE_NG
                'ヘッダも更新
                If "M".Equals(row("TableIDGroup").ToString()) = True Then
                    ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_TIMING_KB") = KBN_TIIMING_MST
                Else
                    ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_TIMING_KB") = KBN_TIIMING_TRN
                End If
                ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_RESULT_KB") = KBN_KEKKA_ERROR
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
                Me.SetMSGStore("E547", New String() {MSG_DELETE_NG}, ds.Tables(TBLNM_TBLIN).Rows(0).Item("CUST_CD_L").ToString())
                Exit For
            ElseIf rtnCount = RTN_STS_NOTHING Then
                'スルー
            Else
                Me.SetDtlLog(ds, row("TableNM").ToString())
                '正常の場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("CNT") = rtnCount
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_DELETE_OK
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 削除可能チェックSQL
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>各種テーブル新規登録SQLの構築・発行</remarks>
    Private Function DeleteCheckSQLCreate(ByVal ds As DataSet, ByVal tgtTbl As TARGET_TABLES) As Integer

        If TARGET_TABLES.NON = tgtTbl = True Then
            Return RTN_STS_NOTHING
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me.GetDelecteCheckSQL(tgtTbl, DeleteCheckMode.DelCheck), Me._Row.Item("NRS_BR_CD").ToString()))

        '件数が膨大なため、タイムアウトを設定しない
        cmd.CommandTimeout = 0

        'パラメータ設定
        'Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetTablesComParameter(Me._Row, Me._SqlPrmList, tgtTbl)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", String.Concat("DeleteCheck_", tgtTbl.ToString()), cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        Dim successFlg As Integer
        reader.Read()
        successFlg = Convert.ToInt32(reader("SUCCESS_FLG"))
        reader.Close()

        If successFlg = 1 Then
            Return RTN_STS_SUCCESS
        Else
            Return RTN_STS_ERROR
        End If

    End Function


#End Region

#Region "元データ削除"

    ''' <summary>
    ''' データ退避処理（元データの削除）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Dim rtnCount As Integer = 0

        For Each row As DataRow In ds.Tables(TBLNM_TARGET_TABLES).Rows

            rtnCount = Me.DeleteSQLCreate(ds, Me.GetTableNM(row("TableNM").ToString()))
            If rtnCount <> RTN_STS_NOTHING Then
                Me.SetDtlLog(ds, row("TableNM").ToString())
                'ログ出力
                '正常の場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("CNT") = rtnCount
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_SUCCESS_DELETE
                Me.StartInputDetailLog(ds, row("TableNM").ToString())

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 削除SQL
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>各種テーブル新規登録SQLの構築・発行</remarks>
    Private Function DeleteSQLCreate(ByVal ds As DataSet, ByVal tgtTbl As TARGET_TABLES) As Integer

        If TARGET_TABLES.NON = tgtTbl = True Then
            Return RTN_STS_NOTHING
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNmEscapeForDelete(Me.GetDelecteSQL(tgtTbl), Me._Row.Item("NRS_BR_CD").ToString()))

        '件数が膨大なため、タイムアウトを設定しない
        cmd.CommandTimeout = 30000

        'パラメータ設定
        'Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetTablesComParameter(Me._Row, Me._SqlPrmList, tgtTbl)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", String.Concat("Delete_", tgtTbl.ToString()), cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return rtn

    End Function


#End Region

#End Region

#Region "戻し処理"

#Region "Insert"

    ''' <summary>
    ''' データ戻し処理（本DBへの登録）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ModoshiData(ByVal ds As DataSet) As DataSet

        Dim rtnCount As Integer = 0

        For Each row As DataRow In ds.Tables(TBLNM_TARGET_TABLES).Rows

            rtnCount = Me.InsertModoshiData(ds, Me.GetTableNM(row("TableNM").ToString()))

            'ログ出力
            If rtnCount = RTN_STS_ERROR Then
                Me.SetDtlLog(ds, row("TableNM").ToString())
                'エラーの場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_ERR_INSERT
                'ヘッダも更新
                If "M".Equals(row("TableIDGroup").ToString()) = True Then
                    ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_TIMING_KB") = KBN_TIIMING_MST
                Else
                    ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_TIMING_KB") = KBN_TIIMING_TRN
                End If
                ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_RESULT_KB") = KBN_KEKKA_ERROR
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
                Me.SetMSGStore("E547", New String() {MSG_ERR_INSERT}, ds.Tables(TBLNM_TBLIN).Rows(0).Item("CUST_CD_L").ToString())
                Exit For
            ElseIf rtnCount = RTN_STS_NOTHING Then
                'スルー
            Else
                Me.SetDtlLog(ds, row("TableNM").ToString())
                '正常の場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("CNT") = rtnCount
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_SUCCESS_INSERT
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 退避データのINSERT
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>各種テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertModoshiData(ByVal ds As DataSet, ByVal tgtTbl As TARGET_TABLES) As Integer

        If TARGET_TABLES.NON = tgtTbl = True Then
            Return RTN_STS_NOTHING
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNmModoshiForInsert(Me.GetInsertSQL(tgtTbl), Me._Row.Item("NRS_BR_CD").ToString()))

        'タイムアウトの設定をなしにする
        cmd.CommandTimeout = 0

        'パラメータ設定
        'Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetTablesComParameter(Me._Row, Me._SqlPrmList, tgtTbl)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", String.Concat("ModoshiInsert", tgtTbl.ToString()), cmd)

        'SQLの発行
        Return MyBase.GetInsertResult(cmd)

    End Function

#End Region

#Region "退避データ削除"

    ''' <summary>
    ''' データ退避処理（元データの削除）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteModoshiData(ByVal ds As DataSet) As DataSet

        Dim rtnCount As Integer = 0

        For Each row As DataRow In ds.Tables(TBLNM_TARGET_TABLES).Rows

            rtnCount = Me.DeleteModoshiData(ds, Me.GetTableNM(row("TableNM").ToString()))
            If rtnCount <> RTN_STS_NOTHING Then
                Me.SetDtlLog(ds, row("TableNM").ToString())
                'ログ出力
                '正常の場合
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("CNT") = rtnCount
                ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count - 1)("MESSAGE") = MSG_SUCCESS_DELETE
                Me.StartInputDetailLog(ds, row("TableNM").ToString())
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 削除SQL
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>各種テーブル新規登録SQLの構築・発行</remarks>
    Private Function DeleteModoshiData(ByVal ds As DataSet, ByVal tgtTbl As TARGET_TABLES) As Integer

        If TARGET_TABLES.NON = tgtTbl = True Then
            Return RTN_STS_NOTHING
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TBLNM_TBLIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNmModoshiForDelete(Me.GetDelecteSQL(tgtTbl), Me._Row.Item("NRS_BR_CD").ToString()))

        '件数が膨大なため、タイムアウトを設定しない
        cmd.CommandTimeout = 30000


        'パラメータ設定
        'Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetTablesComParameter(Me._Row, Me._SqlPrmList, tgtTbl)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", String.Concat("DeleteModoshi_", tgtTbl.ToString()), cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return rtn

    End Function


#End Region

#End Region

#Region "荷主マスタ更新"

    ''' <summary>
    ''' 荷主マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ログテーブルの更新SQLの構築・発行</remarks>
    Private Function UpdateM_CUST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("M_CUST_UPD")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMJ020DAC.SQL_UPDATE_M_CUST, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetMCUSTParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", "UpdateM_CUST", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMCUSTParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BACKUP_FLG", .Item("BACKUP_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "Log"

    ''' <summary>
    ''' データ退避ログテーブルヘッダ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ログテーブル新規登録SQLの構築・発行</remarks>
    Private Function StartInputLog(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMJ020IN_J_DATA_ESC_LOG_HEAD")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMJ020DAC.SQL_INSERT_J_DATA_ESC_LOG_HEAD, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetLogHeadComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", "StartInputLog", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' データ退避ログテーブルヘッダ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ログテーブルの更新SQLの構築・発行</remarks>
    Private Function EndInputLog(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMJ020IN_J_DATA_ESC_LOG_HEAD")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMJ020DAC.SQL_UPDATE_J_DATA_ESC_LOG_HEAD, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetLogHeadComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", "InputLog", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' データ退避ログテーブルヘッダの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetLogHeadComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@BATCH_NO", .Item("BATCH_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OPE_USER_CD", .Item("OPE_USER_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYORI_KB", .Item("SYORI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PROC_DATE", .Item("PROC_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_UPD_DATE", .Item("LAST_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXEC_STATE_KB", .Item("EXEC_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXEC_RESULT_KB", .Item("EXEC_RESULT_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MESSAGE", .Item("MESSAGE").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXEC_TIMING_KB", .Item("EXEC_TIMING_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXEC_START_DATE", .Item("EXEC_START_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXEC_START_TIME", .Item("EXEC_START_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXEC_END_DATE", .Item("EXEC_END_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXEC_END_TIME", .Item("EXEC_END_TIME").ToString(), DBDataType.CHAR))


        End With

    End Sub

    ''' <summary>
    ''' データ退避ログテーブルヘッダ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ログテーブル新規登録SQLの構築・発行</remarks>
    Private Function StartInputDetailLog(ByVal ds As DataSet, ByVal tblNM As String) As DataSet

        'DataSetのIN情報を取得
        Dim inHTbl As DataTable = ds.Tables("LMJ020IN_J_DATA_ESC_LOG_HEAD")
        Dim inDTbl As DataTable = ds.Tables("LMJ020IN_J_DATA_ESC_LOG_DTL")
        'INTableの条件rowの格納
        Dim hRow As DataRow = inHTbl.Rows(0)
        Me._Row = inDTbl.Rows(inDTbl.Rows.Count - 1)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMJ020DAC.SQL_INSERT_J_DATA_ESC_LOG_DTL, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetLogDtlComParameter(Me._Row, hRow, tblNM, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMJ020DAC", "InputDtlLog", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' データ退避明細ログテーブルヘッダの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetLogDtlComParameter(ByVal conditionRow As DataRow, ByVal headRow As DataRow, ByVal tblNM As String, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@BATCH_NO", headRow.Item("BATCH_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", headRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OPE_USER_CD", headRow.Item("OPE_USER_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYORI_KB", headRow.Item("SYORI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PG_ID", tblNM, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CNT", .Item("CNT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@MESSAGE", .Item("MESSAGE").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", systemUserID, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetSysdataParameter(prmList, dataSetNm)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 各種テーブルのの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetTablesComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal tbtTbl As TARGET_TABLES)

        With conditionRow
            Select Case tbtTbl
                Case Else
                    '一般的なテーブル
                    prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

            End Select

        End With

    End Sub

    ''' <summary>
    ''' ログの初期値
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetDtlLog(ByVal ds As DataSet, ByVal tblNM As String) As DataSet

        'ログの書き込み
        Dim row As DataRow = ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).NewRow

        row("BATCH_NO") = ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("BATCH_NO").ToString()
        row("NRS_BR_CD") = ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("NRS_BR_CD").ToString()
        row("OPE_USER_CD") = ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("OPE_USER_CD").ToString()
        row("SYORI_KB") = ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("SYORI_KB").ToString()
        row("REC_NO") = Me.GetRecNo(ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Count + 1)
        row("PG_ID") = tblNM
        row("CNT") = 0
        row("MESSAGE") = String.Empty

        ds.Tables(TBLNM_J_DATA_ESC_LOG_DTL).Rows.Add(row)

        Return ds

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="errCd"></param>
    ''' <param name="msgPara"></param>
    ''' <param name="custCd"></param>
    ''' <remarks></remarks>
    Private Sub SetMSGStore(ByVal errCd As String, ByVal msgPara As String(), ByVal custCd As String)

        MyBase.SetMessageStore("00" _
                       , errCd _
                       , msgPara _
                       , "" _
                       , "荷主コード" _
                       , custCd)

    End Sub
#End Region

#Region "設定処理"

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String, Optional ByVal forcingArchiveFlg As Boolean = False) As String

        If forcingArchiveFlg = False Then

            'トラン系スキーマ名設定
            sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

            'マスタ系スキーマ名設定
            sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

            '退避系スキーマ名設定
            sql = sql.Replace("$LM_ESCAPE_MST$", LM_ARCHIVE)

            '退避系スキーマ名設定
            sql = sql.Replace("$LM_ESCAPE_TRN$", LM_ARCHIVE)

        Else
            'トラン系スキーマ名設定
            sql = sql.Replace("$LM_TRN$", LM_ARCHIVE)

            'マスタ系スキーマ名設定
            sql = sql.Replace("$LM_MST$", LM_ARCHIVE)

            '退避系スキーマ名設定
            sql = sql.Replace("$LM_ESCAPE_MST$", LM_ARCHIVE)

            '退避系スキーマ名設定
            sql = sql.Replace("$LM_ESCAPE_TRN$", LM_ARCHIVE)

        End If

        Return sql

    End Function

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNmEscapeForDelete(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_ESCAPE_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_ESCAPE_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNmModoshiForDelete(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", LM_ARCHIVE)

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", LM_ARCHIVE)

        '退避系スキーマ名設定
        sql = sql.Replace("$LM_ESCAPE_MST$", LM_ARCHIVE)

        '退避系スキーマ名設定
        sql = sql.Replace("$LM_ESCAPE_TRN$", LM_ARCHIVE)

        Return sql

    End Function

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNmModoshiForInsert(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", LM_ARCHIVE)

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", LM_ARCHIVE)

        '退避系スキーマ名設定
        sql = sql.Replace("$LM_ESCAPE_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        '退避系スキーマ名設定
        sql = sql.Replace("$LM_ESCAPE_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        Return sql

    End Function
#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#End Region

    ''' <summary>
    ''' 0埋めで返却
    ''' </summary>
    ''' <param name="cnt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRecNo(ByVal cnt As Integer) As String

        Return cnt.ToString().PadLeft(10, CChar("0"))

    End Function
#End Region

#End Region

End Class
